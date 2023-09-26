namespace pLocker.Forms
{
    partial class FrmLocker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLocker));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.mtxtFieldValue = new System.Windows.Forms.TextBox();
            this.cboField = new System.Windows.Forms.ComboBox();
            this.lblTotLocker = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnMin = new System.Windows.Forms.Button();
            this.label = new System.Windows.Forms.Label();
            this.btnFrmClose = new System.Windows.Forms.Button();
            this.pnlLocker = new System.Windows.Forms.Panel();
            this.txtRFID = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRfid = new System.Windows.Forms.Label();
            this.txtRfid_2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnColorPlate = new System.Windows.Forms.Button();
            this.btnRfid = new System.Windows.Forms.Button();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.lbl_rfid_info = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cboLocation = new System.Windows.Forms.ComboBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.grpRate = new System.Windows.Forms.GroupBox();
            this.txtPerHours = new System.Windows.Forms.TextBox();
            this.txtPerDay = new System.Windows.Forms.TextBox();
            this.txtMonth = new System.Windows.Forms.TextBox();
            this.lalHours = new System.Windows.Forms.Label();
            this.lalDay = new System.Windows.Forms.Label();
            this.lblMonth = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.txtPinNo = new System.Windows.Forms.TextBox();
            this.lblPinNo = new System.Windows.Forms.Label();
            this.txtMacId = new System.Windows.Forms.TextBox();
            this.lblMacId = new System.Windows.Forms.Label();
            this.lblRowNo = new System.Windows.Forms.Label();
            this.txtRowNo = new System.Windows.Forms.TextBox();
            this.txtLocker = new System.Windows.Forms.TextBox();
            this.lblLocker = new System.Windows.Forms.Label();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnLocker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnRowno = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnMacID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnRFID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnPINNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnLockDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnHrRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnDyRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cnMnRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbLocationID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnlLocker.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.grpRate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.SuspendLayout();
            // 
            // colorDialog1
            // 
            this.colorDialog1.Color = System.Drawing.Color.White;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnNew);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.mtxtFieldValue);
            this.panel1.Controls.Add(this.cboField);
            this.panel1.Controls.Add(this.lblTotLocker);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.pnlLocker);
            this.panel1.Controls.Add(this.dgvList);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1021, 527);
            this.panel1.TabIndex = 29;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(10, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 25);
            this.label3.TabIndex = 121;
            this.label3.Text = "Search In :";
            // 
            // btnNew
            // 
            this.btnNew.BackColor = System.Drawing.Color.Transparent;
            this.btnNew.BackgroundImage = global::pLocker.Properties.Resources.New_1;
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnNew.FlatAppearance.BorderSize = 0;
            this.btnNew.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnNew.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.ForeColor = System.Drawing.Color.White;
            this.btnNew.Location = new System.Drawing.Point(864, 60);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(132, 44);
            this.btnNew.TabIndex = 117;
            this.btnNew.UseVisualStyleBackColor = false;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            this.btnNew.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            this.btnNew.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnNew_MouseDown);
            this.btnNew.MouseLeave += new System.EventHandler(this.btnNew_MouseLeave);
            this.btnNew.MouseHover += new System.EventHandler(this.btnNew_MouseHover);
            this.btnNew.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnNew_MouseUp);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.Transparent;
            this.btnSearch.BackgroundImage = global::pLocker.Properties.Resources.search_1;
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(724, 60);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(132, 44);
            this.btnSearch.TabIndex = 116;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            this.btnSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            this.btnSearch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSearch_MouseDown);
            this.btnSearch.MouseLeave += new System.EventHandler(this.btnSearch_MouseLeave);
            this.btnSearch.MouseHover += new System.EventHandler(this.btnSearch_MouseHover);
            this.btnSearch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnSearch_MouseUp);
            // 
            // mtxtFieldValue
            // 
            this.mtxtFieldValue.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtFieldValue.ForeColor = System.Drawing.Color.Silver;
            this.mtxtFieldValue.Location = new System.Drawing.Point(258, 70);
            this.mtxtFieldValue.Name = "mtxtFieldValue";
            this.mtxtFieldValue.Size = new System.Drawing.Size(149, 33);
            this.mtxtFieldValue.TabIndex = 121;
            this.mtxtFieldValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // cboField
            // 
            this.cboField.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboField.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboField.ForeColor = System.Drawing.Color.Silver;
            this.cboField.FormattingEnabled = true;
            this.cboField.Location = new System.Drawing.Point(122, 70);
            this.cboField.Name = "cboField";
            this.cboField.Size = new System.Drawing.Size(130, 33);
            this.cboField.TabIndex = 121;
            this.cboField.SelectionChangeCommitted += new System.EventHandler(this.cboField_SelectionChangeCommitted);
            this.cboField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // lblTotLocker
            // 
            this.lblTotLocker.AutoSize = true;
            this.lblTotLocker.BackColor = System.Drawing.Color.Transparent;
            this.lblTotLocker.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotLocker.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTotLocker.Location = new System.Drawing.Point(3, 508);
            this.lblTotLocker.Name = "lblTotLocker";
            this.lblTotLocker.Size = new System.Drawing.Size(20, 17);
            this.lblTotLocker.TabIndex = 58;
            this.lblTotLocker.Text = "...";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.Controls.Add(this.btnMin);
            this.panel3.Controls.Add(this.label);
            this.panel3.Controls.Add(this.btnFrmClose);
            this.panel3.Location = new System.Drawing.Point(-1, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1021, 45);
            this.panel3.TabIndex = 114;
            // 
            // btnMin
            // 
            this.btnMin.BackColor = System.Drawing.Color.Transparent;
            this.btnMin.BackgroundImage = global::pLocker.Properties.Resources.Minimize_1;
            this.btnMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMin.ForeColor = System.Drawing.Color.White;
            this.btnMin.Location = new System.Drawing.Point(964, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(27, 30);
            this.btnMin.TabIndex = 176;
            this.btnMin.Text = "-";
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Click += new System.EventHandler(this.Minimize);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.BackColor = System.Drawing.Color.Transparent;
            this.label.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
            this.label.Location = new System.Drawing.Point(2, 8);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(150, 25);
            this.label.TabIndex = 44;
            this.label.Text = "mi Loker Details";
            // 
            // btnFrmClose
            // 
            this.btnFrmClose.BackColor = System.Drawing.Color.Transparent;
            this.btnFrmClose.BackgroundImage = global::pLocker.Properties.Resources.close_1_button;
            this.btnFrmClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFrmClose.FlatAppearance.BorderSize = 0;
            this.btnFrmClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFrmClose.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFrmClose.ForeColor = System.Drawing.Color.Transparent;
            this.btnFrmClose.Location = new System.Drawing.Point(992, 0);
            this.btnFrmClose.Name = "btnFrmClose";
            this.btnFrmClose.Size = new System.Drawing.Size(26, 30);
            this.btnFrmClose.TabIndex = 43;
            this.btnFrmClose.UseVisualStyleBackColor = false;
            this.btnFrmClose.Click += new System.EventHandler(this.btnFrmClose_Click);
            // 
            // pnlLocker
            // 
            this.pnlLocker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.pnlLocker.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlLocker.Controls.Add(this.txtRFID);
            this.pnlLocker.Controls.Add(this.tableLayoutPanel1);
            this.pnlLocker.Controls.Add(this.btnDelete);
            this.pnlLocker.Controls.Add(this.label2);
            this.pnlLocker.Controls.Add(this.cboLocation);
            this.pnlLocker.Controls.Add(this.txtID);
            this.pnlLocker.Controls.Add(this.panel4);
            this.pnlLocker.Controls.Add(this.grpRate);
            this.pnlLocker.Controls.Add(this.btnRefresh);
            this.pnlLocker.Controls.Add(this.btnSave);
            this.pnlLocker.Controls.Add(this.txtDesc);
            this.pnlLocker.Controls.Add(this.lblDesc);
            this.pnlLocker.Controls.Add(this.txtPinNo);
            this.pnlLocker.Controls.Add(this.lblPinNo);
            this.pnlLocker.Controls.Add(this.txtMacId);
            this.pnlLocker.Controls.Add(this.lblMacId);
            this.pnlLocker.Controls.Add(this.lblRowNo);
            this.pnlLocker.Controls.Add(this.txtRowNo);
            this.pnlLocker.Controls.Add(this.txtLocker);
            this.pnlLocker.Controls.Add(this.lblLocker);
            this.pnlLocker.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
            this.pnlLocker.Location = new System.Drawing.Point(163, 139);
            this.pnlLocker.Name = "pnlLocker";
            this.pnlLocker.Size = new System.Drawing.Size(750, 361);
            this.pnlLocker.TabIndex = 46;
            // 
            // txtRFID
            // 
            this.txtRFID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRFID.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRFID.ForeColor = System.Drawing.Color.Black;
            this.txtRFID.Location = new System.Drawing.Point(712, 211);
            this.txtRFID.Name = "txtRFID";
            this.txtRFID.ReadOnly = true;
            this.txtRFID.Size = new System.Drawing.Size(19, 33);
            this.txtRFID.TabIndex = 6;
            this.txtRFID.Visible = false;
            this.txtRFID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 199F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Controls.Add(this.lblRfid, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtRfid_2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnColorPlate, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.btnRfid, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.txtColor, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lbl_rfid_info, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(400, 105);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(334, 100);
            this.tableLayoutPanel1.TabIndex = 121;
            // 
            // lblRfid
            // 
            this.lblRfid.AutoSize = true;
            this.lblRfid.BackColor = System.Drawing.Color.Transparent;
            this.lblRfid.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRfid.ForeColor = System.Drawing.Color.Silver;
            this.lblRfid.Location = new System.Drawing.Point(3, 0);
            this.lblRfid.Name = "lblRfid";
            this.lblRfid.Size = new System.Drawing.Size(64, 25);
            this.lblRfid.TabIndex = 52;
            this.lblRfid.Text = "RFID :";
            // 
            // txtRfid_2
            // 
            this.txtRfid_2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRfid_2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRfid_2.ForeColor = System.Drawing.Color.Black;
            this.txtRfid_2.Location = new System.Drawing.Point(98, 3);
            this.txtRfid_2.Name = "txtRfid_2";
            this.txtRfid_2.ReadOnly = true;
            this.txtRfid_2.Size = new System.Drawing.Size(190, 33);
            this.txtRfid_2.TabIndex = 122;
            this.txtRfid_2.Enter += new System.EventHandler(this.txtRfid_2_Enter);
            this.txtRfid_2.Leave += new System.EventHandler(this.txtRfid_2_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(3, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 25);
            this.label1.TabIndex = 117;
            this.label1.Text = "Color :";
            this.label1.Visible = false;
            // 
            // btnColorPlate
            // 
            this.btnColorPlate.BackColor = System.Drawing.Color.Transparent;
            this.btnColorPlate.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnColorPlate.BackgroundImage")));
            this.btnColorPlate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnColorPlate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnColorPlate.Location = new System.Drawing.Point(297, 56);
            this.btnColorPlate.Name = "btnColorPlate";
            this.btnColorPlate.Size = new System.Drawing.Size(27, 31);
            this.btnColorPlate.TabIndex = 11;
            this.btnColorPlate.UseVisualStyleBackColor = false;
            this.btnColorPlate.Visible = false;
            this.btnColorPlate.Click += new System.EventHandler(this.btnColor_Click);
            // 
            // btnRfid
            // 
            this.btnRfid.BackColor = System.Drawing.Color.Transparent;
            this.btnRfid.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRfid.BackgroundImage")));
            this.btnRfid.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRfid.FlatAppearance.BorderSize = 0;
            this.btnRfid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRfid.Location = new System.Drawing.Point(297, 3);
            this.btnRfid.Name = "btnRfid";
            this.btnRfid.Size = new System.Drawing.Size(34, 34);
            this.btnRfid.TabIndex = 12;
            this.btnRfid.UseVisualStyleBackColor = false;
            this.btnRfid.Click += new System.EventHandler(this.btnRfid_Click);
            // 
            // txtColor
            // 
            this.txtColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtColor.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColor.ForeColor = System.Drawing.Color.Black;
            this.txtColor.Location = new System.Drawing.Point(98, 56);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(162, 33);
            this.txtColor.TabIndex = 7;
            this.txtColor.Visible = false;
            this.txtColor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // lbl_rfid_info
            // 
            this.lbl_rfid_info.AutoSize = true;
            this.lbl_rfid_info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_rfid_info.ForeColor = System.Drawing.Color.Red;
            this.lbl_rfid_info.Location = new System.Drawing.Point(98, 40);
            this.lbl_rfid_info.Name = "lbl_rfid_info";
            this.lbl_rfid_info.Size = new System.Drawing.Size(193, 13);
            this.lbl_rfid_info.TabIndex = 118;
            this.lbl_rfid_info.Text = "Please Tap RFID Card";
            this.lbl_rfid_info.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImage = global::pLocker.Properties.Resources.close_1;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Location = new System.Drawing.Point(603, 311);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(132, 44);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            this.btnDelete.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            this.btnDelete.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDelete_MouseDown);
            this.btnDelete.MouseLeave += new System.EventHandler(this.btnDelete_MouseLeave);
            this.btnDelete.MouseHover += new System.EventHandler(this.btnDelete_MouseHover);
            this.btnDelete.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnDelete_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(3, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 25);
            this.label2.TabIndex = 120;
            this.label2.Text = "Location :";
            // 
            // cboLocation
            // 
            this.cboLocation.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboLocation.ForeColor = System.Drawing.Color.Black;
            this.cboLocation.FormattingEnabled = true;
            this.cboLocation.Location = new System.Drawing.Point(106, 68);
            this.cboLocation.Name = "cboLocation";
            this.cboLocation.Size = new System.Drawing.Size(287, 33);
            this.cboLocation.TabIndex = 1;
            this.cboLocation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // txtID
            // 
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtID.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtID.ForeColor = System.Drawing.Color.Black;
            this.txtID.Location = new System.Drawing.Point(693, 68);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(41, 33);
            this.txtID.TabIndex = 13;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(749, 25);
            this.panel4.TabIndex = 115;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkOrange;
            this.label4.Location = new System.Drawing.Point(-1, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 25);
            this.label4.TabIndex = 122;
            this.label4.Text = "mi Loker Details";
            // 
            // grpRate
            // 
            this.grpRate.BackColor = System.Drawing.Color.Transparent;
            this.grpRate.Controls.Add(this.txtPerHours);
            this.grpRate.Controls.Add(this.txtPerDay);
            this.grpRate.Controls.Add(this.txtMonth);
            this.grpRate.Controls.Add(this.lalHours);
            this.grpRate.Controls.Add(this.lalDay);
            this.grpRate.Controls.Add(this.lblMonth);
            this.grpRate.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpRate.ForeColor = System.Drawing.Color.Silver;
            this.grpRate.Location = new System.Drawing.Point(19, 249);
            this.grpRate.Name = "grpRate";
            this.grpRate.Size = new System.Drawing.Size(625, 57);
            this.grpRate.TabIndex = 8;
            this.grpRate.TabStop = false;
            this.grpRate.Text = "Rate / Per";
            // 
            // txtPerHours
            // 
            this.txtPerHours.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPerHours.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPerHours.ForeColor = System.Drawing.Color.Black;
            this.txtPerHours.Location = new System.Drawing.Point(83, 19);
            this.txtPerHours.Name = "txtPerHours";
            this.txtPerHours.Size = new System.Drawing.Size(81, 33);
            this.txtPerHours.TabIndex = 0;
            this.txtPerHours.Text = "....";
            this.txtPerHours.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPerHours.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // txtPerDay
            // 
            this.txtPerDay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPerDay.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPerDay.ForeColor = System.Drawing.Color.Black;
            this.txtPerDay.Location = new System.Drawing.Point(299, 19);
            this.txtPerDay.Name = "txtPerDay";
            this.txtPerDay.Size = new System.Drawing.Size(81, 33);
            this.txtPerDay.TabIndex = 1;
            this.txtPerDay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPerDay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // txtMonth
            // 
            this.txtMonth.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMonth.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMonth.ForeColor = System.Drawing.Color.Black;
            this.txtMonth.Location = new System.Drawing.Point(490, 19);
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(81, 33);
            this.txtMonth.TabIndex = 2;
            this.txtMonth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtMonth.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // lalHours
            // 
            this.lalHours.AutoSize = true;
            this.lalHours.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lalHours.ForeColor = System.Drawing.Color.Silver;
            this.lalHours.Location = new System.Drawing.Point(11, 23);
            this.lalHours.Name = "lalHours";
            this.lalHours.Size = new System.Drawing.Size(73, 25);
            this.lalHours.TabIndex = 8;
            this.lalHours.Text = "Hours :";
            // 
            // lalDay
            // 
            this.lalDay.AutoSize = true;
            this.lalDay.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lalDay.ForeColor = System.Drawing.Color.Silver;
            this.lalDay.Location = new System.Drawing.Point(244, 23);
            this.lalDay.Name = "lalDay";
            this.lalDay.Size = new System.Drawing.Size(56, 25);
            this.lalDay.TabIndex = 9;
            this.lalDay.Text = "Day :";
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMonth.ForeColor = System.Drawing.Color.Silver;
            this.lblMonth.Location = new System.Drawing.Point(411, 23);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(80, 25);
            this.lblMonth.TabIndex = 10;
            this.lblMonth.Text = "Month :";
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.BackgroundImage = global::pLocker.Properties.Resources.refresh_1;
            this.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Location = new System.Drawing.Point(466, 310);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(132, 44);
            this.btnRefresh.TabIndex = 9;
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnRefresh.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            this.btnRefresh.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRefresh_MouseDown);
            this.btnRefresh.MouseLeave += new System.EventHandler(this.btnRefresh_MouseLeave);
            this.btnRefresh.MouseHover += new System.EventHandler(this.btnRefresh_MouseHover);
            this.btnRefresh.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRefresh_MouseUp);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.Transparent;
            this.btnSave.BackgroundImage = global::pLocker.Properties.Resources.save_1;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Location = new System.Drawing.Point(327, 310);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(132, 44);
            this.btnSave.TabIndex = 8;
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.btnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            this.btnSave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSave_MouseDown);
            this.btnSave.MouseLeave += new System.EventHandler(this.btnSave_MouseLeave);
            this.btnSave.MouseHover += new System.EventHandler(this.btnSave_MouseHover);
            this.btnSave.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnSave_MouseUp);
            // 
            // txtDesc
            // 
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesc.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesc.ForeColor = System.Drawing.Color.Black;
            this.txtDesc.Location = new System.Drawing.Point(106, 177);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(287, 64);
            this.txtDesc.TabIndex = 4;
            this.txtDesc.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.ForeColor = System.Drawing.Color.Silver;
            this.lblDesc.Location = new System.Drawing.Point(24, 182);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(81, 25);
            this.lblDesc.TabIndex = 57;
            this.lblDesc.Text = "Details :";
            // 
            // txtPinNo
            // 
            this.txtPinNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPinNo.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPinNo.ForeColor = System.Drawing.Color.Black;
            this.txtPinNo.Location = new System.Drawing.Point(106, 140);
            this.txtPinNo.MaxLength = 6;
            this.txtPinNo.Name = "txtPinNo";
            this.txtPinNo.Size = new System.Drawing.Size(287, 33);
            this.txtPinNo.TabIndex = 3;
            this.txtPinNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // lblPinNo
            // 
            this.lblPinNo.AutoSize = true;
            this.lblPinNo.BackColor = System.Drawing.Color.Transparent;
            this.lblPinNo.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPinNo.ForeColor = System.Drawing.Color.Silver;
            this.lblPinNo.Location = new System.Drawing.Point(22, 149);
            this.lblPinNo.Name = "lblPinNo";
            this.lblPinNo.Size = new System.Drawing.Size(85, 25);
            this.lblPinNo.TabIndex = 55;
            this.lblPinNo.Text = "PIN No :";
            // 
            // txtMacId
            // 
            this.txtMacId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMacId.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMacId.ForeColor = System.Drawing.Color.Black;
            this.txtMacId.Location = new System.Drawing.Point(106, 103);
            this.txtMacId.Name = "txtMacId";
            this.txtMacId.Size = new System.Drawing.Size(287, 33);
            this.txtMacId.TabIndex = 2;
            this.txtMacId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // lblMacId
            // 
            this.lblMacId.AutoSize = true;
            this.lblMacId.BackColor = System.Drawing.Color.Transparent;
            this.lblMacId.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMacId.ForeColor = System.Drawing.Color.Silver;
            this.lblMacId.Location = new System.Drawing.Point(12, 110);
            this.lblMacId.Name = "lblMacId";
            this.lblMacId.Size = new System.Drawing.Size(90, 25);
            this.lblMacId.TabIndex = 50;
            this.lblMacId.Text = "MAC ID :";
            // 
            // lblRowNo
            // 
            this.lblRowNo.AutoSize = true;
            this.lblRowNo.BackColor = System.Drawing.Color.Transparent;
            this.lblRowNo.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRowNo.ForeColor = System.Drawing.Color.Silver;
            this.lblRowNo.Location = new System.Drawing.Point(395, 68);
            this.lblRowNo.Name = "lblRowNo";
            this.lblRowNo.Size = new System.Drawing.Size(94, 25);
            this.lblRowNo.TabIndex = 49;
            this.lblRowNo.Text = "Row No. :";
            // 
            // txtRowNo
            // 
            this.txtRowNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRowNo.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRowNo.ForeColor = System.Drawing.Color.Black;
            this.txtRowNo.Location = new System.Drawing.Point(497, 68);
            this.txtRowNo.Name = "txtRowNo";
            this.txtRowNo.Size = new System.Drawing.Size(190, 33);
            this.txtRowNo.TabIndex = 5;
            this.txtRowNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // txtLocker
            // 
            this.txtLocker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLocker.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocker.ForeColor = System.Drawing.Color.Black;
            this.txtLocker.Location = new System.Drawing.Point(106, 31);
            this.txtLocker.Name = "txtLocker";
            this.txtLocker.Size = new System.Drawing.Size(287, 33);
            this.txtLocker.TabIndex = 0;
            this.txtLocker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
            // 
            // lblLocker
            // 
            this.lblLocker.AutoSize = true;
            this.lblLocker.BackColor = System.Drawing.Color.Transparent;
            this.lblLocker.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocker.ForeColor = System.Drawing.Color.Silver;
            this.lblLocker.Location = new System.Drawing.Point(22, 40);
            this.lblLocker.Name = "lblLocker";
            this.lblLocker.Size = new System.Drawing.Size(78, 25);
            this.lblLocker.TabIndex = 46;
            this.lblLocker.Text = "Locker :";
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToOrderColumns = true;
            this.dgvList.AllowUserToResizeColumns = false;
            this.dgvList.BackgroundColor = System.Drawing.Color.DarkGray;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.cnLocker,
            this.cnLocation,
            this.cnRowno,
            this.cnMacID,
            this.cnRFID,
            this.cnColor,
            this.cnPINNo,
            this.cnLockDesc,
            this.cnHrRate,
            this.cnDyRate,
            this.cnMnRate,
            this.cbLocationID});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvList.GridColor = System.Drawing.SystemColors.ControlLight;
            this.dgvList.Location = new System.Drawing.Point(8, 106);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvList.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvList.RowTemplate.Height = 25;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(1005, 407);
            this.dgvList.TabIndex = 115;
            this.dgvList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvList_CellMouseDoubleClick);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.MaxInputLength = 15;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // cnLocker
            // 
            this.cnLocker.HeaderText = "Locker";
            this.cnLocker.MaxInputLength = 50;
            this.cnLocker.Name = "cnLocker";
            this.cnLocker.ReadOnly = true;
            this.cnLocker.Width = 200;
            // 
            // cnLocation
            // 
            this.cnLocation.HeaderText = "Location";
            this.cnLocation.Name = "cnLocation";
            this.cnLocation.ReadOnly = true;
            // 
            // cnRowno
            // 
            this.cnRowno.HeaderText = "Row No";
            this.cnRowno.Name = "cnRowno";
            this.cnRowno.ReadOnly = true;
            // 
            // cnMacID
            // 
            this.cnMacID.HeaderText = "MacID";
            this.cnMacID.MaxInputLength = 50;
            this.cnMacID.Name = "cnMacID";
            this.cnMacID.ReadOnly = true;
            // 
            // cnRFID
            // 
            this.cnRFID.HeaderText = "RFID";
            this.cnRFID.MaxInputLength = 50;
            this.cnRFID.Name = "cnRFID";
            this.cnRFID.ReadOnly = true;
            // 
            // cnColor
            // 
            this.cnColor.HeaderText = "Color";
            this.cnColor.MaxInputLength = 50;
            this.cnColor.Name = "cnColor";
            this.cnColor.ReadOnly = true;
            // 
            // cnPINNo
            // 
            this.cnPINNo.HeaderText = "PIN No";
            this.cnPINNo.MaxInputLength = 50;
            this.cnPINNo.Name = "cnPINNo";
            this.cnPINNo.ReadOnly = true;
            // 
            // cnLockDesc
            // 
            this.cnLockDesc.HeaderText = "LockDesc";
            this.cnLockDesc.MaxInputLength = 200;
            this.cnLockDesc.Name = "cnLockDesc";
            this.cnLockDesc.ReadOnly = true;
            this.cnLockDesc.Width = 200;
            // 
            // cnHrRate
            // 
            this.cnHrRate.HeaderText = "Hour/Rate";
            this.cnHrRate.MaxInputLength = 12;
            this.cnHrRate.Name = "cnHrRate";
            this.cnHrRate.ReadOnly = true;
            // 
            // cnDyRate
            // 
            this.cnDyRate.HeaderText = "Day/Rate";
            this.cnDyRate.MaxInputLength = 12;
            this.cnDyRate.Name = "cnDyRate";
            this.cnDyRate.ReadOnly = true;
            // 
            // cnMnRate
            // 
            this.cnMnRate.HeaderText = "Month/Rate";
            this.cnMnRate.MaxInputLength = 12;
            this.cnMnRate.Name = "cnMnRate";
            this.cnMnRate.ReadOnly = true;
            // 
            // cbLocationID
            // 
            this.cbLocationID.HeaderText = "LocnID";
            this.cbLocationID.Name = "cbLocationID";
            this.cbLocationID.ReadOnly = true;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmLocker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1024, 540);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "FrmLocker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmLocker";
            this.Load += new System.EventHandler(this.FrmLocker_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmLocker_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnlLocker.ResumeLayout(false);
            this.pnlLocker.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.grpRate.ResumeLayout(false);
            this.grpRate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Button btnFrmClose;
        private System.Windows.Forms.Panel pnlLocker;
        private System.Windows.Forms.GroupBox grpRate;
        private System.Windows.Forms.TextBox txtPerHours;
        private System.Windows.Forms.TextBox txtPerDay;
        private System.Windows.Forms.TextBox txtMonth;
        private System.Windows.Forms.Label lalHours;
        private System.Windows.Forms.Label lalDay;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.TextBox txtPinNo;
        private System.Windows.Forms.Label lblPinNo;
        private System.Windows.Forms.Button btnRfid;
        private System.Windows.Forms.TextBox txtRFID;
        private System.Windows.Forms.Label lblRfid;
        private System.Windows.Forms.TextBox txtMacId;
        private System.Windows.Forms.Label lblMacId;
        private System.Windows.Forms.Label lblRowNo;
        private System.Windows.Forms.TextBox txtRowNo;
        private System.Windows.Forms.TextBox txtLocker;
        private System.Windows.Forms.Label lblLocker;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboLocation;
        private System.Windows.Forms.Label lblTotLocker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mtxtFieldValue;
        private System.Windows.Forms.ComboBox cboField;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnLocker;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnRowno;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnMacID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnRFID;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnPINNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnLockDesc;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnHrRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnDyRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cnMnRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn cbLocationID;
        private System.Windows.Forms.Button btnColorPlate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lbl_rfid_info;
        private System.Windows.Forms.TextBox txtRfid_2;
    }
}