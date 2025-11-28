using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CustomMessageBox;
using DAL;
using Domain;
using FastMember;
using FoodandBeverage.Classes;
using FoodandBeverage.Interfaces;
using Microsoft.AspNet.SignalR.Client;

namespace FoodandBeverage.Forms
{
    public partial class FrmKitchenView : Form
    {
        private SoundPlayer soundPlayer;
        public FrmKitchenView()
        {
            InitializeComponent();
            soundPlayer = new SoundPlayer(FoodandBeverage.Properties.Resources.kitchen_order_sound);
            soundPlayer.Load();
        }

        #region Declaration Grid
        public int dslno = 0;
        public int dactionno = 1;
        public int dptypeno = 2;
        public int ditemno = 3;
        public int dqtyno = 4;
        public int dnotes = 5;
        public int dremnoteno = 6;
        public int ddetailsidno = 7;
        public int dkoidno = 8;
        public int dtidno = 9;
        public int dordermasteridno = 10;
        public int ddatetimeno = 11;
        public int dstatusno = 12;
        public int ditemidno = 13;
        public int dtabletypeno = 14;
        public int dorderno = 15;
        public int dAddonStatusno = 16;
        public int dAVIDno = 17;

        public int addslno = 0;
        public int additemnameno = 1;
        public int addqtyno = 2;
        public int addordermasteridno = 3;
        public int addkoidno = 4;
        public int addorderdetailidno = 5;
        public int addactionno = 6;
        public int addavidno = 7;
        public int addpavidno = 8;
        public int addaddonstatusno = 9;
        public int addonpriceno = 10;
        public int addonorderno = 11;
        public int addccodeno = 12;
        #endregion


        Color boxcolor = Color.FromArgb(0x42, 0x67, 0xB2); //System.Drawing.ColorTranslator.FromHtml("#3AAFA9"); //
        public int dgv_view_rowno = 0;
        public string dgv_view_name = "";
        public List<datagrid> orderbox = new List<datagrid>();
        public bool pbcancelfoodclciked = false;
        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _hubProxy;
        public int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        public string RPTPath = Convert.ToString(FrmDashboard.KitchenBillPath);
        TableLayoutPanel tlp_order_col = new TableLayoutPanel();
        bool addingControls = false;
        public int KitchenDisplayCount = 0;
        ClsGeneral clsgen = new ClsGeneral();
        public int ReprintOrderNo = 0;
        public int ReprintId = 0;

        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);

