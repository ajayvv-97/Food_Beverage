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
        #endregion

        private void cmb_inc_categ_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnsave_Click(object sender, EventArgs e)
        {

        }

        private void FrmIncrediant_Load(object sender, EventArgs e)
        {
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

        private void btntoggle_Click(object sender, EventArgs e)
        {
            tlp_grid.Visible = true;
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
                cmb_item.DataSource = gridSource;
                cmb_item.ValueMember = "Id";
                cmb_item.DisplayMember = "Value";

                objSearch.Select = " UId,UnitName";
                objSearch.Table = " tblUnit";
                objSearch.WhereNdGroupBy = "where status=1";
                objSearch.OrderBy = "";
                var gridSource1 = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmb_unit.DataSource = gridSource;
                cmb_unit.ValueMember = "Id";
                cmb_unit.DisplayMember = "Value";
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

    }
}
