using BLL;
using CustomMessageBox;
using Domain;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace FoodandBeverage.Forms
{
    public partial class FrmOrder : Form
    {
        public int tableId = 0;
        public FrmOrder(int tid)
        {
            tableId = tid;
            InitializeComponent();
        }
        // Function to create a rounded rectangle region
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

        #region Grid Declaration 
        // Registration Grid
        public static int islno = 0;
        public static int ibarcode = 1;
        public static int iname = 2;
        public static int iunit = 3;
        public static int iqty = 4;
        public static int iprate = 5;
        public static int iamount = 6;
        public static int idisper = 7;
        public static int idisamt = 8;
        public static int itaxper = 9;
        public static int itaxamt = 10;
        public static int itotal = 11;
        public static int iitemid = 12;
        public static int itax = 13;
        public static int itaxtype = 14;
        public static int israte = 15;
        public static int imrp = 16;
        public static int itype = 17;
        public static int ibarcode2 = 18;
        public static int itaxsettingid = 19;
        public static int iunitid = 20;
        public static int ipridno = 21;
        public static int iprodesno = 22;

        #endregion
        TableLayoutPanel tlp_table_dashboard_main = new TableLayoutPanel();
        TableLayoutPanel tlp_table_dashboard = new TableLayoutPanel();
        TableLayoutPanel tlp_searchproduct_sidebar = new TableLayoutPanel();
        TableLayoutPanel tlp_searchproduct_dashboard = new TableLayoutPanel();
        TableLayoutPanel tlp_price_list = new TableLayoutPanel();

        Color product_buttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
        Color seleproduct_buttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);

        int currentRowCount = 0;
        public int Productrate_ID = 0;
        public int saletype = 0;
        public int Proid_Prid = 0;

        private Point mouseDownLocation;
        public int r1 = 0;

        public int screenWidth;
        public int screenHeight;
        #endregion

        private void btn_tableorder_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            try
            {
                CheckRe_order();
                Control controlToRemove = tlp_calculation_main.GetControlFromPosition(0, 0);
                tlp_calculation_main.Controls.Remove(controlToRemove);
                tlp_calculation_main.Controls.Add(tlp_calculation_total, 0, 0);
                tlp_calculation_total.Visible = true;

                Screen primaryScreen = Screen.PrimaryScreen;
                screenWidth = primaryScreen.Bounds.Width;
                screenHeight = primaryScreen.Bounds.Height;

                Dgv_Selected_Item.Rows.Add(1);
                Dgv_Selected_Item.BorderStyle = BorderStyle.None;
                Dgv_Selected_Item.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                Dgv_Selected_Item.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                Dgv_Selected_Item.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                Dgv_Selected_Item.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                Dgv_Selected_Item.BackgroundColor = Color.White;

                Dgv_Selected_Item.EnableHeadersVisualStyles = false;
                Dgv_Selected_Item.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                Dgv_Selected_Item.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                Dgv_Selected_Item.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                textb_shape();
                txtname_search.Text = "Food Name .....";
                txtitemcode_serach.Text = "Item Code .....";
                txtother_descr.Text = "Description Type Here ...";
                txtdis_descr.Text = "Description Type Here ...";
                txtother_descr.ForeColor = Color.Gray;
                txtdis_descr.ForeColor = Color.Gray;

                #region Side Bar for Search box
                //sidebar 
                pnlsidebar_searchproduct.Controls.Clear();
                tlp_searchproduct_sidebar.Controls.Clear();
                tlp_searchproduct_sidebar.Dock = DockStyle.Fill;
                tlp_searchproduct_sidebar.AutoScroll = true;
                BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblCategory_FTI";
                dmnSearch.WhereNdGroupBy = "where Status=1 and CategoryType=0";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    int rowIndex = 0;
                    // Configure the Button
                    Button buttonfav = new Button();
                    buttonfav.Text = "Favouriate Item";
                    buttonfav.Name = "btn_fav";
                    buttonfav.Tag = 0;
                    buttonfav.BackColor = Color.Beige;
                    buttonfav.Width = 145;
                    buttonfav.Height = 40;

                    buttonfav.Font = new Font(buttonfav.Font.FontFamily, 8, FontStyle.Bold);
                    buttonfav.Click += new EventHandler(SidebuttonfavClickEventHandler);
                    tlp_searchproduct_sidebar.Controls.Add(buttonfav, 0, rowIndex);
                    rowIndex++;

                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        // Configure the Button
                        Button button = new Button();
                        button.Text = gridSource[i].CategoryName;
                        button.Name = "btn" + gridSource[i].CategoryName;
                        button.Tag = gridSource[i].Fcid;
                        button.BackColor = Color.Beige;
                        button.Width = 145;
                        button.Height = 40;

                        button.Font = new Font(button.Font.FontFamily, 8, FontStyle.Bold);
                        button.Click += new EventHandler(SidebuttonClickEventHandler);
                        tlp_searchproduct_sidebar.Controls.Add(button, 0, rowIndex);
                        rowIndex++;
                    }
                    pnlsidebar_searchproduct.Controls.Add(tlp_searchproduct_sidebar);
                    pnl_toggle_btn.Dock = DockStyle.Fill;
                    btn_sidebar_toggle.Location = new Point(pnl_toggle_btn.Width / 2 - btn_sidebar_toggle.Size.Width / 2, pnl_toggle_btn.Height / 2 - btn_sidebar_toggle.Size.Height / 2);
                }
                #endregion
            }
            catch (Exception ex)
            {

            }

        }
        public void CheckRe_order()
        {
            try
            {
                BllSearch<DmnOrderMaster> bllSearchmaster = new BllSearch<DmnOrderMaster>();
                DmnSearch dmnSearchmaster = new DmnSearch();
                dmnSearchmaster.Select = "*";
                dmnSearchmaster.Table = "tblOrderMaster ";
                dmnSearchmaster.WhereNdGroupBy = "where tid= " + tableId + " and Status=1";
                dmnSearchmaster.OrderBy = "";
                var gridSourcemaster = bllSearchmaster.GetSearchData("prdSearch", dmnSearchmaster);
                if(gridSourcemaster.Count>0)
                {
 
                    for (int j = 0; j < gridSourcemaster.Count; j++)
                    {
                        TableLayoutPanel tlp_orders = new TableLayoutPanel();
                        tlp_orders.Name = "tlp_orders" + gridSourcemaster[j].OrderMasterId;
                        tlp_orders.Tag = "OD"+ gridSourcemaster[j].OrderMasterId + " "+ gridSourcemaster[j].CreatedBy;
                        tlp_orders.Dock = DockStyle.Fill;

                        int rowIndex_cate = 1;
                        //Frist Row Cateory Heading
                        // Configure the Label
                        Label lblcategory_head = new Label();
                        lblcategory_head.Name = "lbcate_head_" + gridSourcemaster[j].OrderMasterId;
                        lblcategory_head.Tag = gridSourcemaster[j].OrderMasterId;
                        lblcategory_head.Text = "OD"+ gridSourcemaster[j].OrderMasterId;
                        lblcategory_head.ForeColor = Color.Black;
                        lblcategory_head.BackColor = Color.Transparent;
                        lblcategory_head.Dock = DockStyle.Top;
                        lblcategory_head.Font = new Font(lblcategory_head.Font.FontFamily, 12, FontStyle.Bold);
                        tlp_orders.Controls.Add(lblcategory_head, 0, 0);

                        BllSearch<DmnOrderDetails> bllSearch = new BllSearch<DmnOrderDetails>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "*";
                        dmnSearch.Table = "tblOrderDetails";
                        dmnSearch.WhereNdGroupBy = "where OrderMasterId= " + gridSourcemaster[j].OrderMasterId + " and Status=1";
                        dmnSearch.OrderBy = "";
                        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSource.Count > 0)
                        {
                            txtordermasterid.Text = Convert.ToString(gridSourcemaster[j].OrderMasterId);
                            txtentrie_descr.Text = Convert.ToString(gridSourcemaster[j].Remarks);
                            Dgv_Selected_Item.Rows.Add(gridSource.Count);

                            for (int i = 0; i < gridSource.Count; i++)
                            {
                                Dgv_Selected_Item.Rows[i].Cells[islno].Value = i + 1;
                                Dgv_Selected_Item.Rows[i].Cells[ibarcode].Value = gridSource[i].ItemBarCode;
                                Dgv_Selected_Item.Rows[i].Cells[iname].Value = gridSource[i].ItemName;
                                Dgv_Selected_Item.Rows[i].Cells[iitemid].Value = gridSource[i].ItemId;
                                Dgv_Selected_Item.Rows[i].Cells[iunit].Value = "Number";
                                //Dgv_Selected_Item.Rows[i].Cells[iqty].Value = gridSource[i].Quantity;
                                Dgv_Selected_Item.Rows[i].Cells[iprate].Value = gridSource[i].PurchaseRate;
                                Dgv_Selected_Item.Rows[i].Cells[iamount].Value = gridSource[i].Amount;
                                Dgv_Selected_Item.Rows[i].Cells[idisper].Value = gridSource[i].DiscountPer;
                                Dgv_Selected_Item.Rows[i].Cells[idisamt].Value = gridSource[i].DiscountAmount;
                                Dgv_Selected_Item.Rows[i].Cells[itaxper].Value = gridSource[i].TaxPer;
                                Dgv_Selected_Item.Rows[i].Cells[itaxamt].Value = gridSource[i].TaxAmount;
                                Dgv_Selected_Item.Rows[i].Cells[itotal].Value = gridSource[i].NetAmount;
                                Dgv_Selected_Item.Rows[i].Cells[itaxtype].Value = gridSource[i].TaxType;
                                Dgv_Selected_Item.Rows[i].Cells[itax].Value = gridSource[i].Tax;
                                Dgv_Selected_Item.Rows[i].Cells[israte].Value = gridSource[i].Srate;
                                Dgv_Selected_Item.Rows[i].Cells[imrp].Value = gridSource[i].MRP;
                                Dgv_Selected_Item.Rows[i].Cells[ipridno].Value = gridSource[i].PrId;
                                Dgv_Selected_Item.Rows[i].Cells[iunitid].Value = gridSource[i].UnitId;

                                #region Add data to the List
                                // Configure the TableLayoutPanel
                                TableLayoutPanel tlp_seletedItem_col = new TableLayoutPanel();
                                float[] columnPercentages = { 44.82f, 6.27f, 14.94f, 6.02f, 19.04f, 8.92f };
                                for (int x = 0; x < columnPercentages.Length; x++)
                                {
                                    tlp_seletedItem_col.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercentages[x]));
                                }
                                tlp_seletedItem_col.Dock = DockStyle.Top;
                                tlp_seletedItem_col.BackColor = Color.LightBlue;
                                tlp_seletedItem_col.Height = 45;

                                // Configure the Button for Item name
                                Button buttonitemname = new Button();

                                buttonitemname.BackColor = Color.Beige;
                                buttonitemname.Height = 40;
                                buttonitemname.Font = new Font(buttonitemname.Font.FontFamily, 8, FontStyle.Bold);


                                // Configure the Button for add Qty
                                Button buttonAdd = new Button();
                                buttonAdd.Text = "+";

                                buttonAdd.BackColor = Color.Beige;
                                buttonAdd.Margin = new Padding(0, 14, 0, 0);
                                buttonAdd.Font = new Font(buttonAdd.Font.FontFamily, 8, FontStyle.Bold);


                                // Configure the textbox enter QTY
                                TextBox txtqty = new TextBox();
                                txtqty.Text = "1";

                                txtqty.BackColor = Color.Beige;
                                txtqty.TextAlign = HorizontalAlignment.Center;
                                txtqty.Margin = new Padding(0, 14, 0, 0);
                                txtqty.Font = new Font(txtqty.Font.FontFamily, 8, FontStyle.Bold);

                                // Configure the textbox enter QTY
                                Button buttonRemove = new Button();
                                buttonRemove.Text = "-";

                                buttonRemove.BackColor = Color.Beige;
                                buttonRemove.Margin = new Padding(0, 14, 0, 0);
                                buttonRemove.Font = new Font(buttonRemove.Font.FontFamily, 8, FontStyle.Bold);


                                // Configure the textbox Price
                                TextBox txtprice = new TextBox();

                                txtprice.BackColor = Color.Beige;
                                txtprice.TextAlign = HorizontalAlignment.Center;
                                txtprice.Margin = new Padding(0, 14, 0, 0);
                                txtprice.Font = new Font(txtprice.Font.FontFamily, 8, FontStyle.Bold);

                                // Configure Item Delete Button
                                PictureBox pb_item_delete = new PictureBox();

                                pb_item_delete.BackColor = Color.Transparent;
                                pb_item_delete.Dock = DockStyle.Top;
                                pb_item_delete.Height = 40;
                                pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.Delete_Grid;
                                pb_item_delete.BackgroundImageLayout = ImageLayout.Zoom; // Change to Zoom instead of Stretch    

                                if (Convert.ToInt32(gridSource[i].PrId) > 0)
                                {
                                    tlp_seletedItem_col.Name = "tlp_seletedItem_col" + gridSource[i].PrId;
                                    tlp_seletedItem_col.Tag = gridSource[i].PrId;
                                    tlp_seletedItem_col.AccessibleDescription = "1";

                                    buttonitemname.Text = gridSource[i].ItemName;
                                    buttonitemname.Name = "buttonitemname_" + gridSource[i].ItemName;
                                    buttonitemname.Tag = gridSource[i].PrId;
                                    buttonitemname.Click += new EventHandler(buttonitemnameClickEventHandler);

                                    buttonAdd.Name = "buttonAdd_" + gridSource[i].PrId;
                                    buttonAdd.Tag = gridSource[i].PrId;
                                    buttonAdd.Click += new EventHandler(ButtonbuttonAddClickEventHandler);

                                    txtqty.Name = "txtqty_" + gridSource[i].PrId;
                                    txtqty.Tag = gridSource[i].PrId;

                                    buttonRemove.Name = "btn_remove_" + gridSource[i].PrId;
                                    buttonRemove.Tag = gridSource[i].PrId;
                                    buttonRemove.Click += new EventHandler(ButtonRemoveClickEventHandler);

                                    txtprice.Text = Convert.ToString(gridSource[i].Srate);
                                    txtprice.Name = "txtprice_" + gridSource[i].PrId;
                                    txtprice.Tag = gridSource[i].PrId;

                                    pb_item_delete.Name = "btn_remove_" + gridSource[i].PrId;
                                    pb_item_delete.Tag = gridSource[i].PrId;
                                    pb_item_delete.Click += new EventHandler(pb_item_deleteClickEventHandler);

                                    CalcTotalAmt_Add_prid(Convert.ToInt32(gridSource[i].PrId), gridSource[i].Quantity);
                                }
                                else
                                {
                                    tlp_seletedItem_col.Name = "tlp_seletedItem_col" + gridSource[i].ItemId;
                                    tlp_seletedItem_col.Tag = gridSource[i].ItemId;
                                    tlp_seletedItem_col.AccessibleDescription = "0";

                                    buttonitemname.Text = gridSource[i].ItemName;
                                    buttonitemname.Name = "buttonitemname_" + gridSource[i].ItemName;
                                    buttonitemname.Tag = gridSource[i].ItemId;
                                    buttonitemname.Click += new EventHandler(buttonitemnameClickEventHandler);

                                    buttonAdd.Name = "buttonAdd_" + gridSource[i].ItemId;
                                    buttonAdd.Tag = gridSource[i].ItemId;
                                    buttonAdd.Click += new EventHandler(ButtonbuttonAddClickEventHandler);

                                    txtqty.Name = "txtqty_" + gridSource[i].ItemId;
                                    txtqty.Tag = gridSource[i].ItemId;

                                    buttonRemove.Name = "btn_remove_" + gridSource[i].ItemId;
                                    buttonRemove.Tag = gridSource[i].ItemId;
                                    buttonRemove.Click += new EventHandler(ButtonRemoveClickEventHandler);

                                    txtprice.Text = Convert.ToString(gridSource[i].Srate);
                                    txtprice.Name = "txtprice_" + gridSource[i].ItemId;
                                    txtprice.Tag = gridSource[i].ItemId;

                                    pb_item_delete.Name = "btn_remove_" + gridSource[i].ItemId;
                                    pb_item_delete.Tag = gridSource[i].ItemId;
                                    pb_item_delete.Click += new EventHandler(pb_item_deleteClickEventHandler);

                                    CalcTotalAmt_Add(Convert.ToInt32(gridSource[i].ItemId), gridSource[i].Quantity);
                                }

                                //Dock Setting
                                tlp_seletedItem_col.Dock = DockStyle.Top;
                                buttonitemname.Dock = DockStyle.Top;
                                buttonAdd.Dock = DockStyle.Top;
                                txtqty.Dock = DockStyle.Fill;
                                buttonRemove.Dock = DockStyle.Top;
                                txtprice.Dock = DockStyle.Fill;

                                // Get the current row count
                                currentRowCount = tlp_seletedItem_col.RowCount;

                                // Add controls to TableLayoutPanel at the next row
                                tlp_seletedItem_col.Controls.Add(buttonitemname, 0, currentRowCount);
                                tlp_seletedItem_col.Controls.Add(buttonAdd, 1, currentRowCount);
                                tlp_seletedItem_col.Controls.Add(txtqty, 2, currentRowCount);
                                tlp_seletedItem_col.Controls.Add(buttonRemove, 3, currentRowCount);
                                tlp_seletedItem_col.Controls.Add(txtprice, 4, currentRowCount);
                                tlp_seletedItem_col.Controls.Add(pb_item_delete, 5, currentRowCount);

                                // Increment the row count
                                tlp_seletedItem_col.RowCount = currentRowCount + 1;
                                // Add the new TableLayoutPanel to tlp_seletedItem_main
                                tlp_orders.Controls.Add(tlp_seletedItem_col, 0, rowIndex_cate);

                                #endregion

                            }
                        }        
                        rowIndex_cate++;
                        tlp_seletedItem_main.Controls.Add(tlp_orders);
                    }
                   
                }

            }
            catch(Exception ex)
            {

            }
        }
        private void SidebuttonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "p.Product as Field1,p.ProductId as Field2,0 as Field3 ";
                dmnSearch.Table = "tblProduct p LEFT JOIN tblMenu m on m.ProductId=p.ProductId where p.Status=1 and p.Fcid= " + clickedControl.Tag + "and m.Status=1 " +
                    " Union Select Itemname as Field1,ItemId as Field2,Saletype as Field3 from tblItem";
                dmnSearch.WhereNdGroupBy = "where Fcid= " + clickedControl.Tag + " and Saletype=1 and Status=1";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                pnl_serachproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                tlp_searchproduct_dashboard.AutoScroll = true;
                if (gridSource.Count > 0)
                {
                    int rowIndex = 1;
                    int columnIndex = 0;
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        // Configure the Button
                        Button button = new Button();
                        button.Text = gridSource[i].Field1;
                        button.Name = "btn" + gridSource[i].Field1;
                        button.Tag = gridSource[i].Field2;
                        button.AccessibleDescription = gridSource[i].Field3;

                        if (screenWidth > 1024 && screenHeight > 767)
                        {
                            if (columnIndex == 3)
                            {
                                button.Width = 160;
                                button.Height = 55;
                                button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);
                                columnIndex = 0;
                                rowIndex++;
                            }
                            else
                            {
                                button.Width = 160;
                                button.Height = 55;
                                button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);
                            }
                        }
                        else
                        {
                            if (columnIndex == 2)
                            {
                                button.Width = 140;
                                button.Height = 40;
                                button.Font = new Font(button.Font.FontFamily, 8, FontStyle.Bold);
                                columnIndex = 0;
                                rowIndex++;
                            }
                            else
                            {
                                button.Width = 140;
                                button.Height = 40;
                                button.Font = new Font(button.Font.FontFamily, 8, FontStyle.Bold);
                            }
                        }


                        // Set the FlatAppearance properties to remove the border
                        button.FlatAppearance.BorderSize = 0; // Set border width to 0
                        button.FlatAppearance.BorderColor = button.BackColor;
                        button.FlatStyle = FlatStyle.Flat;
                        button.BackColor = product_buttoncolor;
                        button.ForeColor = Color.White;

                        //// Create a circular Region
                        button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));

                        //button.Dock = DockStyle.Fill;
                        button.Click += new EventHandler(DashboardButtonClickEventHandler);
                        tlp_searchproduct_dashboard.Controls.Add(button, columnIndex, rowIndex);
                        columnIndex++;
                    }
                    pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void DashboardButtonClickEventHandler(object sender, EventArgs e)
        {
            //item Chosse from serach Box 
            try
            {
                Control clickedControl = sender as Control;
                tlp_seletedItem_main.AutoScroll = true;
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch dmnSearch = new DmnSearch();
                if (Convert.ToInt32(clickedControl.AccessibleDescription) == 1)
                    CheckMultiplePrice(Convert.ToInt32(clickedControl.Tag));
                else
                {
                    dmnSearch.Select = "*";
                    dmnSearch.Table = "tblProduct";
                    dmnSearch.WhereNdGroupBy = "where Status=1 and ProductId = " + clickedControl.Tag;
                    dmnSearch.OrderBy = "";
                }
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    string tlpname = "tlp_seletedItem_col" + clickedControl.Tag;
                    TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                    if (tlp_sele != null)
                    {
                        string txtname = "txtqty_" + clickedControl.Tag;
                        TextBox qty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                        int cur_qty = Convert.ToInt32(qty.Text) + 1;
                        qty.Text = Convert.ToString(cur_qty);
                        CalcTotalAmt_Add(Convert.ToInt32(tlp_sele.Tag), 1);
                        tlp_sele.Focus();
                        netAmount();
                    }
                    else
                    {
                        CalcTotalAmt_Add(Convert.ToInt32(clickedControl.Tag), 1);
                        #region Grid Inseration

                        r1 = Dgv_Selected_Item.RowCount - 1;
                        Dgv_Selected_Item.Rows[r1].Cells[islno].Value = r1 + 1;
                        Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = gridSource[0].ItemCode;
                        Dgv_Selected_Item.Rows[r1].Cells[ibarcode2].Value = gridSource[0].ItemCode;
                        Dgv_Selected_Item.Rows[r1].Cells[iname].Value = gridSource[0].Product;
                        Dgv_Selected_Item.Rows[r1].Cells[iqty].Value = "1";
                        Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = gridSource[0].Price;
                        Dgv_Selected_Item.Rows[r1].Cells[israte].Value = gridSource[0].Price;
                        Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = gridSource[0].MRP;
                        Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = "0";
                        Dgv_Selected_Item.Rows[r1].Cells[idisamt].Value = "0";
                        Dgv_Selected_Item.Rows[r1].Cells[iitemid].Value = gridSource[0].ProductId;
                        Dgv_Selected_Item.Rows[r1].Cells[itax].Value = gridSource[0].Tax;
                        Dgv_Selected_Item.Rows[r1].Cells[itaxtype].Value = gridSource[0].TaxType;
                        Dgv_Selected_Item.Rows[r1].Cells[itype].Value = gridSource[0].Fcid;
                        Dgv_Selected_Item.Rows[r1].Cells[itaxsettingid].Value = gridSource[0].TaxSettingsId;
                        totalcellamount(r1);
                        Dgv_Selected_Item.Rows.Add(1);
                        //Dgv_Selected_Item.Rows[r1].Cells[ipridno].Value = gridSource[0].PrId;
                        #endregion
                        netAmount();
                        // Configure the TableLayoutPanel
                        TableLayoutPanel tlp_seletedItem_col = new TableLayoutPanel();
                        float[] columnPercentages = { 44.82f, 6.27f, 14.94f, 6.02f, 19.04f, 8.92f };
                        for (int i = 0; i < columnPercentages.Length; i++)
                        {
                            tlp_seletedItem_col.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercentages[i]));
                        }

                        tlp_seletedItem_col.Name = "tlp_seletedItem_col" + clickedControl.Tag;
                        tlp_seletedItem_col.Tag = clickedControl.Tag;
                        tlp_seletedItem_col.AccessibleDescription = "0";
                        tlp_seletedItem_col.Dock = DockStyle.Top;
                        tlp_seletedItem_col.BackColor = Color.LightBlue;
                        tlp_seletedItem_col.Height = 45;

                        // Configure the Button for Item name
                        Button buttonitemname = new Button();
                        buttonitemname.Text = gridSource[0].Product;
                        buttonitemname.Name = "buttonitemname_" + gridSource[0].Product; ;
                        buttonitemname.Tag = gridSource[0].ProductId; ;
                        buttonitemname.BackColor = Color.Beige;
                        buttonitemname.Height = 40;
                        buttonitemname.Font = new Font(buttonitemname.Font.FontFamily, 8, FontStyle.Bold);
                        buttonitemname.Click += new EventHandler(buttonitemnameClickEventHandler);

                        // Configure the Button for add Qty
                        Button buttonAdd = new Button();
                        buttonAdd.Text = "+";
                        buttonAdd.Name = "buttonAdd_" + gridSource[0].ProductId;
                        buttonAdd.Tag = gridSource[0].ProductId;
                        buttonAdd.BackColor = Color.Beige;
                        buttonAdd.Margin = new Padding(0, 14, 0, 0);
                        buttonAdd.Font = new Font(buttonAdd.Font.FontFamily, 8, FontStyle.Bold);
                        buttonAdd.Click += new EventHandler(ButtonbuttonAddClickEventHandler);

                        // Configure the textbox enter QTY
                        TextBox txtqty = new TextBox();
                        txtqty.Text = "1";
                        txtqty.Name = "txtqty_" + gridSource[0].ProductId;
                        txtqty.Tag = gridSource[0].ProductId;
                        txtqty.BackColor = Color.Beige;
                        txtqty.TextAlign = HorizontalAlignment.Center;
                        txtqty.Margin = new Padding(0, 14, 0, 0);
                        txtqty.Font = new Font(txtqty.Font.FontFamily, 8, FontStyle.Bold);

                        // Configure the textbox enter QTY
                        Button buttonRemove = new Button();
                        buttonRemove.Text = "-";
                        buttonRemove.Name = "btn_remove_" + gridSource[0].ProductId;
                        buttonRemove.Tag = gridSource[0].ProductId;
                        buttonRemove.BackColor = Color.Beige;
                        buttonRemove.Margin = new Padding(0, 14, 0, 0);
                        buttonRemove.Font = new Font(buttonRemove.Font.FontFamily, 8, FontStyle.Bold);
                        buttonRemove.Click += new EventHandler(ButtonRemoveClickEventHandler);

                        // Configure the textbox Price
                        TextBox txtprice = new TextBox();
                        txtprice.Text = Convert.ToString(gridSource[0].Price);
                        txtprice.Name = "txtprice_" + gridSource[0].ProductId;
                        txtprice.Tag = gridSource[0].ProductId;
                        txtprice.BackColor = Color.Beige;
                        txtprice.TextAlign = HorizontalAlignment.Center;
                        txtprice.Margin = new Padding(0, 14, 0, 0);
                        txtprice.Font = new Font(txtprice.Font.FontFamily, 8, FontStyle.Bold);

                        // Configure Item Delete Button
                        PictureBox pb_item_delete = new PictureBox();
                        pb_item_delete.Name = "btn_remove_" + gridSource[0].ProductId;
                        pb_item_delete.Tag = gridSource[0].ProductId;
                        pb_item_delete.BackColor = Color.Transparent;
                        pb_item_delete.Dock = DockStyle.Top;
                        pb_item_delete.Height = 40;
                        pb_item_delete.Click += new EventHandler(pb_item_deleteClickEventHandler);
                        pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.Delete_Grid;
                        pb_item_delete.BackgroundImageLayout = ImageLayout.Zoom; // Change to Zoom instead of Stretch    

                        //Dock Setting
                        tlp_seletedItem_col.Dock = DockStyle.Top;
                        buttonitemname.Dock = DockStyle.Top;
                        buttonAdd.Dock = DockStyle.Top;
                        txtqty.Dock = DockStyle.Fill;
                        buttonRemove.Dock = DockStyle.Top;
                        txtprice.Dock = DockStyle.Fill;

                        // Get the current row count
                        currentRowCount = tlp_seletedItem_col.RowCount;

                        // Add controls to TableLayoutPanel at the next row
                        tlp_seletedItem_col.Controls.Add(buttonitemname, 0, currentRowCount);
                        tlp_seletedItem_col.Controls.Add(buttonAdd, 1, currentRowCount);
                        tlp_seletedItem_col.Controls.Add(txtqty, 2, currentRowCount);
                        tlp_seletedItem_col.Controls.Add(buttonRemove, 3, currentRowCount);
                        tlp_seletedItem_col.Controls.Add(txtprice, 4, currentRowCount);
                        tlp_seletedItem_col.Controls.Add(pb_item_delete, 5, currentRowCount);

                        // Increment the row count
                        tlp_seletedItem_col.RowCount = currentRowCount + 1;
                        // Add the new TableLayoutPanel to tlp_seletedItem_main
                        tlp_seletedItem_main.Controls.Add(tlp_seletedItem_col);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public void CheckMultiplePrice(int id)
        {
            BllSearch<DmnGridFields> bllSearch1 = new BllSearch<DmnGridFields>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "i.Itemname Field1, pr.SRate Field2, pr.QOH Field3,pr.Id Field4,i.ItemId Field5";
            objSearch.Table = "tblitem i left join tblProductRate pr on pr.ProductId = i.ItemId";
            objSearch.WhereNdGroupBy = "where i.status = 1 and i.itemid=" + id;
            objSearch.OrderBy = "";
            var gridSource = bllSearch1.GetSearchData("prdSearch", objSearch);
            if (gridSource.Count > 1)
            {
                Control controlclose;
                pnlprice.Controls.Clear();
                tlp_price_list.Controls.Clear();
                tlp_price_list.Dock = DockStyle.Fill;
                tlp_price_list.ColumnCount = 1;
                tlp_price_list.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                tlp_price_list.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
                tlp_price_list.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
                Button closebutton = new Button();
                closebutton.Text = "X";
                controlclose = closebutton;
                closebutton.BackgroundImage = FoodandBeverage.Properties.Resources.close_price_change;
                closebutton.BackgroundImageLayout = ImageLayout.Stretch;
                closebutton.FlatStyle = FlatStyle.Flat;
                closebutton.ForeColor = System.Drawing.Color.White;
                closebutton.Font = new Font("Georgia", 14);
                closebutton.Width = 50;
                closebutton.Height = 50;
                closebutton.Dock = DockStyle.Right;
                closebutton.Click += new EventHandler(closebuttonClickEventHandler);
                tlp_price_list.RowCount++;
                tlp_price_list.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                tlp_price_list.Controls.Add(controlclose, 0, 0);

                Control control1, controlsrate, controlqty;
                int rowIndex = 2;
                for (int i = 0; i < gridSource.Count; i++)
                {
                    Button pricebutton = new Button();
                    pricebutton.Text = "RS. " + (gridSource[i].Field2).ToString() + "  QTY : " + (gridSource[i].Field3).ToString(); ;
                    pricebutton.Name = (gridSource[i].Field1).ToString();
                    pricebutton.Tag = gridSource[i].Field5;
                    pricebutton.AccessibleDescription = gridSource[i].Field4;

                    control1 = pricebutton;
                    //pricebutton.Dock = DockStyle.Fill;
                    pricebutton.BackgroundImage = FoodandBeverage.Properties.Resources.Button_Price_change;
                    pricebutton.BackgroundImageLayout = ImageLayout.Stretch;
                    pricebutton.FlatStyle = FlatStyle.Flat;
                    pricebutton.ForeColor = System.Drawing.Color.White;
                    pricebutton.Font = new Font("Georgia", 14);
                    pricebutton.Width = 300;
                    pricebutton.Height = 70;
                    // Add the event handler for the button
                    pricebutton.Click += new EventHandler(MyButtonClickEventHandler);
                    // Add the controls to the row
                    tlp_price_list.RowCount++;
                    tlp_price_list.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                    tlp_price_list.Controls.Add(control1, 0, rowIndex);
                    rowIndex++;
                }
                // Add the TableLayoutPanel to the form

                pnlprice.Visible = true;
                pnlprice.Location = new Point(this.ClientSize.Width / 2 - pnlprice.Size.Width / 2, this.ClientSize.Height / 2 - pnlprice.Size.Height / 2);
                pnlprice.Controls.Add(tlp_price_list);

            }

        }
        private void closebuttonClickEventHandler(object sender, EventArgs e)
        {
            pnlprice.Visible = false;
        }
        private void MyButtonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                Productrate_ID = Convert.ToInt32(clickedControl.Tag);
                pnlprice.Visible = false;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();

                dmnSearch.Select = "Itemname as Field1,ItemId as Field2,ItemCode_UPC as Field3,pr.prate as Field4,pr.srate as Field5," +
                "pr.MRP as Field6,i.Tax as Field7,i.TaxType as Field8,i.TaxSettingsId as Field9,pr.QOH as Field10," +
                "i.unit as Field11,u.UnitName as Field12,i.Fcid as Field13,i.TaxSettingsId as Field14,pr.id Field15";
                dmnSearch.Table = "tblItem i LEFT JOIN tblProductRate pr on  pr.ProductId=i.ItemId LEFT JOIN tblUnit u on  u.UId=i.unit";
                dmnSearch.WhereNdGroupBy = "where i.Status=1 and pr.ProductId = " + clickedControl.Tag + " and pr.id = " + Convert.ToInt32(clickedControl.AccessibleDescription);
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

                string tlpname = "tlp_seletedItem_col" + clickedControl.AccessibleDescription;
                TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                if (string.IsNullOrEmpty(txtGrandTotal.Text))
                    txtGrandTotal.Text = "0";
                if (tlp_sele != null)
                {
                    string txtname = "txtqty_" + clickedControl.AccessibleDescription;
                    TextBox qty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                    int cur_qty = Convert.ToInt32(qty.Text) + 1;
                    qty.Text = Convert.ToString(cur_qty);

                    for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                    {

                        if (Convert.ToInt32(gridSource[0].Field15) == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value))
                        {
                            Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                            totalcellamount(i);
                            break;
                        }
                    }

                    decimal totalamt = Convert.ToDecimal(gridSource[0].Field5) + Convert.ToDecimal(txtGrandTotal.Text);
                    txtGrandTotal.Text = Convert.ToString(totalamt);
                    netAmount();
                    tlp_sele.Focus();

                }
                else
                {
                    for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                    {

                        if (Convert.ToInt32(gridSource[0].Field15) == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value))
                        {
                            Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                            totalcellamount(i);
                            break;
                        }
                    }
                    decimal totalamt = Convert.ToDecimal(gridSource[0].Field5) + Convert.ToDecimal(txtGrandTotal.Text);
                    txtGrandTotal.Text = Convert.ToString(totalamt);

                    #region Grid Inseration
                    r1 = Dgv_Selected_Item.RowCount - 1;

                    Dgv_Selected_Item.Rows[r1].Cells[islno].Value = r1 + 1;
                    Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = gridSource[0].Field3;
                    Dgv_Selected_Item.Rows[r1].Cells[ibarcode2].Value = gridSource[0].Field3;
                    Dgv_Selected_Item.Rows[r1].Cells[iname].Value = gridSource[0].Field1;
                    Dgv_Selected_Item.Rows[r1].Cells[iunit].Value = gridSource[0].Field12;
                    Dgv_Selected_Item.Rows[r1].Cells[iunitid].Value = gridSource[0].Field11;
                    Dgv_Selected_Item.Rows[r1].Cells[iqty].Value = "1";
                    Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = gridSource[0].Field4;
                    Dgv_Selected_Item.Rows[r1].Cells[israte].Value = gridSource[0].Field5;
                    Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = gridSource[0].Field6;
                    Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = "0";
                    Dgv_Selected_Item.Rows[r1].Cells[idisamt].Value = "0";
                    //if (gridSource[0].Field8 == "GST")
                    //{
                    //    Dgv_Selected_Item.Rows[r1].Cells[itaxper].Value = gridSource[0].IgstPrcnt;
                    //}
                    //if (gridSource[0].Field8 == "VAT")
                    //{
                    //    Dgv_Selected_Item.Rows[r1].Cells[itaxper].Value = gridSource[0].VatPrcnt;
                    //}
                    Dgv_Selected_Item.Rows[r1].Cells[iitemid].Value = gridSource[0].Field2;
                    Dgv_Selected_Item.Rows[r1].Cells[itax].Value = gridSource[0].Field7;
                    Dgv_Selected_Item.Rows[r1].Cells[itaxtype].Value = gridSource[0].Field8;
                    Dgv_Selected_Item.Rows[r1].Cells[itype].Value = gridSource[0].Field13;
                    Dgv_Selected_Item.Rows[r1].Cells[itaxsettingid].Value = gridSource[0].Field14;
                    Dgv_Selected_Item.Rows[r1].Cells[ipridno].Value = gridSource[0].Field15;
                    totalcellamount(r1);
                    netAmount();
                    Dgv_Selected_Item.Rows.Add(1);

                    #endregion

                    // Configure the TableLayoutPanel
                    TableLayoutPanel tlp_seletedItem_col = new TableLayoutPanel();
                    float[] columnPercentages = { 44.82f, 6.27f, 14.94f, 6.02f, 19.04f, 8.92f };
                    for (int i = 0; i < columnPercentages.Length; i++)
                    {
                        tlp_seletedItem_col.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercentages[i]));
                    }

                    tlp_seletedItem_col.Name = "tlp_seletedItem_col" + Convert.ToInt32(gridSource[0].Field15);
                    tlp_seletedItem_col.Tag = clickedControl.Tag;
                    tlp_seletedItem_col.AccessibleDescription = "1";
                    tlp_seletedItem_col.BackColor = Color.LightBlue;
                    tlp_seletedItem_col.Height = 45;

                    // Configure the Button for Item name
                    Button buttonitemname = new Button();
                    buttonitemname.Text = gridSource[0].Field1;
                    buttonitemname.Name = "buttonitemname_" + gridSource[0].Field15; ;
                    buttonitemname.Tag = gridSource[0].Field15;
                    buttonitemname.AccessibleDescription = "1";
                    buttonitemname.BackColor = Color.Beige;
                    buttonitemname.Height = 40;
                    buttonitemname.Font = new Font(buttonitemname.Font.FontFamily, 8, FontStyle.Bold);
                    buttonitemname.Click += new EventHandler(buttonitemnameClickEventHandler);

                    // Configure the Button for add Qty
                    Button buttonAdd = new Button();
                    buttonAdd.Text = "+";
                    buttonAdd.Name = "buttonAdd_" + gridSource[0].Field15;
                    buttonAdd.Tag = gridSource[0].Field15;
                    buttonAdd.BackColor = Color.Beige;
                    buttonAdd.Margin = new Padding(0, 14, 0, 0);
                    buttonAdd.Font = new Font(buttonAdd.Font.FontFamily, 8, FontStyle.Bold);
                    buttonAdd.Click += new EventHandler(ButtonbuttonAddClickEventHandler);

                    // Configure the textbox enter QTY
                    TextBox txtqty = new TextBox();
                    txtqty.Text = "1";
                    txtqty.Name = "txtqty_" + gridSource[0].Field15;
                    txtqty.Tag = gridSource[0].Field15;
                    txtqty.BackColor = Color.Beige;
                    txtqty.TextAlign = HorizontalAlignment.Center;
                    txtqty.Margin = new Padding(0, 14, 0, 0);
                    txtqty.Font = new Font(txtqty.Font.FontFamily, 8, FontStyle.Bold);

                    // Configure the textbox enter QTY
                    Button buttonRemove = new Button();
                    buttonRemove.Text = "-";
                    buttonRemove.Name = "btn_remove_" + gridSource[0].Field15;
                    buttonRemove.Tag = gridSource[0].Field15;
                    buttonRemove.BackColor = Color.Beige;
                    buttonRemove.Margin = new Padding(0, 14, 0, 0);
                    buttonRemove.Font = new Font(buttonRemove.Font.FontFamily, 8, FontStyle.Bold);
                    buttonRemove.Click += new EventHandler(ButtonRemoveClickEventHandler);

                    // Configure the textbox Price
                    TextBox txtprice = new TextBox();
                    txtprice.Text = Convert.ToString(gridSource[0].Field5);
                    txtprice.Name = "txtprice_" + gridSource[0].Field15;
                    txtprice.Tag = gridSource[0].Field15;
                    txtprice.BackColor = Color.Beige;
                    txtprice.TextAlign = HorizontalAlignment.Center;
                    txtprice.Margin = new Padding(0, 14, 0, 0);
                    txtprice.Font = new Font(txtprice.Font.FontFamily, 8, FontStyle.Bold);

                    // Configure Item Delete Button
                    PictureBox pb_item_delete = new PictureBox();
                    pb_item_delete.Name = "btn_remove_" + gridSource[0].Field15;
                    pb_item_delete.Tag = gridSource[0].Field15;
                    pb_item_delete.BackColor = Color.Transparent;
                    pb_item_delete.Dock = DockStyle.Top;
                    pb_item_delete.Height = 40;
                    pb_item_delete.Click += new EventHandler(pb_item_deleteClickEventHandler);
                    pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.Delete_Grid;
                    pb_item_delete.BackgroundImageLayout = ImageLayout.Zoom; // Change to Zoom instead of Stretch

                    //Dock Setting
                    tlp_seletedItem_col.Dock = DockStyle.Top;
                    buttonitemname.Dock = DockStyle.Top;
                    buttonAdd.Dock = DockStyle.Top;
                    txtqty.Dock = DockStyle.Fill;
                    buttonRemove.Dock = DockStyle.Top;
                    txtprice.Dock = DockStyle.Fill;


                    // Get the current row count
                    currentRowCount = tlp_seletedItem_col.RowCount;

                    // Add controls to TableLayoutPanel at the next row
                    tlp_seletedItem_col.Controls.Add(buttonitemname, 0, currentRowCount);
                    tlp_seletedItem_col.Controls.Add(buttonAdd, 1, currentRowCount);
                    tlp_seletedItem_col.Controls.Add(txtqty, 2, currentRowCount);
                    tlp_seletedItem_col.Controls.Add(buttonRemove, 3, currentRowCount);
                    tlp_seletedItem_col.Controls.Add(txtprice, 4, currentRowCount);
                    tlp_seletedItem_col.Controls.Add(pb_item_delete, 5, currentRowCount);

                    // Increment the row count
                    tlp_seletedItem_col.RowCount = currentRowCount + 1;
                    // Add the new TableLayoutPanel to tlp_seletedItem_main
                    tlp_seletedItem_main.Controls.Add(tlp_seletedItem_col);
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void totalcellamount(int cellproductid)
        {
            try
            {
                if (Dgv_Selected_Item.Rows[cellproductid].Cells[iitemid].Value != "" && Dgv_Selected_Item.Rows[cellproductid].Cells[iprate].Value != "" && Dgv_Selected_Item.Rows[cellproductid].Cells[iqty].Value != "")
                {
                    decimal amount = Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[iprate].Value) * Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[iqty].Value);
                    Dgv_Selected_Item.Rows[cellproductid].Cells[iamount].Value = amount.ToString("N2");

                    cellDiscountPercentage(cellproductid);
                    cellDiscountAmount(cellproductid);

                    decimal discount = Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[idisamt].Value);
                    cellTaxAmount(cellproductid, amount - discount);
                    if (Convert.ToString(Dgv_Selected_Item.Rows[cellproductid].Cells[itax].Value) == "Exclusive")
                    {
                        decimal tot = (amount - Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[idisamt].Value))
                        + Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[itaxamt].Value);
                        Dgv_Selected_Item.Rows[cellproductid].Cells[itotal].Value = tot.ToString("N2");
                    }
                    if (Convert.ToString(Dgv_Selected_Item.Rows[cellproductid].Cells[itax].Value) == "Inclusive")
                    {
                        decimal tot = (amount - Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[idisamt].Value));
                        Dgv_Selected_Item.Rows[cellproductid].Cells[itotal].Value = tot.ToString("N2");
                    }
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
                string cellpopupid = Convert.ToString(Dgv_Selected_Item.Rows[cellproductid].Cells[iitemid].Value);
                if (cellpopupid != "")
                {
                    BllSearch<DmnTaxSettings> _bllSearch1 = new BllSearch<DmnTaxSettings>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "TaxType, IgstPrcnt,VatPrcnt";
                    dmnSearch.Table = "tblTaxSettings";
                    dmnSearch.WhereNdGroupBy = " where status=1 and TaxSettingsId= " + Dgv_Selected_Item.Rows[cellproductid].Cells[itaxsettingid].Value;
                    dmnSearch.OrderBy = "";
                    var gridSource = _bllSearch1.GetSearchData("prdSearch", dmnSearch);
                    if (gridSource[0].TaxType == "GST")
                    {
                        decimal TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].IgstPrcnt) * amount / 100);
                        Dgv_Selected_Item.Rows[cellproductid].Cells[itaxamt].Value = TaxAmount.ToString("N2");
                        Dgv_Selected_Item.Rows[cellproductid].Cells[itaxper].Value = gridSource[0].IgstPrcnt;
                    }
                    if (gridSource[0].TaxType == "VAT")
                    {
                        decimal TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].VatPrcnt) * amount / 100);
                        Dgv_Selected_Item.Rows[cellproductid].Cells[itaxamt].Value = TaxAmount.ToString("N2");
                        Dgv_Selected_Item.Rows[cellproductid].Cells[itaxper].Value = gridSource[0].VatPrcnt;
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
                if (Dgv_Selected_Item.Rows[cellproductid].Cells[idisper].Value != "" && Dgv_Selected_Item.Rows[cellproductid].Cells[iamount].Value != "")
                {
                    decimal disAmnt = (Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[idisper].Value) * Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[iamount].Value)) / 100;
                    Dgv_Selected_Item.Rows[cellproductid].Cells[idisamt].Value = disAmnt.ToString("N2");
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
                if (Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[iamount].Value) > 0 && Dgv_Selected_Item.Rows[cellproductid].Cells[idisamt].Value != "")
                {
                    decimal disPerc = (Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[idisamt].Value) / Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[iamount].Value)) * 100;
                    Dgv_Selected_Item.Rows[cellproductid].Cells[idisper].Value = disPerc.ToString("N2");
                }
            }
            catch (Exception ex)
            {

            }
        }


        private void pb_item_deleteClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                string txtname = "txtqty_" + clickedControl.Tag;
                string tlpname = "tlp_seletedItem_col" + clickedControl.Tag;
                // Find the TableLayoutPanel control by name
                TextBox txtqty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                if (txtqty != null)
                {
                    int cur_qty = 0;
                    TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                    if (tlp_sele != null)
                        if (Convert.ToInt32(tlp_sele.AccessibleDescription) == 1)
                            CalcTotalAmt_remove_Prid(Convert.ToInt32(clickedControl.Tag), cur_qty);
                        else
                            CalcTotalAmt_remove(Convert.ToInt32(tlp_sele.Tag), cur_qty);
                    foreach (Control control in tlp_seletedItem_main.Controls)
                    {
                        if (control.Name == tlpname)
                        {
                            tlp_seletedItem_main.Controls.Remove(control);
                            break;
                        }
                    }
                    netAmount();
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void ButtonRemoveClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                string txtname = "txtqty_" + clickedControl.Tag;
                string tlpname = "tlp_seletedItem_col" + clickedControl.Tag;
                // Find the TableLayoutPanel control by name
                TextBox txtqty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                if (txtqty != null)
                {
                    int cur_qty = Convert.ToInt32(txtqty.Text) - 1;
                    if (cur_qty == 0)
                    {
                        if (tlp_sele != null)
                            if (Convert.ToInt32(tlp_sele.AccessibleDescription) == 1)
                                CalcTotalAmt_remove_Prid(Convert.ToInt32(clickedControl.Tag), cur_qty);
                            else
                                CalcTotalAmt_remove(Convert.ToInt32(tlp_sele.Tag), cur_qty);
                        foreach (Control control in tlp_seletedItem_main.Controls)
                        {
                            if (control.Name == tlpname)
                            {
                                tlp_seletedItem_main.Controls.Remove(control);
                                break;
                            }
                        }

                    }
                    else
                    {
                        txtqty.Text = Convert.ToString(cur_qty);
                        if (tlp_sele != null)
                            if (Convert.ToInt32(tlp_sele.AccessibleDescription) == 1)
                                CalcTotalAmt_remove_Prid(Convert.ToInt32(clickedControl.Tag), cur_qty);
                            else
                                CalcTotalAmt_remove(Convert.ToInt32(tlp_sele.Tag), cur_qty);
                    }
                    netAmount();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void buttonitemnameClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;

                saletype = Convert.ToInt32(clickedControl.AccessibleDescription);
                Proid_Prid = Convert.ToInt32(clickedControl.Tag);
                txt_product_des.Text = "Description Type Here .....";
                txt_product_des.ForeColor = Color.Gray;
                lblheader_product_des.Text = clickedControl.Text;
                Control parentControl = tlp_seletedItem_main;
                pnl_products_descr.Location = new Point(this.ClientSize.Width / 2 - pnl_products_descr.Size.Width / 2, this.ClientSize.Height / 2 - pnl_products_descr.Size.Height / 2);
                pnl_products_descr.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }

        private void ButtonbuttonAddClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                string txtqty = "txtqty_" + clickedControl.Tag;
                string buttonname = "btn_sele_" + clickedControl.Tag;
                string tlpname = "tlp_seletedItem_col" + clickedControl.Tag;
                // Find the TableLayoutPanel control by name
                TextBox qty = this.Controls.Find(txtqty, true).FirstOrDefault() as TextBox;
                TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                if (qty != null)
                {
                    int cur_qty = Convert.ToInt32(qty.Text) + 1;
                    qty.Text = Convert.ToString(cur_qty);
                    if (tlp_sele != null)
                    {
                        if (Convert.ToInt32(tlp_sele.AccessibleDescription) == 1)
                            CalcTotalAmt_Add_prid(Convert.ToInt32(clickedControl.Tag), cur_qty);
                        else
                            CalcTotalAmt_Add(Convert.ToInt32(tlp_sele.Tag), cur_qty);
                    }
                    netAmount();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void CalcTotalAmt_Add_prid(int prid, int qty)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "SRate as Field1";
                objSearch.Table = "tblProductRate";
                objSearch.WhereNdGroupBy = "where id = " + prid;
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    if (string.IsNullOrEmpty(txtGrandTotal.Text))
                        txtGrandTotal.Text = "0";

                    for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                    {

                        if (prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value))
                        {
                            Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                            totalcellamount(i);
                            break;
                        }
                    }
                    decimal totalamt = Convert.ToDecimal(gridSource[0].Field1) + Convert.ToDecimal(txtGrandTotal.Text);
                    txtGrandTotal.Text = Convert.ToString(totalamt);
                    txtnetamount.Text = Convert.ToString(totalamt);
                    netAmount();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void CalcTotalAmt_Add(int pid, int qty)
        {
            try
            {
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblProduct";
                objSearch.WhereNdGroupBy = "where ProductId=" + pid;
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    if (string.IsNullOrEmpty(txtGrandTotal.Text))
                        txtGrandTotal.Text = "0";

                    for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                    {

                        if (pid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value))
                        {
                            Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                            totalcellamount(i);
                            break;
                        }
                    }
                    decimal totalamt = Convert.ToDecimal(gridSource[0].Price) + Convert.ToDecimal(txtGrandTotal.Text);
                    txtGrandTotal.Text = Convert.ToString(totalamt);
                    txtnetamount.Text = Convert.ToString(totalamt);
                    netAmount();
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void CalcTotalAmt_remove_Prid(int prid, int qty)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "SRate as Field1";
                objSearch.Table = "tblProductRate";
                objSearch.WhereNdGroupBy = "where id = " + prid;
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    if (string.IsNullOrEmpty(txtGrandTotal.Text))
                        txtGrandTotal.Text = "0";
                    if (qty == 0)
                    {
                        string txtqty = "txtqty_" + prid;
                        TextBox totalqty = this.Controls.Find(txtqty, true).FirstOrDefault() as TextBox;
                        decimal TotalPrice = Convert.ToDecimal(gridSource[0].Field1) * Convert.ToDecimal(totalqty.Text);
                        decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) - TotalPrice;
                        txtGrandTotal.Text = Convert.ToString(totalamt);
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value))
                            {
                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) - 1;
                                totalcellamount(i);
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value))
                            {
                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) - 1;
                                totalcellamount(i);
                                break;
                            }
                        }
                        decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) - Convert.ToDecimal(gridSource[0].Field1);
                        txtGrandTotal.Text = Convert.ToString(totalamt);
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void CalcTotalAmt_remove(int pid, int qty)
        {
            try
            {
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblProduct";
                objSearch.WhereNdGroupBy = "where ProductId=" + pid;
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    if (string.IsNullOrEmpty(txtGrandTotal.Text))
                        txtGrandTotal.Text = "0";

                    if (qty == 0)
                    {
                        string txtqty = "txtqty_" + pid;
                        TextBox totalqty = this.Controls.Find(txtqty, true).FirstOrDefault() as TextBox;
                        decimal TotalPrice = Convert.ToDecimal(gridSource[0].Price) * Convert.ToDecimal(totalqty.Text);
                        decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) - TotalPrice;
                        txtGrandTotal.Text = Convert.ToString(totalamt);
                        txtnetamount.Text= Convert.ToString(totalamt);
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (pid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value))
                            {
                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) - 1;
                                totalcellamount(i);
                                break;
                            }
                        }
 
                    }
                    else
                    {
                        decimal totalamt = 0;
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (pid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value))
                            {
                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) - 1;
                                totalcellamount(i);
                                break;
                            }
                        }
                         totalamt = Convert.ToDecimal(txtGrandTotal.Text) - Convert.ToDecimal(gridSource[0].Price);
                        txtGrandTotal.Text = Convert.ToString(totalamt);
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void SidebuttonfavClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblProduct p LEFT JOIN tblMenu m on m.ProductId=p.ProductId ";
                dmnSearch.WhereNdGroupBy = " where p.Status=1 and p.Fcid= " + clickedControl.Tag + "and m.Status=1";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                pnl_serachproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                tlp_searchproduct_dashboard.AutoScroll = true;
                if (gridSource.Count > 0)
                {
                    int rowIndex = 1;
                    int columnIndex = 0;
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        if (columnIndex == 3)
                        {
                            columnIndex = 0;
                            rowIndex++;
                        }

                        // Configure the Button
                        Button button = new Button();
                        button.Text = gridSource[i].Product;
                        button.Name = "btn" + gridSource[i].Product;
                        button.Tag = gridSource[i].ProductId;
                        button.Width = 160;
                        button.Height = 55;

                        // Set the FlatAppearance properties to remove the border
                        button.FlatAppearance.BorderSize = 0; // Set border width to 0
                        button.FlatAppearance.BorderColor = button.BackColor;
                        button.FlatStyle = FlatStyle.Flat;
                        button.BackColor = product_buttoncolor;
                        button.ForeColor = Color.White;

                        //// Create a circular Region
                        button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                        //System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                        //path.AddEllipse(0, 0, button.Width, button.Height);
                        //button.Region = new Region(path);

                        button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);
                        //button.Dock = DockStyle.Fill;
                        button.Click += new EventHandler(DashboardButtonClickEventHandler);
                        tlp_searchproduct_dashboard.Controls.Add(button, columnIndex, rowIndex);
                        columnIndex++;
                    }
                    pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                }
                //else
                //{
                //    CMessageBox.Show("No Products On This Category", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
            }
            catch (Exception ex)
            {
            }
        }




        private void txtname_search_Enter(object sender, EventArgs e)
        {
            if (txtname_search.Text == "Food Name .....")
            {
                txtname_search.Text = "";
                txtname_search.ForeColor = Color.Black;
            }

        }

        private void txtitemcode_serach_Enter(object sender, EventArgs e)
        {
            if (txtitemcode_serach.Text == "Item Code .....")
            {
                txtitemcode_serach.Text = "";
                txtitemcode_serach.ForeColor = Color.Black;
            }
        }

        private void txtname_search_Leave(object sender, EventArgs e)
        {
            if (txtname_search.Text == "")
            {
                txtname_search.Text = "Food Name .....";
                txtname_search.ForeColor = Color.Black;
            }
        }

        private void txtitemcode_serach_Leave(object sender, EventArgs e)
        {
            if (txtitemcode_serach.Text == "")
            {
                txtitemcode_serach.Text = "Item Code .....";
                txtitemcode_serach.ForeColor = Color.Black;
            }
        }

        private void txtname_search_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "p.Product as Field1,p.ProductId as Field2,0 as Field3 ";
                    dmnSearch.Table = "tblProduct p LEFT JOIN tblMenu m on m.ProductId=p.ProductId where p.Status=1 and m.Status=1  and p.Product LIKE '%" + txtname_search.Text + "%'" +
                        " Union Select Itemname as Field1,ItemId as Field2,Saletype as Field3 from tblItem";
                    dmnSearch.WhereNdGroupBy = "where Saletype=1 and Status=1 and Itemname LIKE '%" + txtname_search.Text + "%'";
                    dmnSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    pnl_serachproduct_dashboard.Controls.Clear();
                    tlp_searchproduct_dashboard.Controls.Clear();
                    tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                    tlp_searchproduct_dashboard.AutoScroll = true;
                    if (gridSource.Count > 0)
                    {
                        int rowIndex = 1;
                        int columnIndex = 0;


                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            // Configure the Button
                            Button button = new Button();
                            button.Text = gridSource[i].Field1;
                            button.Name = "btn" + gridSource[i].Field1;
                            button.Tag = gridSource[i].Field2;
                            button.AccessibleDescription = gridSource[i].Field3;

                            if (screenWidth > 1024 && screenHeight > 767)
                            {
                                if (columnIndex == 3)
                                {
                                    button.Width = 160;
                                    button.Height = 55;
                                    button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);
                                    columnIndex = 0;
                                    rowIndex++;
                                }
                                else
                                {
                                    button.Width = 160;
                                    button.Height = 55;
                                    button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);
                                }
                            }
                            else
                            {
                                if (columnIndex == 2)
                                {
                                    button.Width = 140;
                                    button.Height = 40;
                                    button.Font = new Font(button.Font.FontFamily, 8, FontStyle.Bold);
                                    columnIndex = 0;
                                    rowIndex++;
                                }
                                else
                                {
                                    button.Width = 140;
                                    button.Height = 40;
                                    button.Font = new Font(button.Font.FontFamily, 8, FontStyle.Bold);
                                }
                            }


                            // Set the FlatAppearance properties to remove the border
                            button.FlatAppearance.BorderSize = 0; // Set border width to 0
                            button.FlatAppearance.BorderColor = button.BackColor;
                            button.FlatStyle = FlatStyle.Flat;
                            button.BackColor = product_buttoncolor;
                            button.ForeColor = Color.White;
                            button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                            button.Click += new EventHandler(DashboardButtonClickEventHandler);
                            tlp_searchproduct_dashboard.Controls.Add(button, columnIndex, rowIndex);
                            columnIndex++;
                        }
                        pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                    }
                    else
                        CMessageBox.Show("No Products", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtitemcode_serach_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "p.Product as Field1,p.ProductId as Field2,0 as Field3 ";
                    dmnSearch.Table = "tblProduct p LEFT JOIN tblMenu m on m.ProductId=p.ProductId where p.Status=1 and m.Status=1  and ItemCode = '" + txtitemcode_serach.Text + "'" +
                        " Union Select Itemname as Field1,ItemId as Field2,Saletype as Field3 from tblItem";
                    dmnSearch.WhereNdGroupBy = "where Saletype=1 and Status=1 and ItemCode_UPC LIKE '%" + txtitemcode_serach.Text + "%'";
                    dmnSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    pnl_serachproduct_dashboard.Controls.Clear();
                    tlp_searchproduct_dashboard.Controls.Clear();
                    tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                    tlp_searchproduct_dashboard.AutoScroll = true;
                    if (gridSource.Count > 0)
                    {
                        int rowIndex = 1;
                        int columnIndex = 0;
                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            // Configure the Button
                            Button button = new Button();
                            button.Text = gridSource[i].Field1;
                            button.Name = "btn" + gridSource[i].Field1;
                            button.Tag = gridSource[i].Field2;
                            button.AccessibleDescription = gridSource[i].Field3;

                            if (screenWidth > 1024 && screenHeight > 767)
                            {
                                if (columnIndex == 3)
                                {
                                    button.Width = 160;
                                    button.Height = 55;
                                    button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);
                                    columnIndex = 0;
                                    rowIndex++;
                                }
                                else
                                {
                                    button.Width = 160;
                                    button.Height = 55;
                                    button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);
                                }
                            }
                            else
                            {
                                if (columnIndex == 2)
                                {
                                    button.Width = 140;
                                    button.Height = 40;
                                    button.Font = new Font(button.Font.FontFamily, 8, FontStyle.Bold);
                                    columnIndex = 0;
                                    rowIndex++;
                                }
                                else
                                {
                                    button.Width = 140;
                                    button.Height = 40;
                                    button.Font = new Font(button.Font.FontFamily, 8, FontStyle.Bold);
                                }
                            }
                            // Set the FlatAppearance properties to remove the border
                            button.FlatAppearance.BorderSize = 0; // Set border width to 0
                            button.FlatAppearance.BorderColor = button.BackColor;
                            button.FlatStyle = FlatStyle.Flat;
                            button.BackColor = product_buttoncolor;
                            button.ForeColor = Color.White;
                            // Create a circular Region
                            button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                            button.Click += new EventHandler(DashboardButtonClickEventHandler);
                            tlp_searchproduct_dashboard.Controls.Add(button, columnIndex, rowIndex);
                            columnIndex++;
                        }
                        pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                    }
                    else
                        CMessageBox.Show("No Products", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void txtname_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }

        private void txtitemcode_serach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }

        private void btn_sidebar_toggle_Click(object sender, EventArgs e)
        {
            if (pnlsidebar_searchproduct.Visible)
            {
                pnlsidebar_searchproduct.Visible = false;
                lblcategory.Visible = false;
                textb_shape();
            }
            else
            {
                pnlsidebar_searchproduct.Visible = true;
                lblcategory.Visible = true;
            }
        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtitemcode_serach, txtname_search, txt_product_des, txtrecvamount, txtGrandTotal, txtpaidamount, txtotherdedamt, txtotheraddamt, txtdis_amt, txtdis_per, txtother_descr, txtdis_descr, txtnetamount, txtentrie_descr })
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void tlp_payment_outerdesign_Paint(object sender, PaintEventArgs e)
        {
            float borderRadius = 10f; // adjust this value to change the roundness of the corners
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(tlp_payment_outerdesign.ClientRectangle.X, tlp_payment_outerdesign.ClientRectangle.Y, tlp_payment_outerdesign.ClientRectangle.Width, tlp_payment_outerdesign.ClientRectangle.Height);
            path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
            path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
            path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
            path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
            path.CloseFigure();
            tlp_payment_outerdesign.Region = new Region(path);
        }

        private void tlp_seletedItem_main_Paint(object sender, PaintEventArgs e)
        {
            //float borderRadius = 10f; // adjust this value to change the roundness of the corners
            //GraphicsPath path = new GraphicsPath();
            //RectangleF rect = new RectangleF(tlp_seletedItem_main.ClientRectangle.X, tlp_seletedItem_main.ClientRectangle.Y, tlp_seletedItem_main.ClientRectangle.Width, tlp_seletedItem_main.ClientRectangle.Height);
            //path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
            //path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
            //path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
            //path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
            //path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            //path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
            //path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            //path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
            //path.CloseFigure();
            //tlp_seletedItem_main.Region = new Region(path);
        }

        private void pnlprice_Paint(object sender, PaintEventArgs e)
        {
            float borderRadius = 10f; // adjust this value to change the roundness of the corners
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(pnlprice.ClientRectangle.X, pnlprice.ClientRectangle.Y, pnlprice.ClientRectangle.Width, pnlprice.ClientRectangle.Height);
            path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
            path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
            path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
            path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
            path.CloseFigure();
            pnlprice.Region = new Region(path);
        }

        private void txtitemcode_serach_Enter_1(object sender, EventArgs e)
        {
            if (txtitemcode_serach.Text == "Item Code .....")
            {
                txtitemcode_serach.Text = "";
                txtitemcode_serach.ForeColor = Color.Black;
            }
        }

        private void btn_close_prod_des_Click(object sender, EventArgs e)
        {
            pnl_products_descr.Visible = false;
        }

        private void btn_save_product_des_Click(object sender, EventArgs e)
        {
            if (saletype == 1)
            {
                for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                {
                    if (Proid_Prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value))
                    {
                        string name = Dgv_Selected_Item.Rows[i].Cells[iname].Value.ToString();
                        Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value = txt_product_des.Text;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                {
                    if (Proid_Prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value))
                    {
                        string name = Dgv_Selected_Item.Rows[i].Cells[iname].Value.ToString();
                        Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value = txt_product_des.Text;
                        break;
                    }
                }
            }
            pnl_products_descr.Visible = false;
        }

        private void pnl_products_descr_Paint(object sender, PaintEventArgs e)
        {
            float borderRadius = 10f; // adjust this value to change the roundness of the corners
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(pnl_products_descr.ClientRectangle.X, pnl_products_descr.ClientRectangle.Y, pnl_products_descr.ClientRectangle.Width, pnl_products_descr.ClientRectangle.Height);
            path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
            path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
            path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
            path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
            path.CloseFigure();
            pnl_products_descr.Region = new Region(path);
        }

        private void btn_sleteted_list_gridview_Click(object sender, EventArgs e)
        {
            if (tlp_gridview.Visible)
            {
                tlp_gridview.Visible = false;
            }
            else
            {
                tlp_gridview.Visible = true;

                int centerX = this.ClientSize.Width / 2 - tlp_gridview.Size.Width / 2;
                int centerY = this.ClientSize.Height / 2 - tlp_gridview.Size.Height / 2;

                // Get the screen resolution
                Screen screen = Screen.FromControl(this);
                int screenWidth = screen.Bounds.Width;
                int screenHeight = screen.Bounds.Height;

                // Adjust the position based on screen resolution
                int adjustedX = (screenWidth - tlp_gridview.Size.Width) / 2;
                int adjustedY = (screenHeight - tlp_gridview.Size.Height) / 2;

                // Set the location of tlp_gridview
                tlp_gridview.Location = new Point(adjustedX, adjustedY);
            }
        }


        private void btn_gridview_close_Click(object sender, EventArgs e)
        {
            tlp_gridview.Visible = false;
        }

        private void tlp_gridview_Paint(object sender, PaintEventArgs e)
        {
            float borderRadius = 10f; // adjust this value to change the roundness of the corners
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(tlp_gridview.ClientRectangle.X, tlp_gridview.ClientRectangle.Y, tlp_gridview.ClientRectangle.Width, tlp_gridview.ClientRectangle.Height);
            path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
            path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
            path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
            path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
            path.CloseFigure();
            tlp_gridview.Region = new Region(path);
        }

        private void pnl_header_gridview_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownLocation = e.Location;
            }
        }

        private void pnl_header_gridview_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pnl_header_gridview.Left += e.X - mouseDownLocation.X;
                pnl_header_gridview.Top += e.Y - mouseDownLocation.Y;
            }
        }

        private void txt_product_des_Enter(object sender, EventArgs e)
        {
            if (txt_product_des.Text == "Description Type Here .....")
            {
                txt_product_des.Text = "";
                txt_product_des.ForeColor = Color.Black;
            }
        }

        private void txt_product_des_Leave(object sender, EventArgs e)
        {
            if (txt_product_des.Text == "")
            {
                txt_product_des.Text = "Description Type Here .....";
                txt_product_des.ForeColor = Color.Gray;
            }
        }

        private void btn_offer_calc_Click(object sender, EventArgs e)
        {
            Control controlToRemove = tlp_calculation_main.GetControlFromPosition(0, 0);
            tlp_calculation_main.Controls.Remove(controlToRemove);

            if (tlp_discount_calc.Visible)
            {
                tlp_calculation_main.Controls.Add(tlp_calculation_total, 0, 0);
                tlp_calculation_total.Dock = DockStyle.Fill;
                tlp_calculation_total.Visible = true;
                tlp_discount_calc.Visible = false;

            }       
            else
            {
                tlp_calculation_main.Controls.Add(tlp_discount_calc, 0, 0);
                tlp_discount_calc.Dock = DockStyle.Top;
                tlp_discount_calc.Visible = true;
                tlp_calculation_total.Visible = false;
            }
            textb_shape();
        }

        private void txtdis_descr_Enter(object sender, EventArgs e)
        {
            if (txtdis_descr.Text == "Description Type Here ...")
            {
                txtdis_descr.Text = "";
                txtdis_descr.ForeColor = Color.Black;
            }
        }

        private void txtother_descr_Enter(object sender, EventArgs e)
        {
            if (txtother_descr.Text == "Description Type Here ...")
            {
                txtother_descr.Text = "";
                txtother_descr.ForeColor = Color.Black;
            }
        }

        private void txtdis_descr_Leave(object sender, EventArgs e)
        {
            if (txtdis_descr.Text == "")
            {
                txtdis_descr.Text = "Description Type Here ...";
                txtdis_descr.ForeColor = Color.Gray;
            }
        }

        private void txtother_descr_Leave(object sender, EventArgs e)
        {
            if (txtother_descr.Text == "")
            {
                txtother_descr.Text = "Description Type Here ...";
                txtother_descr.ForeColor = Color.Gray;
            }
        }
        public void netAmount()
        {
            try
            {
                if (string.IsNullOrEmpty(txtotheraddamt.Text))
                    txtotheraddamt.Text = "0";
                if (string.IsNullOrEmpty(txtdis_amt.Text))
                    txtdis_amt.Text = "0";
                if (string.IsNullOrEmpty(txtotherdedamt.Text))
                    txtotherdedamt.Text = "0";
                if (string.IsNullOrEmpty(txtpaidamount.Text))
                    txtpaidamount.Text = "0";
                decimal value;
                if (decimal.TryParse(txtotheraddamt.Text, out value) && decimal.TryParse(txtdis_amt.Text, out value) && decimal.TryParse(txtotherdedamt.Text, out value))
                {
                    decimal netAmount = 0;
                    netAmount = (Convert.ToDecimal(txtGrandTotal.Text) + Convert.ToDecimal(txtotheraddamt.Text)) - (Convert.ToDecimal(txtdis_amt.Text) + Convert.ToDecimal(txtotherdedamt.Text));

                    txtnetamount.Text = netAmount.ToString("N2");
                    decimal balAmnt = netAmount - Convert.ToDecimal(txtpaidamount.Text);
                    txtrecvamount.Text = balAmnt.ToString("N2");
                }
                totQty();
            }
            catch (Exception ex)
            {

            }

        }

        private void txtpaidamount_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(txtpaidamount.Text, out value))
            {
                txtpaidamount.Text = "0";
            }
        }

        private void txtpaidamount_TextChanged(object sender, EventArgs e)
        {
            netAmount();
        }

        private void txtdis_per_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
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

        private void txtdis_amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
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

        private void txtotheraddamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
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

        private void txtpaidamount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
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
        public void discountAmnt()
        {
            if (txtdis_per.Text != "")
            {
                if (Convert.ToDecimal(txtdis_per.Text) > 100)
                {
                    CMessageBox.Show("Invalid Discount %", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtdis_per.Focus();
                }
                else
                {
                    if ((string.IsNullOrEmpty(txtGrandTotal.Text)))
                        txtGrandTotal.Text = "0.00";
                    decimal discountAmnt = Convert.ToDecimal(txtdis_per.Text.Trim()) * Convert.ToDecimal(txtGrandTotal.Text) / 100;
                    txtdis_amt.Text = discountAmnt.ToString("N2");
                }
            }
        }
        public void discountPer()
        {
            if (txtdis_amt.Text != "" && txtGrandTotal.Text != "" && Convert.ToDecimal(txtGrandTotal.Text) != 0)
            {
                if (Convert.ToDecimal(txtdis_amt.Text) > Convert.ToDecimal(txtGrandTotal.Text))
                {
                    CMessageBox.Show("Invalid Discount Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtdis_amt.Focus();
                }
                else
                {
                    decimal discountPer = (Convert.ToDecimal(txtdis_amt.Text) / Convert.ToDecimal(txtGrandTotal.Text) * 100);
                    txtdis_per.Text = discountPer.ToString("N2");
                }
            }
        }

        private void txtotheraddamt_TextChanged(object sender, EventArgs e)
        {
            netAmount();
        }

        private void txtdis_per_Leave(object sender, EventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(txtdis_per.Text, out value))
            {
                txtdis_per.Text = "0";
            }
            discountAmnt();
            netAmount();
        }

        private void txtdis_amt_Leave(object sender, EventArgs e)
        {
            decimal value;
            if (!decimal.TryParse(txtdis_amt.Text, out value))
            {
                txtdis_amt.Text = "0";
            }
            discountPer();
            netAmount();
        }

        private void btn_entire_desc_Click(object sender, EventArgs e)
        {
            if (txtentrie_descr.Visible)
                txtentrie_descr.Visible = false;
            else
                txtentrie_descr.Visible = true;
        }

        private void btn_saveKOT_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck_KOT())
                {
                    bool flage = false;
                    DmnOrderMaster dmnordermaster = new DmnOrderMaster();
                    DmnOrderDetails dmnorderdetail = new DmnOrderDetails();
                    BllOrder bll_order = new BllOrder();
                    dmnordermaster.OrderMasterId = Convert.ToInt32("0");
                    dmnordermaster.EntryNo = "";
                    dmnordermaster.TotalQuantity = Convert.ToInt32(txttotalQTY.Text);
                    dmnordermaster.Remarks = txtentrie_descr.Text;
                    dmnordermaster.BranchId = 1;
                    dmnordermaster.CreatedBy = 1;
                    dmnordermaster.Status = 1;
                    dmnordermaster.Tid = tableId;
                    var InsertStatusP = bll_order.AddEditData("prdInsert_Upd_OrderMaster", dmnordermaster);
                    for (int i = 0; i < Dgv_Selected_Item.Rows.Count; i++)
                    {

                        if (Dgv_Selected_Item.Rows[i].Cells[iqty].Value != null &&Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value)>0 )
                        {
                            dmnorderdetail.OrderMasterId = Convert.ToInt32(InsertStatusP.OrderMasterId);
                            dmnorderdetail.ItemBarCode = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ibarcode].Value);
                            dmnorderdetail.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iname].Value);
                            //dmnorderdetail.Unit = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iunit].Value);
                            dmnorderdetail.UnitId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iunitid].Value);
                            dmnorderdetail.Quantity = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value);
                            dmnorderdetail.PurchaseRate = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iprate].Value);
                            dmnorderdetail.Amount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iamount].Value);

                            dmnorderdetail.DiscountPer = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[idisper].Value);
                            dmnorderdetail.DiscountAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[idisamt].Value);

                            dmnorderdetail.TaxPer = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itaxper].Value);
                            dmnorderdetail.TaxAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itaxamt].Value);
                            //dmnorderdetail.TotalAmnt = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);

                            dmnorderdetail.ItemId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value);
                            dmnorderdetail.Tax = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itax].Value);

                            dmnorderdetail.TaxType = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itaxtype].Value);
                            dmnorderdetail.Srate = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[israte].Value);
                            dmnorderdetail.MRP = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[imrp].Value);
                            dmnorderdetail.Fcid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itype].Value);
                            dmnorderdetail.BranchId = 1;
                            dmnorderdetail.CreatedBy = 1;
                            dmnorderdetail.Status = 1;
                            dmnorderdetail.OrderDetailsId = Convert.ToInt32("0");
                            dmnorderdetail.Amount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iamount].Value);
                            dmnorderdetail.PrId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value);
                            dmnorderdetail.Notes = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value);

                            var InsertStatus = bll_order.AddEditDataDet("prdInsert_Upd_OrderDetails", dmnorderdetail);
                            if (InsertStatus == null)
                            {
                                flage = true;
                                CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }

                        }

                    }
                    if (flage == false)
                    {
                        CMessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //ClearAll();
                }
            }
            catch(Exception ex)
            {

            }

        }
        public void totQty()
        {
            int  sum = 0;
            int n = Dgv_Selected_Item.RowCount;
            for (int i = 0; i < n; i++)
            {
                if (Dgv_Selected_Item.Rows[i].Cells[iqty].Value != "")
                    sum = sum + Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value);
            }
            txttotalQTY.Text = Convert.ToString(sum);
        }

        public void ClearAll()
        {
            try
            {
                foreach (TextBox textBox in new TextBox[] { txtitemcode_serach, txtname_search, txt_product_des, txtrecvamount, txtGrandTotal, txtpaidamount, txtotherdedamt, txtotheraddamt, txtdis_amt, txtdis_per, txtother_descr, txtdis_descr, txtnetamount, txtentrie_descr })
                {
                    textBox.Text = "";
                }
                txtname_search.Text = "Food Name .....";
                txtitemcode_serach.Text = "Item Code .....";
                txtother_descr.Text = "Description Type Here ...";
                txtdis_descr.Text = "Description Type Here ...";
                txtother_descr.ForeColor = Color.Gray;
                txtdis_descr.ForeColor = Color.Gray;
                tlp_seletedItem_main.Controls.Clear();
                Dgv_Selected_Item.Rows.Clear();
                Dgv_Selected_Item.Rows.Add();

            }
            catch (Exception)
            {

            }
        }
            

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        public bool MandatoryCheck_KOT()
        {
            if (string.IsNullOrEmpty(txtpaidamount.Text))
            {
                CMessageBox.Show("Please Enter Paid Amount Is Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtpaidamount.Focus();
                return false;
            }
            else if (tlp_seletedItem_main.Controls.Count==0)
            {
                CMessageBox.Show("Product cart is Empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
    }
}
