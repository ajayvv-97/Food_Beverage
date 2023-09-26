namespace pLocker.Forms
{
    partial class FrmLocker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLocker));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.mtxtFieldValue = new System.Windows.Forms.TextBox();
            this.cboField = new System.Windows.Forms.ComboBox();
            this.lblTotLocker = new System.Windows.Forms.Label();
            this.btnFrmClose = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlLocker = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cboLocation = new System.Windows.Forms.ComboBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.grpRate = new System.Windows.Forms.GroupBox();
            this.txtPerHours = new System.Windows.Forms.TextBox();
            this.txtPerDay = new System.Windows.Forms.TextBox();
            this.txtMonth = new System.Windows.Forms.TextBox();
            this.lalHours = new System.Windows.Forms.Label();
            this.lalDay = new System.Windows.Forms.Label();
            this.lblMonth = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtPinNo = new System.Windows.Forms.TextBox();
            this.lblPinNo = new System.Windows.Forms.Label();
            this.btnRfid = new System.Windows.Forms.Button();
            this.txtRFID = new System.Windows.Forms.TextBox();
            this.lblRfid = new System.Windows.Forms.Label();
            this.txtMacId = new System.Windows.Forms.TextBox();
            this.lblMacId = new System.Windows.Forms.Label();
            this.lblRowNo = new System.Windows.Forms.Label();
            this.txtRowNo = new System.Windows.Forms.TextBox();
            this.txtLocker = new System.Windows.Forms.TextBox();
            this.lblLocker = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnLocker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnRowno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnMacID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnRFID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnPINNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnLockDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnHrRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnDyRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnMnRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbLocationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnColorPlate = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.pnlLocker.SuspendLayout();
            this.grpRate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // colorDialog1
            // 
            this.colorDialog1.Color = System.Drawing.Color.White;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.mtxtFieldValue);
            this.panel1.Controls.Add(this.cboField);
            this.panel1.Controls.Add(this.lblTotLocker);
            this.panel1.Controls.Add(this.btnFrmClose);
            this.panel1.Controls.Add(this.btnNew);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.pnlLocker);
            this.panel1.Controls.Add(this.label);
            this.panel1.Controls.Add(this.dgvList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1107, 494);
            this.panel1.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(159, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 22);
            this.label3.TabIndex = 121;
            this.label3.Text = "Search In :";
            // 
            // mtxtFieldValue
            // 
            this.mtxtFieldValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mtxtFieldValue.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.mtxtFieldValue.Location = new System.Drawing.Point(407, 25);
            this.mtxtFieldValue.Name = "mtxtFieldValue";
            this.mtxtFieldValue.Size = new System.Drawing.Size(266, 31);
            this.mtxtFieldValue.TabIndex = 121;
            // 
            // cboField
            // 
            this.cboField.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.cboField.FormattingEnabled = true;
            this.cboField.Location = new System.Drawing.Point(271, 26);
            this.cboField.Name = "cboField";
            this.cboField.Size = new System.Drawing.Size(130, 30);
            this.cboField.TabIndex = 121;
            // 
            // lblTotLocker
            // 
            this.lblTotLocker.AutoSize = true;
            this.lblTotLocker.BackColor = System.Drawing.Color.Transparent;
            this.lblTotLocker.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotLocker.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTotLocker.Location = new System.Drawing.Point(3, 469);
            this.lblTotLocker.Name = "lblTotLocker";
            this.lblTotLocker.Size = new System.Drawing.Size(20, 17);
            this.lblTotLocker.TabIndex = 58;
            this.lblTotLocker.Text = "...";
            // 
            // btnFrmClose
            // 
            this.btnFrmClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFrmClose.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFrmClose.Location = new System.Drawing.Point(1078, 4);
            this.btnFrmClose.Name = "btnFrmClose";
            this.btnFrmClose.Size = new System.Drawing.Size(26, 30);
            this.btnFrmClose.TabIndex = 43;
            this.btnFrmClose.Text = "X";
            this.btnFrmClose.UseVisualStyleBackColor = true;
            this.btnFrmClose.Click += new System.EventHandler(this.btnFrmClose_Click);
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnNew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNew.BackgroundImage")));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnNew.Location = new System.Drawing.Point(903, 20);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(112, 36);
            this.btnNew.TabIndex = 117;
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Location = new System.Drawing.Point(679, 20);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(112, 36);
            this.btnSearch.TabIndex = 116;
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel3.Location = new System.Drawing.Point(-1, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1105, 6);
            this.panel3.TabIndex = 114;
            // 
            // pnlLocker
            // 
            this.pnlLocker.Controls.Add(this.btnColorPlate);
            this.pnlLocker.Controls.Add(this.label2);
            this.pnlLocker.Controls.Add(this.cboLocation);
            this.pnlLocker.Controls.Add(this.txtID);
            this.pnlLocker.Controls.Add(this.label1);
            this.pnlLocker.Controls.Add(this.txtColor);
            this.pnlLocker.Controls.Add(this.panel4);
            this.pnlLocker.Controls.Add(this.grpRate);
            this.pnlLocker.Controls.Add(this.btnDelete);
            this.pnlLocker.Controls.Add(this.btnRefresh);
            this.pnlLocker.Controls.Add(this.btnSave);
            this.pnlLocker.Controls.Add(this.txtDesc);
            this.pnlLocker.Controls.Add(this.lblDesc);
            this.pnlLocker.Controls.Add(this.txtPinNo);
            this.pnlLocker.Controls.Add(this.lblPinNo);
            this.pnlLocker.Controls.Add(this.btnRfid);
            this.pnlLocker.Controls.Add(this.txtRFID);
            this.pnlLocker.Controls.Add(this.lblRfid);
            this.pnlLocker.Controls.Add(this.txtMacId);
            this.pnlLocker.Controls.Add(this.lblMacId);
            this.pnlLocker.Controls.Add(this.lblRowNo);
            this.pnlLocker.Controls.Add(this.txtRowNo);
            this.pnlLocker.Controls.Add(this.txtLocker);
            this.pnlLocker.Controls.Add(this.lblLocker);
            this.pnlLocker.Location = new System.Drawing.Point(185, 88);
            this.pnlLocker.Name = "pnlLocker";
            this.pnlLocker.Size = new System.Drawing.Size(750, 361);
            this.pnlLocker.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(3, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 22);
            this.label2.TabIndex = 120;
            this.label2.Text = "Location :";
            // 
            // cboLocation
            // 
            this.cboLocation.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.cboLocation.FormattingEnabled = true;
            this.cboLocation.Location = new System.Drawing.Point(106, 68);
            this.cboLocation.Name = "cboLocation";
            this.cboLocation.Size = new System.Drawing.Size(287, 30);
            this.cboLocation.TabIndex = 1;
            // 
            // txtID
            // 
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtID.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtID.Location = new System.Drawing.Point(693, 66);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(41, 31);
            this.txtID.TabIndex = 118;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(423, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 22);
            this.label1.TabIndex = 117;
            this.label1.Text = "Color :";
            // 
            // txtColor
            // 
            this.txtColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtColor.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtColor.Location = new System.Drawing.Point(497, 140);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(162, 31);
            this.txtColor.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel4.Location = new System.Drawing.Point(0, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(749, 6);
            this.panel4.TabIndex = 115;
            // 
            // grpRate
            // 
            this.grpRate.BackColor = System.Drawing.Color.Transparent;
            this.grpRate.Controls.Add(this.txtPerHours);
            this.grpRate.Controls.Add(this.txtPerDay);
            this.grpRate.Controls.Add(this.txtMonth);
            this.grpRate.Controls.Add(this.lalHours);
            this.grpRate.Controls.Add(this.lalDay);
            this.grpRate.Controls.Add(this.lblMonth);
            this.grpRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRate.ForeColor = System.Drawing.Color.Black;
            this.grpRate.Location = new System.Drawing.Point(19, 249);
            this.grpRate.Name = "grpRate";
            this.grpRate.Size = new System.Drawing.Size(625, 57);
            this.grpRate.TabIndex = 8;
            this.grpRate.TabStop = false;
            this.grpRate.Text = "Rate / Per";
            // 
            // txtPerHours
            // 
            this.txtPerHours.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPerHours.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtPerHours.Location = new System.Drawing.Point(83, 19);
            this.txtPerHours.Name = "txtPerHours";
            this.txtPerHours.Size = new System.Drawing.Size(81, 31);
            this.txtPerHours.TabIndex = 22;
            this.txtPerHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPerDay
            // 
            this.txtPerDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPerDay.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtPerDay.Location = new System.Drawing.Point(308, 19);
            this.txtPerDay.Name = "txtPerDay";
            this.txtPerDay.Size = new System.Drawing.Size(81, 31);
            this.txtPerDay.TabIndex = 23;
            this.txtPerDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtMonth
            // 
            this.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMonth.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtMonth.Location = new System.Drawing.Point(497, 19);
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(81, 31);
            this.txtMonth.TabIndex = 24;
            this.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lalHours
            // 
            this.lalHours.AutoSize = true;
            this.lalHours.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.lalHours.ForeColor = System.Drawing.Color.Black;
            this.lalHours.Location = new System.Drawing.Point(11, 23);
            this.lalHours.Name = "lalHours";
            this.lalHours.Size = new System.Drawing.Size(70, 22);
            this.lalHours.TabIndex = 8;
            this.lalHours.Text = "Hours :";
            // 
            // lalDay
            // 
            this.lalDay.AutoSize = true;
            this.lalDay.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.lalDay.ForeColor = System.Drawing.Color.Black;
            this.lalDay.Location = new System.Drawing.Point(244, 23);
            this.lalDay.Name = "lalDay";
            this.lalDay.Size = new System.Drawing.Size(58, 22);
            this.lalDay.TabIndex = 9;
            this.lalDay.Text = "Day :";
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.lblMonth.ForeColor = System.Drawing.Color.Black;
            this.lblMonth.Location = new System.Drawing.Point(411, 23);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(80, 22);
            this.lblMonth.TabIndex = 10;
            this.lblMonth.Text = "Month :";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Tomato;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(627, 316);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(112, 36);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.UseVisualStyleBackColor = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.BackgroundImage")));
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Location = new System.Drawing.Point(495, 315);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(112, 36);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Location = new System.Drawing.Point(363, 313);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 38);
            this.btnSave.TabIndex = 9;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtDesc
            // 
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesc.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtDesc.Location = new System.Drawing.Point(106, 177);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(287, 64);
            this.txtDesc.TabIndex = 7;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblDesc.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.lblDesc.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDesc.Location = new System.Drawing.Point(24, 182);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(79, 22);
            this.lblDesc.TabIndex = 57;
            this.lblDesc.Text = "Details :";
            // 
            // txtPinNo
            // 
            this.txtPinNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPinNo.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtPinNo.Location = new System.Drawing.Point(106, 140);
            this.txtPinNo.Name = "txtPinNo";
            this.txtPinNo.Size = new System.Drawing.Size(287, 31);
            this.txtPinNo.TabIndex = 5;
            // 
            // lblPinNo
            // 
            this.lblPinNo.AutoSize = true;
            this.lblPinNo.BackColor = System.Drawing.Color.Transparent;
            this.lblPinNo.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.lblPinNo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblPinNo.Location = new System.Drawing.Point(22, 149);
            this.lblPinNo.Name = "lblPinNo";
            this.lblPinNo.Size = new System.Drawing.Size(81, 22);
            this.lblPinNo.TabIndex = 55;
            this.lblPinNo.Text = "PIN No :";
            // 
            // btnRfid
            // 
            this.btnRfid.BackColor = System.Drawing.Color.ForestGreen;
            this.btnRfid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnRfid.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRfid.Location = new System.Drawing.Point(693, 103);
            this.btnRfid.Name = "btnRfid";
            this.btnRfid.Size = new System.Drawing.Size(46, 25);
            this.btnRfid.TabIndex = 54;
            this.btnRfid.Text = "RFID";
            this.btnRfid.UseVisualStyleBackColor = false;
            // 
            // txtRFID
            // 
            this.txtRFID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRFID.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtRFID.Location = new System.Drawing.Point(497, 103);
            this.txtRFID.Name = "txtRFID";
            this.txtRFID.Size = new System.Drawing.Size(190, 31);
            this.txtRFID.TabIndex = 4;
            // 
            // lblRfid
            // 
            this.lblRfid.AutoSize = true;
            this.lblRfid.BackColor = System.Drawing.Color.Transparent;
            this.lblRfid.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.lblRfid.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblRfid.Location = new System.Drawing.Point(430, 112);
            this.lblRfid.Name = "lblRfid";
            this.lblRfid.Size = new System.Drawing.Size(60, 22);
            this.lblRfid.TabIndex = 52;
            this.lblRfid.Text = "RFID :";
            // 
            // txtMacId
            // 
            this.txtMacId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMacId.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtMacId.Location = new System.Drawing.Point(106, 103);
            this.txtMacId.Name = "txtMacId";
            this.txtMacId.Size = new System.Drawing.Size(287, 31);
            this.txtMacId.TabIndex = 3;
            // 
            // lblMacId
            // 
            this.lblMacId.AutoSize = true;
            this.lblMacId.BackColor = System.Drawing.Color.Transparent;
            this.lblMacId.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.lblMacId.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblMacId.Location = new System.Drawing.Point(12, 110);
            this.lblMacId.Name = "lblMacId";
            this.lblMacId.Size = new System.Drawing.Size(91, 22);
            this.lblMacId.TabIndex = 50;
            this.lblMacId.Text = "MAC ID :";
            // 
            // lblRowNo
            // 
            this.lblRowNo.AutoSize = true;
            this.lblRowNo.BackColor = System.Drawing.Color.Transparent;
            this.lblRowNo.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.lblRowNo.ForeColor = System.Drawing.Color.Black;
            this.lblRowNo.Location = new System.Drawing.Point(395, 68);
            this.lblRowNo.Name = "lblRowNo";
            this.lblRowNo.Size = new System.Drawing.Size(96, 22);
            this.lblRowNo.TabIndex = 49;
            this.lblRowNo.Text = "Row No. :";
            // 
            // txtRowNo
            // 
            this.txtRowNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRowNo.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtRowNo.Location = new System.Drawing.Point(497, 68);
            this.txtRowNo.Name = "txtRowNo";
            this.txtRowNo.Size = new System.Drawing.Size(190, 31);
            this.txtRowNo.TabIndex = 2;
            // 
            // txtLocker
            // 
            this.txtLocker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocker.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.txtLocker.Location = new System.Drawing.Point(106, 31);
            this.txtLocker.Name = "txtLocker";
            this.txtLocker.Size = new System.Drawing.Size(287, 31);
            this.txtLocker.TabIndex = 0;
            // 
            // lblLocker
            // 
            this.lblLocker.AutoSize = true;
            this.lblLocker.BackColor = System.Drawing.Color.Transparent;
            this.lblLocker.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocker.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblLocker.Location = new System.Drawing.Point(22, 40);
            this.lblLocker.Name = "lblLocker";
            this.lblLocker.Size = new System.Drawing.Size(81, 22);
            this.lblLocker.TabIndex = 46;
            this.lblLocker.Text = "Locker :";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.BackColor = System.Drawing.Color.Transparent;
            this.label.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.ForeColor = System.Drawing.Color.Black;
            this.label.Location = new System.Drawing.Point(3, 8);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(132, 26);
            this.label.TabIndex = 44;
            this.label.Text = "Locker Details";
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToOrderColumns = true;
            this.dgvList.AllowUserToResizeColumns = false;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.cnLocker,
            this.cnLocation,
            this.cnRowno,
            this.cnMacID,
            this.cnRFID,
            this.cnColor,
            this.cnPINNo,
            this.cnLockDesc,
            this.cnHrRate,
            this.cnDyRate,
            this.cnMnRate,
            this.cbLocationID});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvList.Location = new System.Drawing.Point(6, 62);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvList.RowHeadersVisible = false;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvList.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvList.RowTemplate.Height = 25;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(1098, 407);
            this.dgvList.TabIndex = 115;
            this.dgvList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvList_CellMouseDoubleClick);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.MaxInputLength = 15;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // cnLocker
            // 
            this.cnLocker.HeaderText = "Locker";
            this.cnLocker.MaxInputLength = 50;
            this.cnLocker.Name = "cnLocker";
            this.cnLocker.ReadOnly = true;
            this.cnLocker.Width = 200;
            // 
            // cnLocation
            // 
            this.cnLocation.HeaderText = "Location";
            this.cnLocation.Name = "cnLocation";
            this.cnLocation.ReadOnly = true;
            // 
            // cnRowno
            // 
            this.cnRowno.HeaderText = "Row No";
            this.cnRowno.Name = "cnRowno";
            this.cnRowno.ReadOnly = true;
            // 
            // cnMacID
            // 
            this.cnMacID.HeaderText = "MacID";
            this.cnMacID.MaxInputLength = 50;
            this.cnMacID.Name = "cnMacID";
            this.cnMacID.ReadOnly = true;
            // 
            // cnRFID
            // 
            this.cnRFID.HeaderText = "RFID";
            this.cnRFID.MaxInputLength = 50;
            this.cnRFID.Name = "cnRFID";
            this.cnRFID.ReadOnly = true;
            // 
            // cnColor
            // 
            this.cnColor.HeaderText = "Color";
            this.cnColor.MaxInputLength = 50;
            this.cnColor.Name = "cnColor";
            this.cnColor.ReadOnly = true;
            // 
            // cnPINNo
            // 
            this.cnPINNo.HeaderText = "PIN No";
            this.cnPINNo.MaxInputLength = 50;
            this.cnPINNo.Name = "cnPINNo";
            this.cnPINNo.ReadOnly = true;
            // 
            // cnLockDesc
            // 
            this.cnLockDesc.HeaderText = "LockDesc";
            this.cnLockDesc.MaxInputLength = 200;
            this.cnLockDesc.Name = "cnLockDesc";
            this.cnLockDesc.ReadOnly = true;
            this.cnLockDesc.Width = 200;
            // 
            // cnHrRate
            // 
            this.cnHrRate.HeaderText = "Hour/Rate";
            this.cnHrRate.MaxInputLength = 12;
            this.cnHrRate.Name = "cnHrRate";
            this.cnHrRate.ReadOnly = true;
            // 
            // cnDyRate
            // 
            this.cnDyRate.HeaderText = "Day/Rate";
            this.cnDyRate.MaxInputLength = 12;
            this.cnDyRate.Name = "cnDyRate";
            this.cnDyRate.ReadOnly = true;
            // 
            // cnMnRate
            // 
            this.cnMnRate.HeaderText = "Month/Rate";
            this.cnMnRate.MaxInputLength = 12;
            this.cnMnRate.Name = "cnMnRate";
            this.cnMnRate.ReadOnly = true;
            // 
            // cbLocationID
            // 
            this.cbLocationID.HeaderText = "LocnID";
            this.cbLocationID.Name = "cbLocationID";
            this.cbLocationID.ReadOnly = true;
            // 
            // btnColorPlate
            // 
            this.btnColorPlate.BackColor = System.Drawing.Color.Transparent;
            this.btnColorPlate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnColorPlate.BackgroundImage")));
            this.btnColorPlate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnColorPlate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnColorPlate.Location = new System.Drawing.Point(660, 140);
            this.btnColorPlate.Name = "btnColorPlate";
            this.btnColorPlate.Size = new System.Drawing.Size(27, 31);
            this.btnColorPlate.TabIndex = 121;
            this.btnColorPlate.UseVisualStyleBackColor = false;
            this.btnColorPlate.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // FrmLocker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1107, 494);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmLocker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLocker";
            this.Load += new System.EventHandler(this.FrmLocker_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLocker_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlLocker.ResumeLayout(false);
            this.pnlLocker.PerformLayout();
            this.grpRate.ResumeLayout(false);
            this.grpRate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button btnFrmClose;
        private System.Windows.Forms.Panel pnlLocker;
        private System.Windows.Forms.GroupBox grpRate;
        private System.Windows.Forms.TextBox txtPerHours;
        private System.Windows.Forms.TextBox txtPerDay;
        private System.Windows.Forms.TextBox txtMonth;
        private System.Windows.Forms.Label lalHours;
        private System.Windows.Forms.Label lalDay;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtPinNo;
        private System.Windows.Forms.Label lblPinNo;
        private System.Windows.Forms.Button btnRfid;
        private System.Windows.Forms.TextBox txtRFID;
        private System.Windows.Forms.Label lblRfid;
        private System.Windows.Forms.TextBox txtMacId;
        private System.Windows.Forms.Label lblMacId;
        private System.Windows.Forms.Label lblRowNo;
        private System.Windows.Forms.TextBox txtRowNo;
        private System.Windows.Forms.TextBox txtLocker;
        private System.Windows.Forms.Label lblLocker;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboLocation;
        private System.Windows.Forms.Label lblTotLocker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mtxtFieldValue;
        private System.Windows.Forms.ComboBox cboField;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnLocker;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnRowno;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnMacID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnRFID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnPINNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnLockDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnHrRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnDyRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnMnRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cbLocationID;
        private System.Windows.Forms.Button btnColorPlate;
    }
}