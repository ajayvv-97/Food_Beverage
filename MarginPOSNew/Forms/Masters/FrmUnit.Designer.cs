namespace MarginPOSNew.Forms.Masters
{
    partial class FrmUnit
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
            this.mtxtUnit = new MetroFramework.Controls.MetroTextBox();
            this.mlblUnitName = new MetroFramework.Controls.MetroLabel();
            this.dgvUnit = new System.Windows.Forms.DataGridView();
            this.hlTitle = new MetroFramework.Drawing.Html.HtmlLabel();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnit)).BeginInit();
            this.SuspendLayout();
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.mbtnRefresh.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnRefresh.ForeColor = System.Drawing.Color.White;
            this.mbtnRefresh.Location = new System.Drawing.Point(716, 61);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(150, 45);
            this.mbtnRefresh.TabIndex = 2;
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
            this.mbtnSave.Location = new System.Drawing.Point(556, 61);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(150, 45);
            this.mbtnSave.TabIndex = 1;
            this.mbtnSave.Text = "Save [F2]";
            this.mbtnSave.UseCustomBackColor = true;
            this.mbtnSave.UseCustomForeColor = true;
            this.mbtnSave.UseSelectable = true;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            // 
            // mtxtUnit
            // 
            // 
            // 
            // 
            this.mtxtUnit.CustomButton.Image = null;
            this.mtxtUnit.CustomButton.Location = new System.Drawing.Point(412, 1);
            this.mtxtUnit.CustomButton.Name = "";
            this.mtxtUnit.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtUnit.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtUnit.CustomButton.TabIndex = 1;
            this.mtxtUnit.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtUnit.CustomButton.UseSelectable = true;
            this.mtxtUnit.CustomButton.Visible = false;
            this.mtxtUnit.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtUnit.Lines = new string[0];
            this.mtxtUnit.Location = new System.Drawing.Point(108, 83);
            this.mtxtUnit.MaxLength = 50;
            this.mtxtUnit.Name = "mtxtUnit";
            this.mtxtUnit.PasswordChar = '\0';
            this.mtxtUnit.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtUnit.SelectedText = "";
            this.mtxtUnit.SelectionLength = 0;
            this.mtxtUnit.SelectionStart = 0;
            this.mtxtUnit.ShortcutsEnabled = true;
            this.mtxtUnit.Size = new System.Drawing.Size(434, 23);
            this.mtxtUnit.TabIndex = 0;
            this.mtxtUnit.Tag = "Unit Name";
            this.mtxtUnit.UseSelectable = true;
            this.mtxtUnit.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtUnit.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtUnit_KeyDown);
            this.mtxtUnit.Leave += new System.EventHandler(this.MtxtUnit_Leave);
            // 
            // mlblUnitName
            // 
            this.mlblUnitName.AutoSize = true;
            this.mlblUnitName.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblUnitName.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblUnitName.ForeColor = System.Drawing.Color.Black;
            this.mlblUnitName.Location = new System.Drawing.Point(11, 82);
            this.mlblUnitName.Name = "mlblUnitName";
            this.mlblUnitName.Size = new System.Drawing.Size(96, 25);
            this.mlblUnitName.TabIndex = 8;
            this.mlblUnitName.Text = "Unit Name";
            // 
            // dgvUnit
            // 
            this.dgvUnit.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvUnit.AllowUserToAddRows = false;
            this.dgvUnit.AllowUserToDeleteRows = false;
            this.dgvUnit.AllowUserToResizeColumns = false;
            this.dgvUnit.AllowUserToResizeRows = false;
            this.dgvUnit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUnit.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvUnit.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUnit.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUnit.ColumnHeadersHeight = 40;
            this.dgvUnit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUnit.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUnit.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvUnit.EnableHeadersVisualStyles = false;
            this.dgvUnit.Location = new System.Drawing.Point(10, 112);
            this.dgvUnit.MultiSelect = false;
            this.dgvUnit.Name = "dgvUnit";
            this.dgvUnit.ReadOnly = true;
            this.dgvUnit.RowHeadersVisible = false;
            this.dgvUnit.RowTemplate.Height = 30;
            this.dgvUnit.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvUnit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUnit.ShowCellErrors = false;
            this.dgvUnit.ShowCellToolTips = false;
            this.dgvUnit.ShowEditingIcon = false;
            this.dgvUnit.Size = new System.Drawing.Size(856, 294);
            this.dgvUnit.TabIndex = 3;
            this.dgvUnit.TabStop = false;
            this.dgvUnit.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvUnit_KeyDown);
            this.dgvUnit.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DgvUnit_MouseDoubleClick);
            // 
            // hlTitle
            // 
            this.hlTitle.AutoScroll = true;
            this.hlTitle.AutoScrollMinSize = new System.Drawing.Size(47, 36);
            this.hlTitle.AutoSize = false;
            this.hlTitle.BackColor = System.Drawing.SystemColors.Window;
            this.hlTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlTitle.Location = new System.Drawing.Point(11, 17);
            this.hlTitle.Name = "hlTitle";
            this.hlTitle.Size = new System.Drawing.Size(138, 38);
            this.hlTitle.TabIndex = 14;
            this.hlTitle.TabStop = false;
            this.hlTitle.Text = "Unit";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(542, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 15);
            this.label1.TabIndex = 15;
            this.label1.Text = "*";
            // 
            // FrmUnit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(877, 419);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hlTitle);
            this.Controls.Add(this.dgvUnit);
            this.Controls.Add(this.mbtnRefresh);
            this.Controls.Add(this.mbtnSave);
            this.Controls.Add(this.mtxtUnit);
            this.Controls.Add(this.mlblUnitName);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUnit";
            this.Resizable = false;
            this.Load += new System.EventHandler(this.FrmUnit_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmUnit_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton mbtnRefresh;
        private MetroFramework.Controls.MetroButton mbtnSave;
        private MetroFramework.Controls.MetroTextBox mtxtUnit;
        private MetroFramework.Controls.MetroLabel mlblUnitName;
        private System.Windows.Forms.DataGridView dgvUnit;
        private MetroFramework.Drawing.Html.HtmlLabel hlTitle;
        private System.Windows.Forms.Label label1;
    }
}