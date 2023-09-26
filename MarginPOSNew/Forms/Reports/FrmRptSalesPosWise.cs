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
    public partial class FrmRptSalesPosWise : MetroFramework.Forms.MetroForm
    {
        #region Declarations 
        ClsGeneral objGeneral = new ClsGeneral();
        long BranchId = Convert.ToInt64(FrmDashboard.BranchId);//16
        long UserId = Convert.ToInt64(FrmDashboard.UserId);//1
        long FinancialYearId = Convert.ToInt64(FrmDashboard.FinancialYearId);//10
        #endregion
        public FrmRptSalesPosWise()
        {
            InitializeComponent();
        }
        public void FillReport()
        {
            #region Print bill
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsSalesPosWise = new DataSet();
                DataTable dtSalesHeader = new DataTable("dtSalesHeader");
                DataTable dtSalesPosWise = new DataTable("dtSalesPosWise");



                BllSearch<DmnSalesInvoiceBillHeaders> bllSearchHeaders = new BllSearch<DmnSalesInvoiceBillHeaders>();
                BllSearch<DmnRptsalesWise> bllSearchDetails = new BllSearch<DmnRptsalesWise>();

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
                dmnSearchDetBill.Select = "DISTINCT S.EntryDate,"
                    +"(select count(EntryNo) from tblSalesMaster Where EntryDate = S.EntryDate) as TotalBill, "
                    +"(select Sum(NetAmount) from tblSalesMaster Where EntryDate = S.EntryDate) as TotalAmt, S.DeviceDescription";
                dmnSearchDetBill.Table = " tblSalesMaster as S ";
                dmnSearchDetBill.WhereNdGroupBy = " WHERE S.EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "', 105),23)) AND " +
                        " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) And S.Status = 0 And S.BranchId = " + BranchId;
                //dmnSearchDetBill.WhereNdGroupBy = " where S.BranchId ='" + BranchId + "'";
                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);
                using (var reader = ObjectReader.Create(invoiceBillMasters, "EntryDate", "TotalBill", "TotalAmt", "DeviceDescription"))
                {
                    dtSalesPosWise.Load(reader);
                }

                dsSalesPosWise.Tables.Add(dtSalesHeader);
                dsSalesPosWise.Tables.Add(dtSalesPosWise);
                //reportDocument.Load(@"C:\Sophia\New\MarginPOSNew\Reports\crptSalesPosWiseA4.rpt");
                //reportDocument.Load(@"C:\Lal\MarginPOS New\MarginPOS New\MarginPOSNew\Reports\crptSalesPosWiseA4.rpt");
                reportDocument.Load(Application.StartupPath + "\\Reports\\crptSalesPosWiseA4.rpt");
                reportDocument.SetDataSource(dsSalesPosWise);
                crvReportSalesPosWise.ReportSource = reportDocument;
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
