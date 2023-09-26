namespace pLocker.Forms
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDepartment));
            this.mtxtName = new MetroFramework.Controls.MetroTextBox();
            this.mtxtEmpSrlNo = new MetroFramework.Controls.MetroTextBox();
            this.dgvDepartment = new System.Windows.Forms.DataGridView();
            this.mtxtCode = new MetroFramework.Controls.MetroTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mbtnSave = new System.Windows.Forms.Button();
            this.mbtnRefresh = new System.Windows.Forms.Button();
            this.mbtnClose = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.hlTitle = new System.Windows.Forms.Label();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.mlblEmpSrlNo = new System.Windows.Forms.Label();
            this.metroLabel1 = new System.Windows.Forms.Label();
            this.mlblName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartment)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mtxtName
            // 
            // 
            // 
            // 
            this.mtxtName.CustomButton.Image = null;
            this.mtxtName.CustomButton.Location = new System.Drawing.Point(328, 1);
            this.mtxtName.CustomButton.Name = "";
            this.mtxtName.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.mtxtName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtName.CustomButton.TabIndex = 1;
            this.mtxtName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtName.CustomButton.UseSelectable = true;
            this.mtxtName.CustomButton.Visible = false;
            this.mtxtName.Lines = new string[0];
            this.mtxtName.Location = new System.Drawing.Point(180, 43);
            this.mtxtName.MaxLength = 50;
            this.mtxtName.Name = "mtxtName";
            this.mtxtName.PasswordChar = '\0';
            this.mtxtName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtName.SelectedText = "";
            this.mtxtName.SelectionLength = 0;
            this.mtxtName.SelectionStart = 0;
            this.mtxtName.ShortcutsEnabled = true;
            this.mtxtName.Size = new System.Drawing.Size(356, 29);
            this.mtxtName.TabIndex = 0;
            this.mtxtName.Tag = "Department";
            this.mtxtName.UseSelectable = true;
            this.mtxtName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtName_KeyDown);
            this.mtxtName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtName_KeyPress);
            this.mtxtName.Leave += new System.EventHandler(this.mtxtName_Leave);
            // 
            // mtxtEmpSrlNo
            // 
            // 
            // 
            // 
            this.mtxtEmpSrlNo.CustomButton.Image = null;
            this.mtxtEmpSrlNo.CustomButton.Location = new System.Drawing.Point(328, 1);
            this.mtxtEmpSrlNo.CustomButton.Name = "";
            this.mtxtEmpSrlNo.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.mtxtEmpSrlNo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtEmpSrlNo.CustomButton.TabIndex = 1;
            this.mtxtEmpSrlNo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtEmpSrlNo.CustomButton.UseSelectable = true;
            this.mtxtEmpSrlNo.CustomButton.Visible = false;
            this.mtxtEmpSrlNo.Lines = new string[0];
            this.mtxtEmpSrlNo.Location = new System.Drawing.Point(180, 117);
            this.mtxtEmpSrlNo.MaxLength = 15;
            this.mtxtEmpSrlNo.Name = "mtxtEmpSrlNo";
            this.mtxtEmpSrlNo.PasswordChar = '\0';
            this.mtxtEmpSrlNo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtEmpSrlNo.SelectedText = "";
            this.mtxtEmpSrlNo.SelectionLength = 0;
            this.mtxtEmpSrlNo.SelectionStart = 0;
            this.mtxtEmpSrlNo.ShortcutsEnabled = true;
            this.mtxtEmpSrlNo.Size = new System.Drawing.Size(356, 29);
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
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepartment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDepartment.ColumnHeadersHeight = 30;
            this.dgvDepartment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDepartment.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDepartment.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDepartment.EnableHeadersVisualStyles = false;
            this.dgvDepartment.Location = new System.Drawing.Point(3, 182);
            this.dgvDepartment.Name = "dgvDepartment";
            this.dgvDepartment.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepartment.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDepartment.RowHeadersVisible = false;
            this.dgvDepartment.RowTemplate.Height = 30;
            this.dgvDepartment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvDepartment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepartment.ShowCellErrors = false;
            this.dgvDepartment.ShowCellToolTips = false;
            this.dgvDepartment.ShowEditingIcon = false;
            this.dgvDepartment.Size = new System.Drawing.Size(782, 320);
            this.dgvDepartment.TabIndex = 3;
            this.dgvDepartment.TabStop = false;
            this.dgvDepartment.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDepartment_CellContentClick);
            this.dgvDepartment.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDepartment_CellDoubleClick);
            this.dgvDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvDepartment_KeyDown);
            // 
            // mtxtCode
            // 
            this.mtxtCode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            // 
            // 
            // 
            this.mtxtCode.CustomButton.Image = null;
            this.mtxtCode.CustomButton.Location = new System.Drawing.Point(328, 1);
            this.mtxtCode.CustomButton.Name = "";
            this.mtxtCode.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.mtxtCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtCode.CustomButton.TabIndex = 1;
            this.mtxtCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtCode.CustomButton.UseSelectable = true;
            this.mtxtCode.CustomButton.Visible = false;
            this.mtxtCode.Lines = new string[0];
            this.mtxtCode.Location = new System.Drawing.Point(180, 80);
            this.mtxtCode.MaxLength = 15;
            this.mtxtCode.Name = "mtxtCode";
            this.mtxtCode.PasswordChar = '\0';
            this.mtxtCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtCode.SelectedText = "";
            this.mtxtCode.SelectionLength = 0;
            this.mtxtCode.SelectionStart = 0;
            this.mtxtCode.ShortcutsEnabled = true;
            this.mtxtCode.Size = new System.Drawing.Size(356, 29);
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
            this.label1.Location = new System.Drawing.Point(539, 52);
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
            this.label2.Location = new System.Drawing.Point(539, 124);
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
            this.label3.Location = new System.Drawing.Point(539, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "*";
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.BackgroundImage = global::pLocker.Properties.Resources.save_1;
            this.mbtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnSave.FlatAppearance.BorderSize = 0;
            this.mbtnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnSave.Location = new System.Drawing.Point(559, 42);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(106, 30);
            this.mbtnSave.TabIndex = 22;
            this.mbtnSave.UseVisualStyleBackColor = false;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            this.mbtnSave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbtnSave_MouseDown);
            this.mbtnSave.MouseLeave += new System.EventHandler(this.mbtnSave_MouseLeave);
            this.mbtnSave.MouseHover += new System.EventHandler(this.mbtnSave_MouseHover);
            this.mbtnSave.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbtnSave_MouseUp);
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mbtnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.BackgroundImage = global::pLocker.Properties.Resources.refresh_1;
            this.mbtnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnRefresh.FlatAppearance.BorderSize = 0;
            this.mbtnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnRefresh.Location = new System.Drawing.Point(560, 79);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(106, 30);
            this.mbtnRefresh.TabIndex = 23;
            this.mbtnRefresh.UseVisualStyleBackColor = false;
            this.mbtnRefresh.Click += new System.EventHandler(this.MbtnRefresh_Click);
            this.mbtnRefresh.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbtnRefresh_MouseDown);
            this.mbtnRefresh.MouseLeave += new System.EventHandler(this.mbtnRefresh_MouseLeave);
            this.mbtnRefresh.MouseHover += new System.EventHandler(this.mbtnRefresh_MouseHover);
            this.mbtnRefresh.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbtnRefresh_MouseUp);
            // 
            // mbtnClose
            // 
            this.mbtnClose.BackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.BackgroundImage = global::pLocker.Properties.Resources.close_1;
            this.mbtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnClose.FlatAppearance.BorderSize = 0;
            this.mbtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnClose.Location = new System.Drawing.Point(562, 117);
            this.mbtnClose.Name = "mbtnClose";
            this.mbtnClose.Size = new System.Drawing.Size(106, 30);
            this.mbtnClose.TabIndex = 24;
            this.mbtnClose.UseVisualStyleBackColor = false;
            this.mbtnClose.Click += new System.EventHandler(this.mbtnClose_Click);
            this.mbtnClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbtnClose_MouseDown);
            this.mbtnClose.MouseLeave += new System.EventHandler(this.mbtnClose_MouseLeave);
            this.mbtnClose.MouseHover += new System.EventHandler(this.mbtnClose_MouseHover);
            this.mbtnClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbtnClose_MouseUp);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(4)))), ((int)(((byte)(209)))));
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.Controls.Add(this.hlTitle);
            this.panel3.Controls.Add(this.btnMin);
            this.panel3.Controls.Add(this.btnClose);
            this.panel3.Location = new System.Drawing.Point(-1, -1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(790, 44);
            this.panel3.TabIndex = 25;
            // 
            // hlTitle
            // 
            this.hlTitle.AutoSize = true;
            this.hlTitle.BackColor = System.Drawing.Color.Transparent;
            this.hlTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
            this.hlTitle.Location = new System.Drawing.Point(5, 10);
            this.hlTitle.Name = "hlTitle";
            this.hlTitle.Size = new System.Drawing.Size(127, 25);
            this.hlTitle.TabIndex = 28;
            this.hlTitle.Text = "Department :";
            // 
            // btnMin
            // 
            this.btnMin.BackColor = System.Drawing.Color.Transparent;
            this.btnMin.BackgroundImage = global::pLocker.Properties.Resources.Minimize_1;
            this.btnMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMin.ForeColor = System.Drawing.Color.White;
            this.btnMin.Location = new System.Drawing.Point(734, 1);
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
            this.btnClose.Location = new System.Drawing.Point(762, 1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(26, 30);
            this.btnClose.TabIndex = 0;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.panel1.Controls.Add(this.mlblEmpSrlNo);
            this.panel1.Controls.Add(this.metroLabel1);
            this.panel1.Controls.Add(this.mlblName);
            this.panel1.Controls.Add(this.mtxtEmpSrlNo);
            this.panel1.Controls.Add(this.dgvDepartment);
            this.panel1.Controls.Add(this.mbtnClose);
            this.panel1.Controls.Add(this.mtxtName);
            this.panel1.Controls.Add(this.mbtnRefresh);
            this.panel1.Controls.Add(this.mbtnSave);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.mtxtCode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(788, 505);
            this.panel1.TabIndex = 26;
            // 
            // mlblEmpSrlNo
            // 
            this.mlblEmpSrlNo.AutoSize = true;
            this.mlblEmpSrlNo.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblEmpSrlNo.Location = new System.Drawing.Point(19, 117);
            this.mlblEmpSrlNo.Name = "mlblEmpSrlNo";
            this.mlblEmpSrlNo.Size = new System.Drawing.Size(155, 25);
            this.mlblEmpSrlNo.TabIndex = 27;
            this.mlblEmpSrlNo.Text = "Employee Code :";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroLabel1.Location = new System.Drawing.Point(-1, 80);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(181, 25);
            this.metroLabel1.TabIndex = 26;
            this.metroLabel1.Text = "Department Code : ";
            // 
            // mlblName
            // 
            this.mlblName.AutoSize = true;
            this.mlblName.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblName.Location = new System.Drawing.Point(48, 44);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(127, 25);
            this.mlblName.TabIndex = 25;
            this.mlblName.Text = "Department :";
            // 
            // FrmDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(790, 550);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDepartment";
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Default;
            this.Theme = MetroFramework.MetroThemeStyle.Default;
            this.Load += new System.EventHandler(this.FrmDepartment_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDepartment_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmDepartment_KeyUp);
            this.Leave += new System.EventHandler(this.FrmDepartment_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartment)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox mtxtName;
        private MetroFramework.Controls.MetroTextBox mtxtEmpSrlNo;
        private System.Windows.Forms.DataGridView dgvDepartment;
        private MetroFramework.Controls.MetroTextBox mtxtCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button mbtnSave;
        private System.Windows.Forms.Button mbtnRefresh;
        private System.Windows.Forms.Button mbtnClose;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Button btnClose;
        private Component1 component11;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label mlblEmpSrlNo;
        private System.Windows.Forms.Label metroLabel1;
        private System.Windows.Forms.Label mlblName;
        private System.Windows.Forms.Label hlTitle;
    }
}