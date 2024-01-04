
namespace FoodandBeverage.Forms
{
    partial class FrmVendor
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtvid = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.tlp_button = new System.Windows.Forms.TableLayoutPanel();
            this.btnclose = new System.Windows.Forms.Button();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_outer_design = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_registration = new System.Windows.Forms.TableLayoutPanel();
            this.txtVendorEmail = new System.Windows.Forms.TextBox();
            this.txtVenderAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVendorName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtVendorTel = new System.Windows.Forms.TextBox();
            this.btn_del_vendor = new System.Windows.Forms.Button();
            this.txtdesc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chk_active = new System.Windows.Forms.CheckBox();
            this.tlp_navbar = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.btnmini = new System.Windows.Forms.Button();
            this.btncolse = new System.Windows.Forms.Button();
            this.dgv_vendor = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phoneno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tlp_button.SuspendLayout();
            this.tlp_main.SuspendLayout();
            this.tlp_outer_design.SuspendLayout();
            this.tlp_registration.SuspendLayout();
            this.tlp_navbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_vendor)).BeginInit();
            this.SuspendLayout();
            // 
            // txtvid
            // 
            this.txtvid.Location = new System.Drawing.Point(359, 27);
            this.txtvid.Name = "txtvid";
            this.txtvid.Size = new System.Drawing.Size(13, 20);
            this.txtvid.TabIndex = 10;
            this.txtvid.Visible = false;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImage = global::FoodandBeverage.Properties.Resources.searchbutton;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(105, 4);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(95, 31);
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
            this.btnsave.Location = new System.Drawing.Point(3, 3);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(95, 33);
            this.btnsave.TabIndex = 0;
            this.btnsave.UseVisualStyleBackColor = false;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnrefresh
            // 
            this.btnrefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.BackgroundImage = global::FoodandBeverage.Properties.Resources.refresh_Button1;
            this.btnrefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrefresh.FlatAppearance.BorderSize = 0;
            this.btnrefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrefresh.Location = new System.Drawing.Point(207, 3);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(95, 33);
            this.btnrefresh.TabIndex = 2;
            this.btnrefresh.UseVisualStyleBackColor = false;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // tlp_button
            // 
            this.tlp_button.ColumnCount = 4;
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tlp_button.Controls.Add(this.btnSearch, 0, 0);
            this.tlp_button.Controls.Add(this.btnsave, 0, 0);
            this.tlp_button.Controls.Add(this.btnrefresh, 1, 0);
            this.tlp_button.Controls.Add(this.btnclose, 2, 0);
            this.tlp_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.tlp_button.Location = new System.Drawing.Point(285, 252);
            this.tlp_button.Name = "tlp_button";
            this.tlp_button.RowCount = 1;
            this.tlp_button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_button.Size = new System.Drawing.Size(438, 39);
            this.tlp_button.TabIndex = 5;
            // 
            // btnclose
            // 
            this.btnclose.BackColor = System.Drawing.Color.Transparent;
            this.btnclose.BackgroundImage = global::FoodandBeverage.Properties.Resources.Closebutton;
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclose.FlatAppearance.BorderSize = 0;
            this.btnclose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnclose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.Location = new System.Drawing.Point(308, 3);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(95, 33);
            this.btnclose.TabIndex = 3;
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // tlp_main
            // 
            this.tlp_main.BackColor = System.Drawing.Color.AliceBlue;
            this.tlp_main.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tlp_outer_design, 0, 1);
            this.tlp_main.Controls.Add(this.tlp_navbar, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_button, 0, 2);
            this.tlp_main.Controls.Add(this.dgv_vendor, 0, 3);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 5;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_main.Size = new System.Drawing.Size(729, 658);
            this.tlp_main.TabIndex = 11;
            // 
            // tlp_outer_design
            // 
            this.tlp_outer_design.BackColor = System.Drawing.Color.AliceBlue;
            this.tlp_outer_design.ColumnCount = 3;
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 98F));
            this.tlp_outer_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tlp_outer_design.Controls.Add(this.tlp_registration, 1, 1);
            this.tlp_outer_design.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_outer_design.Location = new System.Drawing.Point(6, 46);
            this.tlp_outer_design.Name = "tlp_outer_design";
            this.tlp_outer_design.RowCount = 3;
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_outer_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlp_outer_design.Size = new System.Drawing.Size(717, 197);
            this.tlp_outer_design.TabIndex = 8;
            // 
            // tlp_registration
            // 
            this.tlp_registration.BackColor = System.Drawing.Color.White;
            this.tlp_registration.ColumnCount = 3;
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.63939F));
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.36061F));
            this.tlp_registration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 243F));
            this.tlp_registration.Controls.Add(this.txtVendorEmail, 2, 1);
            this.tlp_registration.Controls.Add(this.txtVenderAddress, 0, 3);
            this.tlp_registration.Controls.Add(this.label1, 0, 0);
            this.tlp_registration.Controls.Add(this.label3, 1, 0);
            this.tlp_registration.Controls.Add(this.txtVendorName, 0, 1);
            this.tlp_registration.Controls.Add(this.label2, 0, 2);
            this.tlp_registration.Controls.Add(this.label5, 2, 0);
            this.tlp_registration.Controls.Add(this.txtVendorTel, 1, 1);
            this.tlp_registration.Controls.Add(this.btn_del_vendor, 2, 3);
            this.tlp_registration.Controls.Add(this.txtdesc, 1, 3);
            this.tlp_registration.Controls.Add(this.label4, 1, 2);
            this.tlp_registration.Controls.Add(this.chk_active, 2, 2);
            this.tlp_registration.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_registration.Location = new System.Drawing.Point(16, 11);
            this.tlp_registration.Name = "tlp_registration";
            this.tlp_registration.RowCount = 4;
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.9521F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25.1497F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.56886F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.52695F));
            this.tlp_registration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_registration.Size = new System.Drawing.Size(683, 167);
            this.tlp_registration.TabIndex = 4;
            // 
            // txtVendorEmail
            // 
            this.txtVendorEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtVendorEmail.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtVendorEmail.Location = new System.Drawing.Point(442, 42);
            this.txtVendorEmail.MaxLength = 100;
            this.txtVendorEmail.Multiline = true;
            this.txtVendorEmail.Name = "txtVendorEmail";
            this.txtVendorEmail.Size = new System.Drawing.Size(238, 31);
            this.txtVendorEmail.TabIndex = 18;
            this.txtVendorEmail.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVendorEmail_KeyPress);
            // 
            // txtVenderAddress
            // 
            this.txtVenderAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtVenderAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtVenderAddress.Location = new System.Drawing.Point(3, 108);
            this.txtVenderAddress.Multiline = true;
            this.txtVenderAddress.Name = "txtVenderAddress";
            this.txtVenderAddress.Size = new System.Drawing.Size(216, 48);
            this.txtVenderAddress.TabIndex = 257;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 39);
            this.label1.TabIndex = 5;
            this.label1.Text = "Vendor Name";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(225, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(211, 39);
            this.label3.TabIndex = 7;
            this.label3.Text = "Phone No";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtVendorName
            // 
            this.txtVendorName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtVendorName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtVendorName.Location = new System.Drawing.Point(3, 42);
            this.txtVendorName.Multiline = true;
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.Size = new System.Drawing.Size(216, 31);
            this.txtVendorName.TabIndex = 0;
            this.txtVendorName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVendorName_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(216, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Address";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(442, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(238, 39);
            this.label5.TabIndex = 11;
            this.label5.Text = "Email id";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtVendorTel
            // 
            this.txtVendorTel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtVendorTel.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtVendorTel.Location = new System.Drawing.Point(225, 42);
            this.txtVendorTel.MaxLength = 12;
            this.txtVendorTel.Multiline = true;
            this.txtVendorTel.Name = "txtVendorTel";
            this.txtVendorTel.Size = new System.Drawing.Size(211, 31);
            this.txtVendorTel.TabIndex = 1;
            this.txtVendorTel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVendorTel_KeyPress);
            // 
            // btn_del_vendor
            // 
            this.btn_del_vendor.BackgroundImage = global::FoodandBeverage.Properties.Resources.delete;
            this.btn_del_vendor.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_del_vendor.FlatAppearance.BorderSize = 0;
            this.btn_del_vendor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_del_vendor.Location = new System.Drawing.Point(529, 108);
            this.btn_del_vendor.Margin = new System.Windows.Forms.Padding(90, 3, 3, 3);
            this.btn_del_vendor.Name = "btn_del_vendor";
            this.btn_del_vendor.Size = new System.Drawing.Size(48, 47);
            this.btn_del_vendor.TabIndex = 255;
            this.btn_del_vendor.UseVisualStyleBackColor = true;
            this.btn_del_vendor.Visible = false;
            this.btn_del_vendor.Click += new System.EventHandler(this.btn_del_vendor_Click);
            // 
            // txtdesc
            // 
            this.txtdesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtdesc.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtdesc.Location = new System.Drawing.Point(225, 108);
            this.txtdesc.Multiline = true;
            this.txtdesc.Name = "txtdesc";
            this.txtdesc.Size = new System.Drawing.Size(211, 48);
            this.txtdesc.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(225, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.TabIndex = 256;
            this.label4.Text = "Description";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chk_active
            // 
            this.chk_active.AutoSize = true;
            this.chk_active.Checked = true;
            this.chk_active.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_active.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_active.Location = new System.Drawing.Point(539, 95);
            this.chk_active.Margin = new System.Windows.Forms.Padding(100, 15, 3, 3);
            this.chk_active.Name = "chk_active";
            this.chk_active.Size = new System.Drawing.Size(15, 7);
            this.chk_active.TabIndex = 9;
            this.chk_active.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chk_active.UseVisualStyleBackColor = true;
            this.chk_active.Visible = false;
            // 
            // tlp_navbar
            // 
            this.tlp_navbar.BackColor = System.Drawing.Color.SteelBlue;
            this.tlp_navbar.ColumnCount = 3;
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_navbar.Controls.Add(this.label6, 0, 0);
            this.tlp_navbar.Controls.Add(this.btnmini, 1, 0);
            this.tlp_navbar.Controls.Add(this.btncolse, 2, 0);
            this.tlp_navbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_navbar.Location = new System.Drawing.Point(6, 6);
            this.tlp_navbar.Name = "tlp_navbar";
            this.tlp_navbar.RowCount = 1;
            this.tlp_navbar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.Size = new System.Drawing.Size(717, 31);
            this.tlp_navbar.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(195, 24);
            this.label6.TabIndex = 12;
            this.label6.Text = "Vendor Registartion";
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(658, 3);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(25, 25);
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
            this.btncolse.Location = new System.Drawing.Point(689, 3);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(25, 25);
            this.btncolse.TabIndex = 10;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // dgv_vendor
            // 
            this.dgv_vendor.AllowUserToAddRows = false;
            this.dgv_vendor.AllowUserToDeleteRows = false;
            this.dgv_vendor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_vendor.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_vendor.ColumnHeadersHeight = 46;
            this.dgv_vendor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.vid,
            this.Name,
            this.phoneno,
            this.emailid,
            this.address,
            this.description,
            this.Status});
            this.dgv_vendor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_vendor.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_vendor.Location = new System.Drawing.Point(6, 300);
            this.dgv_vendor.Name = "dgv_vendor";
            this.dgv_vendor.ReadOnly = true;
            this.dgv_vendor.RowHeadersVisible = false;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dgv_vendor.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_vendor.RowTemplate.Height = 30;
            this.dgv_vendor.Size = new System.Drawing.Size(717, 346);
            this.dgv_vendor.TabIndex = 6;
            this.dgv_vendor.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_vendor_CellDoubleClick);
            // 
            // Id
            // 
            dataGridViewCellStyle2.Format = "N0";
            dataGridViewCellStyle2.NullValue = "0";
            this.Id.DefaultCellStyle = dataGridViewCellStyle2;
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            // 
            // vid
            // 
            this.vid.HeaderText = "ID";
            this.vid.Name = "vid";
            this.vid.ReadOnly = true;
            this.vid.Visible = false;
            // 
            // Name
            // 
            this.Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Name.HeaderText = "Name";
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            // 
            // phoneno
            // 
            this.phoneno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.phoneno.HeaderText = "Phone No";
            this.phoneno.Name = "phoneno";
            this.phoneno.ReadOnly = true;
            // 
            // emailid
            // 
            this.emailid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.emailid.HeaderText = "Email  ID";
            this.emailid.Name = "emailid";
            this.emailid.ReadOnly = true;
            // 
            // address
            // 
            this.address.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.address.HeaderText = "Address";
            this.address.Name = "address";
            this.address.ReadOnly = true;
            // 
            // description
            // 
            this.description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.description.HeaderText = "Description";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Visible = false;
            this.Status.Width = 62;
            // 
            // FrmVendor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 658);
            this.Controls.Add(this.txtvid);
            this.Controls.Add(this.tlp_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Text = "FrmVendor";
            this.Load += new System.EventHandler(this.FrmVendor_Load);
            this.Resize += new System.EventHandler(this.FrmVendor_Resize);
            this.tlp_button.ResumeLayout(false);
            this.tlp_main.ResumeLayout(false);
            this.tlp_outer_design.ResumeLayout(false);
            this.tlp_registration.ResumeLayout(false);
            this.tlp_registration.PerformLayout();
            this.tlp_navbar.ResumeLayout(false);
            this.tlp_navbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_vendor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtvid;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.TableLayoutPanel tlp_button;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TableLayoutPanel tlp_outer_design;
        private System.Windows.Forms.TableLayoutPanel tlp_registration;
        private System.Windows.Forms.TextBox txtVendorEmail;
        private System.Windows.Forms.TextBox txtVenderAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtVendorName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtVendorTel;
        private System.Windows.Forms.Button btn_del_vendor;
        private System.Windows.Forms.TextBox txtdesc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox chk_active;
        private System.Windows.Forms.TableLayoutPanel tlp_navbar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.DataGridView dgv_vendor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn vid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn phoneno;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailid;
        private System.Windows.Forms.DataGridViewTextBoxColumn address;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Status;
    }
}