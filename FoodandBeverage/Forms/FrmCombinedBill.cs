using BLL;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FoodandBeverage.Interfaces;
using CustomMessageBox;
using FoodandBeverage.Forms.UserControls;

namespace FoodandBeverage.Forms
{
    public partial class FrmCombinedBill : Form
    {
        public FrmCombinedBill()
        {
            InitializeComponent();
        }
        #region Declaration
        public List<DmnNameList> CheckedtableNames = new List<DmnNameList>();
        TableLayoutPanel tlp_table_dashboard = new TableLayoutPanel();

        public int panelsPerRow = 0;
        public int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        #endregion
        #region Grid Declaration
        public static int cslno = 0;
        public static int actionno = 1;
        public static int orderdatetimeno = 2;
        public static int entryno = 3;
        public static int tablecodeno = 4;
        public static int totalqtyno = 5;
        public static int netamountno = 6;
        public static int recamountno = 7;

        public static int balamountno = 8;
        public static int paytypename = 9;
        public static int statusno = 10;
        public static int ordermasteridno = 11;
        #endregion
        private void FrmCombinedBill_Load(object sender, EventArgs e)
        {
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

            Billcomplete_Fill();
        }
        public void Billcomplete_Fill()
        {
            try
            {
                this.Visible = false;
                this.SuspendLayout();
                this.Controls.Add(pnl_complebill_prevbill);
                pnl_complebill_prevbill.Dock = DockStyle.Fill;
                Control controlToRemove = tlp_compl_pre_bill.GetControlFromPosition(0, 1);
                tlp_compl_pre_bill.Controls.Remove(controlToRemove);
                tlp_compl_pre_bill.Controls.Add(pnl_compelte_bill, 0, 1);
                tlp_compl_pre_bill.Dock = DockStyle.Fill;

                pnl_empty_msg.Visible = false;
                pnl_complebill_prevbill.Visible = true;
                BllSearch<DmnTableinfo> bllSearch = new BllSearch<DmnTableinfo>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "ti.* ,c.CategoryName as CategoryName,om.NetAmount ,(select CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1 from tblOrderDetails WHERE status!=0 and AddonStatus in(1,0) and OrderMasterId =ti.OrderMasterId ) BillStatus";
                dmnSearch.Table = "tblTable_info  ti " +
                    "LEFT JOIN tblCategory_FTI c on c.Fcid=ti.Fcid " +
                    "LEFT JOIN tblOrderMaster om on om.OrderMasterId=ti.OrderMasterId and om.Status !=0";
                dmnSearch.WhereNdGroupBy = "where ti.Status=1";
                dmnSearch.OrderBy = "order by ti.tid";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    pnl_compelte_bill.Controls.Clear();
                    tlp_table_dashboard.Controls.Clear();
                    tlp_table_dashboard.Dock = DockStyle.Fill;
                    tlp_table_dashboard.AutoScroll = true;                

                    #region order complete only dispaly code
                    int rowIndex_cate = 0;
                    foreach (var tabledata in gridSource)
                    {
                        string tlpname = "tlp_category_Com_" + tabledata.CategoryName;
                        TableLayoutPanel tlp_box = tlp_table_dashboard.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                        if (tlp_box == null)
                        {
                            TableLayoutPanel tlp_category = new TableLayoutPanel();
                            tlp_category.Name = "tlp_category_Com_" + tabledata.CategoryName;
                            tlp_category.Tag = tabledata.CategoryName;
                            tlp_category.Dock = DockStyle.Top;
                            tlp_category.AutoSize = true;
                            //tlp_category.BackColor = Color.Green;
                            tlp_category.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                            tlp_category.RowCount = 2;
                            tlp_category.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                            tlp_category.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                            FlowLayoutPanel flp_box = new FlowLayoutPanel();
                            flp_box.Name = "flp_box_Com_" + tabledata.CategoryName;
                            flp_box.Tag = tabledata.CategoryName;
                            flp_box.Dock = DockStyle.Fill;
                            flp_box.AutoSize = true;
                            flp_box.FlowDirection = FlowDirection.LeftToRight;
                            //flp_box.BackColor = Color.Blue;

                            // Configure the Label
                            Label lblcategory_head = new Label();
                            lblcategory_head.Name = "lbcate_head_Com_" + tabledata.CategoryName;
                            lblcategory_head.Tag = tabledata.Fcid;
                            lblcategory_head.Text = tabledata.CategoryName;
                            lblcategory_head.ForeColor = Color.Black;
                            lblcategory_head.BackColor = Color.Transparent;
                            lblcategory_head.Dock = DockStyle.Top;
                            lblcategory_head.Font = new Font(lblcategory_head.Font.FontFamily, 12, FontStyle.Bold);

                            tlp_category.Controls.Add(lblcategory_head, 0, 0);
                            tlp_category.Controls.Add(flp_box, 0, 1);
                            tlp_table_dashboard.Controls.Add(tlp_category, 0, rowIndex_cate);
                            rowIndex_cate++;
                        }

                        string flpname = "flp_box_Com_" + tabledata.CategoryName;
                        FlowLayoutPanel flp_box_check = tlp_table_dashboard.Controls.Find(flpname, true).FirstOrDefault() as FlowLayoutPanel;

                        #region Table Box 
                        // Configure the TableLayoutPanel
                        TableLayoutPanel tlp_table_inner = new TableLayoutPanel();
                        tlp_table_inner.RowCount = 3;
                        tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
                        tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
                        tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33f));
                        tlp_table_inner.Name = "tlp_com" + tabledata.TableCode;
                        tlp_table_inner.Tag = tabledata.Tid;
                        tlp_table_inner.BorderStyle = BorderStyle.FixedSingle;
                        tlp_table_inner.Dock = DockStyle.None;
                        //tlp_table_inner.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                        tlp_table_inner.Margin = new Padding(10, 10, 0, 10);
                        tlp_table_inner.BackColor = Color.White;


                        if (screenWidth > 1366)
                        {
                            tlp_table_inner.Width = 300;
                            tlp_table_inner.Height = 100;
                        }
                        else if (screenWidth >= 1024 && screenWidth <= 1366)
                        {
                            tlp_table_inner.Width = 120;
                            tlp_table_inner.Height = 120;
                        }

                        Button btn_select = new Button();
                        btn_select.Name = "btn_select_com" + tabledata.TableCode;
                        btn_select.AccessibleDescription = Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode);
                        btn_select.Tag = Convert.ToInt32(BillSelect.Unselect);
                        btn_select.FlatStyle = FlatStyle.Flat;
                        btn_select.FlatAppearance.BorderSize = 0;
                        btn_select.BackgroundImage = Properties.Resources.Billaddicon;
                        btn_select.BackgroundImageLayout = ImageLayout.Stretch;
                        btn_select.Dock = DockStyle.Fill;
                        btn_select.Margin = new Padding(20, 0, 20, 0);
                        btn_select.Click += new EventHandler(btn_select_Clicked);

