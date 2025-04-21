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
        public int Drstaffid;
        public FrmZout(int value, int Drawerstaffid)
        {
            flag = value;
            Drstaffid = Drawerstaffid;
            
        }
        ClsGeneral clsgen = new ClsGeneral();
        public void PrintAll()
        {
            try
            {

                if (flag == 1)
                {
                    
                    
                }
                else if (flag == 2)
                {
                    PrintYout();
                   
                }

                else if (flag == 3)
                { }
                   
            }
            catch (Exception ex)
            {
                clsgen.LogException("ZOut page FrmZout_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
            }
        }
        #region Yout
        public void PrintYout()
        {
            try
            {
                ReportDocument reportDocument = new ReportDocument();
                DataSet dsReport = new DataSet();

                string Drawerassigntime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");           
                DataTable DT_Products = new DataTable("DT_Products");


       



                #region Drawer Assign time
                DmnSearch dmnSearchDetBill = new DmnSearch();
                BllSearch<Dmndrawer> bllSearch = new BllSearch<Dmndrawer>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "CreatedDateTime ";
                dmnSearch.Table = "tblDrawerAssign ";
                dmnSearch.WhereNdGroupBy = "where DName='" + System.Environment.MachineName + "' and Status=1";
                var itemsource = bllSearch.GetSearchData("prdsearch", dmnSearch);
                if (itemsource.Count > 0)
                {
                    Drawerassigntime = itemsource[0].CreatedDateTime.ToString("yyyy-MM-dd HH:mm");
                }
                #endregion

                #region Products
                DmnSearch dmnSearchProduct = new DmnSearch();
                dmnSearchProduct.Select = "SUM(d.Quantity) AS TotalQuantity,d.ItemName as Itemname, SUM(d.DiscountAmount) AS TotalDiscount, sum(d.NetAmount) TotalAmount,sum(d.TaxAmount) as Totaltax";
                dmnSearchProduct.Table = "tblSalesDetails d LEFT JOIN tblSalesMaster m ON d.SalesMasterId = m.SalesMasterId ";
                dmnSearchProduct.WhereNdGroupBy = "WHERE m.Status=1 and d.Status= 1 and m.CreatedBy=" + FrmDashboard.UserId + " and m.CreatedDateTime > '" + Drawerassigntime + "' AND m.CreatedDateTime <  SYSDATETIME() GROUP BY d.ItemId, d.ItemName";
                var ProductSaleResult = bllSearch.GetSearchData("prdSearch", dmnSearchProduct);
                using (var reader = ObjectReader.Create(ProductSaleResult, "TotalQuantity", "Itemname", "TotalDiscount", "TotalAmount", "Totaltax", "ProductType", "ProductId"))
                {
                    DT_Products.Load(reader);
                }


                #endregion





            }
            catch (Exception ex)
            {
                clsgen.LogException("ZOut Page PrintXout()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.homepage));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
    }
}
