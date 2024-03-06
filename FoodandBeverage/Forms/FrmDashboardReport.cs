using BLL;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FoodandBeverage.Forms
{
    public partial class FrmDashboardReport : Form
    {
        public static int BranchId = 0;
        public static int UserId = 0;
        public static int UserType = 0;
        public static int Allaccess = 0;
        public int Reportid = 0;
       public FrmDashboardReport()
        {
            InitializeComponent();
            UserId = Convert.ToInt32(FrmLogin.UserId);
            BranchId = Convert.ToInt32(FrmLogin.BranchId);
            UserType = Convert.ToInt32(FrmLogin.UserAccountTypeId);
        }

        #region Declaration


        #endregion
        private void FrmDashboardReport_Load(object sender, EventArgs e)
        {
            Filldashboard();

            SetupChartTwoBar();
            FillChartTwoBarChart();

            Fill_combo_finyear();
            SetupChart_Revenue();
            FillbarRrevenuChart();

            SetupCharttotaldiscount();
            FillCharttotaldiscountChart();

            SetupChartExpence();
            FillChartExpence();

            Fillpaymentdetailed();

            FillBoxdetailed();

        }
        #region ,ODEL BOX DETAILS ACTIVE,CANCELLED,PROMOTION,FOOD ITEM
        public void FillBoxdetailed()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "  ISNULL([CASH],0.00) Field1, ISNULL([CREDIT CARD],0.00) Field2, ISNULL([UPI],0.00) Field3,ISNULL([DEBIT CARD],0.00) Field4,ISNULL([BANKING ONLINE],0.00) Field5, ISNULL([OTHERS],0.00) Field6,ISNULL([PART],0.00) Field7, ISNULL([SPLIT],0.00) Field8    FROM ( SELECT sm.netamount,g.Caption ";
                dmnSearch.Table = " tblSalesMaster sm LEFT JOIN tblGeneralOptions g ON g.GTypeSubId = sm.PaymentMode  where  sm.Status=1 AND CONVERT(DATE, sm.EntryDate) = CONVERT(DATE, '" + dtp_dashboard.Value.ToString("yyyy-MM-dd") + "')) AS SourceTable" +
                                    " PIVOT( SUM(netamount) FOR Caption IN([CASH], [CREDIT CARD],[UPI], [DEBIT CARD], [BANKING ONLINE], [OTHERS], [PART], [SPLIT]) " +
                                    ") AS PivotTable";
                dmnSearch.WhereNdGroupBy = " ";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    ibcash.Text = Convert.ToString(gridSource[0].Field1);
                    ibcreditcard.Text = Convert.ToString(gridSource[0].Field2);
                    ibupi.Text = Convert.ToString(gridSource[0].Field3);
                    ibrupay.Text = Convert.ToString(gridSource[0].Field4);
                    ibnetbanking.Text = Convert.ToString(gridSource[0].Field5);
                    ibothers.Text = Convert.ToString(gridSource[0].Field6);
                    ibpart.Text = Convert.ToString(gridSource[0].Field7);
                    ibsplit.Text = Convert.ToString(gridSource[0].Field8);
                    decimal sumtotal = Convert.ToDecimal(gridSource[0].Field1) + Convert.ToDecimal(gridSource[0].Field2) + Convert.ToDecimal(gridSource[0].Field3) +
                                       Convert.ToDecimal(gridSource[0].Field4) + Convert.ToDecimal(gridSource[0].Field5) + Convert.ToDecimal(gridSource[0].Field6) +
                                       Convert.ToDecimal(gridSource[0].Field7) + Convert.ToDecimal(gridSource[0].Field8);
                    ibtotalsumpayment.Text = Convert.ToString(sumtotal);
                }
                else
                {
                    ibcash.Text = "0.00";
                    ibcreditcard.Text = "0.00";
                    ibupi.Text = "0.00";
                    ibrupay.Text = "0.00";
                    ibnetbanking.Text = "0.00";
                    ibothers.Text = "0.00";
                    ibpart.Text = "0.00";
                    ibsplit.Text = "0.00";
                    ibtotalsumpayment.Text = "0.00";
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region PAYMENT DETAILED
        public void Fillpaymentdetailed()
        {
            try
            {      
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "  ISNULL([CASH],0.00) Field1, ISNULL([CREDIT CARD],0.00) Field2, ISNULL([UPI],0.00) Field3,ISNULL([DEBIT CARD],0.00) Field4,ISNULL([BANKING ONLINE],0.00) Field5, ISNULL([OTHERS],0.00) Field6,ISNULL([PART],0.00) Field7, ISNULL([SPLIT],0.00) Field8    FROM ( SELECT sm.netamount,g.Caption ";
                dmnSearch.Table = " tblSalesMaster sm LEFT JOIN tblGeneralOptions g ON g.GTypeSubId = sm.PaymentMode  where  sm.Status=1 AND CONVERT(DATE, sm.EntryDate) = CONVERT(DATE, '"+ dtp_dashboard.Value.ToString("yyyy-MM-dd") + "')) AS SourceTable"+
                                    " PIVOT( SUM(netamount) FOR Caption IN([CASH], [CREDIT CARD],[UPI], [DEBIT CARD], [BANKING ONLINE], [OTHERS], [PART], [SPLIT]) "+
                                    ") AS PivotTable";
                dmnSearch.WhereNdGroupBy = " ";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    ibcash.Text = Convert.ToString(gridSource[0].Field1);
                    ibcreditcard.Text = Convert.ToString(gridSource[0].Field2);
                    ibupi.Text = Convert.ToString(gridSource[0].Field3);
                    ibrupay.Text = Convert.ToString(gridSource[0].Field4);
                    ibnetbanking.Text = Convert.ToString(gridSource[0].Field5);
                    ibothers.Text = Convert.ToString(gridSource[0].Field6);
                    ibpart.Text = Convert.ToString(gridSource[0].Field7);
                    ibsplit.Text = Convert.ToString(gridSource[0].Field8);
                    decimal sumtotal = Convert.ToDecimal(gridSource[0].Field1) + Convert.ToDecimal(gridSource[0].Field2) + Convert.ToDecimal(gridSource[0].Field3) +
                                       Convert.ToDecimal(gridSource[0].Field4) + Convert.ToDecimal(gridSource[0].Field5) + Convert.ToDecimal(gridSource[0].Field6) +
                                       Convert.ToDecimal(gridSource[0].Field7) + Convert.ToDecimal(gridSource[0].Field8);
                    ibtotalsumpayment.Text= Convert.ToString(sumtotal);
                }
                else
                {
                    ibcash.Text = "0.00";
                    ibcreditcard.Text = "0.00";
                    ibupi.Text = "0.00";
                    ibrupay.Text = "0.00";
                    ibnetbanking.Text = "0.00";
                    ibothers.Text = "0.00";
                    ibpart.Text = "0.00";
                    ibsplit.Text = "0.00";
                    ibtotalsumpayment.Text = "0.00";
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region SALEDETAILS 
        public void Filldashboard()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "COUNT(*) AS Field1," +
                    " SUM(CASE WHEN CONVERT(DATE, EntryDate) = CONVERT(DATE, '" + dtp_dashboard.Value.ToString("yyyy-MM-dd") + "') THEN 1 ELSE 0 END) AS Field2," +
                    " SUM(CASE WHEN CONVERT(DATE, EntryDate) = CONVERT(DATE, '" + dtp_dashboard.Value.ToString("yyyy-MM-dd") + "') THEN netamount ELSE 0 END) AS Field3," +
                    " SUM(netamount) AS Field4,0 Field5,0 Field6 from tblSalesMaster  where Status=1 " +
                    "union select 0 Field1,0 Field2,0 Field3,0 Field4, SUM(CASE WHEN CONVERT(DATE, sm.EntryDate) = CONVERT(DATE, '" + dtp_dashboard.Value.ToString("yyyy-MM-dd") + "') THEN 1 ELSE 0 END) AS Field5," +
                    "SUM(CASE WHEN CONVERT(DATE,sm.EntryDate) = CONVERT(DATE, '" + dtp_dashboard.Value.ToString("yyyy-MM-dd") + "') THEN netamount ELSE 0 END) AS Field6";
                dmnSearch.Table = "tblSalesMaster sm left join tblOrderMaster om on om.OrderMasterId =sm.OrderMasterid ";
                dmnSearch.WhereNdGroupBy = " where om.BookmasterId > 0 and sm.Status=1  ";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    ibsaletotal.Text = Convert.ToString(gridSource[1].Field4);
                    lbltotalsaleno.Text = Convert.ToString(gridSource[1].Field1) + " Orders";
                    ibdinetotal.Text = Convert.ToString(gridSource[1].Field3);
                    lbldinetotalno.Text = Convert.ToString(gridSource[1].Field2) + " Orders";
                    ibreservatiototal.Text = Convert.ToString(gridSource[0].Field6);
                    lblresno.Text = Convert.ToString(gridSource[0].Field5) + " Orders";
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region TWO BAR CHART SALE AND RESVERTION TIME PEROD 24HRS
        public void FillChartTwoBarChart()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 12 AND 4 THEN netamount ELSE 0 END) AS Field1, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 4 AND 8 THEN netamount ELSE 0 END) AS Field2, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 8 AND 12 THEN netamount ELSE 0 END) AS Field3, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 12 AND 16 THEN netamount ELSE 0 END) AS Field4, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 16 AND 20 THEN netamount ELSE 0 END) AS Field5, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 20 AND 24 THEN netamount ELSE 0 END) AS Field6, " +
                                   " COUNT(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 12 AND 4 THEN 1 END) AS Field7," +
                                   " COUNT(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 4 AND 8 THEN 1 END) AS Field8," +
                                    " COUNT(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 8 AND 12 THEN 1 END) AS Field9," +
                                    " COUNT(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 12 AND 16 THEN 1 END) AS Field10," +
                                    " COUNT(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 16 AND 20 THEN 1 END) AS Field12," +
                                    " COUNT(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 20 AND 24 THEN 1 END) AS Field13" +
                    " FROM tblSalesMaster WHERE Status = 1 AND CONVERT(DATE, CreatedDateTime) = CONVERT(DATE, '" + dtp_dashboard.Value.ToString("yyyy-MM-dd") + "') union " +
                    "select  SUM(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 12 AND 4 THEN netamount ELSE 0 END) AS Field1," +
                                    "SUM(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 4 AND 8 THEN netamount ELSE 0 END) AS Field2," +
                                    "SUM(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 8 AND 12 THEN netamount ELSE 0 END) AS Field3," +
                                    "SUM(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 12 AND 16 THEN netamount ELSE 0 END) AS Field4," +
                                    "SUM(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 16 AND 20 THEN netamount ELSE 0 END) AS Field5," +
                                    "SUM(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 20 AND 24 THEN netamount ELSE 0 END) AS Field6," +
                                    "COUNT(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 12 AND 4 THEN 1 END) AS Field7," +
                                    "COUNT(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 4 AND 8 THEN 1 END) AS Field8," +
                                    "COUNT(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 8 AND 12 THEN 1 END) AS Field9," +
                                    "COUNT(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 12 AND 16 THEN 1 END) AS Field10," +
                                    "COUNT(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 16 AND 20 THEN 1 END) AS Field12," +
                                    "COUNT(CASE WHEN DATEPART(HOUR, sm.CreatedDateTime) BETWEEN 20 AND 24 THEN 1 END) AS Field13";
                dmnSearch.Table = "tblSalesMaster sm left join tblOrderMaster om on om.OrderMasterId =sm.OrderMasterid ";
                dmnSearch.WhereNdGroupBy = " where om.BookmasterId > 0 and sm.Status=1 AND CONVERT(DATE, sm.CreatedDateTime) = CONVERT(DATE, '" + dtp_dashboard.Value.ToString("yyyy-MM-dd") + "') ";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    chart_res_dine_sales.Series["s_dine"].Points.Clear();
                    chart_res_dine_sales.Series["s_reservation"].Points.Clear();

                    chart_res_dine_sales.Series["s_dine"].Points.AddXY(1, gridSource[0].Field1);  // 12am-4am Dine
                    chart_res_dine_sales.Series["s_reservation"].Points.AddXY(1, gridSource[1].Field1);  // 12am-4am Reservation

                    chart_res_dine_sales.Series["s_dine"].Points[0].ToolTip = "Dine 12am-4am - Total sales :" + gridSource[0].Field1 + " Orders: " + gridSource[0].Field7;
                    chart_res_dine_sales.Series["s_reservation"].Points[0].ToolTip = "Reservation 12am-4am - Total sales :" + gridSource[1].Field1 + " Orders: " + gridSource[1].Field7;

                    chart_res_dine_sales.Series["s_dine"].Points.AddXY(2, gridSource[0].Field2);  // 4am-8am Dine
                    chart_res_dine_sales.Series["s_reservation"].Points.AddXY(2, gridSource[1].Field2);  // 4am-8am Reservation

                    chart_res_dine_sales.Series["s_dine"].Points[1].ToolTip = "Dine 4am-8am  - Total sales :" + gridSource[0].Field2 + " Orders: " + gridSource[0].Field8;
                    chart_res_dine_sales.Series["s_reservation"].Points[1].ToolTip = "Reservation 4am-8am  - Total sales :" + gridSource[1].Field2 + " Orders: " + gridSource[1].Field8;

                    chart_res_dine_sales.Series["s_dine"].Points.AddXY(3, gridSource[0].Field3);  // 8am-12pm Dine
                    chart_res_dine_sales.Series["s_reservation"].Points.AddXY(3, gridSource[1].Field3);  // 8am-12pm Reservation

                    chart_res_dine_sales.Series["s_dine"].Points[2].ToolTip = "Dine 8am-12pm  - Total sales :" + gridSource[0].Field3 + " Orders: " + gridSource[0].Field9;
                    chart_res_dine_sales.Series["s_reservation"].Points[2].ToolTip = "Reservation 8am-12pm  - Total sales :" + gridSource[1].Field3 + " Orders: " + gridSource[1].Field9;

                    chart_res_dine_sales.Series["s_dine"].Points.AddXY(4, gridSource[0].Field4);  // 12pm-4pm Dine
                    chart_res_dine_sales.Series["s_reservation"].Points.AddXY(4, gridSource[1].Field4);  // 12pm-4pm Reservation

                    chart_res_dine_sales.Series["s_dine"].Points[3].ToolTip = "Dine 12pm-4pm  - Total sales :" + gridSource[0].Field4 + " Orders: " + gridSource[0].Field10;
                    chart_res_dine_sales.Series["s_reservation"].Points[3].ToolTip = "Reservation 12pm-4pm  - Total sales :" + gridSource[1].Field4 + " Orders: " + gridSource[1].Field10;

                    chart_res_dine_sales.Series["s_dine"].Points.AddXY(5, gridSource[0].Field5);  // 4pm-8pm Dine
                    chart_res_dine_sales.Series["s_reservation"].Points.AddXY(5, gridSource[1].Field5);  // 4pm-8pm Reservation

                    chart_res_dine_sales.Series["s_dine"].Points[4].ToolTip = "Dine 4pm-8pm  - Total sales :" + gridSource[0].Field5 + " Orders: " + gridSource[0].Field11;
                    chart_res_dine_sales.Series["s_reservation"].Points[4].ToolTip = "Reservation 4pm-8pm  - Total sales :" + gridSource[1].Field5 + " Orders: " + gridSource[1].Field11;

                    chart_res_dine_sales.Series["s_dine"].Points.AddXY(6, gridSource[0].Field6);  // 8pm-12am Dine
                    chart_res_dine_sales.Series["s_reservation"].Points.AddXY(6, gridSource[1].Field6);  // 8pm-12am Reservation

                    chart_res_dine_sales.Series["s_dine"].Points[5].ToolTip = "Dine 8pm-12am  - Total sales :" + gridSource[0].Field6 + " Orders: " + gridSource[0].Field12;
                    chart_res_dine_sales.Series["s_reservation"].Points[5].ToolTip = "Reservation 8pm-12am  - Total sales :" + gridSource[1].Field6 + " Orders: " + gridSource[1].Field12;

                }
            }
            catch (Exception ex)
            {

            }
        }
        private void SetupChartTwoBar()
        {
            try
            {
                chart_res_dine_sales.Series.Clear();
                // Dine series
                Series seriesDine = new Series("s_dine");
                seriesDine.ChartType = SeriesChartType.Column;
                chart_res_dine_sales.Series.Add(seriesDine);

                // Reservation series
                Series seriesReservation = new Series("s_reservation");
                seriesReservation.ChartType = SeriesChartType.Column;
                chart_res_dine_sales.Series.Add(seriesReservation);

                chart_res_dine_sales.Legends.Add(new Legend("Legend"));
                chart_res_dine_sales.Legends["Legend"].Docking = Docking.Bottom;
                seriesReservation.LegendText = "Reservation";
                seriesDine.LegendText = "Dine";
                chart_res_dine_sales.Legends["Legend"].Font = new Font("Arial", 12, FontStyle.Bold);

                // Title
                Title chartTitledineres = new Title("Dine and Reservation Total");
                chartTitledineres.Font = new Font(chartTitledineres.Font.FontFamily, 12, FontStyle.Bold);
                chart_res_dine_sales.Titles.Add(chartTitledineres);

                chart_res_dine_sales.ChartAreas[0].AxisX.Interval = 1;
                chart_res_dine_sales.ChartAreas[0].AxisX.Minimum = 0;
                chart_res_dine_sales.ChartAreas[0].AxisX.Maximum = 7;

                chart_res_dine_sales.ChartAreas[0].AxisX.CustomLabels.Add(0.5, 1.5, "12am-4am");
                chart_res_dine_sales.ChartAreas[0].AxisX.CustomLabels.Add(1.5, 2.5, "4am-8am");
                chart_res_dine_sales.ChartAreas[0].AxisX.CustomLabels.Add(2.5, 3.5, "8am-12pm");
                chart_res_dine_sales.ChartAreas[0].AxisX.CustomLabels.Add(3.5, 4.5, "12pm-4pm");
                chart_res_dine_sales.ChartAreas[0].AxisX.CustomLabels.Add(4.5, 5.5, "4pm-8pm");
                chart_res_dine_sales.ChartAreas[0].AxisX.CustomLabels.Add(5.5, 6.5, "8pm-12am");

                chart_res_dine_sales.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 9, FontStyle.Bold);
                chart_res_dine_sales.ChartAreas[0].AxisX.LabelStyle.Angle = 0;


            }
            catch (Exception ex)
            {

            }

        }
        #endregion

        #region Anuuval Chart
        public void FillbarRrevenuChart()
        {
            try
            {
                DateTime financialYearStart;
                DateTime financialYearEnd;
                int fin_year;
                if (cmb_fin_year.SelectedIndex==-1)
                {
                    DateTime currentDate = DateTime.Now;
                    fin_year = (currentDate.Month < 4) ? currentDate.Year - 1 : currentDate.Year;
                }
                else
                    fin_year = Convert.ToInt32(cmb_fin_year.SelectedValue);

                financialYearStart = new DateTime(fin_year, 4, 1);
                financialYearEnd = new DateTime(fin_year + 1, 3, 31);

                BllAnnuvalreport<DmnRpt_1> SearchDetails = new BllAnnuvalreport<DmnRpt_1>();
                DmnRpt_1 dmnSearchDetBill = new DmnRpt_1();

                dmnSearchDetBill.dt1 = Convert.ToDateTime(financialYearStart.ToString("yyyy-MM-dd"));
                dmnSearchDetBill.dt2 = Convert.ToDateTime(financialYearEnd.ToString("yyyy-MM-dd"));
                var invoiceBillMasters = SearchDetails.serachreport("prdISerachAnnuvalreport", dmnSearchDetBill);
                var sortedResult = invoiceBillMasters
                    .OrderBy(item => CustomOrder(item.nFld1))
                    .ToList();
                int CustomOrder(int nFld1)
                {
                    if (nFld1 >= 4 && nFld1 <= 12)
                        return nFld1;
                    else
                        return nFld1 + 100;
                }
   
                if (sortedResult.Count > 0)
                {
                    Totalrevenuechart.Series["Sales"].Points.Clear();
                    Totalrevenuechart.Series["Purchase"].Points.Clear();
                    Totalrevenuechart.Series["PurchaseReturn"].Points.Clear();

                    for (int i=0;i< sortedResult.Count;i++)
                    {
                        Totalrevenuechart.Series["Sales"].Points.AddXY(i+1, sortedResult[i].dFld1);
                        Totalrevenuechart.Series["Purchase"].Points.AddXY(i+1, sortedResult[i].dFld2);
                        Totalrevenuechart.Series["PurchaseReturn"].Points.AddXY(i+1, sortedResult[i].dFld3);

                        Totalrevenuechart.Series["Sales"].Points[i].ToolTip = $"Sales: "+ sortedResult[i].dFld1;
                        Totalrevenuechart.Series["Purchase"].Points[i].ToolTip = $"Purchase: "+ sortedResult[i].dFld2;
                        Totalrevenuechart.Series["PurchaseReturn"].Points[i].ToolTip = $"Purchase Return: "+ sortedResult[i].dFld3;

                    }

                }

                Title existingTitle = Totalrevenuechart.Titles.FirstOrDefault(t => t.Text.StartsWith("Annual Report"));
                if (existingTitle != null)
                    existingTitle.Text = $"Annual Report {financialYearStart:MMM-yyyy} - {financialYearEnd:MMM-yyyy}";               
            }
            catch (Exception ex)
            {

            }
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
        private void SetupChart_Revenue()
        {
            try
            {
                DateTime financialYearStart;
                DmnComboBox selectedFinYear = (DmnComboBox)cmb_fin_year.SelectedItem;
                int year = Convert.ToInt32(selectedFinYear.Id);
                financialYearStart = new DateTime(year, 4, 1);
                DateTime financialYearEnd = financialYearStart.AddYears(1).AddDays(-1); 

                Totalrevenuechart.Series.Clear();
                // Sales series
                Series seriesSales = new Series("Sales");
                seriesSales.ChartType = SeriesChartType.Column;
                Totalrevenuechart.Series.Add(seriesSales);

                // Purchase series
                Series seriesPurchase = new Series("Purchase");
                seriesPurchase.ChartType = SeriesChartType.Column;
                Totalrevenuechart.Series.Add(seriesPurchase);

                // Purchase Return series
                Series seriesPurchaseReturn = new Series("PurchaseReturn");
                seriesPurchaseReturn.ChartType = SeriesChartType.Column;
                Totalrevenuechart.Series.Add(seriesPurchaseReturn);


                Totalrevenuechart.Legends.Add(new Legend("Legend"));
                Totalrevenuechart.Legends["Legend"].Docking = Docking.Bottom;
                seriesSales.LegendText = "Sales";
                seriesPurchase.LegendText = "Purchase";
                seriesPurchaseReturn.LegendText = "Purchase Return";
                Totalrevenuechart.Legends["Legend"].Font = new Font("Arial", 12, FontStyle.Bold);

                // Title
                Title chartTitle = new Title($"Annual Report {financialYearStart:MMM-yyyy} - {financialYearEnd:MMM-yyyy}");
                chartTitle.Font = new Font(chartTitle.Font.FontFamily, 12, FontStyle.Bold);
                Totalrevenuechart.Titles.Add(chartTitle);

                Totalrevenuechart.ChartAreas[0].AxisX.Interval = 1;
                Totalrevenuechart.ChartAreas[0].AxisX.Minimum = 0;
                Totalrevenuechart.ChartAreas[0].AxisX.Maximum = 13;

                Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(0.5, 1.5, $"{financialYearStart:MMM-yyyy}");
                Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(1.5, 2.5, $"{financialYearStart.AddMonths(1):MMM-yyyy}");
                Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(2.5, 3.5, $"{financialYearStart.AddMonths(2):MMM-yyyy}");
                Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(3.5, 4.5, $"{financialYearStart.AddMonths(3):MMM-yyyy}");
                Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(4.5, 5.5, $"{financialYearStart.AddMonths(4):MMM-yyyy}");
                Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(5.5, 6.5, $"{financialYearStart.AddMonths(5):MMM-yyyy}");
                Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(6.5, 7.5, $"{financialYearStart.AddMonths(6):MMM-yyyy}");
                Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(7.5, 8.5, $"{financialYearStart.AddMonths(7):MMM-yyyy}");
                Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(8.5, 9.5, $"{financialYearStart.AddMonths(8):MMM-yyyy}");
                Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(9.5, 10.5, $"{financialYearStart.AddMonths(9):MMM-yyyy}");
                Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(10.5, 11.5, $"{financialYearStart.AddMonths(10):MMM-yyyy}");
                Totalrevenuechart.ChartAreas[0].AxisX.CustomLabels.Add(11.5, 12.5, $"{financialYearStart.AddMonths(11):MMM-yyyy}");

                Totalrevenuechart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 9, FontStyle.Bold);

                Totalrevenuechart.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
            }
            catch (Exception ex)
            {
                
            }
        }
        #endregion

        #region ONE BAR CHART TOTAL DISCOUNT TIME PEROD 24HRS
        public void FillCharttotaldiscountChart()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 12 AND 4 THEN TotalDiscount ELSE 0 END) AS Field1, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 4 AND 8 THEN TotalDiscount ELSE 0 END) AS Field2, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 8 AND 12 THEN TotalDiscount ELSE 0 END) AS Field3, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 12 AND 16 THEN TotalDiscount ELSE 0 END) AS Field4, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 16 AND 20 THEN TotalDiscount ELSE 0 END) AS Field5, " +
                                   " SUM(CASE WHEN DATEPART(HOUR, CreatedDateTime) BETWEEN 20 AND 24 THEN TotalDiscount ELSE 0 END) AS Field6";
                dmnSearch.Table = " tblSalesMaster ";
                dmnSearch.WhereNdGroupBy = " where  Status=1 AND CONVERT(DATE, CreatedDateTime) = CONVERT(DATE, '" + dtp_dashboard.Value.ToString("yyyy-MM-dd") + "') ";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    totaldisc_chart.Series["s_totaldiscount"].Points.Clear();
                    totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(1, gridSource[0].Field1);  // 12am-4am 
                    totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(2, gridSource[0].Field2);  // 4am-8am 
                    totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(3, gridSource[0].Field3);  // 8am-12pm 
                    totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(4, gridSource[0].Field4);  // 12pm-4pm 
                    totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(5, gridSource[0].Field5);  // 4pm-8pm
                    totaldisc_chart.Series["s_totaldiscount"].Points.AddXY(6, gridSource[0].Field6);  // 8pm-12am 

                    totaldisc_chart.Series["s_totaldiscount"].Points[0].ToolTip = $"Discount : " + gridSource[0].Field1;
                    totaldisc_chart.Series["s_totaldiscount"].Points[1].ToolTip = $"Discount : " + gridSource[0].Field2;
                    totaldisc_chart.Series["s_totaldiscount"].Points[2].ToolTip = $"Discount : " + gridSource[0].Field3;
                    totaldisc_chart.Series["s_totaldiscount"].Points[3].ToolTip = $"Discount : " + gridSource[0].Field4;
                    totaldisc_chart.Series["s_totaldiscount"].Points[4].ToolTip = $"Discount : " + gridSource[0].Field5;
                    totaldisc_chart.Series["s_totaldiscount"].Points[5].ToolTip = $"Discount : " + gridSource[0].Field6;

                }
            }
            catch (Exception ex)
            {
            }
        }
        private void SetupCharttotaldiscount()
        {
            try
            {
                totaldisc_chart.Series.Clear();
                // Dine series
                Series seriestotalsi = new Series("s_totaldiscount");
                seriestotalsi.ChartType = SeriesChartType.Column;
                totaldisc_chart.Series.Add(seriestotalsi);

                // Title
                Title chartTitletotaldis = new Title("Total Discount");
                chartTitletotaldis.Font = new Font(chartTitletotaldis.Font.FontFamily, 12, FontStyle.Bold);
                totaldisc_chart.Titles.Add(chartTitletotaldis);

                totaldisc_chart.ChartAreas[0].AxisX.Interval = 1;
                totaldisc_chart.ChartAreas[0].AxisX.Minimum = 0;
                totaldisc_chart.ChartAreas[0].AxisX.Maximum = 7;

                totaldisc_chart.ChartAreas[0].AxisX.CustomLabels.Add(0.5, 1.5, "12am-4am");
                totaldisc_chart.ChartAreas[0].AxisX.CustomLabels.Add(1.5, 2.5, "4am-8am");
                totaldisc_chart.ChartAreas[0].AxisX.CustomLabels.Add(2.5, 3.5, "8am-12pm");
                totaldisc_chart.ChartAreas[0].AxisX.CustomLabels.Add(3.5, 4.5, "12pm-4pm");
                totaldisc_chart.ChartAreas[0].AxisX.CustomLabels.Add(4.5, 5.5, "4pm-8pm");
                totaldisc_chart.ChartAreas[0].AxisX.CustomLabels.Add(5.5, 6.5, "8pm-12am");

                totaldisc_chart.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", 8, FontStyle.Bold);
                totaldisc_chart.ChartAreas[0].AxisX.LabelStyle.Angle = 0;


            }
            catch (Exception ex)
            {

            }

        }
        #endregion

        #region PIE CHART EXPENCE
        public void SetupChartExpence()
        {
            try
            {
                expencepiechart.Series.Clear();
                expencepiechart.Legends.Clear();

                Series seriesExpence = new Series("s_expence");
                seriesExpence.ChartType = SeriesChartType.Doughnut;
                expencepiechart.Series.Add(seriesExpence);

                // Add legend
                expencepiechart.Legends.Add(new Legend("Legend"));
                expencepiechart.Legends["Legend"].Docking = Docking.Right;
                seriesExpence.LegendText = "Expence";
                expencepiechart.Legends["Legend"].Font = new Font("Arial", 8, FontStyle.Regular);

                // Add title
                Title chartTitleExpence = new Title("Expence");
                chartTitleExpence.Font = new Font(chartTitleExpence.Font.FontFamily, 12, FontStyle.Bold);
                expencepiechart.Titles.Add(chartTitleExpence);
            }
            catch (Exception ex)
            {

            }
        }

        public void FillChartExpence()
        {
            try
            {
                BllExpenceChart<DmnGridFields> bllSearch = new BllExpenceChart<DmnGridFields>();
                DmnGridFields dmnSearch = new DmnGridFields();
                dmnSearch.StartDate = dtp_dashboard.Value;
                var gridSource = bllSearch.serachexpencedata("prdexpencechartdata", dmnSearch);

                if (gridSource.Count > 0)
                {

                    foreach (var row in gridSource)
                    {
                        var category = row.Field1;
                        var amount = row.Field2;

                        var dataPoint = new DataPoint(); 
                        dataPoint.SetValueXY(category, amount);
                        expencepiechart.Series["s_expence"].Points.Add(dataPoint);
                        dataPoint.LegendText = $"{category}: {amount}"; 
                        dataPoint.ToolTip = $"{category}: {amount}"; 
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }


        #endregion


        private void dtp_dashboard_ValueChanged(object sender, EventArgs e)
        {
            Filldashboard();
            FillChartTwoBarChart();
            FillCharttotaldiscountChart();
            FillChartExpence();
            Fillpaymentdetailed();
        }

        private void cmb_fin_year_SelectionChangeCommitted(object sender, EventArgs e)
        {
           FillbarRrevenuChart();
        }

        private void FrmDashboardReport_Resize(object sender, EventArgs e)
        {
           // cmb_fin_year.Location = new Point(Totalrevenuechart.Right, Totalrevenuechart.Top);
        }
    }
}
