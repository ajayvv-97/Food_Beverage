using BLL;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using CustomMessageBox;

namespace FoodandBeverage.Forms
{
    public partial class FrmUserPermission : Form
    {
        ClsDocument clsDocument = new ClsDocument();
        FilterInfoCollection FilterInfoCollection;
        VideoCaptureDevice VideoCaptureDevice;
        long BranchId = FrmDashboard.BranchId;//16
        long UserId = Forms.FrmLogin.UserId;//1
        public static int Allaccess = 0;
        public static int ReportLevelID = 0;
        long DepartmentId = 0;
        public static long EmployeeId = 0, AccountId = 0;
        string EmployeeCode = "";
        public static string empImgExt = "";
        long UserAccountId = 0;
        public string Compressedimg = Application.StartupPath + "\\CompressedImg";
        Bitmap bitmap;
        //Allaccess = Convert.ToInt32(frmLogin.Allaccess);
        byte[] PasswordSalt, PasswordHash;
        CheckBox headerCheckBoxwrite = new CheckBox();
        CheckBox headerCheckBoxread = new CheckBox();
        public static int iid = 0;
        public static int iformid = 1;
        public static int iforms = 2;
        public static int iwrite = 3;
        public static int iread = 4;
        public static int istatus = 5;
        int empid = 0;
        ClsGeneral clsGeneral = new ClsGeneral();
        public FrmUserPermission()
        {
            InitializeComponent();
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
        private void lblsubcatclose_Click(object sender, EventArgs e)
        {
            tlp_main.Enabled = true;
            pnlusertype.Hide();
        }

        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtusertype, txtdescr, txtUserTypeNote })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
            }

        }

        #endregion

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void HeaderCheckBoxread_Clicked(object sender, EventArgs e)
        {
            try
            {
                dgvforms.EndEdit();
                foreach (DataGridViewRow row in dgvforms.Rows)
                {
                    if (headerCheckBoxread.Checked == true)
                    {
                        //dgvforms.Rows[row.Index].Cells[iwrite].Value = true;
                        dgvforms.Rows[row.Index].Cells[iread].Value = true;
                    }
                    else
                    {
                        //dgvforms.Rows[row.Index].Cells[iwrite].Value = false;
                        dgvforms.Rows[row.Index].Cells[iread].Value = false;
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  HeaderCheckBoxread_Clicked()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
            }
        }
        private void HeaderCheckBoxwrite_Clicked(object sender, EventArgs e)
        {
            try
            {
                dgvforms.EndEdit();
                foreach (DataGridViewRow row in dgvforms.Rows)
                {
                    if (headerCheckBoxwrite.Checked == true)
                    {
                        dgvforms.Rows[row.Index].Cells[iwrite].Value = true;
                        dgvforms.Rows[row.Index].Cells[iread].Value = true;
                    }
                    else
                    {
                        dgvforms.Rows[row.Index].Cells[iwrite].Value = false;
                        dgvforms.Rows[row.Index].Cells[iread].Value = false;
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  HeaderCheckBoxwrite_Clicked()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
            }


        }

        private void cmbUsreAccount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void txtUserTypeNote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        private void cmbapptype_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillForms(Convert.ToInt32(cmbUsreAccount.SelectedValue));
        }

        public bool MandatoryCheckuser()
        {
            try
            {
                if (string.IsNullOrEmpty(txtutypeid.Text))
                {
                    txtutypeid.Text = "0";
                }
                if (txtusertype.Text == "" || txtusertype.Text == null)
                {
                    CMessageBox.Show("Please Enter UserType", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  MandatoryCheckuser()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
                return false;
            }
        }

        public void FillCombo()
        {
            try
            {
                BllSearch<DmnComboBox> bllacctype = new BllSearch<DmnComboBox>();
                DmnSearch dmnacctype = new DmnSearch();
                dmnacctype.Select = "UserAccountTypeId,UserAccountType";
                dmnacctype.Table = " tblUserAccountType ";
                dmnacctype.WhereNdGroupBy = "Where Status = 1 ";//AND DepartmentId = " + mcmbDepartmentId.SelectedValue;
                dmnacctype.OrderBy = "order by Value";
                var acctype = bllacctype.GetSearchData("prdComboFill", dmnacctype);
                cmbUsreAccount.DataSource = acctype;
                cmbUsreAccount.DisplayMember = "Value";
                cmbUsreAccount.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  FillCombo()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
            }

        }
        private void btnusertype_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheckuser())
                {
                    if(Convert.ToInt32(txtutypeid.Text)>0)
                    {
                        if (txtusertype.Text != "")
                        {
                            DmnUserAccountType dmnUser = new DmnUserAccountType();
                            BllUserAccount bllUser = new BllUserAccount();
                            dmnUser.UserAccountTypeId = Convert.ToInt32(txtutypeid.Text);
                            dmnUser.UserAccountType = txtusertype.Text;
                            dmnUser.Note = txtdescr.Text;
                            dmnUser.Status = 1;
                            var usertype = bllUser.AddUserType("prdInsertUserType", dmnUser);
                            if (usertype.UserAccountTypeId > 0)
                            {
                                if (Convert.ToInt32(txtutypeid.Text) > 0)
                                    CMessageBox.Show("Update Successful", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                    CMessageBox.Show("Save Successful", "Save Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                FillCombo();
                                pnlusertype.Hide();
                                txtusertype.Text = "";
                                txtdescr.Text = "";
                                txtutypeid.Text = "";
                                tlp_main.Enabled = true;
                            }
                        }
                    }
                    else
                    {

                        if (clsGeneral.NameExist_UserType(txtusertype.Text))
                        {
                            if (txtusertype.Text != "")
                            {
                                DmnUserAccountType dmnUser = new DmnUserAccountType();
                                BllUserAccount bllUser = new BllUserAccount();
                                dmnUser.UserAccountTypeId = Convert.ToInt32(txtutypeid.Text);
                                dmnUser.UserAccountType = txtusertype.Text;
                                dmnUser.Note = txtdescr.Text;
                                dmnUser.Status = 1;
                                var usertype = bllUser.AddUserType("prdInsertUserType", dmnUser);
                                if (usertype.UserAccountTypeId > 0)
                                {
                                    if (Convert.ToInt32(txtutypeid.Text) > 0)
                                        CMessageBox.Show("Update Successful", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    else
                                        CMessageBox.Show("Save Successful", "Save Status", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    FillCombo();
                                    pnlusertype.Hide();
                                    txtusertype.Text = "";
                                    txtdescr.Text = "";
                                    txtutypeid.Text = "";
                                    tlp_main.Enabled = true;
                                }
                            }
                        }
                        else
                            txtusertype.Focus();
                    }

                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  btnusertype_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
            }

        }

        private void dgvforms_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == iwrite && Convert.ToInt32(dgvforms.Rows[e.RowIndex].Cells[iwrite].Value) == 1)
                {
                    dgvforms.Rows[e.RowIndex].Cells[iread].Value = 1;
                }
                if (e.ColumnIndex == iread && Convert.ToInt32(dgvforms.Rows[e.RowIndex].Cells[iwrite].Value) == 1)
                {
                    dgvforms.Rows[e.RowIndex].Cells[iread].Value = 1;
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  dgvforms_CellEndEdit()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
            }
        }
        public bool MandatoryCheck()
        {
            try
            {
                if (cmbUsreAccount.SelectedIndex < 0)
                {
                    CMessageBox.Show("UserType Need To Be Specifay", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  HeaderCheckBoxread_Clicked()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
                return false;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DmnForms dmnForms = new DmnForms();
                BllUserAccount bllUserAccount = new BllUserAccount();
                if (cmbUsreAccount.SelectedIndex >= 0)
                {
                    bool flag = false;
                    foreach (DataGridViewRow row in dgvforms.Rows)
                    {
                        if (Convert.ToInt32(dgvforms.Rows[row.Index].Cells[iread].Value) == 1 || Convert.ToInt32(dgvforms.Rows[row.Index].Cells[iread].Value) == 1)
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (flag == true && MandatoryCheck())
                    {
                        bool status = false;
                        dmnForms.CreatedBy = FrmDashboard.UserId;
                        dmnForms.UserType = Convert.ToInt32(cmbUsreAccount.SelectedValue);
                        //if (cmbapptype.Text == "BackOffice")
                        //    dmnForms.FormType = 1;
                        //else
                        //    dmnForms.FormType = 2;
                        for (int i = 0; i < dgvforms.Rows.Count; i++)
                        {

                            dmnForms.id = Convert.ToInt32(dgvforms.Rows[i].Cells[iid].Value);
                            dmnForms.Form = Convert.ToInt32(dgvforms.Rows[i].Cells[iformid].Value);
                            dmnForms.Read = Convert.ToInt32(dgvforms.Rows[i].Cells[iread].Value);
                            dmnForms.Write = Convert.ToInt32(dgvforms.Rows[i].Cells[iwrite].Value);
                            dmnForms.Note = txtUserTypeNote.Text.ToString();
                            if (dmnForms.Read == 0 && dmnForms.Write == 0)
                            {
                                dmnForms.Status = 0;
                            }
                            else
                                dmnForms.Status = 1;
                             status = bllUserAccount.AddUserPermission("prdInsertUpdUserPermission", dmnForms);
                        }
                        if (status)
                            CMessageBox.Show("Save Sucessfull", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        CMessageBox.Show(" Need To Assign One Permission Atleast", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    CMessageBox.Show(" Please Select The Account Group", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                clear();
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  btnSave_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
            }

        }
        public void clear()
        {
            try
            {
                dgvforms.Visible = false;
                FillCombo();

                if (!Directory.Exists(Compressedimg))
                {
                    Directory.CreateDirectory(Compressedimg);
                    DirectoryInfo dInfo = new DirectoryInfo(Compressedimg);
                    DirectorySecurity dSecurity = dInfo.GetAccessControl();
                    dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                    dInfo.SetAccessControl(dSecurity);
                }
                else
                {
                    foreach (string file in Directory.GetFiles(Compressedimg))
                    {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        File.Delete(file);
                    }
                }

                cmbUsreAccount.SelectedItem = 0;
                txtUserTypeNote.Text = "";
                dgvforms.Rows.Clear();
                EmployeeId = 0;
                UserAccountId = 0;
                PasswordHash = null;
                PasswordSalt = null;
                txtutypeid.Text = "0";
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  clear()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnaddusertype_Click(object sender, EventArgs e)
        {
            pnlusertype.Location = new Point(this.Location.X + 150, this.Location.Y + 75);
            pnlusertype.Show();
            tlp_main.Enabled = false;
            btnusertype.Enabled = true;
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FrmUserPermission_Load(object sender, EventArgs e)
        {
            try
            {
                if (FrmDashboard.AutoLogoutWhenIdle)
                {
                    AppSession.RegisterForm(this, (int)LogoutFormList.employee);
                }

                dgvforms.BorderStyle = BorderStyle.None;
                dgvforms.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                dgvforms.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvforms.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                dgvforms.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                dgvforms.BackgroundColor = Color.White;

                dgvforms.EnableHeadersVisualStyles = false;
                dgvforms.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvforms.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                dgvforms.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                FillCombo();
                FrmDashboard frmDash = new FrmDashboard();
                if (frmDash.ChkWritePermission(Convert.ToInt32(formsenum.User_Permission)))
                {
                    btnSave.Enabled = true;
                }
                else
                {
                    btnSave.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  FrmUserPermission_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
            }
        }
        //public void FillComboUserType()
        //{
        //    try
        //    {
        //        BllSearch<DmnComboBox> bllacctype = new BllSearch<DmnComboBox>();
        //        DmnSearch dmnacctype = new DmnSearch();
        //        dmnacctype.Select = "UserAccountTypeId,UserAccountType";
        //        dmnacctype.Table = " tblUserAccountType ";
        //        dmnacctype.WhereNdGroupBy = "Where Status = 1 ";//AND DepartmentId = " + mcmbDepartmentId.SelectedValue;
        //        dmnacctype.OrderBy = "order by Value";
        //        var acctype = bllacctype.GetSearchData("prdComboFill", dmnacctype);
        //        cmbUsreAccount.DataSource = acctype;
        //        cmbUsreAccount.DisplayMember = "Value";
        //        cmbUsreAccount.ValueMember = "Id";
        //        cmbUsreAccount.SelectedIndex = 0;
        //    }
        //    catch (Exception ex)
        //    {
        //        CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
        //        clsGeneral.LogException("User Permission Page  FillComboUserType()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
        //    }
        //}

        private void FrmUserPermission_Resize(object sender, EventArgs e)
        {
            #region setting Corner Round 
            UIHelper.RoundCorners(tlp_two_box, 25, null);
            UIHelper.RoundCorners(dgvforms, 10, null);
            #endregion

            textb_shape();
        }

        private void txtusertype_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }

        private void btn_usertype_close_Click(object sender, EventArgs e)
        {
            tlp_main.Enabled = true;
            pnlusertype.Hide();
        }

        private void txtusertype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F8)
            {
                Usertypepopup();
            }
        }
        public void Usertypepopup()
        {
            try
            {
                string returnValue = string.Empty;
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "Id,User Type,Description";
                _objSearch.Select = "UserAccountTypeId as Id,UserAccountType as Field1,Note as Field2";
                _objSearch.Table = "tblUserAccountType ";
                _objSearch.WhereNdGroupBy = "Where Status=1";
                _objSearch.OrderBy = " Order By UserAccountType ASC ";

                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToString(search._retValue);

                    }
                }
                if (returnValue.ToString() != null && returnValue.ToString() != "")
                {
                    int utid = Convert.ToInt32(returnValue.ToString());
                    BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "UserAccountTypeId as Id,UserAccountType as Field1,Note as Field2";
                    objSearch.Table = "tblUserAccountType ";
                    objSearch.WhereNdGroupBy = "Where UserAccountTypeId = " + utid;
                    var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (gridSource.Count > 0)
                    {
                        txtusertype.Text = gridSource[0].Field1;
                        txtdescr.Text = Convert.ToString(gridSource[0].Field2);
                        txtutypeid.Text = Convert.ToString(gridSource[0].Id);

                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  Usertypepopup()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
            }
        }

        private void txtusertype_Enter(object sender, EventArgs e)
        {
            lblusertypeinfo.Visible = true;
        }

        private void txtusertype_Leave(object sender, EventArgs e)
        {
            lblusertypeinfo.Visible = false;
        }

        private void FrmUserPermission_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FrmDashboard.AutoLogoutWhenIdle)
            {
                AppSession.UnregisterForm(this);
            }
        }

        public void FillForms(int id)
        {
            try
            {
                dgvforms.Rows.Clear();
                headerCheckBoxwrite.Checked = false;
                headerCheckBoxread.Checked = false;
                int x = dgvforms.Width;
                headerCheckBoxwrite.Location = new Point(x - 480, 10);
                headerCheckBoxwrite.BackColor = Color.FromArgb(20, 25, 72);
                headerCheckBoxwrite.Size = new Size(18, 18);
                headerCheckBoxwrite.Click += new EventHandler(HeaderCheckBoxwrite_Clicked);
                dgvforms.Controls.Add(headerCheckBoxwrite);
                headerCheckBoxread.Location = new Point(x - 120, 10);
                headerCheckBoxread.BackColor = Color.FromArgb(20, 25, 72);
                headerCheckBoxread.Size = new Size(18, 18);
                headerCheckBoxread.Click += new EventHandler(HeaderCheckBoxread_Clicked);
                dgvforms.Controls.Add(headerCheckBoxread);
                //dgvforms.Rows[].
                BllSearch<DmnForms> bllacctype = new BllSearch<DmnForms>();
                DmnSearch dmnforms = new DmnSearch();
                dmnforms.Select = "f.Id,g.Caption,g.GTypeSubId,[Read],Write,f.UserType,f.Status,Note";

                dmnforms.Table = " tblGeneralOptions g" +
                " left join tblForms f on f.Form=g.GTypeSubId and f.Status=1 and f.UserType=" + id;
                dmnforms.WhereNdGroupBy = " where g.GTypeId=55 and g.Status=0  ";

                dmnforms.OrderBy = "order by GTypeSubId";
                var gridesource = bllacctype.GetSearchData("prdSearch", dmnforms);
                if (gridesource.Count > 0)
                {
                    dgvforms.Rows.Add(gridesource.Count());
                    txtUserTypeNote.Text = gridesource[0].Note;
                    for (int i = 0; i < gridesource.Count; i++)
                    {
                        dgvforms.Rows[i].Cells[iid].Value = gridesource[i].id;
                        dgvforms.Rows[i].Cells[iformid].Value = gridesource[i].GTypeSubId;
                        dgvforms.Rows[i].Cells[iforms].Value = gridesource[i].Caption;
                        dgvforms.Rows[i].Cells[iread].Value = gridesource[i].Read;
                        dgvforms.Rows[i].Cells[iwrite].Value = gridesource[i].Write;
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  FillForms()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
            }
        }

        private void cmbUsreAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbUsreAccount.SelectedIndex <= 0)
                {
                    dgvforms.Rows.Clear();
                    dgvforms.Visible = false;

                }
                else
                {
                    dgvforms.Visible = true;
                    dgvforms.Visible = true;
                    FillForms(Convert.ToInt32(cmbUsreAccount.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsGeneral.LogException("User Permission Page  FillForms()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.User_Permission));
            }
        }
    }
}
