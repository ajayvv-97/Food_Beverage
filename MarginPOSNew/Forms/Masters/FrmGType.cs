using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
using MarginPOSNew.Interfaces;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarginPOSNew.Forms.Masters
{
    public partial class FrmGType : MetroFramework.Forms.MetroForm
    {
        #region Declaration

        ClsGeneral objGeneral = new ClsGeneral();
        int GTypeId = 0;
        int Status = 0;
        #endregion

        #region Functions
        public FrmGType()
        {
            InitializeComponent();
        }
        public bool IsSavable()
        {
            Control[] controllist = { mtxtName };
            if(objGeneral.IsControlNullOrDefault(this,controllist)==true)
            {
                return false;
            }
            else if(IsGTypeExist()==false)
            {
                MetroMessageBox.Show(this, "General Type already exist", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsGTypeExist()
        {
            DmnGType dmnGType = new DmnGType();
            BllSearch<DmnGType> bllSearch = new BllSearch<DmnGType>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = " GType";
            dmnSearch.Table = " tblGType";
            if(GTypeId==0)
                dmnSearch.WhereNdGroupBy = "Where GType='" + mtxtName.Text + "' and Status=0";
            else
                dmnSearch.WhereNdGroupBy = "Where GType='" + mtxtName.Text + "' and Status=0 and GTypeId!='" + GTypeId + "'";
            var user = bllSearch.GetSearchData("prdSearch", dmnSearch);
            if (user.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public void fillGridView()
        {
            try
            {
                dgvGType.Columns.Clear();
                dgvGType.DataSource = null;
                dgvGType.Refresh();
                FrmDataGridView<DmnGType> frmGridSearch=new FrmDataGridView<DmnGType>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.ColumnHeaders = "Id,Name";
                dmnSearch.Select = "GTypeId,GType";
                dmnSearch.Table = "tblGType";
                dmnSearch.WhereNdGroupBy = "Where Status = 2 ";
                dmnSearch.OrderBy = "Order By GType ASC";
                var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
                foreach (DataGridViewColumn col in gridSource.Columns)
                {
                    dgvGType.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvGType.DataSource = gridSource.DataSource;

            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, "Error Message/n" + ex.Message + "\nError code\n" + ex.StackTrace);
            }
        }
        public void RefreshForm()
        {
            GTypeId = 0;
            hlTitle.Text = "General Type";
            objGeneral.ResetAllTextBoxes(this);
            fillGridView();
            mtxtName.Focus();
        }

        #endregion

        #region Events
        private void FrmGType_Load(object sender, EventArgs e)
        {
            try
            {
                mtxtName.Clear();
                fillGridView();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,150);
            }
        }
        private void FrmGType_KeyDown(object sender, KeyEventArgs e)
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
        private void MbtnSave_Click(object sender, EventArgs e)
        {
            bool saveStatus;
            try
            {
                if(IsSavable())
                {
                    BllGType bllGType = new BllGType();
                    DmnGType dmnGType = new DmnGType();
                    dmnGType.GType = mtxtName.Text.Trim().ToUpper();
                    if (GTypeId == 0)
                    {
                        dmnGType.CreatedBy = FrmDashboard.UserId;
                        saveStatus = bllGType.AddData("prdInsertGType", dmnGType);
                        if (saveStatus)
                            MetroMessageBox.Show(this, "Saved successfully " + mtxtName.Text, "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        if(Status != Convert.ToInt32( StatusValue.Locked))
                        {
                            dmnGType.GTypeId = GTypeId;
                            dmnGType.ModifiedBy = FrmDashboard.UserId;
                            saveStatus = bllGType.UpdateData("prdUpdateGType", dmnGType);
                            if (saveStatus)
                                MetroMessageBox.Show(this, "Saved successfully " + mtxtName.Text, "Updated  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        }
                    }
                    
                }
                RefreshForm();
            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this,ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void MbtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshForm();
            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DgvGType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            { 
                if (e.KeyCode == Keys.Delete && dgvGType.Rows.Count>0 && Status != Convert.ToInt32(StatusValue.Locked))
                {
                    DialogResult dialogResult = MetroMessageBox.Show(this, "Are you sure you want to delete "+dgvGType.CurrentRow.Cells[1].Value.ToString()+" ? ", "Delete ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        BllGType bllGType = new BllGType();
                        DmnGType dmnGType = new DmnGType();
                        dmnGType.GTypeId = Convert.ToInt32(dgvGType.CurrentRow.Cells[0].Value);
                        dmnGType.ModifiedBy = FrmDashboard.UserId;
                        bllGType.DeleteById("prdDeleteGType", dmnGType);
                        MetroMessageBox.Show(this, "Deleted successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        RefreshForm();
                    }
                }
                dgvGType.Focus();
                if (e.KeyCode == Keys.Enter)
                {
                    fillGridView();
                    mtxtName.Focus();
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DgvGType_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                mtxtName.Focus();
                hlTitle.Text = "General Type [Edit]";
                mtxtName.Text = dgvGType.CurrentRow.Cells[1].Value.ToString();
                GTypeId = Convert.ToInt32(dgvGType.CurrentRow.Cells[0].Value);

                DmnGType dmnGType = new DmnGType();
                BllSearch<DmnGType> bllSearch = new BllSearch<DmnGType>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = " Status";
                dmnSearch.Table = " tblGType";
                dmnSearch.WhereNdGroupBy = "Where GTypeId='" + GTypeId + "'";
                var statusSearch = bllSearch.GetSearchData("prdSearch", dmnSearch);
                Status= statusSearch[0].Status;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FrmGType_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(mtxtName.Text))
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
            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


    }
}
