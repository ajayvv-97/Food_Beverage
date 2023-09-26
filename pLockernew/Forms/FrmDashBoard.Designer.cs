namespace pLocker.Forms
{
    partial class FrmDashBoard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDashBoard));
            this.pnlDBRight = new System.Windows.Forms.Panel();
            this.pBLogout = new System.Windows.Forms.PictureBox();
            this.pBSettings = new System.Windows.Forms.PictureBox();
            this.pBKeypad = new System.Windows.Forms.PictureBox();
            this.pBCustomer = new System.Windows.Forms.PictureBox();
            this.pBHLocker = new System.Windows.Forms.PictureBox();
            this.pBLocker = new System.Windows.Forms.PictureBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlDBRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBLogout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBKeypad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBHLocker)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBLocker)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDBRight
            // 
            this.pnlDBRight.BackColor = System.Drawing.Color.Transparent;
            this.pnlDBRight.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlDBRight.Controls.Add(this.pBLogout);
            this.pnlDBRight.Controls.Add(this.pBSettings);
            this.pnlDBRight.Controls.Add(this.pBKeypad);
            this.pnlDBRight.Controls.Add(this.pBCustomer);
            this.pnlDBRight.Controls.Add(this.pBHLocker);
            this.pnlDBRight.Controls.Add(this.pBLocker);
            this.pnlDBRight.Location = new System.Drawing.Point(848, 1);
            this.pnlDBRight.Name = "pnlDBRight";
            this.pnlDBRight.Size = new System.Drawing.Size(232, 497);
            this.pnlDBRight.TabIndex = 1;
            // 
            // pBLogout
            // 
            this.pBLogout.BackColor = System.Drawing.Color.Transparent;
            this.pBLogout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pBLogout.BackgroundImage")));
            this.pBLogout.Location = new System.Drawing.Point(10, 425);
            this.pBLogout.Name = "pBLogout";
            this.pBLogout.Size = new System.Drawing.Size(212, 55);
            this.pBLogout.TabIndex = 5;
            this.pBLogout.TabStop = false;
            this.pBLogout.Click += new System.EventHandler(this.pBLogout_Click);
            // 
            // pBSettings
            // 
            this.pBSettings.BackColor = System.Drawing.Color.Transparent;
            this.pBSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pBSettings.BackgroundImage")));
            this.pBSettings.Location = new System.Drawing.Point(9, 260);
            this.pBSettings.Name = "pBSettings";
            this.pBSettings.Size = new System.Drawing.Size(212, 55);
            this.pBSettings.TabIndex = 4;
            this.pBSettings.TabStop = false;
            // 
            // pBKeypad
            // 
            this.pBKeypad.BackColor = System.Drawing.Color.Transparent;
            this.pBKeypad.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pBKeypad.BackgroundImage")));
            this.pBKeypad.Location = new System.Drawing.Point(10, 343);
            this.pBKeypad.Name = "pBKeypad";
            this.pBKeypad.Size = new System.Drawing.Size(212, 55);
            this.pBKeypad.TabIndex = 3;
            this.pBKeypad.TabStop = false;
            // 
            // pBCustomer
            // 
            this.pBCustomer.BackColor = System.Drawing.Color.Transparent;
            this.pBCustomer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pBCustomer.BackgroundImage")));
            this.pBCustomer.Location = new System.Drawing.Point(8, 180);
            this.pBCustomer.Name = "pBCustomer";
            this.pBCustomer.Size = new System.Drawing.Size(212, 52);
            this.pBCustomer.TabIndex = 2;
            this.pBCustomer.TabStop = false;
            // 
            // pBHLocker
            // 
            this.pBHLocker.BackColor = System.Drawing.Color.Transparent;
            this.pBHLocker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pBHLocker.BackgroundImage")));
            this.pBHLocker.Location = new System.Drawing.Point(7, 98);
            this.pBHLocker.Name = "pBHLocker";
            this.pBHLocker.Size = new System.Drawing.Size(212, 55);
            this.pBHLocker.TabIndex = 1;
            this.pBHLocker.TabStop = false;
            this.pBHLocker.Click += new System.EventHandler(this.pBHLocker_Click);
            // 
            // pBLocker
            // 
            this.pBLocker.BackColor = System.Drawing.Color.Transparent;
            this.pBLocker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pBLocker.BackgroundImage")));
            this.pBLocker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pBLocker.Location = new System.Drawing.Point(6, 22);
            this.pBLocker.Name = "pBLocker";
            this.pBLocker.Size = new System.Drawing.Size(212, 55);
            this.pBLocker.TabIndex = 0;
            this.pBLocker.TabStop = false;
            this.pBLocker.Click += new System.EventHandler(this.pBLocker_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Location = new System.Drawing.Point(2, 1);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(840, 358);
            this.pnlMain.TabIndex = 2;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.Transparent;
            this.pnlBottom.Location = new System.Drawing.Point(2, 365);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(840, 132);
            this.pnlBottom.TabIndex = 0;
            // 
            // FrmDashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1077, 500);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlDBRight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.HelpButton = true;
            this.KeyPreview = true;
            this.Name = "FrmDashBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmDashBoard";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.FrmDashBoard_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDashBoard_KeyDown);
            this.pnlDBRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pBLogout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBSettings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBKeypad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBHLocker)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBLocker)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlDBRight;
        private System.Windows.Forms.PictureBox pBLocker;
        private System.Windows.Forms.PictureBox pBLogout;
        private System.Windows.Forms.PictureBox pBSettings;
        private System.Windows.Forms.PictureBox pBKeypad;
        private System.Windows.Forms.PictureBox pBCustomer;
        private System.Windows.Forms.PictureBox pBHLocker;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlBottom;
    }
}