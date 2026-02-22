using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CustomMessageBox;
using DocumentFormat.OpenXml.Drawing.Charts;
using Domain;
using FastMember;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControl;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Size = System.Drawing.Size;


namespace FoodandBeverage.Forms
{
    public partial class FrmOrderDesignCustom : Form
    {
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
        private ToolTip sideMenuToolTip;

        public string GameDB = Convert.ToString(ConfigurationManager.AppSettings["GameDB"]);
        public int columncount = Convert.ToInt32(ConfigurationManager.AppSettings["OrderPageProductViewRow"]);
        public decimal Productheight = Convert.ToDecimal(ConfigurationManager.AppSettings["Productheight"]);
        public decimal Productwidth = Convert.ToDecimal(ConfigurationManager.AppSettings["Productheight"]);
        public int Productspace = Convert.ToInt32(ConfigurationManager.AppSettings["Productspace"]);

        public int columncount_offer = Convert.ToInt32(ConfigurationManager.AppSettings["OrderPageProductViewRow_Offer"]);


        public FrmOrderDesignCustom()
        {
            InitializeComponent();
            f11 = AppFonts.GetFont(11);
            f9 = AppFonts.GetFont(9);
            f10 = AppFonts.GetFont(10);
            f10Strike = new Font(f10, FontStyle.Strikeout);
            f10Bold = new Font(f10, FontStyle.Bold);
        }
        private void FrmOrderDesignCustom_Load(object sender, EventArgs e)
        {
            try
            {
                pageSize = Pagecount;
                string width = ConfigurationManager.AppSettings["Productwidth"];
                string height = ConfigurationManager.AppSettings["Productheight"];

                // Set default if null/empty
                if (string.IsNullOrEmpty(width))
                    width = "120";

                if (string.IsNullOrEmpty(height))
                    height = "120";

                txtwidth.Text = width;
                txtheight.Text = height;

                // Also update runtime variables
                int.TryParse(width, out tileW);
                int.TryParse(height, out tileH);

                // Apply immediately to UI
                UpdateTileSize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #region side menu
        public void Memoryrelease()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
        private void btn_showmenu_Click(object sender, EventArgs e)
        {
            Memoryrelease();

            flp_sidemenu.Visible = !flp_sidemenu.Visible;

            if (!flp_sidemenu.Visible)
                return;

            // Setup menu panel
            flp_sidemenu.FlowDirection = FlowDirection.LeftToRight;
            flp_sidemenu.Width = btn_showmenu.Width + (btn_showmenu.Width / 4);
            flp_sidemenu.Height = flp_productview.Height / 2;

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
     "DISTINCT p.Product as Field1,p.ProductId as Field2,0 as Field3,fav_status as Field4,p.price Field5,q.Discountper Field6,q.FinalPrice Field7";

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
                "AND m.Status=1 " +
                "AND (p.IsStockTracked = 0 OR m.Quantity > 0 or m.Status=1 ) " +

                " Union all Select i.Itemname as Field1,i.ItemId as Field2,i.Saletype as Field3,i.fav_status as Field4,(SELECT MAX(SRate) FROM tblProductRate WHERE ProductId = i.ItemId) AS Field5,q.Discountper Field6,q.FinalPrice Field7 from " +
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
                    tile.BackColor = Color.White;
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
                    lblName.ForeColor = Color.Black;
                    lblName.BackColor = Color.Transparent;
                    lblName.AutoEllipsis = true;

                    // Font adjust based on length
                    Size textSize = TextRenderer.MeasureText(lblName.Text, f11);
                    lblName.Font = (textSize.Width <= tileW - 10) ? f11 : f9;

                    tlpMain.Controls.Add(lblName, 0, 0);



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

                    UIHelper.RoundCorners(tile, 20, null);

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

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmOrderDesignCustom_Resize(object sender, EventArgs e)
        {
            maxColumns = columncount;
            spacing = Productspace;
            containerWidth = flp_productview.ClientSize.Width;
            tileW = (int)(Productwidth);
            tileH = (int)(Productheight);
        }

        private void btn_custom_menu_Click(object sender, EventArgs e)
        {
            try
            {
                string productName = txtproductname.Text.Trim();

                if (string.IsNullOrEmpty(productName))
                {
                    MessageBox.Show("Enter product name");
                    return;
                }

                int productId = 0;   // test id
                int favStatus = 0;   // default not favorite
                int saleTypeNo = 1;  // test
                int foodType = 1;    // 1=Veg, 2=NonVeg

                CreateCustomTile(productName, productId, favStatus, saleTypeNo, foodType);
            }
            catch (Exception ex)
            {
                clsgen.LogException("btn_custom_menu_Click", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order));
            }
        }
        private void CreateCustomTile(string productName, int productId, int favStatus, int saleTypeNo, int foodType)
        {
            try
            {
                int tileW = Convert.ToInt32(txtwidth.Text);
                int tileH = Convert.ToInt32(txtheight.Text);

                Panel tile = new Panel();
                tile.Width = tileW;
                tile.Height = tileH;
                tile.BackColor = Color.White;
                tile.Margin = new Padding(spacing);
                tile.Tag = productId;
                tile.AccessibleDescription = saleTypeNo.ToString();

                // ---------- MAIN LAYOUT ----------
                TableLayoutPanel tlpMain = new TableLayoutPanel();
                tlpMain.Dock = DockStyle.Fill;
                tlpMain.RowCount = 1;
                tlpMain.ColumnCount = 1;

                tlpMain.RowStyles.Clear();
                tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

                // ---------- PRODUCT NAME ----------
                Label lblName = new Label();
                lblName.Text = productName;
                lblName.TextAlign = ContentAlignment.MiddleCenter;
                lblName.Dock = DockStyle.Fill;
                lblName.Padding = new Padding(5);
                lblName.ForeColor = Color.Black;
                lblName.AutoEllipsis = true;

                // Auto font adjust
                Size textSize = TextRenderer.MeasureText(lblName.Text, f11);
                lblName.Font = (textSize.Width <= tileW - 10) ? f11 : f9;

                tlpMain.Controls.Add(lblName, 0, 0);
                tile.Controls.Add(tlpMain);

                // ---------- FAVORITE ICON ----------
                PictureBox picFav = new PictureBox();
                picFav.Width = 22;
                picFav.Height = 22;
                picFav.SizeMode = PictureBoxSizeMode.StretchImage;
                picFav.Location = new Point(tileW - 30, 5);
                picFav.BackColor = Color.White;

                picFav.Image = (favStatus == 1)
                    ? Properties.Resources.fav_enable
                    : Properties.Resources.fav_disable;

                tile.Controls.Add(picFav);
                picFav.BringToFront();

                // ---------- SMALL 🟢 / 🔴 SQUARE ----------
                Panel pnlType = new Panel();
                pnlType.Width = 10;
                pnlType.Height = 10;
                pnlType.Location = new Point(5, 5);

                if (foodType == 1)
                    pnlType.BackColor = Color.Green;   // 🟢 Veg
                else if (foodType == 2)
                    pnlType.BackColor = Color.Red;     // 🔴 Non-Veg
                else
                    pnlType.BackColor = Color.Gray;

                tile.Controls.Add(pnlType);
                pnlType.BringToFront();

                // ---------- ADD TO FLOW ----------
                flp_productview.Controls.Add(tile);
            }
            catch (Exception ex)
            {
                clsgen.LogException("CreateCustomTile()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order));
            }
        }


        private void AllowOnlyDecimal(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;

            // Allow control keys (Backspace, Delete, etc.)
            if (char.IsControl(e.KeyChar))
                return;

            // Allow digits
            if (char.IsDigit(e.KeyChar))
                return;

            // Allow only ONE decimal point
            if (e.KeyChar == '.' && !txt.Text.Contains("."))
                return;

            // Block everything else
            e.Handled = true;
        }

        private void btnwidth_add_Click(object sender, EventArgs e)
        {
            int val = 0;
            int.TryParse(txtwidth.Text, out val);

            val += 1; // ➕ increase by 1
            txtwidth.Text = val.ToString();

            UpdateTileSize(); // 🔥 call common method
        }
        private void btnheightadd_Click(object sender, EventArgs e)
        {
            int val = 0;
            int.TryParse(txtheight.Text, out val);

            val += 1; // ➕ increase by 1
            txtheight.Text = val.ToString();

            UpdateTileSize(); // 🔥 call common method
        }

        private void txtwidth_TextChanged(object sender, EventArgs e)
        {
            UpdateTileSize();
        }

        private void txtheight_TextChanged(object sender, EventArgs e)
        {
            UpdateTileSize();
        }
        private void UpdateTileSize()
        {
            try
            {
                int.TryParse(txtwidth.Text, out tileW);
                int.TryParse(txtheight.Text, out tileH);

                // Optional limits (recommended)
                tileW = Math.Max(50, Math.Min(500, tileW));
                tileH = Math.Max(50, Math.Min(500, tileH));

                foreach (Control ctrl in flp_productview.Controls)
                {
                    if (ctrl is Panel tile)
                    {
                        tile.Width = tileW;
                        tile.Height = tileH;

                        // Update favorite icon position
                        foreach (Control inner in tile.Controls)
                        {
                            if (inner is PictureBox pic)
                            {
                                pic.Location = new Point(tileW - 30, 5);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("UpdateTileSize()", ex,
                    FrmLogin.EmployeName, FrmLogin.Designation,
                    Convert.ToInt32(FormType.Order));
            }
        }

        private void btnwidth_rem_Click(object sender, EventArgs e)
        {
            int val;

            if (!int.TryParse(txtwidth.Text, out val))
                val = 100;

            if (val > 1) // prevent 0 or negative
                val--;

            txtwidth.Text = val.ToString();

            UpdateTileSize();
        }
        private void btnheightrem_Click(object sender, EventArgs e)
        {
            int val;

            if (!int.TryParse(txtheight.Text, out val))
                val = 100;

            if (val > 1) // prevent 0 or negative
                val--;

            txtheight.Text = val.ToString();

            UpdateTileSize();
        }


        private void btnapplydesign_Click(object sender, EventArgs e)
        {
            try
            {
                string widthValue = txtwidth.Text.Trim();
                string heightValue = txtheight.Text.Trim();

                if (string.IsNullOrEmpty(widthValue) || string.IsNullOrEmpty(heightValue))
                {
                    MessageBox.Show("Width and Height required");
                    return;
                }

                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings["Productwidth"].Value = widthValue;
                config.AppSettings.Settings["Productheight"].Value = heightValue;

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                MessageBox.Show("Design settings saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
