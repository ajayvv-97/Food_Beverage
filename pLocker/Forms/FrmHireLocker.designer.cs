namespace pLocker.Forms
{
    partial class FrmHireLocker
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHireLocker));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label26 = new System.Windows.Forms.Label();
			this.txtinvoiceno = new System.Windows.Forms.TextBox();
			this.lblRfid = new System.Windows.Forms.Label();
			this.txtICardNo_2 = new System.Windows.Forms.TextBox();
			this.grpBoxPrint = new System.Windows.Forms.GroupBox();
			this.Cancel = new System.Windows.Forms.Button();
			this.Save_Print = new System.Windows.Forms.Button();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.chBA4 = new System.Windows.Forms.CheckBox();
			this.chBPreview = new System.Windows.Forms.CheckBox();
			this.lblPin = new System.Windows.Forms.Label();
			this.pbCapture = new System.Windows.Forms.PictureBox();
			this.label23 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.txtHireDetails = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.txtICardNo = new System.Windows.Forms.TextBox();
			this.cmbPaymentMode = new System.Windows.Forms.ComboBox();
			this.txtDiscountAmt = new System.Windows.Forms.TextBox();
			this.txtNetAmt = new System.Windows.Forms.TextBox();
			this.txtAmtAdjust = new System.Windows.Forms.TextBox();
			this.txtBalAmt = new System.Windows.Forms.TextBox();
			this.txtPaidAmt = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.chkHired = new System.Windows.Forms.CheckBox();
			this.lblToolTipAccName = new System.Windows.Forms.Label();
			this.txtLedgerID = new System.Windows.Forms.TextBox();
			this.txtLockHireID = new System.Windows.Forms.TextBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnRFID = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label16 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.dtpHireTime = new System.Windows.Forms.DateTimePicker();
			this.dtpHireDate = new System.Windows.Forms.DateTimePicker();
			this.txtAmount = new System.Windows.Forms.TextBox();
			this.lblRate = new System.Windows.Forms.Label();
			this.dtpExpTime = new System.Windows.Forms.DateTimePicker();
			this.txtHireForNo = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.dtpExpDate = new System.Windows.Forms.DateTimePicker();
			this.cboHireFor = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnMin = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.btnFrmClose = new System.Windows.Forms.Button();
			this.cboLockStatus = new System.Windows.Forms.ComboBox();
			this.label15 = new System.Windows.Forms.Label();
			this.txtCustomerPIN = new System.Windows.Forms.TextBox();
			this.cboLocker = new System.Windows.Forms.ComboBox();
			this.lblLocker = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.txtAccNO = new System.Windows.Forms.TextBox();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.txtRemarks = new System.Windows.Forms.TextBox();
			this.txtAccEmail = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtAccAddress = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtAccPhone = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnAdd = new System.Windows.Forms.Button();
			this.txtAccName = new System.Windows.Forms.TextBox();
			this.pnlRfidList = new System.Windows.Forms.Panel();
			this.btnRfidListOk = new System.Windows.Forms.Button();
			this.dgvRfidList = new System.Windows.Forms.DataGridView();
			this.panel4 = new System.Windows.Forms.Panel();
			this.label24 = new System.Windows.Forms.Label();
			this.btnRfidListClose = new System.Windows.Forms.Button();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.CardNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.rfid = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panel1.SuspendLayout();
			this.grpBoxPrint.SuspendLayout();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbCapture)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.pnlRfidList.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvRfidList)).BeginInit();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.label26);
			this.panel1.Controls.Add(this.txtinvoiceno);
			this.panel1.Controls.Add(this.lblRfid);
			this.panel1.Controls.Add(this.txtICardNo_2);
			this.panel1.Controls.Add(this.grpBoxPrint);
			this.panel1.Controls.Add(this.lblPin);
			this.panel1.Controls.Add(this.pbCapture);
			this.panel1.Controls.Add(this.label23);
			this.panel1.Controls.Add(this.label22);
			this.panel1.Controls.Add(this.txtHireDetails);
			this.panel1.Controls.Add(this.label19);
			this.panel1.Controls.Add(this.txtICardNo);
			this.panel1.Controls.Add(this.cmbPaymentMode);
			this.panel1.Controls.Add(this.txtDiscountAmt);
			this.panel1.Controls.Add(this.txtNetAmt);
			this.panel1.Controls.Add(this.txtAmtAdjust);
			this.panel1.Controls.Add(this.txtBalAmt);
			this.panel1.Controls.Add(this.txtPaidAmt);
			this.panel1.Controls.Add(this.label18);
			this.panel1.Controls.Add(this.label17);
			this.panel1.Controls.Add(this.label13);
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.label11);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.chkHired);
			this.panel1.Controls.Add(this.lblToolTipAccName);
			this.panel1.Controls.Add(this.txtLedgerID);
			this.panel1.Controls.Add(this.txtLockHireID);
			this.panel1.Controls.Add(this.btnClose);
			this.panel1.Controls.Add(this.btnRFID);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.cboLockStatus);
			this.panel1.Controls.Add(this.label15);
			this.panel1.Controls.Add(this.txtCustomerPIN);
			this.panel1.Controls.Add(this.cboLocker);
			this.panel1.Controls.Add(this.lblLocker);
			this.panel1.Controls.Add(this.label14);
			this.panel1.Controls.Add(this.txtAccNO);
			this.panel1.Controls.Add(this.btnRefresh);
			this.panel1.Controls.Add(this.btnSave);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.txtRemarks);
			this.panel1.Controls.Add(this.txtAccEmail);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.txtAccAddress);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.txtAccPhone);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.btnAdd);
			this.panel1.Controls.Add(this.txtAccName);
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(916, 588);
			this.panel1.TabIndex = 0;
			// 
			// label26
			// 
			this.label26.AutoSize = true;
			this.label26.BackColor = System.Drawing.Color.Transparent;
			this.label26.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label26.ForeColor = System.Drawing.Color.Silver;
			this.label26.Location = new System.Drawing.Point(19, 40);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(115, 25);
			this.label26.TabIndex = 176;
			this.label26.Text = "Invoice No :";
			// 
			// txtinvoiceno
			// 
			this.txtinvoiceno.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtinvoiceno.Location = new System.Drawing.Point(138, 40);
			this.txtinvoiceno.Multiline = true;
			this.txtinvoiceno.Name = "txtinvoiceno";
			this.txtinvoiceno.ReadOnly = true;
			this.txtinvoiceno.Size = new System.Drawing.Size(279, 29);
			this.txtinvoiceno.TabIndex = 0;
			// 
			// lblRfid
			// 
			this.lblRfid.AutoSize = true;
			this.lblRfid.BackColor = System.Drawing.Color.Transparent;
			this.lblRfid.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRfid.ForeColor = System.Drawing.Color.Red;
			this.lblRfid.Location = new System.Drawing.Point(269, 200);
			this.lblRfid.Name = "lblRfid";
			this.lblRfid.Size = new System.Drawing.Size(146, 16);
			this.lblRfid.TabIndex = 173;
			this.lblRfid.Text = "Please Tap RFID Card";
			this.lblRfid.Visible = false;
			// 
			// txtICardNo_2
			// 
			this.txtICardNo_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtICardNo_2.Location = new System.Drawing.Point(138, 181);
			this.txtICardNo_2.Multiline = true;
			this.txtICardNo_2.Name = "txtICardNo_2";
			this.txtICardNo_2.ReadOnly = true;
			this.txtICardNo_2.Size = new System.Drawing.Size(280, 29);
			this.txtICardNo_2.TabIndex = 4;
			this.txtICardNo_2.Enter += new System.EventHandler(this.txtICardNo_2_Enter);
			this.txtICardNo_2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtICardNo_2_KeyDown);
			this.txtICardNo_2.Leave += new System.EventHandler(this.txtICardNo_2_Leave);
			// 
			// grpBoxPrint
			// 
			this.grpBoxPrint.BackColor = System.Drawing.Color.LightGray;
			this.grpBoxPrint.Controls.Add(this.Cancel);
			this.grpBoxPrint.Controls.Add(this.Save_Print);
			this.grpBoxPrint.Controls.Add(this.groupBox3);
			this.grpBoxPrint.Location = new System.Drawing.Point(89, 546);
			this.grpBoxPrint.Name = "grpBoxPrint";
			this.grpBoxPrint.Size = new System.Drawing.Size(32, 30);
			this.grpBoxPrint.TabIndex = 1;
			this.grpBoxPrint.TabStop = false;
			this.grpBoxPrint.Visible = false;
			// 
			// Cancel
			// 
			this.Cancel.Location = new System.Drawing.Point(138, 85);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(113, 39);
			this.Cancel.TabIndex = 129;
			this.Cancel.Text = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			// 
			// Save_Print
			// 
			this.Save_Print.FlatAppearance.BorderSize = 0;
			this.Save_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Save_Print.Location = new System.Drawing.Point(23, 86);
			this.Save_Print.Name = "Save_Print";
			this.Save_Print.Size = new System.Drawing.Size(109, 38);
			this.Save_Print.TabIndex = 128;
			this.Save_Print.Text = "save";
			this.Save_Print.UseVisualStyleBackColor = true;
			this.Save_Print.Click += new System.EventHandler(this.Save_Print_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.chBA4);
			this.groupBox3.Controls.Add(this.chBPreview);
			this.groupBox3.Location = new System.Drawing.Point(18, 22);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(237, 37);
			this.groupBox3.TabIndex = 17;
			this.groupBox3.TabStop = false;
			// 
			// chBA4
			// 
			this.chBA4.AutoSize = true;
			this.chBA4.Font = new System.Drawing.Font("Century Gothic", 14.25F);
			this.chBA4.Location = new System.Drawing.Point(162, 9);
			this.chBA4.Name = "chBA4";
			this.chBA4.Size = new System.Drawing.Size(55, 26);
			this.chBA4.TabIndex = 132;
			this.chBA4.Text = "A4";
			this.chBA4.UseVisualStyleBackColor = true;
			// 
			// chBPreview
			// 
			this.chBPreview.AutoSize = true;
			this.chBPreview.Checked = true;
			this.chBPreview.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBPreview.Font = new System.Drawing.Font("Century Gothic", 14.25F);
			this.chBPreview.Location = new System.Drawing.Point(16, 7);
			this.chBPreview.Name = "chBPreview";
			this.chBPreview.Size = new System.Drawing.Size(102, 26);
			this.chBPreview.TabIndex = 131;
			this.chBPreview.Text = "Preview";
			this.chBPreview.UseVisualStyleBackColor = true;
			// 
			// lblPin
			// 
			this.lblPin.AutoSize = true;
			this.lblPin.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPin.ForeColor = System.Drawing.Color.Red;
			this.lblPin.Location = new System.Drawing.Point(832, 103);
			this.lblPin.Name = "lblPin";
			this.lblPin.Size = new System.Drawing.Size(50, 16);
			this.lblPin.TabIndex = 171;
			this.lblPin.Text = "123456";
			this.lblPin.Visible = false;
			// 
			// pbCapture
			// 
			this.pbCapture.Location = new System.Drawing.Point(500, 40);
			this.pbCapture.Name = "pbCapture";
			this.pbCapture.Size = new System.Drawing.Size(124, 128);
			this.pbCapture.TabIndex = 1;
			this.pbCapture.TabStop = false;
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.BackColor = System.Drawing.Color.Transparent;
			this.label23.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label23.ForeColor = System.Drawing.Color.Red;
			this.label23.Location = new System.Drawing.Point(471, 238);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(12, 15);
			this.label23.TabIndex = 170;
			this.label23.Text = "*";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.BackColor = System.Drawing.Color.Transparent;
			this.label22.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label22.ForeColor = System.Drawing.Color.Red;
			this.label22.Location = new System.Drawing.Point(457, 145);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(12, 15);
			this.label22.TabIndex = 169;
			this.label22.Text = "*";
			// 
			// txtHireDetails
			// 
			this.txtHireDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtHireDetails.ForeColor = System.Drawing.Color.Red;
			this.txtHireDetails.Location = new System.Drawing.Point(635, 135);
			this.txtHireDetails.Multiline = true;
			this.txtHireDetails.Name = "txtHireDetails";
			this.txtHireDetails.ReadOnly = true;
			this.txtHireDetails.Size = new System.Drawing.Size(274, 33);
			this.txtHireDetails.TabIndex = 20;
			this.txtHireDetails.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtHireDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.BackColor = System.Drawing.Color.Transparent;
			this.label20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label20.ForeColor = System.Drawing.Color.Red;
			this.label20.Location = new System.Drawing.Point(254, 57);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(12, 15);
			this.label20.TabIndex = 167;
			this.label20.Text = "*";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.BackColor = System.Drawing.Color.Transparent;
			this.label19.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label19.ForeColor = System.Drawing.Color.Silver;
			this.label19.Location = new System.Drawing.Point(40, 181);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(94, 25);
			this.label19.TabIndex = 127;
			this.label19.Text = "ICardNo :";
			// 
			// txtICardNo
			// 
			this.txtICardNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtICardNo.Location = new System.Drawing.Point(127, 546);
			this.txtICardNo.Multiline = true;
			this.txtICardNo.Name = "txtICardNo";
			this.txtICardNo.ReadOnly = true;
			this.txtICardNo.Size = new System.Drawing.Size(34, 30);
			this.txtICardNo.TabIndex = 2;
			this.txtICardNo.Visible = false;
			this.txtICardNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			// 
			// cmbPaymentMode
			// 
			this.cmbPaymentMode.BackColor = System.Drawing.Color.White;
			this.cmbPaymentMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbPaymentMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmbPaymentMode.FormattingEnabled = true;
			this.cmbPaymentMode.Location = new System.Drawing.Point(505, 469);
			this.cmbPaymentMode.MaxLength = 4;
			this.cmbPaymentMode.Name = "cmbPaymentMode";
			this.cmbPaymentMode.Size = new System.Drawing.Size(123, 32);
			this.cmbPaymentMode.TabIndex = 14;
			this.cmbPaymentMode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			// 
			// txtDiscountAmt
			// 
			this.txtDiscountAmt.BackColor = System.Drawing.Color.White;
			this.txtDiscountAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtDiscountAmt.ForeColor = System.Drawing.Color.OrangeRed;
			this.txtDiscountAmt.Location = new System.Drawing.Point(505, 409);
			this.txtDiscountAmt.MaxLength = 7;
			this.txtDiscountAmt.Name = "txtDiscountAmt";
			this.txtDiscountAmt.Size = new System.Drawing.Size(125, 29);
			this.txtDiscountAmt.TabIndex = 11;
			this.txtDiscountAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtDiscountAmt.Enter += new System.EventHandler(this.txtDiscountAmt_Enter);
			this.txtDiscountAmt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			this.txtDiscountAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDiscountAmt_KeyPress);
			this.txtDiscountAmt.Leave += new System.EventHandler(this.txtDiscountAmt_Leave);
			// 
			// txtNetAmt
			// 
			this.txtNetAmt.BackColor = System.Drawing.Color.Black;
			this.txtNetAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtNetAmt.ForeColor = System.Drawing.Color.OrangeRed;
			this.txtNetAmt.Location = new System.Drawing.Point(800, 408);
			this.txtNetAmt.MaxLength = 7;
			this.txtNetAmt.Name = "txtNetAmt";
			this.txtNetAmt.ReadOnly = true;
			this.txtNetAmt.Size = new System.Drawing.Size(97, 29);
			this.txtNetAmt.TabIndex = 13;
			this.txtNetAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtNetAmt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			// 
			// txtAmtAdjust
			// 
			this.txtAmtAdjust.BackColor = System.Drawing.Color.White;
			this.txtAmtAdjust.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAmtAdjust.ForeColor = System.Drawing.Color.OrangeRed;
			this.txtAmtAdjust.Location = new System.Drawing.Point(661, 408);
			this.txtAmtAdjust.MaxLength = 7;
			this.txtAmtAdjust.Name = "txtAmtAdjust";
			this.txtAmtAdjust.Size = new System.Drawing.Size(108, 29);
			this.txtAmtAdjust.TabIndex = 12;
			this.txtAmtAdjust.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtAmtAdjust.Enter += new System.EventHandler(this.txtAmtAdjust_Enter);
			this.txtAmtAdjust.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			this.txtAmtAdjust.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAmtAdjust_KeyPress);
			this.txtAmtAdjust.Leave += new System.EventHandler(this.txtAmtAdjust_Leave);
			// 
			// txtBalAmt
			// 
			this.txtBalAmt.BackColor = System.Drawing.Color.White;
			this.txtBalAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtBalAmt.ForeColor = System.Drawing.Color.OrangeRed;
			this.txtBalAmt.Location = new System.Drawing.Point(800, 469);
			this.txtBalAmt.MaxLength = 7;
			this.txtBalAmt.Name = "txtBalAmt";
			this.txtBalAmt.ReadOnly = true;
			this.txtBalAmt.Size = new System.Drawing.Size(102, 29);
			this.txtBalAmt.TabIndex = 16;
			this.txtBalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtBalAmt.Enter += new System.EventHandler(this.txtBalAmt_Enter);
			this.txtBalAmt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			// 
			// txtPaidAmt
			// 
			this.txtPaidAmt.BackColor = System.Drawing.Color.White;
			this.txtPaidAmt.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPaidAmt.ForeColor = System.Drawing.Color.OrangeRed;
			this.txtPaidAmt.Location = new System.Drawing.Point(661, 469);
			this.txtPaidAmt.MaxLength = 7;
			this.txtPaidAmt.Name = "txtPaidAmt";
			this.txtPaidAmt.Size = new System.Drawing.Size(108, 29);
			this.txtPaidAmt.TabIndex = 15;
			this.txtPaidAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtPaidAmt.Enter += new System.EventHandler(this.txtPaidAmt_Enter);
			this.txtPaidAmt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			this.txtPaidAmt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPaidAmt_KeyPress);
			this.txtPaidAmt.Leave += new System.EventHandler(this.txtPaidAmt_Leave);
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.BackColor = System.Drawing.Color.Transparent;
			this.label18.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label18.ForeColor = System.Drawing.Color.Silver;
			this.label18.Location = new System.Drawing.Point(505, 381);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(100, 25);
			this.label18.TabIndex = 125;
			this.label18.Text = "Disc.Amt :";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.BackColor = System.Drawing.Color.Transparent;
			this.label17.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label17.ForeColor = System.Drawing.Color.Silver;
			this.label17.Location = new System.Drawing.Point(661, 381);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(93, 25);
			this.label17.TabIndex = 124;
			this.label17.Text = "Amt Adj :";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.BackColor = System.Drawing.Color.Transparent;
			this.label13.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.ForeColor = System.Drawing.Color.Silver;
			this.label13.Location = new System.Drawing.Point(661, 441);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(95, 25);
			this.label13.TabIndex = 123;
			this.label13.Text = "Paid Amt:";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.BackColor = System.Drawing.Color.Transparent;
			this.label12.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.ForeColor = System.Drawing.Color.Silver;
			this.label12.Location = new System.Drawing.Point(800, 441);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(90, 25);
			this.label12.TabIndex = 122;
			this.label12.Text = "Bal Amt :";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.BackColor = System.Drawing.Color.Transparent;
			this.label11.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.ForeColor = System.Drawing.Color.Silver;
			this.label11.Location = new System.Drawing.Point(800, 381);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(96, 25);
			this.label11.TabIndex = 121;
			this.label11.Text = "Net Amt :";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.BackColor = System.Drawing.Color.Transparent;
			this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.ForeColor = System.Drawing.Color.Silver;
			this.label10.Location = new System.Drawing.Point(505, 441);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(107, 25);
			this.label10.TabIndex = 120;
			this.label10.Text = "Pay Mode :";
			// 
			// chkHired
			// 
			this.chkHired.AutoSize = true;
			this.chkHired.Checked = true;
			this.chkHired.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkHired.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.chkHired.ForeColor = System.Drawing.Color.Silver;
			this.chkHired.Location = new System.Drawing.Point(426, 40);
			this.chkHired.Name = "chkHired";
			this.chkHired.Size = new System.Drawing.Size(78, 29);
			this.chkHired.TabIndex = 119;
			this.chkHired.Text = "Hired";
			this.chkHired.UseVisualStyleBackColor = true;
			// 
			// lblToolTipAccName
			// 
			this.lblToolTipAccName.AutoSize = true;
			this.lblToolTipAccName.BackColor = System.Drawing.Color.Transparent;
			this.lblToolTipAccName.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblToolTipAccName.ForeColor = System.Drawing.Color.Red;
			this.lblToolTipAccName.Location = new System.Drawing.Point(273, 154);
			this.lblToolTipAccName.Name = "lblToolTipAccName";
			this.lblToolTipAccName.Size = new System.Drawing.Size(135, 16);
			this.lblToolTipAccName.TabIndex = 4;
			this.lblToolTipAccName.Text = "[F8]-For Account List";
			this.lblToolTipAccName.Visible = false;
			// 
			// txtLedgerID
			// 
			this.txtLedgerID.Font = new System.Drawing.Font("Century Gothic", 14.25F);
			this.txtLedgerID.Location = new System.Drawing.Point(49, 546);
			this.txtLedgerID.Name = "txtLedgerID";
			this.txtLedgerID.Size = new System.Drawing.Size(34, 31);
			this.txtLedgerID.TabIndex = 117;
			this.txtLedgerID.Text = "0";
			this.txtLedgerID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtLedgerID.Visible = false;
			// 
			// txtLockHireID
			// 
			this.txtLockHireID.Font = new System.Drawing.Font("Century Gothic", 14.25F);
			this.txtLockHireID.Location = new System.Drawing.Point(9, 546);
			this.txtLockHireID.Name = "txtLockHireID";
			this.txtLockHireID.Size = new System.Drawing.Size(34, 31);
			this.txtLockHireID.TabIndex = 86;
			this.txtLockHireID.Text = "0";
			this.txtLockHireID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtLockHireID.Visible = false;
			// 
			// btnClose
			// 
			this.btnClose.BackColor = System.Drawing.Color.Transparent;
			this.btnClose.BackgroundImage = global::pLocker.Properties.Resources.close_1;
			this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnClose.FlatAppearance.BorderSize = 0;
			this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnClose.Location = new System.Drawing.Point(763, 515);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(132, 44);
			this.btnClose.TabIndex = 19;
			this.btnClose.UseVisualStyleBackColor = false;
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			this.btnClose.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			this.btnClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnClose_MouseDown);
			this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
			this.btnClose.MouseHover += new System.EventHandler(this.btnClose_MouseHover);
			this.btnClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnClose_MouseUp);
			// 
			// btnRFID
			// 
			this.btnRFID.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRFID.BackgroundImage")));
			this.btnRFID.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnRFID.FlatAppearance.BorderSize = 0;
			this.btnRFID.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRFID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRFID.Location = new System.Drawing.Point(420, 175);
			this.btnRFID.Name = "btnRFID";
			this.btnRFID.Size = new System.Drawing.Size(50, 44);
			this.btnRFID.TabIndex = 22;
			this.btnRFID.UseVisualStyleBackColor = true;
			this.btnRFID.Click += new System.EventHandler(this.btnRFID_Click);
			this.btnRFID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label16);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.dtpHireTime);
			this.groupBox1.Controls.Add(this.dtpHireDate);
			this.groupBox1.Controls.Add(this.txtAmount);
			this.groupBox1.Controls.Add(this.lblRate);
			this.groupBox1.Controls.Add(this.dtpExpTime);
			this.groupBox1.Controls.Add(this.txtHireForNo);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.dtpExpDate);
			this.groupBox1.Controls.Add(this.cboHireFor);
			this.groupBox1.Controls.Add(this.label20);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.ForeColor = System.Drawing.Color.Silver;
			this.groupBox1.Location = new System.Drawing.Point(489, 169);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(420, 210);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Hire Details :";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.BackColor = System.Drawing.Color.Transparent;
			this.label16.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label16.ForeColor = System.Drawing.Color.Silver;
			this.label16.Location = new System.Drawing.Point(46, 23);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(61, 25);
			this.label16.TabIndex = 85;
			this.label16.Text = "Rate :";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.BackColor = System.Drawing.Color.Transparent;
			this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.ForeColor = System.Drawing.Color.Silver;
			this.label7.Location = new System.Drawing.Point(16, 85);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(220, 25);
			this.label7.TabIndex = 84;
			this.label7.Text = "Hire From Date && Time :";
			// 
			// dtpHireTime
			// 
			this.dtpHireTime.CalendarFont = new System.Drawing.Font("Century Gothic", 14.25F);
			this.dtpHireTime.CustomFormat = "hh:mm";
			this.dtpHireTime.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
			this.dtpHireTime.Enabled = false;
			this.dtpHireTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpHireTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dtpHireTime.Location = new System.Drawing.Point(271, 111);
			this.dtpHireTime.Name = "dtpHireTime";
			this.dtpHireTime.ShowUpDown = true;
			this.dtpHireTime.Size = new System.Drawing.Size(140, 29);
			this.dtpHireTime.TabIndex = 4;
			this.dtpHireTime.Tag = "Time In";
			this.dtpHireTime.ValueChanged += new System.EventHandler(this.dtpHireTime_ValueChanged);
			this.dtpHireTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			// 
			// dtpHireDate
			// 
			this.dtpHireDate.Enabled = false;
			this.dtpHireDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpHireDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpHireDate.Location = new System.Drawing.Point(101, 112);
			this.dtpHireDate.Name = "dtpHireDate";
			this.dtpHireDate.Size = new System.Drawing.Size(151, 29);
			this.dtpHireDate.TabIndex = 3;
			this.dtpHireDate.ValueChanged += new System.EventHandler(this.dtpHireDate_ValueChanged);
			this.dtpHireDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			// 
			// txtAmount
			// 
			this.txtAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAmount.Location = new System.Drawing.Point(272, 50);
			this.txtAmount.Name = "txtAmount";
			this.txtAmount.ReadOnly = true;
			this.txtAmount.Size = new System.Drawing.Size(140, 29);
			this.txtAmount.TabIndex = 2;
			this.txtAmount.Text = "0";
			this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			// 
			// lblRate
			// 
			this.lblRate.BackColor = System.Drawing.Color.Black;
			this.lblRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRate.ForeColor = System.Drawing.Color.OrangeRed;
			this.lblRate.Location = new System.Drawing.Point(107, 18);
			this.lblRate.Name = "lblRate";
			this.lblRate.Size = new System.Drawing.Size(94, 29);
			this.lblRate.TabIndex = 8;
			this.lblRate.Text = "0.00";
			this.lblRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtpExpTime
			// 
			this.dtpExpTime.CalendarFont = new System.Drawing.Font("Century Gothic", 14.25F);
			this.dtpExpTime.CustomFormat = "hh:mm";
			this.dtpExpTime.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
			this.dtpExpTime.Enabled = false;
			this.dtpExpTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpExpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dtpExpTime.Location = new System.Drawing.Point(271, 172);
			this.dtpExpTime.Name = "dtpExpTime";
			this.dtpExpTime.ShowUpDown = true;
			this.dtpExpTime.Size = new System.Drawing.Size(140, 29);
			this.dtpExpTime.TabIndex = 6;
			this.dtpExpTime.Tag = "Time In";
			this.dtpExpTime.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			// 
			// txtHireForNo
			// 
			this.txtHireForNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtHireForNo.Location = new System.Drawing.Point(101, 51);
			this.txtHireForNo.Name = "txtHireForNo";
			this.txtHireForNo.Size = new System.Drawing.Size(34, 29);
			this.txtHireForNo.TabIndex = 0;
			this.txtHireForNo.Text = "1";
			this.txtHireForNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtHireForNo.TextChanged += new System.EventHandler(this.txtHireForNo_TextChanged);
			this.txtHireForNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			this.txtHireForNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtHireForNo_KeyPress);
			this.txtHireForNo.Leave += new System.EventHandler(this.txtHireForNo_Leave);
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.BackColor = System.Drawing.Color.Transparent;
			this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.ForeColor = System.Drawing.Color.Silver;
			this.label9.Location = new System.Drawing.Point(16, 50);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(91, 25);
			this.label9.TabIndex = 79;
			this.label9.Text = "Hire For :";
			// 
			// dtpExpDate
			// 
			this.dtpExpDate.Enabled = false;
			this.dtpExpDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dtpExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpExpDate.Location = new System.Drawing.Point(99, 172);
			this.dtpExpDate.Name = "dtpExpDate";
			this.dtpExpDate.Size = new System.Drawing.Size(151, 29);
			this.dtpExpDate.TabIndex = 5;
			this.dtpExpDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			// 
			// cboHireFor
			// 
			this.cboHireFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboHireFor.FormattingEnabled = true;
			this.cboHireFor.Location = new System.Drawing.Point(140, 50);
			this.cboHireFor.Name = "cboHireFor";
			this.cboHireFor.Size = new System.Drawing.Size(112, 32);
			this.cboHireFor.TabIndex = 1;
			this.cboHireFor.SelectedIndexChanged += new System.EventHandler(this.cboHireFor_SelectedIndexChanged);
			this.cboHireFor.SelectionChangeCommitted += new System.EventHandler(this.cboHireFor_SelectionChangeCommitted);
			this.cboHireFor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.Transparent;
			this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.Silver;
			this.label5.Location = new System.Drawing.Point(6, 145);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(193, 25);
			this.label5.TabIndex = 58;
			this.label5.Text = "Hire to Date && Time :";
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(4)))), ((int)(((byte)(209)))));
			this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
			this.panel3.Controls.Add(this.btnMin);
			this.panel3.Controls.Add(this.label8);
			this.panel3.Controls.Add(this.btnFrmClose);
			this.panel3.Location = new System.Drawing.Point(1, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(914, 34);
			this.panel3.TabIndex = 0;
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
			this.btnMin.Location = new System.Drawing.Point(853, 2);
			this.btnMin.Name = "btnMin";
			this.btnMin.Size = new System.Drawing.Size(27, 30);
			this.btnMin.TabIndex = 176;
			this.btnMin.UseVisualStyleBackColor = false;
			this.btnMin.Click += new System.EventHandler(this.Minimize);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.BackColor = System.Drawing.Color.Transparent;
			this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
			this.label8.Location = new System.Drawing.Point(3, 4);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(112, 25);
			this.label8.TabIndex = 67;
			this.label8.Text = "Hire Details";
			// 
			// btnFrmClose
			// 
			this.btnFrmClose.BackColor = System.Drawing.Color.Transparent;
			this.btnFrmClose.BackgroundImage = global::pLocker.Properties.Resources.close_1_button;
			this.btnFrmClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnFrmClose.FlatAppearance.BorderSize = 0;
			this.btnFrmClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnFrmClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnFrmClose.ForeColor = System.Drawing.Color.Transparent;
			this.btnFrmClose.Location = new System.Drawing.Point(883, 2);
			this.btnFrmClose.Name = "btnFrmClose";
			this.btnFrmClose.Size = new System.Drawing.Size(26, 30);
			this.btnFrmClose.TabIndex = 66;
			this.btnFrmClose.UseVisualStyleBackColor = false;
			this.btnFrmClose.Click += new System.EventHandler(this.btnFrmClose_Click);
			// 
			// cboLockStatus
			// 
			this.cboLockStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboLockStatus.FormattingEnabled = true;
			this.cboLockStatus.Location = new System.Drawing.Point(298, 86);
			this.cboLockStatus.Name = "cboLockStatus";
			this.cboLockStatus.Size = new System.Drawing.Size(120, 32);
			this.cboLockStatus.TabIndex = 2;
			this.cboLockStatus.SelectionChangeCommitted += new System.EventHandler(this.cboLockStatus_SelectionChangeCommitted);
			this.cboLockStatus.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.BackColor = System.Drawing.Color.Transparent;
			this.label15.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label15.ForeColor = System.Drawing.Color.Silver;
			this.label15.Location = new System.Drawing.Point(673, 56);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(75, 25);
			this.label15.TabIndex = 74;
			this.label15.Text = "Pin No:";
			// 
			// txtCustomerPIN
			// 
			this.txtCustomerPIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCustomerPIN.Location = new System.Drawing.Point(705, 83);
			this.txtCustomerPIN.MaxLength = 6;
			this.txtCustomerPIN.Multiline = true;
			this.txtCustomerPIN.Name = "txtCustomerPIN";
			this.txtCustomerPIN.Size = new System.Drawing.Size(177, 29);
			this.txtCustomerPIN.TabIndex = 9;
			this.txtCustomerPIN.Enter += new System.EventHandler(this.txtCustomerPIN_Enter);
			this.txtCustomerPIN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			this.txtCustomerPIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCustomerPIN_KeyPress);
			this.txtCustomerPIN.Leave += new System.EventHandler(this.txtCustomerPIN_Leave);
			// 
			// cboLocker
			// 
			this.cboLocker.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cboLocker.FormattingEnabled = true;
			this.cboLocker.Location = new System.Drawing.Point(138, 86);
			this.cboLocker.Name = "cboLocker";
			this.cboLocker.Size = new System.Drawing.Size(147, 32);
			this.cboLocker.TabIndex = 1;
			// 
			// lblLocker
			// 
			this.lblLocker.AutoSize = true;
			this.lblLocker.BackColor = System.Drawing.Color.Transparent;
			this.lblLocker.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLocker.ForeColor = System.Drawing.Color.Silver;
			this.lblLocker.Location = new System.Drawing.Point(56, 86);
			this.lblLocker.Name = "lblLocker";
			this.lblLocker.Size = new System.Drawing.Size(78, 25);
			this.lblLocker.TabIndex = 71;
			this.lblLocker.Text = "Locker :";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.BackColor = System.Drawing.Color.Transparent;
			this.label14.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.ForeColor = System.Drawing.Color.Silver;
			this.label14.Location = new System.Drawing.Point(60, 135);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(74, 25);
			this.label14.TabIndex = 70;
			this.label14.Text = "Name :";
			// 
			// txtAccNO
			// 
			this.txtAccNO.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAccNO.Location = new System.Drawing.Point(167, 546);
			this.txtAccNO.Multiline = true;
			this.txtAccNO.Name = "txtAccNO";
			this.txtAccNO.Size = new System.Drawing.Size(32, 29);
			this.txtAccNO.TabIndex = 5;
			this.txtAccNO.Visible = false;
			this.txtAccNO.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAccNO_KeyDown);
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
			this.btnRefresh.Location = new System.Drawing.Point(626, 515);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(132, 44);
			this.btnRefresh.TabIndex = 18;
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
			this.btnSave.Location = new System.Drawing.Point(489, 515);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(132, 44);
			this.btnSave.TabIndex = 17;
			this.btnSave.UseVisualStyleBackColor = false;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.btnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			this.btnSave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSave_MouseDown);
			this.btnSave.MouseLeave += new System.EventHandler(this.btnSave_MouseLeave);
			this.btnSave.MouseHover += new System.EventHandler(this.btnSave_MouseHover);
			this.btnSave.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnSave_MouseUp);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.Transparent;
			this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.Silver;
			this.label6.Location = new System.Drawing.Point(39, 466);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(95, 25);
			this.label6.TabIndex = 61;
			this.label6.Text = "Remarks :";
			// 
			// txtRemarks
			// 
			this.txtRemarks.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtRemarks.Location = new System.Drawing.Point(138, 466);
			this.txtRemarks.Multiline = true;
			this.txtRemarks.Name = "txtRemarks";
			this.txtRemarks.Size = new System.Drawing.Size(331, 29);
			this.txtRemarks.TabIndex = 8;
			this.txtRemarks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			// 
			// txtAccEmail
			// 
			this.txtAccEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAccEmail.Location = new System.Drawing.Point(138, 273);
			this.txtAccEmail.Multiline = true;
			this.txtAccEmail.Name = "txtAccEmail";
			this.txtAccEmail.Size = new System.Drawing.Size(331, 29);
			this.txtAccEmail.TabIndex = 6;
			this.txtAccEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.Transparent;
			this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.Silver;
			this.label4.Location = new System.Drawing.Point(65, 273);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(69, 25);
			this.label4.TabIndex = 52;
			this.label4.Text = "Email :";
			// 
			// txtAccAddress
			// 
			this.txtAccAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAccAddress.Location = new System.Drawing.Point(138, 319);
			this.txtAccAddress.Multiline = true;
			this.txtAccAddress.Name = "txtAccAddress";
			this.txtAccAddress.Size = new System.Drawing.Size(331, 130);
			this.txtAccAddress.TabIndex = 7;
			this.txtAccAddress.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.BackColor = System.Drawing.Color.Transparent;
			this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.Silver;
			this.label3.Location = new System.Drawing.Point(49, 319);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(85, 25);
			this.label3.TabIndex = 50;
			this.label3.Text = "Address:";
			// 
			// txtAccPhone
			// 
			this.txtAccPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAccPhone.Location = new System.Drawing.Point(138, 227);
			this.txtAccPhone.MaxLength = 11;
			this.txtAccPhone.Multiline = true;
			this.txtAccPhone.Name = "txtAccPhone";
			this.txtAccPhone.Size = new System.Drawing.Size(331, 29);
			this.txtAccPhone.TabIndex = 5;
			this.txtAccPhone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFlow);
			this.txtAccPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAccPhone_KeyPress);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.BackColor = System.Drawing.Color.Transparent;
			this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Silver;
			this.label2.Location = new System.Drawing.Point(58, 227);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 25);
			this.label2.TabIndex = 48;
			this.label2.Text = "Phone :";
			// 
			// btnAdd
			// 
			this.btnAdd.BackColor = System.Drawing.Color.Transparent;
			this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
			this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnAdd.FlatAppearance.BorderSize = 0;
			this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnAdd.Location = new System.Drawing.Point(420, 135);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(41, 31);
			this.btnAdd.TabIndex = 20;
			this.btnAdd.UseVisualStyleBackColor = false;
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// txtAccName
			// 
			this.txtAccName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAccName.Location = new System.Drawing.Point(138, 135);
			this.txtAccName.Multiline = true;
			this.txtAccName.Name = "txtAccName";
			this.txtAccName.Size = new System.Drawing.Size(280, 29);
			this.txtAccName.TabIndex = 3;
			this.txtAccName.DoubleClick += new System.EventHandler(this.txtAccName_DoubleClick);
			this.txtAccName.Enter += new System.EventHandler(this.txtAccName_Enter);
			this.txtAccName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAccName_KeyDown);
			this.txtAccName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAccName_KeyPress);
			this.txtAccName.Leave += new System.EventHandler(this.txtAccName_Leave);
			// 
			// pnlRfidList
			// 
			this.pnlRfidList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlRfidList.Controls.Add(this.btnRfidListOk);
			this.pnlRfidList.Controls.Add(this.dgvRfidList);
			this.pnlRfidList.Controls.Add(this.panel4);
			this.pnlRfidList.Location = new System.Drawing.Point(922, 42);
			this.pnlRfidList.Name = "pnlRfidList";
			this.pnlRfidList.Size = new System.Drawing.Size(444, 508);
			this.pnlRfidList.TabIndex = 175;
			this.pnlRfidList.Visible = false;
			// 
			// btnRfidListOk
			// 
			this.btnRfidListOk.BackColor = System.Drawing.Color.Transparent;
			this.btnRfidListOk.BackgroundImage = global::pLocker.Properties.Resources.save_1;
			this.btnRfidListOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnRfidListOk.FlatAppearance.BorderSize = 0;
			this.btnRfidListOk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
			this.btnRfidListOk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
			this.btnRfidListOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRfidListOk.Location = new System.Drawing.Point(148, 456);
			this.btnRfidListOk.Name = "btnRfidListOk";
			this.btnRfidListOk.Size = new System.Drawing.Size(132, 44);
			this.btnRfidListOk.TabIndex = 17;
			this.btnRfidListOk.UseVisualStyleBackColor = false;
			this.btnRfidListOk.Click += new System.EventHandler(this.btnRfidListOk_Click);
			// 
			// dgvRfidList
			// 
			this.dgvRfidList.AllowUserToAddRows = false;
			this.dgvRfidList.AllowUserToDeleteRows = false;
			this.dgvRfidList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.dgvRfidList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
			this.dgvRfidList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvRfidList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvRfidList.ColumnHeadersHeight = 40;
			this.dgvRfidList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.No,
            this.CardNO,
            this.rfid});
			this.dgvRfidList.Location = new System.Drawing.Point(0, 44);
			this.dgvRfidList.Name = "dgvRfidList";
			this.dgvRfidList.RowHeadersVisible = false;
			this.dgvRfidList.RowHeadersWidth = 35;
			this.dgvRfidList.RowTemplate.Height = 35;
			this.dgvRfidList.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvRfidList.Size = new System.Drawing.Size(439, 416);
			this.dgvRfidList.TabIndex = 2;
			this.dgvRfidList.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRfidList_CellEnter);
			this.dgvRfidList.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRfidList_CellLeave);
			this.dgvRfidList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvRfidList_KeyDown);
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(4)))), ((int)(((byte)(209)))));
			this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
			this.panel4.Controls.Add(this.label24);
			this.panel4.Controls.Add(this.btnRfidListClose);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(440, 37);
			this.panel4.TabIndex = 1;
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.BackColor = System.Drawing.Color.Transparent;
			this.label24.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
			this.label24.Location = new System.Drawing.Point(3, 4);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(112, 25);
			this.label24.TabIndex = 67;
			this.label24.Text = "Hire Details";
			// 
			// btnRfidListClose
			// 
			this.btnRfidListClose.BackColor = System.Drawing.Color.Transparent;
			this.btnRfidListClose.BackgroundImage = global::pLocker.Properties.Resources.close_1_button;
			this.btnRfidListClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.btnRfidListClose.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnRfidListClose.FlatAppearance.BorderSize = 0;
			this.btnRfidListClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRfidListClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRfidListClose.ForeColor = System.Drawing.Color.Transparent;
			this.btnRfidListClose.Location = new System.Drawing.Point(414, 0);
			this.btnRfidListClose.Name = "btnRfidListClose";
			this.btnRfidListClose.Size = new System.Drawing.Size(26, 37);
			this.btnRfidListClose.TabIndex = 66;
			this.btnRfidListClose.UseVisualStyleBackColor = false;
			this.btnRfidListClose.Click += new System.EventHandler(this.btnRfidListClose_Click);
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// timer2
			// 
			this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// No
			// 
			this.No.HeaderText = "Sl.NO";
			this.No.Name = "No";
			this.No.Width = 78;
			// 
			// CardNO
			// 
			this.CardNO.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			this.CardNO.DefaultCellStyle = dataGridViewCellStyle2;
			this.CardNO.HeaderText = "CardNO";
			this.CardNO.Name = "CardNO";
			this.CardNO.ReadOnly = true;
			// 
			// rfid
			// 
			this.rfid.HeaderText = "rfid";
			this.rfid.Name = "rfid";
			this.rfid.Visible = false;
			this.rfid.Width = 57;
			// 
			// FrmHireLocker
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1075, 591);
			this.Controls.Add(this.pnlRfidList);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "FrmHireLocker";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "FrmHireLocker";
			this.Load += new System.EventHandler(this.FrmHireLocker_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmHireLocker_KeyDown);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.grpBoxPrint.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbCapture)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.pnlRfidList.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgvRfidList)).EndInit();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnFrmClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtRemarks;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAccEmail;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAccAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtAccPhone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtAccName;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtAccNO;
        private System.Windows.Forms.Label lblLocker;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtCustomerPIN;
        private System.Windows.Forms.ComboBox cboLocker;
        private System.Windows.Forms.DateTimePicker dtpExpDate;
        private System.Windows.Forms.ComboBox cboLockStatus;
        private System.Windows.Forms.DateTimePicker dtpExpTime;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dtpHireTime;
        private System.Windows.Forms.DateTimePicker dtpHireDate;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblRate;
        private System.Windows.Forms.TextBox txtHireForNo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboHireFor;
        private System.Windows.Forms.Button btnRFID;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtLockHireID;
        private System.Windows.Forms.TextBox txtLedgerID;
        private System.Windows.Forms.Label lblToolTipAccName;
        private System.Windows.Forms.CheckBox chkHired;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtDiscountAmt;
        private System.Windows.Forms.TextBox txtNetAmt;
        private System.Windows.Forms.TextBox txtAmtAdjust;
        private System.Windows.Forms.TextBox txtBalAmt;
        private System.Windows.Forms.TextBox txtPaidAmt;
        private System.Windows.Forms.ComboBox cmbPaymentMode;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtICardNo;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.PictureBox pbCapture;
        private System.Windows.Forms.Label lblPin;
        private System.Windows.Forms.TextBox txtHireDetails;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.GroupBox grpBoxPrint;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.CheckBox chBA4;
        private System.Windows.Forms.CheckBox chBPreview;
        private System.Windows.Forms.Button Save_Print;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblRfid;
        private System.Windows.Forms.TextBox txtICardNo_2;
		private System.Windows.Forms.Panel pnlRfidList;
		private System.Windows.Forms.DataGridView dgvRfidList;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Button btnRfidListClose;
		private System.Windows.Forms.Button btnRfidListOk;
		private System.Windows.Forms.Timer timer2;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.TextBox txtinvoiceno;
		private System.Windows.Forms.DataGridViewTextBoxColumn No;
		private System.Windows.Forms.DataGridViewTextBoxColumn CardNO;
		private System.Windows.Forms.DataGridViewTextBoxColumn rfid;
	}
}