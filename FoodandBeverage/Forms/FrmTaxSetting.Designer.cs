
namespace FoodandBeverage.Forms
{
    partial class FrmTaxSetting
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
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_navbar = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnmini = new System.Windows.Forms.Button();
            this.btncolse = new System.Windows.Forms.Button();
            this.tlp_submain = new System.Windows.Forms.TableLayoutPanel();
            this.dgvTaxSettings = new System.Windows.Forms.DataGridView();
            this.no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taxid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taxtype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.igst = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sgst = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cgst = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tlp_outer_design = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_registration = new System.Windows.Forms.TableLayoutPanel();
            this.btn_del_tax = new System.Windows.Forms.Button();
            this.tlp_button = new System.Windows.Forms.TableLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.txtSgstPrcnt = new System.Windows.Forms.TextBox();
            this.txtCgstPrcnt = new System.Windows.Forms.TextBox();
            this.lbltaxtype = new System.Windows.Forms.Label();
            this.lblvat = new System.Windows.Forms.Label();
            this.lblsgst = new System.Windows.Forms.Label();
            this.lbligst = new System.Windows.Forms.Label();
            this.txtIgstPrcnt = new System.Windows.Forms.TextBox();
            this.cmbTaxType = new System.Windows.Forms.ComboBox();
            this.txtVatPrcnt = new System.Windows.Forms.TextBox();
            this.lblcgst = new System.Windows.Forms.Label();
            this.pnl_all_controls = new System.Windows.Forms.Panel();
            this.ch_default = new System.Windows.Forms.CheckBox();
            this.chb_active = new System.Windows.Forms.CheckBox();
            this.tlp_main.SuspendLayout();
            this.tlp_navbar.SuspendLayout();
            this.tlp_submain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxSettings)).BeginInit();
            this.tlp_outer_design.SuspendLayout();
            this.tlp_button.SuspendLayout();
            this.pnl_all_controls.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_main
            // 
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tlp_navbar, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_submain, 0, 1);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tlp_main.RowCount = 3;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlp_main.Size = new System.Drawing.Size(1067, 810);
            this.tlp_main.TabIndex = 0;
            // 
            // tlp_navbar
            // 
            this.tlp_navbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(80)))), ((int)(((byte)(185)))));
            this.tlp_navbar.ColumnCount = 3;
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_navbar.Controls.Add(this.label2, 0, 0);
            this.tlp_navbar.Controls.Add(this.btnmini, 1, 0);
            this.tlp_navbar.Controls.Add(this.btncolse, 2, 0);
            this.tlp_navbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_navbar.Location = new System.Drawing.Point(11, 10);
            this.tlp_navbar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_navbar.Name = "tlp_navbar";
            this.tlp_navbar.RowCount = 1;
            this.tlp_navbar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.Size = new System.Drawing.Size(1045, 38);
            this.tlp_navbar.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(4, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 29);
            this.label2.TabIndex = 12;
            this.label2.Text = "Tax Setting";
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(967, 4);
            this.btnmini.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(33, 30);
            this.btnmini.TabIndex = 9;
            this.btnmini.Text = "-";
            this.btnmini.UseVisualStyleBackColor = false;
            this.btnmini.Click += new System.EventHandler(this.btnmini_Click);
            // 
            // btncolse
            // 
            this.btncolse.BackColor = System.Drawing.Color.White;
            this.btncolse.FlatAppearance.BorderSize = 0;
            this.btncolse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncolse.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncolse.ForeColor = System.Drawing.Color.Red;
            this.btncolse.Location = new System.Drawing.Point(1008, 4);
            this.btncolse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(33, 30);
            this.btncolse.TabIndex = 10;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // tlp_submain
            // 
            this.tlp_submain.ColumnCount = 1;
            this.tlp_submain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_submain.Controls.Add(this.dgvTaxSettings, 0, 2);
            this.tlp_submain.Controls.Add(this.tlp_outer_design, 0, 0);
            this.tlp_submain.Controls.Add(this.tlp_button, 0, 1);
            this.tlp_submain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_submain.Location = new System.Drawing.Point(11, 56);
            this.tlp_submain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_submain.Name = "tlp_submain";
            this.tlp_submain.RowCount = 3;
            this.tlp_submain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_submain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_submain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_submain.Size = new System.Drawing.Size(1045, 692);
            this.tlp_submain.TabIndex = 19;
            // 
            // dgvTaxSettings
            // 
            this.dgvTaxSettings.AllowUserToAddRows = false;
            this.dgvTaxSettings.AllowUserToDeleteRows = false;
            this.dgvTaxSettings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTaxSettings.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTaxSettings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTaxSettings.ColumnHeadersHeight = 46;
            this.dgvTaxSettings.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no,
            this.taxid,
            this.taxtype,
            this.igst,
            this.sgst,
            this.cgst,
            this.vat,
            this.Status});
            this.dgvTaxSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTaxSettings.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvTaxSettings.Location = new System.Drawing.Point(4, 359);
            this.dgvTaxSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvTaxSettings.Name = "dgvTaxSettings";
            this.dgvTaxSettings.ReadOnly = true;
            this.dgvTaxSettings.RowHeadersVisible = false;
            this.dgvTaxSettings.RowHeadersWidth = 51;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dgvTaxSettings.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvTaxSettings.RowTemplate.Height = 30;
            this.dgvTaxSettings.Size = new System.Drawing.Size(1037, 372);
            this.dgvTaxSettings.TabIndex = 12;
            this.dgvTaxSettings.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTaxSettings_CellDoubleClick);
            // 
            // no
            // 
            this.no.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.no.HeaderText = "Slno";
            this.no.MinimumWidth = 6;
            this.no.Name = "no";
            this.no.ReadOnly = true;
            this.no.Width = 81;
            // 
            // taxid
            // 
            this.taxid.HeaderText = "Taxid";
            this.taxid.MinimumWidth = 6;
            this.taxid.Name = "taxid";
            this.taxid.ReadOnly = true;
            this.taxid.Visible = false;
            // 
            // taxtype
            // 
            this.taxtype.HeaderText = "Tax Type";
            this.taxtype.MinimumWidth = 6;
            this.taxtype.Name = "taxtype";
            this.taxtype.ReadOnly = true;
            this.taxtype.Visible = false;
            // 
            // igst
            // 
            this.igst.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.igst.HeaderText = "IGST";
            this.igst.MinimumWidth = 6;
            this.igst.Name = "igst";
            this.igst.ReadOnly = true;
            // 
            // sgst
            // 
            this.sgst.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.sgst.HeaderText = "SGST";
            this.sgst.MinimumWidth = 6;
            this.sgst.Name = "sgst";
            this.sgst.ReadOnly = true;
            // 
            // cgst
            // 
            this.cgst.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cgst.HeaderText = "CGST";
            this.cgst.MinimumWidth = 6;
            this.cgst.Name = "cgst";
            this.cgst.ReadOnly = true;
            // 
            // vat
            // 
            this.vat.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.vat.HeaderText = "VAT";
            this.vat.MinimumWidth = 6;
            this.vat.Name = "vat";
            this.vat.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Visible = false;
            this.Status.Width = 74;
            // 
            // tlp_outer_design
            // 
            this.tlp_outer_design.AutoSize = true;
            this.tlp_outer_design.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_outer_design.ColumnCount = 1;
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_outer_design.Controls.Add(this.tlp_registration, 0, 1);
            this.tlp_outer_design.Controls.Add(this.btn_del_tax, 0, 2);
            this.tlp_outer_design.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_outer_design.Location = new System.Drawing.Point(4, 4);
            this.tlp_outer_design.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_outer_design.Name = "tlp_outer_design";
            this.tlp_outer_design.RowCount = 3;
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_outer_design.Size = new System.Drawing.Size(1037, 290);
            this.tlp_outer_design.TabIndex = 10;
            // 
            // tlp_registration
            // 
            this.tlp_registration.BackColor = System.Drawing.Color.White;
            this.tlp_registration.ColumnCount = 3;
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332F));
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tlp_registration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_registration.Location = new System.Drawing.Point(4, 14);
            this.tlp_registration.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_registration.Name = "tlp_registration";
            this.tlp_registration.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tlp_registration.RowCount = 5;
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_registration.Size = new System.Drawing.Size(1029, 212);
            this.tlp_registration.TabIndex = 4;
            // 
            // btn_del_tax
            // 
            this.btn_del_tax.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_TRASH;
            this.btn_del_tax.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_del_tax.FlatAppearance.BorderSize = 0;
            this.btn_del_tax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_del_tax.Location = new System.Drawing.Point(120, 234);
            this.btn_del_tax.Margin = new System.Windows.Forms.Padding(120, 4, 4, 4);
            this.btn_del_tax.Name = "btn_del_tax";
            this.btn_del_tax.Size = new System.Drawing.Size(64, 52);
            this.btn_del_tax.TabIndex = 255;
            this.btn_del_tax.UseVisualStyleBackColor = true;
            this.btn_del_tax.Visible = false;
            this.btn_del_tax.Click += new System.EventHandler(this.btn_del_tax_Click);
            // 
            // tlp_button
            // 
            this.tlp_button.ColumnCount = 4;
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tlp_button.Controls.Add(this.btnSearch, 0, 0);
            this.tlp_button.Controls.Add(this.btnsave, 0, 0);
            this.tlp_button.Controls.Add(this.btnrefresh, 1, 0);
            this.tlp_button.Controls.Add(this.btnclose, 2, 0);
            this.tlp_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.tlp_button.Location = new System.Drawing.Point(468, 302);
            this.tlp_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_button.Name = "tlp_button";
            this.tlp_button.RowCount = 1;
            this.tlp_button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_button.Size = new System.Drawing.Size(573, 49);
            this.tlp_button.TabIndex = 11;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_SEARCH;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(140, 5);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(127, 39);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.Color.Transparent;
            this.btnsave.BackgroundImage = global::FoodandBeverage.Properties.Resources.SaveButton;
            this.btnsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnsave.FlatAppearance.BorderSize = 0;
            this.btnsave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnsave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnsave.Location = new System.Drawing.Point(4, 4);
            this.btnsave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(127, 39);
            this.btnsave.TabIndex = 0;
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnrefresh
            // 
            this.btnrefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_refresh;
            this.btnrefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrefresh.FlatAppearance.BorderSize = 0;
            this.btnrefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrefresh.Location = new System.Drawing.Point(276, 4);
            this.btnrefresh.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(127, 39);
            this.btnrefresh.TabIndex = 2;
            this.btnrefresh.UseVisualStyleBackColor = false;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.Transparent;
            this.btnclose.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_CANCEL;
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclose.FlatAppearance.BorderSize = 0;
            this.btnclose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnclose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.Location = new System.Drawing.Point(411, 4);
            this.btnclose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(127, 39);
            this.btnclose.TabIndex = 3;
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // txtSgstPrcnt
            // 
            this.txtSgstPrcnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.txtSgstPrcnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSgstPrcnt.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSgstPrcnt.Location = new System.Drawing.Point(0, 0);
            this.txtSgstPrcnt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSgstPrcnt.Multiline = true;
            this.txtSgstPrcnt.Name = "txtSgstPrcnt";
            this.txtSgstPrcnt.Size = new System.Drawing.Size(112, 132);
            this.txtSgstPrcnt.TabIndex = 259;
            this.txtSgstPrcnt.TextChanged += new System.EventHandler(this.txtSgstPrcnt_TextChanged);
            this.txtSgstPrcnt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSgstPrcnt_KeyPress);
            // 
            // txtCgstPrcnt
            // 
            this.txtCgstPrcnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.txtCgstPrcnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCgstPrcnt.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtCgstPrcnt.Location = new System.Drawing.Point(0, 132);
            this.txtCgstPrcnt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtCgstPrcnt.Multiline = true;
            this.txtCgstPrcnt.Name = "txtCgstPrcnt";
            this.txtCgstPrcnt.Size = new System.Drawing.Size(112, 34);
            this.txtCgstPrcnt.TabIndex = 258;
            this.txtCgstPrcnt.TextChanged += new System.EventHandler(this.txtCgstPrcnt_TextChanged);
            this.txtCgstPrcnt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSgstPrcnt_KeyPress);
            // 
            // lbltaxtype
            // 
            this.lbltaxtype.AutoSize = true;
            this.lbltaxtype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltaxtype.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltaxtype.Location = new System.Drawing.Point(0, 0);
            this.lbltaxtype.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbltaxtype.Name = "lbltaxtype";
            this.lbltaxtype.Size = new System.Drawing.Size(104, 25);
            this.lbltaxtype.TabIndex = 5;
            this.lbltaxtype.Text = "Tax Type";
            this.lbltaxtype.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblvat
            // 
            this.lblvat.AutoSize = true;
            this.lblvat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblvat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblvat.Location = new System.Drawing.Point(0, 0);
            this.lblvat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblvat.Name = "lblvat";
            this.lblvat.Size = new System.Drawing.Size(56, 25);
            this.lblvat.TabIndex = 7;
            this.lblvat.Text = "VAT";
            this.lblvat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblsgst
            // 
            this.lblsgst.AutoSize = true;
            this.lblsgst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblsgst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsgst.Location = new System.Drawing.Point(0, 0);
            this.lblsgst.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblsgst.Name = "lblsgst";
            this.lblsgst.Size = new System.Drawing.Size(72, 25);
            this.lblsgst.TabIndex = 6;
            this.lblsgst.Text = "SGST";
            this.lblsgst.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbligst
            // 
            this.lbligst.AutoSize = true;
            this.lbligst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbligst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbligst.Location = new System.Drawing.Point(0, 0);
            this.lbligst.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbligst.Name = "lbligst";
            this.lbligst.Size = new System.Drawing.Size(63, 25);
            this.lbligst.TabIndex = 11;
            this.lbligst.Text = "IGST";
            this.lbligst.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtIgstPrcnt
            // 
            this.txtIgstPrcnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.txtIgstPrcnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtIgstPrcnt.Location = new System.Drawing.Point(153, 49);
            this.txtIgstPrcnt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtIgstPrcnt.Multiline = true;
            this.txtIgstPrcnt.Name = "txtIgstPrcnt";
            this.txtIgstPrcnt.Size = new System.Drawing.Size(244, 34);
            this.txtIgstPrcnt.TabIndex = 0;
            this.txtIgstPrcnt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtIgstPrcnt_KeyPress);
            this.txtIgstPrcnt.Validating += new System.ComponentModel.CancelEventHandler(this.txtIgstPrcnt_Validating);
            // 
            // cmbTaxType
            // 
            this.cmbTaxType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.cmbTaxType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbTaxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaxType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTaxType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F);
            this.cmbTaxType.FormattingEnabled = true;
            this.cmbTaxType.Items.AddRange(new object[] {
            "GST",
            "VAT"});
            this.cmbTaxType.Location = new System.Drawing.Point(0, 0);
            this.cmbTaxType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbTaxType.Name = "cmbTaxType";
            this.cmbTaxType.Size = new System.Drawing.Size(112, 33);
            this.cmbTaxType.TabIndex = 10;
            this.cmbTaxType.SelectedIndexChanged += new System.EventHandler(this.cmbTaxType_SelectedIndexChanged);
            // 
            // txtVatPrcnt
            // 
            this.txtVatPrcnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.txtVatPrcnt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtVatPrcnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtVatPrcnt.Location = new System.Drawing.Point(485, 118);
            this.txtVatPrcnt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtVatPrcnt.MaxLength = 5;
            this.txtVatPrcnt.Multiline = true;
            this.txtVatPrcnt.Name = "txtVatPrcnt";
            this.txtVatPrcnt.Size = new System.Drawing.Size(344, 38);
            this.txtVatPrcnt.TabIndex = 256;
            this.txtVatPrcnt.Validating += new System.ComponentModel.CancelEventHandler(this.txtVatPrcnt_Validating);
            // 
            // lblcgst
            // 
            this.lblcgst.AutoSize = true;
            this.lblcgst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcgst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcgst.Location = new System.Drawing.Point(0, 0);
            this.lblcgst.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblcgst.Name = "lblcgst";
            this.lblcgst.Size = new System.Drawing.Size(73, 25);
            this.lblcgst.TabIndex = 257;
            this.lblcgst.Text = "CGST";
            this.lblcgst.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnl_all_controls
            // 
            this.pnl_all_controls.Controls.Add(this.ch_default);
            this.pnl_all_controls.Controls.Add(this.txtCgstPrcnt);
            this.pnl_all_controls.Controls.Add(this.txtSgstPrcnt);
            this.pnl_all_controls.Controls.Add(this.lbltaxtype);
            this.pnl_all_controls.Controls.Add(this.lblcgst);
            this.pnl_all_controls.Controls.Add(this.txtIgstPrcnt);
            this.pnl_all_controls.Controls.Add(this.lbligst);
            this.pnl_all_controls.Controls.Add(this.txtVatPrcnt);
            this.pnl_all_controls.Controls.Add(this.cmbTaxType);
            this.pnl_all_controls.Controls.Add(this.lblvat);
            this.pnl_all_controls.Controls.Add(this.lblsgst);
            this.pnl_all_controls.Location = new System.Drawing.Point(0, 0);
            this.pnl_all_controls.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnl_all_controls.Name = "pnl_all_controls";
            this.pnl_all_controls.Size = new System.Drawing.Size(112, 84);
            this.pnl_all_controls.TabIndex = 260;
            this.pnl_all_controls.Visible = false;
            // 
            // ch_default
            // 
            this.ch_default.AutoSize = true;
            this.ch_default.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ch_default.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.ch_default.Location = new System.Drawing.Point(0, 166);
            this.ch_default.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ch_default.Name = "ch_default";
            this.ch_default.Size = new System.Drawing.Size(112, 0);
            this.ch_default.TabIndex = 260;
            this.ch_default.Text = "Default";
            this.ch_default.UseVisualStyleBackColor = true;
            // 
            // chb_active
            // 
            this.chb_active.AutoSize = true;
            this.chb_active.Location = new System.Drawing.Point(0, 0);
            this.chb_active.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chb_active.Name = "chb_active";
            this.chb_active.Size = new System.Drawing.Size(68, 21);
            this.chb_active.TabIndex = 0;
            this.chb_active.Text = "Active";
            this.chb_active.UseVisualStyleBackColor = true;
            this.chb_active.Visible = false;
            // 
            // FrmTaxSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(1067, 810);
            this.Controls.Add(this.chb_active);
            this.Controls.Add(this.pnl_all_controls);
            this.Controls.Add(this.tlp_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmTaxSetting";
            this.Tag = "11";
            this.Text = "FrmTaxSetting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTaxSetting_FormClosing);
            this.Load += new System.EventHandler(this.FrmTaxSetting_Load);
            this.Resize += new System.EventHandler(this.FrmTaxSetting_Resize);
            this.tlp_main.ResumeLayout(false);
            this.tlp_navbar.ResumeLayout(false);
            this.tlp_navbar.PerformLayout();
            this.tlp_submain.ResumeLayout(false);
            this.tlp_submain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxSettings)).EndInit();
            this.tlp_outer_design.ResumeLayout(false);
            this.tlp_button.ResumeLayout(false);
            this.pnl_all_controls.ResumeLayout(false);
            this.pnl_all_controls.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TableLayoutPanel tlp_navbar;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.TableLayoutPanel tlp_submain;
        private System.Windows.Forms.TableLayoutPanel tlp_outer_design;
        private System.Windows.Forms.TableLayoutPanel tlp_registration;
        private System.Windows.Forms.Button btn_del_tax;
        private System.Windows.Forms.Label lbltaxtype;
        private System.Windows.Forms.Label lblvat;
        private System.Windows.Forms.TextBox txtIgstPrcnt;
        private System.Windows.Forms.ComboBox cmbTaxType;
        private System.Windows.Forms.Label lblsgst;
        private System.Windows.Forms.Label lbligst;
        private System.Windows.Forms.TableLayoutPanel tlp_button;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.DataGridView dgvTaxSettings;
        private System.Windows.Forms.TextBox txtVatPrcnt;
        private System.Windows.Forms.Label lblcgst;
        private System.Windows.Forms.TextBox txtSgstPrcnt;
        private System.Windows.Forms.TextBox txtCgstPrcnt;
        private System.Windows.Forms.DataGridViewTextBoxColumn no;
        private System.Windows.Forms.DataGridViewTextBoxColumn taxid;
        private System.Windows.Forms.DataGridViewTextBoxColumn taxtype;
        private System.Windows.Forms.DataGridViewTextBoxColumn igst;
        private System.Windows.Forms.DataGridViewTextBoxColumn sgst;
        private System.Windows.Forms.DataGridViewTextBoxColumn cgst;
        private System.Windows.Forms.DataGridViewTextBoxColumn vat;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Status;
        private System.Windows.Forms.Panel pnl_all_controls;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chb_active;
        private System.Windows.Forms.CheckBox ch_default;
    }
}