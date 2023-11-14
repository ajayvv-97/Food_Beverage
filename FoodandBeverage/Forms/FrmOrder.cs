using BLL;
using CustomMessageBox;
using Domain;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
namespace FoodandBeverage.Forms
{
    public partial class FrmOrder : Form
    {
        public FrmOrder()
        {
            InitializeComponent();
        }
        // Function to create a rounded rectangle region
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        #region Declaration

        #region Grid Declaration 
        // Registration Grid
        public static int islno = 0;
        public static int ibarcode = 1;
        public static int iname = 2;
        public static int iunit = 3;
        public static int iqty = 4;
        public static int iprate = 5;
        public static int iamount = 6;
        public static int idisper = 7;
        public static int idisamt = 8;
        public static int itaxper = 9;
        public static int itaxamt = 10;
        public static int itotal = 11;
        public static int iitemid = 12;
        public static int itax = 13;
        public static int itaxtype = 14;
        public static int israte = 15;
        public static int imrp = 16;
        public static int itype = 17;
        public static int ibarcode2 = 18;
        public static int itaxsettingid = 19;
        public static int iunitid = 20;
        public static int ipridno = 21;
        #endregion
        TableLayoutPanel tlp_table_dashboard_main = new TableLayoutPanel();
        TableLayoutPanel tlp_table_dashboard = new TableLayoutPanel();
        TableLayoutPanel tlp_searchproduct_sidebar = new TableLayoutPanel();
        TableLayoutPanel tlp_searchproduct_dashboard = new TableLayoutPanel();

        Color product_buttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);
        Color seleproduct_buttoncolor = Color.FromArgb(0x42, 0x67, 0xB2);

        int currentRowCount = 0;
        #endregion

