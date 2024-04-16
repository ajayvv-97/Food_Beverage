using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmAddon : Form
    {
        #region Declaration Grid
        public static int aslno = 0;
        public static int aidno = 1;
        public static int anameno = 2;
        public static int apriceno = 3;
        public static int acategno = 4;
        public static int adesno = 5;
        public static int astatusno = 6;
        public static int afcidno = 7;
        public static int aactiveno = 8;
        #endregion
        public FrmAddon()
        {
            InitializeComponent();
        }
         private void FrmAddon_Load(object sender, EventArgs e)
        {

            if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
            {
                FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                if (_instanceFrmdash.ChkWritePermission(Convert.ToInt32(formsenum.Addon)))
                    btnsave.Enabled = true;
                else
                    btnsave.Enabled = false;
            }
            txtcaption.Focus();

            combofill();
            FillAddon();
            textb_shape();

            dgv_addon.BorderStyle = BorderStyle.None;
            dgv_addon.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv_addon.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv_addon.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv_addon.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv_addon.BackgroundColor = Color.White;

            dgv_addon.EnableHeadersVisualStyles = false;
            dgv_addon.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_addon.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_addon.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void btn_categ_Click(object sender, EventArgs e)
        {
            pnl_categoryadd.Visible = true;
            pnl_categoryadd.Location = new Point(this.ClientSize.Width / 2 - pnl_categoryadd.Size.Width / 2, this.ClientSize.Height / 2 - pnl_categoryadd.Size.Height / 2);
        }

        private void btnClosecat_Click(object sender, EventArgs e)
        {
            pnl_categoryadd.Visible = false;
        }
        public bool Mandatory()
        {
            if (txtcategoryname.Text == "" || txtcategoryname.Text == null)
            {
                CMessageBox.Show("Please Enter Addon Category ", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcategoryname.Focus();
                return false;
            }
            return true;
        }
        public void combofill()
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Fcid,CategoryName";
                objSearch.Table = " tblCategory_FTI";
                objSearch.WhereNdGroupBy = "where status = 1 and CategoryType=4";
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

        private void btnSavecate_Click(object sender, EventArgs e)
        {
            try
            {
                if (Mandatory())
                {
                    bool savestatus;
                    DmnCategory dmnfood = new DmnCategory();
                    BllCategory bllcategory = new BllCategory();
                    if (string.IsNullOrWhiteSpace(txtcategotyid.Text))
                        txtcategotyid.Text = "0";
                    dmnfood.Fcid = Convert.ToInt32(txtcategotyid.Text);
                    dmnfood.CategoryName = txtcategoryname.Text;
                    dmnfood.CategoryType = 4;
                    dmnfood.Description = txtdesc_cate.Text;
                    dmnfood.CreatedBy = FrmDashboard.UserId;
                    dmnfood.BranchId = FrmDashboard.BranchId;
                    dmnfood.Status = 1;
                    var usertype = bllcategory.AddeditData("prdInsert_upd_Category", dmnfood);
                    if (usertype.Fcid > 0)
                    {
                        //if (Convert.ToUInt32(txtFcid.Text) > 0)
                        //    CMessageBox.Show("Update Successfully saved");
                        //else
                        CMessageBox.Show("Successfully saved");
                        combofill();
                        txtdesc_cate.Text = "";
                        txtcategoryname.Text = "";
                        txtcategotyid.Text = "";
                        pnl_categoryadd.Visible = false;
                    }
                    else
                        CMessageBox.Show("Save failed", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {

            }
        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtdesc_cate, txtcategoryname, txtdesc, txtprice, txtcaption })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }
            catch (Exception)
            {

            }
        }

        private void ApplyRoundedTextBox(TextBox textBox, int radius)
        {
            // Create a new GraphicsPath object
            GraphicsPath path = new GraphicsPath();
            // Define the shape of the path using a rounded rectangle
            Rectangle rect = new Rectangle(0, 0, textBox.Width, textBox.Height);
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
            path.CloseAllFigures();
            // Set the Region property of the textbox to the new path
            textBox.Region = new Region(path);
        }

        #endregion

        private void btn_cate_close_Click(object sender, EventArgs e)
        {
            pnl_categoryadd.Visible = false;
        }
        public bool MandatoryCheck()
        {
            if (string.IsNullOrWhiteSpace(txtprice.Text))
                txtprice.Text = "0";

            if (txtcaption.Text == "" || txtcaption.Text == null)
            {
                CMessageBox.Show("Please Enter Caption", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtcaption.Focus();
                return false;
            }
            else if (Convert.ToDecimal(txtprice.Text) == 0)
            {
                CMessageBox.Show("Please Enter Table Price", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtprice.Focus();
                return false;
            }
            else if (Convert.ToInt32(cmb_category.SelectedValue) == -1)
            {
                CMessageBox.Show("Please Select Addon category Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                    DmnAddon dmntableinfo = new DmnAddon();
                    BllAddon blladdon = new BllAddon();
                    if (string.IsNullOrWhiteSpace(txtaid.Text))
                        txtaid.Text = "0";
                    if (string.IsNullOrWhiteSpace(txtprice.Text))
                        txtprice.Text = "0";
                    dmntableinfo.Aid = Convert.ToInt32(txtaid.Text);
                    dmntableinfo.AddonName = txtcaption.Text;
                    dmntableinfo.Price = Convert.ToInt32(txtprice.Text);
                    dmntableinfo.Fcid = Convert.ToInt32(cmb_category.SelectedValue);
                    dmntableinfo.Description = txtdesc.Text;
                    dmntableinfo.CreatedBy = FrmDashboard.UserId;
                    dmntableinfo.BranchId = FrmDashboard.BranchId;
                    if (chb_active.Checked)
                        dmntableinfo.Status = 1;
                    else
                        dmntableinfo.Status = 0;
                    var usertype = blladdon.AddData("prdInsert_upd_Addon", dmntableinfo);

                    if (usertype.Aid > 0)
                    {
                        if (Convert.ToUInt32(txtaid.Text) > 0)
                            CMessageBox.Show(" Update Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        else
                            CMessageBox.Show(" Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        clearall();
                        FillAddon();
                    }
                    else
                        CMessageBox.Show(" Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void FillAddon()
        {
            try
            {
                BllSearch<DmnAddon> bllSearch = new BllSearch<DmnAddon>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "t.*,c.CategoryName";
                objSearch.Table = "tblAddon t LEFT JOIN tblCategory_FTI c ON C.Fcid=T.Fcid and c.status=1";
                objSearch.WhereNdGroupBy = "where t.status in (1)";
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                dgv_addon.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_addon.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_addon.Rows[i].Cells[aslno].Value = i + 1;
                        dgv_addon.Rows[i].Cells[aidno].Value = itemSource[i].Aid;
                        dgv_addon.Rows[i].Cells[anameno].Value = itemSource[i].AddonName;
                        dgv_addon.Rows[i].Cells[apriceno].Value = Convert.ToDecimal(itemSource[i].Price);
                        dgv_addon.Rows[i].Cells[acategno].Value = Convert.ToString(itemSource[i].CategoryName);
                        dgv_addon.Rows[i].Cells[adesno].Value = itemSource[i].Description;
                        dgv_addon.Rows[i].Cells[astatusno].Value = itemSource[i].Status;
                        dgv_addon.Rows[i].Cells[afcidno].Value = itemSource[i].Fcid;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void clearall()
        {
            txtaid.Text = "";
            txtcaption.Text = "";
            txtprice.Text = "";
            txtdesc.Text = "";
            cmb_category.SelectedIndex = 0;
            btn_del_addon.Visible = false;
            combofill();

        }

        private void FrmAddon_Resize(object sender, EventArgs e)
        {
            textb_shape();
        }

        private void txtprice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as System.Windows.Forms.TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            string input = (sender as TextBox).Text;
            if (e.KeyChar == '.')
            {
                if ((sender as TextBox).MaxLength == 4)
                    (sender as TextBox).MaxLength = 7;
                else
                {
                    if (input.Contains('.'))
                    {
                        (sender as TextBox).MaxLength = 7;
                    }
                    else
                    {
                        (sender as TextBox).MaxLength = 4;
                    }
                }

            }
            else
            {
                if (input.Contains('.'))
                {
                    (sender as TextBox).MaxLength = 7;
                }
                else
                {
                    (sender as TextBox).MaxLength = 4;
                }
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    Movefocus(1);
                    return true; 
                case Keys.Enter:
                    Movefocus(0);
                    return true; 
                case Keys.Escape:
                    FrmDashboard _instancedash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                    _instancedash.Closeform();
                    return true;

                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        public void Movefocus(int keytype)
        {
            //txtcaption,txtprice,cmb_category,txtdesc
            if (txtcaption.Focused)
            {
                txtprice.Focus();
            }
            else if (txtprice.Focused)
            {
                cmb_category.Focus();
            }
            else if (cmb_category.Focused)
            {
                txtdesc.Focus();
            }
            else if (txtdesc.Focused)
            {
                if (btn_del_addon.Visible)
                    btn_del_addon.Focus();
                else
                    btnsave.Focus();
            }
            else if (btn_del_addon.Focused)
            {
                if (keytype == 1)
                    btnsave.Focus();
                else if (keytype == 0)
                    btn_del_addon.PerformClick();
            }
            else if (keytype == 1)
            {
                if (btnsave.Focused)
                    btnSearch.Focus();
                else if (btnSearch.Focused)
                    btnrefresh.Focus();
                else if (btnrefresh.Focused)
                    btnclose.Focus();
            }
            else if (keytype == 0)
            {
                if (btnsave.Focused)
                    btnsave.PerformClick();
                else if (btnrefresh.Focused)
                    btnrefresh.PerformClick();
                else if (btnSearch.Focused)
                    btnSearch.PerformClick();
                else if (btnclose.Focused)
                    btnclose.PerformClick();
            }

        }

        private void dgv_addon_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int aid = Convert.ToInt32(dgv_addon.Rows[e.RowIndex].Cells[aidno].Value);
            Filldetails(aid);
        }
        public void Filldetails(int aid)
        {
            try
            {
                BllSearch<DmnAddon> bllSearch = new BllSearch<DmnAddon>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblAddon";
                objSearch.WhereNdGroupBy = "where status in(1)and Aid=" + aid;
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    txtaid.Text = Convert.ToString(aid);
                    cmb_category.SelectedValue = Convert.ToInt64(itemSource[0].Fcid);
                    txtprice.Text = Convert.ToString(itemSource[0].Price);
                    txtdesc.Text = Convert.ToString(itemSource[0].Description);
                    txtcaption.Text = Convert.ToString(itemSource[0].AddonName);
                    btn_del_addon.Visible = true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                int returnValue = 0;
                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "ID,Caption,Price,Addon Category,Description";
                _objSearch.Select = "a.aid as Id,a.AddonName as Field1,a.Price as Field2,c.CategoryName Field3,a.Description as Field4";
                _objSearch.Table = "tblAddon a LEFT JOIN tblCategory_FTI c ON C.Fcid=a.Fcid and c.status=1";
                _objSearch.WhereNdGroupBy = "where a.Status = 1";
                _objSearch.OrderBy = " ORDER BY a.aid";

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
                        int aid = Convert.ToInt32(returnValue);
                        BllSearch<DmnAddon> bllSearch1 = new BllSearch<DmnAddon>();
                        DmnSearch objSearch = new DmnSearch();
                        objSearch.Select = "*";
                        objSearch.Table = "tblAddon";
                        objSearch.WhereNdGroupBy = "where status = 1 and Aid=" + aid;
                        objSearch.OrderBy = "";
                        var itemSource = bllSearch1.GetSearchData("prdSearch", objSearch);
                        if (itemSource.Count > 0)
                        {
                            txtaid.Text = Convert.ToString(aid);
                            txtdesc.Text = Convert.ToString(itemSource[0].Description);
                            cmb_category.SelectedValue = Convert.ToInt64(itemSource[0].Fcid);
                            txtcaption.Text = Convert.ToString(itemSource[0].AddonName);
                            txtprice.Text = Convert.ToString(itemSource[0].Price);
                            btn_del_addon.Visible = true;

                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }          
            
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            clearall();
            FillAddon();
        }

        private void btn_del_addon_Click(object sender, EventArgs e)
        {
            var result = CMessageBox.Show("Do You want Delete Record", "Confirm The Action", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                chb_active.Checked = false;
                btnsave.PerformClick();
                FillAddon();
            }
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
