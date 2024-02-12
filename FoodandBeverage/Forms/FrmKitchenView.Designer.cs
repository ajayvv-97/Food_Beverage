
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_navbar = new System.Windows.Forms.TableLayoutPanel();
            this.btnmini = new System.Windows.Forms.Button();
            this.btncolse = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tlp_search = new System.Windows.Forms.TableLayoutPanel();
            this.txttablecode = new System.Windows.Forms.TextBox();
            this.txtorderid_search = new System.Windows.Forms.TextBox();
            this.btn_refresh = new System.Windows.Forms.Button();
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
            this.tlp_header_viewmore = new System.Windows.Forms.TableLayoutPanel();
            this.btn_close_viewmore = new System.Windows.Forms.Button();
            this.lblviewmore = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.pnl_kitchenorderBill = new System.Windows.Forms.Panel();
            this.crvkitchenbill = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
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
            this.SuspendLayout();
            // 
            // tlp_main
            // 
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tlp_navbar, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_search, 0, 1);
            this.tlp_main.Controls.Add(this.flp_order_view, 0, 2);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 3;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.Size = new System.Drawing.Size(797, 489);
            this.tlp_main.TabIndex = 0;
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
            this.tlp_search.ColumnCount = 5;
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tlp_search.Controls.Add(this.txttablecode, 0, 0);
            this.tlp_search.Controls.Add(this.txtorderid_search, 0, 0);
            this.tlp_search.Controls.Add(this.btn_refresh, 3, 0);
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
            this.txttablecode.Location = new System.Drawing.Point(310, 3);
            this.txttablecode.Multiline = true;
            this.txttablecode.Name = "txttablecode";
            this.txttablecode.Size = new System.Drawing.Size(301, 46);
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
            this.txtorderid_search.Size = new System.Drawing.Size(301, 46);
            this.txtorderid_search.TabIndex = 1;
            this.txtorderid_search.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtorderid_search.Enter += new System.EventHandler(this.txtorderid_search_Enter);
            this.txtorderid_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtorderid_search_KeyDown);
            this.txtorderid_search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtorderid_search_KeyPress);
            this.txtorderid_search.Leave += new System.EventHandler(this.txtorderid_search_Leave);
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackgroundImage = global::FoodandBeverage.Properties.Resources.refresh_button;
            this.btn_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_refresh.FlatAppearance.BorderSize = 0;
            this.btn_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_refresh.Location = new System.Drawing.Point(662, 3);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(64, 46);
            this.btn_refresh.TabIndex = 3;
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // flp_order_view
            // 
            this.flp_order_view.AutoScroll = true;
            this.flp_order_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_order_view.Location = new System.Drawing.Point(3, 98);
            this.flp_order_view.Name = "flp_order_view";
            this.flp_order_view.Size = new System.Drawing.Size(791, 404);
            this.flp_order_view.TabIndex = 21;
            // 
            // pnl_products_rem_descr
            // 
            this.pnl_products_rem_descr.Controls.Add(this.tlp_products_descr_main);
            this.pnl_products_rem_descr.Location = new System.Drawing.Point(99, 98);
            this.pnl_products_rem_descr.Name = "pnl_products_rem_descr";
            this.pnl_products_rem_descr.Size = new System.Drawing.Size(304, 155);
            this.pnl_products_rem_descr.TabIndex = 4;
            this.pnl_products_rem_descr.Visible = false;
            this.pnl_products_rem_descr.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_products_rem_descr_Paint);
            // 
            // tlp_products_descr_main
            // 
            this.tlp_products_descr_main.BackColor = System.Drawing.Color.Silver;
            this.tlp_products_descr_main.ColumnCount = 3;
            this.tlp_products_descr_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.791878F));
            this.tlp_products_descr_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85.85526F));
            this.tlp_products_descr_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.51316F));
            this.tlp_products_descr_main.Controls.Add(this.tlp_product_des_reg, 1, 1);
            this.tlp_products_descr_main.Controls.Add(this.btn_close_prod_des, 2, 0);
            this.tlp_products_descr_main.Controls.Add(this.lblheader_product_des, 1, 0);
            this.tlp_products_descr_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_products_descr_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_products_descr_main.Name = "tlp_products_descr_main";
            this.tlp_products_descr_main.RowCount = 3;
            this.tlp_products_descr_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 21.93548F));
            this.tlp_products_descr_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.90323F));
            this.tlp_products_descr_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.714063F));
            this.tlp_products_descr_main.Size = new System.Drawing.Size(304, 155);
            this.tlp_products_descr_main.TabIndex = 0;
            // 
            // tlp_product_des_reg
            // 
            this.tlp_product_des_reg.BackColor = System.Drawing.Color.Silver;
            this.tlp_product_des_reg.ColumnCount = 1;
            this.tlp_product_des_reg.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_product_des_reg.Controls.Add(this.txt_product_remove_des, 0, 0);
            this.tlp_product_des_reg.Controls.Add(this.tableLayoutPanel5, 0, 1);
            this.tlp_product_des_reg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_product_des_reg.Location = new System.Drawing.Point(11, 37);
            this.tlp_product_des_reg.Name = "tlp_product_des_reg";
            this.tlp_product_des_reg.RowCount = 2;
            this.tlp_product_des_reg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.88889F));
            this.tlp_product_des_reg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 36.11111F));
            this.tlp_product_des_reg.Size = new System.Drawing.Size(254, 107);
            this.tlp_product_des_reg.TabIndex = 0;
            // 
            // txt_product_remove_des
            // 
            this.txt_product_remove_des.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_product_remove_des.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_product_remove_des.Location = new System.Drawing.Point(3, 3);
            this.txt_product_remove_des.Multiline = true;
            this.txt_product_remove_des.Name = "txt_product_remove_des";
            this.txt_product_remove_des.Size = new System.Drawing.Size(248, 62);
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
            this.tableLayoutPanel5.Location = new System.Drawing.Point(3, 71);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(248, 33);
            this.tableLayoutPanel5.TabIndex = 1;
            // 
            // btn_cancel_product_des
            // 
            this.btn_cancel_product_des.Location = new System.Drawing.Point(127, 3);
            this.btn_cancel_product_des.Name = "btn_cancel_product_des";
            this.btn_cancel_product_des.Size = new System.Drawing.Size(75, 27);
            this.btn_cancel_product_des.TabIndex = 2;
            this.btn_cancel_product_des.Text = "Cancel";
            this.btn_cancel_product_des.UseVisualStyleBackColor = true;
            this.btn_cancel_product_des.Click += new System.EventHandler(this.btn_cancel_product_des_Click);
            // 
            // btn_rem_product_des
            // 
            this.btn_rem_product_des.Location = new System.Drawing.Point(3, 3);
            this.btn_rem_product_des.Name = "btn_rem_product_des";
            this.btn_rem_product_des.Size = new System.Drawing.Size(75, 27);
            this.btn_rem_product_des.TabIndex = 1;
            this.btn_rem_product_des.Text = "ok";
            this.btn_rem_product_des.UseVisualStyleBackColor = true;
            this.btn_rem_product_des.Click += new System.EventHandler(this.btn_rem_product_des_Click);
            // 
            // btn_close_prod_des
            // 
            this.btn_close_prod_des.BackgroundImage = global::FoodandBeverage.Properties.Resources.close_price_change;
            this.btn_close_prod_des.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_close_prod_des.FlatAppearance.BorderSize = 0;
            this.btn_close_prod_des.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close_prod_des.Location = new System.Drawing.Point(271, 3);
            this.btn_close_prod_des.Name = "btn_close_prod_des";
            this.btn_close_prod_des.Size = new System.Drawing.Size(30, 28);
            this.btn_close_prod_des.TabIndex = 1;
            this.btn_close_prod_des.UseVisualStyleBackColor = true;
            this.btn_close_prod_des.Click += new System.EventHandler(this.btn_close_prod_des_Click);
            // 
            // lblheader_product_des
            // 
            this.lblheader_product_des.AutoSize = true;
            this.lblheader_product_des.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblheader_product_des.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblheader_product_des.Location = new System.Drawing.Point(11, 0);
            this.lblheader_product_des.Name = "lblheader_product_des";
            this.lblheader_product_des.Size = new System.Drawing.Size(254, 34);
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
            this.pictureBox1.BackgroundImage = global::FoodandBeverage.Properties.Resources.empty_orders;
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
            this.pnlproductviewmore.Location = new System.Drawing.Point(125, 87);
            this.pnlproductviewmore.Name = "pnlproductviewmore";
            this.pnlproductviewmore.Size = new System.Drawing.Size(489, 310);
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
            this.tlp_viewmore.Location = new System.Drawing.Point(0, 0);
            this.tlp_viewmore.Name = "tlp_viewmore";
            this.tlp_viewmore.RowCount = 3;
            this.tlp_viewmore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.76471F));
            this.tlp_viewmore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.23529F));
            this.tlp_viewmore.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_viewmore.Size = new System.Drawing.Size(489, 310);
            this.tlp_viewmore.TabIndex = 0;
            // 
            // dgv_viewmore
            // 
            this.dgv_viewmore.AllowUserToAddRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_viewmore.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_viewmore.ColumnHeadersHeight = 40;
            this.dgv_viewmore.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_viewmore.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.vslno,
            this.vproductname,
            this.vqty});
            this.dgv_viewmore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_viewmore.Location = new System.Drawing.Point(11, 37);
            this.dgv_viewmore.Name = "dgv_viewmore";
            this.dgv_viewmore.RowHeadersVisible = false;
            this.dgv_viewmore.RowHeadersWidth = 50;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_viewmore.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_viewmore.RowTemplate.Height = 30;
            this.dgv_viewmore.Size = new System.Drawing.Size(465, 249);
            this.dgv_viewmore.TabIndex = 2;
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
            this.tlp_header_viewmore.Size = new System.Drawing.Size(465, 28);
            this.tlp_header_viewmore.TabIndex = 4;
            // 
            // btn_close_viewmore
            // 
            this.btn_close_viewmore.BackgroundImage = global::FoodandBeverage.Properties.Resources.close_price_change;
            this.btn_close_viewmore.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_close_viewmore.FlatAppearance.BorderSize = 0;
            this.btn_close_viewmore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close_viewmore.Location = new System.Drawing.Point(425, 3);
            this.btn_close_viewmore.Name = "btn_close_viewmore";
            this.btn_close_viewmore.Size = new System.Drawing.Size(33, 22);
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
            this.lblviewmore.Size = new System.Drawing.Size(350, 28);
            this.lblviewmore.TabIndex = 3;
            this.lblviewmore.Text = "label9";
            this.lblviewmore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(11, 289);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 16);
            this.label9.TabIndex = 5;
            this.label9.Text = "ESC Close";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnl_kitchenorderBill
            // 
            this.pnl_kitchenorderBill.Controls.Add(this.crvkitchenbill);
            this.pnl_kitchenorderBill.Location = new System.Drawing.Point(33, 0);
            this.pnl_kitchenorderBill.Name = "pnl_kitchenorderBill";
            this.pnl_kitchenorderBill.Size = new System.Drawing.Size(364, 419);
            this.pnl_kitchenorderBill.TabIndex = 0;
            this.pnl_kitchenorderBill.Visible = false;
            // 
            // crvkitchenbill
            // 
            this.crvkitchenbill.ActiveViewIndex = -1;
            this.crvkitchenbill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvkitchenbill.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvkitchenbill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvkitchenbill.Location = new System.Drawing.Point(0, 0);
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
            this.crvkitchenbill.Size = new System.Drawing.Size(364, 419);
            this.crvkitchenbill.TabIndex = 0;
            this.crvkitchenbill.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // FrmKitchenView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 489);
            this.Controls.Add(this.pnl_kitchenorderBill);
            this.Controls.Add(this.pnlproductviewmore);
            this.Controls.Add(this.pnl_empty_msg);
            this.Controls.Add(this.pnl_products_rem_descr);
            this.Controls.Add(this.tlp_main);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmKitchenView";
            this.Text = "FrmKitchenView";
            this.Load += new System.EventHandler(this.FrmKitchenView_Load);
            this.tlp_main.ResumeLayout(false);
            this.tlp_navbar.ResumeLayout(false);
            this.tlp_navbar.PerformLayout();
            this.tlp_search.ResumeLayout(false);
            this.tlp_search.PerformLayout();
            this.pnl_products_rem_descr.ResumeLayout(false);
            this.tlp_products_descr_main.ResumeLayout(false);
            this.tlp_products_descr_main.PerformLayout();
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
            this.ResumeLayout(false);

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
        private System.Windows.Forms.DataGridViewTextBoxColumn vslno;
        private System.Windows.Forms.DataGridViewTextBoxColumn vproductname;
        private System.Windows.Forms.DataGridViewTextBoxColumn vqty;
        private System.Windows.Forms.Panel pnl_kitchenorderBill;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvkitchenbill;
    }
}