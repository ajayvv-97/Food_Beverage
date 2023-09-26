namespace MarginPOSNew.Forms.Masters
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mlblEmpSrlNo = new MetroFramework.Controls.MetroLabel();
            this.mbtnRefresh = new MetroFramework.Controls.MetroButton();
            this.mbtnSave = new MetroFramework.Controls.MetroButton();
            this.mlblName = new MetroFramework.Controls.MetroLabel();
            this.mtxtName = new MetroFramework.Controls.MetroTextBox();
            this.hlblHeader = new MetroFramework.Drawing.Html.HtmlLabel();
            this.mcmbDepartment = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.dgvDesignation = new System.Windows.Forms.DataGridView();
            this.dtpWorkingHrs = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesignation)).BeginInit();
            this.SuspendLayout();
            // 
            // mlblEmpSrlNo
            // 
            this.mlblEmpSrlNo.AutoSize = true;
            this.mlblEmpSrlNo.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblEmpSrlNo.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblEmpSrlNo.ForeColor = System.Drawing.Color.Black;
            this.mlblEmpSrlNo.Location = new System.Drawing.Point(18, 128);
            this.mlblEmpSrlNo.Name = "mlblEmpSrlNo";
            this.mlblEmpSrlNo.Size = new System.Drawing.Size(133, 25);
            this.mlblEmpSrlNo.TabIndex = 8;
            this.mlblEmpSrlNo.Text = "Working Hours";
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.mbtnRefresh.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnRefresh.ForeColor = System.Drawing.Color.White;
            this.mbtnRefresh.Location = new System.Drawing.Point(724, 108);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(150, 45);
            this.mbtnRefresh.TabIndex = 4;
            this.mbtnRefresh.Text = "Refresh [F5]";
            this.mbtnRefresh.UseCustomBackColor = true;
            this.mbtnRefresh.UseCustomForeColor = true;
            this.mbtnRefresh.UseSelectable = true;
            this.mbtnRefresh.Click += new System.EventHandler(this.MbtnRefresh_Click);
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackColor = System.Drawing.Color.CornflowerBlue;
            this.mbtnSave.Enabled = false;
            this.mbtnSave.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnSave.ForeColor = System.Drawing.Color.White;
            this.mbtnSave.Location = new System.Drawing.Point(567, 108);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(150, 45);
            this.mbtnSave.TabIndex = 3;
            this.mbtnSave.Text = "Save [F2]";
            this.mbtnSave.UseCustomBackColor = true;
            this.mbtnSave.UseCustomForeColor = true;
            this.mbtnSave.UseSelectable = true;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            // 
            // mlblName
            // 
            this.mlblName.AutoSize = true;
            this.mlblName.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblName.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblName.ForeColor = System.Drawing.Color.Black;
            this.mlblName.Location = new System.Drawing.Point(39, 98);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(107, 25);
            this.mlblName.TabIndex = 7;
            this.mlblName.Text = "Designation";
            // 
            // mtxtName
            // 
            // 
            // 
            // 
            this.mtxtName.CustomButton.Image = null;
            this.mtxtName.CustomButton.Location = new System.Drawing.Point(355, 1);
            this.mtxtName.CustomButton.Name = "";
            this.mtxtName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtName.CustomButton.TabIndex = 1;
            this.mtxtName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtName.CustomButton.UseSelectable = true;
            this.mtxtName.CustomButton.Visible = false;
            this.mtxtName.Lines = new string[0];
            this.mtxtName.Location = new System.Drawing.Point(170, 99);
            this.mtxtName.MaxLength = 50;
            this.mtxtName.Name = "mtxtName";
            this.mtxtName.PasswordChar = '\0';
            this.mtxtName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtName.SelectedText = "";
            this.mtxtName.SelectionLength = 0;
            this.mtxtName.SelectionStart = 0;
            this.mtxtName.ShortcutsEnabled = true;
            this.mtxtName.Size = new System.Drawing.Size(377, 23);
            this.mtxtName.TabIndex = 1;
            this.mtxtName.Tag = "Designation";
            this.mtxtName.UseSelectable = true;
            this.mtxtName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbDepartment_KeyDown);
            this.mtxtName.Leave += new System.EventHandler(this.mtxtName_Leave);
            // 
            // hlblHeader
            // 
            this.hlblHeader.AutoScroll = true;
            this.hlblHeader.AutoScrollMinSize = new System.Drawing.Size(114, 36);
            this.hlblHeader.AutoSize = false;
            this.hlblHeader.BackColor = System.Drawing.SystemColors.Window;
            this.hlblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlblHeader.Location = new System.Drawing.Point(18, 19);
            this.hlblHeader.Name = "hlblHeader";
            this.hlblHeader.Size = new System.Drawing.Size(186, 38);
            this.hlblHeader.TabIndex = 9;
            this.hlblHeader.Text = "Designation";
            // 
            // mcmbDepartment
            // 
            this.mcmbDepartment.BackColor = System.Drawing.Color.White;
            this.mcmbDepartment.DropDownHeight = 250;
            this.mcmbDepartment.ForeColor = System.Drawing.Color.Black;
            this.mcmbDepartment.FormattingEnabled = true;
            this.mcmbDepartment.IntegralHeight = false;
            this.mcmbDepartment.ItemHeight = 23;
            this.mcmbDepartment.Location = new System.Drawing.Point(170, 63);
            this.mcmbDepartment.Name = "mcmbDepartment";
            this.mcmbDepartment.Size = new System.Drawing.Size(377, 29);
            this.mcmbDepartment.TabIndex = 0;
            this.mcmbDepartment.Tag = "Department";
            this.mcmbDepartment.UseCustomBackColor = true;
            this.mcmbDepartment.UseCustomForeColor = true;
            this.mcmbDepartment.UseSelectable = true;
            this.mcmbDepartment.SelectedIndexChanged += new System.EventHandler(this.mcmbDepartment_SelectedIndexChanged);
            this.mcmbDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbDepartment_KeyDown);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.Location = new System.Drawing.Point(39, 65);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(107, 25);
            this.metroLabel1.TabIndex = 6;
            this.metroLabel1.Text = "Department";
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
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDesignation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDesignation.ColumnHeadersHeight = 40;
            this.dgvDesignation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDesignation.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDesignation.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDesignation.EnableHeadersVisualStyles = false;
            this.dgvDesignation.Location = new System.Drawing.Point(18, 159);
            this.dgvDesignation.MultiSelect = false;
            this.dgvDesignation.Name = "dgvDesignation";
            this.dgvDesignation.ReadOnly = true;
            this.dgvDesignation.RowHeadersVisible = false;
            this.dgvDesignation.RowTemplate.Height = 30;
            this.dgvDesignation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvDesignation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDesignation.ShowCellErrors = false;
            this.dgvDesignation.ShowCellToolTips = false;
            this.dgvDesignation.ShowEditingIcon = false;
            this.dgvDesignation.Size = new System.Drawing.Size(856, 379);
            this.dgvDesignation.TabIndex = 5;
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
            this.dtpWorkingHrs.Location = new System.Drawing.Point(170, 128);
            this.dtpWorkingHrs.Name = "dtpWorkingHrs";
            this.dtpWorkingHrs.ShowUpDown = true;
            this.dtpWorkingHrs.Size = new System.Drawing.Size(114, 26);
            this.dtpWorkingHrs.TabIndex = 2;
            this.dtpWorkingHrs.ValueChanged += new System.EventHandler(this.dtpWorkingHrs_ValueChanged);
            this.dtpWorkingHrs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbDepartment_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(547, 74);
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
            this.label2.Location = new System.Drawing.Point(546, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(284, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 15);
            this.label3.TabIndex = 12;
            this.label3.Text = "*";
            // 
            // FrmDesignation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(893, 552);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtpWorkingHrs);
            this.Controls.Add(this.dgvDesignation);
            this.Controls.Add(this.mcmbDepartment);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.mlblEmpSrlNo);
            this.Controls.Add(this.mbtnRefresh);
            this.Controls.Add(this.mbtnSave);
            this.Controls.Add(this.mlblName);
            this.Controls.Add(this.mtxtName);
            this.Controls.Add(this.hlblHeader);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDesignation";
            this.Resizable = false;
            this.Text = "FrmDesignation";
            this.Load += new System.EventHandler(this.FrmDesignation_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmDesignation_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesignation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel mlblEmpSrlNo;
        private MetroFramework.Controls.MetroButton mbtnRefresh;
        private MetroFramework.Controls.MetroButton mbtnSave;
        private MetroFramework.Controls.MetroLabel mlblName;
        private MetroFramework.Controls.MetroTextBox mtxtName;
        private MetroFramework.Drawing.Html.HtmlLabel hlblHeader;
        private MetroFramework.Controls.MetroComboBox mcmbDepartment;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.DataGridView dgvDesignation;
        private System.Windows.Forms.DateTimePicker dtpWorkingHrs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}