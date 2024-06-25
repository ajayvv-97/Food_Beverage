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
using System.Threading;
using System.Globalization;
using FoodandBeverage.Interfaces;
using CustomMessageBox;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
namespace FoodandBeverage.Forms
{
    public partial class FrmDesignation : Form
    {
        #region Declaration
        ClsGeneral objGeneral = new ClsGeneral();
        long BranchId = FrmLogin.BranchId;
        long UserId = 1;
        long DesignationId = 0;
        #endregion

        #region grid Declaration 
        public static int DesignationIdno = 0;
        public static int Departmentno = 1;
        public static int Designationno = 2;
        public static int WorkingHoursno = 3;
        #endregion

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
            );

        #region Functions
        public FrmDesignation()
        {
            InitializeComponent();
        }
        public void FillDepartmentComboBox()
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "DepartmentId,DepartmentName";
                objSearch.Table = " tblDepartment ";
                //objSearch.WhereNdGroupBy = "Where Status = 1 AND BranchId = " + BranchId;
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                mcmbDepartment.DataSource = gridSource;
                mcmbDepartment.ValueMember = "Id";
                mcmbDepartment.DisplayMember = "Value";
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Designation Page  FillDepartmentComboBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
            }
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
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "ds.DesignationId as Id,dp.DepartmentName as Field1,ds.DesignationName as Field2,ds.WorkingHours as Field3";
                dmnSearch.Table = " tblDesignation as ds" +
                                    " LEFT JOIN tblDepartment dp ON ds.DepartmentId = dp.DepartmentId";
                dmnSearch.WhereNdGroupBy = " Where ds.Status = 1 AND ds.BranchId = " + FrmLogin.BranchId;
                dmnSearch.OrderBy = "Order By DesignationName ASC ";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                dgvDesignation.Rows.Clear();
                if (gridSource.Count > 0)
                {
                    dgvDesignation.Rows.Add(gridSource.Count);
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        dgvDesignation.Rows[i].Cells[DesignationIdno].Value = gridSource[i].Id;
                        dgvDesignation.Rows[i].Cells[Departmentno].Value = gridSource[i].Field1;
                        dgvDesignation.Rows[i].Cells[Designationno].Value = gridSource[i].Field2;
                        dgvDesignation.Rows[i].Cells[WorkingHoursno].Value = gridSource[i].Field3;
                    }
                }
            }
            catch (Exception ex)
            {
                objGeneral.LogException("Designation Page  fillGridView()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void RefreshForm()
        {

            try
            {
                dtpWorkingHrs.Text = "08:00";
                DesignationId = 0;
                mtxtName.Text = "";
                mcmbDepartment.SelectedIndex = 0;
                fillGridView();
                mcmbDepartment.Focus();
                if (!string.IsNullOrWhiteSpace(mtxtName.Text))
                {
                    mbtnSave.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                objGeneral.LogException("Designation Page  RefreshForm()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
;

        }
        public Boolean NameExist()
        {

            try
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
                        objSearch.WhereNdGroupBy = " Where DepartmentId= '" + Convert.ToInt32(mcmbDepartment.SelectedValue) + "' AND DesignationName = '" + Convert.ToString(mtxtName.Text) + "' AND Status=1 AND BranchId = " + FrmLogin.BranchId;
                    }
                    else
                    {
                        objSearch.WhereNdGroupBy = " Where DepartmentId= '" + Convert.ToInt32(mcmbDepartment.SelectedValue) + "' AND DesignationId!= " + DesignationId + " AND Status=1 AND DesignationName = '" + Convert.ToString(mtxtName.Text) + "' AND BranchId = " + FrmLogin.BranchId;
                    }
                    //objSearch.OrderBy = "Order By DesignationName ASC";
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSource[0].Count >= 1)
                    {
                        MessageBox.Show("Designation Name " + Convert.ToString(mtxtName.Text) + " Already Exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            catch (Exception ex)
            {       
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Designation Page  NameExist()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
                return false;
 
            }


        }
        public void dgvFrmDesignation()
        {

            try
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

                    mcmbDepartment.Focus();
                    mbtnSave.Enabled = true;

                }
            }
            catch (Exception ex)
            {              
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Designation Page  dgvFrmDesignation()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));

            }

        }
        #endregion


        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { mtxtName })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Designation Page  textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
            }
        }

        private void ApplyRoundedTextBox(TextBox textBox, int radius)
        {

            try
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
            catch (Exception ex)
            {
                objGeneral.LogException("Designation Page  ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (NameExist())
                    {
                        bool savestatus;
                        DmnDesignation dmnDesignation = new DmnDesignation();
                        BllDesignation bllDesignation = new BllDesignation();
                        dmnDesignation.DepartmentId = Convert.ToInt32(mcmbDepartment.SelectedValue);
                        dmnDesignation.WorkingHours = dtpWorkingHrs.Text;
                        dmnDesignation.DesignationName = Convert.ToString(mtxtName.Text);
                        dmnDesignation.BranchId = FrmLogin.BranchId;
                        dmnDesignation.Status = 1;
                        if (DesignationId == 0)
                        {
                            dmnDesignation.CreatedBy = FrmLogin.UserId;
                            var lstData = bllDesignation.AddData("prdInsertDesignation", dmnDesignation);
                            savestatus = lstData;
                            if (savestatus)
                                CMessageBox.Show("\n" + mtxtName.Text + " Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            else
                                CMessageBox.Show("\n" + mtxtName.Text + " Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            dmnDesignation.ModifiedBy = FrmLogin.UserId;
                            dmnDesignation.ModifiedDateTime = DateTime.Now;
                            dmnDesignation.DesignationId = DesignationId;
                            var lstData = bllDesignation.UpdateData("prdUpdateDesignation", dmnDesignation);
                            savestatus = lstData;
                            if (savestatus)
                                CMessageBox.Show("\n" + mtxtName.Text + " Updated successfully ", "Updated  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            else
                                CMessageBox.Show("\n" + mtxtName.Text + " Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        dtpWorkingHrs.Text = "08:00";
                        FrmEmployeeShort frmEmployeeShort = new FrmEmployeeShort();
                        frmEmployeeShort.FillCombo();
                        RefreshForm();

                    }
                    if (string.IsNullOrWhiteSpace(mtxtName.Text))
                    {
                        mbtnSave.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                objGeneral.LogException("Designation Page  MbtnSave_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MbtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                mtxtName.Text = "";
                FillDepartmentComboBox();
                if (string.IsNullOrWhiteSpace(mtxtName.Text))
                {
                    mbtnSave.Enabled = false;

                }
                RefreshForm();
            }
            catch (Exception ex)
            {         
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Designation Page  MbtnRefresh_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));

            }

        }
        private void FrmDesignation_Load(object sender, EventArgs e)
        {
            try
            {
                dgvDesignation.BorderStyle = BorderStyle.None;
                dgvDesignation.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgvDesignation.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvDesignation.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgvDesignation.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgvDesignation.BackgroundColor = Color.White;

                dgvDesignation.EnableHeadersVisualStyles = false;
                dgvDesignation.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvDesignation.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgvDesignation.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                fillGridView();
                FillDepartmentComboBox();
                dtpWorkingHrs.Text = "08:00";

                FrmDashboard frmDash = new FrmDashboard();
                if (frmDash.ChkWritePermission(Convert.ToInt32(formsenum.Designation)))
                {
                    mbtnSave.Enabled = true;

                }
                else
                {
                    mbtnSave.Enabled = false;

                }
                if (FrmLogin.UserId == 1 && Convert.ToString(FrmLogin.EmployeName) == "test")
                {
                    mbtnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Designation Page  FrmDesignation_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
            }



        }

        private void DgvDesignation_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                dgvFrmDesignation();
            }
            catch (Exception ex)
            {
                objGeneral.LogException("Designation Page  DgvDesignation_CellDoubleClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
               CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        DialogResult result = MessageBox.Show("Confirm to delete" + dgvDesignation.CurrentRow.Cells[1].Value.ToString() + "?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                                CMessageBox.Show("Successfully deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                            else
                                CMessageBox.Show("Delete failed " + mtxtName.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                objGeneral.LogException("Designation Page  DgvDesignation_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmDesignation_KeyUp(object sender, KeyEventArgs e)
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
                if (e.KeyCode == Keys.Escape)
                    this.Close();

            }
            catch (Exception ex)
            {
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                objGeneral.LogException("Designation Page  FrmDesignation_KeyUp()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
            }
        }


        private void dtpWorkingHrs_ValueChanged(object sender, EventArgs e)
        {

            try
            {


                if (dtpWorkingHrs.Text != "08:00")
                {
                    string min = "08:00";
                    string max = "12:00";
                    if (Convert.ToDateTime(dtpWorkingHrs.Text) < Convert.ToDateTime(min) || Convert.ToDateTime(dtpWorkingHrs.Text) > Convert.ToDateTime(max))
                    {
                        CMessageBox.Show("invalid time limit ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        dtpWorkingHrs.Text = "08:00";
                        //mbtnSave.Enabled = false;
                    }

                }

            }
            catch (Exception ex)
            {
                objGeneral.LogException("Designation Page  dtpWorkingHrs_ValueChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                objGeneral.LogException("Designation Page  mtxtName_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        #endregion

        private void mbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmDesignation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Escape)
                    this.Close();


                //    btnSave_Click(sender, e);
            }
            catch (Exception ex)
            {
                objGeneral.LogException("Designation Page  FrmDesignation_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
            }
        }

        //save
        private void mbtnSave_MouseHover(object sender, EventArgs e)
        {
            this.mbtnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.SaveButton));
        }

        private void mbtnSave_MouseDown(object sender, MouseEventArgs e)
        {
            this.mbtnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.SaveButton));
        }

        private void mbtnSave_MouseLeave(object sender, EventArgs e)
        {
            this.mbtnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.SaveButton));
        }

        private void mbtnSave_MouseUp(object sender, MouseEventArgs e)
        {
            this.mbtnSave.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.SaveButton));
        }

        //refresh
        private void mbtnRefresh_MouseHover(object sender, EventArgs e)
        {
            this.mbtnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_Button1));
        }

        private void mbtnRefresh_MouseDown(object sender, MouseEventArgs e)
        {
            this.mbtnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_Button1));
        }

        private void mbtnRefresh_MouseLeave(object sender, EventArgs e)
        {
            this.mbtnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_Button1));
        }

        private void mbtnRefresh_MouseUp(object sender, MouseEventArgs e)
        {
            this.mbtnRefresh.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.refresh_Button1));
        }

        //close

        private void mbtnClose_MouseHover(object sender, EventArgs e)
        {
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Closebutton));
        }

        private void mbtnClose_MouseDown(object sender, MouseEventArgs e)
        {
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Closebutton));
        }

        private void mbtnClose_MouseLeave(object sender, EventArgs e)
        {
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Closebutton));
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mtxtName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                objGeneral.NextConrolOnEnterPress(sender, e);
                if (e.KeyCode == Keys.F12)
                {
                    FrmDepartment frmDepartment = new FrmDepartment();
                    frmDepartment.ShowDialog();
                    FillDepartmentComboBox();
                }
            }
            catch (Exception ex)
            {
                objGeneral.LogException("Designation Page  mtxtName_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mtxtName_Leave_1(object sender, EventArgs e)
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
                objGeneral.LogException("Designation Page  mtxtName_Leave_1()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mtxtName_Resize(object sender, EventArgs e)
        {
            textb_shape();
        }

        private void FrmDesignation_Resize(object sender, EventArgs e)
        {
            try
            {

            
            objGeneral.GrideAutoSize(dgvDesignation);
            textb_shape();
        }
            catch (Exception ex)
            {
                objGeneral.LogException("Designation Page  FrmDesignation_Resize()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void mcmbDepartment_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(mtxtName.Text))
                {
                    mbtnSave.Enabled = true;
                }

            }
            catch (Exception ex)
            {
                objGeneral.LogException("Designation Page  mcmbDepartment_SelectionChangeCommitted()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Designation));
                CMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
