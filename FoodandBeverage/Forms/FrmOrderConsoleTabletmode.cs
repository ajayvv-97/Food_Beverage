using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmOrderConsoleTabletmode : Form
    {
        #region Declaration 

        public int tableId = 0;
        public int Bookingmasterid = 0;
        public int BookingType = 0;
        public int PageType = 0;
        public int OmId = 0;
        public int sourcepage = 0;
        public int OrderCancelStatus = 0;
        private List<DmnNameList> checkedtableNames;

        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsgen = new ClsGeneral();

        private Font f11;
        private Font f9;
        private Font f10;
        private Font f10Strike;
        private Font f10Bold;
        private static readonly Font F10 = AppFonts.GetFont(10);
        private static readonly Font F10Strike = new Font(AppFonts.GetFont(10), FontStyle.Strikeout);
        private static readonly Font F10Bold = new Font(AppFonts.GetFont(10), FontStyle.Bold);
        public readonly Font normalFont = new Font("Segoe UI", 10, FontStyle.Regular);

        private ToolTip sideMenuToolTip;

        int maxColumns = 0;
        int spacing = 0;
        int containerWidth = 0;
        int tileW = 0;
        int tileH = 0;

        int Pagecount = Convert.ToInt32(ConfigurationManager.AppSettings["FBpagescount"]);
        int currentPage = 1;
        int pageSize = 0;
        int totalPages = 0;
        private List<DmnGridFields> ProductData;
        private List<DmnGridFields> pagedatadata;
        public int McategoryID = 0;

        public string GameDB = Convert.ToString(ConfigurationManager.AppSettings["GameDB"]);

        public int columncount = Convert.ToInt32(ConfigurationManager.AppSettings["Tmode_OrderPageProductViewRow"]);
        public decimal Productheight = Convert.ToDecimal(ConfigurationManager.AppSettings["Tmode_Productheight"]);
        public decimal Productwidth = Convert.ToDecimal(ConfigurationManager.AppSettings["Tmode_Productwidth"]);
        public int Productspace = Convert.ToInt32(ConfigurationManager.AppSettings["Tmode_Productspace"]);
        public int columncount_offer = Convert.ToInt32(ConfigurationManager.AppSettings["Tmode_OrderPageProductViewRow_Offer"]);

        #region main product variable
        public int Global_ProductId = 0;
        List<DmnOrderMaster> gridSourcemaster = new List<DmnOrderMaster>();
        List<DmnOrderMaster> gridSourcemaster_res = new List<DmnOrderMaster>();
        List<DmnGridFields> gridSource_offer = new List<DmnGridFields>();
        public int r1 = 0;

        public bool OrderRunning_Status = false;
        public bool Booking_Status = false;
        public bool combined_Status = false;
        public bool Pickup_Status = false;
        public bool combined_Pickup_Status = false;
        public bool BillCopy_Status = false;
        public bool KOT_saleprint_status = false;
        public int ordercount = 0;
        public DateTime OrderDateTime;
        #endregion

        bool isOperationShown = false;

        private Button previousClickedButton = null;
        public List<Variation_addon_List> Checked_VariationAddon_List = new List<Variation_addon_List>();
        public string addon_Vari_Id = "";
        public bool variflag = true, addonflag = true;
        public int Split_Rowno = 0;
        public int maxLength_variation = 3;
        Color seleproduct_buttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);

        #region Grid Declaration 
        // Registration Grid
        public int iaction = 0;
        public int islno = 1;
        public int ibarcode = 2;
        public int iproducttypeno = 3;
        public int iname = 4;
        public int iunit = 5;
        public int israte = 6;
        public int iqty = 7;
        public int iprate = 8;
        public int iamount = 9;
        public int idisper = 10;
        public int idisamt = 11;
        public int itaxper = 12;
        public int itaxamt = 13;
        public int itotal = 14;
        public int iitemid = 15;
        public int itax = 16;
        public int itaxtype = 17;
        public int imrp = 18;
        public int itype = 19;
        public int ibarcode2 = 20;
        public int itaxsettingid = 21;
        public int iunitid = 22;
        public int ipridno = 23;
        public int iprodesno = 24;
        public int istatusno = 25;
        public int iigstamtno = 26;
        public int ivatamtno = 27;
        public int iorderdetailsidno = 28;
        public int isubofferstatus = 29;
        public int ioriginalprice = 30;
        public int isubofferid = 31;
        public int itabletype = 32;
        public int iorderdescno = 33;
        public int ioriginalqty = 34;
        public int iavid = 35;
        public int ivaraiationstatusno = 36;
        public int ipavidno = 37;
        public int iMainProductStatusno = 38;
        public int iAddoncountno = 39;
        public int ividno = 40;
        public int iComboTaxamtno = 41;
        public int iccodeno = 42;
        public int iVariatiionNameno = 43;
        public int iPreparationModeno = 44;
        public int isaledetailsidno = 45;


        public int Cbarcode = 0;
        public int vname = 1;
        public int ciunit = 2;
        public int cqty = 3;
        public int cdisper = 4;
        public int cdisamt = 5;
        public int ctaxper = 6;
        public int ctaxamt = 7;
        public int camount = 8;
        public int ctotal = 9;
        public int citemid = 10;
        public int cpridno = 11;
        public int ctax = 12;
        public int ctaxtype = 13;
        public int Csrate = 14;
        public int Cmrp = 15;
        public int Comproducttypeno = 16;
        public int cigstamt = 17;
        public int CCgstamt = 18;
        public int cSgstamt = 19;
        public int cvatamt = 20;
        public int cmarketprice = 21;
        public int Ctabletype = 22;
        public int CPavid = 23;
        public int CAddonStatus = 24;
        public int CAV_id = 25;
        public int CAddoncount = 26;
        public int Cstatusno = 27;
        public int CVariatiionNameno = 28;
        public int CSalesMasterIdno = 29;
        public int CSalesDetailsIdno = 30;
        public int COrderDetailsIdno = 31;

        public int tslno = 0;
        public int ttablenameno = 1;
        public int ttablecategno = 2;
        public int ttablechargeno = 3;
        public int tactionno = 4;
        public int tbookingdetailsid = 5;
        public int tbookingmasteridno = 6;
        public int ttidno = 7;

        public int cnslno = 0;
        public int cnnameno = 1;
        public int cnqtyno = 2;
        public int cnpriceno = 3;
        public int cnreasonno = 4;
        public int cnactionno = 5;
        public int cnordermasteridno = 6;
        public int cnorderdetailsidno = 7;
        public int cnpickupidno = 8;


        #endregion

        #region Declaration image
        private readonly Image imgTime = Properties.Resources.time;
        private readonly Image imgComplete = Properties.Resources.orderview_complete;
        private readonly Image imgKitchen = Properties.Resources.kitchen_oder_icon;
        private readonly Image ImgClose = Properties.Resources.close_price_change;
        private readonly Image Imgaddon = Properties.Resources.addon_comb_item;
        #endregion


        bool DirectPayBill = false;
        int refundsmid = 0, Refundkitchenslno = 0, Refundorderno = 0; decimal refundtotal = 0, Refundproductwisetotal = 0;
        bool Refundproductflag = false;
        public bool itemcode_serach_Status = false;
        TableLayoutPanel tlp_price_list = new TableLayoutPanel();
        public int Productrate_ID = 0;

        #endregion
        public FrmOrderConsoleTabletmode(int tid, int booktype, int bmasterid, List<DmnNameList> checkedtableNames, int Pageno, int Ordermasterid, int sourcepagetype, int cdstatus)
        {
            tableId = tid;
            sourcepage = sourcepagetype;
            Bookingmasterid = bmasterid;
            BookingType = booktype;
            PageType = Pageno;
            this.checkedtableNames = checkedtableNames;
            OmId = Ordermasterid;
            OrderCancelStatus = cdstatus;

            InitializeComponent();

            f11 = new Font("Microsoft Sans Serif", 6f, FontStyle.Bold);
            f9 = new Font("Microsoft Sans Serif", 4f, FontStyle.Bold);
            f10 = new Font("Microsoft Sans Serif", 5f, FontStyle.Bold);

            f10Strike = new Font(f10, FontStyle.Strikeout);
            f10Bold = new Font(f10, FontStyle.Bold);
        }
        #region please wait loading
        private async void ShowLoadingdGif()
        {
            this.Opacity = 0;
            using (LoadingGif gifForm = new LoadingGif())
            {
                gifForm.StartPosition = FormStartPosition.CenterScreen;
                gifForm.ShowDialog();
            }
            this.Opacity = 100;
        }
        #endregion
        private void FrmOrderConsoleTabletmode_Load(object sender, EventArgs e)
        {
            //AutoCloseTimer.Interval = 3000; // close after 3 sec
            //AutoCloseTimer.Tick += AutoCloseTimer_Tick;
            //AutoCloseTimer.Start();

            ShowLoadingdGif();
            pageSize = Pagecount;

            #region ChkWritePermission
            if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
            {
                FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                if (_instanceFrmdash.ChkWritePermission(Convert.ToInt32(formsenum.Order)))
                    btn_saveKOT.Enabled = true;
                else
                    btn_saveKOT.Enabled = false;
            }
            #endregion

            #region Grid color setting
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

            //dgv_split.BorderStyle = BorderStyle.None;
            //dgv_split.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            //dgv_split.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            //dgv_split.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            //dgv_split.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            //dgv_split.BackgroundColor = Color.White;

            //dgv_split.EnableHeadersVisualStyles = false;
            //dgv_split.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            //dgv_split.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            //dgv_split.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


            //dgv_tableprice.BorderStyle = BorderStyle.None;
            //dgv_tableprice.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            //dgv_tableprice.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            //dgv_tableprice.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            //dgv_tableprice.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            //dgv_tableprice.BackgroundColor = Color.White;

            //dgv_tableprice.EnableHeadersVisualStyles = false;
            //dgv_tableprice.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            //dgv_tableprice.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            //dgv_tableprice.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            #endregion

            #region BookingType Checking [Normalorder ,Reverse_overtime_Cancel , CombinedOrderBill,Pickup Order]
            if (BookingType == Convert.ToInt32(DashboardTableType.Normalorder))
            {
                if (OrderCancelStatus == 1)
                {
                    btncancelnotify.Visible = true;
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "OrderMasterId Field1,SalesMasterId Field2,PkId Field3";
                    dmnSearch.Table = "tblOrderMaster";
                    dmnSearch.WhereNdGroupBy = "WHERE Status=0 and tid = " + tableId;
                    dmnSearch.OrderBy = " order by OrderMasterId desc";
                    var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    dgv_ordercancel.Rows.Clear();
                    if (gridSource.Count > 0)
                    {
                        txtordermasterid.Text = gridSource[0].Field1;
                    }
                }
                else
                    btncancelnotify.Visible = false;

                CheckRe_order();
                ChecktablekitchenCancel();
            }
            else if (BookingType == Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel))
                CheckRe_Bookingorder();
            
            #endregion

            #region Palce Holder  Textboxes setting
            txtname_search.Text = "Food Name .....";
            txtitemcode_serach.Text = "Item Code .....";
            //txtentrie_descr.Text = "Description Type here .....";
            #endregion

            textb_shape();
            //ShowLoadingdGif();
            setperfixsale();
            settablecode();

            //btn_clear_cus.Visible = true;

            if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
            {
                FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                _instanceFrmdash.Visible = true;
            }

            if (tlp_netalc_buttons.Controls.Contains(flp_oper_buttons))
                tlp_netalc_buttons.Controls.Remove(flp_oper_buttons);

            tlp_netalc_buttons.Controls.Add(tlp_calculation_total, 0, 0);
            tlp_calculation_total.Dock = DockStyle.Fill;

            textb_shape_paymnet();
        }

        #region Common Used Fuction
        public void discountPer()
        {
            try
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
            catch (Exception ex)
            {
                clsgen.LogException("Order page  discountPer()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void discountAmnt()
        {
            try
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
            catch (Exception ex)
            {
                clsgen.LogException("Order page  discountAmnt()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        #region Fresh or Reorder table
        public void CheckRe_order()
        {
            try
            {
                BllSearch<DmnOrderMaster> bllSearchmaster = new BllSearch<DmnOrderMaster>();
                DmnSearch dmnSearchmaster = new DmnSearch();
                dmnSearchmaster.Select = "*";
                dmnSearchmaster.Table = "tblOrderMaster om left join tblOrderDetails od on od.OrderMasterId = om.OrderMasterId and od.Status IN (1, 2, 3) ";
                dmnSearchmaster.WhereNdGroupBy = "where om.tid= " + tableId + " and om.Status IN (1)";
                dmnSearchmaster.OrderBy = " order by OrderNo ";
                gridSourcemaster = bllSearchmaster.GetSearchData("prdSearch", dmnSearchmaster);
                if (gridSourcemaster.Count > 0)
                {
                    tlp_refund_ktcancel.Visible = false;
                    dgv_ordercancel.Columns[cnactionno].Visible = true;
                    int orginalCount = gridSourcemaster.Count(item => item.AddonStatus == 1 || item.AddonStatus == 0);
                    OrderDateTime = gridSourcemaster[0].CreatedDateTime;
                    txtGrandTotal.Text = "0";

                    if (Convert.ToInt32(gridSourcemaster[0].SalesMasterId) > 0)
                        txtsalemasterid.Text = Convert.ToString(gridSourcemaster[0].SalesMasterId);
                    else
                        txtsalemasterid.Text = "0";

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
                    btn_userreg_submit.Visible = true;
                    btn_clear_cus.Visible = true;

                    ordercount = 0;
                    OrderRunning_Status = true;
                    Dgv_Selected_Item.Rows.Clear();

                    lbltablecode.Visible = true;
                    lblorderid.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);

                    //label18.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);
                    //label25.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);

                    lblorderid.Visible = true;
                    label10.Visible = true;

                    var distinctCategories = gridSourcemaster
                        .Select(item => item.OrderNo)
                        .Distinct()
                        .ToList();

                    int j = 0;
                    int r = 0;
                    int SL_Main = 0;
                    int SL_Addon = 0;
                    decimal gtotal = 0;

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
                            #region Banner            
                            Dgv_Selected_Item.Rows.Add();
                            string bannerText =
                            "Order No : " + orderNo + "|Time : " + Convert.ToDateTime(gridSource[0].CreatedDateTime).ToString("hh:mm tt");
                            // Set the image
                            DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)Dgv_Selected_Item.Rows[r].Cells[iaction];
                            cellcolumntype.Value = imgTime;
                            cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;

                            Dgv_Selected_Item.Rows[r].Cells[iname].Value = bannerText;
                            Dgv_Selected_Item.Rows[r].DefaultCellStyle.BackColor = Color.LightGray;
                            Dgv_Selected_Item.Rows[r].DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                            Dgv_Selected_Item.Rows[r].DefaultCellStyle.ForeColor = Color.Black;
                            Dgv_Selected_Item.Rows[r].ReadOnly = true;
                            #endregion

                            r = r + 1;
                            txtordermasterid.Text = Convert.ToString(gridSourcemaster[j].OrderMasterId);
                            txtentrie_descr.Text = Convert.ToString(gridSourcemaster[j].Remarks);
                            for (int i = 0; i < gridSource.Count; i++)
                            {
                                if (gridSource[i].Status == 3 && (gridSource[i].AddonStatus == 1 || gridSource[i].AddonStatus == 0))
                                    ordercount = ordercount + 1;

                                gtotal = gtotal + Convert.ToDecimal(gridSource[i].NetAmount);

                                if (gridSource[i].TableType == 1)
                                {
                                    #region Grid Inseration product
                                    Dgv_Selected_Item.Rows.Add();
                                    var currentFont = Dgv_Selected_Item.Rows[r].Cells[islno].Style.Font ?? Dgv_Selected_Item.Font;
                                    if (gridSource[i].AddonStatus == 0 || gridSource[i].AddonStatus == (int)Product_Addon_Variation_Type.Variation)
                                    {
                                        SL_Main++;
                                        SL_Addon = 0;
                                        Dgv_Selected_Item.Rows[r].Cells[islno].Value = SL_Main;
                                        Dgv_Selected_Item.Rows[r].Cells[islno].Style.Font = new Font(currentFont, FontStyle.Bold);
                                        Dgv_Selected_Item.Rows[r].Cells[iname].Style.Font = new Font(currentFont, FontStyle.Bold);
                                        Dgv_Selected_Item.Rows[r].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        currentFont = Dgv_Selected_Item.Rows[r].Cells[iname].Style.Font ?? Dgv_Selected_Item.Font;
                                    }
                                    else if (gridSource[i].AddonStatus == 1)
                                    {
                                        SL_Addon = 1;
                                        Dgv_Selected_Item.Rows[r].Cells[islno].Value = SL_Addon;
                                        currentFont = Dgv_Selected_Item.Rows[r].Cells[iname].Style.Font ?? Dgv_Selected_Item.Font;
                                        Dgv_Selected_Item.Rows[r].Cells[iname].Style.Font = new Font(currentFont, FontStyle.Bold);
                                        Dgv_Selected_Item.Rows[r].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                    }
                                    else if (gridSource[i].AddonStatus == 2)
                                    {
                                        SL_Addon++;
                                        Dgv_Selected_Item.Rows[r].Cells[islno].Value = SL_Addon;
                                        Dgv_Selected_Item.Rows[r].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                    }

                                    Dgv_Selected_Item.Rows[r].Cells[ibarcode].Value = Convert.ToString(gridSource[i].ItemBarCode);
                                    Dgv_Selected_Item.Rows[r].Cells[ibarcode2].Value = Convert.ToString(gridSource[i].ItemBarCode);
                                    Dgv_Selected_Item.Rows[r].Cells[iname].Value = Convert.ToString(gridSource[i].ItemName);
                                    Dgv_Selected_Item.Rows[r].Cells[iqty].Value = Convert.ToInt32(gridSource[i].Quantity);
                                    Dgv_Selected_Item.Rows[r].Cells[iprate].Value = Convert.ToDecimal(gridSource[i].PurchaseRate);
                                    Dgv_Selected_Item.Rows[r].Cells[israte].Value = Convert.ToDecimal(gridSource[i].Srate);
                                    Dgv_Selected_Item.Rows[r].Cells[iamount].Value = Convert.ToDecimal(gridSource[i].Amount);
                                    Dgv_Selected_Item.Rows[r].Cells[imrp].Value = Convert.ToDecimal(gridSource[i].MRP);
                                    Dgv_Selected_Item.Rows[r].Cells[idisper].Value = Convert.ToDecimal(gridSource[i].DiscountPer);
                                    Dgv_Selected_Item.Rows[r].Cells[idisamt].Value = Convert.ToDecimal(gridSource[i].DiscountAmount);
                                    Dgv_Selected_Item.Rows[r].Cells[iitemid].Value = Convert.ToInt32(gridSource[i].ItemId);
                                    // Tax
                                    Dgv_Selected_Item.Rows[r].Cells[itaxper].Value = Convert.ToString(gridSource[i].TaxPer);
                                    Dgv_Selected_Item.Rows[r].Cells[itaxamt].Value = Convert.ToString(gridSource[i].TaxAmount);
                                    Dgv_Selected_Item.Rows[r].Cells[itax].Value = Convert.ToString(gridSource[i].Tax);
                                    Dgv_Selected_Item.Rows[r].Cells[itaxtype].Value = Convert.ToString(gridSource[i].TaxType);

                                    Dgv_Selected_Item.Rows[r].Cells[itotal].Value = Convert.ToDecimal(gridSource[i].NetAmount);
                                    Dgv_Selected_Item.Rows[r].Cells[itype].Value = Convert.ToInt32(gridSource[i].TableType);
                                    Dgv_Selected_Item.Rows[r].Cells[istatusno].Value = 0;
                                    Dgv_Selected_Item.Rows[r].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Primary);
                                    Dgv_Selected_Item.Rows[r].DefaultCellStyle.BackColor = Color.White;
                                    Dgv_Selected_Item.Rows[r].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
                                    Dgv_Selected_Item.Rows[r].Cells[itabletype].Value = Convert.ToInt32(Tabletype.Product);
                                    Dgv_Selected_Item.Rows[r].Cells[ioriginalprice].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[r].Cells[itotal].Value);

                                    if (Convert.ToInt32(gridSource[i].Status) == 3 && (gridSource[i].AddonStatus == 1 || gridSource[i].AddonStatus == 0))
                                    {
                                        cellcolumntype = (DataGridViewImageCell)Dgv_Selected_Item.Rows[r].Cells[iaction];
                                        cellcolumntype.Value = imgComplete;
                                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                    }
                                    else if (Convert.ToInt32(gridSource[i].Status) == 1 && (gridSource[i].AddonStatus == 1 || gridSource[i].AddonStatus == 0))
                                    {
                                        cellcolumntype = (DataGridViewImageCell)Dgv_Selected_Item.Rows[r].Cells[iaction];
                                        cellcolumntype.Value = imgKitchen;
                                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                    }

                                    ((DataGridViewImageCell)Dgv_Selected_Item.Rows[r].Cells[iaction]).ImageLayout = DataGridViewImageCellLayout.Zoom;

                                    r++;
                                    #endregion
                                }
                                else if (gridSource[i].TableType == 2)
                                {
                                    #region Grid Inseration multiple 
                                    SL_Main++;
                                    Dgv_Selected_Item.Rows.Add();
                                    Dgv_Selected_Item.Rows[r].Cells[islno].Value = SL_Main;
                                    var currentFont = Dgv_Selected_Item.Rows[r].Cells[islno].Style.Font ?? Dgv_Selected_Item.Font;
                                    Dgv_Selected_Item.Rows[r].Cells[islno].Style.Font = new Font(currentFont, FontStyle.Bold);

                                    Dgv_Selected_Item.Rows[r].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                    Dgv_Selected_Item.Rows[r].Cells[ibarcode].Value = gridSource[i].ItemBarCode;
                                    Dgv_Selected_Item.Rows[r].Cells[ibarcode2].Value = gridSource[i].ItemBarCode;
                                    Dgv_Selected_Item.Rows[r].Cells[iname].Value = gridSource[i].ItemName;
                                    currentFont = Dgv_Selected_Item.Rows[r].Cells[iname].Style.Font ?? Dgv_Selected_Item.Font;
                                    Dgv_Selected_Item.Rows[r].Cells[iname].Style.Font = new Font(currentFont, FontStyle.Bold);

                                    Dgv_Selected_Item.Rows[r].Cells[iunitid].Value = gridSource[i].UnitId;
                                    Dgv_Selected_Item.Rows[r].Cells[iqty].Value = "1";
                                    Dgv_Selected_Item.Rows[r].Cells[iprate].Value = gridSource[i].PurchaseRate;
                                    Dgv_Selected_Item.Rows[r].Cells[israte].Value = gridSource[i].Srate;
                                    Dgv_Selected_Item.Rows[r].Cells[imrp].Value = gridSource[i].MRP;
                                    Dgv_Selected_Item.Rows[r].Cells[idisamt].Value = "0";
                                    Dgv_Selected_Item.Rows[r].Cells[iitemid].Value = gridSource[i].ItemId;
                                    Dgv_Selected_Item.Rows[r].Cells[itax].Value = gridSource[i].Tax;
                                    Dgv_Selected_Item.Rows[r].Cells[itaxtype].Value = gridSource[i].TaxType;
                                    Dgv_Selected_Item.Rows[r].Cells[itype].Value = gridSource[i].TableType;
                                    Dgv_Selected_Item.Rows[r].Cells[ipridno].Value = gridSource[i].PrId;
                                    Dgv_Selected_Item.Rows[r].Cells[istatusno].Value = 0;
                                    Dgv_Selected_Item.Rows[r].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Primary);
                                    Dgv_Selected_Item.Rows[r].DefaultCellStyle.BackColor = Color.White;
                                    Dgv_Selected_Item.Rows[r].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
                                    Dgv_Selected_Item.Rows[r].Cells[itabletype].Value = Convert.ToInt32(Tabletype.DirectSale);

                                    if (Convert.ToInt32(gridSource[i].Status) == 3 && (gridSource[i].AddonStatus == 1 || gridSource[i].AddonStatus == 0))
                                    {
                                        cellcolumntype = (DataGridViewImageCell)Dgv_Selected_Item.Rows[r].Cells[iaction];
                                        cellcolumntype.Value = imgComplete;
                                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                    }
                                    else if (Convert.ToInt32(gridSource[i].Status) == 1 && (gridSource[i].AddonStatus == 1 || gridSource[i].AddonStatus == 0))
                                    {
                                        cellcolumntype = (DataGridViewImageCell)Dgv_Selected_Item.Rows[r].Cells[iaction];
                                        cellcolumntype.Value = imgKitchen;
                                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                    }

                                    Dgv_Selected_Item.Rows[r].Cells[ioriginalprice].Value =
                                    Convert.ToDecimal(Dgv_Selected_Item.Rows[r].Cells[itotal].Value);

                                    r++;
                                    #endregion
                                }
                                else if (gridSource[i].TableType == 3)
                                {
                                    BllSearch<DmnGridFields> bllSearch3 = new BllSearch<DmnGridFields>();
                                    DmnSearch dmnSearch3 = new DmnSearch();
                                    dmnSearch3.Select = "ofm.OfferName Field1, ofm.FinalPrice Field2, ofm.TaxType Field3, ofm.Tax Field4, ofm.TaxSettingsId Field5, ofm.Discountper Field6, ofd.Productname Field7";
                                    dmnSearch3.Table = "tblOfferMaster ofm LEFT JOIN tblOfferDetails ofd on ofm.Omid=ofd.Omid";
                                    dmnSearch3.WhereNdGroupBy = "where ofm.Status=1 and ofm.Omid=" + gridSource[i].ItemId;
                                    dmnSearch3.OrderBy = "";
                                    var offerDetails = bllSearch3.GetSearchData("prdSearch", dmnSearch3);

                                    if (offerDetails.Count > 0)
                                    {
                                        #region Grid inseration Combo Product

                                        SL_Main++;
                                        Dgv_Selected_Item.Rows.Add();
                                        Dgv_Selected_Item.Rows[r].Cells[islno].Value = SL_Main;
                                        Dgv_Selected_Item.Rows[r].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        var currentFont = Dgv_Selected_Item.Rows[r].Cells[islno].Style.Font ?? Dgv_Selected_Item.Font;
                                        Dgv_Selected_Item.Rows[r].Cells[islno].Style.Font = new Font(currentFont, FontStyle.Bold);

                                        Dgv_Selected_Item.Rows[r].Cells[iname].Value = Convert.ToString(offerDetails[0].Field1);
                                        currentFont = Dgv_Selected_Item.Rows[r].Cells[iname].Style.Font ?? Dgv_Selected_Item.Font;
                                        Dgv_Selected_Item.Rows[r].Cells[iname].Style.Font = new Font(currentFont, FontStyle.Bold);

                                        Dgv_Selected_Item.Rows[r].Cells[iproducttypeno].Value = Convert.ToString(offerDetails[0].Field1);
                                        Dgv_Selected_Item.Rows[r].Cells[iqty].Value = gridSource[i].Quantity;
                                        Dgv_Selected_Item.Rows[r].Cells[israte].Value = Convert.ToDecimal(offerDetails[0].Field2);
                                        Dgv_Selected_Item.Rows[r].Cells[itaxtype].Value = Convert.ToString(offerDetails[0].Field3);
                                        Dgv_Selected_Item.Rows[r].Cells[itax].Value = Convert.ToString(offerDetails[0].Field4);
                                        Dgv_Selected_Item.Rows[r].Cells[itaxsettingid].Value = Convert.ToInt32(offerDetails[0].Field5);
                                        Dgv_Selected_Item.Rows[r].Cells[idisper].Value = Convert.ToDecimal(offerDetails[0].Field6);
                                        Dgv_Selected_Item.Rows[r].Cells[iitemid].Value = Convert.ToInt32(gridSource[i].ItemId);
                                        Dgv_Selected_Item.Rows[r].Cells[iproducttypeno].Style.Font = new Font("Arial", 12, FontStyle.Bold);
                                        Dgv_Selected_Item.Rows[r].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Primary);
                                        Dgv_Selected_Item.Rows[r].Cells[istatusno].Value = 0;
                                        Dgv_Selected_Item.Rows[r].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
                                        Dgv_Selected_Item.Rows[r].Cells[isubofferid].Value = 0;
                                        Dgv_Selected_Item.Rows[r].Cells[itabletype].Value = Convert.ToInt32(Tabletype.ComboProduct);

                                        if (Convert.ToInt32(gridSource[i].Status) == 3 && (gridSource[i].AddonStatus == 1 || gridSource[i].AddonStatus == 0))
                                        {
                                            cellcolumntype = (DataGridViewImageCell)Dgv_Selected_Item.Rows[r].Cells[iaction];
                                            cellcolumntype.Value = imgComplete;
                                            cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                        }
                                        else if (Convert.ToInt32(gridSource[i].Status) == 1 && (gridSource[i].AddonStatus == 1 || gridSource[i].AddonStatus == 0))
                                        {
                                            cellcolumntype = (DataGridViewImageCell)Dgv_Selected_Item.Rows[r].Cells[iaction];
                                            cellcolumntype.Value = imgKitchen;
                                            cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                        }

                                        Dgv_Selected_Item.Rows[r].Cells[ioriginalprice].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[r].Cells[itotal].Value);
                                        r++;

                                        #region Combo Detailed Product Lisddt Show
                                        //for (int k = 0; k < offerDetails.Count; k++)
                                        //{
                                        //    Dgv_Selected_Item.Rows.Add();
                                        //    Dgv_Selected_Item.Rows[r].Cells[islno].Value = k + 1;
                                        //    Dgv_Selected_Item.Rows[r].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                        //    Dgv_Selected_Item.Rows[r].Cells[ibarcode].Value = 0;
                                        //    Dgv_Selected_Item.Rows[r].Cells[ibarcode2].Value = 0;
                                        //    Dgv_Selected_Item.Rows[r].Cells[iname].Value = Convert.ToString(offerDetails[k].Field7);
                                        //    Dgv_Selected_Item.Rows[r].Cells[iprate].Value = 0;
                                        //    Dgv_Selected_Item.Rows[r].Cells[imrp].Value = 0;
                                        //    Dgv_Selected_Item.Rows[r].Cells[idisamt].Value = "0";
                                        //    Dgv_Selected_Item.Rows[r].Cells[itype].Value = 0;
                                        //    Dgv_Selected_Item.Rows[r].Cells[istatusno].Value = 0;
                                        //    Dgv_Selected_Item.Rows[r].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Secondary);
                                        //    Dgv_Selected_Item.Rows[r].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.Enable);
                                        //    Dgv_Selected_Item.Rows[r].Cells[isubofferid].Value = Convert.ToInt32(offerDetails[0].Id);
                                        //    Dgv_Selected_Item.Rows[r].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                                        //    r++;
                                        //}
                                        #endregion

                                        #endregion
                                    }
                                }
                            }
                        }
                        j = j + 1;
                    }
                    txtGrandTotal.Text = Convert.ToString(gtotal);

                    if (ordercount == orginalCount && ((PageType == Convert.ToInt32(FoodandBeverage.Interfaces.PageType.Billpage) || (PageType == Convert.ToInt32(FoodandBeverage.Interfaces.PageType.OrderPage)))))
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
                clsgen.LogException("Order page  CheckRe_order()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void ChecktablekitchenCancel()
        {
            try
            {
                // Load Cancel Order Items
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "itemName Field1,Quantity Field2,RemoveNotes Field3,OrderMasterId Field4,OrderDetailsId Field5,NetAmount Field6,CASE WHEN EXISTS(SELECT 1 FROM tblOrderDetails AS x WHERE x.OrderMasterId = tblOrderDetails.OrderMasterId AND x.KitchenCancelStatus = 1 ) THEN 1 ELSE 0 END AS Field7";
                dmnSearch.Table = "tblOrderDetails";
                dmnSearch.WhereNdGroupBy =
                    "WHERE Status = 0 AND AddonStatus IN (1,0) AND CancelNotifyStatus = 1 " +
                    "AND OrderMasterId = " + Convert.ToInt32(lblorderid.Text);
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                dgv_ordercancel.Rows.Clear();
                if (gridSource.Count > 0)
                {
                    if (Convert.ToInt32(gridSource[0].Field7) == 1)
                    {
                        btnreconfirmorder.Visible = true;
                    }
                    else
                    {
                        btnreconfirmorder.Visible = false;
                    }

                    decimal total = 0;
                    dgv_ordercancel.Rows.Add(gridSource.Count);
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        dgv_ordercancel.Rows[i].Cells[cnslno].Value = i + 1;
                        dgv_ordercancel.Rows[i].Cells[cnnameno].Value = Convert.ToString(gridSource[i].Field1);
                        dgv_ordercancel.Rows[i].Cells[cnqtyno].Value = Convert.ToString(gridSource[i].Field2);
                        dgv_ordercancel.Rows[i].Cells[cnpriceno].Value = Convert.ToString(gridSource[i].Field6);
                        dgv_ordercancel.Rows[i].Cells[cnreasonno].Value = gridSource[i].Field3;
                        dgv_ordercancel.Rows[i].Cells[cnordermasteridno].Value = gridSource[i].Field4;
                        dgv_ordercancel.Rows[i].Cells[cnorderdetailsidno].Value = gridSource[i].Field5;
                        //dgv_ordercancel.Rows[i].Cells[cnpickupidno].Value = gridSource[i].Field6;

                        //DataGridViewImageCell imgCell =(DataGridViewImageCell)dgv_ordercancel.Rows[i].Cells[cnactionno];
                        //imgCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        //imgCell.Value = Properties.Resources.Delete_Grid;
                        total = total + Convert.ToDecimal(gridSource[i].Field6);
                    }
                    lblpickupcnlamt.Text = total.ToString("N2");
                    Refundproductwisetotal = total;
                }
                else
                    btnreconfirmorder.Visible = false;
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

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
                    //label15.Text = Convert.ToString(gridSource[0].Field1);
                    //label22.Text = Convert.ToString(gridSource[0].Field1);
                    txttid.Text = Convert.ToString(tableId);
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  settablecode()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
            {

                clsgen.LogException("Order page  setperfixsale()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                //foreach (TextBox textBox in new TextBox[] { txtitemcode_serach, txtname_search, txt_product_des, txtrecvamount, txtGrandTotal, txtpaidamount, txtdis_amt, txtdis_per, txtnetamount, txtentrie_descr, txtdisamt_promo, txtpromocode, txtdiscountamt_promo, txtnumber_split, txtadvance, txtph, txtemailid, txtaddress, txtcusname, txt_split_recamt, txt_split_balamt, txt_cardbalance, txtcardemail, txt_cardno, txt_cardaddress, txt_cardph, txt_cardaccname, txtpkchrge_amt })
                //{
                //    ApplyRoundedTextBox(textBox, 10);
                //    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                //    textBox.BorderStyle = BorderStyle.None;
                //}

            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }
        public void textb_shape_paymnet()
        {
            try
            {
                foreach (TextBox textBox in new TextBox[] { txtrecvamount, txtpaidamount, txtnetamount, txtadvance })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  textb_shape_paymnet", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void ApplyRoundedTextBox(TextBox textBox, int radius)
        {
            try
            {
                using (GraphicsPath path = new GraphicsPath())
                {
                    Rectangle rect = new Rectangle(0, 0, textBox.Width, textBox.Height);
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                    path.CloseAllFigures();

                    textBox.Region?.Dispose(); // Clean previous region (important)
                    textBox.Region = new Region(path);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }


        #endregion

        #region Offer button clcik
        public bool Checkoffer(int id)
        {
            try
            {
                gridSource_offer = null;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();/*LEFT JOIN tblOfferDetails ofd on ofd.ProductId=p.ItemId and ofd.status=1" +
                    " LEFT JOIN tblOfferMaster ofm on ofm.Omid=ofd.Omid AND ofm.OfferType=0 and ofm.status=1*/
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = " p.ItemCode_UPC as Field1, p.Itemname as Field2,p.srate as Field3,p.MRP as Field4,p.ItemId as Field5,p.Tax as Field6,p.TaxType as Field7,p.Fcid as Field8,p.TaxSettingsId as Field9,q.Discountper as Field10";
                dmnSearch.Table = " tblItem p " +
                    " OUTER APPLY ( Select ofd.Discountper,ofd.FinalPrice,ofm.offertype,ofm.OfferstartDatetime,ofm.OfferEndDatetime from  tblOfferDetails ofd   LEFT JOIN tblOfferMaster ofm ON ofd.omid = ofm.omid AND ofd.status = 1  where ofm.status = 1 AND ofd.Producttype=2  AND ofm.OfferType = 0 AND p.itemid = ofd.ProductId and    ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status = 1)q";
                dmnSearch.WhereNdGroupBy = "where p.Status=1 and p.ItemId= " + id + " AND q.OfferstartDatetime <= SYSDATETIME() AND q.OfferEndDatetime >= SYSDATETIME()";
                dmnSearch.OrderBy = "";
                gridSource_offer = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource_offer.Count == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  Checkoffer()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
                return false;
            }
        }
        #endregion
        public void totQty()
        {
            try
            {
                decimal sum = 0;
                int n = Dgv_Selected_Item.RowCount;
                for (int i = 0; i < n; i++)
                {
                    if (Dgv_Selected_Item.Rows[i].Cells[iqty].Value != "")
                    {
                        if ((Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iMainProductStatusno].Value) == Convert.ToInt32(MainProductStatus.Primary)))
                            sum = sum + Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iqty].Value);
                    }

                }
                txttotalQTY.Text = Convert.ToString(sum);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  totQty()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void netAmount()
        {
            try
            {
                #region Setting on textbox Null or Empty

                if (string.IsNullOrEmpty(txtdis_amt.Text))
                    txtdis_amt.Text = "0";
                if (string.IsNullOrEmpty(txtpaidamount.Text))
                    txtpaidamount.Text = "0";
                if (string.IsNullOrEmpty(txtdiscountamt_promo.Text))
                    txtdiscountamt_promo.Text = "0";
                if (string.IsNullOrEmpty(txtadvance.Text))
                    txtadvance.Text = "0";
                if (string.IsNullOrEmpty(txtreservationtotal.Text))
                    txtreservationtotal.Text = "0";
                if (string.IsNullOrEmpty(txtGrandTotal.Text))
                    txtGrandTotal.Text = "0";

                #endregion

                decimal value;
                if (decimal.TryParse(txtdis_amt.Text, out value))
                {
                    decimal netAmount = 0;
                    netAmount = (Convert.ToDecimal(txtGrandTotal.Text)) - (Convert.ToDecimal(txtdis_amt.Text) + Convert.ToDecimal(txtdiscountamt_promo.Text));

                    //if (Booking_Status && (netAmount > Convert.ToDecimal(txtadvance.Text)))
                    //    netAmount = netAmount - Convert.ToDecimal(txtadvance.Text);

                    //if (Pickup_Status || combined_Pickup_Status)
                    //    netAmount = netAmount + Convert.ToDecimal(txtpkchrge_amt.Text);

                    if (Refundproductflag)
                        netAmount = netAmount - Refundproductwisetotal;

                    txtnetamount.Text = netAmount.ToString("N2");
                    //label20.Text = "Net Amount : " + txtnetamount.Text;
                    //label21.Text = "Net Amount : " + txtnetamount.Text;
                    decimal balAmnt = netAmount - Convert.ToDecimal(txtpaidamount.Text);
                    txtrecvamount.Text = balAmnt.ToString("N2");
                }

                totQty();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  netAmount()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                if (OrderCancelStatus == 0)
                {
                    if (typeno == 1)
                    {
                        btn_frm_close.Dock = DockStyle.Left;
                        btn_saveKOT.Visible = true;
                        btn_savekot_print.Visible = false;
                        btn_print_bill.Visible = false;
                        btn_promo_code.Visible = false;
                        btn_PrintCopy.Visible = false;
                        if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                        {
                            btndirectpaybill.Visible = true;
                            btn_savekot_print.Visible = true;
                            btn_saveKOT.Visible = false;
                            btn_promo_code.Visible = true;
                        }
                    }
                    else if (typeno == 2)
                    {
                        btn_frm_close.Dock = DockStyle.Left;
                        btn_saveKOT.Visible = false;
                        btn_savekot_print.Visible = false;
                        btn_print_bill.Visible = true;
                        btn_PrintCopy.Visible = true;
                        btn_promo_code.Visible = true;
                        btndirectpaybill.Visible = true;
                        btn_entire_desc.Visible = false;
                        if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                        {
                            btndirectpaybill.Visible = false;
                        }
                        else if (sourcepage == Convert.ToInt32(SourcepageType.Billingpage))
                        {
                            btndirectpaybill.Visible = false;
                            btn_savekot_print.Visible = false;
                            btn_print_bill.Visible = false;
                            btn_PrintCopy.Visible = false;
                        }
                    }
                    else
                    {
                        btn_frm_close.Dock = DockStyle.Left;
                        btn_saveKOT.Visible = false;
                        btn_savekot_print.Visible = false;
                        btn_print_bill.Visible = false;
                        btn_promo_code.Visible = false;
                        btn_entire_desc.Visible = false;
                        btn_PrintCopy.Visible = false;
                        btndirectpaybill.Visible = false;

                    }
                }
                else
                {
                    btn_frm_close.Dock = DockStyle.Left;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  Showprint_Save()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) > 0 && Dgv_Selected_Item.Rows[i].Cells[iitemid].Value != null)
                    {
                        count = count + 1;
                        break;
                    }
                }

                if (count == 1)
                {
                    Showprint_Save(Convert.ToInt32(ShowButtonType.save));
                }
                else
                {
                    if (ordercount == gridSourcemaster.Count && OrderRunning_Status && PageType == Convert.ToInt32(FoodandBeverage.Interfaces.PageType.Billpage))
                    {
                        Showprint_Save(Convert.ToInt32(ShowButtonType.Print));
                    }
                    else
                    {
                        if (count == 0)
                        {
                            if (Booking_Status)
                            {
                                Showprint_Save(Convert.ToInt32(ShowButtonType.Nnone));
                                btncancelrev.Visible = true;
                            }

                            else
                                Showprint_Save(Convert.ToInt32(ShowButtonType.Nnone));
                        }
                        else
                            Showprint_Save(Convert.ToInt32(ShowButtonType.save));
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  checksave_refresh()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void GridSlno_Refresh()
        {
            try
            {
                if (Dgv_Selected_Item.RowCount == 0)
                    Dgv_Selected_Item.Rows.Add();

                int i = 1;
                for (int j = 0; j < Dgv_Selected_Item.RowCount; j++)
                {
                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[j].Cells[isubofferstatus].Value) == 0 && Dgv_Selected_Item.Rows[j].Cells[isubofferstatus].Value != null)
                    {
                        Dgv_Selected_Item.Rows[j].Cells[islno].Value = i;
                        i = i + 1;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  GridSlno_Refresh()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void AddEmptyRowIfNeeded()
        {
            try
            {
                // If no rows → add first row
                if (Dgv_Selected_Item.RowCount == 0)
                {
                    Dgv_Selected_Item.Rows.Add();
                    return;
                }

                // Get last row
                DataGridViewRow lastRow = Dgv_Selected_Item.Rows[Dgv_Selected_Item.RowCount - 1];

                // Check if last row is empty (based on isubofferstatus column)
                bool isLastRowEmpty =
                    lastRow.Cells[isubofferstatus].Value == null ||
                    string.IsNullOrWhiteSpace(lastRow.Cells[isubofferstatus].Value.ToString());

                // If last row is NOT empty → add new row
                if (!isLastRowEmpty)
                {
                    Dgv_Selected_Item.Rows.Add();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("AddEmptyRowIfNeeded()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        #endregion


        #region Memory
        public void Memoryrelease()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion

        #region side menu
        private void btn_showmenu_Click(object sender, EventArgs e)
        {
            Memoryrelease();

            flp_sidemenu.Visible = !flp_sidemenu.Visible;

            if (!flp_sidemenu.Visible)
                return;

            // Setup menu panel
            flp_sidemenu.FlowDirection = FlowDirection.LeftToRight;
            flp_sidemenu.Width = btn_showmenu.Width + (btn_showmenu.Width / 4);
            flp_sidemenu.Height = tlp_productlist.Height;

            flp_sidemenu.AutoScroll = true;
            flp_sidemenu.HorizontalScroll.Enabled = true;
            flp_sidemenu.HorizontalScroll.Visible = true;
            flp_sidemenu.VerticalScroll.Enabled = false;
            flp_sidemenu.VerticalScroll.Visible = false;

            // Positioning menu panel
            Point btnScreen = btn_showmenu.PointToScreen(Point.Empty);
            int desiredX = btnScreen.X + (btn_showmenu.Width / 2) - (flp_sidemenu.Width / 2);
            int desiredY = btnScreen.Y + btn_showmenu.Height + 20;

            Rectangle screenBounds = Screen.FromControl(btn_showmenu).WorkingArea;

            if (desiredX < screenBounds.Left)
                desiredX = screenBounds.Left;
            else if (desiredX + flp_sidemenu.Width > screenBounds.Right)
                desiredX = screenBounds.Right - flp_sidemenu.Width;

            if (desiredY + flp_sidemenu.Height > screenBounds.Bottom)
            {
                desiredY = btnScreen.Y - flp_sidemenu.Height - 10;
                if (desiredY < screenBounds.Top)
                    desiredY = screenBounds.Top;
            }

            flp_sidemenu.Location = this.PointToClient(new Point(desiredX, desiredY));

            // Generate menu buttons
            GenerateSideMenu();

            // Center items
            foreach (Control item in flp_sidemenu.Controls)
                item.Margin = new Padding((flp_sidemenu.Width - item.Width) / 2, 5, 0, 5);

            flp_sidemenu.BringToFront();
        }
        private void GenerateSideMenu()
        {
            try
            {
                int bheight = btn_showmenu.Height + (btn_showmenu.Height / 2);
                int bWidth = btn_showmenu.Width;

                BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                DmnSearch dmnSearch = new DmnSearch
                {
                    Select = "*",
                    Table = "tblCategory_FTI",
                    WhereNdGroupBy = "WHERE Status = 1 AND CategoryType = " + Convert.ToInt32(CategoryType.Menu_Category),
                    OrderBy = ""
                };

                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                int expectedCount = 3 + gridSource.Count;

                // If count matches, skip regeneration
                if (flp_sidemenu.Controls.Count == expectedCount)
                    return;

                flp_sidemenu.SuspendLayout();

                // Clean old controls
                foreach (Control ctrl in flp_sidemenu.Controls)
                    ctrl.Dispose();
                flp_sidemenu.Controls.Clear();

                if (sideMenuToolTip == null)
                    sideMenuToolTip = new ToolTip();

                // -------------------
                // 1. Favourite Button
                // -------------------
                Button buttonfav = CreateSideMenuButton("Favourite Item", "btn_fav");
                buttonfav.Click += SidebuttonfavClickEventHandler;
                flp_sidemenu.Controls.Add(buttonfav);

                // -------------------
                // 2. Offer Button
                // -------------------
                Button buttonOffer = CreateSideMenuButton("Special Offer", "btn_offer");
                buttonOffer.Click += SidebuttonOfferClickEventHandler;
                flp_sidemenu.Controls.Add(buttonOffer);

                // -------------------
                // 3. Combo Offer Button
                // -------------------
                Button buttonCombo = CreateSideMenuButton("Combo Offer", "btn_combo_offer");
                buttonCombo.Click += SidebuttonComboOfferClickEventHandler;
                flp_sidemenu.Controls.Add(buttonCombo);

                // -------------------
                // 4. Dynamic Category Buttons
                // -------------------
                foreach (var cat in gridSource)
                {
                    Button button = CreateSideMenuButton(cat.CategoryName, "btn_" + cat.CategoryName);
                    button.Tag = cat.Fcid;
                    button.Click += SidebuttonClickEventHandler;
                    flp_sidemenu.Controls.Add(button);
                }

                flp_sidemenu.ResumeLayout();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page GenerateSidemenu()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private Button CreateSideMenuButton(string text, string name)
        {
            int bheight = btn_showmenu.Height;// + (btn_showmenu.Height / 2);
            int bWidth = btn_showmenu.Width;

            Button btn = new Button
            {
                Text = text,
                Name = name,
                Tag = 0,
                ForeColor = Color.White,
                Height = bheight,
                Width = bWidth,
                Dock = DockStyle.Top,
                BackgroundImage = Properties.Resources.fb_sidemenu,
                BackgroundImageLayout = ImageLayout.Stretch,
                FlatStyle = FlatStyle.Flat,
                Font = AppFonts.GetFont(10)
            };

            btn.FlatAppearance.BorderSize = 0;
            sideMenuToolTip.SetToolTip(btn, text);

            return btn;
        }

        #endregion

        #region Normal Product AutoResponsive_ProductTiles
        private void SidebuttonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                McategoryID = Convert.ToInt32(MenucategryType.NormalProduct);
                ProductData = new List<DmnGridFields>();
                pagedatadata = new List<DmnGridFields>();
                currentPage = 1;

                Control clickedControl = sender as Control;

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();

                dmnSearch.Select =
                    "p.Product as Field1,p.ProductId as Field2,0 as Field3,fav_status as Field4,p.price Field5,q.Discountper Field6,q.FinalPrice Field7";
                dmnSearch.Table =
                    "tblProduct p " +
                    "LEFT JOIN tblMenu m on m.ProductId=p.ProductId " +
                    " OUTER APPLY ( Select ofd.Discountper,ofd.FinalPrice from  tblOfferDetails ofd   LEFT JOIN tblOfferMaster ofm ON ofd.omid = ofm.omid AND ofd.status = 1   where ofm.status = 1  AND ofm.OfferType = 0 AND ofd.Producttype=1 AND p.ProductId = ofd.ProductId and ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status = 1)q" +
                    " where p.Status=1 and p.Fcid= " + clickedControl.Tag + " and m.Status=1 and m.Quantity>0 " +
                    " Union Select i.Itemname as Field1,i.ItemId as Field2,i.Saletype as Field3,i.fav_status as Field4,(SELECT MAX(SRate) FROM tblProductRate WHERE ProductId = i.ItemId) AS Field5,q.Discountper Field6,q.FinalPrice Field7 from " +
                    " tblItem i " +
                    " OUTER APPLY ( Select ofd.Discountper,ofd.FinalPrice from  tblOfferDetails ofd   LEFT JOIN tblOfferMaster ofm ON ofd.omid = ofm.omid AND ofd.status = 1   where ofm.status = 1 AND ofd.Producttype=2  AND ofm.OfferType = 0 AND i.itemid = ofd.ProductId and ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status = 1)q";
                // " LEFT JOIN tblOfferMaster ofm ON  ofm.status=1  AND ofm.OfferType=0 AND  ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status=1 LEFT JOIN tblOfferDetails ofd ON ofd.omid=ofm.omid AND ofd.status=1 ";
                dmnSearch.WhereNdGroupBy =
                    " where i.Fcid= " + clickedControl.Tag + " and i.Saletype=1 and i.Status=1  " +
                    " and (Select sum(QOH) from tblProductRate where ProductId=i.ItemId) > 0";

                // " LEFT JOIN tblOfferMaster ofm ON  ofm.status=1  AND ofm.OfferType=0 AND  ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status=1 LEFT JOIN tblOfferDetails ofd ON ofd.omid=ofm.omid AND ofd.status=1 and p.ProductId=ofd.ProductId" +
                ProductData = bllSearch.GetSearchData("prdSearch", dmnSearch);
                totalPages = (int)Math.Ceiling((double)ProductData.Count / pageSize);

                flp_productview.AutoScroll = true;
                flp_productview.WrapContents = true;
                flp_productview.FlowDirection = FlowDirection.LeftToRight;

                if (ProductData.Count == 0)
                    return;

                flp_sidemenu.Visible = false;

                pagedatadata = ProductData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {ProductData.Count}";
                btnprev.Enabled = currentPage > 1;
                btnnext.Enabled = currentPage < totalPages;

                GenearteNormalProduct();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page SidebuttonClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void GenearteNormalProduct()
        {
            try
            {
                foreach (Control c in flp_productview.Controls)
                {
                    foreach (Control child in c.Controls)
                        child.Dispose();
                    c.Dispose();
                }
                flp_productview.Controls.Clear();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                foreach (var item in pagedatadata)
                {
                    string productName = item.Field1;
                    int productId = Convert.ToInt32(item.Field2);
                    decimal price = Convert.ToDecimal(item.Field5);
                    int favStatus = Convert.ToInt32(item.Field4);
                    int Saletypeno = Convert.ToInt32(item.Field3);
                    decimal originalPrice = Convert.ToDecimal(item.Field5);
                    decimal discountPrice = Convert.ToDecimal(item.Field7);
                    decimal discountper = Convert.ToDecimal(item.Field6);

                    Panel tile = new Panel();
                    tile.Width = tileW;
                    tile.Height = tileH;
                    tile.BackColor = Color.White;
                    tile.Margin = new Padding(spacing);
                    tile.Tag = productId;
                    tile.AccessibleDescription = Convert.ToString(Saletypeno);

                    //MakeRounded(tile, 18);

                    TableLayoutPanel tlpMain = new TableLayoutPanel();
                    tlpMain.Parent = tile;
                    tlpMain.RowCount = 2;
                    tlpMain.ColumnCount = 1;
                    tlpMain.Dock = DockStyle.Fill;
                    tlpMain.AccessibleDescription = Convert.ToString(Saletypeno);
                    //tlpMain.BorderStyle = BorderStyle.FixedSingle;
                    tlpMain.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;  // SHOW BORDER
                    tlpMain.Tag = productId;

                    //tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 80f));
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));

                    Label lblName = new Label();
                    lblName.Text = productName;
                    lblName.TextAlign = ContentAlignment.MiddleLeft;
                    Size textSize = TextRenderer.MeasureText(lblName.Text, f11);
                    lblName.Font = (textSize.Width <= tileW - 10) ? f11 : f9;
                    lblName.Dock = DockStyle.Fill;
                    //lblName.Padding = new Padding(3);
                    lblName.ForeColor = Color.Black;
                    lblName.BackColor = Color.Transparent;
                    int nameWidth = TextRenderer.MeasureText(lblName.Text, lblName.Font).Width;

                    Label lblPrice = new Label();
                    lblPrice.Dock = DockStyle.Fill;
                    lblPrice.TextAlign = ContentAlignment.TopLeft;
                    lblPrice.ForeColor = Color.Green;
                    lblPrice.BackColor = Color.Transparent;
                    lblPrice.Font = f10;
                    if (discountper > 0)
                    {
                        //lblPrice.Paint += (s, l) =>
                        //{
                        //    string oldP = "₹ " + originalPrice.ToString("0.00");
                        //    string newP = "₹ " + discountPrice.ToString("0.00");

                        //    SizeF oldSize = l.Graphics.MeasureString(oldP, f10Strike);
                        //    SizeF newSize = l.Graphics.MeasureString(newP, f10Bold);

                        //    float totalWidth = oldSize.Width + 10 + newSize.Width;
                        //    float startX = (lblPrice.Width - totalWidth) / 2;

                        //    l.Graphics.DrawString(oldP, f10Strike, Brushes.Gray, startX, 4);
                        //    l.Graphics.DrawString(newP, f10Bold, Brushes.Green, startX + oldSize.Width + 10, 4);
                        //};
                        lblPrice.Text = "₹ " + discountPrice.ToString("0.00");
                    }
                    else
                    {
                        if (Saletypeno == 1 && price == 0)
                            lblPrice.Text = "";
                        else
                            lblPrice.Text = "₹ " + price.ToString("0.00");
                    }

                    tlpMain.Controls.Add(lblName, 0, 0);
                    tlpMain.Controls.Add(lblPrice, 0, 1);

                    tlpMain.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                    tile.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                    lblPrice.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                    lblName.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);

                    PictureBox picFav = new PictureBox();
                    picFav.Name = "picFav_" + item.Field2 + "_" + item.Field3;
                    int favSize = tileW / 4;     // auto scale
                    picFav.Width = favSize;
                    picFav.Height = favSize;
                    picFav.BackColor = Color.White;
                    picFav.SizeMode = PictureBoxSizeMode.StretchImage;
                    picFav.Anchor = AnchorStyles.Top | AnchorStyles.Right;  
                    picFav.Location = new Point(tile.Width - favSize - 5, 5); 
                    picFav.Tag = productId;
                    picFav.AccessibleName = favStatus == 1 ? "fav" : "notfav";
                    picFav.AccessibleDescription = Convert.ToString(Saletypeno);

                    picFav.Image = (favStatus == 1)
                        ? Properties.Resources.fav_enable
                        : Properties.Resources.fav_disable;

                    picFav.Click += buttonfavClickEventHandler;

                    tile.Controls.Add(picFav);
                    picFav.BringToFront();


                    flp_productview.Controls.Add(tile);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page GenearteNormalProduct()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void PicProduct_Click(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            DashboardButtonClickEventHandler(pic.Parent, e);
        }
        #endregion

        #region Product Favouite Button click (depented)
        private void buttonfavClickEventHandler(object sender, EventArgs e)
        {
            PictureBox pbfav = sender as PictureBox;
            if (pbfav == null) return;

            bool isFav = pbfav.AccessibleName == "fav";

            if (isFav)
            {
                pbfav.Image = Properties.Resources.fav_disable;
                pbfav.AccessibleName = "notfav";

                UpdateFavorite(Convert.ToInt32(pbfav.Tag), "0", pbfav.AccessibleDescription);
            }
            else
            {
                pbfav.Image = Properties.Resources.fav_enable;
                pbfav.AccessibleName = "fav";

                UpdateFavorite(Convert.ToInt32(pbfav.Tag), "1", pbfav.AccessibleDescription);
            }
        }


        private void UpdateFavorite(int id, string favFlag, string accDesc)
        {
            BllProducts bllfav = new BllProducts();
            DmnGridFields dmn = new DmnGridFields();

            dmn.Field1 = Convert.ToString(id);
            dmn.Field2 = favFlag;
            dmn.Field3 = (accDesc == "1") ? "1" : "0";

            bllfav.AddEditData_fav("prd_insert_upd_fav", dmn);
        }

        #endregion

        #region Favourite Products AutoResponsive_ProductTiles
        private async void SidebuttonfavClickEventHandler(object sender, EventArgs e)
        {
            try
            {

                McategoryID = Convert.ToInt32(MenucategryType.favourite);

                ProductData = new List<DmnGridFields>();
                pagedatadata = new List<DmnGridFields>();

                currentPage = 1;

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                List<DmnGridFields> gridSource = new List<DmnGridFields>();

                flp_productview.Controls.Clear();
                flp_productview.AutoScroll = true;
                flp_productview.WrapContents = true;
                flp_productview.FlowDirection = FlowDirection.LeftToRight;

                flp_sidemenu.Visible = false;
                for (int j = 1; j <= 2; j++)
                {
                    if (j == 1)
                    {
                        dmnSearch.Select = "p.Product as Field1,p.ProductId as Field2,0 as Field3,fav_status as Field4,p.price Field5,q.Discountper Field6,q.FinalPrice Field7";
                        dmnSearch.Table = "tblProduct p " +
                            "LEFT JOIN tblMenu m on m.ProductId=p.ProductId " +
                            " OUTER APPLY ( Select ofd.Discountper,ofd.FinalPrice from  tblOfferDetails ofd   LEFT JOIN tblOfferMaster ofm ON ofd.omid = ofm.omid AND ofd.status = 1   where ofm.status = 1 AND ofd.Producttype=1 AND ofm.OfferType = 0 AND p.ProductId = ofd.ProductId and ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status = 1)q";
                        dmnSearch.WhereNdGroupBy = " where p.Status=1 and m.Status=1 and m.Quantity>0 and fav_status=1";
                    }
                    else
                    {
                        dmnSearch.Select = " i.Itemname as Field1,i.ItemId as Field2,i.Saletype as Field3,i.fav_status as Field4,(SELECT MAX(SRate) FROM tblProductRate WHERE ProductId = i.ItemId) AS Field5,q.Discountper Field6,q.FinalPrice Field7";
                        dmnSearch.Table = " tblItem i" +
                            " OUTER APPLY(Select ofd.Discountper, ofd.FinalPrice from  tblOfferDetails ofd   LEFT JOIN tblOfferMaster ofm ON ofd.omid = ofm.omid AND ofd.status = 1   where ofm.status = 1 AND ofd.Producttype=2  AND ofm.OfferType = 0 AND i.itemid = ofd.ProductId and ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status = 1)q";
                        dmnSearch.WhereNdGroupBy = " where  i.Saletype=1 and i.Status=1  and (Select sum(QOH) from tblProductRate where ProductId=i.ItemId) > 0 and  i.fav_status=1";
                    }

                    var queryResult = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    ProductData.AddRange(queryResult);
                }
                totalPages = (int)Math.Ceiling((double)ProductData.Count / pageSize);
                pagedatadata = ProductData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {ProductData.Count}";
                btnprev.Enabled = currentPage > 1;
                btnnext.Enabled = currentPage < totalPages;
                GenerateFavouriteProduct();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page SidebuttonfavClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void GenerateFavouriteProduct()
        {
            try
            {
                flp_productview.SuspendLayout();

                // === Dispose old tiles safely ===
                foreach (Control c in flp_productview.Controls)
                    c.Dispose();

                flp_productview.Controls.Clear();

                foreach (var item in pagedatadata)
                {
                    int productId = Convert.ToInt32(item.Field2);
                    int favStatus = Convert.ToInt32(item.Field4);
                    int saleType = Convert.ToInt32(item.Field3);

                    decimal price = Convert.ToDecimal(item.Field5);
                    decimal discountPrice = Convert.ToDecimal(item.Field7);
                    decimal discountPer = Convert.ToDecimal(item.Field6);

                    Panel tile = new Panel
                    {
                        Width = tileW,
                        Height = tileH,
                        BackColor = Color.White,
                        Margin = new Padding(spacing),
                        Tag = productId,
                        AccessibleDescription = saleType.ToString()
                    };

                    TableLayoutPanel tlpMain = new TableLayoutPanel
                    {
                        Parent = tile,
                        RowCount = 2,
                        ColumnCount = 1,
                        Dock = DockStyle.Fill,
                        Tag = productId,
                        AccessibleDescription = saleType.ToString()
                    };

                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 85));
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 15));

                    Label lblName = new Label
                    {
                        Text = item.Field1,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Tag = productId,
                        Dock = DockStyle.Fill,
                        Padding = new Padding(3),
                        ForeColor = Color.Black,
                        BackColor = Color.Transparent
                    };

                    Size textSize = TextRenderer.MeasureText(lblName.Text, f11);
                    lblName.Font = (textSize.Width <= tileW - 10) ? f11 : f9;

                    Label lblPrice = new Label
                    {
                        Dock = DockStyle.Fill,
                        Tag = productId,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.Green,
                        BackColor = Color.Transparent,
                        Font = f10
                    };

                    if (discountPer > 0)
                    {
                        lblPrice.Paint -= PricePaintHandler;
                        lblPrice.Paint += PricePaintHandler;

                        lblPrice.Tag = new PriceTag
                        {
                            OldPrice = price,
                            NewPrice = discountPrice
                        };
                    }
                    else
                    {
                        lblPrice.Text = (saleType == 1 && price == 0)
                                       ? ""
                                       : "₹ " + price.ToString("0.00");
                    }

                    tlpMain.Controls.Add(lblName, 0, 0);
                    tlpMain.Controls.Add(lblPrice, 0, 1);

                    tile.Click += DashboardButtonClickEventHandler;
                    tlpMain.Click += DashboardButtonClickEventHandler;
                    lblName.Click += DashboardButtonClickEventHandler;
                    lblPrice.Click += DashboardButtonClickEventHandler;

                    PictureBox picFav = new PictureBox
                    {
                        Name = $"picFav_{productId}_{saleType}",
                        Tag = productId,
                        AccessibleDescription = saleType.ToString(),
                        AccessibleName = favStatus == 1 ? "fav" : "notfav",
                        Width = 22,
                        Height = 22,
                        BackColor = Color.White,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Location = new Point(tileW - 30, 8),
                        Image = favStatus == 1 ? Properties.Resources.fav_enable : Properties.Resources.fav_disable
                    };

                    picFav.Click += buttonfavClickEventHandler;

                    tile.Controls.Add(picFav);
                    picFav.BringToFront();

                    flp_productview.Controls.Add(tile);
                }
            }
            finally
            {
                flp_productview.ResumeLayout();
            }
        }
        private void PricePaintHandler(object sender, PaintEventArgs e)
        {
            Label lbl = sender as Label;
            var pt = lbl.Tag as PriceTag;
            if (pt == null) return;

            string oldP = "₹ " + pt.OldPrice.ToString("0.00");
            string newP = "₹ " + pt.NewPrice.ToString("0.00");

            SizeF oldSize = e.Graphics.MeasureString(oldP, f10Strike);
            SizeF newSize = e.Graphics.MeasureString(newP, f10Bold);

            float totalWidth = oldSize.Width + 10 + newSize.Width;
            float startX = (lbl.Width - totalWidth) / 2;

            e.Graphics.DrawString(oldP, f10Strike, Brushes.Gray, startX, 4);
            e.Graphics.DrawString(newP, f10Bold, Brushes.Green, startX + oldSize.Width + 10, 4);
        }

        class PriceTag
        {
            public decimal OldPrice;
            public decimal NewPrice;
        }

        #endregion

        #region Offer Button Click

        private void SidebuttonOfferClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                McategoryID = Convert.ToInt32(MenucategryType.OfferProduct);

                ProductData = new List<DmnGridFields>();
                pagedatadata = new List<DmnGridFields>();
                currentPage = 1;

                Control clickedControl = sender as Control;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();

                flp_productview.Controls.Clear();
                flp_productview.AutoScroll = true;
                flp_productview.WrapContents = true;
                flp_productview.FlowDirection = FlowDirection.LeftToRight;

                dmnSearch.Select = "Omid as Field1,OfferName as Field2,Discountper Field3";
                dmnSearch.Table = "tblOfferMaster";
                dmnSearch.WhereNdGroupBy =
                    " WHERE Status=1 AND OfferType=0 AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME()";
                dmnSearch.OrderBy = "";

                var offerMenu = bllSearch.GetSearchData("prdSearch", dmnSearch);

                #region More offer Runinng
                if (offerMenu.Count > 1)
                {
                    flp_sidemenu.Visible = false;

                    int of_maxColumns = columncount_offer;
                    int of_spacing = Productspace;

                    int of_containerWidth = flp_productview.ClientSize.Width;
                    int of_tileW = (of_containerWidth - ((of_maxColumns + 1) * of_spacing)) / of_maxColumns;
                    int of_tileH = (int)(of_tileW * 0.3);
                    int of_titleHeight = (int)(of_tileH * 0.35);
                    int of_percentHeight = of_tileH - of_titleHeight - 8;

                    flp_productview.Controls.Clear();

                    foreach (var item in offerMenu)
                    {
                        Panel tile = new Panel();
                        tile.Width = of_tileW;
                        tile.Height = of_tileH;
                        tile.Margin = new Padding(of_spacing);
                        tile.BackColor = Color.White;
                        tile.BorderStyle = BorderStyle.None;
                        tile.Cursor = Cursors.Hand;
                        tile.Tag = item.Field1;
                        //MakeRounded(tile, 18);

                        Label lblTitle = new Label();
                        lblTitle.Text = item.Field2;
                        lblTitle.Font = AppFonts.GetFont((int)(of_tileH * .17));
                        lblTitle.ForeColor = Color.Black;
                        lblTitle.TextAlign = ContentAlignment.MiddleCenter;
                        lblTitle.SetBounds(5, 5, tile.Width - 10, of_titleHeight);
                        lblTitle.Cursor = Cursors.Hand;

                        Label lblPercent = new Label();
                        lblPercent.Text = item.Field3 + "%";
                        lblPercent.Font = AppFonts.GetFont((int)(of_tileH * 0.20));
                        lblPercent.ForeColor = Color.Red;
                        lblPercent.TextAlign = ContentAlignment.MiddleCenter;
                        lblPercent.SetBounds(5, 5 + of_titleHeight, tile.Width - 10, of_percentHeight);
                        lblPercent.Cursor = Cursors.Hand;

                        tile.Click += (s, ev) => SpecialOfferSubMenuClickEventHandler(tile, ev);
                        lblTitle.Click += (s, ev) => SpecialOfferSubMenuClickEventHandler(tile, ev);
                        lblPercent.Click += (s, ev) => SpecialOfferSubMenuClickEventHandler(tile, ev);

                        tile.Controls.Add(lblTitle);
                        tile.Controls.Add(lblPercent);

                        flp_productview.Controls.Add(tile);
                    }
                    return;
                }
                #endregion

                flp_productview.Controls.Clear();
                List<DmnGridFields> gridSource = new List<DmnGridFields>();
                flp_sidemenu.Visible = false;
                for (int j = 1; j <= 2; j++)
                {
                    if (j == 1)
                    {
                        dmnSearch.Select = "p.Product as Field1, p.ProductId as Field2, 0 as Field3, od.Discountper Field4,od.Producttype Field5,p.Price Field6,0 Field7,1 Field8";
                        dmnSearch.Table = " tblOfferDetails OD LEFT JOIN tblOfferMaster OM ON OD.Omid = OM.Omid LEFT JOIN tblProduct p ON OD.ProductId = p.ProductId LEFT JOIN tblMenu m ON m.ProductId = p.ProductId";
                        dmnSearch.WhereNdGroupBy = " where OM.Status = 1 and od.Status=1 and OfferType=0 and m.Status=1 and od.Producttype=1 AND GETDATE() >= OM.OfferstartDatetime AND GETDATE() <= OM.OfferEndDatetime and m.Quantity>0 ";
                    }
                    else
                    {
                        dmnSearch.Select = "p.Itemname as Field1, p.ItemId as Field2, 1 as Field3, od.Discountper Field4, od.Producttype Field5,(select PRate from tblProductRate where ProductId = p.ItemId and Status=1) Field6,(select count(*) from tblProductRate where ProductId = p.ItemId and Status=1) Field7,2 Field8";
                        dmnSearch.Table = "tblOfferDetails OD LEFT JOIN tblOfferMaster OM ON OD.Omid = OM.Omid LEFT JOIN tblItem p ON OD.ProductId = p.ItemId ";
                        dmnSearch.WhereNdGroupBy = " where  p.Saletype=1 and p.Status=1and om.OfferType=0 and  GETDATE() >= OM.OfferstartDatetime AND GETDATE() <= OM.OfferEndDatetime and  (Select sum(QOH) from tblProductRate where ProductId=p.ItemId) > 0 and od.Producttype=2 and od.Status=1 and om.Status=1";
                    }
                    var queryResult = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    ProductData.AddRange(queryResult);
                }
                totalPages = (int)Math.Ceiling((double)ProductData.Count / pageSize);
                pagedatadata = ProductData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {ProductData.Count}";
                btnprev.Enabled = currentPage > 1;
                btnnext.Enabled = currentPage < totalPages;

                GenerateofferProduct();
                #region old code
                // ---------------------------------------------------------
                // 2) PRODUCT WISE OFFERS
                // ---------------------------------------------------------
                /*/List<DmnGridFields> gridSource = new List<DmnGridFields>();

                for (int j = 1; j <= 2; j++)
                {
                    if (j == 1)
                    {
                        dmnSearch.Select =
                            "p.Product as Field1, p.ProductId as Field2, 0 as Field3, ofd.Discountper Field4, ofd.Producttype Field5";
                        dmnSearch.Table =
                            "tblProduct p " +
                            "LEFT JOIN tblOfferDetails ofd ON ofd.ProductId=p.ProductId AND ofd.status=1 " +
                            "LEFT JOIN tblOfferMaster ofm ON ofm.Omid=ofd.Omid AND ofm.status=1 " +
                            "LEFT JOIN tblMenu m ON m.ProductId=p.ProductId";
                        dmnSearch.WhereNdGroupBy =
                            " WHERE p.Status=1 AND ofm.OfferType=0 AND m.status=1 AND " +
                            "ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME()";
                    }
                    else
                    {
                        dmnSearch.Select =
                            "i.Itemname as Field1, i.ItemId as Field2, 1 as Field3, ofd.Discountper Field4, ofd.Producttype Field5";
                        dmnSearch.Table =
                            "tblItem i " +
                            "LEFT JOIN tblOfferDetails ofd ON ofd.ProductId=i.ItemId AND ofd.status=1 " +
                            "LEFT JOIN tblOfferMaster ofm ON ofm.Omid=ofd.Omid AND ofm.status=1";
                        dmnSearch.WhereNdGroupBy =
                            " WHERE i.Status=1 AND i.Saletype=1 AND ofm.OfferType=0 AND " +
                            "ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME()";
                    }

                    gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

                    foreach (var g in gridSource)
                    {
                        // MAIN PRODUCT BOX
                        Panel box = new Panel();
                        box.Width = 240;
                        box.Height = 65;
                        box.Margin = new Padding(8);
                        box.BackColor = Color.Transparent;

                        // PRODUCT BUTTON
                        Button btn = new Button();
                        btn.Text = g.Field1;
                        btn.Name = "btn" + g.Field1;
                        btn.Tag = g.Field2;
                        btn.SetBounds(0, 5, 190, 55);
                        btn.BackColor = product_buttoncolor;
                        btn.ForeColor = Color.White;
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.Font = new Font("Arial", 10, FontStyle.Bold);
                        btn.Click += new EventHandler(DashboardButtonClickEventHandler);

                        box.Controls.Add(btn);

                        // OFFER BADGE (%)
                        Button btnOffer = new Button();
                        btnOffer.Text = g.Field4;
                        btnOffer.SetBounds(195, 8, 40, 45);
                        btnOffer.Tag = g.Field2;
                        btnOffer.Font = new Font("Arial", 9, FontStyle.Bold);
                        btnOffer.BackColor = product_buttoncolor;
                        btnOffer.ForeColor = Color.White;
                        btnOffer.FlatStyle = FlatStyle.Flat;
                        btnOffer.BackgroundImage = Properties.Resources.offer_button_percentage;
                        btnOffer.BackgroundImageLayout = ImageLayout.Stretch;
                        btnOffer.Click += new EventHandler(DashboardButtonClickEventHandler);

                        box.Controls.Add(btnOffer);

                        // ADD TO FLOW
                        flp_productview.Controls.Add(box);
                    }
                }*/
                #endregion
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page SidebuttonOfferClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void GenerateofferProduct()
        {
            try
            {
                foreach (Control c in flp_productview.Controls)
                {
                    c.Click -= DashboardButtonClickEventHandler;
                    foreach (Control child in c.Controls)
                        child.Click -= DashboardButtonClickEventHandler;

                    c.Dispose();
                }

                flp_productview.Controls.Clear();

                foreach (var item in pagedatadata)
                {
                    string productName = item.Field1;
                    string Saletypeno = Convert.ToString(item.Field3);
                    int productId = Convert.ToInt32(item.Field2);
                    decimal originalPrice = Convert.ToDecimal(item.Field6);
                    decimal discountPercent = Convert.ToDecimal(item.Field4);
                    decimal discountPrice = originalPrice - (originalPrice * discountPercent / 100);

                    Panel tile = new Panel
                    {
                        Width = tileW,
                        Height = tileH,
                        BackColor = Color.White,
                        Margin = new Padding(spacing),
                        Tag = productId,
                        AccessibleDescription = Saletypeno,
                        BorderStyle = BorderStyle.None
                    };
                    tile.Click += DashboardButtonClickEventHandler;

                    TableLayoutPanel tlpMain = new TableLayoutPanel
                    {
                        Parent = tile,
                        Dock = DockStyle.Fill,
                        RowCount = 2,
                        Tag = productId,
                        AccessibleDescription = Saletypeno,
                        ColumnCount = 1,
                        BackColor = Color.White,
                        Margin = new Padding(0)
                    };
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 85));
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
                    tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
                    tlpMain.Click += DashboardButtonClickEventHandler;

                    Label lblProductName = new Label
                    {
                        Text = productName,
                        Dock = DockStyle.Fill,
                        Tag = productId,
                        AccessibleDescription = Saletypeno,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.Black,
                        BackColor = Color.Transparent,
                        Padding = new Padding(3),
                        AutoSize = false
                    };
                    lblProductName.Click += DashboardButtonClickEventHandler;

                    Size textSize = TextRenderer.MeasureText(lblProductName.Text, AppFonts.GetFont(11));
                    lblProductName.Font = textSize.Width <= tileW - 10 ? AppFonts.GetFont(11) : AppFonts.GetFont(9);

                    Label lblPrice = new Label
                    {
                        Dock = DockStyle.Fill,
                        AutoSize = false,
                        Tag = productId,
                        AccessibleDescription = Saletypeno,
                        ForeColor = Color.Green,
                        BackColor = Color.Transparent,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = AppFonts.GetFont(10)
                    };
                    lblPrice.Click += DashboardButtonClickEventHandler;

                    lblPrice.Paint += (s, l) =>
                    {
                        string oldP = "₹ " + originalPrice.ToString("0.00");
                        string newP = "₹ " + discountPrice.ToString("0.00");

                        using (Font fOld = new Font(lblPrice.Font, FontStyle.Strikeout))
                        using (Font fNew = new Font(lblPrice.Font, FontStyle.Bold))
                        {
                            SizeF oldSize = l.Graphics.MeasureString(oldP, fOld);
                            SizeF newSize = l.Graphics.MeasureString(newP, fNew);

                            float totalWidth = oldSize.Width + 10 + newSize.Width;
                            float startX = (lblPrice.Width - totalWidth) / 2;

                            l.Graphics.DrawString(oldP, fOld, Brushes.Gray, startX, 4);
                            l.Graphics.DrawString(newP, fNew, Brushes.Green, startX + oldSize.Width + 10, 4);
                        }
                    };

                    tlpMain.Controls.Add(lblProductName, 0, 0);
                    tlpMain.Controls.Add(lblPrice, 0, 1);
                    flp_productview.Controls.Add(tile);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page GenerateofferProduct()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }


        private void SpecialOfferSubMenuClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;

                flp_productview.Controls.Clear();
                flp_productview.AutoScroll = true;
                flp_productview.WrapContents = true;
                flp_productview.FlowDirection = FlowDirection.LeftToRight;

                ProductData.Clear();

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();

                for (int j = 1; j <= 2; j++)
                {
                    if (j == 1)
                    {
                        dmnSearch.Select =
                            "p.Product as Field1, p.ProductId as Field2, 0 as Field3," +
                            "ofd.Discountper as Field4, ofd.ProductType as Field5, p.Price Field6," +
                            "0 Field7, 1 Field8";

                        dmnSearch.Table =
                            "tblProduct p " +
                            "LEFT JOIN tblOfferDetails ofd ON ofd.ProductId=p.ProductId AND ofd.status=1 AND ofd.Producttype=1 " +
                            "LEFT JOIN tblOfferMaster ofm ON ofm.Omid=ofd.Omid AND ofm.status=1 " +
                            "LEFT JOIN tblMenu m ON m.ProductId=p.ProductId";

                        dmnSearch.WhereNdGroupBy =
                            " WHERE p.Status=1 AND m.Status=1 AND ofm.OfferType=0 " +
                            "AND ofm.OfferstartDatetime <= SYSDATETIME() " +
                            "AND ofm.OfferEndDatetime >= SYSDATETIME() " +
                            "AND ofm.Omid=" + clickedControl.Tag;
                    }
                    else
                    {
                        dmnSearch.Select =
                            "i.Itemname as Field1, i.ItemId as Field2, 1 as Field3," +
                            "ofd.Discountper as Field4, ofd.ProductType as Field5," +
                            "(select PRate from tblProductRate where ProductId=i.ItemId and Status=1) Field6," +
                            "(select count(*) from tblProductRate where ProductId=i.ItemId and Status=1) Field7," +
                            "2 Field8";

                        dmnSearch.Table =
                            "tblItem i " +
                            "LEFT JOIN tblOfferDetails ofd ON ofd.ProductId=i.ItemId AND ofd.status=1 AND ofd.Producttype=2 " +
                            "LEFT JOIN tblOfferMaster ofm ON ofm.Omid=ofd.Omid AND ofm.status=1";

                        dmnSearch.WhereNdGroupBy =
                            " WHERE i.Status=1 AND i.Saletype=1 AND ofm.OfferType=0 " +
                            "AND ofm.OfferstartDatetime <= SYSDATETIME() " +
                            "AND ofm.OfferEndDatetime >= SYSDATETIME() " +
                            "AND ofm.Omid=" + clickedControl.Tag;
                    }

                    var result = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    ProductData.AddRange(result);
                }

                totalPages = (int)Math.Ceiling((double)ProductData.Count / pageSize);
                pagedatadata = ProductData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

                lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {ProductData.Count}";
                btnprev.Enabled = currentPage > 1;
                btnnext.Enabled = currentPage < totalPages;

                GenerateofferProduct();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page SpecialOfferSubMenuClickEventHandler()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }


        #endregion

        #region Combo Product Cilck
        private void SidebuttonComboOfferClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                McategoryID = Convert.ToInt32(MenucategryType.ComboProduct);
                ProductData = new List<DmnGridFields>();
                pagedatadata = new List<DmnGridFields>();
                currentPage = 1;

                Control clickedControl = sender as Control;

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();

                flp_productview.Controls.Clear();
                flp_productview.AutoScroll = true;
                flp_productview.WrapContents = true;
                flp_productview.FlowDirection = FlowDirection.LeftToRight;

                dmnSearch.Select = "OfferName as Field1,Omid as Field2,3 as Field3,Discountper as Field4,FinalPrice Field5,cmphid Field6";
                dmnSearch.Table = "tblOfferMaster";
                dmnSearch.WhereNdGroupBy =
                    " where Status=1 AND OfferType=1 AND OfferstartDatetime <= SYSDATETIME() " +
                    " AND OfferEndDatetime >= SYSDATETIME()";

                ProductData = bllSearch.GetSearchData("prdSearch", dmnSearch);

                totalPages = (int)Math.Ceiling((double)ProductData.Count / pageSize);
                pagedatadata = ProductData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {ProductData.Count}";
                btnprev.Enabled = currentPage > 1;
                btnnext.Enabled = currentPage < totalPages;

                GenerateComboProduct();

                flp_sidemenu.Visible = false;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page SidebuttonComboOfferClickEventHandler()",
                    ex, FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order));
            }
        }
        public void GenerateComboProduct()
        {
            try
            {

                foreach (Control c in flp_productview.Controls)
                {
                    foreach (Control child in c.Controls)
                        child.Dispose();

                    c.Dispose();
                }
                flp_productview.Controls.Clear();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                foreach (var item in pagedatadata)
                {
                    string offerName = item.Field1;
                    int comboId = Convert.ToInt32(item.Field2);
                    int cmphid = Convert.ToInt32(item.Field6);
                    string totalPrice = item.Field5;

                    #region Design Product

                    Panel tile = new Panel();
                    tile.Width = tileW;
                    tile.Height = tileH;
                    tile.BackColor = Color.White;
                    tile.Margin = new Padding(spacing);
                    tile.Tag = comboId;
                    tile.BorderStyle = BorderStyle.None;
                    tile.AccessibleDescription = item.Field3;
                    tile.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                    //MakeRounded(tile, 18);

                    TableLayoutPanel tblMain = new TableLayoutPanel();
                    tblMain.Parent = tile;
                    tblMain.Dock = DockStyle.Fill;
                    tblMain.BackColor = Color.Transparent;
                    tblMain.RowCount = 2;
                    tblMain.ColumnCount = 1;
                    //tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
                    tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 85));
                    tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));
                    tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

                    Label lblProductName = new Label();
                    lblProductName.Text = offerName;
                    lblProductName.ForeColor = Color.Black;
                    lblProductName.BackColor = Color.Transparent;
                    lblProductName.Dock = DockStyle.Fill;
                    lblProductName.TextAlign = ContentAlignment.MiddleCenter;
                    lblProductName.Padding = new Padding(2);
                    lblProductName.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                    lblProductName.MaximumSize = new Size(tileW - 10, 0);

                    Size textSize = TextRenderer.MeasureText(lblProductName.Text, AppFonts.GetFont(11));
                    lblProductName.Font = (textSize.Width <= tileW - 10)
                                            ? AppFonts.GetFont(11)
                                            : AppFonts.GetFont(9);

                    int nameWidth = TextRenderer.MeasureText(lblProductName.Text, lblProductName.Font).Width;

                    Label lblPrice = new Label();
                    lblPrice.Text = "₹ " + Convert.ToDecimal(totalPrice).ToString("0.00");
                    lblPrice.ForeColor = Color.Green;
                    lblPrice.BackColor = Color.Transparent;
                    lblPrice.Font = AppFonts.GetFont(10);
                    lblPrice.Dock = DockStyle.Fill;
                    lblPrice.TextAlign = ContentAlignment.MiddleCenter;
                    lblPrice.AccessibleDescription = item.Field3;
                    lblPrice.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);

                    tblMain.Controls.Add(lblProductName, 0, 0);
                    tblMain.Controls.Add(lblPrice, 0, 1);

                    PictureBox picMore = new PictureBox();
                    picMore.Name = "picMore" + item.Field2;
                    picMore.Tag = item.Field2;
                    picMore.AccessibleDescription = item.Field3;
                    picMore.Width = 22;
                    picMore.Height = 22;
                    picMore.Location = new Point(tileW - 30, 8);
                    picMore.SizeMode = PictureBoxSizeMode.StretchImage;
                    picMore.BackColor = Color.White;
                    picMore.Image = Properties.Resources.orderpage_more_button;
                    picMore.Click += new EventHandler(ButtonviewmoreClickEventHandler);
                    tile.Controls.Add(picMore);
                    picMore.BringToFront();

                    flp_productview.Controls.Add(tile);

                    #endregion

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page GenerateComboProduct()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                    UIHelper.ResizeOnly(pnlproductviewmore, 30, 25);
                    UIHelper.RoundCorners(pnlproductviewmore, 25);
                    UIHelper.RoundCorners(tlp_viewmore, 25);
                    UIHelper.RoundCorners(tlp_header_viewmore, 25);

                    pnlproductviewmore.BringToFront();
                    pnlproductviewmore.Location = new Point(Cursor.Position.X, Cursor.Position.Y);

                    Control clickedControl = sender as Control;
                    dmnSearch.Select = "ofd.Productname as Field1,ofd.Quantity as Field2,ofm.OfferName Field3,ofd.AddonStatus Field4,ofm.OfferEndDatetime EndDate";
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
                        int c = 0;
                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            dgv_viewmore.Rows[i].Cells[1].Value = gridSource[i].Field1;
                            dgv_viewmore.Rows[i].Cells[2].Value = gridSource[i].Field2;
                            if (Convert.ToInt32(gridSource[i].Field4) == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                            {
                                dgv_viewmore.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dgv_viewmore.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dgv_viewmore.Rows[i].DefaultCellStyle.BackColor = Color.OliveDrab;
                            }
                            else
                            {
                                dgv_viewmore.Rows[i].Cells[0].Value = c + 1;
                                dgv_viewmore.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                dgv_viewmore.Rows[i].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                                c = c + 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  ButtonviewmoreClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        #endregion

        #region All menu button Click

        private void DashboardButtonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Memoryrelease();
                string Focused_tlpname = "";
                Control clickedControl = sender as Control;
                Global_ProductId = Convert.ToInt32(clickedControl.Tag);

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                if (Convert.ToInt32(clickedControl.AccessibleDescription) == 1)
                    CheckMultiplePrice(Convert.ToInt32(clickedControl.Tag));
                else if (Convert.ToInt32(clickedControl.AccessibleDescription) == 3)
                {
                    var data = ProductQtyCheckMenuQty(Convert.ToInt32(clickedControl.Tag), 3);
                    int qty = data.Quantity;
                    int prep = data.PreparationMode;
                    if (qty <= 0 && prep == Convert.ToInt32(PreparationType.ReadyFood))
                    {
                        MessageBox.Show("Stock Out! This item is not available.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    ComboProductFill(Convert.ToInt32(clickedControl.Tag));
                }
                else
                {
                    int prep = 0;
                    var data = ProductQtyCheckMenuQty(Convert.ToInt32(clickedControl.Tag), 0);
                    int qty = data.Quantity;
                    prep = data.PreparationMode;
                    if (qty <= 0 && prep == Convert.ToInt32(PreparationType.ReadyFood))
                    {
                        MessageBox.Show("Stock Out! This item is not available.",
                                        "Out of Stock",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        return;
                    }

                    #region Variation And Addon Checking
                    dmnSearch.Select = "count(*)  Field1";
                    dmnSearch.Table = "tblProduct_Addon_Variation ";
                    dmnSearch.WhereNdGroupBy = " where Status=1 and  ProductId= " + clickedControl.Tag;
                    dmnSearch.OrderBy = "";
                    var gridSource_va = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    if (gridSource_va.Count > 0)
                    {
                        if (Convert.ToInt32(gridSource_va[0].Field1) > 0)
                        {
                            ShowVariationAddon(Convert.ToInt32(clickedControl.Tag));
                        }
                    }
                    #endregion

                    if (Convert.ToInt32(gridSource_va[0].Field1) == 0)
                    {
                        dmnSearch.Select = "p.ItemCode as Field1, p.Product as Field2,p.Price as Field3,p.MRP as Field4,p.ProductId as Field5,p.Tax as Field6,p.TaxType as Field7,p.Fcid as Field8,p.TaxSettingsId as Field9,q.Discountper as Field10,q.offertype Field11,p.DiscountPer Field12,p.PreparationMode Field13";
                        dmnSearch.Table = "tblProduct p " +
                            " LEFT JOIN tblMenu m on m.ProductId=p.ProductId " +
                            " OUTER APPLY( Select ofd.Discountper, ofd.FinalPrice, ofm.offertype from  tblOfferDetails ofd LEFT JOIN tblOfferMaster ofm ON ofd.omid= ofm.omid AND ofd.status= 1 where ofm.status= 1  AND ofm.OfferType= 0 AND p.ProductId= ofd.ProductId and ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status = 1)q";
                        dmnSearch.WhereNdGroupBy = "where p.Status=1 and m.Status=1  and p.ProductId= " + clickedControl.Tag;
                        dmnSearch.OrderBy = "";
                        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSource.Count > 0)
                        {
                            bool flagexist = true;

                            #region Existing Product QTY update
                            for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                            {
                                if (Convert.ToInt32(clickedControl.Tag) == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.Product) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                                {

                                    var dataresult = ProductQtyCheckMenuQty(Convert.ToInt32(clickedControl.Tag), 0);
                                    decimal typeqty = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                                    int availableQty = dataresult.Quantity;
                                    prep = dataresult.PreparationMode;
                                    if (prep == Convert.ToInt32(PreparationType.ReadyFood) && typeqty > availableQty)
                                    {
                                        MessageBox.Show($"Only {availableQty} Qty Available.",
                                                        "Stock Warning",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Warning);
                                        return;
                                    }

                                    Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                                    totalcellamount(i);
                                    decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                                    txtGrandTotal.Text = Convert.ToString(totalamt);
                                    txtnetamount.Text = Convert.ToString(totalamt);
                                    netAmount();
                                    flagexist = false;
                                    break;
                                }
                            }
                            #endregion

                            #region New Product
                            if (flagexist)
                            {
                                if (Booking_Status == false)
                                {
                                    if (string.IsNullOrEmpty(txtGrandTotal.Text))
                                        txtGrandTotal.Text = "0";
                                }

                                #region main Grid Inseration
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

                                Dgv_Selected_Item.Rows[r1].Cells[iaction].Value = Properties.Resources.close_price_change;
                                Dgv_Selected_Item.Rows[r1].Cells[iaction].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                Dgv_Selected_Item.Rows[r1].Cells[iaction].Style.NullValue = null;
                                Dgv_Selected_Item.Rows[r1].Height = 40;
                                ((DataGridViewImageCell)Dgv_Selected_Item.Rows[r1].Cells[iaction]).ImageLayout = DataGridViewImageCellLayout.Zoom;

                                Dgv_Selected_Item.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = Convert.ToString(gridSource[0].Field1);
                                Dgv_Selected_Item.Rows[r1].Cells[ibarcode2].Value = Convert.ToString(gridSource[0].Field1);
                                Dgv_Selected_Item.Rows[r1].Cells[iname].Value = Convert.ToString(gridSource[0].Field2);
                                Dgv_Selected_Item.Rows[r1].Cells[iname].Style.Font = new Font(Dgv_Selected_Item.Font, FontStyle.Bold);
                                Dgv_Selected_Item.Rows[r1].Cells[iqty].Value = "1";
                                Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = Convert.ToDecimal(gridSource[0].Field3);
                                Dgv_Selected_Item.Rows[r1].Cells[israte].Value = Convert.ToDecimal(gridSource[0].Field3);
                                Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = Convert.ToDecimal(gridSource[0].Field4);

                                if (Convert.ToDecimal(gridSource[0].Field10) > 0)
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
                                Dgv_Selected_Item.Rows[r1].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Primary);
                                Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = Color.White;
                                Dgv_Selected_Item.Rows[r1].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
                                Dgv_Selected_Item.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.Product);
                                Dgv_Selected_Item.Rows[r1].Cells[iPreparationModeno].Value = Convert.ToInt32(gridSource[0].Field13);
                                totalcellamount(r1);
                                Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value);
                                Dgv_Selected_Item.Rows.Add(1);
                                GridSlno_Refresh();

                                decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                                txtGrandTotal.Text = Convert.ToString(totalamt);
                                txtnetamount.Text = Convert.ToString(totalamt);
                                #endregion

                                netAmount();

                                //labelproductmName.Click += new EventHandler(buttonitemnameClickEventHandler);
                                //buttonAdd.Click += new EventHandler(ButtonbuttonAddClickEventHandler);
                                //buttonRemove.Click += new EventHandler(ButtonRemoveClickEventHandler);
                                //pb_item_delete.Click += new EventHandler(pb_item_deleteClickEventHandler);
                            }
                            #endregion
                        }
                    }
                }
                checksave_refresh();

            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  DashboardButtonClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        #region Variation product view 
        private void DisposeControls(FlowLayoutPanel flp)
        {
            foreach (Control ctrl in flp.Controls)
            {
                ctrl.Dispose();
            }
            flp.Controls.Clear();
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
                clsgen.LogException("Order page Button_addonaddClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page Button_Remove_addonqtyClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void ShowVariationAddon(int pid)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();

                UIHelper.CenterAndResize(pnl_variation_addon, 55, 65);
                UIHelper.RoundCorners(pnl_variation_addon, 25);
                UIHelper.RoundCorners(tlp_header_vari_addon, 25);
                UIHelper.RoundCorners(tlp_variation_addon, 25);
                pnl_variation_addon.Visible = true;

                // Query variations and addons
                dmnSearch.Select = "pavid Field1,Caption Field2,pav.Price Field3,pav.Aid Field4,a.Price Field5,pav.Type Field6,pav.ProductId Field7,pav.CCode Field8";
                dmnSearch.Table = "tblProduct_Addon_Variation pav left join tblAddon a on a.Aid=pav.Aid and a.Status=1";
                dmnSearch.WhereNdGroupBy = " where pav.Status=1 and ProductId= " + pid;
                dmnSearch.OrderBy = "order by pav.pavid desc";
                var gridSource_va = bllSearch.GetSearchData("prdSearch", dmnSearch);

                if (gridSource_va.Count > 0)
                {
                    tlp_main.Enabled = false;
                    previousClickedButton = null;
                    addonflag = true;
                    variflag = true;
                    lblsizetotal.Text = "0";
                    lbladdontotal.Text = "0";

                    // Dispose old controls before adding new ones
                    DisposeControls(flp_addons);
                    DisposeControls(flp_variation);

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
                            tbl_button_vari.RowStyles.Add(new RowStyle(SizeType.AutoSize, 1));
                            tbl_button_vari.RowStyles.Add(new RowStyle(SizeType.AutoSize, 1));
                            tbl_button_vari.AutoSize = true;
                            tbl_button_vari.Height = 40;
                            tbl_button_vari.Margin = new Padding(15, 5, 0, 20);

                            Button button = new Button();
                            button.Text = item.Field2;
                            button.AutoSize = true;
                            button.Dock = DockStyle.Fill;
                            button.BackColor = ColorTranslator.FromHtml("#3f88c5");
                            button.Name = "btnVariation_" + item.Field1;
                            button.Tag = item.Field1;
                            button.AccessibleDescription = item.Field3;
                            button.Font = new Font("Arial", 14, FontStyle.Regular);
                            button.Click += ButtonVariation_Click;

                            Label label = new Label();
                            label.Text = item.Field3;
                            label.AutoSize = true;
                            label.Dock = DockStyle.Top;
                            label.Font = new Font("Arial", 12, FontStyle.Regular);
                            label.TextAlign = ContentAlignment.MiddleCenter;

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
                            tlp_selected_Item_Box.Height = 30;
                            tlp_selected_Item_Box.ColumnCount = 4;
                            tlp_selected_Item_Box.BorderStyle = BorderStyle.None;
                            tlp_selected_Item_Box.BackColor = seleproduct_buttoncolor;

                            tlp_selected_Item_Box.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                            tlp_selected_Item_Box.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                            tlp_selected_Item_Box.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                            tlp_selected_Item_Box.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                            // Item Button
                            Button Button_Item = new Button();
                            Button_Item.Text = item.Field2;
                            Button_Item.Name = "btn_addon_" + item.Field8 + "_" + item.Field1;
                            Button_Item.Tag = item.Field8 + "_" + item.Field1;
                            Button_Item.AccessibleDescription = item.Field5;
                            Button_Item.AutoSize = true;
                            Button_Item.Height = 20;
                            Button_Item.FlatAppearance.BorderSize = 0;
                            Button_Item.FlatStyle = FlatStyle.Flat;
                            Button_Item.BackColor = Color.White;
                            Button_Item.Click += new EventHandler(Button_addonaddClickEventHandler);
                            Button_Item.Font = new Font(Button_Item.Font.FontFamily, 10, FontStyle.Bold);
                            tlp_selected_Item_Box.Controls.Add(Button_Item, 0, 0);

                            // Quantity Label
                            Label label = new Label();
                            label.Text = "0";
                            label.Name = "lbl_addon_qty_" + item.Field8 + "_" + item.Field1;
                            label.Tag = item.Field8 + "_" + item.Field1;
                            label.AutoSize = true;
                            label.Dock = DockStyle.Fill;
                            label.ForeColor = Color.White;
                            label.TextAlign = ContentAlignment.MiddleCenter;
                            label.Font = new Font(label.Font.FontFamily, 10, FontStyle.Bold);
                            label.Visible = false;
                            tlp_selected_Item_Box.Controls.Add(label, 1, 0);

                            // Remove Button
                            Button Button_Remove = new Button();
                            Button_Remove.Text = "-";
                            Button_Remove.Name = "btn_addon_remqty_" + item.Field8 + "_" + item.Field1;
                            Button_Remove.AccessibleDescription = item.Field5;
                            Button_Remove.Tag = item.Field8 + "_" + item.Field1;
                            Button_Remove.AutoSize = true;
                            Button_Remove.Height = 30;
                            Button_Remove.Dock = DockStyle.Fill;
                            Button_Remove.FlatAppearance.BorderSize = 0;
                            Button_Remove.FlatStyle = FlatStyle.Flat;
                            Button_Remove.BackColor = Color.White;
                            Button_Remove.Click += new EventHandler(Button_Remove_addonqtyClickEventHandler);
                            Button_Remove.Font = new Font(Button_Remove.Font.FontFamily, 10, FontStyle.Bold);
                            // Button_Remove.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Button_Remove.Width, Button_Remove.Height, 50, 50));
                            using (GraphicsPath gp = new GraphicsPath())
                            {
                                gp.AddArc(0, 0, Button_Remove.Height, Button_Remove.Height, 180, 90);
                                gp.AddArc(Button_Remove.Width - Button_Remove.Height, 0, Button_Remove.Height, Button_Remove.Height, 270, 90);
                                gp.AddArc(Button_Remove.Width - Button_Remove.Height, Button_Remove.Height - Button_Remove.Height, Button_Remove.Height, Button_Remove.Height, 0, 90);
                                gp.AddArc(0, Button_Remove.Height - Button_Remove.Height, Button_Remove.Height, Button_Remove.Height, 90, 90);

                                gp.CloseFigure();

                                Button_Remove.Region = new Region(gp);
                            }

                            tlp_selected_Item_Box.Controls.Add(Button_Remove, 2, 0);

                            flp_addons.Controls.Add(tlp_selected_Item_Box);

                            addonflag = false;
                        }
                    }
                    #endregion

                    // Adjust visibility
                    label35.Visible = !variflag;
                    flp_variation.Visible = !variflag;
                    lblsizetotal.Visible = !variflag;
                    label40.Visible = !variflag;

                    label38.Visible = !addonflag;
                    flp_addons.Visible = !addonflag;
                    lbladdontotal.Visible = !addonflag;
                    label46.Visible = !addonflag;

                    flp_addons.ResumeLayout();

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page ShowVariationAddon()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                clsgen.LogException("Order page ButtonVariation_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        #endregion
        public class MenuQtyResult
        {
            public int Quantity { get; set; }
            public int PreparationMode { get; set; }
        }

        public MenuQtyResult ProductQtyCheckMenuQty(int pid, int ptype)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();

                dmnSearch.Select = "Quantity Field1, PreparationMode Field2";
                dmnSearch.Table = "tblMenu";

                if (ptype == 3)
                    dmnSearch.WhereNdGroupBy = " WHERE ProductId = " + pid + " AND producttype=3 AND Status = 1";
                else
                    dmnSearch.WhereNdGroupBy = " WHERE ProductId = " + pid + " AND Status = 1";

                var result = bllSearch.GetSearchData("prdSearch", dmnSearch);

                if (result != null && result.Count > 0)
                {
                    return new MenuQtyResult
                    {
                        Quantity = Convert.ToInt32(result[0].Field1),
                        PreparationMode = Convert.ToInt32(result[0].Field2)
                    };
                }

                return new MenuQtyResult { Quantity = 0, PreparationMode = 0 };
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page ProductQtyCheckMenuQty()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
                return new MenuQtyResult { Quantity = 0, PreparationMode = 0 };
            }
        }
        #endregion

        #region Driect Sale Product Button 
        public void CheckMultiplePrice(int id)
        {
            try
            {
                foreach (Control c in tlp_price_list.Controls.Cast<Control>().ToList())
                {
                    c.Click -= MyButtonClickEventHandler;
                    c.Click -= closebuttonClickEventHandler;
                    foreach (Control child in c.Controls) child.Click -= MyButtonClickEventHandler;
                    c.Dispose();
                }
                tlp_price_list.Controls.Clear();
                tlp_price_list.RowStyles.Clear();
                tlp_price_list.RowCount = 0;

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
                        pricebutton.BackgroundImage = FoodandBeverage.Properties.Resources.Button_Price_change;
                        pricebutton.BackgroundImageLayout = ImageLayout.Stretch;
                        pricebutton.FlatStyle = FlatStyle.Flat;
                        pricebutton.ForeColor = System.Drawing.Color.White;
                        pricebutton.Font = new Font("Georgia", 14);
                        pricebutton.Width = 300;
                        pricebutton.Height = 70;
                        pricebutton.Click += new EventHandler(MyButtonClickEventHandler);
                        tlp_price_list.RowCount++;
                        tlp_price_list.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                        tlp_price_list.Controls.Add(control1, 0, rowIndex);
                        rowIndex++;
                    }

                    UIHelper.CenterAndResize(pnlprice, 30, 25);
                    UIHelper.RoundCorners(pnlprice, 25);
                    UIHelper.RoundCorners(tlp_price_nav, 25);

                    tlp_price_list.AutoScroll = true;
                    tlp_multipleprice.Controls.Add(tlp_price_list, 0, 1);
                }
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
                            bool flagexist = true;
                            if (Booking_Status == false)
                            {
                                if (string.IsNullOrEmpty(txtGrandTotal.Text))
                                    txtGrandTotal.Text = "0";
                            }

                            #region Existing product Qty update

                            for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                            {
                                if (Convert.ToInt32(gridSourceDetails[0].Field15) == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                                {
                                    decimal cqoh = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                                    if (cqoh > Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value))
                                    {
                                        CMessageBox.Show("Aviablle Quantity is : " + Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value), "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }
                                    else
                                    {
                                        flagexist = false;
                                        Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                                        totalcellamount(i);
                                        decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                                        txtGrandTotal.Text = Convert.ToString(totalamt);
                                        netAmount();
                                        break;
                                    }

                                }
                            }
                            #endregion

                            #region  New Product
                            if (flagexist)
                            {
                                #region Grid Inseration multiple 
                                r1 = Dgv_Selected_Item.RowCount - 1;

                                Dgv_Selected_Item.Rows[r1].Cells[islno].Value = r1 + 1;

                                Dgv_Selected_Item.Rows[r1].Cells[iaction].Value = Properties.Resources.close_price_change;
                                Dgv_Selected_Item.Rows[r1].Cells[iaction].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                Dgv_Selected_Item.Rows[r1].Cells[iaction].Style.NullValue = null;
                                Dgv_Selected_Item.Rows[r1].Height = 40;
                                ((DataGridViewImageCell)Dgv_Selected_Item.Rows[r1].Cells[iaction]).ImageLayout = DataGridViewImageCellLayout.Zoom;

                                Dgv_Selected_Item.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = gridSourceDetails[0].Field3;
                                Dgv_Selected_Item.Rows[r1].Cells[ibarcode2].Value = gridSourceDetails[0].Field3;
                                Dgv_Selected_Item.Rows[r1].Cells[iname].Value = gridSourceDetails[0].Field1;
                                Dgv_Selected_Item.Rows[r1].Cells[iname].Style.Font = new Font(Dgv_Selected_Item.Font, FontStyle.Bold);
                                Dgv_Selected_Item.Rows[r1].Cells[iunit].Value = gridSourceDetails[0].Field12;
                                Dgv_Selected_Item.Rows[r1].Cells[iunitid].Value = gridSourceDetails[0].Field11;
                                Dgv_Selected_Item.Rows[r1].Cells[iqty].Value = "1";
                                Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = gridSourceDetails[0].Field4;
                                Dgv_Selected_Item.Rows[r1].Cells[israte].Value = gridSourceDetails[0].Field5;
                                Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = gridSourceDetails[0].Field6;
                                if (Checkoffer(id))
                                {
                                    Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = gridSource_offer[0].Field10;
                                }
                                else
                                {
                                    Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = gridSourceDetails[0].Field16;
                                }
                                Dgv_Selected_Item.Rows[r1].Cells[idisamt].Value = "0";
                                Dgv_Selected_Item.Rows[r1].Cells[iitemid].Value = gridSourceDetails[0].Field2;
                                Dgv_Selected_Item.Rows[r1].Cells[itax].Value = gridSourceDetails[0].Field7;
                                Dgv_Selected_Item.Rows[r1].Cells[itaxtype].Value = gridSourceDetails[0].Field8;
                                Dgv_Selected_Item.Rows[r1].Cells[itype].Value = gridSourceDetails[0].Field13;
                                Dgv_Selected_Item.Rows[r1].Cells[itaxsettingid].Value = gridSourceDetails[0].Field14;
                                Dgv_Selected_Item.Rows[r1].Cells[ipridno].Value = gridSourceDetails[0].Field15;
                                Dgv_Selected_Item.Rows[r1].Cells[istatusno].Value = 1;
                                Dgv_Selected_Item.Rows[r1].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Primary);
                                Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = Color.White;
                                Dgv_Selected_Item.Rows[r1].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
                                Dgv_Selected_Item.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.DirectSale);
                                Dgv_Selected_Item.Rows[r1].Cells[ioriginalqty].Value = gridSourceDetails[0].Field17;

                                totalcellamount(r1);
                                Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value);

                                decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                                txtGrandTotal.Text = Convert.ToString(totalamt);

                                netAmount();
                                Dgv_Selected_Item.Rows.Add(1);
                                GridSlno_Refresh();

                                #endregion
                            }
                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  CheckMultiplePrice()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                        if (Convert.ToInt32(gridSource[0].Field15) == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.DirectSale) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                        {
                            decimal cqoh = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                            if (cqoh > Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value))
                            {
                                CMessageBox.Show("Aviablle Quantity is : " + Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value), "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                //Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) - 1;
                                txtitemcode_serach.Text = "";
                                txtitemcode_serach.Focus();
                                return;
                            }
                            Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
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

                    Dgv_Selected_Item.Rows[r1].Cells[iaction].Value = Properties.Resources.close_price_change;
                    Dgv_Selected_Item.Rows[r1].Cells[iaction].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Dgv_Selected_Item.Rows[r1].Cells[iaction].Style.NullValue = null;
                    Dgv_Selected_Item.Rows[r1].Height = 40;
                    ((DataGridViewImageCell)Dgv_Selected_Item.Rows[r1].Cells[iaction]).ImageLayout = DataGridViewImageCellLayout.Zoom;

                    Dgv_Selected_Item.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = gridSource[0].Field3;
                    Dgv_Selected_Item.Rows[r1].Cells[ibarcode2].Value = gridSource[0].Field3;
                    Dgv_Selected_Item.Rows[r1].Cells[iname].Value = gridSource[0].Field1;
                    Dgv_Selected_Item.Rows[r1].Cells[iname].Style.Font = new Font(Dgv_Selected_Item.Font, FontStyle.Bold);
                    Dgv_Selected_Item.Rows[r1].Cells[iunit].Value = gridSource[0].Field12;
                    Dgv_Selected_Item.Rows[r1].Cells[iunitid].Value = gridSource[0].Field11;
                    Dgv_Selected_Item.Rows[r1].Cells[iqty].Value = "1";
                    Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = gridSource[0].Field4;
                    Dgv_Selected_Item.Rows[r1].Cells[israte].Value = gridSource[0].Field5;
                    Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = gridSource[0].Field6;

                    if (Checkoffer(Convert.ToInt32(gridSource[0].Field2)))
                    {
                        Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = gridSource_offer[0].Field10;
                    }
                    else
                    {
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
                    Dgv_Selected_Item.Rows[r1].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Primary);
                    Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = Color.White;
                    Dgv_Selected_Item.Rows[r1].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
                    Dgv_Selected_Item.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.DirectSale);
                    Dgv_Selected_Item.Rows[r1].Cells[ioriginalqty].Value = gridSource[0].Field10;

                    totalcellamount(r1);
                    Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value);

                    decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                    txtGrandTotal.Text = Convert.ToString(totalamt);

                    netAmount();
                    Dgv_Selected_Item.Rows.Add(1);
                    GridSlno_Refresh();

                    #endregion

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
                clsgen.LogException("Order page  MyButtonClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        #endregion

        #region Booking
        public void CheckRe_Bookingorder()
        {
            try
            {
                //int ordermsid = 0, addonslno = 0; ;
                //BllSearch<DmnOrderMaster> bllSearchmaster = new BllSearch<DmnOrderMaster>();
                //DmnSearch dmnSearchmaster = new DmnSearch();
                //dmnSearchmaster.Select = "*,od.status status,bm.AdvancePay AdvancePay,bm.ReservationTotal ReservationTotal,om.Gender Gender,om.OrderMasterId,(select count(*)  from tblBookDetails where Rid=0 and Status=1 and BookmasterId= " + Bookingmasterid + ") as Tablecount,od.NetAmount NetAmount ";
                //dmnSearchmaster.Table = " tblOrderMaster om left join tblOrderDetails od on od.OrderMasterId = om.OrderMasterId " +
                //    "left join tblbookMaster bm on bm.BookmasterId=om.BookmasterId";
                //dmnSearchmaster.WhereNdGroupBy = "where  om.Status=1 and om.BookmasterId=" + Bookingmasterid;
                //dmnSearchmaster.OrderBy = "";
                //gridSourcemaster = bllSearchmaster.GetSearchData("prdSearch", dmnSearchmaster);
                //if (gridSourcemaster.Count > 0)
                //{
                //    OrderDateTime = gridSourcemaster[0].CreatedDateTime;
                //    ordermsid = Convert.ToInt32(gridSourcemaster[0].OrderMasterId);

                //    if (Convert.ToInt32(gridSourcemaster[0].SalesMasterId) > 0)
                //        txtsalemasterid.Text = Convert.ToString(gridSourcemaster[0].SalesMasterId);
                //    else
                //        txtsalemasterid.Text = "0";

                //    #region Customer Details Filling
                //    txtph.Text = Convert.ToString(gridSourcemaster[0].Telephone);
                //    txtemailid.Text = Convert.ToString(gridSourcemaster[0].Email);
                //    txtaddress.Text = Convert.ToString(gridSourcemaster[0].Address);
                //    txtcusname.Text = Convert.ToString(gridSourcemaster[0].CustomerName);
                //    if (Convert.ToInt32(gridSourcemaster[0].Gender) == 1)
                //        rb_male.Checked = true;
                //    else
                //        rb_female.Checked = true;

                //    txtph.Enabled = false;
                //    txtemailid.Enabled = false;
                //    txtaddress.Enabled = false;
                //    txtcusname.Enabled = false;
                //    rb_female.Enabled = false;
                //    rb_male.Enabled = false;
                //    btn_userreg_submit.Visible = true;
                //    btn_clear_cus.Visible = true;
                //    #endregion

                //    btn_itemwise.Enabled = false;

                //    txttotalQTY.Text = Convert.ToString(gridSourcemaster[0].Quantity);
                //    txtGrandTotal.Text = "0";
                //    ordercount = 0;
                //    Booking_Status = true;
                //    tlp_seletedItem_main.Controls.Clear();
                //    //FillExistingProducts();
                //    lblorderid.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);
                //    label18.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);
                //    label25.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);

                //    lblorderid.Visible = true;
                //    label10.Visible = true;

                //    lbladvanceamt.Visible = true;
                //    txtadvance.Visible = true;
                //    txtpkchrge_amt.Visible = false;
                //    lblpkchrge_amt.Visible = false;

                //    ReservationTotal = Convert.ToDecimal(gridSourcemaster[0].ReservationTotal);
                //    txtreservationtotal.Text = Convert.ToString(gridSourcemaster[0].ReservationTotal);
                //    txtadvance.Text = Convert.ToString(gridSourcemaster[0].FinalAmount);

                //    txtdis_per.Text = Convert.ToString(gridSourcemaster[0].DiscountPer);

                //    #region Add Table Charge on Top
                //    // Configure the TableLayoutPanel
                //    TableLayoutPanel tlp_seletedItem_tablecharge = new TableLayoutPanel();
                //    tlp_seletedItem_tablecharge.Name = "tlp_seletedItem_tablecharge" + Bookingmasterid;
                //    float[] columnPercentages_tablecharge = { 44.82f, 6.27f, 14.94f, 6.02f, 19.04f, 8.92f };
                //    for (int x = 0; x < columnPercentages_tablecharge.Length; x++)
                //    {
                //        tlp_seletedItem_tablecharge.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercentages_tablecharge[x]));
                //    }
                //    tlp_seletedItem_tablecharge.Dock = DockStyle.Top;
                //    tlp_seletedItem_tablecharge.BackColor = Color.LightBlue;
                //    tlp_seletedItem_tablecharge.Height = 45;
                //    tlp_seletedItem_tablecharge.Enabled = true;
                //    //tlp_seletedItem_tablecharge.Padding = new Padding(0, 0, 10, 0);

                //    // Configure the Button for Item name
                //    Button btntablename = new Button();
                //    btntablename.BackColor = Color.Beige;
                //    btntablename.Height = 40;
                //    btntablename.Font = new Font(btntablename.Font.FontFamily, 8, FontStyle.Bold);
                //    btntablename.Text = "Table Charges";
                //    btntablename.Name = "btntablename_" + Bookingmasterid;
                //    btntablename.Click += new EventHandler(tablenameclcik);
                //    btntablename.Tag = Bookingmasterid;
                //    //btntablename.Enabled = false;

                //    // Configure the textbox enter QTY
                //    TextBox txttablecount = new TextBox();
                //    txttablecount.Text = Convert.ToString(gridSourcemaster[0].Tablecount);
                //    txttablecount.BackColor = Color.Beige;
                //    txttablecount.TextAlign = HorizontalAlignment.Center;
                //    txttablecount.Margin = new Padding(0, 14, 0, 0);
                //    txttablecount.Font = new Font(txttablecount.Font.FontFamily, 8, FontStyle.Bold);
                //    txttablecount.Name = "txttablecount_" + Bookingmasterid;
                //    txttablecount.Enabled = false;

                //    // Configure the textbox Price
                //    TextBox txttabel_totprice = new TextBox();
                //    txttabel_totprice.BackColor = Color.Beige;
                //    txttabel_totprice.TextAlign = HorizontalAlignment.Center;
                //    txttabel_totprice.Margin = new Padding(0, 14, 0, 0);
                //    txttabel_totprice.Font = new Font(txttabel_totprice.Font.FontFamily, 8, FontStyle.Bold);
                //    txttabel_totprice.Text = Convert.ToString(gridSourcemaster[0].ReservationTotal);
                //    txttabel_totprice.Name = "txttotalresprice_" + Bookingmasterid;
                //    txttabel_totprice.Enabled = false;

                //    // Configure Item Delete Button
                //    PictureBox pb_item_table_delete = new PictureBox();
                //    pb_item_table_delete.BackColor = Color.Transparent;
                //    pb_item_table_delete.Dock = DockStyle.Top;
                //    pb_item_table_delete.Height = 40;
                //    pb_item_table_delete.Padding = new Padding(5, 5, 5, 5);
                //    pb_item_table_delete.Enabled = false;
                //    pb_item_table_delete.BackgroundImage = FoodandBeverage.Properties.Resources.Delete_Grid;
                //    pb_item_table_delete.Enabled = true;
                //    pb_item_table_delete.Tag = Bookingmasterid;
                //    pb_item_table_delete.Name = "pb_item_table_delete" + Bookingmasterid;
                //    pb_item_table_delete.BackgroundImageLayout = ImageLayout.Zoom; // Change to Zoom instead of Stretch
                //    pb_item_table_delete.Click += new EventHandler(pb_item_table_delete_ClickEventHandler);

                //    //Dock Setting
                //    tlp_seletedItem_tablecharge.Dock = DockStyle.Top;
                //    btntablename.Dock = DockStyle.Top;
                //    txttablecount.Dock = DockStyle.Fill;
                //    txttabel_totprice.Dock = DockStyle.Fill;

                //    // Get the current row count
                //    currentRowCount = tlp_seletedItem_tablecharge.RowCount;

                //    // Add controls to TableLayoutPanel at the next row
                //    tlp_seletedItem_tablecharge.Controls.Add(btntablename, 0, currentRowCount);
                //    tlp_seletedItem_tablecharge.Controls.Add(txttablecount, 2, currentRowCount);
                //    tlp_seletedItem_tablecharge.Controls.Add(txttabel_totprice, 4, currentRowCount);
                //    tlp_seletedItem_tablecharge.Controls.Add(pb_item_table_delete, 5, currentRowCount);
                //    // Increment the row count
                //    tlp_seletedItem_tablecharge.RowCount = currentRowCount + 1;
                //    // Add the new TableLayoutPanel to tlp_seletedItem_main
                //    tlp_seletedItem_main.Controls.Add(tlp_seletedItem_tablecharge);

                //    #endregion

                //    var distinctCategories = gridSourcemaster
                //        .Select(item => item.OrderNo)
                //        .Distinct()
                //        .ToList();

                //    int j = 0;
                //    txtordermasterid.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);
                //    //if (string.IsNullOrEmpty(txtGrandTotal.Text))
                //    //    txtGrandTotal.Text = "0";
                //    foreach (var orderNo in distinctCategories)
                //    {
                //        BllSearch<DmnOrderDetails> bllSearch = new BllSearch<DmnOrderDetails>();
                //        DmnSearch dmnSearch = new DmnSearch();
                //        dmnSearch.Select = "od.*,ti.TableCode as TableCode";
                //        dmnSearch.Table = "tblOrderDetails od left join tblTable_info ti on ti.Tid=od.tid";
                //        dmnSearch.WhereNdGroupBy = "where od.OrderMasterId= " + gridSourcemaster[j].OrderMasterId + " and od.Status IN (1, 2, 3,4) and Orderno=" + orderNo;
                //        dmnSearch.OrderBy = "";
                //        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                //        if (gridSource.Count > 0)
                //        {
                //            #region Order Add to the second Top
                //            //heading Top
                //            Panel pnlbox = new Panel();


                //            pnlbox.Name = "pnlbox" + gridSourcemaster[j].OrderMasterId;
                //            pnlbox.Dock = DockStyle.Top;
                //            pnlbox.Height = 15;
                //            pnlbox.Margin = new Padding(0, 0, 0, 5);
                //            //Frist Row Cateory Heading
                //            // Configure the Label
                //            Label lblcategory_head = new Label();
                //            lblcategory_head.Name = "lbcate_head_" + gridSourcemaster[j].OrderMasterId;
                //            lblcategory_head.Tag = gridSourcemaster[j].OrderMasterId;
                //            int ordernumber = j + 1;
                //            lblcategory_head.Text = "Order ID : " + ordernumber + "           [" + gridSource[0].CreatedDateTime + "]";// + "    Table : " + gridSource[0].TableCode;
                //            lblcategory_head.ForeColor = Color.Black;
                //            lblcategory_head.BackColor = Color.Transparent;
                //            lblcategory_head.Dock = DockStyle.Fill;
                //            lblcategory_head.Font = new Font(lblcategory_head.Font.FontFamily, 9, FontStyle.Bold);
                //            pnlbox.Controls.Add(lblcategory_head);

                //            tlp_seletedItem_main.Controls.Add(pnlbox);
                //            #endregion

                //            Dgv_Selected_Item.Rows.Clear();

                //            txtordermasterid.Text = Convert.ToString(gridSourcemaster[j].OrderMasterId);
                //            txtentrie_descr.Text = Convert.ToString(gridSourcemaster[j].Remarks);

                //            for (int i = 0; i < gridSource.Count; i++)
                //            {
                //                if (gridSource[i].Status == 3)
                //                    ordercount = ordercount + 1;

                //                if (gridSource[i].Status == 4)
                //                {
                //                    bookinorder_F = true;

                //                    #region Grid fill data
                //                    if (gridSource[i].AddonStatus == Convert.ToInt32(AddonType.AddonType) || gridSource[i].AddonStatus == Convert.ToInt32(AddonType.None))
                //                        addonslno = 0;
                //                    else
                //                        addonslno = addonslno + 1;

                //                    Dgv_Selected_Item.Rows.Add(1);
                //                    r1 = Dgv_Selected_Item.RowCount - 1;
                //                    Dgv_Selected_Item.Rows[i].Cells[iorderdetailsidno].Value = Convert.ToInt32(gridSource[i].OrderDetailsId);
                //                    Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = Convert.ToString(gridSource[i].ItemBarCode);
                //                    Dgv_Selected_Item.Rows[r1].Cells[islno].Value = r1 + 1;
                //                    Dgv_Selected_Item.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //                    Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = Convert.ToString(gridSource[i].ItemBarCode);
                //                    Dgv_Selected_Item.Rows[r1].Cells[ibarcode2].Value = Convert.ToString(gridSource[i].ItemBarCode);
                //                    Dgv_Selected_Item.Rows[r1].Cells[iname].Value = Convert.ToString(gridSource[i].ItemName);
                //                    Dgv_Selected_Item.Rows[r1].Cells[iamount].Value = Convert.ToString(gridSource[i].Amount);
                //                    Dgv_Selected_Item.Rows[r1].Cells[itotal].Value = Convert.ToString(gridSource[i].NetAmount);
                //                    Dgv_Selected_Item.Rows[r1].Cells[iqty].Value = Convert.ToInt32(gridSource[i].Quantity);
                //                    Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = Convert.ToDecimal(gridSource[i].PurchaseRate);
                //                    Dgv_Selected_Item.Rows[r1].Cells[israte].Value = Convert.ToDecimal(gridSource[i].Srate);
                //                    Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = Convert.ToDecimal(gridSource[i].MRP);
                //                    Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = Convert.ToDecimal(gridSource[i].DiscountPer);
                //                    Dgv_Selected_Item.Rows[r1].Cells[idisamt].Value = "0";
                //                    Dgv_Selected_Item.Rows[r1].Cells[iitemid].Value = Convert.ToDecimal(gridSource[i].ItemId);
                //                    Dgv_Selected_Item.Rows[r1].Cells[itax].Value = Convert.ToString(gridSource[i].Tax);
                //                    Dgv_Selected_Item.Rows[r1].Cells[itaxtype].Value = Convert.ToString(gridSource[i].TaxType);
                //                    Dgv_Selected_Item.Rows[r1].Cells[itype].Value = Convert.ToInt32(gridSource[i].TableType);
                //                    //Dgv_Selected_Item.Rows[r1].Cells[itaxsettingid].Value = Convert.ToInt32(gridSource[0].tax);
                //                    Dgv_Selected_Item.Rows[r1].Cells[istatusno].Value = 1;
                //                    Dgv_Selected_Item.Rows[r1].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Primary);
                //                    Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = Color.White;
                //                    Dgv_Selected_Item.Rows[r1].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
                //                    Dgv_Selected_Item.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(gridSource[i].TableType);
                //                    Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value = Convert.ToDecimal(gridSource[i].MarketPrice);

                //                    if (gridSource[i].AddonStatus == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                //                    {
                //                        Dgv_Selected_Item.Rows[r1].Cells[islno].Value = addonslno;
                //                        Dgv_Selected_Item.Rows[r1].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Secondary);
                //                        Dgv_Selected_Item.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                //                        Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = Color.OliveDrab;
                //                        Dgv_Selected_Item.Rows[r1].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Secondary);
                //                    }

                //                    #endregion
                //                }

                //                if (gridSource[i].AddonStatus == Convert.ToInt32(AddonType.AddonType) || gridSource[i].AddonStatus == Convert.ToInt32(AddonType.None))
                //                {
                //                    #region Add data to the List
                //                    // Configure the TableLayoutPanel
                //                    TableLayoutPanel tlp_seletedItem_col = new TableLayoutPanel();
                //                    float[] columnPercentages = { 44.82f, 6.27f, 14.94f, 6.02f, 19.04f, 8.92f };
                //                    for (int x = 0; x < columnPercentages.Length; x++)
                //                    {
                //                        tlp_seletedItem_col.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercentages[x]));
                //                    }
                //                    tlp_seletedItem_col.Dock = DockStyle.Top;
                //                    tlp_seletedItem_col.BackColor = Color.LightBlue;
                //                    tlp_seletedItem_col.Height = 45;
                //                    tlp_seletedItem_col.Enabled = true;
                //                    tlp_seletedItem_col.Padding = new Padding(0, 0, 10, 0);
                //                    // Configure the Button for Item name
                //                    Button buttonitemname = new Button();
                //                    buttonitemname.BackColor = Color.Beige;
                //                    buttonitemname.Height = 40;
                //                    buttonitemname.Font = new Font(buttonitemname.Font.FontFamily, 8, FontStyle.Bold);

                //                    //// Configure the Button for add Qty
                //                    //Button buttonAdd = new Button();
                //                    //buttonAdd.Text = "+";
                //                    //buttonAdd.BackColor = Color.Beige;
                //                    //buttonAdd.Margin = new Padding(0, 14, 0, 0);
                //                    //buttonAdd.Font = new Font(buttonAdd.Font.FontFamily, 8, FontStyle.Bold);

                //                    // Configure the textbox enter QTY
                //                    TextBox txtqty = new TextBox();
                //                    txtqty.Text = Convert.ToString(gridSource[i].Quantity);
                //                    txtqty.BackColor = Color.Beige;
                //                    txtqty.TextAlign = HorizontalAlignment.Center;
                //                    txtqty.Margin = new Padding(0, 14, 0, 0);
                //                    txtqty.Font = AppFonts.GetFont(8);

                //                    //// Configure the textbox enter QTY
                //                    //Button buttonRemove = new Button();
                //                    //buttonRemove.Text = "-";
                //                    //buttonRemove.BackColor = Color.Beige;
                //                    //buttonRemove.Margin = new Padding(0, 14, 0, 0);
                //                    //buttonRemove.Font = new Font(buttonRemove.Font.FontFamily, 8, FontStyle.Bold);

                //                    // Configure the textbox Price
                //                    TextBox txtprice = new TextBox();

                //                    txtprice.BackColor = Color.Beige;
                //                    txtprice.TextAlign = HorizontalAlignment.Center;
                //                    txtprice.Margin = new Padding(0, 14, 0, 0);
                //                    txtprice.Font = new Font(txtprice.Font.FontFamily, 8, FontStyle.Bold);

                //                    // Configure Item Delete Button
                //                    PictureBox pb_item_delete = new PictureBox();
                //                    pb_item_delete.BackColor = Color.Transparent;
                //                    pb_item_delete.Dock = DockStyle.Top;
                //                    pb_item_delete.Height = 40;
                //                    pb_item_delete.Padding = new Padding(5, 5, 5, 5);
                //                    pb_item_delete.Enabled = false;
                //                    if (Convert.ToInt32(Food_Status.Booking_order) == Convert.ToInt32(gridSource[i].Status))
                //                    {
                //                        pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.Delete_Grid;
                //                        pb_item_delete.Enabled = true;
                //                    }
                //                    else if (gridSource[i].Status == Convert.ToInt32(Food_Status.Pending))
                //                        pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.kitchen_oder_icon;
                //                    else if (gridSource[i].Status == Convert.ToInt32(Food_Status.FoodProcessing))
                //                        pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.food_preparation;
                //                    else if (gridSource[i].Status == Convert.ToInt32(Food_Status.Complete))
                //                        pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.orderpage_food_finished_icon;

                //                    pb_item_delete.BackgroundImageLayout = ImageLayout.Zoom; // Change to Zoom instead of Stretch
                //                    decimal amount = Convert.ToDecimal(gridSource[i].NetAmount);
                //                    decimal tamt = Convert.ToDecimal(txtGrandTotal.Text) + amount;
                //                    txtGrandTotal.Text = Convert.ToString(tamt);

                //                    tlp_seletedItem_col.Name = "tlp_seletedItem_col_e_" + gridSource[i].ItemId + gridSource[i].TableType;
                //                    tlp_seletedItem_col.Tag = gridSource[i].PrId;
                //                    tlp_seletedItem_col.AccessibleDescription = "1";

                //                    buttonitemname.Text = gridSource[i].ItemName;
                //                    buttonitemname.Name = "buttonitemname_e_" + gridSource[i].ItemId + gridSource[i].TableType;
                //                    buttonitemname.Tag = gridSource[i].PrId;
                //                    buttonitemname.Enabled = false;

                //                    //buttonAdd.Name = "buttonAdd_e_" + gridSource[i].ItemId + gridSource[i].TableType;
                //                    //buttonAdd.Tag = gridSource[i].PrId;
                //                    //buttonAdd.Enabled = false;

                //                    txtqty.Name = "txtqty_e_" + gridSource[i].ItemId + gridSource[i].TableType;
                //                    txtqty.Tag = gridSource[i].PrId;
                //                    txtqty.Enabled = false;

                //                    //buttonRemove.Name = "btn_remove_e_" + gridSource[i].ItemId + gridSource[i].TableType;
                //                    //buttonRemove.Tag = gridSource[i].PrId;
                //                    //buttonRemove.Enabled = false;

                //                    txtprice.Text = Convert.ToString(gridSource[i].MarketPrice);
                //                    txtprice.Name = "txtprice_e_" + gridSource[i].ItemId + gridSource[i].TableType;
                //                    txtprice.Tag = gridSource[i].PrId;
                //                    txtprice.Enabled = false;

                //                    pb_item_delete.Name = "btn_remove_" + gridSource[i].ItemId + gridSource[i].TableType;
                //                    pb_item_delete.Tag = gridSource[i].ItemId;
                //                    pb_item_delete.AccessibleDescription = Convert.ToString(gridSource[i].TableType);
                //                    //pb_item_delete.Click += new EventHandler(pb_item_delete_booking_ClickEventHandler);

                //                    //Dock Setting
                //                    tlp_seletedItem_col.Dock = DockStyle.Top;
                //                    buttonitemname.Dock = DockStyle.Top;
                //                    //buttonAdd.Dock = DockStyle.Top;
                //                    txtqty.Dock = DockStyle.Fill;
                //                    //buttonRemove.Dock = DockStyle.Top;
                //                    txtprice.Dock = DockStyle.Fill;

                //                    // Get the current row count
                //                    currentRowCount = tlp_seletedItem_col.RowCount;

                //                    // Add controls to TableLayoutPanel at the next row
                //                    tlp_seletedItem_col.Controls.Add(buttonitemname, 0, currentRowCount);
                //                    //tlp_seletedItem_col.Controls.Add(buttonAdd, 1, currentRowCount);
                //                    tlp_seletedItem_col.Controls.Add(txtqty, 2, currentRowCount);
                //                    //tlp_seletedItem_col.Controls.Add(buttonRemove, 3, currentRowCount);
                //                    tlp_seletedItem_col.Controls.Add(txtprice, 4, currentRowCount);


                //                    tlp_seletedItem_col.Controls.Add(pb_item_delete, 5, currentRowCount);

                //                    // Increment the row count
                //                    tlp_seletedItem_col.RowCount = currentRowCount + 1;
                //                    // Add the new TableLayoutPanel to tlp_seletedItem_main
                //                    tlp_seletedItem_main.Controls.Add(tlp_seletedItem_col);

                //                    #endregion
                //                }

                //            }
                //        }
                //        j = j + 1;
                //    }

                //    #region End Heading Grid List
                //    //heading Top
                //    Panel pnlboxend = new Panel();
                //    pnlboxend.Name = "pnlbox_end";
                //    pnlboxend.Dock = DockStyle.Top;
                //    pnlboxend.Height = 15;
                //    pnlboxend.Margin = new Padding(0, 5, 0, 5);
                //    // Configure the Label
                //    Label lblend = new Label();
                //    lblend.Name = "lbcate_end";
                //    lblend.Text = DateTime.Now.ToString(" yyyy:MM:dd  HH:mm:ss");
                //    lblend.ForeColor = Color.Black;
                //    lblend.BackColor = Color.Transparent;
                //    lblend.Dock = DockStyle.Fill;
                //    lblend.Font = new Font(lblend.Font.FontFamily, 11, FontStyle.Bold);
                //    pnlboxend.Controls.Add(lblend);
                //    tlp_seletedItem_main.Controls.Add(pnlboxend);
                //    #endregion

                //    if (bookinorder_F)
                //    {
                //        Showprint_Save(Convert.ToInt32(ShowButtonType.save));
                //        bookinorder_F = false;
                //        btn_frm_close.Dock = DockStyle.Left;
                //        btncancelrev.Visible = true;
                //    }
                //    else
                //    {
                //        btncancelrev.Visible = false;
                //        BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                //        DmnSearch dmnSearch = new DmnSearch();
                //        dmnSearch.Select = "CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1";
                //        dmnSearch.Table = "tblOrderDetails";
                //        dmnSearch.WhereNdGroupBy = "WHERE status!=0 and OrderMasterId = " + ordermsid;
                //        dmnSearch.OrderBy = "";
                //        var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);
                //        if (gridSource_order.Count > 0)
                //        {
                //            if (Convert.ToInt32(gridSource_order[0].Field1) == 1)
                //            {
                //                checksave_refresh();
                //            }
                //            else
                //            {
                //                if (PageType == Convert.ToInt32(FoodandBeverage.Interfaces.PageType.Billpage))
                //                    Showprint_Save(Convert.ToInt32(ShowButtonType.Print));
                //                else
                //                    Showprint_Save(Convert.ToInt32(ShowButtonType.Nnone));
                //            }
                //        }
                //    }
                //    decimal Gamt = Convert.ToDecimal(txtGrandTotal.Text) + Convert.ToDecimal(txtreservationtotal.Text);
                //    txtGrandTotal.Text = Convert.ToString(Gamt);

                //    discountAmnt();
                //    netAmount();
                //}
                //else
                //{
                //    OrderRunning_Status = false;
                //    Showprint_Save(Convert.ToInt32(ShowButtonType.save));
                //    lblorderid.Visible = false;
                //    label10.Visible = false;
                //}
                //textb_shape();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  CheckRe_Bookingorder()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        #endregion

        #region  Tax & Discount Calculation Fuction 
        public void totalcellamount_exit_product(int cellproductid)
        {
            try
            {
                //if (dgv_exitsting_product.Rows[cellproductid].Cells[iitemid].Value != "" && dgv_exitsting_product.Rows[cellproductid].Cells[iprate].Value != "" && dgv_exitsting_product.Rows[cellproductid].Cells[iqty].Value != "")
                //{
                //    decimal amount = Convert.ToDecimal(dgv_exitsting_product.Rows[cellproductid].Cells[israte].Value) * Convert.ToDecimal(dgv_exitsting_product.Rows[cellproductid].Cells[iqty].Value);
                //    dgv_exitsting_product.Rows[cellproductid].Cells[iamount].Value = amount.ToString("N2");

                //    //cellDiscountPercentage(cellproductid);
                //    if (Convert.ToInt32(dgv_exitsting_product.Rows[cellproductid].Cells[itabletype].Value) != Convert.ToInt32(Tabletype.ComboProduct))
                //        cellDiscountAmount(cellproductid);

                //    decimal discount = Convert.ToDecimal(dgv_exitsting_product.Rows[cellproductid].Cells[idisamt].Value);
                //    cellTaxAmount(cellproductid, amount - discount);

                //    if (Convert.ToString(dgv_exitsting_product.Rows[cellproductid].Cells[itax].Value) == "Exclusive")
                //    {
                //        decimal tot = (amount - Convert.ToDecimal(dgv_exitsting_product.Rows[cellproductid].Cells[idisamt].Value))
                //        + Convert.ToDecimal(dgv_exitsting_product.Rows[cellproductid].Cells[itaxamt].Value);
                //        dgv_exitsting_product.Rows[cellproductid].Cells[itotal].Value = tot.ToString("N2");
                //    }
                //    if (Convert.ToString(dgv_exitsting_product.Rows[cellproductid].Cells[itax].Value) == "Inclusive")
                //    {
                //        decimal tot = (amount - Convert.ToDecimal(dgv_exitsting_product.Rows[cellproductid].Cells[idisamt].Value));
                //        dgv_exitsting_product.Rows[cellproductid].Cells[itotal].Value = tot.ToString("N2");
                //    }
                //}

            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  totalcellamount_exit_product()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[cellproductid].Cells[itabletype].Value) != Convert.ToInt32(Tabletype.ComboProduct))
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
                    {
                        if (Convert.ToString(Dgv_Selected_Item.Rows[cellproductid].Cells[itaxtype].Value) == "GST")
                        {
                            Dgv_Selected_Item.Rows[cellproductid].Cells[iigstamtno].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[iComboTaxamtno].Value) * Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[iqty].Value);
                        }
                        else if (Convert.ToString(Dgv_Selected_Item.Rows[cellproductid].Cells[itaxtype].Value) == "VAT")
                        {
                            Dgv_Selected_Item.Rows[cellproductid].Cells[ivatamtno].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[iComboTaxamtno].Value) * Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[iqty].Value);
                        }
                        Dgv_Selected_Item.Rows[cellproductid].Cells[itaxamt].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[iComboTaxamtno].Value) * Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[iqty].Value);
                        Dgv_Selected_Item.Rows[cellproductid].Cells[itotal].Value = amount.ToString("N2");

                    }


                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  totalcellamount()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                clsgen.LogException("Order page  cellTaxAmount()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                clsgen.LogException("Order page  cellDiscountAmount()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }

        private void FrmOrderConsoleTabletmode_Resize(object sender, EventArgs e)
        {
            maxColumns = columncount;
            spacing = Productspace;
            containerWidth = flp_productview.ClientSize.Width;
            tileW = (int)(((containerWidth - ((maxColumns + 1) * spacing)) / maxColumns) * Productwidth);
            tileH = (int)(tileW * Productheight);
        }

        private void btn_pa__toggle_Click(object sender, EventArgs e)
        {
            if (flp_oper_buttons.Visible)
            {
                flp_oper_buttons.Visible = false;
                return;
            }
            textb_shape_paymnet();
            UIHelper.ShowCentered(this, flp_oper_buttons);
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
                clsgen.LogException("Order page  cellDiscountPercentage()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        #endregion

        #region Combo Product Button click
        public void ComboProductFill(int id)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "ofm.omid id , ofm.OfferName Field1 ,ofm.Discountper Field2, ofm.tax Field3 ,ofm.taxtype Field4,ofm.TaxSettingsId Field5,ofm.FinalPrice Field6,ofd.Productname Field7,ofd.ProductId Field8,ofd.Prid Field9,ofd.Quantity Field10,ofm.TaxPer Field11,ofm.TaxAmount Field12";
                dmnSearch.Table = "tblOfferMaster ofm LEFT JOIN tblOfferDetails ofd on ofd.Omid=ofm.Omid and ofd.status=1 ";
                dmnSearch.WhereNdGroupBy = "where ofm.Status=1 and ofd.Omid = " + id;
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    bool flagexist = true;

                    #region Existing combo product
                    for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                    {
                        if (id == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.ComboProduct) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                        {
                            int prep = 0;
                            var dataresult = ProductQtyCheckMenuQty(Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value), 3);
                            decimal typeqty = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                            int availableQty = dataresult.Quantity;
                            prep = dataresult.PreparationMode;
                            if (prep == Convert.ToInt32(PreparationType.ReadyFood) && typeqty > availableQty)
                            {
                                MessageBox.Show($"Only {availableQty} Qty Available.",
                                                "Stock Warning",
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Warning);
                                return;
                            }

                            flagexist = false;
                            Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                            totalcellamount(i);
                            decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                            txtGrandTotal.Text = Convert.ToString(totalamt);
                            txtnetamount.Text = Convert.ToString(totalamt);
                            netAmount();
                            break;
                        }
                    }
                    #endregion

                    #region New Combo Product
                    if (flagexist)
                    {
                        if (Booking_Status == false)
                        {
                            if (string.IsNullOrEmpty(txtGrandTotal.Text))
                                txtGrandTotal.Text = "0";
                        }

                        decimal itemprice = 0;

                        #region Grid inseration Combo Product

                        Dgv_Selected_Item.SuspendLayout();
                        r1 = Dgv_Selected_Item.RowCount - 1;
                        Dgv_Selected_Item.Rows[r1].Cells[islno].Value = r1 + 1;

                        Dgv_Selected_Item.Rows[r1].Cells[iaction].Value = ImgClose;
                        Dgv_Selected_Item.Rows[r1].Cells[iaction].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        Dgv_Selected_Item.Rows[r1].Cells[iaction].Style.NullValue = null;
                        ((DataGridViewImageCell)Dgv_Selected_Item.Rows[r1].Cells[iaction]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                        Dgv_Selected_Item.Rows[r1].Height = 40;

                        Dgv_Selected_Item.Rows[r1].Cells[iname].Value = Convert.ToString(gridSource[0].Field1);
                        Dgv_Selected_Item.Rows[r1].Cells[iname].Style.Font = new Font(Dgv_Selected_Item.Font, FontStyle.Bold);
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
                        Dgv_Selected_Item.Rows[r1].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Primary);
                        Dgv_Selected_Item.Rows[r1].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
                        Dgv_Selected_Item.Rows[r1].Cells[isubofferid].Value = 0;
                        Dgv_Selected_Item.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.ComboProduct);
                        Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = Color.DarkGray;
                        Dgv_Selected_Item.Rows[r1].Cells[iComboTaxamtno].Value = Convert.ToDecimal(gridSource[0].Field12);
                        Dgv_Selected_Item.Rows[r1].Cells[itaxamt].Value = Convert.ToDecimal(gridSource[0].Field12);
                        Dgv_Selected_Item.Rows[r1].Cells[itaxper].Value = Convert.ToDecimal(gridSource[0].Field11);
                        if (Convert.ToString(gridSource[0].Field4) == "GST")
                        {
                            Dgv_Selected_Item.Rows[r1].Cells[iigstamtno].Value = Convert.ToDecimal(gridSource[0].Field12);

                        }
                        else if (Convert.ToString(gridSource[0].Field4) == "VAT")
                        {
                            Dgv_Selected_Item.Rows[r1].Cells[ivatamtno].Value = Convert.ToDecimal(gridSource[0].Field12);

                        }
                        Dgv_Selected_Item.Rows.Add(1);
                        totalcellamount(r1);
                        Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value);
                        itemprice = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value);
                        decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                        txtGrandTotal.Text = Convert.ToString(totalamt);
                        txtnetamount.Text = Convert.ToString(totalamt);
                        //for (int j = 0; j < gridSource.Count; j++)
                        //{
                        //    #region Grid Inseration
                        //    int exit_orderdetailid = 0;
                        //    r1 = Dgv_Selected_Item.RowCount - 1;

                        //    Dgv_Selected_Item.Rows[r1].Cells[iaction].Value = Imgaddon;
                        //    Dgv_Selected_Item.Rows[r1].Cells[iaction].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        //    Dgv_Selected_Item.Rows[r1].Cells[iaction].Style.NullValue = null;
                        //    ((DataGridViewImageCell)Dgv_Selected_Item.Rows[r1].Cells[iaction]).ImageLayout = DataGridViewImageCellLayout.Zoom;

                        //    Dgv_Selected_Item.Rows[r1].ReadOnly = true;
                        //    Dgv_Selected_Item.Rows[r1].Cells[islno].Value = j + 1;
                        //    Dgv_Selected_Item.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        //    Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = 0;
                        //    Dgv_Selected_Item.Rows[r1].Cells[ibarcode2].Value = 0;
                        //    Dgv_Selected_Item.Rows[r1].Cells[iname].Value = Convert.ToString(gridSource[j].Field7);
                        //    Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = 0;
                        //    Dgv_Selected_Item.Rows[r1].Cells[iqty].Value = Convert.ToString(gridSource[j].Field10);
                        //    Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = 0;
                        //    Dgv_Selected_Item.Rows[r1].Cells[idisamt].Value = "0";

                        //    //if(Convert.ToInt32(gridSource[j].Field9)>0)
                        //    //    Dgv_Selected_Item.Rows[r1].Cells[iitemid].Value = Convert.ToInt32(gridSource[j].Field9);
                        //    //else
                        //    //    Dgv_Selected_Item.Rows[r1].Cells[iitemid].Value = Convert.ToInt32(gridSource[j].Field8);
                        //    Dgv_Selected_Item.Rows[r1].Cells[itype].Value = 0;
                        //    Dgv_Selected_Item.Rows[r1].Cells[istatusno].Value = 1;
                        //    Dgv_Selected_Item.Rows[r1].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Secondary);
                        //    Dgv_Selected_Item.Rows[r1].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.Enable);
                        //    Dgv_Selected_Item.Rows[r1].Cells[isubofferid].Value = Convert.ToInt32(gridSource[0].Id);
                        //    Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = AddonBGcolor;

                        //    Dgv_Selected_Item.Rows.Add(1);
                        //    #endregion
                        //}

                        Dgv_Selected_Item.ResumeLayout();
                        #endregion

                        netAmount();
                        GridSlno_Refresh();
                    }
                    #endregion

                    checksave_refresh();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  ComboProductFill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        #endregion

        #region Form close and minimize
        private void btncolse_Click(object sender, EventArgs e)
        {
            if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                ResetTableViewPage();
            clsgen.Memoryrelease();
            this.Close();
        }
        public void ResetTableViewPage()
        {
            if (System.Windows.Forms.Application.OpenForms["FrmTableTabletView"] != null)
            {
                FrmTableTabletView _instanceFrmTableTabletView = Application.OpenForms["FrmTableTabletView"] as FrmTableTabletView;
                _instanceFrmTableTabletView.Visible = true;
                _instanceFrmTableTabletView.TableStaus(0);
            }
        }

        #endregion
    }
}
