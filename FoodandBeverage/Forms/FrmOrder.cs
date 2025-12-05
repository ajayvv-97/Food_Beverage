using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FoodandBeverage.Forms.UserControls;

namespace FoodandBeverage.Forms
{
    public partial class FrmOrder : Form
    {

        #region Declaration

        public int tableId = 0;
        public int Bookingmasterid = 0;
        public int BookingType = 0;
        public int PageType = 0;
        public int OmId = 0;
        public int sourcepage = 0;

        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsgen = new ClsGeneral();

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

        TableLayoutPanel tlp_price_list = new TableLayoutPanel();
        Color seleproduct_buttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);

        int currentRowCount = 0;
        public int Productrate_ID = 0;
        public int saletype = 0;
        public int Proid_Prid = 0;
        public bool OrderRunning_Status = false;
        public bool Booking_Status = false;
        public bool combined_Status = false;
        public bool Pickup_Status = false;
        public bool combined_Pickup_Status = false;

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

        public int R_screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int R_screenHeight = Screen.PrimaryScreen.Bounds.Height;

        public List<Namelist> CheckedNames = new List<Namelist>();
        public List<Namelist> BillList = new List<Namelist>();
        public List<OrderDetailsInfo> orderDetailsInfoList = new List<OrderDetailsInfo>();
        public int Splitypeno = 0;
        public int Existingsalemasterid = 0;
        private List<DmnNameList> checkedtableNames;
        public decimal ReservationTotal = 0;
        public bool itemcode_serach_Status = false;
        public DateTime OrderDateTime;

        public bool bookinorder_F = false;//chcek frist order from reservation page

        string reading_RFID = "";
        public bool RFID_Status = false;

        private Button previousClickedButton = null;
        public List<Variation_addon_List> Checked_VariationAddon_List = new List<Variation_addon_List>();
        public int Global_ProductId = 0;
        public string addon_Vari_Id = "";
        public bool variflag = true, addonflag = true;
        public int Split_Rowno = 0;
        public int maxLength_variation = 3;

        public bool BillCopy_Status = false;
        public bool KOT_saleprint_status = false;

        public int Pickup_ID = 0;
        public int columncount = Convert.ToInt32(ConfigurationManager.AppSettings["OrderPageProductViewRow"]);
        public decimal Productheight = Convert.ToDecimal(ConfigurationManager.AppSettings["Productheight"]);
        public decimal Productwidth = Convert.ToDecimal(ConfigurationManager.AppSettings["Productheight"]);
        public int Productspace = Convert.ToInt32(ConfigurationManager.AppSettings["Productspace"]);

        public int columncount_offer = Convert.ToInt32(ConfigurationManager.AppSettings["OrderPageProductViewRow_Offer"]);
        private bool isCategoryExpanded = false;
        public Color AddonBGcolor = ColorTranslator.FromHtml("#EBFFEB");
        bool isOperationShown = false;

        public readonly Font boldFont = new Font("Segoe UI", 10, FontStyle.Bold);
        public readonly Font normalFont = new Font("Segoe UI", 10, FontStyle.Regular);
        private readonly Image imgTime = Properties.Resources.time;
        private readonly Image imgComplete = Properties.Resources.orderview_complete;
        private readonly Image imgKitchen = Properties.Resources.kitchen_oder_icon;
        private readonly Image ImgClose = Properties.Resources.close_price_change;
        private readonly Image Imgaddon = Properties.Resources.addon_comb_item;

        private Font f11;
        private Font f9;
        private Font f10;
        private Font f10Strike;
        private Font f10Bold;

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
        public string msgnodata = " No records available";