        private void btn_tableorder_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmOrder_Load(object sender, EventArgs e)
        {
            try
            {
                Dgv_Selected_Item.BorderStyle = BorderStyle.None;
                Dgv_Selected_Item.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
                Dgv_Selected_Item.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                Dgv_Selected_Item.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
                Dgv_Selected_Item.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
                Dgv_Selected_Item.BackgroundColor = Color.White;

                Dgv_Selected_Item.EnableHeadersVisualStyles = false;
                Dgv_Selected_Item.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                Dgv_Selected_Item.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
                Dgv_Selected_Item.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                textb_shape();
                txtname_search.Text = "Food Name .....";
                txtitemcode_serach.Text = "Item Code .....";

                #region Side Bar for Search box
                //sidebar 
                pnlsidebar_searchproduct.Controls.Clear();
                tlp_searchproduct_sidebar.Controls.Clear();
                tlp_searchproduct_sidebar.Dock = DockStyle.Fill;
                tlp_searchproduct_sidebar.AutoScroll = true;
                BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblCategory_FTI";
                dmnSearch.WhereNdGroupBy = "where Status=1 and CategoryType=0";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    int rowIndex = 0;
                    // Configure the Button
                    Button buttonfav = new Button();
                    buttonfav.Text = "Favouriate Item";
                    buttonfav.Name = "btn_fav";
                    buttonfav.Tag = 0;
                    buttonfav.BackColor = Color.Beige;
                    buttonfav.Width = 145;
                    buttonfav.Height = 40;

                    buttonfav.Font = new Font(buttonfav.Font.FontFamily, 8, FontStyle.Bold);
                    buttonfav.Click += new EventHandler(SidebuttonfavClickEventHandler);
                    tlp_searchproduct_sidebar.Controls.Add(buttonfav, 0, rowIndex);
                    rowIndex++;

                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        // Configure the Button
                        Button button = new Button();
                        button.Text = gridSource[i].CategoryName;
                        button.Name = "btn" + gridSource[i].CategoryName;
                        button.Tag = gridSource[i].Fcid;
                        button.BackColor = Color.Beige;
                        button.Width = 145;
                        button.Height = 40;

                        button.Font = new Font(button.Font.FontFamily, 8, FontStyle.Bold);
                        button.Click += new EventHandler(SidebuttonClickEventHandler);
                        tlp_searchproduct_sidebar.Controls.Add(button, 0, rowIndex);
                        rowIndex++;
                    }
                    pnlsidebar_searchproduct.Controls.Add(tlp_searchproduct_sidebar);
                    pnl_toggle_btn.Dock = DockStyle.Fill;
                    btn_sidebar_toggle.Location = new Point(pnl_toggle_btn.Width / 2 - btn_sidebar_toggle.Size.Width / 2, pnl_toggle_btn.Height / 2 - btn_sidebar_toggle.Size.Height / 2);
                }
                #endregion
            }
            catch (Exception ex)
            {

            }
            
        }
        private void SidebuttonClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblProduct p LEFT JOIN tblMenu m on m.ProductId=p.ProductId ";
                dmnSearch.WhereNdGroupBy = " where p.Status=1 and p.Fcid= " + clickedControl.Tag + "and m.Status=1";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                pnl_serachproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                tlp_searchproduct_dashboard.AutoScroll = true;
                if (gridSource.Count > 0)
                {
                    int rowIndex = 1;
                    int columnIndex = 0;
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        if (columnIndex == 3)
                        {
                            columnIndex = 0;
                            rowIndex++;
                        }
                        // Configure the Button
                        Button button = new Button();
                        button.Text = gridSource[i].Product;
                        button.Name = "btn" + gridSource[i].Product;
                        button.Tag = gridSource[i].ProductId;
                        button.Width = 160;
                        button.Height = 55;

                        // Set the FlatAppearance properties to remove the border
                        button.FlatAppearance.BorderSize = 0; // Set border width to 0
                        button.FlatAppearance.BorderColor = button.BackColor;
                        button.FlatStyle = FlatStyle.Flat;
                        button.BackColor = product_buttoncolor;
                        button.ForeColor = Color.White;

                        //// Create a circular Region
                        button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                        button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);
                        //button.Dock = DockStyle.Fill;
                        button.Click += new EventHandler(DashboardButtonClickEventHandler);
                        tlp_searchproduct_dashboard.Controls.Add(button, columnIndex, rowIndex);
                        columnIndex++;
                    }
                    pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void DashboardButtonClickEventHandler(object sender, EventArgs e)
        {
            //item Chosse from serach Box 
            try
            {
                Control clickedControl = sender as Control;
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblProduct";
                dmnSearch.WhereNdGroupBy = "where Status=1 and ProductId = "+ clickedControl.Tag;
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                if (gridSource.Count > 0)
                {
                    string tlpname = "tlp_seletedItem_col" + clickedControl.Tag;
                    TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                    if (tlp_sele != null)
                    {                      
                        string txtname = "txtqty_" + clickedControl.Tag;
                        TextBox qty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                        int cur_qty = Convert.ToInt32(qty.Text) + 1;
                        qty.Text = Convert.ToString(cur_qty);
                        CalcTotalAmt_Add(Convert.ToInt32(tlp_sele.Tag), 1);
                    }
                    else
                    {
                        #region Grid Inseration
                        int r1=0;
                        if (r1 == Dgv_Selected_Item.RowCount - 1)
                        {
                            Dgv_Selected_Item.Rows.Add(1);
                        }
                        Dgv_Selected_Item.Rows[r1].Cells[islno].Value = r1 + 1;
                        Dgv_Selected_Item.Rows[r1].Cells[ibarcode].Value = gridSource[0].ItemCode;
                        Dgv_Selected_Item.Rows[r1].Cells[ibarcode2].Value = gridSource[0].ItemCode;
                        Dgv_Selected_Item.Rows[r1].Cells[iname].Value = gridSource[0].Product;
                        //Dgv_Selected_Item.Rows[r1].Cells[iunit].Value = gridSource[0].UnitName;
                        //Dgv_Selected_Item.Rows[r1].Cells[iunitid].Value = gridSource[0].unit;
                        Dgv_Selected_Item.Rows[r1].Cells[iqty].Value = "1";
                        Dgv_Selected_Item.Rows[r1].Cells[iprate].Value = gridSource[0].Price;
                        Dgv_Selected_Item.Rows[r1].Cells[israte].Value = gridSource[0].Price;
                        Dgv_Selected_Item.Rows[r1].Cells[imrp].Value = gridSource[0].MRP;
                        Dgv_Selected_Item.Rows[r1].Cells[idisper].Value = "0";
                        Dgv_Selected_Item.Rows[r1].Cells[idisamt].Value = "0";
                        if (gridSource[0].TaxType == "GST")
                        {
                            Dgv_Selected_Item.Rows[r1].Cells[itaxper].Value = gridSource[0].IgstPrcnt;
                        }
                        if (gridSource[0].TaxType == "VAT")
                        {
                            Dgv_Selected_Item.Rows[r1].Cells[itaxper].Value = gridSource[0].VatPrcnt;
                        }
                        Dgv_Selected_Item.Rows[r1].Cells[iitemid].Value = gridSource[0].ProductId;
                        Dgv_Selected_Item.Rows[r1].Cells[itax].Value = gridSource[0].Tax;
                        Dgv_Selected_Item.Rows[r1].Cells[itaxtype].Value = gridSource[0].TaxType;
                        Dgv_Selected_Item.Rows[r1].Cells[itype].Value = gridSource[0].Fcid;
                        Dgv_Selected_Item.Rows[r1].Cells[itaxsettingid].Value = gridSource[0].TaxSettingsId;
                        //Dgv_Selected_Item.Rows[r1].Cells[ipridno].Value = gridSource[0].PrId;
                        #endregion


                        // Configure the TableLayoutPanel
                        TableLayoutPanel tlp_seletedItem_col = new TableLayoutPanel();
                        tlp_seletedItem_col.Name = "tlp_seletedItem_col" + clickedControl.Tag;
                        tlp_seletedItem_col.Tag = clickedControl.Tag;
                        tlp_seletedItem_col.Dock = DockStyle.Top;
                        tlp_seletedItem_col.BackColor = Color.LightBlue;
                        tlp_seletedItem_col.Height = 50;

                        // Configure the Button for Item name
                        Button buttonitemname = new Button();
                        buttonitemname.Text = gridSource[0].Product;
                        buttonitemname.Name = "buttonitemname_" + gridSource[0].Product; ;
                        buttonitemname.Tag = gridSource[0].ProductId; ;
                        buttonitemname.BackColor = Color.Beige;
                        buttonitemname.Width = 220;
                        buttonitemname.Height = 40;
                        buttonitemname.Font = new Font(buttonitemname.Font.FontFamily, 8, FontStyle.Bold);
                        // buttonitemname.Click += new EventHandler(SidebuttonitemnameClickEventHandler);

                        // Configure the Button for add Qty
                        Button buttonAdd = new Button();
                        buttonAdd.Text = "+";
                        buttonAdd.Name = "buttonAdd_" + gridSource[0].ProductId;
                        buttonAdd.Tag = gridSource[0].ProductId;
                        buttonAdd.BackColor = Color.Beige;
                        buttonAdd.Width = 30;
                        buttonAdd.Height = 30;
                        buttonAdd.Margin = new Padding(0, 8, 0, 0);
                        buttonAdd.Font = new Font(buttonAdd.Font.FontFamily, 8, FontStyle.Bold);
                        buttonAdd.Click += new EventHandler(ButtonbuttonAddClickEventHandler);

                        // Configure the textbox enter QTY
                        TextBox txtqty = new TextBox();
                        txtqty.Text = "1";
                        txtqty.Name = "txtqty_" + gridSource[0].ProductId;
                        txtqty.Tag = gridSource[0].ProductId;
                        txtqty.BackColor = Color.Beige;
                        txtqty.Width = 80;
                        txtqty.Height = 30;
                        txtqty.TextAlign = HorizontalAlignment.Center;
                        txtqty.Margin = new Padding(0, 14, 0, 0);
                        txtqty.Font = new Font(txtqty.Font.FontFamily, 8, FontStyle.Bold);

                        // Configure the textbox enter QTY
                        Button buttonRemove = new Button();
                        buttonRemove.Text = "-";
                        buttonRemove.Name = "btn_remove_" + gridSource[0].ProductId;
                        buttonRemove.Tag = gridSource[0].ProductId;
                        buttonRemove.BackColor = Color.Beige;
                        buttonRemove.Width = 30;
                        buttonRemove.Height = 30;
                        buttonRemove.Margin = new Padding(0, 8, 0, 0);
                        buttonRemove.Font = new Font(buttonRemove.Font.FontFamily, 8, FontStyle.Bold);
                        buttonRemove.Click += new EventHandler(ButtonRemoveClickEventHandler);

                        // Configure the textbox Price
                        TextBox txtprice = new TextBox();
                        txtprice.Text = Convert.ToString(gridSource[0].Price);
                        txtprice.Name = "txtprice_" + gridSource[0].ProductId;
                        txtprice.Tag = gridSource[0].ProductId;
                        txtprice.BackColor = Color.Beige;
                        txtprice.Width = 120;
                        txtprice.Height = 30;
                        txtprice.TextAlign = HorizontalAlignment.Center;
                        txtprice.Margin = new Padding(0, 14, 0, 0);
                        txtprice.Font = new Font(txtprice.Font.FontFamily, 8, FontStyle.Bold);

                        // Configure Item Delete Button 
                        PictureBox pb_item_delete = new PictureBox();
                        pb_item_delete.Name = "btn_remove_" + gridSource[0].ProductId;
                        pb_item_delete.Tag = gridSource[0].ProductId;
                        pb_item_delete.BackColor = Color.Beige;
                        pb_item_delete.Width = 30;
                        pb_item_delete.Height = 30;
                        pb_item_delete.BackColor = Color.Transparent;
                        pb_item_delete.Margin = new Padding(9, 8, 0, 0);
                        pb_item_delete.Font = new Font(pb_item_delete.Font.FontFamily, 8, FontStyle.Bold);
                        pb_item_delete.Click += new EventHandler(pb_item_deleteClickEventHandler);
                        pb_item_delete.BackgroundImage = FoodandBeverage.Properties.Resources.Delete_Grid;
                        pb_item_delete.BackgroundImageLayout = ImageLayout.Zoom; // Use Zoom instead of Stretch


                        // Get the current row count
                        currentRowCount = tlp_seletedItem_col.RowCount;

                        // Add controls to TableLayoutPanel at the next row
                        tlp_seletedItem_col.Controls.Add(buttonitemname, 0, currentRowCount);
                        tlp_seletedItem_col.Controls.Add(buttonAdd, 1, currentRowCount);
                        tlp_seletedItem_col.Controls.Add(txtqty, 2, currentRowCount);
                        tlp_seletedItem_col.Controls.Add(buttonRemove, 3, currentRowCount);
                        tlp_seletedItem_col.Controls.Add(txtprice, 4, currentRowCount);
                        tlp_seletedItem_col.Controls.Add(pb_item_delete, 5, currentRowCount);

                        // Increment the row count
                        tlp_seletedItem_col.RowCount = currentRowCount + 1;
                        // Add the new TableLayoutPanel to tlp_seletedItem_main
                        tlp_seletedItem_main.Controls.Add(tlp_seletedItem_col);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void pb_item_deleteClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                string txtname = "txtqty_" + clickedControl.Tag;
                string tlpname = "tlp_seletedItem_col" + clickedControl.Tag;
                // Find the TableLayoutPanel control by name
                TextBox txtqty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                if (txtqty != null)
                {
                    int cur_qty = 0;
                    TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                    if (tlp_sele != null)
                        CalcTotalAmtCombo_remove(Convert.ToInt32(tlp_sele.Tag), cur_qty);
                    foreach (Control control in tlp_seletedItem_main.Controls)
                    {
                        if (control.Name == tlpname)
                        {
                            tlp_seletedItem_main.Controls.Remove(control);
                            break;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
            }
        }
        private void ButtonRemoveClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                string txtname = "txtqty_" + clickedControl.Tag;
                string tlpname = "tlp_seletedItem_col" + clickedControl.Tag;
                // Find the TableLayoutPanel control by name
                TextBox txtqty = this.Controls.Find(txtname, true).FirstOrDefault() as TextBox;
                if (txtqty != null)
                {
                    int cur_qty = Convert.ToInt32(txtqty.Text) - 1;
                    if (cur_qty == 0)
                    {
                        TableLayoutPanel tlp_sele = this.Controls.Find(tlpname, true).FirstOrDefault() as TableLayoutPanel;
                        if (tlp_sele != null)
                            CalcTotalAmtCombo_remove(Convert.ToInt32(tlp_sele.Tag), cur_qty);
                        foreach (Control control in tlp_seletedItem_main.Controls)
                        {
                            if (control.Name == tlpname)
                            {
                                tlp_seletedItem_main.Controls.Remove(control);
                                break;
                            }
                        }

                    }
                    else
                    {
                        txtqty.Text = Convert.ToString(cur_qty);
                        CalcTotalAmtCombo_remove(Convert.ToInt32(clickedControl.Tag), cur_qty);
                    }

                }
            }
            catch (Exception ex)
            {
            }
        }
        private void ButtonbuttonAddClickEventHandler(object sender, EventArgs e)
        {
            try
            {

                Control clickedControl = sender as Control;
                string txtqty = "txtqty_" + clickedControl.Tag;
                string buttonname = "btn_sele_" + clickedControl.Tag;
                // Find the TableLayoutPanel control by name
                TextBox qty = this.Controls.Find(txtqty, true).FirstOrDefault() as TextBox;
                if (qty != null)
                {
                    int cur_qty = Convert.ToInt32(qty.Text) + 1;
                    qty.Text = Convert.ToString(cur_qty);
                    CalcTotalAmt_Add(Convert.ToInt32(clickedControl.Tag), cur_qty);
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        public void CalcTotalAmt_Add(int pid, int qty)
        {
            try
            {
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblProduct";
                objSearch.WhereNdGroupBy = "where ProductId=" + pid;
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    if (string.IsNullOrEmpty(txtTotalAmount.Text))
                        txtTotalAmount.Text = "0";
                    //for (int i = 0; i < dgv_sele_cmb_products.Rows.Count; i++)
                    //{
                    //    if (Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[pidno].Value) == pid)
                    //    {
                    //        dgv_sele_cmb_products.Rows[i].Cells[qtynoo].Value = qty;
                    //        break;
                    //    }
                    //    else
                    //    {
                    //        int rowIndex = dgv_sele_cmb_products.Rows.Count - 1;
                    //        dgv_sele_cmb_products.Rows[rowIndex].Cells[qtynoo].Value = qty;
                    //        break;
                    //    }
                    //}
                    decimal totalamt = Convert.ToDecimal(gridSource[0].Price) + Convert.ToDecimal(txtTotalAmount.Text);
                    txtTotalAmount.Text = Convert.ToString(totalamt);
                }
            }
            catch (Exception ex)
            {

            }

        }
        public void CalcTotalAmtCombo_remove(int pid, int qty)
        {
            try
            {
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch objSearch = new DmnSearch();
                objSearch = new DmnSearch();
                objSearch.Select = "*";
                objSearch.Table = "tblProduct";
                objSearch.WhereNdGroupBy = "where ProductId=" + pid;
                objSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
                if (gridSource.Count > 0)
                {
                    bool flag = true;
                    if (string.IsNullOrEmpty(txtTotalAmount.Text))
                        txtTotalAmount.Text = "0";

                    //for (int i = 0; i < dgv_sele_cmb_products.Rows.Count; i++)
                    //{
                    //    if (Convert.ToInt32(dgv_sele_cmb_products.Rows[i].Cells[pidno].Value) == pid)
                    //    {
                    //        flag = false;
                    //        if (qty == 0)
                    //        {
                    //            dgv_sele_cmb_products.Rows[i].Cells[qtynoo].Value = qty;
                    //            dgv_sele_cmb_products.Rows[i].Cells[statusno].Value = 0;
                    //            break;
                    //        }
                    //        else
                    //        {
                    //            dgv_sele_cmb_products.Rows[i].Cells[qtynoo].Value = qty;
                    //            break;
                    //        }
                    //    }
                    //}
                    //if (flag)
                    //{
                    //    int rowIndex = dgv_sele_cmb_products.Rows.Count - 1;
                    //    dgv_sele_cmb_products.Rows[rowIndex].Cells[qtynoo].Value = qty;
                    //}
                    decimal totalamt = Convert.ToDecimal(txtTotalAmount.Text) - Convert.ToDecimal(gridSource[0].Price);
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void SidebuttonfavClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                DmnSearch dmnSearch = new DmnSearch();
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblProduct p LEFT JOIN tblMenu m on m.ProductId=p.ProductId ";
                dmnSearch.WhereNdGroupBy = " where p.Status=1 and p.Fcid= " + clickedControl.Tag + "and m.Status=1";
                dmnSearch.OrderBy = "";
                var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                pnl_serachproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Controls.Clear();
                tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                tlp_searchproduct_dashboard.AutoScroll = true;
                if (gridSource.Count > 0)
                {
                    int rowIndex = 1;
                    int columnIndex = 0;
                    for (int i = 0; i < gridSource.Count; i++)
                    {
                        if (columnIndex == 3)
                        {
                            columnIndex = 0;
                            rowIndex++;
                        }

                        // Configure the Button
                        Button button = new Button();
                        button.Text = gridSource[i].Product;
                        button.Name = "btn" + gridSource[i].Product;
                        button.Tag = gridSource[i].ProductId;
                        button.Width = 160;
                        button.Height = 55;

                        // Set the FlatAppearance properties to remove the border
                        button.FlatAppearance.BorderSize = 0; // Set border width to 0
                        button.FlatAppearance.BorderColor = button.BackColor;
                        button.FlatStyle = FlatStyle.Flat;
                        button.BackColor = product_buttoncolor;
                        button.ForeColor = Color.White;

                        //// Create a circular Region
                        button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                        //System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                        //path.AddEllipse(0, 0, button.Width, button.Height);
                        //button.Region = new Region(path);

                        button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);
                        //button.Dock = DockStyle.Fill;
                        button.Click += new EventHandler(DashboardButtonClickEventHandler);
                        tlp_searchproduct_dashboard.Controls.Add(button, columnIndex, rowIndex);
                        columnIndex++;
                    }
                    pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                }
                //else
                //{
                //    CMessageBox.Show("No Products On This Category", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
            }
            catch (Exception ex)
            {
            }
        }

       


 private void txtname_search_Enter(object sender, EventArgs e)
        {
            if (txtname_search.Text == "Food Name .....")
            {
                txtname_search.Text = "";
                txtname_search.ForeColor = Color.Black;
            }

        }

        private void txtitemcode_serach_Enter(object sender, EventArgs e)
        {
            if (txtitemcode_serach.Text == "Item Code .....")
            {
                txtitemcode_serach.Text = "";
                txtitemcode_serach.ForeColor = Color.Black;
            }
        }

        private void txtname_search_Leave(object sender, EventArgs e)
        {
            if (txtname_search.Text == "")
            {
                txtname_search.Text = "Food Name .....";
                txtname_search.ForeColor = Color.Black;
            }
        }

        private void txtitemcode_serach_Leave(object sender, EventArgs e)
        {
            if (txtitemcode_serach.Text == "")
            {
                txtitemcode_serach.Text = "Item Code .....";
                txtitemcode_serach.ForeColor = Color.Black;
            }
        }

        private void txtname_search_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "*";
                    dmnSearch.Table = "tblProduct p LEFT JOIN tblMenu m on m.ProductId = p.ProductId";
                    dmnSearch.WhereNdGroupBy = " where p.Status = 1 and m.Status = 1 and Product LIKE '%" + txtname_search.Text + "%'";
                    dmnSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    pnl_serachproduct_dashboard.Controls.Clear();
                    tlp_searchproduct_dashboard.Controls.Clear();
                    tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                    tlp_searchproduct_dashboard.AutoScroll = true;
                    if (gridSource.Count > 0)
                    {
                        int rowIndex = 1;
                        int columnIndex = 0;
                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            if (columnIndex == 3)
                            {
                                columnIndex = 0;
                                rowIndex++;
                            }
                            // Configure the Button
                            Button button = new Button();
                            button.Text = gridSource[i].Product;
                            button.Name = "btn" + gridSource[i].Product;
                            button.Tag = gridSource[i].ProductId;
                            button.Width = 160;
                            button.Height = 55;

                            // Set the FlatAppearance properties to remove the border
                            button.FlatAppearance.BorderSize = 0; // Set border width to 0
                            button.FlatAppearance.BorderColor = button.BackColor;
                            button.FlatStyle = FlatStyle.Flat;
                            button.BackColor = product_buttoncolor;
                            button.ForeColor = Color.White;

                            // Create a circular Region
                            //System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                            //path.AddEllipse(0, 0, button.Width, button.Height);
                            //button.Region = new Region(path);
                            button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));

                            button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);
                            //button.Dock = DockStyle.Fill;
                            button.Click += new EventHandler(DashboardButtonClickEventHandler);
                            tlp_searchproduct_dashboard.Controls.Add(button, columnIndex, rowIndex);
                            columnIndex++;
                        }
                        pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                    }
                    else
                        CMessageBox.Show("No Products", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtitemcode_serach_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BllSearch<DmnProducts> bllSearch = new BllSearch<DmnProducts>();
                    DmnSearch dmnSearch = new DmnSearch();
                    dmnSearch.Select = "*"; 
                    dmnSearch.Table = "tblProduct p LEFT JOIN tblMenu m on m.ProductId = p.ProductId";
                    dmnSearch.WhereNdGroupBy = " where p.Status = 1 and m.Status = 1 and ItemCode = '" + txtitemcode_serach.Text + "'";
                    dmnSearch.OrderBy = "";
                    var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
                    pnl_serachproduct_dashboard.Controls.Clear();
                    tlp_searchproduct_dashboard.Controls.Clear();
                    tlp_searchproduct_dashboard.Dock = DockStyle.Fill;
                    tlp_searchproduct_dashboard.AutoScroll = true;
                    if (gridSource.Count > 0)
                    {
                        int rowIndex = 1;
                        int columnIndex = 0;
                        for (int i = 0; i < gridSource.Count; i++)
                        {
                            if (columnIndex == 3)
                            {
                                columnIndex = 0;
                                rowIndex++;
                            }
                            // Configure the Button
                            Button button = new Button();
                            button.Text = gridSource[i].Product;
                            button.Name = "btn" + gridSource[i].Product;
                            button.Tag = gridSource[i].ProductId;
                            button.Width = 160;
                            button.Height = 55;

                            // Set the FlatAppearance properties to remove the border
                            button.FlatAppearance.BorderSize = 0; // Set border width to 0
                            button.FlatAppearance.BorderColor = button.BackColor;
                            button.FlatStyle = FlatStyle.Flat;
                            button.BackColor = product_buttoncolor;
                            button.ForeColor = Color.White;

                            // Create a circular Region
                            button.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, button.Width, button.Height, 10, 10));
                            //System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                            //path.AddEllipse(0, 0, button.Width, button.Height);
                            //button.Region = new Region(path);

                            button.Font = new Font(button.Font.FontFamily, 10, FontStyle.Bold);
                            //button.Dock = DockStyle.Fill;
                            button.Click += new EventHandler(DashboardButtonClickEventHandler);
                            tlp_searchproduct_dashboard.Controls.Add(button, columnIndex, rowIndex);
                            columnIndex++;
                        }
                        pnl_serachproduct_dashboard.Controls.Add(tlp_searchproduct_dashboard);
                    }
                    else
                        CMessageBox.Show("No Products", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {

            }
        }

        private void txtname_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }

        private void txtitemcode_serach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r') // Check for Enter key
            {
                e.Handled = true; // Suppress the Enter key
            }
        }

        private void btn_sidebar_toggle_Click(object sender, EventArgs e)
        {
            if (pnlsidebar_searchproduct.Visible)
            {
                pnlsidebar_searchproduct.Visible = false;
                lblcategory.Visible = false;
                textb_shape();
            }
            else
            {
                pnlsidebar_searchproduct.Visible = true;
                lblcategory.Visible = true;
            }
        }
        #region TexBox Round function
        public void textb_shape()
        {
            try
            {
                // Apply rounded corners to text boxes based on the list
                foreach (TextBox textBox in new TextBox[] { txtitemcode_serach, txtname_search })
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }



        private void tlp_payment_outerdesign_Paint(object sender, PaintEventArgs e)
        {
            float borderRadius = 10f; // adjust this value to change the roundness of the corners
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(tlp_payment_outerdesign.ClientRectangle.X, tlp_payment_outerdesign.ClientRectangle.Y, tlp_payment_outerdesign.ClientRectangle.Width, tlp_payment_outerdesign.ClientRectangle.Height);
            path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
            path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
            path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
            path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
            path.CloseFigure();
            tlp_payment_outerdesign.Region = new Region(path);
        }

        private void tlp_seletedItem_main_Paint(object sender, PaintEventArgs e)
        {
            float borderRadius = 10f; // adjust this value to change the roundness of the corners
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = new RectangleF(tlp_seletedItem_main.ClientRectangle.X, tlp_seletedItem_main.ClientRectangle.Y, tlp_seletedItem_main.ClientRectangle.Width, tlp_seletedItem_main.ClientRectangle.Height);
            path.AddArc(rect.X, rect.Y, borderRadius * 2, borderRadius * 2, 180, 90);
            path.AddLine(rect.X + borderRadius, rect.Y, rect.Right - borderRadius, rect.Y);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y, borderRadius * 2, borderRadius * 2, 270, 90);
            path.AddLine(rect.Right, rect.Y + borderRadius, rect.Right, rect.Bottom - borderRadius);
            path.AddArc(rect.X + rect.Width - borderRadius * 2, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 0, 90);
            path.AddLine(rect.Right - borderRadius, rect.Bottom, rect.X + borderRadius, rect.Bottom);
            path.AddArc(rect.X, rect.Y + rect.Height - borderRadius * 2, borderRadius * 2, borderRadius * 2, 90, 90);
            path.AddLine(rect.X, rect.Bottom - borderRadius, rect.X, rect.Y + borderRadius);
            path.CloseFigure();
            tlp_seletedItem_main.Region = new Region(path);
        }


    }
}
