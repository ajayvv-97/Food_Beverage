namespace pLocker.Forms
{
    partial class FrmHireList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHireList));
            this.btnFrmClose = new System.Windows.Forms.Button();
            this.lblTotLocker = new System.Windows.Forms.Label();
            this.cboField = new System.Windows.Forms.ComboBox();
            this.dtpEnrollTo = new System.Windows.Forms.DateTimePicker();
            this.dtpEnrollFrom = new System.Windows.Forms.DateTimePicker();
            this.mtxtFieldValue = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.LedgerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnrollDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LockId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Locker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LedgerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dob = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mobile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmailId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HireFor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HireDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExpDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LockHireId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_bulk = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnMin = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnFrmClose
            // 
            this.btnFrmClose.BackColor = System.Drawing.Color.Transparent;
            this.btnFrmClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFrmClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFrmClose.ForeColor = System.Drawing.Color.Red;
            this.btnFrmClose.Location = new System.Drawing.Point(951, 0);
            this.btnFrmClose.Name = "btnFrmClose";
            this.btnFrmClose.Size = new System.Drawing.Size(26, 30);
            this.btnFrmClose.TabIndex = 71;
            this.btnFrmClose.Text = "X";
            this.btnFrmClose.UseVisualStyleBackColor = false;
            this.btnFrmClose.Click += new System.EventHandler(this.btnFrmClose_Click);
            // 
            // lblTotLocker
            // 
            this.lblTotLocker.AutoSize = true;
            this.lblTotLocker.BackColor = System.Drawing.Color.Transparent;
            this.lblTotLocker.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotLocker.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTotLocker.Location = new System.Drawing.Point(0, 497);
            this.lblTotLocker.Name = "lblTotLocker";
            this.lblTotLocker.Size = new System.Drawing.Size(17, 16);
            this.lblTotLocker.TabIndex = 170;
            this.lblTotLocker.Text = "...";
            // 
            // cboField
            // 
            this.cboField.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboField.FormattingEnabled = true;
            this.cboField.Location = new System.Drawing.Point(310, 77);
            this.cboField.Name = "cboField";
            this.cboField.Size = new System.Drawing.Size(161, 33);
            this.cboField.TabIndex = 2;
            this.cboField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // dtpEnrollTo
            // 
            this.dtpEnrollTo.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEnrollTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnrollTo.Location = new System.Drawing.Point(160, 78);
            this.dtpEnrollTo.Name = "dtpEnrollTo";
            this.dtpEnrollTo.Size = new System.Drawing.Size(124, 33);
            this.dtpEnrollTo.TabIndex = 1;
            this.dtpEnrollTo.ValueChanged += new System.EventHandler(this.dtpEnrollTo_ValueChanged);
            this.dtpEnrollTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // dtpEnrollFrom
            // 
            this.dtpEnrollFrom.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEnrollFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnrollFrom.Location = new System.Drawing.Point(11, 78);
            this.dtpEnrollFrom.Name = "dtpEnrollFrom";
            this.dtpEnrollFrom.Size = new System.Drawing.Size(124, 33);
            this.dtpEnrollFrom.TabIndex = 0;
            this.dtpEnrollFrom.ValueChanged += new System.EventHandler(this.dtpEnrollFrom_ValueChanged);
            this.dtpEnrollFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // mtxtFieldValue
            // 
            this.mtxtFieldValue.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtFieldValue.Location = new System.Drawing.Point(488, 77);
            this.mtxtFieldValue.Multiline = true;
            this.mtxtFieldValue.Name = "mtxtFieldValue";
            this.mtxtFieldValue.Size = new System.Drawing.Size(194, 31);
            this.mtxtFieldValue.TabIndex = 3;
            this.mtxtFieldValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(3, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 25);
            this.label2.TabIndex = 162;
            this.label2.Text = "EntryDate:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(303, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 25);
            this.label1.TabIndex = 165;
            this.label1.Text = "Search In:";
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToOrderColumns = true;
            this.dgvList.AllowUserToResizeColumns = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dgvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.BackgroundColor = System.Drawing.Color.White;
            this.dgvList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LedgerID,
            this.EnrollDate,
            this.LedgerCode,
            this.LockId,
            this.Locker,
            this.LedgerName,
            this.dob,
            this.Mobile,
            this.EmailId,
            this.HireFor,
            this.HireDate,
            this.ExpDate,
            this.Rate,
            this.Amt,
            this.LockHireId});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvList.Location = new System.Drawing.Point(8, 114);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvList.RowHeadersVisible = false;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvList.RowTemplate.Height = 25;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(962, 389);
            this.dgvList.TabIndex = 6;
            this.dgvList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellDoubleClick);
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
            this.LedgerCode.HeaderText = "A/cNo";
            this.LedgerCode.MaxInputLength = 50;
            this.LedgerCode.Name = "LedgerCode";
            this.LedgerCode.ReadOnly = true;
            this.LedgerCode.Width = 200;
            // 
            // LockId
            // 
            this.LockId.HeaderText = "LockId";
            this.LockId.Name = "LockId";
            this.LockId.ReadOnly = true;
            // 
            // Locker
            // 
            this.Locker.HeaderText = "Locker";
            this.Locker.Name = "Locker";
            this.Locker.ReadOnly = true;
            // 
            // LedgerName
            // 
            this.LedgerName.HeaderText = "CustomerName";
            this.LedgerName.Name = "LedgerName";
            this.LedgerName.ReadOnly = true;
            // 
            // dob
            // 
            this.dob.HeaderText = "DOB";
            this.dob.Name = "dob";
            this.dob.ReadOnly = true;
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
            // HireFor
            // 
            this.HireFor.HeaderText = "HireFor";
            this.HireFor.Name = "HireFor";
            this.HireFor.ReadOnly = true;
            // 
            // HireDate
            // 
            this.HireDate.HeaderText = "HireDate";
            this.HireDate.Name = "HireDate";
            this.HireDate.ReadOnly = true;
            // 
            // ExpDate
            // 
            this.ExpDate.HeaderText = "ExpDate";
            this.ExpDate.Name = "ExpDate";
            this.ExpDate.ReadOnly = true;
            // 
            // Rate
            // 
            this.Rate.HeaderText = "Rate";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            // 
            // Amt
            // 
            this.Amt.HeaderText = "Amt";
            this.Amt.Name = "Amt";
            this.Amt.ReadOnly = true;
            // 
            // LockHireId
            // 
            this.LockHireId.HeaderText = "LockHireId";
            this.LockHireId.Name = "LockHireId";
            this.LockHireId.ReadOnly = true;
            this.LockHireId.Visible = false;
            // 
            // btn_bulk
            // 
            this.btn_bulk.BackColor = System.Drawing.Color.Transparent;
            this.btn_bulk.BackgroundImage = global::pLocker.Properties.Resources.Blank_button;
            this.btn_bulk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_bulk.FlatAppearance.BorderSize = 0;
            this.btn_bulk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_bulk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_bulk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_bulk.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_bulk.Location = new System.Drawing.Point(688, 64);
            this.btn_bulk.Name = "btn_bulk";
            this.btn_bulk.Size = new System.Drawing.Size(132, 44);
            this.btn_bulk.TabIndex = 172;
            this.btn_bulk.Text = "Bulk Locker";
            this.btn_bulk.UseVisualStyleBackColor = false;
            this.btn_bulk.Click += new System.EventHandler(this.btn_bulk_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(4)))), ((int)(((byte)(209)))));
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.Controls.Add(this.btnNew);
            this.panel1.Controls.Add(this.btnMin);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnclose);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(978, 46);
            this.panel1.TabIndex = 171;
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Transparent;
            this.btnNew.BackgroundImage = global::pLocker.Properties.Resources.New_1;
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(766, 0);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(132, 44);
            this.btnNew.TabIndex = 5;
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Visible = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            this.btnNew.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            this.btnNew.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnNew_MouseDown);
            this.btnNew.MouseLeave += new System.EventHandler(this.btnNew_MouseLeave);
            this.btnNew.MouseHover += new System.EventHandler(this.btnNew_MouseHover);
            this.btnNew.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnNew_MouseUp);
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
            this.btnMin.Location = new System.Drawing.Point(924, -1);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(27, 30);
            this.btnMin.TabIndex = 175;
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Click += new System.EventHandler(this.Minimize);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
            this.label8.Location = new System.Drawing.Point(3, 3);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 25);
            this.label8.TabIndex = 161;
            this.label8.Text = " Hire List";
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.Transparent;
            this.btnclose.BackgroundImage = global::pLocker.Properties.Resources.close_1_button;
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclose.FlatAppearance.BorderSize = 0;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.Transparent;
            this.btnclose.Location = new System.Drawing.Point(952, -1);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(26, 31);
            this.btnclose.TabIndex = 159;
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImage = global::pLocker.Properties.Resources.search_1;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(832, 64);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(132, 44);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            this.btnSearch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSearch_MouseDown);
            this.btnSearch.MouseLeave += new System.EventHandler(this.btnSearch_MouseLeave);
            this.btnSearch.MouseHover += new System.EventHandler(this.btnSearch_MouseHover);
            this.btnSearch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnSearch_MouseUp);
            // 
            // FrmHireList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(977, 516);
            this.Controls.Add(this.btn_bulk);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblTotLocker);
            this.Controls.Add(this.cboField);
            this.Controls.Add(this.dtpEnrollTo);
            this.Controls.Add(this.dtpEnrollFrom);
            this.Controls.Add(this.mtxtFieldValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.btnFrmClose);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmHireList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmKeypad1";
            this.Load += new System.EventHandler(this.FrmKeypad1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmHireList_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFrmClose;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblTotLocker;
        private System.Windows.Forms.ComboBox cboField;
        private System.Windows.Forms.DateTimePicker dtpEnrollTo;
        private System.Windows.Forms.DateTimePicker dtpEnrollFrom;
        private System.Windows.Forms.TextBox mtxtFieldValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnrollDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LockId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Locker;
        private System.Windows.Forms.DataGridViewTextBoxColumn LedgerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dob;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mobile;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmailId;
        private System.Windows.Forms.DataGridViewTextBoxColumn HireFor;
        private System.Windows.Forms.DataGridViewTextBoxColumn HireDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExpDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amt;
        private System.Windows.Forms.DataGridViewTextBoxColumn LockHireId;
        private System.Windows.Forms.Button btn_bulk;
    }
}