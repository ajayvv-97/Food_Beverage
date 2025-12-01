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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.mlblName = new System.Windows.Forms.Label();
            this.mtxtName = new System.Windows.Forms.TextBox();
            this.metroLabel1 = new System.Windows.Forms.Label();
            this.mlblEmpSrlNo = new System.Windows.Forms.Label();
            this.mtxtCode = new System.Windows.Forms.TextBox();
            this.mtxtEmpSrlNo = new System.Windows.Forms.TextBox();
            this.mbtnSave = new System.Windows.Forms.Button();
            this.mbtnRefresh = new System.Windows.Forms.Button();
            this.mbtnClose = new System.Windows.Forms.Button();
            this.tlp_header = new System.Windows.Forms.TableLayoutPanel();
            this.btncolse = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.btnmini = new System.Windows.Forms.Button();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_buttons = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_design = new System.Windows.Forms.TableLayoutPanel();
            this.dgvDepartment = new System.Windows.Forms.DataGridView();
            this.did = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Departmentcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.department = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmployeeSrlNoStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlp_header.SuspendLayout();
            this.tlp_main.SuspendLayout();
            this.tlp_buttons.SuspendLayout();
            this.tlp_design.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartment)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.04167F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.375F));
            this.tableLayoutPanel1.Controls.Add(this.mlblName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mtxtName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.metroLabel1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.mlblEmpSrlNo, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.mtxtCode, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.mtxtEmpSrlNo, 2, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.14286F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.85714F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1018, 118);
            this.tableLayoutPanel1.TabIndex = 30;
            // 
            // mlblName
            // 
            this.mlblName.AutoSize = true;
            this.mlblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mlblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.mlblName.ForeColor = System.Drawing.Color.Black;
            this.mlblName.Location = new System.Drawing.Point(4, 0);
            this.mlblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(318, 43);
            this.mlblName.TabIndex = 25;
            this.mlblName.Text = "Department ";
            this.mlblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mtxtName
            // 
            this.mtxtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.mtxtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxtName.Location = new System.Drawing.Point(4, 47);
            this.mtxtName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtxtName.Multiline = true;
            this.mtxtName.Name = "mtxtName";
            this.mtxtName.Size = new System.Drawing.Size(318, 41);
            this.mtxtName.TabIndex = 33;
            this.mtxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtName_KeyDown_1);
            this.mtxtName.Leave += new System.EventHandler(this.mtxtName_Leave_1);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.Location = new System.Drawing.Point(330, 0);
            this.metroLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(321, 43);
            this.metroLabel1.TabIndex = 26;
            this.metroLabel1.Text = "Department Code ";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mlblEmpSrlNo
            // 
            this.mlblEmpSrlNo.AutoSize = true;
            this.mlblEmpSrlNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mlblEmpSrlNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.mlblEmpSrlNo.ForeColor = System.Drawing.Color.Black;
            this.mlblEmpSrlNo.Location = new System.Drawing.Point(659, 0);
            this.mlblEmpSrlNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.mlblEmpSrlNo.Name = "mlblEmpSrlNo";
            this.mlblEmpSrlNo.Size = new System.Drawing.Size(355, 43);
            this.mlblEmpSrlNo.TabIndex = 27;
            this.mlblEmpSrlNo.Text = "Employee Code ";
            this.mlblEmpSrlNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mtxtCode
            // 
            this.mtxtCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.mtxtCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxtCode.Location = new System.Drawing.Point(330, 47);
            this.mtxtCode.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtxtCode.Multiline = true;
            this.mtxtCode.Name = "mtxtCode";
            this.mtxtCode.Size = new System.Drawing.Size(321, 41);
            this.mtxtCode.TabIndex = 34;
            this.mtxtCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtCode_KeyDown);
            // 
            // mtxtEmpSrlNo
            // 
            this.mtxtEmpSrlNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.mtxtEmpSrlNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxtEmpSrlNo.Location = new System.Drawing.Point(659, 47);
            this.mtxtEmpSrlNo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtxtEmpSrlNo.Multiline = true;
            this.mtxtEmpSrlNo.Name = "mtxtEmpSrlNo";
            this.mtxtEmpSrlNo.Size = new System.Drawing.Size(355, 41);
            this.mtxtEmpSrlNo.TabIndex = 32;
            this.mtxtEmpSrlNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtEmpSrlNo_KeyDown_1);
            this.mtxtEmpSrlNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtEmpSrlNo_KeyPress_1);
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.BackgroundImage = global::FoodandBeverage.Properties.Resources.SaveButton;
            this.mbtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.mbtnSave.FlatAppearance.BorderSize = 0;
            this.mbtnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnSave.Location = new System.Drawing.Point(669, 4);
            this.mbtnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(133, 38);
            this.mbtnSave.TabIndex = 22;
            this.mbtnSave.UseVisualStyleBackColor = false;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mbtnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_REFESH;
            this.mbtnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.mbtnRefresh.FlatAppearance.BorderSize = 0;
            this.mbtnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnRefresh.Location = new System.Drawing.Point(810, 4);
            this.mbtnRefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(96, 38);
            this.mbtnRefresh.TabIndex = 23;
            this.mbtnRefresh.UseVisualStyleBackColor = false;
            this.mbtnRefresh.Click += new System.EventHandler(this.MbtnRefresh_Click);
            // 
            // mbtnClose
            // 
            this.mbtnClose.BackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_CANCEL;
            this.mbtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.mbtnClose.FlatAppearance.BorderSize = 0;
            this.mbtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnClose.Location = new System.Drawing.Point(914, 4);
            this.mbtnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mbtnClose.Name = "mbtnClose";
            this.mbtnClose.Size = new System.Drawing.Size(108, 38);
            this.mbtnClose.TabIndex = 24;
            this.mbtnClose.UseVisualStyleBackColor = false;
            this.mbtnClose.Click += new System.EventHandler(this.mbtnClose_Click_1);
            // 
            // tlp_header
            // 
            this.tlp_header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(80)))), ((int)(((byte)(185)))));
            this.tlp_header.ColumnCount = 3;
            this.tlp_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tlp_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tlp_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlp_header.Controls.Add(this.btncolse, 2, 0);
            this.tlp_header.Controls.Add(this.label23, 0, 0);
            this.tlp_header.Controls.Add(this.btnmini, 1, 0);
            this.tlp_header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_header.Location = new System.Drawing.Point(11, 10);
            this.tlp_header.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_header.Name = "tlp_header";
            this.tlp_header.RowCount = 1;
            this.tlp_header.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_header.Size = new System.Drawing.Size(1026, 39);
            this.tlp_header.TabIndex = 23;
            // 
            // btncolse
            // 
            this.btncolse.BackColor = System.Drawing.Color.White;
            this.btncolse.BackgroundImage = global::FoodandBeverage.Properties.Resources.close_price_change;
            this.btncolse.FlatAppearance.BorderSize = 0;
            this.btncolse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncolse.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncolse.ForeColor = System.Drawing.Color.Red;
            this.btncolse.Location = new System.Drawing.Point(983, 4);
            this.btncolse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(33, 31);
            this.btncolse.TabIndex = 11;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click_1);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.label23.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label23.Location = new System.Drawing.Point(4, 0);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(932, 39);
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
            this.btnmini.Location = new System.Drawing.Point(944, 4);
            this.btnmini.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(31, 31);
            this.btnmini.TabIndex = 10;
            this.btnmini.Text = "-";
            this.btnmini.UseVisualStyleBackColor = false;
            this.btnmini.Click += new System.EventHandler(this.btnmini_Click_1);
            // 
            // tlp_main
            // 
            this.tlp_main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tlp_header, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_buttons, 0, 2);
            this.tlp_main.Controls.Add(this.tlp_design, 0, 1);
            this.tlp_main.Controls.Add(this.dgvDepartment, 0, 3);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tlp_main.RowCount = 4;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.Size = new System.Drawing.Size(1048, 555);
            this.tlp_main.TabIndex = 31;
            // 
            // tlp_buttons
            // 
            this.tlp_buttons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_buttons.ColumnCount = 4;
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_buttons.Controls.Add(this.mbtnClose, 3, 0);
            this.tlp_buttons.Controls.Add(this.mbtnRefresh, 2, 0);
            this.tlp_buttons.Controls.Add(this.mbtnSave, 1, 0);
            this.tlp_buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_buttons.Location = new System.Drawing.Point(11, 194);
            this.tlp_buttons.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_buttons.Name = "tlp_buttons";
            this.tlp_buttons.RowCount = 1;
            this.tlp_buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_buttons.Size = new System.Drawing.Size(1026, 46);
            this.tlp_buttons.TabIndex = 31;
            // 
            // tlp_design
            // 
            this.tlp_design.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_design.ColumnCount = 1;
            this.tlp_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlp_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlp_design.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlp_design.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_design.Location = new System.Drawing.Point(11, 57);
            this.tlp_design.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_design.Name = "tlp_design";
            this.tlp_design.RowCount = 2;
            this.tlp_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 98F));
            this.tlp_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tlp_design.Size = new System.Drawing.Size(1026, 129);
            this.tlp_design.TabIndex = 31;
            // 
            // dgvDepartment
            // 
            this.dgvDepartment.AllowUserToAddRows = false;
            this.dgvDepartment.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepartment.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDepartment.ColumnHeadersHeight = 46;
            this.dgvDepartment.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.did,
            this.Departmentcode,
            this.department,
            this.EmployeeSrlNoStart});
            this.dgvDepartment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDepartment.Location = new System.Drawing.Point(11, 248);
            this.dgvDepartment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvDepartment.Name = "dgvDepartment";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDepartment.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDepartment.RowHeadersVisible = false;
            this.dgvDepartment.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.dgvDepartment.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDepartment.RowTemplate.Height = 30;
            this.dgvDepartment.Size = new System.Drawing.Size(1026, 325);
            this.dgvDepartment.TabIndex = 32;
            // 
            // did
            // 
            this.did.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.did.HeaderText = "ID";
            this.did.MinimumWidth = 6;
            this.did.Name = "did";
            this.did.Visible = false;
            this.did.Width = 60;
            // 
            // Departmentcode
            // 
            this.Departmentcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Departmentcode.HeaderText = "Department Code";
            this.Departmentcode.MinimumWidth = 6;
            this.Departmentcode.Name = "Departmentcode";
            // 
            // department
            // 
            this.department.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.department.HeaderText = "Department";
            this.department.MinimumWidth = 6;
            this.department.Name = "department";
            this.department.Width = 142;
            // 
            // EmployeeSrlNoStart
            // 
            this.EmployeeSrlNoStart.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.EmployeeSrlNoStart.HeaderText = "Employee Sr.No";
            this.EmployeeSrlNoStart.MinimumWidth = 6;
            this.EmployeeSrlNoStart.Name = "EmployeeSrlNoStart";
            this.EmployeeSrlNoStart.Width = 183;
            // 
            // FrmDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1048, 555);
            this.Controls.Add(this.tlp_main);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDepartment";
            this.Tag = "19";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDepartment_FormClosing);
            this.Load += new System.EventHandler(this.FrmDepartment_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDepartment_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmDepartment_KeyUp);
            this.Resize += new System.EventHandler(this.FrmDepartment_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlp_header.ResumeLayout(false);
            this.tlp_header.PerformLayout();
            this.tlp_main.ResumeLayout(false);
            this.tlp_buttons.ResumeLayout(false);
            this.tlp_design.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDepartment)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button mbtnRefresh;
        private System.Windows.Forms.Button mbtnClose;
        private System.Windows.Forms.Label metroLabel1;
        private System.Windows.Forms.Label mlblName;
        private System.Windows.Forms.Button mbtnSave;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label mlblEmpSrlNo;
        private System.Windows.Forms.TableLayoutPanel tlp_header;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.TextBox mtxtEmpSrlNo;
        private System.Windows.Forms.TextBox mtxtName;
        private System.Windows.Forms.TextBox mtxtCode;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TableLayoutPanel tlp_design;
        private System.Windows.Forms.TableLayoutPanel tlp_buttons;
        private System.Windows.Forms.DataGridView dgvDepartment;
        private System.Windows.Forms.DataGridViewTextBoxColumn did;
        private System.Windows.Forms.DataGridViewTextBoxColumn Departmentcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn department;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmployeeSrlNoStart;
    }
}