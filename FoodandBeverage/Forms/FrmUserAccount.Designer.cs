
namespace FoodandBeverage.Forms.UserAccount
{
    partial class FrmUserAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserAccount));
            this.chkshowpassword = new System.Windows.Forms.CheckBox();
            this.chkstatus = new System.Windows.Forms.CheckBox();
            this.txtuname = new System.Windows.Forms.TextBox();
            this.txtpassword = new System.Windows.Forms.TextBox();
            this.txtcconpassword = new System.Windows.Forms.TextBox();
            this.tlp_header = new System.Windows.Forms.TableLayoutPanel();
            this.btncolse = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.tlp_reg = new System.Windows.Forms.TableLayoutPanel();
            this.mcmbaccounttype = new System.Windows.Forms.ComboBox();
            this.txtemp = new System.Windows.Forms.TextBox();
            this.lblToolTipAccName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tlp_buttons = new System.Windows.Forms.TableLayoutPanel();
            this.mbtnClose = new System.Windows.Forms.Button();
            this.mbtnList = new System.Windows.Forms.Button();
            this.mbtnRefresh = new System.Windows.Forms.Button();
            this.mbtnSave = new System.Windows.Forms.Button();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_design = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_header.SuspendLayout();
            this.tlp_reg.SuspendLayout();
            this.tlp_buttons.SuspendLayout();
            this.tlp_main.SuspendLayout();
            this.tlp_design.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkshowpassword
            // 
            this.chkshowpassword.AutoSize = true;
            this.chkshowpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.chkshowpassword.ForeColor = System.Drawing.Color.Black;
            this.chkshowpassword.Location = new System.Drawing.Point(297, 134);
            this.chkshowpassword.Name = "chkshowpassword";
            this.chkshowpassword.Size = new System.Drawing.Size(125, 21);
            this.chkshowpassword.TabIndex = 234;
            this.chkshowpassword.Text = "Show password";
            this.chkshowpassword.UseVisualStyleBackColor = true;
            this.chkshowpassword.CheckedChanged += new System.EventHandler(this.chkshowpassword_CheckedChanged);
            // 
            // chkstatus
            // 
            this.chkstatus.AutoSize = true;
            this.chkstatus.Checked = true;
            this.chkstatus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkstatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.chkstatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.chkstatus.ForeColor = System.Drawing.Color.Black;
            this.chkstatus.Location = new System.Drawing.Point(120, 220);
            this.chkstatus.Name = "chkstatus";
            this.chkstatus.Size = new System.Drawing.Size(171, 21);
            this.chkstatus.TabIndex = 227;
            this.chkstatus.Text = "IsActive";
            this.chkstatus.UseVisualStyleBackColor = true;
            // 
            // txtuname
            // 
            this.txtuname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtuname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtuname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtuname.Location = new System.Drawing.Point(120, 91);
            this.txtuname.Multiline = true;
            this.txtuname.Name = "txtuname";
            this.txtuname.Size = new System.Drawing.Size(171, 37);
            this.txtuname.TabIndex = 215;
            this.txtuname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtuname_KeyDown);
            // 
            // txtpassword
            // 
            this.txtpassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtpassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtpassword.Location = new System.Drawing.Point(120, 134);
            this.txtpassword.Multiline = true;
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.PasswordChar = '*';
            this.txtpassword.Size = new System.Drawing.Size(171, 37);
            this.txtpassword.TabIndex = 216;
            this.txtpassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpassword_KeyDown);
            // 
            // txtcconpassword
            // 
            this.txtcconpassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtcconpassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtcconpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtcconpassword.Location = new System.Drawing.Point(120, 177);
            this.txtcconpassword.Multiline = true;
            this.txtcconpassword.Name = "txtcconpassword";
            this.txtcconpassword.PasswordChar = '*';
            this.txtcconpassword.Size = new System.Drawing.Size(171, 37);
            this.txtcconpassword.TabIndex = 217;
            this.txtcconpassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcconpassword_KeyDown);
            this.txtcconpassword.Leave += new System.EventHandler(this.txtcconpassword_Leave);
            // 
            // tlp_header
            // 
            this.tlp_header.BackColor = System.Drawing.Color.SteelBlue;
            this.tlp_header.ColumnCount = 2;
            this.tlp_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlp_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_header.Controls.Add(this.btncolse, 0, 0);
            this.tlp_header.Controls.Add(this.label21, 0, 0);
            this.tlp_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlp_header.Location = new System.Drawing.Point(5, 5);
            this.tlp_header.Name = "tlp_header";
            this.tlp_header.RowCount = 1;
            this.tlp_header.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_header.Size = new System.Drawing.Size(472, 30);
            this.tlp_header.TabIndex = 243;
            // 
            // btncolse
            // 
            this.btncolse.BackColor = System.Drawing.Color.White;
            this.btncolse.BackgroundImage = global::FoodandBeverage.Properties.Resources.close_price_change;
            this.btncolse.FlatAppearance.BorderSize = 0;
            this.btncolse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncolse.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncolse.ForeColor = System.Drawing.Color.Red;
            this.btncolse.Location = new System.Drawing.Point(445, 3);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(24, 24);
            this.btncolse.TabIndex = 12;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(3, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(436, 30);
            this.label21.TabIndex = 7;
            this.label21.Text = "User Account";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlp_reg
            // 
            this.tlp_reg.BackColor = System.Drawing.Color.White;
            this.tlp_reg.ColumnCount = 3;
            this.tlp_reg.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.31579F));
            this.tlp_reg.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.69298F));
            this.tlp_reg.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.77193F));
            this.tlp_reg.Controls.Add(this.txtuname, 1, 2);
            this.tlp_reg.Controls.Add(this.txtpassword, 1, 3);
            this.tlp_reg.Controls.Add(this.chkshowpassword, 2, 3);
            this.tlp_reg.Controls.Add(this.txtcconpassword, 1, 4);
            this.tlp_reg.Controls.Add(this.mcmbaccounttype, 1, 0);
            this.tlp_reg.Controls.Add(this.txtemp, 1, 1);
            this.tlp_reg.Controls.Add(this.lblToolTipAccName, 2, 1);
            this.tlp_reg.Controls.Add(this.chkstatus, 1, 5);
            this.tlp_reg.Controls.Add(this.label1, 0, 0);
            this.tlp_reg.Controls.Add(this.label2, 0, 1);
            this.tlp_reg.Controls.Add(this.label3, 0, 2);
            this.tlp_reg.Controls.Add(this.label4, 0, 3);
            this.tlp_reg.Controls.Add(this.label5, 0, 4);
            this.tlp_reg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_reg.Location = new System.Drawing.Point(12, 3);
            this.tlp_reg.Name = "tlp_reg";
            this.tlp_reg.RowCount = 6;
            this.tlp_reg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.97711F));
            this.tlp_reg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.18486F));
            this.tlp_reg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.18486F));
            this.tlp_reg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.18486F));
            this.tlp_reg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.18486F));
            this.tlp_reg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.28344F));
            this.tlp_reg.Size = new System.Drawing.Size(447, 287);
            this.tlp_reg.TabIndex = 243;
            // 
            // mcmbaccounttype
            // 
            this.mcmbaccounttype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.mcmbaccounttype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mcmbaccounttype.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mcmbaccounttype.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mcmbaccounttype.FormattingEnabled = true;
            this.mcmbaccounttype.Location = new System.Drawing.Point(120, 3);
            this.mcmbaccounttype.Name = "mcmbaccounttype";
            this.mcmbaccounttype.Size = new System.Drawing.Size(171, 28);
            this.mcmbaccounttype.TabIndex = 241;
            this.mcmbaccounttype.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbaccounttype_KeyDown);
            // 
            // txtemp
            // 
            this.txtemp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtemp.Location = new System.Drawing.Point(120, 48);
            this.txtemp.Multiline = true;
            this.txtemp.Name = "txtemp";
            this.txtemp.ReadOnly = true;
            this.txtemp.Size = new System.Drawing.Size(171, 37);
            this.txtemp.TabIndex = 214;
            this.txtemp.Enter += new System.EventHandler(this.txtemp_Enter);
            this.txtemp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtemp_KeyDown);
            this.txtemp.Leave += new System.EventHandler(this.txtemp_Leave);
            // 
            // lblToolTipAccName
            // 
            this.lblToolTipAccName.AutoSize = true;
            this.lblToolTipAccName.BackColor = System.Drawing.Color.Transparent;
            this.lblToolTipAccName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblToolTipAccName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblToolTipAccName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToolTipAccName.ForeColor = System.Drawing.Color.Red;
            this.lblToolTipAccName.Location = new System.Drawing.Point(297, 45);
            this.lblToolTipAccName.Name = "lblToolTipAccName";
            this.lblToolTipAccName.Size = new System.Drawing.Size(147, 43);
            this.lblToolTipAccName.TabIndex = 392;
            this.lblToolTipAccName.Text = "[F8]-For Account List";
            this.lblToolTipAccName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblToolTipAccName.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 45);
            this.label1.TabIndex = 393;
            this.label1.Text = "User Type ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label2.Location = new System.Drawing.Point(3, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 43);
            this.label2.TabIndex = 394;
            this.label2.Text = "Employee  ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label3.Location = new System.Drawing.Point(3, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 43);
            this.label3.TabIndex = 395;
            this.label3.Text = "User Name  ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label4.Location = new System.Drawing.Point(3, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 43);
            this.label4.TabIndex = 396;
            this.label4.Text = "Password  ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label5.Location = new System.Drawing.Point(3, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 43);
            this.label5.TabIndex = 397;
            this.label5.Text = "Confirm  ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlp_buttons
            // 
            this.tlp_buttons.BackColor = System.Drawing.Color.Transparent;
            this.tlp_buttons.ColumnCount = 5;
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_buttons.Controls.Add(this.mbtnClose, 4, 0);
            this.tlp_buttons.Controls.Add(this.mbtnList, 3, 0);
            this.tlp_buttons.Controls.Add(this.mbtnRefresh, 2, 0);
            this.tlp_buttons.Controls.Add(this.mbtnSave, 1, 0);
            this.tlp_buttons.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlp_buttons.Location = new System.Drawing.Point(5, 361);
            this.tlp_buttons.Name = "tlp_buttons";
            this.tlp_buttons.RowCount = 1;
            this.tlp_buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_buttons.Size = new System.Drawing.Size(472, 39);
            this.tlp_buttons.TabIndex = 244;
            // 
            // mbtnClose
            // 
            this.mbtnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbtnClose.BackgroundImage")));
            this.mbtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbtnClose.FlatAppearance.BorderSize = 0;
            this.mbtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnClose.Location = new System.Drawing.Point(393, 3);
            this.mbtnClose.Name = "mbtnClose";
            this.mbtnClose.Size = new System.Drawing.Size(76, 33);
            this.mbtnClose.TabIndex = 239;
            this.mbtnClose.UseVisualStyleBackColor = true;
            this.mbtnClose.Click += new System.EventHandler(this.mbtnClose_Click);
            // 
            // mbtnList
            // 
            this.mbtnList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbtnList.BackgroundImage")));
            this.mbtnList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbtnList.FlatAppearance.BorderSize = 0;
            this.mbtnList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnList.Location = new System.Drawing.Point(309, 3);
            this.mbtnList.Name = "mbtnList";
            this.mbtnList.Size = new System.Drawing.Size(78, 33);
            this.mbtnList.TabIndex = 237;
            this.mbtnList.UseVisualStyleBackColor = true;
            this.mbtnList.Click += new System.EventHandler(this.mbtnList_Click);
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mbtnRefresh.BackgroundImage")));
            this.mbtnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnRefresh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbtnRefresh.FlatAppearance.BorderSize = 0;
            this.mbtnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnRefresh.Location = new System.Drawing.Point(232, 3);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(71, 33);
            this.mbtnRefresh.TabIndex = 238;
            this.mbtnRefresh.UseVisualStyleBackColor = true;
            this.mbtnRefresh.Click += new System.EventHandler(this.mbtnRefresh_Click);
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackgroundImage = global::FoodandBeverage.Properties.Resources.SaveButton;
            this.mbtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbtnSave.FlatAppearance.BorderSize = 0;
            this.mbtnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnSave.Location = new System.Drawing.Point(149, 3);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(77, 33);
            this.mbtnSave.TabIndex = 241;
            this.mbtnSave.UseVisualStyleBackColor = true;
            this.mbtnSave.Click += new System.EventHandler(this.mbtnSave_Click);
            // 
            // tlp_main
            // 
            this.tlp_main.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tlp_design, 0, 1);
            this.tlp_main.Controls.Add(this.tlp_buttons, 0, 2);
            this.tlp_main.Controls.Add(this.tlp_header, 0, 0);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 3;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_main.Size = new System.Drawing.Size(482, 408);
            this.tlp_main.TabIndex = 245;
            // 
            // tlp_design
            // 
            this.tlp_design.ColumnCount = 3;
            this.tlp_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.960784F));
            this.tlp_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 96.07843F));
            this.tlp_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 1.960784F));
            this.tlp_design.Controls.Add(this.tlp_reg, 1, 0);
            this.tlp_design.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_design.Location = new System.Drawing.Point(5, 43);
            this.tlp_design.Name = "tlp_design";
            this.tlp_design.RowCount = 2;
            this.tlp_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.83871F));
            this.tlp_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.16129F));
            this.tlp_design.Size = new System.Drawing.Size(472, 310);
            this.tlp_design.TabIndex = 246;
            // 
            // FrmUserAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(482, 408);
            this.Controls.Add(this.tlp_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmUserAccount";
            this.Text = "FrmUserAccount";
            this.Load += new System.EventHandler(this.FrmUserAccount_Load);
            this.Resize += new System.EventHandler(this.FrmUserAccount_Resize);
            this.tlp_header.ResumeLayout(false);
            this.tlp_header.PerformLayout();
            this.tlp_reg.ResumeLayout(false);
            this.tlp_reg.PerformLayout();
            this.tlp_buttons.ResumeLayout(false);
            this.tlp_main.ResumeLayout(false);
            this.tlp_design.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkshowpassword;
        private System.Windows.Forms.CheckBox chkstatus;
        private System.Windows.Forms.TextBox txtuname;
        private System.Windows.Forms.TextBox txtpassword;
        private System.Windows.Forms.TextBox txtcconpassword;
        private System.Windows.Forms.Button mbtnRefresh;
       
        private System.Windows.Forms.Button mbtnSave;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TableLayoutPanel tlp_header;
        private System.Windows.Forms.TableLayoutPanel tlp_reg;
        private System.Windows.Forms.TableLayoutPanel tlp_buttons;
        private System.Windows.Forms.TextBox txtemp;
        private System.Windows.Forms.ComboBox mcmbaccounttype;
        private System.Windows.Forms.Button mbtnClose;
        private System.Windows.Forms.Button mbtnList;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TableLayoutPanel tlp_design;
        private System.Windows.Forms.Label lblToolTipAccName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}