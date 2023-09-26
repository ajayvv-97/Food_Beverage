using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
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
using System.Threading;
using System.Globalization;



namespace MarginPOSNew.Forms.Masters
{
    public partial class FrmDesignation : MetroFramework.Forms.MetroForm
    {
        #region Declaration
        ClsGeneral objGeneral = new ClsGeneral();
        long BranchId = Convert.ToInt32(FrmDashboard.BranchId);
        long UserId = Convert.ToInt32(FrmDashboard.UserId);
        long DesignationId = 0;
        #endregion

        #region Functions
        public FrmDesignation()
        {
            InitializeComponent();
        }
        public void FillDepartmentComboBox()// with dummy table
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch = new DmnSearch();
            objSearch.Select = "DepartmentId,DepartmentName";
            objSearch.Table = " tblDepartment ";
            objSearch.WhereNdGroupBy = "Where Status = 0 AND BranchId = " + BranchId;
            var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            mcmbDepartment.DataSource = gridSource;
            mcmbDepartment.ValueMember = "Id";
            mcmbDepartment.DisplayMember = "Value";
        }
        public bool IsSavable()
        {
            Control[] controllist = { mcmbDepartment, mtxtName, dtpWorkingHrs };
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void fillGridView()
        {
            dgvDesignation.Columns.Clear();
            dgvDesignation.DataSource = null;
            FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.ColumnHeaders = "DesignationId,Department,Designation,Working Hours";
            dmnSearch.Select = "ds.DesignationId as Id,dp.DepartmentName as Field1,ds.DesignationName as Field2,ds.WorkingHours as Field3";
            dmnSearch.Table = " tblDesignation as ds" +
                                " LEFT JOIN tblDepartment dp ON ds.DepartmentId = dp.DepartmentId";
            dmnSearch.WhereNdGroupBy = " Where ds.Status = 0 AND ds.BranchId = " + BranchId;
            dmnSearch.OrderBy = "Order By DesignationName ASC ";
            var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
            foreach (DataGridViewColumn col in gridSource.Columns)
            {
                dgvDesignation.Columns.Add(col.Clone() as DataGridViewColumn);
            }
            dgvDesignation.DataSource = gridSource.DataSource;
        }
        public void RefreshForm()
        {
            dtpWorkingHrs.Text = "08:00";
            DesignationId = 0;
            MetroTextBox[] listTbx = { mtxtName };
          objGeneral.ResetSelectedMetroTextBoxes(listTbx);
            fillGridView();
           
            mcmbDepartment.Focus();
            if(!string.IsNullOrWhiteSpace(mtxtName.Text))
            {
                mbtnSave.Enabled = false;
            }
            hlblHeader.Text = "Designation";

        }
        public Boolean NameExist()
        {
            if (Convert.ToString(mtxtName.Text) != "")
            {
                BllAccountGroup bllAccountGroup = new BllAccountGroup();
                BllSearch<DmnAccountGroup> bllSearch = new BllSearch<DmnAccountGroup>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.ColumnHeaders = "Id,DesignationName";
                objSearch.Select = " COUNT(DesignationId) As Count";
                objSearch.Table = " tblDesignation ";

                if (DesignationId == 0)
                {
                    objSearch.WhereNdGroupBy = " Where DepartmentId= '" + Convert.ToInt32(mcmbDepartment.SelectedValue) + "' AND DesignationName = '" + Convert.ToString(mtxtName.Text) + "' AND Status=0 AND BranchId = " + BranchId;
                }
                else
                {
                    objSearch.WhereNdGroupBy = " Where DepartmentId= '" + Convert.ToInt32(mcmbDepartment.SelectedValue) + "' AND DesignationId!= " + DesignationId + " AND Status=0 AND DesignationName = '" + Convert.ToString(mtxtName.Text) + "' AND BranchId = " + BranchId;
                }
                //objSearch.OrderBy = "Order By DesignationName ASC";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource[0].Count >= 1)
                {
                    MetroMessageBox.Show(this, "Designation Name " + Convert.ToString(mtxtName.Text) + " Already Exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    mtxtName.Text = "";
                    mtxtName.Focus();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;

        }
        public void dgvFrmDesignation()
        {
            if (dgvDesignation.Rows.Count >= 0)
            {
                DesignationId = Convert.ToInt32(dgvDesignation.CurrentRow.Cells[0].Value);
                BllDesignation bllDesignation = new BllDesignation();
                BllSearch<DmnDesignation> bllSearch = new BllSearch<DmnDesignation>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.ColumnHeaders = "DesignationId,DepartmentId,DesignationName,Working Hours";
                objSearch.Select = " DesignationId,DepartmentId,DesignationName,WorkingHours";
                objSearch.Table = " tblDesignation";
                objSearch.WhereNdGroupBy = "Where DesignationId = " + DesignationId;
                objSearch.OrderBy = "Order By DesignationName ASC ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                mtxtName.Text = Convert.ToString(gridSource[0].DesignationName);
                dtpWorkingHrs.Text = Convert.ToString(gridSource[0].WorkingHours);
                mcmbDepartment.SelectedValue = gridSource[0].DepartmentId;
                DesignationId = Convert.ToInt32(gridSource[0].DesignationId);
                hlblHeader.Text = "Designation [Edit]";
                mcmbDepartment.Focus();
                mbtnSave.Enabled = false;
           
            }
        }
        #endregion

        #region Events
        private void MbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsSavable())
                {
                    if(NameExist())
                    {
                        bool savestatus;
                        DmnDesignation dmnDesignation = new DmnDesignation();
                        BllDesignation bllDesignation = new BllDesignation();
                        dmnDesignation.DepartmentId = Convert.ToInt32(mcmbDepartment.SelectedValue);
                        dmnDesignation.WorkingHours = dtpWorkingHrs.Text;
                        dmnDesignation.DesignationName = Convert.ToString(mtxtName.Text);
                        dmnDesignation.BranchId = BranchId;
                        dmnDesignation.Status = 0;
                        if (DesignationId == 0)
                        {
                            dmnDesignation.CreatedBy = UserId;
                            var lstData = bllDesignation.AddData("prdInsertDesignation", dmnDesignation);
                            savestatus = lstData;
                            if (savestatus)
                                MetroMessageBox.Show(this, "\n" + mtxtName.Text+ " Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                            else
                                MetroMessageBox.Show(this, "\n" + mtxtName.Text+ " Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                        else
                        {
                            dmnDesignation.ModifiedBy = UserId;
                            dmnDesignation.ModifiedDateTime = DateTime.Now;
                            dmnDesignation.DesignationId = DesignationId;
                            var lstData = bllDesignation.UpdateData("prdUpdateDesignation", dmnDesignation);
                            savestatus = lstData;
                            if (savestatus)
                                MetroMessageBox.Show(this, "\n" +mtxtName.Text+" Updated successfully ", "Updated  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            else
                                MetroMessageBox.Show(this, "\n" + mtxtName.Text+" Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                        RefreshForm();
                        fillGridView();
                        dtpWorkingHrs.Text = "08:00";
                        
                    }
                    if (string.IsNullOrWhiteSpace(mtxtName.Text))
                    {
                        mbtnSave.Enabled = false;
                    }
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
                FillDepartmentComboBox();
                if (string.IsNullOrWhiteSpace(mtxtName.Text))
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
        private void FrmDesignation_Load(object sender, EventArgs e)
        {
            try
            {
                fillGridView();
               
                FillDepartmentComboBox();
                dtpWorkingHrs.Text = "08:00";
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
       
        private void DgvDesignation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (dgvDesignation.Rows.Count >= 0)
                //{
                //    DesignationId = Convert.ToInt32(dgvDesignation.CurrentRow.Cells[0].Value);
                //    BllDesignation bllDesignation = new BllDesignation();
                //    BllSearch<DmnDesignation> bllSearch = new BllSearch<DmnDesignation>();
                //    DmnSearch objSearch = new DmnSearch();
                //    objSearch = new DmnSearch();
                //    objSearch.ColumnHeaders = "DesignationId,DepartmentId,DesignationName,Working Hours";
                //    objSearch.Select = " DesignationId,DepartmentId,DesignationName,WorkingHours";
                //    objSearch.Table = " tblDesignation";
                //    objSearch.WhereNdGroupBy = "Where DesignationId = " + DesignationId;
                //    objSearch.OrderBy = "Order By DesignationName ASC ";
                //    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                //    mtxtName.Text = Convert.ToString(gridSource[0].DesignationName);
                //    dtpWorkingHrs.Text = Convert.ToString(gridSource[0].WorkingHours);
                //    mcmbDepartment.SelectedValue = gridSource[0].DepartmentId;
                //    DesignationId = Convert.ToInt32(gridSource[0].DesignationId);
                //    hlblHeader.Text = "Designation [Edit]";
                //    mcmbDepartment.Focus();
                //}
                dgvFrmDesignation();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void DgvDesignation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgvDesignation.Rows.Count > 0)
                {
                    
                    bool deletestatus;
                    DesignationId = Convert.ToInt32(dgvDesignation.CurrentRow.Cells[0].Value);
                    if (e.KeyCode == Keys.Delete)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Do you want to delete"+ dgvDesignation.CurrentRow.Cells[1].Value.ToString() +"?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            DmnDesignation dmnDesignation = new DmnDesignation();
                            BllDesignation bllDesignation = new BllDesignation();
                            dmnDesignation.DesignationId = DesignationId;
                            var lstData = bllDesignation.DeleteById("prdDeleteDesignation", dmnDesignation);
                            deletestatus = lstData;
                            if (deletestatus)
                            {
                                fillGridView();
                                RefreshForm();
                                MetroMessageBox.Show(this, "Successfully deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                            }
                            else
                                MetroMessageBox.Show(this, "Delete failed " + mtxtName.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                           
                        }
                    }
                    dgvDesignation.Focus();
                    if (e.KeyCode == Keys.Enter)
                    {
                        dgvFrmDesignation();
                        mcmbDepartment.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        
        private void FrmDesignation_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(mtxtName.Text))
                {
                    if (e.KeyCode != Keys.F2 && e.KeyCode != Keys.F5)
                    {
                        mbtnSave.Enabled = true;
                    }
                }
                if (e.KeyCode == Keys.F2 && mbtnSave.Enabled == true)
                    MbtnSave_Click(sender, e);
                if (e.KeyCode == Keys.F5)
                    MbtnRefresh_Click(sender, e);
                if (e.KeyCode == Keys.Escape)
                    this.Close();

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mcmbDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                objGeneral.NextConrolOnEnterPress(sender, e);
                if(e.KeyCode==Keys.F12)
                {
                    FrmDepartment frmDepartment = new FrmDepartment();
                    frmDepartment.ShowDialog();
                    FillDepartmentComboBox();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }
        private void dtpWorkingHrs_ValueChanged(object sender, EventArgs e)
        {

            try
            {

                //var min = new TimeSpan(08, 00, 00);
                //var max = new TimeSpan(12, 00, 00);
                ////if(dtpWorkingHrs.Value.TimeOfDay<min || dtpWorkingHrs.Value.TimeOfDay>max)
                //if (dtpWorkingHrs.Text < min || dtpWorkingHrs.Text > max)
                //{
                //    MetroMessageBox.Show(this, "invalid time limit ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                //    mbtnSave.Enabled = false;
                //}

                if (dtpWorkingHrs.Text != "08:00")
                {
                    string min = "08:00";
                    string max = "12:00";
                    if (Convert.ToDateTime(dtpWorkingHrs.Text) < Convert.ToDateTime(min) || Convert.ToDateTime(dtpWorkingHrs.Text) > Convert.ToDateTime(max))
                    {
                        MetroMessageBox.Show(this, "invalid time limit ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        dtpWorkingHrs.Text = "08:00";
                        //mbtnSave.Enabled = false;
                    }

                }
                
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }
        private void mcmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {

                if(!string.IsNullOrWhiteSpace(mtxtName.Text))
                {
                    mbtnSave.Enabled = true;
                }
                
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }


        }
        private void mtxtName_Leave(object sender, EventArgs e)
        {
            try
            {
                CultureInfo cI = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cI.TextInfo;
                mtxtName.Text = textInfo.ToTitleCase(mtxtName.Text);
                mtxtName.Select(mtxtName.Text.Length, 1);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        #endregion


    }
}
