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
using System.Collections.Generic;

namespace FoodandBeverage.Forms
{
    public partial class FrmReservation : Form
    {
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

        #region Declaration 
        ClsGeneral ClsGeneral = new ClsGeneral();
        ClsDocument clsDocument = new ClsDocument();
        long BranchId = FrmDashboard.BranchId;
        public int finalqty = 0;
        public int BookmasterId = 0;

        public bool variflag = true, addonflag = true;
        private Button previousClickedButton = null;
        public List<Variation_addon_List> Checked_VariationAddon_List = new List<Variation_addon_List>();
        public int Global_ProductId = 0;
        Color seleproduct_buttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
        public int Current_menuGrid_rowno = 0;
        private const string PlaceholderText = "Description..";
        public int maxLength_variation = 3;
        private int yourTargetColumnIndex = 1;

        public bool Expireflag = false;
        public bool PaymentCFlage = false;

        public DateTime modifiedEndDate_before = DateTime.Now;
        public DateTime modifiedEndDate_after = DateTime.Now;
        public DateTime ExpdateNow = DateTime.Now;
        public DateTime Bookdate = DateTime.Now;
        public DateTime Currentdate = DateTime.Now;

        #endregion

        #region Grid Declaration
        public static int iraction = 0;
        public static int itid = 1;
        public static int iTBookingDetailId = 2;
        public static int itablename = 3;
        public static int iprice = 4;
        public static int icapacity = 5;
        public static int idescription = 6;

        public static int iaction = 0;
        public static int islno = 1;
        public static int imid = 2;
        public static int imBookingDetailId = 3;
        public static int imname = 4;
        public static int imdescription = 5;
        public static int imprice = 6;
        public static int iqty = 7;
        public static int iTaxper = 8;
        public static int iFormType = 9;
        public static int iTax = 10;
        public static int iTaxType = 11;
        public static int istaxamt = 12;
        public static int isamount = 13;
        public static int imgst = 14;
        public static int icgst = 15; 
        public static int isgst = 16;
        public static int ivat = 17;
        public static int iprate = 18;
        public static int iunit = 19;
        public static int iitemcode = 20;
        public static int iprid = 21;
        public static int imrp = 22;
        public static int itabletype = 23;
        public static int iproductType = 24;
        public static int imarketprice = 25;
        public static int itaxsettingid = 26;
        public static int iqtyadd = 27;
        public static int idisamt = 28;
        public static int idisper = 29;
        public static int iQOH = 30;
        public static int ibackuptaxamt = 31;

        public static int iAddoncount = 31;
        public static int ivid = 32;
        public static int iVarationName = 33;
        public static int iPavid = 34;
        public static int iAvid = 35;
        public static int iAddonstatus = 36;
        public static int iaddoncode = 37;

        public static int slno = 0;
        public static int prId = 1;
        public static int iiDescription = 2;
        public static int Srate = 3;
        public static int qt = 4;

        public static int mslnono = 0;
        public static int mNameno = 1;
        public static int mPriceno = 2;
        public static int mproducttypenameno = 3;
        public static int mformsenumno = 4;
        public static int mTaxno = 5;
        public static int mTaxTypeno = 6;
        public static int mproducticono = 7;
        public static int midno = 8;

        #endregion

