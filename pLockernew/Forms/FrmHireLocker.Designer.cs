namespace pLocker.Forms
{
    partial class FrmHireLocker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHireLocker));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkHired = new System.Windows.Forms.CheckBox();
            this.lblToolTipAccName = new System.Windows.Forms.Label();
            this.txtLedgerID = new System.Windows.Forms.TextBox();
            this.txtLockHireID = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.dtpHireTime = new System.Windows.Forms.DateTimePicker();
            this.dtpHireDate = new System.Windows.Forms.DateTimePicker();
            this.txtHireDetails = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblRate = new System.Windows.Forms.Label();
            this.dtpExpTime = new System.Windows.Forms.DateTimePicker();
            this.txtHireForNo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpExpDate = new System.Windows.Forms.DateTimePicker();
            this.cboHireFor = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cboLockStatus = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtCustomerPIN = new System.Windows.Forms.TextBox();
            this.cboLocker = new System.Windows.Forms.ComboBox();
            this.lblLocker = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtAccNO = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnFrmClose = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.txtAccEmail = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAccAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAccPhone = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtAccName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkKhaki;
            this.panel1.Controls.Add(this.chkHired);
            this.panel1.Controls.Add(this.lblToolTipAccName);
            this.panel1.Controls.Add(this.txtLedgerID);
            this.panel1.Controls.Add(this.txtLockHireID);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.cboLockStatus);
            this.panel1.Controls.Add(this.label15);
            this.panel1.Controls.Add(this.txtCustomerPIN);
            this.panel1.Controls.Add(this.cboLocker);
            this.panel1.Controls.Add(this.lblLocker);
            this.panel1.Controls.Add(this.label14);
            this.panel1.Controls.Add(this.txtAccNO);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnFrmClose);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txtRemarks);
            this.panel1.Controls.Add(this.txtAccEmail);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtAccAddress);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtAccPhone);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.txtAccName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(942, 453);
            this.panel1.TabIndex = 45;
            // 
            // chkHired
            // 
            this.chkHired.AutoSize = true;
            this.chkHired.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.chkHired.Location = new System.Drawing.Point(362, 12);
            this.chkHired.Name = "chkHired";
            this.chkHired.Size = new System.Drawing.Size(76, 26);
            this.chkHired.TabIndex = 119;
            this.chkHired.Text = "Hired";
            this.chkHired.UseVisualStyleBackColor = true;
            // 
            // lblToolTipAccName
            // 
            this.lblToolTipAccName.AutoSize = true;
            this.lblToolTipAccName.BackColor = System.Drawing.Color.Transparent;
            this.lblToolTipAccName.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToolTipAccName.ForeColor = System.Drawing.Color.Red;
            this.lblToolTipAccName.Location = new System.Drawing.Point(365, 149);
            this.lblToolTipAccName.Name = "lblToolTipAccName";
            this.lblToolTipAccName.Size = new System.Drawing.Size(142, 16);
            this.lblToolTipAccName.TabIndex = 118;
            this.lblToolTipAccName.Text = "[F12]-For Account List";
            // 
            // txtLedgerID
            // 
            this.txtLedgerID.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtLedgerID.Location = new System.Drawing.Point(16, 116);
            this.txtLedgerID.Name = "txtLedgerID";
            this.txtLedgerID.Size = new System.Drawing.Size(34, 31);
            this.txtLedgerID.TabIndex = 117;
            this.txtLedgerID.Text = "0";
            this.txtLedgerID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLedgerID.Visible = false;
            // 
            // txtLockHireID
            // 
            this.txtLockHireID.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtLockHireID.Location = new System.Drawing.Point(16, 39);
            this.txtLockHireID.Name = "txtLockHireID";
            this.txtLockHireID.Size = new System.Drawing.Size(34, 31);
            this.txtLockHireID.TabIndex = 86;
            this.txtLockHireID.Text = "0";
            this.txtLockHireID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLockHireID.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Tomato;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Location = new System.Drawing.Point(831, 389);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(99, 45);
            this.btnClose.TabIndex = 15;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(441, 84);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(41, 31);
            this.button1.TabIndex = 3;
            this.button1.Text = "RFID";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.dtpHireTime);
            this.groupBox1.Controls.Add(this.dtpHireDate);
            this.groupBox1.Controls.Add(this.txtHireDetails);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.lblRate);
            this.groupBox1.Controls.Add(this.dtpExpTime);
            this.groupBox1.Controls.Add(this.txtHireForNo);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.dtpExpDate);
            this.groupBox1.Controls.Add(this.cboHireFor);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(501, 76);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(429, 307);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hire Details :";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(99, 25);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 22);
            this.label16.TabIndex = 85;
            this.label16.Text = "No.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(6, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(218, 22);
            this.label7.TabIndex = 84;
            this.label7.Text = "Hire From Date && Time :";
            // 
            // dtpHireTime
            // 
            this.dtpHireTime.CalendarFont = new System.Drawing.Font("Century Gothic", 14.25F);
            this.dtpHireTime.CustomFormat = "hh:mm";
            this.dtpHireTime.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtpHireTime.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHireTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpHireTime.Location = new System.Drawing.Point(276, 134);
            this.dtpHireTime.Name = "dtpHireTime";
            this.dtpHireTime.ShowUpDown = true;
            this.dtpHireTime.Size = new System.Drawing.Size(140, 31);
            this.dtpHireTime.TabIndex = 4;
            this.dtpHireTime.Tag = "Time In";
            // 
            // dtpHireDate
            // 
            this.dtpHireDate.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.dtpHireDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHireDate.Location = new System.Drawing.Point(106, 134);
            this.dtpHireDate.Name = "dtpHireDate";
            this.dtpHireDate.Size = new System.Drawing.Size(151, 31);
            this.dtpHireDate.TabIndex = 3;
            // 
            // txtHireDetails
            // 
            this.txtHireDetails.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtHireDetails.Location = new System.Drawing.Point(104, 259);
            this.txtHireDetails.Name = "txtHireDetails";
            this.txtHireDetails.ReadOnly = true;
            this.txtHireDetails.Size = new System.Drawing.Size(310, 31);
            this.txtHireDetails.TabIndex = 7;
            this.txtHireDetails.Text = "...";
            this.txtHireDetails.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtAmount.Location = new System.Drawing.Point(276, 50);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(140, 31);
            this.txtAmount.TabIndex = 2;
            this.txtAmount.Text = "0";
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblRate
            // 
            this.lblRate.AutoSize = true;
            this.lblRate.BackColor = System.Drawing.Color.Black;
            this.lblRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRate.ForeColor = System.Drawing.Color.White;
            this.lblRate.Location = new System.Drawing.Point(354, 18);
            this.lblRate.Name = "lblRate";
            this.lblRate.Size = new System.Drawing.Size(62, 29);
            this.lblRate.TabIndex = 80;
            this.lblRate.Text = "0.00";
            // 
            // dtpExpTime
            // 
            this.dtpExpTime.CalendarFont = new System.Drawing.Font("Century Gothic", 14.25F);
            this.dtpExpTime.CustomFormat = "hh:mm";
            this.dtpExpTime.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtpExpTime.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpExpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpExpTime.Location = new System.Drawing.Point(276, 213);
            this.dtpExpTime.Name = "dtpExpTime";
            this.dtpExpTime.ShowUpDown = true;
            this.dtpExpTime.Size = new System.Drawing.Size(140, 31);
            this.dtpExpTime.TabIndex = 6;
            this.dtpExpTime.Tag = "Time In";
            // 
            // txtHireForNo
            // 
            this.txtHireForNo.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtHireForNo.Location = new System.Drawing.Point(106, 50);
            this.txtHireForNo.Name = "txtHireForNo";
            this.txtHireForNo.Size = new System.Drawing.Size(34, 31);
            this.txtHireForNo.TabIndex = 0;
            this.txtHireForNo.Text = "0";
            this.txtHireForNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(14, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(86, 22);
            this.label9.TabIndex = 79;
            this.label9.Text = "Hire For :";
            // 
            // dtpExpDate
            // 
            this.dtpExpDate.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.dtpExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpExpDate.Location = new System.Drawing.Point(106, 213);
            this.dtpExpDate.Name = "dtpExpDate";
            this.dtpExpDate.Size = new System.Drawing.Size(151, 31);
            this.dtpExpDate.TabIndex = 5;
            // 
            // cboHireFor
            // 
            this.cboHireFor.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.cboHireFor.FormattingEnabled = true;
            this.cboHireFor.Location = new System.Drawing.Point(145, 50);
            this.cboHireFor.Name = "cboHireFor";
            this.cboHireFor.Size = new System.Drawing.Size(112, 30);
            this.cboHireFor.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(6, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(193, 22);
            this.label5.TabIndex = 58;
            this.label5.Text = "Hire to Date && Time :";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(941, 6);
            this.panel3.TabIndex = 115;
            // 
            // cboLockStatus
            // 
            this.cboLockStatus.Enabled = false;
            this.cboLockStatus.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.cboLockStatus.FormattingEnabled = true;
            this.cboLockStatus.Location = new System.Drawing.Point(362, 41);
            this.cboLockStatus.Name = "cboLockStatus";
            this.cboLockStatus.Size = new System.Drawing.Size(120, 30);
            this.cboLockStatus.TabIndex = 1;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(500, 45);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 22);
            this.label15.TabIndex = 74;
            this.label15.Text = "Pin No.:";
            // 
            // txtCustomerPIN
            // 
            this.txtCustomerPIN.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtCustomerPIN.Location = new System.Drawing.Point(583, 41);
            this.txtCustomerPIN.Multiline = true;
            this.txtCustomerPIN.Name = "txtCustomerPIN";
            this.txtCustomerPIN.Size = new System.Drawing.Size(331, 29);
            this.txtCustomerPIN.TabIndex = 10;
            // 
            // cboLocker
            // 
            this.cboLocker.Enabled = false;
            this.cboLocker.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.cboLocker.FormattingEnabled = true;
            this.cboLocker.Location = new System.Drawing.Point(151, 41);
            this.cboLocker.Name = "cboLocker";
            this.cboLocker.Size = new System.Drawing.Size(176, 30);
            this.cboLocker.TabIndex = 0;
            // 
            // lblLocker
            // 
            this.lblLocker.AutoSize = true;
            this.lblLocker.BackColor = System.Drawing.Color.Transparent;
            this.lblLocker.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocker.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblLocker.Location = new System.Drawing.Point(64, 44);
            this.lblLocker.Name = "lblLocker";
            this.lblLocker.Size = new System.Drawing.Size(81, 22);
            this.lblLocker.TabIndex = 71;
            this.lblLocker.Text = "Locker :";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(69, 129);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 22);
            this.label14.TabIndex = 70;
            this.label14.Text = "Name :";
            // 
            // txtAccNO
            // 
            this.txtAccNO.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtAccNO.Location = new System.Drawing.Point(151, 84);
            this.txtAccNO.Multiline = true;
            this.txtAccNO.Name = "txtAccNO";
            this.txtAccNO.ReadOnly = true;
            this.txtAccNO.Size = new System.Drawing.Size(284, 29);
            this.txtAccNO.TabIndex = 2;
            this.txtAccNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAccNO_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold);
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(3, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(173, 26);
            this.label8.TabIndex = 67;
            this.label8.Text = "Locker Hire Details";
            // 
            // btnFrmClose
            // 
            this.btnFrmClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFrmClose.Location = new System.Drawing.Point(915, 6);
            this.btnFrmClose.Name = "btnFrmClose";
            this.btnFrmClose.Size = new System.Drawing.Size(27, 29);
            this.btnFrmClose.TabIndex = 66;
            this.btnFrmClose.Text = "X";
            this.btnFrmClose.UseVisualStyleBackColor = true;
            this.btnFrmClose.Click += new System.EventHandler(this.btnFrmClose_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Location = new System.Drawing.Point(721, 389);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(99, 45);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.BackgroundImage")));
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Location = new System.Drawing.Point(611, 391);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(99, 43);
            this.btnRefresh.TabIndex = 13;
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(501, 390);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(99, 43);
            this.btnSave.TabIndex = 12;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(48, 337);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 22);
            this.label6.TabIndex = 61;
            this.label6.Text = "Remarks :";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtRemarks.Location = new System.Drawing.Point(151, 337);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(331, 29);
            this.txtRemarks.TabIndex = 9;
            // 
            // txtAccEmail
            // 
            this.txtAccEmail.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtAccEmail.Location = new System.Drawing.Point(151, 210);
            this.txtAccEmail.Multiline = true;
            this.txtAccEmail.Name = "txtAccEmail";
            this.txtAccEmail.Size = new System.Drawing.Size(331, 29);
            this.txtAccEmail.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(79, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 22);
            this.label4.TabIndex = 52;
            this.label4.Text = "Email :";
            // 
            // txtAccAddress
            // 
            this.txtAccAddress.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtAccAddress.Location = new System.Drawing.Point(151, 252);
            this.txtAccAddress.Multiline = true;
            this.txtAccAddress.Name = "txtAccAddress";
            this.txtAccAddress.Size = new System.Drawing.Size(331, 71);
            this.txtAccAddress.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(57, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 22);
            this.label3.TabIndex = 50;
            this.label3.Text = "Address:";
            // 
            // txtAccPhone
            // 
            this.txtAccPhone.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtAccPhone.Location = new System.Drawing.Point(151, 168);
            this.txtAccPhone.Multiline = true;
            this.txtAccPhone.Name = "txtAccPhone";
            this.txtAccPhone.Size = new System.Drawing.Size(331, 29);
            this.txtAccPhone.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(66, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 22);
            this.label2.TabIndex = 48;
            this.label2.Text = "Phone :";
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(441, 126);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(41, 31);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtAccName
            // 
            this.txtAccName.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtAccName.Location = new System.Drawing.Point(151, 126);
            this.txtAccName.Multiline = true;
            this.txtAccName.Name = "txtAccName";
            this.txtAccName.ReadOnly = true;
            this.txtAccName.Size = new System.Drawing.Size(284, 29);
            this.txtAccName.TabIndex = 4;
            this.txtAccName.Enter += new System.EventHandler(this.txtAccName_Enter);
            this.txtAccName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAccName_KeyDown);
            this.txtAccName.Leave += new System.EventHandler(this.txtAccName_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 22);
            this.label1.TabIndex = 44;
            this.label1.Text = "Account No.:";
            // 
            // FrmHireLocker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(942, 453);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmHireLocker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmHireLocker";
            this.Load += new System.EventHandler(this.FrmHireLocker_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmHireLocker_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnFrmClose;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAccEmail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAccAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAccPhone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtAccName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtAccNO;
        private System.Windows.Forms.Label lblLocker;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCustomerPIN;
        private System.Windows.Forms.ComboBox cboLocker;
        private System.Windows.Forms.TextBox txtHireDetails;
        private System.Windows.Forms.DateTimePicker dtpExpDate;
        private System.Windows.Forms.ComboBox cboLockStatus;
        private System.Windows.Forms.DateTimePicker dtpExpTime;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpHireTime;
        private System.Windows.Forms.DateTimePicker dtpHireDate;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblRate;
        private System.Windows.Forms.TextBox txtHireForNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboHireFor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtLockHireID;
        private System.Windows.Forms.TextBox txtLedgerID;
        private System.Windows.Forms.Label lblToolTipAccName;
        private System.Windows.Forms.CheckBox chkHired;
    }
}