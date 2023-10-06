using BLL;
using Domain;
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

namespace FoodandBeverage.Forms
{
    public partial class FrmUnit : Form
    {
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
                        dmnunit.CreatedBy = 1;
                        dmnunit.BranchId = 1;
                        if (chk_active.Visible)
                        {
                            if (chk_active.Checked)
                                dmnunit.Status = 1;
                            else
                                dmnunit.Status = 0;
                        }
                        else
                            dmnunit.Status = 1;

                        var usertype = bllunit.AddeditData("prdInsert_upd_Unit", dmnunit);
                        if (usertype.UId > 0)
                        {
                            if (Convert.ToUInt32(txtUid.Text) > 0)
                                MessageBox.Show("Update Successfully saved");
                            else
                                MessageBox.Show("Successfully saved");
                            clearall();
                            Fillunit();
                        }
                        else
                            MessageBox.Show("Save failed", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);


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
            Fillunit();
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
                        dgv_unit.Rows[i].Cells[0].Value = i + 1;
                        dgv_unit.Rows[i].Cells[1].Value = itemSource[i].UId;
                        dgv_unit.Rows[i].Cells[2].Value = itemSource[i].UnitName;
                        dgv_unit.Rows[i].Cells[3].Value = itemSource[i].Description;
                        dgv_unit.Rows[i].Cells[4].Value = Convert.ToInt32(itemSource[i].Status);
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
                MessageBox.Show("Please Enter Food Category Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void FrmUnit_Load(object sender, EventArgs e)
        {
            Fillunit();
            dgv_unit.BorderStyle = BorderStyle.None;
            dgv_unit.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv_unit.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv_unit.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv_unit.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv_unit.BackgroundColor = Color.White;

            dgv_unit.EnableHeadersVisualStyles = false;
            dgv_unit.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_unit.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_unit.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            Fillunit();
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
    }
}
