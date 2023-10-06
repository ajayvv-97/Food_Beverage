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
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }
        #region Declare Grid
        public static int slno = 0;
        public static int id = 1;
        public static int fname = 2;
        public static int fcateg = 3;
        public static int fdes = 4;
        public static int fstaus = 5;
        public static int ffcid = 6;
        #endregion

        private void btnclose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void Fillcombo()
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "Fcid,CategoryName";
                objSearch.Table = " tblFoodCategory";
                objSearch.WhereNdGroupBy = "where status=1";
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);
                cmbfcateg.DataSource = gridSource;
                cmbfcateg.ValueMember = "Id";
                cmbfcateg.DisplayMember = "Value";
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

                    DmnMenu dmnmenu = new DmnMenu();
                    BllMenu bllmenu = new BllMenu();
                        if (string.IsNullOrWhiteSpace(txtmid.Text))
                        txtmid.Text = "0";
                    dmnmenu.MId = Convert.ToInt32(txtmid.Text);
                    dmnmenu.Title = txtitemname.Text;
                    dmnmenu.Description = txtdesc.Text;
                    dmnmenu.FcategoryId = Convert.ToInt32(cmbfcateg.SelectedValue);
                    dmnmenu.CreatedBy = 1;
                    dmnmenu.BranchId = 1;
                 
                            if (chb_active.Checked)
                            dmnmenu.Status = 1;
                            else
                            dmnmenu.Status = 0;
                        var usertype = bllmenu.AddData("prdInsert_upd_menu", dmnmenu);

                        if (usertype.MId > 0)
                        {
                            if (Convert.ToUInt32(txtmid.Text) > 0)
                                MessageBox.Show(" Update Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            else
                                MessageBox.Show(" Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            clearall();
                        Fillmenu();
                    }
                    else
                            MessageBox.Show(" Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                
            }
            catch (Exception ex)
            {

            }
        }
        public void Fillmenu()
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
                        dgv_menu.Rows[i].Cells[slno].Value = i + 1;
                        dgv_menu.Rows[i].Cells[id].Value = itemSource[i].Field1;
                        dgv_menu.Rows[i].Cells[fname].Value = itemSource[i].Field2;
                        dgv_menu.Rows[i].Cells[fcateg].Value = itemSource[i].Field3;
                        dgv_menu.Rows[i].Cells[fdes].Value = itemSource[i].Field4;
                        dgv_menu.Rows[i].Cells[fstaus].Value = Convert.ToInt32(itemSource[i].Field5);
                        dgv_menu.Rows[i].Cells[ffcid].Value = itemSource[i].Field6;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        public bool MandatoryCheck()
        {
            if (txtitemname.Text == "" || txtitemname.Text == null)
            {
                MessageBox.Show("Please Enter Food Name", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtitemname.Focus();
                return false;
            }
            else if (Convert.ToInt32(cmbfcateg.SelectedValue) == 0)
            {
                MessageBox.Show("Please Select Food Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbfcateg.Focus();
                return false;
            }
            
            return true;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            clearall();
        }
        public void clearall()
        {
            txtmid.Text = "";
            txtdesc.Text = "";
            txtitemname.Text = "";
            Fillmenu();  
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            dgv_menu.BorderStyle = BorderStyle.None;
            dgv_menu.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv_menu.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv_menu.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv_menu.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv_menu.BackgroundColor = Color.White;

            dgv_menu.EnableHeadersVisualStyles = false;
            dgv_menu.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv_menu.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv_menu.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            clearall();
            Fillcombo();
        }

        private void dgv_menu_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void tlp_registration_Paint(object sender, PaintEventArgs e)
        {
            float borderRadius = 10f; // adjust this value to change the roundness of the corners
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(tlp_registration.ClientRectangle.X, tlp_registration.ClientRectangle.Y, tlp_registration.ClientRectangle.Width, tlp_registration.ClientRectangle.Height);
            path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
            path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
            path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
            path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
            path.CloseFigure();
            tlp_registration.Region = new Region(path);
        }
    }
}
