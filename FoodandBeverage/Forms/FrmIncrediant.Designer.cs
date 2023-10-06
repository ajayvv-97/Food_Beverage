
namespace FoodandBeverage.Forms
{
    partial class FrmIncrediant
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlp_navbar = new System.Windows.Forms.TableLayoutPanel();
            this.btnmini = new System.Windows.Forms.Button();
            this.btncolse = new System.Windows.Forms.Button();
            this.tlp_registartion_toggle = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_outer_design = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_registration = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_menu = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtdesc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chb_active = new System.Windows.Forms.CheckBox();
            this.dgv_registartion = new System.Windows.Forms.DataGridView();
            this.slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmb_item = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmb_unit = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.action = new System.Windows.Forms.DataGridViewImageColumn();
            this.Ing_DetailsId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlp_button = new System.Windows.Forms.TableLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.txtincrid = new System.Windows.Forms.TextBox();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_navbar.SuspendLayout();
            this.tlp_registartion_toggle.SuspendLayout();
            this.tlp_outer_design.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlp_registration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_registartion)).BeginInit();
            this.tlp_button.SuspendLayout();
            this.tlp_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_navbar
            // 
            this.tlp_navbar.BackColor = System.Drawing.Color.SteelBlue;
            this.tlp_navbar.ColumnCount = 3;
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_navbar.Controls.Add(this.btnmini, 1, 0);
            this.tlp_navbar.Controls.Add(this.btncolse, 2, 0);
            this.tlp_navbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_navbar.Location = new System.Drawing.Point(3, 3);
            this.tlp_navbar.Name = "tlp_navbar";
            this.tlp_navbar.RowCount = 1;
            this.tlp_navbar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.Size = new System.Drawing.Size(847, 44);
            this.tlp_navbar.TabIndex = 15;
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(788, 3);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(25, 35);
            this.btnmini.TabIndex = 9;
            this.btnmini.Text = "-";
            this.btnmini.UseVisualStyleBackColor = false;
            this.btnmini.Click += new System.EventHandler(this.btnmini_Click);
            // 
            // btncolse
            // 
            this.btncolse.BackColor = System.Drawing.Color.White;
            this.btncolse.FlatAppearance.BorderSize = 0;
            this.btncolse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncolse.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncolse.ForeColor = System.Drawing.Color.Red;
            this.btncolse.Location = new System.Drawing.Point(819, 3);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(25, 35);
            this.btncolse.TabIndex = 10;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // tlp_registartion_toggle
            // 
            this.tlp_registartion_toggle.ColumnCount = 2;
            this.tlp_registartion_toggle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_registartion_toggle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlp_registartion_toggle.Controls.Add(this.tlp_outer_design, 0, 1);
            this.tlp_registartion_toggle.Controls.Add(this.tlp_button, 0, 2);
            this.tlp_registartion_toggle.Controls.Add(this.txtincrid, 1, 2);
            this.tlp_registartion_toggle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_registartion_toggle.Location = new System.Drawing.Point(3, 53);
            this.tlp_registartion_toggle.Name = "tlp_registartion_toggle";
            this.tlp_registartion_toggle.RowCount = 3;
            this.tlp_registartion_toggle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_registartion_toggle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_registartion_toggle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_registartion_toggle.Size = new System.Drawing.Size(847, 484);
            this.tlp_registartion_toggle.TabIndex = 15;
            // 
            // tlp_outer_design
            // 
            this.tlp_outer_design.ColumnCount = 3;
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 815F));
            this.tlp_outer_design.Controls.Add(this.tableLayoutPanel1, 2, 1);
            this.tlp_outer_design.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_outer_design.Location = new System.Drawing.Point(3, 3);
            this.tlp_outer_design.Name = "tlp_outer_design";
            this.tlp_outer_design.RowCount = 3;
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 126F));
            this.tlp_outer_design.Size = new System.Drawing.Size(833, 391);
            this.tlp_outer_design.TabIndex = 11;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tlp_registration, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv_registartion, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(22, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.29132F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.70869F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(808, 357);
            this.tableLayoutPanel1.TabIndex = 13;
            // 
            // tlp_registration
            // 
            this.tlp_registration.BackColor = System.Drawing.Color.White;
            this.tlp_registration.ColumnCount = 3;
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.20236F));
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 37.72102F));
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.88016F));
            this.tlp_registration.Controls.Add(this.label1, 0, 0);
            this.tlp_registration.Controls.Add(this.cmb_menu, 0, 1);
            this.tlp_registration.Controls.Add(this.label5, 1, 0);
            this.tlp_registration.Controls.Add(this.txtdesc, 1, 1);
            this.tlp_registration.Controls.Add(this.label4, 2, 0);
            this.tlp_registration.Controls.Add(this.chb_active, 2, 1);
            this.tlp_registration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_registration.Location = new System.Drawing.Point(3, 3);
            this.tlp_registration.Name = "tlp_registration";
            this.tlp_registration.RowCount = 2;
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.84211F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.15789F));
            this.tlp_registration.Size = new System.Drawing.Size(802, 95);
            this.tlp_registration.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 35);
            this.label1.TabIndex = 5;
            this.label1.Text = "Menu Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmb_menu
            // 
            this.cmb_menu.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_menu.FormattingEnabled = true;
            this.cmb_menu.Location = new System.Drawing.Point(3, 55);
            this.cmb_menu.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.cmb_menu.Name = "cmb_menu";
            this.cmb_menu.Size = new System.Drawing.Size(260, 26);
            this.cmb_menu.TabIndex = 2;
            this.cmb_menu.SelectionChangeCommitted += new System.EventHandler(this.cmb_menu_SelectionChangeCommitted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(269, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(297, 35);
            this.label5.TabIndex = 10;
            this.label5.Text = "Description";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtdesc
            // 
            this.txtdesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtdesc.Location = new System.Drawing.Point(269, 38);
            this.txtdesc.Multiline = true;
            this.txtdesc.Name = "txtdesc";
            this.txtdesc.Size = new System.Drawing.Size(297, 54);
            this.txtdesc.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(572, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(227, 35);
            this.label4.TabIndex = 8;
            this.label4.Text = "Active";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chb_active
            // 
            this.chb_active.AutoSize = true;
            this.chb_active.Checked = true;
            this.chb_active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_active.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chb_active.Location = new System.Drawing.Point(679, 55);
            this.chb_active.Margin = new System.Windows.Forms.Padding(110, 20, 3, 3);
            this.chb_active.Name = "chb_active";
            this.chb_active.Size = new System.Drawing.Size(15, 14);
            this.chb_active.TabIndex = 9;
            this.chb_active.UseVisualStyleBackColor = true;
            // 
            // dgv_registartion
            // 
            this.dgv_registartion.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_registartion.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_registartion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_registartion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.slno,
            this.cmb_item,
            this.Qty,
            this.cmb_unit,
            this.action,
            this.Ing_DetailsId});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_registartion.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_registartion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_registartion.Location = new System.Drawing.Point(3, 104);
            this.dgv_registartion.Name = "dgv_registartion";
            this.dgv_registartion.RowHeadersVisible = false;
            this.dgv_registartion.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_registartion.RowTemplate.Height = 44;
            this.dgv_registartion.Size = new System.Drawing.Size(802, 250);
            this.dgv_registartion.TabIndex = 13;
            this.dgv_registartion.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_registartion_CellContentClick);
            this.dgv_registartion.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_registartion_CellFormatting);
            // 
            // slno
            // 
            this.slno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.slno.HeaderText = "Slno";
            this.slno.Name = "slno";
            this.slno.Width = 66;
            // 
            // cmb_item
            // 
            this.cmb_item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmb_item.HeaderText = "Item";
            this.cmb_item.Name = "cmb_item";
            // 
            // Qty
            // 
            this.Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Qty.HeaderText = "Quantity";
            this.Qty.Name = "Qty";
            // 
            // cmb_unit
            // 
            this.cmb_unit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmb_unit.HeaderText = "cmb_unit";
            this.cmb_unit.Name = "cmb_unit";
            // 
            // action
            // 
            this.action.HeaderText = "Action";
            this.action.MinimumWidth = 2;
            this.action.Name = "action";
            // 
            // Ing_DetailsId
            // 
            dataGridViewCellStyle2.NullValue = "0";
            this.Ing_DetailsId.DefaultCellStyle = dataGridViewCellStyle2;
            this.Ing_DetailsId.HeaderText = "Ing_DetailsId";
            this.Ing_DetailsId.Name = "Ing_DetailsId";
            this.Ing_DetailsId.Visible = false;
            // 
            // tlp_button
            // 
            this.tlp_button.ColumnCount = 4;
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.Controls.Add(this.btnSearch, 0, 0);
            this.tlp_button.Controls.Add(this.btnsave, 0, 0);
            this.tlp_button.Controls.Add(this.btnrefresh, 1, 0);
            this.tlp_button.Controls.Add(this.btnclose, 2, 0);
            this.tlp_button.Location = new System.Drawing.Point(3, 400);
            this.tlp_button.Name = "tlp_button";
            this.tlp_button.RowCount = 1;
            this.tlp_button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_button.Size = new System.Drawing.Size(326, 45);
            this.tlp_button.TabIndex = 13;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Silver;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(83, 4);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(73, 37);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "search";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.Color.Transparent;
            this.btnsave.BackgroundImage = global::FoodandBeverage.Properties.Resources._25512_4_save_button_clipart;
            this.btnsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnsave.FlatAppearance.BorderSize = 0;
            this.btnsave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnsave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnsave.Location = new System.Drawing.Point(3, 3);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(73, 39);
            this.btnsave.TabIndex = 0;
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnrefresh
            // 
            this.btnrefresh.BackColor = System.Drawing.Color.Silver;
            this.btnrefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrefresh.FlatAppearance.BorderSize = 0;
            this.btnrefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrefresh.Location = new System.Drawing.Point(163, 3);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(73, 39);
            this.btnrefresh.TabIndex = 2;
            this.btnrefresh.Text = "refresh";
            this.btnrefresh.UseVisualStyleBackColor = false;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.Silver;
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclose.FlatAppearance.BorderSize = 0;
            this.btnclose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnclose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.Location = new System.Drawing.Point(242, 3);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(73, 39);
            this.btnclose.TabIndex = 3;
            this.btnclose.Text = "close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // txtincrid
            // 
            this.txtincrid.Location = new System.Drawing.Point(842, 400);
            this.txtincrid.Name = "txtincrid";
            this.txtincrid.Size = new System.Drawing.Size(2, 20);
            this.txtincrid.TabIndex = 16;
            this.txtincrid.Visible = false;
            // 
            // tlp_main
            // 
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tlp_navbar, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_registartion_toggle, 0, 1);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 2;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.411765F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.58823F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_main.Size = new System.Drawing.Size(853, 540);
            this.tlp_main.TabIndex = 18;
            // 
            // FrmIncrediant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 540);
            this.Controls.Add(this.tlp_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmIncrediant";
            this.Load += new System.EventHandler(this.FrmIncrediant_Load);
            this.tlp_navbar.ResumeLayout(false);
            this.tlp_registartion_toggle.ResumeLayout(false);
            this.tlp_registartion_toggle.PerformLayout();
            this.tlp_outer_design.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tlp_registration.ResumeLayout(false);
            this.tlp_registration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_registartion)).EndInit();
            this.tlp_button.ResumeLayout(false);
            this.tlp_main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlp_navbar;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.TableLayoutPanel tlp_registartion_toggle;
        private System.Windows.Forms.TableLayoutPanel tlp_outer_design;
        private System.Windows.Forms.TableLayoutPanel tlp_registration;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtdesc;
        private System.Windows.Forms.ComboBox cmb_menu;
        private System.Windows.Forms.CheckBox chb_active;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tlp_button;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.TextBox txtincrid;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgv_registartion;
        private System.Windows.Forms.DataGridViewTextBoxColumn slno;
        private System.Windows.Forms.DataGridViewComboBoxColumn cmb_item;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewComboBoxColumn cmb_unit;
        private System.Windows.Forms.DataGridViewImageColumn action;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ing_DetailsId;
    }
}