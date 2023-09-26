namespace MarginPOSNew.Forms.UserAccounts
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.mbtnLogin = new MetroFramework.Controls.MetroButton();
            this.mbtnExit = new MetroFramework.Controls.MetroButton();
            this.mtxtUsername = new MetroFramework.Controls.MetroTextBox();
            this.mtxtPassword = new MetroFramework.Controls.MetroTextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mPnlLogin = new System.Windows.Forms.Panel();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pbUser = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pbMallPOS = new System.Windows.Forms.PictureBox();
            this.pbmall = new System.Windows.Forms.PictureBox();
            this.mbtnRFIDRead = new MetroFramework.Controls.MetroButton();
            this.mcmbBranch = new MetroFramework.Controls.MetroComboBox();
            this.mlblAssemblyVersion = new MetroFramework.Controls.MetroLabel();
            this.mlblAyj = new MetroFramework.Controls.MetroLabel();
            this.mlblVersion = new MetroFramework.Controls.MetroLabel();
            this.loginPanel = new MarginPOSNew.Classes.CurvedButton();
            this.pbPanelBackground = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.mPnlLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMallPOS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbmall)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPanelBackground)).BeginInit();
            this.SuspendLayout();
            // 
            // mbtnLogin
            // 
            this.mbtnLogin.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.mbtnLogin.BackgroundImage = global::MarginPOSNew.Properties.Resources.IconLogin;
            this.mbtnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mbtnLogin.Location = new System.Drawing.Point(355, 347);
            this.mbtnLogin.Name = "mbtnLogin";
            this.mbtnLogin.Size = new System.Drawing.Size(100, 35);
            this.mbtnLogin.TabIndex = 4;
            this.mbtnLogin.UseCustomBackColor = true;
            this.mbtnLogin.UseSelectable = true;
            this.mbtnLogin.Click += new System.EventHandler(this.MbtnLogin_Click);
            // 
            // mbtnExit
            // 
            this.mbtnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(91)))), ((int)(((byte)(91)))));
            this.mbtnExit.BackgroundImage = global::MarginPOSNew.Properties.Resources.IconExit;
            this.mbtnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.mbtnExit.Location = new System.Drawing.Point(470, 347);
            this.mbtnExit.Name = "mbtnExit";
            this.mbtnExit.Size = new System.Drawing.Size(100, 35);
            this.mbtnExit.TabIndex = 5;
            this.mbtnExit.UseCustomBackColor = true;
            this.mbtnExit.UseSelectable = true;
            this.mbtnExit.Click += new System.EventHandler(this.MbtnExit_Click);
            // 
            // mtxtUsername
            // 
            // 
            // 
            // 
            this.mtxtUsername.CustomButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtUsername.CustomButton.Image = null;
            this.mtxtUsername.CustomButton.Location = new System.Drawing.Point(257, 2);
            this.mtxtUsername.CustomButton.Name = "";
            this.mtxtUsername.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.mtxtUsername.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtUsername.CustomButton.TabIndex = 1;
            this.mtxtUsername.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtUsername.CustomButton.UseSelectable = true;
            this.mtxtUsername.CustomButton.Visible = false;
            this.mtxtUsername.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.mtxtUsername.Lines = new string[] {
        " "};
            this.mtxtUsername.Location = new System.Drawing.Point(286, 218);
            this.mtxtUsername.MaxLength = 50;
            this.mtxtUsername.Name = "mtxtUsername";
            this.mtxtUsername.PasswordChar = '\0';
            this.mtxtUsername.PromptText = "Username";
            this.mtxtUsername.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtUsername.SelectedText = "";
            this.mtxtUsername.SelectionLength = 0;
            this.mtxtUsername.SelectionStart = 0;
            this.mtxtUsername.ShortcutsEnabled = true;
            this.mtxtUsername.Size = new System.Drawing.Size(285, 30);
            this.mtxtUsername.TabIndex = 0;
            this.mtxtUsername.Tag = "Username";
            this.mtxtUsername.Text = " ";
            this.mtxtUsername.UseSelectable = true;
            this.mtxtUsername.WaterMark = "Username";
            this.mtxtUsername.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtUsername.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtUsername.Click += new System.EventHandler(this.mtxtUsername_Click);
            this.mtxtUsername.Leave += new System.EventHandler(this.mtxtUsername_Leave);
            // 
            // mtxtPassword
            // 
            // 
            // 
            // 
            this.mtxtPassword.CustomButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtPassword.CustomButton.Image = null;
            this.mtxtPassword.CustomButton.Location = new System.Drawing.Point(257, 2);
            this.mtxtPassword.CustomButton.Name = "";
            this.mtxtPassword.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.mtxtPassword.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtPassword.CustomButton.TabIndex = 1;
            this.mtxtPassword.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtPassword.CustomButton.UseSelectable = true;
            this.mtxtPassword.CustomButton.Visible = false;
            this.mtxtPassword.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtPassword.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.mtxtPassword.Lines = new string[0];
            this.mtxtPassword.Location = new System.Drawing.Point(286, 262);
            this.mtxtPassword.MaxLength = 50;
            this.mtxtPassword.Name = "mtxtPassword";
            this.mtxtPassword.PasswordChar = '.';
            this.mtxtPassword.PromptText = "Password";
            this.mtxtPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtPassword.SelectedText = "";
            this.mtxtPassword.SelectionLength = 0;
            this.mtxtPassword.SelectionStart = 0;
            this.mtxtPassword.ShortcutsEnabled = true;
            this.mtxtPassword.Size = new System.Drawing.Size(285, 30);
            this.mtxtPassword.TabIndex = 1;
            this.mtxtPassword.Tag = " Password";
            this.mtxtPassword.UseSelectable = true;
            this.mtxtPassword.WaterMark = "Password";
            this.mtxtPassword.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtPassword.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtPassword_KeyDown);
            this.mtxtPassword.Leave += new System.EventHandler(this.mtxtPassword_Leave);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(240, 256);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 34);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 5;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(240, 214);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(41, 34);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(388, 177);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Login";
            // 
            // mPnlLogin
            // 
            this.mPnlLogin.BackColor = System.Drawing.Color.Transparent;
            this.mPnlLogin.Controls.Add(this.pictureBox5);
            this.mPnlLogin.Controls.Add(this.pbUser);
            this.mPnlLogin.Controls.Add(this.pictureBox4);
            this.mPnlLogin.Controls.Add(this.pbMallPOS);
            this.mPnlLogin.Controls.Add(this.pbmall);
            this.mPnlLogin.Controls.Add(this.mbtnRFIDRead);
            this.mPnlLogin.Controls.Add(this.mcmbBranch);
            this.mPnlLogin.Controls.Add(this.mlblAssemblyVersion);
            this.mPnlLogin.Controls.Add(this.mlblAyj);
            this.mPnlLogin.Controls.Add(this.mlblVersion);
            this.mPnlLogin.Controls.Add(this.label1);
            this.mPnlLogin.Controls.Add(this.mtxtUsername);
            this.mPnlLogin.Controls.Add(this.mbtnExit);
            this.mPnlLogin.Controls.Add(this.pictureBox1);
            this.mPnlLogin.Controls.Add(this.pictureBox2);
            this.mPnlLogin.Controls.Add(this.mtxtPassword);
            this.mPnlLogin.Controls.Add(this.mbtnLogin);
            this.mPnlLogin.Controls.Add(this.loginPanel);
            this.mPnlLogin.Controls.Add(this.pbPanelBackground);
            this.mPnlLogin.Location = new System.Drawing.Point(1, 5);
            this.mPnlLogin.Name = "mPnlLogin";
            this.mPnlLogin.Size = new System.Drawing.Size(836, 500);
            this.mPnlLogin.TabIndex = 13;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(240, 300);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(41, 34);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 92;
            this.pictureBox5.TabStop = false;
            // 
            // pbUser
            // 
            this.pbUser.Image = global::MarginPOSNew.Properties.Resources.userProfile;
            this.pbUser.Location = new System.Drawing.Point(362, 58);
            this.pbUser.Name = "pbUser";
            this.pbUser.Size = new System.Drawing.Size(100, 100);
            this.pbUser.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbUser.TabIndex = 91;
            this.pbUser.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::MarginPOSNew.Properties.Resources.AYJLogo;
            this.pictureBox4.Location = new System.Drawing.Point(22, 436);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(59, 27);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 90;
            this.pictureBox4.TabStop = false;
            // 
            // pbMallPOS
            // 
            this.pbMallPOS.Image = global::MarginPOSNew.Properties.Resources.Margin_apos;
            this.pbMallPOS.Location = new System.Drawing.Point(22, 347);
            this.pbMallPOS.Name = "pbMallPOS";
            this.pbMallPOS.Size = new System.Drawing.Size(59, 59);
            this.pbMallPOS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbMallPOS.TabIndex = 89;
            this.pbMallPOS.TabStop = false;
            // 
            // pbmall
            // 
            this.pbmall.Image = global::MarginPOSNew.Properties.Resources.joshmall_logo;
            this.pbmall.Location = new System.Drawing.Point(664, 402);
            this.pbmall.Name = "pbmall";
            this.pbmall.Size = new System.Drawing.Size(111, 80);
            this.pbmall.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbmall.TabIndex = 8;
            this.pbmall.TabStop = false;
            // 
            // mbtnRFIDRead
            // 
            this.mbtnRFIDRead.BackColor = System.Drawing.Color.ForestGreen;
            this.mbtnRFIDRead.ForeColor = System.Drawing.Color.White;
            this.mbtnRFIDRead.Location = new System.Drawing.Point(240, 347);
            this.mbtnRFIDRead.Name = "mbtnRFIDRead";
            this.mbtnRFIDRead.Size = new System.Drawing.Size(100, 35);
            this.mbtnRFIDRead.TabIndex = 86;
            this.mbtnRFIDRead.Text = "&RFID [F9]";
            this.mbtnRFIDRead.UseCustomBackColor = true;
            this.mbtnRFIDRead.UseCustomForeColor = true;
            this.mbtnRFIDRead.UseSelectable = true;
            this.mbtnRFIDRead.Click += new System.EventHandler(this.MbtnRFIDRead_Click);
            // 
            // mcmbBranch
            // 
            this.mcmbBranch.DropDownWidth = 300;
            this.mcmbBranch.FontWeight = MetroFramework.MetroComboBoxWeight.Bold;
            this.mcmbBranch.FormattingEnabled = true;
            this.mcmbBranch.ItemHeight = 23;
            this.mcmbBranch.Location = new System.Drawing.Point(287, 305);
            this.mcmbBranch.Name = "mcmbBranch";
            this.mcmbBranch.Size = new System.Drawing.Size(284, 29);
            this.mcmbBranch.TabIndex = 3;
            this.mcmbBranch.Tag = "Branch";
            this.mcmbBranch.UseSelectable = true;
            // 
            // mlblAssemblyVersion
            // 
            this.mlblAssemblyVersion.AccessibleDescription = " ";
            this.mlblAssemblyVersion.AutoSize = true;
            this.mlblAssemblyVersion.Location = new System.Drawing.Point(77, 414);
            this.mlblAssemblyVersion.Name = "mlblAssemblyVersion";
            this.mlblAssemblyVersion.Size = new System.Drawing.Size(13, 19);
            this.mlblAssemblyVersion.TabIndex = 16;
            this.mlblAssemblyVersion.Text = " ";
            // 
            // mlblAyj
            // 
            this.mlblAyj.AccessibleDescription = " ";
            this.mlblAyj.AutoSize = true;
            this.mlblAyj.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.mlblAyj.Location = new System.Drawing.Point(19, 463);
            this.mlblAyj.Name = "mlblAyj";
            this.mlblAyj.Size = new System.Drawing.Size(247, 19);
            this.mlblAyj.TabIndex = 15;
            this.mlblAyj.Text = "Anikayaj Software Solutions Pvt.Ltd";
            // 
            // mlblVersion
            // 
            this.mlblVersion.AccessibleDescription = " ";
            this.mlblVersion.AutoSize = true;
            this.mlblVersion.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblVersion.Location = new System.Drawing.Point(19, 414);
            this.mlblVersion.Name = "mlblVersion";
            this.mlblVersion.Size = new System.Drawing.Size(62, 19);
            this.mlblVersion.TabIndex = 14;
            this.mlblVersion.Text = "Version :";
            // 
            // loginPanel
            // 
            this.loginPanel.BackColor = System.Drawing.Color.White;
            this.loginPanel.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.loginPanel.FlatAppearance.BorderSize = 3;
            this.loginPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loginPanel.ForeColor = System.Drawing.Color.MidnightBlue;
            this.loginPanel.Location = new System.Drawing.Point(232, 19);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(360, 414);
            this.loginPanel.TabIndex = 7;
            this.loginPanel.Text = " ";
            this.loginPanel.UseVisualStyleBackColor = false;
            // 
            // pbPanelBackground
            // 
            this.pbPanelBackground.Image = ((System.Drawing.Image)(resources.GetObject("pbPanelBackground.Image")));
            this.pbPanelBackground.Location = new System.Drawing.Point(3, -1);
            this.pbPanelBackground.Name = "pbPanelBackground";
            this.pbPanelBackground.Size = new System.Drawing.Size(834, 498);
            this.pbPanelBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPanelBackground.TabIndex = 12;
            this.pbPanelBackground.TabStop = false;
            // 
            // FrmLogin
            // 
            this.ApplyImageInvert = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BackImage = ((System.Drawing.Image)(resources.GetObject("$this.BackImage")));
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(838, 502);
            this.ControlBox = false;
            this.Controls.Add(this.mPnlLogin);
            this.KeyPreview = true;
            this.Name = "FrmLogin";
            this.Resizable = false;
            this.Text = " ";
            this.TransparencyKey = System.Drawing.Color.Maroon;
            this.Activated += new System.EventHandler(this.FrmLogin_Activated);
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmLogin_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.mPnlLogin.ResumeLayout(false);
            this.mPnlLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMallPOS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbmall)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPanelBackground)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton mbtnLogin;
        private MetroFramework.Controls.MetroButton mbtnExit;
        private MetroFramework.Controls.MetroTextBox mtxtUsername;
        private MetroFramework.Controls.MetroTextBox mtxtPassword;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel mPnlLogin;
        private Classes.CurvedButton loginPanel;
        private System.Windows.Forms.PictureBox pbPanelBackground;
        private MetroFramework.Controls.MetroLabel mlblAssemblyVersion;
        private MetroFramework.Controls.MetroLabel mlblAyj;
        private MetroFramework.Controls.MetroLabel mlblVersion;
        private MetroFramework.Controls.MetroComboBox mcmbBranch;
        private MetroFramework.Controls.MetroButton mbtnRFIDRead;
        private System.Windows.Forms.PictureBox pbmall;
        private System.Windows.Forms.PictureBox pbMallPOS;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pbUser;
        private System.Windows.Forms.PictureBox pictureBox5;
    }
}