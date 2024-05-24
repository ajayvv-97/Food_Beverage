
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
            this.progressbar = new System.Windows.Forms.ProgressBar();
            this.lblloading = new System.Windows.Forms.Label();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnl_all = new System.Windows.Forms.Panel();
            this.talpmain = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_all.SuspendLayout();
            this.talpmain.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressbar
            // 
            this.progressbar.BackColor = System.Drawing.Color.White;
            this.progressbar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressbar.Location = new System.Drawing.Point(0, 48);
            this.progressbar.MarqueeAnimationSpeed = 50;
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(425, 31);
            this.progressbar.TabIndex = 1;
            // 
            // lblloading
            // 
            this.lblloading.AutoSize = true;
            this.lblloading.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblloading.Font = new System.Drawing.Font("Agency FB", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblloading.ForeColor = System.Drawing.Color.White;
            this.lblloading.Location = new System.Drawing.Point(0, 24);
            this.lblloading.Name = "lblloading";
            this.lblloading.Size = new System.Drawing.Size(70, 24);
            this.lblloading.TabIndex = 2;
            this.lblloading.Text = "Loading....";
            this.lblloading.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // Timer1
            // 
            this.Timer1.Interval = 1;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // pnl_all
            // 
            this.pnl_all.Controls.Add(this.lblloading);
            this.pnl_all.Controls.Add(this.progressbar);
            this.pnl_all.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_all.Location = new System.Drawing.Point(218, 276);
            this.pnl_all.Name = "pnl_all";
            this.pnl_all.Size = new System.Drawing.Size(425, 79);
            this.pnl_all.TabIndex = 5;
            // 
            // talpmain
            // 
            this.talpmain.ColumnCount = 3;
            this.talpmain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.talpmain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.talpmain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.talpmain.Controls.Add(this.pnl_all, 1, 1);
            this.talpmain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.talpmain.Location = new System.Drawing.Point(0, 0);
            this.talpmain.Name = "talpmain";
            this.talpmain.RowCount = 3;
            this.talpmain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 76.25698F));
            this.talpmain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.74302F));
            this.talpmain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.talpmain.Size = new System.Drawing.Size(862, 458);
            this.talpmain.TabIndex = 6;
            // 
            // FrmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(862, 458);
            this.Controls.Add(this.talpmain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSplash";
            this.Opacity = 0.7D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmSplash_Load);
            this.pnl_all.ResumeLayout(false);
            this.pnl_all.PerformLayout();
            this.talpmain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressbar;
        private System.Windows.Forms.Label lblloading;
        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.Panel pnl_all;
        private System.Windows.Forms.TableLayoutPanel talpmain;
    }
}