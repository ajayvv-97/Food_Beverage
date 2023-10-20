
namespace FoodandBeverage.Forms
{
    partial class FrmMenu
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
            this.txtdesc = new System.Windows.Forms.TextBox();
            this.cmbMenu_category = new System.Windows.Forms.ComboBox();
            this.txtmid = new System.Windows.Forms.TextBox();
            this.tlp_registration = new System.Windows.Forms.TableLayoutPanel();
            this.btnseach_menu = new System.Windows.Forms.Button();
            this.tlp_button = new System.Windows.Forms.TableLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.dgv_menu = new System.Windows.Forms.DataGridView();
            this.no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Productid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Usertype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menucategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fcid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_registartion_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_outer_design = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_viewmenu_gal = new System.Windows.Forms.Panel();
            this.Btn_view_menu = new System.Windows.Forms.Button();
            this.tlp_regform_dgv = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_navbar = new System.Windows.Forms.TableLayoutPanel();
            this.btnmini = new System.Windows.Forms.Button();
            this.btncolse = new System.Windows.Forms.Button();
            this.tlp_menu_Gallery_main = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_bck_reg = new System.Windows.Forms.Button();
            this.tlp_search_box = new System.Windows.Forms.TableLayoutPanel();
            this.Search = new System.Windows.Forms.Button();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.cmbSearchlist = new MetroFramework.Controls.MetroComboBox();
            this.tlp_registration.SuspendLayout();
            this.tlp_button.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_menu)).BeginInit();
            this.tlp_main.SuspendLayout();
            this.tlp_registartion_main.SuspendLayout();
            this.tlp_outer_design.SuspendLayout();
            this.pnl_viewmenu_gal.SuspendLayout();
            this.tlp_regform_dgv.SuspendLayout();
            this.tlp_navbar.SuspendLayout();
            this.tlp_menu_Gallery_main.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tlp_search_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtdesc
            // 
            this.txtdesc.Location = new System.Drawing.Point(366, 3);
            this.txtdesc.Name = "txtdesc";
            this.txtdesc.Size = new System.Drawing.Size(211, 20);
            this.txtdesc.TabIndex = 1;
            // 
            // cmbMenu_category
            // 
            this.cmbMenu_category.FormattingEnabled = true;
            this.cmbMenu_category.Location = new System.Drawing.Point(150, 3);
            this.cmbMenu_category.Name = "cmbMenu_category";
            this.cmbMenu_category.Size = new System.Drawing.Size(210, 21);
            this.cmbMenu_category.TabIndex = 2;
            this.cmbMenu_category.SelectionChangeCommitted += new System.EventHandler(this.cmbMenu_SelectionChangeCommitted);
            // 
            // txtmid
            // 
            this.txtmid.Location = new System.Drawing.Point(359, 27);
            this.txtmid.Name = "txtmid";
            this.txtmid.Size = new System.Drawing.Size(13, 20);
            this.txtmid.TabIndex = 3;
            // 
            // tlp_registration
            // 
            this.tlp_registration.BackColor = System.Drawing.Color.White;
            this.tlp_registration.ColumnCount = 6;
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 147F));
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 59F));
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tlp_registration.Controls.Add(this.txtdesc, 3, 0);
            this.tlp_registration.Controls.Add(this.cmbMenu_category, 2, 0);
            this.tlp_registration.Controls.Add(this.btnseach_menu, 4, 0);
            this.tlp_registration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_registration.Location = new System.Drawing.Point(3, 3);
            this.tlp_registration.Name = "tlp_registration";
            this.tlp_registration.RowCount = 1;
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_registration.Size = new System.Drawing.Size(673, 42);
            this.tlp_registration.TabIndex = 4;
            this.tlp_registration.Paint += new System.Windows.Forms.PaintEventHandler(this.tlp_registration_Paint);
            // 
            // btnseach_menu
            // 
            this.btnseach_menu.BackColor = System.Drawing.Color.Transparent;
            this.btnseach_menu.BackgroundImage = global::FoodandBeverage.Properties.Resources.search;
            this.btnseach_menu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnseach_menu.FlatAppearance.BorderSize = 0;
            this.btnseach_menu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnseach_menu.Location = new System.Drawing.Point(583, 3);
            this.btnseach_menu.Name = "btnseach_menu";
            this.btnseach_menu.Size = new System.Drawing.Size(53, 36);
            this.btnseach_menu.TabIndex = 3;
            this.btnseach_menu.UseVisualStyleBackColor = false;
            this.btnseach_menu.Click += new System.EventHandler(this.btnseach_menu_Click);
            // 
            // tlp_button
            // 
            this.tlp_button.ColumnCount = 4;
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tlp_button.Controls.Add(this.btnSearch, 0, 0);
            this.tlp_button.Controls.Add(this.btnsave, 0, 0);
            this.tlp_button.Controls.Add(this.btnrefresh, 1, 0);
            this.tlp_button.Controls.Add(this.btnclose, 2, 0);
            this.tlp_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.tlp_button.Location = new System.Drawing.Point(341, 402);
            this.tlp_button.Name = "tlp_button";
            this.tlp_button.RowCount = 1;
            this.tlp_button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_button.Size = new System.Drawing.Size(405, 41);
            this.tlp_button.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(105, 4);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(95, 30);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
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
            this.btnsave.Size = new System.Drawing.Size(95, 32);
            this.btnsave.TabIndex = 0;
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnrefresh
            // 
            this.btnrefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrefresh.FlatAppearance.BorderSize = 0;
            this.btnrefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrefresh.Location = new System.Drawing.Point(207, 3);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(95, 32);
            this.btnrefresh.TabIndex = 2;
            this.btnrefresh.Text = "refresh";
            this.btnrefresh.UseVisualStyleBackColor = false;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.Transparent;
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclose.FlatAppearance.BorderSize = 0;
            this.btnclose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnclose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.Location = new System.Drawing.Point(308, 3);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(95, 32);
            this.btnclose.TabIndex = 3;
            this.btnclose.Text = "close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // dgv_menu
            // 
            this.dgv_menu.AllowUserToAddRows = false;
            this.dgv_menu.AllowUserToDeleteRows = false;
            this.dgv_menu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_menu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_menu.ColumnHeadersHeight = 46;
            this.dgv_menu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no,
            this.mid,
            this.Productid,
            this.Usertype,
            this.menucategory,
            this.price,
            this.Status,
            this.fcid});
            this.dgv_menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_menu.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_menu.Location = new System.Drawing.Point(3, 51);
            this.dgv_menu.Name = "dgv_menu";
            this.dgv_menu.ReadOnly = true;
            this.dgv_menu.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dgv_menu.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_menu.RowTemplate.Height = 30;
            this.dgv_menu.Size = new System.Drawing.Size(673, 293);
            this.dgv_menu.TabIndex = 6;
            // 
            // no
            // 
            this.no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.no.HeaderText = "Slno";
            this.no.Name = "no";
            this.no.ReadOnly = true;
            this.no.Width = 66;
            // 
            // mid
            // 
            this.mid.HeaderText = "mid";
            this.mid.Name = "mid";
            this.mid.ReadOnly = true;
            this.mid.Visible = false;
            // 
            // Productid
            // 
            this.Productid.HeaderText = "Productid";
            this.Productid.Name = "Productid";
            this.Productid.ReadOnly = true;
            this.Productid.Visible = false;
            // 
            // Usertype
            // 
            this.Usertype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Usertype.HeaderText = "Food Name";
            this.Usertype.Name = "Usertype";
            this.Usertype.ReadOnly = true;
            // 
            // menucategory
            // 
            this.menucategory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.menucategory.HeaderText = "Category";
            this.menucategory.Name = "menucategory";
            this.menucategory.ReadOnly = true;
            // 
            // price
            // 
            this.price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.price.HeaderText = "Price";
            this.price.Name = "price";
            this.price.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 62;
            // 
            // fcid
            // 
            this.fcid.HeaderText = "fcid";
            this.fcid.Name = "fcid";
            this.fcid.ReadOnly = true;
            this.fcid.Visible = false;
            // 
            // tlp_main
            // 
            this.tlp_main.BackColor = System.Drawing.Color.AliceBlue;
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tlp_registartion_main, 0, 1);
            this.tlp_main.Controls.Add(this.tlp_navbar, 0, 0);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 2;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_main.Size = new System.Drawing.Size(755, 506);
            this.tlp_main.TabIndex = 7;
            // 
            // tlp_registartion_main
            // 
            this.tlp_registartion_main.ColumnCount = 1;
            this.tlp_registartion_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_registartion_main.Controls.Add(this.tlp_outer_design, 0, 0);
            this.tlp_registartion_main.Controls.Add(this.tlp_button, 0, 1);
            this.tlp_registartion_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_registartion_main.Location = new System.Drawing.Point(3, 57);
            this.tlp_registartion_main.Name = "tlp_registartion_main";
            this.tlp_registartion_main.RowCount = 2;
            this.tlp_registartion_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.6861F));
            this.tlp_registartion_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.3139F));
            this.tlp_registartion_main.Size = new System.Drawing.Size(749, 446);
            this.tlp_registartion_main.TabIndex = 6;
            // 
            // tlp_outer_design
            // 
            this.tlp_outer_design.BackColor = System.Drawing.Color.AliceBlue;
            this.tlp_outer_design.ColumnCount = 3;
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tlp_outer_design.Controls.Add(this.pnl_viewmenu_gal, 2, 1);
            this.tlp_outer_design.Controls.Add(this.tlp_regform_dgv, 1, 1);
            this.tlp_outer_design.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_outer_design.Location = new System.Drawing.Point(3, 3);
            this.tlp_outer_design.Name = "tlp_outer_design";
            this.tlp_outer_design.RowCount = 3;
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 111F));
            this.tlp_outer_design.Size = new System.Drawing.Size(743, 393);
            this.tlp_outer_design.TabIndex = 8;
            // 
            // pnl_viewmenu_gal
            // 
            this.pnl_viewmenu_gal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnl_viewmenu_gal.Controls.Add(this.Btn_view_menu);
            this.pnl_viewmenu_gal.Location = new System.Drawing.Point(702, 12);
            this.pnl_viewmenu_gal.Name = "pnl_viewmenu_gal";
            this.pnl_viewmenu_gal.Size = new System.Drawing.Size(33, 368);
            this.pnl_viewmenu_gal.TabIndex = 5;
            // 
            // Btn_view_menu
            // 
            this.Btn_view_menu.BackgroundImage = global::FoodandBeverage.Properties.Resources.gallery;
            this.Btn_view_menu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_view_menu.FlatAppearance.BorderSize = 0;
            this.Btn_view_menu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_view_menu.Location = new System.Drawing.Point(3, 96);
            this.Btn_view_menu.Name = "Btn_view_menu";
            this.Btn_view_menu.Size = new System.Drawing.Size(28, 41);
            this.Btn_view_menu.TabIndex = 0;
            this.Btn_view_menu.UseVisualStyleBackColor = true;
            this.Btn_view_menu.Click += new System.EventHandler(this.Btn_view_menu_Click);
            // 
            // tlp_regform_dgv
            // 
            this.tlp_regform_dgv.ColumnCount = 1;
            this.tlp_regform_dgv.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_regform_dgv.Controls.Add(this.dgv_menu, 0, 1);
            this.tlp_regform_dgv.Controls.Add(this.tlp_registration, 0, 0);
            this.tlp_regform_dgv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_regform_dgv.Location = new System.Drawing.Point(17, 12);
            this.tlp_regform_dgv.Name = "tlp_regform_dgv";
            this.tlp_regform_dgv.RowCount = 3;
            this.tlp_regform_dgv.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.8587F));
            this.tlp_regform_dgv.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.1413F));
            this.tlp_regform_dgv.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_regform_dgv.Size = new System.Drawing.Size(679, 368);
            this.tlp_regform_dgv.TabIndex = 6;
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
            this.tlp_navbar.Size = new System.Drawing.Size(749, 48);
            this.tlp_navbar.TabIndex = 17;
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(690, 3);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(25, 36);
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
            this.btncolse.Location = new System.Drawing.Point(721, 3);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(25, 36);
            this.btncolse.TabIndex = 10;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // tlp_menu_Gallery_main
            // 
            this.tlp_menu_Gallery_main.ColumnCount = 2;
            this.tlp_menu_Gallery_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 94.99284F));
            this.tlp_menu_Gallery_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 5.007153F));
            this.tlp_menu_Gallery_main.Controls.Add(this.panel2, 1, 1);
            this.tlp_menu_Gallery_main.Controls.Add(this.tlp_search_box, 0, 0);
            this.tlp_menu_Gallery_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_menu_Gallery_main.Name = "tlp_menu_Gallery_main";
            this.tlp_menu_Gallery_main.RowCount = 2;
            this.tlp_menu_Gallery_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_menu_Gallery_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_menu_Gallery_main.Size = new System.Drawing.Size(699, 72);
            this.tlp_menu_Gallery_main.TabIndex = 18;
            this.tlp_menu_Gallery_main.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_bck_reg);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(668, 44);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(28, 368);
            this.panel2.TabIndex = 0;
            // 
            // btn_bck_reg
            // 
            this.btn_bck_reg.BackgroundImage = global::FoodandBeverage.Properties.Resources.back;
            this.btn_bck_reg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_bck_reg.FlatAppearance.BorderSize = 0;
            this.btn_bck_reg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_bck_reg.Location = new System.Drawing.Point(3, 115);
            this.btn_bck_reg.Name = "btn_bck_reg";
            this.btn_bck_reg.Size = new System.Drawing.Size(26, 44);
            this.btn_bck_reg.TabIndex = 0;
            this.btn_bck_reg.UseVisualStyleBackColor = true;
            this.btn_bck_reg.Click += new System.EventHandler(this.btn_bck_reg_Click);
            // 
            // tlp_search_box
            // 
            this.tlp_search_box.ColumnCount = 3;
            this.tlp_search_box.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.09927F));
            this.tlp_search_box.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.90073F));
            this.tlp_search_box.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.tlp_search_box.Controls.Add(this.Search, 2, 0);
            this.tlp_search_box.Controls.Add(this.txtsearch, 1, 0);
            this.tlp_search_box.Controls.Add(this.cmbSearchlist, 0, 0);
            this.tlp_search_box.Dock = System.Windows.Forms.DockStyle.Right;
            this.tlp_search_box.Location = new System.Drawing.Point(101, 3);
            this.tlp_search_box.Name = "tlp_search_box";
            this.tlp_search_box.RowCount = 1;
            this.tlp_search_box.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_search_box.Size = new System.Drawing.Size(559, 35);
            this.tlp_search_box.TabIndex = 175;
            // 
            // Search
            // 
            this.Search.Dock = System.Windows.Forms.DockStyle.Top;
            this.Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Search.Location = new System.Drawing.Point(405, 3);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(151, 29);
            this.Search.TabIndex = 175;
            this.Search.Text = "Search";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // txtsearch
            // 
            this.txtsearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(176, 3);
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(223, 22);
            this.txtsearch.TabIndex = 176;
            // 
            // cmbSearchlist
            // 
            this.cmbSearchlist.BackColor = System.Drawing.Color.White;
            this.cmbSearchlist.DropDownHeight = 200;
            this.cmbSearchlist.ForeColor = System.Drawing.Color.Black;
            this.cmbSearchlist.FormattingEnabled = true;
            this.cmbSearchlist.IntegralHeight = false;
            this.cmbSearchlist.ItemHeight = 23;
            this.cmbSearchlist.Location = new System.Drawing.Point(3, 3);
            this.cmbSearchlist.Name = "cmbSearchlist";
            this.cmbSearchlist.Size = new System.Drawing.Size(167, 29);
            this.cmbSearchlist.TabIndex = 174;
            this.cmbSearchlist.Tag = "Designation";
            this.cmbSearchlist.UseCustomBackColor = true;
            this.cmbSearchlist.UseCustomForeColor = true;
            this.cmbSearchlist.UseSelectable = true;
            this.cmbSearchlist.SelectionChangeCommitted += new System.EventHandler(this.cmbSearchlist_SelectionChangeCommitted);
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 506);
            this.Controls.Add(this.tlp_menu_Gallery_main);
            this.Controls.Add(this.tlp_main);
            this.Controls.Add(this.txtmid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "FrmMenu";
            this.Text = "FrmMenu";
            this.Load += new System.EventHandler(this.FrmMenu_Load);
            this.tlp_registration.ResumeLayout(false);
            this.tlp_registration.PerformLayout();
            this.tlp_button.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_menu)).EndInit();
            this.tlp_main.ResumeLayout(false);
            this.tlp_registartion_main.ResumeLayout(false);
            this.tlp_outer_design.ResumeLayout(false);
            this.pnl_viewmenu_gal.ResumeLayout(false);
            this.tlp_regform_dgv.ResumeLayout(false);
            this.tlp_navbar.ResumeLayout(false);
            this.tlp_menu_Gallery_main.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tlp_search_box.ResumeLayout(false);
            this.tlp_search_box.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtdesc;
        private System.Windows.Forms.ComboBox cmbMenu_category;
        private System.Windows.Forms.TextBox txtmid;
        private System.Windows.Forms.TableLayoutPanel tlp_registration;
        private System.Windows.Forms.TableLayoutPanel tlp_button;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.DataGridView dgv_menu;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TableLayoutPanel tlp_navbar;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.TableLayoutPanel tlp_outer_design;
        private System.Windows.Forms.Panel pnl_viewmenu_gal;
        private System.Windows.Forms.Button Btn_view_menu;
        private System.Windows.Forms.TableLayoutPanel tlp_registartion_main;
        private System.Windows.Forms.Button btnseach_menu;
        private System.Windows.Forms.TableLayoutPanel tlp_regform_dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn no;
        private System.Windows.Forms.DataGridViewTextBoxColumn mid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Productid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usertype;
        private System.Windows.Forms.DataGridViewTextBoxColumn menucategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcid;
        private System.Windows.Forms.TableLayoutPanel tlp_menu_Gallery_main;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_bck_reg;
        private System.Windows.Forms.TableLayoutPanel tlp_search_box;
        private System.Windows.Forms.Button Search;
        private System.Windows.Forms.TextBox txtsearch;
        private MetroFramework.Controls.MetroComboBox cmbSearchlist;
    }
}