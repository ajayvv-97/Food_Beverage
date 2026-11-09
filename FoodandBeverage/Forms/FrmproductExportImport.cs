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
using ClosedXML.Excel;

namespace FoodandBeverage.Forms
{
    public partial class FrmproductExportImport : Form
    {
        public FrmproductExportImport()
        {
            InitializeComponent();
        }
        ClsGeneral clsgen = new ClsGeneral();

        private void FrmproductExportImport_Load(object sender, EventArgs e)
        {
            try
            {
                List<DmnComboBox> itemSource = new List<DmnComboBox>();

                itemSource.Add(
                    new DmnComboBox()
                    {
                        Id = 0,
                        Value = "Item"
                    });

                itemSource.Add(
                    new DmnComboBox()
                    {
                        Id = 1,
                        Value = "Products"
                    });

                comproduct.DataSource = itemSource;
                comproduct.DisplayMember = "Value";
                comproduct.ValueMember = "Id";

                comproduct.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            int PType = Convert.ToInt32(comproduct.SelectedValue);
            comproduct.Enabled = false;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Files|*.xlsx";
            if (PType == 0)
                sfd.FileName = "Item_Import_Template.xlsx";

            else if (PType == 1)
                sfd.FileName = "Product_Import_Template.xlsx";
            if (sfd.ShowDialog() == DialogResult.OK)
            {

                using (XLWorkbook wb = new XLWorkbook())
                {
                    var validationSheet = wb.Worksheets.Add("TemplateInfo");

                    validationSheet.Cell("A1").Value = "MIDASFUN_TEMPLATE";
                    validationSheet.Cell("A2").Value = PType;
                    validationSheet.Cell("A3").Value = "V1.0";

                    validationSheet.Hide();

                    #region Items
                    if (PType == 0)
                    {
                        var ws = wb.Worksheets.Add("Item Registration");

                        // =========================
                        // HEADER
                        // =========================
                        ws.Cell(1, 1).Value = "Item Name";
                        ws.Cell(1, 2).Value = "Barcode";
                        ws.Cell(1, 3).Value = "Category";
                        ws.Cell(1, 4).Value = "Type";
                        ws.Cell(1, 5).Value = "PurchaseRate";
                        ws.Cell(1, 6).Value = "SaleRate";
                        ws.Cell(1, 7).Value = "MRP";
                        ws.Cell(1, 8).Value = "LockSaleRate";
                        ws.Cell(1, 9).Value = "Unit";
                        ws.Cell(1, 10).Value = "BaseUnit";
                        ws.Cell(1, 11).Value = "ConversionFactor";
                        ws.Cell(1, 12).Value = "Tax";
                        ws.Cell(1, 13).Value = "TaxType";
                        ws.Cell(1, 14).Value = "TaxPercentage";
                        ws.Cell(1, 15).Value = "HSNCode";
                        ws.Cell(1, 16).Value = "Is Expiry";


                        // =========================
                        // TAX TYPE DROPDOWN
                        // =========================
                        List<string> TaxtypeList = new List<string>()
                        {
                        "GST",
                        "VAT"
                        };

                        var taxtypeSheet = wb.Worksheets.Add("TaxTypeList");

                        for (int i = 0; i < TaxtypeList.Count; i++)
                        {
                            taxtypeSheet.Cell(i + 1, 1).Value = TaxtypeList[i];
                        }

                        taxtypeSheet.Hide();

                        var taxtypeRange = ws.Range("M2:M1000");
                        var validation = taxtypeRange.SetDataValidation();
                        taxtypeRange.SetDataValidation()
                                    .List(taxtypeSheet.Range("A1:A" + TaxtypeList.Count), true);
                        check_Dropdownvalidation(validation);
                        // =========================
                        // TAX PERCENT DROPDOWN
                        // =========================
                        Dictionary<string, int> taxPercentList = GetTaxTypeList();

                        var gstVatSheet = wb.Worksheets.Add("GSTVATLIST");

                        var taxPercentNames = taxPercentList.Keys.ToList();

                        for (int i = 0; i < taxPercentNames.Count; i++)
                        {
                            gstVatSheet.Cell(i + 1, 1).Value = taxPercentNames[i];
                        }

                        gstVatSheet.Hide();

                        var taxPercentRange = ws.Range("N2:N1000");
                        validation = taxtypeRange.SetDataValidation();
                        taxPercentRange.SetDataValidation()
                                       .List(gstVatSheet.Range("A1:A" + taxPercentList.Count), true);
                        check_Dropdownvalidation(validation);
                        // =========================
                        // TAX DROPDOWN
                        // =========================
                        List<string> taxList = new List<string>()
    {
        "Inclusive",
        "Exclusive"
    };

                        var taxSheet = wb.Worksheets.Add("TaxList");

                        for (int i = 0; i < taxList.Count; i++)
                        {
                            taxSheet.Cell(i + 1, 1).Value = taxList[i];
                        }

                        taxSheet.Hide();

                        var taxRange = ws.Range("L2:L1000");
                        validation = taxRange.SetDataValidation();
                        taxRange.SetDataValidation()
                                .List(taxSheet.Range("A1:A" + taxList.Count), true);
                        check_Dropdownvalidation(validation);
                        // =========================
                        // SUB CATEGORY DROPDOWN
                        // =========================
                        Dictionary<string, int> subCategoryList =
                            GetcategoryList();

                        var subCategorySheet =
                            wb.Worksheets.Add("CategoryList");

                        var subCategoryNames =
                            subCategoryList.Keys.ToList();

                        for (int i = 0; i < subCategoryNames.Count; i++)
                        {
                            subCategorySheet.Cell(i + 1, 1).Value =
                                subCategoryNames[i];
                        }

                        subCategorySheet.Hide();

                        var subCategoryRange = ws.Range("C2:C1000");
                        validation = subCategoryRange.SetDataValidation();
                        subCategoryRange.SetDataValidation()
                                        .List(
                                            subCategorySheet.Range(
                                                "A1:A" + subCategoryList.Count),
                                            true);
                        check_Dropdownvalidation(validation);
                        // =========================
                        // Sale/NonSale Type
                        // =========================
                        List<string> Type = new List<string>()
    {
        "Non SaleType",
        "SaleType"
    };

                        var typeList = wb.Worksheets.Add("Types");

                        for (int i = 0; i < Type.Count; i++)
                        {
                            typeList.Cell(i + 1, 1).Value = Type[i];
                        }

                        typeList.Hide();

                        var typeRange = ws.Range("D2:D1000");
                        validation = typeRange.SetDataValidation();
                        typeRange.SetDataValidation()
                                .List(typeList.Range("A1:A" + taxList.Count), true);
                        check_Dropdownvalidation(validation);

                        // =========================
                        // Unit
                        // =========================

                        Dictionary<string, int> UnitList = GetUnitList();

                        var UnitSheet = wb.Worksheets.Add("UnitLIST");

                        var UnitNames = UnitList.Keys.ToList();

                        for (int i = 0; i < UnitNames.Count; i++)
                        {
                            UnitSheet.Cell(i + 1, 1).Value = UnitNames[i];
                        }

                        UnitSheet.Hide();

                        var UnitRange = ws.Range("I2:I1000");
                        validation = UnitRange.SetDataValidation();
                        UnitRange.SetDataValidation()
                                       .List(UnitSheet.Range("A1:A" + UnitList.Count), true);
                        check_Dropdownvalidation(validation);

                        // =========================
                        // Base Unit
                        // =========================

                        Dictionary<string, int> BaseUnitList = GetUnitList();

                        var BaseUnitSheet = wb.Worksheets.Add("BaseUnitLIST");

                        var BaseUnitNames = BaseUnitList.Keys.ToList();

                        for (int i = 0; i < BaseUnitNames.Count; i++)
                        {
                            BaseUnitSheet.Cell(i + 1, 1).Value = BaseUnitNames[i];
                        }

                        BaseUnitSheet.Hide();

                        var BaseUnitRange = ws.Range("J2:J1000");
                        validation = BaseUnitRange.SetDataValidation();
                        BaseUnitRange.SetDataValidation()
                                       .List(BaseUnitSheet.Range("A1:A" + BaseUnitList.Count), true);
                        check_Dropdownvalidation(validation);


                        SetWholeNumberValidation(ws.Range("K2:K1000"));
                        SetDecimalValidation(ws.Range("E2:E1000"));
                        SetDecimalValidation(ws.Range("F2:F1000"));
                        SetDecimalValidation(ws.Range("G2:G1000"));

                        // =========================
                        // SAMPLE ROW
                        // =========================
                     


                        // =========================
                        // DESIGN
                        // =========================
                        var headerRange = ws.Range("A1:P1");

                        headerRange.Style.Font.Bold = true;
                        headerRange.Style.Fill.BackgroundColor = XLColor.DarkBlue;
                        headerRange.Style.Font.FontColor = XLColor.White;

                        ws.Columns().AdjustToContents();
                        // =========================
                        // SAVE
                        // =========================
                        wb.SaveAs(sfd.FileName);
                    }
                    #endregion
                }

                MessageBox.Show("Sample Excel Exported Successfully");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {

        }

        private void btn_DataSync_Click(object sender, EventArgs e)
        {

        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {


            dgvItem.Rows.Clear();

            // Reset Product Type
            comproduct.SelectedIndex = 0;
            comproduct.Enabled = true;

            // Focus back to Product Type
            comproduct.Focus();
        }

        private void btn_cls_gmsbclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region validation
        private void check_Dropdownvalidation(IXLDataValidation validation)
        {
            validation.IgnoreBlanks = false;

            validation.InCellDropdown = true;

            validation.ShowErrorMessage = true;

            validation.ErrorTitle = "Invalid Entry";

            validation.ErrorMessage =
                "Please select value from dropdown only.";

            validation.ErrorStyle =
                XLErrorStyle.Stop;
        }
        private void SetDecimalValidation(IXLRange range)
        {
            var validation = range.SetDataValidation();

            validation.Decimal.Between(0, 99999999);

            validation.IgnoreBlanks = true;

            validation.ShowErrorMessage = true;

            validation.ErrorTitle = "Invalid Number";

            validation.ErrorMessage =
                "Only numeric or decimal values are allowed.";

            validation.ErrorStyle = XLErrorStyle.Stop;
        }
        private void SetWholeNumberValidation(IXLRange range)
        {
            var validation = range.SetDataValidation();

            validation.WholeNumber.Between(0, 999999);

            validation.IgnoreBlanks = true;

            validation.ShowErrorMessage = true;

            validation.ErrorTitle = "Invalid Number";

            validation.ErrorMessage =
                "Only whole numbers are allowed.";

            validation.ErrorStyle = XLErrorStyle.Stop;
        }

        #endregion
        #region ComboFill
        private Dictionary<string, int> GetTaxTypeList()
        {
            try
            {
                Dictionary<string, int> taxPercentList =
                new Dictionary<string, int>();

                BllSearch<DmnComboBox> bllSearch =
                    new BllSearch<DmnComboBox>();

                DmnSearch objSearch = new DmnSearch();

                objSearch.Select = @"
        DISTINCT TaxSettingsId as Id,

        CASE 
            WHEN TaxType='GST'
                THEN CAST(IgstPrcnt AS VARCHAR)

            WHEN TaxType='VAT'
                THEN CAST(VatPrcnt AS VARCHAR)

        END AS Value";

                objSearch.Table = "tblTaxSettings";

                objSearch.WhereNdGroupBy =
                    "WHERE Status=1";

                objSearch.OrderBy =
                    "ORDER BY Value";

                var taxResult =
                    bllSearch.GetSearchData(
                        "prdSearch",
                        objSearch);

                foreach (var item in taxResult)
                {
                    if (!taxPercentList.ContainsKey(item.Value))
                    {
                        taxPercentList.Add(
                            item.Value,
                            Convert.ToInt32(item.Id));
                    }
                }

                return taxPercentList;
            }
            catch (Exception ex)
            {
                clsgen.LogException("ProductExportImport GetTaxTypeList()", ex,
                   FrmLogin.EmployeName,
                   FrmLogin.Designation,
                   Convert.ToInt32(formsenum.productExportImport));
                return new Dictionary<string, int>();
            }

        }
        private Dictionary<string, int> GetUnitList()
        {
            try
            {
                Dictionary<string, int> UnitList =
                new Dictionary<string, int>();

                BllSearch<DmnComboBox> bllSearch =
                    new BllSearch<DmnComboBox>();

                DmnSearch objSearch = new DmnSearch();

                objSearch.Select = @"
        DISTINCT UId as Id,UnitName AS Value";

                objSearch.Table = "tblUnit";

                objSearch.WhereNdGroupBy =
                    "WHERE Status=1";

                objSearch.OrderBy =
                    "ORDER BY Value";

                var Result =
                    bllSearch.GetSearchData(
                        "prdSearch",
                        objSearch);

                foreach (var item in Result)
                {
                    if (!UnitList.ContainsKey(item.Value))
                    {
                        UnitList.Add(
                            item.Value,
                            Convert.ToInt32(item.Id));
                    }
                }

                return UnitList;
            }
            catch (Exception ex)
            {
                clsgen.LogException("ProductExportImport GetUnitList()", ex,
                   FrmLogin.EmployeName,
                   FrmLogin.Designation,
                   Convert.ToInt32(formsenum.productExportImport));
                return new Dictionary<string, int>();
            }

        }
        public Dictionary<string, int> GetcategoryList()
        {
            try
            {
                Dictionary<string, int> categoryList = new Dictionary<string, int>();

                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();

                objSearch.Select = "Fcid Id,CategoryName as Value";
                objSearch.Table = "tblCategory_FTI";
                objSearch.WhereNdGroupBy = "WHERE Status=1 AND CategoryType=2";
                objSearch.OrderBy = "ORDER BY Value";

                var result = bllSearch.GetSearchData("prdSearch", objSearch);

                foreach (var item in result)
                {
                    categoryList.Add(item.Value, Convert.ToInt32(item.Id));
                }

                return categoryList;
            }
            catch (Exception ex)
            {
                clsgen.LogException("ProductExportImport GetsubcategoryList()", ex,
                   FrmLogin.EmployeName,
                   FrmLogin.Designation,
                   Convert.ToInt32(formsenum.productExportImport));
                return new Dictionary<string, int>();
            }

        }
        #endregion

    }
}
