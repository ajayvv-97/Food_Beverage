using Domain;
using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using MarginPOSNew.Classes;
using MetroFramework.Controls;
using MarginPOSNew.Forms.UserControls;
using System.Threading;
using System.Globalization;

namespace MarginPOSNew.Forms.Masters
{
    public partial class FrmCategory : MetroForm
    {

        #region Declaration

        ClsGeneral objGeneral = new ClsGeneral();// for validation
        long BranchId = Convert.ToInt64(FrmDashboard.BranchId);
        long UserId = Convert.ToInt64(FrmDashboard.UserId);
        long CategoryId = 0;//initially zero, value changes in update

        #endregion

        #region Functions
        public bool IsSavable()
        {
            Control[] controllist = { mtxtCategoryName, mtxtCatSrlNoStart, mtxtCategoryCode };
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
            {
                return false;
            }
            if (!IsCategoryNameValid() || !IsCategoryCodeValid())
            {
                return false;
            }
            //if (!IsSerialNoValid())/used to confirm minimum 5 digits for serial number
            //{
               
            //    return false;
            //}
            else
            {
                return true;
            }
        }

        public FrmCategory()
        {
            InitializeComponent();
        }

        public void fillGridView()
        {
            try
            {
                dgvCategory.Columns.Clear();
                dgvCategory.DataSource = null;
                FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.ColumnHeaders = "CategoryId,Category Name,Category Code,Serial No Start,Serial No Current";
                dmnSearch.Select = "CategoryId as Id,CategoryName as Field1,CategoryCode as Field2,CategorySrlNoStart as Field3,CategorySrlNoCurrent as Field4";
                //dmnSearch.ColumnHeaders = "CategoryId,Category Name,Category Code,Serial No Current";
                //dmnSearch.Select = "CategoryId,CategoryName,CategoryCode,CategorySrlNoCurrent";
                dmnSearch.Table = "tblCategory";
                dmnSearch.WhereNdGroupBy = "Where Status = 0 and BranchId=" + BranchId;
                dmnSearch.OrderBy = "Order By CategoryName ASC";
                var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
                foreach (DataGridViewColumn col in gridSource.Columns)
                {
                    dgvCategory.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvCategory.DataSource = gridSource.DataSource;

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        public void RefreshForm()
        {
            try
            {
                mtxtCatSrlNoStart.ReadOnly = false;
                CategoryId = 0;
                hlTitle.Text = "Category";
                objGeneral.ResetAllTextBoxes(this);
                fillGridView();
                mtxtCategoryName.Focus();
                //GenerateSrNo();
                if (string.IsNullOrWhiteSpace(mtxtCategoryName.Text))
                {
                    mbtnSave.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        public void GenerateSrNo()
        {
            BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "MAX(CategorySrlNoStart) as CategorySrlNoStart";
            objSearch.Table = " tblcategory ";
            objSearch.WhereNdGroupBy = "";
            var gridSourceSrlno = bllSearch.GetSearchData("prdSearch", objSearch);

            BllSearch<DmnGeneralOptions> bllSearchGn = new BllSearch<DmnGeneralOptions>();
            DmnSearch objSearchGn = new DmnSearch();
            objSearchGn.Select = "GeneralId,GTypeSubValue1";
            objSearchGn.Table = " tblGeneralOptions g LEFT JOIN tblGType gt ON g.GTypeId =  gt.GTypeId ";
            objSearchGn.WhereNdGroupBy = " Where gt.GType='CODE DIFFERENCE' AND (g.Status = 0 OR g.Status = 2)";
            var gridSourceGn = bllSearchGn.GetSearchData("prdSearch", objSearchGn);
            decimal difference = Convert.ToDecimal(gridSourceGn[0].GTypeSubValue1);
            decimal CategorySrlNoStart = Convert.ToDecimal(gridSourceSrlno[0].CategorySrlNoStart + difference);

            mtxtCatSrlNoStart.Text = Convert.ToString(CategorySrlNoStart.ToString("0"));
            mtxtCurrentSerialNo.Text = Convert.ToString(CategorySrlNoStart.ToString("0"));
        }
        public bool IsSerialNoStartvalid()//not using now
        {
            if (!string.IsNullOrWhiteSpace(mtxtCatSrlNoStart.Text))
            {
                DmnCategory objCat = new DmnCategory();
                BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "CategoryId ";
                objSearch.Table = " tblcategory ";
                if (CategoryId == 0)
                {
                    objSearch.WhereNdGroupBy = " Where CategorySrlNoStart = '" + mtxtCatSrlNoStart.Text.Trim() + "' AND Status=0 AND BranchId = " + BranchId;
                }
                else
                {
                    objSearch.WhereNdGroupBy = " Where CategoryId<> " + CategoryId + " AND CategoryName='" + mtxtCategoryName.Text.Trim() + "'AND Status=0 AND BranchId = " + BranchId;

                }
                var lstCategory = bllSearch.GetSearchData("prdSearch", objSearch);//lst category will have the no of id
                if (lstCategory.Count > 0)
                {
                    MetroMessageBox.Show(this, "\n\n Category serial no already exists!", "Error", MessageBoxButtons.OK, 150);
                    mtxtCatSrlNoStart.Focus();
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool IsSerialNoValid()
        {
            if (mtxtCurrentSerialNo.Text.Length < 5)

            {
                MetroMessageBox.Show(this, "\nMinimum 5 digits required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                mtxtCatSrlNoStart.Focus();
                return false;
            }
            return true;
        }
        public bool IsCategoryNameValid()
        {
            DmnCategory objCat = new DmnCategory();
            BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "CategoryId ";
            objSearch.Table = " tblcategory ";
            if (CategoryId == 0)
            {
                objSearch.WhereNdGroupBy = " Where CategoryName = '" + mtxtCategoryName.Text.Trim() + "' collate  SQL_Latin1_General_CP1_CS_AS  AND BranchId = " + BranchId;
            }
            else
            {
                objSearch.WhereNdGroupBy = " Where CategoryId<> " + CategoryId + " AND Status=0 AND CategoryName='" + mtxtCategoryName.Text.Trim() + "' collate  SQL_Latin1_General_CP1_CS_AS AND BranchId = " + BranchId;

            }
            var lstCategory = bllSearch.GetSearchData("prdSearch", objSearch);//lst category will have the no of id
            if (lstCategory.Count > 0)
            {
                MetroMessageBox.Show(this, "\nCategory name already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                mtxtCategoryName.Focus();
                return false;
            }
            return true;

        }
        public bool IsCategoryCodeValid()
        {

            DmnCategory objCat = new DmnCategory();
            BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
            DmnSearch objSearch = new DmnSearch();

            objSearch.Select = "CategoryId ";
            objSearch.Table = " tblcategory ";
            if (CategoryId == 0)
            {
                objSearch.WhereNdGroupBy = " Where CategoryCode = '" + mtxtCategoryCode.Text.Trim() + "' collate  SQL_Latin1_General_CP1_CS_AS   AND BranchId = " + BranchId + "AND Status=0";
            }
            else
            {
                objSearch.WhereNdGroupBy = " Where CategoryId<> " + CategoryId + "  AND  CategoryCode='" + mtxtCategoryCode.Text.Trim() + "'  collate  SQL_Latin1_General_CP1_CS_AS AND BranchId = " + BranchId + " AND Status=0";

            }
            var lstCategory = bllSearch.GetSearchData("prdSearch", objSearch);//lst category will have the no of id
            if (lstCategory.Count > 0)
            {
                MetroMessageBox.Show(this, "\n\n Category code already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                mtxtCategoryCode.Focus();
                return false;
            }
            return true;
        }
        public void dvgfrmcategory()
        {
            if (dgvCategory.Rows.Count > 0)
            {
                hlTitle.Text = "Category [Edit]";
                mtxtCatSrlNoStart.ReadOnly = true;
                CategoryId = Convert.ToInt64(dgvCategory.CurrentRow.Cells[0].Value);
                mtxtCategoryName.Text = Convert.ToString(dgvCategory.CurrentRow.Cells[1].Value);
                mtxtCatSrlNoStart.Text = dgvCategory.CurrentRow.Cells[3].Value.ToString();
                mtxtCurrentSerialNo.Text = dgvCategory.CurrentRow.Cells[4].Value.ToString();
                mtxtCategoryCode.Text = Convert.ToString(dgvCategory.CurrentRow.Cells[2].Value);
                mtxtCategoryName.Focus();
                mbtnSave.Enabled = false;
            }
        }
        #endregion

        #region Events
        private void FrmCategory_Load(object sender, EventArgs e)
        {
            try
            {
                //fillGridView();
                //mtxtCategoryName.Focus();
                RefreshForm();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtCatSrlNoStart_Leave(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(mtxtCurrentSerialNo.Text))
                {
                    mtxtCurrentSerialNo.Text = mtxtCatSrlNoStart.Text;
                }
               
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, 150);
            }
           
        }
        private void MbtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(mtxtCategoryName.Text))
                {
                    mbtnSave.Enabled = false;
                }
                RefreshForm();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsSavable())
                {
                    DmnCategory dmnCat = new DmnCategory();
                    BllCategory objBll = new BllCategory();
                    dmnCat.CategoryName = mtxtCategoryName.Text.Trim();
                    dmnCat.CategorySrlNoStart = Convert.ToInt64(mtxtCatSrlNoStart.Text.Trim());
                    dmnCat.CategoryCode = mtxtCategoryCode.Text.Trim();
                    dmnCat.CategorySrlNoCurrent = Convert.ToInt64(mtxtCatSrlNoStart.Text.Trim());
                    dmnCat.BranchId = BranchId;
                    dmnCat.Status = 0;
                    if (CategoryId == 0)//insert
                    {
                        dmnCat.CreatedBy = UserId;
                        bool insertResult = objBll.AddData("prdInsertCategory", dmnCat);
                        if (insertResult == true)
                        {
                            MetroMessageBox.Show(this,mtxtCategoryName.Text+ " Successfully Saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);

                        }
                        else
                        {
                            MetroMessageBox.Show(this,mtxtCategoryName.Text+ " Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                    }
                    else
                    {
                        dmnCat.ModifiedBy = UserId;
                        dmnCat.ModifiedDateTime = DateTime.Now;
                        dmnCat.CategoryId = CategoryId;
                        bool updateResult = objBll.UpdateData("prdUpdateCategory", dmnCat);

                        if (updateResult == true)
                        {
                            MetroMessageBox.Show(this,mtxtCategoryName.Text+ " Updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                        }
                        else
                        {
                            MetroMessageBox.Show(this,mtxtCategoryName.Text+ " Update failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                    }
                    mtxtCategoryCode.ReadOnly = false;
                    mtxtCategoryName.Focus();

                    RefreshForm();
                    
                }
               

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
               
            }
            //mtxtCategoryName.Focus();
        }
        private void DgvCategory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                //if (dgvCategory.Rows.Count > 0)
                //{
                //    hlTitle.Text = "Category [Edit]";
                //    mtxtCatSrlNoStart.ReadOnly = true;
                //    CategoryId = Convert.ToInt64(dgvCategory.CurrentRow.Cells[0].Value);
                //    mtxtCategoryName.Text = Convert.ToString(dgvCategory.CurrentRow.Cells[1].Value);
                //    mtxtCatSrlNoStart.Text = dgvCategory.CurrentRow.Cells[3].Value.ToString();
                //    mtxtCurrentSerialNo.Text = dgvCategory.CurrentRow.Cells[4].Value.ToString();
                //    mtxtCategoryCode.Text = Convert.ToString(dgvCategory.CurrentRow.Cells[2].Value);
                //    mtxtCategoryName.Focus();
                //}
                dvgfrmcategory();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void DgvCategory_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgvCategory.Rows.Count > 0)
                {
                    
                    long CategoryId = Convert.ToInt64(dgvCategory.CurrentRow.Cells[0].Value);
                    if (e.KeyCode == Keys.Delete)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "\nDo you want to delete " + dgvCategory.CurrentRow.Cells[1].Value.ToString() + "?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150);
                        if (result == DialogResult.Yes)
                        {
                            DmnCategory dmnCategory = new DmnCategory();
                            BllCategory bllCategory = new BllCategory();
                            dmnCategory.CategoryId = CategoryId;
                            bool deleteStatus = bllCategory.DeleteById("prdDeleteCategory", dmnCategory);
                            if (deleteStatus)
                            {

                                MetroMessageBox.Show(this, "\nSuccessfully deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                            }
                            else
                            {
                                MetroMessageBox.Show(this,"\nDelete failed", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                            }
                            RefreshForm();
                        }
                    }
                    dgvCategory.Focus();
                    if (e.KeyCode == Keys.Enter)
                    {
                        dvgfrmcategory();
                        mtxtCategoryName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void FrmCategory_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(mtxtCategoryName.Text))
                {
                    if(e.KeyCode!=Keys.F2 && e.KeyCode!=Keys.F5)
                    {
                        mbtnSave.Enabled = true;
                    }
                   
                }

                if (e.KeyCode == Keys.F2 && mbtnSave.Enabled==true)
                    MbtnSave_Click(sender, e);
                if (e.KeyCode == Keys.F5)
                    MbtnRefresh_Click(sender,e);
                if (e.KeyCode == Keys.Escape)
                    this.Close();

                //objGeneral.NextConrolOnEnterPress(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }
        private void mtxtCategoryName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                    objGeneral.NextConrolOnEnterPress(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }
        private void mtxtCategoryCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                objGeneral.NextConrolOnEnterPress(sender, e);
                if(string.IsNullOrWhiteSpace(mtxtCatSrlNoStart.Text))
                {
                    GenerateSrNo();
                }
               
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mtxtCatSrlNoStart_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                objGeneral.NextConrolOnEnterPress(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mtxtCategoryName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(mtxtCategoryName.Text))
                {
                    CultureInfo cI = Thread.CurrentThread.CurrentCulture;
                    TextInfo textInfo = cI.TextInfo;
                    mtxtCategoryName.Text = textInfo.ToTitleCase(mtxtCategoryName.Text);
                    mtxtCategoryName.Select(mtxtCategoryName.Text.Length, 1);
                    if (CategoryId <= 0)
                    {
                        string input = mtxtCategoryName.Text.Trim();
                        string Accode = "";
                        string[] splitString = input.Split(' ');
                        foreach (string s in splitString)
                        {
                            Accode = Accode + s.Substring(0, 1);
                        }
                        mtxtCategoryCode.Text = Accode;
                    }
                    if (string.IsNullOrWhiteSpace(mtxtCatSrlNoStart.Text))
                    {
                        GenerateSrNo();
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }
        private void mtxtCategoryName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    CultureInfo cI = Thread.CurrentThread.CurrentCulture;
            //    TextInfo textInfo = cI.TextInfo;
            //    mtxtCategoryName.Text = textInfo.ToTitleCase(mtxtCategoryName.Text);
            //    mtxtCategoryName.Select(mtxtCategoryName.Text.Length, 1);
            //}
            //catch (Exception ex)
            //{
            //    MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            //}
        }

        #endregion
    }
}
