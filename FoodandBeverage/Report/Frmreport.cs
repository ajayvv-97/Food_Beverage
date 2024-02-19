using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Domain;
using FastMember;
using FoodandBeverage.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage.Report
{
    public partial class Frmreport : Form
    {
        public static int BranchId = 0;
        public static int UserId = 0;
        public static int UserType = 0;
        public static int Allaccess = 0;

        public Frmreport()
        {
            InitializeComponent();
            UserId = Convert.ToInt32(FrmLogin.UserId);
            BranchId = Convert.ToInt32(FrmLogin.BranchId);
            UserType = Convert.ToInt32(FrmLogin.UserAccountTypeId);
        }
        #region report declaartio
        string Dtformat = "dd-MM-yyyy";
        string Timeformat = "HH:mm:ss";
        DateTime fTime, tTime;
        public List<DmnHRegistration> Addressdata;
        string Path = @"C:\Users\ajay\Desktop\Food_Beverage\FoodandBeverage\Report";//Application.StartupPath + @"\Report";
        public Color selectedColor;
        public bool ColorChk = false;
        #endregion
        private void Frmreport_Load(object sender, EventArgs e)
        {
            combofill_report();
            Fill_combo_finyear();
        }
        public void Fill_combo_finyear()
        {
            BllSearch<DmnHRegistration> bllSearch = new BllSearch<DmnHRegistration>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "*";
            objSearch.Table = "tblHotelRegistration";
            objSearch.WhereNdGroupBy = "Where status=1 and BranchId=" + BranchId;
            objSearch.OrderBy = "";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
            int startYear = Convert.ToInt32(itemSource[0].CreatedOn.ToString("yyyy"));
            DateTime currentDate = DateTime.Now;
            List<DmnComboBox> finYearList = new List<DmnComboBox>();
            for (int i = currentDate.Year - startYear; i >= 0; i--)
            {
                int year = startYear + i;
                DateTime financialYearStart = new DateTime(year, 4, 1);
                DateTime financialYearEnd = new DateTime(year + 1, 3, 31);
                string financialYear = string.Format("{0}-{1}", financialYearStart.ToString("yyyy"), financialYearEnd.ToString("yyyy"));
                DmnComboBox finYear = new DmnComboBox { Id = year, Value = financialYear };
                finYearList.Add(finYear);
            }
            cmb_fin_year.DataSource = finYearList;
            cmb_fin_year.DisplayMember = "Value";
            cmb_fin_year.ValueMember = "Id";
        }

        private void chk_time_CheckedChanged(object sender, EventArgs e)
        {
            Checktime_Date();
        }
        public void Checktime_Date()
        {
            Control controlToRemove, controlToRemove1, controlToRemove2;

            if (chk_time.Checked)
            {
                if (this.Width > 805)
                {
                    controlToRemove = tabDateTimePanel.GetControlFromPosition(3, 1);
                    tabDateTimePanel.Controls.Remove(controlToRemove);
                    controlToRemove1 = tabDateTimePanel.GetControlFromPosition(6, 1);
                    tabDateTimePanel.Controls.Remove(controlToRemove1);

                    controlToRemove2 = tlp_top_row.GetControlFromPosition(3, 0);
                    tlp_top_row.Controls.Remove(controlToRemove2);

                    dtpfrom.Dock = DockStyle.Fill;
                    dtpto.Dock = DockStyle.Fill;
                    label5.Dock = DockStyle.Fill;
                    label6.Dock = DockStyle.Fill;
                    dtp_from_time.Dock = DockStyle.Fill;
                    dtp_to_time.Dock = DockStyle.Fill;
                    tabDateTimePanel.Controls.Add(dtpfrom, 2, 0);
                    tabDateTimePanel.Controls.Add(dtpto, 5, 0);
                    tabDateTimePanel.Controls.Add(label6, 1, 0);
                    tabDateTimePanel.Controls.Add(label5, 4, 0);
                    tabDateTimePanel.Controls.Add(dtp_from_time, 3, 0);
                    tabDateTimePanel.Controls.Add(dtp_to_time, 6, 0);
                }
                else
                {
                    if (Convert.ToInt32(cmbreport.SelectedValue) == 36)
                    {
                        if (chk_time.Checked == true)
                        {
                            dtpfrom.Dock = DockStyle.Fill;
                            dtpto.Dock = DockStyle.Fill;
                            label6.Dock = DockStyle.Fill;
                            label5.Dock = DockStyle.Fill;
                            dtp_from_time.Dock = DockStyle.Fill;
                            dtp_to_time.Dock = DockStyle.Fill;
                            tabDateTimePanel.Controls.Add(dtpfrom, 2, 0);
                            tabDateTimePanel.Controls.Add(dtpto, 5, 0);
                            tabDateTimePanel.Controls.Add(label6, 1, 0);
                            tabDateTimePanel.Controls.Add(label5, 4, 0);
                            tabDateTimePanel.Controls.Add(dtp_from_time, 3, 0);
                            tabDateTimePanel.Controls.Add(dtp_to_time, 6, 0);
                            //btn_search_second.Visible = false;
                        }
                        else
                        {
                            controlToRemove = tabDateTimePanel.GetControlFromPosition(3, 0);
                            tabDateTimePanel.Controls.Remove(controlToRemove);
                            controlToRemove1 = tabDateTimePanel.GetControlFromPosition(6, 0);
                            tabDateTimePanel.Controls.Remove(controlToRemove1);

                            dtpfrom.Dock = DockStyle.Fill;
                            dtpto.Dock = DockStyle.Fill;
                            label6.Dock = DockStyle.Fill;
                            label5.Dock = DockStyle.Fill;
                            dtp_from_time.Dock = DockStyle.Fill;
                            dtp_to_time.Dock = DockStyle.Fill;

                            tabDateTimePanel.Controls.Add(dtpfrom, 2, 0);
                            tabDateTimePanel.Controls.Add(dtpto, 5, 0);

                            tabDateTimePanel.Controls.Add(chk_time, 0, 0);
                            tabDateTimePanel.Controls.Add(label6, 1, 0);
                            tabDateTimePanel.Controls.Add(label5, 4, 0);
                        }

                    }
                    else
                    {
                        controlToRemove = tabDateTimePanel.GetControlFromPosition(3, 0);
                        tabDateTimePanel.Controls.Remove(controlToRemove);
                        controlToRemove1 = tabDateTimePanel.GetControlFromPosition(6, 0);
                        tabDateTimePanel.Controls.Remove(controlToRemove1);

                        dtpfrom.Dock = DockStyle.Fill;
                        dtpto.Dock = DockStyle.Fill;
                        label6.Dock = DockStyle.Fill;
                        label5.Dock = DockStyle.Fill;
                        dtp_from_time.Dock = DockStyle.Fill;
                        dtp_to_time.Dock = DockStyle.Fill;

                        tabDateTimePanel.Controls.Add(dtpfrom, 2, 0);
                        tabDateTimePanel.Controls.Add(dtpto, 5, 0);
                        tabDateTimePanel.Controls.Add(label6, 4, 0);
                        tabDateTimePanel.Controls.Add(label5, 4, 1);
                        tabDateTimePanel.Controls.Add(dtp_from_time, 2, 1);
                        tabDateTimePanel.Controls.Add(dtp_to_time, 5, 1);

                    }
                }
                dtp_from_time.Visible = true;
                dtp_to_time.Visible = true;
                tabDateTimePanel.PerformLayout();
            }
            else
            {
                if (this.Width > 805)
                {
                    controlToRemove = tabDateTimePanel.GetControlFromPosition(3, 1);
                    tabDateTimePanel.Controls.Remove(controlToRemove);
                    controlToRemove1 = tabDateTimePanel.GetControlFromPosition(6, 1);
                    tabDateTimePanel.Controls.Remove(controlToRemove1);
                    dtpfrom.Dock = DockStyle.Fill;
                    dtpto.Dock = DockStyle.Fill;
                    label6.Dock = DockStyle.Fill;
                    label5.Dock = DockStyle.Fill;
                    dtp_from_time.Dock = DockStyle.Fill;
                    dtp_to_time.Dock = DockStyle.Fill;
                    tabDateTimePanel.Controls.Add(dtpfrom, 2, 0);
                    tabDateTimePanel.Controls.Add(dtpto, 5, 0);
                    tabDateTimePanel.Controls.Add(label6, 1, 0);
                    tabDateTimePanel.Controls.Add(label5, 4, 0);
                    tabDateTimePanel.Controls.Add(dtp_from_time, 3, 0);
                    tabDateTimePanel.Controls.Add(dtp_to_time, 6, 0);
                }
                else
                {
                    if (Convert.ToInt32(cmbreport.SelectedValue) == 36)
                    {
                        if (chk_time.Checked == true)
                        {
                            dtpfrom.Dock = DockStyle.Fill;
                            dtpto.Dock = DockStyle.Fill;
                            label6.Dock = DockStyle.Fill;
                            label5.Dock = DockStyle.Fill;
                            dtp_from_time.Dock = DockStyle.Fill;
                            dtp_to_time.Dock = DockStyle.Fill;
                            tabDateTimePanel.Controls.Add(dtpfrom, 2, 0);
                            tabDateTimePanel.Controls.Add(dtpto, 5, 0);
                            tabDateTimePanel.Controls.Add(label6, 1, 0);
                            tabDateTimePanel.Controls.Add(label5, 4, 0);
                            tabDateTimePanel.Controls.Add(dtp_from_time, 3, 0);
                            tabDateTimePanel.Controls.Add(dtp_to_time, 6, 0);
                            //btn_search_second.Visible = false;
                        }
                        else
                        {
                            controlToRemove = tabDateTimePanel.GetControlFromPosition(3, 0);
                            tabDateTimePanel.Controls.Remove(controlToRemove);
                            controlToRemove1 = tabDateTimePanel.GetControlFromPosition(6, 0);
                            tabDateTimePanel.Controls.Remove(controlToRemove1);

                            dtpfrom.Dock = DockStyle.Fill;
                            dtpto.Dock = DockStyle.Fill;
                            label6.Dock = DockStyle.Fill;
                            label5.Dock = DockStyle.Fill;
                            dtp_from_time.Dock = DockStyle.Fill;
                            dtp_to_time.Dock = DockStyle.Fill;

                            tabDateTimePanel.Controls.Add(dtpfrom, 2, 0);
                            tabDateTimePanel.Controls.Add(dtpto, 5, 0);

                            tabDateTimePanel.Controls.Add(chk_time, 0, 0);
                            tabDateTimePanel.Controls.Add(label6, 1, 0);
                            tabDateTimePanel.Controls.Add(label5, 4, 0);
                        }

                    }
                    else
                    {
                        controlToRemove = tabDateTimePanel.GetControlFromPosition(3, 0);
                        tabDateTimePanel.Controls.Remove(controlToRemove);
                        controlToRemove1 = tabDateTimePanel.GetControlFromPosition(6, 0);
                        tabDateTimePanel.Controls.Remove(controlToRemove1);

                        dtpfrom.Dock = DockStyle.Fill;
                        dtpto.Dock = DockStyle.Fill;
                        label6.Dock = DockStyle.Fill;
                        label5.Dock = DockStyle.Fill;
                        dtp_from_time.Dock = DockStyle.Fill;
                        dtp_to_time.Dock = DockStyle.Fill;

                        tabDateTimePanel.Controls.Add(dtpfrom, 2, 0);
                        tabDateTimePanel.Controls.Add(dtpto, 5, 0);

                        tabDateTimePanel.Controls.Add(chk_time, 0, 0);
                        tabDateTimePanel.Controls.Add(label6, 1, 0);
                        tabDateTimePanel.Controls.Add(label5, 4, 0);

                        tabDateTimePanel.Controls.Add(dtp_from_time, 2, 1);
                        tabDateTimePanel.Controls.Add(dtp_to_time, 5, 1);
                    }
                }
                dtp_from_time.Visible = false;
                dtp_to_time.Visible = false;
                tabDateTimePanel.PerformLayout();
            }

        }

        private void btnserach_Click(object sender, EventArgs e)
        {
            try
            {
                if (chk_time.Checked == true)
                {
                    fTime = Convert.ToDateTime(dtpfrom.Value.ToString(Dtformat) + " " + dtp_from_time.Value.ToString(Timeformat));
                    tTime = Convert.ToDateTime(dtpto.Value.ToString(Dtformat) + " " + dtp_to_time.Value.ToString(Timeformat));
                }
                else
                {
                    string f = dtpfrom.Value.ToString(Dtformat);
                    string t = dtpto.Value.ToString(Dtformat); 
                    fTime = Convert.ToDateTime(dtpfrom.Value.ToString(Dtformat) + " 00:00:00");
                    tTime = Convert.ToDateTime(t + " 23:59:59");
                }

                switch (Convert.ToInt32(cmbreport.SelectedValue))
                {
                    //case 2:
                    //    s_FillReport_RechargeList();
                    //    break;
                    //case 4:
                    //    //s_FillReport_DailyCollection();
                    //    break;
                    //case 5:
                    //    s_FillReport_TopPlayedGame();
                    //    break;
                    //case 6:
                    //    s_FillReport_TopPlayer();
                    //    break;
                    //case 7:
                    //    s_FillReport_NewRegCus();
                    //    break;
                    //case 9:
                    //    s_FillReport_HappyHoures();
                    //    break;
                    case 10:
                        s_FillReport_Offerlist();
                        break;
                    case 11:
                        s_FillReport_EmployeeList();
                        break;
                    //case 14:
                    //    s_FillReport_DetailRechargeReport();
                    //    break;
                    //case 15:
                    //    s_FillReport_GameList();
                    //    break;
                    //case 16:
                    //    //s_FillReport_CardHistory();
                    //    break;
                    //case 17:
                    //    s_FillReport_CashRevenue();
                    //    break;
                    //case 18:
                    //    s_FillReport_GameRevenue();
                    //    break;
                    //case 19:
                    //    s_FillReport_Redumption();
                    //    break;
                    case 20:
                        s_FillReport_Purchase_Return();
                        break;
                    //case 22:
                    //    s_FillReport_consolidate();
                    //    break;
                    case 23:
                        s_FillReport_Purchase();
                        break;
                    //case 24:
                    //    s_FillReport_Preorder();
                    //    break;
                    //case 25:
                    //    s_FillReport_Complement();
                    //    break;
                    //case 26:
                    //    s_FillReport_PointBalance();
                    //    break;
                    case 27:
                        if (tlp_sublist.Visible)
                            s_FillReport_Inventory(Convert.ToInt32(cmbSublist.SelectedIndex));
                        break;
                    //case 28:
                    //    s_FillReport_CardVoid();
                    //    break;
                    //case 29:
                    //    s_FillReport_SaleVoid();
                    //    break;
                    //case 30:
                    //    s_FillReport_Salesreturns();
                    //    break;
                    //case 31:
                    //    s_FillReport_Clearcard();
                    //    break;
                    //case 32:
                    //    s_FillReport_CardTransfer();
                    //    break;
                    //case 33:
                    //    s_FillReport_FamilyCard();
                    //    break;
                    //case 34:
                    //    s_FillReport_Group_Sale();
                    //    break;
                    //case 35:
                    //    s_FillReport_RefundCard();
                    //    break;
                    //case 36:
                    //    s_FillReport_ThridPartyCardTransfer();
                    //    break;
                    //case 37:
                    //    s_FillReport_CardConsolidation();
                    //    break;
                    //case 38:
                    //    s_FillReport_AnuvalGameRevenue();
                    //    break;
                    //case 39:
                    //    s_FillReport_CardSummary();
                    //    break;
                    //case 40:
                    //    s_FillReport_CardLiability();
                    //    break;
                    //case 41:
                    //    s_FillReport_CardSummaryInDetailed();
                    //    break;
                    //case 42:
                    //    s_FillReport_CustomerTraffic();
                    //    break;
                    //case 43:
                    //    s_FillReport_memberactivity();
                    //    break;
                    //case 44:
                    //    s_FillReport_CreditCardActivity();
                    //    break;
                    //case 45:
                    //    s_FillReport_saleComplementry();
                    //    break;
                    //case 46:
                    //    s_FillReport_saleComplementryDetails();
                    //    break;
                    case 47:
                           s_FillReport_saleReport();
                           break;
                    case 48:
                        s_FillReport_saleDetailsReport();
                        break;
                        //case 49:
                        //    s_FillReport_RedemptionDamagedProducts();
                        //    break;
                        //case 50:
                        //    s_FillReport_Inventory(Convert.ToInt32(cmbreport.SelectedValue));
                        //    break;
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void s_FillReport_saleReport()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dt_Rpt = new DataTable("Rpt_DT");
                BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
                DmnSearch dmnSearchDetBill = new DmnSearch();
                dmnSearchDetBill.Select = "(select CONVERT(varchar(50), s.CreatedDateTime, 105)) as sFld1,EntryNo as sFld2,CusName  as sFld3,Telephone  as sFld4,TotalQuantity  as dFld1,TotalTax  as dFld2,DiscountAmount  as dFld3,NetAmount  as dFld4";/*,fcm. as dFld2,fcm.Tax as dFld3*/
                dmnSearchDetBill.Table = "tblSalesMaster s";
                dmnSearchDetBill.WhereNdGroupBy = "where  s.CreatedDateTime >= '" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "'   and  s.CreatedDateTime<= '" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and s.BranchId = " + BranchId;
                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);
                if (invoiceBillMasters.Count > 0)
                {
                    invoiceBillMasters[0].FromDate = Convert.ToDateTime(dtpfrom.Text);
                    invoiceBillMasters[0].ToDate = Convert.ToDateTime(dtpto.Text);
                }
                using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "sFld3", "sFld4", "dFld1", "dFld2", "dFld3", "dFld4"))
                {
                    dt_Rpt.Load(reader);
                }
                dsReport.Tables.Add(dt_Rpt);
                reportDocument.Load(Path + @"\Rpt_SaleReport.rpt");
                reportDocument.SetDataSource(dsReport);

                /* header section*/
                Addressdata = Get_CompantDetails();
                reportDocument.SetParameterValue("Loc_Name", Convert.ToString(Addressdata[0].Name));
                reportDocument.SetParameterValue("City", Convert.ToString(Addressdata[0].City));
                reportDocument.SetParameterValue("Employe_Name", Convert.ToString(FrmLogin.EmployeName));
                reportDocument.SetParameterValue("GSTno", Convert.ToString(Addressdata[0].GstNo));
                string devicename = System.Environment.MachineName;
                reportDocument.SetParameterValue("Device_name", devicename);
                /*End Header section*/

                reportDocument.SetParameterValue("MyFromDtm", dtpfrom.Value.ToString(Dtformat));
                reportDocument.SetParameterValue("MyToDatem", dtpto.Value.ToString(Dtformat));

                reportDocument.SetParameterValue("From_re_time", dtp_from_time.Value.ToString(Timeformat));
                reportDocument.SetParameterValue("To_re_time", dtp_to_time.Value.ToString(Timeformat));
                if (ColorChk == true)
                {
                    FindAndSetColor(reportDocument, "Box2", selectedColor);
                    FindAndSetColor(reportDocument, "Box3", selectedColor);
                }
                crv_report.ReuseParameterValuesOnRefresh = true;
                crv_report.RefreshReport();
                crv_report.ReportSource = reportDocument;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public void s_FillReport_saleDetailsReport()
        {

            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dt_Rpt = new DataTable("Rpt_DT");
                BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
                DmnSearch dmnSearchDetBill = new DmnSearch();
                dmnSearchDetBill.Select = "(select CONVERT(varchar(50), sm.CreatedDateTime, 105)) as sFld1,sm.EntryNo as sFld2,sm.CusName  as sFld3,sm.Telephone  as sFld4,sm.TotalQuantity  as dFld1,sm.TotalTax  as dFld2,sm.DiscountAmount  as dFld3,sm.NetAmount  as dFld4,sd.Itemname as sFld5,sd.Quantity as nFld1,sd.SalesRate as dFld5,sd.Amount as dFld6,sd.TaxAmount as dFld7,sd.DiscountAmount as dFld8,sd.NetAmount as dFld9";
                dmnSearchDetBill.Table = "tblSalesDetails as sd LEFT JOIN tblSalesMaster sm on sm.SalesMasterId =sd.SalesMasterId";
                dmnSearchDetBill.WhereNdGroupBy = "where sm.CreatedDateTime>= '" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "'   and sm.CreatedDateTime<= '" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and sm.BranchId = " + BranchId;
                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);
                if (invoiceBillMasters.Count > 0)
                {

                    invoiceBillMasters[0].FromDate = Convert.ToDateTime(dtpfrom.Text);
                    invoiceBillMasters[0].ToDate = Convert.ToDateTime(dtpto.Text);
                }
                using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "sFld3", "sFld4", "sFld5", "dFld1", "dFld2", "dFld3", "dFld4", "dFld5", "dFld6", "dFld7", "dFld8", "dFld9", "nFld1"))
                {
                    dt_Rpt.Load(reader);
                }
                dsReport.Tables.Add(dt_Rpt);
                reportDocument.Load(Path + @"\Rpt_SaleDetailsReport.rpt");
                reportDocument.SetDataSource(dsReport);

                /* header section*/
                Addressdata = Get_CompantDetails();
                reportDocument.SetParameterValue("Loc_Name", Convert.ToString(Addressdata[0].Name));
                reportDocument.SetParameterValue("City", Convert.ToString(Addressdata[0].City));
                reportDocument.SetParameterValue("Employe_Name", Convert.ToString(FrmLogin.EmployeName));
                reportDocument.SetParameterValue("GSTno", Convert.ToString(Addressdata[0].GstNo));
                string devicename = System.Environment.MachineName;
                reportDocument.SetParameterValue("Device_name", devicename);
                /*End Header section*/

                reportDocument.SetParameterValue("MyFromDtm", dtpfrom.Value.ToString(Dtformat));
                reportDocument.SetParameterValue("MyToDatem", dtpto.Value.ToString(Dtformat));

                reportDocument.SetParameterValue("From_re_time", dtp_from_time.Value.ToString(Timeformat));
                reportDocument.SetParameterValue("To_re_time", dtp_to_time.Value.ToString(Timeformat));

                if (ColorChk == true)
                {
                    FindAndSetColor(reportDocument, "Box2", selectedColor);
                    FindAndSetColor(reportDocument, "Box3", selectedColor);
                }

                crv_report.ReuseParameterValuesOnRefresh = true;
                crv_report.RefreshReport();
                crv_report.ReportSource = reportDocument;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnpickcolor_Click(object sender, EventArgs e)
        {
            ColorDialog cd_headingcolor = new ColorDialog();
            DialogResult result = cd_headingcolor.ShowDialog();
            if (result == DialogResult.OK)
            {
                ColorChk = true;
                selectedColor = cd_headingcolor.Color;
                btnpickcolor.BackColor = selectedColor;
                //FindAndSetColor
            }
            else
                ColorChk = false;
        }

        private void chk_finyear_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_finyear.Checked)
            {
                tlp_finyear.Visible = true;
                tabDateTimePanel.Visible = false;
            }
            else
            {
                tlp_finyear.Visible = false;
                tabDateTimePanel.Visible = true;
            }


        }

        private void btnWeekEnd_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            //currentDate=Convert.ToDateTime("2023-05-28 10:40:00");
            chk_time.Checked = false;
            int d = Convert.ToInt32(-(int)currentDate.DayOfWeek);
            dtpfrom.Value = currentDate.AddDays(-(int)currentDate.DayOfWeek - 1); // Start date of the current week
            dtpto.Value = currentDate.AddDays(-(int)currentDate.DayOfWeek); // End date of the current week
            btnserach_Click(sender, e);
        }
        private void btnWeekly_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            //currentDate=Convert.ToDateTime("2023-05-28 10:40:00");
            chk_time.Checked = false;
            dtpfrom.Value = currentDate.AddDays(-(int)currentDate.DayOfWeek);
            dtpto.Value = dtpfrom.Value.AddDays(6);
            btnserach_Click(sender, e);
        }
        private void btnMonthly_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            dtpfrom.Value = new DateTime(currentDate.Year, currentDate.Month, 1);
            dtpto.Value = dtpfrom.Value.AddMonths(1).AddDays(-1);
            chk_time.Checked = false;
            btnserach_Click(sender, e);
        }
        private void btnYealy_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;
            dtpfrom.Value = new DateTime(currentDate.Year, 1, 1);
            dtpto.Value = new DateTime(currentDate.Year, 12, 31);
            chk_time.Checked = false;
            btnserach_Click(sender, e);
        }
        public void s_FillReport_Purchase_Return()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dt_Rpt = new DataTable("Rpt_DT");
                BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
                DmnSearch dmnSearchDetBill = new DmnSearch();
                dmnSearchDetBill.Select = "prd.ItemBarCode as sFld1,prd.ItemCode as sFld2,v.VendorName as sFld3,pm.EntryNo as sFld4,(select CONVERT(varchar(50), pm.EntryDate, 105)) as sFld5,h.Name as sFld6,h.Email as sFld7,h.Phone as sFld8,h.Address as sFld9,(select CONVERT(varchar(50),prm.EntryDate, 105)) as sFld10,pd.Quantity as nFld1,prd.Quantity as nFld2,prd.currentQuantity as nFld3,prd.PurchaseRate as dFld1,prd.Amount as dFld2,prd.DiscountAmount as dFld3,prd.TaxPer as dFld4,prd.TaxAmount as dFld5";
                dmnSearchDetBill.Table = " tblPurchaseReturnMaster prm left join tblPurchaseReturnDetails prd on prd.PurchaseReturnMasterId = prm.PurchaseReturnMasterId left join tblVendor v on v.VendorId = prm.LedgerId left join tblPurchaseMaster pm on pm.PurchaseMasterId =prm.PurchaseMasterId left join tblPurchaseDetails pd on pd.PurchaseDetailId =prd.PurchaseDetailId left join tblHotelRegistration h on h.BranchId=pm.BranchId";
                dmnSearchDetBill.WhereNdGroupBy = "where prm.EntryDate>='" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and prm.EntryDate <='" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and  prm.BranchId = " + BranchId;
                dmnSearchDetBill.OrderBy = "Order By prd.PurchaseReturnDetailsId";
                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);


                if (invoiceBillMasters.Count > 0)
                {
                    invoiceBillMasters[0].FromDate = Convert.ToDateTime(dtpfrom.Text);
                    invoiceBillMasters[0].ToDate = Convert.ToDateTime(dtpto.Text);
                }

                using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "sFld3", "sFld4", "sFld5", "sFld6", "sFld7", "sFld8", "sFld9", "sFld10", "nFld1", "nFld2", "nFld3", "dFld1", "dFld2", "dFld3", "dFld4", "dFld5"))
                {
                    dt_Rpt.Load(reader);

                }
                dsReport.Tables.Add(dt_Rpt);
                reportDocument.Load(Path + @"\Rpt_Purchase_Return.rpt");
                reportDocument.SetDataSource(dsReport);


                /* header section*/
                Addressdata = Get_CompantDetails();
                reportDocument.SetParameterValue("Loc_Name", Convert.ToString(Addressdata[0].Name));
                reportDocument.SetParameterValue("City", Convert.ToString(Addressdata[0].City));
                reportDocument.SetParameterValue("Employe_Name", Convert.ToString(FrmLogin.EmployeName));
                reportDocument.SetParameterValue("GSTno", Convert.ToString(Addressdata[0].GstNo));
                string devicename = System.Environment.MachineName;
                reportDocument.SetParameterValue("Device_name", devicename);
                /*End Header section*/

                reportDocument.SetParameterValue("MyFromDtm", dtpfrom.Value.ToString(Dtformat));
                reportDocument.SetParameterValue("MyToDatem", dtpto.Value.ToString(Dtformat));

                reportDocument.SetParameterValue("From_re_time", dtp_from_time.Value.ToString(Timeformat));
                reportDocument.SetParameterValue("To_re_time", dtp_to_time.Value.ToString(Timeformat));
                if (ColorChk == true)
                {
                    FindAndSetColor(reportDocument, "Box2", selectedColor);
                    FindAndSetColor(reportDocument, "Box3", selectedColor);
                }
                crv_report.ReuseParameterValuesOnRefresh = true;
                crv_report.RefreshReport();
                crv_report.ReportSource = reportDocument;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public void s_FillReport_Purchase()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dt_Rpt = new DataTable("Rpt_DT");
                BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
                DmnSearch dmnSearchDetBill = new DmnSearch();
                dmnSearchDetBill.Select = "pd.ItemBarCode as sFld1,pd.itemName as sFld2,v.VendorName as sFld3,pm.EntryNo as sFld4,(select CONVERT(varchar(50), pm.EntryDate, 105)) as sFld5,h.Name as sFld6,h.Email as sFld7,h.Phone as sFld8,h.Address as sFld9,pd.Quantity as nFld1,pd.CurrentQuantity as nFld2,pd.PurchaseRate as dFld1,pd.Srate as dFld2,pd.MRP as dFld3,pd.Amount as dFld4,pd.DiscountAmount as dFld5,pd.TaxAmount as dFld6";
                dmnSearchDetBill.Table = " tblPurchaseMaster pm left join tblVendor v on v.VendorId = pm.LedgerId left join tblPurchaseDetails pd on pd.PurchaseMasterId =pm.PurchaseMasterId left join tblHotelRegistration h on h.BranchId=pm.BranchId";
                dmnSearchDetBill.WhereNdGroupBy = "where pm.EntryDate>='" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and" +
                    " pm.EntryDate<='" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and  pm.BranchId = " + BranchId;

                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);


                if (invoiceBillMasters.Count > 0)
                {
                    invoiceBillMasters[0].FromDate = Convert.ToDateTime(dtpfrom.Text);
                    invoiceBillMasters[0].ToDate = Convert.ToDateTime(dtpto.Text);
                }
                using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "sFld3", "sFld4", "sFld5", "sFld6", "sFld7", "sFld8", "sFld9", "nFld1", "nFld2", "dFld1", "dFld2", "dFld3", "dFld4", "dFld5", "dFld6"))
                {
                    dt_Rpt.Load(reader);
                }
                dsReport.Tables.Add(dt_Rpt);
                reportDocument.Load(Path + @"\Rpt_Purchase.rpt");
                reportDocument.SetDataSource(dsReport);
                /* header section*/
                Addressdata = Get_CompantDetails();
                reportDocument.SetParameterValue("Loc_Name", Convert.ToString(Addressdata[0].Name));
                reportDocument.SetParameterValue("City", Convert.ToString(Addressdata[0].City));
                reportDocument.SetParameterValue("Employe_Name", Convert.ToString(FrmLogin.EmployeName));
                reportDocument.SetParameterValue("GSTno", Convert.ToString(Addressdata[0].GstNo));
                string devicename = System.Environment.MachineName;
                reportDocument.SetParameterValue("Device_name", devicename);
                /*End Header section*/

                reportDocument.SetParameterValue("MyFromDtm", dtpfrom.Value.ToString(Dtformat));
                reportDocument.SetParameterValue("MyToDatem", dtpto.Value.ToString(Dtformat));

                reportDocument.SetParameterValue("From_re_time", dtp_from_time.Value.ToString(Timeformat));
                reportDocument.SetParameterValue("To_re_time", dtp_to_time.Value.ToString(Timeformat));
                if (ColorChk == true)
                {
                    FindAndSetColor(reportDocument, "Box2", selectedColor);
                    FindAndSetColor(reportDocument, "Box3", selectedColor);
                }
                crv_report.ReuseParameterValuesOnRefresh = true;
                crv_report.RefreshReport();
                crv_report.ReportSource = reportDocument;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private List<DmnHRegistration> Get_CompantDetails()
        {
            BllSearch<DmnHRegistration> bllSearchcompany = new BllSearch<DmnHRegistration>();
            DmnSearch dmnSearchcompany = new DmnSearch();
            dmnSearchcompany.Select = "Name,Email,Phone,Address,city,GSTNO";
            dmnSearchcompany.Table = "tblHotelRegistration";
            dmnSearchcompany.WhereNdGroupBy = " where  BranchId = " + BranchId;
            var Addressdata = bllSearchcompany.GetSearchData("prdSearch", dmnSearchcompany);
            return Addressdata;
        }
        private void FindAndSetColor(ReportDocument report, string objectName, Color color)
        {
            foreach (Section section in report.ReportDefinition.Sections)
            {
                foreach (ReportObject reportObject in section.ReportObjects)
                {
                    if (reportObject.Kind == ReportObjectKind.BoxObject && reportObject.Name == objectName)
                    {
                        BoxObject boxObject = (BoxObject)reportObject;
                        boxObject.FillColor = color;
                        break;
                    }
                }
            }
        }

        private void cmbreport_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                tlp_sublist.Visible = false;
                if (Convert.ToInt32(cmbreport.SelectedValue) == 27)
                {
                    cmbSublist.DataSource = null;
                    tlp_sublist.Visible = true;
                    List<string> sublistOptions = new List<string> { "All", "Product", "Item", "Kitchen Item" };
                    cmbSublist.DataSource = sublistOptions;
                    cmbSublist.SelectedIndex = 0;
                }
                else if (Convert.ToInt32(cmbreport.SelectedValue) == 10)
                {
                    cmbSublist.DataSource = null;
                    tlp_sublist.Visible = true;
                    List<string> sublistOptions = new List<string> { "Product Offer", "Combo Offer" };
                    cmbSublist.DataSource = sublistOptions;
                    cmbSublist.SelectedIndex = 0;
                }
            }
            catch(Exception ex)
            {

            }


        }
        public void s_FillReport_Inventory(int typeid)
        {
            string Inventry_Name = "";
            ReportDocument reportDocument = new ReportDocument();
            DataSet dsReport = new DataSet();
            DataTable dt_Rpt = new DataTable("Rpt_DT");
            BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
            DmnSearch dmnSearchDetBill = new DmnSearch();
            if (cmbSublist.SelectedIndex == 0)
            {
                dmnSearchDetBill.Select = "p.itemcode as sFld1,p.Product as sFld2,m.Quantity as sFld3,p.Price as dFld1,0 as dFld2,p.MRP  as dFld3 ";
                dmnSearchDetBill.Table = "tblproduct p left join tblMenu m on m.ProductId=p.ProductId where p.Status = 1 and  m.Quantity > 0 " +
                    "union select i.ItemCode_UPC as sFld1,i.Itemname as sFld2,pr.QOH as sFld3,pr.PRate as dFld2,pr.SRate as dFld1,pr.MRP  as dFld3 " +
                    "from tblItem i left join tblProductRate pr on pr.ProductId=i.ItemId";
                dmnSearchDetBill.WhereNdGroupBy = " where i.Status = 1 and (select Sum(QOH) from tblProductRate where ProductId=i.ItemId) >0";
                dmnSearchDetBill.OrderBy = "";
                Inventry_Name = "F&B Inventory Report";
            }
            else if (cmbSublist.SelectedIndex == 1)
            {
                dmnSearchDetBill.Select = "p.itemcode as sFld1,p.product as sFld2,m.Quantity as sFld3,p.Price as dFld1,0 as dFld2,p.MRP  as dFld3 ";
                dmnSearchDetBill.Table = "tblproduct p left join tblMenu m on m.ProductId=p.ProductId ";
                dmnSearchDetBill.WhereNdGroupBy = "where p.Status = 1 and  m.Quantity > 0";
                dmnSearchDetBill.OrderBy = "";
                Inventry_Name = "Product Inventory Report";
            }
            else if(cmbSublist.SelectedIndex == 2)
            {
                dmnSearchDetBill.Select = "i.ItemCode_UPC as sFld1,i.Itemname as sFld2,pr.QOH as sFld3,pr.PRate as dFld1,pr.SRate as dFld2,pr.MRP  as dFld3";
                dmnSearchDetBill.Table = " tblItem i left join tblProductRate pr on pr.ProductId=i.ItemId";
                dmnSearchDetBill.WhereNdGroupBy = " where i.Status = 1 and  (select Sum(QOH) from tblProductRate where ProductId=i.ItemId) >0 and saletype=1";
                dmnSearchDetBill.OrderBy = "";
                Inventry_Name = "Item Inventory Report";
            }
             else
            {
                dmnSearchDetBill.Select = "i.ItemCode_UPC as sFld1,i.Itemname as sFld2,pr.QOH as sFld3,pr.PRate as dFld1,pr.SRate as dFld2,pr.MRP  as dFld3";
                dmnSearchDetBill.Table = " tblItem i left join tblProductRate pr on pr.ProductId=i.ItemId";
                dmnSearchDetBill.WhereNdGroupBy = " where i.Status = 1 and  (select Sum(QOH) from tblProductRate where ProductId=i.ItemId) >0 and saletype=0";
                dmnSearchDetBill.OrderBy = "";
                Inventry_Name = "Kitchen Item Inventory Report";
            }

            var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);
            using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "sFld3", "dFld1", "dFld2", "dFld3"))
            {
                dt_Rpt.Load(reader);
            }
            dsReport.Tables.Add(dt_Rpt);
            reportDocument.Load(Path + @"\Rpt_Inventory.rpt");
            reportDocument.SetDataSource(dsReport);

            /* header section*/
            Addressdata = Get_CompantDetails();
            reportDocument.SetParameterValue("Loc_Name", Convert.ToString(Addressdata[0].Name));
            reportDocument.SetParameterValue("City", Convert.ToString(Addressdata[0].City));
            reportDocument.SetParameterValue("Employe_Name", Convert.ToString(FrmLogin.EmployeName));
            reportDocument.SetParameterValue("GSTno", Convert.ToString(Addressdata[0].GstNo));
            string devicename = System.Environment.MachineName;
            reportDocument.SetParameterValue("Device_name", devicename);
            reportDocument.SetParameterValue("inventory_heading", Inventry_Name);
            /*End Header section*/

            if (ColorChk == true)
            {
                FindAndSetColor(reportDocument, "Box2", selectedColor);
                FindAndSetColor(reportDocument, "Box3", selectedColor);
            }
            crv_report.ReuseParameterValuesOnRefresh = true;
            crv_report.RefreshReport();
            crv_report.ReportSource = reportDocument;
        }
        public void s_FillReport_Offerlist()
        {
            try
            {

                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dt_Rpt = new DataTable("Rpt_DT");
                BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
                DmnSearch dmnSearchDetBill = new DmnSearch();
                dmnSearchDetBill.Select = " ofm.OfferName as sFld1,ofm.Discountper as dFld1,(select CONVERT(varchar(50),ofm.OfferstartDatetime,105)) as sFld2,(select CONVERT(varchar(50),ofm.OfferEndDatetime,105)) as sFld3,ofd.ItemCode_UPC sFld4,ofd.Productname sFld5,ofd.Price dFld2,ofd.FinalPrice dFld3";
                dmnSearchDetBill.Table = "tblOfferMaster ofm left join tblOfferDetails ofd on ofd.Omid=ofm.Omid and ofd.status = 1 ";
                dmnSearchDetBill.WhereNdGroupBy = " where ofm.OfferstartDatetime>='" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and ofm.OfferEndDatetime<='" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                                 "and ofm.status = 1";
                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);



                if (invoiceBillMasters.Count > 0)
                {
                    invoiceBillMasters[0].FromDate = Convert.ToDateTime(dtpfrom.Text);
                    invoiceBillMasters[0].ToDate = Convert.ToDateTime(dtpto.Text);
                }
                using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "sFld3", "sFld4", "sFld5", "dFld1", "dFld2"))
                {
                    dt_Rpt.Load(reader);
                }
                dsReport.Tables.Add(dt_Rpt);
                reportDocument.Load(Path + @"\Rpt_OfferList.rpt");
                reportDocument.SetDataSource(dsReport);



                /* header section*/
                Addressdata = Get_CompantDetails();
                reportDocument.SetParameterValue("Loc_Name", Convert.ToString(Addressdata[0].Name));
                reportDocument.SetParameterValue("City", Convert.ToString(Addressdata[0].City));
                reportDocument.SetParameterValue("Employe_Name", Convert.ToString(FrmLogin.EmployeName));
                reportDocument.SetParameterValue("GSTno", Convert.ToString(Addressdata[0].GstNo));
                string devicename = System.Environment.MachineName;
                reportDocument.SetParameterValue("Device_name", devicename);
                /*End Header section*/

                reportDocument.SetParameterValue("MyFromDtm", dtpfrom.Value.ToString(Dtformat));
                reportDocument.SetParameterValue("MyToDatem", dtpto.Value.ToString(Dtformat));

                reportDocument.SetParameterValue("From_re_time", dtp_from_time.Value.ToString(Timeformat));
                reportDocument.SetParameterValue("To_re_time", dtp_to_time.Value.ToString(Timeformat));
                if (ColorChk == true)
                {
                    FindAndSetColor(reportDocument, "Box2", selectedColor);
                    FindAndSetColor(reportDocument, "Box3", selectedColor);
                }
                crv_report.ReuseParameterValuesOnRefresh = true;
                crv_report.RefreshReport();
                crv_report.ReportSource = reportDocument;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        public void s_FillReport_EmployeeList()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dt_Rpt = new DataTable("Rpt_DT");
                BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
                DmnSearch dmnSearchDetBill = new DmnSearch();
                dmnSearchDetBill.Select = "EmployeeCode as sFld1,EmployeeName as sFld2,de.DesignationName as sFld3,d.DepartmentName as sFld4,e.Mobile as sFld5,(select CONVERT(varchar(50),e.CreatedDateTime,105)) as sFld6 ";
                dmnSearchDetBill.Table = " tblEmployee e left join tblDepartment d on e.DepartmentId=d.DepartmentId" +
                                         " left join tblDesignation de on e.DesignationId = de.DesignationId";
                dmnSearchDetBill.WhereNdGroupBy = " where e.Status=1";
                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);

                if (invoiceBillMasters.Count > 0)
                {
                    invoiceBillMasters[0].FromDate = Convert.ToDateTime(dtpfrom.Text);
                    invoiceBillMasters[0].ToDate = Convert.ToDateTime(dtpto.Text);

                }
                using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "dFld1", "dFld2", "dFld3", "sFld3", "sFld4", "sFld5", "sFld6"))
                {
                    dt_Rpt.Load(reader);
                }
                dsReport.Tables.Add(dt_Rpt);
                reportDocument.Load(Path + @"\Rpt_employeeList.rpt");
                reportDocument.SetDataSource(dsReport);

                /* header section*/
                Addressdata = Get_CompantDetails();
                reportDocument.SetParameterValue("Loc_Name", Convert.ToString(Addressdata[0].Name));
                reportDocument.SetParameterValue("City", Convert.ToString(Addressdata[0].City));
                reportDocument.SetParameterValue("Employe_Name", Convert.ToString(FrmLogin.EmployeName));
                reportDocument.SetParameterValue("GSTno", Convert.ToString(Addressdata[0].GstNo));
                string devicename = System.Environment.MachineName;
                reportDocument.SetParameterValue("Device_name", devicename);
                /*End Header section*/

                reportDocument.SetParameterValue("MyFromDtm", dtpfrom.Value.ToString(Dtformat));
                reportDocument.SetParameterValue("MyToDatem", dtpto.Value.ToString(Dtformat));

                reportDocument.SetParameterValue("From_re_time", dtp_from_time.Value.ToString(Timeformat));
                reportDocument.SetParameterValue("To_re_time", dtp_to_time.Value.ToString(Timeformat));
                if (ColorChk == true)
                {
                    FindAndSetColor(reportDocument, "Box2", selectedColor);
                    FindAndSetColor(reportDocument, "Box3", selectedColor);
                }
                crv_report.ReuseParameterValuesOnRefresh = true;
                crv_report.RefreshReport();
                crv_report.ReportSource = reportDocument;

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
        public void combofill_report()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = " G.GTypeSubId as Id, G.Caption as Value";
            objSearch.Table = " tblGeneralOptions G";
            objSearch.WhereNdGroupBy = " WHERE G.GTypeId=" + 51;
            objSearch.OrderBy = "order by Id";
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);

            cmbreport.DataSource = gridSource;
            cmbreport.ValueMember = "Id";
            cmbreport.DisplayMember = "value";

        }


    }
}
