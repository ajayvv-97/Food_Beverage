namespace MarginPOSNew.Forms.Transactions
{
    partial class FrmListPurchaseReturn
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mtxtTotalAmt = new MetroFramework.Controls.MetroTextBox();
            this.mtxtTotalQty = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.dgvPurchaseReturnDetails = new System.Windows.Forms.DataGridView();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.mdtpTo = new MetroFramework.Controls.MetroDateTime();
            this.mdtpFrom = new MetroFramework.Controls.MetroDateTime();
            this.dgvLstPurchaseReturnMaster = new System.Windows.Forms.DataGridView();
            this.mcmbParty = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.mbtnRefresh = new MetroFramework.Controls.MetroButton();
            this.mbtnSearch = new MetroFramework.Controls.MetroButton();
            this.mlblName = new MetroFramework.Controls.MetroLabel();
            this.hlblHeader = new MetroFramework.Drawing.Html.HtmlLabel();
            this.mlblTotRecords = new MetroFramework.Controls.MetroLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseReturnDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLstPurchaseReturnMaster)).BeginInit();
            this.SuspendLayout();
            // 
            // mtxtTotalAmt
            // 
            // 
            // 
            // 
            this.mtxtTotalAmt.CustomButton.Image = null;
            this.mtxtTotalAmt.CustomButton.Location = new System.Drawing.Point(241, 1);
            this.mtxtTotalAmt.CustomButton.Name = "";
            this.mtxtTotalAmt.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtTotalAmt.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtTotalAmt.CustomButton.TabIndex = 1;
            this.mtxtTotalAmt.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtTotalAmt.CustomButton.UseSelectable = true;
            this.mtxtTotalAmt.CustomButton.Visible = false;
            this.mtxtTotalAmt.Lines = new string[0];
            this.mtxtTotalAmt.Location = new System.Drawing.Point(994, 541);
            this.mtxtTotalAmt.MaxLength = 50;
            this.mtxtTotalAmt.Name = "mtxtTotalAmt";
            this.mtxtTotalAmt.PasswordChar = '\0';
            this.mtxtTotalAmt.ReadOnly = true;
            this.mtxtTotalAmt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtTotalAmt.SelectedText = "";
            this.mtxtTotalAmt.SelectionLength = 0;
            this.mtxtTotalAmt.SelectionStart = 0;
            this.mtxtTotalAmt.ShortcutsEnabled = true;
            this.mtxtTotalAmt.Size = new System.Drawing.Size(263, 23);
            this.mtxtTotalAmt.TabIndex = 67;
            this.mtxtTotalAmt.Tag = "";
            this.mtxtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mtxtTotalAmt.UseSelectable = true;
            this.mtxtTotalAmt.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtTotalAmt.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // mtxtTotalQty
            // 
            // 
            // 
            // 
            this.mtxtTotalQty.CustomButton.Image = null;
            this.mtxtTotalQty.CustomButton.Location = new System.Drawing.Point(241, 1);
            this.mtxtTotalQty.CustomButton.Name = "";
            this.mtxtTotalQty.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtTotalQty.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtTotalQty.CustomButton.TabIndex = 1;
            this.mtxtTotalQty.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtTotalQty.CustomButton.UseSelectable = true;
            this.mtxtTotalQty.CustomButton.Visible = false;
            this.mtxtTotalQty.Lines = new string[0];
            this.mtxtTotalQty.Location = new System.Drawing.Point(626, 541);
            this.mtxtTotalQty.MaxLength = 50;
            this.mtxtTotalQty.Name = "mtxtTotalQty";
            this.mtxtTotalQty.PasswordChar = '\0';
            this.mtxtTotalQty.ReadOnly = true;
            this.mtxtTotalQty.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtTotalQty.SelectedText = "";
            this.mtxtTotalQty.SelectionLength = 0;
            this.mtxtTotalQty.SelectionStart = 0;
            this.mtxtTotalQty.ShortcutsEnabled = true;
            this.mtxtTotalQty.Size = new System.Drawing.Size(263, 23);
            this.mtxtTotalQty.TabIndex = 66;
            this.mtxtTotalQty.Tag = "";
            this.mtxtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mtxtTotalQty.UseSelectable = true;
            this.mtxtTotalQty.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtTotalQty.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.ForeColor = System.Drawing.Color.Black;
            this.metroLabel4.Location = new System.Drawing.Point(895, 543);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(93, 19);
            this.metroLabel4.TabIndex = 65;
            this.metroLabel4.Text = "Total Amount";
            this.metroLabel4.UseCustomBackColor = true;
            this.metroLabel4.UseCustomForeColor = true;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.ForeColor = System.Drawing.Color.Black;
            this.metroLabel3.Location = new System.Drawing.Point(523, 543);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(97, 19);
            this.metroLabel3.TabIndex = 64;
            this.metroLabel3.Text = "Total Quantity";
            this.metroLabel3.UseCustomBackColor = true;
            this.metroLabel3.UseCustomForeColor = true;
            // 
            // dgvPurchaseReturnDetails
            // 
            this.dgvPurchaseReturnDetails.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvPurchaseReturnDetails.AllowUserToAddRows = false;
            this.dgvPurchaseReturnDetails.AllowUserToDeleteRows = false;
            this.dgvPurchaseReturnDetails.AllowUserToResizeColumns = false;
            this.dgvPurchaseReturnDetails.AllowUserToResizeRows = false;
            this.dgvPurchaseReturnDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPurchaseReturnDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvPurchaseReturnDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPurchaseReturnDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPurchaseReturnDetails.ColumnHeadersHeight = 40;
            this.dgvPurchaseReturnDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPurchaseReturnDetails.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPurchaseReturnDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvPurchaseReturnDetails.EnableHeadersVisualStyles = false;
            this.dgvPurchaseReturnDetails.Location = new System.Drawing.Point(24, 304);
            this.dgvPurchaseReturnDetails.MultiSelect = false;
            this.dgvPurchaseReturnDetails.Name = "dgvPurchaseReturnDetails";
            this.dgvPurchaseReturnDetails.ReadOnly = true;
            this.dgvPurchaseReturnDetails.RowHeadersVisible = false;
            this.dgvPurchaseReturnDetails.RowTemplate.Height = 30;
            this.dgvPurchaseReturnDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvPurchaseReturnDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPurchaseReturnDetails.ShowCellErrors = false;
            this.dgvPurchaseReturnDetails.ShowCellToolTips = false;
            this.dgvPurchaseReturnDetails.ShowEditingIcon = false;
            this.dgvPurchaseReturnDetails.Size = new System.Drawing.Size(1233, 226);
            this.dgvPurchaseReturnDetails.TabIndex = 63;
            this.dgvPurchaseReturnDetails.TabStop = false;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.ForeColor = System.Drawing.Color.Black;
            this.metroLabel2.Location = new System.Drawing.Point(311, 50);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(32, 25);
            this.metroLabel2.TabIndex = 62;
            this.metroLabel2.Text = "To";
            // 
            // mdtpTo
            // 
            this.mdtpTo.CustomFormat = "dd-MM-yyyy";
            this.mdtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mdtpTo.Location = new System.Drawing.Point(343, 50);
            this.mdtpTo.MinimumSize = new System.Drawing.Size(0, 29);
            this.mdtpTo.Name = "mdtpTo";
            this.mdtpTo.Size = new System.Drawing.Size(222, 29);
            this.mdtpTo.TabIndex = 54;
            this.mdtpTo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mdtpTo_KeyUp);
            // 
            // mdtpFrom
            // 
            this.mdtpFrom.CustomFormat = "dd-MM-yyyy";
            this.mdtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mdtpFrom.Location = new System.Drawing.Point(72, 50);
            this.mdtpFrom.MinimumSize = new System.Drawing.Size(0, 29);
            this.mdtpFrom.Name = "mdtpFrom";
            this.mdtpFrom.Size = new System.Drawing.Size(222, 29);
            this.mdtpFrom.TabIndex = 53;
            this.mdtpFrom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mdtpFrom_KeyUp);
            // 
            // dgvLstPurchaseReturnMaster
            // 
            this.dgvLstPurchaseReturnMaster.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvLstPurchaseReturnMaster.AllowUserToAddRows = false;
            this.dgvLstPurchaseReturnMaster.AllowUserToDeleteRows = false;
            this.dgvLstPurchaseReturnMaster.AllowUserToResizeColumns = false;
            this.dgvLstPurchaseReturnMaster.AllowUserToResizeRows = false;
            this.dgvLstPurchaseReturnMaster.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLstPurchaseReturnMaster.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvLstPurchaseReturnMaster.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLstPurchaseReturnMaster.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvLstPurchaseReturnMaster.ColumnHeadersHeight = 40;
            this.dgvLstPurchaseReturnMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLstPurchaseReturnMaster.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvLstPurchaseReturnMaster.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLstPurchaseReturnMaster.EnableHeadersVisualStyles = false;
            this.dgvLstPurchaseReturnMaster.Location = new System.Drawing.Point(24, 85);
            this.dgvLstPurchaseReturnMaster.MultiSelect = false;
            this.dgvLstPurchaseReturnMaster.Name = "dgvLstPurchaseReturnMaster";
            this.dgvLstPurchaseReturnMaster.ReadOnly = true;
            this.dgvLstPurchaseReturnMaster.RowHeadersVisible = false;
            this.dgvLstPurchaseReturnMaster.RowTemplate.Height = 30;
            this.dgvLstPurchaseReturnMaster.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvLstPurchaseReturnMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLstPurchaseReturnMaster.ShowCellErrors = false;
            this.dgvLstPurchaseReturnMaster.ShowCellToolTips = false;
            this.dgvLstPurchaseReturnMaster.ShowEditingIcon = false;
            this.dgvLstPurchaseReturnMaster.Size = new System.Drawing.Size(1233, 213);
            this.dgvLstPurchaseReturnMaster.TabIndex = 58;
            this.dgvLstPurchaseReturnMaster.TabStop = false;
            this.dgvLstPurchaseReturnMaster.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvLstPurchaseReturnMaster_CellMouseDoubleClick);
            this.dgvLstPurchaseReturnMaster.SelectionChanged += new System.EventHandler(this.dgvLstPurchaseReturnMaster_SelectionChanged);
            this.dgvLstPurchaseReturnMaster.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgvLstPurchaseReturnMaster_KeyUp);
            // 
            // mcmbParty
            // 
            this.mcmbParty.BackColor = System.Drawing.Color.White;
            this.mcmbParty.DropDownHeight = 250;
            this.mcmbParty.ForeColor = System.Drawing.Color.Black;
            this.mcmbParty.FormattingEnabled = true;
            this.mcmbParty.IntegralHeight = false;
            this.mcmbParty.ItemHeight = 23;
            this.mcmbParty.Location = new System.Drawing.Point(649, 50);
            this.mcmbParty.Name = "mcmbParty";
            this.mcmbParty.Size = new System.Drawing.Size(269, 29);
            this.mcmbParty.TabIndex = 55;
            this.mcmbParty.Tag = "Department";
            this.mcmbParty.UseSelectable = true;
            this.mcmbParty.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mcmbParty_KeyUp);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.Location = new System.Drawing.Point(18, 50);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(54, 25);
            this.metroLabel1.TabIndex = 59;
            this.metroLabel1.Text = "From";
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.mbtnRefresh.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnRefresh.ForeColor = System.Drawing.Color.White;
            this.mbtnRefresh.Location = new System.Drawing.Point(1107, 34);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(150, 45);
            this.mbtnRefresh.TabIndex = 57;
            this.mbtnRefresh.Text = "&Refresh";
            this.mbtnRefresh.UseCustomBackColor = true;
            this.mbtnRefresh.UseCustomForeColor = true;
            this.mbtnRefresh.UseSelectable = true;
            this.mbtnRefresh.Click += new System.EventHandler(this.mbtnRefresh_Click);
            // 
            // mbtnSearch
            // 
            this.mbtnSearch.BackColor = System.Drawing.Color.CornflowerBlue;
            this.mbtnSearch.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnSearch.ForeColor = System.Drawing.Color.White;
            this.mbtnSearch.Location = new System.Drawing.Point(951, 34);
            this.mbtnSearch.Name = "mbtnSearch";
            this.mbtnSearch.Size = new System.Drawing.Size(150, 45);
            this.mbtnSearch.TabIndex = 56;
            this.mbtnSearch.Text = "&Search";
            this.mbtnSearch.UseCustomBackColor = true;
            this.mbtnSearch.UseCustomForeColor = true;
            this.mbtnSearch.UseSelectable = true;
            this.mbtnSearch.Click += new System.EventHandler(this.mbtnSearch_Click);
            this.mbtnSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mbtnSearch_KeyUp);
            // 
            // mlblName
            // 
            this.mlblName.AutoSize = true;
            this.mlblName.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblName.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblName.ForeColor = System.Drawing.Color.Black;
            this.mlblName.Location = new System.Drawing.Point(591, 50);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(52, 25);
            this.mlblName.TabIndex = 60;
            this.mlblName.Text = "Party";
            // 
            // hlblHeader
            // 
            this.hlblHeader.AutoScroll = true;
            this.hlblHeader.AutoScrollMinSize = new System.Drawing.Size(188, 36);
            this.hlblHeader.AutoSize = false;
            this.hlblHeader.BackColor = System.Drawing.SystemColors.Window;
            this.hlblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlblHeader.Location = new System.Drawing.Point(8, 6);
            this.hlblHeader.Name = "hlblHeader";
            this.hlblHeader.Size = new System.Drawing.Size(318, 38);
            this.hlblHeader.TabIndex = 61;
            this.hlblHeader.Text = "List Purchase Return";
            // 
            // mlblTotRecords
            // 
            this.mlblTotRecords.AutoSize = true;
            this.mlblTotRecords.BackColor = System.Drawing.Color.Transparent;
            this.mlblTotRecords.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblTotRecords.ForeColor = System.Drawing.Color.Black;
            this.mlblTotRecords.Location = new System.Drawing.Point(24, 545);
            this.mlblTotRecords.Name = "mlblTotRecords";
            this.mlblTotRecords.Size = new System.Drawing.Size(0, 0);
            this.mlblTotRecords.TabIndex = 68;
            this.mlblTotRecords.UseCustomBackColor = true;
            this.mlblTotRecords.UseCustomForeColor = true;
            // 
            // FrmListPurchaseReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 570);
            this.Controls.Add(this.mlblTotRecords);
            this.Controls.Add(this.mtxtTotalAmt);
            this.Controls.Add(this.mtxtTotalQty);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.dgvPurchaseReturnDetails);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.mdtpTo);
            this.Controls.Add(this.mdtpFrom);
            this.Controls.Add(this.dgvLstPurchaseReturnMaster);
            this.Controls.Add(this.mcmbParty);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.mbtnRefresh);
            this.Controls.Add(this.mbtnSearch);
            this.Controls.Add(this.mlblName);
            this.Controls.Add(this.hlblHeader);
            this.Name = "FrmListPurchaseReturn";
            this.Load += new System.EventHandler(this.FrmListPurchaseReturn_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmListPurchaseReturn_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPurchaseReturnDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLstPurchaseReturnMaster)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox mtxtTotalAmt;
        private MetroFramework.Controls.MetroTextBox mtxtTotalQty;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.DataGridView dgvPurchaseReturnDetails;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroDateTime mdtpTo;
        private MetroFramework.Controls.MetroDateTime mdtpFrom;
        private System.Windows.Forms.DataGridView dgvLstPurchaseReturnMaster;
        private MetroFramework.Controls.MetroComboBox mcmbParty;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton mbtnRefresh;
        private MetroFramework.Controls.MetroButton mbtnSearch;
        private MetroFramework.Controls.MetroLabel mlblName;
        private MetroFramework.Drawing.Html.HtmlLabel hlblHeader;
        private MetroFramework.Controls.MetroLabel mlblTotRecords;
    }
}