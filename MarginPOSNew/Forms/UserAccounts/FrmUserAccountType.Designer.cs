namespace MarginPOSNew.Forms.UserAccounts
{
    partial class FrmUserAccountType
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mbtnRefresh = new MetroFramework.Controls.MetroButton();
            this.mbtnSave = new MetroFramework.Controls.MetroButton();
            this.mtxtName = new MetroFramework.Controls.MetroTextBox();
            this.hlTitle = new MetroFramework.Drawing.Html.HtmlLabel();
            this.dgvUserAccountType = new System.Windows.Forms.DataGridView();
            this.mlblUserAccountType = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserAccountType)).BeginInit();
            this.SuspendLayout();
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.mbtnRefresh.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnRefresh.ForeColor = System.Drawing.Color.White;
            this.mbtnRefresh.Location = new System.Drawing.Point(719, 53);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(150, 45);
            this.mbtnRefresh.TabIndex = 2;
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
            this.mbtnSave.Location = new System.Drawing.Point(553, 53);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(150, 45);
            this.mbtnSave.TabIndex = 1;
            this.mbtnSave.Text = "&Save [F2]";
            this.mbtnSave.UseCustomBackColor = true;
            this.mbtnSave.UseCustomForeColor = true;
            this.mbtnSave.UseSelectable = true;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
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
            this.mtxtName.Location = new System.Drawing.Point(173, 64);
            this.mtxtName.MaxLength = 50;
            this.mtxtName.Name = "mtxtName";
            this.mtxtName.PasswordChar = '\0';
            this.mtxtName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtName.SelectedText = "";
            this.mtxtName.SelectionLength = 0;
            this.mtxtName.SelectionStart = 0;
            this.mtxtName.ShortcutsEnabled = true;
            this.mtxtName.Size = new System.Drawing.Size(374, 23);
            this.mtxtName.TabIndex = 0;
            this.mtxtName.Tag = "User AccountType Name";
            this.mtxtName.UseSelectable = true;
            this.mtxtName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // hlTitle
            // 
            this.hlTitle.AutoScroll = true;
            this.hlTitle.AutoScrollMinSize = new System.Drawing.Size(174, 36);
            this.hlTitle.AutoSize = false;
            this.hlTitle.BackColor = System.Drawing.SystemColors.Window;
            this.hlTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlTitle.Location = new System.Drawing.Point(13, 13);
            this.hlTitle.Name = "hlTitle";
            this.hlTitle.Size = new System.Drawing.Size(300, 38);
            this.hlTitle.TabIndex = 6;
            this.hlTitle.TabStop = false;
            this.hlTitle.Text = "User Account Type";
            // 
            // dgvUserAccountType
            // 
            this.dgvUserAccountType.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvUserAccountType.AllowUserToAddRows = false;
            this.dgvUserAccountType.AllowUserToDeleteRows = false;
            this.dgvUserAccountType.AllowUserToResizeColumns = false;
            this.dgvUserAccountType.AllowUserToResizeRows = false;
            this.dgvUserAccountType.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUserAccountType.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvUserAccountType.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUserAccountType.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvUserAccountType.ColumnHeadersHeight = 40;
            this.dgvUserAccountType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUserAccountType.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvUserAccountType.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvUserAccountType.EnableHeadersVisualStyles = false;
            this.dgvUserAccountType.Location = new System.Drawing.Point(13, 104);
            this.dgvUserAccountType.MultiSelect = false;
            this.dgvUserAccountType.Name = "dgvUserAccountType";
            this.dgvUserAccountType.ReadOnly = true;
            this.dgvUserAccountType.RowHeadersVisible = false;
            this.dgvUserAccountType.RowTemplate.Height = 30;
            this.dgvUserAccountType.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvUserAccountType.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvUserAccountType.ShowCellErrors = false;
            this.dgvUserAccountType.ShowCellToolTips = false;
            this.dgvUserAccountType.ShowEditingIcon = false;
            this.dgvUserAccountType.Size = new System.Drawing.Size(856, 294);
            this.dgvUserAccountType.TabIndex = 3;
            this.dgvUserAccountType.TabStop = false;
            this.dgvUserAccountType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvUserAccountType_KeyDown);
            this.dgvUserAccountType.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DgvUserAccountType_MouseDoubleClick);
            // 
            // mlblUserAccountType
            // 
            this.mlblUserAccountType.AutoSize = true;
            this.mlblUserAccountType.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblUserAccountType.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblUserAccountType.ForeColor = System.Drawing.Color.Black;
            this.mlblUserAccountType.Location = new System.Drawing.Point(13, 62);
            this.mlblUserAccountType.Name = "mlblUserAccountType";
            this.mlblUserAccountType.Size = new System.Drawing.Size(160, 25);
            this.mlblUserAccountType.TabIndex = 16;
            this.mlblUserAccountType.Text = "User Account Type";
            // 
            // FrmUserAccountType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(884, 411);
            this.Controls.Add(this.mlblUserAccountType);
            this.Controls.Add(this.dgvUserAccountType);
            this.Controls.Add(this.mbtnRefresh);
            this.Controls.Add(this.mbtnSave);
            this.Controls.Add(this.mtxtName);
            this.Controls.Add(this.hlTitle);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmUserAccountType";
            this.Resizable = false;
            this.Load += new System.EventHandler(this.FrmUserAccountType_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmUserAccountType_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUserAccountType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton mbtnRefresh;
        private MetroFramework.Controls.MetroButton mbtnSave;
        private MetroFramework.Controls.MetroTextBox mtxtName;
        private MetroFramework.Drawing.Html.HtmlLabel hlTitle;
        private System.Windows.Forms.DataGridView dgvUserAccountType;
        private MetroFramework.Controls.MetroLabel mlblUserAccountType;
    }
}