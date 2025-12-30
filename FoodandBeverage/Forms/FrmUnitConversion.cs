using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Forms.UserControls;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using FoodandBeverage.Interfaces;
using System.Collections.Generic;
using FoodandBeverage.Classes;

namespace FoodandBeverage.Forms
{
    public partial class FrmUnitConversion : Form
    {
        public FrmUnitConversion()
        {
            InitializeComponent();
        }


        private void FrmUnitConversion_Load(object sender, EventArgs e)
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = " UId,UnitName";
            objSearch.Table = " tblUnit";
            objSearch.WhereNdGroupBy = "where status=1";
            objSearch.OrderBy = "";
            var gridSource1 = bllSearch.GetSearchData("prdComboFill", objSearch);
            var gridSource2 = bllSearch.GetSearchData("prdComboFill", objSearch);
            cmb_fromunit.DataSource = gridSource1;
            cmb_fromunit.ValueMember = "Id";
            cmb_fromunit.DisplayMember = "Value";

            cmb_tounit.DataSource = gridSource2;
            cmb_tounit.ValueMember = "Id";
            cmb_tounit.DisplayMember = "Value";
        }
    }
}
