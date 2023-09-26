using BLL;
using CrystalDecisions.CrystalReports.Engine;
using Domain;
using FastMember;
using MarginPOSNew.Classes;
using MarginPOSNew.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarginPOSNew.Forms.UserControls
{
    public partial class FrmTest : MetroFramework.Forms.MetroForm
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string returnValue = string.Empty;
            DataTable dtAlign = new DataTable();
            dtAlign.Columns.Add();
            dtAlign.Columns.Add();
            dtAlign.Rows.Add("1", "R");
            dtAlign.Rows.Add("2", "R");
            dtAlign.Rows.Add("3", "R");

            DmnSearch _objSearch = new DmnSearch();
                       
            _objSearch.ColumnHeaders = "Id,First,Second,Age,Mob,Mob,Mob,Mob,Mob,Mob,Mob";
            _objSearch.Select = "Id as Id,FirstName as Field1,SecondName as Field2,Age as Field3,"+
                "Mob as Field4,Mob as Field5,Mob as Field6,Mob as Field7,Mob as Field8,Mob as Field9,Mob as Field10";
            _objSearch.Table = " tblTest ";
            _objSearch.WhereNdGroupBy = " ";
            _objSearch.OrderBy = " ";
            using (var search= new FrmSearch<DmnGridFields>(_objSearch,SearchMode.FirstCharacter,800,400,100,100,dtAlign))
            {
                var dialogResult = search.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    returnValue = Convert.ToString(search._retValue);
                }
            }
            MessageBox.Show(returnValue.ToString());

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                ClsDocument clsDocument = new ClsDocument();
                Bitmap bm = new Bitmap(pictureBox1.Image);
                clsDocument.UploadImage(bm, "2.jpeg", DocumentType.EmployeePhoto);
            }
            catch (Exception ex)
            {

            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFd = new OpenFileDialog();
            openFd.Filter = "Images only.| *.jpeg;*.jpg;*.png;*.gif;";
            DialogResult dr = openFd.ShowDialog();
            if (openFd.FileName != "")
            {
                string ext = Path.GetExtension(openFd.FileName);
                pictureBox1.Image = Image.FromFile(openFd.FileName);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
          ClsDocument clsDocument = new ClsDocument();
          Bitmap bitmap= clsDocument.DownloadImage("2.jpeg", DocumentType.EmployeePhoto);
          pictureBox1.Image = bitmap;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            ClsDocument clsDocument = new ClsDocument();
            var status= clsDocument.DeleteImage("2.jpeg", DocumentType.EmployeePhoto);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            ClsDocument clsDocument = new ClsDocument();
            var status = clsDocument.FileExistOrNot("2.jpeg", DocumentType.EmployeePhoto);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsSales = new DataSet();
                DataTable dtSalesHeader = new DataTable("dtSalesHeader");
                DataTable dtSalesMaster = new DataTable("dtSalesMaster");
                DataTable dtSalesDetail = new DataTable("dtSaldet");



                BllSearch<DmnSalesInvoiceBillHeaders> bllSearchHeaders = new BllSearch<DmnSalesInvoiceBillHeaders>();
                BllSearch<DmnSalesInvoiceBillDetails> bllSearchDetails = new BllSearch<DmnSalesInvoiceBillDetails>();
                BllSearch<DmnSalesInvoiceBill> bllSearchInvoiceBill = new BllSearch<DmnSalesInvoiceBill>();

                DmnSearch dmnSearchInvoiceHeaders = new DmnSearch();
                dmnSearchInvoiceHeaders.Select = " B.BranchName,B.BranchAddress,B.Email,B.Telephone,B.GSTNo,C.CompanyName";
                dmnSearchInvoiceHeaders.Table = " tblBranch as B " +
                                                " Left join tblCompany as c on C.CompanyId = B.CompanyId";
                dmnSearchInvoiceHeaders.WhereNdGroupBy = " where B.BranchId= 16";
                var invoiceBillHeaders = bllSearchHeaders.GetSearchData("prdSearch", dmnSearchInvoiceHeaders);
                using(var reader = ObjectReader.Create(invoiceBillHeaders, "BranchName","BranchAddress", "Email", "Telephone","GSTNo", "CompanyName"))
                {
                    dtSalesHeader.Load(reader);
                }

                DmnSearch dmnSearchInvoiceBill = new DmnSearch();
                dmnSearchInvoiceBill.Select = " S.EntryNo,S.EntryDate,A.LedgerName, "+
                                              " (Select Username from tblUserAccount where UserAccountId = 91) as Username, "+
                                              " S.TotalAmount,S.TotalDiscount,S.TotalTax,S.TotalCGST,S.TotalSGST,TotalCess,S.TotalQuantity," +
                                              " S.NetAmount,S.PaidAmount,S.BalanceAmount ";
                dmnSearchInvoiceBill.Table = " tblSalesMaster as S"+
                                             " Left join tblAccountLedger as A on S.LedgerId = A.LedgerId ";
                dmnSearchInvoiceBill.WhereNdGroupBy = " where EntryNo =2123";
                var invoiceBillMasters= bllSearchInvoiceBill.GetSearchData("prdSearch", dmnSearchInvoiceBill);
                using (var reader = ObjectReader.Create(invoiceBillMasters, "EntryNo", "EntryDate", "LedgerName", "Username", "TotalAmount", "TotalDiscount",
                    "TotalTax", "TotalCGST", "TotalSGST", "TotalCess", "TotalQuantity", "NetAmount", "PaidAmount", "BalanceAmount"))
                {
                    dtSalesMaster.Load(reader);
                }

                DmnSearch dmnSearchInvoiceDetails = new DmnSearch();
                dmnSearchInvoiceDetails.Select = " I.SearchName,S.Quantity,S.MRP,S.SalesRate,G.Caption,S.NetAmount";
                dmnSearchInvoiceDetails.Table = " tblSalesDetails as S Left join tblItem as I on I.ItemId = S.ItemId"+
                                                " Left join tblGeneralOptions as G on S.TaxType = G.GeneralId ";
                dmnSearchInvoiceDetails.WhereNdGroupBy = " where S.SalesMasterId=193";
                var invoiceBillDetails= bllSearchDetails.GetSearchData("prdSearch", dmnSearchInvoiceDetails);
                using (var reader = ObjectReader.Create(invoiceBillDetails, "SearchName", "Quantity", "MRP", "SalesRate", "Caption", "NetAmount"))
                {
                    dtSalesDetail.Load(reader);
                }
                dsSales.Tables.Add(dtSalesHeader);
                dsSales.Tables.Add(dtSalesMaster);
                dsSales.Tables.Add(dtSalesDetail);
                reportDocument.Load(@"C:\Lal\MarginPOS New\MarginPOS New\MarginPOSNew\Reports\crySalesInvPos.rpt");
                reportDocument.SetDataSource(dsSales);

                //crystalReportViewer1.ReportSource = reportDocument;
                //crystalReportViewer1.ReuseParameterValuesOnRefresh = true;
                //crystalReportViewer1.RefreshReport();

                if (reportDocument.PrintOptions.NoPrinter == false)
                {
                    reportDocument.PrintToPrinter(2, true, 0, 0);
                }
                else
                {
                    int allowdedFormat = (int)(CrystalDecisions.Shared.ViewerExportFormats.PdfFormat);
                    crystalReportViewer1.AllowedExportFormats = allowdedFormat;
                    crystalReportViewer1.ReportSource = reportDocument;
                    crystalReportViewer1.ReuseParameterValuesOnRefresh = true;
                    crystalReportViewer1.RefreshReport();
                    crystalReportViewer1.PrintMode = CrystalDecisions.Windows.Forms.PrintMode.PrintToPrinter;
                    crystalReportViewer1.PrintReport();
                }
                //reportDocument.Dispose();
                //reportDocument = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
