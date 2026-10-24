
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
            this.pnlloginbox = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_password = new FBControl.ModernTextBox();
            this.txt_username = new FBControl.ModernTextBox();
            this.btnrfid = new System.Windows.Forms.Button();
            this.pblogo = new System.Windows.Forms.PictureBox();
            this.btncancel = new System.Windows.Forms.Button();
            this.btnlogin = new System.Windows.Forms.Button();
            this.Pnl_wifiPopup = new System.Windows.Forms.Panel();
            this.tlp_wifiPopup = new System.Windows.Forms.TableLayoutPanel();
            this.pbwifi = new System.Windows.Forms.PictureBox();
            this.btnwificancel = new System.Windows.Forms.Button();
            this.pnlDbSettings = new System.Windows.Forms.Panel();
            this.btn_dbconf_close = new System.Windows.Forms.Button();
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
            this.longpress_midascard = new System.Windows.Forms.Timer(this.components);
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pb_online_offline = new System.Windows.Forms.PictureBox();
            this.lblcompanyname = new System.Windows.Forms.Label();
            this.pnlloginbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pblogo)).BeginInit();
            this.Pnl_wifiPopup.SuspendLayout();
            this.tlp_wifiPopup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbwifi)).BeginInit();
            this.pnlDbSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.pnlDBLogin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_online_offline)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pnlloginbox
            // 
            this.pnlloginbox.BackColor = System.Drawing.Color.Transparent;
            this.pnlloginbox.BackgroundImage = global::FoodandBeverage.Properties.Resources.Loginbox;
            this.pnlloginbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlloginbox.Controls.Add(this.label2);
            this.pnlloginbox.Controls.Add(this.label1);
            this.pnlloginbox.Controls.Add(this.txt_password);
            this.pnlloginbox.Controls.Add(this.txt_username);
            this.pnlloginbox.Controls.Add(this.btnrfid);
            this.pnlloginbox.Controls.Add(this.pblogo);
            this.pnlloginbox.Controls.Add(this.btncancel);
            this.pnlloginbox.Controls.Add(this.btnlogin);
            this.pnlloginbox.Location = new System.Drawing.Point(546, 11);
            this.pnlloginbox.Margin = new System.Windows.Forms.Padding(0);
            this.pnlloginbox.Name = "pnlloginbox";
            this.pnlloginbox.Size = new System.Drawing.Size(384, 474);
            this.pnlloginbox.TabIndex = 0;
            this.pnlloginbox.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlloginbox_Paint);
            // 
            // label2
            // 
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 133);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(325, 37);
            this.label2.TabIndex = 283;
            this.label2.Text = "Sign in to continue to Midasfun";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 107);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(296, 37);
            this.label1.TabIndex = 282;
            this.label1.Text = "Welcome Back";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_password
            // 
            this.txt_password.BackColor = System.Drawing.Color.White;
            this.txt_password.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(211)))), ((int)(((byte)(248)))));
            this.txt_password.FocusBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(45)))), ((int)(((byte)(238)))));
            this.txt_password.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txt_password.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(28)))), ((int)(((byte)(235)))));
            this.txt_password.IconText = "";
            this.txt_password.InputBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txt_password.InputForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(72)))));
            this.txt_password.Location = new System.Drawing.Point(60, 261);
            this.txt_password.MinimumSize = new System.Drawing.Size(150, 38);
            this.txt_password.Name = "txt_password";
            this.txt_password.Padding = new System.Windows.Forms.Padding(2);
            this.txt_password.Placeholder = "Password";
            this.txt_password.Size = new System.Drawing.Size(269, 46);
            this.txt_password.TabIndex = 261;
            this.txt_password.Click += new System.EventHandler(this.txt_password_Click);
            this.txt_password.Enter += new System.EventHandler(this.txt_password_Enter);
            this.txt_password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_password_KeyDown);
            this.txt_password.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_password_KeyPress);
            this.txt_password.Leave += new System.EventHandler(this.txt_password_Leave);
            // 
            // txt_username
            // 
            this.txt_username.BackColor = System.Drawing.Color.White;
            this.txt_username.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(211)))), ((int)(((byte)(248)))));
            this.txt_username.FocusBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(116)))), ((int)(((byte)(45)))), ((int)(((byte)(238)))));
            this.txt_username.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txt_username.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(28)))), ((int)(((byte)(235)))));
            this.txt_username.IconText = "";
            this.txt_username.InputBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.txt_username.InputForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(72)))));
            this.txt_username.Location = new System.Drawing.Point(60, 185);
            this.txt_username.MinimumSize = new System.Drawing.Size(150, 38);
            this.txt_username.Name = "txt_username";
            this.txt_username.Padding = new System.Windows.Forms.Padding(2);
            this.txt_username.Placeholder = "Username";
            this.txt_username.Size = new System.Drawing.Size(269, 46);
            this.txt_username.TabIndex = 260;
            this.txt_username.Click += new System.EventHandler(this.txt_username_Click);
            this.txt_username.Enter += new System.EventHandler(this.txt_username_Enter);
            this.txt_username.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_username_KeyDown);
            this.txt_username.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_username_KeyPress);
            this.txt_username.Leave += new System.EventHandler(this.txt_username_Leave);
            // 
            // btnrfid
            // 
            this.btnrfid.BackColor = System.Drawing.Color.Transparent;
            this.btnrfid.BackgroundImage = global::FoodandBeverage.Properties.Resources.loginrfid;
            this.btnrfid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrfid.FlatAppearance.BorderSize = 0;
            this.btnrfid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrfid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrfid.Location = new System.Drawing.Point(344, 191);
            this.btnrfid.Name = "btnrfid";
            this.btnrfid.Size = new System.Drawing.Size(28, 30);
            this.btnrfid.TabIndex = 257;
            this.btnrfid.UseVisualStyleBackColor = false;
            this.btnrfid.Click += new System.EventHandler(this.btnrfid_Click_1);
            this.btnrfid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnrfid_MouseDown);
            this.btnrfid.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnrfid_MouseUp);
            // 
            // pblogo
            // 
            this.pblogo.BackColor = System.Drawing.Color.Transparent;
            this.pblogo.BackgroundImage = global::FoodandBeverage.Properties.Resources.RestaurantLogo;
            this.pblogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pblogo.Location = new System.Drawing.Point(149, 23);
            this.pblogo.Margin = new System.Windows.Forms.Padding(2);
            this.pblogo.Name = "pblogo";
            this.pblogo.Size = new System.Drawing.Size(90, 79);
            this.pblogo.TabIndex = 258;
            this.pblogo.TabStop = false;
            this.pblogo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pblogo_MouseDown);
            this.pblogo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pblogo_MouseUp);
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.Color.Transparent;
            this.btncancel.BackgroundImage = global::FoodandBeverage.Properties.Resources.Logincancel;
            this.btncancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btncancel.FlatAppearance.BorderSize = 0;
            this.btncancel.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btncancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btncancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncancel.Location = new System.Drawing.Point(112, 394);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(165, 45);
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
            this.btnlogin.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.btnlogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnlogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnlogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnlogin.Location = new System.Drawing.Point(98, 329);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(192, 45);
            this.btnlogin.TabIndex = 2;
            this.btnlogin.Text = "Login";
            this.btnlogin.UseVisualStyleBackColor = false;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // Pnl_wifiPopup
            // 
            this.Pnl_wifiPopup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(152)))), ((int)(((byte)(227)))));
            this.Pnl_wifiPopup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Pnl_wifiPopup.Controls.Add(this.tlp_wifiPopup);
            this.Pnl_wifiPopup.Location = new System.Drawing.Point(70, 23);
            this.Pnl_wifiPopup.Name = "Pnl_wifiPopup";
            this.Pnl_wifiPopup.Padding = new System.Windows.Forms.Padding(10);
            this.Pnl_wifiPopup.Size = new System.Drawing.Size(358, 304);
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
            this.tlp_wifiPopup.Location = new System.Drawing.Point(10, 10);
            this.tlp_wifiPopup.Name = "tlp_wifiPopup";
            this.tlp_wifiPopup.Padding = new System.Windows.Forms.Padding(5);
            this.tlp_wifiPopup.RowCount = 2;
            this.tlp_wifiPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tlp_wifiPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_wifiPopup.Size = new System.Drawing.Size(334, 280);
            this.tlp_wifiPopup.TabIndex = 2;
            // 
            // pbwifi
            // 
            this.pbwifi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbwifi.Image = ((System.Drawing.Image)(resources.GetObject("pbwifi.Image")));
            this.pbwifi.Location = new System.Drawing.Point(8, 49);
            this.pbwifi.Name = "pbwifi";
            this.pbwifi.Size = new System.Drawing.Size(318, 223);
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
            this.btnwificancel.Location = new System.Drawing.Point(269, 8);
            this.btnwificancel.Name = "btnwificancel";
            this.btnwificancel.Size = new System.Drawing.Size(57, 35);
            this.btnwificancel.TabIndex = 1;
            this.btnwificancel.UseVisualStyleBackColor = true;
            this.btnwificancel.Click += new System.EventHandler(this.btnwificancel_Click);
            // 
            // pnlDbSettings
            // 
            this.pnlDbSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(10)))), ((int)(((byte)(5)))), ((int)(((byte)(0)))));
            this.pnlDbSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlDbSettings.Controls.Add(this.btn_dbconf_close);
            this.pnlDbSettings.Controls.Add(this.btndbcls);
            this.pnlDbSettings.Controls.Add(this.btnsavedbconfig);
            this.pnlDbSettings.Controls.Add(this.label11);
            this.pnlDbSettings.Controls.Add(this.label12);
            this.pnlDbSettings.Controls.Add(this.btntstconn);
            this.pnlDbSettings.Controls.Add(this.label13);
            this.pnlDbSettings.Controls.Add(this.txtnew_conn);
            this.pnlDbSettings.Controls.Add(this.pictureBox5);
            this.pnlDbSettings.Controls.Add(this.txtext_conn);
            this.pnlDbSettings.Location = new System.Drawing.Point(980, 72);
            this.pnlDbSettings.Name = "pnlDbSettings";
            this.pnlDbSettings.Size = new System.Drawing.Size(392, 426);
            this.pnlDbSettings.TabIndex = 280;
            this.pnlDbSettings.Visible = false;
            // 
            // btn_dbconf_close
            // 
            this.btn_dbconf_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_dbconf_close.BackgroundImage = global::FoodandBeverage.Properties.Resources.Round_Close;
            this.btn_dbconf_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_dbconf_close.FlatAppearance.BorderSize = 0;
            this.btn_dbconf_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_dbconf_close.Location = new System.Drawing.Point(327, 10);
            this.btn_dbconf_close.Name = "btn_dbconf_close";
            this.btn_dbconf_close.Size = new System.Drawing.Size(57, 38);
            this.btn_dbconf_close.TabIndex = 108;
            this.btn_dbconf_close.UseVisualStyleBackColor = false;
            this.btn_dbconf_close.Click += new System.EventHandler(this.btn_dbconf_close_Click);
            // 
            // btndbcls
            // 
            this.btndbcls.BackColor = System.Drawing.Color.Transparent;
            this.btndbcls.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btndbcls.FlatAppearance.BorderSize = 0;
            this.btndbcls.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndbcls.Location = new System.Drawing.Point(435, 3);
            this.btndbcls.Name = "btndbcls";
            this.btndbcls.Size = new System.Drawing.Size(57, 38);
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
            this.btnsavedbconfig.Location = new System.Drawing.Point(128, 377);
            this.btnsavedbconfig.Name = "btnsavedbconfig";
            this.btnsavedbconfig.Size = new System.Drawing.Size(137, 33);
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
            this.label11.Location = new System.Drawing.Point(34, 210);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(137, 21);
            this.label11.TabIndex = 103;
            this.label11.Text = "New Connection";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(34, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(167, 21);
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
            this.btntstconn.Location = new System.Drawing.Point(102, 327);
            this.btntstconn.Name = "btntstconn";
            this.btntstconn.Size = new System.Drawing.Size(190, 33);
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
            this.label13.Location = new System.Drawing.Point(123, 53);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(185, 30);
            this.label13.TabIndex = 95;
            this.label13.Text = "DB Configuration";
            // 
            // txtnew_conn
            // 
            this.txtnew_conn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtnew_conn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnew_conn.Location = new System.Drawing.Point(15, 235);
            this.txtnew_conn.Multiline = true;
            this.txtnew_conn.Name = "txtnew_conn";
            this.txtnew_conn.Size = new System.Drawing.Size(366, 72);
            this.txtnew_conn.TabIndex = 1;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.Location = new System.Drawing.Point(80, 49);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(41, 38);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 5;
            this.pictureBox5.TabStop = false;
            // 
            // txtext_conn
            // 
            this.txtext_conn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtext_conn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtext_conn.Location = new System.Drawing.Point(15, 128);
            this.txtext_conn.Multiline = true;
            this.txtext_conn.Name = "txtext_conn";
            this.txtext_conn.Size = new System.Drawing.Size(368, 72);
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
            this.pnlDBLogin.Location = new System.Drawing.Point(980, 72);
            this.pnlDBLogin.Name = "pnlDBLogin";
            this.pnlDBLogin.Size = new System.Drawing.Size(271, 351);
            this.pnlDBLogin.TabIndex = 281;
            this.pnlDBLogin.Visible = false;
            // 
            // btndblogin_close
            // 
            this.btndblogin_close.BackColor = System.Drawing.Color.Transparent;
            this.btndblogin_close.BackgroundImage = global::FoodandBeverage.Properties.Resources.Round_Close;
            this.btndblogin_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btndblogin_close.FlatAppearance.BorderSize = 0;
            this.btndblogin_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndblogin_close.Location = new System.Drawing.Point(271, 10);
            this.btndblogin_close.Name = "btndblogin_close";
            this.btndblogin_close.Size = new System.Drawing.Size(57, 38);
            this.btndblogin_close.TabIndex = 106;
            this.btndblogin_close.UseVisualStyleBackColor = false;
            this.btndblogin_close.Click += new System.EventHandler(this.btndblogin_close_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Yellow;
            this.label6.Location = new System.Drawing.Point(92, 293);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(185, 20);
            this.label6.TabIndex = 105;
            this.label6.Text = "Authentication  Required";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(34, 142);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 21);
            this.label8.TabIndex = 103;
            this.label8.Text = "Password";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(34, 72);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 21);
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
            this.btndbconfiglogin.Location = new System.Drawing.Point(34, 225);
            this.btndbconfiglogin.Name = "btndbconfiglogin";
            this.btndbconfiglogin.Size = new System.Drawing.Size(260, 33);
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
            this.label10.Location = new System.Drawing.Point(150, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 30);
            this.label10.TabIndex = 95;
            this.label10.Text = "Login";
            // 
            // txtpwddb
            // 
            this.txtpwddb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtpwddb.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpwddb.Location = new System.Drawing.Point(34, 165);
            this.txtpwddb.Name = "txtpwddb";
            this.txtpwddb.PasswordChar = '*';
            this.txtpwddb.Size = new System.Drawing.Size(260, 35);
            this.txtpwddb.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(107, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(41, 38);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // txtusernamedb
            // 
            this.txtusernamedb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtusernamedb.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtusernamedb.Location = new System.Drawing.Point(34, 97);
            this.txtusernamedb.Name = "txtusernamedb";
            this.txtusernamedb.Size = new System.Drawing.Size(260, 35);
            this.txtusernamedb.TabIndex = 0;
            // 
            // longpress_midascard
            // 
            this.longpress_midascard.Tick += new System.EventHandler(this.longpress_midascard_Tick);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox3.BackgroundImage = global::FoodandBeverage.Properties.Resources.Footerloginbg;
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Location = new System.Drawing.Point(11, 514);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(276, 64);
            this.pictureBox3.TabIndex = 283;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox4.BackgroundImage = global::FoodandBeverage.Properties.Resources.ResEmblem;
            this.pictureBox4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox4.Location = new System.Drawing.Point(254, 70);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(95, 89);
            this.pictureBox4.TabIndex = 286;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.BackgroundImage = global::FoodandBeverage.Properties.Resources.Login_dine_logo;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(173, 256);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 46);
            this.pictureBox2.TabIndex = 287;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox6.BackgroundImage = global::FoodandBeverage.Properties.Resources.Login_Report_logo;
            this.pictureBox6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox6.Location = new System.Drawing.Point(394, 256);
            this.pictureBox6.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(48, 46);
            this.pictureBox6.TabIndex = 288;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox7.BackgroundImage = global::FoodandBeverage.Properties.Resources.Login_tabel_logo;
            this.pictureBox7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox7.Location = new System.Drawing.Point(285, 256);
            this.pictureBox7.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(48, 46);
            this.pictureBox7.TabIndex = 289;
            this.pictureBox7.TabStop = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("Poppins ExtraBold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(147, 306);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 46);
            this.label5.TabIndex = 290;
            this.label5.Text = "Order Management";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Poppins ExtraBold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(259, 306);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 45);
            this.label7.TabIndex = 291;
            this.label7.Text = "Table Management";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.Font = new System.Drawing.Font("Poppins ExtraBold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(389, 306);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(58, 43);
            this.label14.TabIndex = 292;
            this.label14.Text = "Report && Analytics";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox8.BackgroundImage = global::FoodandBeverage.Properties.Resources.MIDAS_FUN_Wordmark;
            this.pictureBox8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox8.Location = new System.Drawing.Point(131, 171);
            this.pictureBox8.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(348, 51);
            this.pictureBox8.TabIndex = 293;
            this.pictureBox8.TabStop = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(127, 215);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(348, 37);
            this.label3.TabIndex = 294;
            this.label3.Text = "Resturant Management";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox9.BackgroundImage = global::FoodandBeverage.Properties.Resources.Logo_word;
            this.pictureBox9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox9.Location = new System.Drawing.Point(97, 354);
            this.pictureBox9.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(262, 110);
            this.pictureBox9.TabIndex = 295;
            this.pictureBox9.TabStop = false;
            // 
            // pb_online_offline
            // 
            this.pb_online_offline.BackColor = System.Drawing.Color.Transparent;
            this.pb_online_offline.BackgroundImage = global::FoodandBeverage.Properties.Resources.sql_online_icon;
            this.pb_online_offline.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_online_offline.Location = new System.Drawing.Point(909, 541);
            this.pb_online_offline.Margin = new System.Windows.Forms.Padding(2);
            this.pb_online_offline.Name = "pb_online_offline";
            this.pb_online_offline.Size = new System.Drawing.Size(32, 31);
            this.pb_online_offline.TabIndex = 296;
            this.pb_online_offline.TabStop = false;
            this.pb_online_offline.Click += new System.EventHandler(this.pb_online_offline_Click);
            // 
            // lblcompanyname
            // 
            this.lblcompanyname.AutoSize = true;
            this.lblcompanyname.BackColor = System.Drawing.Color.Transparent;
            this.lblcompanyname.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblcompanyname.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcompanyname.ForeColor = System.Drawing.Color.White;
            this.lblcompanyname.Location = new System.Drawing.Point(859, 554);
            this.lblcompanyname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblcompanyname.Name = "lblcompanyname";
            this.lblcompanyname.Size = new System.Drawing.Size(47, 15);
            this.lblcompanyname.TabIndex = 297;
            this.lblcompanyname.Text = "Location";
            this.lblcompanyname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::FoodandBeverage.Properties.Resources.Loginbg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(959, 580);
            this.Controls.Add(this.Pnl_wifiPopup);
            this.Controls.Add(this.lblcompanyname);
            this.Controls.Add(this.pb_online_offline);
            this.Controls.Add(this.pictureBox9);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pnlloginbox);
            this.Controls.Add(this.pnlDbSettings);
            this.Controls.Add(this.pnlDBLogin);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.Shown += new System.EventHandler(this.FrmLogin_Shown);
            this.pnlloginbox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pblogo)).EndInit();
            this.Pnl_wifiPopup.ResumeLayout(false);
            this.tlp_wifiPopup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbwifi)).EndInit();
            this.pnlDbSettings.ResumeLayout(false);
            this.pnlDbSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.pnlDBLogin.ResumeLayout(false);
            this.pnlDBLogin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_online_offline)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnlloginbox;
        private System.Windows.Forms.Button btnrfid;
        private System.Windows.Forms.PictureBox pblogo;
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
        private System.Windows.Forms.Timer longpress_midascard;
        private System.Windows.Forms.Button btn_dbconf_close;
        private FBControl.ModernTextBox txt_password;
        private FBControl.ModernTextBox txt_username;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.PictureBox pb_online_offline;
        private System.Windows.Forms.Label lblcompanyname;
    }
}