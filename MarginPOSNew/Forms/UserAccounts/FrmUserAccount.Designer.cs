namespace MarginPOSNew.Forms.UserAccounts
{
    partial class FrmUserAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserAccount));
            this.hlTitle = new MetroFramework.Drawing.Html.HtmlLabel();
            this.mcmbUserAccountType = new MetroFramework.Controls.MetroComboBox();
            this.mtxtUsername = new MetroFramework.Controls.MetroTextBox();
            this.mtxtPassword = new MetroFramework.Controls.MetroTextBox();
            this.mtxtConfirmPwd = new MetroFramework.Controls.MetroTextBox();
            this.mbtnRefresh = new MetroFramework.Controls.MetroButton();
            this.mbtnSave = new MetroFramework.Controls.MetroButton();
            this.mlblUserAccountType = new MetroFramework.Controls.MetroLabel();
            this.mlblUsename = new MetroFramework.Controls.MetroLabel();
            this.mlblPassword = new MetroFramework.Controls.MetroLabel();
            this.mlblConfirmPwd = new MetroFramework.Controls.MetroLabel();
            this.mbtnDelete = new MetroFramework.Controls.MetroButton();
            this.mbtnList = new MetroFramework.Controls.MetroButton();
            this.mbEmployee = new MetroFramework.Controls.MetroButton();
            this.mtxtEmployee = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.pbIncorrectPwd = new System.Windows.Forms.PictureBox();
            this.pbPassword = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbIncorrectPwd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).BeginInit();
            this.SuspendLayout();
            // 
            // hlTitle
            // 
            this.hlTitle.AutoScroll = true;
            this.hlTitle.AutoScrollMinSize = new System.Drawing.Size(127, 36);
            this.hlTitle.AutoSize = false;
            this.hlTitle.BackColor = System.Drawing.SystemColors.Window;
            this.hlTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlTitle.Location = new System.Drawing.Point(29, 20);
            this.hlTitle.Name = "hlTitle";
            this.hlTitle.Size = new System.Drawing.Size(186, 38);
            this.hlTitle.TabIndex = 7;
            this.hlTitle.TabStop = false;
            this.hlTitle.Text = "User Account";
            // 
            // mcmbUserAccountType
            // 
            this.mcmbUserAccountType.FormattingEnabled = true;
            this.mcmbUserAccountType.ItemHeight = 23;
            this.mcmbUserAccountType.Location = new System.Drawing.Point(194, 114);
            this.mcmbUserAccountType.Name = "mcmbUserAccountType";
            this.mcmbUserAccountType.Size = new System.Drawing.Size(294, 29);
            this.mcmbUserAccountType.TabIndex = 2;
            this.mcmbUserAccountType.Tag = "User Account Type";
            this.mcmbUserAccountType.UseSelectable = true;
            // 
            // mtxtUsername
            // 
            // 
            // 
            // 
            this.mtxtUsername.CustomButton.Image = null;
            this.mtxtUsername.CustomButton.Location = new System.Drawing.Point(272, 1);
            this.mtxtUsername.CustomButton.Name = "";
            this.mtxtUsername.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtUsername.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtUsername.CustomButton.TabIndex = 1;
            this.mtxtUsername.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtUsername.CustomButton.UseSelectable = true;
            this.mtxtUsername.CustomButton.Visible = false;
            this.mtxtUsername.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtUsername.Lines = new string[0];
            this.mtxtUsername.Location = new System.Drawing.Point(194, 158);
            this.mtxtUsername.MaxLength = 50;
            this.mtxtUsername.Name = "mtxtUsername";
            this.mtxtUsername.PasswordChar = '\0';
            this.mtxtUsername.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtUsername.SelectedText = "";
            this.mtxtUsername.SelectionLength = 0;
            this.mtxtUsername.SelectionStart = 0;
            this.mtxtUsername.ShortcutsEnabled = true;
            this.mtxtUsername.Size = new System.Drawing.Size(294, 23);
            this.mtxtUsername.TabIndex = 3;
            this.mtxtUsername.Tag = "Username";
            this.mtxtUsername.UseSelectable = true;
            this.mtxtUsername.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtUsername.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // mtxtPassword
            // 
            // 
            // 
            // 
            this.mtxtPassword.CustomButton.Image = null;
            this.mtxtPassword.CustomButton.Location = new System.Drawing.Point(272, 1);
            this.mtxtPassword.CustomButton.Name = "";
            this.mtxtPassword.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtPassword.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtPassword.CustomButton.TabIndex = 1;
            this.mtxtPassword.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtPassword.CustomButton.UseSelectable = true;
            this.mtxtPassword.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtPassword.Lines = new string[0];
            this.mtxtPassword.Location = new System.Drawing.Point(194, 196);
            this.mtxtPassword.MaxLength = 20;
            this.mtxtPassword.Name = "mtxtPassword";
            this.mtxtPassword.PasswordChar = '●';
            this.mtxtPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtPassword.SelectedText = "";
            this.mtxtPassword.SelectionLength = 0;
            this.mtxtPassword.SelectionStart = 0;
            this.mtxtPassword.ShortcutsEnabled = true;
            this.mtxtPassword.ShowButton = true;
            this.mtxtPassword.Size = new System.Drawing.Size(294, 23);
            this.mtxtPassword.TabIndex = 4;
            this.mtxtPassword.Tag = "Password";
            this.mtxtPassword.UseSelectable = true;
            this.mtxtPassword.UseSystemPasswordChar = true;
            this.mtxtPassword.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtPassword.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtPassword.ButtonClick += new MetroFramework.Controls.MetroTextBox.ButClick(this.MtxtPassword_ButtonClick);
            this.mtxtPassword.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MtxtPassword_KeyUp);
            // 
            // mtxtConfirmPwd
            // 
            // 
            // 
            // 
            this.mtxtConfirmPwd.CustomButton.Image = null;
            this.mtxtConfirmPwd.CustomButton.Location = new System.Drawing.Point(272, 1);
            this.mtxtConfirmPwd.CustomButton.Name = "";
            this.mtxtConfirmPwd.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtConfirmPwd.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtConfirmPwd.CustomButton.TabIndex = 1;
            this.mtxtConfirmPwd.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtConfirmPwd.CustomButton.UseSelectable = true;
            this.mtxtConfirmPwd.CustomButton.Visible = false;
            this.mtxtConfirmPwd.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtConfirmPwd.Lines = new string[0];
            this.mtxtConfirmPwd.Location = new System.Drawing.Point(194, 234);
            this.mtxtConfirmPwd.MaxLength = 20;
            this.mtxtConfirmPwd.Name = "mtxtConfirmPwd";
            this.mtxtConfirmPwd.PasswordChar = '*';
            this.mtxtConfirmPwd.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtConfirmPwd.SelectedText = "";
            this.mtxtConfirmPwd.SelectionLength = 0;
            this.mtxtConfirmPwd.SelectionStart = 0;
            this.mtxtConfirmPwd.ShortcutsEnabled = true;
            this.mtxtConfirmPwd.Size = new System.Drawing.Size(294, 23);
            this.mtxtConfirmPwd.TabIndex = 5;
            this.mtxtConfirmPwd.Tag = "Confirm Password";
            this.mtxtConfirmPwd.UseSelectable = true;
            this.mtxtConfirmPwd.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtConfirmPwd.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtConfirmPwd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MtxtConfirmPwd_KeyUp);
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.mbtnRefresh.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnRefresh.ForeColor = System.Drawing.Color.White;
            this.mbtnRefresh.Location = new System.Drawing.Point(338, 285);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(150, 45);
            this.mbtnRefresh.TabIndex = 8;
            this.mbtnRefresh.Text = "&Refresh [F5]";
            this.mbtnRefresh.UseCustomBackColor = true;
            this.mbtnRefresh.UseCustomForeColor = true;
            this.mbtnRefresh.UseSelectable = true;
            this.mbtnRefresh.Click += new System.EventHandler(this.MbtnRefresh_Click);
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackColor = System.Drawing.Color.CornflowerBlue;
            this.mbtnSave.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnSave.ForeColor = System.Drawing.Color.White;
            this.mbtnSave.Location = new System.Drawing.Point(22, 285);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(150, 45);
            this.mbtnSave.TabIndex = 6;
            this.mbtnSave.Text = "&Save [F2]";
            this.mbtnSave.UseCustomBackColor = true;
            this.mbtnSave.UseCustomForeColor = true;
            this.mbtnSave.UseSelectable = true;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            // 
            // mlblUserAccountType
            // 
            this.mlblUserAccountType.AutoSize = true;
            this.mlblUserAccountType.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblUserAccountType.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblUserAccountType.ForeColor = System.Drawing.Color.Black;
            this.mlblUserAccountType.Location = new System.Drawing.Point(27, 116);
            this.mlblUserAccountType.Name = "mlblUserAccountType";
            this.mlblUserAccountType.Size = new System.Drawing.Size(160, 25);
            this.mlblUserAccountType.TabIndex = 15;
            this.mlblUserAccountType.Text = "User Account Type";
            // 
            // mlblUsename
            // 
            this.mlblUsename.AutoSize = true;
            this.mlblUsename.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblUsename.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblUsename.ForeColor = System.Drawing.Color.Black;
            this.mlblUsename.Location = new System.Drawing.Point(96, 157);
            this.mlblUsename.Name = "mlblUsename";
            this.mlblUsename.Size = new System.Drawing.Size(91, 25);
            this.mlblUsename.TabIndex = 16;
            this.mlblUsename.Text = "Username";
            // 
            // mlblPassword
            // 
            this.mlblPassword.AutoSize = true;
            this.mlblPassword.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblPassword.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblPassword.ForeColor = System.Drawing.Color.Black;
            this.mlblPassword.Location = new System.Drawing.Point(99, 195);
            this.mlblPassword.Name = "mlblPassword";
            this.mlblPassword.Size = new System.Drawing.Size(88, 25);
            this.mlblPassword.TabIndex = 19;
            this.mlblPassword.Text = "Password";
            // 
            // mlblConfirmPwd
            // 
            this.mlblConfirmPwd.AutoSize = true;
            this.mlblConfirmPwd.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblConfirmPwd.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblConfirmPwd.ForeColor = System.Drawing.Color.Black;
            this.mlblConfirmPwd.Location = new System.Drawing.Point(30, 233);
            this.mlblConfirmPwd.Name = "mlblConfirmPwd";
            this.mlblConfirmPwd.Size = new System.Drawing.Size(157, 25);
            this.mlblConfirmPwd.TabIndex = 20;
            this.mlblConfirmPwd.Text = "Confirm Password";
            // 
            // mbtnDelete
            // 
            this.mbtnDelete.BackColor = System.Drawing.Color.Firebrick;
            this.mbtnDelete.Enabled = false;
            this.mbtnDelete.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnDelete.ForeColor = System.Drawing.Color.White;
            this.mbtnDelete.Location = new System.Drawing.Point(496, 285);
            this.mbtnDelete.Name = "mbtnDelete";
            this.mbtnDelete.Size = new System.Drawing.Size(150, 45);
            this.mbtnDelete.TabIndex = 9;
            this.mbtnDelete.Text = "&Delete [F7]";
            this.mbtnDelete.UseCustomBackColor = true;
            this.mbtnDelete.UseCustomForeColor = true;
            this.mbtnDelete.UseSelectable = true;
            this.mbtnDelete.Click += new System.EventHandler(this.MbtnDelete_Click);
            // 
            // mbtnList
            // 
            this.mbtnList.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.mbtnList.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnList.ForeColor = System.Drawing.Color.White;
            this.mbtnList.Location = new System.Drawing.Point(180, 285);
            this.mbtnList.Name = "mbtnList";
            this.mbtnList.Size = new System.Drawing.Size(150, 45);
            this.mbtnList.TabIndex = 7;
            this.mbtnList.Text = "&Search [F3]";
            this.mbtnList.UseCustomBackColor = true;
            this.mbtnList.UseCustomForeColor = true;
            this.mbtnList.UseSelectable = true;
            this.mbtnList.Click += new System.EventHandler(this.MbtnList_Click);
            // 
            // mbEmployee
            // 
            this.mbEmployee.Location = new System.Drawing.Point(493, 76);
            this.mbEmployee.Name = "mbEmployee";
            this.mbEmployee.Size = new System.Drawing.Size(26, 23);
            this.mbEmployee.TabIndex = 1;
            this.mbEmployee.Text = "...";
            this.mbEmployee.UseSelectable = true;
            this.mbEmployee.Click += new System.EventHandler(this.MbEmployee_Click);
            // 
            // mtxtEmployee
            // 
            // 
            // 
            // 
            this.mtxtEmployee.CustomButton.Image = null;
            this.mtxtEmployee.CustomButton.Location = new System.Drawing.Point(272, 1);
            this.mtxtEmployee.CustomButton.Name = "";
            this.mtxtEmployee.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtEmployee.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtEmployee.CustomButton.TabIndex = 1;
            this.mtxtEmployee.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtEmployee.CustomButton.UseSelectable = true;
            this.mtxtEmployee.CustomButton.Visible = false;
            this.mtxtEmployee.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtEmployee.Lines = new string[0];
            this.mtxtEmployee.Location = new System.Drawing.Point(194, 76);
            this.mtxtEmployee.MaxLength = 32767;
            this.mtxtEmployee.Name = "mtxtEmployee";
            this.mtxtEmployee.PasswordChar = '\0';
            this.mtxtEmployee.ReadOnly = true;
            this.mtxtEmployee.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtEmployee.SelectedText = "";
            this.mtxtEmployee.SelectionLength = 0;
            this.mtxtEmployee.SelectionStart = 0;
            this.mtxtEmployee.ShortcutsEnabled = true;
            this.mtxtEmployee.Size = new System.Drawing.Size(294, 23);
            this.mtxtEmployee.TabIndex = 0;
            this.mtxtEmployee.TabStop = false;
            this.mtxtEmployee.Tag = "Employee name";
            this.mtxtEmployee.UseSelectable = true;
            this.mtxtEmployee.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtEmployee.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.ForeColor = System.Drawing.Color.Black;
            this.metroLabel2.Location = new System.Drawing.Point(96, 75);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(90, 25);
            this.metroLabel2.TabIndex = 103;
            this.metroLabel2.Text = "Employee";
            // 
            // pbIncorrectPwd
            // 
            this.pbIncorrectPwd.Image = ((System.Drawing.Image)(resources.GetObject("pbIncorrectPwd.Image")));
            this.pbIncorrectPwd.Location = new System.Drawing.Point(492, 233);
            this.pbIncorrectPwd.Name = "pbIncorrectPwd";
            this.pbIncorrectPwd.Size = new System.Drawing.Size(23, 25);
            this.pbIncorrectPwd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbIncorrectPwd.TabIndex = 104;
            this.pbIncorrectPwd.TabStop = false;
            this.pbIncorrectPwd.Visible = false;
            // 
            // pbPassword
            // 
            this.pbPassword.Image = ((System.Drawing.Image)(resources.GetObject("pbPassword.Image")));
            this.pbPassword.Location = new System.Drawing.Point(492, 233);
            this.pbPassword.Name = "pbPassword";
            this.pbPassword.Size = new System.Drawing.Size(23, 25);
            this.pbPassword.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPassword.TabIndex = 100;
            this.pbPassword.TabStop = false;
            this.pbPassword.Visible = false;
            // 
            // FrmUserAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(655, 344);
            this.Controls.Add(this.pbIncorrectPwd);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.mtxtEmployee);
            this.Controls.Add(this.mbEmployee);
            this.Controls.Add(this.pbPassword);
            this.Controls.Add(this.mbtnList);
            this.Controls.Add(this.mbtnDelete);
            this.Controls.Add(this.mlblConfirmPwd);
            this.Controls.Add(this.mlblPassword);
            this.Controls.Add(this.mlblUsename);
            this.Controls.Add(this.mlblUserAccountType);
            this.Controls.Add(this.mbtnRefresh);
            this.Controls.Add(this.mbtnSave);
            this.Controls.Add(this.mtxtConfirmPwd);
            this.Controls.Add(this.mtxtPassword);
            this.Controls.Add(this.mtxtUsername);
            this.Controls.Add(this.mcmbUserAccountType);
            this.Controls.Add(this.hlTitle);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUserAccount";
            this.Resizable = false;
            this.Load += new System.EventHandler(this.FrmUserAccount_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmUserAccount_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbIncorrectPwd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPassword)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Drawing.Html.HtmlLabel hlTitle;
        private MetroFramework.Controls.MetroComboBox mcmbUserAccountType;
        private MetroFramework.Controls.MetroTextBox mtxtUsername;
        private MetroFramework.Controls.MetroTextBox mtxtPassword;
        private MetroFramework.Controls.MetroTextBox mtxtConfirmPwd;
        private MetroFramework.Controls.MetroButton mbtnRefresh;
        private MetroFramework.Controls.MetroButton mbtnSave;
        private MetroFramework.Controls.MetroLabel mlblUserAccountType;
        private MetroFramework.Controls.MetroLabel mlblUsename;
        private MetroFramework.Controls.MetroLabel mlblPassword;
        private MetroFramework.Controls.MetroLabel mlblConfirmPwd;
        private MetroFramework.Controls.MetroButton mbtnDelete;
        private MetroFramework.Controls.MetroButton mbtnList;
        private System.Windows.Forms.PictureBox pbPassword;
        private MetroFramework.Controls.MetroButton mbEmployee;
        private MetroFramework.Controls.MetroTextBox mtxtEmployee;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.PictureBox pbIncorrectPwd;
    }
}