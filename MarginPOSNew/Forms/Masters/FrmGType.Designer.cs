namespace MarginPOSNew.Forms.Masters
{
    partial class FrmGType
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
            this.hlTitle = new MetroFramework.Drawing.Html.HtmlLabel();
            this.mtxtName = new MetroFramework.Controls.MetroTextBox();
            this.mbtnSave = new MetroFramework.Controls.MetroButton();
            this.mbtnRefresh = new MetroFramework.Controls.MetroButton();
            this.mlblName = new MetroFramework.Controls.MetroLabel();
            this.dgvGType = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGType)).BeginInit();
            this.SuspendLayout();
            // 
            // hlTitle
            // 
            this.hlTitle.AutoScroll = true;
            this.hlTitle.AutoScrollMinSize = new System.Drawing.Size(124, 36);
            this.hlTitle.AutoSize = false;
            this.hlTitle.BackColor = System.Drawing.SystemColors.Window;
            this.hlTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlTitle.Location = new System.Drawing.Point(9, 18);
            this.hlTitle.Name = "hlTitle";
            this.hlTitle.Size = new System.Drawing.Size(186, 38);
            this.hlTitle.TabIndex = 0;
            this.hlTitle.TabStop = false;
            this.hlTitle.Text = "General Type";
            // 
            // mtxtName
            // 
            // 
            // 
            // 
            this.mtxtName.CustomButton.Image = null;
            this.mtxtName.CustomButton.Location = new System.Drawing.Point(398, 1);
            this.mtxtName.CustomButton.Name = "";
            this.mtxtName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtName.CustomButton.TabIndex = 1;
            this.mtxtName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtName.CustomButton.UseSelectable = true;
            this.mtxtName.CustomButton.Visible = false;
            this.mtxtName.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtName.Lines = new string[0];
            this.mtxtName.Location = new System.Drawing.Point(90, 69);
            this.mtxtName.MaxLength = 50;
            this.mtxtName.Name = "mtxtName";
            this.mtxtName.PasswordChar = '\0';
            this.mtxtName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtName.SelectedText = "";
            this.mtxtName.SelectionLength = 0;
            this.mtxtName.SelectionStart = 0;
            this.mtxtName.ShortcutsEnabled = true;
            this.mtxtName.Size = new System.Drawing.Size(420, 23);
            this.mtxtName.TabIndex = 0;
            this.mtxtName.Tag = "General Type Name";
            this.mtxtName.UseSelectable = true;
            this.mtxtName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackColor = System.Drawing.Color.CornflowerBlue;
            this.mbtnSave.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnSave.ForeColor = System.Drawing.Color.White;
            this.mbtnSave.Location = new System.Drawing.Point(549, 58);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(150, 45);
            this.mbtnSave.TabIndex = 1;
            this.mbtnSave.Text = "Save [F2]";
            this.mbtnSave.UseCustomBackColor = true;
            this.mbtnSave.UseCustomForeColor = true;
            this.mbtnSave.UseSelectable = true;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.mbtnRefresh.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnRefresh.ForeColor = System.Drawing.Color.White;
            this.mbtnRefresh.Location = new System.Drawing.Point(715, 58);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(150, 45);
            this.mbtnRefresh.TabIndex = 2;
            this.mbtnRefresh.Text = "Refresh [F5]";
            this.mbtnRefresh.UseCustomBackColor = true;
            this.mbtnRefresh.UseCustomForeColor = true;
            this.mbtnRefresh.UseSelectable = true;
            this.mbtnRefresh.Click += new System.EventHandler(this.MbtnRefresh_Click);
            // 
            // mlblName
            // 
            this.mlblName.AutoSize = true;
            this.mlblName.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblName.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblName.ForeColor = System.Drawing.Color.Black;
            this.mlblName.Location = new System.Drawing.Point(17, 68);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(59, 25);
            this.mlblName.TabIndex = 0;
            this.mlblName.Text = "Name";
            // 
            // dgvGType
            // 
            this.dgvGType.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvGType.AllowUserToAddRows = false;
            this.dgvGType.AllowUserToDeleteRows = false;
            this.dgvGType.AllowUserToResizeColumns = false;
            this.dgvGType.AllowUserToResizeRows = false;
            this.dgvGType.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvGType.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvGType.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvGType.ColumnHeadersHeight = 40;
            this.dgvGType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvGType.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvGType.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvGType.EnableHeadersVisualStyles = false;
            this.dgvGType.Location = new System.Drawing.Point(9, 109);
            this.dgvGType.MultiSelect = false;
            this.dgvGType.Name = "dgvGType";
            this.dgvGType.ReadOnly = true;
            this.dgvGType.RowHeadersVisible = false;
            this.dgvGType.RowTemplate.Height = 30;
            this.dgvGType.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvGType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvGType.ShowCellErrors = false;
            this.dgvGType.ShowCellToolTips = false;
            this.dgvGType.ShowEditingIcon = false;
            this.dgvGType.Size = new System.Drawing.Size(856, 294);
            this.dgvGType.TabIndex = 3;
            this.dgvGType.TabStop = false;
            this.dgvGType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvGType_KeyDown);
            this.dgvGType.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DgvGType_MouseDoubleClick);
            // 
            // FrmGType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(877, 419);
            this.Controls.Add(this.dgvGType);
            this.Controls.Add(this.mbtnRefresh);
            this.Controls.Add(this.mbtnSave);
            this.Controls.Add(this.mlblName);
            this.Controls.Add(this.mtxtName);
            this.Controls.Add(this.hlTitle);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGType";
            this.Resizable = false;
            this.Load += new System.EventHandler(this.FrmGType_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmGType_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmGType_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Drawing.Html.HtmlLabel hlTitle;
        private MetroFramework.Controls.MetroTextBox mtxtName;
        private MetroFramework.Controls.MetroButton mbtnSave;
        private MetroFramework.Controls.MetroButton mbtnRefresh;
        private MetroFramework.Controls.MetroLabel mlblName;
        private System.Windows.Forms.DataGridView dgvGType;
    }
}