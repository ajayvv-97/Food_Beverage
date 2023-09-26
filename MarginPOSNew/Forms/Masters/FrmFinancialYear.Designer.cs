namespace MarginPOSNew.Forms.Masters
{
    partial class FrmFinancialYear
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.mtxtFinyrDesc = new MetroFramework.Controls.MetroTextBox();
            this.mtxtRemarks = new MetroFramework.Controls.MetroTextBox();
            this.mdtpFromDate = new MetroFramework.Controls.MetroDateTime();
            this.mbtnRefresh = new MetroFramework.Controls.MetroButton();
            this.mbtnSave = new MetroFramework.Controls.MetroButton();
            this.hlTitle = new MetroFramework.Drawing.Html.HtmlLabel();
            this.dgvFinancialYear = new System.Windows.Forms.DataGridView();
            this.ckbActive = new System.Windows.Forms.CheckBox();
            this.mdtpToDate = new MetroFramework.Controls.MetroDateTime();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFinancialYear)).BeginInit();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(22, 62);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(74, 19);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.Text = "Description";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(24, 95);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(72, 19);
            this.metroLabel2.TabIndex = 1;
            this.metroLabel2.Text = "From Date";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(39, 132);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(57, 19);
            this.metroLabel3.TabIndex = 2;
            this.metroLabel3.Text = "To  Date";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(438, 73);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(59, 19);
            this.metroLabel4.TabIndex = 3;
            this.metroLabel4.Text = "Remarks";
            // 
            // mtxtFinyrDesc
            // 
            // 
            // 
            // 
            this.mtxtFinyrDesc.CustomButton.Image = null;
            this.mtxtFinyrDesc.CustomButton.Location = new System.Drawing.Point(286, 1);
            this.mtxtFinyrDesc.CustomButton.Name = "";
            this.mtxtFinyrDesc.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtFinyrDesc.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtFinyrDesc.CustomButton.TabIndex = 1;
            this.mtxtFinyrDesc.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtFinyrDesc.CustomButton.UseSelectable = true;
            this.mtxtFinyrDesc.CustomButton.Visible = false;
            this.mtxtFinyrDesc.Lines = new string[] {
        " "};
            this.mtxtFinyrDesc.Location = new System.Drawing.Point(109, 60);
            this.mtxtFinyrDesc.MaxLength = 50;
            this.mtxtFinyrDesc.Name = "mtxtFinyrDesc";
            this.mtxtFinyrDesc.PasswordChar = '\0';
            this.mtxtFinyrDesc.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtFinyrDesc.SelectedText = "";
            this.mtxtFinyrDesc.SelectionLength = 0;
            this.mtxtFinyrDesc.SelectionStart = 0;
            this.mtxtFinyrDesc.ShortcutsEnabled = true;
            this.mtxtFinyrDesc.Size = new System.Drawing.Size(308, 23);
            this.mtxtFinyrDesc.TabIndex = 0;
            this.mtxtFinyrDesc.Tag = "Description";
            this.mtxtFinyrDesc.Text = " ";
            this.mtxtFinyrDesc.UseSelectable = true;
            this.mtxtFinyrDesc.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtFinyrDesc.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtFinyrDesc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtFinyrDesc_KeyDown);
            this.mtxtFinyrDesc.Leave += new System.EventHandler(this.mtxtFinyrDesc_Leave);
            // 
            // mtxtRemarks
            // 
            // 
            // 
            // 
            this.mtxtRemarks.CustomButton.Image = null;
            this.mtxtRemarks.CustomButton.Location = new System.Drawing.Point(318, 1);
            this.mtxtRemarks.CustomButton.Name = "";
            this.mtxtRemarks.CustomButton.Size = new System.Drawing.Size(43, 43);
            this.mtxtRemarks.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtRemarks.CustomButton.TabIndex = 1;
            this.mtxtRemarks.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtRemarks.CustomButton.UseSelectable = true;
            this.mtxtRemarks.CustomButton.Visible = false;
            this.mtxtRemarks.Lines = new string[0];
            this.mtxtRemarks.Location = new System.Drawing.Point(506, 60);
            this.mtxtRemarks.MaxLength = 200;
            this.mtxtRemarks.Multiline = true;
            this.mtxtRemarks.Name = "mtxtRemarks";
            this.mtxtRemarks.PasswordChar = '\0';
            this.mtxtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtRemarks.SelectedText = "";
            this.mtxtRemarks.SelectionLength = 0;
            this.mtxtRemarks.SelectionStart = 0;
            this.mtxtRemarks.ShortcutsEnabled = true;
            this.mtxtRemarks.Size = new System.Drawing.Size(362, 45);
            this.mtxtRemarks.TabIndex = 3;
            this.mtxtRemarks.Tag = "Remarks";
            this.mtxtRemarks.UseSelectable = true;
            this.mtxtRemarks.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtRemarks.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // mdtpFromDate
            // 
            this.mdtpFromDate.CustomFormat = "dd-MM-yyyy";
            this.mdtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mdtpFromDate.Location = new System.Drawing.Point(109, 90);
            this.mdtpFromDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.mdtpFromDate.Name = "mdtpFromDate";
            this.mdtpFromDate.Size = new System.Drawing.Size(308, 29);
            this.mdtpFromDate.TabIndex = 1;
            this.mdtpFromDate.ValueChanged += new System.EventHandler(this.mdtpFromDate_ValueChanged);
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.mbtnRefresh.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnRefresh.ForeColor = System.Drawing.Color.White;
            this.mbtnRefresh.Location = new System.Drawing.Point(718, 111);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(150, 45);
            this.mbtnRefresh.TabIndex = 6;
            this.mbtnRefresh.Text = "Refresh [F5]";
            this.mbtnRefresh.UseCustomBackColor = true;
            this.mbtnRefresh.UseCustomForeColor = true;
            this.mbtnRefresh.UseSelectable = true;
            this.mbtnRefresh.Click += new System.EventHandler(this.mbtnRefresh_Click);
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackColor = System.Drawing.Color.CornflowerBlue;
            this.mbtnSave.Enabled = false;
            this.mbtnSave.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnSave.ForeColor = System.Drawing.Color.White;
            this.mbtnSave.Location = new System.Drawing.Point(554, 111);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(150, 45);
            this.mbtnSave.TabIndex = 5;
            this.mbtnSave.Text = "Save [F2]";
            this.mbtnSave.UseCustomBackColor = true;
            this.mbtnSave.UseCustomForeColor = true;
            this.mbtnSave.UseSelectable = true;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            // 
            // hlTitle
            // 
            this.hlTitle.AutoScroll = true;
            this.hlTitle.AutoScrollMinSize = new System.Drawing.Size(130, 36);
            this.hlTitle.AutoSize = false;
            this.hlTitle.BackColor = System.Drawing.SystemColors.Window;
            this.hlTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlTitle.Location = new System.Drawing.Point(15, 19);
            this.hlTitle.Name = "hlTitle";
            this.hlTitle.Size = new System.Drawing.Size(186, 38);
            this.hlTitle.TabIndex = 7;
            this.hlTitle.TabStop = false;
            this.hlTitle.Text = "Financial Year";
            // 
            // dgvFinancialYear
            // 
            this.dgvFinancialYear.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvFinancialYear.AllowUserToAddRows = false;
            this.dgvFinancialYear.AllowUserToDeleteRows = false;
            this.dgvFinancialYear.AllowUserToResizeColumns = false;
            this.dgvFinancialYear.AllowUserToResizeRows = false;
            this.dgvFinancialYear.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFinancialYear.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvFinancialYear.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvFinancialYear.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvFinancialYear.ColumnHeadersHeight = 40;
            this.dgvFinancialYear.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvFinancialYear.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvFinancialYear.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvFinancialYear.EnableHeadersVisualStyles = false;
            this.dgvFinancialYear.Location = new System.Drawing.Point(9, 162);
            this.dgvFinancialYear.MultiSelect = false;
            this.dgvFinancialYear.Name = "dgvFinancialYear";
            this.dgvFinancialYear.ReadOnly = true;
            this.dgvFinancialYear.RowHeadersVisible = false;
            this.dgvFinancialYear.RowTemplate.Height = 30;
            this.dgvFinancialYear.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvFinancialYear.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFinancialYear.ShowCellErrors = false;
            this.dgvFinancialYear.ShowCellToolTips = false;
            this.dgvFinancialYear.ShowEditingIcon = false;
            this.dgvFinancialYear.Size = new System.Drawing.Size(859, 246);
            this.dgvFinancialYear.TabIndex = 6;
            this.dgvFinancialYear.TabStop = false;
            this.dgvFinancialYear.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvFinancialYear_KeyDown);
            this.dgvFinancialYear.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DgvFinancialYear_MouseDoubleClick);
            // 
            // ckbActive
            // 
            this.ckbActive.AutoSize = true;
            this.ckbActive.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbActive.Location = new System.Drawing.Point(491, 132);
            this.ckbActive.Name = "ckbActive";
            this.ckbActive.Size = new System.Drawing.Size(61, 21);
            this.ckbActive.TabIndex = 4;
            this.ckbActive.Text = "Active";
            this.ckbActive.UseVisualStyleBackColor = true;
            this.ckbActive.CheckedChanged += new System.EventHandler(this.ckbActive_CheckedChanged);
            // 
            // mdtpToDate
            // 
            this.mdtpToDate.CustomFormat = "dd-MM-yyyy";
            this.mdtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mdtpToDate.Location = new System.Drawing.Point(109, 127);
            this.mdtpToDate.MinimumSize = new System.Drawing.Size(0, 29);
            this.mdtpToDate.Name = "mdtpToDate";
            this.mdtpToDate.Size = new System.Drawing.Size(308, 29);
            this.mdtpToDate.TabIndex = 2;
            this.mdtpToDate.ValueChanged += new System.EventHandler(this.mdtpToDate_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(420, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(420, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 15);
            this.label1.TabIndex = 16;
            this.label1.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(420, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 15);
            this.label3.TabIndex = 17;
            this.label3.Text = "*";
            // 
            // FrmFinancialYear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(877, 419);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mdtpToDate);
            this.Controls.Add(this.ckbActive);
            this.Controls.Add(this.dgvFinancialYear);
            this.Controls.Add(this.hlTitle);
            this.Controls.Add(this.mbtnRefresh);
            this.Controls.Add(this.mbtnSave);
            this.Controls.Add(this.mdtpFromDate);
            this.Controls.Add(this.mtxtRemarks);
            this.Controls.Add(this.mtxtFinyrDesc);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFinancialYear";
            this.Resizable = false;
            this.Text = " ";
            this.Load += new System.EventHandler(this.FrmFinancialYear_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmFinancialYear_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFinancialYear)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroTextBox mtxtFinyrDesc;
        private MetroFramework.Controls.MetroTextBox mtxtRemarks;
        private MetroFramework.Controls.MetroDateTime mdtpFromDate;
        private MetroFramework.Controls.MetroButton mbtnRefresh;
        private MetroFramework.Controls.MetroButton mbtnSave;
        private MetroFramework.Drawing.Html.HtmlLabel hlTitle;
        private System.Windows.Forms.DataGridView dgvFinancialYear;
        private System.Windows.Forms.CheckBox ckbActive;
        private MetroFramework.Controls.MetroDateTime mdtpToDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
    }
}