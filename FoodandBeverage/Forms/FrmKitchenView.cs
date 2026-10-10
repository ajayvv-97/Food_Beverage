using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CustomMessageBox;
using DAL;
using Domain;
using FastMember;
using FoodandBeverage.Classes;
using FoodandBeverage.Controls;
using FoodandBeverage.Interfaces;
using Microsoft.AspNet.SignalR.Client;

namespace FoodandBeverage.Forms
{
    public partial class FrmKitchenView : Form
    {
        private SoundPlayer soundPlayer;
        ReportDocument reportDocument = new ReportDocument();
        public FrmKitchenView()
        {
            InitializeComponent();
            soundPlayer = new SoundPlayer(FoodandBeverage.Properties.Resources.kitchen_order_sound);
            soundPlayer.Load();
        }

        #region Declaration Grid
        public int dslno = 0;
        public int dactionno = 1;
        public int dptypeno = 2;
        public int ditemno = 3;
        public int dqtyno = 4;
        public int dnotes = 5;
        public int dremnoteno = 6;
        public int ddetailsidno = 7;
        public int dkoidno = 8;
        public int dtidno = 9;
        public int dordermasteridno = 10;
        public int ddatetimeno = 11;
        public int dstatusno = 12;
        public int ditemidno = 13;
        public int dtabletypeno = 14;
        public int dorderno = 15;
        public int dAddonStatusno = 16;
        public int dAVIDno = 17;

        public static int addslno = 0;
        public int additemnameno = 1;
        public int addqtyno = 2;
        public int addordermasteridno = 3;
        public int addkoidno = 4;
        public int addorderdetailidno = 5;
        public int addactionno = 6;
        public int addavidno = 7;
        public int addpavidno = 8;
        public int addaddonstatusno = 9;
        public int addonpriceno = 10;
        public int addonorderno = 11;
        public int addccodeno = 12;
        #endregion

        Color boxcolor = Color.FromArgb(0x42, 0x67, 0xB2); //System.Drawing.ColorTranslator.FromHtml("#3AAFA9"); //
        public int dgv_view_rowno = 0;
        public string dgv_view_name = "";
        public List<datagrid> orderbox = new List<datagrid>();
        public bool pbcancelfoodclciked = false;
        private readonly HubConnection _hubConnection;
        private readonly IHubProxy _hubProxy;
        public int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        public string RPTPath = Convert.ToString(FrmLogin.KitchenBillPath);
        TableLayoutPanel tlp_order_col = new TableLayoutPanel();
        bool addingControls = false;
        public int KitchenDisplayCount = 0;
        ClsGeneral clsgen = new ClsGeneral();
        public int ReprintOrderNo = 0;
        public int ReprintId = 0;

        private bool isDragging = false;
        private Point startPoint = new Point(0, 0);
        private bool isCancelPrintingRunning = false;
        private bool isOrderPrintingRunning = false;

