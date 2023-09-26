using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MetroFramework;
using MetroFramework.Controls;
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
using System.Threading;
using System.Globalization;

namespace MarginPOSNew.Forms.Masters
{
    public partial class FrmUnit : MetroForm
    {
        #region Declarations

        ClsGeneral objGeneral = new ClsGeneral();// for validation
        long BranchId = Convert.ToInt32(FrmDashboard.BranchId);
        long UserId = Convert.ToInt32(FrmDashboard.UserId);
        long UnitId = 0;//initially zero, value changes in update

        #endregion

        #region Functions
        public FrmUnit()
        {
            InitializeComponent();
        }
        public void fillGridView()
        {
            try
            {
                dgvUnit.Columns.Clear();
                dgvUnit.DataSource = null;

                FrmDataGridView<DmnUnit> frmGridSearch = new FrmDataGridView<DmnUnit>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.ColumnHeaders = "Id,Unit Name";
                dmnSearch.Select = "UnitId,UnitName";
                dmnSearch.Table = "tblUnit";
                dmnSearch.WhereNdGroupBy = "Where Status = 0 and BranchId=" + BranchId;
                dmnSearch.OrderBy = "Order By UnitName ASC";
                var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
                foreach (DataGridViewColumn col in gridSource.Columns)
                {
                    dgvUnit.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvUnit.DataSource = gridSource.DataSource;

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        public bool IsSavable()
        {
            Control[] controllist = { mtxtUnit };
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
            {
                return false;
            }
            if (!IsUnitNameValid())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void RefreshForm()
        {

            try
            {
                UnitId = 0;
                hlTitle.Text = "Unit";
                objGeneral.ResetAllTextBoxes(this);
                fillGridView();
                mtxtUnit.Focus();
                ActiveControl = mtxtUnit;
                if(string.IsNullOrWhiteSpace(mtxtUnit.Text))
                {
                    mbtnSave.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

            }
        }
        public void dgvFrmUnit()
        {

            if (dgvUnit.Rows.Count > 0)
            {
                mtxtUnit.Focus();
                hlTitle.Text = "Unit [Edit]";
                mtxtUnit.Text = dgvUnit.CurrentRow.Cells[1].Value.ToString();
                UnitId = Convert.ToInt64(dgvUnit.CurrentRow.Cells[0].Value);
                mtxtUnit.Focus();
                mbtnSave.Enabled = false;
            }
        }
        #endregion

        #region Events
        private void FrmUnit_Load(object sender, EventArgs e)
        {
            try
            {

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
                    DmnUnit dmnUnit = new DmnUnit();
                    BllUnit objBll = new BllUnit();

                    dmnUnit.UnitName = mtxtUnit.Text.Trim().ToUpper();
                    dmnUnit.BranchId = BranchId;
                    if (UnitId == 0)//insert
                    {
                        dmnUnit.CreatedBy = UserId;
                        bool InsertStatus = objBll.AddData("prdInsertUnit", dmnUnit);

                        if (InsertStatus == true)
                        {
                            MetroMessageBox.Show(this, mtxtUnit.Text+" Successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                        }
                        else
                        {
                            MetroMessageBox.Show(this, mtxtUnit.Text+" Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                    }

                    else
                    {
                        dmnUnit.ModifiedBy = UserId;
                        dmnUnit.ModifiedDateTime = DateTime.Now;
                        dmnUnit.UnitId = UnitId;
                        bool updateResult = objBll.UpdateData("prdUpdateUnit", dmnUnit);

                        if (updateResult == true)
                        {
                            MetroMessageBox.Show(this,mtxtUnit.Text+" Updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                        }
                        else
                        {
                            MetroMessageBox.Show(this,mtxtUnit.Text+ " Update failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }

                    }

                    RefreshForm();
                    mtxtUnit.Focus();
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void MbtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(mtxtUnit.Text))
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

        public bool IsUnitNameValid()
        {
            DmnUnit objUnit = new DmnUnit();
            BllSearch<DmnUnit> bllSearch = new BllSearch<DmnUnit>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "UnitId ";
            objSearch.Table = " tblUnit ";
            if (UnitId == 0)
            {
                objSearch.WhereNdGroupBy = " Where UnitName = '" + mtxtUnit.Text.Trim().ToUpper() + "'  AND Status=0  AND BranchId = " + BranchId;
            }
            else
            {
                objSearch.WhereNdGroupBy = " Where UnitId<> " + UnitId + " AND Status=0 AND UnitName='" + mtxtUnit.Text.Trim().ToUpper() + "' AND BranchId = " + BranchId;

            }
            var lstUnit = bllSearch.GetSearchData("prdSearch", objSearch);//lst category will have the no of id
            if (lstUnit.Count > 0)
            {
                MetroMessageBox.Show(this, "\nUnit  name already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                //mtxtUnit.Text = "";
                mtxtUnit.Focus();
                return false;
            }
            return true;

        }
        private void MtxtUnit_Leave(object sender, EventArgs e)
        {
            try
            {
                //if (!string.IsNullOrWhiteSpace(mtxtUnit.Text))
                //{
                //    DmnUnit objUnit = new DmnUnit();
                //    BllSearch<DmnUnit> bllSearch = new BllSearch<DmnUnit>();
                //    DmnSearch objSearch = new DmnSearch();
                //    objSearch.Select = "UnitId ";
                //    objSearch.Table = " tblUnit ";
                //    if (UnitId == 0)
                //    {
                //        objSearch.WhereNdGroupBy = " Where UnitName = '" + mtxtUnit.Text.Trim() + "'  AND Status=0  AND BranchId = " + BranchId ;
                //    }
                //    else
                //    {
                //        objSearch.WhereNdGroupBy = " Where UnitId<> " + UnitId + " AND Status=0 AND UnitName='" + mtxtUnit.Text.Trim() + "' AND BranchId = " + BranchId;

                //    }
                //    var lstUnit = bllSearch.GetSearchData("prdSearch", objSearch);//lst category will have the no of id
                //    if (lstUnit.Count > 0)
                //    {
                //        MetroMessageBox.Show(this, "\nUnit  name already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                //        mtxtUnit.Text = "";
                //        mtxtUnit.Focus();
                //    }
                //}
                CultureInfo cI = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cI.TextInfo;
                mtxtUnit.Text = textInfo.ToTitleCase(mtxtUnit.Text);
                mtxtUnit.Select(mtxtUnit.Text.Length, 1);


            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

       

        private void DgvUnit_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                //    if (dgvUnit.Rows.Count > 0)
                //    {
                //        mtxtUnit.Focus();
                //        hlTitle.Text = "Unit [Edit]";
                //        mtxtUnit.Text = dgvUnit.CurrentRow.Cells[1].Value.ToString();
                //        UnitId = Convert.ToInt64(dgvUnit.CurrentRow.Cells[0].Value);
                //    }
                dgvFrmUnit();

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FrmUnit_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(mtxtUnit.Text))
                {
                    if (e.KeyCode != Keys.F2 && e.KeyCode != Keys.F5)
                    {
                        mbtnSave.Enabled = true;
                    }
                }
                //objGeneral.NextConrolOnEnterPress(sender, e);
                if (e.KeyCode == Keys.F2 && mbtnSave.Enabled == true)
                    MbtnSave_Click(sender, e);
                if (e.KeyCode == Keys.F5)
                    MbtnRefresh_Click(sender, e);
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mtxtUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                objGeneral.NextConrolOnEnterPress(sender, e);
              
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

           

        private void dgvUnit_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgvUnit.Rows.Count > 0)
                {
                    
                    long UnitId = Convert.ToInt64(dgvUnit.CurrentRow.Cells[0].Value);
                    if (e.KeyCode == Keys.Delete)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "\nDo you want to delete " + dgvUnit.CurrentRow.Cells[1].Value.ToString() +" ? ", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150);
                        if (result == DialogResult.Yes)
                        {
                            DmnUnit dmnUnit = new DmnUnit();
                            BllUnit bllUnit = new BllUnit();
                            dmnUnit.UnitId = UnitId;
                            bool deleteStatus = bllUnit.DeleteById("prdDeleteUnit", dmnUnit);

                            if (deleteStatus)
                            {
                                MetroMessageBox.Show(this, " Successfully deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information, 150);
                            }
                            else
                            {
                                MetroMessageBox.Show(this, " Error in deletion", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                            }
                        }
                        RefreshForm();
                    }
                    dgvUnit.Focus();
                    if (e.KeyCode == Keys.Enter)
                    {
                        dgvFrmUnit();
                        mtxtUnit.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        #endregion
    }
}
