using BLL;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using FoodandBeverage.Forms;
using System.IO;
using CrystalDecisions.Shared;
using iTextSharp.text.pdf;
using System.Runtime.InteropServices;
using FastMember;
using FoodandBeverage.Classes;
using FoodandBeverage.Interfaces;
using System.Linq;

namespace FoodandBeverage.SaleBill
{
    public partial class FrmZout : Form
    {
        public int flag;
        public int Drstaffid;
        public string RPTPath = Convert.ToString(FrmDashboard.SaleBillPath);
        public string printname = "";
        string folderPath = "";
        string pdfFileName = "";
        string reportpdfPath = "";


        public FrmZout(int value, int Drawerstaffid)
        {
            flag = value;
            Drstaffid = Drawerstaffid;
            InitializeComponent();
        }
        ClsGeneral clsgen = new ClsGeneral();

        private void FrmZout_Load(object sender, EventArgs e)
        {
            PrintAll();
        }
        public void PrintAll()
        {
            try
            {
                if (flag == 1)
                {
                    PrintZout();

                }
                else if (flag == 2)
                {
                    PrintYout();

                }

                else if (flag == 3)
                {
                    PrintDailyCollection();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("ZOut page FrmZout_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
                //MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void PrintDetailsCollection()
        {
            try
            {
                printname = "Detailed Collection Report";

                #region Delcration
                string pdffolderPath = null;
                TimeSpan OpeningDate = TimeSpan.Zero;
                BllSearch<DmnSaleBillHeaders> bllSearchHeaders = new BllSearch<DmnSaleBillHeaders>();
                BllSearch<DmnOutCard> bllProducts = new BllSearch<DmnOutCard>();
                BllSearch<DmnOutDrawer> bllDrawer = new BllSearch<DmnOutDrawer>();
                BllSearch<DmnOutGenInfo> bllGeninfo = new BllSearch<DmnOutGenInfo>();


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

                #region Start time
                DmnSearch dmnSearchDetBill = new DmnSearch();
                BllSearch<Dmndrawer> bllSearch = new BllSearch<Dmndrawer>();
                DmnSearch dmnSearch = new DmnSearch();
                DateTime currentDate = DateTime.Now;
                DayOfWeek dayOfWeek = currentDate.DayOfWeek;
                int dayNumber = ((int)dayOfWeek + 6) % 7 + 1;

                dmnSearch.Select = "OpeningTime openTime";
                dmnSearch.Table = "tblHotelRegistration";
                dmnSearch.WhereNdGroupBy = "where Branchid=" + FrmDashboard.BranchId;
                var itemsource = bllSearch.GetSearchData("prdsearch", dmnSearch);
                if (itemsource.Count > 0)
                {
                    OpeningDate = itemsource[0].openTime;
                }
                String dt = DateTime.Now.Date.ToString("yyyy-MM-dd") + " " + itemsource[0].openTime;
                #endregion

                #region Staff List
                DmnSearch dmnSearchStaff = new DmnSearch();
                BllSearch<DmnGridFields> bllSearchStaff = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearchstaff = new DmnSearch();

                dmnSearchstaff.Select = "CreatedBy as Field1";
                dmnSearchstaff.Table = "tblSalesMaster ";
                dmnSearchstaff.WhereNdGroupBy = " where status=1 and  BranchId = " + FrmDashboard.BranchId + " and CONVERT(DATE,CreatedDateTime) = CONVERT(DATE, SYSDATETIME()) "
                    + " AND CONVERT(TIME, CreatedDateTime) >= '" + dt +
                    "' And CONVERT(TIME, CreatedDateTime) <= CONVERT(TIME, SYSDATETIME()) group by CreatedBy";
                var StaffResult = bllSearchStaff.GetSearchData("prdsearch", dmnSearchstaff);

                #endregion

                int Staffid = 0;
                for (int i = 0; i < StaffResult.Count; i++)
                {
                    ReportDocument reportDocument = new ReportDocument();
                    DataSet dsReport = new DataSet();
                    DataTable DT_Header = new DataTable("DT_Header");
                    DataTable DT_Products = new DataTable("DT_Products");
                    DataTable DT_Drawer = new DataTable("DT_Drawer");
                    DataTable DT_GeneralInfo = new DataTable("DT_GeneralInfo");

                    Staffid = Convert.ToInt32(StaffResult[i].Field1);

                    #region Hotel Header
                    DmnSearch dmnSearchInvoiceHeaders = new DmnSearch();
                    dmnSearchInvoiceHeaders.Select = "Name,Address,Email,Phone,GSTNO";
                    dmnSearchInvoiceHeaders.Table = "tblHotelRegistration";
                    dmnSearchInvoiceHeaders.WhereNdGroupBy = " where Branchid=" + FrmDashboard.BranchId;
                    var invoiceBillHeaders = bllSearchHeaders.GetSearchData("prdSearch", dmnSearchInvoiceHeaders);
                    using (var reader = ObjectReader.Create(invoiceBillHeaders, "Name", "Address", "Email", "Phone", "GSTNo"))
                    {
                        DT_Header.Load(reader);
                    }
                    #endregion

                    #region Products
                    DmnSearch dmnSearchProduct = new DmnSearch();
                    dmnSearchProduct.Select = "SUM(d.Quantity) AS TotalQuantity,d.ItemName as Itemname, SUM(d.DiscountAmount) AS TotalDiscount, sum(d.NetAmount) TotalAmount,sum(d.TaxAmount) as Totaltax";
                    dmnSearchProduct.Table = "tblSalesDetails d LEFT JOIN tblSalesMaster m ON d.SalesMasterId = m.SalesMasterId and m.Status=1" +
                    " LEFT JOIN tblGeneralOptions g ON d.ProductType =g.GTypeSubValue1 and GTypeId=53 WHERE d.Status= 1 and m.CreatedBy=" + Staffid + " and m.CreatedDateTime >= '" + dt + "' AND m.CreatedDateTime <= SYSDATETIME() GROUP BY d.ItemId, d.ItemName ,g.Caption";
                    dmnSearchProduct.WhereNdGroupBy = "";
                    var ProductSaleResult = bllProducts.GetSearchData("prdSearch", dmnSearchProduct);
                    using (var reader = ObjectReader.Create(ProductSaleResult, "TotalQuantity", "Itemname", "TotalDiscount", "TotalAmount", "Totaltax", "ProductType", "ProductId"))
                    {
                        DT_Products.Load(reader);
                    }
                    #endregion
                    #region Drawer
                    DmnSearch dmnSearchDrawer = new DmnSearch();
                    dmnSearchDrawer.Select = " 0 as InitialAmount,0 as LoanAmount,0 as Drops,sum(TotalAmount) as TotalNetAmount";
                    dmnSearchDrawer.Table = "tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and PaymentMode = 1 and CreatedBy = " + Staffid + "and status=1 and CreatedDateTime >= '" + dt + "' AND CreatedDateTime < SYSDATETIME() " +
                        "union select InitilAmount as InitialAmount,LoanAmount,Drops,0 TotalAmount from tblDrawerAssign where status=1 and CreatedBy = " + Staffid;
                    dmnSearchDrawer.WhereNdGroupBy = "";
                    var DrawerResult = bllDrawer.GetSearchData("prdSearch", dmnSearchDrawer);
                    DmnOutDrawer Dresult = DrawerResult
                        .GroupBy(row => 1)
                        .Select(group => new DmnOutDrawer
                        {
                            InitialAmount = group.Sum(row => row.InitialAmount),
                            LoanAmount = group.Sum(row => row.LoanAmount),
                            Drops = group.Sum(row => row.Drops),
                            TotalNetAmount = group.Sum(row => row.TotalNetAmount)
                        })
                        .FirstOrDefault();

                    using (var reader = ObjectReader.Create(new List<DmnOutDrawer> { Dresult }, "InitialAmount", "LoanAmount", "Drops", "TotalNetAmount"))
                    {
                        DT_Drawer.Load(reader);
                    }
                    #endregion

                    #region General Information
                    DmnSearch dmnSearchGeninfo = new DmnSearch();
                    dmnSearchGeninfo.Select = "[CREDIT CARD] as TotalCreditAmt,[DEBIT CARD] as TotalDebitAmt, [UPI] as TotalUPI, [BANKING ONLINE] as TotalBankonline, [CASH] as TotalCash, [OTHERS] as Others,[Discount] as TotalDiscount,[TotalTax] as Totaltax,[NetAmount] as TotalAmt,[TotalQuantity] as TotalQuantity";
                    dmnSearchGeninfo.Table = "(select g.Caption as Caption,Sum(sm.NetAmount) as amount from tblSalesMaster sm left join tblGeneralOptions g on g.GTypeSubId =sm.PaymentMode and g.GTypeId = 2 where sm.status = 1 and sm.BranchId = " + FrmDashboard.BranchId + " and sm.CreatedBy = " + Staffid + " and sm.CreatedDateTime >= '" + dt + "' AND sm.CreatedDateTime <= SYSDATETIME() GROUP BY  g.Caption,sm.NetAmount" +
                        " Union all Select 'Discount' Caption ,Sum(DiscountAmount) as amount from tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and Status=1  and CreatedBy = " + Staffid + " and CreatedDateTime >= '" + dt + "' AND CreatedDateTime <= SYSDATETIME()" +
                        " Union all Select 'TotalTax' Caption ,Sum(TotalTax) as amount  from tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and Status=1  and CreatedBy = " + FrmDashboard.UserId + " and CreatedDateTime >= '" + dt + "' AND CreatedDateTime <= SYSDATETIME()" +
                        " Union all Select 'TotalQuantity' Caption ,Sum([TotalQuantity]) as totqty  from tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and Status=1  and CreatedBy = " + Staffid + " and CreatedDateTime >= '" + dt + "' AND CreatedDateTime <= SYSDATETIME()  " +

                        ")ps PIVOT (SUM(amount)FOR Caption IN([CREDIT CARD],[DEBIT CARD],[UPI],[BANKING ONLINE],[CASH],[OTHERS],[Discount],[TotalTax],[NetAmount],[TotalQuantity])) AS pvt  ";
                    dmnSearchGeninfo.WhereNdGroupBy = "";
                    var GenInfoResult = bllGeninfo.GetSearchData("prdSearch", dmnSearchGeninfo);
                    using (var reader = ObjectReader.Create(GenInfoResult, "Others", "TotalCash", "TotalBankonline", "TotalUPI", "TotalDebitAmt", "TotalCreditAmt", "TotalQuantity", "TotalDiscount", "Totaltax", "TotalAmt"))
                    {
                        DT_GeneralInfo.Load(reader);
                    }
                    #endregion

                    dsReport.Tables.Add(DT_Products);
                    dsReport.Tables.Add(DT_Header);
                    dsReport.Tables.Add(DT_Drawer);
                    dsReport.Tables.Add(DT_GeneralInfo);

                    reportDocument.Load(RPTPath + @"\CR_Zout.rpt");
                    reportDocument.SetDataSource(dsReport);


                    string pdfPath = Path.Combine(pdffolderPath, "Bill" + i + ".pdf");
                    reportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath);
                }
                   
                // Output file path for the merged PDF
                string mergedPdfPath = "merged.pdf";
                pdffolderPath = Application.StartupPath + "\\DetailsOUT_PDF";

  

                MergePdfFiles(pdffolderPath, mergedPdfPath);
                reportpdfPath = Application.StartupPath + "\\DetailscollReport_" + DateTime.Now + ".pdf";
                //DisplayPdfInWebBrowser(pdfFilePath);
                crvout.Visible = true;
                this.Hide();

            }
            
            catch (Exception ex)
            {
                clsgen.LogException("ZOut Page PrintDetailsCollection()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        public void PrintDailyCollection()
        {
            try 
            {
             printname = "Daily Collection Report";
            ReportDocument reportDocument = new ReportDocument();
            DataSet dsReport = new DataSet();

            TimeSpan OpeningDate = TimeSpan.Zero;

            DataTable DT_Header = new DataTable("DT_Header");
            DataTable DT_Products = new DataTable("DT_Products");
            DataTable DT_Drawer = new DataTable("DT_Drawer");
            DataTable DT_GeneralInfo = new DataTable("DT_GeneralInfo");

            BllSearch<DmnSaleBillHeaders> bllSearchHeaders = new BllSearch<DmnSaleBillHeaders>();
            BllSearch<DmnOutCard> bllProducts = new BllSearch<DmnOutCard>();
            BllSearch<DmnOutDrawer> bllDrawer = new BllSearch<DmnOutDrawer>();
            BllSearch<DmnOutGenInfo> bllGeninfo = new BllSearch<DmnOutGenInfo>();

                #region Start time
                DmnSearch dmnSearchDetBill = new DmnSearch();
                BllSearch<Dmndrawer> bllSearch = new BllSearch<Dmndrawer>();
                DmnSearch dmnSearch = new DmnSearch();
              

                dmnSearch.Select = "OpeningTime openTime";
                dmnSearch.Table = "tblHotelRegistration";
                dmnSearch.WhereNdGroupBy = "where Branchid=" + FrmDashboard.BranchId;
                var itemsource = bllSearch.GetSearchData("prdsearch", dmnSearch);
                if (itemsource.Count > 0)
                {
                    OpeningDate = itemsource[0].openTime;
                }
                String dt = DateTime.Now.Date.ToString("yyyy-MM-dd") +" " +itemsource[0].openTime;
                #endregion
                #region Hotel Header
                DmnSearch dmnSearchInvoiceHeaders = new DmnSearch();
                dmnSearchInvoiceHeaders.Select = "Name,Address,Email,Phone,GSTNO";
                dmnSearchInvoiceHeaders.Table = "tblHotelRegistration";
                dmnSearchInvoiceHeaders.WhereNdGroupBy = " where Branchid=" + FrmDashboard.BranchId;
                var invoiceBillHeaders = bllSearchHeaders.GetSearchData("prdSearch", dmnSearchInvoiceHeaders);
                using (var reader = ObjectReader.Create(invoiceBillHeaders, "Name", "Address", "Email", "Phone", "GSTNo"))
                {
                    DT_Header.Load(reader);
                }
                #endregion

                #region Products
                DmnSearch dmnSearchProduct = new DmnSearch();
                dmnSearchProduct.Select = "SUM(d.Quantity) AS TotalQuantity,d.ItemName as Itemname, SUM(d.DiscountAmount) AS TotalDiscount, sum(d.NetAmount) TotalAmount,sum(d.TaxAmount) as Totaltax";
                dmnSearchProduct.Table = "tblSalesDetails d LEFT JOIN tblSalesMaster m ON d.SalesMasterId = m.SalesMasterId and m.Status=1" +
                " LEFT JOIN tblGeneralOptions g ON d.ProductType =g.GTypeSubValue1 and GTypeId=53 WHERE d.Status= 1 and m.CreatedBy=" + FrmDashboard.UserId + " and m.CreatedDateTime >= '" + dt+ "' AND m.CreatedDateTime <= SYSDATETIME() GROUP BY d.ItemId, d.ItemName ,g.Caption";
                dmnSearchProduct.WhereNdGroupBy = "";
                var ProductSaleResult = bllProducts.GetSearchData("prdSearch", dmnSearchProduct);
                using (var reader = ObjectReader.Create(ProductSaleResult, "TotalQuantity", "Itemname", "TotalDiscount", "TotalAmount", "Totaltax"))
                {
                    DT_Products.Load(reader);
                }
                #endregion

                #region Drawer
                DmnSearch dmnSearchDrawer = new DmnSearch();
                dmnSearchDrawer.Select = " 0 as InitialAmount,0 as LoanAmount,0 as Drops,sum(TotalAmount) as TotalNetAmount";
                dmnSearchDrawer.Table = "tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and PaymentMode = 1 and CreatedBy = " + FrmDashboard.UserId + "and status=1 and CreatedDateTime >= '" + dt + "' AND CreatedDateTime < SYSDATETIME() " +
                    "union select InitilAmount as InitialAmount,LoanAmount,Drops,0 TotalAmount from tblDrawerAssign where status=1 and CreatedBy = " + FrmDashboard.UserId ;
                dmnSearchDrawer.WhereNdGroupBy = "";
                var DrawerResult = bllDrawer.GetSearchData("prdSearch", dmnSearchDrawer);
                DmnOutDrawer Dresult = DrawerResult
                    .GroupBy(row => 1)
                    .Select(group => new DmnOutDrawer
                    {
                        InitialAmount = group.Sum(row => row.InitialAmount),
                        LoanAmount = group.Sum(row => row.LoanAmount),
                        Drops = group.Sum(row => row.Drops),
                        TotalNetAmount = group.Sum(row => row.TotalNetAmount),
                        TotalCardCashRefund = group.Sum(row => row.TotalCardCashRefund),
                        TotalCashRefund = group.Sum(row => row.TotalCashRefund)
                    })
                    .FirstOrDefault();

                using (var reader = ObjectReader.Create(new List<DmnOutDrawer> { Dresult }, "InitialAmount", "LoanAmount", "Drops", "TotalNetAmount"))
                {
                    DT_Drawer.Load(reader);
                }
                #endregion

                #region General Information
                DmnSearch dmnSearchGeninfo = new DmnSearch();
                dmnSearchGeninfo.Select = "[CREDIT CARD] as TotalCreditAmt,[DEBIT CARD] as TotalDebitAmt, [UPI] as TotalUPI, [BANKING ONLINE] as TotalBankonline, [CASH] as TotalCash, [OTHERS] as Others,[Discount] as TotalDiscount,[TotalTax] as Totaltax,[NetAmount] as TotalAmt,[TotalQuantity] as TotalQuantity";
                dmnSearchGeninfo.Table = "(select g.Caption as Caption,Sum(sm.NetAmount) as amount from tblSalesMaster sm left join tblGeneralOptions g on g.GTypeSubId =sm.PaymentMode and g.GTypeId = 2 where sm.status = 1 and sm.BranchId = " + FrmDashboard.BranchId + " and sm.CreatedBy = " + FrmDashboard.UserId + " and sm.CreatedDateTime >= '" + dt + "' AND sm.CreatedDateTime <= SYSDATETIME() GROUP BY  g.Caption,sm.NetAmount" +
                    " Union all Select 'Discount' Caption ,Sum(DiscountAmount) as amount from tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and Status=1  and CreatedBy = " + FrmDashboard.UserId + " and CreatedDateTime >= '" + dt + "' AND CreatedDateTime <= SYSDATETIME()" +
                    " Union all Select 'TotalTax' Caption ,Sum(TotalTax) as amount  from tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and Status=1  and CreatedBy = " + FrmDashboard.UserId + " and CreatedDateTime >= '" + dt + "' AND CreatedDateTime <= SYSDATETIME()" +
                    " Union all Select 'TotalQuantity' Caption ,Sum([TotalQuantity]) as totqty  from tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and Status=1  and CreatedBy = " + FrmDashboard.UserId + " and CreatedDateTime >= '" + dt + "' AND CreatedDateTime <= SYSDATETIME() and PaymentMode!= " + Convert.ToInt32(PaymentType.COMPLEMENTRY) +

                    ")ps PIVOT (SUM(amount)FOR Caption IN([CREDIT CARD],[DEBIT CARD],[UPI],[BANKING ONLINE],[CASH],[OTHERS],[Discount],[TotalTax],[NetAmount],[TotalQuantity])) AS pvt  ";
                dmnSearchGeninfo.WhereNdGroupBy = "";
                var GenInfoResult = bllGeninfo.GetSearchData("prdSearch", dmnSearchGeninfo);
                using (var reader = ObjectReader.Create(GenInfoResult, "Others", "TotalCash", "TotalBankonline", "TotalUPI", "TotalDebitAmt", "TotalCreditAmt", "TotalQuantity", "TotalDiscount", "Totaltax", "TotalAmt"))
                {
                    DT_GeneralInfo.Load(reader);
                }
                #endregion

                dsReport.Tables.Add(DT_Products);
                dsReport.Tables.Add(DT_Header);
                dsReport.Tables.Add(DT_Drawer);
                dsReport.Tables.Add(DT_GeneralInfo);


                reportDocument.Load(RPTPath + @"\CR_Zout.rpt");
                reportDocument.SetDataSource(dsReport);


                //reportDocument.SetParameterValue("OutHeading", "Yout");

                crvout.ReuseParameterValuesOnRefresh = true;
                crvout.RefreshReport();
                crvout.ReportSource = reportDocument;


                //if (XYDalvalue == false || DalDalvalue == false)
                reportDocument.PrintToPrinter(1, false, 0, 0);

            }
            catch (Exception ex)
            {
                clsgen.LogException("ZOut Page PrintDailyCollection()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

}
        public void PrintZout()
        {
            try
            {

                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();

                string Drawerassigntime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                string Drawerassignouttime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                // DataTable DT_Cardsale = new DataTable("DT_Cardsale");

                DataTable DT_Header = new DataTable("DT_Header");
                DataTable DT_Products = new DataTable("DT_Products");
                DataTable DT_Drawer = new DataTable("DT_Drawer");
                DataTable DT_GeneralInfo = new DataTable("DT_GeneralInfo");

                BllSearch<DmnSaleBillHeaders> bllSearchHeaders = new BllSearch<DmnSaleBillHeaders>();
                BllSearch<DmnOutCard> bllProducts = new BllSearch<DmnOutCard>();
                BllSearch<DmnOutDrawer> bllDrawer = new BllSearch<DmnOutDrawer>();
                BllSearch<DmnOutGenInfo> bllGeninfo = new BllSearch<DmnOutGenInfo>();

                #region Drawer Assign time
                DmnSearch dmnSearchDetBill = new DmnSearch();
                BllSearch<Dmndrawer> bllSearch = new BllSearch<Dmndrawer>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "CreatedDateTime,ModifiedDateTime";
                dmnSearch.Table = "tblDrawerAssign ";
                dmnSearch.WhereNdGroupBy = "where DName='" + System.Environment.MachineName + "' and Status=1";
                var itemsource = bllSearch.GetSearchData("prdsearch", dmnSearch);
                if (itemsource.Count > 0)
                {
                    Drawerassigntime = itemsource[0].CreatedDateTime.ToString("yyyy-MM-dd HH:mm");
                    Drawerassignouttime = itemsource[0].ModifiedDateTime.ToString("yyyy-MM-dd HH:mm");
                }
                #endregion

                #region Hotel Header
                DmnSearch dmnSearchInvoiceHeaders = new DmnSearch();
                dmnSearchInvoiceHeaders.Select = "Name,Address,Email,Phone,GSTNO";
                dmnSearchInvoiceHeaders.Table = "tblHotelRegistration";
                dmnSearchInvoiceHeaders.WhereNdGroupBy = " where Branchid=" + FrmDashboard.BranchId;
                var invoiceBillHeaders = bllSearchHeaders.GetSearchData("prdSearch", dmnSearchInvoiceHeaders);
                using (var reader = ObjectReader.Create(invoiceBillHeaders, "Name", "Address", "Email", "Phone", "GSTNo"))
                {
                    DT_Header.Load(reader);
                }
                #endregion

                #region Products
                DmnSearch dmnSearchProduct = new DmnSearch();
                dmnSearchProduct.Select = "SUM(d.Quantity) AS TotalQuantity,d.ItemName as Itemname, SUM(d.DiscountAmount) AS TotalDiscount, sum(d.NetAmount) TotalAmount,sum(d.TaxAmount) as Totaltax";
                dmnSearchProduct.Table = "tblSalesDetails d LEFT JOIN tblSalesMaster m ON d.SalesMasterId = m.SalesMasterId and m.Status=1" +
                " LEFT JOIN tblGeneralOptions g ON d.ProductType =g.GTypeSubValue1 and GTypeId=53 WHERE d.Status= 1 and m.CreatedBy=" + FrmDashboard.UserId + " and m.CreatedDateTime >= '" + Drawerassigntime + "' AND m.CreatedDateTime <= SYSDATETIME() GROUP BY d.ItemId, d.ItemName ,g.Caption";
                dmnSearchProduct.WhereNdGroupBy = "";
                var ProductSaleResult = bllProducts.GetSearchData("prdSearch", dmnSearchProduct);
                using (var reader = ObjectReader.Create(ProductSaleResult, "TotalQuantity", "Itemname", "TotalDiscount", "TotalAmount", "Totaltax"))
                {
                    DT_Products.Load(reader);
                }
                #endregion

                #region Drawer
                DmnSearch dmnSearchDrawer = new DmnSearch();
                dmnSearchDrawer.Select = " 0 as InitialAmount,0 as LoanAmount,0 as Drops,sum(TotalAmount) as TotalNetAmount";
                dmnSearchDrawer.Table = "tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and PaymentMode = 1 and CreatedBy = " + FrmDashboard.UserId + "and status=1 and CreatedDateTime > '" + Drawerassigntime + "' AND CreatedDateTime < SYSDATETIME() " +
                    "union select InitilAmount as InitialAmount,LoanAmount,Drops,0 TotalAmount from tblDrawerAssign where status=1 and CreatedBy = " + FrmDashboard.UserId;
                dmnSearchDrawer.WhereNdGroupBy = "";
                var DrawerResult = bllDrawer.GetSearchData("prdSearch", dmnSearchDrawer);
                DmnOutDrawer Dresult = DrawerResult
                    .GroupBy(row => 1)
                    .Select(group => new DmnOutDrawer
                    {
                        InitialAmount = group.Sum(row => row.InitialAmount),
                        LoanAmount = group.Sum(row => row.LoanAmount),
                        Drops = group.Sum(row => row.Drops),
                        TotalNetAmount = group.Sum(row => row.TotalNetAmount),
                        TotalCardCashRefund = group.Sum(row => row.TotalCardCashRefund),
                        TotalCashRefund = group.Sum(row => row.TotalCashRefund)
                    })
                    .FirstOrDefault();

                using (var reader = ObjectReader.Create(new List<DmnOutDrawer> { Dresult }, "InitialAmount", "LoanAmount", "Drops", "TotalNetAmount"))
                {
                    DT_Drawer.Load(reader);
                }
                #endregion

                #region General Information
                DmnSearch dmnSearchGeninfo = new DmnSearch();
                dmnSearchGeninfo.Select = "[CREDIT CARD] as TotalCreditAmt,[DEBIT CARD] as TotalDebitAmt, [UPI] as TotalUPI, [BANKING ONLINE] as TotalBankonline, [CASH] as TotalCash, [OTHERS] as Others,[Discount] as TotalDiscount,[TotalTax] as Totaltax,[NetAmount] as TotalAmt,[TotalQuantity] as TotalQuantity";
                dmnSearchGeninfo.Table = "(select g.Caption as Caption,Sum(sm.NetAmount) as amount from tblSalesMaster sm left join tblGeneralOptions g on g.GTypeSubId =sm.PaymentMode and g.GTypeId = 2 where sm.status = 1 and sm.BranchId = " + FrmDashboard.BranchId + " and sm.CreatedBy = " + FrmDashboard.UserId + " and sm.CreatedDateTime >= '" + Drawerassigntime + "' AND sm.CreatedDateTime <= SYSDATETIME() GROUP BY  g.Caption,sm.NetAmount" +
                    " Union all Select 'Discount' Caption ,Sum(DiscountAmount) as amount from tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and Status=1  and CreatedBy = " + FrmDashboard.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME()" +
                    " Union all Select 'TotalTax' Caption ,Sum(TotalTax) as amount  from tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and Status=1  and CreatedBy = " + FrmDashboard.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME()" +
                    " Union all Select 'TotalQuantity' Caption ,Sum([TotalQuantity]) as totqty  from tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and Status=1  and CreatedBy = " + FrmDashboard.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME() and PaymentMode!= " + Convert.ToInt32(PaymentType.COMPLEMENTRY) +

                    ")ps PIVOT (SUM(amount)FOR Caption IN([CREDIT CARD],[DEBIT CARD],[UPI],[BANKING ONLINE],[CASH],[OTHERS],[Discount],[TotalTax],[NetAmount],[TotalQuantity])) AS pvt  ";
                dmnSearchGeninfo.WhereNdGroupBy = "";
                var GenInfoResult = bllGeninfo.GetSearchData("prdSearch", dmnSearchGeninfo);
                using (var reader = ObjectReader.Create(GenInfoResult, "Others", "TotalCash", "TotalBankonline", "TotalUPI", "TotalDebitAmt", "TotalCreditAmt", "TotalQuantity", "TotalDiscount", "Totaltax", "TotalAmt"))
                {
                    DT_GeneralInfo.Load(reader);
                }
                #endregion

                dsReport.Tables.Add(DT_Products);
                dsReport.Tables.Add(DT_Header);
                dsReport.Tables.Add(DT_Drawer);
                dsReport.Tables.Add(DT_GeneralInfo);


                reportDocument.Load(RPTPath + @"\CR_Zout.rpt");
                reportDocument.SetDataSource(dsReport);


                //reportDocument.SetParameterValue("OutHeading", "Yout");

                crvout.ReuseParameterValuesOnRefresh = true;
                crvout.RefreshReport();
                crvout.ReportSource = reportDocument;


                //if (XYDalvalue == false || DalDalvalue == false)
                reportDocument.PrintToPrinter(1, false, 0, 0);

            }
            catch (Exception ex)
            {
                clsgen.LogException("ZOut Page PrintZout()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void PrintYout()
        {
            try
            {

                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();

                string Drawerassigntime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                string Drawerassignouttime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                // DataTable DT_Cardsale = new DataTable("DT_Cardsale");

                DataTable DT_Header = new DataTable("DT_Header");
                DataTable DT_Products = new DataTable("DT_Products");
                DataTable DT_Drawer = new DataTable("DT_Drawer");
                DataTable DT_GeneralInfo = new DataTable("DT_GeneralInfo");

                BllSearch<DmnSaleBillHeaders> bllSearchHeaders = new BllSearch<DmnSaleBillHeaders>();
                BllSearch<DmnOutCard> bllProducts = new BllSearch<DmnOutCard>();
                BllSearch<DmnOutDrawer> bllDrawer = new BllSearch<DmnOutDrawer>();
                BllSearch<DmnOutGenInfo> bllGeninfo = new BllSearch<DmnOutGenInfo>();

                #region Drawer Assign time
                DmnSearch dmnSearchDetBill = new DmnSearch();
                BllSearch<Dmndrawer> bllSearch = new BllSearch<Dmndrawer>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "CreatedDateTime,ModifiedDateTime";
                dmnSearch.Table = "tblDrawerAssign ";
                dmnSearch.WhereNdGroupBy = "where DName='" + System.Environment.MachineName + "' and Status=1";
                var itemsource = bllSearch.GetSearchData("prdsearch", dmnSearch);
                if (itemsource.Count > 0)
                {
                    Drawerassigntime = itemsource[0].CreatedDateTime.ToString("yyyy-MM-dd HH:mm");
                    Drawerassignouttime = itemsource[0].ModifiedDateTime.ToString("yyyy-MM-dd HH:mm");
                }
                #endregion

                #region Hotel Header
                DmnSearch dmnSearchInvoiceHeaders = new DmnSearch();
                dmnSearchInvoiceHeaders.Select = "Name,Address,Email,Phone,GSTNO";
                dmnSearchInvoiceHeaders.Table = "tblHotelRegistration";
                dmnSearchInvoiceHeaders.WhereNdGroupBy = " where Branchid=" + FrmDashboard.BranchId;
                var invoiceBillHeaders = bllSearchHeaders.GetSearchData("prdSearch", dmnSearchInvoiceHeaders);
                using (var reader = ObjectReader.Create(invoiceBillHeaders, "Name", "Address", "Email", "Phone", "GSTNo"))
                {
                    DT_Header.Load(reader);
                }
                #endregion

                #region Products
                DmnSearch dmnSearchProduct = new DmnSearch();
                dmnSearchProduct.Select = "SUM(d.Quantity) AS TotalQuantity,d.ItemName as Itemname, SUM(d.DiscountAmount) AS TotalDiscount, sum(d.NetAmount) TotalAmount,sum(d.TaxAmount) as Totaltax";
                dmnSearchProduct.Table = "tblSalesDetails d LEFT JOIN tblSalesMaster m ON d.SalesMasterId = m.SalesMasterId and m.Status=1" +
                " LEFT JOIN tblGeneralOptions g ON d.ProductType =g.GTypeSubValue1 and GTypeId=53 WHERE d.Status= 1 and m.CreatedBy=" + FrmDashboard.UserId + " and m.CreatedDateTime >= '" + Drawerassigntime + "' AND m.CreatedDateTime <= SYSDATETIME() GROUP BY d.ItemId, d.ItemName ,g.Caption";
                dmnSearchProduct.WhereNdGroupBy = "";
                var ProductSaleResult = bllProducts.GetSearchData("prdSearch", dmnSearchProduct);
                using (var reader = ObjectReader.Create(ProductSaleResult, "TotalQuantity", "Itemname", "TotalDiscount", "TotalAmount", "Totaltax"))
                {
                    DT_Products.Load(reader);
                }
                #endregion

                #region Drawer
                DmnSearch dmnSearchDrawer = new DmnSearch();
                dmnSearchDrawer.Select = " 0 as InitialAmount,0 as LoanAmount,0 as Drops,sum(TotalAmount) as TotalNetAmount";
                dmnSearchDrawer.Table = "tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and PaymentMode = 1 and CreatedBy = " + FrmDashboard.UserId + "and status=1 and CreatedDateTime > '" + Drawerassigntime + "' AND CreatedDateTime < SYSDATETIME() " +
                    "union select InitilAmount as InitialAmount,LoanAmount,Drops,0 TotalAmount from tblDrawerAssign where status=1 and CreatedBy = " + FrmDashboard.UserId;
                dmnSearchDrawer.WhereNdGroupBy = "";
                var DrawerResult = bllDrawer.GetSearchData("prdSearch", dmnSearchDrawer);
                DmnOutDrawer Dresult = DrawerResult
                    .GroupBy(row => 1)
                    .Select(group => new DmnOutDrawer
                    {
                        InitialAmount = group.Sum(row => row.InitialAmount),
                        LoanAmount = group.Sum(row => row.LoanAmount),
                        Drops = group.Sum(row => row.Drops),
                        TotalNetAmount = group.Sum(row => row.TotalNetAmount),
                        TotalCardCashRefund = group.Sum(row => row.TotalCardCashRefund),
                        TotalCashRefund = group.Sum(row => row.TotalCashRefund)
                    })
                    .FirstOrDefault();

                using (var reader = ObjectReader.Create(new List<DmnOutDrawer> { Dresult }, "InitialAmount", "LoanAmount", "Drops", "TotalNetAmount"))
                {
                    DT_Drawer.Load(reader);
                }
                #endregion

                #region General Information
                DmnSearch dmnSearchGeninfo = new DmnSearch();
                dmnSearchGeninfo.Select = "[CREDIT CARD] as TotalCreditAmt,[DEBIT CARD] as TotalDebitAmt, [UPI] as TotalUPI, [BANKING ONLINE] as TotalBankonline, [CASH] as TotalCash, [OTHERS] as Others,[Discount] as TotalDiscount,[TotalTax] as Totaltax,[NetAmount] as TotalAmt,[TotalQuantity] as TotalQuantity";
                dmnSearchGeninfo.Table = "(select g.Caption as Caption,Sum(sm.NetAmount) as amount from tblSalesMaster sm left join tblGeneralOptions g on g.GTypeSubId =sm.PaymentMode and g.GTypeId = 2 where sm.status = 1 and sm.BranchId = " + FrmDashboard.BranchId + " and sm.CreatedBy = " + FrmDashboard.UserId + " and sm.CreatedDateTime >= '" + Drawerassigntime + "' AND sm.CreatedDateTime <= SYSDATETIME() GROUP BY  g.Caption,sm.NetAmount" +
                    " Union all Select 'Discount' Caption ,Sum(DiscountAmount) as amount from tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and Status=1  and CreatedBy = " + FrmDashboard.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME()" +
                    " Union all Select 'TotalTax' Caption ,Sum(TotalTax) as amount  from tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and Status=1  and CreatedBy = " + FrmDashboard.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME()" +
                    " Union all Select 'TotalQuantity' Caption ,Sum([TotalQuantity]) as totqty  from tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and Status=1  and CreatedBy = " + FrmDashboard.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME() and PaymentMode!= " + Convert.ToInt32(PaymentType.COMPLEMENTRY) +                   
                  
                    ")ps PIVOT (SUM(amount)FOR Caption IN([CREDIT CARD],[DEBIT CARD],[UPI],[BANKING ONLINE],[CASH],[OTHERS],[Discount],[TotalTax],[NetAmount],[TotalQuantity])) AS pvt  ";
                dmnSearchGeninfo.WhereNdGroupBy = "";
                var GenInfoResult = bllGeninfo.GetSearchData("prdSearch", dmnSearchGeninfo);
                using (var reader = ObjectReader.Create(GenInfoResult, "Others", "TotalCash", "TotalBankonline", "TotalUPI", "TotalDebitAmt", "TotalCreditAmt", "TotalQuantity", "TotalDiscount", "Totaltax", "TotalAmt"))
                {
                    DT_GeneralInfo.Load(reader);
                }
                #endregion

                dsReport.Tables.Add(DT_Products);
                dsReport.Tables.Add(DT_Header);
                dsReport.Tables.Add(DT_Drawer);
                dsReport.Tables.Add(DT_GeneralInfo);


                reportDocument.Load(RPTPath + @"\CR_Zout.rpt");
                reportDocument.SetDataSource(dsReport);

                
                //reportDocument.SetParameterValue("OutHeading", "Yout");

                crvout.ReuseParameterValuesOnRefresh = true;
                crvout.RefreshReport();
                crvout.ReportSource = reportDocument;
                

                //if (XYDalvalue == false || DalDalvalue == false)
                reportDocument.PrintToPrinter(1, false, 0, 0);

            }
            catch (Exception ex)
            {
                clsgen.LogException("ZOut Page PrintZout()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
