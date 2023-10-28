
namespace FoodandBeverage.Forms
{
    partial class FrmDashboard
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
            this.components = new System.ComponentModel.Container();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.flp_menu = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_header = new System.Windows.Forms.Panel();
            this.pb_appicon = new System.Windows.Forms.PictureBox();
            this.Menuhome = new FontAwesome.Sharp.IconButton();
            this.Menuoperation = new FontAwesome.Sharp.IconButton();
            this.Menubilling = new FontAwesome.Sharp.IconButton();
            this.Menureport = new FontAwesome.Sharp.IconButton();
            this.Menusetting = new FontAwesome.Sharp.IconButton();
            this.Menulogout = new FontAwesome.Sharp.IconButton();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.navibar = new System.Windows.Forms.Panel();
            this.navibar1 = new System.Windows.Forms.Panel();
            this.pnlappcolor = new System.Windows.Forms.Panel();
            this.btnappok = new System.Windows.Forms.Button();
            this.btnappcolse = new System.Windows.Forms.Button();
            this.txtcolorcode = new System.Windows.Forms.TextBox();
            this.chkentercolor = new System.Windows.Forms.CheckBox();
            this.pbappcoloepicker = new System.Windows.Forms.PictureBox();
            this.lblappcolor = new System.Windows.Forms.Label();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.flp_time = new System.Windows.Forms.FlowLayoutPanel();
            this.lblTime = new System.Windows.Forms.Label();
            this.datelbl = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnmini = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tlp_tablestatus = new System.Windows.Forms.TableLayoutPanel();
            this.btntrefresh = new System.Windows.Forms.Button();
            this.btn_avial = new System.Windows.Forms.Button();
            this.btn_rev = new System.Windows.Forms.Button();
            this.btn_running = new System.Windows.Forms.Button();
            this.flp_operations = new System.Windows.Forms.FlowLayoutPanel();
            this.ib_Category = new FontAwesome.Sharp.IconButton();
            this.ib_Item = new FontAwesome.Sharp.IconButton();
            this.ib_ingrediant = new FontAwesome.Sharp.IconButton();
            this.ib_menu = new FontAwesome.Sharp.IconButton();
            this.ib_unit = new FontAwesome.Sharp.IconButton();
            this.ib_table = new FontAwesome.Sharp.IconButton();
            this.ib_purchase = new FontAwesome.Sharp.IconButton();
            this.ib_purchasereturn = new FontAwesome.Sharp.IconButton();
            this.ib_tax = new FontAwesome.Sharp.IconButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panelMenu.SuspendLayout();
            this.flp_menu.SuspendLayout();
            this.pnl_header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_appicon)).BeginInit();
            this.panelDesktop.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlappcolor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbappcoloepicker)).BeginInit();
            this.tlp_main.SuspendLayout();
            this.flp_time.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tlp_tablestatus.SuspendLayout();
            this.flp_operations.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.AutoScroll = true;
            this.panelMenu.AutoScrollMargin = new System.Drawing.Size(0, 50);
            this.panelMenu.AutoScrollMinSize = new System.Drawing.Size(0, 10);
            this.panelMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMenu.BackColor = System.Drawing.Color.Silver;
            this.panelMenu.Controls.Add(this.flp_menu);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMenu.ForeColor = System.Drawing.SystemColors.Window;
            this.panelMenu.Location = new System.Drawing.Point(12, 42);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(2);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(184, 671);
            this.panelMenu.TabIndex = 4;
            this.panelMenu.Tag = "MenuBar";
            // 
            // flp_menu
            // 
            this.flp_menu.BackColor = System.Drawing.Color.White;
            this.flp_menu.Controls.Add(this.pnl_header);
            this.flp_menu.Controls.Add(this.Menuhome);
            this.flp_menu.Controls.Add(this.Menuoperation);
            this.flp_menu.Controls.Add(this.Menubilling);
            this.flp_menu.Controls.Add(this.Menureport);
            this.flp_menu.Controls.Add(this.Menusetting);
            this.flp_menu.Controls.Add(this.Menulogout);
            this.flp_menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_menu.Location = new System.Drawing.Point(0, 0);
            this.flp_menu.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.flp_menu.Name = "flp_menu";
            this.flp_menu.Size = new System.Drawing.Size(184, 671);
            this.flp_menu.TabIndex = 1;
            // 
            // pnl_header
            // 
            this.pnl_header.BackColor = System.Drawing.Color.Transparent;
            this.pnl_header.Controls.Add(this.pb_appicon);
            this.pnl_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_header.Location = new System.Drawing.Point(2, 2);
            this.pnl_header.Margin = new System.Windows.Forms.Padding(2);
            this.pnl_header.Name = "pnl_header";
            this.pnl_header.Size = new System.Drawing.Size(179, 52);
            this.pnl_header.TabIndex = 0;
            this.toolTip1.SetToolTip(this.pnl_header, "collpase");
            this.pnl_header.Click += new System.EventHandler(this.pnl_header_Click);
            // 
            // pb_appicon
            // 
            this.pb_appicon.BackgroundImage = global::FoodandBeverage.Properties.Resources.Icon;
            this.pb_appicon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_appicon.Location = new System.Drawing.Point(50, 1);
            this.pb_appicon.Name = "pb_appicon";
            this.pb_appicon.Size = new System.Drawing.Size(71, 50);
            this.pb_appicon.TabIndex = 0;
            this.pb_appicon.TabStop = false;
            this.toolTip1.SetToolTip(this.pb_appicon, "Collapse ");
            this.pb_appicon.Click += new System.EventHandler(this.pnl_header_Click);
            // 
            // Menuhome
            // 
            this.Menuhome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Menuhome.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Menuhome.FlatAppearance.BorderSize = 0;
            this.Menuhome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Menuhome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menuhome.IconChar = FontAwesome.Sharp.IconChar.XRay;
            this.Menuhome.IconColor = System.Drawing.Color.White;
            this.Menuhome.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Menuhome.IconSize = 30;
            this.Menuhome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Menuhome.Location = new System.Drawing.Point(10, 66);
            this.Menuhome.Margin = new System.Windows.Forms.Padding(10, 10, 10, 2);
            this.Menuhome.Name = "Menuhome";
            this.Menuhome.Size = new System.Drawing.Size(162, 52);
            this.Menuhome.TabIndex = 3;
            this.Menuhome.Tag = "Location";
            this.Menuhome.Text = "Home";
            this.Menuhome.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Menuhome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.Menuhome, "Home");
            this.Menuhome.UseVisualStyleBackColor = false;
            this.Menuhome.Click += new System.EventHandler(this.Menuhome_Click);
            // 
            // Menuoperation
            // 
            this.Menuoperation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Menuoperation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Menuoperation.FlatAppearance.BorderSize = 0;
            this.Menuoperation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Menuoperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menuoperation.IconChar = FontAwesome.Sharp.IconChar.S;
            this.Menuoperation.IconColor = System.Drawing.Color.White;
            this.Menuoperation.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Menuoperation.IconSize = 30;
            this.Menuoperation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Menuoperation.Location = new System.Drawing.Point(10, 130);
            this.Menuoperation.Margin = new System.Windows.Forms.Padding(10, 10, 10, 2);
            this.Menuoperation.Name = "Menuoperation";
            this.Menuoperation.Size = new System.Drawing.Size(162, 52);
            this.Menuoperation.TabIndex = 4;
            this.Menuoperation.Tag = "operation";
            this.Menuoperation.Text = "Operations";
            this.Menuoperation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Menuoperation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.Menuoperation, "Operations");
            this.Menuoperation.UseVisualStyleBackColor = false;
            this.Menuoperation.Click += new System.EventHandler(this.Menuoperation_Click);
            // 
            // Menubilling
            // 
            this.Menubilling.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Menubilling.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Menubilling.FlatAppearance.BorderSize = 0;
            this.Menubilling.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Menubilling.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menubilling.IconChar = FontAwesome.Sharp.IconChar.Landmark;
            this.Menubilling.IconColor = System.Drawing.Color.White;
            this.Menubilling.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Menubilling.IconSize = 30;
            this.Menubilling.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Menubilling.Location = new System.Drawing.Point(10, 194);
            this.Menubilling.Margin = new System.Windows.Forms.Padding(10, 10, 10, 2);
            this.Menubilling.Name = "Menubilling";
            this.Menubilling.Size = new System.Drawing.Size(162, 52);
            this.Menubilling.TabIndex = 6;
            this.Menubilling.Tag = "Location";
            this.Menubilling.Text = "Billing";
            this.Menubilling.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Menubilling.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.Menubilling, "Billing");
            this.Menubilling.UseVisualStyleBackColor = false;
            this.Menubilling.Click += new System.EventHandler(this.Menubilling_Click);
            // 
            // Menureport
            // 
            this.Menureport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Menureport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Menureport.FlatAppearance.BorderSize = 0;
            this.Menureport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Menureport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menureport.IconChar = FontAwesome.Sharp.IconChar.Landmark;
            this.Menureport.IconColor = System.Drawing.Color.White;
            this.Menureport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Menureport.IconSize = 30;
            this.Menureport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Menureport.Location = new System.Drawing.Point(10, 258);
            this.Menureport.Margin = new System.Windows.Forms.Padding(10, 10, 10, 2);
            this.Menureport.Name = "Menureport";
            this.Menureport.Size = new System.Drawing.Size(162, 52);
            this.Menureport.TabIndex = 5;
            this.Menureport.Tag = "Report";
            this.Menureport.Text = "Report";
            this.Menureport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Menureport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.Menureport, "Report");
            this.Menureport.UseVisualStyleBackColor = false;
            this.Menureport.Click += new System.EventHandler(this.Menureport_Click);
            // 
            // Menusetting
            // 
            this.Menusetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Menusetting.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Menusetting.FlatAppearance.BorderSize = 0;
            this.Menusetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Menusetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menusetting.IconChar = FontAwesome.Sharp.IconChar.Landmark;
            this.Menusetting.IconColor = System.Drawing.Color.White;
            this.Menusetting.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Menusetting.IconSize = 30;
            this.Menusetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Menusetting.Location = new System.Drawing.Point(10, 322);
            this.Menusetting.Margin = new System.Windows.Forms.Padding(10, 10, 10, 2);
            this.Menusetting.Name = "Menusetting";
            this.Menusetting.Size = new System.Drawing.Size(162, 52);
            this.Menusetting.TabIndex = 7;
            this.Menusetting.Tag = "Location";
            this.Menusetting.Text = "Settings";
            this.Menusetting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Menusetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.Menusetting, "Settings");
            this.Menusetting.UseVisualStyleBackColor = false;
            this.Menusetting.Click += new System.EventHandler(this.Menusetting_Click);
            // 
            // Menulogout
            // 
            this.Menulogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Menulogout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Menulogout.FlatAppearance.BorderSize = 0;
            this.Menulogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Menulogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menulogout.IconChar = FontAwesome.Sharp.IconChar.Landmark;
            this.Menulogout.IconColor = System.Drawing.Color.White;
            this.Menulogout.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Menulogout.IconSize = 30;
            this.Menulogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Menulogout.Location = new System.Drawing.Point(10, 386);
            this.Menulogout.Margin = new System.Windows.Forms.Padding(10, 10, 10, 2);
            this.Menulogout.Name = "Menulogout";
            this.Menulogout.Size = new System.Drawing.Size(162, 52);
            this.Menulogout.TabIndex = 8;
            this.Menulogout.Tag = "Location";
            this.Menulogout.Text = "Logout";
            this.Menulogout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Menulogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.Menulogout, "Logout");
            this.Menulogout.UseVisualStyleBackColor = false;
            this.Menulogout.Click += new System.EventHandler(this.Menulogout_Click);
            // 
            // panelDesktop
            // 
            this.panelDesktop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelDesktop.BackColor = System.Drawing.Color.White;
            this.panelDesktop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelDesktop.Controls.Add(this.tableLayoutPanel1);
            this.panelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop.Location = new System.Drawing.Point(201, 43);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(820, 669);
            this.panelDesktop.TabIndex = 5;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 91.46982F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.530184F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.Controls.Add(this.navibar, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 185);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(720, 88);
            this.tableLayoutPanel1.TabIndex = 8;
            this.tableLayoutPanel1.Visible = false;
            // 
            // navibar
            // 
            this.navibar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navibar.Location = new System.Drawing.Point(3, 3);
            this.navibar.Name = "navibar";
            this.navibar.Size = new System.Drawing.Size(608, 82);
            this.navibar.TabIndex = 1;
            this.navibar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.navibar_MouseDown);
            this.navibar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.navibar_MouseMove);
            // 
            // navibar1
            // 
            this.navibar1.AutoSize = true;
            this.navibar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navibar1.Location = new System.Drawing.Point(13, 3);
            this.navibar1.Name = "navibar1";
            this.navibar1.Size = new System.Drawing.Size(182, 34);
            this.navibar1.TabIndex = 9;
            this.navibar1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.navibar_MouseDown);
            this.navibar1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.navibar_MouseMove);
            // 
            // pnlappcolor
            // 
            this.pnlappcolor.Controls.Add(this.btnappok);
            this.pnlappcolor.Controls.Add(this.btnappcolse);
            this.pnlappcolor.Controls.Add(this.txtcolorcode);
            this.pnlappcolor.Controls.Add(this.chkentercolor);
            this.pnlappcolor.Controls.Add(this.pbappcoloepicker);
            this.pnlappcolor.Controls.Add(this.lblappcolor);
            this.pnlappcolor.Location = new System.Drawing.Point(425, 90);
            this.pnlappcolor.Name = "pnlappcolor";
            this.pnlappcolor.Size = new System.Drawing.Size(326, 173);
            this.pnlappcolor.TabIndex = 0;
            this.pnlappcolor.Visible = false;
            // 
            // btnappok
            // 
            this.btnappok.BackColor = System.Drawing.Color.Silver;
            this.btnappok.FlatAppearance.BorderSize = 0;
            this.btnappok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnappok.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnappok.ForeColor = System.Drawing.Color.Red;
            this.btnappok.Location = new System.Drawing.Point(138, 123);
            this.btnappok.Name = "btnappok";
            this.btnappok.Size = new System.Drawing.Size(63, 37);
            this.btnappok.TabIndex = 8;
            this.btnappok.Text = "Ok";
            this.btnappok.UseVisualStyleBackColor = false;
            this.btnappok.Visible = false;
            this.btnappok.Click += new System.EventHandler(this.btnappok_Click);
            // 
            // btnappcolse
            // 
            this.btnappcolse.BackColor = System.Drawing.Color.White;
            this.btnappcolse.FlatAppearance.BorderSize = 0;
            this.btnappcolse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnappcolse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnappcolse.ForeColor = System.Drawing.Color.Red;
            this.btnappcolse.Location = new System.Drawing.Point(293, 0);
            this.btnappcolse.Name = "btnappcolse";
            this.btnappcolse.Size = new System.Drawing.Size(30, 32);
            this.btnappcolse.TabIndex = 7;
            this.btnappcolse.Text = "X";
            this.btnappcolse.UseVisualStyleBackColor = false;
            this.btnappcolse.Click += new System.EventHandler(this.btnappcolse_Click);
            // 
            // txtcolorcode
            // 
            this.txtcolorcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcolorcode.Location = new System.Drawing.Point(172, 83);
            this.txtcolorcode.Name = "txtcolorcode";
            this.txtcolorcode.Size = new System.Drawing.Size(142, 26);
            this.txtcolorcode.TabIndex = 3;
            this.txtcolorcode.Visible = false;
            // 
            // chkentercolor
            // 
            this.chkentercolor.AutoSize = true;
            this.chkentercolor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.chkentercolor.Location = new System.Drawing.Point(21, 88);
            this.chkentercolor.Name = "chkentercolor";
            this.chkentercolor.Size = new System.Drawing.Size(145, 20);
            this.chkentercolor.TabIndex = 2;
            this.chkentercolor.Text = "Enter Color Code";
            this.chkentercolor.UseVisualStyleBackColor = true;
            this.chkentercolor.CheckedChanged += new System.EventHandler(this.chkentercolor_CheckedChanged);
            // 
            // pbappcoloepicker
            // 
            this.pbappcoloepicker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbappcoloepicker.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbappcoloepicker.Location = new System.Drawing.Point(151, 14);
            this.pbappcoloepicker.Name = "pbappcoloepicker";
            this.pbappcoloepicker.Size = new System.Drawing.Size(100, 50);
            this.pbappcoloepicker.TabIndex = 1;
            this.pbappcoloepicker.TabStop = false;
            this.pbappcoloepicker.Click += new System.EventHandler(this.pbappcoloepicker_Click);
            // 
            // lblappcolor
            // 
            this.lblappcolor.AutoSize = true;
            this.lblappcolor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblappcolor.Location = new System.Drawing.Point(18, 29);
            this.lblappcolor.Name = "lblappcolor";
            this.lblappcolor.Size = new System.Drawing.Size(127, 16);
            this.lblappcolor.TabIndex = 0;
            this.lblappcolor.Text = "Application Color";
            // 
            // tlp_main
            // 
            this.tlp_main.BackColor = System.Drawing.Color.Gray;
            this.tlp_main.ColumnCount = 3;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_main.Controls.Add(this.flp_time, 2, 2);
            this.tlp_main.Controls.Add(this.navibar1, 1, 0);
            this.tlp_main.Controls.Add(this.flowLayoutPanel2, 2, 0);
            this.tlp_main.Controls.Add(this.panelMenu, 1, 1);
            this.tlp_main.Controls.Add(this.panelDesktop, 2, 1);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 3;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 101F));
            this.tlp_main.Size = new System.Drawing.Size(1024, 748);
            this.tlp_main.TabIndex = 6;
            // 
            // flp_time
            // 
            this.flp_time.Controls.Add(this.lblTime);
            this.flp_time.Controls.Add(this.datelbl);
            this.flp_time.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_time.Location = new System.Drawing.Point(201, 718);
            this.flp_time.Name = "flp_time";
            this.flp_time.Size = new System.Drawing.Size(820, 95);
            this.flp_time.TabIndex = 7;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(3, 0);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(45, 21);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "time";
            // 
            // datelbl
            // 
            this.datelbl.AutoSize = true;
            this.datelbl.Dock = System.Windows.Forms.DockStyle.Left;
            this.datelbl.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datelbl.ForeColor = System.Drawing.Color.White;
            this.datelbl.Location = new System.Drawing.Point(54, 0);
            this.datelbl.Name = "datelbl";
            this.datelbl.Size = new System.Drawing.Size(46, 21);
            this.datelbl.TabIndex = 3;
            this.datelbl.Text = "Date";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnclose);
            this.flowLayoutPanel2.Controls.Add(this.btnmini);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(201, 5);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(820, 32);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.White;
            this.btnclose.FlatAppearance.BorderSize = 0;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.Red;
            this.btnclose.Location = new System.Drawing.Point(795, 3);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(22, 23);
            this.btnclose.TabIndex = 6;
            this.btnclose.Text = "X";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(767, 3);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(22, 23);
            this.btnmini.TabIndex = 8;
            this.btnmini.Text = "-";
            this.btnmini.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnmini.UseVisualStyleBackColor = false;
            this.btnmini.Click += new System.EventHandler(this.btnmini_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tlp_tablestatus
            // 
            this.tlp_tablestatus.ColumnCount = 4;
            this.tlp_tablestatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlp_tablestatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.24028F));
            this.tlp_tablestatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.57895F));
            this.tlp_tablestatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 128F));
            this.tlp_tablestatus.Controls.Add(this.btntrefresh, 0, 0);
            this.tlp_tablestatus.Controls.Add(this.btn_avial, 1, 0);
            this.tlp_tablestatus.Controls.Add(this.btn_rev, 3, 0);
            this.tlp_tablestatus.Controls.Add(this.btn_running, 2, 0);
            this.tlp_tablestatus.Location = new System.Drawing.Point(455, 71);
            this.tlp_tablestatus.Name = "tlp_tablestatus";
            this.tlp_tablestatus.RowCount = 1;
            this.tlp_tablestatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_tablestatus.Size = new System.Drawing.Size(437, 51);
            this.tlp_tablestatus.TabIndex = 0;
            this.tlp_tablestatus.Visible = false;
            // 
            // btntrefresh
            // 
            this.btntrefresh.BackgroundImage = global::FoodandBeverage.Properties.Resources.refresh_button;
            this.btntrefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btntrefresh.FlatAppearance.BorderSize = 0;
            this.btntrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btntrefresh.Location = new System.Drawing.Point(3, 3);
            this.btntrefresh.Name = "btntrefresh";
            this.btntrefresh.Size = new System.Drawing.Size(52, 42);
            this.btntrefresh.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btntrefresh, "Refresh Dashboard");
            this.btntrefresh.UseVisualStyleBackColor = true;
            this.btntrefresh.Click += new System.EventHandler(this.btntrefresh_Click);
            // 
            // btn_avial
            // 
            this.btn_avial.BackgroundImage = global::FoodandBeverage.Properties.Resources.available;
            this.btn_avial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_avial.FlatAppearance.BorderSize = 0;
            this.btn_avial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_avial.Location = new System.Drawing.Point(105, 3);
            this.btn_avial.Name = "btn_avial";
            this.btn_avial.Size = new System.Drawing.Size(52, 42);
            this.btn_avial.TabIndex = 0;
            this.toolTip1.SetToolTip(this.btn_avial, "Available Table");
            this.btn_avial.UseVisualStyleBackColor = true;
            this.btn_avial.Click += new System.EventHandler(this.btn_avial_Click);
            // 
            // btn_rev
            // 
            this.btn_rev.BackgroundImage = global::FoodandBeverage.Properties.Resources.reserve;
            this.btn_rev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_rev.FlatAppearance.BorderSize = 0;
            this.btn_rev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_rev.Location = new System.Drawing.Point(310, 3);
            this.btn_rev.Name = "btn_rev";
            this.btn_rev.Size = new System.Drawing.Size(52, 42);
            this.btn_rev.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btn_rev, "Reservation Table");
            this.btn_rev.UseVisualStyleBackColor = true;
            this.btn_rev.Click += new System.EventHandler(this.btn_rev_Click);
            // 
            // btn_running
            // 
            this.btn_running.BackgroundImage = global::FoodandBeverage.Properties.Resources.time;
            this.btn_running.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_running.FlatAppearance.BorderSize = 0;
            this.btn_running.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_running.Location = new System.Drawing.Point(213, 3);
            this.btn_running.Name = "btn_running";
            this.btn_running.Size = new System.Drawing.Size(52, 42);
            this.btn_running.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btn_running, "currently Running Table");
            this.btn_running.UseVisualStyleBackColor = true;
            this.btn_running.Click += new System.EventHandler(this.btn_running_Click);
            // 
            // flp_operations
            // 
            this.flp_operations.Controls.Add(this.ib_Category);
            this.flp_operations.Controls.Add(this.ib_Item);
            this.flp_operations.Controls.Add(this.ib_ingrediant);
            this.flp_operations.Controls.Add(this.ib_menu);
            this.flp_operations.Controls.Add(this.ib_unit);
            this.flp_operations.Controls.Add(this.ib_table);
            this.flp_operations.Controls.Add(this.ib_purchase);
            this.flp_operations.Controls.Add(this.ib_purchasereturn);
            this.flp_operations.Controls.Add(this.ib_tax);
            this.flp_operations.Location = new System.Drawing.Point(202, 151);
            this.flp_operations.Name = "flp_operations";
            this.flp_operations.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.flp_operations.Size = new System.Drawing.Size(788, 326);
            this.flp_operations.TabIndex = 9;
            this.flp_operations.Visible = false;
            // 
            // ib_Category
            // 
            this.ib_Category.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ib_Category.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ib_Category.FlatAppearance.BorderSize = 0;
            this.ib_Category.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ib_Category.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ib_Category.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ib_Category.IconChar = FontAwesome.Sharp.IconChar.XRay;
            this.ib_Category.IconColor = System.Drawing.Color.White;
            this.ib_Category.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ib_Category.IconSize = 30;
            this.ib_Category.Location = new System.Drawing.Point(30, 10);
            this.ib_Category.Margin = new System.Windows.Forms.Padding(10, 10, 10, 2);
            this.ib_Category.Name = "ib_Category";
            this.ib_Category.Size = new System.Drawing.Size(89, 72);
            this.ib_Category.TabIndex = 4;
            this.ib_Category.Tag = "Category";
            this.ib_Category.Text = "Category";
            this.ib_Category.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.ib_Category, "Category Registartion");
            this.ib_Category.UseVisualStyleBackColor = false;
            this.ib_Category.Click += new System.EventHandler(this.ib_Category_Click);
            this.ib_Category.MouseLeave += new System.EventHandler(this.ib_Category_MouseLeave);
            this.ib_Category.MouseHover += new System.EventHandler(this.ib_Category_MouseHover);
            // 
            // ib_Item
            // 
            this.ib_Item.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ib_Item.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ib_Item.FlatAppearance.BorderSize = 0;
            this.ib_Item.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ib_Item.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ib_Item.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ib_Item.IconChar = FontAwesome.Sharp.IconChar.XRay;
            this.ib_Item.IconColor = System.Drawing.Color.White;
            this.ib_Item.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ib_Item.IconSize = 30;
            this.ib_Item.Location = new System.Drawing.Point(139, 10);
            this.ib_Item.Margin = new System.Windows.Forms.Padding(10, 10, 10, 2);
            this.ib_Item.Name = "ib_Item";
            this.ib_Item.Size = new System.Drawing.Size(89, 72);
            this.ib_Item.TabIndex = 5;
            this.ib_Item.Tag = "Item";
            this.ib_Item.Text = "Item";
            this.ib_Item.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.ib_Item, "Item Registartion");
            this.ib_Item.UseVisualStyleBackColor = false;
            this.ib_Item.Click += new System.EventHandler(this.ib_Item_Click);
            this.ib_Item.MouseLeave += new System.EventHandler(this.ib_Item_MouseLeave);
            this.ib_Item.MouseHover += new System.EventHandler(this.ib_Category_MouseHover);
            // 
            // ib_ingrediant
            // 
            this.ib_ingrediant.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ib_ingrediant.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ib_ingrediant.FlatAppearance.BorderSize = 0;
            this.ib_ingrediant.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ib_ingrediant.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ib_ingrediant.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ib_ingrediant.IconChar = FontAwesome.Sharp.IconChar.XRay;
            this.ib_ingrediant.IconColor = System.Drawing.Color.White;
            this.ib_ingrediant.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ib_ingrediant.IconSize = 30;
            this.ib_ingrediant.Location = new System.Drawing.Point(248, 10);
            this.ib_ingrediant.Margin = new System.Windows.Forms.Padding(10, 10, 10, 2);
            this.ib_ingrediant.Name = "ib_ingrediant";
            this.ib_ingrediant.Size = new System.Drawing.Size(89, 72);
            this.ib_ingrediant.TabIndex = 6;
            this.ib_ingrediant.Tag = "Product";
            this.ib_ingrediant.Text = "Product";
            this.ib_ingrediant.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.ib_ingrediant, "Product Registartion");
            this.ib_ingrediant.UseVisualStyleBackColor = false;
            this.ib_ingrediant.Click += new System.EventHandler(this.ib_ingrediant_Click);
            this.ib_ingrediant.MouseLeave += new System.EventHandler(this.ib_ingrediant_MouseLeave);
            this.ib_ingrediant.MouseHover += new System.EventHandler(this.ib_Category_MouseHover);
            // 
            // ib_menu
            // 
            this.ib_menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ib_menu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ib_menu.FlatAppearance.BorderSize = 0;
            this.ib_menu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ib_menu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ib_menu.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ib_menu.IconChar = FontAwesome.Sharp.IconChar.XRay;
            this.ib_menu.IconColor = System.Drawing.Color.White;
            this.ib_menu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ib_menu.IconSize = 30;
            this.ib_menu.Location = new System.Drawing.Point(357, 10);
            this.ib_menu.Margin = new System.Windows.Forms.Padding(10, 10, 10, 2);
            this.ib_menu.Name = "ib_menu";
            this.ib_menu.Size = new System.Drawing.Size(89, 72);
            this.ib_menu.TabIndex = 7;
            this.ib_menu.Tag = "Menu";
            this.ib_menu.Text = "Menu";
            this.ib_menu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.ib_menu, "Menu Registartion");
            this.ib_menu.UseVisualStyleBackColor = false;
            this.ib_menu.Click += new System.EventHandler(this.ib_menu_Click);
            this.ib_menu.MouseLeave += new System.EventHandler(this.ib_menu_MouseLeave);
            this.ib_menu.MouseHover += new System.EventHandler(this.ib_Category_MouseHover);
            // 
            // ib_unit
            // 
            this.ib_unit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ib_unit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ib_unit.FlatAppearance.BorderSize = 0;
            this.ib_unit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ib_unit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ib_unit.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ib_unit.IconChar = FontAwesome.Sharp.IconChar.XRay;
            this.ib_unit.IconColor = System.Drawing.Color.White;
            this.ib_unit.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ib_unit.IconSize = 30;
            this.ib_unit.Location = new System.Drawing.Point(466, 10);
            this.ib_unit.Margin = new System.Windows.Forms.Padding(10, 10, 10, 2);
            this.ib_unit.Name = "ib_unit";
            this.ib_unit.Size = new System.Drawing.Size(89, 72);
            this.ib_unit.TabIndex = 8;
            this.ib_unit.Tag = "Unit";
            this.ib_unit.Text = "Unit";
            this.ib_unit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.ib_unit, "Unit Registartion");
            this.ib_unit.UseVisualStyleBackColor = false;
            this.ib_unit.Click += new System.EventHandler(this.ib_unit_Click);
            this.ib_unit.MouseLeave += new System.EventHandler(this.ib_unit_MouseLeave);
            this.ib_unit.MouseHover += new System.EventHandler(this.ib_Category_MouseHover);
            // 
            // ib_table
            // 
            this.ib_table.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ib_table.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ib_table.FlatAppearance.BorderSize = 0;
            this.ib_table.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ib_table.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ib_table.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ib_table.IconChar = FontAwesome.Sharp.IconChar.XRay;
            this.ib_table.IconColor = System.Drawing.Color.White;
            this.ib_table.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ib_table.IconSize = 30;
            this.ib_table.Location = new System.Drawing.Point(575, 10);
            this.ib_table.Margin = new System.Windows.Forms.Padding(10, 10, 10, 2);
            this.ib_table.Name = "ib_table";
            this.ib_table.Size = new System.Drawing.Size(89, 72);
            this.ib_table.TabIndex = 9;
            this.ib_table.Tag = "Table";
            this.ib_table.Text = "Table";
            this.ib_table.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.ib_table, "Table Registartion");
            this.ib_table.UseVisualStyleBackColor = false;
            this.ib_table.Click += new System.EventHandler(this.ib_table_Click);
            this.ib_table.MouseLeave += new System.EventHandler(this.ib_table_MouseLeave);
            this.ib_table.MouseHover += new System.EventHandler(this.ib_Category_MouseHover);
            // 
            // ib_purchase
            // 
            this.ib_purchase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ib_purchase.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ib_purchase.FlatAppearance.BorderSize = 0;
            this.ib_purchase.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ib_purchase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ib_purchase.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ib_purchase.IconChar = FontAwesome.Sharp.IconChar.XRay;
            this.ib_purchase.IconColor = System.Drawing.Color.White;
            this.ib_purchase.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ib_purchase.IconSize = 30;
            this.ib_purchase.Location = new System.Drawing.Point(684, 10);
            this.ib_purchase.Margin = new System.Windows.Forms.Padding(10, 10, 10, 2);
            this.ib_purchase.Name = "ib_purchase";
            this.ib_purchase.Size = new System.Drawing.Size(89, 72);
            this.ib_purchase.TabIndex = 10;
            this.ib_purchase.Tag = "Purchase";
            this.ib_purchase.Text = "Purchase";
            this.ib_purchase.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.ib_purchase, "Purchase Registartion");
            this.ib_purchase.UseVisualStyleBackColor = false;
            this.ib_purchase.Click += new System.EventHandler(this.ib_purchase_Click);
            this.ib_purchase.MouseLeave += new System.EventHandler(this.ib_purchase_MouseLeave);
            this.ib_purchase.MouseHover += new System.EventHandler(this.ib_Category_MouseHover);
            // 
            // ib_purchasereturn
            // 
            this.ib_purchasereturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ib_purchasereturn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ib_purchasereturn.FlatAppearance.BorderSize = 0;
            this.ib_purchasereturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ib_purchasereturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ib_purchasereturn.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ib_purchasereturn.IconChar = FontAwesome.Sharp.IconChar.XRay;
            this.ib_purchasereturn.IconColor = System.Drawing.Color.White;
            this.ib_purchasereturn.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ib_purchasereturn.IconSize = 30;
            this.ib_purchasereturn.Location = new System.Drawing.Point(30, 94);
            this.ib_purchasereturn.Margin = new System.Windows.Forms.Padding(10, 10, 10, 2);
            this.ib_purchasereturn.Name = "ib_purchasereturn";
            this.ib_purchasereturn.Size = new System.Drawing.Size(89, 72);
            this.ib_purchasereturn.TabIndex = 11;
            this.ib_purchasereturn.Tag = "Purchase Return";
            this.ib_purchasereturn.Text = "Purchase Return";
            this.ib_purchasereturn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.ib_purchasereturn, "Purchase Return Registartion");
            this.ib_purchasereturn.UseVisualStyleBackColor = false;
            this.ib_purchasereturn.Click += new System.EventHandler(this.ib_purchasereturn_Click);
            this.ib_purchasereturn.MouseLeave += new System.EventHandler(this.ib_purchasereturn_MouseLeave);
            this.ib_purchasereturn.MouseHover += new System.EventHandler(this.ib_Category_MouseHover);
            // 
            // ib_tax
            // 
            this.ib_tax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ib_tax.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ib_tax.FlatAppearance.BorderSize = 0;
            this.ib_tax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ib_tax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ib_tax.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ib_tax.IconChar = FontAwesome.Sharp.IconChar.XRay;
            this.ib_tax.IconColor = System.Drawing.Color.White;
            this.ib_tax.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.ib_tax.IconSize = 30;
            this.ib_tax.Location = new System.Drawing.Point(139, 94);
            this.ib_tax.Margin = new System.Windows.Forms.Padding(10, 10, 10, 2);
            this.ib_tax.Name = "ib_tax";
            this.ib_tax.Size = new System.Drawing.Size(89, 72);
            this.ib_tax.TabIndex = 12;
            this.ib_tax.Tag = "Tax";
            this.ib_tax.Text = "Tax";
            this.ib_tax.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.toolTip1.SetToolTip(this.ib_tax, "Tax Registartion");
            this.ib_tax.UseVisualStyleBackColor = false;
            this.ib_tax.Click += new System.EventHandler(this.ib_tax_Click);
            this.ib_tax.MouseLeave += new System.EventHandler(this.ib_tax_MouseLeave);
            this.ib_tax.MouseHover += new System.EventHandler(this.ib_Category_MouseHover);
            // 
            // FrmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 748);
            this.Controls.Add(this.flp_operations);
            this.Controls.Add(this.tlp_tablestatus);
            this.Controls.Add(this.pnlappcolor);
            this.Controls.Add(this.tlp_main);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmDashboard";
            this.Load += new System.EventHandler(this.FrmDashboard_Load);
            this.panelMenu.ResumeLayout(false);
            this.flp_menu.ResumeLayout(false);
            this.pnl_header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_appicon)).EndInit();
            this.panelDesktop.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlappcolor.ResumeLayout(false);
            this.pnlappcolor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbappcoloepicker)).EndInit();
            this.tlp_main.ResumeLayout(false);
            this.tlp_main.PerformLayout();
            this.flp_time.ResumeLayout(false);
            this.flp_time.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.tlp_tablestatus.ResumeLayout(false);
            this.flp_operations.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel pnl_header;
        private System.Windows.Forms.FlowLayoutPanel flp_menu;
        private System.Windows.Forms.Panel panelDesktop;
        private FontAwesome.Sharp.IconButton Menuoperation;
        private FontAwesome.Sharp.IconButton Menubilling;
        private FontAwesome.Sharp.IconButton Menureport;
        private FontAwesome.Sharp.IconButton Menusetting;
        private FontAwesome.Sharp.IconButton Menulogout;
        private FontAwesome.Sharp.IconButton Menuhome;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.FlowLayoutPanel flp_time;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label datelbl;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel navibar;
        private System.Windows.Forms.Panel navibar1;
        private System.Windows.Forms.Panel pnlappcolor;
        private System.Windows.Forms.CheckBox chkentercolor;
        private System.Windows.Forms.PictureBox pbappcoloepicker;
        private System.Windows.Forms.Label lblappcolor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.TextBox txtcolorcode;
        private System.Windows.Forms.Button btnappcolse;
        private System.Windows.Forms.Button btnappok;
        private System.Windows.Forms.TableLayoutPanel tlp_tablestatus;
        private System.Windows.Forms.Button btn_running;
        private System.Windows.Forms.Button btn_rev;
        private System.Windows.Forms.Button btn_avial;
        private System.Windows.Forms.Button btntrefresh;
        private System.Windows.Forms.PictureBox pb_appicon;
        private System.Windows.Forms.FlowLayoutPanel flp_operations;
        private FontAwesome.Sharp.IconButton ib_Category;
        private FontAwesome.Sharp.IconButton ib_Item;
        private FontAwesome.Sharp.IconButton ib_ingrediant;
        private FontAwesome.Sharp.IconButton ib_menu;
        private FontAwesome.Sharp.IconButton ib_unit;
        private FontAwesome.Sharp.IconButton ib_table;
        private FontAwesome.Sharp.IconButton ib_purchase;
        private FontAwesome.Sharp.IconButton ib_purchasereturn;
        private FontAwesome.Sharp.IconButton ib_tax;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}