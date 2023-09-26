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
using MarginPOSNew.Interfaces;
using MetroFramework;

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
            frmgame_tree_strucher frmgtree = new frmgame_tree_strucher();
            frmgtree.Close();
            clear();
            fillgride();
            
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
            
          
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmgame_tree_strucher frmgtre = new frmgame_tree_strucher();
            frmgtre.ShowDialog();
           
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

        private void btnBrowse_Click(object sender, EventArgs e)
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
            dmngame.Createdby = Convert.ToInt32(FrmDashBoard.UserId);
            dmngame.CreatedDate = System.DateTime.Now;
            dmngame.Branchid = FrmDashBoard.BranchId;
            dmngame.Location = 1;
            dmngame.Icon = txtimg.Text;
            bool InsertStatus = bllGame.AddData("prdInsertGame", dmngame);
            bm = new Bitmap(txtimg.Text);
            if (InsertStatus == true)
            {
                if (pic.BackgroundImage != null)
                    clsDocument.UploadImage(bm, txtgame.Text, DocumentType.Game);
                MetroMessageBox.Show(this, "Successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, 150);
            }
            else
            {
                MetroMessageBox.Show(this, "Save failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, 150);
            }

            clear();
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

        private void btnClose_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            frmgame_tree_strucher frmgtreee = new frmgame_tree_strucher();
            frmgtree.ShowDialog();
        }
    }
}
