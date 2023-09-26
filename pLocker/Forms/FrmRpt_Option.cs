using BLL;
using Domain;
using FastMember;
using MarginPOSNew.Classes;
using MetroFramework;
using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;

namespace pLocker.Forms
{
    public partial class FrmRpt_Option : MetroFramework.Forms.MetroForm
    {
        #region Declarations 
        ClsGeneral objGeneral = new ClsGeneral();
        string Dtformat = "dd-MMM-yyyy";

        long BranchId = frmLogin.BranchId;// Convert.ToInt64(Forms.UserAccounts.FrmLogin.BranchId);//16
        long UserId = 1;//Convert.ToInt64(Forms.UserAccounts.FrmLogin.UserId);//1
        long FinancialYearId = 1;// Convert.ToInt64(FrmDashboard.FinancialYearId);
        int nRPTOpt;
        bool RPTLocalVw = false;
       
        #endregion
        public FrmRpt_Option()
        {
           InitializeComponent();
        }
        public FrmRpt_Option(int nOpt)
        {
            InitializeComponent();

            nRPTOpt = nOpt;
        }
        private string RPTVWFile(string RepFName)
        {
            string fstr1 = "";

            if (RPTLocalVw == false)
            {
                fstr1 = @"C:\Users\ajay\Desktop\MidasFunGame_Plocker\pLocker\Report\";//Application.StartupPath + "\\Reports\\"+RepFName;
            }
            else
            {
                //fstr1 = @"D:\mPOS\mPOS\MarginPOSNew\Reports\" + RepFName;
                fstr1 = fstr1 = @"C:\Users\ajay\Desktop\MidasFunGame_Plocker\pLocker\Report\" + RepFName;
            }
            return fstr1;
        }
 
