namespace MarginPOSNew.Forms.Transactions
{
    partial class FrmOffer
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblfrm = new System.Windows.Forms.Label();
            this.lblto = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtoffer = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnsearch = new System.Windows.Forms.Button();
            this.dTPfrm = new System.Windows.Forms.DateTimePicker();
            this.dTPto = new System.Windows.Forms.DateTimePicker();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvnewgrid = new System.Windows.Forms.DataGridView();
            this.sno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BarCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OffrRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscPer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvnewgrid)).BeginInit();
            this.SuspendLayout();
            // 
            // lblfrm
            // 
            this.lblfrm.AutoSize = true;
            this.lblfrm.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblfrm.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblfrm.ForeColor = System.Drawing.Color.Black;
            this.lblfrm.Location = new System.Drawing.Point(39, 55);
            this.lblfrm.Name = "lblfrm";
            this.lblfrm.Size = new System.Drawing.Size(60, 23);
            this.lblfrm.TabIndex = 0;
            this.lblfrm.Text = "From :";
            // 
            // lblto
            // 
            this.lblto.AutoSize = true;
            this.lblto.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblto.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblto.Location = new System.Drawing.Point(244, 57);
            this.lblto.Name = "lblto";
            this.lblto.Size = new System.Drawing.Size(36, 23);
            this.lblto.TabIndex = 1;
            this.lblto.Text = "To :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "Offer Title :";
            // 
            // txtoffer
            // 
            this.txtoffer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtoffer.Location = new System.Drawing.Point(105, 97);
            this.txtoffer.Name = "txtoffer";
            this.txtoffer.Size = new System.Drawing.Size(322, 26);
            this.txtoffer.TabIndex = 55;
            this.txtoffer.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txtoffer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtoffer_KeyDown);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnSave.Location = new System.Drawing.Point(619, 76);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(129, 45);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "&Save[F6]";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnsearch
            // 
            this.btnsearch.BackColor = System.Drawing.Color.LimeGreen;
            this.btnsearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnsearch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearch.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnsearch.Location = new System.Drawing.Point(754, 76);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(129, 45);
            this.btnsearch.TabIndex = 8;
            this.btnsearch.Text = "&Search[F3]";
            this.btnsearch.UseVisualStyleBackColor = false;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // dTPfrm
            // 
            this.dTPfrm.CustomFormat = "dd-MM-yyyy";
            this.dTPfrm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dTPfrm.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTPfrm.Location = new System.Drawing.Point(105, 54);
            this.dTPfrm.MaxDate = new System.DateTime(3000, 12, 31, 0, 0, 0, 0);
            this.dTPfrm.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dTPfrm.MinimumSize = new System.Drawing.Size(4, 29);
            this.dTPfrm.Name = "dTPfrm";
            this.dTPfrm.Size = new System.Drawing.Size(131, 29);
            this.dTPfrm.TabIndex = 53;
            this.dTPfrm.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            this.dTPfrm.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dTPfrm_KeyDown);
            this.dTPfrm.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dTPfrm_KeyUp);
            // 
            // dTPto
            // 
            this.dTPto.CustomFormat = "dd-MM-yyyy";
            this.dTPto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dTPto.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dTPto.Location = new System.Drawing.Point(286, 54);
            this.dTPto.MaxDate = new System.DateTime(3000, 12, 31, 0, 0, 0, 0);
            this.dTPto.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dTPto.MinimumSize = new System.Drawing.Size(4, 29);
            this.dTPto.Name = "dTPto";
            this.dTPto.Size = new System.Drawing.Size(131, 29);
            this.dTPto.TabIndex = 54;
            this.dTPto.ValueChanged += new System.EventHandler(this.dateTimePicker2_ValueChanged);
            this.dTPto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dTPto_KeyDown);
            this.dTPto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dTPto_KeyUp);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Red;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnRefresh.Location = new System.Drawing.Point(889, 76);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(129, 45);
            this.btnRefresh.TabIndex = 57;
            this.btnRefresh.Text = "&Refresh[F5]";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnClose.Location = new System.Drawing.Point(1010, -1);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(21, 26);
            this.btnClose.TabIndex = 57;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvnewgrid
            // 
            this.dgvnewgrid.AllowUserToDeleteRows = false;
            this.dgvnewgrid.AllowUserToResizeColumns = false;
            this.dgvnewgrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvnewgrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvnewgrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvnewgrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvnewgrid.ColumnHeadersHeight = 30;
            this.dgvnewgrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sno,
            this.ItemId,
            this.ItemCode,
            this.BarCode,
            this.ItemName,
            this.PRate,
            this.SRate,
            this.MRP,
            this.OffrRate,
            this.DiscRate,
            this.DiscPer});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvnewgrid.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvnewgrid.Location = new System.Drawing.Point(12, 151);
            this.dgvnewgrid.Name = "dgvnewgrid";
            this.dgvnewgrid.RowHeadersVisible = false;
            this.dgvnewgrid.Size = new System.Drawing.Size(1006, 415);
            this.dgvnewgrid.TabIndex = 58;
            this.dgvnewgrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvnewgrid_CellDoubleClick_1);
            this.dgvnewgrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvnewgrid_CellEndEdit);
            this.dgvnewgrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvnewgrid_KeyDown);
            // 
            // sno
            // 
            this.sno.HeaderText = "SlNo";
            this.sno.MaxInputLength = 30;
            this.sno.Name = "sno";
            this.sno.Width = 45;
            // 
            // ItemId
            // 
            this.ItemId.HeaderText = "ItemId";
            this.ItemId.Name = "ItemId";
            this.ItemId.Visible = false;
            this.ItemId.Width = 86;
            // 
            // ItemCode
            // 
            this.ItemCode.HeaderText = "ItemCode";
            this.ItemCode.Name = "ItemCode";
            this.ItemCode.Visible = false;
            this.ItemCode.Width = 86;
            // 
            // BarCode
            // 
            this.BarCode.HeaderText = "Barcode";
            this.BarCode.MaxInputLength = 30;
            this.BarCode.Name = "BarCode";
            this.BarCode.Width = 105;
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Item";
            this.ItemName.MaxInputLength = 30;
            this.ItemName.Name = "ItemName";
            this.ItemName.ReadOnly = true;
            this.ItemName.Width = 180;
            // 
            // PRate
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.PRate.DefaultCellStyle = dataGridViewCellStyle3;
            this.PRate.HeaderText = "Pur.Rate";
            this.PRate.Name = "PRate";
            this.PRate.ReadOnly = true;
            this.PRate.Width = 105;
            // 
            // SRate
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SRate.DefaultCellStyle = dataGridViewCellStyle4;
            this.SRate.HeaderText = "Sale Rate";
            this.SRate.Name = "SRate";
            this.SRate.ReadOnly = true;
            this.SRate.Width = 105;
            // 
            // MRP
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.MRP.DefaultCellStyle = dataGridViewCellStyle5;
            this.MRP.HeaderText = "MRP";
            this.MRP.Name = "MRP";
            this.MRP.ReadOnly = true;
            this.MRP.Width = 105;
            // 
            // OffrRate
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.OffrRate.DefaultCellStyle = dataGridViewCellStyle6;
            this.OffrRate.HeaderText = "Offer Rate";
            this.OffrRate.Name = "OffrRate";
            this.OffrRate.Width = 106;
            // 
            // DiscRate
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscRate.DefaultCellStyle = dataGridViewCellStyle7;
            this.DiscRate.HeaderText = "Disc Rate";
            this.DiscRate.Name = "DiscRate";
            this.DiscRate.Width = 105;
            // 
            // DiscPer
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscPer.DefaultCellStyle = dataGridViewCellStyle8;
            this.DiscPer.HeaderText = "Disc %";
            this.DiscPer.Name = "DiscPer";
            this.DiscPer.Width = 105;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(515, 97);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(98, 26);
            this.textBox1.TabIndex = 60;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(432, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 23);
            this.label2.TabIndex = 59;
            this.label2.Text = "Bill Amt :";
            // 
            // FrmOffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.HighlightText;
            this.ClientSize = new System.Drawing.Size(1030, 593);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvnewgrid);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dTPto);
            this.Controls.Add(this.dTPfrm);
            this.Controls.Add(this.btnsearch);
            this.Controls.Add(this.txtoffer);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblfrm);
            this.Controls.Add(this.lblto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmOffer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Frmnew";
            this.Load += new System.EventHandler(this.FrmOffer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmOffer_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FrmOffer_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmOffer_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvnewgrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblfrm;
        private System.Windows.Forms.Label lblto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtoffer;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.DateTimePicker dTPfrm;
        private System.Windows.Forms.DateTimePicker dTPto;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvnewgrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn sno;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn BarCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn MRP;
        private System.Windows.Forms.DataGridViewTextBoxColumn OffrRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscPer;
    }
}