
using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
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
        public static int id = 1;
        public static int fname = 2;
        public static int fcateg = 3;
        public static int fdes = 4;
        public static int fstaus = 5;
        public static int ffcid = 6;
        #endregion

        public string Compressedimg = Application.StartupPath + "\\CompressedImg";
        ClsDocument clsDocument = new ClsDocument();
        public static string empImgExt = "";
        TableLayoutPanel tlp_Gallery_Main = new TableLayoutPanel();
        TableLayoutPanel tlp_Gallery_Box = new TableLayoutPanel(); 
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
                    //dmnmenu.Title = txtitemname.Text;
                    dmnmenu.Description = txtdesc.Text;
                    dmnmenu.CategoryId = Convert.ToInt32(cmbfcateg.SelectedValue);
                    dmnmenu.Recipe_Des = txtrecipedes.Text;
                    dmnmenu.CreatedBy = 1;
                    dmnmenu.BranchId = 1;
                    if (chb_active.Checked)
                        dmnmenu.Status = 1;
                    else
                        dmnmenu.Status = 0;

                    if (chk_avilable.Checked)
                        dmnmenu.Aviable_status = 1;
                    else
                        dmnmenu.Aviable_status = 0;

                    var usertype = bllmenu.AddData("prdInsert_upd_menu", dmnmenu);

                        if (usertype.MId > 0)
                        {
                                if (pb_view_pic.BackgroundImage != null)
                                {
                                    if (File.Exists(Compressedimg + "\\Image.jpeg"))
                                    {
                                        bool status = clsDocument.CopyFile(Compressedimg + "\\Image.jpeg", Convert.ToString(usertype.MId) + ".jpeg", DocumentType.FoodPhoto);
                                        //if (status == false)
                                        //{
                                        //    MessageBox.Show(this, "Unable To Upload Image Please Try Again ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                                        //}
                                    }
                                }
                                else
                                {
                                    bool imageExist = clsDocument.FileExistOrNot(Convert.ToString(usertype.MId) + ".jpeg", DocumentType.FoodPhoto);
                                    if (imageExist)
                                    {
                                        clsDocument.DeleteImage(Convert.ToString(usertype.MId) + ".jpeg", DocumentType.FoodPhoto);
                                    }
                                }

                                if (Convert.ToUInt32(txtmid.Text) > 0)
                                    CMessageBox.Show(" Update Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    else
                                    CMessageBox.Show(" Successfully saved", "Saved  ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    clearall();
                                Fillmenu();
                    }
                    else
                        CMessageBox.Show(" Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                objSearch.Select = "m.mid as Field1, m.Title as Field2,fc.CategoryName as Field3,m.Description as Field4,m.Status as Field5,m.MId as Field6 ";
                objSearch.Table = "tblMenu m left join tblCategory_FTI fc on fc.Fcid=m.MId";
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
            //if (txtitemname.Text == "" || txtitemname.Text == null)
            //{
            //    CMessageBox.Show("Please Enter Food Name", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtitemname.Focus();
            //    return false;
            //}
            //else 
            if (Convert.ToInt32(cmbfcateg.SelectedValue) == 0)
            {
                CMessageBox.Show("Please Select Food Type", "Mandatory Field", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            Fillmenu();
            clearall();
        }
        public void clearall()
        {
            txtmid.Text = "";
            txtdesc.Text = "";
            //txtitemname.Text = "";
            Fillmenu();  
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {
            Fillmenu();
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

        private void pb_menupic_Click(object sender, EventArgs e)
        {
            try
            {
                if (empImgExt != "" && empImgExt != null)
                {
                    pnl_menupic.Visible = true;
                    pnl_menupic.Location = new Point(this.ClientSize.Width / 2 - pnl_menupic.Size.Width / 2, this.ClientSize.Height / 2 - pnl_menupic.Size.Height / 2);
                }
                else
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Images only.| *.jpeg;*.jpg;*.png;*.gif;";
                    DialogResult dialogResult = openFileDialog.ShowDialog();
                    if (openFileDialog.FileName != "")
                    {
                        empImgExt = openFileDialog.FileName;
                        pb_view_pic.BackgroundImage = Image.FromFile(openFileDialog.FileName);
                        pb_view_pic.BackgroundImageLayout = ImageLayout.Stretch;
                        pb_view_pic.Image = null;
                        Bitmap bitmap = new Bitmap(Image.FromFile(openFileDialog.FileName));
                        bitmap.Save(Compressedimg + "\\Image.jpeg");
                    }
                }

                
            }
            catch (Exception ex)
            {
               
            }
        }

        private void btn_close_pic_Click(object sender, EventArgs e)
        {
            pnl_menupic.Visible = false;
        }


        #region gallery creation
        public void GalleryView(int serachmode)
        {
            BllSearch<DmnTableinfo> bllSearch = new BllSearch<DmnTableinfo>();
            DmnSearch dmnSearch = new DmnSearch();
            dmnSearch.Select = "*";
            dmnSearch.Table = "tblTable_info";

            switch (serachmode)
            {
                case 0:
                    dmnSearch.WhereNdGroupBy = "where Status=1";
                    break;
                case 1:
                    dmnSearch.WhereNdGroupBy = "where Status=1 and OrderStatus=1";
                    break;
                case 2:
                    dmnSearch.WhereNdGroupBy = "where Status=1 and RevStatus=1";
                    break;
                case 3:
                    dmnSearch.WhereNdGroupBy = "where Status=1 and OrderStatus=0 and RevStatus=0";
                    break;

            }

            dmnSearch.OrderBy = "";
            var gridSource = bllSearch.GetSearchData("prdSearch", dmnSearch);
            if (gridSource.Count > 0)
            {
                pnlGallery.Controls.Clear();
                tlp_Gallery_Main.RowCount = 2;
                tlp_Gallery_Main.Controls.Clear();
                tlp_Gallery_Main.Dock = DockStyle.Fill;
                tlp_Gallery_Main.AutoScroll = true;


                int rowIndex = 1;
                int columnIndex = 0;
                for (int i = 0; i < gridSource.Count; i++)
                {
                    if (columnIndex == 8)
                    {
                        columnIndex = 0;
                        rowIndex++;
                    }
                    TableLayoutPanel tlp_Gallery_Box = new TableLayoutPanel();
                    PictureBox pictureBox = new PictureBox();
                    Label lbltablename = new Label();

                    // Configure the PictureBox
                    pictureBox.Name = gridSource[i].TableCode;
                    pictureBox.Image = FoodandBeverage.Properties.Resources.restaurant;
                    pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                   // pictureBox.BackColor = "";
                    pictureBox.Width = 60;
                    pictureBox.Height = 80;

                    // Configure the Label
                    lbltablename.Name = gridSource[i].TableCode;
                    lbltablename.Text = gridSource[i].TableCode;
                    lbltablename.ForeColor = Color.Black;
                    lbltablename.BackColor = Color.Transparent;
                    lbltablename.Dock = DockStyle.Top;
                    lbltablename.Width = 60;
                    //lbltablename.Margin = new Padding(10, 10, 0, 0);
                    lbltablename.Font = new Font("Arial", 10, FontStyle.Bold);

                    // Add the PictureBox and Label to the TableLayoutPanel
                    tlp_Gallery_Box.Controls.Add(pictureBox, 0, 0); // PictureBox in the first row
                    tlp_Gallery_Box.Controls.Add(lbltablename, 0, 1); // Label in the second row

                    // Configure the TableLayoutPanel
                    tlp_Gallery_Box.RowCount = 2;
                    //tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));
                    //tlp_table_inner.RowStyles.Add(new RowStyle(SizeType.AutoSize));

                    if (gridSource[i].OrderStatus == 1)
                        lbltablename.BackColor = Color.Red;
                    else if (gridSource[i].RevStatus == 1)
                        lbltablename.BackColor = Color.Yellow;
                    else
                        lbltablename.BackColor = Color.Green;
                    tlp_Gallery_Box.Name = gridSource[i].TableCode;
                    //tlp_table_inner.BorderStyle = BorderStyle.FixedSingle;
                    tlp_Gallery_Box.Dock = DockStyle.None;
                    tlp_Gallery_Box.Margin = new Padding(20, 10, 20, 10);
                    tlp_Gallery_Box.Width = 70;
                    tlp_Gallery_Box.Height = 120;

                    pictureBox.MouseEnter += TlpTableInner_MouseEnter;
                    lbltablename.MouseEnter += TlpTableInner_MouseEnter;

                    pictureBox.MouseLeave += TlpTableInner_MouseLeave;
                    lbltablename.MouseLeave += TlpTableInner_MouseLeave;

                    tlp_Gallery_Box.MouseEnter += TlpTableInner_MouseEnter;
                    tlp_Gallery_Box.MouseLeave += TlpTableInner_MouseLeave;

                    pictureBox.Click += new EventHandler(tableClickEventHandler);
                    lbltablename.Click += new EventHandler(tableClickEventHandler);
                    tlp_Gallery_Box.Click += new EventHandler(tableClickEventHandler);

                    tlp_Gallery_Box.Controls.Add(tlp_Gallery_Box, columnIndex, rowIndex);
                    columnIndex++;
                }
                tlp_Gallery_Box.BorderStyle = BorderStyle.FixedSingle;
                tlp_Gallery_Main.Controls.Add(tlp_Gallery_Box, 0, 1);
                pnlGallery.Controls.Add(tlp_Gallery_Main);
            }
        }
        private void tableClickEventHandler(object sender, EventArgs e)
        {
            try
            {
                Control clickedControl = sender as Control;
                if (clickedControl != null)
                {
                    string tableCode = clickedControl.Name;
                    // Now, you have the ID (tableCode) associated with the clicked control.
                    // You can use it as needed.
                    CMessageBox.Show("Clicked Table ID: " + tableCode);
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
        private void btn_change_pic_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Images only.| *.jpeg;*.jpg;*.png;*.gif;";
            DialogResult dialogResult = openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                Compressedimg = Application.StartupPath + "\\CompressedImg";
                empImgExt = openFileDialog.FileName;
                pb_view_pic.BackgroundImage = Image.FromFile(openFileDialog.FileName);
                pb_view_pic.BackgroundImageLayout = ImageLayout.Stretch;
                pb_view_pic.Image = null;
                Bitmap bitmap = new Bitmap(Image.FromFile(openFileDialog.FileName));
                bitmap.Save(Compressedimg + "\\Image.jpeg");
            }
        }

        private void btn_clear_pic_Click(object sender, EventArgs e)
        {
            empImgExt = "";
            pb_view_pic.BackgroundImage = null;
            pb_view_pic.Image = FoodandBeverage.Properties.Resources.no_pictures;
            pb_view_pic.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Btn_view_menu_Click(object sender, EventArgs e)
        {
            tlp_registartion_main.Visible = false;
            tlp_menu_Gallery.Visible = true;
            Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_main.Controls.Add(tlp_menu_Gallery, 1, 0);
            tlp_menu_Gallery.Dock = DockStyle.Fill;
            GalleryView(0);
        }

        private void btn_back_reg_Click(object sender, EventArgs e)
        {
            tlp_menu_Gallery.Visible = false;
            tlp_registartion_main.Visible = true;
            Control controlToRemove = tlp_main.GetControlFromPosition(1, 0);
            tlp_main.Controls.Remove(controlToRemove);
            tlp_main.Controls.Add(tlp_registartion_main, 1, 0);
            tlp_registartion_main.Dock = DockStyle.Fill;
        }
    }
}
