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
    public partial class FrmIncrediant : Form
    {
        public FrmIncrediant()
        {
            InitializeComponent();
        }
        #region
        public static int Rslno = 0;
        public static int Ritemno = 1;
        public static int Rqtyno = 2;
        public static int Runitno = 3;
        public static int Raction = 4; 
        public static int RIng_DetailsIdno = 5; 
        #endregion

        

        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {
                    Dmnincrediant status = new Dmnincrediant();
                    Dmnincrediant dmnincr = new Dmnincrediant();
                    Bllincrediant bllincr = new Bllincrediant();
                    //if (string.IsNullOrWhiteSpace(txtincrid.Text))
                    //    txtincrid.Text = "0";

                    for(int i=0;i<dgv_registartion.RowCount;i++)
                    {
                        if(  (  (dgv_registartion.Rows[i].Cells[Ritemno].Value)!= null || (dgv_registartion.Rows[i].Cells[Ritemno].Value) != "")  &&
                            (   (dgv_registartion.Rows[i].Cells[Rqtyno].Value) != "" || ( Convert.ToDecimal(dgv_registartion.Rows[i].Cells[Rqtyno].Value))<=0) 
                            )

                        dmnincr.Ing_DetailsId = Convert.ToInt32(dgv_registartion.Rows[i].Cells[RIng_DetailsIdno].Value);
                        dmnincr.ItemId = Convert.ToInt32(dgv_registartion.Rows[i].Cells[Ritemno].Value);
                        dmnincr.quantity = Convert.ToDecimal(dgv_registartion.Rows[i].Cells[Rqtyno].Value);
                        dmnincr.unit = Convert.ToInt32(dgv_registartion.Rows[i].Cells[Runitno].Value);
                        dmnincr.Mid = Convert.ToInt32(cmb_menu.SelectedValue);
                        dmnincr.CreatedBy = 1;
                        dmnincr.BranchId = 1;

                        if (chb_active.Checked)
                            dmnincr.Status = 1;
                        else
                            dmnincr.Status = 0;
                        status = bllincr.AddData("prdInsert_upd_Incrediant", dmnincr);
                    }       

                    if (status.ItemId > 0)
                    {
                        if (Convert.ToUInt32(txtincrid.Text) > 0)
                            MessageBox.Show("Update Successfully saved");
                        else
                            MessageBox.Show("Successfully saved");

                        clearall();
                        //Fillitem();
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
            txtdesc.Text = "";
            cmb_menu.SelectedIndex = 0;
            dgv_registartion.Rows.Clear();
            dgv_registartion.Rows.Add();


        }
        public bool MandatoryCheck()
        {
            //if (txtincr_name.Text == "" || txtincr_name.Text == null)
            //{
            //    MessageBox.Show("Please Enter Item Name", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtitemid.Focus();
            //    return false;
            //}
            ////else if (Convert.ToInt32(cmbfcateg.SelectedValue) == 0)
            ////{
            ////    MessageBox.Show("Please Select Food Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            ////    cmbfcateg.Focus();
            ////    return false;
            ////}

            return true;
        }
        private void FrmIncrediant_Load(object sender, EventArgs e)
        {
            clearall();
            combofill();
            rearrange_slno();

            dgv_registartion.BorderStyle = BorderStyle.None;
            dgv_registartion.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv_registartion.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv_registartion.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv_registartion.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv_registartion.BackgroundColor = Color.White;

            dgv_registartion.EnableHeadersVisualStyles = false;
            dgv_registartion.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_registartion.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_registartion.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;


        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState= FormWindowState.Minimized;
        }


        public void combofill()
        {
            
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "MId,Title";
                objSearch.Table = " tblMenu";
                objSearch.WhereNdGroupBy = "where status=1";
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmb_menu.DataSource = gridSource;
                cmb_menu.ValueMember = "Id";
                cmb_menu.DisplayMember = "Value";  

                objSearch.Select = " UId,UnitName";
                objSearch.Table = " tblUnit";
                objSearch.WhereNdGroupBy = "where status=1";
                objSearch.OrderBy = "";
                var gridSource1 = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmb_unit.DataSource = gridSource1;
                cmb_unit.ValueMember = "Id";
                cmb_unit.DisplayMember = "Value";

                objSearch.Select = "ItemId,Itemname";
                objSearch.Table = " tblItem";
                objSearch.WhereNdGroupBy = "where Status=1 ";
                objSearch.OrderBy = "";
                var gridSource3 = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmb_item.DataSource = gridSource3;
                cmb_item.ValueMember = "Id";
                cmb_item.DisplayMember = "Value";
            }
            catch (Exception ex)
            {

            }
        }
        public void rearrange_slno()
        {
            for(int i=0;i< dgv_registartion.RowCount;i++)
            {
                dgv_registartion.Rows[i].Cells[Rslno].Value = i+1;
            }
        }

        private void dgv_registartion_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == Raction)
            {
                if (e.RowIndex == dgv_registartion.Rows.Count - 1)
                {
                    if (IsValidData(e.RowIndex))
                        return;
                    dgv_registartion.Rows.Add();
                    rearrange_slno();
                }
                else
                {
                    if (IsValidData(e.RowIndex))
                        return;
                 
                }
            }
        }
        private bool IsValidData(int rowno)
        {
            DataGridViewComboBoxCell cell = dgv_registartion.Rows[rowno].Cells[Ritemno] as DataGridViewComboBoxCell;
            string selectedValue = cell.EditedFormattedValue?.ToString();
            if (string.IsNullOrWhiteSpace(selectedValue) || selectedValue == ".")
            {
                MessageBox.Show("Please Select the item", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgv_registartion.CurrentCell = dgv_registartion.Rows[rowno].Cells[Ritemno];
                dgv_registartion.BeginEdit(false);

                return true;
            }
            DataGridViewTextBoxCell cell1 = dgv_registartion[Rqtyno, rowno] as DataGridViewTextBoxCell;
            if (cell.EditedFormattedValue.ToString() == "" || cell.EditedFormattedValue.ToString() == "." || cell.EditedFormattedValue.ToString() == string.Empty )
            {
                MessageBox.Show("Please Enter the Quantity", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgv_registartion.CurrentCell = dgv_registartion.Rows[rowno].Cells[Rqtyno];
                dgv_registartion.BeginEdit(false);
                return true;
            }

            DataGridViewComboBoxCell cell2 = dgv_registartion.Rows[rowno].Cells[Runitno] as DataGridViewComboBoxCell;
            string selectedValue2 = cell2.EditedFormattedValue?.ToString();
            if (string.IsNullOrWhiteSpace(selectedValue2) || selectedValue2 == ".")
            {
                MessageBox.Show("Please Select the Unit Type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgv_registartion.CurrentCell = dgv_registartion.Rows[rowno].Cells[Runitno];
                dgv_registartion.BeginEdit(false);

                return true;
            }
            return false;
        }

        
        private void dgv_registartion_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (e.RowIndex == dgv_registartion.Rows.Count - 1)
                    e.Value = Properties.Resources.add_row;
                else
                    e.Value = Properties.Resources.edit_row;
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void cmb_menu_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BllSearch<Dmnincrediant> bllSearch = new BllSearch<Dmnincrediant>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "*";
            objSearch.Table = "tblIngredientDetails";
            objSearch.WhereNdGroupBy = "where status=1 and mid= " + cmb_menu.SelectedValue;
            objSearch.OrderBy = "";
            var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
            txtincrid.Text = "0";
            if(gridSource.Count>0)
            {
                dgv_registartion.Rows.Add(gridSource.Count);
                cmb_menu.SelectedValue = Convert.ToInt64(gridSource[0].Mid);
                txtdesc.Text = Convert.ToString(gridSource[0].Instructions);

                if (gridSource[0].Status == 1)
                    chb_active.Checked = true;
                else
                    chb_active.Checked = false;
                for (int i = 0; i < gridSource.Count; i++)
                {
                    dgv_registartion.Rows[i].Cells[RIng_DetailsIdno].Value = Convert.ToInt32(gridSource[i].Ing_DetailsId);
                    dgv_registartion.Rows[i].Cells[Ritemno].Value = Convert.ToInt64(gridSource[i].ItemId);
                    dgv_registartion.Rows[i].Cells[Rqtyno].Value = Convert.ToDecimal(gridSource[i].ItemId);
                    dgv_registartion.Rows[i].Cells[Runitno].Value = Convert.ToInt64(gridSource[i].ItemId);                  
                }
                rearrange_slno();
            }
            
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            clearall();
        }
    }
}
