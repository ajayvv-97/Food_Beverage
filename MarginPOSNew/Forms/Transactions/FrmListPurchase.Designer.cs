namespace MarginPOSNew.Forms.Transactions
{
    partial class FrmListPurchase
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvLstPurchaseMaster = new System.Windows.Forms.DataGridView();
            this.mcmbParty = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.mbtnRefresh = new MetroFramework.Controls.MetroButton();
            this.mbtnSearch = new MetroFramework.Controls.MetroButton();
            this.mlblName = new MetroFramework.Controls.MetroLabel();
            this.hlblHeader = new MetroFramework.Drawing.Html.HtmlLabel();
            this.mdtpFrom = new MetroFramework.Controls.MetroDateTime();
            this.mdtpTo = new MetroFramework.Controls.MetroDateTime();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.dgvPurchaseDetails = new System.Windows.Forms.DataGridView();
            this.mtxtTotalAmt = new MetroFramework.Controls.MetroTextBox();
            this.mtxtTotalQty = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.mlblTotRecords = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLstPurchaseMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLstPurchaseMaster
            // 
            this.dgvLstPurchaseMaster.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvLstPurchaseMaster.AllowUserToAddRows = false;
            this.dgvLstPurchaseMaster.AllowUserToDeleteRows = false;
            this.dgvLstPurchaseMaster.AllowUserToResizeColumns = false;
            this.dgvLstPurchaseMaster.AllowUserToResizeRows = false;
            this.dgvLstPurchaseMaster.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLstPurchaseMaster.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvLstPurchaseMaster.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLstPurchaseMaster.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLstPurchaseMaster.ColumnHeadersHeight = 40;
            this.dgvLstPurchaseMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLstPurchaseMaster.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLstPurchaseMaster.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLstPurchaseMaster.EnableHeadersVisualStyles = false;
            this.dgvLstPurchaseMaster.Location = new System.Drawing.Point(18, 85);
            this.dgvLstPurchaseMaster.MultiSelect = false;
            this.dgvLstPurchaseMaster.Name = "dgvLstPurchaseMaster";
            this.dgvLstPurchaseMaster.ReadOnly = true;
            this.dgvLstPurchaseMaster.RowHeadersVisible = false;
            this.dgvLstPurchaseMaster.RowTemplate.Height = 30;
            this.dgvLstPurchaseMaster.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvLstPurchaseMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLstPurchaseMaster.ShowCellErrors = false;
            this.dgvLstPurchaseMaster.ShowCellToolTips = false;
            this.dgvLstPurchaseMaster.ShowEditingIcon = false;
            this.dgvLstPurchaseMaster.Size = new System.Drawing.Size(996, 331);
            this.dgvLstPurchaseMaster.TabIndex = 11;
            this.dgvLstPurchaseMaster.TabStop = false;
            this.dgvLstPurchaseMaster.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLstPurchaseMaster_CellContentClick);
            this.dgvLstPurchaseMaster.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvLstPurchaseMaster_CellMouseDoubleClick);
            this.dgvLstPurchaseMaster.SelectionChanged += new System.EventHandler(this.DgvLstPurchaseMaster_SelectionChanged);
            this.dgvLstPurchaseMaster.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DgvLstPurchaseMaster_KeyUp);
            // 
            // mcmbParty
            // 
            this.mcmbParty.BackColor = System.Drawing.Color.White;
            this.mcmbParty.DropDownHeight = 250;
            this.mcmbParty.ForeColor = System.Drawing.Color.Black;
            this.mcmbParty.FormattingEnabled = true;
            this.mcmbParty.IntegralHeight = false;
            this.mcmbParty.ItemHeight = 23;
            this.mcmbParty.Location = new System.Drawing.Point(456, 50);
            this.mcmbParty.Name = "mcmbParty";
            this.mcmbParty.Size = new System.Drawing.Size(259, 29);
            this.mcmbParty.TabIndex = 8;
            this.mcmbParty.Tag = "Department";
            this.mcmbParty.UseSelectable = true;
            this.mcmbParty.SelectedIndexChanged += new System.EventHandler(this.mcmbParty_SelectedIndexChanged);
            this.mcmbParty.KeyUp += new System.Windows.Forms.KeyEventHandler(this.McmbParty_KeyUp);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.Location = new System.Drawing.Point(12, 50);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(63, 25);
            this.metroLabel1.TabIndex = 19;
            this.metroLabel1.Text = "From :";
            this.metroLabel1.Click += new System.EventHandler(this.metroLabel1_Click);
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.mbtnRefresh.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnRefresh.ForeColor = System.Drawing.Color.White;
            this.mbtnRefresh.Location = new System.Drawing.Point(897, 38);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(106, 41);
            this.mbtnRefresh.TabIndex = 10;
            this.mbtnRefresh.Text = "&Refresh";
            this.mbtnRefresh.UseCustomBackColor = true;
            this.mbtnRefresh.UseCustomForeColor = true;
            this.mbtnRefresh.UseSelectable = true;
            this.mbtnRefresh.Click += new System.EventHandler(this.MbtnRefresh_Click);
            // 
            // mbtnSearch
            // 
            this.mbtnSearch.BackColor = System.Drawing.Color.CornflowerBlue;
            this.mbtnSearch.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnSearch.ForeColor = System.Drawing.Color.White;
            this.mbtnSearch.Location = new System.Drawing.Point(782, 38);
            this.mbtnSearch.Name = "mbtnSearch";
            this.mbtnSearch.Size = new System.Drawing.Size(106, 41);
            this.mbtnSearch.TabIndex = 9;
            this.mbtnSearch.Text = "&Search";
            this.mbtnSearch.UseCustomBackColor = true;
            this.mbtnSearch.UseCustomForeColor = true;
            this.mbtnSearch.UseSelectable = true;
            this.mbtnSearch.Click += new System.EventHandler(this.MbtnSearch_Click);
            this.mbtnSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MbtnSearch_KeyUp);
            // 
            // mlblName
            // 
            this.mlblName.AutoSize = true;
            this.mlblName.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblName.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblName.ForeColor = System.Drawing.Color.Black;
            this.mlblName.Location = new System.Drawing.Point(396, 50);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(61, 25);
            this.mlblName.TabIndex = 20;
            this.mlblName.Text = "Party :";
            this.mlblName.Click += new System.EventHandler(this.mlblName_Click);
            // 
            // hlblHeader
            // 
            this.hlblHeader.AutoScroll = true;
            this.hlblHeader.AutoScrollMinSize = new System.Drawing.Size(177, 36);
            this.hlblHeader.AutoSize = false;
            this.hlblHeader.BackColor = System.Drawing.SystemColors.Window;
            this.hlblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlblHeader.Location = new System.Drawing.Point(2, 6);
            this.hlblHeader.Name = "hlblHeader";
            this.hlblHeader.Size = new System.Drawing.Size(186, 38);
            this.hlblHeader.TabIndex = 22;
            this.hlblHeader.Text = "Purchase Entry List";
            this.hlblHeader.Click += new System.EventHandler(this.hlblHeader_Click);
            // 
            // mdtpFrom
            // 
            this.mdtpFrom.CustomFormat = "dd-MM-yyyy";
            this.mdtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mdtpFrom.Location = new System.Drawing.Point(72, 50);
            this.mdtpFrom.MinimumSize = new System.Drawing.Size(0, 29);
            this.mdtpFrom.Name = "mdtpFrom";
            this.mdtpFrom.Size = new System.Drawing.Size(133, 29);
            this.mdtpFrom.TabIndex = 6;
            this.mdtpFrom.ValueChanged += new System.EventHandler(this.mdtpFrom_ValueChanged);
            this.mdtpFrom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MdtpFrom_KeyUp);
            // 
            // mdtpTo
            // 
            this.mdtpTo.CustomFormat = "dd-MM-yyyy";
            this.mdtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mdtpTo.Location = new System.Drawing.Point(258, 50);
            this.mdtpTo.MinimumSize = new System.Drawing.Size(0, 29);
            this.mdtpTo.Name = "mdtpTo";
            this.mdtpTo.Size = new System.Drawing.Size(132, 29);
            this.mdtpTo.TabIndex = 7;
            this.mdtpTo.ValueChanged += new System.EventHandler(this.mdtpTo_ValueChanged);
            this.mdtpTo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MdtpTo_KeyUp);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.ForeColor = System.Drawing.Color.Black;
            this.metroLabel2.Location = new System.Drawing.Point(211, 50);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(41, 25);
            this.metroLabel2.TabIndex = 27;
            this.metroLabel2.Text = "To :";
            this.metroLabel2.Click += new System.EventHandler(this.metroLabel2_Click);
            // 
            // dgvPurchaseDetails
            // 
            this.dgvPurchaseDetails.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvPurchaseDetails.AllowUserToAddRows = false;
            this.dgvPurchaseDetails.AllowUserToDeleteRows = false;
            this.dgvPurchaseDetails.AllowUserToResizeColumns = false;
            this.dgvPurchaseDetails.AllowUserToResizeRows = false;
            this.dgvPurchaseDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPurchaseDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPurchaseDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPurchaseDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPurchaseDetails.ColumnHeadersHeight = 40;
            this.dgvPurchaseDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPurchaseDetails.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPurchaseDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvPurchaseDetails.EnableHeadersVisualStyles = false;
            this.dgvPurchaseDetails.Location = new System.Drawing.Point(18, 423);
            this.dgvPurchaseDetails.MultiSelect = false;
            this.dgvPurchaseDetails.Name = "dgvPurchaseDetails";
            this.dgvPurchaseDetails.ReadOnly = true;
            this.dgvPurchaseDetails.RowHeadersVisible = false;
            this.dgvPurchaseDetails.RowTemplate.Height = 30;
            this.dgvPurchaseDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvPurchaseDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchaseDetails.ShowCellErrors = false;
            this.dgvPurchaseDetails.ShowCellToolTips = false;
            this.dgvPurchaseDetails.ShowEditingIcon = false;
            this.dgvPurchaseDetails.Size = new System.Drawing.Size(996, 176);
            this.dgvPurchaseDetails.TabIndex = 29;
            this.dgvPurchaseDetails.TabStop = false;
            this.dgvPurchaseDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPurchaseDetails_CellContentClick);
            // 
            // mtxtTotalAmt
            // 
            // 
            // 
            // 
            this.mtxtTotalAmt.CustomButton.Image = null;
            this.mtxtTotalAmt.CustomButton.Location = new System.Drawing.Point(149, 1);
            this.mtxtTotalAmt.CustomButton.Name = "";
            this.mtxtTotalAmt.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtTotalAmt.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtTotalAmt.CustomButton.TabIndex = 1;
            this.mtxtTotalAmt.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtTotalAmt.CustomButton.UseSelectable = true;
            this.mtxtTotalAmt.CustomButton.Visible = false;
            this.mtxtTotalAmt.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtTotalAmt.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.mtxtTotalAmt.Lines = new string[0];
            this.mtxtTotalAmt.Location = new System.Drawing.Point(843, 605);
            this.mtxtTotalAmt.MaxLength = 50;
            this.mtxtTotalAmt.Name = "mtxtTotalAmt";
            this.mtxtTotalAmt.PasswordChar = '\0';
            this.mtxtTotalAmt.ReadOnly = true;
            this.mtxtTotalAmt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtTotalAmt.SelectedText = "";
            this.mtxtTotalAmt.SelectionLength = 0;
            this.mtxtTotalAmt.SelectionStart = 0;
            this.mtxtTotalAmt.ShortcutsEnabled = true;
            this.mtxtTotalAmt.Size = new System.Drawing.Size(171, 23);
            this.mtxtTotalAmt.TabIndex = 52;
            this.mtxtTotalAmt.Tag = "";
            this.mtxtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mtxtTotalAmt.UseSelectable = true;
            this.mtxtTotalAmt.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtTotalAmt.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtTotalAmt.Click += new System.EventHandler(this.mtxtTotalAmt_Click);
            // 
            // mtxtTotalQty
            // 
            // 
            // 
            // 
            this.mtxtTotalQty.CustomButton.Image = null;
            this.mtxtTotalQty.CustomButton.Location = new System.Drawing.Point(124, 1);
            this.mtxtTotalQty.CustomButton.Name = "";
            this.mtxtTotalQty.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtTotalQty.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtTotalQty.CustomButton.TabIndex = 1;
            this.mtxtTotalQty.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtTotalQty.CustomButton.UseSelectable = true;
            this.mtxtTotalQty.CustomButton.Visible = false;
            this.mtxtTotalQty.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtTotalQty.FontWeight = MetroFramework.MetroTextBoxWeight.Bold;
            this.mtxtTotalQty.Lines = new string[0];
            this.mtxtTotalQty.Location = new System.Drawing.Point(592, 607);
            this.mtxtTotalQty.MaxLength = 50;
            this.mtxtTotalQty.Name = "mtxtTotalQty";
            this.mtxtTotalQty.PasswordChar = '\0';
            this.mtxtTotalQty.ReadOnly = true;
            this.mtxtTotalQty.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtTotalQty.SelectedText = "";
            this.mtxtTotalQty.SelectionLength = 0;
            this.mtxtTotalQty.SelectionStart = 0;
            this.mtxtTotalQty.ShortcutsEnabled = true;
            this.mtxtTotalQty.Size = new System.Drawing.Size(146, 23);
            this.mtxtTotalQty.TabIndex = 51;
            this.mtxtTotalQty.Tag = "";
            this.mtxtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mtxtTotalQty.UseSelectable = true;
            this.mtxtTotalQty.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtTotalQty.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtTotalQty.Click += new System.EventHandler(this.mtxtTotalQty_Click);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.ForeColor = System.Drawing.Color.Black;
            this.metroLabel4.Location = new System.Drawing.Point(744, 607);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(93, 19);
            this.metroLabel4.TabIndex = 50;
            this.metroLabel4.Text = "Total Amount";
            this.metroLabel4.UseCustomBackColor = true;
            this.metroLabel4.UseCustomForeColor = true;
            this.metroLabel4.Click += new System.EventHandler(this.metroLabel4_Click);
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.ForeColor = System.Drawing.Color.Black;
            this.metroLabel3.Location = new System.Drawing.Point(489, 609);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(97, 19);
            this.metroLabel3.TabIndex = 49;
            this.metroLabel3.Text = "Total Quantity";
            this.metroLabel3.UseCustomBackColor = true;
            this.metroLabel3.UseCustomForeColor = true;
            this.metroLabel3.Click += new System.EventHandler(this.metroLabel3_Click);
            // 
            // mlblTotRecords
            // 
            this.mlblTotRecords.AutoSize = true;
            this.mlblTotRecords.BackColor = System.Drawing.Color.Transparent;
            this.mlblTotRecords.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblTotRecords.ForeColor = System.Drawing.Color.Black;
            this.mlblTotRecords.Location = new System.Drawing.Point(18, 541);
            this.mlblTotRecords.Name = "mlblTotRecords";
            this.mlblTotRecords.Size = new System.Drawing.Size(0, 0);
            this.mlblTotRecords.TabIndex = 53;
            this.mlblTotRecords.UseCustomBackColor = true;
            this.mlblTotRecords.UseCustomForeColor = true;
            this.mlblTotRecords.Click += new System.EventHandler(this.mlblTotRecords_Click);
            // 
            // FrmListPurchase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(1026, 635);
            this.Controls.Add(this.mlblTotRecords);
            this.Controls.Add(this.mtxtTotalAmt);
            this.Controls.Add(this.mtxtTotalQty);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.dgvPurchaseDetails);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.mdtpTo);
            this.Controls.Add(this.mdtpFrom);
            this.Controls.Add(this.dgvLstPurchaseMaster);
            this.Controls.Add(this.mcmbParty);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.mbtnRefresh);
            this.Controls.Add(this.mbtnSearch);
            this.Controls.Add(this.mlblName);
            this.Controls.Add(this.hlblHeader);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmListPurchase";
            this.Resizable = false;
            this.Load += new System.EventHandler(this.FrmListPurchase_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmListPurchase_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLstPurchaseMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvLstPurchaseMaster;
        private MetroFramework.Controls.MetroComboBox mcmbParty;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton mbtnRefresh;
        private MetroFramework.Controls.MetroButton mbtnSearch;
        private MetroFramework.Controls.MetroLabel mlblName;
        private MetroFramework.Drawing.Html.HtmlLabel hlblHeader;
        private MetroFramework.Controls.MetroDateTime mdtpFrom;
        private MetroFramework.Controls.MetroDateTime mdtpTo;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.DataGridView dgvPurchaseDetails;
        private MetroFramework.Controls.MetroTextBox mtxtTotalAmt;
        private MetroFramework.Controls.MetroTextBox mtxtTotalQty;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel mlblTotRecords;
    }
}