                        // Configure the Label
                        Label lbltablename = new Label();
                        lbltablename.Name = "lb_tablename" + tabledata.TableCode;
                        lbltablename.AccessibleDescription = Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode);
                        lbltablename.TextAlign = ContentAlignment.MiddleCenter;
                        lbltablename.Tag = tabledata.Tid;
                        lbltablename.Text = tabledata.TableCode;
                        lbltablename.ForeColor = Color.Black;
                        lbltablename.BackColor = Color.Transparent;
                        lbltablename.Font = new Font("Arial", 10, FontStyle.Bold);
                        lbltablename.ForeColor = Color.Red;
                        lbltablename.Dock = DockStyle.Fill;

                        // Configure the Label
                        Label netamountlabel = new Label();
                        netamountlabel.Name = "lb_netamount" + tabledata.TableCode;
                        netamountlabel.AccessibleDescription = Convert.ToString(tabledata.OrderMasterId) + "," + tabledata.Tid + "," + Convert.ToString(tabledata.TableCode);
                        netamountlabel.TextAlign = ContentAlignment.MiddleCenter;
                        netamountlabel.Tag = tabledata.Tid;
                        netamountlabel.Text = "\u20B9" + Convert.ToString(tabledata.NetAmount);
                        netamountlabel.ForeColor = Color.Black;
                        netamountlabel.BackColor = Color.Transparent;
                        netamountlabel.Font = new Font("Arial", 10, FontStyle.Bold);
                        netamountlabel.Dock = DockStyle.Fill;

