
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFTICategory));
            this.dgv_category = new System.Windows.Forms.DataGridView();
            this.Slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.catename = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CATColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlp_registartion = new System.Windows.Forms.TableLayoutPanel();
            this.txtcolorname = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.chk_active = new System.Windows.Forms.CheckBox();
            this.txtFcid = new System.Windows.Forms.TextBox();
            this.txtfoodCate = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdescr = new System.Windows.Forms.TextBox();
            this.cmb_cate = new System.Windows.Forms.ComboBox();
            this.btn_del_cate = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pbcolorpicker = new System.Windows.Forms.PictureBox();
            this.btnresetcolor = new System.Windows.Forms.Button();
            this.lblcolor = new System.Windows.Forms.Label();
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
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbcolorpicker)).BeginInit();
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
            this.dgv_category.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
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
            this.Action,
            this.CATColor});
            this.dgv_category.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_category.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_category.Location = new System.Drawing.Point(11, 311);
            this.dgv_category.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgv_category.Name = "dgv_category";
            this.dgv_category.ReadOnly = true;
            this.dgv_category.RowHeadersVisible = false;
            this.dgv_category.RowHeadersWidth = 51;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dgv_category.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_category.RowTemplate.Height = 30;
            this.dgv_category.Size = new System.Drawing.Size(949, 489);
            this.dgv_category.TabIndex = 1;
            this.dgv_category.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_category_CellDoubleClick);
            // 
            // Slno
            // 
            this.Slno.HeaderText = "Slno";
            this.Slno.MinimumWidth = 6;
            this.Slno.Name = "Slno";
            this.Slno.ReadOnly = true;
            // 
            // Id
            // 
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "0";
            this.Id.DefaultCellStyle = dataGridViewCellStyle2;
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // catename
            // 
            this.catename.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.catename.HeaderText = "Category";
            this.catename.MinimumWidth = 6;
            this.catename.Name = "catename";
            this.catename.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.HeaderText = "Description";
            this.Description.MinimumWidth = 6;
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Visible = false;
            this.Status.Width = 74;
            // 
            // Action
            // 
            this.Action.HeaderText = "Action";
            this.Action.MinimumWidth = 6;
            this.Action.Name = "Action";
            this.Action.ReadOnly = true;
            this.Action.Visible = false;
            // 
            // CATColor
            // 
            this.CATColor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.SteelBlue;
            this.CATColor.DefaultCellStyle = dataGridViewCellStyle3;
            this.CATColor.HeaderText = "Button Color";
            this.CATColor.MinimumWidth = 6;
            this.CATColor.Name = "CATColor";
            this.CATColor.ReadOnly = true;
            this.CATColor.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CATColor.Visible = false;
            this.CATColor.Width = 149;
            // 
            // tlp_registartion
            // 
            this.tlp_registartion.BackColor = System.Drawing.Color.White;
            this.tlp_registartion.ColumnCount = 5;
            this.tlp_registartion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlp_registartion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.48837F));
            this.tlp_registartion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.51163F));
            this.tlp_registartion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 284F));
            this.tlp_registartion.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 164F));
            this.tlp_registartion.Controls.Add(this.txtcolorname, 0, 3);
            this.tlp_registartion.Controls.Add(this.label29, 1, 0);
            this.tlp_registartion.Controls.Add(this.chk_active, 0, 0);
            this.tlp_registartion.Controls.Add(this.txtFcid, 4, 0);
            this.tlp_registartion.Controls.Add(this.txtfoodCate, 1, 1);
            this.tlp_registartion.Controls.Add(this.label27, 3, 0);
            this.tlp_registartion.Controls.Add(this.label1, 2, 0);
            this.tlp_registartion.Controls.Add(this.txtdescr, 3, 1);
            this.tlp_registartion.Controls.Add(this.cmb_cate, 2, 1);
            this.tlp_registartion.Controls.Add(this.btn_del_cate, 4, 1);
            this.tlp_registartion.Controls.Add(this.tableLayoutPanel1, 1, 3);
            this.tlp_registartion.Controls.Add(this.lblcolor, 1, 2);
            this.tlp_registartion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_registartion.Location = new System.Drawing.Point(4, 4);
            this.tlp_registartion.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_registartion.Name = "tlp_registartion";
            this.tlp_registartion.RowCount = 4;
            this.tlp_registartion.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_registartion.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_registartion.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_registartion.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_registartion.Size = new System.Drawing.Size(941, 168);
            this.tlp_registartion.TabIndex = 0;
            this.tlp_registartion.Paint += new System.Windows.Forms.PaintEventHandler(this.tlp_registartion_Paint);
            // 
            // txtcolorname
            // 
            this.txtcolorname.Location = new System.Drawing.Point(4, 121);
            this.txtcolorname.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtcolorname.Name = "txtcolorname";
            this.txtcolorname.Size = new System.Drawing.Size(27, 22);
            this.txtcolorname.TabIndex = 256;
            this.txtcolorname.Visible = false;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label29.ForeColor = System.Drawing.Color.Black;
            this.label29.Location = new System.Drawing.Point(40, 0);
            this.label29.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(236, 36);
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
            this.chk_active.Location = new System.Drawing.Point(4, 4);
            this.chk_active.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chk_active.Name = "chk_active";
            this.chk_active.Size = new System.Drawing.Size(28, 28);
            this.chk_active.TabIndex = 2;
            this.chk_active.Text = "Active";
            this.chk_active.UseVisualStyleBackColor = true;
            this.chk_active.Visible = false;
            // 
            // txtFcid
            // 
            this.txtFcid.Location = new System.Drawing.Point(780, 4);
            this.txtFcid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFcid.Name = "txtFcid";
            this.txtFcid.Size = new System.Drawing.Size(36, 22);
            this.txtFcid.TabIndex = 251;
            this.txtFcid.Visible = false;
            // 
            // txtfoodCate
            // 
            this.txtfoodCate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.txtfoodCate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtfoodCate.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtfoodCate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtfoodCate.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtfoodCate.Location = new System.Drawing.Point(40, 40);
            this.txtfoodCate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtfoodCate.Multiline = true;
            this.txtfoodCate.Name = "txtfoodCate";
            this.txtfoodCate.Size = new System.Drawing.Size(236, 34);
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
            this.label27.Location = new System.Drawing.Point(496, 0);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(276, 36);
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
            this.label1.Location = new System.Drawing.Point(284, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 36);
            this.label1.TabIndex = 252;
            this.label1.Text = "Category Type";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtdescr
            // 
            this.txtdescr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.txtdescr.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtdescr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtdescr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdescr.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtdescr.Location = new System.Drawing.Point(496, 40);
            this.txtdescr.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtdescr.Multiline = true;
            this.txtdescr.Name = "txtdescr";
            this.txtdescr.Size = new System.Drawing.Size(276, 53);
            this.txtdescr.TabIndex = 2;
            // 
            // cmb_cate
            // 
            this.cmb_cate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.cmb_cate.Dock = System.Windows.Forms.DockStyle.Top;
            this.cmb_cate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_cate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_cate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.cmb_cate.FormattingEnabled = true;
            this.cmb_cate.Items.AddRange(new object[] {
            "Food Category",
            "Table Category",
            "Item Category",
            "Expence Reasons",
            "Addon Category"});
            this.cmb_cate.Location = new System.Drawing.Point(284, 40);
            this.cmb_cate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmb_cate.Name = "cmb_cate";
            this.cmb_cate.Size = new System.Drawing.Size(204, 33);
            this.cmb_cate.TabIndex = 1;
            this.cmb_cate.SelectedValueChanged += new System.EventHandler(this.cmb_cate_SelectedValueChanged);
            this.cmb_cate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmb_cate_KeyDown);
            // 
            // btn_del_cate
            // 
            this.btn_del_cate.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_TRASH;
            this.btn_del_cate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_del_cate.FlatAppearance.BorderSize = 0;
            this.btn_del_cate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_del_cate.Location = new System.Drawing.Point(811, 40);
            this.btn_del_cate.Margin = new System.Windows.Forms.Padding(35, 4, 4, 4);
            this.btn_del_cate.Name = "btn_del_cate";
            this.btn_del_cate.Size = new System.Drawing.Size(64, 53);
            this.btn_del_cate.TabIndex = 3;
            this.btn_del_cate.UseVisualStyleBackColor = true;
            this.btn_del_cate.Visible = false;
            this.btn_del_cate.Click += new System.EventHandler(this.btn_del_cate_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 66.07143F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.92857F));
            this.tableLayoutPanel1.Controls.Add(this.pbcolorpicker, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnresetcolor, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(40, 121);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(236, 46);
            this.tableLayoutPanel1.TabIndex = 257;
            // 
            // pbcolorpicker
            // 
            this.pbcolorpicker.BackColor = System.Drawing.Color.Transparent;
            this.pbcolorpicker.BackgroundImage = global::FoodandBeverage.Properties.Resources.colourpick_icon;
            this.pbcolorpicker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbcolorpicker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbcolorpicker.Location = new System.Drawing.Point(4, 4);
            this.pbcolorpicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbcolorpicker.Name = "pbcolorpicker";
            this.pbcolorpicker.Size = new System.Drawing.Size(147, 38);
            this.pbcolorpicker.TabIndex = 254;
            this.pbcolorpicker.TabStop = false;
            this.pbcolorpicker.Visible = false;
            this.pbcolorpicker.Click += new System.EventHandler(this.pbcolorpicker_Click);
            // 
            // btnresetcolor
            // 
            this.btnresetcolor.BackColor = System.Drawing.Color.Transparent;
            this.btnresetcolor.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnresetcolor.BackgroundImage")));
            this.btnresetcolor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnresetcolor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnresetcolor.FlatAppearance.BorderSize = 0;
            this.btnresetcolor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnresetcolor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnresetcolor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnresetcolor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnresetcolor.Location = new System.Drawing.Point(159, 4);
            this.btnresetcolor.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnresetcolor.Name = "btnresetcolor";
            this.btnresetcolor.Size = new System.Drawing.Size(73, 38);
            this.btnresetcolor.TabIndex = 255;
            this.btnresetcolor.UseVisualStyleBackColor = false;
            this.btnresetcolor.Visible = false;
            this.btnresetcolor.Click += new System.EventHandler(this.btnresetcolor_Click);
            // 
            // lblcolor
            // 
            this.lblcolor.AutoSize = true;
            this.lblcolor.BackColor = System.Drawing.Color.Transparent;
            this.lblcolor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcolor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.lblcolor.ForeColor = System.Drawing.Color.Black;
            this.lblcolor.Location = new System.Drawing.Point(40, 97);
            this.lblcolor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblcolor.Name = "lblcolor";
            this.lblcolor.Size = new System.Drawing.Size(236, 20);
            this.lblcolor.TabIndex = 253;
            this.lblcolor.Text = "Color";
            this.lblcolor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblcolor.Visible = false;
            // 
            // tlp_main
            // 
            this.tlp_main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tlp_regis_out_design, 0, 1);
            this.tlp_main.Controls.Add(this.tlp_navbar, 0, 0);
            this.tlp_main.Controls.Add(this.dgv_category, 0, 3);
            this.tlp_main.Controls.Add(this.tlp_button, 0, 2);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tlp_main.RowCount = 4;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 202F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 234F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlp_main.Size = new System.Drawing.Size(971, 810);
            this.tlp_main.TabIndex = 3;
            // 
            // tlp_regis_out_design
            // 
            this.tlp_regis_out_design.ColumnCount = 1;
            this.tlp_regis_out_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_regis_out_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tlp_regis_out_design.Controls.Add(this.tlp_registartion, 0, 0);
            this.tlp_regis_out_design.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_regis_out_design.Location = new System.Drawing.Point(11, 57);
            this.tlp_regis_out_design.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_regis_out_design.Name = "tlp_regis_out_design";
            this.tlp_regis_out_design.RowCount = 2;
            this.tlp_regis_out_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_regis_out_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 18F));
            this.tlp_regis_out_design.Size = new System.Drawing.Size(949, 194);
            this.tlp_regis_out_design.TabIndex = 254;
            // 
            // tlp_navbar
            // 
            this.tlp_navbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(80)))), ((int)(((byte)(185)))));
            this.tlp_navbar.ColumnCount = 3;
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_navbar.Controls.Add(this.btnmini, 1, 0);
            this.tlp_navbar.Controls.Add(this.btncolse, 2, 0);
            this.tlp_navbar.Controls.Add(this.label2, 0, 0);
            this.tlp_navbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_navbar.Location = new System.Drawing.Point(11, 10);
            this.tlp_navbar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_navbar.Name = "tlp_navbar";
            this.tlp_navbar.RowCount = 1;
            this.tlp_navbar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tlp_navbar.Size = new System.Drawing.Size(949, 39);
            this.tlp_navbar.TabIndex = 16;
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(871, 4);
            this.btnmini.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(33, 31);
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
            this.btncolse.Location = new System.Drawing.Point(912, 4);
            this.btncolse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(33, 31);
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
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(118, 29);
            this.label2.TabIndex = 11;
            this.label2.Text = "Category";
            // 
            // tlp_button
            // 
            this.tlp_button.ColumnCount = 4;
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tlp_button.Controls.Add(this.btnsave, 0, 0);
            this.tlp_button.Controls.Add(this.btnSearch, 2, 0);
            this.tlp_button.Controls.Add(this.btnclose, 2, 0);
            this.tlp_button.Controls.Add(this.btnrefresh, 1, 0);
            this.tlp_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.tlp_button.Location = new System.Drawing.Point(407, 259);
            this.tlp_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_button.Name = "tlp_button";
            this.tlp_button.RowCount = 1;
            this.tlp_button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_button.Size = new System.Drawing.Size(553, 44);
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
            this.btnsave.Location = new System.Drawing.Point(4, 4);
            this.btnsave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(127, 34);
            this.btnsave.TabIndex = 0;
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_SEARCH;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(275, 5);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(127, 32);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.Transparent;
            this.btnclose.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_CANCEL;
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclose.FlatAppearance.BorderSize = 0;
            this.btnclose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnclose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.Location = new System.Drawing.Point(411, 4);
            this.btnclose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(127, 34);
            this.btnclose.TabIndex = 3;
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnrefresh
            // 
            this.btnrefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_REFESH;
            this.btnrefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrefresh.FlatAppearance.BorderSize = 0;
            this.btnrefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrefresh.Location = new System.Drawing.Point(139, 4);
            this.btnrefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(127, 34);
            this.btnrefresh.TabIndex = 1;
            this.btnrefresh.UseVisualStyleBackColor = false;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // FrmFTICategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(971, 810);
            this.Controls.Add(this.tlp_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmFTICategory";
            this.Tag = "3";
            this.Text = "FrmFoodCategory";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmFTICategory_FormClosing);
            this.Load += new System.EventHandler(this.FrmFTICategory_Load);
            this.Resize += new System.EventHandler(this.FrmFTICategory_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_category)).EndInit();
            this.tlp_registartion.ResumeLayout(false);
            this.tlp_registartion.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbcolorpicker)).EndInit();
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblcolor;
        private System.Windows.Forms.PictureBox pbcolorpicker;
        private System.Windows.Forms.Button btnresetcolor;
        private System.Windows.Forms.TextBox txtcolorname;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Slno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn catename;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Action;
        private System.Windows.Forms.DataGridViewTextBoxColumn CATColor;
    }
}