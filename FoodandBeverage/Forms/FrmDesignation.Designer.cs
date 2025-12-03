namespace FoodandBeverage.Forms
{
    partial class FrmDesignation
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
            this.dtpWorkingHrs = new System.Windows.Forms.DateTimePicker();
            this.dgvDesignation = new System.Windows.Forms.DataGridView();
            this.desid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Department = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Designation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dWorkingHoursno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.metroLabel1 = new System.Windows.Forms.Label();
            this.mcmbDepartment = new System.Windows.Forms.ComboBox();
            this.mlblName = new System.Windows.Forms.Label();
            this.mtxtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mbtnSave = new System.Windows.Forms.Button();
            this.mbtnRefresh = new System.Windows.Forms.Button();
            this.mbtnClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btncolse = new System.Windows.Forms.Button();
            this.btnmini = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_design = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_buttons = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesignation)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tlp_main.SuspendLayout();
            this.tlp_design.SuspendLayout();
            this.tlp_buttons.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpWorkingHrs
            // 
            this.dtpWorkingHrs.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpWorkingHrs.CustomFormat = "HH:mm";
            this.dtpWorkingHrs.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtpWorkingHrs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpWorkingHrs.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWorkingHrs.Location = new System.Drawing.Point(535, 21);
            this.dtpWorkingHrs.Name = "dtpWorkingHrs";
            this.dtpWorkingHrs.ShowUpDown = true;
            this.dtpWorkingHrs.Size = new System.Drawing.Size(201, 26);
            this.dtpWorkingHrs.TabIndex = 2;
            this.dtpWorkingHrs.Value = new System.DateTime(2024, 7, 4, 8, 0, 0, 0);
            this.dtpWorkingHrs.ValueChanged += new System.EventHandler(this.dtpWorkingHrs_ValueChanged);
            // 
            // dgvDesignation
            // 
            this.dgvDesignation.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvDesignation.AllowUserToAddRows = false;
            this.dgvDesignation.AllowUserToDeleteRows = false;
            this.dgvDesignation.AllowUserToResizeColumns = false;
            this.dgvDesignation.AllowUserToResizeRows = false;
            this.dgvDesignation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDesignation.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.dgvDesignation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDesignation.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDesignation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDesignation.ColumnHeadersHeight = 46;
            this.dgvDesignation.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.desid,
            this.Department,
            this.Designation,
            this.dWorkingHoursno});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDesignation.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDesignation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDesignation.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDesignation.EnableHeadersVisualStyles = false;
            this.dgvDesignation.Location = new System.Drawing.Point(8, 193);
            this.dgvDesignation.MultiSelect = false;
            this.dgvDesignation.Name = "dgvDesignation";
            this.dgvDesignation.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDesignation.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDesignation.RowHeadersVisible = false;
            this.dgvDesignation.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.dgvDesignation.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvDesignation.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;
            this.dgvDesignation.RowTemplate.Height = 30;
            this.dgvDesignation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvDesignation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDesignation.ShowCellErrors = false;
            this.dgvDesignation.ShowCellToolTips = false;
            this.dgvDesignation.ShowEditingIcon = false;
            this.dgvDesignation.Size = new System.Drawing.Size(770, 280);
            this.dgvDesignation.TabIndex = 4;
            this.dgvDesignation.TabStop = false;
            this.dgvDesignation.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDesignation_CellDoubleClick);
            this.dgvDesignation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvDesignation_KeyDown);
            // 
            // desid
            // 
            this.desid.HeaderText = "ID";
            this.desid.MinimumWidth = 6;
            this.desid.Name = "desid";
            this.desid.ReadOnly = true;
            this.desid.Visible = false;
            // 
            // Department
            // 
            this.Department.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Department.HeaderText = "Department";
            this.Department.MinimumWidth = 6;
            this.Department.Name = "Department";
            this.Department.ReadOnly = true;
            // 
            // Designation
            // 
            this.Designation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Designation.HeaderText = "Designation";
            this.Designation.MinimumWidth = 6;
            this.Designation.Name = "Designation";
            this.Designation.ReadOnly = true;
            this.Designation.Width = 119;
            // 
            // dWorkingHoursno
            // 
            this.dWorkingHoursno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dWorkingHoursno.HeaderText = "WorkingHoursno";
            this.dWorkingHoursno.MinimumWidth = 6;
            this.dWorkingHoursno.Name = "dWorkingHoursno";
            this.dWorkingHoursno.ReadOnly = true;
            this.dWorkingHoursno.Width = 153;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.dtpWorkingHrs, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.metroLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mcmbDepartment, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.mlblName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.mtxtName, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(764, 86);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.Location = new System.Drawing.Point(3, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(263, 18);
            this.metroLabel1.TabIndex = 13;
            this.metroLabel1.Text = "Department ";
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mcmbDepartment
            // 
            this.mcmbDepartment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.mcmbDepartment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mcmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mcmbDepartment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mcmbDepartment.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mcmbDepartment.FormattingEnabled = true;
            this.mcmbDepartment.Location = new System.Drawing.Point(3, 21);
            this.mcmbDepartment.Name = "mcmbDepartment";
            this.mcmbDepartment.Size = new System.Drawing.Size(263, 26);
            this.mcmbDepartment.TabIndex = 18;
            this.mcmbDepartment.SelectionChangeCommitted += new System.EventHandler(this.mcmbDepartment_SelectionChangeCommitted);
            // 
            // mlblName
            // 
            this.mlblName.AutoSize = true;
            this.mlblName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mlblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblName.ForeColor = System.Drawing.Color.Black;
            this.mlblName.Location = new System.Drawing.Point(272, 0);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(257, 18);
            this.mlblName.TabIndex = 14;
            this.mlblName.Text = "Designation ";
            this.mlblName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mtxtName
            // 
            this.mtxtName.AcceptsReturn = true;
            this.mtxtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.mtxtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mtxtName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mtxtName.Location = new System.Drawing.Point(272, 21);
            this.mtxtName.Multiline = true;
            this.mtxtName.Name = "mtxtName";
            this.mtxtName.Size = new System.Drawing.Size(257, 49);
            this.mtxtName.TabIndex = 17;
            this.mtxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtName_KeyDown);
            this.mtxtName.Leave += new System.EventHandler(this.mtxtName_Leave_1);
            this.mtxtName.Resize += new System.EventHandler(this.mtxtName_Resize);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(535, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(226, 18);
            this.label4.TabIndex = 15;
            this.label4.Text = "Working Hours ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackgroundImage = global::FoodandBeverage.Properties.Resources.SaveButton;
            this.mbtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbtnSave.FlatAppearance.BorderSize = 0;
            this.mbtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnSave.Location = new System.Drawing.Point(563, 3);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(64, 35);
            this.mbtnSave.TabIndex = 16;
            this.mbtnSave.UseVisualStyleBackColor = true;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            this.mbtnSave.MouseLeave += new System.EventHandler(this.mbtnSave_MouseLeave);
            this.mbtnSave.MouseHover += new System.EventHandler(this.mbtnClose_MouseHover);
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_REFESH;
            this.mbtnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbtnRefresh.FlatAppearance.BorderSize = 0;
            this.mbtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnRefresh.Location = new System.Drawing.Point(633, 3);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(64, 35);
            this.mbtnRefresh.TabIndex = 17;
            this.mbtnRefresh.UseVisualStyleBackColor = true;
            this.mbtnRefresh.Click += new System.EventHandler(this.MbtnRefresh_Click);
            this.mbtnRefresh.MouseLeave += new System.EventHandler(this.mbtnRefresh_MouseLeave);
            this.mbtnRefresh.MouseHover += new System.EventHandler(this.mbtnRefresh_MouseHover);
            // 
            // mbtnClose
            // 
            this.mbtnClose.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_CANCEL;
            this.mbtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbtnClose.FlatAppearance.BorderSize = 0;
            this.mbtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnClose.Location = new System.Drawing.Point(703, 3);
            this.mbtnClose.Name = "mbtnClose";
            this.mbtnClose.Size = new System.Drawing.Size(64, 35);
            this.mbtnClose.TabIndex = 18;
            this.mbtnClose.UseVisualStyleBackColor = true;
            this.mbtnClose.Click += new System.EventHandler(this.mbtnClose_Click);
            this.mbtnClose.MouseLeave += new System.EventHandler(this.mbtnClose_MouseLeave);
            this.mbtnClose.MouseHover += new System.EventHandler(this.mbtnClose_MouseHover);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.SteelBlue;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.btncolse, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnmini, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.label23, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(8, 8);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(770, 32);
            this.tableLayoutPanel4.TabIndex = 22;
            // 
            // btncolse
            // 
            this.btncolse.BackColor = System.Drawing.Color.White;
            this.btncolse.BackgroundImage = global::FoodandBeverage.Properties.Resources.close_price_change;
            this.btncolse.FlatAppearance.BorderSize = 0;
            this.btncolse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncolse.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncolse.ForeColor = System.Drawing.Color.Red;
            this.btncolse.Location = new System.Drawing.Point(735, 3);
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
            this.btnmini.BackgroundImage = global::FoodandBeverage.Properties.Resources.minimize_button;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(705, 3);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(23, 25);
            this.btnmini.TabIndex = 10;
            this.btnmini.Text = "-";
            this.btnmini.UseVisualStyleBackColor = false;
            this.btnmini.Click += new System.EventHandler(this.btnmini_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.label23.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label23.Location = new System.Drawing.Point(3, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(696, 32);
            this.label23.TabIndex = 277;
            this.label23.Text = "Designation";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlp_main
            // 
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_design, 0, 1);
            this.tlp_main.Controls.Add(this.dgvDesignation, 0, 3);
            this.tlp_main.Controls.Add(this.tlp_buttons, 0, 2);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tlp_main.RowCount = 4;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.Size = new System.Drawing.Size(786, 481);
            this.tlp_main.TabIndex = 15;
            // 
            // tlp_design
            // 
            this.tlp_design.ColumnCount = 1;
            this.tlp_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_design.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlp_design.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_design.Location = new System.Drawing.Point(8, 46);
            this.tlp_design.Name = "tlp_design";
            this.tlp_design.RowCount = 2;
            this.tlp_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 98F));
            this.tlp_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tlp_design.Size = new System.Drawing.Size(770, 94);
            this.tlp_design.TabIndex = 23;
            // 
            // tlp_buttons
            // 
            this.tlp_buttons.ColumnCount = 4;
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_buttons.Controls.Add(this.mbtnSave, 1, 0);
            this.tlp_buttons.Controls.Add(this.mbtnClose, 3, 0);
            this.tlp_buttons.Controls.Add(this.mbtnRefresh, 2, 0);
            this.tlp_buttons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_buttons.Location = new System.Drawing.Point(8, 146);
            this.tlp_buttons.Name = "tlp_buttons";
            this.tlp_buttons.RowCount = 1;
            this.tlp_buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_buttons.Size = new System.Drawing.Size(770, 41);
            this.tlp_buttons.TabIndex = 24;
            // 
            // FrmDesignation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(786, 481);
            this.Controls.Add(this.tlp_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDesignation";
            this.Tag = "20";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDesignation_FormClosing);
            this.Load += new System.EventHandler(this.FrmDesignation_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDesignation_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmDesignation_KeyUp);
            this.Resize += new System.EventHandler(this.FrmDesignation_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesignation)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tlp_main.ResumeLayout(false);
            this.tlp_design.ResumeLayout(false);
            this.tlp_buttons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dtpWorkingHrs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label mlblName;
        private System.Windows.Forms.Label metroLabel1;
        private System.Windows.Forms.Button mbtnClose;
        private System.Windows.Forms.Button mbtnRefresh;
        private System.Windows.Forms.Button mbtnSave;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DataGridView dgvDesignation;
        private System.Windows.Forms.TextBox mtxtName;
        private System.Windows.Forms.ComboBox mcmbDepartment;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TableLayoutPanel tlp_design;
        private System.Windows.Forms.TableLayoutPanel tlp_buttons;
        private System.Windows.Forms.DataGridViewTextBoxColumn desid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Department;
        private System.Windows.Forms.DataGridViewTextBoxColumn Designation;
        private System.Windows.Forms.DataGridViewTextBoxColumn dWorkingHoursno;
        //private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        //private Guna.UI2.WinForms.Guna2Button guna2Button2;
        //private Guna.UI2.WinForms.Guna2GradientTileButton guna2GradientTileButton1;
        //private Guna.UI2.WinForms.Guna2Button guna2Button3;
        //private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox7;
        //private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        //private Guna.UI2.WinForms.Guna2Button guna2Button1;
        //private Guna.UI2.WinForms.Guna2GradientTileButton guna2GradientTileButton2;
        //private Guna.UI2.WinForms.Guna2Button guna2Button4;
    }
}