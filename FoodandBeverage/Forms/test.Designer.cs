
namespace FoodandBeverage.Forms
{
    partial class test
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
            this.ayTextbox1 = new AYTextbox();
            this.SuspendLayout();
            // 
            // ayTextbox1
            // 
            this.ayTextbox1.BackColor = System.Drawing.SystemColors.Info;
            this.ayTextbox1.BorderColor = System.Drawing.Color.Black;
            this.ayTextbox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ayTextbox1.BorderWidth = 6;
            this.ayTextbox1.Location = new System.Drawing.Point(64, 95);
            this.ayTextbox1.Name = "ayTextbox1";
            this.ayTextbox1.Size = new System.Drawing.Size(214, 20);
            this.ayTextbox1.TabIndex = 0;
            // 
            // test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 300);
            this.Controls.Add(this.ayTextbox1);
            this.Name = "test";
            this.Text = "test";
            this.Load += new System.EventHandler(this.test_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private AYTextbox ayTextbox1;
    }
}