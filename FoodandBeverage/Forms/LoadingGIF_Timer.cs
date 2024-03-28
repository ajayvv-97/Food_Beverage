using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class LoadingGIF_Timer : Form
    {
        public LoadingGIF_Timer()
        {
            InitializeComponent();
        }

        private void LoadingGIF_Timer_Load(object sender, EventArgs e)
        {
            pbloading.Image = Properties.Resources.loading_gif;
            pbloading.SizeMode = PictureBoxSizeMode.StretchImage;
        }

    }
}

