using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
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

namespace FoodandBeverage.Forms
{
    public partial class FrmProductionMapping : Form
    {
        #region DEclaration 

        #region Declare Grid Column Indexes
        private int slno = 0;
        private int piidno = 1;
        private int pnameno = 2;
        private int pcatno = 3;
        private int pfcidno = 4;
        private int ppdtype = 5;       
        private int pkitchenstatusno = 6;
        private int pcounterstatusno = 7;
        #endregion

        ClsGeneral clsgen = new ClsGeneral();


        #endregion
        public FrmProductionMapping()
        {
            InitializeComponent();
        }
        private void FrmProductionMapping_Load(object sender, EventArgs e)
        {
            Fillcombo();
            ProductionMapping_Fill();
            AddHeaderCheckboxes();
        }
        public void ProductionMapping_Fill(int? fcid = null, string searchText = "")
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();

                objSearch.Select = "p.ProductId as Field1, Product as Field2, C.CategoryName AS Field3, " +
                                   "P.Fcid AS Field4, p.PreparationMode AS Field5";

                objSearch.Table = "tblProduct p " +
                                  "LEFT JOIN tblCategory_FTI c ON C.Fcid = P.Fcid";

                string where = "WHERE p.Status = 1 ";

                // ✅ Category filter
                if (fcid != null && fcid != 0 && fcid >=0)
                {
                    where += " AND p.Fcid = " + fcid;
                }

                // ✅ Search filter
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    where += " AND Product LIKE '%" + searchText.Replace("'", "''") + "%'";
                }

                objSearch.WhereNdGroupBy = where +
                    " GROUP BY p.ProductId, Product, C.CategoryName, P.Fcid, p.PreparationMode";

