namespace pLocker.Forms
{
    partial class FrmDesignation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDesignation));
            this.mtxtName = new MetroFramework.Controls.MetroTextBox();
            this.mcmbDepartment = new MetroFramework.Controls.MetroComboBox();
            this.dgvDesignation = new System.Windows.Forms.DataGridView();
            this.dtpWorkingHrs = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.hlblHeader = new System.Windows.Forms.Label();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.mlblEmpSrlNo = new System.Windows.Forms.Panel();
            this.mbtnClose = new System.Windows.Forms.Button();
            this.mbtnRefresh = new System.Windows.Forms.Button();
            this.mbtnSave = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.mlblName = new System.Windows.Forms.Label();
            this.metroLabel1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesignation)).BeginInit();
            this.panel3.SuspendLayout();
            this.mlblEmpSrlNo.SuspendLayout();
            this.SuspendLayout();
            // 
            // mtxtName
            // 
            // 
            // 
            // 
            this.mtxtName.CustomButton.Image = null;
            this.mtxtName.CustomButton.Location = new System.Drawing.Point(236, 1);
            this.mtxtName.CustomButton.Name = "";
            this.mtxtName.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.mtxtName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtName.CustomButton.TabIndex = 1;
            this.mtxtName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtName.CustomButton.UseSelectable = true;
            this.mtxtName.CustomButton.Visible = false;
            this.mtxtName.Lines = new string[0];
            this.mtxtName.Location = new System.Drawing.Point(142, 90);
            this.mtxtName.MaxLength = 50;
            this.mtxtName.Name = "mtxtName";
            this.mtxtName.PasswordChar = '\0';
            this.mtxtName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtName.SelectedText = "";
            this.mtxtName.SelectionLength = 0;
            this.mtxtName.SelectionStart = 0;
            this.mtxtName.ShortcutsEnabled = true;
            this.mtxtName.Size = new System.Drawing.Size(264, 29);
            this.mtxtName.TabIndex = 1;
            this.mtxtName.Tag = "Designation";
            this.mtxtName.UseSelectable = true;
            this.mtxtName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbDepartment_KeyDown);
            this.mtxtName.Leave += new System.EventHandler(this.mtxtName_Leave);
            // 
            // mcmbDepartment
            // 
            this.mcmbDepartment.BackColor = System.Drawing.Color.White;
            this.mcmbDepartment.DropDownHeight = 250;
            this.mcmbDepartment.ForeColor = System.Drawing.Color.Black;
            this.mcmbDepartment.FormattingEnabled = true;
            this.mcmbDepartment.IntegralHeight = false;
            this.mcmbDepartment.ItemHeight = 23;
            this.mcmbDepartment.Location = new System.Drawing.Point(142, 51);
            this.mcmbDepartment.Name = "mcmbDepartment";
            this.mcmbDepartment.Size = new System.Drawing.Size(264, 29);
            this.mcmbDepartment.TabIndex = 0;
            this.mcmbDepartment.Tag = "Department";
            this.mcmbDepartment.UseCustomBackColor = true;
            this.mcmbDepartment.UseCustomForeColor = true;
            this.mcmbDepartment.UseSelectable = true;
            this.mcmbDepartment.SelectedIndexChanged += new System.EventHandler(this.mcmbDepartment_SelectedIndexChanged);
            this.mcmbDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbDepartment_KeyDown);
            // 
            // dgvDesignation
            // 
            this.dgvDesignation.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvDesignation.AllowUserToAddRows = false;
            this.dgvDesignation.AllowUserToDeleteRows = false;
            this.dgvDesignation.AllowUserToResizeColumns = false;
            this.dgvDesignation.AllowUserToResizeRows = false;
            this.dgvDesignation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDesignation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDesignation.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDesignation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDesignation.ColumnHeadersHeight = 30;
            this.dgvDesignation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDesignation.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvDesignation.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDesignation.EnableHeadersVisualStyles = false;
            this.dgvDesignation.Location = new System.Drawing.Point(13, 159);
            this.dgvDesignation.MultiSelect = false;
            this.dgvDesignation.Name = "dgvDesignation";
            this.dgvDesignation.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDesignation.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvDesignation.RowHeadersVisible = false;
            this.dgvDesignation.RowTemplate.Height = 30;
            this.dgvDesignation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvDesignation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDesignation.ShowCellErrors = false;
            this.dgvDesignation.ShowCellToolTips = false;
            this.dgvDesignation.ShowEditingIcon = false;
            this.dgvDesignation.Size = new System.Drawing.Size(783, 326);
            this.dgvDesignation.TabIndex = 3;
            this.dgvDesignation.TabStop = false;
            this.dgvDesignation.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDesignation_CellDoubleClick);
            this.dgvDesignation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvDesignation_KeyDown);
            // 
            // dtpWorkingHrs
            // 
            this.dtpWorkingHrs.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpWorkingHrs.CustomFormat = "HH:mm";
            this.dtpWorkingHrs.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtpWorkingHrs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpWorkingHrs.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWorkingHrs.Location = new System.Drawing.Point(575, 53);
            this.dtpWorkingHrs.Name = "dtpWorkingHrs";
            this.dtpWorkingHrs.ShowUpDown = true;
            this.dtpWorkingHrs.Size = new System.Drawing.Size(73, 26);
            this.dtpWorkingHrs.TabIndex = 2;
            this.dtpWorkingHrs.ValueChanged += new System.EventHandler(this.dtpWorkingHrs_ValueChanged);
            this.dtpWorkingHrs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbDepartment_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(412, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(412, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "*";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(4)))), ((int)(((byte)(209)))));
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.Controls.Add(this.hlblHeader);
            this.panel3.Controls.Add(this.btnMin);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(817, 44);
            this.panel3.TabIndex = 13;
            // 
            // hlblHeader
            // 
            this.hlblHeader.AutoSize = true;
            this.hlblHeader.BackColor = System.Drawing.Color.Transparent;
            this.hlblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
            this.hlblHeader.Location = new System.Drawing.Point(3, 10);
            this.hlblHeader.Name = "hlblHeader";
            this.hlblHeader.Size = new System.Drawing.Size(214, 25);
            this.hlblHeader.TabIndex = 16;
            this.hlblHeader.Text = "Dept.wise Designation :";
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
            this.btnMin.Location = new System.Drawing.Point(760, 2);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(27, 30);
            this.btnMin.TabIndex = 171;
            this.btnMin.Text = "-";
            this.btnMin.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::pLocker.Properties.Resources.close_1_button;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.Transparent;
            this.btnClose.Location = new System.Drawing.Point(788, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(26, 30);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // mlblEmpSrlNo
            // 
            this.mlblEmpSrlNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.mlblEmpSrlNo.Controls.Add(this.mbtnClose);
            this.mlblEmpSrlNo.Controls.Add(this.mbtnRefresh);
            this.mlblEmpSrlNo.Controls.Add(this.mbtnSave);
            this.mlblEmpSrlNo.Controls.Add(this.label4);
            this.mlblEmpSrlNo.Controls.Add(this.mlblName);
            this.mlblEmpSrlNo.Controls.Add(this.metroLabel1);
            this.mlblEmpSrlNo.Controls.Add(this.dgvDesignation);
            this.mlblEmpSrlNo.Controls.Add(this.label2);
            this.mlblEmpSrlNo.Controls.Add(this.mtxtName);
            this.mlblEmpSrlNo.Controls.Add(this.label1);
            this.mlblEmpSrlNo.Controls.Add(this.dtpWorkingHrs);
            this.mlblEmpSrlNo.Controls.Add(this.mcmbDepartment);
            this.mlblEmpSrlNo.Location = new System.Drawing.Point(0, 44);
            this.mlblEmpSrlNo.Name = "mlblEmpSrlNo";
            this.mlblEmpSrlNo.Size = new System.Drawing.Size(815, 507);
            this.mlblEmpSrlNo.TabIndex = 14;
            // 
            // mbtnClose
            // 
            this.mbtnClose.BackgroundImage = global::pLocker.Properties.Resources.close_1;
            this.mbtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnClose.FlatAppearance.BorderSize = 0;
            this.mbtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnClose.Location = new System.Drawing.Point(654, 88);
            this.mbtnClose.Name = "mbtnClose";
            this.mbtnClose.Size = new System.Drawing.Size(106, 30);
            this.mbtnClose.TabIndex = 18;
            this.mbtnClose.UseVisualStyleBackColor = true;
            this.mbtnClose.Click += new System.EventHandler(this.mbtnClose_Click);
            this.mbtnClose.MouseLeave += new System.EventHandler(this.mbtnClose_MouseLeave);
            this.mbtnClose.MouseHover += new System.EventHandler(this.mbtnClose_MouseHover);
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackgroundImage = global::pLocker.Properties.Resources.refresh_1;
            this.mbtnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnRefresh.FlatAppearance.BorderSize = 0;
            this.mbtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnRefresh.Location = new System.Drawing.Point(542, 88);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(106, 30);
            this.mbtnRefresh.TabIndex = 17;
            this.mbtnRefresh.UseVisualStyleBackColor = true;
            this.mbtnRefresh.Click += new System.EventHandler(this.MbtnRefresh_Click);
            this.mbtnRefresh.MouseLeave += new System.EventHandler(this.mbtnRefresh_MouseLeave);
            this.mbtnRefresh.MouseHover += new System.EventHandler(this.mbtnRefresh_MouseHover);
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackgroundImage = global::pLocker.Properties.Resources.save_1;
            this.mbtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnSave.FlatAppearance.BorderSize = 0;
            this.mbtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnSave.Location = new System.Drawing.Point(430, 88);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(106, 30);
            this.mbtnSave.TabIndex = 16;
            this.mbtnSave.UseVisualStyleBackColor = true;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            this.mbtnSave.MouseLeave += new System.EventHandler(this.mbtnSave_MouseLeave);
            this.mbtnSave.MouseHover += new System.EventHandler(this.mbtnClose_MouseHover);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(424, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 25);
            this.label4.TabIndex = 15;
            this.label4.Text = "Working Hours :";
            // 
            // mlblName
            // 
            this.mlblName.AutoSize = true;
            this.mlblName.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblName.ForeColor = System.Drawing.Color.Silver;
            this.mlblName.Location = new System.Drawing.Point(11, 90);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(125, 25);
            this.mlblName.TabIndex = 14;
            this.mlblName.Text = "Designation :";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroLabel1.ForeColor = System.Drawing.Color.Silver;
            this.metroLabel1.Location = new System.Drawing.Point(9, 51);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(127, 25);
            this.metroLabel1.TabIndex = 13;
            this.metroLabel1.Text = "Department :";
            // 
            // FrmDesignation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(817, 552);
            this.Controls.Add(this.mlblEmpSrlNo);
            this.Controls.Add(this.panel3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDesignation";
            this.Resizable = false;
            this.Load += new System.EventHandler(this.FrmDesignation_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDesignation_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmDesignation_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesignation)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.mlblEmpSrlNo.ResumeLayout(false);
            this.mlblEmpSrlNo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox mtxtName;
        private MetroFramework.Controls.MetroComboBox mcmbDepartment;
        private System.Windows.Forms.DataGridView dgvDesignation;
        private System.Windows.Forms.DateTimePicker dtpWorkingHrs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel mlblEmpSrlNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label mlblName;
        private System.Windows.Forms.Label metroLabel1;
        private System.Windows.Forms.Label hlblHeader;
        private System.Windows.Forms.Button mbtnClose;
        private System.Windows.Forms.Button mbtnRefresh;
        private System.Windows.Forms.Button mbtnSave;
    }
}