using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CustomMessageBox;
using Domain;
using FastMember;
using FoodandBeverage.Classes;
using FoodandBeverage.Interfaces;
using Microsoft.AspNet.SignalR.Client;

namespace FoodandBeverage.Forms
{
    public partial class FrmKitchenView : Form
    {
        private Timer refreshtimer;
        public FrmKitchenView()
        {
            InitializeComponent();

        }
        #region Declaration Grid
        public static int dslno = 0;
        public static int dactionno = 1;
        public static int dptypeno = 2;
        public static int ditemno = 3;
        public static int dqtyno =4;
        public static int dnotes = 5;
        public static int dremnoteno = 6;
        public static int ddetailsidno = 7;
        public static int dkoidno = 8;
        public static int dtidno = 9;
        public static int dordermasteridno = 10;
        public static int ddatetimeno = 11;
        public static int dstatusno = 12;
        public static int ditemidno = 13;
        public static int dtabletypeno = 14;
        public static int dorderno = 15;
        #endregion

        Color boxcolor = Color.FromArgb(0x42, 0x67, 0xB2);
        public int dgv_view_rowno = 0;
        public string dgv_view_name = "";
        public List<datagrid> orderbox = new List<datagrid>();
        public bool pbcancelfoodclciked = false;
        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _hubProxy;
        public int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        public string RPTPath =Application.StartupPath + @"\KitchenBill"; 
        TableLayoutPanel tlp_order_col = new TableLayoutPanel();

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtorderid_search, txttablecode, txt_product_remove_des })
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
            refreshtimer = new Timer();
            refreshtimer.Tick += new EventHandler(refreshtimerTick);
            refreshtimer.Interval = 2000; // in miliseconds
            refreshtimer.Start();
        }

        private void refreshtimerTick(object sender, EventArgs e)
        {
            Fillorders();
        }

        private void FrmKitchenView_Load(object sender, EventArgs e)
        {
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
            textb_shape();
            tlp_order_col.Dock = DockStyle.Fill;
            InitTimer();
            //Fillorders();

        }
        public class datagrid
        {
            public int Id { get; set; }
            public string datagridviewname { get; set; }
            public DateTime createtime { get; set; }

        }


        public void Fillorders()
        {
            try
            {
                this.SuspendLayout();
                #region Fill Orders in Datagrid
                txt_product_remove_des.Text = "Reason type here .....";

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "OrderMasterId Field1,OrderNo Field2";
                dmnSearch.Table = " tblkitchenOrder ";
                dmnSearch.WhereNdGroupBy = "where status in(1,2)";
                dmnSearch.OrderBy = "order by koid DESC";
                var gridSourcemaster = bllSearch.GetSearchData("prdSearch", dmnSearch);
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
                    //flp_order_view.Controls.Clear();
                    foreach (var orders in distinctOrders)
                    {
                        bool flagstart_ready = false;
                        dmnSearch.Select = "om.OrderMasterId Field1,ti.TableCode Field2,om.status Field3,od.itemName Field4,od.Quantity Field5,od.Notes Field6,od.OrderNo Field7" +
                            ",od.CreatedDateTime Fielddatetime,ko.koid Field8,ko.OrderDetailsId Field9,om.Tid Field10,ko.RemoveNotes Field11,ko.Status Field12,ko.tabletype Field13,od.itemid Field14,od.TableType Field15,od.MainRemarks Field16,ko.Orderno Field17,ko.KOTPrintStatus Field18";
                        dmnSearch.Table = "tblkitchenOrder ko " +
                            "Left Join tblOrderDetails od  on od.OrderDetailsId=ko.OrderDetailsId " +
                            "Left Join tblOrderMaster om  on om.OrderMasterId=ko.OrderMasterId " +
                            "Left Join tblTable_info  ti on ti.Tid=ko.Tid";
                        dmnSearch.WhereNdGroupBy = "where om.status=1 and ko.OrderMasterId=" + orders.Field1 + " and ko.OrderNo =" + orders.Field2;
                        dmnSearch.OrderBy = "order by ko.koid DESC";
                        var gridSourcedetails = bllSearch_details.GetSearchData("prdSearch", dmnSearch);
                        if(gridSourcedetails.Count>0)
                            {
                            string tlpname = "tlp_product_box_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                            TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                            if (tlp_sele != null)
                            {
                                string dgvname = "dgv_product_details_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                string startbuttonname = "pb_start_k_" + orders.Field1 + gridSourcedetails[0].Field17; ;
                                string readybuttonname = "pb_ready_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                string cancelfoodbuttonname = "pictureBoxRemove_k_" + orders.Field1 + gridSourcedetails[0].Field17; ;

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
                                        dgv_product_details.Rows[k].Cells[dremnoteno].Value = gridSourcedetails[k].Field11;
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
                                refreshtimer.Stop();
                                #region Main TableLayoutPanel 
                                // Main TableLayoutPanel for each product
                                TableLayoutPanel tlp_product_box = new TableLayoutPanel();
                                tlp_product_box.Name = "tlp_product_box_k_" + orders.Field1 + gridSourcedetails[0].Field17;
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
                                ApplyRoundedCorners(tlp_product_box);
                                #endregion

                                #region Heading Time and Tablecode Frist Row
                                // Sub TableLayoutPanel for Heading
                                TableLayoutPanel tlp_product_heading = new TableLayoutPanel();
                                tlp_product_heading.Name = "tlp_product_heading_k_" + orders.Field1 + gridSourcedetails[0].Field17;;
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
                                    Name = "lbl_tablecod_k_" + orders.Field1 + gridSourcedetails[0].Field17,
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
                                    Name = "lbl_time_K_"  + orders.Field1 + gridSourcedetails[0].Field17,
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
                                dgv_product_details.Tag= orders.Field1 + gridSourcedetails[0].Field17;
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


                                dgv_product_details.Columns[dslno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                dgv_product_details.Columns[ditemno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                dgv_product_details.Columns[dptypeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                                dgv_product_details.Columns[dqtyno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                dgv_product_details.Columns[dnotes].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                dgv_product_details.Columns[dremnoteno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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
                                    Name = "lbl_Description_k_" + orders.Field1 + gridSourcedetails[0].Field17,
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
                                tlp_buttons.Name = "tlp_buttons_k_" + orders.Field1 + gridSourcedetails[0].Field17;
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
                                    Name = "pb_start_k_" + orders.Field1  + gridSourcedetails[0].Field17,
                                    Image = Properties.Resources.start,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Dock = DockStyle.None,
                                    Tag =  gridSourcedetails[0].Field17,
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
                                    pb_start.Width = 60;
                                }
                                pb_start.Click += pbstart_Click;

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
                                    pb_ready.Width = 60;
                                }
                                pb_ready.Click += pbreadyfood_Click;

                                PictureBox pb_cancelfood = new PictureBox()
                                {
                                    Name = "pictureBoxRemove_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Image = Properties.Resources.close_price_change,  // Replace "YourRemoveImage" with the name of your resource image
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

                                #region Print Kitchen order base kitchen  Staus Filed 0 or 1
                                if (Convert.ToInt32(gridSourcedetails[0].Field18) == 1)
                                {                       
                                        #region Crystal Report Dclaration
                                        ReportDocument reportDocument = new ReportDocument();
                                        DataSet dsReport = new DataSet();
                                        DataTable dtSalesDetail = new DataTable("DT_kitchenbill");

                                        BllSearch<DmnkitchenBilldetails> bllSearchDetails = new BllSearch<DmnkitchenBilldetails>();

                                        DmnSearch dmnSearchDetails = new DmnSearch();
                                        dmnSearchDetails.Select = "od.itemName ItemName,od.Quantity Quantity,od.Notes Notes,od.MainRemarks Description,ti.TableCode TableCode,om.OrderMasterId OrderMasterId";
                                        dmnSearchDetails.Table = "tblkitchenOrder ko " +
                                            "Left Join tblOrderDetails od  on od.OrderDetailsId=ko.OrderDetailsId " +
                                            "Left Join tblOrderMaster om  on om.OrderMasterId=ko.OrderMasterId " +
                                            "Left Join tblTable_info  ti on ti.Tid=ko.Tid";
                                        dmnSearchDetails.WhereNdGroupBy = "where om.status=1 and ko.OrderMasterId=" + orders.Field1 + " and ko.OrderNo =" + orders.Field2;
                                        dmnSearchDetails.OrderBy = "order by ko.koid DESC";

              
                                        var BillDetails = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetails);

                                        if (BillDetails != null && BillDetails.Count > 0)
                                        {
                                            using (var reader = ObjectReader.Create(BillDetails, "ItemName", "Quantity", "Notes", "Description", "TableCode", "OrderMasterId"))
                                            {
                                                dtSalesDetail.Load(reader);
                                            }
                                            dsReport.Tables.Add(dtSalesDetail);
                                        }

                                        textBox1.Text = RPTPath + @"\crkitchenbill.rpt";
                                        //CMessageBox.Show(RPTPath + @"\crkitchenbill.rpt", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        reportDocument.Load(RPTPath + @"\crkitchenbill.rpt");
                                        //pnl_kitchenorderBill.Visible = true;   

                                        reportDocument.SetDataSource(dsReport);
                                        crvkitchenbill.RefreshReport();
                                        crvkitchenbill.ReportSource = reportDocument;
                                        crvkitchenbill.Focus();
                                        reportDocument.PrintToPrinter(1, false, 0, 0);

                                        for (int k = 0; k < gridSourcedetails.Count; k++)
                                        {
                                            #region Update Prnt Staus Set to2
                                            BllKitchenorder bllkitchen = new BllKitchenorder();
                                            Dmnkitchen dmnkitchen = new Dmnkitchen();
                                            dmnkitchen.Koid = Convert.ToInt32(gridSourcedetails[k].Field8);
                                            dmnkitchen.CreatedBy = FrmDashboard.UserId;
                                            dmnkitchen.KOTPrintStatus = 2;
                                            dmnkitchen.BranchId = 1030;
                                            var InsertStatus = bllkitchen.Kitchen_statusupdate("prd_updateKitchenorderstatus", dmnkitchen);
                                            #endregion
                                        }
                                        #endregion
           
                                }
                                else
                                {

                                }
                                #endregion

                                flp_order_view.Controls.Add(tlp_product_box);
                                tlp_product_box.Focus();

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
                    refreshtimer.Start();
                }
                else
                {
                    timer1.Stop();
                    flp_order_view.Controls.Clear();
                    pnl_empty_msg.Location = new Point(this.ClientSize.Width / 2 - pnl_empty_msg.Size.Width / 2, this.ClientSize.Height / 2 - pnl_empty_msg.Size.Height / 2);
                }

                #endregion

                this.ResumeLayout();
                this.Refresh();
            }
            catch (Exception ex)
            {

            }
        }
        private void ApplyRoundedCorners(TableLayoutPanel panel)
        {
            int cornerRadius = 10; // Adjust the corner radius as needed
            // Override the OnPaint event to create rounded corners
            panel.Paint += (sender, e) =>
            {
                GraphicsPath path = new GraphicsPath();
                path.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90);
                path.AddArc(panel.Width - cornerRadius * 2, 0, cornerRadius * 2, cornerRadius * 2, 270, 90);
                path.AddArc(panel.Width - cornerRadius * 2, panel.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
                path.AddArc(0, panel.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);

                panel.Region = new Region(path);
            };
        }
        private void dgv_product_details_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                       if (e.ColumnIndex == dactionno)
            {
                DataGridView clickedDataGridView = sender as DataGridView;
                if (clickedDataGridView != null && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dstatusno].Value) == 1)
                {
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
                    txt_product_remove_des.Focus();
                }
            }
            else if (e.ColumnIndex == dptypeno)
            {
                if (pnlproductviewmore.Visible)
                    pnlproductviewmore.Visible = false;
                else
                {
                    DataGridView clickedDataGridView = sender as DataGridView;
                    if (clickedDataGridView != null && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dstatusno].Value) == 1 && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dtabletypeno].Value) == Convert.ToInt32(Tabletype.ComboProduct))
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
                            dgv_viewmore.Rows.Clear();
                            for (int k = 0; k < gridSource_offer.Count; k++)
                            {
                                dgv_viewmore.Rows.Add();
                                dgv_viewmore.Rows[k].Cells[0].Value = k + 1;
                                dgv_viewmore.Rows[k].Cells[1].Value = gridSource_offer[k].Field1;
                                dgv_viewmore.Rows[k].Cells[2].Value = gridSource_offer[k].Field2;
                            }
                            pnlproductviewmore.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                            pnlproductviewmore.Visible = true;
                        }              
                    }

                }
 
            }
            }
            catch (Exception ex)
            {

            }
     
        }

        private void pbcancelfood_Click(object sender, EventArgs e)
        {
            try
            {
                PictureBox clickedPictureBox = (PictureBox)sender;
                int id = Convert.ToInt32(clickedPictureBox.Tag);
                dgv_view_name = "dgv_product_details_k_" + clickedPictureBox.AccessibleDescription + id;
                pnl_products_rem_descr.Visible = true;
                int panelWidthPercentage = 20;
                int panelheightPercentage = 20;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                pnl_products_rem_descr.Size = new Size(panelWidth, panelHeight);
                pnl_products_rem_descr.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                pbcancelfoodclciked = true;
                lblheader_product_des.Text = "Order Cancel";
                txt_product_remove_des.Focus();
            }
            catch (Exception ex)
            {

            }

        }
        private void pbstart_Click(object sender, EventArgs e)
        {
            try
            {
                PictureBox clickedPictureBox = (PictureBox)sender;
                int id = Convert.ToInt32(clickedPictureBox.Tag);
                string dgvname = "dgv_product_details_k_"+ clickedPictureBox.AccessibleDescription + id;
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
                    btn_start.Visible = false;
                PictureBox btn_cancelfood = this.Controls.Find(cancelfoodbuttonname, true).FirstOrDefault() as PictureBox;

                DataGridView dgv_product_details = this.Controls.Find(dgvname, true).FirstOrDefault() as DataGridView;
                if (dgv_product_details != null)
                {
                    int tid = 0, orderno = 0, ordermid = 0;
                    for (int i = 0; i < dgv_product_details.RowCount; i++)
                    {
                        if (Convert.ToInt32(dgv_product_details.Rows[i].Cells[dstatusno].Value) == 1)
                        {
                            tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                            orderno = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dorderno].Value);
                            ordermid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dordermasteridno].Value);

                            dmnRemove_order.OrderDetailsId = Convert.ToInt32(dgv_product_details.Rows[i].Cells[ddetailsidno].Value);
                            dmnRemove_order.koid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dkoidno].Value);
                            dmnRemove_order.OrderMasterId = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dordermasteridno].Value);
                            dmnRemove_order.Tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                            decimal originalValue = Convert.ToDecimal(dgv_product_details.Rows[i].Cells[dqtyno].Value);
                            dmnRemove_order.Quantity = (int)Math.Floor(originalValue);
                            dmnRemove_order.Status = 2;
                            dmnRemove_order.RemoveNotes = "";
                            var InsertStatusP = bll_order.Remove_order("prd_remove_KitchenOrder", dmnRemove_order);
                        }
                    }

                    TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                    if (tlp_sele != null)
                    {
                        bool flagstart_ready = false;
                        #region Grid Edit Cancel  product
                        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "Status Field1";
                        dmnSearch.Table = " tblkitchenOrder";
                        dmnSearch.WhereNdGroupBy = "where  OrderNo = " + id + " and OrderMasterId = " + ordermid;
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

                            DataGridViewImageCell cell = (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dactionno];
                            if (Convert.ToInt32(gridSource[k].Field1) == 0)
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
                    if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                    {
                        FrmOrderView _instanceFrmOrderView = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                        _instanceFrmOrderView.Filldetails(tid);
                        _instanceFrmOrderView.FillOrders();
                    }
                }
            }
            catch (Exception ex)
            {

            }
       
            
        }
        
        private void pbreadyfood_Click(object sender, EventArgs e)
        {
            try
            {
                PictureBox clickedPictureBox = (PictureBox)sender;
                int id = Convert.ToInt32(clickedPictureBox.Tag);
                string dgvname = "dgv_product_details_k_" + clickedPictureBox.AccessibleDescription + id;
                DmnOrderMaster dmnRemove_order = new DmnOrderMaster();
                BllOrder bll_order = new BllOrder();

                DataGridView dgv_product_details = this.Controls.Find(dgvname, true).FirstOrDefault() as DataGridView;
                if (dgv_product_details != null)
                {
                    int tid = 0, koid = 0;
                    for (int i = 0; i < dgv_product_details.RowCount; i++)
                    {
                        if (Convert.ToInt32(dgv_product_details.Rows[i].Cells[dstatusno].Value) == 2)
                        {
                            tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                            dmnRemove_order.OrderDetailsId = Convert.ToInt32(dgv_product_details.Rows[i].Cells[ddetailsidno].Value);
                            dmnRemove_order.koid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dkoidno].Value);
                            dmnRemove_order.OrderMasterId = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dordermasteridno].Value);
                            dmnRemove_order.Tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                            decimal originalValue = Convert.ToDecimal(dgv_product_details.Rows[i].Cells[dqtyno].Value);
                            dmnRemove_order.Quantity = (int)Math.Floor(originalValue);
                            dmnRemove_order.Status = 3;
                            dmnRemove_order.RemoveNotes = "";
                            var InsertStatusP = bll_order.Remove_order("prd_remove_KitchenOrder", dmnRemove_order);
                        }
                    }
                    string tlpname = "tlp_product_box_k_" + clickedPictureBox.AccessibleDescription + id;
                    TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                    if (tlp_sele != null && flp_order_view.Controls.Contains(tlp_sele))
                    {
                        flp_order_view.Controls.Remove(tlp_sele);
                        tlp_sele.Dispose();
                    }

                    if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                    {
                        FrmOrderView _instanceFrmOrderView = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                        _instanceFrmOrderView.Filldetails(tid);
                        _instanceFrmOrderView.FillOrders();
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void txt_product_remove_des_Enter(object sender, EventArgs e)
        {
            if (txt_product_remove_des.Text == "Reason type here .....")
            {
                txt_product_remove_des.Text = "";
                txt_product_remove_des.ForeColor = Color.Black;
            }
        }

        private void txt_product_remove_des_Leave(object sender, EventArgs e)
        {
            if (txt_product_remove_des.Text == "")
            {
                txt_product_remove_des.Text = "Reason type here .....";
                txt_product_remove_des.ForeColor = Color.DarkGray;
            }
        }

        private void btn_cancel_product_des_Click(object sender, EventArgs e)
        {
            pnl_products_rem_descr.Visible = false;
        }

        private void btn_close_prod_des_Click(object sender, EventArgs e)
        {
            pnl_products_rem_descr.Visible = false;
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
                            int koid = 0;
                            for (int i = 0; i < dgv_product_details.RowCount; i++)
                            {
                                if(Convert.ToInt32(dgv_product_details.Rows[i].Cells[dstatusno].Value)==1)
                                {
                                    koid= Convert.ToInt32(dgv_product_details.Rows[i].Cells[dkoidno].Value);
                                    tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                                    dmnRemove_order.OrderDetailsId = Convert.ToInt32(dgv_product_details.Rows[i].Cells[ddetailsidno].Value);
                                    dmnRemove_order.koid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dkoidno].Value);
                                    dmnRemove_order.OrderMasterId = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dordermasteridno].Value);
                                    dmnRemove_order.Tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                                    decimal originalValue = Convert.ToDecimal(dgv_product_details.Rows[i].Cells[dqtyno].Value);
                                    dmnRemove_order.Quantity = (int)Math.Floor(originalValue);
                                    dmnRemove_order.Status = 0;
                                    dmnRemove_order.RemoveNotes = txt_product_remove_des.Text;
                                    var InsertStatusP = bll_order.Remove_order("prd_remove_KitchenOrder", dmnRemove_order);
                                }

                            }
                   
                            pbcancelfoodclciked = false;
                            pnl_products_rem_descr.Visible = false;

                            string tlpname = "tlp_product_box_k_" + koid;
                            TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                            if (tlp_sele != null && flp_order_view.Controls.Contains(tlp_sele))
                            {
                                flp_order_view.Controls.Remove(tlp_sele);
                                tlp_sele.Dispose();
                            }
                        }
         
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
                        dmnRemove_order.Quantity = (int)Math.Floor(originalValue);
                        dmnRemove_order.Status = 0;
                        dmnRemove_order.RemoveNotes = txt_product_remove_des.Text;
                        var InsertStatusP = bll_order.Remove_order("prd_remove_Order", dmnRemove_order);

                        DataGridViewImageCell cell = (DataGridViewImageCell)dgv_product_details.Rows[dgv_view_rowno].Cells[dactionno];
                        cell.Value = Properties.Resources.orderview_cancelled;
                        DataGridViewImageCell cell1 = (DataGridViewImageCell)dgv_product_details.Rows[dgv_view_rowno].Cells[dactionno];
                        dgv_product_details.Rows[dgv_view_rowno].Cells[dremnoteno].Value = txt_product_remove_des.Text;
                        pnl_products_rem_descr.Visible = false;
                        txt_product_remove_des.Text = "";
                        Fillorders();
                    }
                    if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                    {
                        FrmOrderView _instanceFrmOrderView = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                        _instanceFrmOrderView.Filldetails(tid);
                        _instanceFrmOrderView.FillOrders();
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
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

                    string timeDifferenceString = $"{hoursDifference} hours, {minutesDifference} minutes, {secondsDifference} seconds";
                    Label lbl_time = Controls.Find(lbltimename, true).FirstOrDefault() as Label;
                    if (lbl_time != null)
                        lbl_time.Text = timeDifferenceString;
                }
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
                    if(txtorderid_search.Text!="")
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
                                        ApplyRoundedCorners(tlp_product_box);
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
                                        dgv_product_details.Name = "dgv_product_details" + gridSourcedetails[0].Field8;

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


                                        dgv_product_details.Columns[dslno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[ditemno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dptypeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                                        dgv_product_details.Columns[dqtyno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dnotes].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dremnoteno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[ddetailsidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dkoidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dtidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dactionno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dordermasteridno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[ddatetimeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dstatusno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[ditemidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dtabletypeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                                        dgv_product_details.Columns[dslno].Visible = false;
                                        dgv_product_details.Columns[ddetailsidno].Visible = false;
                                        dgv_product_details.Columns[dkoidno].Visible = false;
                                        dgv_product_details.Columns[dtidno].Visible = false;
                                        dgv_product_details.Columns[dordermasteridno].Visible = false;
                                        dgv_product_details.Columns[ddatetimeno].Visible = false;
                                        dgv_product_details.Columns[dstatusno].Visible = false;
                                        dgv_product_details.Columns[ditemidno].Visible = false;
                                        dgv_product_details.Columns[dtabletypeno].Visible = false;

                                        dgv_product_details.Rows.Clear();
                                        for (int k = 0; k < gridSourcedetails.Count; k++)
                                        {
                                            if (Convert.ToInt32(gridSourcedetails[k].Field12) == 1)
                                            {
                                                flagstart_ready = true;
                                            }
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
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
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
                        dmnSearch.WhereNdGroupBy = "where ko.status IN(1,2) and ti.TableCode= '" + txttablecode.Text +"'";
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
                                        ApplyRoundedCorners(tlp_product_box);
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
                                        dgv_product_details.Name = "dgv_product_details" + gridSourcedetails[0].Field8;

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


                                        dgv_product_details.Columns[dslno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[ditemno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dptypeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                                        dgv_product_details.Columns[dqtyno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dnotes].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dremnoteno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[ddetailsidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dkoidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dtidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dactionno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dordermasteridno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[ddatetimeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dstatusno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[ditemidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                        dgv_product_details.Columns[dtabletypeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

                                        dgv_product_details.Columns[dslno].Visible = false;
                                        dgv_product_details.Columns[ddetailsidno].Visible = false;
                                        dgv_product_details.Columns[dkoidno].Visible = false;
                                        dgv_product_details.Columns[dtidno].Visible = false;
                                        dgv_product_details.Columns[dordermasteridno].Visible = false;
                                        dgv_product_details.Columns[ddatetimeno].Visible = false;
                                        dgv_product_details.Columns[dstatusno].Visible = false;
                                        dgv_product_details.Columns[ditemidno].Visible = false;
                                        dgv_product_details.Columns[dtabletypeno].Visible = false;

                                        dgv_product_details.Rows.Clear();
                                        for (int k = 0; k < gridSourcedetails.Count; k++)
                                        {
                                            if (Convert.ToInt32(gridSourcedetails[k].Field12) == 1)
                                            {
                                                flagstart_ready = true;
                                            }
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
            txttablecode.Text = "Table Code....";
            txttablecode.ForeColor = Color.DarkGray;
            txtorderid_search.Text = "Order Id ....";
            Fillorders();            
        }

        private void pnl_products_rem_descr_Paint(object sender, PaintEventArgs e)
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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {     
                case Keys.Escape:                    
                    if (pnl_products_rem_descr.Visible)
                        pnl_products_rem_descr.Visible = false;
                    else if (pnlproductviewmore.Visible)
                        pnlproductviewmore.Visible = false;
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
            pnlproductviewmore.Visible = false;
        }
    }
}
