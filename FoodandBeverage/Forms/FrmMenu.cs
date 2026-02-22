
using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Forms.UserControls;
using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
using FoodandBeverage.Interfaces;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace FoodandBeverage.Forms
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }
        #region Declaration
        #region Declare Grid Column Indexes

        private int slno = 0;
        private int pmidno = 1;
        private int piidno = 2;
        private int pnameno = 3;
        private int pcatno = 4;
        private int ppriceno = 5;
        private int pqtyno = 6;
        private int pdefstock = 7;
        private int pstatusno = 8;
        private int pfcidno = 9;
        private int ppdtype = 10;
        private int proptype = 11;
        private int proptypename = 12;
        private int poldqtyno = 13;
        private int pactiveinactiveno = 14;

        #endregion

        private int _oldQty = 0;
        public string Compressedimg = Application.StartupPath + "\\CompressedImg";
        ClsDocument clsDocument = new ClsDocument();
        ClsGeneral clsgen = new ClsGeneral();
        public static string empImgExt = "";
        TableLayoutPanel tlp_Gallery_Main = new TableLayoutPanel();
        TableLayoutPanel tlp_Gallery_Box = new TableLayoutPanel();
        TableLayoutPanel tlp_gallery_sub = new TableLayoutPanel();
        ToolTip toolTip_gallery = new ToolTip();
        private bool comboBoxChanged = false;
        private Color originalBackColor; // Variable to store the original background color

        int Pagecount = 20;//Convert.ToInt32(ConfigurationManager.AppSettings["POSpagescount"]);
        int currentPage = 1;
        int pageSize = 0;
        int totalPages = 0;

        private List<DmnGridFields> productData;
        private List<DmnGridFields> pagedatadata;
        private MenuFillType currentMenuType;
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
                objSearch.Table = " tblCategory_FTI";
                objSearch.WhereNdGroupBy = "where Status=1 and CategoryType=0";
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdComboFill", objSearch);

                gridSource.Add(new DmnComboBox
                {
                    Id = -2,
                    Value = "Combo Product"
                });

                cmbMenu_category.DataSource = gridSource;
                cmbMenu_category.ValueMember = "Id";
                cmbMenu_category.DisplayMember = "Value";

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page  Fillcombo()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }
        private void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MandatoryCheck())
                {
                    DmnMenu dmnmenu = new DmnMenu();
                    DmnMenu usertype = new DmnMenu();

                    BllMenu bllmenu = new BllMenu();
                    int count = 0;

                    if (Convert.ToInt32(cmbMenu_category.SelectedValue) == -2)//Combo product
                    {
                        for (int i = 0; i < dgv_menu.RowCount; i++)
                        {
                            if (((dgv_menu.Rows[i].Cells[piidno].Value) != null && (dgv_menu.Rows[i].Cells[piidno].Value) != "") && (Convert.ToInt32(dgv_menu.Rows[i].Cells[pmidno].Value) > 0 || (Convert.ToInt32(dgv_menu.Rows[i].Cells[pmidno].Value) == 0 && Convert.ToBoolean(dgv_menu.Rows[i].Cells[pstatusno].Value) == true)))  //&& Convert.ToBoolean(dgv_menu.Rows[i].Cells[pstatusno].Value) == true&&(Convert.ToBoolean(dgv_menu.Rows[i].Cells[pstatusno].Value) == true)
                            {
                                dmnmenu.MId = Convert.ToInt32(dgv_menu.Rows[i].Cells[pmidno].Value);
                                dmnmenu.ProductId = Convert.ToInt32(dgv_menu.Rows[i].Cells[piidno].Value);
                                dmnmenu.Name = Convert.ToString(dgv_menu.Rows[i].Cells[pnameno].Value);
                                dmnmenu.Fcid = Convert.ToInt32(dgv_menu.Rows[i].Cells[pfcidno].Value);
                                dmnmenu.Quantity = Convert.ToDecimal(dgv_menu.Rows[i].Cells[pqtyno].Value);

                                if (Convert.ToInt32(dgv_menu.Rows[i].Cells[poldqtyno].Value) == 0)
                                    dmnmenu.ResetStatus = 1;
                                else
                                    dmnmenu.ResetStatus = 0;

                                dmnmenu.ProductType = Convert.ToInt32(dgv_menu.Rows[i].Cells[ppdtype].Value);
                                dmnmenu.CreatedBy = FrmDashboard.UserId;
                                dmnmenu.BranchId = FrmDashboard.BranchId;
                                if (Convert.ToBoolean(dgv_menu.Rows[i].Cells[pstatusno].Value) == true)
                                    dmnmenu.Status = 1;
                                else
                                    dmnmenu.Status = 0;
                                dmnmenu.defaultqty = Convert.ToInt32(dgv_menu.Rows[i].Cells[pdefstock].Value);
                                usertype = bllmenu.AddData("prdInsert_upd_menu", dmnmenu);
                                count++;
                            }
                        }
                        if (count > 0)
                        {
                            CMessageBox.Show(" Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            clearall();
                            ComboProduct_Fill();
                        }
                    }
                    else if (rb_item.Checked)
                    {
                        for (int i = 0; i < dgv_menu.RowCount; i++)
                        {
                            if (((dgv_menu.Rows[i].Cells[piidno].Value) != null && (dgv_menu.Rows[i].Cells[piidno].Value) != ""))  //&& Convert.ToBoolean(dgv_menu.Rows[i].Cells[pstatusno].Value) == true&&(Convert.ToBoolean(dgv_menu.Rows[i].Cells[pstatusno].Value) == true)
                            {
                                dmnmenu.MId = Convert.ToInt32(dgv_menu.Rows[i].Cells[pmidno].Value);
                                dmnmenu.ProductId = Convert.ToInt32(dgv_menu.Rows[i].Cells[piidno].Value);
                                dmnmenu.Name = Convert.ToString(dgv_menu.Rows[i].Cells[pnameno].Value);
                                dmnmenu.Fcid = Convert.ToInt32(dgv_menu.Rows[i].Cells[pfcidno].Value);
                                dmnmenu.Quantity = Convert.ToDecimal(dgv_menu.Rows[i].Cells[pqtyno].Value);
                                dmnmenu.CreatedBy = FrmDashboard.UserId;
                                dmnmenu.BranchId = FrmDashboard.BranchId;

                                usertype = bllmenu.AddData("prdInsert_upd_menu", dmnmenu);
                                count++;
                            }
                        }
                        if (count == dgv_menu.RowCount)
                        {
                            CMessageBox.Show(" Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);                         
                            cmbMenu_category.SelectedIndex = -1;
                            txtsearchlist.Text = "";
                            FillMenu(MenuFillType.Item);
                        }
                    }
                    else if (rb_product.Checked)
                    {
                        for (int i = 0; i < dgv_menu.RowCount; i++)
                        {
                            if (dgv_menu.Rows[i].Cells[pnameno].Value != "F8 for Products")  //(dgv_menu.Rows[i].Cells[pnameno].Value) != null && (dgv_menu.Rows[i].Cells[piidno].Value) != "") && (Convert.ToInt32(dgv_menu.Rows[i].Cells[pmidno].Value) > 0 || (Convert.ToInt32(dgv_menu.Rows[i].Cells[pmidno].Value) == 0 && Convert.ToBoolean(dgv_menu.Rows[i].Cells[pstatusno].Value) == true))&& Convert.ToBoolean(dgv_menu.Rows[i].Cells[pstatusno].Value) == true&&(Convert.ToBoolean(dgv_menu.Rows[i].Cells[pstatusno].Value) == true)
                            {
                                dmnmenu.MId = Convert.ToInt32(dgv_menu.Rows[i].Cells[pmidno].Value);
                                dmnmenu.ProductId = Convert.ToInt32(dgv_menu.Rows[i].Cells[piidno].Value);
                                dmnmenu.Name = Convert.ToString(dgv_menu.Rows[i].Cells[pnameno].Value);
                                dmnmenu.Fcid = Convert.ToInt32(dgv_menu.Rows[i].Cells[pfcidno].Value);
                                dmnmenu.Quantity = Convert.ToDecimal(dgv_menu.Rows[i].Cells[pqtyno].Value);

                                if (Convert.ToInt32(dgv_menu.Rows[i].Cells[poldqtyno].Value) == 0)
                                    dmnmenu.ResetStatus = 1;
                                else
                                    dmnmenu.ResetStatus = 0;

                                dmnmenu.PreparationMode = Convert.ToInt32(dgv_menu.Rows[i].Cells[proptype].Value);
                                dmnmenu.CreatedBy = FrmDashboard.UserId;
                                dmnmenu.BranchId = FrmDashboard.BranchId;
                                if (Convert.ToBoolean(dgv_menu.Rows[i].Cells[pstatusno].Value) == true)
                                    dmnmenu.Status = 1;
                                else
                                    dmnmenu.Status = 0;
                            dmnmenu.defaultqty = Convert.ToInt32(dgv_menu.Rows[i].Cells[pdefstock].Value);

                            usertype = bllmenu.AddData("prdInsert_upd_menu", dmnmenu);
                                count++;
                            }
                        }
                        if (count > 0)
                        {
                            CMessageBox.Show(" Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            clearall();
                            FillMenu(MenuFillType.Product);
                        }
                    }
                }
                //else
                //CMessageBox.Show(" Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page  btnsave_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }
        private void chk_selectall_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (chk_selectall != null)
                {
                    dgv_menu.EndEdit();
                    foreach (DataGridViewRow row in dgv_menu.Rows)
                    {
                        DataGridViewCheckBoxCell checkBox = (row.Cells[pstatusno] as DataGridViewCheckBoxCell);
                        checkBox.Value = chk_selectall.Checked;
                    }
                }
                if (dgv_menu.RowCount > 0)
                    dgv_menu.CurrentCell = dgv_menu.Rows[0].Cells[0];
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page  chk_selectall_Clicked()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }

        public bool MandatoryCheck()
        {
            try
            {
                // Uncommented code for handling specific fields
                // if (txtitemname.Text == "" || txtitemname.Text == null)
                // {
                //     CMessageBox.Show("Please Enter Food Name", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //     txtitemname.Focus();
                //     return false;
                // }
                // else if (Convert.ToInt32(cmbMenu_category.SelectedValue) == 0)
                // {
                //     CMessageBox.Show("Please Select Food Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //     cmbMenu_category.Focus();
                //     return false;
                // }

                // Check if any item in dgv_menu has a positive value in the piidno column
                bool flagcount = true;
                for (int i = 0; i < dgv_menu.RowCount; i++)
                {
                    if (Convert.ToInt32(dgv_menu.Rows[i].Cells[pstatusno].Value) > 0)
                    {
                        flagcount = false;
                        break;
                    }
                }

                // If no item is selected, show a message and return false
                if (flagcount)
                {
                    CMessageBox.Show("Please Select Food To The List", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {

                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page MandatoryCheck()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
                return false;
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

                // ===================== SEARCH POPUP =====================
                if (rb_product.Checked)
                {
                    _objSearch.ColumnHeaders = "ID,ItemCode,Product,Price,Category Name";
                    _objSearch.Select = " DISTINCT p.ProductId as Id,ItemCode as Field1,Product as Field2,Price as Field3,c.CategoryName as Field4";
                    _objSearch.Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON C.Fcid=p.Fcid";
                    _objSearch.WhereNdGroupBy = "where p.Status=1";
                    _objSearch.OrderBy = " ORDER BY p.Product";
                }
                else if (rb_item.Checked)
                {
                    _objSearch.ColumnHeaders = "ID,ItemCode,Product,Price,Category Name,Quantity";
                    _objSearch.Select =
                        " DISTINCT i.ItemId AS ID,i.ItemCode_UPC as Field1,i.Itemname AS Field2," +
                        "(SELECT TOP(1) Srate FROM tblProductRate WHERE ProductId = i.ItemId ORDER BY Id DESC) AS Field3," +
                        "C.CategoryName AS Field4," +
                        "(SELECT SUM(QOH) FROM tblProductRate WHERE ProductId = i.ItemId) AS Field5";

                    _objSearch.Table = "tblItem i LEFT JOIN tblCategory_FTI c ON C.Fcid = i.Fcid";
                    _objSearch.WhereNdGroupBy = "Where i.Saletype=1";
                    _objSearch.OrderBy = "order by i.Itemname";
                }
                else
                {
                    CMessageBox.Show("Please select Product or Item", "Info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                var gridSource = bllSearch.GetSearchData("prdSearch", _objSearch);

                if (gridSource.Count == 0)
                {
                    CMessageBox.Show("No Records", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var search = new FrmSearch<DmnGridFields>(_objSearch,
                    SearchMode.FirstCharacter,
                    this.Width - 150,
                    this.Height - 150,
                    this.Location.Y + 50,
                    this.Location.X + 20,
                    dtColumnAlign))
                {
                    if (search.ShowDialog() == DialogResult.OK)
                    {
                        returnValue = Convert.ToInt32(search._retValue);
                    }
                }

                if (returnValue <= 0) return;

                // 🔥 CLEAR OLD DATA (IMPORTANT)
                productData = new List<DmnGridFields>();
                pagedatadata = new List<DmnGridFields>();

                // ===================== PRODUCT =====================
                if (rb_product.Checked)
                {
                    currentMenuType = MenuFillType.Product;

                    BllSearch<DmnGridFields> bllSearch_product = new BllSearch<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();

                    objSearch.Select =
                        "p.ProductId AS Field1, p.Product AS Field2, C.CategoryName AS Field3," +
                        "p.Price AS Field4, P.Fcid AS Field5," +
                        "m.Status AS Field6, m.Mid AS Field7, m.Quantity AS Field8," +
                        "p.PreparationMode AS Field9, m.DefaultStock AS Field10";

                    objSearch.Table =
                        "tblProduct p " +
                        "LEFT JOIN tblCategory_FTI c ON C.Fcid = P.Fcid " +
                        "LEFT JOIN tblMenu m ON m.ProductId = p.ProductId AND m.producttype = 0";

                    objSearch.WhereNdGroupBy =
                        "WHERE p.status=1 AND p.productid=" + returnValue +
                        " GROUP BY p.ProductId, p.Product, C.CategoryName, p.Price, P.Fcid," +
                        " m.Status, m.Mid, m.Quantity, p.PreparationMode, m.DefaultStock";

                    objSearch.OrderBy = "ORDER BY p.Product";

                    productData = bllSearch_product.GetSearchData("prdSearch", objSearch);

                    if (productData.Count == 0)
                    {
                        CMessageBox.Show("Product not found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // ===================== ITEM =====================
                else if (rb_item.Checked)
                {
                    currentMenuType = MenuFillType.Item;

                    BllSearch<DmnGridFields> bllSearch_item = new BllSearch<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();

                    objSearch.Select =
                        "i.ItemId AS Field1, i.Itemname AS Field2, C.CategoryName AS Field3," +
                        "(SELECT TOP(1) Srate FROM tblProductRate WHERE ProductId = i.ItemId ORDER BY Id DESC) AS Field4," +
                        "i.Fcid AS Field5," +
                        "(SELECT SUM(QOH) FROM tblProductRate WHERE ProductId = i.ItemId) AS Field6," +
                        "i.status AS Field7";

                    objSearch.Table = "tblItem i LEFT JOIN tblCategory_FTI c ON C.Fcid = i.Fcid";
                    objSearch.WhereNdGroupBy = "WHERE i.Saletype=1 AND i.itemid=" + returnValue;
                    objSearch.OrderBy = "ORDER BY i.Itemname";

                    productData = bllSearch_item.GetSearchData("prdSearch", objSearch);

                    if (productData.Count == 0)
                    {
                        CMessageBox.Show("Item not found", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // ===================== PAGING =====================
                currentPage = 1;
                totalPages = (int)Math.Ceiling((double)productData.Count / pageSize);

                LoadPagedData(); // 🔥 SINGLE ENTRY POINT (NO DUPLICATE)
            }
            catch (Exception ex)
            {
                clsgen.LogException("Menu page btnSearch_Click()", ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Menu));
            }
        }
        private void btnrefresh_Click(object sender, EventArgs e)
        {
            try
            {
                clearall();

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page btnrefresh_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }

        public void clearall()
        {
            try
            {
                cmbMenu_category.SelectedIndex = -1;
                txtsearchlist.Text = "";

                txtsearchlist.Text = "Food Name .....";
                txtsearchlist.ForeColor = Color.LightGray;

                if (rb_product.Checked)
                    FillMenu(MenuFillType.Product);
                else if (rb_item.Checked)
                    FillMenu(MenuFillType.Item);

                if(!rb_item.Checked && !rb_item.Checked)
                {
                    FillMenu(MenuFillType.Product);
                }

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page ClearAll()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }
        private void FrmMenu_Load(object sender, EventArgs e)
        {
            try
            {
                if (FrmDashboard.AutoLogoutWhenIdle)
                {
                    AppSession.RegisterForm(this, (int)LogoutFormList.operation);
                }

                if (System.Windows.Forms.Application.OpenForms["FrmDashboard"] != null)
                {
                    FrmDashboard _instanceFrmdash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                    if (_instanceFrmdash.ChkWritePermission(Convert.ToInt32(formsenum.Kitchen_View)))
                        dgv_menu.Enabled = true;
                    else
                        dgv_menu.Enabled = false;
                }

                cmbMenu_category.Focus();
                //Fillmenu(0);
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

             
                Fillcombo();
                textb_shape();

                FillMenu(MenuFillType.Product);

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("FrmMenu_Load", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }


        private void btncolse_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnmini_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


       
        private void txtsearchlist_Leave(object sender, EventArgs e)
        {
            if (txtsearchlist.Text == "")
            {
                txtsearchlist.Text = "Food Name .....";
                txtsearchlist.ForeColor = Color.LightGray;
            }
        }

        private void txtsearchlist_Enter(object sender, EventArgs e)
        {
            if (txtsearchlist.Text == "Food Name .....")
            {
                txtsearchlist.Text = "";
                txtsearchlist.ForeColor = Color.Black;
            }
        }


        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtsearchlist })
                {
                    ApplyRoundedTextBox(textBox, 10);
                    textBox.Font = new Font(textBox.Font.FontFamily, 12);
                    textBox.BorderStyle = BorderStyle.None;
                }

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page textb_shape()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
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
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page ApplyRoundedTextBox()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }

        #endregion

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
                    return true;
                case Keys.End:
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        public void Movefocus(int keytype)
        {

            try
            {
                
                   if (txtsearchlist.Focused)
                    {
                        btnseach_menu.Focus();
                    }

                    else if (btnseach_menu.Focused)
                    {
                        if (keytype == 1)
                        {
                            dgv_menu.Focus();
                            dgv_menu.CurrentCell = dgv_menu[0, 0];
                        }
                        else
                            btnseach_menu.PerformClick();
                    }

                    else if (dgv_menu.Focused)
                    {
                        int currentRowIndex = dgv_menu.CurrentCell.RowIndex;
                        if (keytype == 1)
                        {
                            int currentColumnIndex = 4;
                            if (currentColumnIndex == 4 && currentRowIndex == dgv_menu.RowCount - 1)
                                btnsave.Focus();
                        }
                        else
                        {
                            DataGridViewCell currentCell = dgv_menu.CurrentCell;
                            if (dgv_menu.CurrentCell.ColumnIndex == pstatusno)
                            {
                                DataGridViewCheckBoxCell checkboxCell = currentCell as DataGridViewCheckBoxCell;
                                checkboxCell.Value = !(bool)checkboxCell.Value;
                                dgv_menu.CurrentCell = dgv_menu.Rows[currentRowIndex].Cells[ppriceno];
                                dgv_menu.CurrentCell = dgv_menu.Rows[currentRowIndex].Cells[pstatusno];
                            }
                        }

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
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page Movefocus()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }

        public void FillMenu(MenuFillType type)
        {
            try
            {
                currentMenuType = type; // 🔥 store globally

                currentPage = 1;
                pageSize = Pagecount;
                totalPages = 0;

                if (productData != null) productData.Clear();
                if (pagedatadata != null) pagedatadata.Clear();

                int selectedCategoryId = Convert.ToInt32(cmbMenu_category.SelectedValue);
                string searchText = txtsearchlist.Text.Trim().Replace("'", "''");

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();

                string where = "";

                if (type == MenuFillType.Item)
                {
                    objSearch.Select =
                        "i.ItemId AS Field1, i.Itemname AS Field2, C.CategoryName AS Field3," +
                        "(SELECT TOP(1) Srate FROM tblProductRate WHERE ProductId = i.ItemId ORDER BY Id DESC) AS Field4," +
                        "i.Fcid AS Field5," +
                        "(SELECT SUM(QOH) FROM tblProductRate WHERE ProductId = i.ItemId) AS Field6," +
                        "i.status AS Field7";

                    objSearch.Table = "tblItem i LEFT JOIN tblCategory_FTI c ON C.Fcid = i.Fcid";

                    where = "WHERE i.Saletype=1";

                    if (selectedCategoryId > 0)
                        where += " AND i.Fcid = " + selectedCategoryId;

                    if (!string.IsNullOrEmpty(searchText))
                        where += " AND (i.Itemname LIKE '%" + searchText + "%' " +
                                 "OR C.CategoryName LIKE '%" + searchText + "%')";
                }
                else
                {
                    objSearch.Select =
                        "p.ProductId AS Field1, p.Product AS Field2, C.CategoryName AS Field3," +
                        "p.Price AS Field4, P.Fcid AS Field5," +
                        "m.Status AS Field6, m.Mid AS Field7, m.Quantity AS Field8," +
                        "p.PreparationMode AS Field9, m.DefaultStock AS Field10";

                    objSearch.Table =
                        "tblProduct p " +
                        "LEFT JOIN tblCategory_FTI c ON C.Fcid = P.Fcid " +
                        "LEFT JOIN tblMenu m ON m.ProductId = p.ProductId AND m.producttype = 0";

                    where = "WHERE p.status=1 AND m.status=1";

                    if (selectedCategoryId == -2)
                        where += " AND p.ProductType = 1";
                    else if (selectedCategoryId > 0)
                        where += " AND p.Fcid = " + selectedCategoryId;

                    if (!string.IsNullOrEmpty(searchText))
                        where += " AND (p.Product LIKE '%" + searchText + "%' " +
                                 "OR C.CategoryName LIKE '%" + searchText + "%')";

                    objSearch.WhereNdGroupBy = where +
                        " GROUP BY p.ProductId, p.Product, C.CategoryName, p.Price, P.Fcid," +
                        " m.Status, m.Mid, m.Quantity, p.PreparationMode, m.DefaultStock";

                    objSearch.OrderBy = "ORDER BY p.Product";

                    goto EXECUTE;
                }

                objSearch.WhereNdGroupBy = where;
                objSearch.OrderBy = "ORDER BY Field2";

            EXECUTE:

                productData = bllSearch.GetSearchData("prdSearch", objSearch);

                totalPages = (int)Math.Ceiling((double)productData.Count / pageSize);

                LoadPagedData();
            }
            catch (Exception ex)
            {
                clsgen.LogException("Menu Fill()", ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.Menu));
            }
        }

        private void LoadPagedData()
        {
            try
            {
                if (productData == null || productData.Count == 0)
                {
                    lblpagerinfo.TextAlign = ContentAlignment.MiddleCenter;
                    return;
                }

                lblpagerinfo.TextAlign = ContentAlignment.MiddleRight;

                pagedatadata = productData
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {productData.Count}";

                btn_previous.Enabled = currentPage > 1;
                btn_next.Enabled = currentPage < totalPages;

                var itemSource = pagedatadata;

                dgv_menu.Rows.Clear();

                if (itemSource.Count > 0)
                {
                    dgv_menu.Rows.Add(currentMenuType == MenuFillType.Product ? itemSource.Count + 1 : itemSource.Count);

                    int startIndex = (currentPage - 1) * pageSize;

                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_menu.Rows[i].Cells[slno].Value = startIndex + i + 1;
                        dgv_menu.Rows[i].Cells[piidno].Value = itemSource[i].Field1;
                        dgv_menu.Rows[i].Cells[pnameno].Value = itemSource[i].Field2;
                        dgv_menu.Rows[i].Cells[pcatno].Value = itemSource[i].Field3;
                        dgv_menu.Rows[i].Cells[ppriceno].Value = itemSource[i].Field4;
                        dgv_menu.Rows[i].Cells[pfcidno].Value = itemSource[i].Field5;

                        if (currentMenuType == MenuFillType.Item)
                        {
                            dgv_menu.Rows[i].Cells[pqtyno].Value = itemSource[i].Field6;

                            dgv_menu.Columns[pstatusno].Visible = false;
                            chk_selectall.Visible = false;

                            bool isActive = Convert.ToInt32(itemSource[i].Field7) == 1;
                            SetActionButton(i, isActive);
                        }
                        else
                        {
                            dgv_menu.Rows[i].Cells[pmidno].Value = itemSource[i].Field7;
                            dgv_menu.Rows[i].Cells[pqtyno].Value = itemSource[i].Field8;

                            dgv_menu.Columns[pstatusno].Visible = true;
                            chk_selectall.Visible = true;

                            bool status = Convert.ToInt32(itemSource[i].Field6) == 1;
                            dgv_menu.Rows[i].Cells[pstatusno].Value = status;

                            SetActionButton(i, status);
                        }
                    }

                    SetHeaderCheckbox();
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("LoadPagedData()", ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(formsenum.Menu));
            }
        }

        private void btn_previous_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPage > 1)
                {
                    currentPage--;
                    LoadPagedData();
                    lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {productData?.Count ?? 0}";
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Profile page btn_previous_inout_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Menu));
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentPage < totalPages)
                {
                    currentPage++;
                    LoadPagedData();
                    lblpagerinfo.Text = $"Page {currentPage} of {totalPages} | Total Records: {productData?.Count ?? 0}";
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("Profile page  btn_next_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(formsenum.Menu));
            }
        }


        private void SetActionButton(int rowIndex, bool isActive)
        {
            var cell = dgv_menu.Rows[rowIndex].Cells[pactiveinactiveno];

            if (isActive)
            {
                cell.Value = "Disable";
                cell.Style.BackColor = Color.Red;
                cell.Style.ForeColor = Color.White;
            }
            else
            {
                cell.Value = "Activate";
                cell.Style.BackColor = Color.Green;
                cell.Style.ForeColor = Color.White;
            }
        }
        private void SetHeaderCheckbox()
        {
            Rectangle rect = dgv_menu.GetColumnDisplayRectangle(pstatusno, true);
            int x = rect.X + (rect.Width - chk_selectall.Width) / 2;
            Point pt = dgv_menu.GetCellDisplayRectangle(pstatusno, -1, true).Location;

            chk_selectall.Location = new Point(x, pt.Y + 40);
            chk_selectall.Size = new Size(18, 18);
            chk_selectall.BackColor = Color.White;

            chk_selectall.Click -= chk_selectall_Clicked;
            chk_selectall.Click += chk_selectall_Clicked;

            if (!dgv_menu.Controls.Contains(chk_selectall))
                dgv_menu.Controls.Add(chk_selectall);
        }
        private void rb_product_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_product.Checked)
            {
                dgv_menu.Columns[pactiveinactiveno].Visible = true;
                dgv_menu.Columns[pdefstock].Visible = true;

                cmbMenu_category.SelectedIndex = -1;
                FillMenu(MenuFillType.Product);
                txtsearchlist.Text = "";
            }
        }
        private void rb_item_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_item.Checked)
            {
                dgv_menu.Columns[pdefstock].Visible = false;

                cmbMenu_category.SelectedIndex = -1;
                FillMenu(MenuFillType.Item);
                txtsearchlist.Text = "";
            }
        }


        public void ComboProduct_Fill()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();

                dmnSearch.Select = " omid Field1,OfferName Field2,'Combo Product' Field3,finalprice Field4,m.Status AS Field6,m.Mid AS Field7,m.Quantity AS Field8,3 Field9 ";
                dmnSearch.Table = "tblOfferMaster om LEFT JOIN tblMenu m ON m.ProductId=om.omid and m.producttype = 3 ";
                dmnSearch.WhereNdGroupBy = "where om.status=1  and OfferType=1  AND OfferStartDatetime <= GETDATE()  AND OfferEndDatetime >= GETDATE()";
                dmnSearch.OrderBy = "order by OfferName";

                var itemSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

                dgv_menu.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_menu.Rows.Add(itemSource.Count + 1);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_menu.Rows[i].Cells[slno].Value = i + 1;
                        dgv_menu.Rows[i].Cells[piidno].Value = itemSource[i].Field1;
                        dgv_menu.Rows[i].Cells[pnameno].Value = itemSource[i].Field2;
                        dgv_menu.Rows[i].Cells[pcatno].Value = itemSource[i].Field3;
                        dgv_menu.Rows[i].Cells[ppriceno].Value = itemSource[i].Field4;
                        dgv_menu.Rows[i].Cells[pfcidno].Value = itemSource[i].Field5;
                        dgv_menu.Rows[i].Cells[pmidno].Value = itemSource[i].Field7;
                        dgv_menu.Rows[i].Cells[pqtyno].Value = itemSource[i].Field8;
                        dgv_menu.Rows[i].Cells[pqtyno].Style.BackColor = Color.LightGray;
                        dgv_menu.Columns[pstatusno].Visible = true;

                        chk_selectall.Visible = true;
                    

                        bool currentStatus = Convert.ToInt32(itemSource[i].Field6) == 1;
                        dgv_menu.Rows[i].Cells[pstatusno].Value = currentStatus;
                        DataGridViewCell actionCell = dgv_menu.Rows[i].Cells[pactiveinactiveno];
                        actionCell.Value = currentStatus ? "Disable" : "Activate";
                        if (currentStatus)
                        {
                            // Currently active: show "Disable" in red
                            actionCell.Style.BackColor = Color.Red;
                            actionCell.Style.ForeColor = Color.White;
                            actionCell.Style.SelectionBackColor = Color.DarkRed;
                        }
                        else
                        {
                            // Currently inactive: show "Activate" in green
                            actionCell.Style.BackColor = Color.Green;
                            actionCell.Style.ForeColor = Color.White;
                            actionCell.Style.SelectionBackColor = Color.DarkGreen;
                        }
                    }


                    Rectangle headerCellRect = dgv_menu.GetColumnDisplayRectangle(pstatusno, true);
                    int checkBoxX = headerCellRect.X + (headerCellRect.Width - chk_selectall.Width) / 2;
                    Point headerCellLocation = this.dgv_menu.GetCellDisplayRectangle(pstatusno, -1, true).Location;
                    chk_selectall.Location = new Point(checkBoxX, headerCellLocation.Y + 40);
                    chk_selectall.BackColor = Color.White;
                    chk_selectall.Size = new Size(18, 18);
                    chk_selectall.Click += new EventHandler(chk_selectall_Clicked);
                    dgv_menu.Controls.Add(chk_selectall);


                }

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page ComboProduct_Fill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }

        private void dgv_menu_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            TextBox tb = e.Control as TextBox;
            if (tb != null)
            {
                tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
            }
        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
            // only allow one decimal point   
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }
    

        private void dgv_menu_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyData == Keys.Delete)
                {
                    if (Convert.ToInt32(dgv_menu.Rows[dgv_menu.CurrentRow.Index].Cells[pmidno].Value) > 0)
                    {
                        var result = CMessageBox.Show("Are you sure you want to remove this product from the menu?", "Confirm Remove", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            dgv_menu.Rows[dgv_menu.CurrentRow.Index].Cells[pstatusno].Value = false;
                        }
                    }
                    else
                    {
                        dgv_menu.Rows.RemoveAt(dgv_menu.CurrentRow.Index);
                        int n = dgv_menu.RowCount;
                        int count = 0;
                        if (n > 0)
                        {
                            for (int i = 0; i < n; i++)
                                dgv_menu.Rows[i].Cells[slno].Value = i + 1;
                            dgv_menu.Rows.Add(1);

                            n = dgv_menu.RowCount;
                            for (int i = 0; i < n; i++)
                            {
                                if (dgv_menu.Rows[i].Cells[piidno].Value == null)
                                {
                                    count = count + 1;
                                }
                            }
                            if (count > 1)
                            {
                                for (int i = 0; i < n; i++)
                                {
                                    if (dgv_menu.Rows[i].Cells[piidno].Value == null)
                                    {
                                        dgv_menu.Rows.RemoveAt(i);
                                        break;
                                    }
                                }
                            }

                        }

                        if (n == 0)
                        {
                            dgv_menu.Rows.Add(1);
                            dgv_menu.Rows.Add();
                            int r = dgv_menu.RowCount - 1;
                            dgv_menu.Rows[r].Cells[pnameno].Value = "F8 for Products";
                            dgv_menu.Rows[r].Cells[pnameno].Style.ForeColor = Color.Red;
                        }
                    }


                }

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page dgv_menu_KeyDown()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }




        private void FrmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FrmDashboard.AutoLogoutWhenIdle)
            {
                AppSession.UnregisterForm(this);
            }
        }

        private void FrmMenu_Resize(object sender, EventArgs e)
        {
            #region setting Corner Round 
            UIHelper.RoundCorners(dgv_menu, 25, null);
            UIHelper.RoundCorners(tlp_navbar, 10, null);
            #endregion

        }

        private void dgv_menu_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == pqtyno)
            {
                var value = dgv_menu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                _oldQty = value != null && int.TryParse(value.ToString(), out int qty)
                            ? qty
                            : 0;
            }
        }
        private void dgv_menu_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == pqtyno)
            {
                dgv_menu.Rows[e.RowIndex].Cells[poldqtyno].Value = _oldQty;
            }
        }

        private void dgv_menu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                    return;

                if (e.ColumnIndex != pactiveinactiveno)
                    return;

                var piidValue = dgv_menu.Rows[e.RowIndex].Cells[piidno].Value;
                if (piidValue == null || piidValue == DBNull.Value)
                {
                    return;
                }

                DataGridViewRow row = dgv_menu.Rows[e.RowIndex];

                int mid = Convert.ToInt32(row.Cells[pmidno].Value);
                bool currentStatus = Convert.ToBoolean(row.Cells[pstatusno].Value);

                string actionText = currentStatus ? "Disable" : "Activate";

                DialogResult dr = MessageBox.Show(
                    $"Are you sure you want to {actionText} this menu item?",
                    "Confirm",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (dr != DialogResult.Yes)
                    return;
                if (dgv_menu.Columns[pdefstock].Visible)
                {
                    DmnMenu dmnmenu = new DmnMenu();
                    BllMenu bllmenu = new BllMenu();
                    dmnmenu.MId = mid;
                    dmnmenu.ProductId = Convert.ToInt32(row.Cells[piidno].Value);
                    dmnmenu.CreatedBy = FrmDashboard.UserId;
                    dmnmenu.BranchId = FrmDashboard.BranchId;
                    dmnmenu.Status = currentStatus ? 0 : 1;
                    bllmenu.SetMenuActiveStatus("prdUpdate_MenuStatus", dmnmenu);
                    row.Cells[pstatusno].Value = !currentStatus;
                    row.Cells[pactiveinactiveno].Value = !currentStatus ? "Disable" : "Activate";
                    MessageBox.Show("Menu item successfully " + row.Cells[pactiveinactiveno].Value, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (currentStatus)
                    {
                        // Currently active: show "Disable" in red
                        row.Cells[pactiveinactiveno].Style.BackColor = Color.Red;
                        row.Cells[pactiveinactiveno].Style.ForeColor = Color.White;
                        row.Cells[pactiveinactiveno].Style.SelectionBackColor = Color.DarkRed;
                    }
                    else
                    {
                        // Currently inactive: show "Activate" in green
                        row.Cells[pactiveinactiveno].Style.BackColor = Color.Green;
                        row.Cells[pactiveinactiveno].Style.ForeColor = Color.White;
                        row.Cells[pactiveinactiveno].Style.SelectionBackColor = Color.DarkGreen;
                    }
                }
                else
                {
                    DmnMenu dmnmenu = new DmnMenu();
                    BllMenu bllmenu = new BllMenu();
                    dmnmenu.MId = Convert.ToInt32(row.Cells[piidno].Value);
                    dmnmenu.ProductId = Convert.ToInt32(row.Cells[piidno].Value);
                    dmnmenu.CreatedBy = FrmDashboard.UserId;
                    dmnmenu.ProductType = 1;
                    dmnmenu.BranchId = FrmDashboard.BranchId;
                    dmnmenu.Status = currentStatus ? 0 : 1;
                    bllmenu.SetMenuActiveStatus("prdUpdate_MenuStatus", dmnmenu);
                    row.Cells[pstatusno].Value = !currentStatus;
                    row.Cells[pactiveinactiveno].Value = !currentStatus ? "Disable" : "Activate";
                    MessageBox.Show("Menu item successfully " + row.Cells[pactiveinactiveno].Value, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (currentStatus)
                    {
                        // Currently active: show "Disable" in red
                        row.Cells[pactiveinactiveno].Style.BackColor = Color.Red;
                        row.Cells[pactiveinactiveno].Style.ForeColor = Color.White;
                        row.Cells[pactiveinactiveno].Style.SelectionBackColor = Color.DarkRed;
                    }
                    else
                    {
                        // Currently inactive: show "Activate" in green
                        row.Cells[pactiveinactiveno].Style.BackColor = Color.Green;
                        row.Cells[pactiveinactiveno].Style.ForeColor = Color.White;
                        row.Cells[pactiveinactiveno].Style.SelectionBackColor = Color.DarkGreen;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }


        private void cmbMenu_category_SelectionChangeCommitted(object sender, EventArgs e)
        {
            FillMenu(MenuFillType.Product); // or Item based on your tab
        }

        private void btnseach_menu_Click_1(object sender, EventArgs e)
        {
            FillMenu(MenuFillType.Product);
        }

        private void txtsearchlist_TextChanged(object sender, EventArgs e)
        {
            FillMenu(MenuFillType.Product);
        }

   
    }
}
