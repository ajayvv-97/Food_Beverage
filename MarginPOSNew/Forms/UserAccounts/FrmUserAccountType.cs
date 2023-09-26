using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms.UserControls;
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

namespace MarginPOSNew.Forms.UserAccounts
{
    public partial class FrmUserAccountType : MetroFramework.Forms.MetroForm
    {
        #region Declaration

        ClsGeneral objGeneral = new ClsGeneral();
        int UserAccountTypeId = 0;

        #endregion

        #region Functions
        public FrmUserAccountType()
        {
            InitializeComponent();
        }
        public bool IsSavable()
        {
            Control[] controllist = { mtxtName};
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
            {
                return false;
            }
            else if (IsUserAccTypeExist() == false)
            {
                MetroMessageBox.Show(this, "\nUser account type already exists", "Error !", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                mtxtName.Focus();
                return false;
            }
            else
            {
                return true;
            }

        }
        public bool IsUserAccTypeExist()
        {
            DmnUserAccountType dmnUserAccountType = new DmnUserAccountType();
            BllSearch<DmnUserAccountType> bllSearch = new BllSearch<DmnUserAccountType>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = " UserAccountType";
            dmnSearch.Table = " tblUserAccountType";
            if(UserAccountTypeId==0)
                dmnSearch.WhereNdGroupBy = "Where UserAccountType='" + mtxtName.Text + "' and Status=0";
            else
                dmnSearch.WhereNdGroupBy = "Where UserAccountType='" + mtxtName.Text + "' and Status=0 and UserAccountTypeId!='" + UserAccountTypeId + "'";
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
                dgvUserAccountType.Columns.Clear();
                dgvUserAccountType.DataSource = null;
                dgvUserAccountType.Refresh();
                FrmDataGridView<DmnUserAccountType> frmGridSearch = new FrmDataGridView<DmnUserAccountType>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.ColumnHeaders = "Id,Name";
                dmnSearch.Select = "UserAccountTypeId,UserAccountType";
                dmnSearch.Table = "tblUserAccountType";
                dmnSearch.WhereNdGroupBy = "Where Status = 0";
                dmnSearch.OrderBy = "Order By UserAccountType ASC";
                var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
                foreach(DataGridViewColumn col in gridSource.Columns)
                {
                    dgvUserAccountType.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvUserAccountType.DataSource = gridSource.DataSource;

            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,150);
            }
        }
        public void RefreshForm()
        {
            UserAccountTypeId = 0;
            hlTitle.Text = "User Account Type";
            objGeneral.ResetAllTextBoxes(this);
            fillGridView();
            mtxtName.Focus();
        }
        #endregion

        #region Events
        private void FrmUserAccountType_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshForm();
            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,150);
            }
        }
        private void FrmUserAccountType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode==Keys.Enter)
                objGeneral.NextConrolOnEnterPress(sender, e);
                 if (e.KeyCode == Keys.F2)
                    MbtnSave_Click(sender, e);
                 if (e.KeyCode == Keys.F5)
                    MbtnRefresh_Click(sender, e);
                 if (e.KeyCode == Keys.Escape)
                    this.Close();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,150);
            }
        }
        private void MbtnSave_Click(object sender, EventArgs e)
        {
            bool saveStatus;
            try
            {
                if(IsSavable())
                {

                    BllUserAccountType bllUserAccountType = new BllUserAccountType();
                    DmnUserAccountType dmnUserAccountType = new DmnUserAccountType();
                    dmnUserAccountType.UserAccountType = mtxtName.Text;
                    if (UserAccountTypeId == 0)
                    {
                        dmnUserAccountType.CreatedBy = FrmDashboard.UserId;
                        saveStatus = bllUserAccountType.AddData("prdInsertUserAccountType", dmnUserAccountType);
                        if (saveStatus)
                           MetroMessageBox.Show(this, "\nSaved successfully " + mtxtName.Text, "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk,150);
                    }
                    else
                    {
                        dmnUserAccountType.UserAccountTypeId = UserAccountTypeId;
                        dmnUserAccountType.ModifiedBy = FrmDashboard.UserId;
                        saveStatus = bllUserAccountType.UpdateData("prdUpdateUserAccountType", dmnUserAccountType);
                        if (saveStatus)
                           MetroMessageBox.Show(this, "\nUpdated successfully " + mtxtName.Text, "Success ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk,150);
                    }
                    RefreshForm();
                }
              
            }
            catch(Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,150);
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
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,150);
            }
        }
        private void DgvUserAccountType_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Delete && dgvUserAccountType.Rows.Count>0)
                {
                    DialogResult dialogResult = MetroMessageBox.Show(this, "\nDo you want to delete " + dgvUserAccountType.CurrentRow.Cells[1].Value.ToString() + " ? ", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.Yes)
                    {
                        BllUserAccountType bllUserAccountType = new BllUserAccountType();
                        DmnUserAccountType dmnUserAccountType = new DmnUserAccountType();
                        dmnUserAccountType.UserAccountTypeId = Convert.ToInt32(dgvUserAccountType.CurrentRow.Cells[0].Value);
                        dmnUserAccountType.ModifiedBy = FrmDashboard.UserId;
                        bllUserAccountType.DeleteById("prdDeleteUserAccountType", dmnUserAccountType);
                        MetroMessageBox.Show(this, "\nDeleted successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Asterisk,150);
                        RefreshForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,150);
            }
        }
        private void DgvUserAccountType_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                hlTitle.Text = "User Account Type [Edit]";
                UserAccountTypeId = Convert.ToInt32(dgvUserAccountType.CurrentRow.Cells[0].Value);
                mtxtName.Text = dgvUserAccountType.CurrentRow.Cells[1].Value.ToString();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,150);
            }
        }

        #endregion
    }
}
