using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using ZXing.Common;
using ZXing;
using CrystalDecisions.Shared;
using Domain;
using FastMember;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using BLL;
using CustomMessageBox;
using FoodandBeverage.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
using FoodandBeverage.Interfaces;
using System.Linq;

namespace FoodandBeverage.SaleBill
{
    public partial class FrmSaleInvoice : Form
    {
        public int SalesMasterId = 0;
        public int paytypeid = 0;
        public int paymentid = 0;
        public int BillTypeData = 0;
        public static int BranchId = 0;
        public static int UserId = 0;
        public string folderPath = Application.StartupPath + "\\Barcode";
        private HttpClient httpClient;
        public static int BType = 0;
        public string RPTPath = Convert.ToString(FrmDashboard.SaleBillPath);
        public string locationname_api;
        public decimal netamount_api = 0;
        public string cusname_api;
        public string invoiceno_api;
        public string pdfPath = "";

        public FrmSaleInvoice(int masterId,int paytype,int paytid,int BillType )
        {
            SalesMasterId = masterId;
            paymentid = paytid;
            paytypeid = paytype;
            BillTypeData = BillType;
            InitializeComponent();
            httpClient = new HttpClient();
        }
        bool RPTLocalVw = false;

        #region Printer Default Settings
        [DllImport("winspool.drv", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string printerName);
        #endregion

        private void FrmSaleInvoice_Load(object sender, EventArgs e)
        {
            textb_shape();
            MessageCheck();
            PrintBill();
        }
        #region Message settings
        public void MessageCheck()
        {
            try
            {

                BllSearch<DmnSetting> bllSearch = new BllSearch<DmnSetting>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblsettings";
                dmnSearch.WhereNdGroupBy = " where Status=1 ";
                dmnSearch.OrderBy = "";
                var gridesource = bllSearch.GetSearchData("prdSearch", dmnSearch);

                if (gridesource.Count > 0)
                {
                    if (gridesource[0].Gmail)
                    {
                        btn_gmail.Visible = true;
                        txt_gmail_report.Visible = true;
                    }
                    else
                    {
                        btn_gmail.Visible = false;
                        txt_gmail_report.Visible = false;
                    }

                    if (gridesource[0].Whatsapp)
                    {
                        btn_whatsup.Visible = true;
                        txt_ph_report.Visible = true;
                    }
                    else
                    {
                        btn_whatsup.Visible = false;
                        txt_ph_report.Visible = false;
                    }
                }
                else
                {
                    btn_gmail.Visible = false;
                    txt_gmail_report.Visible = false;
                    btn_whatsup.Visible = false;
                    txt_ph_report.Visible = false;
                }

            }
            catch
            {

            }
        }
        #endregion

        #region Print Bill for Normal & Part 
        public void PrintBill()
        {
            try
            {
                string reportPath,orderdetailsids="", payname = "";
                decimal cashamount = 0, othetamount = 0, splitamount=0,I_TaxAmount = 0, I_CGSTAmount = 0, I_SGSTAmount = 0,I_VatAmount = 0;
                int splittype = 0;
                var  invpayment = new List<DmnGridFields>();

                #region Crystal Report Dclaration
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dtSalesHeader = new DataTable("DT_SaleHeader");
                DataTable dtSalesMaster = new DataTable("DT_salemaster");
                DataTable dtSalesDetail = new DataTable("DT_saledetails");

                BllSearch<DmnSaleBillHeaders> bllSearchHeaders = new BllSearch<DmnSaleBillHeaders>();
                BllSearch<DmnSalesInvoiceBillDetails> bllSearchDetails = new BllSearch<DmnSalesInvoiceBillDetails>();
                BllSearch<DmnSalesInvoiceBill> bllSearchInvoiceBill = new BllSearch<DmnSalesInvoiceBill>();

                DmnSearch dmnSearchInvoiceHeaders = new DmnSearch();
                #endregion

                dmnSearchInvoiceHeaders.Select = "Name,Address,Email,Phone,Id,GSTNO";
                dmnSearchInvoiceHeaders.Table = "tblHotelRegistration";
                dmnSearchInvoiceHeaders.WhereNdGroupBy = " where Branchid=" + FrmDashboard.BranchId;
                var invoiceBillHeaders = bllSearchHeaders.GetSearchData("prdSearch", dmnSearchInvoiceHeaders);
                //locationname_api = invoiceBillHeaders[0].Name;
                DmnSearch dmnSearchInvoiceBill = new DmnSearch();
                dmnSearchInvoiceBill.Select = " S.EntryNo,S.CreatedDateTime as EntryDate, " +
                                              " (Select Username from tblUserAccount where UserAccountId = '" + UserId + "') as Username, " +
                                              " S.TotalAmount,S.DiscountAmount,S.TotalDiscount,S.TotalTax,S.TotalCGST,S.TotalSGST,S.TotalVAT,S.TotalQuantity," +
                                              " S.NetAmount,S.BalAmnt,s.RcvAmnt,s.CusName,s.Email,s.Telephone,s.SalesAddress,s.OaAmount,s.OdAmount,Remarks,PaymentMode,s.PromoDId,s.PromoAmount,AdvanceAmount,ReservationTotal";
                dmnSearchInvoiceBill.Table = " tblSalesMaster as S";
                dmnSearchInvoiceBill.WhereNdGroupBy = " where S.SalesMasterId =" + SalesMasterId;
                var invoiceBillMasters = bllSearchInvoiceBill.GetSearchData("prdSearch", dmnSearchInvoiceBill);
                netamount_api = invoiceBillMasters[0].NetAmount;
                invoiceno_api = invoiceBillMasters[0].EntryNo;
                cusname_api = invoiceBillMasters[0].CusName;
                if (cusname_api == "" || cusname_api == null)
                    cusname_api = "Sir / Mam";
                //stroe barcode_img path header
                var imagePath = folderPath + "\\" + invoiceBillMasters[0].EntryNo + ".png";
                invoiceBillHeaders[0].barcode_img_path = imagePath;  
                 if ( (invoiceBillHeaders[0].Email == "" || invoiceBillHeaders[0].Email == null) && (invoiceBillHeaders[0].Phone == "" || invoiceBillHeaders[0].Phone == null))
                {
                    invoiceBillHeaders[0].Full_Address = invoiceBillHeaders[0].Address;
                }
                else if (invoiceBillMasters[0].Email == "" || invoiceBillMasters[0].Email == null)
                {
                    invoiceBillHeaders[0].Full_Address = invoiceBillHeaders[0].Address +  invoiceBillHeaders[0].Phone;
                }

                GenerateBarcode(invoiceBillMasters[0].EntryNo);
                //header part set data to dataset
                using (var reader = ObjectReader.Create(invoiceBillHeaders, "Name", "Address", "Email", "Phone", "LocId", "barcode_img_path", "GSTNo", "Full_Address"))
                {
                    dtSalesHeader.Load(reader);
                }

                #region Payment Table Fetching
                if (paytypeid == Convert.ToInt32(PaymentType.SPLIT))
                {
                    DmnSearch obj = new DmnSearch();
                    BllSearch<DmnGridFields> bllpayments = new BllSearch<DmnGridFields>();
                    obj.Select = "PayType Field1, Amount Field2,splittype Field3,OrderDetailsId Field4,BillCount Field5,OaAmount Field6,OdAmount Field7,PromoAmt Field8,DiscountAmount Field9,NumberCount Field10,BalAmnt Field11,RcvAmnt Field12";
                    obj.Table = "tblpayment";
                    obj.WhereNdGroupBy = " where payid=" + paymentid;
                     invpayment = bllpayments.GetSearchData("prdSearch", obj);
                    if (invpayment.Count > 0)
                    {
                        orderdetailsids = Convert.ToString(invpayment[0].Field4);
                        splittype = Convert.ToInt32(invpayment[0].Field3);
                    }     
                }
                else
                {
                    splittype = Convert.ToInt32(SplitType.Normal);
                }            
                #endregion

                DmnSearch dmnSearchInvoiceDetails = new DmnSearch();
                dmnSearchInvoiceDetails.Select = "S.ItemName ,S.Quantity,S.MRP,S.MarketPrice as SalesRate,S.NetAmount,S.Amount,S.TaxPer,s.DiscountAmount,s.TaxAmount as TotalTax,s.CGSTAmount TotalCGST,s.SGSTAmount TotalSGST,s.VatAmount as TotalVAT,s.SalesMasterId SalesMasterId,s.AV_id AV_id,s.AddonStatus AddonStatus,s.Prid Prid,s.VariatiionName VariatiionName";
                dmnSearchInvoiceDetails.Table = "tblSalesDetails as S";

                if (splittype == Convert.ToInt32(SplitType.Item))
                    dmnSearchInvoiceDetails.WhereNdGroupBy = " where S.SalesDetailsId IN (" + orderdetailsids.TrimEnd(',') + ")";
                else
                    dmnSearchInvoiceDetails.WhereNdGroupBy = " where S.SalesMasterId=" + SalesMasterId;

                dmnSearchInvoiceDetails.OrderBy = "order by  s.AddonStatus";
                var invoiceBillDetails = bllSearchDetails.GetSearchData("prdSearch", dmnSearchInvoiceDetails);

                #region Sorting Result
                var processedData = invoiceBillDetails.Select(item => new
                {
                    ItemName = !string.IsNullOrEmpty(item.ItemName)
                                   ? item.ItemName + (!string.IsNullOrEmpty(item.VariatiionName) ? " (" + item.VariatiionName + ")" : string.Empty)
                                   : item.ItemName,
                    Quantity = item.Quantity,
                    SalesRate = item.SalesRate,
                    NetAmount = item.NetAmount,
                    DiscountAmount = item.DiscountAmount,
                    Amount = item.Amount,
                    Taxper = item.Taxper,
                    AddonStatus = item.AddonStatus,
                    MainCategory = string.IsNullOrEmpty(item.AV_id) ? $"{item.ItemName}{item.SalesMasterId}{item.Prid}" : item.AV_id,
                    MainCategoryCount = invoiceBillDetails.Count(b => b.MainCategory == item.MainCategory)
                }).ToList();

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
                              processedItem.ItemName,
                              processedItem.Quantity,
                              processedItem.SalesRate,
                              processedItem.NetAmount,
                              processedItem.DiscountAmount,
                              processedItem.Amount,
                              processedItem.Taxper,
                              processedItem.AddonStatus,
                          })
                    .ToList();
                #endregion


                using (var reader = ObjectReader.Create(mergedData,
                    "ItemName", "Quantity", "SalesRate", "NetAmount", "DiscountAmount", "Amount", "Taxper", "MainCategory", "MainCategoryCount", "AddonStatus"))
                {
                    dtSalesDetail.Load(reader);
                }

                #region Tax Calculation
                for (int i = 0; i < invoiceBillDetails.Count; i++)
                {
                    I_TaxAmount = I_TaxAmount + invoiceBillDetails[i].TotalTax;
                    I_CGSTAmount = I_CGSTAmount + invoiceBillDetails[i].TotalCGST;
                    I_SGSTAmount = I_SGSTAmount + invoiceBillDetails[i].TotalSGST;
                    I_VatAmount = I_VatAmount + invoiceBillDetails[i].TotalVAT;
                }
                #endregion

                #region PayType Check
                if (paytypeid == Convert.ToInt32(PaymentType.PART))
                {
                    DmnSearch objPART = new DmnSearch();
                    BllSearch<DmnGridFields> bllpaymentsPART = new BllSearch<DmnGridFields>();
                    objPART.Select = "PayType Field1, Amount Field2";
                    objPART.Table = "tblpayment";
                    objPART.WhereNdGroupBy = " where salemasterid=" + SalesMasterId;
                    var invpaymentPART = bllpaymentsPART.GetSearchData("prdSearch", objPART);
                    if (invpaymentPART.Count > 0)
                    {
                        for (int i = 0; i < invpaymentPART.Count; i++)
                        {
                            switch (Convert.ToInt32(invpaymentPART[i].Field1))
                            {
                                case 1:
                                    cashamount = Convert.ToDecimal(invpaymentPART[i].Field2);
                                    break;
                                case 5:
                                    othetamount = Convert.ToDecimal(invpaymentPART[i].Field2);
                                    payname = "DEBITCARD";
                                    break;
                                case 4:
                                    othetamount = Convert.ToDecimal(invpaymentPART[i].Field2);
                                    payname = "UPI";
                                    break;
                                case 6:
                                    othetamount = Convert.ToDecimal(invpaymentPART[i].Field2);
                                    payname = "BANKINGONLINE";
                                    break;
                                case 8:
                                    othetamount = Convert.ToDecimal(invpaymentPART[i].Field2);
                                    payname = "CREDIT";
                                    break;
                                case 7:
                                    othetamount = Convert.ToDecimal(invpaymentPART[i].Field2);
                                    payname = "OTHERS";
                                    break;
                            }
                        }
                    }
                }
                else if (paytypeid == Convert.ToInt32(PaymentType.SPLIT))
                {
                    if (invpayment.Count > 0)
                    {
                        splitamount = Convert.ToDecimal(invpayment[0].Field2);   

                        if (Convert.ToInt32(invpayment[0].Field5) > 0)
                        {
                            invoiceBillMasters[0].OaAmount =  Convert.ToDecimal(invpayment[0].Field6);
                            invoiceBillMasters[0].OdAmount =  Convert.ToDecimal(invpayment[0].Field7);
                            invoiceBillMasters[0].DiscountAmount =  Convert.ToDecimal(invpayment[0].Field9);

                            invoiceBillMasters[0].TotalTax = I_TaxAmount;
                            invoiceBillMasters[0].TotalCGST = I_CGSTAmount;
                            invoiceBillMasters[0].TotalSGST = I_SGSTAmount;
                            invoiceBillMasters[0].TotalVAT = I_VatAmount;

                            invoiceBillMasters[0].BalAmnt = splitamount;
                            invoiceBillMasters[0].NetAmount = splitamount;

                            invoiceBillMasters[0].BalAmnt = Convert.ToDecimal(invpayment[0].Field11);
                            invoiceBillMasters[0].RcvAmnt = Convert.ToDecimal(invpayment[0].Field12);

                            invoiceBillMasters[0].NetAmount = splitamount;

                        }
                        else
                        {
                            invoiceBillMasters[0].OaAmount = invoiceBillMasters[0].OaAmount / Convert.ToInt32(invpayment[0].Field10);
                            invoiceBillMasters[0].OdAmount = invoiceBillMasters[0].OdAmount / Convert.ToInt32(invpayment[0].Field10);
                            invoiceBillMasters[0].DiscountAmount = invoiceBillMasters[0].DiscountAmount / Convert.ToInt32(invpayment[0].Field10);

                            invoiceBillMasters[0].TotalTax = I_TaxAmount / Convert.ToInt32(invpayment[0].Field10);
                            invoiceBillMasters[0].TotalCGST = I_CGSTAmount / Convert.ToInt32(invpayment[0].Field10);
                            invoiceBillMasters[0].TotalSGST = I_SGSTAmount / Convert.ToInt32(invpayment[0].Field10);
                            invoiceBillMasters[0].TotalVAT = I_VatAmount / Convert.ToInt32(invpayment[0].Field10);

                            invoiceBillMasters[0].BalAmnt = Convert.ToDecimal(invpayment[0].Field11);
                            invoiceBillMasters[0].RcvAmnt = Convert.ToDecimal(invpayment[0].Field12);

                            invoiceBillMasters[0].NetAmount = splitamount;
                        }
                    }
                }
                #endregion

                using (var reader = ObjectReader.Create(invoiceBillMasters, "EntryNo", "EntryDate", "Username", "TotalAmount", "DiscountAmount",
    "TotalTax", "TotalCGST", "TotalSGST", "TotalVAT", "TotalQuantity", "NetAmount", "PaidAmount", "BalAmnt", "RcvAmnt", "CusName", "Email", "Telephone", "SalesAddress", "OaAmount", "OdAmount", "Remarks", "TotalDiscount", "PaymentMode", "PromoDId", "PromoAmount", "AdvanceAmount", "ReservationTotal"))
                {
                    dtSalesMaster.Load(reader);
                }               

                dsReport.Tables.Add(dtSalesDetail);
                dsReport.Tables.Add(dtSalesHeader);
                dsReport.Tables.Add(dtSalesMaster);

                if(BillTypeData == Convert.ToInt32(BillType.NormalBill))
                    reportDocument.Load(RPTPath + @"\CR_Bill.rpt"); //reportDocument.Load(RPTPath + @"\SaleBill_Copy.rpt"); //
                else if (BillTypeData == Convert.ToInt32(BillType.BillCopy))
                    reportDocument.Load(RPTPath + @"\CR_Bill.rpt"); //reportDocument.Load(RPTPath + @"\SaleBill_Copy.rpt");

                reportDocument.SetDataSource(dsReport);

                reportDocument.SetParameterValue("payname", payname);
                reportDocument.SetParameterValue("cashamount",cashamount);
                reportDocument.SetParameterValue("otheramount", othetamount);
                reportDocument.SetParameterValue("SPLIT_TYPE", splittype);

                crvSalesBill.RefreshReport();
                crvSalesBill.ReportSource = reportDocument;

                var pdffolderPath = Application.StartupPath + "\\Bill_pdf";
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
                pdfPath = Path.Combine(pdffolderPath, "Bill.pdf");
                reportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath);
                crvSalesBill.Focus();
                //reportDocument.PrintToPrinter(1, false, 0, 0);
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        private void GenerateBarcode(string barcodeText)
        {
            try
            {
                var writer = new BarcodeWriter
                {
                    Format = BarcodeFormat.CODE_128,
                    Options = new EncodingOptions
                    {
                        Height = 300,
                        Width = 200,
                        Margin = 5
                    }
                };
                var barcodeBitmap = writer.Write(barcodeText);

                var folderPath = Application.StartupPath + "\\Barcode";
                var filePath = folderPath + "\\" + barcodeText + ".png";

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                else
                {
                    string[] files = Directory.GetFiles(folderPath);
                    foreach (string file in files)
                    {
                        File.Delete(file);
                    }
                }
                if (File.Exists(filePath))
                    File.Delete(filePath);

                barcodeBitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception ex)
            {

            }
        }
        private string RPTVWFile(string RepFName)
        {
            string fstr1 = "";

            if (RPTLocalVw == false)
            {
                fstr1 = Application.StartupPath + "\\SaleBill\\" + RepFName;
            }
            return fstr1;
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool bHandled = false;
            switch (keyData)
            {
                case Keys.F4:
                    ReportDocument reportDocument = crvSalesBill.ReportSource as ReportDocument;
                    reportDocument.PrintToPrinter(1, false, 0, 0);
                    bHandled = true;
                    break;
                case Keys.F6:
                    this.Close();
                    bHandled = true;
                    break;
            }
            return bHandled;
        }
        private void btn_print_MouseHover(object sender, EventArgs e)
        {
            lab_prinnter.Visible = true;
        }

        private void btn_print_MouseLeave(object sender, EventArgs e)
        {
            lab_prinnter.Visible = false;
        }

        private void btn_cancel_MouseHover(object sender, EventArgs e)
        {
            lab_repor_cls.Visible = true;
        }

        private void btn_cancel_MouseLeave(object sender, EventArgs e)
        {
            lab_repor_cls.Visible = false;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            ReportDocument reportDocument = crvSalesBill.ReportSource as ReportDocument;
            reportDocument.PrintToPrinter(1, false, 0, 0);
        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txt_ph_report, txt_gmail_report })
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

        private void txt_gmail_report_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }

        private void txt_ph_report_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }
    }
}
