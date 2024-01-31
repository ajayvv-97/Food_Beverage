using BLL;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
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
using FoodandBeverage.Interfaces;
using System.Threading;
using System.Drawing.Drawing2D;

using System.Runtime.InteropServices;

using CustomMessageBox;

namespace FoodandBeverage.Forms
{
    public partial  class FrmDepartment : Form
    {
        #region Declaration
        ClsGeneral objGeneral = new ClsGeneral();
        long BranchId = 1030;
        long UserId = 1;
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

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
       int nLeftRect,
       int nTopRect,
       int nRightRect,
       int nBottomRect,
       int nWidthEllipse,
       int nHeightEllipse
   );
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { mtxtName, mtxtEmpSrlNo, mtxtCode })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }
            catch (Exception)
            {

            }
        }

        private void ApplyRoundedTextBox(TextBox textBox, int radius)
        {
            // Create a new GraphicsPath object
            GraphicsPath path = new GraphicsPath();
            // Define the shape of the path using a rounded rectangle
            Rectangle rect = new Rectangle(0, 0, textBox.Width, textBox.Height);
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            // Set the Region property of the textbox to the new path
            textBox.Region = new Region(path);
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
            dmnSearch.WhereNdGroupBy = "Where Status = 1 and BranchId=" +1030;
            dmnSearch.OrderBy = "Order By DepartmentName ASC";
            var gridSource = frmGridSearch.GetSearchGrid(dmnSearch);
            foreach (DataGridViewColumn col in gridSource.Columns)
            {
                dgvDepartment.Columns.Add(col.Clone() as DataGridViewColumn);
            }            
            dgvDepartment.DataSource = gridSource.DataSource;
            //dgvDepartment.Refresh();
           
        }
        public void RefreshForm()
        {
            DepartmentId = 0;


            //MetroTextBox[] listTbx = { mtxtName, mtxtCode, mtxtEmpSrlNo };
            //objGeneral.ResetSelectedMetroTextBoxes(listTbx);
            fillGridView();
            mtxtName.Focus();
            mtxtEmpSrlNo.ReadOnly = false;
            if (string.IsNullOrWhiteSpace(mtxtName.Text))
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
                    objSearch.WhereNdGroupBy = " Where DepartmentName = '" + Convert.ToString(mtxtName.Text) + "' AND Status=1 AND BranchId = " + FrmLogin.BranchId;
                }
                else
                {
                    objSearch.WhereNdGroupBy = " Where DepartmentId!= " + DepartmentId + " AND Status=1 AND DepartmentName = '" + Convert.ToString(mtxtName.Text) + "' AND BranchId = " + FrmLogin.BranchId;
                }
                //objSearch.OrderBy = "Order By AccountGroupName ASC";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource[0].Count > 0)
                {
                    MetroMessageBox.Show(this, "Department Name '" + Convert.ToString(mtxtName.Text) + "' Already exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
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
                    objSearch.WhereNdGroupBy = " Where DepartmentCode LIKE '" + Convert.ToString(mtxtCode.Text) + "%' COLLATE SQL_Latin1_General_CP1_CS_AS AND Status=1 AND BranchId = " + 1030;
                }
                else
                {
                    objSearch.WhereNdGroupBy = " Where DepartmentId!= " + DepartmentId + " AND Status=1 AND DepartmentCode LIKE '" + Convert.ToString(mtxtCode.Text) + "%' COLLATE SQL_Latin1_General_CP1_CS_AS AND BranchId = " + FrmLogin.BranchId;
                }
                //objSearch.OrderBy = "Order By AccountGroupName ASC";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource[0].Count > 0)
                {
                    MetroMessageBox.Show(this, "Department code '" + Convert.ToString(mtxtCode.Text) + "' Already exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
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

                mtxtEmpSrlNo.ReadOnly = true;
                mtxtName.Focus();
                mbtnSave.Enabled = true;
            }

        }
        #endregion
        #region Events
        private void FrmDepartment_Load(object sender, EventArgs e)
        {


            dgvDepartment.BorderStyle = BorderStyle.None;
            dgvDepartment.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgvDepartment.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvDepartment.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgvDepartment.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgvDepartment.BackgroundColor = Color.White;

            dgvDepartment.EnableHeadersVisualStyles = false;
            dgvDepartment.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvDepartment.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgvDepartment.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;



            FrmDashboard frmDash = new FrmDashboard();
            if (frmDash.ChkWritePermission(Convert.ToInt32(formsenum.Department)))
            {
                mbtnSave.Enabled = true;

            }
            else
            {
                mbtnSave.Enabled = false;

            }
            try
            {
                if (FrmLogin.UserId == 1 && Convert.ToString(FrmLogin.EmployeName) == "test")
                {
                    mbtnSave.Enabled = true;
                }
                mtxtName.Focus();
                RefreshForm();
                //fillGridView();
               
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        DialogResult result = MetroMessageBox.Show(this, "Confirm  to delete " + dgvDepartment.CurrentRow.Cells[1].Value.ToString() + " ? ", "Delete ? ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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


                                CMessageBox.Show(this, "Successfully deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                            else
                                CMessageBox.Show(this, "Delete failed " , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        dmnDepartment.BranchId = 1030;
                        dmnDepartment.Status = 1;
                        if (DepartmentId == 0)
                        {
                            dmnDepartment.EmployeeSrlNoStart = Convert.ToInt64(mtxtEmpSrlNo.Text);
                            dmnDepartment.CreatedBy = 1;
                            var lstData = bllDepartment.AddData("prdInsertDepartment", dmnDepartment);
                            savestatus = lstData;
                            if (savestatus)
                                CMessageBox.Show(this,mtxtName.Text+ " Successfully Saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            else
                                CMessageBox.Show(this,mtxtName.Text+ " Save Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            dmnDepartment.ModifiedBy = 1;
                            dmnDepartment.ModifiedDateTime = DateTime.Now;
                            dmnDepartment.DepartmentId = DepartmentId;
                            var lstData = bllDepartment.UpdateData("prdUpdateDepartment", dmnDepartment);
                            savestatus = lstData;
                            if (savestatus)
                                CMessageBox.Show(this,mtxtName.Text+ " Updated Successfully ", "Updated  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            else
                                CMessageBox.Show(this,mtxtName.Text+ " Save Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                           
                        }
                        
                        fillGridView();
                       
                    }
                    RefreshForm();
                    mtxtName.Focus();
                }

              
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DgvDepartment_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               
                dgvFrmDepartment();
                
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mtxtCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                objGeneral.NextConrolOnEnterPress(sender, e);
               
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void mtxtName_KeyPress(object sender, KeyPressEventArgs e)
        {
        }


        #endregion

        private void mbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
      
        private void FrmDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Escape)
                    this.Close();
               

                //    btnSave_Click(sender, e);
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }
        private void dgvDepartment_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmDepartment_Leave(object sender, EventArgs e)
        {

        }

      

        private void mbtnClose_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        private void btnmini_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncolse_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void mtxtEmpSrlNo_KeyDown_1(object sender, KeyEventArgs e)
        {
            try
            {
                objGeneral.NextConrolOnEnterPress(sender, e);

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mtxtEmpSrlNo_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                objGeneral.IntegerTextBox(sender, e);
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mtxtName_KeyDown_1(object sender, KeyEventArgs e)
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

        private void mtxtName_Leave_1(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(mtxtName.Text))
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

        private void FrmDepartment_Resize(object sender, EventArgs e)
        {
            textb_shape();
        }
    }
}
#endregion