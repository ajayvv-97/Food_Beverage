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

namespace FoodandBeverage
{
    public partial class frmItemRegistartion : Form
    {
        public frmItemRegistartion()
        {
            InitializeComponent();
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
                            MessageBox.Show(" Update Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        else
                            MessageBox.Show(" Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        clearall();
                        Fillitem();
                    }
                    else
                        MessageBox.Show(" Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        //public void Fillcombo()
        //{
        //    try
        //    {
        //        BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
        //        DmnSearch objSearch = new DmnSearch();
        //        objSearch.Select = "Fcid,CategoryName";
        //        objSearch.Table = " tblFoodCategory";
        //        objSearch.WhereNdGroupBy = "where status=1";
        //        objSearch.OrderBy = "";
        //        var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
        //        cmbfcateg.DataSource = gridSource;
        //        cmbfcateg.ValueMember = "Id";
        //        cmbfcateg.DisplayMember = "Value";
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Fillitem()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "m.mid as Field1, m.Title as Field2,fc.CategoryName as Field3,m.Description as Field4,m.Status as Field5,m.FcategoryId as Field6 ";
                objSearch.Table = "tblMenu m left join tblFoodCategory fc on fc.Fcid=m.FcategoryId";
                objSearch.WhereNdGroupBy = "Where m.status=1 ";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                dgv_menu.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_menu.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        //dgv_menu.Rows[i].Cells[slno].Value = i + 1;
                        //dgv_menu.Rows[i].Cells[id].Value = itemSource[i].Field1;
                        //dgv_menu.Rows[i].Cells[fname].Value = itemSource[i].Field2;
                        //dgv_menu.Rows[i].Cells[fcateg].Value = itemSource[i].Field3;
                        //dgv_menu.Rows[i].Cells[fdes].Value = itemSource[i].Field4;
                        //dgv_menu.Rows[i].Cells[fstaus].Value = Convert.ToInt32(itemSource[i].Field5);
                        //dgv_menu.Rows[i].Cells[ffcid].Value = itemSource[i].Field6;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        public bool MandatoryCheck()
        {
            if (txtitemid.Text == "" || txtitemid.Text == null)
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
    }
}
