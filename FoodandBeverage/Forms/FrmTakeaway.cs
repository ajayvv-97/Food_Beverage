using System;
using System.Drawing;
using System.Windows.Forms;
using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Interfaces;
using FoodandBeverage.Classes;
using FoodandBeverage.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace FoodandBeverage.Forms
{
    public partial class FrmTakeaway : Form
    {
        #region Declaration 

        public int panelsPerRow = 0;
        public int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        public int Searchmode = 0;
        public List<DmnNameList> CheckedtableNames = new List<DmnNameList>();
        ClsGeneral clsgen = new ClsGeneral();

        BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
        DmnSearch dmnSearch = new DmnSearch();

        public Color Defaultfontcolor = Color.Black;
        public Color Ordertablefontcolor = Color.Green;
        public Color Netamountfontcolor = Color.Green;
        public Color CancelProduct_order = ColorTranslator.FromHtml("#FFB3B3");
        public Color BillingColor = Color.White;

        int Pagecount = 60;// Convert.ToInt32(ConfigurationManager.AppSettings["FBpagescount"]);
        int currentPage = 1;
        int pageSize = 0;
        int totalPages = 0;
        private List<DmnGridFields> TableData;
        private List<DmnGridFields> pagedatadata;
        private Dictionary<string, (ModernPickupCard Card, int Status)> TakeawayCache = new Dictionary<string, (ModernPickupCard, int)>();

        #endregion
        public FrmTakeaway()
        {
            InitializeComponent();
            Searchmode = 1;
            AutoTimer.Tick -= AutoTimer_Tick;
            AutoTimer.Tick += AutoTimer_Tick;
        }

        #region Deafult Event and Fuction
        private void FrmTakeaway_Load(object sender, System.EventArgs e)
        {
            pageSize = Pagecount;
            if (FrmDashboard.AutoLogoutWhenIdle)
            {
                //AppSession.RegisterForm(this, (int)LogoutFormList.Home);
            }
            ConfigurePickupCardHost();
            LoadTakeawayView(Searchmode);
            ShowEmptyOrderIcon();
            textb_shape();
            txtPkid_search.Text = "Takeaway Token No ....";
            txtPkid_search.ForeColor = Color.DarkGray;
        }
       

        private void ConfigurePickupCardHost()
        {
            flp_pickup_view.Dock = DockStyle.Fill;
            flp_pickup_view.AutoScroll = true;
            flp_pickup_view.WrapContents = true;
            flp_pickup_view.FlowDirection = FlowDirection.LeftToRight;
            flp_pickup_view.Resize -= flp_pickup_view_ResponsiveResize;
            flp_pickup_view.Resize += flp_pickup_view_ResponsiveResize;
            ResizePickupCards();
        }

        private void flp_pickup_view_ResponsiveResize(object sender, EventArgs e)
        {
            ResizePickupCards();
        }

        private void ResizePickupCards()
        {
            if (flp_pickup_view.ClientSize.Width <= 0)
                return;

            int availableWidth = Math.Max(132, flp_pickup_view.ClientSize.Width -
                flp_pickup_view.Padding.Horizontal - SystemInformation.VerticalScrollBarWidth - 8);
            int columns = availableWidth >= 1100 ? 5 : availableWidth >= 850 ? 4 :
                availableWidth >= 620 ? 3 : availableWidth >= 400 ? 2 : 1;
            int cardWidth = Math.Max(132, (availableWidth - columns * 18) / columns);
            int cardHeight = Math.Max(95, Math.Min(120, (int)(cardWidth * 0.78)));

            foreach (Control control in flp_pickup_view.Controls)
            {
                if (control is ModernPickupCard)
                {
                    control.Size = new Size(cardWidth, cardHeight);
                    control.Margin = new Padding(5, 5, 5, 5);
                }
            }
        }

        private void ShowEmptyOrderIcon()
        {
            bool hasRealContent = flp_pickup_view.Controls.Count > 0;

            if (!hasRealContent)
            {
                pbempty.Visible = true;
                pbempty.BringToFront();
                pbempty.SizeMode = PictureBoxSizeMode.Zoom;
                pbempty.Size = new Size(150, 150);
                pbempty.Anchor = AnchorStyles.None;
                int x = (flp_pickup_view.Width - pbempty.Width) / 2;
                int y = (flp_pickup_view.Height - pbempty.Height) / 2;
                pbempty.Location = new Point(x, y);
            }
            else
            {
                pbempty.Visible = false;
            }
        }

        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                foreach (TextBox textBox in new TextBox[] { txtPkid_search })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("TableView page  textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
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
                    if (textBox.Region != null)
                    {
                        textBox.Region.Dispose();
                    }

                    textBox.Region = new Region(path);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("TableView page  ApplyRoundedTextBox()", ex,FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }
        #endregion

        #endregion

        #region Creation & updation pickup
        public class DoubleBufferedFlowLayoutPanel : FlowLayoutPanel
        {
            public DoubleBufferedFlowLayoutPanel()
            {
                this.DoubleBuffered = true;
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                this.UpdateStyles();
            }
        }
        private List<DmnGridFields> GetTakeawayOrderData(string tokenNumber = null)
        {
            try
            {
                string whereClause = "WHERE ot.OrderTypeId = 2 ";

                if (string.IsNullOrWhiteSpace(tokenNumber))
                {
                    whereClause +=
                        "AND ((om.Status IN (1,2) AND om.IsCustomerInformedCancel IS NULL) " +
                        "OR (om.Status = 0 AND om.IsCustomerInformedCancel = 1))";
                }
                else
                {
                    string safeToken = tokenNumber.Trim().ToUpperInvariant().Replace("'", "''");

                    if (safeToken.All(char.IsDigit))
                    {
                        whereClause +=
                            "AND (UPPER(LTRIM(RTRIM(ot.TokenDisplay))) = '" + safeToken + "' " +
                            "OR TRY_CONVERT(INT, REPLACE(REPLACE(UPPER(LTRIM(RTRIM(ot.TokenDisplay))), 'T', ''), '-', '')) = " +
                            "TRY_CONVERT(INT, '" + safeToken + "')) " +
                            "AND om.Status IN (1,2)";
                    }
                    else
                    {
                        whereClause +=
                            "AND UPPER(LTRIM(RTRIM(ot.TokenDisplay))) = '" + safeToken + "' " +
                            "AND om.Status IN (1,2)";
                    }
                }

                BllSearch<DmnGridFields> search = new BllSearch<DmnGridFields>();
                DmnSearch query = new DmnSearch
                {
                    Select =
                        "om.PkId AS Field1, ISNULL(om.PickupOrderFlag, 0) AS Field2, " +
                        "om.OrderMasterId AS Field3, om.NetAmount AS Field4, " +
                        "om.KitchenNumber AS Field5, " +
                        "ISNULL(NULLIF(LTRIM(RTRIM(ot.TokenDisplay)), ''), '') AS Field6, " +
                        "om.IsCustomerInformedCancel AS Field7",
                    Table =
                        "dbo.tblOrderMaster AS om INNER JOIN dbo.tblOrderToken AS ot " +
                        "ON ot.OrderMasterId = om.OrderMasterId AND ot.OrderTypeId = 2",
                    WhereNdGroupBy = whereClause,
                    OrderBy = "ORDER BY om.OrderMasterId DESC"
                };

                List<DmnGridFields> result = search.GetSearchData("prdSearch", query);
                return result ?? new List<DmnGridFields>();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Takeaway page GetTakeawayOrderData()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Pickuppage));
                return new List<DmnGridFields>();
            }
        }

        public void LoadTakeawayView(int searchMode)
        {
            try
            {
                if (pageSize <= 0)
                    pageSize = Pagecount;

                TableData = GetTakeawayOrderData();
                totalPages = (int)Math.Ceiling((double)TableData.Count / pageSize);

                if (totalPages == 0)
                    currentPage = 1;
                else if (currentPage > totalPages)
                    currentPage = totalPages;

                pagedatadata = TableData
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {TableData.Count}";
                btnprev.Enabled = currentPage > 1;
                btnnext.Enabled = currentPage < totalPages;

                LoadPagedataOptimized();

                if (!AutoTimer.Enabled)
                    AutoTimer.Start();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Takeaway page LoadTakeawayView()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Pickuppage));
            }
        }
        private ModernPickupCard CreateTakeawayCard(DmnGridFields item)
        {
            try
            {
                ModernPickupCard card = new ModernPickupCard
                {
                    Name = "tlpboxtakeaway_add_" + Convert.ToString(item.Field3),
                    Tag = item.Field3,
                    Size = new Size(150, 110),
                    MinimumSize = new Size(132, 95),
                    Margin = new Padding(5, 5, 5, 5),
                    AccessibleDescription = Convert.ToString(item.Field1),
                    AccessibleName = "0",
                    Visible = false
                };

                card.Click += TakeawayboxClickEventHandler;
                return card;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Takeawaypage CreateTakeawayCard()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Pickuppage));
                return null;
            }
        }

        private void UpdateTakeawayCard(ModernPickupCard card, DmnGridFields item, bool useSourceKitchenCancelFlag = true)
        {
            try
            {
                if (card == null || item == null)
                    return;

                BllSearch<DmnGridFields> bllSearch_order = new BllSearch<DmnGridFields>();
                DmnSearch statusQuery = new DmnSearch
                {
                    Select =
                        "CASE WHEN SUM(CASE WHEN Status <> 1 THEN 1 ELSE 0 END) > 0 THEN 1 ELSE 0 END AS Field1," +
                        "SUM(CASE WHEN Status = 0 AND AddonStatus IN(1,0) AND CancelNotifyStatus = 1 THEN 1 ELSE 0 END) AS Field2",
                    Table = "tblOrderDetails",
                    WhereNdGroupBy = "WHERE AddonStatus in (1,0) AND OrderMasterId=" + item.Field3
                };

                var grid = bllSearch_order.GetSearchData("prdSearch", statusQuery);
                int orderStatus = grid != null && grid.Count > 0 ? Convert.ToInt32(grid[0].Field1) : 0;
                int cancelCount = grid != null && grid.Count > 0 ? Convert.ToInt32(grid[0].Field2) : 0;
                bool cancelledByKitchen = useSourceKitchenCancelFlag && Convert.ToInt32(item.Field7) == 1;

                card.Tag = item.Field3;
                card.AccessibleDescription = Convert.ToString(item.Field1);
                card.AccessibleName = cancelledByKitchen ? "1" : "0";
                card.SetPickupData(
                    Convert.ToString(item.Field6),
                    orderStatus,
                    cancelCount,
                    cancelledByKitchen,
                    "TAKEAWAY");

                card.Name = "tlpboxtakeaway_add_" + Convert.ToString(item.Field3);
            }
            catch (Exception ex)
            {
                clsgen.LogException("Takeawaypage UpdateTakeawayCard()", ex, FrmLogin.EmployeName,
                    FrmLogin.Designation, Convert.ToInt32(FormType.Pickuppage));
            }
        }

        private void DisplayTakeawaySearchResults(List<DmnGridFields> results)
        {
            flp_pickup_view.SuspendLayout();

            try
            {
                foreach (Control control in flp_pickup_view.Controls.Cast<Control>().ToList())
                {
                    if (control is ModernPickupCard ||
                        control.Name.StartsWith("tlpboxtakeaway_add_", StringComparison.Ordinal))
                    {
                        flp_pickup_view.Controls.Remove(control);
                        control.Dispose();
                    }
                }

                TakeawayCache.Clear();

                foreach (DmnGridFields item in results ?? new List<DmnGridFields>())
                {
                    ModernPickupCard card = CreateTakeawayCard(item);
                    if (card == null)
                        continue;

                    UpdateTakeawayCard(card, item, false);
                    card.Visible = true;

                    int sourceStatus;
                    int.TryParse(Convert.ToString(item.Field7), out sourceStatus);
                    TakeawayCache[Convert.ToString(item.Field3)] = (card, sourceStatus);
                    flp_pickup_view.Controls.Add(card);
                }
            }
            finally
            {
                flp_pickup_view.ResumeLayout(true);
            }
        }

        public void UpdateTakeawayView(int searchMode)
        {
            try
            {
                AutoTimer.Stop();
                ShowEmptyOrderIcon();

                TableData = GetTakeawayOrderData();
                totalPages = (int)Math.Ceiling((double)TableData.Count / pageSize);

                if (totalPages == 0)
                    currentPage = 1;
                else if (currentPage > totalPages)
                    currentPage = totalPages;

                pagedatadata = TableData
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {TableData.Count}";
                btnprev.Enabled = currentPage > 1;
                btnnext.Enabled = currentPage < totalPages;

                LoadPagedataOptimized();
                if (!AutoTimer.Enabled)
                    AutoTimer.Start();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Takeaway page UpdateTakeawayView()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Pickuppage));
            }
        }

        private void LoadPagedataOptimized()
        {
            try
            {
                flp_pickup_view.SuspendLayout();

                HashSet<string> validNames = pagedatadata
                    .Select(x => "tlpboxtakeaway_add_" + x.Field3)
                    .ToHashSet();

                #region Remove completed order
                // If NO active orders in paged data → remove ALL pickup controls
                if (pagedatadata.Count == 0)
                {
                    foreach (Control ctrl in flp_pickup_view.Controls.Cast<Control>().ToList())
                    {
                        flp_pickup_view.Controls.Remove(ctrl);
                        ctrl.Dispose();
                    }

                    TakeawayCache.Clear();
                }
                else
                {
                    // Normal mode: remove only those not found in pagedatadata
                    var validIds = pagedatadata.Select(x => x.Field3).ToHashSet();
                    foreach (Control ctrl in flp_pickup_view.Controls.Cast<Control>().ToList())
                    {
                        if (ctrl.Name.StartsWith("tlpboxtakeaway_add_"))
                        {
                            string orderId = ctrl.Tag?.ToString();
                            if (!validIds.Contains(orderId))
                            {
                                flp_pickup_view.Controls.Remove(ctrl);
                                ctrl.Dispose();

                                if (TakeawayCache.ContainsKey(orderId))
                                    TakeawayCache.Remove(orderId);
                            }
                        }
                    }
                }

                #endregion

                foreach (var item in pagedatadata)
                {
                    (ModernPickupCard Card, int Status) cacheItem;
                    int status = Convert.ToInt32(item.Field7);

                    if (TakeawayCache.TryGetValue(item.Field3, out cacheItem))
                    {
                        UpdateTakeawayCard(cacheItem.Card, item);
                        TakeawayCache[item.Field3] = (cacheItem.Card, status);
                    }
                    else
                    { 
                        var card = CreateTakeawayCard(item);
                        if (card == null)
                            continue;
                        TakeawayCache[item.Field3] = (card, status);
                        UpdateTakeawayCard(card, item);
                        flp_pickup_view.Controls.Add(card);
                    }
                    TakeawayCache[item.Field3].Card.Visible = true;
                }

                flp_pickup_view.ResumeLayout(true);
                ResizePickupCards();
                ShowEmptyOrderIcon();
                if (!AutoTimer.Enabled) AutoTimer.Start();
            }
            catch (Exception ex)
            {
                clsgen.LogException("LoadPagedataOptimized()", ex, FrmLogin.EmployeName,FrmLogin.Designation, Convert.ToInt32(FormType.Pickuppage));
            }
        }


        private (int status, int cancelCount) CheckOrderStatus(string orderId)
        {
            BllSearch<DmnGridFields> bll = new BllSearch<DmnGridFields>();

            DmnSearch dmn = new DmnSearch
            {
                Select = "CASE WHEN SUM(CASE WHEN Status<>1 THEN 1 ELSE 0 END) > 0 THEN 1 ELSE 0 END AS Field1, " +
                         "SUM(CASE WHEN Status = 0 AND AddonStatus IN(1, 0) AND CancelNotifyStatus = 1 THEN 1 ELSE 0 END) AS Field2",
                Table = "tblOrderDetails",
                WhereNdGroupBy = "WHERE AddonStatus IN (1,0) AND OrderMasterId=" + orderId
            };

            var r = bll.GetSearchData("prdSearch", dmn);

            return (Convert.ToInt32(r[0].Field1), Convert.ToInt32(r[0].Field2));
        }


        #endregion

        #region Updation pickup


        #endregion

        private void TakeawayboxClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                if (clickedControl == null)
                    return;

                AutoTimer.Stop();

                int cancelOrderStatus;
                if (!int.TryParse(clickedControl.AccessibleName, out cancelOrderStatus))
                    cancelOrderStatus = 0;

                FrmOrderConsole existingOrderConsole =
                    System.Windows.Forms.Application.OpenForms["FrmOrderConsole"] as FrmOrderConsole;
                if (existingOrderConsole != null)
                {
                    if (existingOrderConsole.WindowState == FormWindowState.Minimized)
                        existingOrderConsole.WindowState = FormWindowState.Maximized;

                    existingOrderConsole.BringToFront();
                    existingOrderConsole.Activate();
                    return;
                }

                if (clickedControl.Tag != null)
                {
                    using (FrmOrderConsole orderConsole = new FrmOrderConsole(
                        0,
                        Convert.ToInt32(FrmOrderConsole.TakeawayBookingType),
                        0,
                        CheckedtableNames,
                        Convert.ToInt32(PageType.Billpage),
                        Convert.ToInt32(clickedControl.Tag),
                        Convert.ToInt32(SourcepageType.Takeawaypage),
                        cancelOrderStatus,
                        2))
                    {
                        orderConsole.StartPosition = FormStartPosition.CenterScreen;
                        orderConsole.WindowState = FormWindowState.Maximized;
                        orderConsole.ShowDialog(this);
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Takeawaypage page TakeawayboxClickEventHandler()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Pickuppage));
            }
        }

        private void btnaddnew_Click(object sender, EventArgs e)
        {
            try
            {
                clsgen.Memoryrelease();
                AutoTimer.Stop();
                CheckedtableNames.Clear();
                Control clickedControl = sender as Control;
                FrmOrderConsole existingOrderConsole =
                    System.Windows.Forms.Application.OpenForms["FrmOrderConsole"] as FrmOrderConsole;
                if (existingOrderConsole != null)
                {
                    if (existingOrderConsole.WindowState == FormWindowState.Minimized)
                        existingOrderConsole.WindowState = FormWindowState.Maximized;

                    existingOrderConsole.BringToFront();
                    existingOrderConsole.Activate();
                }
                else
                {
                    using (FrmOrderConsole frm = new FrmOrderConsole(
                        0, Convert.ToInt32(FrmOrderConsole.TakeawayBookingType), 0,
                        CheckedtableNames, Convert.ToInt32(PageType.Billpage),
                        0, Convert.ToInt32(SourcepageType.Takeawaypage), 0, 2))
                    {
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.WindowState = FormWindowState.Maximized;
                        frm.ShowDialog(this);
                    }
                }
                clsgen.Memoryrelease();
            }
            catch (Exception ex)
            {
                clsgen.LogException("TableView page  Button_pickupClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.TableView));
            }
        }
        private void AutoTimer_Tick(object sender, EventArgs e)
        {
            Process currentProcess = Process.GetCurrentProcess();
            long memoryInBytes = currentProcess.PrivateMemorySize64;
            double memoryInMB = memoryInBytes / (1024.0 * 1024.0);
            lblmemory.Text = $"Memory: {memoryInMB:F2} MB";

            UpdateTakeawayView(Searchmode);
        }
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            currentPage = 1;
            txtPkid_search.Text = "Takeaway Token No ....";
            txtPkid_search.ForeColor = Color.DarkGray;
            LoadTakeawayView(Convert.ToInt32(Food_Status.Pending));
            clsgen.Memoryrelease();
        }
        private void FrmTakeaway_Resize(object sender, EventArgs e)
        {
            textb_shape();
        }  
        private void SearchTakeawayByToken()
        {
            try
            {
                string tokenNumber = (txtPkid_search.Text ?? string.Empty).Trim();
                if (string.IsNullOrWhiteSpace(tokenNumber) || tokenNumber == "Takeaway Token No ....")
                {
                    CMessageBox.Show("Please Enter A Takeaway Token Number.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPkid_search.Focus();
                    return;
                }

                AutoTimer.Stop();

                TableData = GetTakeawayOrderData(tokenNumber);
                if (TableData != null && TableData.Count > 0)
                {
                    lblpagerinfo.Text =
                        $"Page {currentPage} of {totalPages} | Total Records: {TableData.Count}";
                    DisplayTakeawaySearchResults(TableData);
                }
                else
                {
                    CMessageBox.Show("Please Enter A Valid Takeaway Token Number.", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    if (!AutoTimer.Enabled)
                        AutoTimer.Start();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Takeaway page SearchTakeawayByToken()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Pickuppage));
            }
        }

        private void btnsearch_Click(object sender, EventArgs e)
        {
            SearchTakeawayByToken();
        }
        private void txtPkid_search_Leave(object sender, EventArgs e)
        {
            if (txtPkid_search.Text == "")
            {
                txtPkid_search.Text = "Takeaway Token No ....";
                txtPkid_search.ForeColor = Color.DarkGray;
            }
        }
        private void txtPkid_search_Enter(object sender, EventArgs e)
        {
            if (txtPkid_search.Text == "Takeaway Token No ....")
            {
                txtPkid_search.Text = "";
                txtPkid_search.ForeColor = Color.Black;
            }
        }
        private void txtPkid_search_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchTakeawayByToken();
            }
        }
        private void txtPkid_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.Handled = true;
                return;
            }

            if (!char.IsControl(e.KeyChar) &&
                !char.IsLetterOrDigit(e.KeyChar) &&
                e.KeyChar != '-')
            {
                e.Handled = true;
                return;
            }

            if (char.IsLetter(e.KeyChar))
                e.KeyChar = char.ToUpperInvariant(e.KeyChar);
        }

        private void FrmTakeaway_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FrmDashboard.AutoLogoutWhenIdle)
            {
                AppSession.UnregisterForm(this);
            }
        }

        private void btn_cancle_pickup_Click(object sender, EventArgs e)
        {
            //lblcanpickupcount,btn_cancle_pickup
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
            if (System.Windows.Forms.Application.OpenForms["FrmTakeaway"] != null)
            {
                FrmTakeaway _instanceFrmTakeaway = Application.OpenForms["FrmTakeaway"] as FrmTakeaway;
                _instanceFrmTakeaway.Visible = false;
            }
            if (System.Windows.Forms.Application.OpenForms["FrmTableView"] != null)
            {
                FrmTableView _instanceFrmTableView = Application.OpenForms["FrmTableView"] as FrmTableView;
                _instanceFrmTableView.Visible = true;
                _instanceFrmTableView.LoadOrderStatusCache();
                _instanceFrmTableView.UpdateTableUI();
            }
        }
        public void ResetTakeawayViewPage()
        {
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

        private void btnclose_Click(object sender, EventArgs e)
        {
            ResetTableViewPage();
            FrmDashboard.Instance.Menutable.PerformClick();
            this.Close();
        }

        private void btn_backCompleorder_Click(object sender, EventArgs e)
        {
            long mem = Process.GetCurrentProcess().PrivateMemorySize64 / (1024 * 1024); // MB

            if (mem > 1000)
            {
                this.Close();
                FrmDashboard.Instance.StartSyncAnimation();
            }
            else
            {
                FrmDashboard.Instance.Menubilling.PerformClick();
                this.Close();
            }
        }


        #region Next Previous Button
        private void btnprev_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPage > 1) // <-- correct check
                {
                    AutoTimer.Stop();
                    currentPage--;
                    LoadTakeawayView(0);
                    //pagedatadata = TableData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                    //lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {TableData.Count}";
                    btnprev.Enabled = currentPage > 1;
                    btnnext.Enabled = currentPage < totalPages;

                    //UpdatePagerInfo(); // <-- ensure pager label updates
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Takeawaypage page btnprev_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Pickuppage));
            }
        }

        private void btnnext_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPage < totalPages)
                {
                    AutoTimer.Stop();
                    currentPage++;
                    LoadTakeawayView(0);
                    //pagedatadata = TableData.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                    //lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {TableData.Count}";
                    btnprev.Enabled = currentPage > 1;
                    btnnext.Enabled = currentPage < totalPages;
                    //UpdatePagerInfo(); // <-- update pager label
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Takeawaypage page btnnext_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Pickuppage));
            }
        }
        #endregion
    }
}
