
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.mcmbaccounttype = new System.Windows.Forms.ComboBox();
            this.txtemp = new System.Windows.Forms.TextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.lblToolTipAccName = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.mbtnClose = new System.Windows.Forms.Button();
            this.mbtnList = new System.Windows.Forms.Button();
            this.mbtnRefresh = new System.Windows.Forms.Button();
            this.mbtnSave = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkshowpassword
            // 
            this.chkshowpassword.AutoSize = true;
            this.chkshowpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.chkshowpassword.ForeColor = System.Drawing.Color.Black;
            this.chkshowpassword.Location = new System.Drawing.Point(358, 94);
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
            this.chkstatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkstatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.chkstatus.ForeColor = System.Drawing.Color.Black;
            this.chkstatus.Location = new System.Drawing.Point(3, 3);
            this.chkstatus.Name = "chkstatus";
            this.chkstatus.Size = new System.Drawing.Size(84, 21);
            this.chkstatus.TabIndex = 227;
            this.chkstatus.Text = "IsActive";
            this.chkstatus.UseVisualStyleBackColor = true;
            // 
            // txtuname
            // 
            this.txtuname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtuname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtuname.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtuname.Location = new System.Drawing.Point(131, 64);
            this.txtuname.Multiline = true;
            this.txtuname.Name = "txtuname";
            this.txtuname.Size = new System.Drawing.Size(221, 24);
            this.txtuname.TabIndex = 215;
            this.txtuname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtuname_KeyDown);
            // 
            // txtpassword
            // 
            this.txtpassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtpassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtpassword.Location = new System.Drawing.Point(131, 94);
            this.txtpassword.Multiline = true;
            this.txtpassword.Name = "txtpassword";
            this.txtpassword.PasswordChar = '*';
            this.txtpassword.Size = new System.Drawing.Size(221, 24);
            this.txtpassword.TabIndex = 216;
            this.txtpassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtpassword_KeyDown);
            // 
            // txtcconpassword
            // 
            this.txtcconpassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtcconpassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtcconpassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtcconpassword.Location = new System.Drawing.Point(131, 124);
            this.txtcconpassword.Multiline = true;
            this.txtcconpassword.Name = "txtcconpassword";
            this.txtcconpassword.PasswordChar = '*';
            this.txtcconpassword.Size = new System.Drawing.Size(221, 24);
            this.txtcconpassword.TabIndex = 217;
            this.txtcconpassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcconpassword_KeyDown);
            this.txtcconpassword.Leave += new System.EventHandler(this.txtcconpassword_Leave);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.SteelBlue;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.882591F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 93.11741F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox10, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label21, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(526, 30);
            this.tableLayoutPanel1.TabIndex = 243;
            // 
            // pictureBox10
            // 
            this.pictureBox10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox10.BackgroundImage")));
            this.pictureBox10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox10.Location = new System.Drawing.Point(3, 3);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(27, 24);
            this.pictureBox10.TabIndex = 279;
            this.pictureBox10.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(493, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 18);
            this.label4.TabIndex = 8;
            this.label4.Text = "X";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(36, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(136, 24);
            this.label21.TabIndex = 7;
            this.label21.Text = "User Account";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.richTextBox1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtuname, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.txtpassword, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.chkshowpassword, 2, 3);
            this.tableLayoutPanel2.Controls.Add(this.txtcconpassword, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.mcmbaccounttype, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtemp, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.richTextBox2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.richTextBox3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.richTextBox4, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.richTextBox5, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 1, 5);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 30);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.94203F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.15152F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.15152F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.15152F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15.15152F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.23232F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(526, 198);
            this.tableLayoutPanel2.TabIndex = 243;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(122, 25);
            this.richTextBox1.TabIndex = 385;
            this.richTextBox1.Text = "   User Type *";
            // 
            // mcmbaccounttype
            // 
            this.mcmbaccounttype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.mcmbaccounttype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mcmbaccounttype.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mcmbaccounttype.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mcmbaccounttype.FormattingEnabled = true;
            this.mcmbaccounttype.Location = new System.Drawing.Point(131, 3);
            this.mcmbaccounttype.Name = "mcmbaccounttype";
            this.mcmbaccounttype.Size = new System.Drawing.Size(221, 23);
            this.mcmbaccounttype.TabIndex = 241;
            this.mcmbaccounttype.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbaccounttype_KeyDown);
            // 
            // txtemp
            // 
            this.txtemp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txtemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtemp.Location = new System.Drawing.Point(131, 34);
            this.txtemp.Multiline = true;
            this.txtemp.Name = "txtemp";
            this.txtemp.ReadOnly = true;
            this.txtemp.Size = new System.Drawing.Size(221, 24);
            this.txtemp.TabIndex = 214;
            this.txtemp.Enter += new System.EventHandler(this.txtemp_Enter);
            this.txtemp.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtemp_KeyDown);
            this.txtemp.Leave += new System.EventHandler(this.txtemp_Leave);
            // 
            // richTextBox2
            // 
            this.richTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(3, 34);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox2.Size = new System.Drawing.Size(122, 24);
            this.richTextBox2.TabIndex = 386;
            this.richTextBox2.Text = "   Employee  *";
            // 
            // richTextBox3
            // 
            this.richTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox3.Location = new System.Drawing.Point(3, 64);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox3.Size = new System.Drawing.Size(122, 24);
            this.richTextBox3.TabIndex = 387;
            this.richTextBox3.Text = "User Name  *";
            // 
            // richTextBox4
            // 
            this.richTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox4.Location = new System.Drawing.Point(3, 94);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox4.Size = new System.Drawing.Size(122, 24);
            this.richTextBox4.TabIndex = 388;
            this.richTextBox4.Text = "     Password  *";
            // 
            // richTextBox5
            // 
            this.richTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox5.Location = new System.Drawing.Point(3, 124);
            this.richTextBox5.Name = "richTextBox5";
            this.richTextBox5.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox5.Size = new System.Drawing.Size(122, 24);
            this.richTextBox5.TabIndex = 389;
            this.richTextBox5.Text = "         Confirm  *";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.30952F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.69048F));
            this.tableLayoutPanel4.Controls.Add(this.lblToolTipAccName, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(358, 34);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(168, 24);
            this.tableLayoutPanel4.TabIndex = 390;
            // 
            // lblToolTipAccName
            // 
            this.lblToolTipAccName.AutoSize = true;
            this.lblToolTipAccName.BackColor = System.Drawing.Color.Transparent;
            this.lblToolTipAccName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblToolTipAccName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblToolTipAccName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToolTipAccName.ForeColor = System.Drawing.Color.Red;
            this.lblToolTipAccName.Location = new System.Drawing.Point(3, 0);
            this.lblToolTipAccName.Name = "lblToolTipAccName";
            this.lblToolTipAccName.Size = new System.Drawing.Size(138, 16);
            this.lblToolTipAccName.TabIndex = 228;
            this.lblToolTipAccName.Text = "[F8]-For Account List";
            this.lblToolTipAccName.Visible = false;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59F));
            this.tableLayoutPanel5.Controls.Add(this.chkstatus, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(131, 154);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 65.85366F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.14634F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(221, 41);
            this.tableLayoutPanel5.TabIndex = 391;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.mbtnClose, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.mbtnList, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.mbtnRefresh, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.mbtnSave, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 231);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(526, 36);
            this.tableLayoutPanel3.TabIndex = 244;
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
            this.mbtnClose.Location = new System.Drawing.Point(447, 3);
            this.mbtnClose.Name = "mbtnClose";
            this.mbtnClose.Size = new System.Drawing.Size(76, 30);
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
            this.mbtnList.Location = new System.Drawing.Point(363, 3);
            this.mbtnList.Name = "mbtnList";
            this.mbtnList.Size = new System.Drawing.Size(78, 30);
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
            this.mbtnRefresh.Location = new System.Drawing.Point(286, 3);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(71, 30);
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
            this.mbtnSave.Location = new System.Drawing.Point(203, 3);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(77, 30);
            this.mbtnSave.TabIndex = 241;
            this.mbtnSave.UseVisualStyleBackColor = true;
            this.mbtnSave.Click += new System.EventHandler(this.mbtnSave_Click);
            // 
            // FrmUserAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(526, 267);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmUserAccount";
            this.Text = "FrmUserAccount";
            this.Load += new System.EventHandler(this.FrmUserAccount_Load);
            this.Resize += new System.EventHandler(this.FrmUserAccount_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox txtemp;
        private System.Windows.Forms.Label lblToolTipAccName;
        private System.Windows.Forms.ComboBox mcmbaccounttype;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.RichTextBox richTextBox5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button mbtnClose;
        private System.Windows.Forms.Button mbtnList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    }
}