        public int McategoryID = 0;
        bool DirectPayBill = false;
        int refundsmid = 0,Refundkitchenslno=0,Refundorderno=0;decimal refundtotal = 0,Refundproductwisetotal=0;
        bool Refundproductflag = false;
        #endregion
        public FrmOrder(int tid, int booktype, int bmasterid, List<DmnNameList> checkedtableNames, int Pageno, int Ordermasterid, int sourcepagetype)
        {
            tableId = tid;
            sourcepage = sourcepagetype;
            Bookingmasterid = bmasterid;
            BookingType = booktype;
            PageType = Pageno;
            this.checkedtableNames = checkedtableNames;
            OmId = Ordermasterid;
            InitializeComponent();

            f11 = AppFonts.GetFont(11);
            f9 = AppFonts.GetFont(9);
            f10 = AppFonts.GetFont(10);
            f10Strike = new Font(f10, FontStyle.Strikeout);
            f10Bold = new Font(f10, FontStyle.Bold);
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

        #region Default Fuction and Events 
        //public FrmOrder(int tid, int booktype, int bmasterid, List<DmnNameList> checkedtableNames, int Pageno, int pkid)
        private async void FrmOrder_Load(object sender, EventArgs e)
        {
            try
            {
                pageSize = Pagecount;

                //tlp_footer.Visible = false;
                //tlp_leftmain.RowStyles[2].Height = 0;                 
                //tlp_leftmain.RowStyles[2].SizeType = SizeType.AutoSize;
                //tlp_leftmain.RowStyles[1].SizeType = SizeType.Percent;
                //tlp_leftmain.RowStyles[1].Height = 100f;

                //UIHelper.RoundCorners(tlp_footer, 25, null);
                //UIHelper.RoundCorners(pnl_productview, 25, null);

                if (FrmDashboard.AutoLogoutWhenIdle)
                {
                    //AppSession.RegisterForm(this, (int)LogoutFormList.Home);
                }

                lbltablecode_header.Text = "TABLE CODE ";

                tlp_seletedItem_main.AutoScroll = true;
                tlp_seletedItem_main.HorizontalScroll.Enabled = false;
                tlp_seletedItem_main.HorizontalScroll.Visible = false;
                tlp_seletedItem_main.HorizontalScroll.Maximum = 0;

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

                #region BookingType Checking [Normalorder ,Reverse_overtime_Cancel , CombinedOrderBill,Pickup Order]
                if (BookingType == Convert.ToInt32(DashboardTableType.Normalorder))
                {
                    CheckRe_order();
                    ChecktablekitchenCancel();
                }

                else if (BookingType == Convert.ToInt32(DashboardTableType.Reverse_overtime_Cancel))
                    CheckRe_Bookingorder();
                else if (BookingType == Convert.ToInt32(DashboardTableType.CombinedOrderBill))
                {
                    combined_Status = true;
                    Btnpartpayment.Visible = false;
                    btn_splitpayment.Visible = false;
                    int count = checkedtableNames.Count;
                    lblorderid.Visible = false;
                    label10.Visible = false;
                    Combined_Tables();
                    Combined_Table_Bill();
                }
                else if (BookingType == Convert.ToInt32(DashboardTableType.PickupOrder))
                {
                    CheckRe_Pickuporder();
                    CheckPickupkitchenCancel();
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


                dgv_tableprice.BorderStyle = BorderStyle.None;
                dgv_tableprice.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgv_tableprice.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgv_tableprice.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgv_tableprice.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgv_tableprice.BackgroundColor = Color.White;

                dgv_tableprice.EnableHeadersVisualStyles = false;
                dgv_tableprice.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgv_tableprice.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgv_tableprice.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                #endregion

                #region Palce Holder  Textboxes setting
                txtname_search.Text = "Food Name .....";
                txtitemcode_serach.Text = "Item Code .....";
                txtentrie_descr.Text = "Description Type here .....";
                #endregion

                textb_shape();
                ShowLoadingdGif();
                setperfixsale();
                settablecode();

                btn_clear_cus.Visible = true;

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
            catch (Exception ex)
            {
                clsgen.LogException("Order page  FrmOrder_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  settablecode()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
            catch (Exception ex)
            {
                clsgen.LogException("Order page  Showprint_Save()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        public void PagenationSetup()
        {
            #region Next & Previous Button
            bool needLastRow = ProductData.Count > Pagecount;
            if (needLastRow)
            {
                tlp_leftmain.RowStyles[2].SizeType = SizeType.Percent;
                tlp_leftmain.RowStyles[2].Height = 20f;

                tlp_leftmain.RowStyles[1].SizeType = SizeType.Percent;
                tlp_leftmain.RowStyles[1].Height = 80f;

            }
            else
            {
                tlp_leftmain.RowStyles[2].SizeType = SizeType.AutoSize;
                tlp_leftmain.RowStyles[2].Height = 0;

                tlp_leftmain.RowStyles[1].SizeType = SizeType.Percent;
                tlp_leftmain.RowStyles[1].Height = 100f;
            }

            UIHelper.RoundCorners(tlp_footer, 25, null);
            UIHelper.RoundCorners(pnl_productview, 25, null);

            #endregion
        }
        #endregion

        #region Reopen table , Pickup , Rescervation booking  ,combined table
        public void Combined_Table_Bill()
        {
            try
            {
                BllSearch<DmnOrderMaster> bllSearchmaster = new BllSearch<DmnOrderMaster>();
                DmnSearch dmnSearchmaster = new DmnSearch();
                tlp_seletedItem_main.Controls.Clear();
                txtGrandTotal.Text = "0";
                lbltablecode.Text = string.Empty;
                cmb_tblcode.Items.Clear();
                cmb_tblcode.Items.Add("Combined Table");
                bool isPkidConditionMet = false;
                bool isTablecodeConditionMet = false;
                int j = 0;
                int r = 0;
                int SL_Main = 0;
                int SL_Addon = 0;
                decimal gtotal = 0;
                Dgv_Selected_Item.Rows.Clear();

                for (int k = 0; k < checkedtableNames.Count; k++)
                {
                    lbltablecode.Visible = false;
                    cmb_tblcode.Visible = true;
                    if (checkedtableNames[k].pkid > 0)
                    {
                        cmb_tblcode.Items.Add("PICKUP NO " + Convert.ToString(checkedtableNames[k].pkid));
                        //txtpkchrge_amt.Visible = true;
                        //lblpkchrge_amt.Visible = true;
                        isPkidConditionMet = true;
                    }
                    else
                    {
                        cmb_tblcode.Items.Add(Convert.ToString(checkedtableNames[k].Tablecode));
                        isTablecodeConditionMet = true;
                    }
                    if (isPkidConditionMet && isTablecodeConditionMet)
                    {
                        combined_Pickup_Status = true;
                    }

                    #region Table Name Add to the Top
                    string bannerText = "";
                    if (checkedtableNames[k].pkid > 0)
                    {
                        Dgv_Selected_Item.Rows.Add();
                         bannerText = "Pickup NO : " + checkedtableNames[k].pkid;
                        DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)Dgv_Selected_Item.Rows[r].Cells[iaction];
                        cellcolumntype.Value = imgTime;
                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        Dgv_Selected_Item.Rows[r].Cells[iname].Value = bannerText;
                        Dgv_Selected_Item.Rows[r].DefaultCellStyle.BackColor = Color.LightGray;
                        Dgv_Selected_Item.Rows[r].DefaultCellStyle.Font = normalFont;
                        Dgv_Selected_Item.Rows[r].DefaultCellStyle.ForeColor = Color.Black;
                        Dgv_Selected_Item.Rows[r].ReadOnly = true;
                    }
                    else
                    {
                        Dgv_Selected_Item.Rows.Add();
                        bannerText = "Table : " + checkedtableNames[k].Tablecode;
                        DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)Dgv_Selected_Item.Rows[r].Cells[iaction];
                        cellcolumntype.Value = imgTime;
                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        Dgv_Selected_Item.Rows[r].Cells[iname].Value = bannerText;
                        Dgv_Selected_Item.Rows[r].DefaultCellStyle.BackColor = Color.LightGray;
                        Dgv_Selected_Item.Rows[r].DefaultCellStyle.Font = normalFont;
                        Dgv_Selected_Item.Rows[r].DefaultCellStyle.ForeColor = Color.Black;
                        Dgv_Selected_Item.Rows[r].ReadOnly = true;
                    }
                    r++;
                    #endregion

                    dmnSearchmaster.Select = "*";
                    dmnSearchmaster.Table = "tblOrderMaster om left join tblOrderDetails od on od.OrderMasterId = om.OrderMasterId";
                    dmnSearchmaster.WhereNdGroupBy = "where  om.Status=1 and om.OrderMasterId=" + checkedtableNames[k].Ordermasterid;
                    dmnSearchmaster.OrderBy = "";
                    var gridSourcemaster_combined = bllSearchmaster.GetSearchData("prdSearch", dmnSearchmaster);
                    if (gridSourcemaster_combined.Count > 0)
                    {
                        int orginalCount = gridSourcemaster_combined.Count(item => item.AddonStatus == 1 || item.AddonStatus == 0);

                        OrderDateTime = gridSourcemaster_combined[0].CreatedDateTime;
                        ordercount = 0;
                        lblorderid.Visible = false;
                        label10.Visible = false;

                        var distinctCategories = gridSourcemaster_combined
                            .Select(item => item.OrderNo)
                            .Distinct()
                            .ToList();
                         j = 0;

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
                                Dgv_Selected_Item.Rows.Add();
                                bannerText = "Order ID : " + checkedtableNames[k].Ordermasterid + "[" + gridSource[0].CreatedDateTime + "]";
                                DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)Dgv_Selected_Item.Rows[r].Cells[iaction];
                                cellcolumntype.Value = imgTime;
                                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                Dgv_Selected_Item.Rows[r].Cells[iname].Value = bannerText;
                                Dgv_Selected_Item.Rows[r].DefaultCellStyle.BackColor = Color.LightGray;
                                Dgv_Selected_Item.Rows[r].DefaultCellStyle.Font = normalFont;
                                Dgv_Selected_Item.Rows[r].DefaultCellStyle.ForeColor = Color.Black;
                                Dgv_Selected_Item.Rows[r].ReadOnly = true;
                                #endregion

                                r = r + 1;
                                txtordermasterid.Text = Convert.ToString(gridSourcemaster_combined[0].OrderMasterId);
                                txtentrie_descr.Text = Convert.ToString(gridSourcemaster_combined[0].Remarks);
                                for (int i = 0; i < gridSource.Count; i++)
                                {
                                    gtotal = gtotal + Convert.ToDecimal(gridSource[i].NetAmount);

                                    if (gridSource[i].Status == 3 && (gridSource[i].AddonStatus == 1 || gridSource[i].AddonStatus == 0))
                                        ordercount = ordercount + 1;

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

                                        ((DataGridViewImageCell)Dgv_Selected_Item.Rows[r1].Cells[iaction]).ImageLayout = DataGridViewImageCellLayout.Zoom;
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


                                        if (Convert.ToInt32(gridSource[i].Status) == 3)
                                        {
                                            cellcolumntype = (DataGridViewImageCell)Dgv_Selected_Item.Rows[r].Cells[iaction];
                                            cellcolumntype.Value = imgComplete;
                                            cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;

                                        }
                                        else if (Convert.ToInt32(gridSource[i].Status) == 1)
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
                                            if (Convert.ToInt32(Dgv_Selected_Item.Rows[r].Cells[iMainProductStatusno].Value) == Convert.ToInt32(MainProductStatus.Primary))
                                            {
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

                                                if (Convert.ToInt32(gridSource[i].Status) == 3)
                                                {
                                                    cellcolumntype = (DataGridViewImageCell)Dgv_Selected_Item.Rows[r].Cells[iaction];
                                                    cellcolumntype.Value = imgComplete;
                                                    cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;

                                                }
                                                else if (Convert.ToInt32(gridSource[i].Status) == 1)
                                                {
                                                    cellcolumntype = (DataGridViewImageCell)Dgv_Selected_Item.Rows[r].Cells[iaction];
                                                    cellcolumntype.Value = imgKitchen;
                                                    cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                                }

                                                Dgv_Selected_Item.Rows[r].Cells[ioriginalprice].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[r].Cells[itotal].Value);
                                                r++;
                                            }

                                            #region Combo Detailed product details Show
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
                    }
                }

                txtGrandTotal.Text = Convert.ToString(gtotal);

                if (PageType == Convert.ToInt32(FoodandBeverage.Interfaces.PageType.Billpage))
                    Showprint_Save(Convert.ToInt32(ShowButtonType.Print));

                netAmount();
                cmb_tblcode.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  Combined_Table_Bill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void CheckRe_Bookingorder()
        {
            try
            {
                int ordermsid = 0, addonslno = 0; ;
                BllSearch<DmnOrderMaster> bllSearchmaster = new BllSearch<DmnOrderMaster>();
                DmnSearch dmnSearchmaster = new DmnSearch();
                dmnSearchmaster.Select = "*,od.status status,bm.AdvancePay AdvancePay,bm.ReservationTotal ReservationTotal,om.Gender Gender,om.OrderMasterId,(select count(*)  from tblBookDetails where Rid=0 and Status=1 and BookmasterId= " + Bookingmasterid + ") as Tablecount,od.NetAmount NetAmount ";
                dmnSearchmaster.Table = " tblOrderMaster om left join tblOrderDetails od on od.OrderMasterId = om.OrderMasterId " +
                    "left join tblbookMaster bm on bm.BookmasterId=om.BookmasterId";
                dmnSearchmaster.WhereNdGroupBy = "where  om.Status=1 and om.BookmasterId=" + Bookingmasterid;
                dmnSearchmaster.OrderBy = "";
                gridSourcemaster = bllSearchmaster.GetSearchData("prdSearch", dmnSearchmaster);
                if (gridSourcemaster.Count > 0)
                {
                    OrderDateTime = gridSourcemaster[0].CreatedDateTime;
                    ordermsid = Convert.ToInt32(gridSourcemaster[0].OrderMasterId);

                    if (Convert.ToInt32(gridSourcemaster[0].SalesMasterId) > 0)
                        txtsalemasterid.Text = Convert.ToString(gridSourcemaster[0].SalesMasterId);
                    else
                        txtsalemasterid.Text = "0";

                    #region Customer Details Filling
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
                    #endregion

                    btn_itemwise.Enabled = false;

                    txttotalQTY.Text = Convert.ToString(gridSourcemaster[0].Quantity);
                    txtGrandTotal.Text = "0";
                    ordercount = 0;
                    Booking_Status = true;
                    tlp_seletedItem_main.Controls.Clear();
                    //FillExistingProducts();
                    lblorderid.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);
                    label18.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);
                    label25.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);

                    lblorderid.Visible = true;
                    label10.Visible = true;

                    lbladvanceamt.Visible = true;
                    txtadvance.Visible = true;
                    txtpkchrge_amt.Visible = false;
                    lblpkchrge_amt.Visible = false;

                    ReservationTotal = Convert.ToDecimal(gridSourcemaster[0].ReservationTotal);
                    txtreservationtotal.Text = Convert.ToString(gridSourcemaster[0].ReservationTotal);
                    txtadvance.Text = Convert.ToString(gridSourcemaster[0].FinalAmount);

                    txtdis_per.Text = Convert.ToString(gridSourcemaster[0].DiscountPer);

                    #region Add Table Charge on Top
                    // Configure the TableLayoutPanel
                    TableLayoutPanel tlp_seletedItem_tablecharge = new TableLayoutPanel();
                    tlp_seletedItem_tablecharge.Name = "tlp_seletedItem_tablecharge" + Bookingmasterid;
                    float[] columnPercentages_tablecharge = { 44.82f, 6.27f, 14.94f, 6.02f, 19.04f, 8.92f };
                    for (int x = 0; x < columnPercentages_tablecharge.Length; x++)
                    {
                        tlp_seletedItem_tablecharge.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnPercentages_tablecharge[x]));
                    }
                    tlp_seletedItem_tablecharge.Dock = DockStyle.Top;
                    tlp_seletedItem_tablecharge.BackColor = Color.LightBlue;
                    tlp_seletedItem_tablecharge.Height = 45;
                    tlp_seletedItem_tablecharge.Enabled = true;
                    //tlp_seletedItem_tablecharge.Padding = new Padding(0, 0, 10, 0);

                    // Configure the Button for Item name
                    Button btntablename = new Button();
                    btntablename.BackColor = Color.Beige;
                    btntablename.Height = 40;
                    btntablename.Font = new Font(btntablename.Font.FontFamily, 8, FontStyle.Bold);
                    btntablename.Text = "Table Charges";
                    btntablename.Name = "btntablename_" + Bookingmasterid;
                    btntablename.Click += new EventHandler(tablenameclcik);
                    btntablename.Tag = Bookingmasterid;
                    //btntablename.Enabled = false;

                    // Configure the textbox enter QTY
                    TextBox txttablecount = new TextBox();
                    txttablecount.Text = Convert.ToString(gridSourcemaster[0].Tablecount);
                    txttablecount.BackColor = Color.Beige;
                    txttablecount.TextAlign = HorizontalAlignment.Center;
                    txttablecount.Margin = new Padding(0, 14, 0, 0);
                    txttablecount.Font = new Font(txttablecount.Font.FontFamily, 8, FontStyle.Bold);
                    txttablecount.Name = "txttablecount_" + Bookingmasterid;
                    txttablecount.Enabled = false;

                    // Configure the textbox Price
                    TextBox txttabel_totprice = new TextBox();
                    txttabel_totprice.BackColor = Color.Beige;
                    txttabel_totprice.TextAlign = HorizontalAlignment.Center;
                    txttabel_totprice.Margin = new Padding(0, 14, 0, 0);
                    txttabel_totprice.Font = new Font(txttabel_totprice.Font.FontFamily, 8, FontStyle.Bold);
                    txttabel_totprice.Text = Convert.ToString(gridSourcemaster[0].ReservationTotal);
                    txttabel_totprice.Name = "txttotalresprice_" + Bookingmasterid;
                    txttabel_totprice.Enabled = false;

                    // Configure Item Delete Button
                    PictureBox pb_item_table_delete = new PictureBox();
                    pb_item_table_delete.BackColor = Color.Transparent;
                    pb_item_table_delete.Dock = DockStyle.Top;
                    pb_item_table_delete.Height = 40;
                    pb_item_table_delete.Padding = new Padding(5, 5, 5, 5);
                    pb_item_table_delete.Enabled = false;
                    pb_item_table_delete.BackgroundImage = FoodandBeverage.Properties.Resources.Delete_Grid;
                    pb_item_table_delete.Enabled = true;
                    pb_item_table_delete.Tag = Bookingmasterid;
                    pb_item_table_delete.Name = "pb_item_table_delete" + Bookingmasterid;
                    pb_item_table_delete.BackgroundImageLayout = ImageLayout.Zoom; // Change to Zoom instead of Stretch
                    pb_item_table_delete.Click += new EventHandler(pb_item_table_delete_ClickEventHandler);

                    //Dock Setting
                    tlp_seletedItem_tablecharge.Dock = DockStyle.Top;
                    btntablename.Dock = DockStyle.Top;
                    txttablecount.Dock = DockStyle.Fill;
                    txttabel_totprice.Dock = DockStyle.Fill;

                    // Get the current row count
                    currentRowCount = tlp_seletedItem_tablecharge.RowCount;

                    // Add controls to TableLayoutPanel at the next row
                    tlp_seletedItem_tablecharge.Controls.Add(btntablename, 0, currentRowCount);
                    tlp_seletedItem_tablecharge.Controls.Add(txttablecount, 2, currentRowCount);
                    tlp_seletedItem_tablecharge.Controls.Add(txttabel_totprice, 4, currentRowCount);
                    tlp_seletedItem_tablecharge.Controls.Add(pb_item_table_delete, 5, currentRowCount);
                    // Increment the row count
                    tlp_seletedItem_tablecharge.RowCount = currentRowCount + 1;
                    // Add the new TableLayoutPanel to tlp_seletedItem_main
                    tlp_seletedItem_main.Controls.Add(tlp_seletedItem_tablecharge);

                    #endregion

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
                            #region Order Add to the second Top
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
                            lblcategory_head.Text = "Order ID : " + ordernumber + "           [" + gridSource[0].CreatedDateTime + "]";// + "    Table : " + gridSource[0].TableCode;
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
                                    if (gridSource[i].AddonStatus == Convert.ToInt32(AddonType.AddonType) || gridSource[i].AddonStatus == Convert.ToInt32(AddonType.None))
                                        addonslno = 0;
                                    else
                                        addonslno = addonslno + 1;

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
                                    Dgv_Selected_Item.Rows[r1].Cells[itype].Value = Convert.ToInt32(gridSource[i].TableType);
                                    //Dgv_Selected_Item.Rows[r1].Cells[itaxsettingid].Value = Convert.ToInt32(gridSource[0].tax);
                                    Dgv_Selected_Item.Rows[r1].Cells[istatusno].Value = 1;
                                    Dgv_Selected_Item.Rows[r1].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Primary);
                                    Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = Color.White;
                                    Dgv_Selected_Item.Rows[r1].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
                                    Dgv_Selected_Item.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(gridSource[i].TableType);
                                    Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value = Convert.ToDecimal(gridSource[i].MarketPrice);

                                    if (gridSource[i].AddonStatus == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                                    {
                                        Dgv_Selected_Item.Rows[r1].Cells[islno].Value = addonslno;
                                        Dgv_Selected_Item.Rows[r1].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Secondary);
                                        Dgv_Selected_Item.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                        Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = Color.OliveDrab;
                                        Dgv_Selected_Item.Rows[r1].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Secondary);
                                    }

                                    #endregion
                                }

                                if (gridSource[i].AddonStatus == Convert.ToInt32(AddonType.AddonType) || gridSource[i].AddonStatus == Convert.ToInt32(AddonType.None))
                                {
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
                                    tlp_seletedItem_col.Padding = new Padding(0, 0, 10, 0);
                                    // Configure the Button for Item name
                                    Button buttonitemname = new Button();
                                    buttonitemname.BackColor = Color.Beige;
                                    buttonitemname.Height = 40;
                                    buttonitemname.Font = new Font(buttonitemname.Font.FontFamily, 8, FontStyle.Bold);

                                    //// Configure the Button for add Qty
                                    //Button buttonAdd = new Button();
                                    //buttonAdd.Text = "+";
                                    //buttonAdd.BackColor = Color.Beige;
                                    //buttonAdd.Margin = new Padding(0, 14, 0, 0);
                                    //buttonAdd.Font = new Font(buttonAdd.Font.FontFamily, 8, FontStyle.Bold);

                                    // Configure the textbox enter QTY
                                    TextBox txtqty = new TextBox();
                                    txtqty.Text = Convert.ToString(gridSource[i].Quantity);
                                    txtqty.BackColor = Color.Beige;
                                    txtqty.TextAlign = HorizontalAlignment.Center;
                                    txtqty.Margin = new Padding(0, 14, 0, 0);
                                    txtqty.Font = AppFonts.GetFont(8);

                                    //// Configure the textbox enter QTY
                                    //Button buttonRemove = new Button();
                                    //buttonRemove.Text = "-";
                                    //buttonRemove.BackColor = Color.Beige;
                                    //buttonRemove.Margin = new Padding(0, 14, 0, 0);
                                    //buttonRemove.Font = new Font(buttonRemove.Font.FontFamily, 8, FontStyle.Bold);

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
                                    if (Convert.ToInt32(Food_Status.Booking_order) == Convert.ToInt32(gridSource[i].Status))
                                    {
                                        pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.Delete_Grid;
                                        pb_item_delete.Enabled = true;
                                    }
                                    else if (gridSource[i].Status == Convert.ToInt32(Food_Status.Pending))
                                        pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.kitchen_oder_icon;
                                    else if (gridSource[i].Status == Convert.ToInt32(Food_Status.FoodProcessing))
                                        pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.food_preparation;
                                    else if (gridSource[i].Status == Convert.ToInt32(Food_Status.Complete))
                                        pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.orderpage_food_finished_icon;

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

                                    //buttonAdd.Name = "buttonAdd_e_" + gridSource[i].ItemId + gridSource[i].TableType;
                                    //buttonAdd.Tag = gridSource[i].PrId;
                                    //buttonAdd.Enabled = false;

                                    txtqty.Name = "txtqty_e_" + gridSource[i].ItemId + gridSource[i].TableType;
                                    txtqty.Tag = gridSource[i].PrId;
                                    txtqty.Enabled = false;

                                    //buttonRemove.Name = "btn_remove_e_" + gridSource[i].ItemId + gridSource[i].TableType;
                                    //buttonRemove.Tag = gridSource[i].PrId;
                                    //buttonRemove.Enabled = false;

                                    txtprice.Text = Convert.ToString(gridSource[i].MarketPrice);
                                    txtprice.Name = "txtprice_e_" + gridSource[i].ItemId + gridSource[i].TableType;
                                    txtprice.Tag = gridSource[i].PrId;
                                    txtprice.Enabled = false;

                                    pb_item_delete.Name = "btn_remove_" + gridSource[i].ItemId + gridSource[i].TableType;
                                    pb_item_delete.Tag = gridSource[i].ItemId;
                                    pb_item_delete.AccessibleDescription = Convert.ToString(gridSource[i].TableType);
                                    //pb_item_delete.Click += new EventHandler(pb_item_delete_booking_ClickEventHandler);

                                    //Dock Setting
                                    tlp_seletedItem_col.Dock = DockStyle.Top;
                                    buttonitemname.Dock = DockStyle.Top;
                                    //buttonAdd.Dock = DockStyle.Top;
                                    txtqty.Dock = DockStyle.Fill;
                                    //buttonRemove.Dock = DockStyle.Top;
                                    txtprice.Dock = DockStyle.Fill;

                                    // Get the current row count
                                    currentRowCount = tlp_seletedItem_col.RowCount;

                                    // Add controls to TableLayoutPanel at the next row
                                    tlp_seletedItem_col.Controls.Add(buttonitemname, 0, currentRowCount);
                                    //tlp_seletedItem_col.Controls.Add(buttonAdd, 1, currentRowCount);
                                    tlp_seletedItem_col.Controls.Add(txtqty, 2, currentRowCount);
                                    //tlp_seletedItem_col.Controls.Add(buttonRemove, 3, currentRowCount);
                                    tlp_seletedItem_col.Controls.Add(txtprice, 4, currentRowCount);


                                    tlp_seletedItem_col.Controls.Add(pb_item_delete, 5, currentRowCount);

                                    // Increment the row count
                                    tlp_seletedItem_col.RowCount = currentRowCount + 1;
                                    // Add the new TableLayoutPanel to tlp_seletedItem_main
                                    tlp_seletedItem_main.Controls.Add(tlp_seletedItem_col);

                                    #endregion
                                }

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

                    if (bookinorder_F)
                    {
                        Showprint_Save(Convert.ToInt32(ShowButtonType.save));
                        bookinorder_F = false;
                        btn_frm_close.Dock = DockStyle.Left;
                        btncancelrev.Visible = true;
                    }
                    else
                    {
                        btncancelrev.Visible = false;
                        BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "CASE WHEN COUNT(*) = SUM(CASE WHEN Status = 3 THEN 1 ELSE 0 END) THEN 0 ELSE 1 END AS Field1";
                        dmnSearch.Table = "tblOrderDetails";
                        dmnSearch.WhereNdGroupBy = "WHERE status!=0 and OrderMasterId = " + ordermsid;
                        dmnSearch.OrderBy = "";
                        var gridSource_order = bllSearch_order.GetSearchData("prdSearch", dmnSearch);
                        if (gridSource_order.Count > 0)
                        {
                            if (Convert.ToInt32(gridSource_order[0].Field1) == 1)
                            {
                                checksave_refresh();
                            }
                            else
                            {
                                if (PageType == Convert.ToInt32(FoodandBeverage.Interfaces.PageType.Billpage))
                                    Showprint_Save(Convert.ToInt32(ShowButtonType.Print));
                                else
                                    Showprint_Save(Convert.ToInt32(ShowButtonType.Nnone));
                            }
                        }
                    }
                    decimal Gamt = Convert.ToDecimal(txtGrandTotal.Text) + Convert.ToDecimal(txtreservationtotal.Text);
                    txtGrandTotal.Text = Convert.ToString(Gamt);

                    discountAmnt();
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
                clsgen.LogException("Order page  CheckRe_Bookingorder()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void CheckRe_Pickuporder()
        {
            try
            {
                BllSearch<DmnOrderMaster> bllSearchmaster = new BllSearch<DmnOrderMaster>();
                DmnSearch dmnSearchmaster = new DmnSearch();
                dmnSearchmaster.Select = "*";
                dmnSearchmaster.Table = "tblOrderMaster om left join tblOrderDetails od on od.OrderMasterId = om.OrderMasterId and od.Status IN (1, 2, 3) ";
                dmnSearchmaster.WhereNdGroupBy = "where om.OrderMasterId = " + OmId + " and om.Status=1";
                dmnSearchmaster.OrderBy = " order by OrderNo ";
                gridSourcemaster = bllSearchmaster.GetSearchData("prdSearch", dmnSearchmaster);
                if (gridSourcemaster.Count > 0)
                {
                    tlp_refund_ktcancel.Visible = true;
                    btn_showmenu.Enabled = false;
                    dgv_ordercancel.Columns[cnactionno].Visible = false;

                    btn_cancle_pickup.Visible = true;
                    int orginalCount = gridSourcemaster.Count(item => item.AddonStatus == 1 || item.AddonStatus == 0);
                    OrderDateTime = gridSourcemaster[0].CreatedDateTime;
                    txtGrandTotal.Text = "0";

                    if (Convert.ToInt32(gridSourcemaster[0].SalesMasterId) > 0)
                        txtsalemasterid.Text = Convert.ToString(gridSourcemaster[0].SalesMasterId);
                    else
                        txtsalemasterid.Text = "0";

                    Pickup_ID = Convert.ToInt32(gridSourcemaster[0].PkId);
                    Refundkitchenslno = Convert.ToInt32(gridSourcemaster[0].Kitchennumber);
                    Refundorderno = Convert.ToInt32(gridSourcemaster[0].OrderNo);

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

                    lbladvanceamt.Visible = false;
                    txtadvance.Visible = false;

                    //txtpkchrge_amt.Visible = true;
                    //lblpkchrge_amt.Visible = true;

                    ordercount = 0;
                    Pickup_Status = true;

                    cmb_tblcode.Visible = false;
                    lbltablecode.Visible = true;

                    lbltablecode.Text = Convert.ToString(gridSourcemaster[0].Kitchennumber);
                    lbltablecode_header.Text = "PICK UP TOCKEN NO ";

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
                    int r = 0;
                    int SL_Main = 0;
                    int SL_Addon = 0;
                    decimal gtotal = 0;
                    txtordermasterid.Text = Convert.ToString(gridSourcemaster[0].OrderMasterId);

                    if (string.IsNullOrEmpty(txtGrandTotal.Text))
                        txtGrandTotal.Text = "0";

                    Dgv_Selected_Item.Rows.Clear();
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
                            DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)Dgv_Selected_Item.Rows[r].Cells[iaction];
                            cellcolumntype.Value = imgTime;
                            cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                            Dgv_Selected_Item.Rows[r].Cells[iname].Value = bannerText;
                            Dgv_Selected_Item.Rows[r].DefaultCellStyle.BackColor = Color.LightGray;
                            Dgv_Selected_Item.Rows[r].DefaultCellStyle.Font =normalFont;
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

                                gtotal= gtotal+ Convert.ToDecimal(gridSource[i].NetAmount);
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
                                    Dgv_Selected_Item.Rows[r].Cells[itax].Value =Convert.ToString(gridSource[i].Tax);
                                    Dgv_Selected_Item.Rows[r].Cells[itaxtype].Value = Convert.ToString(gridSource[i].TaxType);

                                    Dgv_Selected_Item.Rows[r].Cells[itotal].Value = Convert.ToDecimal(gridSource[i].NetAmount);

                                    Dgv_Selected_Item.Rows[r].Cells[itype].Value = Convert.ToInt32(gridSource[i].TableType);
                                    Dgv_Selected_Item.Rows[r].Cells[istatusno].Value = 0;
                                    Dgv_Selected_Item.Rows[r].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Primary);
                                    Dgv_Selected_Item.Rows[r].DefaultCellStyle.BackColor = Color.White;
                                    Dgv_Selected_Item.Rows[r].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
                                    Dgv_Selected_Item.Rows[r].Cells[itabletype].Value = Convert.ToInt32(Tabletype.Product);

                                    Dgv_Selected_Item.Rows[r].Cells[ioriginalprice].Value =Convert.ToDecimal(Dgv_Selected_Item.Rows[r].Cells[itotal].Value);

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

                                    Dgv_Selected_Item.Rows[r].Cells[ioriginalprice].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[r].Cells[itotal].Value);

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
                                        Dgv_Selected_Item.Rows.Add();
                                        #region Grid inseration Combo Product
                  
                                            SL_Main++;
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
                                        

                                        #region Combo Detailed product details Show
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
                    //txtGrandTotal.Text = Convert.ToString(gtotal);

                    //if (ordercount == orginalCount && PageType == Convert.ToInt32(FoodandBeverage.Interfaces.PageType.Billpage))
                    //{
                    //    Showprint_Save(Convert.ToInt32(ShowButtonType.Print));
                    //}
                    //else
                    //{
                    //    int rcount = 0;
                    //    for (int i = 0; i < Dgv_Selected_Item.RowCount; i++)
                    //    {
                    //        if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) > 0)
                    //        {
                    //            rcount++;
                    //            break;
                    //        }
                    //    }
                    //    if (rcount == 0)
                    //        Showprint_Save(Convert.ToInt32(ShowButtonType.Nnone));
                    //    else
                    //        Showprint_Save(Convert.ToInt32(ShowButtonType.save));
                    //}

                    if (BookingType == Convert.ToInt32(DashboardTableType.PickupOrder))
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
                    }
                    //netAmount();
                }
                else
                {
                    lbltablecode_header.Text = "PICK UP";
                    Pickup_Status = false;
                    Showprint_Save(Convert.ToInt32(ShowButtonType.save));
                    lblorderid.Visible = false;
                    label10.Visible = false;
                    cmb_tblcode.Visible = false;
                    lbltablecode.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  CheckRe_Pickuporder()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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

                    cmb_tblcode.Visible = false;
                    lbltablecode.Visible = true;
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

                    if (ordercount == orginalCount && ( (PageType == Convert.ToInt32(FoodandBeverage.Interfaces.PageType.Billpage) ||( PageType == Convert.ToInt32(FoodandBeverage.Interfaces.PageType.OrderPage) ))))
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
        public void FillExistingProducts()
        {
            try
            {
                if (gridSourcemaster.Count > 0)
                {
                    if (checkedtableNames.Count == 0)
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
                            dgv_exitsting_product.Rows[r].Cells[itype].Value = Convert.ToInt32(gridSourcemaster[i].TableType);
                            //dgv_exitsting_product.Rows[i].Cells[itaxsettingid].Value = Convert.ToInt32(gridSourcemaster[i].);
                            dgv_exitsting_product.Rows[r].Cells[istatusno].Value = 1;
                            dgv_exitsting_product.Rows[r].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Primary);
                            dgv_exitsting_product.Rows[r].DefaultCellStyle.BackColor = Color.White;
                            dgv_exitsting_product.Rows[r].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
                            dgv_exitsting_product.Rows[r].Cells[itabletype].Value = Convert.ToInt32(Tabletype.Product);
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
                            dgv_exitsting_product.Rows[r].Cells[itype].Value = gridSourcemaster[i].TableType;
                            //dgv_exitsting_product.Rows[i].Cells[itaxsettingid].Value = gridSourcemaster[i].Field14;
                            dgv_exitsting_product.Rows[r].Cells[ipridno].Value = gridSourcemaster[i].PrId;
                            dgv_exitsting_product.Rows[r].Cells[istatusno].Value = 1;
                            dgv_exitsting_product.Rows[r].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Primary);
                            dgv_exitsting_product.Rows[r].DefaultCellStyle.BackColor = Color.White;
                            dgv_exitsting_product.Rows[r].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
                            dgv_exitsting_product.Rows[r].Cells[itabletype].Value = Convert.ToInt32(Tabletype.DirectSale);
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
                                dgv_exitsting_product.Rows[r].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Primary);
                                dgv_exitsting_product.Rows[r].Cells[istatusno].Value = 1;
                                dgv_exitsting_product.Rows[r].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
                                dgv_exitsting_product.Rows[r].Cells[isubofferid].Value = 0;
                                dgv_exitsting_product.Rows[r].Cells[itabletype].Value = Convert.ToInt32(Tabletype.ComboProduct);
                                dgv_exitsting_product.Rows[r].DefaultCellStyle.BackColor = Color.DarkGray;
                                totalcellamount_exit_product(r);
                                dgv_exitsting_product.Rows[r].Cells[ioriginalprice].Value = Convert.ToDecimal(dgv_exitsting_product.Rows[r].Cells[itotal].Value);

                                dgv_exitsting_product.Rows.Add(gridSource.Count);
                                r = r + 1;
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
                                    dgv_exitsting_product.Rows[r].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Secondary);
                                    dgv_exitsting_product.Rows[r].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.Enable);
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
                clsgen.LogException("Order page  FillExistingProducts()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        #endregion

        #region Normal Product AutoResponsive_ProductTiles
        private async void SidebuttonClickEventHandler(object sender, EventArgs e)
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

                    MakeRounded(tile, 18);

                    TableLayoutPanel tlpMain = new TableLayoutPanel();
                    tlpMain.Parent = tile;
                    tlpMain.RowCount = 2;
                    tlpMain.ColumnCount = 1;
                    tlpMain.Dock = DockStyle.Fill;
                    tlpMain.AccessibleDescription = Convert.ToString(Saletypeno);
                    //tlpMain.BorderStyle = BorderStyle.FixedSingle;
                    tlpMain.Tag = productId;

                    //tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 85f));
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));

                    //PictureBox picProduct = new PictureBox();
                    //picProduct.Dock = DockStyle.Fill;
                    //picProduct.SizeMode = PictureBoxSizeMode.Zoom;
      
                    //picProduct.Tag = productId;
                    //picProduct.AccessibleDescription = Convert.ToString(Saletypeno);

                    //picProduct.Image = Properties.Resources.Deafult_food_pic;
                    //if (Saletypeno != 1)
                    //{
                    //    int insType = (int)Interfaces.Inseration_Type.Product_Photo;
                    //    _ = LoadImageAsync(picProduct, insType, productId);
                    //}
                    //picProduct.Click += PicProduct_Click;
                    //tlpMain.Controls.Add(picProduct, 0, 0);

                    Label lblName = new Label();
                    lblName.Text = productName;
                    lblName.TextAlign = ContentAlignment.MiddleCenter;
                    Size textSize = TextRenderer.MeasureText(lblName.Text, f11);
                    lblName.Font = (textSize.Width <= tileW - 10) ? f11 : f9;
                    lblName.Dock = DockStyle.Fill;
                    lblName.Padding = new Padding(3);
                    lblName.ForeColor = Color.Black;
                    lblName.BackColor = Color.Transparent;
                    int nameWidth = TextRenderer.MeasureText(lblName.Text, lblName.Font).Width;
                    //if (nameWidth > (tileW - 20))
                    //{
                    //    tlpMain.RowStyles[0].Height = 50f;
                    //    tlpMain.RowStyles[1].Height = 35f;
                    //    tlpMain.RowStyles[2].Height = 15f;
                    //}
                    //else
                    //{
                    //    tlpMain.RowStyles[0].Height = 60f;
                    //    tlpMain.RowStyles[1].Height = 25f;
                    //    tlpMain.RowStyles[2].Height = 15f;
                    //}

                    Label lblPrice = new Label();
                    lblPrice.Dock = DockStyle.Fill;
                    lblPrice.TextAlign = ContentAlignment.MiddleCenter;
                    lblPrice.ForeColor = Color.Green;
                    lblPrice.BackColor = Color.Transparent;
                    lblPrice.Font = f10;
                    if (discountper > 0)
                    {
                        lblPrice.Paint += (s, l) =>
                        {
                            string oldP = "₹ " + originalPrice.ToString("0.00");
                            string newP = "₹ " + discountPrice.ToString("0.00");

                            SizeF oldSize = l.Graphics.MeasureString(oldP, f10Strike);
                            SizeF newSize = l.Graphics.MeasureString(newP, f10Bold);

                            float totalWidth = oldSize.Width + 10 + newSize.Width;
                            float startX = (lblPrice.Width - totalWidth) / 2;

                            l.Graphics.DrawString(oldP, f10Strike, Brushes.Gray, startX, 4);
                            l.Graphics.DrawString(newP, f10Bold, Brushes.Green, startX + oldSize.Width + 10, 4);
                        };
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
                    picFav.Width = 22;
                    picFav.Height = 22;
                    picFav.BackColor = Color.White;
                    picFav.SizeMode = PictureBoxSizeMode.StretchImage;
                    picFav.Location = new Point(tileW - 30, 8);
                    picFav.Tag = productId;
                    picFav.AccessibleDescription =Convert.ToString(Saletypeno);
                    picFav.Image = (favStatus == 1)
                        ? Properties.Resources.fav_enable
                        : Properties.Resources.fav_disable;
                    picFav.Click += buttonfavClickEventHandler;

                    tile.Controls.Add(picFav);
                    picFav.BringToFront();

                    flp_productview.Controls.Add(tile);
                }
            }
            catch(Exception ex)
            {
                clsgen.LogException("Order page GenearteNormalProduct()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void PicProduct_Click(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            DashboardButtonClickEventHandler(pic.Parent, e);
        }
        public void MakeRounded(Panel panel, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path.AddArc(new Rectangle(panel.Width - radius, 0, radius, radius), 270, 90);
            path.AddArc(new Rectangle(panel.Width - radius, panel.Height - radius, radius, radius), 0, 90);
            path.AddArc(new Rectangle(0, panel.Height - radius, radius, radius), 90, 90);
            path.CloseFigure();
            panel.Region = new Region(path);
        }
        private async Task LoadImageAsync(PictureBox pb, int insType, int productId)
        {
            await Task.Run(() =>
            {
                try
                {
                    Image img = clsDocument.DownloadImageDatabase(insType, productId);

                    if (img != null)
                    {
                        pb.Invoke(new Action(() =>
                        {
                            pb.Image = img;
                        }));
                    }
                }
                catch { }
            });
        }
        //private void SidebuttonClickEventHandler(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Control clickedControl = sender as Control;

        //        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
        //        DmnSearch dmnSearch = new DmnSearch();

        //        // -----------------------------
        //        // FETCH NORMAL MENU PRODUCTS
        //        // -----------------------------
        //        dmnSearch.Select =
        //            "p.Product as Field1,p.ProductId as Field2,0 as Field3,fav_status as Field4 ";
        //        dmnSearch.Table =
        //            "tblProduct p LEFT JOIN tblMenu m on m.ProductId=p.ProductId where p.Status=1 and p.Fcid= " + clickedControl.Tag + " and m.Status=1 " +
        //            " Union Select Itemname as Field1,ItemId as Field2,Saletype as Field3,fav_status as Field4 from tblItem";
        //        dmnSearch.WhereNdGroupBy =
        //            " where Fcid= " + clickedControl.Tag + " and Saletype=1 and Status=1  " +
        //            " and (Select sum(QOH) from tblProductRate where ProductId=ItemId) > 0";

        //        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

        //        // -----------------------------
        //        // CLEAN & PREPARE FLOW LAYOUT
        //        // -----------------------------

        //        flp_productview.Controls.Clear();
        //        flp_productview.AutoScroll = true;
        //        flp_productview.WrapContents = true;
        //        flp_productview.FlowDirection = FlowDirection.LeftToRight;

        //        if (gridSource.Count == 0)
        //            return;

        //        flp_sidemenu.Visible = false;

        //        // -----------------------------
        //        // ADD TILES
        //        // -----------------------------
        //        foreach (var item in gridSource)
        //        {
        //            // MAIN TILE PANEL
        //            Panel tile = new Panel();
        //            tile.Width = 260;
        //            tile.Height = 60;
        //            tile.BackColor = product_buttoncolor;
        //            tile.Margin = new Padding(10, 10, 0, 0);

        //            // PRODUCT BUTTON
        //            Button btn = new Button();
        //            btn.Text = item.Field1;
        //            btn.Name = "btn" + item.Field2;
        //            btn.Tag = item.Field2;
        //            btn.AccessibleDescription = item.Field3;
        //            btn.FlatStyle = FlatStyle.Flat;
        //            btn.FlatAppearance.BorderSize = 0;
        //            btn.ForeColor = Color.White;
        //            btn.BackColor = product_buttoncolor;
        //            btn.Font = AppFonts.GetFont(9);
        //            btn.Width = 200;
        //            btn.Height = 60;
        //            btn.Location = new Point(55, 0);
        //            btn.Click += new EventHandler(DashboardButtonClickEventHandler);

        //            // FAVORITE STAR PICTUREBOX
        //            PictureBox picFav = new PictureBox();
        //            picFav.Name = "picFav" + item.Field2;
        //            picFav.Tag = item.Field2;
        //            picFav.AccessibleDescription = item.Field3;
        //            picFav.Width = 50;
        //            picFav.Height = 45;
        //            picFav.Location = new Point(3, 7);
        //            picFav.SizeMode = PictureBoxSizeMode.StretchImage;

        //            if (Convert.ToInt32(item.Field4) == 1)
        //                picFav.Image = Properties.Resources.yellow_star;
        //            else
        //                picFav.Image = Properties.Resources.white_star;

        //            picFav.Click += new EventHandler(buttonfavClickEventHandler);

        //            // ADD TO TILE
        //            tile.Controls.Add(picFav);
        //            tile.Controls.Add(btn);

        //            // ADD TILE TO FLOW PANEL
        //            flp_productview.Controls.Add(tile);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        clsgen.LogException("Order page SidebuttonClickEventHandler()",
        //            ex, FrmLogin.EmployeName, FrmLogin.Designation,
        //            Convert.ToInt32(FormType.Order));
        //    }
        //}
        #endregion

        #region Favourite Button
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

                if (ImageEquals(pbfav.Image, Properties.Resources.fav_disable))
                {
                    pbfav.Image = Properties.Resources.fav_enable;
                    dmnfav.Field2 = "1";
                }
                else
                {
                    pbfav.Image = Properties.Resources.fav_disable;
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
                clsgen.LogException("Order page  buttonfavClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private bool ImageEquals(Image image1, Image image2)
        {
            try
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
            catch (Exception ex)
            {
                clsgen.LogException("Order page  ImageEquals()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
                return false;
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
                dmnSearch.Select =" p.ItemCode_UPC as Field1, p.Itemname as Field2,p.srate as Field3,p.MRP as Field4,p.ItemId as Field5,p.Tax as Field6,p.TaxType as Field7,p.Fcid as Field8,p.TaxSettingsId as Field9,q.Discountper as Field10";
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

        #region All Button Main Fuction
        private void DashboardButtonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Memoryrelease();
                string Focused_tlpname = "";
                Control clickedControl = sender as Control;
                Global_ProductId = Convert.ToInt32(clickedControl.Tag);
                tlp_seletedItem_main.AutoScroll = true;
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
                        MessageBox.Show("Stock Out! This item is not available.","Out of Stock",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
                                if (Convert.ToInt32(clickedControl.Tag) == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.Product) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value)!=0)
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

        #region Driect Sale Product Button 
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

        #region  Tax & Discount Calculation Fuction 
        public void totalcellamount_exit_product(int cellproductid)
        {
            try
            {
                if (dgv_exitsting_product.Rows[cellproductid].Cells[iitemid].Value != "" && dgv_exitsting_product.Rows[cellproductid].Cells[iprate].Value != "" && dgv_exitsting_product.Rows[cellproductid].Cells[iqty].Value != "")
                {
                    decimal amount = Convert.ToDecimal(dgv_exitsting_product.Rows[cellproductid].Cells[israte].Value) * Convert.ToDecimal(dgv_exitsting_product.Rows[cellproductid].Cells[iqty].Value);
                    dgv_exitsting_product.Rows[cellproductid].Cells[iamount].Value = amount.ToString("N2");

                    //cellDiscountPercentage(cellproductid);
                    if (Convert.ToInt32(dgv_exitsting_product.Rows[cellproductid].Cells[itabletype].Value) != Convert.ToInt32(Tabletype.ComboProduct))
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
                clsgen.LogException("Order page  totalcellamount_exit_product()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void totalcellamount(int cellproductid)
        {
            try
            {
                if (Dgv_Selected_Item.Rows[cellproductid].Cells[iitemid].Value != "" && Dgv_Selected_Item.Rows[cellproductid].Cells[iprate].Value != "" && Dgv_Selected_Item.Rows[cellproductid].Cells[iqty].Value != "" )
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

        #region table Booking
        #endregion

        #region NO Refrenec Code
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  Checkoffer_exist()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
                return false;
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  buttoncomboMouseLeaveEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  buttoncomboMousehoverEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  buttonofferMouseLeaveEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  buttonofferMousehoverEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        #endregion
        private void pb_item_delete_booking_ClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                for (int i = 0; i < Dgv_Selected_Item.RowCount; i++)
                {
                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) == Convert.ToInt32(clickedControl.Tag) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(clickedControl.AccessibleDescription))
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

                        #region Net Amount not taking advance amount

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
                        if ( decimal.TryParse(txtdis_amt.Text, out value))
                        {
                            decimal netAmount = 0;
                            netAmount = (Convert.ToDecimal(txtGrandTotal.Text) ) - (Convert.ToDecimal(txtdis_amt.Text)) + Convert.ToDecimal(txtdiscountamt_promo.Text);
                            txtnetamount.Text = netAmount.ToString("N2");
                            label20.Text = "Net Amount : " + txtnetamount.Text;
                            label21.Text = "Net Amount : " + txtnetamount.Text;
                            decimal balAmnt = netAmount - Convert.ToDecimal(txtpaidamount.Text);
                            txtrecvamount.Text = balAmnt.ToString("N2");
                        }
                        totQty();
                        #endregion

                        Dgv_Selected_Item.Rows[i].Cells[istatusno].Value = 0;
                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  pb_item_delete_booking_ClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void pb_item_deleteClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                string txtname = "", tlpname = "", commonname = "";
                Control clickedControl = sender as Control;
                if (clickedControl.AccessibleDescription == "1")
                {
                    txtname = "txtqty_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                    tlpname = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                    commonname = clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                }
                else if (clickedControl.AccessibleDescription == "2")
                {
                    txtname = "txtqty_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    tlpname = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    commonname = clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                }
                else if (clickedControl.AccessibleDescription == "3")
                {
                    txtname = "txtqty_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                    tlpname = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                    commonname = clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                }
                else if (clickedControl.AccessibleDescription == "4")
                {
                    txtname = "txtqty_" + clickedControl.Tag;
                    tlpname = "tlp_seletedItem_col_" + clickedControl.Tag;
                    commonname = Convert.ToString(clickedControl.Tag);
                }

                // Find the TableLayoutPanel control by name
                TextBox txtqty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                if (txtqty != null)
                {
                    int cur_qty = 0;
                    TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                    if (tlp_sele != null)
                    {
                        if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.DirectSale))
                            CalcTotalAmt_remove_Prid(Convert.ToInt32(clickedControl.Tag), cur_qty);
                        else if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.ComboProduct))
                            CalcTotalAmt_remove_Combo(Convert.ToInt32(clickedControl.Tag), cur_qty);
                        else if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.Product))
                            CalcTotalAmt_remove(Convert.ToInt32(tlp_sele.Tag), cur_qty);
                        else if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.Variation_Addon))
                            CalcTotalAmt_Variation_Addon_remove(Convert.ToString(clickedControl.Tag), cur_qty);
                    }
                    foreach (Control control in tlp_seletedItem_main.Controls)
                    {
                        if (control.Name == tlpname)
                        {
                            #region Dispose Control on product creation list
                            string c1 = "tlp_seletedItem_col" + commonname;
                            string c2 = "buttonitemname_" + commonname;
                            string c3 = "buttonAdd_" + commonname;
                            string c4 = "txtqty_" + commonname;
                            string c5 = "btn_remove_" + commonname;
                            string c6 = "txtprice_" + commonname;
                            string c7 = "btn_remove_" + commonname;

                            TableLayoutPanel tlp_seletedItem_col = this.Controls.Find(c1, true).FirstOrDefault() as TableLayoutPanel;
                            Button buttonitemname = this.Controls.Find(c2, true).FirstOrDefault() as Button;
                            Button buttonAdd = this.Controls.Find(c3, true).FirstOrDefault() as Button;
                            Button buttonRemove = this.Controls.Find(c5, true).FirstOrDefault() as Button;
                            TextBox txtprice = this.Controls.Find(c6, true).FirstOrDefault() as TextBox;
                            Button pb_item_delete = this.Controls.Find(c7, true).FirstOrDefault() as Button;
                            tlp_seletedItem_col?.Dispose();
                            buttonitemname?.Dispose();
                            buttonAdd?.Dispose();
                            txtqty?.Dispose();
                            buttonRemove?.Dispose();
                            txtprice?.Dispose();
                            pb_item_delete?.Dispose();
                            GC.Collect();
                            #endregion

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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  pb_item_delete_booking_ClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void ButtonRemoveClickEventHandler(object sender, EventArgs e)
        {

            try
            {
                string txtname = "", tlpname = "", commonname = "";

                Control clickedControl = sender as Control;
                if (clickedControl.AccessibleDescription == "1")
                {
                    txtname = "txtqty_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                    tlpname = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                    commonname = clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.Product);
                }
                else if (clickedControl.AccessibleDescription == "2")
                {
                    txtname = "txtqty_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    tlpname = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                    commonname = clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.DirectSale);
                }
                else if (clickedControl.AccessibleDescription == "3")
                {
                    txtname = "txtqty_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                    tlpname = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                    commonname = clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                }
                else if (clickedControl.AccessibleDescription == "4")
                {
                    txtname = "txtqty_" + clickedControl.Tag;
                    tlpname = "tlp_seletedItem_col" + "_" + clickedControl.Tag;
                    commonname = Convert.ToString(clickedControl.Tag);
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
                            else if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.ComboProduct))
                                CalcTotalAmt_remove_Combo(Convert.ToInt32(clickedControl.Tag), cur_qty);
                            else if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.Product))
                                CalcTotalAmt_remove(Convert.ToInt32(tlp_sele.Tag), cur_qty);
                            else if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.Variation_Addon))
                                CalcTotalAmt_Variation_Addon_remove(Convert.ToString(tlp_sele.Tag), cur_qty);
                        }

                        foreach (Control control in tlp_seletedItem_main.Controls)
                        {
                            if (control.Name == tlpname)
                            {

                                #region Dispose Control on product creation list
                                string c1 = "tlp_seletedItem_col" + commonname;
                                string c2 = "buttonitemname_" + commonname;
                                string c3 = "buttonAdd_" + commonname;
                                string c4 = "txtqty_" + commonname;
                                string c5 = "btn_remove_" + commonname;
                                string c6 = "txtprice_" + commonname;
                                string c7 = "btn_remove_" + commonname;

                                TableLayoutPanel tlp_seletedItem_col = this.Controls.Find(c1, true).FirstOrDefault() as TableLayoutPanel;
                                Button buttonitemname = this.Controls.Find(c2, true).FirstOrDefault() as Button;
                                Button buttonAdd = this.Controls.Find(c3, true).FirstOrDefault() as Button;
                                Button buttonRemove = this.Controls.Find(c5, true).FirstOrDefault() as Button;
                                TextBox txtprice = this.Controls.Find(c6, true).FirstOrDefault() as TextBox;
                                Button pb_item_delete = this.Controls.Find(c7, true).FirstOrDefault() as Button;
                                tlp_seletedItem_col?.Dispose();
                                buttonitemname?.Dispose();
                                buttonAdd?.Dispose();
                                txtqty?.Dispose();
                                buttonRemove?.Dispose();
                                txtprice?.Dispose();
                                pb_item_delete?.Dispose();
                                GC.Collect();
                                #endregion
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
                            else if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.ComboProduct))
                                CalcTotalAmt_remove_Combo(Convert.ToInt32(clickedControl.Tag), cur_qty);
                            else if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.Product))
                                CalcTotalAmt_remove(Convert.ToInt32(tlp_sele.Tag), cur_qty);
                            else if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.Variation_Addon))
                                CalcTotalAmt_Variation_Addon_remove(Convert.ToString(tlp_sele.Tag), cur_qty);
                    }
                    netAmount();
                    checksave_refresh();
                    GridSlno_Refresh();
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  ButtonRemoveClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void buttonitemnameClickEventHandler(object sender, EventArgs e)
        {

            try
            {
                Control clickedControl = sender as Control;
       

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  buttonitemnameClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                else if (clickedControl.AccessibleDescription == "3")
                {
                    txtname = "txtqty_" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                    tlpname = "tlp_seletedItem_col" + clickedControl.Tag + "_" + Convert.ToInt32(Tabletype.ComboProduct);
                }
                else if (clickedControl.AccessibleDescription == "4")
                {
                    txtname = "txtqty_" + clickedControl.Tag;
                    tlpname = "tlp_seletedItem_col" + "_" + clickedControl.Tag;
                }
                // Find the TableLayoutPanel control by name
                TextBox qty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                if (qty != null)
                {
                    if (tlp_sele != null)
                    {
                        if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.DirectSale))
                            CalcTotalAmt_Add_prid(Convert.ToInt32(clickedControl.Tag), 0);
                        else if (Convert.ToInt32(tlp_sele.AccessibleDescription) == 3)
                        {
                            int cur_qty = Convert.ToInt32(qty.Text) + 1;
                            qty.Text = Convert.ToString(cur_qty);
                            CalcTotalAmt_Add_Combo(Convert.ToInt32(clickedControl.Tag), cur_qty);
                        }
                        else if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.Product))
                        {
                            int cur_qty = Convert.ToInt32(qty.Text) + 1;
                            qty.Text = Convert.ToString(cur_qty);
                            CalcTotalAmt_Add(Convert.ToInt32(tlp_sele.Tag), cur_qty);
                        }
                        else if (Convert.ToInt32(tlp_sele.AccessibleDescription) == Convert.ToInt32(Tabletype.Variation_Addon))
                        {
                            int cur_qty = Convert.ToInt32(qty.Text) + 1;
                            qty.Text = Convert.ToString(cur_qty);
                            CalcTotalAmt_Variation_Addon_Add(Convert.ToString(tlp_sele.Tag), cur_qty);
                        }
                    }
                    netAmount();
                    GridSlno_Refresh();
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  ButtonbuttonAddClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        #region Variation and Variation ADD and remove Calculation Function 
        public void CalcTotalAmt_Variation_Addon_Add(string pid, int qty)
        {
            try
            {
                int rowno = 0;
                if (Booking_Status == false)
                {
                    if (string.IsNullOrEmpty(txtGrandTotal.Text))
                        txtGrandTotal.Text = "0";
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page CalcTotalAmt_Variation_Addon_Add()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void CalcTotalAmt_Variation_Addon_remove(string pid, int qty)
        {
            try
            {

                int rowno = 0;
                if (Booking_Status == false)
                {
                    if (string.IsNullOrEmpty(txtGrandTotal.Text))
                        txtGrandTotal.Text = "0";
                }
                if (qty == 0)
                {
                    string txtqty = "txtqty_" + pid;
                    TextBox totalqty = this.Controls.Find(txtqty, true).FirstOrDefault() as TextBox;
                    int dgvcount = Dgv_Selected_Item.RowCount - 1;
                    for (int i = dgvcount; i >= 0; i--)
                    {
                        if (pid == Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iavid].Value))
                        {
                            if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value) == Convert.ToInt32(Product_Addon_Variation_Type.Variation))
                            {
                                decimal TotalPrice = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value) * Convert.ToDecimal(totalqty.Text);
                                decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) - TotalPrice;
                                txtGrandTotal.Text = Convert.ToString(totalamt);
                                txtnetamount.Text = Convert.ToString(totalamt);
                                Dgv_Selected_Item.Rows.RemoveAt(i);
                            }
                            else
                            {
                                Dgv_Selected_Item.Rows.RemoveAt(i);
                            }
                        }
                    }

                }
                else
                {
                    decimal totalamt = 0;
                    for (int i = 0; i < Dgv_Selected_Item.RowCount; i++)
                    {
                        if (pid == Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iavid].Value))
                        {
                            if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value) == Convert.ToInt32(Product_Addon_Variation_Type.Variation))
                            {
                                rowno = i;
                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) - 1;
                                totalcellamount(i);
                                break;
                            }

                        }

                    }
                    totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[itotal].Value);
                    txtGrandTotal.Text = Convert.ToString(totalamt);
                    txtnetamount.Text = Convert.ToString(totalamt);
                    netAmount();
                }
                checksave_refresh();

            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page CalcTotalAmt_Variation_Addon_remove()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }
        #endregion
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
                        if (prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.DirectSale))
                        {
                            decimal cqoh = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                            if (cqoh > Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value))
                            {
                                CMessageBox.Show("Aviablle Quantity is : " + Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value), "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                            Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  CalcTotalAmt_Add_prid()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  CalcTotalAmt_remove_Prid()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                            if (combid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) == 1 && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.ComboProduct))
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
                clsgen.LogException("Order page  CalcTotalAmt_remove_Combo()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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

                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  CalcTotalAmt_Add_Combo()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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

                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  CalcTotalAmt_Add()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                    qty = qty - 1;
                    if (qty == 0)
                    {
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (pid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) == 1 && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.Product))
                            {
                                decimal TotalPrice = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value) * Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value);
                                decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) - TotalPrice;
                                txtGrandTotal.Text = Convert.ToString(totalamt);
                                txtnetamount.Text = Convert.ToString(totalamt);
                                Dgv_Selected_Item.Rows.RemoveAt(i);
                                Dgv_Selected_Item.Update();
                                int l = Dgv_Selected_Item.RowCount;
                                break;
                            }
                        }
                        checksave_refresh();
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  CalcTotalAmt_remove()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }
        #endregion
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
                    MakeRounded(tile, 18);

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

                    //PictureBox picProduct = new PictureBox();
                    //picProduct.Dock = DockStyle.Fill;
                    //picProduct.BackColor = Color.White;
                    //picProduct.SizeMode = PictureBoxSizeMode.Zoom;

                    //picProduct.Name = "picProduct" + item.Field2;
                    //picProduct.Tag = item.Field2;
                    //picProduct.AccessibleDescription = item.Field3;

                    //picProduct.Image = Properties.Resources.Deafult_food_pic;
                    //int insType = (int)Interfaces.Inseration_Type.Product_Photo;
                    //_ = LoadImageAsync(picProduct, insType, cmphid);

                    //picProduct.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                    //tblMain.Controls.Add(picProduct, 0, 0);

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

                    //if (nameWidth > (tileW - 20))
                    //{
                    //    tblMain.RowStyles[0].Height = 50f;
                    //    tblMain.RowStyles[1].Height = 35f;
                    //    tblMain.RowStyles[2].Height = 15f;
                    //}
                    //else
                    //{
                    //    tblMain.RowStyles[0].Height = 60f;
                    //    tblMain.RowStyles[1].Height = 25f;
                    //    tblMain.RowStyles[2].Height = 15f;
                    //}

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
                        MakeRounded(tile, 18);

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
                        dmnSearch.Select ="p.Product as Field1, p.ProductId as Field2, 0 as Field3, od.Discountper Field4,od.Producttype Field5,p.Price Field6,0 Field7,1 Field8";
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
                    string offerText = item.Field4 + "%";
                    decimal price = Convert.ToDecimal(item.Field3);

                    #region Design Product

                    Panel tile = new Panel();
                    tile.Width = tileW;
                    tile.Height = tileH;
                    tile.BackColor = Color.White;
                    tile.Margin = new Padding(spacing);
                    tile.Tag = productId;
                    tile.BorderStyle = BorderStyle.None;
                    tile.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                    MakeRounded(tile, 18);

                    TableLayoutPanel tlpMain = new TableLayoutPanel();
                    tlpMain.Parent = tile;
                    tlpMain.Dock = DockStyle.Fill;
                    tlpMain.RowCount = 2;
                    tlpMain.ColumnCount = 1;
                    tlpMain.BackColor = Color.White;
                    tlpMain.Margin = new Padding(0);
                   // tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 85));
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));
                    tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
                    tlpMain.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);

                    //PictureBox picProduct = new PictureBox();
                    //picProduct.Dock = DockStyle.Fill;
                    //picProduct.Margin = new Padding(8, 5, 8, 0);
                    //picProduct.SizeMode = PictureBoxSizeMode.Zoom;
                    //picProduct.BackColor = Color.White;

                    //picProduct.Name = "picProduct" + item.Field2;
                    //picProduct.Tag = item.Field2;
                    //picProduct.AccessibleDescription = item.Field3;

                    ////picProduct.Image = Properties.Resources.Deafult_food_pic;
                    ////_ = LoadImageAsync(picProduct, (int)Interfaces.Inseration_Type.Product_Photo, productId);

                    //picProduct.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);

                    // tlpMain.Controls.Add(picProduct, 0, 0);

                    Label lblProductName = new Label();
                    lblProductName.Text = productName;
                    lblProductName.Dock = DockStyle.Fill;
                    lblProductName.TextAlign = ContentAlignment.MiddleCenter;
                    lblProductName.ForeColor = Color.Black;
                    lblProductName.BackColor = Color.Transparent;
                    lblProductName.Padding = new Padding(3);
                    lblProductName.AutoSize = false;
                    lblProductName.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);

                    Size textSize = TextRenderer.MeasureText(lblProductName.Text, AppFonts.GetFont(11));
                    lblProductName.Font = (textSize.Width <= tileW - 10)
                        ? AppFonts.GetFont(11)
                        : AppFonts.GetFont(9);

                    int nameWidth = TextRenderer.MeasureText(lblProductName.Text, lblProductName.Font).Width;

                    //if (nameWidth > tileW - 20)
                    //{
                    //    tlpMain.RowStyles[0].Height = 50f;
                    //    tlpMain.RowStyles[1].Height = 35f;
                    //    tlpMain.RowStyles[2].Height = 15f;
                    //}
                    //else
                    //{
                    //    tlpMain.RowStyles[0].Height = 60f;
                    //    tlpMain.RowStyles[1].Height = 25f;
                    //    tlpMain.RowStyles[2].Height = 15f;
                    //}

                    decimal originalPrice = Convert.ToDecimal(item.Field6);
                    decimal discountPercent = Convert.ToDecimal(item.Field4);
                    decimal discountPrice = originalPrice - (originalPrice * discountPercent / 100);

                    Label lblPrice = new Label();
                    lblPrice.Dock = DockStyle.Fill;
                    lblPrice.AutoSize = false;
                    lblPrice.ForeColor = Color.Green;
                    lblPrice.BackColor = Color.Transparent;
                    lblPrice.TextAlign = ContentAlignment.MiddleCenter;
                    lblPrice.Font = AppFonts.GetFont(10);
                    lblPrice.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                    lblPrice.Paint += (s, l) =>
                    {
                        string oldP = "₹ " + originalPrice.ToString("0.00");
                        string newP = "₹ " + discountPrice.ToString("0.00");

                        Font fOld = new Font(AppFonts.GetFont(10), FontStyle.Strikeout);
                        Font fNew = new Font(AppFonts.GetFont(10), FontStyle.Bold);

                        SizeF oldSize = l.Graphics.MeasureString(oldP, fOld);
                        SizeF newSize = l.Graphics.MeasureString(newP, fNew);

                        float totalWidth = oldSize.Width + 10 + newSize.Width;
                        float startX = (lblPrice.Width - totalWidth) / 2;

                        l.Graphics.DrawString(oldP, f10Strike, Brushes.Gray, startX, 4);
                        l.Graphics.DrawString(newP, f10Bold, Brushes.Green, startX + oldSize.Width + 10, 4);

                    };

                    tlpMain.Controls.Add(lblProductName, 0, 0);
                    tlpMain.Controls.Add(lblPrice, 0, 1);

                    flp_productview.Controls.Add(tile);

                    #endregion

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page GenerateofferProduct()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        
        }

        private async void SpecialOfferSubMenuClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                flp_productview.Controls.Clear();
                flp_productview.AutoScroll = true;
                flp_productview.WrapContents = true;
                flp_productview.FlowDirection = FlowDirection.LeftToRight;

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                //  PRODUCT j=1 & ITEMS j=2
                for (int j = 1; j <= 2; j++)
                {
                    if (j == 1)
                    {
                        dmnSearch.Select =
                            "p.Product as Field1, p.ProductId as Field2, 0 as Field3, " +
                            "ofd.Discountper as Field4, ofd.ProductType as Field5,p.Price Field6,0 Field7,1 Field8";

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
                            "i.Itemname as Field1, i.ItemId as Field2, 1 as Field3, " +
                            "ofd.Discountper as Field4, ofd.ProductType as Field5,(select PRate from tblProductRate where ProductId = i.ItemId and Status=1) Field6,(select count(*) from tblProductRate where ProductId = i.ItemId and Status=1) Field7,2 Field8";

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

                    var queryResult = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    ProductData.AddRange(queryResult);
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
                clsgen.LogException("Order page SpecialOfferSubMenuClickEventHandler()", ex,FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
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
                        dmnSearch.Select ="p.Product as Field1,p.ProductId as Field2,0 as Field3,fav_status as Field4,p.price Field5,q.Discountper Field6,q.FinalPrice Field7";
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

                    MakeRounded(tile, 18);

                    TableLayoutPanel tlpMain = new TableLayoutPanel();
                    tlpMain.Parent = tile;
                    tlpMain.RowCount = 2;
                    tlpMain.ColumnCount = 1;
                    tlpMain.Dock = DockStyle.Fill;
                    tlpMain.AccessibleDescription = Convert.ToString(Saletypeno);
                    tlpMain.Tag = productId;

                   // tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 85));
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));

                    //PictureBox picProduct = new PictureBox();
                    //picProduct.Dock = DockStyle.Fill;
                    //picProduct.SizeMode = PictureBoxSizeMode.Zoom;                 
                    //picProduct.Tag = productId;
                    //picProduct.AccessibleDescription = Convert.ToString(Saletypeno);

                    ////picProduct.Image = Properties.Resources.Deafult_food_pic;
                    ////if (Saletypeno != 1)
                    ////{
                    ////    int insType = (int)Interfaces.Inseration_Type.Product_Photo;
                    ////    _ = LoadImageAsync(picProduct, insType, productId);
                    ////}
                   // picProduct.Click += PicProduct_Click;
                    //tlpMain.Controls.Add(picProduct, 0, 0);

                    Label lblName = new Label();
                    lblName.Text = productName;
                    lblName.TextAlign = ContentAlignment.MiddleCenter;
                    Size textSize = TextRenderer.MeasureText(lblName.Text, f11);
                    lblName.Font = (textSize.Width <= tileW - 10) ? f11 : f9;
                    lblName.Dock = DockStyle.Fill;
                    lblName.Padding = new Padding(3);
                    lblName.ForeColor = Color.Black;
                    lblName.BackColor = Color.Transparent;
                    int nameWidth = TextRenderer.MeasureText(lblName.Text, lblName.Font).Width;
                    //if (nameWidth > (tileW - 20))
                    //{
                    //    tlpMain.RowStyles[0].Height = 50f;
                    //    tlpMain.RowStyles[1].Height = 35f;
                    //    tlpMain.RowStyles[2].Height = 15f;
                    //}
                    //else
                    //{
                    //    tlpMain.RowStyles[0].Height = 60f;
                    //    tlpMain.RowStyles[1].Height = 25f;
                    //    tlpMain.RowStyles[2].Height = 15f;
                    //}   

                    Label lblPrice = new Label();
                    lblPrice.Dock = DockStyle.Fill;
                    lblPrice.TextAlign = ContentAlignment.MiddleCenter;
                    lblPrice.ForeColor = Color.Green;
                    lblPrice.BackColor = Color.Transparent;
                    lblPrice.Font = f10;
                    if (discountper > 0)
                    {
                        lblPrice.Paint += (s, l) =>
                        {
                            string oldP = "₹ " + originalPrice.ToString("0.00");
                            string newP = "₹ " + discountPrice.ToString("0.00");

                            SizeF oldSize = l.Graphics.MeasureString(oldP, f10Strike);
                            SizeF newSize = l.Graphics.MeasureString(newP, f10Bold);

                            float totalWidth = oldSize.Width + 10 + newSize.Width;
                            float startX = (lblPrice.Width - totalWidth) / 2;

                            l.Graphics.DrawString(oldP, f10Strike, Brushes.Gray, startX, 4);
                            l.Graphics.DrawString(newP, f10Bold, Brushes.Green, startX + oldSize.Width + 10, 4);
                        };
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
                    picFav.Tag = item.Field2;
                    picFav.AccessibleDescription = item.Field3;
                    picFav.Width = 22;
                    picFav.Height = 22;
                    picFav.BackColor = Color.White;
                    picFav.SizeMode = PictureBoxSizeMode.StretchImage;
                    picFav.Location = new Point(tileW - 30, 8);
                    picFav.Image = (favStatus == 1) ? Properties.Resources.fav_enable : Properties.Resources.fav_disable;
                    picFav.Click += new EventHandler(buttonfavClickEventHandler);

                    tile.Controls.Add(picFav);
                    picFav.BringToFront();

                    flp_productview.Controls.Add(tile);
                }
            }
            catch(Exception ex)
            {
                clsgen.LogException("Order page GenerateFavouriteProduct()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        #endregion

        #region Place Holder Setting
        private void txtname_search_Enter(object sender, EventArgs e)
        {
            txtitemcode_serach.Text = "Item Code .....";
            txtitemcode_serach.ForeColor = Color.DarkGray;
            if (txtname_search.Text == "Food Name .....")
            {
                txtname_search.Text = "";
                txtname_search.ForeColor = Color.Black;
            }

        }
        private void txtitemcode_serach_Enter(object sender, EventArgs e)
        {
            txtname_search.Text = "Food Name .....";
            txtname_search.ForeColor = Color.DarkGray;
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
        #endregion

        #region Serach Product With Name & Code
        private void txtname_search_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ProductNameSearch();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  txtname_search_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void ProductNameSearch()
        {
            try
            {
                if (txtname_search.Text != null && txtname_search.Text != "")
                {
                    McategoryID = Convert.ToInt32(MenucategryType.ProductNameSerach);

                    #region Size configuration on product

                    ProductData = new List<DmnGridFields>();
                    pagedatadata = new List<DmnGridFields>();
                    currentPage = 1;
                    flp_sidemenu.Visible = false;
                    #endregion

                    #region Combo Product
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch dmnSearch = new DmnSearch();

                    dmnSearch.Select = "OfferName as Field1,Omid as Field2,3 as Field3,Discountper as Field4,FinalPrice Field5,cmphid Field6,1 Field8";
                    dmnSearch.Table = "tblOfferMaster";
                    dmnSearch.WhereNdGroupBy =
                        " where Status=1 AND OfferType=1 AND OfferstartDatetime <= SYSDATETIME() " +
                        " AND OfferEndDatetime >= SYSDATETIME() and OfferName like '%" + txtname_search.Text.Trim() + "%'";

                    var queryResult = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    ProductData.AddRange(queryResult);
                    #endregion

                    #region Offer product and Without offer product
                    dmnSearch.Select =
                        "p.Product as Field1,p.ProductId as Field2,0 as Field3,fav_status as Field4,p.price Field5,ofd.Discountper Field6,ofd.FinalPrice Field7,2 Field8";
                    dmnSearch.Table =
                        "tblProduct p " +
                        "LEFT JOIN tblMenu m on m.ProductId=p.ProductId " +
                        " LEFT JOIN tblOfferMaster ofm ON  ofm.status=1  AND ofm.OfferType=0 AND  ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status=1 LEFT JOIN tblOfferDetails ofd ON ofd.omid=ofm.omid AND ofd.status=1 and p.ProductId=ofd.ProductId" +
                        " where p.Status=1 and m.Status=1 and p.Product like '%" + txtname_search.Text.Trim() + "%'" +
                        " Union Select i.Itemname as Field1,i.ItemId as Field2,i.Saletype as Field3,i.fav_status as Field4,(select SRate from tblProductRate ) Field5,ofm.Discountper Field6,ofd.FinalPrice Field7,2 Field8 from " +
                        " tblItem i " +
                        " LEFT JOIN tblOfferMaster ofm ON  ofm.status=1  AND ofm.OfferType=0 AND  ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status=1 LEFT JOIN tblOfferDetails ofd ON ofd.omid=ofm.omid AND ofd.status=1 ";
                    dmnSearch.WhereNdGroupBy =
                        " where i.Fcid= " + 2 + " and i.Saletype=1 and i.Status=1  " +
                        " and (Select sum(QOH) from tblProductRate where ProductId=i.ItemId) > 0 and i.Itemname like '%" + txtname_search.Text.Trim() + "%'";

                    queryResult = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    ProductData.AddRange(queryResult);
                    #endregion

                    totalPages = (int)Math.Ceiling((double)ProductData.Count / pageSize);
                    pagedatadata = ProductData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                    lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {ProductData.Count}";
                    btnprev.Enabled = currentPage > 1;
                    btnnext.Enabled = currentPage < totalPages;
                    GenerateSerachProduct_Name_code();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  txtname_search_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void txtitemcode_serach_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ProductItemcodeSearch();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  txtitemcode_serach_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void ProductItemcodeSearch()
        {
            try
            {
                if (txtitemcode_serach.Text != null && txtitemcode_serach.Text != "")
                {
                    McategoryID = Convert.ToInt32(MenucategryType.ProductcodeSerach);

                    #region Size configuration on product

                    ProductData = new List<DmnGridFields>();
                    pagedatadata = new List<DmnGridFields>();
                    currentPage = 1;

                    flp_sidemenu.Visible = false;
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch dmnSearch = new DmnSearch();

                    #endregion

                    #region Offer product and Without offer product
                    dmnSearch.Select =
                        "p.Product as Field1,p.ProductId as Field2,0 as Field3,fav_status as Field4,p.price Field5,ofd.Discountper Field6,ofd.FinalPrice Field7,2 Field8";
                    dmnSearch.Table =
                        "tblProduct p " +
                        "LEFT JOIN tblMenu m on m.ProductId=p.ProductId " +
                        " LEFT JOIN tblOfferMaster ofm ON  ofm.status=1  AND ofm.OfferType=0 AND  ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status=1 LEFT JOIN tblOfferDetails ofd ON ofd.omid=ofm.omid AND ofd.status=1 and p.ProductId=ofd.ProductId" +
                        " where p.Status=1 and m.Status=1 and p.ItemCode = '" + txtitemcode_serach.Text.Trim() + "'" +
                        " Union Select i.Itemname as Field1,i.ItemId as Field2,i.Saletype as Field3,i.fav_status as Field4,(select SRate from tblProductRate ) Field5,ofm.Discountper Field6,ofd.FinalPrice Field7,2 Field8 from " +
                        " tblItem i " +
                        " LEFT JOIN tblOfferMaster ofm ON  ofm.status=1  AND ofm.OfferType=0 AND  ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status=1 LEFT JOIN tblOfferDetails ofd ON ofd.omid=ofm.omid AND ofd.status=1 ";
                    dmnSearch.WhereNdGroupBy =
                        " where i.Fcid= " + 2 + " and i.Saletype=1 and i.Status=1  " +
                        " and (Select sum(QOH) from tblProductRate where ProductId=i.ItemId) > 0 and i.ItemCode_UPC = '" + txtitemcode_serach.Text.Trim() + "'";

                    ProductData = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    totalPages = (int)Math.Ceiling((double)ProductData.Count / pageSize);
                    pagedatadata = ProductData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                    lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {ProductData.Count}";
                    btnprev.Enabled = currentPage > 1;
                    btnnext.Enabled = currentPage < totalPages;
                    GenerateSerachProduct_Name_code();

                    #endregion

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  ProductItemcodeSearch()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        public void GenerateSerachProduct_Name_code()
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
                foreach (var item in ProductData)
                {
                    if (Convert.ToInt32(item.Field8) == 2)
                    {
                        string productName = item.Field1;
                        int productId = Convert.ToInt32(item.Field2);
                        decimal price = Convert.ToDecimal(item.Field5);
                        int favStatus = Convert.ToInt32(item.Field4);
                        int Saletypeno = Convert.ToInt32(item.Field3);
                        decimal originalPrice = Convert.ToDecimal(item.Field5);
                        decimal discountPrice = Convert.ToDecimal(item.Field7);
                        decimal discountper = Convert.ToDecimal(item.Field6);

                        #region Design Product (One Panel + One TLP + 3 Rows)

                        Panel tile = new Panel();
                        tile.Width = tileW;
                        tile.Height = tileH;
                        tile.BackColor = Color.White;
                        tile.Margin = new Padding(spacing);
                        tile.Tag = productId;
                        tile.BorderStyle = BorderStyle.None;
                        tile.AccessibleDescription = item.Field3;

                        MakeRounded(tile, 18);

                        TableLayoutPanel tlpMain = new TableLayoutPanel();
                        tlpMain.Parent = tile;
                        tlpMain.RowCount = 2;
                        tlpMain.ColumnCount = 1;
                        tlpMain.Dock = DockStyle.Fill;
                        tlpMain.BackColor = Color.White;
                        tlpMain.AutoSize = false;
                        tlpMain.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                        tlpMain.AccessibleDescription = item.Field3;

                        //tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
                         tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 85f));
                        tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));
                        tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

                        //PictureBox picProduct = new PictureBox();
                        //picProduct.Dock = DockStyle.Fill;
                        //picProduct.Margin = new Padding(8, 5, 8, 0);
                        //picProduct.SizeMode = PictureBoxSizeMode.Zoom;
                        //picProduct.BackColor = Color.White;
                        //picProduct.Name = "picProduct" + item.Field2;
                        //picProduct.Tag = item.Field2;
                        //picProduct.AccessibleDescription = item.Field3;

                        ////picProduct.Image = Properties.Resources.Deafult_food_pic;
                        ////if (Saletypeno != 1)
                        ////{
                        ////    int insType = (int)Interfaces.Inseration_Type.Product_Photo;
                        ////    _ = LoadImageAsync(picProduct, insType, productId);
                        ////}

                        //picProduct.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);

                        //tlpMain.Controls.Add(picProduct, 0, 0);

                        Label lblName = new Label();
                        lblName.Text = productName;
                        lblName.TextAlign = ContentAlignment.MiddleCenter;
                        lblName.AutoSize = false;
                        lblName.Dock = DockStyle.Fill;
                        lblName.Padding = new Padding(3);
                        lblName.ForeColor = Color.Black;
                        lblName.BackColor = Color.Transparent;

                        Size textSize = TextRenderer.MeasureText(lblName.Text, AppFonts.GetFont(11));
                        lblName.Font = (textSize.Width <= tileW - 10)
                            ? AppFonts.GetFont(11)
                            : AppFonts.GetFont(9);

                        int nameWidth = TextRenderer.MeasureText(lblName.Text, lblName.Font).Width;

                        //if (nameWidth > (tileW - 20))
                        //{
                        //    tlpMain.RowStyles[0].Height = 50f;
                        //    tlpMain.RowStyles[1].Height = 35f;
                        //    tlpMain.RowStyles[2].Height = 15f;
                        //}
                        //else
                        //{
                        //    tlpMain.RowStyles[0].Height = 60f;
                        //    tlpMain.RowStyles[1].Height = 25f;
                        //    tlpMain.RowStyles[2].Height = 15f;
                        //}

                        Label lblPrice = new Label();
                        lblPrice.AutoSize = false;
                        lblPrice.Dock = DockStyle.Fill;
                        lblPrice.TextAlign = ContentAlignment.MiddleCenter;
                        lblPrice.Font = AppFonts.GetFont(10);
                        lblPrice.ForeColor = Color.Green;
                        lblPrice.BackColor = Color.Transparent;
                        if (discountper > 0)
                        {
                            lblPrice.Paint += (s, l) =>
                            {
                                string oldP = "₹ " + originalPrice.ToString("0.00");
                                string newP = "₹ " + discountPrice.ToString("0.00");

                                SizeF oldSize = l.Graphics.MeasureString(oldP, f10Strike);
                                SizeF newSize = l.Graphics.MeasureString(newP, f10Bold);

                                float totalWidth = oldSize.Width + 10 + newSize.Width;
                                float startX = (lblPrice.Width - totalWidth) / 2;

                                l.Graphics.DrawString(oldP, f10Strike, Brushes.Gray, startX, 4);
                                l.Graphics.DrawString(newP, f10Bold, Brushes.Green, startX + oldSize.Width + 10, 4);
                            };

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
                        picFav.Tag = item.Field2;
                        picFav.AccessibleDescription = item.Field3;
                        picFav.Width = 22;
                        picFav.Height = 22;
                        picFav.BackColor = Color.White;
                        picFav.SizeMode = PictureBoxSizeMode.StretchImage;
                        picFav.Location = new Point(tileW - 30, 8);

                        picFav.Image = (favStatus == 1)
                            ? Properties.Resources.fav_enable
                            : Properties.Resources.fav_disable;

                        picFav.Click += new EventHandler(buttonfavClickEventHandler);

                        tile.Controls.Add(picFav);
                        picFav.BringToFront();

                        flp_productview.Controls.Add(tile);

                        #endregion
                    }
                    else if (Convert.ToInt32(item.Field8) == 1)
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
                        MakeRounded(tile, 18);

                        TableLayoutPanel tblMain = new TableLayoutPanel();
                        tblMain.Parent = tile;
                        tblMain.Dock = DockStyle.Fill;
                        tblMain.BackColor = Color.Transparent;
                        tblMain.RowCount = 3;
                        tblMain.ColumnCount = 1;
                        tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
                        tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
                        tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));
                        tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

                        //PictureBox picProduct = new PictureBox();
                        //picProduct.Dock = DockStyle.Fill;
                        //picProduct.BackColor = Color.White;
                        //picProduct.SizeMode = PictureBoxSizeMode.Zoom;

                        //picProduct.Name = "picProduct" + item.Field2;
                        //picProduct.Tag = item.Field2;
                        //picProduct.AccessibleDescription = item.Field3;

                        ////picProduct.Image = Properties.Resources.Deafult_food_pic;
                        ////int insType = (int)Interfaces.Inseration_Type.Product_Photo;
                        ////_ = LoadImageAsync(picProduct, insType, cmphid);

                        //picProduct.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                        //tblMain.Controls.Add(picProduct, 0, 0);

                        Label lblProductName = new Label();
                        lblProductName.Text = offerName;
                        lblProductName.ForeColor = Color.Black;
                        lblProductName.BackColor = Color.Transparent;
                        lblProductName.Dock = DockStyle.Fill;
                        lblProductName.TextAlign = ContentAlignment.MiddleCenter;
                        lblProductName.Padding = new Padding(2);
                        lblProductName.MaximumSize = new Size(tileW - 10, 0);

                        Size textSize = TextRenderer.MeasureText(lblProductName.Text, AppFonts.GetFont(11));
                        lblProductName.Font = (textSize.Width <= tileW - 10)
                                                ? AppFonts.GetFont(11)
                                                : AppFonts.GetFont(9);



                        int nameWidth = TextRenderer.MeasureText(lblProductName.Text, lblProductName.Font).Width;

                        ////if (nameWidth > (tileW - 20))
                        ////{
                        ////    tblMain.RowStyles[0].Height = 50f;
                        ////    tblMain.RowStyles[1].Height = 35f;
                        ////    tblMain.RowStyles[2].Height = 15f;
                        ////}
                        ////else
                        ////{
                        ////    tblMain.RowStyles[0].Height = 60f;
                        ////    tblMain.RowStyles[1].Height = 25f;
                        ////    tblMain.RowStyles[2].Height = 15f;
                        ////}

                        Label lblPrice = new Label();
                        lblPrice.Text = "₹ " + Convert.ToDecimal(totalPrice).ToString("0.00");
                        lblPrice.ForeColor = Color.Green;
                        lblPrice.BackColor = Color.Transparent;
                        lblPrice.Font = AppFonts.GetFont(10);
                        lblPrice.Dock = DockStyle.Fill;
                        lblPrice.TextAlign = ContentAlignment.MiddleCenter;
                        lblPrice.AccessibleDescription = item.Field3;

                        tblMain.Controls.Add(lblProductName, 0, 0);
                        tblMain.Controls.Add(lblPrice, 0, 1);

                        tblMain.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                        tile.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                        lblPrice.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                        lblProductName.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);

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

            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  SerachProduct_Name_code()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        #endregion

        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtitemcode_serach, txtname_search, txt_product_des, txtrecvamount, txtGrandTotal, txtpaidamount, txtdis_amt, txtdis_per, txtnetamount, txtentrie_descr, txtdisamt_promo, txtpromocode, txtdiscountamt_promo, txtnumber_split, txtadvance, txtph, txtemailid, txtaddress, txtcusname, txt_split_recamt, txt_split_balamt, txt_cardbalance, txtcardemail, txt_cardno, txt_cardaddress, txt_cardph, txt_cardaccname, txtpkchrge_amt })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

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
                foreach (TextBox textBox in new TextBox[] { txtrecvamount, txtpaidamount, txtnetamount, txtadvance, txtpkchrge_amt })
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
                clsgen.LogException("Order page  ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        #endregion

        #region paint Round corner
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

        #endregion

        #region Product Description

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
        private void btn_close_prod_des_Click(object sender, EventArgs e)
        {
            pnl_main.Enabled = true;
            pnl_products_descr.Visible = false;
        }
        private void btn_save_product_des_Click(object sender, EventArgs e)
        {
            try
            {
                if (saletype == Convert.ToInt32(Tabletype.DirectSale))
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
                else if (saletype == Convert.ToInt32(Tabletype.ComboProduct))
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
                else if (saletype == Convert.ToInt32(Tabletype.Product))//Product Item
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
                else if (saletype == Convert.ToInt32(Tabletype.Variation_Addon))//Product Item
                {
                    for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                    {
                        if (addon_Vari_Id == Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iavid].Value))
                        {
                            // string name = Dgv_Selected_Item.Rows[i].Cells[iname].Value.ToString();
                            if (txt_product_des.Text != "Description Type Here .....")
                                Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value = txt_product_des.Text;
                            break;
                        }
                    }
                }
                pnl_products_descr.Visible = false;
                pnl_main.Enabled = true;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  btn_save_product_des_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }

        #endregion

        #region Net amount and other Calculation
        private void btn_offer_calc_Click(object sender, EventArgs e)
        {
            try
            {
                decimal kdata = 0;
                using (UserControl.Frmnumdiskeypad frm = new UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.Number)))
                {
                    frm.ShowDialog();
                    kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                }
                if (UserControl.Frmnumdiskeypad.flagpercentage)
                {
                    txtdis_per.Text = kdata.ToString("0.##");
                    discountAmnt();
                    netAmount();
                }
                else
                {
                    txtdis_amt.Text = kdata.ToString("0.##");
                    discountPer();
                    netAmount();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_offer_calc_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                if (string.IsNullOrEmpty(txtpkchrge_amt.Text))
                    txtpkchrge_amt.Text = "0";
                #endregion

                decimal value;
                if (decimal.TryParse(txtdis_amt.Text, out value))
                {
                    decimal netAmount = 0;
                    netAmount = (Convert.ToDecimal(txtGrandTotal.Text)) - (Convert.ToDecimal(txtdis_amt.Text) + Convert.ToDecimal(txtdiscountamt_promo.Text));

                    if (Booking_Status && (netAmount > Convert.ToDecimal(txtadvance.Text)))
                        netAmount = netAmount - Convert.ToDecimal(txtadvance.Text);

                    if (Pickup_Status || combined_Pickup_Status)
                        netAmount = netAmount + Convert.ToDecimal(txtpkchrge_amt.Text);

                    if (Refundproductflag)
                        netAmount = netAmount - Refundproductwisetotal;

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
                clsgen.LogException("Order page  netAmount()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void txtpaidamount_TextChanged(object sender, EventArgs e)
        {
            if (txtpaidamount.Text != "." && txtpaidamount.Text != "")
            {
                netAmount();
            }

        }

        #endregion

        #region Entire Description
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
        private void btn_entire_desc_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnl_promocode.Visible == false)
                {
                    UIHelper.CenterAndResize(pnl_order_desc, 30, 25);
                    UIHelper.RoundCorners(pnl_order_desc, 25);
                    UIHelper.RoundCorners(tlp_order_desc_nav, 25);
                    UIHelper.RoundCorners(tlp_order_desc, 25);
                    ApplyRoundedTextBox(txtentrie_descr, 10);
                }
                else
                {
                    pnl_order_desc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_entire_desc_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        #endregion

        #region setting Prefix
        public void setKitchenperfix()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "KitchenSrlNo Field1";
                dmnSearch.Table = " tblHotelRegistration";
                dmnSearch.WhereNdGroupBy = " Where BranchId = " + FrmDashboard.BranchId;
                dmnSearch.OrderBy = "";
                var gridSource1 = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource1.Count > 0)
                {
                    txtkitchenslrno.Text = Convert.ToString(gridSource1[0].Field1);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  setKitchenperfix()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        #endregion

        #region  save & send To kitchen order
        private void btn_saveKOT_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck_KOT())
                {
                    setKitchenperfix();
                    bool flage = false;
                    int GBL_pkid = 0;
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

                    if (BookingType == Convert.ToInt32(DashboardTableType.PickupOrder))
                    {
                        #region Pickup Table Inseration
                        DmnPickup dmnDmnPickup = new DmnPickup();
                        BllPickup bll_Pickup = new BllPickup();
                        dmnDmnPickup.PkId = Pickup_ID;
                        dmnDmnPickup.CusName = Convert.ToString(txtcusname.Text);
                        dmnDmnPickup.Mobile = Convert.ToString(txtph.Text);
                        dmnDmnPickup.Address = Convert.ToString(txtaddress.Text);
                        dmnDmnPickup.EmailId = Convert.ToString(txtemailid.Text);
                        dmnDmnPickup.Remarks = "";
                        dmnDmnPickup.BranchId = FrmDashboard.BranchId;
                        if (rb_male.Checked)
                            dmnDmnPickup.Gender = 1;
                        else
                            dmnDmnPickup.Gender = 2;

                        dmnDmnPickup.TotalQuantity = Convert.ToInt32(txttotalQTY.Text);

                        dmnDmnPickup.DiscountAmt = Convert.ToDecimal(txtdis_amt.Text);
                        dmnDmnPickup.DiscountPer = Convert.ToDecimal(txtdis_per.Text);

                        dmnDmnPickup.NetAmount = Convert.ToDecimal(txtnetamount.Text);
                        dmnDmnPickup.CashRev = Convert.ToDecimal(txtpaidamount.Text);

                        dmnDmnPickup.BalnceAmt = Convert.ToDecimal(txtrecvamount.Text);
                        dmnDmnPickup.RemoveNotes = "";
                        dmnDmnPickup.SalesMasterId = Convert.ToInt32(txtsalemasterid.Text);
                        dmnDmnPickup.Status = 1;
                        dmnDmnPickup.CreatedBy = FrmDashboard.UserId;
                        var InsertStatuspkup = bll_Pickup.AddData("prdInsert_upd_Pickup", dmnDmnPickup);
                        #endregion

                        dmnordermaster.PkId = Convert.ToInt32(InsertStatuspkup.PkId);
                        dmnordermaster.PickupOrderFlag = Convert.ToInt32(PickupOrderOrNot.Pickuporder);
                        GBL_pkid = Convert.ToInt32(InsertStatuspkup.PkId);
                    }
                    else
                        dmnordermaster.PickupOrderFlag = Convert.ToInt32(PickupOrderOrNot.None);

                    dmnordermaster.BranchId = FrmDashboard.BranchId;
                    dmnordermaster.CreatedBy = FrmDashboard.UserId;
                    dmnordermaster.Status = 1;
                    dmnordermaster.Tid = tableId;
                    dmnordermaster.NetAmount = Convert.ToDecimal(txtnetamount.Text);
                    dmnordermaster.Kitchennumber = Convert.ToString(txtkitchenslrno.Text);


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

                                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.ComboProduct))
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
                                    dmnorderdetail.Kitchennumber = Convert.ToString(txtkitchenslrno.Text);
                                    dmnorderdetail.VariatiionName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value);
                                    dmnorderdetail.vid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ividno].Value);
                                    dmnorderdetail.PkId = 0;
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
                                if (Dgv_Selected_Item.Rows[i].Cells[iqty].Value != null && Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) > 0 && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[isubofferstatus].Value) == Convert.ToInt32(SubofferStatus.None) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                                {
                                    dmnorderdetail.OrderMasterId = Convert.ToInt32(InsertStatusP.OrderMasterId);
                                    dmnorderdetail.ItemBarCode = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ibarcode].Value);

                                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.ComboProduct))
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

                                    dmnorderdetail.Pavid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipavidno].Value);
                                    dmnorderdetail.AddonStatus = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value);
                                    dmnorderdetail.AV_id = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iavid].Value);
                                    dmnorderdetail.Addoncount = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iAddoncountno].Value);
                                    dmnorderdetail.VariatiionName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value);

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
                                    dmnorderdetail.Kitchennumber = Convert.ToString(txtkitchenslrno.Text);
                                    dmnorderdetail.vid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ividno].Value);
                                    dmnorderdetail.CCode = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iccodeno].Value);
                                    dmnorderdetail.PkId = GBL_pkid;
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
                        clsgen.Memoryrelease();

                        if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                            ResetTableViewPage();
                        else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                            ResetPickupViewPage();
                        CMessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearAll();
                        this.Close();
                        this.Dispose();
                    }

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_saveKOT_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                    ResetFlagvalue = Convert.ToInt32(gridSource[0].Field1) + 1;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  ResetFlag()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }
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
        public bool MandatoryCheck_KOT()
        {
            try
            {
                if (string.IsNullOrEmpty(txtordermasterid.Text))
                    txtordermasterid.Text = "0";
                if (string.IsNullOrEmpty(txttid.Text))
                    txttid.Text = "0";
                if (string.IsNullOrEmpty(txtdis_per.Text))
                    txtdis_per.Text = "0";
                if (string.IsNullOrEmpty(txtsalemasterid.Text))
                    txtsalemasterid.Text = "0";
                if (string.IsNullOrEmpty(txttotalQTY.Text))
                    txttotalQTY.Text = "0";
                if (string.IsNullOrEmpty(txtpaidamount.Text))
                    txtpaidamount.Text = "0";
                if (Convert.ToInt32(txttotalQTY.Text) == 0)
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
            catch (Exception ex)
            {
                clsgen.LogException("Order page  MandatoryCheck_KOT()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
                return false;
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
                    if (Convert.ToInt32(gridSourcemaster[i].Quantity) != null && Convert.ToInt32(gridSourcemaster[i].Quantity) > 0 && Convert.ToInt32(gridSourcemaster[i].Status) == 3)
                        sum = sum + Convert.ToInt32(gridSourcemaster[i].Quantity);
                }
                finalqty = Convert.ToInt32(txttotalQTY.Text) + sum;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  finaltotalqty()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                clsgen.LogException("Order page  FinalTax()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }
        public void ChangeLayoutStyle(TableLayoutPanel tlp, int mode)
        {
            tlp.SuspendLayout();

            if (mode == 1) // ---- PERCENTAGE MODE ----
            {
                // Calculate % width/height
                float colPercent = tlp.ColumnCount > 0 ? 100f / tlp.ColumnCount : 0;
                float rowPercent = tlp.RowCount > 0 ? 100f / tlp.RowCount : 0;

                // Columns -> Percent
                for (int i = 0; i < tlp.ColumnStyles.Count; i++)
                {
                    tlp.ColumnStyles[i].SizeType = SizeType.Percent;
                    tlp.ColumnStyles[i].Width = colPercent;
                }

                // Rows -> Percent
                for (int i = 0; i < tlp.RowStyles.Count; i++)
                {
                    tlp.RowStyles[i].SizeType = SizeType.Percent;
                    tlp.RowStyles[i].Height = rowPercent;
                }
            }
            else if (mode == 2) // ---- AUTOSIZE MODE ----
            {
                // Columns -> AutoSize
                for (int i = 0; i < tlp.ColumnStyles.Count; i++)
                {
                    tlp.ColumnStyles[i].SizeType = SizeType.AutoSize;
                }

                // Rows -> AutoSize
                for (int i = 0; i < tlp.RowStyles.Count; i++)
                {
                    tlp.RowStyles[i].SizeType = SizeType.AutoSize;
                }
            }

            tlp.ResumeLayout();
        }

        private void btn_savekot_print_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck_KOT())
                {
                    BillCopy_Status = false;
                    KOT_saleprint_status = true;
                    Screen screen = Screen.FromControl(this);
                    int screenWidth = screen.Bounds.Width;
                    int screenHeight = screen.Bounds.Height;
                    ChangeLayoutStyle(tlp_payment_option, 1);
                    int adjustedWidth = 0, adjustedHeight = 0;
                    if (combined_Status)
                    {
                        var controlToRemove = tlp_payment_option.GetControlFromPosition(2, 0);
                        tlp_payment_option.Controls.Remove(controlToRemove);
                        controlToRemove = tlp_payment_option.GetControlFromPosition(3, 0);
                        tlp_payment_option.Controls.Remove(controlToRemove);
                        controlToRemove = tlp_payment_option.GetControlFromPosition(2, 1);
                        tlp_payment_option.Controls.Remove(controlToRemove);
                        controlToRemove = tlp_payment_option.GetControlFromPosition(3, 1);
                        tlp_payment_option.Controls.Remove(controlToRemove);

                        tlp_payment_option.Controls.Add(btncreditcard, 2, 0);
                        tlp_payment_option.Controls.Add(btnnetbanking, 2, 1);
                        btncreditcard.Visible = true;
                        btnnetbanking.Visible = true;
                        btn_splitpayment.Visible = false;
                        Btnpartpayment.Visible = false;
                        btncreditcard.Dock = DockStyle.Fill;
                        btnnetbanking.Dock = DockStyle.Fill;
                        ChangeLayoutStyle(tlp_payment_option, 1);
                        UIHelper.CenterAndResize(tlp_payment_option, 40, 45);
                        UIHelper.RoundCorners(tlp_payment_option, 25);
                        pnl_main.Enabled = false;
                    }
                    else
                    {
                        //CMessageBox.Show(screenWidth + "" + adjustedWidth + " " + adjustedHeight, "size", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        var controlToRemove = tlp_payment_option.GetControlFromPosition(2, 0);
                        tlp_payment_option.Controls.Remove(controlToRemove);
                        controlToRemove = tlp_payment_option.GetControlFromPosition(3, 0);
                        tlp_payment_option.Controls.Remove(controlToRemove);
                        controlToRemove = tlp_payment_option.GetControlFromPosition(2, 1);
                        tlp_payment_option.Controls.Remove(controlToRemove);
                        controlToRemove = tlp_payment_option.GetControlFromPosition(3, 1);
                        tlp_payment_option.Controls.Remove(controlToRemove);

                        tlp_payment_option.Controls.Add(btncreditcard, 2, 0);
                        tlp_payment_option.Controls.Add(btnnetbanking, 3, 0);
                        tlp_payment_option.Controls.Add(Btnpartpayment, 2, 1);
                        tlp_payment_option.Controls.Add(btn_splitpayment, 3, 1);

                        btncreditcard.Visible = true;
                        btnnetbanking.Visible = true;
                        btn_splitpayment.Visible = true;
                        Btnpartpayment.Visible = true;

                        btncreditcard.Dock = DockStyle.Fill;
                        btnnetbanking.Dock = DockStyle.Fill;
                        btn_splitpayment.Dock = DockStyle.Fill;
                        Btnpartpayment.Dock = DockStyle.Fill;

                        UIHelper.CenterAndResize(tlp_payment_option, 40, 45);
                        UIHelper.RoundCorners(tlp_payment_option, 25);

                        pnl_main.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_savekot_print_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void paybillcal(int payid)
        {
            try
            {
                if (MandatoryCheck_printbill())
                {
                    setperfixsale();
                    int countflag = 0, Biltypeno;

                    DmnSalemaster dmnsalemaster = new DmnSalemaster();
                    DmnSaledetails dmnsaledetails = new DmnSaledetails();
                    BllSale bllSales = new BllSale();

                    if (string.IsNullOrEmpty(txtdis_per.Text))
                        txtdis_per.Text = "0";
                    if (string.IsNullOrEmpty(txtdiscountamt_promo.Text))
                        txtdiscountamt_promo.Text = "0";
                    if (string.IsNullOrEmpty(lblorderid.Text))
                        lblorderid.Text = "0";
                    if (string.IsNullOrEmpty(txt_cardbalance.Text))
                        txt_cardbalance.Text = "0";
                    if (string.IsNullOrEmpty(txtsalemasterid.Text))
                        txtsalemasterid.Text = "0";
                    if (string.IsNullOrEmpty(lblorderid.Text))
                        lblorderid.Text = "0";

                    if (DirectPayBill) 
                    {
                        #region Normal Save Salemaster And Saledetails

                        finaltotalqty();

                        dmnsalemaster.EntryNo = txtinvoiceno.Text;
                        dmnsalemaster.TotQty = Convert.ToDecimal(finalqty);
                        dmnsalemaster.TotalAmnt = Convert.ToDecimal(txtGrandTotal.Text);
                        dmnsalemaster.GrandTot = Convert.ToDecimal(txtGrandTotal.Text);
                        dmnsalemaster.DisPer = Convert.ToDecimal(txtdis_per.Text);
                        dmnsalemaster.DisAmnt = Convert.ToDecimal(txtdis_amt.Text);
                        if (RFID_Status)
                        {
                            dmnsalemaster.Name = Convert.ToString(txt_cardaccname.Text);
                            dmnsalemaster.ContactNo = Convert.ToString(txt_cardph.Text);
                            dmnsalemaster.Email = Convert.ToString(txtcardemail.Text);
                            dmnsalemaster.Address = Convert.ToString(txt_cardaddress.Text);
                            dmnsalemaster.LedgerId = Convert.ToInt32(txt_LedgerId.Text);

                            if (lbl_cardgender.Text == "Male")
                                dmnsalemaster.Gender = 1;
                            else if (lbl_cardgender.Text == "Female")
                                dmnsalemaster.Gender = 2;
                            else if (lbl_cardgender.Text == "Others")
                                dmnsalemaster.Gender = 3;
                        }
                        else
                        {
                            dmnsalemaster.Name = Convert.ToString(txtcusname.Text);
                            dmnsalemaster.ContactNo = Convert.ToString(txtph.Text);
                            dmnsalemaster.Email = Convert.ToString(txtemailid.Text);
                            dmnsalemaster.Address = Convert.ToString(txtaddress.Text);
                            if (rb_male.Checked)
                                dmnsalemaster.Gender = 1;
                            else
                                dmnsalemaster.Gender = 2;
                        }

                        dmnsalemaster.NetAmount = Convert.ToDecimal(txtnetamount.Text);
                        dmnsalemaster.RcvAmnt = Convert.ToDecimal(txtpaidamount.Text);
                        dmnsalemaster.BalAmnt = Convert.ToDecimal(txtrecvamount.Text);
                        dmnsalemaster.OrderMasterid = Convert.ToInt32(lblorderid.Text);
                        dmnsalemaster.PromoAmount = Convert.ToDecimal(txtdiscountamt_promo.Text);
                        dmnsalemaster.Tid = tableId;
                        dmnsalemaster.PayId = payid;

                        if (BillCopy_Status)
                        {
                            Biltypeno = Convert.ToInt32(BillType.BillCopy);
                            dmnsalemaster.BillCopyStatus = 1;
                        }
                        else
                        {
                            Biltypeno = Convert.ToInt32(BillType.NormalBill);
                            dmnsalemaster.BillCopyStatus = 0;
                        }

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
                        dmnsalemaster.SalesMasterId = Convert.ToInt32(txtsalemasterid.Text);
                        dmnsalemaster.CreatedBy = FrmDashboard.UserId;
                        dmnsalemaster.Status = 1;
                        dmnsalemaster.CombinedOrderBill_TYPE = 0;
                        dmnsalemaster.BookmasterId = Bookingmasterid;

                        if (Booking_Status)
                            dmnsalemaster.Rev_Status = 1;
                        else
                            dmnsalemaster.Rev_Status = 0;

                        dmnsalemaster.AdvanceAmount = Convert.ToDecimal(txtadvance.Text);
                        dmnsalemaster.ReservationTotal = Convert.ToDecimal(txtreservationtotal.Text);
                        FinalTax();
                        //dmnsalemaster.TotalDiscount = Convert.ToDecimal(txtDiscAmnt.Text);
                        dmnsalemaster.TotalTax = TotalTax;
                        dmnsalemaster.TotalIGST = TotalIGST;
                        dmnsalemaster.TotalCGST = TotalCGST;
                        dmnsalemaster.TotalSGST = TotalSGST;
                        dmnsalemaster.TotalVAT = TotalVAT;
                        dmnsalemaster.mDeviceIP = " [ " + System.Environment.MachineName + " ]  -" + Dns.GetHostByName(System.Environment.MachineName).AddressList[0].ToString();
                        dmnsalemaster.mDevice = clsgen.FetchMacId();

                        var duration = DateTime.Now - OrderDateTime;
                        int min = duration.Seconds;
                        dmnsalemaster.TimeDuration = min;

                        dmnsalemaster.ICardNo = reading_RFID;
                        dmnsalemaster.CBalance = Convert.ToDecimal(txt_cardbalance.Text);
                        dmnsalemaster.TapType = Convert.ToInt32(TapType.FandBPayment);
                        if (DirectPayBill)
                            dmnsalemaster.OrderType = Convert.ToInt32(OrderType.Pickup_DirectSale);
                        dmnsalemaster.PkId = 0;
                        dmnsalemaster.PkChargeAmount = Convert.ToDecimal(txtpkchrge_amt.Text);
                        dmnsalemaster.IsDirectSale =true;
                        var InsertStatusP = bllSales.AddEditData("prdInsert_Upd_SalesMaster", dmnsalemaster);

                        #region details
                        if (InsertStatusP.SalesMasterId != 0)
                        {
                            if (payid == Convert.ToInt32(PaymentType.PART))
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

                            for (int i = 0; i < Dgv_Selected_Item.Rows.Count; i++)
                                {
                                if (Dgv_Selected_Item.Rows[i].Cells[iqty].Value != null && Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) > 0 && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[isubofferstatus].Value) == Convert.ToInt32(SubofferStatus.None) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                                {
                                    dmnsaledetails.SalesRate = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[israte].Value);
                                    dmnsaledetails.ItemBarcode = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ibarcode].Value);
                                    txtsalemasterid.Text = Convert.ToString(InsertStatusP.SalesMasterId);

                                    dmnsaledetails.SalesMasterId = Convert.ToInt32(InsertStatusP.SalesMasterId);

                                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.ComboProduct))
                                        dmnsaledetails.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iname].Value);
                                    else
                                        dmnsaledetails.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iname].Value);

                                    dmnsaledetails.UnitId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iunitid].Value);
                                    dmnsaledetails.Quantity = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value);

                                    dmnsaledetails.DiscountPer = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[idisper].Value);
                                    dmnsaledetails.DiscountAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[idisamt].Value);

                                    dmnsaledetails.TaxPer = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itaxper].Value);
                                    dmnsaledetails.TaxAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itaxamt].Value);

                                    dmnsaledetails.Amount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iamount].Value);

                                    if ((Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value)) == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                                        dmnsaledetails.NetAmount = 0;
                                    else
                                        dmnsaledetails.NetAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);

                                    dmnsaledetails.itemid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value);

                                    dmnsaledetails.Prid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value);      
                                    dmnsaledetails.MRP = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[imrp].Value);
                                    dmnsaledetails.ProductType = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value);

                                    dmnsaledetails.Tax = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itax].Value);
                                    dmnsaledetails.TaxType = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itaxtype].Value);
                                    dmnsaledetails.IGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value);
                                    dmnsaledetails.CGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value) / 2;
                                    dmnsaledetails.SGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value) / 2;
                                    dmnsaledetails.VatAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ivatamtno].Value);

                                    dmnsaledetails.MarketPrice = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value);
                                    dmnsaledetails.TableType = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value);
                                    dmnsaledetails.BranchId = FrmDashboard.BranchId;
                                    dmnsaledetails.CreatedBy = FrmDashboard.UserId;
                                    dmnsaledetails.Status = 1;

                                    dmnsaledetails.Pavid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipavidno].Value);
                                    dmnsaledetails.AddonStatus = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value);
                                    dmnsaledetails.AV_id = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iavid].Value);
                                    dmnsaledetails.Addoncount = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iAddoncountno].Value);
                                    dmnsaledetails.VariatiionName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value);

                                    dmnsaledetails.OrderDetailsId ="0";
                                    dmnsaledetails.SalesDetailsId = 0;

                                    if (dmnsaledetails.TableType == Convert.ToInt32(Tabletype.Product) && (dmnsaledetails.AddonStatus == 1 || dmnsaledetails.AddonStatus == 2))
                                        dmnsaledetails.Cmid = Convert.ToString(dmnsaledetails.AV_id);
                                    else if (dmnsaledetails.TableType == Convert.ToInt32(Tabletype.Product) && dmnsaledetails.AddonStatus == 0)
                                        dmnsaledetails.Cmid = Convert.ToString(dmnsaledetails.itemid);
                                    else if (dmnsaledetails.TableType == Convert.ToInt32(Tabletype.DirectSale))
                                        dmnsaledetails.Cmid = Convert.ToString(dmnsaledetails.Prid);
                                    else if (dmnsaledetails.TableType == Convert.ToInt32(Tabletype.ComboProduct))
                                        dmnsaledetails.Cmid = Convert.ToString(dmnsaledetails.itemid);
                                    else
                                        dmnsaledetails.Cmid = "0";

                                    var InsertStatus = bllSales.AddEditDataDet("prdInsert_Upd_SalesDetails", dmnsaledetails);
                                    if (InsertStatus == null)
                                    {
                                        MessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        break;
                                    }
                                    else
                                        countflag = countflag + 1;

                                    int saledetailsid = InsertStatus.SalesDetailsId;
                                    //int odid = Convert.ToInt32(gridSourcemaster[i].OrderDetailsId);
                                    //var existingOrder = orderDetailsInfoList.FirstOrDefault(row => row.OrderDetailsId == odid);
                                    //if (existingOrder != null)
                                    //{
                                    //    int index = orderDetailsInfoList.IndexOf(existingOrder);
                                    //    orderDetailsInfoList[index].saleDetailsId = saledetailsid;
                                    //}
                                }
                                }

                            if (payid == 0)
                            {
                                FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(InsertStatusP.SalesMasterId, payid, 0, Biltypeno);
                                frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                                frmSalesInvoice.ShowDialog();
                                CMessageBox.Show("Billing Printing Completed Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                                    ResetTableViewPage();
                                else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                                    ResetPickupViewPage();

                                ResetBillpage();
                                this.Close();
                            }
                            else if (payid != 12)
                            {
                                FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(InsertStatusP.SalesMasterId, payid, 0, Biltypeno);
                                frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                                frmSalesInvoice.ShowDialog();
                                CMessageBox.Show("Transaction Completed Sucessfully", "Sucessfull", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                                    ResetTableViewPage();
                                else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                                    ResetPickupViewPage();
                                ResetBillpage();

                                //ClearAll();
                                this.Close();
                            }
                            #endregion

                        }
                        #endregion
                    }
                    else
                    {
                        if (checkedtableNames.Count > 0)
                        {
                            #region Combined  Save Salemaster And Saledetails

                            #region sale Master 
                            int sum = 0;
                            int n = dgv_combined.RowCount;
                            for (int i = 0; i < n; i++)
                            {
                                if (Convert.ToInt32(dgv_combined.Rows[i].Cells[cqty].Value) != null && Convert.ToInt32(dgv_combined.Rows[i].Cells[cqty].Value) > 0)
                                    sum = sum + Convert.ToInt32(dgv_combined.Rows[i].Cells[cqty].Value);
                            }
                            if (string.IsNullOrEmpty(txttotalQTY.Text))
                                txttotalQTY.Text = "0";
                            finalqty = Convert.ToInt32(txttotalQTY.Text) + sum;

                            dmnsalemaster.EntryNo = txtinvoiceno.Text;

                            if (RFID_Status)
                            {
                                dmnsalemaster.CombineStatus = 1;
                                dmnsalemaster.Name = Convert.ToString(txt_cardaccname.Text);
                                dmnsalemaster.ContactNo = Convert.ToString(txt_cardph.Text);
                                dmnsalemaster.Email = Convert.ToString(txtcardemail.Text);
                                dmnsalemaster.Address = Convert.ToString(txt_cardaddress.Text);
                                dmnsalemaster.LedgerId = Convert.ToInt32(txt_LedgerId.Text);
                                if (lbl_cardgender.Text == "Male")
                                    dmnsalemaster.Gender = 1;
                                else if (lbl_cardgender.Text == "Female")
                                    dmnsalemaster.Gender = 2;
                                else if (lbl_cardgender.Text == "Others")
                                    dmnsalemaster.Gender = 3;
                            }
                            else
                            {
                                dmnsalemaster.Name = Convert.ToString(txtcusname.Text);
                                dmnsalemaster.ContactNo = Convert.ToString(txtph.Text);
                                dmnsalemaster.Email = Convert.ToString(txtemailid.Text);
                                dmnsalemaster.Address = Convert.ToString(txtaddress.Text);
                                if (rb_male.Checked)
                                    dmnsalemaster.Gender = 1;
                                else
                                    dmnsalemaster.Gender = 2;
                            }

                            dmnsalemaster.TotQty = Convert.ToDecimal(finalqty);
                            dmnsalemaster.TotalAmnt = Convert.ToDecimal(txtGrandTotal.Text);
                            dmnsalemaster.GrandTot = Convert.ToDecimal(txtGrandTotal.Text);
                            dmnsalemaster.DisPer = Convert.ToDecimal(txtdis_per.Text);
                            dmnsalemaster.DisAmnt = Convert.ToDecimal(txtdis_amt.Text);
                            dmnsalemaster.NetAmount = Convert.ToDecimal(txtnetamount.Text);
                            dmnsalemaster.RcvAmnt = Convert.ToDecimal(txtpaidamount.Text);
                            dmnsalemaster.BalAmnt = Convert.ToDecimal(txtrecvamount.Text);
                            dmnsalemaster.OrderMasterid = Convert.ToInt32(0);//lblorderid.Text
                            dmnsalemaster.PromoAmount = Convert.ToDecimal(txtdiscountamt_promo.Text);
                            dmnsalemaster.AdvanceAmount = Convert.ToDecimal(txtadvance.Text);
                            dmnsalemaster.ReservationTotal = Convert.ToDecimal(txtreservationtotal.Text);
                            dmnsalemaster.Tid = tableId;
                            dmnsalemaster.PayId = payid;

                            if (BillCopy_Status)
                            {
                                Biltypeno = Convert.ToInt32(BillType.BillCopy);
                                dmnsalemaster.BillCopyStatus = 1;
                            }
                            else
                            {
                                Biltypeno = Convert.ToInt32(BillType.NormalBill);
                                dmnsalemaster.BillCopyStatus = 0;
                            }

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
                                if (Convert.ToInt32(dgv_combined.Rows[i].Cells[cigstamt].Value) > 0)
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
                            dmnsalemaster.mDevice = clsgen.FetchMacId();
                            //dmnsalemaster.Remark = txtRemark.Text; 
                            dmnsalemaster.BranchId = FrmDashboard.BranchId;
                            dmnsalemaster.SalesMasterId = Convert.ToInt32(txtsalemasterid.Text);
                            dmnsalemaster.CreatedBy = FrmDashboard.UserId;
                            dmnsalemaster.Status = 1;
                            dmnsalemaster.CombinedOrderBill_TYPE = 1;

                            if (Booking_Status)
                                dmnsalemaster.Rev_Status = 1;
                            else
                                dmnsalemaster.Rev_Status = 0;

                            dmnsalemaster.ICardNo = reading_RFID;
                            dmnsalemaster.CBalance = Convert.ToDecimal(txt_cardbalance.Text);
                            dmnsalemaster.TapType = Convert.ToInt32(TapType.FandBPayment);
                            dmnsalemaster.PkChargeAmount = Convert.ToDecimal(txtpkchrge_amt.Text);

                            if (Booking_Status || OrderRunning_Status)
                                dmnsalemaster.OrderType = Convert.ToInt32(OrderType.Booking_NormalOrder);
                            else if (Pickup_Status)
                                dmnsalemaster.OrderType = Convert.ToInt32(OrderType.PickupOrder);
                            else if (combined_Pickup_Status)
                                dmnsalemaster.OrderType = Convert.ToInt32(OrderType.Combined_pick_normal_Order);

                            var InsertStatusP = bllSales.AddEditData("prdInsert_Upd_SalesMaster", dmnsalemaster);
                            #endregion

                            if (InsertStatusP.SalesMasterId != 0)
                            {
                                #region Part Payment save DB
                                if (payid == Convert.ToInt32(PaymentType.PART))
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
                                #endregion

                                if (dgv_combined.RowCount > 0)
                                {
                                    #region CombinedOrderBill Table info save DB
                                    for (int k = 0; k < checkedtableNames.Count; k++)
                                    {
                                        DmnCombinedOrderBill CombinedOrderBill = new DmnCombinedOrderBill();
                                        BllCombinedOrderBill bllCombinedOrderBill = new BllCombinedOrderBill();
                                        CombinedOrderBill.Cbid = Convert.ToInt32(checkedtableNames[k].Cbid);
                                        CombinedOrderBill.SalemasterId = Convert.ToInt32(InsertStatusP.SalesMasterId);
                                        CombinedOrderBill.OrderMasterID = Convert.ToInt32(checkedtableNames[k].Ordermasterid);

                                        if (Convert.ToInt32(checkedtableNames[k].pkid) > 0)
                                        {
                                            CombinedOrderBill.PkId = Convert.ToInt32(checkedtableNames[k].pkid);
                                            CombinedOrderBill.PickupOrderFlag = 1;
                                        }
                                        else
                                            CombinedOrderBill.Tid = Convert.ToInt32(checkedtableNames[k].Id);

                                        CombinedOrderBill.CreatedBy = FrmDashboard.UserId;
                                        CombinedOrderBill.BranchId = FrmDashboard.BranchId;
                                        CombinedOrderBill.Status = 1;

                                        if (BillCopy_Status)
                                            CombinedOrderBill.BillCopyStatus = 1;
                                        else
                                            CombinedOrderBill.BillCopyStatus = 0;

                                        var duration = DateTime.Now - OrderDateTime;
                                        int min = duration.Seconds;
                                        CombinedOrderBill.TimeDuration = min;
                                        var InsertStatus_cbill = bllCombinedOrderBill.AddCombinedOrderBill("prdInsert_CombinedOrderBill", CombinedOrderBill);
                                    }
                                    #endregion

                                    #region Sale details 
                                    for (int i = 0; i < dgv_combined.RowCount; i++)
                                    {
                                        if (Convert.ToInt32(dgv_combined.Rows[i].Cells[Cstatusno].Value) == 3)//Convert.ToInt32(dgv_combined.Rows[i].Cells[citemid].Value) > 0 && (Convert.ToInt32(dgv_combined.Rows[i].Cells[CAddonStatus].Value) == 0 || Convert.ToInt32(dgv_combined.Rows[i].Cells[CAddonStatus].Value) == 1)
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
                                            dmnsaledetails.CreatedBy = FrmDashboard.UserId;
                                            dmnsaledetails.Status = 1;
                                            dmnsaledetails.OrderDetailsId = Convert.ToString(dgv_combined.Rows[i].Cells[COrderDetailsIdno].Value);
                                            dmnsaledetails.SalesDetailsId = Convert.ToInt32(dgv_combined.Rows[i].Cells[CSalesDetailsIdno].Value);

                                            dmnsaledetails.Pavid = Convert.ToInt32(dgv_combined.Rows[i].Cells[CPavid].Value);
                                            dmnsaledetails.AddonStatus = Convert.ToInt32(dgv_combined.Rows[i].Cells[CAddonStatus].Value);
                                            dmnsaledetails.AV_id = Convert.ToString(dgv_combined.Rows[i].Cells[CAV_id].Value);
                                            dmnsaledetails.Addoncount = Convert.ToInt32(dgv_combined.Rows[i].Cells[CAddoncount].Value);
                                            dmnsaledetails.VariatiionName = Convert.ToString(dgv_combined.Rows[i].Cells[CVariatiionNameno].Value);

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
                                    #endregion

                                }
                                if (dgv_combined.RowCount == countflag)
                                {
                                    if (payid != 12)
                                    {
                                        FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(InsertStatusP.SalesMasterId, payid, 0, Biltypeno);
                                        frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                                        frmSalesInvoice.ShowDialog();
                                        CMessageBox.Show("Transaction Completed Sucessfully", "Sucessfull", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                                            ResetTableViewPage();
                                        else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                                            ResetPickupViewPage();

                                        ResetBillpage();
                                        //ClearAll();
                                        this.Close();
                                    }
                                }

                            }

                            #endregion
                        }
                        else
                        {
                            #region Pickup order Order and print bill case
                            bool reorderpickupflag = false;
                            if (Convert.ToInt32(txtsalemasterid.Text) > 0)
                                reorderpickupflag = true;

                            int GBL_pkid = 0, GBL_omid = 0;
                            if (KOT_saleprint_status)
                            {
                                if (MandatoryCheck_KOT())
                                {
                                    setKitchenperfix();
                                    bool flage = false;
                                    
                                    DmnOrderMaster dmnordermaster = new DmnOrderMaster();
                                    DmnOrderDetails dmnorderdetail = new DmnOrderDetails();
                                    BllOrder bll_order = new BllOrder();
                                    dmnordermaster.BookmasterId = Bookingmasterid;
                                    dmnordermaster.OrderMasterId = Convert.ToInt32(txtordermasterid.Text);
                                    dmnordermaster.EntryNo = "";
                                    int sumtemp = 0; decimal nettotatemp = 0;
                                    int ntemp = Dgv_Selected_Item.RowCount;
                                    if (Convert.ToInt32(txtsalemasterid.Text) > 0)
                                    {
                                        for (int i = 0; i < ntemp; i++)
                                        {
                                            if (Dgv_Selected_Item.Rows[i].Cells[iqty].Value != "")
                                            {
                                                if ((Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iMainProductStatusno].Value) == Convert.ToInt32(MainProductStatus.Primary)))
                                                {
                                                    sumtemp = sumtemp + Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value);
                                                    nettotatemp = nettotatemp + Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);
                                                }
                                                   
                                            }

                                        }
                                        dmnordermaster.TotalQuantity = sumtemp;
                                    }                                        
                                    else
                                        dmnordermaster.TotalQuantity = Convert.ToInt32(txttotalQTY.Text);

                                    dmnordermaster.CustomerName = Convert.ToString(txtcusname.Text);
                                    dmnordermaster.Telephone = Convert.ToString(txtph.Text);
                                    dmnordermaster.Email = Convert.ToString(txtemailid.Text);
                                    dmnordermaster.Address = Convert.ToString(txtaddress.Text);
                                    if (rb_male.Checked)
                                        dmnordermaster.Gender = 1;
                                    else
                                        dmnordermaster.Gender = 2;

                                    if (BookingType == Convert.ToInt32(DashboardTableType.PickupOrder))
                                    {
                                        #region Pickup Table Inseration
                                        DmnPickup dmnDmnPickup = new DmnPickup();
                                        BllPickup bll_Pickup = new BllPickup();
                                        dmnDmnPickup.PkId = Pickup_ID;
                                        dmnDmnPickup.CusName = Convert.ToString(txtcusname.Text);
                                        dmnDmnPickup.Mobile = Convert.ToString(txtph.Text);
                                        dmnDmnPickup.Address = Convert.ToString(txtaddress.Text);
                                        dmnDmnPickup.EmailId = Convert.ToString(txtemailid.Text);
                                        dmnDmnPickup.Remarks = "";
                                        dmnDmnPickup.BranchId = FrmDashboard.BranchId;
                                        if (rb_male.Checked)
                                            dmnDmnPickup.Gender = 1;
                                        else
                                            dmnDmnPickup.Gender = 2;
        
                                        if (Convert.ToInt32(txtsalemasterid.Text) > 0)                                       
                                            dmnDmnPickup.TotalQuantity = sumtemp;                                        
                                        else
                                            dmnDmnPickup.TotalQuantity = Convert.ToInt32(txttotalQTY.Text);

                                        dmnDmnPickup.DiscountAmt = Convert.ToDecimal(txtdis_amt.Text);
                                        dmnDmnPickup.DiscountPer = Convert.ToDecimal(txtdis_per.Text);

                                        if (Convert.ToInt32(txtsalemasterid.Text) > 0)                                        
                                            dmnDmnPickup.NetAmount = nettotatemp;                                        
                                        else
                                            dmnDmnPickup.NetAmount = nettotatemp;

                                        dmnDmnPickup.CashRev = Convert.ToDecimal(txtpaidamount.Text);

                                        dmnDmnPickup.BalnceAmt = Convert.ToDecimal(txtrecvamount.Text);
                                        dmnDmnPickup.RemoveNotes = "";
                                        dmnDmnPickup.SalesMasterId = Convert.ToInt32(txtsalemasterid.Text);
                                        dmnDmnPickup.Status = 1;
                                        dmnDmnPickup.CreatedBy = FrmDashboard.UserId;
                                        var InsertStatuspkup = bll_Pickup.AddData("prdInsert_upd_Pickup", dmnDmnPickup);
                                        #endregion

                                        dmnordermaster.PkId = Convert.ToInt32(InsertStatuspkup.PkId);
                                        dmnordermaster.PickupOrderFlag = Convert.ToInt32(PickupOrderOrNot.Pickuporder);
                                        GBL_pkid = Convert.ToInt32(InsertStatuspkup.PkId);
                                    }
                                    else
                                        dmnordermaster.PickupOrderFlag = Convert.ToInt32(PickupOrderOrNot.None);

                                    dmnordermaster.BranchId = FrmDashboard.BranchId;
                                    dmnordermaster.CreatedBy = FrmDashboard.UserId;
                                    dmnordermaster.Status = 1;
                                    dmnordermaster.Tid = tableId;

                                    if (Convert.ToInt32(txtsalemasterid.Text) > 0)
                                        dmnordermaster.NetAmount = nettotatemp;
                                    else
                                        dmnordermaster.NetAmount = Convert.ToDecimal(txtnetamount.Text);

                                    if (Convert.ToInt32(txtsalemasterid.Text) > 0)
                                        dmnordermaster.Kitchennumber = Convert.ToString(Refundkitchenslno);
                                    else
                                        dmnordermaster.Kitchennumber = Convert.ToString(txtkitchenslrno.Text);                                   

                                    var InsertStatusor = bll_order.AddEditData("prdInsert_Upd_OrderMaster", dmnordermaster);
                                    if (InsertStatusor.OrderMasterId > 0)
                                    {
                                        GBL_omid = InsertStatusor.OrderMasterId;
                                        ResetFlag(Convert.ToInt32(txtordermasterid.Text));

                                        for (int i = 0; i < Dgv_Selected_Item.Rows.Count; i++)
                                        {
                                            if (Booking_Status)
                                            {
                                                if (Dgv_Selected_Item.Rows[i].Cells[iitemid].Value != null && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) > 0)
                                                {
                                                    dmnorderdetail.OrderMasterId = Convert.ToInt32(InsertStatusor.OrderMasterId);
                                                    dmnorderdetail.ItemBarCode = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ibarcode].Value);

                                                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.ComboProduct))
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

                                                    dmnorderdetail.NetAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);
                                                    dmnorderdetail.PrId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value);
                                                    dmnorderdetail.Notes = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value);
                                                    dmnorderdetail.OrderNo = ResetFlagvalue;
                                                    dmnorderdetail.BookingFlag = 1;
                                                    dmnorderdetail.KOTPrintStatus = 1;
                                                    dmnorderdetail.Tid = Convert.ToInt32(txttid.Text);
                                                    dmnorderdetail.Kitchennumber = Convert.ToString(txtkitchenslrno.Text);                                                    
                                                    dmnorderdetail.VariatiionName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value);
                                                    dmnorderdetail.vid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ividno].Value);
                                                    dmnorderdetail.PkId = GBL_pkid;
                                                    var InsertStatus = bll_order.AddEditDataDet("prdInsert_Upd_OrderDetails", dmnorderdetail);
         
                                                    if (InsertStatus == null)
                                                    {
                                                        flage = true;
                                                        CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        break;
                                                    }
                                                    else
                                                        Dgv_Selected_Item.Rows[i].Cells[iorderdetailsidno].Value = InsertStatus.OrderDetailsId;

                                                }
                                            }
                                            else
                                            {
                                                if (Dgv_Selected_Item.Rows[i].Cells[iqty].Value != null && Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) > 0 && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[isubofferstatus].Value) == Convert.ToInt32(SubofferStatus.None) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                                                {
                                                    dmnorderdetail.OrderMasterId = Convert.ToInt32(InsertStatusor.OrderMasterId);
                                                    dmnorderdetail.ItemBarCode = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ibarcode].Value);

                                                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.ComboProduct))
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

                                                    dmnorderdetail.Pavid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipavidno].Value);
                                                    dmnorderdetail.AddonStatus = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value);
                                                    dmnorderdetail.AV_id = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iavid].Value);
                                                    dmnorderdetail.Addoncount = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iAddoncountno].Value);
                                                    dmnorderdetail.VariatiionName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value);

                                                    if (txtentrie_descr.Text == "Description Type here .....")
                                                        dmnorderdetail.MainRemarks = "";
                                                    else
                                                        dmnorderdetail.MainRemarks = txtentrie_descr.Text;

                                                    dmnorderdetail.BranchId = FrmDashboard.BranchId;
                                                    dmnorderdetail.CreatedBy = FrmDashboard.UserId;
                                                    dmnorderdetail.Status = 1;

                                                    dmnorderdetail.NetAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);
                                                    dmnorderdetail.PrId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value);
                                                    dmnorderdetail.Notes = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value);
                                                    dmnorderdetail.KOTPrintStatus = 1;
                                                    dmnorderdetail.BookingFlag = 0;

                                                    if (Convert.ToInt32(txtsalemasterid.Text) > 0)
                                                        dmnorderdetail.OrderNo = Refundorderno;
                                                    else
                                                        dmnorderdetail.OrderNo = ResetFlagvalue;

                                                    dmnorderdetail.Tid = Convert.ToInt32(txttid.Text);

                                                    if (Convert.ToInt32(txtsalemasterid.Text) > 0)
                                                        dmnorderdetail.Kitchennumber = Convert.ToString(Refundkitchenslno);
                                                    else
                                                        dmnorderdetail.Kitchennumber = Convert.ToString(txtkitchenslrno.Text);
      
                                                    dmnorderdetail.vid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ividno].Value);
                                                    dmnorderdetail.CCode = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iccodeno].Value);
                                                    dmnorderdetail.PkId = GBL_pkid;
                                                    var InsertStatus = bll_order.AddEditDataDet("prdInsert_Upd_OrderDetails", dmnorderdetail);
                                                    if (InsertStatus == null)
                                                    {
                                                        flage = true;
                                                        CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Dgv_Selected_Item.Rows[i].Cells[iorderdetailsidno].Value = InsertStatus.OrderDetailsId;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Normal Save Salemaster And Saledetails
                            finaltotalqty();

                            int sum = 0; decimal nettotal = 0;
                            int n = Dgv_Selected_Item.RowCount;
                            if (Convert.ToInt32(txtsalemasterid.Text) > 0)
                            {
                                for (int i = 0; i < n; i++)
                                {
                                    if (Dgv_Selected_Item.Rows[i].Cells[iqty].Value != "")
                                    {
                                        if ((Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iMainProductStatusno].Value) == Convert.ToInt32(MainProductStatus.Primary)))
                                        {
                                            sum = sum + Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value);
                                            nettotal = nettotal + Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);
                                        }

                                    }

                                }
                            }
                            dmnsalemaster.EntryNo = txtinvoiceno.Text;

                            if (Convert.ToInt32(txtsalemasterid.Text) > 0)
                            {
                                dmnsalemaster.TotQty = sum;
                                dmnsalemaster.TotalAmnt = nettotal;
                                dmnsalemaster.GrandTot = nettotal;
                            }
                            else
                            {
                                dmnsalemaster.TotQty = Convert.ToDecimal(finalqty);
                                dmnsalemaster.TotalAmnt = Convert.ToDecimal(txtGrandTotal.Text);
                                dmnsalemaster.GrandTot = Convert.ToDecimal(txtGrandTotal.Text);
                            }

                            dmnsalemaster.DisPer = Convert.ToDecimal(txtdis_per.Text);
                            dmnsalemaster.DisAmnt = Convert.ToDecimal(txtdis_amt.Text);
                            if (RFID_Status)
                            {
                                dmnsalemaster.Name = Convert.ToString(txt_cardaccname.Text);
                                dmnsalemaster.ContactNo = Convert.ToString(txt_cardph.Text);
                                dmnsalemaster.Email = Convert.ToString(txtcardemail.Text);
                                dmnsalemaster.Address = Convert.ToString(txt_cardaddress.Text);
                                dmnsalemaster.LedgerId = Convert.ToInt32(txt_LedgerId.Text);

                                if (lbl_cardgender.Text == "Male")
                                    dmnsalemaster.Gender = 1;
                                else if (lbl_cardgender.Text == "Female")
                                    dmnsalemaster.Gender = 2;
                                else if (lbl_cardgender.Text == "Others")
                                    dmnsalemaster.Gender = 3;
                            }
                            else
                            {
                                dmnsalemaster.Name = Convert.ToString(txtcusname.Text);
                                dmnsalemaster.ContactNo = Convert.ToString(txtph.Text);
                                dmnsalemaster.Email = Convert.ToString(txtemailid.Text);
                                dmnsalemaster.Address = Convert.ToString(txtaddress.Text);
                                if (rb_male.Checked)
                                    dmnsalemaster.Gender = 1;
                                else
                                    dmnsalemaster.Gender = 2;
                            }
                            if (Convert.ToInt32(txtsalemasterid.Text) > 0)
                            {
                                dmnsalemaster.NetAmount = nettotal;
                            }
                            else
                            {
                                dmnsalemaster.NetAmount = Convert.ToDecimal(txtnetamount.Text);
                            }
                
                            dmnsalemaster.RcvAmnt = Convert.ToDecimal(txtpaidamount.Text);
                            dmnsalemaster.BalAmnt = Convert.ToDecimal(txtrecvamount.Text);

                            if (KOT_saleprint_status)
                                dmnsalemaster.OrderMasterid = GBL_omid;
                            else
                                dmnsalemaster.OrderMasterid = Convert.ToInt32(lblorderid.Text);

                            dmnsalemaster.PromoAmount = Convert.ToDecimal(txtdiscountamt_promo.Text);
                            dmnsalemaster.Tid = tableId;
                            dmnsalemaster.PayId = payid;

                            if (BillCopy_Status)
                            {
                                Biltypeno = Convert.ToInt32(BillType.BillCopy);
                                dmnsalemaster.BillCopyStatus = 1;
                            }
                            else
                            {
                                Biltypeno = Convert.ToInt32(BillType.NormalBill);
                                dmnsalemaster.BillCopyStatus = 0;
                            }

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
                            dmnsalemaster.SalesMasterId = Convert.ToInt32(txtsalemasterid.Text);
                            dmnsalemaster.CreatedBy = FrmDashboard.UserId;
                            dmnsalemaster.Status = 1;
                            dmnsalemaster.CombinedOrderBill_TYPE = 0;
                            dmnsalemaster.BookmasterId = Bookingmasterid;

                            if (Booking_Status)
                                dmnsalemaster.Rev_Status = 1;
                            else
                                dmnsalemaster.Rev_Status = 0;

                            dmnsalemaster.AdvanceAmount = Convert.ToDecimal(txtadvance.Text);
                            dmnsalemaster.ReservationTotal = Convert.ToDecimal(txtreservationtotal.Text);
                            FinalTax();
                            //dmnsalemaster.TotalDiscount = Convert.ToDecimal(txtDiscAmnt.Text);
                            dmnsalemaster.TotalTax = TotalTax;
                            dmnsalemaster.TotalIGST = TotalIGST;
                            dmnsalemaster.TotalCGST = TotalCGST;
                            dmnsalemaster.TotalSGST = TotalSGST;
                            dmnsalemaster.TotalVAT = TotalVAT;
                            dmnsalemaster.mDeviceIP = " [ " + System.Environment.MachineName + " ]  -" + Dns.GetHostByName(System.Environment.MachineName).AddressList[0].ToString();
                            dmnsalemaster.mDevice = clsgen.FetchMacId();

                            var duration = DateTime.Now - OrderDateTime;
                            int min = duration.Seconds;
                            dmnsalemaster.TimeDuration = min;

                            dmnsalemaster.ICardNo = reading_RFID;
                            dmnsalemaster.CBalance = Convert.ToDecimal(txt_cardbalance.Text);
                            dmnsalemaster.TapType = Convert.ToInt32(TapType.FandBPayment);

                            if (Booking_Status || OrderRunning_Status)
                                dmnsalemaster.OrderType = Convert.ToInt32(OrderType.Booking_NormalOrder);
                            else if (Pickup_Status)
                                dmnsalemaster.OrderType = Convert.ToInt32(OrderType.PickupOrder);
                            else if (combined_Pickup_Status)
                                dmnsalemaster.OrderType = Convert.ToInt32(OrderType.Combined_pick_normal_Order);

                            if (KOT_saleprint_status)
                                dmnsalemaster.PkId = GBL_pkid;
                            else
                                dmnsalemaster.PkId = gridSourcemaster[0].PkId;

                            dmnsalemaster.PkChargeAmount = Convert.ToDecimal(txtpkchrge_amt.Text);

                            var InsertStatusP = bllSales.AddEditData("prdInsert_Upd_SalesMaster", dmnsalemaster);
                            //if (reorderpickupflag)
                            //{
                                #region details Reorder Pickup
                                if (InsertStatusP.SalesMasterId != 0)
                                {
                                    if (payid == Convert.ToInt32(PaymentType.PART))
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

                                    for (int i = 0; i < Dgv_Selected_Item.Rows.Count; i++)
                                    {
                                        if (Dgv_Selected_Item.Rows[i].Cells[iqty].Value != null && Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) > 0 && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[isubofferstatus].Value) == Convert.ToInt32(SubofferStatus.None) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                                        {
                                            dmnsaledetails.SalesRate = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[israte].Value);
                                            dmnsaledetails.ItemBarcode = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ibarcode].Value);
                                            txtsalemasterid.Text = Convert.ToString(InsertStatusP.SalesMasterId);

                                            dmnsaledetails.SalesMasterId = Convert.ToInt32(InsertStatusP.SalesMasterId);

                                            if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.ComboProduct))
                                                dmnsaledetails.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iname].Value);
                                            else
                                                dmnsaledetails.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iname].Value);

                                            dmnsaledetails.UnitId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iunitid].Value);
                                            dmnsaledetails.Quantity = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value);

                                            dmnsaledetails.DiscountPer = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[idisper].Value);
                                            dmnsaledetails.DiscountAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[idisamt].Value);

                                            dmnsaledetails.TaxPer = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itaxper].Value);
                                            dmnsaledetails.TaxAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itaxamt].Value);

                                            dmnsaledetails.Amount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iamount].Value);

                                            if ((Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value)) == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                                                dmnsaledetails.NetAmount = 0;
                                            else
                                                dmnsaledetails.NetAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);

                                            dmnsaledetails.itemid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value);

                                            dmnsaledetails.Prid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value);
                                            dmnsaledetails.MRP = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[imrp].Value);
                                            dmnsaledetails.ProductType = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value);

                                            dmnsaledetails.Tax = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itax].Value);
                                            dmnsaledetails.TaxType = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itaxtype].Value);
                                            dmnsaledetails.IGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value);
                                            dmnsaledetails.CGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value) / 2;
                                            dmnsaledetails.SGSTAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value) / 2;
                                            dmnsaledetails.VatAmount = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ivatamtno].Value);

                                            dmnsaledetails.MarketPrice = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value);
                                            dmnsaledetails.TableType = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value);
                                            dmnsaledetails.BranchId = FrmDashboard.BranchId;
                                            dmnsaledetails.CreatedBy = FrmDashboard.UserId;
                                            dmnsaledetails.Status = 1;

                                            dmnsaledetails.Pavid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipavidno].Value);
                                            dmnsaledetails.AddonStatus = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value);
                                            dmnsaledetails.AV_id = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iavid].Value);
                                            dmnsaledetails.Addoncount = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iAddoncountno].Value);
                                            dmnsaledetails.VariatiionName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value);

                                            dmnsaledetails.OrderDetailsId = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iorderdetailsidno].Value);
                                            dmnsaledetails.SalesDetailsId = 0;

                                            if (dmnsaledetails.TableType == Convert.ToInt32(Tabletype.Product) && (dmnsaledetails.AddonStatus == 1 || dmnsaledetails.AddonStatus == 2))
                                                dmnsaledetails.Cmid = Convert.ToString(dmnsaledetails.AV_id);
                                            else if (dmnsaledetails.TableType == Convert.ToInt32(Tabletype.Product) && dmnsaledetails.AddonStatus == 0)
                                                dmnsaledetails.Cmid = Convert.ToString(dmnsaledetails.itemid);
                                            else if (dmnsaledetails.TableType == Convert.ToInt32(Tabletype.DirectSale))
                                                dmnsaledetails.Cmid = Convert.ToString(dmnsaledetails.Prid);
                                            else if (dmnsaledetails.TableType == Convert.ToInt32(Tabletype.ComboProduct))
                                                dmnsaledetails.Cmid = Convert.ToString(dmnsaledetails.itemid);
                                            else
                                                dmnsaledetails.Cmid = "0";

                                            var InsertStatus = bllSales.AddEditDataDet("prdInsert_Upd_SalesDetails", dmnsaledetails);
                                            if (InsertStatus == null)
                                            {
                                                MessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                break;
                                            }
                                            else
                                                countflag = countflag + 1;

                                            int saledetailsid = InsertStatus.SalesDetailsId;
                                            //int odid = Convert.ToInt32(gridSourcemaster[i].OrderDetailsId);
                                            //var existingOrder = orderDetailsInfoList.FirstOrDefault(row => row.OrderDetailsId == odid);
                                            //if (existingOrder != null)
                                            //{
                                            //    int index = orderDetailsInfoList.IndexOf(existingOrder);
                                            //    orderDetailsInfoList[index].saleDetailsId = saledetailsid;
                                            //}
                                        }
                                    }

                                    if (payid == 0)
                                    {
                                    using (FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(InsertStatusP.SalesMasterId, payid, 0, Biltypeno))
                                    {
                                        frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                                        frmSalesInvoice.ShowDialog();
                                    }
                                        CMessageBox.Show("Billing Printing Completed Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                                            ResetTableViewPage();
                                        else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                                            ResetPickupViewPage();

                                        ResetBillpage();
                                        this.Close();
                                    }
                                    else if (payid != 12)
                                    {
                                    using (FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(InsertStatusP.SalesMasterId, payid, 0, Biltypeno))
                                    {
                                        frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                                        frmSalesInvoice.ShowDialog();
                                    }
                                        //CMessageBox.Show("Transaction Completed Sucessfully", "Sucessfull", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                                            ResetTableViewPage();
                                        else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                                            ResetPickupViewPage();
                                        ResetBillpage();

                                        //ClearAll();
                                        this.Close();
                                    }
                                   
                                }
                                #endregion
                            //}
                            //else
                            //{

                            //    #region Sale details Normal
                            //    if (InsertStatusP.SalesMasterId != 0)
                            //    {
                            //        if (payid == Convert.ToInt32(PaymentType.PART))
                            //        {
                            //            DmnPayment dmnpayment = new DmnPayment();
                            //            Bllpayment bllpay = new Bllpayment();
                            //            for (int i = 0; i < dgv_part.RowCount; i++)
                            //            {
                            //                dmnpayment.PayId = 0;
                            //                dmnpayment.OrderMasterID = Convert.ToInt32(label18.Text);
                            //                dmnpayment.SalemasterId = Convert.ToInt32(InsertStatusP.SalesMasterId);
                            //                dmnpayment.PayType = Convert.ToInt32(dgv_part.Rows[i].Cells[4].Value);
                            //                dmnpayment.Amount = Convert.ToDecimal(dgv_part.Rows[i].Cells[1].Value);
                            //                dmnpayment.Tid = tableId;
                            //                dmnpayment.BranchId = FrmDashboard.BranchId;
                            //                dmnpayment.CreatedBy = FrmDashboard.UserId;
                            //                dmnpayment.Status = 1;
                            //                var InsertStatus = bllpay.AddEditPayment("prdInsert_Upd_Payment", dmnpayment);
                            //            }
                            //        }
                            //        if (gridSourcemaster.Count > 0)
                            //        {
                            //            for (int i = 0; i < gridSourcemaster.Count; i++)
                            //            {
                            //                if (Convert.ToInt32(gridSourcemaster[i].Status) == 3)
                            //                {
                            //                    dmnsaledetails.SalesRate = Convert.ToDecimal(gridSourcemaster[i].Srate);
                            //                    dmnsaledetails.ItemBarcode = Convert.ToString(gridSourcemaster[i].ItemBarCode);
                            //                    txtsalemasterid.Text = Convert.ToString(InsertStatusP.SalesMasterId);
                            //                    dmnsaledetails.SalesMasterId = Convert.ToInt32(InsertStatusP.SalesMasterId);
                            //                    dmnsaledetails.ItemName = Convert.ToString(gridSourcemaster[i].ItemName);
                            //                    dmnsaledetails.UnitId = Convert.ToInt32(gridSourcemaster[i].UnitId);
                            //                    dmnsaledetails.Quantity = Convert.ToInt32(gridSourcemaster[i].Quantity);
                            //                    dmnsaledetails.DiscountPer = Convert.ToDecimal(gridSourcemaster[i].DiscountPer);
                            //                    dmnsaledetails.DiscountAmount = Convert.ToDecimal(gridSourcemaster[i].DiscountAmount);
                            //                    dmnsaledetails.TaxPer = Convert.ToDecimal(gridSourcemaster[i].TaxPer);
                            //                    dmnsaledetails.TaxAmount = Convert.ToDecimal(gridSourcemaster[i].TaxAmount);
                            //                    dmnsaledetails.Amount = Convert.ToDecimal(gridSourcemaster[i].Amount);

                            //                    if (Convert.ToInt32(gridSourcemaster[i].AddonStatus) == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                            //                        dmnsaledetails.NetAmount = 0;
                            //                    else
                            //                        dmnsaledetails.NetAmount = Convert.ToDecimal(gridSourcemaster[i].NetAmount);

                            //                    dmnsaledetails.itemid = Convert.ToInt32(gridSourcemaster[i].ItemId);
                            //                    dmnsaledetails.Prid = Convert.ToInt32(gridSourcemaster[i].PrId);
                            //                    dmnsaledetails.Tax = Convert.ToString(gridSourcemaster[i].Tax);
                            //                    dmnsaledetails.TaxType = Convert.ToString(gridSourcemaster[i].TaxType);
                            //                    dmnsaledetails.MRP = Convert.ToDecimal(gridSourcemaster[i].MRP);
                            //                    dmnsaledetails.ProductType = Convert.ToString(gridSourcemaster[i].TableType);

                            //                    dmnsaledetails.IGSTAmount = Convert.ToDecimal(gridSourcemaster[i].IGSTAmount);
                            //                    dmnsaledetails.CGSTAmount = Convert.ToDecimal(gridSourcemaster[i].CGSTAmount);
                            //                    dmnsaledetails.SGSTAmount = Convert.ToDecimal(gridSourcemaster[i].SGSTAmount);
                            //                    dmnsaledetails.VatAmount = Convert.ToDecimal(gridSourcemaster[i].VATAmount);

                            //                    dmnsaledetails.MarketPrice = Convert.ToDecimal(gridSourcemaster[i].MarketPrice);
                            //                    dmnsaledetails.TableType = Convert.ToInt32(gridSourcemaster[i].TableType);
                            //                    dmnsaledetails.BranchId = FrmDashboard.BranchId;
                            //                    dmnsaledetails.CreatedBy = FrmDashboard.UserId;
                            //                    dmnsaledetails.Status = 1;

                            //                    dmnsaledetails.Pavid = Convert.ToInt32(gridSourcemaster[i].Pavid);
                            //                    dmnsaledetails.AddonStatus = Convert.ToInt32(gridSourcemaster[i].AddonStatus);
                            //                    dmnsaledetails.AV_id = Convert.ToString(gridSourcemaster[i].AV_id);
                            //                    dmnsaledetails.Addoncount = Convert.ToInt32(gridSourcemaster[i].Addoncount);
                            //                    dmnsaledetails.VariatiionName = Convert.ToString(gridSourcemaster[i].VariatiionName);

                            //                    dmnsaledetails.OrderDetailsId = Convert.ToString(gridSourcemaster[i].OrderDetailsId);

                            //                    dmnsaledetails.SalesDetailsId = Convert.ToInt32(gridSourcemaster[i].SalesDetailsId);

                            //                    if (dmnsaledetails.TableType == Convert.ToInt32(Tabletype.Product) && (dmnsaledetails.AddonStatus == 1 || dmnsaledetails.AddonStatus == 2))
                            //                        dmnsaledetails.Cmid = Convert.ToString(dmnsaledetails.AV_id);
                            //                    else if (dmnsaledetails.TableType == Convert.ToInt32(Tabletype.Product) && dmnsaledetails.AddonStatus == 0)
                            //                        dmnsaledetails.Cmid = Convert.ToString(dmnsaledetails.itemid);
                            //                    else if (dmnsaledetails.TableType == Convert.ToInt32(Tabletype.DirectSale))
                            //                        dmnsaledetails.Cmid = Convert.ToString(dmnsaledetails.Prid);
                            //                    else if (dmnsaledetails.TableType == Convert.ToInt32(Tabletype.ComboProduct))
                            //                        dmnsaledetails.Cmid = Convert.ToString(dmnsaledetails.itemid);
                            //                    else
                            //                        dmnsaledetails.Cmid = "0";

                            //                    var InsertStatus = bllSales.AddEditDataDet("prdInsert_Upd_SalesDetails", dmnsaledetails);
                            //                    if (InsertStatus == null)
                            //                    {
                            //                        MessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            //                        break;
                            //                    }
                            //                    else
                            //                        countflag = countflag + 1;

                            //                    int saledetailsid = InsertStatus.SalesDetailsId;
                            //                    int odid = Convert.ToInt32(gridSourcemaster[i].OrderDetailsId);
                            //                    var existingOrder = orderDetailsInfoList.FirstOrDefault(row => row.OrderDetailsId == odid);
                            //                    if (existingOrder != null)
                            //                    {
                            //                        int index = orderDetailsInfoList.IndexOf(existingOrder);
                            //                        orderDetailsInfoList[index].saleDetailsId = saledetailsid;
                            //                    }
                            //                }
                            //            }

                            //        }
                            //        if (gridSourcemaster.Count == countflag)
                            //        {
                            //            if (payid == 0)
                            //            {
                            //                FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(InsertStatusP.SalesMasterId, payid, 0, Biltypeno);
                            //                frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                            //                frmSalesInvoice.ShowDialog();
                            //                CMessageBox.Show("Billing Printing Completed Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //                if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                            //                    ResetTableViewPage();
                            //                else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                            //                    ResetPickupViewPage();
                            //                ResetBillpage();
                            //                this.Close();
                            //            }
                            //            else if (payid != 12)
                            //            {
                            //                FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(InsertStatusP.SalesMasterId, payid, 0, Biltypeno);
                            //                frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                            //                frmSalesInvoice.ShowDialog();
                            //                CMessageBox.Show("Transaction Completed Sucessfully", "Sucessfull", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //                if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                            //                    ResetTableViewPage();
                            //                else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                            //                    ResetPickupViewPage();
                            //                ResetBillpage();

                            //                //ClearAll();
                            //                this.Close();

                            //            }
                            //        }
                                  
                            //    }
                            //    #endregion
                            //}

                            #endregion
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  paybillcal()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
        private void btn_print_bill_Click(object sender, EventArgs e)
        {
            try
            {
                BillCopy_Status = false;
                Screen screen = Screen.FromControl(this);
                int screenWidth = screen.Bounds.Width;
                int screenHeight = screen.Bounds.Height;

                int adjustedWidth = 0, adjustedHeight = 0;
                if (combined_Status)
                {
                    var controlToRemove = tlp_payment_option.GetControlFromPosition(2, 0);
                    tlp_payment_option.Controls.Remove(controlToRemove);
                    controlToRemove = tlp_payment_option.GetControlFromPosition(3, 0);
                    tlp_payment_option.Controls.Remove(controlToRemove);
                    controlToRemove = tlp_payment_option.GetControlFromPosition(2, 1);
                    tlp_payment_option.Controls.Remove(controlToRemove);
                    controlToRemove = tlp_payment_option.GetControlFromPosition(3, 1);
                    tlp_payment_option.Controls.Remove(controlToRemove);

                    tlp_payment_option.Controls.Add(btncreditcard, 2, 0);
                    tlp_payment_option.Controls.Add(btnnetbanking, 2, 1);
                    btncreditcard.Visible = true;
                    btnnetbanking.Visible = true;
                    btn_splitpayment.Visible = false;
                    Btnpartpayment.Visible = false;
                    btncreditcard.Dock = DockStyle.Fill;
                    btnnetbanking.Dock = DockStyle.Fill;

                    UIHelper.CenterAndResize(tlp_payment_option, 40, 45);
                    UIHelper.RoundCorners(tlp_payment_option, 25);

                    pnl_main.Enabled = false;
                }
                else
                {
                    //CMessageBox.Show(screenWidth + "" + adjustedWidth + " " + adjustedHeight, "size", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    var controlToRemove = tlp_payment_option.GetControlFromPosition(2, 0);
                    tlp_payment_option.Controls.Remove(controlToRemove);
                    controlToRemove = tlp_payment_option.GetControlFromPosition(3, 0);
                    tlp_payment_option.Controls.Remove(controlToRemove);
                    controlToRemove = tlp_payment_option.GetControlFromPosition(2, 1);
                    tlp_payment_option.Controls.Remove(controlToRemove);
                    controlToRemove = tlp_payment_option.GetControlFromPosition(3, 1);
                    tlp_payment_option.Controls.Remove(controlToRemove);

                    tlp_payment_option.Controls.Add(btncreditcard, 2, 0);
                    tlp_payment_option.Controls.Add(btnnetbanking, 3, 0);
                    tlp_payment_option.Controls.Add(Btnpartpayment, 2, 1);
                    tlp_payment_option.Controls.Add(btn_splitpayment, 3, 1);

                    btncreditcard.Visible = true;
                    btnnetbanking.Visible = true;
                    btn_splitpayment.Visible = true;
                    Btnpartpayment.Visible = true;

                    btncreditcard.Dock = DockStyle.Fill;
                    btnnetbanking.Dock = DockStyle.Fill;
                    btn_splitpayment.Dock = DockStyle.Fill;
                    Btnpartpayment.Dock = DockStyle.Fill;

                    UIHelper.CenterAndResize(tlp_payment_option, 40, 45);
                    UIHelper.RoundCorners(tlp_payment_option, 25);

                    pnl_main.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_print_bill_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
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
                        payid = Convert.ToInt32(PaymentType.CREDITCARD);
                        break;
                    case "btnothers":
                        panelWidthPercentage = 40;
                        panelheightPercentage = 30;
                        panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                        panelHeight = (int)(this.Height * panelheightPercentage / 100);
                        pnlwifi.Size = new Size(panelWidth, panelHeight);
                        pnlwifi.Location = new Point(this.ClientSize.Width / 2 - pnlwifi.Size.Width / 2, this.ClientSize.Height / 2 - pnlwifi.Size.Height / 2);
                        pnlwifi.Visible = true;
                        wificard_timer.Start();
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

                        UIHelper.CenterAndResize(pnl_partpayment, 40, 40);
                        UIHelper.RoundCorners(pnl_partpayment, 25);
                        UIHelper.RoundCorners(tlp_part_navbar, 25);
                        UIHelper.RoundCorners(tlp_main_part, 25);

                        dgv_part.Rows.Clear();
                        foreach (Control control in flppart_paymentbutton.Controls)
                        {
                            if (control is Button)
                            {
                                ((Button)control).Visible = true;
                                ((Button)control).Enabled = true;
                            }
                        }
                        btnconfirm_part.Visible = false;
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
                tlp_payment_option.Visible = false;
                if (clickedButton.Name != "Btnpartpayment" && clickedButton.Name != "btnothers" && clickedButton.Name != "btn_splitpayment")
                    paybillcal(payid);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btncash_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
        #endregion

        #region Clear ALl
        public void ClearAll()
        {
            try
            {
                foreach (TextBox textBox in new TextBox[] { txtitemcode_serach, txtname_search, txt_product_des, txtrecvamount, txtGrandTotal, txtpaidamount, txtdis_amt, txtdis_per, txtnetamount, txtentrie_descr })
                {
                    textBox.Text = "";
                }

                btn_split_number.Visible = true;
                txtname_search.Text = "Food Name .....";
                txtitemcode_serach.Text = "Item Code .....";
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
                txtpromocode.Enabled = true;
                btn_save_promo.Enabled = true;

                txtph.Text = "";
                txtemailid.Text = "";
                txtaddress.Text = "";
                txtcusname.Text = "";

                rb_female.Checked = false;
                rb_male.Checked = false;

                //Btnpartpayment.Visible = true;
                //btn_splitpayment.Visible = true;
                combined_Status = false;

            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  ClearAll()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            try
            {
                Memoryrelease();

                lbltablecode.Visible = true;
                cmb_tblcode.Visible = false;
                btn_itemwise.Enabled = true;
                textb_shape();
                RFID_Status = false;
                Checked_VariationAddon_List.Clear();
                lblsizetotal.Text = "0";
                lbladdontotal.Text = "0";
                addon_Vari_Id = "";
                Split_Rowno = 0;
                txtdisamt_promo.Text = "0";
                txtpromocode.Text = "";
                btncreditcard.Visible = false;
                btnnetbanking.Visible = false;
                btn_splitpayment.Visible = false;
                Btnpartpayment.Visible = false;
                BillCopy_Status = false;
                txtpkchrge_amt.Text = "0";
                KOT_saleprint_status = false;
                if (OrderRunning_Status)
                {
                    Promodetailsid = 0;
                    Promomasterid = 0;
                    CheckRe_order();
                    btn_itemwise.Visible = true;
                    btn_split_number.Visible = true;
                    txtpromocode.Enabled = true;
                    btn_save_promo.Enabled = true;
                    AddEmptyRowIfNeeded();
                }
                else if (Booking_Status)
                {
                    Promodetailsid = 0;
                    Promomasterid = 0;
                    CheckRe_Bookingorder();

                    txtpromocode.Enabled = true;
                    btn_save_promo.Enabled = true;

                    Dgv_Selected_Item.Rows.Add();
                }
                else if (Pickup_Status)
                {
                   
                    Promodetailsid = 0;
                    Promomasterid = 0;
                    CheckRe_Pickuporder();

                    txtpromocode.Enabled = true;
                    btn_save_promo.Enabled = true;

                    Dgv_Selected_Item.Rows.Add();
                }
                else if (combined_Status)
                {
                    Promodetailsid = 0;
                    Promomasterid = 0;

                    Btnpartpayment.Visible = false;
                    btn_splitpayment.Visible = false;
                    int count = checkedtableNames.Count;
                    lblorderid.Visible = false;
                    label10.Visible = false;

                    Combined_Tables();
                    Combined_Table_Bill();

                    txtpromocode.Enabled = true;
                    btn_save_promo.Enabled = true;

                    Dgv_Selected_Item.Rows.Add();
                }
                else
                {
                    ClearAll();
                    checksave_refresh();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_refresh_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        #endregion

        #region Form close and minimize
        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                ResetTableViewPage();
            else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                ResetPickupViewPage();
            else if (sourcepage == Convert.ToInt32(SourcepageType.Billingpage))
                ResetBillpage();

            clsgen.Memoryrelease();

            this.Close();

        }
        #endregion

        #region Promo code
        private void btn_promo_code_Click(object sender, EventArgs e)
        {
            try
            {
                //label14,label13,label12
                if (pnl_promocode.Visible == false)
                {
                    UIHelper.CenterAndResize(pnl_promocode, 35, 30);
                    UIHelper.RoundCorners(pnl_promocode, 25);
                    UIHelper.RoundCorners(tlp_promo_nav, 25);
                }
                else
                {
                    pnl_promocode.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_promo_code_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void btn_save_promo_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtpromocode.Text == "" || txtpromocode.Text == null)
                {
                    CMessageBox.Show("Enter Promo code ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtpromocode.Focus();
                    return;
                }
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "pm.MinBillAmt Field1,pm.StartDate as StartDate,pm.EnDDate EndDate,pm.MaxDiscount Field2,pd.PromoDId Field3, pm.PromoMId Field4,pm.DiscountPer Field5";
                dmnSearch.Table = "tblPromoMaster  pm LEFT JOIN tblPromoDetails pd on pm.PromoMId =pd.PromoMId";
                dmnSearch.WhereNdGroupBy = "where pm.Status=1 and pd.code ='" + txtpromocode.Text + "'";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
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
                            Promodetailsid = Convert.ToInt32(gridSource[0].Field3);
                            Promomasterid = Convert.ToInt32(gridSource[0].Field4);
                            netAmount();
                            btn_promo_reset.Visible = true;
                        }
                    }
                    else
                    {
                        CMessageBox.Show("Promo code has expired.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                    CMessageBox.Show("Promo code Invalid.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_save_promo_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }
        #endregion

        #region Bill Combined 
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
                                #region check  Existing Item and Store ID and Row 
                                bool valueExists = false;
                                int updaterowindex = 0;
                                DataGridViewRow updateRow = null;
                                foreach (DataGridViewRow row in dgv_combined.Rows)
                                {
                                    string x = Convert.ToString(row.Cells[vname].Value);

                                    int tableType = Convert.ToInt32(gridSourcemaster_combined[i].TableType);//Convert.ToInt32(row.Cells[Ctabletype].Value);
                                    string avId = Convert.ToString(row.Cells[CAV_id].Value);
                                    int itemId = Convert.ToInt32(row.Cells[citemid].Value);
                                    int prid = Convert.ToInt32(row.Cells[cpridno].Value);
                                    int pavid = Convert.ToInt32(row.Cells[CPavid].Value);

                                    int addonStatus = Convert.ToInt32(gridSourcemaster_combined[i].AddonStatus);
                                    string sourceAvId = Convert.ToString(gridSourcemaster_combined[i].AV_id);
                                    int sourceItemId = Convert.ToInt32(gridSourcemaster_combined[i].ItemId);
                                    int sourcePrId = Convert.ToInt32(gridSourcemaster_combined[i].PrId);
                                    int sourcePavId = Convert.ToInt32(gridSourcemaster_combined[i].Pavid);

                                    if (tableType == Convert.ToInt32(Tabletype.Product) && addonStatus == 0 && itemId == sourceItemId)
                                    {
                                        updaterowindex = row.Index;
                                        updateRow = row;
                                        valueExists = true;
                                        break;
                                    }
                                    else if (tableType == Convert.ToInt32(Tabletype.Product) && addonStatus == 1 && avId == sourceAvId)
                                    {
                                        updaterowindex = row.Index;
                                        updateRow = row;
                                        valueExists = true;
                                        break;
                                    }
                                    else if (tableType == Convert.ToInt32(Tabletype.Product) && addonStatus == 2 && avId == sourceAvId && pavid == sourcePavId)
                                    {
                                        updaterowindex = row.Index;
                                        updateRow = row;
                                        valueExists = true;
                                        break;
                                    }
                                    else if (tableType == Convert.ToInt32(Tabletype.DirectSale) && prid == sourcePrId)
                                    {
                                        updaterowindex = row.Index;
                                        updateRow = row;
                                        valueExists = true;
                                        break;
                                    }
                                    else if (tableType == Convert.ToInt32(Tabletype.ComboProduct) && itemId == sourceItemId)
                                    {
                                        updaterowindex = row.Index;
                                        updateRow = row;
                                        valueExists = true;
                                        break;
                                    }

                                }
                                #endregion

                                #region Adding New Row
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
                                    dgv_combined.Rows[rowIndex].Cells[Comproducttypeno].Value = gridSourcemaster_combined[i].TableType;
                                    dgv_combined.Rows[rowIndex].Cells[cigstamt].Value = gridSourcemaster_combined[i].IGSTAmount;
                                    dgv_combined.Rows[rowIndex].Cells[CCgstamt].Value = gridSourcemaster_combined[i].CGSTAmount;
                                    dgv_combined.Rows[rowIndex].Cells[cSgstamt].Value = gridSourcemaster_combined[i].SGSTAmount;
                                    dgv_combined.Rows[rowIndex].Cells[cvatamt].Value = gridSourcemaster_combined[i].VATAmount;
                                    dgv_combined.Rows[rowIndex].Cells[cmarketprice].Value = gridSourcemaster_combined[i].MarketPrice;
                                    dgv_combined.Rows[rowIndex].Cells[Cbarcode].Value = gridSourcemaster_combined[i].ItemBarCode;
                                    dgv_combined.Rows[rowIndex].Cells[Ctabletype].Value = gridSourcemaster_combined[i].TableType;

                                    dgv_combined.Rows[rowIndex].Cells[CPavid].Value = gridSourcemaster_combined[i].Pavid;
                                    dgv_combined.Rows[rowIndex].Cells[CAddonStatus].Value = gridSourcemaster_combined[i].AddonStatus;
                                    dgv_combined.Rows[rowIndex].Cells[CAV_id].Value = gridSourcemaster_combined[i].AV_id;
                                    dgv_combined.Rows[rowIndex].Cells[CAddoncount].Value = gridSourcemaster_combined[i].Addoncount;
                                    dgv_combined.Rows[rowIndex].Cells[Cstatusno].Value = gridSourcemaster_combined[i].Status;
                                    dgv_combined.Rows[rowIndex].Cells[CVariatiionNameno].Value = gridSourcemaster_combined[i].VariatiionName;
                                    dgv_combined.Rows[rowIndex].Cells[CSalesMasterIdno].Value = gridSourcemaster_combined[i].SalesMasterId;
                                    dgv_combined.Rows[rowIndex].Cells[CSalesDetailsIdno].Value = gridSourcemaster_combined[i].SalesDetailsId;
                                    dgv_combined.Rows[rowIndex].Cells[CSalesDetailsIdno].Value = gridSourcemaster_combined[i].SalesDetailsId;
                                    dgv_combined.Rows[rowIndex].Cells[COrderDetailsIdno].Value = gridSourcemaster_combined[i].OrderDetailsId;
                                    txtsalemasterid.Text = Convert.ToString(gridSourcemaster_combined[i].SalesMasterId);
                                }
                                #endregion

                                #region updating row with id
                                if (valueExists && updateRow != null)
                                {
                                    updateRow.Cells[cqty].Value = Convert.ToInt32(updateRow.Cells[cqty].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].Quantity);
                                    //updateRow.Cells[cdisper].Value = Convert.ToInt32(updateRow.Cells[cdisper].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].DiscountPer);
                                    updateRow.Cells[cdisamt].Value = Convert.ToDecimal(updateRow.Cells[cdisamt].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].DiscountAmount);
                                    //updateRow.Cells[ctaxper].Value = Convert.ToDecimal(updateRow.Cells[ctaxper].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].TaxPer);
                                    updateRow.Cells[ctaxamt].Value = Convert.ToDecimal(updateRow.Cells[ctaxamt].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].TaxAmount);
                                    updateRow.Cells[camount].Value = Convert.ToDecimal(updateRow.Cells[camount].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].Amount);
                                    updateRow.Cells[ctotal].Value = Convert.ToDecimal(updateRow.Cells[ctotal].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].NetAmount);
                                    updateRow.Cells[cigstamt].Value = Convert.ToDecimal(updateRow.Cells[cigstamt].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].IGSTAmount);
                                    updateRow.Cells[CCgstamt].Value = Convert.ToDecimal(updateRow.Cells[CCgstamt].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].CGSTAmount);
                                    updateRow.Cells[cSgstamt].Value = Convert.ToDecimal(updateRow.Cells[cSgstamt].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].SGSTAmount);
                                    updateRow.Cells[cvatamt].Value = Convert.ToDecimal(updateRow.Cells[cvatamt].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].VATAmount);
                                    updateRow.Cells[COrderDetailsIdno].Value = updateRow.Cells[COrderDetailsIdno].Value + "," + gridSourcemaster_combined[i].OrderDetailsId;
                                }
                                #endregion

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  Combined_Tables()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        #endregion

        #region Part payment
        private void btn_card_paynow_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnl_partpayment.Visible)
                {
                    if (Convert.ToDecimal(txt_cardbalance.Text) < Convert.ToDecimal(txtcardrecamt.Text))
                    {
                        CMessageBox.Show("The entered amount is greater than the card balance. Please enter a valid amount.", "Insufficient Balance", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtcardrecamt.Focus();
                        return;
                    }
                    else if (Convert.ToDecimal(lbl_card_netamount.Text) < Convert.ToDecimal(txtcardrecamt.Text))
                    {
                        CMessageBox.Show("The entered amount is greater than the Netamount.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtcardrecamt.Focus();
                        return;
                    }
                    pnl_cardAcoount.Visible = false;
                    decimal netAmount = Convert.ToDecimal(txtnetamount.Text);
                    dgv_part.Rows[0].Cells[1].Value = Convert.ToString(txtcardrecamt.Text);
                    if (dgv_part.RowCount == 2)
                    {
                        dgv_part.Rows[1].Cells[4].Value = Convert.ToInt32(PaymentType.CASH);
                        dgv_part.Rows[1].Cells[2].Value = "CASH";
                        dgv_part.Rows[1].Cells[1].Value = netAmount - Convert.ToDecimal(txtcardrecamt.Text);
                        dgv_part.Rows[1].ReadOnly = true;
                    }
                    else
                    {
                        dgv_part.Rows.Add();
                        dgv_part.Rows[1].Cells[4].Value = Convert.ToInt32(PaymentType.CASH);
                        dgv_part.Rows[1].Cells[2].Value = "CASH";
                        dgv_part.Rows[1].Cells[1].Value = netAmount - Convert.ToDecimal(txtcardrecamt.Text);
                        dgv_part.Rows[1].ReadOnly = true;
                    }

                    rearrange_slno_part();
                    btnconfirm_part.Visible = true;

                }
                else
                {
                    paybillcal(Convert.ToInt32(PaymentType.OTHERS));
                }
                blinkTimer.Stop();
            }
            catch (Exception ex)
            {

                clsgen.LogException("Order page btn_card_paynow_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
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
                        dgv_part.BeginEdit(true);
                        break;
                    case "btnupi_part":
                        dgv_part.Rows[rowno].Cells[4].Value = Convert.ToInt32(PaymentType.UPI);
                        dgv_part.Rows[rowno].Cells[2].Value = "UPI";
                        dgv_part.CurrentCell = dgv_part.Rows[rowno].Cells[0];
                        dgv_part.BeginEdit(true);
                        clickedButton.Enabled = false;
                        break;
                    case "btnnetbanking_part":
                        dgv_part.Rows[rowno].Cells[4].Value = Convert.ToInt32(PaymentType.BANKINGONLINE);
                        dgv_part.Rows[rowno].Cells[2].Value = "BANKINGONLINE";
                        dgv_part.CurrentCell = dgv_part.Rows[rowno].Cells[0];
                        dgv_part.BeginEdit(true);
                        clickedButton.Enabled = false;
                        break;
                    case "btncreditcard_part":
                        dgv_part.Rows[rowno].Cells[4].Value = Convert.ToInt32(PaymentType.CREDITCARD);
                        dgv_part.Rows[rowno].Cells[2].Value = "CREDITCARD";
                        dgv_part.CurrentCell = dgv_part.Rows[rowno].Cells[0];
                        dgv_part.BeginEdit(true);
                        clickedButton.Enabled = false;
                        break;
                    case "btnother_part":
                        dgv_part.Rows[rowno].Cells[4].Value = Convert.ToInt32(PaymentType.OTHERS);
                        dgv_part.Rows[rowno].Cells[2].Value = "OTHERS";

                        clickedButton.Enabled = false;
                        txtcardrecamt.Visible = true;

                        int panelWidthPercentage = 40;
                        int panelheightPercentage = 30;
                        int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                        int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                        pnlwifi.Size = new Size(panelWidth, panelHeight);
                        pnlwifi.Location = new Point(this.ClientSize.Width / 2 - pnlwifi.Size.Width / 2, this.ClientSize.Height / 2 - pnlwifi.Size.Height / 2);
                        pnlwifi.Visible = true;
                        pnlwifi.BringToFront();
                        wificard_timer.Start();

                        break;
                }
                foreach (Control control in flppart_paymentbutton.Controls)
                {
                    if (control is Button && control.Name != clickedButton.Name)
                    {
                        ((Button)control).Visible = false;

                    }
                }
                dgv_part.CurrentCell = dgv_part.Rows[rowno].Cells[1];
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  btncash_part_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }

        private void btn_part_close_Click(object sender, EventArgs e)
        {
            pnl_partpayment.Visible = false;
        }

        private void btnclose_part_Click(object sender, EventArgs e)
        {
            pnl_partpayment.Visible = false;
            pnl_main.Enabled = true;
        }

        private void btnconfirm_part_Click(object sender, EventArgs e)
        {

            try
            {
                decimal netAmount = Convert.ToDecimal(txtnetamount.Text);
                if (dgv_part.RowCount == 0)
                {
                    CMessageBox.Show("Please select a payment method from the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (Convert.ToDecimal(dgv_part.Rows[0].Cells[1].Value) == 0 || dgv_part.Rows[0].Cells[1].Value == null)
                {
                    CMessageBox.Show("Please Enter the " + Convert.ToString(dgv_part.Rows[0].Cells[2].Value) + " Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btnconfirm_part_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                        btnconfirm_part.Visible = true;
                    }

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  dgv_part_CellEndEdit()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void btn_split_number_Click(object sender, EventArgs e)
        {
            try
            {
                Splitypeno = Convert.ToInt32(SplitType.Number);
                tlp_main_split.RowStyles[4] = new RowStyle(SizeType.Percent, 15);
                Control controlToRemove = tlp_main_split.GetControlFromPosition(0, 4);
                tlp_main_split.Controls.Remove(controlToRemove);
                tlp_main_split.Controls.Add(tlp_numberwise, 0, 4);
                tlp_numberwise.Dock = DockStyle.Fill;
                tlp_numberwise.Visible = true;
                btn_itemwise.Enabled = true;
                txtnumber_split.Text = "";
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_split_number_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
            catch (Exception ex)
            {

                clsgen.LogException("Order page  dgv_part_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }

        private void dgv_part_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.Value = Properties.Resources.Delete_Grid;
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
                        if (control is Button)
                        {
                            ((Button)control).Visible = true;
                            ((Button)control).Enabled = true;
                        }
                    }
                    dgv_part.Rows.Clear();
                    btnconfirm_part.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  dgv_part_CellContentClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public void rearrange_slno_part()
        {
            try
            {
                for (int i = 0; i < dgv_part.RowCount; i++)
                {
                    dgv_part.Rows[i].Cells[0].Value = i + 1;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  rearrange_slno_part()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }


        }



        private void dgv_part_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                clsgen.LogException("Order page  dgv_part_EditingControlShowing()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }
        private void dgv_part_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
            catch (Exception ex)
            {

                clsgen.LogException("Order page  dgv_part_KeyPress()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }


        }
        private void btn_itemwise_Click(object sender, EventArgs e)
        {
            try
            {
                btn_split_number.Visible = true;
                tlp_main_split.RowStyles[4] = new RowStyle(SizeType.AutoSize);
                Control controlToRemove = tlp_main_split.GetControlFromPosition(0, 4);
                tlp_main_split.Controls.Remove(controlToRemove);
                Splitypeno = Convert.ToInt32(SplitType.Number);
                pnl_split.Visible = false;
                pnl_split_itemwise.Visible = true;

                UIHelper.CenterAndResize(pnl_split_itemwise, 30, 25);
                UIHelper.RoundCorners(pnl_split_itemwise, 25);
                UIHelper.RoundCorners(tlp_itemwise_nav, 25);
                UIHelper.RoundCorners(tlp_itemwise_split, 25);
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
                        control.Dispose();
                    }
                }

                BllSearch<DmnGridFields> bllSearchmaster = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearchmaster = new DmnSearch();

                dmnSearchmaster.Select = "od.ItemName Field1,od.OrderDetailsId Field2,od.ProductType Field4,c.CategoryName Field5";
                dmnSearchmaster.Table = " tblOrderMaster om " +
                    "LEFT JOIN tblOrderDetails od on od.OrderMasterId = om.OrderMasterId and od.status=3 " +
                    "LEFT JOIN tblCategory_FTI c on od.ProductType= c.Fcid";
                dmnSearchmaster.WhereNdGroupBy = "where om.Status=1 and om.OrderMasterid= " + Convert.ToInt32(lblorderid.Text);

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
                    //Existingsalemasterid = Convert.ToInt32(CheckSource[0].Field3);
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
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_itemwise_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void btn_close_split_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                for (int i = 0; i < dgv_split.RowCount; i++)
                {
                    if (Convert.ToInt32(dgv_split.Rows[i].Cells[6].Value) == 2 && Convert.ToInt32(dgv_split.Rows[i].Cells[4].Value) > 0)
                        count++;
                }

                if (count < dgv_split.RowCount && btn_split_number.Visible)
                {
                    CMessageBox.Show("Closing not possible. Pending bills still exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (count == 0 && BillList.Count == 0 && btn_itemwise.Visible)
                {
                    pnl_split.Visible = false;
                    tlp_payment_option.Visible = true;
                    btn_con_nosplit.Visible = true;

                    btn_itemwise.Visible = true;
                    btn_split_number.Visible = true;

                    Control controlToRemove = tlp_main_split.GetControlFromPosition(0, 4);
                    tlp_main_split.Controls.Remove(controlToRemove);
                    tlp_numberwise.Visible = false;

                }
                else if (count < dgv_split.RowCount && btn_itemwise.Visible)
                {
                    CMessageBox.Show("Closing not possible. Pending bills still exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                pnl_split.Visible = false;
                pnl_main.Enabled = true;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  btn_close_split_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void btn_con_nosplit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtnumber_split.Text))
                    txtnumber_split.Text = "0";

                if (Convert.ToInt32(txtnumber_split.Text) == 0)
                {
                    CMessageBox.Show("Please Enter A Valid Number Of Parts To Split The Bill", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtnumber_split.Focus();
                    return;
                }

                paybillcal(Convert.ToInt32(PaymentType.SPLIT));
                txtdis_per.Text = "0";
                decimal shareamount = Convert.ToDecimal(txtnetamount.Text) / Convert.ToInt32(txtnumber_split.Text);
                dgv_split.Rows.Clear();
                dgv_split.Rows.Add(Convert.ToInt32(txtnumber_split.Text));
                for (int i = 0; i < Convert.ToInt32(txtnumber_split.Text); i++)
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
                txtnumber_split.Enabled = false;

            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_con_nosplit_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void dgv_split_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2 && Convert.ToInt32(dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[6].Value) == 1 && e.RowIndex >= 0)
                {
                    txt_split_recamt.Text = "";
                    int panelWidthPercentage = 40;
                    int panelheightPercentage = 40;
                    int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                    int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                    tlp_split_payment_button_main.Size = new Size(panelWidth, panelHeight);

                    tlp_split_payment_button_main.Location = new Point(this.ClientSize.Width / 2 - tlp_split_payment_button_main.Size.Width / 2, this.ClientSize.Height / 2 - tlp_split_payment_button_main.Size.Height);
                    //tlp_split_payment_button_main.Location = new Point(this.ClientSize.Width / 2 - tlp_split_payment_button_main.Size.Width / 2, this.ClientSize.Height / 2 - tlp_split_payment_button_main.Size.Height/2);
                    tlp_split_payment_button_main.Visible = true;
                    tlp_split_payment_button_main.BringToFront();
                    // pnl_split.Visible = false;
                    //// pnl_split.Enabled = false;
                    tlp_split_cash_control.Visible = false;
                    tlp_split_payment_button_main.RowStyles[0] = new RowStyle(SizeType.Percent, 100);
                    Split_Rowno = 0;

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  dgv_split_CellContentClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }
        public void rearrange_slno_split()
        {
            try
            {
                for (int i = 0; i < dgv_part.RowCount; i++)
                {
                    dgv_split.Rows[i].Cells[0].Value = i + 1;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  rearrange_slno_split()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void btn_split_paynow_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(txt_split_recamt.Text))
                {
                    CMessageBox.Show("Please Enter a Received Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_split_recamt.Focus();
                    return;
                }

                int payid = 0;
                string matchingOrderDetailsIds = "";

                tlp_split_payment_button_main.Visible = false;
                DmnPayment dmnpayment = new DmnPayment();
                Bllpayment bllpay = new Bllpayment();
                dmnpayment.PayId = 0;
                dmnpayment.OrderMasterID = Convert.ToInt32(label18.Text);
                dmnpayment.SalemasterId = Convert.ToInt32(txtsalemasterid.Text);
                dmnpayment.PayType = Convert.ToInt32(dgv_split.Rows[Split_Rowno].Cells[4].Value);
                dmnpayment.Amount = Convert.ToDecimal(dgv_split.Rows[Split_Rowno].Cells[1].Value);
                dmnpayment.Tid = tableId;

                foreach (OrderDetailsInfo orderDetailsInfo in orderDetailsInfoList)
                {
                    if (Convert.ToInt32(dgv_split.Rows[Split_Rowno].Cells[7].Value) == orderDetailsInfo.BillNumber)
                    {
                        matchingOrderDetailsIds += orderDetailsInfo.saleDetailsId + ",";
                    }
                }
                if (string.IsNullOrEmpty(txtnumber_split.Text))
                    txtnumber_split.Text = "0";
                if (string.IsNullOrEmpty(txt_split_balamt.Text))
                    txt_split_balamt.Text = "0";
                if (string.IsNullOrEmpty(txt_split_recamt.Text))
                    txt_split_recamt.Text = "0";

                dmnpayment.NumberCount = Convert.ToInt32(txtnumber_split.Text);
                dmnpayment.BillCount = BillList.Count;
                dmnpayment.SplitType = Splitypeno;
                dmnpayment.OaAmount = Convert.ToDecimal(dgv_split.Rows[Split_Rowno].Cells[8].Value);
                dmnpayment.OdAmount = Convert.ToDecimal(dgv_split.Rows[Split_Rowno].Cells[9].Value);
                dmnpayment.PromoAmt = Convert.ToDecimal(dgv_split.Rows[Split_Rowno].Cells[10].Value);
                dmnpayment.DiscountAmount = Convert.ToDecimal(dgv_split.Rows[Split_Rowno].Cells[11].Value);

                dmnpayment.BalAmnt = Convert.ToDecimal(txt_split_balamt.Text);
                dmnpayment.RcvAmnt = Convert.ToDecimal(txt_split_recamt.Text);

                dmnpayment.OrderDetailsId = matchingOrderDetailsIds;
                dmnpayment.BranchId = FrmDashboard.BranchId;
                dmnpayment.CreatedBy = FrmDashboard.UserId;
                dmnpayment.Status = 1;

                var InsertStatus = bllpay.AddEditPayment("prdInsert_Upd_Payment", dmnpayment);
                if (InsertStatus.PayId > 0)
                {
                    FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(Convert.ToInt32(txtsalemasterid.Text), Convert.ToInt32(PaymentType.SPLIT), Convert.ToInt32(InsertStatus.PayId), Convert.ToInt32(BillType.NormalBill));
                    frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                    frmSalesInvoice.ShowDialog();

                    dgv_split.Rows[Split_Rowno].Cells[5].Value = "Print Complete";
                    dgv_split.Rows[Split_Rowno].Cells[6].Value = 2;

                    DataGridViewImageCell cell1 = (DataGridViewImageCell)dgv_split.Rows[Split_Rowno].Cells[2];
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
                    if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                        ResetTableViewPage();
                    else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                        ResetPickupViewPage();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_split_paynow_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }

        private void btn_split_cash_Click(object sender, EventArgs e)
        {

            try
            {
                int payid = 0;
                string matchingOrderDetailsIds = "";
                int rowno = Convert.ToInt32(dgv_split.CurrentCell.RowIndex);
                Split_Rowno = Convert.ToInt32(dgv_split.CurrentCell.RowIndex);
                Button clickedButton = sender as Button;

                tlp_split_cash_control.Visible = false;
                switch (clickedButton.Name)
                {
                    case "btn_split_cash":
                        payid = Convert.ToInt32(PaymentType.CASH);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[4].Value = Convert.ToInt32(PaymentType.CASH);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[3].Value = "CASH";
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].ReadOnly = true;
                        tlp_split_cash_control.Visible = true;
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
                        payid = Convert.ToInt32(PaymentType.CREDITCARD);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[4].Value = Convert.ToInt32(PaymentType.CREDITCARD);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[3].Value = "CREDITCARD";
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].ReadOnly = true;
                        break;
                    case "btn_split_others":
                        payid = Convert.ToInt32(PaymentType.CREDITCARD);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[4].Value = Convert.ToInt32(PaymentType.OTHERS);
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].Cells[3].Value = "OTHERS";
                        dgv_split.Rows[dgv_split.CurrentCell.RowIndex].ReadOnly = true;
                        break;

                }

                if (payid != Convert.ToInt32(PaymentType.CASH))
                {
                    tlp_split_payment_button_main.Visible = false;
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
                            matchingOrderDetailsIds += orderDetailsInfo.saleDetailsId + ",";
                        }
                    }
                    if (string.IsNullOrEmpty(txtnumber_split.Text))
                        txtnumber_split.Text = "0";
                    dmnpayment.NumberCount = Convert.ToInt32(txtnumber_split.Text);
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
                        FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(Convert.ToInt32(txtsalemasterid.Text), Convert.ToInt32(PaymentType.SPLIT), Convert.ToInt32(InsertStatus.PayId), Convert.ToInt32(BillType.NormalBill));
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
                        if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                            ResetTableViewPage();
                        else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                            ResetPickupViewPage();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                clsgen.LogException("Order page  btn_split_cash_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void btn_split_payclose_Click(object sender, EventArgs e)
        {
            tlp_split_payment_button_main.Visible = false;
            pnl_splitpayment.Visible = true;
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
                int totalcount = flp_itemwise_products.Controls.OfType<CheckBox>().Count();
                int checkcount = flp_itemwise_products.Controls.OfType<CheckBox>().Count(cb => cb.Checked);

                if ((totalcount != checkcount) && BillList.Count == 0)
                {
                    CMessageBox.Show("Please add products to the bill before Confirm.", "Incomplete Bill", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    btn_itemwise.Enabled = false;
                    btn_split_number.Visible = false;
                    paybillcal(Convert.ToInt32(PaymentType.SPLIT));
                    int j = 0;
                    decimal amount = 0, tamt = 0, promoamt = 0;
                    dgv_split.Rows.Clear();
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "od.OrderDetailsId Field1,od.NetAmount Field2";
                    dmnSearch.Table = "tblOrderMaster om " +
                        "LEFT JOIN tblOrderDetails od on od.OrderMasterId = om.OrderMasterId and od.status=3 ";
                    dmnSearch.WhereNdGroupBy = "where om.OrderMasterId = " + lblorderid.Text + " and om.Status IN (3)";
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
                                foreach (DmnGridFields OrderDetails in gridSource)
                                {
                                    foreach (DataGridViewRow row in dgv.Rows)
                                    {
                                        if (row.Cells[2].Value != null && Convert.ToInt32(row.Cells[2].Value) == Convert.ToInt32(OrderDetails.Field1))
                                        {
                                            amount = Convert.ToDecimal(OrderDetails.Field2);
                                            tamt = tamt + amount;
                                        }
                                    }
                                }
                            }
                            decimal otheraddamt = 0, otherdedamt = 0;
                            dgv_split.Rows.Add();
                            if (string.IsNullOrEmpty(txtdiscountamt_promo.Text))
                                txtdiscountamt_promo.Text = "0";

                            decimal discountAmnt = Convert.ToDecimal(txtdis_per.Text.Trim()) * tamt / 100;

                            if (Convert.ToDecimal(txtdiscountamt_promo.Text) > 0)
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
                clsgen.LogException("Order page  btns_itemwise_confirm_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void btn_close_pnlitemwise_Click(object sender, EventArgs e)
        {
            pnl_split_itemwise.Visible = false;
            pnl_split.Visible = true;
        }

        #endregion

        #region Item wise Product payment 
        private void ib_add_categ_itemwise_Click(object sender, EventArgs e)
        {
            try
            {
                int count = flp_itemwise_por_sort.Controls.OfType<Panel>().Count() + 1;

                // Main panel
                Panel mainPanel = new Panel
                {
                    Name = "BILL_pnl" + count,
                    Tag = count,
                    BackColor = Color.LightGray,
                    Dock = DockStyle.None,
                    Height = 50,
                    Width = 280,
                    Margin = new Padding(10, 10, 0, 0)
                };

                // Main TableLayoutPanel
                TableLayoutPanel mainTableLayoutPanel = new TableLayoutPanel
                {
                    Name = "BILL_tlp_box" + count,
                    Dock = DockStyle.Fill,
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
                };
                mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
                mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 75));

                // Navbar TableLayoutPanel
                TableLayoutPanel downArrowTableLayoutPanel = new TableLayoutPanel
                {
                    Name = "BILL_tlp_navbar" + count,
                    Dock = DockStyle.Fill,
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
                };
                downArrowTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));
                downArrowTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));
                downArrowTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20));

                // Label
                Label label = new Label
                {
                    Text = "BILL" + count,
                    Tag = count,
                    Dock = DockStyle.Fill,
                    Font = new Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                label.Click += BILLName_Click;
                downArrowTableLayoutPanel.Controls.Add(label, 0, 0);

                // Down arrow icon
                PictureBox downArrowIcon = new PictureBox
                {
                    Name = "BILL_downarrow" + count,
                    Tag = count,
                    Image = Properties.Resources.down_arrow,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(20, 20),
                    Dock = DockStyle.Fill
                };
                downArrowIcon.Click += DownArrowIcon_Click;
                downArrowTableLayoutPanel.Controls.Add(downArrowIcon, 1, 0);

                // Close icon
                PictureBox close_product_cart = new PictureBox
                {
                    Name = "BILLclose" + count,
                    Tag = count,
                    Image = Properties.Resources.Delete_Grid,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(20, 20),
                    Dock = DockStyle.Fill
                };
                close_product_cart.Click += close_product_cart_Click;
                downArrowTableLayoutPanel.Controls.Add(close_product_cart, 2, 0);

                // DataGridView
                DataGridView dataGridView = new DataGridView
                {
                    Name = "BILLdgv" + count,
                    Tag = count,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = true,
                    BorderStyle = BorderStyle.None,
                    AlternatingRowsDefaultCellStyle = { BackColor = Color.FromArgb(238, 239, 249) },
                    CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                    DefaultCellStyle = { SelectionBackColor = Color.DarkTurquoise, SelectionForeColor = Color.WhiteSmoke },
                    BackgroundColor = Color.White,
                    EnableHeadersVisualStyles = false,
                    ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                    RowHeadersVisible = false,
                    Visible = false
                };

                // Columns
                dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Slno", AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader });
                dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Item Name", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
                dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "orderdetailsid", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, Visible = false });
                dataGridView.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "categid", AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells, Visible = false });
                dataGridView.Rows.Add();

                // Add controls to table
                mainTableLayoutPanel.Controls.Add(downArrowTableLayoutPanel, 0, 0);
                mainTableLayoutPanel.Controls.Add(dataGridView, 0, 1);
                mainPanel.Controls.Add(mainTableLayoutPanel);
                flp_itemwise_por_sort.Controls.Add(mainPanel);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page ib_add_categ_itemwise_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page  DownArrowIcon_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                                Cnames = "chk_pro_" + Convert.ToInt32(checkBox.Tag)
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
                                BillNumber = Convert.ToInt32(clickedControl.Tag),
                            });

                            dgvcontrol.Rows.Add();
                            i++;
                        }
                    }

                    // Remove old checkboxes safely
                    foreach (Namelist nameInfo in CheckedNames)
                    {
                        Control controlToRemove = flp_itemwise_products.Controls.Find(nameInfo.Cnames, true).FirstOrDefault();
                        if (controlToRemove != null)
                        {
                            flp_itemwise_products.Controls.Remove(controlToRemove);
                            controlToRemove.Dispose(); // Dispose to prevent memory leak
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page BILLName_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
            public int saleDetailsId { get; set; }
            public int BillNumber { get; set; }
        }

        private void close_product_cart_Click(object sender, EventArgs e)
        {
            try
            {
                CheckedNames.Clear();
                Control clickedControl = sender as Control;
                string pnlname = "BILL_pnl" + clickedControl.Tag;
                Panel pnlcontrol = this.Controls.Find(pnlname, true).FirstOrDefault() as Panel;

                if (pnlcontrol != null)
                {
                    // Restore items back as CheckBoxes
                    DataGridView dgvcontrol = pnlcontrol.Controls.Find("BILLdgv" + clickedControl.Tag, true).FirstOrDefault() as DataGridView;
                    for (int i = 0; i < dgvcontrol.RowCount; i++)
                    {
                        if (Convert.ToInt32(dgvcontrol.Rows[i].Cells[2].Value) > 0)
                        {
                            CheckBox checkBox = new CheckBox
                            {
                                Name = "chk_pro_" + Convert.ToInt32(dgvcontrol.Rows[i].Cells[2].Value),
                                Text = Convert.ToString(dgvcontrol.Rows[i].Cells[1].Value),
                                Tag = Convert.ToInt32(dgvcontrol.Rows[i].Cells[2].Value),
                                AccessibleDescription = Convert.ToString(dgvcontrol.Rows[i].Cells[3].Value),
                                AutoSize = true,
                                Margin = new Padding(10, 10, 0, 0),
                                ForeColor = Color.Black,
                                BackColor = Color.Yellow,
                                Size = new Size(150, 30),
                                Font = new Font("Arial", 14, FontStyle.Bold)
                            };
                            flp_itemwise_products.Controls.Add(checkBox);
                        }
                    }

                    // Remove panel safely
                    flp_itemwise_por_sort.Controls.Remove(pnlcontrol);
                    pnlcontrol.Dispose(); // Dispose panel and all child controls

                    // Remove from BillList
                    var existingItem = BillList.FirstOrDefault(item => item.Id == Convert.ToInt32(clickedControl.Tag));
                    if (existingItem != null) BillList.Remove(existingItem);

                    var existingItem1 = orderDetailsInfoList.FirstOrDefault(item => item.BillNumber == Convert.ToInt32(clickedControl.Tag));
                    if (existingItem1 != null) orderDetailsInfoList.Remove(existingItem1);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page close_product_cart_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void chk_itemwise_pro_selectall_CheckedChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                clsgen.LogException("Order page  chk_itemwise_pro_selectall_CheckedChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }
        #endregion

        #region Customer Registration 

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
            try
            {
                if (!string.IsNullOrWhiteSpace(txtemailid.Text))
                {
                    if (clsgen.CheckEmailId(this, txtemailid.Text) != true)
                        txtemailid.Focus();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page txtemailid_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void txtph_Leave(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                clsgen.LogException("Order page txtph_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void btn_edit_cus_Click(object sender, EventArgs e)
        {
            try
            {
                //txtph.Enabled = true;
                //txtemailid.Enabled = true;
                //txtaddress.Enabled = true;
                //txtcusname.Enabled = true;
                //rb_female.Enabled = true;
                //rb_male.Enabled = true;
                pnl_userreg.Visible = false;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btn_edit_cus_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void btn_clear_cus_Click(object sender, EventArgs e)
        {
            try
            {
                txtph.Text = "";
                txtemailid.Text = "";
                txtaddress.Text = "";
                txtcusname.Text = "";
                rb_female.Checked = false;
                rb_male.Checked = false;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btn_clear_cus_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        #endregion

        #region Reservation
        private void btncancelrev_Click(object sender, EventArgs e)
        {
            try
            {
                var result = CMessageBox.Show("Do You want Cancel  This Reservation", "Confirm The Action", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {

                    BllReservation bllReservation = new BllReservation();
                    DmnReservation dmnReservation = new DmnReservation();

                    dmnReservation.BookmasterId = Bookingmasterid;
                    dmnReservation.OrderMasterId = Convert.ToInt32(lblorderid.Text);
                    dmnReservation.CreatedBy = FrmDashboard.UserId;
                    dmnReservation.Status = 0;
                    dmnReservation.Cancel_Type = Convert.ToInt32(Rev_Cancel_Type.reservation_Order);
                    dmnReservation.BranchId = 1030;
                    var InsertStatus = bllReservation.Reservation_Delete_Orderpage("prd_deleteReservation", dmnReservation);
                    if (InsertStatus.BookmasterId > 0)
                    {
                        CMessageBox.Show("Reservation Cancel Sucessfull", "Information", MessageBoxButtons.OK);
                        this.Close();
                        if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                            ResetTableViewPage();
                        else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                            ResetPickupViewPage();
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btncancelrev_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        #endregion

        #region side menu
        private void btn_showmenu_Click(object sender, EventArgs e)
        {
            Memoryrelease();

            flp_sidemenu.Visible = !flp_sidemenu.Visible;

            if (!flp_sidemenu.Visible)
                return;

            flp_sidemenu.FlowDirection = FlowDirection.LeftToRight;
            flp_sidemenu.Width = btn_showmenu.Width + (btn_showmenu.Width / 4);
            flp_sidemenu.Height = tlp_productlist.Height;
            flp_sidemenu.AutoScroll = true;
            flp_sidemenu.HorizontalScroll.Enabled = true;
            flp_sidemenu.HorizontalScroll.Visible = true;
            flp_sidemenu.VerticalScroll.Enabled = false;
            flp_sidemenu.VerticalScroll.Visible = false;


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
            Point clientPos = this.PointToClient(new Point(desiredX, desiredY));
            flp_sidemenu.Location = clientPos;


            GenerateSidemenu();
            foreach (Control item in flp_sidemenu.Controls)
            {
                item.Margin = new Padding((flp_sidemenu.Width - item.Width) / 2, 5, 0, 5);
            }

            flp_sidemenu.BringToFront();
        }
        private ToolTip sideMenuToolTip; // reuse single tooltip for all buttons

        private void GenerateSidemenu()
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

                // If the count matches, we can skip regeneration
                if (flp_sidemenu.Controls.Count == expectedCount)
                    return;

                flp_sidemenu.SuspendLayout();

                // Dispose old controls to prevent memory leaks
                foreach (Control ctrl in flp_sidemenu.Controls)
                    ctrl.Dispose();
                flp_sidemenu.Controls.Clear();

                // Reuse a single ToolTip
                if (sideMenuToolTip == null)
                    sideMenuToolTip = new ToolTip();

                #region Favorite Button
                Button buttonfav = new Button
                {
                    Text = "Favourite Item",
                    Name = "btn_fav",
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
                buttonfav.FlatAppearance.BorderSize = 0;
                buttonfav.Click += SidebuttonfavClickEventHandler;
                sideMenuToolTip.SetToolTip(buttonfav, "Favourite Item");
                flp_sidemenu.Controls.Add(buttonfav);
                #endregion

                #region Offer Button
                Button buttonOffer = new Button
                {
                    Text = "Special Offer",
                    Name = "btn_offer",
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
                buttonOffer.FlatAppearance.BorderSize = 0;
                buttonOffer.Click += SidebuttonOfferClickEventHandler;
                sideMenuToolTip.SetToolTip(buttonOffer, "Special Offer");
                flp_sidemenu.Controls.Add(buttonOffer);
                #endregion

                #region Combo Offer Button
                Button buttonComboOffer = new Button
                {
                    Text = "Combo Offer",
                    Name = "btn_combo_offer",
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
                buttonComboOffer.FlatAppearance.BorderSize = 0;
                buttonComboOffer.Click += SidebuttonComboOfferClickEventHandler;
                sideMenuToolTip.SetToolTip(buttonComboOffer, "Combo Offer");
                flp_sidemenu.Controls.Add(buttonComboOffer);
                #endregion

                #region Category Buttons
                foreach (var cat in gridSource)
                {
                    Button button = new Button
                    {
                        Text = cat.CategoryName,
                        Name = "btn" + cat.CategoryName,
                        Tag = cat.Fcid,
                        ForeColor = Color.White,
                        Height = bheight,
                        Width = bWidth,
                        Dock = DockStyle.Top,
                        BackgroundImage = Properties.Resources.fb_sidemenu,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        FlatStyle = FlatStyle.Flat,
                        Font = AppFonts.GetFont(9)
                    };
                    button.FlatAppearance.BorderSize = 0;
                    button.Click += SidebuttonClickEventHandler;
                    sideMenuToolTip.SetToolTip(button, cat.CategoryName);

                    flp_sidemenu.Controls.Add(button);
                }
                #endregion

                flp_sidemenu.ResumeLayout();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page GenerateSidemenu()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        #endregion

        #region Next Previous Button
        private void btnprev_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPage > 1) // <-- correct check
                {
                    currentPage--;

                    pagedatadata = ProductData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                    lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {ProductData.Count}";
                    btnprev.Enabled = currentPage > 1;
                    btnnext.Enabled = currentPage < totalPages;

                    if (McategoryID == Convert.ToInt32(MenucategryType.NormalProduct))
                        GenearteNormalProduct();
                    else if (McategoryID == Convert.ToInt32(MenucategryType.OfferProduct))
                        GenerateofferProduct();
                    else if (McategoryID == Convert.ToInt32(MenucategryType.ComboProduct))
                        GenerateComboProduct();
                    else if (McategoryID == Convert.ToInt32(MenucategryType.favourite))
                        GenerateFavouriteProduct();
                    else if (McategoryID == Convert.ToInt32(MenucategryType.ProductcodeSerach))
                        GenerateSerachProduct_Name_code();
                    else if (McategoryID == Convert.ToInt32(MenucategryType.ProductcodeSerach))
                        GenerateSerachProduct_Name_code();

                    //UpdatePagerInfo(); // <-- ensure pager label updates
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btnprev_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPage < totalPages)
                {
                    currentPage++;

                    pagedatadata = ProductData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                    lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {ProductData.Count}";
                    btnprev.Enabled = currentPage > 1;
                    btnnext.Enabled = currentPage < totalPages;

                    if (McategoryID == Convert.ToInt32(MenucategryType.NormalProduct))
                        GenearteNormalProduct();
                    else if (McategoryID == Convert.ToInt32(MenucategryType.OfferProduct))
                        GenerateofferProduct();
                    else if (McategoryID == Convert.ToInt32(MenucategryType.ComboProduct))
                        GenerateComboProduct();
                    else if (McategoryID == Convert.ToInt32(MenucategryType.favourite))
                        GenerateFavouriteProduct();
                    else if (McategoryID == Convert.ToInt32(MenucategryType.ProductcodeSerach))
                        GenerateSerachProduct_Name_code();
                    else if (McategoryID == Convert.ToInt32(MenucategryType.ProductcodeSerach))
                        GenerateSerachProduct_Name_code();

                    //UpdatePagerInfo(); // <-- update pager label
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btnnext_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        #endregion

        #region Unknown 


        public void ResetBillpage()
        {
            if (System.Windows.Forms.Application.OpenForms["FrmCombinedBill"] != null)
            {
                FrmCombinedBill _instanceFrmCombinedBill = Application.OpenForms["FrmCombinedBill"] as FrmCombinedBill;
                if (_instanceFrmCombinedBill.CheckedtableNames.Count() > 0)
                {
                    _instanceFrmCombinedBill.CheckedtableNames.Clear();

                    _instanceFrmCombinedBill.cmb_tbl_names.Visible = false;
                    _instanceFrmCombinedBill.lbl_tbl_totalcount.Visible = false;
                    _instanceFrmCombinedBill.cmb_tbl_names.Items.Clear();
                    _instanceFrmCombinedBill.lbl_tbl_totalcount.Text = "Total Tables : ";
                }
                _instanceFrmCombinedBill.BillCombineBill_Update();
            }
        }
        public void ResetTableViewPage()
        {
            if (System.Windows.Forms.Application.OpenForms["FrmPickup"] != null)
            {
                FrmPickup _instanceFrmPickup = Application.OpenForms["FrmPickup"] as FrmPickup;
                _instanceFrmPickup.Visible = false;
            }
            if (System.Windows.Forms.Application.OpenForms["FrmTableView"] != null)
            {
                FrmTableView _instanceFrmTableView = Application.OpenForms["FrmTableView"] as FrmTableView;
                _instanceFrmTableView.Visible = true;
                _instanceFrmTableView.TableStaus(0);
            }
        }
        public void ResetPickupViewPage()
        {
            if (System.Windows.Forms.Application.OpenForms["FrmTableView"] != null)
            {
                FrmTableView _instanceFrmTableView = Application.OpenForms["FrmTableView"] as FrmTableView;
                _instanceFrmTableView.Visible = false;
            }
            if (System.Windows.Forms.Application.OpenForms["FrmPickup"] != null)
            {
                FrmPickup _instanceFrmPickup = Application.OpenForms["FrmPickup"] as FrmPickup;
                _instanceFrmPickup.Visible = true;
                _instanceFrmPickup.UpdatePickupView(0);
            }
        }
        private void btn_grid_existing_close_Click(object sender, EventArgs e)
        {
            pnl_grid_existing_products.Visible = false;
        }
        private void btn_show_existing_product_Click(object sender, EventArgs e)
        {
            try
            {
                //if (tlp_grid_existing_products.Visible)
                //{
                //    tlp_grid_existing_products.Visible = false;
                //}
                //else
                //{
                //    tlp_grid_existing_products.Visible = true;

                //    int centerX = this.ClientSize.Width / 2 - tlp_grid_existing_products.Size.Width / 2;
                //    int centerY = this.ClientSize.Height / 2 - tlp_grid_existing_products.Size.Height / 2;

                //    // Get the screen resolution
                //    Screen screen = Screen.FromControl(this);
                //    int screenWidth = screen.Bounds.Width;
                //    int screenHeight = screen.Bounds.Height;

                //    // Adjust the position based on screen resolution
                //    int adjustedX = (screenWidth - tlp_grid_existing_products.Size.Width) / 2;
                //    int adjustedY = (screenHeight - tlp_grid_existing_products.Size.Height) / 2;

                //    // Set the location of tlp_grid_existing_products
                //    tlp_grid_existing_products.Location = new Point(adjustedX, adjustedY);
                //}
                if (dgv_combined.Visible)
                {
                    dgv_combined.Visible = false;
                }
                else
                {
                    dgv_combined.Visible = true;

                    int centerX = this.ClientSize.Width / 2 - dgv_combined.Size.Width / 2;
                    int centerY = this.ClientSize.Height / 2 - dgv_combined.Size.Height / 2;

                    // Get the screen resolution
                    Screen screen = Screen.FromControl(this);
                    int screenWidth = screen.Bounds.Width;
                    int screenHeight = screen.Bounds.Height;

                    // Adjust the position based on screen resolution
                    int adjustedX = (screenWidth - dgv_combined.Size.Width) / 2;
                    int adjustedY = (screenHeight - dgv_combined.Size.Height) / 2;

                    // Set the location of dgv_combined
                    dgv_combined.Location = new Point(adjustedX, adjustedY);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_show_existing_product_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }


        }
        private void btn_close_viewmore_Click(object sender, EventArgs e)
        {
            pnl_main.Enabled = true;
            pnlproductviewmore.Visible = false;
        }
        private void btn_frm_close_Click(object sender, EventArgs e)
        {
            if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                ResetTableViewPage();
            else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                ResetPickupViewPage();
            else if (sourcepage == Convert.ToInt32(SourcepageType.Billingpage))
                ResetBillpage();
            this.Close();
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
        private void btn_gridview_close_Click(object sender, EventArgs e)
        {
            pnl_main.Enabled = true;
            pnl_gridview.Visible = false;
        }
        private void btn_multiprice_Click(object sender, EventArgs e)
        {
            pnlprice.Visible = false;
        }
        private void btn_close_tableprice_Click(object sender, EventArgs e)
        {
            pnl_tableprice.Visible = false;
            pnl_main.Enabled = true;

        }
        private void tablenameclcik(object sender, EventArgs e)
        {
            try
            {
                pnl_main.Enabled = false;
                Control clickedControl = sender as Control;

                UIHelper.ResizeOnly(pnl_tableprice, 40, 30);

                UIHelper.RoundCorners(pnl_tableprice, 25);
                UIHelper.RoundCorners(tlp_tablelist_nav, 25);
                UIHelper.RoundCorners(tlp_tablelist, 25);

                pnl_tableprice.Location = new Point(this.ClientSize.Width / 2 - pnl_tableprice.Size.Width / 2, this.ClientSize.Height / 2 - pnl_tableprice.Size.Height / 2);

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "bd.Name Field1,c.CategoryName Field2,bd.Price Field3 ,bd.BookdetailId Field4,bd.BookmasterId Field5,bd.MTID Field6";
                dmnSearch.Table = "tblBookDetails  bd left join tblTable_info t on t.Tid=bd.MTID left join tblCategory_FTI c on c.Fcid=t.Fcid";
                dmnSearch.WhereNdGroupBy = "where Rid=0 and bd.Status=1 and BookmasterId=" + clickedControl.Tag;
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    decimal total = 0;
                    dgv_tableprice.Rows.Clear();
                    dgv_tableprice.Rows.Add(gridSource.Count + 1);
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        dgv_tableprice.Rows[i].Cells[tslno].Value = i + 1;
                        dgv_tableprice.Rows[i].Cells[ttablenameno].Value = Convert.ToString(gridSource[i].Field1);
                        dgv_tableprice.Rows[i].Cells[ttablecategno].Value = Convert.ToString(gridSource[i].Field2);
                        dgv_tableprice.Rows[i].Cells[ttablechargeno].Value = gridSource[i].Field3;
                        dgv_tableprice.Rows[i].Cells[tbookingdetailsid].Value = gridSource[i].Field4;
                        dgv_tableprice.Rows[i].Cells[tbookingmasteridno].Value = gridSource[i].Field5;
                        dgv_tableprice.Rows[i].Cells[ttidno].Value = gridSource[i].Field6;

                        DataGridViewImageCell cell = (DataGridViewImageCell)dgv_tableprice.Rows[i].Cells[tactionno];
                        cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        cell.Value = Properties.Resources.Delete_Grid;

                        total = total + Convert.ToDecimal(gridSource[i].Field3);
                    }
                    dgv_tableprice.Rows[gridSource.Count].Cells[ttablenameno].Value = "Total Amount";
                    dgv_tableprice.Rows[gridSource.Count].Cells[ttablechargeno].Value = total;

                    var oldFont1 = dgv_tableprice.Rows[gridSource.Count].Cells[ttablenameno].Style.Font;
                    var oldFont2 = dgv_tableprice.Rows[gridSource.Count].Cells[ttablechargeno].Style.Font;

                    if (oldFont1 != null) oldFont1.Dispose();
                    if (oldFont2 != null) oldFont2.Dispose();

                    dgv_tableprice.Rows[gridSource.Count].Cells[ttablenameno].Style.Font = new Font("Arial", 10, FontStyle.Bold);
                    dgv_tableprice.Rows[gridSource.Count].Cells[ttablechargeno].Style.Font = new Font("Arial", 10, FontStyle.Bold);


                    int lr = dgv_tableprice.Rows.Count - 1;
                    DataGridViewImageCell blankCell = (DataGridViewImageCell)dgv_tableprice.Rows[lr].Cells[tactionno];
                    blankCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    blankCell.Value = Properties.Resources.orderview_blank;

                    pnl_tableprice.Visible = true;

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page tablenameclcik()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void dgv_tableprice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == tactionno)
                {
                    if (dgv_tableprice.Rows.Count == 2)
                    {
                        CMessageBox.Show("Not all tables can be removed.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        var result = CMessageBox.Show("Do You want To Remove Table From This Reservation", "Confirm The Action", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            int Bookindetaildid = Convert.ToInt32(dgv_tableprice.Rows[e.RowIndex].Cells[tbookingdetailsid].Value);
                            BllReservation bll_order = new BllReservation();
                            DmnReservationDetails dmnremovetable = new DmnReservationDetails();
                            dmnremovetable.BookdetailId = Bookindetaildid;
                            dmnremovetable.BookmasterId = Convert.ToInt32(dgv_tableprice.Rows[e.RowIndex].Cells[tbookingmasteridno].Value);
                            dmnremovetable.Price = Convert.ToDecimal(dgv_tableprice.Rows[e.RowIndex].Cells[ttablechargeno].Value);
                            dmnremovetable.Tid = Convert.ToInt32(dgv_tableprice.Rows[e.RowIndex].Cells[ttidno].Value);
                            dmnremovetable.RemoveNotes = "Cancelled";
                            dmnremovetable.CreatedBy = FrmDashboard.UserId;
                            dmnremovetable.BranchId = FrmDashboard.BranchId;
                            dmnremovetable.Status = 0;
                            // dmnremovetable.RemoveNotes = "";
                            var InsertStatusP = bll_order.Reservation_Table_Delete("[prd_remove_BookingTable]", dmnremovetable);

                            if (InsertStatusP.BookdetailId > 0)
                            {

                                dgv_tableprice.Rows.RemoveAt(e.RowIndex);
                                decimal total = 0;
                                for (int i = 0; i < dgv_tableprice.Rows.Count - 1; i++)
                                {
                                    total += Convert.ToDecimal(dgv_tableprice.Rows[i].Cells[ttablechargeno].Value);
                                }
                                dgv_tableprice.Rows[dgv_tableprice.Rows.Count - 1].Cells[ttablechargeno].Value = total;

                                string txtcountname = "txttablecount_" + Bookingmasterid;
                                string txtpricename = "txttotalresprice_" + Bookingmasterid;

                                TextBox txtcountcontrol = this.Controls.Find(txtcountname, true).FirstOrDefault() as TextBox;
                                TextBox txtpricecontrol = this.Controls.Find(txtpricename, true).FirstOrDefault() as TextBox;
                                if (txtcountcontrol != null && txtpricecontrol != null)
                                {
                                    int count = Convert.ToInt32(txtcountcontrol.Text) - 1;
                                    txtcountcontrol.Text = Convert.ToString(count);
                                    txtpricecontrol.Text = Convert.ToString(total);
                                }
                                txtreservationtotal.Text = Convert.ToString(total);
                                netAmount();
                            }

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page dgv_tableprice_CellClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void pb_item_table_delete_ClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDecimal(txtreservationtotal.Text) > 0)
                {
                    var result = CMessageBox.Show("Do You want To Remove Table Charge", "Confirm The Action", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        Control clickedControl = sender as Control;

                        decimal new_tot = Convert.ToDecimal(txtGrandTotal.Text) - Convert.ToDecimal(txtreservationtotal.Text);
                        txtGrandTotal.Text = new_tot.ToString("N2");
                        discountAmnt();
                        txtreservationtotal.Text = "0";

                        #region Net Amount not taking advance amount

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
                            netAmount = (Convert.ToDecimal(txtGrandTotal.Text)) - (Convert.ToDecimal(txtdis_amt.Text)) + Convert.ToDecimal(txtdiscountamt_promo.Text);
                            txtnetamount.Text = netAmount.ToString("N2");
                            label20.Text = "Net Amount : " + txtnetamount.Text;
                            label21.Text = "Net Amount : " + txtnetamount.Text;
                            decimal balAmnt = netAmount - Convert.ToDecimal(txtpaidamount.Text);
                            txtrecvamount.Text = balAmnt.ToString("N2");
                        }

                        totQty();

                        #endregion

                        string txtcountname = "txttablecount_" + Bookingmasterid;
                        string txtpricename = "txttotalresprice_" + Bookingmasterid;

                        TextBox txtcountcontrol = this.Controls.Find(txtcountname, true).FirstOrDefault() as TextBox;
                        TextBox txtpricecontrol = this.Controls.Find(txtpricename, true).FirstOrDefault() as TextBox;
                        if (txtcountcontrol != null && txtpricecontrol != null)
                        {

                            txtcountcontrol.Text = "0";
                            txtpricecontrol.Text = "0.00";
                        }
                        netAmount();
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page pb_item_table_delete_ClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void btn_cardacc_close_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_cardAcoount.Visible = false;
                pnl_main.Enabled = true;
                blinkTimer.Stop();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btn_cardacc_close_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void wificard_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                reading_RFID = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul(FrmLogin.Compid);
                if (reading_RFID != "Card not in contact")
                {
                    wificard_timer.Stop();
                    if (reading_RFID == "" || reading_RFID == "empty" || string.IsNullOrWhiteSpace(reading_RFID) || reading_RFID == "FailAuthenticationFailAuthenticationFailAuthentication")
                        reading_RFID = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi(FrmLogin.Compid);
                    reading_RFID = RemoveSpecialCharacters(reading_RFID.Trim());
                    if (reading_RFID == "" || string.IsNullOrWhiteSpace(reading_RFID) || reading_RFID == "FailAuthenticationFailAuthenticationFailAuthentication" || reading_RFID == "Cardnotincontact")
                    {
                        CMessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RFID_Status = false;
                        wificard_timer.Start();
                        blinkTimer.Stop();
                    }
                    else
                    {
                        wificard_timer.Stop();
                        RFID_Status = true;
                        btn_card_paynow.Visible = false;
                        lbl_insff_cashbal.Visible = false;
                        BllSearch<DmnAccountLedger> bllSearch = new BllSearch<DmnAccountLedger>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "*";
                        dmnSearch.Table = " mPOS_DBNew.dbo.tblAccountLedger";
                        dmnSearch.WhereNdGroupBy = "Where status = 1 and  ICardNo = '" + reading_RFID + "'";
                        dmnSearch.OrderBy = "";
                        var gridSource = bllSearch.GetSearchData_CardAcc("prdSearch", dmnSearch);
                        if (gridSource.Count > 0)
                        {
                            blinkTimer.Stop();
                            lbl_card_netamount.Text = txtnetamount.Text;
                            txt_cardbalance.Text = Convert.ToString(gridSource[0].CardBalance);
                            txt_cardaddress.Text = Convert.ToString(gridSource[0].LedgerAddress);
                            txtcardemail.Text = Convert.ToString(gridSource[0].EmailId);
                            txt_cardph.Text = Convert.ToString(gridSource[0].Mobile);
                            txt_cardaccname.Text = Convert.ToString(gridSource[0].LedgerName);

                            string modified_cardno = "XXXX XX" + reading_RFID.Substring(reading_RFID.Length - 6, 6);
                            txt_cardno.Text = Convert.ToString(modified_cardno);

                            if (Convert.ToInt32(gridSource[0].Gender) == 1)
                                lbl_cardgender.Text = "Male";
                            else if (Convert.ToInt32(gridSource[0].Gender) == 2)
                                lbl_cardgender.Text = "Female";
                            else
                                lbl_cardgender.Text = "Others";

                            int LedgerId = Convert.ToInt32(gridSource[0].LedgerId);
                            txt_LedgerId.Text = Convert.ToString(gridSource[0].LedgerId);
                            bool imageExist = clsDocument.FileExistOrNot(LedgerId.ToString() + ".jpeg", DocumentType.CustomerPhoto);
                            pb_cardprofile.Image = null;
                            if (imageExist)
                            {
                                pb_cardprofile.Image = null;

                                if (pb_cardprofile.BackgroundImage != null)
                                {
                                    pb_cardprofile.BackgroundImage.Dispose();
                                    pb_cardprofile.BackgroundImage = null;
                                }
                                pb_cardprofile.BackgroundImage = clsDocument.DownloadImage(LedgerId.ToString() + ".jpeg", DocumentType.CustomerPhoto);
                                pb_cardprofile.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            else
                            {
                                if (Convert.ToInt32(gridSource[0].Gender) == 1)
                                    pb_cardprofile.BackgroundImage = Properties.Resources.employe_male_icon;
                                else if (Convert.ToInt32(gridSource[0].Gender) == 2)
                                    pb_cardprofile.BackgroundImage = Properties.Resources.employe_female_icon;
                                else
                                    pb_cardprofile.BackgroundImage = Properties.Resources.user_add_edit;

                                pb_cardprofile.BackgroundImageLayout = ImageLayout.Stretch;
                            }
                            if (pnl_partpayment.Visible != true)
                            {
                                if (Convert.ToDecimal(gridSource[0].CardBalance) < Convert.ToDecimal(txtnetamount.Text))
                                {
                                    lbl_insff_cashbal.Visible = true;
                                    blinkTimer.Start();
                                    btn_card_paynow.Visible = false;
                                }
                                else
                                {
                                    blinkTimer.Stop();
                                    lbl_insff_cashbal.Visible = false;
                                    btn_card_paynow.Visible = true;
                                }
                            }
                            else
                            {
                                txtcardrecamt.Text = "0.00";
                                btn_card_paynow.Visible = true;
                            }

                            UIHelper.CenterAndResize(pnl_cardAcoount, 75, 45);
                            UIHelper.RoundCorners(pnl_cardAcoount, 25);
                            UIHelper.RoundCorners(tlp_card_header, 25);
                            UIHelper.RoundCorners(tlp_cardAcoount, 25);
                            pnl_cardAcoount.BringToFront();

                            pnlwifi.Visible = false;
                            txtcardrecamt.Focus();
                        }
                        else
                        {
                            CMessageBox.Show("No account found for the tapped card. Please try again with a different card.", "Card Not Recognized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            wificard_timer.Start();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page wificard_timer_Tick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public string RemoveSpecialCharacters(string str)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (char c in str)
                {
                    if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                    {
                        sb.Append(c);
                    }
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Order page RemoveSpecialCharacters()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order)); // Replace 'YourFormType' with the appropriate form type
                return string.Empty;
            }
        }
        private void btn_close_cardaccount_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_cardAcoount.Visible = false;
                pnl_main.Enabled = true;
                blinkTimer.Stop();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btn_close_cardaccount_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void blinkTimer_Tick(object sender, EventArgs e)
        {
            lbl_insff_cashbal.Visible = !lbl_insff_cashbal.Visible;
        }
        private void btn_custom_details_Click(object sender, EventArgs e)
        {
            try
            {
                //rb_female,rb_male,label32,label31,label30,label29,label28
                if (pnl_userreg.Visible == false)
                {
                    UIHelper.CenterAndResize(pnl_userreg, 35, 50);
                    UIHelper.RoundCorners(pnl_userreg, 25);
                    UIHelper.RoundCorners(tlp_userreg, 25);
                    UIHelper.RoundCorners(tlp_userreg_nav, 25);
                    pnl_userreg.BringToFront();
                }
                else
                {
                    pnl_userreg.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btn_custom_details_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }
        private void pbwifi_Click(object sender, EventArgs e)
        {
            wificard_timer.Start();
        }
        private void btn_va_close_Click(object sender, EventArgs e)
        {
            try
            {
                // Dispose dynamic controls
                DisposeControls(flp_addons);
                DisposeControls(flp_variation);

                // Reset variables and UI
                pnl_variation_addon.Visible = false;
                Checked_VariationAddon_List.Clear();
                lblsizetotal.Text = "0";
                lbladdontotal.Text = "0";
                pnl_main.Enabled = true;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btn_va_close_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void DisposeControls(FlowLayoutPanel flp)
        {
            foreach (Control ctrl in flp.Controls)
            {
                ctrl.Dispose();
            }
            flp.Controls.Clear();
        }
        private void btn_va_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                // Dispose dynamic controls
                DisposeControls(flp_addons);
                DisposeControls(flp_variation);

                // Reset variables and UI
                pnl_variation_addon.Visible = false;
                Checked_VariationAddon_List.Clear();
                lblsizetotal.Text = "0";
                lbladdontotal.Text = "0";
                pnl_main.Enabled = true;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btn_va_cancel_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public decimal OfferCheckItem(int ProductId, int ProductType)
        {
            try
            {
                BllSearch<DmnMenu> bllSearch = new BllSearch<DmnMenu>();
                BllSearch<DmnOfferMaster> bllSearch1 = new BllSearch<DmnOfferMaster>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblOfferMaster ofm LEFT JOIN tblOfferDetails ofd on ofm.Omid=ofd.Omid and ofd.Status=1 ";
                objSearch.WhereNdGroupBy = " where SYSDATETIME() BETWEEN OfferStartDatetime AND OfferEndDatetime and ofm.Status=1 and  OfferType=0 and ofd.Producttype=" + ProductType + " and ofd.ProductId =" + ProductId;
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
                clsgen.LogException("reservation page OfferCheckItem()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FoodandBeverage.Interfaces.FormType.Reservation));
                return 0;

            }
        }
        private void btn_va_ok_Click(object sender, EventArgs e)
        {
            try
            {
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

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "p.ItemCode as Field1, p.Product as Field2,p.Price as Field3,p.MRP as Field4,p.ProductId as Field5,p.Tax as Field6,p.TaxType as Field7," +
                    "p.Fcid as Field8,p.TaxSettingsId as Field9,p.DiscountPer Field12,pav.Type Field13,pav.Caption Field14," +
                    "pav.Price Field15,a.AddonName Field16,a.Price Field17,pav.Pavid Field18,p.PreparationMode Field19";//,ofd.Discountper as Field10,ofm.offertype Field11
                dmnSearch.Table = "tblProduct p " +
                    " LEFT JOIN tblMenu m on m.ProductId=p.ProductId " +
                    " LEFT JOIN tblProduct_Addon_Variation pav on pav.ProductId=p.ProductId and pav.status=1" +
                    " LEFT JOIN tblAddon a on a.Aid = pav.Aid and a.status = 1"; /*" +
                    " LEFT JOIN tblOfferDetails ofd on ofd.ProductId=p.ProductId and ofd.status=1 " +
                    " LEFT JOIN tblOfferMaster ofm on ofm.Omid=ofd.Omid and ofm.status=1 AND ofm.OfferType=0  AND OfferstartDatetime <= SYSDATETIME() AND OfferEndDatetime >= SYSDATETIME(*/
                dmnSearch.WhereNdGroupBy = "where p.Status=1 and m.Status=1  and p.ProductId= " + Global_ProductId;
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    bool flagexist = true;
                    string vrianame = gridSource
                     .Where(item => Convert.ToInt32(item.Field18) == vid)
                     .Select(item => item.Field14.ToString())
                     .FirstOrDefault();

                    pnl_main.Enabled = true;

                    #region Existing Product
                    decimal totalamt = 0, Addon_amount = 0;
                    for (int i = 0; i < Dgv_Selected_Item.RowCount; i++)
                    {
                        if (Av_ID == Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iavid].Value) && (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value) == Convert.ToInt32(Product_Addon_Variation_Type.Variation)) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                        {
                            int prep = 0;
                            var dataresult = ProductQtyCheckMenuQty(Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value), 0);
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
                            totalamt = Convert.ToDecimal(txtGrandTotal.Text) + Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value);
                            txtGrandTotal.Text = Convert.ToString(totalamt);
                            txtnetamount.Text = Convert.ToString(totalamt);
                            netAmount();
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

                        #region Grid Inseration
                        decimal variation_amt = 0;
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


                        int mainrowno = 0;
                        r1 = Dgv_Selected_Item.RowCount - 1;
                        mainrowno = r1;

                        decimal disper = OfferCheckItem(Convert.ToInt32(Global_ProductId), Convert.ToInt32(Tabletype.Product));
                        if (disper == 0)
                        {
                            Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = gridSource[0].Field12;
                        }
                        else
                        {
                            Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = disper;
                        }
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
                        var Variationprice = Checked_VariationAddon_List.FirstOrDefault(item => item.AVType == Convert.ToInt32(Product_Addon_Variation_Type.Variation));
                        if (Variationprice == null)
                        {
                            Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = 0;
                            Dgv_Selected_Item.Rows[r1].Cells[israte].Value = Convert.ToDecimal(gridSource[0].Field3);
                            Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = Convert.ToDecimal(gridSource[0].Field4);
                        }
                        else
                        {
                            int indexToRemove = Checked_VariationAddon_List.IndexOf(Variationprice);
                            Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = 0;
                            // Dgv_Selected_Item.Rows[r1].Cells[israte].Value = Convert.ToDecimal(Checked_VariationAddon_List[indexToRemove].Price);
                            Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = 0;
                            Dgv_Selected_Item.Rows[r1].Cells[ipavidno].Value = Convert.ToInt32(Checked_VariationAddon_List[indexToRemove].Pavid);
                        }

                        //if (Convert.ToDecimal(gridSource[0].Field10) > 0)
                        //    Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = Convert.ToDecimal(gridSource[0].Field10);
                        //else
                        //    Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = Convert.ToDecimal(gridSource[0].Field12);

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
                        Dgv_Selected_Item.Rows[r1].Cells[iavid].Value = Av_ID;
                        Dgv_Selected_Item.Rows[r1].Cells[ivaraiationstatusno].Value = Convert.ToInt32(Product_Addon_Variation_Type.Variation);
                        Dgv_Selected_Item.Rows[r1].Cells[iAddoncountno].Value = AddonCount;
                        Dgv_Selected_Item.Rows[r1].Cells[ividno].Value = vid;
                        Dgv_Selected_Item.Rows[r1].Cells[iVariatiionNameno].Value = vrianame;
                        Dgv_Selected_Item.Rows[r1].Cells[iPreparationModeno].Value = Convert.ToInt32(gridSource[0].Field13);

                        Dgv_Selected_Item.Rows.Add(1);
                        GridSlno_Refresh();

                        #region Variation or Addon Grid Inseration
                        decimal Total_Addon_Amt = 0;

                        for (int i = 0; i < Checked_VariationAddon_List.Count; i++)
                        {
                            var item = Checked_VariationAddon_List[i];
                            if (item.AVType == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                            {
                                r1 = Dgv_Selected_Item.RowCount - 1;
                                Dgv_Selected_Item.Rows[r1].Cells[islno].Value = i + 1;

                                Dgv_Selected_Item.Rows[r1].Cells[iaction].Value = Properties.Resources.addon_comb_item;
                                Dgv_Selected_Item.Rows[r1].Cells[iaction].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                Dgv_Selected_Item.Rows[r1].Cells[iaction].Style.NullValue = null;
                                ((DataGridViewImageCell)Dgv_Selected_Item.Rows[r1].Cells[iaction]).ImageLayout = DataGridViewImageCellLayout.Zoom;

                                Dgv_Selected_Item.Rows[r1].ReadOnly = true;
                                Dgv_Selected_Item.Rows[r1].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                                Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = 0;
                                Dgv_Selected_Item.Rows[r1].Cells[ibarcode2].Value = 0;
                                Dgv_Selected_Item.Rows[r1].Cells[iname].Value = Convert.ToString(item.Caption);
                                Dgv_Selected_Item.Rows[r1].Cells[israte].Value = Convert.ToDecimal(item.Price);
                                Dgv_Selected_Item.Rows[r1].Cells[iqty].Value = Convert.ToDecimal(item.Qty);
                                Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = 0;
                                Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = 0;
                                Dgv_Selected_Item.Rows[r1].Cells[idisamt].Value = "0";
                                Dgv_Selected_Item.Rows[r1].Cells[iamount].Value = Convert.ToDecimal(item.Price) * Convert.ToDecimal(item.Qty);

                                Dgv_Selected_Item.Rows[r1].Cells[iitemid].Value = Convert.ToInt32(item.Aid);

                                Dgv_Selected_Item.Rows[r1].Cells[itype].Value = 0;
                                Dgv_Selected_Item.Rows[r1].Cells[istatusno].Value = 1;
                                Dgv_Selected_Item.Rows[r1].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
                                Dgv_Selected_Item.Rows[r1].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Secondary);
                                Dgv_Selected_Item.Rows[r1].Cells[itabletype].Value = Convert.ToInt32(Tabletype.Product);

                                Dgv_Selected_Item.Rows[r1].Cells[isubofferid].Value = 0;
                                Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = AddonBGcolor;
                                Dgv_Selected_Item.Rows[r1].Cells[iavid].Value = Av_ID;
                                Dgv_Selected_Item.Rows[r1].Cells[ivaraiationstatusno].Value = Convert.ToInt32(Product_Addon_Variation_Type.Addon);
                                Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value = Convert.ToDecimal(item.Price);
                                Dgv_Selected_Item.Rows[r1].Cells[ipavidno].Value = Convert.ToInt32(item.Pavid);
                                Dgv_Selected_Item.Rows[r1].Cells[iccodeno].Value = Convert.ToString(item.CCode);
                                Dgv_Selected_Item.Rows.Add(1);
                                Total_Addon_Amt = Total_Addon_Amt + Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[iamount].Value);
                            }
                        }

                        Variationprice = Checked_VariationAddon_List.FirstOrDefault(item => item.AVType == Convert.ToInt32(Product_Addon_Variation_Type.Variation));
                        if (Variationprice != null)
                        {
                            int indexToRemove = Checked_VariationAddon_List.IndexOf(Variationprice);
                            Dgv_Selected_Item.Rows[mainrowno].Cells[israte].Value = Convert.ToDecimal(Checked_VariationAddon_List[indexToRemove].Price) + Total_Addon_Amt;
                        }
                        else
                        {
                            Dgv_Selected_Item.Rows[mainrowno].Cells[israte].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[mainrowno].Cells[israte].Value) + Total_Addon_Amt;
                        }

                        totalcellamount(mainrowno);
                        decimal total_Vari_addon = Convert.ToDecimal(Dgv_Selected_Item.Rows[mainrowno].Cells[itotal].Value);
                        Dgv_Selected_Item.Rows[mainrowno].Cells[ioriginalprice].Value = total_Vari_addon;
                        Dgv_Selected_Item.Rows[mainrowno].Cells[itotal].Value = total_Vari_addon;

                        #endregion

                        totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[mainrowno].Cells[itotal].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                        txtGrandTotal.Text = Convert.ToString(totalamt);
                        txtnetamount.Text = Convert.ToString(totalamt);
                        netAmount();

                        #endregion

                        checksave_refresh();
                    }
                    pnl_main.Enabled = true;
                    #endregion
                }
                Checked_VariationAddon_List.Clear();
                pnl_variation_addon.ResumeLayout();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btn_va_ok_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        string TruncateString(string input, int maxLength)
        {
            try
            {
                if (string.IsNullOrEmpty(input)) return input;
                return input.Length <= maxLength ? input : input.Substring(0, maxLength) + "..";
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page TruncateString()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
                return string.Empty;
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
                    pnl_main.Enabled = false;
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
                            Button_Remove.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Button_Remove.Width, Button_Remove.Height, 50, 50));
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
        private void txt_split_recamt_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txt_split_recamt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal receivedAmount;
                if (!decimal.TryParse(txt_split_recamt.Text, out receivedAmount))
                {
                    receivedAmount = 0;
                }
                decimal previousAmount = Convert.ToDecimal(dgv_split.Rows[Split_Rowno].Cells[1].Value);
                decimal balanceAmount = previousAmount - receivedAmount;
                txt_split_balamt.Text = Convert.ToString(balanceAmount);

            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page txt_split_recamt_TextChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void btn_PrintCopy_Click(object sender, EventArgs e)
        {
            BillCopy_Status = true;
            paybillcal(0);
        }
        private void FrmOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DisposeControls(flp_addons);
                DisposeControls(flp_variation);

                DisposeTimer(tm_memory);
                DisposeTimer(blinkTimer);
                DisposeTimer(wificard_timer);

                if (FrmDashboard.AutoLogoutWhenIdle)
                    AppSession.UnregisterForm(this);

                if (Application.OpenForms["FrmTableView"] != null)
                {
                    var frm = Application.OpenForms["FrmTableView"] as FrmTableView;
                    frm.BringToFront();
                    frm.Dock = DockStyle.Fill;
                }
            }
            catch
            { 

            }
        }
        private void DisposeTimer(System.Windows.Forms.Timer timer)
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }
        }

        private void txtcardrecamt_KeyPress(object sender, KeyPressEventArgs e)
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
        private void btn_serach_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtname_search.Text != null && txtname_search.Text != "" && txtname_search.Text != "Food Name .....")
                {
                    ProductNameSearch();
                }
                else if (txtitemcode_serach.Text != null && txtitemcode_serach.Text != "" && txtitemcode_serach.Text != "Item Code .....")
                {
                    ProductItemcodeSearch();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_serach_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void btn_promo_reset_Click(object sender, EventArgs e)
        {
            txtdisamt_promo.Text = "";
            txtdiscountamt_promo.Text = "0";
            txtpromocode.Text = "0";
            btn_promo_reset.Visible = false;
            txtpromocode.Enabled = true;
        }
        private void cmb_tblcode_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cmb_tblcode.SelectedIndex = 0;
        }
        private void txtpkchrge_amt_KeyPress(object sender, KeyPressEventArgs e)
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
        private void txtpkchrge_amt_TextChanged(object sender, EventArgs e)
        {
            if (txtpkchrge_amt.Text != "." && txtpkchrge_amt.Text != "")
            {
                netAmount();
            }

        }
        private void txtpkchrge_amt_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal value;
                if (!decimal.TryParse(txtpkchrge_amt.Text, out value))
                {
                    txtpkchrge_amt.Text = "0";
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page txtpkchrge_amt_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void txtpaidamount_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal value;
                if (!decimal.TryParse(txtpaidamount.Text, out value))
                {
                    txtpaidamount.Text = "0";
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page txtpaidamount_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void FrmOrder_Resize(object sender, EventArgs e)
        {
            maxColumns = columncount;
            spacing = Productspace;
            containerWidth = flp_productview.ClientSize.Width;
            tileW = (int)(((containerWidth - ((maxColumns + 1) * spacing)) / maxColumns) * Productwidth);
            tileH = (int)(tileW * Productheight);

            UIHelper.RoundCorners(pnl_product_calc, 25, null);
            UIHelper.RoundCorners(tlp_footer, 25, null);
            UIHelper.RoundCorners(pnl_productview, 25, null);
            UIHelper.RoundCorners(pnl_categorymenu, 25, null);
            UIHelper.RoundCorners(tlp_product_haeder_main, 25, null);
        }
        private void tm_memory_Tick(object sender, EventArgs e)
        {
            Process currentProcess = Process.GetCurrentProcess();
            long memoryInBytes = currentProcess.PrivateMemorySize64;
            double memoryInMB = memoryInBytes / (1024.0 * 1024.0);
            lblmemory.Text = $"Memory: {memoryInMB:F2} MB";
        }
        private void btn_pcate_more_Click(object sender, EventArgs e)
        {
            foreach (Control ctrl in flp_product_haeder.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Visible = !btn.Visible;
                }
            }

            if (!isCategoryExpanded)
            {
                btn_pcate_more.BackgroundImage = Properties.Resources.categ_back;
                isCategoryExpanded = true;
            }
            else
            {
                btn_pcate_more.BackgroundImage = Properties.Resources.categ_next;
                isCategoryExpanded = false;
            }
        }
        private void btn_promocls_Click(object sender, EventArgs e)
        {
            pnl_promocode.Visible = false;
        }
        private void btn_userreg_cls_Click(object sender, EventArgs e)
        {
            pnl_userreg.Visible = false;
        }
        private void btnwificancel_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_main.Enabled = true;
                pnlwifi.Visible = false;
                blinkTimer.Stop();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btnwificancel_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void btn_orderdesc_submit_Click(object sender, EventArgs e)
        {
            pnl_order_desc.Visible = false;
        }
        private void Dgv_Selected_Item_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex == iname)
                {
                    saletype = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[itabletype].Value);
                    txt_product_des.Text = "Description Type Here .....";
                    txt_product_des.ForeColor = Color.Gray;

                    UIHelper.CenterAndResize(pnl_products_descr, 25, 25);
                    UIHelper.RoundCorners(tlp_products_descr_main, 25);
                    UIHelper.RoundCorners(pnl_products_descr, 25);
                    UIHelper.RoundCorners(tlp_product_desc_nav, 25);

                    if (saletype == Convert.ToInt32(Tabletype.DirectSale))
                    {
                        Proid_Prid = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[ipridno].Value);
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (Proid_Prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value) && Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value) != null && Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value) != "" && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.DirectSale))
                            {
                                txt_product_des.Text = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value);
                                txt_product_des.ForeColor = Color.Black;
                                break;
                            }
                        }
                    }
                    else if (saletype == Convert.ToInt32(Tabletype.ComboProduct))
                    {
                        Proid_Prid = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[iitemid].Value);
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (Proid_Prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value) != null && Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value) != "" && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.ComboProduct))
                            {
                                txt_product_des.Text = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value);
                                txt_product_des.ForeColor = Color.Black;
                                break;
                            }
                        }
                    }
                    else if (saletype == Convert.ToInt32(Tabletype.Product))//Product Items
                    {
                        Proid_Prid = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[iitemid].Value);
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (Proid_Prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value) != null && Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value) != "")
                            {
                                txt_product_des.Text = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value);
                                txt_product_des.ForeColor = Color.Black;
                                break;
                            }
                        }
                    }
                    else if (saletype == Convert.ToInt32(Tabletype.Variation_Addon))//Product wth addon Items
                    {
                        addon_Vari_Id = Convert.ToString(Dgv_Selected_Item.Rows[e.RowIndex].Cells[iavid].Value);
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (addon_Vari_Id == Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iavid].Value) && Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value) != null && Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value) != "")
                            {
                                txt_product_des.Text = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iprodesno].Value);
                                txt_product_des.ForeColor = Color.Black;
                                break;
                            }
                        }
                    }
                    lblheader_product_des.Text = Convert.ToString(Dgv_Selected_Item.Rows[e.RowIndex].Cells[iname].Value);

                }
                if (e.ColumnIndex == iqty)
                {
                    decimal totalamt = 0;
                    saletype = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[itabletype].Value);
                    decimal kdata = 0;
                    using (UserControl.Frmnumdiskeypad frm = new UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.NoDecimal)))
                    {
                        frm.ShowDialog();
                        kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                    }
                    if (kdata == 0)
                        return;

                    int prid = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[ipridno].Value);
                    int pid = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[iitemid].Value);
                    string Av_ID = Convert.ToString(Dgv_Selected_Item.Rows[e.RowIndex].Cells[iavid].Value);

                    if (saletype == Convert.ToInt32(Tabletype.DirectSale))
                    {
                        #region Existing Product
                        int rowno = 0;
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.DirectSale))
                            {
                                //int availableQty = ProductQtyCheckMenuQty(Convert.ToInt32(pid), 0);

                                //// 1. Out-of-stock check
                                //if (availableQty <= 0)
                                //{
                                //    MessageBox.Show("Stock Out! This item is not available.",
                                //                    "Out of Stock",
                                //                    MessageBoxButtons.OK,
                                //                    MessageBoxIcon.Warning);
                                //    return;
                                //}

                                //// 2. Entered qty exceeds available stock
                                //if (kdata > availableQty)
                                //{
                                //    MessageBox.Show($"Only {availableQty} Qty Available.",
                                //                    "Stock Warning",
                                //                    MessageBoxButtons.OK,
                                //                    MessageBoxIcon.Warning);
                                //    return;
                                //}
                                decimal cqoh = kdata;
                                if (cqoh > Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value))
                                {
                                    CMessageBox.Show("Aviablle Quantity is : " + Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value), "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }

                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = kdata;
                                totalcellamount(i);
                                rowno = i;
                                break;
                            }
                        }
                        decimal oldTotal = Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[itotal].Value); // OLD row total
                        Dgv_Selected_Item.Rows[rowno].Cells[iqty].Value = kdata;
                        totalcellamount(rowno);
                        decimal newTotal = Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[itotal].Value);
                        decimal grand = Convert.ToDecimal(txtGrandTotal.Text);
                        grand = grand - oldTotal + newTotal;
                        txtGrandTotal.Text = grand.ToString();
                        txtnetamount.Text = grand.ToString();
                        netAmount();
                        #endregion
                    }
                    else if (saletype == Convert.ToInt32(Tabletype.ComboProduct))
                    {
                        #region Existing combo product
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (pid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.ComboProduct) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                            {
                                int prep = 0;
                                var dataresult = ProductQtyCheckMenuQty(Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value), 0);
                                int availableQty = dataresult.Quantity;
                                prep = dataresult.PreparationMode;
                                if (availableQty <= 0 && prep == Convert.ToInt32(PreparationType.ReadyFood))
                                {
                                    MessageBox.Show("Stock Out! This item is not available.",
                                                    "Out of Stock",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Warning);
                                    return;
                                }

                                // 2. Entered qty exceeds available stock
                                if (kdata > availableQty && prep == Convert.ToInt32(PreparationType.ReadyFood))
                                {
                                    MessageBox.Show($"Only {availableQty} Qty Available.",
                                                    "Stock Warning",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Warning);
                                    return;
                                }

                                decimal oldTotal = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value); // OLD row total
                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = kdata;
                                totalcellamount(i);
                                decimal newTotal = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);
                                decimal grand = Convert.ToDecimal(txtGrandTotal.Text);
                                grand = grand - oldTotal + newTotal;
                                txtGrandTotal.Text = grand.ToString();
                                txtnetamount.Text = grand.ToString();

                                netAmount();
                                break;
                            }
                        }
                        #endregion
                    }
                    else if (saletype == Convert.ToInt32(Tabletype.Product))
                    {
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (pid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.Product) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                            {
                                int prep = 0;
                                var dataresult = ProductQtyCheckMenuQty(Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value), 0);
                                int availableQty = dataresult.Quantity;
                                prep = dataresult.PreparationMode;
                                if (availableQty <= 0 && prep == Convert.ToInt32(PreparationType.ReadyFood))
                                {

                                    MessageBox.Show("Stock Out! This item is not available.",
                                                        "Out of Stock",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Warning);
                                        return;
                                    
                                }

                                if (kdata > availableQty && prep == Convert.ToInt32(PreparationType.ReadyFood))
                                {
                                    MessageBox.Show($"Only {availableQty} Qty Available.",
                                                    "Stock Warning",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Warning);
                                    return;
                                }

                                decimal oldTotal = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value); // OLD row total
                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = kdata;
                                totalcellamount(i);
                                decimal newTotal = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);
                                decimal grand = Convert.ToDecimal(txtGrandTotal.Text);
                                grand = grand - oldTotal + newTotal;
                                txtGrandTotal.Text = grand.ToString();
                                txtnetamount.Text = grand.ToString();

                                netAmount();
                                break;
                            }
                        }
                    }
                    else if (saletype == Convert.ToInt32(Tabletype.Variation_Addon))
                    {
                        #region Existing Product
                        decimal Addon_amount = 0;
                        for (int i = 0; i < Dgv_Selected_Item.RowCount; i++)
                        {
                            if (Av_ID == Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iavid].Value) && (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value) == Convert.ToInt32(Product_Addon_Variation_Type.Variation)) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                            {
                                int prep = 0;
                                var dataresult = ProductQtyCheckMenuQty(Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value), 0);
                                int availableQty = dataresult.Quantity;
                                prep = dataresult.PreparationMode;
                                if (availableQty <= 0 && prep == Convert.ToInt32(PreparationType.ReadyFood))
                                {
                                    MessageBox.Show("Stock Out! This item is not available.",
                                                    "Out of Stock",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Warning);
                                    return;
                                }

                                // 2. Entered qty exceeds available stock
                                if (kdata > availableQty && prep == Convert.ToInt32(PreparationType.ReadyFood))
                                {
                                    MessageBox.Show($"Only {availableQty} Qty Available.",
                                                    "Stock Warning",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Warning);
                                    return;
                                }

                                decimal oldTotal = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value); // OLD row total
                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = kdata;
                                totalcellamount(i);
                                decimal newTotal = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);
                                decimal grand = Convert.ToDecimal(txtGrandTotal.Text);
                                grand = grand - oldTotal + newTotal;
                                txtGrandTotal.Text = grand.ToString();
                                txtnetamount.Text = grand.ToString();

                                netAmount();
                                break;
                            }
                        }
                        #endregion
                    }
                }
                else if (e.ColumnIndex == iaction)
                {
                    int rowType = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[itabletype].Value);
                    int pid = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[iitemid].Value);
                    decimal qty = Convert.ToDecimal(Dgv_Selected_Item.Rows[e.RowIndex].Cells[iqty].Value);
                    int vid = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[ividno].Value);
                    string avid = Convert.ToString(Dgv_Selected_Item.Rows[e.RowIndex].Cells[iavid].Value);
                    int prid = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[ipridno].Value);
                    int addonitem = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[ivaraiationstatusno].Value);

                    if (rowType == Convert.ToInt32(Tabletype.DirectSale))
                    {
                        decimal TotalPrice = 0;
                        int rowno = 0;
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) == 1 && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.DirectSale) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                            {
                                rowno = i;
                                break;
                            }
                        }
                        TotalPrice = Convert.ToDecimal(Dgv_Selected_Item.Rows[rowno].Cells[ioriginalprice].Value) * Convert.ToInt32(Dgv_Selected_Item.Rows[rowno].Cells[iqty].Value);
                        decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) - TotalPrice;
                        txtGrandTotal.Text = Convert.ToString(totalamt);
                        Dgv_Selected_Item.Rows.RemoveAt(rowno);
                    }
                    else if (rowType == Convert.ToInt32(Tabletype.ComboProduct))
                    {
                        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "ofm.omid id , ofm.OfferName Field1 ,ofm.Discountper Field2, ofm.tax Field3 ,ofm.taxtype Field4,ofm.TaxSettingsId Field5,ofm.FinalPrice Field6,ofd.Productname Field7";
                        dmnSearch.Table = "tblOfferMaster ofm LEFT JOIN tblOfferDetails ofd on ofd.Omid=ofm.Omid";
                        dmnSearch.WhereNdGroupBy = "where ofm.Status=1 and ofd.Omid = " + pid;
                        dmnSearch.OrderBy = "";
                        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSource.Count > 0)
                        {
                            if (Booking_Status == false)
                            {
                                if (string.IsNullOrEmpty(txtGrandTotal.Text))
                                    txtGrandTotal.Text = "0";
                            }
                            int n = Dgv_Selected_Item.RowCount - 1;
                            for (int i = 0; i < n; i++)
                            {
                                if (pid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) == 1 && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.ComboProduct) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                                {
                                    decimal TotalPrice = Convert.ToDecimal(Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value)) * Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value);
                                    decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) - TotalPrice;
                                    txtGrandTotal.Text = Convert.ToString(totalamt);
                                    txtnetamount.Text = Convert.ToString(totalamt);
                                    Dgv_Selected_Item.Rows.RemoveAt(i);
                                    break;
                                }

                            }
                            for (int i = n - 1; i >= 0; i--)
                            {
                                if (pid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[isubofferid].Value))
                                    Dgv_Selected_Item.Rows.RemoveAt(i);
                            }
                            //checksave_refresh();
                        }
                    }
                    else if (rowType == Convert.ToInt32(Tabletype.Product))
                    {
                        if (addonitem == Convert.ToInt32(Product_Addon_Variation_Type.Variation) || addonitem == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                        {
                            #region Addon or Variation 
                            if (Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[iMainProductStatusno].Value) == Convert.ToInt32(MainProductStatus.Primary))
                            {
                                int dgvcount = Dgv_Selected_Item.RowCount - 1;
                                for (int i = dgvcount; i >= 0; i--)
                                {
                                    if (avid == Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iavid].Value))
                                    {
                                        if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value) == Convert.ToInt32(Product_Addon_Variation_Type.Variation) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                                        {
                                            decimal TotalPrice = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value) * Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value);
                                            decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) - TotalPrice;
                                            txtGrandTotal.Text = Convert.ToString(totalamt);
                                            txtnetamount.Text = Convert.ToString(totalamt);
                                            Dgv_Selected_Item.Rows.RemoveAt(i);
                                        }
                                        else
                                        {
                                            Dgv_Selected_Item.Rows.RemoveAt(i);
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            #region Normal Product
                            for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                            {
                                if (pid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) == 1 && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.Product) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                                {
                                    decimal TotalPrice = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value) * Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value);
                                    decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) - TotalPrice;
                                    txtGrandTotal.Text = Convert.ToString(totalamt);
                                    txtnetamount.Text = Convert.ToString(totalamt);
                                    Dgv_Selected_Item.Rows.RemoveAt(i);
                                    Dgv_Selected_Item.Update();
                                    break;
                                }
                            }
                            #endregion
                        }
                        checksave_refresh();
                    }

                    netAmount();
                    GridSlno_Refresh();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page Dgv_Selected_Item_CellClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void Dgv_Selected_Item_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                e.CellStyle.ForeColor = Color.Black;
            }
            if (e.RowIndex == -1)
            {
                e.AdvancedBorderStyle.Top = DataGridViewAdvancedCellBorderStyle.None;
                e.AdvancedBorderStyle.Bottom = DataGridViewAdvancedCellBorderStyle.None;
                e.AdvancedBorderStyle.Left = DataGridViewAdvancedCellBorderStyle.None;
                e.AdvancedBorderStyle.Right = DataGridViewAdvancedCellBorderStyle.None;
                e.PaintBackground(e.CellBounds, true);
                e.PaintContent(e.CellBounds);
                e.Handled = true;
            }
        }
        private void btn_pa__toggle_Click(object sender, EventArgs e)
        {
            if (!isOperationShown)
            {
                textb_shape_paymnet();
                var c = tlp_netalc_buttons.GetControlFromPosition(0, 0);
                if (c != null) tlp_netalc_buttons.Controls.Remove(c);

                tlp_netalc_buttons.Controls.Add(flp_oper_buttons, 0, 0);
                flp_oper_buttons.Dock = DockStyle.Fill;
                flp_oper_buttons.Visible = true;
                isOperationShown = true;
            }
            else
            {
                if (tlp_netalc_buttons.Controls.Contains(flp_oper_buttons))
                    tlp_netalc_buttons.Controls.Remove(flp_oper_buttons);
                flp_oper_buttons.Visible = false;
                tlp_netalc_buttons.Controls.Add(tlp_calculation_total, 0, 0);
                tlp_calculation_total.Dock = DockStyle.Fill;

                isOperationShown = false;
            }
        }
        private void btn_orderdes_cls_Click(object sender, EventArgs e)
        {
            pnl_order_desc.Visible = false;
        }
        #endregion

        #region Memory
        public void Memoryrelease()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        #endregion

        #region keypress & keydown 
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
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:

                    if (pnl_variation_addon.Visible)
                    {
                        pnl_main.Enabled = true;
                        pnl_variation_addon.Visible = false;
                    }
                    else if (pnl_products_descr.Visible)
                    {
                        pnl_main.Enabled = true;
                        pnl_products_descr.Visible = false;
                    }
                    else if (pnlproductviewmore.Visible)
                    {
                        pnl_main.Enabled = true;
                        pnlproductviewmore.Visible = false;
                    }
                    else if (pnl_gridview.Visible)
                    {
                        pnl_gridview.Visible = false;
                        pnl_main.Enabled = true;
                    }
                    else if (pnl_tableprice.Visible)
                    {
                        pnl_tableprice.Visible = false;
                        pnl_main.Enabled = true;
                    }
                    else if (pnl_split.Visible)
                    {
                        if (btn_con_nosplit.Visible == false)
                        {
                            int count = 0;
                            for (int i = 0; i < dgv_split.RowCount; i++)
                            {
                                if (Convert.ToInt32(dgv_split.Rows[i].Cells[6].Value) == 2 && Convert.ToInt32(dgv_split.Rows[i].Cells[4].Value) > 0)
                                    count++;
                            }
                            if (count < dgv_split.RowCount && btn_split_number.Visible)
                            {
                                CMessageBox.Show("Closing not possible. Pending bills still exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            if (count == 0 && BillList.Count == 0 && btn_itemwise.Visible)
                            {
                                pnl_split.Visible = false;

                                UIHelper.CenterAndResize(tlp_payment_option, 40, 45);
                                UIHelper.RoundCorners(tlp_payment_option, 25);
                                btn_con_nosplit.Visible = true;

                                btn_itemwise.Visible = true;
                                btn_split_number.Visible = true;

                                Control controlToRemove = tlp_main_split.GetControlFromPosition(0, 4);
                                tlp_main_split.Controls.Remove(controlToRemove);
                                tlp_numberwise.Visible = false;

                            }
                            else if (count < dgv_split.RowCount && btn_itemwise.Visible)
                            {
                                CMessageBox.Show("Closing not possible. Pending bills still exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            pnl_split.Visible = false;

                            UIHelper.CenterAndResize(tlp_payment_option, 40, 45);
                            UIHelper.RoundCorners(tlp_payment_option, 25);

                            btn_con_nosplit.Visible = true;


                            btn_itemwise.Visible = true;
                            btn_split_number.Visible = true;

                            Control controlToRemove = tlp_main_split.GetControlFromPosition(0, 4);
                            tlp_main_split.Controls.Remove(controlToRemove);
                            tlp_numberwise.Visible = false;
                        }
                    }
                    else if (pnl_split_itemwise.Visible)
                    {
                        pnl_split_itemwise.Visible = false;
                        pnl_main.Enabled = true;
                    }
                    else if (tlp_payment_option.Visible)
                    {
                        tlp_payment_option.Visible = false;
                        pnl_main.Enabled = true;
                    }
                    else if (pnl_partpayment.Visible)
                    {
                        pnl_partpayment.Visible = false;
                        pnl_main.Enabled = true;
                    }
                    else
                    {
                        ResetBillpage();

                        this.Close();
                        this.Dispose();
                    }
                    return true;

                case Keys.F4:
                    Button btnfv = this.Controls.Find("btn_fav", true).FirstOrDefault() as Button;
                    if (btnfv != null)
                        btnfv.PerformClick();
                    return true;
                case Keys.F6:
                    Button btnof = this.Controls.Find("btn_offer", true).FirstOrDefault() as Button;
                    if (btnof != null)
                        btnof.PerformClick();
                    return true;
                case Keys.F7:
                    Button btnco = this.Controls.Find("btn_combo_offer", true).FirstOrDefault() as Button;
                    if (btnco != null)
                        btnco.PerformClick();
                    return true;

                case Keys.F9:                   
                    btn_savekot_print.PerformClick();
                    return true;
                case Keys.F11:
                    btn_print_bill.PerformClick();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        private void btn_sleteted_list_gridview_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnl_gridview.Visible == false)
                {
                    UIHelper.CenterAndResize(pnl_gridview, 80, 60);
                    UIHelper.RoundCorners(pnl_gridview, 25);
                    UIHelper.RoundCorners(tlp_gridview_nav, 25);
                    UIHelper.RoundCorners(tlp_productgridview, 25);
                }
                else
                {
                    pnl_gridview.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btn_save_product_des_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
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

        private void btndirectpaybill_Click(object sender, EventArgs e)
        {
            if (MandatoryCheck_KOT())
            {
                DirectPayBill = true;
                var controlToRemove = tlp_payment_option.GetControlFromPosition(2, 0);
                tlp_payment_option.Controls.Remove(controlToRemove);
                controlToRemove = tlp_payment_option.GetControlFromPosition(3, 0);
                tlp_payment_option.Controls.Remove(controlToRemove);
                controlToRemove = tlp_payment_option.GetControlFromPosition(2, 1);
                tlp_payment_option.Controls.Remove(controlToRemove);
                controlToRemove = tlp_payment_option.GetControlFromPosition(3, 1);
                tlp_payment_option.Controls.Remove(controlToRemove);

                tlp_payment_option.Controls.Add(btncreditcard, 2, 0);
                tlp_payment_option.Controls.Add(btnnetbanking, 3, 0);
                tlp_payment_option.Controls.Add(Btnpartpayment, 2, 1);
                tlp_payment_option.Controls.Add(btn_splitpayment, 3, 1);

                btncreditcard.Visible = true;
                btnnetbanking.Visible = true;
                btn_splitpayment.Visible = true;
                Btnpartpayment.Visible = true;

                btncreditcard.Dock = DockStyle.Fill;
                btnnetbanking.Dock = DockStyle.Fill;
                btn_splitpayment.Dock = DockStyle.Fill;
                Btnpartpayment.Dock = DockStyle.Fill;

                UIHelper.CenterAndResize(tlp_payment_option, 40, 45);
                UIHelper.RoundCorners(tlp_payment_option, 25);

                pnl_main.Enabled = false;
            }
        }

        #region Cancel Order
        private void btn_cancle_pickup_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr = MessageBox.Show(
                    "Are you sure you want to cancel this order?",
                    "Confirm Cancel",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                if (dr != DialogResult.Yes)
                {
                    return;  
                }

                frmExtraAccessChk frm = new frmExtraAccessChk(Convert.ToInt32(formsenum.ExtraLoginAccess));
                frm.ShowDialog();
                if (frmExtraAccessChk.access == true)
                {

                   

                    ttxdesc_cancelorder.Text = "Reason Type Here ...";
                    ttxdesc_cancelorder.ForeColor = Color.Gray;
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "SalesMasterId Field1,NetAmount Field2";
                    dmnSearch.Table = "tblSalesMaster";
                    dmnSearch.WhereNdGroupBy = "where  OrderMasterId = " + tableId;
                    dmnSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    if (gridSource.Count > 0)
                    {
                        refundsmid = Convert.ToInt32(gridSource[0].Field1);
                        refundtotal = Convert.ToDecimal(gridSource[0].Field2);
                    }
                    lblcanceltextorder.Text = $"Total Cash refund amount is {refundtotal}";
                    UIHelper.CenterAndResize(pnl_cancelorder, 25, 35);
                    UIHelper.RoundCorners(tlp_cancelorder, 25);
                    UIHelper.RoundCorners(pnl_cancelorder, 25);
                    UIHelper.RoundCorners(tlpnav_cancelorder, 25);
                    ApplyRoundedTextBox(ttxdesc_cancelorder, 10);
                }
                else
                {
                    CMessageBox.Show(this, "\nThis Account Have No Access For This Operations", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_cancle_pickup_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }


        private void ttxdesc_cancelorder_Leave(object sender, EventArgs e)
        {
            if (ttxdesc_cancelorder.Text == "")
            {
                ttxdesc_cancelorder.Text = "Reason Type Here ...";
                ttxdesc_cancelorder.ForeColor = Color.DarkGray;
            }
        }

        private void ttxdesc_cancelorder_Enter(object sender, EventArgs e)
        {
            if (ttxdesc_cancelorder.Text == "Reason Type Here ...")
            {
                ttxdesc_cancelorder.Text = "";
                ttxdesc_cancelorder.ForeColor = Color.Black;
            }
        }

        #endregion



        private void btnsub_cancelorder_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (ttxdesc_cancelorder.Text.Trim() == "" ||
        ttxdesc_cancelorder.Text == "Reason Type Here ...")
                {
                    MessageBox.Show("Please enter a reason for cancelling the order!",
                                    "Reason Required",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);

                    ttxdesc_cancelorder.Focus();
                    return;
                }
                pnl_cancelorder.Visible = false;
                string reason = ttxdesc_cancelorder.Text.Trim();
                
                DmnCancelOrder cancelod = new DmnCancelOrder();
                BllCancelOrder bllcancelod = new BllCancelOrder();
                cancelod.SalesMasterId = refundsmid;
                cancelod.OrderMasterid = tableId;
                cancelod.BranchId = FrmDashboard.BranchId;
                cancelod.CreatedBy = FrmDashboard.UserId;
                cancelod.CancelReason = reason;
                var InsertStatusP = bllcancelod.CancelOrder("prdCancelOrder", cancelod);

                DmnRefund dmnRefund = new DmnRefund();
                BllRefund bllRefund = new BllRefund();

                dmnRefund.SalesMasterId = Convert.ToInt32(txtsalemasterid.Text);
                dmnRefund.OrderMasterId = Convert.ToInt32(txtordermasterid.Text);
                dmnRefund.RefundpayType = 1;
                dmnRefund.RefundReason = 5;
                dmnRefund.ReasonNote = "staff order Cancel";
                dmnRefund.CashRefund = refundtotal;

                //// Balances
                //dmnRefund.CashBalance = Convert.ToDecimal(lblCashBal.Text);
                //dmnRefund.BonusBalance = Convert.ToDecimal(lblBonusBal.Text);
                //dmnRefund.PointBalnce = Convert.ToDecimal(lblPointBal.Text);  // Correct spelling
                //dmnRefund.TokenBalance = Convert.ToDecimal(lblTokenBal.Text);

                //// Refund amounts
                //dmnRefund.CashBalanceRefund = dmnRefund.CashRefund;
                //dmnRefund.BonusRefund = Convert.ToDecimal(lblReBonusBal.Text);
                //dmnRefund.PointRefund = Convert.ToDecimal(lblRePointBal.Text);
                //dmnRefund.TokenRefund = Convert.ToDecimal(lblReTokenBal.Text);

                dmnRefund.CreatedBy = FrmDashboard.UserId;
                dmnRefund.BranchId = FrmDashboard.BranchId;
                dmnRefund.Status = 1;
                //dmnRefund.CreatedDateTime = DateTime.Now;
                var data = bllRefund.AddRefund("prdInsertRefund", dmnRefund);

                if (InsertStatusP.SalesMasterId > 0)
                    MessageBox.Show("Order cancelled successfully!", "Cancelled");
                clsgen.Memoryrelease();

                if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                    ResetTableViewPage();
                else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                    ResetPickupViewPage();

                ResetBillpage();
                this.Close();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btnsub_cancelorder_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }

        private void btn_cancelorder_Click(object sender, EventArgs e)
        {
            pnl_cancelorder.Visible = false;
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
        public void CheckPickupkitchenCancel()
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
                        Refundproductflag = true;
                    }
                    else
                    {
                        btnreconfirmorder.Visible = false;
                        Refundproductflag = false;
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
        private void btnordercallist_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnl_cancelledlist.Visible == false)
                {
                    UIHelper.CenterAndResize(pnl_cancelledlist, 50, 40);
                    UIHelper.RoundCorners(pnl_cancelledlist, 25);
                    UIHelper.RoundCorners(tlp_cancelled_list_nav, 25);
                    CheckPickupkitchenCancel();
                }
                else
                {
                    pnl_cancelledlist.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btnordercallist_Click()", ex,FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void btn_cancel_list_Click(object sender, EventArgs e)
        {
            pnl_cancelledlist.Visible = false;
        }

        private void dgv_ordercancel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0)
                    return;
                if (e.ColumnIndex == cnactionno)
                {
                    int orderDetailsId = Convert.ToInt32(dgv_ordercancel.Rows[e.RowIndex].Cells[cnorderdetailsidno].Value);
                    int ordermasterId = Convert.ToInt32(dgv_ordercancel.Rows[e.RowIndex].Cells[cnordermasteridno].Value);
                    DialogResult dr = MessageBox.Show(
                        "Are you sure you want to notify this cancel item?",
                        "Confirm",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                    if (dr == DialogResult.Yes)
                    {
                        DmnOrderMaster dmnRemove_order = new DmnOrderMaster();
                        BllOrder bll_order = new BllOrder();
                        dmnRemove_order.OrderMasterId = ordermasterId;
                        dmnRemove_order.OrderDetailsId = orderDetailsId;
                        dmnRemove_order.CancelNotifyStatus = 0; 
                        dmnRemove_order.CreatedBy = FrmDashboard.UserId;
                        dmnRemove_order.BranchId = FrmDashboard.BranchId;
                        var InsertStatusP = bll_order.CancelNotifyStatus_update("prd_CancelNotifyStatus_update", dmnRemove_order);
                        dgv_ordercancel.Rows.RemoveAt(e.RowIndex);
                        if (dgv_ordercancel.Rows.Count == 0)
                            pnl_cancelorder.Visible = false;

                        MessageBox.Show("Cancel item notification confirmed!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page dgv_ordercancel_CellClick()", ex,FrmLogin.EmployeName, FrmLogin.Designation,Convert.ToInt32(FormType.Order));
            }
        }

        private void btn_ktcancelrefund_Click(object sender, EventArgs e)
        {
            pnl_cancelledlist.Visible = false;
        }

        private void btnktcancelrefund_Click(object sender, EventArgs e)
        {
            DmnRefund dmnRefund = new DmnRefund();
            BllRefund bllRefund = new BllRefund();

            dmnRefund.SalesMasterId = Convert.ToInt32(txtsalemasterid.Text);
            dmnRefund.OrderMasterId = Convert.ToInt32(txtordermasterid.Text);
            dmnRefund.RefundpayType = 1;
            dmnRefund.RefundReason = 5;
            dmnRefund.ReasonNote = "Kitchen staff Cancel";
            dmnRefund.CashRefund = Convert.ToDecimal(lblpickupcnlamt.Text);

            //// Balances
            //dmnRefund.CashBalance = Convert.ToDecimal(lblCashBal.Text);
            //dmnRefund.BonusBalance = Convert.ToDecimal(lblBonusBal.Text);
            //dmnRefund.PointBalnce = Convert.ToDecimal(lblPointBal.Text);  // Correct spelling
            //dmnRefund.TokenBalance = Convert.ToDecimal(lblTokenBal.Text);

            //// Refund amounts
            //dmnRefund.CashBalanceRefund = dmnRefund.CashRefund;
            //dmnRefund.BonusRefund = Convert.ToDecimal(lblReBonusBal.Text);
            //dmnRefund.PointRefund = Convert.ToDecimal(lblRePointBal.Text);
            //dmnRefund.TokenRefund = Convert.ToDecimal(lblReTokenBal.Text);

            dmnRefund.CreatedBy = FrmDashboard.UserId;
            dmnRefund.BranchId = FrmDashboard.BranchId;
            dmnRefund.Status = 1;
            dmnRefund.CreatedDateTime = DateTime.Now;
            var data = bllRefund.AddRefund("prdInsertRefund", dmnRefund);

            DmnOrderMaster dmnRemove_order = new DmnOrderMaster();
            BllOrder bll_order = new BllOrder();
            dmnRemove_order.OrderMasterId = Convert.ToInt32(txtordermasterid.Text); 
            dmnRemove_order.OrderDetailsId = 0;
            dmnRemove_order.CancelNotifyStatus = 0;
            dmnRemove_order.PkId = 1;
            dmnRemove_order.CreatedBy = FrmDashboard.UserId;
            dmnRemove_order.BranchId = FrmDashboard.BranchId;
            var InsertStatusP = bll_order.CancelNotifyStatus_update("prd_CancelNotifyStatus_update", dmnRemove_order);
        }

        private void btnreconfirmorder_Click(object sender, EventArgs e)
        {
            DmnOrderMaster dmnRemove_order = new DmnOrderMaster();
            BllOrder bll_order = new BllOrder();
            dmnRemove_order.OrderMasterId = Convert.ToInt32(txtordermasterid.Text);
            dmnRemove_order.OrderDetailsId = 0;
            dmnRemove_order.KitchenCancelStatus = 0;
            dmnRemove_order.PkId = 1;
            dmnRemove_order.CreatedBy = FrmDashboard.UserId;
            dmnRemove_order.BranchId = FrmDashboard.BranchId;
            var InsertStatusP = bll_order.Reconfirmorder_update("prd_KitchenCancelStatus_update", dmnRemove_order);

            if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                ResetTableViewPage();
            else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                ResetPickupViewPage();
            else if (sourcepage == Convert.ToInt32(SourcepageType.Billingpage))
                ResetBillpage();
            this.Close();
        }

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
        #endregion

        #region Dispose

        #endregion

    }
}
