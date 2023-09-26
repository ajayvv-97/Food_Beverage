namespace Game.Forms
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmOffer));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlhead = new System.Windows.Forms.Panel();
            this.lblsubhead = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblhead = new System.Windows.Forms.Label();
            this.dgvoffer = new System.Windows.Forms.DataGridView();
            this.txttitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpfrom = new System.Windows.Forms.DateTimePicker();
            this.dtpto = new System.Windows.Forms.DateTimePicker();
            this.button2 = new System.Windows.Forms.Button();
            this.cmbgame = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.CardRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiscountRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Discountper = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VIPrate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vipdiscountrate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VIPDiscountPercentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gmid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlhead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvoffer)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlhead
            // 
            this.pnlhead.BackColor = System.Drawing.Color.Transparent;
            this.pnlhead.Controls.Add(this.lblsubhead);
            this.pnlhead.Controls.Add(this.panel3);
            this.pnlhead.Controls.Add(this.lblhead);
            this.pnlhead.Location = new System.Drawing.Point(2, 1);
            this.pnlhead.Name = "pnlhead";
            this.pnlhead.Size = new System.Drawing.Size(1083, 81);
            this.pnlhead.TabIndex = 2;
            // 
            // lblsubhead
            // 
            this.lblsubhead.AutoSize = true;
            this.lblsubhead.Font = new System.Drawing.Font("OpenSymbol", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsubhead.ForeColor = System.Drawing.Color.White;
            this.lblsubhead.Location = new System.Drawing.Point(29, 42);
            this.lblsubhead.Name = "lblsubhead";
            this.lblsubhead.Size = new System.Drawing.Size(286, 19);
            this.lblsubhead.TabIndex = 204;
            this.lblsubhead.Text = "OFFER BASED ON SPECIFIC DATE";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkGray;
            this.panel3.Location = new System.Drawing.Point(-3, 74);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1086, 5);
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
            this.lblhead.Size = new System.Drawing.Size(94, 32);
            this.lblhead.TabIndex = 202;
            this.lblhead.Text = "OFFER";
            // 
            // dgvoffer
            // 
            this.dgvoffer.AllowUserToDeleteRows = false;
            this.dgvoffer.AllowUserToOrderColumns = true;
            this.dgvoffer.AllowUserToResizeColumns = false;
            this.dgvoffer.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvoffer.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvoffer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvoffer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cmbgame,
            this.CardRate,
            this.DiscountRate,
            this.Discountper,
            this.VIPrate,
            this.Vipdiscountrate,
            this.VIPDiscountPercentage,
            this.gmid});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvoffer.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvoffer.GridColor = System.Drawing.Color.Gray;
            this.dgvoffer.Location = new System.Drawing.Point(5, 159);
            this.dgvoffer.Name = "dgvoffer";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Century Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvoffer.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvoffer.RowHeadersVisible = false;
            this.dgvoffer.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvoffer.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvoffer.RowTemplate.Height = 35;
            this.dgvoffer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvoffer.Size = new System.Drawing.Size(1071, 302);
            this.dgvoffer.TabIndex = 159;
            this.dgvoffer.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvoffer_EditingControlShowing);
            this.dgvoffer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvoffer_KeyUp);
            // 
            // txttitle
            // 
            this.txttitle.Location = new System.Drawing.Point(161, 110);
            this.txttitle.Multiline = true;
            this.txttitle.Name = "txttitle";
            this.txttitle.Size = new System.Drawing.Size(206, 27);
            this.txttitle.TabIndex = 164;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(41, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 24);
            this.label1.TabIndex = 163;
            this.label1.Text = "Description :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(388, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 24);
            this.label2.TabIndex = 165;
            this.label2.Text = "From :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(683, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 24);
            this.label3.TabIndex = 166;
            this.label3.Text = "To :";
            // 
            // dtpfrom
            // 
            this.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfrom.Location = new System.Drawing.Point(459, 112);
            this.dtpfrom.Name = "dtpfrom";
            this.dtpfrom.Size = new System.Drawing.Size(167, 20);
            this.dtpfrom.TabIndex = 167;
            // 
            // dtpto
            // 
            this.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpto.Location = new System.Drawing.Point(742, 112);
            this.dtpto.Name = "dtpto";
            this.dtpto.Size = new System.Drawing.Size(167, 20);
            this.dtpto.TabIndex = 168;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button2.BackgroundImage")));
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(837, 478);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(173, 55);
            this.button2.TabIndex = 224;
            this.button2.Text = "SAVE";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmbgame
            // 
            this.cmbgame.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmbgame.FillWeight = 6.600313F;
            this.cmbgame.HeaderText = "Game";
            this.cmbgame.Name = "cmbgame";
            this.cmbgame.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cmbgame.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // CardRate
            // 
            this.CardRate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CardRate.DefaultCellStyle = dataGridViewCellStyle2;
            this.CardRate.FillWeight = 6.600313F;
            this.CardRate.HeaderText = "Card Rate";
            this.CardRate.Name = "CardRate";
            // 
            // DiscountRate
            // 
            this.DiscountRate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.DiscountRate.DefaultCellStyle = dataGridViewCellStyle3;
            this.DiscountRate.FillWeight = 6.600313F;
            this.DiscountRate.HeaderText = "Discount Rate";
            this.DiscountRate.Name = "DiscountRate";
            // 
            // Discountper
            // 
            this.Discountper.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Discountper.DefaultCellStyle = dataGridViewCellStyle4;
            this.Discountper.FillWeight = 6.600313F;
            this.Discountper.HeaderText = "Discount %";
            this.Discountper.Name = "Discountper";
            // 
            // VIPrate
            // 
            this.VIPrate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.VIPrate.DefaultCellStyle = dataGridViewCellStyle5;
            this.VIPrate.FillWeight = 6.600313F;
            this.VIPrate.HeaderText = "VIP Rate";
            this.VIPrate.Name = "VIPrate";
            // 
            // Vipdiscountrate
            // 
            this.Vipdiscountrate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Vipdiscountrate.DefaultCellStyle = dataGridViewCellStyle6;
            this.Vipdiscountrate.FillWeight = 6.600313F;
            this.Vipdiscountrate.HeaderText = "VIP Discount Rate";
            this.Vipdiscountrate.Name = "Vipdiscountrate";
            // 
            // VIPDiscountPercentage
            // 
            this.VIPDiscountPercentage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.VIPDiscountPercentage.DefaultCellStyle = dataGridViewCellStyle7;
            this.VIPDiscountPercentage.FillWeight = 6.600313F;
            this.VIPDiscountPercentage.HeaderText = "VIP Discount %";
            this.VIPDiscountPercentage.Name = "VIPDiscountPercentage";
            // 
            // gmid
            // 
            this.gmid.HeaderText = "gmid";
            this.gmid.Name = "gmid";
            this.gmid.Visible = false;
            // 
            // FrmOffer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1081, 564);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dtpto);
            this.Controls.Add(this.dtpfrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txttitle);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvoffer);
            this.Controls.Add(this.pnlhead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmOffer";
            this.Text = "FrmOffer";
            this.Load += new System.EventHandler(this.FrmOffer_Load);
            this.pnlhead.ResumeLayout(false);
            this.pnlhead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvoffer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlhead;
        private System.Windows.Forms.Label lblsubhead;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblhead;
        private System.Windows.Forms.DataGridView dgvoffer;
        private System.Windows.Forms.TextBox txttitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpfrom;
        private System.Windows.Forms.DateTimePicker dtpto;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridViewComboBoxColumn cmbgame;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiscountRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Discountper;
        private System.Windows.Forms.DataGridViewTextBoxColumn VIPrate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vipdiscountrate;
        private System.Windows.Forms.DataGridViewTextBoxColumn VIPDiscountPercentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn gmid;
    }
}