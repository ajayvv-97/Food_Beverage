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
    public partial class FrmItemRegistartion : Form
    {
        public FrmItemRegistartion()
        {
            InitializeComponent();
        }
        #region Declare Grid
        public static int slno = 0;
        public static int id = 1;
        public static int incre_name = 2;
        public static int incre_categ = 3;
        public static int iunit = 4;
        public static int iqty = 5;
        public static int iprice = 6;
        public static int idesc = 7;
        public static int istatus = 8;
        public static int iunitid = 9;
        public static int incre_categ_id = 10;
        #endregion
        public void Fillitem()
        {
            try
            {
                BllSearch<DmnItem> bllSearch = new BllSearch<DmnItem>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblItem";
                objSearch.WhereNdGroupBy = "Where status=1 ";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                dgv_item.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_item.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_item.Rows[i].Cells[slno].Value = i + 1;
                        dgv_item.Rows[i].Cells[id].Value = itemSource[i].ItemId;
                        dgv_item.Rows[i].Cells[incre_name].Value = itemSource[i].Itemname;
                        dgv_item.Rows[i].Cells[incre_categ].Value = itemSource[i].Fcid;
                        dgv_item.Rows[i].Cells[iunit].Value = itemSource[i].unit;
                        dgv_item.Rows[i].Cells[iqty].Value = itemSource[i].quantity;
                        dgv_item.Rows[i].Cells[iprice].Value = itemSource[i].price;
                        dgv_item.Rows[i].Cells[idesc].Value = itemSource[i].summary;
                        dgv_item.Rows[i].Cells[istatus].Value = Convert.ToInt32(itemSource[i].Status);
                        dgv_item.Rows[i].Cells[iunitid].Value = itemSource[i].unit;
                        dgv_item.Rows[i].Cells[incre_categ_id].Value = itemSource[i].unit;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void combofill()
        {

            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Fcid,CategoryName";
                objSearch.Table = " tblCategory_FTI";
                objSearch.WhereNdGroupBy = "where Status=1 and CategoryType=2";
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmb_inc_categ.DataSource = gridSource;
                cmb_inc_categ.ValueMember = "Id";
                cmb_inc_categ.DisplayMember = "Value";

                objSearch.Select = " UId,UnitName";
                objSearch.Table = " tblUnit";
                objSearch.WhereNdGroupBy = "where status=1";
                objSearch.OrderBy = "";
                var gridSource1 = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmbunit.DataSource = gridSource1;
                cmbunit.ValueMember = "Id";
                cmbunit.DisplayMember = "Value";

            }
            catch (Exception ex)
            {

            }
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {
                    DmnItem dmnitemreg = new DmnItem();
                    BllItem blltenreg = new BllItem();
                    if (string.IsNullOrWhiteSpace(txtitemid.Text))
                        txtitemid.Text = "0";
                    dmnitemreg.ItemId = Convert.ToInt32(txtitemid.Text);
                    dmnitemreg.Itemname = txtincr_name.Text;
                    dmnitemreg.summary = txtdesc.Text;
                    dmnitemreg.price = Convert.ToDecimal(txtprice.Text);
                    dmnitemreg.quantity = Convert.ToDecimal(txtqty.Text);
                    dmnitemreg.unit = Convert.ToInt32(cmbunit.SelectedValue);
                    dmnitemreg.Fcid = Convert.ToInt32(cmb_inc_categ.SelectedValue);

                    dmnitemreg.CreatedBy = 1;
                    dmnitemreg.BranchId = 1;

                    if (chb_active.Checked)
                        dmnitemreg.Status = 1;
                    else
                        dmnitemreg.Status = 0;
                    var usertype = blltenreg.AddData("prdInsert_upd_ItemReg", dmnitemreg);

                    if (usertype.ItemId > 0)
                    {
                        if (Convert.ToUInt32(txtitemid.Text) > 0)
                            MessageBox.Show("Update Successfully saved");
                        else
                            MessageBox.Show("Successfully saved");

                        clearall();
                        Fillitem();
                    }
                    else
                        MessageBox.Show("Save failed", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void clearall()
        {
            txtincr_name.Text = "";
            txtdesc.Text = "";
            txtqty.Text = "";
            txtprice.Text = "";
            Fillitem();
        }


        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool MandatoryCheck()
        {
            if (txtincr_name.Text == "" || txtincr_name.Text == null)
            {
                MessageBox.Show("Please Enter Item Name", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtitemid.Focus();
                return false;
            }
            //else if (Convert.ToInt32(cmbfcateg.SelectedValue) == 0)
            //{
            //    MessageBox.Show("Please Select Food Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    cmbfcateg.Focus();
            //    return false;
            //}

            return true;
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            Fillitem();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btntoggle_Click(object sender, EventArgs e)
        {
            tlp_registration.Visible = false;
            tlp_grid.Visible = true;
            Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_main.Controls.Add(tlp_grid, 1, 0);
            tlp_grid.Dock = DockStyle.Fill;
        }

        private void toggle_grid_Click(object sender, EventArgs e)
        {
            tlp_grid.Visible = false;
            tlp_registration.Visible = true;
            Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_main.Controls.Add(tlp_registartion_toggle, 1, 0);
            tlp_registartion_toggle.Dock = DockStyle.Fill;
        }

        private void txtprice_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtqty_TextChanged(object sender, EventArgs e)
        {

        }

        private void tlp_registration_Paint(object sender, PaintEventArgs e)
        {
            float borderRadius = 10f; 
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(tlp_registration.ClientRectangle.X, tlp_registration.ClientRectangle.Y, tlp_registration.ClientRectangle.Width, tlp_registration.ClientRectangle.Height);
            path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
            path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
            path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
            path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
            path.CloseFigure();
            tlp_registration.Region = new Region(path);
        }

        private void FrmItemRegistartion_Load(object sender, EventArgs e)
        {
            Fillitem();
            combofill();
            dgv_item.BorderStyle = BorderStyle.None;
            //dgv_item.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            //dgv_item.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            //dgv_item.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            //dgv_item.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            //dgv_item.BackgroundColor = Color.White;

            dgv_item.EnableHeadersVisualStyles = false;
            dgv_item.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_item.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_item.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }
    }
}
