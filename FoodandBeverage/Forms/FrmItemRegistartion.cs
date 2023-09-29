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

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            Fillitem();
        }
    }
}
