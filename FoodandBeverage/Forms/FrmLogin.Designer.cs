
namespace FoodandBeverage.Forms
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tlp_mainouterdesign = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_right = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pblogo = new System.Windows.Forms.PictureBox();
            this.lblcname2 = new System.Windows.Forms.Label();
            this.lblcname = new System.Windows.Forms.Label();
            this.pnlloginbox = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.btnrfid = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.btnlogin = new System.Windows.Forms.Button();
            this.pbleftdesign = new System.Windows.Forms.PictureBox();
            this.Pnl_wifiPopup = new System.Windows.Forms.Panel();
            this.tlp_wifiPopup = new System.Windows.Forms.TableLayoutPanel();
            this.pbwifi = new System.Windows.Forms.PictureBox();
            this.btnwificancel = new System.Windows.Forms.Button();
            this.pnlDbSettings = new System.Windows.Forms.Panel();
            this.btndbcls = new System.Windows.Forms.Button();
            this.btnsavedbconfig = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btntstconn = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.txtnew_conn = new System.Windows.Forms.TextBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.txtext_conn = new System.Windows.Forms.TextBox();
            this.longPressTimer = new System.Windows.Forms.Timer(this.components);
            this.pnlDBLogin = new System.Windows.Forms.Panel();
            this.btndblogin_close = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.btndbconfiglogin = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtpwddb = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtusernamedb = new System.Windows.Forms.TextBox();
            this.tlp_mainouterdesign.SuspendLayout();
            this.tlp_right.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pblogo)).BeginInit();
            this.pnlloginbox.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbleftdesign)).BeginInit();
            this.Pnl_wifiPopup.SuspendLayout();
            this.tlp_wifiPopup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbwifi)).BeginInit();
            this.pnlDbSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.pnlDBLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tlp_mainouterdesign
            // 
            this.tlp_mainouterdesign.BackColor = System.Drawing.Color.Transparent;
            this.tlp_mainouterdesign.ColumnCount = 2;
            this.tlp_mainouterdesign.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 53.03266F));
            this.tlp_mainouterdesign.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 46.96734F));
            this.tlp_mainouterdesign.Controls.Add(this.tlp_right, 1, 0);
            this.tlp_mainouterdesign.Controls.Add(this.pbleftdesign, 0, 0);
            this.tlp_mainouterdesign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_mainouterdesign.Location = new System.Drawing.Point(0, 0);
            this.tlp_mainouterdesign.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_mainouterdesign.Name = "tlp_mainouterdesign";
            this.tlp_mainouterdesign.RowCount = 1;
            this.tlp_mainouterdesign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_mainouterdesign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 714F));
            this.tlp_mainouterdesign.Size = new System.Drawing.Size(1279, 714);
            this.tlp_mainouterdesign.TabIndex = 7;
            // 
            // tlp_right
            // 
            this.tlp_right.BackgroundImage = global::FoodandBeverage.Properties.Resources.logopbright;
            this.tlp_right.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tlp_right.ColumnCount = 1;
            this.tlp_right.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_right.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlp_right.Controls.Add(this.pnlloginbox, 0, 1);
            this.tlp_right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_right.Location = new System.Drawing.Point(678, 0);
            this.tlp_right.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_right.Name = "tlp_right";
            this.tlp_right.RowCount = 2;
            this.tlp_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.37037F));
            this.tlp_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.62963F));
            this.tlp_right.Size = new System.Drawing.Size(601, 714);
            this.tlp_right.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblcname2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblcname, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.83721F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.16279F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(595, 212);
            this.tableLayoutPanel1.TabIndex = 261;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.pblogo, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(589, 125);
            this.tableLayoutPanel2.TabIndex = 260;
            // 
            // pblogo
            // 
            this.pblogo.BackgroundImage = global::FoodandBeverage.Properties.Resources.midas_logo;
            this.pblogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pblogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pblogo.Location = new System.Drawing.Point(232, 2);
            this.pblogo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pblogo.Name = "pblogo";
            this.pblogo.Size = new System.Drawing.Size(125, 121);
            this.pblogo.TabIndex = 258;
            this.pblogo.TabStop = false;
            this.pblogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pblogo_MouseDown);
            this.pblogo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pblogo_MouseUp);
            // 
            // lblcname2
            // 
            this.lblcname2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcname2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcname2.Location = new System.Drawing.Point(3, 174);
            this.lblcname2.Name = "lblcname2";
            this.lblcname2.Size = new System.Drawing.Size(589, 38);
            this.lblcname2.TabIndex = 260;
            this.lblcname2.Text = "RESTURANT MANAGEMENT";
            this.lblcname2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblcname
            // 
            this.lblcname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcname.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcname.Location = new System.Drawing.Point(3, 129);
            this.lblcname.Name = "lblcname";
            this.lblcname.Size = new System.Drawing.Size(589, 45);
            this.lblcname.TabIndex = 259;
            this.lblcname.Text = "MIDAS FUN";
            this.lblcname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlloginbox
            // 
            this.pnlloginbox.Controls.Add(this.panel2);
            this.pnlloginbox.Controls.Add(this.panel1);
            this.pnlloginbox.Controls.Add(this.btnrfid);
            this.pnlloginbox.Controls.Add(this.btncancel);
            this.pnlloginbox.Controls.Add(this.btnlogin);
            this.pnlloginbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlloginbox.Location = new System.Drawing.Point(0, 216);
            this.pnlloginbox.Margin = new System.Windows.Forms.Padding(0);
            this.pnlloginbox.Name = "pnlloginbox";
            this.pnlloginbox.Size = new System.Drawing.Size(601, 498);
            this.pnlloginbox.TabIndex = 0;
            this.pnlloginbox.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlloginbox_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtPwd);
            this.panel2.Location = new System.Drawing.Point(147, 174);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(319, 49);
            this.panel2.TabIndex = 259;
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.Color.White;
            this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPwd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPwd.ForeColor = System.Drawing.Color.DarkGray;
            this.txtPwd.Location = new System.Drawing.Point(0, 0);
            this.txtPwd.Margin = new System.Windows.Forms.Padding(4);
            this.txtPwd.Multiline = true;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(317, 47);
            this.txtPwd.TabIndex = 1;
            this.txtPwd.Text = "123";
            this.txtPwd.Click += new System.EventHandler(this.txtPwd_Click);
            this.txtPwd.TextChanged += new System.EventHandler(this.txtPwd_TextChanged);
            this.txtPwd.Enter += new System.EventHandler(this.txtPwd_Enter);
            this.txtPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPwd_KeyDown);
            this.txtPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPwd_KeyPress);
            this.txtPwd.Leave += new System.EventHandler(this.txtPwd_Leave);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtUserName);
            this.panel1.Location = new System.Drawing.Point(147, 94);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 49);
            this.panel1.TabIndex = 258;
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.White;
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.ForeColor = System.Drawing.Color.DarkGray;
            this.txtUserName.Location = new System.Drawing.Point(0, 0);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(0);
            this.txtUserName.Multiline = true;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(317, 47);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.Tag = "";
            this.txtUserName.Text = "admin";
            this.txtUserName.Click += new System.EventHandler(this.txtUserName_Click);
            this.txtUserName.Enter += new System.EventHandler(this.txtusername_Enter);
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            this.txtUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserName_KeyPress);
            this.txtUserName.Leave += new System.EventHandler(this.txtusername_Leave);
            // 
            // btnrfid
            // 
            this.btnrfid.BackColor = System.Drawing.Color.Transparent;
            this.btnrfid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnrfid.BackgroundImage")));
            this.btnrfid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrfid.FlatAppearance.BorderSize = 0;
            this.btnrfid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrfid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrfid.Location = new System.Drawing.Point(471, 101);
            this.btnrfid.Margin = new System.Windows.Forms.Padding(4);
            this.btnrfid.Name = "btnrfid";
            this.btnrfid.Size = new System.Drawing.Size(37, 37);
            this.btnrfid.TabIndex = 257;
            this.btnrfid.UseVisualStyleBackColor = false;
            this.btnrfid.Click += new System.EventHandler(this.btnrfid_Click_1);
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.Color.Transparent;
            this.btncancel.BackgroundImage = global::FoodandBeverage.Properties.Resources.Logincancel;
            this.btncancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btncancel.FlatAppearance.BorderSize = 0;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.Location = new System.Drawing.Point(147, 262);
            this.btncancel.Margin = new System.Windows.Forms.Padding(4);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(152, 55);
            this.btncancel.TabIndex = 3;
            this.btncancel.Text = "Cancel";
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnlogin
            // 
            this.btnlogin.BackColor = System.Drawing.Color.Transparent;
            this.btnlogin.BackgroundImage = global::FoodandBeverage.Properties.Resources.Loginokay;
            this.btnlogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnlogin.FlatAppearance.BorderSize = 0;
            this.btnlogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlogin.Location = new System.Drawing.Point(320, 262);
            this.btnlogin.Margin = new System.Windows.Forms.Padding(4);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(152, 55);
            this.btnlogin.TabIndex = 2;
            this.btnlogin.Text = "Login";
            this.btnlogin.UseVisualStyleBackColor = false;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // pbleftdesign
            // 
            this.pbleftdesign.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pbleftdesign.BackgroundImage")));
            this.pbleftdesign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbleftdesign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbleftdesign.Location = new System.Drawing.Point(0, 0);
            this.pbleftdesign.Margin = new System.Windows.Forms.Padding(0);
            this.pbleftdesign.Name = "pbleftdesign";
            this.pbleftdesign.Size = new System.Drawing.Size(678, 714);
            this.pbleftdesign.TabIndex = 11;
            this.pbleftdesign.TabStop = false;
            // 
            // Pnl_wifiPopup
            // 
            this.Pnl_wifiPopup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(152)))), ((int)(((byte)(227)))));
            this.Pnl_wifiPopup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Pnl_wifiPopup.Controls.Add(this.tlp_wifiPopup);
            this.Pnl_wifiPopup.Location = new System.Drawing.Point(1315, 140);
            this.Pnl_wifiPopup.Margin = new System.Windows.Forms.Padding(4);
            this.Pnl_wifiPopup.Name = "Pnl_wifiPopup";
            this.Pnl_wifiPopup.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.Pnl_wifiPopup.Size = new System.Drawing.Size(476, 373);
            this.Pnl_wifiPopup.TabIndex = 279;
            this.Pnl_wifiPopup.Visible = false;
            this.Pnl_wifiPopup.Paint += new System.Windows.Forms.PaintEventHandler(this.Pnl_wifiPopup_Paint);
            // 
            // tlp_wifiPopup
            // 
            this.tlp_wifiPopup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(152)))), ((int)(((byte)(227)))));
            this.tlp_wifiPopup.ColumnCount = 1;
            this.tlp_wifiPopup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.25191F));
            this.tlp_wifiPopup.Controls.Add(this.pbwifi, 0, 1);
            this.tlp_wifiPopup.Controls.Add(this.btnwificancel, 0, 0);
            this.tlp_wifiPopup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_wifiPopup.Location = new System.Drawing.Point(13, 12);
            this.tlp_wifiPopup.Margin = new System.Windows.Forms.Padding(4);
            this.tlp_wifiPopup.Name = "tlp_wifiPopup";
            this.tlp_wifiPopup.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tlp_wifiPopup.RowCount = 2;
            this.tlp_wifiPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlp_wifiPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_wifiPopup.Size = new System.Drawing.Size(446, 345);
            this.tlp_wifiPopup.TabIndex = 2;
            // 
            // pbwifi
            // 
            this.pbwifi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbwifi.Image = ((System.Drawing.Image)(resources.GetObject("pbwifi.Image")));
            this.pbwifi.Location = new System.Drawing.Point(11, 60);
            this.pbwifi.Margin = new System.Windows.Forms.Padding(4);
            this.pbwifi.Name = "pbwifi";
            this.pbwifi.Size = new System.Drawing.Size(424, 275);
            this.pbwifi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbwifi.TabIndex = 0;
            this.pbwifi.TabStop = false;
            // 
            // btnwificancel
            // 
            this.btnwificancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnwificancel.BackgroundImage")));
            this.btnwificancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnwificancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnwificancel.FlatAppearance.BorderSize = 0;
            this.btnwificancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnwificancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnwificancel.ForeColor = System.Drawing.Color.Red;
            this.btnwificancel.Location = new System.Drawing.Point(359, 10);
            this.btnwificancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnwificancel.Name = "btnwificancel";
            this.btnwificancel.Size = new System.Drawing.Size(76, 42);
            this.btnwificancel.TabIndex = 1;
            this.btnwificancel.UseVisualStyleBackColor = true;
            this.btnwificancel.Click += new System.EventHandler(this.btnwificancel_Click);
            // 
            // pnlDbSettings
            // 
            this.pnlDbSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(10)))), ((int)(((byte)(5)))), ((int)(((byte)(0)))));
            this.pnlDbSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlDbSettings.Controls.Add(this.btndbcls);
            this.pnlDbSettings.Controls.Add(this.btnsavedbconfig);
            this.pnlDbSettings.Controls.Add(this.label11);
            this.pnlDbSettings.Controls.Add(this.label12);
            this.pnlDbSettings.Controls.Add(this.btntstconn);
            this.pnlDbSettings.Controls.Add(this.label13);
            this.pnlDbSettings.Controls.Add(this.txtnew_conn);
            this.pnlDbSettings.Controls.Add(this.pictureBox5);
            this.pnlDbSettings.Controls.Add(this.txtext_conn);
            this.pnlDbSettings.Location = new System.Drawing.Point(309, 89);
            this.pnlDbSettings.Margin = new System.Windows.Forms.Padding(4);
            this.pnlDbSettings.Name = "pnlDbSettings";
            this.pnlDbSettings.Size = new System.Drawing.Size(523, 524);
            this.pnlDbSettings.TabIndex = 280;
            this.pnlDbSettings.Visible = false;
            // 
            // btndbcls
            // 
            this.btndbcls.BackColor = System.Drawing.Color.Transparent;
            this.btndbcls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btndbcls.FlatAppearance.BorderSize = 0;
            this.btndbcls.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndbcls.Location = new System.Drawing.Point(580, 4);
            this.btndbcls.Margin = new System.Windows.Forms.Padding(4);
            this.btndbcls.Name = "btndbcls";
            this.btndbcls.Size = new System.Drawing.Size(76, 47);
            this.btndbcls.TabIndex = 107;
            this.btndbcls.UseVisualStyleBackColor = false;
            // 
            // btnsavedbconfig
            // 
            this.btnsavedbconfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnsavedbconfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnsavedbconfig.FlatAppearance.BorderSize = 0;
            this.btnsavedbconfig.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnsavedbconfig.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnsavedbconfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsavedbconfig.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsavedbconfig.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnsavedbconfig.Location = new System.Drawing.Point(171, 464);
            this.btnsavedbconfig.Margin = new System.Windows.Forms.Padding(4);
            this.btnsavedbconfig.Name = "btnsavedbconfig";
            this.btnsavedbconfig.Size = new System.Drawing.Size(183, 41);
            this.btnsavedbconfig.TabIndex = 104;
            this.btnsavedbconfig.Text = "Save";
            this.btnsavedbconfig.UseVisualStyleBackColor = false;
            this.btnsavedbconfig.Click += new System.EventHandler(this.btnsavedbconfig_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(45, 258);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(168, 28);
            this.label11.TabIndex = 103;
            this.label11.Text = "New Connection";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(45, 126);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(206, 28);
            this.label12.TabIndex = 102;
            this.label12.Text = "Existing Connection ";
            // 
            // btntstconn
            // 
            this.btntstconn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btntstconn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btntstconn.FlatAppearance.BorderSize = 0;
            this.btntstconn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btntstconn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btntstconn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btntstconn.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntstconn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btntstconn.Location = new System.Drawing.Point(136, 403);
            this.btntstconn.Margin = new System.Windows.Forms.Padding(4);
            this.btntstconn.Name = "btntstconn";
            this.btntstconn.Size = new System.Drawing.Size(253, 41);
            this.btntstconn.TabIndex = 3;
            this.btntstconn.Text = "Test Connection";
            this.btntstconn.UseVisualStyleBackColor = false;
            this.btntstconn.Click += new System.EventHandler(this.btntstconn_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(164, 65);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(240, 37);
            this.label13.TabIndex = 95;
            this.label13.Text = "DB Configuration";
            // 
            // txtnew_conn
            // 
            this.txtnew_conn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnew_conn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnew_conn.Location = new System.Drawing.Point(45, 289);
            this.txtnew_conn.Margin = new System.Windows.Forms.Padding(4);
            this.txtnew_conn.Multiline = true;
            this.txtnew_conn.Name = "txtnew_conn";
            this.txtnew_conn.Size = new System.Drawing.Size(406, 88);
            this.txtnew_conn.TabIndex = 1;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.Location = new System.Drawing.Point(106, 60);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(55, 47);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 5;
            this.pictureBox5.TabStop = false;
            // 
            // txtext_conn
            // 
            this.txtext_conn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtext_conn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtext_conn.Location = new System.Drawing.Point(45, 158);
            this.txtext_conn.Margin = new System.Windows.Forms.Padding(4);
            this.txtext_conn.Multiline = true;
            this.txtext_conn.Name = "txtext_conn";
            this.txtext_conn.Size = new System.Drawing.Size(406, 88);
            this.txtext_conn.TabIndex = 0;
            // 
            // longPressTimer
            // 
            this.longPressTimer.Tick += new System.EventHandler(this.longPressTimer_Tick);
            // 
            // pnlDBLogin
            // 
            this.pnlDBLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(10)))), ((int)(((byte)(5)))), ((int)(((byte)(0)))));
            this.pnlDBLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlDBLogin.Controls.Add(this.btndblogin_close);
            this.pnlDBLogin.Controls.Add(this.label6);
            this.pnlDBLogin.Controls.Add(this.label8);
            this.pnlDBLogin.Controls.Add(this.label9);
            this.pnlDBLogin.Controls.Add(this.btndbconfiglogin);
            this.pnlDBLogin.Controls.Add(this.label10);
            this.pnlDBLogin.Controls.Add(this.txtpwddb);
            this.pnlDBLogin.Controls.Add(this.pictureBox1);
            this.pnlDBLogin.Controls.Add(this.txtusernamedb);
            this.pnlDBLogin.Location = new System.Drawing.Point(401, 88);
            this.pnlDBLogin.Margin = new System.Windows.Forms.Padding(4);
            this.pnlDBLogin.Name = "pnlDBLogin";
            this.pnlDBLogin.Size = new System.Drawing.Size(441, 432);
            this.pnlDBLogin.TabIndex = 281;
            this.pnlDBLogin.Visible = false;
            // 
            // btndblogin_close
            // 
            this.btndblogin_close.BackColor = System.Drawing.Color.Transparent;
            this.btndblogin_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btndblogin_close.FlatAppearance.BorderSize = 0;
            this.btndblogin_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndblogin_close.Location = new System.Drawing.Point(361, 12);
            this.btndblogin_close.Margin = new System.Windows.Forms.Padding(4);
            this.btndblogin_close.Name = "btndblogin_close";
            this.btndblogin_close.Size = new System.Drawing.Size(76, 47);
            this.btndblogin_close.TabIndex = 106;
            this.btndblogin_close.UseVisualStyleBackColor = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Yellow;
            this.label6.Location = new System.Drawing.Point(123, 361);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(224, 25);
            this.label6.TabIndex = 105;
            this.label6.Text = "Authentication  Required";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(45, 175);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(101, 28);
            this.label8.TabIndex = 103;
            this.label8.Text = "Password";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(45, 89);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 28);
            this.label9.TabIndex = 102;
            this.label9.Text = "Login";
            // 
            // btndbconfiglogin
            // 
            this.btndbconfiglogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btndbconfiglogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btndbconfiglogin.FlatAppearance.BorderSize = 0;
            this.btndbconfiglogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btndbconfiglogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btndbconfiglogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndbconfiglogin.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndbconfiglogin.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btndbconfiglogin.Location = new System.Drawing.Point(45, 277);
            this.btndbconfiglogin.Margin = new System.Windows.Forms.Padding(4);
            this.btndbconfiglogin.Name = "btndbconfiglogin";
            this.btndbconfiglogin.Size = new System.Drawing.Size(347, 41);
            this.btndbconfiglogin.TabIndex = 3;
            this.btndbconfiglogin.Text = "LOGIN";
            this.btndbconfiglogin.UseVisualStyleBackColor = false;
            this.btndbconfiglogin.Click += new System.EventHandler(this.btndbconfiglogin_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(200, 26);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 37);
            this.label10.TabIndex = 95;
            this.label10.Text = "Login";
            // 
            // txtpwddb
            // 
            this.txtpwddb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpwddb.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpwddb.Location = new System.Drawing.Point(45, 203);
            this.txtpwddb.Margin = new System.Windows.Forms.Padding(4);
            this.txtpwddb.Name = "txtpwddb";
            this.txtpwddb.PasswordChar = '*';
            this.txtpwddb.Size = new System.Drawing.Size(346, 41);
            this.txtpwddb.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(143, 20);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 47);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // txtusernamedb
            // 
            this.txtusernamedb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtusernamedb.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtusernamedb.Location = new System.Drawing.Point(45, 119);
            this.txtusernamedb.Margin = new System.Windows.Forms.Padding(4);
            this.txtusernamedb.Name = "txtusernamedb";
            this.txtusernamedb.Size = new System.Drawing.Size(346, 41);
            this.txtusernamedb.TabIndex = 0;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1279, 714);
            this.Controls.Add(this.pnlDBLogin);
            this.Controls.Add(this.pnlDbSettings);
            this.Controls.Add(this.Pnl_wifiPopup);
            this.Controls.Add(this.tlp_mainouterdesign);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.Shown += new System.EventHandler(this.FrmLogin_Shown);
            this.tlp_mainouterdesign.ResumeLayout(false);
            this.tlp_right.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pblogo)).EndInit();
            this.pnlloginbox.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbleftdesign)).EndInit();
            this.Pnl_wifiPopup.ResumeLayout(false);
            this.tlp_wifiPopup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbwifi)).EndInit();
            this.pnlDbSettings.ResumeLayout(false);
            this.pnlDbSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.pnlDBLogin.ResumeLayout(false);
            this.pnlDBLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tlp_mainouterdesign;
        private System.Windows.Forms.Panel pnlloginbox;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.TableLayoutPanel tlp_right;
        private System.Windows.Forms.Button btnrfid;
        private System.Windows.Forms.PictureBox pblogo;
        private System.Windows.Forms.Label lblcname2;
        private System.Windows.Forms.Label lblcname;
        private System.Windows.Forms.PictureBox pbleftdesign;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel Pnl_wifiPopup;
        private System.Windows.Forms.TableLayoutPanel tlp_wifiPopup;
        private System.Windows.Forms.PictureBox pbwifi;
        private System.Windows.Forms.Button btnwificancel;
        private System.Windows.Forms.Panel pnlDbSettings;
        private System.Windows.Forms.Button btndbcls;
        private System.Windows.Forms.Button btnsavedbconfig;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btntstconn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtnew_conn;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.TextBox txtext_conn;
        private System.Windows.Forms.Timer longPressTimer;
        private System.Windows.Forms.Panel pnlDBLogin;
        private System.Windows.Forms.Button btndblogin_close;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btndbconfiglogin;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtpwddb;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtusernamedb;
    }
}