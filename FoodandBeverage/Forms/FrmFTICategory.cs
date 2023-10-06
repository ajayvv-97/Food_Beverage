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
    public partial class FrmFTICategory : Form
    {
        public FrmFTICategory()
        {
            InitializeComponent();
        }
        public bool MandatoryCheckuser()
        {
            if (txtfoodCate.Text == "" || txtfoodCate.Text == null)
            {
                MessageBox.Show("Please Enter Food Category Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
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
                        if (txtfoodCate.Text != "")
                        {

                        DmnCategory dmnfood = new DmnCategory();
                        BllCategory bllcategory = new BllCategory();
                        if (string.IsNullOrWhiteSpace(txtFcid.Text))
                            txtFcid.Text = "0";
                        dmnfood.Fcid = Convert.ToInt32(txtFcid.Text);
                        dmnfood.CategoryName = txtfoodCate.Text;
                        dmnfood.CategoryType =Convert.ToInt32(cmb_cate.SelectedIndex);
                        dmnfood.Description = txtdescr.Text;
                        dmnfood.CreatedBy = 1;
                        dmnfood.BranchId = 1;
                        if (chk_active.Visible)
                        {
                            if (chk_active.Checked)
                                dmnfood.Status = 1;
                            else
                                dmnfood.Status = 0;
                        }
                        else
                            dmnfood.Status = 1;
                       
                            var usertype = bllcategory.AddeditData("prdInsert_upd_Category", dmnfood);
                        if (usertype.Fcid>0)
                            {
                                if (Convert.ToUInt32(txtFcid.Text) > 0)
                                CMessageBox.Show("Update Successfully saved");
                            else
                                CMessageBox.Show("Successfully saved");
                                clearall();
                                Fillusertype();
                            }
                            else
                            CMessageBox.Show("Save failed", "Error", MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);

                        
                    }
                    else
                        txtfoodCate.Focus();
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
            Fillusertype();
        }
        public void Fillusertype()
        {
            try
            {
                BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Fcid,CategoryName,Description,Status";
                objSearch.Table = "tblFoodCategory";
                objSearch.WhereNdGroupBy = "Where status=1 ";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                dgv_category.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_category.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_category.Rows[i].Cells[0].Value = i + 1;
                        dgv_category.Rows[i].Cells[1].Value = itemSource[i].Fcid;
                        dgv_category.Rows[i].Cells[2].Value = itemSource[i].CategoryName;
                        dgv_category.Rows[i].Cells[3].Value = itemSource[i].Description;
                        dgv_category.Rows[i].Cells[4].Value = Convert.ToInt32(itemSource[i].Status);
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

        private void cmb_cate_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }
    }
}
