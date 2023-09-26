using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using MetroFramework;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pLocker.Forms
{
    public partial class FrmEmployeeEarnDedu : MetroFramework.Forms.MetroForm
    {
        #region Declaration
        ClsGeneral objGeneral = new ClsGeneral();
        long BranchId = FrmLockerDashBoard.BranchId;
        long EmployeeEarnedId = 0;
        long EmployeeId = 0;
        #endregion
        #region Functions
        public FrmEmployeeEarnDedu()
        {
            InitializeComponent();
        }
        public void FillCombo()
        {
            //Earn Type
            BllSearch<DmnComboBox> bllSearchEarnType = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchEarnType = new DmnSearch();
            dmnSearchEarnType.Select = "GeneralId,Caption";
            dmnSearchEarnType.Table = "tblGeneralOptions g " +
                "left join tblGType gt on g.GTypeId =  gt.GTypeId";
            dmnSearchEarnType.WhereNdGroupBy = "Where gt.GType='EARNTYPE' AND g.Status = 0 ";
            var earntypeSource = bllSearchEarnType.GetSearchData("prdComboFill", dmnSearchEarnType);
            mcmbEarnType.DataSource = earntypeSource;
            mcmbEarnType.DisplayMember = "Value";
            mcmbEarnType.ValueMember = "Id";

            //Deducted Type
            BllSearch<DmnComboBox> bllSearchDeductedType = new BllSearch<DmnComboBox>();
            DmnSearch dmnSearchDeductedType = new DmnSearch();
            dmnSearchDeductedType.Select = "GeneralId,Caption";
            dmnSearchDeductedType.Table = "tblGeneralOptions g " +
                "left join tblGType gt on g.GTypeId =  gt.GTypeId";
            dmnSearchDeductedType.WhereNdGroupBy = "Where gt.GType='DEDUCTIONTYPE' AND g.Status = 0 ";
            var deductedtypeSource = bllSearchDeductedType.GetSearchData("prdComboFill", dmnSearchDeductedType);
            mcmbDeductedType.DataSource = deductedtypeSource;
            mcmbDeductedType.DisplayMember = "Value";
            mcmbDeductedType.ValueMember = "Id";

        }
        public bool IsSavable()
        {
            Control[] controllist = { mtxtEmployee};
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
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
            hlTitle.Text = "Employee Earn & Deduction ";
            FillCombo();
            mtxtEmployee.Focus();
            ActiveControl = mtxtEmployee;
            EmployeeId = 0;
            EmployeeEarnedId = 0;
            //objGeneral.ResetAllTextBoxes(this);
            mtxtDeductDescription.Clear();
            mtxtDeductedAmount.Clear();
            mtxtEarnedAmount.Clear();
            mtxtEarnedDescription.Clear();
            mbtnDelete.Enabled = false;
            EmployeeId = FrmEmployee.EmployeeId;

        }
        #endregion

        #region Events
        private void FrmEmployeeEarnDedu_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshForm();
                EmployeeId = FrmEmployee.EmployeeId;
                BllSearch<DmnEmployee> bllSearch = new BllSearch<DmnEmployee>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "EmployeeId,EmployeeName,BranchId";
                objSearch.Table = "tblEmployee";
                objSearch.WhereNdGroupBy = "Where EmployeeId = " + EmployeeId;
                objSearch.OrderBy = "Order By EmployeeId ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                mtxtEmployee.Text = Convert.ToString(gridSource[0].EmployeeName);
                EmployeeId = gridSource[0].EmployeeId;
                BranchId = gridSource[0].BranchId;
            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }
        private void FrmEmployeeEarnDedu_KeyDown(object sender, KeyEventArgs e)
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
        private void MbEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Employee Code,Employee Name";
                _objSearch.Select = "EmployeeId as Id,EmployeeCode as Field1, EmployeeName as Field2";
                _objSearch.Table = "tblEmployee";
                _objSearch.WhereNdGroupBy = "Where Status=0";
                _objSearch.OrderBy = " Order By EmployeeName ASC ";
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, 800, 400, 190, 250))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);

                    }
                }
                if (returnValue.ToString() != "")
                {
                    EmployeeId = Convert.ToInt32(returnValue.ToString());
                    EmployeeId = FrmEmployee.EmployeeId;
                    BllSearch<DmnEmployee> bllSearch = new BllSearch<DmnEmployee>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "EmployeeId,EmployeeName,BranchId";
                    objSearch.Table = "tblEmployee";
                    objSearch.WhereNdGroupBy = "Where EmployeeId = " + EmployeeId;
                    objSearch.OrderBy = "Order By EmployeeId ";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    mtxtEmployee.Text = Convert.ToString(gridSource[0].EmployeeName);
                    EmployeeId = gridSource[0].EmployeeId;
                    BranchId = gridSource[0].BranchId;

                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnSave_Click(object sender, EventArgs e)
        {
            bool saveStatus;
            try
            {
                if (IsSavable())
                {
                    BllEmployeeEarnDedu bllEmployeeEarnDedu = new BllEmployeeEarnDedu();
                    DmnEmployeeEarnDedu dmnEmployeeEarnDedu = new DmnEmployeeEarnDedu();
                    dmnEmployeeEarnDedu.EmployeeId = EmployeeId;
                    dmnEmployeeEarnDedu.EarnTypeId = Convert.ToInt64(mcmbEarnType.SelectedValue);
                    dmnEmployeeEarnDedu.EarnedAmount = decimal.Parse(mtxtEarnedAmount.Text == string.Empty ? "0.00" : mtxtEarnedAmount.Text);
                    dmnEmployeeEarnDedu.EarnedDescription = mtxtEarnedDescription.Text;
                    dmnEmployeeEarnDedu.DeductedTypeId = Convert.ToInt64(mcmbDeductedType.SelectedValue);
                    dmnEmployeeEarnDedu.DeductedAmount = decimal.Parse(mtxtDeductedAmount.Text == string.Empty ? "0.00" : mtxtDeductedAmount.Text);
                    dmnEmployeeEarnDedu.DeductedDescription = mtxtDeductDescription.Text;
                    dmnEmployeeEarnDedu.BranchId = BranchId;
                    if (EmployeeEarnedId == 0)
                    {
                        dmnEmployeeEarnDedu.CreatedBy = FrmLockerDashBoard.UserId;
                        saveStatus = bllEmployeeEarnDedu.AddData("prdInsertEmployeeEarnDedu", dmnEmployeeEarnDedu);
                        if (saveStatus)
                            MetroMessageBox.Show(this, "Saved successfully ", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        dmnEmployeeEarnDedu.EmployeeEarnedId = EmployeeEarnedId;
                        dmnEmployeeEarnDedu.ModifiedBy = FrmLockerDashBoard.UserId;
                        saveStatus = bllEmployeeEarnDedu.UpdateData("prdUpdateEmployeeEarnDedu", dmnEmployeeEarnDedu);
                        if (saveStatus)
                            MetroMessageBox.Show(this, "Updated successfully ", "Upadted  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    RefreshForm();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnList_Click(object sender, EventArgs e)
        {
            try
            {
                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,Employee,Earned Amount,Deducted Amount";
                _objSearch.Select = "EmployeeEarnedId as Id,emp.EmployeeName as Field1, EarnedAmount as Field2,DeductedAmount as Field3";
                _objSearch.Table = "tblEmployeeEarnDedu as epe " +
                    "left join tblEmployee as emp on epe.EmployeeId = emp.EmployeeId ";
                _objSearch.WhereNdGroupBy = "Where epe.Status=0 AND epe.BranchId = "+ BranchId;
                _objSearch.OrderBy = " Order By EmployeeName ASC ";
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, 800, 250, 230, 280))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);

                    }
                }
                if (returnValue.ToString() != "")
                {
                    DmnEmployeeEarnDedu dmnEmployeeEarnDedu = new DmnEmployeeEarnDedu();
                    EmployeeEarnedId = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnEmployeeEarnDedu> bllSearch = new BllSearch<DmnEmployeeEarnDedu>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "epe.EmployeeEarnedId,epe.EmployeeId,emp.EmployeeName,epe.EarnTypeId,epe.EarnedAmount,"+
                        "epe.EarnedDescription,epe.DeductedTypeId,epe.DeductedAmount," +
                        "epe.DeductedDescription";
                    objSearch.Table = " tblEmployeeEarnDedu as epe " +
                        " left join tblEmployee emp on epe.EmployeeId = emp.EmployeeId ";
                    objSearch.WhereNdGroupBy = "Where EmployeeEarnedId = " + EmployeeEarnedId;
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    mtxtEmployee.Text = gridSource[0].EmployeeName;
                    mcmbEarnType.SelectedValue = gridSource[0].EarnTypeId;
                    mtxtEarnedAmount.Text = Convert.ToString(gridSource[0].EarnedAmount);
                    mtxtEarnedDescription.Text = gridSource[0].EarnedDescription;
                    mcmbDeductedType.SelectedValue = gridSource[0].DeductedTypeId;
                    mtxtDeductedAmount.Text = Convert.ToString(gridSource[0].DeductedAmount);
                    mtxtDeductDescription.Text = gridSource[0].DeductedDescription;
                    EmployeeId = gridSource[0].EmployeeId;
                    hlTitle.Text = "Employee Earn & Deduction [Edit]";
                    mbtnDelete.Enabled = true;
                    mtxtEmployee.Focus();

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
                RefreshForm();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MbtnDelete_Click(object sender, EventArgs e)
        {
            bool deleteStatus;
            try
            {
                DialogResult dialogResult = MetroMessageBox.Show(this, "Confirm to delete " + " ? ", "Delete ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    BllEmployeeEarnDedu bllEmployeeEarnDedu = new BllEmployeeEarnDedu();
                    DmnEmployeeEarnDedu dmnEmployeeEarnDedu = new DmnEmployeeEarnDedu();
                    dmnEmployeeEarnDedu.EmployeeEarnedId = EmployeeEarnedId;
                    dmnEmployeeEarnDedu.ModifiedBy = FrmLockerDashBoard.UserId;
                    deleteStatus = bllEmployeeEarnDedu.DeleteById("prdDeleteEmployeeEarnDedu", dmnEmployeeEarnDedu);
                    if (deleteStatus)
                        MetroMessageBox.Show(this, "Deleted successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    RefreshForm();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void FrmEmployeeEarnDedu_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                    MbtnSave_Click(sender, e);
                if (e.KeyCode == Keys.F3)
                    MbtnList_Click(sender, e);
                if (e.KeyCode == Keys.F5)
                    MbtnRefresh_Click(sender, e);
                if (e.KeyCode == Keys.F7)
                    MbtnDelete_Click(sender, e);
                if (e.KeyCode == Keys.Escape)
                    this.Close();

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        private void mbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void Minimize(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FrmEmployeeEarnDedu_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //save
        private void mbtnSave_MouseHover(object sender, EventArgs e)
        {
            this.mbtnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_3));
        }

        private void mbtnSave_MouseDown(object sender, MouseEventArgs e)
        {
            this.mbtnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_2));
        }

        private void mbtnSave_MouseLeave(object sender, EventArgs e)
        {
            this.mbtnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_1));
        }

        private void mbtnSave_MouseUp(object sender, MouseEventArgs e)
        {
            this.mbtnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.save_1));
        }

        //list
        private void mbtnList_MouseHover(object sender, EventArgs e)
        {
            this.mbtnList.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.search_3));
        }

        private void mbtnList_MouseDown(object sender, MouseEventArgs e)
        {
            this.mbtnList.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.search_2));
        }

        private void mbtnList_MouseLeave(object sender, EventArgs e)
        {
            this.mbtnList.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.search_1));
        }

        private void mbtnList_MouseUp(object sender, MouseEventArgs e)
        {
            this.mbtnList.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.search_1));
        }

        //refresh
        private void mbtnRefresh_MouseHover(object sender, EventArgs e)
        {
            this.mbtnList.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_3));
        }

        private void mbtnRefresh_MouseDown(object sender, MouseEventArgs e)
        {
            this.mbtnList.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_2));
        }

        private void mbtnRefresh_MouseLeave(object sender, EventArgs e)
        {
            this.mbtnList.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_1));
        }

        private void mbtnRefresh_MouseUp(object sender, MouseEventArgs e)
        {
            this.mbtnList.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_1));
        }

        //delete
        private void mbtnDelete_MouseHover(object sender, EventArgs e)
        {
            this.mbtnDelete.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.delete_3));
        }

        private void mbtnDelete_MouseDown(object sender, MouseEventArgs e)
        {
            this.mbtnDelete.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.delete_2));
        }

        private void mbtnDelete_MouseLeave(object sender, EventArgs e)
        {
            this.mbtnDelete.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.delete_1));
        }

        private void mbtnDelete_MouseUp(object sender, MouseEventArgs e)
        {
            this.mbtnDelete.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.delete_1));
        }

        //close
        private void mbtnClose_MouseHover(object sender, EventArgs e)
        {
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_3));
        }

        private void mbtnClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_2));
        }

        private void mbtnClose_MouseLeave(object sender, EventArgs e)
        {
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_1));
        }

        private void mbtnClose_MouseUp(object sender, MouseEventArgs e)
        {
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.close_1));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
