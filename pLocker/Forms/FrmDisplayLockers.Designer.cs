namespace pLocker.Forms
{
    partial class FrmDisplayLockers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
            
        //}

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDisplayLockers));
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnFrmClose = new System.Windows.Forms.Button();
			this.btnclose = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label7 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnTodExp = new System.Windows.Forms.Button();
			this.btnTodNew = new System.Windows.Forms.Button();
			this.btnTodHire = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.txtLocker = new System.Windows.Forms.TextBox();
			this.lblFrom = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.btnTotalAvail = new System.Windows.Forms.Button();
			this.btnTotalHire = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.DodgerBlue;
			this.panel3.Location = new System.Drawing.Point(1, 1);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(1298, 6);
			this.panel3.TabIndex = 115;
			// 
			// btnFrmClose
			// 
			this.btnFrmClose.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFrmClose.Location = new System.Drawing.Point(1273, 4);
			this.btnFrmClose.Name = "btnFrmClose";
			this.btnFrmClose.Size = new System.Drawing.Size(26, 30);
			this.btnFrmClose.TabIndex = 116;
			this.btnFrmClose.Text = "X";
			this.btnFrmClose.UseVisualStyleBackColor = true;
			// 
			// btnclose
			// 
			this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnclose.Location = new System.Drawing.Point(998, 3);
			this.btnclose.Name = "btnclose";
			this.btnclose.Size = new System.Drawing.Size(25, 26);
			this.btnclose.TabIndex = 8;
			this.btnclose.Text = "X";
			this.btnclose.UseVisualStyleBackColor = true;
			// 
			// comboBox1
			// 
			this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
			this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.comboBox1.ForeColor = System.Drawing.Color.Red;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(12, 38);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(168, 32);
			this.comboBox1.TabIndex = 9;
			this.comboBox1.SelectionChangeCommitted += new System.EventHandler(this.comboBox1_SelectionChangeCommitted);
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
			this.panel1.Location = new System.Drawing.Point(12, 98);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(827, 648);
			this.panel1.TabIndex = 10;
			this.panel1.Click += new System.EventHandler(this.panel1_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Black;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.ForeColor = System.Drawing.Color.Yellow;
			this.label7.Location = new System.Drawing.Point(147, 20);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(48, 15);
			this.label7.TabIndex = 12;
			this.label7.Text = "Expires";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Black;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.Chartreuse;
			this.label3.Location = new System.Drawing.Point(73, 21);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(61, 15);
			this.label3.TabIndex = 11;
			this.label3.Text = "New Reg.";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.White;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(397, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 15);
			this.label1.TabIndex = 12;
			this.label1.Text = "Total Hire";
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.Black;
			this.groupBox1.Controls.Add(this.btnTodExp);
			this.groupBox1.Controls.Add(this.btnTodNew);
			this.groupBox1.Controls.Add(this.btnTodHire);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(564, 2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(211, 92);
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			// 
			// btnTodExp
			// 
			this.btnTodExp.BackgroundImage = global::pLocker.Properties.Resources.Counting_2;
			this.btnTodExp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnTodExp.FlatAppearance.BorderSize = 0;
			this.btnTodExp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTodExp.ForeColor = System.Drawing.Color.Yellow;
			this.btnTodExp.Location = new System.Drawing.Point(141, 36);
			this.btnTodExp.Name = "btnTodExp";
			this.btnTodExp.Size = new System.Drawing.Size(65, 60);
			this.btnTodExp.TabIndex = 23;
			this.btnTodExp.Text = "99";
			this.btnTodExp.UseVisualStyleBackColor = true;
			this.btnTodExp.Click += new System.EventHandler(this.btnTodExp_Click);
			// 
			// btnTodNew
			// 
			this.btnTodNew.BackgroundImage = global::pLocker.Properties.Resources.Counting_2;
			this.btnTodNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnTodNew.FlatAppearance.BorderSize = 0;
			this.btnTodNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTodNew.ForeColor = System.Drawing.Color.Chartreuse;
			this.btnTodNew.Location = new System.Drawing.Point(71, 36);
			this.btnTodNew.Name = "btnTodNew";
			this.btnTodNew.Size = new System.Drawing.Size(65, 60);
			this.btnTodNew.TabIndex = 22;
			this.btnTodNew.Text = "99";
			this.btnTodNew.UseVisualStyleBackColor = true;
			// 
			// btnTodHire
			// 
			this.btnTodHire.BackgroundImage = global::pLocker.Properties.Resources.Counting_2;
			this.btnTodHire.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnTodHire.FlatAppearance.BorderSize = 0;
			this.btnTodHire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTodHire.Location = new System.Drawing.Point(5, 36);
			this.btnTodHire.Name = "btnTodHire";
			this.btnTodHire.Size = new System.Drawing.Size(65, 60);
			this.btnTodHire.TabIndex = 21;
			this.btnTodHire.Text = "99";
			this.btnTodHire.UseVisualStyleBackColor = true;
			this.btnTodHire.Click += new System.EventHandler(this.btnTodHire_Click);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.White;
			this.label5.Location = new System.Drawing.Point(6, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(49, 15);
			this.label5.TabIndex = 17;
			this.label5.Text = "Today\'s";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.White;
			this.label2.Location = new System.Drawing.Point(16, 23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(30, 15);
			this.label2.TabIndex = 15;
			this.label2.Text = "Hire";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.White;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.Lime;
			this.label4.Location = new System.Drawing.Point(479, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 15);
			this.label4.TabIndex = 16;
			this.label4.Text = "Available";
			// 
			// txtLocker
			// 
			this.txtLocker.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtLocker.Location = new System.Drawing.Point(203, 38);
			this.txtLocker.Multiline = true;
			this.txtLocker.Name = "txtLocker";
			this.txtLocker.Size = new System.Drawing.Size(83, 32);
			this.txtLocker.TabIndex = 0;
			this.txtLocker.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtLocker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocker_KeyDown);
			this.txtLocker.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLocker_KeyPress);
			// 
			// lblFrom
			// 
			this.lblFrom.AutoSize = true;
			this.lblFrom.BackColor = System.Drawing.Color.Transparent;
			this.lblFrom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFrom.ForeColor = System.Drawing.Color.Black;
			this.lblFrom.Location = new System.Drawing.Point(200, 19);
			this.lblFrom.Name = "lblFrom";
			this.lblFrom.Size = new System.Drawing.Size(86, 15);
			this.lblFrom.TabIndex = 21;
			this.lblFrom.Text = "No.Start From";
			// 
			// button1
			// 
			this.button1.BackColor = System.Drawing.Color.Transparent;
			this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
			this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.button1.ForeColor = System.Drawing.Color.Indigo;
			this.button1.Location = new System.Drawing.Point(342, 43);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(44, 43);
			this.button1.TabIndex = 22;
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnTotalAvail
			// 
			this.btnTotalAvail.BackgroundImage = global::pLocker.Properties.Resources.Counting_3;
			this.btnTotalAvail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnTotalAvail.FlatAppearance.BorderSize = 0;
			this.btnTotalAvail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTotalAvail.ForeColor = System.Drawing.Color.Lime;
			this.btnTotalAvail.Location = new System.Drawing.Point(475, 34);
			this.btnTotalAvail.Name = "btnTotalAvail";
			this.btnTotalAvail.Size = new System.Drawing.Size(65, 60);
			this.btnTotalAvail.TabIndex = 20;
			this.btnTotalAvail.Text = "99";
			this.btnTotalAvail.UseVisualStyleBackColor = true;
			this.btnTotalAvail.Click += new System.EventHandler(this.btnTotalAvail_Click);
			// 
			// btnTotalHire
			// 
			this.btnTotalHire.BackgroundImage = global::pLocker.Properties.Resources.Counting_1;
			this.btnTotalHire.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnTotalHire.FlatAppearance.BorderSize = 0;
			this.btnTotalHire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnTotalHire.ForeColor = System.Drawing.Color.Red;
			this.btnTotalHire.Location = new System.Drawing.Point(395, 34);
			this.btnTotalHire.Name = "btnTotalHire";
			this.btnTotalHire.Size = new System.Drawing.Size(65, 60);
			this.btnTotalHire.TabIndex = 19;
			this.btnTotalHire.Text = "99";
			this.btnTotalHire.UseVisualStyleBackColor = true;
			this.btnTotalHire.Click += new System.EventHandler(this.btnTotalHire_Click);
			// 
			// FrmDisplayLockers
			// 
			this.BackColor = System.Drawing.Color.White;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.ClientSize = new System.Drawing.Size(851, 733);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.lblFrom);
			this.Controls.Add(this.btnTotalAvail);
			this.Controls.Add(this.txtLocker);
			this.Controls.Add(this.btnTotalHire);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.btnclose);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmDisplayLockers";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.FrmDisplayLockers_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDisplayLockers_KeyDown);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnFrmClose;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnTotalHire;
        private System.Windows.Forms.Button btnTotalAvail;
        private System.Windows.Forms.Button btnTodHire;
        private System.Windows.Forms.Button btnTodExp;
        private System.Windows.Forms.Button btnTodNew;
        private System.Windows.Forms.TextBox txtLocker;
        private System.Windows.Forms.Label lblFrom;
		private System.Windows.Forms.Button button1;
	}
}