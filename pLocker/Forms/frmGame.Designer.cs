namespace pLocker.Forms
{
    partial class frmGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGame));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtimg = new System.Windows.Forms.TextBox();
            this.pic = new System.Windows.Forms.PictureBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtdesc = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtcoinrate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtdicount = new System.Windows.Forms.TextBox();
            this.txttokenrate = new System.Windows.Forms.TextBox();
            this.txtgame = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtmacid = new System.Windows.Forms.TextBox();
            this.txtcardrate = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(-2, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(869, 27);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Tomato;
            this.button1.BackgroundImage = global::pLocker.Properties.Resources.IconCloseSale;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(833, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 24);
            this.button1.TabIndex = 201;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.txtimg);
            this.panel2.Controls.Add(this.pic);
            this.panel2.Controls.Add(this.btnBrowse);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.txtdesc);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.txtcoinrate);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.txtdicount);
            this.panel2.Controls.Add(this.txttokenrate);
            this.panel2.Controls.Add(this.txtgame);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.txtmacid);
            this.panel2.Controls.Add(this.txtcardrate);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Location = new System.Drawing.Point(89, 33);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(689, 547);
            this.panel2.TabIndex = 7;
            // 
            // txtimg
            // 
            this.txtimg.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtimg.Location = new System.Drawing.Point(364, 110);
            this.txtimg.Multiline = true;
            this.txtimg.Name = "txtimg";
            this.txtimg.Size = new System.Drawing.Size(253, 39);
            this.txtimg.TabIndex = 205;
            this.txtimg.Visible = false;
            // 
            // pic
            // 
            this.pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pic.Location = new System.Drawing.Point(173, 64);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(185, 85);
            this.pic.TabIndex = 204;
            this.pic.TabStop = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.Transparent;
            this.btnBrowse.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnBrowse.BackgroundImage")));
            this.btnBrowse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowse.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnBrowse.FlatAppearance.BorderSize = 0;
            this.btnBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.WhiteSmoke;
            this.btnBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBrowse.Location = new System.Drawing.Point(364, 71);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(45, 39);
            this.btnBrowse.TabIndex = 203;
            this.btnBrowse.UseVisualStyleBackColor = false;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(71, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(70, 22);
            this.label11.TabIndex = 202;
            this.label11.Text = "Image";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Tomato;
            this.btnClose.BackgroundImage = global::pLocker.Properties.Resources.IconCloseSale;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Location = new System.Drawing.Point(531, 495);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 45);
            this.btnClose.TabIndex = 200;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // txtdesc
            // 
            this.txtdesc.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtdesc.Location = new System.Drawing.Point(173, 452);
            this.txtdesc.Multiline = true;
            this.txtdesc.Name = "txtdesc";
            this.txtdesc.Size = new System.Drawing.Size(457, 39);
            this.txtdesc.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(45, 452);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 22);
            this.label6.TabIndex = 199;
            this.label6.Text = "Description";
            // 
            // txtcoinrate
            // 
            this.txtcoinrate.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtcoinrate.Location = new System.Drawing.Point(173, 335);
            this.txtcoinrate.Multiline = true;
            this.txtcoinrate.Name = "txtcoinrate";
            this.txtcoinrate.Size = new System.Drawing.Size(457, 39);
            this.txtcoinrate.TabIndex = 5;
            this.txtcoinrate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcoinrate_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(45, 335);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 22);
            this.label5.TabIndex = 198;
            this.label5.Text = "Coin Rate";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(426, 497);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(99, 43);
            this.btnSave.TabIndex = 192;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
            // 
            // txtdicount
            // 
            this.txtdicount.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtdicount.Location = new System.Drawing.Point(173, 395);
            this.txtdicount.Multiline = true;
            this.txtdicount.Name = "txtdicount";
            this.txtdicount.Size = new System.Drawing.Size(457, 39);
            this.txtdicount.TabIndex = 6;
            this.txtdicount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtdicount_KeyPress);
            // 
            // txttokenrate
            // 
            this.txttokenrate.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txttokenrate.Location = new System.Drawing.Point(173, 277);
            this.txttokenrate.Multiline = true;
            this.txttokenrate.Name = "txttokenrate";
            this.txttokenrate.Size = new System.Drawing.Size(457, 39);
            this.txttokenrate.TabIndex = 4;
            this.txttokenrate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txttokenrate_KeyPress);
            // 
            // txtgame
            // 
            this.txtgame.BackColor = System.Drawing.Color.White;
            this.txtgame.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtgame.Location = new System.Drawing.Point(173, 19);
            this.txtgame.Multiline = true;
            this.txtgame.Name = "txtgame";
            this.txtgame.Size = new System.Drawing.Size(457, 39);
            this.txtgame.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(82, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 22);
            this.label7.TabIndex = 197;
            this.label7.Text = "Game";
            // 
            // txtmacid
            // 
            this.txtmacid.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtmacid.Location = new System.Drawing.Point(173, 155);
            this.txtmacid.Multiline = true;
            this.txtmacid.Name = "txtmacid";
            this.txtmacid.Size = new System.Drawing.Size(457, 39);
            this.txtmacid.TabIndex = 2;
            // 
            // txtcardrate
            // 
            this.txtcardrate.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtcardrate.Location = new System.Drawing.Point(173, 216);
            this.txtcardrate.Multiline = true;
            this.txtcardrate.Name = "txtcardrate";
            this.txtcardrate.Size = new System.Drawing.Size(457, 39);
            this.txtcardrate.TabIndex = 3;
            this.txtcardrate.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtcardrate_KeyPress);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(45, 395);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 22);
            this.label8.TabIndex = 196;
            this.label8.Text = "Disscount :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(70, 155);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 22);
            this.label9.TabIndex = 193;
            this.label9.Text = "MacID :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(34, 277);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(123, 22);
            this.label10.TabIndex = 194;
            this.label10.Text = "Token Rate :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(35, 216);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(116, 22);
            this.label14.TabIndex = 195;
            this.label14.Text = "Card Rate :";
            // 
            // frmGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(866, 592);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmGame";
            this.Load += new System.EventHandler(this.frmGame_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtdesc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtcoinrate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtdicount;
        private System.Windows.Forms.TextBox txttokenrate;
        private System.Windows.Forms.TextBox txtgame;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtmacid;
        private System.Windows.Forms.TextBox txtcardrate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pic;
        private System.Windows.Forms.TextBox txtimg;
    }
}