                        if (Convert.ToInt32(tabledata.BillStatus) == 0)                        
                            tlp_table_inner.Visible = true; 
                        else
                            tlp_table_inner.Visible = false;

                        if (Convert.ToInt32(tabledata.OrderStatus) == 1)
                            tlp_table_inner.Controls.Add(btn_select, 0, 0);
                        tlp_table_inner.Controls.Add(lbltablename, 0, 1);
                        tlp_table_inner.Controls.Add(netamountlabel, 0, 2);

                        lbltablename.Click += new EventHandler(CompleteEventHandler);
                        netamountlabel.Click += new EventHandler(CompleteEventHandler);
                        tlp_table_inner.Click += new EventHandler(CompleteEventHandler);

                        if (tabledata.OrderStatus == 1)
                        {
                            lbltablename.AccessibleDescription = "0";
                            netamountlabel.AccessibleDescription = "0";
                            tlp_table_inner.AccessibleDescription = "0";
                        }
                        else if (Convert.ToInt32(tabledata.OrderMasterId) > 0)
                        {
                            lbltablename.AccessibleDescription = Convert.ToInt32(DashboardTableType.CombinedOrderBill) + "," + Convert.ToInt32(tabledata.BookmasterId);
                            tlp_table_inner.AccessibleDescription = Convert.ToInt32(DashboardTableType.CombinedOrderBill) + "," + Convert.ToInt32(tabledata.BookmasterId);
                            netamountlabel.AccessibleDescription = Convert.ToInt32(DashboardTableType.CombinedOrderBill) + "," + Convert.ToInt32(tabledata.BookmasterId);
                        }
                        #endregion

                        flp_box_check.Controls.Add(tlp_table_inner);
                        flp_box_check.Refresh();