                objSearch.OrderBy = "ORDER BY Product";

                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);

                dgv_menu.Rows.Clear();

                if (itemSource.Count > 0)
                {
                    dgv_menu.Rows.Add(itemSource.Count);

                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_menu.Rows[i].Cells[slno].Value = i + 1;
                        dgv_menu.Rows[i].Cells[piidno].Value = itemSource[i].Field1;
                        dgv_menu.Rows[i].Cells[pnameno].Value = itemSource[i].Field2;
                        dgv_menu.Rows[i].Cells[pcatno].Value = itemSource[i].Field3;
                        dgv_menu.Rows[i].Cells[pfcidno].Value = itemSource[i].Field4;

                        int prepType = itemSource[i].Field5 == null ? 0 : Convert.ToInt32(itemSource[i].Field5);

                        dgv_menu.Rows[i].Cells[pkitchenstatusno].Value =
                            (prepType == Convert.ToInt32(KOTRouting.kitchen));

                        dgv_menu.Rows[i].Cells[pcounterstatusno].Value =
                            (prepType == Convert.ToInt32(KOTRouting.Counter));
                    }
                }
                else
                {
                    dgv_menu.Rows.Add();
                    dgv_menu.Rows[0].Cells[pnameno].Value = "No Products Found";
                }
         
        }

            catch (Exception ex)
            {
                clsgen.LogException("FoodPreparationMapping page  ProductionMapping_Fill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.FoodPreparationMapping));
            }
}


        public void Fillcombo()
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Fcid,CategoryName";
                objSearch.Table = " tblCategory_FTI";
                objSearch.WhereNdGroupBy = "where Status=1 and CategoryType=0";
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);


                cmbSearchlist.DataSource = gridSource;
                cmbSearchlist.ValueMember = "Id";
                cmbSearchlist.DisplayMember = "Value";

                gridSource.Add(new DmnComboBox
                {
                    Id = -2,
                    Value = "Combo Product"
                });

                cmbMenu_category.DataSource = gridSource;
                cmbMenu_category.ValueMember = "Id";
                cmbMenu_category.DisplayMember = "Value";

            }
            catch (Exception ex)
            {
                clsgen.LogException("FoodPreparationMapping page  Fillcombo()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.FoodPreparationMapping));
            }
        }
        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                BllMenu bll = new BllMenu();

                for (int i = 0; i < dgv_menu.RowCount; i++)
                {
                    if (dgv_menu.Rows[i].Cells[piidno].Value != null)
                    {
                        int productId = Convert.ToInt32(dgv_menu.Rows[i].Cells[piidno].Value);

                        bool isKitchen = Convert.ToBoolean(dgv_menu.Rows[i].Cells[pkitchenstatusno].Value);
                        bool isCounter = Convert.ToBoolean(dgv_menu.Rows[i].Cells[pcounterstatusno].Value);

                        int preparationMode = 0;

                        if (isKitchen)
                            preparationMode = 1;
                        else if (isCounter)
                            preparationMode = 2;

                     
                        DmnMenu obj = new DmnMenu
                        {
                            ProductId = productId,
                            PreparationMode = preparationMode
                        };

                        bll.UpdatePreparationMode("prdUpdate_PreparationMode", obj);

                        count++;
                    }
                }

                if (count > 0)
                {
                    CMessageBox.Show("Successfully saved", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ProductionMapping_Fill();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "Save Production Mapping",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Menu)
                );
            }
        }




        private void btnrefresh_Click(object sender, EventArgs e)
        {
            try
            {
               
                txtsearchlist.Text = "";
                cmbMenu_category.SelectedIndex = -1;
                ProductionMapping_Fill();
                dgv_menu.ClearSelection();
                RefreshSlNo();
            }
             

            catch (Exception ex)
            {
                clsgen.LogException("FoodPreparationMapping page  btnrefresh_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.FoodPreparationMapping));
            }
}


        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                int returnValue = 0;

                DmnSearch _objSearch = new DmnSearch();

                // ✅ PRODUCT search
                _objSearch.ColumnHeaders = "ID,ItemCode,Product,Category";
                _objSearch.Select = "p.ProductId as Id, ItemCode as Field1, Product as Field2, c.CategoryName as Field3";
                _objSearch.Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON c.Fcid = p.Fcid";
                _objSearch.WhereNdGroupBy = "WHERE p.Status = 1";
                _objSearch.OrderBy = "ORDER BY p.Product";

                var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);

                if (gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // ✅ Search Popup
                using (var search = new FrmSearch<DmnGridFields>(
                    _objSearch,
                    SearchMode.FirstCharacter,
                    this.Width - 150,
                    this.Height - 150,
                    this.Location.Y + 50,
                    this.Location.X + 20,
                    new DataTable()))
                {
                    if (search.ShowDialog() == DialogResult.OK)
                    {
                        returnValue = Convert.ToInt32(search._retValue);
                    }
                }

                if (returnValue <= 0) return;

                // ✅ Fetch Selected Product
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "p.ProductId as Field1, Product as Field2, c.CategoryName as Field3, p.Fcid as Field4, p.PreparationMode as Field5";
                objSearch.Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON c.Fcid = p.Fcid";
                objSearch.WhereNdGroupBy = "WHERE p.ProductId = " + returnValue;

                var item = bllSearch.GetSearchData("prdSearch", objSearch);

                if (item.Count == 0) return;

                // ✅ Clear old data
                dgv_menu.Rows.Clear();

                // ✅ Add one row
                int rowIndex = dgv_menu.Rows.Add();

                dgv_menu.Rows[rowIndex].Cells[slno].Value = 1;
                dgv_menu.Rows[rowIndex].Cells[piidno].Value = item[0].Field1;
                dgv_menu.Rows[rowIndex].Cells[pnameno].Value = item[0].Field2;
                dgv_menu.Rows[rowIndex].Cells[pcatno].Value = item[0].Field3;
                dgv_menu.Rows[rowIndex].Cells[pfcidno].Value = item[0].Field4;

                int prepMode = item[0].Field5 == null ? 0 : Convert.ToInt32(item[0].Field5);

                dgv_menu.Rows[rowIndex].Cells[pkitchenstatusno].Value = (prepMode == 1);
                dgv_menu.Rows[rowIndex].Cells[pcounterstatusno].Value = (prepMode == 2);
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "ProductionMapping btnSearch_Click()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.FoodPreparationMapping)
                );
            }
        }


        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbMenu_category_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbMenu_category.SelectedValue != null)
                {
                    int fcid = Convert.ToInt32(cmbMenu_category.SelectedValue);

                    // 🔥 Load filtered data
                    ProductionMapping_Fill(fcid);
                }
                else
                {
                    // 🔥 Load all products
                    ProductionMapping_Fill();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("FoodPreparationMapping page  cmbMenu_category_SelectionChangeCommitted()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.FoodPreparationMapping));
            }
        }

        private void btnseach_menu_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtsearchlist.Text.Trim();

                int? fcid = null;

                if (cmbMenu_category.SelectedValue != null)
                {
                    fcid = Convert.ToInt32(cmbMenu_category.SelectedValue);
                }

                // 🔥 Call with search + category
                ProductionMapping_Fill(fcid, searchText);
            }
            catch (Exception ex)
            {
                clsgen.LogException("FoodPreparationMapping page  btnseach_menu_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.FoodPreparationMapping));
            }
        }

        private void txtsearchlist_TextChanged(object sender, EventArgs e)
        {
            btnseach_menu_Click(sender, e);
        }
        private void dgv_menu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

            
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == pkitchenstatusno)
            {
                dgv_menu.Rows[e.RowIndex].Cells[pcounterstatusno].Value = false;
            }
            else if (e.ColumnIndex == pcounterstatusno)
            {
                dgv_menu.Rows[e.RowIndex].Cells[pkitchenstatusno].Value = false;
            }
            }
            catch (Exception ex)
            {
                clsgen.LogException("FoodPreparationMapping page  dgv_menu_CellContentClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.FoodPreparationMapping));
            }
        }
        private void RefreshSlNo()
        {
            for (int i = 0; i < dgv_menu.Rows.Count; i++)
            {
                dgv_menu.Rows[i].Cells[slno].Value = i + 1;
            }
        }

        private void chk_kitchenall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool isChecked = chk_kitchenall.Checked;

                foreach (DataGridViewRow row in dgv_menu.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        row.Cells[pkitchenstatusno].Value = isChecked;

                        // Optional: uncheck counter if kitchen selected
                        if (isChecked)
                            row.Cells[pcounterstatusno].Value = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("FoodPreparationMapping page  chk_kitchenall_CheckedChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.FoodPreparationMapping));
            }
        }


        private void chkcounterall_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                bool isChecked = chkcounterall.Checked;

                foreach (DataGridViewRow row in dgv_menu.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        row.Cells[pcounterstatusno].Value = isChecked;

                        // Optional: uncheck kitchen if counter selected
                        if (isChecked)
                            row.Cells[pkitchenstatusno].Value = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("FoodPreparationMapping page  chkcounterall_CheckedChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.FoodPreparationMapping));
            }
        }

        private void dgv_menu_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                if (e.ColumnIndex == pkitchenstatusno)
                {
                    chkcounterall.Checked = false;
                    bool isKitchen = Convert.ToBoolean(dgv_menu.Rows[e.RowIndex].Cells[pkitchenstatusno].Value);
                    if (isKitchen)
                        dgv_menu.Rows[e.RowIndex].Cells[pcounterstatusno].Value = false;
                }

                if (e.ColumnIndex == pcounterstatusno)
                {
                    chk_kitchenall.Checked = false;
                    bool isCounter = Convert.ToBoolean(dgv_menu.Rows[e.RowIndex].Cells[pcounterstatusno].Value);
                    if (isCounter)
                        dgv_menu.Rows[e.RowIndex].Cells[pkitchenstatusno].Value = false;
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("FoodPreparationMapping page  dgv_menu_CellValueChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.FoodPreparationMapping));
            }
        }

        private void dgv_menu_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgv_menu.IsCurrentCellDirty)
            {
                dgv_menu.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void AddHeaderCheckboxes()
        {
            try
            {
                // Kitchen checkbox
                Rectangle rectKitchen = dgv_menu.GetCellDisplayRectangle(pkitchenstatusno, -1, true);
                chk_kitchenall.Size = new Size(18, 18);
                chk_kitchenall.Location = new Point(
                    rectKitchen.X + (rectKitchen.Width - chk_kitchenall.Width) / 2,
                  rectKitchen.Y + rectKitchen.Height - chk_kitchenall.Height - 2
                );
                chk_kitchenall.BackColor = Color.Transparent;
                dgv_menu.Controls.Add(chk_kitchenall);

                // Counter checkbox
                Rectangle rectCounter = dgv_menu.GetCellDisplayRectangle(pcounterstatusno, -1, true);
                chkcounterall.Size = new Size(18, 18);
                chkcounterall.Location = new Point(
                    rectCounter.X + (rectCounter.Width - chkcounterall.Width) / 2,
                   rectCounter.Y + rectCounter.Height - chkcounterall.Height - 2
                );
                chkcounterall.BackColor = Color.Transparent;
                dgv_menu.Controls.Add(chkcounterall);
            }
            catch (Exception ex)
            {
                clsgen.LogException("FoodPreparationMapping page  AddHeaderCheckboxes()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.FoodPreparationMapping));
            }
        }

        private void dgv_menu_Resize(object sender, EventArgs e)
        {
            AddHeaderCheckboxes();
        }
    }
}
