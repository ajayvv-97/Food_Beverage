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
    public partial class FrmAccountGroup : MetroFramework.Forms.MetroForm
    {
        #region Declaration
        ClsGeneral objGeneral = new ClsGeneral();
        long BranchId = Convert.ToInt32(FrmDashboard.BranchId);
        long UserId = Convert.ToInt32(FrmDashboard.UserId);
        long AccountGroupId = 0;
        #endregion

        #region Functions
        public FrmAccountGroup()
        {
            InitializeComponent();
        }
        public bool IsSavable()
        {
            Control[] controllist = { mtxtName,mtxtCode,mtxtSrlNo };
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
            dgvAccountGroup.Columns.Clear();
            dgvAccountGroup.DataSource = null;
            FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.ColumnHeaders = "Id,Account Group Name,Account Code,Account Sr.No";
            dmnSearch.Select = "AccountGroupId as Id,AccountGroupName as Field1,AccountGroupCode as Field2,SrlNoStart as Field3";
            dmnSearch.Table = "tblAccountGroup";
            dmnSearch.WhereNdGroupBy = "Where (Status=0 or Status=2) " ;
            dmnSearch.OrderBy = "Order By AccountGroupName ASC";
            var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
            foreach (DataGridViewColumn col in gridSource.Columns)
            {
                dgvAccountGroup.Columns.Add(col.Clone() as DataGridViewColumn);
            }
            dgvAccountGroup.DataSource = gridSource.DataSource;
        }
        public void RefreshForm()
        {
            AccountGroupId = 0;
            hlblHeader.Text = "Account Group";
            MetroTextBox[] listTbx = { mtxtName, mtxtCode, mtxtSrlNo };
            objGeneral.ResetSelectedMetroTextBoxes(listTbx);
            fillGridView();
            mtxtName.Focus();
            mtxtSrlNo.ReadOnly = false;
            if(string.IsNullOrWhiteSpace(mtxtName.Text))
            {
                mbtnSave.Enabled = false;
            }
        }
        public Boolean NameExist()
        {
            if (!string.IsNullOrWhiteSpace(mtxtName.Text))
            {
                BllAccountGroup bllAccountGroup = new BllAccountGroup();
                BllSearch<DmnAccountGroup> bllSearch = new BllSearch<DmnAccountGroup>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.ColumnHeaders = "Id,Account Group";
                objSearch.Select = " COUNT(AccountGroupName) As Count";
                objSearch.Table = " tblAccountGroup ";

                if (AccountGroupId == 0)
                {
                    objSearch.WhereNdGroupBy = " Where AccountGroupName LIKE '" + Convert.ToString(mtxtName.Text) + "%' COLLATE SQL_Latin1_General_CP1_CS_AS AND Status=0 AND BranchId = " + BranchId;
                }
                else
                {
                    objSearch.WhereNdGroupBy = " Where AccountGroupId!= " + AccountGroupId + " AND Status=0 AND AccountGroupName LIKE '" + Convert.ToString(mtxtName.Text) + "%' COLLATE SQL_Latin1_General_CP1_CS_AS AND BranchId = " + BranchId;
                }
                //objSearch.OrderBy = "Order By AccountGroupName ASC";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource[0].Count > 0)
                {
                    MetroMessageBox.Show(this, "Account group name '" + Convert.ToString(mtxtName.Text) + "' already exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
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
                BllAccountGroup bllAccountGroup = new BllAccountGroup();
                BllSearch<DmnAccountGroup> bllSearch = new BllSearch<DmnAccountGroup>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.ColumnHeaders = "Id,Account Group Code";
                objSearch.Select = " COUNT(AccountGroupCode) As Count";
                objSearch.Table = " tblAccountGroup ";

                if (AccountGroupId == 0)
                {
                    objSearch.WhereNdGroupBy = " Where AccountGroupCode LIKE '" + Convert.ToString(mtxtCode.Text) + "%' COLLATE SQL_Latin1_General_CP1_CS_AS AND Status=0 AND BranchId = " + BranchId;
                }
                else
                {
                    objSearch.WhereNdGroupBy = " Where AccountGroupId!= " + AccountGroupId + " AND Status=0 AND AccountGroupCode LIKE '" + Convert.ToString(mtxtCode.Text) + "%' COLLATE SQL_Latin1_General_CP1_CS_AS AND BranchId = " + BranchId;
                }
                //objSearch.OrderBy = "Order By AccountGroupName ASC";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource[0].Count > 0)
                {
                    MetroMessageBox.Show(this, "Account group code '" + Convert.ToString(mtxtCode.Text) + "' already exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
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
        public void dgvFrmAccountGroup()
        {
            if (dgvAccountGroup.Rows.Count >= 0)
            {
                AccountGroupId = Convert.ToInt32(dgvAccountGroup.CurrentRow.Cells[0].Value);
                BllAccountGroup bllAccountGroup = new BllAccountGroup();
                BllSearch<DmnAccountGroup> bllSearch = new BllSearch<DmnAccountGroup>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.ColumnHeaders = "Id,Account Group,Account Code,Sr.No";
                objSearch.Select = " AccountGroupId,AccountGroupName,AccountGroupCode,SrlNoStart";
                objSearch.Table = " tblAccountGroup ";
                objSearch.WhereNdGroupBy = "Where AccountGroupId = " + AccountGroupId;
                objSearch.OrderBy = "Order By AccountGroupName ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                mtxtName.Text = Convert.ToString(gridSource[0].AccountGroupName);
                mtxtCode.Text = Convert.ToString(gridSource[0].AccountGroupCode);
                mtxtSrlNo.Text = Convert.ToString(gridSource[0].SrlNoStart);
                AccountGroupId = Convert.ToInt32(gridSource[0].AccountGroupId);
                hlblHeader.Text = "Account Group [Edit]";
                mtxtSrlNo.ReadOnly = true;
                mbtnSave.Enabled = false;
                
            }
            mtxtName.Focus();
        }
        public void GenerateSrNo()
        {
            BllSearch<DmnAccountGroup> bllSearch = new BllSearch<DmnAccountGroup>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "MAX(SrlNoStart) as SrlNoStart";
            objSearch.Table = " tblAccountGroup ";
            objSearch.WhereNdGroupBy = "";
            var gridSourceSrlno = bllSearch.GetSearchData("prdSearch", objSearch);

            BllSearch<DmnGeneralOptions> bllSearchGn = new BllSearch<DmnGeneralOptions>();
            DmnSearch objSearchGn = new DmnSearch();
            objSearchGn.Select = "GeneralId,GTypeSubValue1";
            objSearchGn.Table = " tblGeneralOptions g LEFT JOIN tblGType gt ON g.GTypeId =  gt.GTypeId ";
            objSearchGn.WhereNdGroupBy = " Where gt.GType='CODE DIFFERENCE' AND (g.Status = 0 OR g.Status = 2)";
            var gridSourceGn = bllSearchGn.GetSearchData("prdSearch", objSearchGn);
            decimal difference = Convert.ToDecimal(gridSourceGn[0].GTypeSubValue1);
            decimal SrlNoStart = Convert.ToDecimal(gridSourceSrlno[0].SrlNoStart + difference);

            mtxtSrlNo.Text = Convert.ToString(SrlNoStart.ToString("0"));
        }
        #endregion

        #region Events
        private void FrmAccountGroup_Load(object sender, EventArgs e)
        {
            try
            {
                Refresh();
                fillGridView();

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
        }
        private void DgvAccountGroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgvAccountGroup.Rows.Count >= 0)
                {
                   
                    bool deletestatus;
                    int AccountGroupId = Convert.ToInt32(dgvAccountGroup.CurrentRow.Cells[0].Value);
                    if (e.KeyCode == Keys.Delete)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Do you want to delete " + dgvAccountGroup.CurrentRow.Cells[1].Value.ToString() + " ? ", "Delete ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            DmnAccountGroup dmnAccountGroup = new DmnAccountGroup();
                            BllAccountGroup bllAccountGroup = new BllAccountGroup();
                            dmnAccountGroup.AccountGroupId = AccountGroupId;
                            var lstData = bllAccountGroup.DeleteById("prdDeleteAccountGroup", dmnAccountGroup);
                            deletestatus = lstData;
                            if (deletestatus)
                            {
                                RefreshForm();
                                fillGridView();
                                MetroMessageBox.Show(this, "Successfully deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                            }
                            else
                                MetroMessageBox.Show(this, "Delete failed " , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                       
                    }
                    dgvAccountGroup.Focus();
                    if (e.KeyCode == Keys.Enter)
                    {
                        dgvFrmAccountGroup();
                        mtxtName.Focus();

                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void FrmAccountGroup_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(mtxtName.Text))
                {
                    if(e.KeyCode!=Keys.F2 && e.KeyCode!=Keys.F5 )
                    {
                        mbtnSave.Enabled = true;
                    }
                }
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
        }
        private void FrmAccountGroup_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2 && mbtnSave.Enabled==true)
                    MbtnSave_Click(sender, e);
                if (e.KeyCode == Keys.F5)
                    MbtnRefresh_Click(sender, e);
            }


            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error");
            }
           
        }
        private void MbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsSavable())
                {  
                    if(NameExist() && CodeExist())
                    {
                        bool savestatus;
                        DmnAccountGroup dmnAccountGroup = new DmnAccountGroup();
                        BllAccountGroup bllAccountGroup = new BllAccountGroup();
                        dmnAccountGroup.AccountGroupName = mtxtName.Text;
                        dmnAccountGroup.AccountGroupCode = mtxtCode.Text;
                        dmnAccountGroup.BranchId = BranchId;
                        dmnAccountGroup.Status = 0;
                        if (AccountGroupId == 0)
                        {
                            dmnAccountGroup.SrlNoStart = Convert.ToInt64(mtxtSrlNo.Text);
                            dmnAccountGroup.CreatedBy = UserId;
                            var lstData = bllAccountGroup.AddData("prdInsertAccountGroup", dmnAccountGroup);
                            savestatus = lstData;
                            if (savestatus)
                                MetroMessageBox.Show(this, mtxtName.Text+ " Successfully saved ", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                            else
                                MetroMessageBox.Show(this, mtxtName.Text+" Save failed ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                        else
                        {
                            dmnAccountGroup.ModifiedBy = UserId;
                            dmnAccountGroup.ModifiedDateTime = DateTime.Now;
                            dmnAccountGroup.AccountGroupId = AccountGroupId;
                            var lstData = bllAccountGroup.UpdateData("prdUpdateAccountGroup", dmnAccountGroup);
                            savestatus = lstData;
                            if (savestatus)
                                MetroMessageBox.Show(this, mtxtName.Text + " Updated successfully " , "Updated  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                            else
                                MetroMessageBox.Show(this, mtxtName.Text+ " Save failed " , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

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
        private void DgvAccountGroup_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (dgvAccountGroup.Rows.Count >= 0)
                //{
                //    AccountGroupId = Convert.ToInt32(dgvAccountGroup.CurrentRow.Cells[0].Value);
                //    BllAccountGroup bllAccountGroup = new BllAccountGroup();
                //    BllSearch<DmnAccountGroup> bllSearch = new BllSearch<DmnAccountGroup>();
                //    DmnSearch objSearch = new DmnSearch();
                //    objSearch = new DmnSearch();
                //    objSearch.ColumnHeaders = "Id,Account Group,Account Group Code,SrlNo";
                //    objSearch.Select = " AccountGroupId,AccountGroupName,AccountGroupCode,SrlNoStart";
                //    objSearch.Table = " tblAccountGroup ";
                //    objSearch.WhereNdGroupBy = "Where AccountGroupId = " + AccountGroupId;
                //    objSearch.OrderBy = "Order By AccountGroupName ";
                //    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                //    mtxtName.Text = Convert.ToString(gridSource[0].AccountGroupName);
                //    mtxtCode.Text = Convert.ToString(gridSource[0].AccountGroupCode);
                //    mtxtSrlNo.Text = Convert.ToString(gridSource[0].SrlNoStart);
                //    AccountGroupId = Convert.ToInt32(gridSource[0].AccountGroupId);
                //    hlblHeader.Text = "Account Group [Edit]";
                //    mtxtSrlNo.ReadOnly = true;
                //    mtxtName.Focus();
                //}
                dgvFrmAccountGroup();
            
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtSrlNo_KeyPress(object sender, KeyPressEventArgs e)
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
        private void mtxtName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                objGeneral.NextConrolOnEnterPress(sender, e);
                if (string.IsNullOrWhiteSpace(mtxtSrlNo.Text))
                {
                    GenerateSrNo();
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
                if (!string.IsNullOrWhiteSpace(mtxtName.Text))
                {
                    CultureInfo cI = Thread.CurrentThread.CurrentCulture;
                    TextInfo textInfo = cI.TextInfo;
                    mtxtName.Text = textInfo.ToTitleCase(mtxtName.Text);
                    mtxtName.Select(mtxtName.Text.Length, 1);
                    if (AccountGroupId <= 0)
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
                    if (string.IsNullOrWhiteSpace(mtxtSrlNo.Text))
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
            //    if (!string.IsNullOrWhiteSpace(mtxtName.Text) )
            //    {
            //        CultureInfo cI = Thread.CurrentThread.CurrentCulture;
            //        TextInfo textInfo = cI.TextInfo;
            //        mtxtName.Text = textInfo.ToTitleCase(mtxtName.Text);
            //        mtxtName.Select(mtxtName.Text.Length, 1);
                 
                   
            //        string input = mtxtName.Text.Trim();
            //        string Accode = "";
            //        string[] splitString = input.Split(' ');
            //        foreach (string s in splitString)
            //        {
            //            Accode = Accode + s.Substring(0, 1);
            //        }
            //        mtxtCode.Text = Accode;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            //}

        }
        private void mtxtName_TextChanged(object sender, EventArgs e)
        {

            //try

            //{
            //    if (!string.IsNullOrWhiteSpace(mtxtName.Text))
            //    {
            //        CultureInfo cI = Thread.CurrentThread.CurrentCulture;
            //        TextInfo textInfo = cI.TextInfo;
            //        mtxtName.Text = textInfo.ToTitleCase(mtxtName.Text);
            //        mtxtName.Select(mtxtName.Text.Length, 1);


            //        string input = mtxtName.Text.Trim();
            //        string Accode = "";
            //        string[] splitString = input.Split(' ');
            //        foreach (string s in splitString)
            //        {
            //            Accode = Accode + s.Substring(0, 1);
            //        }
            //        mtxtCode.Text = Accode;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            //}
        }
        #endregion


    }
}