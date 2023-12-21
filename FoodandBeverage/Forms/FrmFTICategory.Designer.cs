
namespace FoodandBeverage.Forms
{
    partial class FrmFTICategory
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
            this.dgv_category = new System.Windows.Forms.DataGridView();
            this.Slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.catename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlp_registartion = new System.Windows.Forms.TableLayoutPanel();
            this.label29 = new System.Windows.Forms.Label();
            this.chk_active = new System.Windows.Forms.CheckBox();
            this.txtFcid = new System.Windows.Forms.TextBox();
            this.txtfoodCate = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdescr = new System.Windows.Forms.TextBox();
            this.cmb_cate = new System.Windows.Forms.ComboBox();
            this.btn_del_cate = new System.Windows.Forms.Button();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_regis_out_design = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_navbar = new System.Windows.Forms.TableLayoutPanel();
            this.btnmini = new System.Windows.Forms.Button();
            this.btncolse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tlp_button = new System.Windows.Forms.TableLayoutPanel();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnrefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_category)).BeginInit();
            this.tlp_registartion.SuspendLayout();
            this.tlp_main.SuspendLayout();
            this.tlp_regis_out_design.SuspendLayout();
            this.tlp_navbar.SuspendLayout();
            this.tlp_button.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_category
            // 
            this.dgv_category.AllowUserToAddRows = false;
            this.dgv_category.AllowUserToDeleteRows = false;
            this.dgv_category.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_category.BackgroundColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_category.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_category.ColumnHeadersHeight = 46;
            this.dgv_category.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Slno,
            this.Id,
            this.catename,
            this.Description,
            this.Status,
            this.Action});
            this.dgv_category.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_category.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_category.Location = new System.Drawing.Point(6, 259);
            this.dgv_category.Name = "dgv_category";
            this.dgv_category.ReadOnly = true;
            this.dgv_category.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dgv_category.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_category.RowTemplate.Height = 30;
            this.dgv_category.Size = new System.Drawing.Size(716, 393);
            this.dgv_category.TabIndex = 1;
            this.dgv_category.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_category_CellDoubleClick);
            // 
            // Slno
            // 
            this.Slno.HeaderText = "Slno";
            this.Slno.Name = "Slno";
            this.Slno.ReadOnly = true;
            // 
            // Id
            // 
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "0";
            this.Id.DefaultCellStyle = dataGridViewCellStyle2;
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // catename
            // 
            this.catename.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.catename.HeaderText = "Food Category";
            this.catename.Name = "catename";
            this.catename.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Visible = false;
            this.Status.Width = 62;
            // 
            // Action
            // 
            this.Action.HeaderText = "Action";
            this.Action.Name = "Action";
            this.Action.ReadOnly = true;
            // 
            // tlp_registartion
            // 
            this.tlp_registartion.BackColor = System.Drawing.Color.White;
            this.tlp_registartion.ColumnCount = 5;
            this.tlp_registartion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlp_registartion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.48837F));
            this.tlp_registartion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.51163F));
            this.tlp_registartion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 213F));
            this.tlp_registartion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tlp_registartion.Controls.Add(this.label29, 1, 0);
            this.tlp_registartion.Controls.Add(this.chk_active, 0, 0);
            this.tlp_registartion.Controls.Add(this.txtFcid, 4, 0);
            this.tlp_registartion.Controls.Add(this.txtfoodCate, 1, 1);
            this.tlp_registartion.Controls.Add(this.label27, 3, 0);
            this.tlp_registartion.Controls.Add(this.label1, 2, 0);
            this.tlp_registartion.Controls.Add(this.txtdescr, 3, 1);
            this.tlp_registartion.Controls.Add(this.cmb_cate, 2, 1);
            this.tlp_registartion.Controls.Add(this.btn_del_cate, 4, 1);
            this.tlp_registartion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_registartion.Location = new System.Drawing.Point(16, 3);
            this.tlp_registartion.Name = "tlp_registartion";
            this.tlp_registartion.RowCount = 3;
            this.tlp_registartion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.85185F));
            this.tlp_registartion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 68.14815F));
            this.tlp_registartion.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_registartion.Size = new System.Drawing.Size(682, 137);
            this.tlp_registartion.TabIndex = 0;
            this.tlp_registartion.Paint += new System.Windows.Forms.PaintEventHandler(this.tlp_registartion_Paint);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label29.ForeColor = System.Drawing.Color.Black;
            this.label29.Location = new System.Drawing.Point(30, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(175, 37);
            this.label29.TabIndex = 231;
            this.label29.Text = "Category ";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chk_active
            // 
            this.chk_active.AutoSize = true;
            this.chk_active.Checked = true;
            this.chk_active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_active.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_active.Location = new System.Drawing.Point(3, 3);
            this.chk_active.Name = "chk_active";
            this.chk_active.Size = new System.Drawing.Size(21, 22);
            this.chk_active.TabIndex = 2;
            this.chk_active.Text = "Active";
            this.chk_active.UseVisualStyleBackColor = true;
            this.chk_active.Visible = false;
            // 
            // txtFcid
            // 
            this.txtFcid.Location = new System.Drawing.Point(581, 3);
            this.txtFcid.Name = "txtFcid";
            this.txtFcid.Size = new System.Drawing.Size(28, 20);
            this.txtFcid.TabIndex = 251;
            this.txtFcid.Visible = false;
            // 
            // txtfoodCate
            // 
            this.txtfoodCate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtfoodCate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtfoodCate.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtfoodCate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfoodCate.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtfoodCate.Location = new System.Drawing.Point(30, 40);
            this.txtfoodCate.Multiline = true;
            this.txtfoodCate.Name = "txtfoodCate";
            this.txtfoodCate.Size = new System.Drawing.Size(175, 28);
            this.txtfoodCate.TabIndex = 0;
            this.txtfoodCate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtfoodCate_KeyDown);
            this.txtfoodCate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtfoodCate_KeyPress);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(368, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(207, 37);
            this.label27.TabIndex = 221;
            this.label27.Text = "Description ";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(211, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 37);
            this.label1.TabIndex = 252;
            this.label1.Text = "Category Type";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtdescr
            // 
            this.txtdescr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtdescr.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtdescr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtdescr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdescr.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtdescr.Location = new System.Drawing.Point(368, 40);
            this.txtdescr.Multiline = true;
            this.txtdescr.Name = "txtdescr";
            this.txtdescr.Size = new System.Drawing.Size(207, 73);
            this.txtdescr.TabIndex = 2;
            // 
            // cmb_cate
            // 
            this.cmb_cate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.cmb_cate.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmb_cate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_cate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_cate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.cmb_cate.FormattingEnabled = true;
            this.cmb_cate.Items.AddRange(new object[] {
            "Food Category",
            "Table Category",
            "Item Category"});
            this.cmb_cate.Location = new System.Drawing.Point(211, 40);
            this.cmb_cate.Name = "cmb_cate";
            this.cmb_cate.Size = new System.Drawing.Size(151, 28);
            this.cmb_cate.TabIndex = 1;
            this.cmb_cate.SelectedValueChanged += new System.EventHandler(this.cmb_cate_SelectedValueChanged);
            this.cmb_cate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_cate_KeyDown);
            // 
            // btn_del_cate
            // 
            this.btn_del_cate.BackgroundImage = global::FoodandBeverage.Properties.Resources.delete;
            this.btn_del_cate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_del_cate.FlatAppearance.BorderSize = 0;
            this.btn_del_cate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_del_cate.Location = new System.Drawing.Point(604, 40);
            this.btn_del_cate.Margin = new System.Windows.Forms.Padding(26, 3, 3, 3);
            this.btn_del_cate.Name = "btn_del_cate";
            this.btn_del_cate.Size = new System.Drawing.Size(48, 47);
            this.btn_del_cate.TabIndex = 3;
            this.btn_del_cate.UseVisualStyleBackColor = true;
            this.btn_del_cate.Visible = false;
            this.btn_del_cate.Click += new System.EventHandler(this.btn_del_cate_Click);
            // 
            // tlp_main
            // 
            this.tlp_main.BackColor = System.Drawing.Color.AliceBlue;
            this.tlp_main.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tlp_regis_out_design, 0, 1);
            this.tlp_main.Controls.Add(this.tlp_navbar, 0, 0);
            this.tlp_main.Controls.Add(this.dgv_category, 0, 3);
            this.tlp_main.Controls.Add(this.tlp_button, 0, 2);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 4;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 164F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 190F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_main.Size = new System.Drawing.Size(728, 658);
            this.tlp_main.TabIndex = 3;
            // 
            // tlp_regis_out_design
            // 
            this.tlp_regis_out_design.ColumnCount = 3;
            this.tlp_regis_out_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tlp_regis_out_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 98F));
            this.tlp_regis_out_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tlp_regis_out_design.Controls.Add(this.tlp_registartion, 1, 0);
            this.tlp_regis_out_design.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_regis_out_design.Location = new System.Drawing.Point(6, 47);
            this.tlp_regis_out_design.Name = "tlp_regis_out_design";
            this.tlp_regis_out_design.RowCount = 2;
            this.tlp_regis_out_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_regis_out_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlp_regis_out_design.Size = new System.Drawing.Size(716, 158);
            this.tlp_regis_out_design.TabIndex = 254;
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
            this.tlp_navbar.Controls.Add(this.label2, 0, 0);
            this.tlp_navbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_navbar.Location = new System.Drawing.Point(6, 6);
            this.tlp_navbar.Name = "tlp_navbar";
            this.tlp_navbar.RowCount = 1;
            this.tlp_navbar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tlp_navbar.Size = new System.Drawing.Size(716, 32);
            this.tlp_navbar.TabIndex = 16;
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(657, 3);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(25, 25);
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
            this.btncolse.Location = new System.Drawing.Point(688, 3);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(25, 25);
            this.btncolse.TabIndex = 10;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "Category";
            // 
            // tlp_button
            // 
            this.tlp_button.ColumnCount = 4;
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tlp_button.Controls.Add(this.btnsave, 0, 0);
            this.tlp_button.Controls.Add(this.btnSearch, 2, 0);
            this.tlp_button.Controls.Add(this.btnclose, 2, 0);
            this.tlp_button.Controls.Add(this.btnrefresh, 1, 0);
            this.tlp_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.tlp_button.Location = new System.Drawing.Point(307, 214);
            this.tlp_button.Name = "tlp_button";
            this.tlp_button.RowCount = 1;
            this.tlp_button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_button.Size = new System.Drawing.Size(415, 36);
            this.tlp_button.TabIndex = 0;
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.Color.Transparent;
            this.btnsave.BackgroundImage = global::FoodandBeverage.Properties.Resources.SaveButton;
            this.btnsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnsave.FlatAppearance.BorderSize = 0;
            this.btnsave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnsave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnsave.Location = new System.Drawing.Point(3, 3);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(95, 28);
            this.btnsave.TabIndex = 0;
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImage = global::FoodandBeverage.Properties.Resources.searchbutton;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(206, 4);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(95, 26);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.Transparent;
            this.btnclose.BackgroundImage = global::FoodandBeverage.Properties.Resources.Closebutton;
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclose.FlatAppearance.BorderSize = 0;
            this.btnclose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnclose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.Location = new System.Drawing.Point(308, 3);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(95, 28);
            this.btnclose.TabIndex = 3;
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnrefresh
            // 
            this.btnrefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.BackgroundImage = global::FoodandBeverage.Properties.Resources.refresh_Button1;
            this.btnrefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrefresh.FlatAppearance.BorderSize = 0;
            this.btnrefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrefresh.Location = new System.Drawing.Point(104, 3);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(95, 28);
            this.btnrefresh.TabIndex = 1;
            this.btnrefresh.UseVisualStyleBackColor = false;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // FrmFTICategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 658);
            this.Controls.Add(this.tlp_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmFTICategory";
            this.Text = "FrmFoodCategory";
            this.Load += new System.EventHandler(this.FrmFTICategory_Load);
            this.Resize += new System.EventHandler(this.FrmFTICategory_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_category)).EndInit();
            this.tlp_registartion.ResumeLayout(false);
            this.tlp_registartion.PerformLayout();
            this.tlp_main.ResumeLayout(false);
            this.tlp_regis_out_design.ResumeLayout(false);
            this.tlp_navbar.ResumeLayout(false);
            this.tlp_navbar.PerformLayout();
            this.tlp_button.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_category;
        private System.Windows.Forms.TableLayoutPanel tlp_registartion;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtfoodCate;
        private System.Windows.Forms.TextBox txtdescr;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TableLayoutPanel tlp_button;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.TableLayoutPanel tlp_navbar;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_cate;
        private System.Windows.Forms.CheckBox chk_active;
        private System.Windows.Forms.TextBox txtFcid;
        private System.Windows.Forms.TableLayoutPanel tlp_regis_out_design;
        private System.Windows.Forms.Button btn_del_cate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Slno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn catename;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Action;
        private System.Windows.Forms.Label label2;
    }
}