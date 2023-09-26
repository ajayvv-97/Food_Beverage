namespace MarginPOSNew.Forms
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
            this.tmrsplash = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbmpos = new System.Windows.Forms.PictureBox();
            this.pbcmlog = new System.Windows.Forms.PictureBox();
            this.mlblAyj = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pbmpos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbcmlog)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrsplash
            // 
            this.tmrsplash.Tick += new System.EventHandler(this.tmrsplash_Tick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel1.Location = new System.Drawing.Point(-1, -3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(809, 10);
            this.panel1.TabIndex = 17;
            // 
            // pbmpos
            // 
            this.pbmpos.Image = global::MarginPOSNew.Properties.Resources.Margin_apos;
            this.pbmpos.Location = new System.Drawing.Point(239, 42);
            this.pbmpos.Name = "pbmpos";
            this.pbmpos.Size = new System.Drawing.Size(200, 200);
            this.pbmpos.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbmpos.TabIndex = 18;
            this.pbmpos.TabStop = false;
            // 
            // pbcmlog
            // 
            this.pbcmlog.Image = global::MarginPOSNew.Properties.Resources.AYJLogo;
            this.pbcmlog.Location = new System.Drawing.Point(23, 278);
            this.pbcmlog.Name = "pbcmlog";
            this.pbcmlog.Size = new System.Drawing.Size(85, 43);
            this.pbcmlog.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbcmlog.TabIndex = 91;
            this.pbcmlog.TabStop = false;
            // 
            // mlblAyj
            // 
            this.mlblAyj.AccessibleDescription = " ";
            this.mlblAyj.AutoSize = true;
            this.mlblAyj.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.mlblAyj.Location = new System.Drawing.Point(23, 335);
            this.mlblAyj.Name = "mlblAyj";
            this.mlblAyj.Size = new System.Drawing.Size(247, 19);
            this.mlblAyj.TabIndex = 92;
            this.mlblAyj.Text = "Anikayaj Software Solutions Pvt.Ltd";
            // 
            // FrmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(705, 377);
            this.ControlBox = false;
            this.Controls.Add(this.mlblAyj);
            this.Controls.Add(this.pbcmlog);
            this.Controls.Add(this.pbmpos);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSplash";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmSplash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbmpos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbcmlog)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer tmrsplash;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbmpos;
        private System.Windows.Forms.PictureBox pbcmlog;
        private MetroFramework.Controls.MetroLabel mlblAyj;
    }
}