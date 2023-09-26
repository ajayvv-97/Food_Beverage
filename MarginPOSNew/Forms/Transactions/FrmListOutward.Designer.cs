namespace MarginPOSNew.Forms.Transactions
{
    partial class FrmListOutward
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mlblTotRecords = new MetroFramework.Controls.MetroLabel();
            this.mtxtTotalAmt = new MetroFramework.Controls.MetroTextBox();
            this.mtxtTotalQty = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.dgvOutwardDetails = new System.Windows.Forms.DataGridView();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.mdtpTo = new MetroFramework.Controls.MetroDateTime();
            this.mdtpFrom = new MetroFramework.Controls.MetroDateTime();
            this.dgvLstOutward = new System.Windows.Forms.DataGridView();
            this.mcmbParty = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.mbtnRefresh = new MetroFramework.Controls.MetroButton();
            this.mbtnSearch = new MetroFramework.Controls.MetroButton();
            this.mlblName = new MetroFramework.Controls.MetroLabel();
            this.hlblHeader = new MetroFramework.Drawing.Html.HtmlLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutwardDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLstOutward)).BeginInit();
            this.SuspendLayout();
            // 
            // mlblTotRecords
            // 
            this.mlblTotRecords.AutoSize = true;
            this.mlblTotRecords.BackColor = System.Drawing.Color.Transparent;
            this.mlblTotRecords.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblTotRecords.ForeColor = System.Drawing.Color.Black;
            this.mlblTotRecords.Location = new System.Drawing.Point(3, 496);
            this.mlblTotRecords.Name = "mlblTotRecords";
            this.mlblTotRecords.Size = new System.Drawing.Size(0, 0);
            this.mlblTotRecords.TabIndex = 85;
            this.mlblTotRecords.UseCustomBackColor = true;
            this.mlblTotRecords.UseCustomForeColor = true;
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
            this.mtxtTotalAmt.Location = new System.Drawing.Point(828, 560);
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
            this.mtxtTotalAmt.TabIndex = 84;
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
            this.mtxtTotalQty.Location = new System.Drawing.Point(577, 562);
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
            this.mtxtTotalQty.TabIndex = 83;
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
            this.metroLabel4.Location = new System.Drawing.Point(729, 562);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(93, 19);
            this.metroLabel4.TabIndex = 82;
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
            this.metroLabel3.Location = new System.Drawing.Point(474, 564);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(97, 19);
            this.metroLabel3.TabIndex = 81;
            this.metroLabel3.Text = "Total Quantity";
            this.metroLabel3.UseCustomBackColor = true;
            this.metroLabel3.UseCustomForeColor = true;
            // 
            // dgvOutwardDetails
            // 
            this.dgvOutwardDetails.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvOutwardDetails.AllowUserToAddRows = false;
            this.dgvOutwardDetails.AllowUserToDeleteRows = false;
            this.dgvOutwardDetails.AllowUserToResizeColumns = false;
            this.dgvOutwardDetails.AllowUserToResizeRows = false;
            this.dgvOutwardDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOutwardDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvOutwardDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvOutwardDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvOutwardDetails.ColumnHeadersHeight = 40;
            this.dgvOutwardDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOutwardDetails.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvOutwardDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvOutwardDetails.EnableHeadersVisualStyles = false;
            this.dgvOutwardDetails.Location = new System.Drawing.Point(3, 378);
            this.dgvOutwardDetails.MultiSelect = false;
            this.dgvOutwardDetails.Name = "dgvOutwardDetails";
            this.dgvOutwardDetails.ReadOnly = true;
            this.dgvOutwardDetails.RowHeadersVisible = false;
            this.dgvOutwardDetails.RowTemplate.Height = 30;
            this.dgvOutwardDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvOutwardDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOutwardDetails.ShowCellErrors = false;
            this.dgvOutwardDetails.ShowCellToolTips = false;
            this.dgvOutwardDetails.ShowEditingIcon = false;
            this.dgvOutwardDetails.Size = new System.Drawing.Size(996, 176);
            this.dgvOutwardDetails.TabIndex = 80;
            this.dgvOutwardDetails.TabStop = false;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.ForeColor = System.Drawing.Color.Black;
            this.metroLabel2.Location = new System.Drawing.Point(196, 23);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(41, 25);
            this.metroLabel2.TabIndex = 79;
            this.metroLabel2.Text = "To :";
            // 
            // mdtpTo
            // 
            this.mdtpTo.CustomFormat = "dd-MM-yyyy";
            this.mdtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mdtpTo.Location = new System.Drawing.Point(243, 23);
            this.mdtpTo.MinimumSize = new System.Drawing.Size(0, 29);
            this.mdtpTo.Name = "mdtpTo";
            this.mdtpTo.Size = new System.Drawing.Size(132, 29);
            this.mdtpTo.TabIndex = 71;
            this.mdtpTo.ValueChanged += new System.EventHandler(this.mdtpTo_ValueChanged);
            this.mdtpTo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MdtpTo_KeyUp);
            // 
            // mdtpFrom
            // 
            this.mdtpFrom.CustomFormat = "dd-MM-yyyy";
            this.mdtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mdtpFrom.Location = new System.Drawing.Point(57, 23);
            this.mdtpFrom.MinimumSize = new System.Drawing.Size(0, 29);
            this.mdtpFrom.Name = "mdtpFrom";
            this.mdtpFrom.Size = new System.Drawing.Size(133, 29);
            this.mdtpFrom.TabIndex = 70;
            this.mdtpFrom.ValueChanged += new System.EventHandler(this.mdtpFrom_ValueChanged);
            this.mdtpFrom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MdtpFrom_KeyUp);
            // 
            // dgvLstOutward
            // 
            this.dgvLstOutward.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvLstOutward.AllowUserToAddRows = false;
            this.dgvLstOutward.AllowUserToDeleteRows = false;
            this.dgvLstOutward.AllowUserToResizeColumns = false;
            this.dgvLstOutward.AllowUserToResizeRows = false;
            this.dgvLstOutward.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLstOutward.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvLstOutward.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLstOutward.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvLstOutward.ColumnHeadersHeight = 40;
            this.dgvLstOutward.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLstOutward.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvLstOutward.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLstOutward.EnableHeadersVisualStyles = false;
            this.dgvLstOutward.Location = new System.Drawing.Point(3, 56);
            this.dgvLstOutward.MultiSelect = false;
            this.dgvLstOutward.Name = "dgvLstOutward";
            this.dgvLstOutward.ReadOnly = true;
            this.dgvLstOutward.RowHeadersVisible = false;
            this.dgvLstOutward.RowTemplate.Height = 30;
            this.dgvLstOutward.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvLstOutward.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLstOutward.ShowCellErrors = false;
            this.dgvLstOutward.ShowCellToolTips = false;
            this.dgvLstOutward.ShowEditingIcon = false;
            this.dgvLstOutward.Size = new System.Drawing.Size(996, 308);
            this.dgvLstOutward.TabIndex = 75;
            this.dgvLstOutward.TabStop = false;
            this.dgvLstOutward.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvLstOutward_CellMouseDoubleClick);
            this.dgvLstOutward.SelectionChanged += new System.EventHandler(this.dgvLstOutward_SelectionChanged);
            this.dgvLstOutward.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmListOutward_KeyUp);
            // 
            // mcmbParty
            // 
            this.mcmbParty.BackColor = System.Drawing.Color.White;
            this.mcmbParty.DropDownHeight = 250;
            this.mcmbParty.ForeColor = System.Drawing.Color.Black;
            this.mcmbParty.FormattingEnabled = true;
            this.mcmbParty.IntegralHeight = false;
            this.mcmbParty.ItemHeight = 23;
            this.mcmbParty.Location = new System.Drawing.Point(441, 23);
            this.mcmbParty.Name = "mcmbParty";
            this.mcmbParty.Size = new System.Drawing.Size(259, 29);
            this.mcmbParty.TabIndex = 72;
            this.mcmbParty.Tag = "Department";
            this.mcmbParty.UseSelectable = true;
            this.mcmbParty.KeyUp += new System.Windows.Forms.KeyEventHandler(this.McmbParty_KeyUp);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.Location = new System.Drawing.Point(-3, 23);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(63, 25);
            this.metroLabel1.TabIndex = 76;
            this.metroLabel1.Text = "From :";
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.mbtnRefresh.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnRefresh.ForeColor = System.Drawing.Color.White;
            this.mbtnRefresh.Location = new System.Drawing.Point(882, 11);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(106, 41);
            this.mbtnRefresh.TabIndex = 74;
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
            this.mbtnSearch.Location = new System.Drawing.Point(767, 11);
            this.mbtnSearch.Name = "mbtnSearch";
            this.mbtnSearch.Size = new System.Drawing.Size(106, 41);
            this.mbtnSearch.TabIndex = 73;
            this.mbtnSearch.Text = "&Search";
            this.mbtnSearch.UseCustomBackColor = true;
            this.mbtnSearch.UseCustomForeColor = true;
            this.mbtnSearch.UseSelectable = true;
            this.mbtnSearch.Click += new System.EventHandler(this.MbtnSearch_Click);
            // 
            // mlblName
            // 
            this.mlblName.AutoSize = true;
            this.mlblName.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblName.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblName.ForeColor = System.Drawing.Color.Black;
            this.mlblName.Location = new System.Drawing.Point(381, 23);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(61, 25);
            this.mlblName.TabIndex = 77;
            this.mlblName.Text = "Party :";
            // 
            // hlblHeader
            // 
            this.hlblHeader.AutoScroll = true;
            this.hlblHeader.AutoScrollMinSize = new System.Drawing.Size(174, 36);
            this.hlblHeader.AutoSize = false;
            this.hlblHeader.BackColor = System.Drawing.SystemColors.Window;
            this.hlblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlblHeader.Location = new System.Drawing.Point(-1, -10);
            this.hlblHeader.Name = "hlblHeader";
            this.hlblHeader.Size = new System.Drawing.Size(193, 30);
            this.hlblHeader.TabIndex = 78;
            this.hlblHeader.Text = "Outward Entry List";
            // 
            // FrmListOutward
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 557);
            this.Controls.Add(this.mlblTotRecords);
            this.Controls.Add(this.mtxtTotalAmt);
            this.Controls.Add(this.mtxtTotalQty);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.dgvOutwardDetails);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.mdtpTo);
            this.Controls.Add(this.mdtpFrom);
            this.Controls.Add(this.dgvLstOutward);
            this.Controls.Add(this.mcmbParty);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.mbtnRefresh);
            this.Controls.Add(this.mbtnSearch);
            this.Controls.Add(this.mlblName);
            this.Controls.Add(this.hlblHeader);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmListOutward";
            this.Text = "FrmListOutward";
            this.Load += new System.EventHandler(this.FrmListOutward_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmListOutward_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutwardDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLstOutward)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroLabel mlblTotRecords;
        private MetroFramework.Controls.MetroTextBox mtxtTotalAmt;
        private MetroFramework.Controls.MetroTextBox mtxtTotalQty;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.DataGridView dgvOutwardDetails;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroDateTime mdtpTo;
        private MetroFramework.Controls.MetroDateTime mdtpFrom;
        private System.Windows.Forms.DataGridView dgvLstOutward;
        private MetroFramework.Controls.MetroComboBox mcmbParty;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton mbtnRefresh;
        private MetroFramework.Controls.MetroButton mbtnSearch;
        private MetroFramework.Controls.MetroLabel mlblName;
        private MetroFramework.Drawing.Html.HtmlLabel hlblHeader;
    }
}