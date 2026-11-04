using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms;
using FoodandBeverage.Forms.UserControl;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Color = System.Drawing.Color;
using Control = System.Windows.Forms.Control;
using Font = System.Drawing.Font;

namespace FoodandBeverage
{
    public partial class FrmPartyBooking : Form
    {
        #region Decalation

        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsgen = new ClsGeneral();

        #region  Grid Declaration

        public int showonlyslno = 0;
        public int showonlyitemno = 1;
        public int showonlycategmenuno = 2;
        public int showonlyqtyno = 3;
        public int showonlyactionno = 4;
        public int showonlyppcdiidno = 5;

        public int customitemslno = 0;
        public int customitemselectNo = 1;
        public int customitemno = 2;
        public int customitemcategmenuno = 3;
        public int customitemqtyno = 4;
        public int customitemppcdiidno = 5;

        public int selecteditemslno = 0;
        public int selecteditemitemno = 1;
        public int selecteditemcategmenuno = 2;
        public int selecteditemqtyno = 3;
        public int selecteditemppcdiidno = 4;
        public int selecteditemdeafultno = 5;

        public int pslno = 0;
        public int pactionno = 1;
        public int pitemno = 2;
        public int pcategmenuno = 3;
        public int pqtyno = 4;
        public int pPrice = 5;
        public int pAmount = 6;
        public int ptax = 7;
        public int ptaxtype = 8;
        public int ptaxper = 9;
        public int ptaxamt = 10;
        public int pdiscper = 11;
        public int pdisamt = 12;
        public int pigstamt = 13;
        public int pvatamt = 14;
        public int pnetamt = 15;
        public int pProductid = 16;
        public int pproducttype = 17;
        public int proPartyPackDetailsId = 18;
        public int pPartydeafult = 19;
        public int ptaxsettingid = 20;
        public int ppretaxamt = 21;
        public int pmessageno = 22;
        public int premarksno = 23;
        public int psalemasteridno = 24;
        public int psaledetailidno = 25;
        public int PPackProductTypeno = 26;
        public int ppridno = 27;
        public int phsncodeno = 28;


        public int Cancelslno = 0;
        public int Cancelproductno = 1;
        public int Cancelqtyno = 2;
        public int Cancelpriceno = 3;


        public int aislno = 0;
        public int aiactionno = 1;
        public int aiitemno = 2;
        public int aicategmenuno = 3;
        public int aiqtyno = 4;
        public int aiPrice = 5;
        public int aiAmount = 6;
        public int aitax = 7;
        public int aitaxtype = 8;
        public int aitaxper = 9;
        public int aitaxamt = 10;
        public int aidiscper = 11;
        public int aidisamt = 12;
        public int aiigstamt = 13;
        public int aivatamt = 14;
        public int ainetamt = 15;
        public int aiProductid = 16;
        public int aiproducttype = 17;
        public int aiproPartyPackDetailsId = 18;
        public int aiPartydeafult = 19;
        public int aitaxsettingid = 20;
        public int aipretaxamt = 21;
        public int aimessageno = 22;
        public int airemarksno = 23;
        public int aisalemasteridno = 24;
        public int aisaledetailidno = 25;
        public int aiPackProductTypeno = 26;


        #endregion

        #region Domain

        public class BookingItemAdjustment
        {
            public int SlNo { get; set; }
            public string Product { get; set; }
            public string ProductCategory { get; set; }
            public decimal Qty { get; set; }
            public decimal Price { get; set; }
            public decimal Amount { get; set; }
            public int ProductId { get; set; }
            public int SaleDetailId { get; set; }
            public int ProductType { get; set; }
            public int PartyBookingDetailsid { get; set; }
            public string AdjustmentType { get; set; }
        }

        public class SelectedCustomItem
        {
            public int PartyPackCustomDetailsId { get; set; }
            public string ItemName { get; set; }
            public int CategoryId { get; set; }
            public decimal Qty { get; set; }
        }
        #endregion

        private List<DmnPartyPackDetails> packDetailsList;
        private List<DmnCategory> categoryList = new List<DmnCategory>();

        List<SelectedCustomItem> selectedCustomItems = new List<SelectedCustomItem>();
        int CurrentPackId = 0;
        List<DmnPartyPackCustom> customMenuList = new List<DmnPartyPackCustom>();

        // Cake
        private int _cakeId = 0;
        private string _cakeSize = "";
        private string _cakeShape = "";
        private string _cakeMessage = "";
        private string _cakeRemarks = "";
        private decimal _cakePrice = 0;
        private string _cakeHsnCode = "";

        // Tax
        private string _cakeTaxType = "";      // GST / VAT
        private decimal _cakeTaxPer = 0;       // 5, 12, 18
        private int _cakeTaxSettingsId = 0;    // Tax Id
        private string _cakeTaxMode = "";      // Inclusive / Exclusive

        // ===== CAKE TAX GLOBALS =====
        private decimal _cakePreTaxAmount = 0;
        private decimal _cakeTaxAmount = 0;
        private decimal _cakeNetAmount = 0;

        int CategoryFontstatus = Convert.ToInt32(ConfigurationManager.AppSettings["categoryfontSettingvalue"]);
        string CategoryFontdata = Convert.ToString(ConfigurationManager.AppSettings["categoryfontdata"]);
        string Categorybg = Convert.ToString(ConfigurationManager.AppSettings["categorybg"]);

        public int screnwidth = Screen.PrimaryScreen.Bounds.Width;
        public int screnheight = Screen.PrimaryScreen.Bounds.Height;
        public string GameDB = Convert.ToString(ConfigurationManager.AppSettings["GameDB"]);

        int Partypackid = 0;
        private string _partyPackHsnCode = string.Empty;
        private bool _fixedPriceStatus;
        private decimal _fixedPackPrice;
        private const string DefaultCardHsnCode = "8523";
        int PaymentMode = 0;
        bool AdvanceFlag = false;

        decimal dbReceivedAmount = 0;
        decimal dbAdvanceAmount = 0;

        private List<(string PartPayId, string PartAmount)> partPayments = new List<(string, string)>();
        int partCount = 0;
        public bool exitstatus = false;
        int BookingId = 0, SalesMasterId = 0, PartyBookingDetailsId = 0, Partypacksaledetailid = 0;
        decimal MinimumAdvance = 0;

        private readonly Image Grid_Remove = Properties.Resources.Round_Close;
        private readonly Image Blankimage = Properties.Resources.backspace;
        private bool isCategoryExpanded = false;

        List<BookingItemAdjustment> bookingItemAdjustments = new List<BookingItemAdjustment>();

        decimal dbNetAmount = 0;
        decimal dbBalanceAmount = 0;

        private bool isUpdating = false;

        #region Blur Popup Overlay
        private PictureBox _blurPopupPictureBox;
        private Bitmap _blurPopupImage;
        private readonly HashSet<Panel> _activeBlurPopupPanels = new HashSet<Panel>();
        private bool _isUpdatingBlurPopup;
        private bool _isBlurPopupReleaseScheduled;
        private const int BlurPreviewScale = 4;
        private const byte BlurDarkOverlayAlpha = 72;
        #endregion

        private const int DefaultKotLeadTimeHours = 2;
        private bool _isProgrammaticallySettingKotDateTime;
        private bool _isKotDateTimeManuallySet;
        private bool _hasLoadedKotDateTime;
        private DateTime? _loadedKotDateTime;
        private bool _loadedConsumedRawMaterialStatus;
        private bool _loadedKoTPrintStatus;
        private bool _isLoadingPartyBooking;

        private int _calendarBookingId;
        private DateTime? _calendarBookingStart;
        private DateTime? _calendarBookingEnd;
        decimal originalGrandTotal = 0, originalTotalTax = 0;

        private int PaymentSettleStatusData = 0;
        private bool Fullpaymentflag = false, Advanceflag = false;


        #region side menu
        public int Productcorner = Convert.ToInt32(ConfigurationManager.AppSettings["CornerRadius"]);
        public int Productspace = Convert.ToInt32(ConfigurationManager.AppSettings["Productspace"]);
        public decimal Productheight = Convert.ToDecimal(ConfigurationManager.AppSettings["Productheight"] ?? "1.08");
        public int columncount_offer = Convert.ToInt32(ConfigurationManager.AppSettings["OrderPageProductViewRow_Offer"]);
        int maxColumns = 0;
        int spacing = 0;
        int containerWidth = 0;
        int tileW = 0;
        int tileH = 0;

        int currentPage = 1;
        int pageSize = 0;
        int totalPages = 0;
        private List<DmnGridFields> ProductData;
        private List<DmnGridFields> pagedatadata;
        private List<DmnGridFields> partyMenuAllProducts;
        private bool partyMenuProductsLoaded = false;
        private bool partyMenuCategoriesLoaded = false;
        private int partyMenuColumns = 1;
        private int partyMenuRows = 1;
        private int partyMenuTileWidth = 150;
        private int partyMenuTileHeight = 100;
        private int partyMenuGap = 8;
        public string msgnodata = " No records available";

        public int McategoryID = 0;

        private ToolTip sideMenuToolTip;

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
        public int itotal = 13;
        public int iitemid = 14;
        public int itaxtype = 15;
        public int imrp = 16;
        public int itype = 17;
        public int ibarcode2 = 18;
        public int itaxsettingid = 19;
        public int iunitid = 20;
        public int ipridno = 21;
        public int iprodesno = 22;
        public int istatusno = 23;
        public int iigstamtno = 24;
        public int ivatamtno = 25;
        public int iorderdetailsidno = 26;
        public int isubofferstatus = 27;
        public int ioriginalprice = 28;
        public int isubofferid = 29;
        public int itabletype = 30;
        public int iorderdescno = 31;
        public int ioriginalqty = 32;
        public int iavid = 33;
        public int ivaraiationstatusno = 34;
        public int ipavidno = 35;
        public int iMainProductStatusno = 36;
        public int iAddoncountno = 37;
        public int ividno = 38;
        public int iComboTaxamtno = 39;
        public int iccodeno = 40;
        public int iVariatiionNameno = 41;
        public int iPreparationModeno = 42;
        public int isaledetailsidno = 43;
        public int istocktrackno = 44;
        public int iorgname = 45;
        public int icombotaxamt = 46;
        public int ipretaxamtno = 47;
        public int iSaledisperno = 48;
        public int isalediamtno = 49;
        #endregion
        #endregion

        #endregion
        public FrmPartyBooking()
        {
            InitializeComponent();
            InitializeBlurPopupSupport();
            InitializeKotDateTimeControls();
            lbl_Paymentsettled.Text = "Payment Settlement";
            lblbalapay.Text = "Balance Payable";

            dgv_packenter.DataError += dgv_packenter_DataError;
            dgv_packs_items.DataError += dgv_packs_items_DataError;
            btn_cart.BackgroundImage = Properties.Resources.Party_Cart_Icon;
            btn_cart.BackgroundImageLayout = ImageLayout.Stretch;
            btnprev.Click += btnprev_Click;
            btnnext.Click += btnnext_Click;
            btn_serach.Click += btn_serach_Click;
            Dgv_Selected_Item.CellClick += Dgv_Selected_Item_CellClick;
            Dgv_Selected_Item.CellPainting += Dgv_Selected_Item_CellPainting;
            txtname_search.KeyDown += txtname_search_KeyDown;
            txtitemcode_serach.KeyDown += txtitemcode_serach_KeyDown;
            txtname_search.Enter += PartyMenuSearchTextBox_Enter;
            txtname_search.Leave += PartyMenuSearchTextBox_Leave;
            txtitemcode_serach.Enter += PartyMenuSearchTextBox_Enter;
            txtitemcode_serach.Leave += PartyMenuSearchTextBox_Leave;
            LoadPartyBookingVenueOptions();

            // Recalculate once the form is loaded and whenever it is resized.
            Resize += (sender, args) => ConfigureResponsivePartyButtons();
            Shown += (sender, args) => ConfigureResponsivePartyButtons();

            pnl_moreoptions.Visible = false;
            HideBlurPopup(pnl_moreoptions);
            pnl_moreoptions.BringToFront();
            btn_billpreview.Visible = false;
        }

        private void dgv_packenter_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = false;
        }

        #region Blur Popup Overlay (low-memory, cached while a popup is open)

        private void InitializeBlurPopupSupport()
        {
            // These panels are top-level popup panels in the form's designer.
            // Tracking VisibleChanged keeps the existing open/close methods intact.
            Panel[] popupPanels =
            {
                pnl_foodpacks,
                pnl_pak_detailsitems,
                pnl_eventreg,
                pnl_enter_paks,
                pnl_CustomItems_menu,
                pnl_cardpreview,
                pnlgiftpreview,
                pnlcake,
                pnl_cakeregister,
                pnl_Paymentsettled,
                pnlPaymode,
                pnlSplit,
                pnl_PaymentOptions,
                pnl_menu,
                pnl_bookingcancel,
                pnl_extracharge,
                pnl_moreoptions,
                pnl_venue
            };

            foreach (Panel popup in popupPanels)
            {
                if (popup == null)
                    continue;

                popup.VisibleChanged -= BlurPopupPanel_VisibleChanged;
                popup.VisibleChanged += BlurPopupPanel_VisibleChanged;
            }

            FormClosed += FrmPartyBooking_FormClosed;
            Resize += FrmPartyBooking_BlurOverlayResize;
        }

        private void FrmPartyBooking_BlurOverlayResize(object sender, EventArgs e)
        {
            if (_blurPopupPictureBox != null && !_blurPopupPictureBox.IsDisposed)
                _blurPopupPictureBox.Bounds = ClientRectangle;
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
            if (popup == null || popup.IsDisposed)
                return;

            try
            {
                bool isFirstPopup = _activeBlurPopupPanels.Count == 0;

                if (isFirstPopup)
                {
                    // Capture the normal form without the popup itself.
                    bool wasVisible = popup.Visible;
                    _isUpdatingBlurPopup = true;

                    try
                    {
                        popup.Visible = false;

                        Bitmap nextBlurImage;
                        using (Bitmap original = CapturePartyBookingForm())
                        using (Bitmap small = ResizeBlurPreview(
                            original,
                            Math.Max(1, original.Width / BlurPreviewScale),
                            Math.Max(1, original.Height / BlurPreviewScale)))
                        {
                            nextBlurImage = CreateDarkBlurPreview(small);
                        }

                        EnsureBlurPopupPictureBox();

                        // Replace any image left from a popup-to-popup transition.
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
                    }

                    EnsureBlurPopupPictureBox();
                    _blurPopupPictureBox.Image = _blurPopupImage;
                    _blurPopupPictureBox.Visible = true;
                    _blurPopupPictureBox.BringToFront();
                }

                _activeBlurPopupPanels.Add(popup);

                // All popup panels are direct children of FrmPartyBooking.
                // Keep their original parent/layout; only adjust the Z-order.
                popup.BringToFront();
                PositionBlurOverlayBehindPopup(popup);
            }
            catch (Exception ex)
            {
                _isUpdatingBlurPopup = false;
                clsgen.LogException(
                    "Partybooking page ShowBlurPopup()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }


        private void PositionBlurOverlayBehindPopup(Panel popup)
        {
            if (popup == null ||
                popup.IsDisposed ||
                popup.Parent != this ||
                _blurPopupPictureBox == null ||
                _blurPopupPictureBox.IsDisposed)
                return;

            // Z-order index 0 is frontmost. Keep the blur image directly behind
            // the active popup so it cannot be hidden by tlp_main or other panels.
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

            // Defer disposal until the current UI event completes. This allows
            // one popup to switch to another without briefly removing/rebuilding
            // the overlay (for example, More -> Venue or Payment Options -> Paymode).
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
                Name = "picPartyBookingBlurOverlay",
                Bounds = ClientRectangle,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Black,
                TabStop = false,
                Visible = false
            };

            // Add once and reuse it for the lifetime of the form.
            Controls.Add(_blurPopupPictureBox);
        }

        private Bitmap CapturePartyBookingForm()
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

                // Copy by row so both positive and negative bitmap strides work.
                for (int y = 0; y < height; y++)
                {
                    IntPtr sourceRow = IntPtr.Add(sourceData.Scan0, y * sourceData.Stride);
                    Marshal.Copy(sourceRow, sourcePixels, y * rowBytes, rowBytes);
                }

                // Preserve the image edges instead of leaving transparent/black borders.
                Buffer.BlockCopy(sourcePixels, 0, resultPixels, 0, sourcePixels.Length);

                // Small 3x3 box blur. The image has already been reduced to 1/4 size,
                // so this work is inexpensive and does not block the UI for long.
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

        private void FrmPartyBooking_FormClosed(object sender, FormClosedEventArgs e)
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


        public void SetCalendarBookingStart(DateTime startDateTime)
        {
            SetCalendarBookingInterval(startDateTime, startDateTime);
        }


        public void SetCalendarBookingInterval(DateTime startDateTime, DateTime endDateTime)
        {
            _calendarBookingId = 0;
            _calendarBookingStart = startDateTime;
            _calendarBookingEnd = endDateTime < startDateTime ? startDateTime : endDateTime;
            ApplyCalendarDateTimeInterval();
            UpdateSelectedDateLabel();
        }

        public void LoadBookingFromCalendar(int bookingId)
        {
            if (bookingId <= 0)
                return;

            _calendarBookingId = bookingId;
            LoadBookingById(bookingId);
        }

        private void ApplyCalendarDateTimeInterval()
        {
            if (_calendarBookingStart.HasValue)
            {
                DateTime start = _calendarBookingStart.Value;
                dtpEventDate_From.Value = start.Date;
                dtpEventTime_From.Value = DateTime.Today.Add(start.TimeOfDay);
                SetOptionalDateTimePicker("dtpEventDate_From", start.Date);
                SetOptionalDateTimePicker("dtpEventTime_From", DateTime.Today.Add(start.TimeOfDay));
            }

            if (_calendarBookingEnd.HasValue)
            {
                DateTime end = _calendarBookingEnd.Value;
                SetOptionalDateTimePicker("dtpEventDate_To", end.Date);
                SetOptionalDateTimePicker("dtpEventTime_To", DateTime.Today.Add(end.TimeOfDay));
            }
        }

        private void UpdateSelectedDateLabel()
        {
            if (lbl_selectdate == null)
                return;

            DateTime fromDateTime = dtpEventDate_From.Value.Date.Add(dtpEventTime_From.Value.TimeOfDay);
            DateTime? toDate = GetOptionalDateTimePickerValue("dtpEventDate_To");
            DateTime? toTime = GetOptionalDateTimePickerValue("dtpEventTime_To");
            DateTime toDateTime = toDate.HasValue
                ? toDate.Value.Date.Add((toTime ?? dtpEventTime_From.Value).TimeOfDay)
                : fromDateTime;

            lbl_selectdate.AutoSize = false;
            lbl_selectdate.Dock = DockStyle.Fill;
            lbl_selectdate.AutoEllipsis = true;
            lbl_selectdate.TextAlign = ContentAlignment.MiddleLeft;
            lbl_selectdate.Text = string.Format(
                "{0:dd MMM hh:mm tt} - {1:dd MMM hh:mm tt}",
                fromDateTime,
                toDateTime);
        }

        private void SetOptionalDateTimePicker(string controlName, DateTime value)
        {
            Control[] matches = Controls.Find(controlName, true);
            DateTimePicker picker = matches.FirstOrDefault() as DateTimePicker;
            if (picker != null)
                picker.Value = value;
        }

        private DateTime? GetOptionalDateTimePickerValue(string controlName)
        {
            Control[] matches = Controls.Find(controlName, true);
            DateTimePicker picker = matches.FirstOrDefault() as DateTimePicker;
            return picker == null ? (DateTime?)null : picker.Value;
        }

        private void InitializeKotDateTimeControls()
        {
            // KOT date and time must be editable by the booking staff.
            dtp_kotdate.Enabled = true;
            dtp_kottime.Enabled = true;

            dtpEventDate_From.ValueChanged += BookingDateTimePicker_ValueChanged;
            dtpEventTime_From.ValueChanged += BookingDateTimePicker_ValueChanged;
            dtp_kotdate.ValueChanged += KotDateTimePicker_ValueChanged;
            dtp_kottime.ValueChanged += KotDateTimePicker_ValueChanged;

            UpdateDefaultKotDateTime(GetPartyBookingStartDateTime());
        }

        private DateTime GetPartyBookingStartDateTime()
        {
            return dtpEventDate_From.Value.Date.Add(dtpEventTime_From.Value.TimeOfDay);
        }

        private DateTime GetSelectedKotDateTime()
        {
            return dtp_kotdate.Value.Date.Add(dtp_kottime.Value.TimeOfDay);
        }

        private void BookingDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (_isLoadingPartyBooking ||
                _isProgrammaticallySettingKotDateTime ||
                _isKotDateTimeManuallySet ||
                _hasLoadedKotDateTime)
                return;

            UpdateDefaultKotDateTime(GetPartyBookingStartDateTime());
        }

        private void KotDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (_isLoadingPartyBooking || _isProgrammaticallySettingKotDateTime)
                return;

            _isKotDateTimeManuallySet = true;
        }

        private void SetKotDateTimePickerValue(DateTime value)
        {
            _isProgrammaticallySettingKotDateTime = true;
            try
            {
                dtp_kotdate.Value = value.Date;
                dtp_kottime.Value = DateTime.Today.Add(value.TimeOfDay);
            }
            finally
            {
                _isProgrammaticallySettingKotDateTime = false;
            }
        }

        private void UpdateDefaultKotDateTime(DateTime bookingStart)
        {
            // A KOT time cannot be generated for a booking that has already passed.
            if (bookingStart <= DateTime.Now)
                return;

            DateTime automaticKotTime = bookingStart.AddHours(-DefaultKotLeadTimeHours);
            DateTime earliestAllowedTime = DateTime.Now.AddMinutes(1);

            // For bookings made less than two hours ahead, use the earliest
            // practical future time instead of generating a KOT time in the past.
            if (automaticKotTime < earliestAllowedTime)
                automaticKotTime = earliestAllowedTime <= bookingStart
                    ? earliestAllowedTime
                    : bookingStart;

            SetKotDateTimePickerValue(automaticKotTime);
        }

        private bool TryGetKotDateTimeForSave(DateTime bookingStart, out DateTime? kotDateTime)
        {
            kotDateTime = null;

            // Keep a previously saved KOT time when the staff member has not
            // changed it. This also allows old bookings with a historical KOT time
            // to be edited without treating the existing value as a new selection.
            if (_hasLoadedKotDateTime && !_isKotDateTimeManuallySet && _loadedKotDateTime.HasValue)
            {
                if (_loadedKotDateTime.Value > bookingStart)
                {
                    CMessageBox.Show("The KOT date and time cannot be later than the party booking time. Please update the KOT time.");
                    dtp_kotdate.Focus();
                    return false;
                }

                kotDateTime = _loadedKotDateTime.Value;
                return true;
            }

            // Automatically assign a KOT time two hours before the booking when
            // staff have not selected one manually.
            if (!_isKotDateTimeManuallySet && !_hasLoadedKotDateTime)
            {
                if (bookingStart <= DateTime.Now)
                {
                    // Keep a legacy NULL for an already-past booking. A new booking
                    // must have a future booking time to generate a valid KOT time.
                    if (GetInt(txtpartybookingid) > 0)
                        return true;

                    CMessageBox.Show("The party booking time has already passed. Please select a future booking time so the KOT time can be set automatically.");
                    dtpEventDate_From.Focus();
                    return false;
                }

                UpdateDefaultKotDateTime(bookingStart);
            }

            DateTime selectedKotDateTime = GetSelectedKotDateTime();

            if (selectedKotDateTime < DateTime.Now)
            {
                CMessageBox.Show("KOT date and time cannot be in the past. Please select the current or a future date and time.");
                dtp_kotdate.Focus();
                return false;
            }

            if (selectedKotDateTime > bookingStart)
            {
                CMessageBox.Show("KOT date and time cannot be later than the party booking time. Please choose an earlier KOT time.");
                dtp_kotdate.Focus();
                return false;
            }

            kotDateTime = selectedKotDateTime;
            return true;
        }

        private void FrmPartyBooking_Load(object sender, EventArgs e)
        {
            try
            {
                lblNetAmt.ForeColor = Color.DarkBlue;
                lbladvamtdata.ForeColor = Color.SeaGreen;
                txtRcvAmt.ForeColor = Color.Black;
                lblBalAmt.ForeColor = Color.IndianRed;

                lblnetcaption.ForeColor = Color.DimGray;
                lbladvamt.ForeColor = Color.DimGray;
                lblreciamt.ForeColor = Color.DimGray;
                lblbalapay.ForeColor = Color.DimGray;

                lblpackname.ForeColor = Color.DimGray;

                ApplyPartyMenuVisualStyles();
                SetPartyMenuSearchPlaceholders();
                ConfigurePartyBookingHsnColumn();
                LoadPartyBookingVenueOptions();

                Taxfill();

                ToggleFirstRow(0);

                TogglePayCancel(0);

                textb_shape();

                if (FrmLogin.GameBranchId > 0)
                {
                    btngift.Visible = true;
                    btncard.Visible = true;
                }
                else
                {
                    btngift.Visible = false;
                    btncard.Visible = false;
                }
                LoadEventItems();

                if (_calendarBookingId > 0)
                {
                    LoadBookingById(_calendarBookingId);
                }
                else if (_calendarBookingStart.HasValue)
                {
                    ApplyCalendarDateTimeInterval();
                }

                UpdateSelectedDateLabel();

                ConfigureResponsivePartyButtons();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  FrmPartyBooking_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        #region Events And common Fuction 
        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        private void btnfoodpack_Click(object sender, EventArgs e)
        {
            try
            {
                if (pnl_foodpacks.Visible)
                {
                    pnl_foodpacks.Visible = false;
                    HideBlurPopup(pnl_foodpacks);
                    return;
                }
                pnl_foodpacks.Width = (int)(this.ClientSize.Width * 0.7);
                pnl_foodpacks.Height = (int)(this.ClientSize.Height * 0.7);

                pnl_foodpacks.Left = (this.ClientSize.Width - pnl_foodpacks.Width) / 2;
                pnl_foodpacks.Top = (this.ClientSize.Height - pnl_foodpacks.Height) / 2;

                pnl_foodpacks.Visible = true;
                pnl_foodpacks.Visible = true;ShowBlurPopup(pnl_foodpacks);
                pnl_foodpacks.BringToFront();

                LoadPartyPacks();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking btnfoodpack_Click()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking)
                );
            }
        }
        private void btn_pack_close_Click(object sender, EventArgs e)
        {
            pnl_foodpacks.Visible = false;
            HideBlurPopup(pnl_foodpacks);
        }
        private void btn_orderdesc_submit_Click(object sender, EventArgs e)
        {
            pnl_foodpacks.Visible = false;
            HideBlurPopup(pnl_foodpacks);
        }
        public void Loadprice(int pkid)
        {
            try
            {
                BllSearch<DmnPartyPack> bll = new BllSearch<DmnPartyPack>();

                DmnSearch search = new DmnSearch();
                search.Select = @"
                            PartyPackId,
                            PackName,
                            AdultPrice,
                            ChildPrice,
                            FixedPrice,
                            FixedPriceStatus,
                            AdultCount,
                            ChildCount,
                            Tax,TaxSettingsId,HSNCode,
                            (SELECT COUNT(1) 
                             FROM tblPartyPackDetails d 
                             WHERE d.PartyPackId = tblPartyPackMaster.PartyPackId 
                             AND d.Status = 1) AS DetailsCount,MinimumAdvanceAmount";
                search.Table = "tblPartyPackMaster";
                search.WhereNdGroupBy = "WHERE Status = 1 and PartyPackId =" + pkid;
                search.OrderBy = "ORDER BY PackName";
                var list = bll.GetSearchData("prdSearch", search);
                if (list.Count > 0)
                {
                    _fixedPriceStatus = list[0].FixedPriceStatus;
                    _fixedPackPrice = list[0].FixedPrice;
                    txtChildPrice.Text = Convert.ToString(list[0].ChildPrice);
                    txtAdultPrice.Text = Convert.ToString(list[0].AdultPrice);

                    // Use the guest counts configured for this package.
                    txtAdults.Text = Math.Max(0, list[0].AdultCount).ToString();
                    txtChild.Text = Math.Max(0, list[0].ChildCount).ToString();
                    // Fixed-price packages use the configured guest counts only for display.
                    // The guest controls must not accept edits or open the numeric keypad.
                    txtAdults.Enabled = !_fixedPriceStatus;
                    txtChild.Enabled = !_fixedPriceStatus;
                    txtAdults.ReadOnly = _fixedPriceStatus;
                    txtChild.ReadOnly = _fixedPriceStatus;
                    txtAdults.TabStop = !_fixedPriceStatus;
                    txtChild.TabStop = !_fixedPriceStatus;

                    txtTaxSettingId.Text = Convert.ToString(list[0].TaxSettingsId);
                    txttax.Text = Convert.ToString(list[0].Tax);
                    _partyPackHsnCode = Convert.ToString(list[0].HSNCode ?? string.Empty);
                    MinimumAdvance = Convert.ToDecimal(list[0].MinimumAdvanceAmount);
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  Loadprice()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void LoadPartyPacks()
        {
            try
            {
                flp_show_pack_resmenu.Controls.Clear();

                BllSearch<DmnPartyPack> bll = new BllSearch<DmnPartyPack>();

                DmnSearch search = new DmnSearch();
                search.Select = @"
                            PartyPackId,
                            PackName,
                            AdultPrice,
                            ChildPrice,
                            FixedPrice,
                            FixedPriceStatus,
                            AdultCount,
                            ChildCount,
                            Tax,TaxSettingsId,HSNCode,
                            (SELECT COUNT(1) 
                             FROM tblPartyPackDetails d 
                             WHERE d.PartyPackId = tblPartyPackMaster.PartyPackId 
                             AND d.Status = 1) AS DetailsCount";
                search.Table = "tblPartyPackMaster";
                search.WhereNdGroupBy = "WHERE Status = 1 ";
                search.OrderBy = "ORDER BY PackName";

                var list = bll.GetSearchData("prdSearch", search);

                if (list.Count > 0)
                {
                    txtChildPrice.Text = Convert.ToString(list[0].ChildPrice);
                    txtAdultPrice.Text = Convert.ToString(list[0].AdultPrice);

                    txttax.Text = Convert.ToString(list[0].Tax);
                    txtTaxSettingId.Text = Convert.ToString(list[0].TaxSettingsId);

                    SetupGuestControls();

                    foreach (var item in list)
                    {
                        // 🔷 CARD
                        RoundedPanel card = new RoundedPanel();
                        card.Width = 260;
                        card.Height = 140;
                        card.Margin = new Padding(15);
                        card.BackColor = Color.FromArgb(245, 247, 250); // light grey
                        card.Tag = item.PartyPackId;
                        card.Cursor = Cursors.Hand;

                        // 🔷 SHADOW EFFECT (fake shadow panel)
                        Panel shadow = new Panel();
                        shadow.BackColor = Color.FromArgb(30, 0, 0, 0);
                        shadow.Size = card.Size;
                        shadow.Location = new Point(card.Left + 3, card.Top + 3);

                        // 🔷 HOVER EFFECT
                        card.MouseEnter += (s, e) =>
                        {
                            card.BackColor = Color.FromArgb(230, 240, 255); // soft blue hover
                            card.Location = new Point(card.Location.X, card.Location.Y - 3);
                        };

                        card.MouseLeave += (s, e) =>
                        {
                            card.BackColor = Color.FromArgb(245, 247, 250);
                            card.Location = new Point(card.Location.X, card.Location.Y + 3);
                        };

                        // 🔷 PACK NAME
                        Label lblName = new Label();
                        lblName.Text = item.PackName;
                        lblName.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                        lblName.Width = card.Width - 20;
                        lblName.Location = new Point(10, 10);

                        // 🔷 PRICE
                        Label lblPrice = new Label();
                        lblPrice.Font = new Font("Segoe UI", 9);
                        lblPrice.ForeColor = Color.SeaGreen;
                        lblPrice.Width = card.Width - 20;
                        lblPrice.Location = new Point(10, 40);

                        if (item.FixedPriceStatus)
                            lblPrice.Text = $"Fixed Package ₹{item.FixedPrice}";
                        else if (item.AdultPrice > 0 && item.ChildPrice > 0)
                            lblPrice.Text = $"Adult ₹{item.AdultPrice} | Child ₹{item.ChildPrice}";
                        else if (item.AdultPrice > 0)
                            lblPrice.Text = $"₹{item.AdultPrice}";
                        else
                            lblPrice.Text = $"₹{item.ChildPrice}";

                        // 🔷 BIG SELECT BUTTON
                        Button btnSelect = new Button();
                        btnSelect.Text = "SELECT PACK";
                        btnSelect.Width = card.Width - 20;
                        btnSelect.Height = 35;
                        btnSelect.Location = new Point(10, card.Height - 45);
                        btnSelect.BackColor = Color.FromArgb(46, 204, 113);
                        btnSelect.ForeColor = Color.White;
                        btnSelect.FlatStyle = FlatStyle.Flat;
                        btnSelect.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                        btnSelect.Tag = item.PartyPackId;

                        btnSelect.Click += (s, e) =>
                        {
                            //ClearAll();

                            pnl_foodpacks.Visible = false;
                            HideBlurPopup(pnl_foodpacks);

                            lbltotalguest.Visible = true;
                            txttotalguest.Visible = true;

                            //tlp_adult_child_label.Visible = true;
                            //tlp_adult_child_text.Visible = true;

                            lblpackname.Visible = true;
                            lblpacklabel.Visible = true;

                            int packId = Convert.ToInt32(((Button)s).Tag);
                            Partypackid = packId;

                            Loadprice(packId);

                            if (item.DetailsCount > 0)
                            {
                                OpenPackEntryPanel();
                                LoadPartyPackDetails(packId);
                                LoadPackRelatedItems(packId);
                            }
                            else
                            {
                                lblpackname.Visible = true;
                                lblpacklabel.Visible = true;
                                lblpackname.Text = SetPackName(packId);
                                OpenPackEntryPanel();
                            }

                            SetupGuestControls();
                            textb_shape();

                        };

                        if (item.DetailsCount > 0)
                        {
                            Label lblView = new Label();
                            lblView.Text = "View Details →";
                            lblView.ForeColor = Color.RoyalBlue;
                            lblView.Font = new Font("Segoe UI", 8, FontStyle.Underline);
                            lblView.AutoSize = true;
                            lblView.Location = new Point(10, card.Height - 65);
                            lblView.Cursor = Cursors.Hand;
                            lblView.Tag = item.PartyPackId;

                            lblView.Click += (s, e) =>
                            {
                                int packId = Convert.ToInt32(((Label)s).Tag);

                                pnl_pak_detailsitems.Visible = true;
                                pnl_pak_detailsitems.Visible = true;ShowBlurPopup(pnl_pak_detailsitems);
                                pnl_pak_detailsitems.BringToFront();

                                pnl_pak_detailsitems.Width = (int)(this.Width * 0.5);
                                pnl_pak_detailsitems.Height = (int)(this.Height * 0.5);

                                pnl_pak_detailsitems.Left = (this.Width - pnl_pak_detailsitems.Width) / 2;
                                pnl_pak_detailsitems.Top = (this.Height - pnl_pak_detailsitems.Height) / 2;

                                LoadPackItems(packId);
                            };

                            card.Controls.Add(lblView);
                        }

                        card.Click += (s, e) =>
                        {
                            int packId = Convert.ToInt32(card.Tag);
                        };

                        card.Controls.Add(lblName);
                        card.Controls.Add(lblPrice);
                        card.Controls.Add(btnSelect);

                        flp_show_pack_resmenu.Controls.Add(card);
                    }
                }
                else
                {
                    CMessageBox.Show("No packages found ", "Package Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  LoadPartyPacks()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void SetupGuestControls()
        {
            try
            {
                decimal childPrice = 0m;
                decimal adultPrice = 0m;

                decimal.TryParse(txtChildPrice.Text, out childPrice);
                decimal.TryParse(txtAdultPrice.Text, out adultPrice);

                bool showChild = childPrice > 0m;
                bool showAdult = adultPrice > 0m;

                if (_fixedPriceStatus)
                {
                    showChild = true;
                    showAdult = true;
                }

                // If both prices are zero, show Adult controls only.
                if (!showChild && !showAdult)
                {
                    showAdult = true;
                    showChild = false;
                }


                txtChild.Visible = showChild;
                lblnochild.Visible = showChild;

                txtAdults.Visible = showAdult;
                lbladult.Visible = showAdult;


                lbltotalguest.Visible = true;
                txttotalguest.Visible = true;


                tlp_adult_child_label.SuspendLayout();
                tlp_adult_child_text.SuspendLayout();

                try
                {
                    tlp_adult_child_label.Dock = DockStyle.Fill;
                    tlp_adult_child_text.Dock = DockStyle.Fill;

                    tlp_adult_child_label.ColumnCount = 2;
                    tlp_adult_child_text.ColumnCount = 2;

 
                    while (tlp_adult_child_label.ColumnStyles.Count < 2)
                    {
                        tlp_adult_child_label.ColumnStyles.Add(
                            new ColumnStyle(SizeType.Percent, 50F));
                    }

                    while (tlp_adult_child_text.ColumnStyles.Count < 2)
                    {
                        tlp_adult_child_text.ColumnStyles.Add(
                            new ColumnStyle(SizeType.Percent, 50F));
                    }

                    if (showAdult && showChild)
                    {

                        for (int i = 0; i < 2; i++)
                        {
                            tlp_adult_child_label.ColumnStyles[i].SizeType =
                                SizeType.Percent;
                            tlp_adult_child_label.ColumnStyles[i].Width = 50F;

                            tlp_adult_child_text.ColumnStyles[i].SizeType =
                                SizeType.Percent;
                            tlp_adult_child_text.ColumnStyles[i].Width = 50F;
                        }
                    }
                    else
                    {

                        int visibleColumn = showAdult ? 0 : 1;
                        int hiddenColumn = visibleColumn == 0 ? 1 : 0;

                        tlp_adult_child_label.ColumnStyles[visibleColumn].SizeType =
                            SizeType.Percent;
                        tlp_adult_child_label.ColumnStyles[visibleColumn].Width = 100F;

                        tlp_adult_child_text.ColumnStyles[visibleColumn].SizeType =
                            SizeType.Percent;
                        tlp_adult_child_text.ColumnStyles[visibleColumn].Width = 100F;


                        tlp_adult_child_label.ColumnStyles[hiddenColumn].SizeType =
                            SizeType.Absolute;
                        tlp_adult_child_label.ColumnStyles[hiddenColumn].Width = 0F;

                        tlp_adult_child_text.ColumnStyles[hiddenColumn].SizeType =
                            SizeType.Absolute;
                        tlp_adult_child_text.ColumnStyles[hiddenColumn].Width = 0F;
                    }

                    lbladult.AutoSize = false;
                    lblnochild.AutoSize = false;

                    lbladult.TextAlign = ContentAlignment.MiddleLeft;
                    lblnochild.TextAlign = ContentAlignment.MiddleLeft;

                    lbladult.Dock = DockStyle.Fill;
                    lblnochild.Dock = DockStyle.Fill;

                    txtAdults.Dock = DockStyle.Fill;
                    txtChild.Dock = DockStyle.Fill;

                    lbladult.Margin = new Padding(3, 0, 3, 0);
                    lblnochild.Margin = new Padding(3, 0, 3, 0);

                    txtAdults.Margin = new Padding(3);
                    txtChild.Margin = new Padding(3);
                }
                finally
                {
                    tlp_adult_child_label.ResumeLayout(true);
                    tlp_adult_child_text.ResumeLayout(true);
                }

                textb_shape();
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Partybooking page SetupGuestControls()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void LoadPartyPackDetails(int packid)
        {
            try
            {
                dgv_packenter.Rows.Clear();

                int sl = 1;

                BllSearch<DmnPartyPackDetails> bll = new BllSearch<DmnPartyPackDetails>();
                DmnSearch search = new DmnSearch();
                search.Select = "PartyPackDetailsId, Item, Quantity, MenuCategoryId";
                search.Table = "tblPartyPackDetails";
                search.WhereNdGroupBy = $"WHERE PartyPackId = {packid} AND Status = 1 and PackItemType =" + Convert.ToInt32(PackItemType.FoodItem);

                var result = bll.GetSearchData("prdSearch", search);

                if (result != null)
                {
                    lblpackname.Visible = true;
                    lblpacklabel.Visible = true;
                    lblpackname.Text = SetPackName(packid);

                    foreach (var item in result)
                    {
                        int rowIndex = dgv_packenter.Rows.Add();
                        var row = dgv_packenter.Rows[rowIndex];

                        row.Cells[selecteditemslno].Value = sl++;
                        row.Cells[selecteditemitemno].Value = item.Item;

                        if (item.MenuCategoryId != null && Convert.ToInt32(item.MenuCategoryId) > 0)
                            row.Cells[selecteditemcategmenuno].Value = Convert.ToInt64(item.MenuCategoryId);

                        row.Cells[selecteditemqtyno].Value = item.Quantity;
                        row.Cells[selecteditemppcdiidno].Value = 0;
                        row.Cells[selecteditemdeafultno].Value = 1;
                        row.ReadOnly = true;
                    }
                }

                int newRowIndex = dgv_packenter.Rows.Add();
                dgv_packenter.Rows[newRowIndex].Cells[selecteditemslno].Value = sl;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  LoadPartyPackDetails()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private string SetPackName(int packId)
        {
            try
            {

                BllSearch<DmnPartyPack> bll = new BllSearch<DmnPartyPack>();

                DmnSearch search = new DmnSearch();
                search.Select = "PackName";
                search.Table = "tblPartyPackMaster";
                search.WhereNdGroupBy = $"WHERE PartyPackId = {packId} AND Status = 1";

                var result = bll.GetSearchData("prdSearch", search)
                                .FirstOrDefault();

                if (result != null)
                    return result.PackName;

                return "";
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  LoadPartyPackDetails()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
                return "";
            }
        }
        private void LoadMenuCategory(IEnumerable<DmnPartyPackDetails> packItems = null)
        {
            try
            {
                // Keep all categories currently referenced by this saved package in the
                // ComboBox list, even if a category was later disabled or reclassified.
                List<long> usedCategoryIds = (packItems ?? Enumerable.Empty<DmnPartyPackDetails>())
                    .Where(x => x.MenuCategoryId > 0)
                    .Select(x => Convert.ToInt64(x.MenuCategoryId))
                    .Distinct()
                    .ToList();

                string whereClause = "WHERE (ISNULL(Status, 1) = 1 AND CategoryType = 5)";
                if (usedCategoryIds.Count > 0)
                {
                    whereClause += " OR Fcid IN (" +
                        string.Join(",", usedCategoryIds) + ")";
                }

                BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                DmnSearch objSearch = new DmnSearch
                {
                    Select = "Fcid, CategoryName",
                    Table = "tblCategory_FTI",
                    WhereNdGroupBy = whereClause,
                    OrderBy = "ORDER BY CategoryName"
                };

                categoryList = bllSearch.GetSearchData("prdSearch", objSearch) ??
                    new List<DmnCategory>();

                // Preserve legacy IDs if a referenced category record no longer exists.
                HashSet<long> loadedIds = new HashSet<long>(
                    categoryList.Select(x => x.Fcid));
                foreach (long categoryId in usedCategoryIds)
                {
                    if (!loadedIds.Contains(categoryId))
                    {
                        categoryList.Add(new DmnCategory
                        {
                            Fcid = categoryId,
                            CategoryName = "Unavailable category (" + categoryId + ")"
                        });
                    }
                }

                DataGridViewComboBoxColumn cmb =
                    (DataGridViewComboBoxColumn)dgv_packs_items.Columns[showonlycategmenuno];

                cmb.DataSource = null;
                cmb.DisplayMember = "CategoryName";
                cmb.ValueMember = "Fcid";
                cmb.DataSource = categoryList;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page LoadMenuCategory()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void dgv_packs_items_DataError(
            object sender,
            DataGridViewDataErrorEventArgs e)
        {
            // A stale or blank category must never stop the View Details popup.
            e.ThrowException = false;
            if (e.Exception != null)
            {
                clsgen.LogException("Partybooking page dgv_packs_items_DataError()",
                    e.Exception, FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void LoadMenuCategory_cutom()
        {
            try
            {
                BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                DmnSearch objSearch = new DmnSearch();

                objSearch.Select = "Fcid, CategoryName";
                objSearch.Table = "tblCategory_FTI";
                objSearch.WhereNdGroupBy = "WHERE Status = 1 AND CategoryType = 5";
                objSearch.OrderBy = "ORDER BY CategoryName";

                categoryList = bllSearch.GetSearchData("prdSearch", objSearch);

                DataGridViewComboBoxColumn cmb = (DataGridViewComboBoxColumn)dgv_cus_menu_items.Columns[customitemcategmenuno];

                cmb.DataSource = categoryList;
                cmb.DisplayMember = "CategoryName";
                cmb.ValueMember = "Fcid";
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  LoadMenuCategory_cutom()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void LoadPackItems(int packId)
        {
            try
            {
                dgv_packs_items.Rows.Clear();
                flp_pakdet_item_menu.Controls.Clear();

                BllSearch<DmnPartyPackDetails> bll = new BllSearch<DmnPartyPackDetails>();

                DmnSearch search = new DmnSearch();
                search.Select = "PartyPackDetailsId, Item, Quantity, MenuCategoryId";
                search.Table = "tblPartyPackDetails";
                search.WhereNdGroupBy = "WHERE PartyPackId = " + packId + " AND Status = 1 and PackItemType =" + Convert.ToInt32(PackItemType.FoodItem);

                packDetailsList = bll.GetSearchData("prdSearch", search) ??
                    new List<DmnPartyPackDetails>();
                LoadMenuCategory(packDetailsList);

                int sl = 1;
                foreach (var item in packDetailsList)
                {
                    int rowIndex = dgv_packs_items.Rows.Add();
                    var row = dgv_packs_items.Rows[rowIndex];

                    row.Cells[showonlyslno].Value = sl++;
                    row.Cells[showonlyitemno].Value = item.Item;

                    if (item.MenuCategoryId > 0)
                    {
                        row.Cells[showonlycategmenuno].Value =
                            Convert.ToInt64(item.MenuCategoryId);
                    }
                    else
                    {
                        // A null value is the valid blank selection for a ComboBox cell.
                        row.Cells[showonlycategmenuno].Value = null;
                    }
                    row.Cells[showonlyqtyno].Value = item.Quantity;
                }

                LoadUsedCategories();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  LoadPackItems()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void LoadUsedCategories()
        {
            try
            {
                flp_pakdet_item_menu.Controls.Clear();

                var usedCategoryIds = (packDetailsList ?? new List<DmnPartyPackDetails>())
                    .Where(x => x.MenuCategoryId > 0)
                    .Select(x => Convert.ToInt64(x.MenuCategoryId))
                    .Distinct()
                    .ToList();

                foreach (long categoryId in usedCategoryIds)
                {
                    DmnCategory cat = categoryList.FirstOrDefault(x => x.Fcid == categoryId);
                    if (cat == null) continue;

                    Button btn = new Button();
                    btn.Text = cat.CategoryName;
                    btn.Width = 120;
                    btn.Height = 35;
                    btn.Tag = cat.Fcid;
                    btn.BackColor = Color.LightGray;
                    btn.FlatStyle = FlatStyle.Flat;

                    btn.Click += CategoryFilter_Click;

                    flp_pakdet_item_menu.Controls.Add(btn);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  LoadUsedCategories()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void CategoryFilter_Click(object sender, EventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                if (btn == null) return;

                int categoryId = Convert.ToInt32(btn.Tag);

                dgv_packs_items.Rows.Clear();

                var filtered = packDetailsList
                    .Where(x => x.MenuCategoryId == categoryId)
                    .ToList();

                int sl = 1;

                LoadMenuCategory(packDetailsList);

                foreach (var item in filtered)
                {
                    int rowIndex = dgv_packs_items.Rows.Add();
                    var row = dgv_packs_items.Rows[rowIndex];

                    row.Cells[showonlyslno].Value = sl++;
                    row.Cells[showonlyitemno].Value = item.Item;
                    row.Cells[showonlycategmenuno].Value = item.MenuCategoryId > 0
                        ? (object)Convert.ToInt64(item.MenuCategoryId)
                        : null;
                    row.Cells[showonlyqtyno].Value = item.Quantity;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  CategoryFilter_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        public class RoundedPanel : Panel
        {
            public int CornerRadius { get; set; } = 15;

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                int r = CornerRadius;

                path.AddArc(0, 0, r, r, 180, 90);
                path.AddArc(this.Width - r, 0, r, r, 270, 90);
                path.AddArc(this.Width - r, this.Height - r, r, r, 0, 90);
                path.AddArc(0, this.Height - r, r, r, 90, 90);
                path.CloseAllFigures();

                this.Region = new Region(path);
            }
        }
    
        private void FrmPartyBooking_Resize(object sender, EventArgs e)
        {

            if (pnl_foodpacks.Visible)
            {
                pnl_foodpacks.Width = (int)(this.ClientSize.Width * 0.8);
                pnl_foodpacks.Height = (int)(this.ClientSize.Height * 0.8);

                pnl_foodpacks.Left = (this.ClientSize.Width - pnl_foodpacks.Width) / 2;
                pnl_foodpacks.Top = (this.ClientSize.Height - pnl_foodpacks.Height) / 2;
            }

            UIHelper.SetRoundCorner(tlp_textboxes, 15);
            UIHelper.SetRoundCorner(tlp_reg_contrl, 15);
            UIHelper.SetRoundCorner(tlp_address, 15);
            UIHelper.SetRoundCorner(tlp_navbar, 15);
            UIHelper.SetRoundCorner(tlpTop, 15);
            UIHelper.SetRoundCorner(tlp_dis_remarks, 15);
            UIHelper.SetRoundCorner(tlp_function_btn, 15);

        }
        private void LoadEventItems()
        {
            try
            {
                BllSearch<DmnEventCommonItems> bllSearch = new BllSearch<DmnEventCommonItems>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "EventItemId, ItemName";
                objSearch.Table = "tblEventCommonItems";
                objSearch.WhereNdGroupBy = "WHERE Status = 1 AND EventCategoryType = 1 AND BranchId = " + FrmDashboard.BranchId;
                objSearch.OrderBy = "ORDER BY ItemName";
                var eventList = bllSearch.GetSearchData("prdSearch", objSearch);
                cmbevent.DataSource = eventList;
                cmbevent.DisplayMember = "ItemName";
                cmbevent.ValueMember = "EventItemId";
                cmbevent.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  LoadEventItems()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void btncls_eventreg_Click(object sender, EventArgs e)
        {
            pnl_eventreg.Visible = false;
            HideBlurPopup(pnl_eventreg);
        }
        private void btnevnetype_refresh_Click(object sender, EventArgs e)
        {
            txtItemName.Clear();
            txtDescription.Clear();

        }
        private void btneventreg_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_eventreg.Width = this.Width * 40 / 100;
                pnl_eventreg.Height = this.Height * 25 / 100;

                pnl_eventreg.Left = (this.Width - pnl_eventreg.Width) / 2;
                pnl_eventreg.Top = (this.Height - pnl_eventreg.Height) / 2;

                pnl_eventreg.Visible = true;
                pnl_eventreg.Visible = true;ShowBlurPopup(pnl_eventreg);
                pnl_eventreg.BringToFront();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  btneventreg_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private bool ValidateEvent()
        {
            if (string.IsNullOrWhiteSpace(txtItemName.Text))
            {
                CMessageBox.Show("Enter Item Name");
                txtItemName.Focus();
                return false;
            }

            //if (string.IsNullOrWhiteSpace(txtDescription.Text))
            //{
            //    CMessageBox.Show("Enter Description");
            //    txtDescription.Focus();
            //    return false;
            //}

            return true;
        }
        private void btn_del_event_Click(object sender, EventArgs e)
        {
            try
            {
                if (btn_del_event.Tag == null)
                {
                    CMessageBox.Show("Please search and select item first");
                    return;
                }

                DialogResult result = CMessageBox.Show(
                    "Are you sure you want to delete?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes)
                    return;

                DmnEventCommonItems dmn = new DmnEventCommonItems();
                dmn.EventItemId = Convert.ToInt32(btn_del_event.Tag);
                dmn.CreatedBy = FrmDashboard.UserId;

                BllEventCommonItems bll = new BllEventCommonItems();
                bll.DeleteEventItem("prdDelete_EventCommonItems", dmn);

                CMessageBox.Show("Deleted Successfully");

                txtItemName.Clear();
                txtDescription.Clear();
                btn_del_event.Visible = false;
                btn_del_event.Tag = null;

                LoadEventItems();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  btn_del_event_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void btnserach_event_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                int returnValue = 0;

                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();

                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "ID,Item Name,Type";
                _objSearch.Select = "EventItemId as Id, ItemName as Field1, EventCategoryType as Field2";
                _objSearch.Table = "tblEventCommonItems";
                _objSearch.WhereNdGroupBy = "WHERE Status = 1 AND BranchId = " + FrmDashboard.BranchId;
                _objSearch.OrderBy = "ORDER BY ItemName";
                var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);
                if (gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records Found");
                    return;
                }

                using (var search = new FrmSearch<DmnGridFields>(
                    _objSearch,
                    SearchMode.FirstCharacter,
                    this.Width - 150,
                    this.Height - 150,
                    this.Location.Y + 50,
                    this.Location.X + 20,
                    dtColumnAlign))
                {
                    var dialogResult = search.ShowDialog();

                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToInt32(search._retValue);
                    }
                }
                if (returnValue > 0)
                {
                    FillEventData(returnValue);
                    btn_del_event.Visible = true;
                    btn_del_event.Tag = returnValue;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  btnserach_event_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void FillEventData(int id)
        {
            try
            {
                BllSearch<DmnEventCommonItems> bll = new BllSearch<DmnEventCommonItems>();

                DmnSearch search = new DmnSearch();

                search.Select = "EventItemId, ItemName, Description, EventCategoryType";
                search.Table = "tblEventCommonItems";
                search.WhereNdGroupBy = "WHERE EventItemId = " + id;

                var data = bll.GetSearchData("prdSearch", search);

                if (data.Count > 0)
                {
                    var item = data[0];
                    txtItemName.Text = item.ItemName;
                    txtDescription.Text = item.Description;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  FillEventData()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void btn_pak_details_cls_Click(object sender, EventArgs e)
        {
            pnl_pak_detailsitems.Visible = false;
            HideBlurPopup(pnl_pak_detailsitems);
        }
        private void btn_enter_paks_cls_Click(object sender, EventArgs e)
        {
            pnl_enter_paks.Visible = false;
            HideBlurPopup(pnl_enter_paks);
        }
        private void btnaddpack_main_Click(object sender, EventArgs e)
        {
            try
            {
                dgv_product.Rows.Clear();
                int sl = 1;

                foreach (DataGridViewRow sourceRow in dgv_packenter.Rows)
                {
                    if (sourceRow.IsNewRow) continue;
                    if (sourceRow.Cells[selecteditemitemno].Value == null) continue;

                    int rowIndex = dgv_product.Rows.Add();
                    DataGridViewRow targetRow = dgv_product.Rows[rowIndex];

                    targetRow.Cells[pslno].Value = sl++;
                    targetRow.Cells[pitemno].Value = sourceRow.Cells[selecteditemitemno].Value;

                    if (sourceRow.Cells[selecteditemcategmenuno].Value != null)
                    {
                        string categoryName = GetCategoryName(Convert.ToInt32(sourceRow.Cells[selecteditemcategmenuno].Value));
                        targetRow.Cells[pcategmenuno].Value = categoryName;
                    }
                    else
                    {
                        targetRow.Cells[pcategmenuno].Value = "None";
                    }

                    targetRow.Cells[pqtyno].Value = sourceRow.Cells[selecteditemqtyno].Value;
                    targetRow.Cells[proPartyPackDetailsId].Value = sourceRow.Cells[selecteditemppcdiidno].Value;
                    targetRow.Cells[PPackProductTypeno].Value = (int)PackItemType.ManualProduct;


                    if (Convert.ToInt32(sourceRow.Cells[selecteditemdeafultno].Value) == 1)
                    {
                        targetRow.ReadOnly = true;
                        targetRow.DefaultCellStyle.BackColor = Color.Gainsboro;
                        targetRow.DefaultCellStyle.ForeColor = Color.Black;
                        DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_product.Rows[rowIndex].Cells[pactionno];
                        cellcolumntype.Value = Blankimage;
                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        targetRow.Cells[pPartydeafult].Value = sourceRow.Cells[selecteditemdeafultno].Value;
                    }
                    else
                    {
                        DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_product.Rows[rowIndex].Cells[pactionno];
                        cellcolumntype.Value = Grid_Remove;
                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        targetRow.Cells[pPartydeafult].Value = sourceRow.Cells[selecteditemdeafultno].Value;
                    }
                }


                BllSearch<DmnPartyPackDetails> bll = new BllSearch<DmnPartyPackDetails>();
                DmnSearch search = new DmnSearch();
                search.Select = "PartyPackDetailsId, Item, Quantity, MenuCategoryId";
                search.Table = "tblPartyPackDetails";
                search.WhereNdGroupBy = $"WHERE PartyPackId = {Partypackid} AND Status = 1 AND " +
                    $"(PackItemType = {Convert.ToInt32(PackItemType.Card)} OR PackItemType = {Convert.ToInt32(PackItemType.Gift)} OR PackItemType = {Convert.ToInt32(PackItemType.Menu)})";

                var result = bll.GetSearchData("prdSearch", search);

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        int rowIndex = dgv_product.Rows.Add();
                        DataGridViewRow targetRow = dgv_product.Rows[rowIndex];

                        targetRow.Cells[pslno].Value = sl++; // continue serial
                        targetRow.Cells[pitemno].Value = item.Item;

                        if (item.MenuCategoryId != null && Convert.ToInt32(item.MenuCategoryId) > 0)
                            targetRow.Cells[pcategmenuno].Value = GetCategoryName(Convert.ToInt32(item.MenuCategoryId));
                        else
                            targetRow.Cells[pcategmenuno].Value = "None";

                        targetRow.Cells[pqtyno].Value = item.Quantity;
                        targetRow.Cells[proPartyPackDetailsId].Value = 0;
                        targetRow.Cells[pPartydeafult].Value = 1;

                        DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_product.Rows[rowIndex].Cells[pactionno];

                        targetRow.DefaultCellStyle.BackColor = Color.Gainsboro;
                        targetRow.DefaultCellStyle.ForeColor = Color.Black;
                        targetRow.ReadOnly = true;

                        cellcolumntype.Value = Blankimage;
                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    }
                }


                pnl_foodpacks.Visible = false;
                HideBlurPopup(pnl_foodpacks);
                pnl_enter_paks.Visible = false;
                HideBlurPopup(pnl_enter_paks);
                pnl_CustomItems_menu.Visible = false;
                HideBlurPopup(pnl_CustomItems_menu);

                dgv_packenter.Rows.Clear();
                selectedCustomItems.Clear();

                PriceChangecalculation();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  btnaddpack_main_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void AllowDecimalInput(object sender, KeyPressEventArgs e)
        {
            try
            {


                TextBox txt = sender as TextBox;

                if (txt == null) return;


                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
                if (e.KeyChar == '.' && txt.Text.Contains("."))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  AllowDecimalInput()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        #region PACK ENTRY PANEL + DGV AUTO ROW

        private void dgv_packenter_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow row = dgv_packenter.Rows[e.RowIndex];

                if (row.Cells[selecteditemqtyno].Value == null ||
                    row.Cells[selecteditemqtyno].Value.ToString().Trim() == "")
                {
                    row.Cells[selecteditemqtyno].Value = 1;
                    row.Cells[selecteditemdeafultno].Value = 2;
                }

                if (e.RowIndex == dgv_packenter.Rows.Count - 1)
                {
                    bool hasValue = false;

                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        if (i == selecteditemslno)
                            continue;

                        if (row.Cells[i].Value != null &&
                            row.Cells[i].Value.ToString().Trim() != "")
                        {
                            hasValue = true;
                            break;
                        }
                    }

                    if (hasValue)
                    {
                        AddNewRow();
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  dgv_packenter_CellEndEdit()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void OpenPackEntryPanel()
        {
            try
            {
                pnl_enter_paks.Visible = true;
                pnl_enter_paks.Visible = true;ShowBlurPopup(pnl_enter_paks);
                pnl_enter_paks.BringToFront();

                pnl_enter_paks.Width = (int)(this.Width * 0.6);
                pnl_enter_paks.Height = (int)(this.Height * 0.7);

                pnl_enter_paks.Left = (this.Width - pnl_enter_paks.Width) / 2;
                pnl_enter_paks.Top = (this.Height - pnl_enter_paks.Height) / 2;

                LoadMenuCategory_packenter();

                if (dgv_packenter.Rows.Count == 0)
                {
                    AddNewRow();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  OpenPackEntryPanel()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void AddNewRow()
        {
            int rowIndex = dgv_packenter.Rows.Add();
            dgv_packenter.Rows[rowIndex].Cells[customitemslno].Value = rowIndex + 1;
        }
        private void LoadMenuCategory_packenter()
        {
            try
            {
                BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                DmnSearch objSearch = new DmnSearch();

                objSearch.Select = "Fcid, CategoryName";
                objSearch.Table = "tblCategory_FTI";
                objSearch.WhereNdGroupBy = "WHERE Status = 1 AND CategoryType = 5";
                objSearch.OrderBy = "ORDER BY CategoryName";

                categoryList = bllSearch.GetSearchData("prdSearch", objSearch);

                DataGridViewComboBoxColumn cmb = (DataGridViewComboBoxColumn)dgv_packenter.Columns[selecteditemcategmenuno];

                cmb.DataSource = categoryList;
                cmb.DisplayMember = "CategoryName";
                cmb.ValueMember = "Fcid";

            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  LoadMenuCategory_packenter()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        #endregion

        #region Save Menu 
        private void btn_savemenucateg_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateEvent())
                    return;

                DmnEventCommonItems dmn = new DmnEventCommonItems();
                BllEventCommonItems bll = new BllEventCommonItems();

                dmn.EventItemId = 0;
                dmn.EventCategoryType = Convert.ToInt32(EventCategoryType.EventTypes);
                dmn.ItemName = txtItemName.Text.Trim();
                dmn.Quantity = 0;
                dmn.Price = 0;
                dmn.TaxSettingsId = 0;
                dmn.Tax = "";
                dmn.TaxType = "";
                dmn.DiscountPer = 0;
                dmn.DiscountAmt = 0;
                dmn.TotalAmount = 0;
                dmn.Description = txtDescription.Text.Trim();

                dmn.BranchId = FrmDashboard.BranchId;
                dmn.CreatedBy = FrmDashboard.UserId;
                dmn.Status = 1;

                bll.AddeditData("prdInsert_upd_EventCommonItems", dmn);

                CMessageBox.Show("Saved Successfully");

                LoadEventItems();

                txtItemName.Clear();
                txtDescription.Clear();

                pnl_eventreg.Visible = false;
                HideBlurPopup(pnl_eventreg);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  btn_savemenucateg_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        #endregion

        #region Custom Menu
        private void btn_addCusItems_Click(object sender, EventArgs e)
        {
            try
            {
                selectedCustomItems.Clear();
                pnl_CustomItems_menu.Visible = true;
                pnl_CustomItems_menu.Visible = true;ShowBlurPopup(pnl_CustomItems_menu);
                pnl_CustomItems_menu.BringToFront();

                pnl_CustomItems_menu.Width = (int)(this.Width * 0.6);
                pnl_CustomItems_menu.Height = (int)(this.Height * 0.7);

                pnl_CustomItems_menu.Left = (this.Width - pnl_CustomItems_menu.Width) / 2;
                pnl_CustomItems_menu.Top = (this.Height - pnl_CustomItems_menu.Height) / 2;

                LoadCustomMenu(CurrentPackId);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  btn_addCusItems_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void btn_cus_menu_cls_Click(object sender, EventArgs e)
        {
            pnl_CustomItems_menu.Visible = false;
            HideBlurPopup(pnl_CustomItems_menu);
        }

        private void btn_cus_menu_submit_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_CustomItems_menu.Visible = false;
                HideBlurPopup(pnl_CustomItems_menu);
                int sl = dgv_packenter.Rows.Count;

                foreach (var item in selectedCustomItems)
                {
                    bool alreadyExists = false;

                    foreach (DataGridViewRow dgRow in dgv_packenter.Rows)
                    {
                        if (dgRow.IsNewRow) continue;

                        var itemName = dgRow.Cells[selecteditemitemno].Value?.ToString();
                        var categoryId = dgRow.Cells[selecteditemcategmenuno].Value?.ToString();

                        if (itemName == item.ItemName &&
                            categoryId == item.CategoryId.ToString())
                        {
                            alreadyExists = true;
                            break;
                        }
                    }

                    if (alreadyExists) continue;

                    DataGridViewRow targetRow = null;

                    if (dgv_packenter.Rows.Count > 0)
                    {
                        DataGridViewRow lastRow = dgv_packenter.Rows[dgv_packenter.Rows.Count - 1];

                        bool isEmpty = true;

                        for (int i = 0; i < lastRow.Cells.Count; i++)
                        {
                            if (i == selecteditemslno)
                                continue;

                            if (lastRow.Cells[i].Value != null &&
                                lastRow.Cells[i].Value.ToString().Trim() != "")
                            {
                                isEmpty = false;
                                break;
                            }
                        }

                        if (isEmpty)
                        {
                            targetRow = lastRow;
                        }
                    }

                    if (targetRow == null)
                    {
                        int rowIndex = dgv_packenter.Rows.Add();
                        targetRow = dgv_packenter.Rows[rowIndex];
                        sl++;
                    }

                    targetRow.Cells[selecteditemslno].Value = sl;
                    targetRow.Cells[selecteditemitemno].Value = item.ItemName;

                    if (item.CategoryId != null && Convert.ToInt32(item.CategoryId) > 0)
                    {
                        targetRow.Cells[selecteditemcategmenuno].Value = Convert.ToInt64(item.CategoryId);
                    }

                    targetRow.Cells[selecteditemqtyno].Value = item.Qty;
                    targetRow.Cells[selecteditemppcdiidno].Value = 0;
                    targetRow.Cells[selecteditemdeafultno].Value = 1;

                }

                if (dgv_packenter.Rows.Count > 0)
                {
                    DataGridViewRow lastRow = dgv_packenter.Rows[dgv_packenter.Rows.Count - 1];

                    bool hasValue = false;

                    for (int i = 0; i < lastRow.Cells.Count; i++)
                    {
                        if (i == selecteditemslno)
                            continue;

                        if (lastRow.Cells[i].Value != null &&
                            lastRow.Cells[i].Value.ToString().Trim() != "")
                        {
                            hasValue = true;
                            break;
                        }
                    }

                    if (hasValue)
                    {
                        int newRowIndex = dgv_packenter.Rows.Add();
                        dgv_packenter.Rows[newRowIndex].Cells[selecteditemslno].Value =
                            dgv_packenter.Rows.Count;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  btn_cus_menu_submit_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }



        private void LoadCustomMenu(int packId)
        {
            try
            {

                dgv_cus_menu_items.Rows.Clear();
                flp_cus_menuitems.Controls.Clear();

                BllSearch<DmnPartyPackCustom> bll = new BllSearch<DmnPartyPackCustom>();

                DmnSearch search = new DmnSearch();
                search.Select = "PartyPackCustomDetailsId, Product, qty, MenuCategoryId";
                search.Table = "tblPartyPackCustomMenu";
                search.WhereNdGroupBy = "WHERE Status = 1";
                customMenuList = bll.GetSearchData("prdSearch", search);

                LoadCustomUsedCategories();
                BindCustomGrid(customMenuList);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  LoadCustomMenu()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void BindCustomGrid(List<DmnPartyPackCustom> list)
        {
            try
            {
                dgv_cus_menu_items.Rows.Clear();
                int sl = 1;
                LoadMenuCategory_cutom();

                foreach (var item in list)
                {

                    int rowIndex = dgv_cus_menu_items.Rows.Add();
                    var row = dgv_cus_menu_items.Rows[rowIndex];

                    row.Cells[customitemslno].Value = sl++;
                    row.Cells[customitemno].Value = item.Product;

                    if (item.MenuCategoryId != null && Convert.ToInt32(item.MenuCategoryId) > 0)
                    {
                        row.Cells[customitemcategmenuno].Value = Convert.ToInt64(item.MenuCategoryId);
                    }

                    row.Cells[customitemqtyno].Value = item.Qty;
                    row.Cells[customitemppcdiidno].Value = item.PartyPackCustomDetailsId;

                    bool isChecked = selectedCustomItems
                        .Any(x => x.PartyPackCustomDetailsId == item.PartyPackCustomDetailsId);
                    row.Cells[customitemselectNo].Value = isChecked;

                    row.Tag = item;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  BindCustomGrid()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void LoadCustomUsedCategories()
        {
            try
            {
                flp_cus_menuitems.Controls.Clear();

                Button btnAll = new Button();
                btnAll.Text = "All";
                btnAll.Tag = 0;
                btnAll.Width = 120;
                btnAll.Height = 35;
                btnAll.BackColor = Color.LightBlue;
                btnAll.FlatStyle = FlatStyle.Flat;

                btnAll.Click += (s, e) =>
                {
                    BindCustomGrid(customMenuList);
                };

                flp_cus_menuitems.Controls.Add(btnAll);

                var usedCategoryIds = customMenuList
                    .Select(x => x.MenuCategoryId)
                    .Distinct()
                    .ToList();

                foreach (var catId in usedCategoryIds)
                {
                    Button btn = new Button();
                    btn.Text = categoryList
                        .FirstOrDefault(x => x.Fcid == catId)?.CategoryName ?? "Unknown";
                    btn.Tag = catId;
                    btn.Width = 120;
                    btn.Height = 35;
                    btn.BackColor = Color.LightGray;
                    btn.FlatStyle = FlatStyle.Flat;

                    btn.Click += (s, e) =>
                    {
                        int cid = Convert.ToInt32(((Button)s).Tag);

                        var filtered = customMenuList
                            .Where(x => x.MenuCategoryId == cid)
                            .ToList();

                        BindCustomGrid(filtered);
                    };

                    flp_cus_menuitems.Controls.Add(btn);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  LoadCustomUsedCategories()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void dgv_cus_menu_items_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == customitemselectNo)
                {
                    var row = dgv_cus_menu_items.Rows[e.RowIndex];

                    if (row.Cells[customitemppcdiidno].Value == null)
                        return;

                    int id = Convert.ToInt32(row.Cells[customitemppcdiidno].Value);

                    string itemName = Convert.ToString(row.Cells[customitemno].Value ?? "");

                    int catId = row.Cells[customitemcategmenuno].Value != null
                        ? Convert.ToInt32(row.Cells[customitemcategmenuno].Value)
                        : 0;

                    decimal qty = row.Cells[customitemqtyno].Value != null
                        ? Convert.ToDecimal(row.Cells[customitemqtyno].Value)
                        : 0;

                    bool isChecked = row.Cells[customitemselectNo].Value != null && (bool)row.Cells[customitemselectNo].Value;

                    if (isChecked)
                    {
                        if (!selectedCustomItems.Any(x => x.PartyPackCustomDetailsId == id))
                        {
                            selectedCustomItems.Add(new SelectedCustomItem
                            {
                                PartyPackCustomDetailsId = id,
                                ItemName = itemName,
                                CategoryId = catId,
                                Qty = qty
                            });
                        }
                    }
                    else
                    {
                        selectedCustomItems.RemoveAll(x => x.PartyPackCustomDetailsId == id);
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  dgv_cus_menu_items_CellContentClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void dgv_cus_menu_items_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv_cus_menu_items.IsCurrentCellDirty)
                dgv_cus_menu_items.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        #endregion

        #region Card 

        public void FillForms()
        {
            try
            {
                foreach (Control ctrl in flw_categ.Controls)
                {
                    ctrl.Dispose();
                }
                flw_categ.Controls.Clear();
                int top = 5;
                decimal s = 0;
                int left = 0;

                BllSearch<DmnGridFields> bllacctype = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = " TOP 8 g.Caption as Field1,CAST(g.GTypeSubValue1 AS INT) AS Field2";
                dmnSearch.Table = " tblGeneralOptions g ";
                dmnSearch.WhereNdGroupBy = " where g.GTypeId=58 and g.Status=0 and GTypeSubId in(1,3,5,7)";
                dmnSearch.OrderBy = "order by GTypeSubId";
                var gridesource = bllacctype.GetSearchData("prdSearch", dmnSearch);
                if (gridesource.Count > 0)
                {
                    int columns = 4;
                    int spacing = 1;

                    for (int i = 0; i < gridesource.Count; i++)
                    {
                        s = Convert.ToDecimal(i) / 8;
                        Button dynamicButton = new Button();
                        dynamicButton.Width = flw_categ.Width / 5;
                        dynamicButton.Height = (int)(flw_categ.Height * 0.8m);

                        if (i > 3)
                            dynamicButton.Visible = false;
                        else
                            dynamicButton.Visible = true;

                        if (!string.IsNullOrEmpty(Categorybg))
                        {
                            dynamicButton.BackgroundImage = LoadButtonImage(Categorybg, null);
                        }
                        else
                        {
                            if (gridesource[i].Field3 != null && gridesource[i].Field3 != "")
                            {
                                dynamicButton.BackgroundImage = LoadButtonImage(gridesource[i].Field3, null);
                            }
                            else
                                dynamicButton.BackgroundImage = LoadButtonImage(null, Properties.Resources.cancel_32);
                        }

                        dynamicButton.BackgroundImageLayout = ImageLayout.Stretch;
                        dynamicButton.FlatStyle = FlatStyle.Flat;
                        dynamicButton.FlatAppearance.BorderSize = 0;
                        dynamicButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
                        dynamicButton.ForeColor = System.Drawing.Color.White;

                        if (gridesource[i].Field1 == "GeneralMerchandise")
                        {
                            dynamicButton.Text = "General Merchandise";
                        }
                        else if (gridesource[i].Field1 == "BowlingAccessories")
                        {
                            dynamicButton.Text = "Bowling Accessories";
                        }
                        else
                            dynamicButton.Text = gridesource[i].Field1;


                        dynamicButton.Tag = gridesource[i].Field2;
                        dynamicButton.Name = "DynamicButton" + i;

                        if (CategoryFontstatus == 1 && !string.IsNullOrEmpty(CategoryFontdata))
                        {
                            #region saved font
                            try
                            {
                                string[] fontParts = CategoryFontdata.Split(',');
                                if (fontParts.Length == 3)
                                {
                                    string fontName = fontParts[0].Trim();
                                    float fontSize = float.Parse(fontParts[1].Trim());
                                    string fontStyleStr = fontParts[2].Trim().ToLower();
                                    FontStyle style = fontStyleStr == "bold" ? FontStyle.Bold : FontStyle.Regular;

                                    dynamicButton.Font = new Font(fontName, fontSize, style);
                                }
                                else
                                {
                                    dynamicButton.Font = new Font("Georgia", 14); // fallback if config format is wrong
                                }
                            }
                            catch
                            {
                                dynamicButton.Font = new Font("Georgia", 14); // fallback on exception
                            }
                            #endregion
                        }
                        else
                        {
                            #region Defult Font 
                            if (string.IsNullOrEmpty(gridesource[i].Field4))
                            {
                                dynamicButton.Font = new Font("Georgia", 14);
                            }
                            else
                            {
                                try
                                {
                                    string[] textSplit = gridesource[i].Field4.Split(',');
                                    if (textSplit.Length >= 2)
                                    {
                                        string fontName = string.Join(",", textSplit.Take(textSplit.Length - 1)).Trim();
                                        float fontSize = float.Parse(textSplit.Last().Trim());

                                        dynamicButton.Font = new Font(fontName, fontSize);
                                    }
                                    else
                                    {
                                        dynamicButton.Font = new Font("Georgia", 14); // fallback
                                    }
                                }
                                catch
                                {
                                    dynamicButton.Font = new Font("Georgia", 14); // fallback on error
                                }
                            }
                            #endregion
                        }


                        //dynamicButton.MouseDown += DynamicButton_MouseDown;
                        //dynamicButton.MouseUp += DynamicButton_MouseUp;
                        dynamicButton.Click += new EventHandler(DynamicButton_Click);

                        flw_categ.Controls.Add(dynamicButton);

                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  FillForms()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void DynamicButton_Click(object sender, EventArgs e)
        {
            try
            {
                Memoryrelease();
                int n = 5;
                decimal s = 0;
                int r = 0, c = 0;

                foreach (Control ctrl in pnlitembuttons.Controls)
                {
                    ctrl.Dispose();
                }
                pnlitembuttons.Controls.Clear();

                int tag = Convert.ToInt32((sender as Button).Tag);
                BllSearch<DmnGridFields> _bllSearch = new BllSearch<DmnGridFields>();
                string table = "";
                DmnSearch dmnSearch = new DmnSearch();
                int tabtype = 0;
                int gsubid = 0;
                string heading = "";
                string productcolumnname = "";

                int locationId = FrmLogin.GameBranchId;
                switch ((FoodandBeverage.Interfaces.ProductType)tag)
                {
                    case FoodandBeverage.Interfaces.ProductType.Card:
                        table = "tblCardProducts";
                        tabtype = (int)FoodandBeverage.Interfaces.ProductType.Card;
                        heading = "Card Products";
                        productcolumnname = "p.Product_id";
                        break;

                    case FoodandBeverage.Interfaces.ProductType.GateAccess:
                        table = "tblGatePass";
                        tabtype = (int)FoodandBeverage.Interfaces.ProductType.GateAccess;
                        heading = "Gate Pass";
                        productcolumnname = "p.Productid";
                        break;

                    case FoodandBeverage.Interfaces.ProductType.Trampoline:
                        table = "tblTrampolining";
                        tabtype = (int)FoodandBeverage.Interfaces.ProductType.Trampoline;
                        heading = "Trampolining";
                        productcolumnname = "p.Productid";
                        break;

                    case FoodandBeverage.Interfaces.ProductType.TimePlay:
                        table = "tblTimePlayProducts";
                        tabtype = (int)FoodandBeverage.Interfaces.ProductType.TimePlay;
                        heading = "TimePlay Products";
                        productcolumnname = "p.ProductId";
                        break;

                    default:
                        throw new ArgumentException("Invalid product tag");
                }

                txtTable.Text = table;
                lblProductHeading.Text = heading;

                dmnSearch.Select = $@"
                                    TOP 50
                                    {productcolumnname} as Field1,
                                    p.Description as Field2,
                                    0 as Field3,
                                    0 as Field4";

                dmnSearch.Table = $@"
                                    [{GameDB}].dbo.{table} p
                                    LEFT JOIN [{GameDB}].dbo.tblCardProductLocMapping pm 
                                        ON pm.Product = {productcolumnname} AND pm.ProductType = {tabtype}";

                dmnSearch.WhereNdGroupBy = $@"
                                    WHERE p.BranchId = {locationId} and pm.Location={locationId}
                                    AND pm.Status = 1 and p.Status=1
                                    AND pm.ProductType = {tabtype}";

                dmnSearch.OrderBy = "ORDER BY p.Description";

                var gridesource = _bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridesource.Count > 0)
                {
                    if (tag == Convert.ToInt32(PosSalProduct.Card))
                    {
                        // REMOVE DUPLICATES BY SubCategoryId (Field5)
                        var filteredSource = gridesource
                           .GroupBy(x =>
                               x.Field6 == null || Convert.ToInt32(x.Field6) == 0
                                   ? Guid.NewGuid().ToString()
                                   : x.Field5.ToString())
                           .Select(g => g.First())
                           .ToList();

                        if (filteredSource.Count > 0)
                        {
                            txtTable.Text = table;

                            TableLayoutPanel tlpProduct = new TableLayoutPanel();
                            tlpProduct.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                            tlpProduct.Size = pnlitembuttons.Size;
                            tlpProduct.AutoScroll = true;
                            tlpProduct.Name = "tlp_" + table.Trim();
                            tlpProduct.AutoScrollMargin = new Size(10, 10);
                            tlpProduct.ColumnCount = (screnwidth > 1365 && screnheight > 765) ? 6 : 5;

                            for (int i = 0; i < filteredSource.Count; i++)
                            {
                                var item = filteredSource[i];

                                Button dynamicButton = new Button();
                                dynamicButton.Height = 70;
                                dynamicButton.Width = (pnlitembuttons.Width - 43) / tlpProduct.ColumnCount;
                                dynamicButton.Dock = DockStyle.None;

                                if (Convert.ToInt32(item.Field5) > 0)
                                {
                                    dynamicButton.Text = item.Field6;
                                    dynamicButton.Tag = item.Field5;
                                    dynamicButton.AccessibleDescription = "1";
                                }
                                else
                                {
                                    dynamicButton.Text = item.Field2;
                                    dynamicButton.Tag = item.Field1;
                                    dynamicButton.AccessibleDescription = "0";
                                }


                                dynamicButton.Name = "DynamicButton" + i;
                                dynamicButton.FlatStyle = FlatStyle.Flat;
                                dynamicButton.ForeColor = Color.White;

                                // ENABLE / DISABLE LOGIC
                                dynamicButton.BackgroundImage = LoadButtonImage(null, Properties.Resources.Untitled_2_2030);
                                dynamicButton.BackgroundImageLayout = ImageLayout.Stretch;

                                dynamicButton.BackgroundImageLayout = ImageLayout.Stretch;


                                if (string.IsNullOrEmpty(item.Field4) || Convert.ToInt32(item.Field4) == 0)
                                {
                                    dynamicButton.Font = (screnwidth > 1365 && screnheight > 765)
                                        ? new Font("Georgia", 14)
                                        : new Font("Georgia", 10);
                                }
                                else
                                {
                                    string[] textSplit = item.Field4.Split(',');
                                    string fontName = textSplit[0];
                                    float fontSize = float.Parse(textSplit[textSplit.Length - 1]);

                                    dynamicButton.Font = new Font(fontName, fontSize);
                                }

                                // CONTEXT MENU
                                ContextMenuStrip contextMenu = new ContextMenuStrip();
                                ToolStripMenuItem item1 = new ToolStripMenuItem("BG Image");
                                ToolStripMenuItem item2 = new ToolStripMenuItem("Font Style");

                                item1.Image = Properties.Resources.background_mouse_clcik;
                                item2.Image = Properties.Resources.font_mouselick;
                                item1.Font = item2.Font = new Font("Arial", 12, FontStyle.Bold);
                                item1.ForeColor = item2.ForeColor = Color.Black;

                                //item1.Click += Item1_Click;
                                //item2.Click += Item2_Click;

                                contextMenu.Items.Add(item1);
                                contextMenu.Items.Add(item2);
                                dynamicButton.ContextMenuStrip = contextMenu;

                                dynamicButton.Click += DynamicButtonProduct_Click;

                                // GRID POSITIONING
                                tlpProduct.Controls.Add(dynamicButton, c, r);
                                c++;

                                if (c >= tlpProduct.ColumnCount)
                                {
                                    c = 0;
                                    r++;
                                }
                            }

                            pnlitembuttons.Controls.Add(tlpProduct);
                        }
                    }
                    else
                    {
                        txtTable.Text = table;
                        TableLayoutPanel tlpProduct = new TableLayoutPanel();
                        tlpProduct.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                        tlpProduct.Size = pnlitembuttons.Size;
                        tlpProduct.AutoScroll = true;
                        tlpProduct.AutoScrollMargin = new System.Drawing.Size(10, 10);
                        //tlpProduct.Name = tlpProduct + table;
                        if (screnwidth > 1365 && screnheight > 765)
                        {
                            tlpProduct.ColumnCount = 6;
                        }
                        else
                        {
                            tlpProduct.ColumnCount = 5;
                        }
                        for (int i = 0; i < gridesource.Count; i++)
                        {
                            s = Convert.ToDecimal(i % 6);
                            Button dynamicButton = new Button();
                            dynamicButton.Height = 70;
                            if (screnwidth > 1365 && screnheight > 765)
                            {
                                dynamicButton.Width = (pnlitembuttons.Width - 43) / tlpProduct.ColumnCount;
                                dynamicButton.Dock = DockStyle.None;
                            }
                            else
                            {
                                dynamicButton.Width = (pnlitembuttons.Width - 43) / tlpProduct.ColumnCount;//dynamicButton.Width = 156;
                                dynamicButton.Dock = DockStyle.None;
                            }

                            dynamicButton.BackgroundImage = LoadButtonImage(null, Properties.Resources.Untitled_2_2030);
                            dynamicButton.BackgroundImageLayout = ImageLayout.Stretch;
                            dynamicButton.FlatStyle = FlatStyle.Flat;
                            dynamicButton.ForeColor = Color.White;

                            if (s == 0 && i != 0)
                            {
                                r = r + 1;
                                c = 0;
                            }
                            dynamicButton.Text = gridesource[i].Field2;
                            dynamicButton.Tag = gridesource[i].Field1;
                            dynamicButton.Name = "DynamicButton_P" + i;
                            if ((gridesource[i].Field4 == null) || (gridesource[i].Field4 == "") || (Convert.ToInt32(gridesource[i].Field4) == 0))
                            {
                                if (screnwidth > 1365 && screnheight > 765)
                                {
                                    dynamicButton.Font = new Font("Georgia", 14);
                                }
                                else
                                {
                                    dynamicButton.Font = new Font("Georgia", 10);
                                }

                            }
                            else
                            {
                                string strng = gridesource[i].Field4;
                                string[] textSplit = strng.Split(',');
                                int f = textSplit.Length - 2;
                                string fontn = "";
                                for (int t = 0; t < textSplit.Length - 1; t++)
                                {
                                    fontn = fontn + textSplit[f];
                                }
                                float fontsz = float.Parse(textSplit[textSplit.Length - 1]);
                                dynamicButton.Font = new Font(fontn, fontsz);
                            }

                            /* Button sub menu list */
                            ContextMenuStrip contextMenu = new ContextMenuStrip();
                            ToolStripMenuItem item1 = new ToolStripMenuItem("BG Image");
                            ToolStripMenuItem item2 = new ToolStripMenuItem("Font Style");
                            contextMenu.Items.Add(item1);
                            contextMenu.Items.Add(item2);

                            item1.Image = FoodandBeverage.Properties.Resources.background_mouse_clcik;
                            item2.Image = FoodandBeverage.Properties.Resources.font_mouselick;
                            item1.ForeColor = Color.Black;
                            item2.ForeColor = Color.Black;
                            item1.Font = new Font("Arial", 12, FontStyle.Bold);
                            item2.Font = new Font("Arial", 12, FontStyle.Bold);

                            dynamicButton.ContextMenuStrip = contextMenu;
                            //item1.Click += new EventHandler(Item1_Click);
                            //item2.Click += new EventHandler(Item2_Click);


                            dynamicButton.Click += new EventHandler(DynamicButtonProduct_Click);
                            tlpProduct.Controls.Add(dynamicButton, c, r);

                            c++;
                        }

                        pnlitembuttons.Controls.Add(tlpProduct);
                    }
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  DynamicButton_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void DynamicButtonProduct_Click(object sender, EventArgs e)
        {
            try
            {
                string table = string.IsNullOrEmpty(txtTable.Text) ? "tblCardProducts" : txtTable.Text;
                int tag = Convert.ToInt32((sender as Button).Tag);
                int subcatestatus = Convert.ToInt32((sender as Button).AccessibleDescription);

                BllSearch<DmnGridFields> _bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                int ptypeno = 0;

                // CARD PRODUCTS
                if (table == "tblCardProducts")
                {
                    ptypeno = Convert.ToInt32(FoodandBeverage.Interfaces.ProductType.Card);


                    if (subcatestatus == 1)
                    {
                        pnlitembuttons.Controls.Clear();

                        dmnSearch.Select = @"
                    TOP 50 
                    p.Product_Id as Field1,
                    p.Description as Field2,
                    p.SubCategoryid as Field5,
                    sp.SubCategoryName as Field6,
                    pm.ProductType as Field7,
                    pm.Location as Field8";

                        dmnSearch.Table = $@"
                    [{GameDB}].dbo.tblCardProducts p
                    LEFT JOIN [{GameDB}].dbo.tblCardProductLocMapping pm
                        ON pm.Product = p.ProductId AND pm.ProductType = {ptypeno}
                    LEFT JOIN [{GameDB}].dbo.tblCardSubCategory sp
                        ON sp.Id = p.SubCategoryid AND sp.Status = 1";

                        dmnSearch.WhereNdGroupBy = $@"
                    WHERE p.Status = 1 
                        AND p.SubCategoryid = {tag} 
                        AND pm.Location = {FrmLogin.GameBranchId} 
                        AND pm.ProductType = {ptypeno}";

                        dmnSearch.OrderBy = "ORDER BY p.Description";

                        var gridesource = _bllSearch.GetSearchData("prdSearch", dmnSearch);

                        TableLayoutPanel tlpProduct = new TableLayoutPanel
                        {
                            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom,
                            Size = pnlitembuttons.Size,
                            AutoScroll = true,
                            Name = "tlp_" + table.Trim(),
                            AutoScrollMargin = new Size(10, 10),
                            ColumnCount = (screnwidth > 1365 && screnheight > 765) ? 6 : 5
                        };

                        int c = 0, r = 0;
                        foreach (var item in gridesource)
                        {
                            Button dynamicButton = new Button
                            {
                                Height = 70,
                                Width = (pnlitembuttons.Width - 43) / tlpProduct.ColumnCount,
                                Dock = DockStyle.None,
                                Text = item.Field2,
                                Tag = item.Field1,
                                AccessibleDescription = "0",
                                Name = "DynamicButton" + c,
                                FlatStyle = FlatStyle.Flat,
                                ForeColor = Color.White,
                                BackgroundImageLayout = ImageLayout.Stretch
                            };

                            dynamicButton.BackgroundImage =
                                string.IsNullOrEmpty(item.Field3)
                                ? LoadButtonImage(null, Properties.Resources.Untitled_2_2030)
                                : LoadButtonImage(item.Field3, null);

                            if (string.IsNullOrEmpty(item.Field4))
                                dynamicButton.Font = (screnwidth > 1365 && screnheight > 765) ? new Font("Georgia", 14) : new Font("Georgia", 10);
                            else
                            {
                                string[] textSplit = item.Field4.Split(',');
                                string fontName = textSplit[0];
                                float fontSize = float.Parse(textSplit[textSplit.Length - 1]);
                                dynamicButton.Font = new Font(fontName, fontSize);
                            }

                            dynamicButton.Click += DynamicButtonProduct_Click;

                            tlpProduct.Controls.Add(dynamicButton, c, r);
                            c++;
                            if (c >= tlpProduct.ColumnCount)
                            {
                                c = 0;
                                r++;
                            }
                        }

                        pnlitembuttons.Controls.Add(tlpProduct);

                        // Add Back button
                        Button btnBack = new Button
                        {
                            Name = "btnBack",
                            Text = "Back",
                            Height = 45,
                            Width = 120,
                            Font = new Font("Segoe UI", 12, FontStyle.Bold),
                            ForeColor = Color.White,
                            FlatStyle = FlatStyle.Flat,
                            BackColor = Color.Transparent,
                            UseVisualStyleBackColor = false,
                            BackgroundImage = LoadButtonImage(null, Properties.Resources.Untitled_2_2030),
                            BackgroundImageLayout = ImageLayout.Stretch,
                            Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
                            Location = new Point(10, pnlitembuttons.Height - 55)
                        };
                        btnBack.Click += BtnBack_Click;
                        pnlitembuttons.Controls.Add(btnBack);
                        btnBack.BringToFront();

                        return;
                    }
                    else
                    {
                        // Single product load for Card
                        dmnSearch.Select = $@"
                    p.Product_id as Id,
                    p.Description as Field1,
                    p.SuggestedPrice as Field2,
                    p.Tax as Field3,
                    p.TaxSettingsId as Field4";
                        dmnSearch.Table = $"[{GameDB}].dbo.{table} p";
                        dmnSearch.WhereNdGroupBy = $"WHERE p.Product_id = {tag}";
                    }
                }
                // TIME PLAY
                else if (table == "tblTimePlayProducts")
                {
                    ptypeno = Convert.ToInt32(FoodandBeverage.Interfaces.ProductType.TimePlay);
                    dmnSearch.Select = $@"
                p.Productid as Id,
                p.Description as Field1,
                p.SuggestedPrice as Field2,
                p.Tax as Field3,
                p.TaxSettingsId as Field4,
                p.MinDuration as Field5,
                p.RoundsPerPlay as Field6,
                p.GameInterval as Field7";
                    dmnSearch.Table = $"[{GameDB}].dbo.{table} p";
                    dmnSearch.WhereNdGroupBy = $"WHERE p.Productid = {tag}";
                }
                // GATE PASS
                else if (table == "tblGatePass")
                {
                    ptypeno = Convert.ToInt32(FoodandBeverage.Interfaces.ProductType.GateAccess);
                    dmnSearch.Select = $@"
                p.Productid as Id,
                p.Description as Field1,
                p.SuggestedPrice as Field2,
                p.Tax as Field3,
                p.TaxSettingsId as Field4,
                p.CashBalance as Field5,
                p.CashBonusBalance as Field6,
                p.TokenBalance as Field7,
                p.Points as Field8";
                    dmnSearch.Table = $"[{GameDB}].dbo.{table} p";
                    dmnSearch.WhereNdGroupBy = $"WHERE p.Productid = {tag}";
                }
                // TRAMPOLINE
                else if (table == "tblTrampolining")
                {
                    ptypeno = Convert.ToInt32(FoodandBeverage.Interfaces.ProductType.Trampoline);
                    dmnSearch.Select = $@"
                p.Productid as Id,
                p.Description as Field1,
                p.SuggestedPrice as Field2,
                p.Tax as Field3,
                p.TaxSettingsId as Field4,
                p.CashBalance as Field5,
                p.CashBonusBalance as Field6,
                p.TokenBalance as Field7,
                p.Points as Field8";
                    dmnSearch.Table = $"[{GameDB}].dbo.{table} p";
                    dmnSearch.WhereNdGroupBy = $"WHERE p.Productid = {tag}";
                }

                var gridSource = _bllSearch.GetSearchData("prdSearch", dmnSearch);

                if (gridSource != null && gridSource.Count > 0)
                {
                    foreach (var item in gridSource)
                    {
                        bool exists = dgv_product.Rows.Cast<DataGridViewRow>()
                            .Any(r => !r.IsNewRow
                                      && Convert.ToInt32(r.Cells[pProductid].Value ?? 0) == item.Id
                                      && Convert.ToInt32(r.Cells[PPackProductTypeno].Value ?? 0) == (int)PackItemType.Card);

                        if (exists)
                        {
                            CMessageBox.Show("This product is already added!", "Duplicate",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            continue;
                        }

                        ResetDiscount();

                        int rowIndex = dgv_product.Rows.Add();
                        DataGridViewRow row = dgv_product.Rows[rowIndex];

                        row.Cells[pslno].Value = rowIndex + 1;
                        row.Cells[pProductid].Value = item.Id;
                        row.Cells[pitemno].Value = item.Field1;
                        row.Cells[pcategmenuno].Value = ((ProductType)ptypeno).ToString();
                        row.Cells[pqtyno].Value = 1;
                        row.Cells[pPrice].Value = Convert.ToDecimal(item.Field2);
                        row.Cells[pAmount].Value = Convert.ToDecimal(item.Field2);

                        row.Cells[pdiscper].Value = 0;
                        row.Cells[pdisamt].Value = 0;
                        row.Cells[ptax].Value = item.Field3 ?? "";
                        row.Cells[ptaxtype].Value = "";
                        row.Cells[ptaxper].Value = 0;
                        row.Cells[ptaxamt].Value = 0;
                        row.Cells[ppretaxamt].Value = Convert.ToDecimal(item.Field2);
                        row.Cells[pigstamt].Value = 0;
                        row.Cells[pvatamt].Value = 0;
                        row.Cells[pnetamt].Value = Convert.ToDecimal(item.Field2);
                        row.Cells[ppridno].Value = 0;
                        row.Cells[PPackProductTypeno].Value = (int)PackItemType.Card;
                        row.Cells[pproducttype].Value = ptypeno;
                        row.Cells[proPartyPackDetailsId].Value = 0;
                        row.Cells[pPartydeafult].Value = 2;
                        row.Cells[ptaxsettingid].Value = Convert.ToInt32(item.Field4);
                        row.Cells[phsncodeno].Value = GetPartyItemHsnCode((int)PackItemType.Card, Convert.ToInt32(item.Id));

                        DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)row.Cells[pactionno];
                        cellcolumntype.Value = Grid_Remove;
                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;

                        CalculateAndApplyTax(rowIndex, (int)PackItemType.Card, Convert.ToInt32(item.Id));

                    }
                }

                pnl_cardpreview.Visible = false;
                HideBlurPopup(pnl_cardpreview);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  DynamicButtonProduct_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void BtnBack_Click(object sender, EventArgs e)
        {
            FillForms();
        }
        private Image LoadButtonImage(string fileName, Image fallbackImage)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                    return fallbackImage;

                string imagePath = Path.Combine(Application.StartupPath, "Background_Images", fileName + ".png");

                if (!File.Exists(imagePath))
                    return fallbackImage;

                using (Image img = Image.FromFile(imagePath))
                {
                    return new Bitmap(img); // safe copy
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page LoadButtonImage()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));

                return fallbackImage;
            }
        }
        private void btncards_cls_Click(object sender, EventArgs e)
        {
            pnl_cardpreview.Visible = false;
            HideBlurPopup(pnl_cardpreview);
        }
        public bool checkPartySelet()
        {
            if (Partypackid == 0)
            {
                CMessageBox.Show("Please select a Party Pack", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private void btncard_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkPartySelet())
                    return;

                pnl_cardpreview.Visible = true;
                pnl_cardpreview.Visible = true;ShowBlurPopup(pnl_cardpreview);
                pnl_cardpreview.BringToFront();

                int widthPercent = 80;
                int heightPercent = 80;

                pnl_cardpreview.Width = this.ClientSize.Width * widthPercent / 100;
                pnl_cardpreview.Height = this.ClientSize.Height * heightPercent / 100;

                pnl_cardpreview.Left = (this.ClientSize.Width - pnl_cardpreview.Width) / 2;
                pnl_cardpreview.Top = (this.ClientSize.Height - pnl_cardpreview.Height) / 2;

                FillForms();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  btncard_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void LoadProducts(int tag, string searchText = "")
        {
            try
            {
                Memoryrelease();

                pnlitembuttons.Controls.Clear();

                int r = 0, c = 0;

                BllSearch<DmnGridFields> _bllSearch = new BllSearch<DmnGridFields>();
                string table = "";
                DmnSearch dmnSearch = new DmnSearch();
                int tabtype = 0;
                string heading = "";
                string productcolumnname = "";

                int locationId = FrmLogin.GameBranchId;

                switch ((PosSalProduct)tag)
                {
                    case PosSalProduct.Card:
                        table = "tblCardProducts";
                        tabtype = (int)FoodandBeverage.Interfaces.ProductType.Card;
                        heading = "Card Products";
                        productcolumnname = "p.Product_id";
                        break;

                    case PosSalProduct.GateAccess:
                        table = "tblGatePass";
                        tabtype = (int)FoodandBeverage.Interfaces.ProductType.GateAccess;
                        heading = "Gate Pass";
                        productcolumnname = "p.Productid";
                        break;

                    case PosSalProduct.Trampoline:
                        table = "tblTrampolining";
                        tabtype = (int)FoodandBeverage.Interfaces.ProductType.Trampoline;
                        heading = "Trampolining";
                        productcolumnname = "p.Productid";
                        break;

                    case PosSalProduct.TimePlay:
                        table = "tblTimePlayProducts";
                        tabtype = (int)FoodandBeverage.Interfaces.ProductType.TimePlay;
                        heading = "TimePlay Products";
                        productcolumnname = "p.ProductId";
                        break;

                    default:
                        throw new ArgumentException("Invalid product tag");
                }

                txtTable.Text = table;
                lblProductHeading.Text = heading;

                string searchCondition = "";
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    searchCondition = $" AND p.Description LIKE '%{searchText.Replace("'", "''")}%'";
                }

                dmnSearch.Select = $@"
            TOP 100
            {productcolumnname} as Field1,
            p.Description as Field2,
            0 as Field3,
            0 as Field4";

                dmnSearch.Table = $@"
            [{GameDB}].dbo.{table} p
            LEFT JOIN [{GameDB}].dbo.tblCardProductLocMapping pm 
                ON pm.Product = {productcolumnname} 
                AND pm.ProductType = {tabtype}";

                dmnSearch.WhereNdGroupBy = $@"
            WHERE pm.Location = {locationId} 
            AND pm.Status = 1 
            AND p.Status = 1
            AND pm.ProductType = {tabtype}
            {searchCondition}";

                dmnSearch.OrderBy = "ORDER BY p.Description";

                var gridesource = _bllSearch.GetSearchData("prdSearch", dmnSearch);

                if (gridesource.Count == 0)
                    return;

                TableLayoutPanel tlpProduct = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    ColumnCount = (screnwidth > 1365 && screnheight > 765) ? 6 : 5
                };

                foreach (var item in gridesource)
                {
                    if (c >= tlpProduct.ColumnCount)
                    {
                        c = 0;
                        r++;
                    }
                    Button btn = new Button
                    {
                        Height = 70,
                        Width = (pnlitembuttons.Width - 43) / tlpProduct.ColumnCount,
                        Text = item.Field2,
                        Tag = item.Field1,
                        FlatStyle = FlatStyle.Flat,
                        ForeColor = Color.White,
                        BackgroundImage = LoadButtonImage(null, Properties.Resources.Untitled_2_2030),
                        BackgroundImageLayout = ImageLayout.Stretch
                    };

                    btn.Click += DynamicButtonProduct_Click;

                    tlpProduct.Controls.Add(btn, c, r);
                    c++;
                }

                pnlitembuttons.Controls.Add(tlpProduct);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  btncard_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void btncardserach_Click(object sender, EventArgs e)
        {

        }
        private void txtcardserach_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int tag = 0;

                if (lblProductHeading.Text == "Card Products")
                    tag = (int)PosSalProduct.Card;
                else if (lblProductHeading.Text == "Gate Pass")
                    tag = (int)PosSalProduct.GateAccess;
                else if (lblProductHeading.Text == "Trampolining")
                    tag = (int)PosSalProduct.Trampoline;
                else if (lblProductHeading.Text == "TimePlay Products")
                    tag = (int)PosSalProduct.TimePlay;

                if (tag == 0)
                {
                    CMessageBox.Show("Please select category first.",
                                    "Information",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    txtcardserach.Clear();   // optional: clear search box
                    return;
                }

                LoadProducts(tag, txtcardserach.Text.Trim());
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  txtcardserach_TextChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        #endregion

        #region Gift 
        private void btngiftprevcls_Click(object sender, EventArgs e)
        {
            pnlgiftpreview.Visible = false;
            HideBlurPopup(pnlgiftpreview);
        }
        private void btngift_Click(object sender, EventArgs e)
        {
            if (!checkPartySelet())
                return;

            pnlgiftpreview.Visible = true;
            pnlgiftpreview.Visible = true;ShowBlurPopup(pnlgiftpreview);
            pnlgiftpreview.BringToFront();
            pnlgiftpreview.Left = (this.ClientSize.Width - pnlgiftpreview.Width) / 2;
            pnlgiftpreview.Top = (this.ClientSize.Height - pnlgiftpreview.Height) / 2;
            Redeemproducts();
        }
        private void txtserachgift_TextChanged(object sender, EventArgs e)
        {
            Redeemproducts(txtserachgift.Text.Trim());
        }
        private void btn_searchgift_Click(object sender, EventArgs e)
        {
            Redeemproducts(txtserachgift.Text.Trim());
        }

        private void Redeemproducts(string searchText = "")
        {
            try
            {
                Memoryrelease();

                flp_itemredproducts.Controls.Clear();

                BllSearch<DmnGridFields> _bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();

                lblProductHeading.Text = "Gift Products";

                dmnSearch.Select = "r.ProductId as Id, " +
                          "UPC As Field1, " +
                          "Description As Field2, " +
                          "SuggestedPointValue As Field3, " +

                          "(Select top(1) Pr.SRate " +
                          " from [" + GameDB + "].dbo.tblProductRate pr " +
                          " where pr.ProductId = r.ProductId " +
                          " and pr.ProductType = " + (int)FoodandBeverage.Interfaces.ProductType.RedemptionTicket +
                          " and pr.BranchId = " + FrmLogin.GameBranchId +
                          " order by pr.Id desc) As Field4, " +

                          "(Select Sum(QOH) " +
                          " from [" + GameDB + "].dbo.tblProductRate pr " +
                          " where pr.ProductId = r.ProductId " +
                          " and pr.ProductType = " + (int)FoodandBeverage.Interfaces.ProductType.RedemptionTicket +
                          " and pr.BranchId = " + FrmLogin.GameBranchId +
                          ") As Field5, " +

                          "0 as Field6, " +
                          "0 as Field7";

                dmnSearch.Table = "[" + GameDB + "].dbo.tblRedemptionProducts r " +
                                  "left join [" + GameDB + "].dbo.tblCardProductLocMapping l " +
                                  "on l.Product = r.ProductId";

                string searchFilter = "";
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    searchFilter = " AND r.Description LIKE '%" + searchText.Replace("'", "''") + "%'";
                }

                dmnSearch.WhereNdGroupBy = "Where r.Status=1  and l.Location = " + FrmLogin.GameBranchId + " and l.Status=1 and l.ProductType = " + (int)FoodandBeverage.Interfaces.ProductType.RedemptionTicket +
                                          " and (Select Sum(QOH) from [" + GameDB + @"].dbo.tblProductRate pr " +
                                          " where pr.ProductId = r.ProductId " +
                                          " and pr.ProductType = " + (int)FoodandBeverage.Interfaces.ProductType.RedemptionTicket +
                                          " and pr.BranchId=" + FrmLogin.GameBranchId + ") > 0"
                                          + searchFilter;

                var gridesource = _bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridesource.Count == 0)
                    return;

                foreach (var item in gridesource)
                {
                    Button btn = CreateProductButton(item);
                    btn.Margin = new Padding(5);
                    flp_itemredproducts.Controls.Add(btn);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  Redeemproducts()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private Button CreateProductButton(DmnGridFields item)
        {
            try
            {


                Button btn = new Button
                {
                    Height = 70,
                    FlatStyle = FlatStyle.Flat,
                    BackgroundImageLayout = ImageLayout.Stretch,
                    ForeColor = Color.White,
                    Tag = item.Id,
                    Text = item.Field2,
                    Name = "DynamicButton_P" + item.Id
                };

                int columns = (screnwidth > 1365 && screnheight > 765) ? 6 : 5;
                btn.Width = (pnlitembuttons.Width - 43) / columns;

                if (Convert.ToInt32(item.Field5) <= 0)
                {
                    btn.Enabled = false;
                    btn.BackgroundImage = Properties.Resources.INACTIVEBUTTON;
                }
                else
                {
                    if (string.IsNullOrEmpty(item.Field6))
                        btn.BackgroundImage = Properties.Resources.N_dashboard_button;//Properties.Resources.Untitled_2_2030;
                    else
                    {
                        string path = Path.Combine(Application.StartupPath, "Background_Images", item.Field6 + ".png");
                        if (File.Exists(path))
                            btn.BackgroundImage = Image.FromFile(path);
                        else
                            btn.BackgroundImage = Properties.Resources.N_dashboard_button;
                    }
                }

                if (!string.IsNullOrEmpty(item.Field7))
                {
                    try
                    {
                        string[] split = item.Field7.Split(',');
                        float size = float.Parse(split.Last());
                        string fontName = string.Join(",", split.Take(split.Length - 1));
                        btn.Font = new Font(fontName, size);
                    }
                    catch { btn.Font = new Font("Georgia", 10); }
                }
                else
                {
                    btn.Font = (screnwidth > 1365 && screnheight > 765)
                        ? new Font("Georgia", 14)
                        : new Font("Georgia", 10);
                }

                ContextMenuStrip menu = new ContextMenuStrip();
                ToolStripMenuItem bgItem = new ToolStripMenuItem("BG Image", FoodandBeverage.Properties.Resources.background_mouse_clcik);
                ToolStripMenuItem fontItem = new ToolStripMenuItem("Font Style", FoodandBeverage.Properties.Resources.font_mouselick);
                bgItem.ForeColor = fontItem.ForeColor = Color.Black;
                bgItem.Font = fontItem.Font = new Font("Arial", 12, FontStyle.Bold);
                menu.Items.AddRange(new[] { bgItem, fontItem });
                btn.ContextMenuStrip = menu;
                btn.Click += RedemptionProduct_clcik;
                return btn;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  CreateProductButton()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
                return null;
            }
        }

        public void RedemptionProduct_clcik(object sender, EventArgs e)
        {
            try
            {
                int rdprid = 0;
                Button clickedButton = sender as Button;

                if (clickedButton != null)
                    rdprid = Convert.ToInt32(clickedButton.Tag);

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();

                objSearch.Select = @"
            r.ProductId as Id,
            r.Description as Field1,
            pr.SRate as Field2,
            ISNULL(r.Tax,'Exclusive') as Field3,
            ISNULL(ts.TaxSettingsId,0) as Field4, pr.id as Field5";

                objSearch.Table = $"[{GameDB}].dbo.tblRedemptionProducts r " +
                                  $"LEFT JOIN [{GameDB}].dbo.tblProductRate pr ON pr.ProductId = r.ProductId " +
                                  $"LEFT JOIN [{GameDB}].dbo.tblTaxSettings ts ON ts.TaxSettingsId = r.TaxSettingsId";

                objSearch.WhereNdGroupBy = "WHERE r.ProductId = " + rdprid +
                                          " AND pr.ProductType = " + Convert.ToInt32(FoodandBeverage.Interfaces.ProductType.RedemptionTicket) +
                                          " AND pr.BranchId = " + FrmLogin.GameBranchId;

                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);

                if (gridSource != null && gridSource.Count > 0)
                {
                    foreach (var item in gridSource)
                    {
                        AddGiftRow(item, (int)FoodandBeverage.Interfaces.ProductType.RedemptionTicket);
                    }
                    pnlgiftpreview.Visible = false;
                    HideBlurPopup(pnlgiftpreview);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  RedemptionProduct_clcik()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void AddGiftRow(dynamic item, int ptypeno)
        {
            try
            {
                foreach (DataGridViewRow dgRow in dgv_product.Rows)
                {
                    if (dgRow.IsNewRow) continue;

                    int pid = Convert.ToInt32(dgRow.Cells[pProductid].Value ?? 0);
                    int ptype = Convert.ToInt32(dgRow.Cells[PPackProductTypeno].Value ?? 0);
                    if (pid == item.Id && ptype == (int)PackItemType.Gift)
                    {
                        CMessageBox.Show("This product already added!", "Duplicate",
                                         MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                ResetDiscount();

                int rowIndex = dgv_product.Rows.Add();
                var row = dgv_product.Rows[rowIndex];

                row.Cells[pslno].Value = rowIndex + 1;
                row.Cells[pProductid].Value = item.Id;
                row.Cells[pitemno].Value = item.Field1;
                row.Cells[pcategmenuno].Value = "Redemption";
                row.Cells[pqtyno].Value = 1;
                row.Cells[pPrice].Value = Convert.ToDecimal(item.Field2);
                row.Cells[pAmount].Value = Convert.ToDecimal(item.Field2);

                row.Cells[ptax].Value = Convert.ToString(item.Field3);
                row.Cells[ptaxtype].Value = "";
                row.Cells[ptaxper].Value = 0;
                row.Cells[ptaxamt].Value = 0;
                row.Cells[ppretaxamt].Value = Convert.ToDecimal(item.Field2); // Pre-tax = price
                row.Cells[ppridno].Value = item.Field5;

                row.Cells[pigstamt].Value = 0;
                row.Cells[pvatamt].Value = 0;
                row.Cells[pnetamt].Value = Convert.ToDecimal(item.Field2);

                row.Cells[pproducttype].Value = ptypeno;
                row.Cells[proPartyPackDetailsId].Value = 0;
                row.Cells[pPartydeafult].Value = 2;
                row.Cells[ptaxsettingid].Value = Convert.ToInt32(item.Field4);

                row.Cells[PPackProductTypeno].Value = (int)PackItemType.Gift;
                row.Cells[phsncodeno].Value = GetPartyItemHsnCode(
                    (int)PackItemType.Gift, Convert.ToInt32(item.Id));

                // Action column (Remove button)
                DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)row.Cells[pactionno];
                cellcolumntype.Value = Grid_Remove;
                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;

                CalculateAndApplyTax(rowIndex, (int)PackItemType.Gift, Convert.ToInt32(item.Id));
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  AddGiftRow()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        #endregion

        #region Party 
        private void UpdateTotalGuests()
        {
            int adults = 0;
            int children = 0;

            int.TryParse(txtAdults.Text, out adults);
            int.TryParse(txtChild.Text, out children);

            txttotalguest.Text = (adults + children).ToString();
        }

        private void UpdateGrandTotal_Pack()
        {
            try
            {
                decimal adultPrice = 0;
                decimal childPrice = 0;
                int adults = 0;
                int children = 0;

                decimal.TryParse(txtAdultPrice.Text, out adultPrice);
                decimal.TryParse(txtChildPrice.Text, out childPrice);
                int.TryParse(txtAdults.Text, out adults);
                int.TryParse(txtChild.Text, out children);

                decimal grandTotal = _fixedPriceStatus
                    ? _fixedPackPrice
                    : (adultPrice * adults) + (childPrice * children);
                txtGrandTotal_pack.Text = grandTotal.ToString("N2");
                txt_PackTotalAmt.Text = grandTotal.ToString("N2");
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page UpdateGrandTotal_Pack()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        private decimal GetDecimal(TextBox txt)
        {
            try
            {
                decimal.TryParse(txt.Text, out decimal val);
                return val;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page GetDecimal()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));

                return 0;
            }
        }
        private void CalculatePackValues()
        {
            try
            {
                decimal GetDecimal(TextBox txt)
                {
                    decimal.TryParse(txt.Text, out decimal val);
                    return val;
                }

                UpdateGrandTotal_Pack();

                decimal grandTotal = GetDecimal(txtGrandTotal_pack);
                decimal otherAdd = GetDecimal(txtOAAmount);
                decimal otherDeduct = GetDecimal(txtODAmount);

                decimal igst = 0, cgst = 0, sgst = 0, vat = 0;
                decimal totalPercent = 0;
                string taxType = "";
                string taxMode = txttax.Text.Trim(); // Inclusive / Exclusive

                if (!string.IsNullOrEmpty(txtTaxSettingId.Text))
                {
                    int taxSettingId = Convert.ToInt32(txtTaxSettingId.Text);

                    BllSearch<DmnTaxSettings> _bllSearch = new BllSearch<DmnTaxSettings>();
                    DmnSearch dmnSearch = new DmnSearch
                    {
                        Select = "TaxType, IgstPrcnt, CgstPrcnt, SgstPrcnt, VatPrcnt",
                        Table = "tblTaxSettings",
                        WhereNdGroupBy = $"WHERE Status=1 AND TaxSettingsId={taxSettingId}"
                    };

                    var taxSettings = _bllSearch.GetSearchData("prdSearch", dmnSearch);

                    if (taxSettings.Count > 0)
                    {
                        txtTaxtype_pack.Text = taxSettings[0].TaxType;
                        igst = taxSettings[0].IgstPrcnt;
                        cgst = taxSettings[0].CgstPrcnt;
                        sgst = taxSettings[0].SgstPrcnt;
                        vat = taxSettings[0].VatPrcnt;

                        taxType = taxSettings[0].TaxType;
                        totalPercent = taxType == "GST" ? igst : vat;
                    }
                }

                txt_packtaxper.Text = totalPercent.ToString(); 
                txtTaxtype_pack.Text = taxType;

                decimal discountAmt = 0; // you can calculate later if needed
                decimal netBeforeTax = grandTotal - discountAmt + otherAdd - otherDeduct;

                decimal baseAmt = 0, taxAmt = 0, netAmt = 0;

                if (taxMode.Equals("INCLUSIVE", StringComparison.OrdinalIgnoreCase))
                {
                    baseAmt = netBeforeTax / (1 + (totalPercent / 100));
                    taxAmt = netBeforeTax - baseAmt;
                    netAmt = netBeforeTax;
                }
                else
                {
                    baseAmt = netBeforeTax;
                    taxAmt = netBeforeTax * totalPercent / 100;
                    netAmt = netBeforeTax + taxAmt;
                }

                // Round using AwayFromZero
                baseAmt = Math.Round(baseAmt, 2, MidpointRounding.AwayFromZero);
                taxAmt = Math.Round(taxAmt, 2, MidpointRounding.AwayFromZero);
                netAmt = Math.Round(netAmt, 2, MidpointRounding.AwayFromZero);

                txtPreTaxAmount_pack.Text = baseAmt.ToString("0.00");
                txtTotalTaxAmount_pack.Text = taxAmt.ToString("0.00");
                txtGrandTotal_pack.Text = netAmt.ToString("0.00");

                if (taxType == "GST")
                {
                    decimal igstAmt = Math.Round(igst > 0 ? taxAmt : 0, 2, MidpointRounding.AwayFromZero);
                    decimal cgstAmt = Math.Round(cgst > 0 ? taxAmt * (cgst / totalPercent) : 0, 2, MidpointRounding.AwayFromZero);
                    decimal sgstAmt = Math.Round(sgst > 0 ? taxAmt * (sgst / totalPercent) : 0, 2, MidpointRounding.AwayFromZero);

                    txtIGSTAmt_pack.Text = igstAmt.ToString("0.00");
                    txtVatamt_pack.Text = "0.00";
                }
                else
                {
                    txtVatamt_pack.Text = taxAmt.ToString("0.00");
                    txtIGSTAmt_pack.Text = "0.00";
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page CalculatePackValues()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private decimal GetDiscountableBaseAmount()
        {
            decimal discountableBase = 0m;

            // Package amount is already maintained as a tax-exclusive base.
            decimal packBase = 0m;
            decimal.TryParse(txtPreTaxAmount_pack.Text, out packBase);
            discountableBase += packBase;

            foreach (DataGridViewRow row in dgv_product.Rows)
            {
                if (row.IsNewRow) continue;

                int productType = 0;
                int.TryParse(Convert.ToString(row.Cells[PPackProductTypeno].Value ?? 0), out productType);
                int productId = 0;
                int.TryParse(Convert.ToString(row.Cells[pProductid].Value ?? 0), out productId);

                if (productType != (int)PackItemType.Card &&
                    productType != (int)PackItemType.Gift &&
                    productType != (int)PackItemType.Menu &&
                    productType != (int)PackItemType.Cake &&
                    productType != (int)PackItemType.ExtraChargeProduct &&
                    productType != (int)PackItemType.FoodItem &&
                    productType != (int)PackItemType.Venue)
                {
                    continue;
                }

                if (productType == (int)PackItemType.Cake)
                {
                    BllSearch<DmnGridFields> cakeHsnSearch = new BllSearch<DmnGridFields>();
                    DmnSearch cakeHsnQuery = new DmnSearch
                    {
                        Select = "ISNULL(HSNCode,'') AS Field1",
                        Table = "tblPartyPackCake",
                        WhereNdGroupBy = "WHERE PartyPackCakeId=" + productId
                    };
                    var cakeHsn = cakeHsnSearch.GetSearchData("prdSearch", cakeHsnQuery);
                    string cakeHsnCode = cakeHsn != null && cakeHsn.Count > 0
                        ? Convert.ToString(cakeHsn[0].Field1)
                        : string.Empty;
                    if (!string.IsNullOrWhiteSpace(cakeHsnCode))
                        row.Cells[phsncodeno].Value = cakeHsnCode;
                }
                else if (productType != (int)PackItemType.ExtraChargeProduct && productId > 0)
                {
                    string currentHsnCode = Convert.ToString(row.Cells[phsncodeno].Value ?? string.Empty);
                    if (productType != (int)PackItemType.Menu || string.IsNullOrWhiteSpace(currentHsnCode))
                    {
                        string hsnCode = GetPartyItemHsnCode(productType, productId);
                        if (!string.IsNullOrWhiteSpace(hsnCode))
                            row.Cells[phsncodeno].Value = hsnCode;
                    }
                }

                decimal qty = Convert.ToDecimal(row.Cells[pqtyno].Value ?? 0);
                decimal price = Convert.ToDecimal(row.Cells[pPrice].Value ?? 0);
                decimal gross = qty * price;
                string taxMode = Convert.ToString(row.Cells[ptax].Value ?? "");
                decimal taxPercent = Convert.ToDecimal(row.Cells[ptaxper].Value ?? 0);

                if (string.Equals(taxMode, "Inclusive", StringComparison.OrdinalIgnoreCase) && taxPercent > 0m)
                    discountableBase += gross / (1m + taxPercent / 100m);
                else
                    discountableBase += gross;
            }

            return discountableBase;
        }

        public void CalculateDiscountAmount()
        {
            try
            {
                if (string.IsNullOrEmpty(discountper.Text)) discountper.Text = "0";
                if (string.IsNullOrEmpty(txtGrandTotal.Text)) txtGrandTotal.Text = "0";

                decimal discountPer = Convert.ToDecimal(discountper.Text);
                GetGrandTotal_All();

                if (discountPer > 100)
                {
                    CMessageBox.Show("Invalid Discount %", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    discountper.Text = "0";
                    discountper.Focus();
                    return;
                }

                decimal discountableBase = GetDiscountableBaseAmount();
                decimal discountAmt = Math.Round(
                    discountableBase * discountPer / 100m,
                    2,
                    MidpointRounding.AwayFromZero);

                txtTotalDiscountAmt.Text = discountAmt.ToString("0.00");

                CalculateNetAmount();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page  CalculateDiscountAmount()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }
        public void CalculateDiscountPer()
        {
            try
            {
                if (string.IsNullOrEmpty(txtTotalDiscountAmt.Text)) txtTotalDiscountAmt.Text = "0";
                if (string.IsNullOrEmpty(txtGrandTotal.Text)) txtGrandTotal.Text = "0";

                decimal discountAmt = Convert.ToDecimal(txtTotalDiscountAmt.Text);
                GetGrandTotal_All();

                decimal discountableBase = GetDiscountableBaseAmount();
                if (discountAmt > discountableBase)
                {
                    CMessageBox.Show("Invalid Discount Amount", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTotalDiscountAmt.Text = "0";
                    txtTotalDiscountAmt.Focus();
                    return;
                }

                decimal discountPer = discountableBase == 0m ? 0m : (discountAmt / discountableBase * 100m);
                discountPer = Math.Round(discountPer, 2, MidpointRounding.AwayFromZero);

                discountper.Text = discountPer.ToString("");

                CalculateNetAmount();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page CalculateDiscountPer()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void txtOAAmount_TextChanged(object sender, EventArgs e) => CalculateNetAmount();
        private void txtRcvAmt_TextChanged(object sender, EventArgs e)
        {
            GetDecimal(txtRcvAmt);
            CalculateNetAmount();
        }
        private void txtODAmount_TextChanged(object sender, EventArgs e) => CalculateNetAmount();

        #endregion

        #region Cake
        private void btncakeclose_Click(object sender, EventArgs e)
        {
            pnlcake.Visible = false;
            HideBlurPopup(pnlcake);
        }
        private void ResetCakeFields()
        {
            txtcakeremarks.Text = "";
            txtcakemessage.Text = "";
            txtcakeshape.Text = "";
            txtcakeprice.Text = "0";
        }
        private void btnaddcakes_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow dgRow in dgv_product.Rows)
                {
                    if (dgRow.IsNewRow) continue;

                    int productType = Convert.ToInt32(dgRow.Cells[PPackProductTypeno].Value ?? 0);
                    int productId = Convert.ToInt32(dgRow.Cells[pProductid].Value ?? 0);

                    if (productId == _cakeId && productType == (int)PackItemType.Cake)
                    {
                        CMessageBox.Show("This cake is already added!", "Duplicate",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                ResetDiscount();

                _cakeMessage = txtcakemessage.Text;
                _cakeRemarks = txtcakeremarks.Text;

                decimal qty = 1;

                int rowIndex = dgv_product.Rows.Add();
                DataGridViewRow row = dgv_product.Rows[rowIndex];

                row.Cells[pslno].Value = rowIndex + 1;
                row.Cells[pitemno].Value = lblcakename.Text;
                row.Cells[pcategmenuno].Value = "Cake";
                row.Cells[pqtyno].Value = qty;
                row.Cells[pPrice].Value = _cakePrice;
                row.Cells[pAmount].Value = _cakePrice;
                row.Cells[pProductid].Value = _cakeId;
                row.Cells[pmessageno].Value = _cakeMessage;
                row.Cells[premarksno].Value = _cakeRemarks;
                row.Cells[PPackProductTypeno].Value = (int)PackItemType.Cake;
                row.Cells[phsncodeno].Value = _cakeHsnCode;

                row.Cells[ptaxamt].Value = 0;
                row.Cells[ptax].Value = _cakeTaxMode;
                row.Cells[ptaxtype].Value = _cakeTaxType;
                row.Cells[ptaxper].Value = _cakeTaxPer;
                row.Cells[ptaxsettingid].Value = _cakeTaxSettingsId;
                row.Cells[ppretaxamt].Value = _cakePrice;
                row.Cells[pnetamt].Value = _cakePrice;
                row.Cells[pigstamt].Value = 0;
                row.Cells[pvatamt].Value = 0;
                row.Cells[pPartydeafult].Value = 2;

                DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)row.Cells[pactionno];
                cellcolumntype.Value = Grid_Remove;
                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;

                CalculateAndApplyTax(rowIndex, (int)PackItemType.Cake, Convert.ToInt32(_cakeId));

                ResetCakeFields();
                LoadAllcakeTotal();

                pnl_cakeregister.Visible = false;
                HideBlurPopup(pnl_cakeregister);

                ResetDiscount();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btnaddcakes_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void btncake_refresh_Click(object sender, EventArgs e)
        {
            ResetCakeFields();
        }

        private void LoadCakeList()
        {
            try
            {
                flp_cake.Controls.Clear();

                BllSearch<DmnPartyPackCake> bllSearch = new BllSearch<DmnPartyPackCake>();
                DmnSearch objSearch = new DmnSearch
                {
                    Select = "PartyPackCakeId, CakeType",
                    Table = "tblPartyPackCake",
                    WhereNdGroupBy = "WHERE Status = 1",
                    OrderBy = "ORDER BY CakeType"
                };

                var cakeList = bllSearch.GetSearchData("prdSearch", objSearch);

                if (cakeList.Count > 0)
                {
                    foreach (var cake in cakeList)
                    {
                        Button btn = new Button
                        {
                            Text = cake.CakeType,
                            Tag = cake.PartyPackCakeId,
                            Width = 120,
                            Height = 40,
                            Margin = new Padding(5),
                            BackColor = Color.LightPink,
                            FlatStyle = FlatStyle.Flat,
                            Font = new Font("Segoe UI", 10, FontStyle.Bold)
                        };

                        btn.Click += (s, e) =>
                        {
                            Button clickedBtn = s as Button;
                            int cakeId = Convert.ToInt32(clickedBtn.Tag);

                            LoadCakeDetails(cakeId);

                            // Show panel centered
                            pnl_cakeregister.Visible = true;
                            pnl_cakeregister.Visible = true;ShowBlurPopup(pnl_cakeregister);
                            pnl_cakeregister.BringToFront();
                            pnl_cakeregister.Left = (this.ClientSize.Width - pnl_cakeregister.Width) / 2;
                            pnl_cakeregister.Top = (this.ClientSize.Height - pnl_cakeregister.Height) / 2;
                        };

                        flp_cake.Controls.Add(btn);
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page LoadCakeList()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void LoadCakeDetails(int cakeId)
        {
            try
            {
                BllSearch<DmnPartyPackCake> bll = new BllSearch<DmnPartyPackCake>();

                DmnSearch search = new DmnSearch
                {
                    Select = "*",
                    Table = "tblPartyPackCake",
                    WhereNdGroupBy = "WHERE PartyPackCakeId = " + cakeId
                };

                var list = bll.GetSearchData("prdSearch", search);

                if (list.Count > 0)
                {
                    var item = list[0];

                    lblcakename.Text = item.CakeType;

                    // Basic
                    _cakeId = item.PartyPackCakeId;
                    _cakeSize = item.CakeSize;
                    _cakeShape = item.CakeShape;
                    _cakePrice = Convert.ToDecimal(item.Price);
                    _cakeHsnCode = Convert.ToString(item.HSNCode);

                    // Tax
                    _cakeTaxType = item.TaxType;             // GST / VAT
                    _cakeTaxSettingsId = item.TaxSettingsId;
                    _cakeTaxMode = item.Tax;                 // Inclusive / Exclusive

                    // UI
                    txtcakesize.Text = _cakeSize;
                    txtcakeshape.Text = _cakeShape;
                    txtcakeprice.Text = _cakePrice.ToString("0.00");

                    txtcakemessage.Clear();
                    txtcakeremarks.Clear();
                }
            }

            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page LoadCakeDetails()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void btncake_Click(object sender, EventArgs e)
        {
            if (!checkPartySelet())
                return;

            pnlcake.Visible = true;
            pnlcake.Visible = true;ShowBlurPopup(pnlcake);
            pnlcake.BringToFront();
            pnlcake.Left = (this.ClientSize.Width - pnlcake.Width) / 2;
            pnlcake.Top = (this.ClientSize.Height - pnlcake.Height) / 2;

            LoadCakeList();
        }

        private void btncakesubmit_Click(object sender, EventArgs e)
        {
            pnlcake.Visible = false;
            HideBlurPopup(pnlcake);
        }
        public void LoadAllcakeTotal()
        {
            try
            {
                decimal cakeTotal = 0;

                foreach (DataGridViewRow row in dgv_product.Rows)
                {
                    if (row.IsNewRow) continue;

                    int productType = Convert.ToInt32(row.Cells[PPackProductTypeno].Value ?? 0);
                    if (productType != (int)PackItemType.Cake) continue;

                    decimal net = 0;
                    decimal.TryParse(Convert.ToString(row.Cells[pnetamt].Value), out net);
                    cakeTotal += net;
                }

                txtCakeGrandTotal.Text = cakeTotal.ToString("0.00");
                decimal mainTotal = 0;
                decimal.TryParse(txtGrandTotal.Text, out mainTotal);

                CalculateNetAmount();

                pnlcake.Visible = false;
                HideBlurPopup(pnlcake);
                ResetCakeFields();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page LoadAllcakeTotal()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        #endregion

        #region Common
        public void ToggleFirstRow(int mode)
        {
            try
            {
                tlp_textboxes.ColumnCount = 2;
                tlp_textboxes.ColumnStyles[0].SizeType = SizeType.Percent;
                tlp_textboxes.ColumnStyles[1].SizeType = SizeType.Percent;
                tlp_textboxes.ColumnStyles[0].Width = 50;
                tlp_textboxes.ColumnStyles[1].Width = 50;

                if (mode == 1)
                {
                    tlp_textboxes.RowCount = 4;

                    for (int i = 0; i < 4; i++)
                    {
                        tlp_textboxes.RowStyles[i].SizeType = SizeType.Percent;
                        tlp_textboxes.RowStyles[i].Height = 25f;
                    }

                    foreach (Control c in tlp_textboxes.Controls)
                    {
                        if (tlp_textboxes.GetRow(c) == 0)
                            c.Visible = true;
                    }
                }
                else
                {
                    tlp_textboxes.RowStyles[0].Height = 0f;

                    for (int i = 1; i < 4; i++)
                    {
                        tlp_textboxes.RowStyles[i].SizeType = SizeType.Percent;
                        tlp_textboxes.RowStyles[i].Height = 33.33f;
                    }

                    foreach (Control c in tlp_textboxes.Controls)
                    {
                        if (tlp_textboxes.GetRow(c) == 0)
                            c.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page ToggleFirstRow()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        private string GetCategoryName(int fcid)
        {
            try
            {
                string categoryName = "";
                BllSearch<DmnCategory> bll = new BllSearch<DmnCategory>();
                DmnSearch search = new DmnSearch();
                search.Select = "CategoryName";
                search.Table = "tblCategory_FTI";
                search.WhereNdGroupBy = "WHERE Status = 1 AND CategoryType = 5 AND Fcid = " + fcid;
                var list = bll.GetSearchData("prdSearch", search);
                if (list != null && list.Count > 0)
                    categoryName = Convert.ToString(list[0].CategoryName);

                return categoryName;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page ToggleFirstRow()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
                return "";
            }
        }
        private void LoadPackRelatedItems(int partyPackId)
        {
            try
            {
                dgv_product.Rows.Clear();

                BllSearch<DmnPartyPackDetails> bll = new BllSearch<DmnPartyPackDetails>();
                DmnSearch search = new DmnSearch();

                search.Select = @"PartyPackDetailsId,
                          PartyPackId,
                          Item,
                          Quantity,
                          PackItemType,
                          ProductId,
                          ProductType,
                          MenuCategoryId";

                search.Table = "tblPartyPackDetails";
                search.WhereNdGroupBy = "WHERE Status = 1 AND PartyPackId = " + partyPackId;

                var list = bll.GetSearchData("prdSearch", search);

                int sl = 1;

                foreach (var item in list)
                {
                    int rowIndex = dgv_product.Rows.Add();
                    DataGridViewRow row = dgv_product.Rows[rowIndex];

                    row.Cells[pslno].Value = sl++;
                    row.Cells[pitemno].Value = item.Item;
                    row.Cells[pqtyno].Value = item.Quantity;

                    if (item.MenuCategoryId != null)
                        row.Cells[pcategmenuno].Value = item.MenuCategoryId;

                    row.Cells[ppridno].Value = item.Productid;

                    row.Cells[PPackProductTypeno].Value = item.PackItemType;

                    row.Cells[proPartyPackDetailsId].Value =0;
                    row.Cells[pPartydeafult].Value = 1;

                    row.ReadOnly = true;

                    // Default package color
                    row.DefaultCellStyle.BackColor = Color.Gainsboro;
                    row.DefaultCellStyle.ForeColor = Color.Black;

                    DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_product.Rows[rowIndex].Cells[pactionno];

                    cellcolumntype.Value = Blankimage;
                    cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page LoadPackRelatedItems()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void btndispanel_Click(object sender, EventArgs e)
        {
            try
            {
                decimal kdata = 0;
                using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.percentage)))
                {
                    frm.ShowDialog();

                    if (!string.IsNullOrEmpty(frm.txtkeypadvalue.Text))
                        kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);

                    txtdiscountreason.Text = frm.txtdesc.Text.Trim() == "Enter Remarks Here....." ? "" : frm.txtdesc.Text;
                }

                if (Frmnumdiskeypad.flagpercentage)
                {
                    discountper.Text = Math.Round(kdata, 2, MidpointRounding.AwayFromZero).ToString();
                    CalculateDiscountAmount();
                }
                else
                {
                    txtTotalDiscountAmt.Text = Math.Round(kdata, 2, MidpointRounding.AwayFromZero).ToString();
                    CalculateDiscountPer();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btndispanel_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void txtadults_TextChanged(object sender, EventArgs e)
        {
            PriceChangecalculation();
        }
        public void PriceChangecalculation()
        {
            try
            {
                UpdateTotalGuests();
                UpdateGrandTotal_Pack();

                // A fixed-price package is charged at FixedPrice regardless of the
                // configured adult/child counts. Do not run the per-guest pack pricing
                // calculation for it; add-on rows still use the normal tax/price flow.
                if (!_fixedPriceStatus)
                    CalculatePackValues();

                if (Convert.ToDecimal(discountper.Text) > 0)
                    CalculateDiscountAmount();
                else if (Convert.ToDecimal(txtTotalDiscountAmt.Text) > 0)
                    CalculateDiscountPer();
                else
                    CalculateDiscountAmount();

                CalculateNetAmount();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page PriceChangecalculation()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void txtchild_TextChanged(object sender, EventArgs e)
        {
            PriceChangecalculation();
        }

        public void Memoryrelease()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void ClearAll()
        {
            try
            {
                _fixedPriceStatus = false;
                _fixedPackPrice = 0m;
                txtAdults.Enabled = true;
                txtChild.Enabled = true;
                txtAdults.ReadOnly = false;
                txtChild.ReadOnly = false;
                txtAdults.TabStop = true;
                txtChild.TabStop = true;
                _isKotDateTimeManuallySet = false;
                _hasLoadedKotDateTime = false;
                _loadedKotDateTime = null;
                _loadedConsumedRawMaterialStatus = false;
                _loadedKoTPrintStatus = false;

                // ===============================
                // 🔹 Reset Event Info
                // ===============================
                dtpEventDate_From.Value = DateTime.Now.Date;
                dtpEventTime_From.Value = DateTime.Now;

                txteventdesc.Text = "";
                cmbevent.Text = "";
                txtaddress.Text = "";
                txtemail.Text = "";
                txtph.Text = "";
                txtname.Text = "";

                txtpartybookingid.Text = "0";

                // ===============================
                // 🔹 Reset Order/Add Charges
                // ===============================
                oadesc.Text = "";
                txtOAAmount.Text = "0.00";

                oddesc.Text = "";
                txtODAmount.Text = "0.00";

                txtaddcharge.Text = "0";
                txtextracharge_des.Text = "";
                txtextracharge_amt.Text = "0";

                txtPreTaxAmount_extracharge.Text = "0.00";
                txtVatamt_extracharge.Text = "0.00";
                txtIGSTAmt_extracharge.Text = "0.00";
                txtExtrachargeGrandTotal.Text = "0.00";
                txtTotalTaxAmount_extracharge.Text = "0.00";

                // ===============================
                // 🔹 Reset Discount & Tax
                // ===============================
                txtTotalDiscountAmt.Text = "0.00";
                discountper.Text = "0.00";
                txtDiscountPer.Text = "0.00";
                txttax.Text = "";
                txtTotalTaxAmount.Text = "0.00";
                txtPreTaxAmount.Text = "0.00";

                // ===============================
                // 🔹 Reset Grand Totals
                // ===============================
                txtGrandTotal.Text = "0.00";
                txtCakeGrandTotal.Text = "0.00";
                txtCardGrandTotal.Text = "0.00";
                txtGiftGrandTotal.Text = "0.00";
                txtGrandTotal_pack.Text = "0.00";

                txtPreTaxAmount_cake.Text = "0.00";
                txtPreTaxAmount_card.Text = "0.00";
                txtPreTaxAmount_gift.Text = "0.00";
                txtPreTaxAmount_pack.Text = "0.00";

                txtTotalTaxAmount_cake.Text = "0.00";
                txtTotalTaxAmount_card.Text = "0.00";
                txtTotalTaxAmount_gift.Text = "0.00";
                txtTotalTaxAmount_pack.Text = "0.00";

                txtIGSTAmt_pack.Text = "0.00";
                txtVatamt_pack.Text = "0.00";

                txtIGSTAmt_cake.Text = "0.00";
                txtVATAmt_cake.Text = "0.00";

                txtIGSTAmt_card.Text = "0.00";
                txtVATAmt_card.Text = "0.00";

                txtIGSTAmt_gift.Text = "0.00";
                txtVATAmt_gift.Text = "0.00";

                txtTotalIGSTAmt.Text = "0.00";
                txtTotalVATAmt.Text = "0.00";

                txtTaxSettingId.Text = "0";

                sterms.Text = "";

                // ===============================
                // 🔹 Reset Guest Info
                // ===============================
                txtChild.Text = "";
                txtAdults.Text = "";
                txttotalguest.Text = "";
                txtChildPrice.Text = "0.00";
                txtAdultPrice.Text = "0.00";

                lbltotalguest.Visible = false;
                txttotalguest.Visible = false;

                //tlp_adult_child_label.Visible = false;
                //tlp_adult_child_text.Visible = false;

                // ===============================
                // 🔹 Reset Payment Info
                // ===============================
                lblNetAmt.Text = "0.00";
                lblBalAmt.Text = "0.00";
                lblbalapay.Text = "Balance Payable";
                lbl_Paymentsettled.Text = "Payment Settlement";
                lbladvamtdata.Text = "0.00";
                txtRcvAmt.Text = "0.00";

                dbReceivedAmount = 0;
                dbAdvanceAmount = 0;

                AdvanceFlag = false;
                PaymentSettleStatusData = 0;

                TogglePayCancel(0);

                // ===============================
                // 🔹 Reset IDs & Counters
                // ===============================
                BookingId = 0;
                SalesMasterId = 0;
                PartyBookingDetailsId = 0;
                Partypacksaledetailid = 0;
                MinimumAdvance = 0;

                CurrentPackId = 0;
                Partypackid = 0;

                partPayments.Clear();
                partCount = 0;

                // ===============================
                // 🔹 Reset Cake Fields
                // ===============================
                _cakeId = 0;
                _cakeSize = "";
                _cakeShape = "";
                _cakeMessage = "";
                _cakeRemarks = "";
                _cakePrice = 0;

                _cakeTaxType = "";
                _cakeTaxPer = 0;
                _cakeTaxSettingsId = 0;
                _cakeTaxMode = "";

                _cakePreTaxAmount = 0;
                _cakeTaxAmount = 0;
                _cakeNetAmount = 0;

                // ===============================
                // 🔹 Clear Lists
                // ===============================
                packDetailsList?.Clear();
                categoryList?.Clear();
                selectedCustomItems.Clear();
                customMenuList.Clear();
                bookingItemAdjustments.Clear();

                // ===============================
                // 🔹 Clear Grids
                // ===============================
                dgv_cus_menu_items.Rows.Clear();
                dgv_packenter.Rows.Clear();
                dgv_packs_items.Rows.Clear();
                dgv_product.Rows.Clear();
                dgv_cancelleditems.Rows.Clear();

                // ===============================
                // 🔹 Reset Panels Visibility
                // ===============================
                pnlcake.Visible = false;
                HideBlurPopup(pnlcake);
                pnlgiftpreview.Visible = false;
                HideBlurPopup(pnlgiftpreview);
                pnlSplit.Visible = false;
                HideBlurPopup(pnlSplit);
                pnl_CustomItems_menu.Visible = false;
                HideBlurPopup(pnl_CustomItems_menu);
                pnl_enter_paks.Visible = false;
                HideBlurPopup(pnl_enter_paks);
                pnl_foodpacks.Visible = false;
                HideBlurPopup(pnl_foodpacks);
                pnl_pak_detailsitems.Visible = false;
                HideBlurPopup(pnl_pak_detailsitems);
                pnlPaymode.Visible = false;
                HideBlurPopup(pnlPaymode);
                pnl_Paymentsettled.Visible = false;
                HideBlurPopup(pnl_Paymentsettled);

                lblpackname.Visible = false;
                lblpacklabel.Visible = false;

                Frmnumdiskeypad.flagpercentage = false;

                // ===============================
                // 🔹 Reset Internal Totals
                // ===============================
                dbNetAmount = 0;
                dbBalanceAmount = 0;
                originalGrandTotal = 0;
                originalTotalTax = 0;

                txtdeafultpackid.Text = "0";

                // ===============================
                // 🔹 Reset First Row / UI toggles
                // ===============================
                ToggleFirstRow(0);

                // Focus
                txtAdults.Focus();

                lblBalAmt.Text = "0";

                SetupGuestControls();

                Fullpaymentflag = false;
                Advanceflag = false;

                pnl_PaymentOptions.Visible = false;
                HideBlurPopup(pnl_PaymentOptions);

                btnnocharge.Visible = true;
                btn_other.Visible = true;
                btncomple.Visible = true;

                txt_packtaxper.Text = "";
                txtTaxtype_pack.Text = "";
                txt_PackTotalAmt.Text = "0";
                _partyPackHsnCode = "";
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page ClearAll()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        #endregion

        #region Reegistartion event And Fuctions
        private void txtph_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == '\r')
                    e.Handled = true;

                if (char.IsControl(e.KeyChar))
                    return;

                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page txtph_KeyPress()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void txtph_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string digitsOnly = new string(txtph.Text.Where(char.IsDigit).ToArray());

                if (txtph.Text != digitsOnly)
                {
                    int selectionStart = txtph.SelectionStart;
                    txtph.Text = digitsOnly;
                    txtph.SelectionStart = Math.Min(selectionStart, txtph.Text.Length);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page txtph_TextChanged()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                return System.Text.RegularExpressions.Regex.IsMatch(
                    email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase
                );
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page IsValidEmail()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));

                return false;
            }
        }

        private void txtemail_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtemail.Text))
                {
                    if (!IsValidEmail(txtemail.Text))
                    {
                        CMessageBox.Show("Please enter a valid email address");
                        txtemail.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page txtemail_Leave()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void OnlyInteger_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (char.IsControl(e.KeyChar))
                    return;

                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page OnlyInteger_KeyPress()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        #endregion

        #region Payment Realted Fuction event
        private void btn_paynow_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Mandatorycheck())
                    return;

                btnnocharge.Visible = true;
                btn_other.Visible = true;
                btncomple.Visible = true;

                decimal rcvAmt = decimal.TryParse(txtRcvAmt.Text, out decimal ra) ? ra : 0;

                // Check booking
                if (!string.IsNullOrEmpty(txtpartybookingid.Text) &&
                    int.TryParse(txtpartybookingid.Text, out int bookingId) &&
                    bookingId > 0)
                {
                    decimal lblNet = decimal.TryParse(lblNetAmt.Text, out decimal na) ? na : 0;
                    decimal advancePaid = decimal.TryParse(lbladvamtdata.Text, out decimal aa) ? aa : 0;

                    decimal totalAdjustment = bookingItemAdjustments.Sum(a => a.Amount);

                    decimal newNetAmount = lblNet;
                    decimal totalPaid = advancePaid + rcvAmt;

                    decimal refundAmount = totalPaid - newNetAmount;   // >0 → refund
                    decimal finalBalance = newNetAmount - totalPaid;   // >0 → pending

                    bool isFullySettled = Math.Abs(newNetAmount - totalPaid) < 0.01m;
                    bool refundRequired = refundAmount > 0;
                    bool paymentPending = finalBalance > 0;

                    // ✅ 1. FULLY SETTLED (NO PAYMENT / NO REFUND)
                    if (isFullySettled)
                    {
                        ShowPaymentSettlementPanel(lblNet, advancePaid, rcvAmt, refundAmount);
                        return;
                    }

                    // ✅ 2. REFUND CASE
                    if (refundRequired)
                    {
                        btnnocharge.Visible = false;
                        btn_other.Visible = false;
                        btncomple.Visible = false;

                        ShowPaymentSettlementPanel(lblNet, advancePaid, rcvAmt, refundAmount);
                        return;
                    }

                    // ✅ 3. PAYMENT REQUIRED
                    if (paymentPending)
                    {
                        pnl_PaymentOptions.Visible = true;
                        pnl_PaymentOptions.Visible = true;ShowBlurPopup(pnl_PaymentOptions);
                        pnl_PaymentOptions.BringToFront();
                        pnl_PaymentOptions.Left = (this.ClientSize.Width - pnl_PaymentOptions.Width) / 2;
                        pnl_PaymentOptions.Top = (this.ClientSize.Height - pnl_PaymentOptions.Height) / 2;
                        return;
                    }
                }

                pnl_PaymentOptions.Visible = true;
                pnl_PaymentOptions.Visible = true;ShowBlurPopup(pnl_PaymentOptions);
                pnl_PaymentOptions.BringToFront();
                pnl_PaymentOptions.Left = (this.ClientSize.Width - pnl_PaymentOptions.Width) / 2;
                pnl_PaymentOptions.Top = (this.ClientSize.Height - pnl_PaymentOptions.Height) / 2;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btn_paynow_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        public bool Mandatorycheck()
        {
            try
            {
                decimal adultPrice = 0, childPrice = 0;
                decimal.TryParse(txtAdultPrice.Text, out adultPrice);
                decimal.TryParse(txtChildPrice.Text, out childPrice);

                // Check Name
                if (string.IsNullOrWhiteSpace(txtname.Text))
                {
                    CMessageBox.Show("Please enter Name", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtname.Focus();
                    return false;
                }

                // Check Phone
                if (string.IsNullOrWhiteSpace(txtph.Text))
                {
                    CMessageBox.Show("Please enter Phone Number", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtph.Focus();
                    return false;
                }

                string phone = txtph.Text.Trim();
                if (phone.Length < 10)
                {
                    CMessageBox.Show("Phone Number must be at least 10 digits", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtph.Focus();
                    return false;
                }

                if (string.IsNullOrWhiteSpace(cmbevent.Text))
                {
                    CMessageBox.Show("Please select an Event Type", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbevent.Focus();
                    return false;
                }

                if(Partypackid==0 )/*if (adultPrice <= 0 && childPrice <= 0)*/ 
                {
                    CMessageBox.Show("Please select a Party Pack", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (adultPrice > 0 || childPrice > 0)
                {
                    int adults = GetInt(txtAdults);
                    int children = GetInt(txtChild);

                    if (adults == 0 && children == 0)
                    {
                        CMessageBox.Show("Please enter number of attendees (Adults or Children)", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtAdults.Focus();
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btn_paynow_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
                return false;
            }
        }
        public void PaymentProcess()
        {
            try
            {

                decimal rcvAmt = 0;
                decimal.TryParse(txtRcvAmt.Text, out rcvAmt);

                if (!string.IsNullOrEmpty(txtpartybookingid.Text) &&
                    int.TryParse(txtpartybookingid.Text, out int bookingId) &&
                    bookingId > 0)
                {
                    decimal lblNet = decimal.TryParse(lblNetAmt.Text, out decimal na) ? na : 0;
                    decimal advancePaid = decimal.TryParse(lbladvamtdata.Text, out decimal aa) ? aa : 0;
                    decimal balamt = decimal.TryParse(lblBalAmt.Text, out decimal ba) ? ba : 0;

                    decimal totalAdjustment = bookingItemAdjustments.Sum(a => a.Amount);
                    decimal newNetAmount = lblNet + totalAdjustment;
                    decimal totalPaid = advancePaid + rcvAmt;

                    decimal refundAmount = totalPaid - newNetAmount;   // >0 = refund
                    decimal finalBalance = newNetAmount - totalPaid;   // >0 = pending

                    bool adjustmentExists = bookingItemAdjustments.Count > 0;
                    bool netChanged = newNetAmount != dbNetAmount;
                    bool paymentChanged = balamt > 0;
                    bool refundRequired = refundAmount > 0;

                    //  OTHER CASES (SETTLEMENT / REFUND / ADJUSTMENT)
                    if (adjustmentExists || netChanged || paymentChanged || refundRequired)
                    {
                        ShowPaymentSettlementPanel(lblNet, advancePaid, rcvAmt, refundAmount);
                        return;
                    }
                }


                // NORMAL PAYMENT MODE PANEL

                pnlPaymode.Visible = true;
                pnlPaymode.Visible = true;ShowBlurPopup(pnlPaymode);
                pnlPaymode.BringToFront();

                pnlPaymode.Left = (this.ClientSize.Width - pnlPaymode.Width) / 2;
                pnlPaymode.Top = (this.ClientSize.Height - pnlPaymode.Height) / 2;
            }

            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page PaymentProcess()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));

            }
        }



        private void btncash_Click(object sender, EventArgs e)
        {
            try
            {
                Button clickedButton = sender as Button;
                switch (clickedButton.Name)
                {
                    case "btncash":
                        PaymentMode = Convert.ToInt32(PaymentType.CASH);
                        break;
                    case "btndebit":
                        PaymentMode = Convert.ToInt32(PaymentType.DEBITCARD);
                        break;
                    case "btnupi":
                        PaymentMode = Convert.ToInt32(PaymentType.UPI);
                        break;
                    case "btnnetbanking":
                        PaymentMode = Convert.ToInt32(PaymentType.BANKINGONLINE);
                        break;
                    case "btncreditcard":
                        PaymentMode = Convert.ToInt32(PaymentType.CREDITCARD);
                        break;
                    case "btncomple":
                        PaymentMode = Convert.ToInt32(PaymentType.COMPLEMENTRY);
                        break;
                    case "btnnocharge":
                        PaymentMode = Convert.ToInt32(PaymentType.Nocharge);
                        break;
                }
                Paybill();

            }

            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btncash_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private int GetInt(TextBox txt)
        {
            if (txt == null || string.IsNullOrWhiteSpace(txt.Text))
                return 0;

            int.TryParse(txt.Text, out int val);
            return val;
        }
        private decimal GetDecimalFromLabel(Label lbl)
        {
            if (lbl == null || string.IsNullOrWhiteSpace(lbl.Text))
                return 0;

            decimal.TryParse(lbl.Text, out decimal val);
            return val;
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
                clsgen.LogException("Partybooking page setperfixsale()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        public void Paybill()
        {
            try
            {
                int bookingId = 0;

                GetInt(txtpartybookingid);
                GetInt(txtdeafultpackid);
                GetDecimal(txtaddcharge);
                GetDecimal(txt_packtaxper);
                
                BllPartyBooking bll = new BllPartyBooking();
                DmnPartyBooking booking = new DmnPartyBooking();

                #region BOOKING MASTER AND DETAILS

                #region PARTY MASTER

                booking.PartyBookingId = GetInt(txtpartybookingid);
                booking.PartyPackId = Partypackid;

                // 🔹 CUSTOMER INFO
                booking.CustomerName = txtname.Text?.Trim();
                booking.Phone = txtph.Text?.Trim();
                booking.Email = txtemail.Text?.Trim();
                booking.Address = txtaddress.Text?.Trim();

                // 🔹 EVENT INFO
                booking.EventType = Convert.ToInt32(cmbevent.SelectedValue);
                booking.EventDescription = txteventdesc.Text?.Trim();

                // 🔹 DATEtime

                DateTime bookingStart = GetOptionalDateTimePickerValue("dtpEventDate_From").HasValue
                    ? GetOptionalDateTimePickerValue("dtpEventDate_From").Value.Date +
                      (GetOptionalDateTimePickerValue("dtpEventTime_From") ?? dtpEventTime_From.Value).TimeOfDay
                    : dtpEventDate_From.Value.Date + dtpEventTime_From.Value.TimeOfDay;

                DateTime? kotDateTime;
                if (!TryGetKotDateTimeForSave(bookingStart, out kotDateTime))
                    return;

                DateTime? bookingEndDate = GetOptionalDateTimePickerValue("dtpEventDate_To");
                DateTime? bookingEndTime = GetOptionalDateTimePickerValue("dtpEventTime_To");
                DateTime bookingEnd = bookingEndDate.HasValue
                    ? bookingEndDate.Value.Date + (bookingEndTime ?? dtpEventTime_From.Value).TimeOfDay
                    : (_calendarBookingEnd ?? bookingStart);

                booking.BookingDateTime = bookingStart;

                var endProperty = booking.GetType().GetProperty("BookingToDateTime");
                if (endProperty != null && endProperty.CanWrite)
                {
                    Type propertyType = Nullable.GetUnderlyingType(endProperty.PropertyType) ?? endProperty.PropertyType;
                    object endValue = propertyType == typeof(DateTime) ? (object)bookingEnd : null;
                    if (endValue != null)
                        endProperty.SetValue(booking, endValue, null);
                }

                // 🔹 PEOPLE COUNT
                booking.NoOfAdults = GetInt(txtAdults);
                booking.NoOfChildren = GetInt(txtChild);

                // 🔹 PACK PRICE
                decimal adultPrice = GetDecimal(txtAdultPrice);
                decimal childPrice = GetDecimal(txtChildPrice);

                if (booking.NoOfAdults > 0 && booking.NoOfChildren > 0)
                    booking.PackPrice = adultPrice + childPrice;
                else if (booking.NoOfAdults > 0)
                    booking.PackPrice = adultPrice;
                else if (booking.NoOfChildren > 0)
                    booking.PackPrice = childPrice;
                else
                    booking.PackPrice = 0;

                // 🔹 TAX
                booking.Tax = txttax.Text;
                booking.TaxType = txtTaxtype_pack.Text;
                booking.TaxPer =Convert.ToDecimal(txt_packtaxper.Text);
                booking.TaxAmount = GetDecimal(txtTotalTaxAmount);

                // 🔹 GST / VAT
                decimal igst = GetDecimal(txtTotalIGSTAmt);
                booking.IGSTAmt = igst;
                booking.CGSTAmt = igst / 2;
                booking.SGSTAmt = igst / 2;
                booking.VATAmt = GetDecimal(txtTotalVATAmt);

                // 🔹 DISCOUNT
                booking.DiscountPer = GetDecimal(txtDiscountPer);
                booking.DiscountAmount = GetDecimal(txtTotalDiscountAmt);
                booking.DiscountReason = txtdiscountreason.Text ?? "";

                // 🔹 AMOUNTS
                booking.PreTaxAmount = GetDecimal(txtPreTaxAmount);
                booking.PostTaxAmount = GetDecimal(txtTotalTaxAmount);
                booking.NetAmount = GetDecimalFromLabel(lblNetAmt);

                // 🔹 PAYMENT
                decimal currentReceived = GetDecimal(txtRcvAmt), totalAdvance = 0;
                booking.ReceivedAmount = currentReceived;

                decimal lblBalance = decimal.TryParse(lblBalAmt.Text, out decimal bal) ? bal : 0;

                if (lblBalance == 0 || PaymentSettleStatusData == Convert.ToInt32(PaymentSettlementStatus.RefundDue) || PaymentSettleStatusData == Convert.ToInt32(PaymentSettlementStatus.AdditionalDue) || Fullpaymentflag)
                {
                    booking.AdvanceAmount = 0;
                    booking.ReceivedAmount = 0;
                    booking.BalanceAmount = 0;
                }
                else
                {
                    totalAdvance = dbAdvanceAmount + currentReceived;

                    if (totalAdvance >= booking.NetAmount)
                    {
                        booking.AdvanceAmount = booking.NetAmount;
                        booking.BalanceAmount = 0;
                    }
                    else
                    {
                        booking.AdvanceAmount = totalAdvance;
                        booking.BalanceAmount = booking.NetAmount - totalAdvance;
                    }
                }

                booking.PaymentMode = 0;

                // 🔹 CATEGORY TOTALS
                booking.CakeGrandTotal = GetDecimal(txtCakeGrandTotal);
                booking.CakePreTaxAmount = GetDecimal(txtPreTaxAmount_cake);
                booking.CakeTaxAmount = GetDecimal(txtTotalTaxAmount_cake);

                booking.CardGrandTotal = GetDecimal(txtCardGrandTotal);
                booking.CardPreTaxAmount = GetDecimal(txtPreTaxAmount_card);
                booking.CardTaxAmount = GetDecimal(txtTotalTaxAmount_card);

                booking.GiftGrandTotal = GetDecimal(txtGiftGrandTotal);
                booking.GiftPreTaxAmount = GetDecimal(txtPreTaxAmount_gift);
                booking.GiftTaxAmount = GetDecimal(txtTotalTaxAmount_gift);

                booking.PackGrandTotal = GetDecimal(txtGrandTotal_pack);
                booking.PackPreTaxAmount = GetDecimal(txtPreTaxAmount_pack);
                booking.PackTaxAmount = GetDecimal(txtTotalTaxAmount_pack);

                booking.ExtraChargeGrandTotal = GetDecimal(txtExtrachargeGrandTotal);
                booking.ExtraChargePreTaxAmount = GetDecimal(txtPreTaxAmount_extracharge);
                booking.ExtraChargeTaxAmount = GetDecimal(txtTotalTaxAmount_extracharge);

                // 🔹 AUDIT
                booking.CreatedBy = FrmLogin.EmployeeId;
                booking.BranchId = FrmDashboard.BranchId;
                booking.Status = 1;

                booking.AdditionalChargeAmt = GetDecimal(txtaddcharge);
                booking.AdditionalChargeDescription = txtextracharge_des.Text;

                booking.KOTDatetime = kotDateTime;
                booking.ConsumedRawMaterialStatus = _loadedConsumedRawMaterialStatus;
                booking.KoTPrintStatus = _loadedKoTPrintStatus;

                var result = bll.AddEditData("prdInsert_Upd_PartyBooking", booking);

                if (result.PartyBookingId <= 0)
                {
                    CMessageBox.Show("Master Save Failed");
                    return;
                }

                bookingId = result.PartyBookingId;

                // Retain the saved values for any subsequent save in this form.
                _loadedKotDateTime = kotDateTime;
                _hasLoadedKotDateTime = kotDateTime.HasValue;
                _isKotDateTimeManuallySet = false;

                #endregion

                #region SAVE PAYMENT
                if (bookingId > 0 && (PaymentSettleStatusData == 0 || PaymentSettleStatusData == Convert.ToInt32(PaymentSettlementStatus.AdditionalDue)))
                {
                    BllPartyBookingPayment bllPayment = new BllPartyBookingPayment();

                    if (PaymentMode == Convert.ToInt32(PaymentType.SPLIT))
                    {
                        decimal splitTotal = 0;

                        foreach (var p in partPayments)
                        {
                            if (decimal.TryParse(p.PartAmount, out decimal amt))
                                splitTotal += amt;
                        }

                        totalAdvance = dbAdvanceAmount + splitTotal;

                        decimal balance;
                        if (totalAdvance >= booking.NetAmount)
                        {
                            totalAdvance = 0;
                            balance = 0;
                        }
                        else
                        {
                            balance = booking.NetAmount - totalAdvance;
                        }

                        foreach (var p in partPayments)
                        {
                            if (!decimal.TryParse(p.PartAmount, out decimal amt))
                                continue;

                            DmnPartyBookingPayment payment = new DmnPartyBookingPayment
                            {
                                PaymentId = 0,
                                PartyBookingId = bookingId,
                                PaymentMode = Convert.ToInt32(p.PartPayId),

                                PaymentAmount = Convert.ToDecimal(p.PartAmount),
                                NetAmount = booking.NetAmount,
                                RcvAmnt = amt,
                                BalAmnt = balance,

                                IsAdvance = AdvanceFlag,
                                ReferenceNo = "",
                                PaymentDescription = "",

                                CreatedBy = FrmLogin.EmployeeId,
                                BranchId = FrmDashboard.BranchId,
                                Status = 1,
                                IsSplitPayment = true
                            };

                            var paymentResult = bllPayment.AddEditPayment(payment);

                            if (paymentResult.PaymentId <= 0)
                            {
                                CMessageBox.Show("Split payment save failed.");
                                return;
                            }
                        }
                    }
                    else
                    {
                        decimal balance = 0;
                        decimal pdamt = 0;

                        decimal alreadyPaid = dbAdvanceAmount;

                        if (Fullpaymentflag || PaymentSettleStatusData == Convert.ToInt32(PaymentSettlementStatus.AdditionalDue))
                        {
                            decimal remainingAmount = booking.NetAmount - alreadyPaid;

                            if (remainingAmount < 0)
                                remainingAmount = 0;

                            pdamt = remainingAmount;
                            currentReceived = remainingAmount;
                        }
                        else
                        {
                            pdamt = GetDecimal(txtRcvAmt);
                            currentReceived = pdamt;
                        }

                        totalAdvance = alreadyPaid + currentReceived;

                        if (totalAdvance >= booking.NetAmount)
                        {
                            totalAdvance = 0;
                            balance = 0;
                        }
                        else
                        {
                            balance = booking.NetAmount - totalAdvance;
                        }

                        DmnPartyBookingPayment payment = new DmnPartyBookingPayment
                        {
                            PaymentId = 0,
                            PartyBookingId = bookingId,
                            PaymentMode = PaymentMode,

                            PaymentAmount = pdamt,
                            NetAmount = booking.NetAmount,
                            RcvAmnt = currentReceived,
                            BalAmnt = balance,

                            IsAdvance = AdvanceFlag,
                            ReferenceNo = "",
                            PaymentDescription = "",

                            CreatedBy = FrmLogin.EmployeeId,
                            BranchId = FrmDashboard.BranchId,
                            Status = 1,
                            IsSplitPayment = false
                        };

                        var paymentResult = bllPayment.AddEditPayment(payment);

                        if (paymentResult.PaymentId <= 0)
                        {
                            CMessageBox.Show("Payment save failed.");
                        }
                    }
                }
                #endregion

                #region INSERT PARTY PACK AS DETAILS

                DmnPartyBookingDetails packDetails = new DmnPartyBookingDetails();

                packDetails.PartyBookingDetailsId = GetInt(txtdeafultpackid);
                packDetails.PartyBookingId = bookingId;

                packDetails.Product = lblpackname.Text;
                packDetails.ProductId = Partypackid;
                packDetails.ProductType = Convert.ToInt32(PackItemType.Pack);
                packDetails.TableType = 0;

                packDetails.Qty = Convert.ToInt32(txttotalguest.Text);

                packDetails.Tax = txttax.Text;
                packDetails.TaxType = txtTaxtype_pack.Text ?? "";
                packDetails.TaxPer = GetDecimal(txtTaxPer_pack);

                packDetails.TaxAmount = Convert.ToDecimal(txtTotalTaxAmount_pack.Text);
                igst = Convert.ToDecimal(txtIGSTAmt_pack.Text);
                packDetails.IGSTAmt = igst;
                packDetails.CGSTAmt = igst / 2;
                packDetails.SGSTAmt = igst / 2;
                packDetails.VATAmt = Convert.ToDecimal(txtVatamt_pack.Text);

                packDetails.Amount = Convert.ToDecimal(txt_PackTotalAmt.Text);
                packDetails.PostTaxAmount = Convert.ToDecimal(txtTotalTaxAmount_pack.Text);
                packDetails.PreTaxAmount = Convert.ToDecimal(txtPreTaxAmount_pack.Text);
                packDetails.NetAmount = Convert.ToDecimal(txtGrandTotal_pack.Text);

                packDetails.CreatedBy = FrmDashboard.UserId;
                packDetails.BranchId = FrmDashboard.BranchId;
                packDetails.Status = 1;
                packDetails.IsPackage = true;

                var resultpack = bll.AddEditDataDetails("prdInsert_Upd_PartyBookingDetails", packDetails);
                PartyBookingDetailsId = resultpack.PartyBookingDetailsId;

                #endregion

                #region CARD GIFT CAKE Default Products
                foreach (DataGridViewRow row in dgv_product.Rows)
                {
                    if (row.IsNewRow) continue;

                    int productType = Convert.ToInt32(row.Cells[PPackProductTypeno].Value ?? 0);
                    int productId = Convert.ToInt32(row.Cells[pProductid].Value ?? 0);
                    int Defaultproduct = Convert.ToInt32(row.Cells[pPartydeafult].Value ?? 0);

                    //if (Defaultproduct == 1)
                    //    continue;

                    //if (productType <= 0)
                    //    continue;

                    //if (productType != (int)PackItemType.ExtraChargeProduct && productType != (int)PackItemType.ManualProduct && productId <= 0)
                    //    continue;

                    DmnPartyBookingDetails det = new DmnPartyBookingDetails();

                    det.PartyBookingDetailsId = Convert.ToInt32(row.Cells[proPartyPackDetailsId].Value ?? 0);
                    det.PartyBookingId = bookingId;

                    if (Defaultproduct == 1)                    
                        det.DefaultpackItems = true;                    
                    else                    
                        det.DefaultpackItems = false;

                    det.SalesMasterId = SalesMasterId;

                    // PRODUCT INFO
                    det.Product = Convert.ToString(row.Cells[pitemno].Value ?? "");
                    det.CategoryName = Convert.ToString(row.Cells[pcategmenuno].Value ?? "");
                    det.ProductId = productId;
                    det.ProductType = productType;

                    // Assign TableType based on product type
                    if (productType == (int)PackItemType.Card)
                        det.TableType = Convert.ToInt32(FoodandBeverage.Interfaces.ProductType.Card);
                    else if (productType == (int)PackItemType.Gift)
                        det.TableType = Convert.ToInt32(FoodandBeverage.Interfaces.ProductType.RedemptionTicket);
                    else if (productType == (int)PackItemType.Menu)
                        det.TableType = Convert.ToInt32(FoodandBeverage.Interfaces.ProductType.FB);
                    else if (productType == (int)PackItemType.Cake ||
                             productType == (int)PackItemType.Venue)
                        det.TableType = 0;
                    else
                        det.TableType = 0;

                    // QTY & PRICE
                    det.Qty = Convert.ToDecimal(row.Cells[pqtyno].Value ?? 0);
                    det.Price = Convert.ToDecimal(row.Cells[pPrice].Value ?? 0);
                    det.Amount = Convert.ToDecimal(row.Cells[pAmount].Value ?? 0);

                    // TAX
                    det.Tax = Convert.ToString(row.Cells[ptax].Value ?? "");
                    det.TaxType = Convert.ToString(row.Cells[ptaxtype].Value ?? "");
                    det.TaxPer = Convert.ToDecimal(row.Cells[ptaxper].Value ?? 0);
                    det.TaxAmount = Convert.ToDecimal(row.Cells[ptaxamt].Value ?? 0);
                    det.TaxSettingId = Convert.ToInt32(row.Cells[ptaxsettingid].Value ?? 0);
                    det.IGSTAmt = Convert.ToDecimal(row.Cells[pigstamt].Value ?? 0);
                    det.VATAmt = Convert.ToDecimal(row.Cells[pvatamt].Value ?? 0);

                    // DISCOUNT
                    det.DiscountPer = Convert.ToDecimal(row.Cells[pdiscper].Value ?? 0);
                    det.DiscountAmount = Convert.ToDecimal(row.Cells[pdisamt].Value ?? 0);

                    // AMOUNT
                    det.NetAmount = Convert.ToDecimal(row.Cells[pnetamt].Value ?? 0);
                    det.PreTaxAmount = productType == (int)PackItemType.Cake
                                        ? Convert.ToDecimal(txtPreTaxAmount_cake.Text)
                                        : Convert.ToDecimal(row.Cells[ppretaxamt].Value ?? 0);
                    det.PostTaxAmount = productType == (int)PackItemType.Cake
                                        ? Convert.ToDecimal(txtTotalTaxAmount_cake.Text)
                                        : Convert.ToDecimal(row.Cells[ptaxamt].Value ?? 0);

                    // CAKE INFO
                    det.CakeRemarks = productType == (int)PackItemType.Cake ? Convert.ToString(row.Cells[premarksno].Value ?? "") : null;
                    det.CakeMessage = productType == (int)PackItemType.Cake ? Convert.ToString(row.Cells[pmessageno].Value ?? "") : null;

                    // AUDIT
                    det.CreatedBy = FrmDashboard.UserId;
                    det.BranchId = FrmDashboard.BranchId;
                    det.Status = 1;

                    // Insert/Update
                    var resultBooking = bll.AddEditDataDetails("prdInsert_Upd_PartyBookingDetails", det);
                    row.Cells[proPartyPackDetailsId].Value = resultBooking.PartyBookingDetailsId;

                }
                #endregion

                #endregion

                #region SALE MASTER AND DETAILS

                #region SALE MASTER

                BllSale bllSales = new BllSale();
                DmnSalemaster sale = new DmnSalemaster();

                sale.SalesMasterId = SalesMasterId;
                setperfixsale();
                sale.EntryNo = txtinvoiceno.Text;
                sale.EntryDate = dtpEventDate_From.Value.Date.Add(dtpEventTime_From.Value.TimeOfDay);
                //sale.CreatedDateTime = dtpEventDate.Value.Date.Add(dtpEventTime.Value.TimeOfDay); 

                #region Customer Info
                sale.FirstName = txtname.Text;
                sale.PhoneNumber = txtph.Text;
                sale.EmailAddress = txtemail.Text;
                sale.FullAddress = txtaddress.Text;
                #endregion

                sale.PayId = PaymentMode;
                sale.TotQty = 1;
                sale.TotalAmnt = booking.NetAmount;
                sale.NetAmount = booking.NetAmount;
                sale.TotalTax = booking.TaxAmount;

                sale.TotalIGST = booking.IGSTAmt;
                sale.TotalCGST = booking.CGSTAmt;
                sale.TotalSGST = booking.SGSTAmt;
                sale.TotalVAT = booking.VATAmt;

                sale.RcvAmnt = booking.ReceivedAmount;
                sale.BalAmnt = booking.BalanceAmount;
                sale.AdvanceAmount = booking.AdvanceAmount;

                sale.Remark = txteventdesc.Text;

                sale.CreatedBy = FrmLogin.EmployeeId;
                sale.BranchId = FrmDashboard.BranchId;
                sale.Status = 1;

                sale.mDeviceIP = " [ " + System.Environment.MachineName + " ]  -" + Dns.GetHostByName(System.Environment.MachineName).AddressList[0].ToString();
                sale.mDevice = clsgen.FetchMacId();

                sale.TotalPreTaxAmt = booking.PreTaxAmount;
                sale.DisAmnt = booking.DiscountAmount;
                sale.DisPer = booking.DiscountPer;

                sale.PartyBookingId = bookingId;

                sale.AdditionalChargeAmt = GetDecimal(txtaddcharge);
                sale.AdditionalChargeDescription = txtextracharge_des.Text;

                sale.PaymentSettlementStatus = PaymentSettleStatusData;

                sale.SaleType = Convert.ToInt32(SaleCategory.PartyBooking);

                var saleResult = bllSales.AddEditPartyBookingSale("prdInsert_Upd_SalesMaster_PartyBooking", sale);

                if (saleResult.SalesMasterId <= 0)
                {
                    CMessageBox.Show("Sale Master Save Failed");
                    return;
                }

                SalesMasterId = saleResult.SalesMasterId;

                #endregion

                #region REFUND
                if (SalesMasterId > 0 && PaymentSettleStatusData == Convert.ToInt32(PaymentSettlementStatus.RefundDue))
                {
                    ProcessPartyBookingRefund(PaymentMode);
                }
                #endregion  

                #region INSERT PACK SALE DETAILS

                DmnSaledetails detPack = new DmnSaledetails();

                detPack.SalesMasterId = SalesMasterId;
                detPack.SalesDetailsId = Partypacksaledetailid;

                detPack.itemid = Partypackid;
                detPack.ItemName = lblpackname.Text;

                detPack.Quantity = Convert.ToInt32(txttotalguest.Text);
                detPack.SalesRate = booking.PackPrice;
                detPack.MRP = booking.PackPrice;

                detPack.Amount = Convert.ToDecimal(txt_PackTotalAmt.Text); 

                detPack.DiscountPer = booking.DiscountPer;
                detPack.DiscountAmount = booking.DiscountAmount;

                detPack.Tax = booking.Tax;
                detPack.TaxType = booking.TaxType;
                detPack.TaxPer = booking.TaxPer;

                detPack.TaxAmount = booking.PackTaxAmount;

                detPack.IGSTAmount = booking.IGSTAmt;
                detPack.CGSTAmount = booking.CGSTAmt;
                detPack.SGSTAmount = booking.SGSTAmt;

                detPack.VatAmount = booking.VATAmt;

                detPack.MarketPrice = booking.PackPrice;

                detPack.PreTaxAmt = booking.PackPreTaxAmount;
                detPack.NetAmount = booking.PackGrandTotal;

                detPack.IsPackage = true;
                detPack.PackageType = 1;
                detPack.PartyBookingId = bookingId;

                detPack.PartyBookingDetailsId = PartyBookingDetailsId;
                detPack.HSNCode = _partyPackHsnCode;

                detPack.CreatedBy = FrmDashboard.UserId;
                detPack.BranchId = FrmDashboard.BranchId;
                detPack.Status = 1;

                bllSales.AddEditPartyBookingSaleDetails("prdInsert_Upd_SalesDetails_PartyBooking", detPack);

                #endregion

                #region CARD GIFT CAKE SALE DETAILS

                foreach (DataGridViewRow row in dgv_product.Rows)
                {
                    if (row.IsNewRow) continue;

                    int productType = Convert.ToInt32(row.Cells[PPackProductTypeno].Value ?? 0);
                    int productId = Convert.ToInt32(row.Cells[pProductid].Value ?? 0);
                    int Defaultproduct = Convert.ToInt32(row.Cells[pPartydeafult].Value ?? 0);

                    if (Defaultproduct == 1)
                        continue;

                    if (productType <= 0)
                        continue;

                    if (productType != (int)PackItemType.ExtraChargeProduct && productType != (int)PackItemType.ManualProduct && productId <= 0)
                        continue;

                    DmnSaledetails detSale = new DmnSaledetails();

                    detSale.SalesMasterId = SalesMasterId;
                    detSale.SalesDetailsId = Convert.ToInt32(row.Cells[psaledetailidno].Value ?? 0);
                    detSale.itemid = Convert.ToInt32(row.Cells[pProductid].Value ?? 0);
                    detSale.ItemName = Convert.ToString(row.Cells[pitemno].Value ?? "");
                    // The sales-detail procedure expects ProductType as a string value.
                    // Persist the same PackItemType number used by the booking detail,
                    // including Venue=10.
                    detSale.ProductType = productType.ToString();

                    detSale.Quantity = Convert.ToDecimal(row.Cells[pqtyno].Value ?? 0);
                    detSale.SalesRate = Convert.ToDecimal(row.Cells[pPrice].Value ?? 0);
                    detSale.MRP = detSale.SalesRate;

                    // PreTaxAmt and Amount
                    detSale.Amount = Convert.ToDecimal(row.Cells[pAmount].Value ?? 0);
                    detSale.PreTaxAmt = Convert.ToDecimal(row.Cells[ppretaxamt].Value ?? 0);
                    detSale.NetAmount = Convert.ToDecimal(row.Cells[pnetamt].Value ?? 0);

                    // Discount
                    detSale.DiscountPer = Convert.ToDecimal(row.Cells[pdiscper].Value ?? 0);
                    detSale.DiscountAmount = Convert.ToDecimal(row.Cells[pdisamt].Value ?? 0);

                    // Tax
                    detSale.Tax = Convert.ToString(row.Cells[ptax].Value ?? "");
                    detSale.TaxType = Convert.ToString(row.Cells[ptaxtype].Value ?? "");
                    detSale.TaxPer = Convert.ToDecimal(row.Cells[ptaxper].Value ?? 0);
                    detSale.TaxAmount = Convert.ToDecimal(row.Cells[ptaxamt].Value ?? 0);

                    detSale.IGSTAmount = Convert.ToDecimal(row.Cells[pigstamt].Value ?? 0);
                    detSale.CGSTAmount = detSale.IGSTAmount / 2;
                    detSale.SGSTAmount = detSale.IGSTAmount / 2;
                    detSale.VatAmount = Convert.ToDecimal(row.Cells[pvatamt].Value ?? 0);

                    detSale.MarketPrice = detSale.SalesRate;

                    // Cake specific info
                    if (productType == (int)PackItemType.Cake)
                    {
                        detSale.CakeMessage = Convert.ToString(row.Cells[pmessageno].Value ?? "");
                        detSale.PackageType = (int)PackItemType.Cake;
                    }
                    else if (productType == (int)PackItemType.Card)
                        detSale.PackageType = (int)PackItemType.Card; // Card
                    else if (productType == (int)PackItemType.Gift)
                        detSale.PackageType = (int)PackItemType.Gift; // Gift
                    else if (productType == (int)PackItemType.Menu)
                        detSale.PackageType = (int)PackItemType.Menu; // Party menu product
                    else if (productType == (int)PackItemType.ExtraChargeProduct)
                        detSale.PackageType = (int)PackItemType.ExtraChargeProduct;
                    else if (productType == (int)PackItemType.ManualProduct)
                        detSale.PackageType = (int)PackItemType.ManualProduct;
                    else if (productType == (int)PackItemType.Venue)
                        detSale.PackageType = (int)PackItemType.Venue;

                    detSale.IsPackage = false;
                    detSale.PartyBookingId = bookingId;
                    detSale.PartyBookingDetailsId = Convert.ToInt32(row.Cells[proPartyPackDetailsId].Value ?? 0);

                    // Audit
                    detSale.CreatedBy = FrmDashboard.UserId;
                    detSale.BranchId = FrmDashboard.BranchId;
                    detSale.Status = 1;
                    detSale.HSNCode = Convert.ToString(row.Cells[phsncodeno].Value ?? "");

                    bllSales.AddEditPartyBookingSaleDetails("prdInsert_Upd_SalesDetails_PartyBooking", detSale);
                }

                #endregion

                #endregion

                #region BILL PRINT

                if (PaymentSettleStatusData != Convert.ToInt32(PaymentSettlementStatus.RefundDue) &&
                    PaymentSettleStatusData != Convert.ToInt32(PaymentSettlementStatus.Settled))
                {
                    using (FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(SalesMasterId, PaymentMode, 0, Convert.ToInt32(BillType.Partybill)))
                    {
                        frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                        frmSalesInvoice.ShowDialog();
                    }
                }
                switch ((PaymentSettlementStatus)PaymentSettleStatusData)
                {
                    case PaymentSettlementStatus.Settled:
                        CMessageBox.Show("Party Booking Saved Successfully. Payment is fully settled.");
                        break;

                    case PaymentSettlementStatus.RefundDue:
                        CMessageBox.Show("Party Booking Saved Successfully. Refund is due to the customer.");
                        break;

                    case PaymentSettlementStatus.AdditionalDue:
                        CMessageBox.Show("Party Booking Saved Successfully. Additional payment is required from the customer.");
                        break;

                    default:
                        CMessageBox.Show("Party Booking Saved Successfully.");
                        break;
                }

                //ClearAll();

                #endregion

            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page Paybill()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void btncomple_Click(object sender, EventArgs e)
        {
            PaymentMode = Convert.ToInt32(PaymentType.COMPLEMENTRY);
            Paybill();
        }

        private void btn_other_Click(object sender, EventArgs e)
        {
            try
            {
                PaymentMode = Convert.ToInt32(PaymentType.SPLIT);
                pnlPaymode.Visible = false;
                HideBlurPopup(pnlPaymode);
                btnPartPaynow.Visible = false;
                pnlSplit.Visible = true;
                pnlSplit.Visible = true;ShowBlurPopup(pnlSplit);
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

                if (Fullpaymentflag)
                {
                    decimal pdamt = 0, remainingAmount = 0, balance = 0;

                    decimal alreadyPaid = dbAdvanceAmount;

                    decimal netamt = decimal.TryParse(lblNetAmt.Text, out decimal na) ? na : 0;

                    if (Fullpaymentflag || PaymentSettleStatusData == Convert.ToInt32(PaymentSettlementStatus.AdditionalDue))
                    {
                        remainingAmount = netamt - alreadyPaid;
                        if (remainingAmount < 0)
                            remainingAmount = 0;
                    }

                    lblPartNetAmt.Text = remainingAmount.ToString();
                    lblPartBalAmt.Text = remainingAmount.ToString();
                }
                else
                {
                    lblPartNetAmt.Text = txtRcvAmt.Text;
                    lblPartBalAmt.Text = txtRcvAmt.Text;
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btn_other_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void btnclos_payment_Click(object sender, EventArgs e)
        {
            pnlPaymode.Visible = false;
            HideBlurPopup(pnlPaymode);
        }

        #endregion

        #region Search Party Booking
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DmnSearch search = new DmnSearch();
                search.ColumnHeaders = "ID,Customer,Event,DateTime,PhoneNo,Email";
                search.Select = "PartyBookingId as Id, CustomerName as Field1, e.ItemName as Field2, " +
                                "BookingDatetime Field3,pb.Phone as Field4, pb.Email as Field5";
                search.Table = "tblPartyBooking pb " +
                    "left join tblEventCommonItems e on e.EventItemId=pb.EventType and e.Status = 1 AND EventCategoryType = 1 " +
                    "left join tblPartyPackMaster pm on pm.PartyPackId=pb.PartyPackId and pm.status=1";
                search.WhereNdGroupBy = "WHERE pb.Status = 1 and pb.refundstatus!=1";
                search.OrderBy = " ORDER BY PartyBookingId DESC";

                var gridSource = new BllSearch<DmnGridFields>().GetSearchData("prdSearch", search);
                if (gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records");
                    return;
                }

                int bookingId = 0;
                using (var frm = new FrmSearch<DmnGridFields>(
                    search,
                    SearchMode.FirstCharacter,
                    this.Width - 300,
                    this.Height - 300,
                    this.Location.Y + 50,
                    this.Location.X + 20,
                    null))
                {
                    if (frm.ShowDialog() == DialogResult.OK)
                        bookingId = Convert.ToInt32(frm._retValue);
                }

                if (bookingId > 0)
                    LoadBookingById(bookingId);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btnSearch_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void LoadBookingById(int bookingId)
        {
            if (bookingId <= 0)
                return;

            try
            {
                ClearAll();
                BookingId = bookingId;
                TogglePayCancel(1);
                LoadBookingMaster(bookingId, 0);

                int packId;
                if (!int.TryParse(txtpartpackid.Text, out packId) || packId <= 0)
                    return;

                LoadDefaultPackAndBookingItems(packId, bookingId);
                lblpackname.Visible = true;
                lblpacklabel.Visible = true;
                lblpackname.Text = SetPackName(packId);
                textb_shape();
                RecalculateAllRowsAndTotals();
                SetupGuestControls();

                // The bill-preview button depends on the booking and sales IDs
                // populated above, so refresh its visibility after loading a booking.
                ConfigureResponsivePartyButtons();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page LoadBookingById()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private decimal GetTotalPaidAmount(int bookingId)
        {
            try
            {
                decimal totalPaid = 0;

                BllSearch<DmnPartyBookingPayment> bll = new BllSearch<DmnPartyBookingPayment>();
                DmnSearch search = new DmnSearch();

                search.Select = "ISNULL(SUM(PaidAmount),0) AS PaidAmount";
                search.Table = "tblPartyBookingPayment";
                search.WhereNdGroupBy = $"WHERE PartyBookingId = {bookingId} AND Status = 1";

                var result = bll.GetSearchData("prdSearch", search).FirstOrDefault();

                if (result != null)
                    totalPaid = result.PaidAmount;

                return totalPaid;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page GetTotalPaidAmount()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
                return 0;
            }
        }
        public decimal GettotaladvanceAmount(int partybid)
        {
            decimal totalAdvanceAmount = 0;

            try
            {
                var bllPartyPayment = new BllSearch<DmnPartyBookingPayment>();

                var partyPaymentSearch = new DmnSearch
                {
                    Select = @"
                PaymentId,
                PaymentMode,
                NetAmount,
                RcvAmnt,
                BalAmnt,
                IsSplitPayment,
                PaidAmount,
                RefundStatus",
                    Table = "tblPartyBookingPayment",
                    WhereNdGroupBy = "WHERE PartyBookingId = " + partybid
                };

                var partyPaymentData = bllPartyPayment.GetSearchData("prdSearch", partyPaymentSearch);

                if (partyPaymentData != null && partyPaymentData.Count > 0)
                {
                    var sortedList = partyPaymentData
                    .OrderBy(x => Convert.ToInt32(x.PaymentId))
                    .ToList();

                    decimal netAmount = Convert.ToDecimal(sortedList.Last().NetAmount);
                    decimal totalPaid = sortedList.Sum(x => Convert.ToDecimal(x.PaidAmount));

                    if (totalPaid >= netAmount)
                    {
                        totalAdvanceAmount = totalPaid;
                    }
                    else
                    {
                        decimal lastReceivedAmount = Convert.ToDecimal(sortedList.Last().PaidAmount);

                        if (sortedList.Count == 1)
                        {
                            if (totalPaid >= netAmount)
                            {
                                totalAdvanceAmount = totalPaid;
                            }
                            else
                                totalAdvanceAmount = lastReceivedAmount;
                        }
                        else
                        {
                            totalAdvanceAmount = totalPaid;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page GettotaladvanceAmount()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
                return 0;
            }

            return totalAdvanceAmount;
        }
        private void LoadBookingMaster(int bookingId, int packid)
        {
            bool previousLoadingState = _isLoadingPartyBooking;
            _isLoadingPartyBooking = true;

            try
            {
                LoadEventItems();

                BllSearch<DmnPartyBooking> bll = new BllSearch<DmnPartyBooking>();
                DmnSearch search = new DmnSearch();
                search.Select = "*";
                search.Table = "tblPartyBooking";
                search.WhereNdGroupBy = $"WHERE PartyBookingId = {bookingId} AND Status = 1";

                var result = bll.GetSearchData("prdSearch", search).FirstOrDefault();
                if (result == null) return;

                txtpartpackid.Text = result.PartyPackId.ToString();
                Partypackid = Convert.ToInt32(result.PartyPackId);
                txtpartybookingid.Text = result.PartyBookingId.ToString();

                BllSearch<DmnPartyPack> bllMaster = new BllSearch<DmnPartyPack>();
                DmnSearch searchMaster = new DmnSearch
                {
                    Select = "PartyPackId, PackName, AdultPrice, ChildPrice, DiscountPer, DiscountAmt, TaxType, Tax,TaxSettingsId,HSNCode",
                    Table = "tblPartyPackMaster",
                    WhereNdGroupBy = $"WHERE PartyPackId = {Convert.ToInt32(txtpartpackid.Text)} AND Status = 1"
                };

                var master = bllMaster.GetSearchData("prdSearch", searchMaster).FirstOrDefault();

                if (master != null)
                {
                    txttax.Text = master.Tax;
                    txtAdultPrice.Text = master.AdultPrice != null ? Convert.ToDecimal(master.AdultPrice).ToString("0.00") : "0.00";
                    txtChildPrice.Text = master.ChildPrice != null ? Convert.ToDecimal(master.ChildPrice).ToString("0.00") : "0.00";
                    txtTaxSettingId.Text = Convert.ToString(master.TaxSettingsId);
                    _partyPackHsnCode = Convert.ToString(master.HSNCode ?? string.Empty);
                }

                txtname.Text = result.CustomerName;
                txtph.Text = result.Phone;
                txtemail.Text = result.Email;
                txtaddress.Text = result.Address;

                cmbevent.SelectedValue = Convert.ToInt32(result.EventType);

                txteventdesc.Text = result.EventDescription;
                txtAdults.Text = result.NoOfAdults.ToString();
                txtChild.Text = result.NoOfChildren.ToString();

                // ===============================
                // 🔹 CAKE TOTALS
                // ===============================
                txtCakeGrandTotal.Text = result.CakeGrandTotal.ToString("0.00");
                txtPreTaxAmount_cake.Text = result.CakePreTaxAmount.ToString("0.00");
                txtTotalTaxAmount_cake.Text = result.CakeTaxAmount.ToString("0.00");

                // ===============================
                // 🔹 CARD TOTALS
                // ===============================
                txtCardGrandTotal.Text = result.CardGrandTotal.ToString("0.00");
                txtPreTaxAmount_card.Text = result.CardPreTaxAmount.ToString("0.00");
                txtTotalTaxAmount_card.Text = result.CardTaxAmount.ToString("0.00");

                // ===============================
                // 🔹 GIFT TOTALS
                // ===============================
                txtGiftGrandTotal.Text = result.GiftGrandTotal.ToString("0.00");
                txtPreTaxAmount_gift.Text = result.GiftPreTaxAmount.ToString("0.00");
                txtTotalTaxAmount_gift.Text = result.GiftTaxAmount.ToString("0.00");

                // ===============================
                // 🔹 PACKAGE TOTALS
                // ===============================
                txtGrandTotal_pack.Text = result.PackGrandTotal.ToString("0.00");
                txtPreTaxAmount_pack.Text = result.PackPreTaxAmount.ToString("0.00");
                txtTotalTaxAmount_pack.Text = result.PackTaxAmount.ToString("0.00");

                // ===============================
                // 🔹 EXTRA CHARGE  TOTALS
                // ===============================
                txtExtrachargeGrandTotal.Text = result.ExtraChargeGrandTotal.ToString("0.00");
                txtPreTaxAmount_extracharge.Text = result.ExtraChargePreTaxAmount.ToString("0.00");
                txtTotalTaxAmount_extracharge.Text = result.ExtraChargeTaxAmount.ToString("0.00");


                txtDiscountPer.Text = result.DiscountPer.ToString("0.00");
                txtTotalTaxAmount.Text = result.TaxAmount.ToString();

                txtextracharge_amt.Text = Convert.ToString(result.AdditionalChargeAmt);
                txtextracharge_des.Text = result.AdditionalChargeDescription;
                txtaddcharge.Text = Convert.ToString(result.AdditionalChargeAmt);

                decimal netAmt = result.NetAmount;
                decimal advAmt = GettotaladvanceAmount(bookingId);

                txtRcvAmt.Text = "0.00";
                lblNetAmt.Text = netAmt.ToString("0.00");

                lblBalAmt.Text = (netAmt - advAmt).ToString("0.00");

                dbReceivedAmount = result.ReceivedAmount;

                dbAdvanceAmount = GettotaladvanceAmount(bookingId);

                decimal balance = netAmt - advAmt;

                txtRcvAmt.Text = "0.00";
                lblNetAmt.Text = netAmt.ToString("0.00");
                lbladvamtdata.Text = advAmt.ToString("0.00");

                if (Convert.ToDecimal(result.BalanceAmount) != 0)
                {
                    lblBalAmt.Text = balance.ToString("0.00");
                    dbBalanceAmount = balance;
                }
                else
                {
                    lblBalAmt.Text = "0.00";
                    dbBalanceAmount = 0;
                }

                UpdateBalancePayableCaption(dbBalanceAmount);

                dbNetAmount = netAmt;

                if (advAmt > 0)
                {
                    ToggleFirstRow(1);
                }
                else
                {
                    ToggleFirstRow(0);
                }

                txtTotalDiscountAmt.Text = result.DiscountAmount.ToString("0.00");

                if (result.BookingDateTime != DateTime.MinValue)
                {
                    dtpEventDate_From.Value = result.BookingDateTime.Date;
                    dtpEventTime_From.Value = DateTime.Today.Add(result.BookingDateTime.TimeOfDay);
                    _calendarBookingStart = result.BookingDateTime;

                    var endProperty = result.GetType().GetProperty("BookingToDateTime");
                    object endValue = endProperty == null ? null : endProperty.GetValue(result, null);
                    DateTime endDateTime;
                    if (endValue != null && DateTime.TryParse(Convert.ToString(endValue), out endDateTime))
                        _calendarBookingEnd = endDateTime;
                    else
                        _calendarBookingEnd = result.BookingDateTime;

                    ApplyCalendarDateTimeInterval();
                    UpdateSelectedDateLabel();
                }

                txtTotalDiscountAmt.Text = result.DiscountAmount.ToString("0.00");

                _loadedConsumedRawMaterialStatus = result.ConsumedRawMaterialStatus;
                _loadedKoTPrintStatus = result.KoTPrintStatus;
                _loadedKotDateTime = result.KOTDatetime;
                _hasLoadedKotDateTime = result.KOTDatetime.HasValue;
                _isKotDateTimeManuallySet = false;

                if (_loadedKotDateTime.HasValue)
                    SetKotDateTimePickerValue(_loadedKotDateTime.Value);
                else if (result.BookingDateTime != DateTime.MinValue)
                    UpdateDefaultKotDateTime(result.BookingDateTime);

                PriceChangecalculation();
            }

            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page GettotaladvanceAmount()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
            finally
            {
                _isLoadingPartyBooking = previousLoadingState;
            }
        }

        private void LoadDefaultPackAndBookingItems(int partyPackId, int bookingId)
        {
            try
            {
                dgv_packenter.Rows.Clear();

                int productSl = 1;
                int giftSl = 1;
                int packSl = 1;

                #region Load Default Pack ,Cake ,Card ,Gift

                BllSearch<DmnPartyPackDetails> bllPack = new BllSearch<DmnPartyPackDetails>();

                DmnSearch searchPack = new DmnSearch
                {
                    Select = "*",
                    Table = "tblPartyPackDetails",
                    WhereNdGroupBy = $"WHERE Status = 1 AND PartyPackId = {partyPackId}"
                };

                var packItems = bllPack.GetSearchData("prdSearch", searchPack);

                foreach (var item in packItems)
                {
                    int rowIndex = dgv_product.Rows.Add();
                    DataGridViewRow row = dgv_product.Rows[rowIndex];

                    row.Cells[pslno].Value = productSl++;
                    row.Cells[pitemno].Value = item.Item;
                    row.Cells[pqtyno].Value = item.Quantity;

                    row.Cells[ppridno].Value = item.Productid;
                    row.Cells[pproducttype].Value = item.ProductType;

                    row.Cells[proPartyPackDetailsId].Value = 0;
                    row.Cells[pPartydeafult].Value = 1;

                    if (item.MenuCategoryId != null && Convert.ToInt32(item.MenuCategoryId) > 0)
                    {
                        string categoryName = GetCategoryName(Convert.ToInt32(item.MenuCategoryId));
                        row.Cells[pcategmenuno].Value = categoryName;
                    }
                    else
                    {
                        row.Cells[pcategmenuno].Value = "None";
                    }

                    row.Cells[PPackProductTypeno].Value = item.PackItemType;

                    row.ReadOnly = true;
                    row.DefaultCellStyle.BackColor = Color.Gainsboro;
                    row.DefaultCellStyle.ForeColor = Color.Black;


                    DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_product.Rows[rowIndex].Cells[pactionno];
                    cellcolumntype.Value = Blankimage;
                    cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                }

                #endregion

                #region Load Custom Product, card , Gifts
                BllSearch<DmnPartyBookingDetails> bllBooking = new BllSearch<DmnPartyBookingDetails>();
                DmnSearch searchBooking = new DmnSearch
                {
                    Select = "*",
                    Table = "tblPartyBookingDetails",
                    WhereNdGroupBy = $"WHERE PartyBookingId = {bookingId} AND Status = 1"
                };

                var bookingItems = bllBooking.GetSearchData("prdSearch", searchBooking);
                if (bookingItems.Count > 0)
                {
                    foreach (var item in bookingItems)
                    {
                        #region INSERT PARTY PACK AS DETAILS
                        if (item.IsPackage)
                        {
                            // Use the HSN code stored on the package master.
                            _partyPackHsnCode = GetPartyItemHsnCode(
                                (int)PackItemType.Pack,
                                Convert.ToInt32(item.ProductId));

                            txttax.Text = item.Tax;
                            txtTaxtype_pack.Text = item.TaxType;
                            txtTaxPer_pack.Text = item.TaxPer.ToString();
                            txtTotalTaxAmount_pack.Text = item.TaxAmount.ToString();
                            txtPreTaxAmount_pack.Text = item.PreTaxAmount.ToString();
                            txtGrandTotal_pack.Text = item.NetAmount.ToString();
                            txtdeafultpackid.Text = item.PartyBookingDetailsId.ToString();
                            Partypacksaledetailid = item.SalesDetailsId;
                            SalesMasterId = item.SalesMasterId;
                        }
                        #endregion

                        #region CARD CAKE GIFT
                        if (item.Status == 1 &&
                           (item.ProductType == (int)PackItemType.Cake ||
                            item.ProductType == (int)PackItemType.Card ||
                            item.ProductType == (int)PackItemType.Gift ||
                            item.ProductType == (int)PackItemType.Menu ||
                            item.ProductType == (int)PackItemType.ExtraChargeProduct ||
                            item.ProductType == (int)PackItemType.Venue))
                        {
                            int rowIndex = dgv_product.Rows.Add();

                            var row = dgv_product.Rows[rowIndex];

                            row.Cells[pslno].Value = productSl++;
                            row.Cells[proPartyPackDetailsId].Value = item.PartyBookingDetailsId;

                            row.Cells[pitemno].Value = item.Product;
                            row.Cells[pqtyno].Value = item.Qty;
                            row.Cells[pPrice].Value = item.Price;
                            row.Cells[pcategmenuno].Value = item.CategoryName;
                            row.Cells[pAmount].Value = item.Amount;

                            row.Cells[pProductid].Value = item.ProductId;
                            row.Cells[ppridno].Value = item.Prid;
                            row.Cells[pproducttype].Value = item.ProductType;

                            // 🔹 Tax
                            row.Cells[ptax].Value = item.Tax;
                            row.Cells[ptaxtype].Value = item.TaxType;
                            row.Cells[ptaxper].Value = item.TaxPer;
                            row.Cells[ptaxamt].Value = item.TaxAmount;
                            row.Cells[pigstamt].Value = item.IGSTAmt;
                            row.Cells[pvatamt].Value = item.VATAmt;
                            row.Cells[ptaxsettingid].Value = item.TaxSettingId;

                            // 🔹 Amounts
                            row.Cells[ppretaxamt].Value = item.PreTaxAmount;
                            row.Cells[pnetamt].Value = item.NetAmount;

                            // 🔹 Discount
                            row.Cells[pdiscper].Value = item.DiscountPer;
                            row.Cells[pdisamt].Value = item.DiscountAmount;

                            // 🔹 Cake special fields
                            if (item.ProductType == (int)PackItemType.Cake)
                            {
                                row.Cells[premarksno].Value = item.CakeRemarks;
                                row.Cells[pmessageno].Value = item.CakeMessage;
                            }

                            row.Cells[pPartydeafult].Value = 0;
                            row.Cells[PPackProductTypeno].Value = item.ProductType;

                            // Rehydrate HSN values for item types whose HSN is stored
                            // with their source product/venue record. Extra Charge keeps
                            // its entry-time HSN in the grid; its custom HSN is not a
                            // field in DmnPartyBookingDetails/the current save procedure.
                            if (item.ProductType == (int)PackItemType.Cake)
                            {
                                BllSearch<DmnGridFields> hsnSearch = new BllSearch<DmnGridFields>();
                                DmnSearch hsnQuery = new DmnSearch
                                {
                                    Select = "ISNULL(HSNCode,'') AS Field1",
                                    Table = "tblPartyPackCake",
                                    WhereNdGroupBy = "WHERE PartyPackCakeId=" + item.ProductId
                                };
                                var hsnResult = hsnSearch.GetSearchData("prdSearch", hsnQuery);
                                row.Cells[phsncodeno].Value = hsnResult != null && hsnResult.Count > 0
                                    ? Convert.ToString(hsnResult[0].Field1)
                                    : string.Empty;
                            }
                            else if (item.ProductType != (int)PackItemType.ExtraChargeProduct &&
                                     item.ProductId > 0)
                            {
                                string hsnCode = GetPartyItemHsnCode(item.ProductType, item.ProductId);
                                if (!string.IsNullOrWhiteSpace(hsnCode))
                                    row.Cells[phsncodeno].Value = hsnCode;
                            }

                            row.Cells[psaledetailidno].Value = item.SalesDetailsId;
                            row.Cells[psalemasteridno].Value = item.SalesMasterId;

                            DataGridViewImageCell cell = (DataGridViewImageCell)row.Cells[pactionno];
                            cell.Value = Grid_Remove;
                            cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                        }

                        #endregion  

                    }
                }

                #endregion

            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page LoadDefaultPackAndBookingItems()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }

        }
        #endregion

        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                foreach (TextBox textBox in new TextBox[] { txtdiscountreason, txteventdesc, txtemail, txtph, txtname, txtRcvAmt, txtTotalDiscountAmt, txtTotalTaxAmount, txtaddress, txtChild, txtAdults, txttotalguest, txtRenote, txtrefundamt, txtcakeremarks, txtcakemessage, txtcakeshape, txtcakesize, txtcakeprice, txtextracharge_des, txtextracharge_amt, txtaddcharge, txtCgstPrcnt, txtSgstPrcnt })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    //textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }

            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page textb_shape()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
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
                clsgen.LogException("Partybooking page ApplyRoundedTextBox()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        #endregion

        #region Split Payment
        private void btnPartRefresh_Click(object sender, EventArgs e)
        {
            try
            {


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
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btnPartRefresh_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void btnPartPaynow_Click(object sender, EventArgs e)
        {
            Paybill();
        }

        private void btnPartClose_Click(object sender, EventArgs e)
        {
            pnlSplit.Visible = false;
            HideBlurPopup(pnlSplit);
        }
        private void cash_part_Click(object sender, EventArgs e)
        {
            try
            {
                partCount = Convert.ToInt32(partPayments.Count);
                decimal bal = Convert.ToDecimal(lblPartBalAmt.Text);
                if (partCount == 0)
                {
                    decimal kdata = 0;
                    using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.None)))
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
                clsgen.LogException("Partybooking page cash_part_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
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
                clsgen.LogException("Partybooking page Debit_part_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
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
                clsgen.LogException("Partybooking page upi_part_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
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
                clsgen.LogException("Partybooking page netbanking_part_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
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
                clsgen.LogException("Partybooking page credit_part_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }

        }

        private void midas_part_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Refund And Cancel booking
        public void TogglePayCancel(int showCancel)
        {
            if (showCancel == 1)
            {
                btncancelbooking.Visible = true;

                tlp_pay_cancel.ColumnStyles[0].SizeType = SizeType.Percent;
                tlp_pay_cancel.ColumnStyles[0].Width = 50;
                tlp_pay_cancel.ColumnStyles[3].SizeType = SizeType.Percent;
                tlp_pay_cancel.ColumnStyles[3].Width = 50;
            }
            else
            {
                btncancelbooking.Visible = false;

                tlp_pay_cancel.ColumnStyles[0].SizeType = SizeType.Percent;
                tlp_pay_cancel.ColumnStyles[0].Width = 50;
                tlp_pay_cancel.ColumnStyles[3].SizeType = SizeType.Percent;
                tlp_pay_cancel.ColumnStyles[3].Width = 50;
            }
        }

        private void btncancelbooking_Click(object sender, EventArgs e)
        {
            try
            {
                decimal totalPaid = GetTotalPaidAmount(BookingId);
                lbltotalcancelamt.Text = totalPaid.ToString("N2");
                pnl_bookingcancel.Visible = true;
                pnl_bookingcancel.Visible = true;ShowBlurPopup(pnl_bookingcancel);
                pnl_bookingcancel.BringToFront();
                pnl_bookingcancel.Left = (this.ClientSize.Width - pnl_bookingcancel.Width) / 2;
                pnl_bookingcancel.Top = (this.ClientSize.Height - pnl_bookingcancel.Height) / 2;
            }
            catch (Exception ex)
            {
            }
        }
        private void btncls_bookingcancel_Click(object sender, EventArgs e)
        {
            pnl_bookingcancel.Visible = false;
            HideBlurPopup(pnl_bookingcancel);
        }

        private void btnrefundfull_Click(object sender, EventArgs e)
        {
            txtrefundamt.Text = lbltotalcancelamt.Text;
        }

        private void txtrefundamt_Click(object sender, EventArgs e)
        {
            try
            {
                decimal typedAmount = 0;
                using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.percentage)))
                {
                    frm.ShowDialog();

                    if (!string.IsNullOrEmpty(frm.txtkeypadvalue.Text))
                        typedAmount = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                }

                txtrefundamt.Text = typedAmount.ToString("N2");
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page txtrefundamt_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void ProcessPartyBookingRefund(int refundMode)
        {
            try
            {
                DmnRefund dmnRefund = new DmnRefund();
                BllRefund bllRefund = new BllRefund();

                dmnRefund.PartyBookingId = BookingId;
                if (PaymentSettleStatusData == Convert.ToInt32(PaymentSettlementStatus.RefundDue))
                {
                    decimal balAmt = decimal.TryParse(lblBalAmt.Text, out decimal ba) ? ba : 0;
                    dmnRefund.RefundAmount = Math.Abs(balAmt);
                }
                else
                    dmnRefund.RefundAmount = Convert.ToDecimal(txtrefundamt.Text);

                dmnRefund.RefundMode = refundMode;
                dmnRefund.RefundReason = 5;
                dmnRefund.ReasonNote = txtRenote.Text;
                dmnRefund.SalesMasterId = SalesMasterId;

                dmnRefund.CreatedBy = FrmDashboard.UserId;
                dmnRefund.BranchId = FrmDashboard.BranchId;
                dmnRefund.Status = 1;
                dmnRefund.DBname = GameDB;
                dmnRefund.CreatedDateTime = DateTime.Now;

                var data = bllRefund.AddRefundBooking("prdInsertRefund_PartyBooking", dmnRefund);

                if (data.Id > 0)
                {
                    pnl_bookingcancel.Visible = false;
                    HideBlurPopup(pnl_bookingcancel);

                    if (PaymentSettleStatusData != Convert.ToInt32(PaymentSettlementStatus.RefundDue))
                    {
                        ClearAll();
                        CMessageBox.Show("Refund Amount Completed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page ProcessPartyBookingRefund()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void btncash_refund_Click(object sender, EventArgs e)
        {
            ProcessPartyBookingRefund(Convert.ToInt32(PaymentType.CASH));
        }

        private void btnupi_refund_Click(object sender, EventArgs e)
        {
            ProcessPartyBookingRefund(Convert.ToInt32(PaymentType.UPI));
        }

        private void btndebit_refund_Click(object sender, EventArgs e)
        {
            ProcessPartyBookingRefund(Convert.ToInt32(PaymentType.DEBITCARD));
        }

        private void btninterbanking_refund_Click(object sender, EventArgs e)
        {
            ProcessPartyBookingRefund(Convert.ToInt32(PaymentType.BANKINGONLINE));
        }

        private void btncredit_refund_Click(object sender, EventArgs e)
        {
            ProcessPartyBookingRefund(Convert.ToInt32(PaymentType.CREDITCARD));
        }

        private void btncakereg_cls_Click(object sender, EventArgs e)
        {
            pnl_cakeregister.Visible = false;
            HideBlurPopup(pnl_cakeregister);
        }

        private void btn_bookingvoid_Click(object sender, EventArgs e)
        {
            try
            {
                BllPartyBooking bll = new BllPartyBooking();
                DmnPartyBooking booking = new DmnPartyBooking();

                booking.PartyBookingId = GetInt(txtpartybookingid);
                booking.CreatedBy = FrmLogin.EmployeeId;
                booking.BranchId = FrmDashboard.BranchId;
                booking.Status = 0;

                var result = bll.VoidData("prdVoid_PartyBooking", booking);

                if (result.PartyBookingId > 0)
                {
                    pnl_bookingcancel.Visible = false;
                    HideBlurPopup(pnl_bookingcancel);
                    CMessageBox.Show("Booking Voided Successfully");
                    ClearAll();
                }
                else
                {
                    CMessageBox.Show("Void Failed");
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btn_bookingvoid_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        #endregion

        #region Main Datagridview 
        private void dgv_product_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {


                if (dgv_product.CurrentCell.ColumnIndex == pqtyno)
                {
                    TextBox tb = e.Control as TextBox;
                    if (tb != null)
                    {
                        tb.KeyPress -= QuantityColumn_KeyPress;
                        tb.KeyPress += QuantityColumn_KeyPress;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page dgv_product_EditingControlShowing()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void QuantityColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void dgv_product_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {


                if (dgv_product.CurrentCell == null) return;

                int qtyCol = pqtyno;
                if (dgv_product.CurrentCell.ColumnIndex == qtyCol)
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                    {
                        e.Handled = true;
                    }
                    if (e.KeyChar == '.' && dgv_product.CurrentCell.Value != null && dgv_product.CurrentCell.Value.ToString().Contains("."))
                    {
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page dgv_product_KeyPress()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void dgv_product_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                var row = dgv_product.Rows[e.RowIndex];
                int actionCol = pactionno;

                if (e.ColumnIndex == actionCol)
                {
                    int productType = Convert.ToInt32(row.Cells[PPackProductTypeno].Value ?? 0);
                    int defaultType = Convert.ToInt32(row.Cells[pPartydeafult].Value ?? 0);
                    string productName = Convert.ToString(row.Cells[pitemno].Value ?? "");
                    int partyPackDetailsId = Convert.ToInt32(row.Cells[proPartyPackDetailsId].Value ?? 0);

                    if (defaultType == 1)
                    {
                        CMessageBox.Show($"Cannot delete default item '{productName}'!",
                            "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (productType == (int)PackItemType.ExtraChargeProduct)
                    {
                        if (partyPackDetailsId > 0)
                        {
                            var result = CMessageBox.Show(
                            $"Are you sure you want to delete Extra Charge '{productName}'?",
                            "Confirm Delete",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                            if (result != DialogResult.Yes) return;
                        }

                        ResetDiscount();

                        BookingItemAdjustment item = new BookingItemAdjustment()
                        {
                            SlNo = bookingItemAdjustments.Count + 1,
                            Product = productName,
                            Qty = Convert.ToDecimal(row.Cells[pqtyno].Value ?? 0),
                            Price = Convert.ToDecimal(row.Cells[pnetamt].Value ?? 0),
                            Amount = Convert.ToDecimal(row.Cells[pPrice].Value ?? 0),
                            ProductCategory = Convert.ToString(row.Cells[pcategmenuno].Value ?? ""),
                            ProductType = productType,
                            ProductId = Convert.ToInt32(row.Cells[pProductid].Value ?? 0),
                            SaleDetailId = Convert.ToInt32(row.Cells[psaledetailidno].Value ?? 0),
                            PartyBookingDetailsid = partyPackDetailsId
                        };

                        bookingItemAdjustments.Add(item);

                        dgv_product.Rows.RemoveAt(e.RowIndex);

                        CalculateDiscountPer();
                        RecalculateAllRowsAndTotals();

                        return;
                    }

                    // NORMAL PRODUCT DELETE 

                    if (partyPackDetailsId > 0)
                    {
                        var result = CMessageBox.Show(
                            $"Are you sure you want to delete the product '{productName}'?",
                            "Confirm Delete",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question);

                        if (result != DialogResult.Yes) return;

                        ResetDiscount();

                        BookingItemAdjustment item = new BookingItemAdjustment()
                        {
                            SlNo = bookingItemAdjustments.Count + 1,
                            Product = productName,
                            Qty = Convert.ToDecimal(row.Cells[pqtyno].Value ?? 0),
                            Price = Convert.ToDecimal(row.Cells[pnetamt].Value ?? 0),
                            Amount = Convert.ToDecimal(row.Cells[pPrice].Value ?? 0),
                            ProductCategory = Convert.ToString(row.Cells[pcategmenuno].Value ?? ""),
                            ProductType = productType,
                            ProductId = Convert.ToInt32(row.Cells[pProductid].Value ?? 0),
                            SaleDetailId = Convert.ToInt32(row.Cells[psaledetailidno].Value ?? 0),
                            PartyBookingDetailsid = partyPackDetailsId
                        };

                        bookingItemAdjustments.Add(item);
                    }

                    dgv_product.Rows.RemoveAt(e.RowIndex);

                    CalculateDiscountPer();
                    RecalculateAllRowsAndTotals();
                }
                else if (e.ColumnIndex == pqtyno)
                {
                    // Package-default rows are fixed by the package setup. Clicking
                    // their quantity must not open the keypad or alter their product type.
                    int defaultType = Convert.ToInt32(row.Cells[pPartydeafult].Value ?? 0);
                    if (defaultType == 1)
                        return;

                    decimal kdata = 0;
                    using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.NoPercentageReasonNoDecimal)))
                    {
                        frm.ShowDialog();

                        if (!string.IsNullOrEmpty(frm.txtkeypadvalue.Text))
                            kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);

                        txtdiscountreason.Text = frm.txtdesc.Text.Trim() == "Enter Remarks Here....." ? "" : frm.txtdesc.Text;
                    }
                    if (kdata > 0)
                    {
                        row.Cells[pqtyno].Value = kdata;
                        ResetDiscount();

                        decimal qty = 0;
                        decimal.TryParse(Convert.ToString(row.Cells[pqtyno].Value ?? "0"), out qty);
                        row.Cells[pqtyno].Value = Math.Round(qty, 2);

                        int productType = Convert.ToInt32(row.Cells[PPackProductTypeno].Value ?? 0);
                        int productId = Convert.ToInt32(row.Cells[pProductid].Value ?? 0);

                        CalculateAndApplyTax(e.RowIndex, productType, productId);
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page dgv_product_CellClick()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }



        #endregion

        #region  Payment Settled

        private void btn_paynow_ref_add_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_Paymentsettled.Visible = false;
                HideBlurPopup(pnl_Paymentsettled);

                if (PaymentSettleStatusData != Convert.ToInt32(PaymentSettlementStatus.Settled))
                {
                    pnlPaymode.Visible = true;
                    pnlPaymode.Visible = true;ShowBlurPopup(pnlPaymode);
                    pnlPaymode.BringToFront();
                    pnlPaymode.Left = (this.ClientSize.Width - pnlPaymode.Width) / 2;
                    pnlPaymode.Top = (this.ClientSize.Height - pnlPaymode.Height) / 2;
                }
                else
                {
                    Paybill();
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btn_paynow_ref_add_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void btn_Paymentsettled_cls_Click(object sender, EventArgs e)
        {
            pnl_Paymentsettled.Visible = false;
            HideBlurPopup(pnl_Paymentsettled);
        }

        private void btn_ref_add_cancel_Click(object sender, EventArgs e)
        {
            pnl_Paymentsettled.Visible = false;
            HideBlurPopup(pnl_Paymentsettled);
        }
        private decimal GetAdjustmentTotal()
        {
            decimal total = 0;

            foreach (var item in bookingItemAdjustments)
            {
                total += item.Price;
            }

            return total;
        }
        private void LoadAdjustmentGrid()
        {
            try
            {
                dgv_cancelleditems.Rows.Clear();

                int sl = 1;

                foreach (var item in bookingItemAdjustments)
                {
                    dgv_cancelleditems.Rows.Add(
                        sl,
                        item.Product,
                        item.ProductCategory,
                        item.Qty,
                        item.Price
                    );

                    sl++;
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page LoadAdjustmentGrid()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void UpdateBalancePayableCaption(decimal balanceAmount)
        {
            if (lblbalapay == null || lblbalapay.IsDisposed)
                return;

            if (balanceAmount < -0.009m)
                lblbalapay.Text = "Refund Due";
            else if (balanceAmount > 0.009m)
                lblbalapay.Text = "Additional Payment Due";
            else
                lblbalapay.Text = "Balance";
        }

        private void ShowPaymentSettlementPanel(
            decimal netAmount,
            decimal advancePaid,
            decimal paymentReceived,
            decimal refundGiven)
        {
            try
            {
                // 🔹 PANEL UI
                pnl_Paymentsettled.Visible = true;
                pnl_Paymentsettled.Visible = true;ShowBlurPopup(pnl_Paymentsettled);
                pnl_Paymentsettled.BringToFront();
                pnl_Paymentsettled.Left = (this.ClientSize.Width - pnl_Paymentsettled.Width) / 2;
                pnl_Paymentsettled.Top = (this.ClientSize.Height - pnl_Paymentsettled.Height) / 2;

                // 🔹 BASIC INFO
                lbl_ps_partyname.Text = lblpackname.Text;
                lbl_ps_partydatetime.Text = dtpEventDate_From.Text + " " + dtpEventTime_From.Text;
                lbl_ps_phone.Text = txtph.Text;
                lbl_ps_cusname.Text = txtname.Text;
                lbl_ps_eventname.Text = cmbevent.Text;

                // 🔹 LOAD ADJUSTMENTS
                if (bookingItemAdjustments.Count > 0)
                    LoadAdjustmentGrid();

                decimal adjAmount = bookingItemAdjustments.Sum(x => x.Price);
                decimal oldNetAmount = netAmount + adjAmount;

                lbl_sum_netamt.Text = oldNetAmount.ToString("0.00");
                lbl_sum_newnetamt.Text = netAmount.ToString("0.00");
                lbl_sum_advamt.Text = advancePaid.ToString("0.00");

                lbl_sum_netamt.ForeColor = Color.Black;
                lbl_sum_newnetamt.ForeColor = Color.Blue;
                lbl_sum_advamt.ForeColor = Color.DarkGreen;

                pnl_ref_ad_msg.Visible = false;
                dgv_cancelleditems.Visible = false;

                decimal totalPaid = advancePaid + paymentReceived;
                decimal diff = netAmount - totalPaid;

                // Keep the panel title and balance caption aligned with the settlement state.
                UpdateBalancePayableCaption(diff);
                if (diff < -0.009m)
                    lbl_Paymentsettled.Text = "Refund Settlement";
                else if (diff > 0.009m)
                    lbl_Paymentsettled.Text = "Additional Payment";
                else
                    lbl_Paymentsettled.Text = "Payment Settled";

                // 🔹 COLOR INDICATOR
                if (diff < 0)
                    lbl_sum_adjamt.ForeColor = Color.Red;
                else if (diff > 0)
                    lbl_sum_adjamt.ForeColor = Color.DarkOrange;
                else
                    lbl_sum_adjamt.ForeColor = Color.Green;


                // CASE 1: FULLY SETTLED
                if (diff == 0)
                {
                    lbl_paymentsubhead.Text = "Payment Settled";

                    lbl_sum_dueamt.Text = "0.00";
                    lbl_sum_revamt.Text = "0.00";
                    lbl_sum_adjamt.Text = "0.00";

                    pnl_ref_ad_msg.Visible = true;
                    pnl_ref_ad_msg.BackColor = Color.Honeydew;

                    lblrefund_ad_msg.Text = "Payment Settled Successfully";
                    lblrefund_ad_msg.ForeColor = Color.Green;

                    btn_paynow_ref_add.Text = "Update";
                    PaymentSettleStatusData = Convert.ToInt32(PaymentSettlementStatus.Settled);
                }

                // CASE 2: REFUND REQUIRED

                else if (diff < 0)
                {
                    decimal refundAmt = Math.Abs(diff);

                    dgv_cancelleditems.Visible = true;

                    lbl_paymentsubhead.Text = "Refund Required";

                    lbl_sum_adjamt.Text = diff.ToString("0.00");
                    lbl_sum_revamt.Text = "0.00";
                    lbl_sum_dueamt.Text = "0.00";

                    pnl_ref_ad_msg.Visible = true;
                    pnl_ref_ad_msg.BackColor = Color.MistyRose;

                    lblrefund_ad_msg.Text = $"Refund ₹ {refundAmt:0.00} to customer.";
                    lblrefund_ad_msg.ForeColor = Color.Red;

                    btn_paynow_ref_add.Text = "Refund";
                    PaymentSettleStatusData = Convert.ToInt32(PaymentSettlementStatus.RefundDue);
                }

                // CASE 3: ADDITIONAL PAYMENT
                else
                {
                    decimal dueAmt = diff;

                    lbl_paymentsubhead.Text = "Additional Payment Required";

                    lbl_sum_adjamt.Text = "+" + dueAmt.ToString("0.00");
                    lbl_sum_dueamt.Text = dueAmt.ToString("0.00");
                    lbl_sum_revamt.Text = dueAmt.ToString("0.00");

                    pnl_ref_ad_msg.Visible = true;
                    pnl_ref_ad_msg.BackColor = Color.LemonChiffon;

                    lblrefund_ad_msg.Text = $"₹ {dueAmt:0.00} due from customer.";
                    lblrefund_ad_msg.ForeColor = Color.DarkOrange;

                    btn_paynow_ref_add.Text = "Pay Now";
                    PaymentSettleStatusData = Convert.ToInt32(PaymentSettlementStatus.AdditionalDue);
                }

                // 🔹 SAFE PANEL ADD
                if (!tlp_Paymentsettled_cntrl.Controls.Contains(pnl_ref_ad_msg))
                {
                    tlp_Paymentsettled_cntrl.Controls.Add(pnl_ref_ad_msg, 1, 7);
                    pnl_ref_ad_msg.Dock = DockStyle.Fill;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page ShowPaymentSettlementPanel()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        #endregion

        #region Net Amount And Tax Calculation

        public void ResetDiscount()
        {
            txtTotalDiscountAmt.Text = "0";
            discountper.Text = "0";
            PriceChangecalculation();
        }


        private void btnadd_cus_Click(object sender, EventArgs e)
        {
            cmbevent.SelectedIndex = 0;
            txteventdesc.Text = "Birthday Party";
            txtaddress.Text = "Chalakudy, Kerala";
            txtemail.Text = "Rahul@gmail.com";
            txtph.Text = "9876543210";
            txtname.Text = "Rahul";
        }
        private void CalculateAndApplyTax(int rowIndex, int productType, int productId)
        {
            try
            {
                var row = dgv_product.Rows[rowIndex];
                if (row.IsNewRow) return;

                decimal qty = Convert.ToDecimal(row.Cells[pqtyno].Value ?? 0);
                decimal price = Convert.ToDecimal(row.Cells[pPrice].Value ?? 0);
                decimal discountPer = Convert.ToDecimal(row.Cells[pdiscper].Value ?? 0);
                int taxSettingId = Convert.ToInt32(row.Cells[ptaxsettingid].Value ?? 0);
                string taxMode = Convert.ToString(row.Cells[ptax].Value ?? "");

                decimal igst = 0, cgst = 0, sgst = 0, vat = 0;
                decimal totalPercent = 0;
                string taxType = "";

                string tableName = (productType == (int)PackItemType.Card ||
                                    productType == (int)PackItemType.Gift)
                                    ? $"[{GameDB}].dbo.tblTaxSettings"
                                    : "tblTaxSettings";

                if (taxSettingId > 0)
                {
                    BllSearch<DmnTaxSettings> _bllTax = new BllSearch<DmnTaxSettings>();
                    DmnSearch dmnTax = new DmnSearch
                    {
                        Select = "TaxType, IgstPrcnt, CgstPrcnt, SgstPrcnt, VatPrcnt",
                        Table = tableName,
                        WhereNdGroupBy = $"WHERE Status=1 AND TaxSettingsId={taxSettingId}"
                    };

                    var taxSettings = _bllTax.GetSearchData("prdSearch", dmnTax);
                    if (taxSettings.Count > 0)
                    {
                        igst = taxSettings[0].IgstPrcnt;
                        cgst = taxSettings[0].CgstPrcnt;
                        sgst = taxSettings[0].SgstPrcnt;
                        vat = taxSettings[0].VatPrcnt;
                        taxType = taxSettings[0].TaxType;
                        totalPercent = (taxType == "GST") ? igst : vat;
                    }
                }

                // Keep full decimal precision during calculations. For inclusive tax,
                // extract tax first, discount the taxable amount, then recalculate tax.
                decimal gross = qty * price;
                decimal baseAmt = 0m, taxAmt = 0m, netAmt = 0m;
                decimal discountAmt = 0m;

                if (taxMode == "Inclusive")
                {
                    decimal baseBeforeDiscount = gross / (1m + (totalPercent / 100m));
                    discountAmt = baseBeforeDiscount * discountPer / 100m;
                    baseAmt = baseBeforeDiscount - discountAmt;
                    taxAmt = baseAmt * totalPercent / 100m;
                    netAmt = baseAmt + taxAmt;
                }
                else
                {
                    discountAmt = gross * discountPer / 100m;
                    baseAmt = gross - discountAmt;
                    taxAmt = baseAmt * totalPercent / 100m;
                    netAmt = baseAmt + taxAmt;
                }

                row.Cells[pAmount].Value = gross.ToString("N2");
                row.Cells[pdisamt].Value = discountAmt.ToString("N2");
                row.Cells[ppretaxamt].Value = baseAmt.ToString("N2");
                row.Cells[ptaxamt].Value = taxAmt.ToString("N2");
                row.Cells[pnetamt].Value = netAmt.ToString("N2");
                row.Cells[ptaxtype].Value = taxType;
                row.Cells[ptaxper].Value = totalPercent.ToString("N2");

                if (taxType == "GST")
                {
                    row.Cells[pigstamt].Value = (totalPercent > 0
                        ? Math.Round(taxAmt * igst / totalPercent, 2, MidpointRounding.AwayFromZero).ToString("N2")
                        : (cgst + sgst).ToString("N2"));

                    row.Cells[pvatamt].Value = 0.ToString("N2");
                }
                else
                {
                    row.Cells[pvatamt].Value = taxAmt.ToString("N2");
                    row.Cells[pigstamt].Value = 0.ToString("N2");
                }

                RecalculateAllRowsAndTotals();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page CalculateAndApplyTax()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void RecalculateAllRowsAndTotals()
        {
            try
            {
                decimal preTax_card = 0, tax_card = 0, net_card = 0;
                decimal preTax_cake = 0, tax_cake = 0, net_cake = 0;
                decimal preTax_gift = 0, tax_gift = 0, net_gift = 0;
                decimal preTax_pack = 0, tax_pack = 0, net_pack = 0;
                decimal preTax_extra = 0, tax_extra = 0, net_extra = 0;
                decimal preTax_food = 0, tax_food = 0, net_food = 0;
                decimal preTax_venue = 0, tax_venue = 0, net_venue = 0;
                bool hasVenueRows = false;

                foreach (DataGridViewRow row in dgv_product.Rows)
                {
                    int productType = 0;
                    int.TryParse(row.Cells[PPackProductTypeno].Value?.ToString(), out productType);

                    //  Card, Gift, Cake,ExtraChargeProduct
                    if (productType != (int)PackItemType.Card &&
                        productType != (int)PackItemType.Gift &&
                        productType != (int)PackItemType.Menu &&
                        productType != (int)PackItemType.Cake &&
                        productType != (int)PackItemType.ExtraChargeProduct &&
                        productType != (int)PackItemType.FoodItem &&
                        productType != (int)PackItemType.Venue)
                    {
                        continue;
                    }

                    decimal qty = Convert.ToDecimal(row.Cells[pqtyno].Value ?? 0);
                    decimal price = Convert.ToDecimal(row.Cells[pPrice].Value ?? 0);
                    decimal discountPer = Convert.ToDecimal(row.Cells[pdiscper].Value ?? 0);
                    int taxSettingId = Convert.ToInt32(row.Cells[ptaxsettingid].Value ?? 0);
                    string taxMode = Convert.ToString(row.Cells[ptax].Value ?? "");

                    decimal igst = 0, cgst = 0, sgst = 0, vat = 0;
                    decimal totalPercent = 0;
                    string taxType = "";

                    string tableName = (productType == (int)PackItemType.Card ||
                    productType == (int)PackItemType.Gift)
                    ? $"[{GameDB}].dbo.tblTaxSettings"
                    : "tblTaxSettings";

                    // Fetch tax settings
                    if (taxSettingId > 0)
                    {
                        BllSearch<DmnTaxSettings> _bllTax = new BllSearch<DmnTaxSettings>();
                        DmnSearch dmnTax = new DmnSearch
                        {
                            Select = "TaxType, IgstPrcnt, CgstPrcnt, SgstPrcnt, VatPrcnt",
                            Table = tableName,
                            WhereNdGroupBy = $"WHERE Status=1 AND TaxSettingsId={taxSettingId}"
                        };

                        var taxSettings = _bllTax.GetSearchData("prdSearch", dmnTax);

                        if (taxSettings.Count > 0)
                        {
                            igst = taxSettings[0].IgstPrcnt;
                            cgst = taxSettings[0].CgstPrcnt;
                            sgst = taxSettings[0].SgstPrcnt;
                            vat = taxSettings[0].VatPrcnt;

                            taxType = taxSettings[0].TaxType;
                            totalPercent = (taxType == "GST") ? igst : vat;
                        }
                    }

                    // Do not round intermediate values; round only for display/storage.
                    // Inclusive items are discounted on their tax-exclusive base.
                    decimal gross = qty * price;
                    decimal baseAmt = 0m, taxAmt = 0m, netAmt = 0m;
                    decimal discountAmt = 0m;

                    if (taxMode == "Inclusive")
                    {
                        decimal baseBeforeDiscount = gross / (1m + (totalPercent / 100m));
                        discountAmt = baseBeforeDiscount * discountPer / 100m;
                        baseAmt = baseBeforeDiscount - discountAmt;
                        taxAmt = baseAmt * totalPercent / 100m;
                        netAmt = baseAmt + taxAmt;
                    }
                    else
                    {
                        discountAmt = gross * discountPer / 100m;
                        baseAmt = gross - discountAmt;
                        taxAmt = baseAmt * totalPercent / 100m;
                        netAmt = baseAmt + taxAmt;
                    }

                    // ------------------------
                    // ADD TO CATEGORY TOTALS
                    // ------------------------
                    switch ((PackItemType)productType)
                    {
                        case PackItemType.Pack:
                            preTax_pack += baseAmt;
                            tax_pack += taxAmt;
                            net_pack += netAmt;
                            break;
                        case PackItemType.Cake:
                            preTax_cake += baseAmt;
                            tax_cake += taxAmt;
                            net_cake += netAmt;
                            break;
                        case PackItemType.Card:
                            preTax_card += baseAmt;
                            tax_card += taxAmt;
                            net_card += netAmt;
                            break;
                        case PackItemType.Gift:
                            preTax_gift += baseAmt;
                            tax_gift += taxAmt;
                            net_gift += netAmt;
                            break;
                        case PackItemType.ExtraChargeProduct:
                            preTax_extra += baseAmt;
                            tax_extra += taxAmt;
                            net_extra += netAmt;
                            break;
                        case PackItemType.Venue:
                            preTax_venue += baseAmt;
                            tax_venue += taxAmt;
                            net_venue += netAmt;
                            hasVenueRows = true;
                            break;
                        // Keep legacy FoodItem handling unchanged; Menu is an additional type.
                        case PackItemType.FoodItem:
                        case PackItemType.Menu:
                            preTax_food += baseAmt;
                            tax_food += taxAmt;
                            net_food += netAmt;
                            break;
                    }
                }

                txtPreTaxAmount_card.Text = preTax_card.ToString("N2");
                txtTotalTaxAmount_card.Text = tax_card.ToString("N2");
                txtVATAmt_card.Text = tax_card.ToString("N2");
                txtIGSTAmt_card.Text = tax_card.ToString("N2");
                txtCardGrandTotal.Text = net_card.ToString("N2");

                txtPreTaxAmount_cake.Text = preTax_cake.ToString("N2");
                txtTotalTaxAmount_cake.Text = tax_cake.ToString("N2");
                txtVATAmt_cake.Text = tax_cake.ToString("N2");
                txtIGSTAmt_cake.Text = tax_cake.ToString("N2");
                txtCakeGrandTotal.Text = net_cake.ToString("N2");

                txtPreTaxAmount_gift.Text = preTax_gift.ToString("N2");
                txtTotalTaxAmount_gift.Text = tax_gift.ToString("N2");
                txtVATAmt_gift.Text = tax_gift.ToString("N2");
                txtIGSTAmt_gift.Text = tax_gift.ToString("N2");
                txtGiftGrandTotal.Text = net_gift.ToString("N2");

                // EXTRA CHARGE
                txtPreTaxAmount_extracharge.Text = preTax_extra.ToString("N2");
                txtTotalTaxAmount_extracharge.Text = tax_extra.ToString("N2");
                txtVatamt_extracharge.Text = tax_extra.ToString("N2");
                txtIGSTAmt_extracharge.Text = tax_extra.ToString("N2");
                txtExtrachargeGrandTotal.Text = net_extra.ToString("N2");

                // PARTY MENU FOOD
                txtPreTaxAmount_extracharge.Text = (preTax_extra + preTax_food).ToString("N2");
                txtTotalTaxAmount_extracharge.Text = (tax_extra + tax_food).ToString("N2");
                txtVatamt_extracharge.Text = (tax_extra + tax_food).ToString("N2");
                txtIGSTAmt_extracharge.Text = (tax_extra + tax_food).ToString("N2");
                txtExtrachargeGrandTotal.Text = (net_extra + net_food).ToString("N2");

                // Do not clear venue panel values until a Venue row has actually been
                // submitted. Once present, keep the panel totals in sync with the grid row.
                if (hasVenueRows)
                {
                    txtPreTaxAmount_venue.Text = preTax_venue.ToString("N2");
                    txtTotalTaxAmount_venue.Text = tax_venue.ToString("N2");
                    txtGrandTotal_venue.Text = net_venue.ToString("N2");
                }

                CalculateDiscountPer();
                CalculateNetAmount();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page RecalculateAllRowsAndTotals()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        public void CalculateNetAmount()
        {
            if (isUpdating) return;

            try
            {
                isUpdating = true;

                decimal GetDecimal(TextBox txt)
                {
                    decimal.TryParse(txt.Text, out decimal val);
                    return val;
                }

                decimal Tdiacounamt = GetDecimal(txtTotalDiscountAmt);
                decimal discountPer = GetDecimal(discountper);

                // ------------------------
                // PACK
                // ------------------------
                decimal packPreTax = GetDecimal(txtPreTaxAmount_pack);
                decimal packTax = GetDecimal(txtTotalTaxAmount_pack);
                decimal packVAT = GetDecimal(txtVatamt_pack);
                decimal packIGST = GetDecimal(txtIGSTAmt_pack);
                decimal packTotal = GetDecimal(txtGrandTotal_pack);

                // ------------------------
                // CAKE
                // ------------------------
                decimal cakePreTax = GetDecimal(txtPreTaxAmount_cake);
                decimal cakeTax = GetDecimal(txtTotalTaxAmount_cake);
                decimal cakeVAT = GetDecimal(txtVATAmt_cake);
                decimal cakeIGST = GetDecimal(txtIGSTAmt_cake);
                decimal cakeTotal = GetDecimal(txtCakeGrandTotal);

                // ------------------------
                // CARD
                // ------------------------
                decimal cardPreTax = GetDecimal(txtPreTaxAmount_card);
                decimal cardTax = GetDecimal(txtTotalTaxAmount_card);
                decimal cardVAT = GetDecimal(txtVATAmt_card);
                decimal cardIGST = GetDecimal(txtIGSTAmt_card);
                decimal cardTotal = GetDecimal(txtCardGrandTotal);

                // ------------------------
                // GIFT
                // ------------------------
                decimal giftPreTax = GetDecimal(txtPreTaxAmount_gift);
                decimal giftTax = GetDecimal(txtTotalTaxAmount_gift);
                decimal giftVAT = GetDecimal(txtVATAmt_gift);
                decimal giftIGST = GetDecimal(txtIGSTAmt_gift);
                decimal giftTotal = GetDecimal(txtGiftGrandTotal);

                // ------------------------
                // EXTRA CHARGE ✅ NEW
                // ------------------------
                decimal extraPreTax = GetDecimal(txtPreTaxAmount_extracharge);
                decimal extraTax = GetDecimal(txtTotalTaxAmount_extracharge);
                decimal extraVAT = GetDecimal(txtVatamt_extracharge);
                decimal extraIGST = GetDecimal(txtIGSTAmt_extracharge);
                decimal extraTotal = GetDecimal(txtExtrachargeGrandTotal);


                decimal totalPreTax = packPreTax + cakePreTax + cardPreTax + giftPreTax + extraPreTax;
                decimal totalTax = packTax + cakeTax + cardTax + giftTax + extraTax;
                decimal totalVAT = packVAT + cakeVAT + cardVAT + giftVAT + extraVAT;
                decimal totalIGST = packIGST + cakeIGST + cardIGST + giftIGST + extraIGST;

                // Venue is counted only after Venue Submit creates/updates its grid row.
                // Do not include values still sitting in the venue editor, or a deleted
                // Venue row would continue affecting the bill total.
                List<DataGridViewRow> venueRows = dgv_product.Rows
                    .Cast<DataGridViewRow>()
                    .Where(row => !row.IsNewRow &&
                                  Convert.ToInt32(row.Cells[PPackProductTypeno].Value ?? 0) == (int)PackItemType.Venue)
                    .ToList();

                decimal venuePreTax = venueRows.Sum(row => Convert.ToDecimal(row.Cells[ppretaxamt].Value ?? 0));
                decimal venueTax = venueRows.Sum(row => Convert.ToDecimal(row.Cells[ptaxamt].Value ?? 0));
                decimal venueVAT = venueRows.Sum(row => Convert.ToDecimal(row.Cells[pvatamt].Value ?? 0));
                decimal venueIGST = venueRows.Sum(row => Convert.ToDecimal(row.Cells[pigstamt].Value ?? 0));
                decimal venueTotal = venueRows.Sum(row => Convert.ToDecimal(row.Cells[pnetamt].Value ?? 0));

                decimal subtotal = packTotal + cakeTotal + cardTotal + giftTotal + extraTotal + venueTotal;
                totalPreTax += venuePreTax;
                totalTax += venueTax;
                totalVAT += venueVAT;
                totalIGST += venueIGST;

                // The discount amount is calculated on the tax-exclusive base.
                // Also subtract the tax reduction from the gross subtotal so the
                // payable total agrees with the recalculated tax-inclusive amount.
                decimal discountTax = Math.Round(
                    totalTax * discountPer / 100m,
                    2,
                    MidpointRounding.AwayFromZero);
                decimal finalTax = Math.Round(
                    totalTax - discountTax,
                    2,
                    MidpointRounding.AwayFromZero);

                decimal finalNet = subtotal - Tdiacounamt - discountTax;

                txtaddcharge.Text = extraTotal.ToString("N2");

                txtPreTaxAmount.Text = totalPreTax.ToString("N2");
                txtTotalTaxAmount.Text = finalTax.ToString("N2");

                // Keep the VAT/IGST summary boxes in sync with the discounted tax.
                // These values previously displayed the pre-discount tax totals.
                // Preserve full decimal precision; round only when displaying with N2.
                decimal finalVAT = totalVAT - (totalVAT * discountPer / 100m);
                decimal finalIGST = totalIGST - (totalIGST * discountPer / 100m);

                txtTotalVATAmt.Text = finalVAT.ToString("N2");
                txtTotalIGSTAmt.Text = finalIGST.ToString("N2");

                txtGrandTotal.Text = finalNet.ToString("N2");
                lblNetAmt.Text = finalNet.ToString("N2");

                decimal rcvAmt = GetDecimal(txtRcvAmt);
                decimal totalAdvance = dbAdvanceAmount;

                lbladvamtdata.Text = totalAdvance.ToString("N2");
                decimal balanceAmount = finalNet - totalAdvance;
                lblBalAmt.Text = balanceAmount.ToString("N2");
                UpdateBalancePayableCaption(balanceAmount);
            }
            catch (Exception ex)
            {
                clsgen.LogException("PartyBooking - CalculateNetAmount()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
            finally
            {
                isUpdating = false;
            }
        }
        private void GetGrandTotal_All()
        {
            try
            {
                decimal grandTotal = 0;
                decimal packTotal = 0, discounttotal = 0;
                decimal.TryParse(txtGrandTotal_pack.Text, out packTotal);
                decimal.TryParse(txtTotalDiscountAmt.Text, out discounttotal);
                grandTotal += packTotal;

                decimal cardTotal = 0, giftTotal = 0, cakeTotal = 0, menuTotal = 0, addcharge = 0, venueTotal = 0, Tottax = 0;

                foreach (DataGridViewRow row in dgv_product.Rows)
                {
                    if (row.IsNewRow) continue;

                    int productType = Convert.ToInt32(row.Cells[PPackProductTypeno].Value ?? 0);
                    decimal netAmt = Convert.ToDecimal(row.Cells[pnetamt].Value ?? 0);
                    decimal taxAmt = Convert.ToDecimal(row.Cells[ptaxamt].Value ?? 0);

                    switch ((PackItemType)productType)
                    {
                        case PackItemType.Card:
                            cardTotal += netAmt;
                            Tottax += taxAmt;
                            break;
                        case PackItemType.Gift:
                            giftTotal += netAmt;
                            Tottax += taxAmt;
                            break;
                        case PackItemType.Cake:
                            cakeTotal += netAmt;
                            Tottax += taxAmt;
                            break;
                        case PackItemType.Menu:
                            menuTotal += netAmt;
                            Tottax += taxAmt;
                            break;
                        case PackItemType.ExtraChargeProduct:
                            addcharge += netAmt;
                            Tottax += taxAmt;
                            break;
                        case PackItemType.Venue:
                            venueTotal += netAmt;
                            Tottax += taxAmt;
                            break;
                    }
                }

                // Venue is included only when Venue Submit has placed it in the grid.
                grandTotal += cardTotal + giftTotal + cakeTotal + menuTotal + addcharge + venueTotal;
                if (discounttotal > 0)
                {
                    originalGrandTotal = grandTotal - discounttotal;
                }
                else
                {

                    originalGrandTotal = grandTotal;
                    originalTotalTax = Tottax;
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page GetGrandTotal_All()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        #endregion

        #region Extra Charge
        private void btnotherarange_Click(object sender, EventArgs e)
        {
            if (!checkPartySelet())
                return;

            ResetExtraChargeFields();

            pnl_extracharge.Left = (this.ClientSize.Width - pnl_extracharge.Width) / 2;
            pnl_extracharge.Top = (this.ClientSize.Height - pnl_extracharge.Height) / 2;

            pnl_extracharge.Visible = true;
            pnl_extracharge.Visible = true;ShowBlurPopup(pnl_extracharge);
            pnl_extracharge.BringToFront();
            txtextracharge_amt.Focus();

            cmbtax.SelectedIndex = 0;
            cmbTaxType.SelectedIndex = 0;
            SetDefaultIGST();
        }
        private void SetDefaultIGST()
        {
            try
            {
                if (cmbIgstPrcnt.DataSource == null) return;

                bool found = false;

                for (int i = 0; i < cmbIgstPrcnt.Items.Count; i++)
                {
                    var item = cmbIgstPrcnt.Items[i] as DmnComboBox;

                    if (item != null)
                    {
                        decimal val = 0;

                        if (decimal.TryParse(Convert.ToString(item.Value), out val))
                        {
                            if (val == 18)
                            {
                                cmbIgstPrcnt.SelectedIndex = i;
                                found = true;
                                return;
                            }
                        }
                    }
                }


                if (!found)
                {
                    if (cmbIgstPrcnt.Items.Count > 0)
                        cmbIgstPrcnt.SelectedIndex = 0;
                    else
                        cmbIgstPrcnt.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("SetDefaultIGST()", ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void btn_ecclose_Click(object sender, EventArgs e)
        {
            pnl_extracharge.Visible = false;
            HideBlurPopup(pnl_extracharge);
        }
        public void Taxfill()
        {
            try
            {
                BllSearch<DmnComboBox> fiilgst = new BllSearch<DmnComboBox>();
                BllSearch<DmnComboBox> fiilvat = new BllSearch<DmnComboBox>();
                DmnSearch objSearch1 = new DmnSearch();
                DmnSearch objSearch2 = new DmnSearch();

                objSearch1.Select = "TaxSettingsId as Id, IgstPrcnt as Value";
                objSearch1.Table = "tblTaxSettings";
                objSearch1.WhereNdGroupBy = "where status=1 and TaxType='GST'";
                objSearch1.OrderBy = "order by value";

                objSearch2.Select = "TaxSettingsId as Id, VatPrcnt as Value";
                objSearch2.Table = "tblTaxSettings";
                objSearch2.WhereNdGroupBy = "where isnull(status,0)=1 and TaxType='VAT'";
                objSearch2.OrderBy = "order by value";

                var gridSource1 = fiilgst.GetSearchData("prdComboFill", objSearch1);
                var gridSource2 = fiilvat.GetSearchData("prdComboFill", objSearch2);

                cmbVatPrcnt.DataSource = gridSource2;
                cmbVatPrcnt.ValueMember = "Id";
                cmbVatPrcnt.DisplayMember = "Value";

                cmbIgstPrcnt.DataSource = gridSource1;
                cmbIgstPrcnt.ValueMember = "Id";
                cmbIgstPrcnt.DisplayMember = "Value";
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page Taxfill()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void cmbIgstPrcnt_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbIgstPrcnt.SelectedIndex > 0)
                {
                    BllSearch<DmnTaxSettings> bllSearch = new BllSearch<DmnTaxSettings>();
                    DmnSearch objSearch_tax = new DmnSearch();
                    objSearch_tax = new DmnSearch();
                    objSearch_tax.Select = "TaxSettingsId, SgstPrcnt, CgstPrcnt";
                    objSearch_tax.Table = "tblTaxSettings";
                    objSearch_tax.WhereNdGroupBy = "Where TaxSettingsId = " + cmbIgstPrcnt.SelectedValue;
                    objSearch_tax.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch_tax);
                    if (gridSource.Count > 0)
                    {
                        txtSgstPrcnt.Text = gridSource[0].SgstPrcnt.ToString();
                        txtCgstPrcnt.Text = gridSource[0].CgstPrcnt.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page cmbIgstPrcnt_SelectedIndexChanged()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void cmbTaxType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                int startRow = 2;
                int endRow = 3;
                int totalColumns = 4;

                for (int row = startRow; row <= endRow; row++)
                {
                    for (int col = 0; col < totalColumns; col++)
                    {
                        if ((row == 2 && col == 0) || (row == 2 && col == 1))
                            continue;

                        Control ctrl = tlp_add_contrl.GetControlFromPosition(col, row);
                        if (ctrl != null)
                        {
                            tlp_add_contrl.Controls.Remove(ctrl);
                        }
                    }
                }

                // 🔹 GST Layout
                if (cmbTaxType.Text == "GST")
                {

                    lbligst.Dock = DockStyle.Fill;
                    cmbIgstPrcnt.Dock = DockStyle.Fill;

                    lblcgst.Dock = DockStyle.Fill;
                    txtCgstPrcnt.Dock = DockStyle.Top;
                    lblsgst.Dock = DockStyle.Fill;
                    txtSgstPrcnt.Dock = DockStyle.Top;

                    tlp_add_contrl.Controls.Add(lbligst, 2, 2);
                    tlp_add_contrl.Controls.Add(cmbIgstPrcnt, 3, 2);

                    tlp_add_contrl.Controls.Add(lblcgst, 0, 3);
                    tlp_add_contrl.Controls.Add(txtCgstPrcnt, 1, 3);

                    tlp_add_contrl.Controls.Add(lblsgst, 2, 3);
                    tlp_add_contrl.Controls.Add(txtSgstPrcnt, 3, 3);

                    foreach (TextBox textBox in new TextBox[] { txtCgstPrcnt, txtSgstPrcnt })
                    {
                        ApplyRoundedTextBox(textBox, 10);
                        textBox.BorderStyle = BorderStyle.None;
                    }
                }

                // 🔹 VAT Layout
                else if (cmbTaxType.Text == "VAT")
                {
                    lblvat.Dock = DockStyle.Fill;
                    cmbVatPrcnt.Dock = DockStyle.Fill;

                    tlp_add_contrl.Controls.Add(lblvat, 2, 2);
                    tlp_add_contrl.Controls.Add(cmbVatPrcnt, 3, 2);

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page cmbTaxType_SelectedValueChanged()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        private bool ValidateExtraCharge()
        {
            try
            {
                // Description
                if (string.IsNullOrWhiteSpace(txtextracharge_des.Text))
                {
                    CMessageBox.Show("Please enter Description", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtextracharge_des.Focus();
                    return false;
                }
                // Amount
                //decimal amt;
                //if (string.IsNullOrWhiteSpace(txtextracharge_amt.Text) ||
                //    !decimal.TryParse(txtextracharge_amt.Text, out amt) || amt <= 0)
                //{
                //    CMessageBox.Show("Please enter valid Extra Charge Amount", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    txtextracharge_amt.Focus();
                //    return false;
                //}

                // Tax Mode (Inclusive / Exclusive)
                if (cmbtax.SelectedIndex < 0)
                {
                    CMessageBox.Show("Please select Tax Mode (Inclusive / Exclusive)", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbtax.Focus();
                    return false;
                }

                // Tax Type
                if (cmbTaxType.SelectedIndex < 0)
                {
                    CMessageBox.Show("Please select Tax Type (GST / VAT)", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbTaxType.Focus();
                    return false;
                }

                string taxType = cmbTaxType.Text.ToUpper();

                if (taxType == "GST")
                {
                    if (cmbIgstPrcnt.SelectedIndex <= 0)
                    {
                        CMessageBox.Show("Please select IGST %", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbIgstPrcnt.Focus();
                        return false;
                    }
                }
                else if (taxType == "VAT")
                {
                    if (cmbVatPrcnt.SelectedIndex <= 0)
                    {
                        CMessageBox.Show("Please select VAT %", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cmbVatPrcnt.Focus();
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page ValidateExtraCharge()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
                return false;
            }
        }



        private void btn_ec_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateExtraCharge())
                    return;

                ResetDiscount();

                decimal amt = Convert.ToDecimal(txtextracharge_amt.Text);

                // Tax Info
                string taxType = cmbTaxType.Text.ToUpper();     // GST / VAT
                string taxMode = cmbtax.Text;                   // Inclusive / Exclusive

                decimal taxPer = 0;
                int taxSettingId = 0;

                if (taxType == "GST")
                {
                    var item = cmbIgstPrcnt.SelectedItem as DmnComboBox;
                    decimal.TryParse(Convert.ToString(item?.Value), out taxPer);
                    taxSettingId = Convert.ToInt32(item?.Id ?? 0);
                }
                else if (taxType == "VAT")
                {
                    var item = cmbVatPrcnt.SelectedItem as DmnComboBox;
                    decimal.TryParse(Convert.ToString(item?.Value), out taxPer);
                    taxSettingId = Convert.ToInt32(item?.Id ?? 0);
                }

                // Add Row
                int rowIndex = dgv_product.Rows.Add();
                DataGridViewRow row = dgv_product.Rows[rowIndex];

                row.Cells[pslno].Value = rowIndex + 1;
                row.Cells[pitemno].Value = txtextracharge_des.Text;
                row.Cells[pcategmenuno].Value = "Extra Charge";
                row.Cells[pqtyno].Value = 1;
                row.Cells[pPartydeafult].Value = 2;

                row.Cells[pPrice].Value = amt;
                row.Cells[pAmount].Value = amt;

                row.Cells[pProductid].Value = 0;
                row.Cells[pmessageno].Value = "";
                row.Cells[premarksno].Value = "";

                row.Cells[PPackProductTypeno].Value = (int)PackItemType.ExtraChargeProduct;

                // Tax Fields
                row.Cells[ptaxamt].Value = 0;
                row.Cells[ptax].Value = taxMode;
                row.Cells[ptaxtype].Value = taxType;
                row.Cells[ptaxper].Value = taxPer;
                row.Cells[ptaxsettingid].Value = taxSettingId;

                row.Cells[ppretaxamt].Value = amt;
                row.Cells[pnetamt].Value = amt;

                row.Cells[pigstamt].Value = 0;
                row.Cells[pvatamt].Value = 0;
                row.Cells[phsncodeno].Value = txt_add_charge_hsncode.Text;

                row.ReadOnly = true;

                DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)row.Cells[pactionno];
                cellcolumntype.Value = Grid_Remove;
                cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;

                CalculateAndApplyTax(rowIndex, (int)PackItemType.ExtraChargeProduct, 0);

                pnl_extracharge.Visible = false;
                HideBlurPopup(pnl_extracharge);
                GetGrandTotal_All();
                RecalculateAllRowsAndTotals();
                if (Convert.ToDecimal(discountper.Text) > 0)
                    CalculateDiscountAmount();
                else
                    CalculateDiscountPer();
                CalculateNetAmount();

                ResetExtraChargeFields();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btn_ec_submit_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void ResetExtraChargeFields()
        {
            try
            {
                txt_add_charge_hsncode.Text = "";
                txtextracharge_amt.Text = "";
                txtextracharge_des.Text = "";
                cmbTaxType.SelectedIndex = 0;
                cmbtax.SelectedIndex = 0;
                cmbIgstPrcnt.SelectedIndex = -1;
                cmbVatPrcnt.SelectedIndex = -1;
                SetDefaultIGST();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page ResetExtraChargeFields()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void btnrefund_Click(object sender, EventArgs e)
        {
            try
            {
                decimal rcvAmt = 0;
                decimal.TryParse(txtRcvAmt.Text, out rcvAmt);

                decimal lblNet = decimal.TryParse(lblNetAmt.Text, out decimal na) ? na : 0;
                decimal advancePaid = decimal.TryParse(lbladvamtdata.Text, out decimal aa) ? aa : 0;
                decimal balamt = decimal.TryParse(lblBalAmt.Text, out decimal ba) ? ba : 0;

                decimal totalAdjustment = bookingItemAdjustments.Sum(a => a.Amount);
                decimal newNetAmount = lblNet + totalAdjustment;
                decimal totalPaid = advancePaid + rcvAmt;

                decimal refundAmount = totalPaid - newNetAmount;   // >0 = refund
                decimal finalBalance = newNetAmount - totalPaid;   // >0 = pending

                bool adjustmentExists = bookingItemAdjustments.Count > 0;
                bool netChanged = newNetAmount != dbNetAmount;
                bool paymentChanged = balamt > 0;
                bool refundRequired = refundAmount > 0;

                ShowPaymentSettlementPanel(lblNet, advancePaid, rcvAmt, refundAmount);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btnrefund_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void dgv_product_CellEndEdit_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

                if (e.ColumnIndex == pqtyno)
                {
                    var cell = dgv_product.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (cell.Value == null || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        cell.Value = 1;
                    }
                    else
                    {
                        if (Convert.ToDecimal(cell.Value) <= 0)
                        {
                            cell.Value = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page dgv_product_CellEndEdit_1()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void txtname_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Party_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void btn_ec_cancel_Click(object sender, EventArgs e)
        {
            pnl_extracharge.Visible = false;
            HideBlurPopup(pnl_extracharge);
        }

        private void txtname_TextChanged(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Party_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtph_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Party_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtemail_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Party_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtaddress_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Party_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txteventdesc_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Party_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtAdults_Click(object sender, EventArgs e)
        {
            if (_fixedPriceStatus || !txtAdults.Enabled)
                return;

            decimal kdata = 0;
            using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.NoPercentageReasonNoDecimal)))
            {
                frm.ShowDialog();

                if (!string.IsNullOrEmpty(frm.txtkeypadvalue.Text))
                    kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);

                txtdiscountreason.Text = frm.txtdesc.Text.Trim() == "Enter Remarks Here....." ? "" : frm.txtdesc.Text;
            }
            if (kdata > 0)
            {
                txtAdults.Text = kdata.ToString();
            }
        }

        private void txtChild_Click(object sender, EventArgs e)
        {
            if (_fixedPriceStatus || !txtChild.Enabled)
                return;

            decimal kdata = 0;
            using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.NoPercentageReasonNoDecimal)))
            {
                frm.ShowDialog();

                if (!string.IsNullOrEmpty(frm.txtkeypadvalue.Text))
                    kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);

                txtdiscountreason.Text = frm.txtdesc.Text.Trim() == "Enter Remarks Here....." ? "" : frm.txtdesc.Text;
            }
            if (kdata > 0)
            {
                txtChild.Text = kdata.ToString();
            }
        }

        private void txtextracharge_amt_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Party_Number)
            {
                decimal kdata = 0;
                using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.NoPercentageReasonNoDecimal)))
                {
                    frm.ShowDialog();

                    if (!string.IsNullOrEmpty(frm.txtkeypadvalue.Text))
                        kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);

                    txtdiscountreason.Text = frm.txtdesc.Text.Trim() == "Enter Remarks Here....." ? "" : frm.txtdesc.Text;
                }
                if (kdata > 0)
                    txtextracharge_amt.Text = kdata.ToString();
            }

        }

        private void txtdiscountreason_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Party_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtcakemessage_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Party_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtcakeremarks_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Party_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtRenote_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Party_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtItemName_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Party_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtDescription_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Party_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtcardserach_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Party_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtextracharge_des_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Party_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtserachgift_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_Party_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void dgv_packenter_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var row = dgv_packenter.Rows[e.RowIndex];

            if (e.ColumnIndex == selecteditemitemno)
            {
                //if (FrmDashboard.Enb_Party_Normal)
                //{
                //    ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
                //}
            }
            else if (e.ColumnIndex == selecteditemqtyno)
            {
                decimal kdata = 0;
                using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.NoPercentageReasonNoDecimal)))
                {
                    frm.ShowDialog();

                    if (!string.IsNullOrEmpty(frm.txtkeypadvalue.Text))
                        kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);

                    txtdiscountreason.Text = frm.txtdesc.Text.Trim() == "Enter Remarks Here....." ? "" : frm.txtdesc.Text;
                }
                if (kdata > 0)
                {
                    row.Cells[selecteditemqtyno].Value = kdata;
                }
            }

        }

      

        private void txtextracharge_amt_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {


                TextBox txt = sender as TextBox;


                if (char.IsControl(e.KeyChar))
                {
                    return;
                }
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                    return;
                }
                if (e.KeyChar == '.' && txt.Text.Contains("."))
                {
                    e.Handled = true;
                    return;
                }
                if (txt.Text.Contains("."))
                {
                    int index = txt.Text.IndexOf(".");
                    string decimals = txt.Text.Substring(index + 1);

                    if (txt.SelectionStart > index && decimals.Length >= 2)
                    {
                        e.Handled = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page txtextracharge_amt_KeyPress()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }

        }

        #endregion

        #region Payment Option 
        private void btn_poption_cls_Click(object sender, EventArgs e)
        {
            pnl_PaymentOptions.Visible = false;
            HideBlurPopup(pnl_PaymentOptions);
        }

        private void btn_fullpayment_Click(object sender, EventArgs e)
        {
            try
            {


                pnl_PaymentOptions.Visible = false;
                HideBlurPopup(pnl_PaymentOptions);

                decimal rcvAmt = 0;
                decimal.TryParse(txtRcvAmt.Text, out rcvAmt);
                decimal totalAdjustment = bookingItemAdjustments.Sum(a => a.Amount);
                decimal lblNet = decimal.TryParse(lblNetAmt.Text, out decimal na) ? na : 0;

                if (!string.IsNullOrEmpty(txtpartybookingid.Text) &&
                    int.TryParse(txtpartybookingid.Text, out int bookingId) &&
                    bookingId > 0 && dbNetAmount < lblNet)
                {

                    decimal advancePaid = decimal.TryParse(lbladvamtdata.Text, out decimal aa) ? aa : 0;

                    decimal newNetAmount = lblNet + totalAdjustment;
                    decimal totalPaid = advancePaid + rcvAmt;

                    if (totalPaid < newNetAmount)
                    {
                        ShowPaymentSettlementPanel(lblNet, advancePaid, rcvAmt, 0);
                        return;
                    }
                }

                Fullpaymentflag = true;
                pnl_PaymentOptions.Visible = false;
                HideBlurPopup(pnl_PaymentOptions);

                pnlPaymode.Visible = true;
                pnlPaymode.Visible = true;ShowBlurPopup(pnlPaymode);
                pnlPaymode.BringToFront();

                pnlPaymode.Left = (this.ClientSize.Width - pnlPaymode.Width) / 2;
                pnlPaymode.Top = (this.ClientSize.Height - pnlPaymode.Height) / 2;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btn_fullpayment_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

    

        private void btn_advance_Click(object sender, EventArgs e)
        {
            try
            {
                pnl_PaymentOptions.Visible = false;
                HideBlurPopup(pnl_PaymentOptions);
                decimal typedAmount = 0;
                decimal rcvAmt = 0;

                using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.NoPercentageReason)))
                {
                    frm.ShowDialog();

                    if (!string.IsNullOrEmpty(frm.txtkeypadvalue.Text))
                        typedAmount = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                }

                if (typedAmount <= 0)
                {
                    CMessageBox.Show("Please enter a valid received amount", "Validation",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRcvAmt.Focus();
                    return;
                }

                if (MinimumAdvance > 0 && rcvAmt < MinimumAdvance)
                {
                    CMessageBox.Show(
                        $"Minimum advance for package '{lblpackname.Text}' is {MinimumAdvance}",
                        "Validation",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtRcvAmt.Focus();
                    return;
                }

                Advanceflag = true;
                decimal netAmt = Convert.ToDecimal(lblNetAmt.Text);

                txtRcvAmt.Text = typedAmount.ToString("N2");

                decimal totalAdvance = dbAdvanceAmount + typedAmount;

                if (totalAdvance < netAmt)
                {
                    if (dbAdvanceAmount > 0)
                    {
                        AdvanceFlag = true;
                        ToggleFirstRow(1);
                    }
                    else
                        AdvanceFlag = false;
                    //lbladvamtdata.Text = totalAdvance.ToString("N2");
                    decimal balanceAmount = netAmt - totalAdvance;
                    lblBalAmt.Text = balanceAmount.ToString("N2");
                    UpdateBalancePayableCaption(balanceAmount);
                }
                else
                {
                    AdvanceFlag = false;
                    ToggleFirstRow(0);
                    //lbladvamtdata.Text = netAmt.ToString("N2");
                    lblBalAmt.Text = "0.00";
                    UpdateBalancePayableCaption(0m);
                }

                textb_shape();

                pnlPaymode.Visible = true;
                pnlPaymode.Visible = true;ShowBlurPopup(pnlPaymode);
                pnlPaymode.BringToFront();

                pnlPaymode.Left = (this.ClientSize.Width - pnlPaymode.Width) / 2;
                pnlPaymode.Top = (this.ClientSize.Height - pnlPaymode.Height) / 2;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btn_advance_Click()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        #endregion

        #region Keyboard

        public void ShowKeyboard(Control activeControl, int kbtype)
        {
            try
            {
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
                clsgen.LogException("Partybooking page ShowKeyboard()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        #endregion

        #region Menu Selection
        private void btn_menu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!checkPartySelet())
                    return;

                int width = Math.Min((int)(ClientSize.Width * 0.86), ClientSize.Width - 20);
                int height = Math.Min((int)(ClientSize.Height * 0.86), ClientSize.Height - 20);

                pnl_menu.SuspendLayout();
                pnl_menu.Width = width;
                pnl_menu.Height = height;
                pnl_menu.Left = (ClientSize.Width - width) / 2;
                pnl_menu.Top = (ClientSize.Height - height) / 2;
                pnl_menu.Visible = true;
                pnl_menu.Visible = true;ShowBlurPopup(pnl_menu);
                pnl_menu.BringToFront();

                tlp_menu_main.Visible = true;
                tlp_menu_main.BringToFront();
                flp_productview.Visible = true;
                Dgv_Selected_Item.Visible = false;
                flp_sidemenu.Visible = false;
                btn_cart.BackgroundImage = Properties.Resources.Party_Cart_Icon;
                btn_cart.BackgroundImageLayout = ImageLayout.Stretch;

                currentPage = 1;
                pnl_menu.ResumeLayout(true);
                pnl_menu.PerformLayout();
                pnl_dgv_menu.PerformLayout();
                tlp_menu_main.PerformLayout();
                flp_productview.PerformLayout();
                ApplyPartyMenuRoundedRegions();

                if (!partyMenuCategoriesLoaded)
                    GenerateSideMenu();

                // Product/food-item records are cached after the first database load,
                // so reopening the menu does not repeat the same queries.
                LoadPartyMenuProductsForCategory(0);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btn_menu_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void btn_menu_close_Click(object sender, EventArgs e)
        {
            pnl_menu.Visible = false;
            HideBlurPopup(pnl_menu);
            flp_sidemenu.Visible = false;
        }

        private void btn_showmenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!pnl_menu.Visible)
                    btn_menu_Click(sender, e);

                pnl_menu.Visible = true;
                pnl_menu.Visible = true;ShowBlurPopup(pnl_menu);
                pnl_menu.BringToFront();
                tlp_menu_main.Visible = true;
                tlp_menu_main.BringToFront();
                flp_productview.Visible = true;
                Dgv_Selected_Item.Visible = false;

                flp_sidemenu.Visible = !flp_sidemenu.Visible;
                if (!flp_sidemenu.Visible)
                    return;

                flp_sidemenu.FlowDirection = FlowDirection.TopDown;
                flp_sidemenu.WrapContents = false;
                flp_sidemenu.AutoScroll = true;
                flp_sidemenu.AutoScrollMinSize = Size.Empty;
                flp_sidemenu.HorizontalScroll.Enabled = false;
                flp_sidemenu.HorizontalScroll.Visible = false;
                flp_sidemenu.VerticalScroll.Enabled = true;

                // flp_sidemenu is a form-level control while btn_showmenu is inside pnl_menu.
                // Convert the button's screen position into form coordinates so the category
                // dropdown opens directly below Menu instead of at the left edge of the form.
                Point menuButtonBottom = btn_showmenu.PointToScreen(new Point(0, btn_showmenu.Height));
                Point dropdownLocation = this.PointToClient(menuButtonBottom);

                flp_sidemenu.Width = Math.Min(
                    Math.Max(btn_showmenu.Width, 180),
                    Math.Max(180, ClientSize.Width - 20));
                flp_sidemenu.Height = Math.Max(
                    120,
                    Math.Min(ClientSize.Height - dropdownLocation.Y - 20, 420));
                flp_sidemenu.Left = Math.Max(
                    0,
                    Math.Min(dropdownLocation.X, ClientSize.Width - flp_sidemenu.Width));
                flp_sidemenu.Top = Math.Max(0, dropdownLocation.Y + 4);

                GenerateSideMenu();

                // Refit after the popup dimensions and scrollbar visibility are finalized.
                flp_sidemenu.PerformLayout();
                FitPartyMenuCategoryButtonWidths();
                flp_sidemenu.PerformLayout();
                flp_sidemenu.BringToFront();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btn_showmenu_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void GenerateSideMenu()
        {
            try
            {
                if (partyMenuCategoriesLoaded)
                    return;

                BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                DmnSearch dmnSearch = new DmnSearch
                {
                    Select = "DISTINCT c.CategoryName, c.Fcid",
                    Table = "tblCategory_FTI c",
                    WhereNdGroupBy = "WHERE c.Status = 1 AND c.CategoryType = " + Convert.ToInt32(CategoryType.Menu_Category) +
                        " AND (EXISTS (SELECT 1 FROM tblProduct p WHERE p.Fcid = c.Fcid AND p.Status = 1)" +
                        " OR EXISTS (SELECT 1 FROM tblItem i WHERE i.Fcid = c.Fcid AND i.Status = 1 AND i.Saletype = 1))",
                    OrderBy = "ORDER BY c.CategoryName"
                };

                var categories = bllSearch.GetSearchData("prdSearch", dmnSearch);
                flp_sidemenu.SuspendLayout();
                foreach (Control control in flp_sidemenu.Controls)
                    control.Dispose();
                flp_sidemenu.Controls.Clear();

                if (sideMenuToolTip == null)
                    sideMenuToolTip = new ToolTip();

                foreach (var category in categories)
                {
                    Button button = CreateSideMenuButton(category.CategoryName, "btn_menu_category_" + category.Fcid);
                    button.Tag = category.Fcid;
                    button.Click += SidebuttonClickEventHandler;
                    flp_sidemenu.Controls.Add(button);
                }

                flp_sidemenu.ResumeLayout(true);
                FitPartyMenuCategoryButtonWidths();
                partyMenuCategoriesLoaded = true;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page GenerateSideMenu()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        private Button CreateSideMenuButton(string text, string name)
        {
            Button button = new Button
            {
                Text = text,
                Name = name,
                Tag = 0,
                Width = Math.Max(100, flp_sidemenu.ClientSize.Width - 24),
                Height = Math.Max(btn_showmenu.Height, 38),
                Margin = new Padding(5),
                AutoEllipsis = true,
                ForeColor = Color.White,
                BackgroundImage = Properties.Resources.fb_sidemenu,
                BackgroundImageLayout = ImageLayout.Stretch,
                FlatStyle = FlatStyle.Flat,
                Font = AppFonts.GetFont(10),
                Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderSize = 0;
            if (sideMenuToolTip != null)
                sideMenuToolTip.SetToolTip(button, text);
            return button;
        }

        private void FitPartyMenuCategoryButtonWidths()
        {
            if (flp_sidemenu == null || flp_sidemenu.IsDisposed)
                return;

            flp_sidemenu.SuspendLayout();
            flp_sidemenu.FlowDirection = FlowDirection.TopDown;
            flp_sidemenu.WrapContents = false;
            flp_sidemenu.AutoScroll = true;
            flp_sidemenu.AutoScrollMinSize = Size.Empty;
            flp_sidemenu.Padding = Padding.Empty;
            flp_sidemenu.HorizontalScroll.Enabled = false;
            flp_sidemenu.HorizontalScroll.Visible = false;
            flp_sidemenu.VerticalScroll.Enabled = true;

            // ClientSize already excludes a visible vertical scrollbar. Reserve a
            // small gutter plus each button's margins so the horizontal bar is not needed.
            int usableWidth = flp_sidemenu.ClientSize.Width
                - flp_sidemenu.Padding.Horizontal
                - 14;
            int buttonWidth = Math.Max(100, usableWidth);

            foreach (Control control in flp_sidemenu.Controls)
            {
                Button categoryButton = control as Button;
                if (categoryButton == null)
                    continue;

                int buttonHeight = Math.Max(btn_showmenu.Height, 38);
                categoryButton.AutoSize = false;
                categoryButton.MinimumSize = Size.Empty;
                categoryButton.MaximumSize = Size.Empty;
                categoryButton.Width = buttonWidth;
                categoryButton.Height = buttonHeight;
                categoryButton.Margin = new Padding(5);
                categoryButton.AutoEllipsis = true;
                categoryButton.BackgroundImageLayout = ImageLayout.Stretch;
            }

            flp_sidemenu.ResumeLayout(true);
            flp_sidemenu.PerformLayout();
            flp_sidemenu.HorizontalScroll.Enabled = false;
            flp_sidemenu.HorizontalScroll.Visible = false;
        }

        private void SidebuttonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clicked = sender as Control;
                if (clicked == null || !int.TryParse(Convert.ToString(clicked.Tag), out int categoryId))
                    return;
                McategoryID = Convert.ToInt32(MenucategryType.NormalProduct);
                currentPage = 1;
                flp_sidemenu.Visible = false;
                LoadPartyMenuProductsForCategory(categoryId);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page SidebuttonClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void EnsurePartyMenuProductsLoaded()
        {
            if (partyMenuProductsLoaded && partyMenuAllProducts != null)
                return;

            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            DmnSearch search = new DmnSearch();
            List<DmnGridFields> loadedProducts = new List<DmnGridFields>();

            search.Select = "p.Product AS Field1, p.ProductId AS Field2, 0 AS Field3, 0 AS Field4, p.Price AS Field5, p.MRP AS Field6, p.Tax AS Field7, p.TaxType AS Field8, p.Fcid AS Field9, p.TaxSettingsId AS Field10, p.ItemCode AS Field11, 0 AS Field12, ISNULL(p.foodtype,0) AS Field13, p.ForeColor AS Field14, p.BackColor AS Field15";
            search.Table = "tblProduct p";
            search.WhereNdGroupBy = "WHERE p.Status = 1";
            search.OrderBy = "ORDER BY p.Product";
            loadedProducts.AddRange(bllSearch.GetSearchData("prdSearch", search));

            search.Select = "i.Itemname AS Field1, i.ItemId AS Field2, i.Saletype AS Field3, 0 AS Field4, ISNULL((SELECT TOP 1 pr.SRate FROM tblProductRate pr WHERE pr.ProductId=i.ItemId AND pr.Status=1 ORDER BY pr.Id DESC),0) AS Field5, ISNULL((SELECT TOP 1 pr.MRP FROM tblProductRate pr WHERE pr.ProductId=i.ItemId AND pr.Status=1 ORDER BY pr.Id DESC),0) AS Field6, i.Tax AS Field7, i.TaxType AS Field8, i.Fcid AS Field9, i.TaxSettingsId AS Field10, i.ItemCode_UPC AS Field11, ISNULL(i.Unit,0) AS Field12, 0 AS Field13, '' AS Field14, '' AS Field15";
            search.Table = "tblItem i";
            search.WhereNdGroupBy = "WHERE i.Status = 1 AND i.Saletype = 1";
            search.OrderBy = "ORDER BY i.Itemname";
            loadedProducts.AddRange(bllSearch.GetSearchData("prdSearch", search));

            partyMenuAllProducts = loadedProducts;
            partyMenuProductsLoaded = true;
        }

        private void LoadPartyMenuProductsForCategory(int categoryId, string productName = "", string itemCode = "")
        {
            try
            {
                EnsurePartyMenuProductsLoaded();

                IEnumerable<DmnGridFields> filteredProducts = partyMenuAllProducts ?? new List<DmnGridFields>();

                if (categoryId > 0)
                {
                    filteredProducts = filteredProducts.Where(item =>
                    {
                        int productCategoryId;
                        return int.TryParse(Convert.ToString(item.Field9), out productCategoryId)
                            && productCategoryId == categoryId;
                    });
                }

                if (!string.IsNullOrWhiteSpace(productName))
                {
                    string nameFilter = productName.Trim();
                    filteredProducts = filteredProducts.Where(item =>
                        (Convert.ToString(item.Field1) ?? string.Empty).IndexOf(nameFilter, StringComparison.CurrentCultureIgnoreCase) >= 0);
                }

                if (!string.IsNullOrWhiteSpace(itemCode))
                {
                    string codeFilter = itemCode.Trim();
                    filteredProducts = filteredProducts.Where(item =>
                        string.Equals(Convert.ToString(item.Field11), codeFilter, StringComparison.OrdinalIgnoreCase));
                }

                ProductData = filteredProducts
                    .OrderBy(item => Convert.ToString(item.Field1), StringComparer.CurrentCultureIgnoreCase)
                    .ToList();

                pagedatadata = new List<DmnGridFields>();
                pnl_menu.PerformLayout();
                pnl_dgv_menu.PerformLayout();
                tlp_menu_main.PerformLayout();
                flp_productview.PerformLayout();

                CalculatePartyMenuLayout();
                ShowPartyMenuPage();

                tlp_menu_main.Visible = true;
                tlp_menu_main.BringToFront();
                flp_productview.Visible = true;
                Dgv_Selected_Item.Visible = false;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page LoadPartyMenuProductsForCategory()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void CalculatePartyMenuLayout()
        {
            flp_productview.WrapContents = true;
            flp_productview.FlowDirection = FlowDirection.LeftToRight;
            flp_productview.AutoScroll = true;

            partyMenuGap = Math.Max(4, Math.Min(10, Productspace));
            int availableWidth = Math.Max(180, flp_productview.ClientSize.Width - flp_productview.Padding.Horizontal);
            int availableHeight = Math.Max(120, flp_productview.ClientSize.Height - flp_productview.Padding.Vertical);

            const int minimumCardWidth = 150;
            partyMenuColumns = Math.Max(1, availableWidth / (minimumCardWidth + (2 * partyMenuGap)));
            partyMenuTileWidth = Math.Max(110, (availableWidth / partyMenuColumns) - (2 * partyMenuGap));
            partyMenuTileHeight = Math.Max(104, Math.Min(150, (int)(partyMenuTileWidth * 0.82m)));

            partyMenuRows = Math.Max(1, availableHeight / (partyMenuTileHeight + (2 * partyMenuGap)));
            pageSize = Math.Max(1, partyMenuColumns * partyMenuRows);
        }

        private void GeneratePartyMenuProducts()
        {
            try
            {
                flp_productview.SuspendLayout();
                foreach (Control control in flp_productview.Controls)
                    control.Dispose();
                flp_productview.Controls.Clear();

                flp_productview.WrapContents = true;
                flp_productview.FlowDirection = FlowDirection.LeftToRight;
                flp_productview.AutoScroll = true;
                flp_productview.Padding = new Padding(6);

                foreach (var item in pagedatadata)
                {
                    int productId = Convert.ToInt32(item.Field2);
                    int saleType = 0;
                    int.TryParse(Convert.ToString(item.Field3), out saleType);
                    int foodType = 0;
                    int.TryParse(Convert.ToString(item.Field13), out foodType);
                    string productName = Convert.ToString(item.Field1) ?? string.Empty;

                    Color tileColor = Color.White;
                    string backColorValue = Convert.ToString(item.Field15);
                    if (!string.IsNullOrWhiteSpace(backColorValue))
                    {
                        try { tileColor = ColorTranslator.FromHtml(backColorValue); }
                        catch { tileColor = Color.White; }
                    }

                    Panel tile = new Panel
                    {
                        Width = partyMenuTileWidth,
                        Height = partyMenuTileHeight,
                        BackColor = tileColor,
                        Margin = new Padding(partyMenuGap),
                        Tag = productId,
                        AccessibleDescription = Convert.ToString(saleType),
                        Cursor = Cursors.Hand,
                        BorderStyle = BorderStyle.None
                    };

                    TableLayoutPanel tileLayout = new TableLayoutPanel
                    {
                        Dock = DockStyle.Fill,
                        RowCount = 1,
                        ColumnCount = 1,
                        BackColor = Color.Transparent,
                        Margin = new Padding(0),
                        Padding = new Padding(0),
                        Tag = productId,
                        AccessibleDescription = Convert.ToString(saleType)
                    };
                    tileLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                    tileLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

                    Color nameColor = Color.Black;
                    string foreColorValue = Convert.ToString(item.Field14);
                    if (!string.IsNullOrWhiteSpace(foreColorValue))
                    {
                        try { nameColor = ColorTranslator.FromHtml(foreColorValue); }
                        catch { nameColor = Color.Black; }
                    }

                    Label name = new Label
                    {
                        Text = productName,
                        Dock = DockStyle.Fill,
                        Tag = productId,
                        AccessibleDescription = Convert.ToString(saleType),
                        TextAlign = ContentAlignment.MiddleCenter,
                        Padding = new Padding(5),
                        ForeColor = nameColor,
                        BackColor = Color.Transparent,
                        Font = AppFonts.GetFont(11),
                        AutoEllipsis = true
                    };
                    Size textSize = TextRenderer.MeasureText(productName, AppFonts.GetFont(11));
                    if (textSize.Width > partyMenuTileWidth - 18)
                        name.Font = AppFonts.GetFont(9);

                    tileLayout.Controls.Add(name, 0, 0);
                    tile.Controls.Add(tileLayout);

                    // Small food-type indicator remains; favorites and card border are removed.
                    Panel foodIndicator = new Panel
                    {
                        Width = 12,
                        Height = 12,
                        Location = new Point(5, 5),
                        BackColor = Color.Transparent
                    };
                    Color indicatorColor = foodType == 1
                        ? Color.Green
                        : foodType == 2
                            ? Color.Red
                            : Color.Gray;
                    foodIndicator.Paint += (sender, paintArgs) =>
                    {
                        paintArgs.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        using (SolidBrush brush = new SolidBrush(indicatorColor))
                            paintArgs.Graphics.FillEllipse(brush, 0, 0, foodIndicator.Width - 1, foodIndicator.Height - 1);
                    };
                    tile.Controls.Add(foodIndicator);
                    foodIndicator.BringToFront();

                    tile.Click += (sender, args) => MenuProductTile_Click(tile, args);
                    tileLayout.Click += (sender, args) => MenuProductTile_Click(tile, args);
                    name.Click += (sender, args) => MenuProductTile_Click(tile, args);

                    UIHelper.RoundCorners(tile, Productcorner, null);
                    flp_productview.Controls.Add(tile);
                }

                flp_productview.ResumeLayout(true);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page GeneratePartyMenuProducts()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void MenuProductTile_Click(object sender, EventArgs e)
        {
            try
            {
                Control clicked = sender as Control;
                if (clicked == null || !int.TryParse(Convert.ToString(clicked.Tag), out int productId))
                    return;
                int saleType = 0;
                int.TryParse(Convert.ToString(clicked.AccessibleDescription), out saleType);
                AddPartyMenuProduct(productId, saleType);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page MenuProductTile_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        private sealed class PartyMenuSelection
        {
            public int ProductId { get; set; }
            public int SaleType { get; set; } // 0 = tblProduct, 1 = tblItem
            public int CategoryId { get; set; }
            public int UnitId { get; set; }
            public int TaxSettingId { get; set; }
            public string ProductName { get; set; }
            public string ItemCode { get; set; }
            public string TaxMode { get; set; }
            public string TaxType { get; set; }
            public decimal Price { get; set; }
            public decimal MRP { get; set; }
            public string HSNCode { get; set; }
        }

        private void AddPartyMenuProduct(int productId, int saleType)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch search = new DmnSearch();
                string taxMode = "Exclusive", taxType = "GST", itemCode = "", productName = "";
                decimal price = 0m, mrp = 0m;
                int categoryId = 0, taxSettingId = 0, unitId = 0;

                if (saleType == 0)
                {
                    search.Select = "p.Product AS Field1, p.Price AS Field2, p.MRP AS Field3, p.Tax AS Field4, p.TaxType AS Field5, p.Fcid AS Field6, p.TaxSettingsId AS Field7, p.ItemCode AS Field8, 0 AS Field9, ISNULL(p.HSNCode,'') AS Field10";
                    search.Table = "tblProduct p LEFT JOIN tblMenu m ON m.ProductId=p.ProductId AND m.ProductType=0 AND m.Status=1";
                    search.WhereNdGroupBy = "WHERE p.Status=1 AND p.ProductId=" + productId;
                }
                else
                {
                    search.Select = "i.Itemname AS Field1, ISNULL((SELECT TOP 1 pr.SRate FROM tblProductRate pr WHERE pr.ProductId=i.ItemId AND pr.Status=1 ORDER BY pr.Id DESC),0) AS Field2," +
                        " ISNULL((SELECT TOP 1 pr.MRP FROM tblProductRate pr WHERE pr.ProductId=i.ItemId AND pr.Status=1 ORDER BY pr.Id DESC),0) AS Field3, i.Tax AS Field4, i.TaxType AS Field5," +
                        " i.Fcid AS Field6, i.TaxSettingsId AS Field7, i.ItemCode_UPC AS Field8, ISNULL(i.Unit,0) AS Field9, ISNULL(i.HSNCode,'') AS Field10";
                    search.Table = "tblItem i";
                    search.WhereNdGroupBy = "WHERE i.Status=1 AND i.Saletype=1 AND i.ItemId=" + productId;
                }

                search.OrderBy = "";
                var result = bllSearch.GetSearchData("prdSearch", search);
                if (result.Count == 0)
                    return;

                productName = Convert.ToString(result[0].Field1);
                decimal.TryParse(Convert.ToString(result[0].Field2), out price);
                decimal.TryParse(Convert.ToString(result[0].Field3), out mrp);
                taxMode = string.IsNullOrWhiteSpace(Convert.ToString(result[0].Field4)) ? "Exclusive" : Convert.ToString(result[0].Field4);
                taxType = string.IsNullOrWhiteSpace(Convert.ToString(result[0].Field5)) ? "GST" : Convert.ToString(result[0].Field5);
                int.TryParse(Convert.ToString(result[0].Field6), out categoryId);
                int.TryParse(Convert.ToString(result[0].Field7), out taxSettingId);
                itemCode = Convert.ToString(result[0].Field8);
                int.TryParse(Convert.ToString(result[0].Field9), out unitId);
                string hsnCode = Convert.ToString(result[0].Field10);

                if (price < 0m)
                    price = 0m;

                PartyMenuSelection selection = new PartyMenuSelection
                {
                    ProductId = productId,
                    SaleType = saleType,
                    CategoryId = categoryId,
                    UnitId = unitId,
                    TaxSettingId = taxSettingId,
                    ProductName = productName,
                    ItemCode = itemCode,
                    TaxMode = taxMode,
                    TaxType = taxType,
                    Price = price,
                    MRP = mrp,
                    HSNCode = hsnCode
                };

                DataGridViewRow existing = Dgv_Selected_Item.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(row =>
                        !row.IsNewRow &&
                        row.Tag is PartyMenuSelection &&
                        ((PartyMenuSelection)row.Tag).ProductId == productId &&
                        ((PartyMenuSelection)row.Tag).SaleType == saleType);

                if (existing != null)
                {
                    decimal quantity = 1m;
                    decimal.TryParse(Convert.ToString(existing.Cells[iqty].Value), out quantity);
                    existing.Cells[iqty].Value = quantity + 1m;
                    existing.Tag = selection;
                    RefreshPartySelectedItemRow(existing);
                }
                else
                {
                    int rowIndex = Dgv_Selected_Item.Rows.Add();
                    DataGridViewRow row = Dgv_Selected_Item.Rows[rowIndex];
                    row.Tag = selection;

                    row.Cells[iaction].Value = Grid_Remove;
                    row.Cells[islno].Value = rowIndex + 1;
                    row.Cells[ibarcode].Value = itemCode;
                    row.Cells[iproducttypeno].Value = "Food";
                    row.Cells[iname].Value = productName;
                    row.Cells[iunit].Value = unitId > 0 ? unitId.ToString() : "";
                    row.Cells[israte].Value = price;
                    row.Cells[iqty].Value = 1;
                    row.Cells[iprate].Value = price;
                    row.Cells[iamount].Value = price;
                    row.Cells[idisper].Value = 0m;
                    row.Cells[idisamt].Value = 0m;
                    row.Cells[itaxper].Value = 0m;
                    row.Cells[itotal].Value = price;
                    row.Cells[iitemid].Value = productId;
                    row.Cells[itaxtype].Value = taxType;
                    row.Cells[imrp].Value = mrp;
                    row.Cells[itype].Value = saleType;
                    row.Cells[ibarcode2].Value = itemCode;
                    row.Cells[itaxsettingid].Value = taxSettingId;
                    row.Cells[iunitid].Value = unitId;
                    row.Cells[ipretaxamtno].Value = price;
                    row.Cells[iigstamtno].Value = 0m;
                    row.Cells[ivatamtno].Value = 0m;
                    row.Cells[ioriginalprice].Value = price;
                    row.Cells[istatusno].Value = 1;
                    row.Cells[iMainProductStatusno].Value = Convert.ToInt32(MainProductStatus.Primary);
                    row.Cells[isubofferstatus].Value = Convert.ToInt32(SubofferStatus.None);
                    row.Cells[itabletype].Value = Convert.ToInt32(Tabletype.Product);
                    row.Cells[iorgname].Value = productName;
                    row.Cells[istocktrackno].Value = false;

                    DataGridViewImageCell actionCell = row.Cells[iaction] as DataGridViewImageCell;
                    if (actionCell != null)
                        actionCell.ImageLayout = DataGridViewImageCellLayout.Zoom;

                    RefreshPartySelectedItemRow(row);
                }

                // Food stays in this temporary selection grid until Submit is clicked.
                ShowPartySelectedItems();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page AddPartyMenuProduct()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void RefreshPartySelectedItemRow(DataGridViewRow row)
        {
            if (row == null)
                return;

            decimal quantity = 1m, price = 0m, discountPer = 0m;
            decimal.TryParse(Convert.ToString(row.Cells[iqty].Value), out quantity);
            decimal.TryParse(Convert.ToString(row.Cells[israte].Value), out price);
            decimal.TryParse(Convert.ToString(row.Cells[idisper].Value), out discountPer);

            decimal amount = Math.Round(quantity * price, 2, MidpointRounding.AwayFromZero);
            decimal discountAmount = Math.Round(amount * discountPer / 100m, 2, MidpointRounding.AwayFromZero);
            decimal amountAfterDiscount = amount - discountAmount;

            row.Cells[iamount].Value = amount.ToString("N2");
            row.Cells[idisamt].Value = discountAmount.ToString("N2");
            row.Cells[itaxper].Value = "0.00";
            row.Cells[itotal].Value = amountAfterDiscount.ToString("N2");
            row.Cells[ipretaxamtno].Value = amountAfterDiscount.ToString("N2");
            row.Cells[ioriginalprice].Value = amountAfterDiscount.ToString("N2");
            row.Cells[iSaledisperno].Value = discountPer.ToString("N2");
            row.Cells[isalediamtno].Value = discountAmount.ToString("N2");
            GridSlno_Refresh();
        }

        private void GridSlno_Refresh()
        {
            int slno = 1;
            foreach (DataGridViewRow row in Dgv_Selected_Item.Rows)
            {
                if (row.IsNewRow || !(row.Tag is PartyMenuSelection))
                    continue;

                row.Cells[islno].Value = slno++;
            }
        }

        private void AddOrUpdatePartyFoodGrid(PartyMenuSelection item, decimal quantity)
        {
            if (item == null || quantity <= 0m)
                return;

            DataGridViewRow existing = dgv_product.Rows
                .Cast<DataGridViewRow>()
                .FirstOrDefault(candidate =>
                    !candidate.IsNewRow &&
                    Convert.ToInt32(candidate.Cells[pProductid].Value ?? 0) == item.ProductId &&
                    Convert.ToInt32(candidate.Cells[PPackProductTypeno].Value ?? 0) == (int)PackItemType.Menu);

            if (existing != null)
            {
                decimal existingQuantity = 0m;
                decimal.TryParse(Convert.ToString(existing.Cells[pqtyno].Value), out existingQuantity);
                existing.Cells[pqtyno].Value = existingQuantity + quantity;
                if (!string.IsNullOrWhiteSpace(item.HSNCode))
                    existing.Cells[phsncodeno].Value = item.HSNCode;
                CalculateAndApplyTax(existing.Index, (int)PackItemType.Menu, item.ProductId);
                return;
            }

            int index = dgv_product.Rows.Add();
            DataGridViewRow row = dgv_product.Rows[index];
            decimal gross = Math.Round(item.Price * quantity, 2, MidpointRounding.AwayFromZero);

            row.Cells[pslno].Value = dgv_product.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow && r.Index <= index);
            row.Cells[pitemno].Value = item.ProductName;
            row.Cells[pcategmenuno].Value = GetCategoryName(item.CategoryId);
            row.Cells[pqtyno].Value = quantity;
            row.Cells[pPrice].Value = item.Price;
            row.Cells[pAmount].Value = gross;
            row.Cells[pProductid].Value = item.ProductId;
            row.Cells[phsncodeno].Value = item.HSNCode ?? string.Empty;
            row.Cells[pproducttype].Value = (int)FoodandBeverage.Interfaces.ProductType.FB;
            row.Cells[proPartyPackDetailsId].Value = 0;
            row.Cells[pPartydeafult].Value = 2;
            row.Cells[ptaxamt].Value = 0m;
            row.Cells[ptax].Value = item.TaxMode;
            row.Cells[ptaxtype].Value = item.TaxType;
            row.Cells[ptaxper].Value = 0m;
            row.Cells[ptaxsettingid].Value = item.TaxSettingId;
            row.Cells[ppretaxamt].Value = gross;
            row.Cells[pnetamt].Value = gross;
            row.Cells[pigstamt].Value = 0m;
            row.Cells[pvatamt].Value = 0m;
            row.Cells[pdiscper].Value = 0m;
            row.Cells[pdisamt].Value = 0m;
            row.Cells[pmessageno].Value = "";
            row.Cells[premarksno].Value = "";
            row.Cells[PPackProductTypeno].Value = (int)PackItemType.Menu;
            row.ReadOnly = true;

            DataGridViewImageCell actionCell = row.Cells[pactionno] as DataGridViewImageCell;
            if (actionCell != null)
            {
                actionCell.Value = Grid_Remove;
                actionCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }

            CalculateAndApplyTax(index, (int)PackItemType.Menu, item.ProductId);
        }

        private void btn_menu_submit_Click(object sender, EventArgs e)
        {
            try
            {
                List<DataGridViewRow> selectedRows = Dgv_Selected_Item.Rows
                    .Cast<DataGridViewRow>()
                    .Where(row => !row.IsNewRow && row.Tag is PartyMenuSelection)
                    .ToList();

                if (selectedRows.Count == 0)
                {
                    CMessageBox.Show("Please select at least one food item.", "Menu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                ResetDiscount();

                foreach (DataGridViewRow row in selectedRows)
                {
                    PartyMenuSelection item = row.Tag as PartyMenuSelection;
                    if (item == null)
                        continue;

                    decimal quantity = 1m;
                    decimal.TryParse(Convert.ToString(row.Cells[iqty].Value), out quantity);
                    AddOrUpdatePartyFoodGrid(item, quantity);
                }

                Dgv_Selected_Item.Rows.Clear();
                GridSlno_Refresh();
                pnl_menu.Visible = false;
                HideBlurPopup(pnl_menu);
                flp_sidemenu.Visible = false;

                GetGrandTotal_All();
                RecalculateAllRowsAndTotals();
                if (Convert.ToDecimal(discountper.Text) > 0)
                    CalculateDiscountAmount();
                else
                    CalculateDiscountPer();
                CalculateNetAmount();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btn_menu_submit_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void ShowPartyProductTiles()
        {
            btn_cart.BackgroundImage = Properties.Resources.Party_Cart_Icon;
            btn_cart.BackgroundImageLayout = ImageLayout.Stretch;
            tlp_menu_main.Visible = true;
            tlp_menu_main.BringToFront();
            flp_productview.Visible = true;
            flp_sidemenu.Visible = false;
            Dgv_Selected_Item.Visible = false;
            flp_productview.BringToFront();
        }

        private void ShowPartySelectedItems()
        {
            btn_cart.BackgroundImage = Properties.Resources.Party_Food_Items_Icon;
            btn_cart.BackgroundImageLayout = ImageLayout.Stretch;
            tlp_menu_main.Visible = true;
            tlp_menu_main.SendToBack();
            flp_productview.Visible = false;
            flp_sidemenu.Visible = false;
            Dgv_Selected_Item.Visible = true;
            Dgv_Selected_Item.BringToFront();
        }

        private void Dgv_Selected_Item_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex != iaction)
                    return;

                DataGridViewRow row = Dgv_Selected_Item.Rows[e.RowIndex];
                if (!(row.Tag is PartyMenuSelection))
                    return;

                Dgv_Selected_Item.Rows.RemoveAt(e.RowIndex);
                GridSlno_Refresh();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page Dgv_Selected_Item_CellClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void Dgv_Selected_Item_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                e.CellStyle.ForeColor = Color.Black;
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

        private void ShowPartyMenuPage()
        {
            CalculatePartyMenuLayout();
            totalPages = (int)Math.Ceiling((double)(ProductData?.Count ?? 0) / pageSize);
            if (currentPage < 1)
                currentPage = 1;
            if (totalPages > 0 && currentPage > totalPages)
                currentPage = totalPages;

            pagedatadata = (ProductData ?? new List<DmnGridFields>())
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            lblpagerinfo.Text = (ProductData == null || ProductData.Count == 0)
                ? "No products found"
                : $"Page {currentPage} of {Math.Max(1, totalPages)} | Total Records: {ProductData.Count}";
            btnprev.Enabled = currentPage > 1;
            btnnext.Enabled = currentPage < totalPages;
            GeneratePartyMenuProducts();
        }

        private void btnprev_Click(object sender, EventArgs e)
        {
            if (currentPage <= 1)
                return;
            currentPage--;
            ShowPartyMenuPage();
        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            if (currentPage >= totalPages)
                return;
            currentPage++;
            ShowPartyMenuPage();
        }

        private void ApplyPartyMenuVisualStyles()
        {
            Color accentPurple = Color.FromArgb(101, 80, 185);
            Color menuLavender = Color.FromArgb(236, 220, 244);
            Color softLavender = Color.FromArgb(236, 220, 244);
            Color borderLavender = Color.FromArgb(226, 214, 239);
            Color textColor = Color.FromArgb(42, 34, 55);

            flp_productview.BackColor = softLavender;
            flp_productview.Padding = new Padding(6);
            flp_productview.Margin = new Padding(0);

            // Requested menu/search strip background.
            tableLayoutPanel45.BackColor = menuLavender;
            tlp_serachbox.BackColor = menuLavender;

            // Flat white, borderless search fields with rounded corners.
            txtname_search.BackColor = Color.White;
            txtitemcode_serach.BackColor = Color.White;
            txtname_search.BorderStyle = BorderStyle.None;
            txtitemcode_serach.BorderStyle = BorderStyle.None;
            txtname_search.Font = AppFonts.GetFont(10);
            txtitemcode_serach.Font = AppFonts.GetFont(10);
            txtname_search.ForeColor = Color.FromArgb(139, 124, 153);
            txtitemcode_serach.ForeColor = Color.FromArgb(139, 124, 153);

            btn_serach.FlatStyle = FlatStyle.Flat;
            btn_serach.FlatAppearance.BorderSize = 0;
            btn_serach.FlatAppearance.MouseOverBackColor = menuLavender;
            btn_serach.FlatAppearance.MouseDownBackColor = Color.FromArgb(216, 197, 235);

            tlp_footer.BackColor = menuLavender;
            lblpagerinfo.ForeColor = textColor;

            Dgv_Selected_Item.SuspendLayout();
            Dgv_Selected_Item.BorderStyle = BorderStyle.None;
            Dgv_Selected_Item.BackgroundColor = Color.White;
            Dgv_Selected_Item.GridColor = borderLavender;
            Dgv_Selected_Item.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            Dgv_Selected_Item.EnableHeadersVisualStyles = false;
            Dgv_Selected_Item.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            Dgv_Selected_Item.ColumnHeadersHeight = 42;
            Dgv_Selected_Item.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            Dgv_Selected_Item.ColumnHeadersDefaultCellStyle.BackColor = accentPurple;
            Dgv_Selected_Item.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            Dgv_Selected_Item.ColumnHeadersDefaultCellStyle.Font = new Font(AppFonts.GetFont(10), FontStyle.Bold);
            Dgv_Selected_Item.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            Dgv_Selected_Item.ColumnHeadersDefaultCellStyle.SelectionBackColor = accentPurple;
            Dgv_Selected_Item.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.White;
            Dgv_Selected_Item.DefaultCellStyle.BackColor = Color.White;
            Dgv_Selected_Item.DefaultCellStyle.ForeColor = textColor;
            Dgv_Selected_Item.DefaultCellStyle.SelectionBackColor = Color.FromArgb(231, 218, 244);
            Dgv_Selected_Item.DefaultCellStyle.SelectionForeColor = textColor;
            Dgv_Selected_Item.DefaultCellStyle.Font = AppFonts.GetFont(10);
            Dgv_Selected_Item.DefaultCellStyle.Padding = new Padding(4, 2, 4, 2);
            Dgv_Selected_Item.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 246, 253);
            Dgv_Selected_Item.AlternatingRowsDefaultCellStyle.ForeColor = textColor;
            Dgv_Selected_Item.RowTemplate.Height = 34;
            Dgv_Selected_Item.RowHeadersVisible = false;
            Dgv_Selected_Item.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Dgv_Selected_Item.MultiSelect = false;
            Dgv_Selected_Item.ResumeLayout(true);

            ApplyPartyMenuRoundedRegions();
        }

        private void ApplyPartyMenuRoundedRegions()
        {
            // Reapply after resizing the popup so the rounded regions match current sizes.
            UIHelper.RoundCorners(txtname_search, 10);
            UIHelper.RoundCorners(txtitemcode_serach, 10);
            UIHelper.RoundCorners(pnl_dgv_menu, 25);
            UIHelper.RoundCorners(tlp_footer, 25);
        }

        private void SetPartyMenuSearchPlaceholders()
        {
            if (string.IsNullOrWhiteSpace(txtname_search.Text))
            {
                txtname_search.Text = "Food Name .....";
                txtname_search.ForeColor = Color.DarkGray;
            }

            if (string.IsNullOrWhiteSpace(txtitemcode_serach.Text))
            {
                txtitemcode_serach.Text = "Item Code .....";
                txtitemcode_serach.ForeColor = Color.DarkGray;
            }
        }

       

        private void PartyMenuSearchTextBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null)
                return;

            string placeholder = textBox == txtname_search ? "Food Name ....." : "Item Code .....";
            if (textBox.Text == placeholder)
                textBox.Clear();

            textBox.BackColor = Color.White;
            textBox.ForeColor = Color.FromArgb(56, 41, 73);
        }

       

        private void PartyMenuSearchTextBox_Leave(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox == null || !string.IsNullOrWhiteSpace(textBox.Text))
                return;

            textBox.Text = textBox == txtname_search ? "Food Name ....." : "Item Code .....";
            textBox.BackColor = Color.White;
            textBox.ForeColor = Color.FromArgb(139, 124, 153);
        }

        

        private void btn_serach_Click(object sender, EventArgs e)
        {
            string name = txtname_search.Text;
            string code = txtitemcode_serach.Text;
            if (name == "Food Name ....." || name == "Food Name .....")
                name = "";
            if (code == "Item Code ....." || code == "Item Code .....")
                code = "";
            if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrWhiteSpace(code))
            {
                currentPage = 1;
                LoadPartyMenuProductsForCategory(0, name, code);
            }
        }

      
        private void txtname_search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_serach_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void txtitemcode_serach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_serach_Click(sender, e);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }       

        private void btn_cart_Click(object sender, EventArgs e)
        {
            try
            {
                if (!pnl_menu.Visible)
                    btn_menu_Click(sender, e);
                bool cartIsVisible = Dgv_Selected_Item.Visible && !flp_productview.Visible;
                if (cartIsVisible)
                    ShowPartyProductTiles();
                else
                    ShowPartySelectedItems();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Partybooking page btn_cart_Click()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Partybooking));
            }
        }

       

        private void btndateslect_Click(object sender, EventArgs e)
        {
            using (FrmPartyBookingCalendar frm = new FrmPartyBookingCalendar())
            {
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.ShowInTaskbar = false;
                if (frm.ShowDialog(this) == DialogResult.OK && frm.SelectedBookingId > 0)
                    LoadBookingFromCalendar(frm.SelectedBookingId);
                else
                    UpdateSelectedDateLabel();
            }
        }

      

        #endregion

        private void ConfigurePartyBookingHsnColumn()
        {
            try
            {
                if (dgv_product == null ||
                    !dgv_product.Columns.Contains("pahsncode"))
                    return;

                DataGridViewColumn hsnColumn = dgv_product.Columns["pahsncode"];
                hsnColumn.Visible = true;

                // HSN was placed after every billing/hidden column in the designer.
                // Put it beside Product and Category so it is visible without
                // horizontal scrolling; row.Cells[phsncodeno] still uses its
                // stable collection index and does not change.
                int desiredDisplayIndex = Math.Min(4, dgv_product.Columns.Count - 1);
                if (hsnColumn.DisplayIndex != desiredDisplayIndex)
                    hsnColumn.DisplayIndex = desiredDisplayIndex;
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Partybooking page ConfigurePartyBookingHsnColumn()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void ConfigureResponsivePartyButtons()
        {
            if (tlp_partyoptions == null || pnl_hidecontrl == null ||
                pnl_moreoptions == null || flp_moreoptions == null)
                return;

            try
            {

                int screenWidth = Screen.FromControl(this).Bounds.Width;
                bool showSecondaryActionsDirectly = screenWidth >= 1366;
                bool hasSelectedPartyBooking = BookingId > 0 && SalesMasterId > 0;

                tlp_partyoptions.SuspendLayout();
                flp_moreoptions.SuspendLayout();

                try
                {
                    if (showSecondaryActionsDirectly)
                    {

                        PlacePartyButtonInRow(btncake, 5);
                        PlacePartyButtonInRow(btnotherarange, 6);
                        PlacePartyButtonInRow(btn_billpreview, 7);

                        btncake.Visible = true;
                        btnotherarange.Visible = true;
                        btn_billpreview.Visible = hasSelectedPartyBooking;


                        if (btn_more.Parent != pnl_hidecontrl)
                        {
                            btn_more.Parent?.Controls.Remove(btn_more);
                            pnl_hidecontrl.Controls.Add(btn_more);
                        }
                        btn_more.Visible = false;
                    }
                    else
                    {
                        if (btn_more.Parent != tlp_partyoptions)
                        {
                            btn_more.Parent?.Controls.Remove(btn_more);
                            tlp_partyoptions.Controls.Add(btn_more, 5, 0);
                        }
                        tlp_partyoptions.SetCellPosition(btn_more, new TableLayoutPanelCellPosition(5, 0));
                        btn_more.Visible = true;

                        PlacePartyButtonInMorePanel(btncake);
                        PlacePartyButtonInMorePanel(btnotherarange);
                        PlacePartyButtonInRow(btn_billpreview, 6);

                        btncake.Visible = true;
                        btnotherarange.Visible = true;
                        btn_billpreview.Visible = hasSelectedPartyBooking;
                    }
                }
                finally
                {
                    flp_moreoptions.ResumeLayout(true);
                    tlp_partyoptions.ResumeLayout(true);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Partybooking page ConfigureResponsivePartyButtons()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void PlacePartyButtonInRow(Button button, int column)
        {
            if (button == null)
                return;

            if (button.Parent != tlp_partyoptions)
            {
                button.Parent?.Controls.Remove(button);
                tlp_partyoptions.Controls.Add(button, column, 0);
            }

            tlp_partyoptions.SetCellPosition(button, new TableLayoutPanelCellPosition(column, 0));
            button.Margin = new Padding(0);
        }

        private void PlacePartyButtonInMorePanel(Button button)
        {
            if (button == null)
                return;

            if (button.Parent != flp_moreoptions)
            {
                button.Parent?.Controls.Remove(button);
                flp_moreoptions.Controls.Add(button);
            }

            // Keep each button's designer dimensions and preserve the More-panel order.
            button.Margin = new Padding(4);
        }

        private string GetPartyItemHsnCode(int productType, int itemId)
        {
            // Card products use the standard HSN requested for Party Booking.
            if (productType == (int)PackItemType.Card)
                return DefaultCardHsnCode;

            if (itemId <= 0)
                return string.Empty;

            try
            {
                string table;
                string idColumn;

                if (productType == (int)PackItemType.Pack)
                {
                    table = "tblPartyPackMaster";
                    idColumn = "PartyPackId";
                }
                else if (productType == (int)PackItemType.Menu)
                {
                    table = "tblItem";
                    idColumn = "ItemId";
                }
                else if (productType == (int)PackItemType.Venue)
                {
                    table = "tblPartyPackVenue";
                    idColumn = "PartyPackVenueId";
                }
                else if (productType == (int)PackItemType.Card)
                {
                    table = "[" + GameDB + "].dbo.tblCardProducts";
                    idColumn = "Product_id";
                }
                else if (productType == (int)PackItemType.Gift)
                {
                    table = "[" + GameDB + "].dbo.tblRedemptionProducts";
                    idColumn = "ProductId";
                }
                else
                {
                    table = "tblProduct";
                    idColumn = "ProductId";
                }

                BllSearch<DmnGridFields> b = new BllSearch<DmnGridFields>();
                string hsnCode = string.Empty;

                // Try the source table used to create the grid row first.
                try
                {
                    DmnSearch s = new DmnSearch
                    {
                        Select = "ISNULL(HSNCode,'') AS Field1",
                        Table = table,
                        WhereNdGroupBy = "WHERE " + idColumn + "=" + itemId
                    };

                    var result = b.GetSearchData("prdSearch", s);
                    hsnCode = result != null && result.Count > 0
                        ? Convert.ToString(result[0].Field1)
                        : string.Empty;
                }
                catch
                {
                    // Some installations keep the HSN on the central product master
                    // rather than the Card/Redemption source table. Try that fallback.
                    if (productType != (int)PackItemType.Card &&
                        productType != (int)PackItemType.Gift)
                        throw;
                }

                if (!string.IsNullOrWhiteSpace(hsnCode))
                    return hsnCode;

                if (productType == (int)PackItemType.Card ||
                    productType == (int)PackItemType.Gift ||
                    productType == (int)PackItemType.Menu)
                {
                    DmnSearch fallbackSearch = new DmnSearch
                    {
                        Select = "ISNULL(HSNCode,'') AS Field1",
                        Table = "[" + GameDB + "].dbo.tblProduct",
                        WhereNdGroupBy = "WHERE ProductId=" + itemId
                    };

                    var fallbackResult = b.GetSearchData("prdSearch", fallbackSearch);
                    return fallbackResult != null && fallbackResult.Count > 0
                        ? Convert.ToString(fallbackResult[0].Field1)
                        : string.Empty;
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Partybooking page GetPartyItemHsnCode()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));

                return string.Empty;
            }
        }

        #region More Option
        private void btn_more_Click(object sender, EventArgs e)
        {
            try
            {
                // Center the existing More panel inside the Party Booking form
                // each time it is opened; retain its designer size and styling.
                pnl_moreoptions.Left = Math.Max(0, (ClientSize.Width - pnl_moreoptions.Width) / 2);
                pnl_moreoptions.Top = Math.Max(0, (ClientSize.Height - pnl_moreoptions.Height) / 2);
                pnl_moreoptions.Visible = true;
                pnl_moreoptions.Visible = true;ShowBlurPopup(pnl_moreoptions);
                pnl_moreoptions.BringToFront();
                flp_moreoptions.BringToFront();
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Partybooking page btn_more_Click()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void btn_moreoption_close_Click(object sender, EventArgs e)
        {
            pnl_moreoptions.Visible = false;
            HideBlurPopup(pnl_moreoptions);
        }
        #endregion

        #region customer Bill Preview
        private void btn_billpreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (BookingId <= 0 || SalesMasterId <= 0)
                {
                    CMessageBox.Show(
                        "Select an existing party booking to view its bill.",
                        "Bill Preview",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                using (FoodandBeverage.Forms.FrmCombinedBill billForm =
                    new FoodandBeverage.Forms.FrmCombinedBill())
                {
                    billForm.printBill(
                        SalesMasterId,
                        Convert.ToInt32(BillType.Partybill));
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Partybooking page btn_billpreview_Click()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        #endregion

        #region Venue
        private int veniueNoOfAttendees = 0;
        private void LoadPartyBookingVenueOptions()
        {
            try
            {
                BllSearch<DmnComboBox> b = new BllSearch<DmnComboBox>();
                DmnSearch s = new DmnSearch
                {
                    Select = "PartyPackVenueId as Id, VenueName as Value",
                    Table = "tblPartyPackVenue",
                    WhereNdGroupBy = "WHERE Status=1 AND BranchId=" + FrmDashboard.BranchId,
                    OrderBy = "ORDER BY VenueName"
                };

                var items = b.GetSearchData("prdSearch", s);
                if (items == null)
                    return;

                items.Insert(0, new DmnComboBox
                {
                    Id = -1,
                    Value = "-- Select Venue --"
                });

                cmbvenu.DataSource = items;
                cmbvenu.DisplayMember = "Value";
                cmbvenu.ValueMember = "Id";
                cmbvenu.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Partybooking page LoadPartyBookingVenueOptions()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        

        private void btnvenue_Click(object sender, EventArgs e)
        {
            try
            {
                // Venue has its own existing panel; do not open the More-options panel.
                pnl_moreoptions.Visible = false;
                HideBlurPopup(pnl_moreoptions);
                pnl_venue.Left = Math.Max(0, (ClientSize.Width - pnl_venue.Width) / 2);
                pnl_venue.Top = Math.Max(0, (ClientSize.Height - pnl_venue.Height) / 2);
                pnl_venue.Visible = true;
                pnl_venue.Visible = true;ShowBlurPopup(pnl_venue);
                pnl_venue.BringToFront();
                cmbvenu.Focus();
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Partybooking page btnvenue_Click()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }



        private void btnsubmit_venue_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbvenu.SelectedValue == null ||
                    !int.TryParse(Convert.ToString(cmbvenu.SelectedValue), out int venueId) ||
                    venueId <= 0)
                {
                    CMessageBox.Show(
                        "Please select a venue.",
                        "Venue",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }

                ResetDiscount();
                CalculatevenueValues();

                decimal venueBaseAmount = 0m;
                decimal.TryParse(txtvenucost.Text, out venueBaseAmount);
                decimal otherArrangement = 0m;
                decimal.TryParse(txtOAAmount.Text, out otherArrangement);
                decimal otherDeduction = 0m;
                decimal.TryParse(txtODAmount.Text, out otherDeduction);
                decimal grossAmount = venueBaseAmount + otherArrangement - otherDeduction;
                if (grossAmount < 0m)
                {
                    CMessageBox.Show(
                        "Venue amount cannot be negative.",
                        "Venue",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return;
                }

                // Update an existing Venue row when editing a booking; otherwise add one.
                DataGridViewRow row = dgv_product.Rows
                    .Cast<DataGridViewRow>()
                    .FirstOrDefault(r => !r.IsNewRow &&
                        Convert.ToInt32(r.Cells[PPackProductTypeno].Value ?? 0) == (int)PackItemType.Venue);

                int rowIndex;
                if (row == null)
                {
                    rowIndex = dgv_product.Rows.Add();
                    row = dgv_product.Rows[rowIndex];
                    row.Cells[pslno].Value = rowIndex + 1;
                    row.Cells[proPartyPackDetailsId].Value = 0;
                }
                else
                {
                    rowIndex = row.Index;
                }

                row.Cells[pitemno].Value = cmbvenu.Text;
                row.Cells[pcategmenuno].Value = "Venue";
                row.Cells[pqtyno].Value = 1;
                row.Cells[pPrice].Value = grossAmount;
                row.Cells[pAmount].Value = grossAmount;
                row.Cells[pProductid].Value = venueId;
                row.Cells[pproducttype].Value = 0;
                row.Cells[PPackProductTypeno].Value = (int)PackItemType.Venue;
                row.Cells[pPartydeafult].Value = 2;
                row.Cells[ppridno].Value = 0;
                row.Cells[pmessageno].Value = string.Empty;
                row.Cells[premarksno].Value = string.Empty;
                row.Cells[pdiscper].Value = 0m;
                row.Cells[pdisamt].Value = 0m;

                row.Cells[ptax].Value = txttax_venue.Text;
                row.Cells[ptaxtype].Value = txttaxtype_venue.Text;
                decimal taxPercent = 0m, taxAmount = 0m, preTaxAmount = 0m, netAmount = 0m;
                decimal.TryParse(txtTaxPer_venue.Text, out taxPercent);
                decimal.TryParse(txtTotalTaxAmount_venue.Text, out taxAmount);
                decimal.TryParse(txtPreTaxAmount_venue.Text, out preTaxAmount);
                decimal.TryParse(txtGrandTotal_venue.Text, out netAmount);
                row.Cells[ptaxper].Value = taxPercent;
                row.Cells[ptaxamt].Value = taxAmount;
                row.Cells[ptaxsettingid].Value = GetInt(txttaxsettingid_venue);
                row.Cells[ppretaxamt].Value = preTaxAmount;
                row.Cells[pnetamt].Value = netAmount;

                decimal igstAmount = 0m, vatAmount = 0m;
                decimal.TryParse(txtIGSTAmt_venue.Text, out igstAmount);
                decimal.TryParse(txtVatamt_venue.Text, out vatAmount);
                row.Cells[pigstamt].Value = igstAmount;
                row.Cells[pvatamt].Value = vatAmount;
                row.Cells[phsncodeno].Value = GetPartyItemHsnCode((int)PackItemType.Venue, venueId);

                DataGridViewImageCell actionCell = row.Cells[pactionno] as DataGridViewImageCell;
                if (actionCell != null)
                {
                    actionCell.Value = Grid_Remove;
                    actionCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                }

                row.ReadOnly = true;

                CalculateAndApplyTax(rowIndex, (int)PackItemType.Venue, venueId);

                pnl_moreoptions.Visible = false;
                HideBlurPopup(pnl_moreoptions);
                pnl_venue.Visible = false;
                HideBlurPopup(pnl_venue);

                GetGrandTotal_All();
                RecalculateAllRowsAndTotals();
                if (Convert.ToDecimal(discountper.Text) > 0)
                    CalculateDiscountAmount();
                else
                    CalculateDiscountPer();
                CalculateNetAmount();
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Partybooking page btnsubmit_venue_Click()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void cmbvenu_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbvenu.SelectedValue != null &&
                    Convert.ToInt32(cmbvenu.SelectedValue) > 0)
                {
                    txtvenucost.Enabled = true;
                    FillVenuDetails(Convert.ToInt32(cmbvenu.SelectedValue));
                    CalculatevenueValues();
                }
                else
                {
                    txtvenucost.Text = "0.00";
                    txttaxsettingid_venue.Text = "0";
                    txttaxtype_venue.Clear();
                    txttax_venue.Clear();
                    txtVatamt_venue.Text = "0";
                    txtIGSTAmt_venue.Text = "0";
                    txtTaxPer_venue.Text = "0";
                    txtPreTaxAmount_venue.Text = "0";
                    txtTotalTaxAmount_venue.Text = "0";
                    txtGrandTotal_venue.Text = "0";
                }

                CalculateNetAmount();
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Partybooking page cmbvenu_SelectionChangeCommitted()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        public void FillVenuDetails(int venueId)
        {
            try
            {
                BllSearch<DmnPartyPackVenue> b = new BllSearch<DmnPartyPackVenue>();
                DmnSearch s = new DmnSearch
                {
                    Select = "*",
                    Table = "tblPartyPackVenue",
                    WhereNdGroupBy = "WHERE PartyPackVenueId=" + venueId +
                                     " AND Status=1 AND BranchId=" + FrmDashboard.BranchId
                };

                var items = b.GetSearchData("prdSearch", s);
                if (items == null || items.Count == 0)
                    return;

                var item = items[0];

                txtvenucost.Text = Convert.ToDecimal(item.VenueCost).ToString("0.00");
                txttaxtype_venue.Text = item.TaxType;
                txttaxsettingid_venue.Text = Convert.ToString(item.TaxSettingsId);
                txttax_venue.Text = item.Tax;
                txtVatamt_venue.Text = Convert.ToString(item.VatPrcnt);
                txtIGSTAmt_venue.Text = Convert.ToString(item.IgstPrcnt);
                txtTaxPer_venue.Text = (item.VatPrcnt + item.IgstPrcnt).ToString("0.00");
                veniueNoOfAttendees = Convert.ToInt32(item.NoOfAttendees);
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Partybooking page FillVenuDetails()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }

        private void CalculatevenueValues()
        {
            try
            {
                decimal GetDecimal(TextBox textBox)
                {
                    decimal.TryParse(textBox.Text, out decimal value);
                    return value;
                }

                decimal gross = GetDecimal(txtvenucost) +
                                GetDecimal(txtOAAmount) -
                                GetDecimal(txtODAmount);

                decimal percent = 0;
                decimal igst = 0;
                decimal cgst = 0;
                decimal sgst = 0;
                string taxType = string.Empty;
                string mode = txttax_venue.Text.Trim();

                if (int.TryParse(txttaxsettingid_venue.Text, out int taxId) && taxId > 0)
                {
                    BllSearch<DmnTaxSettings> b = new BllSearch<DmnTaxSettings>();
                    DmnSearch s = new DmnSearch
                    {
                        Select = "TaxType, IgstPrcnt, CgstPrcnt, SgstPrcnt, VatPrcnt",
                        Table = "tblTaxSettings",
                        WhereNdGroupBy = "WHERE Status=1 AND TaxSettingsId=" + taxId
                    };

                    var taxes = b.GetSearchData("prdSearch", s);
                    if (taxes != null && taxes.Count > 0)
                    {
                        taxType = taxes[0].TaxType;
                        igst = taxes[0].IgstPrcnt;
                        cgst = taxes[0].CgstPrcnt;
                        sgst = taxes[0].SgstPrcnt;

                        percent = string.Equals(
                            taxType,
                            "GST",
                            StringComparison.OrdinalIgnoreCase)
                            ? (igst > 0 ? igst : cgst + sgst)
                            : taxes[0].VatPrcnt;
                    }
                }

                decimal baseAmount = gross;
                decimal taxAmount = 0;
                decimal net = gross;

                if (percent > 0 &&
                    mode.Equals("INCLUSIVE", StringComparison.OrdinalIgnoreCase))
                {
                    baseAmount = gross / (1m + percent / 100m);
                    taxAmount = gross - baseAmount;
                }
                else if (percent > 0)
                {
                    taxAmount = gross * percent / 100m;
                    net = gross + taxAmount;
                }

                txttaxtype_venue.Text = taxType;
                txtTaxPer_venue.Text = percent.ToString("0.00");
                txtPreTaxAmount_venue.Text = baseAmount.ToString("0.00");
                txtTotalTaxAmount_venue.Text = taxAmount.ToString("0.00");
                txtGrandTotal_venue.Text = net.ToString("0.00");

                txtIGSTAmt_venue.Text = taxType == "GST" && igst > 0
                    ? taxAmount.ToString("0.00")
                    : "0.00";

                txtVatamt_venue.Text = taxType == "GST"
                    ? "0.00"
                    : taxAmount.ToString("0.00");
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Partybooking page CalculatevenueValues()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Partybooking));
            }
        }
        private void btn_venue_close_Click(object sender, EventArgs e)
        {
            pnl_venue.Visible = false;
            HideBlurPopup(pnl_venue);
        }
        #endregion

    }
}
