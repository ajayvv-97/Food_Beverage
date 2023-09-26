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
using System.Threading;
using System.Globalization;

namespace MarginPOSNew.Forms.Masters
{
    public partial class FrmItemClass : MetroFramework.Forms.MetroForm
    {
        #region Declaration
        ClsGeneral objGeneral = new ClsGeneral();
        long BranchId = Convert.ToInt32(FrmDashboard.BranchId);
        long UserId = Convert.ToInt32(FrmDashboard.UserId);
        long ItemClassId = 0;
        #endregion
        #region Functions
        public FrmItemClass()
        {
            InitializeComponent();
        }
        public bool IsSavable()
        {
            Control[] controllist = { mtxtName };
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
            dgvItemClass.Columns.Clear();
            dgvItemClass.DataSource = null;
            FrmDataGridView<DmnItemClass> frmGridSearch = new FrmDataGridView<DmnItemClass>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.ColumnHeaders = "Id,Item Class Name";
            dmnSearch.Select = "ItemClassId,ItemClassName";
            dmnSearch.Table = " tblItemClass ";
            dmnSearch.WhereNdGroupBy = "Where Status = 0 AND BranchId = " + BranchId;
            dmnSearch.OrderBy = "Order By ItemClassName ";
            var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
            foreach (DataGridViewColumn col in gridSource.Columns)
            {
                dgvItemClass.Columns.Add(col.Clone() as DataGridViewColumn);
            }
            dgvItemClass.DataSource = gridSource.DataSource;
            dgvItemClass.Refresh();
            hlTitle.Text = "Item Class";
            
        }
        public void RefreshForm()
        {
            ItemClassId = 0;
            mtxtName.Clear();
            
            fillGridView();
            if(string.IsNullOrWhiteSpace(mtxtName.Text))
            {
                mbtnSave.Enabled = false;
               
            }
            mtxtName.Focus();

        }
        public void dgvFrmItemClass()
        {
            if (dgvItemClass.Rows.Count >= 0)
            {
                ItemClassId = Convert.ToInt32(dgvItemClass.CurrentRow.Cells[0].Value);
                BllItemClass bllItemClass = new BllItemClass();
                BllSearch<DmnItemClass> bllSearch = new BllSearch<DmnItemClass>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.ColumnHeaders = "Id,Item Class";
                objSearch.Select = " ItemClassId,ItemClassName";
                objSearch.Table = " tblItemClass ";
                objSearch.WhereNdGroupBy = "Where ItemClassId = " + ItemClassId;
                objSearch.OrderBy = "Order By ItemClassName ";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                mtxtName.Text = Convert.ToString(gridSource[0].ItemClassName);
                ItemClassId = Convert.ToInt32(gridSource[0].ItemClassId);
                hlTitle.Text = "Item Class [Edit]";
                mtxtName.Focus();
                mbtnSave.Enabled = false;
                
            }
        }
        #endregion
        #region Events
        private void FrmItemClass_Load(object sender, EventArgs e)
        {
            try
            {
                mtxtName.Focus();
                fillGridView();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void DgvItemClass_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgvItemClass.Rows.Count > 0)
                {
                    
                    ItemClassId = Convert.ToInt32(dgvItemClass.CurrentRow.Cells[0].Value);
                    if (e.KeyCode == Keys.Delete)
                    {
                        bool deletestatus;
                        DialogResult result = MetroMessageBox.Show(this, "Do you want to delete " + dgvItemClass.CurrentRow.Cells[1].Value.ToString() + " ? ", "Delete ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            DmnItemClass dmnItemClass = new DmnItemClass();
                            BllItemClass bllItemClass = new BllItemClass();
                            dmnItemClass.ItemClassId = ItemClassId;
                            var lstData = bllItemClass.DeleteById("prdDeleteItemClass", dmnItemClass);
                            deletestatus = lstData;
                            if (deletestatus)
                            {
                                fillGridView();
                                RefreshForm();
                                MetroMessageBox.Show(this, " Successfully deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                            }
                            else
                                MetroMessageBox.Show(this, " Delete failed " + mtxtName.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                          
                        }
                    }
                    dgvItemClass.Focus();
                    if (e.KeyCode == Keys.Enter)
                    {
                        dgvFrmItemClass();
                        mtxtName.Focus();
                    }
                }
               
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void FrmItemClass_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                //objGeneral.NextConrolOnEnterPress(sender, e);
                if(e.KeyCode==Keys.Escape)
                {
                    this.Close();
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
                    bool savestatus;
                    DmnItemClass dmnItemClass = new DmnItemClass();
                    BllItemClass bllItemClass = new BllItemClass();
                    dmnItemClass.ItemClassName = mtxtName.Text;
                    dmnItemClass.BranchId = BranchId;
                    dmnItemClass.Status = 0;
                    if (ItemClassId == 0)
                    {
                        dmnItemClass.CreatedBy = UserId;
                        var lstData = bllItemClass.AddData("prdInsertItemClass", dmnItemClass);
                        savestatus = lstData;
                        savestatus = lstData;
                        if (savestatus)
                            MetroMessageBox.Show(this,mtxtName.Text+ " Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                        else
                            MetroMessageBox.Show(this,mtxtName.Text+ " Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                    }
                    else
                    {
                        dmnItemClass.ModifiedBy = UserId;
                        dmnItemClass.ModifiedDateTime = DateTime.Now;
                        dmnItemClass.ItemClassId = ItemClassId;
                        var lstData = bllItemClass.UpdateData("prdUpdateItemClass", dmnItemClass);
                        savestatus = lstData;
                        if (savestatus)
                            MetroMessageBox.Show(this,mtxtName.Text+ " Updated successfully ", "Updated  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        else
                            MetroMessageBox.Show(this,mtxtName.Text+ " Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        hlTitle.Text = "Department";
                    }
                  
                    fillGridView();
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
        private void DgvItemClass_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (dgvItemClass.Rows.Count >= 0)
                //{
                //    ItemClassId = Convert.ToInt32(dgvItemClass.CurrentRow.Cells[0].Value);
                //    BllItemClass bllItemClass = new BllItemClass();
                //    BllSearch<DmnItemClass> bllSearch = new BllSearch<DmnItemClass>();
                //    DmnSearch objSearch = new DmnSearch();
                //    objSearch = new DmnSearch();
                //    objSearch.ColumnHeaders = "Id,Item Class";
                //    objSearch.Select = " ItemClassId,ItemClassName";
                //    objSearch.Table = " tblItemClass ";
                //    objSearch.WhereNdGroupBy = "Where ItemClassId = " + ItemClassId;
                //    objSearch.OrderBy = "Order By ItemClassName ";
                //    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                //    mtxtName.Text = Convert.ToString(gridSource[0].ItemClassName);
                //    ItemClassId = Convert.ToInt32(gridSource[0].ItemClassId);
                //    hlTitle.Text = "Item Class [Edit]";
                //}
                dgvFrmItemClass();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        private void MtxtName_Leave(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToString(mtxtName.Text) != "")
                { 
                    
                    BllItemClass bllItemClass = new BllItemClass();
                    BllSearch<DmnItemClass> bllSearch = new BllSearch<DmnItemClass>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch = new DmnSearch();
                    objSearch.ColumnHeaders = "I,Item Class";
                    objSearch.Select = " COUNT(ItemClassName) As Count";
                    objSearch.Table = " tblItemClass ";

                    if (ItemClassId == 0)
                    {
                        objSearch.WhereNdGroupBy = " Where ItemClassName = '" + Convert.ToString(mtxtName.Text) + "' AND Status=0 AND BranchId = " + BranchId;
                    }
                    else
                    {
                        objSearch.WhereNdGroupBy = " Where ItemClassId!= " + ItemClassId + " AND Status=0 AND ItemClassName = '" + Convert.ToString(mtxtName.Text) + "' AND BranchId = " + BranchId;
                    }
                    //objSearch.OrderBy = "Order By ItemClassName ASC";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSource[0].Count == 1)
                    {
                        MetroMessageBox.Show(this, "Item Class Name " + Convert.ToString(mtxtName.Text) + " Already exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        mtxtName.Text = "";
                        mtxtName.Focus();
                    }
                    
                  
                      
                }
                // first letter Capslock
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
        private void FrmItemClass_KeyUp(object sender, KeyEventArgs e)
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

        private void mtxtName_KeyPress(object sender, KeyPressEventArgs e)
        {
           
        }
        #endregion


    }
}
