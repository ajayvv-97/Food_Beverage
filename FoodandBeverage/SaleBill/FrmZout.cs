using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Domain;
using FastMember;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms;
using FoodandBeverage.Interfaces;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Xceed.Document.NET;

namespace FoodandBeverage.SaleBill
{
    public partial class FrmZout : Form
    {

        #region Declaration
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvout;
        public string RPTPath = Convert.ToString(FrmLogin.SaleBillPath);
        public int flag, BranchId, UserId;
        public int Drstaffid;
        public bool XYDalvalue = false, DalDalvalue = false, Enabledefgmail = false;
        private TableLayoutPanel tlp_btns;
        public Button btn_print;
        private Button btn_gmail;
        private Button btn_whatsup;
        private Button btn_cancel;
        private TableLayoutPanel tlp_main;

        public List<DmnGridFields> Addressdata;
        bool RPTLocalVw = false;

        public string XYZPath = FrmLogin.XYZPath;
        private List<string> selectedPrinters_TH = new List<string>();
        private List<string> selectedPrinters_A4 = new List<string>();

        public string printname = "";
        public string printfilename = "";
        string folderPath = "";
        string pdfFileName = "";
        string reportpdfPath = "";
        public string invoiceno_api;
        ReportDocument reportDocument = new ReportDocument();
        string DefaultGmail = "";

        public int PrintBillType;
        public int BillTaxType;
        public String FooterContent;
        public String CompanyNameOtherLanguage;
        public String CompanyAddressOtherLanguage;
        public String FooterOtherLanguage;
        public bool AutoPrint { get; set; }
        public bool Save_YZout { get; set; }
        public bool Save_Daily { get; set; }

        public String YZPath = "";
        private Panel pnlgmail;
        private TableLayoutPanel tlp_gmail_btn;
        private TextBox txt_gmail_report;
        private Button btngmailsend;
        private Button btngmailcancel;
        public String DailyPath = "";
        string Regaddress = "";
        #endregion

        #region Printer Default Settings
        [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string printerName);
        #endregion

