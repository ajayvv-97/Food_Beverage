using BLL;
using CustomMessageBox;
using Domain;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoodandBeverage.Interfaces;
using FoodandBeverage.Classes;

namespace FoodandBeverage.Forms
{
    public partial class FrmOrder : Form
    {
        public int tableId = 0;
        public int Bookingmasterid = 0;
        public int BookingType = 0;
        ClsGeneral ClsGeneral = new ClsGeneral();
        public FrmOrder(int tid, int booktype, int bmasterid, List<FrmDashboard.Namelist> checkedtableNames)
        {
            tableId = tid;
            Bookingmasterid = bmasterid;
            BookingType = booktype;
            this.checkedtableNames = checkedtableNames;
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
        public static int iproducttypeno = 2;
        public static int iname = 3;
        public static int iunit = 4;
        public static int iqty = 5;
        public static int iprate = 6;
        public static int iamount = 7;
        public static int idisper = 8;
        public static int idisamt = 9;
        public static int itaxper = 10;
        public static int itaxamt = 11;
        public static int itotal = 12;
        public static int iitemid = 13;
        public static int itax = 14;
        public static int itaxtype = 15;
        public static int israte = 16;
        public static int imrp = 17;
        public static int itype = 18;
        public static int ibarcode2 = 19;
        public static int itaxsettingid = 20;
        public static int iunitid = 21;
        public static int ipridno = 22;
        public static int iprodesno = 23;
        public static int istatusno = 24;
        public static int iigstamtno = 25;
        public static int ivatamtno = 26;
        public static int iorderdetailsidno = 27;
        public static int iofferstatus = 28;
        public static int ioriginalprice = 29;
        public static int isubofferid = 30;
        public static int itabletype = 31;
        public static int ioffertype = 32;
        public static int ioriginalqty = 33;

        public static int vname = 0;
        public static int ciunit = 1;
        public static int cqty = 2;  
        public static int cdisper = 3;
        public static int cdisamt = 4;
        public static int ctaxper = 5;
        public static int ctaxamt = 6;
        public static int camount = 7;
        public static int ctotal = 8;
        public static int citemid = 9;
        public static int cpridno = 10;
        public static int ctax = 11;
        public static int ctaxtype = 12;
        public static int Csrate = 13;
        public static int Cmrp = 14;
        public static int Comproducttypeno = 15;
        public static int cigstamt = 16;
        public static int CCgstamt = 17;
        public static int cSgstamt = 18;
        public static int cvatamt = 19;
        public static int cmarketprice = 20;
        public static int Cbarcode = 21;
        public static int Ctabletype = 22;

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
        public bool OrderRunning_Status = false;
        public bool Booking_Status = false;
        public bool combined_Status = false;

        private Point mouseDownLocation;
        public int r1 = 0;

        public int screenWidth;
        public int screenHeight;
        public int ResetFlagvalue = 0;
        List<DmnOrderMaster> gridSourcemaster = new List<DmnOrderMaster>();
        List<DmnOrderMaster> gridSourcemaster_res = new List<DmnOrderMaster>();
        List<DmnGridFields> gridSource_offer = new List<DmnGridFields>();
        public int finalqty = 0;
        public int Promodetailsid = 0;
        public int Promomasterid = 0;

        public decimal TotalTax = 0;
        public decimal TotalIGST = 0;
        public decimal TotalCGST = 0;
        public decimal TotalSGST = 0;
        public decimal TotalVAT = 0;
        public decimal Totaldis = 0;
        public int ordercount = 0;

        public int desiredWidth = 40;
        public int desiredHeight = 40;

        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _hubProxy;
        // Set the size based on your conditions
        public int R_screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int R_screenHeight = Screen.PrimaryScreen.Bounds.Height;

        public List<Namelist> CheckedNames = new List<Namelist>();
        public List<Namelist> BillList = new List<Namelist>();
        public List<OrderDetailsInfo> orderDetailsInfoList = new List<OrderDetailsInfo>();
        public int Splitypeno = 0;
        public int Existingsalemasterid = 0;
        private List<FrmDashboard.Namelist> checkedtableNames;
        public decimal ReservationTotal = 0;        
        public bool itemcode_serach_Status = false;
        public DateTime OrderDateTime;
        #endregion

        private void btn_tableorder_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private async Task StartHubConnection()
        {
            try
            {
                await _hubConnection.Start();
            }
            catch (Exception ex)
            {
                // Handle the exception (log it, display an error message, etc.)
                MessageBox.Show($"Error starting SignalR connection: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #region please wait loading
        private async void ShowLoadingdGif()
        {
            this.Opacity = 0;
            LoadingGif gifForm = new LoadingGif();
            gifForm.StartPosition = FormStartPosition.CenterScreen;
            gifForm.ShowDialog();
            this.Opacity = 100;
        }
        #endregion
        private async void  FrmOrder_Load(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                {
                    FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                    if (_instanceFrmdash.ChkWritePermission(Convert.ToInt32(formsenum.Order)))
                        btn_saveKOT.Enabled = true;
                    else
                        btn_saveKOT.Enabled = false;
                }
                combined_Status = false;
                //ShowLoadingdGif();
                //tlp_payment_outerdesign.BackColor = ColorTranslator.FromHtml("#205072");
                //tlp_payment_main.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                tlp_seletedItem_main.BackColor = ColorTranslator.FromHtml("#FFFDF8");
                settablecode();                 
                if(BookingType == Convert.ToInt32(DashboardTableType.Normalorder))
                    CheckRe_order();
                else if (BookingType == Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel))                
                    CheckRe_Bookingorder();                
                else if (BookingType == Convert.ToInt32(DashboardTableType.CombinedOrderBill))
                {
                    combined_Status = true;
                    Btnpartpayment.Visible = false;
                    btn_splitpayment.Visible = false;
                    int count = checkedtableNames.Count;

                    if (pnlsidebar_searchproduct.Visible)
                    {
                        pnlsidebar_searchproduct.Visible = false;
                        tlp_food_cate.Visible = false;

                        tlp_serach_main.ColumnStyles[0] = new ColumnStyle(SizeType.AutoSize);
                        tlp_serach_main.ColumnStyles[1] = new ColumnStyle(SizeType.AutoSize);
                        tlp_serach_main.ColumnStyles[2] = new ColumnStyle(SizeType.AutoSize);
                        textb_shape();
                    }
                    
                    lblorderid.Visible = false;
                    label10.Visible = false;
                    tlp_orderid_tbcode.ColumnStyles[0] = new ColumnStyle(SizeType.Percent,5);
                    tlp_orderid_tbcode.ColumnStyles[1] = new ColumnStyle(SizeType.Percent,95);
                    tlp_orderid_tbcode.ColumnStyles[2] = new ColumnStyle(SizeType.AutoSize);
                    tlp_orderid_tbcode.ColumnStyles[3] = new ColumnStyle(SizeType.AutoSize);
                    Combined_Tables();
                    Combined_Table_Bill();
                }

                Control controlToRemove = tlp_calculation_main.GetControlFromPosition(0, 0);
                tlp_calculation_main.Controls.Remove(controlToRemove);
                tlp_calculation_main.Controls.Add(tlp_calculation_total, 0, 0);
                tlp_calculation_total.Visible = true;
                tlp_calculation_total.Dock = DockStyle.Fill;

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

                dgv_viewmore.Rows.Add(1);
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

                dgv_split.BorderStyle = BorderStyle.None;
                dgv_split.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgv_split.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgv_split.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgv_split.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgv_split.BackgroundColor = Color.White;

                dgv_split.EnableHeadersVisualStyles = false;
                dgv_split.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgv_split.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgv_split.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                textb_shape();
                txtname_search.Text = "Food Name .....";
                txtitemcode_serach.Text = "Item Code .....";
                txtother_descr.Text = "Description Type Here ...";
                txtdis_descr.Text = "Description Type Here ...";
                txtentrie_descr.Text = "Description Type here .....";
                txtother_descr.ForeColor = Color.DarkGray;
                txtdis_descr.ForeColor = Color.DarkGray;

                #region Side Bar for Search box main
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
                    ToolTip tooltip = new ToolTip();  

                    #region Fav Button
                    // Configure the Button
                    Button buttonfav = new Button();
                    buttonfav.Text = "Favourite Item";
                    buttonfav.Name = "btn_fav";
                    buttonfav.Tag = 0;
                    buttonfav.BackColor = ColorTranslator.FromHtml("#ffbd03");
                    if (R_screenWidth > 1365 && R_screenHeight > 767)
                    {
                        buttonfav.Height = 50;
                    }
                    else
                        buttonfav.Height = 80;
                    buttonfav.Dock = DockStyle.Top;
                    tooltip.SetToolTip(buttonfav, "Favourite Item");
                    ApplyRoundedCornersToButton(buttonfav);
                    buttonfav.FlatStyle = FlatStyle.Flat;
                    buttonfav.FlatAppearance.BorderSize = 0;

                    buttonfav.Font = new Font(buttonfav.Font.FontFamily, 8, FontStyle.Bold);
                    buttonfav.Click += new EventHandler(SidebuttonfavClickEventHandler);
                    tlp_searchproduct_sidebar.Controls.Add(buttonfav, 0, rowIndex);
                    rowIndex++;
                    #endregion

                    #region offer Button
                    // Configure the Button
                    Button buttonOffer = new Button();
                    buttonOffer.Text = "Special Offer";
                    buttonOffer.Name = "btn_offer";
                    tooltip.SetToolTip(buttonOffer, "Special Offer");
                    buttonOffer.Tag = 0;
                    buttonOffer.BackColor = ColorTranslator.FromHtml("#ffbd03");
                    if (R_screenWidth > 1365 && R_screenHeight > 767)
                    {
                        buttonOffer.Height = 50;
                    }
                    else
                        buttonOffer.Height = 80;
                    buttonOffer.Dock = DockStyle.Top;
                    ApplyRoundedCornersToButton(buttonOffer);
                    buttonOffer.FlatStyle = FlatStyle.Flat;
                    buttonOffer.FlatAppearance.BorderSize = 0;

                    buttonOffer.Font = new Font(buttonOffer.Font.FontFamily, 8, FontStyle.Bold);
                    buttonOffer.Click += new EventHandler(SidebuttonOfferClickEventHandler);  // Update the click event handler if needed
                    tlp_searchproduct_sidebar.Controls.Add(buttonOffer, 0, rowIndex);
                    rowIndex++;
                    #endregion

                    #region Combo Offer Button
                    // Configure the Button
                    Button buttonComboOffer = new Button();
                    buttonComboOffer.Text = "Combo Offer";
                    buttonComboOffer.Name = "btn_combo_offer";
                    tooltip.SetToolTip(buttonComboOffer, "Combo Offer");
                    buttonComboOffer.Tag = 0;
                    buttonComboOffer.BackColor = ColorTranslator.FromHtml("#ffbd03");
                    if (R_screenWidth > 1365 && R_screenHeight > 767)
                    {
                        buttonComboOffer.Height = 50;
                    }
                    else
                        buttonComboOffer.Height = 80;

                    buttonComboOffer.Dock = DockStyle.Top;
                    ApplyRoundedCornersToButton(buttonComboOffer);
                    buttonComboOffer.FlatStyle = FlatStyle.Flat;
                    buttonComboOffer.FlatAppearance.BorderSize = 0;

                    buttonComboOffer.Font = new Font(buttonComboOffer.Font.FontFamily, 8, FontStyle.Bold);
                    buttonComboOffer.Click += new EventHandler(SidebuttonComboOfferClickEventHandler);  // Update the click event handler if needed
                    tlp_searchproduct_sidebar.Controls.Add(buttonComboOffer, 0, rowIndex);
                    rowIndex++;
                    #endregion

                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        // Configure the Button
                        Button button = new Button();
                        button.Text = gridSource[i].CategoryName;
                        button.Name = "btn" + gridSource[i].CategoryName;
                        button.Tag = gridSource[i].Fcid;
                        button.BackColor = ColorTranslator.FromHtml("#3f88c5");
                        tooltip.SetToolTip(button, gridSource[i].CategoryName);
                        button.ForeColor = Color.White;
                        button.FlatStyle = FlatStyle.Flat;
                        button.FlatAppearance.BorderSize = 0;
                        if (R_screenWidth > 1365 && R_screenHeight > 767)
                        {
                            button.Height = 50;
                        }
                        else
                            button.Height = 80;
                        button.Dock = DockStyle.Top;
                        button.Font = new Font(button.Font.FontFamily, 8, FontStyle.Bold);
                        button.Click += new EventHandler(SidebuttonClickEventHandler);
                        ApplyRoundedCornersToButton(button);
                        tlp_searchproduct_sidebar.Controls.Add(button, 0, rowIndex);
                        rowIndex++;
                    }
                    pnlsidebar_searchproduct.Controls.Add(tlp_searchproduct_sidebar);
                    pnl_toggle_btn.Dock = DockStyle.Fill;
                    btn_sidebar_toggle.Location = new Point(pnl_toggle_btn.Width / 2 - btn_sidebar_toggle.Size.Width / 2, pnl_toggle_btn.Height / 2 - btn_sidebar_toggle.Size.Height / 2);
                }
                #endregion Main 

                ShowLoadingdGif();
                setperfixsale();
                btn_clear_cus.Visible = true;

                //int rcount = 0;
                //for (int i = 0; i < Dgv_Selected_Item.RowCount; i++)
                //{
                //    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) > 0)
                //    {
                //        rcount++;
                //        break;
                //    }
                //}
                //if (rcount == 0)
                //    Showprint_Save(Convert.ToInt32(ShowButtonType.Nnone));

            }
            catch (Exception ex)
            {

            }

        }
        private void ApplyRoundedCornersToButton(Button button)
        {
            try
            {
                int cornerRadius = 10;
                button.Paint += (sender, e) =>
                {
                    GraphicsPath path = new GraphicsPath();
                    path.AddArc(0, 0, cornerRadius * 2, cornerRadius * 2, 180, 90);
                    path.AddArc(button.Width - cornerRadius * 2, 0, cornerRadius * 2, cornerRadius * 2, 270, 90);
                    path.AddArc(button.Width - cornerRadius * 2, button.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
                    path.AddArc(0, button.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);

                    button.Region = new Region(path);
                };
            }
            catch (Exception)
            {

            }

        }

        public void settablecode()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "TableCode Field1";
                dmnSearch.Table = "tblTable_info";
                dmnSearch.WhereNdGroupBy = "where Status = 1 and tid = " + tableId;
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    lbltablecode.Text = Convert.ToString(gridSource[0].Field1);
                    label15.Text = Convert.ToString(gridSource[0].Field1);
                    label22.Text = Convert.ToString(gridSource[0].Field1);
                    txttid.Text = Convert.ToString(tableId);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void Combined_Table_Bill()
        {
            try
            {
                BllSearch<DmnOrderMaster> bllSearchmaster = new BllSearch<DmnOrderMaster>();
                DmnSearch dmnSearchmaster = new DmnSearch();
                tlp_seletedItem_main.Controls.Clear();
                txtGrandTotal.Text = "0";
                lbltablecode.Text = string.Empty;

                for (int k = 0; k < checkedtableNames.Count; k++)
                {
                    lbltablecode.Text += "  " + Convert.ToString(checkedtableNames[k].Tablecode);
                    #region Table Name Add to the Top
                    Panel pnlbox_table = new Panel();
                    pnlbox_table.Name = "pnlbox" + checkedtableNames[k].Ordermasterid;
                    pnlbox_table.Dock = DockStyle.Top;
                    pnlbox_table.Height = 15;
                    pnlbox_table.Margin = new Padding(0, 0, 0, 5);
                    Label lblcategory_head_table = new Label();
                    lblcategory_head_table.Name = "lbcate_head_" + checkedtableNames[k].Ordermasterid;
                    lblcategory_head_table.Tag = checkedtableNames[k].Ordermasterid;
                    lblcategory_head_table.Text = "Table : " + checkedtableNames[k].Tablecode;
                    lblcategory_head_table.ForeColor = Color.Black;
                    lblcategory_head_table.BackColor = Color.Transparent;
                    lblcategory_head_table.Dock = DockStyle.Fill;
                    lblcategory_head_table.Font = new Font(lblcategory_head_table.Font.FontFamily, 9, FontStyle.Bold);
                    pnlbox_table.Controls.Add(lblcategory_head_table);
                    tlp_seletedItem_main.Controls.Add(pnlbox_table);
                    #endregion

                    dmnSearchmaster.Select = "*";
                    dmnSearchmaster.Table = "tblOrderMaster om left join tblOrderDetails od on od.OrderMasterId = om.OrderMasterId";
                    dmnSearchmaster.WhereNdGroupBy = "where  om.Status=1 and om.OrderMasterId=" + checkedtableNames[k].Ordermasterid;
                    dmnSearchmaster.OrderBy = "";
                    var gridSourcemaster_combined = bllSearchmaster.GetSearchData("prdSearch", dmnSearchmaster);
                    if (gridSourcemaster_combined.Count > 0)
                    {
                        OrderDateTime = gridSourcemaster_combined[0].CreatedDateTime;
                        ordercount = 0;
                        //lblorderid.Text = Convert.ToString(gridSourcemaster_combined[0].OrderMasterId);
                        //label18.Text = Convert.ToString(gridSourcemaster_combined[0].OrderMasterId);
                        //label25.Text = Convert.ToString(gridSourcemaster_combined[0].OrderMasterId);
                        lblorderid.Visible = false;
                        label10.Visible = false;

                        var distinctCategories = gridSourcemaster_combined
                            .Select(item => item.OrderNo)
                            .Distinct()
                            .ToList();
                        int j = 0;

                        //txtordermasterid.Text = Convert.ToString(gridSourcemaster_combined[0].OrderMasterId);
                        foreach (var orderNo in distinctCategories)
                        {
                            BllSearch<DmnOrderDetails> bllSearch = new BllSearch<DmnOrderDetails>();
                            DmnSearch dmnSearch = new DmnSearch();
                            dmnSearch.Select = "*";
                            dmnSearch.Table = "tblOrderDetails";
                            dmnSearch.WhereNdGroupBy = "where OrderMasterId= " + gridSourcemaster_combined[j].OrderMasterId + " and Status IN (1, 2, 3) and Orderno=" + orderNo;
                            dmnSearch.OrderBy = "";
                            var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                            if (gridSource.Count > 0)
                            {

                                #region Order Add to the Top
                                //heading Top
                                Panel pnlbox = new Panel();
                                pnlbox.Name = "pnlbox" + gridSourcemaster_combined[j].OrderMasterId;
                                pnlbox.Dock = DockStyle.Top;
                                pnlbox.Height = 15;
                                pnlbox.Margin = new Padding(0, 0, 0, 5);
                                //Frist Row Cateory Heading
                                // Configure the Label
                                Label lblcategory_head = new Label();
                                lblcategory_head.Name = "lbcate_head_" + gridSourcemaster_combined[j].OrderMasterId;
                                lblcategory_head.Tag = gridSourcemaster_combined[j].OrderMasterId;
                                int ordernumber = j + 1;
                                lblcategory_head.Text = "Order ID : " + ordernumber + "           [" + gridSource[0].CreatedDateTime + "]";
                                lblcategory_head.ForeColor = Color.Black;
                                lblcategory_head.BackColor = Color.Transparent;
                                lblcategory_head.Dock = DockStyle.Fill;
                                lblcategory_head.Font = new Font(lblcategory_head.Font.FontFamily, 9, FontStyle.Bold);
                                pnlbox.Controls.Add(lblcategory_head);

                                tlp_seletedItem_main.Controls.Add(pnlbox);

                                #endregion

                                txtordermasterid.Text = Convert.ToString(gridSourcemaster_combined[j].OrderMasterId);
                                txtentrie_descr.Text = Convert.ToString(gridSourcemaster_combined[j].Remarks);

                                for (int i = 0; i < gridSource.Count; i++)
                                {
                                    if (gridSource[i].Status == 3)
                                        ordercount = ordercount + 1;

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
                                    tlp_seletedItem_col.Enabled = false;

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
                                    txtqty.Text = Convert.ToString(gridSource[i].Quantity);

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
                                    pb_item_delete.Padding = new Padding(5, 5, 5, 5);
                                    if (gridSource[i].Status == 1)
                                        pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.button_pending_order;
                                    else if (gridSource[i].Status == 2)
                                        pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.food_preparation;
                                    else if (gridSource[i].Status == 3)
                                        pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.button_complete_order;

                                    pb_item_delete.BackgroundImageLayout = ImageLayout.Zoom;

                                    decimal amount = Convert.ToDecimal(gridSource[i].NetAmount);
                                    decimal tamt = Convert.ToDecimal(txtGrandTotal.Text) + amount;
                                    txtGrandTotal.Text = Convert.ToString(tamt);

                                    tlp_seletedItem_col.Name = "tlp_seletedItem_col_e_" + gridSource[i].PrId;
                                    tlp_seletedItem_col.Tag = gridSource[i].PrId;
                                    tlp_seletedItem_col.AccessibleDescription = "1";

                                    buttonitemname.Text = gridSource[i].ItemName;
                                    buttonitemname.Name = "buttonitemname_e_" + gridSource[i].ItemName;
                                    buttonitemname.Tag = gridSource[i].PrId;
                                    //  buttonitemname.Click += new EventHandler(buttonitemnameClickEventHandler);

                                    buttonAdd.Name = "buttonAdd_e_" + gridSource[i].PrId;
                                    buttonAdd.Tag = gridSource[i].PrId;
                                    // buttonAdd.Click += new EventHandler(ButtonbuttonAddClickEventHandler);

                                    txtqty.Name = "txtqty_e_" + gridSource[i].PrId;
                                    txtqty.Tag = gridSource[i].PrId;

                                    buttonRemove.Name = "btn_remove_e_" + gridSource[i].PrId;
                                    buttonRemove.Tag = gridSource[i].PrId;
                                    //buttonRemove.Click += new EventHandler(ButtonRemoveClickEventHandler);

                                    txtprice.Text = Convert.ToString(gridSource[i].MarketPrice);
                                    txtprice.Name = "txtprice_e_" + gridSource[i].PrId;
                                    txtprice.Tag = gridSource[i].PrId;

                                    pb_item_delete.Name = "btn_remove_" + gridSource[i].PrId;
                                    pb_item_delete.Tag = gridSource[i].PrId;
                                    //  pb_item_delete.Click += new EventHandler(pb_item_deleteClickEventHandler);

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

                                    #endregion

                                }
                            }
                            j = j + 1;
                        }

                        #region End Heading Grid List
                        //heading Top
                        Panel pnlboxend = new Panel();
                        pnlboxend.Name = "pnlbox_end";
                        pnlboxend.Dock = DockStyle.Top;
                        pnlboxend.Height = 15;
                        pnlboxend.Margin = new Padding(0, 5, 0, 5);
                        tlp_seletedItem_main.Controls.Add(pnlboxend);
                        #endregion                
                    }        
                }
                Showprint_Save(Convert.ToInt32(ShowButtonType.Print));
                netAmount();
            }
            catch (Exception ex)
            {

            }
        }

        public void CheckRe_Bookingorder()
        {
            try
            {
                bool bookinorder_F = false;//chcek frist order from reservation page
                int ordermsid = 0;
                BllSearch<DmnOrderMaster> bllSearchmaster = new BllSearch<DmnOrderMaster>();
                DmnSearch dmnSearchmaster = new DmnSearch();
                dmnSearchmaster.Select = "*,od.status status,bm.AdvancePay AdvancePay,bm.ReservationTotal ReservationTotal,om.Gender Gender,om.OrderMasterId";
                dmnSearchmaster.Table = "tblOrderMaster om left join tblOrderDetails od on od.OrderMasterId = om.OrderMasterId " +
                    "left join tblbookMaster bm on bm.BookmasterId=om.BookmasterId";
                dmnSearchmaster.WhereNdGroupBy = "where  om.Status=1 and om.BookmasterId="+Bookingmasterid;
                dmnSearchmaster.OrderBy = "";
                gridSourcemaster = bllSearchmaster.GetSearchData("prdSearch", dmnSearchmaster);
                if (gridSourcemaster.Count > 0)
                {
                    OrderDateTime = gridSourcemaster[0].CreatedDateTime;
                    ordermsid = Convert.ToInt32(gridSourcemaster[0].OrderMasterId);
                    txtph.Text = Convert.ToString(gridSourcemaster[0].Telephone);
                    txtemailid.Text = Convert.ToString(gridSourcemaster[0].Email);
                    txtaddress.Text = Convert.ToString(gridSourcemaster[0].Address);
                    txtcusname.Text = Convert.ToString(gridSourcemaster[0].CustomerName);
                    if (Convert.ToInt32(gridSourcemaster[0].Gender) == 1)
                        rb_male.Checked = true;
                    else
                        rb_female.Checked = true;

                    txtph.Enabled = false;
                    txtemailid.Enabled = false;
                    txtaddress.Enabled = false;
                    txtcusname.Enabled = false;
                    rb_female.Enabled = false;
                    rb_male.Enabled = false;
                    btn_edit_cus.Visible = true;
                    btn_clear_cus.Visible = true;

                    btn_itemwise.Enabled = false;
                    tableId = 0;
                    txttotalQTY.Text = Convert.ToString(gridSourcemaster[0].Quantity);
                    txtGrandTotal.Text = "0";
                    ordercount = 0;
                    Booking_Status = true;
                    tlp_seletedItem_main.Controls.Clear();
                    FillExistingProducts();
                    lblorderid.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);
                    label18.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);
                    label25.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);

                    lblorderid.Visible = true;
                    label10.Visible = true;

                    lbladvanceamt.Visible = true;
                    txtadvance.Visible = true;
                    ReservationTotal= Convert.ToDecimal(gridSourcemaster[0].ReservationTotal);
                    txtadvance.Text= Convert.ToString(gridSourcemaster[0].AdvancePay);
                    tlp_calculation_total.RowStyles[0] = new RowStyle(SizeType.Percent, 25);
                    tlp_calculation_total.RowStyles[1] = new RowStyle(SizeType.Percent, 25);
                    tlp_calculation_total.RowStyles[2] = new RowStyle(SizeType.Percent, 25);
                    tlp_calculation_total.RowStyles[3] = new RowStyle(SizeType.Percent, 25);
                    txtadvance.Dock = DockStyle.Fill;

                    var distinctCategories = gridSourcemaster
                        .Select(item => item.OrderNo)
                        .Distinct()
                        .ToList();

                    int j = 0;
                    txtordermasterid.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);
                    //if (string.IsNullOrEmpty(txtGrandTotal.Text))
                    //    txtGrandTotal.Text = "0";
                    foreach (var orderNo in distinctCategories)
                    {
                        BllSearch<DmnOrderDetails> bllSearch = new BllSearch<DmnOrderDetails>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "od.*,ti.TableCode as TableCode";
                        dmnSearch.Table = "tblOrderDetails od left join tblTable_info ti on ti.Tid=od.tid";
                        dmnSearch.WhereNdGroupBy = "where od.OrderMasterId= " + gridSourcemaster[j].OrderMasterId + " and od.Status IN (1, 2, 3,4) and Orderno=" + orderNo;
                        dmnSearch.OrderBy = "";
                        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSource.Count > 0)
                        {
                            #region Order Add to the Top
                            //heading Top
                            Panel pnlbox = new Panel();
                            pnlbox.Name = "pnlbox" + gridSourcemaster[j].OrderMasterId;
                            pnlbox.Dock = DockStyle.Top;
                            pnlbox.Height = 15;
                            pnlbox.Margin = new Padding(0, 0, 0, 5);
                            //Frist Row Cateory Heading
                            // Configure the Label
                            Label lblcategory_head = new Label();
                            lblcategory_head.Name = "lbcate_head_" + gridSourcemaster[j].OrderMasterId;
                            lblcategory_head.Tag = gridSourcemaster[j].OrderMasterId;
                            int ordernumber = j + 1;
                            lblcategory_head.Text = "Order ID : " + ordernumber + "           [" + gridSource[0].CreatedDateTime + "]" + "    Table : "+ gridSource[0].TableCode;
                            lblcategory_head.ForeColor = Color.Black;
                            lblcategory_head.BackColor = Color.Transparent;
                            lblcategory_head.Dock = DockStyle.Fill;
                            lblcategory_head.Font = new Font(lblcategory_head.Font.FontFamily, 9, FontStyle.Bold);
                            pnlbox.Controls.Add(lblcategory_head);

                            tlp_seletedItem_main.Controls.Add(pnlbox);
                            #endregion
                            Dgv_Selected_Item.Rows.Clear();

                            txtordermasterid.Text = Convert.ToString(gridSourcemaster[j].OrderMasterId);
                            txtentrie_descr.Text = Convert.ToString(gridSourcemaster[j].Remarks);

                            for (int i = 0; i < gridSource.Count; i++)
                            {
                                if (gridSource[i].Status == 3)
                                    ordercount = ordercount + 1;

                                if (gridSource[i].Status == 4)
                                {
                                    bookinorder_F = true;
                                    #region Grid fill data
                                    Dgv_Selected_Item.Rows.Add(1);
                                    r1 = Dgv_Selected_Item.RowCount - 1;
                                    Dgv_Selected_Item.Rows[i].Cells[iorderdetailsidno].Value = Convert.ToInt32(gridSource[i].OrderDetailsId);
                                    Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = Convert.ToString(gridSource[i].ItemBarCode);
                                    Dgv_Selected_Item.Rows[r1].Cells[islno].Value = r1 + 1;
                                    Dgv_Selected_Item.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                    Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = Convert.ToString(gridSource[i].ItemBarCode);
                                    Dgv_Selected_Item.Rows[r1].Cells[ibarcode2].Value = Convert.ToString(gridSource[i].ItemBarCode);
                                    Dgv_Selected_Item.Rows[r1].Cells[iname].Value = Convert.ToString(gridSource[i].ItemName);
                                    Dgv_Selected_Item.Rows[r1].Cells[iamount].Value = Convert.ToString(gridSource[i].Amount);
                                    Dgv_Selected_Item.Rows[r1].Cells[itotal].Value = Convert.ToString(gridSource[i].NetAmount);
                                    Dgv_Selected_Item.Rows[r1].Cells[iqty].Value = Convert.ToInt32(gridSource[i].Quantity);
                                    Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = Convert.ToDecimal(gridSource[i].PurchaseRate);
                                    Dgv_Selected_Item.Rows[r1].Cells[israte].Value = Convert.ToDecimal(gridSource[i].Srate);
                                    Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = Convert.ToDecimal(gridSource[i].MRP);
                                    Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = Convert.ToDecimal(gridSource[i].DiscountPer);
                                    Dgv_Selected_Item.Rows[r1].Cells[idisamt].Value = "0";
                                    Dgv_Selected_Item.Rows[r1].Cells[iitemid].Value = Convert.ToDecimal(gridSource[i].ItemId);
                                    Dgv_Selected_Item.Rows[r1].Cells[itax].Value = Convert.ToString(gridSource[i].Tax);
                                    Dgv_Selected_Item.Rows[r1].Cells[itaxtype].Value = Convert.ToString(gridSource[i].TaxType);
                                    Dgv_Selected_Item.Rows[r1].Cells[itype].Value = Convert.ToInt32(gridSource[i].ProductType);
                                    //Dgv_Selected_Item.Rows[r1].Cells[itaxsettingid].Value = Convert.ToInt32(gridSource[0].tax);
                                    Dgv_Selected_Item.Rows[r1].Cells[istatusno].Value = 1;
                                    Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = Color.White;
                                    Dgv_Selected_Item.Rows[r1].Cells[iofferstatus].Value = 0;
                                    Dgv_Selected_Item.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(gridSource[i].TableType);
                                    Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value = Convert.ToDecimal(gridSource[i].MarketPrice);


                                    #endregion
                                }
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
                                tlp_seletedItem_col.Enabled = true;

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
                                txtqty.Text = Convert.ToString(gridSource[i].Quantity);
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
                                pb_item_delete.Padding = new Padding(5, 5, 5, 5);
                                pb_item_delete.Enabled = false;
                                if (Convert.ToInt32(Food_Status.Booking_order)== Convert.ToInt32(gridSource[i].Status))
                                {
                                    pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.Delete_Grid;
                                    pb_item_delete.Enabled = true;
                                }              
                                else if (gridSource[i].Status == Convert.ToInt32(Food_Status.Pending))
                                    pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.button_pending_order;
                                else if (gridSource[i].Status == Convert.ToInt32(Food_Status.FoodProcessing))
                                    pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.food_preparation;
                                else if (gridSource[i].Status == Convert.ToInt32(Food_Status.Complete))
                                    pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.button_complete_order;

                                pb_item_delete.BackgroundImageLayout = ImageLayout.Zoom; // Change to Zoom instead of Stretch
                                decimal amount = Convert.ToDecimal(gridSource[i].NetAmount);
                                decimal tamt = Convert.ToDecimal(txtGrandTotal.Text) + amount;
                                txtGrandTotal.Text = Convert.ToString(tamt);

                                tlp_seletedItem_col.Name = "tlp_seletedItem_col_e_" + gridSource[i].ItemId + gridSource[i].TableType;
                                tlp_seletedItem_col.Tag = gridSource[i].PrId;
                                tlp_seletedItem_col.AccessibleDescription = "1";

                                buttonitemname.Text = gridSource[i].ItemName;
                                buttonitemname.Name = "buttonitemname_e_" + gridSource[i].ItemId + gridSource[i].TableType;
                                buttonitemname.Tag = gridSource[i].PrId;
                                buttonitemname.Enabled = false;

                                buttonAdd.Name = "buttonAdd_e_" + gridSource[i].ItemId + gridSource[i].TableType;
                                buttonAdd.Tag = gridSource[i].PrId;
                                buttonAdd.Enabled = false;


                                txtqty.Name = "txtqty_e_" + gridSource[i].ItemId + gridSource[i].TableType;
                                txtqty.Tag = gridSource[i].PrId;
                                txtqty.Enabled = false;

                                buttonRemove.Name = "btn_remove_e_" + gridSource[i].ItemId + gridSource[i].TableType;
                                buttonRemove.Tag = gridSource[i].PrId;
                                buttonRemove.Enabled = false;

                                txtprice.Text = Convert.ToString(gridSource[i].MarketPrice);
                                txtprice.Name = "txtprice_e_" + gridSource[i].ItemId + gridSource[i].TableType;
                                txtprice.Tag = gridSource[i].PrId;
                                txtprice.Enabled = false;

                                pb_item_delete.Name = "btn_remove_" + gridSource[i].ItemId + gridSource[i].TableType;
                                pb_item_delete.Tag = gridSource[i].ItemId;
                                pb_item_delete.AccessibleDescription =Convert.ToString( gridSource[i].TableType);
                                pb_item_delete.Click += new EventHandler(pb_item_delete_booking_ClickEventHandler);
                                
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

                                #endregion

                            }
                        }
                        j = j + 1;
                    }
                    #region End Heading Grid List
                    //heading Top
                    Panel pnlboxend = new Panel();
                    pnlboxend.Name = "pnlbox_end";
                    pnlboxend.Dock = DockStyle.Top;
                    pnlboxend.Height = 15;
                    pnlboxend.Margin = new Padding(0, 5, 0, 5);
                    // Configure the Label
                    Label lblend = new Label();
                    lblend.Name = "lbcate_end";
                    lblend.Text = DateTime.Now.ToString(" yyyy:MM:dd  HH:mm:ss");
                    lblend.ForeColor = Color.Black;
                    lblend.BackColor = Color.Transparent;
                    lblend.Dock = DockStyle.Fill;
                    lblend.Font = new Font(lblend.Font.FontFamily, 11, FontStyle.Bold);
                    pnlboxend.Controls.Add(lblend);
                    tlp_seletedItem_main.Controls.Add(pnlboxend);
                    #endregion
                    if(bookinorder_F)
                    {
                        Showprint_Save(Convert.ToInt32(ShowButtonType.save));
                        bookinorder_F = false;
                    }
                    else
                    {
                        BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1";
                        dmnSearch.Table = "tblOrderDetails";
                        dmnSearch.WhereNdGroupBy = "WHERE status!=0 and OrderMasterId = " + ordermsid;
                        dmnSearch.OrderBy = "";
                        var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);
                        if(gridSource_order.Count>0)
                        {
                            if ( Convert.ToInt32(gridSource_order[0].Field1)==1)
                            {
                                checksave_refresh();
                            }
                            else
                            {
                                Showprint_Save(Convert.ToInt32(ShowButtonType.Print));
                            }
                        }
                    }


                    netAmount();
                }
                else
                {
                    OrderRunning_Status = false;
                    Showprint_Save(Convert.ToInt32(ShowButtonType.save));
                    lblorderid.Visible = false;
                    label10.Visible = false;
                }
                textb_shape();

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
                dmnSearchmaster.Table = "tblOrderMaster om left join tblOrderDetails od on od.OrderMasterId = om.OrderMasterId and od.Status IN (1, 2, 3)";
                dmnSearchmaster.WhereNdGroupBy = "where om.tid= " + tableId + " and om.Status=1";
                dmnSearchmaster.OrderBy = "";
                gridSourcemaster = bllSearchmaster.GetSearchData("prdSearch", dmnSearchmaster);
                if (gridSourcemaster.Count > 0)
                {
                    OrderDateTime = gridSourcemaster[0].CreatedDateTime;
                    txtGrandTotal.Text = "0";

                    txtph.Text = Convert.ToString(gridSourcemaster[0].Telephone);
                    txtemailid.Text = Convert.ToString(gridSourcemaster[0].Email);
                    txtaddress.Text = Convert.ToString(gridSourcemaster[0].Address);
                    txtcusname.Text = Convert.ToString(gridSourcemaster[0].CustomerName);
                    if (Convert.ToInt32(gridSourcemaster[0].Gender) == 1)
                        rb_male.Checked = true;
                    else
                        rb_female.Checked = true;

                    txtph.Enabled = false;
                    txtemailid.Enabled = false;
                    txtaddress.Enabled = false;
                    txtcusname.Enabled = false;
                    rb_female.Enabled = false;
                    rb_male.Enabled = false;
                    btn_edit_cus.Visible = true;
                    btn_clear_cus.Visible = true;

                    ordercount = 0;
                    OrderRunning_Status = true;
                    tlp_seletedItem_main.Controls.Clear();
                    FillExistingProducts();
                    lblorderid.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);
                    label18.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);
                    label25.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);

                    lblorderid.Visible = true;
                    label10.Visible = true;

                    var distinctCategories = gridSourcemaster
                        .Select(item => item.OrderNo)
                        .Distinct()
                        .ToList();

                    int j = 0;
                    txtordermasterid.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);
                    //if (string.IsNullOrEmpty(txtGrandTotal.Text))
                    //    txtGrandTotal.Text = "0";
                    foreach (var orderNo in distinctCategories)
                    {
                        BllSearch<DmnOrderDetails> bllSearch = new BllSearch<DmnOrderDetails>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "*";
                        dmnSearch.Table = "tblOrderDetails";
                        dmnSearch.WhereNdGroupBy = "where OrderMasterId= " + gridSourcemaster[j].OrderMasterId + " and Status IN (1, 2, 3) and Orderno=" + orderNo;
                        dmnSearch.OrderBy = "";
                        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSource.Count > 0)
                        {
                            #region Order Add to the Top
                            //heading Top
                            Panel pnlbox = new Panel();
                            pnlbox.Name = "pnlbox" + gridSourcemaster[j].OrderMasterId;
                            pnlbox.Dock = DockStyle.Top;
                            pnlbox.Height = 15;
                            pnlbox.Margin = new Padding(0, 0, 0, 5);
                            //Frist Row Cateory Heading
                            // Configure the Label
                            Label lblcategory_head = new Label();
                            lblcategory_head.Name = "lbcate_head_" + gridSourcemaster[j].OrderMasterId;
                            lblcategory_head.Tag = gridSourcemaster[j].OrderMasterId;
                            int ordernumber = j + 1;
                            lblcategory_head.Text = "Order ID : " + ordernumber + "           [" + gridSource[0].CreatedDateTime +"]";
                            lblcategory_head.ForeColor = Color.Black;
                            lblcategory_head.BackColor = Color.Transparent;
                            lblcategory_head.Dock = DockStyle.Fill;
                            lblcategory_head.Font = new Font(lblcategory_head.Font.FontFamily, 9, FontStyle.Bold);
                            pnlbox.Controls.Add(lblcategory_head);

                            tlp_seletedItem_main.Controls.Add(pnlbox);
                            #endregion

                            txtordermasterid.Text = Convert.ToString(gridSourcemaster[j].OrderMasterId);
                            txtentrie_descr.Text = Convert.ToString(gridSourcemaster[j].Remarks);

                            for (int i = 0; i < gridSource.Count; i++)
                            {
                                if (gridSource[i].Status == 3)
                                    ordercount = ordercount + 1;

                                    #region Grid fill data
                                //Dgv_Selected_Item.Rows[i].Cells[islno].Value = i + 1;
                                //Dgv_Selected_Item.Rows[i].Cells[ibarcode].Value = gridSource[i].ItemBarCode;
                                //Dgv_Selected_Item.Rows[i].Cells[iname].Value = gridSource[i].ItemName;
                                //Dgv_Selected_Item.Rows[i].Cells[iitemid].Value = gridSource[i].ItemId;
                                //Dgv_Selected_Item.Rows[i].Cells[iunit].Value = "Number";
                                ////Dgv_Selected_Item.Rows[i].Cells[iqty].Value = gridSource[i].Quantity;
                                //Dgv_Selected_Item.Rows[i].Cells[iprate].Value = gridSource[i].PurchaseRate;
                                //Dgv_Selected_Item.Rows[i].Cells[iamount].Value = gridSource[i].Amount;
                                //Dgv_Selected_Item.Rows[i].Cells[idisper].Value = gridSource[i].DiscountPer;
                                //Dgv_Selected_Item.Rows[i].Cells[idisamt].Value = gridSource[i].DiscountAmount;
                                //Dgv_Selected_Item.Rows[i].Cells[itaxper].Value = gridSource[i].TaxPer;
                                //Dgv_Selected_Item.Rows[i].Cells[itaxamt].Value = gridSource[i].TaxAmount;
                                //Dgv_Selected_Item.Rows[i].Cells[itotal].Value = gridSource[i].NetAmount;
                                //Dgv_Selected_Item.Rows[i].Cells[itaxtype].Value = gridSource[i].TaxType;
                                //Dgv_Selected_Item.Rows[i].Cells[itax].Value = gridSource[i].Tax;
                                //Dgv_Selected_Item.Rows[i].Cells[israte].Value = gridSource[i].Srate;
                                //Dgv_Selected_Item.Rows[i].Cells[imrp].Value = gridSource[i].MRP;
                                //Dgv_Selected_Item.Rows[i].Cells[ipridno].Value = gridSource[i].PrId;
                                //Dgv_Selected_Item.Rows[i].Cells[iunitid].Value = gridSource[i].UnitId;
                                //Dgv_Selected_Item.Rows[i].Cells[istatusno].Value = 0;
                                    #endregion

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
                                tlp_seletedItem_col.Enabled = false;

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
                                txtqty.Text =Convert.ToString(gridSource[i].Quantity);

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
                                pb_item_delete.Padding = new Padding(5,5,5,5);
                                if (gridSource[i].Status==Convert.ToInt32(Food_Status.Pending))
                                    pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.button_pending_order;
                                else if (gridSource[i].Status == Convert.ToInt32(Food_Status.FoodProcessing))
                                    pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.food_preparation;
                                else if (gridSource[i].Status == Convert.ToInt32(Food_Status.Complete))
                                    pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.button_complete_order;

                                pb_item_delete.BackgroundImageLayout = ImageLayout.Zoom; // Change to Zoom instead of Stretch
                                decimal amount = Convert.ToDecimal(gridSource[i].NetAmount);
                                decimal tamt = Convert.ToDecimal(txtGrandTotal.Text) + amount;
                                txtGrandTotal.Text = Convert.ToString(tamt);

                                //if (Convert.ToInt32(gridSource[i].PrId) > 0)
                                //{
                                tlp_seletedItem_col.Name = "tlp_seletedItem_col_e_" + gridSource[i].PrId;
                                    tlp_seletedItem_col.Tag = gridSource[i].PrId;
                                    tlp_seletedItem_col.AccessibleDescription = "1";

                                    buttonitemname.Text = gridSource[i].ItemName;
                                    buttonitemname.Name = "buttonitemname_e_" + gridSource[i].ItemName;
                                    buttonitemname.Tag = gridSource[i].PrId;
                                    buttonitemname.Click += new EventHandler(buttonitemnameClickEventHandler);

                                    buttonAdd.Name = "buttonAdd_e_" + gridSource[i].PrId;
                                    buttonAdd.Tag = gridSource[i].PrId;
                                    buttonAdd.Click += new EventHandler(ButtonbuttonAddClickEventHandler);

                                    txtqty.Name = "txtqty_e_" + gridSource[i].PrId;
                                    txtqty.Tag = gridSource[i].PrId;

                                    buttonRemove.Name = "btn_remove_e_" + gridSource[i].PrId;
                                    buttonRemove.Tag = gridSource[i].PrId;
                                    buttonRemove.Click += new EventHandler(ButtonRemoveClickEventHandler);

                                    txtprice.Text = Convert.ToString(gridSource[i].MarketPrice);
                                    txtprice.Name = "txtprice_e_" + gridSource[i].PrId;
                                    txtprice.Tag = gridSource[i].PrId;

                                    pb_item_delete.Name = "btn_remove_" + gridSource[i].PrId;
                                    pb_item_delete.Tag = gridSource[i].PrId;
                                    pb_item_delete.Click += new EventHandler(pb_item_deleteClickEventHandler);

                                    //CalcTotalAmt_Add_prid(Convert.ToInt32(gridSource[i].PrId), gridSource[i].Quantity);
                                //}
                                //else
                                //{
                                //    tlp_seletedItem_col.Name = "tlp_seletedItem_col_e_" + gridSource[i].ItemId;
                                //    tlp_seletedItem_col.Tag = gridSource[i].ItemId;
                                //    tlp_seletedItem_col.AccessibleDescription = "0";

                                //    buttonitemname.Text = gridSource[i].ItemName;
                                //    buttonitemname.Name = "buttonitemname_e_" + gridSource[i].ItemName;
                                //    buttonitemname.Tag = gridSource[i].ItemId;
                                //    buttonitemname.Click += new EventHandler(buttonitemnameClickEventHandler);

                                //    buttonAdd.Name = "buttonAdd_e_" + gridSource[i].ItemId;
                                //    buttonAdd.Tag = gridSource[i].ItemId;
                                //    buttonAdd.Click += new EventHandler(ButtonbuttonAddClickEventHandler);

                                //    txtqty.Name = "txtqty_e_" + gridSource[i].ItemId;
                                //    txtqty.Tag = gridSource[i].ItemId;

                                //    buttonRemove.Name = "btn_remove_e_" + gridSource[i].ItemId;
                                //    buttonRemove.Tag = gridSource[i].ItemId;
                                //    buttonRemove.Click += new EventHandler(ButtonRemoveClickEventHandler);

                                //    txtprice.Text = Convert.ToString(gridSource[i].Srate);
                                //    txtprice.Name = "txtprice_e_" + gridSource[i].ItemId;
                                //    txtprice.Tag = gridSource[i].ItemId;

                                //    pb_item_delete.Name = "btn_remove_e_" + gridSource[i].ItemId;
                                //    pb_item_delete.Tag = gridSource[i].ItemId;
                                //    pb_item_delete.Click += new EventHandler(pb_item_deleteClickEventHandler);

                                //    //CalcTotalAmt_Add(Convert.ToInt32(gridSource[i].ItemId), gridSource[i].Quantity);
                                //}

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

                                #endregion

                            }
                        }
                        j = j + 1;
                    }
                    #region End Heading Grid List
                    //heading Top
                    Panel pnlboxend = new Panel();
                    pnlboxend.Name = "pnlbox_end";
                    pnlboxend.Dock = DockStyle.Top;
                    pnlboxend.Height = 15;
                    pnlboxend.Margin = new Padding(0, 5, 0, 5);
                    // Configure the Label
                    Label lblend = new Label();
                    lblend.Name = "lbcate_end";
                    lblend.Text = DateTime.Now.ToString(" yyyy:MM:dd  HH:mm:ss");
                    lblend.ForeColor = Color.Black;
                    lblend.BackColor = Color.Transparent;
                    lblend.Dock = DockStyle.Fill;
                    lblend.Font = new Font(lblend.Font.FontFamily, 11, FontStyle.Bold);
                    pnlboxend.Controls.Add(lblend);
                    tlp_seletedItem_main.Controls.Add(pnlboxend);
                    #endregion
                    if (ordercount==gridSourcemaster.Count)
                    {
                        Showprint_Save(Convert.ToInt32(ShowButtonType.Print));
                    }
                    else
                    {
                        int rcount = 0;
                        for (int i = 0; i < Dgv_Selected_Item.RowCount; i++)
                        {
                            if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) > 0)
                            {
                                rcount++;
                                break;
                            }
                        }
                        if (rcount == 0)
                            Showprint_Save(Convert.ToInt32(ShowButtonType.Nnone));
                        else
                            Showprint_Save(Convert.ToInt32(ShowButtonType.save));
                    }
                    netAmount();
                }
                else
                {
                    OrderRunning_Status = false;
                    Showprint_Save(Convert.ToInt32(ShowButtonType.save));
                    lblorderid.Visible = false;
                    label10.Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Showprint_Save(int typeno)
        {
            try
            {
                btn_refresh.Visible = true;
                btn_frm_close.Visible = true;
                int rowToRemove = 0;
                List<int> columnsToRemove = new List<int> { 1, 2, 3 };

                foreach (int column in columnsToRemove)
                {
                    Control controlToRemove = tlp_buttons.GetControlFromPosition(column, rowToRemove);

                    if (controlToRemove != null)
                    {
                        tlp_buttons.Controls.Remove(controlToRemove);
                    }
                }
                if(typeno ==1)
                {
                    tlp_buttons.Controls.Add(btn_savekot_print, 1, 0);
                    btn_savekot_print.Dock = DockStyle.Fill;
                    tlp_buttons.Controls.Add(btn_saveKOT, 2, 0);
                    btn_saveKOT.Dock = DockStyle.Fill;
                    tlp_buttons.Controls.Add(btn_refresh, 3, 0);
                    btn_refresh.Dock = DockStyle.Fill;
                    tlp_buttons.Controls.Add(btn_frm_close, 4, 0);
                    btn_frm_close.Dock = DockStyle.Left;

                    btn_saveKOT.Visible = true;
                    btn_savekot_print.Visible = true;

                    btn_print_bill.Visible = false;
                    btn_promo_code.Visible = false;
                    btn_entire_desc.Visible = true;
                }
                else if (typeno == 2)
                {
                    tlp_buttons.Controls.Add(btn_print_bill, 1, 0);
                    btn_print_bill.Dock = DockStyle.Fill;
                    tlp_buttons.Controls.Add(btn_refresh, 2, 0);
                    btn_refresh.Dock = DockStyle.Fill;
                    tlp_buttons.Controls.Add(btn_frm_close, 3, 0);
                    btn_frm_close.Dock = DockStyle.Left;

                    btn_saveKOT.Visible = false;
                    btn_savekot_print.Visible = false;
                    btn_print_bill.Visible = true;
                    btn_promo_code.Visible = true;
                    btn_entire_desc.Visible = false;
                }
                else
                {
                    tlp_buttons.Controls.Add(btn_refresh, 1, 0);
                    btn_refresh.Dock = DockStyle.Fill;
                    tlp_buttons.Controls.Add(btn_frm_close, 2, 0);
                    btn_frm_close.Dock = DockStyle.Left;
                    btn_saveKOT.Visible = false;
                    btn_savekot_print.Visible = false;
                    btn_print_bill.Visible = false;
                    btn_promo_code.Visible = false;
                    btn_entire_desc.Visible = false;
                }
            }
            catch(Exception ex)
            {

            }
        }
        public void FillExistingProducts()
        {
            try
            {
                if (gridSourcemaster.Count > 0)
                {
                    if(checkedtableNames.Count==0)
                        dgv_exitsting_product.Rows.Clear();

                    dgv_exitsting_product.Rows.Add(gridSourcemaster.Count);
                    int r = 0;
                    for (int i = 0; i < gridSourcemaster.Count; i++)
                    {
                        if (gridSourcemaster[i].TableType == 1)
                        {
                            #region Grid Inseration product
                            dgv_exitsting_product.Rows[r].Cells[islno].Value = i + 1;
                            dgv_exitsting_product.Rows[r].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgv_exitsting_product.Rows[r].Cells[ibarcode].Value = Convert.ToString(gridSourcemaster[i].ItemBarCode);
                            dgv_exitsting_product.Rows[r].Cells[ibarcode2].Value = Convert.ToString(gridSourcemaster[i].ItemBarCode);
                            dgv_exitsting_product.Rows[r].Cells[iname].Value = Convert.ToString(gridSourcemaster[i].ItemName);
                            dgv_exitsting_product.Rows[r].Cells[iqty].Value = "1";
                            dgv_exitsting_product.Rows[r].Cells[iprate].Value = Convert.ToDecimal(gridSourcemaster[i].PurchaseRate);
                            dgv_exitsting_product.Rows[r].Cells[israte].Value = Convert.ToDecimal(gridSourcemaster[i].Srate);
                            dgv_exitsting_product.Rows[r].Cells[imrp].Value = Convert.ToDecimal(gridSourcemaster[i].MRP);
                            dgv_exitsting_product.Rows[r].Cells[idisper].Value = Convert.ToDecimal(gridSourcemaster[i].DiscountPer);
                            dgv_exitsting_product.Rows[r].Cells[idisamt].Value = "0";
                            dgv_exitsting_product.Rows[r].Cells[iitemid].Value = Convert.ToInt32(gridSourcemaster[i].ItemId);
                            dgv_exitsting_product.Rows[r].Cells[itax].Value = Convert.ToString(gridSourcemaster[i].Tax);
                            dgv_exitsting_product.Rows[r].Cells[itaxtype].Value = Convert.ToString(gridSourcemaster[i].TaxType);
                            dgv_exitsting_product.Rows[r].Cells[itype].Value = Convert.ToInt32(gridSourcemaster[i].ProductType);
                            //dgv_exitsting_product.Rows[i].Cells[itaxsettingid].Value = Convert.ToInt32(gridSourcemaster[i].);
                            dgv_exitsting_product.Rows[r].Cells[istatusno].Value = 1;
                            dgv_exitsting_product.Rows[r].DefaultCellStyle.BackColor = Color.White;
                            dgv_exitsting_product.Rows[r].Cells[iofferstatus].Value = 0;
                            dgv_exitsting_product.Rows[r].Cells[itabletype].Value = 1;
                            totalcellamount_exit_product(r);
                            dgv_exitsting_product.Rows[r].Cells[ioriginalprice].Value = Convert.ToDecimal(dgv_exitsting_product.Rows[r].Cells[itotal].Value);
                            r = r + 1;
                            #endregion
                        }
                        else if (gridSourcemaster[i].TableType == 2)
                        {
                            #region Grid Inseration multiple 

                            dgv_exitsting_product.Rows[r].Cells[islno].Value = i + 1;
                            dgv_exitsting_product.Rows[r].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dgv_exitsting_product.Rows[r].Cells[ibarcode].Value = gridSourcemaster[i].ItemBarCode;
                            dgv_exitsting_product.Rows[r].Cells[ibarcode2].Value = gridSourcemaster[i].ItemBarCode;
                            dgv_exitsting_product.Rows[r].Cells[iname].Value = gridSourcemaster[i].ItemName;
                            //dgv_exitsting_product.Rows[i].Cells[iunit].Value = gridSourcemaster[i].un;
                            dgv_exitsting_product.Rows[r].Cells[iunitid].Value = gridSourcemaster[i].UnitId;
                            dgv_exitsting_product.Rows[r].Cells[iqty].Value = "1";
                            dgv_exitsting_product.Rows[r].Cells[iprate].Value = gridSourcemaster[i].PurchaseRate;
                            dgv_exitsting_product.Rows[r].Cells[israte].Value = gridSourcemaster[i].Srate;
                            dgv_exitsting_product.Rows[r].Cells[imrp].Value = gridSourcemaster[i].MRP;


                            dgv_exitsting_product.Rows[r].Cells[idisamt].Value = "0";
                            dgv_exitsting_product.Rows[r].Cells[iitemid].Value = gridSourcemaster[i].ItemId;
                            dgv_exitsting_product.Rows[r].Cells[itax].Value = gridSourcemaster[i].Tax;
                            dgv_exitsting_product.Rows[r].Cells[itaxtype].Value = gridSourcemaster[i].TaxType;
                            dgv_exitsting_product.Rows[r].Cells[itype].Value = gridSourcemaster[i].ProductType;
                            //dgv_exitsting_product.Rows[i].Cells[itaxsettingid].Value = gridSourcemaster[i].Field14;
                            dgv_exitsting_product.Rows[r].Cells[ipridno].Value = gridSourcemaster[i].PrId;
                            dgv_exitsting_product.Rows[r].Cells[istatusno].Value = 1;
                            dgv_exitsting_product.Rows[r].DefaultCellStyle.BackColor = Color.White;
                            dgv_exitsting_product.Rows[r].Cells[iofferstatus].Value = 0;
                            dgv_exitsting_product.Rows[r].Cells[itabletype].Value = 2;
                            totalcellamount_exit_product(r);
                            dgv_exitsting_product.Rows[r].Cells[ioriginalprice].Value = Convert.ToDecimal(dgv_exitsting_product.Rows[r].Cells[itotal].Value);
                            r = r + 1;
                            #endregion
                        }

                        else if (gridSourcemaster[i].TableType == 3)
                        {
                            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                            DmnSearch dmnSearch = new DmnSearch();
                            dmnSearch.Select = "ofm.OfferName Field1,ofm.FinalPrice Field2, ofm.TaxType Field3, ofm.Tax Field4, ofm.TaxSettingsId Field5, ofm.Discountper Field6, ofd.Productname Field7";
                            dmnSearch.Table = "tblOfferMaster ofm LEFT JOIN tblOfferDetails ofd on ofm.Omid=ofd.Omid";
                            dmnSearch.WhereNdGroupBy = "where ofm.Status=1 and  ofm.Omid=" + gridSourcemaster[i].ItemId;
                            dmnSearch.OrderBy = "";
                            var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                            if (gridSource.Count > 0)
                            {
                                #region Grid inseration Combo Product
                                dgv_exitsting_product.Rows.Add();               
                                dgv_exitsting_product.Rows[r].Cells[islno].Value = i + 1;
                                dgv_exitsting_product.Rows[r].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                dgv_exitsting_product.Rows[r].Cells[iproducttypeno].Value = Convert.ToString(gridSource[0].Field1);
                                dgv_exitsting_product.Rows[r].Cells[iqty].Value = gridSourcemaster[i].Quantity;
                                dgv_exitsting_product.Rows[r].Cells[israte].Value = Convert.ToDecimal(gridSource[0].Field2);
                                dgv_exitsting_product.Rows[r].Cells[itaxtype].Value = Convert.ToString(gridSource[0].Field3);
                                dgv_exitsting_product.Rows[r].Cells[itax].Value = Convert.ToString(gridSource[0].Field4);
                                dgv_exitsting_product.Rows[r].Cells[itaxsettingid].Value = Convert.ToInt32(gridSource[0].Field5);
                                dgv_exitsting_product.Rows[r].Cells[idisper].Value = Convert.ToDecimal(gridSource[0].Field6);
                                dgv_exitsting_product.Rows[r].Cells[iitemid].Value = Convert.ToInt32(gridSourcemaster[i].ItemId);
                                dgv_exitsting_product.Rows[r].Cells[iproducttypeno].Style.Font = new Font("Arial", 12, FontStyle.Bold);
                                dgv_exitsting_product.Rows[r].Cells[istatusno].Value = 1;
                                dgv_exitsting_product.Rows[r].Cells[iofferstatus].Value = 0;
                                dgv_exitsting_product.Rows[r].Cells[isubofferid].Value = 0;
                                dgv_exitsting_product.Rows[r].Cells[itabletype].Value = 3;
                                dgv_exitsting_product.Rows[r].DefaultCellStyle.BackColor = Color.DarkGray;
                                totalcellamount_exit_product(r);
                                dgv_exitsting_product.Rows[r].Cells[ioriginalprice].Value = Convert.ToDecimal(dgv_exitsting_product.Rows[r].Cells[itotal].Value);

                                dgv_exitsting_product.Rows.Add(gridSource.Count);
                                 r = r+1;
                                for (int j = 0; j < gridSource.Count; j++)
                                {                                    
                                    #region Grid Inseration sub combo product
                                    dgv_exitsting_product.Rows[r].Cells[islno].Value = j + 1;
                                    dgv_exitsting_product.Rows[r].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    dgv_exitsting_product.Rows[r].Cells[ibarcode].Value = 0;
                                    dgv_exitsting_product.Rows[r].Cells[ibarcode2].Value = 0;
                                    dgv_exitsting_product.Rows[r].Cells[iname].Value = Convert.ToString(gridSource[j].Field7);
                                    dgv_exitsting_product.Rows[r].Cells[iprate].Value = 0;
                                    dgv_exitsting_product.Rows[r].Cells[imrp].Value = 0;
                                    dgv_exitsting_product.Rows[r].Cells[idisamt].Value = "0";
                                    dgv_exitsting_product.Rows[r].Cells[itype].Value = 0;
                                    dgv_exitsting_product.Rows[r].Cells[istatusno].Value = 1;
                                    dgv_exitsting_product.Rows[r].Cells[iofferstatus].Value = 1;
                                    dgv_exitsting_product.Rows[r].Cells[isubofferid].Value = Convert.ToInt32(gridSource[0].Id);
                                    dgv_exitsting_product.Rows[r].DefaultCellStyle.BackColor = Color.OliveDrab;
                                    #endregion
                                    r = r + 1;
                                }
                                #endregion
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
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
                dmnSearch.Select = "p.Product as Field1,p.ProductId as Field2,0 as Field3,fav_status as Field4 ";
                dmnSearch.Table = "tblProduct p LEFT JOIN tblMenu m on m.ProductId=p.ProductId where p.Status=1 and p.Fcid= " + clickedControl.Tag + "and m.Status=1 " +
                    " Union Select Itemname as Field1,ItemId as Field2,Saletype as Field3,fav_status as Field4 from tblItem";
                dmnSearch.WhereNdGroupBy = "where Fcid= " + clickedControl.Tag + " and Saletype=1 and Status=1  and (Select sum(QOH) from tblProductRate where ProductId=ItemId) > 0";
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

                        // Main Panel for each product
                        Panel panelProductBox = new Panel();
                        panelProductBox.Name = "panel_product_box" + gridSource[i].Field1;
                        panelProductBox.Dock = DockStyle.None;
                        panelProductBox.Width = 150;
                        panelProductBox.Height = 55;
                        panelProductBox.BackColor = product_buttoncolor;
                        panelProductBox.Margin = new Padding(10, 10, 0, 0);

                        // Configure the Button
                        Button button = new Button();
                        button.Text = gridSource[i].Field1;
                        button.Name = "btn" + gridSource[i].Field1;
                        button.Tag = gridSource[i].Field2;
                        button.AccessibleDescription = gridSource[i].Field3;
                        button.FlatAppearance.BorderSize = 0; // Set border width to 0
                        button.FlatAppearance.BorderColor = button.BackColor;
                        button.ForeColor = Color.White;
                        button.Width = 120;
                        button.Height = 50;
                        button.Font = new Font("Arial", 9, FontStyle.Bold);
                        button.FlatStyle = FlatStyle.Flat;
                        button.BackColor = product_buttoncolor;
                        //// Create a circular Region
                        button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                        //button.Dock = DockStyle.Fill;
                        button.Click += new EventHandler(DashboardButtonClickEventHandler);
                        panelProductBox.Controls.Add(button);

                        // PictureBox fav
                        PictureBox pictureBoxFav = new PictureBox();
                        pictureBoxFav.Name = "picBoxFav" + gridSource[i].Field1;
                        pictureBoxFav.Tag = gridSource[i].Field2;
                        pictureBoxFav.AccessibleDescription = gridSource[i].Field3;
                        pictureBoxFav.Width = 20;
                        pictureBoxFav.Height = 15;
                        pictureBoxFav.BorderStyle = BorderStyle.None;
                        pictureBoxFav.Click += new EventHandler(buttonfavClickEventHandler);
                        pictureBoxFav.Image = null;
                        if (Convert.ToInt32(gridSource[i].Field4) == 1)
                            pictureBoxFav.Image = Properties.Resources.yellow_star;
                        else
                            pictureBoxFav.Image = Properties.Resources.white_star;

                        pictureBoxFav.SizeMode = PictureBoxSizeMode.StretchImage;
                        panelProductBox.Controls.Add(pictureBoxFav);
                        pictureBoxFav.BringToFront();

                        tlp_searchproduct_dashboard.Controls.Add(panelProductBox, columnIndex, rowIndex);
                        columnIndex++;
                    }
                    pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void buttonfavClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                BllProducts bllfav = new BllProducts();
                DmnGridFields dmnfav = new DmnGridFields();
                Control clickedControl = sender as Control;
                string pb_favname = clickedControl.Name;
                string id = Convert.ToString(clickedControl.Tag);
                PictureBox pbfav = this.Controls.Find(pb_favname, true).FirstOrDefault() as PictureBox;

                if (ImageEquals(pbfav.Image, Properties.Resources.white_star))
                {
                    pbfav.Image = Properties.Resources.yellow_star;
                    dmnfav.Field2 = "1";
                }
                else
                {
                    pbfav.Image = Properties.Resources.white_star;
                    dmnfav.Field2 = "0";
                }
                pbfav.SizeMode = PictureBoxSizeMode.StretchImage;

                if (Convert.ToInt32(clickedControl.AccessibleDescription) == 1)
                    dmnfav.Field3 = "1";
                else
                    dmnfav.Field3 = "0";
                dmnfav.Field1 = id;
                var InsertStatusP = bllfav.AddEditData_fav("prd_insert_upd_fav", dmnfav);
            }
            catch (Exception ex)
            {

            }
        }
        private bool ImageEquals(Image image1, Image image2)
        {
            if (image1 == null || image2 == null)
                return false;

            using (MemoryStream ms1 = new MemoryStream(), ms2 = new MemoryStream())
            {
                image1.Save(ms1, ImageFormat.Png);
                image2.Save(ms2, ImageFormat.Png);

                return ms1.ToArray().SequenceEqual(ms2.ToArray());
            }
        }
        public void checksave_refresh()
        {
            try
            {
                int count = 0;
                Dgv_Selected_Item.Refresh();
                for (int i = 0; i < Dgv_Selected_Item.RowCount; i++)
                {
                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value)>0 && Dgv_Selected_Item.Rows[i].Cells[iitemid].Value!=null)
                    {
                        count = count + 1;
                        break;
                    }
                }

                if (count ==1)
                {
                    Showprint_Save(Convert.ToInt32(ShowButtonType.save));
                }
                else 
                {
                    if (ordercount == gridSourcemaster.Count && OrderRunning_Status)                        
                    {
                        Showprint_Save(Convert.ToInt32(ShowButtonType.Print));
                    }
                    else
                    {
                        if (count == 0)                        
                            Showprint_Save(Convert.ToInt32(ShowButtonType.Nnone));
                        else                       
                            Showprint_Save(Convert.ToInt32(ShowButtonType.save));
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        public bool Checkoffer(int id)
        {
            try
            {
                gridSource_offer = null;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "p.ItemCode_UPC as Field1, p.Itemname as Field2,p.srate as Field3,p.MRP as Field4,p.ItemId as Field5,p.Tax as Field6,p.TaxType as Field7,p.Fcid as Field8,p.TaxSettingsId as Field9,ofd.Discountper as Field10";
                dmnSearch.Table = "tblItem p LEFT JOIN tblOfferDetails ofd on ofd.ProductId=p.ItemId and ofd.status=1" +
                    " LEFT JOIN tblOfferMaster ofm on ofm.Omid=ofd.Omid AND ofm.OfferType=0 and ofm.status=1 ";
                dmnSearch.WhereNdGroupBy = "where p.Status=1 and p.ItemId= " + id + " AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME()";
                dmnSearch.OrderBy = "";
                 gridSource_offer = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource_offer.Count == 0)
                    return false;
                else
                    return true;
            }
            catch(Exception ex)
            {
                return false;
            }          
        }
        private void DashboardButtonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                tlp_seletedItem_main.AutoScroll = true;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                if (Convert.ToInt32(clickedControl.AccessibleDescription) == 1)
                    CheckMultiplePrice(Convert.ToInt32(clickedControl.Tag));
                else if (Convert.ToInt32(clickedControl.AccessibleDescription) == 3)
                    ComboProductFill(Convert.ToInt32(clickedControl.Tag));
                else
                {
                    dmnSearch.Select = "p.ItemCode as Field1, p.Product as Field2,p.Price as Field3,p.MRP as Field4,p.ProductId as Field5,p.Tax as Field6,p.TaxType as Field7,p.Fcid as Field8,p.TaxSettingsId as Field9,ofd.Discountper as Field10,ofm.offertype Field11,p.DiscountPer Field12";
                    dmnSearch.Table = "tblProduct p " +
                        "LEFT JOIN tblMenu m on m.ProductId=p.ProductId " +
                        "LEFT JOIN tblOfferDetails ofd on ofd.ProductId=p.ProductId and ofd.status=1 " +
                        "LEFT JOIN tblOfferMaster ofm on ofm.Omid=ofd.Omid and ofm.status=1 AND ofm.OfferType=0  AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME()"; 
                    dmnSearch.WhereNdGroupBy = "where p.Status=1 and m.Status=1  and p.ProductId= " + clickedControl.Tag;
                    dmnSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    if (gridSource.Count > 0)
                    {
                        string tlpname = "tlp_seletedItem_col" + clickedControl.Tag+"_"+ Convert.ToInt32(Tabletype.Product);
                        TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                        if (tlp_sele != null)
                        {
                            string txtname = "txtqty_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                            TextBox qty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                            int cur_qty = Convert.ToInt32(qty.Text) + 1;
                            qty.Text = Convert.ToString(cur_qty);
                            CalcTotalAmt_Add(Convert.ToInt32(tlp_sele.Tag), 1);
                            tlp_sele.Focus();
                            netAmount();
                        }
                        else
                        {
                            if(Booking_Status==false)
                            {
                                if (string.IsNullOrEmpty(txtGrandTotal.Text))
                                    txtGrandTotal.Text = "0";
                            }

                            #region Grid Inseration
                            int exit_orderdetailid = 0;
                            if (gridSourcemaster.Count > 0)
                            {
                                for (int i = 0; i < gridSourcemaster.Count; i++)
                                {
                                    if (Convert.ToInt32(gridSourcemaster[i].Status) == 3
                                        && Convert.ToInt32(gridSourcemaster[i].PrId) == 0
                                        && Convert.ToInt32(gridSourcemaster[i].ItemId) == Convert.ToInt32(gridSource[0].Field5))
                                    {
                                        exit_orderdetailid = gridSourcemaster[i].OrderDetailsId;
                                        break;
                                    }
                                }
                            }

                            r1 = Dgv_Selected_Item.RowCount - 1;
                            Dgv_Selected_Item.Rows[r1].Cells[islno].Value = r1 + 1;
                            Dgv_Selected_Item.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = Convert.ToString(gridSource[0].Field1);
                            Dgv_Selected_Item.Rows[r1].Cells[ibarcode2].Value = Convert.ToString(gridSource[0].Field1);
                            Dgv_Selected_Item.Rows[r1].Cells[iname].Value = Convert.ToString(gridSource[0].Field2);
                            Dgv_Selected_Item.Rows[r1].Cells[iqty].Value = "1";
                            Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = Convert.ToDecimal(gridSource[0].Field3);
                            Dgv_Selected_Item.Rows[r1].Cells[israte].Value = Convert.ToDecimal(gridSource[0].Field3);
                            Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = Convert.ToDecimal(gridSource[0].Field4);

                            if (Convert.ToDecimal(gridSource[0].Field10)>0)
                                Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = Convert.ToDecimal(gridSource[0].Field10);
                            else
                                Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = Convert.ToDecimal(gridSource[0].Field12);

                            Dgv_Selected_Item.Rows[r1].Cells[idisamt].Value = "0";
                            Dgv_Selected_Item.Rows[r1].Cells[iitemid].Value = Convert.ToInt32(gridSource[0].Field5);
                            Dgv_Selected_Item.Rows[r1].Cells[itax].Value = Convert.ToString(gridSource[0].Field6);
                            Dgv_Selected_Item.Rows[r1].Cells[itaxtype].Value = Convert.ToString(gridSource[0].Field7);
                            Dgv_Selected_Item.Rows[r1].Cells[itype].Value = Convert.ToInt32(gridSource[0].Field8);
                            Dgv_Selected_Item.Rows[r1].Cells[itaxsettingid].Value = Convert.ToInt32(gridSource[0].Field9);
                            Dgv_Selected_Item.Rows[r1].Cells[istatusno].Value = 1;
                            Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = Color.White;
                            Dgv_Selected_Item.Rows[r1].Cells[iofferstatus].Value = 0;
                            Dgv_Selected_Item.Rows[r1].Cells[itabletype].Value = 1;

                            if (gridSource[0].Field11 == null || gridSource[0].Field11 == "")
                                Dgv_Selected_Item.Rows[r1].Cells[ioffertype].Value = 3;
                            else
                                Dgv_Selected_Item.Rows[r1].Cells[ioffertype].Value = Convert.ToInt32(gridSource[0].Field11);

                            totalcellamount(r1);
                            Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value);
                            Dgv_Selected_Item.Rows.Add(1);
                            GridSlno_Refresh();

                            decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                            txtGrandTotal.Text = Convert.ToString(totalamt);
                            txtnetamount.Text = Convert.ToString(totalamt);
                            #endregion
                            netAmount();

                            // Configure the TableLayoutPanel
                            TableLayoutPanel tlp_seletedItem_col = new TableLayoutPanel();
                            float[] columnPercentages = { 44.82f, 6.27f, 14.94f, 6.02f, 19.04f, 8.92f };
                            for (int i = 0; i < columnPercentages.Length; i++)
                            {
                                tlp_seletedItem_col.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercentages[i]));
                            }

                            tlp_seletedItem_col.Name = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product); 
                            tlp_seletedItem_col.Tag = clickedControl.Tag;
                            tlp_seletedItem_col.AccessibleDescription = "1";
                            tlp_seletedItem_col.Dock = DockStyle.Top;
                            tlp_seletedItem_col.BackColor = Color.LightBlue;
                            tlp_seletedItem_col.Height = 45;

                            // Configure the Button for Item name
                            Button buttonitemname = new Button();
                            buttonitemname.Text = gridSource[0].Field2;
                            buttonitemname.Name = "buttonitemname_" + gridSource[0].Field2 + "_" + Convert.ToInt32(Tabletype.Product);
                            buttonitemname.AccessibleDescription = "1";
                            buttonitemname.Tag = gridSource[0].Field5; ;
                            buttonitemname.BackColor = Color.Beige;
                            buttonitemname.Height = 40;
                            buttonitemname.Font = new Font(buttonitemname.Font.FontFamily, 8, FontStyle.Bold);
                            buttonitemname.Click += new EventHandler(buttonitemnameClickEventHandler);

                            // Configure the Button for add Qty
                            Button buttonAdd = new Button();
                            buttonAdd.Text = "+";
                            buttonAdd.Name = "buttonAdd_" + gridSource[0].Field5 + "_" + Convert.ToInt32(Tabletype.Product);
                            buttonAdd.Tag = gridSource[0].Field5;
                            buttonAdd.AccessibleDescription = "1";
                            buttonAdd.BackColor = Color.Beige;
                            buttonAdd.Margin = new Padding(0, 14, 0, 0);
                            buttonAdd.Font = new Font(buttonAdd.Font.FontFamily, 8, FontStyle.Bold);
                            buttonAdd.Click += new EventHandler(ButtonbuttonAddClickEventHandler);

                            // Configure the textbox enter QTY
                            TextBox txtqty = new TextBox();
                            txtqty.Text = "1";
                            txtqty.Name = "txtqty_" + gridSource[0].Field5 + "_" + Convert.ToInt32(Tabletype.Product);
                            txtqty.Tag = gridSource[0].Field5;
                            txtqty.AccessibleDescription = "1";
                            txtqty.BackColor = Color.Beige;
                            txtqty.TextAlign = HorizontalAlignment.Center;
                            txtqty.Margin = new Padding(0, 14, 0, 0);
                            txtqty.Font = new Font(txtqty.Font.FontFamily, 8, FontStyle.Bold);
                            txtqty.Enabled = false;

                            // Configure the textbox enter QTY
                            Button buttonRemove = new Button();
                            buttonRemove.Text = "-";
                            buttonRemove.Name = "btn_remove_" + gridSource[0].Field5 + "_" + Convert.ToInt32(Tabletype.Product);
                            buttonRemove.Tag = gridSource[0].Field5;
                            buttonRemove.AccessibleDescription = "1";
                            buttonRemove.BackColor = Color.Beige;
                            buttonRemove.Margin = new Padding(0, 14, 0, 0);
                            buttonRemove.Font = new Font(buttonRemove.Font.FontFamily, 8, FontStyle.Bold);
                            buttonRemove.Click += new EventHandler(ButtonRemoveClickEventHandler);

                            // Configure the textbox Price
                            TextBox txtprice = new TextBox();
                            decimal F_price = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value);
                            txtprice.Text = Convert.ToString(F_price);
                            txtprice.Name = "txtprice_" + gridSource[0].Field5 + "_" + Convert.ToInt32(Tabletype.Product);
                            txtprice.Tag = gridSource[0].Field5;
                            txtprice.BackColor = Color.Beige;
                            txtprice.TextAlign = HorizontalAlignment.Center;
                            txtprice.Margin = new Padding(0, 14, 0, 0);
                            txtprice.Font = new Font(txtprice.Font.FontFamily, 8, FontStyle.Bold);
                            txtprice.Enabled = false;


                            // Configure Item Delete Button
                            PictureBox pb_item_delete = new PictureBox();
                            pb_item_delete.Name = "btn_remove_" + gridSource[0].Field5 + "_" + Convert.ToInt32(Tabletype.Product);
                            pb_item_delete.AccessibleDescription = "1";
                            pb_item_delete.Tag = gridSource[0].Field5;
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
                            tlp_seletedItem_col.Focus();
                            checksave_refresh();
                        }
                    }

                }

                checksave_refresh();
            }
            catch (Exception ex)
            {
            }
        }

        public void GridSlno_Refresh()
        {
            try
            {
                int i = 1;
                for (int j = 0; j < Dgv_Selected_Item.RowCount; j++)
                {
                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[j].Cells[iofferstatus].Value) == 0 && Dgv_Selected_Item.Rows[j].Cells[iofferstatus].Value != null)
                    {
                        Dgv_Selected_Item.Rows[j].Cells[islno].Value = i;
                        i = i + 1;
                    }
                        
                }
            }
            catch(Exception ex)
            {

            }
        }
       public void ComboProductFill(int id)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "ofm.omid id , ofm.OfferName Field1 ,ofm.Discountper Field2, ofm.tax Field3 ,ofm.taxtype Field4,ofm.TaxSettingsId Field5,ofm.FinalPrice Field6,ofd.Productname Field7,ofd.ProductId Field8,ofd.Prid Field9";
                dmnSearch.Table = "tblOfferMaster ofm LEFT JOIN tblOfferDetails ofd on ofd.Omid=ofm.Omid";
                dmnSearch.WhereNdGroupBy = "where ofm.Status=1 and ofd.Omid = " + id;
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    string tlpname = "tlp_seletedItem_col" + gridSource[0].Id + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                    TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                    if (tlp_sele != null)
                    {
                        string txtname = "txtqty_" + gridSource[0].Id + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                        TextBox qty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                        int cur_qty = Convert.ToInt32(qty.Text) + 1;
                        qty.Text = Convert.ToString(cur_qty);
                        CalcTotalAmt_Add_Combo(Convert.ToInt32(tlp_sele.Tag), 1);
                        tlp_sele.Focus();
                        netAmount();
                    }
                    else
                    {
                        if (Booking_Status == false)
                        {
                            if (string.IsNullOrEmpty(txtGrandTotal.Text))
                                txtGrandTotal.Text = "0";
                        }

                        decimal itemprice = 0;
                        #region Grid inseration Combo Product
                        r1 = Dgv_Selected_Item.RowCount - 1;
                        Dgv_Selected_Item.Rows[r1].Cells[islno].Value = r1 + 1;
                        Dgv_Selected_Item.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Dgv_Selected_Item.Rows[r1].Cells[iproducttypeno].Value = Convert.ToString(gridSource[0].Field1);
                        Dgv_Selected_Item.Rows[r1].Cells[iqty].Value = "1";
                        Dgv_Selected_Item.Rows[r1].Cells[israte].Value = Convert.ToDecimal(gridSource[0].Field6);
                        Dgv_Selected_Item.Rows[r1].Cells[itaxtype].Value = Convert.ToString(gridSource[0].Field4);
                        Dgv_Selected_Item.Rows[r1].Cells[itax].Value = Convert.ToString(gridSource[0].Field3);
                        Dgv_Selected_Item.Rows[r1].Cells[itaxsettingid].Value = Convert.ToInt32(gridSource[0].Field5);
                        Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = Convert.ToDecimal(gridSource[0].Field2);
                        Dgv_Selected_Item.Rows[r1].Cells[iitemid].Value = Convert.ToInt32(gridSource[0].Id);
                        Dgv_Selected_Item.Rows[r1].Cells[iproducttypeno].Style.Font = new Font("Arial", 12, FontStyle.Bold);
                        Dgv_Selected_Item.Rows[r1].Cells[istatusno].Value = 1;
                        Dgv_Selected_Item.Rows[r1].Cells[iofferstatus].Value = 0;
                        Dgv_Selected_Item.Rows[r1].Cells[isubofferid].Value = 0;
                        Dgv_Selected_Item.Rows[r1].Cells[itabletype].Value = 3;
                        Dgv_Selected_Item.Rows[r1].Cells[ioffertype].Value = 1;

                        Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = Color.DarkGray;
                        Dgv_Selected_Item.Rows.Add(1);
                        totalcellamount(r1);            
                        Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value);
                        itemprice = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value);
                        decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                        txtGrandTotal.Text = Convert.ToString(totalamt);
                        txtnetamount.Text = Convert.ToString(totalamt);
                        for (int j = 0; j < gridSource.Count; j++)
                        {   
                            #region Grid Inseration
                            int exit_orderdetailid = 0;                  
                            r1 = Dgv_Selected_Item.RowCount - 1;
                            Dgv_Selected_Item.Rows[r1].Cells[islno].Value = j + 1;
                            Dgv_Selected_Item.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                            Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = 0;
                            Dgv_Selected_Item.Rows[r1].Cells[ibarcode2].Value = 0;          
                            Dgv_Selected_Item.Rows[r1].Cells[iname].Value = Convert.ToString(gridSource[j].Field7); 
                            Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = 0;
                            Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = 0;                      
                            Dgv_Selected_Item.Rows[r1].Cells[idisamt].Value = "0";

                            //if(Convert.ToInt32(gridSource[j].Field9)>0)
                            //    Dgv_Selected_Item.Rows[r1].Cells[iitemid].Value = Convert.ToInt32(gridSource[j].Field9);
                            //else
                            //    Dgv_Selected_Item.Rows[r1].Cells[iitemid].Value = Convert.ToInt32(gridSource[j].Field8);

                            Dgv_Selected_Item.Rows[r1].Cells[itype].Value = 0;
                            Dgv_Selected_Item.Rows[r1].Cells[istatusno].Value = 1;
                            Dgv_Selected_Item.Rows[r1].Cells[iofferstatus].Value = 1;
                            Dgv_Selected_Item.Rows[r1].Cells[isubofferid].Value = Convert.ToInt32(gridSource[0].Id); 
                            Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = Color.OliveDrab;
                            Dgv_Selected_Item.Rows.Add(1);      
                            #endregion
                  
                        }
                        #endregion
                        netAmount();
                        GridSlno_Refresh();

                        // Configure the TableLayoutPanel
                        TableLayoutPanel tlp_seletedItem_col = new TableLayoutPanel();
                        float[] columnPercentages = { 44.82f, 6.27f, 14.94f, 6.02f, 19.04f, 8.92f };
                        for (int i = 0; i < columnPercentages.Length; i++)
                        {
                            tlp_seletedItem_col.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercentages[i]));
                        }

                        tlp_seletedItem_col.Name = "tlp_seletedItem_col" + gridSource[0].Id + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                        tlp_seletedItem_col.Tag = gridSource[0].Id;
                        tlp_seletedItem_col.AccessibleDescription = "3";
                        tlp_seletedItem_col.Dock = DockStyle.Top;
                        tlp_seletedItem_col.BackColor = Color.LightBlue;
                        tlp_seletedItem_col.Height = 45;

                        // Configure the Button for Item name
                        Button buttonitemname = new Button();
                        buttonitemname.Text = gridSource[0].Field1;
                        buttonitemname.Name = "buttonitemname_" + gridSource[0].Id + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                        buttonitemname.Tag = gridSource[0].Id;
                        buttonitemname.AccessibleDescription = "3"+ Convert.ToInt32(Tabletype.ComboProduct);
                        buttonitemname.BackColor = Color.Beige;
                        buttonitemname.Height = 40;
                        buttonitemname.Font = new Font(buttonitemname.Font.FontFamily, 8, FontStyle.Bold);
                        buttonitemname.Click += new EventHandler(buttonitemnameClickEventHandler);

                        // Configure the Button for add Qty
                        Button buttonAdd = new Button();
                        buttonAdd.Text = "+";
                        buttonAdd.Name = "buttonAdd_" + gridSource[0].Id + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                        buttonAdd.Tag = gridSource[0].Id;
                        buttonAdd.AccessibleDescription = "3";
                        buttonAdd.BackColor = Color.Beige;
                        buttonAdd.Margin = new Padding(0, 14, 0, 0);
                        buttonAdd.Font = new Font(buttonAdd.Font.FontFamily, 8, FontStyle.Bold);
                        buttonAdd.Click += new EventHandler(ButtonbuttonAddClickEventHandler);

                        // Configure the textbox enter QTY
                        TextBox txtqty = new TextBox();
                        txtqty.Text = "1";
                        txtqty.Name = "txtqty_" + gridSource[0].Id + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                        txtqty.Tag = gridSource[0].Id;
                        txtqty.AccessibleDescription = "3";
                        txtqty.BackColor = Color.Beige;
                        txtqty.TextAlign = HorizontalAlignment.Center;
                        txtqty.Margin = new Padding(0, 14, 0, 0);
                        txtqty.Font = new Font(txtqty.Font.FontFamily, 8, FontStyle.Bold);
                        txtqty.Enabled = false;

                        // Configure the textbox enter QTY
                        Button buttonRemove = new Button();
                        buttonRemove.Text = "-";
                        buttonRemove.Name = "btn_remove_" + gridSource[0].Id + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                        buttonRemove.Tag = gridSource[0].Id;
                        buttonRemove.AccessibleDescription = "3";
                        buttonRemove.BackColor = Color.Beige;
                        buttonRemove.Margin = new Padding(0, 14, 0, 0);
                        buttonRemove.Font = new Font(buttonRemove.Font.FontFamily, 8, FontStyle.Bold);
                        buttonRemove.Click += new EventHandler(ButtonRemoveClickEventHandler);

                        // Configure the textbox Price
                        TextBox txtprice = new TextBox();
                        txtprice.Text = Convert.ToString(itemprice);
                        txtprice.Name = "txtprice_" + gridSource[0].Id + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                        txtprice.Tag = gridSource[0].Id;
                        txtprice.AccessibleDescription = "3";
                        txtprice.BackColor = Color.Beige;
                        txtprice.TextAlign = HorizontalAlignment.Center;
                        txtprice.Margin = new Padding(0, 14, 0, 0);
                        txtprice.Font = new Font(txtprice.Font.FontFamily, 8, FontStyle.Bold);
                        txtprice.Enabled = false;

                        // Configure Item Delete Button
                        PictureBox pb_item_delete = new PictureBox();
                        pb_item_delete.Name = "btn_remove_" + gridSource[0].Id + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                        pb_item_delete.Tag = gridSource[0].Id;
                        pb_item_delete.AccessibleDescription = "3";
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
                        tlp_seletedItem_col.Focus();
                        checksave_refresh();
                    }
                }
                }
            catch (Exception ex)
            {

            }
        }
        public void CheckMultiplePrice(int id)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch1 = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "i.Itemname Field1, pr.SRate Field2, pr.QOH Field3,pr.Id Field4,i.ItemId Field5";
                objSearch.Table = "tblitem i left join tblProductRate pr on pr.ProductId = i.ItemId";
                objSearch.WhereNdGroupBy = "where i.status = 1 and  pr.QOH > 0 and i.itemid=" + id;
                objSearch.OrderBy = "";
                var gridSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 1)
                {
                    //Checkoffer(id);
                    Control controlclose;
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
                   // tlp_price_list.Controls.Add(controlclose, 0, 0);

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
                    int panelWidthPercentage = 25;
                    int panelheightPercentage = 40;
                    int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                    int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                    pnlprice.Size = new Size(panelWidth, panelHeight);
                    pnlprice.Location = new Point(this.ClientSize.Width / 2 - pnlprice.Size.Width / 2, this.ClientSize.Height / 2 - pnlprice.Size.Height / 2);
                    tlp_price_list.AutoScroll = true;
                    tlp_multipleprice.Controls.Add(tlp_price_list,0,1);
                } //item with multiple price 
                else
                {
                    if (gridSource.Count > 0)
                    {               
                        pnlprice.Visible = false;
                        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                        DmnSearch dmnSearch = new DmnSearch();         
                        dmnSearch.Select = "Itemname as Field1,ItemId as Field2,ItemCode_UPC as Field3,pr.prate as Field4,pr.srate as Field5," +
                        "pr.MRP as Field6,i.Tax as Field7,i.TaxType as Field8,i.TaxSettingsId as Field9,pr.QOH as Field10," +
                        "i.unit as Field11,u.UnitName as Field12,i.Fcid as Field13,i.TaxSettingsId as Field14,pr.id Field15,i.DiscountPer Field16,pr.QOH Field17";
                        dmnSearch.Table = "tblItem i LEFT JOIN tblProductRate pr on  pr.ProductId=i.ItemId LEFT JOIN tblUnit u on  u.UId=i.unit ";
                        dmnSearch.WhereNdGroupBy = "where i.Status=1 and pr.ProductId = " + gridSource[0].Field5 + " and pr.id = " + gridSource[0].Field4;
                        dmnSearch.OrderBy = "";
                        var gridSourceDetails = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSourceDetails.Count > 0)
                        {
                            string tlpname = "tlp_seletedItem_col" + gridSource[0].Field4 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                            TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                            if (Booking_Status == false)
                            {
                                if (string.IsNullOrEmpty(txtGrandTotal.Text))
                                    txtGrandTotal.Text = "0";
                            }

                            if (tlp_sele != null)
                            {     
                                int rowno = 0;
                                for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                                {
                                    if (Convert.ToInt32(gridSourceDetails[0].Field15) == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value))
                                    {
                                        Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                                        if(Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) > Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value))
                                        {
                                            CMessageBox.Show("Aviablle Quantity is : " + Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value), "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            return;
                                        }
                                        else
                                        {
                                            string txtname = "txtqty_" + gridSource[0].Field4 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                            TextBox qty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                                            int cur_qty = Convert.ToInt32(qty.Text) + 1;
                                            qty.Text = Convert.ToString(cur_qty);

                                            rowno = i;
                                            totalcellamount(i);
                                            break;
                                        }
                     
                                    }
                                }               
                                decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[ioriginalprice].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                                txtGrandTotal.Text = Convert.ToString(totalamt);
                                netAmount();
                                tlp_sele.Focus();

                            }
                            else
                            {
                                //for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                                //{
                                //    if (Convert.ToInt32(gridSourceDetails[0].Field15) == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value))
                                //    {
                                //        Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                                //        totalcellamount(i);
                                //        break;
                                //    }
                                //}               

                                #region Grid Inseration multiple 
                                r1 = Dgv_Selected_Item.RowCount - 1;

                                Dgv_Selected_Item.Rows[r1].Cells[islno].Value = r1 + 1;
                                Dgv_Selected_Item.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = gridSourceDetails[0].Field3;
                                Dgv_Selected_Item.Rows[r1].Cells[ibarcode2].Value = gridSourceDetails[0].Field3;
                                Dgv_Selected_Item.Rows[r1].Cells[iname].Value = gridSourceDetails[0].Field1;
                                Dgv_Selected_Item.Rows[r1].Cells[iunit].Value = gridSourceDetails[0].Field12;
                                Dgv_Selected_Item.Rows[r1].Cells[iunitid].Value = gridSourceDetails[0].Field11;
                                Dgv_Selected_Item.Rows[r1].Cells[iqty].Value = "1";
                                Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = gridSourceDetails[0].Field4;
                                Dgv_Selected_Item.Rows[r1].Cells[israte].Value = gridSourceDetails[0].Field5;
                                Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = gridSourceDetails[0].Field6;
                                if (Checkoffer(id))
                                {
                                    Dgv_Selected_Item.Rows[r1].Cells[idisper].Value =gridSource_offer[0].Field10;
                                    Dgv_Selected_Item.Rows[r1].Cells[ioffertype].Value = 0;
                                }
                                else
                                {
                                    Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = gridSourceDetails[0].Field16;
                                    Dgv_Selected_Item.Rows[r1].Cells[ioffertype].Value = 3;
                                }
                                Dgv_Selected_Item.Rows[r1].Cells[idisamt].Value = "0";
                                Dgv_Selected_Item.Rows[r1].Cells[iitemid].Value = gridSourceDetails[0].Field2;
                                Dgv_Selected_Item.Rows[r1].Cells[itax].Value = gridSourceDetails[0].Field7;
                                Dgv_Selected_Item.Rows[r1].Cells[itaxtype].Value = gridSourceDetails[0].Field8;
                                Dgv_Selected_Item.Rows[r1].Cells[itype].Value = gridSourceDetails[0].Field13;
                                Dgv_Selected_Item.Rows[r1].Cells[itaxsettingid].Value = gridSourceDetails[0].Field14;
                                Dgv_Selected_Item.Rows[r1].Cells[ipridno].Value = gridSourceDetails[0].Field15;
                                Dgv_Selected_Item.Rows[r1].Cells[istatusno].Value = 1;
                                Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = Color.White;
                                Dgv_Selected_Item.Rows[r1].Cells[iofferstatus].Value = 0;
                                Dgv_Selected_Item.Rows[r1].Cells[itabletype].Value = 2;
                                Dgv_Selected_Item.Rows[r1].Cells[ioriginalqty].Value = gridSourceDetails[0].Field17;
                                
                                totalcellamount(r1);
                                Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value);
         
                                decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                                txtGrandTotal.Text = Convert.ToString(totalamt);

                                netAmount();
                                Dgv_Selected_Item.Rows.Add(1);
                                GridSlno_Refresh();

                                #endregion

                                // Configure the TableLayoutPanel
                                TableLayoutPanel tlp_seletedItem_col = new TableLayoutPanel();
                                float[] columnPercentages = { 44.82f, 6.27f, 14.94f, 6.02f, 19.04f, 8.92f };
                                for (int i = 0; i < columnPercentages.Length; i++)
                                {
                                    tlp_seletedItem_col.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercentages[i]));
                                }

                                tlp_seletedItem_col.Name = "tlp_seletedItem_col" + Convert.ToInt32(gridSourceDetails[0].Field15) + "_" + Convert.ToInt32(Tabletype.DirectSale); ;
                                tlp_seletedItem_col.Tag = gridSourceDetails[0].Field15;
                                tlp_seletedItem_col.AccessibleDescription = "2";
                                tlp_seletedItem_col.BackColor = Color.LightBlue;
                                tlp_seletedItem_col.Height = 45;

                                // Configure the Button for Item name
                                Button buttonitemname = new Button();
                                buttonitemname.Text = gridSourceDetails[0].Field1;
                                buttonitemname.Name = "buttonitemname_" + gridSourceDetails[0].Field15 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                buttonitemname.Tag = gridSourceDetails[0].Field15;
                                buttonitemname.AccessibleDescription = "1" + Convert.ToInt32(Tabletype.DirectSale);
                                buttonitemname.BackColor = Color.Beige;
                                buttonitemname.Height = 40;
                                buttonitemname.Font = new Font(buttonitemname.Font.FontFamily, 8, FontStyle.Bold);
                                buttonitemname.Click += new EventHandler(buttonitemnameClickEventHandler);

                                // Configure the Button for add Qty
                                Button buttonAdd = new Button();
                                buttonAdd.Text = "+";
                                buttonAdd.Name = "buttonAdd_" + gridSourceDetails[0].Field15 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                buttonAdd.Tag = gridSourceDetails[0].Field15;
                                buttonAdd.AccessibleDescription = "2";
                                buttonAdd.BackColor = Color.Beige;
                                buttonAdd.Margin = new Padding(0, 14, 0, 0);
                                buttonAdd.Font = new Font(buttonAdd.Font.FontFamily, 8, FontStyle.Bold);
                                buttonAdd.Click += new EventHandler(ButtonbuttonAddClickEventHandler);

                                // Configure the textbox enter QTY
                                TextBox txtqty = new TextBox();
                                txtqty.Text = "1";
                                txtqty.Name = "txtqty_" + gridSourceDetails[0].Field15 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                txtqty.Tag = gridSourceDetails[0].Field15;
                                txtqty.AccessibleDescription = "2";
                                txtqty.BackColor = Color.Beige;
                                txtqty.TextAlign = HorizontalAlignment.Center;
                                txtqty.Margin = new Padding(0, 14, 0, 0);
                                txtqty.Font = new Font(txtqty.Font.FontFamily, 8, FontStyle.Bold);
                                txtqty.Enabled = false;

                                // Configure the textbox enter QTY
                                Button buttonRemove = new Button();
                                buttonRemove.Text = "-";
                                buttonRemove.Name = "btn_remove_" + gridSourceDetails[0].Field15 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                buttonRemove.Tag = gridSourceDetails[0].Field15;
                                buttonRemove.AccessibleDescription = "2";
                                buttonRemove.BackColor = Color.Beige;
                                buttonRemove.Margin = new Padding(0, 14, 0, 0);
                                buttonRemove.Font = new Font(buttonRemove.Font.FontFamily, 8, FontStyle.Bold);
                                buttonRemove.Click += new EventHandler(ButtonRemoveClickEventHandler);

                                // Configure the textbox Price
                                TextBox txtprice = new TextBox();
                                decimal F_price = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value);
                                txtprice.Text = Convert.ToString(F_price);
                                txtprice.Tag = gridSourceDetails[0].Field15;
                                txtprice.BackColor = Color.Beige;
                                txtprice.TextAlign = HorizontalAlignment.Center;
                                txtprice.Margin = new Padding(0, 14, 0, 0);
                                txtprice.Font = new Font(txtprice.Font.FontFamily, 8, FontStyle.Bold);
                                txtprice.Enabled = false;

                                // Configure Item Delete Button
                                PictureBox pb_item_delete = new PictureBox();
                                pb_item_delete.Name = "btn_remove_" + gridSourceDetails[0].Field15 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                pb_item_delete.Tag = gridSourceDetails[0].Field15;
                                pb_item_delete.AccessibleDescription = "2";
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

                                tlp_seletedItem_col.Focus();
                            }
                        }
                    }    
                }
            }
            catch(Exception ex)
            {

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

                string tlpname = "tlp_seletedItem_col" + clickedControl.AccessibleDescription + "_" + Convert.ToInt32(Tabletype.DirectSale);
                TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                if (Booking_Status == false)
                {
                    if (string.IsNullOrEmpty(txtGrandTotal.Text))
                        txtGrandTotal.Text = "0";
                }

                if (tlp_sele != null)
                {
                    int rowno = 0;
                    for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                    {
                        if (Convert.ToInt32(gridSource[0].Field15) == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value)== Convert.ToInt32(Tabletype.DirectSale))
                        {
                            Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                            if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) > Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value))
                            {
                                CMessageBox.Show("Aviablle Quantity is : " + Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value), "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) - 1;
                                txtitemcode_serach.Text = "";
                                txtitemcode_serach.Focus();
                                return;
                            }

                            totalcellamount(i);
                            rowno = i;
                            string txtname = "txtqty_" + clickedControl.AccessibleDescription + "_" + Convert.ToInt32(Tabletype.DirectSale);
                            TextBox qty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                            int cur_qty = Convert.ToInt32(qty.Text) + 1;
                            qty.Text = Convert.ToString(cur_qty);
                            break;
                        }   
                    }

                    decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[ioriginalprice].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                    txtGrandTotal.Text = Convert.ToString(totalamt);
                    netAmount();
                    if (itemcode_serach_Status)
                    {
                        txtitemcode_serach.Text = "";
                        txtitemcode_serach.Focus();
                        itemcode_serach_Status = false;
                    }
                    tlp_sele.Focus();
                }
                else
                {                   

                    #region Grid Inseration
                    r1 = Dgv_Selected_Item.RowCount - 1;
                    Dgv_Selected_Item.Rows[r1].Cells[islno].Value = r1 + 1;
                    Dgv_Selected_Item.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = gridSource[0].Field3;
                    Dgv_Selected_Item.Rows[r1].Cells[ibarcode2].Value = gridSource[0].Field3;
                    Dgv_Selected_Item.Rows[r1].Cells[iname].Value = gridSource[0].Field1;
                    Dgv_Selected_Item.Rows[r1].Cells[iunit].Value = gridSource[0].Field12;
                    Dgv_Selected_Item.Rows[r1].Cells[iunitid].Value = gridSource[0].Field11;
                    Dgv_Selected_Item.Rows[r1].Cells[iqty].Value = "1";
                    Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = gridSource[0].Field4;
                    Dgv_Selected_Item.Rows[r1].Cells[israte].Value = gridSource[0].Field5;
                    Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = gridSource[0].Field6;

                    if (Checkoffer(Convert.ToInt32(gridSource[0].Field2)))
                    {
                        Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = gridSource_offer[0].Field10;
                        Dgv_Selected_Item.Rows[r1].Cells[ioffertype].Value = 0;
                    }
                    else
                    {
                        Dgv_Selected_Item.Rows[r1].Cells[ioffertype].Value = 3;
                        Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = "0";
                    }
                    Dgv_Selected_Item.Rows[r1].Cells[idisamt].Value = "0";
                    Dgv_Selected_Item.Rows[r1].Cells[iitemid].Value = gridSource[0].Field2;
                    Dgv_Selected_Item.Rows[r1].Cells[itax].Value = gridSource[0].Field7;
                    Dgv_Selected_Item.Rows[r1].Cells[itaxtype].Value = gridSource[0].Field8;
                    Dgv_Selected_Item.Rows[r1].Cells[itype].Value = gridSource[0].Field13;
                    Dgv_Selected_Item.Rows[r1].Cells[itaxsettingid].Value = gridSource[0].Field14;
                    Dgv_Selected_Item.Rows[r1].Cells[ipridno].Value = gridSource[0].Field15;
                    Dgv_Selected_Item.Rows[r1].Cells[istatusno].Value = 1;
                    Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = Color.White;
                    Dgv_Selected_Item.Rows[r1].Cells[iofferstatus].Value = 0;
                    Dgv_Selected_Item.Rows[r1].Cells[itabletype].Value = 2;
                    Dgv_Selected_Item.Rows[r1].Cells[ioriginalqty].Value = gridSource[0].Field10;

                    totalcellamount(r1);
                    Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value);


                    decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                    txtGrandTotal.Text = Convert.ToString(totalamt);

                    netAmount();
                    Dgv_Selected_Item.Rows.Add(1);
                    GridSlno_Refresh();

                    #endregion

                    // Configure the TableLayoutPanel
                    TableLayoutPanel tlp_seletedItem_col = new TableLayoutPanel();
                    float[] columnPercentages = { 44.82f, 6.27f, 14.94f, 6.02f, 19.04f, 8.92f };
                    for (int i = 0; i < columnPercentages.Length; i++)
                    {
                        tlp_seletedItem_col.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercentages[i]));
                    }

                    tlp_seletedItem_col.Name = "tlp_seletedItem_col" + Convert.ToInt32(gridSource[0].Field15) + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    tlp_seletedItem_col.Tag = clickedControl.Tag;
                    tlp_seletedItem_col.AccessibleDescription = "2";
                    tlp_seletedItem_col.BackColor = Color.LightBlue;
                    tlp_seletedItem_col.Height = 45;

                    // Configure the Button for Item name
                    Button buttonitemname = new Button();
                    buttonitemname.Text = gridSource[0].Field1;
                    buttonitemname.Name = "buttonitemname_" + gridSource[0].Field15 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    buttonitemname.Tag = gridSource[0].Field15;
                    buttonitemname.AccessibleDescription = "1";
                    buttonitemname.BackColor = Color.Beige;
                    buttonitemname.Height = 40;
                    buttonitemname.Font = new Font(buttonitemname.Font.FontFamily, 8, FontStyle.Bold);
                    buttonitemname.Click += new EventHandler(buttonitemnameClickEventHandler);

                    // Configure the Button for add Qty
                    Button buttonAdd = new Button();
                    buttonAdd.Text = "+";
                    buttonAdd.Name = "buttonAdd_" + gridSource[0].Field15 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    buttonAdd.Tag = gridSource[0].Field15;
                    buttonAdd.AccessibleDescription = "2";
                    buttonAdd.BackColor = Color.Beige;
                    buttonAdd.Margin = new Padding(0, 14, 0, 0);
                    buttonAdd.Font = new Font(buttonAdd.Font.FontFamily, 8, FontStyle.Bold);
                    buttonAdd.Click += new EventHandler(ButtonbuttonAddClickEventHandler);

                    // Configure the textbox enter QTY
                    TextBox txtqty = new TextBox();
                    txtqty.Text = "1";
                    txtqty.Name = "txtqty_" + gridSource[0].Field15 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    txtqty.Tag = gridSource[0].Field15;
                    txtqty.AccessibleDescription = "2";
                    txtqty.BackColor = Color.Beige;
                    txtqty.TextAlign = HorizontalAlignment.Center;
                    txtqty.Margin = new Padding(0, 14, 0, 0);
                    txtqty.Font = new Font(txtqty.Font.FontFamily, 8, FontStyle.Bold);
                    txtqty.Enabled = false;

                    // Configure the textbox enter QTY
                    Button buttonRemove = new Button();
                    buttonRemove.Text = "-";
                    buttonRemove.Name = "btn_remove_" + gridSource[0].Field15 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    buttonRemove.Tag = gridSource[0].Field15;
                    buttonRemove.AccessibleDescription = "2";
                    buttonRemove.BackColor = Color.Beige;
                    buttonRemove.Margin = new Padding(0, 14, 0, 0);
                    buttonRemove.Font = new Font(buttonRemove.Font.FontFamily, 8, FontStyle.Bold);
                    buttonRemove.Click += new EventHandler(ButtonRemoveClickEventHandler);

                    // Configure the textbox Price
                    TextBox txtprice = new TextBox();
                    txtprice.Text = Convert.ToString(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value);
                    txtprice.Name = "txtprice_" + gridSource[0].Field15 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    txtprice.Tag = gridSource[0].Field15;
                    txtprice.AccessibleDescription = "2";
                    txtprice.BackColor = Color.Beige;
                    txtprice.TextAlign = HorizontalAlignment.Center;
                    txtprice.Margin = new Padding(0, 14, 0, 0);
                    txtprice.Font = new Font(txtprice.Font.FontFamily, 8, FontStyle.Bold);
                    txtprice.Enabled = false;

                    // Configure Item Delete Button
                    PictureBox pb_item_delete = new PictureBox();
                    pb_item_delete.Name = "btn_remove_" + gridSource[0].Field15;
                    pb_item_delete.Tag = gridSource[0].Field15;
                    pb_item_delete.AccessibleDescription = "2";
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
        
                    tlp_seletedItem_col.Focus();

                    if (itemcode_serach_Status)
                    {
                        txtitemcode_serach.Text = "";
                        txtitemcode_serach.Focus();
                        itemcode_serach_Status = false;
                    }
                }
                checksave_refresh();

            }
            catch (Exception ex)
            {

            }
        }
        public void totalcellamount_exit_product(int cellproductid)
        {
            try
            {
                if (dgv_exitsting_product.Rows[cellproductid].Cells[iitemid].Value != "" && dgv_exitsting_product.Rows[cellproductid].Cells[iprate].Value != "" && dgv_exitsting_product.Rows[cellproductid].Cells[iqty].Value != "")
                {
                    decimal amount = Convert.ToDecimal(dgv_exitsting_product.Rows[cellproductid].Cells[israte].Value) * Convert.ToDecimal(dgv_exitsting_product.Rows[cellproductid].Cells[iqty].Value);
                    dgv_exitsting_product.Rows[cellproductid].Cells[iamount].Value = amount.ToString("N2");

                    //cellDiscountPercentage(cellproductid);
                    if (Convert.ToInt32(dgv_exitsting_product.Rows[cellproductid].Cells[itabletype].Value) != 3)
                        cellDiscountAmount(cellproductid);

                    decimal discount = Convert.ToDecimal(dgv_exitsting_product.Rows[cellproductid].Cells[idisamt].Value);
                    cellTaxAmount(cellproductid, amount - discount);

                    if (Convert.ToString(dgv_exitsting_product.Rows[cellproductid].Cells[itax].Value) == "Exclusive")
                    {
                        decimal tot = (amount - Convert.ToDecimal(dgv_exitsting_product.Rows[cellproductid].Cells[idisamt].Value))
                        + Convert.ToDecimal(dgv_exitsting_product.Rows[cellproductid].Cells[itaxamt].Value);
                        dgv_exitsting_product.Rows[cellproductid].Cells[itotal].Value = tot.ToString("N2");
                    }
                    if (Convert.ToString(dgv_exitsting_product.Rows[cellproductid].Cells[itax].Value) == "Inclusive")
                    {
                        decimal tot = (amount - Convert.ToDecimal(dgv_exitsting_product.Rows[cellproductid].Cells[idisamt].Value));
                        dgv_exitsting_product.Rows[cellproductid].Cells[itotal].Value = tot.ToString("N2");
                    }
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
                    decimal amount = Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[israte].Value) * Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[iqty].Value);
                    Dgv_Selected_Item.Rows[cellproductid].Cells[iamount].Value = amount.ToString("N2");

                    //cellDiscountPercentage(cellproductid);
                    if(Convert.ToInt32(Dgv_Selected_Item.Rows[cellproductid].Cells[itabletype].Value)!=3)
                    {
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
                    else
                        Dgv_Selected_Item.Rows[cellproductid].Cells[itotal].Value = amount.ToString("N2");

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
                        Dgv_Selected_Item.Rows[cellproductid].Cells[iigstamtno].Value = TaxAmount.ToString("N2");
                        Dgv_Selected_Item.Rows[cellproductid].Cells[itaxper].Value = gridSource[0].IgstPrcnt;
                    }
                    if (gridSource[0].TaxType == "VAT")
                    {
                        decimal TaxAmount = Convert.ToDecimal(Convert.ToDecimal(gridSource[0].VatPrcnt) * amount / 100);
                        Dgv_Selected_Item.Rows[cellproductid].Cells[itaxamt].Value = TaxAmount.ToString("N2");
                        Dgv_Selected_Item.Rows[cellproductid].Cells[ivatamtno].Value = TaxAmount.ToString("N2");
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
        private void pb_item_delete_booking_ClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                for(int i=0;i< Dgv_Selected_Item.RowCount;i++)
                {
                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value)== Convert.ToInt32(clickedControl.Tag) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(clickedControl.AccessibleDescription))
                    {
                        string tlpname = "tlp_seletedItem_col_e_" + clickedControl.Tag + clickedControl.AccessibleDescription;
                        TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                        foreach (Control control in tlp_seletedItem_main.Controls)
                        {
                            if (control.Name == tlpname)
                            {
                                tlp_seletedItem_main.Controls.Remove(control);
                                break;
                            }
                        }
                        int qty = Convert.ToInt32(txttotalQTY.Text) - Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value);
                        txttotalQTY.Text = Convert.ToString(qty);
                        decimal amount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);
                        decimal tamt = Convert.ToDecimal(txtGrandTotal.Text) - amount;
                        txtGrandTotal.Text = Convert.ToString(tamt); 
                        netAmount();
                        Dgv_Selected_Item.Rows[i].Cells[istatusno].Value = 0;
                        break;
                    }
                       
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
                string txtname = "";
                string tlpname = "";
                Control clickedControl = sender as Control;
                if(clickedControl.AccessibleDescription == "1")
                {
                     txtname = "txtqty_" + clickedControl.Tag +"_"+Convert.ToInt32(Tabletype.Product);
                     tlpname = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                }
                else if (clickedControl.AccessibleDescription =="2")
                {
                    txtname = "txtqty_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    tlpname = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                }
                else
                {
                    txtname = "txtqty_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                    tlpname = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                }

                // Find the TableLayoutPanel control by name
                TextBox txtqty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                if (txtqty != null)
                {
                    int cur_qty = 0;
                    TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                    if (tlp_sele != null)
                        if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.DirectSale))
                            CalcTotalAmt_remove_Prid(Convert.ToInt32(clickedControl.Tag), cur_qty);
                    else if (Convert.ToInt32(tlp_sele.AccessibleDescription) == 3)
                            CalcTotalAmt_remove_Combo(Convert.ToInt32(clickedControl.Tag), cur_qty);   
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
                    GridSlno_Refresh();

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
                string txtname = "";
                string tlpname = "";
                Control clickedControl = sender as Control;
                if (clickedControl.AccessibleDescription == "1")
                {
                    txtname = "txtqty_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                    tlpname = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                }
                else if (clickedControl.AccessibleDescription == "2")
                {
                    txtname = "txtqty_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    tlpname = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                }
                else
                {
                    txtname = "txtqty_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                    tlpname = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                }

                // Find the TableLayoutPanel control by name
                TextBox txtqty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                if (txtqty != null)
                {
                    int cur_qty = Convert.ToInt32(txtqty.Text) - 1;
                    if (cur_qty == 0)
                    {
                        if (tlp_sele != null)
                        {
                            if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.DirectSale))
                                CalcTotalAmt_remove_Prid(Convert.ToInt32(clickedControl.Tag), cur_qty);
                            else if (Convert.ToInt32(tlp_sele.AccessibleDescription) == 3)
                                CalcTotalAmt_remove_Combo(Convert.ToInt32(clickedControl.Tag), cur_qty);
                            else
                                CalcTotalAmt_remove(Convert.ToInt32(tlp_sele.Tag), cur_qty);
                        }                      

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
                            if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.DirectSale))
                                CalcTotalAmt_remove_Prid(Convert.ToInt32(clickedControl.Tag), cur_qty);
                            else if (Convert.ToInt32(tlp_sele.AccessibleDescription) == 3)
                                CalcTotalAmt_remove_Combo(Convert.ToInt32(clickedControl.Tag), cur_qty);
                            else
                                CalcTotalAmt_remove(Convert.ToInt32(tlp_sele.Tag), cur_qty);
                    }
                    netAmount();
                    checksave_refresh();
                    GridSlno_Refresh();
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
                string textvalue = clickedControl.AccessibleDescription;
                string[] parts = textvalue.Split(',');
                int saletype = 0,tabletype=0;
                if (parts.Length >= 2)
                {
                    saletype = Convert.ToInt32(parts[0]);
                    tabletype = Convert.ToInt32(parts[1]);
                }
                Proid_Prid = Convert.ToInt32(clickedControl.Tag);
                txt_product_des.Text = "Description Type Here .....";
                txt_product_des.ForeColor = Color.Gray;
                int panelWidthPercentage = 20;
                int panelheightPercentage = 20;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                pnl_products_descr.Size = new Size(panelWidth, panelHeight);

                if (saletype == 1)
                {
                    for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                    {
                        if (Proid_Prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value) && Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value) != null && Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value) != "" && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value)==tabletype)
                        {
                            txt_product_des.Text = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value);
                            txt_product_des.ForeColor = Color.Black;
                            break;
                        }
                    }
                }
                else if (saletype == 3)
                {
                    for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                    {
                        if (Proid_Prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value) != null && Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value) != "" && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == tabletype)
                        {
                        txt_product_des.Text = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value);
                        txt_product_des.ForeColor = Color.Black;
                        break;
                        }

                    }
                }
                else
                {
                    for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                    {
                        if (Proid_Prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value)!=null && Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value) != "")
                        {
                            txt_product_des.Text = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value);
                            txt_product_des.ForeColor = Color.Black;
                            break;
                        }
                    }
                }

                lblheader_product_des.Text = clickedControl.Text;
                Control parentControl = tlp_seletedItem_main;
                pnl_products_descr.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
               // pnl_products_descr.Location = new Point(this.ClientSize.Width / 2 - pnl_products_descr.Size.Width / 2, this.ClientSize.Height / 2 - pnl_products_descr.Size.Height / 2);
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
                string txtname = "";
                string tlpname = "";
                Control clickedControl = sender as Control;
                if (clickedControl.AccessibleDescription == "1")
                {
                    txtname = "txtqty_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                    tlpname = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                }
                else if (clickedControl.AccessibleDescription == "2")
                {
                    txtname = "txtqty_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    tlpname = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                }
                else
                {
                    txtname = "txtqty_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                    tlpname = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                }
                // Find the TableLayoutPanel control by name
                TextBox qty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                if (qty != null)
                { 
                    if (tlp_sele != null)
                    {
                        if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.DirectSale))
                            CalcTotalAmt_Add_prid(Convert.ToInt32(clickedControl.Tag),0);
                        else if (Convert.ToInt32(tlp_sele.AccessibleDescription) == 3)
                        {
                            int cur_qty = Convert.ToInt32(qty.Text) + 1;
                            qty.Text = Convert.ToString(cur_qty);
                            CalcTotalAmt_Add_Combo(Convert.ToInt32(clickedControl.Tag), cur_qty);
                        }

                        else
                        {
                            int cur_qty = Convert.ToInt32(qty.Text) + 1;
                            qty.Text = Convert.ToString(cur_qty);
                            CalcTotalAmt_Add(Convert.ToInt32(tlp_sele.Tag), cur_qty);
                        }

                    }
                    netAmount();
                    GridSlno_Refresh();
                }
            }
            catch (Exception ex)
            {

            }
        }
        #region Quantity calculation _Deletion Combo Product Item
        public void CalcTotalAmt_Add_prid(int prid, int producttype)
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
                    if (Booking_Status == false)
                    {
                        if (string.IsNullOrEmpty(txtGrandTotal.Text))
                            txtGrandTotal.Text = "0";
                    }

                    int rowno = 0;
                    for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                    {
                        if (prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value) &&  Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value)==2)
                        {
                            Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                            if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) > Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value))
                            {
                                CMessageBox.Show("Aviablle Quantity is : " + Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value), "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            
                            string txtname = "txtqty_" + Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value) + "_" + Convert.ToInt32(Tabletype.DirectSale);
                            TextBox qty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                            int cur_qty = Convert.ToInt32(qty.Text) + 1;
                            qty.Text = Convert.ToString(cur_qty);
                            totalcellamount(i);
                            rowno = i;
                            break;
                        }
                    }                    
                    decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[ioriginalprice].Value) + Convert.ToDecimal(txtGrandTotal.Text);
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
                    int rowno = 0;
                    if (Booking_Status == false)
                    {
                        if (string.IsNullOrEmpty(txtGrandTotal.Text))
                            txtGrandTotal.Text = "0";
                    }
