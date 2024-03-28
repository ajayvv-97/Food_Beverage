
namespace FoodandBeverage.Forms
{
    partial class LoadingGIF_Timer
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
            this.pbloading = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbloading)).BeginInit();
            this.SuspendLayout();
            // 
            // pbloading
            // 
            this.pbloading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbloading.Location = new System.Drawing.Point(0, 0);
            this.pbloading.Name = "pbloading";
            this.pbloading.Size = new System.Drawing.Size(248, 195);
            this.pbloading.TabIndex = 0;
            this.pbloading.TabStop = false;
            // 
            // LoadingGIF_Timer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(248, 195);
            this.Controls.Add(this.pbloading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoadingGIF_Timer";
            this.Text = "LoadingGIF_Timer";
            this.Load += new System.EventHandler(this.LoadingGIF_Timer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbloading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbloading;
    }
}