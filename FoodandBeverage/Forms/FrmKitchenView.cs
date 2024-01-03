using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Interfaces;
using Microsoft.AspNet.SignalR.Client;

namespace FoodandBeverage.Forms
{
    public partial class FrmKitchenView : Form
    {
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

        private void FrmKitchenView_Load(object sender, EventArgs e)
        {
            txtorderid_search.Text = "Order Id ....";
            txttablecode.Text = "Table Code....";
            textb_shape();
            Fillorders();

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
                #region Fill Orders in Datagrid

                txt_product_remove_des.Text = "Reason type here .....";

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "OrderMasterId Field1,OrderNo Field2";
                dmnSearch.Table = " tblkitchenOrder ";
                dmnSearch.WhereNdGroupBy = "where status in(1,2)";
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
                        dmnSearch.Select = "om.OrderMasterId Field1,ti.TableCode Field2,om.status Field3,od.itemName Field4,od.Quantity Field5,od.Notes Field6,od.OrderNo Field7,od.CreatedDateTime Fielddatetime,ko.koid Field8,ko.OrderDetailsId Field9,om.Tid Field10,ko.RemoveNotes Field11,ko.Status Field12,od.tabletype Field13";
                        dmnSearch.Table = "tblkitchenOrder ko " +
                            "Left Join tblOrderDetails od  on od.OrderDetailsId=ko.OrderDetailsId " +
                            "Left Join tblOrderMaster om  on om.OrderMasterId=ko.OrderMasterId " +
                            "Left Join tblTable_info  ti on ti.Tid=om.Tid";
                        dmnSearch.WhereNdGroupBy = "where om.status=1 and ko.OrderMasterId=" + orders.Field1 + " and ko.OrderNo =" + orders.Field2;
                        dmnSearch.OrderBy = "order by ko.koid DESC";
                        var gridSourcedetails = bllSearch_details.GetSearchData("prdSearch", dmnSearch);
                        if(gridSourcedetails.Count>0)
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
                            if (screenWidth > 1367 && screenHeight > 769)
                            {
                                tlp_product_box.Width = 600;
                                tlp_product_box.Height = 400;
                            }
                            else
                            {
                                tlp_product_box.Width = 300;
                                tlp_product_box.Height = 300;
                            }

                            tlp_product_box.BackColor = boxcolor;
                            tlp_product_box.RowCount = 3;
                            //tlp_product_box.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                            tlp_product_box.Margin = new Padding(20, 10, 0, 0);
                            tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                            tlp_product_box.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // 50% height
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
                            DataGridViewTextBoxColumn columnptype = new DataGridViewTextBoxColumn();
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


                            dgv_product_details.Columns[dslno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dgv_product_details.Columns[ditemno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dgv_product_details.Columns[dptypeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dgv_product_details.Columns[dqtyno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dgv_product_details.Columns[dnotes].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            dgv_product_details.Columns[dremnoteno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dgv_product_details.Columns[ddetailsidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dgv_product_details.Columns[dkoidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dgv_product_details.Columns[dtidno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dgv_product_details.Columns[dactionno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dgv_product_details.Columns[dordermasteridno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells; 
                            dgv_product_details.Columns[ddatetimeno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            dgv_product_details.Columns[dstatusno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                            dgv_product_details.Columns[dslno].Visible = false;
                            dgv_product_details.Columns[ddetailsidno].Visible = false;
                            dgv_product_details.Columns[dkoidno].Visible = false;
                            dgv_product_details.Columns[dtidno].Visible = false;
                            dgv_product_details.Columns[dordermasteridno].Visible = false;
                            dgv_product_details.Columns[ddatetimeno].Visible = false;
                            dgv_product_details.Columns[dstatusno].Visible = false;              

                            pbremove.Image = Properties.Resources.close_price_change;
                            pbremove.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            pbremove.Width = 30;      

                            dgv_product_details.Rows.Clear();
                            for (int k = 0; k < gridSourcedetails.Count; k++)
                            {
                                dgv_product_details.Rows.Add();
                                dgv_product_details.Rows[k].Cells[dslno].Value = k + 1;
                                dgv_product_details.Rows[k].Cells[ditemno].Value = gridSourcedetails[k].Field4;

                                if (Convert.ToInt32(gridSourcedetails[k].Field13) == 3)
                                {
                                    dgv_product_details.Rows[k].Cells[dptypeno].Value = "COMBO";
                                    dgv_product_details.Rows[k].Cells[dptypeno].Style.ForeColor = Color.DarkBlue;
                                    dgv_product_details.Rows[k].Cells[dptypeno].Style.Font = new Font(dgv_product_details.Font, FontStyle.Bold);
                                }
                                else
                                {
                                    dgv_product_details.Rows[k].Cells[dptypeno].Value = "";
                                    dgv_product_details.Rows[k].Cells[dptypeno].Style.ForeColor = dgv_product_details.DefaultCellStyle.ForeColor;
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

                            if(Convert.ToInt32(gridSourcedetails[0].Field12)==1)
                            {
                                pb_start.Visible = true;
                                pb_ready.Visible = false;
                            }
                            else if (Convert.ToInt32(gridSourcedetails[0].Field12) == 2)
                            {
                                pb_start.Visible = false;
                                pb_ready.Visible = true;
                            }

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

                            tlp_buttons.Controls.Add(pb_start, 0, 0);
                            tlp_buttons.Controls.Add(pb_ready, 1, 0);
                            tlp_buttons.Controls.Add(pb_cancelfood, 2, 0);

                            // Adding Buttons TableLayoutPanel to the last row
                            tlp_product_box.Controls.Add(tlp_buttons, 0, 2);
                            #endregion

                            flp_order_view.Controls.Add(tlp_product_box);

                            orderbox.Add(new datagrid
                            {
                                Id =Convert.ToInt32(gridSourcedetails[0].Field8),
                                datagridviewname = dgv_product_details.Name,
                                createtime= gridSourcedetails[0].Fielddatetime
                            });
                        }
                
                    }
                    flp_order_view.Visible = true;
                    timer1.Start();

                }
                else
                {
                    timer1.Stop();
                    flp_order_view.Controls.Clear();
                    pnl_empty_msg.Location = new Point(this.ClientSize.Width / 2 - pnl_empty_msg.Size.Width / 2, this.ClientSize.Height / 2 - pnl_empty_msg.Size.Height / 2);
                }

                #endregion
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
            if (e.ColumnIndex == dactionno)
            {
                if (screenWidth > 1367 && screenHeight > 769)
                {
                    pnl_products_rem_descr.Height = 304;
                    pnl_products_rem_descr.Width = 155;
                }
                else
                {
                    pnl_products_rem_descr.Height = 600;
                    pnl_products_rem_descr.Width = 300;
                }
                DataGridView clickedDataGridView = sender as DataGridView;
                if (clickedDataGridView != null && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dstatusno].Value)==1 )
                {
                    dgv_view_name = clickedDataGridView.Name;
                    dgv_view_rowno = e.RowIndex;
                    pnl_products_rem_descr.Visible = true;
                    pnl_products_rem_descr.Location = new Point(this.ClientSize.Width / 2 - pnl_products_rem_descr.Size.Width / 2, this.ClientSize.Height / 2 - pnl_products_rem_descr.Size.Height / 2);
                    lblheader_product_des.Text =Convert.ToString(clickedDataGridView.Rows[e.RowIndex].Cells[ditemno].Value);
                    txt_product_remove_des.Focus();
                }
            }
        }
        private void pbcancelfood_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = (PictureBox)sender;
            int id = Convert.ToInt32(clickedPictureBox.Tag);
            dgv_view_name = "dgv_product_details" + id;
            pnl_products_rem_descr.Visible = true;
            pnl_products_rem_descr.Location = new Point(this.ClientSize.Width / 2 - pnl_products_rem_descr.Size.Width / 2, this.ClientSize.Height / 2 - pnl_products_rem_descr.Size.Height / 2);
            pbcancelfoodclciked = true;
            lblheader_product_des.Text = "Order Cancel";
            txt_product_remove_des.Focus();
        }
        private void pbstart_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = (PictureBox)sender;
            int id = Convert.ToInt32(clickedPictureBox.Tag);
            string dgvname = "dgv_product_details" + id;
            string startbuttonname = "pb_start" + id;
            string readybuttonname = "pb_ready" + id;
            DmnOrderMaster dmnRemove_order = new DmnOrderMaster();
            BllOrder bll_order = new BllOrder();
            PictureBox btn_ready= this.Controls.Find(readybuttonname, true).FirstOrDefault() as PictureBox;
            if (btn_ready != null)
                btn_ready.Visible = true;
            PictureBox btn_start = this.Controls.Find(startbuttonname, true).FirstOrDefault() as PictureBox;
            if (btn_start != null)
                btn_start.Visible = false;

            DataGridView dgv_product_details = this.Controls.Find(dgvname, true).FirstOrDefault() as DataGridView;
            if (dgv_product_details != null)
            {
                int tid = 0;
                for (int i = 0; i < dgv_product_details.RowCount; i++)
                {
                    if (Convert.ToInt32(dgv_product_details.Rows[i].Cells[dstatusno].Value) == 1)
                    {
                        tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
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
                Fillorders();
                if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                {
                    FrmOrderView _instanceFrmOrderView = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                    _instanceFrmOrderView.Filldetails(tid);
                    _instanceFrmOrderView.FillOrders();
                }
            }
        }
        
        private void pbreadyfood_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = (PictureBox)sender;
            int id = Convert.ToInt32(clickedPictureBox.Tag);
            string dgvname = "dgv_product_details" + id;
            DmnOrderMaster dmnRemove_order = new DmnOrderMaster();
            BllOrder bll_order = new BllOrder();

            DataGridView dgv_product_details = this.Controls.Find(dgvname, true).FirstOrDefault() as DataGridView;
            if (dgv_product_details != null)
            {
                int tid = 0;
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
                Fillorders();
                if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                {
                    FrmOrderView _instanceFrmOrderView = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                    _instanceFrmOrderView.Filldetails(tid);
                    _instanceFrmOrderView.FillOrders();
                }
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
   
                            for (int i = 0; i < dgv_product_details.RowCount; i++)
                            {
                                if(Convert.ToInt32(dgv_product_details.Rows[i].Cells[dstatusno].Value)==1)
                                {
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
                        }
                        Fillorders();
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
                int id = dataGridViewInfo.Id;
                string dataGridViewName = dataGridViewInfo.datagridviewname;
                string lbltimename = "lbl_time" + id;
                DataGridView dgv = Controls.Find(dataGridViewName, true).FirstOrDefault() as DataGridView;
                if (dgv != null)
                {
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
                        dmnSearch.WhereNdGroupBy = "where ko.status IN(1,2) and ko.OrderMasterId= "+ txt_product_remove_des.Text ;
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
                                    #region Main TableLayoutPanel 
                                    // Main TableLayoutPanel for each product
                                    TableLayoutPanel tlp_product_box = new TableLayoutPanel();
                                    tlp_product_box.Name = "tlp_product_box" + gridSourcedetails[0].Field8;
                                    tlp_product_box.Dock = DockStyle.Top;
                                    tlp_product_box.Width = 300;
                                    tlp_product_box.Height = 300;
                                    tlp_product_box.BackColor = boxcolor;
                                    tlp_product_box.RowCount = 3;
                                    //tlp_product_box.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                                    tlp_product_box.Margin = new Padding(20, 10, 0, 0);
                                    tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                    tlp_product_box.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // 50% height
                                    tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                    ApplyRoundedCorners(tlp_product_box);
                                    #endregion

                                    #region Heading Time and Tablecode Frist Row
                                    // Sub TableLayoutPanel for Heading
                                    TableLayoutPanel tlp_product_heading = new TableLayoutPanel();
                                    tlp_product_heading.Name = "tlp_product_heading" + gridSourcedetails[0].Field8;
                                    tlp_product_heading.Dock = DockStyle.Top;
                                    tlp_product_heading.Height = 20;
                                    tlp_product_heading.ColumnCount = 2;
                                    tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                                    tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                                    // Labels in Heading
                                    Label lbl_tablecode = new Label()
                                    {
                                        Text = gridSourcedetails[0].Field2,
                                        Name = "lbl_tablecode" + gridSourcedetails[0].Field8,
                                        Dock = DockStyle.Fill,
                                        ForeColor = Color.White,
                                        Font = new Font("Arial", 10, FontStyle.Bold)
                                    };

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
                                        Font = new Font("Arial", 8, FontStyle.Bold),

                                    };

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

                                    dgv_product_details.ColumnHeadersHeight = 30;
                                    dgv_product_details.RowTemplate.Height = 30;

                                    dgv_product_details.RowHeadersVisible = false;
                                    // Add columns with specific cell type
                                    DataGridViewTextBoxColumn columnSlno = new DataGridViewTextBoxColumn();
                                    DataGridViewImageColumn pbremove = new DataGridViewImageColumn();
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

                                    dgv_product_details.Columns.Add(columnSlno);
                                    dgv_product_details.Columns.Add(pbremove);
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

                                    dgv_product_details.Columns[dslno].HeaderText = "Slno";
                                    dgv_product_details.Columns[ditemno].HeaderText = "Item Name";
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


                                    dgv_product_details.Columns[dslno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                    dgv_product_details.Columns[ditemno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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

                                    dgv_product_details.Columns[dstatusno].Frozen = true;

                                    dgv_product_details.Columns[dslno].Visible = false;
                                    dgv_product_details.Columns[ddetailsidno].Visible = false;
                                    dgv_product_details.Columns[dkoidno].Visible = false;
                                    dgv_product_details.Columns[dtidno].Visible = false;
                                    dgv_product_details.Columns[dordermasteridno].Visible = false;
                                    dgv_product_details.Columns[ddatetimeno].Visible = false;
                                    dgv_product_details.Columns[dstatusno].Visible = false;

                                    pbremove.Image = Properties.Resources.close_price_change;
                                    pbremove.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                    pbremove.Width = 30;

                                    dgv_product_details.Rows.Clear();
                                    for (int k = 0; k < gridSourcedetails.Count; k++)
                                    {
                                        dgv_product_details.Rows.Add();
                                        dgv_product_details.Rows[k].Cells[dslno].Value = k + 1;
                                        dgv_product_details.Rows[k].Cells[ditemno].Value = gridSourcedetails[k].Field4;

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

                                    #region Ready and Close Button Thrid Row

                                    // Sub TableLayoutPanel for Buttons
                                    TableLayoutPanel tlp_buttons = new TableLayoutPanel();
                                    tlp_buttons.Name = "tlp_buttons" + gridSourcedetails[0].Field8;
                                    tlp_buttons.Dock = DockStyle.Top;
                                    tlp_buttons.Height = 30;
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
                                        Height = 30,
                                        Width = 60,
                                        Tag = gridSourcedetails[0].Field8,
                                    };
                                    pb_start.Click += pbstart_Click;

                                    PictureBox pb_ready = new PictureBox()
                                    {
                                        Name = "pb_ready" + gridSourcedetails[0].Field8,
                                        Image = Properties.Resources.ready_button,
                                        SizeMode = PictureBoxSizeMode.StretchImage,
                                        Dock = DockStyle.None,
                                        Height = 30,
                                        Width = 60,
                                        Tag = gridSourcedetails[0].Field8,
                                        Visible = false,
                                    };
                                    pb_ready.Click += pbreadyfood_Click;

                                    PictureBox pb_cancelfood = new PictureBox()
                                    {
                                        Name = "pictureBoxRemove" + gridSourcedetails[0].Field8,
                                        Image = Properties.Resources.close_price_change,  // Replace "YourRemoveImage" with the name of your resource image
                                        SizeMode = PictureBoxSizeMode.StretchImage,
                                        Dock = DockStyle.None,
                                        Height = 30,
                                        Width = 60,
                                        Tag = gridSourcedetails[0].Field8,
                                    };
                                    pb_cancelfood.Click += pbcancelfood_Click;

                                    tlp_buttons.Controls.Add(pb_start, 0, 0);
                                    tlp_buttons.Controls.Add(pb_ready, 1, 0);
                                    tlp_buttons.Controls.Add(pb_cancelfood, 2, 0);

                                    // Adding Buttons TableLayoutPanel to the last row
                                    tlp_product_box.Controls.Add(tlp_buttons, 0, 2);
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
                        dmnSearch.Table = " tblkitchenOrder ko  " +
                            "LEFT JOIN tblOrderMaster om on om.OrderMasterId=ko.OrderMasterId " +
                            "LEFT JOIN tblTable_info ti on ti.Tid=om.Tid";
                        dmnSearch.WhereNdGroupBy = "where ko.status IN(1,2) and ti.TableCode = '" + txttablecode.Text + "'";
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
                                    #region Main TableLayoutPanel 
                                    // Main TableLayoutPanel for each product
                                    TableLayoutPanel tlp_product_box = new TableLayoutPanel();
                                    tlp_product_box.Name = "tlp_product_box" + gridSourcedetails[0].Field8;
                                    tlp_product_box.Dock = DockStyle.Top;
                                    tlp_product_box.Width = 300;
                                    tlp_product_box.Height = 300;
                                    tlp_product_box.BackColor = boxcolor;
                                    tlp_product_box.RowCount = 3;
                                    //tlp_product_box.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                                    tlp_product_box.Margin = new Padding(20, 10, 0, 0);
                                    tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                    tlp_product_box.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // 50% height
                                    tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                    ApplyRoundedCorners(tlp_product_box);
                                    #endregion

                                    #region Heading Time and Tablecode Frist Row
                                    // Sub TableLayoutPanel for Heading
                                    TableLayoutPanel tlp_product_heading = new TableLayoutPanel();
                                    tlp_product_heading.Name = "tlp_product_heading" + gridSourcedetails[0].Field8;
                                    tlp_product_heading.Dock = DockStyle.Top;
                                    tlp_product_heading.Height = 20;
                                    tlp_product_heading.ColumnCount = 2;
                                    tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                                    tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                                    // Labels in Heading
                                    Label lbl_tablecode = new Label()
                                    {
                                        Text = gridSourcedetails[0].Field2,
                                        Name = "lbl_tablecode" + gridSourcedetails[0].Field8,
                                        Dock = DockStyle.Fill,
                                        ForeColor = Color.White,
                                        Font = new Font("Arial", 10, FontStyle.Bold)
                                    };

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
                                        Font = new Font("Arial", 8, FontStyle.Bold),

                                    };

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

                                    dgv_product_details.ColumnHeadersHeight = 30;
                                    dgv_product_details.RowTemplate.Height = 30;

                                    dgv_product_details.RowHeadersVisible = false;
                                    // Add columns with specific cell type
                                    DataGridViewTextBoxColumn columnSlno = new DataGridViewTextBoxColumn();
                                    DataGridViewImageColumn pbremove = new DataGridViewImageColumn();
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

                                    dgv_product_details.Columns.Add(columnSlno);
                                    dgv_product_details.Columns.Add(pbremove);
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

                                    dgv_product_details.Columns[dslno].HeaderText = "Slno";
                                    dgv_product_details.Columns[ditemno].HeaderText = "Item Name";
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


                                    dgv_product_details.Columns[dslno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                                    dgv_product_details.Columns[ditemno].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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

                                    dgv_product_details.Columns[dstatusno].Frozen = true;

                                    dgv_product_details.Columns[dslno].Visible = false;
                                    dgv_product_details.Columns[ddetailsidno].Visible = false;
                                    dgv_product_details.Columns[dkoidno].Visible = false;
                                    dgv_product_details.Columns[dtidno].Visible = false;
                                    dgv_product_details.Columns[dordermasteridno].Visible = false;
                                    dgv_product_details.Columns[ddatetimeno].Visible = false;
                                    dgv_product_details.Columns[dstatusno].Visible = false;

                                    pbremove.Image = Properties.Resources.close_price_change;
                                    pbremove.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                    pbremove.Width = 30;

                                    dgv_product_details.Rows.Clear();
                                    for (int k = 0; k < gridSourcedetails.Count; k++)
                                    {
                                        dgv_product_details.Rows.Add();
                                        dgv_product_details.Rows[k].Cells[dslno].Value = k + 1;
                                        dgv_product_details.Rows[k].Cells[ditemno].Value = gridSourcedetails[k].Field4;

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

                                    #region Ready and Close Button Thrid Row

                                    // Sub TableLayoutPanel for Buttons
                                    TableLayoutPanel tlp_buttons = new TableLayoutPanel();
                                    tlp_buttons.Name = "tlp_buttons" + gridSourcedetails[0].Field8;
                                    tlp_buttons.Dock = DockStyle.Top;
                                    tlp_buttons.Height = 30;
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
                                        Height = 30,
                                        Width = 60,
                                        Tag = gridSourcedetails[0].Field8,
                                    };
                                    pb_start.Click += pbstart_Click;

                                    PictureBox pb_ready = new PictureBox()
                                    {
                                        Name = "pb_ready" + gridSourcedetails[0].Field8,
                                        Image = Properties.Resources.ready_button,
                                        SizeMode = PictureBoxSizeMode.StretchImage,
                                        Dock = DockStyle.None,
                                        Height = 30,
                                        Width = 60,
                                        Tag = gridSourcedetails[0].Field8,
                                        Visible = false,
                                    };
                                    pb_ready.Click += pbreadyfood_Click;

                                    PictureBox pb_cancelfood = new PictureBox()
                                    {
                                        Name = "pictureBoxRemove" + gridSourcedetails[0].Field8,
                                        Image = Properties.Resources.close_price_change,  // Replace "YourRemoveImage" with the name of your resource image
                                        SizeMode = PictureBoxSizeMode.StretchImage,
                                        Dock = DockStyle.None,
                                        Height = 30,
                                        Width = 60,
                                        Tag = gridSourcedetails[0].Field8,
                                    };
                                    pb_cancelfood.Click += pbcancelfood_Click;

                                    tlp_buttons.Controls.Add(pb_start, 0, 0);
                                    tlp_buttons.Controls.Add(pb_ready, 1, 0);
                                    tlp_buttons.Controls.Add(pb_cancelfood, 2, 0);

                                    // Adding Buttons TableLayoutPanel to the last row
                                    tlp_product_box.Controls.Add(tlp_buttons, 0, 2);
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
                    FrmDashboard _instancedash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                    _instancedash.Closeform();
                    return true;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
    }
}