        public FrmZout(int value, int Drawerstaffid)
        {
            flag = value;
            BranchId = Convert.ToInt32(FrmLogin.BranchId);
            UserId = Convert.ToInt32(FrmLogin.UserId); 
            Drstaffid = Drawerstaffid;
            InitializeComponent();

        }
        private void InitializeComponent()
        {
            this.crvout = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tlp_btns = new System.Windows.Forms.TableLayoutPanel();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_gmail = new System.Windows.Forms.Button();
            this.btn_whatsup = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.pnlgmail = new System.Windows.Forms.Panel();
            this.tlp_gmail_btn = new System.Windows.Forms.TableLayoutPanel();
            this.txt_gmail_report = new System.Windows.Forms.TextBox();
            this.btngmailsend = new System.Windows.Forms.Button();
            this.btngmailcancel = new System.Windows.Forms.Button();
            this.tlp_btns.SuspendLayout();
            this.tlp_main.SuspendLayout();
            this.pnlgmail.SuspendLayout();
            this.tlp_gmail_btn.SuspendLayout();
            this.SuspendLayout();
            // 
            // crvout
            // 
            this.crvout.ActiveViewIndex = -1;
            this.crvout.AutoScroll = true;
            this.crvout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvout.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvout.DisplayBackgroundEdge = false;
            this.crvout.DisplayStatusBar = false;
            this.crvout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvout.EnableRefresh = false;
            this.crvout.Location = new System.Drawing.Point(3, 3);
            this.crvout.Name = "crvout";
            this.crvout.ShowCloseButton = false;
            this.crvout.ShowCopyButton = false;
            this.crvout.ShowGotoPageButton = false;
            this.crvout.ShowGroupTreeButton = false;
            this.crvout.ShowLogo = false;
            this.crvout.ShowParameterPanelButton = false;
            this.crvout.ShowRefreshButton = false;
            this.crvout.ShowTextSearchButton = false;
            this.crvout.ShowZoomButton = false;
            this.crvout.Size = new System.Drawing.Size(515, 474);
            this.crvout.TabIndex = 54;
            this.crvout.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crvout.ToolPanelWidth = 10;
            // 
            // tlp_btns
            // 
            this.tlp_btns.ColumnCount = 6;
            this.tlp_btns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 52.55102F));
            this.tlp_btns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_btns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_btns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_btns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_btns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.44898F));
            this.tlp_btns.Controls.Add(this.btn_print, 1, 0);
            this.tlp_btns.Controls.Add(this.btn_gmail, 2, 0);
            this.tlp_btns.Controls.Add(this.btn_whatsup, 3, 0);
            this.tlp_btns.Controls.Add(this.btn_cancel, 4, 0);
            this.tlp_btns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_btns.Location = new System.Drawing.Point(2, 482);
            this.tlp_btns.Margin = new System.Windows.Forms.Padding(2);
            this.tlp_btns.Name = "tlp_btns";
            this.tlp_btns.RowCount = 1;
            this.tlp_btns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_btns.Size = new System.Drawing.Size(517, 48);
            this.tlp_btns.TabIndex = 500;
            // 
            // btn_print
            // 
            this.btn_print.BackgroundImage = global::FoodandBeverage.Properties.Resources.printer;
            this.btn_print.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_print.FlatAppearance.BorderSize = 0;
            this.btn_print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_print.Location = new System.Drawing.Point(112, 3);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(71, 41);
            this.btn_print.TabIndex = 54;
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // btn_gmail
            // 
            this.btn_gmail.BackgroundImage = global::FoodandBeverage.Properties.Resources.GMAIL;
            this.btn_gmail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_gmail.FlatAppearance.BorderSize = 0;
            this.btn_gmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_gmail.Location = new System.Drawing.Point(189, 3);
            this.btn_gmail.Name = "btn_gmail";
            this.btn_gmail.Size = new System.Drawing.Size(71, 41);
            this.btn_gmail.TabIndex = 56;
            this.btn_gmail.UseVisualStyleBackColor = true;
            this.btn_gmail.Click += new System.EventHandler(this.btn_gmail_Click);
            // 
            // btn_whatsup
            // 
            this.btn_whatsup.BackgroundImage = global::FoodandBeverage.Properties.Resources.WhatsApp;
            this.btn_whatsup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_whatsup.FlatAppearance.BorderSize = 0;
            this.btn_whatsup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_whatsup.Location = new System.Drawing.Point(266, 3);
            this.btn_whatsup.Name = "btn_whatsup";
            this.btn_whatsup.Size = new System.Drawing.Size(71, 41);
            this.btn_whatsup.TabIndex = 57;
            this.btn_whatsup.UseVisualStyleBackColor = true;
            this.btn_whatsup.Click += new System.EventHandler(this.btn_whatsup_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackgroundImage = global::FoodandBeverage.Properties.Resources.cancel_description;
            this.btn_cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Location = new System.Drawing.Point(343, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(71, 41);
            this.btn_cancel.TabIndex = 59;
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // tlp_main
            // 
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_main.Controls.Add(this.crvout, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_btns, 0, 1);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 2;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.32882F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.67118F));
            this.tlp_main.Size = new System.Drawing.Size(521, 532);
            this.tlp_main.TabIndex = 501;
            // 
            // pnlgmail
            // 
            this.pnlgmail.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlgmail.Controls.Add(this.tlp_gmail_btn);
            this.pnlgmail.Location = new System.Drawing.Point(11, 229);
            this.pnlgmail.Name = "pnlgmail";
            this.pnlgmail.Padding = new System.Windows.Forms.Padding(8);
            this.pnlgmail.Size = new System.Drawing.Size(500, 59);
            this.pnlgmail.TabIndex = 502;
            this.pnlgmail.Visible = false;
            // 
            // tlp_gmail_btn
            // 
            this.tlp_gmail_btn.ColumnCount = 3;
            this.tlp_gmail_btn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.28571F));
            this.tlp_gmail_btn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.71429F));
            this.tlp_gmail_btn.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49F));
            this.tlp_gmail_btn.Controls.Add(this.txt_gmail_report, 0, 0);
            this.tlp_gmail_btn.Controls.Add(this.btngmailsend, 1, 0);
            this.tlp_gmail_btn.Controls.Add(this.btngmailcancel, 2, 0);
            this.tlp_gmail_btn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_gmail_btn.Location = new System.Drawing.Point(8, 8);
            this.tlp_gmail_btn.Name = "tlp_gmail_btn";
            this.tlp_gmail_btn.RowCount = 1;
            this.tlp_gmail_btn.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_gmail_btn.Size = new System.Drawing.Size(484, 43);
            this.tlp_gmail_btn.TabIndex = 56;
            // 
            // txt_gmail_report
            // 
            this.txt_gmail_report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_gmail_report.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gmail_report.Location = new System.Drawing.Point(3, 3);
            this.txt_gmail_report.Multiline = true;
            this.txt_gmail_report.Name = "txt_gmail_report";
            this.txt_gmail_report.Size = new System.Drawing.Size(382, 37);
            this.txt_gmail_report.TabIndex = 55;
            // 
            // btngmailsend
            // 
            this.btngmailsend.BackgroundImage = global::FoodandBeverage.Properties.Resources.msgsend;
            this.btngmailsend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btngmailsend.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btngmailsend.FlatAppearance.BorderSize = 0;
            this.btngmailsend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btngmailsend.Location = new System.Drawing.Point(391, 3);
            this.btngmailsend.Name = "btngmailsend";
            this.btngmailsend.Size = new System.Drawing.Size(40, 37);
            this.btngmailsend.TabIndex = 56;
            this.btngmailsend.UseVisualStyleBackColor = true;
            this.btngmailsend.Click += new System.EventHandler(this.btngmailsend_Click);
            // 
            // btngmailcancel
            // 
            this.btngmailcancel.BackgroundImage = global::FoodandBeverage.Properties.Resources.msgcancel;
            this.btngmailcancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btngmailcancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btngmailcancel.FlatAppearance.BorderSize = 0;
            this.btngmailcancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btngmailcancel.Location = new System.Drawing.Point(437, 3);
            this.btngmailcancel.Name = "btngmailcancel";
            this.btngmailcancel.Size = new System.Drawing.Size(44, 37);
            this.btngmailcancel.TabIndex = 57;
            this.btngmailcancel.UseVisualStyleBackColor = true;
            this.btngmailcancel.Click += new System.EventHandler(this.btngmailcancel_Click);
            // 
            // FrmZout
            // 
            this.ClientSize = new System.Drawing.Size(521, 532);
            this.Controls.Add(this.pnlgmail);
            this.Controls.Add(this.tlp_main);
            this.Name = "FrmZout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "34";
            this.Load += new System.EventHandler(this.FrmZout_Load);
            this.tlp_btns.ResumeLayout(false);
            this.tlp_main.ResumeLayout(false);
            this.pnlgmail.ResumeLayout(false);
            this.tlp_gmail_btn.ResumeLayout(false);
            this.tlp_gmail_btn.PerformLayout();
            this.ResumeLayout(false);

        }
        ClsGeneral clsgen = new ClsGeneral();

        private void FrmZout_Load(object sender, EventArgs e)
        {
            SetPlaceholder(txt_gmail_report, "Enter Email Address");
            ApplyRoundedTextBox(txt_gmail_report, 10);
            txt_gmail_report.Font = new System.Drawing.Font(txt_gmail_report.Font.FontFamily, 12);
            txt_gmail_report.BorderStyle = System.Windows.Forms.BorderStyle.None;

            folderPath = Path.Combine(Application.StartupPath, "YZOutReport");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            folderPath = Path.Combine(Application.StartupPath, "SaleBill");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            Generalsettingcheck();
            if (XYDalvalue && DalDalvalue)
                PrintAll();
        }

        public void PrintAll()
        {
            try
            {
                Generalsettingcheck();
                if (flag == Convert.ToInt32(BillCategory.Zout))
                {
                    PrintZout();
                    if (XYDalvalue == false)
                        this.Close();
                }
                else if (flag == Convert.ToInt32(BillCategory.Yout))
                {
                    PrintYout();
                    if (DalDalvalue == false)
                        this.Close();
                }

                else if (flag == Convert.ToInt32(BillCategory.DailyCollection))
                    PrintDailyCollection();
            }
            catch (Exception ex)
            {
            }
        }
        public void Generalsettingcheck()
        {
            try
            {
                BllSearch<DmnGeneralSetting> bllsearchdrawer = new BllSearch<DmnGeneralSetting>();
                DmnSearch dmnsearckgen = new DmnSearch
                {
                    Select = "*",
                    Table = "tblGeneralSetting",
                    WhereNdGroupBy = $"where BranchId={BranchId} and Status=1",
                    OrderBy = ""
                };
                var gridesource = bllsearchdrawer.GetSearchData("prdSearch", dmnsearckgen);
                if (gridesource.Count > 0)
                {
                    var settings = gridesource[0];
                    XYDalvalue = settings.xyprintdal;
                    DalDalvalue = settings.dalydal;
                    DefaultGmail = settings.DefaultGmail;
                    Enabledefgmail = settings.EnableDefaultGmail;
                    PrintBillType = settings.PrintBillType;
                    BillTaxType = settings.BillTaxType;
                    FooterContent = settings.FooterContent;
                    CompanyNameOtherLanguage = settings.CompanyNameOtherLanguage;
                    CompanyAddressOtherLanguage = settings.CompanyAddressOtherLanguage;
                    FooterOtherLanguage = settings.FooterOtherLanguage;
                    AutoPrint = settings.AutoPrint;
                    Save_YZout = settings.Save_YZout;
                    Save_Daily = settings.Save_Daily;
                    YZPath = settings.YZPath;
                    DailyPath = settings.DailyPath;
                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("YZout page Generalsettingcheck()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.YZout));
            }
        }

        #region PrintDailyCollection
        public void PrintDailyCollection()
        {
            try
            {
                printname = "Daily Collection Report";

                #region Delcration
                DataSet dsReport = new DataSet();

                TimeSpan OpeningDate = TimeSpan.Zero;

                DataTable DT_Drawer = new DataTable("DT_Drawer");
                DataTable DT_Geninfo = new DataTable("DT_Geninfo");
                DataTable DT_Products = new DataTable("DT_Products");
                DataTable dtgeneralsettings = new DataTable("DT_GeneralSettings");
                DataTable DT_Return = new DataTable("DT_Return");


                BllSearch<DmnOutDrawer> bllDrawer = new BllSearch<DmnOutDrawer>();
                BllSearch<DmnOutGenInfo> bllGeninfo = new BllSearch<DmnOutGenInfo>();
                BllSearch<DmnOutCard> bllProducts = new BllSearch<DmnOutCard>();
                BllSearch<DmnGeneralSetting> bllgeneralsetting = new BllSearch<DmnGeneralSetting>();
                BllSearch<DmnReturn> bllReturn = new BllSearch<DmnReturn>();
                #endregion

                #region GeneralSettings
                DmnSearch dmnGeneralSetting = new DmnSearch();
                dmnGeneralSetting.Select = "BillTaxType,FooterContent,CompanyNameOtherLanguage,CompanyAddressOtherLanguage,FooterOtherLanguage";
                dmnGeneralSetting.Table = "tblGeneralSetting";
                dmnGeneralSetting.WhereNdGroupBy = " where BranchId=" + FrmLogin.BranchId;
                var generalsett = bllgeneralsetting.GetSearchData("prdSearch", dmnGeneralSetting);
                if (generalsett.Count > 0)
                {
                    if (PrintBillType == Convert.ToInt32(SalePrintbilltype.English))
                    {
                        var BillTaxType = Convert.ToInt32(generalsett[0].BillTaxType);
                        var FooterContent = Convert.ToString(generalsett[0].FooterContent);
                        var CompanyNameOtherLanguage = Convert.ToString(generalsett[0].CompanyNameOtherLanguage);
                        var CompanyAddressOtherLanguage = Convert.ToString(generalsett[0].CompanyAddressOtherLanguage);
                        var FooterOtherLanguage = Convert.ToString(generalsett[0].FooterOtherLanguage);
                        using (var reader = ObjectReader.Create(generalsett, "BillTaxType", "FooterContent", "CompanyNameOtherLanguage", "CompanyAddressOtherLanguage", "FooterOtherLanguage"))
                        {
                            dtgeneralsettings.Load(reader);
                        }
                    }
                }
                #endregion

                #region Start time
                DmnSearch dmnSearchDetBill = new DmnSearch();
                BllSearch<DmnHRegistration> bllSearch = new BllSearch<DmnHRegistration>();
                DmnSearch dmnSearch = new DmnSearch();
                DateTime currentDate = DateTime.Now;
                DayOfWeek dayOfWeek = currentDate.DayOfWeek;
                int dayNumber = ((int)dayOfWeek + 6) % 7 + 1;

                dmnSearch.Select = "OpeningTime";
                dmnSearch.Table = "tblHotelRegistration ";
                dmnSearch.WhereNdGroupBy = "where Status=1";
                var itemsource = bllSearch.GetSearchData("prdsearch", dmnSearch);
                if (itemsource.Count > 0)
                {
                    OpeningDate = itemsource[0].OpeningTime;
                }
                #endregion                

                String dt = DateTime.Now.Date.ToString("yyyy-MM-dd") + " " + itemsource[0].OpeningTime;

                #region Products
                DmnSearch dmnSearchProduct = new DmnSearch();
                dmnSearchProduct.Select = "SUM(d.Quantity) AS TotalQuantity,d.ItemName as Itemname, CAST(SUM(d.DiscountAmount) AS decimal(18,2)) AS TotalDiscount, CAST(sum(d.NetAmount) AS decimal(18,2)) TotalAmount,g.Caption as ProductType,d.ProductType as ProductId,CAST(sum(d.TaxAmount) AS decimal(18,2)) as Totaltax ";
                dmnSearchProduct.Table = "tblSalesDetails d LEFT JOIN tblSalesMaster m ON d.SalesMasterId = m.SalesMasterId and m.Status=1 LEFT JOIN tblGeneralOptions g ON d.ProductType =g.GTypeSubValue1 and GTypeId=53";
                dmnSearchProduct.WhereNdGroupBy = " WHERE m.BranchId = " + BranchId + " and d.Status= 1 and m.CreatedDateTime >= '" + dt + "' AND m.CreatedDateTime <= SYSDATETIME() GROUP BY  d.ItemName ,g.Caption,d.ProductType";
                var ProductSaleResult = bllProducts.GetSearchData("prdSearch", dmnSearchProduct);
                using (var reader = ObjectReader.Create(ProductSaleResult, "TotalQuantity", "Itemname", "TotalDiscount", "TotalAmount", "Totaltax", "ProductType", "ProductId"))
                {
                    DT_Products.Load(reader);
                }
                #endregion

                #region Drawer
                DmnSearch dmnSearchDrawer = new DmnSearch();
                dmnSearchDrawer.Select = "  0 as InitialAmount,0 as LoanAmount,0 as Drops,sum(NetAmount) as TotalAmount";
                dmnSearchDrawer.Table = "tblSalesMaster WHERE BranchId = " + BranchId + " and PaymentMode = 1" + " and status=1 and"+
                    " CONVERT(DATE, CreatedDateTime) = CONVERT(DATE, SYSDATETIME())"
                    + " AND CONVERT(TIME, CreatedDateTime) >= '" + OpeningDate +
                    "' And CONVERT(TIME, CreatedDateTime) <= CONVERT(TIME, SYSDATETIME()) " +
                    " union all select InitilAmount as InitialAmount,LoanAmount,Drops,0 TotalAmount  from tblDrawerAssign";
                dmnSearchDrawer.WhereNdGroupBy = " where status = 1 and BranchId = " + BranchId +
                    " and CONVERT(DATE, CreatedDateTime) = CONVERT(DATE, SYSDATETIME())"
                    + " AND CONVERT(TIME, CreatedDateTime) >= '" + OpeningDate +
                    "' And CONVERT(TIME, CreatedDateTime) <= CONVERT(TIME, SYSDATETIME()) ";
                var DrawerResult = bllDrawer.GetSearchData("prdSearch", dmnSearchDrawer);
                DmnOutDrawer Dresult = DrawerResult
                    .GroupBy(row => 1)
                    .Select(group => new DmnOutDrawer
                    {
                        InitialAmount = group.Sum(row => row.InitialAmount),
                        LoanAmount = group.Sum(row => row.LoanAmount),
                        Drops = group.Sum(row => row.Drops),
                        TotalAmount = group.Sum(row => row.TotalAmount)
                    })
                    .FirstOrDefault();

                using (var reader = ObjectReader.Create(new List<DmnOutDrawer> { Dresult }, "InitialAmount", "LoanAmount", "Drops", "TotalAmount"))
                {
                    DT_Drawer.Load(reader);
                }
                #endregion

                #region General Information
                DmnSearch dmnSearchGeninfo = new DmnSearch();
                dmnSearchGeninfo.Select = "[CREDIT CARD] as TotalCreditAmt,[DEBIT CARD] as TotalDebitAmt, [UPI] as TotalUPI, [BANKING ONLINE] as TotalBankonline, [CASH] as TotalCash, [OTHERS] as Others, [COMPLEMENTRY] as TotalCompAmt,[Discount] as TotalDiscount,[TotalTax] as Totaltax,[NetAmount] as TotalAmount,[TotalQuantity] as TotalQuantity";
                dmnSearchGeninfo.Table = "(select g.Caption as Caption,CAST(Sum(sm.NetAmount) AS decimal(18,2))  as amount from tblSalesMaster sm left join tblGeneralOptions g on g.GTypeSubId =sm.PaymentMode and g.GTypeId = 2 where sm.status = 1 and sm.BranchId = " + BranchId + "  and  CONVERT(DATE, sm.CreatedDateTime) = CONVERT(DATE, SYSDATETIME()) AND CONVERT(TIME, sm.CreatedDateTime) >= '" + OpeningDate + "' And CONVERT(TIME, sm.CreatedDateTime) <= CONVERT(TIME, SYSDATETIME())  GROUP BY  g.Caption,sm.NetAmount" +
                    " union all Select 'Discount' Caption ,CAST(Sum(DiscountAmount) AS decimal(18,2)) as amount from tblSalesMaster WHERE BranchId = " + BranchId + " and Status=1   and CONVERT(DATE, CreatedDateTime) = CONVERT(DATE, SYSDATETIME()) AND CONVERT(TIME, CreatedDateTime) >= '" + OpeningDate +"' And CONVERT(TIME, CreatedDateTime) <= CONVERT(TIME, SYSDATETIME()) "+
                    " Union all Select 'TotalTax' Caption ,CAST(Sum(TotalTax) AS decimal(18,2)) as amount  from tblSalesMaster WHERE BranchId = " + BranchId + " and Status=1  and CONVERT(DATE, CreatedDateTime) = CONVERT(DATE, SYSDATETIME())" + " AND CONVERT(TIME, CreatedDateTime) >= '" + OpeningDate +      "' And CONVERT(TIME, CreatedDateTime) <= CONVERT(TIME, SYSDATETIME()) and PaymentMode!= " + Convert.ToInt32(PaymentType.COMPLEMENTRY) +
                    " Union all Select 'NetAmount' Caption,CAST(Sum(NetAmount) AS decimal(18,2)) as amount  from tblSalesMaster WHERE BranchId = " + BranchId + " and Status=1   and CreatedDateTime >= '" + OpeningDate + "' AND CreatedDateTime <= SYSDATETIME() and PaymentMode!= " + Convert.ToInt32(PaymentType.COMPLEMENTRY) +
                    " Union all Select 'TotalQuantity',Sum(TotalQuantity) as amount  from tblSalesMaster WHERE BranchId = " + BranchId + " and Status=1 and CONVERT(DATE, CreatedDateTime) = CONVERT(DATE, SYSDATETIME())" + " AND CONVERT(TIME, CreatedDateTime) >= '" + OpeningDate + "' And CONVERT(TIME, CreatedDateTime) <= CONVERT(TIME, SYSDATETIME()) " +
                    ")ps PIVOT (SUM(amount)FOR Caption IN([CREDIT CARD],[DEBIT CARD],[UPI],[BANKING ONLINE],[CASH],[OTHERS],[COMPLEMENTRY],[Discount],[TotalTax],[NetAmount],[TotalQuantity])) AS pvt  ";
                dmnSearchGeninfo.WhereNdGroupBy = "";
                var GenInfoResult = bllGeninfo.GetSearchData("prdSearch", dmnSearchGeninfo);
                using (var reader = ObjectReader.Create(GenInfoResult, "TotalCompAmt", "Others", "TotalCash", "TotalBankonline", "TotalUPI", "TotalDebitAmt", "TotalCreditAmt", "TotalQuantity", "TotalDiscount", "Totaltax", "TotalAmount"))
                {
                    DT_Geninfo.Load(reader);
                }
                #endregion

                #region Return Summary
                DmnSearch dmnSearchRtn = new DmnSearch();
                dmnSearchRtn.Select = "sum(cashRefund) as TotalRtnCash,sum(cashbalanceRefund) as TotalRtnCard";
                dmnSearchRtn.Table = "tblrefund r";
                dmnSearchRtn.WhereNdGroupBy = "where r.Status = 1 AND r.BranchId = " + BranchId + " AND " +
                     " CONVERT(DATE, r.CreatedDateTime) = CONVERT(DATE, SYSDATETIME())"
                    + " AND CONVERT(TIME, r.CreatedDateTime) >= '" + OpeningDate +
                    "' And CONVERT(TIME, r.CreatedDateTime) <= CONVERT(TIME, SYSDATETIME()) ";
                var RtnResult = bllReturn.GetSearchData("prdSearch", dmnSearchRtn);
                using (var reader = ObjectReader.Create(RtnResult, "TotalRtnCash", "TotalRtnCard"))//"TotalRtnQty", "TotalRtnAmt"
                {
                    DT_Return.Load(reader);
                }
                #endregion                

                dsReport.Tables.Add(DT_Products);
                dsReport.Tables.Add(DT_Drawer);
                dsReport.Tables.Add(DT_Geninfo);
                dsReport.Tables.Add(dtgeneralsettings);
                dsReport.Tables.Add(DT_Return);

                if (PrintBillType == Convert.ToInt32(SalePrintbilltype.English_Arabic))
                {
                    reportDocument.Load(RPTVWFile("\\CR_Zout_English_With_Arabic.rpt"));
                }
                else if (PrintBillType == Convert.ToInt32(SalePrintbilltype.English))
                {
                    reportDocument.Load(RPTVWFile("\\CR_Zout.rpt"));
                }
                else if (PrintBillType == Convert.ToInt32(SalePrintbilltype.Arabic))
                {
                }
                reportDocument.SetDataSource(dsReport);

                /* header section*/
                Addressdata = Get_CompantDetails();
                reportDocument.SetParameterValue("Loc_Name", Convert.ToString(Addressdata[0].Field1));
                reportDocument.SetParameterValue("City", Convert.ToString(Addressdata[0].Field5));
                reportDocument.SetParameterValue("Employe_Name", Convert.ToString(FrmLogin.EmployeName));
                reportDocument.SetParameterValue("GSTno", Convert.ToString(Addressdata[0].Field6));
                reportDocument.SetParameterValue("Address", Convert.ToString(Addressdata[0].Field4));
                reportDocument.SetParameterValue("Designation", Convert.ToString(Addressdata[0].Field8));
                string devicename = System.Environment.MachineName;
                reportDocument.SetParameterValue("Device_name", devicename);
                /*End Header section*/

                /* report header Name*/
                reportDocument.SetParameterValue("OutHeading", "Daily Collection Report");


                crvout.ReuseParameterValuesOnRefresh = true;
                //crvout.RefreshReport();
                crvout.ReportSource = reportDocument;
                if (Convert.ToInt32(AutoPrint) == Convert.ToInt32(AutoPrintToPrinter.Yes))
                {
                    if (selectedPrinters_TH.Count > 0)
                        SetDefaultPrinter(selectedPrinters_TH[0]);

                    //if (XYDalvalue == false || DalDalvalue == false)
                    reportDocument.PrintToPrinter(1, false, 0, 0);
                }

                if (Convert.ToInt32(Save_Daily) == Convert.ToInt32(Save_Copy_DailyRpt.Yes))
                {
                    string folderPath = DailyPath + "\\DailyCollection";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    #region pdf Generate
                    string currentDateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    pdfFileName = $"{printname}_{currentDateTime}.pdf";
                    reportpdfPath = Path.Combine(folderPath, pdfFileName);
                    // Export report to PDF
                    reportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, reportpdfPath);
                    #endregion

                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("YZout Page PrintDailyCollection()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.YZout));
            }
        }
        #endregion

        #region DetailsCollection
        public void PrintDetailsCollection()
        {
            try
            {
                Generalsettingcheck();
                printname = "Detailed Collection Report";

                BranchId = FrmDashboard.BranchId;

                #region Delcration
                string pdffolderPath = null;
                TimeSpan OpeningDate = TimeSpan.Zero;

                BllSearch<DmnOutDrawer> bllDrawer = new BllSearch<DmnOutDrawer>();
                BllSearch<DmnOutGenInfo> bllGeninfo = new BllSearch<DmnOutGenInfo>();
                BllSearch<DmnOutCard> bllProducts = new BllSearch<DmnOutCard>();
                BllSearch<DmnGeneralSetting> bllgeneralsetting = new BllSearch<DmnGeneralSetting>();
                BllSearch<DmnReturn> bllReturn = new BllSearch<DmnReturn>();
                DataTable dtgeneralsettings = new DataTable("DT_GeneralSettings");
       

                pdffolderPath = Application.StartupPath + "\\DetailsOUT_PDF";
                if (!Directory.Exists(pdffolderPath))
                    Directory.CreateDirectory(pdffolderPath);
                else
                {
                    string[] files = Directory.GetFiles(pdffolderPath);
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                }
                #endregion

                #region GeneralSettings
                DmnSearch dmnGeneralSetting = new DmnSearch();
                dmnGeneralSetting.Select = "BillTaxType,FooterContent,CompanyNameOtherLanguage,CompanyAddressOtherLanguage,FooterOtherLanguage";
                dmnGeneralSetting.Table = "tblGeneralSetting";
                dmnGeneralSetting.WhereNdGroupBy = " where BranchId=" + FrmLogin.BranchId;
                var generalsett = bllgeneralsetting.GetSearchData("prdSearch", dmnGeneralSetting);
                if (generalsett.Count > 0)
                {
                    if (PrintBillType == Convert.ToInt32(SalePrintbilltype.English))
                    {
                        var BillTaxType = Convert.ToInt32(generalsett[0].BillTaxType);
                        var FooterContent = Convert.ToString(generalsett[0].FooterContent);
                        var CompanyNameOtherLanguage = Convert.ToString(generalsett[0].CompanyNameOtherLanguage);
                        var CompanyAddressOtherLanguage = Convert.ToString(generalsett[0].CompanyAddressOtherLanguage);
                        var FooterOtherLanguage = Convert.ToString(generalsett[0].FooterOtherLanguage);
                        using (var reader = ObjectReader.Create(generalsett, "BillTaxType", "FooterContent", "CompanyNameOtherLanguage", "CompanyAddressOtherLanguage", "FooterOtherLanguage"))
                        {
                            dtgeneralsettings.Load(reader);
                        }
                    }
                }
                #endregion

                #region Start time
                DmnSearch dmnSearchDetBill = new DmnSearch();
                BllSearch<DmnHRegistration> bllSearch = new BllSearch<DmnHRegistration>();
                DmnSearch dmnSearch = new DmnSearch();
                DateTime currentDate = DateTime.Now;
                DayOfWeek dayOfWeek = currentDate.DayOfWeek;
                int dayNumber = ((int)dayOfWeek + 6) % 7 + 1;

                dmnSearch.Select = "OpeningTime";
                dmnSearch.Table = "tblHotelRegistration ";
                dmnSearch.WhereNdGroupBy = "where Status=1";
                var itemsource = bllSearch.GetSearchData("prdsearch", dmnSearch);
                if (itemsource.Count > 0)
                {
                    OpeningDate = itemsource[0].OpeningTime;
                }
                #endregion                

                String dt = DateTime.Now.Date.ToString("yyyy-MM-dd") + " " + itemsource[0].OpeningTime;

                #region Staff List
                DmnSearch dmnSearchStaff = new DmnSearch();
                BllSearch<DmnGridFields> bllSearchStaff = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearchstaff = new DmnSearch();

                dmnSearchstaff.Select = "CreatedBy as Field1";
                dmnSearchstaff.Table = "tblSalesMaster ";
                dmnSearchstaff.WhereNdGroupBy = " where status=1 and  BranchId = " + BranchId + " and CONVERT(DATE,CreatedDateTime) = CONVERT(DATE, SYSDATETIME()) "
                    + " AND CONVERT(TIME, CreatedDateTime) >= '" + OpeningDate +
                    "' And CONVERT(TIME, CreatedDateTime) <= CONVERT(TIME, SYSDATETIME()) group by CreatedBy";
                var StaffResult = bllSearchStaff.GetSearchData("prdsearch", dmnSearchstaff);

                #endregion

                int Staffid = 0;
                for (int i = 0; i < StaffResult.Count; i++)
                {
                    DataSet dsReport = new DataSet();

                    DataTable DT_Drawer = new DataTable("DT_Drawer");
                    DataTable DT_Geninfo = new DataTable("DT_Geninfo");
                    DataTable DT_Products = new DataTable("DT_Products");
                    DataTable DT_Return = new DataTable("DT_Return");

                    Staffid = Convert.ToInt32(StaffResult[i].Field1);

                    #region Products
                    DmnSearch dmnSearchProduct = new DmnSearch();
                    dmnSearchProduct.Select = "SUM(d.Quantity) AS TotalQuantity,d.ItemName as Itemname, CAST(SUM(d.DiscountAmount) AS decimal(18,2)) AS TotalDiscount, CAST(sum(d.NetAmount) AS decimal(18,2)) TotalAmount,g.Caption as ProductType,d.ProductType as ProductId,CAST(sum(d.TaxAmount) AS decimal(18,2)) as Totaltax ";
                    dmnSearchProduct.Table = "tblSalesDetails d LEFT JOIN tblSalesMaster m ON d.SalesMasterId = m.SalesMasterId and m.Status=1 LEFT JOIN tblGeneralOptions g ON d.ProductType =g.GTypeSubValue1 and GTypeId=53";
                    dmnSearchProduct.WhereNdGroupBy = " WHERE m.BranchId = " + BranchId + " and d.Status= 1 and m.CreatedBy = " + Staffid + " and  m.CreatedDateTime >= '" + dt + "' AND m.CreatedDateTime <= SYSDATETIME() GROUP BY  d.ItemName ,g.Caption,d.ProductType";
                    var ProductSaleResult = bllProducts.GetSearchData("prdSearch", dmnSearchProduct);
                    using (var reader = ObjectReader.Create(ProductSaleResult, "TotalQuantity", "Itemname", "TotalDiscount", "TotalAmount", "Totaltax", "ProductType", "ProductId"))
                    {
                        DT_Products.Load(reader);
                    }
                    #endregion

                    #region Drawer
                    DmnSearch dmnSearchDrawer = new DmnSearch();
                    dmnSearchDrawer.Select = "  0 as InitialAmount,0 as LoanAmount,0 as Drops,sum(NetAmount) as TotalAmount";
                    dmnSearchDrawer.Table = "tblSalesMaster WHERE BranchId = " + BranchId + " and PaymentMode = 1" + " and status=1 and" +
                        " CONVERT(DATE, CreatedDateTime) = CONVERT(DATE, SYSDATETIME())"
                        + " AND CONVERT(TIME, CreatedDateTime) >= '" + OpeningDate +
                        "' And CONVERT(TIME, CreatedDateTime) <= CONVERT(TIME, SYSDATETIME()) and CreatedBy = " + Staffid +
                        " union all select InitilAmount as InitialAmount,LoanAmount,Drops,0 TotalAmount  from tblDrawerAssign";
                    dmnSearchDrawer.WhereNdGroupBy = " where status = 1 and BranchId = " + BranchId +
                        " and CONVERT(DATE, CreatedDateTime) = CONVERT(DATE, SYSDATETIME())"
                        + " AND CONVERT(TIME, CreatedDateTime) >= '" + OpeningDate +
                        "' And CONVERT(TIME, CreatedDateTime) <= CONVERT(TIME, SYSDATETIME()) and CreatedBy = " + Staffid;
                    var DrawerResult = bllDrawer.GetSearchData("prdSearch", dmnSearchDrawer);
                    DmnOutDrawer Dresult = DrawerResult
                        .GroupBy(row => 1)
                        .Select(group => new DmnOutDrawer
                        {
                            InitialAmount = group.Sum(row => row.InitialAmount),
                            LoanAmount = group.Sum(row => row.LoanAmount),
                            Drops = group.Sum(row => row.Drops),
                            TotalAmount = group.Sum(row => row.TotalAmount)
                        })
                        .FirstOrDefault();

                    using (var reader = ObjectReader.Create(new List<DmnOutDrawer> { Dresult }, "InitialAmount", "LoanAmount", "Drops", "TotalAmount"))
                    {
                        DT_Drawer.Load(reader);
                    }
                    #endregion

                    #region General Information
                    DmnSearch dmnSearchGeninfo = new DmnSearch();
                    dmnSearchGeninfo.Select = "[CREDIT CARD] as TotalCreditAmt,[DEBIT CARD] as TotalDebitAmt, [UPI] as TotalUPI, [BANKING ONLINE] as TotalBankonline, [CASH] as TotalCash, [OTHERS] as Others, [COMPLEMENTRY] as TotalCompAmt,[Discount] as TotalDiscount,[TotalTax] as Totaltax,[NetAmount] as TotalAmount,[TotalQuantity] as TotalQuantity";
                    dmnSearchGeninfo.Table = "(select g.Caption as Caption,CAST(Sum(sm.NetAmount) AS decimal(18,2))  as amount from tblSalesMaster sm left join tblGeneralOptions g on g.GTypeSubId =sm.PaymentMode and g.GTypeId = 2 where sm.status = 1 and sm.BranchId = " + BranchId + "  and  CONVERT(DATE, sm.CreatedDateTime) = CONVERT(DATE, SYSDATETIME()) AND CONVERT(TIME, sm.CreatedDateTime) >= '" + OpeningDate + "' And CONVERT(TIME, sm.CreatedDateTime) <= CONVERT(TIME, SYSDATETIME()) and sm.CreatedBy = " + Staffid + " GROUP BY  g.Caption,sm.NetAmount" +
                        " union all Select 'Discount' Caption ,CAST(Sum(DiscountAmount) AS decimal(18,2)) as amount from tblSalesMaster WHERE BranchId = " + BranchId + " and Status=1   and CONVERT(DATE, CreatedDateTime) = CONVERT(DATE, SYSDATETIME()) AND CONVERT(TIME, CreatedDateTime) >= '" + OpeningDate + "' And CONVERT(TIME, CreatedDateTime) <= CONVERT(TIME, SYSDATETIME()) and CreatedBy = " + Staffid  +
                        " Union all Select 'TotalTax' Caption ,CAST(Sum(TotalTax) AS decimal(18,2)) as amount  from tblSalesMaster WHERE BranchId = " + BranchId + " and Status=1  and CONVERT(DATE, CreatedDateTime) = CONVERT(DATE, SYSDATETIME())" + " AND CONVERT(TIME, CreatedDateTime) >= '" + OpeningDate + "' And CONVERT(TIME, CreatedDateTime) <= CONVERT(TIME, SYSDATETIME())  and CreatedBy = " + Staffid+ " and PaymentMode!= " + Convert.ToInt32(PaymentType.COMPLEMENTRY) +
                        " Union all Select 'NetAmount' Caption,CAST(Sum(NetAmount) AS decimal(18,2)) as amount  from tblSalesMaster WHERE BranchId = " + BranchId + " and Status=1   and CreatedDateTime >= '" + OpeningDate + "' AND CreatedDateTime <= SYSDATETIME() and CreatedBy = " + Staffid + " and PaymentMode!= " + Convert.ToInt32(PaymentType.COMPLEMENTRY) +
                        " Union all Select 'TotalQuantity',Sum(TotalQuantity) as amount  from tblSalesMaster WHERE BranchId = " + BranchId + " and Status=1 and CONVERT(DATE, CreatedDateTime) = CONVERT(DATE, SYSDATETIME())" + " AND CONVERT(TIME, CreatedDateTime) >= '" + OpeningDate + "' And CONVERT(TIME, CreatedDateTime) <= CONVERT(TIME, SYSDATETIME())  and CreatedBy = " + Staffid+
                        ")ps PIVOT (SUM(amount)FOR Caption IN([CREDIT CARD],[DEBIT CARD],[UPI],[BANKING ONLINE],[CASH],[OTHERS],[COMPLEMENTRY],[Discount],[TotalTax],[NetAmount],[TotalQuantity])) AS pvt  ";
                    dmnSearchGeninfo.WhereNdGroupBy = "";
                    var GenInfoResult = bllGeninfo.GetSearchData("prdSearch", dmnSearchGeninfo);
                    using (var reader = ObjectReader.Create(GenInfoResult, "TotalCompAmt", "Others", "TotalCash", "TotalBankonline", "TotalUPI", "TotalDebitAmt", "TotalCreditAmt", "TotalQuantity", "TotalDiscount", "Totaltax", "TotalAmount"))
                    {
                        DT_Geninfo.Load(reader);
                    }
                    #endregion

                    #region Return Summary
                    DmnSearch dmnSearchRtn = new DmnSearch();
                    dmnSearchRtn.Select = "sum(cashRefund) as TotalRtnCash,sum(cashbalanceRefund) as TotalRtnCard";
                    dmnSearchRtn.Table = "tblrefund r";
                    dmnSearchRtn.WhereNdGroupBy = "where r.Status = 1 AND r.BranchId = " + BranchId + " AND " +
                        " CONVERT(DATE, r.CreatedDateTime) = CONVERT(DATE, SYSDATETIME())"
                        + " AND CONVERT(TIME, r.CreatedDateTime) >= '" + OpeningDate +
                        "' And CONVERT(TIME, r.CreatedDateTime) <= CONVERT(TIME, SYSDATETIME()) and r.CreatedBy = " + Staffid;
                    var RtnResult = bllReturn.GetSearchData("prdSearch", dmnSearchRtn);
                    using (var reader = ObjectReader.Create(RtnResult, "TotalRtnCash", "TotalRtnCard"))//"TotalRtnQty", "TotalRtnAmt"
                    {
                        DT_Return.Load(reader);
                    }
                    #endregion

                    dsReport.Tables.Add(DT_Products);
                    dsReport.Tables.Add(DT_Drawer);
                    dsReport.Tables.Add(DT_Geninfo);
                    dsReport.Tables.Add(dtgeneralsettings);
                    dsReport.Tables.Add(DT_Return);

                    if (PrintBillType == Convert.ToInt32(SalePrintbilltype.English_Arabic))
                    {
                        reportDocument.Load(RPTVWFile("\\CR_Zout_English_With_Arabic.rpt"));
                    }
                    else if (PrintBillType == Convert.ToInt32(SalePrintbilltype.English))
                    {
                        reportDocument.Load(RPTVWFile("\\CR_Zout.rpt"));
                    }
                    else if (PrintBillType == Convert.ToInt32(SalePrintbilltype.Arabic))
                    {
                    }
                    reportDocument.SetDataSource(dsReport);


                    /* header section*/
                    Addressdata = Get_CompantDetails();
                    reportDocument.SetParameterValue("Loc_Name", Convert.ToString(Addressdata[0].Field1));
                    reportDocument.SetParameterValue("City", Convert.ToString(Addressdata[0].Field5));
                    reportDocument.SetParameterValue("Employe_Name", Convert.ToString(FrmLogin.EmployeName));
                    reportDocument.SetParameterValue("GSTno", Convert.ToString(Addressdata[0].Field6));
                    reportDocument.SetParameterValue("Address", Convert.ToString(Addressdata[0].Field4));
                    reportDocument.SetParameterValue("Designation", Convert.ToString(Addressdata[0].Field8));
                    string devicename = System.Environment.MachineName;
                    reportDocument.SetParameterValue("Device_name", devicename);
                    /*End Header section*/

                    /*report header Name*/
                    reportDocument.SetParameterValue("OutHeading", "Details Collection report");

                    string pdfPath = Path.Combine(pdffolderPath, "Bill" + i + ".pdf");
                    reportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath);
                    //if (Convert.ToInt32(AutoPrint) == Convert.ToInt32(AutoPrintToPrinter.Yes))
                    //{
                    if (selectedPrinters_TH.Count > 0)
                        SetDefaultPrinter(selectedPrinters_TH[0]);
                    reportDocument.PrintToPrinter(1, false, 0, 0);

                    //}
                }

                // Output file path for the merged PDF
                string mergedPdfPath = "merged.pdf";
                pdffolderPath = Application.StartupPath + "\\DetailsOUT_PDF";

                if (!Directory.Exists(mergedPdfPath))
                    Directory.CreateDirectory(mergedPdfPath);
                else
                {
                    string[] files = Directory.GetFiles(mergedPdfPath);
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                }

                MergePdfFiles(pdffolderPath, mergedPdfPath);
                reportpdfPath = Application.StartupPath + "\\Details_coll_Report_" + DateTime.Now + ".pdf";
                crvout.Visible = true;
                this.Hide();
            }
            catch (Exception ex)
            {
                clsgen.LogException("ZOut Page PrintDetailsCollection()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.YZout));
            }
        }
        static void MergePdfFiles(string pdffolderPath, string mergedPdfPath)
        {
            string[] pdfFiles = Directory.GetFiles(pdffolderPath, "*.pdf");

            if (pdfFiles.Length > 0)
            {
                using (FileStream stream = new FileStream(mergedPdfPath, FileMode.Create))
                {
                    iTextSharp.text.Document document = new iTextSharp.text.Document();
                    PdfCopy pdfCopy = new PdfCopy(document, stream);
                    document.Open();

                    foreach (string pdfFile in pdfFiles)
                    {
                        using (PdfReader reader = new PdfReader(pdfFile))
                        {
                            for (int i = 1; i <= reader.NumberOfPages; i++)
                            {
                                PdfImportedPage page = pdfCopy.GetImportedPage(reader, i);
                                pdfCopy.AddPage(page);
                            }
                        }
                    }

                    document.Close();
                }
            }
            else
            {
                Console.WriteLine("No PDF files found in the directory.");
            }
        }
        #endregion  

        #region Zout
        public void PrintZout()
        {
            try
            {
                printname = "Zout";
                DataSet dsReport = new DataSet();

                string Drawerassigntime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                DataTable DT_Drawer = new DataTable("DT_Drawer");
                DataTable DT_Geninfo = new DataTable("DT_Geninfo");
                DataTable DT_Products = new DataTable("DT_Products");
                DataTable dtgeneralsettings = new DataTable("DT_GeneralSettings");
                DataTable DT_Return = new DataTable("DT_Return"); 

                BllSearch<DmnOutDrawer> bllDrawer = new BllSearch<DmnOutDrawer>();
                BllSearch<DmnOutGenInfo> bllGeninfo = new BllSearch<DmnOutGenInfo>();
                BllSearch<DmnOutCard> bllProducts = new BllSearch<DmnOutCard>();
                BllSearch<DmnGeneralSetting> bllgeneralsetting = new BllSearch<DmnGeneralSetting>();
                BllSearch<DmnReturn> bllReturn = new BllSearch<DmnReturn>();

                #region GeneralSettings
                DmnSearch dmnGeneralSetting = new DmnSearch();
                dmnGeneralSetting.Select = "BillTaxType,FooterContent,CompanyNameOtherLanguage,CompanyAddressOtherLanguage,FooterOtherLanguage";
                dmnGeneralSetting.Table = "tblGeneralSetting";
                dmnGeneralSetting.WhereNdGroupBy = " where BranchId=" + FrmLogin.BranchId;
                var generalsett = bllgeneralsetting.GetSearchData("prdSearch", dmnGeneralSetting);
                if (generalsett.Count > 0)
                {
                    if (PrintBillType == Convert.ToInt32(SalePrintbilltype.English))
                    {
                        var BillTaxType = Convert.ToInt32(generalsett[0].BillTaxType);
                        var FooterContent = Convert.ToString(generalsett[0].FooterContent);
                        var CompanyNameOtherLanguage = Convert.ToString(generalsett[0].CompanyNameOtherLanguage);
                        var CompanyAddressOtherLanguage = Convert.ToString(generalsett[0].CompanyAddressOtherLanguage);
                        var FooterOtherLanguage = Convert.ToString(generalsett[0].FooterOtherLanguage);
                        using (var reader = ObjectReader.Create(generalsett, "BillTaxType", "FooterContent", "CompanyNameOtherLanguage", "CompanyAddressOtherLanguage", "FooterOtherLanguage"))
                        {
                            dtgeneralsettings.Load(reader);
                        }
                    }
                }
                #endregion

                #region Drawer Assign time
                DmnSearch dmnSearchDetBill = new DmnSearch();
                BllSearch<Dmndrawer> bllSearch = new BllSearch<Dmndrawer>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "CreatedDateTime ";
                dmnSearch.Table = "tblDrawerAssign ";
                //dmnSearch.WhereNdGroupBy = "where BranchId =" + BranchId + " and CreatedBy=" + FrmLogin.UserId + " and Status=1";
                dmnSearch.WhereNdGroupBy = "where BranchId =" + BranchId + " and DName='" + System.Environment.MachineName + "' and Status=1";
                var itemsource = bllSearch.GetSearchData("prdsearch", dmnSearch);
                if (itemsource.Count > 0)
                {
                    Drawerassigntime = itemsource[0].CreatedDateTime.ToString("yyyy-MM-dd HH:mm");
                }
                #endregion

                #region Products
                DmnSearch dmnSearchProduct = new DmnSearch();
                dmnSearchProduct.Select = "SUM(d.Quantity) AS TotalQuantity,d.ItemName as Itemname, CAST(SUM(d.DiscountAmount) AS decimal(18,2)) AS TotalDiscount, CAST(sum(d.NetAmount) AS decimal(18,2)) TotalAmount,g.Caption as ProductType,d.ProductType as ProductId,CAST(sum(d.TaxAmount) AS decimal(18,2)) as Totaltax ";
                dmnSearchProduct.Table = "tblSalesDetails d LEFT JOIN tblSalesMaster m ON d.SalesMasterId = m.SalesMasterId and m.Status=1 LEFT JOIN tblGeneralOptions g ON d.ProductType =g.GTypeSubValue1 and GTypeId=53";
                dmnSearchProduct.WhereNdGroupBy = " WHERE m.BranchId = " + BranchId + " and d.Status= 1 and m.CreatedBy=" + FrmLogin.UserId + " and m.CreatedDateTime >= '" + Drawerassigntime + "' AND m.CreatedDateTime <= SYSDATETIME() GROUP BY  d.ItemName ,g.Caption,d.ProductType";
                var ProductSaleResult = bllProducts.GetSearchData("prdSearch", dmnSearchProduct);
                using (var reader = ObjectReader.Create(ProductSaleResult, "TotalQuantity", "Itemname", "TotalDiscount", "TotalAmount", "Totaltax", "ProductType", "ProductId"))
                {
                    DT_Products.Load(reader);
                }
                #endregion

                #region Drawer
                DmnSearch dmnSearchDrawer = new DmnSearch();
                dmnSearchDrawer.Select = "  0 as InitialAmount,0 as LoanAmount,0 as Drops,sum(NetAmount) as TotalAmount";
                dmnSearchDrawer.Table = "tblSalesMaster WHERE BranchId = " + BranchId + " and PaymentMode = 1 and CreatedBy = " + FrmLogin.UserId + "and status=1 and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME() " +
                    " union all select InitilAmount as InitialAmount,LoanAmount,Drops,0 TotalAmount  from tblDrawerAssign";
                dmnSearchDrawer.WhereNdGroupBy = " where status = 1 and BranchId = " + BranchId + "and CreatedBy = " + FrmLogin.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME() ";
                var DrawerResult = bllDrawer.GetSearchData("prdSearch", dmnSearchDrawer);
                DmnOutDrawer Dresult = DrawerResult
                    .GroupBy(row => 1)
                    .Select(group => new DmnOutDrawer
                    {
                        InitialAmount = group.Sum(row => row.InitialAmount),
                        LoanAmount = group.Sum(row => row.LoanAmount),
                        Drops = group.Sum(row => row.Drops),
                        TotalAmount = group.Sum(row => row.TotalAmount)
                    })
                    .FirstOrDefault();

                using (var reader = ObjectReader.Create(new List<DmnOutDrawer> { Dresult }, "InitialAmount", "LoanAmount", "Drops", "TotalAmount"))
                {
                    DT_Drawer.Load(reader);
                }
                #endregion

                #region General Information
                DmnSearch dmnSearchGeninfo = new DmnSearch();
                dmnSearchGeninfo.Select = "[CREDIT CARD] as TotalCreditAmt,[DEBIT CARD] as TotalDebitAmt, [UPI] as TotalUPI, [BANKING ONLINE] as TotalBankonline, [CASH] as TotalCash, [OTHERS] as Others, [COMPLEMENTRY] as TotalCompAmt,[Discount] as TotalDiscount,[TotalTax] as Totaltax,[NetAmount] as TotalAmount,[TotalQuantity] as TotalQuantity,[TotalPreTaxAmt] AS TotalPreTaxAmt,[MIDASCARD] as Totalmidascard";
                dmnSearchGeninfo.Table = "(select g.Caption as Caption,CAST(Sum(sm.NetAmount) AS decimal(18,2))  as amount from tblSalesMaster sm left join tblGeneralOptions g on g.GTypeSubId =sm.PaymentMode and g.GTypeId = 2 where sm.status = 1 and sm.BranchId = " + BranchId + " and sm.CreatedBy = " + FrmLogin.UserId + " and sm.CreatedDateTime >= '" + Drawerassigntime + "' AND sm.CreatedDateTime <= SYSDATETIME() GROUP BY  g.Caption,sm.NetAmount" +
                    " union all Select 'Discount' Caption ,CAST(Sum(DiscountAmount) AS decimal(18,2)) as amount from tblSalesMaster WHERE BranchId = " + BranchId + " and Status=1  and CreatedBy = " + FrmLogin.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME()" +
                    " Union all Select 'TotalPreTaxAmt', CAST(SUM(NetAmount) - SUM(TotalTax) AS decimal(18, 2)) as amount from tblSalesMaster WHERE BranchId = " + BranchId + " AND Status = 1 AND CreatedBy = " + FrmLogin.UserId + " AND CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME()" +
                    " Union all Select 'TotalTax' Caption ,CAST(Sum(TotalTax) AS decimal(18,2)) as amount  from tblSalesMaster WHERE BranchId = " + BranchId + " and Status=1  and CreatedBy = " + FrmLogin.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME() and PaymentMode!= " + Convert.ToInt32(PaymentType.COMPLEMENTRY) +
                    " Union all Select 'NetAmount' Caption,CAST(Sum(NetAmount) AS decimal(18,2)) as amount  from tblSalesMaster WHERE BranchId = " + BranchId + " and Status=1  and CreatedBy = " + FrmLogin.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME() and PaymentMode!= " + Convert.ToInt32(PaymentType.COMPLEMENTRY) +
                    " Union all Select 'TotalQuantity',Sum(TotalQuantity) as amount  from tblSalesMaster WHERE BranchId = " + BranchId + " and Status=1 and CreatedBy = " + FrmLogin.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME()" +
                    " UNION ALL SELECT g.Caption AS Caption,CAST(SUM(p.paidAmt) AS decimal(18, 2)) AS amount FROM tblPartPaymentDetails p INNER JOIN tblSalesMaster s ON p.SalesMasterId = s.SalesMasterId LEFT JOIN tblGeneralOptions g ON g.GTypeSubId = p.PayMode AND g.GTypeId = 2 WHERE p.PayMode!= 11 AND s.BranchId= " + BranchId + " AND s.PaymentMode = 12 AND s.Status = 1 AND s.CreatedBy = " + FrmLogin.UserId + " and s.CreatedDateTime >= '" + Drawerassigntime + "' AND s.CreatedDateTime <= SYSDATETIME() GROUP BY g.Caption" +
                    " UNION ALL  SELECT 'MIDASCARD' AS Caption, CAST(SUM(p.paidAmt) AS DECIMAL(18, 2)) AS amount FROM tblPartPaymentDetails p INNER JOIN tblSalesMaster s ON p.SalesMasterId = s.SalesMasterId WHERE p.PayMode = 11 AND s.PaymentMode = 12 AND s.BranchId= " + BranchId + " AND s.Status = 1 AND s.CreatedBy = " + FrmLogin.UserId + "AND s.CreatedDateTime >= '" + Drawerassigntime + "' AND s.CreatedDateTime <= SYSDATETIME()" +
                    ")ps PIVOT (SUM(amount)FOR Caption IN([CREDIT CARD],[DEBIT CARD],[UPI],[BANKING ONLINE],[CASH],[OTHERS],[COMPLEMENTRY],[Discount],[TotalTax],[NetAmount],[TotalQuantity],[MIDASCARD],[TotalPreTaxAmt])) AS pvt  ";
                dmnSearchGeninfo.WhereNdGroupBy = "";
                var GenInfoResult = bllGeninfo.GetSearchData("prdSearch", dmnSearchGeninfo);
                using (var reader = ObjectReader.Create(GenInfoResult,
                    "TotalCreditAmt",
                    "TotalDebitAmt",
                    "TotalUPI",
                    "TotalBankonline",
                    "TotalCash",
                    "Others",
                    "TotalCompAmt",
                    "TotalDiscount",
                    "TotalQuantity",
                    "Totaltax",
                    "TotalAmount",
                    "TotalPreTaxAmt",
                    "Totalmidascard"))
                {

                    DT_Geninfo.Load(reader);
                }
                #endregion

                #region Return Summary
                DmnSearch dmnSearchRtn = new DmnSearch();
                dmnSearchRtn.Select = "sum(cashRefund) as TotalRtnCash,sum(cashbalanceRefund) as TotalRtnCard";
                dmnSearchRtn.Table = "tblrefund r";
                dmnSearchRtn.WhereNdGroupBy = "where r.Status = 1 AND r.BranchId = " + BranchId + " AND r.CreatedBy = " + FrmDashboard.UserId + "  AND r.CreatedDateTime >= '" + Drawerassigntime + "' AND r.CreatedDateTime <= SYSDATETIME()";
                var RtnResult = bllReturn.GetSearchData("prdSearch", dmnSearchRtn);
                using (var reader = ObjectReader.Create(RtnResult, "TotalRtnCash", "TotalRtnCard"))//"TotalRtnQty", "TotalRtnAmt"
                {
                    DT_Return.Load(reader);
                }
                #endregion                

                dsReport.Tables.Add(DT_Products);
                dsReport.Tables.Add(DT_Drawer);
                dsReport.Tables.Add(DT_Geninfo);
                dsReport.Tables.Add(dtgeneralsettings);
                dsReport.Tables.Add(DT_Return);

                if (PrintBillType == Convert.ToInt32(SalePrintbilltype.English_Arabic))
                {
                    reportDocument.Load(RPTVWFile("CR_Zout_English_With_Arabic.rpt"));
                }
                else if (PrintBillType == Convert.ToInt32(SalePrintbilltype.English))
                {
                    reportDocument.Load(RPTVWFile("CR_Zout.rpt"));
                }
                else if (PrintBillType == Convert.ToInt32(SalePrintbilltype.Arabic))
                {

                }
                reportDocument.SetDataSource(dsReport);

                /* header section*/
                Addressdata = Get_CompantDetails();
                reportDocument.SetParameterValue("Loc_Name", Convert.ToString(Addressdata[0].Field1));
                reportDocument.SetParameterValue("City", Convert.ToString(Addressdata[0].Field5));
                reportDocument.SetParameterValue("Employe_Name", Convert.ToString(FrmLogin.EmployeName));
                reportDocument.SetParameterValue("GSTno", Convert.ToString(Addressdata[0].Field6));
                reportDocument.SetParameterValue("Address", Convert.ToString(Addressdata[0].Field4));
                reportDocument.SetParameterValue("Designation", Convert.ToString(Addressdata[0].Field8));
                string devicename = System.Environment.MachineName;
                reportDocument.SetParameterValue("Device_name", devicename);
                /*End Header section*/

                /* report header Name*/
                reportDocument.SetParameterValue("OutHeading", "Zout");

                crvout.ReuseParameterValuesOnRefresh = true;
                //crvout.RefreshReport();
                crvout.ReportSource = reportDocument;
                if (Convert.ToInt32(AutoPrint) == Convert.ToInt32(AutoPrintToPrinter.Yes))
                {
                    if (selectedPrinters_TH.Count > 0)
                        SetDefaultPrinter(selectedPrinters_TH[0]);

                    //if (XYDalvalue == false || DalDalvalue == false)
                    reportDocument.PrintToPrinter(1, false, 0, 0);
                }

                if (Convert.ToInt32(Save_YZout) == Convert.ToInt32(Save_Copy_YZout.Yes))
                {
                    string folderPath = YZPath + "\\Yout";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    #region pdf Generate
                    string currentDateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    pdfFileName = $"{printname}_{currentDateTime}.pdf";
                    reportpdfPath = Path.Combine(folderPath, pdfFileName);
                    // Export report to PDF
                    reportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, reportpdfPath);
                    #endregion

                    MessageBox.Show($"PDF saved at:\n{reportpdfPath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("ZOut Page PrintXout()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.YZout));
            }
        }
        #endregion

        #region Yout
        public void PrintYout()
        {
            try
            {
                printname = "Yout";
                DataSet dsReport = new DataSet();

                string Drawerassigntime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                DataTable DT_Drawer = new DataTable("DT_Drawer");
                DataTable DT_Geninfo = new DataTable("DT_Geninfo");
                DataTable DT_Products = new DataTable("DT_Products");
                DataTable dtgeneralsettings = new DataTable("DT_GeneralSettings");
                DataTable DT_Return = new DataTable("DT_Return");

                BllSearch<DmnOutDrawer> bllDrawer = new BllSearch<DmnOutDrawer>();
                BllSearch<DmnOutGenInfo> bllGeninfo = new BllSearch<DmnOutGenInfo>();
                BllSearch<DmnOutCard> bllProducts = new BllSearch<DmnOutCard>();
                BllSearch<DmnGeneralSetting> bllgeneralsetting = new BllSearch<DmnGeneralSetting>();
                BllSearch<DmnReturn> bllReturn = new BllSearch<DmnReturn>();

                #region GeneralSettings
                DmnSearch dmnGeneralSetting = new DmnSearch();
                dmnGeneralSetting.Select = "BillTaxType,FooterContent,CompanyNameOtherLanguage,CompanyAddressOtherLanguage,FooterOtherLanguage";
                dmnGeneralSetting.Table = "tblGeneralSetting";
                dmnGeneralSetting.WhereNdGroupBy = " where BranchId=" + FrmLogin.BranchId;
                var generalsett = bllgeneralsetting.GetSearchData("prdSearch", dmnGeneralSetting);
                if (generalsett.Count > 0)
                {
                    if (PrintBillType == Convert.ToInt32(SalePrintbilltype.English))
                    {
                        var BillTaxType = Convert.ToInt32(generalsett[0].BillTaxType);
                        var FooterContent = Convert.ToString(generalsett[0].FooterContent);
                        var CompanyNameOtherLanguage = Convert.ToString(generalsett[0].CompanyNameOtherLanguage);
                        var CompanyAddressOtherLanguage = Convert.ToString(generalsett[0].CompanyAddressOtherLanguage);
                        var FooterOtherLanguage = Convert.ToString(generalsett[0].FooterOtherLanguage);
                        using (var reader = ObjectReader.Create(generalsett, "BillTaxType", "FooterContent", "CompanyNameOtherLanguage", "CompanyAddressOtherLanguage", "FooterOtherLanguage"))
                        {
                            dtgeneralsettings.Load(reader);
                        }
                    }
                }
                #endregion

                #region Drawer Assign time
                DmnSearch dmnSearchDetBill = new DmnSearch();
                BllSearch<Dmndrawer> bllSearch = new BllSearch<Dmndrawer>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "CreatedDateTime ";
                dmnSearch.Table = "tblDrawerAssign ";
                //dmnSearch.WhereNdGroupBy = "where BranchId =" + BranchId + " and CreatedBy=" + FrmLogin.UserId + " and Status=1";
                dmnSearch.WhereNdGroupBy = "where BranchId =" + BranchId + " and DName='" + System.Environment.MachineName + "' and Status=1";
                var itemsource = bllSearch.GetSearchData("prdsearch", dmnSearch);
                if (itemsource.Count > 0)
                {
                    Drawerassigntime = itemsource[0].CreatedDateTime.ToString("yyyy-MM-dd HH:mm");
                }
                #endregion      

                #region Products
                DmnSearch dmnSearchProduct = new DmnSearch();
                dmnSearchProduct.Select = "SUM(d.Quantity) AS TotalQuantity,d.ItemName as Itemname, CAST(SUM(d.DiscountAmount) AS decimal(18,2)) AS TotalDiscount, CAST(sum(d.NetAmount) AS decimal(18,2)) TotalAmount,g.Caption as ProductType,d.ProductType as ProductId,CAST(sum(d.TaxAmount) AS decimal(18,2)) as Totaltax ";
                dmnSearchProduct.Table = "tblSalesDetails d LEFT JOIN tblSalesMaster m ON d.SalesMasterId = m.SalesMasterId and m.Status=1 LEFT JOIN tblGeneralOptions g ON d.ProductType =g.GTypeSubValue1 and GTypeId=53";
                dmnSearchProduct.WhereNdGroupBy = " WHERE m.BranchId = " + BranchId + " and d.Status= 1 and m.CreatedBy=" + FrmLogin.UserId + " and m.CreatedDateTime >= '" + Drawerassigntime + "' AND m.CreatedDateTime <= SYSDATETIME() GROUP BY  d.ItemName ,g.Caption,d.ProductType";
                var ProductSaleResult = bllProducts.GetSearchData("prdSearch", dmnSearchProduct);
                using (var reader = ObjectReader.Create(ProductSaleResult, "TotalQuantity", "Itemname", "TotalDiscount", "TotalAmount", "Totaltax", "ProductType", "ProductId"))
                {
                    DT_Products.Load(reader);
                }
                #endregion       

                #region Drawer
                DmnSearch dmnSearchDrawer = new DmnSearch();
                dmnSearchDrawer.Select = "  0 as InitialAmount,0 as LoanAmount,0 as Drops,sum(NetAmount) as TotalAmount";
                dmnSearchDrawer.Table = "tblSalesMaster WHERE BranchId = " + BranchId + " and PaymentMode = 1 and CreatedBy = " + FrmLogin.UserId + "and status=1 and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME() " +
                    " union all select InitilAmount as InitialAmount,LoanAmount,Drops,0 TotalAmount  from tblDrawerAssign";
                dmnSearchDrawer.WhereNdGroupBy = " where status = 1 and BranchId = " + BranchId + "and CreatedBy = " + FrmLogin.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME() ";
                var DrawerResult = bllDrawer.GetSearchData("prdSearch", dmnSearchDrawer);
                DmnOutDrawer Dresult = DrawerResult
                    .GroupBy(row => 1)
                    .Select(group => new DmnOutDrawer
                    {
                        InitialAmount = group.Sum(row => row.InitialAmount),
                        LoanAmount = group.Sum(row => row.LoanAmount),
                        Drops = group.Sum(row => row.Drops),
                        TotalAmount = group.Sum(row => row.TotalAmount)
                    })
                    .FirstOrDefault();

                using (var reader = ObjectReader.Create(new List<DmnOutDrawer> { Dresult }, "InitialAmount", "LoanAmount", "Drops", "TotalAmount"))
                {
                    DT_Drawer.Load(reader);
                }
                #endregion

                #region General Information
                DmnSearch dmnSearchGeninfo = new DmnSearch();
                dmnSearchGeninfo.Select = "[CREDIT CARD] as TotalCreditAmt,[DEBIT CARD] as TotalDebitAmt, [UPI] as TotalUPI, [BANKING ONLINE] as TotalBankonline, [CASH] as TotalCash, [OTHERS] as Others, [COMPLEMENTRY] as TotalCompAmt,[Discount] as TotalDiscount,[TotalTax] as Totaltax,[NetAmount] as TotalAmount,[TotalQuantity] as TotalQuantity,[TotalPreTaxAmt] AS TotalPreTaxAmt,[MIDASCARD] as Totalmidascard";
                dmnSearchGeninfo.Table = "(select g.Caption as Caption,CAST(Sum(sm.NetAmount) AS decimal(18,2))  as amount from tblSalesMaster sm left join tblGeneralOptions g on g.GTypeSubId =sm.PaymentMode and g.GTypeId = 2 where sm.status = 1 and sm.BranchId = " + BranchId + " and sm.CreatedBy = " + FrmLogin.UserId + " and sm.CreatedDateTime >= '" + Drawerassigntime + "' AND sm.CreatedDateTime <= SYSDATETIME() GROUP BY  g.Caption,sm.NetAmount" +
                    " union all Select 'Discount' Caption ,CAST(Sum(DiscountAmount) AS decimal(18,2)) as amount from tblSalesMaster WHERE BranchId = " + BranchId + " and Status=1  and CreatedBy = " + FrmLogin.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME()" +
                    " Union all Select 'TotalPreTaxAmt', CAST(SUM(NetAmount) - SUM(TotalTax) AS decimal(18, 2)) as amount from tblSalesMaster WHERE BranchId = " + BranchId + " AND Status = 1 AND CreatedBy = " + FrmLogin.UserId + " AND CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME()" +
                    " Union all Select 'TotalTax' Caption ,CAST(Sum(TotalTax) AS decimal(18,2)) as amount  from tblSalesMaster WHERE BranchId = " + BranchId + " and Status=1  and CreatedBy = " + FrmLogin.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME() and PaymentMode!= " + Convert.ToInt32(PaymentType.COMPLEMENTRY) +
                    " Union all Select 'NetAmount' Caption,CAST(Sum(NetAmount) AS decimal(18,2)) as amount  from tblSalesMaster WHERE BranchId = " + BranchId + " and Status=1  and CreatedBy = " + FrmLogin.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME() and PaymentMode!= " + Convert.ToInt32(PaymentType.COMPLEMENTRY) +
                    " Union all Select 'TotalQuantity',Sum(TotalQuantity) as amount  from tblSalesMaster WHERE BranchId = " + BranchId + " and Status=1 and CreatedBy = " + FrmLogin.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME()" +
                    " UNION ALL SELECT g.Caption AS Caption,CAST(SUM(p.paidAmt) AS decimal(18, 2)) AS amount FROM tblPartPaymentDetails p INNER JOIN tblSalesMaster s ON p.SalesMasterId = s.SalesMasterId LEFT JOIN tblGeneralOptions g ON g.GTypeSubId = p.PayMode AND g.GTypeId = 2 WHERE p.PayMode!= 11 AND s.BranchId= " + BranchId + " AND s.PaymentMode = 12 AND s.Status = 1 AND s.CreatedBy = " + FrmLogin.UserId + " and s.CreatedDateTime >= '" + Drawerassigntime + "' AND s.CreatedDateTime <= SYSDATETIME() GROUP BY g.Caption" +
                    " UNION ALL  SELECT 'MIDASCARD' AS Caption, CAST(SUM(p.paidAmt) AS DECIMAL(18, 2)) AS amount FROM tblPartPaymentDetails p INNER JOIN tblSalesMaster s ON p.SalesMasterId = s.SalesMasterId WHERE p.PayMode = 11 AND s.PaymentMode = 12 AND s.BranchId= " + BranchId + " AND s.Status = 1 AND s.CreatedBy = " + FrmLogin.UserId + "AND s.CreatedDateTime >= '" + Drawerassigntime + "' AND s.CreatedDateTime <= SYSDATETIME()" +
                    ")ps PIVOT (SUM(amount)FOR Caption IN([CREDIT CARD],[DEBIT CARD],[UPI],[BANKING ONLINE],[CASH],[OTHERS],[COMPLEMENTRY],[Discount],[TotalTax],[NetAmount],[TotalQuantity],[MIDASCARD],[TotalPreTaxAmt])) AS pvt  ";
                dmnSearchGeninfo.WhereNdGroupBy = "";
                var GenInfoResult = bllGeninfo.GetSearchData("prdSearch", dmnSearchGeninfo);
                using (var reader = ObjectReader.Create(GenInfoResult,
                    "TotalCreditAmt",
                    "TotalDebitAmt",
                    "TotalUPI",
                    "TotalBankonline",
                    "TotalCash",
                    "Others",
                    "TotalCompAmt",
                    "TotalDiscount",
                    "TotalQuantity",
                    "Totaltax",
                    "TotalAmount",
                    "TotalPreTaxAmt",
                    "Totalmidascard"))
                {
                   
                    DT_Geninfo.Load(reader);
                }
                #endregion

                #region Return Summary
                DmnSearch dmnSearchRtn = new DmnSearch();
                dmnSearchRtn.Select = "sum(cashRefund) as TotalRtnCash,sum(cashbalanceRefund) as TotalRtnCard";
                dmnSearchRtn.Table = "tblrefund r";
                dmnSearchRtn.WhereNdGroupBy = "where r.Status = 1 AND r.BranchId = " + BranchId + " AND r.CreatedBy = " + FrmDashboard.UserId + "  AND r.CreatedDateTime >= '" + Drawerassigntime + "' AND r.CreatedDateTime <= SYSDATETIME()";
                var RtnResult = bllReturn.GetSearchData("prdSearch", dmnSearchRtn);
                using (var reader = ObjectReader.Create(RtnResult, "TotalRtnCash", "TotalRtnCard"))//"TotalRtnQty", "TotalRtnAmt"
                {
                    DT_Return.Load(reader);
                }
                #endregion

                dsReport.Tables.Add(DT_Products);
                dsReport.Tables.Add(DT_Drawer);
                dsReport.Tables.Add(DT_Geninfo);
                dsReport.Tables.Add(dtgeneralsettings);
                dsReport.Tables.Add(DT_Return);

                if (PrintBillType == Convert.ToInt32(SalePrintbilltype.English_Arabic))
                {
                    reportDocument.Load(RPTVWFile("CR_Zout_English_With_Arabic.rpt"));
                }
                else if (PrintBillType == Convert.ToInt32(SalePrintbilltype.English))
                {
                    reportDocument.Load(RPTVWFile("CR_Zout.rpt"));
                }
                else if (PrintBillType == Convert.ToInt32(SalePrintbilltype.Arabic))
                {

                }
                reportDocument.SetDataSource(dsReport);

                /* header section*/
                Addressdata = Get_CompantDetails();
   
                reportDocument.SetParameterValue("Loc_Name", Convert.ToString(Addressdata[0].Field1));
                reportDocument.SetParameterValue("City", Convert.ToString(Addressdata[0].Field5));
                reportDocument.SetParameterValue("Employe_Name", Convert.ToString(FrmLogin.EmployeName));
                reportDocument.SetParameterValue("GSTno", Convert.ToString(Addressdata[0].Field6));
                reportDocument.SetParameterValue("Address", Convert.ToString(Addressdata[0].Field4));
                reportDocument.SetParameterValue("Designation", Convert.ToString(Addressdata[0].Field8));
                string devicename = System.Environment.MachineName;
                GetRegadd();
                reportDocument.SetParameterValue("Regaddress", Regaddress); 
                reportDocument.SetParameterValue("Device_name", devicename);
                /*End Header section*/

                /* report header Name*/
                reportDocument.SetParameterValue("OutHeading", "Yout");

                crvout.ReuseParameterValuesOnRefresh = true;
                //crvout.RefreshReport();
                crvout.ReportSource = reportDocument;
                if (Convert.ToInt32(AutoPrint) == Convert.ToInt32(AutoPrintToPrinter.Yes))
                {
                    if (selectedPrinters_TH.Count > 0)
                        SetDefaultPrinter(selectedPrinters_TH[0]);

                    //if (XYDalvalue == false || DalDalvalue == false)
                    reportDocument.PrintToPrinter(1, false, 0, 0);
                }

                if (Convert.ToInt32(Save_YZout) == Convert.ToInt32(Save_Copy_YZout.Yes))
                {
                    string folderPath = YZPath + "\\Yout";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    #region pdf Generate
                    string currentDateTime = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    pdfFileName = $"{printname}_{currentDateTime}.pdf";
                    reportpdfPath = Path.Combine(folderPath, pdfFileName);
                    // Export report to PDF
                    reportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, reportpdfPath);
                    #endregion

                    MessageBox.Show($"PDF saved at:\n{reportpdfPath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("ZOut Page PrintXout()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.YZout));
            }
        }
        #endregion

        public void GetRegadd()
        {
            try
            {

                BllSearch<DmnHRegistration> bllSearchgen = new BllSearch<DmnHRegistration>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Name,Address,Email,Phone,Id,GSTNO,RegAddress";
                objSearch.Table = "tblHotelRegistration";
                objSearch.WhereNdGroupBy = " where Branchid=" + FrmDashboard.BranchId;
                objSearch.OrderBy = "";
                var itemSource = bllSearchgen.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    Regaddress = Convert.ToString(itemSource[0].RegAddress);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("ZOut Page GetRegadd()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.YZout));
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the ReportDocument object used by the Crystal Report Viewer
                ReportDocument reportDocument = crvout.ReportSource as ReportDocument;
                if (selectedPrinters_TH.Count > 0)
                    SetDefaultPrinter(selectedPrinters_TH[0]);
                reportDocument.PrintToPrinter(1, false, 0, 0);
            }
            catch (Exception ex)
            {
                clsgen.LogException("ZOut Page btn_print_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.YZout));
            }
        }

        private void btn_gmail_Click(object sender, EventArgs e)
        {
            if (Enabledefgmail)
                txt_gmail_report.Text = DefaultGmail;
            else
                SetPlaceholder(txt_gmail_report, "Enter Email Address");

            pnlgmail.Visible = !pnlgmail.Visible;

            if (pnlgmail.Visible)
            {
                pnlgmail.Location = new Point(
                    (pnlgmail.Parent.Width - pnlgmail.Width) / 2,
                    (pnlgmail.Parent.Height - pnlgmail.Height) / 2
                );
            }
        }
        private void SetPlaceholder(TextBox txtBox, string placeholder)
        {
            if (string.IsNullOrWhiteSpace(txtBox.Text))
            {
                txtBox.ForeColor = Color.Gray;
                txtBox.Text = placeholder;
                txtBox.GotFocus += (s, e) =>
                {
                    if (txtBox.Text == placeholder)
                    {
                        txtBox.Text = "";
                        txtBox.ForeColor = Color.Black;
                    }
                };
                txtBox.LostFocus += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(txtBox.Text))
                    {
                        txtBox.Text = placeholder;
                        txtBox.ForeColor = Color.Gray;
                    }
                };
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
                clsgen.LogException("ZOut Page ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.YZout));
            }

        }
        private void SetRoundedCorners(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }
        private void btn_whatsup_Click(object sender, EventArgs e)
        {

        }
        static bool IsValidEmail(string email)
        {
            // Check if the email string is null or empty
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            try
            {
                // Use a regular expression to validate the email address
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                return Regex.IsMatch(email, pattern);
            }
            catch (Exception ex)
            {
                // Catch any format exceptions thrown by the regular expression
                return false;
            }
        }
        private void btngmailsend_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_gmail_report.Text == "" || txt_gmail_report.Text == null)
                {
                    MessageBox.Show("Please Enter Mail ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_gmail_report.Focus();
                }
                else
                {
                    string email = txt_gmail_report.Text;
                    bool isValid = IsValidEmail(email);

                    if (isValid)
                    {
                        string name = "Sir";
                        MailMessage message = new MailMessage();
                        SmtpClient smtp = new SmtpClient();
                        message.From = new MailAddress("rahulcrpanthalloor@gmail.com");
                        message.To.Add(new MailAddress(txt_gmail_report.Text.Trim()));
                        message.Subject = printname;
                        message.IsBodyHtml = true;
                        message.Body = "Yout";

                        Attachment attachment = new Attachment(reportpdfPath);
                        message.Attachments.Add(attachment);

                        smtp.Port = 587; // use port 587 for TLS
                        smtp.Host = "smtp.gmail.com"; //for gmail host                 
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential("rahulcrpanthalloor@gmail.com", "kuws pilw uvlw ylgf");
                        smtp.EnableSsl = true; // enable SSL/TLS
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                        smtp.Send(message);

                        //DmnGmail dmngmail = new DmnGmail();
                        //BllGmail Bllgmail = new BllGmail();
                        //dmngmail.Gid = 0;
                        //dmngmail.Salemasterid = Convert.ToString(invoiceno_api);
                        //dmngmail.Phone = Convert.ToString(txt_ph_report.Text);
                        //dmngmail.LedgerId = 0;
                        //dmngmail.BranchId = FrmDashBoard.BranchId;
                        //dmngmail.CreatedBy = FrmDashBoard.UserId;
                        //dmngmail.Status = 1;
                        //var InsertStatus = Bllgmail.AddEditDataGmail("prdInsertGmail", dmngmail);

                        MessageBox.Show("Mail sent successfully", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Please Enter Valid Mail ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txt_gmail_report.Focus();
                    }

                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("ZOut Page btngmailsend_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.YZout));
            }
        }

        private void btngmailcancel_Click(object sender, EventArgs e)
        {
            pnlgmail.Visible = false;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private string RPTVWFile(string RepFName)
        {
            string fstr1 = "";

            if (RPTLocalVw == false)
            {
                fstr1 = XYZPath + RepFName;
            }
            else
            {
                //fstr1 = @"D:\mPOS\mPOS\MarginPOSNew\Reports\" + RepFName;
                //fstr1 = @ConfigurationManager.AppSettings["ReportLocalPath"] + RepFName;
            }
            return fstr1;
        }

        private List<DmnGridFields> Get_CompantDetails()
        {
            try
            {
                int empid = Drstaffid > 0 ? Drstaffid : FrmLogin.UserId;

                BllSearch<DmnGridFields> bllSearchcompany = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearchcompany = new DmnSearch();
                dmnSearchcompany.Select = " l.Name as Field1, l.Email as Field2, l.Phone as Field3, l.Address as Field4, l.city as Field5, l.GSTNO as Field6, e.EmployeeName as Field7, d.DesignationName as Field8";
                dmnSearchcompany.Table = "tblHotelRegistration l LEFT JOIN tblUserAccount u ON u.UserAccountId = " + empid + " LEFT JOIN tblEmployee e ON e.EmployeeId = u.EmployeeId LEFT JOIN tblDesignation d ON d.DesignationId = e.DesignationId";
                dmnSearchcompany.WhereNdGroupBy = " WHERE l.Id = " + BranchId;

                var Addressdata = bllSearchcompany.GetSearchData("prdSearch", dmnSearchcompany);
                return Addressdata;
            }
            catch (Exception ex)
            {
                clsgen.LogException("ZOut Page Get_CompantDetails()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.YZout));
                return new List<DmnGridFields>();
            }
        }


    }
}
