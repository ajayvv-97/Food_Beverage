using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Forms.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FoodandBeverage.Interfaces.Interfaces;

namespace FoodandBeverage.Forms
{
    public partial class FrmUnit : Form
    {
        #region Declare Grid
        public static int slno = 0;
        public static int idno = 1;
        public static int iunitno = 2;
        public static int idesno = 3;
        public static int istatusno = 4;

        #endregion
        public FrmUnit()
        {
            InitializeComponent();
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheckuser())
                {

                    //if (clsGeneral.NameExist_UserType(txtusertype.Text))
                    //{
                    bool savestatus;
                    if (txtunit.Text != "")
                    {
                        DmnUnit dmnunit = new DmnUnit();
                        BllUnit bllunit = new BllUnit();
                        if (string.IsNullOrWhiteSpace(txtUid.Text))
                            txtUid.Text = "0";
                        dmnunit.UId = Convert.ToInt32(txtUid.Text);
                        dmnunit.UnitName = txtunit.Text;
                        dmnunit.Description = txtdescr.Text;
                        dmnunit.CreatedBy = FrmDashboard.UserId;
                        dmnunit.BranchId = FrmDashboard.BranchId;
                        if (chk_active.Checked)
                            dmnunit.Status = 1;
                        else
                            dmnunit.Status = 0;
                        var usertype = bllunit.AddeditData("prdInsert_upd_Unit", dmnunit);
                        if (usertype.UId > 0)
                        {
                            if (Convert.ToUInt32(txtUid.Text) > 0)
                                CMessageBox.Show("Update Successfully saved");
                            else
                                CMessageBox.Show("Successfully saved");
                            clearall();
                            Fillunit();
                        }
                        else
                            CMessageBox.Show("Save failed", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                    }
                    else
                        txtunit.Focus();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void clearall()
        {
            txtUid.Text = "";
            txtdescr.Text = "";
            txtunit.Text = "";
            chk_active.Visible = false;
        }
        public void Fillunit()
        {
            try
            {
                BllSearch<DmnUnit> bllSearch = new BllSearch<DmnUnit>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "uid,unitName,Description,Status";
                objSearch.Table = "tblunit";
                objSearch.WhereNdGroupBy = "Where status=1 ";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                dgv_unit.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_unit.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_unit.Rows[i].Cells[slno].Value = i + 1;
                        dgv_unit.Rows[i].Cells[idno].Value = itemSource[i].UId;
                        dgv_unit.Rows[i].Cells[iunitno].Value = itemSource[i].UnitName;
                        dgv_unit.Rows[i].Cells[idesno].Value = itemSource[i].Description;
                        dgv_unit.Rows[i].Cells[istatusno].Value = Convert.ToInt32(itemSource[i].Status);
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        public bool MandatoryCheckuser()
        {
            if (txtunit.Text == "" || txtunit.Text == null)
            {
                CMessageBox.Show("Please Enter Unit Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void FrmUnit_Load(object sender, EventArgs e)
        {
            txtunit.Focus();
            Fillunit(); dgv_unit.BorderStyle = BorderStyle.None;
            dgv_unit.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv_unit.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv_unit.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv_unit.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv_unit.BackgroundColor = Color.White;

            dgv_unit.EnableHeadersVisualStyles = false;
            dgv_unit.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_unit.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_unit.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            textb_shape();
        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtdescr,txtunit })
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
           
        private void btnrefresh_Click(object sender, EventArgs e)
        {
            clearall();
            Fillunit();        
            btn_del_unit.Visible = false;
        }

        private void tlp_registartion_Paint(object sender, PaintEventArgs e)
        {
            float borderRadius = 10f; // adjust this value to change the roundness of the corners
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(tlp_registartion.ClientRectangle.X, tlp_registartion.ClientRectangle.Y, tlp_registartion.ClientRectangle.Width, tlp_registartion.ClientRectangle.Height);
            path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
            path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
            path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
            path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
            path.CloseFigure();
            tlp_registartion.Region = new Region(path);
        }

        private void btn_del_unit_Click(object sender, EventArgs e)
        {
            var result = CMessageBox.Show("Do You want Delete This Unit", "Confirm The Action", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                chk_active.Checked = false;
                btnsave.PerformClick();
                Fillunit();
            }
        }
        public void Filldetails(int unitid)
        {
            try
            {

                BllSearch<DmnUnit> bllSearch = new BllSearch<DmnUnit>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblunit";
                objSearch.WhereNdGroupBy = "where status = 1 and uid=" + unitid;
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    txtUid.Text = Convert.ToString(unitid);
                    txtunit.Text = Convert.ToString(itemSource[0].UnitName);
                    txtdescr.Text = Convert.ToString(itemSource[0].Description);
                    btn_del_unit.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void dgv_unit_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int unitid = Convert.ToInt32(dgv_unit.Rows[e.RowIndex].Cells[idno].Value);
            Filldetails(unitid);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            int returnValue = 0;
            DataTable dtColumnAlign = new DataTable();
            dtColumnAlign.Columns.Add();
            DmnSearch _objSearch = new DmnSearch();
            _objSearch.ColumnHeaders = "ID,UnitName,Description";
            _objSearch.Select = "uid as Id,UnitName  as Field1,Description  as Field2";
            _objSearch.Table = "tblUnit";
            _objSearch.WhereNdGroupBy = "where Status = 1";
            _objSearch.OrderBy = " ORDER BY UnitName";
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
                    int unitid = Convert.ToInt32(returnValue);
                    BllSearch<DmnUnit> bllSearch1 = new BllSearch<DmnUnit>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = "tblunit";
                    objSearch.WhereNdGroupBy = "where status = 1 and uid=" + unitid;
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count > 0)
                    {
                        txtUid.Text = Convert.ToString(unitid);
                        txtunit.Text = Convert.ToString(itemSource[0].UnitName);
                        txtdescr.Text = Convert.ToString(itemSource[0].Description);
                        btn_del_unit.Visible = true;
                    }
                }
            }
        }

        private void txtunit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    Movefocus(1);
                    return true; // Mark the key as handled
                case Keys.Enter:
                    if (dgv_unit.Focused)
                    {
                        int unitid = Convert.ToInt32(dgv_unit.Rows[dgv_unit.CurrentCell.RowIndex].Cells[idno].Value);
                        Filldetails(unitid);
                    }
                    Movefocus(0);
                    return true; // Mark the key as handled
                case Keys.Escape:
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        public void Movefocus(int keytype)
        {
            if (txtunit.Focused)
                txtdescr.Focus(); 
            else if (txtdescr.Focused)
            {       
                if (btn_del_unit.Visible)
                    btn_del_unit.Focus();
                else
                    btnsave.Focus();
            }
            else if (btn_del_unit.Focused)
            {
                if (keytype == 1)
                    btnsave.Focus();
                else if (keytype == 0)
                    btn_del_unit.PerformClick();
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
                    dgv_unit.Focus();
                    dgv_unit.CurrentCell = dgv_unit[0, 0];
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
    }
}
