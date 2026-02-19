namespace FoodandBeverage.Forms
{
    partial class FrmPartyPackSetup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btncancel = new System.Windows.Forms.Button();
            this.tlp_drawer = new System.Windows.Forms.TableLayoutPanel();
            this.dgv_packs = new System.Windows.Forms.DataGridView();
            this.slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.action = new System.Windows.Forms.DataGridViewImageColumn();
            this.pPartyPackDetailsId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlp_registration = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtpackname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtprice = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDisPer = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtDiscAmnt = new System.Windows.Forms.TextBox();
            this.lbltax = new System.Windows.Forms.Label();
            this.lbltaxtype = new System.Windows.Forms.Label();
            this.cmbtax = new System.Windows.Forms.ComboBox();
            this.cmbTaxType = new System.Windows.Forms.ComboBox();
            this.tlp_setting = new System.Windows.Forms.TableLayoutPanel();
            this.btnclose_drawer = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tlp_d_save_canl = new System.Windows.Forms.TableLayoutPanel();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.pnl_ = new System.Windows.Forms.Panel();
            this.btn_del_pack = new System.Windows.Forms.Button();
            this.lblvat = new System.Windows.Forms.Label();
            this.lbligst = new System.Windows.Forms.Label();
            this.cmbVatPrcnt = new System.Windows.Forms.ComboBox();
            this.cmbIgstPrcnt = new System.Windows.Forms.ComboBox();
            this.tlp_s_c_gst_lbl = new System.Windows.Forms.TableLayoutPanel();
            this.lblsgst = new System.Windows.Forms.Label();
            this.lblcgst = new System.Windows.Forms.Label();
            this.tlp_S_C_text = new System.Windows.Forms.TableLayoutPanel();
            this.txtCgstPrcnt = new System.Windows.Forms.TextBox();
            this.txtSgstPrcnt = new System.Windows.Forms.TextBox();
            this.chb_active = new System.Windows.Forms.CheckBox();
            this.tlp_drawer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_packs)).BeginInit();
            this.tlp_registration.SuspendLayout();
            this.tlp_setting.SuspendLayout();
            this.tlp_d_save_canl.SuspendLayout();
            this.pnl_.SuspendLayout();
            this.tlp_s_c_gst_lbl.SuspendLayout();
            this.tlp_S_C_text.SuspendLayout();
            this.SuspendLayout();
            // 
            // btncancel
            // 
            this.btncancel.BackColor = System.Drawing.Color.Transparent;
            this.btncancel.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_CANCEL;
            this.btncancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btncancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btncancel.FlatAppearance.BorderSize = 0;
            this.btncancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncancel.Location = new System.Drawing.Point(964, 4);
            this.btncancel.Margin = new System.Windows.Forms.Padding(4);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(124, 41);
            this.btncancel.TabIndex = 23;
            this.btncancel.UseVisualStyleBackColor = false;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // tlp_drawer
            // 
            this.tlp_drawer.BackColor = System.Drawing.Color.White;
            this.tlp_drawer.ColumnCount = 1;
            this.tlp_drawer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_drawer.Controls.Add(this.dgv_packs, 0, 2);
            this.tlp_drawer.Controls.Add(this.tlp_registration, 0, 1);
            this.tlp_drawer.Controls.Add(this.tlp_setting, 0, 0);
            this.tlp_drawer.Controls.Add(this.tlp_d_save_canl, 0, 3);
            this.tlp_drawer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_drawer.Location = new System.Drawing.Point(8, 8);
            this.tlp_drawer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlp_drawer.Name = "tlp_drawer";
            this.tlp_drawer.RowCount = 4;
            this.tlp_drawer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_drawer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_drawer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_drawer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_drawer.Size = new System.Drawing.Size(1098, 654);
            this.tlp_drawer.TabIndex = 24;
            // 
            // dgv_packs
            // 
            this.dgv_packs.AllowUserToAddRows = false;
            this.dgv_packs.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_packs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_packs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_packs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.slno,
            this.item,
            this.Qty,
            this.action,
            this.pPartyPackDetailsId});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_packs.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_packs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_packs.Location = new System.Drawing.Point(4, 291);
            this.dgv_packs.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_packs.Name = "dgv_packs";
            this.dgv_packs.RowHeadersVisible = false;
            this.dgv_packs.RowHeadersWidth = 51;
            this.dgv_packs.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_packs.RowTemplate.Height = 44;
            this.dgv_packs.Size = new System.Drawing.Size(1090, 306);
            this.dgv_packs.TabIndex = 28;
            this.dgv_packs.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_packs_CellContentClick);
            this.dgv_packs.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_packs_CellFormatting);
            this.dgv_packs.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_packs_CellValueChanged);
            // 
            // slno
            // 
            this.slno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.slno.HeaderText = "Slno";
            this.slno.MinimumWidth = 6;
            this.slno.Name = "slno";
            this.slno.Width = 81;
            // 
            // item
            // 
            this.item.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.item.HeaderText = "Product";
            this.item.MinimumWidth = 6;
            this.item.Name = "item";
            this.item.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Qty
            // 
            this.Qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Qty.HeaderText = "Quantity";
            this.Qty.MinimumWidth = 6;
            this.Qty.Name = "Qty";
            this.Qty.Width = 114;
            // 
            // action
            // 
            this.action.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.action.HeaderText = "Action";
            this.action.Image = global::FoodandBeverage.Properties.Resources.Round_Close;
            this.action.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.action.MinimumWidth = 2;
            this.action.Name = "action";
            this.action.ToolTipText = "delete row";
            this.action.Width = 73;
            // 
            // pPartyPackDetailsId
            // 
            this.pPartyPackDetailsId.HeaderText = "PartyPackDetailsId";
            this.pPartyPackDetailsId.MinimumWidth = 6;
            this.pPartyPackDetailsId.Name = "pPartyPackDetailsId";
            this.pPartyPackDetailsId.Visible = false;
            this.pPartyPackDetailsId.Width = 125;
            // 
            // tlp_registration
            // 
            this.tlp_registration.BackColor = System.Drawing.Color.White;
            this.tlp_registration.ColumnCount = 4;
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_registration.Controls.Add(this.label2, 0, 1);
            this.tlp_registration.Controls.Add(this.txtpackname, 0, 2);
            this.tlp_registration.Controls.Add(this.label3, 1, 1);
            this.tlp_registration.Controls.Add(this.txtprice, 1, 2);
            this.tlp_registration.Controls.Add(this.label10, 2, 1);
            this.tlp_registration.Controls.Add(this.txtDisPer, 2, 2);
            this.tlp_registration.Controls.Add(this.label11, 3, 1);
            this.tlp_registration.Controls.Add(this.txtDiscAmnt, 3, 2);
            this.tlp_registration.Controls.Add(this.lbltax, 0, 3);
            this.tlp_registration.Controls.Add(this.lbltaxtype, 1, 3);
            this.tlp_registration.Controls.Add(this.cmbtax, 0, 4);
            this.tlp_registration.Controls.Add(this.cmbTaxType, 1, 4);
            this.tlp_registration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_registration.Location = new System.Drawing.Point(4, 53);
            this.tlp_registration.Margin = new System.Windows.Forms.Padding(4);
            this.tlp_registration.Name = "tlp_registration";
            this.tlp_registration.Padding = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.tlp_registration.RowCount = 6;
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_registration.Size = new System.Drawing.Size(1090, 230);
            this.tlp_registration.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(261, 41);
            this.label2.TabIndex = 5;
            this.label2.Text = "Party Pack Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtpackname
            // 
            this.txtpackname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.txtpackname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtpackname.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtpackname.Location = new System.Drawing.Point(11, 70);
            this.txtpackname.Margin = new System.Windows.Forms.Padding(4);
            this.txtpackname.Multiline = true;
            this.txtpackname.Name = "txtpackname";
            this.txtpackname.Size = new System.Drawing.Size(261, 33);
            this.txtpackname.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(280, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(261, 41);
            this.label3.TabIndex = 13;
            this.label3.Text = "Price";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtprice
            // 
            this.txtprice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.txtprice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtprice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtprice.Location = new System.Drawing.Point(280, 70);
            this.txtprice.Margin = new System.Windows.Forms.Padding(4);
            this.txtprice.MaxLength = 7;
            this.txtprice.Multiline = true;
            this.txtprice.Name = "txtprice";
            this.txtprice.Size = new System.Drawing.Size(261, 33);
            this.txtprice.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(549, 25);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(261, 41);
            this.label10.TabIndex = 20;
            this.label10.Text = "Discout %";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDisPer
            // 
            this.txtDisPer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.txtDisPer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDisPer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtDisPer.Location = new System.Drawing.Point(549, 70);
            this.txtDisPer.Margin = new System.Windows.Forms.Padding(4);
            this.txtDisPer.MaxLength = 5;
            this.txtDisPer.Multiline = true;
            this.txtDisPer.Name = "txtDisPer";
            this.txtDisPer.Size = new System.Drawing.Size(261, 33);
            this.txtDisPer.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(818, 25);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(261, 41);
            this.label11.TabIndex = 18;
            this.label11.Text = "Discount Amt";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDiscAmnt
            // 
            this.txtDiscAmnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.txtDiscAmnt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDiscAmnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtDiscAmnt.Location = new System.Drawing.Point(818, 70);
            this.txtDiscAmnt.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiscAmnt.MaxLength = 7;
            this.txtDiscAmnt.Multiline = true;
            this.txtDiscAmnt.Name = "txtDiscAmnt";
            this.txtDiscAmnt.Size = new System.Drawing.Size(261, 33);
            this.txtDiscAmnt.TabIndex = 1;
            // 
            // lbltax
            // 
            this.lbltax.AutoSize = true;
            this.lbltax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltax.Location = new System.Drawing.Point(11, 107);
            this.lbltax.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbltax.Name = "lbltax";
            this.lbltax.Size = new System.Drawing.Size(261, 41);
            this.lbltax.TabIndex = 15;
            this.lbltax.Text = "Tax";
            this.lbltax.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbltaxtype
            // 
            this.lbltaxtype.AutoSize = true;
            this.lbltaxtype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltaxtype.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltaxtype.Location = new System.Drawing.Point(280, 107);
            this.lbltaxtype.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbltaxtype.Name = "lbltaxtype";
            this.lbltaxtype.Size = new System.Drawing.Size(261, 41);
            this.lbltaxtype.TabIndex = 14;
            this.lbltaxtype.Text = "Tax Type";
            this.lbltaxtype.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbtax
            // 
            this.cmbtax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.cmbtax.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbtax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbtax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbtax.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.cmbtax.FormattingEnabled = true;
            this.cmbtax.Items.AddRange(new object[] {
            "Inclusive",
            "Exclusive"});
            this.cmbtax.Location = new System.Drawing.Point(11, 152);
            this.cmbtax.Margin = new System.Windows.Forms.Padding(4);
            this.cmbtax.Name = "cmbtax";
            this.cmbtax.Size = new System.Drawing.Size(261, 32);
            this.cmbtax.TabIndex = 12;
            // 
            // cmbTaxType
            // 
            this.cmbTaxType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.cmbTaxType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTaxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaxType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTaxType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.cmbTaxType.FormattingEnabled = true;
            this.cmbTaxType.Items.AddRange(new object[] {
            "GST",
            "VAT"});
            this.cmbTaxType.Location = new System.Drawing.Point(280, 152);
            this.cmbTaxType.Margin = new System.Windows.Forms.Padding(4);
            this.cmbTaxType.Name = "cmbTaxType";
            this.cmbTaxType.Size = new System.Drawing.Size(261, 32);
            this.cmbTaxType.TabIndex = 11;
            this.cmbTaxType.SelectedValueChanged += new System.EventHandler(this.cmbTaxType_SelectedValueChanged);
            // 
            // tlp_setting
            // 
            this.tlp_setting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(80)))), ((int)(((byte)(185)))));
            this.tlp_setting.ColumnCount = 3;
            this.tlp_setting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 95.57603F));
            this.tlp_setting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 4.423963F));
            this.tlp_setting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tlp_setting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_setting.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_setting.Controls.Add(this.btnclose_drawer, 1, 0);
            this.tlp_setting.Controls.Add(this.label1, 0, 0);
            this.tlp_setting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_setting.Location = new System.Drawing.Point(4, 4);
            this.tlp_setting.Margin = new System.Windows.Forms.Padding(4);
            this.tlp_setting.Name = "tlp_setting";
            this.tlp_setting.RowCount = 1;
            this.tlp_setting.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_setting.Size = new System.Drawing.Size(1090, 41);
            this.tlp_setting.TabIndex = 26;
            // 
            // btnclose_drawer
            // 
            this.btnclose_drawer.BackColor = System.Drawing.Color.White;
            this.btnclose_drawer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclose_drawer.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnclose_drawer.FlatAppearance.BorderSize = 0;
            this.btnclose_drawer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose_drawer.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose_drawer.ForeColor = System.Drawing.Color.Red;
            this.btnclose_drawer.Location = new System.Drawing.Point(1024, 4);
            this.btnclose_drawer.Margin = new System.Windows.Forms.Padding(4);
            this.btnclose_drawer.Name = "btnclose_drawer";
            this.btnclose_drawer.Size = new System.Drawing.Size(39, 34);
            this.btnclose_drawer.TabIndex = 21;
            this.btnclose_drawer.Text = "X";
            this.btnclose_drawer.UseVisualStyleBackColor = false;
            this.btnclose_drawer.Click += new System.EventHandler(this.btnclose_drawer_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1014, 42);
            this.label1.TabIndex = 22;
            this.label1.Text = "Party Pack Setup";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlp_d_save_canl
            // 
            this.tlp_d_save_canl.ColumnCount = 5;
            this.tlp_d_save_canl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_d_save_canl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_d_save_canl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_d_save_canl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_d_save_canl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_d_save_canl.Controls.Add(this.btnsave, 1, 0);
            this.tlp_d_save_canl.Controls.Add(this.btnSearch, 2, 0);
            this.tlp_d_save_canl.Controls.Add(this.btncancel, 4, 0);
            this.tlp_d_save_canl.Controls.Add(this.btnrefresh, 3, 0);
            this.tlp_d_save_canl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_d_save_canl.Location = new System.Drawing.Point(3, 603);
            this.tlp_d_save_canl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tlp_d_save_canl.Name = "tlp_d_save_canl";
            this.tlp_d_save_canl.RowCount = 1;
            this.tlp_d_save_canl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_d_save_canl.Size = new System.Drawing.Size(1092, 49);
            this.tlp_d_save_canl.TabIndex = 25;
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.Color.Transparent;
            this.btnsave.BackgroundImage = global::FoodandBeverage.Properties.Resources.SaveButton;
            this.btnsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnsave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsave.FlatAppearance.BorderSize = 0;
            this.btnsave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnsave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnsave.Location = new System.Drawing.Point(647, 4);
            this.btnsave.Margin = new System.Windows.Forms.Padding(4);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(97, 41);
            this.btnsave.TabIndex = 26;
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_SEARCH;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(753, 5);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(97, 39);
            this.btnSearch.TabIndex = 24;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnrefresh
            // 
            this.btnrefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_REFESH;
            this.btnrefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnrefresh.FlatAppearance.BorderSize = 0;
            this.btnrefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrefresh.Location = new System.Drawing.Point(859, 4);
            this.btnrefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(97, 41);
            this.btnrefresh.TabIndex = 25;
            this.btnrefresh.UseVisualStyleBackColor = false;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // pnl_
            // 
            this.pnl_.Controls.Add(this.btn_del_pack);
            this.pnl_.Controls.Add(this.lblvat);
            this.pnl_.Controls.Add(this.lbligst);
            this.pnl_.Controls.Add(this.cmbVatPrcnt);
            this.pnl_.Controls.Add(this.cmbIgstPrcnt);
            this.pnl_.Controls.Add(this.tlp_s_c_gst_lbl);
            this.pnl_.Controls.Add(this.tlp_S_C_text);
            this.pnl_.Controls.Add(this.chb_active);
            this.pnl_.Location = new System.Drawing.Point(27, 8);
            this.pnl_.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_.Name = "pnl_";
            this.pnl_.Size = new System.Drawing.Size(69, 42);
            this.pnl_.TabIndex = 177;
            this.pnl_.Visible = false;
            // 
            // btn_del_pack
            // 
            this.btn_del_pack.BackgroundImage = global::FoodandBeverage.Properties.Resources.delete;
            this.btn_del_pack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_del_pack.FlatAppearance.BorderSize = 0;
            this.btn_del_pack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_del_pack.Location = new System.Drawing.Point(7, 10);
            this.btn_del_pack.Margin = new System.Windows.Forms.Padding(80, 4, 4, 4);
            this.btn_del_pack.Name = "btn_del_pack";
            this.btn_del_pack.Size = new System.Drawing.Size(64, 54);
            this.btn_del_pack.TabIndex = 258;
            this.btn_del_pack.UseVisualStyleBackColor = true;
            // 
            // lblvat
            // 
            this.lblvat.AutoSize = true;
            this.lblvat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvat.Location = new System.Drawing.Point(63, 16);
            this.lblvat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblvat.Name = "lblvat";
            this.lblvat.Size = new System.Drawing.Size(44, 20);
            this.lblvat.TabIndex = 15;
            this.lblvat.Text = "VAT";
            this.lblvat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbligst
            // 
            this.lbligst.AutoSize = true;
            this.lbligst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbligst.Location = new System.Drawing.Point(63, 32);
            this.lbligst.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbligst.Name = "lbligst";
            this.lbligst.Size = new System.Drawing.Size(51, 20);
            this.lbligst.TabIndex = 16;
            this.lbligst.Text = "IGST";
            this.lbligst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbVatPrcnt
            // 
            this.cmbVatPrcnt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbVatPrcnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.cmbVatPrcnt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVatPrcnt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbVatPrcnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.cmbVatPrcnt.FormattingEnabled = true;
            this.cmbVatPrcnt.Location = new System.Drawing.Point(268, 356);
            this.cmbVatPrcnt.Margin = new System.Windows.Forms.Padding(4);
            this.cmbVatPrcnt.Name = "cmbVatPrcnt";
            this.cmbVatPrcnt.Size = new System.Drawing.Size(39, 32);
            this.cmbVatPrcnt.TabIndex = 12;
            // 
            // cmbIgstPrcnt
            // 
            this.cmbIgstPrcnt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbIgstPrcnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.cmbIgstPrcnt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbIgstPrcnt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbIgstPrcnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.cmbIgstPrcnt.FormattingEnabled = true;
            this.cmbIgstPrcnt.Location = new System.Drawing.Point(268, 215);
            this.cmbIgstPrcnt.Margin = new System.Windows.Forms.Padding(4);
            this.cmbIgstPrcnt.Name = "cmbIgstPrcnt";
            this.cmbIgstPrcnt.Size = new System.Drawing.Size(39, 32);
            this.cmbIgstPrcnt.TabIndex = 13;
            this.cmbIgstPrcnt.SelectedIndexChanged += new System.EventHandler(this.cmbIgstPrcnt_SelectedIndexChanged);
            // 
            // tlp_s_c_gst_lbl
            // 
            this.tlp_s_c_gst_lbl.ColumnCount = 2;
            this.tlp_s_c_gst_lbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_s_c_gst_lbl.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_s_c_gst_lbl.Controls.Add(this.lblsgst, 0, 0);
            this.tlp_s_c_gst_lbl.Controls.Add(this.lblcgst, 1, 0);
            this.tlp_s_c_gst_lbl.Location = new System.Drawing.Point(264, 402);
            this.tlp_s_c_gst_lbl.Margin = new System.Windows.Forms.Padding(4);
            this.tlp_s_c_gst_lbl.Name = "tlp_s_c_gst_lbl";
            this.tlp_s_c_gst_lbl.RowCount = 1;
            this.tlp_s_c_gst_lbl.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_s_c_gst_lbl.Size = new System.Drawing.Size(336, 32);
            this.tlp_s_c_gst_lbl.TabIndex = 26;
            // 
            // lblsgst
            // 
            this.lblsgst.AutoSize = true;
            this.lblsgst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblsgst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsgst.Location = new System.Drawing.Point(4, 0);
            this.lblsgst.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblsgst.Name = "lblsgst";
            this.lblsgst.Size = new System.Drawing.Size(160, 32);
            this.lblsgst.TabIndex = 20;
            this.lblsgst.Text = "SGST";
            this.lblsgst.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblcgst
            // 
            this.lblcgst.AutoSize = true;
            this.lblcgst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcgst.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcgst.Location = new System.Drawing.Point(172, 0);
            this.lblcgst.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblcgst.Name = "lblcgst";
            this.lblcgst.Size = new System.Drawing.Size(160, 32);
            this.lblcgst.TabIndex = 18;
            this.lblcgst.Text = "CGST";
            this.lblcgst.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tlp_S_C_text
            // 
            this.tlp_S_C_text.ColumnCount = 2;
            this.tlp_S_C_text.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_S_C_text.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_S_C_text.Controls.Add(this.txtCgstPrcnt, 1, 1);
            this.tlp_S_C_text.Controls.Add(this.txtSgstPrcnt, 0, 1);
            this.tlp_S_C_text.Location = new System.Drawing.Point(268, 17);
            this.tlp_S_C_text.Margin = new System.Windows.Forms.Padding(4);
            this.tlp_S_C_text.Name = "tlp_S_C_text";
            this.tlp_S_C_text.RowCount = 2;
            this.tlp_S_C_text.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_S_C_text.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_S_C_text.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlp_S_C_text.Size = new System.Drawing.Size(291, 50);
            this.tlp_S_C_text.TabIndex = 14;
            // 
            // txtCgstPrcnt
            // 
            this.txtCgstPrcnt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCgstPrcnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.txtCgstPrcnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtCgstPrcnt.Location = new System.Drawing.Point(149, 4);
            this.txtCgstPrcnt.Margin = new System.Windows.Forms.Padding(4);
            this.txtCgstPrcnt.Multiline = true;
            this.txtCgstPrcnt.Name = "txtCgstPrcnt";
            this.txtCgstPrcnt.ReadOnly = true;
            this.txtCgstPrcnt.Size = new System.Drawing.Size(138, 37);
            this.txtCgstPrcnt.TabIndex = 22;
            // 
            // txtSgstPrcnt
            // 
            this.txtSgstPrcnt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSgstPrcnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.txtSgstPrcnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.txtSgstPrcnt.Location = new System.Drawing.Point(4, 4);
            this.txtSgstPrcnt.Margin = new System.Windows.Forms.Padding(4);
            this.txtSgstPrcnt.Multiline = true;
            this.txtSgstPrcnt.Name = "txtSgstPrcnt";
            this.txtSgstPrcnt.ReadOnly = true;
            this.txtSgstPrcnt.Size = new System.Drawing.Size(137, 37);
            this.txtSgstPrcnt.TabIndex = 23;
            // 
            // chb_active
            // 
            this.chb_active.AutoSize = true;
            this.chb_active.Checked = true;
            this.chb_active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_active.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chb_active.Location = new System.Drawing.Point(436, 18);
            this.chb_active.Margin = new System.Windows.Forms.Padding(160, 25, 4, 4);
            this.chb_active.Name = "chb_active";
            this.chb_active.Size = new System.Drawing.Size(18, 17);
            this.chb_active.TabIndex = 9;
            this.chb_active.UseVisualStyleBackColor = true;
            this.chb_active.Visible = false;
            // 
            // FrmPartyPackSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1114, 670);
            this.Controls.Add(this.pnl_);
            this.Controls.Add(this.tlp_drawer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPartyPackSetup";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Text = "FrmPartyPackSetup";
            this.Load += new System.EventHandler(this.FrmPartyPackSetup_Load);
            this.tlp_drawer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_packs)).EndInit();
            this.tlp_registration.ResumeLayout(false);
            this.tlp_registration.PerformLayout();
            this.tlp_setting.ResumeLayout(false);
            this.tlp_setting.PerformLayout();
            this.tlp_d_save_canl.ResumeLayout(false);
            this.pnl_.ResumeLayout(false);
            this.pnl_.PerformLayout();
            this.tlp_s_c_gst_lbl.ResumeLayout(false);
            this.tlp_s_c_gst_lbl.PerformLayout();
            this.tlp_S_C_text.ResumeLayout(false);
            this.tlp_S_C_text.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.TableLayoutPanel tlp_drawer;
        private System.Windows.Forms.TableLayoutPanel tlp_setting;
        private System.Windows.Forms.Button btnclose_drawer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tlp_d_save_canl;
        private System.Windows.Forms.TableLayoutPanel tlp_registration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtpackname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtprice;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDisPer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDiscAmnt;
        private System.Windows.Forms.Panel pnl_;
        private System.Windows.Forms.Label lblvat;
        private System.Windows.Forms.Label lbligst;
        private System.Windows.Forms.ComboBox cmbtax;
        private System.Windows.Forms.Label lbltax;
        private System.Windows.Forms.ComboBox cmbTaxType;
        private System.Windows.Forms.Label lbltaxtype;
        private System.Windows.Forms.ComboBox cmbVatPrcnt;
        private System.Windows.Forms.ComboBox cmbIgstPrcnt;
        private System.Windows.Forms.TableLayoutPanel tlp_s_c_gst_lbl;
        private System.Windows.Forms.Label lblsgst;
        private System.Windows.Forms.Label lblcgst;
        private System.Windows.Forms.TableLayoutPanel tlp_S_C_text;
        private System.Windows.Forms.TextBox txtCgstPrcnt;
        private System.Windows.Forms.TextBox txtSgstPrcnt;
        private System.Windows.Forms.CheckBox chb_active;
        private System.Windows.Forms.Button btn_del_pack;
        private System.Windows.Forms.DataGridView dgv_packs;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.DataGridViewTextBoxColumn slno;
        private System.Windows.Forms.DataGridViewTextBoxColumn item;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewImageColumn action;
        private System.Windows.Forms.DataGridViewTextBoxColumn pPartyPackDetailsId;
    }
}