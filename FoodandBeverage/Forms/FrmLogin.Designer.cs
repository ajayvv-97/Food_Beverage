
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
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btncancel = new System.Windows.Forms.Button();
            this.btnlogin = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_login = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnl_login.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUserName
            // 
            this.txtUserName.BackColor = System.Drawing.Color.White;
            this.txtUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.ForeColor = System.Drawing.Color.DarkGray;
            this.txtUserName.Location = new System.Drawing.Point(403, 185);
            this.txtUserName.Multiline = true;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(217, 31);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.Text = "User Name .....";
            this.toolTip1.SetToolTip(this.txtUserName, "User Name");
            this.txtUserName.Enter += new System.EventHandler(this.txtusername_Enter);
            this.txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            this.txtUserName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUserName_KeyPress);
            this.txtUserName.Leave += new System.EventHandler(this.txtusername_Leave);
            // 
            // txtPwd
            // 
            this.txtPwd.BackColor = System.Drawing.Color.White;
            this.txtPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPwd.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPwd.ForeColor = System.Drawing.Color.DarkGray;
            this.txtPwd.Location = new System.Drawing.Point(403, 235);
            this.txtPwd.Multiline = true;
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(217, 31);
            this.txtPwd.TabIndex = 2;
            this.txtPwd.Text = "Password .....";
            this.toolTip1.SetToolTip(this.txtPwd, "Password");
            this.txtPwd.TextChanged += new System.EventHandler(this.txtPwd_TextChanged);
            this.txtPwd.Enter += new System.EventHandler(this.txtpassword_Enter);
            this.txtPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPwd_KeyDown);
            this.txtPwd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPwd_KeyPress);
            this.txtPwd.Leave += new System.EventHandler(this.txtpassword_Leave);
            // 
            // btncancel
            // 
            this.btncancel.BackgroundImage = global::FoodandBeverage.Properties.Resources.LoginCancelButton;
            this.btncancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Location = new System.Drawing.Point(525, 309);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(95, 36);
            this.btncancel.TabIndex = 4;
            this.toolTip1.SetToolTip(this.btncancel, "Cancel");
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // btnlogin
            // 
            this.btnlogin.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnlogin.BackgroundImage")));
            this.btnlogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnlogin.FlatAppearance.BorderSize = 0;
            this.btnlogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnlogin.Location = new System.Drawing.Point(419, 309);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(100, 36);
            this.btnlogin.TabIndex = 3;
            this.toolTip1.SetToolTip(this.btnlogin, "Login");
            this.btnlogin.UseVisualStyleBackColor = true;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::FoodandBeverage.Properties.Resources.Rotating_earth_animated;
            this.pictureBox1.Location = new System.Drawing.Point(26, 104);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(339, 255);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Cooper Black", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(179)))), ((int)(((byte)(1)))));
            this.label2.Location = new System.Drawing.Point(119, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(180, 36);
            this.label2.TabIndex = 6;
            this.label2.Text = "Midas Fun";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Cooper Black", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(179)))), ((int)(((byte)(1)))));
            this.label1.Location = new System.Drawing.Point(76, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 36);
            this.label1.TabIndex = 7;
            this.label1.Text = "Food  Beverage";
            // 
            // pnl_login
            // 
            this.pnl_login.BackColor = System.Drawing.Color.Black;
            this.pnl_login.Controls.Add(this.txtUserName);
            this.pnl_login.Controls.Add(this.label1);
            this.pnl_login.Controls.Add(this.btnlogin);
            this.pnl_login.Controls.Add(this.label2);
            this.pnl_login.Controls.Add(this.btncancel);
            this.pnl_login.Controls.Add(this.pictureBox1);
            this.pnl_login.Controls.Add(this.txtPwd);
            this.pnl_login.Location = new System.Drawing.Point(67, 32);
            this.pnl_login.Name = "pnl_login";
            this.pnl_login.Size = new System.Drawing.Size(856, 405);
            this.pnl_login.TabIndex = 8;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(956, 472);
            this.Controls.Add(this.pnl_login);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnl_login.ResumeLayout(false);
            this.pnl_login.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnl_login;
    }
}