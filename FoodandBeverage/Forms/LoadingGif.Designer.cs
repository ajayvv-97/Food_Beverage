
namespace FoodandBeverage.Forms
{
    partial class LoadingGif
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pb_loadingimg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_loadingimg)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_loadingimg
            // 
            this.pb_loadingimg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_loadingimg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_loadingimg.Location = new System.Drawing.Point(0, 0);
            this.pb_loadingimg.Name = "pb_loadingimg";
            this.pb_loadingimg.Size = new System.Drawing.Size(270, 180);
            this.pb_loadingimg.TabIndex = 0;
            this.pb_loadingimg.TabStop = false;
            // 
            // LoadingGif
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 180);
            this.Controls.Add(this.pb_loadingimg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoadingGif";
            this.Text = "LoadingGif";
            this.Load += new System.EventHandler(this.LoadingGif_LoadAsync);
            ((System.ComponentModel.ISupportInitialize)(this.pb_loadingimg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_loadingimg;
    }
}