;
                    if (qty == 0)
                    {
                        decimal TotalPrice = 0;

                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) == 1 && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.DirectSale))
                            {
                                rowno = i;
                                break;
                            }
                        }
                        string txtqty = "txtqty_" + prid + "_" + Convert.ToInt32(Tabletype.DirectSale);
                        TextBox totalqty = this.Controls.Find(txtqty, true).FirstOrDefault() as TextBox;

                        TotalPrice = Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[ioriginalprice].Value) * Convert.ToDecimal(totalqty.Text);
                        decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) - TotalPrice;
                        txtGrandTotal.Text = Convert.ToString(totalamt);
                        Dgv_Selected_Item.Rows.RemoveAt(rowno);
                    }
                    else
                    {
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value))
                            {
                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) - 1;
                                totalcellamount(i);
                                rowno = i;
                                break;
                            }
                        }
                        decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) - Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[ioriginalprice].Value);
                        txtGrandTotal.Text = Convert.ToString(totalamt);
                    }
                    checksave_refresh();
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void CalcTotalAmt_remove_Combo(int combid, int qty)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "ofm.omid id , ofm.OfferName Field1 ,ofm.Discountper Field2, ofm.tax Field3 ,ofm.taxtype Field4,ofm.TaxSettingsId Field5,ofm.FinalPrice Field6,ofd.Productname Field7";
                dmnSearch.Table = "tblOfferMaster ofm LEFT JOIN tblOfferDetails ofd on ofd.Omid=ofm.Omid";
                dmnSearch.WhereNdGroupBy = "where ofm.Status=1 and ofd.Omid = " + combid;
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    if (Booking_Status == false)
                    {
                        if (string.IsNullOrEmpty(txtGrandTotal.Text))
                            txtGrandTotal.Text = "0";
                    }
                    if (qty == 0)
                    {
                        string txtqty = "txtqty_" + combid + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                        TextBox totalqty = this.Controls.Find(txtqty, true).FirstOrDefault() as TextBox;
                        int n = Dgv_Selected_Item.RowCount - 1;
                        for (int i = 0; i < n; i++)
                        {
                            if (combid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) == 1 && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == 3)
                            {
                                decimal TotalPrice = Convert.ToDecimal(Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value)) * Convert.ToDecimal(totalqty.Text);
                                decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) - TotalPrice;
                                txtGrandTotal.Text = Convert.ToString(totalamt);
                                txtnetamount.Text = Convert.ToString(totalamt);
                                Dgv_Selected_Item.Rows.RemoveAt(i);
                                break;
                            }

                        }
                        for (int i = n - 1; i >= 0; i--)
                        {
                            if (combid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[isubofferid].Value))
                                Dgv_Selected_Item.Rows.RemoveAt(i);
                        }

                    }
                    else
                    {
                        decimal totalamt = 0;
                        int rowno = 0;
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (combid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) == 1)
                            {
                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) - 1;
                                totalcellamount(i);
                                rowno = i;
                                break;
                            }
                        }
                        totalamt = Convert.ToDecimal(txtGrandTotal.Text) - Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[ioriginalprice].Value);
                        txtGrandTotal.Text = Convert.ToString(totalamt);
                    }
                    checksave_refresh();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void CalcTotalAmt_Add_Combo(int combid, int qty)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "ofm.omid id , ofm.OfferName Field1 ,ofm.Discountper Field2, ofm.tax Field3 ,ofm.taxtype Field4,ofm.TaxSettingsId Field5,ofm.FinalPrice Field6,ofd.Productname Field7";
                dmnSearch.Table = "tblOfferMaster ofm LEFT JOIN tblOfferDetails ofd on ofd.Omid=ofm.Omid";
                dmnSearch.WhereNdGroupBy = "where ofm.Status=1 and ofd.Omid = " + combid;
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    int rowno = 0;
                    if (Booking_Status == false)
                    {
                        if (string.IsNullOrEmpty(txtGrandTotal.Text))
                            txtGrandTotal.Text = "0";
                    }
                    for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                    {
                        if (combid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == 3)
                        {
                            Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                            totalcellamount(i);
                            rowno = i;
                            break;
                        }
                    }                    
                    decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[ioriginalprice].Value) + Convert.ToDecimal(txtGrandTotal.Text);
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
                    int rowno = 0;
                    if (Booking_Status == false)
                    {
                        if (string.IsNullOrEmpty(txtGrandTotal.Text))
                            txtGrandTotal.Text = "0";
                    }
                    for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                    {
                        if (pid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == 1) 
                        {
                            Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                            totalcellamount(i);
                            rowno = i;
                            break;
                        }
                    }
                    decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[ioriginalprice].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                    txtGrandTotal.Text = Convert.ToString(totalamt);
                    txtnetamount.Text = Convert.ToString(totalamt);
                    netAmount();
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
                    if (Booking_Status == false)
                    {
                        if (string.IsNullOrEmpty(txtGrandTotal.Text))
                            txtGrandTotal.Text = "0";
                    }

                    if (qty == 0)
                    {
                        string txtqty = "txtqty_" + pid + "_" + Convert.ToInt32(Tabletype.Product);
                        TextBox totalqty = this.Controls.Find(txtqty, true).FirstOrDefault() as TextBox;

                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (pid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) == 1 && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.Product))
                            {
                                //if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ioffertype].Value) == 0)
                                //{                                 
                                    decimal TotalPrice = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value) * Convert.ToDecimal(totalqty.Text);
                                    decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) - TotalPrice;
                                    txtGrandTotal.Text = Convert.ToString(totalamt);
                                    txtnetamount.Text = Convert.ToString(totalamt);
                                    Dgv_Selected_Item.Rows.RemoveAt(i);
                                    Dgv_Selected_Item.Update();
                                    int l = Dgv_Selected_Item.RowCount;
                                    break;
                                //}
                                //else
                                //{
                                //    decimal disAmnt = (Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[idisper].Value) * Convert.ToDecimal(gridSource[0].Price)) / 100;
                                //    decimal F_price = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value) - disAmnt;
                                //    decimal TotalPrice = F_price * Convert.ToDecimal(totalqty.Text);
                                //    decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) - TotalPrice;
                                //    txtGrandTotal.Text = Convert.ToString(totalamt);
                                //    txtnetamount.Text = Convert.ToString(totalamt);
                                //    Dgv_Selected_Item.Rows.RemoveAt(i);
                                //    break;
                                //}
                            }
                        }

                    }
                    else
                    {
                        decimal totalamt = 0;
                        int rowno = 0;
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (pid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) == 1)
                            {
                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) - 1;
                                //Dgv_Selected_Item.Rows[i].Cells[istatusno].Value = 0;
                                totalcellamount(i);
                                rowno = i;
                                break;
                            }
                        }
                        //if (Convert.ToInt32( Dgv_Selected_Item.Rows[rowno].Cells[ioffertype].Value) == 0)
                        //{
                            totalamt = Convert.ToDecimal(txtGrandTotal.Text) - Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[ioriginalprice].Value);
                            txtGrandTotal.Text = Convert.ToString(totalamt);
                        //}
                        //else
                        //{
                        //    decimal disAmnt = (Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[idisper].Value) * Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[ioriginalprice].Value)) / 100;
                        //    decimal F_price = Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[ioriginalprice].Value) - disAmnt;
                        //    totalamt = Convert.ToDecimal(txtGrandTotal.Text) - F_price;
                        //    txtGrandTotal.Text = Convert.ToString(totalamt);
                        //}

                    }
                    checksave_refresh();
                }
            }
            catch (Exception ex)
            {

            }

        }
        #endregion

        private void SidebuttonComboOfferClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                List<DmnGridFields> gridSource = new List<DmnGridFields>();

                pnl_serachproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                tlp_searchproduct_dashboard.AutoScroll = true;
                int rowIndex = 1;
                int columnIndex = 0;
                dmnSearch.Select = "OfferName as Field1,Omid as Field2,3 as Field3,Discountper Field4";
                dmnSearch.Table = "tblOfferMaster ";
                dmnSearch.WhereNdGroupBy = " where Status=1 AND OfferType=1 AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME()";
                dmnSearch.OrderBy = "";
                gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        if (columnIndex == 2)
                        {
                            columnIndex = 0;
                            rowIndex++;
                        }

                        // Main Panel for each product
                        Panel panelProductBox = new Panel();
                        panelProductBox.Name = "panel_product_box" + gridSource[i].Field1;
                        panelProductBox.Dock = DockStyle.None;
                        panelProductBox.Width = 190;
                        panelProductBox.Height = 73;
                        panelProductBox.BackColor = Color.Transparent;//product_buttoncolor;
                        panelProductBox.Margin = new Padding(10, 10, 0, 0);

                        ToolTip tooltip = new ToolTip();

                        // Create a TableLayoutPanel
                        TableLayoutPanel tlp_box_combo = new TableLayoutPanel();
                        tlp_box_combo.Name = "tlp_box_combo" + gridSource[i].Field1;
                        tlp_box_combo.Dock = DockStyle.None;
                        tlp_box_combo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85));
                        tlp_box_combo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
                        tlp_box_combo.Location = new Point(0, panelProductBox.Top + 20);
                        tlp_box_combo.Width = 190;
                        tlp_box_combo.Height = 50;


                        // Configure the Button
                        Button button = new Button();
                        button.Text = gridSource[i].Field1;
                        button.Name = "btn" + gridSource[i].Field1;
                        button.Tag = gridSource[i].Field2;
                        button.Padding = new Padding(16, 0, 0, 0);
                        //button.Margin = new Padding(0);
                        button.AccessibleDescription = "3";
                        button.FlatAppearance.BorderSize = 0;
                        button.FlatAppearance.BorderColor = button.BackColor;
                        button.Font = new Font("Arial", 9, FontStyle.Bold);
                        button.Width = 158;
                        button.Height = 50;
                        button.FlatStyle = FlatStyle.Flat;
                        button.BackColor = product_buttoncolor;
                        button.ForeColor = Color.White;
                        tooltip.SetToolTip(button, gridSource[i].Field1 + " " + gridSource[i].Field4 + "%");
                        button.Click += new EventHandler(DashboardButtonClickEventHandler);
                        button.MouseEnter += new EventHandler(buttoncomboMousehoverEventHandler);
                        button.MouseLeave += new EventHandler(buttoncomboMouseLeaveEventHandler);
                        // Create a circular Region
                        button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                        tlp_box_combo.Controls.Add(button, 0, 0);

                        // Configure the btnviewmore
                        Button btnviewmore = new Button();
                        btnviewmore.Text = ">";
                        btnviewmore.Name = "btnviewmore"; // Change the btnviewmore name to "viewmore"
                        btnviewmore.Tag = gridSource[i].Field2;
                        btnviewmore.AccessibleDescription = "3";
                        btnviewmore.FlatAppearance.BorderSize = 0;
                        btnviewmore.Margin = new Padding(0);
                        btnviewmore.FlatAppearance.BorderColor = btnviewmore.BackColor;
                        btnviewmore.Font = new Font("Arial", 9, FontStyle.Bold);
                        btnviewmore.Width = 30;
                        btnviewmore.Height = 50;
                        //btnviewmore.Dock = DockStyle.Top;
                        btnviewmore.FlatStyle = FlatStyle.Flat;
                        btnviewmore.BackColor = Color.Gold;
                        btnviewmore.ForeColor = Color.Black;
                        //tooltip.SetToolTip(btnviewmore, gridSource[i].Field1 + " " + gridSource[i].Field4 + "%");
                        btnviewmore.Click += new EventHandler(ButtonviewmoreClickEventHandler);
                        btnviewmore.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnviewmore.Width, btnviewmore.Height, 10, 10));
                        tlp_box_combo.Controls.Add(btnviewmore, 1, 0);

                        panelProductBox.Controls.Add(tlp_box_combo);


                        // Button fav
                        Button buttoncombo = new Button();
                        buttoncombo.Name = "buttoncombo" + gridSource[i].Field1;
                        buttoncombo.Tag = gridSource[i].Field2;
                        buttoncombo.AccessibleDescription = "3";
                        buttoncombo.Width = 50;
                        buttoncombo.Height = 40;
                        buttoncombo.FlatStyle = FlatStyle.Flat;
                        buttoncombo.FlatAppearance.BorderSize = 0;
                        buttoncombo.Text = gridSource[i].Field4;
                        buttoncombo.ForeColor = Color.White;
                        buttoncombo.BackColor= product_buttoncolor;
                        buttoncombo.Location = new Point(panelProductBox.Left+3, panelProductBox.Top);
                        buttoncombo.Font = new Font("Arial", 9, FontStyle.Bold);
                        buttoncombo.BackgroundImage = Properties.Resources.combo_button_percentage;
                        buttoncombo.BackgroundImageLayout = ImageLayout.Stretch;
                        tooltip.SetToolTip(buttoncombo, gridSource[i].Field1 + " "+ gridSource[i].Field4 + "%");
                        buttoncombo.Click += new EventHandler(DashboardButtonClickEventHandler);
                        buttoncombo.MouseEnter += new EventHandler(buttoncomboMousehoverEventHandler);
                        buttoncombo.MouseLeave += new EventHandler(buttoncomboMouseLeaveEventHandler);
                        panelProductBox.Controls.Add(buttoncombo);
                        buttoncombo.BringToFront();
                        tlp_searchproduct_dashboard.Controls.Add(panelProductBox, columnIndex, rowIndex);
                        columnIndex++;
                    }
                    pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                }
                else
                {
                    CMessageBox.Show("No Products On This Category", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ButtonviewmoreClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                if (pnlproductviewmore.Visible)
                    pnlproductviewmore.Visible = false;
                else
                {
                    pnlproductviewmore.Visible = true;
                    int panelWidthPercentage = 35;
                    int panelheightPercentage = 35;
                    int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                    int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                    pnlproductviewmore.Size = new Size(panelWidth, panelHeight);
                    pnlproductviewmore.Location = new Point(Cursor.Position.X, Cursor.Position.Y);


                    Control clickedControl = sender as Control;
                    dmnSearch.Select = "ofd.Productname as Field1,ofd.Quantity as Field2,ofm.OfferName Field3,ofm.OfferEndDatetime EndDate";
                    dmnSearch.Table = "tblOfferMaster ofm   LEFT JOIN tblOfferDetails ofd on ofm.Omid=ofd.Omid";
                    dmnSearch.WhereNdGroupBy = " where ofm.Status=1 AND ofm.Omid=" + clickedControl.Tag;
                    dmnSearch.OrderBy = "";
                    //TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                    var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    if (gridSource.Count > 0)
                    {
                        dgv_viewmore.Rows.Clear();
                        dgv_viewmore.Rows.Add(gridSource.Count);
                        lblviewmore.Text = Convert.ToString(gridSource[0].Field3);
                        lblenddatecombo.Text = "[ Offer End On : " + Convert.ToString(gridSource[0].EndDate) + "]";
                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            dgv_viewmore.Rows[i].Cells[0].Value = i + 1;
                            dgv_viewmore.Rows[i].Cells[1].Value = gridSource[i].Field1;
                            dgv_viewmore.Rows[i].Cells[2].Value = gridSource[i].Field2;
                        }
                    }
                }


            }
            catch (Exception ex)
            {

            }
        }

                #region Combo And Offer mouseenter and mouseleave

                private void buttoncomboMouseLeaveEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                //+ gridSource[i].Field1; "btn" + gridSource[i].Field1;
                string btnname = "buttoncombo" + clickedControl.Text;
                Button btncombo = this.Controls.Find(btnname, true).FirstOrDefault() as Button;
                if (btncombo != null)
                {
                    Color i = clickedControl.BackColor;
                    // Using System.Drawing
                    btncombo.BackColor = ColorTranslator.FromHtml("#4267b2");

                }

            }
            catch (Exception ex)
            {

            }
        }
        private void buttoncomboMousehoverEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                //+ gridSource[i].Field1; "btn" + gridSource[i].Field1;
                string btnname = "buttoncombo" + clickedControl.Text;
                Button btncombo = this.Controls.Find(btnname, true).FirstOrDefault() as Button;
                if (btncombo != null)
                {
                    Color i = clickedControl.BackColor;
                    // Using System.Drawing
                    btncombo.BackColor = ColorTranslator.FromHtml("#4f7bd5"); //4267b2

                }

            }
            catch (Exception ex)
            {

            }
        }

        private void buttonofferMouseLeaveEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                //+ gridSource[i].Field1; "btn" + gridSource[i].Field1;
                string btnname = "btnFav" + clickedControl.Text;
                Button btncombo = this.Controls.Find(btnname, true).FirstOrDefault() as Button;
                if (btncombo != null)               
                    btncombo.BackColor = ColorTranslator.FromHtml("#4267b2");               
            }
            catch (Exception ex)
            {

            }
        }
        private void buttonofferMousehoverEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                string btnname = "btnFav" + clickedControl.Text;
                Button btncombo = this.Controls.Find(btnname, true).FirstOrDefault() as Button;
                if (btncombo != null)
                    btncombo.BackColor = ColorTranslator.FromHtml("#4f7bd5");                

            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        private void SidebuttonOfferClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                List<DmnGridFields> gridSource = new List<DmnGridFields>();

                pnl_serachproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                tlp_searchproduct_dashboard.AutoScroll = true;
                int rowIndex = 1;
                int columnIndex = 0;

                dmnSearch.Select = "Omid as Field1,OfferName as Field2,Discountper Field3";
                dmnSearch.Table = "tblOfferMaster";
                dmnSearch.WhereNdGroupBy = " where Status=1 AND OfferType=0 AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME()";
                dmnSearch.OrderBy = "";
                var gridSource_submenu = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource_submenu.Count > 1)
                {
                    ToolTip tooltipsub = new ToolTip();

                        for (int i = 0; i < gridSource_submenu.Count; i++)
                        {
                            if (columnIndex == 2)
                            {
                                columnIndex = 0;
                                rowIndex++;
                            }
                            // Configure the Button
                            Button button = new Button();
                            button.Text = gridSource_submenu[i].Field2;
                            button.Name = "btnsploff_sub_" + gridSource_submenu[i].Field1;
                            button.Tag = gridSource_submenu[i].Field1;
                            button.FlatAppearance.BorderSize = 0;
                            button.FlatAppearance.BorderColor = button.BackColor;
                            button.Font = new Font("Arial", 9, FontStyle.Bold);
                            //button.Margin = new Padding(10, 30, 0, 0); 
                            button.Width = 170;
                            button.Height = 50;
                            button.FlatStyle = FlatStyle.Flat;
                            button.BackColor = product_buttoncolor;
                            button.ForeColor = Color.White;
                            button.Padding = new Padding(16, 0, 0, 0);
                            tooltipsub.SetToolTip(button, gridSource_submenu[i].Field2 + "  " + gridSource_submenu[i].Field3 + "%");
                            //// Create a circular Region
                            button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                            button.Click += new EventHandler(SpecialOfferSubMenuClickEventHandler);

                            tlp_searchproduct_dashboard.Controls.Add(button, columnIndex, rowIndex);
                            columnIndex++;
                        }
                        pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                    

                }
                else
                {
                    for (int j = 1; j <= 2; j++)
                    {
                        if (j == 1)
                        {
                            dmnSearch.Select = "p.Product as Field1,p.ProductId as Field2,0 as Field3,ofd.Discountper Field4,ofd.Producttype Field5";
                            dmnSearch.Table = "tblProduct p " +
                                "LEFT JOIN tblOfferDetails ofd on ofd.ProductId=p.ProductId and ofd.status=1 " +
                                "LEFT JOIN tblOfferMaster ofm on ofm.Omid=ofd.Omid  and ofm.status=1 " +
                                "LEFT JOIN tblMenu m on m.ProductId=p.ProductId ";
                            dmnSearch.WhereNdGroupBy = " where p.Status=1 AND ofm.OfferType=0 AND ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and m.status=1";
                            dmnSearch.OrderBy = "";
                        }
                        if (j == 2)
                        {
                            dmnSearch.Select = "i.Itemname as Field1,i.ItemId as Field2,1 as Field3,ofd.Discountper Field4,ofd.Producttype Field5";
                            dmnSearch.Table = "tblItem i " +
                                "LEFT JOIN tblOfferDetails ofd on ofd.ProductId=i.ItemId and ofd.status=1" +
                                "LEFT JOIN tblOfferMaster ofm on ofm.Omid=ofd.Omid and ofm.status=1 ";
                            dmnSearch.WhereNdGroupBy = " where i.Status=1 AND ofm.OfferType=0 and i.Saletype=1 AND ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME()";
                            dmnSearch.OrderBy = "";
                        }
                        gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSource.Count > 0)
                        {
                            for (int i = 0; i < gridSource.Count; i++)
                            {
                                if (columnIndex == 2)
                                {
                                    columnIndex = 0;
                                    rowIndex++;
                                }
                                // Main Panel for each product
                                Panel panelProductBox = new Panel();
                                panelProductBox.Name = "panel_product_box" + gridSource[i].Field1;
                                panelProductBox.Dock = DockStyle.None;
                                panelProductBox.Width = 190;
                                panelProductBox.Height = 73;
                                panelProductBox.BackColor = Color.Transparent; //product_buttoncolor;
                                panelProductBox.Margin = new Padding(10, 10, 0, 0);

                                ToolTip tooltip = new ToolTip();

                                // Configure the Button
                                Button button = new Button();
                                button.Text = gridSource[i].Field1;
                                button.Name = "btn" + gridSource[i].Field1;
                                button.Tag = gridSource[i].Field2;

                                if (Convert.ToInt32(gridSource[i].Field3) == 0)
                                    button.AccessibleDescription = "0";
                                else
                                    button.AccessibleDescription = "1";

                                button.FlatAppearance.BorderSize = 0;
                                button.FlatAppearance.BorderColor = button.BackColor;
                                button.Font = new Font("Arial", 9, FontStyle.Bold);
                                //button.Margin = new Padding(10, 30, 0, 0); 
                                button.Location = new Point(panelProductBox.Top + 10, panelProductBox.Left + 20);
                                button.Width = 170;
                                button.Height = 50;
                                button.FlatStyle = FlatStyle.Flat;
                                button.BackColor = product_buttoncolor;
                                button.ForeColor = Color.White;
                                button.Padding = new Padding(16, 0, 0, 0);
                                tooltip.SetToolTip(button, gridSource[i].Field1 + " " + gridSource[i].Field4 + "%");
                                //// Create a circular Region
                                button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                                button.Click += new EventHandler(DashboardButtonClickEventHandler);
                                button.MouseEnter += new EventHandler(buttonofferMousehoverEventHandler);
                                button.MouseLeave += new EventHandler(buttonofferMouseLeaveEventHandler);
                                panelProductBox.Controls.Add(button);


                                // Button offer
                                Button buttonoffer = new Button();
                                buttonoffer.Name = "btnFav" + gridSource[i].Field1;
                                buttonoffer.Tag = gridSource[i].Field2;
                                if (Convert.ToInt32(gridSource[i].Field3) == 0)
                                    buttonoffer.AccessibleDescription = "0";
                                else
                                    buttonoffer.AccessibleDescription = "1";
                                buttonoffer.Width = 50;
                                buttonoffer.Height = 45;
                                buttonoffer.FlatStyle = FlatStyle.Flat;
                                buttonoffer.FlatAppearance.BorderSize = 0;
                                buttonoffer.Text = gridSource[i].Field4;
                                buttonoffer.ForeColor = Color.White;
                                buttonoffer.Font = new Font("Arial", 9, FontStyle.Bold);
                                buttonoffer.BackColor = product_buttoncolor;
                                tooltip.SetToolTip(buttonoffer, gridSource[i].Field1 + " " + gridSource[i].Field4 + "%");
                                buttonoffer.BackgroundImage = Properties.Resources.offer_button_percentage;
                                buttonoffer.BackgroundImageLayout = ImageLayout.Stretch;
                                buttonoffer.Click += new EventHandler(DashboardButtonClickEventHandler);
                                buttonoffer.MouseEnter += new EventHandler(buttonofferMousehoverEventHandler);
                                buttonoffer.MouseLeave += new EventHandler(buttonofferMouseLeaveEventHandler);
                                panelProductBox.Controls.Add(buttonoffer);
                                buttonoffer.BringToFront();

                                tlp_searchproduct_dashboard.Controls.Add(panelProductBox, columnIndex, rowIndex);
                                columnIndex++;

                                #region table with offer
                                //// Main Panel for each product
                                //Panel panelProductBox = new Panel();
                                //panelProductBox.Name = "panel_product_box" + gridSource[i].Field1;
                                //panelProductBox.Dock = DockStyle.None;
                                //panelProductBox.Width = 230;
                                //panelProductBox.Height = 50;
                                /////panelProductBox.BackColor = product_buttoncolor;
                                //panelProductBox.Margin = new Padding(10, 10, 0, 0);

                                //TableLayoutPanel tlp_box = new TableLayoutPanel();
                                //tlp_box.Name = "tlp_box" + gridSource[i].Field1;
                                //tlp_box.Dock = DockStyle.Fill;
                                //// Set the number of columns as needed
                                //tlp_box.ColumnCount = 2;
                                //tlp_box.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                                //tlp_box.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                                ////tlp_box.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;


                                //// Configure the Button
                                //Button button = new Button();
                                //button.Text = gridSource[i].Field1;
                                //button.Name = "btn" + gridSource[i].Field1;
                                //button.Tag = gridSource[i].Field2;

                                //if (Convert.ToInt32(gridSource[i].Field3) == 0)
                                //    button.AccessibleDescription = "0";
                                //else
                                //    button.AccessibleDescription = "1";

                                //button.FlatAppearance.BorderSize = 0; // Set border width to 0
                                //button.FlatAppearance.BorderColor = button.BackColor;
                                //button.Font = new Font("Arial", 9, FontStyle.Bold);
                                ////button.Margin = new Padding(0, 30, 0, 0);
                                //button.Width = 160;
                                //button.Height = 50;
                                //button.FlatStyle = FlatStyle.Flat;
                                //button.BackColor = product_buttoncolor;
                                //button.ForeColor = Color.White;
                                ////// Create a circular Region
                                //button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                                ////button.Dock = DockStyle.Fill;
                                //button.Click += new EventHandler(DashboardButtonClickEventHandler);
                                //tlp_box.Controls.Add(button, 1, 0);


                                //// Button fav
                                //Button buttonFav = new Button();
                                //buttonFav.Name = "btnFav" + gridSource[i].Field1;
                                //buttonFav.Tag = gridSource[i].Field2;

                                //if(Convert.ToInt32(gridSource[i].Field3)==0)
                                //    buttonFav.AccessibleDescription = "0";
                                //else
                                //    buttonFav.AccessibleDescription = "1";

                                //buttonFav.Width = 50;
                                //buttonFav.Height = 45;
                                //buttonFav.FlatStyle = FlatStyle.Flat;
                                //buttonFav.FlatAppearance.BorderSize = 0;
                                //buttonFav.Text = gridSource[i].Field4;
                                //buttonFav.ForeColor = Color.White;
                                //buttonFav.Font = new Font("Arial", 9, FontStyle.Bold);
                                //buttonFav.BackgroundImage = Properties.Resources.offer_button_percentage;
                                //buttonFav.BackgroundImageLayout = ImageLayout.Stretch;
                                //buttonFav.Click += new EventHandler(buttonfavClickEventHandler);
                                //tlp_box.Controls.Add(buttonFav, 0, 0);
                                //buttonFav.BringToFront();

                                //panelProductBox.Controls.Add(tlp_box);

                                //tlp_searchproduct_dashboard.Controls.Add(panelProductBox, columnIndex, rowIndex);
                                //columnIndex++;
                                #endregion


                            }
                            pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                        }

                        //else
                        //{
                        //    CMessageBox.Show("No Products On This Category", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //}

                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void SpecialOfferSubMenuClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                List<DmnGridFields> gridSource = new List<DmnGridFields>();
                pnl_serachproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                tlp_searchproduct_dashboard.AutoScroll = true;
                int rowIndex = 1;
                int columnIndex = 0;
                for (int j = 1; j <= 2; j++)
                {
                    if (j == 1)
                    {
                        dmnSearch.Select = "p.Product as Field1,p.ProductId as Field2,0 as Field3,ofd.Discountper Field4,ofd.ProductType Field5";
                        dmnSearch.Table = "tblProduct p " +
                            "LEFT JOIN tblOfferDetails ofd on ofd.ProductId=p.ProductId " +
                            "LEFT JOIN tblOfferMaster ofm on ofm.Omid=ofd.Omid  ";
                        dmnSearch.WhereNdGroupBy = " where p.Status=1 AND ofm.OfferType=0 AND ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Omid ="+ clickedControl.Tag;
                        dmnSearch.OrderBy = "";
                    }
                    if (j == 2)
                    {
                        dmnSearch.Select = "i.Itemname as Field1,i.ItemId as Field2,1 as Field3,ofd.Discountper Field4,ofd.ProductType Field5";
                        dmnSearch.Table = "tblItem i " +
                            "LEFT JOIN tblOfferDetails ofd on ofd.ProductId=i.ItemId " +
                            "LEFT JOIN tblOfferMaster ofm on ofm.Omid=ofd.Omid  ";
                        dmnSearch.WhereNdGroupBy = " where i.Status=1 AND ofm.OfferType=0 and i.Saletype=1 AND ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Omid =" + clickedControl.Tag;
                        dmnSearch.OrderBy = "";
                    }
                    gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    if (gridSource.Count > 0)
                    {
                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            if (columnIndex == 2)
                            {
                                columnIndex = 0;
                                rowIndex++;
                            }
                            // Main Panel for each product
                            Panel panelProductBox = new Panel();
                            panelProductBox.Name = "panel_product_box" + gridSource[i].Field1;
                            panelProductBox.Dock = DockStyle.None;
                            panelProductBox.Width = 190;
                            panelProductBox.Height = 73;
                            panelProductBox.BackColor = Color.Transparent; //product_buttoncolor;
                            panelProductBox.Margin = new Padding(10, 10, 0, 0);

                            ToolTip tooltip = new ToolTip();

                            // Configure the Button
                            Button button = new Button();
                            button.Text = gridSource[i].Field1;
                            button.Name = "btn" + gridSource[i].Field1;
                            button.Tag = gridSource[i].Field2;

                            if (Convert.ToInt32(gridSource[i].Field3) == 0)
                                button.AccessibleDescription = "0";
                            else
                                button.AccessibleDescription = "1";

                            button.FlatAppearance.BorderSize = 0;
                            button.FlatAppearance.BorderColor = button.BackColor;
                            button.Font = new Font("Arial", 9, FontStyle.Bold);
                            //button.Margin = new Padding(10, 30, 0, 0); 
                            button.Location = new Point(panelProductBox.Top + 10, panelProductBox.Left + 20);
                            button.Width = 170;
                            button.Height = 50;
                            button.FlatStyle = FlatStyle.Flat;
                            button.BackColor = product_buttoncolor;
                            button.ForeColor = Color.White;
                            button.Padding = new Padding(16, 0, 0, 0);
                            tooltip.SetToolTip(button, gridSource[i].Field1 + " " + gridSource[i].Field4 + "%");
                            //// Create a circular Region
                            button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                            button.Click += new EventHandler(DashboardButtonClickEventHandler);
                            button.MouseEnter += new EventHandler(buttonofferMousehoverEventHandler);
                            button.MouseLeave += new EventHandler(buttonofferMouseLeaveEventHandler);
                            panelProductBox.Controls.Add(button);


                            // Button offer
                            Button buttonoffer = new Button();
                            buttonoffer.Name = "btnFav" + gridSource[i].Field1;
                            buttonoffer.Tag = gridSource[i].Field2;
                            if (Convert.ToInt32(gridSource[i].Field3) == 0)
                                buttonoffer.AccessibleDescription = "0";
                            else
                                buttonoffer.AccessibleDescription = "1";
                            buttonoffer.Width = 50;
                            buttonoffer.Height = 45;
                            buttonoffer.FlatStyle = FlatStyle.Flat;
                            buttonoffer.FlatAppearance.BorderSize = 0;
                            buttonoffer.Text = gridSource[i].Field4;
                            buttonoffer.ForeColor = Color.White;
                            buttonoffer.Font = new Font("Arial", 9, FontStyle.Bold);
                            buttonoffer.BackColor = product_buttoncolor;
                            tooltip.SetToolTip(buttonoffer, gridSource[i].Field1 + " " + gridSource[i].Field4 + "%");
                            buttonoffer.BackgroundImage = Properties.Resources.offer_button_percentage;
                            buttonoffer.BackgroundImageLayout = ImageLayout.Stretch;
                            buttonoffer.Click += new EventHandler(DashboardButtonClickEventHandler);
                            buttonoffer.MouseEnter += new EventHandler(buttonofferMousehoverEventHandler);
                            buttonoffer.MouseLeave += new EventHandler(buttonofferMouseLeaveEventHandler);
                            panelProductBox.Controls.Add(buttonoffer);
                            buttonoffer.BringToFront();

                            tlp_searchproduct_dashboard.Controls.Add(panelProductBox, columnIndex, rowIndex);
                            columnIndex++;

                            #region table with offer
                            //// Main Panel for each product
                            //Panel panelProductBox = new Panel();
                            //panelProductBox.Name = "panel_product_box" + gridSource[i].Field1;
                            //panelProductBox.Dock = DockStyle.None;
                            //panelProductBox.Width = 230;
                            //panelProductBox.Height = 50;
                            /////panelProductBox.BackColor = product_buttoncolor;
                            //panelProductBox.Margin = new Padding(10, 10, 0, 0);

                            //TableLayoutPanel tlp_box = new TableLayoutPanel();
                            //tlp_box.Name = "tlp_box" + gridSource[i].Field1;
                            //tlp_box.Dock = DockStyle.Fill;
                            //// Set the number of columns as needed
                            //tlp_box.ColumnCount = 2;
                            //tlp_box.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                            //tlp_box.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                            ////tlp_box.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;


                            //// Configure the Button
                            //Button button = new Button();
                            //button.Text = gridSource[i].Field1;
                            //button.Name = "btn" + gridSource[i].Field1;
                            //button.Tag = gridSource[i].Field2;

                            //if (Convert.ToInt32(gridSource[i].Field3) == 0)
                            //    button.AccessibleDescription = "0";
                            //else
                            //    button.AccessibleDescription = "1";

                            //button.FlatAppearance.BorderSize = 0; // Set border width to 0
                            //button.FlatAppearance.BorderColor = button.BackColor;
                            //button.Font = new Font("Arial", 9, FontStyle.Bold);
                            ////button.Margin = new Padding(0, 30, 0, 0);
                            //button.Width = 160;
                            //button.Height = 50;
                            //button.FlatStyle = FlatStyle.Flat;
                            //button.BackColor = product_buttoncolor;
                            //button.ForeColor = Color.White;
                            ////// Create a circular Region
                            //button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                            ////button.Dock = DockStyle.Fill;
                            //button.Click += new EventHandler(DashboardButtonClickEventHandler);
                            //tlp_box.Controls.Add(button, 1, 0);


                            //// Button fav
                            //Button buttonFav = new Button();
                            //buttonFav.Name = "btnFav" + gridSource[i].Field1;
                            //buttonFav.Tag = gridSource[i].Field2;

                            //if(Convert.ToInt32(gridSource[i].Field3)==0)
                            //    buttonFav.AccessibleDescription = "0";
                            //else
                            //    buttonFav.AccessibleDescription = "1";

                            //buttonFav.Width = 50;
                            //buttonFav.Height = 45;
                            //buttonFav.FlatStyle = FlatStyle.Flat;
                            //buttonFav.FlatAppearance.BorderSize = 0;
                            //buttonFav.Text = gridSource[i].Field4;
                            //buttonFav.ForeColor = Color.White;
                            //buttonFav.Font = new Font("Arial", 9, FontStyle.Bold);
                            //buttonFav.BackgroundImage = Properties.Resources.offer_button_percentage;
                            //buttonFav.BackgroundImageLayout = ImageLayout.Stretch;
                            //buttonFav.Click += new EventHandler(buttonfavClickEventHandler);
                            //tlp_box.Controls.Add(buttonFav, 0, 0);
                            //buttonFav.BringToFront();

                            //panelProductBox.Controls.Add(tlp_box);

                            //tlp_searchproduct_dashboard.Controls.Add(panelProductBox, columnIndex, rowIndex);
                            //columnIndex++;
                            #endregion


                        }
                        pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);

                    }

                    //else
                    //{
                    //    CMessageBox.Show("No Products On This Category", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //}

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
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                List<DmnGridFields> gridSource = new List<DmnGridFields>();

                pnl_serachproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                tlp_searchproduct_dashboard.AutoScroll = true;
                int rowIndex = 1;
                int columnIndex = 0;

                for(int j=1;j<=2;j++)
                {
                    if(j==1)
                    {
                        dmnSearch.Select = "p.Product as Field1,p.ProductId as Field2,0 as Field3,fav_status as Field4";
                        dmnSearch.Table = "tblProduct p LEFT JOIN tblMenu m on m.ProductId=p.ProductId ";
                        dmnSearch.WhereNdGroupBy = " where p.Status=1 and m.Status=1 and fav_status=1";
                        dmnSearch.OrderBy = "";
                         gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    }
                    else if(j==2)
                    {
                        dmnSearch.Select = "Itemname as Field1,ItemId as Field2,Saletype as Field3,fav_status as Field4  ";
                        dmnSearch.Table = "tblItem ";
                        dmnSearch.WhereNdGroupBy = " where Status=1 and fav_status=1 ";
                        dmnSearch.OrderBy = "";
                         gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    }

                    if (gridSource.Count > 0)
                    {

                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            if (columnIndex == 3)
                            {
                                columnIndex = 0;
                                rowIndex++;
                            }

                            // Main Panel for each product
                            Panel panelProductBox = new Panel();
                            panelProductBox.Name = "panel_product_box" + gridSource[i].Field1;
                            panelProductBox.Dock = DockStyle.None;
                            panelProductBox.Width = 150;
                            panelProductBox.Height = 55;
                            panelProductBox.BackColor = product_buttoncolor;
                            panelProductBox.Margin = new Padding(10, 10, 0, 0);

                            // Configure the Button
                            Button button = new Button();
                            button.Text = gridSource[i].Field1;
                            button.Name = "btn" + gridSource[i].Field1;
                            button.Tag = gridSource[i].Field2;
                            button.AccessibleDescription = gridSource[i].Field3;
                            button.FlatAppearance.BorderSize = 0; // Set border width to 0
                            button.FlatAppearance.BorderColor = button.BackColor;
                            // Assuming 'button' is your Button control
                            button.Font = new Font("Arial", 9, FontStyle.Bold);

                            button.Width = 120;
                            button.Height = 50;

                            button.FlatStyle = FlatStyle.Flat;
                            button.BackColor = product_buttoncolor;
                            button.ForeColor = Color.White;

                            //// Create a circular Region
                            button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                            //button.Dock = DockStyle.Fill;
                            button.Click += new EventHandler(DashboardButtonClickEventHandler);
                            panelProductBox.Controls.Add(button);

                                 // PictureBox fav
                            PictureBox pictureBoxFav = new PictureBox();
                            pictureBoxFav.Name = "picBoxFav" + gridSource[i].Field1;
                            pictureBoxFav.Tag = gridSource[i].Field2;
                            pictureBoxFav.AccessibleDescription = gridSource[i].Field3;
                            pictureBoxFav.Width = 20;
                            pictureBoxFav.Height = 15;
                            pictureBoxFav.BorderStyle = BorderStyle.None;
                            pictureBoxFav.Image = null;

                            if (Convert.ToInt32(gridSource[i].Field4) == 1)
                                pictureBoxFav.Image = Properties.Resources.yellow_star;
                            else
                                pictureBoxFav.Image = Properties.Resources.white_star;

                            pictureBoxFav.SizeMode = PictureBoxSizeMode.StretchImage;
                            pictureBoxFav.Click += new EventHandler(buttonfavClickEventHandler);
                            panelProductBox.Controls.Add(pictureBoxFav);
                            pictureBoxFav.BringToFront();

                            tlp_searchproduct_dashboard.Controls.Add(panelProductBox, columnIndex, rowIndex);
                            columnIndex++;
                        }
                        pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                    }
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
                txtname_search.ForeColor = Color.DarkGray;
            }

        }

        private void txtitemcode_serach_Leave(object sender, EventArgs e)
        {
            if (txtitemcode_serach.Text == "")
            {
                txtitemcode_serach.Text = "Item Code .....";
                txtitemcode_serach.ForeColor = Color.DarkGray;
            }
        }

        private void txtname_search_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtname_search.Text != null && txtname_search.Text != "")
                    {
                        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                        DmnSearch dmnSearch = new DmnSearch();
                        pnl_serachproduct_dashboard.Controls.Clear();
                        tlp_searchproduct_dashboard.Controls.Clear();
                        tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                        tlp_searchproduct_dashboard.AutoScroll = true;

                        int rowIndex = 1;
                        int columnIndex = 0;
                        int columnIndex_p = 0;
                        bool flag = false;

                        dmnSearch.Select = "p.Product as Field1,p.ProductId as Field2,0 as Field3 ,ofm.Omid Field4,0 Field5,ofm.Discountper Field6";
                        dmnSearch.Table = " tblProduct p " +
                            " LEFT JOIN tblMenu m on m.ProductId=p.ProductId " +
                            " LEFT JOIN tblOfferDetails ofd on ofd.ProductId=p.ProductId and  ofd.Status=1 " +
                            " LEFT JOIN tblOfferMaster ofm on ofm.Omid=ofd.Omid and ofm.OfferType=0 AND ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME()" +
                            " where p.Status=1 and m.Status=1  and p.Product LIKE'%" + txtname_search.Text + "%'" +

                            " Union Select Itemname as Field1,ItemId as Field2,i.Saletype as Field3,ofm.Omid Field4,0 Field5,ofm.Discountper Field6 from " +
                            " tblItem i " +
                            " LEFT JOIN tblOfferDetails ofd on ofd.ProductId = i.ItemId and ofd.Status = 1 " +
                            " LEFT JOIN tblOfferMaster ofm on ofm.Omid = ofd.Omid and ofm.OfferType = 0 AND ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() " +
                            " where i.Saletype = 1  and i.Status = 1  and i.Itemname LIKE '" + txtname_search.Text + " % '" +

                            " Union select  OfferName as Field1,0 as Field2,2 as Field3,0 Field4,Omid Field5,0 Field6 from tblOfferMaster ";
                        dmnSearch.WhereNdGroupBy = " where Status=1 AND OfferType=1 AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME() and OfferName LIKE '%" + txtname_search.Text + "%'";
                        dmnSearch.OrderBy = "";
                        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSource.Count > 0)
                        {
                            #region LINQ sort Code
                            //var sortedGridSource = gridSource
                            //.OrderBy(item => Convert.ToInt32(item.Field4) > 0 ? 0 : (Convert.ToInt32(item.Field5) > 0 ? 1 : 2))
                            //.ThenBy(item => item.Field1);

                            //foreach (var item in sortedGridSource)
                            //{
                            //    if (Convert.ToInt32(item.Field4) > 0)
                            //    {
                            //        #region with offer product and item

                            //        if (columnIndex == 2)
                            //        {
                            //            columnIndex = 0;
                            //            rowIndex++;
                            //        }
                            //        // Main Panel for each product
                            //        Panel panelProductBox = new Panel();
                            //        panelProductBox.Name = "panel_product_box" + item.Field1;
                            //        panelProductBox.Dock = DockStyle.None;
                            //        panelProductBox.Width = 190;
                            //        panelProductBox.Height = 73;
                            //        panelProductBox.BackColor = Color.Transparent; //product_buttoncolor;
                            //        panelProductBox.Margin = new Padding(10, 10, 0, 0);

                            //        ToolTip tooltip = new ToolTip();

                            //        // Configure the Button
                            //        Button button = new Button();
                            //        button.Text = item.Field1;
                            //        button.Name = "btn" + item.Field1;
                            //        button.Tag = item.Field2;

                            //        if (Convert.ToInt32(item.Field3) == 0)
                            //            button.AccessibleDescription = "0";
                            //        else
                            //            button.AccessibleDescription = "1";

                            //        button.FlatAppearance.BorderSize = 0;
                            //        button.FlatAppearance.BorderColor = button.BackColor;
                            //        button.Font = new Font("Arial", 9, FontStyle.Bold);
                            //        //button.Margin = new Padding(10, 30, 0, 0); 
                            //        button.Location = new Point(panelProductBox.Top + 10, panelProductBox.Left + 20);
                            //        button.Width = 170;
                            //        button.Height = 50;
                            //        button.FlatStyle = FlatStyle.Flat;
                            //        button.BackColor = product_buttoncolor;
                            //        button.ForeColor = Color.White;
                            //        button.Padding = new Padding(16, 0, 0, 0);
                            //        tooltip.SetToolTip(button, item.Field1 + " " + item.Field4 + "%");
                            //        //// Create a circular Region
                            //        button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                            //        button.Click += new EventHandler(DashboardButtonClickEventHandler);
                            //        button.MouseEnter += new EventHandler(buttonofferMousehoverEventHandler);
                            //        button.MouseLeave += new EventHandler(buttonofferMouseLeaveEventHandler);
                            //        panelProductBox.Controls.Add(button);


                            //        // Button offer
                            //        Button buttonoffer = new Button();
                            //        buttonoffer.Name = "btnFav" + item.Field1;
                            //        buttonoffer.Tag = item.Field2;
                            //        if (Convert.ToInt32(item.Field3) == 0)
                            //            buttonoffer.AccessibleDescription = "0";
                            //        else
                            //            buttonoffer.AccessibleDescription = "1";
                            //        buttonoffer.Width = 50;
                            //        buttonoffer.Height = 45;
                            //        buttonoffer.FlatStyle = FlatStyle.Flat;
                            //        buttonoffer.FlatAppearance.BorderSize = 0;
                            //        buttonoffer.Text = item.Field6;
                            //        buttonoffer.ForeColor = Color.White;
                            //        buttonoffer.Font = new Font("Arial", 9, FontStyle.Bold);
                            //        buttonoffer.BackColor = product_buttoncolor;
                            //        tooltip.SetToolTip(buttonoffer, item.Field1 + " " + item.Field6 + "%");
                            //        buttonoffer.BackgroundImage = Properties.Resources.offer_button_percentage;
                            //        buttonoffer.BackgroundImageLayout = ImageLayout.Stretch;
                            //        buttonoffer.Click += new EventHandler(DashboardButtonClickEventHandler);
                            //        buttonoffer.MouseEnter += new EventHandler(buttonofferMousehoverEventHandler);
                            //        buttonoffer.MouseLeave += new EventHandler(buttonofferMouseLeaveEventHandler);
                            //        panelProductBox.Controls.Add(buttonoffer);
                            //        buttonoffer.BringToFront();

                            //        tlp_searchproduct_dashboard.Controls.Add(panelProductBox, columnIndex, rowIndex);
                            //        columnIndex++;
                            //        #endregion
                            //    }
                            //    else if (Convert.ToInt32(item.Field5) > 0)
                            //    {
                            //        #region Combo product

                            //        if (columnIndex == 2)
                            //        {
                            //            columnIndex = 0;
                            //            rowIndex++;
                            //        }

                            //        // Main Panel for each product
                            //        Panel panelProductBox = new Panel();
                            //        panelProductBox.Name = "panel_product_box" + item.Field1;
                            //        panelProductBox.Dock = DockStyle.None;
                            //        panelProductBox.Width = 190;
                            //        panelProductBox.Height = 73;
                            //        panelProductBox.BackColor = Color.Transparent;//product_buttoncolor;
                            //        panelProductBox.Margin = new Padding(10, 10, 0, 0);

                            //        ToolTip tooltip = new ToolTip();

                            //        // Create a TableLayoutPanel
                            //        TableLayoutPanel tlp_box_combo = new TableLayoutPanel();
                            //        tlp_box_combo.Name = "tlp_box_combo" + item.Field1;
                            //        tlp_box_combo.Dock = DockStyle.None;
                            //        tlp_box_combo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85));
                            //        tlp_box_combo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
                            //        tlp_box_combo.Location = new Point(0, panelProductBox.Top + 20);
                            //        tlp_box_combo.Width = 190;
                            //        tlp_box_combo.Height = 50;


                            //        // Configure the Button
                            //        Button button = new Button();
                            //        button.Text = item.Field1;
                            //        button.Name = "btn" + item.Field1;
                            //        button.Tag = item.Field2;
                            //        button.Padding = new Padding(16, 0, 0, 0);
                            //        //button.Margin = new Padding(0);
                            //        button.AccessibleDescription = "3";
                            //        button.FlatAppearance.BorderSize = 0;
                            //        button.FlatAppearance.BorderColor = button.BackColor;
                            //        button.Font = new Font("Arial", 9, FontStyle.Bold);
                            //        button.Width = 158;
                            //        button.Height = 50;
                            //        button.FlatStyle = FlatStyle.Flat;
                            //        button.BackColor = product_buttoncolor;
                            //        button.ForeColor = Color.White;
                            //        tooltip.SetToolTip(button, item.Field1 + " " + item.Field4 + "%");
                            //        button.Click += new EventHandler(DashboardButtonClickEventHandler);
                            //        button.MouseEnter += new EventHandler(buttoncomboMousehoverEventHandler);
                            //        button.MouseLeave += new EventHandler(buttoncomboMouseLeaveEventHandler);
                            //        // Create a circular Region
                            //        button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                            //        tlp_box_combo.Controls.Add(button, 0, 0);

                            //        // Configure the btnviewmore
                            //        Button btnviewmore = new Button();
                            //        btnviewmore.Text = ">";
                            //        btnviewmore.Name = "btnviewmore_" + item.Field5; // Change the btnviewmore name to "viewmore"
                            //        btnviewmore.Tag = item.Field5;
                            //        btnviewmore.AccessibleDescription = "3";
                            //        btnviewmore.FlatAppearance.BorderSize = 0;
                            //        btnviewmore.Margin = new Padding(0);
                            //        btnviewmore.FlatAppearance.BorderColor = btnviewmore.BackColor;
                            //        btnviewmore.Font = new Font("Arial", 9, FontStyle.Bold);
                            //        btnviewmore.Width = 30;
                            //        btnviewmore.Height = 50;
                            //        //btnviewmore.Dock = DockStyle.Top;
                            //        btnviewmore.FlatStyle = FlatStyle.Flat;
                            //        btnviewmore.BackColor = Color.Gold;
                            //        btnviewmore.ForeColor = Color.Black;
                            //        //tooltip.SetToolTip(btnviewmore, item.Field1 + " " + item.Field4 + "%");
                            //        btnviewmore.Click += new EventHandler(ButtonviewmoreClickEventHandler);
                            //        btnviewmore.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnviewmore.Width, btnviewmore.Height, 10, 10));
                            //        tlp_box_combo.Controls.Add(btnviewmore, 1, 0);

                            //        panelProductBox.Controls.Add(tlp_box_combo);

                            //        // Button fav
                            //        Button buttoncombo = new Button();
                            //        buttoncombo.Name = "buttoncombo" + item.Field1;
                            //        buttoncombo.Tag = item.Field2;
                            //        buttoncombo.AccessibleDescription = "3";
                            //        buttoncombo.Width = 50;
                            //        buttoncombo.Height = 40;
                            //        buttoncombo.FlatStyle = FlatStyle.Flat;
                            //        buttoncombo.FlatAppearance.BorderSize = 0;
                            //        buttoncombo.Text = item.Field4;
                            //        buttoncombo.ForeColor = Color.White;
                            //        buttoncombo.BackColor = product_buttoncolor;
                            //        buttoncombo.Location = new Point(panelProductBox.Left + 3, panelProductBox.Top);
                            //        buttoncombo.Font = new Font("Arial", 9, FontStyle.Bold);
                            //        buttoncombo.BackgroundImage = Properties.Resources.combo_button_percentage;
                            //        buttoncombo.BackgroundImageLayout = ImageLayout.Stretch;
                            //        tooltip.SetToolTip(buttoncombo, item.Field1 + " " + item.Field4 + "%");
                            //        buttoncombo.Click += new EventHandler(DashboardButtonClickEventHandler);
                            //        buttoncombo.MouseEnter += new EventHandler(buttoncomboMousehoverEventHandler);
                            //        buttoncombo.MouseLeave += new EventHandler(buttoncomboMouseLeaveEventHandler);
                            //        panelProductBox.Controls.Add(buttoncombo);
                            //        buttoncombo.BringToFront();
                            //        tlp_searchproduct_dashboard.Controls.Add(panelProductBox, columnIndex, rowIndex);
                            //        columnIndex++;

                            //        #endregion
                            //    }
                            //    else
                            //    {
                            //        #region without offer product and item

                            //        if(flag==false)
                            //        {
                            //            rowIndex++;
                            //            flag = true;
                            //        }

                            //        if (columnIndex_p == 3)
                            //        {
                            //            columnIndex_p = 0;
                            //            rowIndex++;
                            //        }

                            //        // Main Panel for each product
                            //        Panel panelProductBox = new Panel();
                            //        panelProductBox.Name = "panel_product_box" + item.Field1;
                            //        panelProductBox.Dock = DockStyle.None;
                            //        panelProductBox.Width = 150;
                            //        panelProductBox.Height = 55;
                            //        panelProductBox.BackColor = product_buttoncolor;
                            //        panelProductBox.Margin = new Padding(3, 10, 0, 0);

                            //        // Configure the Button
                            //        Button button = new Button();
                            //        button.Text = item.Field1;
                            //        button.Name = "btn" + item.Field1;
                            //        button.Tag = item.Field2;
                            //        button.AccessibleDescription = item.Field3;
                            //        button.FlatAppearance.BorderSize = 0; // Set border width to 0
                            //        button.FlatAppearance.BorderColor = button.BackColor;
                            //        button.ForeColor = Color.White;
                            //        button.Width = 120;
                            //        button.Height = 50;
                            //        button.Font = new Font("Arial", 9, FontStyle.Bold);
                            //        button.FlatStyle = FlatStyle.Flat;
                            //        button.BackColor = product_buttoncolor;
                            //        //// Create a circular Region
                            //        button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                            //        //button.Dock = DockStyle.Fill;
                            //        button.Click += new EventHandler(DashboardButtonClickEventHandler);
                            //        panelProductBox.Controls.Add(button);

                            //        // PictureBox fav
                            //        PictureBox pictureBoxFav = new PictureBox();
                            //        pictureBoxFav.Name = "picBoxFav" + item.Field1;
                            //        pictureBoxFav.Tag = item.Field2;
                            //        pictureBoxFav.AccessibleDescription = item.Field3;
                            //        pictureBoxFav.Width = 20;
                            //        pictureBoxFav.Height = 15;
                            //        pictureBoxFav.BorderStyle = BorderStyle.None;
                            //        pictureBoxFav.Click += new EventHandler(buttonfavClickEventHandler);
                            //        pictureBoxFav.Image = null;
                            //        if (Convert.ToInt32(item.Field4) == 1)
                            //            pictureBoxFav.Image = Properties.Resources.yellow_star;
                            //        else
                            //            pictureBoxFav.Image = Properties.Resources.white_star;

                            //        pictureBoxFav.SizeMode = PictureBoxSizeMode.StretchImage;
                            //        panelProductBox.Controls.Add(pictureBoxFav);
                            //        pictureBoxFav.BringToFront();

                            //        tlp_searchproduct_dashboard.Controls.Add(panelProductBox, columnIndex_p, rowIndex);
                            //        columnIndex_p++;

                            //        #endregion
                            //    }
                            //}

                            #endregion

                            for (int i = 0; i < gridSource.Count; i++)
                            {

                                #region with offer product and item
                                if (Convert.ToInt32(gridSource[i].Field4) > 0)
                                {
                                    if (columnIndex == 2)
                                    {
                                        columnIndex = 0;
                                        rowIndex++;
                                    }
                                    // Main Panel for each product
                                    Panel panelProductBox = new Panel();
                                    panelProductBox.Name = "panel_product_box" + gridSource[i].Field1;
                                    panelProductBox.Dock = DockStyle.None;
                                    panelProductBox.Width = 190;
                                    panelProductBox.Height = 73;
                                    panelProductBox.BackColor = Color.Transparent; //product_buttoncolor;
                                    panelProductBox.Margin = new Padding(10, 10, 0, 0);

                                    ToolTip tooltip = new ToolTip();

                                    // Configure the Button
                                    Button button = new Button();
                                    button.Text = gridSource[i].Field1;
                                    button.Name = "btn" + gridSource[i].Field1;
                                    button.Tag = gridSource[i].Field2;

                                    if (Convert.ToInt32(gridSource[i].Field3) == 0)
                                        button.AccessibleDescription = "0";
                                    else
                                        button.AccessibleDescription = "1";

                                    button.FlatAppearance.BorderSize = 0;
                                    button.FlatAppearance.BorderColor = button.BackColor;
                                    button.Font = new Font("Arial", 9, FontStyle.Bold);
                                    //button.Margin = new Padding(10, 30, 0, 0); 
                                    button.Location = new Point(panelProductBox.Top + 10, panelProductBox.Left + 20);
                                    button.Width = 170;
                                    button.Height = 50;
                                    button.FlatStyle = FlatStyle.Flat;
                                    button.BackColor = product_buttoncolor;
                                    button.ForeColor = Color.White;
                                    button.Padding = new Padding(16, 0, 0, 0);
                                    tooltip.SetToolTip(button, gridSource[i].Field1 + " " + gridSource[i].Field4 + "%");
                                    //// Create a circular Region
                                    button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                                    button.Click += new EventHandler(DashboardButtonClickEventHandler);
                                    button.MouseEnter += new EventHandler(buttonofferMousehoverEventHandler);
                                    button.MouseLeave += new EventHandler(buttonofferMouseLeaveEventHandler);
                                    panelProductBox.Controls.Add(button);


                                    // Button offer
                                    Button buttonoffer = new Button();
                                    buttonoffer.Name = "btnFav" + gridSource[i].Field1;
                                    buttonoffer.Tag = gridSource[i].Field2;
                                    if (Convert.ToInt32(gridSource[i].Field3) == 0)
                                        buttonoffer.AccessibleDescription = "0";
                                    else
                                        buttonoffer.AccessibleDescription = "1";
                                    buttonoffer.Width = 50;
                                    buttonoffer.Height = 45;
                                    buttonoffer.FlatStyle = FlatStyle.Flat;
                                    buttonoffer.FlatAppearance.BorderSize = 0;
                                    buttonoffer.Text = gridSource[i].Field6;
                                    buttonoffer.ForeColor = Color.White;
                                    buttonoffer.Font = new Font("Arial", 9, FontStyle.Bold);
                                    buttonoffer.BackColor = product_buttoncolor;
                                    tooltip.SetToolTip(buttonoffer, gridSource[i].Field1 + " " + gridSource[i].Field6 + "%");
                                    buttonoffer.BackgroundImage = Properties.Resources.offer_button_percentage;
                                    buttonoffer.BackgroundImageLayout = ImageLayout.Stretch;
                                    buttonoffer.Click += new EventHandler(DashboardButtonClickEventHandler);
                                    buttonoffer.MouseEnter += new EventHandler(buttonofferMousehoverEventHandler);
                                    buttonoffer.MouseLeave += new EventHandler(buttonofferMouseLeaveEventHandler);
                                    panelProductBox.Controls.Add(buttonoffer);
                                    buttonoffer.BringToFront();

                                    tlp_searchproduct_dashboard.Controls.Add(panelProductBox, columnIndex, rowIndex);
                                    columnIndex++;
                                }
                                #endregion

                                #region Combo product
                                else if (Convert.ToInt32(gridSource[i].Field5) > 0)
                                {
                                    if (columnIndex == 2)
                                    {
                                        columnIndex = 0;
                                        rowIndex++;
                                    }

                                    // Main Panel for each product
                                    Panel panelProductBox = new Panel();
                                    panelProductBox.Name = "panel_product_box" + gridSource[i].Field1;
                                    panelProductBox.Dock = DockStyle.None;
                                    panelProductBox.Width = 190;
                                    panelProductBox.Height = 73;
                                    panelProductBox.BackColor = Color.Transparent;//product_buttoncolor;
                                    panelProductBox.Margin = new Padding(10, 10, 0, 0);

                                    ToolTip tooltip = new ToolTip();

                                    // Create a TableLayoutPanel
                                    TableLayoutPanel tlp_box_combo = new TableLayoutPanel();
                                    tlp_box_combo.Name = "tlp_box_combo" + gridSource[i].Field1;
                                    tlp_box_combo.Dock = DockStyle.None;
                                    tlp_box_combo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 85));
                                    tlp_box_combo.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15));
                                    tlp_box_combo.Location = new Point(0, panelProductBox.Top + 20);
                                    tlp_box_combo.Width = 190;
                                    tlp_box_combo.Height = 50;


                                    // Configure the Button
                                    Button button = new Button();
                                    button.Text = gridSource[i].Field1;
                                    button.Name = "btn" + gridSource[i].Field1;
                                    button.Tag = gridSource[i].Field2;
                                    button.Padding = new Padding(16, 0, 0, 0);
                                    //button.Margin = new Padding(0);
                                    button.AccessibleDescription = "3";
                                    button.FlatAppearance.BorderSize = 0;
                                    button.FlatAppearance.BorderColor = button.BackColor;
                                    button.Font = new Font("Arial", 9, FontStyle.Bold);
                                    button.Width = 158;
                                    button.Height = 50;
                                    button.FlatStyle = FlatStyle.Flat;
                                    button.BackColor = product_buttoncolor;
                                    button.ForeColor = Color.White;
                                    tooltip.SetToolTip(button, gridSource[i].Field1 + " " + gridSource[i].Field4 + "%");
                                    button.Click += new EventHandler(DashboardButtonClickEventHandler);
                                    button.MouseEnter += new EventHandler(buttoncomboMousehoverEventHandler);
                                    button.MouseLeave += new EventHandler(buttoncomboMouseLeaveEventHandler);
                                    // Create a circular Region
                                    button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                                    tlp_box_combo.Controls.Add(button, 0, 0);

                                    // Configure the btnviewmore
                                    Button btnviewmore = new Button();
                                    btnviewmore.Text = ">";
                                    btnviewmore.Name = "btnviewmore_" + gridSource[i].Field5; // Change the btnviewmore name to "viewmore"
                                    btnviewmore.Tag = gridSource[i].Field5;
                                    btnviewmore.AccessibleDescription = "3";
                                    btnviewmore.FlatAppearance.BorderSize = 0;
                                    btnviewmore.Margin = new Padding(0);
                                    btnviewmore.FlatAppearance.BorderColor = btnviewmore.BackColor;
                                    btnviewmore.Font = new Font("Arial", 9, FontStyle.Bold);
                                    btnviewmore.Width = 30;
                                    btnviewmore.Height = 50;
                                    //btnviewmore.Dock = DockStyle.Top;
                                    btnviewmore.FlatStyle = FlatStyle.Flat;
                                    btnviewmore.BackColor = Color.Gold;
                                    btnviewmore.ForeColor = Color.Black;
                                    //tooltip.SetToolTip(btnviewmore, gridSource[i].Field1 + " " + gridSource[i].Field4 + "%");
                                    btnviewmore.Click += new EventHandler(ButtonviewmoreClickEventHandler);
                                    btnviewmore.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, btnviewmore.Width, btnviewmore.Height, 10, 10));
                                    tlp_box_combo.Controls.Add(btnviewmore, 1, 0);

                                    panelProductBox.Controls.Add(tlp_box_combo);


                                    // Button fav
                                    Button buttoncombo = new Button();
                                    buttoncombo.Name = "buttoncombo" + gridSource[i].Field1;
                                    buttoncombo.Tag = gridSource[i].Field2;
                                    buttoncombo.AccessibleDescription = "3";
                                    buttoncombo.Width = 50;
                                    buttoncombo.Height = 40;
                                    buttoncombo.FlatStyle = FlatStyle.Flat;
                                    buttoncombo.FlatAppearance.BorderSize = 0;
                                    buttoncombo.Text = gridSource[i].Field4;
                                    buttoncombo.ForeColor = Color.White;
                                    buttoncombo.BackColor = product_buttoncolor;
                                    buttoncombo.Location = new Point(panelProductBox.Left + 3, panelProductBox.Top);
                                    buttoncombo.Font = new Font("Arial", 9, FontStyle.Bold);
                                    buttoncombo.BackgroundImage = Properties.Resources.combo_button_percentage;
                                    buttoncombo.BackgroundImageLayout = ImageLayout.Stretch;
                                    tooltip.SetToolTip(buttoncombo, gridSource[i].Field1 + " " + gridSource[i].Field4 + "%");
                                    buttoncombo.Click += new EventHandler(DashboardButtonClickEventHandler);
                                    buttoncombo.MouseEnter += new EventHandler(buttoncomboMousehoverEventHandler);
                                    buttoncombo.MouseLeave += new EventHandler(buttoncomboMouseLeaveEventHandler);
                                    panelProductBox.Controls.Add(buttoncombo);
                                    buttoncombo.BringToFront();
                                    tlp_searchproduct_dashboard.Controls.Add(panelProductBox, columnIndex, rowIndex);
                                    columnIndex++;
                                }
                                #endregion

                                #region without offer product and item
                                else
                                {
                                    if (columnIndex == 3)
                                    {
                                        columnIndex = 0;
                                        rowIndex++;
                                    }

                                    // Main Panel for each product
                                    Panel panelProductBox = new Panel();
                                    panelProductBox.Name = "panel_product_box" + gridSource[i].Field1;
                                    panelProductBox.Dock = DockStyle.None;
                                    panelProductBox.Width = 150;
                                    panelProductBox.Height = 55;
                                    panelProductBox.BackColor = product_buttoncolor;
                                    panelProductBox.Margin = new Padding(10, 10, 0, 0);

                                    // Configure the Button
                                    Button button = new Button();
                                    button.Text = gridSource[i].Field1;
                                    button.Name = "btn" + gridSource[i].Field1;
                                    button.Tag = gridSource[i].Field2;
                                    button.AccessibleDescription = gridSource[i].Field3;
                                    button.FlatAppearance.BorderSize = 0; // Set border width to 0
                                    button.FlatAppearance.BorderColor = button.BackColor;
                                    button.ForeColor = Color.White;
                                    button.Width = 120;
                                    button.Height = 50;
                                    button.Font = new Font("Arial", 9, FontStyle.Bold);
                                    button.FlatStyle = FlatStyle.Flat;
                                    button.BackColor = product_buttoncolor;
                                    //// Create a circular Region
                                    button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                                    //button.Dock = DockStyle.Fill;
                                    button.Click += new EventHandler(DashboardButtonClickEventHandler);
                                    panelProductBox.Controls.Add(button);

                                    // PictureBox fav
                                    PictureBox pictureBoxFav = new PictureBox();
                                    pictureBoxFav.Name = "picBoxFav" + gridSource[i].Field1;
                                    pictureBoxFav.Tag = gridSource[i].Field2;
                                    pictureBoxFav.AccessibleDescription = gridSource[i].Field3;
                                    pictureBoxFav.Width = 20;
                                    pictureBoxFav.Height = 15;
                                    pictureBoxFav.BorderStyle = BorderStyle.None;
                                    pictureBoxFav.Click += new EventHandler(buttonfavClickEventHandler);
                                    pictureBoxFav.Image = null;
                                    if (Convert.ToInt32(gridSource[i].Field4) == 1)
                                        pictureBoxFav.Image = Properties.Resources.yellow_star;
                                    else
                                        pictureBoxFav.Image = Properties.Resources.white_star;

                                    pictureBoxFav.SizeMode = PictureBoxSizeMode.StretchImage;
                                    panelProductBox.Controls.Add(pictureBoxFav);
                                    pictureBoxFav.BringToFront();

                                    tlp_searchproduct_dashboard.Controls.Add(panelProductBox, columnIndex, rowIndex);
                                    columnIndex++;
                                }
                                #endregion
                            }

                        }
                        pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);

                        //else
                        //    CMessageBox.Show("No Products", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        public bool Checkoffer_exist(int id)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "p.ItemCode_UPC as Field1, p.Itemname as Field2,p.srate as Field3,p.MRP as Field4,p.ItemId as Field5,p.Tax as Field6,p.TaxType as Field7,p.Fcid as Field8,p.TaxSettingsId as Field9,ofd.Discountper as Field10";
                dmnSearch.Table = "tblItem p LEFT JOIN tblOfferDetails ofd on ofd.ProductId=p.ItemId LEFT JOIN tblOfferMaster ofm on ofm.Omid=ofd.Omid AND ofm.OfferType=0  ";
                dmnSearch.WhereNdGroupBy = "where p.Status=1 and p.ItemId= " + id + " AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME()";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private void txtitemcode_serach_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //int rowIndex = 1;
    
                    int columnIndex = 0;
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch dmnSearch = new DmnSearch();
                    if(txtitemcode_serach.Text !=null && txtitemcode_serach.Text !="" )
                    {
                        dmnSearch.Select = "p.Product as Field1,p.ProductId as Field2,0 as Field3 ,ofm.Omid Field4,ofm.Discountper Field5,0 Field6, 0 Field7,0 Field8";
                        dmnSearch.Table = " tblProduct p " +
                            " LEFT JOIN tblMenu m on m.ProductId=p.ProductId " +
                            " LEFT JOIN tblOfferDetails ofd on ofd.ProductId=p.ProductId and  ofd.Status=1 " +
                            " LEFT JOIN tblOfferMaster ofm on ofm.Omid=ofd.Omid and ofm.OfferType=0 AND ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME()" +
                            " where p.Status=1 and m.Status=1  and p.ItemCode ='" + txtitemcode_serach.Text + "'" +

                            " Union Select Itemname as Field1,ItemId as Field2,i.Saletype as Field3,ofm.Omid Field4,ofm.Discountper Field5,pr.SRate Field6, pr.QOH Field7,pr.Id Field8 from " +
                            " tblItem i " +
                            " LEFT JOIN tblOfferDetails ofd on ofd.ProductId = i.ItemId and ofd.Status = 1 " +
                            " LEFT JOIN tblOfferMaster ofm on ofm.Omid = ofd.Omid and ofm.OfferType = 0 AND ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() " +
                            " LEFT JOIN tblProductRate pr on pr.ProductId = i.ItemId and pr.Status = 1 and pr.QOH>0";
                        dmnSearch.WhereNdGroupBy = " where i.Saletype = 1  and i.Status = 1  and  i.ItemCode_UPC ='" + txtitemcode_serach.Text + "'";

                        dmnSearch.OrderBy = "";
                        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        pnl_serachproduct_dashboard.Controls.Clear();
                        tlp_searchproduct_dashboard.Controls.Clear();
                        tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                        tlp_searchproduct_dashboard.AutoScroll = true;

                        if (gridSource.Count == 1)
                        {
                            pnlprice.Visible = false;
                            BllSearch<DmnGridFields> bllSearch_item = new BllSearch<DmnGridFields>();
                            dmnSearch.Select = "Itemname as Field1,ItemId as Field2,ItemCode_UPC as Field3,pr.prate as Field4,pr.srate as Field5," +
                            "pr.MRP as Field6,i.Tax as Field7,i.TaxType as Field8,i.TaxSettingsId as Field9,pr.QOH as Field10," +
                            "i.unit as Field11,u.UnitName as Field12,i.Fcid as Field13,i.TaxSettingsId as Field14,pr.id Field15,i.DiscountPer Field16,pr.QOH Field17";
                            dmnSearch.Table = "tblItem i LEFT JOIN tblProductRate pr on  pr.ProductId=i.ItemId LEFT JOIN tblUnit u on  u.UId=i.unit ";
                            dmnSearch.WhereNdGroupBy = "where i.Status=1 and pr.ProductId = " + gridSource[0].Field2 + " and pr.id = " + gridSource[0].Field8;
                            dmnSearch.OrderBy = "";
                            var gridSourceDetails = bllSearch_item.GetSearchData("prdSearch", dmnSearch);
                            if (gridSourceDetails.Count > 0)
                            {
                                string tlpname = "tlp_seletedItem_col" + gridSource[0].Field8 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                                if (Booking_Status == false)
                                {
                                    if (string.IsNullOrEmpty(txtGrandTotal.Text))
                                        txtGrandTotal.Text = "0";
                                }

                                if (tlp_sele != null)
                                {
                                    int rowno = 0;
                                    for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                                    {
                                        if (Convert.ToInt32(gridSourceDetails[0].Field15) == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value))
                                        {
                                            Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                                            if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) > Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value))
                                            {
                                                CMessageBox.Show("Aviablle Quantity is : " + Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value), "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) - 1;
                                                txtitemcode_serach.Text = "";
                                                txtitemcode_serach.Focus();
                                                return;
                                            }
                                            rowno = i;
                                            totalcellamount(i);
                                            string txtname = "txtqty_" + gridSource[0].Field8 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                            TextBox qty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                                            int cur_qty = Convert.ToInt32(qty.Text) + 1;
                                            qty.Text = Convert.ToString(cur_qty);
                                            break;
                                        }
                                    }
                                    decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[ioriginalprice].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                                    txtGrandTotal.Text = Convert.ToString(totalamt);
                                    netAmount();
                                    tlp_sele.Focus();
                                    txtitemcode_serach.Text = "";
                                    txtitemcode_serach.Focus();
                                }
                                else
                                {
                                    for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                                    {

                                        if (Convert.ToInt32(gridSourceDetails[0].Field15) == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value))
                                        {
                                            Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                                            totalcellamount(i);
                                            break;
                                        }
                                    }

                                    #region Grid Inseration multiple 
                                    r1 = Dgv_Selected_Item.RowCount - 1;

                                    Dgv_Selected_Item.Rows[r1].Cells[islno].Value = r1 + 1;
                                    Dgv_Selected_Item.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                    Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = gridSourceDetails[0].Field3;
                                    Dgv_Selected_Item.Rows[r1].Cells[ibarcode2].Value = gridSourceDetails[0].Field3;
                                    Dgv_Selected_Item.Rows[r1].Cells[iname].Value = gridSourceDetails[0].Field1;
                                    Dgv_Selected_Item.Rows[r1].Cells[iunit].Value = gridSourceDetails[0].Field12;
                                    Dgv_Selected_Item.Rows[r1].Cells[iunitid].Value = gridSourceDetails[0].Field11;
                                    Dgv_Selected_Item.Rows[r1].Cells[iqty].Value = "1";
                                    Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = gridSourceDetails[0].Field4;
                                    Dgv_Selected_Item.Rows[r1].Cells[israte].Value = gridSourceDetails[0].Field5;
                                    Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = gridSourceDetails[0].Field6;
                                    if (Checkoffer(Convert.ToInt32(gridSource[0].Field2)))
                                    {
                                        Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = gridSource_offer[0].Field10;
                                        Dgv_Selected_Item.Rows[r1].Cells[ioffertype].Value = 0;
                                    }
                                    else
                                    {
                                        Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = gridSourceDetails[0].Field16;
                                        Dgv_Selected_Item.Rows[r1].Cells[ioffertype].Value = 3;
                                    }
                                    Dgv_Selected_Item.Rows[r1].Cells[idisamt].Value = "0";
                                    Dgv_Selected_Item.Rows[r1].Cells[iitemid].Value = gridSourceDetails[0].Field2;
                                    Dgv_Selected_Item.Rows[r1].Cells[itax].Value = gridSourceDetails[0].Field7;
                                    Dgv_Selected_Item.Rows[r1].Cells[itaxtype].Value = gridSourceDetails[0].Field8;
                                    Dgv_Selected_Item.Rows[r1].Cells[itype].Value = gridSourceDetails[0].Field13;
                                    Dgv_Selected_Item.Rows[r1].Cells[itaxsettingid].Value = gridSourceDetails[0].Field14;
                                    Dgv_Selected_Item.Rows[r1].Cells[ipridno].Value = gridSourceDetails[0].Field15;
                                    Dgv_Selected_Item.Rows[r1].Cells[istatusno].Value = 1;
                                    Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = Color.White;
                                    Dgv_Selected_Item.Rows[r1].Cells[iofferstatus].Value = 0;
                                    Dgv_Selected_Item.Rows[r1].Cells[itabletype].Value = 2;
                                    Dgv_Selected_Item.Rows[r1].Cells[ioriginalqty].Value = gridSourceDetails[0].Field17;

                                    totalcellamount(r1);
                                    Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value);

                                    decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                                    txtGrandTotal.Text = Convert.ToString(totalamt);

                                    netAmount();
                                    Dgv_Selected_Item.Rows.Add(1);
                                    GridSlno_Refresh();

                                    #endregion

                                    // Configure the TableLayoutPanel
                                    TableLayoutPanel tlp_seletedItem_col = new TableLayoutPanel();
                                    float[] columnPercentages = { 44.82f, 6.27f, 14.94f, 6.02f, 19.04f, 8.92f };
                                    for (int i = 0; i < columnPercentages.Length; i++)
                                    {
                                        tlp_seletedItem_col.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercentages[i]));
                                    }

                                    tlp_seletedItem_col.Name = "tlp_seletedItem_col" + Convert.ToInt32(gridSourceDetails[0].Field15) + "_" + Convert.ToInt32(Tabletype.DirectSale); ;
                                    tlp_seletedItem_col.Tag = gridSource[0].Field8;
                                    tlp_seletedItem_col.AccessibleDescription = "2";
                                    tlp_seletedItem_col.BackColor = Color.LightBlue;
                                    tlp_seletedItem_col.Height = 45;

                                    // Configure the Button for Item name
                                    Button buttonitemname = new Button();
                                    buttonitemname.Text = gridSourceDetails[0].Field1;
                                    buttonitemname.Name = "buttonitemname_" + gridSourceDetails[0].Field15 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                    buttonitemname.Tag = gridSourceDetails[0].Field15;
                                    buttonitemname.AccessibleDescription = "1";
                                    buttonitemname.BackColor = Color.Beige;
                                    buttonitemname.Height = 40;
                                    buttonitemname.Font = new Font(buttonitemname.Font.FontFamily, 8, FontStyle.Bold);
                                    buttonitemname.Click += new EventHandler(buttonitemnameClickEventHandler);

                                    // Configure the Button for add Qty
                                    Button buttonAdd = new Button();
                                    buttonAdd.Text = "+";
                                    buttonAdd.Name = "buttonAdd_" + gridSourceDetails[0].Field15 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                    buttonAdd.Tag = gridSourceDetails[0].Field15;
                                    buttonAdd.AccessibleDescription = "2";
                                    buttonAdd.BackColor = Color.Beige;
                                    buttonAdd.Margin = new Padding(0, 14, 0, 0);
                                    buttonAdd.Font = new Font(buttonAdd.Font.FontFamily, 8, FontStyle.Bold);
                                    buttonAdd.Click += new EventHandler(ButtonbuttonAddClickEventHandler);

                                    // Configure the textbox enter QTY
                                    TextBox txtqty = new TextBox();
                                    txtqty.Text = "1";
                                    txtqty.Name = "txtqty_" + gridSourceDetails[0].Field15 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                    txtqty.Tag = gridSourceDetails[0].Field15;
                                    txtqty.AccessibleDescription = "2";
                                    txtqty.BackColor = Color.Beige;
                                    txtqty.TextAlign = HorizontalAlignment.Center;
                                    txtqty.Margin = new Padding(0, 14, 0, 0);
                                    txtqty.Font = new Font(txtqty.Font.FontFamily, 8, FontStyle.Bold);
                                    txtqty.Enabled = false;

                                    // Configure the textbox enter QTY
                                    Button buttonRemove = new Button();
                                    buttonRemove.Text = "-";
                                    buttonRemove.Name = "btn_remove_" + gridSourceDetails[0].Field15 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                    buttonRemove.Tag = gridSourceDetails[0].Field15;
                                    buttonRemove.AccessibleDescription = "2";
                                    buttonRemove.BackColor = Color.Beige;
                                    buttonRemove.Margin = new Padding(0, 14, 0, 0);
                                    buttonRemove.Font = new Font(buttonRemove.Font.FontFamily, 8, FontStyle.Bold);
                                    buttonRemove.Click += new EventHandler(ButtonRemoveClickEventHandler);

                                    // Configure the textbox Price
                                    TextBox txtprice = new TextBox();
                                    decimal F_price = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value);
                                    txtprice.Text = Convert.ToString(F_price);
                                    txtprice.Tag = gridSourceDetails[0].Field15;
                                    txtprice.BackColor = Color.Beige;
                                    txtprice.TextAlign = HorizontalAlignment.Center;
                                    txtprice.Margin = new Padding(0, 14, 0, 0);
                                    txtprice.Font = new Font(txtprice.Font.FontFamily, 8, FontStyle.Bold);
                                    txtprice.Enabled = false;

                                    // Configure Item Delete Button
                                    PictureBox pb_item_delete = new PictureBox();
                                    pb_item_delete.Name = "btn_remove_" + gridSourceDetails[0].Field15 + "_" + Convert.ToInt32(Tabletype.DirectSale);
                                    pb_item_delete.Tag = gridSourceDetails[0].Field15;
                                    pb_item_delete.AccessibleDescription = "2";
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

                                    tlp_seletedItem_col.Focus();
                                }

                                txtitemcode_serach.Text = "";
                                txtitemcode_serach.Focus();
                            }
                            checksave_refresh();
                        }
                        else if (gridSource.Count > 0)
                        {
                            itemcode_serach_Status = true;
                            //Checkoffer(id);
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
                                pricebutton.Text = "RS. " + (gridSource[i].Field6).ToString() + "  QTY : " + (gridSource[i].Field7).ToString(); ;
                                pricebutton.Name = "pr_" + (gridSource[i].Field8).ToString();
                                pricebutton.Tag = gridSource[i].Field2;
                                pricebutton.AccessibleDescription = gridSource[i].Field8;

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
                            int panelWidthPercentage = 25;
                            int panelheightPercentage = 45;
                            int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                            int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                            pnlprice.Size = new Size(panelWidth, panelHeight);
                            pnlprice.Location = new Point(this.ClientSize.Width / 2 - pnlprice.Size.Width / 2, this.ClientSize.Height / 2 - pnlprice.Size.Height / 2);
                            pnlprice.Controls.Add(tlp_price_list);

                            #region old code
                            //for (int i = 0; i < gridSource.Count; i++)
                            //{
                            //    #region with offer product and item
                            //    if (Convert.ToInt32(gridSource[i].Field4) > 0)
                            //    {
                            //        if (columnIndex == 2)
                            //        {
                            //            columnIndex = 0;
                            //            rowIndex++;
                            //        }
                            //        // Main Panel for each product
                            //        Panel panelProductBox = new Panel();
                            //        panelProductBox.Name = "panel_product_box" + gridSource[i].Field1;
                            //        panelProductBox.Dock = DockStyle.None;
                            //        panelProductBox.Width = 190;
                            //        panelProductBox.Height = 73;
                            //        panelProductBox.BackColor = Color.Transparent; //product_buttoncolor;
                            //        panelProductBox.Margin = new Padding(10, 10, 0, 0);

                            //        ToolTip tooltip = new ToolTip();

                            //        // Configure the Button
                            //        Button button = new Button();
                            //        button.Text = gridSource[i].Field1;
                            //        button.Name = "btn" + gridSource[i].Field1;
                            //        button.Tag = gridSource[i].Field2;

                            //        if (Convert.ToInt32(gridSource[i].Field3) == 0)
                            //            button.AccessibleDescription = "0";
                            //        else
                            //            button.AccessibleDescription = "1";

                            //        button.FlatAppearance.BorderSize = 0;
                            //        button.FlatAppearance.BorderColor = button.BackColor;
                            //        button.Font = new Font("Arial", 9, FontStyle.Bold);
                            //        //button.Margin = new Padding(10, 30, 0, 0); 
                            //        button.Location = new Point(panelProductBox.Top + 10, panelProductBox.Left + 20);
                            //        button.Width = 170;
                            //        button.Height = 50;
                            //        button.FlatStyle = FlatStyle.Flat;
                            //        button.BackColor = product_buttoncolor;
                            //        button.ForeColor = Color.White;
                            //        button.Padding = new Padding(16, 0, 0, 0);
                            //        tooltip.SetToolTip(button, gridSource[i].Field1 + " " + gridSource[i].Field4 + "%");
                            //        //// Create a circular Region
                            //        button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                            //        button.Click += new EventHandler(DashboardButtonClickEventHandler);
                            //        button.MouseEnter += new EventHandler(buttonofferMousehoverEventHandler);
                            //        button.MouseLeave += new EventHandler(buttonofferMouseLeaveEventHandler);
                            //        panelProductBox.Controls.Add(button);


                            //        // Button offer
                            //        Button buttonoffer = new Button();
                            //        buttonoffer.Name = "btnFav" + gridSource[i].Field1;
                            //        buttonoffer.Tag = gridSource[i].Field2;
                            //        if (Convert.ToInt32(gridSource[i].Field3) == 0)
                            //            buttonoffer.AccessibleDescription = "0";
                            //        else
                            //            buttonoffer.AccessibleDescription = "1";
                            //        buttonoffer.Width = 50;
                            //        buttonoffer.Height = 45;
                            //        buttonoffer.FlatStyle = FlatStyle.Flat;
                            //        buttonoffer.FlatAppearance.BorderSize = 0;
                            //        buttonoffer.Text = gridSource[i].Field5;
                            //        buttonoffer.ForeColor = Color.White;
                            //        buttonoffer.Font = new Font("Arial", 9, FontStyle.Bold);
                            //        buttonoffer.BackColor = product_buttoncolor;
                            //        tooltip.SetToolTip(buttonoffer, gridSource[i].Field1 + " " + gridSource[i].Field6 + "%");
                            //        buttonoffer.BackgroundImage = Properties.Resources.offer_button_percentage;
                            //        buttonoffer.BackgroundImageLayout = ImageLayout.Stretch;
                            //        buttonoffer.Click += new EventHandler(DashboardButtonClickEventHandler);
                            //        buttonoffer.MouseEnter += new EventHandler(buttonofferMousehoverEventHandler);
                            //        buttonoffer.MouseLeave += new EventHandler(buttonofferMouseLeaveEventHandler);
                            //        panelProductBox.Controls.Add(buttonoffer);
                            //        buttonoffer.BringToFront();

                            //        tlp_searchproduct_dashboard.Controls.Add(panelProductBox, columnIndex, rowIndex);
                            //        columnIndex++;
                            //    }
                            //    #endregion

                            //    #region without offer product and item
                            //    else
                            //    {
                            //        if (columnIndex == 3)
                            //        {
                            //            columnIndex = 0;
                            //            rowIndex++;
                            //        }

                            //        // Main Panel for each product
                            //        Panel panelProductBox = new Panel();
                            //        panelProductBox.Name = "panel_product_box" + gridSource[i].Field1;
                            //        panelProductBox.Dock = DockStyle.None;
                            //        panelProductBox.Width = 150;
                            //        panelProductBox.Height = 55;
                            //        panelProductBox.BackColor = product_buttoncolor;
                            //        panelProductBox.Margin = new Padding(10, 10, 0, 0);

                            //        // Configure the Button
                            //        Button button = new Button();
                            //        button.Text = gridSource[i].Field1;
                            //        button.Name = "btn" + gridSource[i].Field1;
                            //        button.Tag = gridSource[i].Field2;
                            //        button.AccessibleDescription = gridSource[i].Field3;
                            //        button.FlatAppearance.BorderSize = 0; // Set border width to 0
                            //        button.FlatAppearance.BorderColor = button.BackColor;
                            //        button.ForeColor = Color.White;
                            //        button.Width = 120;
                            //        button.Height = 50;
                            //        button.Font = new Font("Arial", 9, FontStyle.Bold);
                            //        button.FlatStyle = FlatStyle.Flat;
                            //        button.BackColor = product_buttoncolor;
                            //        //// Create a circular Region
                            //        button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                            //        //button.Dock = DockStyle.Fill;
                            //        button.Click += new EventHandler(DashboardButtonClickEventHandler);
                            //        panelProductBox.Controls.Add(button);

                            //        // PictureBox fav
                            //        PictureBox pictureBoxFav = new PictureBox();
                            //        pictureBoxFav.Name = "picBoxFav" + gridSource[i].Field1;
                            //        pictureBoxFav.Tag = gridSource[i].Field2;
                            //        pictureBoxFav.AccessibleDescription = gridSource[i].Field3;
                            //        pictureBoxFav.Width = 20;
                            //        pictureBoxFav.Height = 15;
                            //        pictureBoxFav.BorderStyle = BorderStyle.None;
                            //        pictureBoxFav.Click += new EventHandler(buttonfavClickEventHandler);
                            //        pictureBoxFav.Image = null;
                            //        if (Convert.ToInt32(gridSource[i].Field4) == 1)
                            //            pictureBoxFav.Image = Properties.Resources.yellow_star;
                            //        else
                            //            pictureBoxFav.Image = Properties.Resources.white_star;

                            //        pictureBoxFav.SizeMode = PictureBoxSizeMode.StretchImage;
                            //        panelProductBox.Controls.Add(pictureBoxFav);
                            //        pictureBoxFav.BringToFront();

                            //        tlp_searchproduct_dashboard.Controls.Add(panelProductBox, columnIndex, rowIndex);
                            //        columnIndex++;
                            //    }
                            //    #endregion
                            //}
                            //pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                            #endregion
                        }
                        else
                        {
                            txtitemcode_serach.Text = "";
                            CMessageBox.Show("No Products", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtitemcode_serach.Focus();
                        }
                    }
                

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
        private void AdjustLabelFontSize(System.Windows.Forms.Label label)
        {
            int originalFontSize = (int)label.Font.Size;
            Size textSize = TextRenderer.MeasureText(label.Text, label.Font);
            float originalSize = 0;
            bool flag = false;
            // Check if the text exceeds a single line
            if (textSize.Width > label.Width)
            {
                originalSize = label.Font.Size; // Store the original font size
                // Reduce the font size until it fits within a single line
                while (textSize.Width > label.Width && label.Font.Size > 1)
                {
                    label.Font = new Font(label.Font.FontFamily, label.Font.Size - 1);
                    textSize = TextRenderer.MeasureText(label.Text, label.Font);
                }

                flag = true;
            }
            //if (flag)
            //{
            //    labelsWithMultilineText.Add(new LabelInfo
            //    {
            //        LabelName = label.Name,
            //        OriginalFontSize = originalSize,
            //        ReduceFontSize = label.Font.Size
            //    });
            //}
        }
        private void btn_sidebar_toggle_Click(object sender, EventArgs e)
        {
            if (pnlsidebar_searchproduct.Visible)
            {
                pnlsidebar_searchproduct.Visible = false;
                tlp_food_cate.Visible = false;

                tlp_serach_main.ColumnStyles[0] = new ColumnStyle(SizeType.AutoSize);
                tlp_serach_main.ColumnStyles[1] = new ColumnStyle(SizeType.AutoSize);
                tlp_serach_main.ColumnStyles[2] = new ColumnStyle(SizeType.AutoSize);
                textb_shape();

            }
            else
            {
                if (checkedtableNames.Count == 0)
                {
                    pnlsidebar_searchproduct.Visible = true;
                    tlp_serach_main.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 27.22F);
                    tlp_serach_main.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 6.50f);
                    tlp_serach_main.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 66.21f);
                    tlp_food_cate.Visible = true;
                    textb_shape();
                }
            }
        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtitemcode_serach, txtname_search, txt_product_des, txtrecvamount, txtGrandTotal, txtpaidamount, txtotherdedamt, txtotheraddamt, txtdis_amt, txtdis_per, txtother_descr, txtdis_descr, txtnetamount, txtentrie_descr, txtdisamt_promo, txtpromocode, txtdiscountamt_promo, txtnumber_split, txtadvance, txtph, txtemailid, txtaddress, txtcusname })
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
                    if (pnl_products_descr.Visible)
                        pnl_products_descr.Visible = false;
                    else if (pnlproductviewmore.Visible)
                        pnlproductviewmore.Visible = false;
                    else if (tlp_gridview.Visible)
                        tlp_gridview.Visible = false;
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
                        if (txt_product_des.Text != "Description Type Here .....")
                            Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value = txt_product_des.Text;
                        break;
                    }
                }
            }
            else if(saletype == 3)
            {
                for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                {
                    if (Proid_Prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value))
                    {
                        string name = Dgv_Selected_Item.Rows[i].Cells[iproducttypeno].Value.ToString();
                        if (txt_product_des.Text != "Description Type Here .....")
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
                       // string name = Dgv_Selected_Item.Rows[i].Cells[iname].Value.ToString();
                        if (txt_product_des.Text != "Description Type Here .....")
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
                txt_product_des.ForeColor = Color.DarkGray;
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
            AdjustLabelFontSize(label3);
            AdjustLabelFontSize(label4);
            AdjustLabelFontSize(label6);
            AdjustLabelFontSize(label5);
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
                txtdis_descr.ForeColor = Color.DarkGray;
            }
        }

        private void txtother_descr_Leave(object sender, EventArgs e)
        {
            if (txtother_descr.Text == "")
            {
                txtother_descr.Text = "Description Type Here ...";
                txtother_descr.ForeColor = Color.DarkGray;
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
                if (string.IsNullOrEmpty(txtdiscountamt_promo.Text))
                    txtdiscountamt_promo.Text = "0";
                if (string.IsNullOrEmpty(txtadvance.Text))
                    txtadvance.Text = "0";
                decimal value;
                if (decimal.TryParse(txtotheraddamt.Text, out value) && decimal.TryParse(txtdis_amt.Text, out value) && decimal.TryParse(txtotherdedamt.Text, out value))
                {
                    decimal netAmount = 0;
                    netAmount = (Convert.ToDecimal(txtGrandTotal.Text) + Convert.ToDecimal(txtotheraddamt.Text)) - (Convert.ToDecimal(txtdis_amt.Text) + Convert.ToDecimal(txtotherdedamt.Text) + Convert.ToDecimal(txtdiscountamt_promo.Text));
                    
                    if (Booking_Status && (netAmount > Convert.ToDecimal(txtadvance.Text)))
                        netAmount = netAmount - Convert.ToDecimal(txtadvance.Text);

                    txtnetamount.Text = netAmount.ToString("N2");
                    label20.Text = "Net Amount : " + txtnetamount.Text;
                    label21.Text = "Net Amount : " + txtnetamount.Text;
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
            {
                tlp_payment_main.RowStyles[0] = new RowStyle(SizeType.Percent, 5.90F);
                tlp_payment_main.RowStyles[2] = new RowStyle(SizeType.AutoSize);
                tlp_payment_main.RowStyles[3] = new RowStyle(SizeType.Percent, 2.16F);
                tlp_payment_main.RowStyles[4] = new RowStyle(SizeType.AutoSize);
                txtentrie_descr.Visible = false;

                if (pnl_userdetails.Visible == false)
                {
                    tlp_payment_main.RowStyles[1] = new RowStyle(SizeType.Percent, 81.54F);
                    tlp_payment_main.RowStyles[5] = new RowStyle(SizeType.Percent, 10.39F);
                }
                else
                {
                    tlp_payment_main.RowStyles[1] = new RowStyle(SizeType.Percent, 40);
                    tlp_payment_main.RowStyles[5] = new RowStyle(SizeType.Percent, 11F);
                }
            }
            else
            {
                tlp_payment_main.RowStyles[0] = new RowStyle(SizeType.Percent, 5.90F);
                tlp_payment_main.RowStyles[1] = new RowStyle(SizeType.Percent, 38);
                tlp_payment_main.RowStyles[2] = new RowStyle(SizeType.AutoSize);
                tlp_payment_main.RowStyles[3] = new RowStyle(SizeType.Percent, 2.16F);
                tlp_payment_main.RowStyles[4] = new RowStyle(SizeType.AutoSize);
                tlp_payment_main.RowStyles[5] = new RowStyle(SizeType.Percent, 13F);

                if (pnl_userdetails.Visible == false)
                {
                    tlp_payment_main.RowStyles[1] = new RowStyle(SizeType.Percent, 81.54F);
                    tlp_payment_main.RowStyles[5] = new RowStyle(SizeType.Percent, 10.39F);
                }
                else
                {
                    tlp_payment_main.RowStyles[1] = new RowStyle(SizeType.Percent, 40);
                    tlp_payment_main.RowStyles[5] = new RowStyle(SizeType.Percent, 11F);
                }

                txtentrie_descr.Visible = true;
                txtentrie_descr.Dock = DockStyle.Fill;
                textb_shape();
            }
           
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
                    dmnordermaster.BookmasterId = Bookingmasterid;
                    dmnordermaster.OrderMasterId = Convert.ToInt32(txtordermasterid.Text);
                    dmnordermaster.EntryNo = "";
                    dmnordermaster.TotalQuantity = Convert.ToInt32(txttotalQTY.Text);
                    dmnordermaster.CustomerName = Convert.ToString(txtcusname.Text);
                    dmnordermaster.Telephone = Convert.ToString(txtph.Text);
                    dmnordermaster.Email = Convert.ToString(txtemailid.Text);
                    dmnordermaster.Address = Convert.ToString(txtaddress.Text);
                    if (rb_male.Checked)
                        dmnordermaster.Gender = 1;
                    else
                        dmnordermaster.Gender = 2;

                    dmnordermaster.BranchId = FrmDashboard.BranchId;
                    dmnordermaster.CreatedBy = FrmDashboard.UserId;
                    dmnordermaster.Status = 1;
                    dmnordermaster.Tid = tableId;
                    var InsertStatusP = bll_order.AddEditData("prdInsert_Upd_OrderMaster", dmnordermaster);
                    if (InsertStatusP.OrderMasterId > 0)
                    {
                        ResetFlag(Convert.ToInt32(txtordermasterid.Text));

                    for (int i = 0; i < Dgv_Selected_Item.Rows.Count; i++)
                    {
                            if(Booking_Status)
                            {
                                if (Dgv_Selected_Item.Rows[i].Cells[iitemid].Value != null && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) > 0)
                                {
                                    dmnorderdetail.OrderMasterId = Convert.ToInt32(InsertStatusP.OrderMasterId);
                                    dmnorderdetail.ItemBarCode = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ibarcode].Value);

                                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == 3)
                                        dmnorderdetail.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iproducttypeno].Value);
                                    else
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
                                    dmnorderdetail.Tax = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itax].Value);
                                    dmnorderdetail.TaxType = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itaxtype].Value);

                                    dmnorderdetail.IGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value);
                                    dmnorderdetail.CGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value) / 2;
                                    dmnorderdetail.SGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value) / 2;
                                    dmnorderdetail.VATAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ivatamtno].Value);

                                    dmnorderdetail.ItemId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value);
                                    dmnorderdetail.Srate = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[israte].Value);
                                    dmnorderdetail.MRP = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[imrp].Value);
                                    dmnorderdetail.Fcid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itype].Value);
                                    dmnorderdetail.TableType = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value);
                                    dmnorderdetail.MarketPrice = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value);
                                    if (txtentrie_descr.Text == "Description Type here .....")
                                        dmnorderdetail.MainRemarks = "";
                                    else
                                        dmnorderdetail.MainRemarks = txtentrie_descr.Text;

                                    dmnorderdetail.BranchId = FrmDashboard.BranchId;
                                    dmnorderdetail.CreatedBy = FrmDashboard.UserId;
                                    dmnorderdetail.Status = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value);

                                    dmnorderdetail.OrderDetailsId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iorderdetailsidno].Value);
                                    dmnorderdetail.NetAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);
                                    dmnorderdetail.PrId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value);
                                    dmnorderdetail.Notes = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value);
                                    dmnorderdetail.OrderNo = ResetFlagvalue;
                                    dmnorderdetail.BookingFlag = 1;
                                    dmnorderdetail.KOTPrintStatus = 0; 
                                    dmnorderdetail.Tid = Convert.ToInt32(txttid.Text);

                                    var InsertStatus = bll_order.AddEditDataDet("prdInsert_Upd_OrderDetails", dmnorderdetail);
                                    if (InsertStatus == null)
                                    {
                                        flage = true;
                                        CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        break;
                                    }

                                }
                            }
                            else
                            {
                                if (Dgv_Selected_Item.Rows[i].Cells[iqty].Value != null && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) > 0)
                                {
                                    dmnorderdetail.OrderMasterId = Convert.ToInt32(InsertStatusP.OrderMasterId);
                                    dmnorderdetail.ItemBarCode = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ibarcode].Value);

                                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == 3)
                                        dmnorderdetail.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iproducttypeno].Value);
                                    else
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
                                    dmnorderdetail.Tax = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itax].Value);
                                    dmnorderdetail.TaxType = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itaxtype].Value);

                                    dmnorderdetail.IGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value);
                                    dmnorderdetail.CGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value) / 2;
                                    dmnorderdetail.SGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value) / 2;
                                    dmnorderdetail.VATAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ivatamtno].Value);

                                    dmnorderdetail.ItemId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value);
                                    dmnorderdetail.Srate = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[israte].Value);
                                    dmnorderdetail.MRP = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[imrp].Value);
                                    dmnorderdetail.Fcid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itype].Value);
                                    dmnorderdetail.TableType = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value);
                                    dmnorderdetail.MarketPrice = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value);

                                    if (txtentrie_descr.Text == "Description Type here .....")
                                        dmnorderdetail.MainRemarks = "";
                                    else
                                        dmnorderdetail.MainRemarks = txtentrie_descr.Text;


                                    dmnorderdetail.BranchId = FrmDashboard.BranchId;
                                    dmnorderdetail.CreatedBy = FrmDashboard.UserId;
                                    dmnorderdetail.Status = 1;

                                    dmnorderdetail.OrderDetailsId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iorderdetailsidno].Value);
                                    dmnorderdetail.NetAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);
                                    dmnorderdetail.PrId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value);
                                    dmnorderdetail.Notes = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value);
                                    dmnorderdetail.BookingFlag = 0;
                                    dmnorderdetail.OrderNo = ResetFlagvalue;
                                    dmnorderdetail.Tid = Convert.ToInt32(txttid.Text);

                                    var InsertStatus = bll_order.AddEditDataDet("prdInsert_Upd_OrderDetails", dmnorderdetail);
                                    if (InsertStatus == null)
                                    {
                                        flage = true;
                                        CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        break;
                                    }

                                }
                            }
 

                    }
                    }
                    if (flage == false)
                    {
                        if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                        {
                            FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                            _instanceFrmdash.LoadTableStaus(0);
                        }
                        //if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                        //{
                        //    FrmOrderView _instanceFrmOrderView = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                        //    _instanceFrmOrderView.FillOrders();
                        //}
                        //if (System.Windows.Forms.Application.OpenForms["FrmKitchenView"] != null)
                        //{
                        //    FrmKitchenView _instanceFrmKitchenView = Application.OpenForms["FrmKitchenView"] as FrmKitchenView;
                        //    _instanceFrmKitchenView.Fillorders();
                        //}
                        CMessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Notify the kitchen view to refresh
                        ClearAll();
                        this.Close();
                    }

                }
            }
            catch(Exception ex)
            {

            }

        }

        public void ResetFlag(int ordermasterid)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "ISNULL(MAX(OrderNo),0) as Field1";
                dmnSearch.Table = "tblkitchenOrder";
                dmnSearch.WhereNdGroupBy = "where OrderMasterId = " + ordermasterid;
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    ResetFlagvalue = Convert.ToInt32(gridSource[0].Field1) +1;
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
                btn_itemwise.Visible = true;
                btn_split_number.Visible = true;
                txtname_search.Text = "Food Name .....";
                txtitemcode_serach.Text = "Item Code .....";
                txtother_descr.Text = "Description Type Here ...";
                txtdis_descr.Text = "Description Type Here ...";
                txtother_descr.ForeColor = Color.DarkGray;
                txtdis_descr.ForeColor = Color.DarkGray;
                tlp_seletedItem_main.Controls.Clear();
                Dgv_Selected_Item.Rows.Clear();
                Dgv_Selected_Item.Rows.Add();
                ResetFlagvalue = 0;
                Promodetailsid = 0;
                Promomasterid = 0;
                Splitypeno = 0;
                Existingsalemasterid = 0;
                ReservationTotal = 0;

                btn_itemwise.Enabled = true;
                itemcode_serach_Status = false;

                int rcount = 0;
                if (Dgv_Selected_Item.RowCount == 0)
                    for (int i = 0; i < Dgv_Selected_Item.RowCount; i++)
                    {
                        if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) > 0)
                        {
                            rcount++;
                            break;
                        }
                    }
                if (rcount == 0)
                    Showprint_Save(Convert.ToInt32(ShowButtonType.Nnone));

                setperfixsale();

                lbladvanceamt.Visible = false;
                txtadvance.Visible = false;
                tlp_calculation_total.RowStyles[0] = new RowStyle(SizeType.AutoSize);
                tlp_calculation_total.RowStyles[1] = new RowStyle(SizeType.Percent, 25);
                tlp_calculation_total.RowStyles[2] = new RowStyle(SizeType.Percent, 25);
                tlp_calculation_total.RowStyles[3] = new RowStyle(SizeType.Percent, 25);
                txtpromocode.Enabled = true;
                btn_save_promo.Enabled = true;

                txtph.Text = "";
                txtemailid.Text = "";
                txtaddress.Text = "";
                txtcusname.Text = "";

                rb_female.Checked = false;
                rb_male.Checked = false;

                Btnpartpayment.Visible = true;
                btn_splitpayment.Visible = true;
                combined_Status = false;

            }
            catch (Exception)
            {

            }
        }
        private void ChangeRowColor(TableLayoutPanel tableLayoutPanel, int rowIndex, Color backgroundColor)
        {
            // Iterate through controls in the specified row
            foreach (Control control in tableLayoutPanel.Controls)
            {
                TableLayoutPanelCellPosition cellPosition = tableLayoutPanel.GetCellPosition(control);

                if (cellPosition.Row == rowIndex)
                {
                    // Set the background color of the control in the specified row
                    control.BackColor = backgroundColor;
                }
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
                pnlsidebar_searchproduct.Visible = true;
                tlp_serach_main.ColumnStyles[0] = new ColumnStyle(SizeType.Percent, 27.22F);
                tlp_serach_main.ColumnStyles[1] = new ColumnStyle(SizeType.Percent, 6.50f);
                tlp_serach_main.ColumnStyles[2] = new ColumnStyle(SizeType.Percent, 66.21f);
                tlp_food_cate.Visible = true;
                textb_shape();           

            if (OrderRunning_Status)
            {
                Promodetailsid = 0;
                Promomasterid = 0;
                CheckRe_order();

                btn_itemwise.Visible = true;
                btn_split_number.Visible = true;
                txtpromocode.Enabled = true;
                btn_save_promo.Enabled = true;
                Dgv_Selected_Item.Rows.Clear();
                Dgv_Selected_Item.Rows.Add();
            }
            else if (Booking_Status)
            {
                Promodetailsid = 0;
                Promomasterid = 0;
                CheckRe_Bookingorder();
                btn_itemwise.Visible = true;
                btn_split_number.Visible = true;
                txtpromocode.Enabled = true;
                btn_save_promo.Enabled = true;

                Dgv_Selected_Item.Rows.Clear();
                Dgv_Selected_Item.Rows.Add();
            }
            else
            {
                ClearAll();
                checksave_refresh();
            }

        }
        public bool MandatoryCheck_KOT()
        {
            if (string.IsNullOrEmpty(txtordermasterid.Text))
                txtordermasterid.Text = "0";
            if (tlp_seletedItem_main.Controls.Count == 0)
            {

                CMessageBox.Show("Product cart is Empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (string.IsNullOrEmpty(txtpaidamount.Text))
            {
                CMessageBox.Show("Please Enter Paid Amount Is Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtpaidamount.Focus();
                return false;
            }
            return true;
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtentrie_descr_Enter(object sender, EventArgs e)
        {
            if (txtentrie_descr.Text == "Description Type here .....")
            {
                txtentrie_descr.Text = "";
                txtentrie_descr.ForeColor = Color.Black;
            }
        }

        private void txtentrie_descr_Leave(object sender, EventArgs e)
        {
            if (txtentrie_descr.Text == "")
            {
                txtentrie_descr.Text = "Description Type here .....";
                txtentrie_descr.ForeColor = Color.DarkGray;
            }
        }

        private void btn_promo_code_Click(object sender, EventArgs e)
        {
            if (tlp_promo_code.Visible)
            {
                Control controlToRemove = tlp_calculation_main.GetControlFromPosition(0, 0);
                tlp_calculation_main.Controls.Remove(controlToRemove);
                tlp_calculation_main.Controls.Add(tlp_calculation_total, 0, 0);
                tlp_calculation_total.Dock = DockStyle.Fill;
                tlp_calculation_total.Visible = true;
                tlp_promo_code.Visible = false;
                tlp_discount_calc.Visible = false;
            }
            else
            {
                Control controlToRemove = tlp_calculation_main.GetControlFromPosition(0, 0);
                tlp_calculation_main.Controls.Remove(controlToRemove);
                tlp_calculation_main.Controls.Add(tlp_promo_code, 0, 0);
                tlp_promo_code.Dock = DockStyle.Fill;
                tlp_promo_code.Visible = true;
                tlp_calculation_total.Visible = false;
                tlp_discount_calc.Visible = false;
            }
  
            textb_shape();
        }

        private void btn_save_promo_Click(object sender, EventArgs e)
        {
            if(txtpromocode.Text=="" || txtpromocode.Text == null)
            {
                CMessageBox.Show("Enter Promo code ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtpromocode.Focus();
                return;
            }
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = "pm.MinBillAmt Field1,pm.StartDate as StartDate,pm.EnDDate EndDate,pm.MaxDiscount Field2,pd.PromoDId Field3, pm.PromoMId Field4,pm.DiscountPer Field5";
            dmnSearch.Table = "tblPromoMaster  pm LEFT JOIN tblPromoDetails pd on pm.PromoMId =pd.PromoMId";
            dmnSearch.WhereNdGroupBy = "where pm.Status=1 and pd.code LIKE '%" + txtpromocode.Text + "%'";
            dmnSearch.OrderBy = "";
            var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
            if(gridSource.Count>0)
            {
                if (string.IsNullOrEmpty(txtdiscountamt_promo.Text))
                    txtdiscountamt_promo.Text = "0";
                DateTime startDate = Convert.ToDateTime(gridSource[0].StartDate);
                DateTime endDate = Convert.ToDateTime(gridSource[0].EndDate);
                DateTime currentDate = DateTime.Now;
                if (currentDate >= startDate && currentDate <= endDate)
                {
                    CMessageBox.Show("Promo code valid.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (Convert.ToDecimal(txtnetamount.Text) < Convert.ToDecimal(gridSource[0].Field1))
                    {
                        CMessageBox.Show("The net amount must be greater than the applicable total for the promo code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        txtpromocode.Enabled = false;
                        btn_save_promo.Enabled = false;
                        decimal disAmnt = (Convert.ToDecimal(gridSource[0].Field5) * Convert.ToDecimal(txtnetamount.Text)) / 100;
                        if (disAmnt < Convert.ToDecimal(gridSource[0].Field2))
                        {                       
                            decimal ftotal = Convert.ToDecimal(txtnetamount.Text) - disAmnt;
                            txtdiscountamt_promo.Text = disAmnt.ToString("N");
                            txtdisamt_promo.Text = Convert.ToString(ftotal);
                        }
                        else
                        {
                            decimal ftotal = Convert.ToDecimal(txtnetamount.Text) - Convert.ToDecimal(gridSource[0].Field2);
                            txtdiscountamt_promo.Text = Convert.ToString(gridSource[0].Field2);
                            txtdisamt_promo.Text = Convert.ToString(ftotal);
                        }
                        Promodetailsid= Convert.ToInt32(gridSource[0].Field3);
                        Promomasterid = Convert.ToInt32(gridSource[0].Field4);
                        netAmount();
                    }
                }
                else
                {
                    CMessageBox.Show("Promo code has expired.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }                    
            }
        
        }
        public void finaltotalqty()
        {
            try
            {
                int sum = 0;
                int n = gridSourcemaster.Count;
                for (int i = 0; i < n; i++)
                {
                    if (Convert.ToInt32(gridSourcemaster[i].Quantity) != null && Convert.ToInt32(gridSourcemaster[i].Quantity) >0 && Convert.ToInt32(gridSourcemaster[i].Status)==3)
                        sum = sum + Convert.ToInt32(gridSourcemaster[i].Quantity);
                }
                finalqty = Convert.ToInt32(txttotalQTY.Text) + sum;
            }
            catch (Exception ex)
            {

            }
        }
        public void FinalTax()
        {
            try
            {
                TotalTax = 0; TotalIGST = 0; TotalCGST = 0; TotalSGST = 0; TotalVAT = 0; Totaldis = 0;
                for (int i = 0; i < gridSourcemaster.Count; i++)
                {
                    if (Convert.ToInt32(gridSourcemaster[i].Status) == 3)
                    {
                        TotalIGST = TotalIGST + Convert.ToDecimal(gridSourcemaster[i].IGSTAmount);
                        TotalCGST = TotalCGST + Convert.ToDecimal(gridSourcemaster[i].CGSTAmount);
                        TotalSGST = TotalSGST + Convert.ToDecimal(gridSourcemaster[i].SGSTAmount);
                        TotalVAT = TotalVAT + Convert.ToDecimal(gridSourcemaster[i].VATAmount);
                        //Totaldis = Totaldis + Convert.ToDecimal(dgv_exitsting_product.Rows[i].Cells[pDisAmnt].Value);
                    }
                }
                TotalTax = TotalIGST + TotalVAT;
            }
            catch (Exception ex)
            {
            }
        }
        public void Combined_Tables()
        {
            try
            {
                BllSearch<DmnOrderMaster> bllSearchmaster = new BllSearch<DmnOrderMaster>();
                DmnSearch dmnSearchmaster = new DmnSearch();
                dgv_combined.Rows.Clear();
                for (int k = 0; k < checkedtableNames.Count; k++)
                {
                    dmnSearchmaster.Select = "*";
                    dmnSearchmaster.Table = "tblOrderMaster om left join tblOrderDetails od on od.OrderMasterId = om.OrderMasterId";
                    dmnSearchmaster.WhereNdGroupBy = "where  om.Status=1 and om.OrderMasterId=" + checkedtableNames[k].Ordermasterid;
                    dmnSearchmaster.OrderBy = "";
                    var gridSourcemaster_combined = bllSearchmaster.GetSearchData("prdSearch", dmnSearchmaster);

                    if (gridSourcemaster_combined.Count > 0)
                    {
                        for (int i = 0; i < gridSourcemaster_combined.Count; i++)
                        {
                            if (Convert.ToInt32(gridSourcemaster_combined[i].Status) == 3)
                            {
                                bool valueExists = false;
                                foreach (DataGridViewRow row in dgv_combined.Rows)
                                {
                                    if (Convert.ToInt32(row.Cells[Ctabletype].Value) == gridSourcemaster_combined[i].TableType
                                        && Convert.ToInt32(row.Cells[citemid].Value) == gridSourcemaster_combined[i].ItemId)
                                    {
                                        valueExists = true;
                                        row.Cells[cqty].Value = Convert.ToInt32(row.Cells[cqty].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].Quantity);
                                        row.Cells[cdisper].Value = Convert.ToInt32(row.Cells[cdisper].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].DiscountPer);
                                        row.Cells[cdisamt].Value = Convert.ToDecimal(row.Cells[cdisamt].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].DiscountAmount);
                                        row.Cells[ctaxper].Value = Convert.ToDecimal(row.Cells[ctaxper].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].TaxPer);
                                        row.Cells[ctaxamt].Value = Convert.ToDecimal(row.Cells[ctaxamt].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].TaxAmount);
                                        row.Cells[camount].Value = Convert.ToDecimal(row.Cells[camount].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].Amount);
                                        row.Cells[ctotal].Value = Convert.ToDecimal(row.Cells[ctotal].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].NetAmount);
                                        row.Cells[cigstamt].Value = Convert.ToDecimal(row.Cells[cigstamt].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].IGSTAmount);
                                        row.Cells[CCgstamt].Value = Convert.ToDecimal(row.Cells[CCgstamt].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].CGSTAmount);
                                        row.Cells[cSgstamt].Value = Convert.ToDecimal(row.Cells[cSgstamt].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].SGSTAmount);
                                        row.Cells[cvatamt].Value = Convert.ToDecimal(row.Cells[cvatamt].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].VATAmount);
                                        break;
                                    }
                                }
                                if (!valueExists)
                                {
                                    int rowIndex = dgv_combined.Rows.Add();
                                    dgv_combined.Rows[rowIndex].Cells[vname].Value = gridSourcemaster_combined[i].ItemName;
                                    dgv_combined.Rows[rowIndex].Cells[ciunit].Value = gridSourcemaster_combined[i].UnitId;
                                    dgv_combined.Rows[rowIndex].Cells[cqty].Value = gridSourcemaster_combined[i].Quantity;
                                    dgv_combined.Rows[rowIndex].Cells[cdisper].Value = gridSourcemaster_combined[i].DiscountPer;
                                    dgv_combined.Rows[rowIndex].Cells[cdisamt].Value = gridSourcemaster_combined[i].DiscountAmount;
                                    dgv_combined.Rows[rowIndex].Cells[ctaxper].Value = gridSourcemaster_combined[i].TaxPer;
                                    dgv_combined.Rows[rowIndex].Cells[ctaxamt].Value = gridSourcemaster_combined[i].TaxAmount;
                                    dgv_combined.Rows[rowIndex].Cells[camount].Value = gridSourcemaster_combined[i].Amount;
                                    dgv_combined.Rows[rowIndex].Cells[ctotal].Value = gridSourcemaster_combined[i].NetAmount;
                                    dgv_combined.Rows[rowIndex].Cells[citemid].Value = gridSourcemaster_combined[i].ItemId;
                                    dgv_combined.Rows[rowIndex].Cells[cpridno].Value = gridSourcemaster_combined[i].PrId;
                                    dgv_combined.Rows[rowIndex].Cells[ctax].Value = gridSourcemaster_combined[i].Tax;
                                    dgv_combined.Rows[rowIndex].Cells[ctaxtype].Value = gridSourcemaster_combined[i].TaxType;
                                    dgv_combined.Rows[rowIndex].Cells[Csrate].Value = gridSourcemaster_combined[i].Srate;
                                    dgv_combined.Rows[rowIndex].Cells[Cmrp].Value = gridSourcemaster_combined[i].MRP;
                                    dgv_combined.Rows[rowIndex].Cells[Comproducttypeno].Value = gridSourcemaster_combined[i].ProductType;
                                    dgv_combined.Rows[rowIndex].Cells[cigstamt].Value = gridSourcemaster_combined[i].IGSTAmount;
                                    dgv_combined.Rows[rowIndex].Cells[CCgstamt].Value = gridSourcemaster_combined[i].CGSTAmount;
                                    dgv_combined.Rows[rowIndex].Cells[cSgstamt].Value = gridSourcemaster_combined[i].SGSTAmount;
                                    dgv_combined.Rows[rowIndex].Cells[cvatamt].Value = gridSourcemaster_combined[i].VATAmount;
                                    dgv_combined.Rows[rowIndex].Cells[cmarketprice].Value = gridSourcemaster_combined[i].MarketPrice;
                                    dgv_combined.Rows[rowIndex].Cells[Cbarcode].Value = gridSourcemaster_combined[i].ItemBarCode;
                                    dgv_combined.Rows[rowIndex].Cells[Ctabletype].Value = gridSourcemaster_combined[i].TableType;
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void paybillcal(int payid)
        {
            try
            {
                if (MandatoryCheck_printbill())
                {
                    setperfixsale();
                    int countflag = 0;
                    DmnSalemaster dmnsalemaster = new DmnSalemaster();
                    DmnSaledetails dmnsaledetails = new DmnSaledetails();
                    BllSale bllSales = new BllSale();
                    if (string.IsNullOrEmpty(txtdis_per.Text))
                        txtdis_per.Text = "0";
                    if (string.IsNullOrEmpty(txtdiscountamt_promo.Text))
                        txtdiscountamt_promo.Text = "0";
                    if (string.IsNullOrEmpty(lblorderid.Text))
                        lblorderid.Text = "0";
                    if (checkedtableNames.Count > 0)
                    {
                        #region CoBined  Save Salemaster And Saledetails
                        int sum = 0;
                        int n = dgv_combined.RowCount;
                        for (int i = 0; i < n; i++)
                        {
                            if (Convert.ToInt32(dgv_combined.Rows[i].Cells[cqty].Value) != null && Convert.ToInt32(dgv_combined.Rows[i].Cells[cqty].Value) > 0)
                                sum = sum + Convert.ToInt32(dgv_combined.Rows[i].Cells[cqty].Value);
                        }
                        finalqty = Convert.ToInt32(txttotalQTY.Text) + sum;

                        dmnsalemaster.EntryNo = txtinvoiceno.Text;

                        dmnsalemaster.Name = Convert.ToString(txtcusname.Text);
                        dmnsalemaster.ContactNo = Convert.ToString(txtph.Text);
                        dmnsalemaster.Email = Convert.ToString(txtemailid.Text);
                        dmnsalemaster.Address = Convert.ToString(txtaddress.Text);
                        if (rb_male.Checked)
                            dmnsalemaster.Gender = 1;
                        else
                            dmnsalemaster.Gender = 2;

                        dmnsalemaster.TotQty = Convert.ToDecimal(finalqty);
                        dmnsalemaster.TotalAmnt = Convert.ToDecimal(txtGrandTotal.Text);
                        dmnsalemaster.GrandTot = Convert.ToDecimal(txtGrandTotal.Text);
                        dmnsalemaster.DisPer = Convert.ToDecimal(txtdis_per.Text);
                        dmnsalemaster.DisAmnt = Convert.ToDecimal(txtdis_amt.Text);
                        dmnsalemaster.OaAmount = Convert.ToDecimal(txtotheraddamt.Text);
                        dmnsalemaster.OaDesc = Convert.ToString(txtother_descr.Text);
                        dmnsalemaster.OdAmount = Convert.ToDecimal(txtotherdedamt.Text);
                        dmnsalemaster.OdDesc = Convert.ToString(txtother_descr.Text);
                        dmnsalemaster.NetAmount = Convert.ToDecimal(txtnetamount.Text);
                        dmnsalemaster.RcvAmnt = Convert.ToDecimal(txtpaidamount.Text);
                        dmnsalemaster.BalAmnt = Convert.ToDecimal(txtrecvamount.Text);
                        dmnsalemaster.OrderMasterid = Convert.ToInt32(0);//lblorderid.Text
                        dmnsalemaster.PromoAmount = Convert.ToDecimal(txtdiscountamt_promo.Text);
                        dmnsalemaster.AdvanceAmount = Convert.ToDecimal(txtadvance.Text);
                        dmnsalemaster.ReservationTotal = ReservationTotal;
                        dmnsalemaster.Tid = tableId;
                        dmnsalemaster.PayId = payid;

                        if (Promomasterid > 0 && Promodetailsid > 0)
                        {
                            dmnsalemaster.PromoMId = Promomasterid;
                            dmnsalemaster.PromoDId = Promodetailsid;
                            dmnsalemaster.Promostatus = 1;
                        }
                        else
                        {
                            dmnsalemaster.PromoMId = 0;
                            dmnsalemaster.PromoDId = 0;
                            dmnsalemaster.Promostatus = 0;
                        }

                        TotalTax = 0; TotalIGST = 0; TotalCGST = 0; TotalSGST = 0; TotalVAT = 0; Totaldis = 0;
                        for (int i = 0; i < dgv_combined.RowCount; i++)
                        {
                            if (Convert.ToInt32(dgv_combined.Rows[i].Cells[cigstamt].Value) >0)
                            {
                                TotalIGST = TotalIGST + Convert.ToDecimal(dgv_combined.Rows[i].Cells[cigstamt].Value);
                                TotalCGST = TotalCGST + Convert.ToDecimal(dgv_combined.Rows[i].Cells[CCgstamt].Value);
                                TotalSGST = TotalSGST + Convert.ToDecimal(dgv_combined.Rows[i].Cells[cSgstamt].Value);
                                TotalVAT = TotalVAT + Convert.ToDecimal(dgv_combined.Rows[i].Cells[cvatamt].Value);
                                //Totaldis = Totaldis + Convert.ToDecimal(dgv_exitsting_product.Rows[i].Cells[pDisAmnt].Value);
                            }
                        }
                        TotalTax = TotalIGST + TotalVAT;

                        //dmnsalemaster.TotalDiscount = Convert.ToDecimal(txtDiscAmnt.Text);
                        dmnsalemaster.TotalTax = TotalTax; 
                        dmnsalemaster.TotalIGST = TotalIGST;
                        dmnsalemaster.TotalCGST = TotalIGST / 2;
                        dmnsalemaster.TotalSGST = TotalIGST / 2;
                        dmnsalemaster.TotalVAT = TotalVAT;
                        dmnsalemaster.mDeviceIP = " [ " + System.Environment.MachineName + " ]  -" + Dns.GetHostByName(System.Environment.MachineName).AddressList[0].ToString();
                        ////dmnsalemaster.mDevice = clsGeneral.FetchMacId();
                        //dmnsalemaster.Remark = txtRemark.Text; 
                        dmnsalemaster.BranchId = FrmDashboard.BranchId;
                        dmnsalemaster.SalesMasterId = Convert.ToInt32("0");
                        dmnsalemaster.CreatedBy = FrmDashboard.UserId;
                        dmnsalemaster.Status = 1;
                        dmnsalemaster.CombinedOrderBill_TYPE = 1;
                        var InsertStatusP = bllSales.AddEditData("prdInsert_Upd_SalesMaster", dmnsalemaster);
                        if (InsertStatusP.SalesMasterId != 0)
                        {
                            if (payid == 11)
                            {
                                DmnPayment dmnpayment = new DmnPayment();
                                Bllpayment bllpay = new Bllpayment();
                                for (int i = 0; i < dgv_part.RowCount; i++)
                                {
                                    dmnpayment.PayId = 0;
                                    dmnpayment.OrderMasterID = Convert.ToInt32(label18.Text);
                                    dmnpayment.SalemasterId = Convert.ToInt32(InsertStatusP.SalesMasterId);
                                    dmnpayment.PayType = Convert.ToInt32(dgv_part.Rows[i].Cells[4].Value);
                                    dmnpayment.Amount = Convert.ToDecimal(dgv_part.Rows[i].Cells[1].Value);
                                    dmnpayment.Tid = tableId;
                                    dmnpayment.BranchId = FrmDashboard.BranchId;
                                    dmnpayment.CreatedBy = FrmDashboard.UserId;
                                    dmnpayment.Status = 1;
                                    var InsertStatus = bllpay.AddEditPayment("prdInsert_Upd_Payment", dmnpayment);
                                }
                            }

                            if (dgv_combined.RowCount > 0)
                            {
                                for (int k = 0; k < checkedtableNames.Count; k++)
                                {
                                    DmnCombinedOrderBill CombinedOrderBill = new DmnCombinedOrderBill();
                                    BllCombinedOrderBill bllCombinedOrderBill = new BllCombinedOrderBill();
                                    CombinedOrderBill.Cbid = 0;
                                    CombinedOrderBill.SalemasterId = Convert.ToInt32(InsertStatusP.SalesMasterId); ;
                                    CombinedOrderBill.OrderMasterID = Convert.ToInt32(checkedtableNames[k].Ordermasterid); 
                                    CombinedOrderBill.Tid = Convert.ToInt32(checkedtableNames[k].Id);
                                    CombinedOrderBill.BranchId = FrmDashboard.BranchId;
                                    CombinedOrderBill.CreatedBy = FrmDashboard.UserId;
                                    CombinedOrderBill.Status = 1;
                                    var duration = DateTime.Now - OrderDateTime;
                                    int min = duration.Seconds;
                                    CombinedOrderBill.TimeDuration = min;
                                    var InsertStatus_cbill = bllCombinedOrderBill.AddCombinedOrderBill("prdInsert_CombinedOrderBill", CombinedOrderBill);

                                }

                                    for (int i = 0; i < dgv_combined.RowCount; i++)
                                {
                                    if (Convert.ToInt32(dgv_combined.Rows[i].Cells[citemid].Value) > 0)
                                    {
                                        txtsalemasterid.Text = Convert.ToString(InsertStatusP.SalesMasterId);
                                        dmnsaledetails.SalesMasterId = Convert.ToInt32(InsertStatusP.SalesMasterId);
                                        dmnsaledetails.SalesRate = Convert.ToDecimal(dgv_combined.Rows[i].Cells[Csrate].Value);
                                        dmnsaledetails.ItemBarcode = Convert.ToString(dgv_combined.Rows[i].Cells[Cbarcode].Value);
                                        dmnsaledetails.ItemName = Convert.ToString(dgv_combined.Rows[i].Cells[vname].Value);
                                        dmnsaledetails.UnitId = Convert.ToInt32(dgv_combined.Rows[i].Cells[ciunit].Value);
                                        dmnsaledetails.Quantity = Convert.ToInt32(dgv_combined.Rows[i].Cells[cqty].Value);
                                        dmnsaledetails.DiscountPer = Convert.ToDecimal(dgv_combined.Rows[i].Cells[cdisper].Value);
                                        dmnsaledetails.DiscountAmount = Convert.ToDecimal(dgv_combined.Rows[i].Cells[cdisamt].Value);
                                        dmnsaledetails.TaxPer = Convert.ToDecimal(dgv_combined.Rows[i].Cells[ctaxper].Value);
                                        dmnsaledetails.TaxAmount = Convert.ToDecimal(dgv_combined.Rows[i].Cells[ctaxamt].Value);
                                        dmnsaledetails.Amount = Convert.ToDecimal(dgv_combined.Rows[i].Cells[camount].Value);
                                        dmnsaledetails.NetAmount = Convert.ToDecimal(dgv_combined.Rows[i].Cells[ctotal].Value);
                                        dmnsaledetails.itemid = Convert.ToInt32(dgv_combined.Rows[i].Cells[citemid].Value);
                                        dmnsaledetails.Prid = Convert.ToInt32(dgv_combined.Rows[i].Cells[cpridno].Value);
                                        dmnsaledetails.Tax = Convert.ToString(dgv_combined.Rows[i].Cells[ctax].Value);
                                        dmnsaledetails.TaxType = Convert.ToString(dgv_combined.Rows[i].Cells[ctaxtype].Value);
                                        dmnsaledetails.MRP = Convert.ToDecimal(dgv_combined.Rows[i].Cells[Cmrp].Value);
                                        dmnsaledetails.ProductType = Convert.ToString(dgv_combined.Rows[i].Cells[Comproducttypeno].Value);
                                        dmnsaledetails.IGSTAmount = Convert.ToDecimal(dgv_combined.Rows[i].Cells[cigstamt].Value);
                                        dmnsaledetails.CGSTAmount = Convert.ToDecimal(dgv_combined.Rows[i].Cells[CCgstamt].Value);
                                        dmnsaledetails.SGSTAmount = Convert.ToDecimal(dgv_combined.Rows[i].Cells[cSgstamt].Value);
                                        dmnsaledetails.VatAmount = Convert.ToDecimal(dgv_combined.Rows[i].Cells[cvatamt].Value);
                                        dmnsaledetails.MarketPrice = Convert.ToDecimal(dgv_combined.Rows[i].Cells[cmarketprice].Value);
                                        dmnsaledetails.TableType = Convert.ToInt32(dgv_combined.Rows[i].Cells[Ctabletype].Value);
                                        dmnsaledetails.BranchId = FrmDashboard.BranchId;
                                        dmnsaledetails.CreatedBy = 1;
                                        dmnsaledetails.Status = 1;
                                        dmnsaledetails.SalesDetailsId = Convert.ToInt32("0");
                                        var InsertStatus = bllSales.AddEditDataDet("prdInsert_Upd_SalesDetails", dmnsaledetails);
                                        if (InsertStatus == null)
                                        {
                                            MessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            break;
                                        }
                                        else
                                            countflag = countflag + 1;
                                    }
                                }

                            }
                            if (dgv_combined.RowCount == countflag)
                            {
                                if (payid != 12)
                                {
                                    FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(InsertStatusP.SalesMasterId, payid, 0);
                                    frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                                    frmSalesInvoice.ShowDialog();
                                    CMessageBox.Show("Transaction Completed Sucessfully", "Sucessfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                                    {
                                        FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                                        _instanceFrmdash.LoadTableStaus(0);
                                    }
                                    //ClearAll();
                                    this.Close();
                                }
                            }

                        }

                   #endregion
                    }
                    else
                    {
                        #region Normal Save Salemaster And Saledetails
                        finaltotalqty();
                        dmnsalemaster.EntryNo = txtinvoiceno.Text;
                        dmnsalemaster.TotQty = Convert.ToDecimal(finalqty);
                        dmnsalemaster.TotalAmnt = Convert.ToDecimal(txtGrandTotal.Text);
                        dmnsalemaster.GrandTot = Convert.ToDecimal(txtGrandTotal.Text);
                        dmnsalemaster.DisPer = Convert.ToDecimal(txtdis_per.Text);
                        dmnsalemaster.DisAmnt = Convert.ToDecimal(txtdis_amt.Text);
                        dmnsalemaster.OaAmount = Convert.ToDecimal(txtotheraddamt.Text);

                        dmnsalemaster.Name = Convert.ToString(txtcusname.Text);
                        dmnsalemaster.ContactNo = Convert.ToString(txtph.Text);
                        dmnsalemaster.Email = Convert.ToString(txtemailid.Text);
                        dmnsalemaster.Address = Convert.ToString(txtaddress.Text);
                        if(rb_male.Checked)
                            dmnsalemaster.Gender = 1;
                        else
                            dmnsalemaster.Gender = 2;

                        if (txtother_descr.Text == "Description Type Here ...")
                            dmnsalemaster.OaDesc ="";
                        else
                            dmnsalemaster.OaDesc = Convert.ToString(txtother_descr.Text);
                        dmnsalemaster.OdAmount = Convert.ToDecimal(txtotherdedamt.Text);

                        if (txtother_descr.Text == "Description Type Here ...")
                            dmnsalemaster.OdDesc = "";
                        else
                            dmnsalemaster.OdDesc = Convert.ToString(txtother_descr.Text);

                        dmnsalemaster.NetAmount = Convert.ToDecimal(txtnetamount.Text);
                        dmnsalemaster.RcvAmnt = Convert.ToDecimal(txtpaidamount.Text);
                        dmnsalemaster.BalAmnt = Convert.ToDecimal(txtrecvamount.Text);
                        dmnsalemaster.OrderMasterid = Convert.ToInt32(lblorderid.Text);
                        dmnsalemaster.PromoAmount = Convert.ToDecimal(txtdiscountamt_promo.Text);
                        dmnsalemaster.Tid = tableId;
                        dmnsalemaster.PayId = payid;

                        if (Promomasterid > 0 && Promodetailsid > 0)
                        {
                            dmnsalemaster.PromoMId = Promomasterid;
                            dmnsalemaster.PromoDId = Promodetailsid;
                            dmnsalemaster.Promostatus = 1;
                        }
                        else
                        {
                            dmnsalemaster.PromoMId = 0;
                            dmnsalemaster.PromoDId = 0;
                            dmnsalemaster.Promostatus = 0;
                        }
                        //dmnsalemaster.Remark = txtRemark.Text; 
                        dmnsalemaster.BranchId = FrmDashboard.BranchId;
                        dmnsalemaster.SalesMasterId = Convert.ToInt32("0");
                        dmnsalemaster.CreatedBy = FrmDashboard.UserId;
                        dmnsalemaster.Status = 1;
                        dmnsalemaster.CombinedOrderBill_TYPE = 0;
                        dmnsalemaster.BookmasterId = Bookingmasterid;
                        dmnsalemaster.AdvanceAmount = Convert.ToDecimal(txtadvance.Text);
                        dmnsalemaster.ReservationTotal = ReservationTotal;
                        FinalTax();
                        //dmnsalemaster.TotalDiscount = Convert.ToDecimal(txtDiscAmnt.Text);
                        dmnsalemaster.TotalTax = TotalTax;
                        dmnsalemaster.TotalIGST = TotalIGST;
                        dmnsalemaster.TotalCGST = TotalCGST;
                        dmnsalemaster.TotalSGST = TotalSGST;
                        dmnsalemaster.TotalVAT = TotalVAT;
                        dmnsalemaster.mDeviceIP = " [ " + System.Environment.MachineName + " ]  -" + Dns.GetHostByName(System.Environment.MachineName).AddressList[0].ToString();
                        //
                        //dmnsalemaster.mDevice = clsGeneral.FetchMacId();

                        var duration = DateTime.Now - OrderDateTime;
                        int min = duration.Seconds;
                        dmnsalemaster.TimeDuration = min;

                        var InsertStatusP = bllSales.AddEditData("prdInsert_Upd_SalesMaster", dmnsalemaster);
                        #region details
                        if (InsertStatusP.SalesMasterId != 0)
                        {
                            if (payid == 11)
                            {
                                DmnPayment dmnpayment = new DmnPayment();
                                Bllpayment bllpay = new Bllpayment();
                                for (int i = 0; i < dgv_part.RowCount; i++)
                                {
                                    dmnpayment.PayId = 0;
                                    dmnpayment.OrderMasterID = Convert.ToInt32(label18.Text);
                                    dmnpayment.SalemasterId = Convert.ToInt32(InsertStatusP.SalesMasterId);
                                    dmnpayment.PayType = Convert.ToInt32(dgv_part.Rows[i].Cells[4].Value);
                                    dmnpayment.Amount = Convert.ToDecimal(dgv_part.Rows[i].Cells[1].Value);
                                    dmnpayment.Tid = tableId;
                                    dmnpayment.BranchId = FrmDashboard.BranchId;
                                    dmnpayment.CreatedBy = FrmDashboard.UserId;
                                    dmnpayment.Status = 1;
                                    var InsertStatus = bllpay.AddEditPayment("prdInsert_Upd_Payment", dmnpayment);
                                }
                            }

                            if (gridSourcemaster.Count > 0)
                            {
                                for (int i = 0; i < gridSourcemaster.Count; i++)
                                {
                                    if (Convert.ToInt32(gridSourcemaster[i].Status) == 3)
                                    {
                                        dmnsaledetails.SalesRate = Convert.ToDecimal(gridSourcemaster[i].Srate);
                                        dmnsaledetails.ItemBarcode = Convert.ToString(gridSourcemaster[i].ItemBarCode);
                                        txtsalemasterid.Text = Convert.ToString(InsertStatusP.SalesMasterId);
                                        dmnsaledetails.SalesMasterId = Convert.ToInt32(InsertStatusP.SalesMasterId);
                                        dmnsaledetails.ItemName = Convert.ToString(gridSourcemaster[i].ItemName);
                                        dmnsaledetails.UnitId = Convert.ToInt32(gridSourcemaster[i].UnitId);
                                        dmnsaledetails.Quantity = Convert.ToInt32(gridSourcemaster[i].Quantity);
                                        dmnsaledetails.DiscountPer = Convert.ToDecimal(gridSourcemaster[i].DiscountPer);
                                        dmnsaledetails.DiscountAmount = Convert.ToDecimal(gridSourcemaster[i].DiscountAmount);
                                        dmnsaledetails.TaxPer = Convert.ToDecimal(gridSourcemaster[i].TaxPer);
                                        dmnsaledetails.TaxAmount = Convert.ToDecimal(gridSourcemaster[i].TaxAmount);
                                        dmnsaledetails.Amount = Convert.ToDecimal(gridSourcemaster[i].Amount);
                                        dmnsaledetails.NetAmount = Convert.ToDecimal(gridSourcemaster[i].NetAmount);
                                        dmnsaledetails.itemid = Convert.ToInt32(gridSourcemaster[i].ItemId);
                                        dmnsaledetails.Prid = Convert.ToInt32(gridSourcemaster[i].PrId);
                                        dmnsaledetails.Tax = Convert.ToString(gridSourcemaster[i].Tax);
                                        dmnsaledetails.TaxType = Convert.ToString(gridSourcemaster[i].TaxType);
                                        dmnsaledetails.MRP = Convert.ToDecimal(gridSourcemaster[i].MRP);
                                        dmnsaledetails.ProductType = Convert.ToString(gridSourcemaster[i].ProductType);

                                        dmnsaledetails.IGSTAmount = Convert.ToDecimal(gridSourcemaster[i].IGSTAmount);
                                        dmnsaledetails.CGSTAmount = Convert.ToDecimal(gridSourcemaster[i].CGSTAmount);
                                        dmnsaledetails.SGSTAmount = Convert.ToDecimal(gridSourcemaster[i].SGSTAmount);
                                        dmnsaledetails.VatAmount = Convert.ToDecimal(gridSourcemaster[i].VATAmount);
                                        dmnsaledetails.MarketPrice = Convert.ToDecimal(gridSourcemaster[i].MarketPrice);
                                        dmnsaledetails.TableType = Convert.ToInt32(gridSourcemaster[i].TableType);
                                        dmnsaledetails.BranchId = FrmDashboard.BranchId;
                                        dmnsaledetails.CreatedBy = 1;
                                        dmnsaledetails.Status = 1;

                                        dmnsaledetails.SalesDetailsId = Convert.ToInt32("0");
                                        var InsertStatus = bllSales.AddEditDataDet("prdInsert_Upd_SalesDetails", dmnsaledetails);
                                        if (InsertStatus == null)
                                        {
                                            MessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            break;
                                        }
                                        else
                                            countflag = countflag + 1;
                                    }
                                }

                            }
                            if (gridSourcemaster.Count == countflag)
                            {
                                if (payid != 12)
                                {
                                    FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(InsertStatusP.SalesMasterId, payid, 0);
                                    frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                                    frmSalesInvoice.ShowDialog();
                                    CMessageBox.Show("Transaction Completed Sucessfully", "Sucessfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                                    {
                                        FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                                        _instanceFrmdash.LoadTableStaus(0);
                                    }
                                    //ClearAll();
                                    this.Close();
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    


    public void setperfixsale()
        {
            try
            {
                BllSearch<DmnSalemaster> bllSearch = new BllSearch<DmnSalemaster>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "SalesPrefix ,SalesSerialNo";
                dmnSearch.Table = " tblHotelRegistration";
                dmnSearch.WhereNdGroupBy = " Where BranchId = " + FrmDashboard.BranchId;
                dmnSearch.OrderBy = "";
                var gridSource1 = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource1.Count > 0)
                {
                    txtinvoiceno.Text = gridSource1[0].SalesPrefix + FrmDashboard.BranchId + DateTime.Now.Year.ToString() + gridSource1[0].SalesSerialNo;//PERRET20221001
                }
            }
            catch (Exception ex)
            { }

        }
        private void btn_print_bill_Click(object sender, EventArgs e)
        {
            if (combined_Status)
            {

                int panelWidthPercentage = 32;
                int panelheightPercentage = 36;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                flp_payment_option.Size = new Size(panelWidth, panelHeight);
                flp_payment_option.Location = new Point(this.ClientSize.Width / 2 - flp_payment_option.Size.Width / 2, this.ClientSize.Height / 2 - flp_payment_option.Size.Height / 2);
                flp_payment_option.Visible = true;
                tlp_tableorder.Enabled = false;
            }
            else
            {
                int panelWidthPercentage = 43;
                int panelheightPercentage = 36;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                flp_payment_option.Size = new Size(panelWidth, panelHeight);
                flp_payment_option.Location = new Point(this.ClientSize.Width / 2 - flp_payment_option.Size.Width / 2, this.ClientSize.Height / 2 - flp_payment_option.Size.Height / 2);
                flp_payment_option.Visible = true;
                tlp_tableorder.Enabled = false;
            }

        }
        public bool MandatoryCheck_printbill()
        {
            int count = 0;
            int rowno = 0;
            //for(int i=0;i< Dgv_Selected_Item.RowCount;i++)
            //{
            //    if (Dgv_Selected_Item.Rows[i].Cells[iitemid] != null)
            //    {
            //        count = count + 1;
            //        rowno = i;
            //        break;
            //    }

            //}
            //if(count==1)
            //{
            //    string tlpname = "";
            //    CMessageBox.Show("Product cart is Empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    if (Convert.ToInt32(Dgv_Selected_Item.Rows[rowno].Cells[ipridno])>0)
            //         tlpname = "tlp_seletedItem_col" + Dgv_Selected_Item.Rows[rowno].Cells[ipridno];
            //    else
            //         tlpname = "tlp_seletedItem_col" + Dgv_Selected_Item.Rows[rowno].Cells[iitemid];

            //    TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
            //    if (tlp_sele != null)
            //    {     
            //        tlp_sele.Focus();
            //    }
            //    return false;
            //}


            //if (string.IsNullOrEmpty(txtordermasterid.Text))
            //    txtordermasterid.Text = "0";

            //if (string.IsNullOrEmpty(txtpaidamount.Text))
            //{
            //    CMessageBox.Show("Please Enter Paid Amount Is Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtpaidamount.Focus();
            //    return false;
            //}
            //else if (tlp_seletedItem_main.Controls.Count == 0)
            //{
            //    CMessageBox.Show("Product cart is Empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            return true;
        }

        private void btn_grid_existing_close_Click(object sender, EventArgs e)
        {
            tlp_grid_existing_products.Visible = false;
        }

        private void btn_show_existing_product_Click(object sender, EventArgs e)
        {
            if (tlp_grid_existing_products.Visible)
            {
                tlp_grid_existing_products.Visible = false;
            }
            else
            {
                tlp_grid_existing_products.Visible = true;

                int centerX = this.ClientSize.Width / 2 - tlp_grid_existing_products.Size.Width / 2;
                int centerY = this.ClientSize.Height / 2 - tlp_grid_existing_products.Size.Height / 2;

                // Get the screen resolution
                Screen screen = Screen.FromControl(this);
                int screenWidth = screen.Bounds.Width;
                int screenHeight = screen.Bounds.Height;

                // Adjust the position based on screen resolution
                int adjustedX = (screenWidth - tlp_grid_existing_products.Size.Width) / 2;
                int adjustedY = (screenHeight - tlp_grid_existing_products.Size.Height) / 2;

                // Set the location of tlp_grid_existing_products
                tlp_grid_existing_products.Location = new Point(adjustedX, adjustedY);
            }

        }

        private void btn_close_viewmore_Click(object sender, EventArgs e)
        {
            pnlproductviewmore.Visible = false;
        }

        private void btn_billtype_Click(object sender, EventArgs e)
        {

        }

        private void btncash_Click(object sender, EventArgs e)
        {
            try
            {
                int payid = 0, panelWidthPercentage = 0, panelheightPercentage = 0, panelWidth = 0, panelHeight = 0;
                Button clickedButton = sender as Button;
                switch (clickedButton.Name)
                {
                    case "btncash":
                        payid = Convert.ToInt32(PaymentType.CASH);
                        break;
                    case "btndebit":
                        payid = Convert.ToInt32(PaymentType.DEBITCARD);
                        break;
                    case "btnupi":
                        payid = Convert.ToInt32(PaymentType.UPI);
                        break;
                    case "btnnetbanking":
                        payid = Convert.ToInt32(PaymentType.BANKINGONLINE);
                        break;
                    case "btncreditcard":
                        payid = Convert.ToInt32(PaymentType.CREDIT);
                        break;
                    case "btnothers":
                        payid = Convert.ToInt32(PaymentType.OTHERS);
                        break;
                    case "Btnpartpayment":
                        payid = Convert.ToInt32(PaymentType.PART);

                        dgv_part.BorderStyle = BorderStyle.None;
                        dgv_part.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                        dgv_part.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                        dgv_part.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                        dgv_part.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                        dgv_part.BackgroundColor = Color.White;

                        dgv_part.EnableHeadersVisualStyles = false;
                        dgv_part.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                        dgv_part.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                        dgv_part.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                         panelWidthPercentage = 50;
                         panelheightPercentage = 70;
                         panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                         panelHeight = (int)(this.Height * panelheightPercentage / 100);
                        pnl_partpayment.Size = new Size(panelWidth, panelHeight);

                        pnl_partpayment.Location = new Point(this.ClientSize.Width / 2 - pnl_partpayment.Size.Width / 2, this.ClientSize.Height / 2 - pnl_partpayment.Size.Height / 2);
                        pnl_partpayment.Visible = true;
                        dgv_part.Rows.Clear();
                        foreach (Control control in flppart_paymentbutton.Controls)
                        {
                            if (control is Button)
                            {
                                ((Button)control).Visible = true;
                                ((Button)control).Enabled = true;
                            }
                        }
                        break;
                    case "btn_splitpayment":
                        tlp_main_split.RowStyles[4] = new RowStyle(SizeType.AutoSize);
                        payid = Convert.ToInt32(PaymentType.SPLIT);
                        pnl_split.Visible = true;
                         panelWidthPercentage = 50;
                         panelheightPercentage = 70;
                         panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                         panelHeight = (int)(this.Height * panelheightPercentage / 100);
                        pnl_split.Size = new Size(panelWidth, panelHeight);

                        pnl_split.Location = new Point(this.ClientSize.Width / 2 - pnl_split.Size.Width / 2, this.ClientSize.Height / 2 - pnl_split.Size.Height / 2);
                        dgv_split.Rows.Clear();
                        break;
                }
                flp_payment_option.Visible = false;
                if (clickedButton.Name != "Btnpartpayment")
                    paybillcal(payid);
            }
            catch (Exception ex)
            {
            }

        }

        private void btn_frm_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btncash_part_Click(object sender, EventArgs e)
        {   
            try
            {
                dgv_part.Rows.Add();
                rearrange_slno_part();
                int rowno = dgv_part.RowCount - 1;
                Button clickedButton = sender as Button;
                switch (clickedButton.Name)
                {                   
                    case "btndebit_part":
                        dgv_part.Rows[rowno].Cells[4].Value = Convert.ToInt32(PaymentType.DEBITCARD);
                        dgv_part.Rows[rowno].Cells[2].Value = "DEBITCARD";
                        clickedButton.Enabled = false;
                        dgv_part.CurrentCell = dgv_part.Rows[rowno].Cells[0];
                        break;
                    case "btnupi_part":
                        dgv_part.Rows[rowno].Cells[4].Value = Convert.ToInt32(PaymentType.UPI);
                        dgv_part.Rows[rowno].Cells[2].Value = "UPI";
                        dgv_part.CurrentCell = dgv_part.Rows[rowno].Cells[0];
                        clickedButton.Enabled = false;
                        break;
                    case "btnnetbanking_part":
                        dgv_part.Rows[rowno].Cells[4].Value = Convert.ToInt32(PaymentType.BANKINGONLINE);
                        dgv_part.Rows[rowno].Cells[2].Value = "BANKINGONLINE";
                        dgv_part.CurrentCell = dgv_part.Rows[rowno].Cells[0];
                        clickedButton.Enabled = false;
                        break;
                    case "btncreditcard_part":
                        dgv_part.Rows[rowno].Cells[4].Value = Convert.ToInt32(PaymentType.CREDIT);
                        dgv_part.Rows[rowno].Cells[2].Value = "CREDIT";
                        dgv_part.CurrentCell = dgv_part.Rows[rowno].Cells[0];
                        clickedButton.Enabled = false;
                        break;
                    case "btnother_part":
                        dgv_part.Rows[rowno].Cells[4].Value = Convert.ToInt32(PaymentType.OTHERS);
                        dgv_part.Rows[rowno].Cells[2].Value = "OTHERS";
                        dgv_part.CurrentCell = dgv_part.Rows[rowno].Cells[0];
                        clickedButton.Enabled = false;
                        break;
                }
                foreach (Control control in flppart_paymentbutton.Controls)
                {
                    if (control is Button && control.Name!= clickedButton.Name)
                    {
                        ((Button)control).Visible = false;

                    }
                }
                dgv_part.CurrentCell = dgv_part.Rows[rowno].Cells[1];
            }
            catch (Exception ex)
            {
            }

        }

        private void btn_part_close_Click(object sender, EventArgs e)
        {
            pnl_partpayment.Visible = false;
        }

        private void btnclose_part_Click(object sender, EventArgs e)
        {
            pnl_partpayment.Visible = false;
            tlp_tableorder.Enabled = true;
        }

        private void btnconfirm_part_Click(object sender, EventArgs e)
        {   
            decimal netAmount = Convert.ToDecimal(txtnetamount.Text);
            if (Convert.ToDecimal(dgv_part.Rows[0].Cells[1].Value)== 0 || dgv_part.Rows[0].Cells[1].Value == null)
            {
                CMessageBox.Show("Please Enter the " + Convert.ToString(dgv_part.Rows[0].Cells[2].Value)+" Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgv_part.Rows[0].Cells[1].Value = 0;
                dgv_part.CurrentCell = dgv_part.Rows[0].Cells[1];
                return;
            }
            else
            {
                pnl_partpayment.Visible = false;
                paybillcal(Convert.ToInt32(PaymentType.PART));
            }
             
        }

        private void dgv_part_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    //int btnid = Convert.ToInt32(dgv_part.Rows[dgv_part.CurrentRow.Index].Cells[4].Value);
                    foreach (Control control in flppart_paymentbutton.Controls)
                    {
                        if (control is Button )
                        {
                            ((Button)control).Visible = true;
                            ((Button)control).Enabled = true;
                        }
                    }
                    dgv_part.Rows.Clear();               
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void rearrange_slno_part()
        {
            for (int i = 0; i < dgv_part.RowCount; i++)
            {
                dgv_part.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void dgv_part_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgv_part.CurrentCell.ColumnIndex == 1)
            {
                e.Control.KeyPress -= new KeyPressEventHandler(dgv_part_KeyPress);
                System.Windows.Forms.TextBox tb = e.Control as System.Windows.Forms.TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(dgv_part_KeyPress);
                }
            }
        }
        private void dgv_part_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dgv_part.CurrentCell.ColumnIndex == 1)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
                // only allow one decimal point
                if (e.KeyChar == '.' && (sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            }
        }

        private void dgv_part_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Delete)
                {
                    foreach (Control control in flppart_paymentbutton.Controls)
                    {
                        if (control is Button)
                        {
                            ((Button)control).Visible = true;
                            ((Button)control).Enabled = true;
                        }
                    }
                    dgv_part.Rows.Clear();

                }
            }
            catch(Exception ex)
            {

            }

        }

        private void dgv_part_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
               e.Value = Properties.Resources.Delete_Grid;
            }
        }

        private void dgv_part_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1 && e.RowIndex == 0)
                {
                    decimal netAmount = Convert.ToDecimal(txtnetamount.Text);
                    decimal cellValue = Convert.ToDecimal(dgv_part.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    if (cellValue > netAmount)
                    {
                        CMessageBox.Show("The entered value must not exceed the net amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dgv_part.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
                        dgv_part.CurrentCell = dgv_part.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    }
                    else
                    {
                        if (dgv_part.RowCount == 2)
                        {
                            dgv_part.Rows[1].Cells[4].Value = Convert.ToInt32(PaymentType.CASH);
                            dgv_part.Rows[1].Cells[2].Value = "CASH";
                            dgv_part.Rows[1].Cells[e.ColumnIndex].Value = netAmount - cellValue;
                            dgv_part.Rows[1].ReadOnly = true;
                        }
                        else
                        {
                            dgv_part.Rows.Add();
                            dgv_part.Rows[1].Cells[4].Value = Convert.ToInt32(PaymentType.CASH);
                            dgv_part.Rows[1].Cells[2].Value = "CASH";
                            dgv_part.Rows[1].Cells[e.ColumnIndex].Value = netAmount - cellValue;
                            dgv_part.Rows[1].ReadOnly = true;
                        }
                        rearrange_slno_part();
                    }

                }
            }
            catch(Exception ex)
            {

            }

            
        }

        private void btn_split_number_Click(object sender, EventArgs e)
        {
            Splitypeno = 1;
            tlp_main_split.RowStyles[4] = new RowStyle(SizeType.Percent,15);
            Control controlToRemove = tlp_main_split.GetControlFromPosition(0,4);
            tlp_main_split.Controls.Remove(controlToRemove);
            tlp_main_split.Controls.Add(tlp_numberwise, 0,4);
            tlp_numberwise.Dock = DockStyle.Fill;
            tlp_numberwise.Visible = true;
        }
        public void SetButtonImage(Button button, Image originalImage, int desiredWidth, int desiredHeight)
        {
            Image resizedImage = new Bitmap(originalImage, new Size(desiredWidth, desiredHeight));
            button.Image = resizedImage;
        }
        private void SetRoundedButton(Button button, int cornerRadius)
        {
            using (var path = new GraphicsPath())
            {
                Rectangle rect = new Rectangle(0, 0, button.Width, button.Height);
                path.StartFigure();
                path.AddArc(rect.Left, rect.Top, cornerRadius, cornerRadius, 180, 90);
                path.AddArc(rect.Right - cornerRadius, rect.Top, cornerRadius, cornerRadius, 270, 90);
                path.AddArc(rect.Right - cornerRadius, rect.Bottom - cornerRadius, cornerRadius, cornerRadius, 0, 90);
                path.AddArc(rect.Left, rect.Bottom - cornerRadius, cornerRadius, cornerRadius, 90, 90);
                path.CloseFigure();
                button.Region = new Region(path);
            }
        }
        private void btn_itemwise_Click(object sender, EventArgs e)
        {
            btn_split_number.Visible = false;
            tlp_main_split.RowStyles[4] = new RowStyle(SizeType.AutoSize);
            Control controlToRemove = tlp_main_split.GetControlFromPosition(0, 4);
            tlp_main_split.Controls.Remove(controlToRemove);
            Splitypeno = 2;
            pnl_split.Visible = false;
            pnl_split_itemwise.Visible = true;

            int panelWidthPercentage = 90;
            int panelheightPercentage = 85;
            int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
            int panelHeight = (int)(this.Height * panelheightPercentage / 100);
            pnl_split_itemwise.Size = new Size(panelWidth, panelHeight);
            pnl_split_itemwise.Location = new Point(this.ClientSize.Width / 2 - pnl_split_itemwise.Size.Width / 2, this.ClientSize.Height / 2 - pnl_split_itemwise.Size.Height / 2);
            pnl_split_itemwise.BringToFront();
            SetButtonImage(ib_add_categ_itemwise, FoodandBeverage.Properties.Resources.plus, desiredWidth, desiredHeight);
            SetRoundedButton(ib_add_categ_itemwise, 70);
            flp_itemwise_products.Controls.Clear();
            dgv_split.Rows.Clear();
            CheckedNames.Clear();
            orderDetailsInfoList.Clear();
            BillList.Clear();
            foreach (Control control in flp_itemwise_por_sort.Controls.Cast<Control>().ToList())
            {
                if (control != ib_add_categ_itemwise)
                {
                    flp_itemwise_por_sort.Controls.Remove(control);
                    control.Dispose(); // Optionally dispose of the removed control
                }
            }
            BllSearch<DmnGridFields> bllSearchmaster = new BllSearch<DmnGridFields>();
            DmnSearch dmnSearchmaster = new DmnSearch();
            dmnSearchmaster.Select = "sd.ItemName Field1,sd.SalesDetailsId Field2,sd.SalesMasterId Field3,sd.ProductType Field4,c.CategoryName Field5";
            dmnSearchmaster.Table = "  tblSalesMaster sm " +
                "LEFT JOIN tblSalesDetails sd on sd.SalesMasterId= sm.SalesMasterId " +
                "LEFT JOIN tblCategory_FTI c on sd.ProductType= c.Fcid";
            dmnSearchmaster.WhereNdGroupBy = "where sm.tid= " + tableId + " and sm.Status=1 and sm.OrderMasterid= "+ Convert.ToInt32(lblorderid.Text);
            dmnSearchmaster.OrderBy = "";
            var CheckSource = bllSearchmaster.GetSearchData("prdSearch", dmnSearchmaster);
            if (CheckSource.Count > 0)
            {
                var distinctCheckSource = CheckSource
                    .GroupBy(x => x.Field4)
                    .Select(g => g.First())
                    .ToList();
                distinctCheckSource.Insert(0, new DmnGridFields { Field4 = "-1", Field5 = "None" });
                cmb_itemcategory_split.DataSource = distinctCheckSource;
                cmb_itemcategory_split.ValueMember = "Field4";
                cmb_itemcategory_split.DisplayMember = "Field5";
                cmb_itemcategory_split.SelectedIndex = 0; 
                Existingsalemasterid = Convert.ToInt32(CheckSource[0].Field3);
                foreach (var item in CheckSource)
                {
                    CheckBox checkBox = new CheckBox();
                    checkBox.Name = "chk_pro_" + item.Field2;
                    checkBox.Text = item.Field1;
                    checkBox.Tag = item.Field2;
                    checkBox.AccessibleDescription = item.Field4;
                    checkBox.AutoSize = true;
                    checkBox.Margin = new Padding(10, 10, 0, 0);
                    checkBox.ForeColor = Color.Black;
                    checkBox.BackColor = Color.Yellow;
                    checkBox.Size = new Size(150, 30);
                    checkBox.Font = new Font("Arial", 14, FontStyle.Bold);
                    flp_itemwise_products.Controls.Add(checkBox);
                }
            }
        }


        private void btn_close_split_Click(object sender, EventArgs e)
        {
            pnl_split.Visible = false;
        }

        private void btn_close_split2_Click(object sender, EventArgs e)
        {
            pnl_split.Visible = false;
        }

        private void btn_confirm_split_Click(object sender, EventArgs e)
        {

        }
        

        private void btn_con_nosplit_Click(object sender, EventArgs e)
        {

            decimal shareamount = Convert.ToDecimal(txtnetamount.Text) / Convert.ToInt32(txtnumber_split.Text);
            dgv_split.Rows.Clear();
            dgv_split.Rows.Add(Convert.ToInt32(txtnumber_split.Text));
            for (int i=0;i< Convert.ToInt32(txtnumber_split.Text);i++)
            {
                dgv_split.Rows[i].Cells[0].Value = i + 1; ;
                dgv_split.Rows[i].Cells[1].Value = shareamount.ToString("N");
                dgv_split.Rows[i].Cells[6].Value = 1;
                DataGridViewImageCell cell = (DataGridViewImageCell)dgv_split.Rows[i].Cells[2];
                cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                cell.Value = Properties.Resources.pay_per_click;
            }
            btn_itemwise.Visible = false;
            btn_con_nosplit.Visible = false;
        }

        private void dgv_split_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2 && Convert.ToInt32(dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[6].Value) == 1)
                {
                    int panelWidthPercentage = 40;
                    int panelheightPercentage = 40;
                    int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                    int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                    tlp_split_payment_button.Size = new Size(panelWidth, panelHeight);

                    tlp_split_payment_button.Location = new Point(this.ClientSize.Width / 2 - tlp_split_payment_button.Size.Width / 2, this.ClientSize.Height / 2 - tlp_split_payment_button.Size.Height );
                   // tlp_split_payment_button.Location = new Point(this.ClientSize.Width / 2 - tlp_split_payment_button.Size.Width / 2, this.ClientSize.Height / 2 - tlp_split_payment_button.Size.Height/2);
                    tlp_split_payment_button.Visible = true;
                    tlp_split_payment_button.BringToFront();
                   // pnl_split.Visible = false;
                   //// pnl_split.Enabled = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

     
        public void rearrange_slno_split()
        {
            for (int i = 0; i < dgv_part.RowCount; i++)
            {
                dgv_split.Rows[i].Cells[0].Value = i + 1;
            }
        }

        private void btn_split_cash_Click(object sender, EventArgs e)
        {
            try
            {
                int payid = 0;
                string matchingOrderDetailsIds = "";
                int rowno = Convert.ToInt32(dgv_split.CurrentCell.RowIndex);
                Button clickedButton = sender as Button;
                switch (clickedButton.Name)
                {
                    case "btn_split_cash":
                        payid = Convert.ToInt32(PaymentType.CASH);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[4].Value = Convert.ToInt32(PaymentType.CASH);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[3].Value = "CASH";
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].ReadOnly = true;
                        break;
                    case "btn_split_debit":
                        payid = Convert.ToInt32(PaymentType.DEBITCARD);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[4].Value = Convert.ToInt32(PaymentType.DEBITCARD);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[3].Value = "DEBITCARD";
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].ReadOnly = true;
                        break;
                    case "btn_split_upi":
                        payid = Convert.ToInt32(PaymentType.UPI);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[4].Value = Convert.ToInt32(PaymentType.UPI);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[3].Value = "UPI";
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].ReadOnly = true;
                        break;
                    case "btn_split_netbanking":
                        payid = Convert.ToInt32(PaymentType.BANKINGONLINE);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[4].Value = Convert.ToInt32(PaymentType.BANKINGONLINE);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[3].Value = "BANKINGONLINE";
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].ReadOnly = true;
                        break;
                    case "btn_split_credit":
                        payid = Convert.ToInt32(PaymentType.CREDIT);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[4].Value = Convert.ToInt32(PaymentType.CREDIT);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[3].Value = "CREDIT";
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].ReadOnly = true;
                        break;
                    case "btn_split_others":
                        payid = Convert.ToInt32(PaymentType.CREDIT);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[4].Value = Convert.ToInt32(PaymentType.OTHERS);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[3].Value = "OTHERS";
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].ReadOnly = true;
                        break;

                }
                tlp_split_payment_button.Visible = false;
                DmnPayment dmnpayment = new DmnPayment();
                Bllpayment bllpay = new Bllpayment();
                dmnpayment.PayId = 0;
                dmnpayment.OrderMasterID = Convert.ToInt32(label18.Text);
                dmnpayment.SalemasterId = Convert.ToInt32(txtsalemasterid.Text);
                dmnpayment.PayType = Convert.ToInt32(dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[4].Value);
                dmnpayment.Amount = Convert.ToDecimal(dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[1].Value);
                dmnpayment.Tid = tableId;

                foreach (OrderDetailsInfo orderDetailsInfo in orderDetailsInfoList)
                {
                    if (Convert.ToInt32(dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[7].Value) == orderDetailsInfo.BillNumber)
                    {
                        matchingOrderDetailsIds += orderDetailsInfo.OrderDetailsId + ",";
                    }
                }
                dmnpayment.BillCount = BillList.Count;
                dmnpayment.SplitType = Splitypeno;
                dmnpayment.OaAmount = Convert.ToDecimal(dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[8].Value);
                dmnpayment.OdAmount = Convert.ToDecimal(dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[9].Value);
                dmnpayment.PromoAmt = Convert.ToDecimal(dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[10].Value);
                dmnpayment.DiscountAmount = Convert.ToDecimal(dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[11].Value);

                dmnpayment.OrderDetailsId = matchingOrderDetailsIds;
                dmnpayment.BranchId = FrmDashboard.BranchId;
                dmnpayment.CreatedBy = FrmDashboard.UserId;
                dmnpayment.Status = 1;
                var InsertStatus = bllpay.AddEditPayment("prdInsert_Upd_Payment", dmnpayment);
                if (InsertStatus.PayId > 0)
                {
                    FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(Convert.ToInt32(txtsalemasterid.Text), Convert.ToInt32(PaymentType.SPLIT), Convert.ToInt32(InsertStatus.PayId));
                    frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                    frmSalesInvoice.ShowDialog();

                    dgv_split.Rows[rowno].Cells[5].Value = "Print Complete";
                    dgv_split.Rows[rowno].Cells[6].Value = 2;

                    DataGridViewImageCell cell1 = (DataGridViewImageCell)dgv_split.Rows[rowno].Cells[2];
                    cell1.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    cell1.Value = Properties.Resources.orderview_complete;

                }
                int count = 0;
                for (int i = 0; i < dgv_split.RowCount; i++)
                {
                    if (Convert.ToInt32(dgv_split.Rows[i].Cells[6].Value) == 2 && Convert.ToInt32(dgv_split.Rows[i].Cells[4].Value) > 0)
                        count = count + 1;
                }
                if (count == dgv_split.RowCount)
                {
                    CMessageBox.Show("Transaction Completed Sucessfully", "Sucessfull", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                    {
                        FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                        _instanceFrmdash.LoadTableStaus(0);
                    }
                    this.Close();
                }


            }
            catch (Exception ex)
            {

            }
            
        }

        private void btn_split_payclose_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < Convert.ToInt32(txtnumber_split.Text); i++)
            //{              
            //        DataGridViewImageCell cell1 = (DataGridViewImageCell)dgv_split.Rows[i].Cells[2];
            //        dgv_split.Rows[i].Cells[2].Value = null;
            //        if (Convert.ToInt32(dgv_split.Rows[i].Cells[6].Value) == 1)
            //        {
            //            cell1.ImageLayout = DataGridViewImageCellLayout.Stretch;
            //            cell1.Value = Properties.Resources.pay_per_click;
            //        }
            //        else
            //        {
            //            cell1.ImageLayout = DataGridViewImageCellLayout.Zoom;
            //            cell1.Value = Properties.Resources.orderview_complete;
            //        }
                
            //}
            tlp_split_payment_button.Visible = false;
            pnl_split.Visible = true;
        }

        private void btn_split_itemwiseclose_Click(object sender, EventArgs e)
        {
            pnl_split_itemwise.Visible = false;
           pnl_split.Visible = true;
        }

        private void btns_itemwise_confirm_Click(object sender, EventArgs e)
        {
            try
            {
                int count = flp_itemwise_products.Controls.OfType<CheckBox>().Count();
                if (count > 0)
                {
                    CMessageBox.Show("Please add products to the bill before Confirm.", "Incomplete Bill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    int j = 0;
                    decimal amount = 0, tamt = 0,promoamt=0;
                    dgv_split.Rows.Clear();
                    BllSearch<DmnSaledetails> bllSearch = new BllSearch<DmnSaledetails>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "*";
                    dmnSearch.Table = "tblSalesDetails";
                    dmnSearch.WhereNdGroupBy = "where SalesMasterId = " + Existingsalemasterid + " and Status IN (1)";
                    dmnSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    if (gridSource.Count > 0)
                    {
                        foreach (Namelist nameInfo in BillList)
                        {
                            tamt = 0;
                            promoamt = 0;
                            string controlName = nameInfo.Cnames;
                            Control control = flp_itemwise_por_sort.Controls.Find(controlName, true).OfType<DataGridView>().FirstOrDefault();

                            if (control != null)
                            {
                                DataGridView dgv = (DataGridView)control;
                                foreach (DmnSaledetails saleDetails in gridSource)
                                {
                                    foreach (DataGridViewRow row in dgv.Rows)
                                    {
                                        if (row.Cells[2].Value != null && Convert.ToInt32(row.Cells[2].Value) == saleDetails.SalesDetailsId)
                                        {
                                             amount = Convert.ToDecimal(saleDetails.NetAmount);
                                             tamt = tamt + amount;
                                        }
                                    }                
                                }
                            }
                            decimal otheraddamt=0, otherdedamt=0;
                            dgv_split.Rows.Add();
                            if (string.IsNullOrEmpty(txtdiscountamt_promo.Text))
                                txtdiscountamt_promo.Text = "0";
                            if (string.IsNullOrEmpty(txtotheraddamt.Text))
                                txtotheraddamt.Text = "0";
                            if (string.IsNullOrEmpty(txtotherdedamt.Text))
                                txtotherdedamt.Text = "0";
                            decimal discountAmnt = Convert.ToDecimal(txtdis_per.Text.Trim()) * tamt / 100;

                            if(Convert.ToDecimal(txtotheraddamt.Text) >0)
                                 otheraddamt = Convert.ToDecimal(txtotheraddamt.Text) / Convert.ToInt32(BillList.Count);
                            if (Convert.ToDecimal(txtotherdedamt.Text) > 0)
                                 otherdedamt = Convert.ToDecimal(txtotherdedamt.Text) / Convert.ToInt32(BillList.Count);

                            if (Convert.ToDecimal(txtdiscountamt_promo.Text)>0)
                            {
                                promoamt = Convert.ToDecimal(txtdiscountamt_promo.Text) / Convert.ToInt32(BillList.Count);
                            }                            
                            decimal finalamt = (tamt + otheraddamt) - (discountAmnt + promoamt + otherdedamt);
                            dgv_split.Rows[j].Cells[0].Value = j + 1; ;
                            dgv_split.Rows[j].Cells[1].Value = finalamt.ToString("N");
                            dgv_split.Rows[j].Cells[6].Value = 1;
                            dgv_split.Rows[j].Cells[7].Value = nameInfo.Id;

                            dgv_split.Rows[j].Cells[8].Value = otheraddamt;
                            dgv_split.Rows[j].Cells[9].Value = otherdedamt;
                            dgv_split.Rows[j].Cells[10].Value = promoamt;
                            dgv_split.Rows[j].Cells[11].Value = discountAmnt;
                            DataGridViewImageCell cell = (DataGridViewImageCell)dgv_split.Rows[j].Cells[2];
                            cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                            cell.Value = Properties.Resources.pay_per_click;
                            j = j + 1;
                        }
                    }
                    pnl_split_itemwise.Visible = false;
                    pnl_split.Visible = true;

                }
            }
            catch (Exception ex)
            {

            }


        }

        private void btn_close_pnlitemwise_Click(object sender, EventArgs e)
        {
            pnl_split_itemwise.Visible = false;
            pnl_split.Visible = true;
        }

        private void ib_add_categ_itemwise_Click(object sender, EventArgs e)
        {
            try
            {
                //foreach (Control control in flp_itemwise_por_sort.Controls.Cast<Control>().ToList())
                //{
                //    if (control != ib_add_categ_itemwise)
                //    {
                //        flp_itemwise_por_sort.Controls.Remove(control);
                //        control.Dispose(); // Optionally dispose of the removed control
                //    }
                //}
                int count = flp_itemwise_por_sort.Controls.OfType<Panel>().Count();
                if (count == 0)
                    count = count + 1;
                else
                    count = count + 1;

                Panel mainPanel = new Panel();
                mainPanel.Name = "BILL_pnl" + count;
                mainPanel.Tag =  count;
                mainPanel.BackColor = Color.LightGray;
                mainPanel.Dock = DockStyle.None;
                mainPanel.Height = 50;
                mainPanel.Width = 280;
                mainPanel.Margin = new Padding(10, 10, 0, 0);

                TableLayoutPanel mainTableLayoutPanel = new TableLayoutPanel();
                mainTableLayoutPanel.Name = "BILL_tlp_box" + count;
                mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
                mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 75));
                mainTableLayoutPanel.Dock = DockStyle.Fill;
                mainTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

                TableLayoutPanel downArrowTableLayoutPanel = new TableLayoutPanel();
                downArrowTableLayoutPanel.Name = "BILL_tlp_navbar" + count;
                downArrowTableLayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                downArrowTableLayoutPanel.Dock = DockStyle.Fill;
                downArrowTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
                downArrowTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
                downArrowTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

                Label label = new Label();
                label.Text = "BILL" + count;
                label.Tag = count;
                label.Dock = DockStyle.Fill;
                label.Click += BILLName_Click;
                label.Font = new Font(label.Font, FontStyle.Bold);
                label.TextAlign = ContentAlignment.MiddleCenter;
                downArrowTableLayoutPanel.Controls.Add(label, 0, 0);

                PictureBox downArrowIcon = new PictureBox();
                downArrowIcon.Name = "BILL_downarrow" + count;
                downArrowIcon.Tag = count;
                downArrowIcon.Image = Properties.Resources.down_arrow;
                downArrowIcon.SizeMode = PictureBoxSizeMode.StretchImage;
                downArrowIcon.Size = new Size(20, 20);
                downArrowIcon.Dock = DockStyle.Fill;
                downArrowTableLayoutPanel.Controls.Add(downArrowIcon, 1, 0);
                downArrowIcon.Click += DownArrowIcon_Click;

                PictureBox close_product_cart = new PictureBox();
                close_product_cart.Name = "BILLclose" + count;
                close_product_cart.Tag = count;
                close_product_cart.Image = Properties.Resources.Delete_Grid;
                close_product_cart.SizeMode = PictureBoxSizeMode.StretchImage;
                close_product_cart.Size = new Size(20, 20);
                close_product_cart.Dock = DockStyle.Fill;
                downArrowTableLayoutPanel.Controls.Add(close_product_cart, 2, 0);
                close_product_cart.Click += close_product_cart_Click;

                DataGridView dataGridView = new DataGridView();
                dataGridView.Name = "BILLdgv" + count;

                dataGridView.Tag = count;
                dataGridView.AllowUserToDeleteRows = true;
                dataGridView.AllowUserToAddRows = false;

                dataGridView.BorderStyle = BorderStyle.None;
                dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dataGridView.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dataGridView.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dataGridView.BackgroundColor = Color.White;

                dataGridView.EnableHeadersVisualStyles = false;
                dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                dataGridView.RowHeadersVisible = false;
                DataGridViewTextBoxColumn columnSlno = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn columnItemName = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn columnorderdetailsid = new DataGridViewTextBoxColumn();
                DataGridViewTextBoxColumn columncateid = new DataGridViewTextBoxColumn();
                dataGridView.Columns.Add(columnSlno);
                dataGridView.Columns.Add(columnItemName);
                dataGridView.Columns.Add(columnorderdetailsid);
                dataGridView.Columns.Add(columncateid);

                dataGridView.Columns[0].HeaderText = "Slno";
                dataGridView.Columns[1].HeaderText = "Item Name";
                dataGridView.Columns[2].HeaderText = "orderdetailsid";
                dataGridView.Columns[3].HeaderText = "categid";

                dataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dataGridView.Columns[2].Visible = false;
                dataGridView.Columns[3].Visible = false;
                dataGridView.Rows.Add();
                dataGridView.Visible = false;

                mainTableLayoutPanel.Controls.Add(downArrowTableLayoutPanel, 0, 0);
                mainTableLayoutPanel.Controls.Add(dataGridView, 0, 1);

                mainPanel.Controls.Add(mainTableLayoutPanel);
                flp_itemwise_por_sort.Controls.Add(mainPanel);
            }
            catch (Exception ex)
            {

            }
            
        }
        // Event handler for the PictureBox Click event
        private void DownArrowIcon_Click(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                string dgvname = "BILLdgv" + clickedControl.Tag;
                string pnlname = "BILL_pnl" + clickedControl.Tag;
                string BILL_tlp_box_name = "BILL_tlp_box" + clickedControl.Tag;
                string BILL_tlp_navbar_name = "BILL_tlp_navbar" + clickedControl.Tag;
                DataGridView dgvcontrol = this.Controls.Find(dgvname, true).FirstOrDefault() as DataGridView;
                Panel pnlcontrol = this.Controls.Find(pnlname, true).FirstOrDefault() as Panel;
                TableLayoutPanel tlp_control = this.Controls.Find(BILL_tlp_box_name, true).FirstOrDefault() as TableLayoutPanel;
                TableLayoutPanel BILL_tlp_navbar_control = this.Controls.Find(BILL_tlp_navbar_name, true).FirstOrDefault() as TableLayoutPanel;

                if (dgvcontrol != null)
                {
                    if (dgvcontrol.Visible)
                    {
                        dgvcontrol.Visible = false;
                        dgvcontrol.Dock = DockStyle.None;
                        dgvcontrol.ScrollBars = ScrollBars.None;
                        BILL_tlp_navbar_control.Dock = DockStyle.Fill;
                        pnlcontrol.Height = 50;
                    }
                    else
                    {
                        dgvcontrol.Visible = true;
                        dgvcontrol.ScrollBars = ScrollBars.Both;
                        dgvcontrol.Dock = DockStyle.Fill;
                        pnlcontrol.Height = 150;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }


        private void BILLName_Click(object sender, EventArgs e)
        {
            try
            {
                CheckedNames.Clear();
                Control clickedControl = sender as Control;
                string dgvname = "BILLdgv" + clickedControl.Tag;
                DataGridView dgvcontrol = this.Controls.Find(dgvname, true).FirstOrDefault() as DataGridView;
                if (dgvcontrol != null)
                {                  
                    int i = dgvcontrol.RowCount - 1;
                    int totalcount = flp_itemwise_products.Controls.Count;
                    for (int index = 0; index < totalcount; index++)
                    {
                        Control control = flp_itemwise_products.Controls[index];
                        if (control is CheckBox checkBox && checkBox.Checked)
                        {
                            dgvcontrol.Rows[i].Cells[0].Value = i + 1;
                            dgvcontrol.Rows[i].Cells[1].Value = checkBox.Text;
                            dgvcontrol.Rows[i].Cells[2].Value = Convert.ToInt32(checkBox.Tag);
                            dgvcontrol.Rows[i].Cells[3].Value = Convert.ToInt32(checkBox.AccessibleDescription);
                            CheckedNames.Add(new Namelist
                            {
                                Id = Convert.ToInt32(checkBox.Tag),
                                Cnames = "chk_pro_"+ Convert.ToInt32(checkBox.Tag)
                            });
                            if (!BillList.Any(item => item.Id == Convert.ToInt32(clickedControl.Tag)))
                            {
                                BillList.Add(new Namelist
                                {
                                    Id = Convert.ToInt32(clickedControl.Tag),
                                    Cnames = "BILLdgv" + clickedControl.Tag
                                });
                            }
                            orderDetailsInfoList.Add(new OrderDetailsInfo
                            {
                                OrderDetailsId = Convert.ToInt32(checkBox.Tag),
                                BillNumber =Convert.ToInt32(clickedControl.Tag),                                                                   
                            });

                            dgvcontrol.Rows.Add();
                            i = i + 1;
                        }

                    }

                    foreach (Namelist nameInfo in CheckedNames)
                    {
                        string controlNameToRemove = nameInfo.Cnames;
                        Control controlToRemove = flp_itemwise_products.Controls.Find(controlNameToRemove, true).FirstOrDefault();
                        if (controlToRemove != null)
                        {
                            flp_itemwise_products.Controls.Remove(controlToRemove);
                        }
                    }

                }
            }
            catch(Exception ex)
            {

            }            
        }
        public class Namelist
        {
            public int Id { get; set; }
            public string Cnames { get; set; }
            public int Ordermasterid { get; set; }
        }
        public class OrderDetailsInfo
        {             
            public int OrderDetailsId { get; set; }
            public int BillNumber { get; set; }
        }       

        private void close_product_cart_Click(object sender, EventArgs e)
        {
            try
            {
                CheckedNames.Clear();
                Control clickedControl = sender as Control;
                string pnlname = "BILL_pnl" + clickedControl.Tag;
                string dgvname = "BILLdgv" + clickedControl.Tag;
                DataGridView dgvcontrol = this.Controls.Find(dgvname, true).FirstOrDefault() as DataGridView;
                Panel pnlcontrol = this.Controls.Find(pnlname, true).FirstOrDefault() as Panel;
                if (pnlcontrol != null)
                {
                    for(int i=0;i< dgvcontrol.RowCount;i++)
                    {
                        if(Convert.ToInt32(dgvcontrol.Rows[i].Cells[2].Value)>0)
                        {
                            CheckBox checkBox = new CheckBox();
                            checkBox.Name = "chk_pro_" + Convert.ToInt32(dgvcontrol.Rows[i].Cells[2].Value);
                            checkBox.Text = Convert.ToString(dgvcontrol.Rows[i].Cells[1].Value);
                            checkBox.Tag = Convert.ToInt32(dgvcontrol.Rows[i].Cells[2].Value);
                            checkBox.AccessibleDescription = Convert.ToString(dgvcontrol.Rows[i].Cells[3].Value);
                            checkBox.AutoSize = true;
                            checkBox.Margin = new Padding(10, 10, 0, 0);
                            checkBox.ForeColor = Color.Black;
                            checkBox.BackColor = Color.Yellow;
                            checkBox.Size = new Size(150, 30);
                            checkBox.Font = new Font("Arial", 14, FontStyle.Bold);
                            flp_itemwise_products.Controls.Add(checkBox);
                        }
        
                    }
                    flp_itemwise_por_sort.Controls.Remove(pnlcontrol);
                }

            }
            catch (Exception ex)
            {

            }

        }

        private void txtnumber_split_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void chk_itemwise_pro_selectall_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_itemwise_pro_selectall.Checked)
            {
                if (cmb_itemcategory_split.SelectedIndex == 0)
                {
                    foreach (Control control in flp_itemwise_products.Controls)
                    {
                        if (control is CheckBox checkBox)
                        {
                            checkBox.Checked = true;
                        }
                    }
                }
                else
                {
                    foreach (Control control in flp_itemwise_products.Controls)
                    {
                        if (control is CheckBox checkBox && (Convert.ToInt32(checkBox.AccessibleDescription) == Convert.ToInt32(cmb_itemcategory_split.SelectedValue)))
                        {
                            checkBox.Checked = true;
                        }
                    }
                }
            }
            else
            {
                if (cmb_itemcategory_split.SelectedIndex == 0)
                {
                    foreach (Control control in flp_itemwise_products.Controls)
                    {
                        if (control is CheckBox checkBox)
                        {
                            checkBox.Checked = false;
                        }
                    }
                }
                else
                {
                    foreach (Control control in flp_itemwise_products.Controls)
                    {
                        if (control is CheckBox checkBox && (Convert.ToInt32(checkBox.AccessibleDescription) == Convert.ToInt32(cmb_itemcategory_split.SelectedValue)))
                        {
                            checkBox.Checked = false;
                        }
                    }
                }
            }
        }

        private void btn_gridview_close_Click(object sender, EventArgs e)
        {
            tlp_gridview.Visible = false;
        }

        private void btn_custom_details_Click(object sender, EventArgs e)
        {
            if (pnl_userdetails.Visible)
            {
                if (txtentrie_descr.Visible==false)
                {
                    tlp_payment_main.RowStyles[0] = new RowStyle(SizeType.Percent, 5.90F);
                    tlp_payment_main.RowStyles[1] = new RowStyle(SizeType.Percent, 40);
                    tlp_payment_main.RowStyles[2] = new RowStyle(SizeType.AutoSize);
                    tlp_payment_main.RowStyles[3] = new RowStyle(SizeType.Percent, 2.16F);
                    tlp_payment_main.RowStyles[4] = new RowStyle(SizeType.AutoSize);
                    tlp_payment_main.RowStyles[5] = new RowStyle(SizeType.Percent, 11F);
                    txtentrie_descr.Visible = false;
                }

                    pnl_userdetails.Visible = false;
                    tlp_payment_outerdesign.RowStyles[1] = new RowStyle(SizeType.AutoSize);
                    tlp_payment_main.RowStyles[0] = new RowStyle(SizeType.Percent, 5.90F);
                    tlp_payment_main.RowStyles[1] = new RowStyle(SizeType.Percent, 81.54F);
                    tlp_payment_main.RowStyles[2] = new RowStyle(SizeType.AutoSize);
                    tlp_payment_main.RowStyles[3] = new RowStyle(SizeType.Percent, 2.16F);
                    tlp_payment_main.RowStyles[4] = new RowStyle(SizeType.AutoSize);
                    tlp_payment_main.RowStyles[5] = new RowStyle(SizeType.Percent, 10.39F);
                
              
            }
            else
            {
                tlp_payment_main.RowStyles[0] = new RowStyle(SizeType.Percent, 5.90F);
                tlp_payment_main.RowStyles[1] = new RowStyle(SizeType.AutoSize);
                tlp_payment_main.RowStyles[2] = new RowStyle(SizeType.AutoSize);
                tlp_payment_main.RowStyles[3] = new RowStyle(SizeType.Percent, 2.16F);
                tlp_payment_main.RowStyles[4] = new RowStyle(SizeType.AutoSize);

                tlp_payment_outerdesign.RowStyles[1] = new RowStyle(SizeType.Percent, 50);

                if (txtentrie_descr.Visible)
                {
                    tlp_payment_main.RowStyles[1] = new RowStyle(SizeType.Percent, 38);
                    tlp_payment_main.RowStyles[5] = new RowStyle(SizeType.Percent, 12);
                }
                else
                {
                    tlp_payment_main.RowStyles[1] = new RowStyle(SizeType.Percent, 38);
                    tlp_payment_main.RowStyles[5] = new RowStyle(SizeType.Percent, 10.39F);
                }
                pnl_userdetails.Visible = true;
                pnl_userdetails.Dock = DockStyle.Fill;

                textb_shape();
   
            }
        }

        private void txtph_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') 
            {
                e.Handled = true; 
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtemailid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') 
            {
                e.Handled = true; 
            }
        }

        private void txtcusname_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') 
            {
                e.Handled = true; 
            }
        }

        private void txtemailid_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtemailid.Text))
            {
                 if(ClsGeneral.CheckEmailId(this, txtemailid.Text) != true)
                    txtemailid.Focus();
            }
        }

        private void txtph_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtph.Text))
            {
                if (txtph.Text.Length > 10)
                {
                    CMessageBox.Show("Please Enter a Valid Phone Number!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtph.Focus();
                }
            }
        }
        private void btn_edit_cus_Click(object sender, EventArgs e)
        {
            txtph.Enabled = true;
            txtemailid.Enabled = true;
            txtaddress.Enabled = true;
            txtcusname.Enabled = true;
            rb_female.Enabled = true;
            rb_male.Enabled = true;
        }

        private void btn_clear_cus_Click(object sender, EventArgs e)
        {
            txtph.Text = "";
            txtemailid.Text = "";
            txtaddress.Text = "";
            txtcusname.Text = "";
            rb_female.Checked = false;
            rb_male.Checked = false;
        }

        private void btn_multiprice_Click(object sender, EventArgs e)
        {
            pnlprice.Visible = false;
        }

        private void btn_savekot_print_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck_KOT())
                {
                    bool flage = false;
                    DmnOrderMaster dmnordermaster = new DmnOrderMaster();
                    DmnOrderDetails dmnorderdetail = new DmnOrderDetails();
                    BllOrder bll_order = new BllOrder();
                    dmnordermaster.BookmasterId = Bookingmasterid;
                    dmnordermaster.OrderMasterId = Convert.ToInt32(txtordermasterid.Text);
                    dmnordermaster.EntryNo = "";
                    dmnordermaster.TotalQuantity = Convert.ToInt32(txttotalQTY.Text);
                    dmnordermaster.CustomerName = Convert.ToString(txtcusname.Text);
                    dmnordermaster.Telephone = Convert.ToString(txtph.Text);
                    dmnordermaster.Email = Convert.ToString(txtemailid.Text);
                    dmnordermaster.Address = Convert.ToString(txtaddress.Text);
                    if (rb_male.Checked)
                        dmnordermaster.Gender = 1;
                    else
                        dmnordermaster.Gender = 2;

                    dmnordermaster.BranchId = FrmDashboard.BranchId;
                    dmnordermaster.CreatedBy = FrmDashboard.UserId;
                    dmnordermaster.Status = 1;
                    dmnordermaster.Tid = tableId;
                    var InsertStatusP = bll_order.AddEditData("prdInsert_Upd_OrderMaster", dmnordermaster);
                    if (InsertStatusP.OrderMasterId > 0)
                    {
                        ResetFlag(Convert.ToInt32(txtordermasterid.Text));

                        for (int i = 0; i < Dgv_Selected_Item.Rows.Count; i++)
                        {
                            if (Booking_Status)
                            {
                                if (Dgv_Selected_Item.Rows[i].Cells[iitemid].Value != null && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) > 0)
                                {
                                    dmnorderdetail.OrderMasterId = Convert.ToInt32(InsertStatusP.OrderMasterId);
                                    dmnorderdetail.ItemBarCode = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ibarcode].Value);

                                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == 3)
                                        dmnorderdetail.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iproducttypeno].Value);
                                    else
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
                                    dmnorderdetail.Tax = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itax].Value);
                                    dmnorderdetail.TaxType = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itaxtype].Value);

                                    dmnorderdetail.IGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value);
                                    dmnorderdetail.CGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value) / 2;
                                    dmnorderdetail.SGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value) / 2;
                                    dmnorderdetail.VATAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ivatamtno].Value);

                                    dmnorderdetail.ItemId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value);
                                    dmnorderdetail.Srate = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[israte].Value);
                                    dmnorderdetail.MRP = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[imrp].Value);
                                    dmnorderdetail.Fcid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itype].Value);
                                    dmnorderdetail.TableType = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value);
                                    dmnorderdetail.MarketPrice = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value);
                                    if (txtentrie_descr.Text == "Description Type here .....")
                                        dmnorderdetail.MainRemarks = "";
                                    else
                                        dmnorderdetail.MainRemarks = txtentrie_descr.Text;

                                    dmnorderdetail.BranchId = FrmDashboard.BranchId;
                                    dmnorderdetail.CreatedBy = FrmDashboard.UserId;
                                    dmnorderdetail.Status = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value);

                                    dmnorderdetail.OrderDetailsId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iorderdetailsidno].Value);
                                    dmnorderdetail.NetAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);
                                    dmnorderdetail.PrId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value);
                                    dmnorderdetail.Notes = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value);
                                    dmnorderdetail.OrderNo = ResetFlagvalue;
                                    dmnorderdetail.BookingFlag = 1;
                                    dmnorderdetail.KOTPrintStatus = 1;
                                    dmnorderdetail.Tid = Convert.ToInt32(txttid.Text);

                                    var InsertStatus = bll_order.AddEditDataDet("prdInsert_Upd_OrderDetails", dmnorderdetail);
                                    if (InsertStatus == null)
                                    {
                                        flage = true;
                                        CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        break;
                                    }

                                }
                            }
                            else
                            {
                                if (Dgv_Selected_Item.Rows[i].Cells[iqty].Value != null && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) > 0)
                                {
                                    dmnorderdetail.OrderMasterId = Convert.ToInt32(InsertStatusP.OrderMasterId);
                                    dmnorderdetail.ItemBarCode = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ibarcode].Value);

                                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == 3)
                                        dmnorderdetail.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iproducttypeno].Value);
                                    else
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
                                    dmnorderdetail.Tax = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itax].Value);
                                    dmnorderdetail.TaxType = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itaxtype].Value);

                                    dmnorderdetail.IGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value);
                                    dmnorderdetail.CGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value) / 2;
                                    dmnorderdetail.SGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value) / 2;
                                    dmnorderdetail.VATAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ivatamtno].Value);

                                    dmnorderdetail.ItemId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value);
                                    dmnorderdetail.Srate = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[israte].Value);
                                    dmnorderdetail.MRP = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[imrp].Value);
                                    dmnorderdetail.Fcid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itype].Value);
                                    dmnorderdetail.TableType = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value);
                                    dmnorderdetail.MarketPrice = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value);

                                    if (txtentrie_descr.Text == "Description Type here .....")
                                        dmnorderdetail.MainRemarks = "";
                                    else
                                        dmnorderdetail.MainRemarks = txtentrie_descr.Text;


                                    dmnorderdetail.BranchId = FrmDashboard.BranchId;
                                    dmnorderdetail.CreatedBy = FrmDashboard.UserId;
                                    dmnorderdetail.Status = 1;

                                    dmnorderdetail.OrderDetailsId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iorderdetailsidno].Value);
                                    dmnorderdetail.NetAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);
                                    dmnorderdetail.PrId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value);
                                    dmnorderdetail.Notes = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value);
                                    dmnorderdetail.KOTPrintStatus = 1;
                                    dmnorderdetail.BookingFlag = 0;
                                    dmnorderdetail.OrderNo = ResetFlagvalue;
                                    dmnorderdetail.Tid = Convert.ToInt32(txttid.Text);

                                    var InsertStatus = bll_order.AddEditDataDet("prdInsert_Upd_OrderDetails", dmnorderdetail);
                                    if (InsertStatus == null)
                                    {
                                        flage = true;
                                        CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        break;
                                    }

                                }
                            }


                        }
                    }
                    if (flage == false)
                    {
                        if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                        {
                            FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                            _instanceFrmdash.LoadTableStaus(0);
                        }
                        //if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                        //{
                        //    FrmOrderView _instanceFrmOrderView = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                        //    _instanceFrmOrderView.FillOrders();
                        //}
                        //if (System.Windows.Forms.Application.OpenForms["FrmKitchenView"] != null)
                        //{
                        //    FrmKitchenView _instanceFrmKitchenView = Application.OpenForms["FrmKitchenView"] as FrmKitchenView;
                        //    _instanceFrmKitchenView.Fillorders();
                        //}
                        CMessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Notify the kitchen view to refresh
                        ClearAll();
                        this.Close();
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
