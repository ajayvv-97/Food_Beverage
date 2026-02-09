
namespace FoodandBeverage.Report
{
    partial class Frmreport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmreport));
            this.pnl_report = new System.Windows.Forms.Panel();
            this.tlp_report_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_navbar = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnmini = new System.Windows.Forms.Button();
            this.btncolse = new System.Windows.Forms.Button();
            this.tlp_top_row = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_finyear = new System.Windows.Forms.TableLayoutPanel();
            this.cmb_fin_year = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tabDateTimePanel = new System.Windows.Forms.TableLayoutPanel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chk_time = new System.Windows.Forms.CheckBox();
            this.tlp_second_row = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_reorder = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.nud_reorderqty = new System.Windows.Forms.NumericUpDown();
            this.tlp_sublist = new System.Windows.Forms.TableLayoutPanel();
            this.lblsublist = new System.Windows.Forms.Label();
            this.cmbSublist = new System.Windows.Forms.ComboBox();
            this.tblReportCategory = new System.Windows.Forms.TableLayoutPanel();
            this.btnYealy = new System.Windows.Forms.Button();
            this.btnWeekEnd = new System.Windows.Forms.Button();
            this.btnMonthly = new System.Windows.Forms.Button();
            this.btnWeekly = new System.Windows.Forms.Button();
            this.btnserach = new System.Windows.Forms.Button();
            this.crv_report = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.dtp_to_time = new System.Windows.Forms.DateTimePicker();
            this.dtpto = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dtp_from_time = new System.Windows.Forms.DateTimePicker();
            this.dtpfrom = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.pnl_report.SuspendLayout();
            this.tlp_report_main.SuspendLayout();
            this.tlp_navbar.SuspendLayout();
            this.tlp_top_row.SuspendLayout();
            this.tlp_finyear.SuspendLayout();
            this.tabDateTimePanel.SuspendLayout();
            this.tlp_second_row.SuspendLayout();
            this.tlp_reorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_reorderqty)).BeginInit();
            this.tlp_sublist.SuspendLayout();
            this.tblReportCategory.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl_report
            // 
            this.pnl_report.Controls.Add(this.tlp_report_main);
            this.pnl_report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_report.Location = new System.Drawing.Point(7, 6);
            this.pnl_report.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnl_report.Name = "pnl_report";
            this.pnl_report.Size = new System.Drawing.Size(1190, 649);
            this.pnl_report.TabIndex = 3;
            // 
            // tlp_report_main
            // 
            this.tlp_report_main.BackColor = System.Drawing.Color.White;
            this.tlp_report_main.ColumnCount = 1;
            this.tlp_report_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_report_main.Controls.Add(this.tlp_navbar, 0, 0);
            this.tlp_report_main.Controls.Add(this.tlp_top_row, 0, 1);
            this.tlp_report_main.Controls.Add(this.tlp_second_row, 0, 2);
            this.tlp_report_main.Controls.Add(this.crv_report, 0, 3);
            this.tlp_report_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_report_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_report_main.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_report_main.Name = "tlp_report_main";
            this.tlp_report_main.RowCount = 4;
            this.tlp_report_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tlp_report_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.56716F));
            this.tlp_report_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.701493F));
            this.tlp_report_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 78.73135F));
            this.tlp_report_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlp_report_main.Size = new System.Drawing.Size(1190, 649);
            this.tlp_report_main.TabIndex = 49;
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
            this.tlp_navbar.Location = new System.Drawing.Point(4, 4);
            this.tlp_navbar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_navbar.Name = "tlp_navbar";
            this.tlp_navbar.RowCount = 1;
            this.tlp_navbar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_navbar.Size = new System.Drawing.Size(1182, 38);
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
            this.label2.Size = new System.Drawing.Size(92, 29);
            this.label2.TabIndex = 12;
            this.label2.Text = "Report";
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(1104, 4);
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
            this.btncolse.Location = new System.Drawing.Point(1145, 4);
            this.btncolse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(33, 30);
            this.btncolse.TabIndex = 10;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // tlp_top_row
            // 
            this.tlp_top_row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_top_row.ColumnCount = 5;
            this.tlp_top_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_top_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_top_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_top_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_top_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_top_row.Controls.Add(this.tlp_finyear, 3, 0);
            this.tlp_top_row.Controls.Add(this.tabDateTimePanel, 4, 0);
            this.tlp_top_row.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_top_row.Location = new System.Drawing.Point(4, 50);
            this.tlp_top_row.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_top_row.Name = "tlp_top_row";
            this.tlp_top_row.RowCount = 1;
            this.tlp_top_row.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_top_row.Size = new System.Drawing.Size(1182, 61);
            this.tlp_top_row.TabIndex = 0;
            // 
            // tlp_finyear
            // 
            this.tlp_finyear.ColumnCount = 2;
            this.tlp_finyear.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_finyear.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_finyear.Controls.Add(this.cmb_fin_year, 1, 0);
            this.tlp_finyear.Controls.Add(this.label8, 0, 0);
            this.tlp_finyear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_finyear.Location = new System.Drawing.Point(4, 4);
            this.tlp_finyear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_finyear.Name = "tlp_finyear";
            this.tlp_finyear.RowCount = 2;
            this.tlp_finyear.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_finyear.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlp_finyear.Size = new System.Drawing.Size(413, 53);
            this.tlp_finyear.TabIndex = 50;
            this.tlp_finyear.Visible = false;
            // 
            // cmb_fin_year
            // 
            this.cmb_fin_year.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.cmb_fin_year.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_fin_year.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_fin_year.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_fin_year.FormattingEnabled = true;
            this.cmb_fin_year.Location = new System.Drawing.Point(142, 4);
            this.cmb_fin_year.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmb_fin_year.Name = "cmb_fin_year";
            this.cmb_fin_year.Size = new System.Drawing.Size(255, 32);
            this.cmb_fin_year.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(4, 0);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 28);
            this.label8.TabIndex = 7;
            this.label8.Text = "Financial Year";
            // 
            // tabDateTimePanel
            // 
            this.tabDateTimePanel.BackColor = System.Drawing.Color.Transparent;
            this.tabDateTimePanel.ColumnCount = 8;
            this.tabDateTimePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tabDateTimePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tabDateTimePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tabDateTimePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tabDateTimePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tabDateTimePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tabDateTimePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tabDateTimePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tabDateTimePanel.Controls.Add(this.textBox1, 7, 0);
            this.tabDateTimePanel.Controls.Add(this.chk_time, 0, 0);
            this.tabDateTimePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDateTimePanel.Location = new System.Drawing.Point(425, 4);
            this.tabDateTimePanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabDateTimePanel.Name = "tabDateTimePanel";
            this.tabDateTimePanel.RowCount = 2;
            this.tabDateTimePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabDateTimePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabDateTimePanel.Size = new System.Drawing.Size(753, 53);
            this.tabDateTimePanel.TabIndex = 33;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(87, 4);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(132, 22);
            this.textBox1.TabIndex = 35;
            this.textBox1.Visible = false;
            // 
            // chk_time
            // 
            this.chk_time.AutoSize = true;
            this.chk_time.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chk_time.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.chk_time.Location = new System.Drawing.Point(4, 4);
            this.chk_time.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chk_time.Name = "chk_time";
            this.chk_time.Size = new System.Drawing.Size(75, 28);
            this.chk_time.TabIndex = 34;
            this.chk_time.Text = "Time";
            this.chk_time.UseVisualStyleBackColor = true;
            this.chk_time.CheckedChanged += new System.EventHandler(this.chk_time_CheckedChanged);
            // 
            // tlp_second_row
            // 
            this.tlp_second_row.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_second_row.ColumnCount = 7;
            this.tlp_second_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_second_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_second_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_second_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_second_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_second_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_second_row.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_second_row.Controls.Add(this.tlp_reorder, 1, 0);
            this.tlp_second_row.Controls.Add(this.tlp_sublist, 0, 0);
            this.tlp_second_row.Controls.Add(this.tblReportCategory, 4, 0);
            this.tlp_second_row.Controls.Add(this.btnserach, 6, 0);
            this.tlp_second_row.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_second_row.Location = new System.Drawing.Point(4, 119);
            this.tlp_second_row.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_second_row.Name = "tlp_second_row";
            this.tlp_second_row.RowCount = 1;
            this.tlp_second_row.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_second_row.Size = new System.Drawing.Size(1182, 50);
            this.tlp_second_row.TabIndex = 1;
            // 
            // tlp_reorder
            // 
            this.tlp_reorder.ColumnCount = 2;
            this.tlp_reorder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.65563F));
            this.tlp_reorder.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.34437F));
            this.tlp_reorder.Controls.Add(this.label1, 0, 0);
            this.tlp_reorder.Controls.Add(this.nud_reorderqty, 1, 0);
            this.tlp_reorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_reorder.Location = new System.Drawing.Point(349, 4);
            this.tlp_reorder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_reorder.Name = "tlp_reorder";
            this.tlp_reorder.RowCount = 1;
            this.tlp_reorder.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_reorder.Size = new System.Drawing.Size(201, 42);
            this.tlp_reorder.TabIndex = 50;
            this.tlp_reorder.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label1.Location = new System.Drawing.Point(4, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter Qty";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nud_reorderqty
            // 
            this.nud_reorderqty.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nud_reorderqty.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nud_reorderqty.Location = new System.Drawing.Point(107, 4);
            this.nud_reorderqty.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nud_reorderqty.Name = "nud_reorderqty";
            this.nud_reorderqty.Size = new System.Drawing.Size(90, 29);
            this.nud_reorderqty.TabIndex = 1;
            // 
            // tlp_sublist
            // 
            this.tlp_sublist.ColumnCount = 2;
            this.tlp_sublist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.71542F));
            this.tlp_sublist.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 76.28458F));
            this.tlp_sublist.Controls.Add(this.lblsublist, 0, 0);
            this.tlp_sublist.Controls.Add(this.cmbSublist, 1, 0);
            this.tlp_sublist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_sublist.Location = new System.Drawing.Point(4, 4);
            this.tlp_sublist.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tlp_sublist.Name = "tlp_sublist";
            this.tlp_sublist.RowCount = 1;
            this.tlp_sublist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_sublist.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tlp_sublist.Size = new System.Drawing.Size(337, 42);
            this.tlp_sublist.TabIndex = 12;
            this.tlp_sublist.Visible = false;
            // 
            // lblsublist
            // 
            this.lblsublist.AutoSize = true;
            this.lblsublist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblsublist.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.lblsublist.Location = new System.Drawing.Point(4, 0);
            this.lblsublist.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblsublist.Name = "lblsublist";
            this.lblsublist.Size = new System.Drawing.Size(71, 42);
            this.lblsublist.TabIndex = 12;
            this.lblsublist.Text = "List";
            this.lblsublist.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbSublist
            // 
            this.cmbSublist.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.cmbSublist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbSublist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSublist.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSublist.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSublist.FormattingEnabled = true;
            this.cmbSublist.Location = new System.Drawing.Point(83, 4);
            this.cmbSublist.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbSublist.Name = "cmbSublist";
            this.cmbSublist.Size = new System.Drawing.Size(250, 32);
            this.cmbSublist.TabIndex = 48;
            // 
            // tblReportCategory
            // 
            this.tblReportCategory.ColumnCount = 4;
            this.tblReportCategory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblReportCategory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblReportCategory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblReportCategory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblReportCategory.Controls.Add(this.btnYealy, 3, 0);
            this.tblReportCategory.Controls.Add(this.btnWeekEnd, 0, 0);
            this.tblReportCategory.Controls.Add(this.btnMonthly, 2, 0);
            this.tblReportCategory.Controls.Add(this.btnWeekly, 1, 0);
            this.tblReportCategory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblReportCategory.Location = new System.Drawing.Point(558, 4);
            this.tblReportCategory.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tblReportCategory.Name = "tblReportCategory";
            this.tblReportCategory.RowCount = 1;
            this.tblReportCategory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblReportCategory.Size = new System.Drawing.Size(503, 42);
            this.tblReportCategory.TabIndex = 45;
            // 
            // btnYealy
            // 
            this.btnYealy.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnYealy.BackgroundImage")));
            this.btnYealy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnYealy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnYealy.FlatAppearance.BorderSize = 0;
            this.btnYealy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYealy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnYealy.ForeColor = System.Drawing.Color.White;
            this.btnYealy.Location = new System.Drawing.Point(379, 4);
            this.btnYealy.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnYealy.Name = "btnYealy";
            this.btnYealy.Size = new System.Drawing.Size(120, 34);
            this.btnYealy.TabIndex = 47;
            this.btnYealy.Text = "Yearly";
            this.btnYealy.UseVisualStyleBackColor = true;
            this.btnYealy.Click += new System.EventHandler(this.btnYealy_Click);
            // 
            // btnWeekEnd
            // 
            this.btnWeekEnd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnWeekEnd.BackgroundImage")));
            this.btnWeekEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnWeekEnd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWeekEnd.FlatAppearance.BorderSize = 0;
            this.btnWeekEnd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWeekEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeekEnd.ForeColor = System.Drawing.Color.White;
            this.btnWeekEnd.Location = new System.Drawing.Point(4, 4);
            this.btnWeekEnd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnWeekEnd.Name = "btnWeekEnd";
            this.btnWeekEnd.Size = new System.Drawing.Size(117, 34);
            this.btnWeekEnd.TabIndex = 43;
            this.btnWeekEnd.Text = "WeekEnd";
            this.btnWeekEnd.UseVisualStyleBackColor = true;
            this.btnWeekEnd.Click += new System.EventHandler(this.btnWeekEnd_Click);
            // 
            // btnMonthly
            // 
            this.btnMonthly.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMonthly.BackgroundImage")));
            this.btnMonthly.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMonthly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMonthly.FlatAppearance.BorderSize = 0;
            this.btnMonthly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMonthly.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonthly.ForeColor = System.Drawing.Color.White;
            this.btnMonthly.Location = new System.Drawing.Point(254, 4);
            this.btnMonthly.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnMonthly.Name = "btnMonthly";
            this.btnMonthly.Size = new System.Drawing.Size(117, 34);
            this.btnMonthly.TabIndex = 46;
            this.btnMonthly.Text = "Monthly";
            this.btnMonthly.UseVisualStyleBackColor = true;
            this.btnMonthly.Click += new System.EventHandler(this.btnMonthly_Click);
            // 
            // btnWeekly
            // 
            this.btnWeekly.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnWeekly.BackgroundImage")));
            this.btnWeekly.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnWeekly.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnWeekly.FlatAppearance.BorderSize = 0;
            this.btnWeekly.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWeekly.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWeekly.ForeColor = System.Drawing.Color.White;
            this.btnWeekly.Location = new System.Drawing.Point(129, 4);
            this.btnWeekly.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnWeekly.Name = "btnWeekly";
            this.btnWeekly.Size = new System.Drawing.Size(117, 34);
            this.btnWeekly.TabIndex = 44;
            this.btnWeekly.Text = "Weekly";
            this.btnWeekly.UseVisualStyleBackColor = true;
            this.btnWeekly.Click += new System.EventHandler(this.btnWeekly_Click);
            // 
            // btnserach
            // 
            this.btnserach.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_SEARCH;
            this.btnserach.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnserach.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnserach.FlatAppearance.BorderSize = 0;
            this.btnserach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnserach.Location = new System.Drawing.Point(1069, 4);
            this.btnserach.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnserach.Name = "btnserach";
            this.btnserach.Size = new System.Drawing.Size(109, 42);
            this.btnserach.TabIndex = 48;
            this.btnserach.UseVisualStyleBackColor = true;
            this.btnserach.Click += new System.EventHandler(this.btnserach_Click);
            // 
            // crv_report
            // 
            this.crv_report.ActiveViewIndex = -1;
            this.crv_report.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crv_report.Cursor = System.Windows.Forms.Cursors.Default;
            this.crv_report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crv_report.Location = new System.Drawing.Point(4, 177);
            this.crv_report.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.crv_report.Name = "crv_report";
            this.crv_report.ShowCloseButton = false;
            this.crv_report.ShowCopyButton = false;
            this.crv_report.ShowGotoPageButton = false;
            this.crv_report.ShowGroupTreeButton = false;
            this.crv_report.ShowLogo = false;
            this.crv_report.ShowParameterPanelButton = false;
            this.crv_report.ShowPrintButton = false;
            this.crv_report.ShowRefreshButton = false;
            this.crv_report.ShowTextSearchButton = false;
            this.crv_report.ShowZoomButton = false;
            this.crv_report.Size = new System.Drawing.Size(1182, 468);
            this.crv_report.TabIndex = 2;
            this.crv_report.Tag = "31";
            this.crv_report.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // dtp_to_time
            // 
            this.dtp_to_time.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_to_time.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_to_time.Location = new System.Drawing.Point(49, 23);
            this.dtp_to_time.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtp_to_time.Name = "dtp_to_time";
            this.dtp_to_time.Size = new System.Drawing.Size(147, 30);
            this.dtp_to_time.TabIndex = 32;
            this.dtp_to_time.Visible = false;
            // 
            // dtpto
            // 
            this.dtpto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpto.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpto.Location = new System.Drawing.Point(47, 26);
            this.dtpto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpto.Name = "dtpto";
            this.dtpto.Size = new System.Drawing.Size(136, 30);
            this.dtpto.TabIndex = 29;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 6);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 24);
            this.label5.TabIndex = 30;
            this.label5.Text = "To :";
            // 
            // dtp_from_time
            // 
            this.dtp_from_time.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_from_time.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtp_from_time.Location = new System.Drawing.Point(40, 23);
            this.dtp_from_time.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtp_from_time.Name = "dtp_from_time";
            this.dtp_from_time.Size = new System.Drawing.Size(147, 30);
            this.dtp_from_time.TabIndex = 31;
            this.dtp_from_time.Visible = false;
            // 
            // dtpfrom
            // 
            this.dtpfrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpfrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpfrom.Location = new System.Drawing.Point(48, 26);
            this.dtpfrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpfrom.Name = "dtpfrom";
            this.dtpfrom.Size = new System.Drawing.Size(136, 30);
            this.dtpfrom.TabIndex = 28;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 36);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 24);
            this.label6.TabIndex = 27;
            this.label6.Text = "From :";
            // 
            // Frmreport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(1204, 661);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dtpfrom);
            this.Controls.Add(this.dtp_from_time);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dtpto);
            this.Controls.Add(this.dtp_to_time);
            this.Controls.Add(this.pnl_report);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Frmreport";
            this.Padding = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Text = "Frmreport";
            this.Load += new System.EventHandler(this.Frmreport_Load);
            this.pnl_report.ResumeLayout(false);
            this.tlp_report_main.ResumeLayout(false);
            this.tlp_navbar.ResumeLayout(false);
            this.tlp_navbar.PerformLayout();
            this.tlp_top_row.ResumeLayout(false);
            this.tlp_finyear.ResumeLayout(false);
            this.tlp_finyear.PerformLayout();
            this.tabDateTimePanel.ResumeLayout(false);
            this.tabDateTimePanel.PerformLayout();
            this.tlp_second_row.ResumeLayout(false);
            this.tlp_reorder.ResumeLayout(false);
            this.tlp_reorder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_reorderqty)).EndInit();
            this.tlp_sublist.ResumeLayout(false);
            this.tlp_sublist.PerformLayout();
            this.tblReportCategory.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_report;
        private System.Windows.Forms.TableLayoutPanel tlp_report_main;
        private System.Windows.Forms.TableLayoutPanel tlp_top_row;
        private System.Windows.Forms.TableLayoutPanel tabDateTimePanel;
        private System.Windows.Forms.DateTimePicker dtp_to_time;
        private System.Windows.Forms.DateTimePicker dtpto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dtp_from_time;
        private System.Windows.Forms.DateTimePicker dtpfrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chk_time;
        private System.Windows.Forms.TableLayoutPanel tlp_finyear;
        private System.Windows.Forms.ComboBox cmb_fin_year;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TableLayoutPanel tlp_second_row;
        public System.Windows.Forms.TableLayoutPanel tlp_sublist;
        private System.Windows.Forms.Label lblsublist;
        private System.Windows.Forms.ComboBox cmbSublist;
        private System.Windows.Forms.TableLayoutPanel tblReportCategory;
        private System.Windows.Forms.Button btnYealy;
        private System.Windows.Forms.Button btnWeekEnd;
        private System.Windows.Forms.Button btnMonthly;
        private System.Windows.Forms.Button btnWeekly;
        private System.Windows.Forms.Button btnserach;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crv_report;
        private System.Windows.Forms.TableLayoutPanel tlp_reorder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nud_reorderqty;
        private System.Windows.Forms.TableLayoutPanel tlp_navbar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.TextBox textBox1;
    }
}