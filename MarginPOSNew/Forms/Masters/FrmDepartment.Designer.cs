namespace MarginPOSNew.Forms.Masters
{
    partial class FrmDepartment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mbtnRefresh = new MetroFramework.Controls.MetroButton();
            this.mbtnSave = new MetroFramework.Controls.MetroButton();
            this.mlblName = new MetroFramework.Controls.MetroLabel();
            this.mtxtName = new MetroFramework.Controls.MetroTextBox();
            this.hlTitle = new MetroFramework.Drawing.Html.HtmlLabel();
            this.mlblEmpSrlNo = new MetroFramework.Controls.MetroLabel();
            this.mtxtEmpSrlNo = new MetroFramework.Controls.MetroTextBox();
            this.dgvDepartment = new System.Windows.Forms.DataGridView();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.mtxtCode = new MetroFramework.Controls.MetroTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartment)).BeginInit();
            this.SuspendLayout();
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.mbtnRefresh.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnRefresh.ForeColor = System.Drawing.Color.White;
            this.mbtnRefresh.Location = new System.Drawing.Point(720, 103);
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
            this.mbtnSave.Location = new System.Drawing.Point(554, 103);
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
            this.mlblName.Location = new System.Drawing.Point(61, 67);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(107, 25);
            this.mlblName.TabIndex = 6;
            this.mlblName.Text = "Department";
            // 
            // mtxtName
            // 
            // 
            // 
            // 
            this.mtxtName.CustomButton.Image = null;
            this.mtxtName.CustomButton.Location = new System.Drawing.Point(334, 1);
            this.mtxtName.CustomButton.Name = "";
            this.mtxtName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtName.CustomButton.TabIndex = 1;
            this.mtxtName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtName.CustomButton.UseSelectable = true;
            this.mtxtName.CustomButton.Visible = false;
            this.mtxtName.Lines = new string[0];
            this.mtxtName.Location = new System.Drawing.Point(177, 68);
            this.mtxtName.MaxLength = 50;
            this.mtxtName.Name = "mtxtName";
            this.mtxtName.PasswordChar = '\0';
            this.mtxtName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtName.SelectedText = "";
            this.mtxtName.SelectionLength = 0;
            this.mtxtName.SelectionStart = 0;
            this.mtxtName.ShortcutsEnabled = true;
            this.mtxtName.Size = new System.Drawing.Size(356, 23);
            this.mtxtName.TabIndex = 0;
            this.mtxtName.Tag = "Department";
            this.mtxtName.UseSelectable = true;
            this.mtxtName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtName_KeyDown);
            this.mtxtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtName_KeyPress);
            this.mtxtName.Leave += new System.EventHandler(this.mtxtName_Leave);
            // 
            // hlTitle
            // 
            this.hlTitle.AutoScroll = true;
            this.hlTitle.AutoScrollMinSize = new System.Drawing.Size(116, 36);
            this.hlTitle.AutoSize = false;
            this.hlTitle.BackColor = System.Drawing.SystemColors.Window;
            this.hlTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlTitle.Location = new System.Drawing.Point(14, 19);
            this.hlTitle.Name = "hlTitle";
            this.hlTitle.Size = new System.Drawing.Size(186, 38);
            this.hlTitle.TabIndex = 5;
            this.hlTitle.Text = "Department";
            // 
            // mlblEmpSrlNo
            // 
            this.mlblEmpSrlNo.AutoSize = true;
            this.mlblEmpSrlNo.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblEmpSrlNo.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblEmpSrlNo.ForeColor = System.Drawing.Color.Black;
            this.mlblEmpSrlNo.Location = new System.Drawing.Point(25, 123);
            this.mlblEmpSrlNo.Name = "mlblEmpSrlNo";
            this.mlblEmpSrlNo.Size = new System.Drawing.Size(143, 25);
            this.mlblEmpSrlNo.TabIndex = 7;
            this.mlblEmpSrlNo.Text = "Employee Sr.No.";
            // 
            // mtxtEmpSrlNo
            // 
            // 
            // 
            // 
            this.mtxtEmpSrlNo.CustomButton.Image = null;
            this.mtxtEmpSrlNo.CustomButton.Location = new System.Drawing.Point(334, 1);
            this.mtxtEmpSrlNo.CustomButton.Name = "";
            this.mtxtEmpSrlNo.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtEmpSrlNo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtEmpSrlNo.CustomButton.TabIndex = 1;
            this.mtxtEmpSrlNo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtEmpSrlNo.CustomButton.UseSelectable = true;
            this.mtxtEmpSrlNo.CustomButton.Visible = false;
            this.mtxtEmpSrlNo.Lines = new string[0];
            this.mtxtEmpSrlNo.Location = new System.Drawing.Point(177, 124);
            this.mtxtEmpSrlNo.MaxLength = 15;
            this.mtxtEmpSrlNo.Name = "mtxtEmpSrlNo";
            this.mtxtEmpSrlNo.PasswordChar = '\0';
            this.mtxtEmpSrlNo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtEmpSrlNo.SelectedText = "";
            this.mtxtEmpSrlNo.SelectionLength = 0;
            this.mtxtEmpSrlNo.SelectionStart = 0;
            this.mtxtEmpSrlNo.ShortcutsEnabled = true;
            this.mtxtEmpSrlNo.Size = new System.Drawing.Size(356, 23);
            this.mtxtEmpSrlNo.TabIndex = 2;
            this.mtxtEmpSrlNo.Tag = "Employee Srl.No.";
            this.mtxtEmpSrlNo.UseSelectable = true;
            this.mtxtEmpSrlNo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtEmpSrlNo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtEmpSrlNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtEmpSrlNo_KeyDown);
            this.mtxtEmpSrlNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MtxtEmpSrlNo_KeyPress);
            // 
            // dgvDepartment
            // 
            this.dgvDepartment.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvDepartment.AllowUserToAddRows = false;
            this.dgvDepartment.AllowUserToDeleteRows = false;
            this.dgvDepartment.AllowUserToResizeColumns = false;
            this.dgvDepartment.AllowUserToResizeRows = false;
            this.dgvDepartment.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDepartment.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDepartment.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepartment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDepartment.ColumnHeadersHeight = 40;
            this.dgvDepartment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDepartment.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDepartment.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDepartment.EnableHeadersVisualStyles = false;
            this.dgvDepartment.Location = new System.Drawing.Point(14, 154);
            this.dgvDepartment.MultiSelect = false;
            this.dgvDepartment.Name = "dgvDepartment";
            this.dgvDepartment.ReadOnly = true;
            this.dgvDepartment.RowHeadersVisible = false;
            this.dgvDepartment.RowTemplate.Height = 30;
            this.dgvDepartment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvDepartment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepartment.ShowCellErrors = false;
            this.dgvDepartment.ShowCellToolTips = false;
            this.dgvDepartment.ShowEditingIcon = false;
            this.dgvDepartment.Size = new System.Drawing.Size(856, 382);
            this.dgvDepartment.TabIndex = 5;
            this.dgvDepartment.TabStop = false;
            this.dgvDepartment.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDepartment_CellDoubleClick);
            this.dgvDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvDepartment_KeyDown);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.Location = new System.Drawing.Point(14, 95);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(154, 25);
            this.metroLabel1.TabIndex = 13;
            this.metroLabel1.Text = "Department Code";
            // 
            // mtxtCode
            // 
            this.mtxtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // 
            // 
            this.mtxtCode.CustomButton.Image = null;
            this.mtxtCode.CustomButton.Location = new System.Drawing.Point(334, 1);
            this.mtxtCode.CustomButton.Name = "";
            this.mtxtCode.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtCode.CustomButton.TabIndex = 1;
            this.mtxtCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtCode.CustomButton.UseSelectable = true;
            this.mtxtCode.CustomButton.Visible = false;
            this.mtxtCode.Lines = new string[0];
            this.mtxtCode.Location = new System.Drawing.Point(177, 96);
            this.mtxtCode.MaxLength = 15;
            this.mtxtCode.Name = "mtxtCode";
            this.mtxtCode.PasswordChar = '\0';
            this.mtxtCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtCode.SelectedText = "";
            this.mtxtCode.SelectionLength = 0;
            this.mtxtCode.SelectionStart = 0;
            this.mtxtCode.ShortcutsEnabled = true;
            this.mtxtCode.Size = new System.Drawing.Size(356, 23);
            this.mtxtCode.TabIndex = 1;
            this.mtxtCode.Tag = "Code";
            this.mtxtCode.UseSelectable = true;
            this.mtxtCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtCode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtCode_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(532, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(532, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(532, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "*";
            // 
            // FrmDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(885, 550);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.mtxtCode);
            this.Controls.Add(this.dgvDepartment);
            this.Controls.Add(this.mlblEmpSrlNo);
            this.Controls.Add(this.mtxtEmpSrlNo);
            this.Controls.Add(this.mbtnRefresh);
            this.Controls.Add(this.mbtnSave);
            this.Controls.Add(this.mlblName);
            this.Controls.Add(this.mtxtName);
            this.Controls.Add(this.hlTitle);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDepartment";
            this.Resizable = false;
            this.Text = "FrmDepartment";
            this.Load += new System.EventHandler(this.FrmDepartment_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmDepartment_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartment)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton mbtnRefresh;
        private MetroFramework.Controls.MetroButton mbtnSave;
        private MetroFramework.Controls.MetroLabel mlblName;
        private MetroFramework.Controls.MetroTextBox mtxtName;
        private MetroFramework.Drawing.Html.HtmlLabel hlTitle;
        private MetroFramework.Controls.MetroLabel mlblEmpSrlNo;
        private MetroFramework.Controls.MetroTextBox mtxtEmpSrlNo;
        private System.Windows.Forms.DataGridView dgvDepartment;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox mtxtCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}