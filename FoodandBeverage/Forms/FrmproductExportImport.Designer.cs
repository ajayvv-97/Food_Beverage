
namespace FoodandBeverage.Forms
{
    partial class FrmproductExportImport
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.comproduct = new MetroFramework.Controls.MetroComboBox();
            this.btn_DataSync = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.tblExpImp = new System.Windows.Forms.TableLayoutPanel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btn_cls_gmsbclose = new FontAwesome.Sharp.IconButton();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.lblhead = new System.Windows.Forms.Label();
            this.pnlhead = new System.Windows.Forms.Panel();
            this.dgvItem = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.SSlno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SItem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SBarcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SPRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SSRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SMRP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SLocksrate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SBaseunit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SConversionFactor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STaxType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STaxPerc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SHSNCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SReorderLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SIsExpiry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tblExpImp.SuspendLayout();
            this.pnlhead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).BeginInit();
            this.panel1.SuspendLayout();
            this.tlp_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // comproduct
            // 
            this.comproduct.BackColor = System.Drawing.Color.White;
            this.comproduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comproduct.DropDownHeight = 200;
            this.comproduct.ForeColor = System.Drawing.Color.Black;
            this.comproduct.FormattingEnabled = true;
            this.comproduct.IntegralHeight = false;
            this.comproduct.ItemHeight = 23;
            this.comproduct.Location = new System.Drawing.Point(3, 3);
            this.comproduct.Name = "comproduct";
            this.comproduct.Size = new System.Drawing.Size(310, 29);
            this.comproduct.TabIndex = 177;
            this.comproduct.Tag = "Designation";
            this.comproduct.UseCustomBackColor = true;
            this.comproduct.UseCustomForeColor = true;
            this.comproduct.UseSelectable = true;
            // 
            // btn_DataSync
            // 
            this.btn_DataSync.BackColor = System.Drawing.Color.Transparent;
            this.btn_DataSync.BackgroundImage = global::FoodandBeverage.Properties.Resources.fb_sidemenu;
            this.btn_DataSync.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_DataSync.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_DataSync.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DataSync.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DataSync.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_DataSync.Location = new System.Drawing.Point(561, 3);
            this.btn_DataSync.Name = "btn_DataSync";
            this.btn_DataSync.Size = new System.Drawing.Size(115, 43);
            this.btn_DataSync.TabIndex = 181;
            this.btn_DataSync.Text = "Data Sync";
            this.btn_DataSync.UseVisualStyleBackColor = false;
            this.btn_DataSync.Click += new System.EventHandler(this.btn_DataSync_Click);
            // 
            // btnImport
            // 
            this.btnImport.BackColor = System.Drawing.Color.Transparent;
            this.btnImport.BackgroundImage = global::FoodandBeverage.Properties.Resources.cancel_32;
            this.btnImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnImport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnImport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImport.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnImport.Location = new System.Drawing.Point(440, 3);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(115, 43);
            this.btnImport.TabIndex = 179;
            this.btnImport.Text = "import";
            this.btnImport.UseVisualStyleBackColor = false;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // tblExpImp
            // 
            this.tblExpImp.BackColor = System.Drawing.Color.Transparent;
            this.tblExpImp.ColumnCount = 6;
            this.tblExpImp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblExpImp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblExpImp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblExpImp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblExpImp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblExpImp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblExpImp.Controls.Add(this.comproduct, 0, 0);
            this.tblExpImp.Controls.Add(this.btn_DataSync, 3, 0);
            this.tblExpImp.Controls.Add(this.btnExport, 1, 0);
            this.tblExpImp.Controls.Add(this.btnImport, 2, 0);
            this.tblExpImp.Controls.Add(this.btn_cls_gmsbclose, 5, 0);
            this.tblExpImp.Controls.Add(this.btn_refresh, 4, 0);
            this.tblExpImp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblExpImp.Location = new System.Drawing.Point(8, 59);
            this.tblExpImp.Name = "tblExpImp";
            this.tblExpImp.RowCount = 1;
            this.tblExpImp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblExpImp.Size = new System.Drawing.Size(963, 49);
            this.tblExpImp.TabIndex = 182;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.Transparent;
            this.btnExport.BackgroundImage = global::FoodandBeverage.Properties.Resources.fb_operationbtn;
            this.btnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Arial Narrow", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExport.Location = new System.Drawing.Point(319, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(115, 43);
            this.btnExport.TabIndex = 178;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btn_cls_gmsbclose
            // 
            this.btn_cls_gmsbclose.BackColor = System.Drawing.Color.Transparent;
            this.btn_cls_gmsbclose.BackgroundImage = global::FoodandBeverage.Properties.Resources.cancel_2;
            this.btn_cls_gmsbclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_cls_gmsbclose.FlatAppearance.BorderSize = 0;
            this.btn_cls_gmsbclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cls_gmsbclose.Flip = FontAwesome.Sharp.FlipOrientation.Horizontal;
            this.btn_cls_gmsbclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cls_gmsbclose.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_cls_gmsbclose.IconChar = FontAwesome.Sharp.IconChar.None;
            this.btn_cls_gmsbclose.IconColor = System.Drawing.Color.Snow;
            this.btn_cls_gmsbclose.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_cls_gmsbclose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_cls_gmsbclose.Location = new System.Drawing.Point(803, 3);
            this.btn_cls_gmsbclose.Name = "btn_cls_gmsbclose";
            this.btn_cls_gmsbclose.Size = new System.Drawing.Size(105, 43);
            this.btn_cls_gmsbclose.TabIndex = 180;
            this.btn_cls_gmsbclose.Text = "Close";
            this.btn_cls_gmsbclose.UseVisualStyleBackColor = false;
            this.btn_cls_gmsbclose.Click += new System.EventHandler(this.btn_cls_gmsbclose_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackgroundImage = global::FoodandBeverage.Properties.Resources.Untitled_2_2030;
            this.btn_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_refresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_refresh.FlatAppearance.BorderSize = 0;
            this.btn_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_refresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_refresh.ForeColor = System.Drawing.SystemColors.Window;
            this.btn_refresh.Location = new System.Drawing.Point(682, 3);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(115, 43);
            this.btn_refresh.TabIndex = 182;
            this.btn_refresh.Text = "Refresh";
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // lblhead
            // 
            this.lblhead.AutoSize = true;
            this.lblhead.BackColor = System.Drawing.Color.Transparent;
            this.lblhead.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblhead.ForeColor = System.Drawing.Color.White;
            this.lblhead.Location = new System.Drawing.Point(3, 7);
            this.lblhead.Name = "lblhead";
            this.lblhead.Size = new System.Drawing.Size(213, 24);
            this.lblhead.TabIndex = 202;
            this.lblhead.Text = "Product Export/Import";
            // 
            // pnlhead
            // 
            this.pnlhead.BackColor = System.Drawing.Color.Purple;
            this.pnlhead.Controls.Add(this.lblhead);
            this.pnlhead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlhead.Location = new System.Drawing.Point(8, 8);
            this.pnlhead.Name = "pnlhead";
            this.pnlhead.Size = new System.Drawing.Size(963, 45);
            this.pnlhead.TabIndex = 7;
            // 
            // dgvItem
            // 
            this.dgvItem.AllowUserToAddRows = false;
            this.dgvItem.AllowUserToDeleteRows = false;
            this.dgvItem.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvItem.ColumnHeadersHeight = 46;
            this.dgvItem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SSlno,
            this.SItem,
            this.SBarcode,
            this.SCategory,
            this.SType,
            this.SPRate,
            this.SSRate,
            this.SMRP,
            this.SLocksrate,
            this.SUnit,
            this.SBaseunit,
            this.SConversionFactor,
            this.STaxType,
            this.STaxPerc,
            this.STax,
            this.SHSNCode,
            this.SReorderLevel,
            this.SIsExpiry});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvItem.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItem.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvItem.Location = new System.Drawing.Point(0, 0);
            this.dgvItem.Name = "dgvItem";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvItem.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvItem.RowHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvItem.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvItem.RowTemplate.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvItem.RowTemplate.Height = 30;
            this.dgvItem.Size = new System.Drawing.Size(963, 470);
            this.dgvItem.TabIndex = 184;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvItem);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(8, 114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(963, 470);
            this.panel1.TabIndex = 185;
            // 
            // tlp_main
            // 
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.pnlhead, 0, 0);
            this.tlp_main.Controls.Add(this.tblExpImp, 0, 1);
            this.tlp_main.Controls.Add(this.panel1, 0, 2);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.Padding = new System.Windows.Forms.Padding(5);
            this.tlp_main.RowCount = 4;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_main.Size = new System.Drawing.Size(979, 612);
            this.tlp_main.TabIndex = 205;
            // 
            // SSlno
            // 
            this.SSlno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SSlno.HeaderText = "Slno";
            this.SSlno.Name = "SSlno";
            this.SSlno.ReadOnly = true;
            this.SSlno.Width = 66;
            // 
            // SItem
            // 
            this.SItem.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.SItem.DefaultCellStyle = dataGridViewCellStyle2;
            this.SItem.HeaderText = "Item";
            this.SItem.Name = "SItem";
            this.SItem.ReadOnly = true;
            this.SItem.Width = 66;
            // 
            // SBarcode
            // 
            this.SBarcode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SBarcode.HeaderText = "Barcode";
            this.SBarcode.Name = "SBarcode";
            this.SBarcode.ReadOnly = true;
            this.SBarcode.Width = 94;
            // 
            // SCategory
            // 
            this.SCategory.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SCategory.HeaderText = "Category";
            this.SCategory.Name = "SCategory";
            this.SCategory.ReadOnly = true;
            this.SCategory.Width = 98;
            // 
            // SType
            // 
            this.SType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SType.HeaderText = "Type";
            this.SType.Name = "SType";
            this.SType.ReadOnly = true;
            this.SType.Width = 68;
            // 
            // SPRate
            // 
            this.SPRate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SPRate.HeaderText = "PRate";
            this.SPRate.Name = "SPRate";
            this.SPRate.ReadOnly = true;
            this.SPRate.Width = 79;
            // 
            // SSRate
            // 
            this.SSRate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SSRate.HeaderText = "SRate";
            this.SSRate.Name = "SSRate";
            this.SSRate.ReadOnly = true;
            this.SSRate.Width = 80;
            // 
            // SMRP
            // 
            this.SMRP.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SMRP.HeaderText = "MRP";
            this.SMRP.Name = "SMRP";
            this.SMRP.ReadOnly = true;
            this.SMRP.Width = 69;
            // 
            // SLocksrate
            // 
            this.SLocksrate.HeaderText = "LockSaleRate";
            this.SLocksrate.Name = "SLocksrate";
            this.SLocksrate.Width = 135;
            // 
            // SUnit
            // 
            this.SUnit.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SUnit.HeaderText = "Unit";
            this.SUnit.Name = "SUnit";
            this.SUnit.ReadOnly = true;
            this.SUnit.Width = 63;
            // 
            // SBaseunit
            // 
            this.SBaseunit.HeaderText = "Baseunit";
            this.SBaseunit.Name = "SBaseunit";
            this.SBaseunit.Width = 97;
            // 
            // SConversionFactor
            // 
            this.SConversionFactor.HeaderText = "ConversionFactor";
            this.SConversionFactor.Name = "SConversionFactor";
            this.SConversionFactor.Width = 159;
            // 
            // STaxType
            // 
            this.STaxType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = "0";
            this.STaxType.DefaultCellStyle = dataGridViewCellStyle3;
            this.STaxType.HeaderText = "TaxType";
            this.STaxType.Name = "STaxType";
            this.STaxType.ReadOnly = true;
            this.STaxType.Width = 93;
            // 
            // STaxPerc
            // 
            this.STaxPerc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.STaxPerc.HeaderText = "TaxPerc";
            this.STaxPerc.Name = "STaxPerc";
            this.STaxPerc.ReadOnly = true;
            this.STaxPerc.Width = 91;
            // 
            // STax
            // 
            this.STax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.STax.HeaderText = "Tax";
            this.STax.Name = "STax";
            this.STax.ReadOnly = true;
            this.STax.Width = 59;
            // 
            // SHSNCode
            // 
            this.SHSNCode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SHSNCode.HeaderText = "HSNCode";
            this.SHSNCode.Name = "SHSNCode";
            this.SHSNCode.ReadOnly = true;
            this.SHSNCode.Width = 106;
            // 
            // SReorderLevel
            // 
            this.SReorderLevel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SReorderLevel.HeaderText = "ReorderLevel";
            this.SReorderLevel.Name = "SReorderLevel";
            this.SReorderLevel.Width = 129;
            // 
            // SIsExpiry
            // 
            this.SIsExpiry.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.SIsExpiry.HeaderText = "IsExpiry";
            this.SIsExpiry.Name = "SIsExpiry";
            this.SIsExpiry.Width = 89;
            // 
            // FrmproductExportImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 612);
            this.Controls.Add(this.tlp_main);
            this.Name = "FrmproductExportImport";
            this.Text = "FrmproductExportImport";
            this.Load += new System.EventHandler(this.FrmproductExportImport_Load);
            this.tblExpImp.ResumeLayout(false);
            this.pnlhead.ResumeLayout(false);
            this.pnlhead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItem)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tlp_main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroComboBox comproduct;
        private System.Windows.Forms.Button btn_DataSync;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private FontAwesome.Sharp.IconButton btn_cls_gmsbclose;
        private System.Windows.Forms.TableLayoutPanel tblExpImp;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Label lblhead;
        private System.Windows.Forms.Panel pnlhead;
        private System.Windows.Forms.DataGridView dgvItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.DataGridViewTextBoxColumn SSlno;
        private System.Windows.Forms.DataGridViewTextBoxColumn SItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn SBarcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn SCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn SType;
        private System.Windows.Forms.DataGridViewTextBoxColumn SPRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SSRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SMRP;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLocksrate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn SBaseunit;
        private System.Windows.Forms.DataGridViewTextBoxColumn SConversionFactor;
        private System.Windows.Forms.DataGridViewTextBoxColumn STaxType;
        private System.Windows.Forms.DataGridViewTextBoxColumn STaxPerc;
        private System.Windows.Forms.DataGridViewTextBoxColumn STax;
        private System.Windows.Forms.DataGridViewTextBoxColumn SHSNCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn SReorderLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn SIsExpiry;
    }
}