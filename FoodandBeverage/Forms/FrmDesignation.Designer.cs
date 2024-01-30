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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mcmbDepartment = new MetroFramework.Controls.MetroComboBox();
            this.dtpWorkingHrs = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mlblEmpSrlNo = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.metroLabel1 = new System.Windows.Forms.Label();
            this.mlblName = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.mbtnClose = new System.Windows.Forms.Button();
            this.mbtnRefresh = new System.Windows.Forms.Button();
            this.mbtnSave = new System.Windows.Forms.Button();
            this.mtxtName = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvDesignation = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btncolse = new System.Windows.Forms.Button();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.btnmini = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.mlblEmpSrlNo.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesignation)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            this.SuspendLayout();
            // 
            // mcmbDepartment
            // 
            this.mcmbDepartment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mcmbDepartment.DropDownHeight = 250;
            this.mcmbDepartment.ForeColor = System.Drawing.Color.Black;
            this.mcmbDepartment.FormattingEnabled = true;
            this.mcmbDepartment.IntegralHeight = false;
            this.mcmbDepartment.ItemHeight = 23;
            this.mcmbDepartment.Location = new System.Drawing.Point(153, 3);
            this.mcmbDepartment.Name = "mcmbDepartment";
            this.mcmbDepartment.Size = new System.Drawing.Size(207, 29);
            this.mcmbDepartment.TabIndex = 0;
            this.mcmbDepartment.Tag = "Department";
            this.mcmbDepartment.UseSelectable = true;
            this.mcmbDepartment.SelectedIndexChanged += new System.EventHandler(this.mcmbDepartment_SelectedIndexChanged);
            this.mcmbDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbDepartment_KeyDown);
            // 
            // dtpWorkingHrs
            // 
            this.dtpWorkingHrs.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpWorkingHrs.CustomFormat = "HH:mm";
            this.dtpWorkingHrs.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtpWorkingHrs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpWorkingHrs.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWorkingHrs.Location = new System.Drawing.Point(161, 3);
            this.dtpWorkingHrs.Name = "dtpWorkingHrs";
            this.dtpWorkingHrs.ShowUpDown = true;
            this.dtpWorkingHrs.Size = new System.Drawing.Size(73, 26);
            this.dtpWorkingHrs.TabIndex = 2;
            this.dtpWorkingHrs.ValueChanged += new System.EventHandler(this.dtpWorkingHrs_ValueChanged);
            this.dtpWorkingHrs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbDepartment_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(138, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(9, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(138, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(9, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "*";
            // 
            // mlblEmpSrlNo
            // 
            this.mlblEmpSrlNo.BackColor = System.Drawing.Color.AliceBlue;
            this.mlblEmpSrlNo.Controls.Add(this.tableLayoutPanel4);
            this.mlblEmpSrlNo.Controls.Add(this.tableLayoutPanel1);
            this.mlblEmpSrlNo.Controls.Add(this.panel2);
            this.mlblEmpSrlNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mlblEmpSrlNo.Location = new System.Drawing.Point(0, 0);
            this.mlblEmpSrlNo.Name = "mlblEmpSrlNo";
            this.mlblEmpSrlNo.Size = new System.Drawing.Size(794, 557);
            this.mlblEmpSrlNo.TabIndex = 14;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.47369F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.52632F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 322F));
            this.tableLayoutPanel1.Controls.Add(this.metroLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.mcmbDepartment, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.mlblName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.mtxtName, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 49);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.24675F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.75325F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 116F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 308F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(473, 502);
            this.tableLayoutPanel1.TabIndex = 21;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.Location = new System.Drawing.Point(3, 0);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(127, 25);
            this.metroLabel1.TabIndex = 13;
            this.metroLabel1.Text = "Department :";
            // 
            // mlblName
            // 
            this.mlblName.AutoSize = true;
            this.mlblName.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblName.ForeColor = System.Drawing.Color.Black;
            this.mlblName.Location = new System.Drawing.Point(3, 41);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(125, 25);
            this.mlblName.TabIndex = 14;
            this.mlblName.Text = "Designation :";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dtpWorkingHrs, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(153, 80);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(317, 99);
            this.tableLayoutPanel2.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 25);
            this.label4.TabIndex = 15;
            this.label4.Text = "Working Hours :";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 78F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel3.Controls.Add(this.mbtnClose, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.mbtnRefresh, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.mbtnSave, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(153, 196);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(317, 44);
            this.tableLayoutPanel3.TabIndex = 16;
            // 
            // mbtnClose
            // 
            this.mbtnClose.BackgroundImage = global::FoodandBeverage.Properties.Resources.Closebutton;
            this.mbtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbtnClose.FlatAppearance.BorderSize = 0;
            this.mbtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnClose.Location = new System.Drawing.Point(239, 3);
            this.mbtnClose.Name = "mbtnClose";
            this.mbtnClose.Size = new System.Drawing.Size(75, 38);
            this.mbtnClose.TabIndex = 18;
            this.mbtnClose.UseVisualStyleBackColor = true;
            this.mbtnClose.Click += new System.EventHandler(this.mbtnClose_Click);
            this.mbtnClose.MouseLeave += new System.EventHandler(this.mbtnClose_MouseLeave);
            this.mbtnClose.MouseHover += new System.EventHandler(this.mbtnClose_MouseHover);
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackgroundImage = global::FoodandBeverage.Properties.Resources.refresh_Button1;
            this.mbtnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbtnRefresh.FlatAppearance.BorderSize = 0;
            this.mbtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnRefresh.Location = new System.Drawing.Point(161, 3);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(72, 38);
            this.mbtnRefresh.TabIndex = 17;
            this.mbtnRefresh.UseVisualStyleBackColor = true;
            this.mbtnRefresh.Click += new System.EventHandler(this.MbtnRefresh_Click);
            this.mbtnRefresh.MouseLeave += new System.EventHandler(this.mbtnRefresh_MouseLeave);
            this.mbtnRefresh.MouseHover += new System.EventHandler(this.mbtnRefresh_MouseHover);
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackgroundImage = global::FoodandBeverage.Properties.Resources.SaveButton;
            this.mbtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbtnSave.FlatAppearance.BorderSize = 0;
            this.mbtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnSave.Location = new System.Drawing.Point(82, 3);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(73, 38);
            this.mbtnSave.TabIndex = 16;
            this.mbtnSave.UseVisualStyleBackColor = true;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            this.mbtnSave.MouseLeave += new System.EventHandler(this.mbtnSave_MouseLeave);
            this.mbtnSave.MouseHover += new System.EventHandler(this.mbtnClose_MouseHover);
            // 
            // mtxtName
            // 
            this.mtxtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.mtxtName.Location = new System.Drawing.Point(153, 44);
            this.mtxtName.Multiline = true;
            this.mtxtName.Name = "mtxtName";
            this.mtxtName.Size = new System.Drawing.Size(207, 29);
            this.mtxtName.TabIndex = 17;
            this.mtxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtName_KeyDown);
            this.mtxtName.Leave += new System.EventHandler(this.mtxtName_Leave_1);
            this.mtxtName.Resize += new System.EventHandler(this.mtxtName_Resize);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.dgvDesignation);
            this.panel2.Location = new System.Drawing.Point(479, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(312, 502);
            this.panel2.TabIndex = 20;
            // 
            // dgvDesignation
            // 
            this.dgvDesignation.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvDesignation.AllowUserToAddRows = false;
            this.dgvDesignation.AllowUserToDeleteRows = false;
            this.dgvDesignation.AllowUserToResizeColumns = false;
            this.dgvDesignation.AllowUserToResizeRows = false;
            this.dgvDesignation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDesignation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDesignation.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDesignation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvDesignation.ColumnHeadersHeight = 46;
            this.dgvDesignation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDesignation.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvDesignation.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDesignation.EnableHeadersVisualStyles = false;
            this.dgvDesignation.Location = new System.Drawing.Point(5, 5);
            this.dgvDesignation.MultiSelect = false;
            this.dgvDesignation.Name = "dgvDesignation";
            this.dgvDesignation.ReadOnly = true;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDesignation.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvDesignation.RowHeadersVisible = false;
            this.dgvDesignation.RowTemplate.Height = 30;
            this.dgvDesignation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvDesignation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDesignation.ShowCellErrors = false;
            this.dgvDesignation.ShowCellToolTips = false;
            this.dgvDesignation.ShowEditingIcon = false;
            this.dgvDesignation.Size = new System.Drawing.Size(303, 480);
            this.dgvDesignation.TabIndex = 4;
            this.dgvDesignation.TabStop = false;
            this.dgvDesignation.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDesignation_CellDoubleClick);
            this.dgvDesignation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvDesignation_KeyDown);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.SteelBlue;
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.358381F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 93.64162F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.btncolse, 3, 0);
            this.tableLayoutPanel4.Controls.Add(this.pictureBox10, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.btnmini, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.label23, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(794, 49);
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
            this.btncolse.Location = new System.Drawing.Point(759, 3);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(25, 25);
            this.btncolse.TabIndex = 11;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // pictureBox10
            // 
            this.pictureBox10.BackgroundImage = global::FoodandBeverage.Properties.Resources.white_star;
            this.pictureBox10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox10.Location = new System.Drawing.Point(3, 3);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(37, 22);
            this.pictureBox10.TabIndex = 278;
            this.pictureBox10.TabStop = false;
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.BackgroundImage = global::FoodandBeverage.Properties.Resources.minimize_button;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(728, 3);
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
            this.label23.Font = new System.Drawing.Font("Elephant", 14.25F, System.Drawing.FontStyle.Bold);
            this.label23.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label23.Location = new System.Drawing.Point(49, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(136, 25);
            this.label23.TabIndex = 277;
            this.label23.Text = "Designation";
            // 
            // FrmDesignation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 557);
            this.Controls.Add(this.mlblEmpSrlNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDesignation";
            this.Load += new System.EventHandler(this.FrmDesignation_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDesignation_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmDesignation_KeyUp);
            this.mlblEmpSrlNo.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesignation)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroComboBox mcmbDepartment;
        private System.Windows.Forms.DateTimePicker dtpWorkingHrs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel mlblEmpSrlNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label mlblName;
        private System.Windows.Forms.Label metroLabel1;
        private System.Windows.Forms.Button mbtnClose;
        private System.Windows.Forms.Button mbtnRefresh;
        private System.Windows.Forms.Button mbtnSave;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.DataGridView dgvDesignation;
        private System.Windows.Forms.TextBox mtxtName;
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