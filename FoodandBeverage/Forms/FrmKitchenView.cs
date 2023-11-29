using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using BLL;
using CustomMessageBox;
using Domain;

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
        public static int ditemno = 1;
        public static int dqtyno = 2;
        public static int dnotes = 3;
        public static int dremnoteno = 4;
        public static int ddetailsidno = 5;
        public static int dkoidno = 6;
        public static int dtidno = 7;
        public static int dactionno = 8;
        public static int dordermasteridno = 9;
        public static int ddatetimeno = 10;
        public static int dstatusno = 11;
        #endregion

        Color boxcolor = Color.FromArgb(0x42, 0x67, 0xB2);
        public int dgv_view_rowno = 0;
        public string dgv_view_name = "";
        public List<datagrid> orderbox = new List<datagrid>();
        public bool pbcancelfoodclciked = false;

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
                foreach (TextBox textBox in new TextBox[] { txtorderid_search })
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
                dmnSearch.WhereNdGroupBy = "where status=1 or status=2";
                dmnSearch.OrderBy = "";
                var gridSourcemaster = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSourcemaster.Count > 0)
                {
                    flp_order_view.Visible = false;
                    var distinctOrders = gridSourcemaster
                       .GroupBy(order => new { order.Field1, order.Field2 })
                       .Select(group => group.First())
                       .OrderBy(order => order.Field1)
                       .ThenBy(order => order.Field2)
                       .ToList();            

                    flp_order_view.Controls.Clear();
                    pnl_dispaly_msg.Visible = false;
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
                        if(gridSourcedetails.Count>0)
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
                            tlp_product_box.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                            tlp_product_box.Margin = new Padding(20, 10, 0, 0);
                            tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                            tlp_product_box.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // 50% height
                            tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
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
                            string timeDifferenceString = $"{hoursDifference} hours, {minutesDifference} minutes, {secondsDifference} seconds";

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
                            dgv_product_details.Dock = DockStyle.Fill;

                            dgv_product_details.ColumnHeadersHeight = 30;
                            dgv_product_details.RowTemplate.Height = 30;

                            dgv_product_details.RowHeadersVisible = false;
                            // Add columns with specific cell type
                            DataGridViewTextBoxColumn columnSlno = new DataGridViewTextBoxColumn();
                            DataGridViewTextBoxColumn columnItemName = new DataGridViewTextBoxColumn();
                            DataGridViewTextBoxColumn columnQty = new DataGridViewTextBoxColumn();
                            DataGridViewTextBoxColumn columnNotes = new DataGridViewTextBoxColumn();
                            DataGridViewTextBoxColumn columnRemNotes = new DataGridViewTextBoxColumn();
                            DataGridViewImageColumn pbremove = new DataGridViewImageColumn();
                            DataGridViewTextBoxColumn columnorderdetailsid = new DataGridViewTextBoxColumn();
                            DataGridViewTextBoxColumn columnkoid = new DataGridViewTextBoxColumn();
                            DataGridViewTextBoxColumn columntid = new DataGridViewTextBoxColumn();
                            DataGridViewTextBoxColumn columnmasterid = new DataGridViewTextBoxColumn();
                            DataGridViewTextBoxColumn columndate = new DataGridViewTextBoxColumn();
                            DataGridViewTextBoxColumn columnstatus = new DataGridViewTextBoxColumn();

                            dgv_product_details.Columns.Add(columnSlno);
                            dgv_product_details.Columns.Add(columnItemName);
                            dgv_product_details.Columns.Add(columnQty);
                            dgv_product_details.Columns.Add(columnNotes);
                            dgv_product_details.Columns.Add(columnRemNotes);
                            dgv_product_details.Columns.Add(columnorderdetailsid);
                            dgv_product_details.Columns.Add(columnkoid);
                            dgv_product_details.Columns.Add(columntid);
                            dgv_product_details.Columns.Add(pbremove);
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
                            for (int i = 0; i < dgv_product_details.Columns.Count; i++)
                            {
                                dgv_product_details.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            }
                            // Adding DataGridView to the second row
                            tlp_product_box.Controls.Add(dgv_product_details, 0, 1);
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
                    flp_order_view.Controls.Add(pnl_dispaly_msg);
                    pnl_dispaly_msg.Visible = true;
                }
                
                #endregion
            }
            catch (Exception ex)
            {

            }
        }

        private void dgv_product_details_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
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
                txt_product_remove_des.ForeColor = Color.Black;
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
                    if (pbcancelfoodclciked)
                    {
                        DataGridView dgv_product_details = this.Controls.Find(dgv_view_name, true).FirstOrDefault() as DataGridView;
                        if (dgv_product_details != null)
                        {
                            int tid = 0;
                            for (int i = 0; i < dgv_product_details.RowCount; i++)
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
                            if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                            {
                                FrmOrderView _instanceFrmOrderView = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                                _instanceFrmOrderView.Filldetails(tid);
                                _instanceFrmOrderView.FillOrders();
                            }
                            pbcancelfoodclciked = false;
                            pnl_products_rem_descr.Visible = false;
                        }
                        Fillorders();
                    }
                    else
                    {   
                        DataGridView dgv_product_details = this.Controls.Find(dgv_view_name, true).FirstOrDefault() as DataGridView;
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
}
}