        private void FrmReservation_Load(object sender, EventArgs e)
        {
            try
            {
                this.SuspendLayout();
                string dtfrom = dtpBooking.Value.ToString("yyyy-MM-dd") + " " + dtp_from_time.Value.ToString("HH:mm:ss");

                #region Grid Design
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
                #endregion

                panelProductPopup.Visible = false;
                txt_HR.Text = Convert.ToString(FrmLogin.BookingInterwell);
                combofill();
                FillCombo();
                dgvforms.Rows.Add(1);
                dgvmenu.Rows.Add(1);
                int rc = dgvmenu.RowCount;
                dtp_from_time.MinDate = DateTime.Now;
                dtpBooking.MinDate = DateTime.Today;
                dtp_from_time.Format = DateTimePickerFormat.Custom;
                dtp_from_time.CustomFormat = "hh:mm:tt";

                pnl_menu.Visible = false;

                #region Setting Permission on Save Button
                FrmDashboard frmDash = new FrmDashboard();
                if (frmDash.ChkWritePermission(Convert.ToInt32(formsenum.Reservation)))
                {
                    mbtnSave.Enabled = true;
                }
                else
                {
                    mbtnSave.Enabled = false;
                }
                #endregion

                #region Setting Placeholder
                oddesc.Text = PlaceholderText;
                oadesc.Text = PlaceholderText;
                oddesc.ForeColor = Color.Gray;
                oadesc.ForeColor = Color.Gray;

                txtname_search.Text = "Food Name .....";
                txtname_search.ForeColor = Color.Gray;
                #endregion

                #region Setting Blank icon on Grids
                DataGridViewImageCell cell = (DataGridViewImageCell)dgvmenu.Rows[0].Cells[iaction];
                cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                cell.Value = null;
                cell.Value = Properties.Resources.orderview_blank;

                DataGridViewImageCell cell1 = (DataGridViewImageCell)dgvforms.Rows[0].Cells[iraction];
                cell1.ImageLayout = DataGridViewImageCellLayout.Zoom;
                cell1.Value = null;
                cell1.Value = Properties.Resources.orderview_blank;
                #endregion

                #region Setting F8 For Table List Table Grid
                dgvforms.Focus();
                dgvforms.CurrentCell = dgvforms.Rows[0].Cells[itablename];
                if (Convert.ToInt32(dgvforms.Rows[0].Cells[itid].Value) == 0 || Convert.ToInt32(dgvforms.Rows[0].Cells[itid].Value) == null)
                {
                    dgvforms.Rows[0].Cells[itablename].Value = "F8 For Table List";
                    dgvforms.Rows[0].Cells[itablename].Style.ForeColor = Color.Red;
                }
                #endregion

                lblBAmt_Pay.Visible = false;
                label21.Visible = false;

                lblSTotal.Text = "0";

                dgvforms.BeginEdit(true);

                this.ResumeLayout();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("Reservation Page FrmReservation_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("Reservation Page textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void ApplyRoundedTextBox(TextBox textBox, int radius)
        {
            try
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("Reservation Page ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }
        #endregion

        public void clear()
        {
            try
            {
                chb_active.Checked = true;
                btn_rev_cancel.Visible = false;
                PaymentCFlage = false;
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
                lblSTotal.Text = "0";
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
                lblBAmt_Pay.Text = "0.00";
                discountper.Text = "0.00";
                label21.Visible = false;
                lblBAmt_Pay.Visible = false;
                panelProductPopup.Visible = false;
                dgvmenu.Enabled = true;
                txtadvpa.Enabled = true;
                dgvmenu.Focus();
                dtp_from_time.MinDate = DateTime.Now;
                dtpBooking.MinDate = DateTime.Today;
                dtpBooking.Value = DateTime.Now;
                pnl_menu.Visible = false;
                btn_rev_cancel.Visible = false;
                Current_menuGrid_rowno = 0;
                tlp_reg_design.Visible = true;
                btn_res_details.BackgroundImage = null;
                btn_res_details.BackgroundImage = FoodandBeverage.Properties.Resources.res_hide_button;

                oddesc.Text = PlaceholderText;
                oadesc.Text = PlaceholderText;
                oddesc.ForeColor = Color.Gray;
                oadesc.ForeColor = Color.Gray;

                if (tc_rese_menu.SelectedTab == tp_menu)
                {
                    dgvforms.Focus();
                    if (dgvmenu.Rows.Count > 0)
                    {
                        dgvmenu.CurrentCell = dgvmenu.Rows[0].Cells[imname];
                        dgvmenu.BeginEdit(true);
                    }
                }
                else if (tc_rese_menu.SelectedTab == tp_reservation)
                {
                    dgvmenu.Focus();
                    if (dgvforms.Rows.Count > 0)
                    {
                        dgvforms.CurrentCell = dgvforms.Rows[0].Cells[itablename];
                        dgvforms.BeginEdit(true);

                    }
                }

                DataGridViewImageCell cell = (DataGridViewImageCell)dgvmenu.Rows[0].Cells[iaction];
                cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                cell.Value = null;
                cell.Value = Properties.Resources.orderview_blank;

                DataGridViewImageCell cell1 = (DataGridViewImageCell)dgvforms.Rows[0].Cells[iraction];
                cell1.ImageLayout = DataGridViewImageCellLayout.Zoom;
                cell1.Value = null;
                cell1.Value = Properties.Resources.orderview_blank;

                tlp_textboxes.Enabled = true;
                tlp_reg_form.Enabled = true;
                mbtnSave.Visible = true;

                Expireflag = false;

                if (blinkTimer.Enabled)
                    blinkTimer.Stop();

                lbl_infoExpire.Visible = false;
                label21.Text = "Balance To Pay";
                lblpayment_status.Visible = false;
                txtfinalamt.Text = "0.00";
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("Reservation Page clear()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        public bool CheckEmailId(Form form, string EmailId)
        {
            try
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("Reservation Page CheckEmailId()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
                return false;
            }
        }

        public bool capasity()
        {
            try
            {
                if (int.TryParse(txtNOP.Text, out int capacity))
                {
                    int totalCapacity = 0;
                    foreach (DataGridViewRow row in dgvforms.Rows)
                    {
                        if (!row.IsNewRow && row.Cells[icapacity].Value != null)
                        {
                            if (int.TryParse(row.Cells[icapacity].Value.ToString(), out int personCapacity))
                            {
                                totalCapacity += personCapacity;
                            }
                        }
                    }
                    if (capacity > totalCapacity)
                    {
                        CMessageBox.Show("Table capacity is not sufficient for the quantity of people.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        RecallTable();
                        return false;
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("Reservation Page capasity()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
                return false;
            }
        }
        public void ShowuserDetails()
        {
            try
            {
                if (tlp_reg_design.Visible == false)
                    tlp_reg_design.Visible = true;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("Reservation Page ShowuserDetails()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }
        }
        public bool MandatoryCheck()
        {
            try
            {
                if (String.IsNullOrEmpty(txtBookmasterId.Text))
                    txtBookmasterId.Text = "0";
                if (String.IsNullOrEmpty(txt_PaymentTimes.Text))
                    txt_PaymentTimes.Text = "0";
                if (String.IsNullOrEmpty(txt_CashGivenAmt.Text))
                    txt_CashGivenAmt.Text = "0";

                bool data = true;
                string dtfrom = dtpBooking.Value.ToString("yyyy-MM-dd") + " " + dtp_from_time.Value.ToString("HH:mm:ss");
                DateTime selectedDateTime = DateTime.Parse(dtfrom);
                DateTime selectedDate = dtpBooking.Value.Date;
                DateTime selectedTime = dtp_from_time.Value;
                DateTime currentDate = DateTime.Now.Date;
                DateTime currentTime = DateTime.Now;

                if (txtNam.Text == "" || txtNam.Text == string.Empty)
                {
                    CMessageBox.Show("Please enter Customer Name. This field is mandatory.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ShowuserDetails();
                    txtNam.Focus();
                    return false;
                }
                else if (txtMob.Text == "" || txtMob.Text == null)
                {
                    DialogResult result = CMessageBox.Show("Please enter mobile number. This field is mandatory.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ShowuserDetails();
                    txtMob.Focus();
                    return false;
                }
                else if (txtEmail.Text == "" || txtEmail.Text == string.Empty)
                {
                    CMessageBox.Show("Please enter email address. This field is mandatory.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ShowuserDetails();
                    txtEmail.Focus();
                    return false;
                }
                else if (cboGender.SelectedIndex == 0 || cboGender.SelectedIndex == -1)
                {
                    CMessageBox.Show("Please select gender. This information is mandatory.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ShowuserDetails();
                    cboGender.Focus();
                    return false;
                }
                else if (txtNOP.Text == "" || txtNOP.Text == string.Empty)
                {
                    CMessageBox.Show("Please enter no of people. This field is mandatory.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ShowuserDetails();
                    txtNOP.Focus();
                    return false;
                }
                else if (selectedDateTime < DateTime.Now && Convert.ToInt32(txtBookmasterId.Text) == 0)
                {
                    if (selectedDate < currentDate)
                    {
                        CMessageBox.Show("The selected date cannot be in the past. Please select a future date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ShowuserDetails();
                        dtpBooking.Focus();
                        return false;
                    }
                    else if (selectedDate == currentDate)
                    {
                        if (selectedTime.TimeOfDay < currentTime.TimeOfDay)
                        {
                            CMessageBox.Show("The selected time cannot be in the past. Please select a future time.", "Invalid Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            ShowuserDetails();
                            dtp_from_time.Focus();
                            return false;
                        }
                    }
                }
                else if (cboBtype.SelectedIndex == 0 || cboBtype.SelectedIndex == -1)
                {
                    CMessageBox.Show("Please select Booking Type. This information is mandatory.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ShowuserDetails();
                    cboBtype.Focus();
                    return false;
                }
                else if (txtAddress.Text == "" || txtAddress.Text == string.Empty)
                {
                    CMessageBox.Show("Please enter  address. This field is mandatory.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ShowuserDetails();
                    txtAddress.Focus();
                    return false;
                }
                if (Convert.ToString(dgvforms.Rows[0].Cells[itablename].Value) == "F8 For Table List" || Convert.ToString(dgvforms.Rows[0].Cells[itablename].Value) == "")
                {
                    bool flag = true;
                    for (int i = 0; i < dgvforms.Rows.Count; i++)
                    {
                        if (Convert.ToString(dgvforms.Rows[i].Cells[itablename].Value) != "F8 For Table List" && Convert.ToString(dgvforms.Rows[i].Cells[itablename].Value) != "")
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                    {
                        CMessageBox.Show("Please select a table first.", "Required Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        dgvforms.Focus();
                        tc_rese_menu.SelectedTab = tp_reservation;
                        RecallTable();
                        return false;
                    }

                }
                else if (lblGTotal.Text == "00")
                {
                    CMessageBox.Show("Fill The Balance Details", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (data == true)
                {
                    return capasity();
                }
                else
                {
                    return false;
                }

                for (int i = 0; i < dgvmenu.Rows.Count - 1; i++)
                {
                    if (Convert.ToString(dgvmenu.Rows[i].Cells[iqty].Value) == "0" || Convert.ToString(dgvmenu.Rows[i].Cells[iqty].Value) == "")
                    {
                        if (Convert.ToString(dgvmenu.Rows[i].Cells[0].Value) != "")
                        {
                            CMessageBox.Show("Quantity cannot be less than one. Please enter a valid quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            dgvmenu.Focus();
                            RecallTable();
                            return false;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("Reservation Page MandatoryCheck()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
                return false;
            }
        }
        public void FillCombo()
        {
            try
            {
                // Gender
                BllSearch<DmnComboBox> bllGender = new BllSearch<DmnComboBox>();
                DmnSearch dmngender = new DmnSearch
                {
                    Select = "GTypeSubId,Caption",
                    Table = "tblGeneralOptions",
                    WhereNdGroupBy = "Where GTypeId = 25",
                    OrderBy = "order by Value"
                };
                var gender = bllGender.GetSearchData("prdComboFill", dmngender);
                cboGender.DataSource = gender;
                cboGender.DisplayMember = "Value";
                cboGender.ValueMember = "Id";

                // Reservation
                BllSearch<DmnComboBox> bllReservation = new BllSearch<DmnComboBox>();
                DmnSearch dmnReservation = new DmnSearch
                {
                    Select = "GTypeSubId,Caption",
                    Table = "tblGeneralOptions",
                    WhereNdGroupBy = "Where GTypeId = 67",
                    OrderBy = "order by Value"
                };
                var table = bllReservation.GetSearchData("prdComboFill", dmnReservation);
                cboBtype.DataSource = table;
                cboBtype.DisplayMember = "Value";
                cboBtype.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation FillCombo()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
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
            try
            {
                if (e.ColumnIndex == itablename)
                {
                    if (Convert.ToInt32(dgvforms.Rows[e.RowIndex].Cells[itid].Value) == 0 || Convert.ToString(dgvforms.Rows[e.RowIndex].Cells[itid].Value) == null)
                    {
                        dgvforms.Rows[e.RowIndex].Cells[itablename].Value = "F8 for Table LIST";
                        dgvforms.Rows[e.RowIndex].Cells[itablename].Style.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("Reservation page dgvforms_CellEnter()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        public void menu(int BranchId)
        {
            try
            {
                BllSearch<DmnGridFields> _bllSearch = new BllSearch<DmnGridFields>();
                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                panelProductPopup.Visible = true;
                int panelWidthPercentage = 80;
                int panelheightPercentage = 70;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                panelProductPopup.Size = new Size(panelWidth, panelHeight);
                panelProductPopup.Location = new Point(this.ClientSize.Width / 2 - panelProductPopup.Size.Width / 2, (this.ClientSize.Height / 3 - panelProductPopup.Size.Height / 3));
                dgvItemPop.Rows.Clear();
                _objSearch.Select = "p.ProductId as Id , Name as Field1,p.Price as Field2,  'Menu' as Field3, " + Convert.ToInt32(formsenum.Menu) + " Field4 ,p.Tax as Field5,p.TaxType as Field6, " +
                    "(select count(*) from tblOfferMaster ofm left join tblOfferDetails ofd on ofd.Omid=ofm.Omid and ofd.Status=1 where ofm.Status=1 AND OfferType=0 AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME() and ofd.ProductId= m.ProductId) as Field7";
                _objSearch.Table = " tblMenu m   " +
                    "left join tblProduct p on m.ProductId = p.ProductId  where m.Status = 1 and p.Status = 1 " +
                    "UNION select i.ItemId as Id, i.Itemname as Field1,(Select Top(1)Srate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field2,'Item' as Field3, " + Convert.ToInt32(formsenum.Item) + " Field4 ,i.Tax as Field5,i.TaxType as Field6,0 Field7 from tblItem i  where Saletype=1 " +
                    "UNION select ofm.Omid as Id,ofm.OfferName Field1,ofm.FinalPrice Field2,'Offer' as Field3," + Convert.ToInt32(formsenum.Discount_And_Offer) + " Field4 , ofm.Tax Field5,ofm.TaxType Field6,0 Field7 from tblOfferMaster ofm " +
                    "LEFT JOIN tblOfferDetails ofd on ofm.Omid=ofd.Omid ";
                _objSearch.WhereNdGroupBy = " Where ofd.Status=1 and ofm.OfferType = 1";
                _objSearch.OrderBy = "";
                var gridSource = _bllSearch.GetSearchData("prdSearch", _objSearch);
                if (gridSource.Count > 0)
                {
                    tlp_main.Enabled = false;
                    dgvItemPop.Rows.Add(gridSource.Count);
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        dgvItemPop.Rows[i].Cells[mslnono].Value = i + 1;
                        dgvItemPop.Rows[i].Cells[mNameno].Value = gridSource[i].Field1;
                        dgvItemPop.Rows[i].Cells[mPriceno].Value = gridSource[i].Field2;
                        dgvItemPop.Rows[i].Cells[mproducttypenameno].Value = gridSource[i].Field3;
                        dgvItemPop.Rows[i].Cells[mformsenumno].Value = gridSource[i].Field4;
                        dgvItemPop.Rows[i].Cells[mTaxno].Value = gridSource[i].Field5;
                        dgvItemPop.Rows[i].Cells[midno].Value = gridSource[i].Id;

                        dgvItemPop.Columns[mNameno].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        dgvItemPop.Columns[mPriceno].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                        DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgvItemPop.Rows[i].Cells[mproducticono];
                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        if (Convert.ToInt32(gridSource[i].Field4) == Convert.ToInt32(formsenum.Discount_And_Offer))
                        {
                            cellcolumntype.Value = Properties.Resources.kitchengridcomboicon;
                            cellcolumntype.ToolTipText = gridSource[i].Field1;
                        }
                        else if (Convert.ToInt32(gridSource[i].Field7) > 0)
                        {
                            cellcolumntype.Value = Properties.Resources.offer_button;
                            cellcolumntype.ToolTipText = gridSource[i].Field1;
                        }
                        else
                        {
                            if (Convert.ToInt32(dgvItemPop.Rows[i].Cells[mslnono].Value) % 2 == 0)
                                cellcolumntype.Value = Properties.Resources.orderview_blank_2;
                            else
                                cellcolumntype.Value = Properties.Resources.orderview_blank;
                        }

                        //else if (Convert.ToInt32(gridSource[i].Field4) == Convert.ToInt32(formsenum.Addon))
                        //{
                        //    cellcolumntype.Value = Properties.Resources.addonbutton;
                        //    cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        //    cellcolumntype.ToolTipText = gridSource[i].Field1;
                        //}
                    }
                }
                panelProductPopup.Show();
                dgvItemPop.Focus();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("Reservation page menu()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        public void Tablepop(int BranchId)
        {
            try
            {
                string dtfrom = dtpBooking.Value.ToString("yyyy-MM-dd") + " " + dtp_from_time.Value.ToString("HH:mm:ss");
                int r = dgvforms.CurrentCell.RowIndex;
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                DmnSearch _objSearch = new DmnSearch
                {
                    ColumnHeaders = "Tid,TableCode,Price,Capcity,Description",
                    Select = "Tid as Id,TableCode as Field1,Price as Field2,Capcity as Field3,Description as Field4",
                    Table = "tblTable_info",
                    WhereNdGroupBy = "WHERE Tid NOT IN (SELECT MTID FROM tblbookMaster m LEFT JOIN tblBookDetails d ON m.BookmasterId = d.BookmasterId and d.status=1 WHERE BookingDateTime >= DATEADD(MINUTE,-" + txt_HR.Text + ", '" + dtfrom + "') AND BookingDateTime <= DATEADD(MINUTE, " + txt_HR.Text + ", '" + dtfrom + "') and m.Status= 1) and Status=1 AND Tid NOT IN (SELECT Tid FROM tblOrderMaster WHERE CreatedDateTime >= DATEADD(MINUTE,-" + txt_HR.Text + ", '" + dtfrom + "') and Status= 1)"
                };
                _objSearch.OrderBy = " ";
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);
                    }
                }
                if (returnValue != "")
                {
                    BllSearch<Dmntable> bllSearch = new BllSearch<Dmntable>();
                    DmnSearch objSearch = new DmnSearch
                    {
                        Select = "Tid,TableCode,Price,Capcity,Description",
                        Table = "tblTable_info",
                        WhereNdGroupBy = "Where Tid = " + returnValue
                    };
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSource.Count == 0)
                    {
                        CMessageBox.Show("As of right now, seats are reserved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (gridSource.Count > 0)
                        {
                            int qty = 1;
                            bool flag = true;
                            for (int i = 0; i <= dgvforms.RowCount - 1; i++)
                            {
                                if (Convert.ToInt32(dgvforms.Rows[i].Cells[itid].Value) == gridSource[0].Tid)
                                {
                                    flag = false;
                                    break;
                                }
                            }
                            if (flag == true)
                            {
                                if (dgvforms.Rows.Count - 1 == dgvforms.CurrentCell.RowIndex)
                                {
                                    dgvforms.Rows.Add(1);
                                }
                                DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgvforms.Rows[r].Cells[iaction];
                                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                cellcolumntype.Value = Properties.Resources.food_cancel_icon;

                                dgvforms.Rows[r].Cells[itid].Value = gridSource[0].Tid;
                                dgvforms.Rows[r].Cells[iTBookingDetailId].Value = 0;
                                dgvforms.Rows[r].Cells[itablename].Value = gridSource[0].TableCode;
                                dgvforms.Rows[r].Cells[iprice].Value = gridSource[0].Price;
                                dgvforms.Rows[r].Cells[icapacity].Value = gridSource[0].Capcity;
                                dgvforms.Rows[r].Cells[idescription].Value = gridSource[0].Decription;
                                dgvforms.Rows[r].Cells[itablename].Style.ForeColor = Color.Black;
                            }
                        }
                    }
                    sumTable();
                    GrandTotal();
                    CalNetAmt();
                    if (dgvforms.Controls.OfType<VScrollBar>().First().Visible && tlp_reg_design.Visible != false)
                    {
                        tlp_reg_design.Visible = false;
                        btn_res_details.BackgroundImage = null;
                        btn_res_details.BackgroundImage = FoodandBeverage.Properties.Resources.res_show_button;
                    }
                }
                dgvforms.Focus();
                RecallTable();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page purchasepop()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }
        private void dgvforms_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //if (e.KeyData == Keys.Enter)
                //{
                //    if (dgvforms.CurrentCell.ColumnIndex == idescription && dgvforms.CurrentCell.RowIndex == dgvforms.Rows.Count - 1)
                //    {
                //        discountper.Focus();
                //    }
                //    else
                //    {
                //        e.SuppressKeyPress = true;
                //        SendKeys.Send("{TAB}");
                //    }
                //}
                //if (e.KeyData == Keys.F8 && dgvforms.CurrentCell.ColumnIndex == itablename)
                //{
                //    if (Convert.ToInt32(cboBtype.SelectedValue) == Convert.ToInt32(Reservation.TableBooking))
                //    {
                //        Tablepop(Convert.ToInt32(cboBtype.SelectedValue));
                //        RecallTable();
                //    }
                //    else
                //    {
                //        CMessageBox.Show("First Choose the Booking Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        if (tlp_reg_design.Visible == false)
                //        {
                //            tlp_reg_design.Visible = true;
                //        }
                //        cboBtype.Focus();
                //    }
                //}
                //else if (e.KeyData == Keys.Delete)
                //{
                //    dgvforms.Rows.RemoveAt(dgvforms.CurrentCell.RowIndex);
                //    if (dgvforms.Rows.Count == 0)
                //    {
                //        dgvforms.Rows.Add(1);
                //    }
                //}

                //if (dgvforms.CurrentCell.ColumnIndex >= 0 && dgvforms.CurrentCell.RowIndex >= 0)
                //{
                //    decimal totalSum = 0;
                //    foreach (DataGridViewRow row in dgvforms.Rows)
                //    {
                //        if (row.Cells[iprice].Value != null)
                //        {
                //            decimal cellValue;
                //            if (decimal.TryParse(row.Cells[iprice].Value.ToString(), out cellValue))
                //            {
                //                totalSum += cellValue;
                //            }
                //        }
                //    }
                //    lblSTotal.Text = totalSum.ToString();
                //}

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page dgvforms_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }
        private void cboBtype_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                int cunt = 0;
                string dtfrom = dtpBooking.Value.ToString("yyyy-MM-dd") + " " + dtp_from_time.Value.ToString("HH:mm:ss");
                dgvforms.Rows.Clear();
                dgvforms.Rows.Add(1);

                if (Convert.ToInt32(cboBtype.SelectedValue) == Convert.ToInt32(Reservation.Restaurant))
                {
                    dgvforms.Rows.Clear();
                    dgvforms.Rows.Add(1);

                    BllSearch<Dmntable> bllSearch = new BllSearch<Dmntable>();
                    DmnSearch objSearch = new DmnSearch
                    {
                        Select = "*,(Select Count(*) from tblTable_info where Status =1) tblCnt",
                        Table = "tblTable_info",
                        WhereNdGroupBy = $"WHERE Tid NOT IN (SELECT MTID FROM tblbookMaster m LEFT JOIN tblBookDetails d ON m.BookmasterId = d.BookmasterId AND d.status=1 " +
                                         $"WHERE BookingDateTime >= DATEADD(MINUTE, -{txt_HR.Text}, '{dtfrom}') " +
                                         $"AND BookingDateTime <= DATEADD(MINUTE, {txt_HR.Text}, '{dtfrom}') AND m.Status=1) " +
                                         $"AND Status=1 AND Tid NOT IN (SELECT Tid FROM tblOrderMaster WHERE CreatedDateTime >= DATEADD(MINUTE, -{txt_HR.Text}, '{dtfrom}') AND Status=1)"
                    };

                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);

                    if (gridSource.Count == 0)
                    {
                        CMessageBox.Show("As of right now, seats are reserved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        cunt = Convert.ToInt32(gridSource[0].tblCnt) - gridSource.Count;
                        if (cunt > 0)
                        {
                            CMessageBox.Show("Several of the chairs are currently in use.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        dgvforms.Rows.Add(gridSource.Count);
                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgvforms.Rows[i].Cells[iaction];
                            cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            cellcolumntype.Value = Properties.Resources.food_cancel_icon;

                            dgvforms.Rows[i].Cells[itid].Value = gridSource[i].Tid;
                            dgvforms.Rows[i].Cells[itablename].Value = gridSource[i].TableCode;
                            dgvforms.Rows[i].Cells[itablename].Style.ForeColor = Color.Black;
                            dgvforms.Rows[i].Cells[iprice].Value = gridSource[i].Price;
                            dgvforms.Rows[i].Cells[icapacity].Value = gridSource[i].Capcity;
                            dgvforms.Rows[i].Cells[idescription].Value = gridSource[i].Decription;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page cboBtype_SelectionChangeCommitted", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void dgvmenu_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    SendKeys.Send("{TAB}");
                }
                else if (e.KeyData == Keys.F8 && dgvmenu.CurrentCell.ColumnIndex == imname)
                {
                    menu(Convert.ToInt32(cboBtype.SelectedValue));

                }
                //else if (e.KeyData == Keys.Delete)
                //{
                //    dgvmenu.Rows.RemoveAt(dgvmenu.CurrentCell.RowIndex);
                //    if (dgvmenu.Rows.Count == 0)
                //    {
                //        dgvmenu.Rows.Add(1);
                //    }
                //}
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page  dgvmenu_KeyDown", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void dgvmenu_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == imname)
                {
                    if (Convert.ToInt32(dgvmenu.Rows[e.RowIndex].Cells[imid].Value) == 0 || string.IsNullOrEmpty(Convert.ToString(dgvmenu.Rows[e.RowIndex].Cells[imid].Value)))
                    {
                        dgvmenu.Rows[e.RowIndex].Cells[imname].Value = "F8 For Menu List";
                        dgvmenu.Rows[e.RowIndex].Cells[imname].Style.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page dgvmenu_CellEnter", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        public void CapacityCheck()
        {
            try
            {
                if (int.TryParse(txtNOP.Text, out int capacity))
                {
                    int totalCapacity = dgvforms.Rows
                                               .OfType<DataGridViewRow>()
                                               .Where(row => !row.IsNewRow && row.Cells[icapacity].Value != null)
                                               .Sum(row => int.TryParse(row.Cells[icapacity].Value.ToString(), out int personCapacity) ? personCapacity : 0);

                    if (capacity > totalCapacity)
                    {
                        CMessageBox.Show("Capacity is not sufficient for the people.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page CapacityCheck", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void dgvforms_Leave(object sender, EventArgs e)
        {
            GrandTotal();
            CalNetAmt();
        }

        private bool ContainsAlphabets(string text)
        {
            return text.Any(char.IsLetter);
        }

        private void dgvmenu_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == iqty && dgvmenu.Rows[e.RowIndex].Cells[imid].Value != null)
                {
                    string value = e.FormattedValue.ToString();

                    if (ContainsAlphabets(value))
                    {
                        e.Cancel = true;
                        CMessageBox.Show("Alphabetic characters are not allowed in this column.");
                        dgvmenu.Rows[e.RowIndex].Cells[iqty].Value = "1";
                    }

                    int qoh = Convert.ToInt32(dgvmenu.Rows[e.RowIndex].Cells[iQOH].Value);
                    int qty = Convert.ToInt32(e.FormattedValue);

                    if (qty > qoh && Convert.ToInt32(dgvmenu.Rows[e.RowIndex].Cells[iFormType].Value) == Convert.ToInt32(formsenum.Item))
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(dgvmenu.Rows[e.RowIndex].Cells[imid].Value)))
                        {
                            dgvmenu.Rows[e.RowIndex].Cells[iqty].Value = qoh;
                        }
                        else
                        {
                            CMessageBox.Show($"Available Stock Quantity is {qoh}", "Insufficient Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgvmenu.Rows[e.RowIndex].Cells[iqty].Value = qoh;
                            dgvmenu.CurrentCell = dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[iqty];
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        dgvmenu.Rows[dgvmenu.CurrentCell.RowIndex].Cells[isamount].Value = Convert.ToDecimal(dgvmenu.Rows[e.RowIndex].Cells[imprice].Value) * qty;
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page dgvmenu_CellValidating", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
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

            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        public void sdiscountPer()
        {
            try
            {
                if (string.IsNullOrEmpty(discountper.Text) || discountper.Text == ".")
                {
                    discountper.Text = "0";
                    totaldiscountamt.Text = "0";
                }

                if (!string.IsNullOrEmpty(discountper.Text) && Convert.ToDecimal(discountper.Text) != 0)
                {
                    decimal discountAmnt = Convert.ToDecimal(discountper.Text.Trim()) * Convert.ToDecimal(lblGTotal.Text) / 100;
                    totaldiscountamt.Text = discountAmnt.ToString("N2");
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page sdiscountPer", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        public void CalNetAmt()
        {
            try
            {
                Resrvation_period();

                if (string.IsNullOrEmpty(txtBookmasterId.Text))
                    txtBookmasterId.Text = "0";
                if (string.IsNullOrEmpty(totaldiscountamt.Text))
                    totaldiscountamt.Text = "0";
                if (string.IsNullOrEmpty(oaamount.Text))
                    oaamount.Text = "0";
                if (string.IsNullOrEmpty(txtadvpa.Text))
                    txtadvpa.Text = "0";
                if (string.IsNullOrEmpty(lblBAmt_Pay.Text))
                    lblBAmt_Pay.Text = "0";
                if (string.IsNullOrEmpty(lblGTotal.Text))
                    lblGTotal.Text = "0";
                if (string.IsNullOrEmpty(netamount.Text))
                    netamount.Text = "0";
                if (string.IsNullOrEmpty(lblNetAmt.Text))
                    lblNetAmt.Text = "0";
                if (string.IsNullOrEmpty(txtfinalamt.Text))
                    txtfinalamt.Text = "0";

                decimal Update_BalnceAmt = 0;
                decimal discount = Convert.ToDecimal(totaldiscountamt.Text);
                decimal otheraddamt = Convert.ToDecimal(oaamount.Text);
                decimal otherdedamt = Convert.ToDecimal(odamt.Text);
                decimal adpayment = Convert.ToDecimal(txtadvpa.Text);
                decimal balanceneed = Convert.ToDecimal(lblBAmt_Pay.Text);
                decimal cashreciv = Convert.ToDecimal(rcvamt.Text);
                decimal netamt = (Convert.ToDecimal(lblGTotal.Text) - discount - otherdedamt) + otheraddamt;

                netamount.Text = netamt.ToString("0.00");
                lblNetAmt.Text = netamt.ToString("0.00");

                decimal finalAmount = Convert.ToDecimal(txtfinalamt.Text);
                if (finalAmount >= netamt)
                {
                    Update_BalnceAmt = finalAmount - netamt;
                }
                else
                {
                    Update_BalnceAmt = netamt - finalAmount;
                }
                lblBAmt_Pay.Text = Update_BalnceAmt.ToString("0.00");
                if(netamt>0)
                {
                    if (Convert.ToDecimal(txtfinalamt.Text) > netamt)
                    {
                        label21.Text = "Cash Given";
                        lblpayment_status.Visible = false;
                        rcvamt.Enabled = false;
                    }
                    else if (Convert.ToDecimal(txtfinalamt.Text) == netamt)
                    {
                        lblpayment_status.Visible = true;
                        label21.Text = "Balance To Pay";
                        lblpayment_status.Visible = true;
                    }
                    else
                    {
                        label21.Text = "Balance To Pay";
                        rcvamt.Enabled = true;
                        lblpayment_status.Visible = false;
                    }
                }
                else
                {
                    label21.Text = "Balance To Pay";
                    rcvamt.Enabled = true;
                    lblpayment_status.Visible = false;
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page CalNetAmt", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void discountper_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(discountper.Text))
                {
                    discountper.Text = "0";
                    totaldiscountamt.Text = "0";
                }
                else if (Convert.ToDecimal(discountper.Text) == 0)
                {
                    discountper.Text = "0";
                    totaldiscountamt.Text = "0";
                    sdiscountPer();
                    CalNetAmt();
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
                    CMessageBox.Show("Discount Percentage Cannot be More Than 100", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    discountper.Text = "0.00";
                    totaldiscountamt.Text = "0.00";
                    discountper.Focus();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page discountper_Leave", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
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
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page sdiscountAmnt", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
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
                    CMessageBox.Show("Discount Amount Cant Be More Than Total Amount", "Mandatory", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    totaldiscountamt.Text = "0.00";
                    discountper.Text = "0.00";
                    totaldiscountamt.Focus();
                }
            }

            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page totaldiscountamt_Leave", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }
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

            try
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


                //if (Convert.ToInt32(txtBookmasterId.Text) == 0)
                //{
                //    decimal adpayment = Convert.ToDecimal(txtadvpa.Text);
                //    decimal balAmnt = adpayment - Convert.ToDecimal(rcvamt.Text);
                //    lblBalAmt.Text = balAmnt.ToString("N2");
                //}
                //else
                //{
                //    decimal adpayment = Convert.ToDecimal(txtadvpa.Text);
                //    decimal bal = Convert.ToDecimal(lblBalAmt.Text);
                //    decimal net = Convert.ToDecimal(netamount.Text);
                //    decimal balanceneed = Convert.ToDecimal(lblBAmt.Text);
                //    decimal cashreciv = Convert.ToDecimal(rcvamt.Text);
                //    bal = balanceneed - cashreciv;
                //    lblBalAmt.Text = bal.ToString();

                //}
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page rcvamt_Leave", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page SalesGridCalc", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
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
                if (string.IsNullOrWhiteSpace(txt_pre_balamtpay.Text))
                {
                    txt_pre_balamtpay.Text = "0.00";
                }
                if (string.IsNullOrWhiteSpace(txtfinalamt.Text))
                {
                    txtfinalamt.Text = "0.00";
                }
                if (rcvamt.Text != "." && rcvamt.Text != "")
                {
                    decimal advancePayment = Convert.ToDecimal(txtadvpa.Text);
                    decimal receivedAmount = Convert.ToDecimal(rcvamt.Text);
                    decimal balancePayment = Convert.ToDecimal(lblBAmt_Pay.Text);
                    decimal balanceAmount = 0;
                    decimal NetAmt = Convert.ToDecimal(lblNetAmt.Text);
                    decimal Finalamt = Convert.ToDecimal(txtfinalamt.Text);

                    #region Payment Complete or advanaced payment
                    if (Convert.ToInt32(txtBookmasterId.Text) > 0 && lblBAmt_Pay.Visible)
                    {
                        //if (receivedAmount == 0)
                        //    balanceAmount = 0;
                        //else
                        //{
                        //    #region Payment completed
                        //    if (PaymentCFlage)
                        //    {
                        //        balanceAmount = Convert.ToDecimal(lblBAmt_Pay.Text) - receivedAmount;
                        //        lblBalAmt.Text = balanceAmount.ToString("0.00");
                        //    }
                        //    #endregion
                        //    else
                        //    {
                        //        balanceAmount = Convert.ToDecimal(txt_pre_balamtpay.Text) - receivedAmount;
                        //        if (receivedAmount >= Convert.ToDecimal(txt_pre_balamtpay.Text))
                        //            lblBAmt_Pay.Text = "0.00";
                        //        else
                        //            lblBAmt_Pay.Text = balanceAmount.ToString("0.00");
                        //    }
                        //}

                        balanceAmount = NetAmt - Finalamt - receivedAmount;
                        lblBalAmt.Text = balanceAmount.ToString("0.00");

                        balanceAmount = NetAmt - Finalamt;
                        lblBAmt_Pay.Text = balanceAmount.ToString("0.00");

                    }
                    #endregion

                    #region Payment not complete Not save 
                    else
                    {
                        if (receivedAmount == 0)
                            balanceAmount = 0;
                        else
                            balanceAmount = receivedAmount - advancePayment;// balanceAmount = NetAmt - receivedAmount - advancePayment;

                        lblBalAmt.Text = balanceAmount.ToString("0.00");
                    }
                    #endregion

                }
                else
                {
                    lblBalAmt.Text = "0.00";
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page rcvamt_TextChanged", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void txtadvpa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                rcvamt.Focus();
            }
        }
        public void GrandTotal()
        {
            try
            {
                if (String.IsNullOrEmpty(lblMTotal.Text))
                    lblMTotal.Text = "0";
                decimal totalSum = 0;
                for (int i = 0; i < dgvmenu.Rows.Count; i++)
                {
                    if ((Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddonstatus].Value) == 0 || Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddonstatus].Value) == 1) && Convert.ToString(dgvmenu.Rows[i].Cells[iAddonstatus].Value) != null)
                        totalSum = totalSum + Convert.ToDecimal(dgvmenu.Rows[i].Cells[isamount].Value);
                }

                lblMTotal.Text = Convert.ToString(totalSum);
                decimal value1 = totalSum;
                decimal value2 = Convert.ToDecimal(lblSTotal.Text);
                decimal sum = value1 + value2;
                lblGTotal.Text = sum.ToString();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page GrandTotal", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }

        }
        private void dgvmenu_Leave(object sender, EventArgs e)
        {
            GrandTotal();
            CalNetAmt();
        }
        public void SumMenu()
        {
            try
            {
                decimal totalSum = 0;
                int i = 0;
                for (i = 0; i <= dgvmenu.RowCount - 1; i++)
                {
                    if (dgvmenu.Rows[i].Cells[imprice].Value != null &&
                        (Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddonstatus].Value) == Convert.ToInt32(AddonType.AddonType) ||
                        Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddonstatus].Value) == Convert.ToInt32(AddonType.None)))
                    {
                        decimal cellValue;
                        if (decimal.TryParse(dgvmenu.Rows[i].Cells[isamount].Value.ToString(), out cellValue))
                        {
                            totalSum += cellValue;
                        }
                    }
                }
                lblMTotal.Text = totalSum.ToString();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page summenu()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }
        }



        public void sumTable()
        {
            try
            {
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
                lblSTotal.Text = totalSum.ToString();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page sumTable()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }
        }
        private void dgvforms_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
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
                    lblSTotal.Text = totalSum.ToString();
                }
                GrandTotal();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page dgvforms_CellEndEdit", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }

        }

        private void dgvmenu_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.ColumnIndex == iqty && e.RowIndex >= 0)
                {
                   
                    int r= dgvmenu.CurrentCell.RowIndex;
                    dgvmenu.Rows[r].Cells[isamount].Value = Convert.ToDecimal(dgvmenu.Rows[e.RowIndex].Cells[imprice].Value) * Convert.ToDecimal(dgvmenu.Rows[e.RowIndex].Cells[iqty].Value);
                    totalcellamount(r);
                    decimal totalSum = 0;
                    for (int i = 0; i < dgvmenu.Rows.Count; i++)
                    {
                        if ((Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddonstatus].Value) == 0 || Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddonstatus].Value) == 1) && Convert.ToString(dgvmenu.Rows[i].Cells[iAddonstatus].Value) != null)
                            totalSum = totalSum + Convert.ToDecimal(dgvmenu.Rows[i].Cells[isamount].Value);
                    }
                    lblMTotal.Text = totalSum.ToString();

                    SumMenu();
                    GrandTotal();
                    CalNetAmt();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page dgvmenu_CellEndEdit", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
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
            try
            {
                decimal value;
                if (!decimal.TryParse(txtadvpa.Text, out value))
                {
                    txtadvpa.Text = "0.00";
                }
                decimal adpayment = Convert.ToDecimal(txtadvpa.Text);
                decimal netamt = Convert.ToDecimal(lblNetAmt.Text);
                if (adpayment > netamt)
                {
                    CMessageBox.Show("Advance payment is greater than the net amount.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtadvpa.Focus();
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page txtadvpa_Leave", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

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


        private void mbtnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
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
                    dmnReservation.BookingDateTime = Convert.ToDateTime(dtfrom);

                    if (oddesc.Text == PlaceholderText)
                        oddesc.Text = string.Empty;
                    if (oadesc.Text == PlaceholderText)
                        oadesc.Text = string.Empty;

                        #region booking Master Inseration  
                    dmnReservation.BookmasterId = Convert.ToInt32(txtBookmasterId.Text);
                    dmnReservation.cusName = txtNam.Text;
                    dmnReservation.Mobile = txtMob.Text;
                    dmnReservation.Address = txtAddress.Text;
                    dmnReservation.EmailId = txtEmail.Text;
                    dmnReservation.Remarks = txtRemarks.Text;
                    dmnReservation.gender = Convert.ToString(cboGender.SelectedValue);
                    dmnReservation.CreatedBy = Convert.ToInt32(FrmLogin.UserId);
                    dmnReservation.Status = 1;
                    dmnReservation.BookingType = Convert.ToString(cboBtype.SelectedValue);
                    dmnReservation.NoOfPeople = txtNOP.Text;
                    dmnReservation.BranchId = Convert.ToInt32(FrmLogin.BranchId);

                    dmnReservation.ReservationTotal = Convert.ToDecimal(lblSTotal.Text);
                    if (String.IsNullOrEmpty(lblMTotal.Text))
                        lblMTotal.Text = "0";
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
                    dmnReservation.BalnceNeed = Convert.ToDecimal(lblBAmt_Pay.Text);

                    decimal tota_amt = 0, adv_amt = 0, rcv_amt = 0, Net_amt = 0, Balpay_amt=0,Final_Amt=0;
                    adv_amt = Convert.ToDecimal(txtadvpa.Text);
                    rcv_amt = Convert.ToDecimal(rcvamt.Text);
                    Net_amt = Convert.ToDecimal(lblNetAmt.Text);
                    Balpay_amt = Convert.ToDecimal(lblBAmt_Pay.Text);
                    Balpay_amt = Convert.ToDecimal(lblBAmt_Pay.Text);
                    Final_Amt = Convert.ToDecimal(txtfinalamt.Text);

                    if (Convert.ToInt32(txtBookmasterId.Text) > 0)
                    {
                        #region Regsiatartion complete But Payment complete or Not

                        tota_amt = Final_Amt + rcv_amt;
                        if(tota_amt>Net_amt)
                        {
                            dmnReservation.FinalAmount = Net_amt;
                            txtadvpa.Text = Convert.ToString(Net_amt);

                        }
                        else if (tota_amt == Net_amt)
                        {
                            dmnReservation.FinalAmount = tota_amt;
                        }
                        else
                        {
                            dmnReservation.FinalAmount = tota_amt;
                            txtadvpa.Text = Convert.ToString(tota_amt);
                        }              

                        if (label21.Text == "Cash Given")
                        {
                            dmnReservation.CashGivenflag = 1;
                            dmnReservation.CashGivenAmt = Convert.ToDecimal(lblBAmt_Pay.Text);
                        }                    
                        else
                        {
                            dmnReservation.CashGivenflag = 0;
                            dmnReservation.CashGivenAmt = Convert.ToDecimal(txt_CashGivenAmt.Text);
                        }

                        #endregion
                    }
                    else
                    {
                        #region Regsiatartion not savein db
                        tota_amt = Net_amt - (adv_amt + rcv_amt);
                        if(adv_amt ==0 && rcv_amt==0)// without recive amount and advance amount 
                        {
                            dmnReservation.FinalAmount = 0;
                        }
                        else if ( adv_amt < Net_amt) //advance amount  less than Net amount
                        {
                            dmnReservation.FinalAmount = Convert.ToDecimal(txtadvpa.Text);
                        }
                        else if (adv_amt == Net_amt)//full payment
                        {
                            dmnReservation.FinalAmount = Convert.ToDecimal(lblNetAmt.Text);
                        }
                        #endregion
                    }                   

                    dmnReservation.AdvancePay = Convert.ToDecimal(txtadvpa.Text);
                    dmnReservation.CashRev = Convert.ToDecimal(rcvamt.Text);
                    dmnReservation.BalnceAmt = Convert.ToDecimal(lblBalAmt.Text);
                    dmnReservation.PaymentTimes = Convert.ToInt32(txt_PaymentTimes.Text);
                    dmnReservation.AdvanceAmts_List = Convert.ToString(txt_AdvanceAmts_List.Text);

                    if (chb_active.Checked)
                        dmnReservation.Status = 1;
                    else
                    {
                        dmnReservation.Status = 0;
                    }
                    var InsertStatusP = bllReservation.AddData("prdInsert_Upd_bookMaster", dmnReservation);

                    #endregion

                        #region Booking Details Inseration Table
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
                                dmnReservation1.CreatedBy = Convert.ToInt32(FrmLogin.UserId);
                                dmnReservation1.BranchId = Convert.ToInt32(FrmLogin.BranchId);
                                dmnReservation1.Rid = Convert.ToInt32(TableMenu.Rid);
                                dmnReservation1.Status = 1;
                                datadetails = bllReservation.AddDetails("prdInsert_Upd_bookDetails", dmnReservation1);
                            }

                        }
                        #endregion

                        #region Booking Details Inseration Menu
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
                                dmnReservation1.TaxAmt = Convert.ToDecimal(dgvmenu.Rows[i].Cells[istaxamt].Value);
                                dmnReservation1.IgstPrcnt = Convert.ToDecimal(dgvmenu.Rows[i].Cells[imgst].Value);
                                dmnReservation1.CgstPrcnt = Convert.ToDecimal(dgvmenu.Rows[i].Cells[icgst].Value);
                                dmnReservation1.SgstPrcnt = Convert.ToDecimal(dgvmenu.Rows[i].Cells[isgst].Value);
                                dmnReservation1.VatPrcnt = Convert.ToDecimal(dgvmenu.Rows[i].Cells[ivat].Value);
                                dmnReservation1.Description = Convert.ToString(dgvmenu.Rows[i].Cells[imdescription].Value);
                                dmnReservation1.CreatedBy = Convert.ToInt32(FrmLogin.UserId);
                                dmnReservation1.BranchId = Convert.ToInt32(FrmLogin.BranchId);
                                dmnReservation1.Rid = Convert.ToInt32(TableMenu.Mid);
                                dmnReservation1.Status = 1;
                                dmnReservation1.Net = Convert.ToDecimal(dgvmenu.Rows[i].Cells[isamount].Value);


                                dmnReservation1.TableType = Convert.ToInt32(dgvmenu.Rows[i].Cells[itabletype].Value);
                                dmnReservation1.MarketPrice = Convert.ToDecimal(dgvmenu.Rows[i].Cells[imarketprice].Value);
                                dmnReservation1.MainRemarks = "";
                                dmnReservation1.Pavid = Convert.ToInt32(dgvmenu.Rows[i].Cells[iPavid].Value);
                                dmnReservation1.AddonStatus = Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddonstatus].Value);
                                dmnReservation1.AV_id = Convert.ToString(dgvmenu.Rows[i].Cells[iAvid].Value);
                                dmnReservation1.Addoncount = Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddoncount].Value);
                                dmnReservation1.vid = Convert.ToInt32(dgvmenu.Rows[i].Cells[ivid].Value);
                                dmnReservation1.CCode = Convert.ToString(dgvmenu.Rows[i].Cells[iaddoncode].Value);
                                dmnReservation1.VariatiionName = Convert.ToString(dgvmenu.Rows[i].Cells[iVarationName].Value);
                                dmnReservation1.ItemBarCode = Convert.ToString(dgvmenu.Rows[i].Cells[iitemcode].Value);
                                dmnReservation1.unit = Convert.ToInt32(dgvmenu.Rows[i].Cells[iunit].Value);
                                dmnReservation1.Fcid = Convert.ToInt32(dgvmenu.Rows[i].Cells[iproductType].Value);
                                dmnReservation1.PrId = Convert.ToInt32(dgvmenu.Rows[i].Cells[iprid].Value);
                                dmnReservation1.TaxSettingsId = Convert.ToInt32(dgvmenu.Rows[i].Cells[itaxsettingid].Value);

                                datadetails = bllReservation.AddDetails("prdInsert_Upd_bookDetails", dmnReservation1);
                            }
                        }
                        #endregion

                        #region Order Master Inseration
                        int sum = 0;
                        int n = dgvmenu.RowCount;
                        for (int i = 0; i < n; i++)
                        {
                            if ((Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value) != null && Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value) > 0) && (Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddonstatus].Value) == 1 || Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddonstatus].Value) == 0))
                                sum = sum + Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value);
                        }
                        dmnordermaster.OrderMasterId = 0;
                        dmnordermaster.EntryNo = "";
                        dmnordermaster.BookmasterId = InsertStatusP.BookmasterId;
                        dmnordermaster.TotalQuantity = Convert.ToInt32(sum);
                        dmnordermaster.Remarks = txtRemarks.Text;
                        dmnordermaster.Address = txtAddress.Text;
                        dmnordermaster.Telephone = txtMob.Text;
                        dmnordermaster.CustomerName = txtNam.Text;
                        dmnordermaster.Gender = Convert.ToInt32(cboGender.SelectedValue);
                        dmnordermaster.Email = txtEmail.Text;
                        dmnordermaster.BranchId = FrmDashboard.BranchId;
                        dmnordermaster.CreatedBy = Convert.ToInt32(FrmLogin.UserId);
                        dmnordermaster.Status = 1;
                        dmnordermaster.Tid = 0;
                        dmnordermaster.NetAmount = 0;
                        dmnordermaster.PickupOrderFlag = 0;
                        dmnordermaster.PkId = 0;
                        var InsertStatusO = bllOrder.AddData("prdInsert_Upd_OrderMaster", dmnordermaster);
                        #endregion

                        #region Order Details Inseration
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
                                    dmnOrderDetails.CreatedBy = Convert.ToInt32(FrmLogin.UserId);
                                    dmnOrderDetails.BranchId = Convert.ToInt32(FrmLogin.BranchId);
                                    dmnOrderDetails.Status = 4;
                                    dmnOrderDetails.UnitId = Convert.ToInt32(dgvmenu.Rows[i].Cells[iunit].Value);
                                    dmnOrderDetails.Amount = Convert.ToDecimal(dgvmenu.Rows[i].Cells[isamount].Value);
                                    dmnOrderDetails.ItemBarCode = Convert.ToString(dgvmenu.Rows[i].Cells[iitemcode].Value);
                                    dmnOrderDetails.TableType = Convert.ToInt32(dgvmenu.Rows[i].Cells[itabletype].Value);
                                    dmnOrderDetails.MarketPrice = Convert.ToDecimal(dgvmenu.Rows[i].Cells[imarketprice].Value);
                                    dmnOrderDetails.Fcid = Convert.ToInt32(dgvmenu.Rows[i].Cells[iproductType].Value);
                                    dmnOrderDetails.DiscountAmount = Convert.ToDecimal(dgvmenu.Rows[i].Cells[idisamt].Value);
                                    dmnOrderDetails.DiscountPer = Convert.ToDecimal(dgvmenu.Rows[i].Cells[idisper].Value);
                                    dmnOrderDetails.PurchaseRate = Convert.ToInt32(dgvmenu.Rows[i].Cells[iprate].Value);
                                    dmnOrderDetails.NetAmount = Convert.ToDecimal(dgvmenu.Rows[i].Cells[isamount].Value);

                                    dmnOrderDetails.DiscountAmount = Convert.ToDecimal(dgvmenu.Rows[i].Cells[idisamt].Value);
                                    dmnOrderDetails.DiscountPer = Convert.ToDecimal(dgvmenu.Rows[i].Cells[idisper].Value);
                                    dmnOrderDetails.PurchaseRate = Convert.ToInt32(dgvmenu.Rows[i].Cells[iprate].Value);
                                    dmnOrderDetails.NetAmount = Convert.ToDecimal(dgvmenu.Rows[i].Cells[isamount].Value);

                                    dmnOrderDetails.Tid = 0;
                                    dmnOrderDetails.Pavid = Convert.ToInt32(dgvmenu.Rows[i].Cells[iPavid].Value);
                                    dmnOrderDetails.AddonStatus = Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddonstatus].Value);
                                    dmnOrderDetails.AV_id = Convert.ToString(dgvmenu.Rows[i].Cells[iAvid].Value);
                                    dmnOrderDetails.Addoncount = Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddoncount].Value);
                                    dmnOrderDetails.vid = Convert.ToInt32(dgvmenu.Rows[i].Cells[ivid].Value);
                                    dmnOrderDetails.CCode = Convert.ToString(dgvmenu.Rows[i].Cells[iaddoncode].Value);
                                    dmnOrderDetails.VariatiionName = Convert.ToString(dgvmenu.Rows[i].Cells[iVarationName].Value);

                                    datadetails = bllOrder.AddDetail("prdInsert_Upd_OrderDetailsUpdate", dmnOrderDetails);
                                }
                            }
                        }
                        #endregion

                        if (chb_active.Checked == false)
                        {
                            CMessageBox.Show("Reservation cancelled successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            btn_rev_cancel.Visible = false;
                        }
                        else
                        {
                            if (Convert.ToInt32(txtBookmasterId.Text) > 0)
                            {
                                CMessageBox.Show("Update Successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                CMessageBox.Show("Saved Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            if (!datadetails)
                            {
                                CMessageBox.Show("Unable to Save", "Error on Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    clear();
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page save()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }

        }

        private void mbtnSave_Click_1(object sender, EventArgs e)
        {
            save();
        }

        private void lblSTotal_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal value;
                if (!decimal.TryParse(lblSTotal.Text, out value))
                {
                    lblSTotal.Text = "0";

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page lblSTotal_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void lblMTotal_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal value;
                if (!decimal.TryParse(lblMTotal.Text, out value))
                {
                    lblMTotal.Text = "0";

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page lblMTotal_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }

        }

        private void lblGTotal_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal value;
                if (!decimal.TryParse(lblMTotal.Text, out value))
                {
                    lblMTotal.Text = "0";

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page lblGTotal_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }

        }


        private void dgvItemPop_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                tlp_main.Enabled = true;
                int r = dgvItemPop.CurrentCell.RowIndex;
                int r1 = dgvmenu.CurrentCell.RowIndex;
                Current_menuGrid_rowno = dgvmenu.CurrentCell.RowIndex; ;
                bool Variation_Flag = false;
                BllSearch<DmnMenu> bllSearch = new BllSearch<DmnMenu>();
                BllSearch<DmnOfferMaster> bllSearch1 = new BllSearch<DmnOfferMaster>();
                DmnSearch objSearch = new DmnSearch();

                panelProductPopup.Visible = false;

                #region Fetch DB
                if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[mformsenumno].Value) == Convert.ToInt32(formsenum.Menu))
                {
                    #region Variation And Addon Checking
                    DmnSearch dmnSearch = new DmnSearch();
                    BllSearch<DmnGridFields> Search_variation = new BllSearch<DmnGridFields>();
                    dmnSearch.Select = "count(*)  Field1";
                    dmnSearch.Table = "tblProduct_Addon_Variation ";
                    dmnSearch.WhereNdGroupBy = " where Status=1 and  ProductId= " + dgvItemPop.Rows[r].Cells[midno].Value;
                    dmnSearch.OrderBy = "";
                    var gridSource_va = Search_variation.GetSearchData("prdSearch", dmnSearch);
                    if (gridSource_va.Count > 0)
                    {
                        if (Convert.ToInt32(gridSource_va[0].Field1) > 0)
                        {
                            Variation_Flag = true;
                        }
                    }
                    #endregion

                    if (!Variation_Flag)
                    {
                        objSearch.Select = "MId ,Name as Title,p.Price,p.Tax,p.TaxType ,IgstPrcnt,VatPrcnt ,ts.TaxSettingsId," +
                                      "ItemCode as ItemCode_UPC,CgstPrcnt,SgstPrcnt,p.Fcid," +
                                      "p.DiscountAmt as DisAmt,p.DiscountPer as DisPer ";
                        objSearch.Table = " tblMenu m left join tblProduct p on m.ProductId=p.ProductId " +
                            " left join tblTaxSettings ts on p.TaxSettingsId=ts.TaxSettingsId ";
                        objSearch.WhereNdGroupBy = " where MId =" + dgvItemPop.Rows[r].Cells[midno].Value;
                        objSearch.OrderBy = "";
                    }

                }
                else if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[mformsenumno].Value) == Convert.ToInt32(formsenum.Item))
                {
                    objSearch.Select = " ItemId,Itemname ,p.SRate,p.PRate,p.MRP,i.Tax,i.TaxType,IgstPrcnt,VatPrcnt,CgstPrcnt,SgstPrcnt," +
                        "unit,ItemCode_UPC,p.Id as PrId,Fcid,ts.TaxSettingsId,quantity as QOH,i.DiscountAmt,i.DiscountPer ";
                    objSearch.Table = "  tblItem i  left join tblTaxSettings ts on i.TaxSettingsId=ts.TaxSettingsId " +
                        " left join tblProductRate p   on ProductId=i.ItemId  and p.QOH>0";
                    objSearch.WhereNdGroupBy = " where  Saletype=1 and  ItemId =" + dgvItemPop.Rows[r].Cells[midno].Value;
                    objSearch.OrderBy = "";
                }
                else if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[mformsenumno].Value) == Convert.ToInt32(formsenum.Discount_And_Offer))
                {
                    objSearch.Select = "ofm.OfferName ,ofm.FinalPrice,ofm.Omid , ofm.TaxType , ofm.Tax , ofm.TaxSettingsId , ofm.Discountper , ofd.Productname, " +
                        " CgstPrcnt,SgstPrcnt,VatPrcnt,IgstPrcnt ,ofm.DiscountAmt,ofm.Discountper,ofm.TaxSettingsId,ofm.TaxAmount,ofm.TaxPer";

                    objSearch.Table = "  tblOfferMaster ofm LEFT JOIN tblOfferDetails ofd on ofm.Omid=ofd.Omid  left join tblTaxSettings ts  on ts.TaxSettingsId=ofm.TaxSettingsId ";
                    objSearch.WhereNdGroupBy = " where ofm.Omid =" + dgvItemPop.Rows[r].Cells[midno].Value;
                    objSearch.OrderBy = "";
                }
                #endregion

                #region Calculation

                #region Product With offer, addon

                if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[mformsenumno].Value) == Convert.ToInt32(formsenum.Menu))
                {
                    if (Variation_Flag)
                    {
                        Global_ProductId = Convert.ToInt32(dgvItemPop.Rows[r].Cells[midno].Value);
                        ShowVariationAddon(Convert.ToInt32(dgvItemPop.Rows[r].Cells[midno].Value));
                    }
                    else
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

                                DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgvmenu.Rows[r1].Cells[iaction];
                                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                cellcolumntype.Value = Properties.Resources.food_cancel_icon;
                                dgvmenu.Rows[r1].Cells[islno].Value = r1 + 1;
                                dgvmenu.Rows[r1].Cells[imid].Value = gridSource[0].MId;
                                dgvmenu.Rows[r1].Cells[imBookingDetailId].Value = 0;
                                dgvmenu.Rows[r1].Cells[imname].Value = gridSource[0].Title;
                                dgvmenu.Rows[r1].Cells[imname].Style.ForeColor = Color.Black;
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
                                dgvmenu.Rows[r1].Cells[iAddonstatus].Value = Convert.ToInt32(AddonType.None);
                                //dgvmenu.Rows[r1].Cells[idisamt].Value = gridSource[0].DisAmt;

                                dgvmenu.Rows[r1].Cells[iunit].Value = gridSource[0].unit;
                                dgvmenu.Rows[r1].Cells[iitemcode].Value = gridSource[0].ItemCode_UPC;
                                dgvmenu.Rows[r1].Cells[iprid].Value = gridSource[0].PrId;
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
                            SumMenu();
                            panelProductPopup.Visible = false;
                            dgvmenu.Enabled = true;
                            dgvmenu.Focus();

                        }
                    }

                }
                #endregion

                #region Multiple price
                else if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[mformsenumno].Value) == Convert.ToInt32(formsenum.Item))
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

                                DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgvmenu.Rows[r1].Cells[iaction];
                                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                cellcolumntype.Value = Properties.Resources.food_cancel_icon;

                                dgvmenu.Rows[r1].Cells[islno].Value = r1 + 1;
                                dgvmenu.Rows[r1].Cells[imid].Value = gridSource[0].ItemId;
                                dgvmenu.Rows[r1].Cells[imBookingDetailId].Value = 0;
                                dgvmenu.Rows[r1].Cells[imname].Value = gridSource[0].Itemname;
                                dgvmenu.Rows[r1].Cells[imname].Style.ForeColor = Color.Black;
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
                                dgvmenu.Rows[r1].Cells[iprid].Value = gridSource[0].PrId;
                                dgvmenu.Rows[r1].Cells[itaxsettingid].Value = gridSource[0].TaxSettingsId;
                                dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.DirectSale);
                                dgvmenu.Rows[r1].Cells[iQOH].Value = gridSource[0].QOH;
                                dgvmenu.Rows[r1].Cells[iproductType].Value = gridSource[0].Fcid;
                                dgvmenu.Rows[r1].Cells[iAddonstatus].Value = Convert.ToInt32(AddonType.None);
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
                        else
                        {
                            CMessageBox.Show("Avilable Stock Quanity Is " + gridSource[0].QOH, "Insufficent Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //dgvSProduct.Rows[i].Cells[isqty].Value = gridSource[0].QOH;
                        }
                        panelProductPopup.Visible = false;
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
                        pnl_menu.Location = new Point(
                          this.ClientSize.Width / 5 - pnl_menu.Size.Width / 5,
                          (this.ClientSize.Height / 5 - pnl_menu.Size.Height / 5));
                        pnl_menu.Anchor = AnchorStyles.None;
                        dgvmenu.Enabled = false;
                        dgv_spopup.Enabled = true;
                        pnl_menu.Visible = true;
                        dgv_spopup.Visible = true;
                        dgv_spopup.Focus();
                        panelProductPopup.Visible = false;
                    }
                    if (dgvmenu.CurrentCell.ColumnIndex == imname && dgvmenu.CurrentCell.RowIndex >= 0)
                    {
                        SumMenu();
                    }
                }
                #endregion

                #region Combo Products
                else if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[mformsenumno].Value) == Convert.ToInt32(formsenum.Discount_And_Offer))
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

                            DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgvmenu.Rows[r1].Cells[iaction];
                            cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            cellcolumntype.Value = Properties.Resources.food_cancel_icon;

                            dgvmenu.Rows[r1].Cells[islno].Value = r1 + 1;
                            dgvmenu.Rows[r1].Cells[imid].Value = gridSource1[0].Omid;
                            dgvmenu.Rows[r1].Cells[imBookingDetailId].Value = 0;
                            dgvmenu.Rows[r1].Cells[imname].Value = gridSource1[0].OfferName;
                            dgvmenu.Rows[r1].Cells[imname].Style.ForeColor = Color.Black;
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
                            dgvmenu.Rows[r1].Cells[iTaxper].Value = gridSource1[0].TaxPer;
                            dgvmenu.Rows[r1].Cells[istaxamt].Value = gridSource1[0].TaxAmount;
                            dgvmenu.Rows[r1].Cells[ibackuptaxamt].Value = gridSource1[0].TaxAmount; 
                            dgvmenu.Rows[r1].Cells[iprate].Value = 0;
                            dgvmenu.Rows[r1].Cells[imrp].Value = 0;
                            dgvmenu.Rows[r1].Cells[itaxsettingid].Value = gridSource1[0].TaxSettingsId;
                            dgvmenu.Rows[r1].Cells[imarketprice].Value = gridSource1[0].FinalPrice;
                            dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.ComboProduct);
                            dgvmenu.Rows[r1].Cells[iAddonstatus].Value = Convert.ToInt32(AddonType.None);
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
                        SumMenu();
                        panelProductPopup.Visible = false;
                        dgvmenu.Enabled = true;
                        dgvmenu.Focus();
                    }

                }
                #endregion

                #endregion

                SumMenu();
                GrandTotal();
                CalNetAmt();
                dgvmenu.Focus();
                Recallmenu();
                GridSlno_Refresh();

                if (dgvmenu.Controls.OfType<VScrollBar>().First().Visible && tlp_reg_design.Visible != false)
                {
                    tlp_reg_design.Visible = false;
                    btn_res_details.BackgroundImage = null;
                    btn_res_details.BackgroundImage = FoodandBeverage.Properties.Resources.res_show_button;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page dgvItemPop_CellClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }

        }

        public void ShowVariationAddon(int pid)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();

                pnl_variation_addon.Location = new Point(this.Width / 2 - pnl_variation_addon.Size.Width / 2, this.Height / 2 - pnl_variation_addon.Size.Height / 2);

                dmnSearch.Select = "pavid Field1,Caption Field2,pav.Price Field3,pav.Aid Field4,a.Price Field5,pav.Type Field6,pav.ProductId Field7,pav.CCode Field8";
                dmnSearch.Table = "tblProduct_Addon_Variation pav left join tblAddon a on a.Aid=pav.Aid and a.Status=1 ";
                dmnSearch.WhereNdGroupBy = " where pav.Status=1 and ProductId= " + pid; //type= "+Convert.ToInt32(Product_Addon_Variation_Type.Variation) +" and
                dmnSearch.OrderBy = "order by pav.pavid desc";
                var gridSource_va = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource_va.Count > 0)
                {
                    tlp_main.Enabled = false;
                    previousClickedButton = null;
                    addonflag = true; variflag = true;
                    lblsizetotal.Text = "0";
                    lbladdontotal.Text = "0";

                    flp_addons.SuspendLayout();
                    flp_addons.Controls.Clear();
                    flp_variation.Controls.Clear();

                    #region Variation 
                    foreach (var item in gridSource_va)
                    {
                        if (Convert.ToInt32(item.Field6) == 1)
                        {
                            TableLayoutPanel tbl_button_vari = new TableLayoutPanel();
                            tbl_button_vari.Name = "tbl_button_vari" + item.Field1;
                            tbl_button_vari.Tag = item.Field1;
                            tbl_button_vari.Dock = DockStyle.None;
                            tbl_button_vari.BorderStyle = BorderStyle.FixedSingle;
                            //tbl_button_vari.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                            tbl_button_vari.RowStyles.Add(new RowStyle(SizeType.AutoSize, 1));
                            tbl_button_vari.RowStyles.Add(new RowStyle(SizeType.AutoSize, 1));
                            tbl_button_vari.AutoSize = true;
                            tbl_button_vari.Height = 40;
                            tbl_button_vari.Margin = new Padding(15, 5, 0, 20);

                            Button button = new Button();
                            button.Text = item.Field2;
                            button.ForeColor = Color.Black;
                            button.AutoSize = true;
                            button.Dock = DockStyle.Fill;
                            button.BackColor = ColorTranslator.FromHtml("#3f88c5");
                            button.ForeColor = Color.Black;
                            button.Name = "btnVariation_" + item.Field1;
                            button.Tag = item.Field1;
                            button.AccessibleDescription = item.Field3;
                            button.Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Regular);
                            //button.Margin = new Padding(10, 5, 0, 20);
                            button.Click += ButtonVariation_Click;

                            Label label = new Label();
                            label.Text = item.Field3;
                            label.ForeColor = Color.Black;
                            label.AutoSize = true;
                            label.Dock = DockStyle.Top;
                            label.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Regular);
                            label.TextAlign = ContentAlignment.MiddleCenter;
                            //label.Margin = new Padding(10, 5, 0, 20);

                            tbl_button_vari.Controls.Add(button, 0, 0);
                            tbl_button_vari.Controls.Add(label, 0, 1);

                            flp_variation.Controls.Add(tbl_button_vari);

                            variflag = false;
                        }
                    }
                    #endregion

                    #region Addon
                    foreach (var item in gridSource_va)
                    {
                        if (Convert.ToInt32(item.Field6) == 2)
                        {
                            TableLayoutPanel tlp_selected_Item_Box = new TableLayoutPanel();

                            tlp_selected_Item_Box.Name = "tlp_addon_" + item.Field8 + "_" + item.Field1;
                            tlp_selected_Item_Box.AccessibleDescription = "1";
                            tlp_selected_Item_Box.Tag = item.Field8 + "_" + item.Field1;
                            tlp_selected_Item_Box.AutoSize = true;
                            //tlp_selected_Item_Box.Width = 220;
                            tlp_selected_Item_Box.Height = 30;
                            tlp_selected_Item_Box.ColumnCount = 4;

                            tlp_selected_Item_Box.BorderStyle = BorderStyle.None;
                            tlp_selected_Item_Box.BackColor = seleproduct_buttoncolor;

                            tlp_selected_Item_Box.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                            tlp_selected_Item_Box.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                            tlp_selected_Item_Box.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                            tlp_selected_Item_Box.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                            //Item Button   
                            Button Button_Item = new Button();
                            Button_Item.Text = item.Field2;
                            Button_Item.ForeColor = Color.Black;
                            Button_Item.Name = "btn_addon_" + item.Field8 + "_" + item.Field1;
                            Button_Item.Tag = item.Field8 + "_" + item.Field1;
                            Button_Item.AccessibleDescription = item.Field5;
                            Button_Item.AutoSize = true;
                            //Button_Item.Width = 150;
                            Button_Item.Height = 20;
                            Button_Item.FlatAppearance.BorderSize = 0;
                            Button_Item.FlatAppearance.BorderColor = Button_Item.BackColor;
                            Button_Item.FlatStyle = FlatStyle.Flat;
                            Button_Item.BackColor = Color.White;
                            Button_Item.Click += new EventHandler(Button_addonaddClickEventHandler);
                            Button_Item.Font = new Font(Button_Item.Font.FontFamily, 10, FontStyle.Bold);
                            tlp_selected_Item_Box.Controls.Add(Button_Item, 0, 0);

                            // Creating the label for the Qty
                            Label label = new Label();
                            label.Text = "0";
                            label.ForeColor = Color.Black;
                            label.Name = "lbl_addon_qty_" + item.Field8 + "_" + item.Field1;
                            label.Tag = item.Field8 + "_" + item.Field1;
                            label.AutoSize = true;
                            label.Dock = DockStyle.Fill;
                            label.ForeColor = Color.White;
                            label.TextAlign = ContentAlignment.MiddleCenter;
                            label.Font = new Font(label.Font.FontFamily, 10, FontStyle.Bold);
                            label.Visible = false;
                            tlp_selected_Item_Box.Controls.Add(label, 1, 0);

                            //Remove Button
                            Button Button_Remove = new Button();
                            Button_Remove.Text = "-";
                            Button_Remove.ForeColor = Color.Black;
                            Button_Remove.Name = "btn_addon_remqty_" + item.Field8 + "_" + item.Field1;
                            Button_Remove.AccessibleDescription = item.Field5;
                            Button_Remove.Tag = item.Field8 + "_" + item.Field1;
                            Button_Remove.AutoSize = true;
                            //Button_Remove.Width = 20;
                            Button_Remove.Height = 30;
                            Button_Remove.Dock = DockStyle.Fill;
                            Button_Remove.FlatAppearance.BorderSize = 0;
                            Button_Remove.FlatAppearance.BorderColor = Button_Remove.BackColor;
                            Button_Remove.FlatStyle = FlatStyle.Flat;
                            Button_Remove.BackColor = Color.White;
                            Button_Remove.Click += new EventHandler(Button_Remove_addonqtyClickEventHandler);
                            Button_Remove.Font = new Font(Button_Remove.Font.FontFamily, 10, FontStyle.Bold);
                            Button_Remove.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Button_Remove.Width, Button_Remove.Height, 50, 50));
                            tlp_selected_Item_Box.Controls.Add(Button_Remove, 2, 0);
                            flp_addons.Controls.Add(tlp_selected_Item_Box);
                            //tlp_selected_Item_Box.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, tlp_selected_Item_Box.Width, tlp_selected_Item_Box.Height, 10, 10));
                            addonflag = false;
                        }
                    }

