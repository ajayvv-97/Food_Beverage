using BLL;
using CustomMessageBox;
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
    public partial class FrmFTICategory : Form
    {
        public FrmFTICategory()
        {
            InitializeComponent();
        }
        #region Declaration Grid
        public static int cslno = 0;
        public static int cidno = 1;
        public static int cnameno = 2;
        public static int cdesno = 3;
        public static int cstatusno = 4;
        public static int cactionno = 5;
        #endregion
        public bool Mandatory()
        {
            if (txtfoodCate.Text == "" || txtfoodCate.Text == null)
            {
                CMessageBox.Show("Please Enter Food Category Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtfoodCate.Focus();
                return false;
            }
            if (cmb_cate.Text == "" || cmb_cate.SelectedIndex == -1)
            {
                CMessageBox.Show("Please select Category Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_cate.Focus();
                return false;
            }
            return true;
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Mandatory())
                {
                    bool savestatus;
                    DmnCategory dmnfood = new DmnCategory();
                    BllCategory bllcategory = new BllCategory();
                    if (string.IsNullOrWhiteSpace(txtFcid.Text))
                        txtFcid.Text = "0";
                    dmnfood.Fcid = Convert.ToInt32(txtFcid.Text);
                    dmnfood.CategoryName = txtfoodCate.Text;
                    dmnfood.CategoryType = Convert.ToInt32(cmb_cate.SelectedIndex);
                    dmnfood.Description = txtdescr.Text;
                    dmnfood.CreatedBy = 1;
                    dmnfood.BranchId = 1;
                    if (chk_active.Checked)
                        dmnfood.Status = 1;
                    else
                        dmnfood.Status = 0;

                    var usertype = bllcategory.AddeditData("prdInsert_upd_Category", dmnfood);
                    if (usertype.Fcid > 0)
                    {
                        if (Convert.ToUInt32(txtFcid.Text) > 0)
                            CMessageBox.Show("Update Successfully saved");
                        else
                            CMessageBox.Show("Successfully saved");
                        clearall();
                        Fillcategory(Convert.ToInt32(cmb_cate.SelectedIndex));
                    }
                    else
                        CMessageBox.Show("Save failed", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {

            }
        }
        public void clearall()
        {
            txtFcid.Text = "";
            txtdescr.Text = "";
            txtfoodCate.Text = "";
            chk_active.Visible = false;
            btn_del_cate.Visible = false;
        }
        public void Fillcategory(int n)
        {
            try
            {
                BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Fcid,CategoryName,Description,Status";
                objSearch.Table = "tblCategory_FTI";
                switch (n)
                {               
                    case 0:
                        objSearch.WhereNdGroupBy = "where status = 1 and CategoryType=0";
                        break;
                    case 1:
                        objSearch.WhereNdGroupBy = "where status = 1 and CategoryType=1";
                        break;
                    case 2:
                        objSearch.WhereNdGroupBy = "where status = 1 and CategoryType=2";
                        break;
                    case 4:
                        objSearch.WhereNdGroupBy = "where status = 1";
                        break;
                };
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                dgv_category.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_category.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_category.Rows[i].Cells[cslno].Value = i + 1;
                        dgv_category.Rows[i].Cells[cidno].Value = itemSource[i].Fcid;
                        dgv_category.Rows[i].Cells[cnameno].Value = itemSource[i].CategoryName;
                        dgv_category.Rows[i].Cells[cdesno].Value = itemSource[i].Description;
                        dgv_category.Rows[i].Cells[cstatusno].Value = Convert.ToInt32(itemSource[i].Status);
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            clearall();
            Fillcategory(4);
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
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


        private void FrmFTICategory_Load(object sender, EventArgs e)
        {
            dgv_category.BorderStyle = BorderStyle.None;
            dgv_category.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv_category.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv_category.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv_category.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv_category.BackgroundColor = Color.White;

            dgv_category.EnableHeadersVisualStyles = false;
            dgv_category.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_category.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_category.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            Fillcategory(4);
        }

        private void cmb_cate_SelectedValueChanged(object sender, EventArgs e)
        {
            Fillcategory(Convert.ToInt32(cmb_cate.SelectedIndex));
        }

        private void btn_del_cate_Click(object sender, EventArgs e)
        {         
            var result = CMessageBox.Show("Do You want Delete This Category", "Confirm The Action", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                chk_active.Checked = false;
                btnsave.PerformClick();
                Fillcategory(4);
            }
        }


        private void dgv_category_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int catid = Convert.ToInt32(dgv_category.Rows[e.RowIndex].Cells[cidno].Value);
                BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Fcid,CategoryName,Description,Status";
                objSearch.Table = "tblCategory_FTI";
                objSearch.WhereNdGroupBy = "where status = 1 and Fcid=" + catid;
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    txtFcid.Text = Convert.ToString(catid);
                    txtfoodCate.Text = Convert.ToString(itemSource[0].CategoryName);
                    cmb_cate.SelectedIndex = Convert.ToInt32(itemSource[0].CategoryType);
                    txtdescr.Text = itemSource[0].Description;
                    btn_del_cate.Visible = true;
                }

            }
            catch (Exception ex)
            {

            }
        }
    }
}
