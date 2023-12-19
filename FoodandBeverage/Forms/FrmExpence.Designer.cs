
namespace FoodandBeverage.Forms
{
    partial class FrmExpence
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExpence));
            this.tlp_navbar = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.btnmini = new System.Windows.Forms.Button();
            this.btncolse = new System.Windows.Forms.Button();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_button = new System.Windows.Forms.TableLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.dgv_expense = new System.Windows.Forms.DataGridView();
            this.Slno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmb_reasontype = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmb_employee = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cmb_paymenttype = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Action = new System.Windows.Forms.DataGridViewImageColumn();
            this.employeeid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtvid = new System.Windows.Forms.TextBox();
            this.pnl_serachBox = new System.Windows.Forms.Panel();
            this.tlp_search_main = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_searchbox_close = new System.Windows.Forms.Button();
            this.tlp_serachtype = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_searchtype = new System.Windows.Forms.ComboBox();
            this.tlp_serachtype_controls = new System.Windows.Forms.TableLayoutPanel();
            this.cmb_search_reason = new System.Windows.Forms.ComboBox();
            this.cmb_search_Employee = new System.Windows.Forms.ComboBox();
            this.cmb_search_payment = new System.Windows.Forms.ComboBox();
            this.lblserach_payment = new System.Windows.Forms.Label();
            this.lblserach_employee = new System.Windows.Forms.Label();
            this.lblserach_reason = new System.Windows.Forms.Label();
            this.tlp_navbar.SuspendLayout();
            this.tlp_main.SuspendLayout();
            this.tlp_button.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_expense)).BeginInit();
            this.pnl_serachBox.SuspendLayout();
            this.tlp_search_main.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tlp_serachtype.SuspendLayout();
            this.SuspendLayout();
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
            this.tlp_navbar.Size = new System.Drawing.Size(644, 31);
            this.tlp_navbar.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 24);
            this.label6.TabIndex = 12;
            this.label6.Text = "Expence ";
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(585, 3);
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
            this.btncolse.Location = new System.Drawing.Point(616, 3);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(25, 25);
            this.btncolse.TabIndex = 10;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // tlp_main
            // 
            this.tlp_main.BackColor = System.Drawing.Color.AliceBlue;
            this.tlp_main.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.OutsetDouble;
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tlp_navbar, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_button, 0, 1);
            this.tlp_main.Controls.Add(this.dgv_expense, 0, 2);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 4;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_main.Size = new System.Drawing.Size(656, 539);
            this.tlp_main.TabIndex = 13;
            // 
            // tlp_button
            // 
            this.tlp_button.ColumnCount = 9;
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 135F));
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_button.Controls.Add(this.btnSearch, 0, 0);
            this.tlp_button.Controls.Add(this.btnsave, 0, 0);
            this.tlp_button.Controls.Add(this.btnrefresh, 1, 0);
            this.tlp_button.Controls.Add(this.btnclose, 2, 0);
            this.tlp_button.Dock = System.Windows.Forms.DockStyle.Right;
            this.tlp_button.Location = new System.Drawing.Point(212, 46);
            this.tlp_button.Name = "tlp_button";
            this.tlp_button.RowCount = 1;
            this.tlp_button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_button.Size = new System.Drawing.Size(438, 39);
            this.tlp_button.TabIndex = 5;
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
            // dgv_expense
            // 
            this.dgv_expense.AllowUserToAddRows = false;
            this.dgv_expense.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_expense.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgv_expense.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_expense.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Slno,
            this.cmb_reasontype,
            this.Amount,
            this.Description,
            this.cmb_employee,
            this.cmb_paymenttype,
            this.Action,
            this.employeeid,
            this.expid,
            this.status});
            this.dgv_expense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_expense.Location = new System.Drawing.Point(6, 94);
            this.dgv_expense.Name = "dgv_expense";
            this.dgv_expense.RowHeadersVisible = false;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_expense.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_expense.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgv_expense.RowTemplate.Height = 39;
            this.dgv_expense.Size = new System.Drawing.Size(644, 439);
            this.dgv_expense.TabIndex = 18;
            this.dgv_expense.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_expense_CellContentClick);
            this.dgv_expense.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_expense_CellFormatting);
            this.dgv_expense.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_expense_EditingControlShowing);
            this.dgv_expense.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_expense_KeyDown);
            this.dgv_expense.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dgv_expense_KeyPress);
            // 
            // Slno
            // 
            this.Slno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Slno.HeaderText = "Slno";
            this.Slno.Name = "Slno";
            this.Slno.Width = 66;
            // 
            // cmb_reasontype
            // 
            this.cmb_reasontype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmb_reasontype.HeaderText = "Reason";
            this.cmb_reasontype.Name = "cmb_reasontype";
            // 
            // Amount
            // 
            this.Amount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Amount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cmb_employee
            // 
            this.cmb_employee.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmb_employee.HeaderText = "Employee Name";
            this.cmb_employee.Name = "cmb_employee";
            // 
            // cmb_paymenttype
            // 
            this.cmb_paymenttype.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cmb_paymenttype.HeaderText = "Payment Type";
            this.cmb_paymenttype.Name = "cmb_paymenttype";
            // 
            // Action
            // 
            this.Action.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle5.NullValue")));
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.Action.DefaultCellStyle = dataGridViewCellStyle5;
            this.Action.HeaderText = "Action";
            this.Action.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Action.Name = "Action";
            this.Action.Width = 60;
            // 
            // employeeid
            // 
            this.employeeid.HeaderText = "employeeid";
            this.employeeid.Name = "employeeid";
            this.employeeid.Visible = false;
            // 
            // expid
            // 
            this.expid.HeaderText = "Expenceid";
            this.expid.Name = "expid";
            this.expid.Visible = false;
            // 
            // status
            // 
            this.status.HeaderText = "status";
            this.status.Name = "status";
            this.status.Visible = false;
            // 
            // txtvid
            // 
            this.txtvid.Location = new System.Drawing.Point(359, 27);
            this.txtvid.Name = "txtvid";
            this.txtvid.Size = new System.Drawing.Size(13, 20);
            this.txtvid.TabIndex = 12;
            // 
            // pnl_serachBox
            // 
            this.pnl_serachBox.Controls.Add(this.tlp_search_main);
            this.pnl_serachBox.Location = new System.Drawing.Point(26, 145);
            this.pnl_serachBox.Name = "pnl_serachBox";
            this.pnl_serachBox.Size = new System.Drawing.Size(606, 178);
            this.pnl_serachBox.TabIndex = 19;
            this.pnl_serachBox.Visible = false;
            // 
            // tlp_search_main
            // 
            this.tlp_search_main.ColumnCount = 1;
            this.tlp_search_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_search_main.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlp_search_main.Controls.Add(this.tlp_serachtype, 0, 1);
            this.tlp_search_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_search_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_search_main.Name = "tlp_search_main";
            this.tlp_search_main.RowCount = 2;
            this.tlp_search_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_search_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_search_main.Size = new System.Drawing.Size(606, 178);
            this.tlp_search_main.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.SteelBlue;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_searchbox_close, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(600, 31);
            this.tableLayoutPanel1.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 24);
            this.label1.TabIndex = 12;
            this.label1.Text = "Serach Box";
            // 
            // btn_searchbox_close
            // 
            this.btn_searchbox_close.BackColor = System.Drawing.Color.White;
            this.btn_searchbox_close.FlatAppearance.BorderSize = 0;
            this.btn_searchbox_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_searchbox_close.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_searchbox_close.ForeColor = System.Drawing.Color.Red;
            this.btn_searchbox_close.Location = new System.Drawing.Point(572, 3);
            this.btn_searchbox_close.Name = "btn_searchbox_close";
            this.btn_searchbox_close.Size = new System.Drawing.Size(25, 25);
            this.btn_searchbox_close.TabIndex = 10;
            this.btn_searchbox_close.Text = "X";
            this.btn_searchbox_close.UseVisualStyleBackColor = false;
            this.btn_searchbox_close.Click += new System.EventHandler(this.btn_searchbox_close_Click);
            // 
            // tlp_serachtype
            // 
            this.tlp_serachtype.ColumnCount = 2;
            this.tlp_serachtype.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.83333F));
            this.tlp_serachtype.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.16666F));
            this.tlp_serachtype.Controls.Add(this.label2, 0, 0);
            this.tlp_serachtype.Controls.Add(this.cmb_searchtype, 1, 0);
            this.tlp_serachtype.Controls.Add(this.tlp_serachtype_controls, 1, 1);
            this.tlp_serachtype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_serachtype.Location = new System.Drawing.Point(3, 40);
            this.tlp_serachtype.Name = "tlp_serachtype";
            this.tlp_serachtype.RowCount = 2;
            this.tlp_serachtype.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.37209F));
            this.tlp_serachtype.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.62791F));
            this.tlp_serachtype.Size = new System.Drawing.Size(600, 135);
            this.tlp_serachtype.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 51);
            this.label2.TabIndex = 0;
            this.label2.Text = "Serach Type";
            // 
            // cmb_searchtype
            // 
            this.cmb_searchtype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.cmb_searchtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_searchtype.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_searchtype.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_searchtype.FormattingEnabled = true;
            this.cmb_searchtype.Items.AddRange(new object[] {
            "Reason Type",
            "Amount",
            "Employee Name",
            "Payment Type"});
            this.cmb_searchtype.Location = new System.Drawing.Point(121, 3);
            this.cmb_searchtype.Name = "cmb_searchtype";
            this.cmb_searchtype.Size = new System.Drawing.Size(206, 24);
            this.cmb_searchtype.TabIndex = 1;
            this.cmb_searchtype.SelectedIndexChanged += new System.EventHandler(this.cmb_searchtype_SelectedIndexChanged);
            // 
            // tlp_serachtype_controls
            // 
            this.tlp_serachtype_controls.ColumnCount = 2;
            this.tlp_serachtype_controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_serachtype_controls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_serachtype_controls.Location = new System.Drawing.Point(121, 54);
            this.tlp_serachtype_controls.Name = "tlp_serachtype_controls";
            this.tlp_serachtype_controls.RowCount = 1;
            this.tlp_serachtype_controls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_serachtype_controls.Size = new System.Drawing.Size(475, 69);
            this.tlp_serachtype_controls.TabIndex = 2;
            // 
            // cmb_search_reason
            // 
            this.cmb_search_reason.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.cmb_search_reason.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_search_reason.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_search_reason.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_search_reason.FormattingEnabled = true;
            this.cmb_search_reason.Items.AddRange(new object[] {
            "Reason Type",
            "Amount",
            "Employee Name",
            "Payment Type"});
            this.cmb_search_reason.Location = new System.Drawing.Point(0, 0);
            this.cmb_search_reason.Name = "cmb_search_reason";
            this.cmb_search_reason.Size = new System.Drawing.Size(112, 24);
            this.cmb_search_reason.TabIndex = 3;
            // 
            // cmb_search_Employee
            // 
            this.cmb_search_Employee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.cmb_search_Employee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_search_Employee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_search_Employee.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_search_Employee.FormattingEnabled = true;
            this.cmb_search_Employee.Items.AddRange(new object[] {
            "Reason Type",
            "Amount",
            "Employee Name",
            "Payment Type"});
            this.cmb_search_Employee.Location = new System.Drawing.Point(272, 4);
            this.cmb_search_Employee.Name = "cmb_search_Employee";
            this.cmb_search_Employee.Size = new System.Drawing.Size(112, 24);
            this.cmb_search_Employee.TabIndex = 20;
            // 
            // cmb_search_payment
            // 
            this.cmb_search_payment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.cmb_search_payment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_search_payment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmb_search_payment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_search_payment.FormattingEnabled = true;
            this.cmb_search_payment.Items.AddRange(new object[] {
            "Reason Type",
            "Amount",
            "Employee Name",
            "Payment Type"});
            this.cmb_search_payment.Location = new System.Drawing.Point(122, 24);
            this.cmb_search_payment.Name = "cmb_search_payment";
            this.cmb_search_payment.Size = new System.Drawing.Size(87, 24);
            this.cmb_search_payment.TabIndex = 21;
            // 
            // lblserach_payment
            // 
            this.lblserach_payment.AutoSize = true;
            this.lblserach_payment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblserach_payment.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblserach_payment.Location = new System.Drawing.Point(0, 0);
            this.lblserach_payment.Name = "lblserach_payment";
            this.lblserach_payment.Size = new System.Drawing.Size(102, 18);
            this.lblserach_payment.TabIndex = 22;
            this.lblserach_payment.Text = "Serach Type";
            // 
            // lblserach_employee
            // 
            this.lblserach_employee.AutoSize = true;
            this.lblserach_employee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblserach_employee.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblserach_employee.Location = new System.Drawing.Point(0, 0);
            this.lblserach_employee.Name = "lblserach_employee";
            this.lblserach_employee.Size = new System.Drawing.Size(102, 18);
            this.lblserach_employee.TabIndex = 23;
            this.lblserach_employee.Text = "Serach Type";
            // 
            // lblserach_reason
            // 
            this.lblserach_reason.AutoSize = true;
            this.lblserach_reason.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblserach_reason.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblserach_reason.Location = new System.Drawing.Point(0, 0);
            this.lblserach_reason.Name = "lblserach_reason";
            this.lblserach_reason.Size = new System.Drawing.Size(102, 18);
            this.lblserach_reason.TabIndex = 24;
            this.lblserach_reason.Text = "Serach Type";
            // 
            // FrmExpence
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 539);
            this.Controls.Add(this.lblserach_reason);
            this.Controls.Add(this.lblserach_employee);
            this.Controls.Add(this.lblserach_payment);
            this.Controls.Add(this.cmb_search_reason);
            this.Controls.Add(this.cmb_search_Employee);
            this.Controls.Add(this.cmb_search_payment);
            this.Controls.Add(this.pnl_serachBox);
            this.Controls.Add(this.tlp_main);
            this.Controls.Add(this.txtvid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmExpence";
            this.Text = "Frmwithdrawal";
            this.Load += new System.EventHandler(this.FrmExpence_Load);
            this.tlp_navbar.ResumeLayout(false);
            this.tlp_navbar.PerformLayout();
            this.tlp_main.ResumeLayout(false);
            this.tlp_button.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_expense)).EndInit();
            this.pnl_serachBox.ResumeLayout(false);
            this.tlp_search_main.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tlp_serachtype.ResumeLayout(false);
            this.tlp_serachtype.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_navbar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TableLayoutPanel tlp_button;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.TextBox txtvid;
        private System.Windows.Forms.DataGridView dgv_expense;
        private System.Windows.Forms.DataGridViewTextBoxColumn Slno;
        private System.Windows.Forms.DataGridViewComboBoxColumn cmb_reasontype;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewComboBoxColumn cmb_employee;
        private System.Windows.Forms.DataGridViewComboBoxColumn cmb_paymenttype;
        private System.Windows.Forms.DataGridViewImageColumn Action;
        private System.Windows.Forms.DataGridViewTextBoxColumn employeeid;
        private System.Windows.Forms.DataGridViewTextBoxColumn expid;
        private System.Windows.Forms.DataGridViewCheckBoxColumn status;
        private System.Windows.Forms.Panel pnl_serachBox;
        private System.Windows.Forms.TableLayoutPanel tlp_search_main;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_searchbox_close;
        private System.Windows.Forms.TableLayoutPanel tlp_serachtype;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_searchtype;
        private System.Windows.Forms.TableLayoutPanel tlp_serachtype_controls;
        private System.Windows.Forms.ComboBox cmb_search_reason;
        private System.Windows.Forms.ComboBox cmb_search_Employee;
        private System.Windows.Forms.ComboBox cmb_search_payment;
        private System.Windows.Forms.Label lblserach_payment;
        private System.Windows.Forms.Label lblserach_employee;
        private System.Windows.Forms.Label lblserach_reason;
    }
}