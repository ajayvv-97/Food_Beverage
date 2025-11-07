using BLL;
using Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using FoodandBeverage.Forms;
using System.IO;
using CrystalDecisions.Shared;
using System.Runtime.InteropServices;
using FastMember;
using FoodandBeverage.Classes;
using FoodandBeverage.Interfaces;
using System.Linq;

namespace FoodandBeverage.SaleBill
{
    public partial class FrmZout : Form
    {
        public int flag;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvout;
        public string RPTPath = Convert.ToString(FrmDashboard.SaleBillPath);

  
        public FrmZout()
        {
            InitializeComponent();
        }
        ClsGeneral clsgen = new ClsGeneral();

        private void FrmZout_Load(object sender, EventArgs e)
        {
            PrintYout();
        }
     
        public void PrintYout()
        {
            try
            {

                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();

                string Drawerassigntime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                string Drawerassignouttime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                // DataTable DT_Cardsale = new DataTable("DT_Cardsale");

                DataTable DT_Header = new DataTable("DT_Header");
                DataTable DT_Products = new DataTable("DT_Products");
                DataTable DT_Drawer = new DataTable("DT_Drawer");
                DataTable DT_Geninfo = new DataTable("DT_Geninfo");

                BllSearch<DmnSaleBillHeaders> bllSearchHeaders = new BllSearch<DmnSaleBillHeaders>();
                BllSearch<DmnOutCard> bllProducts = new BllSearch<DmnOutCard>();
                BllSearch<DmnOutDrawer> bllDrawer = new BllSearch<DmnOutDrawer>();
                BllSearch<DmnOutGenInfo> bllGeninfo = new BllSearch<DmnOutGenInfo>();

                #region Drawer Assign time
                DmnSearch dmnSearchDetBill = new DmnSearch();
                BllSearch<Dmndrawer> bllSearch = new BllSearch<Dmndrawer>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "CreatedDateTime,ModifiedDateTime";
                dmnSearch.Table = "tblDrawerAssign ";
                dmnSearch.WhereNdGroupBy = "where DName='" + System.Environment.MachineName + "' and Status=1";
                var itemsource = bllSearch.GetSearchData("prdsearch", dmnSearch);
                if (itemsource.Count > 0)
                {
                    Drawerassigntime = itemsource[0].CreatedDateTime.ToString("yyyy-MM-dd HH:mm");
                    Drawerassignouttime = itemsource[0].ModifiedDateTime.ToString("yyyy-MM-dd HH:mm");
                }
                #endregion

                #region Hotel Header
                DmnSearch dmnSearchInvoiceHeaders = new DmnSearch();
                dmnSearchInvoiceHeaders.Select = "Name,Address,Email,Phone,GSTNO";
                dmnSearchInvoiceHeaders.Table = "tblHotelRegistration";
                dmnSearchInvoiceHeaders.WhereNdGroupBy = " where Branchid=" + FrmDashboard.BranchId;
                var invoiceBillHeaders = bllSearchHeaders.GetSearchData("prdSearch", dmnSearchInvoiceHeaders);
                using (var reader = ObjectReader.Create(invoiceBillHeaders, "Name", "Address", "Email", "Phone", "GSTNo"))
                {
                    DT_Header.Load(reader);
                }
                #endregion

                #region Products
                DmnSearch dmnSearchProduct = new DmnSearch();
                dmnSearchProduct.Select = "SUM(d.Quantity) AS TotalQuantity,d.ItemName as Itemname, SUM(d.DiscountAmount) AS TotalDiscount, sum(d.NetAmount) TotalAmount,sum(d.TaxAmount) as Totaltax";
                dmnSearchProduct.Table = "tblSalesDetails d LEFT JOIN tblSalesMaster m ON d.SalesMasterId = m.SalesMasterId and m.Status=1" +
                " LEFT JOIN tblGeneralOptions g ON d.ProductType =g.GTypeSubValue1 and GTypeId=53 WHERE d.Status= 1 and m.CreatedBy=" + FrmDashboard.UserId + " and m.CreatedDateTime >= '" + Drawerassigntime + "' AND m.CreatedDateTime <= SYSDATETIME() GROUP BY d.ItemId, d.ItemName ,g.Caption";
                dmnSearchProduct.WhereNdGroupBy = "";
                var ProductSaleResult = bllProducts.GetSearchData("prdSearch", dmnSearchProduct);
                using (var reader = ObjectReader.Create(ProductSaleResult, "TotalQuantity", "Itemname", "TotalDiscount", "TotalAmount", "Totaltax"))
                {
                    DT_Products.Load(reader);
                }
                #endregion

                #region Drawer
                DmnSearch dmnSearchDrawer = new DmnSearch();
                dmnSearchDrawer.Select = " 0 as InitialAmount,0 as LoanAmount,0 as Drops,sum(NetAmount) as TotalAmount";
                dmnSearchDrawer.Table = "tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and PaymentMode = 1 and CreatedBy = " + FrmDashboard.UserId + "and status=1 and CreatedDateTime > '" + Drawerassigntime + "' AND CreatedDateTime < SYSDATETIME() " +
                    "union select InitilAmount as InitialAmount,LoanAmount,Drops,0 TotalAmount from tblDrawerAssign where status=1 and CreatedBy = " + FrmDashboard.UserId + " and CreatedDateTime > '" + Drawerassigntime + "' AND CreatedDateTime < SYSDATETIME() ";
                dmnSearchDrawer.WhereNdGroupBy = "";
                var DrawerResult = bllDrawer.GetSearchData("prdSearch", dmnSearchDrawer);
                DmnOutDrawer Dresult = DrawerResult
                    .GroupBy(row => 1)
                    .Select(group => new DmnOutDrawer
                    {
                        InitialAmount = group.Sum(row => row.InitialAmount),
                        LoanAmount = group.Sum(row => row.LoanAmount),
                        Drops = group.Sum(row => row.Drops),
                        TotalAmount = group.Sum(row => row.TotalAmount),
                        TotalCardCashRefund = group.Sum(row => row.TotalCardCashRefund),
                        TotalCashRefund = group.Sum(row => row.TotalCashRefund)
                    })
                    .FirstOrDefault();

                using (var reader = ObjectReader.Create(new List<DmnOutDrawer> { Dresult }, "InitialAmount", "LoanAmount", "Drops", "TotalAmount"))
                {
                    DT_Drawer.Load(reader);
                }
                #endregion

                #region General Information
                DmnSearch dmnSearchGeninfo = new DmnSearch();
                dmnSearchGeninfo.Select = "[CREDIT CARD] as TotalCreditAmt,[DEBIT CARD] as TotalDebitAmt, [UPI] as TotalUPI, [BANKING ONLINE] as TotalBankonline, [CASH] as TotalCash, [OTHERS] as Others,[Discount] as TotalDiscount,[TotalTax] as Totaltax,[NetAmount] as TotalAmount,[TotalQuantity] as TotalQuantity";
                dmnSearchGeninfo.Table = "(select g.Caption as Caption,Sum(sm.NetAmount) as amount from tblSalesMaster sm left join tblGeneralOptions g on g.GTypeSubId =sm.PaymentMode and g.GTypeId = 2 where sm.status = 1 and sm.BranchId = " + FrmDashboard.BranchId + " and sm.CreatedBy = " + FrmDashboard.UserId + " and sm.CreatedDateTime >= '" + Drawerassigntime + "' AND sm.CreatedDateTime <= SYSDATETIME() GROUP BY  g.Caption,sm.NetAmount" +
                    " Union all Select 'Discount' Caption ,Sum(DiscountAmount) as amount from tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and Status=1  and CreatedBy = " + FrmDashboard.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME()" +
                    " Union all Select 'TotalTax' Caption ,Sum(TotalTax) as amount  from tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and Status=1  and CreatedBy = " + FrmDashboard.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME()" +
                    " Union all Select 'TotalQuantity' Caption ,Sum([TotalQuantity]) as totqty  from tblSalesMaster WHERE BranchId = " + FrmDashboard.BranchId + " and Status=1  and CreatedBy = " + FrmDashboard.UserId + " and CreatedDateTime >= '" + Drawerassigntime + "' AND CreatedDateTime <= SYSDATETIME() and PaymentMode!= " + Convert.ToInt32(PaymentType.COMPLEMENTRY) +                   
                  
                    ")ps PIVOT (SUM(amount)FOR Caption IN([CREDIT CARD],[DEBIT CARD],[UPI],[BANKING ONLINE],[CASH],[OTHERS],[Discount],[TotalTax],[NetAmount],[TotalQuantity])) AS pvt  ";
                dmnSearchGeninfo.WhereNdGroupBy = "";
                var GenInfoResult = bllGeninfo.GetSearchData("prdSearch", dmnSearchGeninfo);
                using (var reader = ObjectReader.Create(GenInfoResult, "Others", "TotalCash", "TotalBankonline", "TotalUPI", "TotalDebitAmt", "TotalCreditAmt", "TotalQuantity", "TotalDiscount", "Totaltax", "TotalAmount"))
                {
                    DT_Geninfo.Load(reader);
                }
                #endregion

                dsReport.Tables.Add(DT_Products);
                dsReport.Tables.Add(DT_Header);
                dsReport.Tables.Add(DT_Drawer);
                dsReport.Tables.Add(DT_Geninfo);


                reportDocument.Load(RPTPath + @"\CR_Zout.rpt");
                reportDocument.SetDataSource(dsReport);

                
                //reportDocument.SetParameterValue("OutHeading", "Yout");

                crvout.ReuseParameterValuesOnRefresh = true;
                crvout.RefreshReport();
                crvout.ReportSource = reportDocument;
                

                //if (XYDalvalue == false || DalDalvalue == false)
                reportDocument.PrintToPrinter(1, false, 0, 0);

            }
            catch (Exception ex)
            {
                clsgen.LogException("ZOut Page PrintZout()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void InitializeComponent()
        {
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.crvout = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(704, 422);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // crvout
            // 
            this.crvout.ActiveViewIndex = -1;
            this.crvout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvout.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvout.Location = new System.Drawing.Point(0, 0);
            this.crvout.Name = "crvout";
            this.crvout.Size = new System.Drawing.Size(704, 422);
            this.crvout.TabIndex = 1;
            // 
            // FrmZout
            // 
            this.ClientSize = new System.Drawing.Size(704, 422);
            this.Controls.Add(this.crvout);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "FrmZout";
            this.ResumeLayout(false);

        }
    }
}