        public void s_FillReport5()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport_Sale = new DataSet();
                DataTable dtSales_RPT = new DataTable("dtSale_RPT");
                BllSearch<DmnSales_RPT> bllSearchDetails = new BllSearch<DmnSales_RPT>();
                DmnSearch dmnSearchDetBill = new DmnSearch();
                switch (Convert.ToInt32(mcmbReports.SelectedValue))
                {
                    case 1:
                        dmnSearchDetBill.Select = "dbo.tblLockerHire.EntryDate AS dt1,dbo.tblAccountLedger.LedgerName AS sFld1,dbo.tblLockerHire.AccPhone AS sFld2,dbo.tblLockerHire.AccNo sFld3, " +
                                                    " dbo.tblLockerHire.HireFor AS nFld1, dbo.tblLockerHire.RateType AS nFld2,dbo.tblLockerHire.HireDate AS dt2,dbo.tblLockerHire.HireTime As dt2,  " +
                                                     " dbo.tblLockerHire.ExpDate AS dt3,dbo.tblLockerHire.ExpTime As dt3, dbo.tblLockerHire.Amt as nFld3 ";



                        dmnSearchDetBill.Table = "dbo.tblLockerHire INNER JOIN dbo.tblLocker ON dbo.tblLockerHire.LockID = dbo.tblLocker.ID" +
                                                 " inner join dbo.tblAccountLedger on dbo.tblAccountLedger.LedgerId = dbo.tblLockerHire.AccID ";


                        dmnSearchDetBill.WhereNdGroupBy = " WHERE dbo.tblLockerHire.EntryDate  BETWEEN (SELECT convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "', 105),23)) AND " +
                                                          " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) AND tblLockerHire.BranchId = " + BranchId; 
 



                        //dmnSearchDetBill.WhereNdGroupBy = "WHERE P.EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "', 105),23)) AND " +
                        //                              " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) AND P.BranchId = " + BranchId;

                        break;
                }
               
                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);
                if (invoiceBillMasters.Count <= 0)
                {
                    MetroMessageBox.Show(this,"No Data Found !!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (invoiceBillMasters.Count > 0)
                {
                    invoiceBillMasters[0].FromDate = mdtpFrom.Text;
                    invoiceBillMasters[0].ToDate = mdtpTo.Text;
                }
                using (var reader = ObjectReader.Create(invoiceBillMasters, "dt1", "dt2", "dt3", "sFld1", "sFld2", "sFld3", "nFld1", "nFld2", "nFld3"))
                {
                    dtSales_RPT.Load(reader);
                }
                dsReport_Sale.Tables.Add(dtSales_RPT);

                switch (Convert.ToInt32(mcmbReports.SelectedValue))
                {

                    case 1:
                        reportDocument.Load(RPTVWFile("RPT_LockerHIreDetails.rpt"));
                        break;
                }

                reportDocument.SetDataSource(dsReport_Sale);

                reportDocument.SetParameterValue("myFDt", mdtpFrom.Value.ToString(Dtformat));
                reportDocument.SetParameterValue("myTDt", mdtpTo.Value.ToString(Dtformat));

                CrvPlocker.ReuseParameterValuesOnRefresh = true;
                CrvPlocker.RefreshReport();
                CrvPlocker.ReportSource = reportDocument;


            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this,ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //new

        public void s_FillReport6()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport_Sale = new DataSet();
                DataTable dtSales_RPT = new DataTable("dtSale_RPT");
                BllSearch<DmnSales_RPT> bllSearchDetails = new BllSearch<DmnSales_RPT>();

                DmnSearch dmnSearchDetBill = new DmnSearch();


                switch (Convert.ToInt32(mcmbReports.SelectedValue))
                {
                    case 2:
                        dmnSearchDetBill.Select = "dbo.tblAccountLedger.EnrollDate AS dt1,dbo.tblAccountLedger.LedgerName AS sFld1,dbo.tblAccountLedger.Mobile AS sFld2,dbo.tblAccountLedger.LedgerCode sFld3," +
                                                     "dbo.tblAccountLedger.zipcode AS nFld1, dbo.tblAccountLedger.LedgerAddress AS sFld4,dbo.tblAccountLedger.EmailId AS sFld5,dbo.tblAccountLedger.city As sFld6 ";



                        dmnSearchDetBill.Table = " dbo.tblAccountLedger";
                                                


                        dmnSearchDetBill.WhereNdGroupBy = " WHERE dbo.tblAccountLedger.EnrollDate  BETWEEN (SELECT convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "', 105),23)) AND " +
                                                          " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) AND tblAccountLedger.BranchId = " + BranchId;




                        //dmnSearchDetBill.WhereNdGroupBy = "WHERE P.EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "', 105),23)) AND " +
                        //                              " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) AND P.BranchId = " + BranchId;

                        break;
                }

                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);
                if (invoiceBillMasters.Count <= 0)
                {
                    MetroMessageBox.Show(this,"No Data Found !!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (invoiceBillMasters.Count > 0)
                {
                    invoiceBillMasters[0].FromDate = mdtpFrom.Text;
                    invoiceBillMasters[0].ToDate = mdtpTo.Text;
                }
                using (var reader = ObjectReader.Create(invoiceBillMasters, "dt1", "sFld1", "sFld2", "sFld3", "sFld4", "sFld5", "sFld6", "nFld1"))
                {
                    dtSales_RPT.Load(reader);
                }
                dsReport_Sale.Tables.Add(dtSales_RPT);

                switch (Convert.ToInt32(mcmbReports.SelectedValue))
                {

                    case 2:
                        reportDocument.Load(RPTVWFile("RPT_LockerCustDetails.rpt"));
                        break;
                }

                reportDocument.SetDataSource(dsReport_Sale);

                reportDocument.SetParameterValue("myFDt", mdtpFrom.Value.ToString(Dtformat));
                reportDocument.SetParameterValue("myTDt", mdtpTo.Value.ToString(Dtformat));

                CrvPlocker.ReuseParameterValuesOnRefresh = true;
                CrvPlocker.RefreshReport();
                CrvPlocker.ReportSource = reportDocument;


            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this,ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //new

        public void s_FillReport7()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport_Sale = new DataSet();
                DataTable dtSales_RPT = new DataTable("dtSale_RPT");
                BllSearch<DmnSales_RPT> bllSearchDetails = new BllSearch<DmnSales_RPT>();

                DmnSearch dmnSearchDetBill = new DmnSearch();


                switch (Convert.ToInt32(mcmbReports.SelectedValue))
                {
                    case 3:
                        dmnSearchDetBill.Select = "dbo.tblLocker.ID AS nFld1,dbo.tblLocker.Locker AS sFld1,dbo.tblLocker.RFID sFld2," +
                                                  "dbo.tblLocker.HrRate AS nFld2, dbo.tblLocker.DyRate AS nFld3,dbo.tblLocker.MnRate AS nFld4,dbo.tblLocker.Hired as nFld5 ";



                        dmnSearchDetBill.Table = "dbo.tblLocker";



                        // dmnSearchDetBill.WhereNdGroupBy = " WHERE dbo.tblAccountLedger.EnrollDate  BETWEEN (SELECT convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "', 105),23)) AND " +
                        // " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) AND tblAccountLedger.BranchId = " + BranchId;




                        //dmnSearchDetBill.WhereNdGroupBy = "WHERE P.EntryDate BETWEEN (SELECT convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "', 105),23)) AND " +
                        //                              " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) AND P.BranchId = " + BranchId;


                        dmnSearchDetBill.WhereNdGroupBy = "WHERE dbo.tblLocker.status=1 and  dbo.tblLocker.BranchId = " + BranchId;

                        break;
                }

                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);
                if (invoiceBillMasters.Count <= 0)
                {
                    MetroMessageBox.Show(this,"No Data Found !!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (invoiceBillMasters.Count > 0)
                {
                   // invoiceBillMasters[0].FromDate = mdtpFrom.Text;
                    //invoiceBillMasters[0].ToDate = mdtpTo.Text;
                }
                using (var reader = ObjectReader.Create(invoiceBillMasters, "nFld1", "nFld2", "nFld3", "nFld4", "nFld5", "sFld1", "sFld2"))
                {
                    dtSales_RPT.Load(reader);
                }
                dsReport_Sale.Tables.Add(dtSales_RPT);

                switch (Convert.ToInt32(mcmbReports.SelectedValue))
                {

                    case 3:
                        reportDocument.Load(RPTVWFile("RPT_LockerDetails.rpt"));
                        break;
                }

                reportDocument.SetDataSource(dsReport_Sale);

                reportDocument.SetParameterValue("myFDt", mdtpFrom.Value.ToString(Dtformat));
                reportDocument.SetParameterValue("myTDt", mdtpTo.Value.ToString(Dtformat));

                CrvPlocker.ReuseParameterValuesOnRefresh = true;
                CrvPlocker.RefreshReport();
                CrvPlocker.ReportSource = reportDocument;


            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this,ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //add new
        public void s_FillReport8()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport_Sale = new DataSet();
                DataTable dtSales_RPT = new DataTable("dtSale_RPT");
                BllSearch<DmnSales_RPT> bllSearchDetails = new BllSearch<DmnSales_RPT>();

                DmnSearch dmnSearchDetBill = new DmnSearch();


                switch (Convert.ToInt32(mcmbReports.SelectedValue))
                {
                    case 4:
                        dmnSearchDetBill.Select = "dbo.tblLockerHire.EntryDate AS dt1,dbo.tblAccountLedger.LedgerName AS sFld1, " +
                                                    " dbo.tblLockerHire.NetAmount as nFld1, dbo.tblLockerHire.HireFor AS nFld2, tgl.Caption AS sFld2,dbo.tblGeneralOptions.Caption as sFld3 ";
                                                  



                        dmnSearchDetBill.Table = " dbo.tblLockerHire left join dbo.tblAccountLedger on dbo.tblLockerHire.AccID = dbo.tblAccountLedger.LedgerId left join dbo.tblGeneralOptions on dbo.tblGeneralOptions.GTypeSubId = dbo.tblLockerHire.HireFor  left join dbo.tblGeneralOptions tgl on tgl.GTypeSubId = dbo.tblLockerHire.PaymentMode";



                        dmnSearchDetBill.WhereNdGroupBy = " WHERE dbo.tblLockerHire.EntryDate  BETWEEN (SELECT convert(varchar(10), Convert(date,'" + mdtpFrom.Text + "', 105),23)) AND " +
                                                          " (SELECT convert(varchar(10), Convert(date,'" + mdtpTo.Text + "',105), 23)) and dbo.tblGeneralOptions.GTypeId=28 AND tgl.GTypeId=2 and  tblLockerHire.BranchId = " + BranchId ;


                        break;
                }

                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);
                if (invoiceBillMasters.Count <= 0)
                {
                    MetroMessageBox.Show(this, "No Data Found !!!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (invoiceBillMasters.Count > 0)
                {
                   invoiceBillMasters[0].FromDate = mdtpFrom.Text;
                    invoiceBillMasters[0].ToDate = mdtpTo.Text;
                }
                using (var reader = ObjectReader.Create(invoiceBillMasters, "dt1", "sFld1", "nFld1", "nFld2", "sFld2", "sFld3"))
                {
                    dtSales_RPT.Load(reader);
                }
                dsReport_Sale.Tables.Add(dtSales_RPT);

                switch (Convert.ToInt32(mcmbReports.SelectedValue))
                {

                    case 4:
                        reportDocument.Load(RPTVWFile("RPT_LockerPaymentDetails.rpt"));
                        break;
                }

                reportDocument.SetDataSource(dsReport_Sale);

                reportDocument.SetParameterValue("myFDt", mdtpFrom.Value.ToString(Dtformat));
                reportDocument.SetParameterValue("myTDt", mdtpTo.Value.ToString(Dtformat));

                CrvPlocker.ReuseParameterValuesOnRefresh = true;
                CrvPlocker.RefreshReport();
                CrvPlocker.ReportSource = reportDocument;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this,ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        public void Sale_ReportOpt()
        {
            try
            {
                lblRProgress.Visible = true;
                int nVal1 = Convert.ToInt32(mcmbReports.SelectedValue);
                switch (nVal1)
                {
                    case 1:
              
                        s_FillReport5();
                        break;

                    case 2:

                        s_FillReport6();
                        break;

                    case 3:

                        s_FillReport7();
                        break;
                    case 4:

                        s_FillReport8();
                        break;

                    default:
                        break;
                }
                lblRProgress.Visible = false;

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }
     
        private void mbtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //crvReportSalesDetails.Visible = true;
                int nRPTOpt = Convert.ToInt32(mcmbReports.SelectedValue);
                switch (nRPTOpt)
                {
                    case 1: //Sale
                        Sale_ReportOpt();
                        break;
                    case 2: //Sale
                        Sale_ReportOpt();
                        break;
                    case 3: //Sale
                        Sale_ReportOpt();
                        break;
                    case 4: //Sale
                        Sale_ReportOpt();
                        break;



                    default:
                        break;

                }
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
                FillReportList(nRPTOpt);
               
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void RefreshRPT()
        {
            //crvReportSalesDetails.ReportSource = "";
            //crvReportSalesDetails.Visible = false;
        }
        private void FrmRpt_Option_Load(object sender, EventArgs e)
        {
            try
           {
                lblRProgress.Visible = false;
                //this.crvReportSalesDetails.Size = new System.Drawing.Size(1010, 434);
                mcmbField.Visible = false;
                mtxtFieldValue.Visible = false;

                DateTime date1 = new DateTime(2020, 04, 01);
                mdtpFrom.Value = date1;

                //this.crvReportSalesDetails.Size = new System.Drawing.Size(1010, this.Height);
                //this.ControlBox = false;

                int nOpt = 48;
                switch (nRPTOpt)
                {
                    case 1: //Sale
                        nOpt = 48;
                        break;
           
                    default:
                        //nOpt = 32;
                        break;
                }

                FillReportList(nOpt);

                RPTLocalVw= Convert.ToBoolean(@ConfigurationManager.AppSettings["ReportFileFromLocal"]);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        public void FillReportList(Int32 nOpt)
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = " G.GTypeSubId, G.Caption";
            objSearch.Table = " tblGeneralOptions G";
            objSearch.WhereNdGroupBy = " WHERE G.GTypeId=" + nOpt ;
            objSearch.OrderBy = "order by id";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbReports.DataSource = gridSource;
            mcmbReports.ValueMember = "Id";
            mcmbReports.DisplayMember = "value";
            mcmbReports.SelectedIndex = 1;

        }
        public void FillReport_FilterOpt(Int32 nROpt)
        {
            //var sQry1 = "";
            //BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            //DmnSearch objSearch = new DmnSearch();
            //objSearch = new DmnSearch();
            //objSearch.Select = " G.GTypeSubId, G.Caption";
            //objSearch.Table = " tblGeneralOptions G";

            //int nVal1 = Convert.ToInt32(mcmbReports.SelectedValue);

            //mdtpFrom.Visible = true;
            //mdtpTo.Visible = true;
            //mlblFrom.Visible = true;
            //mlblTo.Visible = true;

            //mcmbField.Visible = true;
            //mtxtFieldValue.Visible = true;


            ////objSearch.WhereNdGroupBy = "WHERE G.GTypeId = 23 and "+sQry1;
            //objSearch.WhereNdGroupBy = "WHERE G.GTypeId = 35 and " + sQry1;
            //objSearch.OrderBy = "order by id";
            //var gridSource1 = bllSearch.GetSearchData("prdComboFill", objSearch);
            //mcmbField.DataSource = gridSource1;
            //mtxtFieldValue.Text = "";
            //mcmbField.ValueMember = "Id";
            //mcmbField.DisplayMember = "Value";
            //mcmbField.SelectedIndex = 0;
        }
        private void mbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        private void FrmRpt_Option_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                //DialogResult dialogResult = MetroMessageBox.Show(this, "Want to Close " + "?", " ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, 150);
                //if (dialogResult == DialogResult.Yes)
                //{

                //if (crvReportSalesDetails.Visible == false)
                //    this.Close();
                //else
                //        crvReportSalesDetails.Visible = false;
                    //RefreshRPT();
                //System.Windows.Forms.Application.ExitThread();
                //   }
            }

        }
        private void mcmbReports_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillReport_FilterOpt(nRPTOpt);
        }
        private void mtxtFieldValue_TextChanged(object sender, EventArgs e)
        {
            if (mtxtFieldValue.Text.Length==0)
            {
                mcmbField.SelectedIndex = 0;
            }
        }
        private void mcmbField_Enter(object sender, EventArgs e)
        {
            mcmbReports_SelectionChangeCommitted(null, null);
        }
        private void mtxtFieldValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                mbtnSearch.Focus();
            }

        }
        private void mcmbField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                mtxtFieldValue.Focus();
            }

        }
        private void mcmbReports_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                mcmbField.Focus();
                if (mdtpFrom.Visible==true)
                {
                    mdtpFrom.Focus();
                }
                
            }

        }
        private void mdtpFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                mbtnSearch.Focus();
                if (mdtpTo.Visible == true)
                {
                    mdtpTo.Focus();
                }

                
            }

        }
        private void mdtpTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                mbtnSearch.Focus();
            }

        }

        private void FrmRpt_Option_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Escape)
                    this.Close();

                //    btnSave_Click(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        private void btnFrmClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //search
        private void mbtnSearch_MouseHover(object sender, EventArgs e)
        {
            this.mbtnSearch.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.search_3));
        }

        private void mbtnSearch_MouseDown(object sender, MouseEventArgs e)
        {
            this.mbtnSearch.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.search_2));
        }
        
        private void mbtnSearch_MouseLeave(object sender, EventArgs e)
        {
            this.mbtnSearch.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.search_1));
        }

        private void mbtnSearch_MouseUp(object sender, MouseEventArgs e)
        {
            this.mbtnSearch.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.search_1));
        }

        //close
        private void mbtnClose_MouseHover(object sender, EventArgs e)
        {
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_3));
        }

        private void mbtnClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_2));
        }

        private void mbtnClose_MouseLeave(object sender, EventArgs e)
        {
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_1));
        }

        private void mbtnClose_MouseUp(object sender, MouseEventArgs e)
        {
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_1));
        }

        private void lblRProgress_Click(object sender, EventArgs e)
        {

        }
    }
}

