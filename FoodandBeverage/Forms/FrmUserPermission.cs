using BLL;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
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
using System.Diagnostics;
using MetroFramework;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using FoodandBeverage.Forms.UserAccount;
using System.Text.RegularExpressions;
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
        long BranchId = 1030;//16
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
            foreach (Control control in this.Controls)
            {
                control.Enabled = true;
            }
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

        #endregion

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void HeaderCheckBoxread_Clicked(object sender, EventArgs e)
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
        private void HeaderCheckBoxwrite_Clicked(object sender, EventArgs e)
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
            if (txtusertype.Text == "" || txtusertype.Text == null)
            {
                CMessageBox.Show("Please Enter UserType", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public void FillCombo()
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
        private void btnusertype_Click(object sender, EventArgs e)
        {
            if (MandatoryCheckuser())
            {
                if (clsGeneral.NameExist_UserType(txtusertype.Text))
                {
                    if (txtusertype.Text != "")
                    {
                        DmnUserAccountType dmnUser = new DmnUserAccountType();
                        BllUserAccount bllUser = new BllUserAccount();
                        dmnUser.UserAccountType = txtusertype.Text;
                        dmnUser.Note = txtdescr.Text;
                        dmnUser.Status = 1;
                        bool usertype = bllUser.AddUserType("prdInsertUserType", dmnUser);
                        FillCombo();
                        pnlusertype.Hide();
                        txtusertype.Text = "";
                        txtdescr.Text = "";
                        FillComboUserType();
                    }
                }
                else
                    txtusertype.Focus();
            }
        }

        private void dgvforms_CellEndEdit(object sender, DataGridViewCellEventArgs e)
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
        public bool MandatoryCheck()
        {
            if (cmbUsreAccount.SelectedIndex < 0)
            {
                CMessageBox.Show("UserType Need To Be Specifay", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
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
                    dmnForms.CreatedBy = 1;//FrmDashboard.UserType;
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

                        bool status = bllUserAccount.AddUserPermission("prdInsertUpdUserPermission", dmnForms);

                    }
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
        public void clear()
        {
            
            
            //pbCust.Image = Game.Properties.Resources.webcam;
           
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
            FillComboUserType();
            PasswordHash = null;
            PasswordSalt = null;
          
            //mcmblocation.SelectedValue = Convert.ToInt64(FrmDashBoard.BranchId);
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
            btnusertype.Enabled = true;
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void FrmUserPermission_Load(object sender, EventArgs e)
        {
            FillCombo();
            FillComboUserType();
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
        public void FillComboUserType()
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
            cmbUsreAccount.SelectedIndex = 0;

            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FrmUserPermission_Resize(object sender, EventArgs e)
        {
            textb_shape();
        }

        public void FillForms(int id)
        {

            dgvforms.Rows.Clear();
            headerCheckBoxwrite.Checked = false;
            headerCheckBoxread.Checked = false;
            int x = dgvforms.Width;
            headerCheckBoxwrite.Location = new Point(x - 420, 10);
            headerCheckBoxwrite.BackColor = Color.White;
            headerCheckBoxwrite.Size = new Size(18, 18);
            headerCheckBoxwrite.Click += new EventHandler(HeaderCheckBoxwrite_Clicked);
            dgvforms.Controls.Add(headerCheckBoxwrite);
            headerCheckBoxread.Location = new Point(x - 140, 10);
            headerCheckBoxread.BackColor = Color.White;
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

        private void cmbUsreAccount_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbUsreAccount.SelectedIndex <= 0)
            {
                dgvforms.Rows.Clear();
            }
            else
            {
                FillForms(Convert.ToInt32(cmbUsreAccount.SelectedValue));
            }

        }
    }
}
