namespace MarginPOSNew.Forms.Masters
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
            this.hlTitle = new MetroFramework.Drawing.Html.HtmlLabel();
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
            this.mbtnList = new MetroFramework.Controls.MetroButton();
            this.mbtnDelete = new MetroFramework.Controls.MetroButton();
            this.mbtnRefresh = new MetroFramework.Controls.MetroButton();
            this.mbtnSave = new MetroFramework.Controls.MetroButton();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // hlTitle
            // 
            this.hlTitle.AutoScroll = true;
            this.hlTitle.AutoScrollMinSize = new System.Drawing.Size(257, 36);
            this.hlTitle.AutoSize = false;
            this.hlTitle.BackColor = System.Drawing.SystemColors.Window;
            this.hlTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlTitle.Location = new System.Drawing.Point(23, 16);
            this.hlTitle.Name = "hlTitle";
            this.hlTitle.Size = new System.Drawing.Size(382, 38);
            this.hlTitle.TabIndex = 9;
            this.hlTitle.TabStop = false;
            this.hlTitle.Text = "Employee Earn & Deduction";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.ForeColor = System.Drawing.Color.Black;
            this.metroLabel2.Location = new System.Drawing.Point(23, 74);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(90, 25);
            this.metroLabel2.TabIndex = 106;
            this.metroLabel2.Text = "Employee";
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
            this.mtxtEmployee.Location = new System.Drawing.Point(126, 75);
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
            this.mtxtEmployee.Tag = "Employee name";
            this.mtxtEmployee.UseSelectable = true;
            this.mtxtEmployee.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtEmployee.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // mbEmployee
            // 
            this.mbEmployee.Location = new System.Drawing.Point(425, 75);
            this.mbEmployee.Name = "mbEmployee";
            this.mbEmployee.Size = new System.Drawing.Size(26, 23);
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
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(29, 115);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 165);
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
            this.mtxtEarnedAmount.CustomButton.Location = new System.Drawing.Point(245, 1);
            this.mtxtEarnedAmount.CustomButton.Name = "";
            this.mtxtEarnedAmount.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.mtxtEarnedAmount.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtEarnedAmount.CustomButton.TabIndex = 1;
            this.mtxtEarnedAmount.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtEarnedAmount.CustomButton.UseSelectable = true;
            this.mtxtEarnedAmount.CustomButton.Visible = false;
            this.mtxtEarnedAmount.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtEarnedAmount.Lines = new string[0];
            this.mtxtEarnedAmount.Location = new System.Drawing.Point(106, 79);
            this.mtxtEarnedAmount.MaxLength = 50;
            this.mtxtEarnedAmount.Name = "mtxtEarnedAmount";
            this.mtxtEarnedAmount.PasswordChar = '\0';
            this.mtxtEarnedAmount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtEarnedAmount.SelectedText = "";
            this.mtxtEarnedAmount.SelectionLength = 0;
            this.mtxtEarnedAmount.SelectionStart = 0;
            this.mtxtEarnedAmount.ShortcutsEnabled = true;
            this.mtxtEarnedAmount.Size = new System.Drawing.Size(273, 29);
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
            this.metroLabel38.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel38.ForeColor = System.Drawing.Color.Black;
            this.metroLabel38.Location = new System.Drawing.Point(37, 77);
            this.metroLabel38.Name = "metroLabel38";
            this.metroLabel38.Size = new System.Drawing.Size(59, 19);
            this.metroLabel38.TabIndex = 104;
            this.metroLabel38.Text = "Amount";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.Location = new System.Drawing.Point(18, 116);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(78, 19);
            this.metroLabel1.TabIndex = 67;
            this.metroLabel1.Text = "Description";
            // 
            // mtxtEarnedDescription
            // 
            // 
            // 
            // 
            this.mtxtEarnedDescription.CustomButton.Image = null;
            this.mtxtEarnedDescription.CustomButton.Location = new System.Drawing.Point(245, 1);
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
            this.mtxtEarnedDescription.Size = new System.Drawing.Size(273, 29);
            this.mtxtEarnedDescription.TabIndex = 2;
            this.mtxtEarnedDescription.Tag = "Earned Description";
            this.mtxtEarnedDescription.UseSelectable = true;
            this.mtxtEarnedDescription.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtEarnedDescription.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel13
            // 
            this.metroLabel13.AutoSize = true;
            this.metroLabel13.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel13.ForeColor = System.Drawing.Color.Black;
            this.metroLabel13.Location = new System.Drawing.Point(27, 38);
            this.metroLabel13.Name = "metroLabel13";
            this.metroLabel13.Size = new System.Drawing.Size(69, 19);
            this.metroLabel13.TabIndex = 65;
            this.metroLabel13.Text = "Earn Type";
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
            this.mcmbEarnType.Size = new System.Drawing.Size(273, 29);
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
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(439, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(412, 165);
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
            this.mtxtDeductedAmount.CustomButton.Location = new System.Drawing.Point(236, 1);
            this.mtxtDeductedAmount.CustomButton.Name = "";
            this.mtxtDeductedAmount.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtDeductedAmount.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtDeductedAmount.CustomButton.TabIndex = 1;
            this.mtxtDeductedAmount.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtDeductedAmount.CustomButton.UseSelectable = true;
            this.mtxtDeductedAmount.CustomButton.Visible = false;
            this.mtxtDeductedAmount.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtDeductedAmount.Lines = new string[0];
            this.mtxtDeductedAmount.Location = new System.Drawing.Point(135, 79);
            this.mtxtDeductedAmount.MaxLength = 50;
            this.mtxtDeductedAmount.Name = "mtxtDeductedAmount";
            this.mtxtDeductedAmount.PasswordChar = '\0';
            this.mtxtDeductedAmount.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtDeductedAmount.SelectedText = "";
            this.mtxtDeductedAmount.SelectionLength = 0;
            this.mtxtDeductedAmount.SelectionStart = 0;
            this.mtxtDeductedAmount.ShortcutsEnabled = true;
            this.mtxtDeductedAmount.Size = new System.Drawing.Size(258, 23);
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
            this.metroLabel5.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel5.ForeColor = System.Drawing.Color.Black;
            this.metroLabel5.Location = new System.Drawing.Point(58, 77);
            this.metroLabel5.Name = "metroLabel5";
            this.metroLabel5.Size = new System.Drawing.Size(59, 19);
            this.metroLabel5.TabIndex = 106;
            this.metroLabel5.Text = "Amount";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.ForeColor = System.Drawing.Color.Black;
            this.metroLabel4.Location = new System.Drawing.Point(16, 38);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(101, 19);
            this.metroLabel4.TabIndex = 71;
            this.metroLabel4.Text = "Deducted Type";
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
            this.mcmbDeductedType.Size = new System.Drawing.Size(258, 29);
            this.mcmbDeductedType.TabIndex = 0;
            this.mcmbDeductedType.Tag = "Deducted Type";
            this.mcmbDeductedType.UseCustomBackColor = true;
            this.mcmbDeductedType.UseCustomForeColor = true;
            this.mcmbDeductedType.UseSelectable = true;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.ForeColor = System.Drawing.Color.Black;
            this.metroLabel3.Location = new System.Drawing.Point(39, 117);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(78, 19);
            this.metroLabel3.TabIndex = 69;
            this.metroLabel3.Text = "Description";
            // 
            // mtxtDeductDescription
            // 
            // 
            // 
            // 
            this.mtxtDeductDescription.CustomButton.Image = null;
            this.mtxtDeductDescription.CustomButton.Location = new System.Drawing.Point(236, 1);
            this.mtxtDeductDescription.CustomButton.Name = "";
            this.mtxtDeductDescription.CustomButton.Size = new System.Drawing.Size(21, 21);
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
            this.mtxtDeductDescription.Size = new System.Drawing.Size(258, 23);
            this.mtxtDeductDescription.TabIndex = 2;
            this.mtxtDeductDescription.Tag = "Deducted Description";
            this.mtxtDeductDescription.UseSelectable = true;
            this.mtxtDeductDescription.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtDeductDescription.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // mbtnList
            // 
            this.mbtnList.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.mbtnList.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnList.ForeColor = System.Drawing.Color.White;
            this.mbtnList.Location = new System.Drawing.Point(349, 300);
            this.mbtnList.Name = "mbtnList";
            this.mbtnList.Size = new System.Drawing.Size(150, 45);
            this.mbtnList.TabIndex = 5;
            this.mbtnList.Text = "Search [F3]";
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
            this.mbtnDelete.Location = new System.Drawing.Point(690, 300);
            this.mbtnDelete.Name = "mbtnDelete";
            this.mbtnDelete.Size = new System.Drawing.Size(150, 45);
            this.mbtnDelete.TabIndex = 7;
            this.mbtnDelete.Text = "Delete [F7]";
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
            this.mbtnRefresh.Location = new System.Drawing.Point(523, 300);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(150, 45);
            this.mbtnRefresh.TabIndex = 6;
            this.mbtnRefresh.Text = "Refresh [F5]";
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
            this.mbtnSave.Location = new System.Drawing.Point(174, 300);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(150, 45);
            this.mbtnSave.TabIndex = 4;
            this.mbtnSave.Text = "Save [F2]";
            this.mbtnSave.UseCustomBackColor = true;
            this.mbtnSave.UseCustomForeColor = true;
            this.mbtnSave.UseSelectable = true;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            // 
            // FrmEmployeeEarnDedu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(874, 368);
            this.Controls.Add(this.mbtnList);
            this.Controls.Add(this.mbtnDelete);
            this.Controls.Add(this.mbtnRefresh);
            this.Controls.Add(this.mbtnSave);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.mtxtEmployee);
            this.Controls.Add(this.mbEmployee);
            this.Controls.Add(this.hlTitle);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmEmployeeEarnDedu";
            this.Resizable = false;
            this.Load += new System.EventHandler(this.FrmEmployeeEarnDedu_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmEmployeeEarnDedu_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmEmployeeEarnDedu_KeyUp);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Drawing.Html.HtmlLabel hlTitle;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroTextBox mtxtEmployee;
        private MetroFramework.Controls.MetroButton mbEmployee;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private MetroFramework.Controls.MetroButton mbtnList;
        private MetroFramework.Controls.MetroButton mbtnDelete;
        private MetroFramework.Controls.MetroButton mbtnRefresh;
        private MetroFramework.Controls.MetroButton mbtnSave;
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
    }
}