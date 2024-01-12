using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class LoadingGif : Form
    {
        public LoadingGif()
        {
            InitializeComponent();
        }

        private async void LoadingGif_LoadAsync(object sender, EventArgs e)
        {
            pb_loadingimg.Image = Properties.Resources.loading_gif;
            pb_loadingimg.SizeMode = PictureBoxSizeMode.StretchImage;
            await Task.Delay(1000);
            this.Close();
        }
    }
}
