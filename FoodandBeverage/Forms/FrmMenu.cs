
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

namespace FoodandBeverage.Forms
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
        }
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


                cmbSearchlist.DataSource = gridSource;
                cmbSearchlist.ValueMember = "Id";
                cmbSearchlist.DisplayMember = "Value";

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

                    if (Convert.ToInt32(cmbMenu_category.SelectedValue) == -2)
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
                                if (Convert.ToBoolean(dgv_menu.Rows[i].Cells[pstatusno].Value) == true)
                                    dmnmenu.Status = 1;
                                else
                                    dmnmenu.Status = 0;
                                dmnmenu.defaultqty = Convert.ToInt32(dgv_menu.Rows[i].Cells[pdefstock].Value);

                                usertype = bllmenu.AddData("prdInsert_upd_menu", dmnmenu);
                                count++;
                            }
                        }
                        if (count == dgv_menu.RowCount)
                        {
                            CMessageBox.Show(" Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                            cmbSearchlist.SelectedIndex = -1;
                            cmbMenu_category.SelectedIndex = -1;
                            txtsearchlist.Text = "";
                            txtsearch.Text = "";
                            Item_Fill();
                        }
                    }
                    else if (rb_product.Checked)
                    {
                        for (int i = 0; i < dgv_menu.RowCount; i++)
                        {
                            //if (((dgv_menu.Rows[i].Cells[piidno].Value) != null && (dgv_menu.Rows[i].Cells[piidno].Value) != "") && (Convert.ToInt32(dgv_menu.Rows[i].Cells[pmidno].Value) > 0 || (Convert.ToInt32(dgv_menu.Rows[i].Cells[pmidno].Value) == 0 && Convert.ToBoolean(dgv_menu.Rows[i].Cells[pstatusno].Value) == true)))  //&& Convert.ToBoolean(dgv_menu.Rows[i].Cells[pstatusno].Value) == true&&(Convert.ToBoolean(dgv_menu.Rows[i].Cells[pstatusno].Value) == true)
                            //{
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
                            //}
                        }
                        if (count > 0)
                        {
                            CMessageBox.Show(" Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            clearall();
                            Product_Fill(0);
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

        public void Fillmenu(int searchmode)
        {
            try
            {

                var itemSource = new List<DmnGridFields>();
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                if (searchmode == -2)
                {
                    dmnSearch.Select = " omid Field1,OfferName Field2,'Combo Product' Field3,finalprice Field4,m.Status AS Field6,m.Mid AS Field7,m.Quantity AS Field8,3 Field9,m.DefaultStock AS Field10 ";
                    dmnSearch.Table = "tblOfferMaster om LEFT JOIN tblMenu m ON m.ProductId=om.omid and m.producttype = 3 ";
                    dmnSearch.WhereNdGroupBy = "where om.status=1  and OfferType=1  AND OfferStartDatetime <= GETDATE()  AND OfferEndDatetime >= GETDATE()";
                    dmnSearch.OrderBy = "order by OfferName";

                    itemSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
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
                            dgv_menu.Rows[i].Cells[ppdtype].Value = itemSource[i].Field9;
                            dgv_menu.Rows[i].Cells[pdefstock].Value =
                                itemSource[i].Field10 == null
                                    ? (decimal)0
                                    : Convert.ToDecimal(itemSource[i].Field10);
                            bool currentStatus = Convert.ToInt32(itemSource[i].Field6) == 1;

                            // Set checkbox column
                            dgv_menu.Rows[i].Cells[pstatusno].Value = currentStatus;

                            // Set action column text
                            DataGridViewCell actionCell = dgv_menu.Rows[i].Cells[pactiveinactiveno];
                            actionCell.Value = currentStatus ? "Disable" : "Activate";

                            // Set cell style colors based on status
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



                            dgv_menu.Rows[i].Cells[pfcidno].Value = itemSource[i].Field5;
                            dgv_menu.Rows[i].Cells[pmidno].Value = itemSource[i].Field7;
                            dgv_menu.Rows[i].Cells[pqtyno].Value = itemSource[i].Field8;

                        }
                    }
                }
                else
                {
                    if (rb_product.Checked == false && rb_item.Checked == false)
                    {
                        dmnSearch.Select = "p.ProductId as Field1,Product as Field2,C.CategoryName AS Field3,p.Price as Field4,P.Fcid AS Field5 ,m.Status AS Field6,m.Mid AS Field7,m.Quantity AS Field8,p.PreparationMode Field9";
                        dmnSearch.Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON C.Fcid=P.Fcid LEFT JOIN tblMenu m ON m.ProductId=p.ProductId";
                        dmnSearch.WhereNdGroupBy = "where p.Status=1 and m.status=1 and P.Fcid=" + searchmode;
                        dmnSearch.OrderBy = "order by Product";
                        rb_product.Checked = true;
                    }
                    else if (searchmode == 0)
                    {
                        dmnSearch.Select = "p.ProductId as Field1,Product as Field2,C.CategoryName AS Field3,p.Price as Field4,P.Fcid AS Field5 ,m.Status AS Field6,m.Mid AS Field7,m.Quantity AS Field8,p.PreparationMode Field9";
                        dmnSearch.Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON C.Fcid=P.Fcid " +
                            "LEFT JOIN tblMenu m ON m.ProductId=p.ProductId ";
                        dmnSearch.WhereNdGroupBy = "Where p.status=1 and  m.status=1";
                        dmnSearch.OrderBy = "order by Product";
                    }
                    else if (rb_product.Checked)
                    {
                        dmnSearch.Select = "p.ProductId as Field1,Product as Field2,C.CategoryName AS Field3,p.Price as Field4,P.Fcid AS Field5 ,m.Status AS Field6,m.Mid AS Field7,m.Quantity AS Field8,p.PreparationMode Field9";
                        dmnSearch.Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON C.Fcid=P.Fcid LEFT JOIN tblMenu m ON m.ProductId=p.ProductId";
                        dmnSearch.WhereNdGroupBy = "where p.Status=1 and m.status=1 and P.Fcid=" + searchmode;
                        dmnSearch.OrderBy = "order by Product";
                    }
                    else if (rb_item.Checked)
                    {
                        dmnSearch.Select = "i.ItemId AS Field1, i.Itemname AS Field2,C.CategoryName AS Field3," +
                  "(SELECT TOP(1) Srate FROM tblProductRate WHERE ProductId = i.ItemId ORDER BY Id DESC) AS Field4, i.Fcid AS Field5," +
                  "(SELECT SUM(QOH) FROM tblProductRate WHERE ProductId = i.ItemId) AS Field6 ";
                        dmnSearch.Table = "tblItem i LEFT JOIN tblCategory_FTI c ON C.Fcid = i.Fcid ";
                        dmnSearch.WhereNdGroupBy = "Where  i.Saletype=1 and i.Status=1 and i.Fcid=" + searchmode;
                        dmnSearch.OrderBy = "order by i.Itemname";
                    }
                    itemSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
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

                            bool currentStatus = Convert.ToInt32(itemSource[i].Field6) == 1;
                            dgv_menu.Rows[i].Cells[pstatusno].Value = currentStatus;


                            if (rb_product.Checked)
                            {
                                dgv_menu.Rows[i].Cells[pfcidno].Value = itemSource[i].Field5;
                                dgv_menu.Rows[i].Cells[pmidno].Value = itemSource[i].Field7;
                                dgv_menu.Rows[i].Cells[pqtyno].Value = itemSource[i].Field8;
                                dgv_menu.Rows[i].Cells[proptype].Value = itemSource[i].Field9;

                                if (Convert.ToInt32(itemSource[i].Field9) == Convert.ToInt32(PreparationType.ReadyFood) && itemSource[i].Field9 != null)
                                    dgv_menu.Rows[i].Cells[proptypename].Value = "Requires cooking";
                                else if (Convert.ToInt32(itemSource[i].Field9) == Convert.ToInt32(PreparationType.FreshCooked) && itemSource[i].Field9 != null)
                                {
                                    //dgv_menu.Rows[i].Cells[pqtyno].ReadOnly = true;
                                    dgv_menu.Rows[i].Cells[proptypename].Value = "Freshly Cooked";
                                }

                                 currentStatus = Convert.ToInt32(itemSource[i].Field6) == 1;

                                // Set checkbox column
                                dgv_menu.Rows[i].Cells[pstatusno].Value = currentStatus;

                                // Set action column text
                                DataGridViewCell actionCell = dgv_menu.Rows[i].Cells[pactiveinactiveno];
                                actionCell.Value = currentStatus ? "Disable" : "Activate";

                                // Set cell style colors based on status
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
                            else if (rb_item.Checked)
                                dgv_menu.Rows[i].Cells[pqtyno].Value = itemSource[i].Field6;

                        }

                        if (rb_product.Checked)
                        {
                            Rectangle headerCellRect = dgv_menu.GetColumnDisplayRectangle(pstatusno, true);
                            int checkBoxX = headerCellRect.X + (headerCellRect.Width - chk_selectall.Width) / 2;
                            Point headerCellLocation = this.dgv_menu.GetCellDisplayRectangle(pstatusno, -1, true).Location;
                            chk_selectall.Location = new Point(checkBoxX, headerCellLocation.Y + 40);
                            chk_selectall.BackColor = Color.White;
                            chk_selectall.Size = new Size(18, 18);
                            chk_selectall.Click += new EventHandler(chk_selectall_Clicked);
                            dgv_menu.Controls.Add(chk_selectall);

                            chk_seleall_pro.Visible = true;
                            chk_selectall.Visible = true;
                        }
                        else if (rb_item.Checked)
                        {
                            dgv_menu.Columns[pstatusno].Visible = false;
                            chk_selectall.Visible = false;
                            chk_seleall_pro.Visible = false;
                        }

                        int n = dgv_menu.RowCount - 1;
                        dgv_menu.Rows[n].Cells[pnameno].Value = "F8 for Products";
                        dgv_menu.Rows[n].Cells[pnameno].Style.ForeColor = Color.Red;

                    }
                    else
                    {
                        if (rb_product.Checked)
                            chk_seleall_pro.Visible = true;
                        chk_selectall.Visible = false;
                        CMessageBox.Show("No Products For This Category", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page  Fillmenu()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));

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
                if (rb_product.Checked)
                {
                    _objSearch.ColumnHeaders = "ID,ItemCode,Product,Price,Category Name";
                    _objSearch.Select = "p.ProductId as Id,ItemCode  as Field1,Product  as Field2,Price  as Field3,c.CategoryName  as Field4";
                    _objSearch.Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON C.Fcid=p.Fcid LEFT JOIN tblMenu m ON m.ProductId=p.ProductId and m.Status=1";
                    _objSearch.WhereNdGroupBy = "where p.Status=1 and ISNULL(m.MId,0)=0";
                    _objSearch.OrderBy = " ORDER BY p.Product";
                }
                else if (rb_item.Checked)
                {
                    _objSearch.ColumnHeaders = "ID,ItemCode,Product,Price,Category Name,Quantity";
                    _objSearch.Select = "i.ItemId AS ID,i.ItemCode_UPC Field3, i.Itemname AS Field2," +
    "(SELECT TOP(1) Srate FROM tblProductRate WHERE ProductId = i.ItemId ORDER BY Id DESC) AS Field3,C.CategoryName AS Field4," +
    "(SELECT SUM(QOH) FROM tblProductRate WHERE ProductId = i.ItemId) AS Field5";
                    _objSearch.Table = "tblItem i LEFT JOIN tblCategory_FTI c ON C.Fcid = i.Fcid";
                    _objSearch.WhereNdGroupBy = "Where  i.Saletype=1 and i.Status=1";
                    _objSearch.OrderBy = "order by i.Itemname";

                }
                else
                {
                    CMessageBox.Show("Please select a product or item", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
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
                        if (rb_product.Checked)
                        {

                            BllSearch<DmnGridFields> bllSearch_product = new BllSearch<DmnGridFields>();
                            DmnSearch objSearch = new DmnSearch();
                            objSearch.Select = "p.ProductId as Field1,Product as Field2,C.CategoryName AS Field3,p.Price as Field4,P.Fcid AS Field5 ";
                            objSearch.Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON C.Fcid=P.Fcid ";
                            objSearch.WhereNdGroupBy = "Where p.status=1 and  p.productid=" + returnValue;
                            objSearch.OrderBy = "";
                            var itemSource = bllSearch_product.GetSearchData("prdSearch", objSearch);
                            if (itemSource.Count > 0)
                            {
                                for (int i = 0; i < dgv_menu.RowCount; i++)
                                {
                                    if (Convert.ToInt32(dgv_menu.Rows[i].Cells[piidno].Value) == Convert.ToInt32(itemSource[0].Field1))
                                    {
                                        CMessageBox.Show("Product Already selected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        dgv_menu.ClearSelection();
                                        dgv_menu.Rows[i].Selected = true;
                                        return;
                                    }
                                }
                                int rowno = dgv_menu.RowCount - 1;
                                dgv_menu.Rows[rowno].Cells[slno].Value = rowno + 1;
                                dgv_menu.Rows[rowno].Cells[piidno].Value = itemSource[0].Field1;
                                dgv_menu.Rows[rowno].Cells[pnameno].Value = itemSource[0].Field2;
                                dgv_menu.Rows[rowno].Cells[pcatno].Value = itemSource[0].Field3;
                                dgv_menu.Rows[rowno].Cells[ppriceno].Value = itemSource[0].Field4;
                                dgv_menu.Rows[rowno].Cells[pfcidno].Value = itemSource[0].Field5;
                                dgv_menu.Rows[rowno].Cells[pmidno].Value = itemSource[0].Field7;
                                dgv_menu.Rows[rowno].Cells[pnameno].Style.ForeColor = Color.Black;
                                dgv_menu.Rows[rowno].Cells[pqtyno].Style.BackColor = Color.LightGray;
                                dgv_menu.Columns[pstatusno].Visible = true;
                                chk_selectall.Visible = true;
                            }
                            dgv_menu.Rows.Add();
                            int r = dgv_menu.RowCount - 1;
                            dgv_menu.Rows[r].Cells[pnameno].Value = "F8 for Products";
                            dgv_menu.Rows[r].Cells[pnameno].Style.ForeColor = Color.Red;

                        }

                        else if (rb_item.Checked)
                        {
                            BllSearch<DmnGridFields> bllSearch_product = new BllSearch<DmnGridFields>();
                            DmnSearch objSearch = new DmnSearch();
                            objSearch.Select = "i.ItemId AS Field1, i.Itemname AS Field2,C.CategoryName AS Field3," +
                    "(SELECT TOP(1) Srate FROM tblProductRate WHERE ProductId = i.ItemId ORDER BY Id DESC) AS Field4, i.Fcid AS Field5," +
                    "(SELECT SUM(QOH) FROM tblProductRate WHERE ProductId = i.ItemId) AS Field6 ";
                            objSearch.Table = "tblItem i LEFT JOIN tblCategory_FTI c ON C.Fcid = i.Fcid";
                            objSearch.WhereNdGroupBy = "Where  i.Saletype=1 and i.Status=1 and i.itemid = " + returnValue;
                            objSearch.OrderBy = "order by i.Itemname";
                            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                            dgv_menu.Rows.Clear();
                            if (itemSource.Count > 0)
                            {
                                dgv_menu.Rows.Add(itemSource.Count);
                                for (int i = 0; i < itemSource.Count; i++)
                                {
                                    dgv_menu.Rows[i].Cells[slno].Value = i + 1;
                                    dgv_menu.Rows[i].Cells[piidno].Value = itemSource[i].Field1;
                                    dgv_menu.Rows[i].Cells[pnameno].Value = itemSource[i].Field2;
                                    dgv_menu.Rows[i].Cells[pcatno].Value = itemSource[i].Field3;
                                    dgv_menu.Rows[i].Cells[ppriceno].Value = itemSource[i].Field4;
                                    dgv_menu.Rows[i].Cells[pqtyno].Value = itemSource[i].Field6;
                                    dgv_menu.Rows[i].Cells[pfcidno].Value = itemSource[i].Field5;

                                    dgv_menu.Columns[pstatusno].Visible = false;
                                    chk_selectall.Visible = false;

                                    if (Convert.ToInt32(itemSource[i].Field6) == 1)
                                        dgv_menu.Rows[i].Cells[pstatusno].Value = true;
                                    else
                                        dgv_menu.Rows[i].Cells[pstatusno].Value = false;

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
                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page btnSearch_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }

        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            try
            {
                clearall();

                if (rb_product.Checked && dgv_menu.RowCount == 0)
                {
                    dgv_menu.Rows.Clear();
                    dgv_menu.Rows.Add();
                    int r = dgv_menu.RowCount - 1;
                    dgv_menu.Rows[r].Cells[pnameno].Value = "F8 for Products";
                    dgv_menu.Rows[r].Cells[pnameno].Style.ForeColor = Color.Red;
                }

                if (rb_product.Checked)
                    chk_seleall_pro.Checked = true;
                else
                    chk_seleall_pro.Checked = false;
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
                cmbSearchlist.SelectedIndex = -1;
                cmbMenu_category.SelectedIndex = -1;
                txtsearchlist.Text = "";
                txtsearch.Text = "";

                //if (rb_product.Checked==false)
                //{
                //    rb_product.Checked = true;
                //    chk_seleall_pro.Checked = false;
                //}
                //rb_item.Checked = false;
                if (rb_product.Checked)
                    Product_Fill(0);
                else if (rb_item.Checked)
                    Item_Fill();
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
                clearall();

                if (rb_item.Checked)
                    Item_Fill();
                else if (rb_product.Checked)
                    Product_Fill(0);

                Fillcombo();
                textb_shape();

                #region menu Picture Folder 
                if (!Directory.Exists(Compressedimg))
                {
                    Directory.CreateDirectory(Compressedimg);
                    DirectoryInfo dInfo = new DirectoryInfo(Compressedimg);
                    DirectorySecurity dSecurity = dInfo.GetAccessControl();
                    dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                    dInfo.SetAccessControl(dSecurity);
                }
                else
                {
                    foreach (string file in Directory.GetFiles(Compressedimg))
                    {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        File.Delete(file);
                    }
                }
                #endregion
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

        #region gallery creation
        public void GalleryView(int serachmode)
        {
            try
            {
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch dmnSearch = new DmnSearch();

                dmnSearch.Select = "p.*, m.status as menustatus";
                dmnSearch.Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON C.Fcid=P.Fcid LEFT JOIN tblMenu m ON m.ProductId=p.ProductId";
                if (comboBoxChanged)
                {
                    comboBoxChanged = false;
                    if (cmbSearchlist.SelectedIndex == 0)
                        dmnSearch.WhereNdGroupBy = "where m.Status=1";
                    else
                        dmnSearch.WhereNdGroupBy = "where m.Status=1 and  p.Fcid = " + serachmode;
                }
                else
                    dmnSearch.WhereNdGroupBy = "where m.Status=1";
                dmnSearch.OrderBy = "order by Product";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);

                if (gridSource.Count > 0)
                {
                    tlp_gallery_sub.Controls.Clear();
                    int rowIndex = 0;
                    int columnIndex = 0;
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        if (Convert.ToInt32(gridSource[i].menustatus) == 1)
                        {
                            if (columnIndex == 5)
                            {
                                columnIndex = 0;
                                rowIndex++;
                            }
                            string info = gridSource[i].Product + "," + gridSource[i].Price + "," + gridSource[i].MRP + "," + gridSource[i].Tax + "," + gridSource[i].TaxType + "," + gridSource[i].DiscountPer + "," + gridSource[i].DiscountAmt;

                            TableLayoutPanel tlp_Gallery_Box = new TableLayoutPanel();
                            PictureBox pictureBox = new PictureBox();
                            Label lbltablename = new Label();

                            // Configure the PictureBox
                            string imageName = Convert.ToString(gridSource[i].ProductId) + ".jpeg";
                            bool imageExist, brLogoExist;
                            if (imageName != "" && imageName != null)
                            {
                                imageExist = clsDocument.FileExistOrNot(imageName, DocumentType.FoodPhoto);
                                if (imageExist)
                                    pictureBox.Image = clsDocument.DownloadImage(imageName, DocumentType.FoodPhoto);
                                else
                                    pictureBox.Image = FoodandBeverage.Properties.Resources.Deafult_food_pic;
                            }
                            pictureBox.Name = Convert.ToString(gridSource[i].ProductId);
                            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                            pictureBox.Width = 150;
                            pictureBox.Height = 150;
                            pictureBox.Tag = info;

                            // Configure the Label
                            lbltablename.Name = Convert.ToString(gridSource[i].ProductId);
                            tlp_Gallery_Box.Tag = info;
                            lbltablename.Text = Convert.ToString(gridSource[i].Product);
                            lbltablename.ForeColor = Color.Black;
                            lbltablename.AutoSize = true;
                            lbltablename.BackColor = Color.Transparent;
                            lbltablename.Dock = DockStyle.Top;
                            lbltablename.Width = 60;
                            lbltablename.Tag = info;
                            lbltablename.Font = new Font("Arial", 10, FontStyle.Bold);

                            // Configure the TableLayoutPanel
                            tlp_Gallery_Box.RowCount = 2;
                            // Add the PictureBox and Label to the TableLayoutPanel
                            tlp_Gallery_Box.Controls.Add(pictureBox, 0, 0); // PictureBox in the first row
                            tlp_Gallery_Box.Controls.Add(lbltablename, 0, 1); // Label in the second row
                            tlp_Gallery_Box.Name = Convert.ToString(gridSource[i].ProductId);
                            tlp_Gallery_Box.Tag = info;
                            tlp_Gallery_Box.Dock = DockStyle.None;
                            tlp_Gallery_Box.Margin = new Padding(20, 10, 20, 10);
                            tlp_Gallery_Box.Width = 150;
                            tlp_Gallery_Box.Height = 200;
                            tlp_Gallery_Box.BorderStyle = BorderStyle.FixedSingle;
                            tlp_Gallery_Box.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

                            pictureBox.MouseEnter += TlpTableInner_MouseEnter;
                            lbltablename.MouseEnter += TlpTableInner_MouseEnter;

                            pictureBox.MouseLeave += TlpTableInner_MouseLeave;
                            lbltablename.MouseLeave += TlpTableInner_MouseLeave;

                            tlp_Gallery_Box.MouseEnter += TlpTableInner_MouseEnter;
                            tlp_Gallery_Box.MouseLeave += TlpTableInner_MouseLeave;

                            pictureBox.Click += new EventHandler(ProductClickEventHandler);
                            lbltablename.Click += new EventHandler(ProductClickEventHandler);
                            tlp_Gallery_Box.Click += new EventHandler(ProductClickEventHandler);

                            pictureBox.MouseHover += new EventHandler(ProductMousehoverEventHandler);
                            lbltablename.MouseHover += new EventHandler(ProductMousehoverEventHandler);
                            tlp_Gallery_Box.MouseHover += new EventHandler(ProductMousehoverEventHandler);

                            tlp_gallery_sub.Controls.Add(tlp_Gallery_Box, columnIndex, rowIndex);
                            columnIndex++;
                        }

                    }
                    tlp_Gallery_Box.BorderStyle = BorderStyle.FixedSingle;
                    tlp_Gallery_Box.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    tlp_Gallery_Main.Dock = DockStyle.Fill;
                    tlp_gallery_sub.Dock = DockStyle.Fill;
                    tlp_menu_Gallery_main.Dock = DockStyle.Fill;
                    tlp_gallery_sub.AutoScroll = true;
                    tlp_menu_Gallery_main.Controls.Add(tlp_gallery_sub, 0, 1);
                }
                else
                    CMessageBox.Show("No Products For This Category");
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page GalleryView()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }


        }
        private void ProductMousehoverEventHandler(object sender, EventArgs e)
        {
            try
            {
                toolTip_gallery.BackColor = Color.Green;
                toolTip_gallery.ForeColor = Color.Black;
                if (sender is PictureBox)
                {
                    PictureBox pictureBox = (PictureBox)sender;
                    string info = Convert.ToString(pictureBox.Tag);
                    // Split the info string by commas
                    string[] infoParts = info.Split(',');
                    string tooltipText = "";
                    tooltipText = $"Name : {infoParts[0]}{Environment.NewLine}price :{infoParts[1]}{Environment.NewLine}Mrp :{infoParts[2]}{Environment.NewLine}Tax :{infoParts[3]}{Environment.NewLine}Tax Type :{infoParts[4]}{Environment.NewLine}Discount % :{infoParts[5]}{Environment.NewLine}Discount Amount :{infoParts[6]}";
                    toolTip_gallery.Show(tooltipText, pictureBox);

                }
                else if (sender is Label)
                {
                    // Handle Label hover
                    Label label = (Label)sender;
                    string info = Convert.ToString(label.Tag);
                    // Split the info string by commas
                    if (info != null)
                    {
                        string[] infoParts = info.Split(',');
                        string tooltipText = "";
                        tooltipText = $"Name : {infoParts[0]}{Environment.NewLine}price :{infoParts[1]}{Environment.NewLine}Mrp :{infoParts[2]}{Environment.NewLine}Tax :{infoParts[3]}{Environment.NewLine}Tax Type :{infoParts[4]}{Environment.NewLine}Discount % :{infoParts[5]}{Environment.NewLine}Discount Amount :{infoParts[6]}";
                        toolTip_gallery.Show(tooltipText, label);
                    }
                }
                else if (sender is TableLayoutPanel)
                {
                    // Handle TableLayoutPanel hover
                    TableLayoutPanel tlp = (TableLayoutPanel)sender;
                    string info = Convert.ToString(tlp.Tag);
                    // Split the info string by commas
                    string[] infoParts = info.Split(',');
                    string tooltipText = "";
                    tooltipText = $"Name : {infoParts[0]}{Environment.NewLine}price :{infoParts[1]}{Environment.NewLine}Mrp :{infoParts[2]}{Environment.NewLine}Tax :{infoParts[3]}{Environment.NewLine}Tax Type :{infoParts[4]}{Environment.NewLine}Discount % :{infoParts[5]}{Environment.NewLine}Discount Amount :{infoParts[6]}";
                    toolTip_gallery.Show(tooltipText, tlp);
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page ProductMousehoverEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }


        private void ProductClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                if (clickedControl != null)
                {
                    string pid = clickedControl.Name;
                    // FillData(Convert.ToInt32(pid));
                    CMessageBox.Show("Clicked Table ID: " + pid);
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page ProductClickEventHandler()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }

        private void TlpTableInner_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                TableLayoutPanel tlp = sender as TableLayoutPanel;
                if (tlp != null)
                {
                    foreach (Control control in tlp.Controls)
                    {
                        if (control is Label)
                        {
                            Label lbl = control as Label;
                            originalBackColor = lbl.BackColor;
                            lbl.BackColor = Color.LightGray;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page TlpTableInner_MouseEnter()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }

        private void TlpTableInner_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                TableLayoutPanel tlp = sender as TableLayoutPanel;
                if (tlp != null)
                {
                    foreach (Control control in tlp.Controls)
                    {
                        if (control is Label)
                        {
                            Label lbl = control as Label;
                            lbl.BackColor = originalBackColor;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page TlpTableInner_MouseLeave()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }
        #endregion
        private void Btn_view_menu_Click(object sender, EventArgs e)
        {
            try
            {
                tlp_registartion_main.Visible = false;
                tlp_menu_Gallery_main.Visible = true;
                Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
                tlp_main.Controls.Remove(controlToRemove);
                tlp_main.Controls.Add(tlp_menu_Gallery_main, 1, 0);
                tlp_menu_Gallery_main.Dock = DockStyle.Fill;
                GalleryView(0);
                cmbSearchlist.Focus();
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page Btn_view_menu_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }
        private void cmbMenu_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Fillmenu(Convert.ToInt32(cmbMenu_category.SelectedValue));
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page cmbMenu_SelectionChangeCommitted()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }
        private void btn_bck_reg_Click(object sender, EventArgs e)
        {
            try
            {
                tlp_menu_Gallery_main.Visible = false;
                tlp_registartion_main.Visible = true;
                Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
                tlp_main.Controls.Remove(controlToRemove);
                tlp_main.Controls.Add(tlp_registartion_main, 1, 0);
                tlp_registartion_main.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page btn_bck_reg_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }


        private void txtsearch_Leave(object sender, EventArgs e)
        {
            if (txtsearch.Text == "")
            {
                txtsearch.Text = "Food Name .....";
                txtsearch.ForeColor = Color.LightGray;
            }
        }

        private void txtsearch_Enter(object sender, EventArgs e)
        {
            if (txtsearch.Text == "Food Name .....")
            {
                txtsearch.Text = "";
                txtsearch.ForeColor = Color.Black;
            }
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

        private void btn_refresh_list_Click(object sender, EventArgs e)
        {
            try
            {
                txtsearchlist.Text = "";
                cmbMenu_category.SelectedIndex = -1;
                if (rb_item.Checked)
                {
                    Item_Fill();
                }
                else if (rb_product.Checked)
                {
                    if (chk_seleall_pro.Checked)
                        chk_seleall_pro.Checked = false;
                    Product_Fill(0);
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page btn_refresh_list_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }

        private void btnseach_menu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtsearchlist.Text))
                {
                    CMessageBox.Show("Please Enter Food Name ");
                    txtsearchlist.Focus();
                    return;
                }

                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch dmnSearch = new DmnSearch();
                if (rb_product.Checked)
                {
                    dmnSearch.Select = "p.ProductId as Field1,Product as Field2,C.CategoryName AS Field3,p.Price as Field4,P.Fcid AS Field5 ,m.Status AS Field6,m.Mid AS Field7,m.Quantity AS Field8";
                    dmnSearch.Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON C.Fcid=P.Fcid LEFT JOIN tblMenu m ON m.ProductId=p.ProductId";
                    dmnSearch.WhereNdGroupBy = $"where p.Status=1 and m.status=1 and p.Product LIKE '%{txtsearchlist.Text}%'";
                    dmnSearch.OrderBy = "order by Product";
                }
                else if (rb_item.Checked)
                {
                    dmnSearch.Select = "i.ItemId AS Field1, i.Itemname AS Field2,C.CategoryName AS Field3," +
                        "(SELECT TOP(1) Srate FROM tblProductRate WHERE ProductId = i.ItemId ORDER BY Id DESC) AS Field4, i.Fcid AS Field5," +
                        "(SELECT SUM(QOH) FROM tblProductRate WHERE ProductId = i.ItemId) AS Field6 ";
                    dmnSearch.Table = "tblItem i LEFT JOIN tblCategory_FTI c ON C.Fcid = i.Fcid ";
                    dmnSearch.WhereNdGroupBy = $"Where i.Saletype=1 and i.Status=1 and i.itemname LIKE '%{txtsearchlist.Text}%'";
                    dmnSearch.OrderBy = "order by i.Itemname";
                }

                var itemSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                dgv_menu.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_menu.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_menu.Rows[i].Cells[slno].Value = i + 1;
                        dgv_menu.Rows[i].Cells[piidno].Value = itemSource[i].Field1;
                        dgv_menu.Rows[i].Cells[pnameno].Value = itemSource[i].Field2;
                        dgv_menu.Rows[i].Cells[pcatno].Value = itemSource[i].Field3;
                        dgv_menu.Rows[i].Cells[ppriceno].Value = itemSource[i].Field4;

                        if (Convert.ToInt32(itemSource[i].Field6) == 1)
                            dgv_menu.Rows[i].Cells[pstatusno].Value = true;
                        else
                            dgv_menu.Rows[i].Cells[pstatusno].Value = false;

                        if (rb_product.Checked)
                        {
                            dgv_menu.Rows[i].Cells[pfcidno].Value = itemSource[i].Field5;
                            dgv_menu.Rows[i].Cells[pmidno].Value = itemSource[i].Field7;
                            dgv_menu.Rows[i].Cells[pqtyno].Value = itemSource[i].Field8;

                            dgv_menu.Rows[i].Cells[pqtyno].Style.BackColor = Color.LightGray;

                            Rectangle headerCellRect = dgv_menu.GetColumnDisplayRectangle(pstatusno, true);
                            int checkBoxX = headerCellRect.X + (headerCellRect.Width - chk_selectall.Width) / 2;
                            Point headerCellLocation = this.dgv_menu.GetCellDisplayRectangle(pstatusno, -1, true).Location;
                            chk_selectall.Location = new Point(checkBoxX, headerCellLocation.Y + 40);
                            chk_selectall.BackColor = Color.White;
                            chk_selectall.Size = new Size(18, 18);
                            chk_selectall.Click += new EventHandler(chk_selectall_Clicked);
                            dgv_menu.Controls.Add(chk_selectall);
                        }
                        else if (rb_item.Checked)
                        {
                            dgv_menu.Rows[i].Cells[pqtyno].Value = itemSource[i].Field6;
                        }
                    }

                    if (rb_product.Checked)
                    {
                        chk_selectall.Visible = true;
                        chk_seleall_pro.Visible = true;
                    }
                    else if (rb_item.Checked)
                    {
                        dgv_menu.Columns[pstatusno].Visible = false;
                        chk_selectall.Visible = false;
                        chk_seleall_pro.Visible = false;
                    }

                    if (chk_seleall_pro.Checked)
                        chk_seleall_pro.Checked = false;
                }
                else
                {
                    if (chk_seleall_pro.Checked)
                        chk_seleall_pro.Checked = false;
                    chk_selectall.Visible = false;
                    CMessageBox.Show("No Products For This Name", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page btnseach_menu_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }




        private void Search_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtsearch.Text == "" || txtsearch.Text == null)
                {
                    CMessageBox.Show("Please Enter Food Name ");
                    txtsearch.Focus();
                    return;
                }
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "p.*";
                dmnSearch.Table = "tblProduct p  LEFT JOIN tblMenu m ON m.ProductId=p.ProductId";
                dmnSearch.WhereNdGroupBy = "where m.Status=1 and  m.Name LIKE '%" + txtsearch.Text + "%'";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    tlp_gallery_sub.Controls.Clear();
                    int rowIndex = 0;
                    int columnIndex = 0;
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        if (columnIndex == 5)
                        {
                            columnIndex = 0;
                            rowIndex++;
                        }
                        string info = gridSource[i].Product + "," + gridSource[i].Price + "," + gridSource[i].MRP + "," + gridSource[i].Tax + "," + gridSource[i].TaxType + "," + gridSource[i].DiscountPer + "," + gridSource[i].DiscountAmt;

                        TableLayoutPanel tlp_Gallery_Box = new TableLayoutPanel();
                        PictureBox pictureBox = new PictureBox();
                        Label lbltablename = new Label();

                        // Configure the PictureBox
                        string imageName = Convert.ToString(gridSource[i].ProductId) + ".jpeg";
                        bool imageExist, brLogoExist;
                        if (imageName != "" && imageName != null)
                        {
                            imageExist = clsDocument.FileExistOrNot(imageName, DocumentType.FoodPhoto);
                            if (imageExist)
                                pictureBox.Image = clsDocument.DownloadImage(imageName, DocumentType.FoodPhoto);
                            else
                                pictureBox.Image = FoodandBeverage.Properties.Resources.Deafult_food_pic;
                        }
                        pictureBox.Name = Convert.ToString(gridSource[i].ProductId);
                        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                        pictureBox.Width = 150;
                        pictureBox.Height = 150;
                        pictureBox.Tag = info;

                        // Configure the Label
                        lbltablename.Name = Convert.ToString(gridSource[i].ProductId);
                        tlp_Gallery_Box.Tag = info;
                        lbltablename.Text = Convert.ToString(gridSource[i].Product);
                        lbltablename.ForeColor = Color.Black;
                        lbltablename.AutoSize = true;
                        lbltablename.BackColor = Color.Transparent;
                        lbltablename.Dock = DockStyle.Top;
                        lbltablename.Width = 60;
                        lbltablename.Tag = info;
                        lbltablename.Font = new Font("Arial", 10, FontStyle.Bold);

                        // Configure the TableLayoutPanel
                        tlp_Gallery_Box.RowCount = 2;
                        // Add the PictureBox and Label to the TableLayoutPanel
                        tlp_Gallery_Box.Controls.Add(pictureBox, 0, 0); // PictureBox in the first row
                        tlp_Gallery_Box.Controls.Add(lbltablename, 0, 1); // Label in the second row
                        tlp_Gallery_Box.Name = Convert.ToString(gridSource[i].ProductId);
                        tlp_Gallery_Box.Tag = info;
                        tlp_Gallery_Box.Dock = DockStyle.None;
                        tlp_Gallery_Box.Margin = new Padding(20, 10, 20, 10);
                        tlp_Gallery_Box.Width = 150;
                        tlp_Gallery_Box.Height = 200;
                        tlp_Gallery_Box.BorderStyle = BorderStyle.FixedSingle;
                        tlp_Gallery_Box.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;

                        pictureBox.MouseEnter += TlpTableInner_MouseEnter;
                        lbltablename.MouseEnter += TlpTableInner_MouseEnter;

                        pictureBox.MouseLeave += TlpTableInner_MouseLeave;
                        lbltablename.MouseLeave += TlpTableInner_MouseLeave;

                        tlp_Gallery_Box.MouseEnter += TlpTableInner_MouseEnter;
                        tlp_Gallery_Box.MouseLeave += TlpTableInner_MouseLeave;

                        pictureBox.Click += new EventHandler(ProductClickEventHandler);
                        lbltablename.Click += new EventHandler(ProductClickEventHandler);
                        tlp_Gallery_Box.Click += new EventHandler(ProductClickEventHandler);

                        pictureBox.MouseHover += new EventHandler(ProductMousehoverEventHandler);
                        lbltablename.MouseHover += new EventHandler(ProductMousehoverEventHandler);
                        tlp_Gallery_Box.MouseHover += new EventHandler(ProductMousehoverEventHandler);

                        tlp_gallery_sub.Controls.Add(tlp_Gallery_Box, columnIndex, rowIndex);
                        columnIndex++;
                    }
                    tlp_Gallery_Box.BorderStyle = BorderStyle.FixedSingle;
                    tlp_Gallery_Box.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                    tlp_Gallery_Main.Dock = DockStyle.Fill;
                    tlp_gallery_sub.Dock = DockStyle.Fill;
                    tlp_menu_Gallery_main.Dock = DockStyle.Fill;
                    tlp_gallery_sub.AutoScroll = true;
                    tlp_menu_Gallery_main.Controls.Add(tlp_gallery_sub, 0, 1);
                }
                else
                    CMessageBox.Show("No Products For This Category", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page Search_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }

        private void btn_refresh_list2_Click(object sender, EventArgs e)
        {
            GalleryView(0);
        }

        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtsearchlist, txtsearch })
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

        private void cmbSearchlist_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            comboBoxChanged = true;
            GalleryView(Convert.ToInt32(cmbSearchlist.SelectedValue));
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
                    if (tlp_menu_Gallery_main.Visible)
                    {
                        tlp_menu_Gallery_main.Visible = false;
                        tlp_registartion_main.Visible = true;
                        Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
                        tlp_main.Controls.Remove(controlToRemove);
                        tlp_main.Controls.Add(tlp_registartion_main, 1, 0);
                        tlp_registartion_main.Dock = DockStyle.Fill;
                    }
                    else
                    {
                        FrmDashboard _instancedash = Application.OpenForms["FrmDashboard"] as FrmDashboard;
                        _instancedash.Closeform();
                    }
                    return true;
                case Keys.End:
                    if (tlp_menu_Gallery_main.Visible != true)
                    {
                        int lastColumnIndex = 6;
                        int lastRowIndex = dgv_menu.Rows.Count - 1;
                        dgv_menu.CurrentCell = dgv_menu[lastColumnIndex, lastRowIndex];
                    }
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        public void Movefocus(int keytype)
        {

            try
            {
                if (tlp_menu_Gallery_main.Visible)
                {
                    if (cmbSearchlist.Focused)
                    {
                        btn_refresh_list2.Focus();
                    }
                    else if (btn_refresh_list2.Focused)
                    {
                        if (keytype == 1)
                            txtsearch.Focus();
                        else
                            btn_refresh_list2.PerformClick();
                    }
                    else if (txtsearch.Focused)
                    {
                        Search.Focus();
                    }

                    else if (Search.Focused)
                    {
                        if (keytype == 1)
                        {
                            tlp_menu_Gallery_main.Focus();
                        }
                        else
                            Search.PerformClick();
                    }
                }
                else
                {
                    if (cmbMenu_category.Focused)
                    {
                        btn_refresh_list.Focus();
                    }
                    else if (btn_refresh_list.Focused)
                    {
                        if (keytype == 1)
                            txtsearchlist.Focus();
                        else
                            btn_refresh_list.PerformClick();
                    }
                    else if (txtsearchlist.Focused)
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
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page Movefocus()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }

        private void rb_product_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_product.Checked)
            {
                cmbMenu_category.SelectedIndex = -1;
                Product_Fill(0);
                txtsearchlist.Text = "";
                chk_seleall_pro.Visible = true;
            }
        }
        public void Item_Fill()
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "i.ItemId AS Field1, i.Itemname AS Field2,C.CategoryName AS Field3," +
                    "(SELECT TOP(1) Srate FROM tblProductRate WHERE ProductId = i.ItemId ORDER BY Id DESC) AS Field4, i.Fcid AS Field5," +
                    "(SELECT SUM(QOH) FROM tblProductRate WHERE ProductId = i.ItemId) AS Field6 ";
                objSearch.Table = "tblItem i LEFT JOIN tblCategory_FTI c ON C.Fcid = i.Fcid";
                objSearch.WhereNdGroupBy = "Where  i.Saletype=1 and i.Status=1";
                objSearch.OrderBy = "order by i.Itemname";
                var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
                dgv_menu.Rows.Clear();
                if (itemSource.Count > 0)
                {
                    dgv_menu.Rows.Add(itemSource.Count);
                    for (int i = 0; i < itemSource.Count; i++)
                    {
                        dgv_menu.Rows[i].Cells[slno].Value = i + 1;
                        dgv_menu.Rows[i].Cells[piidno].Value = itemSource[i].Field1;
                        dgv_menu.Rows[i].Cells[pnameno].Value = itemSource[i].Field2;
                        dgv_menu.Rows[i].Cells[pcatno].Value = itemSource[i].Field3;
                        dgv_menu.Rows[i].Cells[ppriceno].Value = itemSource[i].Field4;
                        dgv_menu.Rows[i].Cells[pqtyno].Value = itemSource[i].Field6;
                        dgv_menu.Rows[i].Cells[pfcidno].Value = itemSource[i].Field5;

                        dgv_menu.Columns[pstatusno].Visible = false;
                        chk_selectall.Visible = false;
                        chk_seleall_pro.Visible = false;

                        if (Convert.ToInt32(itemSource[i].Field6) == 1)
                            dgv_menu.Rows[i].Cells[pstatusno].Value = true;
                        else
                            dgv_menu.Rows[i].Cells[pstatusno].Value = false;

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
                else
                    CMessageBox.Show("No Products For This Category", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page Item_Fill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
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
                        chk_seleall_pro.Visible = true;

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
        public void Product_Fill(int flag)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                if (flag == 0)
                {
                    objSearch.Select = "p.ProductId as Field1,Product as Field2,C.CategoryName AS Field3,p.Price as Field4,P.Fcid AS Field5 ,m.Status AS Field6,m.Mid AS Field7,m.Quantity AS Field8,p.PreparationMode Field9,m.DefaultStock AS Field10";
                    objSearch.Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON C.Fcid=P.Fcid " +
                        "LEFT JOIN tblMenu m ON m.ProductId=p.ProductId ";
                    objSearch.WhereNdGroupBy = "Where p.status=1 and  m.status=1 GROUP BY p.ProductId,Product,C.CategoryName,p.Price,P.Fcid,m.Status,m.Mid,m.Quantity,p.PreparationMode,m.DefaultStock";


                    objSearch.OrderBy = "order by Product";
                    var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);

                    dgv_menu.Rows.Clear();
                    if (itemSource.Count > 0)
                    {
                        //Decimal totstock = "0.00";
                        dgv_menu.Rows.Add(itemSource.Count + 1);
                        for (int i = 0; i < itemSource.Count; i++)
                        {
                            //totstock = totstock + Convert.ToDecimal(itemSource[i].Field8);
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
                            chk_seleall_pro.Visible = true;

                            bool currentStatus = Convert.ToInt32(itemSource[i].Field6) == 1;

                            // Set checkbox column
                            dgv_menu.Rows[i].Cells[pstatusno].Value = currentStatus;

                            // Set action column text
                            DataGridViewCell buttonCell = dgv_menu.Rows[i].Cells[pactiveinactiveno];
                            buttonCell.Value = currentStatus ? "Disable" : "Activate";
                            if (buttonCell != null)
                            {
                                if (currentStatus)
                                {
                                    buttonCell.Style.BackColor = Color.Red;
                                    buttonCell.Style.ForeColor = Color.White;
                                }
                                else
                                {
                                    buttonCell.Style.BackColor = Color.Green;
                                    buttonCell.Style.ForeColor = Color.White;
                                }
                            }


                            dgv_menu.Rows[i].Cells[proptype].Value = itemSource[i].Field9;
                            dgv_menu.Rows[i].Cells[pdefstock].Value =
                                 itemSource[i].Field10 == null
                                     ? (decimal)0
                                     : Convert.ToDecimal(itemSource[i].Field10);

                            if (Convert.ToInt32(itemSource[i].Field9) == Convert.ToInt32(PreparationType.ReadyFood) && itemSource[i].Field9 != null)
                                dgv_menu.Rows[i].Cells[proptypename].Value = "Requires cooking";
                            else if (Convert.ToInt32(itemSource[i].Field9) == Convert.ToInt32(PreparationType.FreshCooked) && itemSource[i].Field9 != null)
                            {
                                dgv_menu.Rows[i].Cells[proptypename].Value = "Freshly Cooked";
                                //dgv_menu.Rows[i].Cells[pqtyno].ReadOnly = true;
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


                        int n = dgv_menu.RowCount - 1;
                        dgv_menu.Rows[n].Cells[pnameno].Value = "F8 for Products";
                        dgv_menu.Rows[n].Cells[pnameno].Style.ForeColor = Color.Red;

                    }
                    else
                    {
                        chk_seleall_pro.Visible = true;
                        chk_selectall.Visible = false;
                        dgv_menu.Rows.Clear();
                        dgv_menu.Rows.Add();
                        int r = dgv_menu.RowCount - 1;
                        dgv_menu.Rows[r].Cells[pnameno].Value = "F8 for Products";
                        dgv_menu.Rows[r].Cells[pnameno].Style.ForeColor = Color.Red;
                    }
                }
                

            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page Product_Fill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }
        
        
       
        private void rb_item_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rb_item.Checked)
                {
                    cmbMenu_category.SelectedIndex = -1;
                    Item_Fill();
                    txtsearchlist.Text = "";

                    if (chk_seleall_pro.Checked)
                        chk_seleall_pro.Checked = false;
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page rb_item_CheckedChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
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
        public void productPopup(int rowno)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                int returnValue = 0;
                DataTable dtColumnAlign = new DataTable();
                dtColumnAlign.Columns.Add();
                DmnSearch _objSearch = new DmnSearch();
                _objSearch.ColumnHeaders = "ID,ItemCode,Product,Price,Category Name";
                _objSearch.Select = "p.ProductId as Id,ItemCode  as Field1,Product  as Field2,Price  as Field3,c.CategoryName  as Field4";
                _objSearch.Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON C.Fcid=p.Fcid LEFT JOIN tblMenu m ON m.ProductId=p.ProductId and m.Status=1";
                _objSearch.WhereNdGroupBy = "where p.Status=1 and ISNULL(m.MId,0)=0";
                _objSearch.OrderBy = " ORDER BY p.Product";

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
                        BllSearch<DmnGridFields> bllSearch_product = new BllSearch<DmnGridFields>();
                        DmnSearch objSearch = new DmnSearch();
                        objSearch.Select = "p.ProductId as Field1,Product as Field2,C.CategoryName AS Field3,p.Price as Field4,P.Fcid AS Field5,p.PreparationMode Field9 ";
                        objSearch.Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON C.Fcid=P.Fcid ";
                        objSearch.WhereNdGroupBy = "Where p.status=1 and  p.productid=" + returnValue;
                        objSearch.OrderBy = "";
                        var itemSource = bllSearch_product.GetSearchData("prdSearch", objSearch);
                        if (itemSource.Count > 0)
                        {
                            for (int i = 0; i < dgv_menu.RowCount; i++)
                            {
                                if (Convert.ToInt32(dgv_menu.Rows[i].Cells[piidno].Value) == Convert.ToInt32(itemSource[0].Field1))
                                {
                                    CMessageBox.Show("Product Already selected", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    dgv_menu.ClearSelection();
                                    dgv_menu.Rows[i].Selected = true;
                                    return;
                                }
                            }
                            int n = dgv_menu.RowCount;
                            dgv_menu.Rows[rowno].Cells[slno].Value = rowno + 1;
                            dgv_menu.Rows[rowno].Cells[piidno].Value = itemSource[0].Field1;
                            dgv_menu.Rows[rowno].Cells[pnameno].Value = itemSource[0].Field2;
                            dgv_menu.Rows[rowno].Cells[pcatno].Value = itemSource[0].Field3;
                            dgv_menu.Rows[rowno].Cells[ppriceno].Value = itemSource[0].Field4;
                            dgv_menu.Rows[rowno].Cells[pfcidno].Value = itemSource[0].Field5;
                            dgv_menu.Rows[rowno].Cells[pmidno].Value = itemSource[0].Field7;
                            dgv_menu.Rows[rowno].Cells[pnameno].Style.ForeColor = Color.Black;
                            dgv_menu.Rows[rowno].Cells[pqtyno].Style.BackColor = Color.LightGray;
                            dgv_menu.Columns[pstatusno].Visible = true;
                            chk_selectall.Visible = true;
                            dgv_menu.Rows[rowno].Cells[proptype].Value = itemSource[0].Field9;

                            if (Convert.ToInt32(itemSource[0].Field9) == Convert.ToInt32(PreparationType.ReadyFood) && itemSource[0].Field9 != null)
                                dgv_menu.Rows[rowno].Cells[proptypename].Value = "Requires cooking";
                            else if (Convert.ToInt32(itemSource[0].Field9) == Convert.ToInt32(PreparationType.FreshCooked) && itemSource[0].Field9 != null)
                            {
                                dgv_menu.Rows[rowno].Cells[proptypename].Value = "Freshly Cooked";
                                //dgv_menu.Rows[rowno].Cells[pqtyno].ReadOnly = true;
                            }
                        }
                        dgv_menu.Rows.Add();
                        int r = dgv_menu.RowCount - 1;
                        dgv_menu.Rows[r].Cells[pnameno].Value = "F8 for Products";
                        dgv_menu.Rows[r].Cells[pnameno].Style.ForeColor = Color.Red;

                    }
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page productPopup()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
            }
        }

        private void dgv_menu_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F8)
                {
                    int rowno = dgv_menu.CurrentRow.Index;
                    if (dgv_menu.Rows[rowno].Cells[piidno].Value == null)
                        productPopup(rowno);
                }
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

        private void dgv_menu_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == pnameno)
            {
                if (dgv_menu.Rows[dgv_menu.CurrentCell.RowIndex].Cells[piidno].Value == null)
                {
                    dgv_menu.Rows[dgv_menu.CurrentCell.RowIndex].Cells[pnameno].Value = "F8 for Products";
                    dgv_menu.Rows[dgv_menu.CurrentCell.RowIndex].Cells[pnameno].Style.ForeColor = Color.Red;
                }
                else
                    dgv_menu.Rows[dgv_menu.CurrentCell.RowIndex].Cells[pnameno].Style.ForeColor = Color.Black;
            }
        }

        private void dgv_menu_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == pnameno)
            //{
            //    if (dgv_menu.Rows[dgv_menu.CurrentCell.RowIndex].Cells[piidno].Value == null)
            //    {
            //        dgv_menu.Rows[dgv_menu.CurrentCell.RowIndex].Cells[pnameno].Value = "";
            //    }
            //}
        }

        private void chk_seleall_pro_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chk_seleall_pro.Checked)
                {
                    BllSearch<DmnGridFields> bllSearch_product = new BllSearch<DmnGridFields>();
                    DmnSearch objSearch = new DmnSearch();
                    objSearch.Select = "p.ProductId as Field1,Product as Field2,C.CategoryName AS Field3,p.Price as Field4,P.Fcid AS Field5,m.Status AS Field6,m.MId Field7,p.PreparationMode Field8,m.Quantity AS Field9,m.DefaultStock AS Field10";
                    objSearch.Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON C.Fcid=P.Fcid LEFT JOIN tblMenu m ON m.ProductId=p.ProductId and m.Status=1";//
                    objSearch.WhereNdGroupBy = "where p.Status=1";
                    objSearch.OrderBy = "order by m.Status desc";
                    var itemSource = bllSearch_product.GetSearchData("prdSearch", objSearch);
                    if (itemSource.Count > 0)
                    {
                        dgv_menu.Rows.Clear();
                        for (int i = 0; i < itemSource.Count; i++)
                        {
                            dgv_menu.Rows.Add();
                            dgv_menu.Rows[i].Cells[slno].Value = i + 1;
                            dgv_menu.Rows[i].Cells[piidno].Value = itemSource[i].Field1;
                            dgv_menu.Rows[i].Cells[pnameno].Value = itemSource[i].Field2;
                            dgv_menu.Rows[i].Cells[pcatno].Value = itemSource[i].Field3;
                            dgv_menu.Rows[i].Cells[ppriceno].Value = itemSource[i].Field4;
                            dgv_menu.Rows[i].Cells[pfcidno].Value = itemSource[i].Field5;
                            dgv_menu.Rows[i].Cells[pmidno].Value = itemSource[i].Field7;
                            dgv_menu.Rows[i].Cells[pqtyno].Value = itemSource[i].Field9;
                            dgv_menu.Rows[i].Cells[pnameno].Style.ForeColor = Color.Black;
                            dgv_menu.Rows[i].Cells[pqtyno].Style.BackColor = Color.LightGray;
                            dgv_menu.Columns[pstatusno].Visible = true;
                            dgv_menu.Rows[i].Cells[pdefstock].Value = itemSource[i].Field10 == null ? (decimal)0 : Convert.ToDecimal(itemSource[i].Field10);

                            dgv_menu.Rows[i].Cells[proptype].Value = itemSource[i].Field8;
                            if (Convert.ToInt32(itemSource[i].Field8) == Convert.ToInt32(PreparationType.ReadyFood) && itemSource[i].Field8 != null)
                                dgv_menu.Rows[i].Cells[proptypename].Value = "Requires cooking";
                            else if (Convert.ToInt32(itemSource[i].Field8) == Convert.ToInt32(PreparationType.FreshCooked) && itemSource[i].Field8 != null)
                            {
                                //dgv_menu.Rows[i].Cells[pqtyno].ReadOnly = true;
                                dgv_menu.Rows[i].Cells[proptypename].Value = "Freshly Cooked";
                            }

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
                        chk_selectall.Checked = false;
                        int checkBoxX = headerCellRect.X + (headerCellRect.Width - chk_selectall.Width) / 2;
                        Point headerCellLocation = this.dgv_menu.GetCellDisplayRectangle(pstatusno, -1, true).Location;
                        chk_selectall.Location = new Point(checkBoxX, headerCellLocation.Y + 40);
                        chk_selectall.BackColor = Color.White;
                        chk_selectall.Size = new Size(18, 18);
                        chk_selectall.Click += new EventHandler(chk_selectall_Clicked);
                        dgv_menu.Controls.Add(chk_selectall);

                    }
                }
                else
                {
                    // Product_Fill();
                }
            }
            catch (Exception ex)
            {
                //CMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("Menu page chk_seleall_pro_CheckedChanged()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.Menu));
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
                MessageBox.Show("Menu item successfully "+ row.Cells[pactiveinactiveno].Value, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            catch (Exception ex)
            {

            }

        }

        private void btnresetstock_Click(object sender, EventArgs e)
        {
            Product_Fill(1);
        }

        private void tlp_button_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
