using BLL;
using CustomMessageBox;
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
    public partial class FrmTableInfo : Form
    {
        public FrmTableInfo()
        {
            InitializeComponent();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public bool MandatoryCheck()
        {
            if (txttablecode.Text == "" || txttablecode.Text == null)
            {
                CMessageBox.Show("Please Enter Food Name", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttablecode.Focus();
                return false;
            }
            else if (Convert.ToInt32(cmb_category.SelectedValue) == 0)
            {
                CMessageBox.Show("Please Select Table category Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_category.Focus();
                return false;
            }

            return true;
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {

                    DmnTableinfo dmntableinfo = new DmnTableinfo();
                    BllTableinfo bllmenu = new BllTableinfo();
                    if (string.IsNullOrWhiteSpace(txttid.Text))
                        txttid.Text = "0";
                    dmntableinfo.Tid = Convert.ToInt32(txttid.Text);
                    dmntableinfo.TableCode = txttablecode.Text;
                    dmntableinfo.Capcity = Convert.ToInt32(txtcapcity.Text);
                    dmntableinfo.Description = txtdesc.Text;
                    dmntableinfo.TCid = Convert.ToInt32(cmb_category.SelectedValue);
                    dmntableinfo.RevStatus = 0;
                    dmntableinfo.OrderStatus = 0;
                    dmntableinfo.CreatedBy = 1;
                    dmntableinfo.BranchId = 1;
                    if (chb_active.Checked)
                        dmntableinfo.Status = 1;
                    else
                        dmntableinfo.Status = 0;
                    var usertype = bllmenu.AddData("prdInsert_upd_Tableinfo", dmntableinfo);

                    if (usertype.Tid > 0)
                    {
                        if (Convert.ToUInt32(txttid.Text) > 0)
                            CMessageBox.Show(" Update Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        else
                            CMessageBox.Show(" Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        clearall();
                        //Fillmenu();
                    }
                    else
                        CMessageBox.Show(" Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void Fillcombo()
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Fcid,CategoryName";
                objSearch.Table = " tblCategory_FTI";
                objSearch.WhereNdGroupBy = "where Status=1 and CategoryType=1";
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmb_category.DataSource = gridSource;
                cmb_category.ValueMember = "Id";
                cmb_category.DisplayMember = "Value";
            }
            catch (Exception ex)
            {

            }
        }
        public void clearall()
        {
            txttid.Text = "";
            txtcapcity.Text = "";
            txttablecode.Text = "";
            //Fillmenu();
        }

        private void FrmTableInfo_Load(object sender, EventArgs e)
        {
            Fillcombo();
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            clearall();
        }


    }
}
