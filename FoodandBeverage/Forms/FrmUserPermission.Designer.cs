
namespace FoodandBeverage.Forms
{
    partial class FrmUserPermission
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserPermission));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.label23 = new System.Windows.Forms.Label();
            this.btncolse = new System.Windows.Forms.Button();
            this.btnmini = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserTypeNote = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbUsreAccount = new System.Windows.Forms.ComboBox();
            this.btnaddusertype = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvforms = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Form = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Write = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Read = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlusertype = new System.Windows.Forms.Panel();
            this.btnusertype = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblsubcatclose = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.txtusertype = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.txtdescr = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvforms)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlusertype.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.SteelBlue;
            this.panel5.Controls.Add(this.pictureBox10);
            this.panel5.Controls.Add(this.label23);
            this.panel5.Controls.Add(this.btncolse);
            this.panel5.Controls.Add(this.btnmini);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(820, 59);
            this.panel5.TabIndex = 276;
            // 
            // pictureBox10
            // 
            this.pictureBox10.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox10.BackgroundImage")));
            this.pictureBox10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox10.Location = new System.Drawing.Point(10, 17);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(38, 23);
            this.pictureBox10.TabIndex = 278;
            this.pictureBox10.TabStop = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Elephant", 14.25F, System.Drawing.FontStyle.Bold);
            this.label23.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label23.Location = new System.Drawing.Point(50, 17);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(183, 25);
            this.label23.TabIndex = 277;
            this.label23.Text = "User Permission";
            // 
            // btncolse
            // 
            this.btncolse.BackColor = System.Drawing.Color.White;
            this.btncolse.FlatAppearance.BorderSize = 0;
            this.btncolse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncolse.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncolse.ForeColor = System.Drawing.Color.Red;
            this.btncolse.Location = new System.Drawing.Point(756, 17);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(25, 25);
            this.btncolse.TabIndex = 11;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(725, 17);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(25, 25);
            this.btnmini.TabIndex = 10;
            this.btnmini.Text = "-";
            this.btnmini.UseVisualStyleBackColor = false;
            this.btnmini.Click += new System.EventHandler(this.btnmini_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(415, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 22);
            this.label3.TabIndex = 280;
            this.label3.Text = "Note  :";
            // 
            // txtUserTypeNote
            // 
            this.txtUserTypeNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserTypeNote.Location = new System.Drawing.Point(505, 93);
            this.txtUserTypeNote.Multiline = true;
            this.txtUserTypeNote.Name = "txtUserTypeNote";
            this.txtUserTypeNote.Size = new System.Drawing.Size(199, 76);
            this.txtUserTypeNote.TabIndex = 279;
            this.txtUserTypeNote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserTypeNote_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 14.25F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 22);
            this.label2.TabIndex = 278;
            this.label2.Text = "Access Type :";
            // 
            // cmbUsreAccount
            // 
            this.cmbUsreAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsreAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbUsreAccount.FormattingEnabled = true;
            this.cmbUsreAccount.Location = new System.Drawing.Point(146, 93);
            this.cmbUsreAccount.Name = "cmbUsreAccount";
            this.cmbUsreAccount.Size = new System.Drawing.Size(199, 26);
            this.cmbUsreAccount.TabIndex = 277;
            this.cmbUsreAccount.SelectionChangeCommitted += new System.EventHandler(this.cmbUsreAccount_SelectionChangeCommitted);
            this.cmbUsreAccount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUsreAccount_KeyDown);
            // 
            // btnaddusertype
            // 
            this.btnaddusertype.BackColor = System.Drawing.Color.Transparent;
            this.btnaddusertype.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnaddusertype.BackgroundImage")));
            this.btnaddusertype.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnaddusertype.FlatAppearance.BorderSize = 0;
            this.btnaddusertype.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnaddusertype.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnaddusertype.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnaddusertype.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaddusertype.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaddusertype.Location = new System.Drawing.Point(351, 94);
            this.btnaddusertype.Name = "btnaddusertype";
            this.btnaddusertype.Size = new System.Drawing.Size(27, 26);
            this.btnaddusertype.TabIndex = 283;
            this.btnaddusertype.UseVisualStyleBackColor = false;
            this.btnaddusertype.Click += new System.EventHandler(this.btnaddusertype_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvforms);
            this.panel1.Location = new System.Drawing.Point(0, 207);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(820, 284);
            this.panel1.TabIndex = 284;
            // 
            // dgvforms
            // 
            this.dgvforms.AllowUserToAddRows = false;
            this.dgvforms.AllowUserToDeleteRows = false;
            this.dgvforms.ColumnHeadersHeight = 35;
            this.dgvforms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.formid,
            this.Form,
            this.Write,
            this.Read});
            this.dgvforms.Location = new System.Drawing.Point(0, 3);
            this.dgvforms.Name = "dgvforms";
            this.dgvforms.RowHeadersVisible = false;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvforms.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvforms.RowTemplate.Height = 35;
            this.dgvforms.Size = new System.Drawing.Size(817, 284);
            this.dgvforms.TabIndex = 223;
            this.dgvforms.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvforms_CellEndEdit);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // formid
            // 
            this.formid.HeaderText = "formid";
            this.formid.Name = "formid";
            this.formid.Visible = false;
            // 
            // Form
            // 
            this.Form.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Form.HeaderText = "Form";
            this.Form.Name = "Form";
            // 
            // Write
            // 
            this.Write.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Write.HeaderText = "Write Permission";
            this.Write.Name = "Write";
            this.Write.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Write.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Read
            // 
            this.Read.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Read.HeaderText = "Read Permission";
            this.Read.Name = "Read";
            this.Read.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Read.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 103F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 463F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btnSave, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnRefresh, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 497);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(820, 33);
            this.tableLayoutPanel1.TabIndex = 224;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.tableLayoutPanel1_Paint);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSave.BackgroundImage")));
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(163, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(70, 23);
            this.btnSave.TabIndex = 19;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRefresh.BackgroundImage")));
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(266, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(70, 23);
            this.btnRefresh.TabIndex = 20;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(360, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(70, 23);
            this.btnClose.TabIndex = 21;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlusertype
            // 
            this.pnlusertype.BackColor = System.Drawing.Color.AliceBlue;
            this.pnlusertype.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlusertype.Controls.Add(this.btnusertype);
            this.pnlusertype.Controls.Add(this.panel2);
            this.pnlusertype.Controls.Add(this.label30);
            this.pnlusertype.Controls.Add(this.txtusertype);
            this.pnlusertype.Controls.Add(this.label29);
            this.pnlusertype.Controls.Add(this.txtdescr);
            this.pnlusertype.Controls.Add(this.label27);
            this.pnlusertype.ForeColor = System.Drawing.Color.Black;
            this.pnlusertype.Location = new System.Drawing.Point(179, 137);
            this.pnlusertype.Name = "pnlusertype";
            this.pnlusertype.Size = new System.Drawing.Size(463, 257);
            this.pnlusertype.TabIndex = 285;
            this.pnlusertype.Visible = false;
            // 
            // btnusertype
            // 
            this.btnusertype.BackColor = System.Drawing.Color.Transparent;
            this.btnusertype.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnusertype.BackgroundImage")));
            this.btnusertype.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnusertype.Enabled = false;
            this.btnusertype.FlatAppearance.BorderSize = 0;
            this.btnusertype.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnusertype.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnusertype.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnusertype.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnusertype.Location = new System.Drawing.Point(191, 215);
            this.btnusertype.Name = "btnusertype";
            this.btnusertype.Size = new System.Drawing.Size(70, 23);
            this.btnusertype.TabIndex = 234;
            this.btnusertype.UseVisualStyleBackColor = false;
            this.btnusertype.Click += new System.EventHandler(this.btnusertype_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SteelBlue;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.lblsubcatclose);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(459, 44);
            this.panel2.TabIndex = 233;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(18, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 30);
            this.pictureBox1.TabIndex = 279;
            this.pictureBox1.TabStop = false;
            // 
            // lblsubcatclose
            // 
            this.lblsubcatclose.BackColor = System.Drawing.Color.Transparent;
            this.lblsubcatclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lblsubcatclose.FlatAppearance.BorderSize = 0;
            this.lblsubcatclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblsubcatclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsubcatclose.ForeColor = System.Drawing.Color.Transparent;
            this.lblsubcatclose.Location = new System.Drawing.Point(425, 7);
            this.lblsubcatclose.Name = "lblsubcatclose";
            this.lblsubcatclose.Size = new System.Drawing.Size(26, 30);
            this.lblsubcatclose.TabIndex = 173;
            this.lblsubcatclose.UseVisualStyleBackColor = false;
            this.lblsubcatclose.Click += new System.EventHandler(this.lblsubcatclose_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(56, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 25);
            this.label6.TabIndex = 67;
            this.label6.Text = "User Type";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label30.Location = new System.Drawing.Point(94, 69);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(13, 17);
            this.label30.TabIndex = 232;
            this.label30.Text = "*";
            // 
            // txtusertype
            // 
            this.txtusertype.BackColor = System.Drawing.Color.White;
            this.txtusertype.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtusertype.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtusertype.Location = new System.Drawing.Point(108, 66);
            this.txtusertype.Name = "txtusertype";
            this.txtusertype.Size = new System.Drawing.Size(265, 26);
            this.txtusertype.TabIndex = 0;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label29.ForeColor = System.Drawing.Color.Black;
            this.label29.Location = new System.Drawing.Point(15, 66);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(82, 17);
            this.label29.TabIndex = 231;
            this.label29.Text = "User Type :";
            // 
            // txtdescr
            // 
            this.txtdescr.BackColor = System.Drawing.Color.White;
            this.txtdescr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdescr.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtdescr.Location = new System.Drawing.Point(108, 108);
            this.txtdescr.Multiline = true;
            this.txtdescr.Name = "txtdescr";
            this.txtdescr.Size = new System.Drawing.Size(265, 87);
            this.txtdescr.TabIndex = 1;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(15, 108);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(87, 17);
            this.label27.TabIndex = 221;
            this.label27.Text = "Description :";
            // 
            // FrmUserPermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(820, 530);
            this.Controls.Add(this.pnlusertype);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnaddusertype);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUserTypeNote);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbUsreAccount);
            this.Controls.Add(this.panel5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmUserPermission";
            this.Text = "FrmUserPermission";
            this.Load += new System.EventHandler(this.FrmUserPermission_Load);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvforms)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.pnlusertype.ResumeLayout(false);
            this.pnlusertype.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserTypeNote;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbUsreAccount;
        private System.Windows.Forms.Button btnaddusertype;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvforms;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn formid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Form;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Write;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Read;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlusertype;
        private System.Windows.Forms.Button btnusertype;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button lblsubcatclose;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtusertype;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtdescr;
        private System.Windows.Forms.Label label27;
    }
}