        Dictionary<string, int> rowCounters = new Dictionary<string, int>(); 
        Dictionary<string, bool> rowColorState = new Dictionary<string, bool>(); 
        int blinkPeriod = 1; 
        public int korderno = 0;

   
        private void FrmKitchenView_Load(object sender, EventArgs e)
        {
            try
            {
                cmbserachtype.SelectedIndex = 0;
                int stype = cmbserachtype.SelectedIndex;
                if (stype == 0)
                {
                    txtsearchData.Text = "Enter Kitchen Number...";
                }
                else if (stype == 1)
                {
                    txtsearchData.Text = "Enter Table Code...";
                }
                else if (stype == 2)
                {
                    txtsearchData.Text = "Enter Pickup No...";
                }

                if (FrmDashboard.AutoLogoutWhenIdle)
                {
                    //AppSession.RegisterForm(this, (int)LogoutFormList.Home);
                }

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

                txt_product_remove_des.Text = "Reason type here .....";
                textb_shape();
                tlp_order_col.Dock = DockStyle.Fill;

                InitTimer();

                KitchenDisplayCount = FrmLogin.KitchenDisplayCount;
                txt_product_remove_des.ForeColor = Color.DarkGray;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View FrmKitchenView_Load", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
        private void btncolse_Click(object sender, EventArgs e)
        {
            refreshtimer.Stop();
            this.Close();
            this.Dispose();
        }

        #region KitchenView - Cached card UI (Layout A) and blinking
        public class OrderCardCache
        {
            public ModernKitchenOrderCard CardPanel;
            public DataGridView Grid;
            public Label TimeLabel;
            public Label StatusLabel;
            public Label TableCodeLabel;
            public TextBox Description;
            public PictureBox BtnStart;
            public PictureBox BtnReady;
            public PictureBox BtnCancel;
            public PictureBox BtnReprint;
            public DateTime CreatedTime;

            // DescriptionPanel remains a Control reference for the existing status/update path.
            public Control DescriptionPanel;
            public int CancelStatus { get; set; } = -1;

        }
        private Dictionary<string, OrderCardCache> KitchenCardCache = new Dictionary<string, OrderCardCache>();
        private Dictionary<string, int> RowBlinkCounter = new Dictionary<string, int>();
        private Dictionary<string, bool> RowBlinkState = new Dictionary<string, bool>();
        private const int BlinkThreshold = 1;

        /// <summary>
        /// Main optimized FillOrdersAsync - creates cards once and updates on subsequent calls.
        /// </summary>
        public void FillOrders()
        {
            try
            {
                GC.Collect();
                SetEmptyIcon();
                //+ " and p.PreparationMode ="+ Convert.ToInt32(PreparationType.kitchen);

                txt_product_remove_des.Text = "Reason type here .....";

                // single master fetch
                BllKitchenData<DmnGridFields> SearchDetails = new BllKitchenData<DmnGridFields>();
                DmnGridFields dmnSearchKitchendata = new DmnGridFields();
                dmnSearchKitchendata.Searchtype = Convert.ToInt32(KitchenSearchType.AllOrders);
                var gridSourcemaster = SearchDetails.serachreport("prdKitchenData", dmnSearchKitchendata);

                if (gridSourcemaster == null || gridSourcemaster.Count == 0)
                {
                    btntotalorders.Text = "0";
                    ClearKitchenUI();
                    SetEmptyIcon();
                    return;
                }

                // distinct orders grouping
                var distinctOrders = gridSourcemaster
                   .GroupBy(order => new { order.Field1, order.Field2 })
                   .Select(group => group.First())
                   .OrderBy(order => order.Field1)
                   .ThenBy(order => order.Field2)
                   .ToList();

                btntotalorders.Text = Convert.ToString(distinctOrders.Count);
                int countpendingorder = distinctOrders.Count(order => Convert.ToInt32(order.Field3) == 1);
                btnprintcount.Text = Convert.ToString(countpendingorder);


                HashSet<string> currentKeys = new HashSet<string>();
                foreach (var od in distinctOrders)
                {
                    // unique key: Field1 (OrderMasterId) + '_' + Field17 (OrderNo will be fetched later with details, but use Field2? we will fetch details soon)
                    // We'll use Field1 + "_" + Field2 initially; after fetching details we will actually use Field1 + "_" + Field17.
                    // To keep consistent, we'll fetch details below and compute the real key.
                    // For removal, compute keys from existing cache and then check existence after creating keys list below.
                }
                List<string> newKeysInOrder = new List<string>();
                Dictionary<string, (DmnGridFields Header, List<Dmnkitchendetails> Details)> orderDataMap =
                    new Dictionary<string, (DmnGridFields, List<Dmnkitchendetails>)>();

                int rowCountDisplay = 0;
                foreach (var orders in distinctOrders)
                {
                    //if (rowCountDisplay >= KitchenDisplayCount) break;

                    BllkitchenDataDetails<Dmnkitchendetails> KtDetails = new BllkitchenDataDetails<Dmnkitchendetails>();
                    Dmnkitchendetails dmnSearchtabledata = new Dmnkitchendetails();
                    dmnSearchtabledata.OrderNo = Convert.ToInt32(orders.Field2); // fallback; real token/order no used below
                    dmnSearchtabledata.OrderMasterId = Convert.ToInt32(orders.Field1);
                    var gridSourcedetails = KtDetails.serachData("prdKitchendetails", dmnSearchtabledata);

                    if (gridSourcedetails == null || gridSourcedetails.Count == 0)
                        continue;

                    // Unique key: Field1 + "_" + Field17 (OrderNo from details)
                    string orderNoSuffix = Convert.ToString(gridSourcedetails[0].Field17);
                    string key = orders.Field1 + "_" + orderNoSuffix;

                    newKeysInOrder.Add(key);
                    orderDataMap[key] = (orders, gridSourcedetails);

                    rowCountDisplay++;
                }

                // Remove cached cards that are no longer present
                List<string> toRemove = new List<string>();
                foreach (var existingKey in KitchenCardCache.Keys)
                {
                    if (!newKeysInOrder.Contains(existingKey))
                        toRemove.Add(existingKey);
                }
                foreach (var rem in toRemove)
                {
                    try
                    {
                        var dead = KitchenCardCache[rem];
                        if (dead != null && dead.CardPanel != null)
                        {
                            flp_order_view.Controls.Remove(dead.CardPanel);
                            dead.CardPanel.Dispose();
                        }
                    }
                    catch 
                    { 

                    }
                    KitchenCardCache.Remove(rem);
                }

                // Add or update cards
                foreach (var kv in orderDataMap)
                {
                    string key = kv.Key;
                    DmnGridFields header = kv.Value.Header;
                    List<Dmnkitchendetails> details = kv.Value.Details;

                    OrderCardCache cache;
                    if (!KitchenCardCache.TryGetValue(key, out cache))
                    {
                        // create new card (build and add)
                        cache = BuildOrderCard(header, details);
                        if (cache != null && cache.CardPanel != null)
                        {
                            KitchenCardCache[key] = cache;
                            flp_order_view.Controls.Add(cache.CardPanel);
                        }
                    }
                    else
                    {
                        // update existing
                        UpdateOrderCard(cache, header, details);
                    }
                }

                timer1.Start();
                refreshtimer.Start();
                SetEmptyIcon();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View Page FillOrdersAsync()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        /// <summary>
        /// Create a full card (Layout A) for an order and return cache object.
        /// </summary>
        private OrderCardCache BuildOrderCard(DmnGridFields orders, List<Dmnkitchendetails> details)
        {
            try
            {
                if (details == null || details.Count == 0)
                    return null;

                string suffix = Convert.ToString(details[0].Field17);
                string orderId = Convert.ToString(orders.Field1);
                string cardName = "tlp_product_box_k_" + orderId + suffix;
                OrderCardCache cache = new OrderCardCache();

                int controlWidth = Math.Max(300, (int)(ClientSize.Width * 0.30));
                int controlHeight = Math.Max(250, (int)(ClientSize.Height * 0.40));
                int headerHeight = (screenWidth > 1367 && screenHeight > 769) ? 42 : 32;
                int buttonHeight = (screenWidth > 1367 && screenHeight > 769) ? 45 : 35;
                int buttonWidth = (screenWidth > 1367 && screenHeight > 769) ? 120 : 80;

                ModernKitchenOrderCard card = new ModernKitchenOrderCard
                {
                    Name = cardName,
                    Size = new Size(controlWidth, controlHeight),
                    MinimumSize = new Size(300, 230),
                    Margin = new Padding(12, 8, 8, 0),
                    BackColor = Color.White,
                    Visible = true,
                    RowCount = 4,
                    ColumnCount = 4
                };

                card.ColumnStyles.Clear();
                card.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
                card.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27F));
                card.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27F));
                card.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));
                card.RowStyles.Clear();
                card.RowStyles.Add(new RowStyle(SizeType.Absolute, headerHeight));
                card.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                card.RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));
                card.RowStyles.Add(new RowStyle(SizeType.Absolute, buttonHeight));
                card.SuspendLayout();

                Label tableCode = new Label
                {
                    Name = "lbl_tablecod_k_" + orderId + suffix,
                    Text = Convert.ToInt32(details[0].Field25) > 0
                        ? "PICK UP " + Convert.ToString(details[0].Field25)
                        : Convert.ToString(details[0].Field2),
                    Dock = DockStyle.Fill,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(8, 0, 2, 0),
                    Margin = Padding.Empty,
                    Font = ModernKitchenOrderCard.TableCodeFont,
                    ForeColor = Color.White
                };

                Label token = new Label
                {
                    Name = "lbl_tokkenno_k_" + orderId + suffix,
                    Text = "KITCHEN TOKEN " + Convert.ToString(details[0].Field22),
                    Dock = DockStyle.Fill,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = Padding.Empty,
                    Font = ModernKitchenOrderCard.TokenFont,
                    ForeColor = Color.White
                };

                Label statusLabel = new Label
                {
                    Name = "lbl_status_K_" + orderId + suffix,
                    Dock = DockStyle.Fill,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = new Padding(2, 5, 2, 5),
                    Font = ModernKitchenOrderCard.StatusFont,
                    ForeColor = Color.FromArgb(44, 96, 163)
                };

                TimeSpan elapsed = DateTime.Now - details[0].Fielddatetime;
                Label time = new Label
                {
                    Name = "lbl_time_K_" + orderId + suffix,
                    Text = FormatKitchenElapsed(elapsed),
                    Dock = DockStyle.Fill,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleRight,
                    Padding = new Padding(0, 0, 8, 0),
                    Margin = Padding.Empty,
                    Font = ModernKitchenOrderCard.TimeFont,
                    ForeColor = Color.White
                };

                card.Controls.Add(tableCode, 0, 0);
                card.Controls.Add(token, 1, 0);
                card.Controls.Add(statusLabel, 2, 0);
                card.Controls.Add(time, 3, 0);

                DataGridView grid = BuildProductGrid(orders, details);
                grid.Dock = DockStyle.Fill;
                grid.Margin = Padding.Empty;
                card.Controls.Add(grid, 0, 1);
                card.SetColumnSpan(grid, 4);

                TextBox description = BuildDescription(orders, details);
                description.Name = "txt_Description_k_" + orderId + suffix;
                description.Dock = DockStyle.Fill;
                description.Margin = new Padding(8, 4, 8, 4);
                description.BackColor = Color.White;
                description.ForeColor = Color.FromArgb(55, 65, 81);
                description.Visible = !string.IsNullOrWhiteSpace(Convert.ToString(details[0].Field16));
                card.Controls.Add(description, 0, 2);
                card.SetColumnSpan(description, 4);

                PictureBox startButton, readyButton, cancelButton, reprintButton;
                startButton = CreateKitchenActionButton(
                    "pb_start_k_" + orderId + suffix, Properties.Resources.start,
                    suffix, orderId, buttonWidth, buttonHeight, pbstart_Click);
                readyButton = CreateKitchenActionButton(
                    "pb_ready_k_" + orderId + suffix, Properties.Resources.ready_button,
                    suffix, orderId, buttonWidth, buttonHeight, pbreadyfood_Click);
                cancelButton = CreateKitchenActionButton(
                    "pictureBoxRemove_k_" + orderId + suffix, Properties.Resources.Kitchen_Ordercancel,
                    suffix, orderId, buttonWidth, buttonHeight, pbcancelfood_Click);
                reprintButton = CreateKitchenActionButton(
                    "reprintbutton_k_" + orderId + suffix, Properties.Resources.reprint,
                    suffix, orderId, buttonWidth, buttonHeight, reprintbutton_Click);

                card.Controls.Add(startButton, 0, 3);
                card.Controls.Add(readyButton, 1, 3);
                card.Controls.Add(cancelButton, 2, 3);
                card.Controls.Add(reprintButton, 3, 3);

                bool hasPendingStart = details.Any(d => Convert.ToInt32(d.Field12) == 1);
                bool waitingForCustomer = Convert.ToInt32(details[0].Field27) == 1;
                startButton.Visible = hasPendingStart && !waitingForCustomer;
                cancelButton.Visible = hasPendingStart && !waitingForCustomer;
                readyButton.Visible = !hasPendingStart && !waitingForCustomer;
                reprintButton.Visible = !waitingForCustomer;

                card.SetKitchenState(GetKitchenVisualState(waitingForCustomer, hasPendingStart));
                card.ResumeLayout(true);

                cache.CardPanel = card;
                cache.Grid = grid;
                cache.TimeLabel = time;
                cache.StatusLabel = statusLabel;
                cache.TableCodeLabel = tableCode;
                cache.Description = description;
                cache.DescriptionPanel = description;
                cache.BtnStart = startButton;
                cache.BtnReady = readyButton;
                cache.BtnCancel = cancelButton;
                cache.BtnReprint = reprintButton;
                cache.CreatedTime = details[0].Fielddatetime;
                cache.CancelStatus = Convert.ToInt32(details[0].Field27);

                UpdateGridFull(grid, details);
                if (waitingForCustomer)
                {
                    description.Text = "Waiting for customer confirmation.";
                    description.Visible = true;
                    cache.BtnStart.Visible = false;
                    cache.BtnReady.Visible = false;
                    cache.BtnCancel.Visible = false;
                    cache.BtnReprint.Visible = false;
                    ToggleDescriptionRow(card, true);
                }
                else
                {
                    description.Text = string.IsNullOrWhiteSpace(Convert.ToString(details[0].Field16))
                        ? string.Empty
                        : "Note: " + Convert.ToString(details[0].Field16);
                    bool hasDescription = !string.IsNullOrWhiteSpace(Convert.ToString(details[0].Field16));
                    ToggleDescriptionRow(card, hasDescription);
                }

                if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                {
                    FrmDashboard dashboard = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                    if (dashboard != null)
                        card.Enabled = dashboard.ChkWritePermission(Convert.ToInt32(formsenum.Kitchen_View));
                }

                return cache;
            }
            catch (Exception ex)
            {
                clsgen.LogException("FrmKitchenView BuildOrderCard()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Kitchen_View));
                return null;
            }
        }

        private PictureBox CreateKitchenActionButton(
            string name, Image image, object suffix, object orderMasterId,
            int width, int height, EventHandler clickHandler)
        {
            PictureBox button = new PictureBox
            {
                Name = name,
                Image = image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Dock = DockStyle.Fill,
                Margin = new Padding(3, 2, 3, 2),
                Tag = suffix,
                AccessibleDescription = Convert.ToString(orderMasterId),
                Cursor = Cursors.Hand
            };
            button.Click += clickHandler;
            return button;
        }

        private static string FormatKitchenElapsed(TimeSpan elapsed)
        {
            int hours = Math.Max(0, (int)elapsed.TotalHours);
            return hours + " h, " + elapsed.Minutes + " min " + elapsed.Seconds + " s";
        }

        private static KitchenOrderVisualState GetKitchenVisualState(bool waitingForCustomer, bool pendingStart)
        {
            if (waitingForCustomer)
                return KitchenOrderVisualState.WaitingForCustomer;
            return pendingStart ? KitchenOrderVisualState.NewOrder : KitchenOrderVisualState.Ready;
        }

        /// <summary>
        /// Header builder: creates blue header with table/pickup, token no and time label.
        /// </summary>
        private TableLayoutPanel BuildHeader(DmnGridFields orders, List<Dmnkitchendetails> details)
        {
            string suffix = Convert.ToString(details[0].Field17);

            TableLayoutPanel tlp_product_heading = new TableLayoutPanel();
            tlp_product_heading.Name = "tlp_product_heading_k_" + orders.Field1 + suffix;
            tlp_product_heading.Dock = DockStyle.Top;
            tlp_product_heading.ColumnCount = 3;
            tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlp_product_heading.Height = (screenWidth > 1367 && screenHeight > 769) ? 50 : 28;
            tlp_product_heading.BackColor = Color.FromArgb(60, 90, 160); // stronger blue header

            // Table code / pickup
            Label lbl_tablecode = new Label();
            lbl_tablecode.Name = "lbl_tablecod_k_" + orders.Field1 + suffix;
            lbl_tablecode.Dock = DockStyle.Fill;
            lbl_tablecode.ForeColor = Color.White;
            lbl_tablecode.AutoSize = true;
            lbl_tablecode.TextAlign = ContentAlignment.MiddleLeft;

            int pickupNo = 0;
            int.TryParse(Convert.ToString(details[0].Field25), out pickupNo);
            if (pickupNo > 0)
            {
                lbl_tablecode.Text = "Pick Up  " + pickupNo;
                lbl_tablecode.Font = new Font("Arial", 12, FontStyle.Bold);
            }
            else
            {
                lbl_tablecode.Text = Convert.ToString(details[0].Field2);
                lbl_tablecode.Font = (screenWidth > 1367 && screenHeight > 769) ? new Font("Arial", 15, FontStyle.Bold)
                                                                             : new Font("Arial", 10, FontStyle.Bold);
            }

            // Token no
            Label lbl_tokkenno = new Label();
            lbl_tokkenno.Text = "Kitchen Token No: " + Convert.ToString(details[0].Field22);
            lbl_tokkenno.Name = "lbl_tokkenno_k_" + orders.Field1 + suffix;
            lbl_tokkenno.Dock = DockStyle.Fill;
            lbl_tokkenno.ForeColor = Color.White;
            lbl_tokkenno.AutoSize = true;
            lbl_tokkenno.TextAlign = ContentAlignment.MiddleCenter;
            lbl_tokkenno.Font = (screenWidth > 1367 && screenHeight > 769) ? new Font("Arial", 15, FontStyle.Bold)
                                                                             : new Font("Arial", 12, FontStyle.Bold);

            // Time label
            Label lbl_time = new Label();
            lbl_time.Text = ""; // will be updated by UpdateOrderCard
            lbl_time.Name = "lbl_time_K_" + orders.Field1 + suffix;
            lbl_time.Dock = DockStyle.Fill;
            lbl_time.ForeColor = Color.White;
            lbl_time.AutoSize = true;
            lbl_time.TextAlign = ContentAlignment.MiddleRight;
            lbl_time.Font = (screenWidth > 1367 && screenHeight > 769) ? new Font("Arial", 11, FontStyle.Bold)
                                                                          : new Font("Arial", 8, FontStyle.Bold);

            tlp_product_heading.Controls.Add(lbl_tablecode, 0, 0);
            tlp_product_heading.Controls.Add(lbl_tokkenno, 1, 0);
            tlp_product_heading.Controls.Add(lbl_time, 2, 0);

            return tlp_product_heading;
        }

        /// <summary>
        /// Build the DataGridView for kitchen card (uses your column indices).
        /// </summary>
        private DataGridView BuildProductGrid(DmnGridFields orders, List<Dmnkitchendetails> details)
        {
            DataGridView dgv_product_details = new DataGridView();
            string suffix = Convert.ToString(details[0].Field17);

            dgv_product_details.Name = "dgv_product_details_k_" + orders.Field1 + suffix;
            dgv_product_details.Tag = orders.Field1 + suffix;
            dgv_product_details.CellClick += dgv_product_details_CellClick;
            dgv_product_details.AllowUserToAddRows = false;
            dgv_product_details.AllowUserToDeleteRows = true;
            dgv_product_details.RowHeadersVisible = false;
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
            dgv_product_details.AllowUserToResizeRows = false;

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

            // create columns in same order as your indices
            dgv_product_details.Columns.Clear();

            // 0 - Slno (hidden)
            dgv_product_details.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Slno", HeaderText = "Slno" });

            // 1 - Action (image)
            dgv_product_details.Columns.Add(new DataGridViewImageColumn() { Name = "Action", HeaderText = "Action" });

            // 2 - PType (image)
            dgv_product_details.Columns.Add(new DataGridViewImageColumn() { Name = "PType", HeaderText = "PType" });

            // 3 - Item Name
            dgv_product_details.Columns.Add(new DataGridViewTextBoxColumn() { Name = "ItemName", HeaderText = "Item Name" });

            // 4 - Qty
            dgv_product_details.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Qty", HeaderText = "Qty" });

            // 5 - Notes
            dgv_product_details.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Notes", HeaderText = "Notes" });

            // 6 - Remove Notes
            dgv_product_details.Columns.Add(new DataGridViewTextBoxColumn() { Name = "RemoveNotes", HeaderText = "Remove Notes" });

            // 7 - orderdetailsid (hidden)
            dgv_product_details.Columns.Add(new DataGridViewTextBoxColumn() { Name = "orderdetailsid", HeaderText = "orderdetailsid" });

            // 8 - koid (hidden)
            dgv_product_details.Columns.Add(new DataGridViewTextBoxColumn() { Name = "koid", HeaderText = "koid" });

            // 9 - tid (hidden)
            dgv_product_details.Columns.Add(new DataGridViewTextBoxColumn() { Name = "tid", HeaderText = "tid" });

            // 10 - Ordermasterid (hidden)
            dgv_product_details.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Ordermasterid", HeaderText = "Ordermasterid" });

            // 11 - datetime (hidden)
            dgv_product_details.Columns.Add(new DataGridViewTextBoxColumn() { Name = "datetime", HeaderText = "datetime" });

            // 12 - status (hidden)
            dgv_product_details.Columns.Add(new DataGridViewTextBoxColumn() { Name = "status", HeaderText = "status" });

            // 13 - itemid (hidden)
            dgv_product_details.Columns.Add(new DataGridViewTextBoxColumn() { Name = "itemid", HeaderText = "itemid" });

            // 14 - tabletype (hidden)
            dgv_product_details.Columns.Add(new DataGridViewTextBoxColumn() { Name = "tabletype", HeaderText = "tabletype" });

            // 15 - orderno (hidden)
            dgv_product_details.Columns.Add(new DataGridViewTextBoxColumn() { Name = "orderno", HeaderText = "orderno" });

            // 16 - AddonStatus (hidden)
            dgv_product_details.Columns.Add(new DataGridViewTextBoxColumn() { Name = "AddonStatus", HeaderText = "AddonStatus" });

            // 17 - AVID (hidden)
            dgv_product_details.Columns.Add(new DataGridViewTextBoxColumn() { Name = "AVID", HeaderText = "AVID" });

            // Hide backend columns
            //dgv_product_details.Columns[0].Visible = false; // Slno
            for (int i = 7; i <= 17; i++)
            {
                dgv_product_details.Columns[i].Visible = false;
                dgv_product_details.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgv_product_details.Columns[i].Width = 1;
            }

            // fix widths
            dgv_product_details.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgv_product_details.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgv_product_details.ScrollBars = ScrollBars.Both;
            dgv_product_details.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            // item name wide
            dgv_product_details.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv_product_details.Columns[3].Width = 260;

            // image columns width
            dgv_product_details.Columns[1].Width = 60;
            dgv_product_details.Columns[2].Width = 80;

            dgv_product_details.AllowUserToResizeColumns = false;

            return dgv_product_details;
        }

        /// <summary>
        /// Build description textbox
        /// </summary>
        private TextBox BuildDescription(DmnGridFields orders, List<Dmnkitchendetails> details)
        {
            TextBox txt_Description = new TextBox();
            txt_Description.Text = "Note :   " + Convert.ToString(details[0].Field16);
            txt_Description.Name = "txt_Description_k_" + orders.Field1 + details[0].Field17;
            txt_Description.Dock = DockStyle.Fill;
            txt_Description.ForeColor = Color.White;
            txt_Description.BackColor = boxcolor;
            txt_Description.ReadOnly = true;
            txt_Description.Multiline = true;
            txt_Description.ScrollBars = ScrollBars.Vertical;
            txt_Description.BorderStyle = BorderStyle.None;
            txt_Description.Visible = !string.IsNullOrEmpty(Convert.ToString(details[0].Field16));
            txt_Description.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold);
            return txt_Description;
        }

        /// <summary>
        /// Build bottom buttons and return panel; out parameters for buttons.
        /// </summary>
        private TableLayoutPanel BuildButtons(DmnGridFields orders, List<Dmnkitchendetails> details, out PictureBox startBtn, out PictureBox readyBtn, out PictureBox cancelBtn, out PictureBox reprintBtn)
        {
            string suffix = Convert.ToString(details[0].Field17);

            TableLayoutPanel tlp_buttons = new TableLayoutPanel();
            tlp_buttons.Name = "tlp_buttons_k_" + orders.Field1 + suffix;
            tlp_buttons.Dock = DockStyle.Top;
            tlp_buttons.ColumnCount = 4;
            tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            tlp_buttons.Height = (screenWidth > 1367 && screenHeight > 769) ? 45 : 35;
            tlp_buttons.BackColor = Color.FromArgb(60, 90, 160); // same blue as header base

            startBtn = new PictureBox();
            startBtn.Name = "pb_start_k_" + orders.Field1 + suffix;
            startBtn.Image = Properties.Resources.start;
            startBtn.SizeMode = PictureBoxSizeMode.StretchImage;
            startBtn.Dock = DockStyle.None;
            startBtn.Tag = suffix;
            startBtn.AccessibleDescription = Convert.ToString(orders.Field1);
            startBtn.Click += pbstart_Click;
            startBtn.Width = (screenWidth > 1367 && screenHeight > 769) ? 120 : 80;
            startBtn.Height = (screenWidth > 1367 && screenHeight > 769) ? 40 : 30;

            readyBtn = new PictureBox();
            readyBtn.Name = "pb_ready_k_" + orders.Field1 + suffix;
            readyBtn.Image = Properties.Resources.ready_button;
            readyBtn.SizeMode = PictureBoxSizeMode.StretchImage;
            readyBtn.Dock = DockStyle.None;
            readyBtn.Tag = suffix;
            readyBtn.AccessibleDescription = Convert.ToString(orders.Field1);
            readyBtn.Click += pbreadyfood_Click;
            readyBtn.Width = startBtn.Width;
            readyBtn.Height = startBtn.Height;

            cancelBtn = new PictureBox();
            cancelBtn.Name = "pictureBoxRemove_k_" + orders.Field1 + suffix;
            cancelBtn.Image = Properties.Resources.Kitchen_Ordercancel;
            cancelBtn.SizeMode = PictureBoxSizeMode.StretchImage;
            cancelBtn.Dock = DockStyle.None;
            cancelBtn.Tag = suffix;
            cancelBtn.AccessibleDescription = Convert.ToString(orders.Field1);
            cancelBtn.Click += pbcancelfood_Click;
            cancelBtn.Width = startBtn.Width;
            cancelBtn.Height = startBtn.Height;

            reprintBtn = new PictureBox();
            reprintBtn.Name = "reprintbutton_k_" + orders.Field1 + suffix;
            reprintBtn.Image = Properties.Resources.reprint;
            reprintBtn.SizeMode = PictureBoxSizeMode.StretchImage;
            reprintBtn.Dock = DockStyle.None;
            reprintBtn.Tag = suffix;
            reprintBtn.AccessibleDescription = Convert.ToString(orders.Field1);
            reprintBtn.Click += reprintbutton_Click;
            reprintBtn.Width = startBtn.Width;
            reprintBtn.Height = startBtn.Height;

            // decide initial visibility based on details statuses
            bool flagstart_ready = details.Any(d => Convert.ToInt32(d.Field12) == 1);
            if (flagstart_ready)
            {
                startBtn.Visible = true;
                cancelBtn.Visible = true;
                readyBtn.Visible = false;
            }
            else
            {
                startBtn.Visible = false;
                readyBtn.Visible = true;
                cancelBtn.Visible = false;
            }

            tlp_buttons.Controls.Add(startBtn, 0, 0);
            tlp_buttons.Controls.Add(readyBtn, 1, 0);
            tlp_buttons.Controls.Add(cancelBtn, 2, 0);
            tlp_buttons.Controls.Add(reprintBtn, 3, 0);

            return tlp_buttons;
        }

        /// <summary>
        /// Update a card's dynamic parts: time, description, buttons and grid
        /// </summary>
        private void UpdateOrderCard(OrderCardCache cache, DmnGridFields orders, List<Dmnkitchendetails> details)
        {
            try
            {
                if (cache == null || cache.CardPanel == null ||
                    cache.Grid == null || details == null || details.Count == 0)
                    return;

                TimeSpan elapsed = DateTime.Now - details[0].Fielddatetime;
                if (cache.TimeLabel != null)
                    cache.TimeLabel.Text = FormatKitchenElapsed(elapsed);

                if (cache.Grid.Rows.Count == details.Count)
                    UpdateGridLight(cache.Grid, details);
                else
                    UpdateGridFull(cache.Grid, details);

                bool waitingForCustomer = Convert.ToInt32(details[0].Field27) == 1;
                bool hasPendingStart = details.Any(d => Convert.ToInt32(d.Field12) == 1);
                bool hasDescription = !string.IsNullOrWhiteSpace(Convert.ToString(details[0].Field16));

                if (waitingForCustomer)
                {
                    if (cache.BtnStart != null) cache.BtnStart.Visible = false;
                    if (cache.BtnReady != null) cache.BtnReady.Visible = false;
                    if (cache.BtnCancel != null) cache.BtnCancel.Visible = false;
                    if (cache.BtnReprint != null) cache.BtnReprint.Visible = false;

                    if (cache.Description != null)
                    {
                        cache.Description.Text = "Waiting for customer confirmation.";
                        cache.Description.Visible = true;
                    }
                    if (cache.DescriptionPanel != null)
                        cache.DescriptionPanel.Visible = true;

                    ToggleDescriptionRow(cache.CardPanel, true);
                }
                else
                {
                    if (cache.BtnStart != null) cache.BtnStart.Visible = hasPendingStart;
                    if (cache.BtnReady != null) cache.BtnReady.Visible = !hasPendingStart;
                    if (cache.BtnCancel != null) cache.BtnCancel.Visible = hasPendingStart;
                    if (cache.BtnReprint != null) cache.BtnReprint.Visible = true;

                    if (cache.Description != null)
                    {
                        cache.Description.Text = hasDescription
                            ? "Note: " + Convert.ToString(details[0].Field16)
                            : string.Empty;
                        cache.Description.Visible = hasDescription;
                    }
                    if (cache.DescriptionPanel != null)
                        cache.DescriptionPanel.Visible = hasDescription;

                    ToggleDescriptionRow(cache.CardPanel, hasDescription);
                }

                cache.CardPanel.SetKitchenState(
                    GetKitchenVisualState(waitingForCustomer, hasPendingStart));
                cache.CancelStatus = Convert.ToInt32(details[0].Field27);
            }
            catch (Exception ex)
            {
                clsgen.LogException("FrmKitchenView UpdateOrderCard()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        private void ToggleDescriptionRow(TableLayoutPanel card, bool show)
        {
            if (card == null || card.RowStyles.Count < 4) return;

            card.SuspendLayout();

            if (show)
            {
                // SHOW description
                card.RowStyles[2].SizeType = SizeType.AutoSize;
                card.RowStyles[2].Height = 0;

                // Grid normal
                card.RowStyles[1].SizeType = SizeType.Percent;
                card.RowStyles[1].Height = 100;
            }
            else
            {
                // HIDE description COMPLETELY
                card.RowStyles[2].SizeType = SizeType.Absolute;
                card.RowStyles[2].Height = 0;

                // Grid must take FULL SPACE
                card.RowStyles[1].SizeType = SizeType.Percent;
                card.RowStyles[1].Height = 100;
            }

            card.ResumeLayout(true);
            card.PerformLayout();
        }
        /// <summary>
        /// UpdateGrid with blinking canceled rows logic.
        /// Uses KOID (Field8) as unique row key; falls back to orderdetailsid (Field9)
        /// </summary>
        private void UpdateGridFull( DataGridView dgv_product_details, List<Dmnkitchendetails> gridSourcedetails)
        {
            if (dgv_product_details == null || gridSourcedetails == null)
                return;

            // Save scroll position
            int scrollIndex = -1;
            if (dgv_product_details.FirstDisplayedScrollingRowIndex >= 0)
                scrollIndex = dgv_product_details.FirstDisplayedScrollingRowIndex;

            dgv_product_details.SuspendLayout();
            dgv_product_details.Rows.Clear();

            bool comboflag = false;
            bool addonflag = false;
            int mainNo=1, subNo = 1;
            for (int k = 0; k < gridSourcedetails.Count; k++)
            {
                var rowObj = gridSourcedetails[k];
                dgv_product_details.Rows.Add();

                // Unique key for blinking
                string keyRow = Convert.ToString(rowObj.Field8);
                if (string.IsNullOrEmpty(keyRow))
                    keyRow = Convert.ToString(rowObj.Field9);

                if (!RowBlinkCounter.ContainsKey(keyRow))
                    RowBlinkCounter[keyRow] = 0;

                if (!RowBlinkState.ContainsKey(keyRow))
                    RowBlinkState[keyRow] = false;

                // Sl No
                dgv_product_details.Rows[k].Cells[dslno].Value = k + 1;

                #region Slno Setting
                int prodStatus = Convert.ToInt32(rowObj.Field19);
                // MAIN PRODUCT
                if (prodStatus == 1)
                {
                    dgv_product_details.Rows[k].Cells[dslno].Value = mainNo;
                    dgv_product_details.Rows[k].Cells[dslno].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    mainNo++;
                    subNo = 1;
                }
                // SUB PRODUCT
                else if (prodStatus == 2)
                {
                    dgv_product_details.Rows[k].Cells[dslno].Value = subNo;
                    dgv_product_details.Rows[k].Cells[dslno].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgv_product_details.Rows[k].DefaultCellStyle.BackColor =   Color.WhiteSmoke;
                    subNo++;
                }
                #endregion

                // Item name + variant
                string variant = Convert.ToString(rowObj.Field24);
                string itemName = Convert.ToString(rowObj.Field4);
                dgv_product_details.Rows[k].Cells[ditemno].Value =
                    !string.IsNullOrEmpty(variant)
                        ? itemName + " ( " + variant + " )"
                        : itemName;

                // PType icon
                DataGridViewImageCell typeCell =
                    (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dptypeno];

                if (Convert.ToInt32(rowObj.Field13) == Convert.ToInt32(Tabletype.ComboProduct))
                {
                    comboflag = true;
                    typeCell.Value = Properties.Resources.kitchengridcomboicon;
                }
                else if (Convert.ToInt32(rowObj.Field23) >= 1)
                {
                    addonflag = true;
                    typeCell.Value = Properties.Resources.addonbutton;
                }
                else
                {
                    typeCell.Value = (k % 2 == 0)
                        ? Properties.Resources.orderview_blank
                        : Properties.Resources.orderview_blank_2;
                }

                typeCell.ImageLayout = DataGridViewImageCellLayout.Zoom;

                // Qty
                decimal qty;
                decimal.TryParse(Convert.ToString(rowObj.Field5), out qty);
                dgv_product_details.Rows[k].Cells[dqtyno].Value = (int)Math.Floor(qty);


                // Backend fields
                dgv_product_details.Rows[k].Cells[dnotes].Value = rowObj.Field6;
                dgv_product_details.Rows[k].Cells[dremnoteno].Value = rowObj.Field11;
                dgv_product_details.Rows[k].Cells[ddetailsidno].Value = rowObj.Field9;
                dgv_product_details.Rows[k].Cells[dkoidno].Value = rowObj.Field8;
                dgv_product_details.Rows[k].Cells[dtidno].Value = rowObj.Field10;
                dgv_product_details.Rows[k].Cells[dordermasteridno].Value = rowObj.Field1;
                dgv_product_details.Rows[k].Cells[ddatetimeno].Value = rowObj.Fielddatetime;
                dgv_product_details.Rows[k].Cells[dstatusno].Value = rowObj.Field12;
                dgv_product_details.Rows[k].Cells[ditemidno].Value = rowObj.Field14;
                dgv_product_details.Rows[k].Cells[dtabletypeno].Value = rowObj.Field15;
                dgv_product_details.Rows[k].Cells[dorderno].Value = rowObj.Field17;

                dgv_product_details.Rows[k].Cells[dAddonStatusno].Value = rowObj.Field19;
                dgv_product_details.Rows[k].Cells[dAVIDno].Value = rowObj.Field20;

                // Addon
                //if (Convert.ToInt32(rowObj.Field19) == Convert.ToInt32(AddonType.AddonType))
                //{
  
                //}
                //else
                //{
                //    dgv_product_details.Rows[k].Cells[dAddonStatusno].Value =  Convert.ToInt32(Product_Addon_Variation_Type.Addon);
                //    dgv_product_details.Rows[k].Cells[dAVIDno].Value = rowObj.Field20;
                //}

                // Action + blinking
                DataGridViewImageCell actionCell =
                    (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dactionno];

                int status;
                int.TryParse(Convert.ToString(rowObj.Field12), out status);

                if (status == 0)
                {
                    RowBlinkCounter[keyRow]++;
                    if (RowBlinkCounter[keyRow] >= BlinkThreshold)
                    {
                        RowBlinkCounter[keyRow] = 0;
                        RowBlinkState[keyRow] = !RowBlinkState[keyRow];
                    }

                    dgv_product_details.Rows[k].DefaultCellStyle.BackColor =
                        RowBlinkState[keyRow] ? Color.LightCoral : Color.White;

                    actionCell.Value = Properties.Resources.orderview_cancelled;
                }
                else
                {
                    actionCell.Value = Properties.Resources.kitchenfood_cancel_icon;

                    if (Convert.ToInt32(rowObj.Field19) == 1)
                        dgv_product_details.Rows[k].DefaultCellStyle.BackColor = Color.White;

                }

                actionCell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                dgv_product_details.Rows[k].ReadOnly = true;
            }

            dgv_product_details.Columns[dptypeno].Visible = comboflag || addonflag;

            dgv_product_details.ResumeLayout();

            // Restore scroll
            if (scrollIndex >= 0 && scrollIndex < dgv_product_details.RowCount)
                dgv_product_details.FirstDisplayedScrollingRowIndex = scrollIndex;
        }


        private void UpdateGridLight( DataGridView dgv, List<Dmnkitchendetails> details)
        {
            if (dgv == null || details == null) return;

            for (int k = 0; k < details.Count; k++)
            {
                if (k >= dgv.Rows.Count) break;

                var rowObj = details[k];
                DataGridViewRow row = dgv.Rows[k];

                // Unique blink key
                string keyRow = Convert.ToString(rowObj.Field8);
                if (string.IsNullOrEmpty(keyRow))
                    keyRow = Convert.ToString(rowObj.Field9);

                if (!RowBlinkCounter.ContainsKey(keyRow))
                    RowBlinkCounter[keyRow] = 0;
                if (!RowBlinkState.ContainsKey(keyRow))
                    RowBlinkState[keyRow] = false;

                int status = Convert.ToInt32(rowObj.Field12);
                var actionCell = (DataGridViewImageCell)row.Cells[dactionno];

                if (status == 0) // cancelled
                {
                    RowBlinkCounter[keyRow]++;
                    if (RowBlinkCounter[keyRow] >= BlinkThreshold)
                    {
                        RowBlinkCounter[keyRow] = 0;
                        RowBlinkState[keyRow] = !RowBlinkState[keyRow];
                    }

                    row.DefaultCellStyle.BackColor =
                        RowBlinkState[keyRow] ? Color.LightCoral : Color.White;

                    actionCell.Value = Properties.Resources.orderview_cancelled;
                }
                else
                {
                    actionCell.Value = Properties.Resources.kitchenfood_cancel_icon;
                    if (Convert.ToInt32(rowObj.Field19) == 1)
                        row.DefaultCellStyle.BackColor = Color.White;

                }

                // Live fields (safe)
                row.Cells[dqtyno].Value =
                    (int)Math.Floor(Convert.ToDecimal(rowObj.Field5));
                row.Cells[dnotes].Value = rowObj.Field6;
                row.Cells[dremnoteno].Value = rowObj.Field11;
            }
        }


        /// <summary>
        /// Clear all cached UI and dispose controls
        /// </summary>
        private void ClearKitchenUI()
        {
            try
            {
                refreshtimer.Stop();
                timer1.Stop();

                foreach (KeyValuePair<string, OrderCardCache> kv in KitchenCardCache.ToList())
                {
                    try
                    {
                        if (kv.Value != null && kv.Value.CardPanel != null)
                        {
                            flp_order_view.Controls.Remove(kv.Value.CardPanel);
                            kv.Value.CardPanel.Dispose();
                        }
                    }
                    catch { }
                }
                KitchenCardCache.Clear();

                foreach (Control ctrl in flp_order_view.Controls)
                {
                    try { ctrl.Dispose(); } catch { }
                }
                flp_order_view.Controls.Clear();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Pickuppage ClearKitchenUI()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        #endregion

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PlaySound()
        {
        //    try
        //    {
        //        soundPlayer.Play();
        //    }
        //    catch (Exception ex)
        //    {
        //        //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        clsgen.LogException("Kitchen_View Page PlaySound()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
        //    }
        }

        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtsearchData, txt_product_remove_des, txt_product_remove_des })
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

        public void InitTimer()
        {
            refreshtimer.Start();
        }

        private void PrintOrderAsync()
        {
            if (isOrderPrintingRunning) return;
            isOrderPrintingRunning = true;
            ReportDocument reportDocument = new ReportDocument();
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                BllKitchenOrderPrint<DmnGridFields> SearchDetails = new BllKitchenOrderPrint<DmnGridFields>();
                DmnGridFields dmnSearchKitchendata = new DmnGridFields();
                var gridSourcemaster = SearchDetails.serachreport("prdPrintOrder", dmnSearchKitchendata);
                if (gridSourcemaster == null || gridSourcemaster.Count == 0)
                {
                    btntotalorders.Text = "0";
                    return;
                }
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Start();
                        refreshtimer.Stop();
                    }));
                }
                var distinctOrders = gridSourcemaster
                    .GroupBy(order => new { order.Field1, order.Field2 })
                    .Select(group => group.First())
                    .OrderBy(order => order.Field1)
                    .ThenBy(order => order.Field2)
                    .ToList();

                int countpendingorder = distinctOrders.Count(order => Convert.ToInt32(order.Field3) == 1);
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        btnprintcount.Text = Convert.ToString(countpendingorder);
                    }));
                }

                foreach (var orders in distinctOrders)
                {
                    #region Crystal Report Declaration
                    DataSet dsReport = new DataSet();
                    DataTable dtSalesDetail = new DataTable("DT_kitchenbill");
                    BllSearch<DmnkitchenBilldetails> bllSearchDetails = new BllSearch<DmnkitchenBilldetails>();
                    DmnSearch dmnSearchDetails = new DmnSearch();

                    //dmnSearchDetails.Select = "od.itemName ItemName, od.Quantity Quantity, od.Notes Notes, od.MainRemarks Description, ti.TableCode TableCode, om.OrderMasterId OrderMasterId, od.AddonStatus AddonStatus, od.AV_id AV_id, od.TableType TableType,'' offeritemname, 0 offerqty, od.PrId PrId, ko.Kitchennumber,od.VariatiionName VariatiionName" +
                    //" from tblkitchenOrder ko " +
                    //    " Left Join tblOrderDetails od on od.OrderDetailsId=ko.OrderDetailsId " +
                    //    " Left Join tblOrderMaster om on om.OrderMasterId=ko.OrderMasterId " +
                    //    " Left Join tblTable_info ti on ti.Tid=ko.Tid " +
                    //    $" where om.status=1 and ko.OrderMasterId={orders.Field1} and ko.OrderNo={orders.Field2}" + " and od.TableType!=" + Convert.ToInt32(Tabletype.ComboProduct)+
                    //" union select od.itemName ItemName, od.Quantity Quantity, od.Notes Notes, od.MainRemarks Description, ti.TableCode TableCode, om.OrderMasterId OrderMasterId, od.AddonStatus AddonStatus, od.AV_id AV_id, od.TableType TableType, ofd.Productname offeritemname, ofd.Quantity offerqty, od.PrId PrId, ko.Kitchennumber, od.VariatiionName VariatiionName ";
                    //dmnSearchDetails.Table = " tblkitchenOrder ko" +
                    //                        " Left Join tblOrderDetails od on od.OrderDetailsId = ko.OrderDetailsId" +
                    //                        " Left Join tblOrderMaster om on om.OrderMasterId = ko.OrderMasterId" +
                    //                        " Left Join tblTable_info ti on ti.Tid = ko.Tid" +
                    //                        " Left Join tblOfferMaster ofm on ofm.Omid = od.ItemId" +
                    //                        " Left Join tblOfferDetails ofd on ofd.Omid = ofm.Omid";
                    //dmnSearchDetails.WhereNdGroupBy = $" where om.status=1 and ko.OrderMasterId={orders.Field1} and ko.OrderNo={orders.Field2}" + " and od.TableType=" + Convert.ToInt32(Tabletype.ComboProduct);
                    //dmnSearchDetails.OrderBy = "order by AddonStatus";

                    dmnSearchDetails.Select = "od.itemName ItemName, od.Quantity Quantity, od.Notes Notes, od.MainRemarks Description, ti.TableCode TableCode, om.OrderMasterId OrderMasterId, od.AddonStatus AddonStatus, od.AV_id AV_id, od.TableType TableType, ofd.Productname offeritemname, ofd.Quantity offerqty, od.PrId PrId, ko.Kitchennumber, od.VariatiionName VariatiionName," +
                        "( select sum(Quantity) from  tblOrderDetails  where status in(1,2) and OrderMasterId=" + orders.Field1 + " and OrderNo=" + orders.Field2 + " and AddonStatus in(0,1)) as TotalQTY,om.pkid Pkid,'' as Station,e.EmployeeName,(" +
                        " 'PAX : ' + CAST(om.TotalGuestCount AS VARCHAR) + " +
                        " ' (A:' + CAST(om.AdultCount AS VARCHAR) + " +
                        " ' C:' + CAST(om.ChildCount AS VARCHAR) + " +
                        " ' O:' + CAST(om.OtherGuestCount AS VARCHAR) + ')'" +
                        ") AS GuestDetails,ko.PriorityType";
                    dmnSearchDetails.Table = "tblkitchenOrder ko " +
                        "Left Join tblOrderDetails od  on od.OrderDetailsId=ko.OrderDetailsId " +
                        "Left Join tblOrderMaster om  on om.OrderMasterId=ko.OrderMasterId " +
                        "Left Join tblTable_info  ti on ti.Tid=ko.Tid" +
                        " Left Join tblOfferMaster ofm  on od.ItemId = ofm.Omid and od.TableType = 3" +
                        " Left Join tblOfferDetails ofd  on ofd.Omid = ofm.Omid" +
                        " Left Join tblEmployee e  on e.EmployeeId = om.CreatedBy";
                    dmnSearchDetails.WhereNdGroupBy = "where om.status=1 and ko.OrderMasterId=" + orders.Field1 + " and ko.OrderNo =" + orders.Field2+" and ko.PreparationMode = "+ Convert.ToInt32(PreparationType.kitchen);
                    dmnSearchDetails.OrderBy = "order by od.AddonStatus ";

                    var BillDetails = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetails);

                    if (BillDetails == null || BillDetails.Count == 0)
                    {
                        continue;
                    }

                    var processedData = BillDetails.Select(item => new
                    {
                        ItemName = !string.IsNullOrEmpty(item.ItemName)
                                   ? item.ItemName + (!string.IsNullOrEmpty(item.VariatiionName) ? " (" + item.VariatiionName + ")" : string.Empty)
                                   : item.ItemName,
                        Quantity = item.Quantity,
                        Notes = item.Notes,
                        Description = item.Description,
                        TableCode = item.TableCode,
                        OrderMasterId = item.OrderMasterId,
                        TableType = item.TableType,
                        AddonStatus = item.AddonStatus,
                        offeritemname = item.offeritemname,
                        offerqty = item.offerqty,
                        Kitchennumber = item.Kitchennumber,
                        MainCategory = string.IsNullOrEmpty(item.AV_id) ? $"{item.ItemName}{item.OrderMasterId}{item.PrId}" : item.AV_id,
                        MainCategoryCount = BillDetails.Count(b => b.MainCategory == item.MainCategory),
                        TotalQTY = item.TotalQTY,
                        Pkid = item.Pkid,
                        EmployeeName = item.EmployeeName,
                        Station = item.Station,
                        GuestDetails = item.GuestDetails,
                        Orderno= orders.Field2,
                        PriorityType= item.PriorityType,
                    }).ToList();

                    //if (item.VariatiionNam != null && item.VariatiionNam != "")
                    //    Description = item.Description + "(" + item.VariatiionNam + ")";
                    //else
                    //    Description = itemname;

                    var mainCategoryCounts = processedData
                        .GroupBy(item => item.MainCategory)
                        .Select(group => new
                        {
                            MainCategory = group.Key,
                            MainCategoryCount = group.Count()
                        }).ToList();


                    var mergedData = processedData
                        .Join(mainCategoryCounts,
                              processedItem => processedItem.MainCategory,
                              mainCategoryCount => mainCategoryCount.MainCategory,
                              (processedItem, mainCategoryCount) => new
                              {
                                  MainCategoryCount = mainCategoryCount.MainCategoryCount,
                                  processedItem.MainCategory,
                                  processedItem.TableType,
                                  processedItem.AddonStatus,
                                  processedItem.ItemName,
                                  processedItem.Quantity,
                                  processedItem.Notes,
                                  processedItem.Description,
                                  processedItem.TableCode,
                                  processedItem.OrderMasterId,
                                  processedItem.offeritemname,
                                  processedItem.offerqty,
                                  processedItem.Kitchennumber,
                                  processedItem.TotalQTY,
                                  processedItem.Pkid,
                                  processedItem.EmployeeName,
                                  processedItem.Station,
                                  processedItem.GuestDetails,
                                  processedItem.Orderno,
                                  processedItem.PriorityType,

                              })
                        .ToList();

                    if (mergedData != null && mergedData.Count > 0)
                    {
                        using (var reader = ObjectReader.Create(mergedData, "ItemName", "Quantity", "Notes", "Description", "TableCode", "OrderMasterId", "MainCategory", "TableType", "AddonStatus", "MainCategoryCount", "offeritemname", "offerqty", "Kitchennumber", "TotalQTY", "Pkid", "EmployeeName", "Station", "GuestDetails", "Orderno", "PriorityType"))
                        {
                            dtSalesDetail.Load(reader);
                        }
                        dsReport.Tables.Add(dtSalesDetail);
                    }

                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            string reportPath = "";
                            //MessageBox.Show(RPTPath + "CRkitchenbill_Pickup.rpt");
                            //textBox2.Text = RPTPath + "CRkitchenbill_Pickup.rpt";
                            if (Convert.ToInt32(BillDetails[0].Pkid) > 0)
                                reportPath = RPTPath + "CRkitchenbill_Pickup.rpt";
                            else
                                reportPath = RPTPath + "crkitchenbill.rpt";

                            if (!File.Exists(reportPath))
                            {
                                throw new FileNotFoundException("Report file not found.", reportPath);
                            }
                            reportDocument.Load(reportPath);

                            reportDocument.SetDataSource(dsReport);
                            crvkitchenbill.ReportSource = reportDocument;
                            reportDocument.PrintToPrinter(1, false, 0, 0);
                        }));
                    }
                    #endregion

                    #region Update Print Status to 2
                    BllKitchenorder bllkitchen = new BllKitchenorder();
                    Dmnkitchen dmnkitchen = new Dmnkitchen
                    {
                        OrderMasterId = Convert.ToInt32(orders.Field1),
                        OrderNo = Convert.ToInt32(orders.Field2),
                        CreatedBy = FrmDashboard.UserId,
                        KOTPrintStatus = 2,
                        BranchId = FrmDashboard.BranchId
                    };
                    var InsertStatus = bllkitchen.Kitchen_statusupdate("prd_updateKitchenorderstatus", dmnkitchen);
                    #endregion

                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            PlaySound();
                            int c = Convert.ToInt32(btnprintcount.Text) - 1;
                            if (c < 0)
                                btnprintcount.Text = "0";
                            else
                                btnprintcount.Text = Convert.ToString(c);
                        }));
                    }

                }

                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                        refreshtimer.Start();
                    }));
                }
            }
            catch (Exception ex)
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                        lblprintloading.Text = "Printing Stopped";
                        clsgen.LogException("Kitchen_View Page PrintOrderAsync()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
                    }));
                }
            }
            finally
            {
                this.Invoke(new Action(() =>
                {
                    lblprintloading.Visible = false;
                }));
                isOrderPrintingRunning = false;
                reportDocument.Close();
                reportDocument.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();

            }
        }

        private void PrintcancelOrder()
        {
            if (isCancelPrintingRunning) return;
            isCancelPrintingRunning = true;

            ReportDocument reportDocument = null;
            DataSet dsReport = new DataSet();
            DataTable DT_cancel = new DataTable("DT_CancelOrder");

            try
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() => refreshtimer.Stop()));
                }

                // Fetch Cancel Orders
                BllCancelsale<DmnOrderCancel> SearchDetails = new BllCancelsale<DmnOrderCancel>();
                DmnOrderCancel dmncancel = new DmnOrderCancel();
                var gridSourcemaster = SearchDetails.serachreport("prdPrintcancelOrder", dmncancel);

                if (gridSourcemaster == null || gridSourcemaster.Count == 0)
                {
                   
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() => refreshtimer.Start()));
                    }
                    return;
                }
                int omid = gridSourcemaster[0].OrderMasterId;
                int pkidno = gridSourcemaster[0].Pkid;
                using (var reader = ObjectReader.Create(gridSourcemaster,
                    "SalesMasterId",
                    "OrderMasterId",
                    "CancelPrintStatus",
                    "CancelDescription",
                    "TableCode",
                    "Pkid",
                    "Station",
                    "EmployeeName",
                    "Tid",
                    "OrderType"))
                {
                    DT_cancel.Load(reader);
                }

                dsReport.Tables.Add(DT_cancel);
                reportDocument = new ReportDocument();
                string reportPath = RPTPath + "CRCancelPrint.rpt";

                if (!File.Exists(reportPath))
                {
                    CMessageBox.Show(this, "Report file not found: " + reportPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new FileNotFoundException("Report file not found.", reportPath);
                }
                reportDocument.Load(reportPath);
                reportDocument.SetDataSource(dsReport);

                // LOOP AND PRINT ONLY FOR RECORDS WITH CancelPrintStatus = 1
                foreach (var orders in gridSourcemaster)
                {
                    if (orders.CancelPrintStatus == 1)
                    {
                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new Action(() => blinkTimer.Start()));
                        }

                        reportDocument.PrintToPrinter(1, false, 0, 0);
                        BllKitchenorder bllkitchen = new BllKitchenorder();
                        DmnOrderCancel dmncnl = new DmnOrderCancel
                        {
                            Pkid = pkidno,
                            OrderMasterId = omid,
                            SalesMasterId = Convert.ToInt32(orders.SalesMasterId),
                            CreatedBy = FrmDashboard.UserId,
                            BranchId = FrmDashboard.BranchId
                        };

                        bllkitchen.Cancel_statusupdate("prd_updateCancelprintstatus", dmncnl);

                      
                    }
                }

                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                        refreshtimer.Start();
                    }));
                }
            }
            catch (Exception ex)
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                        lblprintloading.Text = "Printing Stopped";
                        CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clsgen.LogException("Kitchen_View Page PrintOrderAsync()", ex,
                            FrmLogin.EmployeName, FrmLogin.Designation,
                            Convert.ToInt32(FormType.Kitchen_View));
                    }));
                }
            }
            finally
            {
                if (reportDocument != null)
                {
                    reportDocument.Close();
                    reportDocument.Dispose();
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
                isCancelPrintingRunning = false;
            }
        }
        private void Printcancelitems()
        {
            if (isCancelPrintingRunning) return;
            isCancelPrintingRunning = true;

            ReportDocument reportDocument = null;
            DataSet dsReport = new DataSet();
            DataTable DT_cancelitems = new DataTable("DT_CancelItems");

            try
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() => refreshtimer.Stop()));
                }

                Bllcancelitemprint<DmnCancelItemPrint> SearchDetails = new Bllcancelitemprint<DmnCancelItemPrint>();
                DmnCancelItemPrint dmncancel = new DmnCancelItemPrint();
                var gridSourcemaster = SearchDetails.serachreport("prdCancelItemPrint", dmncancel);

                if (gridSourcemaster == null || gridSourcemaster.Count == 0)
                {
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() => refreshtimer.Start()));
                    }
                    return;
                }

                using (var reader = ObjectReader.Create(gridSourcemaster,
                    "OrderMasterId",
                    "Tid",
                    "TableCode",
                    "KOTNo",
                    "OrderNo",
                    "Quantity",
                    "RemoveNotes",
                    "CreatedDateTime",
                    "EmployeeName",
                    "itemName"))
                {
                    DT_cancelitems.Load(reader);
                }

                dsReport.Tables.Add(DT_cancelitems);
                reportDocument = new ReportDocument();
                string reportPath = RPTPath + "CRcancelitems.rpt";

                if (!File.Exists(reportPath))
                {
                    CMessageBox.Show(this, "Report file not found: " + reportPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw new FileNotFoundException("Report file not found.", reportPath);
                }

                reportDocument.Load(reportPath);
                reportDocument.SetDataSource(dsReport);

                foreach (var item in gridSourcemaster)
                {
                    if (item.IsItemCancelPrintPending)
                    {
                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new Action(() => blinkTimer.Start()));
                        }

                        reportDocument.PrintToPrinter(1, false, 0, 0);

                        BllKitchenorder bllkitchen = new BllKitchenorder();
                        DmnCancelItemPrint dmncnl = new DmnCancelItemPrint
                        {
                            OrderDetailId = item.OrderDetailId,
                            //CreatedBy = FrmDashboard.UserId,
                            //BranchId = FrmDashboard.BranchId
                        };
                        bllkitchen.Cancelitemprint_statusupdate("prd_updateCancelprintstatus_item", dmncnl);
                    }
                }

                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                        refreshtimer.Start();
                    }));
                }
            }
            catch (Exception ex)
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                        lblprintloading.Text = "Printing Stopped";
                        CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clsgen.LogException("Kitchen_View Page PrintCancelItems()", ex,
                            FrmLogin.EmployeName, FrmLogin.Designation,
                            Convert.ToInt32(FormType.Kitchen_View));
                    }));
                }
            }
            finally
            {
                if (reportDocument != null)
                {
                    reportDocument.Close();
                    reportDocument.Dispose();
                }
                isCancelPrintingRunning = false;
            }
        }


        private void dgv_product_details_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dactionno && e.RowIndex >= 0)// Remove Prodcut one by one by clciking column 
                {
                    refreshtimer.Stop();
                    DataGridView clickedDataGridView = sender as DataGridView;
                    if (clickedDataGridView != null && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dstatusno].Value) == 1)//  Food processing Not started
                    {
                        korderno = 0;
                        tlp_main.Enabled = false;
                        txt_product_remove_des.Text = "Reason type here .....";
                        txt_product_remove_des.ForeColor = Color.LightGray;
                        dgv_view_name = clickedDataGridView.Name;
                        dgv_view_rowno = e.RowIndex;
                        pnl_products_rem_descr.Visible = true;
                        int panelWidthPercentage = 25;
                        int panelheightPercentage = 25;
                        int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                        int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                        pnl_products_rem_descr.Size = new Size(panelWidth, panelHeight);
                        pnl_products_rem_descr.Location = new Point(this.ClientSize.Width / 2 - pnl_products_rem_descr.Size.Width / 2, this.ClientSize.Height / 2 - pnl_products_rem_descr.Size.Height / 2);
                        tlp_products_descr_main.Dock = DockStyle.Fill;
                        lblheader_product_des.Text = Convert.ToString(clickedDataGridView.Rows[e.RowIndex].Cells[ditemno].Value);
                        korderno=Convert.ToInt32(clickedDataGridView.Tag);
                        //txt_product_remove_des.Focus();
                    }
                    else if (clickedDataGridView != null && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dstatusno].Value) == 2)//  Food processing started
                    {
                        var result = CMessageBox.Show("Food processing has started. Are you sure you want to cancel this item?", "Confirm The Action", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            tlp_main.Enabled = false;
                            txt_product_remove_des.Text = "Reason type here .....";
                            txt_product_remove_des.ForeColor = Color.LightGray;
                            dgv_view_name = clickedDataGridView.Name;
                            dgv_view_rowno = e.RowIndex;
                            pnl_products_rem_descr.Visible = true;
                            int panelWidthPercentage = 20;
                            int panelheightPercentage = 20;
                            int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                            int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                            pnl_products_rem_descr.Size = new Size(panelWidth, panelHeight);
                            pnl_products_rem_descr.Location = new Point(this.ClientSize.Width / 2 - pnl_products_rem_descr.Size.Width / 2, this.ClientSize.Height / 2 - pnl_products_rem_descr.Size.Height / 2);
                            lblheader_product_des.Text = Convert.ToString(clickedDataGridView.Rows[e.RowIndex].Cells[ditemno].Value);
                        }

                    }
                }
                else if (e.ColumnIndex == dptypeno && e.RowIndex >= 0)//Addon or Combo product View more panel show
                {
                    if (pnlproductviewmore.Visible)
                    {
                        tlp_main.Enabled = true;
                        pnlproductviewmore.Visible = false;
                    }
                    else
                    {

                        DataGridView clickedDataGridView = sender as DataGridView;
                        if (clickedDataGridView != null && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dstatusno].Value) == 1 && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dtabletypeno].Value) == Convert.ToInt32(Tabletype.ComboProduct))//cmbo products
                        {
                            lblviewmore.Text = Convert.ToString(clickedDataGridView.Rows[e.RowIndex].Cells[ditemno].Value);
                            int itemid = Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[ditemidno].Value);
                            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                            BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                            DmnSearch dmnSearch = new DmnSearch();
                            dmnSearch.Select = "ofd.Productname Field1,ofd.Quantity Field2";
                            dmnSearch.Table = " tblOfferDetails ofd LEFT JOIN tblOfferMaster ofm on ofm.Omid=ofd.Omid";
                            dmnSearch.WhereNdGroupBy = "where ofm.Status=1 and ofd.Status=1 and ofm.Omid = " + itemid;
                            dmnSearch.OrderBy = "";
                            var gridSource_offer = bllSearch.GetSearchData("prdSearch", dmnSearch);
                            if (gridSource_offer.Count > 0)
                            {
                                tlp_main.Enabled = false;
                                dgv_viewmore.Rows.Clear();
                                dgv_viewmore.Columns[addactionno].Visible = false;
                                for (int k = 0; k < gridSource_offer.Count; k++)
                                {
                                    dgv_viewmore.Rows.Add();
                                    dgv_viewmore.Rows[k].Cells[addslno].Value = k + 1;
                                    dgv_viewmore.Rows[k].Cells[additemnameno].Value = gridSource_offer[k].Field1;
                                    dgv_viewmore.Rows[k].Cells[addqtyno].Value = gridSource_offer[k].Field2;
                                }
                                // pnlproductviewmore.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                                pnlproductviewmore.Location = new Point(this.ClientSize.Width / 2 - pnlproductviewmore.Size.Width / 2, this.ClientSize.Height / 2 - pnlproductviewmore.Size.Height / 2);
                                pnlproductviewmore.Visible = true;
                            }

                        }
                        else if (clickedDataGridView != null && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dstatusno].Value) == 1 && Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dAddonStatusno].Value) == Convert.ToInt32(AddonType.AddonType))//addon products
                        {
                            lblviewmore.Text = Convert.ToString(clickedDataGridView.Rows[e.RowIndex].Cells[ditemno].Value);
                            string itemid = Convert.ToString(clickedDataGridView.Rows[e.RowIndex].Cells[dAVIDno].Value);
                            int ordermasid = Convert.ToInt32(clickedDataGridView.Rows[e.RowIndex].Cells[dordermasteridno].Value);
                            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                            BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                            DmnSearch dmnSearch = new DmnSearch();
                            dmnSearch.Select = "itemName Field1,Quantity Field2,od.AddonStatus Field3,ko.OrderMasterId Field4,ko.Koid Field5,od.OrderDetailsId Field6,od.AV_id Field7,od.Pavid Field8,pav.Price Field9,od.OrderNo Field10,pav.CCode Field11";
                            dmnSearch.Table = "tblOrderDetails od left join tblkitchenOrder ko on  ko.OrderDetailsId=od.OrderDetailsId " +
                                "left join tblProduct_Addon_Variation pav on  pav.Pavid=od.Pavid and pav.status=1";
                            dmnSearch.WhereNdGroupBy = "where od.Status!=0 and od.OrderMasterId= " + ordermasid + " and  od.AV_id = '" + itemid + "'";
                            dmnSearch.OrderBy = "order by od.Pavid";
                            var gridSource_offer = bllSearch.GetSearchData("prdSearch", dmnSearch);
                            if (gridSource_offer.Count > 1)
                            {
                                tlp_main.Enabled = false;
                                dgv_viewmore.Rows.Clear();
                                dgv_viewmore.Columns[addactionno].Visible = true;
                                int c = 0;
                                for (int k = 0; k < gridSource_offer.Count; k++)
                                {
                                    if (Convert.ToInt32(gridSource_offer[k].Field3) == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                                    {
                                        int rowIndex = dgv_viewmore.Rows.Add();
                                        dgv_viewmore.Rows[rowIndex].Cells[addslno].Value = c + 1;
                                        dgv_viewmore.Rows[rowIndex].Cells[additemnameno].Value = gridSource_offer[k].Field1;
                                        dgv_viewmore.Rows[rowIndex].Cells[addqtyno].Value = gridSource_offer[k].Field2;
                                        dgv_viewmore.Rows[rowIndex].Cells[addordermasteridno].Value = gridSource_offer[k].Field4;
                                        dgv_viewmore.Rows[rowIndex].Cells[addkoidno].Value = gridSource_offer[k].Field5;
                                        dgv_viewmore.Rows[rowIndex].Cells[addorderdetailidno].Value = gridSource_offer[k].Field6;
                                        dgv_viewmore.Rows[rowIndex].Cells[addavidno].Value = gridSource_offer[k].Field7;
                                        dgv_viewmore.Rows[rowIndex].Cells[addpavidno].Value = gridSource_offer[k].Field8;
                                        dgv_viewmore.Rows[rowIndex].Cells[addaddonstatusno].Value = gridSource_offer[k].Field3;
                                        dgv_viewmore.Rows[rowIndex].Cells[addonpriceno].Value = gridSource_offer[k].Field9;
                                        dgv_viewmore.Rows[rowIndex].Cells[addonorderno].Value = gridSource_offer[k].Field10;
                                        dgv_viewmore.Rows[rowIndex].Cells[addccodeno].Value = gridSource_offer[k].Field11;
                                        c = c + 1;
                                    }
                                }
                                // pnlproductviewmore.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                                pnlproductviewmore.Location = new Point(this.ClientSize.Width / 2 - pnlproductviewmore.Size.Width / 2, this.ClientSize.Height / 2 - pnlproductviewmore.Size.Height / 2);
                                pnlproductviewmore.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View dgv_product_details_CellClick", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }

        }
        public void ReprintBill()
        {
            ReportDocument reportDocument = new ReportDocument();
            try
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Start();
                        refreshtimer.Stop();
                    }));
                }

                dgv_view_name = "dgv_product_details_k_" + ReprintOrderNo + ReprintId;
                DataGridView dgv_product_details = this.Controls.Find(dgv_view_name, true).FirstOrDefault() as DataGridView;
                if (dgv_product_details != null)
                {
                    int orderno = 0, ordermid = 0;
                    for (int i = 0; i < dgv_product_details.RowCount; i++)
                    {
                        if (Convert.ToInt32(dgv_product_details.Rows[i].Cells[dstatusno].Value) == 1 || Convert.ToInt32(dgv_product_details.Rows[i].Cells[dstatusno].Value) == 2)
                        {
                            orderno = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dorderno].Value);
                            ordermid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dordermasteridno].Value);
                            break;
                        }
                    }
                    #region Crystal Report Dclaration
                    DataSet dsReport = new DataSet();
                    DataTable dtSalesDetail = new DataTable("DT_kitchenbill");
                    BllSearch<DmnkitchenBilldetails> bllSearchDetails = new BllSearch<DmnkitchenBilldetails>();
                    DmnSearch dmnSearchDetails = new DmnSearch();
                    dmnSearchDetails.Select = "od.itemName ItemName, od.Quantity Quantity, od.Notes Notes, od.MainRemarks Description, ti.TableCode TableCode, om.OrderMasterId OrderMasterId, od.AddonStatus AddonStatus, od.AV_id AV_id, od.TableType TableType,'' offeritemname, 0 offerqty, od.PrId PrId, ko.Kitchennumber,od.VariatiionName VariatiionName,'' as Station,e.EmployeeName," +
                        "( select sum(Quantity) from  tblOrderDetails  where status in(1,2) and OrderMasterId=" + ordermid + " and OrderNo=" + orderno + " and AddonStatus in(0,1)) as TotalQTY,om.pkid Pkid,'' as Station,e.EmployeeName,(" +
                        " 'PAX : ' + CAST(om.TotalGuestCount AS VARCHAR) + " +
                        " ' (A:' + CAST(om.AdultCount AS VARCHAR) + " +
                        " ' C:' + CAST(om.ChildCount AS VARCHAR) + " +
                        " ' O:' + CAST(om.OtherGuestCount AS VARCHAR) + ')'" +
                        ") AS GuestDetails,ko.PriorityType " +

                    " from tblkitchenOrder ko " +
                        " Left Join tblOrderDetails od on od.OrderDetailsId=ko.OrderDetailsId " +
                        " Left Join tblOrderMaster om on om.OrderMasterId=ko.OrderMasterId " +
                        " Left Join tblTable_info ti on ti.Tid=ko.Tid " +
                        " Left Join tblEmployee e  on e.EmployeeId = om.CreatedBy " +
                    " where om.status=1 and ko.OrderMasterId=" + ordermid + " and ko.OrderNo=" + orderno + " and od.TableType!=" + Convert.ToInt32(Tabletype.ComboProduct) + " and ko.status in(1,2)" +
                    " union select od.itemName ItemName, od.Quantity Quantity, od.Notes Notes, od.MainRemarks Description, ti.TableCode TableCode, om.OrderMasterId OrderMasterId, od.AddonStatus AddonStatus, od.AV_id AV_id, od.TableType TableType, ofd.Productname offeritemname, ofd.Quantity offerqty, od.PrId PrId, ko.Kitchennumber, od.VariatiionName VariatiionName,'' as Station,e.EmployeeName," +
                         "( select sum(Quantity) from  tblOrderDetails  where status in(1,2) and OrderMasterId=" + ordermid + " and OrderNo=" + orderno + " and AddonStatus in(0,1)) as TotalQTY,om.pkid Pkid,'' as Station,e.EmployeeName,(" +
                        " 'PAX : ' + CAST(om.TotalGuestCount AS VARCHAR) + " +
                        " ' (A:' + CAST(om.AdultCount AS VARCHAR) + " +
                        " ' C:' + CAST(om.ChildCount AS VARCHAR) + " +
                        " ' O:' + CAST(om.OtherGuestCount AS VARCHAR) + ')'" +
                        ") AS GuestDetails,ko.PriorityType ";
                    dmnSearchDetails.Table = " tblkitchenOrder ko" +
                                            " Left Join tblOrderDetails od on od.OrderDetailsId = ko.OrderDetailsId" +
                                            " Left Join tblOrderMaster om on om.OrderMasterId = ko.OrderMasterId" +
                                            " Left Join tblTable_info ti on ti.Tid = ko.Tid" +
                                            " Left Join tblOfferMaster ofm on ofm.Omid = od.ItemId" +
                                            " Left Join tblOfferDetails ofd on ofd.Omid = ofm.Omid"+
                                            " Left Join tblEmployee e  on e.EmployeeId = om.CreatedBy";
                    dmnSearchDetails.WhereNdGroupBy = $" where om.status=1 and ko.OrderMasterId=" + ordermid + " and ko.OrderNo=" + orderno + " and od.TableType=" + Convert.ToInt32(Tabletype.ComboProduct) + " and ko.status in(1,2) and ko.PreparationMode = "+ Convert.ToInt32(PreparationType.kitchen);
                    dmnSearchDetails.OrderBy = "order by AddonStatus";
                    var BillDetails = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetails);
                    if (BillDetails.Count > 0)
                    {
                        var processedData = BillDetails.Select(item => new
                        {
                            ItemName = !string.IsNullOrEmpty(item.ItemName)
                                  ? item.ItemName + (!string.IsNullOrEmpty(item.VariatiionName) ? " (" + item.VariatiionName + ")" : string.Empty)
                                  : item.ItemName,
                            Quantity = item.Quantity,
                            Notes = item.Notes,
                            Description = item.Description,
                            TableCode = item.TableCode,
                            OrderMasterId = item.OrderMasterId,
                            TableType = item.TableType,
                            AddonStatus = item.AddonStatus,
                            offeritemname = item.offeritemname,
                            offerqty = item.offerqty,
                            Kitchennumber = item.Kitchennumber,
                            MainCategory = string.IsNullOrEmpty(item.AV_id) ? $"{item.ItemName}{item.OrderMasterId}{item.PrId}" : item.AV_id,
                            MainCategoryCount = BillDetails.Count(b => b.MainCategory == item.MainCategory),
                            TotalQTY = item.TotalQTY,
                            Pkid = item.Pkid,
                            EmployeeName = item.EmployeeName,
                            Station = item.Station,
                            GuestDetails = item.GuestDetails,
                            Orderno = orderno,
                            PriorityType = item.PriorityType,
                        }).ToList();

                        //if (item.VariatiionNam != null && item.VariatiionNam != "")
                        //    Description = item.Description + "(" + item.VariatiionNam + ")";
                        //else
                        //    Description = itemname;

                        var mainCategoryCounts = processedData
                            .GroupBy(item => item.MainCategory)
                            .Select(group => new
                            {
                                MainCategory = group.Key,
                                MainCategoryCount = group.Count()
                            }).ToList();


                        var mergedData = processedData
                            .Join(mainCategoryCounts,
                                  processedItem => processedItem.MainCategory,
                                  mainCategoryCount => mainCategoryCount.MainCategory,
                                  (processedItem, mainCategoryCount) => new
                                  {
                                      MainCategoryCount = mainCategoryCount.MainCategoryCount,
                                      processedItem.MainCategory,
                                      processedItem.TableType,
                                      processedItem.AddonStatus,
                                      processedItem.ItemName,
                                      processedItem.Quantity,
                                      processedItem.Notes,
                                      processedItem.Description,
                                      processedItem.TableCode,
                                      processedItem.OrderMasterId,
                                      processedItem.offeritemname,
                                      processedItem.offerqty,
                                      processedItem.Kitchennumber,
                                      processedItem.TotalQTY,
                                      processedItem.Pkid,
                                      processedItem.EmployeeName,
                                      processedItem.Station,
                                      processedItem.GuestDetails,
                                      processedItem.Orderno,
                                      processedItem.PriorityType,

                                  })
                            .ToList();

                        if (mergedData != null && mergedData.Count > 0)
                        {
                            using (var reader = ObjectReader.Create(mergedData, "ItemName", "Quantity", "Notes", "Description", "TableCode", "OrderMasterId", "MainCategory", "TableType", "AddonStatus", "MainCategoryCount", "offeritemname", "offerqty", "Kitchennumber", "TotalQTY", "Pkid", "EmployeeName", "Station", "GuestDetails", "Orderno", "PriorityType"))
                            {
                                dtSalesDetail.Load(reader);
                            }
                            dsReport.Tables.Add(dtSalesDetail);
                        }

                        //MessageBox.Show(RPTPath + "crkitchenbill.rpt.rpt" + 2);
                        //textBox2.Text = RPTPath + "CRkitchenbill_Pickup.rpt";
                        string reportPath = "";
                        if (Convert.ToInt32(BillDetails[0].Pkid) > 0)
                            reportPath = RPTPath + "CRkitchenbill_Pickup.rpt";
                        else
                            reportPath = RPTPath + "crkitchenbill.rpt";

                        if (!File.Exists(reportPath))
                        {
                            throw new FileNotFoundException("Report file not found.", reportPath);
                        }
                        reportDocument.Load(reportPath);

                        reportDocument.SetDataSource(dsReport);

                        if (this.IsHandleCreated)
                        {
                            this.Invoke(new Action(() =>
                            {
                                crvkitchenbill.ReportSource = reportDocument;
                            }));
                        }
                        //reportDocument.SetParameterValue("TotalQtyMain", mainCategoryCounts.Count);
                        reportDocument.PrintToPrinter(1, false, 0, 0);
                    }
                    if (this.IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            refreshtimer.Start();
                        }));
                    }

                }
                #endregion

                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                    }));
                }
            }
            catch (Exception ex)
            {
                if (this.IsHandleCreated)
                {
                    this.Invoke(new Action(() =>
                    {
                        blinkTimer.Stop();
                        lblprintloading.Text = "Reprinting Stopped";

                        //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        clsgen.LogException("Kitchen_View ReprintBill", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
                    }));
                }

            }
            finally
            {
                reportDocument.Close();
                reportDocument.Dispose();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
        private void reprintbutton_Click(object sender, EventArgs e)
        {
            ReprintOrderNo = 0;
            ReprintId = 0;
            PictureBox clickedPictureBox = (PictureBox)sender;
            ReprintOrderNo = Convert.ToInt32(clickedPictureBox.AccessibleDescription);
            ReprintId = Convert.ToInt32(clickedPictureBox.Tag);
            RunMethodInSeparateThread(ReprintBill);

        }
        private void pbcancelfood_Click(object sender, EventArgs e)
        {
            try
            {
                tlp_main.Enabled = false;
                refreshtimer.Stop();
                PictureBox clickedPictureBox = (PictureBox)sender;
                int id = Convert.ToInt32(clickedPictureBox.Tag);
                dgv_view_name = "dgv_product_details_k_" + clickedPictureBox.AccessibleDescription + id;
                pnl_products_rem_descr.Visible = true;
                int panelWidthPercentage = 20;
                int panelheightPercentage = 20;
                int panelWidth = (int)(this.Width * panelWidthPercentage / 100);
                int panelHeight = (int)(this.Height * panelheightPercentage / 100);
                pnl_products_rem_descr.Size = new Size(panelWidth, panelHeight);
                pnl_products_rem_descr.Location = new Point(this.ClientSize.Width / 2 - pnl_products_rem_descr.Size.Width / 2, this.ClientSize.Height / 2 - pnl_products_rem_descr.Size.Height / 2);
                pbcancelfoodclciked = true;
                lblheader_product_des.Text = "Order Cancel";
                //txt_product_remove_des.Focus();
                pnl_products_rem_descr.Focus();

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View pbcancelfood_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }

        }
        private void pbstart_Click(object sender, EventArgs e)
        {
            try
            {
                refreshtimer.Stop();
                PictureBox clickedPictureBox = (PictureBox)sender;
                int id = Convert.ToInt32(clickedPictureBox.Tag);
                string dgvname = "dgv_product_details_k_" + clickedPictureBox.AccessibleDescription + id;
                string startbuttonname = "pb_start_k_" + clickedPictureBox.AccessibleDescription + id;
                string readybuttonname = "pb_ready_k_" + clickedPictureBox.AccessibleDescription + id;
                string tlpname = "tlp_product_box_k_" + clickedPictureBox.AccessibleDescription + id;
                string cancelfoodbuttonname = "pictureBoxRemove_k_" + clickedPictureBox.AccessibleDescription + id;
                DmnOrderMaster dmnRemove_order = new DmnOrderMaster();
                BllOrder bll_order = new BllOrder();
                PictureBox btn_ready = this.Controls.Find(readybuttonname, true).FirstOrDefault() as PictureBox;
                if (btn_ready != null)
                    btn_ready.Visible = true;
                PictureBox btn_start = this.Controls.Find(startbuttonname, true).FirstOrDefault() as PictureBox;
                if (btn_start != null)
                {
                    btn_start.Visible = false;
                    btn_start.Dispose();
                }
                PictureBox btn_cancelfood = this.Controls.Find(cancelfoodbuttonname, true).FirstOrDefault() as PictureBox;

                DataGridView dgv_product_details = this.Controls.Find(dgvname, true).FirstOrDefault() as DataGridView;
                if (dgv_product_details != null)
                {
                    int tid = 0, orderno = 0, ordermid = 0;

                    ordermid = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dordermasteridno].Value);
                    dmnRemove_order.OrderDetailsId = Convert.ToInt32(dgv_product_details.Rows[0].Cells[ddetailsidno].Value);
                    dmnRemove_order.koid = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dkoidno].Value);
                    dmnRemove_order.OrderMasterId = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dordermasteridno].Value);
                    dmnRemove_order.AddonStatus = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dAddonStatusno].Value);
                    dmnRemove_order.AV_id = Convert.ToString(dgv_product_details.Rows[0].Cells[dAVIDno].Value);
                    dmnRemove_order.OrderNo = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dorderno].Value);
                    //dmnRemove_order.Tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                    //decimal originalValue = Convert.ToDecimal(dgv_product_details.Rows[i].Cells[dqtyno].Value);
                    //dmnRemove_order.Quantity = (int)Math.Floor(originalValue);  
                    //tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                    //dmnRemove_order.RemoveNotes = "";
                    dmnRemove_order.CreatedBy = FrmDashboard.UserId;
                    dmnRemove_order.BranchId = FrmDashboard.BranchId;
                    dmnRemove_order.Status = 2;
                    var InsertStatusP = bll_order.Remove_order_KitchenOrder_Start_Rady("prd_KitchenOrder_Updates", dmnRemove_order);
                    if (InsertStatusP.Status == 1)
                    {
                        TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                        if (tlp_sele != null)
                        {
                            bool flagstart_ready = false;

                            #region Grid Edit Cancel  product
                            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                            DmnSearch dmnSearch = new DmnSearch();
                            dmnSearch.Select = "Status Field1,Koid Field2";
                            dmnSearch.Table = " tblkitchenOrder";
                            dmnSearch.WhereNdGroupBy = "where  AddonStatus in(1,0)  and  OrderNo = " + id + " and OrderMasterId = " + ordermid;
                            dmnSearch.OrderBy = "";
                            var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

                            for (int k = 0; k < gridSource.Count; k++)
                            {
                                if (Convert.ToInt32(gridSource[k].Field1) == 1)
                                {
                                    flagstart_ready = true;
                                }

                                dgv_product_details.Rows[k].Cells[dactionno].Value = Properties.Resources.close_price_change;

                                dgv_product_details.Rows[k].Cells[dstatusno].Value = gridSource[k].Field1;


                                for (int j = 0; j < dgv_product_details.RowCount; j++)
                                {
                                    DataGridViewImageCell cell = (DataGridViewImageCell)dgv_product_details.Rows[j].Cells[dactionno];
                                    if (Convert.ToInt32(dgv_product_details.Rows[j].Cells[dkoidno].Value) == Convert.ToInt32(gridSource[k].Field2))
                                    {
                                        if (Convert.ToInt32(gridSource[k].Field1) == 0)
                                        {
                                            cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                            cell.Value = Properties.Resources.orderview_cancelled;
                                            dgv_product_details.Rows[j].ReadOnly = true;
                                        }
                                        else
                                        {
                                            cell.Value = Properties.Resources.close_price_change;
                                            cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                        }
                                    }

                                }
                            }
                            #endregion

                            #region Edit Ready and Close Button 
                            if (flagstart_ready)
                            {
                                btn_start.Visible = true;
                                btn_cancelfood.Visible = true;
                                btn_ready.Visible = false;
                            }
                            else
                            {
                                btn_start.Visible = false;
                                btn_ready.Visible = true;
                                btn_cancelfood.Visible = false;
                            }
                            #endregion

                            refreshtimer.Start();
                        }
                        if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                        {
                            FrmOrderView _instanceFrmOrderView = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                            _instanceFrmOrderView.FillOrders();

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View pbstart_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
        private void pbreadyfood_Click(object sender, EventArgs e)
        {
            try
            {
                refreshtimer.Stop();
                PictureBox clickedPictureBox = (PictureBox)sender;
                int id = Convert.ToInt32(clickedPictureBox.Tag);
                string dgvname = "dgv_product_details_k_" + clickedPictureBox.AccessibleDescription + id;
                DmnOrderMaster dmnRemove_order = new DmnOrderMaster();
                BllOrder bll_order = new BllOrder();

                DataGridView dgv_product_details = this.Controls.Find(dgvname, true).FirstOrDefault() as DataGridView;
                if (dgv_product_details != null)
                {
                    int tid = 0, koid = 0;

                    //tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                    //dmnRemove_order.Tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                    //decimal originalValue = Convert.ToDecimal(dgv_product_details.Rows[i].Cells[dqtyno].Value);  
                    //dmnRemove_order.Quantity = (int)Math.Floor(originalValue); 
                    //dmnRemove_order.RemoveNotes = "";
                    dmnRemove_order.AddonStatus = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dAddonStatusno].Value);
                    dmnRemove_order.AV_id = Convert.ToString(dgv_product_details.Rows[0].Cells[dAVIDno].Value);
                    dmnRemove_order.OrderDetailsId = Convert.ToInt32(dgv_product_details.Rows[0].Cells[ddetailsidno].Value);
                    dmnRemove_order.koid = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dkoidno].Value);
                    dmnRemove_order.OrderMasterId = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dordermasteridno].Value);
                    dmnRemove_order.OrderNo = Convert.ToInt32(dgv_product_details.Rows[0].Cells[dorderno].Value);
                    dmnRemove_order.CreatedBy = FrmDashboard.UserId;
                    dmnRemove_order.BranchId = FrmDashboard.BranchId;
                    dmnRemove_order.Status = 3;
                    var InsertStatusP = bll_order.Remove_order_KitchenOrder_Start_Rady("prd_KitchenOrder_Updates", dmnRemove_order);

                    if (InsertStatusP.Status == 1)
                    {
                        string dgvName = "dgv_product_details_k_" + clickedPictureBox.AccessibleDescription + id;
                        string startButtonName = "pb_start_k_" + clickedPictureBox.AccessibleDescription + id;
                        string readyButtonName = "pb_ready_k_" + clickedPictureBox.AccessibleDescription + id;
                        string cancelFoodButtonName = "pictureBoxRemove_k_" + clickedPictureBox.AccessibleDescription + id;
                        string lblTimeName = "lbl_time_K_" + clickedPictureBox.AccessibleDescription + id;
                        string lblTableCodeName = "lbl_tablecod_k_" + clickedPictureBox.AccessibleDescription + id;

                        Label lblTime = this.Controls.Find(lblTimeName, true).FirstOrDefault() as Label;
                        Label lblTableCode = this.Controls.Find(lblTableCodeName, true).FirstOrDefault() as Label;
                        PictureBox btnReady = this.Controls.Find(readyButtonName, true).FirstOrDefault() as PictureBox;
                        PictureBox btnStart = this.Controls.Find(startButtonName, true).FirstOrDefault() as PictureBox;
                        PictureBox btnCancelFood = this.Controls.Find(cancelFoodButtonName, true).FirstOrDefault() as PictureBox;
                        if (btnReady != null && btnStart != null && btnCancelFood != null && lblTime != null && lblTableCode != null)
                        {
                            btnReady.Dispose();
                            btnStart.Dispose();
                            btnCancelFood.Dispose();
                            lblTime?.Dispose();
                            lblTableCode?.Dispose();
                        }


                        string tlpname = "tlp_product_box_k_" + clickedPictureBox.AccessibleDescription + id;
                        TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                        if (tlp_sele != null && flp_order_view.Controls.Contains(tlp_sele))
                        {
                            flp_order_view.Controls.Remove(tlp_sele);
                            tlp_sele.Dispose();
                            refreshtimer.Start();
                        }

                        if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                        {
                            FrmOrderView _instanceFrmOrderView = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                            _instanceFrmOrderView.FillOrders();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View pbreadyfood_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }

        }
        private void txt_product_remove_des_Enter(object sender, EventArgs e)
        {
            if (txt_product_remove_des.Text == "Reason type here .....")
            {
                txt_product_remove_des.Text = "";
            }
            txt_product_remove_des.ForeColor = Color.Black;
        }
        private void txt_product_remove_des_Leave(object sender, EventArgs e)
        {
            if (txt_product_remove_des.Text == "")
            {
                txt_product_remove_des.Text = "Reason type here .....";

            }
            txt_product_remove_des.ForeColor = Color.DarkGray;
        }
        private void btn_cancel_product_des_Click(object sender, EventArgs e)
        {
            tlp_main.Enabled = true;
            pnl_products_rem_descr.Visible = false;
            txt_product_remove_des.Text = "Reason type here .....";
            refreshtimer.Start();
        }
        private void btn_close_prod_des_Click(object sender, EventArgs e)
        {
            tlp_main.Enabled = true;
            pnl_products_rem_descr.Visible = false;
            txt_product_remove_des.Text = "Reason type here .....";
            refreshtimer.Start();
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
                    int tid = 0,addonrowindex=0;
                    if (pbcancelfoodclciked)
                    {
                        DataGridView dgv_product_details = this.Controls.Find(dgv_view_name, true).FirstOrDefault() as DataGridView;
                        if (dgv_product_details != null)
                        {
                            int omid = 0, orderno = 0;
                            for (int i = 0; i < dgv_product_details.RowCount; i++)
                            {
                                if (Convert.ToInt32(dgv_product_details.Rows[i].Cells[dstatusno].Value) == 1)
                                {
                                    omid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dordermasteridno].Value);
                                    tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                                    orderno = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dorderno].Value);
                                    dmnRemove_order.OrderDetailsId = Convert.ToInt32(dgv_product_details.Rows[i].Cells[ddetailsidno].Value);
                                    dmnRemove_order.koid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dkoidno].Value);
                                    dmnRemove_order.OrderMasterId = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dordermasteridno].Value);
                                    dmnRemove_order.Tid = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dtidno].Value);
                                    decimal originalValue = Convert.ToDecimal(dgv_product_details.Rows[i].Cells[dqtyno].Value);

                                    dmnRemove_order.AddonStatus = Convert.ToInt32(dgv_product_details.Rows[i].Cells[dAddonStatusno].Value);
                                    dmnRemove_order.AV_id = Convert.ToString(dgv_product_details.Rows[i].Cells[dAVIDno].Value);

                                    dmnRemove_order.Quantity = (int)Math.Floor(originalValue);
                                    dmnRemove_order.Status = 0;
                                    dmnRemove_order.CancelNotifyStatus = 1;
                                    dmnRemove_order.OrderCancelNotes = txt_product_remove_des.Text;
                                    dmnRemove_order.Sourcepage = Convert.ToInt32(CancelSourcePage.Kitchen);
                                    var InsertStatusP = bll_order.Remove_order_ALL("prd_remove_KitchenOrder", dmnRemove_order);
                                }
                            }

                            tlp_main.Enabled = true;
                            pbcancelfoodclciked = false;
                            pnl_products_rem_descr.Visible = false;
                            txt_product_remove_des.Text = "";

                            string tlpname = "tlp_product_box_k_" + omid + orderno;
                            TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;

                            string dgvName = "dgv_product_details_k_" + omid + orderno;
                            string startButtonName = "pb_start_k_" + omid + orderno;
                            string readyButtonName = "pb_ready_k_" + omid + orderno;
                            string cancelFoodButtonName = "pictureBoxRemove_k_" + omid + orderno;
                            string lblTimeName = "lbl_time_K_" + omid + orderno;
                            string lblTableCodeName = "lbl_tablecod_k_" + omid + orderno;

                            Label lblTime = this.Controls.Find(lblTimeName, true).FirstOrDefault() as Label;
                            Label lblTableCode = this.Controls.Find(lblTableCodeName, true).FirstOrDefault() as Label;
                            PictureBox btnReady = this.Controls.Find(readyButtonName, true).FirstOrDefault() as PictureBox;
                            PictureBox btnStart = this.Controls.Find(startButtonName, true).FirstOrDefault() as PictureBox;
                            PictureBox btnCancelFood = this.Controls.Find(cancelFoodButtonName, true).FirstOrDefault() as PictureBox;


                            if (btnReady != null && btnStart != null && btnCancelFood != null && lblTime != null && lblTableCode != null)
                            {
                                btnReady.Dispose();
                                btnStart.Dispose();
                                btnCancelFood.Dispose();
                                lblTime?.Dispose();
                                lblTableCode?.Dispose();
                            }

                            if (tlp_sele != null && flp_order_view.Controls.Contains(tlp_sele))
                            {
                                flp_order_view.Controls.Remove(tlp_sele);
                                tlp_sele.Dispose();
                                dgv_product_details.Dispose();
                            }
                        }
                        refreshtimer.Start();

                    }
                    else
                    {
                        DataGridView dgv_product_details = this.Controls.Find(dgv_view_name, true).FirstOrDefault() as DataGridView;
                        if (Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dAddonStatusno].Value) == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                        {
                            #region Load data to Grid
                            string itemid = Convert.ToString(dgv_product_details.Rows[dgv_view_rowno].Cells[dAVIDno].Value);
                            int ordermasid = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dordermasteridno].Value);
                            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                            BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                            DmnSearch dmnSearch = new DmnSearch();
                            dmnSearch.Select = "itemName Field1,Quantity Field2,od.AddonStatus Field3,ko.OrderMasterId Field4,ko.Koid Field5,od.OrderDetailsId Field6,od.AV_id Field7,od.Pavid Field8,pav.Price Field9,od.OrderNo Field10,pav.CCode Field11";
                            dmnSearch.Table = "tblOrderDetails od left join tblkitchenOrder ko on  ko.OrderDetailsId=od.OrderDetailsId " +
                                "left join tblProduct_Addon_Variation pav on  pav.Pavid=od.Pavid and pav.status=1";
                            dmnSearch.WhereNdGroupBy = "where od.Status!=0 and od.OrderMasterId= " + ordermasid + " and  od.AV_id = '" + itemid + "'";
                            dmnSearch.OrderBy = "order by od.Pavid";
                            var gridSource_offer = bllSearch.GetSearchData("prdSearch", dmnSearch);
                            if (gridSource_offer.Count > 1)
                            {
                                dgv_viewmore.Rows.Clear();
                                dgv_viewmore.Columns[addactionno].Visible = true;
                                int c = 0;
                                for (int k = 0; k < gridSource_offer.Count; k++)
                                {
                                    if (Convert.ToInt32(gridSource_offer[k].Field3) == Convert.ToInt32(Product_Addon_Variation_Type.Addon))
                                    {
                                        int rowIndex = dgv_viewmore.Rows.Add();
                                        dgv_viewmore.Rows[rowIndex].Cells[addslno].Value = c + 1;
                                        dgv_viewmore.Rows[rowIndex].Cells[additemnameno].Value = gridSource_offer[k].Field1;
                                        dgv_viewmore.Rows[rowIndex].Cells[addqtyno].Value = gridSource_offer[k].Field2;
                                        dgv_viewmore.Rows[rowIndex].Cells[addordermasteridno].Value = gridSource_offer[k].Field4;
                                        dgv_viewmore.Rows[rowIndex].Cells[addkoidno].Value = gridSource_offer[k].Field5;
                                        dgv_viewmore.Rows[rowIndex].Cells[addorderdetailidno].Value = gridSource_offer[k].Field6;
                                        dgv_viewmore.Rows[rowIndex].Cells[addavidno].Value = gridSource_offer[k].Field7;
                                        dgv_viewmore.Rows[rowIndex].Cells[addpavidno].Value = gridSource_offer[k].Field8;
                                        dgv_viewmore.Rows[rowIndex].Cells[addaddonstatusno].Value = gridSource_offer[k].Field3;
                                        dgv_viewmore.Rows[rowIndex].Cells[addonpriceno].Value = gridSource_offer[k].Field9;
                                        dgv_viewmore.Rows[rowIndex].Cells[addonorderno].Value = gridSource_offer[k].Field10;
                                        dgv_viewmore.Rows[rowIndex].Cells[addccodeno].Value = gridSource_offer[k].Field11;
                                        c = c + 1;
                                    }
                                }
                                // pnlproductviewmore.Location = new Point(Cursor.Position.X, Cursor.Position.Y);
                                pnlproductviewmore.Location = new Point(this.ClientSize.Width / 2 - pnlproductviewmore.Size.Width / 2, this.ClientSize.Height / 2 - pnlproductviewmore.Size.Height / 2);
                                pnlproductviewmore.Visible = true;
                            }
                            #endregion

                            #region Remove Addon
                            DmnKitchenaddonremove dmnRemove_addon = new DmnKitchenaddonremove();
                            BllKitchenorder bll_rem_addon = new BllKitchenorder();
                            decimal newprice = 0, discountprice = 0, TaxAmount = 0, finalprice = 0, disAmnt = 0;
                            string newpart = "";
                            //var result = CMessageBox.Show("Are you sure you want to cancel this Addon?", "Confirm The Action", MessageBoxButtons.YesNo);
                            //if (result == DialogResult.Yes)
                            //{
                                refreshtimer.Stop();

                            #region To Reset Addon ID(remove id form avid name)
                            for (int i = 0; i < dgv_viewmore.RowCount; i++)
                            {
                                if (Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addaddonstatusno].Value) == 2 && Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addkoidno].Value) == Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dkoidno].Value))
                                {
                                    addonrowindex = i;
                                    break;
                                }
                            }

                                string avidValue = Convert.ToString(dgv_product_details.Rows[dgv_view_rowno].Cells[dAVIDno].Value);
                                string[] avidParts = avidValue.Split('_');
                                string extractedpid = $"{avidParts[0]}";
                                newpart = newpart + extractedpid + "_";
                                int remove_koid = Convert.ToInt32(dgv_viewmore.Rows[addonrowindex].Cells[addkoidno].Value);
                                for (int i = 0; i < dgv_viewmore.RowCount; i++)
                                {
                                    if (Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addaddonstatusno].Value) == 2 && Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addkoidno].Value) != remove_koid)
                                    {
                                        string ccode = Convert.ToString(dgv_viewmore.Rows[i].Cells[addccodeno].Value);
                                        decimal quantityDecimal = Convert.ToDecimal(dgv_viewmore.Rows[i].Cells[addqtyno].Value);
                                        int quantityInt = (int)Math.Floor(quantityDecimal);
                                        newpart = newpart + ccode + quantityInt.ToString();
                                    }
                                }

                                for (int i = 0; i < dgv_viewmore.RowCount; i++)
                                {
                                    if (Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addaddonstatusno].Value) == 2 && Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addkoidno].Value) != remove_koid)
                                    {
                                        dgv_viewmore.Rows[i].Cells[addavidno].Value = newpart;
                                    }
                                }

                                #endregion

                                #region Price Reset main product after remove addon

                                dmnSearch.Select = "DiscountPer Field1,TaxType Field2,TaxPer Field3,Tax Field4,srate Field5";
                                dmnSearch.Table = "tblOrderDetails";
                                dmnSearch.WhereNdGroupBy = "where OrderMasterId = " + Convert.ToInt32(dgv_viewmore.Rows[addonrowindex].Cells[addordermasteridno].Value) + " and OrderNo = " + Convert.ToInt32(dgv_viewmore.Rows[addonrowindex].Cells[addonorderno].Value) + " and AddonStatus = 1 and AV_id = '" + avidValue + "'";
                                dmnSearch.OrderBy = "";
                                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                                if (gridSource.Count > 0)
                                {
                                    newprice = Convert.ToDecimal(gridSource[0].Field5) - (Convert.ToDecimal(dgv_viewmore.Rows[addonrowindex].Cells[addqtyno].Value) * Convert.ToDecimal(dgv_viewmore.Rows[addonrowindex].Cells[addonpriceno].Value));
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
                                //}
                                dmnRemove_addon.TaxAmount = TaxAmount;
                                dmnRemove_addon.NetAmount = finalprice;
                                dmnRemove_addon.Srate = newprice;
                                dmnRemove_addon.MarketPrice = finalprice;
                                dmnRemove_addon.OrderNo = Convert.ToInt32(dgv_viewmore.Rows[addonrowindex].Cells[addonorderno].Value);

                                dmnRemove_addon.OrderDetailsId = Convert.ToInt32(dgv_viewmore.Rows[addonrowindex].Cells[addorderdetailidno].Value);
                                dmnRemove_addon.koid = Convert.ToInt32(dgv_viewmore.Rows[addonrowindex].Cells[addkoidno].Value);
                                dmnRemove_addon.OrderMasterId = Convert.ToInt32(dgv_viewmore.Rows[addonrowindex].Cells[addordermasteridno].Value);
                                dmnRemove_addon.AVid = Convert.ToString(dgv_viewmore.Rows[addonrowindex].Cells[addavidno].Value);
                                dmnRemove_addon.Status = 0;
                                dmnRemove_addon.CreatedBy = FrmDashboard.UserId;
                                dmnRemove_addon.Update_AVid = newpart;
                                var InsertStatusaddon = bll_rem_addon.Kitchenremove_addon("prd_remove_KitchenOrder_addon", dmnRemove_addon);

                                dgv_viewmore.Rows.RemoveAt(addonrowindex);

                                if (dgv_viewmore.RowCount == 0)
                                {
                                    pnlproductviewmore.Visible = false;
                                    tlp_main.Enabled = true;
                                }
                                else
                                {
                                    for (int i = 0; i < dgv_viewmore.RowCount; i++)
                                        dgv_viewmore.Rows[i].Cells[addslno].Value = i + 1;
                                }

                            }
                            #endregion
                        }
                        tid = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dtidno].Value);
                        dmnRemove_order.OrderDetailsId = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[ddetailsidno].Value);
                        dmnRemove_order.koid = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dkoidno].Value);
                        dmnRemove_order.OrderMasterId = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dordermasteridno].Value);
                        dmnRemove_order.Tid = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dtidno].Value);
                        decimal originalValue = Convert.ToDecimal(dgv_product_details.Rows[dgv_view_rowno].Cells[dqtyno].Value);

                        dmnRemove_order.AddonStatus = Convert.ToInt32(dgv_product_details.Rows[dgv_view_rowno].Cells[dAddonStatusno].Value);
                        dmnRemove_order.AV_id = Convert.ToString(dgv_product_details.Rows[dgv_view_rowno].Cells[dAVIDno].Value);

                        dmnRemove_order.Quantity = (int)Math.Floor(originalValue);
                        dmnRemove_order.Status = 0;
                        dmnRemove_order.RemoveNotes = txt_product_remove_des.Text;
                        dmnRemove_order.CancelNotifyStatus = 1;
                        dmnRemove_order.KitchenCancelStatus = 1;
                        dmnRemove_order.Sourcepage = Convert.ToInt32(CancelSourcePage.Kitchen);
                        var InsertStatusP = bll_order.Remove_order_KitchenOrder_ByProduct("prd_remove_Order_kitchen", dmnRemove_order);

                        string startbuttonname = "pb_start_k_" + korderno;
                        string readybuttonname = "pb_ready_k_" + korderno;
                        string cancelfoodbuttonname = "pictureBoxRemove_k_" + korderno;
                        string rprint = "reprintbutton_k_" + korderno;
                        string descrip = "txt_Description_k_" + korderno;
                        string tlpname = "tlp_product_box_k_" +korderno;
                        TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel; 
                        PictureBox btn_ready = this.Controls.Find(readybuttonname, true).FirstOrDefault() as PictureBox;
                        PictureBox btn_start = this.Controls.Find(startbuttonname, true).FirstOrDefault() as PictureBox;
                        PictureBox btn_cancelfood = this.Controls.Find(cancelfoodbuttonname, true).FirstOrDefault() as PictureBox;
                        TextBox txtdes = this.Controls.Find(descrip, true).FirstOrDefault() as TextBox;

                        PictureBox pb_rprint = this.Controls.Find(rprint, true).FirstOrDefault() as PictureBox;
                        if (btn_ready != null)
                            btn_ready.Visible = false;
                        if (btn_start != null)
                            btn_start.Visible = false;
                        if (btn_cancelfood != null)
                            btn_cancelfood.Visible = false;
                        if (pb_rprint != null)
                            pb_rprint.Visible = false;
                        if (txtdes != null)
                        {
                            txtdes.Visible = true;
                            txtdes.Text = "Waiting for customer confirmation.";
                        }

                        if(tlp_sele!=null)
                            tlp_sele.RowStyles[2] = new RowStyle(SizeType.AutoSize);

                        DataGridViewImageCell cell = (DataGridViewImageCell)dgv_product_details.Rows[dgv_view_rowno].Cells[dactionno];
                        cell.Value = Properties.Resources.orderview_cancelled;
                        DataGridViewImageCell cell1 = (DataGridViewImageCell)dgv_product_details.Rows[dgv_view_rowno].Cells[dactionno];
                        dgv_product_details.Rows[dgv_view_rowno].Cells[dremnoteno].Value = txt_product_remove_des.Text;
                        pnl_products_rem_descr.Visible = false;
                        tlp_main.Enabled = true;
                        txt_product_remove_des.Text = "";
                        refreshtimer.Start();
                    }
                    if (System.Windows.Forms.Application.OpenForms["FrmOrderView"] != null)
                    {
                        FrmOrderView _instanceFrmOrderView = Application.OpenForms["FrmOrderView"] as FrmOrderView;
                        _instanceFrmOrderView.FillOrders();
                    }

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View btn_rem_product_des_Click", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        public void GridSlno_Refresh()
        {
            /*
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
             int mainNo = 1;
                int subNo = 1;
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
            */
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // Assuming orderbox is a member of the class
                foreach (var dataGridViewInfo in orderbox)
                {
                    string dataGridViewName = dataGridViewInfo.datagridviewname;
                    DataGridView dgv = Controls.Find(dataGridViewName, true).FirstOrDefault() as DataGridView;
                    if (dgv != null)
                    {
                        string lbltimename = "lbl_time_K_" + dgv.Tag;
                        DateTime startTime = dataGridViewInfo.createtime;
                        DateTime currentTime = DateTime.Now;
                        TimeSpan timeDifference = currentTime - startTime;

                        int hoursDifference = timeDifference.Hours;
                        int minutesDifference = timeDifference.Minutes;
                        int secondsDifference = timeDifference.Seconds;

                        string timeDifferenceString = $"{hoursDifference} h, {minutesDifference} min, {secondsDifference} s";
                        Label lbl_time = Controls.Find(lbltimename, true).FirstOrDefault() as Label;
                        if (lbl_time != null)
                            lbl_time.Text = timeDifferenceString;
                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View timer1_Tick", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }

        }
        private string GetPlaceholder()
        {
            switch (cmbserachtype.SelectedIndex)
            {
                case 0:
                    return "Enter Kitchen Number...";
                case 1:
                    return "Enter Table Code...";
                case 2:
                    return "Enter Pickup No...";
                default:
                    return "Enter Value...";
            }
        }
        private void txtorderid_search_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtsearchData.Text))
            {
                txtsearchData.Text = GetPlaceholder();
                txtsearchData.ForeColor = Color.DarkGray;
            }
        }
        private void txtorderid_search_Enter(object sender, EventArgs e)
        {
            string placeholder = GetPlaceholder();

            if (txtsearchData.Text == placeholder)
            {
                txtsearchData.Text = "";
                txtsearchData.ForeColor = Color.Black;
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
                    btnsearch.PerformClick();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View txtorderid_search_KeyDown", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            try
            {
                refreshtimer.Stop();
                cmbserachtype.SelectedIndex = 0;
                KitchenDisplayCount = FrmLogin.KitchenDisplayCount;
                //btn_viewmore_order.Visible = true;
                pnl_empty_msg.Visible = false;

                txtsearchData.Text = GetPlaceholder();
                txtsearchData.ForeColor = Color.DarkGray;
                ClearKitchenCards();
                refreshtimer.Start();
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Kitchen_View btn_refresh_Click",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Kitchen_View)
                );
            }
        }
        private void ClearKitchenCards()
        {          
            flp_order_view.SuspendLayout();
            foreach (Control ctrl in flp_order_view.Controls)
            {
                ctrl.Dispose();
            }

            flp_order_view.Controls.Clear();
            flp_order_view.ResumeLayout(true);
            KitchenCardCache.Clear();
        }


        private void pnl_products_rem_descr_Paint(object sender, PaintEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View pnl_products_rem_descr_Paint", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    if (pnl_products_rem_descr.Visible)
                    {
                        tlp_main.Enabled = true;
                        pnl_products_rem_descr.Visible = false;
                        refreshtimer.Start();
                    }
                    else if (pnlproductviewmore.Visible)
                    {
                        tlp_main.Enabled = true;
                        pnlproductviewmore.Visible = false;
                    }
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
        private void btn_close_viewmore_Click(object sender, EventArgs e)
        {
            tlp_main.Enabled = true;
            pnlproductviewmore.Visible = false;
        }
        private void FrmKitchenView_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (FrmDashboard.AutoLogoutWhenIdle)
                {
                    AppSession.UnregisterForm(this);
                }
                refreshtimer.Stop();
                this.Dispose();
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View FrmKitchenView_FormClosing", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
        private void flp_order_view_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                flp_order_view.SuspendLayout();
                flp_order_view.ResumeLayout();
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View flp_order_view_Scroll", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
        private void txtdisplaycount_KeyPress(object sender, KeyPressEventArgs e)
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
        private void btn_viewmore_order_Click(object sender, EventArgs e)
        {
            KitchenDisplayCount = 25;
            btn_viewmore_order.Visible = false;
        }
        private void blinkTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                lblprintloading.Text = Convert.ToString(lblprintloading.Tag);
                lblprintloading.Visible = !lblprintloading.Visible;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View blinkTimer_Tick", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
        private void btnprintcount_Click(object sender, EventArgs e)
        {
            if (refreshtimer.Enabled)
            {
                refreshtimer.Start();
            }
        }

        //private void refreshtimer_Tick(object sender, EventArgs e)
        //{
        //    RunMethodInSeparateThread(PrintcancelOrder);
        //    FillOrdersAsync();
        //    if (Convert.ToInt32(btnprintcount.Text) > 0)
        //        RunMethodInSeparateThread(PrintOrderAsync);
        //    else
        //    {
        //        this.Invoke(new Action(() =>
        //        {
        //            lblprintloading.Visible = false;
        //        }));
        //    }

        //}

        private void refreshtimer_Tick(object sender, EventArgs e)
        {
            refreshtimer.Stop();
            try
            {
                FillOrders();
                PrintOrderAsync();
                PrintcancelOrder();
                Printcancelitems();
            }
            finally
            {
                refreshtimer.Start();
            }
        }

        private static void RunMethodInSeparateThread(Action action)
        {
            var thread = new Thread(new ThreadStart(action));
            thread.Start();
        }

        private void dgv_viewmore_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == addactionno && e.RowIndex>=0)
                {
                    DmnKitchenaddonremove dmnRemove_addon = new DmnKitchenaddonremove();
                    BllKitchenorder bll_rem_addon = new BllKitchenorder();
                    decimal newprice = 0, discountprice = 0, TaxAmount = 0, finalprice = 0, disAmnt = 0;
                    string newpart = "";
                    var result = CMessageBox.Show("Are you sure you want to cancel this Addon?", "Confirm The Action", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        refreshtimer.Stop();

                        #region To Reset Addon ID(remove id form avid name)

                        string avidValue = Convert.ToString(dgv_viewmore.Rows[e.RowIndex].Cells[addavidno].Value);
                        string[] avidParts = avidValue.Split('_');
                        string extractedpid = $"{avidParts[0]}";
                        newpart = newpart + extractedpid + "_";
                        int remove_koid = Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addkoidno].Value);
                        for (int i = 0; i < dgv_viewmore.RowCount; i++)
                        {
                            if (Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addaddonstatusno].Value) == 2 && Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addkoidno].Value) != remove_koid)
                            {
                                string ccode = Convert.ToString(dgv_viewmore.Rows[i].Cells[addccodeno].Value);
                                decimal quantityDecimal = Convert.ToDecimal(dgv_viewmore.Rows[i].Cells[addqtyno].Value);
                                int quantityInt = (int)Math.Floor(quantityDecimal);
                                newpart = newpart + ccode + quantityInt.ToString();
                            }
                        }

                        for (int i = 0; i < dgv_viewmore.RowCount; i++)
                        {
                            if (Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addaddonstatusno].Value) == 2 && Convert.ToInt32(dgv_viewmore.Rows[i].Cells[addkoidno].Value) != remove_koid)
                            {
                                dgv_viewmore.Rows[i].Cells[addavidno].Value = newpart;
                            }
                        }

                        #endregion

                        #region Price Reset main product after remove addon
                        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                        DmnSearch dmnSearch = new DmnSearch();
                        dmnSearch.Select = "DiscountPer Field1,TaxType Field2,TaxPer Field3,Tax Field4,srate Field5";
                        dmnSearch.Table = "tblOrderDetails";
                        dmnSearch.WhereNdGroupBy = "where OrderMasterId = " + Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addordermasteridno].Value) + " and OrderNo = " + Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addonorderno].Value) + " and AddonStatus = 1 and AV_id = '" + avidValue + "'";
                        dmnSearch.OrderBy = "";
                        var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                        if (gridSource.Count > 0)
                        {
                            newprice = Convert.ToDecimal(gridSource[0].Field5) - Convert.ToDecimal(dgv_viewmore.Rows[e.RowIndex].Cells[addonpriceno].Value);
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
                        dmnRemove_addon.OrderNo = Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addonorderno].Value);

                        dmnRemove_addon.OrderDetailsId = Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addorderdetailidno].Value);
                        dmnRemove_addon.koid = Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addkoidno].Value);
                        dmnRemove_addon.OrderMasterId = Convert.ToInt32(dgv_viewmore.Rows[e.RowIndex].Cells[addordermasteridno].Value);
                        dmnRemove_addon.AVid = Convert.ToString(dgv_viewmore.Rows[e.RowIndex].Cells[addavidno].Value);
                        dmnRemove_addon.Status = 0;
                        dmnRemove_addon.CreatedBy = FrmDashboard.UserId;
                        dmnRemove_addon.Update_AVid = newpart;
                        var InsertStatusaddon = bll_rem_addon.Kitchenremove_addon("prd_remove_KitchenOrder_addon", dmnRemove_addon);

                        dgv_viewmore.Rows.RemoveAt(e.RowIndex);
                        if(dgv_viewmore.RowCount==0)
                        {
                            pnlproductviewmore.Visible = false;
                            tlp_main.Enabled = true;
                        }
                        else
                        {
                            for (int i = 0; i < dgv_viewmore.RowCount; i++)
                                dgv_viewmore.Rows[i].Cells[addslno].Value = i + 1;
                        }
                        refreshtimer.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Kitchen_View dgv_viewmore_CellDoubleClick", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }

        }

        private void pnl_kitchenorderBill_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            startPoint = new Point(e.X, e.Y);
        }

        private void pnl_kitchenorderBill_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point p = PointToScreen(e.Location);
                pnl_kitchenorderBill.Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
            }
        }

        private void pnl_kitchenorderBill_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void tm_memory_Tick(object sender, EventArgs e)
        {
            Process currentProcess = Process.GetCurrentProcess();
            long memoryInBytes = currentProcess.PrivateMemorySize64;
            double memoryInMB = memoryInBytes / (1024.0 * 1024.0);
            lblmemory.Text = $"Memory: {memoryInMB:F2} MB";
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            // Mandatory textbox check
            if (string.IsNullOrWhiteSpace(txtsearchData.Text))
            {
                CMessageBox.Show("Please enter search text.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtsearchData.Focus();
                return;
            }

            // Mandatory combobox check
            if (cmbserachtype.SelectedIndex == -1)
            {
                CMessageBox.Show("Please select search type.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbserachtype.DroppedDown = true;
                return;
            }

            string searchText = txtsearchData.Text.Trim();
            int stype = cmbserachtype.SelectedIndex;
            if (stype == 0)
                SerachTable_Pickup(searchText, Convert.ToInt32(KitchenSearchType.ByKitchenNumber));
            else if (stype == 1)
                SerachTable_Pickup(searchText, Convert.ToInt32(KitchenSearchType.ByTableCode));
            else if (stype == 2)
                SerachTable_Pickup(searchText, Convert.ToInt32(KitchenSearchType.ByPkId));

        }
        public void SerachTable_Pickup(string sdata,int serachtype)
        {
            try
            {
                GC.Collect();

                refreshtimer.Stop();

                SetEmptyIcon();
                #region Fill Orders in Datagrid
                txt_product_remove_des.Text = "Reason type here .....";
                int countpendingorder = 0;

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                BllSearch<DmnGridFields> bllSearch_details = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                BllKitchenData<DmnGridFields> SearchDetails = new BllKitchenData<DmnGridFields>();
                DmnGridFields dmnSearchKitchendata = new DmnGridFields();
                dmnSearchKitchendata.Searchtype =serachtype;
                dmnSearchKitchendata.Field30 = sdata;
                var gridSourcemaster = SearchDetails.serachreport("prdKitchenData", dmnSearchKitchendata);
                if (gridSourcemaster.Count > 0)
                {
                    //pnl_empty_msg.Visible = false;
                    //flp_order_view.Visible = false;
                    var distinctOrders = gridSourcemaster
                       .GroupBy(order => new { order.Field1, order.Field2 })
                       .Select(group => group.First())
                       .OrderBy(order => order.Field1)
                       .ThenBy(order => order.Field2)
                       .ToList();

                    btntotalorders.Text = Convert.ToString(distinctOrders.Count);

                    countpendingorder = distinctOrders.Count(order => Convert.ToInt32(order.Field3) == 1);
                    btnprintcount.Text = Convert.ToString(countpendingorder);

                    var preserveNames = distinctOrders.Select(orders => "tlp_product_box_k_" + orders.Field1 + orders.Field2);
                    int l = 0;
                    foreach (Control control in flp_order_view.Controls.Cast<Control>().ToArray())
                    {
                        if (control is TableLayoutPanel tlp && !preserveNames.Contains(tlp.Name))
                        {
                            flp_order_view.Controls.Remove(control);
                            control.Dispose();
                            clsgen.Memoryrelease();
                        }
                        l = l + 1;
                    }
                    int rowCountDisplay = 0;
                    //flp_order_view.Controls.Clear();
                    foreach (var orders in distinctOrders)
                    {
                        bool flagstart_ready = false;

                        BllkitchenDataDetails<Dmnkitchendetails> KtDetails = new BllkitchenDataDetails<Dmnkitchendetails>();
                        Dmnkitchendetails dmnSearchtabledata = new Dmnkitchendetails();
                        dmnSearchtabledata.OrderNo = Convert.ToInt32(orders.Field2);
                        dmnSearchtabledata.OrderMasterId = Convert.ToInt32(orders.Field1);
                        var gridSourcedetails = KtDetails.serachData("prdKitchendetails", dmnSearchtabledata);
                        if (gridSourcedetails.Count > 0)
                        {
                            string tlpname = "tlp_product_box_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                            TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                            if (tlp_sele != null)
                            {
                                flp_order_view.SuspendLayout();
                                string dgvname = "dgv_product_details_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                string startbuttonname = "pb_start_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                string readybuttonname = "pb_ready_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                string cancelfoodbuttonname = "pictureBoxRemove_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                string rprint = "reprintbutton_k_" + orders.Field1 + gridSourcedetails[0].Field17;

                                string pnldesname = "pnl_Description_k_" + orders.Field1 + gridSourcedetails[0].Field17;

                                // ---------------- FIND ALL CONTROLS ----------------

                                // Start button
                                PictureBox btn_start = this.Controls
                                    .Find(startbuttonname, true)
                                    .FirstOrDefault() as PictureBox;

                                // Ready button
                                PictureBox btn_ready = this.Controls
                                    .Find(readybuttonname, true)
                                    .FirstOrDefault() as PictureBox;

                                // Cancel food button
                                PictureBox btn_cancelfood = this.Controls
                                    .Find(cancelfoodbuttonname, true)
                                    .FirstOrDefault() as PictureBox;

                                // Reprint button
                                PictureBox pbrprint = this.Controls
                                    .Find(rprint, true)
                                    .FirstOrDefault() as PictureBox;

                                // Description panel
                                Panel pnldesccont = this.Controls
                                    .Find(pnldesname, true)
                                    .FirstOrDefault() as Panel;

                                // TextBox inside panel
                                TextBox txtdes = null;
                                if (pnldesccont != null)
                                {
                                    // Finds first textbox inside panel
                                    txtdes = pnldesccont.Controls
                                        .OfType<TextBox>()
                                        .FirstOrDefault();
                                }

                                // DataGridView
                                DataGridView dgv_product_details = this.Controls
                                    .Find(dgvname, true)
                                    .FirstOrDefault() as DataGridView;


                                if (Convert.ToInt32(gridSourcedetails[0].Field26) == 1)
                                {
                                    if (btn_ready != null)
                                        btn_ready.Visible = false;
                                    if (btn_start != null)
                                        btn_start.Visible = false;
                                    if (btn_cancelfood != null)
                                        btn_cancelfood.Visible = false;
                                    if (pbrprint != null)
                                        pbrprint.Visible = false;

                                    if (txtdes != null)
                                    {
                                        txtdes.Visible = true;
                                        txtdes.Text = "Waiting for customer confirmation.";
                                    }
                                    tlp_sele.RowStyles[2] = new RowStyle(SizeType.AutoSize);
                                }
                                else
                                {
                                    #region Grid Edit Cancel  product

                                    for (int k = 0; k < gridSourcedetails.Count; k++)
                                    {
                                        if (Convert.ToInt32(gridSourcedetails[k].Field12) == 1)
                                            flagstart_ready = true;
                                        else if (Convert.ToInt32(gridSourcedetails[k].Field12) == 2)
                                            flagstart_ready = false;

                                        dgv_product_details.Rows[k].Cells[dactionno].Value = Properties.Resources.close_price_change;
                                        dgv_product_details.Rows[k].Cells[dAVIDno].Value = gridSourcedetails[k].Field20;
                                        dgv_product_details.Rows[k].Cells[dstatusno].Value = gridSourcedetails[k].Field12;

                                        DataGridViewImageCell cell = (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dactionno];
                                        if (Convert.ToInt32(gridSourcedetails[k].Field12) == 0)
                                        {
                                            var row = dgv_product_details.Rows[k];

                                            // Create a unique key for this row (e.g., using order ID + product ID)
                                            string rowKey = gridSourcedetails[k].Field1 + "_" + gridSourcedetails[k].Field11;

                                            // UI setup (can be repeated safely)
                                            row.ReadOnly = true;
                                            row.Cells[dremnoteno].Value = gridSourcedetails[k].Field11;
                                            cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                            cell.Value = Properties.Resources.orderview_cancelled;

                                            // Initialize counter and color state if not exists
                                            if (!rowCounters.ContainsKey(rowKey))
                                                rowCounters[rowKey] = 0;
                                            if (!rowColorState.ContainsKey(rowKey))
                                                rowColorState[rowKey] = false; // start with white

                                            // Increment counter
                                            rowCounters[rowKey]++;

                                            // Toggle color when counter reaches blinkPeriod
                                            if (rowCounters[rowKey] >= blinkPeriod)
                                            {
                                                rowColorState[rowKey] = !rowColorState[rowKey];
                                                row.DefaultCellStyle.BackColor = rowColorState[rowKey] ? Color.LightCoral : Color.White;

                                                // Reset counter
                                                rowCounters[rowKey] = 0;
                                            }
                                        }
                                        else
                                        {
                                            cell.Value = Properties.Resources.kitchenfood_cancel_icon;
                                            cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                        }

                                        DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dptypeno];
                                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                        cellcolumntype.ToolTipText = gridSourcedetails[k].Field4;
                                        if (Convert.ToInt32(gridSourcedetails[k].Field13) == Convert.ToInt32(Tabletype.ComboProduct))
                                        {
                                            cellcolumntype.Value = Properties.Resources.kitchengridcomboicon;
                                        }
                                        else if (Convert.ToInt32(gridSourcedetails[k].Field23) >= 1)
                                        {
                                            cellcolumntype.Value = Properties.Resources.addonbutton;
                                            cellcolumntype.ToolTipText = "Addon [clcik view more...]";
                                        }
                                        else
                                        {
                                            if (Convert.ToInt32(k) % 2 == 0)
                                                cellcolumntype.Value = Properties.Resources.orderview_blank;
                                            else
                                                cellcolumntype.Value = Properties.Resources.orderview_blank_2;
                                        }

                                    }
                                    #endregion

                                    #region Edit Ready and Close Button 
                                    if (flagstart_ready)
                                    {
                                        btn_start.Visible = true;
                                        btn_cancelfood.Visible = true;
                                        btn_ready.Visible = false;
                                    }
                                    else
                                    {
                                        if (btn_start != null)
                                            btn_start.Visible = false;
                                        btn_ready.Visible = true;
                                        btn_cancelfood.Visible = false;
                                    }
                                    #endregion
                                }

                                flp_order_view.ResumeLayout();
                            }
                            else
                            {
                                //if (rowCountDisplay >= KitchenDisplayCount)
                                //    break;

                                flp_order_view.SuspendLayout();
                                refreshtimer.Stop();

                                #region Main TableLayoutPanel 
                                // Main TableLayoutPanel for each product
                                TableLayoutPanel tlp_product_box = new TableLayoutPanel();
                                tlp_product_box.Name = "tlp_product_box_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                tlp_product_box.Dock = DockStyle.Top;

                                tlp_product_box.Visible = false;
                                tlp_product_box.SuspendLayout();

                                if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                                {
                                    FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                                    if (_instanceFrmdash.ChkWritePermission(Convert.ToInt32(formsenum.Kitchen_View)))
                                        tlp_product_box.Enabled = true;
                                    else
                                        tlp_product_box.Enabled = false;
                                }

                                int WidthPercentage = 30;
                                int heightPercentage = 40;
                                int controlWidth = (int)(this.Width * WidthPercentage / 100);
                                int controlHeight = (int)(this.Height * heightPercentage / 100);
                                tlp_product_box.Size = new Size(controlWidth, controlHeight);

                                tlp_product_box.BackColor = boxcolor;
                                tlp_product_box.RowCount = 4;
                                //tlp_product_box.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
                                tlp_product_box.Margin = new Padding(20, 10, 0, 0);
                                tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                tlp_product_box.RowStyles.Add(new RowStyle(SizeType.Percent, 100)); // 50% height
                                tlp_product_box.RowStyles.Add(new RowStyle(SizeType.Percent, 25));
                                tlp_product_box.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                                //ApplyRoundedCorners(tlp_product_box);
                                #endregion

                                #region Heading Time and Tablecode Frist Row
                                // Sub TableLayoutPanel for Heading
                                TableLayoutPanel tlp_product_heading = new TableLayoutPanel();
                                tlp_product_heading.Name = "tlp_product_heading_k_" + orders.Field1 + gridSourcedetails[0].Field17; ;
                                tlp_product_heading.Dock = DockStyle.Top;
                                if (screenWidth > 1367 && screenHeight > 769)
                                {
                                    tlp_product_heading.Height = 50;
                                }
                                else
                                {
                                    tlp_product_heading.Height = 20;
                                }

                                tlp_product_heading.ColumnCount = 3;
                                tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));//33.33F
                                tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                                tlp_product_heading.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                                #region Table Name 
                                Label lbl_tablecode = new Label()
                                {

                                    Name = "lbl_tablecod_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Dock = DockStyle.Fill,
                                    ForeColor = Color.White,
                                    AutoSize = true,
                                    TextAlign = ContentAlignment.MiddleLeft,

                                };
                                #endregion

                                if (Convert.ToInt32(gridSourcedetails[0].Field25) > 0)
                                {
                                    lbl_tablecode.Text = "Pick Up  " + gridSourcedetails[0].Field25;
                                    lbl_tablecode.Font = new Font("Arial", 12, FontStyle.Bold);
                                }
                                else
                                {
                                    lbl_tablecode.Text = gridSourcedetails[0].Field2;

                                    if (screenWidth > 1367 && screenHeight > 769)
                                        lbl_tablecode.Font = new Font("Arial", 15, FontStyle.Bold);
                                    else
                                        lbl_tablecode.Font = new Font("Arial", 10, FontStyle.Bold);

                                }
                                #region Token No

                                Label lbl_tokkenno = new Label()
                                {
                                    Text = "Token No: " + gridSourcedetails[0].Field22,
                                    Name = "lbl_tokkenno_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Dock = DockStyle.Fill,
                                    ForeColor = Color.White,
                                    AutoSize = true,
                                    TextAlign = ContentAlignment.MiddleCenter,
                                };

                                if (screenWidth > 1367 && screenHeight > 769)
                                    lbl_tokkenno.Font = new Font("Arial", 15, FontStyle.Bold);
                                else
                                    lbl_tokkenno.Font = new Font("Arial", 12, FontStyle.Bold);

                                #endregion

                                #region Time Duration
                                DateTime startTime = gridSourcedetails[0].Fielddatetime;
                                DateTime currentTime = DateTime.Now;
                                TimeSpan timeDifference = currentTime - startTime;
                                int hoursDifference = timeDifference.Hours;
                                int minutesDifference = timeDifference.Minutes;
                                int secondsDifference = timeDifference.Seconds;
                                string timeDifferenceString = $"{hoursDifference} h, {minutesDifference} min {secondsDifference}";//,  seconds

                                Label lbl_time = new Label()
                                {
                                    Text = timeDifferenceString,
                                    Name = "lbl_time_K_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Dock = DockStyle.Fill,
                                    ForeColor = Color.White,
                                    AutoSize = true,
                                    TextAlign = ContentAlignment.MiddleRight,

                                };

                                if (screenWidth > 1367 && screenHeight > 769)
                                    lbl_time.Font = new Font("Arial", 11, FontStyle.Bold);
                                else
                                    lbl_time.Font = new Font("Arial", 8, FontStyle.Bold);
                                #endregion

                                tlp_product_heading.Controls.Add(lbl_tablecode, 0, 0);
                                tlp_product_heading.Controls.Add(lbl_tokkenno, 1, 0);
                                tlp_product_heading.Controls.Add(lbl_time, 2, 0);

                                // Adding Heading TableLayoutPanel to the first row
                                tlp_product_box.Controls.Add(tlp_product_heading, 0, 0);
                                #endregion

                                #region DataGridView Second Row
                                bool comboflag = false, addonflag = false;

                                #region dgv_product_details Declaration
                                // DataGridView for product details
                                DataGridView dgv_product_details = new DataGridView();

                                // Basic config
                                dgv_product_details.CellClick += dgv_product_details_CellClick;
                                dgv_product_details.AllowUserToDeleteRows = true;
                                dgv_product_details.ColumnHeadersDefaultCellStyle.Font = new Font(dgv_product_details.Font, FontStyle.Bold);
                                dgv_product_details.ColumnHeadersDefaultCellStyle.Font =
                                    new Font(dgv_product_details.ColumnHeadersDefaultCellStyle.Font.FontFamily, 12, FontStyle.Bold);

                                dgv_product_details.AllowUserToAddRows = false;
                                dgv_product_details.Name = "dgv_product_details_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                dgv_product_details.Tag = orders.Field1 + gridSourcedetails[0].Field17;

                                dgv_product_details.DoubleBuffered(true);
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
                                dgv_product_details.AllowUserToResizeRows = false;

                                if (screenWidth > 1367 && screenHeight > 769)
                                {
                                    dgv_product_details.ColumnHeadersHeight = 70;
                                    dgv_product_details.RowTemplate.Height = 70;
                                }
                                else
                                {
                                    dgv_product_details.ColumnHeadersHeight = 30;
                                    dgv_product_details.RowTemplate.Height = 30;
                                }

                                dgv_product_details.RowHeadersVisible = false;

                                // Add columns
                                DataGridViewTextBoxColumn columnSlno = new DataGridViewTextBoxColumn();
                                DataGridViewImageColumn pbremove = new DataGridViewImageColumn();
                                DataGridViewImageColumn columnptype = new DataGridViewImageColumn();
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
                                DataGridViewTextBoxColumn columnitemid = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columntabletype = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columnorderno = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columnAddonStatusno = new DataGridViewTextBoxColumn();
                                DataGridViewTextBoxColumn columnAVIDno = new DataGridViewTextBoxColumn();

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
                                dgv_product_details.Columns.Add(columnitemid);
                                dgv_product_details.Columns.Add(columntabletype);
                                dgv_product_details.Columns.Add(columnorderno);
                                dgv_product_details.Columns.Add(columnAddonStatusno);
                                dgv_product_details.Columns.Add(columnAVIDno);

                                // Header text
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
                                dgv_product_details.Columns[ditemidno].HeaderText = "itemid";
                                dgv_product_details.Columns[dtabletypeno].HeaderText = "tabletype";
                                dgv_product_details.Columns[dorderno].HeaderText = "orderno";
                                dgv_product_details.Columns[dAddonStatusno].HeaderText = "AddonStatus";
                                dgv_product_details.Columns[dAVIDno].HeaderText = "AVID";

                                // Hide backend columns
                                dgv_product_details.Columns[dslno].Visible = false;
                                dgv_product_details.Columns[ddetailsidno].Visible = false;
                                dgv_product_details.Columns[dkoidno].Visible = false;
                                dgv_product_details.Columns[dtidno].Visible = false;
                                dgv_product_details.Columns[dordermasteridno].Visible = false;
                                dgv_product_details.Columns[ddatetimeno].Visible = false;
                                dgv_product_details.Columns[dstatusno].Visible = false;
                                dgv_product_details.Columns[ditemidno].Visible = false;
                                dgv_product_details.Columns[dtabletypeno].Visible = false;
                                dgv_product_details.Columns[dorderno].Visible = false;
                                dgv_product_details.Columns[dAddonStatusno].Visible = false;
                                dgv_product_details.Columns[dAVIDno].Visible = false;

                                // Bold text
                                Font boldFont = new Font(dgv_product_details.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Bold);
                                dgv_product_details.Columns[ditemno].DefaultCellStyle.Font = boldFont;
                                dgv_product_details.Columns[dqtyno].DefaultCellStyle.Font = boldFont;
                                dgv_product_details.Columns[dnotes].DefaultCellStyle.Font = boldFont;
                                dgv_product_details.Columns[dremnoteno].DefaultCellStyle.Font = boldFont;


                                // =====================================================================
                                // FINAL SCROLL + WIDTH SETTINGS (correct position — after columns added)
                                // =====================================================================

                                // Disable autosize fully
                                dgv_product_details.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                                dgv_product_details.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                                // Full scrollbars
                                dgv_product_details.ScrollBars = ScrollBars.Both;

                                // Prevent wrap (no height increase)
                                dgv_product_details.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                                // Item name column — long width for scrolling
                                dgv_product_details.Columns[ditemno].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                                dgv_product_details.Columns[ditemno].Width = 450;

                                // Image columns fixed width
                                dgv_product_details.Columns[dactionno].Width = 60;
                                dgv_product_details.Columns[dptypeno].Width = 60;

                                // Hidden columns width fix
                                foreach (DataGridViewColumn col in dgv_product_details.Columns)
                                {
                                    if (!col.Visible)
                                    {
                                        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                                        col.Width = 1;
                                    }
                                }

                                // Prevent resizing
                                dgv_product_details.AllowUserToResizeColumns = false;

                                #endregion

                                //#region dgv_product_details Declaration
                                //// DataGridView for product details
                                //DataGridView dgv_product_details = new DataGridView();

                                //// 1. Disable AUTO sizing completely
                                //dgv_product_details.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                                //dgv_product_details.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;

                                //// 2. Full scrollbars
                                //dgv_product_details.ScrollBars = ScrollBars.Both;

                                //// 3. Important: Wrap OFF so rows do not increase height
                                //dgv_product_details.DefaultCellStyle.WrapMode = DataGridViewTriState.False;

                                //// 4. Set fixed width for Item Name (show full text)
                                //dgv_product_details.Columns[ditemno].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                                //dgv_product_details.Columns[ditemno].Width = 450;   // adjust as needed

                                //// 5. Fix widths for image columns
                                //dgv_product_details.Columns[dactionno].Width = 60;
                                //dgv_product_details.Columns[dptypeno].Width = 60;

                                //// 6. Hidden columns must not take width (VERY IMPORTANT)
                                //foreach (DataGridViewColumn col in dgv_product_details.Columns)
                                //{
                                //    if (!col.Visible)
                                //    {
                                //        col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                                //        col.Width = 1;
                                //    }
                                //}

                                //// 7. Prevent user resizing columns
                                //dgv_product_details.AllowUserToResizeColumns = false;

                                //dgv_product_details.CellClick += dgv_product_details_CellClick;
                                //dgv_product_details.AllowUserToDeleteRows = true;
                                //dgv_product_details.ColumnHeadersDefaultCellStyle.Font = new Font(dgv_product_details.Font, FontStyle.Bold);
                                //dgv_product_details.ColumnHeadersDefaultCellStyle.Font = new Font(dgv_product_details.ColumnHeadersDefaultCellStyle.Font.FontFamily, 12, FontStyle.Bold);
                                //// Prevent the user from adding new rows
                                //dgv_product_details.AllowUserToAddRows = false;
                                //dgv_product_details.Name = "dgv_product_details_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                //dgv_product_details.Tag = orders.Field1 + gridSourcedetails[0].Field17;
                                //// Enable double buffering for the DataGridView
                                //dgv_product_details.DoubleBuffered(true);


                                //dgv_product_details.BorderStyle = BorderStyle.None;
                                //dgv_product_details.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                                //dgv_product_details.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                                //dgv_product_details.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                                //dgv_product_details.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                                //dgv_product_details.BackgroundColor = Color.White;

                                //dgv_product_details.EnableHeadersVisualStyles = false;
                                //dgv_product_details.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                                //dgv_product_details.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                                //dgv_product_details.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                                //dgv_product_details.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                                //dgv_product_details.AllowUserToResizeRows = false;

                                //if (screenWidth > 1367 && screenHeight > 769)
                                //{
                                //    dgv_product_details.ColumnHeadersHeight = 70;
                                //    dgv_product_details.RowTemplate.Height = 70;
                                //}
                                //else
                                //{
                                //    dgv_product_details.ColumnHeadersHeight = 30;
                                //    dgv_product_details.RowTemplate.Height = 30;
                                //}

                                //dgv_product_details.RowHeadersVisible = false;
                                //// Add columns with specific cell type
                                //DataGridViewTextBoxColumn columnSlno = new DataGridViewTextBoxColumn();
                                //DataGridViewImageColumn pbremove = new DataGridViewImageColumn();
                                //DataGridViewImageColumn columnptype = new DataGridViewImageColumn();
                                //DataGridViewTextBoxColumn columnItemName = new DataGridViewTextBoxColumn();
                                //DataGridViewTextBoxColumn columnQty = new DataGridViewTextBoxColumn();
                                //DataGridViewTextBoxColumn columnNotes = new DataGridViewTextBoxColumn();
                                //DataGridViewTextBoxColumn columnRemNotes = new DataGridViewTextBoxColumn();

                                //DataGridViewTextBoxColumn columnorderdetailsid = new DataGridViewTextBoxColumn();
                                //DataGridViewTextBoxColumn columnkoid = new DataGridViewTextBoxColumn();
                                //DataGridViewTextBoxColumn columntid = new DataGridViewTextBoxColumn();
                                //DataGridViewTextBoxColumn columnmasterid = new DataGridViewTextBoxColumn();
                                //DataGridViewTextBoxColumn columndate = new DataGridViewTextBoxColumn();
                                //DataGridViewTextBoxColumn columnstatus = new DataGridViewTextBoxColumn();
                                //DataGridViewTextBoxColumn columnitemid = new DataGridViewTextBoxColumn();
                                //DataGridViewTextBoxColumn columntabletype = new DataGridViewTextBoxColumn();
                                //DataGridViewTextBoxColumn columnorderno = new DataGridViewTextBoxColumn();
                                //DataGridViewTextBoxColumn columnAddonStatusno = new DataGridViewTextBoxColumn();
                                //DataGridViewTextBoxColumn columnAVIDno = new DataGridViewTextBoxColumn();

                                //dgv_product_details.Columns.Add(columnSlno);
                                //dgv_product_details.Columns.Add(pbremove);
                                //dgv_product_details.Columns.Add(columnptype);
                                //dgv_product_details.Columns.Add(columnItemName);
                                //dgv_product_details.Columns.Add(columnQty);
                                //dgv_product_details.Columns.Add(columnNotes);
                                //dgv_product_details.Columns.Add(columnRemNotes);
                                //dgv_product_details.Columns.Add(columnorderdetailsid);
                                //dgv_product_details.Columns.Add(columnkoid);
                                //dgv_product_details.Columns.Add(columntid);
                                //dgv_product_details.Columns.Add(columnmasterid);
                                //dgv_product_details.Columns.Add(columndate);
                                //dgv_product_details.Columns.Add(columnstatus);
                                //dgv_product_details.Columns.Add(columnitemid);
                                //dgv_product_details.Columns.Add(columntabletype);
                                //dgv_product_details.Columns.Add(columnorderno);
                                //dgv_product_details.Columns.Add(columnAddonStatusno);
                                //dgv_product_details.Columns.Add(columnAVIDno);

                                //dgv_product_details.Columns[dslno].HeaderText = "Slno";
                                //dgv_product_details.Columns[ditemno].HeaderText = "Item Name";
                                //dgv_product_details.Columns[dptypeno].HeaderText = "PType";
                                //dgv_product_details.Columns[dqtyno].HeaderText = "Qty";
                                //dgv_product_details.Columns[dnotes].HeaderText = "Notes";
                                //dgv_product_details.Columns[dremnoteno].HeaderText = "Remove Notes";
                                //dgv_product_details.Columns[ddetailsidno].HeaderText = "orderdetailsid";
                                //dgv_product_details.Columns[dkoidno].HeaderText = "koid";
                                //dgv_product_details.Columns[dtidno].HeaderText = "tid";
                                //dgv_product_details.Columns[dactionno].HeaderText = "Action";
                                //dgv_product_details.Columns[dordermasteridno].HeaderText = "Ordermasterid";
                                //dgv_product_details.Columns[ddatetimeno].HeaderText = "datetime";
                                //dgv_product_details.Columns[dstatusno].HeaderText = "status";
                                //dgv_product_details.Columns[ditemidno].HeaderText = "itemid";
                                //dgv_product_details.Columns[dtabletypeno].HeaderText = "tabletype";
                                //dgv_product_details.Columns[dorderno].HeaderText = "orderno";
                                //dgv_product_details.Columns[dAddonStatusno].HeaderText = "AddonStatus";
                                //dgv_product_details.Columns[dAVIDno].HeaderText = "AVID";

                                //dgv_product_details.Columns[dslno].Visible = false;
                                //dgv_product_details.Columns[ddetailsidno].Visible = false;
                                //dgv_product_details.Columns[dkoidno].Visible = false;
                                //dgv_product_details.Columns[dtidno].Visible = false;
                                //dgv_product_details.Columns[dordermasteridno].Visible = false;
                                //dgv_product_details.Columns[ddatetimeno].Visible = false;
                                //dgv_product_details.Columns[dstatusno].Visible = false;
                                //dgv_product_details.Columns[ditemidno].Visible = false;
                                //dgv_product_details.Columns[dtabletypeno].Visible = false;
                                //dgv_product_details.Columns[dorderno].Visible = false;
                                //dgv_product_details.Columns[dAddonStatusno].Visible = false;
                                //dgv_product_details.Columns[dAVIDno].Visible = false;

                                //Font boldFont = new Font(dgv_product_details.DefaultCellStyle.Font.FontFamily, 10, FontStyle.Bold);
                                //dgv_product_details.Columns[ditemno].DefaultCellStyle.Font = boldFont;
                                //dgv_product_details.Columns[dqtyno].DefaultCellStyle.Font = boldFont;
                                //dgv_product_details.Columns[dnotes].DefaultCellStyle.Font = boldFont;
                                //dgv_product_details.Columns[dremnoteno].DefaultCellStyle.Font = boldFont;
                                //#endregion

                                dgv_product_details.Rows.Clear();
                                for (int k = 0; k < gridSourcedetails.Count; k++)
                                {
                                    if (Convert.ToInt32(gridSourcedetails[k].Field12) == 1)//check ready or not
                                    {
                                        flagstart_ready = true;
                                    }

                                    //#region Update Prnt Staus Set to2
                                    //BllKitchenorder bllkitchen = new BllKitchenorder();
                                    //Dmnkitchen dmnkitchen = new Dmnkitchen();
                                    //dmnkitchen.Koid = Convert.ToInt32(gridSourcedetails[k].Field8);
                                    //dmnkitchen.CreatedBy = FrmDashboard.UserId;
                                    //dmnkitchen.KOTPrintStatus = 2;
                                    //dmnkitchen.BranchId = 1030;
                                    //var InsertStatus = bllkitchen.Kitchen_statusupdate("prd_updateKitchenorderstatus", dmnkitchen);
                                    //#endregion

                                    dgv_product_details.Rows.Add();
                                    dgv_product_details.Rows[k].Cells[dslno].Value = k + 1;
                                    int AddonCount = Convert.ToInt32(gridSourcedetails[k].Field23);
                                    string vrianame = Convert.ToString(gridSourcedetails[k].Field24);
                                    string itemname = Convert.ToString(gridSourcedetails[k].Field4);

                                    //if (AddonCount > 0 && vrianame != null && vrianame != "")
                                    //    dgv_product_details.Rows[k].Cells[ditemno].Value = itemname + "(" + vrianame + ")";
                                    //else if (AddonCount > 0)
                                    //    dgv_product_details.Rows[k].Cells[ditemno].Value = itemname;
                                    if (vrianame != null && vrianame != "")
                                        dgv_product_details.Rows[k].Cells[ditemno].Value = itemname + " ( " + vrianame + " )";
                                    else
                                        dgv_product_details.Rows[k].Cells[ditemno].Value = itemname;


                                    DataGridViewImageCell cellcolumntype = (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dptypeno];
                                    if (Convert.ToInt32(gridSourcedetails[k].Field13) == Convert.ToInt32(Tabletype.ComboProduct))
                                    {
                                        comboflag = true;
                                        cellcolumntype.Value = Properties.Resources.kitchengridcomboicon;
                                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                        cellcolumntype.ToolTipText = gridSourcedetails[k].Field4;
                                    }
                                    else if (Convert.ToInt32(gridSourcedetails[k].Field23) >= 1)
                                    {
                                        addonflag = true;
                                        cellcolumntype.Value = Properties.Resources.addonbutton;
                                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                        cellcolumntype.ToolTipText = "Addon [clcik view more...]";
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(k) % 2 == 0)
                                            cellcolumntype.Value = Properties.Resources.orderview_blank;
                                        else
                                            cellcolumntype.Value = Properties.Resources.orderview_blank_2;

                                        //cellcolumntype.Value = Properties.Resources.grid_select;
                                        cellcolumntype.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                        cellcolumntype.ToolTipText = gridSourcedetails[k].Field4;
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
                                    dgv_product_details.Rows[k].Cells[ditemidno].Value = gridSourcedetails[k].Field14;
                                    dgv_product_details.Rows[k].Cells[dtabletypeno].Value = gridSourcedetails[k].Field15;
                                    dgv_product_details.Rows[k].Cells[dorderno].Value = gridSourcedetails[k].Field17;

                                    DataGridViewImageCell cell = (DataGridViewImageCell)dgv_product_details.Rows[k].Cells[dactionno];
                                    if (Convert.ToInt32(gridSourcedetails[k].Field12) == 0)
                                    {
                                        cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                        cell.Value = Properties.Resources.orderview_cancelled;
                                        dgv_product_details.Rows[k].ReadOnly = true;
                                    }
                                    else
                                    {
                                        cell.Value = Properties.Resources.kitchenfood_cancel_icon;
                                        cell.ImageLayout = DataGridViewImageCellLayout.Zoom;
                                    }

                                    if (Convert.ToInt32(gridSourcedetails[k].Field19) == Convert.ToInt32(AddonType.AddonType))
                                    {
                                        dgv_product_details.Rows[k].Cells[dAddonStatusno].Value = Convert.ToInt32(AddonType.AddonType);
                                        dgv_product_details.Rows[k].Cells[dAVIDno].Value = gridSourcedetails[k].Field20;
                                    }
                                    else
                                        dgv_product_details.Rows[k].Cells[dAddonStatusno].Value = Convert.ToInt32(AddonType.None);
                                }

                                for (int x = 0; x < dgv_product_details.RowCount; x++)
                                {
                                    //if (x != dactionno)
                                    dgv_product_details.Rows[x].ReadOnly = true;
                                }
                                if (addonflag != true && comboflag != true)
                                    dgv_product_details.Columns[dptypeno].Visible = false;

                                tlp_product_box.Controls.Add(dgv_product_details, 0, 1);
                                dgv_product_details.Dock = DockStyle.Fill;

                                //clsgen.GridAutoSizeSmart(dgv_product_details);
                                #endregion

                                #region Description
                                Panel pnldesc = new Panel();
                                pnldesc.Name = "pnl_Description_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                pnldesc.AutoScroll = true;
                                pnldesc.Dock = DockStyle.Fill;

                                //pnldesc.Padding = new Padding(8, 8, 8, 8);
                                //Label lbl_Description = new Label()
                                //{
                                //    Text = "Note :   " + gridSourcedetails[0].Field16,
                                //    Name = "lbl_Description_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                //    Dock = DockStyle.Fill,
                                //    ForeColor = Color.White,
                                //    AutoSize = true,
                                //    BackColor = Color.Transparent,
                                //};
                                TextBox txt_Description = new TextBox()
                                {
                                    Text = "Note :   " + gridSourcedetails[0].Field16,
                                    Name = "txt_Description_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Dock = DockStyle.Fill,
                                    ForeColor = Color.White,
                                    BackColor = boxcolor,
                                    ReadOnly = true,
                                    Multiline = true,
                                    ScrollBars = ScrollBars.Vertical,
                                    BorderStyle = BorderStyle.None,
                                    Visible = false,
                                };

                                pnldesc.Controls.Add(txt_Description);
                                if (gridSourcedetails[0].Field16 != null && gridSourcedetails[0].Field16 != "")
                                {
                                    tlp_product_box.Controls.Add(pnldesc, 0, 2);
                                    txt_Description.Visible = true;
                                }
                                else
                                {
                                    txt_Description.Visible = false;
                                    tlp_product_box.RowStyles[2] = new RowStyle(SizeType.AutoSize);
                                }

                                #endregion

                                #region Ready and Close Button,Reprint buttons Thrid Row
                                // Sub TableLayoutPanel for Buttons
                                TableLayoutPanel tlp_buttons = new TableLayoutPanel();
                                tlp_buttons.Name = "tlp_buttons_k_" + orders.Field1 + gridSourcedetails[0].Field17;
                                tlp_buttons.Dock = DockStyle.Top;

                                if (screenWidth > 1367 && screenHeight > 769)
                                {
                                    tlp_buttons.Height = 45;
                                }
                                else
                                {
                                    tlp_buttons.Height = 35;
                                }

                                tlp_buttons.ColumnCount = 4;
                                tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                                tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                                tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                                tlp_buttons.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));

                                #region start Button
                                PictureBox pb_start = new PictureBox()
                                {
                                    Name = "pb_start_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Image = Properties.Resources.start,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Dock = DockStyle.None,
                                    Tag = gridSourcedetails[0].Field17,
                                    AccessibleDescription = orders.Field1,
                                };
                                if (screenWidth > 1367 && screenHeight > 769)
                                {
                                    pb_start.Height = 40;
                                    pb_start.Width = 120;
                                }
                                else
                                {
                                    pb_start.Height = 30;
                                    pb_start.Width = 80;
                                }
                                pb_start.Click += pbstart_Click;
                                #endregion

                                #region Ready Button
                                PictureBox pb_ready = new PictureBox()
                                {
                                    Name = "pb_ready_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Image = Properties.Resources.ready_button,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Dock = DockStyle.None,
                                    Tag = gridSourcedetails[0].Field17,
                                    AccessibleDescription = orders.Field1,

                                };
                                if (screenWidth > 1367 && screenHeight > 769)
                                {
                                    pb_ready.Height = 40;
                                    pb_ready.Width = 120;
                                }
                                else
                                {
                                    pb_ready.Height = 30;
                                    pb_ready.Width = 80;
                                }
                                pb_ready.Click += pbreadyfood_Click;
                                #endregion

                                #region Cancel Button
                                PictureBox pb_cancelfood = new PictureBox()
                                {
                                    Name = "pictureBoxRemove_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Image = Properties.Resources.Kitchen_Ordercancel,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Dock = DockStyle.None,
                                    Tag = gridSourcedetails[0].Field17,
                                    AccessibleDescription = orders.Field1,
                                };
                                if (screenWidth > 1367 && screenHeight > 769)
                                {
                                    pb_cancelfood.Height = 40;
                                    pb_cancelfood.Width = 120;
                                }
                                else
                                {
                                    pb_cancelfood.Height = 30;
                                    pb_cancelfood.Width = 80;
                                }
                                pb_cancelfood.Click += pbcancelfood_Click;
                                #endregion

                                #region Reprint Button
                                PictureBox reprintbutton = new PictureBox()
                                {
                                    Name = "reprintbutton_k_" + orders.Field1 + gridSourcedetails[0].Field17,
                                    Image = Properties.Resources.reprint,
                                    SizeMode = PictureBoxSizeMode.StretchImage,
                                    Dock = DockStyle.None,
                                    Tag = gridSourcedetails[0].Field17,
                                    AccessibleDescription = orders.Field1,
                                };


                                if (screenWidth > 1367 && screenHeight > 769)
                                {
                                    reprintbutton.Height = 40;
                                    reprintbutton.Width = 120;
                                }
                                else
                                {
                                    reprintbutton.Height = 30;
                                    reprintbutton.Width = 80;
                                }
                                reprintbutton.Click += reprintbutton_Click;
                                #endregion

                                if (flagstart_ready)
                                {
                                    pb_start.Visible = true;
                                    pb_cancelfood.Visible = true;
                                    pb_ready.Visible = false;
                                }
                                else
                                {
                                    pb_start.Visible = false;
                                    pb_ready.Visible = true;
                                    pb_cancelfood.Visible = false;
                                }

                                tlp_buttons.Controls.Add(pb_start, 0, 0);
                                tlp_buttons.Controls.Add(pb_ready, 1, 0);
                                tlp_buttons.Controls.Add(pb_cancelfood, 2, 0);
                                tlp_buttons.Controls.Add(reprintbutton, 3, 0);

                                // Adding Buttons TableLayoutPanel to the last row
                                tlp_product_box.Controls.Add(tlp_buttons, 0, 3);
                                #endregion

                                if (Convert.ToInt32(gridSourcedetails[0].Field27) == 1)
                                {
                                    pb_start.Visible = false;
                                    pb_ready.Visible = false;
                                    pb_cancelfood.Visible = false;
                                    reprintbutton.Visible = false;
                                    if (reprintbutton != null)
                                    {
                                        txt_Description.Visible = true;
                                        txt_Description.Text = "Waiting for customer confirmation.";
                                        tlp_product_box.RowStyles[2] = new RowStyle(SizeType.AutoSize);
                                    }
                                }
                                else
                                {
                                    if (flagstart_ready)
                                    {
                                        pb_start.Visible = true;
                                        pb_cancelfood.Visible = true;
                                        pb_ready.Visible = false;
                                    }
                                    else
                                    {
                                        pb_start.Visible = false;
                                        pb_ready.Visible = true;
                                        pb_cancelfood.Visible = false;
                                    }
                                }

                                flp_order_view.Controls.Add(tlp_product_box);
                                orderbox.Add(new datagrid
                                {
                                    Id = Convert.ToInt32(gridSourcedetails[0].Field8),
                                    datagridviewname = dgv_product_details.Name,
                                    createtime = gridSourcedetails[0].Fielddatetime
                                });

                                tlp_product_box.ResumeLayout();
                                tlp_product_box.Visible = true;
                                flp_order_view.ResumeLayout();
                            }
                            rowCountDisplay++;
                        }
                    }

                    //timer1.Start();
                    //refreshtimer.Start();
                }
                else
                {
                    btntotalorders.Text = "0";
                    timer1.Stop();
                    foreach (Control ctrl in flp_order_view.Controls)
                    {
                        ctrl.Dispose();
                    }

                    flp_order_view.Controls.Clear();
                    // pnl_empty_msg.Location = new Point(this.ClientSize.Width / 2 - pnl_empty_msg.Size.Width / 2, this.ClientSize.Height / 2 - pnl_empty_msg.Size.Height / 2);
                }

                SetEmptyIcon();

                #endregion
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View Page SerachTable_Pickup()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }

        private void cmbserachtype_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtsearchData.Text = GetPlaceholder();
            txtsearchData.ForeColor = Color.DarkGray;
        }
        public void SetEmptyIcon()
        {
            try
            {
                bool anyVisible =Convert.ToInt32(btntotalorders.Text) > 0 ? true : false;
                if (!anyVisible)
                {
                    pnl_empty_msg.Anchor = AnchorStyles.None;

                    int x = (this.Width - pnl_empty_msg.Width) / 2;
                    int y = (this.Height - pnl_empty_msg.Height) / 2;
                    pnl_empty_msg.Location = new Point(x, y);

                    pnl_empty_msg.Visible = true;
                    pnl_empty_msg.BringToFront();

                    //btn_prevbill.PerformClick();
                }
                else
                {
                    pnl_empty_msg.Visible = false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Kitchen_View Page SetEmptyIcon()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Kitchen_View));
            }
        }
    }
}
