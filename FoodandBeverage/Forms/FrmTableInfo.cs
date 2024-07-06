using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
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
    public partial class FrmTableInfo : Form
    {
        public FrmTableInfo()
        {
            InitializeComponent();
        }
        ClsGeneral clsgen = new ClsGeneral();

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
        public static int tactiveno = 9;
        #endregion
        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool MandatoryCheck_Activeorder()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "count(*) Field1";
                objSearch.Table = "tblTable_info";
                objSearch.WhereNdGroupBy = "where status=1 and OrderStatus=1 and Tid=" + txttid.Text;
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    if (Convert.ToInt32(itemSource[0].Field1) > 0)
                    {
                        CMessageBox.Show("There is Already An Active Order For This Table.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    else if (Convert.ToInt32(itemSource[0].Field1) == 0)
                    {
                        return true;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Table page MandatoryCheck_Activeorder()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Table));
                return false;
            }

        }

        public bool MandatoryCheck()
        {
            try
            {
                if (string.IsNullOrEmpty(txttablecode.Text))
                {
                    CMessageBox.Show("Please Enter Food Name", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txttablecode.Focus();
                    return false;
                }
                else if (string.IsNullOrEmpty(txtcapcity.Text) || Convert.ToDecimal(txtcapcity.Text) < 0)
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Table page MandatoryCheck()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Table));
                return false;
            }
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
                    dmntableinfo.CreatedBy = FrmDashboard.UserId;
                    dmntableinfo.BranchId = FrmDashboard.BranchId;
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Table page btnsave_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Table));
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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Table page Fillcombo()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Table));
            }
        }
        public void clearall()
        {
            try
            {


                txttid.Text = "";
                txtcapcity.Text = "";
                txttablecode.Text = "";
                txtrate.Text = "";
                txtdesc.Text = "";
                cmb_category.SelectedIndex = 0;
                btn_del_table.Visible = false;
                pbactive_in.Visible = false;
                chb_active.Checked = true;
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Table page clearall()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Table));
            }
        }

        private void FrmTableInfo_Load(object sender, EventArgs e)
        {
            try
            {
                if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                {
                    FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                    if (_instanceFrmdash.ChkWritePermission(Convert.ToInt32(formsenum.Table)))
                        btnsave.Enabled = true;
                    else
                        btnsave.Enabled = false;
                }
                txttablecode.Focus();
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Table page FrmTableInfo_Load()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Table));
            }
        }

        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txttablecode, txtcapcity, txtdesc, txtrate })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Table page textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Table));
            }
        }

        private void ApplyRoundedTextBox(TextBox textBox, int radius)
        {
            try
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Table page ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Table));
            }
        }

        #endregion

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            clearall();
            Filltable();
        }
        public void Filldetails(int tableid)
        {
            try
            {
                BllSearch<DmnTableinfo> bllSearch = new BllSearch<DmnTableinfo>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblTable_info";
                objSearch.WhereNdGroupBy = "where status in(1,2)and Tid=" + tableid;
                objSearch.OrderBy = "";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (itemSource.Count > 0)
                {
                    txttid.Text = Convert.ToString(tableid);
                    txtdesc.Text = Convert.ToString(itemSource[0].Description);
                    cmb_category.SelectedValue = Convert.ToInt64(itemSource[0].Fcid);
                    txtcapcity.Text = Convert.ToString(itemSource[0].Capcity);
                    txttablecode.Text = Convert.ToString(itemSource[0].TableCode);
                    txtrate.Text = Convert.ToString(itemSource[0].Price);
                    btn_del_table.Visible = true;

                    pbactive_in.Image = null;
                    if (itemSource[0].Status == 2)
                    {
                        pbactive_in.Image = Properties.Resources.ACTIVIATE;
                        pbactive_in.Tag = 2;
                    }
                    else if (itemSource[0].Status == 1)
                    {
                        pbactive_in.Image = Properties.Resources.DEACTIVIATE;
                        pbactive_in.Tag = 1;
                    }

                    pbactive_in.SizeMode = PictureBoxSizeMode.StretchImage;
                    pbactive_in.Visible = true;
                }

            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Table page Filldetails()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Table));
            }
        }
        private void dgv_table_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int tableid = Convert.ToInt32(dgv_table.Rows[e.RowIndex].Cells[tidno].Value);
            Filldetails(tableid);
        }

        private void btn_del_table_Click(object sender, EventArgs e)
        {
            try
            {
                var result = CMessageBox.Show("Do You want Delete Record", "Confirm The Action", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    chb_active.Checked = false;
                    btnsave.PerformClick();
                    Filltable();
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Table page btn_del_table_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Table));
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
                objSearch.WhereNdGroupBy = "where t.status in (1,2)";
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
                        if (itemSource[i].Status == 2)
                        {
                            dgv_table.Rows[i].Cells[tactiveno].Value = Properties.Resources.INACTIVEBUTTON;
                        }
                        else if (itemSource[i].Status == 1)
                        {
                            dgv_table.Rows[i].Cells[tactiveno].Value = Properties.Resources.ACTIVEBUTTON;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Table page Filltable()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Table));
            }

        }

        private void txtcapcity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
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
                            pbactive_in.Image = null;
                            if (itemSource[0].Status == 2)
                            {
                                pbactive_in.Image = Properties.Resources.ACTIVIATE;
                                pbactive_in.Tag = 2;
                            }
                            else if (itemSource[0].Status == 1)
                            {
                                pbactive_in.Image = Properties.Resources.DEACTIVIATE;
                                pbactive_in.Tag = 1;
                            }
                            pbactive_in.SizeMode = PictureBoxSizeMode.StretchImage;
                            pbactive_in.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Table page btnSearch_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Table));
            }

        }

        private void txtrate_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Table page txtrate_KeyPress()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Table));
            }

        }

        private void txttablecode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Tab:
                    Movefocus(1);
                    return true; // Mark the key as handled
                case Keys.Enter:
                    if (dgv_table.Focused)
                    {
                        int tableid = Convert.ToInt32(dgv_table.Rows[dgv_table.CurrentCell.RowIndex].Cells[tidno].Value);
                        Filldetails(tableid);
                    }
                    Movefocus(0);
                    return true; // Mark the key as handled
                case Keys.Escape:
                    FrmDashboard _instancedash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                    _instancedash.Closeform();
                    return true;
                case Keys.End:
                    int lastColumnIndex = tdesno;
                    int lastRowIndex = dgv_table.Rows.Count - 1;
                    dgv_table.CurrentCell = dgv_table[lastColumnIndex, lastRowIndex];
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        public void Movefocus(int keytype)
        {
            try
            {
                if (txttablecode.Focused)
                {
                    txtcapcity.Focus();
                }
                else if (txtcapcity.Focused)
                {
                    cmb_category.Focus();
                }
                else if (cmb_category.Focused)
                {
                    txtdesc.Focus();
                }
                else if (txtdesc.Focused)
                {
                    txtrate.Focus();
                }
                else if (txtrate.Focused)
                {
                    if (btn_del_table.Visible)
                        btn_del_table.Focus();
                    else
                        btnsave.Focus();
                }
                else if (btn_del_table.Focused)
                {
                    if (keytype == 1)
                        btnsave.Focus();
                    else if (keytype == 0)
                        btn_del_table.PerformClick();
                }
                else if (keytype == 1)
                {
                    if (btnsave.Focused)
                        btnSearch.Focus();
                    else if (btnSearch.Focused)
                        btnrefresh.Focus();
                    else if (btnrefresh.Focused)
                        btnclose.Focus();
                    else if (btnclose.Focused)
                    {
                        dgv_table.Focus();
                        dgv_table.CurrentCell = dgv_table[0, 0];
                    }
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
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Table page Movefocus()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Table));
            }

        }

        private void txttablecode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txttablecode.Text != "" && (txttid.Text == "" || Convert.ToInt32(txttid.Text) == 0))
                {
                    BllSearch<DmnTableinfo> bllSearch = new BllSearch<DmnTableinfo>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "*";
                    objSearch.Table = "tblTable_info";
                    objSearch.WhereNdGroupBy = "Where status=1 and TableCode = '" + txttablecode.Text + "' ";// and BranchId = " + branch; ; ;
                    objSearch.OrderBy = "";
                    var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count > 0)
                    {
                        CMessageBox.Show(itemSource[0].TableCode + " Already Exist", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txttablecode.Text = "";
                        txttablecode.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Table page txttablecode_Leave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Table));
            }
        }

        private void FrmTableInfo_Resize(object sender, EventArgs e)
        {
            textb_shape();
        }

        private void pbactive_in_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck_Activeorder())
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
                    dmntableinfo.CreatedBy = FrmDashboard.UserId;
                    dmntableinfo.BranchId = FrmDashboard.BranchId;

                    if (Convert.ToInt32(pbactive_in.Tag) == 2)
                        dmntableinfo.Status = 1;
                    else if (Convert.ToInt32(pbactive_in.Tag) == 1)
                        dmntableinfo.Status = 2;

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
                CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Table page pbactive_in_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Table));
            }
        }
    }
}
