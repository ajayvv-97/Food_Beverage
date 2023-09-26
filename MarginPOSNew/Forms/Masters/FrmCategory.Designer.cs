namespace MarginPOSNew.Forms.Masters
{
    partial class FrmCategory
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
            this.hlTitle = new MetroFramework.Drawing.Html.HtmlLabel();
            this.mlblName = new MetroFramework.Controls.MetroLabel();
            this.mbtnRefresh = new MetroFramework.Controls.MetroButton();
            this.mbtnSave = new MetroFramework.Controls.MetroButton();
            this.mtxtCategoryName = new MetroFramework.Controls.MetroTextBox();
            this.mtxtCatSrlNoStart = new MetroFramework.Controls.MetroTextBox();
            this.mlblSrlNo = new MetroFramework.Controls.MetroLabel();
            this.mtxtCategoryCode = new MetroFramework.Controls.MetroTextBox();
            this.mlblCode = new MetroFramework.Controls.MetroLabel();
            this.dgvCategory = new System.Windows.Forms.DataGridView();
            this.mtxtCurrentSerialNo = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategory)).BeginInit();
            this.SuspendLayout();
            // 
            // hlTitle
            // 
            this.hlTitle.AutoScroll = true;
            this.hlTitle.AutoScrollMinSize = new System.Drawing.Size(90, 36);
            this.hlTitle.AutoSize = false;
            this.hlTitle.BackColor = System.Drawing.SystemColors.Window;
            this.hlTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlTitle.Location = new System.Drawing.Point(10, 15);
            this.hlTitle.Name = "hlTitle";
            this.hlTitle.Size = new System.Drawing.Size(186, 38);
            this.hlTitle.TabIndex = 7;
            this.hlTitle.TabStop = false;
            this.hlTitle.Text = "Category";
            // 
            // mlblName
            // 
            this.mlblName.AutoSize = true;
            this.mlblName.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblName.ForeColor = System.Drawing.Color.Black;
            this.mlblName.Location = new System.Drawing.Point(21, 58);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(105, 19);
            this.mlblName.TabIndex = 8;
            this.mlblName.Text = "Category Name";
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.mbtnRefresh.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnRefresh.ForeColor = System.Drawing.Color.White;
            this.mbtnRefresh.Location = new System.Drawing.Point(716, 125);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(150, 45);
            this.mbtnRefresh.TabIndex = 5;
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
            this.mbtnSave.Location = new System.Drawing.Point(555, 125);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(150, 45);
            this.mbtnSave.TabIndex = 4;
            this.mbtnSave.Text = "Save [F2]";
            this.mbtnSave.UseCustomBackColor = true;
            this.mbtnSave.UseCustomForeColor = true;
            this.mbtnSave.UseSelectable = true;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            // 
            // mtxtCategoryName
            // 
            // 
            // 
            // 
            this.mtxtCategoryName.CustomButton.Image = null;
            this.mtxtCategoryName.CustomButton.Location = new System.Drawing.Point(374, 1);
            this.mtxtCategoryName.CustomButton.Name = "";
            this.mtxtCategoryName.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtCategoryName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtCategoryName.CustomButton.TabIndex = 1;
            this.mtxtCategoryName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtCategoryName.CustomButton.UseSelectable = true;
            this.mtxtCategoryName.CustomButton.Visible = false;
            this.mtxtCategoryName.Lines = new string[0];
            this.mtxtCategoryName.Location = new System.Drawing.Point(139, 54);
            this.mtxtCategoryName.MaxLength = 50;
            this.mtxtCategoryName.Name = "mtxtCategoryName";
            this.mtxtCategoryName.PasswordChar = '\0';
            this.mtxtCategoryName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtCategoryName.SelectedText = "";
            this.mtxtCategoryName.SelectionLength = 0;
            this.mtxtCategoryName.SelectionStart = 0;
            this.mtxtCategoryName.ShortcutsEnabled = true;
            this.mtxtCategoryName.Size = new System.Drawing.Size(396, 23);
            this.mtxtCategoryName.TabIndex = 0;
            this.mtxtCategoryName.Tag = "Category Name";
            this.mtxtCategoryName.UseSelectable = true;
            this.mtxtCategoryName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtCategoryName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtCategoryName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtCategoryName_KeyDown);
            this.mtxtCategoryName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtCategoryName_KeyPress);
            this.mtxtCategoryName.Leave += new System.EventHandler(this.mtxtCategoryName_Leave);
            // 
            // mtxtCatSrlNoStart
            // 
            // 
            // 
            // 
            this.mtxtCatSrlNoStart.CustomButton.Image = null;
            this.mtxtCatSrlNoStart.CustomButton.Location = new System.Drawing.Point(374, 1);
            this.mtxtCatSrlNoStart.CustomButton.Name = "";
            this.mtxtCatSrlNoStart.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtCatSrlNoStart.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtCatSrlNoStart.CustomButton.TabIndex = 1;
            this.mtxtCatSrlNoStart.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtCatSrlNoStart.CustomButton.UseSelectable = true;
            this.mtxtCatSrlNoStart.CustomButton.Visible = false;
            this.mtxtCatSrlNoStart.Lines = new string[] {
        " "};
            this.mtxtCatSrlNoStart.Location = new System.Drawing.Point(139, 116);
            this.mtxtCatSrlNoStart.MaxLength = 19;
            this.mtxtCatSrlNoStart.Name = "mtxtCatSrlNoStart";
            this.mtxtCatSrlNoStart.PasswordChar = '\0';
            this.mtxtCatSrlNoStart.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtCatSrlNoStart.SelectedText = "";
            this.mtxtCatSrlNoStart.SelectionLength = 0;
            this.mtxtCatSrlNoStart.SelectionStart = 0;
            this.mtxtCatSrlNoStart.ShortcutsEnabled = true;
            this.mtxtCatSrlNoStart.Size = new System.Drawing.Size(396, 23);
            this.mtxtCatSrlNoStart.TabIndex = 2;
            this.mtxtCatSrlNoStart.Tag = "Category Serial No Start";
            this.mtxtCatSrlNoStart.Text = " ";
            this.mtxtCatSrlNoStart.UseSelectable = true;
            this.mtxtCatSrlNoStart.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtCatSrlNoStart.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtCatSrlNoStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtCatSrlNoStart_KeyDown);
            this.mtxtCatSrlNoStart.Leave += new System.EventHandler(this.MtxtCatSrlNoStart_Leave);
            // 
            // mlblSrlNo
            // 
            this.mlblSrlNo.AutoSize = true;
            this.mlblSrlNo.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblSrlNo.ForeColor = System.Drawing.Color.Black;
            this.mlblSrlNo.Location = new System.Drawing.Point(26, 118);
            this.mlblSrlNo.Name = "mlblSrlNo";
            this.mlblSrlNo.Size = new System.Drawing.Size(100, 19);
            this.mlblSrlNo.TabIndex = 10;
            this.mlblSrlNo.Text = "Serial No Start ";
            // 
            // mtxtCategoryCode
            // 
            this.mtxtCategoryCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // 
            // 
            this.mtxtCategoryCode.CustomButton.Image = null;
            this.mtxtCategoryCode.CustomButton.Location = new System.Drawing.Point(374, 1);
            this.mtxtCategoryCode.CustomButton.Name = "";
            this.mtxtCategoryCode.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtCategoryCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtCategoryCode.CustomButton.TabIndex = 1;
            this.mtxtCategoryCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtCategoryCode.CustomButton.UseSelectable = true;
            this.mtxtCategoryCode.CustomButton.Visible = false;
            this.mtxtCategoryCode.Lines = new string[0];
            this.mtxtCategoryCode.Location = new System.Drawing.Point(139, 85);
            this.mtxtCategoryCode.MaxLength = 15;
            this.mtxtCategoryCode.Name = "mtxtCategoryCode";
            this.mtxtCategoryCode.PasswordChar = '\0';
            this.mtxtCategoryCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtCategoryCode.SelectedText = "";
            this.mtxtCategoryCode.SelectionLength = 0;
            this.mtxtCategoryCode.SelectionStart = 0;
            this.mtxtCategoryCode.ShortcutsEnabled = true;
            this.mtxtCategoryCode.Size = new System.Drawing.Size(396, 23);
            this.mtxtCategoryCode.TabIndex = 1;
            this.mtxtCategoryCode.Tag = "Category Code";
            this.mtxtCategoryCode.UseSelectable = true;
            this.mtxtCategoryCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtCategoryCode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtCategoryCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtCategoryCode_KeyDown);
            // 
            // mlblCode
            // 
            this.mlblCode.AutoSize = true;
            this.mlblCode.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblCode.ForeColor = System.Drawing.Color.Black;
            this.mlblCode.Location = new System.Drawing.Point(25, 87);
            this.mlblCode.Name = "mlblCode";
            this.mlblCode.Size = new System.Drawing.Size(101, 19);
            this.mlblCode.TabIndex = 9;
            this.mlblCode.Tag = "";
            this.mlblCode.Text = "Category Code";
            // 
            // dgvCategory
            // 
            this.dgvCategory.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvCategory.AllowUserToAddRows = false;
            this.dgvCategory.AllowUserToDeleteRows = false;
            this.dgvCategory.AllowUserToResizeColumns = false;
            this.dgvCategory.AllowUserToResizeRows = false;
            this.dgvCategory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCategory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvCategory.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCategory.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCategory.ColumnHeadersHeight = 40;
            this.dgvCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCategory.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvCategory.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvCategory.EnableHeadersVisualStyles = false;
            this.dgvCategory.Location = new System.Drawing.Point(10, 177);
            this.dgvCategory.MultiSelect = false;
            this.dgvCategory.Name = "dgvCategory";
            this.dgvCategory.ReadOnly = true;
            this.dgvCategory.RowHeadersVisible = false;
            this.dgvCategory.RowTemplate.Height = 30;
            this.dgvCategory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvCategory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategory.ShowCellErrors = false;
            this.dgvCategory.ShowCellToolTips = false;
            this.dgvCategory.ShowEditingIcon = false;
            this.dgvCategory.Size = new System.Drawing.Size(856, 232);
            this.dgvCategory.TabIndex = 6;
            this.dgvCategory.TabStop = false;
            this.dgvCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvCategory_KeyDown);
            this.dgvCategory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DgvCategory_MouseDoubleClick);
            // 
            // mtxtCurrentSerialNo
            // 
            // 
            // 
            // 
            this.mtxtCurrentSerialNo.CustomButton.Image = null;
            this.mtxtCurrentSerialNo.CustomButton.Location = new System.Drawing.Point(374, 1);
            this.mtxtCurrentSerialNo.CustomButton.Name = "";
            this.mtxtCurrentSerialNo.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtCurrentSerialNo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtCurrentSerialNo.CustomButton.TabIndex = 1;
            this.mtxtCurrentSerialNo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtCurrentSerialNo.CustomButton.UseSelectable = true;
            this.mtxtCurrentSerialNo.CustomButton.Visible = false;
            this.mtxtCurrentSerialNo.Lines = new string[] {
        " "};
            this.mtxtCurrentSerialNo.Location = new System.Drawing.Point(139, 147);
            this.mtxtCurrentSerialNo.MaxLength = 19;
            this.mtxtCurrentSerialNo.Name = "mtxtCurrentSerialNo";
            this.mtxtCurrentSerialNo.PasswordChar = '\0';
            this.mtxtCurrentSerialNo.ReadOnly = true;
            this.mtxtCurrentSerialNo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtCurrentSerialNo.SelectedText = "";
            this.mtxtCurrentSerialNo.SelectionLength = 0;
            this.mtxtCurrentSerialNo.SelectionStart = 0;
            this.mtxtCurrentSerialNo.ShortcutsEnabled = true;
            this.mtxtCurrentSerialNo.Size = new System.Drawing.Size(396, 23);
            this.mtxtCurrentSerialNo.TabIndex = 3;
            this.mtxtCurrentSerialNo.TabStop = false;
            this.mtxtCurrentSerialNo.Tag = "Current  Serial No";
            this.mtxtCurrentSerialNo.Text = " ";
            this.mtxtCurrentSerialNo.UseSelectable = true;
            this.mtxtCurrentSerialNo.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtCurrentSerialNo.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.Location = new System.Drawing.Point(8, 149);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(118, 19);
            this.metroLabel1.TabIndex = 11;
            this.metroLabel1.Text = "Serial No Current ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(535, 58);
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
            this.label2.Location = new System.Drawing.Point(535, 89);
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
            this.label3.Location = new System.Drawing.Point(535, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "*";
            // 
            // FrmCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(877, 419);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mtxtCurrentSerialNo);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.dgvCategory);
            this.Controls.Add(this.mtxtCategoryCode);
            this.Controls.Add(this.mlblCode);
            this.Controls.Add(this.mtxtCatSrlNoStart);
            this.Controls.Add(this.mlblSrlNo);
            this.Controls.Add(this.mbtnRefresh);
            this.Controls.Add(this.mbtnSave);
            this.Controls.Add(this.mtxtCategoryName);
            this.Controls.Add(this.mlblName);
            this.Controls.Add(this.hlTitle);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCategory";
            this.Resizable = false;
            this.Load += new System.EventHandler(this.FrmCategory_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmCategory_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Drawing.Html.HtmlLabel hlTitle;
        private MetroFramework.Controls.MetroLabel mlblName;
        private MetroFramework.Controls.MetroButton mbtnRefresh;
        private MetroFramework.Controls.MetroButton mbtnSave;
        private MetroFramework.Controls.MetroTextBox mtxtCategoryName;
        private MetroFramework.Controls.MetroTextBox mtxtCatSrlNoStart;
        private MetroFramework.Controls.MetroLabel mlblSrlNo;
        private MetroFramework.Controls.MetroTextBox mtxtCategoryCode;
        private MetroFramework.Controls.MetroLabel mlblCode;
        private System.Windows.Forms.DataGridView dgvCategory;
        private MetroFramework.Controls.MetroTextBox mtxtCurrentSerialNo;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}