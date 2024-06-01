using BLL;
using CustomMessageBox;
using DAL;
using Domain;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmOrderView : Form
    {
        #region Declaration Grid
        public static int cslno = 0;
        public static int orderdatetimeno = 1;
        public static int orderidno = 2;
        public static int tablecodeno = 3;
        public static int totalqtyno = 4;
        public static int statusno = 5;
        public static int descno = 6;
        public static int statusvalueno = 7;


        public static int dslno = 0;
        public static int ditemno = 1;
        public static int dqtyno = 2;
        public static int dstatusno = 3;
        public static int dactionno = 4;
        public static int ddetailsidno = 5;
        public static int ddatetimeno = 6;
        public static int dnotesno = 7;
        public static int dkoidno = 8;
        public static int dstatusvalueno = 9;
        public static int dordermasteridno = 10;
        public static int dtableidno = 11;
        public static int daddonstatusno = 12;
        public static int davidno = 13;
        public static int daddccodenono = 14;
        public static int daddoonpriceno = 15;
        public static int dorderno = 16;

        #endregion
        public int dgv_details_view_rowno = 0;
        public int OrderMasterId = 0;
        public int SearchType = 0;
        public bool Total_Order_Flag = false;
        public bool PermissionFlag = false;
        List<DmnComboBox> comboBoxItems = new List<DmnComboBox>();

        private List<DmnGridFields> pagedata;
        private List<DmnGridFields> Resultdata;
        int currentPage = 1;
        int pageSize = 20;
        int totalPages = 0;

        public FrmOrderView()
        {
            InitializeComponent();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            refreshtimer_main.Stop();
            refreshtimer_sub.Stop();
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txttablecode_serach_Enter(object sender, EventArgs e)
        {
            if (txttablecode_serach.Text == "Table Code .....")
            {
                txttablecode_serach.Text = "";
                txttablecode_serach.ForeColor = Color.Black;
            }
            txtorderid_search.Text = "Order ID .....";
        }

        private void txtorderid_search_Enter(object sender, EventArgs e)
        {
            if (txtorderid_search.Text == "Order ID .....")
            {
                txtorderid_search.Text = "";
                txtorderid_search.ForeColor = Color.Black;
            }
            txttablecode_serach.Text = "Table Code .....";
        }

        private void txtorderid_search_Leave(object sender, EventArgs e)
        {
            if (txtorderid_search.Text == "")
            {
                txtorderid_search.Text = "Order ID .....";
                txtorderid_search.ForeColor = Color.DarkGray;
            }
        }

        private void txttablecode_serach_Leave(object sender, EventArgs e)
        {
            if (txttablecode_serach.Text == "")
            {
                txttablecode_serach.Text = "Table Code .....";
                txttablecode_serach.ForeColor = Color.DarkGray;
            }
        }

        private void FrmOrderView_Load(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                {
                    FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                    if (_instanceFrmdash.ChkWritePermission(Convert.ToInt32(formsenum.OrderView)))                    
                        PermissionFlag = true;                    
                    else                    
                        PermissionFlag = false;                    

                }
                dgvorderview.BorderStyle = BorderStyle.None;
                dgvorderview.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgvorderview.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvorderview.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgvorderview.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgvorderview.BackgroundColor = Color.White;

                dgvorderview.EnableHeadersVisualStyles = false;
                dgvorderview.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvorderview.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgvorderview.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                dgv_details_view.BorderStyle = BorderStyle.None;
                dgv_details_view.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgv_details_view.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgv_details_view.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgv_details_view.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgv_details_view.BackgroundColor = Color.White;

                dgv_details_view.EnableHeadersVisualStyles = false;
                dgv_details_view.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgv_details_view.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgv_details_view.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                txttablecode_serach.Text = "Table Code .....";
                txtorderid_search.Text = "Order ID .....";
                txt_product_remove_des.Text = "Reason type here .....";
                lblorderheading.Text = "Active Orders";
                refreshtimer_main.Start();
            }
            catch (Exception ex)
            {
            }
        }
        private List<DmnGridFields> GetDataorders()
        {
            refreshtimer_main.Stop();
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = " om.OrderMasterId Field1,ti.TableCode Field2,om.TotalQuantity Field3,om.status Field4 ," +
                "(SELECT CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 0 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Result FROM tblOrderDetails WHERE OrderMasterId =  om.OrderMasterId ) Field5,om.CreatedDateTime Field6,om.BookmasterId Field7,ti.Tid Field8,om.RemoveNotes Field9,om.RemoveNotes Field9";
            dmnSearch.Table = "tblOrderMaster om Left Join tblTable_info  ti on ti.Tid=om.Tid";//Left Join tblOrderDetails od on od.OrderMasterId=om.OrderMasterId

            if (SearchType == 4)
            {
                dmnSearch.WhereNdGroupBy = "where om.status in (1,2)";
                dmnSearch.OrderBy = "order by om.OrderMasterId DESC";
            }
                else if (SearchType == Convert.ToInt32(Food_Status.Pending))
            {
                dmnSearch.WhereNdGroupBy = "where om.status=1";
                dmnSearch.OrderBy = "order by om.OrderMasterId DESC";
            }
            else if (SearchType == Convert.ToInt32(Food_Status.FoodProcessing))
            {
                dmnSearch.WhereNdGroupBy = "where om.status=2";
                dmnSearch.OrderBy = "order by om.OrderMasterId DESC";
            }
            else if (SearchType == Convert.ToInt32(Food_Status.Complete))
            {
                dmnSearch.WhereNdGroupBy = "where om.status=3";
                dmnSearch.OrderBy = "order by om.OrderMasterId DESC";
            }
            else if (SearchType == Convert.ToInt32(Food_Status.Cancel))
            {
                dmnSearch.WhereNdGroupBy = "where om.status=0";
                dmnSearch.OrderBy = "order by om.OrderMasterId DESC";
            }

            var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
            Resultdata = gridSource;
            totalPages = (int)Math.Ceiling((double)Resultdata.Count / pageSize);
            pagedata = Resultdata.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            if (currentPage == 1)
                btnprevious.Enabled = false;
            else
                btnprevious.Enabled = true;

            if (currentPage == totalPages)
                btnnext.Enabled = false;
            else
                btnnext.Enabled = true;

            return Resultdata;
        }
        public void FillOrders()
        {
            try
            {
                var gridSource = pagedata;
                dgvorderview.Rows.Clear();
                if (gridSource.Count == 0)
                {
                    btnprevious.Visible = false;
                    btnnext.Visible = false;
                }
                else
                {
                    btnprevious.Visible = true;
                    btnnext.Visible = true;
                }
                #region Fill Orders in Datagrid
                int rowIndex = 0;

                    if (gridSource.Count > 0)
                    {
                        dgvorderview.SuspendLayout();
                        int imageSize = 32;
                        dgvorderview.Rows.Add(gridSource.Count);
                        int x = dgvorderview.RowCount;
                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            dgvorderview.Rows[rowIndex].Cells[cslno].Value = rowIndex + 1;
                            dgvorderview.Rows[rowIndex].Cells[orderidno].Value = gridSource[i].Field1;

                            if (Convert.ToInt32(gridSource[i].Field7) > 0)
                            {
                                FillTablecode_Comb(rowIndex, Convert.ToInt32(gridSource[i].Field7));
                            }
                            else
                            {
                                DataGridViewComboBoxCell dropDown = (DataGridViewComboBoxCell)dgvorderview.Rows[rowIndex].Cells[tablecodeno];
                                List<ComboBoxItem> comboBoxItems = new List<ComboBoxItem>();
                                ComboBoxItem comboBoxItem = new ComboBoxItem(Convert.ToInt32(gridSource[i].Field8), gridSource[i].Field2);
                                comboBoxItems.Add(comboBoxItem);
                                dropDown.DataSource = comboBoxItems;
                                dropDown.ValueMember = "Id";
                                dropDown.DisplayMember = "Value";
                                dropDown.Value = comboBoxItems[0].Id;
                                dropDown.ReadOnly = true;
                            }

                            dgvorderview.Rows[rowIndex].Cells[totalqtyno].Value = gridSource[i].Field3;
                            dgvorderview.Rows[rowIndex].Cells[statusvalueno].Value = gridSource[i].Field4;
                            dgvorderview.Rows[rowIndex].Cells[descno].Value = gridSource[i].Field9;
                            dgvorderview.Rows[rowIndex].Cells[orderdatetimeno].Value = gridSource[i].Field6;      

                            // Set the image for the "statusno" column
                            if (Convert.ToInt32(gridSource[i].Field4) == 0 || Convert.ToInt32(gridSource[i].Field5) == 0)
                            {
                                DataGridViewImageCell cell = (DataGridViewImageCell)dgvorderview.Rows[rowIndex].Cells[statusno];
                                cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                                cell.Value = Properties.Resources.orderview_cancelled;//ResizeImage((Image)Properties.Resources.orderview_pending, imageSize, imageSize);
                            }
                            else if (Convert.ToInt32(gridSource[i].Field4) == 1)
                            {
                                DataGridViewImageCell cell = (DataGridViewImageCell)dgvorderview.Rows[rowIndex].Cells[statusno];
                                cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                                cell.Value = Properties.Resources.orderview_pending;
                            }
                            else if (Convert.ToInt32(gridSource[i].Field4) == 2)
                            {
                                DataGridViewImageCell cell = (DataGridViewImageCell)dgvorderview.Rows[rowIndex].Cells[statusno];
                                cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                                cell.Value = Properties.Resources.food_preparation;
                            }
                            else if (Convert.ToInt32(gridSource[i].Field4) == 3)
                            {
                                DataGridViewImageCell cell = (DataGridViewImageCell)dgvorderview.Rows[rowIndex].Cells[statusno];
                                cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                                cell.Value = Properties.Resources.orderview_complete;
                            }
                            rowIndex++;
                        }

                        if(pnl_ordergallery.Visible!=true)
                        {
                            Control controlToRemove = tlp_sub_main.GetControlFromPosition(0, 2);
                            tlp_sub_main.Controls.Remove(controlToRemove);
                            tlp_sub_main.Controls.Add(pnl_ordergallery, 0, 2);
                            pnl_ordergallery.Dock = DockStyle.Fill;
                            pnl_ordergallery.Visible = true;
                        }            
                        pnl_empty_msg.Visible = false;
                        tlp_next_prev.Visible = true;
                        pnl_details_view.Visible = false;

                    #region old code
                    ///TableLayoutPanel tlp_seletedItem_col = new TableLayoutPanel();
                    //float[] columnPercentages = { 44.82f, 6.27f, 14.94f, 6.02f, 19.04f, 8.92f };
                    //for (int i = 0; i < columnPercentages.Length; i++)
                    //{
                    //    tlp_seletedItem_col.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercentages[i]));
                    //}

                    //tlp_seletedItem_col.Name = "tlp_seletedItem_col" + clickedControl.Tag;
                    //tlp_seletedItem_col.Tag = clickedControl.Tag;
                    //tlp_seletedItem_col.AccessibleDescription = "0";
                    //tlp_seletedItem_col.Dock = DockStyle.Top;
                    //tlp_seletedItem_col.BackColor = Color.LightBlue;
                    //tlp_seletedItem_col.Height = 45;

                    //// Configure the Button for Item name
                    //Button buttonitemname = new Button();
                    //buttonitemname.Text = gridSource[0].Product;
                    //buttonitemname.Name = "buttonitemname_" + gridSource[0].Product; ;
                    //buttonitemname.Tag = gridSource[0].ProductId; ;
                    //buttonitemname.BackColor = Color.Beige;
                    //buttonitemname.Height = 40;
                    //buttonitemname.Font = new Font(buttonitemname.Font.FontFamily, 8, FontStyle.Bold);
                    //buttonitemname.Click += new EventHandler(buttonitemnameClickEventHandler);

                    //// Configure the Button for add Qty
                    //Button buttonAdd = new Button();
                    //buttonAdd.Text = "+";
                    //buttonAdd.Name = "buttonAdd_" + gridSource[0].ProductId;
                    //buttonAdd.Tag = gridSource[0].ProductId;
                    //buttonAdd.BackColor = Color.Beige;
                    //buttonAdd.Margin = new Padding(0, 14, 0, 0);
                    //buttonAdd.Font = new Font(buttonAdd.Font.FontFamily, 8, FontStyle.Bold);
                    //buttonAdd.Click += new EventHandler(ButtonbuttonAddClickEventHandler);

                    //// Configure the textbox enter QTY
                    //TextBox txtqty = new TextBox();
                    //txtqty.Text = "1";
                    //txtqty.Name = "txtqty_" + gridSource[0].ProductId;
                    //txtqty.Tag = gridSource[0].ProductId;
                    //txtqty.BackColor = Color.Beige;
                    //txtqty.TextAlign = HorizontalAlignment.Center;
                    //txtqty.Margin = new Padding(0, 14, 0, 0);
                    //txtqty.Font = new Font(txtqty.Font.FontFamily, 8, FontStyle.Bold);
                    //txtqty.Enabled = false;

                    //// Configure the textbox enter QTY
                    //Button buttonRemove = new Button();
                    //buttonRemove.Text = "-";
                    //buttonRemove.Name = "btn_remove_" + gridSource[0].ProductId;
                    //buttonRemove.Tag = gridSource[0].ProductId;
                    //buttonRemove.BackColor = Color.Beige;
                    //buttonRemove.Margin = new Padding(0, 14, 0, 0);
                    //buttonRemove.Font = new Font(buttonRemove.Font.FontFamily, 8, FontStyle.Bold);
                    //buttonRemove.Click += new EventHandler(ButtonRemoveClickEventHandler);

                    //// Configure the textbox Price
                    //TextBox txtprice = new TextBox();
                    //txtprice.Text = Convert.ToString(gridSource[0].Price);
                    //txtprice.Name = "txtprice_" + gridSource[0].ProductId;
                    //txtprice.Tag = gridSource[0].ProductId;
                    //txtprice.BackColor = Color.Beige;
                    //txtprice.TextAlign = HorizontalAlignment.Center;
                    //txtprice.Margin = new Padding(0, 14, 0, 0);
                    //txtprice.Font = new Font(txtprice.Font.FontFamily, 8, FontStyle.Bold);
                    //txtprice.Enabled = false;


                    //// Configure Item Delete Button
                    //PictureBox pb_item_delete = new PictureBox();
                    //pb_item_delete.Name = "btn_remove_" + gridSource[0].ProductId;
                    //pb_item_delete.Tag = gridSource[0].ProductId;
                    //pb_item_delete.BackColor = Color.Transparent;
                    //pb_item_delete.Dock = DockStyle.Top;
                    //pb_item_delete.Height = 40;
                    //pb_item_delete.Click += new EventHandler(pb_item_deleteClickEventHandler);
                    //pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.Delete_Grid;
                    //pb_item_delete.BackgroundImageLayout = ImageLayout.Zoom; // Change to Zoom instead of Stretch    

                    ////Dock Setting
                    //tlp_seletedItem_col.Dock = DockStyle.Top;
                    //buttonitemname.Dock = DockStyle.Top;
                    //buttonAdd.Dock = DockStyle.Top;
                    //txtqty.Dock = DockStyle.Fill;
                    //buttonRemove.Dock = DockStyle.Top;
                    //txtprice.Dock = DockStyle.Fill;

                    //// Get the current row count
                    //currentRowCount = tlp_seletedItem_col.RowCount;

                    //// Add controls to TableLayoutPanel at the next row
                    //tlp_seletedItem_col.Controls.Add(buttonitemname, 0, currentRowCount);
                    //tlp_seletedItem_col.Controls.Add(buttonAdd, 1, currentRowCount);
                    //tlp_seletedItem_col.Controls.Add(txtqty, 2, currentRowCount);
                    //tlp_seletedItem_col.Controls.Add(buttonRemove, 3, currentRowCount);
                    //tlp_seletedItem_col.Controls.Add(txtprice, 4, currentRowCount);
                    //tlp_seletedItem_col.Controls.Add(pb_item_delete, 5, currentRowCount);

                    //// Increment the row count
                    //tlp_seletedItem_col.RowCount = currentRowCount + 1;
                    //// Add the new TableLayoutPanel to tlp_seletedItem_main
                    //pnl_ordergallery.Controls.Add(tlp_seletedItem_col);
                    #endregion
                        dgvorderview.ResumeLayout();
                }
                //}
                if (dgvorderview.Rows.Count == 0)
                {              
                    tlp_next_prev.Visible = false;
                    pnl_ordergallery.Visible = false;
                    pnl_details_view.Visible = false;  
        
                    if (pnl_empty_msg.Visible != true)
                    {
                        pnl_empty_msg.Visible = true;
                        Control controlToRemove = tlp_sub_main.GetControlFromPosition(0, 2);
                        tlp_sub_main.Controls.Remove(controlToRemove);
                        pnl_empty_msg.Location = new Point(this.ClientSize.Width / 2 - pnl_empty_msg.Size.Width / 2, this.ClientSize.Height / 2 - pnl_empty_msg.Size.Height / 2);
                        SearchType = Convert.ToInt32(Food_Status.Pending);
                        lblorderheading.Text = "Active Orders";
                    }                  

                }
                #endregion

                refreshtimer_main.Start();
            }
            catch (Exception ex)
            {

            }

        }
        public void FillTablecode_Comb(int rowindex, int bookingmasterid)
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "MTID as id, Name as value";
                objSearch.Table = "tblBookDetails";
                objSearch.WhereNdGroupBy = "where BookmasterId =" + bookingmasterid + " and Rid=0";
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                gridSource[0].Id = 0;
                gridSource[0].Value = "Show Table";

                DataGridViewComboBoxCell dropDown = (DataGridViewComboBoxCell)dgvorderview.Rows[rowindex].Cells[tablecodeno];
                dropDown.DataSource = gridSource;
                dropDown.ValueMember = "Id";
                dropDown.DisplayMember = "Value";

                dropDown.Value = gridSource[0].Id;
                dropDown.ReadOnly = false;
            }
            catch (Exception ex)
            {
            }
        }
        public class ComboBoxItem
        {
            public int Id { get; set; }
            public string Value { get; set; }

            public ComboBoxItem(int id, string value)
            {
                Id = id;
                Value = value;
            }

            public override string ToString()
            {
                return Value;
            }
        }
        // Method to resize the image
        private Image ResizeImage(Image originalImage, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(resizedImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalImage, 0, 0, width, height);
            }
            return resizedImage;
        }

        private void txtorderid_search_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if(txtorderid_search.Text!=null || txtorderid_search.Text!="")
                    {
                        btn_backorder.Visible = true;
                        OrderMasterId = Convert.ToInt32(txtorderid_search.Text);
                        Filldetails();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txttablecode_serach_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txttablecode_serach.Text != null || txttablecode_serach.Text != "")
                    {
                        btn_backorder.Visible = true;
                        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                        DmnSearch dmnSearch = new DmnSearch();

                        dmnSearch.Select = "od.itemName Field1,od.Quantity Field2,od.CreatedDateTime Field3,ko.Status Field4,od.OrderDetailsId Field5,ko.koid Field6,ko.RemoveNotes Field7,om.OrderMasterId Field8,om.Tid Field9";
                        dmnSearch.Table = "tblOrderMaster om " +
                            "Left Join tblOrderDetails od on od.OrderMasterId=om.OrderMasterId  " +
                            "Left Join tblTable_info  ti on ti.Tid=om.Tid " +
                            "Left Join tblkitchenOrder  ko on ko.OrderDetailsId=od.OrderDetailsId";
                        dmnSearch.WhereNdGroupBy = "where om.Status=1 and ti.TableCode ='" + txttablecode_serach.Text+"'";
                        dmnSearch.OrderBy = "order by  om.OrderMasterId";
                        var detailslist = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (detailslist.Count > 0)
                        {
                            int count = 0;
                            pnl_empty_msg.Visible = false;
                            tlp_next_prev.Visible = true;
                            pnl_ordergallery.Visible = false;
                            pnl_details_view.Visible = true;
                            Control controlToRemove = tlp_sub_main.GetControlFromPosition(0, 2);
                            tlp_sub_main.Controls.Remove(controlToRemove);
                            tlp_sub_main.Controls.Add(pnl_details_view, 0, 2);
                            pnl_details_view.Dock = DockStyle.Fill;
                            dgv_details_view.Rows.Clear();
                            dgv_details_view.Rows.Add(detailslist.Count);
                            for (int i = 0; i < detailslist.Count; i++)
                            {
                                dgv_details_view.Rows[i].Cells[dslno].Value = i + 1;
                                dgv_details_view.Rows[i].Cells[ditemno].Value = detailslist[i].Field1;
                                dgv_details_view.Rows[i].Cells[dqtyno].Value = detailslist[i].Field2;
                                dgv_details_view.Rows[i].Cells[ddatetimeno].Value = detailslist[i].Field3;
                                dgv_details_view.Rows[i].Cells[ddetailsidno].Value = detailslist[i].Field5;
                                dgv_details_view.Rows[i].Cells[dkoidno].Value = detailslist[i].Field6;
                                dgv_details_view.Rows[i].Cells[dnotesno].Value = detailslist[i].Field7;
                                dgv_details_view.Rows[i].Cells[dstatusvalueno].Value = detailslist[i].Field4;
                                dgv_details_view.Rows[i].Cells[dordermasteridno].Value = detailslist[i].Field8;
                                dgv_details_view.Rows[i].Cells[dtableidno].Value = detailslist[i].Field9;

                                // Set the image for the "statusno" column
                                if (Convert.ToInt32(detailslist[i].Field4) == 0)
                                {
                                    count = count + 1;
                                    DataGridViewImageCell cell = (DataGridViewImageCell)dgv_details_view.Rows[i].Cells[dstatusno];
                                    cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                                    cell.Value = Properties.Resources.orderview_cancelled;
                                    DataGridViewImageCell cell1 = (DataGridViewImageCell)dgv_details_view.Rows[i].Cells[dactionno];

                                    if (Convert.ToInt32(dgv_details_view.Rows[i].Cells[dslno].Value) % 2 == 0)
                                    {
                                        cell1.ImageLayout = DataGridViewImageCellLayout.Stretch;
                                        cell1.Value = Properties.Resources.orderview_blank_2;
                                    }
                                    else
                                    {
                                        cell1.ImageLayout = DataGridViewImageCellLayout.Stretch;
                                        cell1.Value = Properties.Resources.orderview_blank;
                                    }

                                }
                                else if (Convert.ToInt32(detailslist[i].Field4) == 1)
                                {
                                    DataGridViewImageCell cell = (DataGridViewImageCell)dgv_details_view.Rows[i].Cells[dstatusno];
                                    cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                                    cell.Value = Properties.Resources.orderview_pending;//ResizeImage((Image)Properties.Resources.orderview_pending, imageSize, imageSize);
                                }
                                else if (Convert.ToInt32(detailslist[i].Field4) == 2)
                                {
                                    DataGridViewImageCell cell = (DataGridViewImageCell)dgv_details_view.Rows[i].Cells[dstatusno];
                                    cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                                    cell.Value = Properties.Resources.orderview_complete;//ResizeImage((Image)Properties.Resources.orderview_pending, imageSize, imageSize);
                                }
                            }
                            if (count == detailslist.Count)
                                dgv_details_view.Columns[dactionno].Visible = false;
                            else
                                dgv_details_view.Columns[dactionno].Visible = true;

                        }
                        else
                            CMessageBox.Show("No Records", "Information", MessageBoxButtons.OK);
                    }

                }

            }
            catch (Exception ex)
            {

            }
        }

        private void txtorderid_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }

        private void txttablecode_serach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }

        private void dgvorderview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            refreshtimer_main.Stop();
            if (PermissionFlag)
            {
                OrderMasterId = Convert.ToInt32(dgvorderview.Rows[e.RowIndex].Cells[orderidno].Value);
                btn_backorder.Visible = true;
                refreshtimer_sub.Start();
            }
        }


        public void Filldetails()
        {
            try
            {
                refreshtimer_sub.Stop();
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();

                dmnSearch.Select = "od.itemName Field1,od.Quantity Field2,od.CreatedDateTime Field3,ko.Status Field4,od.OrderDetailsId Field5,ko.koid Field6,ko.RemoveNotes Field7,om.OrderMasterId Field8,om.Tid Field9,od.AddonStatus Field10,od.AV_id Field11,od.CCode Field12,od.Srate Field13,od.OrderNo Field14";
                dmnSearch.Table = "tblOrderMaster om " +
                    "Left Join tblOrderDetails od on od.OrderMasterId=om.OrderMasterId  " +
                    "Left Join tblTable_info  ti on ti.Tid=om.Tid " +
                    "Left Join tblkitchenOrder  ko on ko.OrderDetailsId=od.OrderDetailsId";
                dmnSearch.WhereNdGroupBy = "where om.OrderMasterId ="+ OrderMasterId;
                dmnSearch.OrderBy = "order by  od.CreatedDateTime DESC";
                 var detailslist = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (detailslist.Count > 0)
                {
                    dgv_details_view.SuspendLayout();
                    pnl_details_view.SuspendLayout();
                    int count = 0;
                    if (tlp_next_prev.Visible !=false)
                        pnl_empty_msg.Visible = false;

                    if (tlp_next_prev.Visible != true)
                        tlp_next_prev.Visible = true;

                    if (pnl_ordergallery.Visible == true)
                        pnl_ordergallery.Visible = false;

                    if (pnl_details_view.Visible != true)
                    {
                        pnl_details_view.Visible = true;
                        Control controlToRemove = tlp_sub_main.GetControlFromPosition(0, 2);
                        tlp_sub_main.Controls.Remove(controlToRemove);
                        tlp_sub_main.Controls.Add(pnl_details_view, 0, 2);
                    }

                    pnl_details_view.Dock = DockStyle.Fill;
                    dgv_details_view.Rows.Clear();
                    dgv_details_view.Rows.Add(detailslist.Count);
                    int headcount = 0, subcount = 0;
                    string pre_avid = "";
                    for (int i = 0; i < detailslist.Count; i++)
                    {
                        if (Convert.ToInt32(detailslist[i].Field10) == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                        {
                            dgv_details_view.Rows[i].Cells[dslno].Style.Font = new Font(dgv_details_view.Font.FontFamily, 11, FontStyle.Bold);
                            dgv_details_view.Rows[i].Cells[ditemno].Style.Font = new Font(dgv_details_view.Font.FontFamily, 11, FontStyle.Bold);
                            dgv_details_view.Rows[i].Cells[ditemno].Value ="(Addon)   "+ detailslist[i].Field1;
                            dgv_details_view.Rows[i].Cells[davidno].Value = detailslist[i].Field11;
                            dgv_details_view.Rows[i].Cells[daddccodenono].Value = detailslist[i].Field12;
                            dgv_details_view.Rows[i].Cells[daddoonpriceno].Value = detailslist[i].Field13;
                            dgv_details_view.Rows[i].Cells[dslno].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgv_details_view.Rows[i].Cells[ditemno].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dgv_details_view.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                            dgv_details_view.Rows[i].Cells[dslno].Value = subcount + 1;
                            subcount = subcount + 1;   
                        }
                        else
                        {
                            //dgv_details_view.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                            dgv_details_view.Rows[i].Cells[dslno].Style.Font = new Font(dgv_details_view.Font.FontFamily, 12, FontStyle.Bold);
                            dgv_details_view.Rows[i].Cells[ditemno].Style.Font = new Font(dgv_details_view.Font.FontFamily, 12, FontStyle.Bold);
                            dgv_details_view.Rows[i].Cells[ditemno].Value = detailslist[i].Field1;
                            dgv_details_view.Rows[i].Cells[daddccodenono].Value = 0;
                            dgv_details_view.Rows[i].Cells[daddoonpriceno].Value =0;
                            dgv_details_view.Rows[i].Cells[dslno].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dgv_details_view.Rows[i].Cells[ditemno].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                            dgv_details_view.Rows[i].Cells[dslno].Value = headcount + 1;
                            headcount = headcount + 1;
                            subcount = 0;
                        }

                        dgv_details_view.Rows[i].Cells[dqtyno].Value = detailslist[i].Field2;
                        dgv_details_view.Rows[i].Cells[ddatetimeno].Value = detailslist[i].Field3;
                        dgv_details_view.Rows[i].Cells[ddetailsidno].Value = detailslist[i].Field5;
                        dgv_details_view.Rows[i].Cells[dkoidno].Value = detailslist[i].Field6;
                        dgv_details_view.Rows[i].Cells[dnotesno].Value = detailslist[i].Field7;
                        dgv_details_view.Rows[i].Cells[dstatusvalueno].Value = detailslist[i].Field4;
                        dgv_details_view.Rows[i].Cells[dordermasteridno].Value = detailslist[i].Field8;
                        dgv_details_view.Rows[i].Cells[dtableidno].Value = detailslist[i].Field9;
                        dgv_details_view.Rows[i].Cells[daddonstatusno].Value = detailslist[i].Field10;
                        dgv_details_view.Rows[i].Cells[dorderno].Value = detailslist[i].Field14;

                        // Set the image for the "statusno" column
                        if (Convert.ToInt32(detailslist[i].Field4) == 0)
                        {
                            count = count + 1;
                            DataGridViewImageCell cell = (DataGridViewImageCell)dgv_details_view.Rows[i].Cells[dstatusno];
                            cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                            cell.Value = null;
                            cell.Value = Properties.Resources.orderview_cancelled_icon;
                            DataGridViewImageCell cell1 = (DataGridViewImageCell)dgv_details_view.Rows[i].Cells[dactionno];
                            if(Convert.ToInt32(detailslist[i].Field10)==2)
                            {
                                cell1.Value = null;
                                cell1.ImageLayout = DataGridViewImageCellLayout.Stretch;
                                cell1.Value = Properties.Resources.orderview_addon_blank_icon;
                            }
                            else
                            {
                                if (Convert.ToInt32(dgv_details_view.Rows[i].Cells[dslno].Value) % 2 == 0)
                                {
                                    cell1.Value = null;
                                    cell1.ImageLayout = DataGridViewImageCellLayout.Stretch;
                                    cell1.Value = Properties.Resources.orderview_blank_2;
                                }
                                else
                                {
                                    cell1.Value = null;
                                    cell1.ImageLayout = DataGridViewImageCellLayout.Stretch;
                                    cell1.Value = Properties.Resources.orderview_blank;
                                }
                            }
                      

                        }
                        else if (Convert.ToInt32(detailslist[i].Field4) == 1)
                        {
                            DataGridViewImageCell cell = (DataGridViewImageCell)dgv_details_view.Rows[i].Cells[dstatusno];
                            cell.Value = null;
                            cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                            cell.Value = Properties.Resources.orderview_pending;
                        }
                        else if (Convert.ToInt32(detailslist[i].Field4) == 2)
                        {
                            DataGridViewImageCell cell = (DataGridViewImageCell)dgv_details_view.Rows[i].Cells[dstatusno];
                            cell.Value = null;
                            cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                            cell.Value = Properties.Resources.food_preparation;
                        }
                        else if (Convert.ToInt32(detailslist[i].Field4) == 3)
                        {
                            DataGridViewImageCell cell = (DataGridViewImageCell)dgv_details_view.Rows[i].Cells[dstatusno];
                            cell.Value = null;
                            cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                            cell.Value = Properties.Resources.orderview_tick_icon;
                        }
                    }
                    if(count==detailslist.Count)
                        dgv_details_view.Columns[dactionno].Visible = false;
                    else
                        dgv_details_view.Columns[dactionno].Visible = true;

                    dgv_details_view.ResumeLayout();
                    pnl_details_view.ResumeLayout();
                }
                refreshtimer_sub.Start();
            }
            catch (Exception ex)
            {

            }
        }

        private void btn_backorder_Click(object sender, EventArgs e)
        {
            refreshtimer_sub.Stop();
            if (pnl_ordergallery.Visible==false)
            {
                Control controlToRemove = tlp_sub_main.GetControlFromPosition(0, 2);
                tlp_sub_main.Controls.Remove(controlToRemove);
                tlp_sub_main.Controls.Add(pnl_ordergallery, 0, 2);
                pnl_ordergallery.Dock = DockStyle.Fill;
                pnl_ordergallery.Visible = true;
                pnl_empty_msg.Visible = false;
                tlp_next_prev.Visible = true;
                pnl_details_view.Visible = false;
                btn_backorder.Visible = false;
                refreshtimer_main.Start();
            }
 
        }
        private void dgv_details_view_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            refreshtimer_sub.Start();
            if (e.ColumnIndex == dactionno)
            {
                if (Convert.ToInt32(dgv_details_view.Rows[e.RowIndex].Cells[dstatusvalueno].Value) == 1 || Convert.ToInt32(dgv_details_view.Rows[e.RowIndex].Cells[dstatusvalueno].Value) == 2)
                {
                    var result = CMessageBox.Show("Do You want Delete This Addon", "Confirm The Action", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        decimal newprice = 0, discountprice = 0, TaxAmount = 0, finalprice = 0, disAmnt = 0;
                        string newpart = "";
                        DmnKitchenaddonremove dmnRemove_addon = new DmnKitchenaddonremove();
                        BllKitchenorder bll_rem_addon = new BllKitchenorder();

                        #region To Reset Addon ID(remove id form avid name)

                        string avidValue = Convert.ToString(dgv_details_view.Rows[dgv_details_view_rowno].Cells[davidno].Value);
                        string[] avidParts = avidValue.Split('_');
                        string extractedpid = $"{avidParts[0]}";
                        newpart = newpart + extractedpid + "_";
                        int remove_koid = Convert.ToInt32(dgv_details_view.Rows[dgv_details_view_rowno].Cells[dkoidno].Value);
                        for (int i = 0; i < dgv_details_view.RowCount; i++)
                        {
                            if (Convert.ToInt32(dgv_details_view.Rows[i].Cells[daddonstatusno].Value) == 2 && Convert.ToInt32(dgv_details_view.Rows[i].Cells[dkoidno].Value) != remove_koid && Convert.ToString(dgv_details_view.Rows[i].Cells[davidno].Value) == avidValue)
                            {
                                string ccode = Convert.ToString(dgv_details_view.Rows[i].Cells[daddccodenono].Value);
                                decimal quantityDecimal = Convert.ToDecimal(dgv_details_view.Rows[i].Cells[dqtyno].Value);
                                int quantityInt = (int)Math.Floor(quantityDecimal);
                                newpart = newpart + ccode + quantityInt.ToString();
                            }
                        }

                        for (int i = 0; i < dgv_details_view.RowCount; i++)
                        {
                            if (Convert.ToInt32(dgv_details_view.Rows[i].Cells[daddonstatusno].Value) == 2 && Convert.ToInt32(dgv_details_view.Rows[i].Cells[dkoidno].Value) != remove_koid && Convert.ToString(dgv_details_view.Rows[i].Cells[davidno].Value) == avidValue)
                            {
                                dgv_details_view.Rows[i].Cells[davidno].Value = newpart;
                            }
                        }

                        #endregion

                        #region Price Reset main product after remove addon
                        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "DiscountPer Field1,TaxType Field2,TaxPer Field3,Tax Field4,srate Field5";
                        dmnSearch.Table = "tblOrderDetails";
                        dmnSearch.WhereNdGroupBy = "where OrderMasterId = " + Convert.ToInt32(dgv_details_view.Rows[dgv_details_view_rowno].Cells[dordermasteridno].Value) + " and OrderNo = " + Convert.ToInt32(dgv_details_view.Rows[dgv_details_view_rowno].Cells[dorderno].Value) + " and AddonStatus = 1 and AV_id = '" + avidValue + "'";
                        dmnSearch.OrderBy = "";
                        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSource.Count > 0)
                        {
                            newprice = Convert.ToDecimal(gridSource[0].Field5) - Convert.ToDecimal(dgv_details_view.Rows[dgv_details_view_rowno].Cells[daddoonpriceno].Value);
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

                            #region Db Updation
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

                        dmnRemove_addon.OrderNo = Convert.ToInt32(dgv_details_view.Rows[dgv_details_view_rowno].Cells[dorderno].Value);
                        dmnRemove_addon.OrderDetailsId = Convert.ToInt32(dgv_details_view.Rows[dgv_details_view_rowno].Cells[ddetailsidno].Value);
                        dmnRemove_addon.koid = Convert.ToInt32(dgv_details_view.Rows[dgv_details_view_rowno].Cells[dkoidno].Value);
                        dmnRemove_addon.OrderMasterId = Convert.ToInt32(dgv_details_view.Rows[dgv_details_view_rowno].Cells[dordermasteridno].Value);
                        dmnRemove_addon.AVid = Convert.ToString(dgv_details_view.Rows[dgv_details_view_rowno].Cells[davidno].Value);

                        dmnRemove_addon.Status = 0;
                        dmnRemove_addon.CreatedBy = FrmDashboard.UserId;
                        dmnRemove_addon.Update_AVid = newpart;
                        var InsertStatusaddon = bll_rem_addon.Kitchenremove_addon("prd_remove_KitchenOrder_addon", dmnRemove_addon);
                        pnl_products_rem_descr.Visible = false;
                        txt_product_remove_des.Text = "";
                        Filldetails();
                        if (System.Windows.Forms.Application.OpenForms["FrmKitchenView"] != null)
                        {
                            FrmKitchenView _instanceFrmKitchenView = Application.OpenForms["FrmKitchenView"] as FrmKitchenView;
                            _instanceFrmKitchenView.refreshtimer.Start();
                        }
                        #endregion
                    }

                    txt_product_remove_des.Text = "";
                    pnl_products_rem_descr.Location = new Point(this.ClientSize.Width / 2 - pnl_products_rem_descr.Size.Width / 2, this.ClientSize.Height / 2 - pnl_products_rem_descr.Size.Height / 2);
                    dgv_details_view_rowno = e.RowIndex;
                    pnl_products_rem_descr.Visible = true;
                    lblheader_product_des.Text = "Remove " + Convert.ToString(dgv_details_view.Rows[dgv_details_view_rowno].Cells[ditemno].Value);
                }
            }
            else
            {
                if (Convert.ToInt32(dgv_details_view.Rows[e.RowIndex].Cells[dstatusvalueno].Value) == 0)
                {
                    CMessageBox.Show("Product Cancelled", "Information", MessageBoxButtons.OK);
                }
            }
        }
        public bool MandatoryCheck()
        {
            //if (string.IsNullOrEmpty(txtordermasterid.Text))
            //    txtordermasterid.Text = "0";

            if (txt_product_remove_des.Text=="Reason type here ....." || txt_product_remove_des.Text == "")
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

                    //if (Convert.ToInt32(dgv_details_view.Rows[dgv_details_view_rowno].Cells[daddonstatusno].Value) == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                    //{
            
                    //}
                    //else
                    //{
                        DmnOrderMaster dmnRemove_order = new DmnOrderMaster();
                        BllOrder bll_order = new BllOrder();
                        dmnRemove_order.OrderDetailsId = Convert.ToInt32(dgv_details_view.Rows[dgv_details_view_rowno].Cells[ddetailsidno].Value);
                        dmnRemove_order.koid = Convert.ToInt32(dgv_details_view.Rows[dgv_details_view_rowno].Cells[dkoidno].Value);
                        dmnRemove_order.OrderMasterId = Convert.ToInt32(dgv_details_view.Rows[dgv_details_view_rowno].Cells[dordermasteridno].Value);
                        dmnRemove_order.Tid = Convert.ToInt32(dgv_details_view.Rows[dgv_details_view_rowno].Cells[dtableidno].Value);
                        decimal originalValue = Convert.ToDecimal(dgv_details_view.Rows[dgv_details_view_rowno].Cells[dqtyno].Value);
                        dmnRemove_order.AddonStatus = Convert.ToInt32(dgv_details_view.Rows[dgv_details_view_rowno].Cells[daddonstatusno].Value);
                        dmnRemove_order.Quantity = (int)Math.Floor(originalValue);
                        dmnRemove_order.Status = 0;
                        dmnRemove_order.RemoveNotes = txt_product_remove_des.Text;
                        dmnRemove_order.AV_id = Convert.ToString(dgv_details_view.Rows[dgv_details_view_rowno].Cells[davidno].Value);
                        var InsertStatusP = bll_order.Remove_order_orderview("prd_remove_Order", dmnRemove_order);

                        dgv_details_view.Rows[dgv_details_view_rowno].ReadOnly = true;

                        DataGridViewImageCell cell = (DataGridViewImageCell)dgv_details_view.Rows[dgv_details_view_rowno].Cells[dstatusno];
                        cell.Value = Properties.Resources.orderview_cancelled;
                        DataGridViewImageCell cell1 = (DataGridViewImageCell)dgv_details_view.Rows[dgv_details_view_rowno].Cells[dactionno];

                        if (Convert.ToInt32(dgv_details_view.Rows[dgv_details_view_rowno].Cells[dslno].Value) % 2 == 0)
                            cell1.Value = Properties.Resources.orderview_blank_2;
                        else
                            cell1.Value = Properties.Resources.orderview_blank;

                        dgv_details_view.Rows[dgv_details_view_rowno].Cells[dnotesno].Value = txt_product_remove_des.Text;
                        pnl_products_rem_descr.Visible = false;
                        txt_product_remove_des.Text = "";

                        Filldetails();
                        if (System.Windows.Forms.Application.OpenForms["FrmKitchenView"] != null)
                        {
                            FrmKitchenView _instanceFrmKitchenView = Application.OpenForms["FrmKitchenView"] as FrmKitchenView;
                            _instanceFrmKitchenView.refreshtimer.Start();
                        }
                    //}                    
                }
            }
            catch(Exception ex)
            {

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

        private void btn_refresh_detaislview_Click(object sender, EventArgs e)
        {
            lblorderheading.Text = "Active Orders";
            if (dgv_details_view.Visible)
                refreshtimer_sub.Start();
            else
            {
                refreshtimer_main.Start();
            }
        }

        private void btn_complete_order_Click(object sender, EventArgs e)
        {
            refreshtimer_main.Stop();
            lblorderheading.Text = "Sucessfull Orders";
            SearchType = Convert.ToInt32(Food_Status.Complete);
            GetDataorders();
            FillOrders();
            refreshtimer_main.Start();
        }

        private void btn_Pending_order_Click(object sender, EventArgs e)
        {
            refreshtimer_main.Stop();
            lblorderheading.Text = "Active Orders";
            SearchType = Convert.ToInt32(Food_Status.Pending);
            GetDataorders();
            FillOrders();
            refreshtimer_main.Start();
        }

        private void btn_cancel_order_Click(object sender, EventArgs e)
        {
            refreshtimer_main.Stop();
            lblorderheading.Text = "Cancelled Orders";
            SearchType = Convert.ToInt32(Food_Status.Cancel);
            GetDataorders();
            FillOrders();
            refreshtimer_main.Start();
        }
        public void FillOrders_button(int serachtype)
        {      
            try
            {
                #region Fill Orders in Datagrid
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = " om.OrderMasterId Field1,ti.TableCode Field2,om.TotalQuantity Field3,om.status Field4 ," +
                    "(SELECT CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 0 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Result FROM tblOrderDetails WHERE OrderMasterId =  om.OrderMasterId ) Field5,om.CreatedDateTime Field6,om.BookmasterId Field7,ti.Tid Field8";
                dmnSearch.Table = "tblOrderMaster om Left Join tblTable_info  ti on ti.Tid=om.Tid";//Left Join tblOrderDetails od on od.OrderMasterId=om.OrderMasterId
                dgvorderview.Rows.Clear();
                int rowIndex = 0;
                if (serachtype == 1)
                {
                    dmnSearch.WhereNdGroupBy = "where om.status=1";
                    dmnSearch.OrderBy = "order by om.OrderMasterId DESC";
                }
                else if (serachtype == 0)
                {
                    dmnSearch.WhereNdGroupBy = "where om.status=0";
                    dmnSearch.OrderBy = "order by om.OrderMasterId DESC";
                }
                else if (serachtype == 2)
                {
                    dmnSearch.WhereNdGroupBy = "where om.status=2";
                    dmnSearch.OrderBy = "order by om.OrderMasterId DESC";
                }
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    
                #endregion
            }
            catch (Exception ex)
            {

            }


        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    if (pnl_products_rem_descr.Visible)
                        pnl_products_rem_descr.Visible = false;
                    else
                    {
                        FrmDashboard _instancedash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                        _instancedash.Closeform();
                    }
                    return true;
                case Keys.Back:
                    if (!pnl_products_rem_descr.Visible && !txttablecode_serach.Focused && !txtorderid_search.Focused)
                    {
                        Control controlToRemove = tlp_sub_main.GetControlFromPosition(0, 2);
                        tlp_sub_main.Controls.Remove(controlToRemove);
                        tlp_sub_main.Controls.Add(pnl_ordergallery, 0, 2);
                        pnl_ordergallery.Dock = DockStyle.Fill;
                        pnl_ordergallery.Visible = true;
                        pnl_empty_msg.Visible = false;
                        tlp_next_prev.Visible = true;
                        pnl_details_view.Visible = false;
                        btn_backorder.Visible = false;
                        FillOrders();
                    }
                    return true;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void dgvorderview_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void dgv_details_view_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void btnprevious_Click(object sender, EventArgs e)
        {
            if (currentPage == 1)
                btnprevious.Enabled = false;
            else
                btnprevious.Enabled = true;

            if (currentPage == totalPages)
                btnnext.Enabled = false;
            else
                btnnext.Enabled = true;
            if (currentPage > 1)
            {
                // Decrement the current page number
                currentPage--;
                // Recalculate the page data
                pagedata = Resultdata.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                FillOrders();
            }
        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            if (currentPage == 1)
                btnprevious.Enabled = false;
            else
                btnprevious.Enabled = true;

            if (currentPage == totalPages)
                btnnext.Enabled = false;
            else
                btnnext.Enabled = true;

            if (currentPage < totalPages)
            {
                // Increment the current page number
                currentPage++;
                // Recalculate the page data
                pagedata = Resultdata.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                FillOrders();

            }
        }

        private void refreshtimer_main_Tick(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Food_Status.Complete) == SearchType)
            {
                lblorderheading.Text = "Sucessfull Orders";
            }
            else if (Convert.ToInt32(Food_Status.Pending) == SearchType)
            {
                lblorderheading.Text = "Active Orders";
            }
            else if (Convert.ToInt32(Food_Status.Cancel) == SearchType)
            {
                lblorderheading.Text = "Cancelled Orders";
            }
            GetDataorders();
            FillOrders();
        }

        private void refreshtimer_sub_Tick(object sender, EventArgs e)
        {
            Filldetails();
        }
    }
}
