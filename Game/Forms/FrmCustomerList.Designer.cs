namespace pLocker.Forms
{
    partial class FrmCustomerList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCustomerList));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mtxtFieldValue = new System.Windows.Forms.TextBox();
            this.dtpEnrollFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpEnrollTo = new System.Windows.Forms.DateTimePicker();
            this.cboField = new System.Windows.Forms.ComboBox();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.LedgerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnrollDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmailId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cEmergency = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Zipcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.City = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cCountry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dob = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProofNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cInstitute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Occupation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotLocker = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(448, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Search In:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "EnrollDate:";
            // 
            // mtxtFieldValue
            // 
            this.mtxtFieldValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtFieldValue.Location = new System.Drawing.Point(693, 96);
            this.mtxtFieldValue.Multiline = true;
            this.mtxtFieldValue.Name = "mtxtFieldValue";
            this.mtxtFieldValue.Size = new System.Drawing.Size(130, 31);
            this.mtxtFieldValue.TabIndex = 18;
            // 
            // dtpEnrollFrom
            // 
            this.dtpEnrollFrom.CalendarMonthBackground = System.Drawing.Color.Transparent;
            this.dtpEnrollFrom.CalendarTitleBackColor = System.Drawing.Color.Transparent;
            this.dtpEnrollFrom.CalendarTitleForeColor = System.Drawing.Color.Transparent;
            this.dtpEnrollFrom.CalendarTrailingForeColor = System.Drawing.Color.Transparent;
            this.dtpEnrollFrom.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEnrollFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnrollFrom.Location = new System.Drawing.Point(110, 97);
            this.dtpEnrollFrom.Name = "dtpEnrollFrom";
            this.dtpEnrollFrom.Size = new System.Drawing.Size(143, 31);
            this.dtpEnrollFrom.TabIndex = 154;
            // 
            // dtpEnrollTo
            // 
            this.dtpEnrollTo.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEnrollTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnrollTo.Location = new System.Drawing.Point(265, 97);
            this.dtpEnrollTo.Name = "dtpEnrollTo";
            this.dtpEnrollTo.Size = new System.Drawing.Size(151, 31);
            this.dtpEnrollTo.TabIndex = 155;
            // 
            // cboField
            // 
            this.cboField.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.cboField.FormattingEnabled = true;
            this.cboField.Location = new System.Drawing.Point(539, 96);
            this.cboField.Name = "cboField";
            this.cboField.Size = new System.Drawing.Size(148, 30);
            this.cboField.TabIndex = 156;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToOrderColumns = true;
            this.dgvList.AllowUserToResizeColumns = false;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LedgerID,
            this.EnrollDate,
            this.LedgerCode,
            this.LedgerName,
            this.Mobile,
            this.EmailId,
            this.cEmergency,
            this.LedgerAddress,
            this.Zipcode,
            this.State,
            this.City,
            this.cCountry,
            this.dob,
            this.ProofNo,
            this.cInstitute,
            this.Occupation});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvList.GridColor = System.Drawing.Color.Gray;
            this.dgvList.Location = new System.Drawing.Point(3, 134);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvList.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvList.RowTemplate.Height = 25;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(1053, 368);
            this.dgvList.TabIndex = 157;
            // 
            // LedgerID
            // 
            this.LedgerID.HeaderText = "ID";
            this.LedgerID.MaxInputLength = 15;
            this.LedgerID.Name = "LedgerID";
            this.LedgerID.ReadOnly = true;
            this.LedgerID.Visible = false;
            // 
            // EnrollDate
            // 
            this.EnrollDate.HeaderText = "EnrollDate";
            this.EnrollDate.Name = "EnrollDate";
            this.EnrollDate.ReadOnly = true;
            // 
            // LedgerCode
            // 
            this.LedgerCode.HeaderText = "AccNo";
            this.LedgerCode.MaxInputLength = 50;
            this.LedgerCode.Name = "LedgerCode";
            this.LedgerCode.ReadOnly = true;
            this.LedgerCode.Width = 200;
            // 
            // LedgerName
            // 
            this.LedgerName.HeaderText = "AccName";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.ReadOnly = true;
            // 
            // Mobile
            // 
            this.Mobile.HeaderText = "Mobile";
            this.Mobile.MaxInputLength = 50;
            this.Mobile.Name = "Mobile";
            this.Mobile.ReadOnly = true;
            // 
            // EmailId
            // 
            this.EmailId.HeaderText = "EmailId";
            this.EmailId.Name = "EmailId";
            this.EmailId.ReadOnly = true;
            // 
            // cEmergency
            // 
            this.cEmergency.HeaderText = "Emergency";
            this.cEmergency.MaxInputLength = 200;
            this.cEmergency.Name = "cEmergency";
            this.cEmergency.ReadOnly = true;
            this.cEmergency.Width = 200;
            // 
            // LedgerAddress
            // 
            this.LedgerAddress.HeaderText = "Address";
            this.LedgerAddress.Name = "LedgerAddress";
            this.LedgerAddress.ReadOnly = true;
            // 
            // Zipcode
            // 
            this.Zipcode.HeaderText = "Zipcode";
            this.Zipcode.MaxInputLength = 50;
            this.Zipcode.Name = "Zipcode";
            this.Zipcode.ReadOnly = true;
            // 
            // State
            // 
            this.State.HeaderText = "State";
            this.State.MaxInputLength = 12;
            this.State.Name = "State";
            this.State.ReadOnly = true;
            // 
            // City
            // 
            this.City.HeaderText = "City";
            this.City.MaxInputLength = 12;
            this.City.Name = "City";
            this.City.ReadOnly = true;
            // 
            // cCountry
            // 
            this.cCountry.HeaderText = "Country";
            this.cCountry.MaxInputLength = 12;
            this.cCountry.Name = "cCountry";
            this.cCountry.ReadOnly = true;
            // 
            // dob
            // 
            this.dob.HeaderText = "dob";
            this.dob.Name = "dob";
            this.dob.ReadOnly = true;
            // 
            // ProofNo
            // 
            this.ProofNo.HeaderText = "ProofNo";
            this.ProofNo.Name = "ProofNo";
            this.ProofNo.ReadOnly = true;
            // 
            // cInstitute
            // 
            this.cInstitute.HeaderText = "Institute";
            this.cInstitute.MaxInputLength = 50;
            this.cInstitute.Name = "cInstitute";
            this.cInstitute.ReadOnly = true;
            // 
            // Occupation
            // 
            this.Occupation.HeaderText = "Occupation";
            this.Occupation.MaxInputLength = 50;
            this.Occupation.Name = "Occupation";
            this.Occupation.ReadOnly = true;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.CadetBlue;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(890, 93);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(112, 36);
            this.btnSearch.TabIndex = 159;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(832, 96);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(31, 32);
            this.btnAdd.TabIndex = 160;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(3, -3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1072, 78);
            this.panel1.TabIndex = 161;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("OpenSymbol", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(28, 50);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(252, 19);
            this.label10.TabIndex = 16;
            this.label10.Text = "EXISTING CUSTOMER DETAILS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 27.75F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(24, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(263, 45);
            this.label3.TabIndex = 6;
            this.label3.Text = "CUSTOMER LIST";
            // 
            // lblTotLocker
            // 
            this.lblTotLocker.AutoSize = true;
            this.lblTotLocker.BackColor = System.Drawing.Color.Transparent;
            this.lblTotLocker.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotLocker.ForeColor = System.Drawing.Color.White;
            this.lblTotLocker.Location = new System.Drawing.Point(24, 487);
            this.lblTotLocker.Name = "lblTotLocker";
            this.lblTotLocker.Size = new System.Drawing.Size(0, 24);
            this.lblTotLocker.TabIndex = 162;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkGray;
            this.panel2.Location = new System.Drawing.Point(-4, 71);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1090, 4);
            this.panel2.TabIndex = 163;
            // 
            // FrmCustomerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1074, 608);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblTotLocker);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.cboField);
            this.Controls.Add(this.dtpEnrollTo);
            this.Controls.Add(this.dtpEnrollFrom);
            this.Controls.Add(this.mtxtFieldValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmCustomerList";
            this.Load += new System.EventHandler(this.FrmLockerList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox mtxtFieldValue;
        private System.Windows.Forms.DateTimePicker dtpEnrollFrom;
        private System.Windows.Forms.DateTimePicker dtpEnrollTo;
        private System.Windows.Forms.ComboBox cboField;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnrollDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmailId;
        private System.Windows.Forms.DataGridViewTextBoxColumn cEmergency;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn Zipcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn City;
        private System.Windows.Forms.DataGridViewTextBoxColumn cCountry;
        private System.Windows.Forms.DataGridViewTextBoxColumn dob;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProofNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cInstitute;
        private System.Windows.Forms.DataGridViewTextBoxColumn Occupation;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotLocker;
        private System.Windows.Forms.Panel panel2;
    }
}