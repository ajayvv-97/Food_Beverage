
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
            this.tlp_mainouterdesign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 580F));
            this.tlp_mainouterdesign.Size = new System.Drawing.Size(959, 580);
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
            this.tlp_right.Location = new System.Drawing.Point(508, 0);
            this.tlp_right.Margin = new System.Windows.Forms.Padding(0);
            this.tlp_right.Name = "tlp_right";
            this.tlp_right.RowCount = 2;
            this.tlp_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30.37037F));
            this.tlp_right.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 69.62963F));
            this.tlp_right.Size = new System.Drawing.Size(451, 580);
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
            this.tableLayoutPanel1.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 73.83721F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 26.16279F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(447, 172);
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
            this.tableLayoutPanel2.Location = new System.Drawing.Point(2, 2);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(443, 100);
            this.tableLayoutPanel2.TabIndex = 260;
            // 
            // pblogo
            // 
            this.pblogo.BackgroundImage = global::FoodandBeverage.Properties.Resources.midas_logo;
            this.pblogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pblogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pblogo.Location = new System.Drawing.Point(174, 2);
            this.pblogo.Margin = new System.Windows.Forms.Padding(2);
            this.pblogo.Name = "pblogo";
            this.pblogo.Size = new System.Drawing.Size(94, 96);
            this.pblogo.TabIndex = 258;
            this.pblogo.TabStop = false;
            // 
            // lblcname2
            // 
            this.lblcname2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcname2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcname2.Location = new System.Drawing.Point(2, 141);
            this.lblcname2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblcname2.Name = "lblcname2";
            this.lblcname2.Size = new System.Drawing.Size(443, 31);
            this.lblcname2.TabIndex = 260;
            this.lblcname2.Text = "RESTURANT MANAGEMENT";
            this.lblcname2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblcname
            // 
            this.lblcname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcname.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcname.Location = new System.Drawing.Point(2, 104);
            this.lblcname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblcname.Name = "lblcname";
            this.lblcname.Size = new System.Drawing.Size(443, 37);
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
            this.pnlloginbox.Location = new System.Drawing.Point(0, 176);
            this.pnlloginbox.Margin = new System.Windows.Forms.Padding(0);
            this.pnlloginbox.Name = "pnlloginbox";
            this.pnlloginbox.Size = new System.Drawing.Size(451, 404);
            this.pnlloginbox.TabIndex = 0;
            this.pnlloginbox.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlloginbox_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtPwd);
            this.panel2.Location = new System.Drawing.Point(110, 141);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(240, 40);
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
            this.txtPwd.Multiline = true;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(238, 38);
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
            this.panel1.Location = new System.Drawing.Point(110, 76);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 40);
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
            this.txtUserName.Size = new System.Drawing.Size(238, 38);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.Tag = "";
            this.txtUserName.Text = "ajay";
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
            this.btnrfid.Location = new System.Drawing.Point(353, 82);
            this.btnrfid.Name = "btnrfid";
            this.btnrfid.Size = new System.Drawing.Size(28, 30);
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
            this.btncancel.Location = new System.Drawing.Point(110, 213);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(114, 45);
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
            this.btnlogin.Location = new System.Drawing.Point(240, 213);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(114, 45);
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
            this.pbleftdesign.Size = new System.Drawing.Size(508, 580);
            this.pbleftdesign.TabIndex = 11;
            this.pbleftdesign.TabStop = false;
            // 
            // Pnl_wifiPopup
            // 
            this.Pnl_wifiPopup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(152)))), ((int)(((byte)(227)))));
            this.Pnl_wifiPopup.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Pnl_wifiPopup.Controls.Add(this.tlp_wifiPopup);
            this.Pnl_wifiPopup.Location = new System.Drawing.Point(986, 114);
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
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(959, 580);
            this.Controls.Add(this.Pnl_wifiPopup);
            this.Controls.Add(this.tlp_mainouterdesign);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLogin";
            this.Opacity = 0D;
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
    }
}