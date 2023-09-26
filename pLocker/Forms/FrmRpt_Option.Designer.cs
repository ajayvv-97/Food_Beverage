using CrystalDecisions.Windows.Forms;

namespace pLocker.Forms
{
    partial class FrmRpt_Option
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRpt_Option));
            this.hlblHeader = new MetroFramework.Drawing.Html.HtmlLabel();
            this.mlblTo = new MetroFramework.Controls.MetroLabel();
            this.mdtpTo = new MetroFramework.Controls.MetroDateTime();
            this.mdtpFrom = new MetroFramework.Controls.MetroDateTime();
            this.mlblFrom = new MetroFramework.Controls.MetroLabel();
            this.mcmbReports = new MetroFramework.Controls.MetroComboBox();
            this.mcmbField = new MetroFramework.Controls.MetroComboBox();
            this.mtxtFieldValue = new MetroFramework.Controls.MetroTextBox();
            this.lblRProgress = new System.Windows.Forms.Label();
            this.mbtnSearch = new System.Windows.Forms.Button();
            this.mbtnClose = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMin = new System.Windows.Forms.Button();
            this.btnFrmClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CrvPlocker = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hlblHeader
            // 
            this.hlblHeader.AutoScroll = true;
            this.hlblHeader.AutoScrollMinSize = new System.Drawing.Size(69, 36);
            this.hlblHeader.AutoSize = false;
            this.hlblHeader.BackColor = System.Drawing.SystemColors.Window;
            this.hlblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
            this.hlblHeader.Location = new System.Drawing.Point(16, 19);
            this.hlblHeader.Name = "hlblHeader";
            this.hlblHeader.Size = new System.Drawing.Size(175, 38);
            this.hlblHeader.TabIndex = 7;
            this.hlblHeader.Text = "Report";
            // 
            // mlblTo
            // 
            this.mlblTo.AutoSize = true;
            this.mlblTo.BackColor = System.Drawing.Color.Transparent;
            this.mlblTo.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblTo.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
            this.mlblTo.Location = new System.Drawing.Point(690, 28);
            this.mlblTo.Name = "mlblTo";
            this.mlblTo.Size = new System.Drawing.Size(30, 25);
            this.mlblTo.TabIndex = 35;
            this.mlblTo.Text = "To";
            // 
            // mdtpTo
            // 
            this.mdtpTo.CustomFormat = "dd-MM-yyyy";
            this.mdtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mdtpTo.Location = new System.Drawing.Point(728, 24);
            this.mdtpTo.MinimumSize = new System.Drawing.Size(0, 29);
            this.mdtpTo.Name = "mdtpTo";
            this.mdtpTo.Size = new System.Drawing.Size(112, 29);
            this.mdtpTo.TabIndex = 2;
            this.mdtpTo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mdtpTo_KeyDown);
            // 
            // mdtpFrom
            // 
            this.mdtpFrom.CustomFormat = "dd-MM-yyyy";
            this.mdtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mdtpFrom.Location = new System.Drawing.Point(562, 25);
            this.mdtpFrom.MinimumSize = new System.Drawing.Size(0, 29);
            this.mdtpFrom.Name = "mdtpFrom";
            this.mdtpFrom.Size = new System.Drawing.Size(112, 29);
            this.mdtpFrom.TabIndex = 1;
            this.mdtpFrom.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mdtpFrom_KeyDown);
            // 
            // mlblFrom
            // 
            this.mlblFrom.AutoSize = true;
            this.mlblFrom.BackColor = System.Drawing.Color.Transparent;
            this.mlblFrom.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblFrom.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblFrom.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
            this.mlblFrom.Location = new System.Drawing.Point(502, 29);
            this.mlblFrom.Name = "mlblFrom";
            this.mlblFrom.Size = new System.Drawing.Size(54, 25);
            this.mlblFrom.TabIndex = 33;
            this.mlblFrom.Text = "From";
            // 
            // mcmbReports
            // 
            this.mcmbReports.BackColor = System.Drawing.Color.White;
            this.mcmbReports.DropDownHeight = 250;
            this.mcmbReports.ForeColor = System.Drawing.Color.Black;
            this.mcmbReports.FormattingEnabled = true;
            this.mcmbReports.IntegralHeight = false;
            this.mcmbReports.ItemHeight = 23;
            this.mcmbReports.Location = new System.Drawing.Point(195, 24);
            this.mcmbReports.Name = "mcmbReports";
            this.mcmbReports.Size = new System.Drawing.Size(259, 29);
            this.mcmbReports.TabIndex = 0;
            this.mcmbReports.Tag = "Department";
            this.mcmbReports.UseSelectable = true;
            this.mcmbReports.SelectionChangeCommitted += new System.EventHandler(this.mcmbReports_SelectionChangeCommitted);
            this.mcmbReports.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbReports_KeyDown);
            // 
            // mcmbField
            // 
            this.mcmbField.BackColor = System.Drawing.Color.White;
            this.mcmbField.DropDownHeight = 250;
            this.mcmbField.FormattingEnabled = true;
            this.mcmbField.IntegralHeight = false;
            this.mcmbField.ItemHeight = 23;
            this.mcmbField.Location = new System.Drawing.Point(16, 61);
            this.mcmbField.Name = "mcmbField";
            this.mcmbField.Size = new System.Drawing.Size(173, 29);
            this.mcmbField.TabIndex = 3;
            this.mcmbField.Tag = "Category";
            this.mcmbField.UseCustomBackColor = true;
            this.mcmbField.UseCustomForeColor = true;
            this.mcmbField.UseSelectable = true;
            this.mcmbField.Enter += new System.EventHandler(this.mcmbField_Enter);
            this.mcmbField.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbField_KeyDown);
            // 
            // mtxtFieldValue
            // 
            // 
            // 
            // 
            this.mtxtFieldValue.CustomButton.Image = null;
            this.mtxtFieldValue.CustomButton.Location = new System.Drawing.Point(231, 1);
            this.mtxtFieldValue.CustomButton.Name = "";
            this.mtxtFieldValue.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.mtxtFieldValue.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtFieldValue.CustomButton.TabIndex = 1;
            this.mtxtFieldValue.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtFieldValue.CustomButton.UseSelectable = true;
            this.mtxtFieldValue.CustomButton.Visible = false;
            this.mtxtFieldValue.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.mtxtFieldValue.Lines = new string[0];
            this.mtxtFieldValue.Location = new System.Drawing.Point(195, 61);
            this.mtxtFieldValue.MaxLength = 32767;
            this.mtxtFieldValue.Name = "mtxtFieldValue";
            this.mtxtFieldValue.PasswordChar = '\0';
            this.mtxtFieldValue.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtFieldValue.SelectedText = "";
            this.mtxtFieldValue.SelectionLength = 0;
            this.mtxtFieldValue.SelectionStart = 0;
            this.mtxtFieldValue.ShortcutsEnabled = true;
            this.mtxtFieldValue.Size = new System.Drawing.Size(259, 29);
            this.mtxtFieldValue.TabIndex = 4;
            this.mtxtFieldValue.UseSelectable = true;
            this.mtxtFieldValue.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtFieldValue.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtFieldValue.TextChanged += new System.EventHandler(this.mtxtFieldValue_TextChanged);
            this.mtxtFieldValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtFieldValue_KeyDown);
            // 
            // lblRProgress
            // 
            this.lblRProgress.AutoSize = true;
            this.lblRProgress.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRProgress.ForeColor = System.Drawing.Color.Red;
            this.lblRProgress.Location = new System.Drawing.Point(244, 262);
            this.lblRProgress.Name = "lblRProgress";
            this.lblRProgress.Size = new System.Drawing.Size(489, 45);
            this.lblRProgress.TabIndex = 36;
            this.lblRProgress.Text = "Preparing Report  Please Wait ...";
            this.lblRProgress.Click += new System.EventHandler(this.lblRProgress_Click);
            // 
            // mbtnSearch
            // 
            this.mbtnSearch.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mbtnSearch.BackColor = System.Drawing.Color.Transparent;
            this.mbtnSearch.BackgroundImage = global::pLocker.Properties.Resources.search_1;
            this.mbtnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnSearch.FlatAppearance.BorderSize = 0;
            this.mbtnSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnSearch.Location = new System.Drawing.Point(881, 23);
            this.mbtnSearch.Name = "mbtnSearch";
            this.mbtnSearch.Size = new System.Drawing.Size(132, 44);
            this.mbtnSearch.TabIndex = 37;
            this.mbtnSearch.UseVisualStyleBackColor = false;
            this.mbtnSearch.Click += new System.EventHandler(this.mbtnSearch_Click);
            this.mbtnSearch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbtnSearch_MouseDown);
            this.mbtnSearch.MouseLeave += new System.EventHandler(this.mbtnSearch_MouseLeave);
            this.mbtnSearch.MouseHover += new System.EventHandler(this.mbtnSearch_MouseHover);
            this.mbtnSearch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbtnSearch_MouseUp);
            // 
            // mbtnClose
            // 
            this.mbtnClose.BackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.BackgroundImage = global::pLocker.Properties.Resources.close_1;
            this.mbtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnClose.FlatAppearance.BorderSize = 0;
            this.mbtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnClose.Location = new System.Drawing.Point(881, 70);
            this.mbtnClose.Name = "mbtnClose";
            this.mbtnClose.Size = new System.Drawing.Size(132, 44);
            this.mbtnClose.TabIndex = 38;
            this.mbtnClose.UseVisualStyleBackColor = false;
            this.mbtnClose.Click += new System.EventHandler(this.mbtnClose_Click);
            this.mbtnClose.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mbtnClose_MouseDown);
            this.mbtnClose.MouseLeave += new System.EventHandler(this.mbtnClose_MouseLeave);
            this.mbtnClose.MouseHover += new System.EventHandler(this.mbtnClose_MouseHover);
            this.mbtnClose.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mbtnClose_MouseUp);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.btnMin);
            this.panel3.Controls.Add(this.btnFrmClose);
            this.panel3.Location = new System.Drawing.Point(-2, 1);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1038, 34);
            this.panel3.TabIndex = 115;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
            this.label1.Location = new System.Drawing.Point(10, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 25);
            this.label1.TabIndex = 177;
            this.label1.Text = "Report";
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
            this.btnMin.Location = new System.Drawing.Point(980, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(27, 30);
            this.btnMin.TabIndex = 176;
            this.btnMin.Text = "-";
            this.btnMin.UseVisualStyleBackColor = false;
            // 
            // btnFrmClose
            // 
            this.btnFrmClose.BackColor = System.Drawing.Color.Transparent;
            this.btnFrmClose.BackgroundImage = global::pLocker.Properties.Resources.close_1_button;
            this.btnFrmClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFrmClose.FlatAppearance.BorderSize = 0;
            this.btnFrmClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnFrmClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnFrmClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFrmClose.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFrmClose.ForeColor = System.Drawing.Color.Transparent;
            this.btnFrmClose.Location = new System.Drawing.Point(1009, 0);
            this.btnFrmClose.Name = "btnFrmClose";
            this.btnFrmClose.Size = new System.Drawing.Size(26, 30);
            this.btnFrmClose.TabIndex = 43;
            this.btnFrmClose.UseVisualStyleBackColor = false;
            this.btnFrmClose.Click += new System.EventHandler(this.btnFrmClose_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.panel1.Controls.Add(this.CrvPlocker);
            this.panel1.Controls.Add(this.mdtpFrom);
            this.panel1.Controls.Add(this.hlblHeader);
            this.panel1.Controls.Add(this.lblRProgress);
            this.panel1.Controls.Add(this.mbtnClose);
            this.panel1.Controls.Add(this.mlblFrom);
            this.panel1.Controls.Add(this.mbtnSearch);
            this.panel1.Controls.Add(this.mdtpTo);
            this.panel1.Controls.Add(this.mlblTo);
            this.panel1.Controls.Add(this.mtxtFieldValue);
            this.panel1.Controls.Add(this.mcmbReports);
            this.panel1.Controls.Add(this.mcmbField);
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1035, 523);
            this.panel1.TabIndex = 116;
            // 
            // CrvPlocker
            // 
            this.CrvPlocker.ActiveViewIndex = -1;
            this.CrvPlocker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CrvPlocker.Cursor = System.Windows.Forms.Cursors.Default;
            this.CrvPlocker.Location = new System.Drawing.Point(16, 120);
            this.CrvPlocker.Name = "CrvPlocker";
            this.CrvPlocker.ShowCloseButton = false;
            this.CrvPlocker.ShowCopyButton = false;
            this.CrvPlocker.ShowGotoPageButton = false;
            this.CrvPlocker.ShowGroupTreeButton = false;
            this.CrvPlocker.ShowLogo = false;
            this.CrvPlocker.ShowPageNavigateButtons = false;
            this.CrvPlocker.ShowParameterPanelButton = false;
            this.CrvPlocker.ShowPrintButton = false;
            this.CrvPlocker.ShowRefreshButton = false;
            this.CrvPlocker.ShowTextSearchButton = false;
            this.CrvPlocker.ShowZoomButton = false;
            this.CrvPlocker.Size = new System.Drawing.Size(997, 398);
            this.CrvPlocker.TabIndex = 39;
            this.CrvPlocker.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // FrmRpt_Option
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(1036, 558);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmRpt_Option";
            this.Resizable = false;
            this.Load += new System.EventHandler(this.FrmRpt_Option_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmRpt_Option_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmRpt_Option_KeyUp);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Drawing.Html.HtmlLabel hlblHeader;
        private MetroFramework.Controls.MetroLabel mlblTo;
        private MetroFramework.Controls.MetroDateTime mdtpTo;
        private MetroFramework.Controls.MetroDateTime mdtpFrom;
        private MetroFramework.Controls.MetroLabel mlblFrom;
        private MetroFramework.Controls.MetroComboBox mcmbReports;
        private MetroFramework.Controls.MetroComboBox mcmbField;
        private MetroFramework.Controls.MetroTextBox mtxtFieldValue;
        private System.Windows.Forms.Label lblRProgress;
        private System.Windows.Forms.Button mbtnSearch;
        private System.Windows.Forms.Button mbtnClose;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Button btnFrmClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private CrystalReportViewer CrvPlocker;
    }
}