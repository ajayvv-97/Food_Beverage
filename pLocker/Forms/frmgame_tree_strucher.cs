using BLL;
using Domain;
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

        private void frmgame_tree_strucher_Load(object sender, EventArgs e)
        {           
            DisplayLockerList();
        }
      
        private void DisplayLockerList()
        {
            BllSearch<DmnGame> bllSearch = new BllSearch<DmnGame>();
            DmnSearch objSearch = new DmnSearch();
            objSearch.Select = "*";
            objSearch.Table = "tblGame";
           // objSearch.WhereNdGroupBy = "Where isnull(status,0)=0 and BranchId=" + BranchId;
            objSearch.OrderBy = "Order By ID";
            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);
            if (itemSource.Count > 0)
            {
                int x = 0;
                int y = 30;
                int ir = 1;              
                for (int i = 0; i < itemSource.Count; i++)
                {
                    string path = "";
                    PictureBox pic = new PictureBox();

                    string imageName = itemSource[i].mGame;
                   // imageName = gridSource[0].mGame;
                    bool imageExist, brLogoExist;
                    if (imageName != "")
                    {
                        imageExist = clsDocument.FileExistOrNot(imageName, DocumentType.Game);
                        if (imageExist)
                            pic.BackgroundImage = clsDocument.DownloadImage(imageName, DocumentType.Game);
                    }




                    //Bitmap img = new Bitmap(path);
                    //pic.BackgroundImage = img;
                    pic.BackgroundImageLayout = ImageLayout.Zoom;
                    pic.Size = new Size(100, 100);

;                    Label label1 = new Label();
                    label1.Text = itemSource[i].mGame;
                    label1.ForeColor = Color.DarkGreen;
                    label1.Font = new Font("Arial", 20, FontStyle.Bold);
                    label1.AutoSize = true;

                    Label label2 = new Label();
                    label2.Text = itemSource[i].Macid;
                    label2.ForeColor = Color.DarkGreen;
                    label2.Font = new Font("Arial", 15, FontStyle.Bold);

                    Label label3 = new Label();
                    label3.Text = itemSource[i].CardRate.ToString();
                    label3.ForeColor = Color.DarkGreen;
                    label3.Font = new Font("Arial", 15, FontStyle.Bold);

                    Label label4 = new Label();
                    label4.Text = itemSource[i].TokenRate.ToString();
                    label4.ForeColor = Color.DarkGreen;
                    label4.Font = new Font("Arial", 15, FontStyle.Bold);


                    Label label5 = new Label();
                    label5.Text = itemSource[i].CoinRate.ToString();
                    label5.ForeColor = Color.DarkGreen;
                    label5.Font = new Font("Arial", 15, FontStyle.Bold);


                    Label label6 = new Label();
                    label6.Text = itemSource[i].DiscRate.ToString();
                    label6.ForeColor = Color.DarkGreen;
                    label6.Font = new Font("Arial", 15, FontStyle.Bold);
                  
                    Label label7 = new Label();
                    label7.Text = itemSource[i].gDesc;
                    label7.ForeColor = Color.DarkGreen;
                    label7.Font = new Font("Arial", 15, FontStyle.Bold);

                    Panel pnl = new Panel();
                    pnl.Name = itemSource[i].Id.ToString();
                    pnl.BorderStyle = BorderStyle.Fixed3D;
                    pnl.Size=new Size(1500, 50);                    
                    pnl.BackColor = Color.CadetBlue;
                    pnl.DoubleClick += new EventHandler(this.panel_DoubleClick);

                    label1.Location = new Point(111, 10);
                    label2.Location = new Point(429, 10);
                    label3.Location = new Point(536, 10);
                    label4.Location = new Point(680, 10);
                    label5.Location = new Point(835, 10);
                    label6.Location = new Point(977, 10);
                    label7.Location = new Point(1152, 10);

                    pic.Location = new Point(5, -25);
                    pnl.Controls.Add(label1);
                    pnl.Controls.Add(label2);
                    pnl.Controls.Add(label3);
                    pnl.Controls.Add(label4);
                    pnl.Controls.Add(label5);
                    pnl.Controls.Add(label6);
                    pnl.Controls.Add(label7);

                    pnl.Controls.Add(pic);
                    pnl.Visible = true;
                    pnl.Location = new Point(x, y);
                   
                    y += 50;                 
                    this.Controls.Add(pnl);
                  

            }      
                
            }         

        }
        private void panel_DoubleClick(object sender, EventArgs e)
        {
            this.Hide();
            Panel pnl = sender as Panel;
             Id = Convert.ToInt32(pnl.Name);
            frmGame frmg = new frmGame(Id);
            frmg.ShowDialog();          

        }

        private void lbladd_Click(object sender, EventArgs e)
        {
            this.Hide();
            Id = 0;
            frmGame frmg = new frmGame(Id);
            frmg.ShowDialog();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
