using BLL;
using Domain;
using Game.Forms;
using MarginPOSNew.Classes;
using MarginPOSNew.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace pLocker.Forms
{
    public partial class frmgame_tree_strucher : Form
    {
        public static int Id = 0;
        ClsDocument clsDocument = new ClsDocument();
        public frmgame_tree_strucher()
        {
            InitializeComponent();
        }

        public void frmgame_tree_strucher_Load(object sender, EventArgs e)
        {           
            DisplayLockerList();
            loadgame();
        }
      
        public  void DisplayLockerList()
        {
            BllSearch<DmnGame> bllSearch = new BllSearch<DmnGame>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "*";
            objSearch.Table = "tblGame";
            objSearch.WhereNdGroupBy = "Where status=1 ";
            objSearch.OrderBy = "Order By ID";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
            if (itemSource.Count > 0)
            {
                int x = 30;
                int y = 100;
                int ir = 1;
                int flag = 0;
                for (int i = 0; i < itemSource.Count; i++)
                {
                    flag++;
                    string path = "";
                    PictureBox pic = new PictureBox();

                    Panel pnl = new Panel();
                    pnl.Name = itemSource[i].Id.ToString();
                    pnl.BorderStyle = BorderStyle.Fixed3D;
                    pnl.Size = new Size(300, 150);

                    string imageName = itemSource[i].mGame;
                   // imageName = gridSource[0].mGame;
                    bool imageExist, brLogoExist;
                    if (imageName != "")
                    {
                        imageExist = clsDocument.FileExistOrNot(imageName, DocumentType.Game);
                        if (imageExist)
                            pnl.BackgroundImage = clsDocument.DownloadImage(imageName, DocumentType.Game);
                    }




                    //Bitmap img = new Bitmap(path);
                    //pic.BackgroundImage = img;
                    pnl.BackgroundImageLayout = ImageLayout.Stretch;
                    pnl.Size = new Size(300, 150);
                   
                    
                    //Label label1 = new Label();
                    //label1.Text = itemSource[i].mGame;
                    //label1.ForeColor = Color.DarkGreen;
                    //label1.Font = new Font("Arial", 20, FontStyle.Bold);
                    //label1.AutoSize = true;

                    //Label label2 = new Label();
                    //label2.Text = itemSource[i].Macid;
                    //label2.ForeColor = Color.DarkGreen;
                    //label2.Font = new Font("Arial", 15, FontStyle.Bold);

                    //Label label3 = new Label();
                    //label3.Text = itemSource[i].CardRate.ToString();
                    //label3.ForeColor = Color.DarkGreen;
                    //label3.Font = new Font("Arial", 15, FontStyle.Bold);

                    //Label label4 = new Label();
                    //label4.Text = itemSource[i].TokenRate.ToString();
                    //label4.ForeColor = Color.DarkGreen;
                    //label4.Font = new Font("Arial", 15, FontStyle.Bold);


                    //Label label5 = new Label();
                    //label5.Text = itemSource[i].CoinRate.ToString();
                    //label5.ForeColor = Color.DarkGreen;
                    //label5.Font = new Font("Arial", 15, FontStyle.Bold);


                    //Label label6 = new Label();
                    //label6.Text = itemSource[i].DiscRate.ToString();
                    //label6.ForeColor = Color.DarkGreen;
                    //label6.Font = new Font("Arial", 15, FontStyle.Bold);
                  
                    //Label label7 = new Label();
                    //label7.Text = itemSource[i].gDesc;
                    //label7.ForeColor = Color.DarkGreen;
                    //label7.Font = new Font("Arial", 15, FontStyle.Bold);

                  
                    //  pnl.BackColor = Color.CadetBlue;
                    pic.DoubleClick += new EventHandler(this.panel_DoubleClick);
                    pnl.DoubleClick += new EventHandler(this.panel_DoubleClick);

                    //label1.Location = new Point(111, 10);
                    //label2.Location = new Point(429, 10);
                    //label3.Location = new Point(536, 10);
                    //label4.Location = new Point(680, 10);
                    //label5.Location = new Point(835, 10);
                    //label6.Location = new Point(977, 10);
                    //label7.Location = new Point(1152, 10);

                    //pic.Location = new Point(0, 0);
                    //pnl.Controls.Add(label1);
                    //pnl.Controls.Add(label2);
                    //pnl.Controls.Add(label3);
                    //pnl.Controls.Add(label4);
                    //pnl.Controls.Add(label5);
                    //pnl.Controls.Add(label6);
                    //pnl.Controls.Add(label7);

                   // pnl.Controls.Add(pic);
                    pnl.Visible = true;
                    pnl.Location = new Point(x, y);
                    x += 340;
                    if (flag == 3)
                    {
                        flag = 0;
                        x = 30;
                        y += 180;
                    }
                    
                    this.Controls.Add(pnl);
                    this.Refresh();
                  

            }      
                
            }         

        }
        private void panel_DoubleClick(object sender, EventArgs e)
        {    
            Panel pnl = sender as Panel;
             Id = Convert.ToInt32(pnl.Name);
            //FrmDashBoard frmd = new FrmDashBoard();
            //frmd.Gamedetails(Id);
            frmGame frmg = new frmGame(Id);
            frmg.ShowDialog();
            //this.Close();

        }


        private void lbladd_Click(object sender, EventArgs e)
        {
            //this.Hide();
            Id = 0;
            frmGame frmg = new frmGame(Id);
            frmg.ShowDialog();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmgame_tree_strucher_Enter(object sender, EventArgs e)
        {
           // MessageBox.Show("test");
            DisplayLockerList();
        }
        public void refreshform()
        {
            
        }

        private void label7_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbladd_Click_1(object sender, EventArgs e)
        {
            frmGame frmg = new frmGame(0);
            frmg.ShowDialog();    
        }

        private void label7_Click_2(object sender, EventArgs e)
        {
            frmGame frmg = new frmGame(0);
            frmg.ShowDialog();
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
         {

          
        }

        private void cmbgame_SelectedIndexChanged(object sender, EventArgs e)
        {

            //int id = Convert.ToInt32(cmbgame.SelectedValue);
           // frmGame frmg = new frmGame(id) ;
            //frmg.ShowDialog();
        }
        public void loadgame()
        {
            BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "ID,mGame";
            objSearch.Table = "tblGame";
            objSearch.WhereNdGroupBy = "Where status=1 ";
            objSearch.OrderBy = "Order By ID";
            var itemSource = bllSearch.GetSearchData("prdComboFill", objSearch);
            cmbgame.DataSource = itemSource;
            cmbgame.DisplayMember = "Value";
            cmbgame.ValueMember = "Id";
            cmbgame.Text = "";
        }

        private void cmbgame_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(cmbgame.SelectedValue);
            frmGame frmg = new frmGame(id);
            frmg.ShowDialog();

        }
    }
}
