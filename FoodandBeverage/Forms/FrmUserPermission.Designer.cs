
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUserPermission));
            this.tlp_header = new System.Windows.Forms.TableLayoutPanel();
            this.label23 = new System.Windows.Forms.Label();
            this.btncolse = new System.Windows.Forms.Button();
            this.btnmini = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtUserTypeNote = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbUsreAccount = new System.Windows.Forms.ComboBox();
            this.dgvforms = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.formid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Form = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Write = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Read = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tlp_buttons = new System.Windows.Forms.TableLayoutPanel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlusertype = new System.Windows.Forms.Panel();
            this.tlp_usertypemain = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_usertype_buttons = new System.Windows.Forms.TableLayoutPanel();
            this.txtutypeid = new System.Windows.Forms.TextBox();
            this.btn_usertype_close = new System.Windows.Forms.Button();
            this.btnusertype = new System.Windows.Forms.Button();
            this.tlp_usertype_re = new System.Windows.Forms.TableLayoutPanel();
            this.label27 = new System.Windows.Forms.Label();
            this.txtusertype = new System.Windows.Forms.TextBox();
            this.txtdescr = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.lblusertypeinfo = new System.Windows.Forms.Label();
            this.tlp_usertype_header = new System.Windows.Forms.TableLayoutPanel();
            this.lblsubcatclose = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.tlp_reg = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.btnaddusertype = new System.Windows.Forms.Button();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_design = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_two_box = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_note = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvforms)).BeginInit();
            this.tlp_buttons.SuspendLayout();
            this.pnlusertype.SuspendLayout();
            this.tlp_usertypemain.SuspendLayout();
            this.tlp_usertype_buttons.SuspendLayout();
            this.tlp_usertype_re.SuspendLayout();
            this.tlp_usertype_header.SuspendLayout();
            this.tlp_reg.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tlp_main.SuspendLayout();
            this.tlp_design.SuspendLayout();
            this.tlp_two_box.SuspendLayout();
            this.tlp_note.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_header
            // 
            this.tlp_header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(80)))), ((int)(((byte)(185)))));
            this.tlp_header.ColumnCount = 3;
            this.tlp_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlp_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tlp_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_header.Controls.Add(this.label23, 0, 0);
            this.tlp_header.Controls.Add(this.btncolse, 2, 0);
            this.tlp_header.Controls.Add(this.btnmini, 1, 0);
            this.tlp_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlp_header.Location = new System.Drawing.Point(8, 8);
            this.tlp_header.Name = "tlp_header";
            this.tlp_header.RowCount = 1;
            this.tlp_header.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_header.Size = new System.Drawing.Size(738, 31);
            this.tlp_header.TabIndex = 286;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.label23.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label23.Location = new System.Drawing.Point(3, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(653, 31);
            this.label23.TabIndex = 277;
            this.label23.Text = "User Permission";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btncolse
            // 
            this.btncolse.BackColor = System.Drawing.Color.White;
            this.btncolse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btncolse.FlatAppearance.BorderSize = 0;
            this.btncolse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncolse.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncolse.ForeColor = System.Drawing.Color.Red;
            this.btncolse.Location = new System.Drawing.Point(700, 3);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(35, 25);
            this.btncolse.TabIndex = 11;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(662, 3);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(32, 25);
            this.btnmini.TabIndex = 10;
            this.btnmini.Text = "-";
            this.btnmini.UseVisualStyleBackColor = false;
            this.btnmini.Click += new System.EventHandler(this.btnmini_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(354, 34);
            this.label3.TabIndex = 280;
            this.label3.Text = "Note  ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtUserTypeNote
            // 
            this.txtUserTypeNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.txtUserTypeNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtUserTypeNote.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtUserTypeNote.Location = new System.Drawing.Point(3, 37);
            this.txtUserTypeNote.Multiline = true;
            this.txtUserTypeNote.Name = "txtUserTypeNote";
            this.txtUserTypeNote.Size = new System.Drawing.Size(354, 46);
            this.txtUserTypeNote.TabIndex = 279;
            this.txtUserTypeNote.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserTypeNote_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(295, 29);
            this.label2.TabIndex = 278;
            this.label2.Text = "Access Type ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbUsreAccount
            // 
            this.cmbUsreAccount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.cmbUsreAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbUsreAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsreAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbUsreAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbUsreAccount.FormattingEnabled = true;
            this.cmbUsreAccount.Location = new System.Drawing.Point(3, 32);
            this.cmbUsreAccount.Name = "cmbUsreAccount";
            this.cmbUsreAccount.Size = new System.Drawing.Size(295, 28);
            this.cmbUsreAccount.TabIndex = 277;
            this.cmbUsreAccount.SelectionChangeCommitted += new System.EventHandler(this.cmbUsreAccount_SelectionChangeCommitted);
            this.cmbUsreAccount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbUsreAccount_KeyDown);
            // 
            // dgvforms
            // 
            this.dgvforms.AllowUserToAddRows = false;
            this.dgvforms.AllowUserToDeleteRows = false;
            this.dgvforms.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvforms.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvforms.ColumnHeadersHeight = 46;
            this.dgvforms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.formid,
            this.Form,
            this.Write,
            this.Read});
            this.dgvforms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvforms.Location = new System.Drawing.Point(8, 151);
            this.dgvforms.Name = "dgvforms";
            this.dgvforms.RowHeadersVisible = false;
            this.dgvforms.RowHeadersWidth = 51;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvforms.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvforms.RowTemplate.Height = 30;
            this.dgvforms.Size = new System.Drawing.Size(738, 324);
            this.dgvforms.TabIndex = 223;
            this.dgvforms.Visible = false;
            this.dgvforms.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvforms_CellEndEdit);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.MinimumWidth = 6;
            this.id.Name = "id";
            this.id.Visible = false;
            this.id.Width = 125;
            // 
            // formid
            // 
            this.formid.HeaderText = "formid";
            this.formid.MinimumWidth = 6;
            this.formid.Name = "formid";
            this.formid.Visible = false;
            this.formid.Width = 125;
            // 
            // Form
            // 
            this.Form.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Form.HeaderText = "Form";
            this.Form.MinimumWidth = 6;
            this.Form.Name = "Form";
            // 
            // Write
            // 
            this.Write.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle2.NullValue = false;
            this.Write.DefaultCellStyle = dataGridViewCellStyle2;
            this.Write.HeaderText = "Write Permission";
            this.Write.MinimumWidth = 6;
            this.Write.Name = "Write";
            this.Write.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Write.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Read
            // 
            this.Read.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Read.HeaderText = "Read Permission";
            this.Read.MinimumWidth = 6;
            this.Read.Name = "Read";
            this.Read.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Read.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // tlp_buttons
            // 
            this.tlp_buttons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_buttons.ColumnCount = 4;
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_buttons.Controls.Add(this.btnSave, 1, 0);
            this.tlp_buttons.Controls.Add(this.btnRefresh, 2, 0);
            this.tlp_buttons.Controls.Add(this.btnClose, 3, 0);
            this.tlp_buttons.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlp_buttons.Location = new System.Drawing.Point(8, 481);
            this.tlp_buttons.Name = "tlp_buttons";
            this.tlp_buttons.RowCount = 1;
            this.tlp_buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_buttons.Size = new System.Drawing.Size(738, 39);
            this.tlp_buttons.TabIndex = 224;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImage = global::FoodandBeverage.Properties.Resources.SaveButton;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(489, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 33);
            this.btnSave.TabIndex = 19;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_REFESH;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(577, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(77, 33);
            this.btnRefresh.TabIndex = 20;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnClose.BackgroundImage")));
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(660, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 33);
            this.btnClose.TabIndex = 21;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pnlusertype
            // 
            this.pnlusertype.BackColor = System.Drawing.Color.White;
            this.pnlusertype.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlusertype.Controls.Add(this.tlp_usertypemain);
            this.pnlusertype.Controls.Add(this.label30);
            this.pnlusertype.ForeColor = System.Drawing.Color.Black;
            this.pnlusertype.Location = new System.Drawing.Point(84, 126);
            this.pnlusertype.Name = "pnlusertype";
            this.pnlusertype.Size = new System.Drawing.Size(419, 281);
            this.pnlusertype.TabIndex = 285;
            this.pnlusertype.Visible = false;
            // 
            // tlp_usertypemain
            // 
            this.tlp_usertypemain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_usertypemain.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Outset;
            this.tlp_usertypemain.ColumnCount = 1;
            this.tlp_usertypemain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_usertypemain.Controls.Add(this.tlp_usertype_buttons, 0, 2);
            this.tlp_usertypemain.Controls.Add(this.tlp_usertype_re, 0, 1);
            this.tlp_usertypemain.Controls.Add(this.tlp_usertype_header, 0, 0);
            this.tlp_usertypemain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_usertypemain.Location = new System.Drawing.Point(0, 0);
            this.tlp_usertypemain.Name = "tlp_usertypemain";
            this.tlp_usertypemain.RowCount = 3;
            this.tlp_usertypemain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_usertypemain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_usertypemain.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_usertypemain.Size = new System.Drawing.Size(415, 277);
            this.tlp_usertypemain.TabIndex = 235;
            // 
            // tlp_usertype_buttons
            // 
            this.tlp_usertype_buttons.ColumnCount = 3;
            this.tlp_usertype_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_usertype_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_usertype_buttons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_usertype_buttons.Controls.Add(this.txtutypeid, 0, 1);
            this.tlp_usertype_buttons.Controls.Add(this.btn_usertype_close, 2, 1);
            this.tlp_usertype_buttons.Controls.Add(this.btnusertype, 1, 1);
            this.tlp_usertype_buttons.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlp_usertype_buttons.Location = new System.Drawing.Point(5, 206);
            this.tlp_usertype_buttons.Name = "tlp_usertype_buttons";
            this.tlp_usertype_buttons.RowCount = 2;
            this.tlp_usertype_buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tlp_usertype_buttons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_usertype_buttons.Size = new System.Drawing.Size(405, 56);
            this.tlp_usertype_buttons.TabIndex = 1;
            // 
            // txtutypeid
            // 
            this.txtutypeid.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.txtutypeid.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtutypeid.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtutypeid.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtutypeid.Location = new System.Drawing.Point(3, 17);
            this.txtutypeid.Multiline = true;
            this.txtutypeid.Name = "txtutypeid";
            this.txtutypeid.Size = new System.Drawing.Size(237, 34);
            this.txtutypeid.TabIndex = 234;
            this.txtutypeid.Visible = false;
            // 
            // btn_usertype_close
            // 
            this.btn_usertype_close.BackColor = System.Drawing.Color.Transparent;
            this.btn_usertype_close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_usertype_close.BackgroundImage")));
            this.btn_usertype_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_usertype_close.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_usertype_close.FlatAppearance.BorderSize = 0;
            this.btn_usertype_close.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_usertype_close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_usertype_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_usertype_close.Location = new System.Drawing.Point(327, 17);
            this.btn_usertype_close.Name = "btn_usertype_close";
            this.btn_usertype_close.Size = new System.Drawing.Size(75, 36);
            this.btn_usertype_close.TabIndex = 1;
            this.btn_usertype_close.UseVisualStyleBackColor = false;
            this.btn_usertype_close.Click += new System.EventHandler(this.btn_usertype_close_Click);
            // 
            // btnusertype
            // 
            this.btnusertype.BackColor = System.Drawing.Color.Transparent;
            this.btnusertype.BackgroundImage = global::FoodandBeverage.Properties.Resources.SaveButton;
            this.btnusertype.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnusertype.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnusertype.Enabled = false;
            this.btnusertype.FlatAppearance.BorderSize = 0;
            this.btnusertype.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnusertype.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnusertype.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnusertype.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnusertype.Location = new System.Drawing.Point(246, 17);
            this.btnusertype.Name = "btnusertype";
            this.btnusertype.Size = new System.Drawing.Size(75, 36);
            this.btnusertype.TabIndex = 0;
            this.btnusertype.UseVisualStyleBackColor = false;
            this.btnusertype.Click += new System.EventHandler(this.btnusertype_Click);
            // 
            // tlp_usertype_re
            // 
            this.tlp_usertype_re.ColumnCount = 2;
            this.tlp_usertype_re.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_usertype_re.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_usertype_re.Controls.Add(this.label27, 0, 2);
            this.tlp_usertype_re.Controls.Add(this.txtusertype, 1, 0);
            this.tlp_usertype_re.Controls.Add(this.txtdescr, 1, 2);
            this.tlp_usertype_re.Controls.Add(this.label29, 0, 0);
            this.tlp_usertype_re.Controls.Add(this.lblusertypeinfo, 1, 1);
            this.tlp_usertype_re.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_usertype_re.Location = new System.Drawing.Point(5, 51);
            this.tlp_usertype_re.Name = "tlp_usertype_re";
            this.tlp_usertype_re.RowCount = 3;
            this.tlp_usertype_re.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_usertype_re.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_usertype_re.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_usertype_re.Size = new System.Drawing.Size(405, 147);
            this.tlp_usertype_re.TabIndex = 0;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label27.ForeColor = System.Drawing.Color.Black;
            this.label27.Location = new System.Drawing.Point(3, 56);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(87, 93);
            this.label27.TabIndex = 221;
            this.label27.Text = "Description ";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtusertype
            // 
            this.txtusertype.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.txtusertype.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtusertype.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtusertype.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtusertype.Location = new System.Drawing.Point(96, 3);
            this.txtusertype.Multiline = true;
            this.txtusertype.Name = "txtusertype";
            this.txtusertype.Size = new System.Drawing.Size(330, 34);
            this.txtusertype.TabIndex = 0;
            this.txtusertype.Enter += new System.EventHandler(this.txtusertype_Enter);
            this.txtusertype.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtusertype_KeyDown);
            this.txtusertype.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtusertype_KeyPress);
            this.txtusertype.Leave += new System.EventHandler(this.txtusertype_Leave);
            // 
            // txtdescr
            // 
            this.txtdescr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(209)))), ((int)(((byte)(234)))));
            this.txtdescr.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtdescr.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtdescr.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtdescr.Location = new System.Drawing.Point(96, 59);
            this.txtdescr.Multiline = true;
            this.txtdescr.Name = "txtdescr";
            this.txtdescr.Size = new System.Drawing.Size(330, 87);
            this.txtdescr.TabIndex = 1;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.BackColor = System.Drawing.Color.Transparent;
            this.label29.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label29.ForeColor = System.Drawing.Color.Black;
            this.label29.Location = new System.Drawing.Point(3, 0);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(87, 40);
            this.label29.TabIndex = 231;
            this.label29.Text = "User Type ";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblusertypeinfo
            // 
            this.lblusertypeinfo.AutoSize = true;
            this.lblusertypeinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblusertypeinfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblusertypeinfo.ForeColor = System.Drawing.Color.Red;
            this.lblusertypeinfo.Location = new System.Drawing.Point(96, 40);
            this.lblusertypeinfo.Name = "lblusertypeinfo";
            this.lblusertypeinfo.Size = new System.Drawing.Size(330, 16);
            this.lblusertypeinfo.TabIndex = 232;
            this.lblusertypeinfo.Text = "F8 For User Type List";
            this.lblusertypeinfo.Visible = false;
            // 
            // tlp_usertype_header
            // 
            this.tlp_usertype_header.BackColor = System.Drawing.Color.SteelBlue;
            this.tlp_usertype_header.ColumnCount = 2;
            this.tlp_usertype_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_usertype_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tlp_usertype_header.Controls.Add(this.lblsubcatclose, 1, 0);
            this.tlp_usertype_header.Controls.Add(this.label6, 0, 0);
            this.tlp_usertype_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlp_usertype_header.Location = new System.Drawing.Point(5, 5);
            this.tlp_usertype_header.Name = "tlp_usertype_header";
            this.tlp_usertype_header.RowCount = 1;
            this.tlp_usertype_header.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_usertype_header.Size = new System.Drawing.Size(405, 38);
            this.tlp_usertype_header.TabIndex = 236;
            // 
            // lblsubcatclose
            // 
            this.lblsubcatclose.BackColor = System.Drawing.Color.Transparent;
            this.lblsubcatclose.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lblsubcatclose.BackgroundImage")));
            this.lblsubcatclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lblsubcatclose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblsubcatclose.FlatAppearance.BorderSize = 0;
            this.lblsubcatclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblsubcatclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsubcatclose.ForeColor = System.Drawing.Color.Transparent;
            this.lblsubcatclose.Location = new System.Drawing.Point(358, 3);
            this.lblsubcatclose.Name = "lblsubcatclose";
            this.lblsubcatclose.Size = new System.Drawing.Size(44, 32);
            this.lblsubcatclose.TabIndex = 173;
            this.lblsubcatclose.UseVisualStyleBackColor = false;
            this.lblsubcatclose.Click += new System.EventHandler(this.lblsubcatclose_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(349, 38);
            this.label6.TabIndex = 67;
            this.label6.Text = "User Type Registartion";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // tlp_reg
            // 
            this.tlp_reg.BackColor = System.Drawing.Color.Transparent;
            this.tlp_reg.ColumnCount = 2;
            this.tlp_reg.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.76289F));
            this.tlp_reg.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.23711F));
            this.tlp_reg.Controls.Add(this.label2, 0, 0);
            this.tlp_reg.Controls.Add(this.cmbUsreAccount, 0, 1);
            this.tlp_reg.Controls.Add(this.tableLayoutPanel4, 1, 1);
            this.tlp_reg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_reg.Location = new System.Drawing.Point(3, 3);
            this.tlp_reg.Name = "tlp_reg";
            this.tlp_reg.RowCount = 2;
            this.tlp_reg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_reg.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 57F));
            this.tlp_reg.Size = new System.Drawing.Size(360, 86);
            this.tlp_reg.TabIndex = 285;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.btnaddusertype, 0, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(304, 32);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(51, 28);
            this.tableLayoutPanel4.TabIndex = 284;
            // 
            // btnaddusertype
            // 
            this.btnaddusertype.BackColor = System.Drawing.Color.Transparent;
            this.btnaddusertype.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnaddusertype.BackgroundImage")));
            this.btnaddusertype.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnaddusertype.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnaddusertype.FlatAppearance.BorderSize = 0;
            this.btnaddusertype.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnaddusertype.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnaddusertype.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnaddusertype.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaddusertype.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaddusertype.Location = new System.Drawing.Point(3, 3);
            this.btnaddusertype.Name = "btnaddusertype";
            this.btnaddusertype.Size = new System.Drawing.Size(41, 22);
            this.btnaddusertype.TabIndex = 283;
            this.btnaddusertype.UseVisualStyleBackColor = false;
            this.btnaddusertype.Click += new System.EventHandler(this.btnaddusertype_Click);
            // 
            // tlp_main
            // 
            this.tlp_main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tlp_design, 0, 1);
            this.tlp_main.Controls.Add(this.tlp_header, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_buttons, 0, 3);
            this.tlp_main.Controls.Add(this.dgvforms, 0, 2);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.Padding = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.tlp_main.RowCount = 4;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.Size = new System.Drawing.Size(754, 530);
            this.tlp_main.TabIndex = 287;
            // 
            // tlp_design
            // 
            this.tlp_design.ColumnCount = 1;
            this.tlp_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_design.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_design.Controls.Add(this.tlp_two_box, 0, 0);
            this.tlp_design.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_design.Location = new System.Drawing.Point(8, 45);
            this.tlp_design.Name = "tlp_design";
            this.tlp_design.RowCount = 2;
            this.tlp_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 98F));
            this.tlp_design.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2F));
            this.tlp_design.Size = new System.Drawing.Size(738, 100);
            this.tlp_design.TabIndex = 288;
            // 
            // tlp_two_box
            // 
            this.tlp_two_box.BackColor = System.Drawing.Color.White;
            this.tlp_two_box.ColumnCount = 2;
            this.tlp_two_box.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_two_box.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_two_box.Controls.Add(this.tlp_reg, 0, 0);
            this.tlp_two_box.Controls.Add(this.tlp_note, 1, 0);
            this.tlp_two_box.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_two_box.Location = new System.Drawing.Point(3, 3);
            this.tlp_two_box.Name = "tlp_two_box";
            this.tlp_two_box.RowCount = 1;
            this.tlp_two_box.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_two_box.Size = new System.Drawing.Size(732, 92);
            this.tlp_two_box.TabIndex = 285;
            // 
            // tlp_note
            // 
            this.tlp_note.ColumnCount = 1;
            this.tlp_note.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_note.Controls.Add(this.label3, 0, 0);
            this.tlp_note.Controls.Add(this.txtUserTypeNote, 0, 1);
            this.tlp_note.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_note.Location = new System.Drawing.Point(369, 3);
            this.tlp_note.Name = "tlp_note";
            this.tlp_note.RowCount = 2;
            this.tlp_note.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 39.53489F));
            this.tlp_note.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60.46511F));
            this.tlp_note.Size = new System.Drawing.Size(360, 86);
            this.tlp_note.TabIndex = 286;
            // 
            // FrmUserPermission
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(754, 530);
            this.Controls.Add(this.pnlusertype);
            this.Controls.Add(this.tlp_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmUserPermission";
            this.Tag = "22";
            this.Text = "FrmUserPermission";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmUserPermission_FormClosing);
            this.Load += new System.EventHandler(this.FrmUserPermission_Load);
            this.Resize += new System.EventHandler(this.FrmUserPermission_Resize);
            this.tlp_header.ResumeLayout(false);
            this.tlp_header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvforms)).EndInit();
            this.tlp_buttons.ResumeLayout(false);
            this.pnlusertype.ResumeLayout(false);
            this.pnlusertype.PerformLayout();
            this.tlp_usertypemain.ResumeLayout(false);
            this.tlp_usertype_buttons.ResumeLayout(false);
            this.tlp_usertype_buttons.PerformLayout();
            this.tlp_usertype_re.ResumeLayout(false);
            this.tlp_usertype_re.PerformLayout();
            this.tlp_usertype_header.ResumeLayout(false);
            this.tlp_usertype_header.PerformLayout();
            this.tlp_reg.ResumeLayout(false);
            this.tlp_reg.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tlp_main.ResumeLayout(false);
            this.tlp_design.ResumeLayout(false);
            this.tlp_two_box.ResumeLayout(false);
            this.tlp_note.ResumeLayout(false);
            this.tlp_note.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtUserTypeNote;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbUsreAccount;
        private System.Windows.Forms.Button btnaddusertype;
        private System.Windows.Forms.DataGridView dgvforms;
        private System.Windows.Forms.TableLayoutPanel tlp_buttons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel pnlusertype;
        private System.Windows.Forms.Button btnusertype;
        private System.Windows.Forms.Button lblsubcatclose;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox txtusertype;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtdescr;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TableLayoutPanel tlp_header;
        private System.Windows.Forms.TableLayoutPanel tlp_reg;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn formid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Form;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Write;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Read;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TableLayoutPanel tlp_design;
        private System.Windows.Forms.TableLayoutPanel tlp_two_box;
        private System.Windows.Forms.TableLayoutPanel tlp_note;
        private System.Windows.Forms.TableLayoutPanel tlp_usertypemain;
        private System.Windows.Forms.TableLayoutPanel tlp_usertype_buttons;
        private System.Windows.Forms.Button btn_usertype_close;
        private System.Windows.Forms.TableLayoutPanel tlp_usertype_re;
        private System.Windows.Forms.TableLayoutPanel tlp_usertype_header;
        private System.Windows.Forms.Label lblusertypeinfo;
        private System.Windows.Forms.TextBox txtutypeid;
    }
}