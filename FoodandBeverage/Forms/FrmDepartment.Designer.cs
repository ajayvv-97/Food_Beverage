namespace FoodandBeverage.Forms
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDepartment));
            this.mtxtName = new MetroFramework.Controls.MetroTextBox();
            this.mtxtEmpSrlNo = new MetroFramework.Controls.MetroTextBox();
            this.dgvDepartment = new System.Windows.Forms.DataGridView();
            this.mtxtCode = new MetroFramework.Controls.MetroTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mlblEmpSrlNo = new System.Windows.Forms.Label();
            this.mbtnClose = new System.Windows.Forms.Button();
            this.mbtnRefresh = new System.Windows.Forms.Button();
            this.metroLabel1 = new System.Windows.Forms.Label();
            this.mbtnSave = new System.Windows.Forms.Button();
            this.mlblName = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label23 = new System.Windows.Forms.Label();
            this.btncolse = new System.Windows.Forms.Button();
            this.btnmini = new System.Windows.Forms.Button();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartment)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            this.SuspendLayout();
            // 
            // mtxtName
            // 
            // 
            // 
            // 
            this.mtxtName.CustomButton.Image = null;
            this.mtxtName.CustomButton.Location = new System.Drawing.Point(155, 1);
            this.mtxtName.CustomButton.Name = "";
            this.mtxtName.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.mtxtName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtName.CustomButton.TabIndex = 1;
            this.mtxtName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtName.CustomButton.UseSelectable = true;
            this.mtxtName.CustomButton.Visible = false;
            this.mtxtName.Lines = new string[0];
            this.mtxtName.Location = new System.Drawing.Point(193, 33);
            this.mtxtName.MaxLength = 50;
            this.mtxtName.Name = "mtxtName";
            this.mtxtName.PasswordChar = '\0';
            this.mtxtName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtName.SelectedText = "";
            this.mtxtName.SelectionLength = 0;
            this.mtxtName.SelectionStart = 0;
            this.mtxtName.ShortcutsEnabled = true;
            this.mtxtName.Size = new System.Drawing.Size(183, 29);
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
            this.mtxtEmpSrlNo.CustomButton.Location = new System.Drawing.Point(155, 1);
            this.mtxtEmpSrlNo.CustomButton.Name = "";
            this.mtxtEmpSrlNo.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.mtxtEmpSrlNo.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtEmpSrlNo.CustomButton.TabIndex = 1;
            this.mtxtEmpSrlNo.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtEmpSrlNo.CustomButton.UseSelectable = true;
            this.mtxtEmpSrlNo.CustomButton.Visible = false;
            this.mtxtEmpSrlNo.Lines = new string[0];
            this.mtxtEmpSrlNo.Location = new System.Drawing.Point(193, 161);
            this.mtxtEmpSrlNo.MaxLength = 15;
            this.mtxtEmpSrlNo.Name = "mtxtEmpSrlNo";
            this.mtxtEmpSrlNo.PasswordChar = '\0';
            this.mtxtEmpSrlNo.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtEmpSrlNo.SelectedText = "";
            this.mtxtEmpSrlNo.SelectionLength = 0;
            this.mtxtEmpSrlNo.SelectionStart = 0;
            this.mtxtEmpSrlNo.ShortcutsEnabled = true;
            this.mtxtEmpSrlNo.Size = new System.Drawing.Size(183, 29);
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
            this.dgvDepartment.Location = new System.Drawing.Point(3, 59);
            this.dgvDepartment.Name = "dgvDepartment";
            this.dgvDepartment.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepartment.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDepartment.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDepartment.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDepartment.RowTemplate.Height = 30;
            this.dgvDepartment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvDepartment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepartment.ShowCellErrors = false;
            this.dgvDepartment.ShowCellToolTips = false;
            this.dgvDepartment.ShowEditingIcon = false;
            this.dgvDepartment.Size = new System.Drawing.Size(393, 491);
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
            this.mtxtCode.CustomButton.Location = new System.Drawing.Point(155, 1);
            this.mtxtCode.CustomButton.Name = "";
            this.mtxtCode.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.mtxtCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtCode.CustomButton.TabIndex = 1;
            this.mtxtCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtCode.CustomButton.UseSelectable = true;
            this.mtxtCode.CustomButton.Visible = false;
            this.mtxtCode.Lines = new string[0];
            this.mtxtCode.Location = new System.Drawing.Point(193, 93);
            this.mtxtCode.MaxLength = 15;
            this.mtxtCode.Name = "mtxtCode";
            this.mtxtCode.PasswordChar = '\0';
            this.mtxtCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtCode.SelectedText = "";
            this.mtxtCode.SelectionLength = 0;
            this.mtxtCode.SelectionStart = 0;
            this.mtxtCode.ShortcutsEnabled = true;
            this.mtxtCode.Size = new System.Drawing.Size(183, 29);
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
            this.label1.Location = new System.Drawing.Point(552, 33);
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
            this.label2.Location = new System.Drawing.Point(552, 105);
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
            this.label3.Location = new System.Drawing.Point(552, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "*";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 550);
            this.panel1.TabIndex = 26;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.dgvDepartment);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(382, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(404, 550);
            this.panel3.TabIndex = 29;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.mtxtCode);
            this.panel2.Controls.Add(this.mlblEmpSrlNo);
            this.panel2.Controls.Add(this.mbtnClose);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.mbtnRefresh);
            this.panel2.Controls.Add(this.metroLabel1);
            this.panel2.Controls.Add(this.mbtnSave);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.mlblName);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.mtxtEmpSrlNo);
            this.panel2.Controls.Add(this.mtxtName);
            this.panel2.Location = new System.Drawing.Point(0, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(383, 491);
            this.panel2.TabIndex = 28;
            // 
            // mlblEmpSrlNo
            // 
            this.mlblEmpSrlNo.AutoSize = true;
            this.mlblEmpSrlNo.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblEmpSrlNo.ForeColor = System.Drawing.Color.Black;
            this.mlblEmpSrlNo.Location = new System.Drawing.Point(12, 165);
            this.mlblEmpSrlNo.Name = "mlblEmpSrlNo";
            this.mlblEmpSrlNo.Size = new System.Drawing.Size(155, 25);
            this.mlblEmpSrlNo.TabIndex = 27;
            this.mlblEmpSrlNo.Text = "Employee Code :";
            // 
            // mbtnClose
            // 
            this.mbtnClose.BackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbtnClose.BackgroundImage")));
            this.mbtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnClose.FlatAppearance.BorderSize = 0;
            this.mbtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnClose.Location = new System.Drawing.Point(281, 293);
            this.mbtnClose.Name = "mbtnClose";
            this.mbtnClose.Size = new System.Drawing.Size(77, 23);
            this.mbtnClose.TabIndex = 24;
            this.mbtnClose.UseVisualStyleBackColor = false;
            this.mbtnClose.Click += new System.EventHandler(this.mbtnClose_Click_1);
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mbtnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbtnRefresh.BackgroundImage")));
            this.mbtnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnRefresh.FlatAppearance.BorderSize = 0;
            this.mbtnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnRefresh.Location = new System.Drawing.Point(159, 293);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(77, 23);
            this.mbtnRefresh.TabIndex = 23;
            this.mbtnRefresh.UseVisualStyleBackColor = false;
            this.mbtnRefresh.Click += new System.EventHandler(this.MbtnRefresh_Click);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.Location = new System.Drawing.Point(3, 97);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(181, 25);
            this.metroLabel1.TabIndex = 26;
            this.metroLabel1.Text = "Department Code : ";
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbtnSave.BackgroundImage")));
            this.mbtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnSave.FlatAppearance.BorderSize = 0;
            this.mbtnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnSave.Location = new System.Drawing.Point(37, 293);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(77, 23);
            this.mbtnSave.TabIndex = 22;
            this.mbtnSave.UseVisualStyleBackColor = false;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            // 
            // mlblName
            // 
            this.mlblName.AutoSize = true;
            this.mlblName.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblName.ForeColor = System.Drawing.Color.Black;
            this.mlblName.Location = new System.Drawing.Point(40, 33);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(127, 25);
            this.mlblName.TabIndex = 25;
            this.mlblName.Text = "Department :";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.SteelBlue;
            this.panel5.Controls.Add(this.pictureBox10);
            this.panel5.Controls.Add(this.label23);
            this.panel5.Controls.Add(this.btncolse);
            this.panel5.Controls.Add(this.btnmini);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(786, 59);
            this.panel5.TabIndex = 277;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Elephant", 14.25F, System.Drawing.FontStyle.Bold);
            this.label23.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label23.Location = new System.Drawing.Point(58, 14);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(139, 25);
            this.label23.TabIndex = 277;
            this.label23.Text = "Department";
            // 
            // btncolse
            // 
            this.btncolse.BackColor = System.Drawing.Color.White;
            this.btncolse.FlatAppearance.BorderSize = 0;
            this.btncolse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncolse.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncolse.ForeColor = System.Drawing.Color.Red;
            this.btncolse.Location = new System.Drawing.Point(751, 17);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(25, 25);
            this.btncolse.TabIndex = 11;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(720, 17);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(25, 25);
            this.btnmini.TabIndex = 10;
            this.btnmini.Text = "-";
            this.btnmini.UseVisualStyleBackColor = false;
            this.btnmini.Click += new System.EventHandler(this.btnmini_Click);
            // 
            // pictureBox10
            // 
            this.pictureBox10.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox10.BackgroundImage")));
            this.pictureBox10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox10.Location = new System.Drawing.Point(16, 14);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(38, 27);
            this.pictureBox10.TabIndex = 278;
            this.pictureBox10.TabStop = false;
            // 
            // FrmDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(786, 550);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDepartment";
            this.Load += new System.EventHandler(this.FrmDepartment_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDepartment_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmDepartment_KeyUp);
            this.Leave += new System.EventHandler(this.FrmDepartment_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartment)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
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
        
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label mlblEmpSrlNo;
        private System.Windows.Forms.Label metroLabel1;
        private System.Windows.Forms.Label mlblName;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
    }
}