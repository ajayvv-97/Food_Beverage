using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CustomMessageBox;
using Domain;
using FastMember;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControl;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace FoodandBeverage.Forms
{
    public partial class FrmOrderConsole : Form
    {
        #region Declaration

        public int tableId = 0;
        public int Bookingmasterid = 0;
        public int BookingType = 0;
        public int OrderTypeId = 0;
        public int StockDeductionOrderTypeId = 0;
        public const int TakeawayBookingType = 1001;
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

        public string GameDB = Convert.ToString(ConfigurationManager.AppSettings["GameDB"]);
        public int columncount = Convert.ToInt32(ConfigurationManager.AppSettings["OrderPageProductViewRow"]);
        public decimal Productheight = Convert.ToDecimal(ConfigurationManager.AppSettings["Productheight"]);
        public decimal Productwidth = Convert.ToDecimal(ConfigurationManager.AppSettings["Productwidth"]);
        public int Productcorner = Convert.ToInt32(ConfigurationManager.AppSettings["CornerRadius"]);
        public int Productspace = Convert.ToInt32(ConfigurationManager.AppSettings["Productspace"]);

        public int columncount_offer = Convert.ToInt32(ConfigurationManager.AppSettings["OrderPageProductViewRow_Offer"]);


        private ToolTip sideMenuToolTip;
        private readonly Image sideMenuButtonImage = (Image)Properties.Resources.fb_sidemenu.Clone();

        private readonly BllSearch<DmnGridFields> _search = new BllSearch<DmnGridFields>();
        private readonly DmnSearch _query = new DmnSearch();

        private bool _packingTaxConfigurationLoaded;
        private bool _packingTaxConfigured;
        private string _packingTaxMode;
        private string _packingTaxType;
        private decimal _packingTaxPercentage;

        #region main product variable
        public int Global_ProductId = 0;
        List<DmnOrderMaster> gridSourcemaster = new List<DmnOrderMaster>();
        List<DmnOrderMaster> gridSourcemaster_res = new List<DmnOrderMaster>();
        List<DmnGridFields> gridSource_offer = new List<DmnGridFields>();
        public int r1 = 0;
        #endregion

        public bool OrderRunning_Status = false;
        public bool Booking_Status = false;
        public bool combined_Status = false;
        public bool Pickup_Status = false;
        public bool combined_Pickup_Status = false;
        public bool BillCopy_Status = false;
        public bool KOT_saleprint_status = false;
        public int ordercount = 0;
        public DateTime OrderDateTime;


        bool DirectPayBill = false;
        int refundsmid = 0, Refundkitchenslno = 0, Refundorderno = 0; decimal refundtotal = 0, Refundproductwisetotal = 0;
        bool Refundproductflag = false;

        public bool itemcode_serach_Status = false;

        public int Productrate_ID = 0;

        public static Decimal midas_balance = 0;
        private List<(string PartPayId, string PartAmount)> partPayments = new List<(string, string)>();
        int partCount = 0;
        public static bool exitstatus = false;

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
        public int iorgname = 46;
        public int icombotaxamt = 47;
        public int istocktrackno = 48;
        public int ipretaxamtno = 49;
        public int iSaledisperno = 50;
        public int isalediamtno = 51;


        // Combined grid column indexes match Dgv_Selected_Item.
        public int Caction = 0;
        public int Cslno = 1;
        public int Cbarcode = 2;
        public int vname = 4;
        public int ciunit = 5;
        public int Csrate = 6;
        public int cqty = 7;
        public int cprate = 8;
        public int camount = 9;
        public int cdisper = 10;
        public int cdisamt = 11;
        public int ctaxper = 12;
        public int ctaxamt = 13;
        public int ctotal = 14;
        public int citemid = 15;
        public int ctax = 16;
        public int ctaxtype = 17;
        public int Cmrp = 18;
        public int Comproducttypeno = 3;
        public int Ctype = 19;
        public int Cbarcode2 = 20;
        public int Ctaxsettingidno = 21;
        public int Cunitidno = 22;
        public int cpridno = 23;
        public int Cnotesno = 24;
        public int Cstatusno = 25;
        public int cigstamt = 26;
        public int cvatamt = 27;
        public int COrderDetailsIdno = 28;
        public int Cofferstatusno = 29;
        public int Coriginalprice = 30;
        public int cmarketprice = 30;
        public int Csubofferidno = 31;
        public int Ctabletype = 32;
        public int Corderdescriptionno = 33;
        public int Coriginalqtyno = 34;
        public int CAV_id = 35;
        public int CAddonStatus = 36;
        public int CPavid = 37;
        public int CMainProductStatusno = 38;
        public int CAddoncount = 39;
        public int Cvariationidno = 40;
        public int CComboTaxamtno = 41;
        public int Ccodeno = 42;
        public int CVariatiionNameno = 43;
        public int CPreparationModeno = 44;
        public int CSalesDetailsIdno = 45;
        public int COrginalname = 46;
        public int Ccombotaxamtno = 47;
        public int Cstocktrackno = 48;
        public int Cpretaxamtno = 49;
        public int cSaledisperno = 50;
        public int csalediamtno = 51;
        public int COrderTypeIdno = 54;

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

        public int Pickup_ID = 0;

        bool isOperationShown = false;

        #region Blur Popup Overlay
        private PictureBox _blurPopupPictureBox;
        private Bitmap _blurPopupImage;
        private readonly HashSet<Panel> _activeBlurPopupPanels = new HashSet<Panel>();
        private bool _isUpdatingBlurPopup;
        private bool _isBlurPopupReleaseScheduled;
        private bool _isConfiguringOperationButtons;
        private const int BlurPreviewScale = 4;
        private const byte BlurDarkOverlayAlpha = 76;
        #endregion

        private Button previousClickedButton = null;
        public List<Variation_addon_List> Checked_VariationAddon_List = new List<Variation_addon_List>();
        public string addon_Vari_Id = "";
        public bool variflag = true, addonflag = true;
        public int Split_Rowno = 0;
        public int maxLength_variation = 3;
        Color seleproduct_buttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);

        public int Promodetailsid = 0;
        public int Promomasterid = 0;

        private System.Windows.Forms.Timer AutoCloseTimer = new System.Windows.Forms.Timer();

        private bool isCategoryExpanded = false;
        public Color AddonBGcolor = ColorTranslator.FromHtml("#EBFFEB");

        public int ResetFlagvalue = 0;
        public bool RFID_Status = false;

        public decimal TotalTax = 0;
        public decimal TotalIGST = 0;
        public decimal TotalCGST = 0;
        public decimal TotalSGST = 0;
        public decimal TotalVAT = 0;
        public decimal TotalPreTaxAmt = 0;
        public decimal Totaldis = 0;

        string reading_RFID = "";
        public int finalqty = 0;

        #region Midascard
        public int CTyp = 0;
        public int DebitCustomerId = 0;
        public string DebitCardNumber = "";
        public int CardTypeno = 0;
        public int fmasterid = 0;
        public int Fmcardtypeno = 0;
        private bool isReadingCard = false;
        int payid = 0;
        #endregion

        bool cancel_byKitchen = false;

        public List<OrderDetailsInfo> orderDetailsInfoList = new List<OrderDetailsInfo>();
        public class OrderDetailsInfo
        {
            public int OrderDetailsId { get; set; }
            public int saleDetailsId { get; set; }
            public int BillNumber { get; set; }
        }

        public int saletype = 0;
        public int Proid_Prid = 0;
        public bool ordercancelbystaff = false;
        public string RPTPath = Convert.ToString(FrmLogin.KitchenBillPath);
        int CounterKotNo = 0;
        public bool Comboproductflag = false;
        public bool DiscountApplied = false;
        public bool DiscountAmountApplied = false;
        public bool CompBillPreview = false;
        private bool EnableCompReasonPrompt = false;

        RoundOffMode currentRoundMode = RoundOffMode.None;
        decimal originalRoundValue = 0;
        bool isRoundValueCaptured = false;

        // Saved SalesMaster.Roundoff is a signed adjustment, not the final invoice amount.
        private bool hasRestoredRoundoffAdjustment = false;
        private decimal restoredRoundoffAdjustment = 0m;
        private readonly string reasonPlaceholder = "Type reason description...";

        #region Drag and Drop
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x02;

        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(
            IntPtr hWnd,
            int msg,
            IntPtr wParam,
            IntPtr lParam);

        private bool isDraggingProductCart;
        private Point productCartDragStartMouse;
        private Point productCartDragStartLocation;
        private Control productCartDragSource;
        #endregion

        #endregion
        public FrmOrderConsole(int tid, int booktype, int bmasterid, List<DmnNameList> checkedtableNames, int Pageno, int Ordermasterid, int sourcepagetype, int cdstatus, int orderTypeId = 0)
        {
            tableId = tid;
            sourcepage = sourcepagetype;
            Bookingmasterid = bmasterid;
            BookingType = booktype;
            OrderTypeId = orderTypeId > 0
                ? orderTypeId
                : sourcepage == Convert.ToInt32(SourcepageType.Pickuppage)
                    ? 1
                    : sourcepage == Convert.ToInt32(SourcepageType.Takeawaypage)
                        ? 2
                        : booktype == Convert.ToInt32(DashboardTableType.PickupOrder)
                            ? 1
                            : booktype == TakeawayBookingType ? 2 : 0;
            StockDeductionOrderTypeId = OrderTypeId;
            PageType = Pageno;
            this.checkedtableNames = checkedtableNames;
            OmId = Ordermasterid;
            OrderCancelStatus = cdstatus;

            InitializeComponent();

            if (sourcepage == Convert.ToInt32(SourcepageType.Takeawaypage))
            {
                StartPosition = FormStartPosition.CenterScreen;
                WindowState = FormWindowState.Maximized;
            }

            InitializeBlurPopupSupport();
            tlp_netalc_buttons.SizeChanged += delegate { ConfigureResponsiveOperationButtons(); };

            EnableProductCartDragging();

            //lbl_header_cart.Cursor = Cursors.SizeAll;
            //lbl_header_cart.MouseDown += lbl_header_cart_MouseDown;

            f11 = AppFonts.GetFont(11);
            f9 = AppFonts.GetFont(9);
            f10 = AppFonts.GetFont(10);
            f10Strike = new Font(f10, FontStyle.Strikeout);
            f10Bold = new Font(f10, FontStyle.Bold);
        }

        #region Blur Popup Overlay

        private void InitializeBlurPopupSupport()
        {
            // These panels are direct children of FrmOrderConsole and act as popups.
            // The Crystal Report viewer is intentionally excluded from bitmap capture.
            Panel[] popupPanels =
            {
                pnlPaymode,
                pnl_complementry_reason,
                pnl_productCart,
                pnl_variation,
                pnl_userreg,
                pnlSplit,
                pnl_order_desc,
                pnl_Guests,
                pnl_orderprority,
                pnl_cancelledlist,
                pnl_multprice,
                pnl_Refund_paymode,
                pnlproductviewmore,
                pnl_gridview,
                pnl_products_descr,
                pnl_cardAcoount,
                Pnl_wifiPopup,
                pnlOthers,
                pnl_cancelorder,
                pnl_partpayment,
                pnl_variation_addon,
                pnl_promocode,
                pnlprice
            };

            foreach (Panel popup in popupPanels)
            {
                if (popup == null)
                    continue;

                popup.VisibleChanged -= BlurPopupPanel_VisibleChanged;
                popup.VisibleChanged += BlurPopupPanel_VisibleChanged;
            }

            FormClosed += FrmOrderConsole_FormClosed;
        }

        private void BlurPopupPanel_VisibleChanged(object sender, EventArgs e)
        {
            if (_isUpdatingBlurPopup)
                return;

            Panel popup = sender as Panel;
            if (popup == null || popup.IsDisposed)
                return;

            if (popup.Visible)
                ShowBlurPopup(popup);
            else
                HideBlurPopup(popup);
        }

        private void ShowBlurPopup(Panel popup)
        {
            if (popup == null || popup.IsDisposed || popup.Parent != this)
                return;

            try
            {
                bool isFirstPopup = _activeBlurPopupPanels.Count == 0;

                if (isFirstPopup)
                {
                    bool wasVisible = popup.Visible;
                    bool wasOverlayVisible =
                        _blurPopupPictureBox != null &&
                        !_blurPopupPictureBox.IsDisposed &&
                        _blurPopupPictureBox.Visible;

                    _isUpdatingBlurPopup = true;

                    try
                    {
                        // Exclude the popup being opened and any previous overlay
                        // from the background snapshot during popup-to-popup transitions.
                        popup.Visible = false;

                        if (wasOverlayVisible)
                            _blurPopupPictureBox.Visible = false;

                        Bitmap nextBlurImage;
                        using (Bitmap originalImage = CaptureOrderConsoleForm())
                        using (Bitmap smallImage = ResizeBlurPreview(
                            originalImage,
                            Math.Max(1, originalImage.Width / BlurPreviewScale),
                            Math.Max(1, originalImage.Height / BlurPreviewScale)))
                        {
                            nextBlurImage = CreateDarkBlurPreview(smallImage);
                        }

                        EnsureBlurPopupPictureBox();

                        Bitmap previousImage = _blurPopupImage;
                        _blurPopupImage = nextBlurImage;
                        _blurPopupPictureBox.Image = _blurPopupImage;

                        if (previousImage != null)
                            previousImage.Dispose();
                    }
                    finally
                    {
                        popup.Visible = wasVisible;
                        _isUpdatingBlurPopup = false;

                        if (wasOverlayVisible &&
                            _blurPopupPictureBox != null &&
                            !_blurPopupPictureBox.IsDisposed)
                            _blurPopupPictureBox.Visible = true;
                    }

                    EnsureBlurPopupPictureBox();
                    _blurPopupPictureBox.Image = _blurPopupImage;
                    _blurPopupPictureBox.Bounds = ClientRectangle;
                    _blurPopupPictureBox.Visible = true;
                    _blurPopupPictureBox.BringToFront();
                }

                _activeBlurPopupPanels.Add(popup);
                PositionBlurOverlayBehindPopup(popup);
            }
            catch (Exception ex)
            {
                _isUpdatingBlurPopup = false;
                clsgen.LogException(
                    "Order Console ShowBlurPopup()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order));
            }
        }

        private void PositionBlurOverlayBehindPopup(Panel popup)
        {
            if (popup == null ||
                popup.IsDisposed ||
                popup.Parent != this ||
                _blurPopupPictureBox == null ||
                _blurPopupPictureBox.IsDisposed ||
                !Controls.Contains(_blurPopupPictureBox))
                return;

            popup.BringToFront();

            int popupIndex = Controls.GetChildIndex(popup);
            int overlayIndex = Math.Min(popupIndex + 1, Controls.Count - 1);
            Controls.SetChildIndex(_blurPopupPictureBox, overlayIndex);
        }

        private void HideBlurPopup(Panel popup)
        {
            if (popup == null)
                return;

            _activeBlurPopupPanels.Remove(popup);

            if (_activeBlurPopupPanels.Count == 0)
                ScheduleBlurPopupRelease();
        }

        private void ScheduleBlurPopupRelease()
        {
            if (_isBlurPopupReleaseScheduled)
                return;

            _isBlurPopupReleaseScheduled = true;

            if (!IsHandleCreated || IsDisposed || Disposing)
            {
                _isBlurPopupReleaseScheduled = false;
                ReleaseBlurPopupImage();
                return;
            }

            try
            {
                BeginInvoke((MethodInvoker)delegate
                {
                    _isBlurPopupReleaseScheduled = false;

                    if (IsDisposed || Disposing)
                        return;

                    if (_activeBlurPopupPanels.Count == 0)
                        ReleaseBlurPopupImage();
                });
            }
            catch (InvalidOperationException)
            {
                _isBlurPopupReleaseScheduled = false;
                ReleaseBlurPopupImage();
            }
        }

        private void EnsureBlurPopupPictureBox()
        {
            if (_blurPopupPictureBox != null && !_blurPopupPictureBox.IsDisposed)
                return;

            _blurPopupPictureBox = new PictureBox
            {
                Name = "picOrderConsoleBlurOverlay",
                Bounds = ClientRectangle,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Black,
                TabStop = false,
                Enabled = false,
                Visible = false
            };

            Controls.Add(_blurPopupPictureBox);
        }

        private Bitmap CaptureOrderConsoleForm()
        {
            int width = Math.Max(1, ClientSize.Width);
            int height = Math.Max(1, ClientSize.Height);

            Bitmap captured = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            try
            {
                DrawToBitmap(captured, new Rectangle(0, 0, width, height));
                return captured;
            }
            catch
            {
                captured.Dispose();
                throw;
            }
        }

        private Bitmap ResizeBlurPreview(Bitmap image, int width, int height)
        {
            Bitmap resized = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(resized))
            {
                graphics.InterpolationMode = InterpolationMode.Low;
                graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.DrawImage(image, 0, 0, width, height);
            }

            return resized;
        }

        private Bitmap CreateDarkBlurPreview(Bitmap image)
        {
            Bitmap blurred = BlurImageFast(image);

            try
            {
                using (Graphics graphics = Graphics.FromImage(blurred))
                using (SolidBrush shade = new SolidBrush(Color.FromArgb(BlurDarkOverlayAlpha, 0, 0, 0)))
                {
                    graphics.CompositingMode = CompositingMode.SourceOver;
                    graphics.FillRectangle(shade, 0, 0, blurred.Width, blurred.Height);
                }

                return blurred;
            }
            catch
            {
                blurred.Dispose();
                throw;
            }
        }

        private Bitmap BlurImageFast(Bitmap image)
        {
            const int bytesPerPixel = 4;
            int width = image.Width;
            int height = image.Height;
            int rowBytes = width * bytesPerPixel;

            Bitmap blurred = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData sourceData = null;
            BitmapData destinationData = null;
            bool completed = false;

            try
            {
                Rectangle bounds = new Rectangle(0, 0, width, height);
                sourceData = image.LockBits(bounds, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                destinationData = blurred.LockBits(bounds, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

                byte[] sourcePixels = new byte[rowBytes * height];
                byte[] resultPixels = new byte[sourcePixels.Length];

                for (int y = 0; y < height; y++)
                {
                    IntPtr sourceRow = IntPtr.Add(sourceData.Scan0, y * sourceData.Stride);
                    Marshal.Copy(sourceRow, sourcePixels, y * rowBytes, rowBytes);
                }

                Buffer.BlockCopy(sourcePixels, 0, resultPixels, 0, sourcePixels.Length);

                // A small 3x3 box blur on a 1/4-size bitmap keeps the UI responsive.
                for (int y = 1; y < height - 1; y++)
                {
                    for (int x = 1; x < width - 1; x++)
                    {
                        int blue = 0;
                        int green = 0;
                        int red = 0;

                        for (int fy = -1; fy <= 1; fy++)
                        {
                            int rowOffset = (y + fy) * rowBytes;

                            for (int fx = -1; fx <= 1; fx++)
                            {
                                int offset = rowOffset + ((x + fx) * bytesPerPixel);
                                blue += sourcePixels[offset];
                                green += sourcePixels[offset + 1];
                                red += sourcePixels[offset + 2];
                            }
                        }

                        int targetOffset = (y * rowBytes) + (x * bytesPerPixel);
                        resultPixels[targetOffset] = (byte)(blue / 9);
                        resultPixels[targetOffset + 1] = (byte)(green / 9);
                        resultPixels[targetOffset + 2] = (byte)(red / 9);
                        resultPixels[targetOffset + 3] = sourcePixels[targetOffset + 3];
                    }
                }

                for (int y = 0; y < height; y++)
                {
                    IntPtr destinationRow = IntPtr.Add(destinationData.Scan0, y * destinationData.Stride);
                    Marshal.Copy(resultPixels, y * rowBytes, destinationRow, rowBytes);
                }

                completed = true;
                return blurred;
            }
            finally
            {
                try
                {
                    if (destinationData != null)
                        blurred.UnlockBits(destinationData);
                }
                finally
                {
                    try
                    {
                        if (sourceData != null)
                            image.UnlockBits(sourceData);
                    }
                    finally
                    {
                        if (!completed)
                            blurred.Dispose();
                    }
                }
            }
        }

        private void ReleaseBlurPopupImage()
        {
            if (_blurPopupPictureBox != null && !_blurPopupPictureBox.IsDisposed)
            {
                _blurPopupPictureBox.Visible = false;
                _blurPopupPictureBox.Image = null;
            }

            if (_blurPopupImage != null)
            {
                _blurPopupImage.Dispose();
                _blurPopupImage = null;
            }
        }

        private void FrmOrderConsole_FormClosed(object sender, FormClosedEventArgs e)
        {
            _activeBlurPopupPanels.Clear();
            ReleaseBlurPopupImage();

            if (_blurPopupPictureBox != null)
            {
                Controls.Remove(_blurPopupPictureBox);
                _blurPopupPictureBox.Dispose();
                _blurPopupPictureBox = null;
            }
        }

        #endregion

        #region Deafult Fuction & event 
        private void AutoCloseTimer_Tick(object sender, EventArgs e)
        {
            AutoCloseTimer.Stop();
            this.Close();   // safely return to FrmTableView
        }

        private void FrmOrderConsole_Load(object sender, EventArgs e)
        {
            try
            {
                //AutoCloseTimer.Interval = 3000; // close after 3 sec
                //AutoCloseTimer.Tick += AutoCloseTimer_Tick;
                //AutoCloseTimer.Start();

                txtcom_reason.Text = reasonPlaceholder;
                txtcom_reason.ForeColor = Color.Gray;

                ShowLoadingdGif();
                pageSize = Pagecount;

                Generalsettingcheck();
                Pagecount = Convert.ToInt32(ConfigurationManager.AppSettings["FBpagescount"]);

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
                else if (BookingType == Convert.ToInt32(DashboardTableType.CombinedOrderBill))
                {
                    if (OrderCancelStatus == 1)
                    {
                        btncancelnotify.Visible = true;
                    }
                    else
                        btncancelnotify.Visible = false;

                    combined_Status = true;
                    //Btnpartpayment.Visible = false;
                    //btn_splitpayment.Visible = false;
                    int count = checkedtableNames.Count;
                    lblorderid.Visible = false;
                    label10.Visible = false;

                    Combined_Tables();
                    Combined_Table_Bill();
                }
                else if (BookingType == Convert.ToInt32(DashboardTableType.PickupOrder))
                {
                    Pickup_Status = true;
                    combined_Pickup_Status = true;
                    if (OrderCancelStatus == 1)
                    {
                        btncancelnotify.Visible = false;
                        btnordercallist.Visible = false;
                        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "OrderMasterId Field1,SalesMasterId Field2,PkId Field3";
                        dmnSearch.Table = "tblOrderMaster";
                        dmnSearch.WhereNdGroupBy = "WHERE Status=0 and OrderMasterId = " + OmId;
                        dmnSearch.OrderBy = " order by OrderMasterId desc";
                        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        dgv_ordercancel.Rows.Clear();
                        if (gridSource.Count > 0)
                        {
                            txtordermasterid.Text = gridSource[0].Field1;
                            txtsalemasterid.Text = gridSource[0].Field2;

                        }
                    }
                    CheckRe_Pickuporder();

                    CheckPickupkitchenCancel();

                    if (OrderCancelStatus == 1)
                    {
                        btn_cancle_pickup.Visible = false;
                        btnordercallist.PerformClick();

                        UIHelper.CenterAndResize(pnl_cancelledlist, 50, 40);
                        UIHelper.RoundCorners(pnl_cancelledlist, 25);
                        UIHelper.RoundCorners(tlp_cancelled_list_nav, 25);
                    }

                }
                else if (BookingType == TakeawayBookingType)
                {
                    // Existing takeaway KOTs populate the selected-item grid through
                    // the shared Pickup KOT loader. Stock posting still uses type 2.
                    lbltablecode_header.Text = "TAKEAWAY";
                    if (OmId > 0)
                    {
                        Pickup_Status = true;
                        combined_Pickup_Status = true;
                        CheckRe_Pickuporder();
                        CheckPickupkitchenCancel();
                        lbltablecode_header.Text = "TAKEAWAY";
                    }
                    else
                    {
                        Pickup_Status = false;
                        combined_Pickup_Status = false;
                        Showprint_Save(Convert.ToInt32(ShowButtonType.save));
                    }

                    lblorderid.Visible = false;
                    label10.Visible = false;
                    cmb_tblcode.Visible = false;
                    lbltablecode.Visible = false;
                }

                #endregion

                if (tableId > 0 && OmId <= 0 && OrderRunning_Status == false &&
                    Pickup_Status == false && OrderCancelStatus != 1)
                {
                    UIHelper.CenterAndResize(pnl_Guests, 35, 50);
                    UIHelper.RoundCorners(pnl_Guests, 25);
                    UIHelper.RoundCorners(tlp_guest_nav, 25);
                    UIHelper.RoundCorners(tlp_Guests_main, 25);
                    pnl_Guests.BringToFront();

                }

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

                MoveSelectedGrid(tlp_productlist, 0, 0);
                InitializePaymentCalculationPanel();
                FillComboReason();
            }
            catch(Exception ex)
            {
                clsgen.LogException("homepage page  FrmOrderConsole_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void FillComboReason()
        {
            try
            {
                BllSearch<DmnComboBox> bllSearchReason = new BllSearch<DmnComboBox>();
                DmnSearch dmnSearchReason = new DmnSearch();
                dmnSearchReason.Select = "ReasonId, ReasonName";
                dmnSearchReason.Table = "tblreason";
                dmnSearchReason.WhereNdGroupBy = "Where Status = 1";
                cmb_com_reason.DataSource = null;
                var reasonSource = bllSearchReason.GetSearchData("prdComboFill", dmnSearchReason);
                if (reasonSource.Count > 0)
                {
                    cmb_com_reason.DataSource = reasonSource;
                    cmb_com_reason.DisplayMember = "Value";
                    cmb_com_reason.ValueMember = "Id";
                }
            }
             
            catch (Exception ex)
            {
                clsgen.LogException("homepage page  FillComboReason()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
}

        private void FrmOrderConsole_Resize(object sender, EventArgs e)
        {
            maxColumns = columncount;
            spacing = Productspace;
            containerWidth = flp_productview.ClientSize.Width;
            tileW = Convert.ToInt32(Productwidth);//(int)(((containerWidth - ((maxColumns + 1) * spacing)) / maxColumns) * Productwidth);
            tileH = Convert.ToInt32(Productheight);//(int)(tileW * Productheight);

            // Keep the blur overlay matched to the client area as the form changes size.
            if (_blurPopupPictureBox != null && !_blurPopupPictureBox.IsDisposed)
                _blurPopupPictureBox.Bounds = ClientRectangle;

            ConfigureResponsiveOperationButtons();
        }
        #endregion

        #region Common Used Fuction

        public void Generalsettingcheck()
        {
            try
            {
                BllSearch<DmnGeneralSetting> bllsearchdrawer = new BllSearch<DmnGeneralSetting>();
                DmnSearch dmnsearckgen = new DmnSearch
                {
                    Select = "*",
                    Table = "tblGeneralSetting",
                    WhereNdGroupBy = $"where BranchId={FrmDashboard.BranchId} and Status=1",
                    OrderBy = ""
                };
                var gridesource = bllsearchdrawer.GetSearchData("prdSearch", dmnsearckgen);
                if (gridesource.Count > 0)
                {
                    var settings = gridesource[0];
                    CompBillPreview = settings.EnableCompbillPrompt;
                    EnableCompReasonPrompt = settings.EnableCompReasonPrompt;
                    FrmDashboard.UserSavePrompt= settings.EnableUserPrompt;
                    Pagecount = settings.ProductpageperCount;

                    Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    if (config.AppSettings.Settings["FBpagescount"] != null)
                    {
                        config.AppSettings.Settings["FBpagescount"].Value = settings.ProductpageperCount.ToString();
                    }
                    else
                    {
                        config.AppSettings.Settings.Add("FBpagescount", settings.ProductpageperCount.ToString());
                    }
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("homepage page  Generalsettingcheck()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
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

        private int ResolveStockOrderTypeId(DmnOrderMaster orderMaster, DmnNameList selectedOrder)
        {
            // A positive Tid always means a dine-in/table order. It takes priority
            // over legacy token flags because the table identity is authoritative.
            if ((orderMaster != null && orderMaster.Tid > 0) ||
                (selectedOrder != null && selectedOrder.Id > 0 && selectedOrder.pkid <= 0))
                return 0;

            // Pickup orders from the pickup list are always Pickup.
            if (selectedOrder != null && selectedOrder.pkid > 0)
                return 1;

            // OrderMaster.OrderTypeId is the daily-token type:
            // 1 = Pickup, 2 = Takeaway, 3 = Delivery. Table orders use 0.
            if (orderMaster != null &&
                orderMaster.OrderTypeId >= 1 &&
                orderMaster.OrderTypeId <= 3)
                return orderMaster.OrderTypeId;

            // Compatibility for old pickup orders that predate OrderTypeId.
            if (orderMaster != null &&
                (orderMaster.PickupOrderFlag == 1 || orderMaster.PkId > 0))
                return 1;

            if (sourcepage == Convert.ToInt32(SourcepageType.Takeawaypage) ||
                BookingType == TakeawayBookingType)
                return 2;

            if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                return 1;

            return 0;
        }

        private int GetRowOrderTypeId(DataGridViewRow row, int columnIndex, int fallback)
        {
            if (row == null || columnIndex < 0 || columnIndex >= row.Cells.Count)
                return fallback;

            object value = row.Cells[columnIndex].Value;
            if (value == null || value == DBNull.Value)
                return fallback;

            int orderTypeId;
            return int.TryParse(Convert.ToString(value), out orderTypeId)
                ? orderTypeId
                : fallback;
        }

        #region Bill Combined 
        public void Combined_Table_Bill()
        {
            try
            {
                BllSearch<DmnOrderMaster> bllSearchmaster = new BllSearch<DmnOrderMaster>();
                DmnSearch dmnSearchmaster = new DmnSearch();
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
                    int sourceOrderTypeId = checkedtableNames[k].pkid > 0 ? 1 : 0;

                    dmnSearchmaster.Select = "*";
                    dmnSearchmaster.Table = "tblOrderMaster om " +
                        "LEFT JOIN tblOrderDetails od ON od.OrderMasterId = om.OrderMasterId " +
                        "LEFT JOIN tblUnit u ON u.UId = od.UnitId";
                    dmnSearchmaster.WhereNdGroupBy = "where om.Status=1 and om.OrderMasterId=" + checkedtableNames[k].Ordermasterid;
                    dmnSearchmaster.OrderBy = "";
                    var gridSourcemaster_combined = bllSearchmaster.GetSearchData("prdSearch", dmnSearchmaster);

                    if (gridSourcemaster_combined != null && gridSourcemaster_combined.Count > 0)
                        sourceOrderTypeId = ResolveStockOrderTypeId(gridSourcemaster_combined[0], checkedtableNames[k]);

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
                        Dgv_Selected_Item.Rows[Dgv_Selected_Item.Rows.Count - 1].Cells[ipackingOrderTypeIdno.Index].Value = sourceOrderTypeId;
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
                        Dgv_Selected_Item.Rows[Dgv_Selected_Item.Rows.Count - 1].Cells[ipackingOrderTypeIdno.Index].Value = sourceOrderTypeId;
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

                    if (gridSourcemaster_combined != null && gridSourcemaster_combined.Count > 0)
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
                            dmnSearch.Select = "od.*, u.UnitName AS UnitName";
                            dmnSearch.Table = "tblOrderDetails od LEFT JOIN tblUnit u ON u.UId = od.UnitId";
                            dmnSearch.WhereNdGroupBy = "where OrderMasterId= " + gridSourcemaster_combined[j].OrderMasterId + " and Status IN (1, 2, 3) and Orderno=" + orderNo;
                            dmnSearch.OrderBy = "";
                            var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                            if (gridSource.Count > 0)
                            {

                                #region Order Add to the Top
                                Dgv_Selected_Item.Rows.Add();
                        Dgv_Selected_Item.Rows[Dgv_Selected_Item.Rows.Count - 1].Cells[ipackingOrderTypeIdno.Index].Value = sourceOrderTypeId;
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
                        Dgv_Selected_Item.Rows[Dgv_Selected_Item.Rows.Count - 1].Cells[ipackingOrderTypeIdno.Index].Value = sourceOrderTypeId;
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

                                        if (Convert.ToInt32(gridSource[i].vid) > 0)
                                        {

                                            Dgv_Selected_Item.Rows[r].Cells[iname].Value = Convert.ToString(gridSource[i].ItemName) + "(" + Convert.ToString(gridSource[i].VariatiionName) + ")";
                                            Dgv_Selected_Item.Rows[r].Cells[iorgname].Value = Convert.ToString(gridSource[i].ItemName);
                                            Dgv_Selected_Item.Rows[r].Cells[ividno].Value = Convert.ToString(gridSource[i].vid);
                                            Dgv_Selected_Item.Rows[r].Cells[ivaraiationstatusno].Value = 1;

                                        }
                                        else
                                            Dgv_Selected_Item.Rows[r].Cells[iname].Value = Convert.ToString(gridSource[i].ItemName);

                                        Dgv_Selected_Item.Rows[r].Cells[iunit].Value =
                                            string.IsNullOrWhiteSpace(gridSource[i].UnitName)
                                                ? Convert.ToString(gridSource[i].UnitId)
                                                : gridSource[i].UnitName;
                                        Dgv_Selected_Item.Rows[r].Cells[iunitid].Value = gridSource[i].UnitId;
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
                        Dgv_Selected_Item.Rows[Dgv_Selected_Item.Rows.Count - 1].Cells[ipackingOrderTypeIdno.Index].Value = sourceOrderTypeId;
                                        Dgv_Selected_Item.Rows[r].Cells[islno].Value = SL_Main;
                                        var currentFont = Dgv_Selected_Item.Rows[r].Cells[islno].Style.Font ?? Dgv_Selected_Item.Font;
                                        Dgv_Selected_Item.Rows[r].Cells[islno].Style.Font = new Font(currentFont, FontStyle.Bold);

                                        Dgv_Selected_Item.Rows[r].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                        Dgv_Selected_Item.Rows[r].Cells[ibarcode].Value = gridSource[i].ItemBarCode;
                                        Dgv_Selected_Item.Rows[r].Cells[ibarcode2].Value = gridSource[i].ItemBarCode;
                                        Dgv_Selected_Item.Rows[r].Cells[iname].Value = gridSource[i].ItemName;
                                        currentFont = Dgv_Selected_Item.Rows[r].Cells[iname].Style.Font ?? Dgv_Selected_Item.Font;
                                        Dgv_Selected_Item.Rows[r].Cells[iname].Style.Font = new Font(currentFont, FontStyle.Bold);

                                        Dgv_Selected_Item.Rows[r].Cells[iunit].Value =
                                            string.IsNullOrWhiteSpace(gridSource[i].UnitName)
                                                ? Convert.ToString(gridSource[i].UnitId)
                                                : gridSource[i].UnitName;
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
                                            #region Grid inseration Combo Product
                                            if (Convert.ToInt32(Dgv_Selected_Item.Rows[r].Cells[iMainProductStatusno].Value) == Convert.ToInt32(MainProductStatus.Primary))
                                            {
                                                SL_Main++;
                                                Dgv_Selected_Item.Rows.Add();
                        Dgv_Selected_Item.Rows[Dgv_Selected_Item.Rows.Count - 1].Cells[ipackingOrderTypeIdno.Index].Value = sourceOrderTypeId;
                                                Dgv_Selected_Item.Rows[r].Cells[islno].Value = SL_Main;
                                                Dgv_Selected_Item.Rows[r].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                                                var currentFont = Dgv_Selected_Item.Rows[r].Cells[islno].Style.Font ?? Dgv_Selected_Item.Font;
                                                Dgv_Selected_Item.Rows[r].Cells[islno].Style.Font = new Font(currentFont, FontStyle.Bold);

                                                Dgv_Selected_Item.Rows[r].Cells[iname].Value = Convert.ToString(offerDetails[0].Field1);
                                                currentFont = Dgv_Selected_Item.Rows[r].Cells[iname].Style.Font ?? Dgv_Selected_Item.Font;
                                                Dgv_Selected_Item.Rows[r].Cells[iname].Style.Font = new Font(currentFont, FontStyle.Bold);

                                                Dgv_Selected_Item.Rows[r].Cells[iproducttypeno].Value = Convert.ToString(offerDetails[0].Field1);
                                                Dgv_Selected_Item.Rows[r].Cells[iunit].Value =
                                                    string.IsNullOrWhiteSpace(gridSource[i].UnitName)
                                                        ? Convert.ToString(gridSource[i].UnitId)
                                                        : gridSource[i].UnitName;
                                                Dgv_Selected_Item.Rows[r].Cells[iunitid].Value = gridSource[i].UnitId;
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
        public void Combined_Tables()
        {

            try
            {
                BllSearch<DmnOrderMaster> bllSearchmaster = new BllSearch<DmnOrderMaster>();
                DmnSearch dmnSearchmaster = new DmnSearch();
                dgv_combined.Rows.Clear();
                for (int k = 0; k < checkedtableNames.Count; k++)
                {
                    int sourceOrderTypeId = checkedtableNames[k].pkid > 0 ? 1 : 0;

                    dmnSearchmaster.Select = "*";
                    dmnSearchmaster.Table = "tblOrderMaster om " +
                        "LEFT JOIN tblOrderDetails od ON od.OrderMasterId = om.OrderMasterId " +
                        "LEFT JOIN tblUnit u ON u.UId = od.UnitId";
                    dmnSearchmaster.WhereNdGroupBy = "where  om.Status=1 and om.OrderMasterId=" + checkedtableNames[k].Ordermasterid;
                    dmnSearchmaster.OrderBy = "";
                    var gridSourcemaster_combined = bllSearchmaster.GetSearchData("prdSearch", dmnSearchmaster);

                    if (gridSourcemaster_combined != null && gridSourcemaster_combined.Count > 0)
                    {
                        sourceOrderTypeId = ResolveStockOrderTypeId(gridSourcemaster_combined[0], checkedtableNames[k]);

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
                                    int existingOrderTypeId;
                                    int.TryParse(Convert.ToString(row.Cells[COrderTypeIdno].Value), out existingOrderTypeId);
                                    if (existingOrderTypeId != sourceOrderTypeId) continue;
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
                                    dgv_combined.Rows[rowIndex].Cells[Cslno].Value = rowIndex + 1; // SlNo
                                    dgv_combined.Rows[rowIndex].Cells[COrderTypeIdno].Value = sourceOrderTypeId;
                                    dgv_combined.Rows[rowIndex].Cells[cpackingpriceno.Index].Value = 0m;
                                    dgv_combined.Rows[rowIndex].Cells[cpackingtaxamountno.Index].Value = 0m;
                                    dgv_combined.Rows[rowIndex].Cells[vname].Value = gridSourcemaster_combined[i].ItemName;
                                    dgv_combined.Rows[rowIndex].Cells[ciunit].Value =
                                        string.IsNullOrWhiteSpace(gridSourcemaster_combined[i].UnitName)
                                            ? Convert.ToString(gridSourcemaster_combined[i].UnitId)
                                            : gridSourcemaster_combined[i].UnitName;
                                    dgv_combined.Rows[rowIndex].Cells[cprate].Value = gridSourcemaster_combined[i].PurchaseRate;
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
                                    dgv_combined.Rows[rowIndex].Cells[Ctype].Value = gridSourcemaster_combined[i].TableType;
                                    dgv_combined.Rows[rowIndex].Cells[cigstamt].Value = gridSourcemaster_combined[i].IGSTAmount;
                                    dgv_combined.Rows[rowIndex].Cells[cvatamt].Value = gridSourcemaster_combined[i].VATAmount;
                                    dgv_combined.Rows[rowIndex].Cells[cmarketprice].Value = gridSourcemaster_combined[i].MarketPrice;
                                    dgv_combined.Rows[rowIndex].Cells[Cbarcode].Value = gridSourcemaster_combined[i].ItemBarCode;
                                    dgv_combined.Rows[rowIndex].Cells[Cbarcode2].Value = gridSourcemaster_combined[i].ItemBarCode;
                                    dgv_combined.Rows[rowIndex].Cells[Ctaxsettingidno].Value = 0;
                                    dgv_combined.Rows[rowIndex].Cells[Cunitidno].Value = gridSourcemaster_combined[i].UnitId;
                                    dgv_combined.Rows[rowIndex].Cells[Ctabletype].Value = gridSourcemaster_combined[i].TableType;

                                    dgv_combined.Rows[rowIndex].Cells[CPavid].Value = gridSourcemaster_combined[i].Pavid;
                                    dgv_combined.Rows[rowIndex].Cells[CAddonStatus].Value = gridSourcemaster_combined[i].AddonStatus;
                                    dgv_combined.Rows[rowIndex].Cells[CAV_id].Value = gridSourcemaster_combined[i].AV_id;
                                    dgv_combined.Rows[rowIndex].Cells[CAddoncount].Value = gridSourcemaster_combined[i].Addoncount;
                                    dgv_combined.Rows[rowIndex].Cells[Cstatusno].Value = gridSourcemaster_combined[i].Status;
                                    dgv_combined.Rows[rowIndex].Cells[CVariatiionNameno].Value = gridSourcemaster_combined[i].VariatiionName;
                                    dgv_combined.Rows[rowIndex].Cells[CSalesDetailsIdno].Value = gridSourcemaster_combined[i].SalesDetailsId;
                                    dgv_combined.Rows[rowIndex].Cells[CSalesDetailsIdno].Value = gridSourcemaster_combined[i].SalesDetailsId;
                                    dgv_combined.Rows[rowIndex].Cells[COrderDetailsIdno].Value = gridSourcemaster_combined[i].OrderDetailsId;
                                    dgv_combined.Rows[rowIndex].Cells[Cpretaxamtno].Value = gridSourcemaster_combined[i].PreTaxAmt;
                                    dgv_combined.Rows[rowIndex].Cells[Cstocktrackno].Value = gridSourcemaster_combined[i].IsStockTracked;

                                    // Populate the same hidden detail fields used by Dgv_Selected_Item.
                                    // These values are required when saving combined-table sales rows.
                                    dgv_combined.Rows[rowIndex].Cells[Cnotesno].Value = gridSourcemaster_combined[i].Notes;
                                    dgv_combined.Rows[rowIndex].Cells[Cofferstatusno].Value = 0;
                                    dgv_combined.Rows[rowIndex].Cells[Csubofferidno].Value = 0;
                                    dgv_combined.Rows[rowIndex].Cells[Corderdescriptionno].Value = gridSourcemaster_combined[i].MainRemarks;
                                    dgv_combined.Rows[rowIndex].Cells[Coriginalqtyno].Value =
                                        gridSourcemaster_combined[i].CurrentQuantity > 0
                                            ? gridSourcemaster_combined[i].CurrentQuantity
                                            : gridSourcemaster_combined[i].Quantity;
                                    dgv_combined.Rows[rowIndex].Cells[CMainProductStatusno].Value =
                                        Convert.ToInt32(MainProductStatus.Primary);
                                    dgv_combined.Rows[rowIndex].Cells[Cvariationidno].Value = gridSourcemaster_combined[i].vid;
                                    dgv_combined.Rows[rowIndex].Cells[CComboTaxamtno].Value = 0m;
                                    dgv_combined.Rows[rowIndex].Cells[Ccodeno].Value = gridSourcemaster_combined[i].CCode;
                                    dgv_combined.Rows[rowIndex].Cells[CPreparationModeno].Value = gridSourcemaster_combined[i].PreparationMode;
                                    dgv_combined.Rows[rowIndex].Cells[COrginalname].Value = gridSourcemaster_combined[i].ItemName;
                                    dgv_combined.Rows[rowIndex].Cells[Ccombotaxamtno].Value = 0m;
                                    dgv_combined.Rows[rowIndex].Cells[cSaledisperno].Value = 0m;
                                    dgv_combined.Rows[rowIndex].Cells[csalediamtno].Value = 0m;

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
                                    updateRow.Cells[cvatamt].Value = Convert.ToDecimal(updateRow.Cells[cvatamt].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].VATAmount);
                                    updateRow.Cells[Cpretaxamtno].Value = Convert.ToDecimal(updateRow.Cells[Cpretaxamtno].Value) + Convert.ToDecimal(gridSourcemaster_combined[i].PreTaxAmt);
                                    int sourceOriginalQty = gridSourcemaster_combined[i].CurrentQuantity > 0
                                        ? gridSourcemaster_combined[i].CurrentQuantity
                                        : gridSourcemaster_combined[i].Quantity;
                                    int existingOriginalQty = 0;
                                    int.TryParse(Convert.ToString(updateRow.Cells[Coriginalqtyno].Value), out existingOriginalQty);
                                    updateRow.Cells[Coriginalqtyno].Value = existingOriginalQty + sourceOriginalQty;
                                    updateRow.Cells[COrderDetailsIdno].Value =
                                        Convert.ToString(updateRow.Cells[COrderDetailsIdno].Value) + "," +
                                        gridSourcemaster_combined[i].OrderDetailsId;
                                }
                                #endregion

                            }
                        }
                    }
                }

                RefreshCombinedPackingAmounts();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  Combined_Tables()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void RefreshCombinedPackingAmounts()
        {
            if (dgv_combined == null ||
                !dgv_combined.Columns.Contains("cpackingpriceno") ||
                !dgv_combined.Columns.Contains("cpackingtaxamountno"))
                return;

            foreach (DataGridViewRow row in dgv_combined.Rows)
            {
                if (row.IsNewRow) continue;

                int productId, tableType, addonStatus;
                int.TryParse(Convert.ToString(row.Cells[citemid].Value), out productId);
                int.TryParse(Convert.ToString(row.Cells[Ctabletype].Value), out tableType);
                int.TryParse(Convert.ToString(row.Cells[CAddonStatus].Value), out addonStatus);
                int orderTypeId = GetRowOrderTypeId(row, COrderTypeIdno, 0);

                if (orderTypeId != 1 ||
                    tableType != Convert.ToInt32(Tabletype.Product) ||
                    addonStatus != 0 ||
                    productId <= 0)
                {
                    row.Cells[cpackingpriceno.Index].Value = 0m;
                    row.Cells[cpackingtaxamountno.Index].Value = 0m;
                    continue;
                }

                decimal quantity = ToPackingDecimal(row.Cells[cqty].Value);
                ProductPackingCost packing = GetProductPackingCost(productId);

                decimal packingPrice = packing == null || quantity <= 0m
                    ? 0m
                    : Math.Round(packing.UnitGrossPrice * quantity, 2, MidpointRounding.AwayFromZero);

                decimal packingTax = packing == null || quantity <= 0m
                    ? 0m
                    : Math.Round(packing.UnitTaxAmount * quantity, 2, MidpointRounding.AwayFromZero);

                row.Cells[cpackingpriceno.Index].Value = packingPrice;
                row.Cells[cpackingtaxamountno.Index].Value = packingTax;
            }
        }
        #endregion

        #region Pickup order
        public void CheckPickupkitchenCancel()
        {
            try
            {
                #region Load Cancel Order Items
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "itemName Field1,Quantity Field2,RemoveNotes Field3,OrderMasterId Field4,OrderDetailsId Field5,NetAmount Field6,CASE WHEN EXISTS(SELECT 1 FROM tblOrderDetails AS x WHERE x.OrderMasterId = tblOrderDetails.OrderMasterId AND x.KitchenCancelStatus = 1 ) THEN 1 ELSE 0 END AS Field7";
                dmnSearch.Table = "tblOrderDetails";
                dmnSearch.WhereNdGroupBy =
                    "WHERE Status = 0 AND AddonStatus IN (1,0,2) AND CancelNotifyStatus = 1 and RefundStatus !=1" +
                    "AND OrderMasterId = " + Convert.ToInt32(lblorderid.Text);//KitchenCancelStatus = 1
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                dgv_ordercancel.Rows.Clear();
                if (gridSource.Count > 0)
                {


                    if (Convert.ToInt32(gridSource[0].Field7) == 1)
                    {
                        btnordercallist.Visible = true;
                        Refundproductflag = true;


                    }
                    else
                    {
                        btnordercallist.Visible = false;
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

                    UIHelper.CenterAndResize(pnl_cancelledlist, 50, 40);
                    UIHelper.RoundCorners(pnl_cancelledlist, 25);
                    UIHelper.RoundCorners(tlp_cancelled_list_nav, 25);
                }
                else
                {
                    btnordercallist.Visible = false;
                    lblpickupcnlamt.Text = "0";
                    pnl_cancelledlist.Visible = false;
                }
                #endregion

                if (gridSource.Count == 0)
                    CheckReconfirmPickup();
                btn_pa__toggle.PerformClick();
            }

            catch (Exception ex)
            {

            }
        }
        public void CheckReconfirmPickup()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                #region Load Cancel Order Items

                dmnSearch.Select = "CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS Field1";
                dmnSearch.Table = "tblOrderDetails";
                dmnSearch.WhereNdGroupBy =
                    "WHERE Status = 0 AND AddonStatus IN (1,0,2) AND KitchenCancelStatus = 1 " +
                    "AND OrderMasterId = " + Convert.ToInt32(lblorderid.Text);

                var gridSourcekcancelnfy = bllSearch.GetSearchData("prdSearch", dmnSearch);

                if (gridSourcekcancelnfy.Count > 0 && Convert.ToInt32(gridSourcekcancelnfy[0].Field1) == 1)
                {
                    btnreconfirmorder.Visible = true;

                }
                else
                {
                    btnreconfirmorder.Visible = false;
                }
                btn_pa__toggle.PerformClick();

                #endregion
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  discountAmnt()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                dmnSearchmaster.WhereNdGroupBy = "where om.OrderMasterId = " + OmId;
                //" and om.Status=1";
                dmnSearchmaster.OrderBy = " order by OrderNo ";
                gridSourcemaster = bllSearchmaster.GetSearchData("prdSearch", dmnSearchmaster);
                if (gridSourcemaster.Count > 0)
                {
                    var data = gridSourcemaster[0];
                    // ========== BASIC CUSTOMER DETAILS ==========
                    txtfristname.Text = data.FirstName;
                    txtsecondname.Text = data.SecondName;
                    txtcompanyname.Text = data.CompanyName;
                    txtgstno.Text = data.GSTNo;
                    txtph.Text = data.PhoneNumber;
                    txtemailid.Text = data.EmailAddress;
                    txtaddress.Text = data.FullAddress;
                    string gender = Convert.ToString(gridSourcemaster[0].Gender);
                    if (!string.IsNullOrEmpty(gender))
                    {
                        if (gender == "Male" || gender == "M" || gender == "1")
                        {
                            rb_male.Checked = true;
                            rb_female.Checked = false;
                        }
                        else if (gender == "Female" || gender == "F" || gender == "2")
                        {
                            rb_female.Checked = true;
                            rb_male.Checked = false;
                        }
                    }

                    txtitemcode_serach.Enabled = false;
                    txtname_search.Enabled = false;


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

                    lbladvanceamt.Visible = false;
                    txtadvance.Visible = false;

                    //txtpkchrge_amt.Visible = true;
                    //lblpkchrge_amt.Visible = true;

                    ordercount = 0;
                    Pickup_Status = true;

                    cmb_tblcode.Visible = false;
                    lbltablecode.Visible = true;

                    // Show the daily Pickup token (for example P005), not the kitchen serial number.
                    lbltablecode.Text = GetOrderTokenDisplay(Convert.ToInt32(gridSourcemaster[0].OrderMasterId));
                    lbltablecode_header.Text = "PICK UP TOCKEN NO ";

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
                            Dgv_Selected_Item.Rows[r].DefaultCellStyle.Font = normalFont;
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

                                    if (Convert.ToInt32(gridSource[i].vid) > 0)
                                    {

                                        Dgv_Selected_Item.Rows[r].Cells[iname].Value = Convert.ToString(gridSource[i].ItemName) + "(" + Convert.ToString(gridSource[i].VariatiionName) + ")";
                                        Dgv_Selected_Item.Rows[r].Cells[iorgname].Value = Convert.ToString(gridSource[i].ItemName);
                                        Dgv_Selected_Item.Rows[r].Cells[ividno].Value = Convert.ToString(gridSource[i].vid);
                                        Dgv_Selected_Item.Rows[r].Cells[ivaraiationstatusno].Value = 1;

                                    }
                                    else
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
                                    Dgv_Selected_Item.Rows[r].Cells[iqty].Value = gridSource[i].Quantity;
                                    Dgv_Selected_Item.Rows[r].Cells[iprate].Value = gridSource[i].PurchaseRate;
                                    Dgv_Selected_Item.Rows[r].Cells[israte].Value = gridSource[i].Srate;
                                    Dgv_Selected_Item.Rows[r].Cells[iamount].Value = gridSource[i].Amount;
                                    Dgv_Selected_Item.Rows[r].Cells[imrp].Value = gridSource[i].MRP;
                                    Dgv_Selected_Item.Rows[r].Cells[idisper].Value = gridSource[i].DiscountPer;
                                    Dgv_Selected_Item.Rows[r].Cells[idisamt].Value = gridSource[i].DiscountAmount;
                                    Dgv_Selected_Item.Rows[r].Cells[iitemid].Value = gridSource[i].ItemId;
                                    Dgv_Selected_Item.Rows[r].Cells[itaxper].Value = gridSource[i].TaxPer;
                                    Dgv_Selected_Item.Rows[r].Cells[itaxamt].Value = gridSource[i].TaxAmount;
                                    Dgv_Selected_Item.Rows[r].Cells[itax].Value = gridSource[i].Tax;
                                    Dgv_Selected_Item.Rows[r].Cells[itaxtype].Value = gridSource[i].TaxType;
                                    Dgv_Selected_Item.Rows[r].Cells[itype].Value = gridSource[i].TableType;
                                    Dgv_Selected_Item.Rows[r].Cells[itotal].Value = gridSource[i].NetAmount;
                                    Dgv_Selected_Item.Rows[r].Cells[ioriginalprice].Value = gridSource[i].NetAmount;
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
                                        Dgv_Selected_Item.Rows[r].Cells[iprate].Value = gridSource[i].PurchaseRate;
                                        Dgv_Selected_Item.Rows[r].Cells[israte].Value = gridSource[i].Srate;
                                        Dgv_Selected_Item.Rows[r].Cells[iamount].Value = gridSource[i].Amount;
                                        Dgv_Selected_Item.Rows[r].Cells[imrp].Value = gridSource[i].MRP;
                                        Dgv_Selected_Item.Rows[r].Cells[itaxper].Value = gridSource[i].TaxPer;
                                        Dgv_Selected_Item.Rows[r].Cells[itaxamt].Value = gridSource[i].TaxAmount;
                                        Dgv_Selected_Item.Rows[r].Cells[itaxtype].Value = gridSource[i].TaxType;
                                        Dgv_Selected_Item.Rows[r].Cells[itax].Value = gridSource[i].Tax;
                                        Dgv_Selected_Item.Rows[r].Cells[itaxsettingid].Value = Convert.ToInt32(offerDetails[0].Field5);
                                        Dgv_Selected_Item.Rows[r].Cells[idisper].Value = gridSource[i].DiscountPer;
                                        Dgv_Selected_Item.Rows[r].Cells[idisamt].Value = gridSource[i].DiscountAmount;
                                        Dgv_Selected_Item.Rows[r].Cells[iitemid].Value = Convert.ToInt32(gridSource[i].ItemId);
                                        Dgv_Selected_Item.Rows[r].Cells[itotal].Value = gridSource[i].NetAmount;
                                        Dgv_Selected_Item.Rows[r].Cells[ioriginalprice].Value = gridSource[i].NetAmount;
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
                    // Rebuild the invoice total and tax/packing calculations from the
                    // saved KOT detail values for both Pickup and Takeaway reopen flows.
                    txtGrandTotal.Text = gtotal.ToString("0.00");
                    FillSalesMasterDiscount();
                    RecalculateGridTotal();
                    netAmount();
                    FillSalesMasterDiscount();
                    RestoreSavedRoundoffAdjustment();

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

        #region Fresh or Reorder table
        public void CheckRe_order()
        {
            try
            {
                BllSearch<DmnOrderMaster> bllSearchmaster = new BllSearch<DmnOrderMaster>();
                DmnSearch dmnSearchmaster = new DmnSearch();
                dmnSearchmaster.Select = "*";
                dmnSearchmaster.Table =
                    "tblOrderMaster om " +
                    "LEFT JOIN tblOrderDetails od ON od.OrderMasterId = om.OrderMasterId " +
                    "AND od.Status IN (1, 2, 3) ";

                // Checkout tables already have an OrderMasterId in tblTable_info.
                // Reopen that exact order even when its master status is no longer 1.
                // Only use the active-order lookup for genuinely new/unlinked tables.
                dmnSearchmaster.WhereNdGroupBy = OmId > 0
                    ? "WHERE om.OrderMasterId = " + OmId
                    : "WHERE om.Tid = " + tableId + " AND om.Status = 1";

                dmnSearchmaster.OrderBy = " ORDER BY od.OrderNo, od.OrderDetailsId ";
                gridSourcemaster = bllSearchmaster.GetSearchData("prdSearch", dmnSearchmaster);
                if (gridSourcemaster.Count > 0)
                {
                    var data = gridSourcemaster[0];
                    // ========== BASIC CUSTOMER DETAILS ==========
                    txtfristname.Text = data.FirstName;
                    txtsecondname.Text = data.SecondName;
                    txtcompanyname.Text = data.CompanyName;
                    txtgstno.Text = data.GSTNo;
                    txtph.Text = data.PhoneNumber;
                    txtemailid.Text = data.EmailAddress;
                    txtaddress.Text = data.FullAddress;
                    string gender = Convert.ToString(gridSourcemaster[0].Gender);
                    if (!string.IsNullOrEmpty(gender))
                    {
                        if (gender == "Male" || gender == "M" || gender == "1")
                        {
                            rb_male.Checked = true;
                            rb_female.Checked = false;
                        }
                        else if (gender == "Female" || gender == "F" || gender == "2")
                        {
                            rb_female.Checked = true;
                            rb_male.Checked = false;
                        }
                    }

                    tlp_refund_ktcancel.Visible = false;
                    dgv_ordercancel.Columns[cnactionno].Visible = true;
                    int orginalCount = gridSourcemaster.Count(item => item.AddonStatus == 1 || item.AddonStatus == 0);
                    OrderDateTime = gridSourcemaster[0].CreatedDateTime;
                    txtGrandTotal.Text = "0";

                    if (Convert.ToInt32(gridSourcemaster[0].SalesMasterId) > 0)
                        txtsalemasterid.Text = Convert.ToString(gridSourcemaster[0].SalesMasterId);
                    else
                        txtsalemasterid.Text = "0";

                    ordercount = 0;
                    OrderRunning_Status = true;
                    Dgv_Selected_Item.Rows.Clear();

                    cmb_tblcode.Visible = false;
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

                                    if (Convert.ToInt32(gridSource[i].vid) > 0)
                                    {

                                        Dgv_Selected_Item.Rows[r].Cells[iname].Value = Convert.ToString(gridSource[i].ItemName) + "(" + Convert.ToString(gridSource[i].VariatiionName) + ")";
                                        Dgv_Selected_Item.Rows[r].Cells[iorgname].Value = Convert.ToString(gridSource[i].ItemName);
                                        Dgv_Selected_Item.Rows[r].Cells[ividno].Value = Convert.ToString(gridSource[i].vid);
                                        Dgv_Selected_Item.Rows[r].Cells[ivaraiationstatusno].Value = 1;

                                    }
                                    else
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
                                    Dgv_Selected_Item.Rows[r].Cells[ioriginalprice].Value = Convert.ToDecimal(gridSource[i].MarketPrice);

                                    Dgv_Selected_Item.Rows[r].Cells[ipretaxamtno].Value = Convert.ToDecimal(gridSource[i].PreTaxAmt);

                                    Dgv_Selected_Item.Rows[r].Cells[iigstamtno].Value = Convert.ToDecimal(gridSource[i].IGSTAmount);
                                    Dgv_Selected_Item.Rows[r].Cells[ivatamtno].Value = Convert.ToDecimal(gridSource[i].VATAmount);

                                    //Dgv_Selected_Item.Rows[r].Cells[iSaledisperno].Value = Convert.ToDecimal(gridSource[i].);
                                    //Dgv_Selected_Item.Rows[r].Cells[isalediamtno].Value = Convert.ToDecimal(gridSource[i].MarketPrice);

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
                                    Dgv_Selected_Item.Rows[r].Cells[iqty].Value = gridSource[i].Quantity;
                                    Dgv_Selected_Item.Rows[r].Cells[iprate].Value = gridSource[i].PurchaseRate;
                                    Dgv_Selected_Item.Rows[r].Cells[israte].Value = gridSource[i].Srate;
                                    Dgv_Selected_Item.Rows[r].Cells[imrp].Value = gridSource[i].MRP;
                                    Dgv_Selected_Item.Rows[r].Cells[idisamt].Value = "0";

                                    Dgv_Selected_Item.Rows[r].Cells[iamount].Value = Convert.ToDecimal(gridSource[i].Amount);
                                    Dgv_Selected_Item.Rows[r].Cells[itotal].Value = Convert.ToDecimal(gridSource[i].NetAmount);
                                    Dgv_Selected_Item.Rows[r].Cells[itaxper].Value = Convert.ToString(gridSource[i].TaxPer);
                                    Dgv_Selected_Item.Rows[r].Cells[itaxamt].Value = Convert.ToString(gridSource[i].TaxAmount);
                                    Dgv_Selected_Item.Rows[r].Cells[itax].Value = Convert.ToString(gridSource[i].Tax);
                                    Dgv_Selected_Item.Rows[r].Cells[itaxtype].Value = Convert.ToString(gridSource[i].TaxType);


                                    Dgv_Selected_Item.Rows[r].Cells[iitemid].Value = gridSource[i].ItemId;

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

                                    Dgv_Selected_Item.Rows[r].Cells[ioriginalprice].Value =  Convert.ToDecimal(Dgv_Selected_Item.Rows[r].Cells[itotal].Value);

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
                                        //Dgv_Selected_Item.Rows[r].Cells[itaxtype].Value = Convert.ToString(offerDetails[0].Field3);
                                        //Dgv_Selected_Item.Rows[r].Cells[itax].Value = Convert.ToString(offerDetails[0].Field4);
                                        Dgv_Selected_Item.Rows[r].Cells[itaxsettingid].Value = Convert.ToInt32(offerDetails[0].Field5);
                                        Dgv_Selected_Item.Rows[r].Cells[idisper].Value = Convert.ToDecimal(offerDetails[0].Field6);
                                        Dgv_Selected_Item.Rows[r].Cells[iitemid].Value = Convert.ToInt32(gridSource[i].ItemId);
                                        Dgv_Selected_Item.Rows[r].Cells[iproducttypeno].Style.Font = new Font("Arial", 12, FontStyle.Bold);
                                        Dgv_Selected_Item.Rows[r].Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Primary);
                                        Dgv_Selected_Item.Rows[r].Cells[istatusno].Value = 0;
                                        Dgv_Selected_Item.Rows[r].Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
                                        Dgv_Selected_Item.Rows[r].Cells[isubofferid].Value = 0;
                                        Dgv_Selected_Item.Rows[r].Cells[itabletype].Value = Convert.ToInt32(Tabletype.ComboProduct);
                                        Dgv_Selected_Item.Rows[r].Cells[iamount].Value = Convert.ToDecimal(gridSource[i].Amount);
                                        Dgv_Selected_Item.Rows[r].Cells[itotal].Value = Convert.ToDecimal(gridSource[i].NetAmount);

                                        Dgv_Selected_Item.Rows[r].Cells[iamount].Value = Convert.ToDecimal(gridSource[i].Amount);
                                        Dgv_Selected_Item.Rows[r].Cells[itotal].Value = Convert.ToDecimal(gridSource[i].NetAmount);
                                        Dgv_Selected_Item.Rows[r].Cells[itaxper].Value = Convert.ToString(gridSource[i].TaxPer);
                                        Dgv_Selected_Item.Rows[r].Cells[itaxamt].Value = Convert.ToString(gridSource[i].TaxAmount);
                                        Dgv_Selected_Item.Rows[r].Cells[itax].Value = Convert.ToString(gridSource[i].Tax);
                                        Dgv_Selected_Item.Rows[r].Cells[itaxtype].Value = Convert.ToString(gridSource[i].TaxType);

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

                    FillSalesMasterDiscount();

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

                    // Reload the saved sales-master adjustment after recalculating the base total.
                    // Do not trigger Auto Roundoff here: that would replace the saved DB value.
                    FillSalesMasterDiscount();
                    RestoreSavedRoundoffAdjustment();
                }
                else
                {
                    OrderRunning_Status = false;
                    Showprint_Save(Convert.ToInt32(ShowButtonType.save));
                    lblorderid.Visible = false;
                    label10.Visible = false;
                }
                AddEmptyRowIfNeeded();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  CheckRe_order()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void FillSalesMasterDiscount()
        {
            try
            {
                if (!int.TryParse(txtsalemasterid.Text, out int salesMasterId) ||
                    salesMasterId <= 0)
                {
                    txtdis_per.Text = "0";
                    txtdis_amt.Text = "0";
                    txtroundoff.Text = "0.00";
                    hasRestoredRoundoffAdjustment = false;
                    restoredRoundoffAdjustment = 0m;
                    currentRoundMode = RoundOffMode.None;
                    originalRoundValue = 0m;
                    isRoundValueCaptured = false;
                    return;
                }

                BllSearch<DmnSalemaster> bllSearch = new BllSearch<DmnSalemaster>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "DiscountPer DisPer,DiscountAmount DisAmnt,Roundoff";
                dmnSearch.Table = "tblSalesMaster";
                // SalesMasterId is the key; do not filter by Status for checked-out orders.
                dmnSearch.WhereNdGroupBy ="WHERE SalesMasterId=" + salesMasterId;
                dmnSearch.OrderBy = "";
                var result = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (result.Count > 0)
                {
                    txtdis_per.Text = Convert.ToString(result[0].DisPer);
                    txtdis_amt.Text = Convert.ToString(result[0].DisAmnt);
                    txtroundoff.Text = Convert.ToString(result[0].Roundoff);
                }
                else
                {
                    txtdis_per.Text = "0";
                    txtdis_amt.Text = "0";
                    txtroundoff.Text = "0.00";
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Order page FillSalesMasterDiscount()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order));
            }
        }



        private void RestoreSavedRoundoffAdjustment()
        {
            try
            {
                decimal savedAdjustment = GetDecimalValue(txtroundoff);

                currentRoundMode = RoundOffMode.None;
                originalRoundValue = 0m;
                isRoundValueCaptured = false;

                restoredRoundoffAdjustment = savedAdjustment;
                hasRestoredRoundoffAdjustment = savedAdjustment != 0m;

                if (hasRestoredRoundoffAdjustment)
                    CalculateNetamount_ALl();
                else
                    txtroundoff.Text = "0.00";
            }
            catch (Exception ex)
            {
                hasRestoredRoundoffAdjustment = false;
                restoredRoundoffAdjustment = 0m;
                clsgen.LogException(
                    "Order page RestoreSavedRoundoffAdjustment()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order));
            }
        }

        public void ChecktablekitchenCancel()
        {
            try
            {
                #region Load Cancel Order Items
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "itemName Field1,Quantity Field2,RemoveNotes Field3,OrderMasterId Field4,OrderDetailsId Field5,NetAmount Field6,CASE WHEN EXISTS(SELECT 1 FROM tblOrderDetails AS x WHERE x.OrderMasterId = tblOrderDetails.OrderMasterId AND x.KitchenCancelStatus = 1 ) THEN 1 ELSE 0 END AS Field7";
                dmnSearch.Table = "tblOrderDetails";
                dmnSearch.WhereNdGroupBy =
                    "WHERE Status = 0 AND AddonStatus IN (1,0,2) AND CancelNotifyStatus = 1 " +
                    "AND OrderMasterId = " + Convert.ToInt32(lblorderid.Text);
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                dgv_ordercancel.Rows.Clear();
                if (gridSource.Count > 0)
                {
                    //if (Convert.ToInt32(gridSource[0].Field7) == 1)
                    //{
                    //    btnreconfirmorder.Visible = true;
                    //}
                    //else
                    //{
                    //    btnreconfirmorder.Visible = false;
                    //}

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
                {
                    btnordercallist.Visible = false;
                    lblpickupcnlamt.Text = "0";
                }
                #endregion

                if (gridSource.Count == 0)
                    CheckReConfirmTableorder();
            }
            catch (Exception ex)
            {

            }
        }

        public void CheckReConfirmTableorder()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                #region Load Cancel Order Items

                dmnSearch.Select = "CASE WHEN COUNT(*) > 0 THEN 1 ELSE 0 END AS Field1";
                dmnSearch.Table = "tblOrderDetails";
                dmnSearch.WhereNdGroupBy =
                    "WHERE Status = 0 AND AddonStatus IN (1,0,2) AND KitchenCancelStatus = 1 " +
                    "AND OrderMasterId = " + Convert.ToInt32(lblorderid.Text);
                var gridSourcekcancelnfy = bllSearch.GetSearchData("prdSearch", dmnSearch);

                if (gridSourcekcancelnfy.Count > 0 && Convert.ToInt32(gridSourcekcancelnfy[0].Field1) == 1)
                {
                    btnreconfirmorder.Visible = true;
                }
                else
                {
                    btnreconfirmorder.Visible = false;
                }

                #endregion
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  CheckReConfirmTableorder()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                //foreach (TextBox textBox in new TextBox[] { txtitemcode_serach, txtname_search, txt_product_des, lblBalcAmnt, txtGrandTotal, txtRcvdAmnt, txtdis_amt, txtdis_per, lblNetAmnt, txtentrie_descr, txtdisamt_promo, txtpromocode, txtdiscountamt_promo, txtnumber_split, txtadvance, txtph, txtemailid, txtaddress, txtcusname, txt_split_recamt, txt_split_balamt, txt_cardbalance, txtcardemail, txt_cardno, txt_cardaddress, txt_cardph, txt_cardaccname, txtpkchrge_amt })
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
                foreach (TextBox textBox in new TextBox[] { txtRcvdAmnt, txtadvance })
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
        private decimal RecalculateGridTotal()
        {
            decimal totalAmount = 0;
            try
            {
                foreach (DataGridViewRow row in Dgv_Selected_Item.Rows)
                {
                    if (row.IsNewRow) continue;

                    if (row.Cells[iitemid].Value != null &&
                        !string.IsNullOrWhiteSpace(row.Cells[iitemid].Value.ToString()))
                    {
                        decimal rowTotal = 0;
                        decimal.TryParse(Convert.ToString(row.Cells[itotal].Value), out rowTotal);
                        totalAmount += rowTotal;
                    }
                }
                txtGrandTotal.Text = totalAmount.ToString("0.00"); 
            }
            catch (Exception ex)
            {
            }

            return totalAmount; 
        }

        //public void netAmount()
        //{
        //    try
        //    {
        //        #region Setting on textbox Null or Empty

        //        if (string.IsNullOrEmpty(txtdis_amt.Text))
        //            txtdis_amt.Text = "0";
        //        if (string.IsNullOrEmpty(txtRcvdAmnt.Text))
        //            txtRcvdAmnt.Text = "0";
        //        if (string.IsNullOrEmpty(txtdiscountamt_promo.Text))
        //            txtdiscountamt_promo.Text = "0";
        //        if (string.IsNullOrEmpty(txtadvance.Text))
        //            txtadvance.Text = "0";
        //        if (string.IsNullOrEmpty(txtreservationtotal.Text))
        //            txtreservationtotal.Text = "0";
        //        if (string.IsNullOrEmpty(txtGrandTotal.Text))
        //            txtGrandTotal.Text = "0";
        //        if (string.IsNullOrEmpty(txtpkchrge_amt.Text))
        //            txtpkchrge_amt.Text = "0";
        //        #endregion

        //        decimal value;
        //        if (decimal.TryParse(txtdis_amt.Text, out value))
        //        {
        //            decimal netAmount = 0;
        //            netAmount = (Convert.ToDecimal(txtGrandTotal.Text)) - (Convert.ToDecimal(txtdiscountamt_promo.Text));//Convert.ToDecimal(txtdis_amt.Text)

        //            if (Booking_Status && (netAmount > Convert.ToDecimal(txtadvance.Text)))
        //                netAmount = netAmount - Convert.ToDecimal(txtadvance.Text);

        //            if (Pickup_Status || combined_Pickup_Status)
        //                netAmount = netAmount + Convert.ToDecimal(txtpkchrge_amt.Text);

        //            if (Refundproductflag)
        //                netAmount = netAmount - Refundproductwisetotal;

        //            lblNetAmnt.Text = netAmount.ToString("N2");
        //            //label20.Text = "Net Amount : " + lblNetAmnt.Text;
        //            //label21.Text = "Net Amount : " + lblNetAmnt.Text;
        //            decimal balAmnt = netAmount - Convert.ToDecimal(txtRcvdAmnt.Text);
        //            lblBalcAmnt.Text = balAmnt.ToString("N2");
        //        }

        //        totQty();
        //    }
        //    catch (Exception ex)
        //    {
        //        clsgen.LogException("Order page  netAmount()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
        //    }
        //}

        public void netAmount()
        {
            CalculateNetamount_ALl();
        }
        private sealed class ProductPackingCost
        {
            public decimal UnitGrossPrice { get; set; }
            public decimal UnitGstTaxAmount { get; set; }
            public decimal UnitVatTaxAmount { get; set; }

            public decimal UnitTaxAmount
            {
                get { return UnitGstTaxAmount + UnitVatTaxAmount; }
            }
        }

        private readonly Dictionary<int, ProductPackingCost> _productPackingCostCache =
            new Dictionary<int, ProductPackingCost>();

        private static decimal ToPackingDecimal(object value)
        {
            decimal amount;
            return decimal.TryParse(Convert.ToString(value), out amount) ? amount : 0m;
        }

        private ProductPackingCost GetProductPackingCost(int productId)
        {
            if (productId <= 0)
                return null;

            ProductPackingCost cached;
            if (_productPackingCostCache.TryGetValue(productId, out cached))
                return cached;

            try
            {
                BllSearch<DmnGridFields> search = new BllSearch<DmnGridFields>();
                DmnSearch query = new DmnSearch();

                // Use the packaging item's own sale rate and tax configuration.
                // Inclusive rates already contain tax; Exclusive rates receive tax on top.
                query.Select =
                    "ISNULL(SUM(ing.Quantity * unitFactor.QuantityFactor * " +
                    "CASE WHEN UPPER(LTRIM(RTRIM(ISNULL(i.Tax,''))))='EXCLUSIVE' " +
                    "THEN ISNULL(i.Srate,0) * (1 + (tax.GstRate + tax.VatRate) / 100.0) " +
                    "ELSE ISNULL(i.Srate,0) END),0) AS Field1, " +
                    "ISNULL(SUM(ing.Quantity * unitFactor.QuantityFactor * " +
                    "CASE WHEN UPPER(LTRIM(RTRIM(ISNULL(i.Tax,''))))='INCLUSIVE' " +
                    "THEN ISNULL(i.Srate,0) * tax.GstRate / NULLIF(100.0 + tax.GstRate + tax.VatRate,0) " +
                    "WHEN UPPER(LTRIM(RTRIM(ISNULL(i.Tax,''))))='EXCLUSIVE' " +
                    "THEN ISNULL(i.Srate,0) * tax.GstRate / 100.0 ELSE 0 END),0) AS Field2, " +
                    "ISNULL(SUM(ing.Quantity * unitFactor.QuantityFactor * " +
                    "CASE WHEN UPPER(LTRIM(RTRIM(ISNULL(i.Tax,''))))='INCLUSIVE' " +
                    "THEN ISNULL(i.Srate,0) * tax.VatRate / NULLIF(100.0 + tax.GstRate + tax.VatRate,0) " +
                    "WHEN UPPER(LTRIM(RTRIM(ISNULL(i.Tax,''))))='EXCLUSIVE' " +
                    "THEN ISNULL(i.Srate,0) * tax.VatRate / 100.0 ELSE 0 END),0) AS Field3";
                query.Table =
                    "tblProduct p " +
                    "INNER JOIN tblIngredientDetails ing ON ing.ProductId=p.ProductId " +
                    "AND ing.Status=1 AND ing.ProductType=2 " +
                    "INNER JOIN tblItem i ON i.ItemId=ing.ItemId AND i.Status=1 AND i.Saletype=0 " +
                    "LEFT JOIN tblTaxSettings ts ON ts.TaxSettingsId=i.TaxSettingsId AND ts.Status=1 " +
                    "LEFT JOIN tblUnitConversion conv ON conv.FromUnit=ing.Unit AND conv.ToUnit=i.Unit " +
                    "OUTER APPLY (SELECT " +
                    "CASE WHEN UPPER(LTRIM(RTRIM(ISNULL(i.TaxType,''))))='GST' " +
                    "THEN ISNULL(ts.IgstPrcnt,0) ELSE 0 END AS GstRate, " +
                    "CASE WHEN UPPER(LTRIM(RTRIM(ISNULL(i.TaxType,''))))='VAT' " +
                    "THEN ISNULL(ts.VatPrcnt,0) ELSE 0 END AS VatRate) tax " +
                    "OUTER APPLY (SELECT CONVERT(DECIMAL(18,8), " +
                    "CASE WHEN ing.Unit=i.Unit THEN 1.0 " +
                    "WHEN ing.Unit=i.Baseunit AND i.Baseunit<>i.Unit AND ISNULL(i.Conversionfactor,0)>0 " +
                    "THEN 1.0/NULLIF(i.Conversionfactor,0) " +
                    "ELSE ISNULL(conv.Factor,1) END) AS QuantityFactor) unitFactor";
                query.WhereNdGroupBy =
                    "WHERE p.Status=1 AND p.PackagingRequired=1 AND p.ProductId=" + productId;
                query.OrderBy = "";

                List<DmnGridFields> result = search.GetSearchData("prdSearch", query);
                if (result == null || result.Count == 0)
                    return null;

                decimal grossPrice = ToPackingDecimal(result[0].Field1);
                if (grossPrice <= 0m)
                    return null;

                cached = new ProductPackingCost
                {
                    UnitGrossPrice = Math.Round(grossPrice, 4, MidpointRounding.AwayFromZero),
                    UnitGstTaxAmount = Math.Round(
                        ToPackingDecimal(result[0].Field2), 4, MidpointRounding.AwayFromZero),
                    UnitVatTaxAmount = Math.Round(
                        ToPackingDecimal(result[0].Field3), 4, MidpointRounding.AwayFromZero)
                };

                _productPackingCostCache[productId] = cached;
                return cached;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page GetProductPackingCost()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
                return null;
            }
        }

        private void RefreshProductPackingAmounts()
        {
            if (Dgv_Selected_Item == null ||
                !Dgv_Selected_Item.Columns.Contains("ipackingpriceno"))
                return;

            foreach (DataGridViewRow row in Dgv_Selected_Item.Rows)
            {
                if (row.IsNewRow)
                    continue;

                int productId;
                int tableType;
                int addonStatus;
                int.TryParse(Convert.ToString(row.Cells[iitemid].Value), out productId);
                int.TryParse(Convert.ToString(row.Cells[itabletype].Value), out tableType);
                int.TryParse(Convert.ToString(row.Cells[ivaraiationstatusno].Value), out addonStatus);

                int rowOrderTypeId = GetRowOrderTypeId(
                    row, ipackingOrderTypeIdno.Index, OrderTypeId);

                bool eligibleOrder = rowOrderTypeId == 1;
                bool eligibleProduct =
                    tableType == Convert.ToInt32(Tabletype.Product) &&
                    addonStatus == 0 &&
                    productId > 0;

                if (!eligibleOrder || !eligibleProduct)
                {
                    row.Cells[ipackingpriceno.Index].Value = 0m;
                    row.Cells[ipackingtaxamountno.Index].Value = 0m;
                    continue;
                }

                decimal quantity = ToPackingDecimal(row.Cells[iqty].Value);
                ProductPackingCost packing = GetProductPackingCost(productId);

                decimal packingPrice = packing == null || quantity <= 0m
                    ? 0m
                    : Math.Round(packing.UnitGrossPrice * quantity, 2, MidpointRounding.AwayFromZero);

                decimal packingTax = packing == null || quantity <= 0m
                    ? 0m
                    : Math.Round(packing.UnitTaxAmount * quantity, 2, MidpointRounding.AwayFromZero);

                row.Cells[ipackingpriceno.Index].Value = packingPrice;
                row.Cells[ipackingtaxamountno.Index].Value = packingTax;
            }
        }

        private decimal GetGridPackingPrice()
        {
            decimal total = 0m;
            foreach (DataGridViewRow row in Dgv_Selected_Item.Rows)
                if (!row.IsNewRow)
                    total += ToPackingDecimal(row.Cells[ipackingpriceno.Index].Value);

            return Math.Round(total, 2, MidpointRounding.AwayFromZero);
        }

        private decimal GetGridPackingTax()
        {
            decimal total = 0m;
            foreach (DataGridViewRow row in Dgv_Selected_Item.Rows)
                if (!row.IsNewRow)
                    total += ToPackingDecimal(row.Cells[ipackingtaxamountno.Index].Value);

            return Math.Round(total, 2, MidpointRounding.AwayFromZero);
        }

        private decimal GetGridPackingGstTax()
        {
            decimal total = 0m;
            foreach (DataGridViewRow row in Dgv_Selected_Item.Rows)
            {
                if (row.IsNewRow) continue;

                int productId, tableType, addonStatus;
                int.TryParse(Convert.ToString(row.Cells[iitemid].Value), out productId);
                int.TryParse(Convert.ToString(row.Cells[itabletype].Value), out tableType);
                int.TryParse(Convert.ToString(row.Cells[ivaraiationstatusno].Value), out addonStatus);
                int orderTypeId = GetRowOrderTypeId(row, ipackingOrderTypeIdno.Index, OrderTypeId);

                if (orderTypeId != 1 || tableType != Convert.ToInt32(Tabletype.Product) ||
                    addonStatus != 0 || productId <= 0)
                    continue;

                decimal quantity = ToPackingDecimal(row.Cells[iqty].Value);
                ProductPackingCost packing = GetProductPackingCost(productId);
                if (packing != null && quantity > 0m)
                    total += packing.UnitGstTaxAmount * quantity;
            }

            return Math.Round(total, 2, MidpointRounding.AwayFromZero);
        }

        private decimal GetGridPackingVatTax()
        {
            decimal total = 0m;
            foreach (DataGridViewRow row in Dgv_Selected_Item.Rows)
            {
                if (row.IsNewRow) continue;

                int productId, tableType, addonStatus;
                int.TryParse(Convert.ToString(row.Cells[iitemid].Value), out productId);
                int.TryParse(Convert.ToString(row.Cells[itabletype].Value), out tableType);
                int.TryParse(Convert.ToString(row.Cells[ivaraiationstatusno].Value), out addonStatus);
                int orderTypeId = GetRowOrderTypeId(row, ipackingOrderTypeIdno.Index, OrderTypeId);

                if (orderTypeId != 1 || tableType != Convert.ToInt32(Tabletype.Product) ||
                    addonStatus != 0 || productId <= 0)
                    continue;

                decimal quantity = ToPackingDecimal(row.Cells[iqty].Value);
                ProductPackingCost packing = GetProductPackingCost(productId);
                if (packing != null && quantity > 0m)
                    total += packing.UnitVatTaxAmount * quantity;
            }

            return Math.Round(total, 2, MidpointRounding.AwayFromZero);
        }

        private decimal GetCombinedGridPackingPrice()
        {
            decimal total = 0m;
            if (dgv_combined == null) return 0m;

            foreach (DataGridViewRow row in dgv_combined.Rows)
                if (!row.IsNewRow)
                    total += ToPackingDecimal(row.Cells[cpackingpriceno.Index].Value);

            return Math.Round(total, 2, MidpointRounding.AwayFromZero);
        }

        private void GetCombinedGridPackingTax(out decimal gstTax, out decimal vatTax)
        {
            gstTax = 0m;
            vatTax = 0m;

            if (dgv_combined == null)
                return;

            foreach (DataGridViewRow row in dgv_combined.Rows)
            {
                if (row.IsNewRow) continue;

                int productId, tableType, addonStatus;
                int.TryParse(Convert.ToString(row.Cells[citemid].Value), out productId);
                int.TryParse(Convert.ToString(row.Cells[Ctabletype].Value), out tableType);
                int.TryParse(Convert.ToString(row.Cells[CAddonStatus].Value), out addonStatus);
                int orderTypeId = GetRowOrderTypeId(row, COrderTypeIdno, 0);

                if (orderTypeId != 1 || tableType != Convert.ToInt32(Tabletype.Product) ||
                    addonStatus != 0 || productId <= 0)
                    continue;

                decimal quantity = ToPackingDecimal(row.Cells[cqty].Value);
                ProductPackingCost packing = GetProductPackingCost(productId);
                if (packing == null || quantity <= 0m)
                    continue;

                gstTax += packing.UnitGstTaxAmount * quantity;
                vatTax += packing.UnitVatTaxAmount * quantity;
            }

            gstTax = Math.Round(gstTax, 2, MidpointRounding.AwayFromZero);
            vatTax = Math.Round(vatTax, 2, MidpointRounding.AwayFromZero);
        }

        private void SetPackingSaleDetailValues(DmnSaledetails saleDetail, DataGridViewRow row)
        {
            saleDetail.TotalPackingPrice = ToPackingDecimal(row.Cells[ipackingpriceno.Index].Value);
            saleDetail.TotalPackingTax = ToPackingDecimal(row.Cells[ipackingtaxamountno.Index].Value);
        }

        private decimal CalculateSubTotalValue()
        {
            decimal subTotal = 0m;

            foreach (DataGridViewRow row in Dgv_Selected_Item.Rows)
            {
                if (row.IsNewRow)
                    continue;

                if (row.Cells[iamount].Value == null || row.Cells[iamount].Value == DBNull.Value)
                    continue;

                decimal amount = 0m;
                decimal taxPer = 0m;

                decimal.TryParse(Convert.ToString(row.Cells[iamount].Value),out amount);
                decimal.TryParse(Convert.ToString(row.Cells[itaxper].Value),out taxPer);
                string taxType = Convert.ToString(row.Cells[itax].Value) ?? "";

                decimal preTaxAmount;

                if (taxType.Equals("Inclusive",StringComparison.OrdinalIgnoreCase))
                {
                    preTaxAmount = taxPer > 0m
                        ? amount * 100m / (100m + taxPer)
                        : amount;
                }
                else
                {
                    preTaxAmount = amount;
                }

                subTotal += preTaxAmount;
            }

            return Math.Round(subTotal,2,MidpointRounding.AwayFromZero);
        }

        private bool TryGetPackingTaxConfiguration(
            out string taxMode, out string taxType, out decimal taxPercentage)
        {
            taxMode = string.Empty;
            taxType = string.Empty;
            taxPercentage = 0m;

            if (!_packingTaxConfigurationLoaded)
            {
                _packingTaxConfigurationLoaded = true;
                _packingTaxConfigured = false;

                try
                {
                    BllSearch<DmnGeneralSetting> generalSearch =
                        new BllSearch<DmnGeneralSetting>();

                    DmnSearch generalQuery = new DmnSearch
                    {
                        Select = "Gid, Tax, TaxType, TaxSettingId",
                        Table = "tblGeneralSetting",
                        WhereNdGroupBy = "WHERE BranchId = " + FrmDashboard.BranchId +
                                         " AND Status = 1",
                        OrderBy = "ORDER BY Gid DESC"
                    };

                    List<DmnGeneralSetting> generalSettings =
                        generalSearch.GetSearchData("prdSearch", generalQuery);

                    if (generalSettings == null || generalSettings.Count == 0)
                        return false;

                    DmnGeneralSetting setting = generalSettings[0];

                    int taxSettingId = setting.TaxSettingId.GetValueOrDefault();
                    if (taxSettingId <= 0 ||
                        string.IsNullOrWhiteSpace(setting.TaxType) ||
                        string.IsNullOrWhiteSpace(setting.Tax))
                    {
                        return false;
                    }

                    BllSearch<DmnTaxSettings> taxSearch =
                        new BllSearch<DmnTaxSettings>();

                    DmnSearch taxQuery = new DmnSearch
                    {
                        Select = "TaxSettingsId, TaxType, IgstPrcnt, VatPrcnt",
                        Table = "tblTaxSettings",
                        WhereNdGroupBy = "WHERE TaxSettingsId = " + taxSettingId +
                                         " AND Status = 1",
                        OrderBy = ""
                    };

                    List<DmnTaxSettings> taxSettings =
                        taxSearch.GetSearchData("prdSearch", taxQuery);

                    if (taxSettings == null || taxSettings.Count == 0)
                        return false;

                    DmnTaxSettings selectedTax = taxSettings[0];

                    if (!string.Equals(selectedTax.TaxType, setting.TaxType,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }

                    _packingTaxMode = setting.Tax.Trim();
                    _packingTaxType = setting.TaxType.Trim();

                    if (string.Equals(_packingTaxType, "GST",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        _packingTaxPercentage = selectedTax.IgstPrcnt;
                    }
                    else if (string.Equals(_packingTaxType, "VAT",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        _packingTaxPercentage = selectedTax.VatPrcnt;
                    }
                    else
                    {
                        return false;
                    }

                    _packingTaxConfigured = _packingTaxPercentage >= 0m &&
                        (string.Equals(_packingTaxMode, "Inclusive",
                            StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(_packingTaxMode, "Exclusive",
                            StringComparison.OrdinalIgnoreCase));
                }
                catch (Exception ex)
                {
                    _packingTaxConfigured = false;
                    clsgen.LogException(
                        "Order page TryGetPackingTaxConfiguration()",
                        ex,
                        FrmLogin.EmployeName,
                        FrmLogin.Designation,
                        Convert.ToInt32(FormType.Order));
                }
            }

            taxMode = _packingTaxMode ?? string.Empty;
            taxType = _packingTaxType ?? string.Empty;
            taxPercentage = _packingTaxPercentage;
            return _packingTaxConfigured;
        }

        private decimal GetPackingTaxAmount(out bool isGst)
        {
            isGst = false;

            decimal packingCharge = GetDecimalValue(txtpkchrge_amt);
            if (packingCharge <= 0m)
                return 0m;

            string taxMode;
            string taxType;
            decimal taxPercentage;

            if (!TryGetPackingTaxConfiguration(out taxMode, out taxType, out taxPercentage))
                return 0m;

            decimal taxableAmount;
            decimal taxAmount;

            if (string.Equals(taxMode, "Inclusive", StringComparison.OrdinalIgnoreCase))
            {
                taxableAmount = taxPercentage > 0m
                    ? packingCharge * 100m / (100m + taxPercentage)
                    : packingCharge;

                taxAmount = packingCharge - taxableAmount;
            }
            else
            {
                taxableAmount = packingCharge;
                taxAmount = taxableAmount * taxPercentage / 100m;
            }

            isGst = string.Equals(taxType, "GST", StringComparison.OrdinalIgnoreCase);

            return Math.Round(taxAmount, 2, MidpointRounding.AwayFromZero);
        }

        public void CalculateNetamount_ALl()
        {
            try
            {
                RefreshProductPackingAmounts();

                decimal productSubTotal = CalculateSubTotalValue();

                decimal subTotal = productSubTotal;

                lblsubtotal.Text = subTotal.ToString("N2");

                decimal discount = 0m;

                decimal discountPer = 0m;

                decimal enteredDiscountPer = GetDecimalValue(txtdis_per);

                decimal enteredDiscountAmount = GetDecimalValue(txtdis_amt);

                if (Frmnumdiskeypad.flagpercentage)
                {
                    discountPer = enteredDiscountPer;

                    if (discountPer < 0m)
                        discountPer = 0m;

                    if (discountPer > 100m)
                        discountPer = 100m;

                    discount =
                        subTotal *
                        discountPer /
                        100m;
                }
                else
                {
                    discount = enteredDiscountAmount;

                    if (discount < 0m)
                        discount = 0m;

                    if (discount > subTotal)
                        discount = subTotal;

                    discountPer =
                        subTotal > 0m
                            ? discount /
                              subTotal *
                              100m
                            : 0m;
                }

                if (discount > subTotal)
                    discount = subTotal;

                //discount = Math.Round(discount, 2, MidpointRounding.AwayFromZero);

                decimal rawTaxable = subTotal - discount;

                if (rawTaxable < 0m)
                    rawTaxable = 0m;

                // Packing charges are not discounted. If a packing tax is configured,
                // include its taxable base here; inclusive charges are split into base
                // and tax, while exclusive charges use the entered amount as the base.
                decimal packingCharge = GetDecimalValue(txtpkchrge_amt);
                if (packingCharge < 0m)
                    packingCharge = 0m;

                decimal packingTaxableAmount = 0m;
                decimal packingTaxAmount = 0m;
                decimal packingNonTaxableAmount = packingCharge;

                string packingTaxMode;
                string packingTaxType;
                decimal packingTaxPercentage;

                if (packingCharge > 0m &&
                    TryGetPackingTaxConfiguration(
                        out packingTaxMode,
                        out packingTaxType,
                        out packingTaxPercentage))
                {
                    if (string.Equals(packingTaxMode, "Inclusive",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        packingTaxableAmount = packingTaxPercentage > 0m
                            ? packingCharge * 100m / (100m + packingTaxPercentage)
                            : packingCharge;

                        packingTaxAmount = packingCharge - packingTaxableAmount;
                    }
                    else
                    {
                        packingTaxableAmount = packingCharge;
                        packingTaxAmount =
                            packingTaxableAmount * packingTaxPercentage / 100m;
                    }

                    packingTaxableAmount = Math.Round(
                        packingTaxableAmount, 2, MidpointRounding.AwayFromZero);
                    packingTaxAmount = Math.Round(
                        packingTaxAmount, 2, MidpointRounding.AwayFromZero);

                    // The entered packing amount is already inclusive or exclusive
                    // according to the saved setting, so it is no longer an untaxed add-on.
                    packingNonTaxableAmount = 0m;
                }

                decimal productPackingPrice = GetGridPackingPrice();
                decimal productPackingTax = GetGridPackingTax();

                // Product packing amounts are already gross amounts based on each
                // tblItem Inclusive/Exclusive setting. Remove their tax to get the base.
                decimal productPackingTaxableBase =
                    Math.Max(0m, productPackingPrice - productPackingTax);

                decimal totalTaxable =
                    rawTaxable + packingTaxableAmount + productPackingTaxableBase;

                decimal discountRatio = subTotal > 0m
                    ? discount / subTotal
                    : 0m;

                decimal productTax = 0m;

                foreach (DataGridViewRow row in Dgv_Selected_Item.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    if (row.Cells[iamount].Value == null || row.Cells[iamount].Value == DBNull.Value)
                        continue;

                    decimal amount = 0m;

                    decimal taxPer = 0m;

                    decimal.TryParse(Convert.ToString(row.Cells[iamount].Value), out amount);

                    decimal.TryParse(Convert.ToString(row.Cells[itaxper].Value), out taxPer);

                    string taxType = Convert.ToString(row.Cells[itax].Value) ?? "";

                    decimal preTaxAmount;

                    if (taxType.Equals("Inclusive", StringComparison.OrdinalIgnoreCase))
                    {
                        preTaxAmount = taxPer > 0m
                            ? amount * 100m / (100m + taxPer)
                            : amount;
                    }
                    else
                    {
                        preTaxAmount = amount;
                    }

                    decimal discountedPreTax = preTaxAmount * (1m - discountRatio);

                    decimal tax = discountedPreTax * taxPer / 100m;

                    productTax += tax;
                }

    
                decimal rawTotalTax = productTax + packingTaxAmount + productPackingTax;

                // Product-specific packing is already included in the taxable base and tax.
                decimal rawNetAmount =
                    totalTaxable + rawTotalTax + packingNonTaxableAmount;

                decimal netAmount = Math.Round(rawNetAmount, 2, MidpointRounding.AwayFromZero);

                decimal totalTax = Math.Round(
                    netAmount - totalTaxable - packingNonTaxableAmount,
                    2,
                    MidpointRounding.AwayFromZero);

                lblTotTaxAmnt.Text = totalTax.ToString("N2");

                lbltaxableamt.Text = totalTaxable.ToString("N2");

                lbldis.Text = discount.ToString("N2");

                txtdis_amt.Text = discount.ToString("N2");

                txtdis_per.Text = discountPer.ToString("0.##");


                decimal finalAmount = netAmount;

                decimal roundOffValue = 0m;

                if (hasRestoredRoundoffAdjustment)
                {
                    roundOffValue = restoredRoundoffAdjustment;
                    finalAmount = Math.Round(
                        netAmount + roundOffValue,
                        2,
                        MidpointRounding.AwayFromZero);
                }
                else if (currentRoundMode == RoundOffMode.Manual && originalRoundValue > 0m)
                {
                    finalAmount = originalRoundValue;

                    roundOffValue = originalRoundValue - netAmount;
                }
                else if (currentRoundMode == RoundOffMode.Auto)
                {
                    decimal roundedAmount = Math.Round(netAmount, 0, MidpointRounding.AwayFromZero);

                    roundOffValue = roundedAmount - netAmount;

                    finalAmount = roundedAmount;
                }

                lblNetAmnt.Text = finalAmount.ToString("N2");

                txtroundoff.Text = roundOffValue.ToString("N2");

                decimal received = GetDecimalValue(txtRcvdAmnt);

                lblBalcAmnt.Text = (finalAmount - received).ToString("N2");

                totQty();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  CalculateNetamount_ALl()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private decimal GetDecimalValue(Control ctrl)
        {
            if (ctrl == null) return 0;
            decimal value = 0;
            decimal.TryParse(ctrl.Text, out value);
            return value;
        }
        public void Showprint_Save(int typeno)
        {
            try
            {
                btn_refresh.Visible = true;
                btn_frm_close.Visible = true;
                int rowToRemove = 0;
                btn_offer_calc.Visible = false;
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
                        if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage) ||
                            sourcepage == Convert.ToInt32(SourcepageType.Takeawaypage))
                        {
                            // Pickup and Takeaway use the same KOT print action.
                            // Do not show the separate Save KOT button on either page.
                            btn_savekot_print.Visible = true;
                            btn_saveKOT.Visible = false;
                            btn_promo_code.Visible = true;
                            btn_offer_calc.Visible = true;
                        }
                    }
                    else if (typeno == 2)
                    {
                        btn_frm_close.Dock = DockStyle.Left;
                        btn_saveKOT.Visible = false;
                        btn_savekot_print.Visible = false;
                        btn_print_bill.Visible = true;
                        btn_PrintCopy.Visible = true;
                        btn_offer_calc.Visible = true;
                        btn_promo_code.Visible = true;
                        //btndirectpaybill.Visible = true;
                        btn_entire_desc.Visible = false;
                        if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                        {
                            btndirectpaybill.Visible = false;
                        }
                        else if (sourcepage == Convert.ToInt32(SourcepageType.Billingpage))
                        {
                            btndirectpaybill.Visible = false;
                            btn_savekot_print.Visible = false;
                            btn_print_bill.Visible = true;
                            btn_PrintCopy.Visible = true;
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
                    return;

                int mainNo = 1;
                int subNo = 1;

                for (int j = 0; j < Dgv_Selected_Item.RowCount; j++)
                {
                    var itemId = Dgv_Selected_Item.Rows[j].Cells[iqty].Value;
                    if (itemId == null || Convert.ToInt32(itemId) <= 0)//|| Convert.ToInt32(Dgv_Selected_Item.Rows[j].Cells[iMainProductStatusno].Value)== (int)MainProductStatus.Secondary
                        continue;

                    int prodStatus = Convert.ToInt32(Dgv_Selected_Item.Rows[j].Cells[iMainProductStatusno].Value);

                    // MAIN PRODUCT
                    if (prodStatus == (int)MainProductStatus.Primary)
                    {
                        Dgv_Selected_Item.Rows[j].Cells[islno].Value = mainNo;
                        Dgv_Selected_Item.Rows[j].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        mainNo++;
                        subNo = 1;
                    }
                    // SUB PRODUCT
                    else if (prodStatus == (int)MainProductStatus.Secondary)
                    {
                        Dgv_Selected_Item.Rows[j].Cells[islno].Value = subNo;
                        Dgv_Selected_Item.Rows[j].Cells[islno].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        subNo++;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page GridSlno_Refresh()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                    Select = "distinct CategoryName,c.Fcid",
                    Table = "tblProduct p  left join tblCategory_FTI c on c.Fcid = p.Fcid and c.Status = 1",
                    WhereNdGroupBy = "WHERE p.Status = 1 AND c.CategoryType = " + Convert.ToInt32(CategoryType.Menu_Category),
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
                // 4. Packing Charge Button
                // -------------------
                //Button buttonPacking = CreateSideMenuButton("Packing Charge", "btn_packing_charge");
                //buttonPacking.Click += SidebuttonPackingChargeClickEventHandler;
                //flp_sidemenu.Controls.Add(buttonPacking);

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
            int bheight = btn_showmenu.Height + (btn_showmenu.Height / 2);
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

                dmnSearch.Select = "DISTINCT p.Product as Field1,p.ProductId as Field2,0 as Field3,fav_status as Field4,p.price Field5,q.Discountper Field6,q.FinalPrice Field7,p.foodtype Field8,p.ForeColor Field9,p.BackColor Field10";

                dmnSearch.Table =
                "tblProduct p " +
                "LEFT JOIN tblMenu m on m.ProductId=p.ProductId " +
                "OUTER APPLY ( Select ofd.Discountper,ofd.FinalPrice from tblOfferDetails ofd " +
                "LEFT JOIN tblOfferMaster ofm ON ofd.omid = ofm.omid AND ofd.status = 1 " +
                "where ofm.status = 1 AND ofm.OfferType = 0 AND ofd.Producttype=1 " +
                "AND p.ProductId = ofd.ProductId " +
                "and ofm.OfferstartDatetime <= SYSDATETIME() " +
                "AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status = 1) q " +

                "WHERE p.Status=1 " +
                "AND p.Fcid=" + clickedControl.Tag + " " +
                "AND (p.IsStockTracked = 0 OR m.Quantity > 0 or m.Status=1 ) " +

                " Union all Select i.Itemname as Field1,i.ItemId as Field2,i.Saletype as Field3,i.fav_status as Field4,(SELECT MAX(SRate) FROM tblProductRate WHERE ProductId = i.ItemId) AS Field5,q.Discountper Field6,q.FinalPrice Field7,0 Field8,'' Field9,'' Field10 from " +
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
                flp_productview.SuspendLayout();
                flp_productview.Controls.Clear();

                foreach (var item in pagedatadata)
                {
                    string productName = item.Field1;
                    int productId = Convert.ToInt32(item.Field2);
                    int favStatus = Convert.ToInt32(item.Field4);
                    int Saletypeno = Convert.ToInt32(item.Field3);
                    int foodType = Convert.ToInt32(item.Field8); // Veg / NonVeg

                    Panel tile = new Panel();
                    tile.Width = tileW;
                    tile.Height = tileH;

                    if (!string.IsNullOrEmpty(item.Field10))
                    {
                        try
                        {
                            tile.BackColor = ColorTranslator.FromHtml(item.Field10);
                        }
                        catch
                        {
                            tile.BackColor = Color.White; 
                        }
                    }
                    else
                    {
                        tile.BackColor = Color.White;
                    }

                    tile.Margin = new Padding(spacing);
                    tile.Tag = productId;
                    tile.AccessibleDescription = Convert.ToString(Saletypeno);

                    // ---------- MAIN LAYOUT ----------
                    TableLayoutPanel tlpMain = new TableLayoutPanel();
                    tlpMain.Parent = tile;
                    tlpMain.RowCount = 1;
                    tlpMain.ColumnCount = 1;
                    tlpMain.Dock = DockStyle.Fill;
                    tlpMain.Tag = productId;

                    tlpMain.RowStyles.Clear();
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

                    // ---------- PRODUCT NAME ----------
                    Label lblName = new Label();
                    lblName.Text = productName;
                    lblName.TextAlign = ContentAlignment.MiddleCenter;
                    lblName.Dock = DockStyle.Fill;
                    lblName.Padding = new Padding(5);

                    if (!string.IsNullOrWhiteSpace(item.Field9))
                    {
                        try
                        {
                            lblName.ForeColor = ColorTranslator.FromHtml(item.Field9);
                        }
                        catch
                        {
                            lblName.ForeColor = Color.Black; 
                        }
                    }
                    else
                    {
                        lblName.ForeColor = Color.Black; // default
                    }

                    lblName.BackColor = Color.Transparent;
                    lblName.AutoEllipsis = true;

                    // Font adjust based on length
                    Size textSize = TextRenderer.MeasureText(lblName.Text, f11);
                    lblName.Font = (textSize.Width <= tileW - 10) ? f11 : f9;

                    tlpMain.Controls.Add(lblName, 0, 0);

                    // ---------- CLICK EVENTS ----------
                    tlpMain.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                    tile.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                    lblName.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);

                    // ---------- FAVORITE ICON (Top Right) ----------
                    PictureBox picFav = new PictureBox();
                    picFav.Name = "picFav_" + item.Field2 + "_" + item.Field3;
                    picFav.Width = 22;
                    picFav.Height = 22;
                    picFav.BackColor = Color.White;
                    picFav.SizeMode = PictureBoxSizeMode.StretchImage;
                    picFav.Location = new Point(tileW - 30, 8);
                    picFav.Tag = productId;
                    picFav.AccessibleName = favStatus == 1 ? "fav" : "notfav";
                    picFav.AccessibleDescription = Convert.ToString(Saletypeno);
                    picFav.Image = (favStatus == 1)
                        ? Properties.Resources.fav_enable
                        : Properties.Resources.fav_disable;
                    picFav.Click += buttonfavClickEventHandler;

                    tile.Controls.Add(picFav);
                    picFav.BringToFront();

                    // ---------- VEG / NON-VEG INDICATOR (Top Left Circle) ----------
                    Panel pnlType = new Panel();
                    pnlType.Width = 12;
                    pnlType.Height = 12;
                    pnlType.Location = new Point(5, 5);
                    pnlType.BackColor = Color.Transparent;

                    Color indicatorColor = Color.Gray;

                    // 🔥 Your condition here
                    if (foodType == 1)        // Veg
                        indicatorColor = Color.Green;
                    else if (foodType == 2)   // Non-Veg
                        indicatorColor = Color.Red;

                    pnlType.Paint += (s, e) =>
                    {
                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        using (SolidBrush brush = new SolidBrush(indicatorColor))
                        {
                            e.Graphics.FillEllipse(brush, 0, 0, pnlType.Width - 1, pnlType.Height - 1);
                        }
                    };

                    tile.Controls.Add(pnlType);
                    pnlType.BringToFront();

                    UIHelper.RoundCorners(tile, Productcorner, null);

                    // ---------- ADD TILE ----------
                    flp_productview.Controls.Add(tile);
                }

                flp_productview.ResumeLayout();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page GenearteNormalProduct()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order));
            }
        }

        //public void GenearteNormalProduct()
        //{
        //    try
        //    {
        //        foreach (Control c in flp_productview.Controls)
        //        {
        //            foreach (Control child in c.Controls)
        //                child.Dispose();
        //            c.Dispose();
        //        }
        //        flp_productview.Controls.Clear();
        //        GC.Collect();
        //        GC.WaitForPendingFinalizers();

        //        foreach (var item in pagedatadata)
        //        {
        //            string productName = item.Field1;
        //            int productId = Convert.ToInt32(item.Field2);
        //            decimal price = Convert.ToDecimal(item.Field5);
        //            int favStatus = Convert.ToInt32(item.Field4);
        //            int Saletypeno = Convert.ToInt32(item.Field3);
        //            decimal originalPrice = Convert.ToDecimal(item.Field5);
        //            decimal discountPrice = Convert.ToDecimal(item.Field7);
        //            decimal discountper = Convert.ToDecimal(item.Field6);

        //            Panel tile = new Panel();
        //            tile.Width = tileW;
        //            tile.Height = tileH;
        //            tile.BackColor = Color.White;
        //            tile.Margin = new Padding(spacing);
        //            tile.Tag = productId;
        //            tile.AccessibleDescription = Convert.ToString(Saletypeno);

        //            //MakeRounded(tile, 18);

        //            TableLayoutPanel tlpMain = new TableLayoutPanel();
        //            tlpMain.Parent = tile;
        //            tlpMain.RowCount = 2;
        //            tlpMain.ColumnCount = 1;
        //            tlpMain.Dock = DockStyle.Fill;
        //            tlpMain.AccessibleDescription = Convert.ToString(Saletypeno);
        //            //tlpMain.BorderStyle = BorderStyle.FixedSingle;
        //            tlpMain.Tag = productId;

        //            //tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
        //            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 85f));
        //            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));

        //            Label lblName = new Label();
        //            lblName.Text = productName;
        //            lblName.TextAlign = ContentAlignment.MiddleCenter;
        //            Size textSize = TextRenderer.MeasureText(lblName.Text, f11);
        //            lblName.Font = (textSize.Width <= tileW - 10) ? f11 : f9;
        //            lblName.Dock = DockStyle.Fill;
        //            lblName.Padding = new Padding(3);
        //            lblName.ForeColor = Color.Black;
        //            lblName.BackColor = Color.Transparent;
        //            int nameWidth = TextRenderer.MeasureText(lblName.Text, lblName.Font).Width;

        //            Label lblPrice = new Label();
        //            lblPrice.Dock = DockStyle.Fill;
        //            lblPrice.TextAlign = ContentAlignment.MiddleCenter;
        //            lblPrice.ForeColor = Color.Green;
        //            lblPrice.BackColor = Color.Transparent;
        //            lblPrice.Font = f10;
        //            if (discountper > 0)
        //            {
        //                lblPrice.Paint += (s, l) =>
        //                {
        //                    string oldP = "₹ " + originalPrice.ToString("0.00");
        //                    string newP = "₹ " + discountPrice.ToString("0.00");

        //                    SizeF oldSize = l.Graphics.MeasureString(oldP, f10Strike);
        //                    SizeF newSize = l.Graphics.MeasureString(newP, f10Bold);

        //                    float totalWidth = oldSize.Width + 10 + newSize.Width;
        //                    float startX = (lblPrice.Width - totalWidth) / 2;

        //                    l.Graphics.DrawString(oldP, f10Strike, Brushes.Gray, startX, 4);
        //                    l.Graphics.DrawString(newP, f10Bold, Brushes.Green, startX + oldSize.Width + 10, 4);
        //                };
        //            }
        //            else
        //            {
        //                if (Saletypeno == 1 && price == 0)
        //                    lblPrice.Text = "";
        //                else
        //                    lblPrice.Text = "₹ " + price.ToString("0.00");
        //            }

        //            tlpMain.Controls.Add(lblName, 0, 0);
        //            tlpMain.Controls.Add(lblPrice, 0, 1);

        //            tlpMain.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
        //            tile.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
        //            lblPrice.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
        //            lblName.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);

        //            PictureBox picFav = new PictureBox();
        //            picFav.Name = "picFav_" + item.Field2 + "_" + item.Field3;
        //            picFav.Width = 22;
        //            picFav.Height = 22;
        //            picFav.BackColor = Color.White;
        //            picFav.SizeMode = PictureBoxSizeMode.StretchImage;
        //            picFav.Location = new Point(tileW - 30, 8);
        //            picFav.Tag = productId;
        //            picFav.AccessibleName = favStatus == 1 ? "fav" : "notfav";
        //            picFav.AccessibleDescription = Convert.ToString(Saletypeno);
        //            picFav.Image = (favStatus == 1)
        //                ? Properties.Resources.fav_enable
        //                : Properties.Resources.fav_disable;
        //            picFav.Click += buttonfavClickEventHandler;

        //            tile.Controls.Add(picFav);
        //            picFav.BringToFront();

        //            flp_productview.Controls.Add(tile);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsgen.LogException("Order page GenearteNormalProduct()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
        //    }
        //}
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
                        dmnSearch.Select = "DISTINCT p.Product as Field1,p.ProductId as Field2,0 as Field3,fav_status as Field4,p.price Field5,q.Discountper Field6,q.FinalPrice Field7,p.foodtype Field8,p.ForeColor Field9,p.BackColor Field10";

                        dmnSearch.Table = "tblProduct p " +
                            "LEFT JOIN tblMenu m on m.ProductId=p.ProductId " +
                            "OUTER APPLY ( Select ofd.Discountper,ofd.FinalPrice from tblOfferDetails ofd " +
                            "LEFT JOIN tblOfferMaster ofm ON ofd.omid = ofm.omid AND ofd.status = 1 " +
                            "where ofm.status = 1 AND ofd.Producttype=1 AND ofm.OfferType = 0 " +
                            "AND p.ProductId = ofd.ProductId " +
                            "and ofm.OfferstartDatetime <= SYSDATETIME() " +
                            "AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status = 1) q";

                        dmnSearch.WhereNdGroupBy =
                            "WHERE p.Status=1 " +
                            "AND fav_status=1 " +
                            "AND (p.IsStockTracked = 0 OR m.Quantity > 0 or m.Status=1 )";
            }
                    else
            {
                dmnSearch.Select = " i.Itemname as Field1,i.ItemId as Field2,i.Saletype as Field3,i.fav_status as Field4,(SELECT MAX(SRate) FROM tblProductRate WHERE ProductId = i.ItemId) AS Field5,q.Discountper Field6,q.FinalPrice Field7,0 Field8,'' Field9,'' Field10";
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
                    int foodType = Convert.ToInt32(item.Field8);

                    decimal price = Convert.ToDecimal(item.Field5);
                    decimal discountPrice = Convert.ToDecimal(item.Field7);
                    decimal discountPer = Convert.ToDecimal(item.Field6);

                    Panel tile = new Panel
                    {
                        Width = tileW,
                        Height = tileH,
                        Margin = new Padding(spacing),
                        Tag = productId,
                        AccessibleDescription = saleType.ToString()
                    };

                    if (!string.IsNullOrEmpty(item.Field10))
                    {
                        try
                        {
                            tile.BackColor = ColorTranslator.FromHtml(item.Field10);
                        }
                        catch
                        {
                            tile.BackColor = Color.White;
                        }
                    }
                    else
                    {
                        tile.BackColor = Color.White;
                    }

                    TableLayoutPanel tlpMain = new TableLayoutPanel
                    {
                        Parent = tile,
                        RowCount = 1,
                        ColumnCount = 1,
                        Dock = DockStyle.Fill,
                        Tag = productId,
                        AccessibleDescription = saleType.ToString()
                    };

                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                    //tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 15));

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

                    if (!string.IsNullOrWhiteSpace(item.Field9))
                    {
                        try
                        {
                            lblName.ForeColor = ColorTranslator.FromHtml(item.Field9);
                        }
                        catch
                        {
                            lblName.ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        lblName.ForeColor = Color.Black; // default
                    }

                    Size textSize = TextRenderer.MeasureText(lblName.Text, f11);
                    lblName.Font = (textSize.Width <= tileW - 10) ? f11 : f9;

                    //Label lblPrice = new Label
                    //{
                    //    Dock = DockStyle.Fill,
                    //    Tag = productId,
                    //    TextAlign = ContentAlignment.MiddleCenter,
                    //    ForeColor = Color.Green,
                    //    BackColor = Color.Transparent,
                    //    Font = f10
                    //};

                    //if (discountPer > 0)
                    //{
                    //    lblPrice.Paint -= PricePaintHandler;
                    //    lblPrice.Paint += PricePaintHandler;

                    //    lblPrice.Tag = new PriceTag
                    //    {
                    //        OldPrice = price,
                    //        NewPrice = discountPrice
                    //    };
                    //}
                    //else
                    //{
                    //    lblPrice.Text = (saleType == 1 && price == 0)
                    //                   ? ""
                    //                   : "₹ " + price.ToString("0.00");
                    //}

                    tlpMain.Controls.Add(lblName, 0, 0);
                    //tlpMain.Controls.Add(lblPrice, 0, 1);

                    tile.Click += DashboardButtonClickEventHandler;
                    tlpMain.Click += DashboardButtonClickEventHandler;
                    lblName.Click += DashboardButtonClickEventHandler;

                    //lblPrice.Click += DashboardButtonClickEventHandler;

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


                    // ---------- VEG / NON-VEG INDICATOR (Top Left Circle) ----------
                    Panel pnlType = new Panel();
                    pnlType.Width = 12;
                    pnlType.Height = 12;
                    pnlType.Location = new Point(5, 5);
                    pnlType.BackColor = Color.Transparent;

                    Color indicatorColor = Color.Gray;

                    // 🔥 Your condition here
                    if (foodType == 1)        // Veg
                        indicatorColor = Color.Green;
                    else if (foodType == 2)   // Non-Veg
                        indicatorColor = Color.Red;

                    pnlType.Paint += (s, e) =>
                    {
                        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        using (SolidBrush brush = new SolidBrush(indicatorColor))
                        {
                            e.Graphics.FillEllipse(brush, 0, 0, pnlType.Width - 1, pnlType.Height - 1);
                        }
                    };

                    tile.Controls.Add(pnlType);
                    pnlType.BringToFront();

                    UIHelper.RoundCorners(tile, Productcorner, null);

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

                        UIHelper.RoundCorners(tile, Productcorner, null);

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

                        dmnSearch.Table =
                        "tblOfferDetails OD " +
                        "LEFT JOIN tblOfferMaster OM ON OD.Omid = OM.Omid " +
                        "LEFT JOIN tblProduct p ON OD.ProductId = p.ProductId " +
                        "LEFT JOIN tblMenu m ON m.ProductId = p.ProductId";

                        dmnSearch.WhereNdGroupBy =
                        "WHERE OM.Status = 1 " +
                        "AND od.Status = 1 " +
                        "AND OfferType = 0 " +
                        "AND m.Status = 1 " +
                        "AND od.Producttype = 1 " +
                        "AND GETDATE() >= OM.OfferstartDatetime " +
                        "AND GETDATE() <= OM.OfferEndDatetime " +
                        "AND (p.IsStockTracked = 0 OR m.Quantity > 0 or m.Status=1)";
                    }
                    else
                    {
                        dmnSearch.Select =
                                    @"p.Itemname AS Field1,
                                      p.ItemId AS Field2,
                                      1 AS Field3,
                                      od.Discountper AS Field4,
                                      od.Producttype AS Field5,
                                      MAX(pr.srate) AS Field6,
                                      2 AS Field8";
                        dmnSearch.Table =
 @"tblOfferDetails od
                                      JOIN tblOfferMaster om ON od.Omid = om.Omid
                                      JOIN tblItem p ON od.ProductId = p.ItemId
                                      JOIN tblProductRate pr ON pr.ProductId = p.ItemId AND pr.Status = 1";
                        dmnSearch.WhereNdGroupBy =
@"WHERE p.Saletype = 1
                                       AND p.Status = 1
                                       AND om.OfferType = 0
                                       AND GETDATE() BETWEEN om.OfferstartDatetime AND om.OfferEndDatetime
                                       AND od.Producttype = 2
                                       AND od.Status = 1
                                       AND om.Status = 1
                                     GROUP BY
                                       p.Itemname,
                                       p.ItemId,
                                       od.Discountper,
                                       od.Producttype
                                     HAVING SUM(pr.QOH) > 0";

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
                        RowCount = 1,
                        Tag = productId,
                        AccessibleDescription = Saletypeno,
                        ColumnCount = 1,
                        BackColor = Color.White,
                        Margin = new Padding(0)
                    };
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                    //tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 15));
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

                    //Label lblPrice = new Label
                    //{
                    //    Dock = DockStyle.Fill,
                    //    AutoSize = false,
                    //    Tag = productId,
                    //    AccessibleDescription = Saletypeno,
                    //    ForeColor = Color.Green,
                    //    BackColor = Color.Transparent,
                    //    TextAlign = ContentAlignment.MiddleCenter,
                    //    Font = AppFonts.GetFont(10)
                    //};
                    //lblPrice.Click += DashboardButtonClickEventHandler;

                    //lblPrice.Paint += (s, l) =>
                    //{
                    //    string oldP = "₹ " + originalPrice.ToString("0.00");
                    //    string newP = "₹ " + discountPrice.ToString("0.00");

                    //    using (Font fOld = new Font(lblPrice.Font, FontStyle.Strikeout))
                    //    using (Font fNew = new Font(lblPrice.Font, FontStyle.Bold))
                    //    {
                    //        SizeF oldSize = l.Graphics.MeasureString(oldP, fOld);
                    //        SizeF newSize = l.Graphics.MeasureString(newP, fNew);

                    //        float totalWidth = oldSize.Width + 10 + newSize.Width;
                    //        float startX = (lblPrice.Width - totalWidth) / 2;

                    //        l.Graphics.DrawString(oldP, fOld, Brushes.Gray, startX, 4);
                    //        l.Graphics.DrawString(newP, fNew, Brushes.Green, startX + oldSize.Width + 10, 4);
                    //    }
                    //};

                    tlpMain.Controls.Add(lblProductName, 0, 0);
                    //tlpMain.Controls.Add(lblPrice, 0, 1);

                    UIHelper.RoundCorners(tile, Productcorner, null);

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
                            "LEFT JOIN tblMenu m ON m.ProductId=p.ProductId and m.ProductType=0";

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
                            "(select TOP 1 srate from tblProductRate where ProductId=i.ItemId and Status=1 order by id desc) Field6," +
                            "0 Field7," +
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
                dmnSearch.Select = @"
                         om.OfferName  AS Field1,
                         om.Omid       AS Field2,
                         3             AS Field3,
                         om.Discountper AS Field4,
                         om.FinalPrice  AS Field5,
                         om.cmphid      AS Field6
                     ";

                dmnSearch.Table = @"
                     tblOfferMaster om
                     LEFT JOIN tblmenu tm ON tm.ProductId = om.Omid ";

                dmnSearch.WhereNdGroupBy = @"
                     WHERE om.Status = 1
                       AND om.OfferType = 1
                       AND om.OfferstartDatetime <= SYSDATETIME()
                       AND om.OfferEndDatetime >= SYSDATETIME()                    
                     GROUP BY om.OfferName, om.Omid, om.Discountper, om.FinalPrice, om.cmphid";// AND tm.Quantity > 0

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
                    tblMain.RowCount = 1;
                    tblMain.ColumnCount = 1;
                    //tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
                    tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                    //tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));
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

                    //Label lblPrice = new Label();
                    //lblPrice.Text = "₹ " + Convert.ToDecimal(totalPrice).ToString("0.00");
                    //lblPrice.ForeColor = Color.Green;
                    //lblPrice.BackColor = Color.Transparent;
                    //lblPrice.Font = AppFonts.GetFont(10);
                    //lblPrice.Dock = DockStyle.Fill;
                    //lblPrice.TextAlign = ContentAlignment.MiddleCenter;
                    //lblPrice.AccessibleDescription = item.Field3;
                    //lblPrice.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);

                    tblMain.Controls.Add(lblProductName, 0, 0);
                    // tblMain.Controls.Add(lblPrice, 0, 1);

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

                    UIHelper.RoundCorners(tile, Productcorner, null);

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

        #region Variation Product
        private void ShowVariation(int productId)
        {
            try
            {
                pnl_variation.Visible = true;
                ShowBlurPopup(pnl_variation);
                pnl_variation.BringToFront();

                pnl_variation.Left = (this.Width - pnl_variation.Width) / 2;
                pnl_variation.Top = (this.Height - pnl_variation.Height) / 2;

                GenerateVariationProduct(productId);
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private void GenerateVariationProduct(int productId)
        {
            try
            {
                flp_variations.SuspendLayout();
                flp_variations.Controls.Clear();

                DmnSearch dmnSearch = new DmnSearch();
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                dmnSearch.Select = "VariationId Field1, VariationName Field2";
                dmnSearch.Table = "tblProductVariation";
                dmnSearch.WhereNdGroupBy = " WHERE Status = 1 AND ProductId = " + productId;
                var variationList = bllSearch.GetSearchData("prdSearch", dmnSearch);
                foreach (var item in variationList)
                {
                    string variationName = item.Field2;
                    int variationId = Convert.ToInt32(item.Field1);

                    Panel tile = new Panel();
                    tile.Width = tileW;
                    tile.Height = tileH;
                    tile.BackColor = Color.White;
                    tile.Margin = new Padding(spacing);
                    tile.Tag = variationId;


                    TableLayoutPanel tlpMain = new TableLayoutPanel();
                    tlpMain.Parent = tile;
                    tlpMain.RowCount = 1;
                    tlpMain.ColumnCount = 1;
                    tlpMain.Dock = DockStyle.Fill;
                    tlpMain.Tag = variationId;

                    tlpMain.RowStyles.Clear();
                    tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

                    Label lblName = new Label();
                    lblName.Text = variationName;
                    lblName.TextAlign = ContentAlignment.MiddleCenter;
                    lblName.Dock = DockStyle.Fill;
                    lblName.Padding = new Padding(5);
                    lblName.ForeColor = Color.Black;
                    lblName.BackColor = Color.Transparent;
                    lblName.AutoEllipsis = true;

                    Size textSize = TextRenderer.MeasureText(lblName.Text, f11);
                    lblName.Font = (textSize.Width <= tileW - 10) ? f11 : f9;

                    tlpMain.Controls.Add(lblName, 0, 0);

                    tlpMain.Click += (s, ev) => VariationClick(productId, variationId);
                    tile.Click += (s, ev) => VariationClick(productId, variationId);
                    lblName.Click += (s, ev) => VariationClick(productId, variationId);

                    UIHelper.RoundCorners(tile, Productcorner, null);

                    flp_variations.Controls.Add(tile);
                }

                flp_variations.ResumeLayout();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }
        private string GetVariationName(int variationId)
        {
            DmnSearch dmnSearch = new DmnSearch();
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();

            dmnSearch.Select = "VariationName Field1";
            dmnSearch.Table = "tblProductVariation";
            dmnSearch.WhereNdGroupBy = " WHERE VariationId = " + variationId;

            var result = bllSearch.GetSearchData("prdSearch", dmnSearch);

            return result.Count > 0 ? result[0].Field1 : "";
        }
        private void VariationClick(int productId, int variationId)
        {
            try
            {
                pnl_variation.Visible = false;

                DmnSearch dmnSearch = new DmnSearch();
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                dmnSearch.Select = @"
                                    p.ItemCode as Field1,
                                    p.Product as Field2,
                                    v.Price as Field3,             
                                    0 as Field4,               
                                    p.ProductId as Field5,
                                    p.Tax as Field6,
                                    p.TaxType as Field7,
                                    p.Fcid as Field8,
                                    p.TaxSettingsId as Field9,
                                    q.Discountper as Field10,
                                    q.offertype as Field11,
                                    p.DiscountPer as Field12,
                                    p.PreparationMode as Field13,
                                    v.VariationId as Field14       
                                ";

                dmnSearch.Table = @"
                                    tblProduct p
                                    LEFT JOIN tblMenu m ON m.ProductId = p.ProductId

                                    INNER JOIN tblProductVariation v 
                                        ON v.ProductId = p.ProductId AND v.Status = 1

                                    OUTER APPLY (
                                        SELECT 
                                            ofd.Discountper, 
                                            ofd.FinalPrice, 
                                            ofm.offertype 
                                        FROM tblOfferDetails ofd
                                        LEFT JOIN tblOfferMaster ofm 
                                            ON ofd.omid = ofm.omid 
                                            AND ofd.status = 1
                                        WHERE 
                                            ofm.status = 1  
                                            AND ofm.OfferType = 0 
                                            AND p.ProductId = ofd.ProductId 
                                            AND ofm.OfferstartDatetime <= SYSDATETIME() 
                                            AND ofm.OfferEndDatetime >= SYSDATETIME() 
                                            AND ofm.Status = 1
                                    ) q
                                ";

                dmnSearch.WhereNdGroupBy = @"
                                            WHERE 
                                                p.Status = 1  
                                                AND p.ProductId = " + productId + @" 
                                                AND v.VariationId = " + variationId;

                dmnSearch.OrderBy = "";

                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

                if (gridSource.Count == 0)
                    return;

                bool flagexist = true;

                int prep = 0;
                bool isStockTracked = Product_isstocktrack(Convert.ToInt32(productId), 1);
                if (isStockTracked)
                {
                    var data = ProductQtyCheckMenuQty(Convert.ToInt32(productId), 0);
                    int qty = data.Quantity;
                    prep = data.PreparationMode;

                    if (qty <= 0)
                    {
                        CMessageBox.Show("Stock Out! This item is not available.",
                                        "Out of Stock",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        return;
                    }
                }

                if (string.IsNullOrEmpty(txtGrandTotal.Text))
                    txtGrandTotal.Text = "0";

                #region EXISTING ROW CHECK

                for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                {
                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) == productId
                        && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ividno].Value) == variationId
                        && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                    {
                        Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                        totalcellamount(i);
                        decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) + Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value);

                        txtGrandTotal.Text = totalamt.ToString();
                        lblNetAmnt.Text = totalamt.ToString();

                        netAmount();

                        flagexist = false;
                        break;
                    }
                }

                #endregion

                #region NEW ROW INSERT

                if (flagexist)
                {
                    int r1 = Dgv_Selected_Item.RowCount - 1;
                    if (isStockTracked)
                    {
                        ReserveQtyRouter(Convert.ToInt32(gridSource[0].Field5), Convert.ToInt32(ReserveQtyProducttype.Menu), Convert.ToInt32(ReserveQtyActiontype.Update), Convert.ToInt32(ReserveQtyTabletype.Menu), 1);
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
                    Dgv_Selected_Item.Rows[r1].Cells[iname].Value = Convert.ToString(gridSource[0].Field2) + "(" + GetVariationName(variationId) + ")";
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
                    Dgv_Selected_Item.Rows[r1].Cells[istocktrackno].Value = isStockTracked;

                    Dgv_Selected_Item.Rows[r1].Cells[iVariatiionNameno].Value = GetVariationName(variationId);
                    Dgv_Selected_Item.Rows[r1].Cells[iorgname].Value = Convert.ToString(gridSource[0].Field2);
                    Dgv_Selected_Item.Rows[r1].Cells[ividno].Value = variationId;
                    Dgv_Selected_Item.Rows[r1].Cells[ivaraiationstatusno].Value = 1;

                    totalcellamount(r1);
                    Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value);
                    Dgv_Selected_Item.Rows.Add(1);
                    GridSlno_Refresh();

                    decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                    txtGrandTotal.Text = Convert.ToString(totalamt);
                    lblNetAmnt.Text = Convert.ToString(totalamt);
                    #endregion

                    netAmount();
                }

                GridSlno_Refresh();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region All menu button Click      
        private void DashboardButtonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                if (pnl_multprice.Visible) pnl_multprice.Visible = false;

                Memoryrelease();

                ExpiredReservations();

                string Focused_tlpname = "";
                Control clickedControl = sender as Control;
                Global_ProductId = Convert.ToInt32(clickedControl.Tag);

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                if (Convert.ToInt32(clickedControl.AccessibleDescription) == 1)
                    CheckMultiplePrice(Convert.ToInt32(clickedControl.Tag));
                else if (Convert.ToInt32(clickedControl.AccessibleDescription) == 3)
                {
                    //var data = ProductQtyCheckMenuQty(Convert.ToInt32(clickedControl.Tag), 3);
                    //int qty = data.Quantity;
                    //int prep = data.PreparationMode;
                    ////if (qty <= 0 && prep == Convert.ToInt32(PreparationType.ReadyFood))
                    //if (qty <= 0)
                    //{
                    //    MessageBox.Show("Stock Out! This item is not available.", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    return;
                    //}

                    Comboproductflag = true;

                    ComboProductFill(Convert.ToInt32(clickedControl.Tag));
                }
                else
                {
                    int prep = 0;
                    bool isStockTracked = Product_isstocktrack(Convert.ToInt32(clickedControl.Tag), 1);
                    if (isStockTracked)
                    {
                        var data = ProductQtyCheckMenuQty(Convert.ToInt32(clickedControl.Tag), 0);
                        int qty = data.Quantity;
                        prep = data.PreparationMode;

                        if (qty <= 0)
                        {
                            CMessageBox.Show("Stock Out! This item is not available.",
                                            "Out of Stock",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    #region Variation Checking

                    dmnSearch.Select = "COUNT(*) Field1";
                    dmnSearch.Table = "tblProductVariation";
                    dmnSearch.WhereNdGroupBy = " WHERE Status = 1 AND ProductId = " + clickedControl.Tag;

                    var gridSource_va = bllSearch.GetSearchData("prdSearch", dmnSearch);

                    if (gridSource_va.Count > 0 &&
                        Convert.ToInt32(gridSource_va[0].Field1) > 0)
                    {
                        ShowVariation(Convert.ToInt32(clickedControl.Tag));
                        return;
                    }

                    #endregion


                    if (Convert.ToInt32(gridSource_va[0].Field1) == 0)
                    {
                        dmnSearch.Select = "p.ItemCode as Field1, p.Product as Field2,p.Price as Field3,p.MRP as Field4,p.ProductId as Field5,p.Tax as Field6,p.TaxType as Field7,p.Fcid as Field8,p.TaxSettingsId as Field9,q.Discountper as Field10,q.offertype Field11,p.DiscountPer Field12,p.PreparationMode Field13";
                        dmnSearch.Table = "tblProduct p " +
                            " LEFT JOIN tblMenu m on m.ProductId=p.ProductId " +
                            " OUTER APPLY( Select ofd.Discountper, ofd.FinalPrice, ofm.offertype from  tblOfferDetails ofd LEFT JOIN tblOfferMaster ofm ON ofd.omid= ofm.omid AND ofd.status= 1 where ofm.status= 1  AND ofm.OfferType= 0 AND p.ProductId= ofd.ProductId and ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status = 1)q";
                        dmnSearch.WhereNdGroupBy = "where p.Status=1 and p.ProductId= " + clickedControl.Tag;
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

                                    isStockTracked = Product_isstocktrack(Convert.ToInt32(clickedControl.Tag), 1);
                                    if (isStockTracked)
                                    {
                                        var dataresult = ProductQtyCheckMenuQty(Convert.ToInt32(clickedControl.Tag), 0);
                                        decimal typeqty = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                                        int availableQty = dataresult.Quantity;
                                        prep = dataresult.PreparationMode;
                                        //if (prep == Convert.ToInt32(PreparationType.ReadyFood) && typeqty > availableQty)
                                        if (typeqty > availableQty)
                                        {
                                            CMessageBox.Show($"Only {availableQty} Qty Available.",
                                                            "Stock Warning",
                                                            MessageBoxButtons.OK,
                                                            MessageBoxIcon.Warning);
                                            return;
                                        }

                                        ReserveQtyRouter(Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value), Convert.ToInt32(ReserveQtyProducttype.Menu), Convert.ToInt32(ReserveQtyActiontype.Update), Convert.ToInt32(ReserveQtyTabletype.Menu), 1);
                                    }

                                    Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                                    totalcellamount(i);
                                    decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                                    txtGrandTotal.Text = Convert.ToString(totalamt);
                                    lblNetAmnt.Text = Convert.ToString(totalamt);
                                    netAmount();
                                    flagexist = false;
                                    break;
                                }
                            }
                            #endregion

                            #region New Product
                            if (flagexist)
                            {
                                ResetNetAmount();

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
                                isStockTracked = Product_isstocktrack(Convert.ToInt32(clickedControl.Tag), 1);
                                if (isStockTracked)
                                {
                                    ReserveQtyRouter(Convert.ToInt32(gridSource[0].Field5), Convert.ToInt32(ReserveQtyProducttype.Menu), Convert.ToInt32(ReserveQtyActiontype.Update), Convert.ToInt32(ReserveQtyTabletype.Menu), 1);
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
                                Dgv_Selected_Item.Rows[r1].Cells[istocktrackno].Value = isStockTracked;

                                totalcellamount(r1);
                                Dgv_Selected_Item.Rows[r1].Cells[ioriginalprice].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value);
                                Dgv_Selected_Item.Rows.Add(1);
                                GridSlno_Refresh();

                                decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[r1].Cells[itotal].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                                txtGrandTotal.Text = Convert.ToString(totalamt);
                                lblNetAmnt.Text = Convert.ToString(totalamt);

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
            finally
            {
                Comboproductflag = false;
            }
        }

        public void ReserveQtyRouter(int ProductId, int ProductType, int Actiontype, int TableType, int OrdQty, int CQtyFlag = 0, int porderqty = 0)
        {
            DmnMenu dmnMenu = new DmnMenu
            {
                ProductId = ProductId,
                ProductType = ProductType,
                OrderQty = OrdQty,
                OrderReservedQty = OrdQty,
                Actiontype = Actiontype,
                TableType = TableType,
                CustomQtyFlag = CQtyFlag,
                PreviousOrderQty = porderqty
            };

            BllQtyReserve bllReserve = new BllQtyReserve();

            switch (Actiontype)
            {
                case 1: // Reserve
                    bllReserve.UpdateReserveQty("prdReserve_All", dmnMenu);
                    break;

                case 2: // Cancel
                    bllReserve.CancelReserveQty("prdReserve_All", dmnMenu);
                    break;

                case 3: // Reset all
                    bllReserve.ResetAllReservations("prdReserve_All", dmnMenu);
                    break;

                default:
                    throw new ArgumentException("Invalid Actiontype: " + Actiontype);
            }
        }

        private void ExpiredReservations()
        {
            try
            {
                DmnOrderMaster dmnRemove_order = new DmnOrderMaster();
                BllOrder bll_order = new BllOrder();
                var InsertStatusP = bll_order.ReleaseQty("prdReleaseExpiredReservations", dmnRemove_order);
                if (InsertStatusP != null)
                {
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btncancelnotify_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order)); // Replace 'YourFormType' with the appropriate form type
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
                ShowBlurPopup(pnl_variation_addon);

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
                if (ptype == 3)
                {
                    dmnSearch.Select = "tm.Quantity Field1,tm.OrderReservedQty Field3, PreparationMode Field2";
                    dmnSearch.Table = " tblmenu tm ";
                    dmnSearch.WhereNdGroupBy = " WHERE ProductId  = " + pid + " AND Status = 1 and ProductType=3 ";
                }

                else
                {
                    dmnSearch.Select = "Quantity Field1,OrderReservedQty Field3, PreparationMode Field2";
                    dmnSearch.Table = "tblMenu";
                    dmnSearch.WhereNdGroupBy = " WHERE ProductId = " + pid + " AND Status = 1 and ProductType = 0 ";
                }

                var result = bllSearch.GetSearchData("prdSearch", dmnSearch);

                if (result != null && result.Count > 0)
                {
                    return new MenuQtyResult
                    {
                        Quantity = Convert.ToInt32(result[0].Field1) - Convert.ToInt32(result[0].Field3),
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
        public bool Product_isstocktrack(int pid, int ptype)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();

                if (ptype == 1)
                {
                    dmnSearch.Select = "IsStockTracked AS Field1";
                    dmnSearch.Table = "tblProduct";
                    dmnSearch.WhereNdGroupBy = "WHERE ProductId = " + pid + " AND Status = 1";
                }

                var result = bllSearch.GetSearchData("prdSearch", dmnSearch);

                if (result != null && result.Count > 0)
                {
                    return Convert.ToBoolean(result[0].Field1); // ✅ return actual value
                }

                return false;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page Product_isstocktrack()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order));

                return false;
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

                            //var dataresult = ProductQtyCheckMenuQty(Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value), 3);
                            //decimal typeqty = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                            //int availableQty = dataresult.Quantity;
                            //prep = dataresult.PreparationMode;
                            ////if (prep == Convert.ToInt32(PreparationType.ReadyFood) && typeqty > availableQty)
                            //if (typeqty > availableQty)
                            //{
                            //    MessageBox.Show($"Only {availableQty} Qty Available.",
                            //                    "Stock Warning",
                            //                    MessageBoxButtons.OK,
                            //                    MessageBoxIcon.Warning);
                            //    return;
                            //}

                            flagexist = false;

                            ReserveQtyRouter(Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value), Convert.ToInt32(ReserveQtyProducttype.Combo), Convert.ToInt32(ReserveQtyActiontype.Update), Convert.ToInt32(ReserveQtyTabletype.Menu), 1);

                            Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                            totalcellamount(i);
                            decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                            txtGrandTotal.Text = Convert.ToString(totalamt);
                            lblNetAmnt.Text = Convert.ToString(totalamt);
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
                        ReserveQtyRouter(Convert.ToInt32(gridSource[0].Id), Convert.ToInt32(ReserveQtyProducttype.Combo), Convert.ToInt32(ReserveQtyActiontype.Update), Convert.ToInt32(ReserveQtyTabletype.Menu), 1);

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
                        Dgv_Selected_Item.Rows[r1].DefaultCellStyle.BackColor = Color.White;
                        Dgv_Selected_Item.Rows[r1].Cells[iComboTaxamtno].Value = Convert.ToDecimal(gridSource[0].Field12);

                        Dgv_Selected_Item.Rows[r1].Cells[itaxamt].Value = Convert.ToDecimal(gridSource[0].Field12);
                        Dgv_Selected_Item.Rows[r1].Cells[icombotaxamt].Value = Convert.ToDecimal(gridSource[0].Field12);
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
                        lblNetAmnt.Text = Convert.ToString(totalamt);
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
                // 🔴 Clear previous controls safely
                foreach (Control c in flp_multprice.Controls.Cast<Control>().ToList())
                {
                    c.Click -= MyButtonClickEventHandler;
                    c.Dispose();
                }
                flp_multprice.Controls.Clear();

                // 🔍 Get data
                BllSearch<DmnGridFields> bllSearch1 = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();

                objSearch.Select = "i.Itemname Field1, pr.SRate Field2, pr.QOH Field3, pr.Id Field4, i.ItemId Field5";
                objSearch.Table = "tblitem i LEFT JOIN tblProductRate pr ON pr.ProductId = i.ItemId";
                objSearch.WhereNdGroupBy = "WHERE i.status = 1 AND pr.QOH > 0 AND i.itemid = " + id;

                var gridSource = bllSearch1.GetSearchData("prdSearch", objSearch);

                // ✅ Add ONLY price buttons
                if (gridSource.Count > 1)
                {
                    flp_multprice.SuspendLayout();

                    foreach (var item in gridSource)
                    {
                        Button priceButton = new Button();

                        priceButton.Text = $"₹ {item.Field2}";
                        priceButton.Name = item.Field1.ToString();
                        priceButton.Tag = item.Field5;                     // ProductId
                        priceButton.AccessibleDescription = item.Field4;   // ProductRateId

                        priceButton.BackgroundImage = FoodandBeverage.Properties.Resources.Button_Price_change;
                        priceButton.BackgroundImageLayout = ImageLayout.Stretch;
                        priceButton.FlatStyle = FlatStyle.Flat;
                        priceButton.FlatAppearance.BorderSize = 0;
                        priceButton.ForeColor = Color.White;
                        priceButton.Font = new Font("Georgia", 14F);
                        priceButton.Width = 300;
                        priceButton.Height = 70;
                        priceButton.Margin = new Padding(10);

                        priceButton.Click += MyButtonClickEventHandler;

                        flp_multprice.Controls.Add(priceButton);
                    }

                    flp_multprice.ResumeLayout();

                    UIHelper.CenterAndResize(pnl_multprice, 35, 50);
                    UIHelper.RoundCorners(pnl_multprice, 25);
                    pnl_multprice.BringToFront();
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
                                        ReserveQtyRouter(Convert.ToInt32(gridSource[0].Field4), Convert.ToInt32(ReserveQtyProducttype.Menu), Convert.ToInt32(ReserveQtyActiontype.Update), Convert.ToInt32(ReserveQtyTabletype.Direct), 1);
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
                                ReserveQtyRouter(Convert.ToInt32(gridSource[0].Field4), Convert.ToInt32(ReserveQtyProducttype.Menu), Convert.ToInt32(ReserveQtyActiontype.Update), Convert.ToInt32(ReserveQtyTabletype.Direct), 1);

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

        private void MyButtonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                Productrate_ID = Convert.ToInt32(clickedControl.Tag);
                pnl_multprice.Visible = false;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();

                dmnSearch.Select = "Itemname as Field1,ItemId as Field2,ItemCode_UPC as Field3,pr.prate as Field4,pr.srate as Field5," +
                "pr.MRP as Field6,i.Tax as Field7,i.TaxType as Field8,i.TaxSettingsId as Field9,pr.QOH as Field10," +
                "i.unit as Field11,u.UnitName as Field12,i.Fcid as Field13,i.TaxSettingsId as Field14,pr.id Field15";
                dmnSearch.Table = "tblItem i LEFT JOIN tblProductRate pr on  pr.ProductId=i.ItemId LEFT JOIN tblUnit u on  u.UId=i.unit";
                dmnSearch.WhereNdGroupBy = "where i.Status=1 and pr.ProductId = " + clickedControl.Tag + " and pr.id = " + Convert.ToInt32(clickedControl.AccessibleDescription);
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);


                if (Booking_Status == false)
                {
                    if (string.IsNullOrEmpty(txtGrandTotal.Text))
                        txtGrandTotal.Text = "0";
                }


                int rowno = 0; bool Itemfoundflag = true;
                for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                {
                    if (Convert.ToInt32(gridSource[0].Field15) == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.DirectSale) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                    {
                        decimal cqoh = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                        if (cqoh > Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value))
                        {
                            CMessageBox.Show("Aviablle Quantity is : " + Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value), "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            txtitemcode_serach.Text = "";
                            txtitemcode_serach.Focus();
                            return;
                        }
                        ReserveQtyRouter(Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value), Convert.ToInt32(ReserveQtyProducttype.Menu), Convert.ToInt32(ReserveQtyActiontype.Update), Convert.ToInt32(ReserveQtyTabletype.Direct), 1);

                        Dgv_Selected_Item.Rows[i].Cells[iqty].Value = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) + 1;
                        totalcellamount(i);

                        decimal totalamt = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value) + Convert.ToDecimal(txtGrandTotal.Text);
                        txtGrandTotal.Text = Convert.ToString(totalamt);
                        netAmount();

                        Itemfoundflag = false;
                        break;
                    }
                }

                if (itemcode_serach_Status)
                {
                    txtitemcode_serach.Text = "";
                    txtitemcode_serach.Focus();
                    itemcode_serach_Status = false;
                }

                if (Itemfoundflag)
                {
                    #region Grid Inseration
                    ReserveQtyRouter(Convert.ToInt32(gridSource[0].Field15), Convert.ToInt32(ReserveQtyProducttype.Menu), Convert.ToInt32(ReserveQtyActiontype.Update), Convert.ToInt32(ReserveQtyTabletype.Direct), 1);
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
                }

                if (itemcode_serach_Status)
                {
                    txtitemcode_serach.Text = "";
                    txtitemcode_serach.Focus();
                    itemcode_serach_Status = false;
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
                    if (!Comboproductflag)
                    {
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
                                Dgv_Selected_Item.Rows[cellproductid].Cells[ipretaxamtno].Value = amount;
                            }
                            if (Convert.ToString(Dgv_Selected_Item.Rows[cellproductid].Cells[itax].Value) == "Inclusive")
                            {
                                decimal tot = (amount - Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[idisamt].Value));
                                Dgv_Selected_Item.Rows[cellproductid].Cells[itotal].Value = tot.ToString("N2");
                                Dgv_Selected_Item.Rows[cellproductid].Cells[ipretaxamtno].Value = amount - Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[itaxamt].Value);
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
                    else
                    {
                        Dgv_Selected_Item.Rows[cellproductid].Cells[itaxamt].Value = Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[icombotaxamt].Value) * Convert.ToDecimal(Dgv_Selected_Item.Rows[cellproductid].Cells[iqty].Value);
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
                    decimal TaxAmount = 0;
                    decimal originalCost = amount;
                    decimal gstPercent = 0, vatPercent = 0;
                    if (gridSource[0].TaxType == "GST")
                    {
                        TaxAmount = 0;
                        originalCost = amount;
                        gstPercent = Convert.ToDecimal(gridSource[0].IgstPrcnt);
                        if (Convert.ToString(Dgv_Selected_Item.Rows[cellproductid].Cells[itax].Value) == "Exclusive")
                        {
                            Dgv_Selected_Item.Rows[cellproductid].Cells[itaxper].Value = (Convert.ToDecimal(gridSource[0].IgstPrcnt));
                            TaxAmount = Convert.ToDecimal((Convert.ToDecimal(gridSource[0].IgstPrcnt) * amount) / 100);
                            Dgv_Selected_Item.Rows[cellproductid].Cells[itaxamt].Value = (TaxAmount).ToString("N2");
                            Dgv_Selected_Item.Rows[cellproductid].Cells[iigstamtno].Value = (TaxAmount).ToString("N2");
                        }
                        else if (Convert.ToString(Dgv_Selected_Item.Rows[cellproductid].Cells[itax].Value) == "Inclusive")
                        {
                            Dgv_Selected_Item.Rows[cellproductid].Cells[itaxper].Value = (Convert.ToDecimal(gridSource[0].IgstPrcnt));
                            TaxAmount = originalCost * (100 / (100 + gstPercent));
                            Dgv_Selected_Item.Rows[cellproductid].Cells[itaxamt].Value = (originalCost - TaxAmount).ToString("N2");
                            Dgv_Selected_Item.Rows[cellproductid].Cells[iigstamtno].Value = (originalCost - TaxAmount).ToString("N2");
                        }

                        Dgv_Selected_Item.Rows[cellproductid].Cells[itaxtype].Value = gridSource[0].TaxType;

                    }

                    else if (gridSource[0].TaxType == "VAT")
                    {
                        TaxAmount = 0;
                        originalCost = amount;
                        vatPercent = Convert.ToDecimal(gridSource[0].VatPrcnt);
                        if (Convert.ToString(Dgv_Selected_Item.Rows[cellproductid].Cells[itax].Value) == "Exclusive")
                        {
                            Dgv_Selected_Item.Rows[cellproductid].Cells[itaxper].Value = (Convert.ToDecimal(gridSource[0].VatPrcnt));
                            TaxAmount = Convert.ToDecimal((Convert.ToDecimal(gridSource[0].VatPrcnt) * amount) / 100);
                        }
                        else if (Convert.ToString(Dgv_Selected_Item.Rows[cellproductid].Cells[itax].Value) == "Inclusive")
                        {
                            Dgv_Selected_Item.Rows[cellproductid].Cells[itaxper].Value = (Convert.ToDecimal(gridSource[0].VatPrcnt));
                            TaxAmount = originalCost - (originalCost * (100 / (100 + vatPercent)));
                        }

                        Dgv_Selected_Item.Rows[cellproductid].Cells[itaxamt].Value = TaxAmount.ToString("N2");
                        Dgv_Selected_Item.Rows[cellproductid].Cells[ivatamtno].Value = TaxAmount.ToString("N2");
                        Dgv_Selected_Item.Rows[cellproductid].Cells[itaxtype].Value = gridSource[0].TaxType;

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

        #region Memory
        public void Memoryrelease()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
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

        #region Serach Product With Name & Code

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

                    dmnSearch.Select = "OfferName as Field1,Omid as Field2,3 as Field3,Discountper as Field4,FinalPrice Field5,cmphid Field6,1 Field8,0 Field9";
                    dmnSearch.Table = "tblOfferMaster";
                    dmnSearch.WhereNdGroupBy =
                        " where Status=1 AND OfferType=1 AND OfferstartDatetime <= SYSDATETIME() " +
                        " AND OfferEndDatetime >= SYSDATETIME() and OfferName like '%" + txtname_search.Text.Trim() + "%'";

                    var queryResult = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    ProductData.AddRange(queryResult);
                    #endregion

                    #region Offer product and Without offer product
                    dmnSearch.Select =
    "p.Product as Field1,p.ProductId as Field2,0 as Field3,fav_status as Field4,p.price Field5,ofd.Discountper Field6,ofd.FinalPrice Field7,2 Field8,p.FoodType Field9";

                    dmnSearch.Table =
                    "tblProduct p " +
                    "LEFT JOIN tblMenu m on m.ProductId=p.ProductId " +
                    "LEFT JOIN tblOfferMaster ofm ON ofm.status=1 AND ofm.OfferType=0 " +
                    "AND ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() " +
                    "LEFT JOIN tblOfferDetails ofd ON ofd.omid=ofm.omid AND ofd.status=1 AND p.ProductId=ofd.ProductId " +

                    "WHERE p.Status=1 " +
                    "AND (p.IsStockTracked = 0 OR m.Quantity > 0 or m.Status=1 ) " +
                    "AND p.Product LIKE '%" + txtname_search.Text.Trim() + "%'" +

                    " Union Select i.Itemname as Field1,i.ItemId as Field2,i.Saletype as Field3,i.fav_status as Field4,(select SRate from tblProductRate ) Field5,ofm.Discountper Field6,ofd.FinalPrice Field7,2 Field8,0 Field9 from " +
                        " tblItem i " +
                        " LEFT JOIN tblOfferMaster ofm ON  ofm.status=1  AND ofm.OfferType=0 AND  ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status=1 LEFT JOIN tblOfferDetails ofd ON ofd.omid=ofm.omid AND ofd.status=1 ";
                    dmnSearch.WhereNdGroupBy =
                        " where  i.Saletype=1 and i.Status=1  " +
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

        private void ResetNetAmount()
        {
            try
            {

                txtdis_per.Text = "0.000";
                txtdis_amt.Text = "0.000";

                decimal totalAmount = 0;

                for (int i = 0; i < Dgv_Selected_Item.Rows.Count; i++)
                {
                    var row = Dgv_Selected_Item.Rows[i];
                    if (row.Cells[iitemid].Value != null && row.Cells[iitemid].Value.ToString() != "")
                    {
                        row.Cells[isalediamtno].Value = "0.000";
                        row.Cells[iSaledisperno].Value = "0.000";

                        decimal rowTotal = 0;
                        decimal.TryParse(Convert.ToString(row.Cells[itotal].Value), out rowTotal);
                        totalAmount += rowTotal;
                    }
                }

                txtGrandTotal.Text = totalAmount.ToString("0.00");
                lblNetAmnt.Text = totalAmount.ToString("0.00");

                FinalTax();

                TotalCGST = Math.Round(TotalTax / 2m, 2, MidpointRounding.AwayFromZero);
                TotalSGST = Math.Round(TotalTax / 2m, 2, MidpointRounding.AwayFromZero);
                TotalPreTaxAmt = Math.Round(totalAmount - TotalTax, 2, MidpointRounding.AwayFromZero);

                DiscountApplied = false;
                DiscountAmountApplied = false;

                netAmount();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error resetting amounts: " + ex.Message);
            }
        }
        private void btn_offer_calc_Click(object sender, EventArgs e)
        {
            try
            {
                decimal kdata = 0m;
                using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.Number)))
                {
                    frm.ShowDialog();

                    if (!decimal.TryParse(frm.txtkeypadvalue.Text, out kdata))
                    {
                        return;
                    }
                }

                decimal productSubTotal = CalculateSubTotalValue();

                decimal subTotal =Math.Round(productSubTotal,2,MidpointRounding.AwayFromZero);

                if (Frmnumdiskeypad.flagpercentage)
                {
                    decimal disPer = kdata;

                    if (disPer < 0m || disPer > 100m)
                    {
                        MessageBox.Show("Invalid Discount %");
                        return;
                    }

                    decimal discountAmount =Math.Round(subTotal * disPer /100m,2,MidpointRounding.AwayFromZero);

                    txtdis_per.Text = disPer.ToString("0.##");

                    txtdis_amt.Text = discountAmount.ToString("N2");

                    lbldis.Text = discountAmount.ToString("N2");
                }
                else
                {
                    decimal discAmt = kdata;
                    if (discAmt < 0m || discAmt > subTotal)
                    {
                        MessageBox.Show("Invalid Discount Amount");
                        return;
                    }

                    discAmt =Math.Round(discAmt,2,MidpointRounding.AwayFromZero);

                    decimal discountPer =
                        subTotal > 0m
                            ? discAmt /
                              subTotal *
                              100m
                            : 0m;

                    discountPer = Math.Round(discountPer,2,MidpointRounding.AwayFromZero);

                    txtdis_amt.Text = discAmt.ToString("N2");

                    txtdis_per.Text = discountPer.ToString("0.##");

                    lbldis.Text =discAmt.ToString("N2");
                }

                bool hasDiscount = GetDecimalValue(txtdis_amt) > 0m;

                lbl_cap_discountamt.Visible =  hasDiscount;
                lbldis.Visible = hasDiscount;

                DiscountApplied = true;
                DiscountAmountApplied = !UserControl.Frmnumdiskeypad.flagpercentage;

                CalculateNetamount_ALl();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btn_offer_calc_Click()", ex,FrmLogin.EmployeName,FrmLogin.Designation,Convert.ToInt32(FormType.Order));
            }

            #region  Old code
            //try
            //{
            //    decimal enteredValue = 0;
            //    using (UserControl.Frmnumdiskeypad frm = new UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.Number)))
            //    {
            //        frm.ShowDialog();
            //        enteredValue = string.IsNullOrEmpty(frm.txtkeypadvalue.Text)
            //            ? 0
            //            : Convert.ToDecimal(frm.txtkeypadvalue.Text);
            //    }

            //    decimal grossTotal = GetGrossTotal();
            //    if (grossTotal <= 0) return;

            //    decimal salePercent = 0;
            //    decimal saleAmount = 0;

            //    if (UserControl.Frmnumdiskeypad.flagpercentage)
            //    {
            //        salePercent = enteredValue;
            //        saleAmount = (grossTotal * salePercent) / 100m;
            //    }
            //    else
            //    {
            //        saleAmount = enteredValue;
            //        if (saleAmount > grossTotal)
            //        {
            //            CMessageBox.Show("Discount amount cannot be greater than total amount",
            //                            "Invalid Discount",
            //                            MessageBoxButtons.OK,
            //                            MessageBoxIcon.Warning);
            //            return;
            //        }

            //        salePercent = (saleAmount * 100m) / grossTotal;
            //    }

            //    txtdis_per.Text = salePercent.ToString("0.000");
            //    txtdis_amt.Text = saleAmount.ToString("0.000");

            //    //tlp_discountshow.Visible = true;
            //    //lbldiscdata.Text = salePercent.ToString("0.00") + "% - ₹" + saleAmount.ToString("0.00") + "";
            //    //UIHelper.RoundCorners(tlp_discountshow, 15);

            //    for (int i = 0; i < Dgv_Selected_Item.Rows.Count; i++)
            //    {
            //        var row = Dgv_Selected_Item.Rows[i];
            //        if (row.Cells[iitemid].Value != null && row.Cells[iitemid].Value.ToString() != "")
            //        {
            //            decimal rowAmount = Convert.ToDecimal(row.Cells[iamount].Value ?? 0);

            //            decimal rowDiscountAmount = (rowAmount * salePercent) / 100m;

            //            row.Cells[isalediamtno].Value = rowDiscountAmount.ToString("0.000");
            //            row.Cells[iSaledisperno].Value = salePercent.ToString("0.000");
            //        }
            //    }

            //    decimal discountedGrossTotal = grossTotal - saleAmount;
            //    txtGrandTotal.Text = discountedGrossTotal.ToString("0.00");
            //    lblNetAmnt.Text = discountedGrossTotal.ToString("0.00");

            //    FinalTax(); 

            //    decimal discountedTax = TotalTax - (TotalTax * salePercent / 100m);

            //    TotalCGST = discountedTax / 2m;
            //    TotalSGST = discountedTax / 2m;

            //    TotalPreTaxAmt = discountedGrossTotal - discountedTax;

            //    DiscountApplied = true;
            //    DiscountAmountApplied = !UserControl.Frmnumdiskeypad.flagpercentage;

            //    netAmount();
            //}
            //catch (Exception ex)
            //{
            //    clsgen.LogException("Order page btn_offer_calc_Click()",
            //        ex,
            //        FrmLogin.EmployeName,
            //        FrmLogin.Designation,
            //        Convert.ToInt32(FormType.Order));
            //}
            #endregion
        }

        private void CalculateDiscountTaxAmt()
        {
            try
            {
                FinalTax();

                decimal discountPerc = 0;
                if (!decimal.TryParse(txtdis_per.Text, out discountPerc))
                    discountPerc = 0;

                decimal netAmount = 0;
                if (!decimal.TryParse(lblNetAmnt.Text, out netAmount))
                    netAmount = 0;

                // Packing tax is added after the product discount, matching the
                // order total calculation. Apply the discount only to product tax.
                bool packingTaxIsGst;
                decimal packingTaxAmount = GetPackingTaxAmount(out packingTaxIsGst);

                decimal productPackingGstTax = GetGridPackingGstTax();
                decimal productPackingVatTax = GetGridPackingVatTax();

                // Do not discount either the entered packing charge or the
                // product-specific packaging tax calculated from tblItem.
                decimal productIgst = Math.Max(
                    0m,
                    TotalIGST - (packingTaxIsGst ? packingTaxAmount : 0m) - productPackingGstTax);
                decimal productVat = Math.Max(
                    0m,
                    TotalVAT - (!packingTaxIsGst ? packingTaxAmount : 0m) - productPackingVatTax);

                productIgst = Math.Round(
                    productIgst * (1m - discountPerc / 100m),
                    2,
                    MidpointRounding.AwayFromZero);

                productVat = Math.Round(
                    productVat * (1m - discountPerc / 100m),
                    2,
                    MidpointRounding.AwayFromZero);

                TotalIGST = productIgst +
                    (packingTaxIsGst ? packingTaxAmount : 0m) + productPackingGstTax;
                TotalVAT = productVat +
                    (!packingTaxIsGst ? packingTaxAmount : 0m) + productPackingVatTax;
                TotalCGST = Math.Round(TotalIGST / 2m, 2, MidpointRounding.AwayFromZero);
                TotalSGST = Math.Round(TotalIGST / 2m, 2, MidpointRounding.AwayFromZero);
                TotalTax = TotalIGST + TotalVAT;

                TotalPreTaxAmt = Math.Round(netAmount - TotalTax, 2, MidpointRounding.AwayFromZero);

            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page CalculateDiscountTaxAmt()", ex, FrmLogin.EmployeName,  FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

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
                        if (Convert.ToDecimal(lblNetAmnt.Text) < Convert.ToDecimal(gridSource[0].Field1))
                        {
                            CMessageBox.Show("The net amount must be greater than the applicable total for the promo code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            txtpromocode.Enabled = false;
                            btn_save_promo.Enabled = false;
                            decimal disAmnt = (Convert.ToDecimal(gridSource[0].Field5) * Convert.ToDecimal(lblNetAmnt.Text)) / 100;
                            if (disAmnt < Convert.ToDecimal(gridSource[0].Field2))
                            {
                                decimal ftotal = Convert.ToDecimal(lblNetAmnt.Text) - disAmnt;
                                txtdiscountamt_promo.Text = disAmnt.ToString("N");
                                txtdisamt_promo.Text = Convert.ToString(ftotal);
                            }
                            else
                            {
                                decimal ftotal = Convert.ToDecimal(lblNetAmnt.Text) - Convert.ToDecimal(gridSource[0].Field2);
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

        private void btn_sleteted_list_gridview_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnl_orderprority.Visible == false)
                {
                    UIHelper.CenterAndResize(pnl_orderprority, 80, 60);
                    UIHelper.RoundCorners(pnl_orderprority, 25);
                    UIHelper.RoundCorners(tlp_orderprority_nav, 25);
                }
                else
                {
                    pnl_orderprority.Visible = false;
                }

                //if (pnl_gridview.Visible == false)
                //{
                //    UIHelper.CenterAndResize(pnl_gridview, 80, 60);
                //    UIHelper.RoundCorners(pnl_gridview, 25);
                //    UIHelper.RoundCorners(tlp_gridview_nav, 25);
                //    UIHelper.RoundCorners(tlp_productgridview, 25);
                //}
                //else
                //{
                //    pnl_gridview.Visible = false;
                //}
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btn_save_product_des_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void btnordercallist_Click(object sender, EventArgs e)
        {
            try
            {
                cancel_byKitchen = true;
                if (pnl_cancelledlist.Visible == false)
                {
                    //UIHelper.CenterAndResize(pnl_cancelledlist, 50, 40);
                    //UIHelper.RoundCorners(pnl_cancelledlist, 25);
                    //UIHelper.RoundCorners(tlp_cancelled_list_nav, 25);

                    CheckPickupkitchenCancel();
                }
                else
                {
                    pnl_Refund_paymode.Visible = false;
                    pnl_cancelledlist.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btnordercallist_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

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
            else if (sourcepage == Convert.ToInt32(SourcepageType.Takeawaypage))
                ResetTakeawayViewPage();

            clsgen.Memoryrelease();

            this.Close();

        }

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

            if (System.Windows.Forms.Application.OpenForms["FrmTakeaway"] != null)
            {
                FrmTakeaway _instanceFrmTakeaway = Application.OpenForms["FrmTakeaway"] as FrmTakeaway;
                _instanceFrmTakeaway.Visible = false;
            }

            if (System.Windows.Forms.Application.OpenForms["FrmTableView"] != null)
            {
                FrmTableView _instanceFrmTableView = Application.OpenForms["FrmTableView"] as FrmTableView;
                _instanceFrmTableView.Visible = true;
                _instanceFrmTableView.RefreshTableOrders();


            }
        }
        public void ResetTakeawayViewPage()
        {
            if (System.Windows.Forms.Application.OpenForms["FrmPickup"] != null)
            {
                FrmPickup _instanceFrmPickup = Application.OpenForms["FrmPickup"] as FrmPickup;
                _instanceFrmPickup.Visible = false;
            }

            if (System.Windows.Forms.Application.OpenForms["FrmTableView"] != null)
            {
                FrmTableView _instanceFrmTableView = Application.OpenForms["FrmTableView"] as FrmTableView;
                _instanceFrmTableView.Visible = false;

            }
            if (System.Windows.Forms.Application.OpenForms["FrmTakeaway"] != null)
            {
                FrmTakeaway _instanceFrmTakeaway = Application.OpenForms["FrmTakeaway"] as FrmTakeaway;
                _instanceFrmTakeaway.Visible = true;
                _instanceFrmTakeaway.UpdateTakeawayView(0);
            }
        }
        public void ResetPickupViewPage()
        {
            if (System.Windows.Forms.Application.OpenForms["FrmTableView"] != null)
            {
                FrmTableView _instanceFrmTableView = Application.OpenForms["FrmTableView"] as FrmTableView;
                _instanceFrmTableView.Visible = false;
            }
            if (System.Windows.Forms.Application.OpenForms["FrmTakeaway"] != null)
            {
                FrmTakeaway _instanceFrmTakeaway = Application.OpenForms["FrmTakeaway"] as FrmTakeaway;
                _instanceFrmTakeaway.Visible = false;
            }
            if (System.Windows.Forms.Application.OpenForms["FrmPickup"] != null)
            {
                FrmPickup _instanceFrmPickup = Application.OpenForms["FrmPickup"] as FrmPickup;
                _instanceFrmPickup.Visible = true;
                _instanceFrmPickup.UpdatePickupView(0);
            }
        }
        #endregion

        private void btn_promocls_Click(object sender, EventArgs e)
        {
            pnl_promocode.Visible = false;
        }

        private void btn_promo_reset_Click(object sender, EventArgs e)
        {
            txtdisamt_promo.Text = "";
            txtdiscountamt_promo.Text = "0";
            txtpromocode.Text = "0";
            btn_promo_reset.Visible = false;
            btn_save_promo.Enabled = true;
            txtpromocode.Enabled = true;
            netAmount();
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
                            //if (prep == Convert.ToInt32(PreparationType.ReadyFood) && typeqty > availableQty)
                            if (typeqty > availableQty)
                            {

                                CMessageBox.Show($"Only {availableQty} Qty Available.",
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
                            lblNetAmnt.Text = Convert.ToString(totalamt);
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
                        Dgv_Selected_Item.Rows[r1].Cells[iname].Value = Convert.ToString(gridSource[0].Field2) + " ( " + vrianame + " )";
                        Dgv_Selected_Item.Rows[r1].Cells[iorgname].Value = Convert.ToString(gridSource[0].Field2);
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
                        lblNetAmnt.Text = Convert.ToString(totalamt);
                        netAmount();

                        #endregion

                        checksave_refresh();
                    }

                    GridSlno_Refresh();
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

        private void btn_va_cancel_Click(object sender, EventArgs e)
        {
            try
            {
                DisposeControls(flp_addons);
                DisposeControls(flp_variation);

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

        private void btn_orderdes_cls_Click(object sender, EventArgs e)
        {
            pnl_order_desc.Visible = false;
        }

        private void btn_orderdesc_submit_Click(object sender, EventArgs e)
        {
            pnl_order_desc.Visible = false;
        }

        private void btn_pa__toggle_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            tlp_netalc_buttons.SuspendLayout();
            flp_oper_buttons.SuspendLayout();

            try
            {
                if (!isOperationShown)
                {
                    textb_shape_paymnet();

                    Control currentControl = tlp_netalc_buttons.GetControlFromPosition(0, 0);

                    if (currentControl != null &&
                        currentControl != flp_oper_buttons)
                    {
                        currentControl.Visible = false;
                        tlp_netalc_buttons.Controls.Remove(currentControl);
                    }

                    flp_oper_buttons.AutoSize = false;
                    flp_oper_buttons.WrapContents = true;
                    flp_oper_buttons.FlowDirection = FlowDirection.LeftToRight;

                    flp_oper_buttons.Dock = DockStyle.Top;
                    flp_oper_buttons.Anchor =
                        AnchorStyles.Top |
                        AnchorStyles.Left |
                        AnchorStyles.Right;

                    if (!tlp_netalc_buttons.Controls.Contains(flp_oper_buttons))
                    {
                        tlp_netalc_buttons.Controls.Add(
                            flp_oper_buttons,
                            0,
                            0);
                    }

                    flp_oper_buttons.Visible = true;
                    flp_oper_buttons.BringToFront();

                    flp_oper_buttons.PerformLayout();
                    ConfigureResponsiveOperationButtons();

                    isOperationShown = true;
                }
                else
                {

                    flp_oper_buttons.Visible = false;

                    if (tlp_netalc_buttons.Controls.Contains(flp_oper_buttons))
                    {
                        tlp_netalc_buttons.Controls.Remove(flp_oper_buttons);
                    }


                    if (!tlp_netalc_buttons.Controls.Contains(tlp_calculation_total))
                    {
                        tlp_netalc_buttons.Controls.Add(tlp_calculation_total, 0, 0);
                    }

                    tlp_calculation_total.Dock = DockStyle.Fill;

                    if (tlp_netalc_buttons.RowStyles.Count > 0)
                    {
                        tlp_netalc_buttons.RowStyles[0].SizeType = SizeType.Percent;

                        tlp_netalc_buttons.RowStyles[0].Height = 100;
                    }

                    tlp_calculation_total.Visible = true;
                    tlp_calculation_total.BringToFront();

                    isOperationShown = false;
                }
            }
            finally
            {
                flp_oper_buttons.ResumeLayout(false);
                tlp_netalc_buttons.ResumeLayout(false);
                this.ResumeLayout(false);

                tlp_netalc_buttons.PerformLayout();
                ConfigureResponsiveOperationButtons();
                tlp_netalc_buttons.Invalidate();
            }
        }

        private void ConfigureResponsiveOperationButtons()
        {
            if (_isConfiguringOperationButtons ||
                flp_oper_buttons == null ||
                flp_oper_buttons.IsDisposed ||
                tlp_netalc_buttons == null ||
                tlp_netalc_buttons.IsDisposed ||
                !tlp_netalc_buttons.Controls.Contains(flp_oper_buttons) ||
                !flp_oper_buttons.Visible)
                return;

            _isConfiguringOperationButtons = true;

            try
            {
                const int minimumButtonWidth = 76;
                const int maximumButtonWidth = 120;
                const int horizontalGap = 6;
                const int buttonMargin = 3;

                int availableWidth = Math.Max(
                    1,
                    tlp_netalc_buttons.ClientSize.Width -
                    flp_oper_buttons.Margin.Horizontal -
                    tlp_netalc_buttons.Padding.Horizontal);

                // Keep the operation strip fitted to the live table cell width.
                flp_oper_buttons.Width = availableWidth;

                int columnCount = Math.Max(
                    1,
                    Math.Min(4, (availableWidth + horizontalGap) /
                        (minimumButtonWidth + horizontalGap)));

                int availableButtonWidth = Math.Max(
                    1,
                    (availableWidth - (columnCount * horizontalGap)) / columnCount);

                int buttonWidth = Math.Min(maximumButtonWidth, availableButtonWidth);

                int buttonHeight = Math.Max(
                    40,
                    Math.Min(84, (int)Math.Round(buttonWidth * 0.69)));

                foreach (Control control in flp_oper_buttons.Controls)
                {
                    Button operationButton = control as Button;
                    if (operationButton == null)
                        continue;

                    operationButton.Size = new Size(buttonWidth, buttonHeight);
                    operationButton.Margin = new Padding(buttonMargin);
                }

                flp_oper_buttons.AutoSize = false;
                flp_oper_buttons.WrapContents = true;
                flp_oper_buttons.FlowDirection = FlowDirection.LeftToRight;
                flp_oper_buttons.AutoScroll = false;
                flp_oper_buttons.Dock = DockStyle.Top;
                flp_oper_buttons.Anchor =
                    AnchorStyles.Top |
                    AnchorStyles.Left |
                    AnchorStyles.Right;

                flp_oper_buttons.PerformLayout();

                // Re-evaluate wrapping after the panel and its buttons have their new widths.
                flp_oper_buttons.ResumeLayout(true);
                flp_oper_buttons.SuspendLayout();

                int visibleButtonCount = 0;
                foreach (Control control in flp_oper_buttons.Controls)
                {
                    if (control.Visible)
                        visibleButtonCount++;
                }

                int rowCount = Math.Max(
                    1,
                    (visibleButtonCount + columnCount - 1) / columnCount);

                int requiredHeight =
                    (rowCount * (buttonHeight + (buttonMargin * 2))) +
                    flp_oper_buttons.Padding.Vertical +
                    4;

                flp_oper_buttons.Height = requiredHeight;

                if (tlp_netalc_buttons.RowStyles.Count > 0)
                {
                    tlp_netalc_buttons.RowStyles[0].SizeType = SizeType.Absolute;
                    tlp_netalc_buttons.RowStyles[0].Height = requiredHeight;
                }

                tlp_netalc_buttons.PerformLayout();
                flp_oper_buttons.ResumeLayout(true);
                flp_oper_buttons.PerformLayout();
                flp_oper_buttons.Invalidate();
            }
            finally
            {
                _isConfiguringOperationButtons = false;
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
    "p.Product as Field1,p.ProductId as Field2,0 as Field3,fav_status as Field4,p.price Field5,ofd.Discountper Field6,ofd.FinalPrice Field7,2 Field8,p.FoodType Field9";

                    dmnSearch.Table =
                    "tblProduct p " +
                    "LEFT JOIN tblMenu m on m.ProductId=p.ProductId " +
                    "LEFT JOIN tblOfferMaster ofm ON ofm.status=1 AND ofm.OfferType=0 " +
                    "AND ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() " +
                    "AND ofm.Status=1 " +
                    "LEFT JOIN tblOfferDetails ofd ON ofd.omid=ofm.omid AND ofd.status=1 AND p.ProductId=ofd.ProductId " +

                    "WHERE p.Status=1 " +
                    "AND (p.IsStockTracked = 0 OR m.Quantity > 0 or m.Status=1 ) " +
                    "AND p.ItemCode = '" + txtitemcode_serach.Text.Trim() + "'" +

                    " Union Select i.Itemname as Field1,i.ItemId as Field2,i.Saletype as Field3,i.fav_status as Field4,(select SRate from tblProductRate ) Field5,ofm.Discountper Field6,ofd.FinalPrice Field7,2 Field8,0 Field9 from " +
                        " tblItem i " +
                        " LEFT JOIN tblOfferMaster ofm ON  ofm.status=1  AND ofm.OfferType=0 AND  ofm.OfferstartDatetime <= SYSDATETIME() AND ofm.OfferEndDatetime >= SYSDATETIME() and ofm.Status=1 LEFT JOIN tblOfferDetails ofd ON ofd.omid=ofm.omid AND ofd.status=1 ";
                    dmnSearch.WhereNdGroupBy =
                        " where  i.Saletype=1 and i.Status=1  " +
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
                        int foodType = Convert.ToInt32(item.Field9);

                        #region Design Product (One Panel + One TLP + 3 Rows)

                        Panel tile = new Panel();
                        tile.Width = tileW;
                        tile.Height = tileH;
                        tile.BackColor = Color.White;
                        tile.Margin = new Padding(spacing);
                        tile.Tag = productId;
                        tile.BorderStyle = BorderStyle.None;
                        tile.AccessibleDescription = item.Field3;

                        //MakeRounded(tile, 18);

                        TableLayoutPanel tlpMain = new TableLayoutPanel();
                        tlpMain.Parent = tile;
                        tlpMain.RowCount = 1;
                        tlpMain.ColumnCount = 1;
                        tlpMain.Dock = DockStyle.Fill;
                        tlpMain.BackColor = Color.White;
                        tlpMain.AutoSize = false;
                        tlpMain.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                        tlpMain.AccessibleDescription = item.Field3;

                        //tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
                        tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                        //tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));
                        tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

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

                        //Label lblPrice = new Label();
                        //lblPrice.AutoSize = false;
                        //lblPrice.Dock = DockStyle.Fill;
                        //lblPrice.TextAlign = ContentAlignment.MiddleCenter;
                        //lblPrice.Font = AppFonts.GetFont(10);
                        //lblPrice.ForeColor = Color.Green;
                        //lblPrice.BackColor = Color.Transparent;
                        //if (discountper > 0)
                        //{
                        //    lblPrice.Paint += (s, l) =>
                        //    {
                        //        string oldP = "₹ " + originalPrice.ToString("0.00");
                        //        string newP = "₹ " + discountPrice.ToString("0.00");

                        //        SizeF oldSize = l.Graphics.MeasureString(oldP, f10Strike);
                        //        SizeF newSize = l.Graphics.MeasureString(newP, f10Bold);

                        //        float totalWidth = oldSize.Width + 10 + newSize.Width;
                        //        float startX = (lblPrice.Width - totalWidth) / 2;

                        //        l.Graphics.DrawString(oldP, f10Strike, Brushes.Gray, startX, 4);
                        //        l.Graphics.DrawString(newP, f10Bold, Brushes.Green, startX + oldSize.Width + 10, 4);
                        //    };

                        //}
                        //else
                        //{
                        //    if (Saletypeno == 1 && price == 0)
                        //        lblPrice.Text = "";
                        //    else
                        //        lblPrice.Text = "₹ " + price.ToString("0.00");
                        //}

                        tlpMain.Controls.Add(lblName, 0, 0);
                        //tlpMain.Controls.Add(lblPrice, 0, 1);

                        tlpMain.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                        tile.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                        // lblPrice.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                        lblName.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);

                        PictureBox picFav = new PictureBox();
                        picFav.Name = "picFav_" + item.Field2 + "_" + item.Field3;
                        picFav.Tag = item.Field2;
                        picFav.AccessibleDescription = item.Field3;
                        picFav.AccessibleName = favStatus == 1 ? "fav" : "notfav";
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

                        // ---------- VEG / NON-VEG INDICATOR (Top Left Circle) ----------
                        Panel pnlType = new Panel();
                        pnlType.Width = 12;
                        pnlType.Height = 12;
                        pnlType.Location = new Point(5, 5);
                        pnlType.BackColor = Color.Transparent;

                        Color indicatorColor = Color.Gray;

                        // 🔥 Your condition here
                        if (foodType == 1)        // Veg
                            indicatorColor = Color.Green;
                        else if (foodType == 2)   // Non-Veg
                            indicatorColor = Color.Red;

                        pnlType.Paint += (s, e) =>
                        {
                            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            using (SolidBrush brush = new SolidBrush(indicatorColor))
                            {
                                e.Graphics.FillEllipse(brush, 0, 0, pnlType.Width - 1, pnlType.Height - 1);
                            }
                        };

                        tile.Controls.Add(pnlType);
                        pnlType.BringToFront();

                        //
                        UIHelper.RoundCorners(tile, Productcorner, null);

                        flp_productview.Controls.Add(tile);

                        #endregion
                    }
                    else if (Convert.ToInt32(item.Field8) == 1)
                    {
                        string offerName = item.Field1;
                        int comboId = Convert.ToInt32(item.Field2);
                        int cmphid = Convert.ToInt32(item.Field6);
                        string totalPrice = item.Field5;
                        int foodType = Convert.ToInt32(item.Field9);

                        #region Design Product

                        Panel tile = new Panel();
                        tile.Width = tileW;
                        tile.Height = tileH;
                        tile.BackColor = Color.White;
                        tile.Margin = new Padding(spacing);
                        tile.Tag = comboId;
                        tile.BorderStyle = BorderStyle.None;
                        tile.AccessibleDescription = item.Field3;
                        //MakeRounded(tile, 18);

                        TableLayoutPanel tblMain = new TableLayoutPanel();
                        tblMain.Parent = tile;
                        tblMain.Dock = DockStyle.Fill;
                        tblMain.BackColor = Color.Transparent;
                        tblMain.RowCount = 1;
                        tblMain.ColumnCount = 1;
                        tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 60f));
                        tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 25f));
                        tblMain.RowStyles.Add(new RowStyle(SizeType.Percent, 15f));
                        tblMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));


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

                        //Label lblPrice = new Label();
                        //lblPrice.Text = "₹ " + Convert.ToDecimal(totalPrice).ToString("0.00");
                        //lblPrice.ForeColor = Color.Green;
                        //lblPrice.BackColor = Color.Transparent;
                        //lblPrice.Font = AppFonts.GetFont(10);
                        //lblPrice.Dock = DockStyle.Fill;
                        //lblPrice.TextAlign = ContentAlignment.MiddleCenter;
                        //lblPrice.AccessibleDescription = item.Field3;

                        tblMain.Controls.Add(lblProductName, 0, 0);
                        //tblMain.Controls.Add(lblPrice, 0, 1);

                        tblMain.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                        tile.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
                        //lblPrice.Click += (s, ev) => DashboardButtonClickEventHandler(tile, ev);
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

                        // ---------- VEG / NON-VEG INDICATOR (Top Left Circle) ----------
                        Panel pnlType = new Panel();
                        pnlType.Width = 12;
                        pnlType.Height = 12;
                        pnlType.Location = new Point(5, 5);
                        pnlType.BackColor = Color.Transparent;

                        Color indicatorColor = Color.Gray;

                        // 🔥 Your condition here
                        if (foodType == 1)        // Veg
                            indicatorColor = Color.Green;
                        else if (foodType == 2)   // Non-Veg
                            indicatorColor = Color.Red;

                        pnlType.Paint += (s, e) =>
                        {
                            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            using (SolidBrush brush = new SolidBrush(indicatorColor))
                            {
                                e.Graphics.FillEllipse(brush, 0, 0, pnlType.Width - 1, pnlType.Height - 1);
                            }
                        };

                        tile.Controls.Add(pnlType);
                        pnlType.BringToFront();
                        //


                        UIHelper.RoundCorners(tile, Productcorner, null);

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

        #region  Save & Print Bill & kitchen send 

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
        private Thread _orderConsoleLoadingThread;
        private LoadingGIF_Timer _orderConsoleLoadingForm;

        private void StartOrderConsoleLoadingAnimation()
        {
            if (_orderConsoleLoadingThread != null && _orderConsoleLoadingThread.IsAlive)
                return;

            _orderConsoleLoadingThread = new Thread(() =>
            {
                using (LoadingGIF_Timer loader = new LoadingGIF_Timer())
                {
                    loader.StartPosition = FormStartPosition.CenterScreen;
                    loader.TitleText = "Saving order";
                    loader.StatusText = "Saving items and sending the kitchen order…";
                    _orderConsoleLoadingForm = loader;
                    try { Application.Run(loader); }
                    finally { _orderConsoleLoadingForm = null; }
                }
            });
            _orderConsoleLoadingThread.IsBackground = true;
            _orderConsoleLoadingThread.SetApartmentState(ApartmentState.STA);
            _orderConsoleLoadingThread.Start();
            Thread.Sleep(100);
        }

        private void StopOrderConsoleLoadingAnimation()
        {
            LoadingGIF_Timer loader = _orderConsoleLoadingForm;
            if (loader != null && !loader.IsDisposed)
            {
                try { loader.BeginInvoke(new Action(() => loader.Close())); }
                catch { /* The animation may already be closing. */ }
            }

            if (_orderConsoleLoadingThread != null && _orderConsoleLoadingThread.IsAlive &&
                Thread.CurrentThread != _orderConsoleLoadingThread)
                _orderConsoleLoadingThread.Join(500);

            _orderConsoleLoadingThread = null;
        }

        private void btn_saveKOT_Click(object sender, EventArgs e)
        {
            StartOrderConsoleLoadingAnimation();
            try
            {
                //using (FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(204, payid, 0, Convert.ToInt32(BillType.NormalBill)))
                //{
                //    frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                //    frmSalesInvoice.ShowDialog();
                //}
                //this.Close();

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


                    dmnordermaster.FirstName = txtfristname.Text.Trim();      // First name
                    dmnordermaster.SecondName = txtsecondname.Text.Trim();     // Second name
                    dmnordermaster.CompanyName = txtcompanyname.Text.Trim();    // Company
                    dmnordermaster.GSTNo = txtgstno.Text.Trim();          // GST
                    dmnordermaster.PhoneNumber = txtph.Text.Trim();             // Phone number
                    dmnordermaster.EmailAddress = txtemailid.Text.Trim();        // Email
                    dmnordermaster.FullAddress = txtaddress.Text.Trim();        // Address
                    if (rb_male.Checked)
                        dmnordermaster.Gender = 1;        // Male
                    else if (rb_female.Checked)
                        dmnordermaster.Gender = 2;        // Female
                    dmnordermaster.cusid = Convert.ToInt32(txtcusid.Text);

                    if (BookingType == Convert.ToInt32(DashboardTableType.PickupOrder))
                    {
                        #region Pickup Table Inseration
                        DmnPickup dmnDmnPickup = new DmnPickup();
                        BllPickup bll_Pickup = new BllPickup();
                        dmnDmnPickup.PkId = Pickup_ID;
                        dmnDmnPickup.CusName = Convert.ToString(txtfristname.Text) + Convert.ToString(txtsecondname.Text);
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

                        dmnDmnPickup.NetAmount = Convert.ToDecimal(lblNetAmnt.Text);
                        dmnDmnPickup.CashRev = Convert.ToDecimal(txtRcvdAmnt.Text);

                        dmnDmnPickup.BalnceAmt = Convert.ToDecimal(lblBalcAmnt.Text);
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
                    dmnordermaster.NetAmount = Convert.ToDecimal(lblNetAmnt.Text);
                    dmnordermaster.Kitchennumber = Convert.ToString(txtkitchenslrno.Text);

                    dmnordermaster.OrderPlatformmasterName = " [ " + System.Environment.MachineName + " ]  -" + Dns.GetHostByName(System.Environment.MachineName).AddressList[0].ToString();
                    dmnordermaster.OrderPlatformmaster = 1;
                    dmnordermaster.AdultCount = Convert.ToInt32(txtadults.Text);
                    dmnordermaster.ChildCount = Convert.ToInt32(txtchild.Text);
                    dmnordermaster.OtherGuestCount = Convert.ToInt32(txtother.Text);
                    dmnordermaster.TotalGuestCount = Convert.ToInt32(totguests.Text);

                    dmnordermaster.OrderTypeId = OrderTypeId;
                    var InsertStatusP = bll_order.AddEditData("prdInsert_Upd_OrderMaster", dmnordermaster);
                    if (InsertStatusP.OrderMasterId > 0)
                    {
                        ResetFlag(Convert.ToInt32(txtordermasterid.Text));

                        for (int i = 0; i < Dgv_Selected_Item.Rows.Count; i++)
                        {
                            #region Reservation
                            if (Booking_Status)
                            {

                                if (Dgv_Selected_Item.Rows[i].Cells[iitemid].Value != null && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) > 0)
                                {
                                    dmnorderdetail.OrderMasterId = Convert.ToInt32(InsertStatusP.OrderMasterId);
                                    dmnorderdetail.ItemBarCode = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ibarcode].Value);

                                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.ComboProduct))
                                        dmnorderdetail.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iproducttypeno].Value);
                                    else
                                    {
                                        dmnorderdetail.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iname].Value);
                                    }

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
                                    dmnorderdetail.VariatiionName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iVariatiionNameno].Value);
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
                            #endregion

                            else
                            {
                                if (Dgv_Selected_Item.Rows[i].Cells[iqty].Value != null && Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iqty].Value) > 0 && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[isubofferstatus].Value) == Convert.ToInt32(SubofferStatus.None) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                                {
                                    dmnorderdetail.OrderMasterId = Convert.ToInt32(InsertStatusP.OrderMasterId);
                                    dmnorderdetail.ItemBarCode = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ibarcode].Value);

                                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.ComboProduct))
                                        dmnorderdetail.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iproducttypeno].Value);
                                    else
                                    {
                                        if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ividno].Value) > 0 && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value) != 2)
                                            dmnorderdetail.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iorgname].Value);
                                        else
                                            dmnorderdetail.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iname].Value);
                                    }

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
                                    dmnorderdetail.VariatiionName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iVariatiionNameno].Value);

                                    if (txtentrie_descr.Text == "Description Type here .....")
                                        dmnorderdetail.MainRemarks = "";
                                    else
                                        dmnorderdetail.MainRemarks = txtentrie_descr.Text;

                                    dmnorderdetail.BranchId = FrmDashboard.BranchId;
                                    dmnorderdetail.CreatedBy = FrmDashboard.UserId;

                                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iPreparationModeno].Value) == Convert.ToInt32(KOTRouting.Counter))
                                        dmnorderdetail.Status = 3;
                                    else
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

                                    dmnorderdetail.OrderPlatformdetailsName = " [ " + System.Environment.MachineName + " ]  -" + Dns.GetHostByName(System.Environment.MachineName).AddressList[0].ToString();
                                    dmnorderdetail.OrderPlatformdetails = 1;

                                    dmnorderdetail.orderpriority = cmb_proritytype.SelectedIndex >= 0 ? cmb_proritytype.SelectedIndex + 1 : 1;
                                    dmnorderdetail.ReprintStatus = chk_reprint_status.Checked ? 1 : 0;

                                    dmnorderdetail.PreparationMode = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iPreparationModeno].Value);
                                    dmnorderdetail.IsStockTracked = Convert.ToBoolean(Dgv_Selected_Item.Rows[i].Cells[istocktrackno].Value);

                                    dmnorderdetail.PreTaxAmt = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ipretaxamtno].Value);

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

                        if (!flage && OrderTypeId > 0 && InsertStatusP != null &&
                            InsertStatusP.OrderMasterId > 0)
                        {
                            bll_order.RefreshOrderTokenStatus(
                                "prdOrderToken_RefreshStatus",
                                InsertStatusP.OrderMasterId,
                                FrmDashboard.UserId);
                        }

                        PrintOrderNokot(Convert.ToInt32(KOTbilltype.Tableorder), InsertStatusP != null ? InsertStatusP.OrderMasterId : 0);
                    }
                    if (flage == false)
                    {
                        clsgen.Memoryrelease();

                        if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                            ResetTableViewPage();
                        else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                            ResetPickupViewPage();
                        else if (sourcepage == Convert.ToInt32(SourcepageType.Takeawaypage))
                            ResetTakeawayViewPage();
                        
                        if (FrmDashboard.UserSavePrompt)
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
            finally
            {
                StopOrderConsoleLoadingAnimation();
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

        private void btndirectpaybill_Click(object sender, EventArgs e)
        {
            if (MandatoryCheck_KOT())
            {
                DirectPayBill = true;
                UIHelper.CenterAndResize(pnlPaymode, 40, 45);
                pnl_main.Enabled = false;

            }
        }
        private string GetOrderTokenDisplay(int orderMasterId)
        {
            if (orderMasterId <= 0)
                return string.Empty;

            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "TokenDisplay AS Field1";
                dmnSearch.Table = "tblOrderToken";
                dmnSearch.WhereNdGroupBy = "WHERE OrderMasterId = " + orderMasterId;
                dmnSearch.OrderBy = "";

                var tokenRows = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (tokenRows != null && tokenRows.Count > 0)
                    return Convert.ToString(tokenRows[0].Field1).Trim();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page GetOrderTokenDisplay()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

            return string.Empty;
        }

        private void PrintOrderNokot(int btype, int orderMasterId = 0)
        {
            string tokenDisplay = GetOrderTokenDisplay(orderMasterId);
            ReportDocument reportDocument = new ReportDocument();

            try
            {
                #region Crystal Report Declaration
                DataSet dsReport = new DataSet();
                DataTable dtSalesDetail = new DataTable("DT_kitchenbill");

                List<KitchenProcessedRow> processedData = new List<KitchenProcessedRow>();

                decimal totalQty = 0;
                for (int i = 0; i < Dgv_Selected_Item.Rows.Count; i++)
                {
                    DataGridViewRow row = Dgv_Selected_Item.Rows[i];
                    if (row.IsNewRow) continue;
                    if (row.Cells[iqty].Value == null) continue;
                    if (Convert.ToDecimal(row.Cells[iqty].Value) <= 0) continue;
                    if (Convert.ToInt32(row.Cells[istatusno].Value) == 0) continue;

                    totalQty = Convert.ToDecimal(row.Cells[iqty].Value);
                }

                for (int i = 0; i < Dgv_Selected_Item.Rows.Count; i++)
                {

                    DataGridViewRow row = Dgv_Selected_Item.Rows[i];
                    if (row.IsNewRow) continue;

                    if (Convert.ToInt32(row.Cells[iPreparationModeno].Value) == Convert.ToInt32(KOTRouting.kitchen)) continue;

                    if (row.Cells[iqty].Value == null) continue;

                    if (Convert.ToDecimal(row.Cells[iqty].Value) <= 0) continue;
                    if (Convert.ToInt32(row.Cells[istatusno].Value) == 0) continue;

                    string itemName = Convert.ToString(row.Cells[iname].Value);
                    string variationName = Convert.ToString(row.Cells[ivaraiationstatusno].Value);

                    if (!string.IsNullOrEmpty(variationName))
                        itemName += " (" + variationName + ")";

                    string avId = Convert.ToString(row.Cells[iavid].Value);
                    string mainCategory = string.IsNullOrEmpty(avId)
                        ? itemName + row.Cells[iitemid].Value
                        : avId;

                    processedData.Add(new KitchenProcessedRow
                    {
                        ItemName = itemName,
                        Quantity = Convert.ToDecimal(row.Cells[iqty].Value),
                        Notes = "",
                        Description = txtentrie_descr.Text,
                        TableCode = lbltablecode.Text,
                        OrderMasterId = Convert.ToInt32(txtsalemasterid.Text),
                        TableType = Convert.ToInt32(row.Cells[itabletype].Value),
                        AddonStatus = Convert.ToInt32(row.Cells[ivaraiationstatusno].Value),
                        offeritemname = "",
                        offerqty = 0,
                        Kitchennumber = "",
                        MainCategory = mainCategory,
                        TotalQTY = totalQty,
                        Pkid = CounterKotNo,
                        EmployeeName = FrmLogin.EmployeName,
                        Station = "",
                        GuestDetails = " 'PAX : ( A :" + txtadults.Text + " C:" + txtchild.Text + " O:" + txtother.Text + " )",
                        Orderno = ResetFlagvalue,
                        PriorityType = 1,
                        CounterKotNo = CounterKotNo,
                        Billtype = btype,
                        TokenNo = tokenDisplay,
                    });
                }


                for (int i = 0; i < processedData.Count; i++)
                {
                    int count = 0;
                    for (int j = 0; j < processedData.Count; j++)
                    {
                        if (processedData[i].MainCategory == processedData[j].MainCategory)
                            count++;
                    }
                    processedData[i].MainCategoryCount = count;
                }

                if (processedData.Count > 0)
                {
                    using (var reader = ObjectReader.Create(
                        processedData,
                        "ItemName", "Quantity", "Notes", "Description",
                        "TableCode", "OrderMasterId", "MainCategory",
                        "TableType", "AddonStatus", "MainCategoryCount",
                        "offeritemname", "offerqty", "Kitchennumber",
                        "TotalQTY", "Pkid", "EmployeeName", "Station",
                        "GuestDetails", "Orderno", "PriorityType", "CounterKotNo", "Billtype", "TokenNo"))
                    {
                        dtSalesDetail.Load(reader);
                    }

                    dsReport.Tables.Add(dtSalesDetail);
                }
                if (processedData.Count > 0)
                {
                    // ================================
                    // 4. CRYSTAL REPORT PRINT
                    // ================================
                    string reportPath = RPTPath + "CRkitchenbillnokot.rpt";

                    if (!File.Exists(reportPath))
                        throw new FileNotFoundException("Report file not found.", reportPath);

                    reportDocument.Load(reportPath);
                    reportDocument.SetDataSource(dsReport);
                    crvkitchenbill.ReportSource = reportDocument;
                    reportDocument.PrintToPrinter(1, false, 0, 0);
                }
                #endregion
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  PrintOrderNokot()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
            finally
            {
                //reportDocument.Close();
                //reportDocument.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        public class KitchenProcessedRow
        {
            public string ItemName { get; set; }
            public decimal Quantity { get; set; }
            public string Notes { get; set; }
            public string Description { get; set; }
            public string TableCode { get; set; }
            public int OrderMasterId { get; set; }
            public int TableType { get; set; }
            public int AddonStatus { get; set; }
            public string offeritemname { get; set; }
            public decimal offerqty { get; set; }
            public string Kitchennumber { get; set; }
            public string MainCategory { get; set; }
            public int MainCategoryCount { get; set; }
            public decimal TotalQTY { get; set; }
            public int Pkid { get; set; }
            public string EmployeeName { get; set; }
            public string Station { get; set; }
            public string GuestDetails { get; set; }
            public int Orderno { get; set; }
            public int PriorityType { get; set; }
            public int CounterKotNo { get; set; }
            public int Billtype { get; set; }
            public string TokenNo { get; set; }

        }
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            try
            {
                currentRoundMode = RoundOffMode.None;
                originalRoundValue = 0;
                isRoundValueCaptured = false;
                hasRestoredRoundoffAdjustment = false;
                restoredRoundoffAdjustment = 0m;
                txtroundoff.Text = "0.00";

                DiscountApplied = false;

                ordercancelbystaff = false;
                Memoryrelease();

                lbltablecode.Visible = true;
                cmb_tblcode.Visible = false;
                textb_shape();
                RFID_Status = false;
                Checked_VariationAddon_List.Clear();
                lblsizetotal.Text = "0";
                lbladdontotal.Text = "0";
                addon_Vari_Id = "";
                Split_Rowno = 0;
                txtdisamt_promo.Text = "0";
                txtpromocode.Text = "";
                BillCopy_Status = false;
                txtpkchrge_amt.Text = "0";
                KOT_saleprint_status = false;

                lblTotTaxAmnt.Text = "0";
                lbltaxableamt.Text = "0";
                lbldis.Text = "0";
                txtdis_amt.Text = "0";
                txtdis_per.Text = "0";
                lblNetAmnt.Text = "0";
                txtroundoff.Text = "0";
                lblBalcAmnt.Text = "0";
                lblsubtotal.Text = "0";

                if (OrderRunning_Status)
                {
                    Promodetailsid = 0;
                    Promomasterid = 0;
                    CheckRe_order();

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

                    if (OrderCancelStatus == 1)
                    {
                        btn_cancle_pickup.Visible = false;
                        btnordercallist.PerformClick();
                    }
                    else
                        btn_cancle_pickup.Visible = true;
                }
                else if (combined_Status)
                {
                    Promodetailsid = 0;
                    Promomasterid = 0;
                    int count = checkedtableNames.Count;
                    lblorderid.Visible = false;
                    label10.Visible = false;

                    Combined_Tables();
                    Combined_Table_Bill();

                    txtpromocode.Enabled = true;
                    btn_save_promo.Enabled = true;

                    Dgv_Selected_Item.Rows.Add();
                }
                else if (OrderTypeId == 2)
                {
                    // Keep the Takeaway console in its own mode after refresh.
                    ClearAll();
                    checksave_refresh();
                    lbltablecode_header.Text = "TAKEAWAY";
                    lblorderid.Visible = false;
                    label10.Visible = false;
                    cmb_tblcode.Visible = false;
                    lbltablecode.Visible = false;
                    btn_cancle_pickup.Visible = false;
                }
                else
                {
                    ClearAll();
                    checksave_refresh();
                }
                if (tableId > 0 && OmId <= 0 && OrderRunning_Status == false &&
                    Pickup_Status == false && OrderCancelStatus != 1)
                {
                    UIHelper.CenterAndResize(pnl_Guests, 35, 50);
                    UIHelper.RoundCorners(pnl_Guests, 25);
                    UIHelper.RoundCorners(tlp_guest_nav, 25);
                    UIHelper.RoundCorners(tlp_Guests_main, 25);
                    pnl_Guests.BringToFront();
                }

                AddEmptyRowIfNeeded();

                //tlp_discountshow.Visible = false;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_refresh_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                    case "btncomple":
                        payid = Convert.ToInt32(PaymentType.COMPLEMENTRY);

                        if (EnableCompReasonPrompt)
                        {
                            UIHelper.CenterAndResize(pnl_complementry_reason, 35, 50);
                            UIHelper.RoundCorners(pnl_complementry_reason, 25);
                            UIHelper.RoundCorners(tlp_comp_rea_nav, 25);
                            UIHelper.RoundCorners(tlp_com_reason, 25);
                            pnl_complementry_reason.BringToFront();
                            pnl_complementry_reason.Visible = true;
                            ShowBlurPopup(pnl_complementry_reason);
                        }
                        else
                        {
                            paybillcal(payid);
                        }
                        break;
                    case "btnnocharge":
                        payid = Convert.ToInt32(PaymentType.Nocharge);
                        break;
                    case "btn_other":
                        pnlPaymode.Visible = false;
                        btnPartPaynow.Visible = false;
                        payid = Convert.ToInt32(PaymentType.SPLIT);
                        pnlSplit.Visible = true;
                        ShowBlurPopup(pnlSplit);
                        pnlSplit.BringToFront();
                        pnlSplit.Location = new Point(this.ClientSize.Width / 2 - pnlSplit.Size.Width / 2, (this.ClientSize.Height / 2 - pnlSplit.Size.Height / 2));
                        partPayments.Clear();
                        lblpartpayment1.Text = "Payment 1";
                        lblpartAmt2.Text = "Payment 2";
                        lblpartAmt1.Text = "0.00";
                        lblpartAmt2.Text = "0.00";
                        cash_part.Enabled = true;
                        credit_part.Enabled = true;
                        Debit_part.Enabled = true;
                        upi_part.Enabled = true;
                        netbanking_part.Enabled = true;
                        midas_part.Enabled = true;

                        lblPartNetAmt.Text = lblNetAmnt.Text;
                        lblPartBalAmt.Text = lblNetAmnt.Text;
                        break;

                        //case "Btnpartpayment":
                        //    payid = Convert.ToInt32(PaymentType.PART);
                        //    dgv_part.BorderStyle = BorderStyle.None;
                        //    dgv_part.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                        //    dgv_part.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                        //    dgv_part.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                        //    dgv_part.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                        //    dgv_part.BackgroundColor = Color.White;

                        //    dgv_part.EnableHeadersVisualStyles = false;
                        //    dgv_part.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                        //    dgv_part.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                        //    dgv_part.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                        //    UIHelper.CenterAndResize(pnl_partpayment, 40, 40);
                        //    UIHelper.RoundCorners(pnl_partpayment, 25);
                        //    UIHelper.RoundCorners(tlp_part_navbar, 25);
                        //    UIHelper.RoundCorners(tlp_main_part, 25);

                        //    dgv_part.Rows.Clear();
                        //    foreach (Control control in flppart_paymentbutton.Controls)
                        //    {
                        //        if (control is Button)
                        //        {
                        //            ((Button)control).Visible = true;
                        //            ((Button)control).Enabled = true;
                        //        }
                        //    }
                        //    btnconfirm_part.Visible = false;
                        //    break;
                        //case "btn_splitpayment":
                        //    tlp_main_split.RowStyles[4] = new RowStyle(SizeType.AutoSize);
                        //    payid = Convert.ToInt32(PaymentType.SPLIT);
                        //    pnl_split.Visible = true;
                        //    panelWidthPercentage = 50;
                        //    panelheightPercentage = 70;
                        //    panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                        //    panelHeight = (int)(this.Height * panelheightPercentage / 100);
                        //    pnl_split.Size = new Size(panelWidth, panelHeight);

                        //    pnl_split.Location = new Point(this.ClientSize.Width / 2 - pnl_split.Size.Width / 2, this.ClientSize.Height / 2 - pnl_split.Size.Height / 2);
                        //    dgv_split.Rows.Clear();
                        //    break;
                }
                pnlPaymode.Visible = false;
                if (clickedButton.Name != "Btnpartpayment" && clickedButton.Name != "btn_other" && clickedButton.Name != "btn_splitpayment" && clickedButton.Name != "btncomple")
                    paybillcal(payid);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btncash_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
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
                if (string.IsNullOrEmpty(txtRcvdAmnt.Text))
                    txtRcvdAmnt.Text = "0";
                if (Convert.ToInt32(txttotalQTY.Text) == 0)
                {
                    CMessageBox.Show("Product cart is Empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (string.IsNullOrEmpty(txtRcvdAmnt.Text))
                {
                    CMessageBox.Show("Please Enter Paid Amount Is Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRcvdAmnt.Focus();
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

        private void btn_print_bill_Click(object sender, EventArgs e)
        {
            try
            {
                BillCopy_Status = false;
                Screen screen = Screen.FromControl(this);
                int screenWidth = screen.Bounds.Width;
                int screenHeight = screen.Bounds.Height;

                UIHelper.CenterAndResize(pnlPaymode, 40, 45);
                pnl_main.Enabled = false;

                //if (combined_Status)
                //{
                //    UIHelper.CenterAndResize(pnlPaymode, 40, 45);
                //    pnl_main.Enabled = false;
                //}
                //else
                //{
                //    UIHelper.CenterAndResize(tlp_payment_option, 40, 45);
                //    UIHelper.RoundCorners(tlp_payment_option, 25);
                //    pnl_main.Enabled = false;
                //}
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_print_bill_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void btn_PrintCopy_Click(object sender, EventArgs e)
        {
            BillCopy_Status = true;
            paybillcal(0);
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

            //if (string.IsNullOrEmpty(txtRcvdAmnt.Text))
            //{
            //    CMessageBox.Show("Please Enter Paid Amount Is Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtRcvdAmnt.Focus();
            //    return false;
            //}
            //else if (tlp_seletedItem_main.Controls.Count == 0)
            //{
            //    CMessageBox.Show("Product cart is Empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            return true;
        }
        public void finaltotalqty()
        {

            try
            {
                if (gridSourcemaster.Count > 0)
                {
                    int sum = 0;
                    int n = gridSourcemaster.Count;
                    for (int i = 0; i < n; i++)
                    {
                        if (Convert.ToInt32(gridSourcemaster[i].Quantity) != null && Convert.ToInt32(gridSourcemaster[i].Quantity) > 0 && Convert.ToInt32(gridSourcemaster[i].Status) == 3)
                            sum = sum + Convert.ToInt32(gridSourcemaster[i].Quantity);
                    }
                    //finalqty = Convert.ToInt32(txttotalQTY.Text) + sum;
                    finalqty = sum;
                }
                else
                {
                    int sum = 0;
                    int n = Dgv_Selected_Item.Rows.Count;
                    for (int i = 0; i < n; i++)
                    {
                        var qtyValue = Dgv_Selected_Item.Rows[i].Cells[iqty].Value;
                        var statusValue = Dgv_Selected_Item.Rows[i].Cells[istatusno].Value;

                        if (qtyValue != null
                            && statusValue != null
                            && int.TryParse(qtyValue.ToString(), out int qty)
                            && int.TryParse(statusValue.ToString(), out int status)
                            && qty > 0
                            && status == 3)

                            sum = sum + Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty]);
                    }
                    finalqty = Convert.ToInt32(txttotalQTY.Text) + sum;
                }



            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  finaltotalqty()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        //public void FinalTax()
        //{
        //    try
        //    {

        //        if (gridSourcemaster.Count > 0 && !DiscountApplied)
        //        {
        //            TotalTax = 0; TotalIGST = 0; TotalCGST = 0; TotalSGST = 0; TotalVAT = 0; Totaldis = 0;
        //            for (int i = 0; i < gridSourcemaster.Count; i++)
        //            {
        //                if (Convert.ToInt32(gridSourcemaster[i].Status) == 3)
        //                {
        //                    TotalIGST = TotalIGST + Convert.ToDecimal(gridSourcemaster[i].IGSTAmount);
        //                    TotalCGST = TotalCGST + Convert.ToDecimal(gridSourcemaster[i].CGSTAmount);
        //                    TotalSGST = TotalSGST + Convert.ToDecimal(gridSourcemaster[i].SGSTAmount);
        //                    TotalVAT = TotalVAT + Convert.ToDecimal(gridSourcemaster[i].VATAmount);
        //                    TotalPreTaxAmt = TotalPreTaxAmt + Convert.ToDecimal(gridSourcemaster[i].PreTaxAmt);
        //                    //Totaldis = Totaldis + Convert.ToDecimal(dgv_exitsting_product.Rows[i].Cells[pDisAmnt].Value);

        //                }
        //            }
        //            TotalTax = TotalIGST + TotalVAT;
        //        }
        //        else
        //        {
        //            TotalTax = 0; TotalIGST = 0; TotalCGST = 0; TotalSGST = 0; TotalVAT = 0; Totaldis = 0; TotalPreTaxAmt = 0;
        //            for (int i = 0; i < Dgv_Selected_Item.Rows.Count; i++)
        //            {
        //                decimal igstAmt = 0;
        //                decimal vatAmt = 0;
        //                decimal preAmt = 0;

        //                decimal.TryParse(Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value), out igstAmt);
        //                decimal.TryParse(Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ivatamtno].Value), out vatAmt);
        //                decimal.TryParse(Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ipretaxamtno].Value), out preAmt);

        //                TotalIGST += igstAmt;
        //                TotalCGST += igstAmt / 2;
        //                TotalSGST += igstAmt / 2;
        //                TotalVAT += vatAmt;
        //                TotalPreTaxAmt += preAmt;
        //            }

        //            TotalTax = TotalIGST + TotalVAT;

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        clsgen.LogException("Order page  FinalTax()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
        //    }

        //}
        public void FinalTax()
        {
            try
            {
                if (gridSourcemaster.Count > 0 && !DiscountApplied)
                {
                    TotalTax = 0;
                    TotalIGST = 0;
                    TotalCGST = 0;
                    TotalSGST = 0;
                    TotalVAT = 0;
                    Totaldis = 0;
                    TotalPreTaxAmt = 0;

                    for (int i = 0; i < gridSourcemaster.Count; i++)
                    {
                        if (Convert.ToInt32(gridSourcemaster[i].Status) == 3)
                        {
                            TotalIGST += Convert.ToDecimal(gridSourcemaster[i].IGSTAmount);
                            TotalCGST += Convert.ToDecimal(gridSourcemaster[i].CGSTAmount);
                            TotalSGST += Convert.ToDecimal(gridSourcemaster[i].SGSTAmount);
                            TotalVAT += Convert.ToDecimal(gridSourcemaster[i].VATAmount);
                            TotalPreTaxAmt += Convert.ToDecimal(gridSourcemaster[i].PreTaxAmt);
                        }
                    }
                }
                else
                {
                    TotalTax = 0;
                    TotalIGST = 0;
                    TotalCGST = 0;
                    TotalSGST = 0;
                    TotalVAT = 0;
                    Totaldis = 0;
                    TotalPreTaxAmt = 0;

                    for (int i = 0; i < Dgv_Selected_Item.Rows.Count; i++)
                    {
                        decimal igstAmt = 0;
                        decimal vatAmt = 0;
                        decimal preAmt = 0;

                        decimal.TryParse(
                            Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iigstamtno].Value),
                            out igstAmt);

                        decimal.TryParse(
                            Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ivatamtno].Value),
                            out vatAmt);

                        decimal.TryParse(
                            Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ipretaxamtno].Value),
                            out preAmt);

                        TotalIGST += igstAmt;

                        TotalCGST += Math.Round(
                            igstAmt / 2m, 2, MidpointRounding.AwayFromZero);

                        TotalSGST += igstAmt - Math.Round(
                            igstAmt / 2m, 2, MidpointRounding.AwayFromZero);

                        TotalVAT += vatAmt;
                        TotalPreTaxAmt += preAmt;
                    }
                }

                // ==========================================
                // ADD PACKING TAX
                // ==========================================

                bool packingTaxIsGst;
                decimal packingTaxAmount = GetPackingTaxAmount(out packingTaxIsGst);

                if (packingTaxAmount > 0)
                {
                    if (packingTaxIsGst)
                    {
                        TotalIGST += packingTaxAmount;

                        decimal packingCGST = Math.Round(
                            packingTaxAmount / 2m,
                            2,
                            MidpointRounding.AwayFromZero);

                        decimal packingSGST = packingTaxAmount - packingCGST;

                        TotalCGST += packingCGST;
                        TotalSGST += packingSGST;
                    }
                    else
                    {
                        TotalVAT += packingTaxAmount;
                    }
                }

                // Allocate product-specific packing tax using each tblItem tax type.
                decimal productPackingGstTax = GetGridPackingGstTax();
                decimal productPackingVatTax = GetGridPackingVatTax();

                if (productPackingGstTax > 0m)
                {
                    TotalIGST += productPackingGstTax;
                    decimal packingCGST = Math.Round(
                        productPackingGstTax / 2m, 2, MidpointRounding.AwayFromZero);
                    TotalCGST += packingCGST;
                    TotalSGST += productPackingGstTax - packingCGST;
                }

                if (productPackingVatTax > 0m)
                    TotalVAT += productPackingVatTax;

                // ==========================================
                // FINAL TOTAL TAX
                // ==========================================

                TotalTax = TotalIGST + TotalVAT;
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Order page FinalTax()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order));
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
                    if (string.IsNullOrEmpty(txtadvance.Text))
                        txtadvance.Text = "0";
                    if (string.IsNullOrEmpty(txtpkchrge_amt.Text))
                        txtpkchrge_amt.Text = "0";
                    if (string.IsNullOrEmpty(txtroundoff.Text))
                        txtroundoff.Text = "0";

                    #region Direct Print Bill No kitchen send 
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
                        dmnsalemaster.Roundoff = Convert.ToDecimal(txtroundoff.Text);

                        #region Customer Registartion
                        dmnsalemaster.cusid = Convert.ToInt32(txtcusid.Text);
                        dmnsalemaster.FirstName = Convert.ToString(txtfristname.Text).Trim();
                        dmnsalemaster.SecondName = Convert.ToString(txtsecondname.Text).Trim();
                        dmnsalemaster.CompanyName = Convert.ToString(txtcompanyname.Text).Trim();
                        dmnsalemaster.GSTNo = Convert.ToString(txtgstno.Text).Trim();
                        dmnsalemaster.PhoneNumber = Convert.ToString(txtph.Text).Trim();
                        dmnsalemaster.EmailAddress = Convert.ToString(txtemailid.Text).Trim();
                        dmnsalemaster.FullAddress = Convert.ToString(txtaddress.Text).Trim();
                        if (rb_male.Checked)
                            dmnsalemaster.Gender = 1;
                        else if (rb_female.Checked)
                            dmnsalemaster.Gender = 2;
                        #endregion

                        //if (RFID_Status)
                        //{
                        //    dmnsalemaster.Name = Convert.ToString(txt_cardaccname.Text);
                        //    dmnsalemaster.ContactNo = Convert.ToString(txt_cardph.Text);
                        //    dmnsalemaster.Email = Convert.ToString(txtcardemail.Text);
                        //    dmnsalemaster.Address = Convert.ToString(txt_cardaddress.Text);
                        //    dmnsalemaster.LedgerId = Convert.ToInt32(txt_LedgerId.Text);

                        //    if (lbl_cardgender.Text == "Male")
                        //        dmnsalemaster.Gender = 1;
                        //    else if (lbl_cardgender.Text == "Female")
                        //        dmnsalemaster.Gender = 2;
                        //    else if (lbl_cardgender.Text == "Others")
                        //        dmnsalemaster.Gender = 3;
                        //}
                        //else
                        //{
                        //    dmnsalemaster.Name = Convert.ToString(txtfristname.Text) + Convert.ToString(txtsecondname.Text);
                        //    dmnsalemaster.ContactNo = Convert.ToString(txtph.Text);
                        //    dmnsalemaster.Email = Convert.ToString(txtemailid.Text);
                        //    dmnsalemaster.Address = Convert.ToString(txtaddress.Text);
                        //    if (rb_male.Checked)
                        //        dmnsalemaster.Gender = 1;
                        //    else
                        //        dmnsalemaster.Gender = 2;
                        //}

                        dmnsalemaster.NetAmount = Convert.ToDecimal(lblNetAmnt.Text);
                        dmnsalemaster.RcvAmnt = Convert.ToDecimal(txtRcvdAmnt.Text);
                        dmnsalemaster.BalAmnt = Convert.ToDecimal(lblBalcAmnt.Text);
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

                        if(DiscountApplied)
                            CalculateDiscountTaxAmt();
                        else
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
                        dmnsalemaster.IsDirectSale = true;

                        decimal actualNet = dmnsalemaster.NetAmount;
                        if (Convert.ToDecimal(txtroundoff.Text) != 0)
                        {
                            actualNet = dmnsalemaster.NetAmount - Convert.ToDecimal(txtroundoff.Text);
                        }
                        TotalPreTaxAmt = actualNet - dmnsalemaster.TotalTax;
                        dmnsalemaster.TotalPreTaxAmt = TotalPreTaxAmt;

                        if (payid == Convert.ToInt32(PaymentType.COMPLEMENTRY))
                        {
                            dmnsalemaster.ReasonId = Convert.ToInt32(cmb_com_reason.SelectedValue);

                            if (txtcom_reason.Text.Trim() == reasonPlaceholder)
                                dmnsalemaster.ReasonText = "";
                            else
                                dmnsalemaster.ReasonText = txtcom_reason.Text;
                        }

                        var InsertStatusP = bllSales.AddEditData("prdInsert_Upd_SalesMaster", dmnsalemaster);

                        #region details
                        if (InsertStatusP.SalesMasterId != 0)
                        {
                            CounterKotNo = InsertStatusP.SalesMasterId;
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

                            #region To Save partpayment Table 
                            if (payid == Convert.ToInt32(PaymentType.SPLIT))
                            {
                                foreach (var payment in partPayments)
                                {
                                    dmnsalemaster.PartPayId = payment.PartPayId;
                                    dmnsalemaster.FormType = Convert.ToInt32(TapType.FandBPayment);
                                    if (Convert.ToInt32(dmnsalemaster.PartPayId) == Convert.ToInt32(PaymentType.MidasCard))
                                    {
                                        dmnsalemaster.DebitLedgerId = DebitCustomerId;
                                        dmnsalemaster.DebitICardNo = DebitCardNumber;
                                        dmnsalemaster.CardType = CardTypeno;
                                        dmnsalemaster.FMCardType = Fmcardtypeno;
                                    }
                                    else
                                    {
                                        dmnsalemaster.DebitLedgerId = 0;
                                        dmnsalemaster.DebitICardNo = null;
                                        dmnsalemaster.CardType = 0;
                                        dmnsalemaster.FMCardType = 0;
                                    }
                                    dmnsalemaster.SalesMasterId = Convert.ToInt32(InsertStatusP.SalesMasterId);
                                    dmnsalemaster.DBname = GameDB;
                                    if (decimal.TryParse(payment.PartAmount, out decimal amt))
                                    {
                                        dmnsalemaster.PartAmount = amt.ToString("0.##");
                                    }

                                    var InsertStatus1 = bllSales.AddEditSplit("prdInsert_PartPaymentDetails", dmnsalemaster);
                                }
                            }
                            #endregion

                            #region Midascard
                            if (payid == Convert.ToInt32(PaymentType.MidasCard))
                            {
                                dmnsalemaster.DebitLedgerId = DebitCustomerId;
                                dmnsalemaster.DebitICardNo = DebitCardNumber;
                                dmnsalemaster.CardType = CardTypeno;
                                dmnsalemaster.FMCardType = Fmcardtypeno;
                                dmnsalemaster.DBname = GameDB;
                                var InsertStatus1 = bllSales.AddEditmidas("prdpayment_midascard", dmnsalemaster);
                            }
                            #endregion

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
                                    // BllSale resolves menu-product FoodPreparationType; combo components are resolved by SQL.
                                    dmnsaledetails.OrderTypeId = GetRowOrderTypeId(Dgv_Selected_Item.Rows[i], ipackingOrderTypeIdno.Index, this.StockDeductionOrderTypeId);

                                    dmnsaledetails.OrderDetailsId = "0";
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

                                    dmnsaledetails.PreTaxAmt = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ipretaxamtno].Value);
                                    SetPackingSaleDetailValues(dmnsaledetails, Dgv_Selected_Item.Rows[i]);

                                    var InsertStatus = bllSales.AddEditDataDet("prdInsert_Upd_SalesDetails", dmnsaledetails);

                                    if (InsertStatus == null)
                                    {
                                        CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        break;
                                    }
                                    else
                                        countflag = countflag + 1;

                                    int saledetailsid = InsertStatus.SalesDetailsId;


                                    var InsertS = bllSales.AddEditstock("prdInsert_Upd_Menu_Daily", dmnsaledetails);


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
                                else if (sourcepage == Convert.ToInt32(SourcepageType.Takeawaypage))
                                    ResetTakeawayViewPage();

                                ResetBillpage();
                                this.Close();
                            }
                            else
                            {
                                FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(InsertStatusP.SalesMasterId, payid, 0, Biltypeno);
                                frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                                frmSalesInvoice.ShowDialog();
                                CMessageBox.Show("Transaction Completed Sucessfully", "Sucessfull", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                                    ResetTableViewPage();
                                else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                                    ResetPickupViewPage();
                                else if (sourcepage == Convert.ToInt32(SourcepageType.Takeawaypage))
                                    ResetTakeawayViewPage();
                                ResetBillpage();

                                //ClearAll();
                                this.Close();
                            }
                            #endregion

                        }
                        #endregion

                        int tokenOrderMasterIdForPickup = 0;
                        int.TryParse(lblorderid.Text, out tokenOrderMasterIdForPickup);
                        PrintOrderNokot(Convert.ToInt32(KOTbilltype.Pickuporder), tokenOrderMasterIdForPickup);
                    }
                    #endregion

                    else
                    {
                        #region Combined  Save Salemaster And Saledetails
                        if (checkedtableNames.Count > 1)
                        {
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

                            #region Customer Registartion
                            dmnsalemaster.cusid = Convert.ToInt32(txtcusid.Text);
                            dmnsalemaster.FirstName = Convert.ToString(txtfristname.Text).Trim();
                            dmnsalemaster.SecondName = Convert.ToString(txtsecondname.Text).Trim();
                            dmnsalemaster.CompanyName = Convert.ToString(txtcompanyname.Text).Trim();
                            dmnsalemaster.GSTNo = Convert.ToString(txtgstno.Text).Trim();
                            dmnsalemaster.PhoneNumber = Convert.ToString(txtph.Text).Trim();
                            dmnsalemaster.EmailAddress = Convert.ToString(txtemailid.Text).Trim();
                            dmnsalemaster.FullAddress = Convert.ToString(txtaddress.Text).Trim();
                            if (rb_male.Checked)
                                dmnsalemaster.Gender = 1;
                            else if (rb_female.Checked)
                                dmnsalemaster.Gender = 2;
                            #endregion

                            dmnsalemaster.Roundoff = Convert.ToDecimal(txtroundoff.Text);
                            dmnsalemaster.TotQty = Convert.ToDecimal(finalqty);

                            // Combined table KOT rows do not include product packaging in their
                            // stored order-line totals. Add the calculated pickup packaging once
                            // to the invoice amount before saving the sales master.
                            decimal combinedProductPackingPrice = GetCombinedGridPackingPrice();
                            dmnsalemaster.TotalAmnt =
                                Convert.ToDecimal(txtGrandTotal.Text) + combinedProductPackingPrice;
                            dmnsalemaster.GrandTot =
                                Convert.ToDecimal(txtGrandTotal.Text) + combinedProductPackingPrice;
                            dmnsalemaster.DisPer = Convert.ToDecimal(txtdis_per.Text);
                            dmnsalemaster.DisAmnt = Convert.ToDecimal(txtdis_amt.Text);
                            dmnsalemaster.NetAmount =
                                Convert.ToDecimal(lblNetAmnt.Text) + combinedProductPackingPrice;
                            dmnsalemaster.RcvAmnt = Convert.ToDecimal(txtRcvdAmnt.Text);
                            dmnsalemaster.BalAmnt = dmnsalemaster.NetAmount - dmnsalemaster.RcvAmnt;

                            txtGrandTotal.Text = dmnsalemaster.TotalAmnt.ToString("N2");
                            lblNetAmnt.Text = dmnsalemaster.NetAmount.ToString("N2");
                            lblBalcAmnt.Text = dmnsalemaster.BalAmnt.ToString("N2");
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
                                    decimal rowIgst = Convert.ToDecimal(dgv_combined.Rows[i].Cells[cigstamt].Value);
                                    TotalIGST += rowIgst;
                                    decimal rowCgst = Math.Round(rowIgst / 2m, 2, MidpointRounding.AwayFromZero);
                                    TotalCGST += rowCgst;
                                    TotalSGST += rowIgst - rowCgst;
                                    TotalVAT += Convert.ToDecimal(dgv_combined.Rows[i].Cells[cvatamt].Value);
                                    TotalPreTaxAmt = TotalPreTaxAmt + Convert.ToDecimal(dgv_combined.Rows[i].Cells[ipretaxamtno].Value);
                                    //Totaldis = Totaldis + Convert.ToDecimal(dgv_exitsting_product.Rows[i].Cells[pDisAmnt].Value);
                                }
                            }
                            bool packingTaxIsGst;
                            decimal packingTaxAmount = GetPackingTaxAmount(out packingTaxIsGst);

                            // The manually entered packing charge uses branch settings.
                            // Product packaging uses each ingredient item's own GST/VAT setup.
                            if (packingTaxAmount > 0m)
                            {
                                if (packingTaxIsGst)
                                {
                                    TotalIGST += packingTaxAmount;
                                    decimal packingCgst = Math.Round(
                                        packingTaxAmount / 2m, 2, MidpointRounding.AwayFromZero);
                                    TotalCGST += packingCgst;
                                    TotalSGST += packingTaxAmount - packingCgst;
                                }
                                else
                                {
                                    TotalVAT += packingTaxAmount;
                                }
                            }

                            decimal combinedProductPackingGstTax;
                            decimal combinedProductPackingVatTax;
                            GetCombinedGridPackingTax(
                                out combinedProductPackingGstTax,
                                out combinedProductPackingVatTax);

                            if (combinedProductPackingGstTax > 0m)
                            {
                                TotalIGST += combinedProductPackingGstTax;
                                decimal packingCgst = Math.Round(
                                    combinedProductPackingGstTax / 2m, 2, MidpointRounding.AwayFromZero);
                                TotalCGST += packingCgst;
                                TotalSGST += combinedProductPackingGstTax - packingCgst;
                            }

                            TotalVAT += combinedProductPackingVatTax;

                            TotalTax = TotalIGST + TotalVAT;

                            //dmnsalemaster.TotalDiscount = Convert.ToDecimal(txtDiscAmnt.Text);
                            dmnsalemaster.TotalTax = TotalTax;
                            dmnsalemaster.TotalIGST = TotalIGST;
                            dmnsalemaster.TotalCGST = TotalCGST;
                            dmnsalemaster.TotalSGST = TotalSGST;
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

                            decimal actualNet = dmnsalemaster.NetAmount;
                            if (Convert.ToDecimal(txtroundoff.Text) != 0)
                            {
                                actualNet = dmnsalemaster.NetAmount - Convert.ToDecimal(txtroundoff.Text);
                            }
                            TotalPreTaxAmt = actualNet - dmnsalemaster.TotalTax;
                            dmnsalemaster.TotalPreTaxAmt = TotalPreTaxAmt;
                            if (payid == Convert.ToInt32(PaymentType.COMPLEMENTRY))
                            {
                                dmnsalemaster.ReasonId = Convert.ToInt32(cmb_com_reason.SelectedValue);

                                if (txtcom_reason.Text.Trim() == reasonPlaceholder)
                                    dmnsalemaster.ReasonText = "";
                                else
                                    dmnsalemaster.ReasonText = txtcom_reason.Text;
                            }
                            var InsertStatusP = bllSales.AddEditData("prdInsert_Upd_SalesMaster", dmnsalemaster);
                            #endregion

                            #region Sale Details
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

                                #region To Save partpayment Table 
                                if (payid == Convert.ToInt32(PaymentType.SPLIT))
                                {
                                    foreach (var payment in partPayments)
                                    {
                                        dmnsalemaster.PartPayId = payment.PartPayId;
                                        dmnsalemaster.FormType = Convert.ToInt32(TapType.FandBPayment);
                                        if (Convert.ToInt32(dmnsalemaster.PartPayId) == Convert.ToInt32(PaymentType.MidasCard))
                                        {
                                            dmnsalemaster.DebitLedgerId = DebitCustomerId;
                                            dmnsalemaster.DebitICardNo = DebitCardNumber;
                                            dmnsalemaster.CardType = CardTypeno;
                                            dmnsalemaster.FMCardType = Fmcardtypeno;
                                        }
                                        else
                                        {
                                            dmnsalemaster.DebitLedgerId = 0;
                                            dmnsalemaster.DebitICardNo = null;
                                            dmnsalemaster.CardType = 0;
                                            dmnsalemaster.FMCardType = 0;
                                        }
                                        dmnsalemaster.SalesMasterId = Convert.ToInt32(InsertStatusP.SalesMasterId);
                                        dmnsalemaster.DBname = GameDB;
                                        if (decimal.TryParse(payment.PartAmount, out decimal amt))
                                        {
                                            dmnsalemaster.PartAmount = amt.ToString("0.##");
                                        }

                                        var InsertStatus1 = bllSales.AddEditSplit("prdInsert_PartPaymentDetails", dmnsalemaster);
                                    }
                                }

                                #region Midascard
                                if (payid == Convert.ToInt32(PaymentType.MidasCard))
                                {
                                    dmnsalemaster.DebitLedgerId = DebitCustomerId;
                                    dmnsalemaster.DebitICardNo = DebitCardNumber;
                                    dmnsalemaster.CardType = CardTypeno;
                                    dmnsalemaster.FMCardType = Fmcardtypeno;
                                    dmnsalemaster.DBname = GameDB;
                                    var InsertStatus1 = bllSales.AddEditmidas("prdpayment_midascard", dmnsalemaster);
                                }
                                #endregion

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
                                            dmnsaledetails.UnitId = Convert.ToInt32(dgv_combined.Rows[i].Cells[Cunitidno].Value);
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
                                            dmnsaledetails.CGSTAmount = Math.Round(dmnsaledetails.IGSTAmount / 2m, 2, MidpointRounding.AwayFromZero);
                                            dmnsaledetails.SGSTAmount = dmnsaledetails.IGSTAmount - dmnsaledetails.CGSTAmount;
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
                                            // BllSale resolves menu-product FoodPreparationType; combo components are resolved by SQL.
                                            dmnsaledetails.OrderTypeId = Convert.ToInt32(dgv_combined.Rows[i].Cells[COrderTypeIdno].Value ?? 0);
                                            dmnsaledetails.PreTaxAmt = Convert.ToDecimal(dgv_combined.Rows[i].Cells[Cpretaxamtno].Value);
                                            dmnsaledetails.TotalPackingPrice = ToPackingDecimal(dgv_combined.Rows[i].Cells[cpackingpriceno.Index].Value);
                                            dmnsaledetails.TotalPackingTax = ToPackingDecimal(dgv_combined.Rows[i].Cells[cpackingtaxamountno.Index].Value);

                                            var InsertStatus = bllSales.AddEditDataDet("prdInsert_Upd_SalesDetails", dmnsaledetails);
                                            if (InsertStatus == null)
                                            {
                                                CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                    bool isComplimentary = payid == Convert.ToInt32(PaymentType.COMPLEMENTRY);//                         if(FrmDashboard.UserSavePrompt)
                                    if (!isComplimentary || (isComplimentary && CompBillPreview))
                                    {
                                        FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(InsertStatusP.SalesMasterId, payid, 0, Biltypeno);
                                        frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                                        frmSalesInvoice.ShowDialog();
                                    }

                                    if(FrmDashboard.UserSavePrompt)
                                        CMessageBox.Show("Transaction Completed Sucessfully", "Sucessfull", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                                        ResetTableViewPage();
                                    else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                                        ResetPickupViewPage();
                                    else if (sourcepage == Convert.ToInt32(SourcepageType.Takeawaypage))
                                        ResetTakeawayViewPage();

                                    ResetBillpage();
                                    //ClearAll();
                                    this.Close();
                                    //}
                                }

                            }
                            #endregion
                        }
                        #endregion

                        #region Pickup & normal print Bill
                        else
                        {
                            #region Pickup order -> Send order to kitchen 
                            bool reorderpickupflag = false;
                            if (Convert.ToInt32(txtsalemasterid.Text) > 0)
                                reorderpickupflag = true;
                            int GBL_pkid = 0, GBL_omid = 0;
                            bool flage = false;
                            BllOrder bll_order = new BllOrder();

                            if (KOT_saleprint_status)
                            {
                                if (MandatoryCheck_KOT())
                                {
                                    #region Order Master
                                    setKitchenperfix();

                                    DmnOrderMaster dmnordermaster = new DmnOrderMaster();
                                    DmnOrderDetails dmnorderdetail = new DmnOrderDetails();
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

                                    dmnordermaster.CustomerName = Convert.ToString(txtfristname.Text) + Convert.ToString(txtsecondname.Text);
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
                                        dmnDmnPickup.CusName = Convert.ToString(txtfristname.Text) + Convert.ToString(txtsecondname.Text);
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

                                        dmnDmnPickup.CashRev = Convert.ToDecimal(txtRcvdAmnt.Text);

                                        dmnDmnPickup.BalnceAmt = Convert.ToDecimal(lblBalcAmnt.Text);
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

                                    bool hasKitchenItem = false;

                                    for (int i = 0; i < ntemp; i++)
                                    {
                                        if (Dgv_Selected_Item.Rows[i].Cells[iqty].Value != null &&
                                            Dgv_Selected_Item.Rows[i].Cells[iqty].Value.ToString() != "")
                                        {
                                            if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iPreparationModeno].Value)
                                                == Convert.ToInt32(KOTRouting.kitchen))
                                            {
                                                hasKitchenItem = true;
                                                break;
                                            }
                                        }
                                    }

                                    if (hasKitchenItem)
                                        dmnordermaster.Status = 1;
                                    else
                                        dmnordermaster.Status = 3;


                                    dmnordermaster.Tid = tableId;

                                    if (Convert.ToInt32(txtsalemasterid.Text) > 0)
                                        dmnordermaster.NetAmount = nettotatemp;
                                    else
                                        dmnordermaster.NetAmount = Convert.ToDecimal(lblNetAmnt.Text);

                                    if (Convert.ToInt32(txtsalemasterid.Text) > 0)
                                        dmnordermaster.Kitchennumber = Convert.ToString(Refundkitchenslno);
                                    else
                                        dmnordermaster.Kitchennumber = Convert.ToString(txtkitchenslrno.Text);

                                    dmnordermaster.OrderTypeId = OrderTypeId;
                                    var InsertStatusor = bll_order.AddEditData("prdInsert_Upd_OrderMaster", dmnordermaster);
                                    #endregion

                                    #region Order Details
                                    if (InsertStatusor.OrderMasterId > 0)
                                    {
                                        GBL_omid = InsertStatusor.OrderMasterId;
                                        ResetFlag(Convert.ToInt32(txtordermasterid.Text));

                                        for (int i = 0; i < Dgv_Selected_Item.Rows.Count; i++)
                                        {
                                            #region Booking
                                            if (Booking_Status)
                                            {
                                                if (Dgv_Selected_Item.Rows[i].Cells[iitemid].Value != null && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) > 0)
                                                {
                                                    dmnorderdetail.OrderMasterId = Convert.ToInt32(InsertStatusor.OrderMasterId);
                                                    dmnorderdetail.ItemBarCode = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[ibarcode].Value);

                                                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.ComboProduct))
                                                        dmnorderdetail.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iproducttypeno].Value);
                                                    else
                                                    {
                                                        if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipavidno].Value) > 0)
                                                            dmnorderdetail.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iorgname].Value);
                                                        else
                                                            dmnorderdetail.ItemName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iname].Value);
                                                    }
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
                                                    dmnorderdetail.VariatiionName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iVariatiionNameno].Value);
                                                    dmnorderdetail.vid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ividno].Value);
                                                    dmnorderdetail.PkId = GBL_pkid;
                                                    dmnorderdetail.orderpriority = cmb_proritytype.SelectedIndex >= 0 ? cmb_proritytype.SelectedIndex + 1 : 1;
                                                    dmnorderdetail.ReprintStatus = chk_reprint_status.Checked ? 1 : 0;
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
                                            #endregion

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
                                                    dmnorderdetail.VariatiionName = Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iVariatiionNameno].Value);


                                                    if (txtentrie_descr.Text == "Description Type here .....")
                                                        dmnorderdetail.MainRemarks = "";
                                                    else
                                                        dmnorderdetail.MainRemarks = txtentrie_descr.Text;

                                                    dmnorderdetail.BranchId = FrmDashboard.BranchId;
                                                    dmnorderdetail.CreatedBy = FrmDashboard.UserId;

                                                    if (Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iPreparationModeno].Value) == Convert.ToInt32(KOTRouting.Counter))
                                                        dmnorderdetail.Status = 3;
                                                    else
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

                                                    dmnorderdetail.orderpriority = cmb_proritytype.SelectedIndex >= 0 ? cmb_proritytype.SelectedIndex + 1 : 1;
                                                    dmnorderdetail.ReprintStatus = chk_reprint_status.Checked ? 1 : 0;

                                                    dmnorderdetail.PreparationMode = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iPreparationModeno].Value);
                                                    dmnsaledetails.IsStockTracked = Convert.ToBoolean(Dgv_Selected_Item.Rows[i].Cells[istocktrackno].Value);
                                                    dmnsaledetails.PreTaxAmt = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ipretaxamtno].Value);
                                                    SetPackingSaleDetailValues(dmnsaledetails, Dgv_Selected_Item.Rows[i]);

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
                                    #endregion
                                }
                            }
                            #endregion

                            if (KOT_saleprint_status && !flage && OrderTypeId > 0 && GBL_omid > 0)
                            {
                                bll_order.RefreshOrderTokenStatus(
                                    "prdOrderToken_RefreshStatus",
                                    GBL_omid,
                                    FrmDashboard.UserId);
                            }

                            #region Sale master
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
                                //dmnsalemaster.TotalAmnt = nettotal;
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

                            dmnsalemaster.Roundoff = Convert.ToDecimal(txtroundoff.Text);

                            #region Customer Registartion
                            dmnsalemaster.cusid = Convert.ToInt32(txtcusid.Text);
                            dmnsalemaster.FirstName = Convert.ToString(txtfristname.Text).Trim();
                            dmnsalemaster.SecondName = Convert.ToString(txtsecondname.Text).Trim();
                            dmnsalemaster.CompanyName = Convert.ToString(txtcompanyname.Text).Trim();
                            dmnsalemaster.GSTNo = Convert.ToString(txtgstno.Text).Trim();
                            dmnsalemaster.PhoneNumber = Convert.ToString(txtph.Text).Trim();
                            dmnsalemaster.EmailAddress = Convert.ToString(txtemailid.Text).Trim();
                            dmnsalemaster.FullAddress = Convert.ToString(txtaddress.Text).Trim();
                            if (rb_male.Checked)
                                dmnsalemaster.Gender = 1;
                            else if (rb_female.Checked)
                                dmnsalemaster.Gender = 2;
                            #endregion

                            //if (Convert.ToInt32(txtsalemasterid.Text) > 0)
                            //{
                            //    dmnsalemaster.NetAmount = nettotal;
                            //}
                            //else
                            //{
                                dmnsalemaster.NetAmount = Convert.ToDecimal(lblNetAmnt.Text);
                            //}

                            dmnsalemaster.RcvAmnt = Convert.ToDecimal(txtRcvdAmnt.Text);
                            dmnsalemaster.BalAmnt = Convert.ToDecimal(lblBalcAmnt.Text);

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

                            if (DiscountApplied)
                                CalculateDiscountTaxAmt();
                            else
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

                            if (sourcepage == Convert.ToInt32(SourcepageType.Takeawaypage))
                                dmnsalemaster.OrderType = Convert.ToInt32(OrderType.Takeaway);
                            else if (Booking_Status || OrderRunning_Status)
                                dmnsalemaster.OrderType = Convert.ToInt32(OrderType.Booking_NormalOrder);
                            else if (KOT_saleprint_status)
                                dmnsalemaster.OrderType = Convert.ToInt32(OrderType.PickupOrder);
                            else if (combined_Pickup_Status)
                                dmnsalemaster.OrderType = Convert.ToInt32(OrderType.Combined_pick_normal_Order);

                            if (KOT_saleprint_status)
                                dmnsalemaster.PkId = GBL_pkid;
                            else
                                dmnsalemaster.PkId = gridSourcemaster[0].PkId;

                            dmnsalemaster.PkChargeAmount = Convert.ToDecimal(txtpkchrge_amt.Text);

                            decimal actualNet = dmnsalemaster.NetAmount;
                            if (Convert.ToDecimal(txtroundoff.Text) != 0)
                            {
                                actualNet = dmnsalemaster.NetAmount - Convert.ToDecimal(txtroundoff.Text);
                            }
                            TotalPreTaxAmt = actualNet - dmnsalemaster.TotalTax;
                            dmnsalemaster.TotalPreTaxAmt = TotalPreTaxAmt;

                            if (payid == Convert.ToInt32(PaymentType.COMPLEMENTRY))
                            {
                                dmnsalemaster.ReasonId = Convert.ToInt32(cmb_com_reason.SelectedValue);

                                if(txtcom_reason.Text.Trim() == reasonPlaceholder)
                                    dmnsalemaster.ReasonText = "";
                                else
                                    dmnsalemaster.ReasonText = txtcom_reason.Text;

                            }

                            var InsertStatusP = bllSales.AddEditData("prdInsert_Upd_SalesMaster", dmnsalemaster);
                            #endregion

                            if (KOT_saleprint_status)
                            {
                                #region  Sale Detail from  Datagridview 

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

                                    #region To Save partpayment Table 
                                    if (payid == Convert.ToInt32(PaymentType.SPLIT))
                                    {
                                        foreach (var payment in partPayments)
                                        {
                                            dmnsalemaster.PartPayId = payment.PartPayId;
                                            dmnsalemaster.FormType = Convert.ToInt32(TapType.FandBPayment);
                                            if (Convert.ToInt32(dmnsalemaster.PartPayId) == Convert.ToInt32(PaymentType.MidasCard))
                                            {
                                                dmnsalemaster.DebitLedgerId = DebitCustomerId;
                                                dmnsalemaster.DebitICardNo = DebitCardNumber;
                                                dmnsalemaster.CardType = CardTypeno;
                                                dmnsalemaster.FMCardType = Fmcardtypeno;
                                            }
                                            else
                                            {
                                                dmnsalemaster.DebitLedgerId = 0;
                                                dmnsalemaster.DebitICardNo = null;
                                                dmnsalemaster.CardType = 0;
                                                dmnsalemaster.FMCardType = 0;
                                            }
                                            dmnsalemaster.SalesMasterId = Convert.ToInt32(InsertStatusP.SalesMasterId);
                                            dmnsalemaster.DBname = GameDB;
                                            if (decimal.TryParse(payment.PartAmount, out decimal amt))
                                            {
                                                dmnsalemaster.PartAmount = amt.ToString("0.##");
                                            }

                                            var InsertStatus1 = bllSales.AddEditSplit("prdInsert_PartPaymentDetails", dmnsalemaster);
                                        }
                                    }
                                    #endregion

                                    #region Midascard
                                    if (payid == Convert.ToInt32(PaymentType.MidasCard))
                                    {
                                        dmnsalemaster.DebitLedgerId = DebitCustomerId;
                                        dmnsalemaster.DebitICardNo = DebitCardNumber;
                                        dmnsalemaster.CardType = CardTypeno;
                                        dmnsalemaster.FMCardType = Fmcardtypeno;
                                        dmnsalemaster.DBname = GameDB;
                                        var InsertStatus1 = bllSales.AddEditmidas("prdpayment_midascard", dmnsalemaster);
                                    }
                                    #endregion

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
                                            // BllSale resolves menu-product FoodPreparationType; combo components are resolved by SQL.
                                            dmnsaledetails.OrderTypeId = GetRowOrderTypeId(Dgv_Selected_Item.Rows[i], ipackingOrderTypeIdno.Index, this.StockDeductionOrderTypeId);
                                            dmnsaledetails.PreTaxAmt = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ipretaxamtno].Value);
                                             SetPackingSaleDetailValues(dmnsaledetails, Dgv_Selected_Item.Rows[i]);

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
                                                CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                                        bool isComplimentary = payid == Convert.ToInt32(PaymentType.COMPLEMENTRY);
                                        if (!isComplimentary || (isComplimentary && CompBillPreview))
                                        {
                                            using (FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(InsertStatusP.SalesMasterId, payid, 0, 3))
                                            {
                                                frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                                                frmSalesInvoice.ShowDialog();
                                            }
                                        }

                                        if (FrmDashboard.UserSavePrompt)
                                            CMessageBox.Show("Billing Printing Completed Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                                            ResetTableViewPage();
                                        else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                                            ResetPickupViewPage();
                                        else if (sourcepage == Convert.ToInt32(SourcepageType.Takeawaypage))
                                            ResetTakeawayViewPage();

                                        ResetBillpage();
                                        this.Close();
                                    }
                                    else
                                    {
                                        bool isComplimentary = payid == Convert.ToInt32(PaymentType.COMPLEMENTRY);  //if (FrmDashboard.UserSavePrompt)                   
                                        if (!isComplimentary || (isComplimentary && CompBillPreview))
                                        {
                                            using (FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(InsertStatusP.SalesMasterId, payid, 0, 3))
                                            {
                                                frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                                                frmSalesInvoice.ShowDialog();
                                            }
                                        }

                                        if (FrmDashboard.UserSavePrompt)
                                            CMessageBox.Show("Transaction Completed Sucessfully", "Sucessfull", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                        if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                                            ResetTableViewPage();
                                        else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                                            ResetPickupViewPage();
                                        else if (sourcepage == Convert.ToInt32(SourcepageType.Takeawaypage))
                                            ResetTakeawayViewPage();
                                        ResetBillpage();

                                        //ClearAll();
                                        this.Close();
                                    }

                                }

                                #endregion

                                CounterKotNo = dmnsalemaster.PkId;
                                PrintOrderNokot(Convert.ToInt32(KOTbilltype.Pickuporder), GBL_omid);

                            }
                            else
                            {
                                #region sale Details From Gridsource
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

                                    #region To Save partpayment Table 
                                    if (payid == Convert.ToInt32(PaymentType.SPLIT))
                                    {
                                        foreach (var payment in partPayments)
                                        {
                                            dmnsalemaster.PartPayId = payment.PartPayId;
                                            dmnsalemaster.FormType = Convert.ToInt32(TapType.FandBPayment);
                                            if (Convert.ToInt32(dmnsalemaster.PartPayId) == Convert.ToInt32(PaymentType.MidasCard))
                                            {
                                                dmnsalemaster.DebitLedgerId = DebitCustomerId;
                                                dmnsalemaster.DebitICardNo = DebitCardNumber;
                                                dmnsalemaster.CardType = CardTypeno;
                                                dmnsalemaster.FMCardType = Fmcardtypeno;
                                            }
                                            else
                                            {
                                                dmnsalemaster.DebitLedgerId = 0;
                                                dmnsalemaster.DebitICardNo = null;
                                                dmnsalemaster.CardType = 0;
                                                dmnsalemaster.FMCardType = 0;
                                            }
                                            dmnsalemaster.SalesMasterId = Convert.ToInt32(InsertStatusP.SalesMasterId);
                                            dmnsalemaster.DBname = GameDB;
                                            if (decimal.TryParse(payment.PartAmount, out decimal amt))
                                            {
                                                dmnsalemaster.PartAmount = amt.ToString("0.##");
                                            }

                                            var InsertStatus1 = bllSales.AddEditSplit("prdInsert_PartPaymentDetails", dmnsalemaster);
                                        }
                                    }
                                    #region Midascard
                                    if (payid == Convert.ToInt32(PaymentType.MidasCard))
                                    {
                                        dmnsalemaster.DebitLedgerId = DebitCustomerId;
                                        dmnsalemaster.DebitICardNo = DebitCardNumber;
                                        dmnsalemaster.CardType = CardTypeno;
                                        dmnsalemaster.FMCardType = Fmcardtypeno;
                                        dmnsalemaster.DBname = GameDB;
                                        var InsertStatus1 = bllSales.AddEditmidas("prdpayment_midascard", dmnsalemaster);
                                    }
                                    #endregion
                                    #endregion

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

                                                if (Convert.ToInt32(gridSourcemaster[i].AddonStatus) == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                                                    dmnsaledetails.NetAmount = 0;
                                                else
                                                    dmnsaledetails.NetAmount = Convert.ToDecimal(gridSourcemaster[i].NetAmount);

                                                dmnsaledetails.itemid = Convert.ToInt32(gridSourcemaster[i].ItemId);
                                                dmnsaledetails.Prid = Convert.ToInt32(gridSourcemaster[i].PrId);
                                                dmnsaledetails.Tax = Convert.ToString(gridSourcemaster[i].Tax);
                                                dmnsaledetails.TaxType = Convert.ToString(gridSourcemaster[i].TaxType);
                                                dmnsaledetails.MRP = Convert.ToDecimal(gridSourcemaster[i].MRP);
                                                dmnsaledetails.ProductType = Convert.ToString(gridSourcemaster[i].TableType);

                                                dmnsaledetails.IGSTAmount = Convert.ToDecimal(gridSourcemaster[i].IGSTAmount);
                                                dmnsaledetails.CGSTAmount = Convert.ToDecimal(gridSourcemaster[i].CGSTAmount);
                                                dmnsaledetails.SGSTAmount = Convert.ToDecimal(gridSourcemaster[i].SGSTAmount);
                                                dmnsaledetails.VatAmount = Convert.ToDecimal(gridSourcemaster[i].VATAmount);

                                                dmnsaledetails.MarketPrice = Convert.ToDecimal(gridSourcemaster[i].MarketPrice);
                                                dmnsaledetails.TableType = Convert.ToInt32(gridSourcemaster[i].TableType);
                                                dmnsaledetails.BranchId = FrmDashboard.BranchId;
                                                dmnsaledetails.CreatedBy = FrmDashboard.UserId;
                                                dmnsaledetails.Status = 1;

                                                dmnsaledetails.Pavid = Convert.ToInt32(gridSourcemaster[i].Pavid);
                                                dmnsaledetails.AddonStatus = Convert.ToInt32(gridSourcemaster[i].AddonStatus);
                                                dmnsaledetails.AV_id = Convert.ToString(gridSourcemaster[i].AV_id);
                                                dmnsaledetails.Addoncount = Convert.ToInt32(gridSourcemaster[i].Addoncount);
                                                dmnsaledetails.VariatiionName = Convert.ToString(gridSourcemaster[i].VariatiionName);

                                                dmnsaledetails.OrderDetailsId = Convert.ToString(gridSourcemaster[i].OrderDetailsId);

                                                dmnsaledetails.SalesDetailsId = Convert.ToInt32(gridSourcemaster[i].SalesDetailsId);

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

                                                // BllSale resolves menu-product FoodPreparationType; combo components are resolved by SQL.
                                                dmnsaledetails.OrderTypeId = GetRowOrderTypeId(Dgv_Selected_Item.Rows[i + 1], ipackingOrderTypeIdno.Index, this.StockDeductionOrderTypeId);
                                                dmnsaledetails.PreTaxAmt = Convert.ToDecimal(Dgv_Selected_Item.Rows[i + 1].Cells[ipretaxamtno].Value);
                                                SetPackingSaleDetailValues(dmnsaledetails, Dgv_Selected_Item.Rows[i + 1]);

                                                var InsertStatus = bllSales.AddEditDataDet("prdInsert_Upd_SalesDetails", dmnsaledetails);
                                                if (InsertStatus == null)
                                                {
                                                    CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                                    break;
                                                }
                                                else
                                                    countflag = countflag + 1;

                                                int saledetailsid = InsertStatus.SalesDetailsId;
                                                int odid = Convert.ToInt32(gridSourcemaster[i].OrderDetailsId);
                                                var existingOrder = orderDetailsInfoList.FirstOrDefault(row => row.OrderDetailsId == odid);
                                                if (existingOrder != null)
                                                {
                                                    int index = orderDetailsInfoList.IndexOf(existingOrder);
                                                    orderDetailsInfoList[index].saleDetailsId = saledetailsid;
                                                }
                                            }
                                        }

                                    }
                                    if (gridSourcemaster.Count == countflag)
                                    {
                                        if (payid == 0)
                                        {
                                            bool isComplimentary = payid == Convert.ToInt32(PaymentType.COMPLEMENTRY);  //if (FrmDashboard.UserSavePrompt)                   
                                            if (!isComplimentary || (isComplimentary && CompBillPreview))
                                            {
                                                using (FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(InsertStatusP.SalesMasterId, payid, 0, Biltypeno))
                                                {
                                                    frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                                                    frmSalesInvoice.ShowDialog();
                                                }
                                            }

                                            if (FrmDashboard.UserSavePrompt)
                                                CMessageBox.Show("Billing Printing Completed Successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                                                ResetTableViewPage();
                                            else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                                                ResetPickupViewPage();
                                            else if (sourcepage == Convert.ToInt32(SourcepageType.Takeawaypage))
                                                ResetTakeawayViewPage();

                                            ResetBillpage();
                                            this.Close();
                                        }
                                        else
                                        {
                                            bool isComplimentary = payid == Convert.ToInt32(PaymentType.COMPLEMENTRY);  //if (FrmDashboard.UserSavePrompt)                   
                                            if (!isComplimentary || (isComplimentary && CompBillPreview))
                                            {
                                                using (FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(InsertStatusP.SalesMasterId, payid, 0, Biltypeno))
                                                {
                                                    frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                                                    frmSalesInvoice.ShowDialog();
                                                }
                                            }

                                            if (FrmDashboard.UserSavePrompt)
                                                CMessageBox.Show("Transaction Completed Sucessfully", "Sucessfull", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                            if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                                                ResetTableViewPage();
                                            else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                                                ResetPickupViewPage();
                                            else if (sourcepage == Convert.ToInt32(SourcepageType.Takeawaypage))
                                                ResetTakeawayViewPage();

                                            ResetBillpage();

                                            //ClearAll();
                                            this.Close();

                                        }
                                    }

                                }
                                #endregion
                            }
                        }
                        #endregion                    
                    }

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  paybillcal()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void btn_savekot_print_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck_KOT())
                {
                    BillCopy_Status = false;
                    KOT_saleprint_status = true;
                    UIHelper.CenterAndResize(pnlPaymode, 40, 45);
                    pnl_main.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  btn_savekot_print_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void btncancelrev_Click(object sender, EventArgs e)
        {

        }

        private void btn_cancle_pickup_Click(object sender, EventArgs e)
        {
            try
            {

                if (OrderCancelStatus == 1)
                {
                    pnl_cancelledlist.Visible = false;
                    pnl_Refund_paymode.Visible = true;
                ShowBlurPopup(pnl_Refund_paymode);
                    pnl_Refund_paymode.Location = new Point(this.ClientSize.Width / 2 - pnl_Refund_paymode.Size.Width / 2, (this.ClientSize.Height / 2 - pnl_Refund_paymode.Size.Height / 2));
                }
                else
                {
                    DialogResult dr = CMessageBox.Show(
                    "Are you sure you want to cancel this order?",
                    "Confirm Cancel",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                    if (dr != DialogResult.Yes)
                    {
                        return;
                    }


                    //ttxdesc_cancelorder.Text = "Reason Type Here ...";
                    //ttxdesc_cancelorder.ForeColor = Color.Gray;
                    //BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    //DmnSearch dmnSearch = new DmnSearch();
                    //dmnSearch.Select = "SalesMasterId Field1,NetAmount Field2";
                    //dmnSearch.Table = "tblSalesMaster";
                    //dmnSearch.WhereNdGroupBy = "where  OrderMasterId = " + tableId;
                    //dmnSearch.OrderBy = "";
                    //var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    //if (gridSource.Count > 0)
                    //{
                    //    refundsmid = Convert.ToInt32(gridSource[0].Field1);
                    //    refundtotal = Convert.ToDecimal(gridSource[0].Field2);
                    //}
                    //lblcanceltextorder.Text = $"Total Cash refund amount is {refundtotal}";
                    //UIHelper.CenterAndResize(pnl_cancelorder, 25, 35);
                    //UIHelper.RoundCorners(tlp_cancelorder, 25);
                    //UIHelper.RoundCorners(pnl_cancelorder, 25);
                    //UIHelper.RoundCorners(tlpnav_cancelorder, 25);
                    //ApplyRoundedTextBox(ttxdesc_cancelorder, 10);
                    #region Load Cancel Order Items

                    UIHelper.CenterAndResize(pnl_cancelledlist, 50, 40);
                    UIHelper.RoundCorners(pnl_cancelledlist, 25);
                    UIHelper.RoundCorners(tlp_cancelled_list_nav, 25);
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "itemName Field1,Quantity Field2,RemoveNotes Field3,OrderMasterId Field4,OrderDetailsId Field5,NetAmount Field6,CASE WHEN EXISTS(SELECT 1 FROM tblOrderDetails AS x WHERE x.OrderMasterId = tblOrderDetails.OrderMasterId AND x.KitchenCancelStatus = 1 ) THEN 1 ELSE 0 END AS Field7";
                    dmnSearch.Table = "tblOrderDetails";
                    dmnSearch.WhereNdGroupBy =
                        "WHERE AddonStatus IN (1,0) AND RefundStatus !=1" +
                        "AND OrderMasterId = " + Convert.ToInt32(lblorderid.Text);//KitchenCancelStatus = 1
                    var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    dgv_ordercancel.Rows.Clear();
                    if (gridSource.Count > 0)
                    {
                        ordercancelbystaff = true;

                        if (Convert.ToInt32(gridSource[0].Field7) == 1)
                        {
                            //btnordercallist.Visible = true;
                            Refundproductflag = true;
                        }
                        else
                        {
                            btnordercallist.Visible = false;
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
                    {
                        btnordercallist.Visible = false;
                        lblpickupcnlamt.Text = "0";
                    }
                    #endregion

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

        private void btnreconfirmorder_Click(object sender, EventArgs e)
        {
            ReconfirmOrder();
        }
        public void ReconfirmOrder()
        {
            try
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
                else if (sourcepage == Convert.ToInt32(SourcepageType.Takeawaypage))
                    ResetTakeawayViewPage();
                this.Close();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  ReconfirmOrder()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        #endregion

        #region Clear ALl

        private void btn_frm_close_Click(object sender, EventArgs e)
        {

            //ReserveQtyRouter(Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value), Convert.ToInt32(ReserveQtyProducttype.Menu), Convert.ToInt32(ReserveQtyActiontype.Update), Convert.ToInt32(ReserveQtyTabletype.Menu));

            if (sourcepage == Convert.ToInt32(SourcepageType.Tableviewpage))
                ResetTableViewPage();
            else if (sourcepage == Convert.ToInt32(SourcepageType.Pickuppage))
                ResetPickupViewPage();
            else if (sourcepage == Convert.ToInt32(SourcepageType.Billingpage))
                ResetBillpage();
            else if (sourcepage == Convert.ToInt32(SourcepageType.Takeawaypage))
                ResetTakeawayViewPage();
            this.Close();
        }

        public void ClearAll()
        {
            try
            {
                foreach (TextBox textBox in new TextBox[] { txtitemcode_serach, txtname_search, txtGrandTotal, txtRcvdAmnt, txtdis_amt, txtdis_per, txtentrie_descr })//txt_product_des
                {
                    textBox.Text = "";
                }


                //btn_split_number.Visible = true;
                txtname_search.Text = "Food Name .....";
                txtitemcode_serach.Text = "Item Code .....";

                Dgv_Selected_Item.Rows.Clear();
                Dgv_Selected_Item.Rows.Add();
                ResetFlagvalue = 0;
                Promodetailsid = 0;
                Promomasterid = 0;
                //Splitypeno = 0;
                //Existingsalemasterid = 0;
                //ReservationTotal = 0;

                //btn_itemwise.Enabled = true;
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
                txtfristname.Text = "";
                txtsecondname.Text = "";
                txtcompanyname.Text = "";
                txtgstno.Text = "";

                rb_female.Checked = false;
                rb_male.Checked = false;

                //Btnpartpayment.Visible = true;
                //btn_splitpayment.Visible = true;
                combined_Status = false;
                txtadults.Text = Convert.ToString(1);
                txtchild.Text = Convert.ToString(0);
                txtother.Text = Convert.ToString(0);
                totguests.Text = Convert.ToString(1);

            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  ClearAll()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pnl_wifiPopup.Visible = false;
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

        #region Card Account
        private void btn_cardacc_close_Click(object sender, EventArgs e)
        {
            pnl_cardAcoount.Visible = false;
        }
        private void btn_close_cardaccount_Click(object sender, EventArgs e)
        {
            pnl_cardAcoount.Visible = false;
        }
        #endregion

        #region Midas Card Payment
        private void ToggleWifiPopup(int inp)
        {
            if (inp == 1)
            {
                Pnl_wifiPopup.Left = (this.ClientSize.Width - Pnl_wifiPopup.Width) / 2;
                Pnl_wifiPopup.Top = (this.ClientSize.Height - Pnl_wifiPopup.Height) / 2;

                Pnl_wifiPopup.Visible = true;
                ShowBlurPopup(Pnl_wifiPopup);
            }
            else
            {
                Pnl_wifiPopup.Visible = false;
            }
        }
        public void MidascardCheck(string cardno)
        {
            try
            {
                if (CheckEmployeeCard(cardno))
                {
                    CMessageBox.Show("Employee ID cards cannot be used for purchases or recharges.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    CTyp = CheckCardType(cardno);
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                    switch ((CardType)CTyp)
                    {
                        case CardType.FamilyCard:
                            if (!FrmDashboard.EnableFamilyCardInPayment)
                            {
                                CMessageBox.Show("Family card payment option is disabled.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                tm_midascard.Start();
                                return;
                            }
                            CardTypeno = Convert.ToInt32(CardType.FamilyCard);
                            if (Fmcardtypeno == 1)
                            {
                                objSearch.Select = "FcardDetId Field1, CashBalance Field2";
                                objSearch.Table = GameDB + ".dbo.tblFamilyCardDetails";
                                objSearch.WhereNdGroupBy = "WHERE Status = 1 AND CardNo = '" + cardno + "'";
                            }
                            else
                            {
                                objSearch.Select = "FCardId Field1, CashBalance Field2";
                                objSearch.Table = GameDB + ".dbo.tblFamilyCardsMaster";
                                objSearch.WhereNdGroupBy = "WHERE Status = 1 AND FCardId = " + fmasterid;
                            }
                            break;

                        case CardType.FamilyCardMaster:
                            if (!FrmDashboard.EnableFamilyCardInPayment)
                            {
                                CMessageBox.Show("Family card payment option is disabled.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                tm_midascard.Start();
                                return;
                            }
                            CardTypeno = Convert.ToInt32(CardType.FamilyCardMaster);
                            if (Fmcardtypeno == 1)
                            {
                                objSearch.Select = "FcardDetId Field1, CashBalance Field2";
                                objSearch.Table = GameDB + ".dbo.tblFamilyCardDetails";
                                objSearch.WhereNdGroupBy = "WHERE Status = 1 AND CardNo = '" + cardno + "'";
                            }
                            else
                            {
                                objSearch.Select = "FCardId Field1, CashBalance Field2";
                                objSearch.Table = GameDB + ".dbo.tblFamilyCardsMaster";
                                objSearch.WhereNdGroupBy = "WHERE Status = 1 AND MasterCardNo = '" + cardno + "'";
                            }
                            break;

                        case CardType.NormalCard:
                            CardTypeno = Convert.ToInt32(CardType.NormalCard);
                            objSearch.Select = "LedgerId Field1, CardBalance Field2";
                            objSearch.Table = GameDB + ".dbo.tblAccountLedger";
                            objSearch.WhereNdGroupBy = "WHERE Status = 1 AND ICardNo = '" + cardno + "'";
                            break;

                        case CardType.GroupCard:
                            if (!FrmDashboard.EnableGroupCardInPayment)
                            {
                                CMessageBox.Show("Group Sale card payment option is disabled.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                tm_midascard.Start();
                                return;
                            }
                            if (CheckGroupsaleExpire(cardno))
                            {
                                ToggleWifiPopup(0);
                                CMessageBox.Show("This Group Sale Card has expired or has no active member.\nPayment cannot be processed for expired cards.", "Group Card Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            CardTypeno = Convert.ToInt32(CardType.GroupCard);
                            objSearch.Select = "GLedgerId Field1,CardBalance Field2";
                            objSearch.Table = GameDB + ".dbo.tblGroup_AccountLedger";
                            objSearch.WhereNdGroupBy = "WHERE Status = 1 AND ICardNo = '" + cardno + "'";
                            break;

                        default:
                            CMessageBox.Show("Invalid or unknown card type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                    }
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count > 0)
                    {
                        if (pnlSplit.Visible)
                        {
                            if (decimal.TryParse(lblPartBalAmt.Text, out decimal netAmount))
                            {
                                midas_balance = Convert.ToDecimal(itemSource[0].Field2);
                                if (midas_balance <= 0)
                                {
                                    CMessageBox.Show("Insufficient balance for purchase.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    tm_midascard.Start();
                                }
                                else
                                {
                                    tm_midascard.Stop();
                                    Pnl_wifiPopup.Hide();
                                    DebitCustomerId = Convert.ToInt32(itemSource[0].Field1);
                                    DebitCardNumber = cardno;
                                    #region Fetch midascard at payment Type table 
                                    Fetch_midas();
                                    #endregion
                                }
                            }
                        }
                        else if (pnl_Refund_paymode.Visible)
                        {
                            if (decimal.TryParse(lblpickupcnlamt.Text, out decimal netAmount))
                            {
                                midas_balance = Convert.ToDecimal(itemSource[0].Field2);
                                if (midas_balance <= 0)
                                {
                                    CMessageBox.Show("Insufficient balance for purchase.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    tm_midascard.Start();
                                }
                                else
                                {
                                    tm_midascard.Stop();
                                    Pnl_wifiPopup.Hide();
                                    DebitCustomerId = Convert.ToInt32(itemSource[0].Field1);
                                    DebitCardNumber = cardno;

                                    #region To save Refund Amt throgh Midas card 
                                    Refund(2);
                                    CheckReconfirmPickup();
                                    #endregion
                                }
                            }
                        }
                        else
                        {
                            if (decimal.TryParse(lblNetAmnt.Text, out decimal netAmount))
                            {
                                decimal balance = Convert.ToDecimal(itemSource[0].Field2);
                                if (netAmount > balance)
                                {
                                    CMessageBox.Show("Insufficient balance for purchase.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    tm_midascard.Start();
                                }
                                else
                                {
                                    tm_midascard.Stop();
                                    Pnl_wifiPopup.Hide();
                                    payid = Convert.ToInt32(PaymentType.MidasCard);
                                    DebitCustomerId = Convert.ToInt32(itemSource[0].Field1);
                                    DebitCardNumber = cardno;
                                    pnlPaymode.Visible = false;
                                    paybillcal(payid);

                                }
                            }
                        }
                    }
                    else
                    {
                        CMessageBox.Show("Invalid card. No active member found.", "Card Member Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tm_midascard.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page MidascardCheckBalance()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        private void tm_midascard_Tick(object sender, EventArgs e)
        {
            try
            {
                if (isReadingCard) return;
                isReadingCard = true;
                string str1 = AYJ_CardLib_Ul.AYJ_ReadCardLib_Ul.ReadCard_Ul(FrmLogin.CorpID);
                if (str1 != "Card not in contact")
                {
                    tm_midascard.Stop();
                    if (str1 == "" || str1 == "empty" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Invalid Card")
                        str1 = AYJ_CardLib_Mi.AYJ_ReadCardLib_Mi.ReadCard_Mi(FrmLogin.CorpID);
                    str1 = RemoveSpecialCharacters(str1.Trim());
                    if (str1 == "" || string.IsNullOrWhiteSpace(str1) || str1 == "FailAuthenticationFailAuthenticationFailAuthentication" || str1 == "Cardnotincontact" || str1 == "InvalidCard")
                    {
                        CMessageBox.Show("Card not in contact", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Pnl_wifiPopup.Visible = false;
                    }
                    else
                    {
                        tm_midascard.Stop();
                        MidascardCheck(str1);
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page tm_midascard_Tick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));

            }
            finally
            {
                isReadingCard = false;
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

        private void btncancelnotify_Click(object sender, EventArgs e)
        {
            try
            {
                DmnOrderMaster dmnRemove_order = new DmnOrderMaster();
                BllOrder bll_order = new BllOrder();
                dmnRemove_order.OrderMasterId = Convert.ToInt32(txtordermasterid.Text);
                dmnRemove_order.SalesMasterId = Convert.ToInt32(txtsalemasterid.Text);
                dmnRemove_order.OrderDetailsId = 0;
                dmnRemove_order.Tid = tableId;
                dmnRemove_order.PkId = Pickup_ID;
                dmnRemove_order.CreatedBy = FrmDashboard.UserId;
                dmnRemove_order.BranchId = FrmDashboard.BranchId;
                var InsertStatusP = bll_order.OrderCancelNotify_update("prd_CompleteCancelNotifyStatus_update", dmnRemove_order);
                if (InsertStatusP != null)
                {
                    CMessageBox.Show("Customer Notified", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (tableId > 0)
                        OrderCancelStatus = 0;
                }
                this.Close();
                ResetTableViewPage();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btncancelnotify_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order)); // Replace 'YourFormType' with the appropriate form type
            }
        }


        private void btn_midascard_Click(object sender, EventArgs e)
        {
            //UIHelper.CenterAndResize(pnl_cardAcoount, 30, 25);
            //UIHelper.RoundCorners(pnl_cardAcoount, 25);
            //UIHelper.RoundCorners(tlp_card_header, 25);
            //UIHelper.RoundCorners(tlp_cardAcoount, 25);
            ////ApplyRoundedTextBox(txtentrie_descr, 10);
            ///
            Pnl_wifiPopup.Show();
            Pnl_wifiPopup.BringToFront();
            Pnl_wifiPopup.Location = new Point(this.ClientSize.Width / 2 - Pnl_wifiPopup.Size.Width / 2, (this.ClientSize.Height / 2 - Pnl_wifiPopup.Size.Height / 2));
            tm_midascard.Start();
        }
        public bool CheckGroupsaleExpire(string cardno)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch1 = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "count(*) Field1";
                dmnSearch.Table = GameDB + ".dbo.tblGroup_AccountLedger";
                dmnSearch.WhereNdGroupBy = "where Status=1 and ICardNo='" + cardno + "' and ExpireDate>SYSDATETIME();";
                var itemsource = bllSearch1.GetSearchData("prdsearch", dmnSearch);
                if (itemsource.Count > 0)
                {
                    int activeCount = Convert.ToInt32(itemsource[0].Field1);
                    if (activeCount == 0)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                clsgen.LogException("Order page  CheckGroupsaleExpire()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }
        public bool CheckEmployeeCard(string cardno)
        {
            try
            {
                string mastercardfullno = cardno;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "SUM(cnt) AS Field1";
                objSearch.Table = " ( SELECT COUNT(*) AS cnt FROM " + GameDB + ".dbo.tblEmployee WHERE Status=1 and ICardNo = '" + cardno + "' ) AS counts";
                objSearch.WhereNdGroupBy = "";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    return Convert.ToInt32(itemSource[0].Field1) != 0;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page  CheckEmployeeCard()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
                return false;
            }
        }
        public int CheckCardType(string cardno)
        {
            try
            {
                string mastercardfullno = cardno;
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = " " + Convert.ToInt32(CardType.FamilyCard) + " as Field1,FCardId Field2,CardAcessType Field3 from " + GameDB + ".dbo.tblFamilyCardDetails where CardNo='" + cardno + "' and Status=1 " +
                    " Union All Select " + Convert.ToInt32(CardType.FamilyCardMaster) + " as Field1,FCardId Field2,CardAcessType Field3 from " + GameDB + ".dbo.tblFamilyCardsMaster where MasterCardNo='" + cardno + "' and Status=1" +
                    " Union All Select " + Convert.ToInt32(CardType.NormalCard) + " as Field1,0 Field2,0 Field3 from " + GameDB + ".dbo.tblAccountLedger where ICardNo='" + cardno + "' and Status=1" +
                    " Union All Select " + Convert.ToInt32(CardType.GroupCard) + " as Field1,0 Field2,0 Field3";
                objSearch.Table = GameDB + ".dbo.tblGroup_AccountLedger";
                objSearch.WhereNdGroupBy = " where ICardNo='" + cardno + "' and Status=1";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    Fmcardtypeno = Convert.ToInt32(itemSource[0].Field3);
                    fmasterid = Convert.ToInt32(itemSource[0].Field2);
                    return Convert.ToInt32(itemSource[0].Field1);
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
                clsgen.LogException("Order page  CheckCardType()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));


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

                    ResetNetAmount();
                    int prid = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[ipridno].Value);
                    int pid = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[iitemid].Value);
                    string Av_ID = Convert.ToString(Dgv_Selected_Item.Rows[e.RowIndex].Cells[iavid].Value);
                    int variationId = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[ividno].Value);

                    if (saletype == Convert.ToInt32(Tabletype.DirectSale))
                    {
                        #region Existing Product
                        int rowno = 0;
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (prid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.DirectSale) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                            {
                                decimal cqoh = kdata;
                                if (cqoh > Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value))
                                {
                                    CMessageBox.Show("Aviablle Quantity is : " + Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ioriginalqty].Value), "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }

                                int beforeQty = 0;
                                object oldVal = Dgv_Selected_Item.Rows[i].Cells[iqty].Value;
                                if (oldVal != null && oldVal != DBNull.Value)
                                {
                                    beforeQty = Convert.ToInt32(Convert.ToDecimal(oldVal));
                                }
                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = kdata;
                                ReserveQtyRouter(Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ipridno].Value), Convert.ToInt32(ReserveQtyProducttype.Menu), Convert.ToInt32(ReserveQtyActiontype.Update), Convert.ToInt32(ReserveQtyTabletype.Direct), Convert.ToInt32(kdata), Convert.ToInt32(CustomQty.Enabled), beforeQty);

                                decimal oldTotal = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value); // OLD row total
                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = kdata;
                                decimal newTotal = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);
                                totalcellamount(i);
                                newTotal = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);
                                decimal grand = Convert.ToDecimal(txtGrandTotal.Text);
                                grand = grand - oldTotal + newTotal;
                                RecalculateGridTotal();
                                netAmount();
                                break;
                            }
                        }

                        #endregion
                    }

                    else if (saletype == Convert.ToInt32(Tabletype.ComboProduct))
                    {
                        #region Existing combo product
                        for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                        {
                            if (pid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.ComboProduct) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                            {
                                //int prep = 0;
                                //var dataresult = ProductQtyCheckMenuQty(Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value), saletype);
                                //int availableQty = dataresult.Quantity;
                                //prep = dataresult.PreparationMode;
                                //if (availableQty <= 0 )
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

                                decimal oldTotal = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value); // OLD row total
                                int beforeQty = 0;
                                object oldVal = Dgv_Selected_Item.Rows[i].Cells[iqty].Value;

                                if (oldVal != null && oldVal != DBNull.Value)
                                {
                                    beforeQty = Convert.ToInt32(Convert.ToDecimal(oldVal));
                                }//, Convert.ToInt32(CustomQty.Enabled), beforeQty
                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = kdata;

                                ReserveQtyRouter(Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value), Convert.ToInt32(ReserveQtyProducttype.Combo), Convert.ToInt32(ReserveQtyActiontype.Update), Convert.ToInt32(ReserveQtyTabletype.Menu), Convert.ToInt32(kdata), Convert.ToInt32(CustomQty.Enabled), beforeQty);
                                totalcellamount(i);
                                decimal newTotal = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);
                                decimal grand = Convert.ToDecimal(txtGrandTotal.Text);
                                grand = grand - oldTotal + newTotal;
                                txtGrandTotal.Text = grand.ToString();
                                lblNetAmnt.Text = grand.ToString();

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
                            int gridPid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value);
                            int tableType = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value);
                            int status = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value);

                            int variationStatus = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ivaraiationstatusno].Value);
                            int gridVarId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ividno].Value);

                            bool isMatch = false;

                            if (variationStatus == 0)
                            {
                                if (pid == gridPid)
                                    isMatch = true;
                            }
                            else if (variationStatus == 1)
                            {
                                if (pid == gridPid && variationId == gridVarId)
                                    isMatch = true;
                            }

                            if (isMatch &&
                                tableType == Convert.ToInt32(Tabletype.Product) &&
                                status != 0)
                            {
                                bool isStockTracked = Product_isstocktrack(pid, 1);

                                if (isStockTracked)
                                {
                                    int prep = 0;


                                    var dataresult = ProductQtyCheckMenuQty(pid, 0);

                                    int availableQty = dataresult.Quantity;
                                    prep = dataresult.PreparationMode;

                                    if (availableQty <= 0)
                                    {
                                        CMessageBox.Show("Stock Out! This item is not available.",
                                                        "Out of Stock",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Warning);
                                        return;
                                    }

                                    if (kdata > availableQty)
                                    {
                                        CMessageBox.Show($"Only {availableQty} Qty Available.",
                                                        "Stock Warning",
                                                        MessageBoxButtons.OK,
                                                        MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                                decimal oldTotal = 0;
                                if (Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itax].Value) == "Exclusive")
                                {
                                    oldTotal = (Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iqty].Value ?? 0) * Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[israte].Value ?? 0)) - (Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[idisamt].Value)) + (Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itaxamt].Value));
                                }
                                else if (Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[itax].Value) == "Inclusive")
                                {
                                    oldTotal = (Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[iqty].Value ?? 0) * Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[israte].Value ?? 0)) - (Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[idisamt].Value));

                                }


                                int beforeQty = 0;
                                object oldVal = Dgv_Selected_Item.Rows[i].Cells[iqty].Value;

                                if (oldVal != null && oldVal != DBNull.Value)
                                {
                                    beforeQty = Convert.ToInt32(Convert.ToDecimal(oldVal));
                                }

                                Dgv_Selected_Item.Rows[i].Cells[iqty].Value = kdata;

                                if (isStockTracked)
                                {
                                    ReserveQtyRouter(
                                    pid,
                                    Convert.ToInt32(ReserveQtyProducttype.Menu),
                                    Convert.ToInt32(ReserveQtyActiontype.Update),
                                    Convert.ToInt32(ReserveQtyTabletype.Menu),
                                    Convert.ToInt32(kdata),
                                    Convert.ToInt32(CustomQty.Enabled),
                                    beforeQty
                                );
                                }

                                totalcellamount(i);
                                netAmount();
                                decimal newTotal = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[itotal].Value);
                                RecalculateGridTotal();
                                decimal grand = Convert.ToDecimal(txtGrandTotal.Text);
                                lblNetAmnt.Text = Convert.ToString(totalamt);

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
                                //if (availableQty <= 0 && prep == Convert.ToInt32(PreparationType.ReadyFood))
                                if (availableQty <= 0)
                                {
                                    CMessageBox.Show("Stock Out! This item is not available.",
                                                    "Out of Stock",
                                                    MessageBoxButtons.OK,
                                                    MessageBoxIcon.Warning);
                                    return;
                                }

                                // 2. Entered qty exceeds available stock
                                //if (kdata > availableQty && prep == Convert.ToInt32(PreparationType.ReadyFood))
                                if (kdata > availableQty)
                                {
                                    CMessageBox.Show($"Only {availableQty} Qty Available.",
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
                                lblNetAmnt.Text = grand.ToString();

                                netAmount();
                                break;
                            }
                        }
                        #endregion
                    }
                }
                else if (e.ColumnIndex == iaction)
                {
                    ResetNetAmount();

                    int rowType = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[itabletype].Value);
                    int pid = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[iitemid].Value);
                    decimal qty = Convert.ToDecimal(Dgv_Selected_Item.Rows[e.RowIndex].Cells[iqty].Value);
                    int vid = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[ividno].Value);
                    string avid = Convert.ToString(Dgv_Selected_Item.Rows[e.RowIndex].Cells[iavid].Value);
                    int prid = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[ipridno].Value);
                    int addonitem = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[ivaraiationstatusno].Value);
                    int variationId = Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[ividno].Value);

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
                        ReserveQtyRouter(Convert.ToInt32(Dgv_Selected_Item.Rows[rowno].Cells[ipridno].Value), Convert.ToInt32(ReserveQtyProducttype.Menu), Convert.ToInt32(ReserveQtyActiontype.Reset), Convert.ToInt32(ReserveQtyTabletype.Direct), Convert.ToInt32(Dgv_Selected_Item.Rows[rowno].Cells[iqty].Value));

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
                                    //ReserveQtyRouter(Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value), Convert.ToInt32(ReserveQtyProducttype.Combo), Convert.ToInt32(ReserveQtyActiontype.Reset), Convert.ToInt32(ReserveQtyTabletype.Menu), Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value));
                                    decimal TotalPrice = Convert.ToDecimal(Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value)) * Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value);
                                    decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) - TotalPrice;
                                    txtGrandTotal.Text = Convert.ToString(totalamt);
                                    lblNetAmnt.Text = Convert.ToString(totalamt);
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
                        if (addonitem == Convert.ToInt32(Product_Addon_Variation_Type.Variation))
                        {
                            #region Variation Delete (with group)

                            if (Convert.ToInt32(Dgv_Selected_Item.Rows[e.RowIndex].Cells[iMainProductStatusno].Value)
                                == Convert.ToInt32(MainProductStatus.Primary))
                            {
                                int dgvcount = Dgv_Selected_Item.RowCount - 1;

                                for (int i = dgvcount; i >= 0; i--)
                                {
                                    if (avid == Convert.ToString(Dgv_Selected_Item.Rows[i].Cells[iavid].Value))
                                    {
                                        int gridPid = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value);
                                        int gridVarId = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[ividno].Value);
                                        int status = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value);

                                        if (status == 0) continue;

                                        // 🔥 STOCK RESET
                                        bool isStockTracked = Product_isstocktrack(gridPid, 1);
                                        if (isStockTracked)
                                        {
                                            int qtytrack = Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value);

                                            ReserveQtyRouter(
                                                gridPid,
                                                Convert.ToInt32(ReserveQtyProducttype.Menu),
                                                Convert.ToInt32(ReserveQtyActiontype.Reset),
                                                Convert.ToInt32(ReserveQtyTabletype.Menu),
                                                qtytrack,
                                                0,
                                                0
                                            );
                                        }

                                        // 🔥 TOTAL REMOVE (SAFE)
                                        decimal rowTotal = 0;
                                        object totalVal = Dgv_Selected_Item.Rows[i].Cells[itotal].Value;

                                        if (totalVal != null && totalVal != DBNull.Value)
                                            rowTotal = Convert.ToDecimal(totalVal);

                                        decimal grand = Convert.ToDecimal(txtGrandTotal.Text);
                                        grand -= rowTotal;

                                        txtGrandTotal.Text = grand.ToString();
                                        lblNetAmnt.Text = grand.ToString();
                                        Dgv_Selected_Item.Rows.RemoveAt(i);
                                        break;
                                    }
                                }
                            }
                            checksave_refresh();
                            #endregion
                        }
                        else
                        {
                            #region Normal Product
                            for (int i = 0; i <= Dgv_Selected_Item.RowCount - 1; i++)
                            {
                                if (pid == Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) == 1 && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[itabletype].Value) == Convert.ToInt32(Tabletype.Product) && Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[istatusno].Value) != 0)
                                {
                                    //        public void ReserveQtyRouter(int ProductId, int ProductType, int Actiontype, int TableType, int OrdQty, int CQtyFlag=0,int porderqty=0)
                                    bool isStockTracked = Product_isstocktrack(pid, 1);
                                    if (isStockTracked)
                                    {
                                        ReserveQtyRouter(Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iitemid].Value), Convert.ToInt32(ReserveQtyProducttype.Menu), Convert.ToInt32(ReserveQtyActiontype.Reset), Convert.ToInt32(ReserveQtyTabletype.Menu), Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value));

                                    }
                                    decimal TotalPrice = Convert.ToDecimal(Dgv_Selected_Item.Rows[i].Cells[ioriginalprice].Value) * Convert.ToInt32(Dgv_Selected_Item.Rows[i].Cells[iqty].Value);
                                    decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) - TotalPrice;
                                    txtGrandTotal.Text = Convert.ToString(totalamt);
                                    lblNetAmnt.Text = Convert.ToString(totalamt);
                                    Dgv_Selected_Item.Rows.RemoveAt(i);
                                    Dgv_Selected_Item.Update();
                                    break;
                                }
                            }
                            #endregion
                        }
                        checksave_refresh();
                    }


                    GridSlno_Refresh();
                    netAmount();
                }
                netAmount();
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
        private void txtRcvdAmnt_TextChanged(object sender, EventArgs e)
        {
            if (txtRcvdAmnt.Text != "." && txtRcvdAmnt.Text != "")
            {
                netAmount();
            }
        }

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
        private void btn_close_prod_des_Click(object sender, EventArgs e)
        {
            pnl_main.Enabled = true;
            pnl_products_descr.Visible = false;
        }

        #endregion



        #region Customer Registration 

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
        private void btn_clear_cus_Click_1(object sender, EventArgs e)
        {

            txtph.Text = "";
            txtemailid.Text = "";
            txtaddress.Text = "";
            txtfristname.Text = "";
            txtsecondname.Text = "";
            txtcompanyname.Text = "";
            txtgstno.Text = "";

            rb_female.Checked = false;
            rb_male.Checked = false;
        }

        private void btn_skipcusreg_Click(object sender, EventArgs e)
        {
            pnl_userreg.Visible = false;
        }

        private void btn_userreg_submit_Click(object sender, EventArgs e)
        {
            pnl_userreg.Visible = false;
        }

        private void btn_userreg_cls_Click(object sender, EventArgs e)
        {
            pnl_userreg.Visible = false;
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
        #endregion

        private void btn_va_close_Click(object sender, EventArgs e)
        {
            try
            {
                DisposeControls(flp_addons);
                DisposeControls(flp_variation);

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

        private void btncuslist_Click(object sender, EventArgs e)
        {
            LedgerPopup();
        }
        public void LedgerPopup()
        {
            try
            {
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "CusId,FirstName,SecondName,CompanyName,GSTNo,PhoneNumber,EmailAddress,FullAddress";
                _objSearch.Select = "CusId ID,FirstName Field1,SecondName Field2,CompanyName Field3,GSTNo Field4,PhoneNumber Field5,EmailAddress Field6,FullAddress Field7";
                _objSearch.Table = " tblCustomer ";
                _objSearch.WhereNdGroupBy = " WHERE Status = 1 ";
                _objSearch.OrderBy = " ORDER BY CusId DESC";

                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter,
                    this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);
                    }
                }

                if (!string.IsNullOrEmpty(returnValue))
                {
                    int cusId = Convert.ToInt32(returnValue);
                    txtcusid.Text = cusId.ToString();

                    BllSearch<DmnAccountLedger> bllCustomer = new BllSearch<DmnAccountLedger>();
                    DmnSearch custSearch = new DmnSearch();

                    custSearch.Select = "*";  // Select all columns from tblCustomer
                    custSearch.Table = " tblCustomer ";
                    custSearch.WhereNdGroupBy = " WHERE CusId = " + cusId;

                    var custData = bllCustomer.GetSearchData("prdSearch", custSearch);

                    if (custData != null && custData.Count > 0)
                    {
                        txtfristname.Text = custData[0].FirstName ?? "";
                        txtsecondname.Text = custData[0].SecondName ?? "";
                        txtcompanyname.Text = custData[0].CompanyName ?? "";
                        txtgstno.Text = custData[0].GSTNo ?? "";
                        txtph.Text = custData[0].PhoneNumber ?? "";
                        txtemailid.Text = custData[0].EmailAddress ?? "";
                        txtaddress.Text = custData[0].FullAddress ?? "";

                        // Set gender radio buttons
                        if (custData[0].Gender == 1) // Male
                        {
                            rb_male.Checked = true;
                            rb_female.Checked = false;
                        }
                        else if (custData[0].Gender == 2) // Female
                        {
                            rb_male.Checked = false;
                            rb_female.Checked = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Customer Registration page LedgerPopup()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order)
                );
            }
        }
        private void btnktcancelrefund_Click(object sender, EventArgs e)
        {
            if (dgv_ordercancel.Rows.Count == 0)
            {
                CMessageBox.Show("No cancel item product for refund", "Refund", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            pnl_cancelledlist.Visible = false;
            pnl_Refund_paymode.Visible = true;
            ShowBlurPopup(pnl_Refund_paymode);
            pnl_Refund_paymode.Location = new Point(this.ClientSize.Width / 2 - pnl_Refund_paymode.Size.Width / 2, (this.ClientSize.Height / 2 - pnl_Refund_paymode.Size.Height / 2));
        }
        private void cashRefund_Click(object sender, EventArgs e)
        {
            Refund(1);

        }
        private void cardRefund_Click(object sender, EventArgs e)
        {
            Pnl_wifiPopup.Show();
            Pnl_wifiPopup.BringToFront();
            Pnl_wifiPopup.Location = new Point(this.ClientSize.Width / 2 - Pnl_wifiPopup.Size.Width / 2, (this.ClientSize.Height / 2 - Pnl_wifiPopup.Size.Height / 2));
            tm_midascard.Start();

        }
        private void Refund(int RefundpayType)
        {
            try
            {
                DmnRefund dmnRefund = new DmnRefund();
                BllRefund bllRefund = new BllRefund();
                if (RefundpayType == 1)
                {
                    dmnRefund.CashRefund = Convert.ToDecimal(lblpickupcnlamt.Text);
                }
                else if (RefundpayType == 2)
                {
                    dmnRefund.DebitLedgerId = DebitCustomerId;
                    dmnRefund.CardType = CardTypeno;
                    dmnRefund.FMCardType = Fmcardtypeno;
                    dmnRefund.DebitICardNo = DebitCardNumber; ;
                    dmnRefund.CashBalanceRefund = Convert.ToDecimal(lblpickupcnlamt.Text);
                }

                dmnRefund.SalesMasterId = Convert.ToInt32(txtsalemasterid.Text);
                dmnRefund.OrderMasterId = Convert.ToInt32(txtordermasterid.Text);
                dmnRefund.RefundpayType = RefundpayType;
                dmnRefund.RefundReason = 5;
                dmnRefund.ReasonNote = "Kitchen staff Cancel";

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
                dmnRefund.Cancelorderstatus = ordercancelbystaff;
                dmnRefund.DBname = GameDB;
                var data = bllRefund.AddRefund("prdInsertRefundConsole", dmnRefund);

                //DmnOrderMaster dmnRemove_order = new DmnOrderMaster();
                //BllOrder bll_order = new BllOrder();
                //dmnRemove_order.OrderMasterId = Convert.ToInt32(txtordermasterid.Text);
                //dmnRemove_order.OrderDetailsId = 0;
                //dmnRemove_order.CancelNotifyStatus = 1;
                //dmnRemove_order.PkId = 1;
                //dmnRemove_order.CreatedBy = FrmDashboard.UserId;
                //dmnRemove_order.BranchId = FrmDashboard.BranchId;

                //var InsertStatusP = bll_order.CancelNotifyStatus_update("prd_CancelNotifyStatus_update", dmnRemove_order);
                if (OrderCancelStatus == 1 || ordercancelbystaff)
                {
                    CMessageBox.Show("Refund Completed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DialogResult dr = CMessageBox.Show(
                    "Refund Completed, Are sure to Reconfirm ?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);

                    if (dr == DialogResult.Yes)
                    {
                        ReconfirmOrder();
                    }

                }


                CheckReconfirmPickup();
                pnl_Refund_paymode.Visible = false;

                clsgen.Memoryrelease();
                ResetPickupViewPage();
                ClearAll();
                this.Close();
                this.Dispose();

            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Order page Refund()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order)
                );
            }
        }

        #region Part Payment
        private void cash_part_Click(object sender, EventArgs e)
        {
            try
            {
                partCount = Convert.ToInt32(partPayments.Count);
                decimal bal = Convert.ToDecimal(lblPartBalAmt.Text);
                if (partCount == 0)
                {
                    decimal kdata = 0;
                    using (FoodandBeverage.Forms.UserControl.Frmnumdiskeypad frm = new FoodandBeverage.Forms.UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.None)))
                    {
                        frm.ShowDialog();
                        kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                        frm.Dispose();
                    }

                    if (kdata >= bal || kdata <= 0)
                    {
                        if (exitstatus == false)
                            CMessageBox.Show("Amount should not be greater than TotalAmount.\nCURRENT BALANCE: " + bal, "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        lblpartAmt1.Text = "0.00";
                    }
                    else
                    {
                        txtPayid1.Text = Convert.ToInt32(PaymentType.CASH).ToString();
                        lblpartpayment1.Text = "CASH";
                        lblpartAmt1.Text = kdata.ToString("0.##");
                        partPayments.Add((txtPayid1.Text, lblpartAmt1.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt1.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }
                }
                else if (partCount == 1)
                {
                    if (txtPayid1.Text != Convert.ToInt32(PaymentType.CASH).ToString())
                    {
                        txtPayid2.Text = Convert.ToInt32(PaymentType.CASH).ToString();
                        lblpartAmt2.Text = "CASH";
                        lblpartAmt2.Text = bal.ToString("0.##");
                        partPayments.Add((txtPayid2.Text, lblpartAmt2.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt2.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }
                    else
                    {
                        CMessageBox.Show("Similar Paymode Twice Not Valid", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        lblpartAmt2.Text = "Payment 2";
                    }


                }
                if (string.IsNullOrWhiteSpace(lblPartBalAmt.Text) || lblPartBalAmt.Text == "0.00")
                {
                    if (partPayments.Count == 2)
                    {
                        btnPartPaynow.Visible = true;
                        cash_part.Enabled = false;
                        Debit_part.Enabled = false;
                        credit_part.Enabled = false;
                        netbanking_part.Enabled = false;
                        midas_part.Enabled = false;
                        upi_part.Enabled = false;
                    }

                }
            }

            catch (Exception ex)
            {
                clsgen.LogException("POSDashboard page cash_part_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }

        private void Debit_part_Click(object sender, EventArgs e)
        {
            try
            {
                partCount = Convert.ToInt32(partPayments.Count);
                decimal bal = Convert.ToDecimal(lblPartBalAmt.Text);
                if (partCount == 0)
                {
                    decimal kdata = 0;
                    using (FoodandBeverage.Forms.UserControl.Frmnumdiskeypad frm = new FoodandBeverage.Forms.UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.None)))
                    {
                        frm.ShowDialog();
                        kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                        frm.Dispose();
                    }

                    if (kdata >= bal || kdata <= 0)
                    {
                        if (exitstatus == false)
                            CMessageBox.Show(
    "Amount should not be greater than TotalAmount.\nCURRENT BALANCE: " + bal,
    "Warning",
    MessageBoxButtons.RetryCancel,
    MessageBoxIcon.Warning
);
                        lblpartAmt1.Text = "0.00";
                    }
                    else
                    {
                        txtPayid1.Text = Convert.ToInt32(PaymentType.DEBITCARD).ToString();
                        lblpartpayment1.Text = "DEBITCARD";
                        lblpartAmt1.Text = kdata.ToString("0.##");
                        partPayments.Add((txtPayid1.Text, lblpartAmt1.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt1.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }



                }
                else if (partCount == 1)
                {
                    if (txtPayid1.Text != Convert.ToInt32(PaymentType.DEBITCARD).ToString())
                    {

                        txtPayid2.Text = Convert.ToInt32(PaymentType.DEBITCARD).ToString();
                        lblpartpayment2.Text = "DEBITCARD";
                        lblpartAmt2.Text = bal.ToString("0.##");
                        partPayments.Add((txtPayid2.Text, lblpartAmt2.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt2.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }
                    else
                    {
                        CMessageBox.Show("Similar Paymode Twice Not Valid", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        lblpartAmt2.Text = "Payment 2";
                    }
                }

                if (string.IsNullOrWhiteSpace(lblPartBalAmt.Text) || lblPartBalAmt.Text == "0.00")
                {
                    if (partPayments.Count == 2)
                    {
                        btnPartPaynow.Visible = true;
                        cash_part.Enabled = false;
                        Debit_part.Enabled = false;
                        credit_part.Enabled = false;
                        netbanking_part.Enabled = false;
                        midas_part.Enabled = false;
                        upi_part.Enabled = false;
                    }

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("POSDashboard page Debit_part_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }
        private void upi_part_Click(object sender, EventArgs e)
        {
            try
            {
                partCount = Convert.ToInt32(partPayments.Count);
                decimal bal = Convert.ToDecimal(lblPartBalAmt.Text);
                if (partCount == 0)
                {

                    decimal kdata = 0;
                    using (FoodandBeverage.Forms.UserControl.Frmnumdiskeypad frm = new FoodandBeverage.Forms.UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.None)))
                    {
                        frm.ShowDialog();
                        kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                        frm.Dispose();
                    }

                    if (kdata >= bal || kdata <= 0)
                    {
                        if (exitstatus == false)
                            CMessageBox.Show(
    "Amount should not be greater than TotalAmount.\nCURRENT BALANCE: " + bal,
    "Warning",
    MessageBoxButtons.RetryCancel,
    MessageBoxIcon.Warning
);
                        lblpartAmt1.Text = "0.00";
                    }
                    else
                    {
                        txtPayid1.Text = Convert.ToInt32(PaymentType.UPI).ToString();
                        lblpartpayment1.Text = "UPI";
                        lblpartAmt1.Text = kdata.ToString("0.##");
                        partPayments.Add((txtPayid1.Text, lblpartAmt1.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt1.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }



                }
                else if (partCount == 1)
                {
                    if (txtPayid1.Text != Convert.ToInt32(PaymentType.UPI).ToString())
                    {

                        txtPayid2.Text = Convert.ToInt32(PaymentType.UPI).ToString();
                        lblpartpayment2.Text = "UPI";
                        lblpartAmt2.Text = bal.ToString("0.##");
                        partPayments.Add((txtPayid2.Text, lblpartAmt2.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt2.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }
                    else
                    {
                        CMessageBox.Show("Similar Paymode Twice Not Valid", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        lblpartAmt2.Text = "Payment 2";
                    }

                }
                if (string.IsNullOrWhiteSpace(lblPartBalAmt.Text) || lblPartBalAmt.Text == "0.00")
                {
                    if (partPayments.Count == 2)
                    {
                        btnPartPaynow.Visible = true;
                        cash_part.Enabled = false;
                        Debit_part.Enabled = false;
                        credit_part.Enabled = false;
                        netbanking_part.Enabled = false;
                        midas_part.Enabled = false;
                        upi_part.Enabled = false;
                    }


                }
            }

            catch (Exception ex)
            {
                clsgen.LogException("POSDashboard page upi_part_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }

        private void netbanking_part_Click(object sender, EventArgs e)
        {
            try
            {
                partCount = Convert.ToInt32(partPayments.Count);
                decimal bal = Convert.ToDecimal(lblPartBalAmt.Text);
                if (partCount == 0)
                {
                    decimal kdata = 0;
                    using (FoodandBeverage.Forms.UserControl.Frmnumdiskeypad frm = new FoodandBeverage.Forms.UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.None)))
                    {
                        frm.ShowDialog();
                        kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                        frm.Dispose();
                    }
                    if (kdata >= bal || kdata <= 0)
                    {
                        if (exitstatus == false)
                            CMessageBox.Show(
    "Amount should not be greater than TotalAmount.\nCURRENT BALANCE: " + bal,
    "Warning",
    MessageBoxButtons.RetryCancel,
    MessageBoxIcon.Warning
);
                        lblpartAmt1.Text = "0.00";
                    }
                    else
                    {
                        txtPayid1.Text = Convert.ToInt32(PaymentType.BANKINGONLINE).ToString();
                        lblpartpayment1.Text = "NETBANKING";
                        lblpartAmt1.Text = kdata.ToString("0.##");
                        partPayments.Add((txtPayid1.Text, lblpartAmt1.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt1.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }
                }
                else if (partCount == 1)
                {
                    if (txtPayid1.Text != Convert.ToInt32(PaymentType.BANKINGONLINE).ToString())
                    {

                        txtPayid2.Text = Convert.ToInt32(PaymentType.BANKINGONLINE).ToString();
                        lblpartpayment2.Text = "NETBANKING";
                        lblpartAmt2.Text = bal.ToString("0.##");
                        partPayments.Add((txtPayid2.Text, lblpartAmt2.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt2.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }
                    else
                    {
                        CMessageBox.Show("Similar Paymode Twice Not Valid", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        lblpartAmt2.Text = "Payment 2";
                    }

                }
                if (string.IsNullOrWhiteSpace(lblPartBalAmt.Text) || lblPartBalAmt.Text == "0.00")
                {
                    if (partPayments.Count == 2)
                    {
                        btnPartPaynow.Visible = true;
                        cash_part.Enabled = false;
                        Debit_part.Enabled = false;
                        credit_part.Enabled = false;
                        netbanking_part.Enabled = false;
                        midas_part.Enabled = false;
                        upi_part.Enabled = false;
                    }

                }
            }

            catch (Exception ex)
            {
                clsgen.LogException("POSDashboard page netbanking_part_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }

        private void credit_part_Click(object sender, EventArgs e)
        {
            try
            {
                partCount = Convert.ToInt32(partPayments.Count);
                decimal bal = Convert.ToDecimal(lblPartBalAmt.Text);
                if (partCount == 0)
                {

                    decimal kdata = 0;
                    using (FoodandBeverage.Forms.UserControl.Frmnumdiskeypad frm = new FoodandBeverage.Forms.UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.None)))
                    {
                        frm.ShowDialog();
                        kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                        frm.Dispose();
                    }
                    if (kdata >= bal || kdata <= 0)
                    {
                        if (exitstatus == false)
                            CMessageBox.Show(
    "Amount should not be greater than TotalAmount.\nCURRENT BALANCE: " + bal,
    "Warning",
    MessageBoxButtons.RetryCancel,
    MessageBoxIcon.Warning
);
                        lblpartAmt1.Text = "0.00";
                    }
                    else
                    {
                        txtPayid1.Text = Convert.ToInt32(PaymentType.CREDITCARD).ToString();
                        lblpartpayment1.Text = "CREDITCARD";
                        lblpartAmt1.Text = kdata.ToString("0.##");
                        partPayments.Add((txtPayid1.Text, lblpartAmt1.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt1.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }

                }

                else if (partCount == 1)
                {
                    if (txtPayid1.Text != Convert.ToInt32(PaymentType.CREDITCARD).ToString())
                    {
                        txtPayid2.Text = Convert.ToInt32(PaymentType.CREDITCARD).ToString();
                        lblpartpayment2.Text = "CREDITCARD";
                        lblpartAmt2.Text = bal.ToString("0.##");
                        partPayments.Add((txtPayid2.Text, lblpartAmt2.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt2.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }
                    else
                    {
                        CMessageBox.Show("Similar Paymode Twice Not Valid", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        lblpartAmt2.Text = "Payment 2";
                    }

                }
                if (string.IsNullOrWhiteSpace(lblPartBalAmt.Text) || lblPartBalAmt.Text == "0.00")
                {
                    if (partPayments.Count == 2)
                    {
                        btnPartPaynow.Visible = true;
                        cash_part.Enabled = false;
                        Debit_part.Enabled = false;
                        credit_part.Enabled = false;
                        netbanking_part.Enabled = false;
                        midas_part.Enabled = false;
                        upi_part.Enabled = false;
                    }

                }
            }

            catch (Exception ex)
            {
                clsgen.LogException("POSDashboard page credit_part_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }

        private void midas_part_Click(object sender, EventArgs e)
        {
            Pnl_wifiPopup.Show();
            Pnl_wifiPopup.BringToFront();
            Pnl_wifiPopup.Location = new Point(this.ClientSize.Width / 2 - Pnl_wifiPopup.Size.Width / 2, (this.ClientSize.Height / 2 - Pnl_wifiPopup.Size.Height / 2));
            tm_midascard.Start();
        }

        private void btn_ktcancelrefund_Click(object sender, EventArgs e)
        {
            ordercancelbystaff = false;
            pnl_Refund_paymode.Visible = false;
            pnl_cancelledlist.Visible = false;
        }

        private void btn_cancel_list_Click(object sender, EventArgs e)
        {
            ordercancelbystaff = false;
            pnl_Refund_paymode.Visible = false;
            pnl_cancelledlist.Visible = false;
        }

        private void btnsub_cancelorder_Click(object sender, EventArgs e)
        {

        }

        private void btn_cancelorder_Click(object sender, EventArgs e)
        {
            pnl_cancelorder.Visible = false;
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
                    DialogResult dr = CMessageBox.Show(
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

                        CMessageBox.Show("Cancel item notification confirmed!", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnordercallist.Visible = false;
                        btn_pa__toggle.PerformClick();
                    }
                    CheckReConfirmTableorder();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page dgv_ordercancel_CellClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }

        private void txtpkchrge_amt_TextChanged(object sender, EventArgs e)
        {
            netAmount();
        }


        private void txtpkchrge_amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // Allow only one decimal point
            if (e.KeyChar == '.' && txt.Text.Contains("."))
            {
                e.Handled = true;
            }
        }

        private void btn_Guest_close_Click(object sender, EventArgs e)
        {
            txtadults.Text = Convert.ToString(1);
            txtchild.Text = Convert.ToString(0);
            txtother.Text = Convert.ToString(0);
            totguests.Text = Convert.ToString(1);
            pnl_Guests.Visible = false;
        }

        private void btn_searchbox_close_Click(object sender, EventArgs e)
        {
            txtadults.Text = Convert.ToString(1);
            txtchild.Text = Convert.ToString(0);
            txtother.Text = Convert.ToString(0);
            totguests.Text = Convert.ToString(1);
            pnl_Guests.Visible = false;
        }

        private void btn_startOrder_Click(object sender, EventArgs e)
        {
            pnl_Guests.Visible = false;
        }

        private void txtadults_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalGuests();
        }
        private void UpdateTotalGuests()
        {
            try
            {
                int adults = int.TryParse(txtadults.Text, out var a) ? a : 1;
                int child = int.TryParse(txtchild.Text, out var c) ? c : 0;
                int other = int.TryParse(txtother.Text, out var o) ? o : 0;

                totguests.Text = (adults + child + other).ToString();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page UpdateTotalGuests()", ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order));
            }
        }



        private void txtadults_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits, backspace, decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }



        private void txtchild_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits, backspace, decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }



        private void txtother_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow digits, backspace, decimal point
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btncls_mulprice_Click(object sender, EventArgs e)
        {
            pnl_multprice.Visible = false;
        }

        private void txtadults_Leave(object sender, EventArgs e)
        {
            Guest_Leave(sender, e);
        }

        private void Guest_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (txt == null) return;

            // Adults must be minimum 1
            if (txt == txtadults)
            {
                if (!int.TryParse(txt.Text, out int val) || val < 1)
                    txt.Text = "1";
            }
            else
            {
                // Child & Other minimum 0
                if (!int.TryParse(txt.Text, out int val) || val < 0)
                    txt.Text = "0";
            }

            UpdateTotalGuests();
        }

        private void txtadults_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_OrderPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.FunctionKey));
            }
        }

        private void btnPartPaynow_Click(object sender, EventArgs e)
        {
            pnl_partpayment.Visible = false;
            paybillcal(Convert.ToInt32(PaymentType.SPLIT));
            Memoryrelease();
        }

        private void txtchild_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_OrderPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.FunctionKey));
            }
        }

        private void txtother_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_OrderPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.FunctionKey));
            }
        }

        private void txtfristname_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_OrderPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtsecondname_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_OrderPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtcompanyname_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_OrderPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtgstno_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_OrderPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtph_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_OrderPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.FunctionKey));
            }
        }

        private void txtemailid_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_OrderPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtaddress_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_OrderPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void btn_orderproritycls_Click(object sender, EventArgs e)
        {
            pnl_orderprority.Visible = false;
        }

        private void btn_submit_orderprority_Click(object sender, EventArgs e)
        {
            pnl_orderprority.Visible = false;
        }

        private void btnclos_payment_Click(object sender, EventArgs e)
        {
            pnl_main.Enabled = true;
            pnlPaymode.Visible = false;
        }



        private void btnPartRefresh_Click(object sender, EventArgs e)
        {
            partPayments.Clear();
            lblpartpayment1.Text = "Payment 1";
            lblpartAmt2.Text = "Payment 2";
            lblpartAmt1.Text = "0.00";
            lblpartAmt2.Text = "0.00";
            lblPartBalAmt.Text = lblPartNetAmt.Text;
            btnPartPaynow.Visible = false;
            cash_part.Enabled = true;
            Debit_part.Enabled = true;
            credit_part.Enabled = true;
            netbanking_part.Enabled = true;
            midas_part.Enabled = true;
            upi_part.Enabled = true;
        }



        private void btnPartClose_Click(object sender, EventArgs e)
        {
            pnlSplit.Visible = false;
            pnlPaymode.Visible = true;
            ShowBlurPopup(pnlPaymode);
            partPayments.Clear();
            lblpartpayment1.Text = "Payment 1";
            lblpartAmt2.Text = "Payment 2";
            lblpartAmt1.Text = "0.00";
            lblpartAmt2.Text = "0.00";

        }

       

        public void Fetch_midas()
        {
            try
            {
                partCount = Convert.ToInt32(partPayments.Count);
                if (partCount == 0)
                {


                    decimal kdata = 0;
                    using (FoodandBeverage.Forms.UserControl.Frmnumdiskeypad frm = new FoodandBeverage.Forms.UserControl.Frmnumdiskeypad(Convert.ToInt32(KeypadType.None)))
                    {
                        frm.ShowDialog();
                        kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                        frm.Dispose();
                    }
                    decimal bal = Convert.ToDecimal(lblPartBalAmt.Text);
                    if (kdata > midas_balance)
                    {
                        CMessageBox.Show("Card Not have Sufficient Balance. \nNeed Balance Payment :" + bal, "Information", MessageBoxButtons.RetryCancel, MessageBoxIcon.Information);
                        lblpartAmt1.Text = "0.00";
                    }
                    else if (kdata >= bal || kdata <= 0)
                    {
                        if (exitstatus == false)
                            CMessageBox.Show("Amount should not be greater than TotalAmount. \nCURRENT BALANCE :" + bal, "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        lblpartAmt1.Text = "0.00";
                    }
                    else
                    {
                        txtPayid1.Text = Convert.ToInt32(PaymentType.MidasCard).ToString();
                        lblpartpayment1.Text = "MIDASCARD";
                        lblpartAmt1.Text = kdata.ToString("0.##");
                        partPayments.Add((txtPayid1.Text, lblpartAmt1.Text));
                        decimal amt2 = Convert.ToDecimal(lblpartAmt1.Text);

                        decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                            ? Convert.ToDecimal(lblPartNetAmt.Text)
                            : Convert.ToDecimal(lblPartBalAmt.Text);

                        decimal balAmt = currentBalance - amt2;
                        lblPartBalAmt.Text = balAmt.ToString();
                    }

                }
                else if (partCount == 1)
                {
                    if (txtPayid1.Text != Convert.ToInt32(PaymentType.MidasCard).ToString())
                    {
                        decimal bal = Convert.ToDecimal(lblPartBalAmt.Text);
                        if (bal > midas_balance)
                        {
                            CMessageBox.Show("Card Not have Sufficient Balance. \nNeed Balance Payment :" + bal, "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                            lblpartAmt2.Text = "0.00";
                        }
                        else
                        {
                            txtPayid2.Text = Convert.ToInt32(PaymentType.MidasCard).ToString();
                            lblpartAmt2.Text = "MIDASCARD";
                            lblpartAmt2.Text = lblPartBalAmt.Text;
                            partPayments.Add((txtPayid2.Text, lblpartAmt2.Text));
                            decimal amt2 = Convert.ToDecimal(lblpartAmt2.Text);

                            decimal currentBalance = string.IsNullOrWhiteSpace(lblPartBalAmt.Text)
                                ? Convert.ToDecimal(lblPartNetAmt.Text)
                                : Convert.ToDecimal(lblPartBalAmt.Text);

                            decimal balAmt = currentBalance - amt2;
                            lblPartBalAmt.Text = balAmt.ToString();
                        }
                    }
                    else
                    {
                        CMessageBox.Show("Similar Paymode Twice Not Valid", "Warning", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                        lblpartAmt2.Text = "Payment 2";
                    }

                }
                if (string.IsNullOrWhiteSpace(lblPartBalAmt.Text) || lblPartBalAmt.Text == "0.00")
                {
                    if (partPayments.Count == 2)
                    {
                        btnPartPaynow.Visible = true;
                        cash_part.Enabled = false;
                        Debit_part.Enabled = false;
                        credit_part.Enabled = false;
                        netbanking_part.Enabled = false;
                        midas_part.Enabled = false;
                        upi_part.Enabled = false;
                    }

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException(" page Fetch_midas()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
        }




        #endregion

        #region Keyboard

        public void ShowKeyboard(Control activeControl, int kbtype)
        {
            try
            {
                ToggleWifiPopup(0);
                if (activeControl == null) return;

                if (activeControl is TextBox activeTextBox)
                {
                    Point textBoxScreenLocation = activeTextBox.PointToScreen(Point.Empty);
                    int desiredX = 0, desiredY = 0;
                    if (Frmkeyboard.IsOpen)
                    {
                        Frmkeyboard.Instance.Close();
                    }

                    Frmkeyboard frmKey = new Frmkeyboard(kbtype);

                    #region Calculate Position of Keyboard 
                    desiredX = textBoxScreenLocation.X + (activeTextBox.Width / 2) - (Frmkeyboard.Instance.Width / 2);
                    desiredY = textBoxScreenLocation.Y + activeTextBox.Height + 5;
                    Rectangle screenBounds = Screen.FromControl(activeTextBox).WorkingArea;
                    if (desiredX < screenBounds.Left)
                        desiredX = screenBounds.Left;
                    else if (desiredX + Frmkeyboard.Instance.Width > screenBounds.Right)
                        desiredX = screenBounds.Right - Frmkeyboard.Instance.Width;
                    if (desiredY + Frmkeyboard.Instance.Height > screenBounds.Bottom)
                    {
                        desiredY = textBoxScreenLocation.Y - Frmkeyboard.Instance.Height - 5;
                        if (desiredY < screenBounds.Top)
                            desiredY = screenBounds.Top;
                    }
                    Frmkeyboard.Instance.StartPosition = FormStartPosition.Manual;
                    Frmkeyboard.Instance.Location = new Point(desiredX, desiredY);
                    #endregion


                    frmKey.OnKeyPressed += (keyText) =>
                    {
                        if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.backspace))
                        {
                            if (activeTextBox.Text.Length > 0)
                                activeTextBox.Text = activeTextBox.Text.Substring(0, activeTextBox.Text.Length - 1);
                        }
                        else if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.space))
                        {
                            activeTextBox.Text += " ";
                        }
                        else if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.clear))
                        {
                            activeTextBox.Text = "";
                        }
                        else if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.keyf8))
                        {
                            if (Convert.ToInt32(activeTextBox.Tag) == 1)
                            { }
                        }
                        else
                        {
                            activeTextBox.Text += keyText;
                        }
                        activeTextBox.SelectionStart = activeTextBox.Text.Length;
                        activeTextBox.SelectionLength = 0;
                    };

                    frmKey.Show();
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException(" page ShowKeyboard()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));

            }

        }
        #endregion

        #region Variatioin
        private void btncls_variation_Click(object sender, EventArgs e)
        {
            pnl_variation.Visible = false;
        }
        #endregion

        #region Sale Discount Percentage and Discount Amount
        public void CalculateFullRow(int rowIndex)
        {
            try
            {
                var row = Dgv_Selected_Item.Rows[rowIndex];
                if (row.IsNewRow) return;

                if (row.Cells[iitemid].Value == null ||
                    row.Cells[israte].Value == null ||
                    row.Cells[iqty].Value == null)
                    return;

                string taxType = "";
                decimal taxPercent = 0;
                decimal taxAmount = 0;
                decimal rate = 0;
                bool isExclusive = Convert.ToString(row.Cells[itax].Value) == "Exclusive";
                if (isExclusive)
                {
                    BllSearch<DmnTaxSettings> bll = new BllSearch<DmnTaxSettings>();
                    DmnSearch search = new DmnSearch();
                    search.Select = "TaxType, IgstPrcnt, VatPrcnt";
                    search.Table = "tblTaxSettings";
                    search.WhereNdGroupBy = " WHERE status=1 AND TaxSettingsId=" + row.Cells[itaxsettingid].Value;
                    var taxData = bll.GetSearchData("prdSearch", search);
                    if (taxData.Count == 0) return;

                    taxType = taxData[0].TaxType;

                    if (taxType == "GST")
                        taxPercent = Convert.ToDecimal(taxData[0].IgstPrcnt);
                    else if (taxType == "VAT")
                        taxPercent = Convert.ToDecimal(taxData[0].VatPrcnt);
                    decimal srate = Convert.ToDecimal(row.Cells[israte].Value);
                    rate = srate + ((Convert.ToDecimal(taxPercent) * Convert.ToDecimal(srate)) / 100);
                }
                else
                {
                    rate = Convert.ToDecimal(row.Cells[israte].Value);
                }

                decimal qty = Convert.ToDecimal(row.Cells[iqty].Value);

                // 1️⃣ Amount
                decimal amount = rate * qty;
                row.Cells[iamount].Value = amount.ToString("N2");


                // 2️⃣ Item Discount
                decimal itemDisPer = row.Cells[idisper].Value == null ? 0 :
                                     Convert.ToDecimal(row.Cells[idisper].Value);

                decimal itemDisAmt = (itemDisPer * amount) / 100;
                row.Cells[idisamt].Value = itemDisAmt.ToString("N2");

                decimal afterItemDiscount = amount - itemDisAmt;

                // 3️⃣ Sale Discount
                decimal saleDisPer = row.Cells[iSaledisperno].Value == null ? 0 :
                                     Convert.ToDecimal(row.Cells[iSaledisperno].Value);

                decimal saleDisAmt = row.Cells[isalediamtno].Value == null ? 0 :
                                     Convert.ToDecimal(row.Cells[isalediamtno].Value);
                row.Cells[isalediamtno].Value = saleDisAmt.ToString("N2");

                decimal taxableAmount = afterItemDiscount - saleDisAmt;



                if (Convert.ToInt32(row.Cells[istatusno].Value) == 0)
                {
                    taxType = Convert.ToString(row.Cells[itaxtype].Value);
                    taxPercent = Convert.ToDecimal(row.Cells[itaxper].Value);
                }
                else
                {
                    BllSearch<DmnTaxSettings> bll = new BllSearch<DmnTaxSettings>();
                    DmnSearch search = new DmnSearch();
                    search.Select = "TaxType, IgstPrcnt, VatPrcnt";
                    search.Table = "tblTaxSettings";
                    search.WhereNdGroupBy = " WHERE status=1 AND TaxSettingsId=" + row.Cells[itaxsettingid].Value;
                    var taxData = bll.GetSearchData("prdSearch", search);
                    if (taxData.Count == 0) return;

                    taxType = taxData[0].TaxType;

                    if (taxType == "GST")
                        taxPercent = Convert.ToDecimal(taxData[0].IgstPrcnt);
                    else if (taxType == "VAT")
                        taxPercent = Convert.ToDecimal(taxData[0].VatPrcnt);
                    row.Cells[itaxper].Value = taxPercent;
                    row.Cells[itaxtype].Value = taxType;
                }

                if (isExclusive)
                {
                    taxAmount = (taxPercent * taxableAmount) / 100;
                    row.Cells[ipretaxamtno].Value = taxableAmount.ToString("N2");

                }
                else // Inclusive
                {
                    decimal baseAmount = taxableAmount * 100 / (100 + taxPercent);
                    taxAmount = taxableAmount - baseAmount;
                    row.Cells[ipretaxamtno].Value = baseAmount.ToString("N2");
                }

                row.Cells[itaxamt].Value = taxAmount.ToString("N2");

                if (taxType == "GST")
                    row.Cells[iigstamtno].Value = taxAmount.ToString("N2");
                else
                    row.Cells[ivatamtno].Value = taxAmount.ToString("N2");


                //decimal finalTotal = isExclusive
                //? taxableAmount + taxAmount
                // : taxableAmount;
                decimal finalTotal = taxableAmount;

                row.Cells[itotal].Value = finalTotal.ToString("N2");
                row.Cells[ioriginalprice].Value = finalTotal.ToString("N2");

                decimal totalamt = Convert.ToDecimal(txtGrandTotal.Text) + Convert.ToDecimal(row.Cells[ioriginalprice].Value);

                txtGrandTotal.Text = totalamt.ToString();
                lblNetAmnt.Text = totalamt.ToString();

                netAmount();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page CalculateFullRow()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order));
            }
        }

      

        #endregion

        #region Round OFf     
        private void btn_auto_roundoff_Click(object sender, EventArgs e)
        {
            try
            {
                hasRestoredRoundoffAdjustment = false;
                restoredRoundoffAdjustment = 0m;
                currentRoundMode = RoundOffMode.Auto;
                originalRoundValue = 0;
                isRoundValueCaptured = false;

                // Use the common calculation so packing charges and their tax
                // are included in the automatic rounded total.
                CalculateNetamount_ALl();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page AutoRoundOff()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order));
            }
        }
        private void txtroundoff_Click(object sender, EventArgs e)
        {
            try
            {
                hasRestoredRoundoffAdjustment = false;
                restoredRoundoffAdjustment = 0m;
                currentRoundMode = RoundOffMode.Manual;

                isRoundValueCaptured = false;
                originalRoundValue = 0;

                decimal kdata = 0;

                using (var frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.Roundoff)))
                {
                    frm.ShowDialog();
                    decimal.TryParse(frm.txtkeypadvalue.Text, out kdata);
                }

                txtroundoff.Text = kdata.ToString("0.##");

                originalRoundValue = kdata;

                decimal receivedAmt = GetDecimalValue(txtRcvdAmnt);

                // lbltaxableamt already includes discounted food taxable value and,
                // when configured, the packing taxable base. Keep an unconfigured
                // packing charge outside the tax total.
                decimal packingAddOn = Math.Max(0m, GetDecimalValue(txtpkchrge_amt));
                string configuredPackingMode;
                string configuredPackingType;
                decimal configuredPackingRate;

                if (TryGetPackingTaxConfiguration(
                    out configuredPackingMode,
                    out configuredPackingType,
                    out configuredPackingRate))
                {
                    packingAddOn = 0m;
                }

                decimal netAmount =
                    GetDecimalValue(lbltaxableamt) +
                    GetDecimalValue(lblTotTaxAmnt) +
                    packingAddOn;

                decimal finalAmount = netAmount;
                decimal roundOffValue = 0;

                if (originalRoundValue > 0)
                {
                    finalAmount = originalRoundValue;
                    roundOffValue = originalRoundValue - netAmount;
                }
                else
                {
                    finalAmount = netAmount;
                    roundOffValue = 0;
                }

                // Set values
                txtroundoff.Text = roundOffValue.ToString("N2");
                lblNetAmnt.Text = finalAmount.ToString("N2");

                lblBalcAmnt.Text = (finalAmount - receivedAmt).ToString("N2");
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page txtroundoff_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }

        }



        #endregion

        #region Cart

        private void btn_close_productcart_Click(object sender, EventArgs e)
        {
            try
            {
                SuspendLayout();
                pnl_productCart.SuspendLayout();
                tlp_main_productcart.SuspendLayout();
                tlp_productlist.SuspendLayout();
                Dgv_Selected_Item.SuspendLayout();

                MoveSelectedGrid(tlp_productlist, 0, 0);
                pnl_productCart.Visible = false;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btn_close_productcart_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
            finally
            {
                Dgv_Selected_Item.ResumeLayout(false);
                tlp_productlist.ResumeLayout(false);
                tlp_main_productcart.ResumeLayout(false);
                pnl_productCart.ResumeLayout(false);
                ResumeLayout(false);

                tlp_productlist.PerformLayout();
                tlp_main_productcart.PerformLayout();
            }
        }



        private void btn_cart_Click(object sender, EventArgs e)
        {
            try
            {
                SuspendLayout();
                pnl_productCart.SuspendLayout();
                tlp_main_productcart.SuspendLayout();
                tlp_productlist.SuspendLayout();
                Dgv_Selected_Item.SuspendLayout();

                if (!pnl_productCart.Visible)
                {
                    UIHelper.CenterAndResize(pnl_productCart, 90, 90);
                    UIHelper.RoundCorners(pnl_productCart, 25);
                    UIHelper.RoundCorners(tlp_main_productcart, 25);
                    UIHelper.RoundCorners(tlp_productcart_nav, 25);
                    
                    MoveSelectedGrid(tlp_main_productcart, 0, 1);
                    pnl_productCart.Visible = true;
                ShowBlurPopup(pnl_productCart);
                    pnl_productCart.BringToFront();
                }
                else
                {
                    MoveSelectedGrid(tlp_productlist, 0, 0);
                    pnl_productCart.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page btn_cart_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));
            }
            finally
            {
                Dgv_Selected_Item.ResumeLayout(false);
                tlp_productlist.ResumeLayout(false);
                tlp_main_productcart.ResumeLayout(false);
                pnl_productCart.ResumeLayout(false);
                ResumeLayout(false);

                tlp_productlist.PerformLayout();
                tlp_main_productcart.PerformLayout();
            }
        }

       

        private void MoveSelectedGrid(TableLayoutPanel targetPanel, int column, int row)
        {
            if (Dgv_Selected_Item.Parent != null)
                Dgv_Selected_Item.Parent.Controls.Remove(Dgv_Selected_Item);

            targetPanel.Controls.Add(Dgv_Selected_Item, column, row);
            Dgv_Selected_Item.Dock = DockStyle.Fill;
            Dgv_Selected_Item.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Dgv_Selected_Item.Visible = true;
        }
        private void InitializePaymentCalculationPanel()
        {
            if (tlp_netalc_buttons.Controls.Contains(flp_oper_buttons))
                tlp_netalc_buttons.Controls.Remove(flp_oper_buttons);

            if (!tlp_netalc_buttons.Controls.Contains(tlp_calculation_total))
                tlp_netalc_buttons.Controls.Add(tlp_calculation_total, 0, 0);

            tlp_calculation_total.Dock = DockStyle.Fill;
            tlp_calculation_total.Visible = true;
            tlp_calculation_total.BringToFront();

            if (tlp_netalc_buttons.RowStyles.Count > 0)
            {
                tlp_netalc_buttons.RowStyles[0].SizeType = SizeType.Percent;
                tlp_netalc_buttons.RowStyles[0].Height = 100;
            }

            flp_oper_buttons.Visible = false;
            isOperationShown = false;
        }
        #endregion

        #region Complementry

        private void btn_com_raeson_save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtcom_reason.Text) &&
                string.IsNullOrWhiteSpace(cmb_com_reason.Text))
            {
                MessageBox.Show(
                    "Please enter a reason or select a reason from the list.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtcom_reason.Focus();
                return;
            }

            paybillcal(Convert.ToInt32(PaymentType.COMPLEMENTRY));
        }


        private void btn_com_reason_cls_Click(object sender, EventArgs e)
        {
            pnl_complementry_reason.Visible = false;
            pnl_main.Enabled = true;
        }


        private void txtcom_reason_Enter(object sender, EventArgs e)
        {
            if (txtcom_reason.Text == reasonPlaceholder)
            {
                txtcom_reason.Text = "";
                txtcom_reason.ForeColor = Color.Black;
            }
        }

        private void txtcom_reason_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtcom_reason.Text))
            {
                txtcom_reason.Text = reasonPlaceholder;
                txtcom_reason.ForeColor = Color.Gray;
            }
        }
        #endregion

        #region Drag & Drop
        private void lbl_header_cart_MouseDown( object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            ReleaseCapture();

            SendMessage(
                this.Handle,
                WM_NCLBUTTONDOWN,
                (IntPtr)HTCAPTION,
                IntPtr.Zero);
        }

        private void txtpkchrge_amt_Click(object sender, EventArgs e)
        {
            try
            {
                decimal packingCharge = 0m;

                using (var frm = new Frmnumdiskeypad(
                    Convert.ToInt32(KeypadType.Roundoff)))
                {
                    frm.ShowDialog();
                    if (!decimal.TryParse(frm.txtkeypadvalue.Text, out packingCharge))
                        return;
                }

                if (packingCharge < 0m)
                {
                    CMessageBox.Show(
                        "Packing charge cannot be negative.",
                        "Invalid Packing Charge",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                txtpkchrge_amt.Text = packingCharge.ToString("0.00");
                netAmount();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Order page txtpkchrge_amt_Click()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order));
            }
        }



        private void EnableProductCartDragging()
        {
            // Drag using the Product Cart header or its title.
            tlp_productcart_nav.Cursor = Cursors.SizeAll;
            lbl_header_cart.Cursor = Cursors.SizeAll;

            tlp_productcart_nav.MouseDown += ProductCartHeader_MouseDown;
            tlp_productcart_nav.MouseMove += ProductCartHeader_MouseMove;
            tlp_productcart_nav.MouseUp += ProductCartHeader_MouseUp;

            lbl_header_cart.MouseDown += ProductCartHeader_MouseDown;
            lbl_header_cart.MouseMove += ProductCartHeader_MouseMove;
            lbl_header_cart.MouseUp += ProductCartHeader_MouseUp;
        }

        private void ProductCartHeader_MouseDown(
            object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left ||
                !pnl_productCart.Visible)
                return;

            productCartDragSource = sender as Control;

            if (productCartDragSource == null)
                return;

            isDraggingProductCart = true;

            productCartDragStartMouse = Control.MousePosition;
            productCartDragStartLocation = pnl_productCart.Location;

            // Continue tracking the mouse even if it leaves the header.
            productCartDragSource.Capture = true;
        }

        private void ProductCartHeader_MouseMove(
            object sender, MouseEventArgs e)
        {
            if (!isDraggingProductCart ||
                e.Button != MouseButtons.Left)
                return;

            Point mouseNow = Control.MousePosition;

            int x = productCartDragStartLocation.X
                  + mouseNow.X - productCartDragStartMouse.X;

            int y = productCartDragStartLocation.Y
                  + mouseNow.Y - productCartDragStartMouse.Y;

            // Keep the floating cart inside its parent form.
            Control parent = pnl_productCart.Parent;

            if (parent != null)
            {
                x = Math.Max(0, Math.Min(
                    x, parent.ClientSize.Width - pnl_productCart.Width));

                y = Math.Max(0, Math.Min(
                    y, parent.ClientSize.Height - pnl_productCart.Height));
            }

            // Move only the Product Cart panel.
            pnl_productCart.Location = new Point(x, y);
        }

        private void ProductCartHeader_MouseUp(
            object sender, MouseEventArgs e)
        {
            isDraggingProductCart = false;

            if (productCartDragSource != null)
                productCartDragSource.Capture = false;

            productCartDragSource = null;
        }
        #endregion
    }
}