                        //if (tlp_table_dashboard.Controls.Count == 0)
                        //{
                        //    controlToRemove = tlp_compl_pre_bill.GetControlFromPosition(0, 1);
                        //    tlp_compl_pre_bill.Controls.Remove(controlToRemove);
                        //    tlp_compl_pre_bill.Controls.Add(pnl_empty_msg, 0, 1);
                        //    pnl_empty_msg.Dock = DockStyle.Fill;
                        //    pnl_empty_msg.Visible = true;
                        //    btn_prevbill.PerformClick();
                        //}
                    }
                    pnl_compelte_bill.Controls.Add(tlp_table_dashboard);
                    pnl_compelte_bill.Dock = DockStyle.Fill;
                    pnl_compelte_bill.Visible = true;
                    #endregion

                    CombineBillAutoTimer.Start();
                }
                else
                {
                    controlToRemove = tlp_compl_pre_bill.GetControlFromPosition(0, 1);
                    tlp_compl_pre_bill.Controls.Remove(controlToRemove);
                    tlp_compl_pre_bill.Controls.Add(pnl_empty_msg, 0, 1);
                    pnl_empty_msg.Dock = DockStyle.Fill;
                    pnl_empty_msg.Visible = true;
                    btn_prevbill.PerformClick();
                }
                this.ResumeLayout();
                this.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }

        private void btn_select_Clicked(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                string textvalue = button.AccessibleDescription;
                string[] parts = textvalue.Split(',');
                int omasterid = 0, tid = 0;
                string tablecode = "";
                if (button != null)
                {
                    if (parts.Length >= 2)
                    {
                        omasterid = Convert.ToInt32(parts[0]);
                        tid = Convert.ToInt32(parts[1]);
                        tablecode = Convert.ToString(parts[2]);
                    }

                    if (Convert.ToInt32(button.Tag) == Convert.ToInt32(BillSelect.Unselect))
                    {
                        CheckedtableNames.Add(new DmnNameList
                        {
                            Id = tid,
                            Ordermasterid = omasterid,
                            Tablecode = tablecode
                        });
                        button.BackgroundImage = Properties.Resources.billselectedicon;
                        button.Tag = Convert.ToInt32(BillSelect.Selected);
                    }
                    else if (Convert.ToInt32(button.Tag) == Convert.ToInt32(BillSelect.Selected))
                    {
                        DmnNameList itemToRemove = CheckedtableNames.FirstOrDefault(item => item.Ordermasterid == omasterid);
                        if (itemToRemove != null)
                        {
                            CheckedtableNames.Remove(itemToRemove);
                        }
                        button.BackgroundImage = Properties.Resources.Billaddicon;
                        button.Tag = Convert.ToInt32(BillSelect.Unselect);
                    }

                    button.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            catch (Exception ex)
            {
            }
        }
        public class CategoryInfo
        {
            public int Fcid { get; set; }
            public string CategoryName { get; set; }
        }

        private void CompleteEventHandler(object sender, EventArgs e)
        {
            try
            {
                CombineBillAutoTimer.Stop();
                Control clickedControl = sender as Control;
                if (clickedControl != null)
                {
                    string tableCode = clickedControl.Name;
                    int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                    int screenHeight = Screen.PrimaryScreen.Bounds.Height;
                    if (CheckedtableNames.Count > 0)
                    {
                        if (System.Windows.Forms.Application.OpenForms["FrmOrder"] != null)
                        {
                            FrmOrder _instanceFrmOrder = Application.OpenForms["FrmOrder"] as FrmOrder;
                            if (_instanceFrmOrder.WindowState == FormWindowState.Minimized)
                            {
                                _instanceFrmOrder.WindowState = FormWindowState.Maximized;
                            }
                            else
                            {
                                _instanceFrmOrder.Show();
                                _instanceFrmOrder.BringToFront();
                            }
                        }
                        else
                        {
                            FrmOrder _instanceFrmOrder = new FrmOrder(0, Convert.ToInt32(DashboardTableType.CombinedOrderBill), 0, CheckedtableNames);
                            _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                            _instanceFrmOrder.Opacity = 0;
                            _instanceFrmOrder.Visible = false;
                            _instanceFrmOrder.Show();
                            _instanceFrmOrder.BringToFront();
                        }

                    }
                    else
                    {
                        string textvalue = clickedControl.AccessibleDescription;
                        string[] parts = textvalue.Split(',');
                        string bmasterid = "";
                        int Btype = 0;
                        if (parts.Length >= 2)
                        {
                            Btype = Convert.ToInt32(parts[0]);
                            bmasterid = parts[1];
                        }
                        else
                            Btype = Convert.ToInt32(parts[0]);
                        if (Btype == Convert.ToInt32(DashboardTableType.Normalorder))
                        {
                            if (System.Windows.Forms.Application.OpenForms["FrmOrder"] != null)
                            {
                                FrmOrder _instanceFrmOrder = Application.OpenForms["FrmOrder"] as FrmOrder;
                                if (_instanceFrmOrder.WindowState == FormWindowState.Minimized)
                                {
                                    _instanceFrmOrder.WindowState = FormWindowState.Maximized;
                                }
                                else
                                {
                                    _instanceFrmOrder.Show();
                                    _instanceFrmOrder.BringToFront();
                                }
                            }
                            else
                            {
                                int tagValue;
                                if (clickedControl.Tag != null)
                                {
                                    FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Normalorder), 0, CheckedtableNames);
                                    _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                                    _instanceFrmOrder.Opacity = 0;
                                    _instanceFrmOrder.Visible = false;
                                    _instanceFrmOrder.Show();
                                    _instanceFrmOrder.BringToFront();
                                }
                            }
                        }
                        else
                        {
                            if (System.Windows.Forms.Application.OpenForms["FrmOrder"] != null)
                            {
                                FrmOrder _instanceFrmOrder = Application.OpenForms["FrmOrder"] as FrmOrder;
                                if (_instanceFrmOrder.WindowState == FormWindowState.Minimized)
                                {
                                    _instanceFrmOrder.WindowState = FormWindowState.Maximized;
                                }
                                else
                                {
                                    _instanceFrmOrder.Show();
                                    _instanceFrmOrder.BringToFront();
                                }
                            }
                            else
                            {
                                int tagValue;
                                if (clickedControl.Tag != null)
                                {
                                    FrmOrder _instanceFrmOrder = new FrmOrder(Convert.ToInt32(clickedControl.Tag), Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel), Convert.ToInt32(bmasterid), CheckedtableNames);
                                    _instanceFrmOrder.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                                    _instanceFrmOrder.Opacity = 0;
                                    _instanceFrmOrder.Visible = false;
                                    _instanceFrmOrder.Show();
                                    _instanceFrmOrder.BringToFront();
                                }
                            }
                        }

                    }


                }
                CombineBillAutoTimer.Start();
            }
            catch (Exception ex)
            {
            }
        }
        private void tlp_category_com_MouseHover(object sender, EventArgs e)
        {
            try
            {
                ToolTip toolTip1 = new ToolTip();
                TableLayoutPanel tlpcate = sender as TableLayoutPanel;
                string tlpName = "tlp_category_com_" + tlpcate.Tag;
                // Find the TableLayoutPanel control by name
                TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
                if (tlp != null)
                {
                    tlp.BackColor = Color.LightBlue;
                    // Show tooltip with the item name
                    toolTip1.SetToolTip(tlp, tlpcate.Tag.ToString());
                }
            }
            catch (Exception ex)
            {

            }

        }
        private void tlp_category_com_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                TableLayoutPanel tlpcate = sender as TableLayoutPanel;
                string tlpName = "tlp_category_com_" + tlpcate.Tag;
                // Find the TableLayoutPanel control by name
                TableLayoutPanel tlp = this.Controls.Find(tlpName, true).FirstOrDefault() as TableLayoutPanel;
                if (tlp != null)
                {
                    tlp.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void btn_backCompleorder_Click(object sender, EventArgs e)
        {
            btn_backCompleorder.Visible = false;
            btn_prevbill.Visible = true;

            this.Controls.Add(pnl_complebill_prevbill);
            pnl_complebill_prevbill.Dock = DockStyle.Fill;
            Control controlToRemove = tlp_compl_pre_bill.GetControlFromPosition(0, 1);
            tlp_compl_pre_bill.Controls.Remove(controlToRemove);
            tlp_compl_pre_bill.Controls.Add(pnl_compelte_bill, 0, 1);
            tlp_compl_pre_bill.Dock = DockStyle.Fill;

            pnl_empty_msg.Visible = false;
            pnl_complebill_prevbill.Visible = true;

            CombineBillAutoTimer.Start();
        }

        private void btn_serach_prev_bill_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "ID,DateTime,Invoice No,Table Code,TotalQuantity,NetAmount,RcvAmnt,BalAmnt,PaymentMode";
                _objSearch.Select = "sm.SalesMasterId ID,sm.CreatedDateTime Field1,sm.EntryNo Field2,ti.TableCode Field3,sm.TotalQuantity Field4,sm.NetAmount Field5,sm.RcvAmnt Field6,sm.BalAmnt Field7,g.Caption Field8";
                _objSearch.Table = "tblSalesMaster sm LEFT JOIN tblTable_info ti on ti.tid=sm.Tid LEFT JOIN tblGeneralOptions g ON g.GTypeSubId=sm.PaymentMode";
                _objSearch.WhereNdGroupBy = "where sm.status=1 and  g.GTypeId=2";
                _objSearch.OrderBy = "order by sm.SalesMasterId DESC";
                var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);
                if (gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        int oid = Convert.ToInt32(returnValue);
                        printBill(oid);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btn_prevbill_Click(object sender, EventArgs e)
        {
            try
            {
                CombineBillAutoTimer.Stop();

                this.Controls.Add(pnl_complebill_prevbill);
                pnl_complebill_prevbill.Dock = DockStyle.Fill;
                pnl_complebill_prevbill.BringToFront();

                Control controlToRemove = tlp_compl_pre_bill.GetControlFromPosition(0, 1);
                tlp_compl_pre_bill.Controls.Remove(controlToRemove);
                tlp_compl_pre_bill.Controls.Add(pnl_previous_bill, 0, 1);
                tlp_compl_pre_bill.Dock = DockStyle.Fill;
                pnl_previous_bill.Dock = DockStyle.Fill;
                pnl_previous_bill.Visible = true;
                pnl_empty_msg.Visible = false;

                btn_backCompleorder.Visible = true;
                btn_prevbill.Visible = false;

                #region Fill complete or cancelled order in Datagrid
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "Top 20 sm.CreatedDateTime Field1,sm.EntryNo Field2,ti.TableCode Field3,sm.TotalQuantity Field4,sm.Status Field5,sm.SalesMasterId Field6 ,sm.NetAmount Field7,sm.RcvAmnt Field8,sm.BalAmnt Field9,sm.PaymentMode Field10,sm.Tid Field11,sm.CombinedOrderBill_TYPE Field12,sm.OrderMasterid Field13";
                dmnSearch.Table = "tblSalesMaster sm LEFT JOIN tblTable_info ti on ti.tid=sm.Tid";
                dmnSearch.WhereNdGroupBy = "where sm.status=1";
                dmnSearch.OrderBy = "order by sm.SalesMasterId DESC";
                dgvorderview.Rows.Clear();
                int rowIndex = 0;
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    int imageSize = 32;
                    dgvorderview.Rows.Add(gridSource.Count);
                    int x = dgvorderview.RowCount;
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        dgvorderview.Rows[rowIndex].Cells[cslno].Value = rowIndex + 1;
                        dgvorderview.Rows[rowIndex].Cells[orderdatetimeno].Value = gridSource[i].Field1;
                        dgvorderview.Rows[rowIndex].Cells[entryno].Value = gridSource[i].Field2;

                        dgvorderview.Rows[rowIndex].Cells[tablecodeno].Value = gridSource[i].Field3;
                        #region multiple table code case reservation 
                        if (Convert.ToInt32(gridSource[i].Field11) == 0)
                        {
                            FillTablecode_Comb(rowIndex, Convert.ToInt32(gridSource[i].Field6), Convert.ToInt32(gridSource[i].Field13), Convert.ToInt32(gridSource[i].Field12));
                        }
                        else
                        {
                            DataGridViewComboBoxCell dropDown = (DataGridViewComboBoxCell)dgvorderview.Rows[rowIndex].Cells[tablecodeno];
                            List<ComboBoxItem> comboBoxItems = new List<ComboBoxItem>();
                            ComboBoxItem comboBoxItem = new ComboBoxItem(Convert.ToInt32(gridSource[i].Field11), gridSource[i].Field3);
                            comboBoxItems.Add(comboBoxItem);
                            dropDown.DataSource = comboBoxItems;
                            dropDown.ValueMember = "Id";
                            dropDown.DisplayMember = "Value";
                            dropDown.Value = comboBoxItems[0].Id;
                            dropDown.ReadOnly = true;
                        }
                        #endregion


                        dgvorderview.Rows[rowIndex].Cells[totalqtyno].Value = gridSource[i].Field4;
                        dgvorderview.Rows[rowIndex].Cells[ordermasteridno].Value = gridSource[i].Field6;
                        dgvorderview.Rows[rowIndex].Cells[netamountno].Value = gridSource[i].Field7;
                        dgvorderview.Rows[rowIndex].Cells[recamountno].Value = gridSource[i].Field8;
                        dgvorderview.Rows[rowIndex].Cells[balamountno].Value = gridSource[i].Field9;
                        switch (Convert.ToInt32(gridSource[i].Field10))
                        {
                            case 1:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "CASH";
                                break;
                            case 5:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "DEBITCARD";
                                break;
                            case 4:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "UPI";
                                break;
                            case 6:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "BANKINGONLINE";
                                break;
                            case 3:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "CREDIT";
                                break;
                            case 7:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "OTHERS";
                                break;
                            case 11:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "PART";
                                break;
                            case 12:
                                dgvorderview.Rows[rowIndex].Cells[paytypename].Value = "SPLIT";
                                break;
                        }

                        DataGridViewImageCell cell = (DataGridViewImageCell)dgvorderview.Rows[rowIndex].Cells[statusno];
                        cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                        cell.Value = Properties.Resources.orderview_complete;

                        DataGridViewImageCell cell1 = (DataGridViewImageCell)dgvorderview.Rows[rowIndex].Cells[actionno];
                        cell1.ImageLayout = DataGridViewImageCellLayout.Stretch;
                        cell1.Value = Properties.Resources.viewbill_billing;

                        rowIndex++;
                    }
                }
                else
                {

                    controlToRemove = tlp_compl_pre_bill.GetControlFromPosition(0, 1);
                    tlp_compl_pre_bill.Controls.Remove(controlToRemove);
                    tlp_compl_pre_bill.Controls.Add(pnl_empty_msg, 0, 1);
                    pnl_empty_msg.Dock = DockStyle.Fill;
                    pnl_empty_msg.Visible = true;
                }

                #endregion
            }
            catch (Exception ex)
            {

            }
        }

        private void dgvorderview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == actionno)
                {
                    int oid = Convert.ToInt32(dgvorderview.Rows[e.RowIndex].Cells[ordermasteridno].Value);
                    printBill(oid);
                }

            }
            catch (Exception ex)
            {

            }

        }
        public void printBill(int oid)
        {
            try
            {
                DmnSearch obj = new DmnSearch();
                BllSearch<DmnGridFields> bllpayments = new BllSearch<DmnGridFields>();
                obj.Select = "PaymentMode Field1,SalesMasterId Field2";
                obj.Table = "tblSalesMaster";
                obj.WhereNdGroupBy = " where status=1 and SalesMasterId=" + oid;
                var result = bllpayments.GetSearchData("prdSearch", obj);
                if (result.Count > 0)
                {
                    if (Convert.ToInt32(result[0].Field1) != 12)
                    {
                        pnl_split.Visible = false;
                        FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(oid, Convert.ToInt32(result[0].Field1), 0);
                        frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                        frmSalesInvoice.ShowDialog();
                    }
                    else
                    {
                        obj.Select = "Amount Field1,SplitType Field2,SalemasterId Field3,PayId Field4 ";
                        obj.Table = "tblpayment";
                        obj.WhereNdGroupBy = " where status=1 and SaleMasterId=" + oid;
                        var paymentresult = bllpayments.GetSearchData("prdSearch", obj);
                        if (paymentresult.Count > 0)
                        {
                            dgv_split.Rows.Clear();
                            dgv_split.Rows.Add(paymentresult.Count);
                            int x = dgv_split.RowCount;
                            for (int i = 0; i < paymentresult.Count; i++)
                            {
                                dgv_split.Rows[i].Cells[0].Value = i + 1;
                                dgv_split.Rows[i].Cells[1].Value = paymentresult[i].Field1;
                                DataGridViewImageCell cell = (DataGridViewImageCell)dgv_split.Rows[i].Cells[2];
                                cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                                cell.Value = Properties.Resources.eye;
                                dgv_split.Rows[i].Cells[3].Value = paymentresult[i].Field3;
                                dgv_split.Rows[i].Cells[4].Value = paymentresult[i].Field4;
                            }
                        }
                        pnl_split.Location = new Point(this.ClientSize.Width / 2 - pnl_split.Size.Width + 500, this.ClientSize.Height / 2 - pnl_split.Size.Height / 2);
                        pnl_split.Visible = true;
                    }

                }

            }
            catch (Exception ex)
            {

            }
        }
        public void FillTablecode_Comb(int rowindex, int salemasterid, int ordermasterid, int ordertype)
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                DataGridViewComboBoxCell dropDown = (DataGridViewComboBoxCell)dgvorderview.Rows[rowindex].Cells[tablecodeno];
                if (ordertype == 0)
                {
                    objSearch.Select = "bd.MTID id,bd.Name value";
                    objSearch.Table = "tblOrderMaster om " +
                        "left join tblbookMaster bm on om.BookmasterId=bm.BookmasterId " +
                        "left join tblBookDetails bd on bd.BookmasterId=bm.BookmasterId and bd.Rid=0 ";
                    objSearch.WhereNdGroupBy = "where om.OrderMasterId=" + ordermasterid;
                    objSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                    gridSource[0].Id = 0;
                    gridSource[0].Value = "Show Table";
                    dropDown.DataSource = gridSource;
                    dropDown.ValueMember = "Id";
                    dropDown.DisplayMember = "Value";

                    dropDown.Value = gridSource[0].Id;
                    dropDown.ReadOnly = false;
                    dgvorderview.Rows[rowindex].Cells[tablecodeno].ReadOnly = false;
                }
                else
                {
                    objSearch.Select = "com.tid id,ti.TableCode value";
                    objSearch.Table = "tblCombinedOrderBill com left join tblTable_info ti on ti.tid=com.Tid";
                    objSearch.WhereNdGroupBy = "where SalesMasterId =" + salemasterid;
                    objSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                    gridSource[0].Id = 0;
                    gridSource[0].Value = "Show Table";
                    dropDown.DataSource = gridSource;
                    dropDown.ValueMember = "Id";
                    dropDown.DisplayMember = "Value";

                    dropDown.Value = gridSource[0].Id;
                    dropDown.ReadOnly = false;
                    dgvorderview.Rows[rowindex].Cells[tablecodeno].ReadOnly = false;
                }

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

        private void btn_close_split_Click(object sender, EventArgs e)
        {
            pnl_split.Visible = false;
        }

        private void dgv_split_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(Convert.ToInt32(dgv_split.Rows[e.RowIndex].Cells[3].Value), Convert.ToInt32(PaymentType.SPLIT), Convert.ToInt32(dgv_split.Rows[e.RowIndex].Cells[4].Value));
                frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                frmSalesInvoice.ShowDialog();
            }
        }

        private void dgvorderview_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void CombineBillAutoTimer_Tick(object sender, EventArgs e)
        {
            BillCombineBill_Update();
        }
        public void BillCombineBill_Update()
        {
            try
            {
                CombineBillAutoTimer.Stop();

                BllSearch<DmnTableinfo> bllSearch = new BllSearch<DmnTableinfo>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "ti.* ,c.CategoryName as CategoryName,om.NetAmount ,(select CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1 from tblOrderDetails WHERE status!=0 and AddonStatus in(1,0) and OrderMasterId =ti.OrderMasterId ) BillStatus";
                dmnSearch.Table = "tblTable_info  ti " +
                    "LEFT JOIN tblCategory_FTI c on c.Fcid=ti.Fcid " +
                    "LEFT JOIN tblOrderMaster om on om.OrderMasterId=ti.OrderMasterId and om.Status !=0";
                dmnSearch.WhereNdGroupBy = "where ti.Status=1";
                dmnSearch.OrderBy = "order by ti.tid";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    #region Check order status and set visible flase and true
                    int rowIndex_cate = 0;
                    foreach (var tabledata in gridSource)
                    {
                        string tlpname = "tlp_com" + tabledata.TableCode;
                        TableLayoutPanel tlp_box = tlp_table_dashboard.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                        if (tlp_box != null)
                        {
                            if(tabledata.BillStatus==0)
                            {
                                string lblname = "lb_netamount" + tabledata.TableCode;
                                Label lblnet = tlp_table_dashboard.Controls.Find(lblname, true).FirstOrDefault() as Label;
                                lblnet.Text = "\u20B9" + Convert.ToString(tabledata.NetAmount);
                                tlp_box.Visible = true;
                            }
                            else                            
                                tlp_box.Visible = false;                            
                        }
                    }
                    #endregion
                }

                CombineBillAutoTimer.Start();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
