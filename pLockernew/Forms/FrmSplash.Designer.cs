namespace pLocker.Forms
{
    partial class FrmSplash
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSplash));
            this.pbcmlog = new System.Windows.Forms.PictureBox();
            this.tmrsplash = new System.Windows.Forms.Timer(this.components);
            this.pblocker = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mlblAyj = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbcmlog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pblocker)).BeginInit();
            this.SuspendLayout();
            // 
            // pbcmlog
            // 
            this.pbcmlog.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbcmlog.BackgroundImage")));
            this.pbcmlog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbcmlog.Location = new System.Drawing.Point(23, 278);
            this.pbcmlog.Name = "pbcmlog";
            this.pbcmlog.Size = new System.Drawing.Size(85, 43);
            this.pbcmlog.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbcmlog.TabIndex = 94;
            this.pbcmlog.TabStop = false;
            // 
            // tmrsplash
            // 
            this.tmrsplash.Tick += new System.EventHandler(this.tmrsplash_Tick);
            // 
            // pblocker
            // 
            this.pblocker.Image = ((System.Drawing.Image)(resources.GetObject("pblocker.Image")));
            this.pblocker.Location = new System.Drawing.Point(235, 54);
            this.pblocker.Name = "pblocker";
            this.pblocker.Size = new System.Drawing.Size(200, 200);
            this.pblocker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pblocker.TabIndex = 93;
            this.pblocker.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel1.Location = new System.Drawing.Point(-8, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(831, 10);
            this.panel1.TabIndex = 92;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // mlblAyj
            // 
            this.mlblAyj.AutoSize = true;
            this.mlblAyj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblAyj.Location = new System.Drawing.Point(23, 336);
            this.mlblAyj.Name = "mlblAyj";
            this.mlblAyj.Size = new System.Drawing.Size(251, 16);
            this.mlblAyj.TabIndex = 95;
            this.mlblAyj.Text = "Anikayaj Software Solutions Pvt Ltd";
            // 
            // FrmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(707, 379);
            this.ControlBox = false;
            this.Controls.Add(this.mlblAyj);
            this.Controls.Add(this.pbcmlog);
            this.Controls.Add(this.pblocker);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSplash";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmSpalsh";
            this.Load += new System.EventHandler(this.FrmSplash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbcmlog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pblocker)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbcmlog;
        private System.Windows.Forms.Timer tmrsplash;
       // private MetroFramework.Controls.MetroLabel mlblAyj;
        private System.Windows.Forms.PictureBox pblocker;
        private System.Windows.Forms.Panel panel1;
       // private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.Label mlblAyj;
    }
}