
namespace FoodandBeverage.Forms
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
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.talpmain = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_all = new System.Windows.Forms.Panel();
            this.lblloading = new System.Windows.Forms.Label();
            this.progressbar = new System.Windows.Forms.ProgressBar();
            this.tlp_icon = new System.Windows.Forms.TableLayoutPanel();
            this.pbfbicon = new System.Windows.Forms.PictureBox();
            this.talpmain.SuspendLayout();
            this.pnl_all.SuspendLayout();
            this.tlp_icon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbfbicon)).BeginInit();
            this.SuspendLayout();
            // 
            // Timer1
            // 
            this.Timer1.Interval = 20;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // talpmain
            // 
            this.talpmain.BackColor = System.Drawing.Color.Transparent;
            this.talpmain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.talpmain.ColumnCount = 3;
            this.talpmain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.talpmain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.talpmain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.talpmain.Controls.Add(this.pnl_all, 1, 2);
            this.talpmain.Controls.Add(this.tlp_icon, 1, 1);
            this.talpmain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.talpmain.Location = new System.Drawing.Point(0, 0);
            this.talpmain.Name = "talpmain";
            this.talpmain.RowCount = 4;
            this.talpmain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.25698F));
            this.talpmain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.talpmain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.74302F));
            this.talpmain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.talpmain.Size = new System.Drawing.Size(862, 458);
            this.talpmain.TabIndex = 6;
            // 
            // pnl_all
            // 
            this.pnl_all.BackColor = System.Drawing.Color.Transparent;
            this.pnl_all.Controls.Add(this.lblloading);
            this.pnl_all.Controls.Add(this.progressbar);
            this.pnl_all.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_all.Location = new System.Drawing.Point(218, 297);
            this.pnl_all.Name = "pnl_all";
            this.pnl_all.Size = new System.Drawing.Size(425, 58);
            this.pnl_all.TabIndex = 5;
            // 
            // lblloading
            // 
            this.lblloading.AutoSize = true;
            this.lblloading.BackColor = System.Drawing.Color.Transparent;
            this.lblloading.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblloading.Font = new System.Drawing.Font("Agency FB", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblloading.ForeColor = System.Drawing.Color.White;
            this.lblloading.Location = new System.Drawing.Point(0, 3);
            this.lblloading.Name = "lblloading";
            this.lblloading.Size = new System.Drawing.Size(70, 24);
            this.lblloading.TabIndex = 2;
            this.lblloading.Text = "Loading....";
            this.lblloading.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // progressbar
            // 
            this.progressbar.BackColor = System.Drawing.Color.White;
            this.progressbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressbar.Location = new System.Drawing.Point(0, 27);
            this.progressbar.MarqueeAnimationSpeed = 50;
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(425, 31);
            this.progressbar.TabIndex = 1;
            // 
            // tlp_icon
            // 
            this.tlp_icon.ColumnCount = 3;
            this.tlp_icon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.88235F));
            this.tlp_icon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.47059F));
            this.tlp_icon.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33223F));
            this.tlp_icon.Controls.Add(this.pbfbicon, 1, 0);
            this.tlp_icon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_icon.Location = new System.Drawing.Point(218, 211);
            this.tlp_icon.Name = "tlp_icon";
            this.tlp_icon.RowCount = 1;
            this.tlp_icon.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_icon.Size = new System.Drawing.Size(425, 80);
            this.tlp_icon.TabIndex = 6;
            // 
            // pbfbicon
            // 
            this.pbfbicon.BackgroundImage = global::FoodandBeverage.Properties.Resources.midas_icon;
            this.pbfbicon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbfbicon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbfbicon.Location = new System.Drawing.Point(181, 3);
            this.pbfbicon.Name = "pbfbicon";
            this.pbfbicon.Size = new System.Drawing.Size(98, 74);
            this.pbfbicon.TabIndex = 0;
            this.pbfbicon.TabStop = false;
            // 
            // FrmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(862, 458);
            this.Controls.Add(this.talpmain);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSplash";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmSplash_Load);
            this.talpmain.ResumeLayout(false);
            this.pnl_all.ResumeLayout(false);
            this.pnl_all.PerformLayout();
            this.tlp_icon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbfbicon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressbar;
        private System.Windows.Forms.Label lblloading;
        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.Panel pnl_all;
        private System.Windows.Forms.TableLayoutPanel talpmain;
        private System.Windows.Forms.TableLayoutPanel tlp_icon;
        private System.Windows.Forms.PictureBox pbfbicon;
    }
}