        private void btncolse_Click(object sender, EventArgs e)
        {
            refreshtimer.Stop();
            this.Close();
            this.Dispose();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void PlaySound()
        {
            try
            {
                soundPlayer.Play();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View Page PlaySound()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtorderid_search, txttablecode, txt_product_remove_des, txt_product_remove_des })
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
        public void InitTimer()
        {
            refreshtimer.Start();
        }
        public void FillOrdersAsync()
        {
            try
            {
                GC.Collect();

                #region Fill Orders in Datagrid
                txt_product_remove_des.Text = "Reason type here .....";
                int countpendingorder = 0;

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                BllKitchenData<DmnGridFields> SearchDetails = new BllKitchenData<DmnGridFields>();
                DmnGridFields dmnSearchKitchendata = new DmnGridFields();
                var gridSourcemaster = SearchDetails.serachreport("prdKitchenData", dmnSearchKitchendata);

                if (gridSourcemaster.Count > 0)
                {
                    //pnl_empty_msg.Visible = false;
                    //flp_order_view.Visible = false;
                    var distinctOrders = gridSourcemaster
                       .GroupBy(order => new { order.Field1, order.Field2 })
                       .Select(group => group.First())
                       .OrderBy(order => order.Field1)
                       .ThenBy(order => order.Field2)
                       .ToList();

                    btntotalorders.Text = Convert.ToString(distinctOrders.Count);

                    countpendingorder = distinctOrders.Count(order => Convert.ToInt32(order.Field3) == 1);
                    btnprintcount.Text = Convert.ToString(countpendingorder);
           
                    var preserveNames = distinctOrders.Select(orders => "tlp_product_box_k_" + orders.Field1 + orders.Field2);
                    int l = 0;
                    foreach (Control control in flp_order_view.Controls.Cast<Control>().ToArray())
                    {
                        if (control is TableLayoutPanel tlp && !preserveNames.Contains(tlp.Name))
                        {
                            flp_order_view.Controls.Remove(control);
                            control.Dispose();
                        }
                        l = l + 1;
                    }
                    int rowCountDisplay = 0;
                    //flp_order_view.Controls.Clear();
                    foreach (var orders in distinctOrders)
                    {
                        bool flagstart_ready = false;

                        BllkitchenDataDetails<Dmnkitchendetails> KtDetails = new BllkitchenDataDetails<Dmnkitchendetails>();
                        Dmnkitchendetails dmnSearchtabledata = new Dmnkitchendetails();
                        dmnSearchtabledata.OrderNo =Convert.ToInt32(orders.Field2);
                        dmnSearchtabledata.OrderMasterId = Convert.ToInt32(orders.Field1);
                        var gridSourcedetails = KtDetails.serachData("prdKitchendetails", dmnSearchtabledata);
                        if (gridSourcedetails.Count > 0)
                        {
                            string tlpname = "tlp_product_box_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                            TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                            if (tlp_sele != null)
                            {
                                flp_order_view.SuspendLayout();
                                string dgvname = "dgv_product_details_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                string startbuttonname = "pb_start_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                string readybuttonname = "pb_ready_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                string cancelfoodbuttonname = "pictureBoxRemove_k_" + orders.Field1 + gridSourcedetails[0].Field17;

                                PictureBox btn_ready = this.Controls.Find(readybuttonname, true).FirstOrDefault() as PictureBox;
                                PictureBox btn_start = this.Controls.Find(startbuttonname, true).FirstOrDefault() as PictureBox;
                                PictureBox btn_cancelfood = this.Controls.Find(cancelfoodbuttonname, true).FirstOrDefault() as PictureBox;
                                DataGridView dgv_product_details = this.Controls.Find(dgvname, true).FirstOrDefault() as DataGridView;

                                #region Grid Edit Cancel  product

                                for (int k = 0; k < gridSourcedetails.Count; k++)
                                {
                                    if (Convert.ToInt32(gridSourcedetails[k].Field12) == 1)
                                        flagstart_ready = true;
                                    else if (Convert.ToInt32(gridSourcedetails[k].Field12) == 2)
                                        flagstart_ready = false;

                                    dgv_product_details.Rows[k].Cells[dactionno].Value = Properties.Resources.close_price_change;
                                    dgv_product_details.Rows[k].Cells[dAVIDno].Value = gridSourcedetails[k].Field20;
                                    dgv_product_details.Rows[k].Cells[dstatusno].Value = gridSourcedetails[k].Field12;

                                    DataGridViewImageCell cell = (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dactionno];
                                    if (Convert.ToInt32(gridSourcedetails[k].Field12) == 0)
                                    {
                                        cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                        cell.Value = Properties.Resources.orderview_cancelled;
                                        dgv_product_details.Rows[k].ReadOnly = true;
                                        dgv_product_details.Rows[k].Cells[dremnoteno].Value = gridSourcedetails[k].Field11;
                                    }
                                    else
                                    {
                                        cell.Value = Properties.Resources.kitchenfood_cancel_icon;
                                        cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                    }

                                    DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dptypeno];
                                    cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                    cellcolumntype.ToolTipText = gridSourcedetails[k].Field4;
                                    if (Convert.ToInt32(gridSourcedetails[k].Field13) == Convert.ToInt32(Tabletype.ComboProduct))
                                    {
                                        cellcolumntype.Value = Properties.Resources.kitchengridcomboicon;
                                    }
                                    else if (Convert.ToInt32(gridSourcedetails[k].Field23) >= 1)
                                    {
                                        cellcolumntype.Value = Properties.Resources.addonbutton;
                                        cellcolumntype.ToolTipText = "Addon [clcik view more...]";
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(k) % 2 == 0)
                                            cellcolumntype.Value = Properties.Resources.orderview_blank;
                                        else
                                            cellcolumntype.Value = Properties.Resources.orderview_blank_2;               
                                    }

                                }
                                #endregion

                                #region Edit Ready and Close Button 
                                if (flagstart_ready)
                                {
                                    btn_start.Visible = true;
                                    btn_cancelfood.Visible = true;
                                    btn_ready.Visible = false;
                                }
                                else
                                {
                                    if (btn_start != null)
                                        btn_start.Visible = false;
                                    btn_ready.Visible = true;
                                    btn_cancelfood.Visible = false;
                                }
                                #endregion

                                flp_order_view.ResumeLayout();
                            }
                            else
                            {
                                if (rowCountDisplay >= KitchenDisplayCount)
                                    break;

                                flp_order_view.SuspendLayout();
                                refreshtimer.Stop();

                                #region Main TableLayoutPanel 
                                // Main TableLayoutPanel for each product
                                TableLayoutPanel tlp_product_box = new TableLayoutPanel();
                                tlp_product_box.Name = "tlp_product_box_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                tlp_product_box.Dock = DockStyle.Top;

                                tlp_product_box.Visible = false;
                                tlp_product_box.SuspendLayout();

                                if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                                {
                                    FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                                    if (_instanceFrmdash.ChkWritePermission(Convert.ToInt32(formsenum.Kitchen_View)))
                                        tlp_product_box.Enabled = true;
                                    else
                                        tlp_product_box.Enabled = false;
                                }

                                int WidthPercentage = 30;
                                int heightPercentage = 40;
                                int controlWidth = (int)(this.Width * WidthPercentage / 100);
                                int controlHeight = (int)(this.Height * heightPercentage / 100);
                                tlp_product_box.Size = new Size(controlWidth, controlHeight);

                                tlp_product_box.BackColor = boxcolor;
                                tlp_product_box.RowCount = 4;
                                //tlp_product_box.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                                tlp_product_box.Margin = new Padding(20, 10, 0, 0);
                                tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                tlp_product_box.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // 50% height
                                tlp_product_box.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
                                tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                //ApplyRoundedCorners(tlp_product_box);
                                #endregion

                                #region Heading Time and Tablecode Frist Row
                                // Sub TableLayoutPanel for Heading
                                TableLayoutPanel tlp_product_heading = new TableLayoutPanel();
                                tlp_product_heading.Name = "tlp_product_heading_k_" + orders.Field1 + gridSourcedetails[0].Field17; ;
                                tlp_product_heading.Dock = DockStyle.Top;
                                if (screenWidth > 1367 && screenHeight > 769)
                                {
                                    tlp_product_heading.Height = 50;
                                }
                                else
                                {
                                    tlp_product_heading.Height = 20;
                                }

                                tlp_product_heading.ColumnCount = 3;
                                tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));//33.33F
                                tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                                tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                                #region Table Name 
                                Label lbl_tablecode = new Label()
                                {

                                    Name = "lbl_tablecod_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Dock = DockStyle.Fill,
                                    ForeColor = Color.White,
                                    AutoSize = true,
                                    TextAlign = ContentAlignment.MiddleLeft,

                                };                              
                                #endregion

                                if (Convert.ToInt32(gridSourcedetails[0].Field25) > 0)
                                {
                                    lbl_tablecode.Text = "Pick Up  " + gridSourcedetails[0].Field25;
                                    lbl_tablecode.Font = new Font("Arial", 12, FontStyle.Bold);
                                }
                                else
                                {
                                    lbl_tablecode.Text = gridSourcedetails[0].Field2;

                                    if (screenWidth > 1367 && screenHeight > 769)
                                        lbl_tablecode.Font = new Font("Arial", 15, FontStyle.Bold);
                                    else
                                        lbl_tablecode.Font = new Font("Arial", 10, FontStyle.Bold);
                    
                                }
                                #region Token No

                                Label lbl_tokkenno = new Label()
                                {
                                    Text = "Token No: " + gridSourcedetails[0].Field22,
                                    Name = "lbl_tokkenno_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Dock = DockStyle.Fill,
                                    ForeColor = Color.White,
                                    AutoSize = true,
                                    TextAlign = ContentAlignment.MiddleCenter,
                                };

                                if (screenWidth > 1367 && screenHeight > 769)
                                    lbl_tokkenno.Font = new Font("Arial", 15, FontStyle.Bold);
                                else
                                    lbl_tokkenno.Font = new Font("Arial", 12, FontStyle.Bold);

                                #endregion

                                #region Time Duration
                                DateTime startTime = gridSourcedetails[0].Fielddatetime;
                                DateTime currentTime = DateTime.Now;
                                TimeSpan timeDifference = currentTime - startTime;
                                int hoursDifference = timeDifference.Hours;
                                int minutesDifference = timeDifference.Minutes;
                                int secondsDifference = timeDifference.Seconds;
                                string timeDifferenceString = $"{hoursDifference} h, {minutesDifference} min {secondsDifference}";//,  seconds

                                Label lbl_time = new Label()
                                {
                                    Text = timeDifferenceString,
                                    Name = "lbl_time_K_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Dock = DockStyle.Fill,
                                    ForeColor = Color.White,
                                    AutoSize = true,
                                    TextAlign = ContentAlignment.MiddleRight,

                                };

                                if (screenWidth > 1367 && screenHeight > 769)
                                    lbl_time.Font = new Font("Arial", 11, FontStyle.Bold);
                                else
                                    lbl_time.Font = new Font("Arial", 8, FontStyle.Bold);
                                #endregion

                                tlp_product_heading.Controls.Add(lbl_tablecode, 0, 0);
                                tlp_product_heading.Controls.Add(lbl_tokkenno, 1, 0);
                                tlp_product_heading.Controls.Add(lbl_time, 2, 0);

                                // Adding Heading TableLayoutPanel to the first row
                                tlp_product_box.Controls.Add(tlp_product_heading, 0, 0);
                                #endregion

                                #region DataGridView Second Row
                                bool comboflag = false, addonflag = false;

                                #region dgv_product_details Declaration
                                // DataGridView for product details
                                DataGridView dgv_product_details = new DataGridView();
                                dgv_product_details.CellClick += dgv_product_details_CellClick;
                                dgv_product_details.AllowUserToDeleteRows = true;
                                dgv_product_details.ColumnHeadersDefaultCellStyle.Font = new Font(dgv_product_details.Font, FontStyle.Bold);
                                dgv_product_details.ColumnHeadersDefaultCellStyle.Font = new Font(dgv_product_details.ColumnHeadersDefaultCellStyle.Font.FontFamily, 12, FontStyle.Bold);
                                // Prevent the user from adding new rows
                                dgv_product_details.AllowUserToAddRows = false;
                                dgv_product_details.Name = "dgv_product_details_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                dgv_product_details.Tag = orders.Field1 + gridSourcedetails[0].Field17;
                                // Enable double buffering for the DataGridView
                                dgv_product_details.DoubleBuffered(true);


                                dgv_product_details.BorderStyle = BorderStyle.None;
                                dgv_product_details.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                                dgv_product_details.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                                dgv_product_details.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                                dgv_product_details.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                                dgv_product_details.BackgroundColor = Color.White;

                                dgv_product_details.EnableHeadersVisualStyles = false;
                                dgv_product_details.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                                dgv_product_details.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                                dgv_product_details.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                                dgv_product_details.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                                dgv_product_details.AllowUserToResizeRows = false;

                                if (screenWidth > 1367 && screenHeight > 769)
                                {
                                    dgv_product_details.ColumnHeadersHeight = 70;
                                    dgv_product_details.RowTemplate.Height = 70;
                                }
                                else
                                {
                                    dgv_product_details.ColumnHeadersHeight = 30;
                                    dgv_product_details.RowTemplate.Height = 30;
                                }

                                dgv_product_details.RowHeadersVisible = false;
                                // Add columns with specific cell type
                                DataGridViewTextBoxColumn columnSlno = new DataGridViewTextBoxColumn();
                                DataGridViewImageColumn pbremove = new DataGridViewImageColumn();
                                DataGridViewImageColumn columnptype = new DataGridViewImageColumn();
                                DataGridViewTextBoxColumn columnItemName = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columnQty = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columnNotes = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columnRemNotes = new DataGridViewTextBoxColumn();

                                DataGridViewTextBoxColumn columnorderdetailsid = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columnkoid = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columntid = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columnmasterid = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columndate = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columnstatus = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columnitemid = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columntabletype = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columnorderno = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columnAddonStatusno = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columnAVIDno = new DataGridViewTextBoxColumn();

                                dgv_product_details.Columns.Add(columnSlno);
                                dgv_product_details.Columns.Add(pbremove);
                                dgv_product_details.Columns.Add(columnptype);
                                dgv_product_details.Columns.Add(columnItemName);
                                dgv_product_details.Columns.Add(columnQty);
                                dgv_product_details.Columns.Add(columnNotes);
                                dgv_product_details.Columns.Add(columnRemNotes);
                                dgv_product_details.Columns.Add(columnorderdetailsid);
                                dgv_product_details.Columns.Add(columnkoid);
                                dgv_product_details.Columns.Add(columntid);
                                dgv_product_details.Columns.Add(columnmasterid);
                                dgv_product_details.Columns.Add(columndate);
                                dgv_product_details.Columns.Add(columnstatus);
                                dgv_product_details.Columns.Add(columnitemid);
                                dgv_product_details.Columns.Add(columntabletype);
                                dgv_product_details.Columns.Add(columnorderno);
                                dgv_product_details.Columns.Add(columnAddonStatusno);
                                dgv_product_details.Columns.Add(columnAVIDno);

                                dgv_product_details.Columns[dslno].HeaderText = "Slno";
                                dgv_product_details.Columns[ditemno].HeaderText = "Item Name";
                                dgv_product_details.Columns[dptypeno].HeaderText = "PType";
                                dgv_product_details.Columns[dqtyno].HeaderText = "Qty";
                                dgv_product_details.Columns[dnotes].HeaderText = "Notes";
                                dgv_product_details.Columns[dremnoteno].HeaderText = "Remove Notes";
                                dgv_product_details.Columns[ddetailsidno].HeaderText = "orderdetailsid";
                                dgv_product_details.Columns[dkoidno].HeaderText = "koid";
                                dgv_product_details.Columns[dtidno].HeaderText = "tid";
                                dgv_product_details.Columns[dactionno].HeaderText = "Action";
                                dgv_product_details.Columns[dordermasteridno].HeaderText = "Ordermasterid";
                                dgv_product_details.Columns[ddatetimeno].HeaderText = "datetime";
                                dgv_product_details.Columns[dstatusno].HeaderText = "status";
                                dgv_product_details.Columns[ditemidno].HeaderText = "itemid";
                                dgv_product_details.Columns[dtabletypeno].HeaderText = "tabletype";
                                dgv_product_details.Columns[dorderno].HeaderText = "orderno";
                                dgv_product_details.Columns[dAddonStatusno].HeaderText = "AddonStatus";
                                dgv_product_details.Columns[dAVIDno].HeaderText = "AVID";

                                dgv_product_details.Columns[dslno].Visible = false;
                                dgv_product_details.Columns[ddetailsidno].Visible = false;
                                dgv_product_details.Columns[dkoidno].Visible = false;
                                dgv_product_details.Columns[dtidno].Visible = false;
                                dgv_product_details.Columns[dordermasteridno].Visible = false;
                                dgv_product_details.Columns[ddatetimeno].Visible = false;
                                dgv_product_details.Columns[dstatusno].Visible = false;
                                dgv_product_details.Columns[ditemidno].Visible = false;
                                dgv_product_details.Columns[dtabletypeno].Visible = false;
                                dgv_product_details.Columns[dorderno].Visible = false;
                                dgv_product_details.Columns[dAddonStatusno].Visible = false;
                                dgv_product_details.Columns[dAVIDno].Visible = false;

                                Font boldFont = new Font(dgv_product_details.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Bold);
                                dgv_product_details.Columns[ditemno].DefaultCellStyle.Font = boldFont;
                                dgv_product_details.Columns[dqtyno].DefaultCellStyle.Font = boldFont;
                                dgv_product_details.Columns[dnotes].DefaultCellStyle.Font = boldFont;
                                dgv_product_details.Columns[dremnoteno].DefaultCellStyle.Font = boldFont;
                                #endregion

                                dgv_product_details.Rows.Clear();
                                for (int k = 0; k < gridSourcedetails.Count; k++)
                                {
                                    if (Convert.ToInt32(gridSourcedetails[k].Field12) == 1)//check ready or not
                                    {
                                        flagstart_ready = true;
                                    }

                                    //#region Update Prnt Staus Set to2
                                    //BllKitchenorder bllkitchen = new BllKitchenorder();
                                    //Dmnkitchen dmnkitchen = new Dmnkitchen();
                                    //dmnkitchen.Koid = Convert.ToInt32(gridSourcedetails[k].Field8);
                                    //dmnkitchen.CreatedBy = FrmDashboard.UserId;
                                    //dmnkitchen.KOTPrintStatus = 2;
                                    //dmnkitchen.BranchId = 1030;
                                    //var InsertStatus = bllkitchen.Kitchen_statusupdate("prd_updateKitchenorderstatus", dmnkitchen);
                                    //#endregion

                                    dgv_product_details.Rows.Add();
                                    dgv_product_details.Rows[k].Cells[dslno].Value = k + 1;
                                    int AddonCount = Convert.ToInt32(gridSourcedetails[k].Field23);
                                    string vrianame = Convert.ToString(gridSourcedetails[k].Field24);
                                    string itemname = Convert.ToString(gridSourcedetails[k].Field4);

                                    //if (AddonCount > 0 && vrianame != null && vrianame != "")
                                    //    dgv_product_details.Rows[k].Cells[ditemno].Value = itemname + "(" + vrianame + ")";
                                    //else if (AddonCount > 0)
                                    //    dgv_product_details.Rows[k].Cells[ditemno].Value = itemname;
                                    if (vrianame != null && vrianame != "")
                                        dgv_product_details.Rows[k].Cells[ditemno].Value = itemname + "(" + vrianame + ")";
                                    else
                                        dgv_product_details.Rows[k].Cells[ditemno].Value = itemname;


                                    DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dptypeno];
                                    if (Convert.ToInt32(gridSourcedetails[k].Field13) == Convert.ToInt32(Tabletype.ComboProduct))
                                    {
                                        comboflag = true;
                                        cellcolumntype.Value = Properties.Resources.kitchengridcomboicon;
                                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                        cellcolumntype.ToolTipText = gridSourcedetails[k].Field4;
                                    }
                                    else if (Convert.ToInt32(gridSourcedetails[k].Field23) >= 1)
                                    {
                                        addonflag = true;
                                        cellcolumntype.Value = Properties.Resources.addonbutton;
                                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                        cellcolumntype.ToolTipText = "Addon [clcik view more...]";
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(k) % 2 == 0)
                                            cellcolumntype.Value = Properties.Resources.orderview_blank;
                                        else
                                            cellcolumntype.Value = Properties.Resources.orderview_blank_2;

                                        //cellcolumntype.Value = Properties.Resources.grid_select;
                                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                        cellcolumntype.ToolTipText = gridSourcedetails[k].Field4;
                                    }

                                    decimal originalValue = Convert.ToDecimal(gridSourcedetails[k].Field5);
                                    dgv_product_details.Rows[k].Cells[dqtyno].Value = (int)Math.Floor(originalValue);

                                    dgv_product_details.Rows[k].Cells[dnotes].Value = gridSourcedetails[k].Field6;
                                    dgv_product_details.Rows[k].Cells[dremnoteno].Value = gridSourcedetails[k].Field11;
                                    dgv_product_details.Rows[k].Cells[ddetailsidno].Value = gridSourcedetails[k].Field9;
                                    dgv_product_details.Rows[k].Cells[dkoidno].Value = gridSourcedetails[k].Field8;
                                    dgv_product_details.Rows[k].Cells[dtidno].Value = gridSourcedetails[k].Field10;
                                    dgv_product_details.Rows[k].Cells[dactionno].Value = Properties.Resources.close_price_change;
                                    dgv_product_details.Rows[k].Cells[dordermasteridno].Value = gridSourcedetails[k].Field1;
                                    dgv_product_details.Rows[k].Cells[ddatetimeno].Value = gridSourcedetails[k].Fielddatetime;
                                    dgv_product_details.Rows[k].Cells[dstatusno].Value = gridSourcedetails[k].Field12;
                                    dgv_product_details.Rows[k].Cells[ditemidno].Value = gridSourcedetails[k].Field14;
                                    dgv_product_details.Rows[k].Cells[dtabletypeno].Value = gridSourcedetails[k].Field15;
                                    dgv_product_details.Rows[k].Cells[dorderno].Value = gridSourcedetails[k].Field17;

                                    DataGridViewImageCell cell = (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dactionno];
                                    if (Convert.ToInt32(gridSourcedetails[k].Field12) == 0)
                                    {
                                        cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                        cell.Value = Properties.Resources.orderview_cancelled;
                                        dgv_product_details.Rows[k].ReadOnly = true;
                                    }
                                    else
                                    {
                                        cell.Value = Properties.Resources.kitchenfood_cancel_icon;
                                        cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                    }

                                    if (Convert.ToInt32(gridSourcedetails[k].Field19) == Convert.ToInt32(AddonType.AddonType))
                                    {
                                        dgv_product_details.Rows[k].Cells[dAddonStatusno].Value = Convert.ToInt32(AddonType.AddonType);
                                        dgv_product_details.Rows[k].Cells[dAVIDno].Value = gridSourcedetails[k].Field20;
                                    }
                                    else
                                        dgv_product_details.Rows[k].Cells[dAddonStatusno].Value = Convert.ToInt32(AddonType.None);
                                }

                                for (int x = 0; x < dgv_product_details.RowCount; x++)
                                {
                                    //if (x != dactionno)
                                    dgv_product_details.Rows[x].ReadOnly = true;
                                }
                                if (addonflag != true && comboflag != true)
                                    dgv_product_details.Columns[dptypeno].Visible = false;

                                //#region Datagridview column Sizemode Set
                                //dgv_product_details.Columns[dslno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //dgv_product_details.Columns[ditemno].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                //dgv_product_details.Columns[dptypeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //dgv_product_details.Columns[dqtyno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //dgv_product_details.Columns[dnotes].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //dgv_product_details.Columns[dremnoteno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //dgv_product_details.Columns[ddetailsidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //dgv_product_details.Columns[dkoidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //dgv_product_details.Columns[dtidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //dgv_product_details.Columns[dactionno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //dgv_product_details.Columns[dordermasteridno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //dgv_product_details.Columns[ddatetimeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //dgv_product_details.Columns[dstatusno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //dgv_product_details.Columns[ditemidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //dgv_product_details.Columns[dtabletypeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //dgv_product_details.Columns[dorderno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //dgv_product_details.Columns[dAddonStatusno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //dgv_product_details.Columns[dAVIDno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                //#endregion

                                // Adding DataGridView to the second row
                                tlp_product_box.Controls.Add(dgv_product_details, 0, 1);
                                dgv_product_details.Dock = DockStyle.Fill;

                                clsgen.GridAutoSizeSmart(dgv_product_details);
                                #endregion

                                #region Description
                                Panel pnldesc = new Panel();
                                pnldesc.Name = "pnl_Description_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                pnldesc.AutoScroll = true;
                                pnldesc.Dock = DockStyle.Fill;

                                //pnldesc.Padding = new Padding(8, 8, 8, 8);
                                //Label lbl_Description = new Label()
                                //{
                                //    Text = "Note :   " + gridSourcedetails[0].Field16,
                                //    Name = "lbl_Description_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                //    Dock = DockStyle.Fill,
                                //    ForeColor = Color.White,
                                //    AutoSize = true,
                                //    BackColor = Color.Transparent,
                                //};
                                TextBox txt_Description = new TextBox()
                                {
                                    Text = "Note :   " + gridSourcedetails[0].Field16,
                                    Name = "txt_Description_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Dock = DockStyle.Fill,
                                    ForeColor = Color.White,
                                    BackColor = boxcolor,
                                    ReadOnly = true,
                                    Multiline = true,
                                    ScrollBars = ScrollBars.Vertical,
                                    BorderStyle = BorderStyle.None,
                                    Visible = false,
                                };

                                pnldesc.Controls.Add(txt_Description);
                                if (gridSourcedetails[0].Field16 != null && gridSourcedetails[0].Field16 != "")
                                {
                                    tlp_product_box.Controls.Add(pnldesc, 0, 2);
                                    txt_Description.Visible = true;
                                }
                                else
                                {
                                    txt_Description.Visible = false;
                                    tlp_product_box.RowStyles[2] = new RowStyle(SizeType.AutoSize);
                                }

                                #endregion

                                #region Ready and Close Button,Reprint buttons Thrid Row
                                // Sub TableLayoutPanel for Buttons
                                TableLayoutPanel tlp_buttons = new TableLayoutPanel();
                                tlp_buttons.Name = "tlp_buttons_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                tlp_buttons.Dock = DockStyle.Top;

                                if (screenWidth > 1367 && screenHeight > 769)
                                {
                                    tlp_buttons.Height = 45;
                                }
                                else
                                {
                                    tlp_buttons.Height = 35;
                                }

                                tlp_buttons.ColumnCount = 4;
                                tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                                tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                                tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                                tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                                #region start Button
                                PictureBox pb_start = new PictureBox()
                                {
                                    Name = "pb_start_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Image = Properties.Resources.start,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Dock = DockStyle.None,
                                    Tag = gridSourcedetails[0].Field17,
                                    AccessibleDescription = orders.Field1,
                                };
                                if (screenWidth > 1367 && screenHeight > 769)
                                {
                                    pb_start.Height = 40;
                                    pb_start.Width = 120;
                                }
                                else
                                {
                                    pb_start.Height = 30;
                                    pb_start.Width = 80;
                                }
                                pb_start.Click += pbstart_Click;
                                #endregion

                                #region Ready Button
                                PictureBox pb_ready = new PictureBox()
                                {
                                    Name = "pb_ready_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Image = Properties.Resources.ready_button,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Dock = DockStyle.None,
                                    Tag = gridSourcedetails[0].Field17,
                                    AccessibleDescription = orders.Field1,

                                };
                                if (screenWidth > 1367 && screenHeight > 769)
                                {
                                    pb_ready.Height = 40;
                                    pb_ready.Width = 120;
                                }
                                else
                                {
                                    pb_ready.Height = 30;
                                    pb_ready.Width = 80;
                                }
                                pb_ready.Click += pbreadyfood_Click;
                                #endregion

                                #region Cancel Button
                                PictureBox pb_cancelfood = new PictureBox()
                                {
                                    Name = "pictureBoxRemove_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Image = Properties.Resources.Kitchen_Ordercancel,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Dock = DockStyle.None,
                                    Tag = gridSourcedetails[0].Field17,
                                    AccessibleDescription = orders.Field1,
                                };
                                if (screenWidth > 1367 && screenHeight > 769)
                                {
                                    pb_cancelfood.Height = 40;
                                    pb_cancelfood.Width = 120;
                                }
                                else
                                {
                                    pb_cancelfood.Height = 30;
                                    pb_cancelfood.Width = 80;
                                }
                                pb_cancelfood.Click += pbcancelfood_Click;
                                #endregion

                                #region Reprint Button
                                PictureBox reprintbutton = new PictureBox()
                                {
                                    Name = "reprintbutton_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Image = Properties.Resources.reprint,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Dock = DockStyle.None,
                                    Tag = gridSourcedetails[0].Field17,
                                    AccessibleDescription = orders.Field1,
                                };


                                if (screenWidth > 1367 && screenHeight > 769)
                                {
                                    reprintbutton.Height = 40;
                                    reprintbutton.Width = 120;
                                }
                                else
                                {
                                    reprintbutton.Height = 30;
                                    reprintbutton.Width = 80;
                                }
                                reprintbutton.Click += reprintbutton_Click;
                                #endregion

                                if (flagstart_ready)
                                {
                                    pb_start.Visible = true;
                                    pb_cancelfood.Visible = true;
                                    pb_ready.Visible = false;
                                }
                                else
                                {
                                    pb_start.Visible = false;
                                    pb_ready.Visible = true;
                                    pb_cancelfood.Visible = false;
                                }

                                tlp_buttons.Controls.Add(pb_start, 0, 0);
                                tlp_buttons.Controls.Add(pb_ready, 1, 0);
                                tlp_buttons.Controls.Add(pb_cancelfood, 2, 0);
                                tlp_buttons.Controls.Add(reprintbutton, 3, 0);

                                // Adding Buttons TableLayoutPanel to the last row
                                tlp_product_box.Controls.Add(tlp_buttons, 0, 3);
                                #endregion

                                flp_order_view.Controls.Add(tlp_product_box);
                                orderbox.Add(new datagrid
                                {
                                    Id = Convert.ToInt32(gridSourcedetails[0].Field8),
                                    datagridviewname = dgv_product_details.Name,
                                    createtime = gridSourcedetails[0].Fielddatetime
                                });

                                tlp_product_box.ResumeLayout();
                                tlp_product_box.Visible = true;
                                flp_order_view.ResumeLayout();
                            }
                            rowCountDisplay++;
                        }
                    }

                    timer1.Start();
                    refreshtimer.Start();
                }
                else
                {
                    btntotalorders.Text = "0";
                    timer1.Stop();
                    flp_order_view.Controls.Clear();
                    pnl_empty_msg.Location = new Point(this.ClientSize.Width / 2 - pnl_empty_msg.Size.Width / 2, this.ClientSize.Height / 2 - pnl_empty_msg.Size.Height / 2);
                }
                #endregion
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View Page FillOrdersAsync()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        private void PrintOrderAsync()
        {
            ReportDocument reportDocument = new ReportDocument();
            try
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Start();
                        refreshtimer.Stop();
                    }));
                }

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                BllKitchenData<DmnGridFields> SearchDetails = new BllKitchenData<DmnGridFields>();
                DmnGridFields dmnSearchKitchendata = new DmnGridFields();
                var gridSourcemaster = SearchDetails.serachreport("prdPrintOrder", dmnSearchKitchendata);

                if (gridSourcemaster == null || gridSourcemaster.Count == 0)
                {
                    return;
                }

                var distinctOrders = gridSourcemaster
                    .GroupBy(order => new { order.Field1, order.Field2 })
                    .Select(group => group.First())
                    .OrderBy(order => order.Field1)
                    .ThenBy(order => order.Field2)
                    .ToList();

                int countpendingorder = distinctOrders.Count(order => Convert.ToInt32(order.Field3) == 1);
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        btnprintcount.Text = Convert.ToString(countpendingorder);
                    }));
                }

                foreach (var orders in distinctOrders)
                {
                    #region Crystal Report Declaration
                    DataSet dsReport = new DataSet();
                    DataTable dtSalesDetail = new DataTable("DT_kitchenbill");
                    BllSearch<DmnkitchenBilldetails> bllSearchDetails = new BllSearch<DmnkitchenBilldetails>();
                    DmnSearch dmnSearchDetails = new DmnSearch();

                    //dmnSearchDetails.Select = "od.itemName ItemName, od.Quantity Quantity, od.Notes Notes, od.MainRemarks Description, ti.TableCode TableCode, om.OrderMasterId OrderMasterId, od.AddonStatus AddonStatus, od.AV_id AV_id, od.TableType TableType,'' offeritemname, 0 offerqty, od.PrId PrId, ko.Kitchennumber,od.VariatiionName VariatiionName" +
                    //" from tblkitchenOrder ko " +
                    //    " Left Join tblOrderDetails od on od.OrderDetailsId=ko.OrderDetailsId " +
                    //    " Left Join tblOrderMaster om on om.OrderMasterId=ko.OrderMasterId " +
                    //    " Left Join tblTable_info ti on ti.Tid=ko.Tid " +
                    //    $" where om.status=1 and ko.OrderMasterId={orders.Field1} and ko.OrderNo={orders.Field2}" + " and od.TableType!=" + Convert.ToInt32(Tabletype.ComboProduct)+
                    //" union select od.itemName ItemName, od.Quantity Quantity, od.Notes Notes, od.MainRemarks Description, ti.TableCode TableCode, om.OrderMasterId OrderMasterId, od.AddonStatus AddonStatus, od.AV_id AV_id, od.TableType TableType, ofd.Productname offeritemname, ofd.Quantity offerqty, od.PrId PrId, ko.Kitchennumber, od.VariatiionName VariatiionName ";
                    //dmnSearchDetails.Table = " tblkitchenOrder ko" +
                    //                        " Left Join tblOrderDetails od on od.OrderDetailsId = ko.OrderDetailsId" +
                    //                        " Left Join tblOrderMaster om on om.OrderMasterId = ko.OrderMasterId" +
                    //                        " Left Join tblTable_info ti on ti.Tid = ko.Tid" +
                    //                        " Left Join tblOfferMaster ofm on ofm.Omid = od.ItemId" +
                    //                        " Left Join tblOfferDetails ofd on ofd.Omid = ofm.Omid";
                    //dmnSearchDetails.WhereNdGroupBy = $" where om.status=1 and ko.OrderMasterId={orders.Field1} and ko.OrderNo={orders.Field2}" + " and od.TableType=" + Convert.ToInt32(Tabletype.ComboProduct);
                    //dmnSearchDetails.OrderBy = "order by AddonStatus";

                    dmnSearchDetails.Select = "od.itemName ItemName, od.Quantity Quantity, od.Notes Notes, od.MainRemarks Description, ti.TableCode TableCode, om.OrderMasterId OrderMasterId, od.AddonStatus AddonStatus, od.AV_id AV_id, od.TableType TableType, ofd.Productname offeritemname, ofd.Quantity offerqty, od.PrId PrId, ko.Kitchennumber, od.VariatiionName VariatiionName," +
                        "( select sum(Quantity) from  tblOrderDetails  where status in(1,2) and OrderMasterId=" + orders.Field1 + " and OrderNo=" + orders.Field2 + " and AddonStatus in(0,1)) as TotalQTY,om.pkid Pkid";
                    dmnSearchDetails.Table = "tblkitchenOrder ko " +
                        "Left Join tblOrderDetails od  on od.OrderDetailsId=ko.OrderDetailsId " +
                        "Left Join tblOrderMaster om  on om.OrderMasterId=ko.OrderMasterId " +
                        "Left Join tblTable_info  ti on ti.Tid=ko.Tid" +
                        " Left Join tblOfferMaster ofm  on od.ItemId = ofm.Omid and od.TableType = 3" +
                        " Left Join tblOfferDetails ofd  on ofd.Omid = ofm.Omid";
                    dmnSearchDetails.WhereNdGroupBy = "where om.status=1 and ko.OrderMasterId=" + orders.Field1 + " and ko.OrderNo =" + orders.Field2 + "";
                    dmnSearchDetails.OrderBy = "order by od.AddonStatus ";

                    var BillDetails = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetails);

                    if (BillDetails == null || BillDetails.Count == 0)
                    {
                        continue;
                    }

                    var processedData = BillDetails.Select(item => new
                    {
                        ItemName = !string.IsNullOrEmpty(item.ItemName)
                                   ? item.ItemName + (!string.IsNullOrEmpty(item.VariatiionName) ? " (" + item.VariatiionName + ")" : string.Empty)
                                   : item.ItemName,
                        Quantity = item.Quantity,
                        Notes = item.Notes,
                        Description = item.Description,
                        TableCode = item.TableCode,
                        OrderMasterId = item.OrderMasterId,
                        TableType = item.TableType,
                        AddonStatus = item.AddonStatus,
                        offeritemname = item.offeritemname,
                        offerqty = item.offerqty,
                        Kitchennumber = item.Kitchennumber,
                        MainCategory = string.IsNullOrEmpty(item.AV_id) ? $"{item.ItemName}{item.OrderMasterId}{item.PrId}" : item.AV_id,
                        MainCategoryCount = BillDetails.Count(b => b.MainCategory == item.MainCategory),
                        TotalQTY = item.TotalQTY,
                        Pkid = item.Pkid,
                    }).ToList();

                    //if (item.VariatiionNam != null && item.VariatiionNam != "")
                    //    Description = item.Description + "(" + item.VariatiionNam + ")";
                    //else
                    //    Description = itemname;

                    var mainCategoryCounts = processedData
                        .GroupBy(item => item.MainCategory)
                        .Select(group => new
                        {
                            MainCategory = group.Key,
                            MainCategoryCount = group.Count()
                        }).ToList();


                    var mergedData = processedData
                        .Join(mainCategoryCounts,
                              processedItem => processedItem.MainCategory,
                              mainCategoryCount => mainCategoryCount.MainCategory,
                              (processedItem, mainCategoryCount) => new
                              {
                                  MainCategoryCount = mainCategoryCount.MainCategoryCount,
                                  processedItem.MainCategory,
                                  processedItem.TableType,
                                  processedItem.AddonStatus,
                                  processedItem.ItemName,
                                  processedItem.Quantity,
                                  processedItem.Notes,
                                  processedItem.Description,
                                  processedItem.TableCode,
                                  processedItem.OrderMasterId,
                                  processedItem.offeritemname,
                                  processedItem.offerqty,
                                  processedItem.Kitchennumber,
                                  processedItem.TotalQTY,
                                  processedItem.Pkid
                              })
                        .ToList();

                    if (mergedData != null && mergedData.Count > 0)
                    {
                        using (var reader = ObjectReader.Create(mergedData, "ItemName", "Quantity", "Notes", "Description", "TableCode", "OrderMasterId", "MainCategory", "TableType", "AddonStatus", "MainCategoryCount", "offeritemname", "offerqty", "Kitchennumber", "TotalQTY","Pkid"))
                        {
                            dtSalesDetail.Load(reader);
                        }
                        dsReport.Tables.Add(dtSalesDetail);
                    }                   

                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            string reportPath = "";
                            if (Convert.ToInt32(BillDetails[0].Pkid)>0)
                                reportPath = RPTPath + @"\CRkitchenbill_Pickup.rpt";
                            else
                                 reportPath = RPTPath + @"\crkitchenbill.rpt";

                            if (!File.Exists(reportPath))
                            {
                                throw new FileNotFoundException("Report file not found.", reportPath);
                            }
                            reportDocument.Load(reportPath);

                            reportDocument.SetDataSource(dsReport);
                            crvkitchenbill.ReportSource = reportDocument;
                            reportDocument.PrintToPrinter(1, false, 0, 0);
                        }));
                    }
                    #endregion

                    #region Update Print Status to 2
                    BllKitchenorder bllkitchen = new BllKitchenorder();
                    Dmnkitchen dmnkitchen = new Dmnkitchen
                    {
                        OrderMasterId = Convert.ToInt32(orders.Field1),
                        OrderNo = Convert.ToInt32(orders.Field2),
                        CreatedBy = FrmDashboard.UserId,
                        KOTPrintStatus = 2,
                        BranchId = FrmDashboard.BranchId
                    };
                    var InsertStatus = bllkitchen.Kitchen_statusupdate("prd_updateKitchenorderstatus", dmnkitchen);
                    #endregion

                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            PlaySound();
                            int c = Convert.ToInt32(btnprintcount.Text) - 1;
                            if (c < 0)
                                btnprintcount.Text = "0";
                            else
                                btnprintcount.Text = Convert.ToString(c);
                        }));
                    }

                }

                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                        refreshtimer.Start();
                    }));
                }
            }
            catch (Exception ex)
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                        lblprintloading.Text = "Printing Stopped";

                        CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clsgen.LogException("Kitchen_View Page PrintOrderAsync()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
                    }));
                }
            }
            finally
            {
                reportDocument.Close();
                reportDocument.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }


        private void FrmKitchenView_Load(object sender, EventArgs e)
        {
            try
            {
                if (FrmDashboard.AutoLogoutWhenIdle)
                {
                    //AppSession.RegisterForm(this, (int)LogoutFormList.Home);
                }
                dgv_viewmore.BorderStyle = BorderStyle.None;
                dgv_viewmore.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgv_viewmore.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgv_viewmore.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgv_viewmore.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgv_viewmore.BackgroundColor = Color.White;

                dgv_viewmore.EnableHeadersVisualStyles = false;
                dgv_viewmore.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgv_viewmore.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgv_viewmore.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                txtorderid_search.Text = "Order Id ....";
                txttablecode.Text = "Table Code....";
                txt_product_remove_des.Text = "Reason type here .....";
                textb_shape();
                tlp_order_col.Dock = DockStyle.Fill;
                InitTimer();
                //Fillorders();
                KitchenDisplayCount = FrmLogin.KitchenDisplayCount;
                //tlp_main.RowStyles[3] = new RowStyle(SizeType.AutoSize);
                //tlp_main.Controls.Add(btn_viewmore_order,0,3);
                txt_product_remove_des.ForeColor = Color.DarkGray;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View FrmKitchenView_Load", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }


        //public class datagrid
        //{
        //    public int Id { get; set; }
        //    public string datagridviewname { get; set; }
        //    public DateTime createtime { get; set; }

        //}

        //private void ApplyRoundedCorners(TableLayoutPanel panel)
        //{
        //    int cornerRadius = 10; // Adjust the corner radius as needed
        //    // Override the OnPaint event to create rounded corners
        //    panel.Paint += (sender, e) =>
        //    {
        //        GraphicsPath path = new GraphicsPath();
        //        path.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90);
        //        path.AddArc(panel.Width - cornerRadius * 2, 0, cornerRadius * 2, cornerRadius * 2, 270, 90);
        //        path.AddArc(panel.Width - cornerRadius * 2, panel.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
        //        path.AddArc(0, panel.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);

        //        panel.Region = new Region(path);
        //    };
        //}

        private void dgv_product_details_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dactionno && e.RowIndex >= 0)// Remove Prodcut one by one by clciking column 
                {
                    refreshtimer.Stop();
                    DataGridView clickedDataGridView = sender as DataGridView;
                    if (clickedDataGridView != null && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dstatusno].Value) == 1)//  Food processing Not started
                    {
                        tlp_main.Enabled = false;
                        txt_product_remove_des.Text = "Reason type here .....";
                        txt_product_remove_des.ForeColor = Color.LightGray;
                        dgv_view_name = clickedDataGridView.Name;
                        dgv_view_rowno = e.RowIndex;
                        pnl_products_rem_descr.Visible = true;
                        int panelWidthPercentage = 25;
                        int panelheightPercentage = 25;
                        int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                        int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                        pnl_products_rem_descr.Size = new Size(panelWidth, panelHeight);
                        pnl_products_rem_descr.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                        tlp_products_descr_main.Dock = DockStyle.Fill;
                        lblheader_product_des.Text = Convert.ToString(clickedDataGridView.Rows[e.RowIndex].Cells[ditemno].Value);
                        //txt_product_remove_des.Focus();
                    }
                    else if (clickedDataGridView != null && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dstatusno].Value) == 2)//  Food processing started
                    {
                        var result = CMessageBox.Show("Food processing has started. Are you sure you want to cancel this item?", "Confirm The Action", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            tlp_main.Enabled = false;
                            txt_product_remove_des.Text = "Reason type here .....";
                            txt_product_remove_des.ForeColor = Color.LightGray;
                            dgv_view_name = clickedDataGridView.Name;
                            dgv_view_rowno = e.RowIndex;
                            pnl_products_rem_descr.Visible = true;
                            int panelWidthPercentage = 20;
                            int panelheightPercentage = 20;
                            int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                            int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                            pnl_products_rem_descr.Size = new Size(panelWidth, panelHeight);
                            pnl_products_rem_descr.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                            lblheader_product_des.Text = Convert.ToString(clickedDataGridView.Rows[e.RowIndex].Cells[ditemno].Value);
                        }

                    }
                }
                else if (e.ColumnIndex == dptypeno && e.RowIndex >= 0)//Addon or Combo product View more panel show
                {
                    if (pnlproductviewmore.Visible)
                    {
                        tlp_main.Enabled = true;
                        pnlproductviewmore.Visible = false;
                    }
                    else
                    {

                        DataGridView clickedDataGridView = sender as DataGridView;
                        if (clickedDataGridView != null && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dstatusno].Value) == 1 && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dtabletypeno].Value) == Convert.ToInt32(Tabletype.ComboProduct))//cmbo products
                        {
                            lblviewmore.Text = Convert.ToString(clickedDataGridView.Rows[e.RowIndex].Cells[ditemno].Value);
                            int itemid = Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[ditemidno].Value);
                            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                            BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                            DmnSearch dmnSearch = new DmnSearch();
                            dmnSearch.Select = "ofd.Productname Field1,ofd.Quantity Field2";
                            dmnSearch.Table = " tblOfferDetails ofd LEFT JOIN tblOfferMaster ofm on ofm.Omid=ofd.Omid";
                            dmnSearch.WhereNdGroupBy = "where ofm.Status=1 and ofm.Omid = " + itemid;
                            dmnSearch.OrderBy = "";
                            var gridSource_offer = bllSearch.GetSearchData("prdSearch", dmnSearch);
                            if (gridSource_offer.Count > 0)
                            {
                                tlp_main.Enabled = false;
                                dgv_viewmore.Rows.Clear();
                                dgv_viewmore.Columns[addactionno].Visible = false;
                                for (int k = 0; k < gridSource_offer.Count; k++)
                                {
                                    dgv_viewmore.Rows.Add();
                                    dgv_viewmore.Rows[k].Cells[addslno].Value = k + 1;
                                    dgv_viewmore.Rows[k].Cells[additemnameno].Value = gridSource_offer[k].Field1;
                                    dgv_viewmore.Rows[k].Cells[addqtyno].Value = gridSource_offer[k].Field2;
                                }
                                // pnlproductviewmore.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                                pnlproductviewmore.Location = new Point(this.ClientSize.Width / 2 - pnlproductviewmore.Size.Width / 2, this.ClientSize.Height / 2 - pnlproductviewmore.Size.Height / 2);
                                pnlproductviewmore.Visible = true;
                            }

                        }
                        else if (clickedDataGridView != null && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dstatusno].Value) == 1 && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dAddonStatusno].Value) == Convert.ToInt32(AddonType.AddonType))//addon products
                        {
                            lblviewmore.Text = Convert.ToString(clickedDataGridView.Rows[e.RowIndex].Cells[ditemno].Value);
                            string itemid = Convert.ToString(clickedDataGridView.Rows[e.RowIndex].Cells[dAVIDno].Value);
                            int ordermasid = Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dordermasteridno].Value);
                            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                            BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                            DmnSearch dmnSearch = new DmnSearch();
                            dmnSearch.Select = "itemName Field1,Quantity Field2,od.AddonStatus Field3,ko.OrderMasterId Field4,ko.Koid Field5,od.OrderDetailsId Field6,od.AV_id Field7,od.Pavid Field8,pav.Price Field9,od.OrderNo Field10,pav.CCode Field11";
                            dmnSearch.Table = "tblOrderDetails od left join tblkitchenOrder ko on  ko.OrderDetailsId=od.OrderDetailsId " +
                                "left join tblProduct_Addon_Variation pav on  pav.Pavid=od.Pavid";
                            dmnSearch.WhereNdGroupBy = "where od.Status!=0 and od.OrderMasterId= " + ordermasid + " and  od.AV_id = '" + itemid + "'";
                            dmnSearch.OrderBy = "order by od.Pavid";
                            var gridSource_offer = bllSearch.GetSearchData("prdSearch", dmnSearch);
                            if (gridSource_offer.Count > 1)
                            {
                                tlp_main.Enabled = false;
                                dgv_viewmore.Rows.Clear();
                                dgv_viewmore.Columns[addactionno].Visible = true;
                                int c = 0;
                                for (int k = 0; k < gridSource_offer.Count; k++)
                                {
                                    if (Convert.ToInt32(gridSource_offer[k].Field3) == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                                    {
                                        int rowIndex = dgv_viewmore.Rows.Add();
                                        dgv_viewmore.Rows[rowIndex].Cells[addslno].Value = c + 1;
                                        dgv_viewmore.Rows[rowIndex].Cells[additemnameno].Value = gridSource_offer[k].Field1;
                                        dgv_viewmore.Rows[rowIndex].Cells[addqtyno].Value = gridSource_offer[k].Field2;
                                        dgv_viewmore.Rows[rowIndex].Cells[addordermasteridno].Value = gridSource_offer[k].Field4;
                                        dgv_viewmore.Rows[rowIndex].Cells[addkoidno].Value = gridSource_offer[k].Field5;
                                        dgv_viewmore.Rows[rowIndex].Cells[addorderdetailidno].Value = gridSource_offer[k].Field6;
                                        dgv_viewmore.Rows[rowIndex].Cells[addavidno].Value = gridSource_offer[k].Field7;
                                        dgv_viewmore.Rows[rowIndex].Cells[addpavidno].Value = gridSource_offer[k].Field8;
                                        dgv_viewmore.Rows[rowIndex].Cells[addaddonstatusno].Value = gridSource_offer[k].Field3;
                                        dgv_viewmore.Rows[rowIndex].Cells[addonpriceno].Value = gridSource_offer[k].Field9;
                                        dgv_viewmore.Rows[rowIndex].Cells[addonorderno].Value = gridSource_offer[k].Field10;
                                        dgv_viewmore.Rows[rowIndex].Cells[addccodeno].Value = gridSource_offer[k].Field11;
                                        c = c + 1;
                                    }
                                }
                                // pnlproductviewmore.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                                pnlproductviewmore.Location = new Point(this.ClientSize.Width / 2 - pnlproductviewmore.Size.Width / 2, this.ClientSize.Height / 2 - pnlproductviewmore.Size.Height / 2);
                                pnlproductviewmore.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View dgv_product_details_CellClick", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }

        }
        public void ReprintBill()
        {
            ReportDocument reportDocument = new ReportDocument();
            try
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Start();
                        refreshtimer.Stop();
                    }));
                }

                dgv_view_name = "dgv_product_details_k_" + ReprintOrderNo + ReprintId;
                DataGridView dgv_product_details = this.Controls.Find(dgv_view_name, true).FirstOrDefault() as DataGridView;
                if (dgv_product_details != null)
                {
                    int orderno = 0, ordermid = 0;
                    for (int i = 0; i < dgv_product_details.RowCount; i++)
                    {
                        if (Convert.ToInt32(dgv_product_details.Rows[i].Cells[dstatusno].Value) == 1 || Convert.ToInt32(dgv_product_details.Rows[i].Cells[dstatusno].Value) == 2)
                        {
                            orderno = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dorderno].Value);
                            ordermid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dordermasteridno].Value);
                            break;
                        }
                    }
                    #region Crystal Report Dclaration
                    DataSet dsReport = new DataSet();
                    DataTable dtSalesDetail = new DataTable("DT_kitchenbill");
                    BllSearch<DmnkitchenBilldetails> bllSearchDetails = new BllSearch<DmnkitchenBilldetails>();
                    DmnSearch dmnSearchDetails = new DmnSearch();
                    dmnSearchDetails.Select = "od.itemName ItemName, od.Quantity Quantity, od.Notes Notes, od.MainRemarks Description, ti.TableCode TableCode, om.OrderMasterId OrderMasterId, od.AddonStatus AddonStatus, od.AV_id AV_id, od.TableType TableType,'' offeritemname, 0 offerqty, od.PrId PrId, ko.Kitchennumber,od.VariatiionName VariatiionName" +
                    " from tblkitchenOrder ko " +
                        " Left Join tblOrderDetails od on od.OrderDetailsId=ko.OrderDetailsId " +
                        " Left Join tblOrderMaster om on om.OrderMasterId=ko.OrderMasterId " +
                        " Left Join tblTable_info ti on ti.Tid=ko.Tid " +
                        " where om.status=1 and ko.OrderMasterId=" + ordermid + " and ko.OrderNo=" + orderno + " and od.TableType!=" + Convert.ToInt32(Tabletype.ComboProduct) + " and ko.status in(1,2)" +
                    " union select od.itemName ItemName, od.Quantity Quantity, od.Notes Notes, od.MainRemarks Description, ti.TableCode TableCode, om.OrderMasterId OrderMasterId, od.AddonStatus AddonStatus, od.AV_id AV_id, od.TableType TableType, ofd.Productname offeritemname, ofd.Quantity offerqty, od.PrId PrId, ko.Kitchennumber, od.VariatiionName VariatiionName ";
                    dmnSearchDetails.Table = " tblkitchenOrder ko" +
                                            " Left Join tblOrderDetails od on od.OrderDetailsId = ko.OrderDetailsId" +
                                            " Left Join tblOrderMaster om on om.OrderMasterId = ko.OrderMasterId" +
                                            " Left Join tblTable_info ti on ti.Tid = ko.Tid" +
                                            " Left Join tblOfferMaster ofm on ofm.Omid = od.ItemId" +
                                            " Left Join tblOfferDetails ofd on ofd.Omid = ofm.Omid";
                    dmnSearchDetails.WhereNdGroupBy = $" where om.status=1 and ko.OrderMasterId=" + ordermid + " and ko.OrderNo=" + orderno + " and od.TableType=" + Convert.ToInt32(Tabletype.ComboProduct) + " and ko.status in(1,2)";
                    dmnSearchDetails.OrderBy = "order by AddonStatus";

                    var BillDetails = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetails);
                    if (BillDetails.Count > 0)
                    {
                        var processedData = BillDetails.Select(item => new
                        {
                            ItemName = !string.IsNullOrEmpty(item.ItemName)
                                       ? item.ItemName + (!string.IsNullOrEmpty(item.VariatiionName) ? " (" + item.VariatiionName + ")" : string.Empty)
                                       : item.ItemName,
                            Quantity = item.Quantity,
                            Notes = item.Notes,
                            Description = item.Description,
                            TableCode = item.TableCode,
                            OrderMasterId = item.OrderMasterId,
                            TableType = item.TableType,
                            AddonStatus = item.AddonStatus,
                            offeritemname = item.offeritemname,
                            offerqty = item.offerqty,
                            Kitchennumber = item.Kitchennumber,
                            MainCategory = string.IsNullOrEmpty(item.AV_id) ? $"{item.ItemName}{item.OrderMasterId}{item.PrId}" : item.AV_id,
                            MainCategoryCount = BillDetails.Count(b => b.MainCategory == item.MainCategory)
                        }).ToList();

                        var mainCategoryCounts = processedData
                            .GroupBy(item => item.MainCategory)
                            .Select(group => new
                            {
                                MainCategory = group.Key,
                                MainCategoryCount = group.Count()
                            }).ToList();

                        var mergedData = processedData
                            .Join(mainCategoryCounts,
                                  processedItem => processedItem.MainCategory,
                                  mainCategoryCount => mainCategoryCount.MainCategory,
                                  (processedItem, mainCategoryCount) => new
                                  {
                                      MainCategoryCount = mainCategoryCount.MainCategoryCount,
                                      processedItem.MainCategory,
                                      processedItem.TableType,
                                      processedItem.AddonStatus,
                                      processedItem.ItemName,
                                      processedItem.Quantity,
                                      processedItem.Notes,
                                      processedItem.Description,
                                      processedItem.TableCode,
                                      processedItem.OrderMasterId,
                                      processedItem.offeritemname,
                                      processedItem.offerqty,
                                      processedItem.Kitchennumber
                                  })
                            .ToList();

                        if (mergedData != null && mergedData.Count > 0)
                        {
                            using (var reader = ObjectReader.Create(mergedData, "ItemName", "Quantity", "Notes", "Description", "TableCode", "OrderMasterId", "MainCategory", "TableType", "AddonStatus", "MainCategoryCount", "offeritemname", "offerqty", "Kitchennumber"))//, "cobmoitemname"
                            {
                                dtSalesDetail.Load(reader);
                            }
                            dsReport.Tables.Add(dtSalesDetail);
                        }
                        reportDocument.Load(RPTPath + @"\crkitchenbill.rpt");
                        reportDocument.SetDataSource(dsReport);
                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new Action(() =>
                            {
                                crvkitchenbill.ReportSource = reportDocument;
                            }));
                        }
                        //reportDocument.SetParameterValue("TotalQtyMain", mainCategoryCounts.Count);
                        reportDocument.PrintToPrinter(1, false, 0, 0);
                    }
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            refreshtimer.Start();
                        }));
                    }

                }
                #endregion
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                    }));
                }
            }
            catch (Exception ex)
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                        lblprintloading.Text = "Reprinting Stopped";

                        CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clsgen.LogException("Kitchen_View ReprintBill", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
                    }));
                }

            }
            finally
            {
                reportDocument.Close();
                reportDocument.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void reprintbutton_Click(object sender, EventArgs e)
        {
            ReprintOrderNo = 0;
            ReprintId = 0;
            PictureBox clickedPictureBox = (PictureBox)sender;
            ReprintOrderNo = Convert.ToInt32(clickedPictureBox.AccessibleDescription);
            ReprintId = Convert.ToInt32(clickedPictureBox.Tag);
            RunMethodInSeparateThread(ReprintBill);

        }
        private void pbcancelfood_Click(object sender, EventArgs e)
        {
            try
            {
                tlp_main.Enabled = false;
                refreshtimer.Stop();
                PictureBox clickedPictureBox = (PictureBox)sender;
                int id = Convert.ToInt32(clickedPictureBox.Tag);
                dgv_view_name = "dgv_product_details_k_" + clickedPictureBox.AccessibleDescription + id;
                pnl_products_rem_descr.Visible = true;
                int panelWidthPercentage = 20;
                int panelheightPercentage = 20;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                pnl_products_rem_descr.Size = new Size(panelWidth, panelHeight);
                pnl_products_rem_descr.Location = new Point(this.ClientSize.Width / 2 - pnl_products_rem_descr.Size.Width / 2, this.ClientSize.Height / 2 - pnl_products_rem_descr.Size.Height / 2);
                //pnl_products_rem_descr.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                pbcancelfoodclciked = true;
                lblheader_product_des.Text = "Order Cancel";
                //txt_product_remove_des.Focus();
                pnl_products_rem_descr.Focus();

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View pbcancelfood_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }

        }
        private void pbstart_Click(object sender, EventArgs e)
        {
            try
            {
                refreshtimer.Stop();
                PictureBox clickedPictureBox = (PictureBox)sender;
                int id = Convert.ToInt32(clickedPictureBox.Tag);
                string dgvname = "dgv_product_details_k_" + clickedPictureBox.AccessibleDescription + id;
                string startbuttonname = "pb_start_k_" + clickedPictureBox.AccessibleDescription + id;
                string readybuttonname = "pb_ready_k_" + clickedPictureBox.AccessibleDescription + id;
                string tlpname = "tlp_product_box_k_" + clickedPictureBox.AccessibleDescription + id;
                string cancelfoodbuttonname = "pictureBoxRemove_k_" + clickedPictureBox.AccessibleDescription + id;
                DmnOrderMaster dmnRemove_order = new DmnOrderMaster();
                BllOrder bll_order = new BllOrder();
                PictureBox btn_ready = this.Controls.Find(readybuttonname, true).FirstOrDefault() as PictureBox;
                if (btn_ready != null)
                    btn_ready.Visible = true;
                PictureBox btn_start = this.Controls.Find(startbuttonname, true).FirstOrDefault() as PictureBox;
                if (btn_start != null)
                {
                    btn_start.Visible = false;
                    btn_start.Dispose();
                }
                PictureBox btn_cancelfood = this.Controls.Find(cancelfoodbuttonname, true).FirstOrDefault() as PictureBox;

                DataGridView dgv_product_details = this.Controls.Find(dgvname, true).FirstOrDefault() as DataGridView;
                if (dgv_product_details != null)
                {
                    int tid = 0, orderno = 0, ordermid = 0;

                    ordermid = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dordermasteridno].Value);
                    dmnRemove_order.OrderDetailsId = Convert.ToInt32(dgv_product_details.Rows[0].Cells[ddetailsidno].Value);
                    dmnRemove_order.koid = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dkoidno].Value);
                    dmnRemove_order.OrderMasterId = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dordermasteridno].Value);
                    dmnRemove_order.AddonStatus = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dAddonStatusno].Value);
                    dmnRemove_order.AV_id = Convert.ToString(dgv_product_details.Rows[0].Cells[dAVIDno].Value);
                    dmnRemove_order.OrderNo = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dorderno].Value);
                    //dmnRemove_order.Tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                    //decimal originalValue = Convert.ToDecimal(dgv_product_details.Rows[i].Cells[dqtyno].Value);
                    //dmnRemove_order.Quantity = (int)Math.Floor(originalValue);  
                    //tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                    //dmnRemove_order.RemoveNotes = "";
                    dmnRemove_order.CreatedBy = FrmDashboard.UserId;
                    dmnRemove_order.BranchId = FrmDashboard.BranchId;
                    dmnRemove_order.Status = 2;
                    var InsertStatusP = bll_order.Remove_order_KitchenOrder_Start_Rady("prd_KitchenOrder_Updates", dmnRemove_order);
                    if (InsertStatusP.Status == 1)
                    {
                        TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                        if (tlp_sele != null)
                        {
                            bool flagstart_ready = false;
                            #region Grid Edit Cancel  product
                            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                            DmnSearch dmnSearch = new DmnSearch();
                            dmnSearch.Select = "Status Field1,Koid Field2";
                            dmnSearch.Table = " tblkitchenOrder";
                            dmnSearch.WhereNdGroupBy = "where  AddonStatus in(1,0)  and  OrderNo = " + id + " and OrderMasterId = " + ordermid;
                            dmnSearch.OrderBy = "";
                            var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

                            for (int k = 0; k < gridSource.Count; k++)
                            {
                                if (Convert.ToInt32(gridSource[k].Field1) == 1)
                                {
                                    flagstart_ready = true;
                                }

                                dgv_product_details.Rows[k].Cells[dactionno].Value = Properties.Resources.close_price_change;

                                dgv_product_details.Rows[k].Cells[dstatusno].Value = gridSource[k].Field1;


                                for (int j = 0; j < dgv_product_details.RowCount; j++)
                                {
                                    DataGridViewImageCell cell = (DataGridViewImageCell)dgv_product_details.Rows[j].Cells[dactionno];
                                    if (Convert.ToInt32(dgv_product_details.Rows[j].Cells[dkoidno].Value) == Convert.ToInt32(gridSource[k].Field2))
                                    {
                                        if (Convert.ToInt32(gridSource[k].Field1) == 0)
                                        {
                                            cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                            cell.Value = Properties.Resources.orderview_cancelled;
                                            dgv_product_details.Rows[j].ReadOnly = true;
                                        }
                                        else
                                        {
                                            cell.Value = Properties.Resources.close_price_change;
                                            cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                        }
                                    }

                                }
                            }
                            #endregion

                            #region Edit Ready and Close Button 
                            if (flagstart_ready)
                            {
                                btn_start.Visible = true;
                                btn_cancelfood.Visible = true;
                                btn_ready.Visible = false;
                            }
                            else
                            {
                                btn_start.Visible = false;
                                btn_ready.Visible = true;
                                btn_cancelfood.Visible = false;
                            }
                            #endregion
                            refreshtimer.Start();

                        }
                        if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                        {
                            FrmOrderView _instanceFrmOrderView = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                            _instanceFrmOrderView.FillOrders();

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View pbstart_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }

        }

        private void pbreadyfood_Click(object sender, EventArgs e)
        {
            try
            {
                refreshtimer.Stop();
                PictureBox clickedPictureBox = (PictureBox)sender;
                int id = Convert.ToInt32(clickedPictureBox.Tag);
                string dgvname = "dgv_product_details_k_" + clickedPictureBox.AccessibleDescription + id;
                DmnOrderMaster dmnRemove_order = new DmnOrderMaster();
                BllOrder bll_order = new BllOrder();

                DataGridView dgv_product_details = this.Controls.Find(dgvname, true).FirstOrDefault() as DataGridView;
                if (dgv_product_details != null)
                {
                    int tid = 0, koid = 0;

                    //tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                    //dmnRemove_order.Tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                    //decimal originalValue = Convert.ToDecimal(dgv_product_details.Rows[i].Cells[dqtyno].Value);  
                    //dmnRemove_order.Quantity = (int)Math.Floor(originalValue); 
                    //dmnRemove_order.RemoveNotes = "";
                    dmnRemove_order.AddonStatus = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dAddonStatusno].Value);
                    dmnRemove_order.AV_id = Convert.ToString(dgv_product_details.Rows[0].Cells[dAVIDno].Value);
                    dmnRemove_order.OrderDetailsId = Convert.ToInt32(dgv_product_details.Rows[0].Cells[ddetailsidno].Value);
                    dmnRemove_order.koid = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dkoidno].Value);
                    dmnRemove_order.OrderMasterId = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dordermasteridno].Value);
                    dmnRemove_order.OrderNo = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dorderno].Value);
                    dmnRemove_order.CreatedBy = FrmDashboard.UserId;
                    dmnRemove_order.BranchId = FrmDashboard.BranchId;
                    dmnRemove_order.Status = 3;
                    var InsertStatusP = bll_order.Remove_order_KitchenOrder_Start_Rady("prd_KitchenOrder_Updates", dmnRemove_order);

                    if (InsertStatusP.Status == 1)
                    {
                        string dgvName = "dgv_product_details_k_" + clickedPictureBox.AccessibleDescription + id;
                        string startButtonName = "pb_start_k_" + clickedPictureBox.AccessibleDescription + id;
                        string readyButtonName = "pb_ready_k_" + clickedPictureBox.AccessibleDescription + id;
                        string cancelFoodButtonName = "pictureBoxRemove_k_" + clickedPictureBox.AccessibleDescription + id;
                        string lblTimeName = "lbl_time_K_" + clickedPictureBox.AccessibleDescription + id;
                        string lblTableCodeName = "lbl_tablecod_k_" + clickedPictureBox.AccessibleDescription + id;

                        Label lblTime = this.Controls.Find(lblTimeName, true).FirstOrDefault() as Label;
                        Label lblTableCode = this.Controls.Find(lblTableCodeName, true).FirstOrDefault() as Label;
                        PictureBox btnReady = this.Controls.Find(readyButtonName, true).FirstOrDefault() as PictureBox;
                        PictureBox btnStart = this.Controls.Find(startButtonName, true).FirstOrDefault() as PictureBox;
                        PictureBox btnCancelFood = this.Controls.Find(cancelFoodButtonName, true).FirstOrDefault() as PictureBox;
                        if (btnReady != null && btnStart != null && btnCancelFood != null && lblTime != null && lblTableCode != null)
                        {
                            btnReady.Dispose();
                            btnStart.Dispose();
                            btnCancelFood.Dispose();
                            lblTime?.Dispose();
                            lblTableCode?.Dispose();
                        }


                        string tlpname = "tlp_product_box_k_" + clickedPictureBox.AccessibleDescription + id;
                        TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                        if (tlp_sele != null && flp_order_view.Controls.Contains(tlp_sele))
                        {
                            flp_order_view.Controls.Remove(tlp_sele);
                            tlp_sele.Dispose();
                            refreshtimer.Start();
                        }

                        if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                        {
                            FrmOrderView _instanceFrmOrderView = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                            _instanceFrmOrderView.FillOrders();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View pbreadyfood_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }

        }

        private void txt_product_remove_des_Enter(object sender, EventArgs e)
        {
            if (txt_product_remove_des.Text == "Reason type here .....")
            {
                txt_product_remove_des.Text = "";
            }
            txt_product_remove_des.ForeColor = Color.Black;
        }

        private void txt_product_remove_des_Leave(object sender, EventArgs e)
        {
            if (txt_product_remove_des.Text == "")
            {
                txt_product_remove_des.Text = "Reason type here .....";

            }
            txt_product_remove_des.ForeColor = Color.DarkGray;
        }

        private void btn_cancel_product_des_Click(object sender, EventArgs e)
        {
            tlp_main.Enabled = true;
            pnl_products_rem_descr.Visible = false;
            txt_product_remove_des.Text = "Reason type here .....";
            refreshtimer.Start();
        }

        private void btn_close_prod_des_Click(object sender, EventArgs e)
        {
            tlp_main.Enabled = true;
            pnl_products_rem_descr.Visible = false;
            txt_product_remove_des.Text = "Reason type here .....";
            refreshtimer.Start();
        }
        public bool MandatoryCheck()
        {
            //if (string.IsNullOrEmpty(txtordermasterid.Text))
            //    txtordermasterid.Text = "0";

            if (txt_product_remove_des.Text == "Reason type here ....." || txt_product_remove_des.Text == "")
            {
                CMessageBox.Show("Please Enter Reason", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_product_remove_des.Focus();
                return false;
            }

            return true;
        }
        private void btn_rem_product_des_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {
                    DmnOrderMaster dmnRemove_order = new DmnOrderMaster();
                    BllOrder bll_order = new BllOrder();
                    int tid = 0;
                    if (pbcancelfoodclciked)
                    {
                        DataGridView dgv_product_details = this.Controls.Find(dgv_view_name, true).FirstOrDefault() as DataGridView;
                        if (dgv_product_details != null)
                        {
                            int omid = 0, orderno = 0;
                            for (int i = 0; i < dgv_product_details.RowCount; i++)
                            {
                                if (Convert.ToInt32(dgv_product_details.Rows[i].Cells[dstatusno].Value) == 1)
                                {
                                    omid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dordermasteridno].Value);
                                    tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                                    orderno = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dorderno].Value);
                                    dmnRemove_order.OrderDetailsId = Convert.ToInt32(dgv_product_details.Rows[i].Cells[ddetailsidno].Value);
                                    dmnRemove_order.koid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dkoidno].Value);
                                    dmnRemove_order.OrderMasterId = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dordermasteridno].Value);
                                    dmnRemove_order.Tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                                    decimal originalValue = Convert.ToDecimal(dgv_product_details.Rows[i].Cells[dqtyno].Value);

                                    dmnRemove_order.AddonStatus = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dAddonStatusno].Value);
                                    dmnRemove_order.AV_id = Convert.ToString(dgv_product_details.Rows[i].Cells[dAVIDno].Value);

                                    dmnRemove_order.Quantity = (int)Math.Floor(originalValue);
                                    dmnRemove_order.Status = 0;
                                    dmnRemove_order.OrderCancelNotes = txt_product_remove_des.Text;
                                    var InsertStatusP = bll_order.Remove_order_ALL("prd_remove_KitchenOrder", dmnRemove_order);
                                }

                            }
                            tlp_main.Enabled = true;
                            pbcancelfoodclciked = false;
                            pnl_products_rem_descr.Visible = false;
                            txt_product_remove_des.Text = "";

                            string tlpname = "tlp_product_box_k_" + omid + orderno;
                            TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;

                            string dgvName = "dgv_product_details_k_" + omid + orderno;
                            string startButtonName = "pb_start_k_" + omid + orderno;
                            string readyButtonName = "pb_ready_k_" + omid + orderno;
                            string cancelFoodButtonName = "pictureBoxRemove_k_" + omid + orderno;
                            string lblTimeName = "lbl_time_K_" + omid + orderno;
                            string lblTableCodeName = "lbl_tablecod_k_" + omid + orderno;

                            Label lblTime = this.Controls.Find(lblTimeName, true).FirstOrDefault() as Label;
                            Label lblTableCode = this.Controls.Find(lblTableCodeName, true).FirstOrDefault() as Label;
                            PictureBox btnReady = this.Controls.Find(readyButtonName, true).FirstOrDefault() as PictureBox;
                            PictureBox btnStart = this.Controls.Find(startButtonName, true).FirstOrDefault() as PictureBox;
                            PictureBox btnCancelFood = this.Controls.Find(cancelFoodButtonName, true).FirstOrDefault() as PictureBox;
                            if (btnReady != null && btnStart != null && btnCancelFood != null && lblTime != null && lblTableCode != null)
                            {
                                btnReady.Dispose();
                                btnStart.Dispose();
                                btnCancelFood.Dispose();
                                lblTime?.Dispose();
                                lblTableCode?.Dispose();
                            }

                            if (tlp_sele != null && flp_order_view.Controls.Contains(tlp_sele))
                            {
                                flp_order_view.Controls.Remove(tlp_sele);
                                tlp_sele.Dispose();
                                dgv_product_details.Dispose();
                            }
                        }
                        refreshtimer.Start();

                    }
                    else
                    {
                        DataGridView dgv_product_details = this.Controls.Find(dgv_view_name, true).FirstOrDefault() as DataGridView;
                        tid = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dtidno].Value);
                        dmnRemove_order.OrderDetailsId = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[ddetailsidno].Value);
                        dmnRemove_order.koid = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dkoidno].Value);
                        dmnRemove_order.OrderMasterId = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dordermasteridno].Value);
                        dmnRemove_order.Tid = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dtidno].Value);
                        decimal originalValue = Convert.ToDecimal(dgv_product_details.Rows[dgv_view_rowno].Cells[dqtyno].Value);

                        dmnRemove_order.AddonStatus = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dAddonStatusno].Value);
                        dmnRemove_order.AV_id = Convert.ToString(dgv_product_details.Rows[dgv_view_rowno].Cells[dAVIDno].Value);

                        dmnRemove_order.Quantity = (int)Math.Floor(originalValue);
                        dmnRemove_order.Status = 0;
                        dmnRemove_order.RemoveNotes = txt_product_remove_des.Text;
                        var InsertStatusP = bll_order.Remove_order_KitchenOrder_ByProduct("prd_remove_Order", dmnRemove_order);

                        DataGridViewImageCell cell = (DataGridViewImageCell)dgv_product_details.Rows[dgv_view_rowno].Cells[dactionno];
                        cell.Value = Properties.Resources.orderview_cancelled;
                        DataGridViewImageCell cell1 = (DataGridViewImageCell)dgv_product_details.Rows[dgv_view_rowno].Cells[dactionno];
                        dgv_product_details.Rows[dgv_view_rowno].Cells[dremnoteno].Value = txt_product_remove_des.Text;
                        pnl_products_rem_descr.Visible = false;
                        tlp_main.Enabled = true;
                        txt_product_remove_des.Text = "";
                        refreshtimer.Start();
                    }
                    if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                    {
                        FrmOrderView _instanceFrmOrderView = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                        _instanceFrmOrderView.FillOrders();
                    }

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View btn_rem_product_des_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {


                // Assuming orderbox is a member of the class
                foreach (var dataGridViewInfo in orderbox)
                {
                    string dataGridViewName = dataGridViewInfo.datagridviewname;
                    DataGridView dgv = Controls.Find(dataGridViewName, true).FirstOrDefault() as DataGridView;
                    if (dgv != null)
                    {
                        string lbltimename = "lbl_time_K_" + dgv.Tag;
                        DateTime startTime = dataGridViewInfo.createtime;
                        DateTime currentTime = DateTime.Now;
                        TimeSpan timeDifference = currentTime - startTime;

                        int hoursDifference = timeDifference.Hours;
                        int minutesDifference = timeDifference.Minutes;
                        int secondsDifference = timeDifference.Seconds;

                        string timeDifferenceString = $"{hoursDifference} h, {minutesDifference} min, {secondsDifference} s";
                        Label lbl_time = Controls.Find(lbltimename, true).FirstOrDefault() as Label;
                        if (lbl_time != null)
                            lbl_time.Text = timeDifferenceString;
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View timer1_Tick", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }

        }

        private void txtorderid_search_Leave(object sender, EventArgs e)
        {
            if (txtorderid_search.Text == "")
            {
                txtorderid_search.Text = "Order Id ....";
                txtorderid_search.ForeColor = Color.DarkGray;
            }
        }

        private void txtorderid_search_Enter(object sender, EventArgs e)
        {
            if (txtorderid_search.Text == "Order Id ....")
            {
                txtorderid_search.Text = "";
                txtorderid_search.ForeColor = Color.Black;
            }

        }

        private void txtorderid_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If the pressed key is not a digit and not a control key (like backspace), suppress it
                e.Handled = true;
            }
        }

        private void txtorderid_search_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtorderid_search.Text != "")
                    {
                        #region Fill Orders in Datagrid

                        txt_product_remove_des.Text = "Reason type here .....";
                        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                        BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "ko.OrderMasterId Field1,OrderNo Field2";
                        dmnSearch.Table = " tblkitchenOrder ko  LEFT JOIN tblOrderMaster om on om.OrderMasterId=ko.OrderMasterId";
                        dmnSearch.WhereNdGroupBy = "where ko.status IN(1,2) and ko.OrderMasterId= " + txtorderid_search.Text;
                        dmnSearch.OrderBy = "";
                        var gridSourcemaster = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSourcemaster.Count > 0)
                        {
                            pnl_empty_msg.Visible = false;
                            flp_order_view.Visible = false;
                            var distinctOrders = gridSourcemaster
                               .GroupBy(order => new { order.Field1, order.Field2 })
                               .Select(group => group.First())
                               .OrderBy(order => order.Field1)
                               .ThenBy(order => order.Field2)
                               .ToList();

                            flp_order_view.Controls.Clear();
                            TableLayoutPanel tlp_order_col = new TableLayoutPanel();
                            tlp_order_col.Dock = DockStyle.Fill;

                            foreach (var orders in distinctOrders)
                            {
                                bool flagstart_ready = false;
                                dmnSearch.Select = "om.OrderMasterId Field1,ti.TableCode Field2,om.status Field3,od.itemName Field4,od.Quantity Field5,od.Notes Field6,od.OrderNo Field7,od.CreatedDateTime Fielddatetime,ko.koid Field8,ko.OrderDetailsId Field9,om.Tid Field10,ko.RemoveNotes Field11,ko.Status Field12";
                                dmnSearch.Table = "tblkitchenOrder ko " +
                                    "Left Join tblOrderDetails od  on od.OrderDetailsId=ko.OrderDetailsId " +
                                    "Left Join tblOrderMaster om  on om.OrderMasterId=ko.OrderMasterId " +
                                    "Left Join tblTable_info  ti on ti.Tid=om.Tid";
                                dmnSearch.WhereNdGroupBy = "where om.status=1 and ko.OrderMasterId=" + orders.Field1 + " and ko.OrderNo =" + orders.Field2;
                                dmnSearch.OrderBy = "order by ko.koid DESC";
                                var gridSourcedetails = bllSearch_details.GetSearchData("prdSearch", dmnSearch);
                                if (gridSourcedetails.Count > 0)
                                {
                                    string tlpname = "tlp_product_box" + gridSourcedetails[0].Field8;
                                    TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                                    if (tlp_sele != null)
                                    {

                                        string dgvname = "dgv_product_details" + gridSourcedetails[0].Field8;
                                        string startbuttonname = "pb_start" + gridSourcedetails[0].Field8;
                                        string readybuttonname = "pb_ready" + gridSourcedetails[0].Field8;
                                        string cancelfoodbuttonname = "pictureBoxRemove" + gridSourcedetails[0].Field8;

                                        PictureBox btn_ready = this.Controls.Find(readybuttonname, true).FirstOrDefault() as PictureBox;
                                        PictureBox btn_start = this.Controls.Find(startbuttonname, true).FirstOrDefault() as PictureBox;
                                        PictureBox btn_cancelfood = this.Controls.Find(cancelfoodbuttonname, true).FirstOrDefault() as PictureBox;
                                        DataGridView dgv_product_details = this.Controls.Find(dgvname, true).FirstOrDefault() as DataGridView;

                                        #region Grid Edit Cancel  product
                                        for (int k = 0; k < gridSourcedetails.Count; k++)
                                        {
                                            if (Convert.ToInt32(gridSourcedetails[k].Field12) == 1)
                                            {
                                                flagstart_ready = true;
                                            }
                                            dgv_product_details.Rows[k].Cells[dactionno].Value = Properties.Resources.close_price_change;

                                            dgv_product_details.Rows[k].Cells[dstatusno].Value = gridSourcedetails[k].Field12;

                                            DataGridViewImageCell cell = (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dactionno];
                                            if (Convert.ToInt32(gridSourcedetails[k].Field12) == 0)
                                            {
                                                cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                                cell.Value = Properties.Resources.orderview_cancelled;
                                                dgv_product_details.Rows[k].ReadOnly = true;
                                            }
                                            else
                                            {
                                                cell.Value = Properties.Resources.close_price_change;
                                                cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                            }

                                        }
                                        #endregion

                                        #region Edit Ready and Close Button 
                                        if (flagstart_ready)
                                        {
                                            btn_start.Visible = true;
                                            btn_cancelfood.Visible = true;
                                            btn_ready.Visible = false;
                                        }
                                        else
                                        {
                                            btn_start.Visible = false;
                                            btn_ready.Visible = true;
                                            btn_cancelfood.Visible = false;
                                        }
                                        #endregion

                                    }
                                    else
                                    {
                                        #region Main TableLayoutPanel 
                                        // Main TableLayoutPanel for each product
                                        TableLayoutPanel tlp_product_box = new TableLayoutPanel();
                                        tlp_product_box.Name = "tlp_product_box" + gridSourcedetails[0].Field8;
                                        tlp_product_box.Dock = DockStyle.Top;
                                        if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                                        {
                                            FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                                            if (_instanceFrmdash.ChkWritePermission(Convert.ToInt32(formsenum.Kitchen_View)))
                                                tlp_product_box.Enabled = true;
                                            else
                                                tlp_product_box.Enabled = false;
                                        }
                                        int WidthPercentage = 30;
                                        int heightPercentage = 30;
                                        int controlWidth = (int)(this.Width * WidthPercentage / 100);
                                        int controlHeight = (int)(this.Height * heightPercentage / 100);
                                        tlp_product_box.Size = new Size(controlWidth, controlHeight);



                                        tlp_product_box.BackColor = boxcolor;
                                        tlp_product_box.RowCount = 4;
                                        //tlp_product_box.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                                        tlp_product_box.Margin = new Padding(20, 10, 0, 0);
                                        tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                        tlp_product_box.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // 50% height
                                        tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                        tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                        //ApplyRoundedCorners(tlp_product_box);
                                        #endregion

                                        #region Heading Time and Tablecode Frist Row
                                        // Sub TableLayoutPanel for Heading
                                        TableLayoutPanel tlp_product_heading = new TableLayoutPanel();
                                        tlp_product_heading.Name = "tlp_product_heading" + gridSourcedetails[0].Field8;
                                        tlp_product_heading.Dock = DockStyle.Top;
                                        if (screenWidth > 1367 && screenHeight > 769)
                                        {
                                            tlp_product_heading.Height = 50;
                                        }
                                        else
                                        {
                                            tlp_product_heading.Height = 20;
                                        }

                                        tlp_product_heading.ColumnCount = 2;
                                        tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                                        tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                                        Label lbl_tablecode = new Label()
                                        {
                                            Text = gridSourcedetails[0].Field2,
                                            Name = "lbl_tablecode" + gridSourcedetails[0].Field8,
                                            Dock = DockStyle.Fill,
                                            ForeColor = Color.White,

                                        };
                                        if (screenWidth > 1367 && screenHeight > 769)
                                            lbl_tablecode.Font = new Font("Arial", 15, FontStyle.Bold);
                                        else
                                            lbl_tablecode.Font = new Font("Arial", 10, FontStyle.Bold);

                                        DateTime startTime = gridSourcedetails[0].Fielddatetime;
                                        DateTime currentTime = DateTime.Now;
                                        TimeSpan timeDifference = currentTime - startTime;
                                        int hoursDifference = timeDifference.Hours;
                                        int minutesDifference = timeDifference.Minutes;
                                        int secondsDifference = timeDifference.Seconds;
                                        string timeDifferenceString = $"{hoursDifference} hours, {minutesDifference} minutes {secondsDifference}";//,  seconds

                                        Label lbl_time = new Label()
                                        {
                                            Text = timeDifferenceString,
                                            Name = "lbl_time" + gridSourcedetails[0].Field8,
                                            Dock = DockStyle.Fill,
                                            ForeColor = Color.White,

                                        };
                                        if (screenWidth > 1367 && screenHeight > 769)
                                            lbl_time.Font = new Font("Arial", 13, FontStyle.Bold);
                                        else
                                            lbl_time.Font = new Font("Arial", 8, FontStyle.Bold);

                                        tlp_product_heading.Controls.Add(lbl_tablecode, 0, 0);
                                        tlp_product_heading.Controls.Add(lbl_time, 1, 0);

                                        // Adding Heading TableLayoutPanel to the first row
                                        tlp_product_box.Controls.Add(tlp_product_heading, 0, 0);
                                        #endregion

                                        #region DataGridView Second Row
                                        // DataGridView for product details
                                        DataGridView dgv_product_details = new DataGridView();
                                        dgv_product_details.CellClick += dgv_product_details_CellClick;
                                        dgv_product_details.AllowUserToDeleteRows = true;
                                        // Prevent the user from adding new rows
                                        dgv_product_details.AllowUserToAddRows = false;
                                        dgv_product_details.Name = "dgv_product_details_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                        dgv_product_details.Tag = orders.Field1 + gridSourcedetails[0].Field17;
                                        // Enable double buffering for the DataGridView
                                        dgv_product_details.DoubleBuffered(true);


                                        dgv_product_details.BorderStyle = BorderStyle.None;
                                        dgv_product_details.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                                        dgv_product_details.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                                        dgv_product_details.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                                        dgv_product_details.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                                        dgv_product_details.BackgroundColor = Color.White;

                                        dgv_product_details.EnableHeadersVisualStyles = false;
                                        dgv_product_details.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                                        dgv_product_details.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                                        dgv_product_details.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                                        if (screenWidth > 1367 && screenHeight > 769)
                                        {
                                            dgv_product_details.ColumnHeadersHeight = 50;
                                            dgv_product_details.RowTemplate.Height = 50;
                                        }
                                        else
                                        {
                                            dgv_product_details.ColumnHeadersHeight = 30;
                                            dgv_product_details.RowTemplate.Height = 30;
                                        }

                                        dgv_product_details.RowHeadersVisible = false;
                                        // Add columns with specific cell type
                                        DataGridViewTextBoxColumn columnSlno = new DataGridViewTextBoxColumn();
                                        DataGridViewImageColumn pbremove = new DataGridViewImageColumn();
                                        DataGridViewImageColumn columnptype = new DataGridViewImageColumn();
                                        DataGridViewTextBoxColumn columnItemName = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnQty = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnNotes = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnRemNotes = new DataGridViewTextBoxColumn();

                                        DataGridViewTextBoxColumn columnorderdetailsid = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnkoid = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columntid = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnmasterid = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columndate = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnstatus = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnitemid = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columntabletype = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnorderno = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnAddonStatusno = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnAVIDno = new DataGridViewTextBoxColumn();

                                        dgv_product_details.Columns.Add(columnSlno);
                                        dgv_product_details.Columns.Add(pbremove);
                                        dgv_product_details.Columns.Add(columnptype);
                                        dgv_product_details.Columns.Add(columnItemName);
                                        dgv_product_details.Columns.Add(columnQty);
                                        dgv_product_details.Columns.Add(columnNotes);
                                        dgv_product_details.Columns.Add(columnRemNotes);
                                        dgv_product_details.Columns.Add(columnorderdetailsid);
                                        dgv_product_details.Columns.Add(columnkoid);
                                        dgv_product_details.Columns.Add(columntid);
                                        dgv_product_details.Columns.Add(columnmasterid);
                                        dgv_product_details.Columns.Add(columndate);
                                        dgv_product_details.Columns.Add(columnstatus);
                                        dgv_product_details.Columns.Add(columnitemid);
                                        dgv_product_details.Columns.Add(columntabletype);
                                        dgv_product_details.Columns.Add(columnorderno);
                                        dgv_product_details.Columns.Add(columnAddonStatusno);
                                        dgv_product_details.Columns.Add(columnAVIDno);

                                        dgv_product_details.Columns[dslno].HeaderText = "Slno";
                                        dgv_product_details.Columns[ditemno].HeaderText = "Item Name";
                                        dgv_product_details.Columns[dptypeno].HeaderText = "PType";
                                        dgv_product_details.Columns[dqtyno].HeaderText = "Qty";
                                        dgv_product_details.Columns[dnotes].HeaderText = "Notes";
                                        dgv_product_details.Columns[dremnoteno].HeaderText = "Remove Notes";
                                        dgv_product_details.Columns[ddetailsidno].HeaderText = "orderdetailsid";
                                        dgv_product_details.Columns[dkoidno].HeaderText = "koid";
                                        dgv_product_details.Columns[dtidno].HeaderText = "tid";
                                        dgv_product_details.Columns[dactionno].HeaderText = "Action";
                                        dgv_product_details.Columns[dordermasteridno].HeaderText = "Ordermasterid";
                                        dgv_product_details.Columns[ddatetimeno].HeaderText = "datetime";
                                        dgv_product_details.Columns[dstatusno].HeaderText = "status";
                                        dgv_product_details.Columns[ditemidno].HeaderText = "itemid";
                                        dgv_product_details.Columns[dtabletypeno].HeaderText = "tabletype";
                                        dgv_product_details.Columns[dorderno].HeaderText = "orderno";
                                        dgv_product_details.Columns[dAddonStatusno].HeaderText = "AddonStatus";
                                        dgv_product_details.Columns[dAVIDno].HeaderText = "AVID";

                                        dgv_product_details.Columns[dslno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[ditemno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dptypeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                                        dgv_product_details.Columns[dqtyno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dnotes].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dremnoteno].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                        dgv_product_details.Columns[ddetailsidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dkoidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dtidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dactionno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dordermasteridno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[ddatetimeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dstatusno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[ditemidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dtabletypeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dorderno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dAddonStatusno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dAVIDno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                                        dgv_product_details.Columns[dslno].Visible = false;
                                        dgv_product_details.Columns[ddetailsidno].Visible = false;
                                        dgv_product_details.Columns[dkoidno].Visible = false;
                                        dgv_product_details.Columns[dtidno].Visible = false;
                                        dgv_product_details.Columns[dordermasteridno].Visible = false;
                                        dgv_product_details.Columns[ddatetimeno].Visible = false;
                                        dgv_product_details.Columns[dstatusno].Visible = false;
                                        dgv_product_details.Columns[ditemidno].Visible = false;
                                        dgv_product_details.Columns[dtabletypeno].Visible = false;
                                        dgv_product_details.Columns[dorderno].Visible = false;
                                        dgv_product_details.Columns[dAddonStatusno].Visible = false;
                                        dgv_product_details.Columns[dAVIDno].Visible = false;

                                        dgv_product_details.Rows.Clear();
                                        for (int k = 0; k < gridSourcedetails.Count; k++)
                                        {
                                            if (Convert.ToInt32(gridSourcedetails[k].Field12) == 1)
                                            {
                                                flagstart_ready = true;
                                            }

                                            //#region Update Prnt Staus Set to2
                                            //BllKitchenorder bllkitchen = new BllKitchenorder();
                                            //Dmnkitchen dmnkitchen = new Dmnkitchen();
                                            //dmnkitchen.Koid = Convert.ToInt32(gridSourcedetails[k].Field8);
                                            //dmnkitchen.CreatedBy = FrmDashboard.UserId;
                                            //dmnkitchen.KOTPrintStatus = 2;
                                            //dmnkitchen.BranchId = 1030;
                                            //var InsertStatus = bllkitchen.Kitchen_statusupdate("prd_updateKitchenorderstatus", dmnkitchen);
                                            //#endregion

                                            dgv_product_details.Rows.Add();
                                            dgv_product_details.Rows[k].Cells[dslno].Value = k + 1;
                                            dgv_product_details.Rows[k].Cells[ditemno].Value = gridSourcedetails[k].Field4;
                                            DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dptypeno];
                                            if (Convert.ToInt32(gridSourcedetails[k].Field13) == 3)
                                            {
                                                cellcolumntype.Value = Properties.Resources.combo_kicthenview_dgv;
                                                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                                cellcolumntype.ToolTipText = gridSourcedetails[k].Field4;
                                            }
                                            if (Convert.ToInt32(gridSourcedetails[k].Field19) == 1)
                                            {
                                                cellcolumntype.Value = Properties.Resources.addonbutton;
                                                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                                cellcolumntype.ToolTipText = "Addon [clcik view more...]";
                                            }
                                            else
                                            {
                                                cellcolumntype.Value = Properties.Resources.grid_select;
                                                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                                cellcolumntype.ToolTipText = gridSourcedetails[k].Field4;
                                            }
                                            decimal originalValue = Convert.ToDecimal(gridSourcedetails[k].Field5);
                                            dgv_product_details.Rows[k].Cells[dqtyno].Value = (int)Math.Floor(originalValue);

                                            dgv_product_details.Rows[k].Cells[dnotes].Value = gridSourcedetails[k].Field6;
                                            dgv_product_details.Rows[k].Cells[dremnoteno].Value = gridSourcedetails[k].Field11;
                                            dgv_product_details.Rows[k].Cells[ddetailsidno].Value = gridSourcedetails[k].Field9;
                                            dgv_product_details.Rows[k].Cells[dkoidno].Value = gridSourcedetails[k].Field8;
                                            dgv_product_details.Rows[k].Cells[dtidno].Value = gridSourcedetails[k].Field10;
                                            dgv_product_details.Rows[k].Cells[dactionno].Value = Properties.Resources.close_price_change;
                                            dgv_product_details.Rows[k].Cells[dordermasteridno].Value = gridSourcedetails[k].Field1;
                                            dgv_product_details.Rows[k].Cells[ddatetimeno].Value = gridSourcedetails[k].Fielddatetime;
                                            dgv_product_details.Rows[k].Cells[dstatusno].Value = gridSourcedetails[k].Field12;
                                            dgv_product_details.Rows[k].Cells[ditemidno].Value = gridSourcedetails[k].Field14;
                                            dgv_product_details.Rows[k].Cells[dtabletypeno].Value = gridSourcedetails[k].Field15;
                                            dgv_product_details.Rows[k].Cells[dorderno].Value = gridSourcedetails[k].Field17;

                                            DataGridViewImageCell cell = (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dactionno];
                                            if (Convert.ToInt32(gridSourcedetails[k].Field12) == 0)
                                            {
                                                cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                                cell.Value = Properties.Resources.orderview_cancelled;
                                                dgv_product_details.Rows[k].ReadOnly = true;
                                            }
                                            else
                                            {
                                                cell.Value = Properties.Resources.close_price_change;
                                                cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                            }

                                            if (Convert.ToInt32(gridSourcedetails[k].Field19) == Convert.ToInt32(AddonType.AddonType))
                                            {
                                                dgv_product_details.Rows[k].Cells[dAddonStatusno].Value = Convert.ToInt32(AddonType.AddonType);
                                                dgv_product_details.Rows[k].Cells[dAVIDno].Value = gridSourcedetails[k].Field20;
                                            }
                                            else
                                                dgv_product_details.Rows[k].Cells[dAddonStatusno].Value = Convert.ToInt32(AddonType.None);
                                        }
                                        for (int x = 0; x < dgv_product_details.RowCount; x++)
                                        {
                                            //if (x != dactionno)
                                            dgv_product_details.Rows[x].ReadOnly = true;
                                        }

                                        // Adding DataGridView to the second row
                                        tlp_product_box.Controls.Add(dgv_product_details, 0, 1);
                                        dgv_product_details.Dock = DockStyle.Fill;

                                        #endregion

                                        #region Description
                                        Label lbl_Description = new Label()
                                        {
                                            Text = "Note :   " + gridSourcedetails[0].Field16,
                                            Name = "lbl_Description" + gridSourcedetails[0].Field1,
                                            Dock = DockStyle.Fill,
                                            ForeColor = Color.White,
                                            AutoSize = true

                                        };
                                        if (gridSourcedetails[0].Field16 != null && gridSourcedetails[0].Field16 != "")
                                            tlp_product_box.Controls.Add(lbl_Description, 0, 2);
                                        #endregion

                                        #region Ready and Close Button Thrid Row

                                        // Sub TableLayoutPanel for Buttons
                                        TableLayoutPanel tlp_buttons = new TableLayoutPanel();
                                        tlp_buttons.Name = "tlp_buttons" + gridSourcedetails[0].Field8;
                                        tlp_buttons.Dock = DockStyle.Top;
                                        if (screenWidth > 1367 && screenHeight > 769)
                                        {
                                            tlp_buttons.Height = 45;
                                        }
                                        else
                                        {
                                            tlp_buttons.Height = 30;
                                        }

                                        tlp_buttons.ColumnCount = 3;
                                        tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                                        tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                                        tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                                        PictureBox pb_start = new PictureBox()
                                        {
                                            Name = "pb_start" + gridSourcedetails[0].Field8,
                                            Image = Properties.Resources.start,
                                            SizeMode = PictureBoxSizeMode.StretchImage,
                                            Dock = DockStyle.None,
                                            Tag = gridSourcedetails[0].Field8,
                                        };
                                        if (screenWidth > 1367 && screenHeight > 769)
                                        {
                                            pb_start.Height = 40;
                                            pb_start.Width = 120;
                                        }
                                        else
                                        {
                                            pb_start.Height = 30;
                                            pb_start.Width = 60;
                                        }
                                        pb_start.Click += pbstart_Click;

                                        PictureBox pb_ready = new PictureBox()
                                        {
                                            Name = "pb_ready" + gridSourcedetails[0].Field8,
                                            Image = Properties.Resources.ready_button,
                                            SizeMode = PictureBoxSizeMode.StretchImage,
                                            Dock = DockStyle.None,
                                            Tag = gridSourcedetails[0].Field8,

                                        };
                                        if (screenWidth > 1367 && screenHeight > 769)
                                        {
                                            pb_ready.Height = 40;
                                            pb_ready.Width = 120;
                                        }
                                        else
                                        {
                                            pb_ready.Height = 30;
                                            pb_ready.Width = 60;
                                        }
                                        pb_ready.Click += pbreadyfood_Click;

                                        PictureBox pb_cancelfood = new PictureBox()
                                        {
                                            Name = "pictureBoxRemove" + gridSourcedetails[0].Field8,
                                            Image = Properties.Resources.close_price_change,  // Replace "YourRemoveImage" with the name of your resource image
                                            SizeMode = PictureBoxSizeMode.StretchImage,
                                            Dock = DockStyle.None,
                                            Tag = gridSourcedetails[0].Field8,
                                        };
                                        if (screenWidth > 1367 && screenHeight > 769)
                                        {
                                            pb_cancelfood.Height = 40;
                                            pb_cancelfood.Width = 120;
                                        }
                                        else
                                        {
                                            pb_cancelfood.Height = 30;
                                            pb_cancelfood.Width = 60;
                                        }
                                        pb_cancelfood.Click += pbcancelfood_Click;

                                        if (flagstart_ready)
                                        {
                                            pb_start.Visible = true;
                                            pb_cancelfood.Visible = true;
                                            pb_ready.Visible = false;
                                        }
                                        else
                                        {
                                            pb_start.Visible = false;
                                            pb_ready.Visible = true;
                                            pb_cancelfood.Visible = false;
                                        }

                                        tlp_buttons.Controls.Add(pb_start, 0, 0);
                                        tlp_buttons.Controls.Add(pb_ready, 1, 0);
                                        tlp_buttons.Controls.Add(pb_cancelfood, 2, 0);

                                        // Adding Buttons TableLayoutPanel to the last row
                                        tlp_product_box.Controls.Add(tlp_buttons, 0, 3);
                                        #endregion

                                        flp_order_view.Controls.Add(tlp_product_box);

                                        orderbox.Add(new datagrid
                                        {
                                            Id = Convert.ToInt32(gridSourcedetails[0].Field8),
                                            datagridviewname = dgv_product_details.Name,
                                            createtime = gridSourcedetails[0].Fielddatetime
                                        });
                                    }
                                }

                            }
                            flp_order_view.Visible = true;
                            timer1.Start();

                        }
                        else
                        {
                            timer1.Stop();
                            flp_order_view.Controls.Clear();
                            pnl_empty_msg.Location = new Point(this.Width / 2 - pnl_empty_msg.Size.Width / 2, this.Height / 2 - pnl_empty_msg.Size.Height / 2);
                            pnl_empty_msg.Visible = true;
                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View txtorderid_search_KeyDown", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        private void txttablecode_Leave(object sender, EventArgs e)
        {
            if (txttablecode.Text == "")
            {
                txttablecode.Text = "Table Code....";
                txttablecode.ForeColor = Color.DarkGray;
            }
        }

        private void txttablecode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
            }
        }

        private void txttablecode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txttablecode.Text != "")
                    {
                        #region Fill Orders in Datagrid

                        txt_product_remove_des.Text = "Reason type here .....";
                        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                        BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "ko.OrderMasterId Field1,OrderNo Field2";
                        dmnSearch.Table = " tblkitchenOrder ko  LEFT JOIN tblOrderMaster om on om.OrderMasterId=ko.OrderMasterId left join tblTable_info ti on ti.tid=om.tid";
                        dmnSearch.WhereNdGroupBy = "where ko.status IN(1,2) and ti.TableCode= '" + txttablecode.Text + "'";
                        dmnSearch.OrderBy = "";
                        var gridSourcemaster = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSourcemaster.Count > 0)
                        {
                            pnl_empty_msg.Visible = false;
                            flp_order_view.Visible = false;
                            var distinctOrders = gridSourcemaster
                               .GroupBy(order => new { order.Field1, order.Field2 })
                               .Select(group => group.First())
                               .OrderBy(order => order.Field1)
                               .ThenBy(order => order.Field2)
                               .ToList();

                            flp_order_view.Controls.Clear();
                            TableLayoutPanel tlp_order_col = new TableLayoutPanel();
                            tlp_order_col.Dock = DockStyle.Fill;

                            foreach (var orders in distinctOrders)
                            {
                                bool flagstart_ready = false;
                                dmnSearch.Select = "om.OrderMasterId Field1,ti.TableCode Field2,om.status Field3,od.itemName Field4,od.Quantity Field5,od.Notes Field6,od.OrderNo Field7,od.CreatedDateTime Fielddatetime,ko.koid Field8,ko.OrderDetailsId Field9,om.Tid Field10,ko.RemoveNotes Field11,ko.Status Field12";
                                dmnSearch.Table = "tblkitchenOrder ko " +
                                    "Left Join tblOrderDetails od  on od.OrderDetailsId=ko.OrderDetailsId " +
                                    "Left Join tblOrderMaster om  on om.OrderMasterId=ko.OrderMasterId " +
                                    "Left Join tblTable_info  ti on ti.Tid=om.Tid";
                                dmnSearch.WhereNdGroupBy = "where om.status=1 and ko.OrderMasterId=" + orders.Field1 + " and ko.OrderNo =" + orders.Field2;
                                dmnSearch.OrderBy = "order by ko.koid DESC";
                                var gridSourcedetails = bllSearch_details.GetSearchData("prdSearch", dmnSearch);
                                if (gridSourcedetails.Count > 0)
                                {
                                    string tlpname = "tlp_product_box" + gridSourcedetails[0].Field8;
                                    TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                                    if (tlp_sele != null)
                                    {

                                        string dgvname = "dgv_product_details" + gridSourcedetails[0].Field8;
                                        string startbuttonname = "pb_start" + gridSourcedetails[0].Field8;
                                        string readybuttonname = "pb_ready" + gridSourcedetails[0].Field8;
                                        string cancelfoodbuttonname = "pictureBoxRemove" + gridSourcedetails[0].Field8;

                                        PictureBox btn_ready = this.Controls.Find(readybuttonname, true).FirstOrDefault() as PictureBox;
                                        PictureBox btn_start = this.Controls.Find(startbuttonname, true).FirstOrDefault() as PictureBox;
                                        PictureBox btn_cancelfood = this.Controls.Find(cancelfoodbuttonname, true).FirstOrDefault() as PictureBox;
                                        DataGridView dgv_product_details = this.Controls.Find(dgvname, true).FirstOrDefault() as DataGridView;

                                        #region Grid Edit Cancel  product
                                        for (int k = 0; k < gridSourcedetails.Count; k++)
                                        {
                                            if (Convert.ToInt32(gridSourcedetails[k].Field12) == 1)
                                            {
                                                flagstart_ready = true;
                                            }
                                            dgv_product_details.Rows[k].Cells[dactionno].Value = Properties.Resources.close_price_change;

                                            dgv_product_details.Rows[k].Cells[dstatusno].Value = gridSourcedetails[k].Field12;

                                            DataGridViewImageCell cell = (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dactionno];
                                            if (Convert.ToInt32(gridSourcedetails[k].Field12) == 0)
                                            {
                                                cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                                cell.Value = Properties.Resources.orderview_cancelled;
                                                dgv_product_details.Rows[k].ReadOnly = true;
                                            }
                                            else
                                            {
                                                cell.Value = Properties.Resources.close_price_change;
                                                cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                            }

                                        }
                                        #endregion

                                        #region Edit Ready and Close Button 
                                        if (flagstart_ready)
                                        {
                                            btn_start.Visible = true;
                                            btn_cancelfood.Visible = true;
                                            btn_ready.Visible = false;
                                        }
                                        else
                                        {
                                            btn_start.Visible = false;
                                            btn_ready.Visible = true;
                                            btn_cancelfood.Visible = false;
                                        }
                                        #endregion

                                    }
                                    else
                                    {
                                        #region Main TableLayoutPanel 
                                        // Main TableLayoutPanel for each product
                                        TableLayoutPanel tlp_product_box = new TableLayoutPanel();
                                        tlp_product_box.Name = "tlp_product_box" + gridSourcedetails[0].Field8;
                                        tlp_product_box.Dock = DockStyle.Top;
                                        if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                                        {
                                            FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                                            if (_instanceFrmdash.ChkWritePermission(Convert.ToInt32(formsenum.Kitchen_View)))
                                                tlp_product_box.Enabled = true;
                                            else
                                                tlp_product_box.Enabled = false;
                                        }
                                        int WidthPercentage = 30;
                                        int heightPercentage = 30;
                                        int controlWidth = (int)(this.Width * WidthPercentage / 100);
                                        int controlHeight = (int)(this.Height * heightPercentage / 100);
                                        tlp_product_box.Size = new Size(controlWidth, controlHeight);



                                        tlp_product_box.BackColor = boxcolor;
                                        tlp_product_box.RowCount = 4;
                                        //tlp_product_box.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                                        tlp_product_box.Margin = new Padding(20, 10, 0, 0);
                                        tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                        tlp_product_box.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // 50% height
                                        tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                        tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                        //ApplyRoundedCorners(tlp_product_box);
                                        #endregion

                                        #region Heading Time and Tablecode Frist Row
                                        // Sub TableLayoutPanel for Heading
                                        TableLayoutPanel tlp_product_heading = new TableLayoutPanel();
                                        tlp_product_heading.Name = "tlp_product_heading" + gridSourcedetails[0].Field8;
                                        tlp_product_heading.Dock = DockStyle.Top;
                                        if (screenWidth > 1367 && screenHeight > 769)
                                        {
                                            tlp_product_heading.Height = 50;
                                        }
                                        else
                                        {
                                            tlp_product_heading.Height = 20;
                                        }

                                        tlp_product_heading.ColumnCount = 2;
                                        tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                                        tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                                        Label lbl_tablecode = new Label()
                                        {
                                            Text = gridSourcedetails[0].Field2,
                                            Name = "lbl_tablecode" + gridSourcedetails[0].Field8,
                                            Dock = DockStyle.Fill,
                                            ForeColor = Color.White,

                                        };
                                        if (screenWidth > 1367 && screenHeight > 769)
                                            lbl_tablecode.Font = new Font("Arial", 15, FontStyle.Bold);
                                        else
                                            lbl_tablecode.Font = new Font("Arial", 10, FontStyle.Bold);

                                        DateTime startTime = gridSourcedetails[0].Fielddatetime;
                                        DateTime currentTime = DateTime.Now;
                                        TimeSpan timeDifference = currentTime - startTime;
                                        int hoursDifference = timeDifference.Hours;
                                        int minutesDifference = timeDifference.Minutes;
                                        int secondsDifference = timeDifference.Seconds;
                                        string timeDifferenceString = $"{hoursDifference} hours, {minutesDifference} minutes {secondsDifference}";//,  seconds

                                        Label lbl_time = new Label()
                                        {
                                            Text = timeDifferenceString,
                                            Name = "lbl_time" + gridSourcedetails[0].Field8,
                                            Dock = DockStyle.Fill,
                                            ForeColor = Color.White,

                                        };
                                        if (screenWidth > 1367 && screenHeight > 769)
                                            lbl_time.Font = new Font("Arial", 13, FontStyle.Bold);
                                        else
                                            lbl_time.Font = new Font("Arial", 8, FontStyle.Bold);

                                        tlp_product_heading.Controls.Add(lbl_tablecode, 0, 0);
                                        tlp_product_heading.Controls.Add(lbl_time, 1, 0);

                                        // Adding Heading TableLayoutPanel to the first row
                                        tlp_product_box.Controls.Add(tlp_product_heading, 0, 0);
                                        #endregion

                                        #region DataGridView Second Row
                                        // DataGridView for product details
                                        DataGridView dgv_product_details = new DataGridView();
                                        dgv_product_details.CellClick += dgv_product_details_CellClick;
                                        dgv_product_details.AllowUserToDeleteRows = true;
                                        // Prevent the user from adding new rows
                                        dgv_product_details.AllowUserToAddRows = false;
                                        dgv_product_details.Name = "dgv_product_details_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                        dgv_product_details.Tag = orders.Field1 + gridSourcedetails[0].Field17;
                                        // Enable double buffering for the DataGridView
                                        dgv_product_details.DoubleBuffered(true);


                                        dgv_product_details.BorderStyle = BorderStyle.None;
                                        dgv_product_details.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                                        dgv_product_details.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                                        dgv_product_details.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                                        dgv_product_details.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                                        dgv_product_details.BackgroundColor = Color.White;

                                        dgv_product_details.EnableHeadersVisualStyles = false;
                                        dgv_product_details.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                                        dgv_product_details.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                                        dgv_product_details.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                                        if (screenWidth > 1367 && screenHeight > 769)
                                        {
                                            dgv_product_details.ColumnHeadersHeight = 50;
                                            dgv_product_details.RowTemplate.Height = 50;
                                        }
                                        else
                                        {
                                            dgv_product_details.ColumnHeadersHeight = 30;
                                            dgv_product_details.RowTemplate.Height = 30;
                                        }

                                        dgv_product_details.RowHeadersVisible = false;
                                        // Add columns with specific cell type
                                        DataGridViewTextBoxColumn columnSlno = new DataGridViewTextBoxColumn();
                                        DataGridViewImageColumn pbremove = new DataGridViewImageColumn();
                                        DataGridViewImageColumn columnptype = new DataGridViewImageColumn();
                                        DataGridViewTextBoxColumn columnItemName = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnQty = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnNotes = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnRemNotes = new DataGridViewTextBoxColumn();

                                        DataGridViewTextBoxColumn columnorderdetailsid = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnkoid = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columntid = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnmasterid = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columndate = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnstatus = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnitemid = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columntabletype = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnorderno = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnAddonStatusno = new DataGridViewTextBoxColumn();
                                        DataGridViewTextBoxColumn columnAVIDno = new DataGridViewTextBoxColumn();

                                        dgv_product_details.Columns.Add(columnSlno);
                                        dgv_product_details.Columns.Add(pbremove);
                                        dgv_product_details.Columns.Add(columnptype);
                                        dgv_product_details.Columns.Add(columnItemName);
                                        dgv_product_details.Columns.Add(columnQty);
                                        dgv_product_details.Columns.Add(columnNotes);
                                        dgv_product_details.Columns.Add(columnRemNotes);
                                        dgv_product_details.Columns.Add(columnorderdetailsid);
                                        dgv_product_details.Columns.Add(columnkoid);
                                        dgv_product_details.Columns.Add(columntid);
                                        dgv_product_details.Columns.Add(columnmasterid);
                                        dgv_product_details.Columns.Add(columndate);
                                        dgv_product_details.Columns.Add(columnstatus);
                                        dgv_product_details.Columns.Add(columnitemid);
                                        dgv_product_details.Columns.Add(columntabletype);
                                        dgv_product_details.Columns.Add(columnorderno);
                                        dgv_product_details.Columns.Add(columnAddonStatusno);
                                        dgv_product_details.Columns.Add(columnAVIDno);

                                        dgv_product_details.Columns[dslno].HeaderText = "Slno";
                                        dgv_product_details.Columns[ditemno].HeaderText = "Item Name";
                                        dgv_product_details.Columns[dptypeno].HeaderText = "PType";
                                        dgv_product_details.Columns[dqtyno].HeaderText = "Qty";
                                        dgv_product_details.Columns[dnotes].HeaderText = "Notes";
                                        dgv_product_details.Columns[dremnoteno].HeaderText = "Remove Notes";
                                        dgv_product_details.Columns[ddetailsidno].HeaderText = "orderdetailsid";
                                        dgv_product_details.Columns[dkoidno].HeaderText = "koid";
                                        dgv_product_details.Columns[dtidno].HeaderText = "tid";
                                        dgv_product_details.Columns[dactionno].HeaderText = "Action";
                                        dgv_product_details.Columns[dordermasteridno].HeaderText = "Ordermasterid";
                                        dgv_product_details.Columns[ddatetimeno].HeaderText = "datetime";
                                        dgv_product_details.Columns[dstatusno].HeaderText = "status";
                                        dgv_product_details.Columns[ditemidno].HeaderText = "itemid";
                                        dgv_product_details.Columns[dtabletypeno].HeaderText = "tabletype";
                                        dgv_product_details.Columns[dorderno].HeaderText = "orderno";
                                        dgv_product_details.Columns[dAddonStatusno].HeaderText = "AddonStatus";
                                        dgv_product_details.Columns[dAVIDno].HeaderText = "AVID";

                                        dgv_product_details.Columns[dslno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[ditemno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dptypeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                                        dgv_product_details.Columns[dqtyno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dnotes].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dremnoteno].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                                        dgv_product_details.Columns[ddetailsidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dkoidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dtidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dactionno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dordermasteridno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[ddatetimeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dstatusno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[ditemidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dtabletypeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dorderno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dAddonStatusno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dAVIDno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                                        dgv_product_details.Columns[dslno].Visible = false;
                                        dgv_product_details.Columns[ddetailsidno].Visible = false;
                                        dgv_product_details.Columns[dkoidno].Visible = false;
                                        dgv_product_details.Columns[dtidno].Visible = false;
                                        dgv_product_details.Columns[dordermasteridno].Visible = false;
                                        dgv_product_details.Columns[ddatetimeno].Visible = false;
                                        dgv_product_details.Columns[dstatusno].Visible = false;
                                        dgv_product_details.Columns[ditemidno].Visible = false;
                                        dgv_product_details.Columns[dtabletypeno].Visible = false;
                                        dgv_product_details.Columns[dorderno].Visible = false;
                                        dgv_product_details.Columns[dAddonStatusno].Visible = false;
                                        dgv_product_details.Columns[dAVIDno].Visible = false;

                                        dgv_product_details.Rows.Clear();
                                        for (int k = 0; k < gridSourcedetails.Count; k++)
                                        {
                                            if (Convert.ToInt32(gridSourcedetails[k].Field12) == 1)
                                            {
                                                flagstart_ready = true;
                                            }

                                            //#region Update Prnt Staus Set to2
                                            //BllKitchenorder bllkitchen = new BllKitchenorder();
                                            //Dmnkitchen dmnkitchen = new Dmnkitchen();
                                            //dmnkitchen.Koid = Convert.ToInt32(gridSourcedetails[k].Field8);
                                            //dmnkitchen.CreatedBy = FrmDashboard.UserId;
                                            //dmnkitchen.KOTPrintStatus = 2;
                                            //dmnkitchen.BranchId = 1030;
                                            //var InsertStatus = bllkitchen.Kitchen_statusupdate("prd_updateKitchenorderstatus", dmnkitchen);
                                            //#endregion

                                            dgv_product_details.Rows.Add();
                                            dgv_product_details.Rows[k].Cells[dslno].Value = k + 1;
                                            dgv_product_details.Rows[k].Cells[ditemno].Value = gridSourcedetails[k].Field4;
                                            DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dptypeno];
                                            if (Convert.ToInt32(gridSourcedetails[k].Field13) == 3)
                                            {
                                                cellcolumntype.Value = Properties.Resources.combo_kicthenview_dgv;
                                                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                                cellcolumntype.ToolTipText = gridSourcedetails[k].Field4;
                                            }
                                            if (Convert.ToInt32(gridSourcedetails[k].Field19) == 1)
                                            {
                                                cellcolumntype.Value = Properties.Resources.addonbutton;
                                                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                                cellcolumntype.ToolTipText = "Addon [clcik view more...]";
                                            }
                                            else
                                            {
                                                cellcolumntype.Value = Properties.Resources.grid_select;
                                                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                                cellcolumntype.ToolTipText = gridSourcedetails[k].Field4;
                                            }
                                            decimal originalValue = Convert.ToDecimal(gridSourcedetails[k].Field5);
                                            dgv_product_details.Rows[k].Cells[dqtyno].Value = (int)Math.Floor(originalValue);

                                            dgv_product_details.Rows[k].Cells[dnotes].Value = gridSourcedetails[k].Field6;
                                            dgv_product_details.Rows[k].Cells[dremnoteno].Value = gridSourcedetails[k].Field11;
                                            dgv_product_details.Rows[k].Cells[ddetailsidno].Value = gridSourcedetails[k].Field9;
                                            dgv_product_details.Rows[k].Cells[dkoidno].Value = gridSourcedetails[k].Field8;
                                            dgv_product_details.Rows[k].Cells[dtidno].Value = gridSourcedetails[k].Field10;
                                            dgv_product_details.Rows[k].Cells[dactionno].Value = Properties.Resources.close_price_change;
                                            dgv_product_details.Rows[k].Cells[dordermasteridno].Value = gridSourcedetails[k].Field1;
                                            dgv_product_details.Rows[k].Cells[ddatetimeno].Value = gridSourcedetails[k].Fielddatetime;
                                            dgv_product_details.Rows[k].Cells[dstatusno].Value = gridSourcedetails[k].Field12;
                                            dgv_product_details.Rows[k].Cells[ditemidno].Value = gridSourcedetails[k].Field14;
                                            dgv_product_details.Rows[k].Cells[dtabletypeno].Value = gridSourcedetails[k].Field15;
                                            dgv_product_details.Rows[k].Cells[dorderno].Value = gridSourcedetails[k].Field17;

                                            DataGridViewImageCell cell = (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dactionno];
                                            if (Convert.ToInt32(gridSourcedetails[k].Field12) == 0)
                                            {
                                                cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                                cell.Value = Properties.Resources.orderview_cancelled;
                                                dgv_product_details.Rows[k].ReadOnly = true;
                                            }
                                            else
                                            {
                                                cell.Value = Properties.Resources.close_price_change;
                                                cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                            }

                                            if (Convert.ToInt32(gridSourcedetails[k].Field19) == Convert.ToInt32(AddonType.AddonType))
                                            {
                                                dgv_product_details.Rows[k].Cells[dAddonStatusno].Value = Convert.ToInt32(AddonType.AddonType);
                                                dgv_product_details.Rows[k].Cells[dAVIDno].Value = gridSourcedetails[k].Field20;
                                            }
                                            else
                                                dgv_product_details.Rows[k].Cells[dAddonStatusno].Value = Convert.ToInt32(AddonType.None);
                                        }
                                        for (int x = 0; x < dgv_product_details.RowCount; x++)
                                        {
                                            //if (x != dactionno)
                                            dgv_product_details.Rows[x].ReadOnly = true;
                                        }

                                        // Adding DataGridView to the second row
                                        tlp_product_box.Controls.Add(dgv_product_details, 0, 1);
                                        dgv_product_details.Dock = DockStyle.Fill;

                                        #endregion

                                        #region Description
                                        Label lbl_Description = new Label()
                                        {
                                            Text = "Note :   " + gridSourcedetails[0].Field16,
                                            Name = "lbl_Description" + gridSourcedetails[0].Field1,
                                            Dock = DockStyle.Fill,
                                            ForeColor = Color.White,
                                            AutoSize = true

                                        };
                                        if (gridSourcedetails[0].Field16 != null && gridSourcedetails[0].Field16 != "")
                                            tlp_product_box.Controls.Add(lbl_Description, 0, 2);
                                        #endregion

                                        #region Ready and Close Button Thrid Row

                                        // Sub TableLayoutPanel for Buttons
                                        TableLayoutPanel tlp_buttons = new TableLayoutPanel();
                                        tlp_buttons.Name = "tlp_buttons" + gridSourcedetails[0].Field8;
                                        tlp_buttons.Dock = DockStyle.Top;
                                        if (screenWidth > 1367 && screenHeight > 769)
                                        {
                                            tlp_buttons.Height = 45;
                                        }
                                        else
                                        {
                                            tlp_buttons.Height = 30;
                                        }

                                        tlp_buttons.ColumnCount = 3;
                                        tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                                        tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                                        tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                                        PictureBox pb_start = new PictureBox()
                                        {
                                            Name = "pb_start" + gridSourcedetails[0].Field8,
                                            Image = Properties.Resources.start,
                                            SizeMode = PictureBoxSizeMode.StretchImage,
                                            Dock = DockStyle.None,
                                            Tag = gridSourcedetails[0].Field8,
                                        };
                                        if (screenWidth > 1367 && screenHeight > 769)
                                        {
                                            pb_start.Height = 40;
                                            pb_start.Width = 120;
                                        }
                                        else
                                        {
                                            pb_start.Height = 30;
                                            pb_start.Width = 60;
                                        }
                                        pb_start.Click += pbstart_Click;

                                        PictureBox pb_ready = new PictureBox()
                                        {
                                            Name = "pb_ready" + gridSourcedetails[0].Field8,
                                            Image = Properties.Resources.ready_button,
                                            SizeMode = PictureBoxSizeMode.StretchImage,
                                            Dock = DockStyle.None,
                                            Tag = gridSourcedetails[0].Field8,

                                        };
                                        if (screenWidth > 1367 && screenHeight > 769)
                                        {
                                            pb_ready.Height = 40;
                                            pb_ready.Width = 120;
                                        }
                                        else
                                        {
                                            pb_ready.Height = 30;
                                            pb_ready.Width = 60;
                                        }
                                        pb_ready.Click += pbreadyfood_Click;

                                        PictureBox pb_cancelfood = new PictureBox()
                                        {
                                            Name = "pictureBoxRemove" + gridSourcedetails[0].Field8,
                                            Image = Properties.Resources.close_price_change,  // Replace "YourRemoveImage" with the name of your resource image
                                            SizeMode = PictureBoxSizeMode.StretchImage,
                                            Dock = DockStyle.None,
                                            Tag = gridSourcedetails[0].Field8,
                                        };
                                        if (screenWidth > 1367 && screenHeight > 769)
                                        {
                                            pb_cancelfood.Height = 40;
                                            pb_cancelfood.Width = 120;
                                        }
                                        else
                                        {
                                            pb_cancelfood.Height = 30;
                                            pb_cancelfood.Width = 60;
                                        }
                                        pb_cancelfood.Click += pbcancelfood_Click;

                                        if (flagstart_ready)
                                        {
                                            pb_start.Visible = true;
                                            pb_cancelfood.Visible = true;
                                            pb_ready.Visible = false;
                                        }
                                        else
                                        {
                                            pb_start.Visible = false;
                                            pb_ready.Visible = true;
                                            pb_cancelfood.Visible = false;
                                        }

                                        tlp_buttons.Controls.Add(pb_start, 0, 0);
                                        tlp_buttons.Controls.Add(pb_ready, 1, 0);
                                        tlp_buttons.Controls.Add(pb_cancelfood, 2, 0);

                                        // Adding Buttons TableLayoutPanel to the last row
                                        tlp_product_box.Controls.Add(tlp_buttons, 0, 3);
                                        #endregion

                                        flp_order_view.Controls.Add(tlp_product_box);

                                        orderbox.Add(new datagrid
                                        {
                                            Id = Convert.ToInt32(gridSourcedetails[0].Field8),
                                            datagridviewname = dgv_product_details.Name,
                                            createtime = gridSourcedetails[0].Fielddatetime
                                        });
                                    }

                                }

                            }
                            flp_order_view.Visible = true;
                            timer1.Start();

                        }
                        else
                        {
                            timer1.Stop();
                            flp_order_view.Controls.Clear();
                            pnl_empty_msg.Location = new Point(this.Width / 2 - pnl_empty_msg.Size.Width / 2, this.Height / 2 - pnl_empty_msg.Size.Height / 2);
                            pnl_empty_msg.Visible = true;
                        }

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View txttablecode_KeyDown", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
        private void txttablecode_Enter(object sender, EventArgs e)
        {
            if (txttablecode.Text == "Table Code....")
            {
                txttablecode.Text = "";
                txttablecode.ForeColor = Color.Black;
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            try
            {
                KitchenDisplayCount = FrmLogin.KitchenDisplayCount;
                btn_viewmore_order.Visible = true;
                txttablecode.Text = "Table Code....";
                txttablecode.ForeColor = Color.DarkGray;
                txtorderid_search.Text = "Order Id ....";
                pnl_empty_msg.Visible = false;
                refreshtimer.Start();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View btn_refresh_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
        private void pnl_products_rem_descr_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                float borderRadius = 10f; // adjust this value to change the roundness of the corners
                GraphicsPath path = new GraphicsPath();
                RectangleF rect = new RectangleF(pnl_products_rem_descr.ClientRectangle.X, pnl_products_rem_descr.ClientRectangle.Y, pnl_products_rem_descr.ClientRectangle.Width, pnl_products_rem_descr.ClientRectangle.Height);
                path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
                path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
                path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
                path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
                path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
                path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
                path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
                path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
                path.CloseFigure();
                pnl_products_rem_descr.Region = new Region(path);
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View pnl_products_rem_descr_Paint", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    if (pnl_products_rem_descr.Visible)
                    {
                        tlp_main.Enabled = true;
                        pnl_products_rem_descr.Visible = false;
                        refreshtimer.Start();
                    }
                    else if (pnlproductviewmore.Visible)
                    {
                        tlp_main.Enabled = true;
                        pnlproductviewmore.Visible = false;
                    }
                    else
                    {
                        FrmDashboard _instancedash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                        _instancedash.Closeform();
                    }
                    return true;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void btn_close_viewmore_Click(object sender, EventArgs e)
        {
            tlp_main.Enabled = true;
            pnlproductviewmore.Visible = false;
        }

        private void FrmKitchenView_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (FrmDashboard.AutoLogoutWhenIdle)
                {
                    AppSession.UnregisterForm(this);
                }
                refreshtimer.Stop();
                this.Dispose();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View FrmKitchenView_FormClosing", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        private void flp_order_view_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                flp_order_view.SuspendLayout();
                flp_order_view.ResumeLayout();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View flp_order_view_Scroll", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        private void txtdisplaycount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                // If the pressed key is not a digit and not a control key (like backspace), suppress it
                e.Handled = true;
            }
        }

        private void btn_viewmore_order_Click(object sender, EventArgs e)
        {
            KitchenDisplayCount = 25;
            btn_viewmore_order.Visible = false;
        }

        private void blinkTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                lblprintloading.Text = Convert.ToString(lblprintloading.Tag);
                lblprintloading.Visible = !lblprintloading.Visible;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View blinkTimer_Tick", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        private void btnprintcount_Click(object sender, EventArgs e)
        {
            if (refreshtimer.Enabled)
            {
                refreshtimer.Start();
            }
        }
        private void refreshtimer_Tick(object sender, EventArgs e)
        {
            FillOrdersAsync();

            if (Convert.ToInt32(btnprintcount.Text) > 0)
                RunMethodInSeparateThread(PrintOrderAsync);
            else
            {
                this.Invoke(new Action(() =>
                {
                    lblprintloading.Visible = false;
                }));
            }
        }
        private static void RunMethodInSeparateThread(Action action)
        {
            var thread = new Thread(new ThreadStart(action));
            thread.Start();
        }

        private void dgv_viewmore_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == addactionno && e.RowIndex>=0)
                {
                    DmnKitchenaddonremove dmnRemove_addon = new DmnKitchenaddonremove();
                    BllKitchenorder bll_rem_addon = new BllKitchenorder();
                    decimal newprice = 0, discountprice = 0, TaxAmount = 0, finalprice = 0, disAmnt = 0;
                    string newpart = "";
                    var result = CMessageBox.Show("Are you sure you want to cancel this Addon?", "Confirm The Action", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        refreshtimer.Stop();
                        #region To Reset Addon ID(remove id form avid name)

                        string avidValue = Convert.ToString(dgv_viewmore.Rows[e.RowIndex].Cells[addavidno].Value);
                        string[] avidParts = avidValue.Split('_');
                        string extractedpid = $"{avidParts[0]}";
                        newpart = newpart + extractedpid + "_";
                        int remove_koid = Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addkoidno].Value);
                        for (int i = 0; i < dgv_viewmore.RowCount; i++)
                        {
                            if (Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addaddonstatusno].Value) == 2 && Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addkoidno].Value) != remove_koid)
                            {
                                string ccode = Convert.ToString(dgv_viewmore.Rows[i].Cells[addccodeno].Value);
                                decimal quantityDecimal = Convert.ToDecimal(dgv_viewmore.Rows[i].Cells[addqtyno].Value);
                                int quantityInt = (int)Math.Floor(quantityDecimal);
                                newpart = newpart + ccode + quantityInt.ToString();
                            }
                        }

                        for (int i = 0; i < dgv_viewmore.RowCount; i++)
                        {
                            if (Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addaddonstatusno].Value) == 2 && Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addkoidno].Value) != remove_koid)
                            {
                                dgv_viewmore.Rows[i].Cells[addavidno].Value = newpart;
                            }
                        }

                        #endregion

                        #region Price Reset main product after remove addon
                        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "DiscountPer Field1,TaxType Field2,TaxPer Field3,Tax Field4,srate Field5";
                        dmnSearch.Table = "tblOrderDetails";
                        dmnSearch.WhereNdGroupBy = "where OrderMasterId = " + Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addordermasteridno].Value) + " and OrderNo = " + Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addonorderno].Value) + " and AddonStatus = 1 and AV_id = '" + avidValue + "'";
                        dmnSearch.OrderBy = "";
                        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSource.Count > 0)
                        {
                            newprice = Convert.ToDecimal(gridSource[0].Field5) - Convert.ToDecimal(dgv_viewmore.Rows[e.RowIndex].Cells[addonpriceno].Value);
                            disAmnt = (Convert.ToDecimal(gridSource[0].Field1) * newprice) / 100;

                            discountprice = newprice - disAmnt;
                            if (gridSource[0].Field2 == "GST")
                                TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].Field3) * discountprice / 100);
                            else if (gridSource[0].Field2 == "VAT")
                                TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].Field3) * discountprice / 100);

                            if (Convert.ToString(gridSource[0].Field4) == "Exclusive")
                                finalprice = (newprice - disAmnt) + TaxAmount;
                            else if (Convert.ToString(gridSource[0].Field4) == "Inclusive")
                                finalprice = (newprice - disAmnt);

                            #endregion

                            dmnRemove_addon.Amount = newprice;
                            dmnRemove_addon.DiscountAmount = disAmnt;
                            if (gridSource[0].Field2 == "GST")
                            {
                                dmnRemove_addon.IGSTAmount = TaxAmount;
                                dmnRemove_addon.CGSTAmount = TaxAmount / 2;
                                dmnRemove_addon.SGSTAmount = TaxAmount / 2;
                                dmnRemove_addon.VATAmount = 0;
                            }
                            else if (gridSource[0].Field2 == "VAT")
                            {
                                dmnRemove_addon.VATAmount = TaxAmount;
                                dmnRemove_addon.IGSTAmount = 0;
                                dmnRemove_addon.CGSTAmount = 0;
                                dmnRemove_addon.SGSTAmount = 0;
                            }
                        }
                        dmnRemove_addon.TaxAmount = TaxAmount;
                        dmnRemove_addon.NetAmount = finalprice;
                        dmnRemove_addon.Srate = newprice;
                        dmnRemove_addon.MarketPrice = finalprice;
                        dmnRemove_addon.OrderNo = Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addonorderno].Value);

                        dmnRemove_addon.OrderDetailsId = Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addorderdetailidno].Value);
                        dmnRemove_addon.koid = Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addkoidno].Value);
                        dmnRemove_addon.OrderMasterId = Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addordermasteridno].Value);
                        dmnRemove_addon.AVid = Convert.ToString(dgv_viewmore.Rows[e.RowIndex].Cells[addavidno].Value);
                        dmnRemove_addon.Status = 0;
                        dmnRemove_addon.CreatedBy = FrmDashboard.UserId;
                        dmnRemove_addon.Update_AVid = newpart;
                        var InsertStatusaddon = bll_rem_addon.Kitchenremove_addon("prd_remove_KitchenOrder_addon", dmnRemove_addon);

                        dgv_viewmore.Rows.RemoveAt(e.RowIndex);
                        if(dgv_viewmore.RowCount==0)
                        {
                            pnlproductviewmore.Visible = false;
                            tlp_main.Enabled = true;
                        }
                        else
                        {
                            for (int i = 0; i < dgv_viewmore.RowCount; i++)
                                dgv_viewmore.Rows[i].Cells[addslno].Value = i + 1;
                        }
                        refreshtimer.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View dgv_viewmore_CellDoubleClick", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }

        }

        private void pnl_kitchenorderBill_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void pnl_kitchenorderBill_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point p = PointToScreen(e.Location);
                pnl_kitchenorderBill.Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }

        private void pnl_kitchenorderBill_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void tm_memory_Tick(object sender, EventArgs e)
        {
            Process currentProcess = Process.GetCurrentProcess();
            long memoryInBytes = currentProcess.PrivateMemorySize64;
            double memoryInMB = memoryInBytes / (1024.0 * 1024.0);
            lblmemory.Text = $"Memory: {memoryInMB:F2} MB";
        }
    }
}
