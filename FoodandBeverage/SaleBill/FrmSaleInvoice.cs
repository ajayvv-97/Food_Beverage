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

namespace FoodandBeverage.SaleBill
{
    public partial class FrmSaleInvoice : Form
    {
        public int SalesMasterId = 0;
        public int paytypeid = 0;
        public int paymentid = 0;
        public static int BranchId = 0;
        public static int UserId = 0;
        public string folderPath = Application.StartupPath + "\\Barcode";
        private HttpClient httpClient;
        public static int BType = 0;
        public string RPTPath = @"C:\Users\ajay\Desktop\Food_Beverage\FoodandBeverage\SaleBill\";//Application.StartupPath + @"\SaleBill";//";
        public string locationname_api;
        public decimal netamount_api = 0;
        public string cusname_api;
        public string invoiceno_api;
        public string pdfPath = "";

        public FrmSaleInvoice(int masterId,int paytype,int paytid)
        {
            SalesMasterId = masterId;
            paymentid = paytid;
            paytypeid = paytype;
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
            //if (paytypeid == 12)
            //    PrintBill_Split();
            //else
                PrintBill();
        }
        #region Print Bill for Normal & Part 
        public void PrintBill()
        {
            try
            {
                string reportPath,orderdetailsids="";
                decimal cashamount = 0, othetamount = 0, splitamount=0;
                string payname = "";
                decimal I_TaxAmount = 0, I_CGSTAmount = 0, I_SGSTAmount = 0,
                    I_VatAmount = 0, I_OaddAmt = 0, I_OdedAmt = 0, OaAmount=0,
            OdAmount=0,
            PromoAmt=0,
            DiscountAmount=0, Totaldiscount = 0, totalsaveamt = 0; ;
                int splittype = 0;
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
                dmnSearchInvoiceHeaders.WhereNdGroupBy = " where Id=" + FrmDashboard.BranchId;
                var invoiceBillHeaders = bllSearchHeaders.GetSearchData("prdSearch", dmnSearchInvoiceHeaders);
                //locationname_api = invoiceBillHeaders[0].Name;
                DmnSearch dmnSearchInvoiceBill = new DmnSearch();
                dmnSearchInvoiceBill.Select = " S.EntryNo,S.CreatedDateTime as EntryDate, " +
                                              " (Select Username from tblUserAccount where UserAccountId = '" + UserId + "') as Username, " +
                                              " S.TotalAmount,S.DiscountAmount,S.TotalDiscount,S.TotalTax,S.TotalCGST,S.TotalSGST,S.TotalVAT,S.TotalQuantity," +
                                              " S.NetAmount,S.BalAmnt,s.RcvAmnt,s.CusName,s.Email,s.Telephone,s.SalesAddress,s.OaAmount,s.OdAmount,Remarks,PaymentMode,s.PromoDId,s.PromoAmount";
                dmnSearchInvoiceBill.Table = " tblSalesMaster as S";
                //if (splittype == 2)
                //    dmnSearchInvoiceBill.WhereNdGroupBy = " where S.SalesDetailsId IN (" + orderdetailsids.TrimEnd(',') + ")";
                //else
                    dmnSearchInvoiceBill.WhereNdGroupBy = " where S.SalesMasterId =" + SalesMasterId;
                var invoiceBillMasters = bllSearchInvoiceBill.GetSearchData("prdSearch", dmnSearchInvoiceBill);
                netamount_api = invoiceBillMasters[0].NetAmount;
                invoiceno_api = invoiceBillMasters[0].EntryNo;
                cusname_api = invoiceBillMasters[0].CusName;
                if (cusname_api == "" || cusname_api == null)
                    cusname_api = "Sir / Mam";
                //stroe barcode_img path header
                var imagePath = folderPath + "\\" + invoiceBillMasters[0].EntryNo + ".png";
                //invoiceBillHeaders[0].barcode_img_path = imagePath;
                GenerateBarcode(invoiceBillMasters[0].EntryNo);               

                using (var reader = ObjectReader.Create(invoiceBillMasters, "EntryNo", "EntryDate", "Username", "TotalAmount", "DiscountAmount",
                    "TotalTax", "TotalCGST", "TotalSGST", "TotalVAT", "TotalQuantity", "NetAmount", "PaidAmount", "BalAmnt", "RcvAmnt", "CusName", "Email", "Telephone", "SalesAddress", "OaAmount", "OdAmount", "Remarks", "TotalDiscount", "PaymentMode", "PromoDId","PromoAmount"))
                {
                    dtSalesMaster.Load(reader);
                }

                //header part set data to dataset
                using (var reader = ObjectReader.Create(invoiceBillHeaders, "Name", "Address", "Email", "Phone", "LocId", "barcode_img_path", "GSTNo"))
                {
                    dtSalesHeader.Load(reader);
                }

                #region PayType Check
                if (paytypeid == 11)
                {
                    DmnSearch obj = new DmnSearch();
                    BllSearch<DmnGridFields> bllpayments = new BllSearch<DmnGridFields>();
                    obj.Select = "PayType Field1, Amount Field2";
                    obj.Table = "tblpayment";
                    obj.WhereNdGroupBy = " where salemasterid=" + SalesMasterId;
                    var invpayment = bllpayments.GetSearchData("prdSearch", obj);
                    if (invpayment.Count > 0)
                    {

                        for (int i = 0; i < invpayment.Count; i++)
                        {
                            switch (Convert.ToInt32(invpayment[i].Field1))
                            {
                                case 1:
                                    cashamount = Convert.ToDecimal(invpayment[i].Field2);
                                    break;
                                case 5:
                                    othetamount = Convert.ToDecimal(invpayment[i].Field2);
                                    payname = "DEBITCARD";
                                    break;
                                case 4:
                                    othetamount = Convert.ToDecimal(invpayment[i].Field2);
                                    payname = "UPI";
                                    break;
                                case 6:
                                    othetamount = Convert.ToDecimal(invpayment[i].Field2);
                                    payname = "BANKINGONLINE";
                                    break;
                                case 8:
                                    othetamount = Convert.ToDecimal(invpayment[i].Field2);
                                    payname = "CREDIT";
                                    break;
                                case 7:
                                    othetamount = Convert.ToDecimal(invpayment[i].Field2);
                                    payname = "OTHERS";
                                    break;
                            }
                        }
                    }

                }
                else if (paytypeid == 12)
                {
                    DmnSearch obj = new DmnSearch();
                    BllSearch<DmnGridFields> bllpayments = new BllSearch<DmnGridFields>();
                    obj.Select = "PayType Field1, Amount Field2,splittype Field3,OrderDetailsId Field4,BillCount Field5,OaAmount Field6,OdAmount Field7,PromoAmt Field8,DiscountAmount Field9 ";
                    obj.Table = "tblpayment";
                    obj.WhereNdGroupBy = " where payid=" + paymentid;
                    var invpayment = bllpayments.GetSearchData("prdSearch", obj);
                    if (invpayment.Count > 0)
                    {
                        splitamount = Convert.ToDecimal(invpayment[0].Field2);
                        splittype = Convert.ToInt32(invpayment[0].Field3);
                        orderdetailsids = Convert.ToString(invpayment[0].Field4);
                        OaAmount = Convert.ToDecimal(invpayment[0].Field6);
                        OdAmount = Convert.ToDecimal(invpayment[0].Field7);
                        PromoAmt = Convert.ToDecimal(invpayment[0].Field8);
                        DiscountAmount= Convert.ToDecimal(invpayment[0].Field9);
                        totalsaveamt = totalsaveamt + OdAmount + DiscountAmount;

                        if (Convert.ToInt32(invpayment[0].Field5) > 0)
                        {
                            I_OaddAmt = invoiceBillMasters[0].OaAmount / Convert.ToInt32(invpayment[0].Field5);
                            I_OdedAmt = invoiceBillMasters[0].OdAmount / Convert.ToInt32(invpayment[0].Field5);
                        }                       

                    }

                }
                #endregion
                DmnSearch dmnSearchInvoiceDetails = new DmnSearch();
                dmnSearchInvoiceDetails.Select = "S.ItemName ,S.Quantity,S.MRP,S.MarketPrice as SalesRate,S.NetAmount,S.Amount,S.TaxPer,s.DiscountAmount,s.TaxAmount as TotalTax,s.CGSTAmount TotalCGST,s.SGSTAmount TotalSGST,s.VatAmount as TotalVAT"; 
                dmnSearchInvoiceDetails.Table = "tblSalesDetails as S";
                if (splittype==2)                
                    dmnSearchInvoiceDetails.WhereNdGroupBy = " where S.SalesDetailsId IN (" + orderdetailsids.TrimEnd(',') + ")";        
                else
                    dmnSearchInvoiceDetails.WhereNdGroupBy = " where S.SalesMasterId=" + SalesMasterId;

                var invoiceBillDetails = bllSearchDetails.GetSearchData("prdSearch", dmnSearchInvoiceDetails);
                using (var reader = ObjectReader.Create(invoiceBillDetails,
                    "ItemName", "Quantity", "SalesRate", "NetAmount", "DiscountAmount", "Amount", "Taxper"))
                {
                    dtSalesDetail.Load(reader);
                }
                dsReport.Tables.Add(dtSalesDetail);
                dsReport.Tables.Add(dtSalesHeader);
                dsReport.Tables.Add(dtSalesMaster);

                reportDocument.Load(RPTPath + @"\CR_Bill.rpt");

                reportDocument.SetDataSource(dsReport);

                reportDocument.SetParameterValue("payname", payname);
                reportDocument.SetParameterValue("cashamount",cashamount);
                reportDocument.SetParameterValue("otheramount", othetamount);
                reportDocument.SetParameterValue("splitamount", splitamount);
                if (splittype == 2 && invoiceBillDetails.Count>0)
                {
                    for (int i = 0; i < invoiceBillDetails.Count; i++)
                    {
                        I_TaxAmount = I_TaxAmount + invoiceBillDetails[i].TotalTax;
                        I_CGSTAmount = I_CGSTAmount + invoiceBillDetails[i].TotalCGST;
                        I_SGSTAmount = I_SGSTAmount + invoiceBillDetails[i].TotalSGST;
                        I_VatAmount = I_VatAmount + invoiceBillDetails[i].TotalVAT;
                        Totaldiscount = Totaldiscount + invoiceBillDetails[i].DiscountAmount;
                    }
                    totalsaveamt = totalsaveamt + Totaldiscount;
                    Totaldiscount = Totaldiscount + DiscountAmount;
                }
                reportDocument.SetParameterValue("I_totalsaveamt", totalsaveamt);
                reportDocument.SetParameterValue("I_totaldiscoutamt", Totaldiscount);
                reportDocument.SetParameterValue("I_OaddAmt", I_OaddAmt);
                reportDocument.SetParameterValue("I_OdedAmt", I_OdedAmt);

                reportDocument.SetParameterValue("I_TaxAmount", I_TaxAmount);
                reportDocument.SetParameterValue("I_CGSTAmount", I_CGSTAmount);
                reportDocument.SetParameterValue("I_SGSTAmount", I_SGSTAmount);
                reportDocument.SetParameterValue("I_VatAmount", I_VatAmount);
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
                reportDocument.PrintToPrinter(1, false, 0, 0);
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
    }
}
