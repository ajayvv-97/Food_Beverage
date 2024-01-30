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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.mbtnSave = new System.Windows.Forms.Button();
            this.mbtnClose = new System.Windows.Forms.Button();
            this.mbtnRefresh = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mtxtEmpSrlNo = new System.Windows.Forms.TextBox();
            this.mlblEmpSrlNo = new System.Windows.Forms.Label();
            this.mlblName = new System.Windows.Forms.Label();
            this.metroLabel1 = new System.Windows.Forms.Label();
            this.mtxtName = new System.Windows.Forms.TextBox();
            this.mtxtCode = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.dgvDepartment = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btncolse = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.btnmini = new System.Windows.Forms.Button();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartment)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.tableLayoutPanel4);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(786, 550);
            this.panel1.TabIndex = 26;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 74F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 81F));
            this.tableLayoutPanel2.Controls.Add(this.mbtnClose, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.mbtnRefresh, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.mbtnSave, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 261);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 243F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(442, 286);
            this.tableLayoutPanel2.TabIndex = 31;
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.BackgroundImage = global::FoodandBeverage.Properties.Resources.SaveButton;
            this.mbtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnSave.FlatAppearance.BorderSize = 0;
            this.mbtnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnSave.Location = new System.Drawing.Point(211, 3);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(73, 37);
            this.mbtnSave.TabIndex = 22;
            this.mbtnSave.UseVisualStyleBackColor = false;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            // 
            // mbtnClose
            // 
            this.mbtnClose.BackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.BackgroundImage = global::FoodandBeverage.Properties.Resources.Closebutton;
            this.mbtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbtnClose.FlatAppearance.BorderSize = 0;
            this.mbtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnClose.Location = new System.Drawing.Point(364, 3);
            this.mbtnClose.Name = "mbtnClose";
            this.mbtnClose.Size = new System.Drawing.Size(75, 37);
            this.mbtnClose.TabIndex = 24;
            this.mbtnClose.UseVisualStyleBackColor = false;
            this.mbtnClose.Click += new System.EventHandler(this.mbtnClose_Click_1);
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mbtnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.BackgroundImage = global::FoodandBeverage.Properties.Resources.refresh_Button1;
            this.mbtnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbtnRefresh.FlatAppearance.BorderSize = 0;
            this.mbtnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnRefresh.Location = new System.Drawing.Point(290, 3);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(68, 37);
            this.mbtnRefresh.TabIndex = 23;
            this.mbtnRefresh.UseVisualStyleBackColor = false;
            this.mbtnRefresh.Click += new System.EventHandler(this.MbtnRefresh_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.74107F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.25893F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.mtxtEmpSrlNo, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.mlblEmpSrlNo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mlblName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.mtxtName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel5, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 53);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.39806F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.60194F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(442, 207);
            this.tableLayoutPanel1.TabIndex = 30;
            // 
            // mtxtEmpSrlNo
            // 
            this.mtxtEmpSrlNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.mtxtEmpSrlNo.Location = new System.Drawing.Point(187, 3);
            this.mtxtEmpSrlNo.Multiline = true;
            this.mtxtEmpSrlNo.Name = "mtxtEmpSrlNo";
            this.mtxtEmpSrlNo.Size = new System.Drawing.Size(197, 30);
            this.mtxtEmpSrlNo.TabIndex = 32;
            this.mtxtEmpSrlNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtEmpSrlNo_KeyDown_1);
            this.mtxtEmpSrlNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtEmpSrlNo_KeyPress_1);
            // 
            // mlblEmpSrlNo
            // 
            this.mlblEmpSrlNo.AutoSize = true;
            this.mlblEmpSrlNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mlblEmpSrlNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.mlblEmpSrlNo.ForeColor = System.Drawing.Color.Black;
            this.mlblEmpSrlNo.Location = new System.Drawing.Point(3, 0);
            this.mlblEmpSrlNo.Name = "mlblEmpSrlNo";
            this.mlblEmpSrlNo.Size = new System.Drawing.Size(178, 55);
            this.mlblEmpSrlNo.TabIndex = 27;
            this.mlblEmpSrlNo.Text = "Employee Code :";
            this.mlblEmpSrlNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mlblName
            // 
            this.mlblName.AutoSize = true;
            this.mlblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mlblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.mlblName.ForeColor = System.Drawing.Color.Black;
            this.mlblName.Location = new System.Drawing.Point(3, 55);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(178, 48);
            this.mlblName.TabIndex = 25;
            this.mlblName.Text = "Department :";
            this.mlblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.Location = new System.Drawing.Point(3, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(172, 49);
            this.metroLabel1.TabIndex = 26;
            this.metroLabel1.Text = "Department Code : ";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mtxtName
            // 
            this.mtxtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.mtxtName.Location = new System.Drawing.Point(187, 58);
            this.mtxtName.Multiline = true;
            this.mtxtName.Name = "mtxtName";
            this.mtxtName.Size = new System.Drawing.Size(197, 30);
            this.mtxtName.TabIndex = 33;
            this.mtxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtName_KeyDown_1);
            this.mtxtName.Leave += new System.EventHandler(this.mtxtName_Leave_1);
            // 
            // mtxtCode
            // 
            this.mtxtCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.mtxtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxtCode.Location = new System.Drawing.Point(3, 3);
            this.mtxtCode.Multiline = true;
            this.mtxtCode.Name = "mtxtCode";
            this.mtxtCode.Size = new System.Drawing.Size(194, 32);
            this.mtxtCode.TabIndex = 34;
            this.mtxtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtCode_KeyDown);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.dgvDepartment);
            this.panel3.Location = new System.Drawing.Point(451, 51);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(335, 499);
            this.panel3.TabIndex = 29;
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
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepartment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvDepartment.ColumnHeadersHeight = 46;
            this.dgvDepartment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDepartment.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvDepartment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDepartment.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDepartment.EnableHeadersVisualStyles = false;
            this.dgvDepartment.Location = new System.Drawing.Point(0, 0);
            this.dgvDepartment.Name = "dgvDepartment";
            this.dgvDepartment.ReadOnly = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepartment.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvDepartment.RowHeadersVisible = false;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvDepartment.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvDepartment.RowTemplate.Height = 30;
            this.dgvDepartment.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvDepartment.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDepartment.ShowCellErrors = false;
            this.dgvDepartment.ShowCellToolTips = false;
            this.dgvDepartment.ShowEditingIcon = false;
            this.dgvDepartment.Size = new System.Drawing.Size(335, 499);
            this.dgvDepartment.TabIndex = 4;
            this.dgvDepartment.TabStop = false;
            this.dgvDepartment.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDepartment_CellContentClick);
            this.dgvDepartment.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDepartment_CellDoubleClick);
            this.dgvDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvDepartment_KeyDown);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.SteelBlue;
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.358381F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 93.64162F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.btncolse, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.label23, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnmini, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.pictureBox10, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(786, 49);
            this.tableLayoutPanel4.TabIndex = 23;
            // 
            // btncolse
            // 
            this.btncolse.BackColor = System.Drawing.Color.White;
            this.btncolse.BackgroundImage = global::FoodandBeverage.Properties.Resources.close_price_change;
            this.btncolse.FlatAppearance.BorderSize = 0;
            this.btncolse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncolse.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncolse.ForeColor = System.Drawing.Color.Red;
            this.btncolse.Location = new System.Drawing.Point(747, 3);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(25, 25);
            this.btncolse.TabIndex = 11;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click_1);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Elephant", 14.25F, System.Drawing.FontStyle.Bold);
            this.label23.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label23.Location = new System.Drawing.Point(48, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(139, 25);
            this.label23.TabIndex = 277;
            this.label23.Text = "Department";
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.BackgroundImage = global::FoodandBeverage.Properties.Resources.minimize_button;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(716, 3);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(23, 25);
            this.btnmini.TabIndex = 10;
            this.btnmini.Text = "-";
            this.btnmini.UseVisualStyleBackColor = false;
            this.btnmini.Click += new System.EventHandler(this.btnmini_Click_1);
            // 
            // pictureBox10
            // 
            this.pictureBox10.BackgroundImage = global::FoodandBeverage.Properties.Resources.yellow_star;
            this.pictureBox10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox10.Location = new System.Drawing.Point(3, 3);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(37, 22);
            this.pictureBox10.TabIndex = 278;
            this.pictureBox10.TabStop = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.mtxtCode, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(187, 106);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(200, 98);
            this.tableLayoutPanel3.TabIndex = 32;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.metroLabel1, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 106);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(178, 98);
            this.tableLayoutPanel5.TabIndex = 34;
            // 
            // FrmDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(786, 550);
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
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartment)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button mbtnRefresh;
        private System.Windows.Forms.Button mbtnClose;
        
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label metroLabel1;
        private System.Windows.Forms.Label mlblName;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button mbtnSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label mlblEmpSrlNo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.DataGridView dgvDepartment;
        private System.Windows.Forms.TextBox mtxtEmpSrlNo;
        private System.Windows.Forms.TextBox mtxtName;
        private System.Windows.Forms.TextBox mtxtCode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    }
}