using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using Domain;
using MarginPOSNew.Classes;
using MarginPOSNew.Forms;
using MarginPOSNew.Forms.UserAccounts;
using MarginPOSNew.Interfaces;
using MetroFramework;
using System.Drawing;
using System.Drawing.Drawing2D;
using Game.Classes;
using System.Security.AccessControl;
using System.Security.Principal;

namespace pLocker.Forms
{
    public partial class frmGame : Form
    {
        const int iid = 0;
        const int igame = 1;
        const int imacid = 2;
        const int icardrate = 3;
        const int itokenrate = 4;
        const int iconirate = 5;
        const int idiscount = 6;
        const int idescription = 7;
        ClsDocument clsDocument = new ClsDocument();
        frmgame_tree_strucher frmgtree = new frmgame_tree_strucher();
        string imageFolder = Path.GetDirectoryName(Application.ExecutablePath);
        int idi = 0;
        Bitmap bm, bmbrLogo;
        public frmGame(int Id)
        {
            idi = Id;
            InitializeComponent();
        }

        private void frmGame_Load(object sender, EventArgs e)
        {
           // frmgame_tree_strucher frmgtree = new frmgame_tree_strucher();
            //frmgtree.Show();
            clear();
            fillgride();
            loadgcategory();


        }
        public void fillgride()
        {
            
            BllSearch<DmnGame> bllSearch = new BllSearch<DmnGame>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "*";
            objSearch.Table = "tblGame";
            objSearch.WhereNdGroupBy = "where ID='" + idi + "'";
            var gridSource = bllSearch.GetSearchData("prdSearch", objSearch);
            string path = "";
            if (gridSource.Count() > 0)
            {
                txtgame.Text = gridSource[0].mGame;
                txtmacid.Text = gridSource[0].Macid;
                txtcardrate.Text = gridSource[0].CardRate.ToString(); ;
                txttokenrate.Text = gridSource[0].TokenRate.ToString();
                txtcoinrate.Text = gridSource[0].CoinRate.ToString();
                txtdicount.Text = gridSource[0].DiscRate.ToString();
                txtdesc.Text = gridSource[0].gDesc;
                txtviprate.Text = gridSource[0].viprate.ToString();
                txthoures.Text = gridSource[0].TimeDuration.ToString();
                // path = gridSource[0].Icon;

                string imageName = string.Empty;
                imageName = gridSource[0].mGame;
                bool imageExist, brLogoExist;
                if (imageName != "")
                {
                    imageExist = clsDocument.FileExistOrNot(imageName, DocumentType.Game);
                    if (imageExist)
                        pic.BackgroundImage = clsDocument.DownloadImage(imageName, DocumentType.Game);
                }
            }
            //txtimg.Text = gridSource[0].Icon;
            //pic.BackgroundImage = Image.FromFile(path);
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
           
        }
        public void clear()
        {
            txtgame.Text = "";
            txtmacid.Text = "";
            txtcardrate.Text = "";
            txttokenrate.Text = "";
            txtcoinrate.Text = "";
            txtdesc.Text = "";
            txtdicount.Text = "";
            pic.BackgroundImage = null;
            
        }
        private void dgvgame_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
          
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
           // frmgame_tree_strucher frmgtre = new frmgame_tree_strucher();
            //frmgtre.ShowDialog();
           
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
          
           
        }
        private void dgvgame_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData==Keys.Delete)
            {

            }
        }
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            DmnGame dmngame = new DmnGame();
            BllGame bllGame = new BllGame();
            dmngame.Id = idi;
            dmngame.mGame = txtgame.Text;
            dmngame.Macid = txtmacid.Text;
            dmngame.CardRate = Convert.ToDecimal(txtcardrate.Text);
            dmngame.TokenRate = Convert.ToDecimal(txttokenrate.Text);
            dmngame.CoinRate = Convert.ToDecimal(txtcoinrate.Text);
            dmngame.DiscRate = Convert.ToDecimal(txtdicount.Text);
            dmngame.gDesc = txtdesc.Text;
            dmngame.Createdby = Convert.ToInt32(FrmDashboard.UserId);
            dmngame.CreatedDate = System.DateTime.Now;
            dmngame.Branchid = FrmDashboard.BranchId;
            dmngame.viprate = Convert.ToDecimal(txtviprate.Text);
            dmngame.TimeDuration = Convert.ToInt32(txthoures.Text);
            dmngame.Location = 1;
            dmngame.status = 1;
            dmngame.Icon = txtimg.Text;
            dmngame.CategoryId = Convert.ToInt32(cmbgcategory.SelectedValue);
            
            bool InsertStatus = bllGame.AddData("prdInsertGame", dmngame);

            if (InsertStatus == true)
            {
                if (txtimg.Text != "")
                {
                   
                        // ClsImgComprossor clsimg = new ClsImgComprossor();
                        string Compressedimg = Application.StartupPath + "\\CompressedImg";
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
                     Compressedimg=ClsImgComprossor.CompressImage(txtimg.Text,Compressedimg, 30);
                    //string p = Compressedimg + "\\" + txtgame.Text + ".jpg";
                    bm = new Bitmap(Compressedimg);
                    if (pic.BackgroundImage != null)
                        clsDocument.UploadImage(bm, txtgame.Text, DocumentType.Game);
                    MetroMessageBox.Show(this, "Successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
                    this.Close();
                }
            }
            else
            {
                MetroMessageBox.Show(this, "Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

            clear();
            frmgtree.refreshform();
          
        }

        private void txtcardrate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&(e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txttokenrate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtcoinrate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtdicount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void frmGame_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F6)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSave_Click_1(sender, e);
            }
        }     
        private void pic_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                if (pic.Text != string.Empty)
                {
                    MetroMessageBox.Show(this, "Remove File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
                }
                else
                {
                    OpenFileDialog openFd = new OpenFileDialog();
                    openFd.Filter = "Images only.| *.jpeg;*.jpg;*.png;*.gif;";
                    DialogResult dr = openFd.ShowDialog();
                    if (openFd.FileName != "")
                    {
                        string ext = Path.GetExtension(openFd.FileName);
                        txtimg.Text = openFd.FileName.ToString().Trim();
                        pic.BackgroundImage = Image.FromFile(openFd.FileName);
                        pic.BackgroundImageLayout = ImageLayout.Zoom;
                        #region create directory if not exists

                        bool exists = System.IO.Directory.Exists(imageFolder);
                        if (!exists)
                        {
                            System.IO.Directory.CreateDirectory(imageFolder);
                        }

                        #endregion

                    }
                    //mbtnSave.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, ex.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

        } 
        private void pic_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
             
                contextMenuStrip1.Show(pic, e.Location);
            }

        }       

        private void addToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            pic_DoubleClick(sender, e);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           DialogResult r= MessageBox.Show("Do You Want To Remove The Image", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.OK)
            {
                pic.BackgroundImage = null;
              bool  imageExist = clsDocument.FileExistOrNot(txtgame.Text.Trim(), DocumentType.Game);
                if (imageExist)
                    clsDocument.DeleteImage(txtgame.Text, DocumentType.Game);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DmnGame dmngame = new DmnGame();
            BllGame bllGame = new BllGame();
            dmngame.Id = idi;
            dmngame.mGame = txtgame.Text;
            dmngame.Macid = txtmacid.Text;
            dmngame.CardRate = Convert.ToDecimal(txtcardrate.Text);
            dmngame.TokenRate = Convert.ToDecimal(txttokenrate.Text);
            dmngame.CoinRate = Convert.ToDecimal(txtcoinrate.Text);
            dmngame.DiscRate = Convert.ToDecimal(txtdicount.Text);
            dmngame.gDesc = txtdesc.Text;
            dmngame.Createdby = Convert.ToInt32(FrmDashboard.UserId);
            dmngame.CreatedDate = System.DateTime.Now;
            dmngame.Branchid = FrmDashboard.BranchId;
            dmngame.Location = 1;
            dmngame.status = 0;
            dmngame.Icon = txtimg.Text;
            bool InsertStatus = bllGame.AddData("prdInsertGame", dmngame);

            if (InsertStatus == true)
            {
                if (txtimg.Text != "")
                {
                    bm = new Bitmap(txtimg.Text);
                    if (pic.BackgroundImage != null)
                        clsDocument.UploadImage(bm, txtgame.Text, DocumentType.Game);
                    MetroMessageBox.Show(this, "The Game Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);

                }
            }
            else
            {
                MetroMessageBox.Show(this, "Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

            clear();
            frmgtree.refreshform();
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmgame_tree_strucher frmgtreee = new frmgame_tree_strucher();
            frmgtree.ShowDialog();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DmnGameCategory dmngcatgory = new DmnGameCategory();
            dmngcatgory.Gcategory = txtcategory.Text;
            dmngcatgory.gDesc = txtcatedesc.Text;
            dmngcatgory.Createdby = 1;
            BllGame bllgame = new BllGame();
            bool insertstatus=bllgame.AddCategory("prdinsertGcategory", dmngcatgory);
            loadgcategory();
            pnlcategory.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            pnlcategory.Show();
        }

        private void label15_Click(object sender, EventArgs e)
        {
            pnlcategory.Hide();
        }

        private void txthoures_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtviprate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void pic_Click(object sender, EventArgs e)
        {

        }

        public void loadgcategory()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "ID,Gcategory";
            objSearch.Table = "tblGameCategory";
            //objSearch.WhereNdGroupBy = "Where status=1 ";
            objSearch.OrderBy = "Order By ID";
            var itemSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            cmbgcategory.DataSource = itemSource;
            cmbgcategory.DisplayMember = "Value";
            cmbgcategory.ValueMember = "Id";
            
        }
       
    }
}
