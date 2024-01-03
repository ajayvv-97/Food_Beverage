using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmVendor : Form
    {
        public FrmVendor()
        {
            InitializeComponent();
        }
        #region Declare Grid
        public static int slno = 0;
        public static int idno = 1;
        public static int inameno = 2;
        public static int iphno = 3;
        public static int iemaino = 4;
        public static int iaddno = 5;
        public static int idesno = 6;
        public static int istatusno = 7;

        #endregion

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            if (VendorMandatoryCheck())
            {
                DmnVendor dmnvendor = new DmnVendor();
                Bllvendor bllvendor = new Bllvendor();
                if (string.IsNullOrWhiteSpace(txtvid.Text))
                    txtvid.Text = "0";
                dmnvendor.VendorId =Convert.ToInt32(txtvid.Text);
                dmnvendor.VendorName = txtVendorName.Text;
                dmnvendor.VendorName = txtVendorName.Text;
                dmnvendor.Address = txtVenderAddress.Text;
                dmnvendor.Email = txtVendorEmail.Text;
                dmnvendor.Telephone = txtVendorTel.Text;
                dmnvendor.Description = txtdesc.Text;
                dmnvendor.BranchId = FrmDashboard.BranchId;
                dmnvendor.CreatedBy = FrmDashboard.UserId; 
                if (chk_active.Checked)
                    dmnvendor.Status = 1;
                else
                    dmnvendor.Status = 0;
                var InsertStatus = bllvendor.AddEditDataVendor("prdInsert_Upd_Vendor", dmnvendor);
                if (InsertStatus == null)
                {
                    CMessageBox.Show("Save faild", "Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //break;
                }
                if (InsertStatus != null)
                {
                    CMessageBox.Show("Successfuly Saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Fillvendor();
                    clearall();
                }
            }
        }
        public bool VendorMandatoryCheck()
        {
            if (string.IsNullOrEmpty(txtVendorName.Text))
            {
                CMessageBox.Show("Vendor Name Is Mandatory & * Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtVendorName.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtVenderAddress.Text))
            {
                CMessageBox.Show("Vendor Address Is Mandatory & * Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtVenderAddress.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(txtVendorTel.Text))
            {
                CMessageBox.Show("Vendor PhNo Is Mandatory & * Mandatory", "Mandatory Filed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtVendorTel.Focus();
                return false;
            }
            return true;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            int returnValue = 0;
            DataTable dtColumnAlign = new DataTable();
            dtColumnAlign.Columns.Add();
            DmnSearch _objSearch = new DmnSearch();
            _objSearch.ColumnHeaders = "ID,VendorName,Telephone,Email,Description"; 
            _objSearch.Select = "VendorId as Id,VendorName as Field1,Telephone as Field2,Email as Field3,Description as Field4";
            _objSearch.Table = "tblVendor";
            _objSearch.WhereNdGroupBy = "where Status = 1";
            _objSearch.OrderBy = " ORDER BY VendorName";
            var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);
            if (gridSource.Count == 0)
            {
                CMessageBox.Show("No Records", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToInt32(search._retValue);
                    }
                }
                if (returnValue > 0)
                {
                    int vid = Convert.ToInt32(returnValue);
                    BllSearch<DmnVendor> bllSearch1 = new BllSearch<DmnVendor>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "* ";
                    objSearch.Table = "tblVendor";
                    objSearch.WhereNdGroupBy = "where status = 1 and VendorId=" + vid;
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count > 0)
                    {
                        txtvid.Text = Convert.ToString(itemSource[0].VendorId);
                        txtVendorName.Text = Convert.ToString(itemSource[0].VendorName);
                        txtVendorTel.Text = Convert.ToString(itemSource[0].Telephone);
                        txtVendorEmail.Text = Convert.ToString(itemSource[0].Email);
                        txtVenderAddress.Text = Convert.ToString(itemSource[0].Address);
                        btn_del_vendor.Visible = true;
                    }
                }
            }
        }

        private void btn_del_vendor_Click(object sender, EventArgs e)
        {
            var result = CMessageBox.Show("Do You want Delete This Unit", "Confirm The Action", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                chk_active.Checked = false;
                btnsave.PerformClick();
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            clearall();
        }
        public void clearall()
        {
            txtvid.Text = "";
            txtVendorName.Text = "";
            txtVendorTel.Text = "";
            txtVendorEmail.Text = "";
            txtVenderAddress.Text = "";
            txtdesc.Text = "";
            chk_active.Visible = false;
        }
        public void Fillvendor()
        {
            try
            {
                BllSearch<DmnVendor> bllSearch = new BllSearch<DmnVendor>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblVendor";
                objSearch.WhereNdGroupBy = "Where status=1 ";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                dgv_vendor.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_vendor.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_vendor.Rows[i].Cells[slno].Value = i + 1;
                        dgv_vendor.Rows[i].Cells[idno].Value = itemSource[i].VendorId;
                        dgv_vendor.Rows[i].Cells[inameno].Value = itemSource[i].VendorName;
                        dgv_vendor.Rows[i].Cells[iphno].Value = itemSource[i].Telephone;
                        dgv_vendor.Rows[i].Cells[iemaino].Value = itemSource[i].Email;
                        dgv_vendor.Rows[i].Cells[iaddno].Value = itemSource[i].Address;
                        dgv_vendor.Rows[i].Cells[idesno].Value = itemSource[i].Description;
                        dgv_vendor.Rows[i].Cells[istatusno].Value = Convert.ToInt32(itemSource[i].Status);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void FrmVendor_Load(object sender, EventArgs e)
        {
            if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
            {
                FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                if (_instanceFrmdash.ChkWritePermission(Convert.ToInt32(formsenum.Vendor)))
                    btnsave.Enabled = true;
                else
                    btnsave.Enabled = false;
            }
            txtVendorName.Focus();
            Fillvendor();
            clearall();
            dgv_vendor.BorderStyle = BorderStyle.None;
            dgv_vendor.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv_vendor.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv_vendor.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv_vendor.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv_vendor.BackgroundColor = Color.White;

            dgv_vendor.EnableHeadersVisualStyles = false;
            dgv_vendor.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_vendor.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_vendor.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtdesc, txtVenderAddress, txtVendorEmail, txtVendorTel, txtVendorName })
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

        private void txtVendorName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }

        private void txtVendorTel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key            
                e.Handled = true; // Suppress the Enter key
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtVendorEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }
        public void Filldetails(int vid)
        {
            try
            {                
                BllSearch<DmnVendor> bllSearch1 = new BllSearch<DmnVendor>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "* ";
                objSearch.Table = "tblVendor";
                objSearch.WhereNdGroupBy = "where status = 1 and VendorId=" + vid;
                objSearch.OrderBy = "";
                var itemSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    txtvid.Text = Convert.ToString(itemSource[0].VendorId);
                    txtVendorName.Text = Convert.ToString(itemSource[0].VendorName);
                    txtVendorTel.Text = Convert.ToString(itemSource[0].Telephone);
                    txtVendorEmail.Text = Convert.ToString(itemSource[0].Email);
                    txtVenderAddress.Text = Convert.ToString(itemSource[0].Address);
                    btn_del_vendor.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void dgv_vendor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int vid = Convert.ToInt32(dgv_vendor.Rows[e.RowIndex].Cells[idno].Value);
            Filldetails(vid);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    Movefocus(1);
                    return true; // Mark the key as handled
                case Keys.Enter:
                    if (dgv_vendor.Focused)
                    {
                        int vid = Convert.ToInt32(dgv_vendor.Rows[dgv_vendor.CurrentCell.RowIndex].Cells[idno].Value);
                        Filldetails(vid);
                    }
                    Movefocus(0);
                    return true; // Mark the key as handled
                case Keys.Escape:

                        FrmDashboard _instancedash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                        _instancedash.Closeform();
  
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        public void Movefocus(int keytype)
        {
            try
            {
                if (txtVendorName.Focused)
                    txtVendorTel.Focus();
                else if (txtVendorTel.Focused)
                    txtVendorEmail.Focus();
                else if (txtVendorEmail.Focused)
                    txtVenderAddress.Focus();
                else if (txtVenderAddress.Focused)
                    txtdesc.Focus();
                else if (txtdesc.Focused)
                {
                    if (btn_del_vendor.Visible)
                        btn_del_vendor.Focus();
                    else
                        btnsave.Focus();
                }
                else if (btn_del_vendor.Focused)
                {
                    if (keytype == 1)
                        btnsave.Focus();
                    else if (keytype == 0)
                        btn_del_vendor.PerformClick();
                }
                else if (keytype == 1)
                {
                    if (btnsave.Focused)
                        btnSearch.Focus();
                    else if (btnSearch.Focused)
                        btnrefresh.Focus();
                    else if (btnrefresh.Focused)
                        btnclose.Focus();
                    else if (btnclose.Focused)
                    {
                        dgv_vendor.Focus();
                        dgv_vendor.CurrentCell = dgv_vendor[1, 0];
                    }
                }
                else if (keytype == 0)
                {
                    if (btnsave.Focused)
                        btnsave.PerformClick();
                    else if (btnrefresh.Focused)
                        btnrefresh.PerformClick();
                    else if (btnSearch.Focused)
                        btnSearch.PerformClick();
                    else if (btnclose.Focused)
                        btnclose.PerformClick();
                }

            }
            catch (Exception ex)
            {

            }
            
        }

        private void FrmVendor_Resize(object sender, EventArgs e)
        {
            textb_shape();
        }
    }
}
