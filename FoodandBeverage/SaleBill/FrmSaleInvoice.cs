using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CustomMessageBox;
using Domain;
using FastMember;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms;
using FoodandBeverage.Forms.UserControl;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;

namespace FoodandBeverage.SaleBill
{
    public partial class FrmSaleInvoice : Form
    {
        public int SalesMasterId = 0;
        public int paytypeid = 0;
        public int paymentid = 0;
        public int BillTypeData = 0;
        public  int BranchId = 0;
        public  int UserId = 0;
        public string folderPath = Application.StartupPath + "\\Barcode";
        private HttpClient httpClient;
        public  int BType = 0;
        public string RPTPath = Convert.ToString(FrmLogin.SaleBillPath);
        public string locationname_api;
        public decimal netamount_api = 0;
        public string cusname_api;
        public string invoiceno_api;
        public string pdfPath = "";
        bool GstBillflag = false;
        ReportDocument reportDocument = new ReportDocument();
        ClsGeneral clsgen = new ClsGeneral();
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

        #region Declaration
        public bool Logo_Status = false;
        public bool QR_Status = false;
        public bool Customer_Status = false;
        public string FooterMsg = "";
        int Fdefaultwidth = 0;
        int Fdefaultheight = 0;
        #endregion
        private void FrmSaleInvoice_Load(object sender, EventArgs e)
        {
            Fdefaultwidth = this.Width;
            Fdefaultheight = this.Height;

            textb_shape();
            MessageCheck();
            if (BillTypeData == Convert.ToInt32(BillType.NormalBill) || BillTypeData == Convert.ToInt32(BillType.PickupBill) || BillTypeData == Convert.ToInt32(BillType.BillCopy))//
            {
                PrintBill();
            }
            else if (BillTypeData == Convert.ToInt32(BillType.Partybill))
            {
                rb_a4.Visible = true;
                rb_th.Visible = true;

                rb_th.Checked = true;
                PrintPartyBill();
            }
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
                    if (gridesource[0].Logostatus)
                    {
                        Logo_Status = true;
                    }
                    if (gridesource[0].QRcode)
                    {
                        QR_Status = true;
                    }
                    if (gridesource[0].Footermsg != "")
                    {
                        FooterMsg = gridesource[0].Footermsg;
                    }
                    if (gridesource[0].Customer_Status)
                    {
                        Customer_Status = true;
                    }

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


                BllSearch<DmnGeneralSetting> bllSearchgen = new BllSearch<DmnGeneralSetting>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblGeneralSetting";
                objSearch.WhereNdGroupBy = "WHERE BranchId = " + FrmDashboard.BranchId + " AND Status = 1";
                objSearch.OrderBy = "";
                var itemSource = bllSearchgen.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    FooterMsg = Convert.ToString(itemSource[0].FooterContent);
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Addon page MessageCheck()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.SalesBill));
            }
        }
        #endregion

        #region Print Bill for Normal & Part & Party Booking  Bill
        public void PrintBill()
        {
            try
            {
                string reportPath, orderdetailsids = "", payname = "";
                decimal cashamount = 0, othetamount = 0, splitamount = 0, I_TaxAmount = 0, I_CGSTAmount = 0, I_SGSTAmount = 0, I_VatAmount = 0;
                int splittype = 0;
                var invpayment = new List<DmnGridFields>();

                #region Crystal Report Dclaration


                if (crvSalesBill.ReportSource != null)
                    crvSalesBill.ReportSource = null;

                if (reportDocument != null)
                {
                    reportDocument.Close();
                    reportDocument.Dispose();
                    reportDocument = null;
                }

                reportDocument = new ReportDocument();

                DataSet dsReport = new DataSet();
                DataTable dtSalesHeader = new DataTable("DT_SaleHeader");
                DataTable dtSalesMaster = new DataTable("DT_salemaster");
                DataTable dtSalesDetail = new DataTable("DT_saledetails");

                BllSearch<DmnSaleBillHeaders> bllSearchHeaders = new BllSearch<DmnSaleBillHeaders>();
                BllSearch<DmnSalesInvoiceBillDetails> bllSearchDetails = new BllSearch<DmnSalesInvoiceBillDetails>();
                BllSearch<DmnSalesInvoiceBill> bllSearchInvoiceBill = new BllSearch<DmnSalesInvoiceBill>();

                DmnSearch dmnSearchInvoiceHeaders = new DmnSearch();
                #endregion

                dmnSearchInvoiceHeaders.Select = "Name,Address,Email,Phone,Id,GSTNO,RegAddress";
                dmnSearchInvoiceHeaders.Table = "tblHotelRegistration";
                dmnSearchInvoiceHeaders.WhereNdGroupBy = " where Branchid=" + FrmDashboard.BranchId;
                var invoiceBillHeaders = bllSearchHeaders.GetSearchData("prdSearch", dmnSearchInvoiceHeaders);
                //locationname_api = invoiceBillHeaders[0].Name;
                DmnSearch dmnSearchInvoiceBill = new DmnSearch();
                dmnSearchInvoiceBill.Select =
                   " S.EntryNo, S.CreatedDateTime AS EntryDate, " +
                   " (SELECT Username FROM tblUserAccount WHERE UserAccountId = '" + UserId + "') AS Username, " +
                   " S.TotalAmount, S.DiscountAmount, S.TotalDiscount, S.TotalTax, " +
                   " S.TotalCGST, S.TotalSGST, S.TotalVAT, S.TotalQuantity, " +
                   " S.NetAmount, S.BalAmnt, S.RcvAmnt, " +

                  " (LTRIM(RTRIM(ISNULL(S.FirstName,''))) + ' ' + LTRIM(RTRIM(ISNULL(S.SecondName,'')))) AS CusName, " +
                   " S.EmailAddress as Email, S.PhoneNumber as Telephone, S.FullAddress as SalesAddress,s.CompanyName,s.GSTNo, " +

                   " S.OaAmount, S.OdAmount, S.Remarks, S.PaymentMode, " +
                   " S.PromoDId, S.PromoAmount, S.AdvanceAmount, " +
                   " S.ReservationTotal, S.PkChargeAmount, " +
                   " S.Rev_Status, S.OrderType, S.PkId ,s.Cusid,s.Gender,s.FirstName,s.SecondName";

                dmnSearchInvoiceBill.Table = " tblSalesMaster AS S";
                dmnSearchInvoiceBill.WhereNdGroupBy = " WHERE S.SalesMasterId = " + SalesMasterId;
                var invoiceBillMasters = bllSearchInvoiceBill.GetSearchData("prdSearch", dmnSearchInvoiceBill);
                netamount_api = invoiceBillMasters[0].NetAmount;
                invoiceno_api = invoiceBillMasters[0].EntryNo;
                cusname_api = invoiceBillMasters[0].CusName;
                if (cusname_api == "" || cusname_api == null)
                    cusname_api = "Sir / Mam";
                //stroe barcode_img path header
                var imagePath = folderPath + "\\" + invoiceBillMasters[0].EntryNo + ".png";
                invoiceBillHeaders[0].barcode_img_path = imagePath;
                if ((invoiceBillHeaders[0].Email == "" || invoiceBillHeaders[0].Email == null) && (invoiceBillHeaders[0].Phone == "" || invoiceBillHeaders[0].Phone == null))
                {
                    invoiceBillHeaders[0].Full_Address = invoiceBillHeaders[0].Address;
                }
                else if (invoiceBillMasters[0].Email == "" || invoiceBillMasters[0].Email == null)
                {
                    invoiceBillHeaders[0].Full_Address = invoiceBillHeaders[0].Address + invoiceBillHeaders[0].Phone;
                }

                if (invoiceBillMasters[0].Cusid > 0)
                {
                    txtcusid.Text = Convert.ToString(invoiceBillMasters[0].Cusid);
                    LoadCustomer(Convert.ToInt32(txtcusid.Text));
                }

                //txtfristname.Text = invoiceBillMasters[0].FirstName ?? "";
                //txtsecondname.Text = invoiceBillMasters[0].SecondName ?? "";
                //txtcompanyname.Text = invoiceBillMasters[0].CompanyName ?? "";
                //txtgstno.Text = invoiceBillMasters[0].GSTNo ?? "";
                //txtph.Text = invoiceBillMasters[0].Telephone ?? "";
                //txtemailid.Text = invoiceBillMasters[0].Email ?? "";
                //txtaddress.Text = invoiceBillMasters[0].SalesAddress ?? "";

                //// Set gender radio buttons
                //if (invoiceBillMasters[0].Gender == 1) // Male
                //{
                //    rb_male.Checked = true;
                //    rb_female.Checked = false;
                //}
                //else if (invoiceBillMasters[0].Gender == 2) // Female
                //{
                //    rb_male.Checked = false;
                //    rb_female.Checked = true;
                //}

                GenerateBarcode(invoiceBillMasters[0].EntryNo);
                //header part set data to dataset
                using (var reader = ObjectReader.Create(invoiceBillHeaders, "Name", "Address", "Email", "Phone", "LocId", "barcode_img_path", "GSTNo", "Full_Address", "RegAddress"))
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
                            invoiceBillMasters[0].OaAmount = Convert.ToDecimal(invpayment[0].Field6);
                            invoiceBillMasters[0].OdAmount = Convert.ToDecimal(invpayment[0].Field7);
                            invoiceBillMasters[0].DiscountAmount = Convert.ToDecimal(invpayment[0].Field9);

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
    "TotalTax", "TotalCGST", "TotalSGST", "TotalVAT", "TotalQuantity", "NetAmount", "PaidAmount", "BalAmnt", "RcvAmnt", "CusName", "Email", "Telephone", "SalesAddress", "OaAmount", "OdAmount", "Remarks", "TotalDiscount", "PaymentMode", "PromoDId", "PromoAmount", "AdvanceAmount", "ReservationTotal", "PkChargeAmount", "Rev_Status", "OrderType", "PkId", "CompanyName", "GSTNo"))
                {
                    dtSalesMaster.Load(reader);
                }

                dsReport.Tables.Add(dtSalesDetail);
                dsReport.Tables.Add(dtSalesHeader);
                dsReport.Tables.Add(dtSalesMaster);

                if (BillTypeData == Convert.ToInt32(BillType.NormalBill))
                {
                    reportDocument.Load(RPTPath + @"\CR_Bill.rpt");
                    string reportFileName = "";
                    reportFileName = "CR_Bill.rpt";
                    string xy = RPTPath + @"\CR_Bill.rpt";
                }
                else if (BillTypeData == Convert.ToInt32(BillType.BillCopy))
                {
                    reportDocument.Load(RPTPath + @"\CR_Bill.rpt");
                }
                else if (BillTypeData == Convert.ToInt32(BillType.PickupBill))
                    reportDocument.Load(RPTPath + @"\CR_PickupBill.rpt");

                reportDocument.SetDataSource(dsReport);

                if (GstBillflag)
                    reportDocument.SetParameterValue("Gstbill", 1);
                else
                    reportDocument.SetParameterValue("Gstbill", 0);

                reportDocument.SetParameterValue("payname", payname);
                reportDocument.SetParameterValue("cashamount", cashamount);
                reportDocument.SetParameterValue("otheramount", othetamount);
                reportDocument.SetParameterValue("SPLIT_TYPE", splittype);
                reportDocument.SetParameterValue("footermsg", FooterMsg);

                reportDocument.SetParameterValue("smid", SalesMasterId);

                if (invoiceBillMasters[0].Rev_Status == 1)
                {
                    reportDocument.SetParameterValue("Adv_pkc_label", "Advance Amount");
                    reportDocument.SetParameterValue("Adv_pkc_amt", invoiceBillMasters[0].AdvanceAmount);
                }
                else
                {
                    reportDocument.SetParameterValue("Adv_pkc_label", "Packing Charge");
                    reportDocument.SetParameterValue("Adv_pkc_amt", invoiceBillMasters[0].PkChargeAmount);
                }

                string imgpath = FrmLogin.logoPath + @"sblogo.png";
                if (File.Exists(imgpath))
                {
                    reportDocument.SetParameterValue("Logopath", imgpath);
                }
                else
                {
                    reportDocument.SetParameterValue("Logopath", string.Empty);
                }


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
                clsgen.LogException("Addon page PrintBill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.SalesBill));

                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PrintPartyBill()
        {
            try
            {
                #region INITIAL SETUP

                string payname = "";
                decimal cashamount = 0, otheramount = 0, splitamount = 0;
                decimal I_TaxAmount = 0, I_CGSTAmount = 0, I_SGSTAmount = 0, I_VatAmount = 0;
                int splittype = 0, PartybookingId=0;

                var invpayment = new List<DmnGridFields>();

                if (crvSalesBill.ReportSource != null)
                    crvSalesBill.ReportSource = null;

                if (reportDocument != null)
                {
                    reportDocument.Close();
                    reportDocument.Dispose();
                    reportDocument = null;
                }

                reportDocument = new ReportDocument();

                DataSet dsReport = new DataSet();

                DataTable dtSalesHeader = new DataTable("DT_SaleHeader");
                DataTable dtSalesMaster = new DataTable("DT_salemaster");
                DataTable dtSalesDetail = new DataTable("DT_saledetails");
                DataTable dtPartybooking = new DataTable("DT_Partybooking");
                DataTable dtPartybookingpayment = new DataTable("DT_PartybookingPayment");

                #endregion

                #region HEADER

                var bllHeader = new BllSearch<DmnSaleBillHeaders>();
                var headerSearch = new DmnSearch
                {
                    Select = "Name,Address,Email,Phone,id as LocId,GSTNO,RegAddress",
                    Table = "tblHotelRegistration",
                    WhereNdGroupBy = "WHERE BranchId=" + FrmDashboard.BranchId
                };

                var headerData = bllHeader.GetSearchData("prdSearch", headerSearch);

                using (var reader = ObjectReader.Create(headerData,
                    "Name", "Address", "Email", "Phone", "LocId", "GSTNo", "RegAddress"))
                {
                    dtSalesHeader.Load(reader);
                }

                #endregion

                #region SALES MASTER

                var bllMaster = new BllSearch<DmnSalesInvoiceBill>();
                var masterSearch = new DmnSearch
                {
                    Select = @"
                S.EntryNo,
                S.CreatedDateTime AS EntryDate,
                S.TotalAmount,
                S.DiscountAmount,
                S.TotalTax,
                S.TotalCGST,
                S.TotalSGST,
                S.TotalVAT,
                S.TotalQuantity,
                S.NetAmount,
                S.BalAmnt,
                S.RcvAmnt,
                (ISNULL(S.FirstName,'') + ' ' + ISNULL(S.SecondName,'')) AS CusName,
                S.EmailAddress AS Email,
                S.PhoneNumber AS Telephone,
                S.FullAddress AS SalesAddress,
                S.PaymentMode,
                S.AdvanceAmount,
                S.CompanyName,
                S.GSTNo",
                    Table = "tblSalesMaster S",
                    WhereNdGroupBy = "WHERE S.SalesMasterId = " + SalesMasterId
                };

                var masterData = bllMaster.GetSearchData("prdSearch", masterSearch);

                if (masterData == null || masterData.Count == 0)
                {
                    CMessageBox.Show("No invoice data found");
                    return;
                }

                using (var reader = ObjectReader.Create(masterData,
                    "EntryNo", "EntryDate", "TotalAmount", "DiscountAmount",
                    "TotalTax", "TotalCGST", "TotalSGST", "TotalVAT",
                    "TotalQuantity", "NetAmount", "BalAmnt", "RcvAmnt",
                    "CusName", "Email", "Telephone", "SalesAddress",
                    "PaymentMode", "AdvanceAmount", "CompanyName", "GSTNo"))
                {
                    dtSalesMaster.Load(reader);
                }

                #endregion

                #region SALES DETAILS

                var bllDetails = new BllSearch<DmnSalesInvoiceBillDetails>();
                var detailSearch = new DmnSearch
                {
                    Select = @"ItemName, Quantity, SalesRate AS SalesRate,
                       NetAmount, DiscountAmount, Amount, Taxper,PartyBookingId,PackageType,TaxAmount",
                    Table = "tblSalesDetails",
                    WhereNdGroupBy = "WHERE SalesMasterId=" + SalesMasterId
                };

                var detailData = bllDetails.GetSearchData("prdSearch", detailSearch);
                if(detailData.Count>0)
                {
                    PartybookingId = detailData[0].PartyBookingId;
                }

                using (var reader = ObjectReader.Create(detailData,"ItemName", "Quantity", "SalesRate","NetAmount", "DiscountAmount", "Amount", "Taxper", "PackageType", "TaxAmount"))
                {
                    dtSalesDetail.Load(reader);
                }

                #endregion

                #region PARTY BOOKING

                var bllParty = new BllSearch<DmnSalespartyBill>();
                var partySearch = new DmnSearch
                {
                    Select = @"
                            pb.CustomerName,
                            pb.Phone,
                            pb.Email,
                            pb.Address,
                            pb.BookingDateTime,     
                            e.ItemName AS EventType,
                            pb.NoOfAdults,
                            pb.NoOfChildren,
                            pb.DiscountReason,
                            pb.EventDescription,
                            pb.NoOfAdults,
                            pb.NoOfChildren,
                            pb.PartyPackId,
                            pm.AdultPrice, 
                            pm.ChildPrice",

                    Table = "tblPartyBooking pb " +
                            "LEFT JOIN tblEventCommonItems e ON e.EventItemId = pb.EventType AND e.Status = 1 " +
                            "LEFT JOIN tblPartyPackMaster pm ON pb.PartyPackId=pm.PartyPackId",
                    WhereNdGroupBy = "WHERE pb.PartyBookingId = " + PartybookingId
                };

                var partyData = bllParty.GetSearchData("prdSearch", partySearch);
                if (partyData != null && partyData.Count > 0)
                {
                    using (var reader = ObjectReader.Create(partyData,
                        "CustomerName",
                        "Phone",
                        "Email",
                        "Address",
                        "BookingDateTime",
                        "EventType",        
                        "NoOfAdults",
                        "NoOfChildren",
                        "DiscountReason",   
                        "EventDescription",
                        "PartyPackId",
                        "AdultPrice",
                        "ChildPrice"))
                    {
                        dtPartybooking.Load(reader);
                    }
                }
                #endregion

                #region PARTY BOOKING PAYMENT

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

                    WhereNdGroupBy = "WHERE PartyBookingId = " + PartybookingId
                };

                var partyPaymentData = bllPartyPayment.GetSearchData("prdSearch", partyPaymentSearch);

                decimal lastReceivedAmount = 0;
                decimal totalAdvanceAmount = 0;
                decimal netAmount = 0;

                if (partyPaymentData != null && partyPaymentData.Count > 0)
                {
                    var sortedList = partyPaymentData
                                        .OrderBy(x => Convert.ToInt32(x.PaymentId))
                                        .ToList();

                    int count = sortedList.Count;

                    // Get NetAmount (same for all rows, take from first)
                    netAmount = Convert.ToDecimal(sortedList[0].NetAmount);

                    decimal totalPaid = sortedList.Sum(x => Convert.ToDecimal(x.PaidAmount));

                    var lastRow = sortedList.Last();
                    lastReceivedAmount = Convert.ToDecimal(lastRow.PaidAmount);

                    if (count == 1)
                    {
                        if (lastReceivedAmount == netAmount)
                        {
                            // Full payment in single entry
                            totalAdvanceAmount = 0;
                        }
                        else
                        {
                            // Partial → advance
                            totalAdvanceAmount = lastReceivedAmount;
                        }
                    }
                    else
                    {
                        // Multiple payments
                        totalAdvanceAmount = totalPaid - lastReceivedAmount;
                    }
                

                using (var reader = ObjectReader.Create(partyPaymentData,
                      "PaymentId",
                      "PaymentMode",
                      "NetAmount",
                      "RcvAmnt",
                      "BalAmnt",
                      "IsSplitPayment",
                      "PaidAmount",
                      "RefundStatus"))
                    {
                        dtPartybookingpayment.Load(reader);
                    }
                }


                #endregion

                #region DATASET

                dsReport.Tables.Add(dtSalesHeader);
                dsReport.Tables.Add(dtSalesMaster);
                dsReport.Tables.Add(dtSalesDetail);
                dsReport.Tables.Add(dtPartybooking);

                #endregion

                #region REPORT LOAD

                if (BillTypeData == Convert.ToInt32(BillType.Partybill))
                {
                    if(rb_th.Checked)
                        reportDocument.Load(RPTPath + @"\CR_PartyBill_TH.rpt");
                    else if(rb_a4.Checked)
                        reportDocument.Load(RPTPath + @"\CR_PartyBill_A4.rpt");
                }

                reportDocument.SetDataSource(dsReport);

                if (GstBillflag)
                    reportDocument.SetParameterValue("Gstbill", 1);
                else
                    reportDocument.SetParameterValue("Gstbill", 0);

                if (rb_th.Checked)
                {
                    reportDocument.SetParameterValue("payname", payname);
                    reportDocument.SetParameterValue("cashamount", cashamount);
                    reportDocument.SetParameterValue("otheramount", 0);
                    reportDocument.SetParameterValue("SPLIT_TYPE", splittype);
                    reportDocument.SetParameterValue("footermsg", FooterMsg);

                    reportDocument.SetParameterValue("smid", SalesMasterId);

                    reportDocument.SetParameterValue("RecvAmt", lastReceivedAmount);
                    reportDocument.SetParameterValue("BalAmt", totalAdvanceAmount);

                    string imgpath = FrmLogin.logoPath + @"sblogo.png";
                    if (File.Exists(imgpath))
                    {
                        reportDocument.SetParameterValue("Logopath", imgpath);
                    }
                    else
                    {
                        reportDocument.SetParameterValue("Logopath", string.Empty);
                    }

                }
                #endregion

                #region SHOW + EXPORT

                crvSalesBill.ReportSource = reportDocument;
                crvSalesBill.Refresh();

                string pdfFolder = Application.StartupPath + "\\Bill_pdf";
                if (!Directory.Exists(pdfFolder))
                    Directory.CreateDirectory(pdfFolder);

                pdfPath = Path.Combine(pdfFolder, "Bill.pdf");
                reportDocument.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath);

                #endregion
            }
            catch (Exception ex)
            {
                clsgen.LogException("Addon page PrintPartyBill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.SalesBill));
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                clsgen.LogException("Addon page GenerateBarcode()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.SalesBill));

            }
        }
        private string RPTVWFile(string RepFName)
        {
            string fstr1 = "";

            if (RPTLocalVw == false)
            {
                fstr1 = "C:\\Users\\silpa\\Desktop\\F_B\\Food_Beverage\\FoodandBeverage\\SaleBill\\" + RepFName;
                //fstr1 = Application.StartupPath + "\\SaleBill\\" + RepFName;
            }
            return fstr1;
        }

        static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            try
            {
                string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                return Regex.IsMatch(email, pattern);
            }
            catch (Exception ex)
            {
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
            try
            {
                if (BillTypeData == Convert.ToInt32(BillType.NormalBill) || BillTypeData == Convert.ToInt32(BillType.PickupBill))
                {
                    ReportDocument reportDocument = crvSalesBill.ReportSource as ReportDocument;
                    reportDocument.PrintToPrinter(1, false, 0, 0);
                }
                else if (BillTypeData == Convert.ToInt32(BillType.Partybill))
                {
                    ApplyFormSize();
                    ReportDocument reportDocument = crvSalesBill.ReportSource as ReportDocument;
                    reportDocument.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch(Exception ex)
            {
                clsgen.LogException("Addon page btn_print_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.SalesBill));

            }
        }
        private void ApplyFormSize()
        {
            if (rb_a4.Checked)
            {
                int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
                int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

                this.Width = (int)(screenWidth * 0.8);
                this.Height = (int)(screenHeight * 0.8);
            }
            else if (rb_th.Checked)
            {
                this.Width = Fdefaultwidth;
                this.Height = Fdefaultheight;
            }
            else
            {
                this.Width = Fdefaultwidth;
                this.Height = Fdefaultheight;
            }

            crvSalesBill.Dock = DockStyle.Fill;

            CenterForm(); 
        }
        private void CenterForm()
        {
            this.StartPosition = FormStartPosition.Manual;

            int x = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2;

            this.Location = new Point(x, y);
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

        private void btn_userreg_cls_Click(object sender, EventArgs e)
        {
            pnl_userreg.Visible = false;
        }

        private void btn_userreg_submit_Click(object sender, EventArgs e)
        {
            try
            {
                DmnAccountLedger customer = new DmnAccountLedger();

                customer.CusId = string.IsNullOrEmpty(txtcusid.Text)
                                    ? 0
                                    : Convert.ToInt32(txtcusid.Text);

                customer.FirstName = txtfristname.Text.Trim();
                customer.SecondName = txtsecondname.Text.Trim();
                customer.CompanyName = txtcompanyname.Text.Trim();
                customer.GSTNo = txtgstno.Text.Trim();
                customer.PhoneNumber = txtph.Text.Trim();
                customer.EmailAddress = txtemailid.Text.Trim();
                customer.FullAddress = txtaddress.Text.Trim();

                customer.Gender = rb_male.Checked
                                    ? 1
                                    : (rb_female.Checked ? 2 : 0);

                customer.Status = 1;
                customer.BranchId = FrmDashboard.BranchId;
                customer.CreatedBy = FrmDashboard.UserId;
                customer.SalesMasterId = SalesMasterId;


                BllCutomerAccount bllCustomer = new BllCutomerAccount();
                var InsertStatus = bllCustomer.AddEditCustomer("prdInsert_Upd_Customer", customer); 
                pnl_userreg.Visible = false;
                GstBillflag = true;
                if(InsertStatus.CusId>0)
                {
                    if (BillTypeData == Convert.ToInt32(BillType.NormalBill) || BillTypeData == Convert.ToInt32(BillType.PickupBill))
                    {
                        rb_a4.Visible = false;
                        rb_th.Visible = false;

                        PrintBill();
                    }
                    else if (BillTypeData == Convert.ToInt32(BillType.Partybill))
                    {
                        rb_a4.Visible = true;
                        rb_th.Visible = true;

                        PrintPartyBill();
                    }
                }
                   
            }
            catch (Exception ex)
            {
                clsgen.LogException("Addon page btn_userreg_submit_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.SalesBill));

                //MessageBox.Show("Error saving customer details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btngst_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtcusid.Text, out int customerId) && customerId > 0)
            {
                // Check if the important fields are empty
                bool isPhoneEmpty = string.IsNullOrWhiteSpace(txtph.Text);
                bool isCompanyEmpty = string.IsNullOrWhiteSpace(txtcompanyname.Text);
                bool isGSTEmpty = string.IsNullOrWhiteSpace(txtgstno.Text);

                if (isPhoneEmpty || isCompanyEmpty || isGSTEmpty)
                {
                    pnl_userreg.Visible = true;
                    pnl_userreg.BringToFront();
                    pnl_userreg.Left = (this.ClientSize.Width - pnl_userreg.Width) / 2;
                    pnl_userreg.Top = (this.ClientSize.Height - pnl_userreg.Height) / 2;
                }
                else
                {
                    GstBillflag = true;
                    PrintBill();
                }
            }
            else
            {
                pnl_userreg.Visible = true;
                pnl_userreg.BringToFront();
                pnl_userreg.Left = (this.ClientSize.Width - pnl_userreg.Width) / 2;
                pnl_userreg.Top = (this.ClientSize.Height - pnl_userreg.Height) / 2;
            }
        }


        private void btncuslist_Click(object sender, EventArgs e)
        {
            LedgerPopup();
        }
        public void LoadCustomer(int id)
        {
            try
            {
                BllSearch<DmnAccountLedger> bllCustomer = new BllSearch<DmnAccountLedger>();
                DmnSearch custSearch = new DmnSearch();

                custSearch.Select = "*";  // Select all columns from tblCustomer
                custSearch.Table = " tblCustomer ";
                custSearch.WhereNdGroupBy = " WHERE CusId = " + id;

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
            catch (Exception ex)
            {
                clsgen.LogException("Addon page LoadCustomer()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.SalesBill));

            }
        }
        public void LedgerPopup()
        {
            try
            {
                string returnValue = string.Empty;
                DataTable dtColumnAlign = new DataTable();
                DmnSearch _objSearch = new DmnSearch();

                _objSearch.ColumnHeaders = "CusId,FirstName,SecondName,CompanyName,GSTNo,PhoneNumber,EmailAddress,FullAddress";
                _objSearch.Select = "CusId id,FirstName Field1,SecondName Field2,CompanyName Field3,GSTNo Field4,PhoneNumber Field5,EmailAddress Field6,FullAddress Field7";
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

                    custSearch.Select = "*";  
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
                clsgen.LogException("Addon page LedgerPopup()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.SalesBill));
            }
        }

        private void FrmSaleInvoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            reportDocument.Close();
            reportDocument.Dispose();

            if (crvSalesBill.ReportSource != null)
            {
                crvSalesBill.ReportSource = null;
                crvSalesBill.Dispose();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

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
               // clsgen.LogException(" page ShowKeyboard()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Order));

            }

        }


        #endregion

        private void txtfristname_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_SaleBill_YZoutPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtcompanyname_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_SaleBill_YZoutPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtph_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_SaleBill_YZoutPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.FunctionKey));
            }
        }

        private void txtaddress_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_SaleBill_YZoutPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtemailid_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_SaleBill_YZoutPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtgstno_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_SaleBill_YZoutPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void txtsecondname_Enter(object sender, EventArgs e)
        {
            if (FrmDashboard.Enb_SaleBill_YZoutPage_Normal)
            {
                ShowKeyboard(sender as TextBox, Convert.ToInt32(KeyboardType.Normal));
            }
        }

        private void rb_th_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_th.Checked)
            {
                ApplyFormSize();
                PrintPartyBill(); 
            }
        }

        private void rb_a4_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_a4.Checked)
            {
                ApplyFormSize();
                PrintPartyBill(); 
            }
        }
      
    }
}
