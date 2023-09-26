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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarginPOSNew.Forms.Masters
{
    public partial class FrmFinancialYear : MetroForm
    {

        #region Declarations


        ClsGeneral objGeneral = new ClsGeneral();// for validation
        int BranchId = Convert.ToInt32(FrmDashboard.BranchId);
        int UserId = Convert.ToInt32(FrmDashboard.UserId);
        int FinancialYearId = 0;//initially zero, value changes in update
        #endregion


        #region Functions
        public bool IsSavable()
        {
            Control[] controllist = { mtxtFinyrDesc };
            if (objGeneral.IsControlNullOrDefault(this, controllist) == true)
            {
                return false;
            }
            if (!IsToDateValidorNot())
            {
                return false;
            }

            else
            {
                return true;
            }
        }
        public bool IsFirstEntry()
        {
            DmnFinancialYear objCat = new DmnFinancialYear();
            BllSearch<DmnFinancialYear> bllSearch = new BllSearch<DmnFinancialYear>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "FinancialyearId ";
            objSearch.Table = " tblFinancialYear ";
            objSearch.WhereNdGroupBy = " Where Status=0";
            var idCount = bllSearch.GetSearchData("prdSearch", objSearch);
            if (idCount.Count > 0)
            {
                return false;
            }
            return true;
        }
        public bool IsToDateValidorNot()
        {
            if (mdtpFromDate.Value > mdtpToDate.Value)
            {
                MetroMessageBox.Show(this, "\n\n Date cannot be less than from date!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                return false;
            }
            return true;
        }
        public bool IsToDateValid()
        {
            if (mdtpFromDate.Value > mdtpToDate.Value)
            {
                MetroMessageBox.Show(this, "\n\n Date cannot be less than from date!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                return false;
            }
            DmnFinancialYear objCat = new DmnFinancialYear();
            BllSearch<DmnFinancialYear> bllSearch = new BllSearch<DmnFinancialYear>();
            DmnSearch objSearch = new DmnSearch();

            objSearch.Select = "FinancialyearId ";
            objSearch.Table = " tblFinancialYear ";
            if (FinancialYearId == 0)
            {
                objSearch.WhereNdGroupBy = " Where   FromDate= (SELECT CONVERT(VARCHAR(10),'" + mdtpFromDate.Value.ToString("yyyy-MM-dd") + "',126)) AND ToDate =(SELECT CONVERT(VARCHAR(10),'" + mdtpToDate.Value.ToString("yyyy-MM-dd") + "',126))";
            }
            else
            {
                objSearch.WhereNdGroupBy = " Where FinancialYearId<> " + FinancialYearId + " AND  FromDate=  (SELECT CONVERT(VARCHAR(10),'" + mdtpFromDate.Value.ToString("yyyy-MM-dd") + "',126)) AND   ToDate= (SELECT CONVERT(VARCHAR(10),'" + mdtpToDate.Value.ToString("yyyy-MM-dd") + "',126))";//noneed to check status here AND Status=0

            }
            var lstCategory = bllSearch.GetSearchData("prdSearch", objSearch);//lst category will have the no of id
            if (lstCategory.Count > 0)
            {
                MetroMessageBox.Show(this, "\n\n Date  already exists!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                //   mdtpToDate.Value = DateTime.Now.Date;
                mdtpToDate.Focus();
                return false;
            }
            return true;

        }

        public void fillGridView()
        {
            try
            {
                dgvFinancialYear.Columns.Clear();
                dgvFinancialYear.DataSource = null;
                
                // FrmDataGridView<DmnFinancialYear> frmGridSearch = new FrmDataGridView<DmnFinancialYear>();
               
                FrmDataGridView<DmnGridFields> frmGridSearch = new FrmDataGridView<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.ColumnHeaders = "FinancialYearId,Description,From Date,To Date,Status,Remarks";
                dmnSearch.Select = "FinancialYearId as Id,Description as Field1,FromDate as Field2,ToDate as Field3, case when [status]=0 then 'Active' else 'Inactive' end as  Field4 ,Remarks as Field5";
                dmnSearch.Table = "tblFinancialYear ";
                //dmnSearch.WhereNdGroupBy = "Where Status = 0";
                dmnSearch.OrderBy = "Order By FromDate ASC";
                var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
                foreach (DataGridViewColumn col in gridSource.Columns)
                {
                    dgvFinancialYear.Columns.Add(col.Clone() as DataGridViewColumn);
                }
                dgvFinancialYear.DataSource = gridSource.DataSource;

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
                FinancialYearId = 0;
                hlTitle.Text = "Financial Year";
                objGeneral.ResetAllTextBoxes(this);
                MetroDateTime[] dtplist = { mdtpFromDate, mdtpToDate };
                objGeneral.ResetSelectedMetroDateTime(dtplist);
                objGeneral.ResetAllTextBoxes(this);
                fillGridView();
                mtxtFinyrDesc.Focus();
                if(string.IsNullOrWhiteSpace(mtxtFinyrDesc.Text))
                {
                    mbtnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }
        public void dvgFrmFinancialYear()
        {
            if (dgvFinancialYear.Rows.Count > 0)
            {
                mtxtFinyrDesc.Focus();
                hlTitle.Text = "Financial Year [Edit]";
                FinancialYearId = Convert.ToInt32(dgvFinancialYear.CurrentRow.Cells[0].Value);
                mtxtFinyrDesc.Text = Convert.ToString(dgvFinancialYear.CurrentRow.Cells[1].Value);
                // mdtpFromDate.Value = Convert.ToDateTime(dgvFinancialYear.CurrentRow.Cells[2].Value);

                // mdtpFromDate.Text= DateTime.ParseExact(dgvFinancialYear.CurrentRow.Cells[2].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                mdtpFromDate.Text = dgvFinancialYear.CurrentRow.Cells[2].Value.ToString();


                //  mdtpToDate.MinDate = mdtpFromDate.Value;
                mdtpToDate.Text = dgvFinancialYear.CurrentRow.Cells[3].Value.ToString();

                if (Convert.ToString(dgvFinancialYear.CurrentRow.Cells[4].Value) == "Active")
                {
                    ckbActive.Checked = true;
                }
                else
                {
                    ckbActive.Checked = false;
                }
                mtxtRemarks.Text = Convert.ToString(dgvFinancialYear.CurrentRow.Cells[5].Value);
                mbtnSave.Enabled = false;
            }
        }

        #endregion


        #region Events
        public FrmFinancialYear()
        {
            InitializeComponent();
        }

        private void FrmFinancialYear_Load(object sender, EventArgs e)
        {
            try
            {
                //CultureInfo culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                //culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
                //culture.DateTimeFormat.LongTimePattern = "";
                //Thread.CurrentThread.CurrentCulture = culture;

                RefreshForm();
                // mdtpToDate.MinDate = mdtpFromDate.Value.Date;
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, 150);
            }
        }

        private void MbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsSavable())
                {
                    DmnFinancialYear dmnFinancial = new DmnFinancialYear();
                    BllFinancialYear objBll = new BllFinancialYear();
                    dmnFinancial.Description = mtxtFinyrDesc.Text.Trim();
                    dmnFinancial.FromDate = mdtpFromDate.Value.Date;//DateTime.ParseExact(mdtpFromDate.Text,"dd/MM/yyyy",CultureInfo.InvariantCulture);
                   //dmnFinancial.FromDate = DateTime.ParseExact(mdtpFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    dmnFinancial.ToDate = mdtpToDate.Value.Date;
                    dmnFinancial.Remarks = mtxtRemarks.Text.Trim();
                    if (ckbActive.Checked == true)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "\n This will be the active financial year.\nDo you want to continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 150);
                        if (result == DialogResult.Yes)
                        {
                            dmnFinancial.Status = 0;//zero means it sets as active in genoption via procedure
                        }
                        else
                        {
                            dmnFinancial.Status = 1;
                        }
                    }
                    else
                    {
                        dmnFinancial.Status = 1;
                    }

                    #region first entry
                    if (IsFirstEntry())
                    {
                        dmnFinancial.Status = 0;
                    }
                    #endregion

                    if (FinancialYearId == 0)
                    {
                        dmnFinancial.CreatedBy = UserId;
                        bool insertResult = objBll.AddData("prdInsertFinancialYear", dmnFinancial);
                        if (insertResult == true)
                        {
                            MetroMessageBox.Show(this, mtxtFinyrDesc.Text+" Successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                        }
                        else
                        {
                            MetroMessageBox.Show(this,mtxtFinyrDesc.Text+ " Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                        }
                    }
                    else
                    {
                        if (dmnFinancial.Status == 1)
                        {

                            #region checking any active financial exists

                            DmnFinancialYear objCat = new DmnFinancialYear();
                            BllSearch<DmnFinancialYear> bllSearch = new BllSearch<DmnFinancialYear>();
                            DmnSearch objSearch = new DmnSearch();
                            objSearch.Select = "FinancialyearId ";
                            objSearch.Table = " tblFinancialYear ";
                            objSearch.WhereNdGroupBy = " Where FinancialyearId<>'" + FinancialYearId + "' And  Status=0";
                            var idCount = bllSearch.GetSearchData("prdSearch", objSearch);
                            if (idCount.Count == 0)
                            {
                                MetroMessageBox.Show(this, "\nThere should be atleast one active financial year", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, 150);
                                return;
                            }

                            #endregion
                        }
                        else
                        {
                            dmnFinancial.ModifiedBy = UserId;
                            dmnFinancial.ModifiedDateTime = DateTime.Now;
                            dmnFinancial.FinancialYearId = FinancialYearId;
                            bool updateResult = objBll.UpdateData("prdUpdateFinancialYear", dmnFinancial);

                            if (updateResult == true)
                            {
                                MetroMessageBox.Show(this, mtxtFinyrDesc.Text + " Updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                            }
                            else
                            {
                                MetroMessageBox.Show(this, mtxtFinyrDesc.Text + " Update failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                            }
                        }
                        
                    }

                    RefreshForm();


                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, 150);
            }
        }

       

        private void FrmFinancialYear_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(mtxtFinyrDesc.Text))
                {
                    if (e.KeyCode != Keys.F2 && e.KeyCode != Keys.F5)
                    {
                        mbtnSave.Enabled = true;
                    }
                }
                objGeneral.NextConrolOnEnterPress(sender, e);
                if (e.KeyCode == Keys.F2 && mbtnSave.Enabled == true)
                    MbtnSave_Click(sender, e);
                if (e.KeyCode == Keys.F5)
                    mbtnRefresh_Click(sender, e);
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, 150);

            }
        }
       
        

      
        private void mtxtFinyrDesc_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                objGeneral.NextConrolOnEnterPress(sender, e);
                
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, 150);
            }
        }
       

        private void mdtpFromDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime frmdate = Convert.ToDateTime(mdtpFromDate.Text);

            DateTime newdate = frmdate.AddDays(365);
            mdtpToDate.Text = newdate.ToShortDateString();
            mtxtFinyrDesc.Text= frmdate + " To " + newdate;
            mbtnSave.Enabled = true;
        }

        private void mdtpToDate_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                //// mdtpToDate.MinDate = mdtpFromDate.Value;
                DateTime todate = Convert.ToDateTime(mdtpToDate.Text);

                DateTime newdate = todate.AddDays(-365);
                mdtpFromDate.Text = newdate.ToShortDateString();
                mbtnSave.Enabled = true;

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, 150);

            }
        }

        private void DgvFinancialYear_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                //if (dgvFinancialYear.Rows.Count > 0)
                //{
                //    mtxtFinyrDesc.Focus();
                //    hlTitle.Text = "Financial Year [Edit]";
                //    FinancialYearId = Convert.ToInt32(dgvFinancialYear.CurrentRow.Cells[0].Value);
                //    mtxtFinyrDesc.Text = Convert.ToString(dgvFinancialYear.CurrentRow.Cells[1].Value);
                //    // mdtpFromDate.Value = Convert.ToDateTime(dgvFinancialYear.CurrentRow.Cells[2].Value);

                //    // mdtpFromDate.Text= DateTime.ParseExact(dgvFinancialYear.CurrentRow.Cells[2].Value.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                //    mdtpFromDate.Text = dgvFinancialYear.CurrentRow.Cells[2].Value.ToString();


                //    //  mdtpToDate.MinDate = mdtpFromDate.Value;
                //    mdtpToDate.Text = dgvFinancialYear.CurrentRow.Cells[3].Value.ToString();

                //    if (Convert.ToString(dgvFinancialYear.CurrentRow.Cells[4].Value) == "Active")
                //    {
                //        ckbActive.Checked = true;
                //    }
                //    else
                //    {
                //        ckbActive.Checked = false;
                //    }
                //    mtxtRemarks.Text = Convert.ToString(dgvFinancialYear.CurrentRow.Cells[5].Value);
                //}
                dvgFrmFinancialYear();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void DgvFinancialYear_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (dgvFinancialYear.Rows.Count > 0)
                {
                    
                    int FinancialYearId = Convert.ToInt32(dgvFinancialYear.CurrentRow.Cells[0].Value);
                    if (e.KeyCode == Keys.Delete)
                    {
                        if (dgvFinancialYear.CurrentRow.Cells[4].Value.ToString() == "Active")
                        {
                            MetroMessageBox.Show(this, " Cannot delete active financial year", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                        }

                        else
                        {
                            DialogResult result = MetroMessageBox.Show(this, " Do You Want to delete " + dgvFinancialYear.CurrentRow.Cells[1].Value.ToString() + "?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, 150);
                            if (result == DialogResult.Yes)
                            {
                                DmnFinancialYear dmnFinYear = new DmnFinancialYear();
                                BllFinancialYear bllFinYear = new BllFinancialYear();
                                dmnFinYear.FinancialYearId = FinancialYearId;
                                dmnFinYear.ModifiedBy = UserId;
                                bool deleteStatus = bllFinYear.DeleteById("prdDeleteFinancialYear", dmnFinYear);

                                if (deleteStatus == true)
                                {
                                    MetroMessageBox.Show(this, " Successfully deleted", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information, 150);
                                }
                                else
                                {
                                    MetroMessageBox.Show(this, "  Delete failed", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);

                                }
                                RefreshForm();

                            }
                        }
                    }
                    dgvFinancialYear.Focus();
                    if (e.KeyCode == Keys.Enter)
                    {
                        dvgFrmFinancialYear();
                        mtxtFinyrDesc.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }
        }

        private void mbtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(mtxtFinyrDesc.Text))
                {
                    mbtnSave.Enabled = false;
                }
                RefreshForm();
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, 150);
            }

        }
        private void ckbActive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
               if(!string.IsNullOrWhiteSpace(mtxtFinyrDesc.Text))
                {
                    mbtnSave.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, 150);
            }
        }

        private void mtxtFinyrDesc_Leave(object sender, EventArgs e)
        {
            try
            {
                CultureInfo cI = Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cI.TextInfo;
                mtxtFinyrDesc.Text = textInfo.ToTitleCase(mtxtFinyrDesc.Text);
                mtxtFinyrDesc.Select(mtxtFinyrDesc.Text.Length, 1);

            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, 150);
            }
        }
        #endregion


    }
}
