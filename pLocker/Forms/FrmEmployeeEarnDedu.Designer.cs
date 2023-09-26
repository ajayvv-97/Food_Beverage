namespace pLocker.Forms
{
    partial class FrmEmployeeEarnDedu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmployeeEarnDedu));
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.mtxtEmployee = new MetroFramework.Controls.MetroTextBox();
            this.mbEmployee = new MetroFramework.Controls.MetroButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mtxtEarnedAmount = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel38 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.mtxtEarnedDescription = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel13 = new MetroFramework.Controls.MetroLabel();
            this.mcmbEarnType = new MetroFramework.Controls.MetroComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.mtxtDeductedAmount = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel5 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.mcmbDeductedType = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.mtxtDeductDescription = new MetroFramework.Controls.MetroTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnMin = new System.Windows.Forms.Button();
            this.hlTitle = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.mbtnClose = new System.Windows.Forms.Button();
            this.mbtnDelete = new System.Windows.Forms.Button();
            this.mbtnRefresh = new System.Windows.Forms.Button();
            this.mbtnList = new System.Windows.Forms.Button();
            this.mbtnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.ForeColor = System.Drawing.Color.Silver;
            this.metroLabel2.Location = new System.Drawing.Point(10, 36);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(99, 25);
            this.metroLabel2.TabIndex = 106;
            this.metroLabel2.Text = "Employee :";
            this.metroLabel2.UseCustomBackColor = true;
            this.metroLabel2.UseCustomForeColor = true;
            // 
            // mtxtEmployee
            // 
            // 
            // 
            // 
            this.mtxtEmployee.CustomButton.Image = null;
            this.mtxtEmployee.CustomButton.Location = new System.Drawing.Point(192, 1);
            this.mtxtEmployee.CustomButton.Name = "";
            this.mtxtEmployee.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.mtxtEmployee.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtEmployee.CustomButton.TabIndex = 1;
            this.mtxtEmployee.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtEmployee.CustomButton.UseSelectable = true;
            this.mtxtEmployee.CustomButton.Visible = false;
            this.mtxtEmployee.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtEmployee.Lines = new string[0];
            this.mtxtEmployee.Location = new System.Drawing.Point(113, 37);
            this.mtxtEmployee.MaxLength = 32767;
            this.mtxtEmployee.Name = "mtxtEmployee";
            this.mtxtEmployee.PasswordChar = '\0';
            this.mtxtEmployee.ReadOnly = true;
            this.mtxtEmployee.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtEmployee.SelectedText = "";
            this.mtxtEmployee.SelectionLength = 0;
            this.mtxtEmployee.SelectionStart = 0;
            this.mtxtEmployee.ShortcutsEnabled = true;
            this.mtxtEmployee.Size = new System.Drawing.Size(220, 29);
            this.mtxtEmployee.TabIndex = 0;
            this.mtxtEmployee.Tag = "Employee name";
            this.mtxtEmployee.UseSelectable = true;
            this.mtxtEmployee.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtEmployee.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // mbEmployee
            // 
            this.mbEmployee.Location = new System.Drawing.Point(339, 37);
            this.mbEmployee.Name = "mbEmployee";
            this.mbEmployee.Size = new System.Drawing.Size(26, 29);
            this.mbEmployee.TabIndex = 1;
            this.mbEmployee.TabStop = false;
            this.mbEmployee.Text = "...";
            this.mbEmployee.UseSelectable = true;
            this.mbEmployee.Visible = false;
            this.mbEmployee.Click += new System.EventHandler(this.MbEmployee_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.mtxtEarnedAmount);
            this.groupBox1.Controls.Add(this.metroLabel38);
            this.groupBox1.Controls.Add(this.metroLabel1);
            this.groupBox1.Controls.Add(this.mtxtEarnedDescription);
            this.groupBox1.Controls.Add(this.metroLabel13);
            this.groupBox1.Controls.Add(this.mcmbEarnType);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Silver;
            this.groupBox1.Location = new System.Drawing.Point(16, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 165);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Earn";
            // 
            // mtxtEarnedAmount
            // 
            // 
            // 
            // 
            this.mtxtEarnedAmount.CustomButton.Image = null;
            this.mtxtEarnedAmount.CustomButton.Location = new System.Drawing.Point(183, 1);
            this.mtxtEarnedAmount.CustomButton.Name = "";
            this.mtxtEarnedAmount.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.mtxtEarnedAmount.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtEarnedAmount.CustomButton.TabIndex = 1;
            this.mtxtEarnedAmount.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtEarnedAmount.CustomButton.UseSelectable = true;
            this.mtxtEarnedAmount.CustomButton.Visible = false;
            this.mtxtEarnedAmount.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtEarnedAmount.Lines = new string[0];
            this.mtxtEarnedAmount.Location = new System.Drawing.Point(106, 76);
            this.mtxtEarnedAmount.MaxLength = 50;
            this.mtxtEarnedAmount.Name = "mtxtEarnedAmount";
            this.mtxtEarnedAmount.PasswordChar = '\0';
            this.mtxtEarnedAmount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtEarnedAmount.SelectedText = "";
            this.mtxtEarnedAmount.SelectionLength = 0;
            this.mtxtEarnedAmount.SelectionStart = 0;
            this.mtxtEarnedAmount.ShortcutsEnabled = true;
            this.mtxtEarnedAmount.Size = new System.Drawing.Size(211, 29);
            this.mtxtEarnedAmount.TabIndex = 1;
            this.mtxtEarnedAmount.Tag = "Earned Amount";
            this.mtxtEarnedAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mtxtEarnedAmount.UseSelectable = true;
            this.mtxtEarnedAmount.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtEarnedAmount.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel38
            // 
            this.metroLabel38.AutoSize = true;
            this.metroLabel38.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel38.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel38.ForeColor = System.Drawing.Color.Silver;
            this.metroLabel38.Location = new System.Drawing.Point(37, 79);
            this.metroLabel38.Name = "metroLabel38";
            this.metroLabel38.Size = new System.Drawing.Size(66, 19);
            this.metroLabel38.TabIndex = 104;
            this.metroLabel38.Text = "Amount :";
            this.metroLabel38.UseCustomBackColor = true;
            this.metroLabel38.UseCustomForeColor = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.ForeColor = System.Drawing.Color.Silver;
            this.metroLabel1.Location = new System.Drawing.Point(18, 116);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(85, 19);
            this.metroLabel1.TabIndex = 67;
            this.metroLabel1.Text = "Description :";
            this.metroLabel1.UseCustomBackColor = true;
            this.metroLabel1.UseCustomForeColor = true;
            // 
            // mtxtEarnedDescription
            // 
            // 
            // 
            // 
            this.mtxtEarnedDescription.CustomButton.Image = null;
            this.mtxtEarnedDescription.CustomButton.Location = new System.Drawing.Point(183, 1);
            this.mtxtEarnedDescription.CustomButton.Name = "";
            this.mtxtEarnedDescription.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.mtxtEarnedDescription.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtEarnedDescription.CustomButton.TabIndex = 1;
            this.mtxtEarnedDescription.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtEarnedDescription.CustomButton.UseSelectable = true;
            this.mtxtEarnedDescription.CustomButton.Visible = false;
            this.mtxtEarnedDescription.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtEarnedDescription.Lines = new string[0];
            this.mtxtEarnedDescription.Location = new System.Drawing.Point(106, 118);
            this.mtxtEarnedDescription.MaxLength = 50;
            this.mtxtEarnedDescription.Name = "mtxtEarnedDescription";
            this.mtxtEarnedDescription.PasswordChar = '\0';
            this.mtxtEarnedDescription.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtEarnedDescription.SelectedText = "";
            this.mtxtEarnedDescription.SelectionLength = 0;
            this.mtxtEarnedDescription.SelectionStart = 0;
            this.mtxtEarnedDescription.ShortcutsEnabled = true;
            this.mtxtEarnedDescription.Size = new System.Drawing.Size(211, 29);
            this.mtxtEarnedDescription.TabIndex = 2;
            this.mtxtEarnedDescription.Tag = "Earned Description";
            this.mtxtEarnedDescription.UseSelectable = true;
            this.mtxtEarnedDescription.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtEarnedDescription.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel13
            // 
            this.metroLabel13.AutoSize = true;
            this.metroLabel13.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel13.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel13.ForeColor = System.Drawing.Color.Silver;
            this.metroLabel13.Location = new System.Drawing.Point(27, 38);
            this.metroLabel13.Name = "metroLabel13";
            this.metroLabel13.Size = new System.Drawing.Size(76, 19);
            this.metroLabel13.TabIndex = 65;
            this.metroLabel13.Text = "Earn Type :";
            this.metroLabel13.UseCustomBackColor = true;
            this.metroLabel13.UseCustomForeColor = true;
            // 
            // mcmbEarnType
            // 
            this.mcmbEarnType.BackColor = System.Drawing.Color.White;
            this.mcmbEarnType.DropDownHeight = 250;
            this.mcmbEarnType.ForeColor = System.Drawing.Color.Black;
            this.mcmbEarnType.FormattingEnabled = true;
            this.mcmbEarnType.IntegralHeight = false;
            this.mcmbEarnType.ItemHeight = 23;
            this.mcmbEarnType.Location = new System.Drawing.Point(106, 34);
            this.mcmbEarnType.Name = "mcmbEarnType";
            this.mcmbEarnType.Size = new System.Drawing.Size(211, 29);
            this.mcmbEarnType.TabIndex = 0;
            this.mcmbEarnType.Tag = "Earn Type";
            this.mcmbEarnType.UseCustomBackColor = true;
            this.mcmbEarnType.UseCustomForeColor = true;
            this.mcmbEarnType.UseSelectable = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.mtxtDeductedAmount);
            this.groupBox2.Controls.Add(this.metroLabel5);
            this.groupBox2.Controls.Add(this.metroLabel4);
            this.groupBox2.Controls.Add(this.mcmbDeductedType);
            this.groupBox2.Controls.Add(this.metroLabel3);
            this.groupBox2.Controls.Add(this.mtxtDeductDescription);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.Silver;
            this.groupBox2.Location = new System.Drawing.Point(14, 255);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(351, 165);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Deduct";
            // 
            // mtxtDeductedAmount
            // 
            // 
            // 
            // 
            this.mtxtDeductedAmount.CustomButton.Image = null;
            this.mtxtDeductedAmount.CustomButton.Location = new System.Drawing.Point(169, 1);
            this.mtxtDeductedAmount.CustomButton.Name = "";
            this.mtxtDeductedAmount.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.mtxtDeductedAmount.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtDeductedAmount.CustomButton.TabIndex = 1;
            this.mtxtDeductedAmount.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtDeductedAmount.CustomButton.UseSelectable = true;
            this.mtxtDeductedAmount.CustomButton.Visible = false;
            this.mtxtDeductedAmount.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtDeductedAmount.Lines = new string[0];
            this.mtxtDeductedAmount.Location = new System.Drawing.Point(135, 76);
            this.mtxtDeductedAmount.MaxLength = 50;
            this.mtxtDeductedAmount.Name = "mtxtDeductedAmount";
            this.mtxtDeductedAmount.PasswordChar = '\0';
            this.mtxtDeductedAmount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtDeductedAmount.SelectedText = "";
            this.mtxtDeductedAmount.SelectionLength = 0;
            this.mtxtDeductedAmount.SelectionStart = 0;
            this.mtxtDeductedAmount.ShortcutsEnabled = true;
            this.mtxtDeductedAmount.Size = new System.Drawing.Size(197, 29);
            this.mtxtDeductedAmount.TabIndex = 1;
            this.mtxtDeductedAmount.Tag = "Deducted Amount";
            this.mtxtDeductedAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mtxtDeductedAmount.UseSelectable = true;
            this.mtxtDeductedAmount.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtDeductedAmount.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel5
            // 
            this.metroLabel5.AutoSize = true;
            this.metroLabel5.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.ForeColor = System.Drawing.Color.Silver;
            this.metroLabel5.Location = new System.Drawing.Point(58, 79);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(66, 19);
            this.metroLabel5.TabIndex = 106;
            this.metroLabel5.Text = "Amount :";
            this.metroLabel5.UseCustomBackColor = true;
            this.metroLabel5.UseCustomForeColor = true;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.ForeColor = System.Drawing.Color.Silver;
            this.metroLabel4.Location = new System.Drawing.Point(31, 38);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(93, 19);
            this.metroLabel4.TabIndex = 71;
            this.metroLabel4.Text = "Deduct Type :";
            this.metroLabel4.UseCustomBackColor = true;
            this.metroLabel4.UseCustomForeColor = true;
            // 
            // mcmbDeductedType
            // 
            this.mcmbDeductedType.BackColor = System.Drawing.Color.White;
            this.mcmbDeductedType.DropDownHeight = 250;
            this.mcmbDeductedType.ForeColor = System.Drawing.Color.Black;
            this.mcmbDeductedType.FormattingEnabled = true;
            this.mcmbDeductedType.IntegralHeight = false;
            this.mcmbDeductedType.ItemHeight = 23;
            this.mcmbDeductedType.Location = new System.Drawing.Point(135, 34);
            this.mcmbDeductedType.Name = "mcmbDeductedType";
            this.mcmbDeductedType.Size = new System.Drawing.Size(197, 29);
            this.mcmbDeductedType.TabIndex = 0;
            this.mcmbDeductedType.Tag = "Deducted Type";
            this.mcmbDeductedType.UseCustomBackColor = true;
            this.mcmbDeductedType.UseCustomForeColor = true;
            this.mcmbDeductedType.UseSelectable = true;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.ForeColor = System.Drawing.Color.Silver;
            this.metroLabel3.Location = new System.Drawing.Point(39, 117);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(85, 19);
            this.metroLabel3.TabIndex = 69;
            this.metroLabel3.Text = "Description :";
            this.metroLabel3.UseCustomBackColor = true;
            this.metroLabel3.UseCustomForeColor = true;
            // 
            // mtxtDeductDescription
            // 
            // 
            // 
            // 
            this.mtxtDeductDescription.CustomButton.Image = null;
            this.mtxtDeductDescription.CustomButton.Location = new System.Drawing.Point(169, 1);
            this.mtxtDeductDescription.CustomButton.Name = "";
            this.mtxtDeductDescription.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.mtxtDeductDescription.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtDeductDescription.CustomButton.TabIndex = 1;
            this.mtxtDeductDescription.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtDeductDescription.CustomButton.UseSelectable = true;
            this.mtxtDeductDescription.CustomButton.Visible = false;
            this.mtxtDeductDescription.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtDeductDescription.Lines = new string[0];
            this.mtxtDeductDescription.Location = new System.Drawing.Point(135, 118);
            this.mtxtDeductDescription.MaxLength = 50;
            this.mtxtDeductDescription.Name = "mtxtDeductDescription";
            this.mtxtDeductDescription.PasswordChar = '\0';
            this.mtxtDeductDescription.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtDeductDescription.SelectedText = "";
            this.mtxtDeductDescription.SelectionLength = 0;
            this.mtxtDeductDescription.SelectionStart = 0;
            this.mtxtDeductDescription.ShortcutsEnabled = true;
            this.mtxtDeductDescription.Size = new System.Drawing.Size(197, 29);
            this.mtxtDeductDescription.TabIndex = 2;
            this.mtxtDeductDescription.Tag = "Deducted Description";
            this.mtxtDeductDescription.UseSelectable = true;
            this.mtxtDeductDescription.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtDeductDescription.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(4)))), ((int)(((byte)(209)))));
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btnMin);
            this.panel2.Controls.Add(this.hlTitle);
            this.panel2.Controls.Add(this.btnclose);
            this.panel2.Location = new System.Drawing.Point(-1, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(553, 39);
            this.panel2.TabIndex = 162;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::pLocker.Properties.Resources.close_1_button;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(525, 1);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 31);
            this.button1.TabIndex = 163;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnMin
            // 
            this.btnMin.BackColor = System.Drawing.Color.Transparent;
            this.btnMin.BackgroundImage = global::pLocker.Properties.Resources.Minimize_1;
            this.btnMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMin.ForeColor = System.Drawing.Color.White;
            this.btnMin.Location = new System.Drawing.Point(497, 1);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(27, 30);
            this.btnMin.TabIndex = 174;
            this.btnMin.Text = "-";
            this.btnMin.UseVisualStyleBackColor = false;
            // 
            // hlTitle
            // 
            this.hlTitle.AutoSize = true;
            this.hlTitle.BackColor = System.Drawing.Color.Transparent;
            this.hlTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
            this.hlTitle.Location = new System.Drawing.Point(0, 6);
            this.hlTitle.Name = "hlTitle";
            this.hlTitle.Size = new System.Drawing.Size(329, 25);
            this.hlTitle.TabIndex = 161;
            this.hlTitle.Text = "Employee Earning & Deduction Details";
            // 
            // btnclose
            // 
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(978, 0);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(26, 31);
            this.btnclose.TabIndex = 0;
            this.btnclose.Text = "X";
            this.btnclose.UseVisualStyleBackColor = true;
            // 
            // mbtnClose
            // 
            this.mbtnClose.BackgroundImage = global::pLocker.Properties.Resources.close_1;
            this.mbtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnClose.FlatAppearance.BorderSize = 0;
            this.mbtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnClose.Location = new System.Drawing.Point(403, 316);
            this.mbtnClose.Name = "mbtnClose";
            this.mbtnClose.Size = new System.Drawing.Size(132, 44);
            this.mbtnClose.TabIndex = 112;
            this.mbtnClose.UseVisualStyleBackColor = true;
            this.mbtnClose.Click += new System.EventHandler(this.mbtnClose_Click);
            this.mbtnClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbtnClose_MouseDown);
            this.mbtnClose.MouseLeave += new System.EventHandler(this.mbtnClose_MouseLeave);
            this.mbtnClose.MouseHover += new System.EventHandler(this.mbtnClose_MouseHover);
            this.mbtnClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbtnClose_MouseUp);
            // 
            // mbtnDelete
            // 
            this.mbtnDelete.BackgroundImage = global::pLocker.Properties.Resources.delete_1;
            this.mbtnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnDelete.FlatAppearance.BorderSize = 0;
            this.mbtnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnDelete.Location = new System.Drawing.Point(403, 266);
            this.mbtnDelete.Name = "mbtnDelete";
            this.mbtnDelete.Size = new System.Drawing.Size(132, 44);
            this.mbtnDelete.TabIndex = 111;
            this.mbtnDelete.UseVisualStyleBackColor = true;
            this.mbtnDelete.Click += new System.EventHandler(this.MbtnDelete_Click);
            this.mbtnDelete.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbtnDelete_MouseDown);
            this.mbtnDelete.MouseLeave += new System.EventHandler(this.mbtnDelete_MouseLeave);
            this.mbtnDelete.MouseHover += new System.EventHandler(this.mbtnDelete_MouseHover);
            this.mbtnDelete.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbtnDelete_MouseUp);
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackgroundImage = global::pLocker.Properties.Resources.refresh_1;
            this.mbtnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnRefresh.FlatAppearance.BorderSize = 0;
            this.mbtnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnRefresh.Location = new System.Drawing.Point(403, 214);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(132, 44);
            this.mbtnRefresh.TabIndex = 110;
            this.mbtnRefresh.UseVisualStyleBackColor = true;
            this.mbtnRefresh.Click += new System.EventHandler(this.MbtnRefresh_Click);
            this.mbtnRefresh.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbtnRefresh_MouseDown);
            this.mbtnRefresh.MouseLeave += new System.EventHandler(this.mbtnRefresh_MouseLeave);
            this.mbtnRefresh.MouseHover += new System.EventHandler(this.mbtnRefresh_MouseHover);
            this.mbtnRefresh.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbtnRefresh_MouseUp);
            // 
            // mbtnList
            // 
            this.mbtnList.BackgroundImage = global::pLocker.Properties.Resources.search_1;
            this.mbtnList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnList.FlatAppearance.BorderSize = 0;
            this.mbtnList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnList.Location = new System.Drawing.Point(403, 160);
            this.mbtnList.Name = "mbtnList";
            this.mbtnList.Size = new System.Drawing.Size(132, 44);
            this.mbtnList.TabIndex = 109;
            this.mbtnList.UseVisualStyleBackColor = true;
            this.mbtnList.Click += new System.EventHandler(this.MbtnList_Click);
            this.mbtnList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbtnList_MouseDown);
            this.mbtnList.MouseLeave += new System.EventHandler(this.mbtnList_MouseLeave);
            this.mbtnList.MouseHover += new System.EventHandler(this.mbtnList_MouseHover);
            this.mbtnList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbtnList_MouseUp);
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbtnSave.BackgroundImage")));
            this.mbtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnSave.FlatAppearance.BorderSize = 0;
            this.mbtnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnSave.Location = new System.Drawing.Point(403, 110);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(132, 44);
            this.mbtnSave.TabIndex = 108;
            this.mbtnSave.UseVisualStyleBackColor = true;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            this.mbtnSave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbtnSave_MouseDown);
            this.mbtnSave.MouseLeave += new System.EventHandler(this.mbtnSave_MouseLeave);
            this.mbtnSave.MouseHover += new System.EventHandler(this.mbtnSave_MouseHover);
            this.mbtnSave.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbtnSave_MouseUp);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.panel1.Controls.Add(this.mtxtEmployee);
            this.panel1.Controls.Add(this.mbEmployee);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.mbtnClose);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.metroLabel2);
            this.panel1.Controls.Add(this.mbtnDelete);
            this.panel1.Controls.Add(this.mbtnSave);
            this.panel1.Controls.Add(this.mbtnRefresh);
            this.panel1.Controls.Add(this.mbtnList);
            this.panel1.Location = new System.Drawing.Point(-1, 38);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(553, 427);
            this.panel1.TabIndex = 163;
            // 
            // FrmEmployeeEarnDedu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(553, 467);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEmployeeEarnDedu";
            this.Resizable = false;
            this.Load += new System.EventHandler(this.FrmEmployeeEarnDedu_Load);
            this.Click += new System.EventHandler(this.FrmEmployeeEarnDedu_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmEmployeeEarnDedu_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmEmployeeEarnDedu_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox mtxtEmployee;
        private MetroFramework.Controls.MetroButton mbEmployee;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroLabel metroLabel13;
        private MetroFramework.Controls.MetroComboBox mcmbEarnType;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox mtxtEarnedDescription;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroComboBox mcmbDeductedType;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroTextBox mtxtDeductDescription;
        private MetroFramework.Controls.MetroTextBox mtxtEarnedAmount;
        private MetroFramework.Controls.MetroLabel metroLabel38;
        private MetroFramework.Controls.MetroTextBox mtxtDeductedAmount;
        private MetroFramework.Controls.MetroLabel metroLabel5;
        private System.Windows.Forms.Button mbtnSave;
        private System.Windows.Forms.Button mbtnList;
        private System.Windows.Forms.Button mbtnRefresh;
        private System.Windows.Forms.Button mbtnDelete;
        private System.Windows.Forms.Button mbtnClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label hlTitle;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
    }
}