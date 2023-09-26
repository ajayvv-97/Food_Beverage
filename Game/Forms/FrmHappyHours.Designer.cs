namespace Game.Forms
{
    partial class FrmHappyHours
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHappyHours));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlhead = new System.Windows.Forms.Panel();
            this.lblsubhead = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblhead = new System.Windows.Forms.Label();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txttitle = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.day = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.from = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.to = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dur = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discountper = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VIPrate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vipdiscountrate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VIPDiscountPercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dayid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbgame = new MetroFramework.Controls.MetroComboBox();
            this.pnlhead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlhead
            // 
            this.pnlhead.BackColor = System.Drawing.Color.Transparent;
            this.pnlhead.Controls.Add(this.lblsubhead);
            this.pnlhead.Controls.Add(this.panel3);
            this.pnlhead.Controls.Add(this.lblhead);
            this.pnlhead.Location = new System.Drawing.Point(0, 2);
            this.pnlhead.Name = "pnlhead";
            this.pnlhead.Size = new System.Drawing.Size(1083, 81);
            this.pnlhead.TabIndex = 1;
            // 
            // lblsubhead
            // 
            this.lblsubhead.AutoSize = true;
            this.lblsubhead.Font = new System.Drawing.Font("OpenSymbol", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsubhead.ForeColor = System.Drawing.Color.White;
            this.lblsubhead.Location = new System.Drawing.Point(29, 42);
            this.lblsubhead.Name = "lblsubhead";
            this.lblsubhead.Size = new System.Drawing.Size(281, 19);
            this.lblsubhead.TabIndex = 204;
            this.lblsubhead.Text = "OFFER BASED ON SPECIFIC TIME";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkGray;
            this.panel3.Location = new System.Drawing.Point(-3, 74);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1054, 5);
            this.panel3.TabIndex = 203;
            // 
            // lblhead
            // 
            this.lblhead.AutoSize = true;
            this.lblhead.BackColor = System.Drawing.Color.Transparent;
            this.lblhead.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblhead.ForeColor = System.Drawing.Color.White;
            this.lblhead.Location = new System.Drawing.Point(29, 5);
            this.lblhead.Name = "lblhead";
            this.lblhead.Size = new System.Drawing.Size(197, 32);
            this.lblhead.TabIndex = 202;
            this.lblhead.Text = "HAPPY HOURS";
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
            this.day,
            this.from,
            this.to,
            this.dur,
            this.CardRate,
            this.DiscountRate,
            this.Discountper,
            this.VIPrate,
            this.Vipdiscountrate,
            this.VIPDiscountPercentage,
            this.dayid});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvList.GridColor = System.Drawing.Color.Gray;
            this.dgvList.Location = new System.Drawing.Point(5, 143);
            this.dgvList.Name = "dgvList";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvList.RowHeadersVisible = false;
            this.dgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvList.RowTemplate.Height = 25;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(1071, 302);
            this.dgvList.TabIndex = 158;
            this.dgvList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellEndEdit);
            this.dgvList.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellLeave);
            this.dgvList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvList_CellMouseDown);
            this.dgvList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellValueChanged);
            this.dgvList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvList_KeyDown);
       
            this.dgvList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvList_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(29, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 24);
            this.label2.TabIndex = 159;
            this.label2.Text = "Game :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(302, 109);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 24);
            this.label1.TabIndex = 161;
            this.label1.Text = "Title :";
            // 
            // txttitle
            // 
            this.txttitle.Location = new System.Drawing.Point(358, 107);
            this.txttitle.Multiline = true;
            this.txttitle.Name = "txttitle";
            this.txttitle.Size = new System.Drawing.Size(206, 30);
            this.txttitle.TabIndex = 162;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(791, 448);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(173, 55);
            this.button2.TabIndex = 223;
            this.button2.Text = "SAVE";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // day
            // 
            this.day.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.day.FillWeight = 6.600313F;
            this.day.HeaderText = "Day";
            this.day.Name = "day";
            this.day.ReadOnly = true;
            this.day.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // from
            // 
            dataGridViewCellStyle2.NullValue = null;
            this.from.DefaultCellStyle = dataGridViewCellStyle2;
            this.from.FillWeight = 168.5186F;
            this.from.HeaderText = "From";
            this.from.Name = "from";
            this.from.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.from.Width = 150;
            // 
            // to
            // 
            this.to.FillWeight = 685.2791F;
            this.to.HeaderText = "To";
            this.to.Name = "to";
            this.to.Width = 150;
            // 
            // dur
            // 
            this.dur.HeaderText = "Duration";
            this.dur.Name = "dur";
            this.dur.ReadOnly = true;
            // 
            // CardRate
            // 
            this.CardRate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.CardRate.FillWeight = 6.600313F;
            this.CardRate.HeaderText = "Card Rate";
            this.CardRate.Name = "CardRate";
            this.CardRate.ReadOnly = true;
            // 
            // DiscountRate
            // 
            this.DiscountRate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DiscountRate.FillWeight = 6.600313F;
            this.DiscountRate.HeaderText = "Discount Rate";
            this.DiscountRate.Name = "DiscountRate";
            // 
            // Discountper
            // 
            this.Discountper.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Discountper.FillWeight = 6.600313F;
            this.Discountper.HeaderText = "Discount %";
            this.Discountper.Name = "Discountper";
            // 
            // VIPrate
            // 
            this.VIPrate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.VIPrate.FillWeight = 6.600313F;
            this.VIPrate.HeaderText = "VIP Rate";
            this.VIPrate.Name = "VIPrate";
            this.VIPrate.ReadOnly = true;
            // 
            // Vipdiscountrate
            // 
            this.Vipdiscountrate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Vipdiscountrate.FillWeight = 6.600313F;
            this.Vipdiscountrate.HeaderText = "VIP Discount Rate";
            this.Vipdiscountrate.Name = "Vipdiscountrate";
            // 
            // VIPDiscountPercentage
            // 
            this.VIPDiscountPercentage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.VIPDiscountPercentage.FillWeight = 6.600313F;
            this.VIPDiscountPercentage.HeaderText = "VIP Discount %";
            this.VIPDiscountPercentage.Name = "VIPDiscountPercentage";
            // 
            // dayid
            // 
            this.dayid.HeaderText = "dayid";
            this.dayid.Name = "dayid";
            this.dayid.Visible = false;
            // 
            // cmbgame
            // 
            this.cmbgame.FormattingEnabled = true;
            this.cmbgame.ItemHeight = 23;
            this.cmbgame.Location = new System.Drawing.Point(95, 104);
            this.cmbgame.Name = "cmbgame";
            this.cmbgame.Size = new System.Drawing.Size(201, 29);
            this.cmbgame.TabIndex = 224;
            this.cmbgame.Tag = "Payment Mode";
            this.cmbgame.UseSelectable = true;
            this.cmbgame.SelectionChangeCommitted += new System.EventHandler(this.cmbgame_SelectionChangeCommitted);
            // 
            // FrmHappyHours
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1084, 572);
            this.Controls.Add(this.cmbgame);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txttitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.pnlhead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmHappyHours";
            this.Text = "FrmHappyHours";
            this.Load += new System.EventHandler(this.FrmHappyHours_Load);
            this.pnlhead.ResumeLayout(false);
            this.pnlhead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlhead;
        private System.Windows.Forms.Label lblsubhead;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblhead;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txttitle;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewTextBoxColumn day;
        private System.Windows.Forms.DataGridViewTextBoxColumn from;
        private System.Windows.Forms.DataGridViewTextBoxColumn to;
        private System.Windows.Forms.DataGridViewTextBoxColumn dur;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discountper;
        private System.Windows.Forms.DataGridViewTextBoxColumn VIPrate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vipdiscountrate;
        private System.Windows.Forms.DataGridViewTextBoxColumn VIPDiscountPercentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn dayid;
        private MetroFramework.Controls.MetroComboBox cmbgame;
    }
}