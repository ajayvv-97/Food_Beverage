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

namespace FoodandBeverage.Forms
{
    public partial class FrmDashboardReport : Form
    {
        public FrmDashboardReport()
        {
            InitializeComponent();
        }

        private void FrmDashboardReport_Load(object sender, EventArgs e)
        {
            Filldashboard();
        }
        private void Filldashboard()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "COUNT(*) AS Field1," +
                    " SUM(CASE WHEN CONVERT(DATE, EntryDate) = CONVERT(DATE, '" + dtp_dashboard.Value.ToString("yyyy-MM-dd") + "') THEN 1 ELSE 0 END) AS Field2," +
                    " SUM(CASE WHEN CONVERT(DATE, EntryDate) = CONVERT(DATE, '" + dtp_dashboard.Value.ToString("yyyy-MM-dd") + "') THEN netamount ELSE 0 END) AS Field3," +
                    " SUM(netamount) AS Field4";
                dmnSearch.Table = "tblSalesMaster";
                dmnSearch.WhereNdGroupBy = " where Status=1 ";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    lblsaletotal.Text = Convert.ToString(gridSource[0].Field4);
                    lbltotalsaleno.Text = Convert.ToString(gridSource[0].Field1);
                    lbldinetotal.Text = Convert.ToString(gridSource[0].Field3);
                    lbldinetotalno.Text = Convert.ToString(gridSource[0].Field2);

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
