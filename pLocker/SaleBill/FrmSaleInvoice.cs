using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Domain;
using FastMember;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
using pLocker.Interfaces;

namespace pLocker.SaleBill
{
    public partial class FrmSaleInvoice : Form
    {
        public static  int LockerId;
        public static int BType;
        public static long BranchId;
        public static long UserId;
        public FrmSaleInvoice(int LocId, int billtype)
        {
             BranchId = Forms.frmLogin.BranchId;
             UserId = Forms.frmLogin.UserId;
            LockerId = LocId;
            BType = billtype;
            InitializeComponent();
        }

        public string folderPath = Application.StartupPath + "\\Barcode";
        public string RPTPath = @"C:\Users\ajay\Desktop\MidasFunGame_Plocker\pLocker\SaleBill\";//Application.StartupPath +@"\SaleBill";
        public string locationname_api;
        public decimal netamount_api = 0;
        public string cusname_api;
        public string invoiceno_api;
        public string pdfPath = "";
        private void FrmSaleInvoice_Load(object sender, EventArgs e)
        {
            if (BType == Convert.ToInt32(BillType.SingleLockerBill))
                PrintBill();
            else if (BType == Convert.ToInt32(BillType.BulkLockerBill))
                PrintBulkBill();
            else if (BType == Convert.ToInt32(BillType.MultiLockerBill))
                PrintMultiBill();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void PrintMultiBill()
        {
            try
            {
                string reportPath;
                #region Crystal Report Dclaration
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dtSalesHeader = new DataTable("DT_SaleHeader");
                DataTable dtSalesDetail = new DataTable("DT_saledetails");


                BllSearch<DmnSaleBillHeaders_PLocker> bllSearchHeaders = new BllSearch<DmnSaleBillHeaders_PLocker>();
                BllSearch<DmnSalesInvoiceBillDetails_Plocker> bllSearchDetails = new BllSearch<DmnSalesInvoiceBillDetails_Plocker>();
                //BllSearch<DmnSalesInvoiceBill> bllSearchInvoiceBill = new BllSearch<DmnSalesInvoiceBill>();

                DmnSearch dmnSearchInvoiceHeaders = new DmnSearch();
                #endregion

                dmnSearchInvoiceHeaders.Select = "Name,Address,Email,Phone,LocId,GSTNO";
                dmnSearchInvoiceHeaders.Table = "tblLocationDetails";
                dmnSearchInvoiceHeaders.WhereNdGroupBy = " where LocId=" + BranchId;
                var invoiceBillHeaders = bllSearchHeaders.GetSearchData("prdSearch", dmnSearchInvoiceHeaders);

                locationname_api = invoiceBillHeaders[0].Name;
                DmnSearch dmnSearchInvoiceBill = new DmnSearch();
                dmnSearchInvoiceBill.Select = " l.Locker,AccNo,AccPhone,AccEmail,NetAmount,BalanceAmount,DiscountAmount," +
                    " lh.PaymentMode,Rate,Amt,ExpDate,Hiredetail,HireTime,HireDate,ExpTime,EntryDate,PaidAmount,lh.LockID," +
                    " LedgerName as AccName,lh.EntryNo";
                dmnSearchInvoiceBill.Table = "tblLockerHire lh " +
                    " left join tblLocker l on lh.LockID=l.ID" +
                    " left join tblAccountLedger  bl on bl.LedgerId=lh.AccID";
                dmnSearchInvoiceBill.WhereNdGroupBy = " where lh.MultiLockHireID in (select MultiLockHireID from tblLockerHire where LockHireID=" + LockerId + ")";
                var invoiceBillDetails = bllSearchDetails.GetSearchData("prdSearch", dmnSearchInvoiceBill);

                //netamount_api = invoiceBillMasters[0].NetAmount;
                //invoiceno_api = invoiceBillMasters[0].EntryNo;
                //cusname_api = invoiceBillMasters[0].CusName;
                //if (cusname_api == "" || cusname_api == null)
                //    cusname_api = "Sir / Mam";

                //stroe barcode_img path header
                var imagePath = folderPath + "\\" + invoiceBillDetails[0].LockHireID + ".png";
                invoiceBillHeaders[0].barcode_img_path = imagePath;
                GenerateBarcode(Convert.ToString(invoiceBillDetails[0].LockHireID));
                using (var reader = ObjectReader.Create(invoiceBillDetails, "PaidAmount", "Locker", "LockID", "LockHireID", "AccNo", "AccPhone", "AccEmail", "NetAmount", "BalanceAmount", "DiscountAmount", "PaymentMode", "Rate", "Amt", "ExpDate", "Hiredetail", "HireTime", "HireDate", "ExpTime", "EntryDate", "AccName", "EntryNo"))
                {
                    dtSalesDetail.Load(reader);
                }
                //header part set data to dataset
                using (var reader = ObjectReader.Create(invoiceBillHeaders, "Name", "Address", "Email", "Phone", "LocId", "barcode_img_path", "GSTNo"))
                {
                    dtSalesHeader.Load(reader);
                }
                dsReport.Tables.Add(dtSalesDetail);
                dsReport.Tables.Add(dtSalesHeader);

                reportDocument.Load(RPTPath + @"\CR_Bulk_Bill.rpt");

                reportDocument.SetDataSource(dsReport);
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
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void PrintBulkBill()
        {
            try
            {
                string reportPath;
                #region Crystal Report Dclaration
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dtSalesHeader = new DataTable("DT_SaleHeader");
                DataTable dtSalesDetail = new DataTable("DT_saledetails");


                BllSearch<DmnSaleBillHeaders_PLocker> bllSearchHeaders = new BllSearch<DmnSaleBillHeaders_PLocker>();
                BllSearch<DmnSalesInvoiceBillDetails_Plocker> bllSearchDetails = new BllSearch<DmnSalesInvoiceBillDetails_Plocker>();
                //BllSearch<DmnSalesInvoiceBill> bllSearchInvoiceBill = new BllSearch<DmnSalesInvoiceBill>();
                DmnSearch dmnSearchInvoiceHeaders = new DmnSearch();
                #endregion

                dmnSearchInvoiceHeaders.Select = "Name,Address,Email,Phone,LocId,GSTNO";
                dmnSearchInvoiceHeaders.Table = "tblLocationDetails";
                dmnSearchInvoiceHeaders.WhereNdGroupBy = " where LocId=" + BranchId;
                var invoiceBillHeaders = bllSearchHeaders.GetSearchData("prdSearch", dmnSearchInvoiceHeaders);

                locationname_api = invoiceBillHeaders[0].Name;
                DmnSearch dmnSearchInvoiceBill = new DmnSearch();
                dmnSearchInvoiceBill.Select = " l.Locker,AccNo,bl.AccPhone,bl.AccEmail,bl.NetAmount,bl.BalanceAmount,bl.DiscountAmount,lh.PaymentMode,lh.Rate,lh.Amt,bl.ExpDate,bl.Hiredetail,bl.HireTime,bl.HireDate,bl.ExpTime,EntryDate,bl.PaidAmount,lh.LockID,bl.AccName,bl.EntryNo";
                dmnSearchInvoiceBill.Table = "tblLockerHire lh left join tblLocker l on lh.LockID=l.ID left join tblBulkLockHire  bl on bl.BulkLockHireID=lh.BulkLockHireID";
                dmnSearchInvoiceBill.WhereNdGroupBy = " where lh.BulkLockHireID = " + LockerId;
                var invoiceBillDetails = bllSearchDetails.GetSearchData("prdSearch", dmnSearchInvoiceBill);

                //netamount_api = invoiceBillMasters[0].NetAmount;
                //invoiceno_api = invoiceBillMasters[0].EntryNo;
                //cusname_api = invoiceBillMasters[0].CusName;
                //if (cusname_api == "" || cusname_api == null)
                //    cusname_api = "Sir / Mam";

                //stroe barcode_img path header
                var imagePath = folderPath + "\\" + invoiceBillDetails[0].EntryNo + ".png";
                invoiceBillHeaders[0].barcode_img_path = imagePath;
                GenerateBarcode(Convert.ToString(invoiceBillDetails[0].EntryNo));
                using (var reader = ObjectReader.Create(invoiceBillDetails, "PaidAmount", "Locker", "LockID", "LockHireID", "AccNo", "AccPhone", "AccEmail", "NetAmount", "BalanceAmount", "DiscountAmount", "PaymentMode", "Rate", "Amt", "ExpDate", "Hiredetail", "HireTime", "HireDate", "ExpTime", "EntryDate", "AccName", "EntryNo"))
                {
                    dtSalesDetail.Load(reader);
                }
                //header part set data to dataset
                using (var reader = ObjectReader.Create(invoiceBillHeaders, "Name", "Address", "Email", "Phone", "LocId", "barcode_img_path", "GSTNo"))
                {
                    dtSalesHeader.Load(reader);
                }
                dsReport.Tables.Add(dtSalesDetail);
                dsReport.Tables.Add(dtSalesHeader);

                reportDocument.Load(RPTPath + @"\CR_Bulk_Bill.rpt");

                reportDocument.SetDataSource(dsReport);
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
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PrintBill()
        {
            try
            {
                string reportPath;
                #region Crystal Report Dclaration
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dtSalesHeader = new DataTable("DT_SaleHeader");
                DataTable dtSalesDetail = new DataTable("DT_saledetails");


                BllSearch<DmnSaleBillHeaders_PLocker> bllSearchHeaders = new BllSearch<DmnSaleBillHeaders_PLocker>();
                BllSearch<DmnSalesInvoiceBillDetails_Plocker> bllSearchDetails = new BllSearch<DmnSalesInvoiceBillDetails_Plocker>();
                //BllSearch<DmnSalesInvoiceBill> bllSearchInvoiceBill = new BllSearch<DmnSalesInvoiceBill>();

                DmnSearch dmnSearchInvoiceHeaders = new DmnSearch();
                #endregion

                dmnSearchInvoiceHeaders.Select = "Name,Address,Email,Phone,LocId,GSTNO";
                dmnSearchInvoiceHeaders.Table = "tblLocationDetails";
                dmnSearchInvoiceHeaders.WhereNdGroupBy = " where LocId=" + BranchId;
                var invoiceBillHeaders = bllSearchHeaders.GetSearchData("prdSearch", dmnSearchInvoiceHeaders);

                locationname_api = invoiceBillHeaders[0].Name;
                DmnSearch dmnSearchInvoiceBill = new DmnSearch();
                dmnSearchInvoiceBill.Select = "lh.LockID,l.Locker,al.Mobile as AccPhone,al.EmailId as AccEmail,al.LedgerName as AccName,NetAmount,BalanceAmount,DiscountAmount,PaymentMode,Rate,Amt,ExpDate,Hiredetail,HireTime,HireDate,ExpTime,EntryDate,PaidAmount,lh.Entryno";
                dmnSearchInvoiceBill.Table = "tblLockerHire lh left join tblLocker l on lh.LockID=l.ID   left join tblAccountLedger al on al.LedgerId=lh.AccID";
                dmnSearchInvoiceBill.WhereNdGroupBy = " where LockHireID = " + LockerId;
                var invoiceBillDetails = bllSearchDetails.GetSearchData("prdSearch", dmnSearchInvoiceBill);

                //netamount_api = invoiceBillMasters[0].NetAmount;
                //invoiceno_api = invoiceBillMasters[0].EntryNo;
                //cusname_api = invoiceBillMasters[0].CusName;
                //if (cusname_api == "" || cusname_api == null)
                //    cusname_api = "Sir / Mam";

                //stroe barcode_img path header
                var imagePath = folderPath + "\\" + invoiceBillDetails[0].LockHireID + ".png";
                invoiceBillHeaders[0].barcode_img_path = imagePath;
                GenerateBarcode(Convert.ToString(invoiceBillDetails[0].LockHireID));
                using (var reader = ObjectReader.Create(invoiceBillDetails, "PaidAmount", "Locker", "LockID", "LockHireID", "AccNo", "AccPhone", "AccEmail", "NetAmount", "BalanceAmount", "DiscountAmount", "PaymentMode", "Rate", "Amt", "ExpDate", "Hiredetail", "HireTime", "HireDate", "ExpTime", "EntryDate", "AccName", "EntryNo"))
                {
                    dtSalesDetail.Load(reader);
                }
                //header part set data to dataset
                using (var reader = ObjectReader.Create(invoiceBillHeaders, "Name", "Address", "Email", "Phone", "LocId", "barcode_img_path", "GSTNo"))
                {
                    dtSalesHeader.Load(reader);
                }
                dsReport.Tables.Add(dtSalesDetail);
                dsReport.Tables.Add(dtSalesHeader);

                reportDocument.Load(RPTPath + @"\CR_Game_Sale_BillNew.rpt");

                reportDocument.SetDataSource(dsReport);
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
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            try
            {
                // Get the ReportDocument object used by the Crystal Report Viewer
                ReportDocument reportDocument = crvSalesBill.ReportSource as ReportDocument;
                // Set the printer name
                //reportDocument.PrintOptions.PrinterName = "Your Printer Name";
                // Print the report without showing the print dialog
                reportDocument.PrintToPrinter(1, false, 0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
