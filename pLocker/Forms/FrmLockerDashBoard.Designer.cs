namespace pLocker.Forms
{
    partial class FrmLockerDashBoard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLockerDashBoard));
            this.pCenter = new System.Windows.Forms.Panel();
            this.timerDateTime = new System.Windows.Forms.Timer(this.components);
            this.pLeft = new System.Windows.Forms.Panel();
            this.btnEmployee = new System.Windows.Forms.Button();
            this.btnReport = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnCustomer = new System.Windows.Forms.Button();
            this.btnHire = new System.Windows.Forms.Button();
            this.btnLocker = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMin = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.pbEmployee = new System.Windows.Forms.PictureBox();
            this.lblEmpName = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lbltoday = new System.Windows.Forms.Label();
            this.lblTT = new System.Windows.Forms.Label();
            this.lblDay = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.rjDropdownMenuEmployee = new RJCodeAdvance.RJControls.RJDropdownMenu(this.components);
            this.toolStripMenuemployee = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuUserReg = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuDepartment = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuDesignation = new System.Windows.Forms.ToolStripMenuItem();
            this.pLeft.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmployee)).BeginInit();
            this.panel3.SuspendLayout();
            this.rjDropdownMenuEmployee.SuspendLayout();
            this.SuspendLayout();
            // 
            // pCenter
            // 
            this.pCenter.BackColor = System.Drawing.Color.Transparent;
            this.pCenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pCenter.Location = new System.Drawing.Point(3, 96);
            this.pCenter.Name = "pCenter";
            this.pCenter.Size = new System.Drawing.Size(790, 629);
            this.pCenter.TabIndex = 2;
            // 
            // timerDateTime
            // 
            this.timerDateTime.Enabled = true;
            this.timerDateTime.Tick += new System.EventHandler(this.timerDateTime_Tick);
            // 
            // pLeft
            // 
            this.pLeft.BackColor = System.Drawing.Color.Black;
            this.pLeft.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pLeft.BackgroundImage")));
            this.pLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pLeft.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pLeft.Controls.Add(this.btnEmployee);
            this.pLeft.Controls.Add(this.btnReport);
            this.pLeft.Controls.Add(this.btnSettings);
            this.pLeft.Controls.Add(this.btnCustomer);
            this.pLeft.Controls.Add(this.btnHire);
            this.pLeft.Controls.Add(this.btnLocker);
            this.pLeft.Controls.Add(this.btnLogout);
            this.pLeft.Controls.Add(this.panel1);
            this.pLeft.Location = new System.Drawing.Point(799, 2);
            this.pLeft.Name = "pLeft";
            this.pLeft.Size = new System.Drawing.Size(222, 734);
            this.pLeft.TabIndex = 0;
            // 
            // btnEmployee
            // 
            this.btnEmployee.BackColor = System.Drawing.Color.Transparent;
            this.btnEmployee.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnEmployee.BackgroundImage")));
            this.btnEmployee.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEmployee.FlatAppearance.BorderSize = 0;
            this.btnEmployee.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnEmployee.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnEmployee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmployee.Location = new System.Drawing.Point(5, 378);
            this.btnEmployee.Name = "btnEmployee";
            this.btnEmployee.Size = new System.Drawing.Size(209, 54);
            this.btnEmployee.TabIndex = 3;
            this.btnEmployee.UseVisualStyleBackColor = false;
            this.btnEmployee.Click += new System.EventHandler(this.btnEmployee_Click);
            this.btnEmployee.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            this.btnEmployee.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnEmployee_MouseDown);
            this.btnEmployee.MouseLeave += new System.EventHandler(this.btnEmployee_MouseLeave);
            this.btnEmployee.MouseHover += new System.EventHandler(this.btnEmployee_MouseHover);
            this.btnEmployee.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnEmployee_MouseUp);
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.Transparent;
            this.btnReport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnReport.BackgroundImage")));
            this.btnReport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Location = new System.Drawing.Point(5, 473);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(209, 54);
            this.btnReport.TabIndex = 4;
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            this.btnReport.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            this.btnReport.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnReport_MouseDown);
            this.btnReport.MouseLeave += new System.EventHandler(this.btnReport_MouseLeave);
            this.btnReport.MouseHover += new System.EventHandler(this.btnReport_MouseHover);
            this.btnReport.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnReport_MouseUp);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSettings.BackgroundImage")));
            this.btnSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Location = new System.Drawing.Point(4, 568);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(209, 54);
            this.btnSettings.TabIndex = 5;
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            this.btnSettings.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            this.btnSettings.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSettings_MouseDown);
            this.btnSettings.MouseLeave += new System.EventHandler(this.btnSettings_MouseLeave);
            this.btnSettings.MouseHover += new System.EventHandler(this.btnSettings_MouseHover);
            this.btnSettings.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnSettings_MouseUp);
            // 
            // btnCustomer
            // 
            this.btnCustomer.BackColor = System.Drawing.Color.Transparent;
            this.btnCustomer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCustomer.BackgroundImage")));
            this.btnCustomer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCustomer.FlatAppearance.BorderSize = 0;
            this.btnCustomer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCustomer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomer.Location = new System.Drawing.Point(4, 188);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(209, 54);
            this.btnCustomer.TabIndex = 1;
            this.btnCustomer.UseVisualStyleBackColor = false;
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            this.btnCustomer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            this.btnCustomer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnCustomer_MouseDown);
            this.btnCustomer.MouseLeave += new System.EventHandler(this.btnCustomer_MouseLeave);
            this.btnCustomer.MouseHover += new System.EventHandler(this.btnCustomer_MouseHover);
            this.btnCustomer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnCustomer_MouseUp);
            // 
            // btnHire
            // 
            this.btnHire.BackColor = System.Drawing.Color.Transparent;
            this.btnHire.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHire.BackgroundImage")));
            this.btnHire.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHire.FlatAppearance.BorderSize = 0;
            this.btnHire.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnHire.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnHire.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHire.Location = new System.Drawing.Point(5, 94);
            this.btnHire.Name = "btnHire";
            this.btnHire.Size = new System.Drawing.Size(209, 53);
            this.btnHire.TabIndex = 0;
            this.btnHire.UseVisualStyleBackColor = false;
            this.btnHire.Click += new System.EventHandler(this.btnHire_Click);
            this.btnHire.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            this.btnHire.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnHire_MouseDown);
            this.btnHire.MouseLeave += new System.EventHandler(this.btnHire_MouseLeave);
            this.btnHire.MouseHover += new System.EventHandler(this.btnHire_MouseHover);
            this.btnHire.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnHire_MouseUp);
            // 
            // btnLocker
            // 
            this.btnLocker.BackColor = System.Drawing.Color.Transparent;
            this.btnLocker.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLocker.BackgroundImage")));
            this.btnLocker.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLocker.FlatAppearance.BorderSize = 0;
            this.btnLocker.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLocker.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLocker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLocker.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnLocker.Location = new System.Drawing.Point(4, 283);
            this.btnLocker.Name = "btnLocker";
            this.btnLocker.Size = new System.Drawing.Size(209, 54);
            this.btnLocker.TabIndex = 2;
            this.btnLocker.UseVisualStyleBackColor = false;
            this.btnLocker.Click += new System.EventHandler(this.btnLocker_Click);
            this.btnLocker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            this.btnLocker.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLocker_MouseDown);
            this.btnLocker.MouseLeave += new System.EventHandler(this.btnLocker_MouseLeave);
            this.btnLocker.MouseHover += new System.EventHandler(this.btnLocker_MouseHover);
            this.btnLocker.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnLocker_MouseUp);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.Transparent;
            this.btnLogout.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnLogout.BackgroundImage")));
            this.btnLogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.btnLogout.Location = new System.Drawing.Point(6, 663);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(209, 54);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            this.btnLogout.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            this.btnLogout.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLogout_MouseDown);
            this.btnLogout.MouseLeave += new System.EventHandler(this.btnLogout_MouseLeave);
            this.btnLogout.MouseHover += new System.EventHandler(this.btnLogout_MouseHover);
            this.btnLogout.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnLogout_MouseUp);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnMin);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(222, 90);
            this.panel1.TabIndex = 7;
            // 
            // btnMin
            // 
            this.btnMin.BackColor = System.Drawing.Color.Transparent;
            this.btnMin.BackgroundImage = global::pLocker.Properties.Resources.Minimize_1;
            this.btnMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMin.ForeColor = System.Drawing.Color.White;
            this.btnMin.Location = new System.Drawing.Point(161, -2);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(27, 30);
            this.btnMin.TabIndex = 8;
            this.btnMin.Text = "-";
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Click += new System.EventHandler(this.Minimize);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(7, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Copyright © 2020";
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.Transparent;
            this.btnclose.BackgroundImage = global::pLocker.Properties.Resources.close_1_button;
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclose.FlatAppearance.BorderSize = 0;
            this.btnclose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnclose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.Transparent;
            this.btnclose.Location = new System.Drawing.Point(189, -2);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(27, 30);
            this.btnclose.TabIndex = 7;
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Vs:  4.7.2 ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Elephant", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Orange;
            this.label1.Location = new System.Drawing.Point(3, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "mi Locker-DashBoard";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.MidnightBlue;
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.pbEmployee);
            this.panel2.Controls.Add(this.lblEmpName);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(4, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(789, 90);
            this.panel2.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Elephant", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Orange;
            this.label4.Location = new System.Drawing.Point(13, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 41);
            this.label4.TabIndex = 1;
            this.label4.Text = "mi Locker";
            // 
            // pbEmployee
            // 
            this.pbEmployee.BackColor = System.Drawing.Color.Transparent;
            this.pbEmployee.Location = new System.Drawing.Point(505, 7);
            this.pbEmployee.Name = "pbEmployee";
            this.pbEmployee.Size = new System.Drawing.Size(67, 61);
            this.pbEmployee.TabIndex = 11;
            this.pbEmployee.TabStop = false;
            // 
            // lblEmpName
            // 
            this.lblEmpName.AutoSize = true;
            this.lblEmpName.BackColor = System.Drawing.Color.Transparent;
            this.lblEmpName.Font = new System.Drawing.Font("Century Gothic", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpName.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblEmpName.Location = new System.Drawing.Point(520, 68);
            this.lblEmpName.Name = "lblEmpName";
            this.lblEmpName.Size = new System.Drawing.Size(0, 15);
            this.lblEmpName.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.lblTime);
            this.panel3.Controls.Add(this.lblMonth);
            this.panel3.Controls.Add(this.lbltoday);
            this.panel3.Controls.Add(this.lblTT);
            this.panel3.Controls.Add(this.lblDay);
            this.panel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(623, 10);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(151, 52);
            this.panel3.TabIndex = 10;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(252)))), ((int)(((byte)(0)))));
            this.lblTime.Location = new System.Drawing.Point(3, 6);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(68, 20);
            this.lblTime.TabIndex = 5;
            this.lblTime.Text = "HH:MM";
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(252)))), ((int)(((byte)(0)))));
            this.lblMonth.Location = new System.Drawing.Point(43, 23);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(47, 20);
            this.lblMonth.TabIndex = 8;
            this.lblMonth.Text = "MM :";
            // 
            // lbltoday
            // 
            this.lbltoday.AutoSize = true;
            this.lbltoday.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltoday.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(252)))), ((int)(((byte)(0)))));
            this.lbltoday.Location = new System.Drawing.Point(3, 23);
            this.lbltoday.Name = "lbltoday";
            this.lbltoday.Size = new System.Drawing.Size(45, 20);
            this.lbltoday.TabIndex = 9;
            this.lbltoday.Text = "DD :";
            // 
            // lblTT
            // 
            this.lblTT.AutoSize = true;
            this.lblTT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(252)))), ((int)(((byte)(0)))));
            this.lblTT.Location = new System.Drawing.Point(78, 6);
            this.lblTT.Name = "lblTT";
            this.lblTT.Size = new System.Drawing.Size(65, 20);
            this.lblTT.TabIndex = 7;
            this.lblTT.Text = "AM/PM";
            // 
            // lblDay
            // 
            this.lblDay.AutoSize = true;
            this.lblDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(252)))), ((int)(((byte)(0)))));
            this.lblDay.Location = new System.Drawing.Point(89, 23);
            this.lblDay.Name = "lblDay";
            this.lblDay.Size = new System.Drawing.Size(40, 20);
            this.lblDay.TabIndex = 6;
            this.lblDay.Text = "Day";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(609, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Anikayaj Software solution";
            // 
            // rjDropdownMenuEmployee
            // 
            this.rjDropdownMenuEmployee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.rjDropdownMenuEmployee.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rjDropdownMenuEmployee.IsMainMenu = false;
            this.rjDropdownMenuEmployee.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuemployee,
            this.toolStripMenuUserReg,
            this.ToolStripMenuDepartment,
            this.ToolStripMenuDesignation});
            this.rjDropdownMenuEmployee.MenuItemHeight = 25;
            this.rjDropdownMenuEmployee.MenuItemTextColor = System.Drawing.Color.White;
            this.rjDropdownMenuEmployee.Name = "rjDropdownMenuEmployee";
            this.rjDropdownMenuEmployee.PrimaryColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.rjDropdownMenuEmployee.ShowCheckMargin = true;
            this.rjDropdownMenuEmployee.Size = new System.Drawing.Size(215, 92);
            // 
            // toolStripMenuemployee
            // 
            this.toolStripMenuemployee.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuemployee.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuemployee.Name = "toolStripMenuemployee";
            this.toolStripMenuemployee.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuemployee.Text = "Employee Registration";
            this.toolStripMenuemployee.Click += new System.EventHandler(this.toolStripMenuemployee_Click);
            // 
            // toolStripMenuUserReg
            // 
            this.toolStripMenuUserReg.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripMenuUserReg.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuUserReg.Name = "toolStripMenuUserReg";
            this.toolStripMenuUserReg.Size = new System.Drawing.Size(192, 22);
            this.toolStripMenuUserReg.Text = "User Registration";
            this.toolStripMenuUserReg.Click += new System.EventHandler(this.toolStripMenuUserReg_Click);
            // 
            // ToolStripMenuDepartment
            // 
            this.ToolStripMenuDepartment.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ToolStripMenuDepartment.ForeColor = System.Drawing.Color.White;
            this.ToolStripMenuDepartment.Name = "ToolStripMenuDepartment";
            this.ToolStripMenuDepartment.Size = new System.Drawing.Size(192, 22);
            this.ToolStripMenuDepartment.Text = "Department";
            this.ToolStripMenuDepartment.Click += new System.EventHandler(this.ToolStripMenuDepartment_Click);
            // 
            // ToolStripMenuDesignation
            // 
            this.ToolStripMenuDesignation.ForeColor = System.Drawing.Color.White;
            this.ToolStripMenuDesignation.Name = "ToolStripMenuDesignation";
            this.ToolStripMenuDesignation.Size = new System.Drawing.Size(192, 22);
            this.ToolStripMenuDesignation.Text = "Designation";
            this.ToolStripMenuDesignation.Click += new System.EventHandler(this.ToolStripMenuDesignation_Click);
            // 
            // FrmLockerDashBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 737);
            this.Controls.Add(this.pLeft);
            this.Controls.Add(this.pCenter);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLockerDashBoard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLockerDashBoard";
            this.Load += new System.EventHandler(this.FrmLockerDashBoard_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLockerDashBoard_KeyDown);
            this.pLeft.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbEmployee)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.rjDropdownMenuEmployee.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pLeft;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pCenter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblDay;
        private System.Windows.Forms.Label lblTT;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lbltoday;
        private System.Windows.Forms.Timer timerDateTime;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnReport;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnCustomer;
        private System.Windows.Forms.Button btnHire;
        private System.Windows.Forms.Button btnLocker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEmployee;
        private System.Windows.Forms.PictureBox pbEmployee;
        private System.Windows.Forms.Label lblEmpName;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Label label4;
        private RJCodeAdvance.RJControls.RJDropdownMenu rjDropdownMenuEmployee;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuemployee;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuUserReg;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuDepartment;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuDesignation;
    }
}