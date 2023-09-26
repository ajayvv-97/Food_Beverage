using BLL;
using CrystalDecisions.CrystalReports.Engine;
using Domain;
using FastMember;
using MarginPOSNew.Classes;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarginPOSNew.Forms.Reports
{
    public partial class FrmRptSalesDetail : MetroFramework.Forms.MetroForm
    {
        #region Declarations 
        ClsGeneral objGeneral = new ClsGeneral();
        long BranchId = Convert.ToInt64(FrmDashboard.BranchId);//16
        long UserId = Convert.ToInt64(FrmDashboard.UserId);//1
        long FinancialYearId = Convert.ToInt64(FrmDashboard.FinancialYearId);//10
        #endregion
        public FrmRptSalesDetail()
        {
            InitializeComponent();
        }
        public void FillReport()
        {
            #region Print bill
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsSalesDetails = new DataSet();
                DataTable dtSalesHeader = new DataTable("dtSalesHeader");
                DataTable dtsalesDetails = new DataTable("dtsalesDetails");



                BllSearch<DmnSalesInvoiceBillHeaders> bllSearchHeaders = new BllSearch<DmnSalesInvoiceBillHeaders>();
                BllSearch<DmnRptSalesDetails> bllSearchDetails = new BllSearch<DmnRptSalesDetails>();

                DmnSearch dmnSearchInvoiceHeaders = new DmnSearch();
                dmnSearchInvoiceHeaders.Select = " B.BranchName,B.BranchAddress,B.Email,B.Telephone,B.GSTNo,C.CompanyName";
                dmnSearchInvoiceHeaders.Table = " tblBranch as B " +
                                                " inner join tblCompany as c on C.CompanyId = B.CompanyId";
                dmnSearchInvoiceHeaders.WhereNdGroupBy = " where B.BranchId=" + BranchId;
                var invoiceBillHeaders = bllSearchHeaders.GetSearchData("prdSearch", dmnSearchInvoiceHeaders);
                using (var reader = ObjectReader.Create(invoiceBillHeaders, "BranchName", "BranchAddress", "Email", "Telephone", "GSTNo", "CompanyName"))
                {
                    dtSalesHeader.Load(reader);
                }

                DmnSearch dmnSearchDetBill = new DmnSearch();
                dmnSearchDetBill.Select = " DISTINCT S.DeviceDescription, S.EntryDate,S.EntryNo,S.TotalQuantity,S.NetAmount, S.DeviceDescription as FromDate, S.DeviceDescription as ToDate ";
                dmnSearchDetBill.Table = " tblSalesMaster as S ";
                dmnSearchDetBill.WhereNdGroupBy = " WHERE S.EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "', 105),23)) AND " +
                        " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) And S.Status = 0 And S.BranchId = " + BranchId;
               //dmnSearchDetBill.WhereNdGroupBy = " where s.BranchId ='" + BranchId + "'";
                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);
                if (invoiceBillMasters.Count > 0)
                {
                    invoiceBillMasters[0].FromDate = mdtpFrom.Text;
                    invoiceBillMasters[0].ToDate = mdtpTo.Text;
                }
                using (var reader = ObjectReader.Create(invoiceBillMasters, "DeviceDescription", "EntryDate", "EntryNo", "TotalQuantity", "NetAmount", "FromDate", "ToDate"))
                {
                    dtsalesDetails.Load(reader);
                }
                dsSalesDetails.Tables.Add(dtsalesDetails);
                //reportDocument.Load(@"C:\Sophia\New\MarginPOSNew\Reports\crptSalesDetailsA4.rpt");
                //reportDocument.Load(@"C:\Lal\MarginPOS New\MarginPOS New\MarginPOSNew\Reports\crptSalesDetailsA4.rpt");
                reportDocument.Load(Application.StartupPath + "\\Reports\\crptSalesDetailsA4.rpt");
                reportDocument.SetDataSource(dsSalesDetails);
                crvReportSalesDetails.ReportSource = reportDocument;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
        }
        private void mbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                FillReport();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mbtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                mdtpFrom.Text = Convert.ToString(DateTime.Now);
                mdtpTo.Text = Convert.ToString(DateTime.Now);
                FillReport();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void FrmRptSalesDetail_Load(object sender, EventArgs e)
        {
            try
            {
                FillReport();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
    }
}
