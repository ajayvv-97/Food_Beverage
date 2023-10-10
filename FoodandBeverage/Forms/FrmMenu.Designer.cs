
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtdesc = new System.Windows.Forms.TextBox();
            this.cmbfcateg = new System.Windows.Forms.ComboBox();
            this.txtmid = new System.Windows.Forms.TextBox();
            this.tlp_registration = new System.Windows.Forms.TableLayoutPanel();
            this.txtrecipedes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chb_active = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.chk_avilable = new System.Windows.Forms.CheckBox();
            this.tlp_button = new System.Windows.Forms.TableLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.dgv_menu = new System.Windows.Forms.DataGridView();
            this.Slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Usertype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.foodcateg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.fcid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_registartion_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_outer_design = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_viewmenu_gal = new System.Windows.Forms.Panel();
            this.Btn_view_menu = new System.Windows.Forms.Button();
            this.tlp_navbar = new System.Windows.Forms.TableLayoutPanel();
            this.btnmini = new System.Windows.Forms.Button();
            this.btncolse = new System.Windows.Forms.Button();
            this.tlp_main_pic = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_buttons_pic = new System.Windows.Forms.Panel();
            this.btn_clear_pic = new System.Windows.Forms.Button();
            this.btn_change_pic = new System.Windows.Forms.Button();
            this.btn_close_pic = new System.Windows.Forms.Button();
            this.pb_view_pic = new System.Windows.Forms.PictureBox();
            this.pnl_menupic = new System.Windows.Forms.Panel();
            this.tlp_menu_Gallery = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_back_reg = new System.Windows.Forms.Button();
            this.pnlGallery = new System.Windows.Forms.Panel();
            this.tlp_registration.SuspendLayout();
            this.tlp_button.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_menu)).BeginInit();
            this.tlp_main.SuspendLayout();
            this.tlp_registartion_main.SuspendLayout();
            this.tlp_outer_design.SuspendLayout();
            this.pnl_viewmenu_gal.SuspendLayout();
            this.tlp_navbar.SuspendLayout();
            this.tlp_main_pic.SuspendLayout();
            this.pnl_buttons_pic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_view_pic)).BeginInit();
            this.pnl_menupic.SuspendLayout();
            this.tlp_menu_Gallery.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtdesc
            // 
            this.txtdesc.Location = new System.Drawing.Point(436, 47);
            this.txtdesc.Multiline = true;
            this.txtdesc.Name = "txtdesc";
            this.txtdesc.Size = new System.Drawing.Size(211, 19);
            this.txtdesc.TabIndex = 1;
            // 
            // cmbfcateg
            // 
            this.cmbfcateg.FormattingEnabled = true;
            this.cmbfcateg.Location = new System.Drawing.Point(220, 47);
            this.cmbfcateg.Name = "cmbfcateg";
            this.cmbfcateg.Size = new System.Drawing.Size(210, 21);
            this.cmbfcateg.TabIndex = 2;
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
            this.tlp_registration.ColumnCount = 3;
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_registration.Controls.Add(this.txtrecipedes, 0, 3);
            this.tlp_registration.Controls.Add(this.label1, 0, 0);
            this.tlp_registration.Controls.Add(this.cmbfcateg, 1, 1);
            this.tlp_registration.Controls.Add(this.label3, 1, 0);
            this.tlp_registration.Controls.Add(this.label2, 2, 0);
            this.tlp_registration.Controls.Add(this.txtdesc, 2, 1);
            this.tlp_registration.Controls.Add(this.label5, 0, 2);
            this.tlp_registration.Controls.Add(this.label6, 1, 2);
            this.tlp_registration.Controls.Add(this.chk_avilable, 1, 3);
            this.tlp_registration.Controls.Add(this.label4, 2, 2);
            this.tlp_registration.Controls.Add(this.chb_active, 2, 3);
            this.tlp_registration.Location = new System.Drawing.Point(14, 18);
            this.tlp_registration.Name = "tlp_registration";
            this.tlp_registration.RowCount = 4;
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.35211F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.77528F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 31.46067F));
            this.tlp_registration.Size = new System.Drawing.Size(663, 178);
            this.tlp_registration.TabIndex = 4;
            this.tlp_registration.Paint += new System.Windows.Forms.PaintEventHandler(this.tlp_registration_Paint);
            // 
            // txtrecipedes
            // 
            this.txtrecipedes.Location = new System.Drawing.Point(3, 124);
            this.txtrecipedes.Multiline = true;
            this.txtrecipedes.Name = "txtrecipedes";
            this.txtrecipedes.Size = new System.Drawing.Size(211, 30);
            this.txtrecipedes.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(211, 44);
            this.label1.TabIndex = 5;
            this.label1.Text = "Food Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(220, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 44);
            this.label3.TabIndex = 7;
            this.label3.Text = "Food Category";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(436, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 44);
            this.label2.TabIndex = 6;
            this.label2.Text = "Description";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(211, 52);
            this.label5.TabIndex = 10;
            this.label5.Text = "Recipe Description";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(436, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(224, 52);
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
            this.chb_active.Location = new System.Drawing.Point(533, 136);
            this.chb_active.Margin = new System.Windows.Forms.Padding(100, 15, 3, 3);
            this.chb_active.Name = "chb_active";
            this.chb_active.Size = new System.Drawing.Size(15, 14);
            this.chb_active.TabIndex = 9;
            this.chb_active.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chb_active.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(220, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(210, 52);
            this.label6.TabIndex = 11;
            this.label6.Text = "Avilable Status";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chk_avilable
            // 
            this.chk_avilable.AutoSize = true;
            this.chk_avilable.Checked = true;
            this.chk_avilable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_avilable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_avilable.Location = new System.Drawing.Point(317, 136);
            this.chk_avilable.Margin = new System.Windows.Forms.Padding(100, 15, 3, 3);
            this.chk_avilable.Name = "chk_avilable";
            this.chk_avilable.Size = new System.Drawing.Size(15, 14);
            this.chk_avilable.TabIndex = 13;
            this.chk_avilable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chk_avilable.UseVisualStyleBackColor = true;
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
            this.tlp_button.Location = new System.Drawing.Point(311, 220);
            this.tlp_button.Name = "tlp_button";
            this.tlp_button.RowCount = 1;
            this.tlp_button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_button.Size = new System.Drawing.Size(405, 38);
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
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_menu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_menu.ColumnHeadersHeight = 46;
            this.dgv_menu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Slno,
            this.Id,
            this.Usertype,
            this.foodcateg,
            this.Description,
            this.Status,
            this.fcid});
            this.dgv_menu.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_menu.Location = new System.Drawing.Point(711, 0);
            this.dgv_menu.Name = "dgv_menu";
            this.dgv_menu.RowHeadersVisible = false;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_menu.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgv_menu.RowTemplate.Height = 30;
            this.dgv_menu.Size = new System.Drawing.Size(326, 112);
            this.dgv_menu.TabIndex = 6;
            this.dgv_menu.Visible = false;
            this.dgv_menu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_menu_CellClick);
            // 
            // Slno
            // 
            this.Slno.HeaderText = "Slno";
            this.Slno.Name = "Slno";
            this.Slno.ReadOnly = true;
            // 
            // Id
            // 
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewCellStyle8.NullValue = "0";
            this.Id.DefaultCellStyle = dataGridViewCellStyle8;
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // Usertype
            // 
            this.Usertype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Usertype.HeaderText = "Food Name";
            this.Usertype.Name = "Usertype";
            // 
            // foodcateg
            // 
            this.foodcateg.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.foodcateg.HeaderText = "Food Category";
            this.foodcateg.Name = "foodcateg";
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.Visible = false;
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
            this.tlp_main.Size = new System.Drawing.Size(725, 321);
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
            this.tlp_registartion_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tlp_registartion_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp_registartion_main.Size = new System.Drawing.Size(719, 261);
            this.tlp_registartion_main.TabIndex = 6;
            // 
            // tlp_outer_design
            // 
            this.tlp_outer_design.BackColor = System.Drawing.Color.AliceBlue;
            this.tlp_outer_design.ColumnCount = 3;
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 11F));
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 179F));
            this.tlp_outer_design.Controls.Add(this.tlp_registration, 1, 1);
            this.tlp_outer_design.Controls.Add(this.pnl_viewmenu_gal, 2, 1);
            this.tlp_outer_design.Location = new System.Drawing.Point(3, 3);
            this.tlp_outer_design.Name = "tlp_outer_design";
            this.tlp_outer_design.RowCount = 3;
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 105F));
            this.tlp_outer_design.Size = new System.Drawing.Size(713, 211);
            this.tlp_outer_design.TabIndex = 8;
            // 
            // pnl_viewmenu_gal
            // 
            this.pnl_viewmenu_gal.Controls.Add(this.Btn_view_menu);
            this.pnl_viewmenu_gal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_viewmenu_gal.Location = new System.Drawing.Point(683, 18);
            this.pnl_viewmenu_gal.Name = "pnl_viewmenu_gal";
            this.pnl_viewmenu_gal.Size = new System.Drawing.Size(173, 224);
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
            this.tlp_navbar.Size = new System.Drawing.Size(719, 48);
            this.tlp_navbar.TabIndex = 17;
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(660, 3);
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
            this.btncolse.Location = new System.Drawing.Point(691, 3);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(25, 36);
            this.btncolse.TabIndex = 10;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // tlp_main_pic
            // 
            this.tlp_main_pic.ColumnCount = 1;
            this.tlp_main_pic.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main_pic.Controls.Add(this.pnl_buttons_pic, 0, 2);
            this.tlp_main_pic.Controls.Add(this.btn_close_pic, 0, 0);
            this.tlp_main_pic.Controls.Add(this.pb_view_pic, 0, 1);
            this.tlp_main_pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main_pic.Location = new System.Drawing.Point(0, 0);
            this.tlp_main_pic.Name = "tlp_main_pic";
            this.tlp_main_pic.RowCount = 3;
            this.tlp_main_pic.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlp_main_pic.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81.91882F));
            this.tlp_main_pic.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 18.08118F));
            this.tlp_main_pic.Size = new System.Drawing.Size(276, 267);
            this.tlp_main_pic.TabIndex = 0;
            // 
            // pnl_buttons_pic
            // 
            this.pnl_buttons_pic.Controls.Add(this.btn_clear_pic);
            this.pnl_buttons_pic.Controls.Add(this.btn_change_pic);
            this.pnl_buttons_pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_buttons_pic.Location = new System.Drawing.Point(3, 227);
            this.pnl_buttons_pic.Name = "pnl_buttons_pic";
            this.pnl_buttons_pic.Size = new System.Drawing.Size(270, 37);
            this.pnl_buttons_pic.TabIndex = 0;
            // 
            // btn_clear_pic
            // 
            this.btn_clear_pic.BackgroundImage = global::FoodandBeverage.Properties.Resources.remove_image;
            this.btn_clear_pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_clear_pic.FlatAppearance.BorderSize = 0;
            this.btn_clear_pic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear_pic.Location = new System.Drawing.Point(157, 4);
            this.btn_clear_pic.Name = "btn_clear_pic";
            this.btn_clear_pic.Size = new System.Drawing.Size(75, 30);
            this.btn_clear_pic.TabIndex = 1;
            this.btn_clear_pic.UseVisualStyleBackColor = true;
            this.btn_clear_pic.Click += new System.EventHandler(this.btn_clear_pic_Click);
            // 
            // btn_change_pic
            // 
            this.btn_change_pic.BackgroundImage = global::FoodandBeverage.Properties.Resources.FoodPic;
            this.btn_change_pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_change_pic.FlatAppearance.BorderSize = 0;
            this.btn_change_pic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_change_pic.Location = new System.Drawing.Point(39, 4);
            this.btn_change_pic.Name = "btn_change_pic";
            this.btn_change_pic.Size = new System.Drawing.Size(75, 30);
            this.btn_change_pic.TabIndex = 0;
            this.btn_change_pic.UseVisualStyleBackColor = true;
            this.btn_change_pic.Click += new System.EventHandler(this.btn_change_pic_Click);
            // 
            // btn_close_pic
            // 
            this.btn_close_pic.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_close_pic.FlatAppearance.BorderSize = 0;
            this.btn_close_pic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close_pic.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_close_pic.ForeColor = System.Drawing.Color.Red;
            this.btn_close_pic.Location = new System.Drawing.Point(227, 3);
            this.btn_close_pic.Name = "btn_close_pic";
            this.btn_close_pic.Size = new System.Drawing.Size(46, 24);
            this.btn_close_pic.TabIndex = 1;
            this.btn_close_pic.Text = "X";
            this.btn_close_pic.UseVisualStyleBackColor = true;
            this.btn_close_pic.Click += new System.EventHandler(this.btn_close_pic_Click);
            // 
            // pb_view_pic
            // 
            this.pb_view_pic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pb_view_pic.Location = new System.Drawing.Point(3, 33);
            this.pb_view_pic.Name = "pb_view_pic";
            this.pb_view_pic.Size = new System.Drawing.Size(270, 188);
            this.pb_view_pic.TabIndex = 2;
            this.pb_view_pic.TabStop = false;
            // 
            // pnl_menupic
            // 
            this.pnl_menupic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_menupic.Controls.Add(this.tlp_main_pic);
            this.pnl_menupic.Location = new System.Drawing.Point(744, 107);
            this.pnl_menupic.Name = "pnl_menupic";
            this.pnl_menupic.Size = new System.Drawing.Size(280, 271);
            this.pnl_menupic.TabIndex = 5;
            this.pnl_menupic.Visible = false;
            // 
            // tlp_menu_Gallery
            // 
            this.tlp_menu_Gallery.ColumnCount = 2;
            this.tlp_menu_Gallery.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 93.4033F));
            this.tlp_menu_Gallery.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.596702F));
            this.tlp_menu_Gallery.Controls.Add(this.panel1, 1, 0);
            this.tlp_menu_Gallery.Controls.Add(this.pnlGallery, 0, 0);
            this.tlp_menu_Gallery.Location = new System.Drawing.Point(764, 0);
            this.tlp_menu_Gallery.Name = "tlp_menu_Gallery";
            this.tlp_menu_Gallery.RowCount = 1;
            this.tlp_menu_Gallery.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_menu_Gallery.Size = new System.Drawing.Size(295, 300);
            this.tlp_menu_Gallery.TabIndex = 16;
            this.tlp_menu_Gallery.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_back_reg);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(278, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(14, 294);
            this.panel1.TabIndex = 0;
            // 
            // btn_back_reg
            // 
            this.btn_back_reg.BackgroundImage = global::FoodandBeverage.Properties.Resources.back;
            this.btn_back_reg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_back_reg.FlatAppearance.BorderSize = 0;
            this.btn_back_reg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_back_reg.Location = new System.Drawing.Point(3, 115);
            this.btn_back_reg.Name = "btn_back_reg";
            this.btn_back_reg.Size = new System.Drawing.Size(32, 44);
            this.btn_back_reg.TabIndex = 0;
            this.btn_back_reg.UseVisualStyleBackColor = true;
            this.btn_back_reg.Click += new System.EventHandler(this.btn_back_reg_Click);
            // 
            // pnlGallery
            // 
            this.pnlGallery.AutoScroll = true;
            this.pnlGallery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGallery.Location = new System.Drawing.Point(3, 3);
            this.pnlGallery.Name = "pnlGallery";
            this.pnlGallery.Size = new System.Drawing.Size(269, 294);
            this.pnlGallery.TabIndex = 1;
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 321);
            this.Controls.Add(this.tlp_menu_Gallery);
            this.Controls.Add(this.pnl_menupic);
            this.Controls.Add(this.dgv_menu);
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
            this.tlp_navbar.ResumeLayout(false);
            this.tlp_main_pic.ResumeLayout(false);
            this.pnl_buttons_pic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_view_pic)).EndInit();
            this.pnl_menupic.ResumeLayout(false);
            this.tlp_menu_Gallery.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtdesc;
        private System.Windows.Forms.ComboBox cmbfcateg;
        private System.Windows.Forms.TextBox txtmid;
        private System.Windows.Forms.TableLayoutPanel tlp_registration;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tlp_button;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chb_active;
        private System.Windows.Forms.DataGridView dgv_menu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Slno;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usertype;
        private System.Windows.Forms.DataGridViewTextBoxColumn foodcateg;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn fcid;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TableLayoutPanel tlp_navbar;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.TableLayoutPanel tlp_outer_design;
        private System.Windows.Forms.CheckBox chk_avilable;
        private System.Windows.Forms.TextBox txtrecipedes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel pnl_viewmenu_gal;
        private System.Windows.Forms.Button Btn_view_menu;
        private System.Windows.Forms.TableLayoutPanel tlp_main_pic;
        private System.Windows.Forms.Panel pnl_buttons_pic;
        private System.Windows.Forms.Button btn_clear_pic;
        private System.Windows.Forms.Button btn_change_pic;
        private System.Windows.Forms.Button btn_close_pic;
        private System.Windows.Forms.PictureBox pb_view_pic;
        private System.Windows.Forms.Panel pnl_menupic;
        private System.Windows.Forms.TableLayoutPanel tlp_registartion_main;
        private System.Windows.Forms.TableLayoutPanel tlp_menu_Gallery;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_back_reg;
        private System.Windows.Forms.Panel pnlGallery;
    }
}