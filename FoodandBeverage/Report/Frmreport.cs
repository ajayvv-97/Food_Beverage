using BLL;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Domain;
using FastMember;
using FoodandBeverage.Forms;
using FoodandBeverage.Interfaces;
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
        public int Reportid = 0;
        public Frmreport(int Rpid)
        {
            Reportid = Rpid;
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
        bool FlagExcel = false;
        bool FlagColor = false;
        #endregion
        private void Frmreport_Load(object sender, EventArgs e)
        {
            combofill_report();
            Fill_combo_finyear();

        }
        public void Fill_combo_finyear()
        {
            try
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
            catch (Exception ex)
            {

            }

        }
        private void chk_time_CheckedChanged(object sender, EventArgs e)
        {
            Checktime_Date();
        }
        public void Checktime_Date()
        {
            try
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
            catch (Exception ex)
            {

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
                    case 7:
                        s_FillReport_PopularItems();
                        break;
                    case 8:
                        s_FillReport_SlowmovingItems();
                        break;
                    case 10:
                        s_FillReport_Offerlist();
                        break;
                    case 11:
                        s_FillReport_EmployeeList();
                        break;
                    case 14:
                        s_FillReport_MidasCard_PaymetReport();
                        break;
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
                    case 19:
                        s_FillReport_supplierPurchase_return(); ;
                        break;
                    case 20:
                        s_FillReport_Purchase_Return();
                        break;
                    case 22:
                        s_FillReport_supplierPurchase();
                        break;
                    case 23:
                        s_FillReport_Purchase();
                        break;
                    case 24:
                        s_FillReport_Reorder();
                        break;
                    case 25:
                        s_FillReport_Expence();
                        break;
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
                    case 37:
                        s_FillReport_Stockusage();
                        break;

                    case 38:
                        s_FillReport_AnuvalGameRevenue();
                        break;
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
                    case 44:
                        s_FillReport_TableOccupancyReport();
                        break;
                    case 45:
                        s_FillReport_Reservation_Details();
                        break;
                    case 46:
                        s_FillReport_Reservation();
                        break;
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
        public void s_FillReport_Reservation_Details()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dt_Rpt = new DataTable("Rpt_DT");
                BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
                DmnSearch dmnSearchDetBill = new DmnSearch();
                dmnSearchDetBill.Select = "BookingDateTime sFld1,cusName sFld2,Mobile sFld3,bd.Name sFld4,NoOfPeople nFld1,bm.BookmasterId nFld2,bd.Rid nFld3,DiscountPer dFld1,AdvancePay dFld2,CashRev dFld3,NetAmount dFld4,bd.Quantity dFld5";/*,fcm. as dFld2,fcm.Tax as dFld3*/
                dmnSearchDetBill.Table = "tblbookMaster bm left join tblBookDetails bd on bd.BookmasterId =bm.BookmasterId ";
                if(cmbSublist.SelectedIndex==0)
                    dmnSearchDetBill.WhereNdGroupBy = "where  BookingDateTime >= '" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and BookingDateTime<= '" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and bm.status=3 and bd.Status=3";
                else if (cmbSublist.SelectedIndex == 1)
                    dmnSearchDetBill.WhereNdGroupBy = "where  BookingDateTime >= '" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and BookingDateTime<= '" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and bm.status=1 and bd.Status=1";
                else 
                    dmnSearchDetBill.WhereNdGroupBy = "where  BookingDateTime >= '" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and BookingDateTime<= '" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and bm.status=0 and bd.Status=0";

                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);

                var filteredmenudata = invoiceBillMasters
                    .Where(item => item.nFld3 != 0)
                    .OrderBy(item => item.sFld1)
                    .ToList();

                var filteredList = invoiceBillMasters
                    .Where(item => item.nFld3 == 0)
                    .OrderBy(item => item.sFld1)
                    .Select(item => new {
                        sFld1 = item.sFld1,
                        sFld2 = item.sFld2,
                        sFld3 = item.sFld3,
                        sFld5 = item.sFld4,

                        nFld1 = item.nFld1,
                        nFld2 = item.nFld2,
                        nFld3 = item.nFld3,

                        dFld1 = item.dFld1,
                        dFld2 = item.dFld2,
                        dFld3 = item.dFld3,
                        dFld4 = item.dFld4,
                        dFld5 = item.dFld5,

                    })
                    .ToList();

                var mergedList = filteredmenudata
                    .Zip(filteredList, (menu, list) => new
                    {
                        menu.sFld1,
                        menu.sFld2,
                        menu.sFld3,
                        menu.sFld4,
                        sFld5 = list.sFld5,
                        menu.nFld1,
                        menu.nFld2,
                        menu.nFld3,
                        dFld1 = (double)menu.dFld1,
                        dFld2 = (double)menu.dFld2,
                        dFld3 = (double)menu.dFld3,
                        dFld4 = (double)menu.dFld4,
                        dFld5 = (double)menu.dFld5,
                    })
                    .Concat(filteredList.Skip(filteredmenudata.Count())
                                        .Select(item => new
                                        {                   
                                            sFld1 = item.sFld1,
                                            sFld2 = item.sFld2,
                                            sFld3 = item.sFld3,
                                            sFld4 = string.Empty,
                                            sFld5 = item.sFld5,

                                            nFld1 = 0,
                                            nFld2 = item.nFld2,
                                            nFld3 = 0,

                                            dFld1 = (double)item.dFld1,
                                            dFld2 = (double)item.dFld2,
                                            dFld3 = (double)item.dFld3,
                                            dFld4 = (double)item.dFld4,
                                            dFld5 = (double)item.dFld5,
                                        }))
                    .OrderBy(item => item.sFld1)
                    .ToList();


                //if (mergedList.Count > 0)
                //{
                //    mergedList[0].sFld1 = Convert.ToDateTime(dtpfrom.Text);
                //    mergedList[0].sFld2 = Convert.ToDateTime(dtpto.Text);
                //}
                using (var reader = ObjectReader.Create(mergedList, "sFld1", "sFld2", "sFld3", "sFld4", "sFld5", "nFld1", "nFld2", "nFld3", "dFld1", "dFld2", "dFld3", "dFld4", "dFld5"))
                {
                    dt_Rpt.Load(reader);
                }
                dsReport.Tables.Add(dt_Rpt);
                reportDocument.Load(Path + @"\Rpt_reservation_details.rpt");
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
        public void s_FillReport_TableOccupancyReport()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dt_Rpt = new DataTable("Rpt_DT");
                BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
                DmnSearch dmnSearchDetBill = new DmnSearch();  
                
                dmnSearchDetBill.Select = "ti.Tid nFld1, ti.TableCode sFld1, ti.Capcity nFld2, COALESCE(sm.TimeDuration, 0) AS nFld3,0 AS nFld4,isnull(DATEPART(HOUR, sm.CreatedDateTime),0) nFld6";
                dmnSearchDetBill.Table = "tblTable_info ti " +
                    " left join tblSalesMaster sm on sm.Tid =ti.Tid and sm.CreatedDateTime >= '" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and sm.CreatedDateTime<= '" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and sm.status=1 where ti.Status = 1 " +
                    " union select cb.Tid nFld1, ti.TableCode sFld1, ti.Capcity nFld2, 0 nFld3,COALESCE(TimeDuration, 0) AS nFld4,DATEPART(HOUR, cb.CreatedOn) nFld6  from tblCombinedOrderBill cb left join tblTable_info ti on ti.Tid = cb.tid ";
                dmnSearchDetBill.WhereNdGroupBy = " where cb.CreatedOn >= '" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and cb.CreatedOn<= '" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and cb.status=1"; 
                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);

                //var extractedData1 = invoiceBillMasters.Select(item => new
                //{
                //    nFld1 = item.nFld1,
                //    nFld7 = item.nFld6
                //});
                //var extractedData2 = invoiceBillMasters.Select(item => new
                //{
                //    nFld1 = item.nFld1,
                //    nFld7 = item.nFld7
                //});

                //var combinedData = extractedData1.Concat(extractedData2);

                var duplicateCounts = invoiceBillMasters
                    .GroupBy(item => new { item.nFld1, item.nFld6 })
                    .Where(group => group.Count() > 1)
                    .Select(group => new
                    {
                        nFld1 = group.Key.nFld1,
                        nFld6 = group.Key.nFld6,
                        nFld7 = group.Count()
                    })
                    .ToList();

                var result = duplicateCounts.GroupBy(x => x.nFld1, (key, g) => g.OrderBy(e => e.nFld6).First());

                var sortedInvoiceBillMasters = invoiceBillMasters
                    .GroupBy(item => item.nFld1)
                    .Select(group => new
                    {
                        nFld1 = group.Key,
                        sFld1 = group.First().sFld1,
                        nFld2 = group.First().nFld2,
                        nFld3 = group.Sum(item => item.nFld3) + group.Sum(item => item.nFld4),
                        nFld5 = group.Count(item => item.nFld3 > 0 || item.nFld4 > 0),
                        nFld6 = result.Any(r => group.First().nFld1 == r.nFld1) ? result.First(r => group.First().nFld1 == r.nFld1).nFld6 : group.First().nFld6,
                    })
                    .OrderByDescending(item => item.nFld3)
                    .ToList();
                if (invoiceBillMasters.Count > 0)
                {
                    invoiceBillMasters[0].FromDate = Convert.ToDateTime(dtpfrom.Text);
                    invoiceBillMasters[0].ToDate = Convert.ToDateTime(dtpto.Text);
                }
                using (var reader = ObjectReader.Create(sortedInvoiceBillMasters, "sFld1", "nFld2", "nFld3", "nFld5", "nFld6"))
                {
                    dt_Rpt.Load(reader);
                }
                dsReport.Tables.Add(dt_Rpt);
                reportDocument.Load(Path + @"\Rpt_TableOccupancyReport.rpt");
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
        public void s_FillReport_Reservation()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dt_Rpt = new DataTable("Rpt_DT");
                BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
                DmnSearch dmnSearchDetBill = new DmnSearch();
                dmnSearchDetBill.Select = "BookingDateTime sFld1,cusName sFld2,Mobile sFld3,NoOfPeople nFld1,DiscountPer dFld1,AdvancePay dFld2,CashRev dFld3,NetAmount dFld4";/*,fcm. as dFld2,fcm.Tax as dFld3*/
                dmnSearchDetBill.Table = "tblbookMaster";

                if (cmbSublist.SelectedIndex == 0)
                    dmnSearchDetBill.WhereNdGroupBy = "where  BookingDateTime >= '" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "'   and BookingDateTime<= '" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and status=3 ";
                else if (cmbSublist.SelectedIndex == 1)
                    dmnSearchDetBill.WhereNdGroupBy = "where  BookingDateTime >= '" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "'   and BookingDateTime<= '" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and status=1 ";
                else
                    dmnSearchDetBill.WhereNdGroupBy = "where  BookingDateTime >= '" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "'   and BookingDateTime<= '" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and status=0 ";


                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);
                if (invoiceBillMasters.Count > 0)
                {
                    invoiceBillMasters[0].FromDate = Convert.ToDateTime(dtpfrom.Text);
                    invoiceBillMasters[0].ToDate = Convert.ToDateTime(dtpto.Text);
                }
                using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "sFld3", "sFld4", "nFld1", "dFld1", "dFld2", "dFld3", "dFld4"))
                {
                    dt_Rpt.Load(reader);
                }
                dsReport.Tables.Add(dt_Rpt);
                reportDocument.Load(Path + @"\Rpt_reservation.rpt");
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
        
        public void s_FillReport_MidasCard_PaymetReport()
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
            try
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
            catch(Exception ex)
            {

            }

        }

        private void btnWeekEnd_Click(object sender, EventArgs e)
        {
            try
            {             DateTime currentDate = DateTime.Now;
            //currentDate=Convert.ToDateTime("2023-05-28 10:40:00");
            chk_time.Checked = false;
            int d = Convert.ToInt32(-(int)currentDate.DayOfWeek);
            dtpfrom.Value = currentDate.AddDays(-(int)currentDate.DayOfWeek - 1); // Start date of the current week
            dtpto.Value = currentDate.AddDays(-(int)currentDate.DayOfWeek); // End date of the current week
            btnserach_Click(sender, e);}
            catch (Exception ex)
            {

            }

        }
        private void btnWeekly_Click(object sender, EventArgs e)
        {
            try
            {            DateTime currentDate = DateTime.Now;
            //currentDate=Convert.ToDateTime("2023-05-28 10:40:00");
            chk_time.Checked = false;
            dtpfrom.Value = currentDate.AddDays(-(int)currentDate.DayOfWeek);
            dtpto.Value = dtpfrom.Value.AddDays(6);
            btnserach_Click(sender, e); }
            catch (Exception ex)
            {

            }

        }
        private void btnMonthly_Click(object sender, EventArgs e)
        {
            try
            {             DateTime currentDate = DateTime.Now;
            dtpfrom.Value = new DateTime(currentDate.Year, currentDate.Month, 1);
            dtpto.Value = dtpfrom.Value.AddMonths(1).AddDays(-1);
            chk_time.Checked = false;
            btnserach_Click(sender, e);}
            catch (Exception ex)
            {

            }

        }
        private void btnYealy_Click(object sender, EventArgs e)
        {
            try
            {             DateTime currentDate = DateTime.Now;
            dtpfrom.Value = new DateTime(currentDate.Year, 1, 1);
            dtpto.Value = new DateTime(currentDate.Year, 12, 31);
            chk_time.Checked = false;
            btnserach_Click(sender, e);}
            catch (Exception ex)
            {

            }

        }
        public void s_FillReport_supplierPurchase_return()
        {
            try
            {    
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dt_Rpt = new DataTable("Rpt_DT");
                BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
                DmnSearch dmnSearchDetBill = new DmnSearch();

                dmnSearchDetBill.Select = "v.VendorName as sFld1,(select CONVERT(varchar(50), pm.EntryDate, 105)) as sFld2,(select CONVERT(varchar(50), prm.EntryDate, 105)) as sFld3,h.Name as sFld4,h.Email as sFld5,h.Phone as sFld6,h.Address as sFld7," +
                    "pd.Quantity as dFld1,prd.Quantity as dFld2,prm.DiscountAmount as dFld3,prm.NetAmount as dFld4,prm.BalAmnt as dFld5,prm.RcvAmnt as dFld6";
                dmnSearchDetBill.Table = " tblPurchaseReturnMaster prm left join tblPurchaseReturnDetails prd on prd.PurchaseReturnMasterId = prm.PurchaseReturnMasterId left join tblVendor v on v.VendorId = prm.LedgerId left join tblPurchaseMaster pm on pm.PurchaseMasterId =prm.PurchaseMasterId left join tblPurchaseDetails pd on pd.PurchaseDetailId =prd.PurchaseDetailId left join tblHotelRegistration h on h.BranchId=pm.BranchId";
                dmnSearchDetBill.WhereNdGroupBy = "where prm.EntryDate>='" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and prm.EntryDate <='" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and  prm.BranchId = " + BranchId;
                dmnSearchDetBill.OrderBy = "Order By prd.PurchaseReturnDetailsId";
                if (cmbSublist.SelectedIndex ==0)
                {
                    dmnSearchDetBill.WhereNdGroupBy = "where  prm.EntryDate>='" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and prm.EntryDate <='" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and  prm.BranchId = " + BranchId;
                }
                else
                {
                    dmnSearchDetBill.WhereNdGroupBy = "where prm.LedgerId = " + cmbSublist.SelectedIndex + " and  prm.EntryDate>='" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and prm.EntryDate <='" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and  prm.BranchId = " + BranchId;

                }

                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);

                if (invoiceBillMasters.Count > 0)
                {
                    invoiceBillMasters[0].FromDate = Convert.ToDateTime(dtpfrom.Text);
                    invoiceBillMasters[0].ToDate = Convert.ToDateTime(dtpto.Text);
                }

                using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "sFld3", "dFld1", "dFld2", "dFld3", "dFld4", "dFld5", "dFld6"))
                {
                    dt_Rpt.Load(reader);
                }
                dsReport.Tables.Add(dt_Rpt);
                reportDocument.Load(Path + @"\Rpt_Supplier_purchasereturn.rpt");
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
        public void s_FillReport_AnuvalGameRevenue()
        {
            try
            {
                DateTime currentDate = DateTime.Now;
                int fin_year = (currentDate.Month < 4) ? currentDate.Year - 1 : currentDate.Year;
                DateTime financialYearStart;
                DateTime financialYearEnd;
                string financialYear;
                if (FlagExcel)
                {
                    DmnComboBox selectedFinYear = (DmnComboBox)cmb_fin_year.SelectedItem;
                    int year = Convert.ToInt32(selectedFinYear.Id);
                    financialYearStart = new DateTime(year, 4, 1);
                    financialYearEnd = new DateTime(year + 1, 3, 31);
                }
                else
                {
                    financialYearStart = new DateTime(fin_year, 4, 1);
                    financialYearEnd = new DateTime(fin_year + 1, 3, 31);
                }

                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dt_Rpt = new DataTable("Rpt_DT");

                BllAnnuvalreport<DmnRpt_1> SearchDetails = new BllAnnuvalreport<DmnRpt_1>();
                DmnRpt_1 dmnSearchDetBill = new DmnRpt_1();

                dmnSearchDetBill.dt1 = Convert.ToDateTime(financialYearStart.ToString("yyyy-MM-dd"));
                dmnSearchDetBill.dt2 = Convert.ToDateTime(financialYearEnd.ToString("yyyy-MM-dd"));
                var invoiceBillMasters = SearchDetails.serachreport("prdISerachAnnuvalreport", dmnSearchDetBill);

                #region old code
                //DmnSearch dmnSearchDetBill = new DmnSearch();
                //dmnSearchDetBill.Select = " WITH RecursiveMonths AS ( SELECT  DATEFROMPARTS(YEAR('" + financialYearStart.ToString("yyyy-MM-dd") + "'), MONTH('" + financialYearStart.ToString("yyyy-MM-dd") + "'), 1) AS StartMonth UNION ALL SELECT DATEADD(MONTH, 1, StartMonth)FROM RecursiveMonths WHERE StartMonth< '" + financialYearStart.ToString("yyyy-MM-dd") + "')," +
                //    " DT AS (SELECT MONTH(RM.StartMonth) AS nFld1, CONCAT(DATENAME(MONTH, RM.StartMonth), '-', YEAR(RM.StartMonth)) AS sFld1,COALESCE(SUM(NetAmount), 0) AS dFld1, NULL AS dFld2, NULL AS dFld3 FROM RecursiveMonths RM LEFT JOIN  tblSalesMaster S ON MONTH(RM.StartMonth) = MONTH(S.EntryDate) AND YEAR(RM.StartMonth) = YEAR(S.EntryDate) " +
                //    " WHERE RM.StartMonth >= '" + financialYearStart.ToString("yyyy-MM-dd") + "' AND RM.StartMonth < '" + financialYearEnd.ToString("yyyy-MM-dd") + "' GROUP BY  YEAR(RM.StartMonth), MONTH(RM.StartMonth), DATENAME(MONTH, RM.StartMonth)" +
                //    " union SELECT MONTH(EntryDate) AS nFld1,CONCAT(DATENAME(MONTH, EntryDate), '-', YEAR(EntryDate)) AS sFld1, NULL AS dFld1,SUM(NetAmount) AS dFld2, NULL AS dFld3 FROM tblPurchaseMaster where EntryDate >= '" + financialYearStart.ToString("yyyy-MM-dd HH:mm:ss") + "' AND EntryDate < ' " + financialYearEnd.ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY  YEAR(EntryDate), MONTH(EntryDate), DATENAME(MONTH, EntryDate) " +
                //    "union SELECT MONTH(EntryDate) AS nFld1, CONCAT(DATENAME(MONTH, EntryDate), '-', YEAR(EntryDate)) AS sFld1, NULL AS dFld1,NULL AS dFld2,    SUM(NetAmount) AS dFld3 FROM tblPurchaseReturnMaster WHERE EntryDate >= '" + financialYearStart.ToString("yyyy-MM-dd HH:mm:ss") + "' AND EntryDate < ' " + financialYearEnd.ToString("yyyy-MM-dd HH:mm:ss") + "'GROUP BY YEAR(EntryDate), MONTH(EntryDate), DATENAME(MONTH, EntryDate) )" +
                //    "(SELECT  nFld1,sFld1,sum(isnull(dFld1,0)) dFld1,ISNULL(sum(dFld2),0) dFld2,ISNULL(sum(dFld3),0) dFld3";
                //dmnSearchDetBill.Table = "DT";
                //dmnSearchDetBill.WhereNdGroupBy = "";
                //dmnSearchDetBill.OrderBy = "GROUP BY sFld1,nFld1)";

                //dmnSearchDetBill.Select = " MONTH(EntryDate) AS nFld1, CONCAT(DATENAME(MONTH, EntryDate), '-', YEAR(EntryDate)) AS sFld1, SUM(NetAmount) AS dFld1,0 AS dFld2,0 AS dFld3 FROM tblSalesMaster WHERE EntryDate >= '" + financialYearStart.ToString("yyyy-MM-dd HH:mm:ss") + "' AND EntryDate < ' " + financialYearEnd.ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY  YEAR(EntryDate), MONTH(EntryDate), DATENAME(MONTH, EntryDate) " +
                //" UNION SELECT MONTH(EntryDate) AS nFld1, CONCAT(DATENAME(MONTH, EntryDate), '-', YEAR(EntryDate)) AS sFld1, 0 AS dFld1, SUM(NetAmount) AS dFld2, 0 AS dFld3 FROM tblPurchaseMaster WHERE EntryDate >= '" + financialYearStart.ToString("yyyy-MM-dd HH:mm:ss") + "' AND EntryDate < ' " + financialYearEnd.ToString("yyyy-MM-dd HH:mm:ss") + "' GROUP BY  YEAR(EntryDate), MONTH(EntryDate), DATENAME(MONTH, EntryDate) " +
                //" UNION SELECT 	MONTH(EntryDate) AS nFld1, CONCAT(DATENAME(MONTH, EntryDate), '-', YEAR(EntryDate)) AS sFld1, 0 AS dFld1,0 AS dFld2,    SUM(NetAmount) AS dFld3 ";
                //dmnSearchDetBill.Table = " tblPurchaseReturnMaster";
                //dmnSearchDetBill.WhereNdGroupBy = "WHERE EntryDate >= '" + financialYearStart.ToString("yyyy-MM-dd HH:mm:ss") + "' AND EntryDate< ' " + financialYearEnd.ToString("yyyy-MM-dd HH:mm:ss")+"' ";
                //dmnSearchDetBill.OrderBy = " GROUP BY YEAR(EntryDate), MONTH(EntryDate), DATENAME(MONTH, EntryDate)";
                //var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);

                //var allMonths = Enumerable.Range(1, 12);

                //var sortedResult = allMonths
                //    .GroupJoin(
                //        invoiceBillMasters,
                //        month => month,
                //        invoice => invoice.nFld1,
                //        (month, invoices) => new
                //        {
                //            nFld1 = month,
                //            sFld1 = invoices.Any() ? invoices.First().sFld1 : null,
                //            dFld1 = invoices.Sum(item => item.dFld1),
                //            dFld2 = invoices.Sum(item => item.dFld2),
                //            dFld3 = invoices.Sum(item => item.dFld3)
                //        })
                //    .OrderBy(item => item.nFld1)
                //    .ToList();



                //if (invoiceBillMasters.Count > 0)
                //{
                //    invoiceBillMasters[0].nFld1 = 0;
                //}
                #endregion

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


                using (var reader = ObjectReader.Create(sortedResult, "sFld1", "dFld1", "dFld2", "dFld3", "nFld1"))
                {
                    dt_Rpt.Load(reader);
                }
                dsReport.Tables.Add(dt_Rpt);
                reportDocument.Load(Path + @"\Rpt_Annuvalreport.rpt");
                reportDocument.SetDataSource(dsReport);

                /* header section*/
                Addressdata = Get_CompantDetails();
                reportDocument.SetParameterValue("Loc_Name", Convert.ToString(Addressdata[0].Name));
                reportDocument.SetParameterValue("City", Convert.ToString(Addressdata[0].City));
                reportDocument.SetParameterValue("Employe_Name", Convert.ToString(FrmLogin.EmployeName));
                reportDocument.SetParameterValue("GSTno", Convert.ToString(Addressdata[0].GstNo));
                string devicename = System.Environment.MachineName;
                reportDocument.SetParameterValue("Device_name", devicename);

                reportDocument.SetParameterValue("MyFromDtm", financialYearStart);
                reportDocument.SetParameterValue("MyToDatem", financialYearEnd);

                reportDocument.SetParameterValue("From_re_time", financialYearStart);
                reportDocument.SetParameterValue("To_re_time", financialYearEnd);
                /* report Heading  */

                financialYear = string.Format("{0}-{1}", financialYearStart.ToString("yyyy"), financialYearEnd.ToString("yyyy"));
                reportDocument.SetParameterValue("Heading", "Annuval Revenue Report " + financialYear);


                if (FlagExcel)
                {
                    reportDocument.SetParameterValue("Flag", 1);
                    TextObject textObj1 = reportDocument.ReportDefinition.ReportObjects["Text6"] as TextObject;
                    textObj1.Color = Color.Black;
                    TextObject textObj2 = reportDocument.ReportDefinition.ReportObjects["Text40"] as TextObject;
                    textObj2.Color = Color.Black;
                    TextObject textObj3 = reportDocument.ReportDefinition.ReportObjects["Text35"] as TextObject;
                    textObj3.Color = Color.Black;
                    // Export the report to Excel
                    ExportOptions exportOptions = new ExportOptions();
                    ExcelFormatOptions excelFormatOptions = new ExcelFormatOptions();
                    exportOptions.ExportFormatType = ExportFormatType.Excel;
                    exportOptions.FormatOptions = excelFormatOptions;
                    reportDocument.ExportToDisk(ExportFormatType.Excel, @"D:\excel_report\AnnuvalGameRevenueReport" + financialYear + ".xls");
                }
                else
                {
                    reportDocument.SetParameterValue("Flag", 0);
                    if (FlagColor)
                    {
                        //FindAndSetColor(reportDocument, "Box3", selectedColor);
                        //FindAndSetColor(reportDocument, "Box2", selectedColor);
                    }
                }
                FlagExcel = false;
                FlagColor = false;
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

        public void s_FillReport_Stockusage()
        {
            try
            {ReportDocument reportDocument = new ReportDocument();
            DataSet dsReport = new DataSet();
            DataTable dt_Rpt = new DataTable("Rpt_DT");
            BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
            DmnSearch dmnSearchDetBill = new DmnSearch();
            dmnSearchDetBill.Select = "it.ItemCode_UPC sFld1,it.Itemname sFld2, " +
                "(select Sum(QOH) from tblProductRate where ProductId=it.ItemId) dFld1,SUM(sd.Quantity * inc.Quantity) AS dFld2";
            dmnSearchDetBill.Table = " tblSalesDetails sd	JOIN tblIngredientDetails inc ON inc.ProductID = sd.itemid  JOIN tblItem it ON it.ItemId = inc.ItemId";
            dmnSearchDetBill.WhereNdGroupBy = "where inc.ProductID = sd.itemid AND sd.CreatedDateTime >='" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' AND sd.CreatedDateTime <='" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            dmnSearchDetBill.OrderBy = "Group by it.ItemCode_UPC, it.Itemname,it.ItemId";

            var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);
            using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "dFld1", "dFld2"))
            {
                dt_Rpt.Load(reader);
            }
            dsReport.Tables.Add(dt_Rpt);
            reportDocument.Load(Path + @"\Rpt_Stockusage.rpt");
            reportDocument.SetDataSource(dsReport);
            /* header section*/
            Addressdata = Get_CompantDetails();
            reportDocument.SetParameterValue("Loc_Name", Convert.ToString(Addressdata[0].Name));
            reportDocument.SetParameterValue("City", Convert.ToString(Addressdata[0].City));
            reportDocument.SetParameterValue("Employe_Name", Convert.ToString(FrmLogin.EmployeName));
            reportDocument.SetParameterValue("GSTno", Convert.ToString(Addressdata[0].GstNo));
            string devicename = System.Environment.MachineName;
            reportDocument.SetParameterValue("Device_name", devicename);

            reportDocument.SetParameterValue("MyFromDtm", dtpfrom.Value.ToString(Dtformat));
            reportDocument.SetParameterValue("MyToDatem", dtpto.Value.ToString(Dtformat));

            reportDocument.SetParameterValue("From_re_time", dtp_from_time.Value.ToString(Timeformat));
            reportDocument.SetParameterValue("To_re_time", dtp_to_time.Value.ToString(Timeformat));

            /*End Header section*/
            if (ColorChk == true)
            {
                FindAndSetColor(reportDocument, "Box2", selectedColor);
                FindAndSetColor(reportDocument, "Box3", selectedColor);
            }
            crv_report.ReuseParameterValuesOnRefresh = true;
            crv_report.RefreshReport();
            crv_report.ReportSource = reportDocument; }
            catch (Exception ex)
            {

            }
            
        }

        public void s_FillReport_Expence()
        {
            try
            {  ReportDocument reportDocument = new ReportDocument();
            DataSet dsReport = new DataSet();
            DataTable dt_Rpt = new DataTable("Rpt_DT");
            BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
            DmnSearch dmnSearchDetBill = new DmnSearch();
  
                dmnSearchDetBill.Select = "c.CategoryName sFld1,ep.Description sFld2,em.EmployeeName sFld3,g.Caption sFld4,ep.Amount dFld1";
                dmnSearchDetBill.Table = "tblExpence ep " +
                " left join tblGeneralOptions g on g.GTypeSubId=ep.paymentid and g.GTypeId=2 " +
                " left join tblEmployee em on em.EmployeeId=ep.Empolyeeid " +
                " left join tblCategory_FTI c on c.Fcid=ep.reason ";
            dmnSearchDetBill.WhereNdGroupBy = "where ep.CreatedOn>='" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and ep.CreatedOn <='" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' ";
            dmnSearchDetBill.OrderBy = "order by ep.eid DESC";        
 
            var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);
            //Get_Address();

            using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "sFld3", "sFld4", "dFld1"))
            {
                dt_Rpt.Load(reader);
            }
            dsReport.Tables.Add(dt_Rpt);
            reportDocument.Load(Path + @"\Rpt_Expence.rpt");
            reportDocument.SetDataSource(dsReport);
            /* header section*/
            Addressdata = Get_CompantDetails();
            reportDocument.SetParameterValue("Loc_Name", Convert.ToString(Addressdata[0].Name));
            reportDocument.SetParameterValue("City", Convert.ToString(Addressdata[0].City));
            reportDocument.SetParameterValue("Employe_Name", Convert.ToString(FrmLogin.EmployeName));
            reportDocument.SetParameterValue("GSTno", Convert.ToString(Addressdata[0].GstNo));
            string devicename = System.Environment.MachineName;
            reportDocument.SetParameterValue("Device_name", devicename);

            reportDocument.SetParameterValue("MyFromDtm", dtpfrom.Value.ToString(Dtformat));
            reportDocument.SetParameterValue("MyToDatem", dtpto.Value.ToString(Dtformat));

            reportDocument.SetParameterValue("From_re_time", dtp_from_time.Value.ToString(Timeformat));
            reportDocument.SetParameterValue("To_re_time", dtp_to_time.Value.ToString(Timeformat));

            /*End Header section*/
            if (ColorChk == true)
            {
                FindAndSetColor(reportDocument, "Box2", selectedColor);
                FindAndSetColor(reportDocument, "Box3", selectedColor);
            }
            crv_report.ReuseParameterValuesOnRefresh = true;
            crv_report.RefreshReport();
            crv_report.ReportSource = reportDocument;}
            catch (Exception ex)
            {

            }

           
        }
        public void s_FillReport_Reorder()
        {
            try
            {   string Reorder_Name = "";
            ReportDocument reportDocument = new ReportDocument();
            DataSet dsReport = new DataSet();
            DataTable dt_Rpt = new DataTable("Rpt_DT");
            BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
            DmnSearch dmnSearchDetBill = new DmnSearch();
            if (cmbSublist.SelectedIndex == 0)
            {
                dmnSearchDetBill.Select = "p.itemcode as sFld1,p.Product as sFld2,m.Quantity as sFld3,p.Price as dFld1,0 as dFld2,p.MRP  as dFld3 ";
                dmnSearchDetBill.Table = "tblproduct p left join tblMenu m on m.ProductId=p.ProductId where p.Status = 1 and  m.Quantity >  "+ nud_reorderqty.Text +
                    " union select i.ItemCode_UPC as sFld1,i.Itemname as sFld2,pr.QOH as sFld3,pr.PRate as dFld2,pr.SRate as dFld1,pr.MRP  as dFld3 " +
                    " from tblItem i left join tblProductRate pr on pr.ProductId=i.ItemId";
                dmnSearchDetBill.WhereNdGroupBy = " where i.Status = 1 and (select Sum(QOH) from tblProductRate where ProductId=i.ItemId) >" + nud_reorderqty.Text;
                dmnSearchDetBill.OrderBy = "";
                Reorder_Name = "F&B Reorder Report";
            }
            else if (cmbSublist.SelectedIndex == 1)
            {
                dmnSearchDetBill.Select = "p.itemcode as sFld1,p.product as sFld2,m.Quantity as sFld3,p.Price as dFld1,0 as dFld2,p.MRP  as dFld3 ";
                dmnSearchDetBill.Table = "tblproduct p left join tblMenu m on m.ProductId=p.ProductId ";
                dmnSearchDetBill.WhereNdGroupBy = "where p.Status = 1 and  m.Quantity > "+ nud_reorderqty.Text;
                dmnSearchDetBill.OrderBy = "";
                Reorder_Name = "Product Reorder Report";
            }
            else if (cmbSublist.SelectedIndex == 2)
            {
                dmnSearchDetBill.Select = "i.ItemCode_UPC as sFld1,i.Itemname as sFld2,pr.QOH as sFld3,pr.PRate as dFld1,pr.SRate as dFld2,pr.MRP  as dFld3";
                dmnSearchDetBill.Table = " tblItem i left join tblProductRate pr on pr.ProductId=i.ItemId";
                dmnSearchDetBill.WhereNdGroupBy = " where i.Status = 1 and  (select Sum(QOH) from tblProductRate where ProductId=i.ItemId) >"+ nud_reorderqty.Text + " and saletype=1";
                dmnSearchDetBill.OrderBy = "";
                Reorder_Name = "Item Reorder Report";
            }
            else
            {
                dmnSearchDetBill.Select = "i.ItemCode_UPC as sFld1,i.Itemname as sFld2,pr.QOH as sFld3,pr.PRate as dFld1,pr.SRate as dFld2,pr.MRP  as dFld3";
                dmnSearchDetBill.Table = " tblItem i left join tblProductRate pr on pr.ProductId=i.ItemId";
                dmnSearchDetBill.WhereNdGroupBy = " where i.Status = 1 and  (select Sum(QOH) from tblProductRate where ProductId=i.ItemId) >"+ nud_reorderqty.Text+" and saletype=0";
                dmnSearchDetBill.OrderBy = "";
                Reorder_Name = "Kitchen Item Reorder Report";
            }

            var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);
            //Get_Address();

            using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "sFld3", "sFld4", "dFld1", "dFld2", "dFld3"))
            {
                dt_Rpt.Load(reader);
            }
            dsReport.Tables.Add(dt_Rpt);
            reportDocument.Load(Path + @"\Rpt_ReOrder.rpt");
            reportDocument.SetDataSource(dsReport);
            /* header section*/
            Addressdata = Get_CompantDetails();
            reportDocument.SetParameterValue("Loc_Name", Convert.ToString(Addressdata[0].Name));
            reportDocument.SetParameterValue("City", Convert.ToString(Addressdata[0].City));
            reportDocument.SetParameterValue("Employe_Name", Convert.ToString(FrmLogin.EmployeName));
            reportDocument.SetParameterValue("GSTno", Convert.ToString(Addressdata[0].GstNo));
            string devicename = System.Environment.MachineName;
            reportDocument.SetParameterValue("Device_name", devicename);
            reportDocument.SetParameterValue("Reorderheading", Reorder_Name);
            /*End Header section*/
            if (ColorChk == true)
            {
                FindAndSetColor(reportDocument, "Box2", selectedColor);
                FindAndSetColor(reportDocument, "Box3", selectedColor);
            }
            crv_report.ReuseParameterValuesOnRefresh = true;
            crv_report.RefreshReport();
            crv_report.ReportSource = reportDocument;}
            catch (Exception ex)
            {

            }
          
        }
        
        public void s_FillReport_supplierPurchase()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dt_Rpt = new DataTable("Rpt_DT");
                BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
                DmnSearch dmnSearchDetBill = new DmnSearch();
                dmnSearchDetBill.Select = "v.VendorName as sFld1,pm.EntryNo as sFld2,(select CONVERT(varchar(50), pm.EntryDate, 105)) as sFld3,h.Name as sFld4,h.Email as sFld5,h.Phone as sFld6,h.Address as sFld7,pm.TotalQuantity as dFld1,pm.DiscountAmount as dFld2,pm.NetAmount as dFld3,pm.RcvAmnt as dFld4,pm.BalAmnt as dFld5";
                dmnSearchDetBill.Table = " tblPurchaseMaster pm left join tblVendor v on v.VendorId = pm.LedgerId left join tblPurchaseDetails pd on pd.PurchaseMasterId =pm.PurchaseMasterId left join tblHotelRegistration h on h.BranchId=pm.BranchId";

                if (cmbSublist.SelectedIndex==0)
                {
                    dmnSearchDetBill.WhereNdGroupBy = "where  pm.EntryDate>='" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and" +
    " pm.EntryDate<='" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and  pm.BranchId = " + BranchId;
                }
                else
                {
                    dmnSearchDetBill.WhereNdGroupBy = "where pm.LedgerId = " + cmbSublist.SelectedIndex + " and pm.EntryDate>='" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and" +
" pm.EntryDate<='" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and  pm.BranchId = " + BranchId;
                }
                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);

                if (invoiceBillMasters.Count > 0)
                {
                    invoiceBillMasters[0].FromDate = Convert.ToDateTime(dtpfrom.Text);
                    invoiceBillMasters[0].ToDate = Convert.ToDateTime(dtpto.Text);
                }

                using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "sFld3", "dFld1", "dFld2", "dFld3","dFld4","dFld5"))
                {
                    dt_Rpt.Load(reader);
                }
                dsReport.Tables.Add(dt_Rpt);
                reportDocument.Load(Path + @"\Rpt_Supplier.rpt");
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
            try
            {  foreach (Section section in report.ReportDefinition.Sections)
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
            } }
            catch (Exception ex)
            {

            }
          
        }

        private void cmbreport_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                tlp_sublist.Visible = false;
                tlp_reorder.Visible = false;
                tlp_finyear.Visible = false;
                tabDateTimePanel.Visible = false;
                tblReportCategory.Visible = false;
                if (Convert.ToInt32(cmbreport.SelectedValue) == 27) 
                {
                    tabDateTimePanel.Visible = true;
                    cmbSublist.DataSource = null;
                    tlp_sublist.Visible = true;
                    tblReportCategory.Visible = true;
                    List<string> sublistOptions = new List<string> { "All", "Product", "Item", "Kitchen Item" };
                    cmbSublist.DataSource = sublistOptions;
                    cmbSublist.SelectedIndex = 0;
                }
                else if (Convert.ToInt32(cmbreport.SelectedValue) == 10)
                {
                    tabDateTimePanel.Visible = true;
                    cmbSublist.DataSource = null;
                    tlp_sublist.Visible = true;
                    tblReportCategory.Visible = true;
                    List<string> sublistOptions = new List<string> { "Product Offer", "Combo Offer" };
                    cmbSublist.DataSource = sublistOptions;
                    cmbSublist.SelectedIndex = 0;
                }
                else if (Convert.ToInt32(cmbreport.SelectedValue) == 24)
                {
                    tabDateTimePanel.Visible = true;
                    tlp_reorder.Visible = true;
                    cmbSublist.DataSource = null;
                    tlp_sublist.Visible = true;
                    tblReportCategory.Visible = true;
                    List<string> sublistOptions = new List<string> { "All", "Product", "Item", "Kitchen Item" };
                    cmbSublist.DataSource = sublistOptions;
                    cmbSublist.SelectedIndex = 0;
                }
                else if (Convert.ToInt32(cmbreport.SelectedValue) == 38)
                {
                    tlp_sublist.Visible = false;
                    tlp_reorder.Visible = false;
                    tlp_finyear.Visible = true;
                    tblReportCategory.Visible = false;
                }
                else if (Convert.ToInt32(cmbreport.SelectedValue) == 45)
                {
                    tlp_sublist.Visible = true;
                    tlp_reorder.Visible = false;
                    tlp_finyear.Visible = true;
                    tblReportCategory.Visible = false;
                    cmbSublist.DataSource = null;
                    tlp_sublist.Visible = true;
                    tblReportCategory.Visible = true;
                    List<string> sublistOptions = new List<string> { "Complete", "Pending", "Cancelled" };
                    cmbSublist.DataSource = sublistOptions;
                    cmbSublist.SelectedIndex = 0;
                }
                else if (Convert.ToInt32(cmbreport.SelectedValue) == 46)
                {
                    tlp_sublist.Visible = true;
                    tlp_reorder.Visible = false;
                    tlp_finyear.Visible = true;
                    tblReportCategory.Visible = false;
                    cmbSublist.DataSource = null;

                    tblReportCategory.Visible = true;
                    List<string> sublistOptions = new List<string> { "Complete", "Pending", "Cancelled" };
                    cmbSublist.DataSource = sublistOptions;
                    cmbSublist.SelectedIndex = 0;
                }
                else if (Convert.ToInt32(cmbreport.SelectedValue) == 22)
                {
                    tlp_sublist.Visible = true;
                    tlp_reorder.Visible = false;
                    tlp_finyear.Visible = false;
                    tblReportCategory.Visible = false;
                    cmbSublist.DataSource = null;
                    tabDateTimePanel.Visible = true;
                    tblReportCategory.Visible = true;

                    BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "VendorId,VendorName";
                    objSearch.Table = " tblVendor";
                    objSearch.WhereNdGroupBy = "where Status=1";
                    objSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                    cmbSublist.DataSource = gridSource;
                    cmbSublist.ValueMember = "Id";
                    cmbSublist.DisplayMember = "Value";
                }
                else if (Convert.ToInt32(cmbreport.SelectedValue) == 19)
                {
                    tlp_sublist.Visible = true;
                    tlp_reorder.Visible = false;
                    tlp_finyear.Visible = false;
                    tblReportCategory.Visible = false;
                    cmbSublist.DataSource = null;
                    tabDateTimePanel.Visible = true;
                    tblReportCategory.Visible = true;

                    BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "VendorId,VendorName";
                    objSearch.Table = " tblVendor";
                    objSearch.WhereNdGroupBy = "where Status=1";
                    objSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                    cmbSublist.DataSource = gridSource;
                    cmbSublist.ValueMember = "Id";
                    cmbSublist.DisplayMember = "Value";
                }
                else
                {
                    tabDateTimePanel.Visible = true;
                    tblReportCategory.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }


        }
        public void s_FillReport_Inventory(int typeid)
        {
            try
            { string Inventry_Name = "";
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
            crv_report.ReportSource = reportDocument;}
            catch (Exception ex)
            {

            }
            
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
                if (Convert.ToInt32(cmbSublist.SelectedIndex) == 0)
                {
                    dmnSearchDetBill.Select = " ofm.OfferName as sFld1,ofm.Discountper as dFld1,OfferstartDatetime as sFld2,OfferEndDatetime as sFld3,ofd.ItemCode_UPC sFld4,ofd.Productname sFld5,ofd.Price dFld2,ofd.FinalPrice dFld3";//(select CONVERT(varchar(50),ofm.OfferstartDatetime,105))
                    dmnSearchDetBill.Table = "tblOfferMaster ofm left join tblOfferDetails ofd on ofd.Omid=ofm.Omid and ofd.status = 1 ";
                    dmnSearchDetBill.WhereNdGroupBy = " where ofm.OfferstartDatetime>='" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and ofm.OfferEndDatetime<='" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                                     "and ofm.status = 1 and OfferType=0";
                    var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);

                    if (invoiceBillMasters.Count > 0)
                    {
                        invoiceBillMasters[0].FromDate = Convert.ToDateTime(dtpfrom.Text);
                        invoiceBillMasters[0].ToDate = Convert.ToDateTime(dtpto.Text);
                    }
                    using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "sFld3", "sFld4", "sFld5", "dFld1", "dFld2", "dFld3"))
                    {
                        dt_Rpt.Load(reader);
                    }
                    dsReport.Tables.Add(dt_Rpt);
                    reportDocument.Load(Path + @"\Rpt_OfferList.rpt");
                    reportDocument.SetDataSource(dsReport);
                }
                else
                {
                    dmnSearchDetBill.Select = " ofm.OfferName as sFld1,ofm.Discountper as dFld1,ofm.OfferstartDatetime as sFld2,ofm.OfferEndDatetime as sFld3,ofd.ItemCode_UPC sFld4,ofd.Productname sFld5,ofd.Price dFld2,ofm.FinalPrice dFld3";//(select CONVERT(varchar(50),ofm.OfferstartDatetime,105))
                    dmnSearchDetBill.Table = "tblOfferMaster ofm left join tblOfferDetails ofd on ofd.Omid=ofm.Omid and ofd.status = 1 ";
                    dmnSearchDetBill.WhereNdGroupBy = " where ofm.OfferstartDatetime>='" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and ofm.OfferEndDatetime<='" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "'" +
                                                     "and ofm.status = 1 and OfferType=1";
                    var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);

                    if (invoiceBillMasters.Count > 0)
                    {
                        invoiceBillMasters[0].FromDate = Convert.ToDateTime(dtpfrom.Text);
                        invoiceBillMasters[0].ToDate = Convert.ToDateTime(dtpto.Text);
                    }
                    using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "sFld3", "sFld4", "sFld5", "dFld1", "dFld2", "dFld3"))
                    {
                        dt_Rpt.Load(reader);
                    }
                    dsReport.Tables.Add(dt_Rpt);
                    reportDocument.Load(Path + @"\Rpt_OfferList_Combo.rpt");
                    reportDocument.SetDataSource(dsReport);
                }

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

        public void s_FillReport_SlowmovingItems()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dt_Rpt = new DataTable("Rpt_DT");
                BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
                DmnSearch dmnSearchDetBill = new DmnSearch();
                dmnSearchDetBill.Select = "p.ItemCode AS sFld1,    p.Product AS sFld2,SUM(sd.Quantity) AS dFld1, SUM(sd.NetAmount) AS dFld2," +
                    " CASE WHEN EXISTS (SELECT 1 FROM tblOfferMaster om LEFT JOIN tblOfferDetails od ON od.Omid = om.Omid  AND od.ProductId = p.ProductId WHERE om.OfferType = 1 and om.status=1 and od.status=1 ) THEN 1 ELSE 0 END AS sFld3," +
                    "CASE WHEN EXISTS (SELECT 1 FROM tblOfferMaster om LEFT JOIN tblOfferDetails od ON od.Omid = om.Omid  AND od.ProductId = p.ProductId WHERE om.OfferType = 0 and om.status=1 and od.status=1 ) THEN 1 ELSE 0 END AS sFld4";
                dmnSearchDetBill.Table = " tblSalesDetails sd JOIN tblProduct p ON p.ProductId = sd.ItemId";
                dmnSearchDetBill.WhereNdGroupBy = " where  sd.CreatedDateTime>='" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and  sd.CreatedDateTime<='" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                dmnSearchDetBill.OrderBy = "GROUP BY  p.ItemCode, p.Product, p.ProductId ORDER BY    SUM(sd.Quantity)";
                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);

                if (invoiceBillMasters.Count > 0)
                {
                    invoiceBillMasters[0].FromDate = Convert.ToDateTime(dtpfrom.Text);
                    invoiceBillMasters[0].ToDate = Convert.ToDateTime(dtpto.Text);

                }
                using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "sFld3", "sFld4", "dFld1", "dFld2"))
                {
                    dt_Rpt.Load(reader);
                }
                dsReport.Tables.Add(dt_Rpt);
                reportDocument.Load(Path + @"\Rpt_Solwmovingitem.rpt");
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
        public void s_FillReport_PopularItems()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();
                DataTable dt_Rpt = new DataTable("Rpt_DT");
                BllSearch<DmnRpt_1> bllSearchDetails = new BllSearch<DmnRpt_1>();
                DmnSearch dmnSearchDetBill = new DmnSearch();
                dmnSearchDetBill.Select = "p.ItemCode AS sFld1,    p.Product AS sFld2,SUM(sd.Quantity) AS dFld1, SUM(sd.NetAmount) AS dFld2," +
                    " CASE WHEN EXISTS (SELECT 1 FROM tblOfferMaster om LEFT JOIN tblOfferDetails od ON od.Omid = om.Omid  AND od.ProductId = p.ProductId WHERE om.OfferType = 1 and om.status=1 and od.status=1 ) THEN 1 ELSE 0 END AS sFld3," +
                    "CASE WHEN EXISTS (SELECT 1 FROM tblOfferMaster om LEFT JOIN tblOfferDetails od ON od.Omid = om.Omid  AND od.ProductId = p.ProductId WHERE om.OfferType = 0 and om.status=1 and od.status=1 ) THEN 1 ELSE 0 END AS sFld4";
                dmnSearchDetBill.Table = " tblSalesDetails sd JOIN tblProduct p ON p.ProductId = sd.ItemId";
                dmnSearchDetBill.WhereNdGroupBy = " where  sd.CreatedDateTime>='" + fTime.ToString("yyyy-MM-dd HH:mm:ss") + "' and  sd.CreatedDateTime<='" + tTime.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                dmnSearchDetBill.OrderBy = "GROUP BY  p.ItemCode, p.Product, p.ProductId ORDER BY    SUM(sd.Quantity) DESC"; 
                var invoiceBillMasters = bllSearchDetails.GetSearchData("prdSearch", dmnSearchDetBill);

                if (invoiceBillMasters.Count > 0)
                {
                    invoiceBillMasters[0].FromDate = Convert.ToDateTime(dtpfrom.Text);
                    invoiceBillMasters[0].ToDate = Convert.ToDateTime(dtpto.Text);

                }
                using (var reader = ObjectReader.Create(invoiceBillMasters, "sFld1", "sFld2", "sFld3", "sFld4","dFld1", "dFld2"))
                {
                    dt_Rpt.Load(reader);
                }
                dsReport.Tables.Add(dt_Rpt);
                reportDocument.Load(Path + @"\Rpt_PopularItem.rpt");
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
            try
            {
                cmbreport.DataSource = null;
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = " G.GTypeSubId as Id, G.Caption as Value";
                objSearch.Table = " tblGeneralOptions G";
                switch (Reportid)
                {
                    case (int)ReportType.salesReportItem:
                        objSearch.WhereNdGroupBy = " WHERE G.GTypeId= 51 and GTypeSubId in(47,48,38)";
                        break;

                    case (int)ReportType.inventoryReportItem:
                        objSearch.WhereNdGroupBy = " WHERE G.GTypeId= 51 and GTypeSubId in(24,27,37)";
                        break;
                    case (int)ReportType.promotionsDiscountsItem:
                        objSearch.WhereNdGroupBy = " WHERE G.GTypeId= 51 and GTypeSubId in(10)";
                        break;
                    case (int)ReportType.employeePerformanceItem:
                        objSearch.WhereNdGroupBy = " WHERE G.GTypeId= 51 and GTypeSubId in(11)";
                        break;
                    case (int)ReportType.expenseReportItem:
                        objSearch.WhereNdGroupBy = " WHERE G.GTypeId= 51 and GTypeSubId in(25)";
                        break;
                    case (int)ReportType.reservationSeatingItem:
                        objSearch.WhereNdGroupBy = " WHERE G.GTypeId= 51 and GTypeSubId in(46,45)";
                        break;
                    case (int)ReportType.Purchasereportitem:
                        objSearch.WhereNdGroupBy = " WHERE G.GTypeId= 51 and GTypeSubId in(20,23)";
                        break;
                    case (int)ReportType.MenuAnalysisReports:
                        objSearch.WhereNdGroupBy = " WHERE G.GTypeId= 51 and GTypeSubId in(7,8)";
                        break;
                    case (int)ReportType.TableTurnoverReports:
                        objSearch.WhereNdGroupBy = " WHERE G.GTypeId= 51 and GTypeSubId in(44)";
                        break;
                    case (int)ReportType.VendorReports:
                        objSearch.WhereNdGroupBy = " WHERE G.GTypeId= 51 and GTypeSubId in(22,19)";
                        break;
                }
                objSearch.OrderBy = "order by Id";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);

                cmbreport.DataSource = gridSource;
                cmbreport.ValueMember = "Id";
                cmbreport.DisplayMember = "value";
            }
            catch(Exception ex)
            {

            }
            

        }
        
    }
}
