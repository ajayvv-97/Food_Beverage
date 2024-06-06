
namespace FoodandBeverage.Forms
{
    partial class FrmKitchenView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmKitchenView));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.btn_viewmore_order = new System.Windows.Forms.Button();
            this.tlp_navbar = new System.Windows.Forms.TableLayoutPanel();
            this.btnmini = new System.Windows.Forms.Button();
            this.btncolse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tlp_search = new System.Windows.Forms.TableLayoutPanel();
            this.txttablecode = new System.Windows.Forms.TextBox();
            this.txtorderid_search = new System.Windows.Forms.TextBox();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.btnprintcount = new System.Windows.Forms.Button();
            this.btntotalorders = new System.Windows.Forms.Button();
            this.lblprintloading = new System.Windows.Forms.Label();
            this.flp_order_view = new System.Windows.Forms.FlowLayoutPanel();
            this.pnl_products_rem_descr = new System.Windows.Forms.Panel();
            this.tlp_products_descr_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_product_des_reg = new System.Windows.Forms.TableLayoutPanel();
            this.txt_product_remove_des = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_cancel_product_des = new System.Windows.Forms.Button();
            this.btn_rem_product_des = new System.Windows.Forms.Button();
            this.btn_close_prod_des = new System.Windows.Forms.Button();
            this.lblheader_product_des = new System.Windows.Forms.Label();
            this.lblmsg = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnl_empty_msg = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlproductviewmore = new System.Windows.Forms.Panel();
            this.tlp_viewmore = new System.Windows.Forms.TableLayoutPanel();
            this.dgv_viewmore = new System.Windows.Forms.DataGridView();
            this.vslno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vproductname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vqty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.add_Ordermaster = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.add_koid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addorderdetailsid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addaction = new System.Windows.Forms.DataGridViewImageColumn();
            this.addavid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addpavid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addaddonstatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addonprice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addorderno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tlp_header_viewmore = new System.Windows.Forms.TableLayoutPanel();
            this.btn_close_viewmore = new System.Windows.Forms.Button();
            this.lblviewmore = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnl_kitchenorderBill = new System.Windows.Forms.Panel();
            this.crvkitchenbill = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.blinkTimer = new System.Windows.Forms.Timer(this.components);
            this.refreshtimer = new System.Windows.Forms.Timer(this.components);
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.tlp_des_header = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_main.SuspendLayout();
            this.tlp_navbar.SuspendLayout();
            this.tlp_search.SuspendLayout();
            this.pnl_products_rem_descr.SuspendLayout();
            this.tlp_products_descr_main.SuspendLayout();
            this.tlp_product_des_reg.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.pnl_empty_msg.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlproductviewmore.SuspendLayout();
            this.tlp_viewmore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_viewmore)).BeginInit();
            this.tlp_header_viewmore.SuspendLayout();
            this.pnl_kitchenorderBill.SuspendLayout();
            this.tlp_des_header.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_main
            // 
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.btn_viewmore_order, 0, 3);
            this.tlp_main.Controls.Add(this.tlp_navbar, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_search, 0, 1);
            this.tlp_main.Controls.Add(this.flp_order_view, 0, 2);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 4;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            this.tlp_main.Size = new System.Drawing.Size(797, 533);
            this.tlp_main.TabIndex = 0;
            // 
            // btn_viewmore_order
            // 
            this.btn_viewmore_order.BackgroundImage = global::FoodandBeverage.Properties.Resources.Displaymore_kitchenorder;
            this.btn_viewmore_order.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_viewmore_order.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_viewmore_order.FlatAppearance.BorderSize = 0;
            this.btn_viewmore_order.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_viewmore_order.Location = new System.Drawing.Point(660, 505);
            this.btn_viewmore_order.Name = "btn_viewmore_order";
            this.btn_viewmore_order.Size = new System.Drawing.Size(134, 25);
            this.btn_viewmore_order.TabIndex = 6;
            this.btn_viewmore_order.UseVisualStyleBackColor = true;
            this.btn_viewmore_order.Click += new System.EventHandler(this.btn_viewmore_order_Click);
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
            this.tlp_navbar.Controls.Add(this.label2, 0, 0);
            this.tlp_navbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_navbar.Location = new System.Drawing.Point(3, 3);
            this.tlp_navbar.Name = "tlp_navbar";
            this.tlp_navbar.RowCount = 1;
            this.tlp_navbar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.Size = new System.Drawing.Size(791, 31);
            this.tlp_navbar.TabIndex = 18;
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(732, 3);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(25, 25);
            this.btnmini.TabIndex = 9;
            this.btnmini.Text = "-";
            this.btnmini.UseVisualStyleBackColor = false;
            this.btnmini.Visible = false;
            this.btnmini.Click += new System.EventHandler(this.btnmini_Click);
            // 
            // btncolse
            // 
            this.btncolse.BackColor = System.Drawing.Color.White;
            this.btncolse.FlatAppearance.BorderSize = 0;
            this.btncolse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncolse.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncolse.ForeColor = System.Drawing.Color.Red;
            this.btncolse.Location = new System.Drawing.Point(763, 3);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(25, 25);
            this.btncolse.TabIndex = 10;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "Kitchen View ";
            // 
            // tlp_search
            // 
            this.tlp_search.ColumnCount = 7;
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 44.98715F));
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.01285F));
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142F));
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlp_search.Controls.Add(this.txttablecode, 0, 0);
            this.tlp_search.Controls.Add(this.txtorderid_search, 0, 0);
            this.tlp_search.Controls.Add(this.btn_refresh, 2, 0);
            this.tlp_search.Controls.Add(this.btnprintcount, 4, 0);
            this.tlp_search.Controls.Add(this.btntotalorders, 5, 0);
            this.tlp_search.Controls.Add(this.lblprintloading, 3, 0);
            this.tlp_search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_search.Location = new System.Drawing.Point(3, 40);
            this.tlp_search.Name = "tlp_search";
            this.tlp_search.RowCount = 1;
            this.tlp_search.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_search.Size = new System.Drawing.Size(791, 52);
            this.tlp_search.TabIndex = 20;
            // 
            // txttablecode
            // 
            this.txttablecode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txttablecode.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttablecode.ForeColor = System.Drawing.Color.DarkGray;
            this.txttablecode.Location = new System.Drawing.Point(172, 3);
            this.txttablecode.Multiline = true;
            this.txttablecode.Name = "txttablecode";
            this.txttablecode.Size = new System.Drawing.Size(200, 46);
            this.txttablecode.TabIndex = 2;
            this.txttablecode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txttablecode.Enter += new System.EventHandler(this.txttablecode_Enter);
            this.txttablecode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txttablecode_KeyDown);
            this.txttablecode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txttablecode_KeyPress);
            this.txttablecode.Leave += new System.EventHandler(this.txttablecode_Leave);
            // 
            // txtorderid_search
            // 
            this.txtorderid_search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtorderid_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtorderid_search.ForeColor = System.Drawing.Color.DarkGray;
            this.txtorderid_search.Location = new System.Drawing.Point(3, 3);
            this.txtorderid_search.Multiline = true;
            this.txtorderid_search.Name = "txtorderid_search";
            this.txtorderid_search.Size = new System.Drawing.Size(163, 46);
            this.txtorderid_search.TabIndex = 1;
            this.txtorderid_search.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtorderid_search.Enter += new System.EventHandler(this.txtorderid_search_Enter);
            this.txtorderid_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtorderid_search_KeyDown);
            this.txtorderid_search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtorderid_search_KeyPress);
            this.txtorderid_search.Leave += new System.EventHandler(this.txtorderid_search_Leave);
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_refresh.BackgroundImage")));
            this.btn_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_refresh.FlatAppearance.BorderSize = 0;
            this.btn_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_refresh.Location = new System.Drawing.Point(378, 3);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(50, 46);
            this.btn_refresh.TabIndex = 3;
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // btnprintcount
            // 
            this.btnprintcount.BackgroundImage = global::FoodandBeverage.Properties.Resources.kitchenprintcounticon;
            this.btnprintcount.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnprintcount.FlatAppearance.BorderSize = 0;
            this.btnprintcount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprintcount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnprintcount.Location = new System.Drawing.Point(592, 3);
            this.btnprintcount.Name = "btnprintcount";
            this.btnprintcount.Size = new System.Drawing.Size(53, 46);
            this.btnprintcount.TabIndex = 5;
            this.btnprintcount.Text = "0";
            this.btnprintcount.UseVisualStyleBackColor = true;
            this.btnprintcount.Click += new System.EventHandler(this.btnprintcount_Click);
            // 
            // btntotalorders
            // 
            this.btntotalorders.BackgroundImage = global::FoodandBeverage.Properties.Resources.kitchentotalodericon;
            this.btntotalorders.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btntotalorders.FlatAppearance.BorderSize = 0;
            this.btntotalorders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btntotalorders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btntotalorders.Location = new System.Drawing.Point(657, 3);
            this.btntotalorders.Name = "btntotalorders";
            this.btntotalorders.Size = new System.Drawing.Size(58, 46);
            this.btntotalorders.TabIndex = 6;
            this.btntotalorders.Text = "0";
            this.btntotalorders.UseVisualStyleBackColor = true;
            // 
            // lblprintloading
            // 
            this.lblprintloading.AutoSize = true;
            this.lblprintloading.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblprintloading.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblprintloading.ForeColor = System.Drawing.Color.Red;
            this.lblprintloading.Location = new System.Drawing.Point(450, 0);
            this.lblprintloading.Name = "lblprintloading";
            this.lblprintloading.Size = new System.Drawing.Size(136, 52);
            this.lblprintloading.TabIndex = 7;
            this.lblprintloading.Tag = "Printing.....";
            this.lblprintloading.Text = "Printing.....";
            this.lblprintloading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblprintloading.Visible = false;
            // 
            // flp_order_view
            // 
            this.flp_order_view.AutoScroll = true;
            this.flp_order_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_order_view.Location = new System.Drawing.Point(3, 98);
            this.flp_order_view.Name = "flp_order_view";
            this.flp_order_view.Size = new System.Drawing.Size(791, 401);
            this.flp_order_view.TabIndex = 21;
            this.flp_order_view.Scroll += new System.Windows.Forms.ScrollEventHandler(this.flp_order_view_Scroll);
            // 
            // pnl_products_rem_descr
            // 
            this.pnl_products_rem_descr.Controls.Add(this.tlp_products_descr_main);
            this.pnl_products_rem_descr.Location = new System.Drawing.Point(99, 46);
            this.pnl_products_rem_descr.Name = "pnl_products_rem_descr";
            this.pnl_products_rem_descr.Padding = new System.Windows.Forms.Padding(3);
            this.pnl_products_rem_descr.Size = new System.Drawing.Size(401, 185);
            this.pnl_products_rem_descr.TabIndex = 4;
            this.pnl_products_rem_descr.Visible = false;
            this.pnl_products_rem_descr.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_products_rem_descr_Paint);
            // 
            // tlp_products_descr_main
            // 
            this.tlp_products_descr_main.BackColor = System.Drawing.Color.Silver;
            this.tlp_products_descr_main.ColumnCount = 1;
            this.tlp_products_descr_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_products_descr_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_products_descr_main.Controls.Add(this.tlp_product_des_reg, 0, 1);
            this.tlp_products_descr_main.Controls.Add(this.tableLayoutPanel5, 0, 2);
            this.tlp_products_descr_main.Controls.Add(this.tlp_des_header, 0, 0);
            this.tlp_products_descr_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_products_descr_main.Location = new System.Drawing.Point(3, 3);
            this.tlp_products_descr_main.Name = "tlp_products_descr_main";
            this.tlp_products_descr_main.RowCount = 3;
            this.tlp_products_descr_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.17647F));
            this.tlp_products_descr_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 57.05882F));
            this.tlp_products_descr_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.35294F));
            this.tlp_products_descr_main.Size = new System.Drawing.Size(395, 179);
            this.tlp_products_descr_main.TabIndex = 0;
            // 
            // tlp_product_des_reg
            // 
            this.tlp_product_des_reg.BackColor = System.Drawing.Color.Silver;
            this.tlp_product_des_reg.ColumnCount = 1;
            this.tlp_product_des_reg.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_product_des_reg.Controls.Add(this.txt_product_remove_des, 0, 0);
            this.tlp_product_des_reg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_product_des_reg.Location = new System.Drawing.Point(3, 40);
            this.tlp_product_des_reg.Name = "tlp_product_des_reg";
            this.tlp_product_des_reg.RowCount = 1;
            this.tlp_product_des_reg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.88889F));
            this.tlp_product_des_reg.Size = new System.Drawing.Size(389, 95);
            this.tlp_product_des_reg.TabIndex = 0;
            // 
            // txt_product_remove_des
            // 
            this.txt_product_remove_des.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_product_remove_des.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_product_remove_des.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_product_remove_des.Location = new System.Drawing.Point(3, 3);
            this.txt_product_remove_des.Multiline = true;
            this.txt_product_remove_des.Name = "txt_product_remove_des";
            this.txt_product_remove_des.Size = new System.Drawing.Size(383, 89);
            this.txt_product_remove_des.TabIndex = 0;
            this.txt_product_remove_des.Enter += new System.EventHandler(this.txt_product_remove_des_Enter);
            this.txt_product_remove_des.Leave += new System.EventHandler(this.txt_product_remove_des_Leave);
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.btn_cancel_product_des, 1, 0);
            this.tableLayoutPanel5.Controls.Add(this.btn_rem_product_des, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 141);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(389, 35);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // btn_cancel_product_des
            // 
            this.btn_cancel_product_des.BackgroundImage = global::FoodandBeverage.Properties.Resources.cancel_description;
            this.btn_cancel_product_des.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_cancel_product_des.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_cancel_product_des.FlatAppearance.BorderSize = 0;
            this.btn_cancel_product_des.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel_product_des.Location = new System.Drawing.Point(197, 3);
            this.btn_cancel_product_des.Name = "btn_cancel_product_des";
            this.btn_cancel_product_des.Size = new System.Drawing.Size(189, 29);
            this.btn_cancel_product_des.TabIndex = 2;
            this.btn_cancel_product_des.UseVisualStyleBackColor = true;
            this.btn_cancel_product_des.Click += new System.EventHandler(this.btn_cancel_product_des_Click);
            // 
            // btn_rem_product_des
            // 
            this.btn_rem_product_des.BackgroundImage = global::FoodandBeverage.Properties.Resources.okay_description;
            this.btn_rem_product_des.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_rem_product_des.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_rem_product_des.FlatAppearance.BorderSize = 0;
            this.btn_rem_product_des.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_rem_product_des.Location = new System.Drawing.Point(3, 3);
            this.btn_rem_product_des.Name = "btn_rem_product_des";
            this.btn_rem_product_des.Size = new System.Drawing.Size(188, 29);
            this.btn_rem_product_des.TabIndex = 1;
            this.btn_rem_product_des.UseVisualStyleBackColor = true;
            this.btn_rem_product_des.Click += new System.EventHandler(this.btn_rem_product_des_Click);
            // 
            // btn_close_prod_des
            // 
            this.btn_close_prod_des.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_close_prod_des.BackgroundImage")));
            this.btn_close_prod_des.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_close_prod_des.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_close_prod_des.FlatAppearance.BorderSize = 0;
            this.btn_close_prod_des.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close_prod_des.Location = new System.Drawing.Point(333, 3);
            this.btn_close_prod_des.Name = "btn_close_prod_des";
            this.btn_close_prod_des.Size = new System.Drawing.Size(53, 25);
            this.btn_close_prod_des.TabIndex = 1;
            this.btn_close_prod_des.UseVisualStyleBackColor = true;
            this.btn_close_prod_des.Click += new System.EventHandler(this.btn_close_prod_des_Click);
            // 
            // lblheader_product_des
            // 
            this.lblheader_product_des.AutoSize = true;
            this.lblheader_product_des.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblheader_product_des.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblheader_product_des.Location = new System.Drawing.Point(3, 0);
            this.lblheader_product_des.Name = "lblheader_product_des";
            this.lblheader_product_des.Size = new System.Drawing.Size(324, 31);
            this.lblheader_product_des.TabIndex = 2;
            this.lblheader_product_des.Text = "Product Name";
            this.lblheader_product_des.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblmsg
            // 
            this.lblmsg.AutoSize = true;
            this.lblmsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmsg.ForeColor = System.Drawing.Color.Red;
            this.lblmsg.Location = new System.Drawing.Point(25, 27);
            this.lblmsg.Name = "lblmsg";
            this.lblmsg.Size = new System.Drawing.Size(196, 37);
            this.lblmsg.TabIndex = 0;
            this.lblmsg.Text = "No Records";
            this.lblmsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pnl_empty_msg
            // 
            this.pnl_empty_msg.Controls.Add(this.pictureBox1);
            this.pnl_empty_msg.Controls.Add(this.lblmsg);
            this.pnl_empty_msg.Location = new System.Drawing.Point(0, 160);
            this.pnl_empty_msg.Name = "pnl_empty_msg";
            this.pnl_empty_msg.Size = new System.Drawing.Size(363, 89);
            this.pnl_empty_msg.TabIndex = 0;
            this.pnl_empty_msg.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(227, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(74, 53);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // pnlproductviewmore
            // 
            this.pnlproductviewmore.Controls.Add(this.tlp_viewmore);
            this.pnlproductviewmore.Location = new System.Drawing.Point(42, 228);
            this.pnlproductviewmore.Name = "pnlproductviewmore";
            this.pnlproductviewmore.Padding = new System.Windows.Forms.Padding(3);
            this.pnlproductviewmore.Size = new System.Drawing.Size(683, 378);
            this.pnlproductviewmore.TabIndex = 5;
            this.pnlproductviewmore.Visible = false;
            // 
            // tlp_viewmore
            // 
            this.tlp_viewmore.BackColor = System.Drawing.Color.SteelBlue;
            this.tlp_viewmore.ColumnCount = 3;
            this.tlp_viewmore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlp_viewmore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 98.06451F));
            this.tlp_viewmore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.935484F));
            this.tlp_viewmore.Controls.Add(this.dgv_viewmore, 1, 1);
            this.tlp_viewmore.Controls.Add(this.tlp_header_viewmore, 1, 0);
            this.tlp_viewmore.Controls.Add(this.label9, 1, 2);
            this.tlp_viewmore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_viewmore.Location = new System.Drawing.Point(3, 3);
            this.tlp_viewmore.Name = "tlp_viewmore";
            this.tlp_viewmore.RowCount = 3;
            this.tlp_viewmore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.76471F));
            this.tlp_viewmore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.23529F));
            this.tlp_viewmore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_viewmore.Size = new System.Drawing.Size(677, 372);
            this.tlp_viewmore.TabIndex = 0;
            // 
            // dgv_viewmore
            // 
            this.dgv_viewmore.AllowUserToAddRows = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_viewmore.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_viewmore.ColumnHeadersHeight = 40;
            this.dgv_viewmore.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_viewmore.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.vslno,
            this.vproductname,
            this.vqty,
            this.add_Ordermaster,
            this.add_koid,
            this.addorderdetailsid,
            this.addaction,
            this.addavid,
            this.addpavid,
            this.addaddonstatus,
            this.addonprice,
            this.addorderno,
            this.CCode});
            this.dgv_viewmore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_viewmore.Location = new System.Drawing.Point(11, 44);
            this.dgv_viewmore.Name = "dgv_viewmore";
            this.dgv_viewmore.RowHeadersVisible = false;
            this.dgv_viewmore.RowHeadersWidth = 50;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_viewmore.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgv_viewmore.RowTemplate.Height = 35;
            this.dgv_viewmore.Size = new System.Drawing.Size(650, 304);
            this.dgv_viewmore.TabIndex = 2;
            this.dgv_viewmore.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_viewmore_CellDoubleClick);
            // 
            // vslno
            // 
            this.vslno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.vslno.HeaderText = "Slno";
            this.vslno.Name = "vslno";
            this.vslno.Width = 64;
            // 
            // vproductname
            // 
            this.vproductname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.vproductname.HeaderText = "Product";
            this.vproductname.Name = "vproductname";
            // 
            // vqty
            // 
            this.vqty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.vqty.HeaderText = "Quantitiy";
            this.vqty.Name = "vqty";
            this.vqty.Width = 93;
            // 
            // add_Ordermaster
            // 
            this.add_Ordermaster.HeaderText = "add_Ordermaster";
            this.add_Ordermaster.Name = "add_Ordermaster";
            this.add_Ordermaster.Visible = false;
            // 
            // add_koid
            // 
            this.add_koid.HeaderText = "add_koid";
            this.add_koid.Name = "add_koid";
            this.add_koid.Visible = false;
            // 
            // addorderdetailsid
            // 
            this.addorderdetailsid.HeaderText = "addorderdetailsid";
            this.addorderdetailsid.Name = "addorderdetailsid";
            this.addorderdetailsid.Visible = false;
            // 
            // addaction
            // 
            this.addaction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle6.NullValue")));
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(3);
            this.addaction.DefaultCellStyle = dataGridViewCellStyle6;
            this.addaction.HeaderText = "Action";
            this.addaction.Image = global::FoodandBeverage.Properties.Resources.Delete_Grid;
            this.addaction.Name = "addaction";
            this.addaction.Width = 57;
            // 
            // addavid
            // 
            this.addavid.HeaderText = "addavid";
            this.addavid.Name = "addavid";
            this.addavid.Visible = false;
            // 
            // addpavid
            // 
            this.addpavid.HeaderText = "addpavid";
            this.addpavid.Name = "addpavid";
            this.addpavid.Visible = false;
            // 
            // addaddonstatus
            // 
            this.addaddonstatus.HeaderText = "addaddonstatus";
            this.addaddonstatus.Name = "addaddonstatus";
            this.addaddonstatus.Visible = false;
            // 
            // addonprice
            // 
            this.addonprice.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.addonprice.HeaderText = "addonprice";
            this.addonprice.Name = "addonprice";
            this.addonprice.Width = 112;
            // 
            // addorderno
            // 
            this.addorderno.HeaderText = "addorderno";
            this.addorderno.Name = "addorderno";
            this.addorderno.Visible = false;
            // 
            // CCode
            // 
            this.CCode.HeaderText = "Column1";
            this.CCode.Name = "CCode";
            this.CCode.Visible = false;
            // 
            // tlp_header_viewmore
            // 
            this.tlp_header_viewmore.ColumnCount = 3;
            this.tlp_header_viewmore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.55914F));
            this.tlp_header_viewmore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.19355F));
            this.tlp_header_viewmore.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.032258F));
            this.tlp_header_viewmore.Controls.Add(this.btn_close_viewmore, 2, 0);
            this.tlp_header_viewmore.Controls.Add(this.lblviewmore, 0, 0);
            this.tlp_header_viewmore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_header_viewmore.Location = new System.Drawing.Point(11, 3);
            this.tlp_header_viewmore.Name = "tlp_header_viewmore";
            this.tlp_header_viewmore.RowCount = 1;
            this.tlp_header_viewmore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_header_viewmore.Size = new System.Drawing.Size(650, 35);
            this.tlp_header_viewmore.TabIndex = 4;
            // 
            // btn_close_viewmore
            // 
            this.btn_close_viewmore.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_close_viewmore.BackgroundImage")));
            this.btn_close_viewmore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_close_viewmore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_close_viewmore.FlatAppearance.BorderSize = 0;
            this.btn_close_viewmore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close_viewmore.Location = new System.Drawing.Point(593, 3);
            this.btn_close_viewmore.Name = "btn_close_viewmore";
            this.btn_close_viewmore.Size = new System.Drawing.Size(54, 29);
            this.btn_close_viewmore.TabIndex = 1;
            this.btn_close_viewmore.UseVisualStyleBackColor = true;
            this.btn_close_viewmore.Click += new System.EventHandler(this.btn_close_viewmore_Click);
            // 
            // lblviewmore
            // 
            this.lblviewmore.AutoSize = true;
            this.lblviewmore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblviewmore.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblviewmore.ForeColor = System.Drawing.Color.White;
            this.lblviewmore.Location = new System.Drawing.Point(3, 0);
            this.lblviewmore.Name = "lblviewmore";
            this.lblviewmore.Size = new System.Drawing.Size(492, 35);
            this.lblviewmore.TabIndex = 3;
            this.lblviewmore.Text = "label9";
            this.lblviewmore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(11, 351);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 16);
            this.label9.TabIndex = 5;
            this.label9.Text = "ESC Close";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnl_kitchenorderBill
            // 
            this.pnl_kitchenorderBill.Controls.Add(this.crvkitchenbill);
            this.pnl_kitchenorderBill.Location = new System.Drawing.Point(243, 121);
            this.pnl_kitchenorderBill.Name = "pnl_kitchenorderBill";
            this.pnl_kitchenorderBill.Padding = new System.Windows.Forms.Padding(10);
            this.pnl_kitchenorderBill.Size = new System.Drawing.Size(417, 477);
            this.pnl_kitchenorderBill.TabIndex = 0;
            this.pnl_kitchenorderBill.Visible = false;
            // 
            // crvkitchenbill
            // 
            this.crvkitchenbill.ActiveViewIndex = -1;
            this.crvkitchenbill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvkitchenbill.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvkitchenbill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvkitchenbill.Location = new System.Drawing.Point(10, 10);
            this.crvkitchenbill.Name = "crvkitchenbill";
            this.crvkitchenbill.ShowCloseButton = false;
            this.crvkitchenbill.ShowCopyButton = false;
            this.crvkitchenbill.ShowGotoPageButton = false;
            this.crvkitchenbill.ShowGroupTreeButton = false;
            this.crvkitchenbill.ShowLogo = false;
            this.crvkitchenbill.ShowPageNavigateButtons = false;
            this.crvkitchenbill.ShowParameterPanelButton = false;
            this.crvkitchenbill.ShowTextSearchButton = false;
            this.crvkitchenbill.ShowZoomButton = false;
            this.crvkitchenbill.Size = new System.Drawing.Size(397, 457);
            this.crvkitchenbill.TabIndex = 0;
            this.crvkitchenbill.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(62, 129);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(312, 47);
            this.textBox1.TabIndex = 1;
            this.textBox1.Visible = false;
            // 
            // blinkTimer
            // 
            this.blinkTimer.Interval = 500;
            this.blinkTimer.Tick += new System.EventHandler(this.blinkTimer_Tick);
            // 
            // refreshtimer
            // 
            this.refreshtimer.Enabled = true;
            this.refreshtimer.Interval = 2000;
            this.refreshtimer.Tick += new System.EventHandler(this.refreshtimer_Tick);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle8.NullValue")));
            dataGridViewCellStyle8.Padding = new System.Windows.Forms.Padding(5);
            this.dataGridViewImageColumn1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewImageColumn1.HeaderText = "Action";
            this.dataGridViewImageColumn1.Image = global::FoodandBeverage.Properties.Resources.Delete_Grid;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // tlp_des_header
            // 
            this.tlp_des_header.ColumnCount = 2;
            this.tlp_des_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tlp_des_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tlp_des_header.Controls.Add(this.lblheader_product_des, 0, 0);
            this.tlp_des_header.Controls.Add(this.btn_close_prod_des, 1, 0);
            this.tlp_des_header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_des_header.Location = new System.Drawing.Point(3, 3);
            this.tlp_des_header.Name = "tlp_des_header";
            this.tlp_des_header.RowCount = 1;
            this.tlp_des_header.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_des_header.Size = new System.Drawing.Size(389, 31);
            this.tlp_des_header.TabIndex = 2;
            // 
            // FrmKitchenView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 533);
            this.Controls.Add(this.pnlproductviewmore);
            this.Controls.Add(this.pnl_products_rem_descr);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pnl_kitchenorderBill);
            this.Controls.Add(this.pnl_empty_msg);
            this.Controls.Add(this.tlp_main);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmKitchenView";
            this.Text = "FrmKitchenView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmKitchenView_FormClosing);
            this.Load += new System.EventHandler(this.FrmKitchenView_Load);
            this.tlp_main.ResumeLayout(false);
            this.tlp_navbar.ResumeLayout(false);
            this.tlp_navbar.PerformLayout();
            this.tlp_search.ResumeLayout(false);
            this.tlp_search.PerformLayout();
            this.pnl_products_rem_descr.ResumeLayout(false);
            this.tlp_products_descr_main.ResumeLayout(false);
            this.tlp_product_des_reg.ResumeLayout(false);
            this.tlp_product_des_reg.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.pnl_empty_msg.ResumeLayout(false);
            this.pnl_empty_msg.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlproductviewmore.ResumeLayout(false);
            this.tlp_viewmore.ResumeLayout(false);
            this.tlp_viewmore.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_viewmore)).EndInit();
            this.tlp_header_viewmore.ResumeLayout(false);
            this.tlp_header_viewmore.PerformLayout();
            this.pnl_kitchenorderBill.ResumeLayout(false);
            this.tlp_des_header.ResumeLayout(false);
            this.tlp_des_header.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TableLayoutPanel tlp_navbar;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tlp_search;
        private System.Windows.Forms.TextBox txtorderid_search;
        private System.Windows.Forms.FlowLayoutPanel flp_order_view;
        private System.Windows.Forms.Panel pnl_products_rem_descr;
        private System.Windows.Forms.TableLayoutPanel tlp_products_descr_main;
        private System.Windows.Forms.TableLayoutPanel tlp_product_des_reg;
        private System.Windows.Forms.TextBox txt_product_remove_des;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Button btn_cancel_product_des;
        private System.Windows.Forms.Button btn_rem_product_des;
        private System.Windows.Forms.Button btn_close_prod_des;
        private System.Windows.Forms.Label lblheader_product_des;
        private System.Windows.Forms.Label lblmsg;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnl_empty_msg;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txttablecode;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Panel pnlproductviewmore;
        private System.Windows.Forms.TableLayoutPanel tlp_viewmore;
        private System.Windows.Forms.DataGridView dgv_viewmore;
        private System.Windows.Forms.TableLayoutPanel tlp_header_viewmore;
        private System.Windows.Forms.Button btn_close_viewmore;
        private System.Windows.Forms.Label lblviewmore;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel pnl_kitchenorderBill;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvkitchenbill;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_viewmore_order;
        private System.Windows.Forms.Button btnprintcount;
        private System.Windows.Forms.Button btntotalorders;
        private System.Windows.Forms.Timer blinkTimer;
        private System.Windows.Forms.Label lblprintloading;
        public System.Windows.Forms.Timer refreshtimer;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn vslno;
        private System.Windows.Forms.DataGridViewTextBoxColumn vproductname;
        private System.Windows.Forms.DataGridViewTextBoxColumn vqty;
        private System.Windows.Forms.DataGridViewTextBoxColumn add_Ordermaster;
        private System.Windows.Forms.DataGridViewTextBoxColumn add_koid;
        private System.Windows.Forms.DataGridViewTextBoxColumn addorderdetailsid;
        private System.Windows.Forms.DataGridViewImageColumn addaction;
        private System.Windows.Forms.DataGridViewTextBoxColumn addavid;
        private System.Windows.Forms.DataGridViewTextBoxColumn addpavid;
        private System.Windows.Forms.DataGridViewTextBoxColumn addaddonstatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn addonprice;
        private System.Windows.Forms.DataGridViewTextBoxColumn addorderno;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCode;
        private System.Windows.Forms.TableLayoutPanel tlp_des_header;
    }
}