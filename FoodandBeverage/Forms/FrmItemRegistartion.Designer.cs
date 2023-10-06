
namespace FoodandBeverage.Forms
{
    partial class FrmItemRegistartion
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
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtdesc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtincr_name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_inc_categ = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chb_active = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbunit = new System.Windows.Forms.ComboBox();
            this.txtprice = new System.Windows.Forms.TextBox();
            this.txtqty = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.tlp_button = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_registration = new System.Windows.Forms.TableLayoutPanel();
            this.dgv_item = new System.Windows.Forms.DataGridView();
            this.no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.incrediantname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Incrediantcateg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.incre_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlp_registartion_toggle = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_outer_design = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_toggle = new System.Windows.Forms.Panel();
            this.btntoggle = new System.Windows.Forms.Button();
            this.txtitemid = new System.Windows.Forms.TextBox();
            this.tlp_navbar = new System.Windows.Forms.TableLayoutPanel();
            this.btnmini = new System.Windows.Forms.Button();
            this.btncolse = new System.Windows.Forms.Button();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_grid = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_toggle_grid = new System.Windows.Forms.Panel();
            this.toggle_grid = new System.Windows.Forms.Button();
            this.tlp_button.SuspendLayout();
            this.tlp_registration.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_item)).BeginInit();
            this.tlp_registartion_toggle.SuspendLayout();
            this.tlp_outer_design.SuspendLayout();
            this.pnl_toggle.SuspendLayout();
            this.tlp_navbar.SuspendLayout();
            this.tlp_main.SuspendLayout();
            this.tlp_grid.SuspendLayout();
            this.pnl_toggle_grid.SuspendLayout();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 257);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(143, 50);
            this.label5.TabIndex = 10;
            this.label5.Text = "Description";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 207);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 50);
            this.label6.TabIndex = 12;
            this.label6.Text = "Price";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 56);
            this.label1.TabIndex = 5;
            this.label1.Text = "Incredient Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtdesc
            // 
            this.txtdesc.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtdesc.Location = new System.Drawing.Point(152, 260);
            this.txtdesc.Multiline = true;
            this.txtdesc.Name = "txtdesc";
            this.txtdesc.Size = new System.Drawing.Size(290, 44);
            this.txtdesc.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(3, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 48);
            this.label7.TabIndex = 11;
            this.label7.Text = "Quantity";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtincr_name
            // 
            this.txtincr_name.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtincr_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtincr_name.Location = new System.Drawing.Point(152, 20);
            this.txtincr_name.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.txtincr_name.Name = "txtincr_name";
            this.txtincr_name.Size = new System.Drawing.Size(290, 24);
            this.txtincr_name.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 55);
            this.label3.TabIndex = 7;
            this.label3.Text = "incrdeiant Category";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmb_inc_categ
            // 
            this.cmb_inc_categ.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmb_inc_categ.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_inc_categ.FormattingEnabled = true;
            this.cmb_inc_categ.Location = new System.Drawing.Point(152, 76);
            this.cmb_inc_categ.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.cmb_inc_categ.Name = "cmb_inc_categ";
            this.cmb_inc_categ.Size = new System.Drawing.Size(290, 26);
            this.cmb_inc_categ.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 48);
            this.label2.TabIndex = 6;
            this.label2.Text = "Unit";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chb_active
            // 
            this.chb_active.AutoSize = true;
            this.chb_active.Checked = true;
            this.chb_active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_active.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chb_active.Location = new System.Drawing.Point(152, 327);
            this.chb_active.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.chb_active.Name = "chb_active";
            this.chb_active.Size = new System.Drawing.Size(15, 14);
            this.chb_active.TabIndex = 9;
            this.chb_active.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 307);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 54);
            this.label4.TabIndex = 8;
            this.label4.Text = "Active";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbunit
            // 
            this.cmbunit.Dock = System.Windows.Forms.DockStyle.Left;
            this.cmbunit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbunit.FormattingEnabled = true;
            this.cmbunit.Location = new System.Drawing.Point(152, 126);
            this.cmbunit.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.cmbunit.Name = "cmbunit";
            this.cmbunit.Size = new System.Drawing.Size(290, 26);
            this.cmbunit.TabIndex = 13;
            // 
            // txtprice
            // 
            this.txtprice.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtprice.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtprice.Location = new System.Drawing.Point(152, 227);
            this.txtprice.Margin = new System.Windows.Forms.Padding(3, 20, 3, 3);
            this.txtprice.Name = "txtprice";
            this.txtprice.Size = new System.Drawing.Size(290, 24);
            this.txtprice.TabIndex = 15;
            this.txtprice.TextChanged += new System.EventHandler(this.txtprice_TextChanged);
            // 
            // txtqty
            // 
            this.txtqty.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtqty.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtqty.Location = new System.Drawing.Point(152, 174);
            this.txtqty.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.txtqty.Name = "txtqty";
            this.txtqty.Size = new System.Drawing.Size(290, 24);
            this.txtqty.TabIndex = 14;
            this.txtqty.TextChanged += new System.EventHandler(this.txtqty_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Silver;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(83, 4);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(73, 44);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "search";
            this.btnSearch.UseVisualStyleBackColor = false;
            // 
            // btnsave
            // 
            this.btnsave.BackColor = System.Drawing.Color.Transparent;
            this.btnsave.BackgroundImage = global::FoodandBeverage.Properties.Resources._25512_4_save_button_clipart;
            this.btnsave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnsave.FlatAppearance.BorderSize = 0;
            this.btnsave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnsave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnsave.Location = new System.Drawing.Point(3, 3);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(73, 44);
            this.btnsave.TabIndex = 0;
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnrefresh
            // 
            this.btnrefresh.BackColor = System.Drawing.Color.Silver;
            this.btnrefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrefresh.FlatAppearance.BorderSize = 0;
            this.btnrefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrefresh.Location = new System.Drawing.Point(163, 3);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(73, 44);
            this.btnrefresh.TabIndex = 2;
            this.btnrefresh.Text = "refresh";
            this.btnrefresh.UseVisualStyleBackColor = false;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.Silver;
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclose.FlatAppearance.BorderSize = 0;
            this.btnclose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnclose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.Location = new System.Drawing.Point(242, 3);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(73, 44);
            this.btnclose.TabIndex = 3;
            this.btnclose.Text = "close";
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // tlp_button
            // 
            this.tlp_button.ColumnCount = 4;
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.Controls.Add(this.btnSearch, 0, 0);
            this.tlp_button.Controls.Add(this.btnsave, 0, 0);
            this.tlp_button.Controls.Add(this.btnrefresh, 1, 0);
            this.tlp_button.Controls.Add(this.btnclose, 2, 0);
            this.tlp_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.tlp_button.Location = new System.Drawing.Point(302, 400);
            this.tlp_button.Name = "tlp_button";
            this.tlp_button.RowCount = 1;
            this.tlp_button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_button.Size = new System.Drawing.Size(314, 53);
            this.tlp_button.TabIndex = 13;
            // 
            // tlp_registration
            // 
            this.tlp_registration.BackColor = System.Drawing.Color.White;
            this.tlp_registration.ColumnCount = 2;
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_registration.Controls.Add(this.label5, 0, 5);
            this.tlp_registration.Controls.Add(this.label6, 0, 4);
            this.tlp_registration.Controls.Add(this.label1, 0, 0);
            this.tlp_registration.Controls.Add(this.txtdesc, 1, 5);
            this.tlp_registration.Controls.Add(this.label7, 0, 3);
            this.tlp_registration.Controls.Add(this.txtincr_name, 1, 0);
            this.tlp_registration.Controls.Add(this.label3, 0, 1);
            this.tlp_registration.Controls.Add(this.cmb_inc_categ, 1, 1);
            this.tlp_registration.Controls.Add(this.label2, 0, 2);
            this.tlp_registration.Controls.Add(this.chb_active, 1, 6);
            this.tlp_registration.Controls.Add(this.label4, 0, 6);
            this.tlp_registration.Controls.Add(this.cmbunit, 1, 2);
            this.tlp_registration.Controls.Add(this.txtprice, 1, 4);
            this.tlp_registration.Controls.Add(this.txtqty, 1, 3);
            this.tlp_registration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_registration.Location = new System.Drawing.Point(13, 13);
            this.tlp_registration.Name = "tlp_registration";
            this.tlp_registration.RowCount = 7;
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.64003F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.46563F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.45043F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.45043F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.99783F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.99783F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.99783F));
            this.tlp_registration.Size = new System.Drawing.Size(589, 361);
            this.tlp_registration.TabIndex = 12;
            this.tlp_registration.Paint += new System.Windows.Forms.PaintEventHandler(this.tlp_registration_Paint);
            // 
            // dgv_item
            // 
            this.dgv_item.BackgroundColor = System.Drawing.Color.AliceBlue;
            this.dgv_item.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_item.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_item.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_item.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no,
            this.iid,
            this.incrediantname,
            this.Incrediantcateg,
            this.unit,
            this.Quantity,
            this.price,
            this.Description,
            this.status,
            this.incre_id,
            this.unitid});
            this.dgv_item.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_item.Location = new System.Drawing.Point(3, 3);
            this.dgv_item.Name = "dgv_item";
            this.dgv_item.Size = new System.Drawing.Size(118, 283);
            this.dgv_item.TabIndex = 14;
            // 
            // no
            // 
            this.no.HeaderText = "Slno";
            this.no.Name = "no";
            // 
            // iid
            // 
            this.iid.HeaderText = "iid";
            this.iid.Name = "iid";
            this.iid.Visible = false;
            // 
            // incrediantname
            // 
            this.incrediantname.HeaderText = "Incrediant Name";
            this.incrediantname.Name = "incrediantname";
            // 
            // Incrediantcateg
            // 
            this.Incrediantcateg.HeaderText = "Incrediant Category";
            this.Incrediantcateg.Name = "Incrediantcateg";
            // 
            // unit
            // 
            this.unit.HeaderText = "Unit";
            this.unit.Name = "unit";
            // 
            // Quantity
            // 
            this.Quantity.HeaderText = "Qty";
            this.Quantity.Name = "Quantity";
            // 
            // price
            // 
            this.price.HeaderText = "Price";
            this.price.Name = "price";
            // 
            // Description
            // 
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            // 
            // status
            // 
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.Visible = false;
            // 
            // incre_id
            // 
            this.incre_id.HeaderText = "incre_id";
            this.incre_id.Name = "incre_id";
            this.incre_id.Visible = false;
            // 
            // unitid
            // 
            this.unitid.HeaderText = "Unitid";
            this.unitid.Name = "unitid";
            this.unitid.Visible = false;
            // 
            // tlp_registartion_toggle
            // 
            this.tlp_registartion_toggle.ColumnCount = 2;
            this.tlp_registartion_toggle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_registartion_toggle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tlp_registartion_toggle.Controls.Add(this.tlp_outer_design, 0, 1);
            this.tlp_registartion_toggle.Controls.Add(this.tlp_button, 0, 2);
            this.tlp_registartion_toggle.Controls.Add(this.pnl_toggle, 1, 1);
            this.tlp_registartion_toggle.Controls.Add(this.txtitemid, 1, 2);
            this.tlp_registartion_toggle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_registartion_toggle.Location = new System.Drawing.Point(3, 51);
            this.tlp_registartion_toggle.Name = "tlp_registartion_toggle";
            this.tlp_registartion_toggle.RowCount = 3;
            this.tlp_registartion_toggle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_registartion_toggle.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_registartion_toggle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_registartion_toggle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_registartion_toggle.Size = new System.Drawing.Size(661, 456);
            this.tlp_registartion_toggle.TabIndex = 15;
            // 
            // tlp_outer_design
            // 
            this.tlp_outer_design.ColumnCount = 3;
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlp_outer_design.Controls.Add(this.tlp_registration, 1, 1);
            this.tlp_outer_design.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_outer_design.Location = new System.Drawing.Point(3, 3);
            this.tlp_outer_design.Name = "tlp_outer_design";
            this.tlp_outer_design.RowCount = 3;
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlp_outer_design.Size = new System.Drawing.Size(613, 391);
            this.tlp_outer_design.TabIndex = 11;
            // 
            // pnl_toggle
            // 
            this.pnl_toggle.Controls.Add(this.btntoggle);
            this.pnl_toggle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_toggle.Location = new System.Drawing.Point(622, 3);
            this.pnl_toggle.Name = "pnl_toggle";
            this.pnl_toggle.Size = new System.Drawing.Size(36, 391);
            this.pnl_toggle.TabIndex = 13;
            // 
            // btntoggle
            // 
            this.btntoggle.BackColor = System.Drawing.Color.Transparent;
            this.btntoggle.BackgroundImage = global::FoodandBeverage.Properties.Resources.table;
            this.btntoggle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btntoggle.Location = new System.Drawing.Point(0, 160);
            this.btntoggle.Name = "btntoggle";
            this.btntoggle.Size = new System.Drawing.Size(36, 40);
            this.btntoggle.TabIndex = 0;
            this.btntoggle.UseVisualStyleBackColor = false;
            this.btntoggle.Click += new System.EventHandler(this.btntoggle_Click);
            // 
            // txtitemid
            // 
            this.txtitemid.Location = new System.Drawing.Point(622, 400);
            this.txtitemid.Name = "txtitemid";
            this.txtitemid.Size = new System.Drawing.Size(30, 20);
            this.txtitemid.TabIndex = 16;
            this.txtitemid.Visible = false;
            // 
            // tlp_navbar
            // 
            this.tlp_navbar.BackColor = System.Drawing.Color.SteelBlue;
            this.tlp_navbar.ColumnCount = 3;
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_navbar.Controls.Add(this.btnmini, 1, 0);
            this.tlp_navbar.Controls.Add(this.btncolse, 2, 0);
            this.tlp_navbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_navbar.Location = new System.Drawing.Point(3, 3);
            this.tlp_navbar.Name = "tlp_navbar";
            this.tlp_navbar.RowCount = 1;
            this.tlp_navbar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.Size = new System.Drawing.Size(661, 42);
            this.tlp_navbar.TabIndex = 15;
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(602, 3);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(25, 36);
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
            this.btncolse.Location = new System.Drawing.Point(633, 3);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(25, 36);
            this.btncolse.TabIndex = 10;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // tlp_main
            // 
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tlp_navbar, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_registartion_toggle, 0, 1);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 2;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.411765F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.58823F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_main.Size = new System.Drawing.Size(667, 510);
            this.tlp_main.TabIndex = 16;
            // 
            // tlp_grid
            // 
            this.tlp_grid.ColumnCount = 2;
            this.tlp_grid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 93.28F));
            this.tlp_grid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.72F));
            this.tlp_grid.Controls.Add(this.pnl_toggle_grid, 1, 0);
            this.tlp_grid.Controls.Add(this.dgv_item, 0, 0);
            this.tlp_grid.Location = new System.Drawing.Point(830, 1);
            this.tlp_grid.Name = "tlp_grid";
            this.tlp_grid.RowCount = 1;
            this.tlp_grid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_grid.Size = new System.Drawing.Size(134, 289);
            this.tlp_grid.TabIndex = 11;
            // 
            // pnl_toggle_grid
            // 
            this.pnl_toggle_grid.Controls.Add(this.toggle_grid);
            this.pnl_toggle_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_toggle_grid.Location = new System.Drawing.Point(127, 3);
            this.pnl_toggle_grid.Name = "pnl_toggle_grid";
            this.pnl_toggle_grid.Size = new System.Drawing.Size(4, 283);
            this.pnl_toggle_grid.TabIndex = 14;
            // 
            // toggle_grid
            // 
            this.toggle_grid.BackColor = System.Drawing.Color.Transparent;
            this.toggle_grid.BackgroundImage = global::FoodandBeverage.Properties.Resources.item;
            this.toggle_grid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toggle_grid.Location = new System.Drawing.Point(0, 118);
            this.toggle_grid.Name = "toggle_grid";
            this.toggle_grid.Size = new System.Drawing.Size(36, 40);
            this.toggle_grid.TabIndex = 0;
            this.toggle_grid.UseVisualStyleBackColor = false;
            this.toggle_grid.Click += new System.EventHandler(this.toggle_grid_Click);
            // 
            // FrmItemRegistartion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(667, 510);
            this.Controls.Add(this.tlp_grid);
            this.Controls.Add(this.tlp_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmItemRegistartion";
            this.Text = "FrmItemRegistartion";
            this.tlp_button.ResumeLayout(false);
            this.tlp_registration.ResumeLayout(false);
            this.tlp_registration.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_item)).EndInit();
            this.tlp_registartion_toggle.ResumeLayout(false);
            this.tlp_registartion_toggle.PerformLayout();
            this.tlp_outer_design.ResumeLayout(false);
            this.pnl_toggle.ResumeLayout(false);
            this.tlp_navbar.ResumeLayout(false);
            this.tlp_main.ResumeLayout(false);
            this.tlp_grid.ResumeLayout(false);
            this.pnl_toggle_grid.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtdesc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtincr_name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_inc_categ;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chb_active;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbunit;
        private System.Windows.Forms.TextBox txtprice;
        private System.Windows.Forms.TextBox txtqty;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.TableLayoutPanel tlp_button;
        private System.Windows.Forms.TableLayoutPanel tlp_registration;
        private System.Windows.Forms.DataGridView dgv_item;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemid;
        private System.Windows.Forms.DataGridViewTextBoxColumn no;
        private System.Windows.Forms.DataGridViewTextBoxColumn iid;
        private System.Windows.Forms.DataGridViewTextBoxColumn incrediantname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Incrediantcateg;
        private System.Windows.Forms.DataGridViewTextBoxColumn unit;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.DataGridViewTextBoxColumn incre_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn unitid;
        private System.Windows.Forms.TableLayoutPanel tlp_registartion_toggle;
        private System.Windows.Forms.TableLayoutPanel tlp_navbar;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.Panel pnl_toggle;
        private System.Windows.Forms.Button btntoggle;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.TableLayoutPanel tlp_grid;
        private System.Windows.Forms.Panel pnl_toggle_grid;
        private System.Windows.Forms.Button toggle_grid;
        private System.Windows.Forms.TextBox txtitemid;
        private System.Windows.Forms.TableLayoutPanel tlp_outer_design;
    }
}