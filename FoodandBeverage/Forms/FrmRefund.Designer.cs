
namespace FoodandBeverage.Forms
{
    partial class FrmRefund
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRefund));
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.pnl_cus_cash_details = new System.Windows.Forms.Panel();
            this.tlp_custom = new System.Windows.Forms.TableLayoutPanel();
            this.lblFMCardType = new System.Windows.Forms.Label();
            this.lblCardType = new System.Windows.Forms.Label();
            this.lblcbalance = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblrefunbalance = new System.Windows.Forms.Label();
            this.lblcardno = new System.Windows.Forms.Label();
            this.tlp_cashrefund = new System.Windows.Forms.TableLayoutPanel();
            this.lblcashrefundamt = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tlp_navbar = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.btnmini = new System.Windows.Forms.Button();
            this.btncolse = new System.Windows.Forms.Button();
            this.tlp_button = new System.Windows.Forms.TableLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnrefund = new System.Windows.Forms.Button();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.dgv_productlist = new System.Windows.Forms.DataGridView();
            this.no = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Usertype = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Quantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnsearchbill = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btnrefund_cash_point = new System.Windows.Forms.Button();
            this.label55 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.cmbRefundPayType = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtRenote = new System.Windows.Forms.TextBox();
            this.cmbRefundReson = new System.Windows.Forms.ComboBox();
            this.tlp_token_net_pay = new System.Windows.Forms.TableLayoutPanel();
            this.lbltoken = new System.Windows.Forms.Label();
            this.lblpaymenttype = new System.Windows.Forms.Label();
            this.lblnetamount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtsalemasterid = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtordermasterid = new System.Windows.Forms.TextBox();
            this.txtledgerid = new System.Windows.Forms.TextBox();
            this.wificard_timer = new System.Windows.Forms.Timer(this.components);
            this.pnlwifi = new System.Windows.Forms.Panel();
            this.tlp_wifiPopup = new System.Windows.Forms.TableLayoutPanel();
            this.pbwifi = new System.Windows.Forms.PictureBox();
            this.btnwificancel = new System.Windows.Forms.Button();
            this.lblorderstatus = new System.Windows.Forms.Label();
            this.lblDebitLedgerId = new System.Windows.Forms.Label();
            this.tlp_main.SuspendLayout();
            this.pnl_cus_cash_details.SuspendLayout();
            this.tlp_custom.SuspendLayout();
            this.tlp_cashrefund.SuspendLayout();
            this.tlp_navbar.SuspendLayout();
            this.tlp_button.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_productlist)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tlp_token_net_pay.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlwifi.SuspendLayout();
            this.tlp_wifiPopup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbwifi)).BeginInit();
            this.SuspendLayout();
            // 
            // tlp_main
            // 
            this.tlp_main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.pnl_cus_cash_details, 0, 4);
            this.tlp_main.Controls.Add(this.tlp_navbar, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_button, 0, 6);
            this.tlp_main.Controls.Add(this.dgv_productlist, 0, 5);
            this.tlp_main.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tlp_main.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tlp_main.Controls.Add(this.tlp_token_net_pay, 0, 2);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tlp_main.RowCount = 8;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.Size = new System.Drawing.Size(820, 593);
            this.tlp_main.TabIndex = 14;
            // 
            // pnl_cus_cash_details
            // 
            this.pnl_cus_cash_details.Controls.Add(this.tlp_custom);
            this.pnl_cus_cash_details.Controls.Add(this.tlp_cashrefund);
            this.pnl_cus_cash_details.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_cus_cash_details.Location = new System.Drawing.Point(11, 323);
            this.pnl_cus_cash_details.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnl_cus_cash_details.Name = "pnl_cus_cash_details";
            this.pnl_cus_cash_details.Size = new System.Drawing.Size(798, 68);
            this.pnl_cus_cash_details.TabIndex = 22;
            this.pnl_cus_cash_details.Visible = false;
            // 
            // tlp_custom
            // 
            this.tlp_custom.ColumnCount = 4;
            this.tlp_custom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.53659F));
            this.tlp_custom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.46341F));
            this.tlp_custom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 241F));
            this.tlp_custom.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tlp_custom.Controls.Add(this.lblDebitLedgerId, 0, 2);
            this.tlp_custom.Controls.Add(this.lblFMCardType, 3, 1);
            this.tlp_custom.Controls.Add(this.lblCardType, 3, 0);
            this.tlp_custom.Controls.Add(this.lblcbalance, 0, 1);
            this.tlp_custom.Controls.Add(this.label3, 0, 0);
            this.tlp_custom.Controls.Add(this.label4, 1, 0);
            this.tlp_custom.Controls.Add(this.label5, 2, 0);
            this.tlp_custom.Controls.Add(this.lblrefunbalance, 1, 1);
            this.tlp_custom.Controls.Add(this.lblcardno, 2, 1);
            this.tlp_custom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_custom.Location = new System.Drawing.Point(0, 0);
            this.tlp_custom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_custom.Name = "tlp_custom";
            this.tlp_custom.RowCount = 3;
            this.tlp_custom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_custom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tlp_custom.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlp_custom.Size = new System.Drawing.Size(798, 68);
            this.tlp_custom.TabIndex = 22;
            this.tlp_custom.Paint += new System.Windows.Forms.PaintEventHandler(this.tlp_custom_Paint);
            // 
            // lblFMCardType
            // 
            this.lblFMCardType.AutoSize = true;
            this.lblFMCardType.Location = new System.Drawing.Point(753, 19);
            this.lblFMCardType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFMCardType.Name = "lblFMCardType";
            this.lblFMCardType.Size = new System.Drawing.Size(0, 16);
            this.lblFMCardType.TabIndex = 7;
            this.lblFMCardType.Visible = false;
            // 
            // lblCardType
            // 
            this.lblCardType.AutoSize = true;
            this.lblCardType.Location = new System.Drawing.Point(753, 0);
            this.lblCardType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCardType.Name = "lblCardType";
            this.lblCardType.Size = new System.Drawing.Size(0, 16);
            this.lblCardType.TabIndex = 6;
            this.lblCardType.Visible = false;
            // 
            // lblcbalance
            // 
            this.lblcbalance.AutoSize = true;
            this.lblcbalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcbalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcbalance.Location = new System.Drawing.Point(4, 19);
            this.lblcbalance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblcbalance.Name = "lblcbalance";
            this.lblcbalance.Size = new System.Drawing.Size(239, 39);
            this.lblcbalance.TabIndex = 4;
            this.lblcbalance.Text = "label9";
            this.lblcbalance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label3.Location = new System.Drawing.Point(4, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(239, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Current Balance";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label4.Location = new System.Drawing.Point(251, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(253, 19);
            this.label4.TabIndex = 1;
            this.label4.Text = "Refund Balance";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label5.Location = new System.Drawing.Point(512, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(233, 19);
            this.label5.TabIndex = 2;
            this.label5.Text = "Card No";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblrefunbalance
            // 
            this.lblrefunbalance.AutoSize = true;
            this.lblrefunbalance.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblrefunbalance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblrefunbalance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblrefunbalance.Location = new System.Drawing.Point(251, 19);
            this.lblrefunbalance.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblrefunbalance.Name = "lblrefunbalance";
            this.lblrefunbalance.Size = new System.Drawing.Size(253, 39);
            this.lblrefunbalance.TabIndex = 3;
            this.lblrefunbalance.Text = "label8";
            this.lblrefunbalance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblcardno
            // 
            this.lblcardno.AutoSize = true;
            this.lblcardno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcardno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcardno.Location = new System.Drawing.Point(512, 19);
            this.lblcardno.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblcardno.Name = "lblcardno";
            this.lblcardno.Size = new System.Drawing.Size(233, 39);
            this.lblcardno.TabIndex = 5;
            this.lblcardno.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlp_cashrefund
            // 
            this.tlp_cashrefund.ColumnCount = 2;
            this.tlp_cashrefund.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_cashrefund.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_cashrefund.Controls.Add(this.lblcashrefundamt, 1, 0);
            this.tlp_cashrefund.Controls.Add(this.label8, 0, 0);
            this.tlp_cashrefund.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_cashrefund.Location = new System.Drawing.Point(0, 0);
            this.tlp_cashrefund.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_cashrefund.Name = "tlp_cashrefund";
            this.tlp_cashrefund.RowCount = 1;
            this.tlp_cashrefund.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_cashrefund.Size = new System.Drawing.Size(798, 68);
            this.tlp_cashrefund.TabIndex = 23;
            // 
            // lblcashrefundamt
            // 
            this.lblcashrefundamt.AutoSize = true;
            this.lblcashrefundamt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcashrefundamt.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcashrefundamt.Location = new System.Drawing.Point(202, 0);
            this.lblcashrefundamt.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblcashrefundamt.Name = "lblcashrefundamt";
            this.lblcashrefundamt.Size = new System.Drawing.Size(592, 68);
            this.lblcashrefundamt.TabIndex = 1;
            this.lblcashrefundamt.Text = "0.00";
            this.lblcashrefundamt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(190, 68);
            this.label8.TabIndex = 0;
            this.label8.Text = "Refund Amount";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlp_navbar
            // 
            this.tlp_navbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(80)))), ((int)(((byte)(185)))));
            this.tlp_navbar.ColumnCount = 3;
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_navbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_navbar.Controls.Add(this.label6, 0, 0);
            this.tlp_navbar.Controls.Add(this.btnmini, 1, 0);
            this.tlp_navbar.Controls.Add(this.btncolse, 2, 0);
            this.tlp_navbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_navbar.Location = new System.Drawing.Point(11, 10);
            this.tlp_navbar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_navbar.Name = "tlp_navbar";
            this.tlp_navbar.RowCount = 1;
            this.tlp_navbar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.Size = new System.Drawing.Size(798, 38);
            this.tlp_navbar.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(4, 0);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 29);
            this.label6.TabIndex = 12;
            this.label6.Text = "Refund";
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(720, 4);
            this.btnmini.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(33, 30);
            this.btnmini.TabIndex = 9;
            this.btnmini.Text = "-";
            this.btnmini.UseVisualStyleBackColor = false;
            // 
            // btncolse
            // 
            this.btncolse.BackColor = System.Drawing.Color.White;
            this.btncolse.FlatAppearance.BorderSize = 0;
            this.btncolse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncolse.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncolse.ForeColor = System.Drawing.Color.Red;
            this.btncolse.Location = new System.Drawing.Point(761, 4);
            this.btncolse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(33, 30);
            this.btncolse.TabIndex = 10;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // tlp_button
            // 
            this.tlp_button.ColumnCount = 5;
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.Controls.Add(this.btnSearch, 1, 0);
            this.tlp_button.Controls.Add(this.btnrefund, 1, 0);
            this.tlp_button.Controls.Add(this.btnrefresh, 2, 0);
            this.tlp_button.Controls.Add(this.btnclose, 3, 0);
            this.tlp_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_button.Location = new System.Drawing.Point(11, 535);
            this.tlp_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_button.Name = "tlp_button";
            this.tlp_button.RowCount = 1;
            this.tlp_button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_button.Size = new System.Drawing.Size(798, 48);
            this.tlp_button.TabIndex = 5;
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
            this.btnSearch.Location = new System.Drawing.Point(396, 5);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(127, 38);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnrefund
            // 
            this.btnrefund.BackColor = System.Drawing.Color.Transparent;
            this.btnrefund.BackgroundImage = global::FoodandBeverage.Properties.Resources.SaveButton;
            this.btnrefund.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrefund.FlatAppearance.BorderSize = 0;
            this.btnrefund.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnrefund.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnrefund.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrefund.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnrefund.Location = new System.Drawing.Point(260, 4);
            this.btnrefund.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnrefund.Name = "btnrefund";
            this.btnrefund.Size = new System.Drawing.Size(127, 39);
            this.btnrefund.TabIndex = 0;
            this.btnrefund.UseVisualStyleBackColor = false;
            this.btnrefund.Click += new System.EventHandler(this.btnrefund_Click);
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
            this.btnrefresh.Location = new System.Drawing.Point(532, 4);
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
            this.btnclose.Location = new System.Drawing.Point(667, 4);
            this.btnclose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(127, 39);
            this.btnclose.TabIndex = 3;
            this.btnclose.UseVisualStyleBackColor = false;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // dgv_productlist
            // 
            this.dgv_productlist.AllowUserToAddRows = false;
            this.dgv_productlist.AllowUserToDeleteRows = false;
            this.dgv_productlist.AllowUserToResizeRows = false;
            this.dgv_productlist.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_productlist.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_productlist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_productlist.ColumnHeadersHeight = 60;
            this.dgv_productlist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.no,
            this.Usertype,
            this.price,
            this.Quantity});
            this.dgv_productlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_productlist.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_productlist.Location = new System.Drawing.Point(11, 399);
            this.dgv_productlist.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgv_productlist.Name = "dgv_productlist";
            this.dgv_productlist.RowHeadersVisible = false;
            this.dgv_productlist.RowHeadersWidth = 51;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.dgv_productlist.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_productlist.RowTemplate.Height = 30;
            this.dgv_productlist.Size = new System.Drawing.Size(798, 128);
            this.dgv_productlist.TabIndex = 18;
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
            // Usertype
            // 
            this.Usertype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Usertype.HeaderText = "Food Name";
            this.Usertype.MinimumWidth = 6;
            this.Usertype.Name = "Usertype";
            this.Usertype.ReadOnly = true;
            // 
            // price
            // 
            this.price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.price.HeaderText = "Price";
            this.price.MinimumWidth = 6;
            this.price.Name = "price";
            this.price.ReadOnly = true;
            // 
            // Quantity
            // 
            this.Quantity.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle5.NullValue = "0";
            this.Quantity.DefaultCellStyle = dataGridViewCellStyle5;
            this.Quantity.HeaderText = "Quantity";
            this.Quantity.MinimumWidth = 6;
            this.Quantity.Name = "Quantity";
            this.Quantity.Width = 114;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.lblorderstatus, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnsearchbill, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(11, 56);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(798, 58);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // btnsearchbill
            // 
            this.btnsearchbill.BackgroundImage = global::FoodandBeverage.Properties.Resources.searchbill;
            this.btnsearchbill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnsearchbill.FlatAppearance.BorderSize = 0;
            this.btnsearchbill.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsearchbill.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsearchbill.ForeColor = System.Drawing.Color.White;
            this.btnsearchbill.Location = new System.Drawing.Point(4, 4);
            this.btnsearchbill.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnsearchbill.Name = "btnsearchbill";
            this.btnsearchbill.Size = new System.Drawing.Size(168, 47);
            this.btnsearchbill.TabIndex = 1;
            this.btnsearchbill.UseVisualStyleBackColor = true;
            this.btnsearchbill.Click += new System.EventHandler(this.btnsearchbill_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.btnrefund_cash_point, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label55, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label15, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.cmbRefundPayType, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label16, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtRenote, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.cmbRefundReson, 2, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(11, 201);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.25806F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 67.74194F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(798, 114);
            this.tableLayoutPanel2.TabIndex = 20;
            // 
            // btnrefund_cash_point
            // 
            this.btnrefund_cash_point.BackgroundImage = global::FoodandBeverage.Properties.Resources.Button_Price_change;
            this.btnrefund_cash_point.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrefund_cash_point.FlatAppearance.BorderSize = 0;
            this.btnrefund_cash_point.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrefund_cash_point.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnrefund_cash_point.ForeColor = System.Drawing.Color.White;
            this.btnrefund_cash_point.Location = new System.Drawing.Point(268, 40);
            this.btnrefund_cash_point.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnrefund_cash_point.Name = "btnrefund_cash_point";
            this.btnrefund_cash_point.Size = new System.Drawing.Size(119, 60);
            this.btnrefund_cash_point.TabIndex = 360;
            this.btnrefund_cash_point.Text = "Cash Refund";
            this.btnrefund_cash_point.UseVisualStyleBackColor = true;
            this.btnrefund_cash_point.Visible = false;
            this.btnrefund_cash_point.Click += new System.EventHandler(this.btnrefund_cash_point_Click);
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label55.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label55.ForeColor = System.Drawing.Color.Black;
            this.label55.Location = new System.Drawing.Point(4, 0);
            this.label55.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(256, 36);
            this.label55.TabIndex = 341;
            this.label55.Text = "Refund Pay Type";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(395, 0);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(187, 36);
            this.label15.TabIndex = 356;
            this.label15.Text = "Refund Reason";
            // 
            // cmbRefundPayType
            // 
            this.cmbRefundPayType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbRefundPayType.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRefundPayType.FormattingEnabled = true;
            this.cmbRefundPayType.Location = new System.Drawing.Point(4, 40);
            this.cmbRefundPayType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbRefundPayType.Name = "cmbRefundPayType";
            this.cmbRefundPayType.Size = new System.Drawing.Size(256, 32);
            this.cmbRefundPayType.TabIndex = 342;
            this.cmbRefundPayType.SelectionChangeCommitted += new System.EventHandler(this.cmbRefundPayType_SelectionChangeCommitted);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Black;
            this.label16.Location = new System.Drawing.Point(590, 0);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(204, 36);
            this.label16.TabIndex = 358;
            this.label16.Text = "Note";
            this.label16.Visible = false;
            // 
            // txtRenote
            // 
            this.txtRenote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRenote.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRenote.Location = new System.Drawing.Point(590, 40);
            this.txtRenote.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtRenote.Multiline = true;
            this.txtRenote.Name = "txtRenote";
            this.txtRenote.Size = new System.Drawing.Size(204, 70);
            this.txtRenote.TabIndex = 359;
            this.txtRenote.Visible = false;
            // 
            // cmbRefundReson
            // 
            this.cmbRefundReson.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbRefundReson.FormattingEnabled = true;
            this.cmbRefundReson.Location = new System.Drawing.Point(395, 40);
            this.cmbRefundReson.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbRefundReson.Name = "cmbRefundReson";
            this.cmbRefundReson.Size = new System.Drawing.Size(187, 32);
            this.cmbRefundReson.TabIndex = 357;
            this.cmbRefundReson.SelectionChangeCommitted += new System.EventHandler(this.cmbRefundReson_SelectionChangeCommitted);
            // 
            // tlp_token_net_pay
            // 
            this.tlp_token_net_pay.ColumnCount = 4;
            this.tlp_token_net_pay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_token_net_pay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_token_net_pay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_token_net_pay.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 507F));
            this.tlp_token_net_pay.Controls.Add(this.lbltoken, 0, 1);
            this.tlp_token_net_pay.Controls.Add(this.lblpaymenttype, 1, 1);
            this.tlp_token_net_pay.Controls.Add(this.lblnetamount, 0, 1);
            this.tlp_token_net_pay.Controls.Add(this.label2, 1, 0);
            this.tlp_token_net_pay.Controls.Add(this.label1, 0, 0);
            this.tlp_token_net_pay.Controls.Add(this.label7, 2, 0);
            this.tlp_token_net_pay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_token_net_pay.Location = new System.Drawing.Point(11, 122);
            this.tlp_token_net_pay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_token_net_pay.Name = "tlp_token_net_pay";
            this.tlp_token_net_pay.RowCount = 2;
            this.tlp_token_net_pay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_token_net_pay.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlp_token_net_pay.Size = new System.Drawing.Size(798, 71);
            this.tlp_token_net_pay.TabIndex = 21;
            // 
            // lbltoken
            // 
            this.lbltoken.AutoSize = true;
            this.lbltoken.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltoken.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltoken.Location = new System.Drawing.Point(4, 35);
            this.lbltoken.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbltoken.Name = "lbltoken";
            this.lbltoken.Size = new System.Drawing.Size(94, 36);
            this.lbltoken.TabIndex = 4;
            this.lbltoken.Text = "0";
            this.lbltoken.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblpaymenttype
            // 
            this.lblpaymenttype.AutoSize = true;
            this.lblpaymenttype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblpaymenttype.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpaymenttype.Location = new System.Drawing.Point(224, 35);
            this.lblpaymenttype.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblpaymenttype.Name = "lblpaymenttype";
            this.lblpaymenttype.Size = new System.Drawing.Size(137, 36);
            this.lblpaymenttype.TabIndex = 3;
            this.lblpaymenttype.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblnetamount
            // 
            this.lblnetamount.AutoSize = true;
            this.lblnetamount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblnetamount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblnetamount.Location = new System.Drawing.Point(106, 35);
            this.lblnetamount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblnetamount.Name = "lblnetamount";
            this.lblnetamount.Size = new System.Drawing.Size(110, 36);
            this.lblnetamount.TabIndex = 2;
            this.lblnetamount.Text = "0.00";
            this.lblnetamount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label2.Location = new System.Drawing.Point(106, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 35);
            this.label2.TabIndex = 1;
            this.label2.Text = "Net Amount";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Token No";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label7.Location = new System.Drawing.Point(224, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 35);
            this.label7.TabIndex = 5;
            this.label7.Text = "Payment Mode";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtsalemasterid
            // 
            this.txtsalemasterid.Location = new System.Drawing.Point(44, 5);
            this.txtsalemasterid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtsalemasterid.Name = "txtsalemasterid";
            this.txtsalemasterid.Size = new System.Drawing.Size(17, 22);
            this.txtsalemasterid.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtordermasterid);
            this.panel1.Controls.Add(this.txtledgerid);
            this.panel1.Controls.Add(this.txtsalemasterid);
            this.panel1.Location = new System.Drawing.Point(20, 15);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(177, 30);
            this.panel1.TabIndex = 16;
            this.panel1.Visible = false;
            // 
            // txtordermasterid
            // 
            this.txtordermasterid.Location = new System.Drawing.Point(28, 4);
            this.txtordermasterid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtordermasterid.Name = "txtordermasterid";
            this.txtordermasterid.Size = new System.Drawing.Size(17, 22);
            this.txtordermasterid.TabIndex = 4;
            this.txtordermasterid.Text = "0";
            // 
            // txtledgerid
            // 
            this.txtledgerid.Location = new System.Drawing.Point(17, 5);
            this.txtledgerid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtledgerid.Name = "txtledgerid";
            this.txtledgerid.Size = new System.Drawing.Size(17, 22);
            this.txtledgerid.TabIndex = 3;
            this.txtledgerid.Text = "0";
            // 
            // wificard_timer
            // 
            this.wificard_timer.Tick += new System.EventHandler(this.wificard_timer_Tick);
            // 
            // pnlwifi
            // 
            this.pnlwifi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(152)))), ((int)(((byte)(227)))));
            this.pnlwifi.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlwifi.Controls.Add(this.tlp_wifiPopup);
            this.pnlwifi.Location = new System.Drawing.Point(867, 110);
            this.pnlwifi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlwifi.Name = "pnlwifi";
            this.pnlwifi.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
            this.pnlwifi.Size = new System.Drawing.Size(476, 373);
            this.pnlwifi.TabIndex = 300;
            this.pnlwifi.Visible = false;
            // 
            // tlp_wifiPopup
            // 
            this.tlp_wifiPopup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(152)))), ((int)(((byte)(227)))));
            this.tlp_wifiPopup.ColumnCount = 1;
            this.tlp_wifiPopup.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 57.25191F));
            this.tlp_wifiPopup.Controls.Add(this.pbwifi, 0, 1);
            this.tlp_wifiPopup.Controls.Add(this.btnwificancel, 0, 0);
            this.tlp_wifiPopup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_wifiPopup.Location = new System.Drawing.Point(13, 12);
            this.tlp_wifiPopup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_wifiPopup.Name = "tlp_wifiPopup";
            this.tlp_wifiPopup.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.tlp_wifiPopup.RowCount = 2;
            this.tlp_wifiPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlp_wifiPopup.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_wifiPopup.Size = new System.Drawing.Size(446, 345);
            this.tlp_wifiPopup.TabIndex = 2;
            // 
            // pbwifi
            // 
            this.pbwifi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbwifi.Image = ((System.Drawing.Image)(resources.GetObject("pbwifi.Image")));
            this.pbwifi.Location = new System.Drawing.Point(11, 60);
            this.pbwifi.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbwifi.Name = "pbwifi";
            this.pbwifi.Size = new System.Drawing.Size(424, 275);
            this.pbwifi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbwifi.TabIndex = 0;
            this.pbwifi.TabStop = false;
            // 
            // btnwificancel
            // 
            this.btnwificancel.BackgroundImage = global::FoodandBeverage.Properties.Resources.cancel_icon_wifi;
            this.btnwificancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnwificancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnwificancel.FlatAppearance.BorderSize = 0;
            this.btnwificancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnwificancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnwificancel.ForeColor = System.Drawing.Color.Red;
            this.btnwificancel.Location = new System.Drawing.Point(379, 10);
            this.btnwificancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnwificancel.Name = "btnwificancel";
            this.btnwificancel.Size = new System.Drawing.Size(56, 42);
            this.btnwificancel.TabIndex = 1;
            this.btnwificancel.UseVisualStyleBackColor = true;
            this.btnwificancel.Click += new System.EventHandler(this.btnwificancel_Click);
            // 
            // lblorderstatus
            // 
            this.lblorderstatus.AutoSize = true;
            this.lblorderstatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblorderstatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblorderstatus.Location = new System.Drawing.Point(179, 0);
            this.lblorderstatus.Name = "lblorderstatus";
            this.lblorderstatus.Size = new System.Drawing.Size(155, 58);
            this.lblorderstatus.TabIndex = 3;
            this.lblorderstatus.Text = "order Status";
            this.lblorderstatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblorderstatus.Visible = false;
            // 
            // lblDebitLedgerId
            // 
            this.lblDebitLedgerId.AutoSize = true;
            this.lblDebitLedgerId.Location = new System.Drawing.Point(4, 58);
            this.lblDebitLedgerId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDebitLedgerId.Name = "lblDebitLedgerId";
            this.lblDebitLedgerId.Size = new System.Drawing.Size(0, 10);
            this.lblDebitLedgerId.TabIndex = 8;
            this.lblDebitLedgerId.Visible = false;
            // 
            // FrmRefund
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 593);
            this.Controls.Add(this.tlp_main);
            this.Controls.Add(this.pnlwifi);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FrmRefund";
            this.Text = "FrmRefund";
            this.Load += new System.EventHandler(this.FrmRefund_Load);
            this.Resize += new System.EventHandler(this.FrmRefund_Resize);
            this.tlp_main.ResumeLayout(false);
            this.pnl_cus_cash_details.ResumeLayout(false);
            this.tlp_custom.ResumeLayout(false);
            this.tlp_custom.PerformLayout();
            this.tlp_cashrefund.ResumeLayout(false);
            this.tlp_cashrefund.PerformLayout();
            this.tlp_navbar.ResumeLayout(false);
            this.tlp_navbar.PerformLayout();
            this.tlp_button.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_productlist)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tlp_token_net_pay.ResumeLayout(false);
            this.tlp_token_net_pay.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlwifi.ResumeLayout(false);
            this.tlp_wifiPopup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbwifi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TableLayoutPanel tlp_navbar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.TableLayoutPanel tlp_button;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnrefund;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.DataGridView dgv_productlist;
        private System.Windows.Forms.DataGridViewTextBoxColumn no;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usertype;
        private System.Windows.Forms.DataGridViewTextBoxColumn price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnsearchbill;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.ComboBox cmbRefundReson;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbRefundPayType;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtRenote;
        private System.Windows.Forms.TableLayoutPanel tlp_token_net_pay;
        private System.Windows.Forms.Label lbltoken;
        private System.Windows.Forms.Label lblpaymenttype;
        private System.Windows.Forms.Label lblnetamount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtsalemasterid;
        private System.Windows.Forms.Button btnrefund_cash_point;
        private System.Windows.Forms.TableLayoutPanel tlp_custom;
        private System.Windows.Forms.Label lblcardno;
        private System.Windows.Forms.Label lblcbalance;
        private System.Windows.Forms.Label lblrefunbalance;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtledgerid;
        private System.Windows.Forms.Timer wificard_timer;
        private System.Windows.Forms.Panel pnlwifi;
        private System.Windows.Forms.TableLayoutPanel tlp_wifiPopup;
        private System.Windows.Forms.PictureBox pbwifi;
        private System.Windows.Forms.Button btnwificancel;
        private System.Windows.Forms.TextBox txtordermasterid;
        private System.Windows.Forms.Panel pnl_cus_cash_details;
        private System.Windows.Forms.TableLayoutPanel tlp_cashrefund;
        private System.Windows.Forms.Label lblcashrefundamt;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCardType;
        private System.Windows.Forms.Label lblFMCardType;
        private System.Windows.Forms.Label lblorderstatus;
        private System.Windows.Forms.Label lblDebitLedgerId;
    }
}