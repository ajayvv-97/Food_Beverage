using BLL;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using MetroFramework;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using FoodandBeverage.Interfaces;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using CustomMessageBox;

namespace FoodandBeverage.Forms
{
    public partial class FrmDepartment : Form
    {
        #region Declaration
        ClsGeneral objGeneral = new ClsGeneral();
        long BranchId = FrmDashboard.BranchId;
        long UserId = FrmDashboard.UserId;
        long DepartmentId = 0;
        ClsGeneral clsgen = new ClsGeneral();
        #endregion

        #region grid Declaration 
        public static int dIdno = 0;
        public static int dDepartmentno = 1;
        public static int dDepartmentCodeno = 2;
        public static int dEmployeeSrNo = 3;
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Department Page  textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Department Page  ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
            }
        }

        public void fillGridView()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "DepartmentId as Id,DepartmentName as Field1,DepartmentCode as Field2,EmployeeSrlNoStart as Field3";
                dmnSearch.Table = "tblDepartment";
                dmnSearch.WhereNdGroupBy = "Where Status = 1 and BranchId=" + BranchId;
                dmnSearch.OrderBy = "Order By DepartmentName ASC";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                dgvDepartment.Rows.Clear();
                if (gridSource.Count > 0)
                {
                    dgvDepartment.Rows.Add(gridSource.Count);
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        dgvDepartment.Rows[i].Cells[dIdno].Value = gridSource[i].Id;
                        dgvDepartment.Rows[i].Cells[dDepartmentno].Value = gridSource[i].Field1;
                        dgvDepartment.Rows[i].Cells[dDepartmentCodeno].Value = gridSource[i].Field2;
                        dgvDepartment.Rows[i].Cells[dEmployeeSrNo].Value = gridSource[i].Field3;
                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Department Page  fillGridView()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
            }

        }
        public void RefreshForm()
        {
            try
            {
                DepartmentId = 0;
                mtxtEmpSrlNo.Text = "";
                mtxtName.Text = "";
                mtxtCode.Text = "";
                //MetroTextBox[] listTbx = { mtxtName, mtxtCode, mtxtEmpSrlNo };
                //objGeneral.ResetSelectedMetroTextBoxes(listTbx);
                fillGridView();
                mtxtName.Focus();
                mtxtEmpSrlNo.ReadOnly = false;
                if (!string.IsNullOrWhiteSpace(mtxtName.Text))
                {
                    mbtnSave.Enabled = false;
                }
                mtxtName.Focus();
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Department Page  RefreshForm()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
            }
        }

        public Boolean NameExist()
        {
            try
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Department Page  NameExist()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
                return true;
            }
        }
        public Boolean CodeExist()
        {
            try
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
                        objSearch.WhereNdGroupBy = " Where DepartmentCode LIKE '" + Convert.ToString(mtxtCode.Text) + "%' COLLATE SQL_Latin1_General_CP1_CS_AS AND Status=1 AND BranchId = " + BranchId;
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Department Page  CodeExist()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
                return false;
            }
        }

        public void GenerateSrNo()
        {
            try
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
                if (gridSourceGn.Count > 0)
                {
                    decimal difference = Convert.ToDecimal(gridSourceGn[0].GTypeSubValue1);
                    decimal SrlNoStart = Convert.ToDecimal(gridSourceSrlno[0].EmployeeSrlNoStart + difference);
                    mtxtEmpSrlNo.Text = Convert.ToString(SrlNoStart.ToString("0"));
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Department Page  GenerateSrNo()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));

            }
        }
        public void dgvFrmDepartment()
        {
            try
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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Department Page  dgvFrmDepartment()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
            }
        }
        #endregion

        #region Events
        private void FrmDepartment_Load(object sender, EventArgs e)
        {
            try
            {
                if (FrmDashboard.AutoLogoutWhenIdle)
                {
                    AppSession.RegisterForm(this, (int)LogoutFormList.employee);
                }

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
                if (FrmLogin.UserId == 1 && Convert.ToString(FrmLogin.EmployeName) == "test")
                {
                    mbtnSave.Enabled = true;
                }
                mtxtName.Focus();
                RefreshForm();
                mtxtName.Focus();

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Department Page  FrmDepartment_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));

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
                                CMessageBox.Show(this, "Successfully deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            }
                            else
                                CMessageBox.Show(this, "Delete failed ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                clsgen.LogException("Department Page  DgvDepartment_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
               //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        dmnDepartment.BranchId = Convert.ToInt32(FrmLogin.BranchId);
                        dmnDepartment.Status = 1;

                        if (DepartmentId == 0)
                        {
                            int empNo;
                            if (!int.TryParse(mtxtEmpSrlNo.Text, out empNo))
                            {
                                CMessageBox.Show(this, " Fill Employee Code", "Warning  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                            }
                            else
                            {
                                dmnDepartment.EmployeeSrlNoStart = Convert.ToInt32(mtxtEmpSrlNo.Text);
                                dmnDepartment.CreatedBy = Convert.ToInt32(FrmLogin.UserId);
                                var lstData = bllDepartment.AddData("prdInsertDepartment", dmnDepartment);
                                savestatus = lstData;
                                if (savestatus)
                                    CMessageBox.Show(this, mtxtName.Text + " Successfully Saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                else
                                    CMessageBox.Show(this, mtxtName.Text + " Save Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            
                        }
                        else
                        {
                            dmnDepartment.ModifiedBy = Convert.ToInt32(FrmLogin.UserId);
                            dmnDepartment.ModifiedDateTime = DateTime.Now;
                            dmnDepartment.DepartmentId = DepartmentId;
                            var lstData = bllDepartment.UpdateData("prdUpdateDepartment", dmnDepartment);
                            savestatus = lstData;
                            if (savestatus)
                                CMessageBox.Show(this, mtxtName.Text + " Updated Successfully ", "Updated  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            else
                                CMessageBox.Show(this, mtxtName.Text + " Save Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        fillGridView();
                    }
                    RefreshForm();
                    mtxtName.Focus();
                    FrmEmployeeShort frmEmployeeShort = new FrmEmployeeShort();
                    frmEmployeeShort.FillCombo();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Department Page  MbtnSave_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void MbtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(mtxtName.Text))
                {
                    mbtnSave.Enabled = false;
                }
                RefreshForm();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Department Page  MbtnRefresh_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                clsgen.LogException("Department Page  DgvDepartment_CellDoubleClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                clsgen.LogException("Department Page  MtxtEmpSrlNo_KeyPress()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void FrmDepartment_KeyUp(object sender, KeyEventArgs e)
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
                //objGeneral.NextConrolOnEnterPress(sender, e);
                if (e.KeyCode == Keys.Escape)
                    this.Close();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Department Page  FrmDepartment_KeyUp()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
               //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Department Page  mtxtName_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Department Page  mtxtCode_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Department Page  mtxtEmpSrlNo_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Department Page  mtxtName_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
            }
        }
        #endregion

        private void FrmDepartment_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Escape)
                    this.Close();
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Department Page  FrmDepartment_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
            }
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Department Page  mtxtEmpSrlNo_KeyDown_1()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Department Page  mtxtEmpSrlNo_KeyPress_1()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Department Page  mtxtName_KeyDown_1()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
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

                if (mtxtName.Text != "" && DepartmentId == 0)
                {
                    BllSearch<DmnDepartment> bllSearch = new BllSearch<DmnDepartment>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "DepartmentId,DepartmentName";
                    objSearch.Table = "tblDepartment";
                    objSearch.WhereNdGroupBy = "Where status=1 and DepartmentName = '" + mtxtName.Text + "' and BranchId = " + BranchId;
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count > 0)
                    {
                        MessageBox.Show("Department Name Already Exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        mtxtName.Text = "";
                    }
                }

            }
            catch (Exception ex)
            {
                //MetroMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                clsgen.LogException("Department Page  mtxtName_Leave_1()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Department));
            }
        }

        private void FrmDepartment_Resize(object sender, EventArgs e)
        {
            #region setting Corner Round 
            UIHelper.RoundCorners(dgvDepartment, 25, null);
            UIHelper.RoundCorners(tableLayoutPanel1, 25, null);
            UIHelper.RoundCorners(tlp_header, 10, null);
            #endregion

            textb_shape();
            objGeneral.GrideAutoSize(dgvDepartment);
        }

        private void FrmDepartment_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FrmDashboard.AutoLogoutWhenIdle)
            {
                AppSession.UnregisterForm(this);
            }
        }
    }
}
#endregion