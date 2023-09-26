
namespace pLocker.Forms.UserAccount
{
    partial class FrmUserAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserAccount));
            this.chkshowpassword = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.lblToolTipAccName = new System.Windows.Forms.Label();
            this.chkstatus = new System.Windows.Forms.CheckBox();
            this.txtuname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtemp = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.mcmbaccounttype = new MetroFramework.Controls.MetroComboBox();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.txtcconpassword = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnMin = new System.Windows.Forms.Button();
            this.hlTitle = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.mbtnClose = new System.Windows.Forms.Button();
            this.mbtnRefresh = new System.Windows.Forms.Button();
            this.mbtnList = new System.Windows.Forms.Button();
            this.mbtnSave = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkshowpassword
            // 
            this.chkshowpassword.AutoSize = true;
            this.chkshowpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkshowpassword.ForeColor = System.Drawing.Color.Silver;
            this.chkshowpassword.Location = new System.Drawing.Point(364, 179);
            this.chkshowpassword.Name = "chkshowpassword";
            this.chkshowpassword.Size = new System.Drawing.Size(101, 17);
            this.chkshowpassword.TabIndex = 234;
            this.chkshowpassword.Text = "Show password";
            this.chkshowpassword.UseVisualStyleBackColor = true;
            this.chkshowpassword.CheckedChanged += new System.EventHandler(this.chkshowpassword_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(102, 214);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(13, 17);
            this.label6.TabIndex = 233;
            this.label6.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(102, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 17);
            this.label5.TabIndex = 232;
            this.label5.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(102, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(13, 17);
            this.label3.TabIndex = 231;
            this.label3.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(102, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 17);
            this.label2.TabIndex = 230;
            this.label2.Text = "*";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(102, 54);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(13, 17);
            this.label18.TabIndex = 229;
            this.label18.Text = "*";
            // 
            // lblToolTipAccName
            // 
            this.lblToolTipAccName.AutoSize = true;
            this.lblToolTipAccName.BackColor = System.Drawing.Color.Transparent;
            this.lblToolTipAccName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblToolTipAccName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToolTipAccName.ForeColor = System.Drawing.Color.Red;
            this.lblToolTipAccName.Location = new System.Drawing.Point(219, 115);
            this.lblToolTipAccName.Name = "lblToolTipAccName";
            this.lblToolTipAccName.Size = new System.Drawing.Size(133, 17);
            this.lblToolTipAccName.TabIndex = 228;
            this.lblToolTipAccName.Text = "[F8]-For Account List";
            this.lblToolTipAccName.Visible = false;
            // 
            // chkstatus
            // 
            this.chkstatus.AutoSize = true;
            this.chkstatus.Checked = true;
            this.chkstatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkstatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkstatus.ForeColor = System.Drawing.Color.Silver;
            this.chkstatus.Location = new System.Drawing.Point(330, 247);
            this.chkstatus.Name = "chkstatus";
            this.chkstatus.Size = new System.Drawing.Size(74, 20);
            this.chkstatus.TabIndex = 227;
            this.chkstatus.Text = "IsActive";
            this.chkstatus.UseVisualStyleBackColor = true;
            // 
            // txtuname
            // 
            this.txtuname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtuname.Location = new System.Drawing.Point(115, 134);
            this.txtuname.Multiline = true;
            this.txtuname.Name = "txtuname";
            this.txtuname.Size = new System.Drawing.Size(243, 29);
            this.txtuname.TabIndex = 215;
            this.txtuname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtuname_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(15, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 18);
            this.label1.TabIndex = 226;
            this.label1.Text = "User Name :";
            // 
            // txtemp
            // 
            this.txtemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtemp.Location = new System.Drawing.Point(115, 94);
            this.txtemp.Multiline = true;
            this.txtemp.Name = "txtemp";
            this.txtemp.ReadOnly = true;
            this.txtemp.Size = new System.Drawing.Size(243, 29);
            this.txtemp.TabIndex = 214;
            this.txtemp.Enter += new System.EventHandler(this.txtemp_Enter);
            this.txtemp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtemp_KeyDown);
            this.txtemp.Leave += new System.EventHandler(this.txtemp_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Silver;
            this.label11.Location = new System.Drawing.Point(25, 94);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 18);
            this.label11.TabIndex = 222;
            this.label11.Text = "Employee :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Silver;
            this.label16.Location = new System.Drawing.Point(23, 54);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(84, 18);
            this.label16.TabIndex = 225;
            this.label16.Text = "User Type :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Silver;
            this.label12.Location = new System.Drawing.Point(24, 174);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(83, 18);
            this.label12.TabIndex = 223;
            this.label12.Text = "Password :";
            // 
            // mcmbaccounttype
            // 
            this.mcmbaccounttype.BackColor = System.Drawing.Color.White;
            this.mcmbaccounttype.DropDownHeight = 200;
            this.mcmbaccounttype.ForeColor = System.Drawing.Color.Black;
            this.mcmbaccounttype.FormattingEnabled = true;
            this.mcmbaccounttype.IntegralHeight = false;
            this.mcmbaccounttype.ItemHeight = 23;
            this.mcmbaccounttype.Location = new System.Drawing.Point(115, 54);
            this.mcmbaccounttype.Name = "mcmbaccounttype";
            this.mcmbaccounttype.Size = new System.Drawing.Size(243, 29);
            this.mcmbaccounttype.TabIndex = 213;
            this.mcmbaccounttype.Tag = "Department";
            this.mcmbaccounttype.UseCustomBackColor = true;
            this.mcmbaccounttype.UseCustomForeColor = true;
            this.mcmbaccounttype.UseSelectable = true;
            this.mcmbaccounttype.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbaccounttype_KeyDown);
            // 
            // txtpassword
            // 
            this.txtpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtpassword.Location = new System.Drawing.Point(115, 174);
            this.txtpassword.Multiline = true;
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.PasswordChar = '*';
            this.txtpassword.Size = new System.Drawing.Size(243, 29);
            this.txtpassword.TabIndex = 216;
            this.txtpassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpassword_KeyDown);
            // 
            // txtcconpassword
            // 
            this.txtcconpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtcconpassword.Location = new System.Drawing.Point(115, 214);
            this.txtcconpassword.Multiline = true;
            this.txtcconpassword.Name = "txtcconpassword";
            this.txtcconpassword.PasswordChar = '*';
            this.txtcconpassword.Size = new System.Drawing.Size(243, 29);
            this.txtcconpassword.TabIndex = 217;
            this.txtcconpassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcconpassword_KeyDown);
            this.txtcconpassword.Leave += new System.EventHandler(this.txtcconpassword_Leave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Silver;
            this.label15.Location = new System.Drawing.Point(38, 219);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 18);
            this.label15.TabIndex = 224;
            this.label15.Text = "Confirm :";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(515, 319);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 40);
            this.btnClose.TabIndex = 221;
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(4)))), ((int)(((byte)(209)))));
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.Controls.Add(this.btnMin);
            this.panel2.Controls.Add(this.hlTitle);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(465, 43);
            this.panel2.TabIndex = 235;
            // 
            // btnMin
            // 
            this.btnMin.BackColor = System.Drawing.Color.Transparent;
            this.btnMin.BackgroundImage = global::pLocker.Properties.Resources.Minimize_1;
            this.btnMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnMin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMin.ForeColor = System.Drawing.Color.White;
            this.btnMin.Location = new System.Drawing.Point(405, 1);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(27, 30);
            this.btnMin.TabIndex = 173;
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // hlTitle
            // 
            this.hlTitle.AutoSize = true;
            this.hlTitle.BackColor = System.Drawing.Color.Transparent;
            this.hlTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
            this.hlTitle.Location = new System.Drawing.Point(3, 9);
            this.hlTitle.Name = "hlTitle";
            this.hlTitle.Size = new System.Drawing.Size(126, 25);
            this.hlTitle.TabIndex = 161;
            this.hlTitle.Text = "User Account";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::pLocker.Properties.Resources.close_1_button;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(436, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 31);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mbtnClose
            // 
            this.mbtnClose.BackgroundImage = global::pLocker.Properties.Resources.close_1;
            this.mbtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnClose.FlatAppearance.BorderSize = 0;
            this.mbtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnClose.Location = new System.Drawing.Point(348, 279);
            this.mbtnClose.Name = "mbtnClose";
            this.mbtnClose.Size = new System.Drawing.Size(95, 32);
            this.mbtnClose.TabIndex = 239;
            this.mbtnClose.UseVisualStyleBackColor = true;
            this.mbtnClose.Click += new System.EventHandler(this.mbtnClose_Click);
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackgroundImage = global::pLocker.Properties.Resources.refresh_1;
            this.mbtnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnRefresh.FlatAppearance.BorderSize = 0;
            this.mbtnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnRefresh.Location = new System.Drawing.Point(124, 279);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(95, 32);
            this.mbtnRefresh.TabIndex = 238;
            this.mbtnRefresh.UseVisualStyleBackColor = true;
            this.mbtnRefresh.Click += new System.EventHandler(this.mbtnRefresh_Click);
            // 
            // mbtnList
            // 
            this.mbtnList.BackgroundImage = global::pLocker.Properties.Resources.search_1;
            this.mbtnList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnList.FlatAppearance.BorderSize = 0;
            this.mbtnList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnList.Location = new System.Drawing.Point(237, 279);
            this.mbtnList.Name = "mbtnList";
            this.mbtnList.Size = new System.Drawing.Size(95, 32);
            this.mbtnList.TabIndex = 237;
            this.mbtnList.UseVisualStyleBackColor = true;
            this.mbtnList.Click += new System.EventHandler(this.mbtnList_Click);
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackgroundImage = global::pLocker.Properties.Resources.save_1;
            this.mbtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnSave.FlatAppearance.BorderSize = 0;
            this.mbtnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnSave.Location = new System.Drawing.Point(12, 276);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(95, 32);
            this.mbtnSave.TabIndex = 236;
            this.mbtnSave.UseVisualStyleBackColor = true;
            this.mbtnSave.Click += new System.EventHandler(this.mbtnSave_Click);
            // 
            // FrmUserAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(465, 329);
            this.Controls.Add(this.mbtnClose);
            this.Controls.Add(this.mbtnRefresh);
            this.Controls.Add(this.mbtnList);
            this.Controls.Add(this.mbtnSave);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.chkshowpassword);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.lblToolTipAccName);
            this.Controls.Add(this.chkstatus);
            this.Controls.Add(this.txtuname);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtemp);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.mcmbaccounttype);
            this.Controls.Add(this.txtpassword);
            this.Controls.Add(this.txtcconpassword);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmUserAccount";
            this.Text = "FrmUserAccount";
            this.Load += new System.EventHandler(this.FrmUserAccount_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkshowpassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblToolTipAccName;
        private System.Windows.Forms.CheckBox chkstatus;
        private System.Windows.Forms.TextBox txtuname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtemp;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label12;
        private MetroFramework.Controls.MetroComboBox mcmbaccounttype;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.TextBox txtcconpassword;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Label hlTitle;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button mbtnClose;
        private System.Windows.Forms.Button mbtnRefresh;
        private System.Windows.Forms.Button mbtnList;
        private System.Windows.Forms.Button mbtnSave;
    }
}