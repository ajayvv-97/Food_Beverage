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
using System.Globalization;
using System.Threading;
namespace MarginPOSNew.Forms.Masters
{
    public partial class FrmDepartment : MetroFramework.Forms.MetroForm
    {
        #region Declaration
        ClsGeneral objGeneral = new ClsGeneral();
        long BranchId = Convert.ToInt32(FrmDashboard.BranchId);
        long UserId = Convert.ToInt32(FrmDashboard.UserId);
        long DepartmentId = 0;
        #endregion
        #region Functions
        public FrmDepartment()
        {
            InitializeComponent();
        }
        public bool IsSavable()
        {
            Control[] controllist = { mtxtName, mtxtCode, mtxtEmpSrlNo };
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
            dgvDepartment.Columns.Clear();
            dgvDepartment.DataSource = null;
            FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.ColumnHeaders = "Id,Department,Department Code,Employee Sr.No";
            dmnSearch.Select = "DepartmentId as Id,DepartmentName as Field1,DepartmentCode as Field2,EmployeeSrlNoStart as Field3";
            dmnSearch.Table = "tblDepartment";
            dmnSearch.WhereNdGroupBy = "Where Status = 0 and BranchId=" + BranchId;
            dmnSearch.OrderBy = "Order By DepartmentName ASC";
            var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
            foreach (DataGridViewColumn col in gridSource.Columns)
            {
                dgvDepartment.Columns.Add(col.Clone() as DataGridViewColumn);
            }
            dgvDepartment.DataSource = gridSource.DataSource;
            hlTitle.Text = "Department";
        }
        public void RefreshForm()
        {
            DepartmentId = 0;
            hlTitle.Text = "Department";
            MetroTextBox[] listTbx = { mtxtName, mtxtCode,mtxtEmpSrlNo };
            objGeneral.ResetSelectedMetroTextBoxes(listTbx);
            fillGridView();
            mtxtName.Focus();
            mtxtEmpSrlNo.ReadOnly = false;
            if(string.IsNullOrWhiteSpace(mtxtName.Text))
            {
                mbtnSave.Enabled = false;

            }
        }
        public Boolean NameExist()
        {
            if (!string.IsNullOrWhiteSpace(mtxtName.Text))
            {
                BllDepartment bllAccountGroup = new BllDepartment();
                BllSearch<DmnDepartment> bllSearch = new BllSearch<DmnDepartment>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.ColumnHeaders = "DepartmentName";
                objSearch.Select = " COUNT(DepartmentName) As Count";
                objSearch.Table = " tblDepartment ";

                if (DepartmentId == 0)
                {
                    objSearch.WhereNdGroupBy = " Where DepartmentName = '" + Convert.ToString(mtxtName.Text) + "' AND Status=0 AND BranchId = " + BranchId;
                }
                else
                {
                    objSearch.WhereNdGroupBy = " Where DepartmentId!= " + DepartmentId + " AND Status=0 AND DepartmentName = '" + Convert.ToString(mtxtName.Text) + "' AND BranchId = " + BranchId;
                }
                //objSearch.OrderBy = "Order By AccountGroupName ASC";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource[0].Count > 0)
                {
                    MetroMessageBox.Show(this, "Department Name '" + Convert.ToString(mtxtName.Text) + "' already exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
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
        public Boolean CodeExist()
        {
            if (!string.IsNullOrWhiteSpace(mtxtCode.Text))
            {
                BllDepartment bllAccountGroup = new BllDepartment();
                BllSearch<DmnDepartment> bllSearch = new BllSearch<DmnDepartment>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.ColumnHeaders = "DepartmentCode";
                objSearch.Select = " COUNT(DepartmentCode) As Count";
                objSearch.Table = " tblDepartment ";

                if (DepartmentId == 0)
                {
                    objSearch.WhereNdGroupBy = " Where DepartmentCode LIKE '" + Convert.ToString(mtxtCode.Text) + "%' COLLATE SQL_Latin1_General_CP1_CS_AS AND Status=0 AND BranchId = " + BranchId;
                }
                else
                {
                    objSearch.WhereNdGroupBy = " Where DepartmentId!= " + DepartmentId + " AND Status=0 AND DepartmentCode LIKE '" + Convert.ToString(mtxtCode.Text) + "%' COLLATE SQL_Latin1_General_CP1_CS_AS AND BranchId = " + BranchId;
                }
                //objSearch.OrderBy = "Order By AccountGroupName ASC";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource[0].Count > 0)
                {
                    MetroMessageBox.Show(this, "Department code '" + Convert.ToString(mtxtCode.Text) + "' already exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    mtxtCode.Text = "";
                    mtxtCode.Focus();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        public void GenerateSrNo()
        {
            BllSearch<DmnDepartment> bllSearch = new BllSearch<DmnDepartment>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "MAX(EmployeeSrlNoStart) as EmployeeSrlNoStart";
            objSearch.Table = " tblDepartment ";
            objSearch.WhereNdGroupBy = "";
            var gridSourceSrlno = bllSearch.GetSearchData("prdSearch", objSearch);

            BllSearch<DmnGeneralOptions> bllSearchGn = new BllSearch<DmnGeneralOptions>();
            DmnSearch objSearchGn = new DmnSearch();
            objSearchGn.Select = "GeneralId,GTypeSubValue1";
            objSearchGn.Table = " tblGeneralOptions g LEFT JOIN tblGType gt ON g.GTypeId =  gt.GTypeId ";
            objSearchGn.WhereNdGroupBy = " Where gt.GType='CODE DIFFERENCE' AND (g.Status = 0 OR g.Status = 2)";
            var gridSourceGn = bllSearchGn.GetSearchData("prdSearch", objSearchGn);
            decimal difference = Convert.ToDecimal(gridSourceGn[0].GTypeSubValue1);
            decimal SrlNoStart = Convert.ToDecimal(gridSourceSrlno[0].EmployeeSrlNoStart + difference);

            mtxtEmpSrlNo.Text = Convert.ToString(SrlNoStart.ToString("0"));
        }
        public void dgvFrmDepartment()
        {
            if (dgvDepartment.Rows.Count >= 0)
            {
                DepartmentId = Convert.ToInt32(dgvDepartment.CurrentRow.Cells[0].Value);
                DmnDepartment dmnDepartment = new DmnDepartment();
                BllSearch<DmnDepartment> bllSearch = new BllSearch<DmnDepartment>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.ColumnHeaders = "Id,Department,Department Code,EmpSrlNoStart";
                objSearch.Select = " DepartmentId,DepartmentName,DepartmentCode,EmployeeSrlNoStart";
                objSearch.Table = " tblDepartment ";
                objSearch.WhereNdGroupBy = " Where DepartmentId = " + DepartmentId;
                objSearch.OrderBy = " Order By DepartmentName ASC";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                mtxtName.Text = Convert.ToString(gridSource[0].DepartmentName);
                mtxtCode.Text = Convert.ToString(gridSource[0].DepartmentCode);
                mtxtEmpSrlNo.Text = Convert.ToString(gridSource[0].EmployeeSrlNoStart);
                DepartmentId = Convert.ToInt32(gridSource[0].DepartmentId);
                hlTitle.Text = "Department [Edit]";
                mtxtEmpSrlNo.ReadOnly = true;
                mtxtName.Focus();
                mbtnSave.Enabled = false;
            }

        }
        #endregion
        #region Events
        private void FrmDepartment_Load(object sender, EventArgs e)
        {
            try
            {
                mtxtName.Focus();
                RefreshForm();
                fillGridView();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void DgvDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgvDepartment.Rows.Count > 0)
                {
                    
                    bool deletestatus;
                    int DepartmentId = Convert.ToInt32(dgvDepartment.CurrentRow.Cells[0].Value);
                    if (e.KeyCode == Keys.Delete)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Do you want to delete " + dgvDepartment.CurrentRow.Cells[1].Value.ToString() + " ? ", "Delete ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            DmnDepartment dmnDepartment = new DmnDepartment();
                            BllDepartment bllDepartment = new BllDepartment();
                            dmnDepartment.DepartmentId = DepartmentId;
                            var lstData = bllDepartment.DeleteById("prdDeleteDepartment", dmnDepartment);
                            deletestatus = lstData;
                            if (deletestatus)
                            {
                               // mtxtName.Focus();
                               
                                
                                MetroMessageBox.Show(this, "Successfully deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                            }
                            else
                                MetroMessageBox.Show(this, "Delete failed " , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                            fillGridView();

                            RefreshForm();
                        }
                    } 
                    dgvDepartment.Focus();
                    if (e.KeyCode == Keys.Enter)
                    {
                        dgvFrmDepartment();
                        mtxtName.Focus();
                    }
                }
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
                    if (NameExist() && CodeExist())
                    {
                        bool savestatus;
                        DmnDepartment dmnDepartment = new DmnDepartment();
                        BllDepartment bllDepartment = new BllDepartment();
                        dmnDepartment.DepartmentName = mtxtName.Text;
                        dmnDepartment.DepartmentCode = mtxtCode.Text;
                        dmnDepartment.BranchId = BranchId;
                        dmnDepartment.Status = 0;
                        if (DepartmentId == 0)
                        {
                            dmnDepartment.EmployeeSrlNoStart = Convert.ToInt64(mtxtEmpSrlNo.Text);
                            dmnDepartment.CreatedBy = UserId;
                            var lstData = bllDepartment.AddData("prdInsertDepartment", dmnDepartment);
                            savestatus = lstData;
                            if (savestatus)
                                MetroMessageBox.Show(this,mtxtName.Text+ " Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                            else
                                MetroMessageBox.Show(this,mtxtName.Text+ " Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                        else
                        {
                            dmnDepartment.ModifiedBy = UserId;
                            dmnDepartment.ModifiedDateTime = DateTime.Now;
                            dmnDepartment.DepartmentId = DepartmentId;
                            var lstData = bllDepartment.UpdateData("prdUpdateDepartment", dmnDepartment);
                            savestatus = lstData;
                            if (savestatus)
                                MetroMessageBox.Show(this,mtxtName.Text+ " Updated successfully ", "Updated  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            else
                                MetroMessageBox.Show(this,mtxtName.Text+ " Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                            hlTitle.Text = "Department";
                        }
                        
                        fillGridView();
                       
                    }
                    RefreshForm();
                    mtxtName.Focus();
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
                if(!string.IsNullOrWhiteSpace(mtxtName.Text))
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
        private void DgvDepartment_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (dgvDepartment.Rows.Count >= 0)
                //{
                //    DepartmentId = Convert.ToInt32(dgvDepartment.CurrentRow.Cells[0].Value);
                //    DmnDepartment dmnDepartment = new DmnDepartment();
                //    BllSearch<DmnDepartment> bllSearch = new BllSearch<DmnDepartment>();
                //    DmnSearch objSearch = new DmnSearch();
                //    objSearch = new DmnSearch();
                //    objSearch.ColumnHeaders = "Id,Department,Department Code,EmpSrlNoStart";
                //    objSearch.Select = " DepartmentId,DepartmentName,DepartmentCode,EmployeeSrlNoStart";
                //    objSearch.Table = " tblDepartment ";
                //    objSearch.WhereNdGroupBy = " Where DepartmentId = " + DepartmentId;
                //    objSearch.OrderBy = " Order By DepartmentName ASC";
                //    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                //    mtxtName.Text = Convert.ToString(gridSource[0].DepartmentName);
                //    mtxtCode.Text = Convert.ToString(gridSource[0].DepartmentCode);
                //    mtxtEmpSrlNo.Text = Convert.ToString(gridSource[0].EmployeeSrlNoStart);
                //    DepartmentId = Convert.ToInt32(gridSource[0].DepartmentId);
                //    hlTitle.Text = "Department [Edit]";
                //    mtxtEmpSrlNo.ReadOnly = true;
                //    mtxtName.Focus();
                //}
                dgvFrmDepartment();
                
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtEmpSrlNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.IntegerTextBox(sender, e);
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        }
        private void FrmDepartment_KeyUp(object sender, KeyEventArgs e)
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
                //objGeneral.NextConrolOnEnterPress(sender, e);
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void mtxtName_KeyDown(object sender, KeyEventArgs e)
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

        private void mtxtCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                objGeneral.NextConrolOnEnterPress(sender, e);
                if (string.IsNullOrWhiteSpace(mtxtEmpSrlNo.Text))
                {
                    GenerateSrNo();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mtxtEmpSrlNo_KeyDown(object sender, KeyEventArgs e)
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
        private void mtxtName_Leave(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(mtxtName.Text))
                {
                    CultureInfo cI = Thread.CurrentThread.CurrentCulture;
                    TextInfo textInfo = cI.TextInfo;
                    mtxtName.Text = textInfo.ToTitleCase(mtxtName.Text);
                    mtxtName.Select(mtxtName.Text.Length, 1);

                    if (DepartmentId <= 0)
                    {
                        string input = mtxtName.Text.Trim();
                        string Accode = "";
                        string[] splitString = input.Split(' ');
                        foreach (string s in splitString)
                        {
                            Accode = Accode + s.Substring(0, 1);
                        }
                        mtxtCode.Text = Accode;
                    }

                    if (string.IsNullOrWhiteSpace(mtxtEmpSrlNo.Text))
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
        private void mtxtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
                

            //}
            //catch (Exception ex)
            //{
            //    MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            //}
        }


        #endregion

    }
}
