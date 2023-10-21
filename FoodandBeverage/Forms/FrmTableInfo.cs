using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Forms.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static FoodandBeverage.Interfaces.Interfaces;

namespace FoodandBeverage.Forms
{
    public partial class FrmTableInfo : Form
    {
        public FrmTableInfo()
        {
            InitializeComponent();
        }
        #region Declaration Grid
        public static int cslno = 0;
        public static int tidno = 1;
        public static int tcodeno = 2;
        public static int tcapcno = 3;
        public static int tcategno = 4;
        public static int trateno = 5;
        public static int tdesno = 6;
        public static int tstatusno = 7;
        public static int tfcid = 8;
        #endregion
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
            else if (txtcapcity.Text == "" && txtcapcity.Text == null && Convert.ToDecimal(txtcapcity.Text)>=0)
            {
                CMessageBox.Show("Please Enter Table Capacity", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcapcity.Focus();
                return false;
            }
            else if (Convert.ToInt32(cmb_category.SelectedValue) == -1)
            {
                CMessageBox.Show("Please Select Table category Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmb_category.Focus();
                return false;
            }
            //else if (txtrate.Text == "" || txtrate.Text == null || Convert.ToDecimal(txtrate.Text) >= 0)
            //{
            //    CMessageBox.Show("Please Enter Table Rate", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtrate.Focus();
            //    return false;
            //}

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
                    if (string.IsNullOrWhiteSpace(txtrate.Text))
                        txtrate.Text = "0";
                    dmntableinfo.Tid = Convert.ToInt32(txttid.Text);
                    dmntableinfo.TableCode = txttablecode.Text;
                    dmntableinfo.Capcity = Convert.ToInt32(txtcapcity.Text);
                    dmntableinfo.Description = txtdesc.Text;
                    dmntableinfo.Fcid = Convert.ToInt32(cmb_category.SelectedValue);
                    dmntableinfo.Price = Convert.ToDecimal(txtrate.Text);
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
                        Filltable();
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
            txtrate.Text = "";
            cmb_category.SelectedIndex = 0;
            btn_del_table.Visible = false;
        }

        private void FrmTableInfo_Load(object sender, EventArgs e)
        {
            Fillcombo();
            Filltable();
            dgv_table.BorderStyle = BorderStyle.None;
            dgv_table.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv_table.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv_table.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv_table.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv_table.BackgroundColor = Color.White;

            dgv_table.EnableHeadersVisualStyles = false;
            dgv_table.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_table.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_table.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            clearall();
            Filltable();
         
        }

        private void dgv_table_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int tableid = Convert.ToInt32(dgv_table.Rows[e.RowIndex].Cells[tidno].Value);
                BllSearch<DmnTableinfo> bllSearch = new BllSearch<DmnTableinfo>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblTable_info";
                objSearch.WhereNdGroupBy = "where status = 1 and Tid=" + tableid;
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    txttid.Text = Convert.ToString(tableid);
                    txtdesc.Text = Convert.ToString(itemSource[0].Description);
                    cmb_category.SelectedValue= Convert.ToInt64(itemSource[0].Fcid);
                    txtcapcity.Text = Convert.ToString(itemSource[0].Capcity);
                    txttablecode.Text = Convert.ToString(itemSource[0].TableCode);
                    txtrate.Text = Convert.ToString(itemSource[0].Price);
                    btn_del_table.Visible = true;
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void btn_del_table_Click(object sender, EventArgs e)
        {

            var result = CMessageBox.Show("Do You want Delete Record", "Confirm The Action", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                chb_active.Checked = false;
                btnsave.PerformClick();
                Filltable();
            }
        }
            public void Filltable()
            {
            try
            {
                BllSearch<DmnTableinfo> bllSearch = new BllSearch<DmnTableinfo>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "t.*,c.CategoryName";
                objSearch.Table = "tblTable_info t LEFT JOIN tblCategory_FTI c ON C.Fcid=T.Fcid";
                objSearch.WhereNdGroupBy = "where t.status=1";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                dgv_table.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_table.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_table.Rows[i].Cells[cslno].Value = i + 1;
                        dgv_table.Rows[i].Cells[tidno].Value = itemSource[i].Tid;
                        dgv_table.Rows[i].Cells[tcodeno].Value = itemSource[i].TableCode;
                        dgv_table.Rows[i].Cells[tcapcno].Value = Convert.ToInt32(itemSource[i].Capcity);
                        dgv_table.Rows[i].Cells[tcategno].Value = itemSource[i].CategoryName;
                        dgv_table.Rows[i].Cells[trateno].Value = itemSource[i].Price;
                        dgv_table.Rows[i].Cells[tdesno].Value = itemSource[i].Description;
                        dgv_table.Rows[i].Cells[tstatusno].Value = itemSource[i].Status;
                        dgv_table.Rows[i].Cells[tfcid].Value = Convert.ToInt32(itemSource[i].Fcid);
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void txtcapcity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            int returnValue = 0;
            DataTable dtColumnAlign = new DataTable();
            dtColumnAlign.Columns.Add();
            DmnSearch _objSearch = new DmnSearch();
            _objSearch.ColumnHeaders = "ID,TableCode,Capcity,Price,Description";
            _objSearch.Select = "tid as Id,TableCode as Field1,Capcity as Field2,Price as Field3,Description as Field4";
            _objSearch.Table = "tblTable_info";
            _objSearch.WhereNdGroupBy = "where Status = 1";
            _objSearch.OrderBy = " ORDER BY tid";
            var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);
            if (gridSource.Count == 0)
            {
                CMessageBox.Show("No Records", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                using (var search = new FrmSearch<DmnGridFields>(_objSearch, SearchMode.FirstCharacter, this.Width - 150, (this.Height - 150), this.Location.Y + 50, this.Location.X + 20, dtColumnAlign))
                {
                    var dialogResult = search.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        returnValue = Convert.ToInt32(search._retValue);
                    }
                }
                if (returnValue > 0)
                {
                    int tableid = Convert.ToInt32(returnValue);
                    BllSearch<DmnTableinfo> bllSearch1 = new BllSearch<DmnTableinfo>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = "tblTable_info";
                    objSearch.WhereNdGroupBy = "where status = 1 and Tid=" + tableid;
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count > 0)
                    {
                        txttid.Text = Convert.ToString(tableid);
                        txtdesc.Text = Convert.ToString(itemSource[0].Description);
                        cmb_category.SelectedValue = Convert.ToInt64(itemSource[0].Fcid);
                        txtcapcity.Text = Convert.ToString(itemSource[0].Capcity);
                        txttablecode.Text = Convert.ToString(itemSource[0].TableCode);
                        txtrate.Text = Convert.ToString(itemSource[0].Price);
                        btn_del_table.Visible = true;
                    }
                }
            }
        }
    }
}
