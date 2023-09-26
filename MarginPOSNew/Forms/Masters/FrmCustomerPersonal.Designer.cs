namespace MarginPOSNew.Forms.Masters
{
    partial class FrmCustomerPersonal
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
            this.hlTitle = new MetroFramework.Drawing.Html.HtmlLabel();
            this.mtxtFirstName = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.mtxtLastName = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.mtxtCustomerCode = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.mdtDob = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel8 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel12 = new MetroFramework.Controls.MetroLabel();
            this.mrbtnFemale = new MetroFramework.Controls.MetroRadioButton();
            this.mrbtnMale = new MetroFramework.Controls.MetroRadioButton();
            this.mcmbMaritalStatus = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel13 = new MetroFramework.Controls.MetroLabel();
            this.mtxtAddress = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel6 = new MetroFramework.Controls.MetroLabel();
            this.mtxtTelephone = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel9 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel11 = new MetroFramework.Controls.MetroLabel();
            this.mtxtEmail = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel10 = new MetroFramework.Controls.MetroLabel();
            this.mtxtMobile = new MetroFramework.Controls.MetroTextBox();
            this.mbtnList = new MetroFramework.Controls.MetroButton();
            this.mbtnDelete = new MetroFramework.Controls.MetroButton();
            this.mbtnRefresh = new MetroFramework.Controls.MetroButton();
            this.mbtnSave = new MetroFramework.Controls.MetroButton();
            this.mtxtICardNo = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel46 = new MetroFramework.Controls.MetroLabel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.metroLabel47 = new MetroFramework.Controls.MetroLabel();
            this.mpbRemove = new System.Windows.Forms.PictureBox();
            this.mpbAdd = new System.Windows.Forms.PictureBox();
            this.pbCustomer = new System.Windows.Forms.PictureBox();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mpbRemove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mpbAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomer)).BeginInit();
            this.SuspendLayout();
            // 
            // hlTitle
            // 
            this.hlTitle.AutoScroll = true;
            this.hlTitle.AutoScrollMinSize = new System.Drawing.Size(94, 36);
            this.hlTitle.AutoSize = false;
            this.hlTitle.BackColor = System.Drawing.SystemColors.Window;
            this.hlTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlTitle.Location = new System.Drawing.Point(15, 13);
            this.hlTitle.Name = "hlTitle";
            this.hlTitle.Size = new System.Drawing.Size(186, 38);
            this.hlTitle.TabIndex = 2;
            this.hlTitle.TabStop = false;
            this.hlTitle.Text = "Customer";
            // 
            // mtxtFirstName
            // 
            // 
            // 
            // 
            this.mtxtFirstName.CustomButton.Image = null;
            this.mtxtFirstName.CustomButton.Location = new System.Drawing.Point(233, 1);
            this.mtxtFirstName.CustomButton.Name = "";
            this.mtxtFirstName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtFirstName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtFirstName.CustomButton.TabIndex = 1;
            this.mtxtFirstName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtFirstName.CustomButton.UseSelectable = true;
            this.mtxtFirstName.CustomButton.Visible = false;
            this.mtxtFirstName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtFirstName.Lines = new string[0];
            this.mtxtFirstName.Location = new System.Drawing.Point(164, 237);
            this.mtxtFirstName.MaxLength = 50;
            this.mtxtFirstName.Name = "mtxtFirstName";
            this.mtxtFirstName.PasswordChar = '\0';
            this.mtxtFirstName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtFirstName.SelectedText = "";
            this.mtxtFirstName.SelectionLength = 0;
            this.mtxtFirstName.SelectionStart = 0;
            this.mtxtFirstName.ShortcutsEnabled = true;
            this.mtxtFirstName.Size = new System.Drawing.Size(255, 23);
            this.mtxtFirstName.TabIndex = 1;
            this.mtxtFirstName.Tag = "Customer Name";
            this.mtxtFirstName.UseSelectable = true;
            this.mtxtFirstName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtFirstName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.Location = new System.Drawing.Point(72, 235);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(75, 19);
            this.metroLabel1.TabIndex = 27;
            this.metroLabel1.Text = "First Name";
            // 
            // mtxtLastName
            // 
            // 
            // 
            // 
            this.mtxtLastName.CustomButton.Image = null;
            this.mtxtLastName.CustomButton.Location = new System.Drawing.Point(232, 1);
            this.mtxtLastName.CustomButton.Name = "";
            this.mtxtLastName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtLastName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtLastName.CustomButton.TabIndex = 1;
            this.mtxtLastName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtLastName.CustomButton.UseSelectable = true;
            this.mtxtLastName.CustomButton.Visible = false;
            this.mtxtLastName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtLastName.Lines = new string[0];
            this.mtxtLastName.Location = new System.Drawing.Point(164, 263);
            this.mtxtLastName.MaxLength = 50;
            this.mtxtLastName.Name = "mtxtLastName";
            this.mtxtLastName.PasswordChar = '\0';
            this.mtxtLastName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtLastName.SelectedText = "";
            this.mtxtLastName.SelectionLength = 0;
            this.mtxtLastName.SelectionStart = 0;
            this.mtxtLastName.ShortcutsEnabled = true;
            this.mtxtLastName.Size = new System.Drawing.Size(254, 23);
            this.mtxtLastName.TabIndex = 2;
            this.mtxtLastName.Tag = "Last Name";
            this.mtxtLastName.UseSelectable = true;
            this.mtxtLastName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtLastName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.ForeColor = System.Drawing.Color.Black;
            this.metroLabel2.Location = new System.Drawing.Point(73, 261);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(74, 19);
            this.metroLabel2.TabIndex = 29;
            this.metroLabel2.Text = "Last Name";
            // 
            // mtxtCustomerCode
            // 
            // 
            // 
            // 
            this.mtxtCustomerCode.CustomButton.Image = null;
            this.mtxtCustomerCode.CustomButton.Location = new System.Drawing.Point(233, 1);
            this.mtxtCustomerCode.CustomButton.Name = "";
            this.mtxtCustomerCode.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtCustomerCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtCustomerCode.CustomButton.TabIndex = 1;
            this.mtxtCustomerCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtCustomerCode.CustomButton.UseSelectable = true;
            this.mtxtCustomerCode.CustomButton.Visible = false;
            this.mtxtCustomerCode.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtCustomerCode.Lines = new string[0];
            this.mtxtCustomerCode.Location = new System.Drawing.Point(164, 210);
            this.mtxtCustomerCode.MaxLength = 50;
            this.mtxtCustomerCode.Name = "mtxtCustomerCode";
            this.mtxtCustomerCode.PasswordChar = '\0';
            this.mtxtCustomerCode.ReadOnly = true;
            this.mtxtCustomerCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtCustomerCode.SelectedText = "";
            this.mtxtCustomerCode.SelectionLength = 0;
            this.mtxtCustomerCode.SelectionStart = 0;
            this.mtxtCustomerCode.ShortcutsEnabled = true;
            this.mtxtCustomerCode.Size = new System.Drawing.Size(255, 23);
            this.mtxtCustomerCode.TabIndex = 100;
            this.mtxtCustomerCode.TabStop = false;
            this.mtxtCustomerCode.Tag = "Customer Code";
            this.mtxtCustomerCode.UseSelectable = true;
            this.mtxtCustomerCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtCustomerCode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.ForeColor = System.Drawing.Color.Black;
            this.metroLabel3.Location = new System.Drawing.Point(42, 209);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(105, 19);
            this.metroLabel3.TabIndex = 31;
            this.metroLabel3.Text = "Customer Code";
            // 
            // mdtDob
            // 
            this.mdtDob.Location = new System.Drawing.Point(577, 56);
            this.mdtDob.MinimumSize = new System.Drawing.Size(0, 29);
            this.mdtDob.Name = "mdtDob";
            this.mdtDob.Size = new System.Drawing.Size(255, 29);
            this.mdtDob.TabIndex = 4;
            this.mdtDob.Tag = "Date of Birth";
            // 
            // metroLabel8
            // 
            this.metroLabel8.AutoSize = true;
            this.metroLabel8.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel8.ForeColor = System.Drawing.Color.Black;
            this.metroLabel8.Location = new System.Drawing.Point(476, 60);
            this.metroLabel8.Name = "metroLabel8";
            this.metroLabel8.Size = new System.Drawing.Size(87, 19);
            this.metroLabel8.TabIndex = 42;
            this.metroLabel8.Text = "Date of Birth";
            // 
            // metroLabel12
            // 
            this.metroLabel12.AutoSize = true;
            this.metroLabel12.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel12.ForeColor = System.Drawing.Color.Black;
            this.metroLabel12.Location = new System.Drawing.Point(509, 95);
            this.metroLabel12.Name = "metroLabel12";
            this.metroLabel12.Size = new System.Drawing.Size(54, 19);
            this.metroLabel12.TabIndex = 61;
            this.metroLabel12.Text = "Gender";
            // 
            // mrbtnFemale
            // 
            this.mrbtnFemale.AutoSize = true;
            this.mrbtnFemale.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.mrbtnFemale.Location = new System.Drawing.Point(103, 3);
            this.mrbtnFemale.Name = "mrbtnFemale";
            this.mrbtnFemale.Size = new System.Drawing.Size(68, 19);
            this.mrbtnFemale.TabIndex = 2;
            this.mrbtnFemale.Text = "Female";
            this.mrbtnFemale.UseSelectable = true;
            // 
            // mrbtnMale
            // 
            this.mrbtnMale.AutoSize = true;
            this.mrbtnMale.Checked = true;
            this.mrbtnMale.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.mrbtnMale.Location = new System.Drawing.Point(5, 3);
            this.mrbtnMale.Name = "mrbtnMale";
            this.mrbtnMale.Size = new System.Drawing.Size(55, 19);
            this.mrbtnMale.TabIndex = 1;
            this.mrbtnMale.TabStop = true;
            this.mrbtnMale.Text = "Male";
            this.mrbtnMale.UseSelectable = true;
            // 
            // mcmbMaritalStatus
            // 
            this.mcmbMaritalStatus.FormattingEnabled = true;
            this.mcmbMaritalStatus.ItemHeight = 23;
            this.mcmbMaritalStatus.Location = new System.Drawing.Point(577, 126);
            this.mcmbMaritalStatus.Name = "mcmbMaritalStatus";
            this.mcmbMaritalStatus.Size = new System.Drawing.Size(254, 29);
            this.mcmbMaritalStatus.TabIndex = 7;
            this.mcmbMaritalStatus.Tag = "Marital Status";
            this.mcmbMaritalStatus.UseSelectable = true;
            // 
            // metroLabel13
            // 
            this.metroLabel13.AutoSize = true;
            this.metroLabel13.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel13.ForeColor = System.Drawing.Color.Black;
            this.metroLabel13.Location = new System.Drawing.Point(469, 130);
            this.metroLabel13.Name = "metroLabel13";
            this.metroLabel13.Size = new System.Drawing.Size(94, 19);
            this.metroLabel13.TabIndex = 63;
            this.metroLabel13.Text = "Marital Status";
            // 
            // mtxtAddress
            // 
            // 
            // 
            // 
            this.mtxtAddress.CustomButton.Image = null;
            this.mtxtAddress.CustomButton.Location = new System.Drawing.Point(182, 1);
            this.mtxtAddress.CustomButton.Name = "";
            this.mtxtAddress.CustomButton.Size = new System.Drawing.Size(71, 71);
            this.mtxtAddress.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtAddress.CustomButton.TabIndex = 1;
            this.mtxtAddress.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtAddress.CustomButton.UseSelectable = true;
            this.mtxtAddress.CustomButton.Visible = false;
            this.mtxtAddress.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtAddress.Lines = new string[0];
            this.mtxtAddress.Location = new System.Drawing.Point(577, 161);
            this.mtxtAddress.MaxLength = 50;
            this.mtxtAddress.Multiline = true;
            this.mtxtAddress.Name = "mtxtAddress";
            this.mtxtAddress.PasswordChar = '\0';
            this.mtxtAddress.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtAddress.SelectedText = "";
            this.mtxtAddress.SelectionLength = 0;
            this.mtxtAddress.SelectionStart = 0;
            this.mtxtAddress.ShortcutsEnabled = true;
            this.mtxtAddress.Size = new System.Drawing.Size(254, 73);
            this.mtxtAddress.TabIndex = 8;
            this.mtxtAddress.Tag = "Address";
            this.mtxtAddress.UseSelectable = true;
            this.mtxtAddress.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtAddress.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel6
            // 
            this.metroLabel6.AutoSize = true;
            this.metroLabel6.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel6.ForeColor = System.Drawing.Color.Black;
            this.metroLabel6.Location = new System.Drawing.Point(505, 161);
            this.metroLabel6.Name = "metroLabel6";
            this.metroLabel6.Size = new System.Drawing.Size(58, 19);
            this.metroLabel6.TabIndex = 65;
            this.metroLabel6.Text = "Address";
            // 
            // mtxtTelephone
            // 
            // 
            // 
            // 
            this.mtxtTelephone.CustomButton.Image = null;
            this.mtxtTelephone.CustomButton.Location = new System.Drawing.Point(232, 1);
            this.mtxtTelephone.CustomButton.Name = "";
            this.mtxtTelephone.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtTelephone.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtTelephone.CustomButton.TabIndex = 1;
            this.mtxtTelephone.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtTelephone.CustomButton.UseSelectable = true;
            this.mtxtTelephone.CustomButton.Visible = false;
            this.mtxtTelephone.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtTelephone.Lines = new string[0];
            this.mtxtTelephone.Location = new System.Drawing.Point(577, 237);
            this.mtxtTelephone.MaxLength = 50;
            this.mtxtTelephone.Name = "mtxtTelephone";
            this.mtxtTelephone.PasswordChar = '\0';
            this.mtxtTelephone.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtTelephone.SelectedText = "";
            this.mtxtTelephone.SelectionLength = 0;
            this.mtxtTelephone.SelectionStart = 0;
            this.mtxtTelephone.ShortcutsEnabled = true;
            this.mtxtTelephone.Size = new System.Drawing.Size(254, 23);
            this.mtxtTelephone.TabIndex = 9;
            this.mtxtTelephone.Tag = "Telephone";
            this.mtxtTelephone.UseSelectable = true;
            this.mtxtTelephone.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtTelephone.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel9
            // 
            this.metroLabel9.AutoSize = true;
            this.metroLabel9.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel9.ForeColor = System.Drawing.Color.Black;
            this.metroLabel9.Location = new System.Drawing.Point(492, 235);
            this.metroLabel9.Name = "metroLabel9";
            this.metroLabel9.Size = new System.Drawing.Size(72, 19);
            this.metroLabel9.TabIndex = 67;
            this.metroLabel9.Text = "Telephone";
            // 
            // metroLabel11
            // 
            this.metroLabel11.AutoSize = true;
            this.metroLabel11.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel11.ForeColor = System.Drawing.Color.Black;
            this.metroLabel11.Location = new System.Drawing.Point(522, 288);
            this.metroLabel11.Name = "metroLabel11";
            this.metroLabel11.Size = new System.Drawing.Size(41, 19);
            this.metroLabel11.TabIndex = 71;
            this.metroLabel11.Text = "Email";
            // 
            // mtxtEmail
            // 
            // 
            // 
            // 
            this.mtxtEmail.CustomButton.Image = null;
            this.mtxtEmail.CustomButton.Location = new System.Drawing.Point(232, 1);
            this.mtxtEmail.CustomButton.Name = "";
            this.mtxtEmail.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtEmail.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtEmail.CustomButton.TabIndex = 1;
            this.mtxtEmail.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtEmail.CustomButton.UseSelectable = true;
            this.mtxtEmail.CustomButton.Visible = false;
            this.mtxtEmail.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtEmail.Lines = new string[0];
            this.mtxtEmail.Location = new System.Drawing.Point(577, 290);
            this.mtxtEmail.MaxLength = 50;
            this.mtxtEmail.Name = "mtxtEmail";
            this.mtxtEmail.PasswordChar = '\0';
            this.mtxtEmail.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtEmail.SelectedText = "";
            this.mtxtEmail.SelectionLength = 0;
            this.mtxtEmail.SelectionStart = 0;
            this.mtxtEmail.ShortcutsEnabled = true;
            this.mtxtEmail.Size = new System.Drawing.Size(254, 23);
            this.mtxtEmail.TabIndex = 11;
            this.mtxtEmail.Tag = "Email";
            this.mtxtEmail.UseSelectable = true;
            this.mtxtEmail.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtEmail.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel10
            // 
            this.metroLabel10.AutoSize = true;
            this.metroLabel10.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel10.ForeColor = System.Drawing.Color.Black;
            this.metroLabel10.Location = new System.Drawing.Point(512, 261);
            this.metroLabel10.Name = "metroLabel10";
            this.metroLabel10.Size = new System.Drawing.Size(51, 19);
            this.metroLabel10.TabIndex = 70;
            this.metroLabel10.Text = "Mobile";
            // 
            // mtxtMobile
            // 
            // 
            // 
            // 
            this.mtxtMobile.CustomButton.Image = null;
            this.mtxtMobile.CustomButton.Location = new System.Drawing.Point(232, 1);
            this.mtxtMobile.CustomButton.Name = "";
            this.mtxtMobile.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtMobile.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtMobile.CustomButton.TabIndex = 1;
            this.mtxtMobile.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtMobile.CustomButton.UseSelectable = true;
            this.mtxtMobile.CustomButton.Visible = false;
            this.mtxtMobile.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtMobile.Lines = new string[0];
            this.mtxtMobile.Location = new System.Drawing.Point(577, 263);
            this.mtxtMobile.MaxLength = 50;
            this.mtxtMobile.Name = "mtxtMobile";
            this.mtxtMobile.PasswordChar = '\0';
            this.mtxtMobile.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtMobile.SelectedText = "";
            this.mtxtMobile.SelectionLength = 0;
            this.mtxtMobile.SelectionStart = 0;
            this.mtxtMobile.ShortcutsEnabled = true;
            this.mtxtMobile.Size = new System.Drawing.Size(254, 23);
            this.mtxtMobile.TabIndex = 10;
            this.mtxtMobile.Tag = "Mobile";
            this.mtxtMobile.UseSelectable = true;
            this.mtxtMobile.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtMobile.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtMobile.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MtxtMobile_KeyPress);
            // 
            // mbtnList
            // 
            this.mbtnList.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.mbtnList.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnList.ForeColor = System.Drawing.Color.White;
            this.mbtnList.Location = new System.Drawing.Point(357, 340);
            this.mbtnList.Name = "mbtnList";
            this.mbtnList.Size = new System.Drawing.Size(150, 45);
            this.mbtnList.TabIndex = 13;
            this.mbtnList.Text = "&Search";
            this.mbtnList.UseCustomBackColor = true;
            this.mbtnList.UseCustomForeColor = true;
            this.mbtnList.UseSelectable = true;
            this.mbtnList.Click += new System.EventHandler(this.MbtnList_Click);
            // 
            // mbtnDelete
            // 
            this.mbtnDelete.BackColor = System.Drawing.Color.Firebrick;
            this.mbtnDelete.Enabled = false;
            this.mbtnDelete.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnDelete.ForeColor = System.Drawing.Color.White;
            this.mbtnDelete.Location = new System.Drawing.Point(679, 340);
            this.mbtnDelete.Name = "mbtnDelete";
            this.mbtnDelete.Size = new System.Drawing.Size(150, 45);
            this.mbtnDelete.TabIndex = 15;
            this.mbtnDelete.Text = "&Delete";
            this.mbtnDelete.UseCustomBackColor = true;
            this.mbtnDelete.UseCustomForeColor = true;
            this.mbtnDelete.UseSelectable = true;
            this.mbtnDelete.Click += new System.EventHandler(this.MbtnDelete_Click);
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.mbtnRefresh.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnRefresh.ForeColor = System.Drawing.Color.White;
            this.mbtnRefresh.Location = new System.Drawing.Point(518, 340);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(150, 45);
            this.mbtnRefresh.TabIndex = 14;
            this.mbtnRefresh.Text = "&Refresh";
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
            this.mbtnSave.Location = new System.Drawing.Point(196, 340);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(150, 45);
            this.mbtnSave.TabIndex = 12;
            this.mbtnSave.Text = "&Save";
            this.mbtnSave.UseCustomBackColor = true;
            this.mbtnSave.UseCustomForeColor = true;
            this.mbtnSave.UseSelectable = true;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            // 
            // mtxtICardNo
            // 
            this.mtxtICardNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // 
            // 
            this.mtxtICardNo.CustomButton.Image = null;
            this.mtxtICardNo.CustomButton.Location = new System.Drawing.Point(232, 1);
            this.mtxtICardNo.CustomButton.Name = "";
            this.mtxtICardNo.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtICardNo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtICardNo.CustomButton.TabIndex = 1;
            this.mtxtICardNo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtICardNo.CustomButton.UseSelectable = true;
            this.mtxtICardNo.CustomButton.Visible = false;
            this.mtxtICardNo.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtICardNo.Lines = new string[0];
            this.mtxtICardNo.Location = new System.Drawing.Point(164, 290);
            this.mtxtICardNo.MaxLength = 50;
            this.mtxtICardNo.Name = "mtxtICardNo";
            this.mtxtICardNo.PasswordChar = '\0';
            this.mtxtICardNo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtICardNo.SelectedText = "";
            this.mtxtICardNo.SelectionLength = 0;
            this.mtxtICardNo.SelectionStart = 0;
            this.mtxtICardNo.ShortcutsEnabled = true;
            this.mtxtICardNo.Size = new System.Drawing.Size(254, 23);
            this.mtxtICardNo.TabIndex = 3;
            this.mtxtICardNo.Tag = "ICard No";
            this.mtxtICardNo.UseSelectable = true;
            this.mtxtICardNo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtICardNo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel46
            // 
            this.metroLabel46.AutoSize = true;
            this.metroLabel46.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel46.ForeColor = System.Drawing.Color.Black;
            this.metroLabel46.Location = new System.Drawing.Point(83, 288);
            this.metroLabel46.Name = "metroLabel46";
            this.metroLabel46.Size = new System.Drawing.Size(64, 19);
            this.metroLabel46.TabIndex = 144;
            this.metroLabel46.Text = "ICard No";
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.metroPanel1.Controls.Add(this.mrbtnMale);
            this.metroPanel1.Controls.Add(this.mrbtnFemale);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(577, 90);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(254, 30);
            this.metroPanel1.TabIndex = 5;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(420, 245);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 15);
            this.label1.TabIndex = 149;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(830, 270);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 15);
            this.label2.TabIndex = 150;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(831, 295);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 15);
            this.label3.TabIndex = 151;
            this.label3.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(831, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 15);
            this.label4.TabIndex = 152;
            this.label4.Text = "*";
            // 
            // metroLabel47
            // 
            this.metroLabel47.AutoSize = true;
            this.metroLabel47.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel47.Location = new System.Drawing.Point(40, 126);
            this.metroLabel47.Name = "metroLabel47";
            this.metroLabel47.Size = new System.Drawing.Size(111, 19);
            this.metroLabel47.TabIndex = 153;
            this.metroLabel47.Text = "Customer Image";
            // 
            // mpbRemove
            // 
            this.mpbRemove.Image = global::MarginPOSNew.Properties.Resources.remove_btn;
            this.mpbRemove.Location = new System.Drawing.Point(331, 175);
            this.mpbRemove.Name = "mpbRemove";
            this.mpbRemove.Size = new System.Drawing.Size(30, 30);
            this.mpbRemove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mpbRemove.TabIndex = 155;
            this.mpbRemove.TabStop = false;
            this.mpbRemove.Click += new System.EventHandler(this.MpbRemove_Click);
            // 
            // mpbAdd
            // 
            this.mpbAdd.Image = global::MarginPOSNew.Properties.Resources.add_btn;
            this.mpbAdd.Location = new System.Drawing.Point(331, 143);
            this.mpbAdd.Name = "mpbAdd";
            this.mpbAdd.Size = new System.Drawing.Size(30, 30);
            this.mpbAdd.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mpbAdd.TabIndex = 154;
            this.mpbAdd.TabStop = false;
            this.mpbAdd.Click += new System.EventHandler(this.MpbAdd_Click);
            // 
            // pbCustomer
            // 
            this.pbCustomer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbCustomer.Location = new System.Drawing.Point(163, 56);
            this.pbCustomer.Name = "pbCustomer";
            this.pbCustomer.Size = new System.Drawing.Size(150, 150);
            this.pbCustomer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbCustomer.TabIndex = 145;
            this.pbCustomer.TabStop = false;
            // 
            // FrmCustomerPersonal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(863, 405);
            this.Controls.Add(this.metroLabel47);
            this.Controls.Add(this.mpbRemove);
            this.Controls.Add(this.mpbAdd);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.pbCustomer);
            this.Controls.Add(this.metroLabel46);
            this.Controls.Add(this.mtxtICardNo);
            this.Controls.Add(this.mbtnList);
            this.Controls.Add(this.mbtnDelete);
            this.Controls.Add(this.mbtnRefresh);
            this.Controls.Add(this.mbtnSave);
            this.Controls.Add(this.metroLabel11);
            this.Controls.Add(this.mtxtEmail);
            this.Controls.Add(this.metroLabel10);
            this.Controls.Add(this.mtxtMobile);
            this.Controls.Add(this.metroLabel9);
            this.Controls.Add(this.mtxtTelephone);
            this.Controls.Add(this.metroLabel6);
            this.Controls.Add(this.mtxtAddress);
            this.Controls.Add(this.metroLabel13);
            this.Controls.Add(this.mcmbMaritalStatus);
            this.Controls.Add(this.metroLabel12);
            this.Controls.Add(this.metroLabel8);
            this.Controls.Add(this.mdtDob);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.mtxtCustomerCode);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.mtxtLastName);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.mtxtFirstName);
            this.Controls.Add(this.hlTitle);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCustomerPersonal";
            this.Resizable = false;
            this.Load += new System.EventHandler(this.FrmCustomerPersonal_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCustomerPersonal_KeyDown);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mpbRemove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mpbAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCustomer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Drawing.Html.HtmlLabel hlTitle;
        private MetroFramework.Controls.MetroTextBox mtxtFirstName;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox mtxtLastName;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox mtxtCustomerCode;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroDateTime mdtDob;
        private MetroFramework.Controls.MetroLabel metroLabel8;
        private MetroFramework.Controls.MetroLabel metroLabel12;
        private MetroFramework.Controls.MetroRadioButton mrbtnFemale;
        private MetroFramework.Controls.MetroRadioButton mrbtnMale;
        private MetroFramework.Controls.MetroComboBox mcmbMaritalStatus;
        private MetroFramework.Controls.MetroLabel metroLabel13;
        private MetroFramework.Controls.MetroTextBox mtxtAddress;
        private MetroFramework.Controls.MetroLabel metroLabel6;
        private MetroFramework.Controls.MetroTextBox mtxtTelephone;
        private MetroFramework.Controls.MetroLabel metroLabel9;
        private MetroFramework.Controls.MetroLabel metroLabel11;
        private MetroFramework.Controls.MetroTextBox mtxtEmail;
        private MetroFramework.Controls.MetroLabel metroLabel10;
        private MetroFramework.Controls.MetroTextBox mtxtMobile;
        private MetroFramework.Controls.MetroButton mbtnList;
        private MetroFramework.Controls.MetroButton mbtnDelete;
        private MetroFramework.Controls.MetroButton mbtnRefresh;
        private MetroFramework.Controls.MetroButton mbtnSave;
        private MetroFramework.Controls.MetroTextBox mtxtICardNo;
        private MetroFramework.Controls.MetroLabel metroLabel46;
        private System.Windows.Forms.PictureBox pbCustomer;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroLabel metroLabel47;
        private System.Windows.Forms.PictureBox mpbRemove;
        private System.Windows.Forms.PictureBox mpbAdd;
    }
}