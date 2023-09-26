using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using CrystalDecisions.CrystalReports.Engine;
using Domain;
using MetroFramework;
using MetroFramework.Controls;

namespace MarginPOSNew.Forms.Reports
{
    public partial class FrmSalesBill : MetroFramework.Forms.MetroForm
    {
        #region Declaration
        ReportDocument crySaleInvPos = new ReportDocument();
        long BranchId = Convert.ToInt64(FrmDashboard.BranchId);
        long _CompanyId = Convert.ToInt64(FrmDashboard.CompanyId);
        long UserId = Convert.ToInt64(FrmDashboard.UserId);
        public long _salId;
        #endregion
        public FrmSalesBill(long salId)
        {
            InitializeComponent();
            _salId = salId;
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private void FrmSalesBill_Load(object sender, EventArgs e)
        {
            BllSearch<DmnCompany> bllSearchCompany = new BllSearch<DmnCompany>();
            DmnSearch objSearchCompany = new DmnSearch();
            objSearchCompany = new DmnSearch();
            objSearchCompany.Select = " CompanyName As company";
            objSearchCompany.Table = " tblCompany ";
            var lstCompany = bllSearchCompany.GetSearchData("prdSearch", objSearchCompany);
            DataSet dst = new System.Data.DataSet("dstSalesInvPos");
            DataTable dtComp = new DataTable("dtComp");
            //a.Fill(dtComp);
            //dst.Tables.Add(dtComp);
            //var dt= ToDataTable<DmnCompany>(List<DmnCompany> lstCompany);
            


            BllSearch<DmnBranch> bllSearchBranch = new BllSearch<DmnBranch>();
            DmnSearch objSearchBranch = new DmnSearch();
            objSearchBranch = new DmnSearch();
            objSearchBranch.Select = " BranchName as branch,BranchAddress as address, CONCAT(Email,Website) as emweb,Telephone as tel,GSTNo as GSTNO";
            objSearchBranch.Table = " tblCompany ";
            var lstBranch = bllSearchBranch.GetSearchData("prdSearch", objSearchBranch);
            DataTable dtBranch = new DataTable("dtBranch");

            BllSearch<DmnSalesMaster> bllSearchMaster = new BllSearch<DmnSalesMaster>();
            DmnSearch objSearchMaster = new DmnSearch();
            objSearchMaster = new DmnSearch();
            objSearchMaster.Select = " CompanyName As company";
            objSearchMaster.Table = " tblCompany ";
            var lstMaster = bllSearchMaster.GetSearchData("prdSearch", objSearchMaster);
            DataTable dtSal = new DataTable("dtSal");


            BllSearch<DmnSalesDetails> bllSearchDetails = new BllSearch<DmnSalesDetails>();
            DmnSearch objSearchDetails = new DmnSearch();
            objSearchDetails = new DmnSearch();
            objSearchDetails.Select = "*";
            objSearchDetails.Table = " tblPurchaseDetails ";
            objSearchDetails.WhereNdGroupBy = " where PurchaseMasterId=" + _salId;
            var lstDetails = bllSearchDetails.GetSearchData("prdSearch", objSearchDetails);
            DataSet dstdet = new System.Data.DataSet("dstSalesInvPos");
            DataTable dtSaldet = new DataTable("dtSaldet");

            #region Direct Print
            DialogResult result = MessageBox.Show("Want New Bill?", "Slip", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                crvSalesBill.ReportSource = crySaleInvPos;


                if (crySaleInvPos.PrintOptions.NoPrinter == false)
                {
                    crySaleInvPos.PrintToPrinter(2, true, 0, 0);
                }
                else
                {
                    int allowdedFormat = (int)(CrystalDecisions.Shared.ViewerExportFormats.PdfFormat);
                    crvSalesBill.AllowedExportFormats = allowdedFormat;
                    crvSalesBill.ReportSource = crySaleInvPos;
                    crvSalesBill.ReuseParameterValuesOnRefresh = true;
                    crvSalesBill.RefreshReport();
                    crvSalesBill.PrintMode = CrystalDecisions.Windows.Forms.PrintMode.PrintToPrinter;
                    crvSalesBill.PrintReport();
                }
            }
            if (result == DialogResult.No)
            {
                crvSalesBill.ReportSource = crySaleInvPos;
            }
            #endregion
        }
    }
}
