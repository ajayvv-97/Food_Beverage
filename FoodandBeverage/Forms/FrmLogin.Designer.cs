
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
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.btnlogin = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tlp_mainouterdesign = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlloginbox = new System.Windows.Forms.Panel();
            this.lblappname = new System.Windows.Forms.Label();
            this.btnrfid = new System.Windows.Forms.Button();
            this.tlp_mainouterdesign.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.pnlloginbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.White;
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.ForeColor = System.Drawing.Color.DarkGray;
            this.txtUserName.Location = new System.Drawing.Point(37, 58);
            this.txtUserName.Multiline = true;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(212, 31);
            this.txtUserName.TabIndex = 0;
            this.txtUserName.Text = "User Name .....";
            this.toolTip1.SetToolTip(this.txtUserName, "User Name");
            this.txtUserName.Enter += new System.EventHandler(this.txtusername_Enter);
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            this.txtUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserName_KeyPress);
            this.txtUserName.Leave += new System.EventHandler(this.txtusername_Leave);
            // 
            // btnlogin
            // 
            this.btnlogin.BackColor = System.Drawing.Color.Transparent;
            this.btnlogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnlogin.BackgroundImage")));
            this.btnlogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnlogin.FlatAppearance.BorderSize = 0;
            this.btnlogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlogin.Location = new System.Drawing.Point(30, 181);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(99, 35);
            this.btnlogin.TabIndex = 2;
            this.toolTip1.SetToolTip(this.btnlogin, "Login");
            this.btnlogin.UseVisualStyleBackColor = false;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.Color.Transparent;
            this.btncancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btncancel.BackgroundImage")));
            this.btncancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btncancel.FlatAppearance.BorderSize = 0;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Location = new System.Drawing.Point(147, 181);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(99, 35);
            this.btncancel.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btncancel, "Cancel");
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.Color.White;
            this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPwd.ForeColor = System.Drawing.Color.DarkGray;
            this.txtPwd.Location = new System.Drawing.Point(37, 126);
            this.txtPwd.Multiline = true;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(212, 31);
            this.txtPwd.TabIndex = 1;
            this.txtPwd.Text = "Password .....";
            this.toolTip1.SetToolTip(this.txtPwd, "Password");
            this.txtPwd.TextChanged += new System.EventHandler(this.txtPwd_TextChanged);
            this.txtPwd.Enter += new System.EventHandler(this.txtPwd_Enter);
            this.txtPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPwd_KeyDown);
            this.txtPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPwd_KeyPress);
            this.txtPwd.Leave += new System.EventHandler(this.txtPwd_Leave);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tlp_mainouterdesign
            // 
            this.tlp_mainouterdesign.BackColor = System.Drawing.Color.Transparent;
            this.tlp_mainouterdesign.ColumnCount = 3;
            this.tlp_mainouterdesign.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.95238F));
            this.tlp_mainouterdesign.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.04762F));
            this.tlp_mainouterdesign.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 9F));
            this.tlp_mainouterdesign.Controls.Add(this.tableLayoutPanel2, 1, 1);
            this.tlp_mainouterdesign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_mainouterdesign.Location = new System.Drawing.Point(0, 0);
            this.tlp_mainouterdesign.Name = "tlp_mainouterdesign";
            this.tlp_mainouterdesign.RowCount = 2;
            this.tlp_mainouterdesign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp_mainouterdesign.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 83.33334F));
            this.tlp_mainouterdesign.Size = new System.Drawing.Size(877, 472);
            this.tlp_mainouterdesign.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.pnlloginbox, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblappname, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(575, 81);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.17857F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 84.82143F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(289, 336);
            this.tableLayoutPanel2.TabIndex = 10;
            // 
            // pnlloginbox
            // 
            this.pnlloginbox.Controls.Add(this.btnrfid);
            this.pnlloginbox.Controls.Add(this.txtPwd);
            this.pnlloginbox.Controls.Add(this.btncancel);
            this.pnlloginbox.Controls.Add(this.txtUserName);
            this.pnlloginbox.Controls.Add(this.btnlogin);
            this.pnlloginbox.Location = new System.Drawing.Point(3, 53);
            this.pnlloginbox.Name = "pnlloginbox";
            this.pnlloginbox.Size = new System.Drawing.Size(283, 279);
            this.pnlloginbox.TabIndex = 0;
            this.pnlloginbox.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlloginbox_Paint);
            // 
            // lblappname
            // 
            this.lblappname.AutoSize = true;
            this.lblappname.BackColor = System.Drawing.Color.Transparent;
            this.lblappname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblappname.Font = new System.Drawing.Font("Hobo Std", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblappname.ForeColor = System.Drawing.Color.White;
            this.lblappname.Location = new System.Drawing.Point(3, 0);
            this.lblappname.Name = "lblappname";
            this.lblappname.Size = new System.Drawing.Size(283, 50);
            this.lblappname.TabIndex = 9;
            this.lblappname.Text = "Food && Beverage";
            this.lblappname.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnrfid
            // 
            this.btnrfid.BackColor = System.Drawing.Color.Transparent;
            this.btnrfid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnrfid.BackgroundImage")));
            this.btnrfid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrfid.FlatAppearance.BorderSize = 0;
            this.btnrfid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrfid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrfid.Location = new System.Drawing.Point(252, 59);
            this.btnrfid.Name = "btnrfid";
            this.btnrfid.Size = new System.Drawing.Size(28, 30);
            this.btnrfid.TabIndex = 257;
            this.btnrfid.UseVisualStyleBackColor = false;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::FoodandBeverage.Properties.Resources.FB_LOGIN_BG;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(877, 472);
            this.Controls.Add(this.tlp_mainouterdesign);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLogin";
            this.Opacity = 0D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.tlp_mainouterdesign.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.pnlloginbox.ResumeLayout(false);
            this.pnlloginbox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tlp_mainouterdesign;
        private System.Windows.Forms.Panel pnlloginbox;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label lblappname;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnrfid;
    }
}