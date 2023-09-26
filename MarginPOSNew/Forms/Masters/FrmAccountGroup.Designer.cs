namespace MarginPOSNew.Forms.Masters
{
    partial class FrmAccountGroup
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
            this.hlblHeader = new MetroFramework.Drawing.Html.HtmlLabel();
            this.mbtnRefresh = new MetroFramework.Controls.MetroButton();
            this.mbtnSave = new MetroFramework.Controls.MetroButton();
            this.mlblName = new MetroFramework.Controls.MetroLabel();
            this.mtxtName = new MetroFramework.Controls.MetroTextBox();
            this.dgvAccountGroup = new System.Windows.Forms.DataGridView();
            this.mlblEmpSrlNo = new MetroFramework.Controls.MetroLabel();
            this.mtxtSrlNo = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.mtxtCode = new MetroFramework.Controls.MetroTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // hlblHeader
            // 
            this.hlblHeader.AutoScroll = true;
            this.hlblHeader.AutoScrollMinSize = new System.Drawing.Size(142, 36);
            this.hlblHeader.AutoSize = false;
            this.hlblHeader.BackColor = System.Drawing.SystemColors.Window;
            this.hlblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlblHeader.Location = new System.Drawing.Point(9, 18);
            this.hlblHeader.Name = "hlblHeader";
            this.hlblHeader.Size = new System.Drawing.Size(278, 38);
            this.hlblHeader.TabIndex = 6;
            this.hlblHeader.Text = "Account Group";
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.mbtnRefresh.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnRefresh.ForeColor = System.Drawing.Color.White;
            this.mbtnRefresh.Location = new System.Drawing.Point(722, 106);
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
            this.mbtnSave.Location = new System.Drawing.Point(556, 106);
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
            this.mlblName.Location = new System.Drawing.Point(9, 69);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(132, 25);
            this.mlblName.TabIndex = 7;
            this.mlblName.Text = "Account Group";
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
            this.mtxtName.Location = new System.Drawing.Point(158, 69);
            this.mtxtName.MaxLength = 50;
            this.mtxtName.Name = "mtxtName";
            this.mtxtName.PasswordChar = '\0';
            this.mtxtName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtName.SelectedText = "";
            this.mtxtName.SelectionLength = 0;
            this.mtxtName.SelectionStart = 0;
            this.mtxtName.ShortcutsEnabled = true;
            this.mtxtName.Size = new System.Drawing.Size(377, 23);
            this.mtxtName.TabIndex = 0;
            this.mtxtName.Tag = "Account Group";
            this.mtxtName.UseSelectable = true;
            this.mtxtName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtName.TextChanged += new System.EventHandler(this.mtxtName_TextChanged);
            this.mtxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtName_KeyDown);
            this.mtxtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtName_KeyPress);
            this.mtxtName.Leave += new System.EventHandler(this.mtxtName_Leave);
            // 
            // dgvAccountGroup
            // 
            this.dgvAccountGroup.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvAccountGroup.AllowUserToAddRows = false;
            this.dgvAccountGroup.AllowUserToDeleteRows = false;
            this.dgvAccountGroup.AllowUserToResizeColumns = false;
            this.dgvAccountGroup.AllowUserToResizeRows = false;
            this.dgvAccountGroup.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAccountGroup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvAccountGroup.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAccountGroup.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAccountGroup.ColumnHeadersHeight = 40;
            this.dgvAccountGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAccountGroup.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAccountGroup.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvAccountGroup.EnableHeadersVisualStyles = false;
            this.dgvAccountGroup.Location = new System.Drawing.Point(14, 156);
            this.dgvAccountGroup.MultiSelect = false;
            this.dgvAccountGroup.Name = "dgvAccountGroup";
            this.dgvAccountGroup.ReadOnly = true;
            this.dgvAccountGroup.RowHeadersVisible = false;
            this.dgvAccountGroup.RowTemplate.Height = 30;
            this.dgvAccountGroup.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvAccountGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAccountGroup.ShowCellErrors = false;
            this.dgvAccountGroup.ShowCellToolTips = false;
            this.dgvAccountGroup.ShowEditingIcon = false;
            this.dgvAccountGroup.Size = new System.Drawing.Size(856, 379);
            this.dgvAccountGroup.TabIndex = 5;
            this.dgvAccountGroup.TabStop = false;
            this.dgvAccountGroup.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvAccountGroup_CellDoubleClick);
            this.dgvAccountGroup.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvAccountGroup_KeyDown);
            // 
            // mlblEmpSrlNo
            // 
            this.mlblEmpSrlNo.AutoSize = true;
            this.mlblEmpSrlNo.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblEmpSrlNo.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblEmpSrlNo.ForeColor = System.Drawing.Color.Black;
            this.mlblEmpSrlNo.Location = new System.Drawing.Point(16, 127);
            this.mlblEmpSrlNo.Name = "mlblEmpSrlNo";
            this.mlblEmpSrlNo.Size = new System.Drawing.Size(126, 25);
            this.mlblEmpSrlNo.TabIndex = 9;
            this.mlblEmpSrlNo.Text = "Account Sr.No";
            // 
            // mtxtSrlNo
            // 
            // 
            // 
            // 
            this.mtxtSrlNo.CustomButton.Image = null;
            this.mtxtSrlNo.CustomButton.Location = new System.Drawing.Point(355, 1);
            this.mtxtSrlNo.CustomButton.Name = "";
            this.mtxtSrlNo.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtSrlNo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtSrlNo.CustomButton.TabIndex = 1;
            this.mtxtSrlNo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtSrlNo.CustomButton.UseSelectable = true;
            this.mtxtSrlNo.CustomButton.Visible = false;
            this.mtxtSrlNo.Lines = new string[0];
            this.mtxtSrlNo.Location = new System.Drawing.Point(158, 128);
            this.mtxtSrlNo.MaxLength = 15;
            this.mtxtSrlNo.Name = "mtxtSrlNo";
            this.mtxtSrlNo.PasswordChar = '\0';
            this.mtxtSrlNo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtSrlNo.SelectedText = "";
            this.mtxtSrlNo.SelectionLength = 0;
            this.mtxtSrlNo.SelectionStart = 0;
            this.mtxtSrlNo.ShortcutsEnabled = true;
            this.mtxtSrlNo.Size = new System.Drawing.Size(377, 23);
            this.mtxtSrlNo.TabIndex = 2;
            this.mtxtSrlNo.Tag = "SrlNo";
            this.mtxtSrlNo.UseSelectable = true;
            this.mtxtSrlNo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtSrlNo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtSrlNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtName_KeyDown);
            this.mtxtSrlNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MtxtSrlNo_KeyPress);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.Location = new System.Drawing.Point(17, 98);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(124, 25);
            this.metroLabel1.TabIndex = 11;
            this.metroLabel1.Text = "Account Code";
            // 
            // mtxtCode
            // 
            this.mtxtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // 
            // 
            this.mtxtCode.CustomButton.Image = null;
            this.mtxtCode.CustomButton.Location = new System.Drawing.Point(355, 1);
            this.mtxtCode.CustomButton.Name = "";
            this.mtxtCode.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtCode.CustomButton.TabIndex = 1;
            this.mtxtCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtCode.CustomButton.UseSelectable = true;
            this.mtxtCode.CustomButton.Visible = false;
            this.mtxtCode.Lines = new string[0];
            this.mtxtCode.Location = new System.Drawing.Point(158, 98);
            this.mtxtCode.MaxLength = 15;
            this.mtxtCode.Name = "mtxtCode";
            this.mtxtCode.PasswordChar = '\0';
            this.mtxtCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtCode.SelectedText = "";
            this.mtxtCode.SelectionLength = 0;
            this.mtxtCode.SelectionStart = 0;
            this.mtxtCode.ShortcutsEnabled = true;
            this.mtxtCode.Size = new System.Drawing.Size(377, 23);
            this.mtxtCode.TabIndex = 1;
            this.mtxtCode.Tag = "Code";
            this.mtxtCode.UseSelectable = true;
            this.mtxtCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtCode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtName_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(534, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 15);
            this.label1.TabIndex = 12;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(534, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 15);
            this.label2.TabIndex = 13;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(534, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "*";
            // 
            // FrmAccountGroup
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
            this.Controls.Add(this.mlblEmpSrlNo);
            this.Controls.Add(this.mtxtSrlNo);
            this.Controls.Add(this.dgvAccountGroup);
            this.Controls.Add(this.mbtnRefresh);
            this.Controls.Add(this.mbtnSave);
            this.Controls.Add(this.mlblName);
            this.Controls.Add(this.mtxtName);
            this.Controls.Add(this.hlblHeader);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAccountGroup";
            this.Resizable = false;
            this.Load += new System.EventHandler(this.FrmAccountGroup_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAccountGroup_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmAccountGroup_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccountGroup)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Drawing.Html.HtmlLabel hlblHeader;
        private MetroFramework.Controls.MetroButton mbtnRefresh;
        private MetroFramework.Controls.MetroButton mbtnSave;
        private MetroFramework.Controls.MetroLabel mlblName;
        private MetroFramework.Controls.MetroTextBox mtxtName;
        private System.Windows.Forms.DataGridView dgvAccountGroup;
        private MetroFramework.Controls.MetroLabel mlblEmpSrlNo;
        private MetroFramework.Controls.MetroTextBox mtxtSrlNo;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroTextBox mtxtCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}