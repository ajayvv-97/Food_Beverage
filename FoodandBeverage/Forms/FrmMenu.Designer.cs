
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
            this.cmbMenu_category = new System.Windows.Forms.ComboBox();
            this.txtmid = new System.Windows.Forms.TextBox();
            this.tlp_registration = new System.Windows.Forms.TableLayoutPanel();
            this.btnseach_menu = new System.Windows.Forms.Button();
            this.txtsearchlist = new System.Windows.Forms.TextBox();
            this.btn_refresh_list = new System.Windows.Forms.Button();
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnmini = new System.Windows.Forms.Button();
            this.btncolse = new System.Windows.Forms.Button();
            this.tlp_menu_Gallery_main = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_bck_reg = new System.Windows.Forms.Button();
            this.tlp_search_box = new System.Windows.Forms.TableLayoutPanel();
            this.btn_refresh_list2 = new System.Windows.Forms.Button();
            this.Search = new System.Windows.Forms.Button();
            this.txtsearch = new System.Windows.Forms.TextBox();
            this.cmbSearchlist = new System.Windows.Forms.ComboBox();
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
            // cmbMenu_category
            // 
            this.cmbMenu_category.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.cmbMenu_category.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbMenu_category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMenu_category.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbMenu_category.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.cmbMenu_category.FormattingEnabled = true;
            this.cmbMenu_category.Location = new System.Drawing.Point(3, 3);
            this.cmbMenu_category.Name = "cmbMenu_category";
            this.cmbMenu_category.Size = new System.Drawing.Size(210, 28);
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
            this.tlp_registration.ColumnCount = 8;
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_registration.Controls.Add(this.cmbMenu_category, 2, 0);
            this.tlp_registration.Controls.Add(this.btnseach_menu, 6, 0);
            this.tlp_registration.Controls.Add(this.txtsearchlist, 5, 0);
            this.tlp_registration.Controls.Add(this.btn_refresh_list, 4, 0);
            this.tlp_registration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_registration.Location = new System.Drawing.Point(3, 3);
            this.tlp_registration.Name = "tlp_registration";
            this.tlp_registration.RowCount = 1;
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_registration.Size = new System.Drawing.Size(678, 38);
            this.tlp_registration.TabIndex = 4;
            this.tlp_registration.Paint += new System.Windows.Forms.PaintEventHandler(this.tlp_registration_Paint);
            // 
            // btnseach_menu
            // 
            this.btnseach_menu.BackColor = System.Drawing.Color.Transparent;
            this.btnseach_menu.BackgroundImage = global::FoodandBeverage.Properties.Resources.Search_icon;
            this.btnseach_menu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnseach_menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnseach_menu.FlatAppearance.BorderSize = 0;
            this.btnseach_menu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnseach_menu.Location = new System.Drawing.Point(498, 3);
            this.btnseach_menu.Name = "btnseach_menu";
            this.btnseach_menu.Size = new System.Drawing.Size(28, 32);
            this.btnseach_menu.TabIndex = 3;
            this.btnseach_menu.UseVisualStyleBackColor = false;
            this.btnseach_menu.Click += new System.EventHandler(this.btnseach_menu_Click);
            // 
            // txtsearchlist
            // 
            this.txtsearchlist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtsearchlist.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtsearchlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtsearchlist.Location = new System.Drawing.Point(259, 3);
            this.txtsearchlist.Multiline = true;
            this.txtsearchlist.Name = "txtsearchlist";
            this.txtsearchlist.Size = new System.Drawing.Size(233, 32);
            this.txtsearchlist.TabIndex = 4;
            this.txtsearchlist.Enter += new System.EventHandler(this.txtsearchlist_Enter);
            this.txtsearchlist.Leave += new System.EventHandler(this.txtsearchlist_Leave);
            // 
            // btn_refresh_list
            // 
            this.btn_refresh_list.BackColor = System.Drawing.Color.Transparent;
            this.btn_refresh_list.BackgroundImage = global::FoodandBeverage.Properties.Resources.refresh_button;
            this.btn_refresh_list.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_refresh_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_refresh_list.FlatAppearance.BorderSize = 0;
            this.btn_refresh_list.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_refresh_list.Location = new System.Drawing.Point(219, 3);
            this.btn_refresh_list.Name = "btn_refresh_list";
            this.btn_refresh_list.Size = new System.Drawing.Size(34, 32);
            this.btn_refresh_list.TabIndex = 5;
            this.btn_refresh_list.UseVisualStyleBackColor = false;
            this.btn_refresh_list.Click += new System.EventHandler(this.btn_refresh_list_Click);
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
            this.tlp_button.Location = new System.Drawing.Point(341, 554);
            this.tlp_button.Name = "tlp_button";
            this.tlp_button.RowCount = 1;
            this.tlp_button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_button.Size = new System.Drawing.Size(405, 58);
            this.tlp_button.TabIndex = 5;
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
            this.btnSearch.Location = new System.Drawing.Point(105, 4);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(95, 32);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
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
            this.btnsave.Size = new System.Drawing.Size(95, 32);
            this.btnsave.TabIndex = 0;
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
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
            this.btnrefresh.Location = new System.Drawing.Point(207, 3);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(95, 32);
            this.btnrefresh.TabIndex = 2;
            this.btnrefresh.UseVisualStyleBackColor = false;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
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
            this.btnclose.Size = new System.Drawing.Size(95, 32);
            this.btnclose.TabIndex = 3;
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
            this.dgv_menu.Location = new System.Drawing.Point(3, 47);
            this.dgv_menu.Name = "dgv_menu";
            this.dgv_menu.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dgv_menu.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_menu.RowTemplate.Height = 30;
            this.dgv_menu.Size = new System.Drawing.Size(678, 457);
            this.dgv_menu.TabIndex = 0;
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
            this.Status.Width = 62;
            // 
            // fcid
            // 
            this.fcid.HeaderText = "fcid";
            this.fcid.Name = "fcid";
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
            this.tlp_main.Size = new System.Drawing.Size(755, 658);
            this.tlp_main.TabIndex = 7;
            // 
            // tlp_registartion_main
            // 
            this.tlp_registartion_main.ColumnCount = 1;
            this.tlp_registartion_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_registartion_main.Controls.Add(this.tlp_outer_design, 0, 0);
            this.tlp_registartion_main.Controls.Add(this.tlp_button, 0, 1);
            this.tlp_registartion_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_registartion_main.Location = new System.Drawing.Point(3, 40);
            this.tlp_registartion_main.Name = "tlp_registartion_main";
            this.tlp_registartion_main.RowCount = 2;
            this.tlp_registartion_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.6861F));
            this.tlp_registartion_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.3139F));
            this.tlp_registartion_main.Size = new System.Drawing.Size(749, 615);
            this.tlp_registartion_main.TabIndex = 0;
            // 
            // tlp_outer_design
            // 
            this.tlp_outer_design.BackColor = System.Drawing.Color.AliceBlue;
            this.tlp_outer_design.ColumnCount = 3;
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_outer_design.Controls.Add(this.pnl_viewmenu_gal, 2, 1);
            this.tlp_outer_design.Controls.Add(this.tlp_regform_dgv, 1, 1);
            this.tlp_outer_design.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_outer_design.Location = new System.Drawing.Point(3, 3);
            this.tlp_outer_design.Name = "tlp_outer_design";
            this.tlp_outer_design.RowCount = 3;
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 109F));
            this.tlp_outer_design.Size = new System.Drawing.Size(743, 545);
            this.tlp_outer_design.TabIndex = 8;
            // 
            // pnl_viewmenu_gal
            // 
            this.pnl_viewmenu_gal.BackColor = System.Drawing.Color.AliceBlue;
            this.pnl_viewmenu_gal.Controls.Add(this.Btn_view_menu);
            this.pnl_viewmenu_gal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_viewmenu_gal.Location = new System.Drawing.Point(707, 14);
            this.pnl_viewmenu_gal.Name = "pnl_viewmenu_gal";
            this.pnl_viewmenu_gal.Size = new System.Drawing.Size(33, 516);
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
            this.tlp_regform_dgv.Location = new System.Drawing.Point(17, 14);
            this.tlp_regform_dgv.Name = "tlp_regform_dgv";
            this.tlp_regform_dgv.RowCount = 3;
            this.tlp_regform_dgv.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.678501F));
            this.tlp_regform_dgv.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.3215F));
            this.tlp_regform_dgv.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlp_regform_dgv.Size = new System.Drawing.Size(684, 516);
            this.tlp_regform_dgv.TabIndex = 6;
            // 
            // tlp_navbar
            // 
            this.tlp_navbar.BackColor = System.Drawing.Color.SteelBlue;
            this.tlp_navbar.ColumnCount = 3;
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_navbar.Controls.Add(this.label2, 0, 0);
            this.tlp_navbar.Controls.Add(this.btnmini, 1, 0);
            this.tlp_navbar.Controls.Add(this.btncolse, 2, 0);
            this.tlp_navbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_navbar.Location = new System.Drawing.Point(3, 3);
            this.tlp_navbar.Name = "tlp_navbar";
            this.tlp_navbar.RowCount = 1;
            this.tlp_navbar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.Size = new System.Drawing.Size(749, 31);
            this.tlp_navbar.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 24);
            this.label2.TabIndex = 12;
            this.label2.Text = "Menu";
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
            this.btncolse.Location = new System.Drawing.Point(721, 3);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(25, 25);
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
            this.tlp_menu_Gallery_main.Location = new System.Drawing.Point(0, 165);
            this.tlp_menu_Gallery_main.Name = "tlp_menu_Gallery_main";
            this.tlp_menu_Gallery_main.RowCount = 2;
            this.tlp_menu_Gallery_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_menu_Gallery_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_menu_Gallery_main.Size = new System.Drawing.Size(699, 72);
            this.tlp_menu_Gallery_main.TabIndex = 0;
            this.tlp_menu_Gallery_main.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_bck_reg);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(668, 46);
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
            this.tlp_search_box.ColumnCount = 4;
            this.tlp_search_box.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.52427F));
            this.tlp_search_box.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.47573F));
            this.tlp_search_box.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tlp_search_box.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.tlp_search_box.Controls.Add(this.btn_refresh_list2, 1, 0);
            this.tlp_search_box.Controls.Add(this.Search, 3, 0);
            this.tlp_search_box.Controls.Add(this.txtsearch, 2, 0);
            this.tlp_search_box.Controls.Add(this.cmbSearchlist, 0, 0);
            this.tlp_search_box.Dock = System.Windows.Forms.DockStyle.Right;
            this.tlp_search_box.Location = new System.Drawing.Point(101, 3);
            this.tlp_search_box.Name = "tlp_search_box";
            this.tlp_search_box.RowCount = 1;
            this.tlp_search_box.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_search_box.Size = new System.Drawing.Size(559, 37);
            this.tlp_search_box.TabIndex = 0;
            // 
            // btn_refresh_list2
            // 
            this.btn_refresh_list2.BackColor = System.Drawing.Color.Transparent;
            this.btn_refresh_list2.BackgroundImage = global::FoodandBeverage.Properties.Resources.refresh_button;
            this.btn_refresh_list2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_refresh_list2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_refresh_list2.FlatAppearance.BorderSize = 0;
            this.btn_refresh_list2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_refresh_list2.Location = new System.Drawing.Point(234, 3);
            this.btn_refresh_list2.Name = "btn_refresh_list2";
            this.btn_refresh_list2.Size = new System.Drawing.Size(43, 31);
            this.btn_refresh_list2.TabIndex = 1;
            this.btn_refresh_list2.UseVisualStyleBackColor = false;
            this.btn_refresh_list2.Click += new System.EventHandler(this.btn_refresh_list2_Click);
            // 
            // Search
            // 
            this.Search.BackgroundImage = global::FoodandBeverage.Properties.Resources.Search_icon;
            this.Search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Search.FlatAppearance.BorderSize = 0;
            this.Search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Search.Location = new System.Drawing.Point(493, 3);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(63, 31);
            this.Search.TabIndex = 3;
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // txtsearch
            // 
            this.txtsearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtsearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtsearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsearch.Location = new System.Drawing.Point(283, 3);
            this.txtsearch.Multiline = true;
            this.txtsearch.Name = "txtsearch";
            this.txtsearch.Size = new System.Drawing.Size(204, 31);
            this.txtsearch.TabIndex = 2;
            this.txtsearch.Enter += new System.EventHandler(this.txtsearch_Enter);
            this.txtsearch.Leave += new System.EventHandler(this.txtsearch_Leave);
            // 
            // cmbSearchlist
            // 
            this.cmbSearchlist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.cmbSearchlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSearchlist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchlist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSearchlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.cmbSearchlist.FormattingEnabled = true;
            this.cmbSearchlist.Location = new System.Drawing.Point(3, 3);
            this.cmbSearchlist.Name = "cmbSearchlist";
            this.cmbSearchlist.Size = new System.Drawing.Size(225, 28);
            this.cmbSearchlist.TabIndex = 0;
            this.cmbSearchlist.SelectionChangeCommitted += new System.EventHandler(this.cmbSearchlist_SelectionChangeCommitted_1);
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 658);
            this.Controls.Add(this.tlp_menu_Gallery_main);
            this.Controls.Add(this.tlp_main);
            this.Controls.Add(this.txtmid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.On;
            this.Name = "FrmMenu";
            this.Text = "FrmMenu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
            this.tlp_navbar.PerformLayout();
            this.tlp_menu_Gallery_main.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tlp_search_box.ResumeLayout(false);
            this.tlp_search_box.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        private System.Windows.Forms.TableLayoutPanel tlp_regform_dgv;
        private System.Windows.Forms.TableLayoutPanel tlp_menu_Gallery_main;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_bck_reg;
        private System.Windows.Forms.TableLayoutPanel tlp_search_box;
        private System.Windows.Forms.Button Search;
        private System.Windows.Forms.TextBox txtsearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn no;
        private System.Windows.Forms.DataGridViewTextBoxColumn mid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Productid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usertype;
        private System.Windows.Forms.DataGridViewTextBoxColumn menucategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcid;
        private System.Windows.Forms.Button btn_refresh_list2;
        private System.Windows.Forms.ComboBox cmbSearchlist;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnseach_menu;
        private System.Windows.Forms.TextBox txtsearchlist;
        private System.Windows.Forms.Button btn_refresh_list;
    }
}