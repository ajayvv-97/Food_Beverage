
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
using System.Text;
using System.Windows.Forms;
using static FoodandBeverage.Interfaces.Interfaces;

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
        public static int pmidno = 1;
        public static int piidno = 2;
        public static int pnameno = 3;
        public static int pcatno = 4;
        public static int ppriceno = 5;
        public static int pstatusno = 6;
        public static int pfcidno = 7;

        #endregion

        public string Compressedimg = Application.StartupPath + "\\CompressedImg";
        ClsDocument clsDocument = new ClsDocument();
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
                cmbMenu_category.DataSource = gridSource;
                cmbMenu_category.ValueMember = "Id";
                cmbMenu_category.DisplayMember = "Value";

                cmbSearchlist.DataSource = gridSource;
                cmbSearchlist.ValueMember = "Id";
                cmbSearchlist.DisplayMember = "Value";

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
                    DmnMenu usertype = new DmnMenu();
                    
                    BllMenu bllmenu = new BllMenu();
                    int count = 0;
                    for (int i = 0; i < dgv_menu.RowCount; i++)
                    {
                        if ( ((dgv_menu.Rows[i].Cells[piidno].Value) != null || (dgv_menu.Rows[i].Cells[piidno].Value) != ""))//&&(Convert.ToBoolean(dgv_menu.Rows[i].Cells[pstatusno].Value) == true)

                        {
                            dmnmenu.MId = Convert.ToInt32(dgv_menu.Rows[i].Cells[pmidno].Value);
                            dmnmenu.ProductId = Convert.ToInt32(dgv_menu.Rows[i].Cells[piidno].Value);
                            dmnmenu.Name = Convert.ToString(dgv_menu.Rows[i].Cells[pnameno].Value);
                            dmnmenu.Fcid = Convert.ToInt32(dgv_menu.Rows[i].Cells[pfcidno].Value);
                            dmnmenu.CreatedBy = 1;
                            dmnmenu.BranchId = 1;
                            if (Convert.ToBoolean(dgv_menu.Rows[i].Cells[pstatusno].Value) ==true)
                                dmnmenu.Status =1;
                            else
                                dmnmenu.Status = 0;
                            usertype = bllmenu.AddData("prdInsert_upd_menu", dmnmenu);
                            count++;
                        }
                    }
                    if (count== dgv_menu.RowCount)
                    {
                        CMessageBox.Show(" Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        clearall();
                        Fillmenu(0);
                    }
                    else
                        CMessageBox.Show(" Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }                
            }
            catch (Exception ex)
            {

            }
        }

        public void Fillmenu(int searchmode)
        {
            try
            {
                BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
                DmnSearch objSearch = new DmnSearch();
                objSearch.Select = "p.ProductId as Field1,Product as Field2,C.CategoryName AS Field3,Price as Field4,P.Fcid AS Field5 ,m.Status AS Field6,m.Mid AS Field7";
                objSearch.Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON C.Fcid=P.Fcid LEFT JOIN tblMenu m ON m.ProductId=p.ProductId";
                if(searchmode == 0 || searchmode == -1)
                    objSearch.WhereNdGroupBy = "Where P.status=1";
                else
                    objSearch.WhereNdGroupBy = "Where P.status=1 and P.Fcid=" + searchmode;

                objSearch.OrderBy = "order by Product";
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
                        dgv_menu.Rows[i].Cells[pfcidno].Value = itemSource[i].Field5;
                        dgv_menu.Rows[i].Cells[pmidno].Value = itemSource[i].Field7;

                        if (Convert.ToInt32(itemSource[i].Field6)==1)
                            dgv_menu.Rows[i].Cells[pstatusno].Value = true;
                        else
                            dgv_menu.Rows[i].Cells[pstatusno].Value = false;
                    }
                }
                else
                    CMessageBox.Show("No Products For This Category");
            }
            catch (Exception ex)
            {

            }

        }
        public bool MandatoryCheck()
        {
            //if (txtitemname.Text == "" || txtitemname.Text == null)
            //{
            //    CMessageBox.Show("Please Enter Food Name", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtitemname.Focus();
            //    return false;
            //}
            //else 
            if (Convert.ToInt32(cmbMenu_category.SelectedValue) == 0)
            {
                CMessageBox.Show("Please Select Food Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbMenu_category.Focus();
                return false;
            }
            
            return true;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            int returnValue = 0;
            DataTable dtColumnAlign = new DataTable();
            dtColumnAlign.Columns.Add();
            DmnSearch _objSearch = new DmnSearch();
            _objSearch.ColumnHeaders = "ID,ItemCode,Product";
            _objSearch.Select = "ProductId as Id,ItemCode  as Field1,Product  as Field2";
            _objSearch.Table = "tblProduct";
            _objSearch.WhereNdGroupBy = "where Status = 1";
            _objSearch.OrderBy = " ORDER BY Product";
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
                    
                    for(int i=0;i<dgv_menu.Rows.Count;i++)
                    {
                        if(Convert.ToInt32(dgv_menu.Rows[i].Cells[piidno].Value)== returnValue)                        
                            dgv_menu.Rows[i].Selected = true;
                        else
                            dgv_menu.Rows[i].Selected = false;

                    }
                }
            }
        }

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            clearall();
            Fillmenu(0);           
        }
        public void clearall()
        {
            cmbSearchlist.SelectedIndex = -1;
            txtsearch.Text = "";
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            Fillmenu(0);
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
            catch(Exception ex)
            {

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
                    string[] infoParts = info.Split(',');
                    string tooltipText = "";
                    tooltipText = $"Name : {infoParts[0]}{Environment.NewLine}price :{infoParts[1]}{Environment.NewLine}Mrp :{infoParts[2]}{Environment.NewLine}Tax :{infoParts[3]}{Environment.NewLine}Tax Type :{infoParts[4]}{Environment.NewLine}Discount % :{infoParts[5]}{Environment.NewLine}Discount Amount :{infoParts[6]}";
                    toolTip_gallery.Show(tooltipText, label);
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
                // Handle exceptions if necessary
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
                    ///FillData(Convert.ToInt32(pid));
                    CMessageBox.Show("Clicked Table ID: " + pid);
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void TlpTableInner_MouseEnter(object sender, EventArgs e)
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

        private void TlpTableInner_MouseLeave(object sender, EventArgs e)
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
        #endregion


        private void Btn_view_menu_Click(object sender, EventArgs e)
        {
            this.Width = 1025;
            this.Height = 617;
            tlp_registartion_main.Visible = false;
            tlp_menu_Gallery_main.Visible = true;
            Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_main.Controls.Add(tlp_menu_Gallery_main, 1, 0);
            tlp_menu_Gallery_main.Dock = DockStyle.Fill;
            GalleryView(0);
        } 



        private void cmbMenu_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Fillmenu(Convert.ToInt32(cmbMenu_category.SelectedValue));
        }

        private void btn_bck_reg_Click(object sender, EventArgs e)
        {
            this.Width = 755;
            this.Height = 506;
            tlp_menu_Gallery_main.Visible = false;
            tlp_registartion_main.Visible = true;
            Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_main.Controls.Add(tlp_registartion_main, 1, 0);
            tlp_registartion_main.Dock = DockStyle.Fill;
        }

        private void cmbSearchlist_SelectionChangeCommitted(object sender, EventArgs e)
        {
            comboBoxChanged = true;
            GalleryView(Convert.ToInt32(cmbSearchlist.SelectedValue));
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
            txtsearchlist.Text = "";
            Fillmenu(0); 
        }
        private void btnseach_menu_Click(object sender, EventArgs e)
        {
            if (txtsearchlist.Text == "" || txtsearchlist.Text == null)
            {
                CMessageBox.Show("Please Enter Food Name ");
                txtsearchlist.Focus();
                return;
            }
            BllSearch<DmnGridFields> bllSearch = new BllSearch<DmnGridFields>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = "p.ProductId as Field1,Product as Field2,C.CategoryName AS Field3,Price as Field4,P.Fcid AS Field5 ,m.Status AS Field6,m.Mid AS Field7";
            dmnSearch.Table = "tblProduct p LEFT JOIN tblCategory_FTI c ON C.Fcid=P.Fcid LEFT JOIN tblMenu m ON m.ProductId=p.ProductId";
            dmnSearch.WhereNdGroupBy = "where p.Status=1 and  p.Product LIKE '%" + txtsearchlist.Text + "%'";
            dmnSearch.OrderBy = "order by Product";
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
                    dgv_menu.Rows[i].Cells[pfcidno].Value = itemSource[i].Field5;
                    dgv_menu.Rows[i].Cells[pmidno].Value = itemSource[i].Field7;

                    if (Convert.ToInt32(itemSource[i].Field6) == 1)
                        dgv_menu.Rows[i].Cells[pstatusno].Value = true;
                    else
                        dgv_menu.Rows[i].Cells[pstatusno].Value = false;
                }
            }
            else
                CMessageBox.Show("No Products For This Name");
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
                dmnSearch.Select = "*";
                dmnSearch.Table = "tblmenu m";
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
                    CMessageBox.Show("No Products For This Category");
            }
            catch(Exception ex)
            {

            }
        }

        private void btn_refresh_list2_Click(object sender, EventArgs e)
        {
            GalleryView(0);
        }
    }
}