                    #endregion

                    if (variflag)
                    {
                        label35.Visible = false;
                        flp_variation.Visible = false;
                        lblsizetotal.Visible = false;
                        label40.Visible = false;

                    }
                    else
                    {
                        label35.Visible = true;
                        flp_variation.Visible = true;
                        lblsizetotal.Visible = true;
                        label40.Visible = true;
                    }

                    if (addonflag)
                    {
                        label38.Visible = false;
                        flp_addons.Visible = false;
                        lbladdontotal.Visible = false;
                        label46.Visible = false;
                    }
                    else
                    {
                        label38.Visible = true;
                        flp_addons.Visible = true;
                        lbladdontotal.Visible = true;
                        label46.Visible = true;
                    }
                    flp_addons.ResumeLayout();
                    pnl_variation_addon.Visible = true;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page ShowVariationAddon()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }

        }
        private void Button_addonaddClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                if (Checked_VariationAddon_List.Count > 10)
                {
                    CMessageBox.Show("You can only add up to 10 Addons.", "Maximum Add-ons Reached", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    Control clickedControl = sender as Control;
                    string tlpname = "tlp_addon_" + clickedControl.Tag;
                    TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;

                    if (tlp_sele != null)
                    {
                        Label lblqty = tlp_sele.Controls.Find("lbl_addon_qty_" + clickedControl.Tag, true).FirstOrDefault() as Label;
                        if (lblqty != null)
                        {
                            if (lblqty.Text == "0")
                            {
                                lblqty.Visible = true;
                                lblqty.Text = "1";
                                string val = Convert.ToString(clickedControl.Tag);
                                string[] parts = val.Split('_');
                                Checked_VariationAddon_List.Add(new Variation_addon_List
                                {
                                    Pid = Global_ProductId,
                                    Vid = 0,
                                    CCode = parts[0],
                                    Pavid = Convert.ToInt32(parts[1]),
                                    Qty = 1,
                                    Price = Convert.ToDecimal(clickedControl.AccessibleDescription),
                                    AVType = Convert.ToInt32(Product_Addon_Variation_Type.Addon),
                                    Caption = Convert.ToString(clickedControl.Text),
                                });
                                decimal t = Convert.ToDecimal(lbladdontotal.Text) + Convert.ToDecimal(clickedControl.AccessibleDescription);
                                lbladdontotal.Text = Convert.ToString(t);
                            }
                            else
                            {
                                string val = Convert.ToString(clickedControl.Tag);
                                string[] parts = val.Split('_');
                                var existingItem = Checked_VariationAddon_List.FirstOrDefault(item => item.Pavid == Convert.ToInt32(parts[1]));
                                if (existingItem != null)
                                {
                                    int cur_qty = Convert.ToInt32(lblqty.Text) + 1;
                                    lblqty.Text = Convert.ToString(cur_qty);
                                    int indexToRemove = Checked_VariationAddon_List.IndexOf(existingItem);
                                    Checked_VariationAddon_List[indexToRemove].Qty = cur_qty;
                                    decimal t = Convert.ToDecimal(lbladdontotal.Text) + Convert.ToDecimal(clickedControl.AccessibleDescription);
                                    lbladdontotal.Text = Convert.ToString(t);
                                }

                            }
                            Button btnrem = tlp_sele.Controls.Find("btn_addon_remqty_" + clickedControl.Tag, true).FirstOrDefault() as Button;
                            //btnrem.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnrem.Width, btnrem.Height, btnrem.Width, btnrem.Height));
                            btnrem.Dock = DockStyle.Right;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page Button_addonaddClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }
        }
        private void Button_Remove_addonqtyClickEventHandler(object sender, EventArgs e)
        {

            try
            {
                Control clickedControl = sender as Control;
                string tlpname = "tlp_addon_" + clickedControl.Tag;
                TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;

                if (tlp_sele != null)
                {
                    Label lblqty = tlp_sele.Controls.Find("lbl_addon_qty_" + clickedControl.Tag, true).FirstOrDefault() as Label;
                    if (lblqty != null)
                    {
                        int cur_qty = Convert.ToInt32(lblqty.Text);
                        if (cur_qty > 0)
                        {
                            string val = Convert.ToString(clickedControl.Tag);
                            string[] parts = val.Split('_');
                            var existingItem = Checked_VariationAddon_List.FirstOrDefault(item => item.Pavid == Convert.ToInt32(parts[1]));
                            if (existingItem != null)
                            {
                                cur_qty--;
                                lblqty.Text = Convert.ToString(cur_qty);
                                int indexToRemove = Checked_VariationAddon_List.IndexOf(existingItem);
                                Checked_VariationAddon_List[indexToRemove].Qty = cur_qty;
                                decimal t = Convert.ToDecimal(lbladdontotal.Text) - Convert.ToDecimal(Checked_VariationAddon_List[indexToRemove].Price);
                                lbladdontotal.Text = Convert.ToString(t);
                            }
                        }
                        if (cur_qty == 0)
                        {
                            string val = Convert.ToString(clickedControl.Tag);
                            string[] parts = val.Split('_');
                            var existingItem = Checked_VariationAddon_List.FirstOrDefault(item => item.Pavid == Convert.ToInt32(parts[1]));
                            if (existingItem != null)
                            {
                                lblqty.Visible = false;
                                int indexToRemove = Checked_VariationAddon_List.IndexOf(existingItem);
                                if (indexToRemove >= 0)
                                {
                                    Checked_VariationAddon_List.RemoveAt(indexToRemove);
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page Button_Remove_addonqtyClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }
        }

        private void ButtonVariation_Click(object sender, EventArgs e)
        {

            try
            {
                Button clickedControl = (Button)sender;
                if (previousClickedButton != null && previousClickedButton == clickedControl)
                {
                    clickedControl.BackColor = ColorTranslator.FromHtml("#3f88c5");
                    previousClickedButton = null;
                    lblsizetotal.Text = "0";
                }
                else
                {
                    if (previousClickedButton != null)
                    {
                        previousClickedButton.BackColor = ColorTranslator.FromHtml("#3f88c5");
                    }
                    clickedControl.BackColor = Color.Red;
                    previousClickedButton = clickedControl;

                    var existingItem = Checked_VariationAddon_List.FirstOrDefault(item => item.AVType == Convert.ToInt32(Product_Addon_Variation_Type.Variation));
                    if (existingItem != null)
                    {
                        int indexToUpdate = Checked_VariationAddon_List.IndexOf(existingItem);
                        Checked_VariationAddon_List[indexToUpdate] = new Variation_addon_List
                        {
                            Pid = Global_ProductId,
                            Vid = Convert.ToInt32(previousClickedButton.Tag),
                            Aid = Convert.ToInt32(clickedControl.Tag),
                            Pavid = Convert.ToInt32(clickedControl.Tag),
                            Qty = 1,
                            Price = Convert.ToDecimal(clickedControl.AccessibleDescription),
                            AVType = Convert.ToInt32(Product_Addon_Variation_Type.Variation),
                            Caption = Convert.ToString(clickedControl.Text),
                        };
                    }
                    else
                    {
                        Checked_VariationAddon_List.Add(new Variation_addon_List
                        {
                            Pid = Global_ProductId,
                            Vid = Convert.ToInt32(previousClickedButton.Tag),
                            Aid = Convert.ToInt32(clickedControl.Tag),
                            Pavid = Convert.ToInt32(clickedControl.Tag),
                            Qty = 1,
                            Price = Convert.ToDecimal(clickedControl.AccessibleDescription),
                            AVType = Convert.ToInt32(Product_Addon_Variation_Type.Variation),
                            Caption = Convert.ToString(clickedControl.Text),
                        });
                    }
                    lblsizetotal.Text = Convert.ToString(clickedControl.AccessibleDescription);
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page ButtonVariation_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }
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
                    if (Convert.ToInt32(dgvmenu.Rows[cellproductid].Cells[itabletype].Value) != Convert.ToInt32(Tabletype.ComboProduct))
                    {
                        cellDiscountAmount(cellproductid);

                        discount = Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[idisamt].Value);

                        cellTaxAmount(cellproductid, amount - discount);

                        if (Convert.ToString(dgvmenu.Rows[cellproductid].Cells[iTax].Value) == "Exclusive")
                        {
                            decimal tot = (amount - Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[idisamt].Value))
                        + Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[istaxamt].Value);
                            dgvmenu.Rows[cellproductid].Cells[isamount].Value = tot.ToString("N2");

                        }
                        if (Convert.ToString(dgvmenu.Rows[cellproductid].Cells[iTax].Value) == "Inclusive")
                        {
                            decimal tot = (amount - Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[idisamt].Value));
                            dgvmenu.Rows[cellproductid].Cells[isamount].Value = tot.ToString("N2");
                        }
                    }
                    else
                    {
                        decimal newtaxamt = Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[ibackuptaxamt].Value) * Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[iqty].Value);
                        dgvmenu.Rows[cellproductid].Cells[istaxamt].Value = newtaxamt.ToString("N2");
                    }
              }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page totalcellamount()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page cellDiscountAmount()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page cellDiscountPercentage()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

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
                        decimal divtax = TaxAmount / 2;
                        dgvmenu.Rows[cellproductid].Cells[isgst].Value = divtax.ToString("N2");
                        dgvmenu.Rows[cellproductid].Cells[icgst].Value = divtax.ToString("N2");
                    }
                    if (gridSource[0].TaxType == "VAT")
                    {
                        decimal TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].VatPrcnt) * Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[isamount].Value) / 100);
                        dgvmenu.Rows[cellproductid].Cells[istaxamt].Value = TaxAmount.ToString("N2");
                        dgvmenu.Rows[cellproductid].Cells[ivat].Value = TaxAmount.ToString("N2");
                        dgvmenu.Rows[cellproductid].Cells[iTaxper].Value = gridSource[0].VatPrcnt;
                    }
                }
                // dgvmenu.Rows[cellproductid].Cells[istaxamt].Value = Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[isamount].Value) * Convert.ToDecimal(dgvmenu.Rows[cellproductid].Cells[iTaxper].Value) / 100;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page cellTaxAmount()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

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
            try
            {
                textb_shape();
                ClsGeneral.GrideAutoSize(dgvforms);
                ClsGeneral.GrideAutoSize(dgvmenu);
                ClsGeneral.GrideAutoSize(dgvItemPop);
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page FrmReservation_Resize()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }


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
                    if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[mformsenumno].Value) == Convert.ToInt32(formsenum.Menu))
                    {
                        objSearch.Select = "MId ,Name as Title,p.Price,p.Tax,p.TaxType ,IgstPrcnt,VatPrcnt ,ts.TaxSettingsId," +
                            "ItemCode as ItemCode_UPC,CgstPrcnt,SgstPrcnt,p.Fcid," +
                            "p.DiscountAmt as DisAmt,p.DiscountPer as DisPer ";
                        objSearch.Table = " tblMenu m left join tblProduct p on m.ProductId=p.ProductId " +
                            " left join tblTaxSettings ts on p.TaxSettingsId=ts.TaxSettingsId ";
                        objSearch.WhereNdGroupBy = " where MId =" + dgvItemPop.Rows[r].Cells[midno].Value;
                        objSearch.OrderBy = "";
                    }
                    else if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[mformsenumno].Value) == Convert.ToInt32(formsenum.Item))
                    {
                        objSearch.Select = " ItemId,Itemname ,p.SRate,p.PRate,p.MRP,i.Tax,i.TaxType,IgstPrcnt,VatPrcnt,CgstPrcnt,SgstPrcnt," +
                            " unit,ItemCode_UPC,p.Id as PrId,Fcid,ts.TaxSettingsId,quantity as QOH,i.DiscountAmt,i.DiscountPer ";
                        objSearch.Table = "  tblItem i  left join tblTaxSettings ts on i.TaxSettingsId=ts.TaxSettingsId " +
                            " left join tblProductRate p   on ProductId=i.ItemId  and p.QOH>0";
                        objSearch.WhereNdGroupBy = " where  Saletype=1 and  ItemId =" + dgvItemPop.Rows[r].Cells[midno].Value;
                        objSearch.OrderBy = "";
                    }
                    else if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[mformsenumno].Value) == Convert.ToInt32(formsenum.Discount_And_Offer))
                    {
                        objSearch.Select = "ofm.OfferName ,ofm.FinalPrice,ofm.Omid , ofm.TaxType , ofm.Tax , ofm.TaxSettingsId , ofm.Discountper , ofd.Productname, " +
                            " CgstPrcnt,SgstPrcnt,VatPrcnt,IgstPrcnt ,ofm.DiscountAmt,ofm.Discountper,ofm.TaxSettingsId ";

                        objSearch.Table = "  tblOfferMaster ofm LEFT JOIN tblOfferDetails ofd on ofm.Omid=ofd.Omid  left join tblTaxSettings ts  on ts.TaxSettingsId=ofm.TaxSettingsId ";
                        objSearch.WhereNdGroupBy = " where ofm.Omid =" + dgvItemPop.Rows[r].Cells[midno].Value;
                        objSearch.OrderBy = "";
                    }
                    if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[mformsenumno].Value) == Convert.ToInt32(formsenum.Menu))
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
                                dgvmenu.Rows[r1].Cells[iprid].Value = gridSource[0].PrId;
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
                    else if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[mformsenumno].Value) == Convert.ToInt32(formsenum.Item))
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
                            pnl_menu.Location = new Point(
                              this.ClientSize.Width / 5 - pnl_menu.Size.Width / 5,
                              (this.ClientSize.Height / 5 - pnl_menu.Size.Height / 5));
                            pnl_menu.Anchor = AnchorStyles.None;
                            dgvmenu.Enabled = false;
                            dgv_spopup.Enabled = true;
                            pnl_menu.Visible = true;
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
                    else if (Convert.ToInt32(dgvItemPop.Rows[r].Cells[mformsenumno].Value) == Convert.ToInt32(formsenum.Discount_And_Offer))
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
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page dgvItemPop_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }
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
                if(gridSource.Count>0)
                {
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
                    else
                    {
                        CMessageBox.Show("Avilable Stock Quanity Is " + gridSource[0].QOH, "Insufficent Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    dgvmenu.Enabled = true;
                    pnl_menu.Visible = false;
                    dgvmenu.Focus();
                    panelProductPopup.Visible = false;
                    dgvmenu.Enabled = true;
                    dgvmenu.Focus();
                }
                
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page dgv_spopup_CellClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            pnl_menu.Visible = false;
            panelProductPopup.Visible = false;
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
                    pnl_menu.Visible = false;
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page dgv_spopup_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page OfferCheckItem()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
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
            try
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
                    if (e.KeyData == Keys.Up)
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page gridefllow()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }

        }

        private void btnItemPopclose_Click(object sender, EventArgs e)
        {
            try
            {
                panelProductPopup.Visible = false;
                tlp_main.Enabled = true;
                dgvmenu.Focus();
                Recallmenu();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page btnItemPopclose_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void txtname_search_Enter(object sender, EventArgs e)
        {
            try
            {
                if (txtname_search.Text == "Food Name .....")
                {
                    txtname_search.Text = "";
                    txtname_search.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page txtname_search_Enter()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void txtname_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                    e.Handled = true;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page txtname_search_KeyPress()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void txtname_search_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtname_search.Text == "")
                {
                    txtname_search.Text = "Food Name .....";
                    txtname_search.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page txtname_search_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }


        private void txtname_search_TextChanged(object sender, EventArgs e)
        {

            try
            {
                if (txtname_search.Text != "" && txtname_search.Text != null && txtname_search.Text != "Food Name .....")
                {

                    dgvItemPop.Rows.Clear();
                    BllSearch<DmnGridFields> _bllSearch = new BllSearch<DmnGridFields>();
                    string returnValue = string.Empty;
                    dgvItemPop.Rows.Clear();
                    DmnSearch _objSearch = new DmnSearch();
                    _objSearch.Select = " MId as Id ,Name as Field1 ,p.Price as Field2,  'Menu' as Field3, " + Convert.ToInt32(formsenum.Menu) + " Field4 ,p.Tax as Field5,p.TaxType as Field6," +
                        "(select count(*) from tblOfferMaster ofm left join tblOfferDetails ofd on ofd.Omid=ofm.Omid and ofd.Status=1 where ofm.Status=1 AND OfferType=0 AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME() and ofd.ProductId= m.ProductId) as Field7";
                    _objSearch.Table = " tblMenu m   left join tblProduct p on m.ProductId = p.ProductId  where  m.Status = 1 " +
                        "and p.Status = 1 and name like '%" + txtname_search.Text + "%'" +
                        " UNION  select i.ItemId  as Id, i.Itemname as Field1,(Select Top(1)Srate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field2,'Item' as Field3, " + Convert.ToInt32(formsenum.Item) + " Field4 ,i.Tax as Field5,i.TaxType as Field6,0 Field7 from tblItem i  where Saletype=1 and Itemname like '%" + txtname_search.Text + "%'" +
                        "  UNION  select ofm.Omid as Id,ofm.OfferName Field1,ofm.FinalPrice Field2,'Offer' as Field3," + Convert.ToInt32(formsenum.Discount_And_Offer) + " Field4 , ofm.Tax Field5,ofm.TaxType Field6,0 Field7  from  tblOfferMaster ofm " +
                        "  LEFT JOIN tblOfferDetails ofd on ofm.Omid=ofd.Omid ";
                    _objSearch.WhereNdGroupBy = "where ofd.Status=1 and ofm.OfferType = 1 and ofm.OfferName LIKE '%" + txtname_search.Text + "%'";
                    _objSearch.OrderBy = "";
                    var gridSource = _bllSearch.GetSearchData("prdSearch", _objSearch);
                    if (gridSource.Count > 0)
                    {
                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            dgvItemPop.Rows.Add(1);
                            dgvItemPop.Rows[i].Cells[mslnono].Value = i + 1;
                            dgvItemPop.Rows[i].Cells[mNameno].Value = gridSource[i].Field1;
                            dgvItemPop.Rows[i].Cells[mPriceno].Value = gridSource[i].Field2;
                            dgvItemPop.Rows[i].Cells[mproducttypenameno].Value = gridSource[i].Field3;
                            dgvItemPop.Rows[i].Cells[mTaxno].Value = gridSource[i].Field5;
                            dgvItemPop.Rows[i].Cells[midno].Value = gridSource[i].Id;


                            DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgvItemPop.Rows[i].Cells[mproducticono];
                            cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            if (Convert.ToInt32(gridSource[i].Field4) == Convert.ToInt32(formsenum.Discount_And_Offer))
                            {
                                cellcolumntype.Value = Properties.Resources.kitchengridcomboicon;
                                cellcolumntype.ToolTipText = gridSource[i].Field1;
                            }
                            else if (Convert.ToInt32(gridSource[i].Field7) > 0)
                            {
                                cellcolumntype.Value = Properties.Resources.offer_button;
                                cellcolumntype.ToolTipText = gridSource[i].Field1;
                            }
                            else
                            {
                                if (Convert.ToInt32(dgvItemPop.Rows[i].Cells[mslnono].Value) % 2 == 0)
                                    cellcolumntype.Value = Properties.Resources.orderview_blank_2;
                                else
                                    cellcolumntype.Value = Properties.Resources.orderview_blank;
                            }

                        }

                    }
                    else
                        CMessageBox.Show("No Products", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)

            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page txtname_search_TextChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }
        }
        public void combofill()
        {

            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "  0 as fcid,'Combo' as CategoryName  UNION " +
                    "select fcid,CategoryName";
                objSearch.Table = " tblCategory_FTI";
                objSearch.WhereNdGroupBy = "where Status=1 and CategoryType=0";
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                if (gridSource.Count > 0)
                {
                    cmb_productlist.DataSource = gridSource;
                    cmb_productlist.ValueMember = "Id";
                    cmb_productlist.DisplayMember = "Value";
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page combofill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }
        }

        private void cmb_productlist_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ProductPopup(Convert.ToInt32(cmb_productlist.SelectedValue));
        }
        public void ProductPopup(int fcid)
        {

            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch _objSearch = new DmnSearch();
                if (fcid == 0)
                {
                    _objSearch.Select = " ofm.Omid as Id,ofm.OfferName Field1,ofm.FinalPrice Field2,'Offer' as Field3," + Convert.ToInt32(formsenum.Discount_And_Offer) + " Field4 , ofm.Tax Field5,ofm.TaxType Field6,0 Field7";
                    _objSearch.Table = " tblOfferMaster ofm LEFT JOIN tblOfferDetails ofd on ofm.Omid=ofd.Omid";
                    _objSearch.WhereNdGroupBy = " Where ofd.Status=1 and ofm.OfferType = 1";
                }
                else
                {
                    _objSearch.Select = "MId as Id , Name as Field1,p.Price as Field2,  'Menu' as Field3, " + Convert.ToInt32(formsenum.Menu) + " Field4 ,p.Tax as Field5,p.TaxType as Field6, (select count(*) from tblOfferMaster ofm left join tblOfferDetails ofd on ofd.Omid=ofm.Omid and ofd.Status=1 where ofm.Status=1 AND OfferType=0 AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME() and ofd.ProductId= m.ProductId) as Field7";
                    _objSearch.Table = " tblMenu m   left join tblProduct p on m.ProductId = p.ProductId and  p.Fcid= " + fcid +
                        " where m.Status = 1 and p.Status = 1 " +
                         "   UNION " +
                          "   select i.ItemId  as Id, i.Itemname as Field1," +
                          " (Select Top(1)Srate from tblProductRate where ProductId=i.ItemId order by Id desc) as Field2," +
                          " 'Item' as Field3, " + Convert.ToInt32(formsenum.Item) + " Field4 ,i.Tax as Field5,i.TaxType as Field6,0 Field7" +
                           " from tblItem i";
                    _objSearch.WhereNdGroupBy = "where i.Status=1 and i.Saletype= 1 and i.Fcid=" + fcid;
                }
                _objSearch.OrderBy = "";
                var Datasource = bllSearch.GetSearchData("prdSearch", _objSearch);
                if (Datasource.Count > 0)
                {
                    dgvItemPop.Rows.Clear();
                    dgvItemPop.Rows.Add(Datasource.Count);
                    for (int i = 0; i < Datasource.Count; i++)
                    {
                        dgvItemPop.Rows[i].Cells[mslnono].Value = i + 1;
                        dgvItemPop.Rows[i].Cells[mNameno].Value = Datasource[i].Field1;
                        dgvItemPop.Rows[i].Cells[mPriceno].Value = Datasource[i].Field2;
                        dgvItemPop.Rows[i].Cells[mproducttypenameno].Value = Datasource[i].Field3;
                        dgvItemPop.Rows[i].Cells[mformsenumno].Value = Datasource[i].Field4;
                        dgvItemPop.Rows[i].Cells[mTaxno].Value = Datasource[i].Field5;
                        dgvItemPop.Rows[i].Cells[midno].Value = Datasource[i].Id;

                        DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgvItemPop.Rows[i].Cells[mproducticono];
                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        if (Convert.ToInt32(Datasource[i].Field4) == Convert.ToInt32(formsenum.Discount_And_Offer))
                        {
                            cellcolumntype.Value = Properties.Resources.kitchengridcomboicon;
                            cellcolumntype.ToolTipText = Datasource[i].Field1;
                        }
                        else if (Convert.ToInt32(Datasource[i].Field7) > 0)
                        {
                            cellcolumntype.Value = Properties.Resources.offer_button;
                            cellcolumntype.ToolTipText = Datasource[i].Field1;
                        }
                        else
                        {
                            if (Convert.ToInt32(dgvItemPop.Rows[i].Cells[mslnono].Value) % 2 == 0)
                                cellcolumntype.Value = Properties.Resources.orderview_blank_2;
                            else
                                cellcolumntype.Value = Properties.Resources.orderview_blank;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page ProductPopup()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void discountper_Enter(object sender, EventArgs e)
        {
            try
            {
                GrandTotal();
                CalNetAmt();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page discountper_Enter()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        

        private void btn_rev_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                var result = CMessageBox.Show("Do You want to cancel this reservation?", "Confirm the Action", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    chb_active.Checked = false;
                    save();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page btn_rev_cancel_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void dgvforms_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == itablename)
                {
                    var cellValue = dgvforms.Rows[e.RowIndex].Cells[itid].Value;
                    if (cellValue == null || Convert.ToInt32(cellValue) == 0)
                    {
                        dgvforms.Rows[e.RowIndex].Cells[itablename].Value = "";
                        dgvforms.Rows[e.RowIndex].Cells[itablename].Style.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page dgvforms_CellLeave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }


        private void dgvmenu_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == imname && e.RowIndex >= 0)
                {
                    var cellValue = dgvmenu.Rows[e.RowIndex].Cells[imid].Value;
                    if (cellValue == null || Convert.ToInt32(cellValue) == 0)
                    {
                        dgvmenu.Rows[e.RowIndex].Cells[imname].Value = "";
                        dgvmenu.Rows[e.RowIndex].Cells[imname].Style.ForeColor = Color.Black;
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page dgvmenu_CellLeave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }


        private void tc_rese_menu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tc_rese_menu.SelectedTab == tp_reservation)
                {
                    RecallTable();
                }
                else if (tc_rese_menu.SelectedTab == tp_menu)
                {
                    Recallmenu();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("Reservation tc_rese_menu_SelectedIndexChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }
        private void btn_va_ok_Click(object sender, EventArgs e)
        {
            try
            {
                int rc = dgvmenu.RowCount;
                int rowno = 0;
                BllSearch<DmnVariation> bllSearch = new BllSearch<DmnVariation>();
                DmnSearch objSearch = new DmnSearch();
                int r1 = Current_menuGrid_rowno;
                if (variflag == false && previousClickedButton == null)// addonflag 
                {
                    CMessageBox.Show("Please Select Size", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                pnl_variation_addon.SuspendLayout();
                pnl_variation_addon.Visible = false;
                int vid = 0, pid = 0;
                string combinedId = "";
                Checked_VariationAddon_List = Checked_VariationAddon_List.OrderBy(item => item.Pavid).ToList();
                var existingItem = Checked_VariationAddon_List.FirstOrDefault(item => item.AVType == Convert.ToInt32(Product_Addon_Variation_Type.Variation));
                if (existingItem != null)
                {
                    int indexToUpdate = Checked_VariationAddon_List.IndexOf(existingItem);
                    vid = Checked_VariationAddon_List[indexToUpdate].Pavid;
                    pid = Checked_VariationAddon_List[indexToUpdate].Pid;
                }
                else
                    pid = Global_ProductId;

                int AddonCount = 0;
                for (int i = 0; i < Checked_VariationAddon_List.Count; i++)
                {
                    var item = Checked_VariationAddon_List[i];
                    if (item.AVType == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                    {
                        combinedId = combinedId + $"{item.CCode}" + $"{item.Qty}";
                        AddonCount = AddonCount + 1;
                    }
                }
                string Av_ID = Convert.ToString(pid) + Convert.ToString(vid) + "_" + combinedId;

                #region grid Inseration

                objSearch.Select = "p.ProductId ProductId ,Name as Title,p.Price,p.Tax,p.TaxType ,IgstPrcnt,VatPrcnt ,ts.TaxSettingsId," + "ItemCode as ItemCode_UPC,CgstPrcnt,SgstPrcnt,p.Fcid," +
              "p.DiscountAmt as DisAmt,p.DiscountPer as DisPer,pav.Type pavType,pav.Caption Variationname,pav.Price VariationPrice,a.AddonName AddonName,a.Price AddonNamePrice,pav.Pavid Pavid ";
                objSearch.Table = "tblProduct p " +
                    " LEFT JOIN tblMenu m on m.ProductId=p.ProductId " +
                    " left join tblTaxSettings ts on p.TaxSettingsId=ts.TaxSettingsId" +
                    " LEFT JOIN tblProduct_Addon_Variation pav on pav.ProductId=p.ProductId and pav.status=1" +
                    " LEFT JOIN tblAddon a on a.Aid = pav.Aid and a.status = 1" +
                    " LEFT JOIN tblOfferDetails ofd on ofd.ProductId=p.ProductId and ofd.status=1 " +
                    " LEFT JOIN tblOfferMaster ofm on ofm.Omid=ofd.Omid and ofm.status=1 AND ofm.OfferType=0  AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME()";
                objSearch.WhereNdGroupBy = "where p.Status=1 and m.Status=1  and p.ProductId= " + Global_ProductId;
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    int qty = 1;
                    decimal price = 0;
                    bool flag = true;
                    int i = 0;

                    string vrianame = gridSource
                   .Where(item => Convert.ToInt32(item.Pavid) == vid)
                   .Select(item => item.Variationname.ToString())
                   .FirstOrDefault();

                    for (i = 0; i <= dgvmenu.RowCount - 1; i++)
                    {
                        if (Av_ID == Convert.ToString(dgvmenu.Rows[i].Cells[iAvid].Value) && (Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddonstatus].Value) == Convert.ToInt32(Product_Addon_Variation_Type.Variation)))
                        {
                            rowno = i;
                            flag = false;
                            qty += Convert.ToInt32(dgvmenu.Rows[i].Cells[iqty].Value);
                            price += Convert.ToDecimal(dgvmenu.Rows[i].Cells[imprice].Value);
                            break;
                        }
                    }

                    #region New Inseration
                    if (flag == true)
                    {
                        int mainrowno = 0;
                        mainrowno = r1;
                        decimal disper = OfferCheckItem(Convert.ToInt32(gridSource[0].ProductId), Convert.ToInt32(Tabletype.Product));
                        if (disper == 0)
                        {
                            dgvmenu.Rows[r1].Cells[idisper].Value = gridSource[0].DisPer;
                        }
                        else
                        {
                            dgvmenu.Rows[r1].Cells[idisper].Value = disper;
                        }

                        DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgvmenu.Rows[r1].Cells[iaction];
                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        cellcolumntype.Value = Properties.Resources.food_cancel_icon;
                        //cellcolumntype.ToolTipText ="delete";

                        dgvmenu.Rows[r1].Cells[islno].Value = r1 + 1;
                        dgvmenu.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                        dgvmenu.Rows[r1].Cells[imid].Value = gridSource[0].ProductId;
                        dgvmenu.Rows[r1].Cells[imBookingDetailId].Value = 0;

                        string shortVrianame = TruncateString(vrianame, maxLength_variation);
                        if (AddonCount > 0 && vrianame != null && vrianame != "")
                            dgvmenu.Rows[r1].Cells[imname].Value = gridSource[0].Title + "(" + shortVrianame + ") Add[" + AddonCount + "]";
                        else if (AddonCount > 0)
                            dgvmenu.Rows[r1].Cells[imname].Value = gridSource[0].Title + " Add[" + AddonCount + "]";
                        else if (vrianame != null && vrianame != "")
                            dgvmenu.Rows[r1].Cells[imname].Value = gridSource[0].Title + "(" + vrianame + ")";
                        else
                            dgvmenu.Rows[r1].Cells[imname].Value = gridSource[0].Title;

                        dgvmenu.Rows[r1].Cells[imname].Style.ForeColor = Color.Black;
                        dgvmenu.Rows[r1].Cells[imprice].Value = gridSource[0].Price;
                        dgvmenu.Rows[r1].Cells[iqty].Value = 1;
                        dgvmenu.Rows[r1].Cells[imdescription].Value = gridSource[0].Description;
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
                        dgvmenu.Rows[r1].Cells[iprid].Value = gridSource[0].PrId;
                        dgvmenu.Rows[r1].Cells[itaxsettingid].Value = gridSource[0].TaxSettingsId;
                        dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.Product);

                        dgvmenu.Rows[r1].Cells[iVarationName].Value = vrianame;
                        dgvmenu.Rows[r1].Cells[iAddoncount].Value = AddonCount;
                        dgvmenu.Rows[r1].Cells[ivid].Value = vid;
                        dgvmenu.Rows[r1].Cells[iAddonstatus].Value = Convert.ToInt32(Product_Addon_Variation_Type.Variation);
                        dgvmenu.Rows[r1].Cells[iAvid].Value = Av_ID;

                        //dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(formsenum.Menu);
                        dgvmenu.Rows[r1].Cells[iproductType].Value = gridSource[0].Fcid;

                        txtqty.Text = Convert.ToString(dgvmenu.Rows[r1].Cells[iqty].Value);

                        #region Variation or Addon Grid Inseration
                        decimal Total_Addon_Amt = 0;
                        int add_slno = 0;
                        for (int j = 0; j < Checked_VariationAddon_List.Count; j++)
                        {
                            var item = Checked_VariationAddon_List[j];
                            if (item.AVType == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                            {
                                dgvmenu.Rows.Add(1);
                                r1 = dgvmenu.RowCount - 1;
                                DataGridViewImageCell cellcolumntype_addon = (DataGridViewImageCell)dgvmenu.Rows[r1].Cells[iaction];
                                cellcolumntype_addon.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                cellcolumntype_addon.Value = Properties.Resources.orderview_addon_removeicon;

                                dgvmenu.Rows[r1].Cells[islno].Value = add_slno + 1;
                                dgvmenu.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dgvmenu.Rows[r1].Cells[imid].Value = item.Pavid;
                                dgvmenu.Rows[r1].Cells[imBookingDetailId].Value = 0;
                                dgvmenu.Rows[r1].Cells[imname].Value = item.Caption;
                                dgvmenu.Rows[r1].Cells[imprice].Value = item.Price;
                                dgvmenu.Rows[r1].Cells[iqty].Value = item.Qty;
                                dgvmenu.Rows[r1].Cells[isamount].Value = Convert.ToDecimal(item.Price) * Convert.ToDecimal(item.Qty);
                                dgvmenu.Rows[r1].Cells[iFormType].Value = Convert.ToInt32(formsenum.Menu);
                                dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.Product);

                                dgvmenu.Rows[r1].DefaultCellStyle.BackColor = Color.OliveDrab;
                                dgvmenu.Rows[r1].Cells[iAvid].Value = item.Pavid;
                                dgvmenu.Rows[r1].Cells[iAddoncount].Value = 0;
                                dgvmenu.Rows[r1].Cells[iAddonstatus].Value = Convert.ToInt32(Product_Addon_Variation_Type.Addon);
                                dgvmenu.Rows[r1].Cells[iAvid].Value = Av_ID;
                                dgvmenu.Rows[r1].Cells[iPavid].Value = Convert.ToInt32(item.Pavid);
                                dgvmenu.Rows[r1].Cells[iaddoncode].Value = Convert.ToString(item.CCode);
                                dgvmenu.Rows[r1].Cells[imarketprice].Value = Convert.ToDecimal(item.Price);

                                Total_Addon_Amt = Total_Addon_Amt + Convert.ToDecimal(item.Price) * Convert.ToDecimal(item.Qty);
                                add_slno = add_slno + 1;
                            }
                        }

                        #endregion

                        var Variationprice = Checked_VariationAddon_List.FirstOrDefault(item => item.AVType == Convert.ToInt32(Product_Addon_Variation_Type.Variation));
                        if (Variationprice != null)
                        {
                            int indexToRemove = Checked_VariationAddon_List.IndexOf(Variationprice);
                            dgvmenu.Rows[mainrowno].Cells[imprice].Value = Convert.ToDecimal(Checked_VariationAddon_List[indexToRemove].Price) + Total_Addon_Amt;
                        }
                        else
                        {
                            dgvmenu.Rows[mainrowno].Cells[imprice].Value = Convert.ToDecimal(dgvmenu.Rows[mainrowno].Cells[imprice].Value) + Total_Addon_Amt;
                        }
                        totalcellamount(mainrowno);
                        dgvmenu.Rows[mainrowno].Cells[imarketprice].Value = Convert.ToDecimal(dgvmenu.Rows[mainrowno].Cells[isamount].Value);
                        dgvmenu.Rows.Add(1);
                    }
                    #endregion

                    else
                    {
                        dgvmenu.Rows[rowno].Cells[iqty].Value = qty;
                        dgvmenu.Rows[rowno].Cells[imprice].Value = price;
                        txtqty.Text = Convert.ToString(dgvmenu.Rows[i].Cells[iqty].Value);
                        totalcellamount(i);
                    }
                }

                lblMTotal.Text = Convert.ToString(dgvmenu.Rows[rowno].Cells[isamount].Value);
                panelProductPopup.Visible = false;
                dgvmenu.Enabled = true;
                dgvmenu.Focus();

                #endregion

                SumMenu();
                GrandTotal();
                CalNetAmt();
                Recallmenu();
                GridSlno_Refresh();
                if (dgvmenu.Controls.OfType<VScrollBar>().First().Visible && tlp_reg_design.Visible != false)
                {
                    tlp_reg_design.Visible = false;
                    btn_res_details.BackgroundImage = null;
                    btn_res_details.BackgroundImage = FoodandBeverage.Properties.Resources.res_show_button;
                }
                Checked_VariationAddon_List.Clear();
                pnl_variation_addon.ResumeLayout();
                tlp_main.Enabled = true;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page btn_va_ok_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }
        }
        string TruncateString(string input, int maxLength)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return input.Length <= maxLength ? input : input.Substring(0, maxLength) + "..";
        }
        private void btn_va_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                tlp_main.Enabled = true;
                pnl_variation_addon.Visible = false;
                Recallmenu();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page btn_va_cancel_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }


        private void dgvmenu_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgvmenu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == iaction && Convert.ToInt32(dgvmenu.Rows[e.RowIndex].Cells[imid].Value) > 0 && Expireflag == false)
                {
                    if (Convert.ToInt32(dgvmenu.Rows[e.RowIndex].Cells[iAddonstatus].Value) == Convert.ToInt32(AddonType.AddonType) || Convert.ToInt32(dgvmenu.Rows[e.RowIndex].Cells[iAddonstatus].Value) == Convert.ToInt32(AddonType.None))
                    {
                        #region Product & product With addon (complete Remove )
                        var itemName = dgvmenu.Rows[e.RowIndex].Cells[imname].Value.ToString();
                        var result = CMessageBox.Show($"Are you sure you want to remove {itemName} ? ", "Confirm Action", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            //Proudct
                            if (Convert.ToInt32(dgvmenu.Rows[e.RowIndex].Cells[iAddonstatus].Value) == Convert.ToInt32(AddonType.None))
                            {
                                dgvmenu.Rows.RemoveAt(dgvmenu.CurrentCell.RowIndex);
                                if (dgvmenu.Rows.Count == 0)
                                {
                                    dgvmenu.Rows.Add(1);
                                }
                            }

                            //product with addon
                            else if (Convert.ToInt32(dgvmenu.Rows[e.RowIndex].Cells[iAddonstatus].Value) == Convert.ToInt32(AddonType.AddonType))
                            {
                                int dgvcount = dgvmenu.RowCount - 1;
                                string remove_id = Convert.ToString(dgvmenu.Rows[e.RowIndex].Cells[iAvid].Value);
                                for (int i = dgvcount; i >= 0; i--)
                                {
                                    if (remove_id == Convert.ToString(dgvmenu.Rows[i].Cells[iAvid].Value))
                                    {
                                        dgvmenu.Rows.RemoveAt(i);
                                        //if (Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddonstatus].Value) == Convert.ToInt32(Product_Addon_Variation_Type.Variation))
                                        //{
                                        //    decimal TotalPrice = Convert.ToDecimal(dgvmenu.Rows[i].Cells[imarketprice].Value) * Convert.ToDecimal(dgvmenu.Rows[i].Cells[iqty].Value);
                                        //    decimal totalamt = Convert.ToDecimal(lblMTotal.Text) - TotalPrice;
                                        //    lblMTotal.Text = Convert.ToString(totalamt);
                                        //    dgvmenu.Rows.RemoveAt(i);
                                        //}
                                        //else
                                        //{
                                        // }
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else if (Convert.ToInt32(dgvmenu.Rows[e.RowIndex].Cells[iAddonstatus].Value) == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                    {
                        #region Addon items Remove

                        decimal newprice = 0, discountprice = 0, TaxAmount = 0, finalprice = 0, disAmnt = 0;
                        string newpart = "";
                        int addoncount = 0;

                        #region To Reset Addon ID(remove id form avid name)
                        string avidValue = Convert.ToString(dgvmenu.Rows[e.RowIndex].Cells[iAvid].Value);
                        string[] avidParts = avidValue.Split('_');
                        string extractedpid = $"{avidParts[0]}";
                        newpart = newpart + extractedpid + "_";
                        int remove_pavid = Convert.ToInt32(dgvmenu.Rows[e.RowIndex].Cells[iPavid].Value);
                        string remove_avid = Convert.ToString(dgvmenu.Rows[e.RowIndex].Cells[iAvid].Value);
                        int rowno = 0;
                        for (int i = 0; i < dgvmenu.RowCount; i++)
                        {
                            if (Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddonstatus].Value) == Convert.ToInt32(Product_Addon_Variation_Type.Addon) && Convert.ToInt32(dgvmenu.Rows[i].Cells[iPavid].Value) != remove_pavid && Convert.ToString(dgvmenu.Rows[i].Cells[iAvid].Value) == remove_avid)
                            {
                                string ccode = Convert.ToString(dgvmenu.Rows[i].Cells[iaddoncode].Value);
                                decimal quantityDecimal = Convert.ToDecimal(dgvmenu.Rows[i].Cells[iqty].Value);
                                int quantityInt = (int)Math.Floor(quantityDecimal);
                                newpart = newpart + ccode + quantityInt.ToString();
                                addoncount = addoncount + 1;
                            }
                            if (Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddonstatus].Value) == Convert.ToInt32(Product_Addon_Variation_Type.Variation) && Convert.ToString(dgvmenu.Rows[i].Cells[iAvid].Value) == remove_avid)
                                rowno = i;
                        }
                        #endregion

                        #region Price Reset main product 
                        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "Price Field1";
                        dmnSearch.Table = "tblProduct_Addon_Variation";
                        dmnSearch.WhereNdGroupBy = "where Status=1 and Pavid= " + Convert.ToInt32(dgvmenu.Rows[e.RowIndex].Cells[iPavid].Value);
                        dmnSearch.OrderBy = "";
                        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSource.Count > 0)
                        {
                            newprice = Convert.ToDecimal(dgvmenu.Rows[rowno].Cells[imprice].Value) - Convert.ToDecimal(gridSource[0].Field1);
                            dgvmenu.Rows[rowno].Cells[imprice].Value = newprice;
                            dgvmenu.Rows[rowno].Cells[iAddoncount].Value = addoncount;
                            totalcellamount(rowno);
                        }
                        #endregion

                        #region Update Grid on New Avid and Remove addon item
                        int dgvcount = dgvmenu.RowCount - 1;
                        for (int i = dgvcount; i >= 0; i--)
                        {
                            if (Convert.ToString(dgvmenu.Rows[i].Cells[iAvid].Value) == remove_avid)
                            {
                                if (Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddonstatus].Value) == Convert.ToInt32(Product_Addon_Variation_Type.Variation))
                                {
                                    dgvmenu.Rows[i].Cells[iAvid].Value = newpart;
                                }
                                else if (Convert.ToInt32(dgvmenu.Rows[i].Cells[iAddonstatus].Value) == Convert.ToInt32(Product_Addon_Variation_Type.Addon) && Convert.ToInt32(dgvmenu.Rows[i].Cells[iPavid].Value) == remove_pavid && Convert.ToString(dgvmenu.Rows[i].Cells[iAvid].Value) == remove_avid)
                                    dgvmenu.Rows.RemoveAt(i);
                                else
                                    dgvmenu.Rows[i].Cells[iAvid].Value = newpart;
                            }
                        }
                        #endregion

                        #endregion
                    }

                    SumMenu();
                    GrandTotal();
                    CalNetAmt();
                    GridSlno_Refresh();
                    Recallmenu();

                    /*if (dgvmenu.Controls.OfType<VScrollBar>().First().Visible == false && tlp_reg_design.Visible == false)
                    {
                        tlp_reg_design.Visible = true;
                        btn_res_details.BackgroundImage = null;
                        btn_res_details.BackgroundImage = FoodandBeverage.Properties.Resources.res_hide_button;
                    }*/

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("Reservation Page  dgvmenu_CellDoubleClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }

        }

        private void btn_res_details_Click(object sender, EventArgs e)
        {
            try
            {
                if (tlp_reg_design.Visible)
                {
                    tlp_reg_design.Visible = false;
                    btn_res_details.BackgroundImage = null;
                    btn_res_details.BackgroundImage = FoodandBeverage.Properties.Resources.res_show_button;

                    if (tc_rese_menu.SelectedTab == tp_reservation)
                    {
                        RecallTable();
                    }
                    else if (tc_rese_menu.SelectedTab == tp_menu)
                    {
                        Recallmenu();
                    }
                }
                else
                {
                    tlp_reg_design.Visible = true;
                    btn_res_details.BackgroundImage = null;
                    btn_res_details.BackgroundImage = FoodandBeverage.Properties.Resources.res_hide_button;

                    if (tc_rese_menu.SelectedTab == tp_reservation)
                    {
                        RecallTable();
                    }
                    else if (tc_rese_menu.SelectedTab == tp_menu)
                    {
                        Recallmenu();
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page btn_res_details_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }
        }

        private void btn_va_close_Click(object sender, EventArgs e)
        {
            try
            {
                tlp_main.Enabled = true;
                pnl_variation_addon.Visible = false;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page btn_va_close_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void oadesc_Enter(object sender, EventArgs e)
        {
            try
            {
                if (oadesc.Text == PlaceholderText)
                {
                    oadesc.Text = string.Empty;
                    oadesc.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page oadesc_Enter()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void oadesc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(oadesc.Text))
                {
                    oadesc.Text = PlaceholderText;
                    oadesc.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page oadesc_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void oddesc_Enter(object sender, EventArgs e)
        {
            try
            {
                if (oddesc.Text == PlaceholderText)
                {
                    oddesc.Text = string.Empty;
                    oddesc.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page oddesc_Enter()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void oddesc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(oddesc.Text))
                {
                    oddesc.Text = PlaceholderText;
                    oddesc.ForeColor = Color.Gray;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page oddesc_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }


        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text != "")
            {
                ClsGeneral cls = new ClsGeneral();
                if (!cls.CheckEmailId(this, txtEmail.Text))
                {
                    e.Cancel = true;
                }
            }
        }

        public void GridSlno_Refresh()
        {
            try
            {
                int i = 1;
                for (int j = 0; j < dgvmenu.RowCount; j++)
                {
                    if ((Convert.ToInt32(dgvmenu.Rows[j].Cells[iAddonstatus].Value) == 0 || Convert.ToInt32(dgvmenu.Rows[j].Cells[iAddonstatus].Value) == 1))
                    {
                        dgvmenu.Rows[j].Cells[islno].Value = i;
                        i = i + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page GridSlno_Refresh()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }

        }
        #region Grid Flow Functions

        private int GetNextVisibleColumn(DataGridView dataGridView, int currentColumnIndex)
        {
            try
            {
                for (int i = currentColumnIndex + 1; i < dataGridView.ColumnCount; i++)
                {
                    if (dataGridView.Columns[i].Visible)
                    {
                        return i;
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page GetNextVisibleColumn()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

                return -1;
            }
        }

        private int GetNextVisibleRow(DataGridView dataGridView, int currentRowIndex)
        {
            try
            {
                for (int i = currentRowIndex + 1; i < dataGridView.RowCount; i++)
                {
                    if (dataGridView.Rows[i].Visible)
                    {
                        return i;
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page GetNextVisibleRow()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

                return -1;
            }
        }

        private int GetFirstVisibleColumn(DataGridView dataGridView)
        {
            try
            {
                for (int i = 0; i < dataGridView.ColumnCount; i++)
                {
                    if (dataGridView.Columns[i].Visible)
                    {
                        return i;
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page GetFirstVisibleColumn()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

                return -1;
            }
        }

        #endregion


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    if (dgvforms.Focused)
                    {
                        try
                        {
                            int currentRowIndex = dgvforms.CurrentCell.RowIndex;
                            int currentColumnIndex = dgvforms.CurrentCell.ColumnIndex;

                            int nextColumnIndex = GetNextVisibleColumn(dgvforms, currentColumnIndex);

                            if (nextColumnIndex != -1)
                            {
                                dgvforms.CurrentCell = dgvforms[nextColumnIndex, currentRowIndex];
                            }
                            else
                            {
                                int nextRowIndex = GetNextVisibleRow(dgvforms, currentRowIndex);
                                if (nextRowIndex != -1)
                                {
                                    dgvforms.CurrentCell = dgvforms[GetFirstVisibleColumn(dgvforms), nextRowIndex];
                                }
                                else
                                {
                                    discountper.Focus();
                                }
                            }
                        }

                        catch (Exception ex)
                        {
                            CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                            ClsGeneral.LogException("reservation page ProcessCmdKeycase Keys.Tab:if (dgvforms.Focused) ", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

                        }
                    }
                    else if (dgvmenu.Focused)
                    {
                        try
                        {
                            int currentRowIndex = dgvmenu.CurrentCell.RowIndex;
                            int currentColumnIndex = dgvmenu.CurrentCell.ColumnIndex;

                            int nextColumnIndex = GetNextVisibleColumn(dgvmenu, currentColumnIndex);

                            if (nextColumnIndex != -1)
                            {
                                dgvmenu.CurrentCell = dgvmenu[nextColumnIndex, currentRowIndex];
                            }
                            else
                            {
                                int nextRowIndex = GetNextVisibleRow(dgvmenu, currentRowIndex);
                                if (nextRowIndex != -1)
                                {
                                    dgvmenu.CurrentCell = dgvmenu[GetFirstVisibleColumn(dgvmenu), nextRowIndex];
                                }
                                else
                                {
                                    discountper.Focus();
                                }
                            }

                        }

                        catch (Exception ex)
                        {
                            CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                            ClsGeneral.LogException("reservation page ProcessCmdKeycase Keys.Tab:if (dgvmenu.Focused) ", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

                        }
                    }
                    else
                        Movefocus(1);
                    return true;
                case Keys.Enter:
                    if (dgvmenu.Focused)
                    {
                        try
                        {
                            int currentRowIndex = dgvmenu.CurrentCell.RowIndex;
                            int currentColumnIndex = dgvmenu.CurrentCell.ColumnIndex;

                            int nextColumnIndex = GetNextVisibleColumn(dgvmenu, currentColumnIndex);

                            if (nextColumnIndex != -1)
                            {
                                dgvmenu.CurrentCell = dgvmenu[nextColumnIndex, currentRowIndex];
                            }
                            else
                            {
                                int nextRowIndex = GetNextVisibleRow(dgvmenu, currentRowIndex);
                                if (nextRowIndex != -1)
                                {
                                    dgvmenu.CurrentCell = dgvmenu[GetFirstVisibleColumn(dgvmenu), nextRowIndex];
                                }
                                else
                                {
                                    discountper.Focus();
                                }
                            }
                        }

                        catch (Exception ex)
                        {
                            CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                            ClsGeneral.LogException("reservation page ProcessCmdKeycase Keys.Enter:if (dgvmenu.Focused) ", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

                        }
                    }
                    else if (dgvforms.Focused)
                    {
                        try
                        {
                            int currentRowIndex = dgvforms.CurrentCell.RowIndex;
                            int currentColumnIndex = dgvforms.CurrentCell.ColumnIndex;

                            int nextColumnIndex = GetNextVisibleColumn(dgvforms, currentColumnIndex);

                            if (nextColumnIndex != -1)
                            {
                                dgvforms.CurrentCell = dgvforms[nextColumnIndex, currentRowIndex];
                            }
                            else
                            {
                                int nextRowIndex = GetNextVisibleRow(dgvforms, currentRowIndex);
                                if (nextRowIndex != -1)
                                {
                                    dgvforms.CurrentCell = dgvforms[GetFirstVisibleColumn(dgvforms), nextRowIndex];
                                }
                                else
                                {
                                    discountper.Focus();
                                }
                            }
                        }

                        catch (Exception ex)
                        {
                            CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                            ClsGeneral.LogException("reservation page ProcessCmdKeycase Keys.Enter:if (dgvforms.Focused) ", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

                        }
                    }
                    else
                        Movefocus(0);
                    return true;

                case Keys.Escape:
                    if (panelProductPopup.Visible)
                    {
                        try
                        {
                            panelProductPopup.Visible = false;
                            tlp_main.Enabled = true;
                            Recallmenu();
                        }
                        catch (Exception ex)
                        {
                            CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                            ClsGeneral.LogException("reservation page ProcessCmdKeycase Keys.Escape :if (panelProductPopup.Visible) ", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

                        }

                    }
                    else if (pnl_variation_addon.Visible)
                    {
                        tlp_main.Enabled = true;
                        pnl_variation_addon.Visible = false;
                    }
                    else if (pnl_variation_addon.Visible)
                    {
                        try
                        {
                            panelProductPopup.Visible = false;
                            tlp_main.Enabled = true;
                            Recallmenu();
                        }
                        catch (Exception ex)
                        {
                            CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                            ClsGeneral.LogException("reservation page ProcessCmdKeycase Keys.Escape :if (pnl_variation_addon.Visible) ", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

                        }
                    }
                    else
                    {
                        FrmDashboard _instancedash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                        _instancedash.Closeform();
                    }
                    return true;
                case Keys.F8:
                    if (tc_rese_menu.SelectedTab == tp_menu)
                    {
                        Recallmenu();
                        menu(Convert.ToInt32(cboBtype.SelectedValue));
                    }
                    else if (tc_rese_menu.SelectedTab == tp_reservation)
                    {
                        RecallTable();
                        Tablepop(Convert.ToInt32(cboBtype.SelectedValue));
                    }

                    return true;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        public void RecallTable()
        {
            try
            {
                if (dgvforms.Rows.Count == 1)
                {
                    dgvforms.Focus();
                    dgvforms.CurrentCell = dgvforms.Rows[0].Cells[itablename];
                    if (Convert.ToInt32(dgvforms.Rows[0].Cells[itid].Value) == 0 || Convert.ToInt32(dgvforms.Rows[0].Cells[itid].Value) == null)
                    {
                        dgvforms.Rows[0].Cells[itablename].Value = "F8 For Table List";
                        dgvforms.Rows[0].Cells[itablename].Style.ForeColor = Color.Red;

                        DataGridViewImageCell cell1 = (DataGridViewImageCell)dgvforms.Rows[0].Cells[iraction];
                        cell1.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        cell1.Value = null;
                        cell1.Value = Properties.Resources.orderview_blank;
                    }
                    dgvforms.BeginEdit(true);
                }
                else if (dgvforms.Rows.Count > 1)
                {
                    for (int i = 0; i < dgvforms.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dgvforms.Rows[i].Cells[itid].Value) == 0 || Convert.ToInt32(dgvforms.Rows[i].Cells[itid].Value) == null)
                        {
                            DataGridViewImageCell cell = (DataGridViewImageCell)dgvforms.Rows[i].Cells[iraction];
                            cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            cell.Value = null;
                            if ((i + 1) % 2 == 0)
                                cell.Value = Properties.Resources.orderview_blank_2;
                            else
                                cell.Value = Properties.Resources.orderview_blank;

                            dgvforms.Focus();
                            dgvforms.CurrentCell = dgvforms.Rows[i].Cells[itablename];
                            dgvforms.BeginEdit(true);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page Recallmenu() ", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }
        }
        public void Recallmenu()
        {
            try
            {
                if (dgvmenu.Rows.Count == 1)
                {
                    dgvmenu.Focus();
                    dgvmenu.CurrentCell = dgvmenu.Rows[0].Cells[imname];
                    if (Convert.ToInt32(dgvmenu.Rows[0].Cells[imid].Value) == 0 || Convert.ToInt32(dgvmenu.Rows[0].Cells[imid].Value) == null)
                    {
                        dgvmenu.Rows[0].Cells[imname].Value = "F8 For Menu List";
                        dgvmenu.Rows[0].Cells[imname].Style.ForeColor = Color.Red;
                    }
                    dgvmenu.BeginEdit(true);
                }
                else if (dgvmenu.Rows.Count > 1)
                {
                    for (int i = 0; i < dgvmenu.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dgvmenu.Rows[i].Cells[imid].Value) == 0 || Convert.ToInt32(dgvmenu.Rows[i].Cells[imid].Value) == null)
                        {
                            DataGridViewImageCell cell = (DataGridViewImageCell)dgvmenu.Rows[i].Cells[iaction];
                            cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            cell.Value = null;
                            if ((i + 1) % 2 == 0)
                                cell.Value = Properties.Resources.orderview_blank_2;
                            else
                                cell.Value = Properties.Resources.orderview_blank;

                            dgvmenu.Focus();
                            dgvmenu.CurrentCell = dgvmenu.Rows[i].Cells[imname];
                            dgvmenu.BeginEdit(true);
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page Recallmenu() ", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));

            }
        }

        private void mbtnRefresh_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void mbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                string returnValue = string.Empty;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DataTable dtColumnAlign = new DataTable();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,cusName,Address,Mobile,BookingDateTime,Remarks";
                _objSearch.Select = "BookmasterId as Id,cusName as Field1,Address as Field2,Mobile as Field3,BookingDateTime as Field4,Remarks as Field5";
                _objSearch.Table = "tblbookMaster";
                _objSearch.WhereNdGroupBy = " WHERE Status = 1";
                _objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);
                if (gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btn_rev_cancel.Visible = false;
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
                        dmnSearch.WhereNdGroupBy = " Where BranchId = " + BranchId + "   and BookmasterId ='" + returnValue + "'";
                        dmnSearch.OrderBy = "";
                        var gridSource1 = _bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSource1.Count > 0)
                        {
                            if (blinkTimer.Enabled)
                                blinkTimer.Stop();

                             modifiedEndDate_before = gridSource1[0].BookingDateTime.AddMinutes(-Convert.ToDouble(FrmLogin.BookingInterwell));
                             modifiedEndDate_after = gridSource1[0].BookingDateTime.AddMinutes(Convert.ToDouble(FrmLogin.BookingInterwell));//
                             ExpdateNow = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
                             Bookdate = Convert.ToDateTime(gridSource1[0].BookingDateTime.ToString("yyyy-MM-dd HH:mm"));
                             Currentdate = DateTime.Now;

                            #region Fill Textbox and labels
                            btn_rev_cancel.Visible = true;
                            txtadvpa.Enabled = false;
                            label21.Visible = true;
                            lblBAmt_Pay.Visible = true;
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
                            if (gridSource1[0].OtherDedText == "")
                            {
                                oddesc.Text = PlaceholderText;
                                oddesc.ForeColor = Color.Gray;
                            }
                            if (gridSource1[0].OtherAddText == "")
                            {
                                oadesc.Text = PlaceholderText;
                                oadesc.ForeColor = Color.Gray;
                            }
                            netamount.Text = Convert.ToString(gridSource1[0].NetAmount);
                            sterms.Text = Convert.ToString(gridSource1[0].Terms);
                            txtadvpa.Text = Convert.ToString(gridSource1[0].AdvancePay);
                            rcvamt.Text = Convert.ToDecimal(gridSource1[0].CashRev).ToString();
                            dtp_from_time.MinDate = gridSource1[0].BookingDateTime;
                            dtp_from_time.Value = gridSource1[0].BookingDateTime;
                            dtpBooking.MinDate = gridSource1[0].BookingDateTime;
                            dtpBooking.Value = gridSource1[0].BookingDateTime;
                            lblNetAmt.Text = Convert.ToString(gridSource1[0].NetAmount);
                            decimal adpayment = Convert.ToDecimal(txtadvpa.Text);
                            decimal bal = Convert.ToDecimal(lblBalAmt.Text);
                            decimal net = Convert.ToDecimal(netamount.Text);
                            decimal balanceneed = 0;
                            decimal cashreciv = Convert.ToDecimal(rcvamt.Text);
                            decimal finalAmt = Convert.ToDecimal(gridSource1[0].FinalAmount);
                            txtfinalamt.Text = Convert.ToString(gridSource1[0].FinalAmount);
                            txt_PaymentTimes.Text = Convert.ToString(gridSource1[0].PaymentTimes);
                            txt_AdvanceAmts_List.Text = Convert.ToString(gridSource1[0].AdvanceAmts_List);
                            txt_CashGivenAmt.Text = Convert.ToString(gridSource1[0].CashGivenAmt);

                            rcvamt.Text = "0";
                            lblBalAmt.Text = "0";

                            balanceneed = net - finalAmt;
                            lblBAmt_Pay.Text = balanceneed.ToString("0.00");
                            txt_pre_balamtpay.Text = Convert.ToString(gridSource1[0].FinalAmount);

                            if (finalAmt >= net)
                            {
                                lblpayment_status.Visible = true;
                                PaymentCFlage = true;
                            }
                            else
                            {
                                PaymentCFlage = false;
                                lblpayment_status.Visible = false;
                            }

                            #endregion

                            #region Grid Details

                            DmnSearch dmnSearch1 = new DmnSearch();
                            BllSearch<DmnReservationDetails> _bllSearch1 = new BllSearch<DmnReservationDetails>();
                            dmnSearch1.Select = "*";
                            dmnSearch1.Table = "tblBookDetails ";
                            dmnSearch1.WhereNdGroupBy = " Where BookmasterId ='" + returnValue + "' and status=1";
                            var gridSource2 = _bllSearch1.GetSearchData("prdSearch", dmnSearch1);
                            if (gridSource2.Count > 0)
                            {
                                dgvforms.Rows.Clear();
                                dgvmenu.Rows.Clear();
                                int r = 0;
                                int r1 = 0;
                                int add_slno = 0;
                                for (int i = 0; i < gridSource2.Count; i++)
                                {
                                    #region Table Grid

                                    if (gridSource2[i].Rid == Convert.ToInt32(TableMenu.Rid))
                                    {
                                        dgvforms.Rows.Add(1);
                                        DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgvforms.Rows[r].Cells[iaction];
                                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                        cellcolumntype.Value = Properties.Resources.food_cancel_icon;

                                        dgvforms.Rows[r].Cells[itid].Value = gridSource2[i].MTID;
                                        dgvforms.Rows[r].Cells[iTBookingDetailId].Value = gridSource2[i].BookdetailId;
                                        dgvforms.Rows[r].Cells[itablename].Value = gridSource2[i].Name;
                                        dgvforms.Rows[r].Cells[itablename].Style.ForeColor = Color.Black;
                                        dgvforms.Rows[r].Cells[iprice].Value = gridSource2[i].Price;
                                        dgvforms.Rows[r].Cells[icapacity].Value = gridSource2[i].Quantity;
                                        dgvforms.Rows[r].Cells[idescription].Value = gridSource2[i].Description;
                                        r++;
                                    }
                                    #endregion

                                    #region Menu Grid
                                    else if (gridSource2[i].Rid == Convert.ToInt32(TableMenu.Mid))
                                    {
                                        dgvmenu.Rows.Add(1);
                                        if (Convert.ToInt32(gridSource2[i].AddonStatus) == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                                        {
                                            #region Addon Grid Fill                                  

                                            DataGridViewImageCell cellcolumntype_addon = (DataGridViewImageCell)dgvmenu.Rows[r1].Cells[iaction];
                                            cellcolumntype_addon.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                            cellcolumntype_addon.Value = Properties.Resources.orderview_addon_removeicon;

                                            dgvmenu.Rows[r1].Cells[islno].Value = add_slno + 1;
                                            dgvmenu.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                            dgvmenu.Rows[r1].Cells[imid].Value = gridSource2[i].Pavid;
                                            dgvmenu.Rows[r1].Cells[imBookingDetailId].Value = gridSource2[i].BookdetailId;
                                            dgvmenu.Rows[r1].Cells[imname].Value = gridSource2[i].Name;
                                            dgvmenu.Rows[r1].Cells[imprice].Value = gridSource2[i].Price;
                                            dgvmenu.Rows[r1].Cells[iqty].Value = gridSource2[i].Quantity;
                                            dgvmenu.Rows[r1].Cells[isamount].Value = gridSource2[i].Net;
                                            dgvmenu.Rows[r1].Cells[iitemcode].Value = gridSource2[i].ItemBarCode;
                                            dgvmenu.Rows[r1].Cells[iproductType].Value = gridSource2[i].Fcid;
                                            dgvmenu.Rows[r1].Cells[iprid].Value = gridSource2[i].PrId;

                                            if (gridSource2[i].TableType == Convert.ToInt32(Tabletype.Product))
                                            {
                                                dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.Product);
                                                dgvmenu.Rows[r1].Cells[iFormType].Value = Convert.ToInt32(formsenum.Menu);
                                            }
                                            else if (gridSource2[i].TableType == Convert.ToInt32(Tabletype.DirectSale))
                                            {
                                                dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.DirectSale);
                                                dgvmenu.Rows[r1].Cells[iFormType].Value = Convert.ToInt32(formsenum.Item);
                                            }
                                            else if (gridSource2[i].TableType == Convert.ToInt32(Tabletype.ComboProduct))
                                            {
                                                dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.ComboProduct);
                                                dgvmenu.Rows[r1].Cells[iFormType].Value = Convert.ToInt32(formsenum.Discount_And_Offer);
                                            }

                                            dgvmenu.Rows[r1].DefaultCellStyle.BackColor = Color.OliveDrab;
                                            dgvmenu.Rows[r1].Cells[iAvid].Value = gridSource2[i].AV_id;
                                            dgvmenu.Rows[r1].Cells[iAddoncount].Value = 0;
                                            dgvmenu.Rows[r1].Cells[iAddonstatus].Value = Convert.ToInt32(Product_Addon_Variation_Type.Addon);
                                            dgvmenu.Rows[r1].Cells[iAvid].Value = gridSource2[i].AV_id;
                                            dgvmenu.Rows[r1].Cells[iPavid].Value = gridSource2[i].Pavid;
                                            dgvmenu.Rows[r1].Cells[iaddoncode].Value = gridSource2[i].CCode;
                                            dgvmenu.Rows[r1].Cells[imarketprice].Value = gridSource2[i].MarketPrice;
                                            add_slno = add_slno + 1;
                                            r1++;
                                            #endregion
                                        }
                                        else
                                        {
                                            #region Product without addon or product with variation 
                                            add_slno = 0;
                                            DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgvmenu.Rows[r1].Cells[iaction];
                                            cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                            cellcolumntype.Value = Properties.Resources.food_cancel_icon;

                                            dgvmenu.Rows[r1].Cells[islno].Value = r1 + 1;
                                            dgvmenu.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                            dgvmenu.Rows[r1].Cells[imname].Value = gridSource2[i].Name;
                                            dgvmenu.Rows[r1].Cells[imBookingDetailId].Value = gridSource2[i].BookdetailId;
                                            dgvmenu.Rows[r1].Cells[imname].Style.ForeColor = Color.Black;
                                            dgvmenu.Rows[r1].Cells[imprice].Value = gridSource2[i].Price;
                                            dgvmenu.Rows[r1].Cells[iqty].Value = gridSource2[i].Quantity;
                                            dgvmenu.Rows[r1].Cells[imdescription].Value = gridSource2[i].Description;
                                            dgvmenu.Rows[r1].Cells[imid].Value = gridSource2[i].MTID;

                                            if (gridSource2[i].TableType == Convert.ToInt32(Tabletype.Product))
                                            {
                                                dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.Product);
                                                dgvmenu.Rows[r1].Cells[iFormType].Value = Convert.ToInt32(formsenum.Menu);
                                            }
                                            else if (gridSource2[i].TableType == Convert.ToInt32(Tabletype.DirectSale))
                                            {
                                                dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.DirectSale);
                                                dgvmenu.Rows[r1].Cells[iFormType].Value = Convert.ToInt32(formsenum.Item);
                                            }
                                            else if (gridSource2[i].TableType == Convert.ToInt32(Tabletype.ComboProduct))
                                            {
                                                dgvmenu.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.ComboProduct);
                                                dgvmenu.Rows[r1].Cells[iFormType].Value = Convert.ToInt32(formsenum.Discount_And_Offer);
                                            }

                                            dgvmenu.Rows[r1].Cells[iTax].Value = gridSource2[i].Tax;
                                            dgvmenu.Rows[r1].Cells[iTaxType].Value = gridSource2[i].TaxType;
                                            dgvmenu.Rows[r1].Cells[icgst].Value = gridSource2[i].CgstPrcnt;
                                            dgvmenu.Rows[r1].Cells[ivat].Value = gridSource2[i].VatPrcnt;
                                            dgvmenu.Rows[r1].Cells[imgst].Value = gridSource2[i].IgstPrcnt;
                                            dgvmenu.Rows[r1].Cells[isgst].Value = gridSource2[i].SgstPrcnt;
                                            dgvmenu.Rows[r1].Cells[imrp].Value = gridSource2[i].MRP;
                                            dgvmenu.Rows[r1].Cells[isamount].Value = gridSource2[i].Net;
                                            dgvmenu.Rows[r1].Cells[iTaxper].Value = gridSource2[i].TaxPer;
                                            dgvmenu.Rows[r1].Cells[istaxamt].Value = gridSource2[i].TaxAmt;
                                            dgvmenu.Rows[r1].Cells[itaxsettingid].Value = gridSource2[i].TaxSettingsId;

                                            dgvmenu.Rows[r1].Cells[iunit].Value = gridSource2[i].unit;
                                            dgvmenu.Rows[r1].Cells[iitemcode].Value = gridSource2[i].ItemBarCode;
                                            dgvmenu.Rows[r1].Cells[iprid].Value = gridSource2[i].PrId;
                                            dgvmenu.Rows[r1].Cells[itaxsettingid].Value = gridSource2[i].TaxSettingsId;

                                            dgvmenu.Rows[r1].Cells[ivid].Value = gridSource2[i].vid;
                                            dgvmenu.Rows[r1].Cells[iVarationName].Value = gridSource2[i].VariatiionName;
                                            dgvmenu.Rows[r1].Cells[iproductType].Value = gridSource2[i].Fcid;
                                            dgvmenu.Rows[r1].Cells[iAvid].Value = gridSource2[i].AV_id;
                                            dgvmenu.Rows[r1].Cells[iAddonstatus].Value = gridSource2[i].AddonStatus;
                                            dgvmenu.Rows[r1].Cells[iAddoncount].Value = gridSource2[i].Addoncount;

                                            r1++;
                                            #endregion
                                        }
                                    }
                                    #endregion
                                }
                                GridSlno_Refresh();

                                tlp_reg_design.Visible = false;
                                btn_res_details.BackgroundImage = null;
                                btn_res_details.BackgroundImage = FoodandBeverage.Properties.Resources.res_show_button;

                            }

                            #endregion

                            if (Currentdate >= modifiedEndDate_before && Currentdate <= Bookdate)//Check  before booking interval time current date equal
                            {                                 
                                tlp_textboxes.Enabled = false;
                                tlp_reg_form.Enabled = false;
                                mbtnSave.Visible = false;
                                Expireflag = true;
                                blinkTimer.Start();
                                lbl_infoExpire.Text = "The reservation time period has started.";
                                lbl_infoExpire.Visible = true;
                            }
                            else if (ExpdateNow > Bookdate)//  expired  .AddMinutes(FrmLogin.BookingInterwell))
                            {
                                tlp_textboxes.Enabled = false;
                                tlp_reg_form.Enabled = false;
                                mbtnSave.Visible = false;
                                Expireflag = true;
                                blinkTimer.Start();
                                lbl_infoExpire.Visible = true;
                                lbl_infoExpire.Text = "This reservation has expired!";
                            }                            
                            else
                            {
                                Expireflag = false;
                                tlp_textboxes.Enabled = true;
                                tlp_reg_form.Enabled = true;
                                mbtnSave.Visible = true;

                                dgvforms.Rows.Add(1);
                                dgvmenu.Rows.Add(1);

                                if (tc_rese_menu.SelectedTab == tp_reservation)
                                {
                                    RecallTable();
                                }
                                else if (tc_rese_menu.SelectedTab == tp_menu)
                                {
                                    Recallmenu();
                                }
                            }

                        }
                        textb_shape();
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page mbtnList_Click_1", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }
        public void Resrvation_period()
        {
            try
            {
                if (Currentdate >= modifiedEndDate_before && Currentdate <= Bookdate && BookmasterId>0)//Check  before booking interval time current date equal
                {
                    tlp_textboxes.Enabled = false;
                    tlp_reg_form.Enabled = false;
                    mbtnSave.Visible = false;
                    Expireflag = true;
                    blinkTimer.Start();
                    lbl_infoExpire.Text = "The reservation time period has started.";
                    lbl_infoExpire.Visible = true;
                }
                else if (ExpdateNow > Bookdate && BookmasterId > 0)//  expired  .AddMinutes(FrmLogin.BookingInterwell))
                {
                    tlp_textboxes.Enabled = false;
                    tlp_reg_form.Enabled = false;
                    mbtnSave.Visible = false;
                    Expireflag = true;
                    blinkTimer.Start();
                    lbl_infoExpire.Visible = true;
                    lbl_infoExpire.Text = "This reservation has expired!";
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page mbtnList_Click_1", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void dgvforms_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == iraction && Convert.ToInt32(dgvforms.Rows[e.RowIndex].Cells[itid].Value) > 0 && Expireflag == false)
                {
                    #region Product & product With addon (complete Remove )
                    var itemName = dgvforms.Rows[e.RowIndex].Cells[itablename].Value.ToString();
                    var result = CMessageBox.Show($"Are you sure you want to remove {itemName} ? ", "Confirm Action", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        dgvforms.Rows.RemoveAt(dgvforms.CurrentCell.RowIndex);
                        if (dgvforms.Rows.Count == 0)
                        {
                            dgvforms.Rows.Add(1);
                        }
                    }
                    #endregion

                    sumTable();
                    GrandTotal();
                    CalNetAmt();
                    GridSlno_Refresh();
                    RecallTable();

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("Reservation Page  dgvmenu_CellDoubleClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void blinkTimer_Tick(object sender, EventArgs e)
        {
            lbl_infoExpire.Visible = !lbl_infoExpire.Visible;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                txtNam.Text = "John Doe";
                txtMob.Text = "8922115230";
                txtEmail.Text = "johndoe@gmail.com";
                txtNOP.Text = "2";
                txtAddress.Text = "123 Main St, Anytown, USA";
                txtRemarks.Text = "No remarks.";
                cboGender.SelectedIndex = 2;
                cboBtype.SelectedIndex = 2;
                DateTime currentTime = DateTime.Now;
                dtp_from_time.Value = currentTime.AddMinutes(15);
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("Reservation Page  button1_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }

        }

        private void txtadvpa_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtfinalamt.Text))
                    txtfinalamt.Text = "0";
                if (Convert.ToInt32(txtBookmasterId.Text) == 0)
                {
                    if (txtadvpa.Text != "" && txtadvpa.Text != "." && txtadvpa.Text != "0.00")
                    {
                        decimal advancePayment = Convert.ToDecimal(txtadvpa.Text);

                        decimal balamt_pay = 0, balamt = 0;
                        balamt_pay = Convert.ToDecimal(lblNetAmt.Text) - Convert.ToDecimal(txtadvpa.Text);
                        if (rcvamt.Text != "" && rcvamt.Text != ".")
                        {
                            decimal receivedAmount = Convert.ToDecimal(rcvamt.Text);
                            if (receivedAmount == 0)
                                lblBalAmt.Text = "0";
                            else
                            {
                                balamt = advancePayment - receivedAmount;
                                lblBalAmt.Text = balamt.ToString("N2");
                            }
                        }

                        lblBAmt_Pay.Visible = true;
                        label21.Visible = true;
                        lblBAmt_Pay.Text = balamt_pay.ToString("N2");
                        textb_shape();
                    }
                    else
                    {
                        lblBAmt_Pay.Visible = false;
                        label21.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("Reservation Page  txtadvpa_TextChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        private void netamount_Enter(object sender, EventArgs e)
        {
            try
            {
                GrandTotal();
                CalNetAmt();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("reservation page netamount_Enter()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }
        }

        public void Movefocus(int keytype)
        {
            try
            {
                if (txtNam.Focused)
                {
                    txtMob.Focus();
                }
                else if (txtMob.Focused)
                {
                    txtEmail.Focus();
                }
                else if (txtEmail.Focused)
                {
                    cboGender.Focus();
                }
                else if (cboGender.Focused)
                {
                    txtNOP.Focus();
                }
                else if (txtNOP.Focused)
                {
                    dtpBooking.Focus();
                }
                else if (dtpBooking.Focused)
                {
                    dtp_from_time.Focus();
                }
                else if (dtp_from_time.Focused)
                {
                    cboBtype.Focus();
                }
                else if (cboBtype.Focused)
                {
                    txtAddress.Focus();
                }
                else if (txtAddress.Focused)
                {
                    txtRemarks.Focus();
                }
                else if (txtRemarks.Focused)
                {
                    if (keytype == 1)
                        btn_res_details.Focus();
                    else if (keytype == 0)
                        btn_res_details.PerformClick();
                }
                else if (btn_res_details.Focused)
                {
                    if (keytype == 1)
                    {
                        if (tc_rese_menu.SelectedTab == tp_reservation)
                            RecallTable();
                        else
                            Recallmenu();
                    }
                    else if (keytype == 0)
                        btn_res_details.PerformClick();
                }
                else if (dgvforms.Focused)
                {
                    int currentRowIndex = dgvforms.CurrentCell.RowIndex;
                    int currentColumnIndex = itablename;
                    if (currentColumnIndex == itablename && currentRowIndex == dgvforms.RowCount - 1)
                        mbtnSave.Focus();
                }
                else if (dgvmenu.Focused)
                {
                    int currentRowIndex = dgvmenu.CurrentCell.RowIndex;
                    int currentColumnIndex = iqty;
                    if (currentColumnIndex == iqty && currentRowIndex == dgvmenu.RowCount - 1)
                        discountper.Focus();
                }
                else if (discountper.Focused)
                {
                    totaldiscountamt.Focus();
                }
                else if (totaldiscountamt.Focused)
                {
                    oaamount.Focus();
                }
                else if (oaamount.Focused)
                {
                    oadesc.Focus();
                }
                else if (oadesc.Focused)
                {
                    odamt.Focus();
                }
                else if (odamt.Focused)
                {
                    oddesc.Focus();
                }
                else if (oddesc.Focused)
                {
                    sterms.Focus();
                }
                else if (sterms.Focused)
                {
                    netamount.Focus();
                }
                else if (netamount.Focused)
                {
                    txtadvpa.Focus();
                }
                else if (txtadvpa.Focused)
                {
                    rcvamt.Focus();
                }
                else if (rcvamt.Focused)
                {
                    mbtnSave.Focus();
                }
                else if (keytype == 1)
                {
                    if (mbtnSave.Focused)
                        mbtnRefresh.Focus();
                    else if (mbtnRefresh.Focused)
                        mbtnList.Focus();
                    else if (mbtnList.Focused)
                        mbtnClose.Focus();
                    else if (mbtnClose.Focused)
                        btn_rev_cancel.Focus();
                    else if (btn_rev_cancel.Focused)
                        txtNam.Focus();
                }
                else if (keytype == 0)
                {
                    if (mbtnSave.Focused)
                        mbtnSave.PerformClick();
                    else if (mbtnRefresh.Focused)
                        mbtnRefresh.PerformClick();
                    else if (mbtnList.Focused)
                        mbtnList.PerformClick();
                    else if (mbtnClose.Focused)
                        mbtnClose.PerformClick();
                    else if (btn_rev_cancel.Focused)
                        btn_rev_cancel.PerformClick();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                ClsGeneral.LogException("Reservation Page  Movefocus()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
            }


        }

    }
}
