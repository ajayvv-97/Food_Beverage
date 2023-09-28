using BLL;
using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmFoodCategory : Form
    {
        public FrmFoodCategory()
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

                        DmnFoodCategory dmnfood = new DmnFoodCategory();
                        BllFoodCategory bllfood = new BllFoodCategory();
                        if (string.IsNullOrWhiteSpace(txtFcid.Text))
                            txtFcid.Text = "0";
                        dmnfood.Fcid = Convert.ToInt32(txtFcid.Text);
                        dmnfood.CategoryName = txtfoodCate.Text;
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
                        var usertype = bllfood.AddData("prdInsert_upd_FoodCategory", dmnfood);

                            if (usertype.Fcid>0)
                            {
                                if (Convert.ToUInt32(txtFcid.Text) > 0)
                                    MessageBox.Show(" Update Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                else
                                    MessageBox.Show(" Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                clearall();
                                Fillusertype();
                            }
                            else
                                MessageBox.Show(" Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}
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
                BllSearch<DmnFoodCategory> bllSearch = new BllSearch<DmnFoodCategory>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Fcid,CategoryName,Description,Status";
                objSearch.Table = "tblFoodCategory";
                objSearch.WhereNdGroupBy = "Where status=1 ";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                dgv_usertype.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_usertype.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_usertype.Rows[i].Cells[0].Value = i + 1;
                        dgv_usertype.Rows[i].Cells[1].Value = itemSource[i].Fcid;
                        dgv_usertype.Rows[i].Cells[2].Value = itemSource[i].CategoryName;
                        dgv_usertype.Rows[i].Cells[3].Value = itemSource[i].Description;
                        dgv_usertype.Rows[i].Cells[4].Value = Convert.ToInt32(itemSource[i].Status);
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
    }
}
