namespace MarginPOSNew.Forms.Transactions
{
    partial class FrmListSales
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
            this.hlblHeader = new MetroFramework.Drawing.Html.HtmlLabel();
            this.dgvLstSalesMaster = new System.Windows.Forms.DataGridView();
            this.dgvSalesDetails = new System.Windows.Forms.DataGridView();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.mdtpTo = new MetroFramework.Controls.MetroDateTime();
            this.mdtpFrom = new MetroFramework.Controls.MetroDateTime();
            this.mcmbParty = new MetroFramework.Controls.MetroComboBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.mbtnRefresh = new MetroFramework.Controls.MetroButton();
            this.mbtnSearch = new MetroFramework.Controls.MetroButton();
            this.mlblName = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.mtxtTotalQty = new MetroFramework.Controls.MetroTextBox();
            this.mtxtTotalAmt = new MetroFramework.Controls.MetroTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLstSalesMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // hlblHeader
            // 
            this.hlblHeader.AutoScroll = true;
            this.hlblHeader.AutoScrollMinSize = new System.Drawing.Size(90, 36);
            this.hlblHeader.AutoSize = false;
            this.hlblHeader.BackColor = System.Drawing.SystemColors.Window;
            this.hlblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlblHeader.Location = new System.Drawing.Point(4, 8);
            this.hlblHeader.Name = "hlblHeader";
            this.hlblHeader.Size = new System.Drawing.Size(183, 38);
            this.hlblHeader.TabIndex = 23;
            this.hlblHeader.Text = "Sales List";
            // 
            // dgvLstSalesMaster
            // 
            this.dgvLstSalesMaster.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvLstSalesMaster.AllowUserToAddRows = false;
            this.dgvLstSalesMaster.AllowUserToDeleteRows = false;
            this.dgvLstSalesMaster.AllowUserToResizeColumns = false;
            this.dgvLstSalesMaster.AllowUserToResizeRows = false;
            this.dgvLstSalesMaster.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLstSalesMaster.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvLstSalesMaster.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLstSalesMaster.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLstSalesMaster.ColumnHeadersHeight = 40;
            this.dgvLstSalesMaster.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLstSalesMaster.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLstSalesMaster.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvLstSalesMaster.EnableHeadersVisualStyles = false;
            this.dgvLstSalesMaster.Location = new System.Drawing.Point(15, 85);
            this.dgvLstSalesMaster.MultiSelect = false;
            this.dgvLstSalesMaster.Name = "dgvLstSalesMaster";
            this.dgvLstSalesMaster.ReadOnly = true;
            this.dgvLstSalesMaster.RowHeadersVisible = false;
            this.dgvLstSalesMaster.RowTemplate.Height = 30;
            this.dgvLstSalesMaster.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvLstSalesMaster.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLstSalesMaster.ShowCellErrors = false;
            this.dgvLstSalesMaster.ShowCellToolTips = false;
            this.dgvLstSalesMaster.ShowEditingIcon = false;
            this.dgvLstSalesMaster.Size = new System.Drawing.Size(996, 331);
            this.dgvLstSalesMaster.TabIndex = 5;
            this.dgvLstSalesMaster.TabStop = false;
            this.dgvLstSalesMaster.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLstSalesMaster_CellContentClick);
            this.dgvLstSalesMaster.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgvLstPurchaseMaster_CellMouseDoubleClick);
            this.dgvLstSalesMaster.SelectionChanged += new System.EventHandler(this.DgvLstPurchaseMaster_SelectionChanged);
            this.dgvLstSalesMaster.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DgvLstSalesMaster_KeyUp);
            // 
            // dgvSalesDetails
            // 
            this.dgvSalesDetails.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvSalesDetails.AllowUserToAddRows = false;
            this.dgvSalesDetails.AllowUserToDeleteRows = false;
            this.dgvSalesDetails.AllowUserToResizeColumns = false;
            this.dgvSalesDetails.AllowUserToResizeRows = false;
            this.dgvSalesDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSalesDetails.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvSalesDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvSalesDetails.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvSalesDetails.ColumnHeadersHeight = 40;
            this.dgvSalesDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvSalesDetails.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvSalesDetails.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvSalesDetails.EnableHeadersVisualStyles = false;
            this.dgvSalesDetails.Location = new System.Drawing.Point(15, 424);
            this.dgvSalesDetails.MultiSelect = false;
            this.dgvSalesDetails.Name = "dgvSalesDetails";
            this.dgvSalesDetails.ReadOnly = true;
            this.dgvSalesDetails.RowHeadersVisible = false;
            this.dgvSalesDetails.RowTemplate.Height = 30;
            this.dgvSalesDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvSalesDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvSalesDetails.ShowCellErrors = false;
            this.dgvSalesDetails.ShowCellToolTips = false;
            this.dgvSalesDetails.ShowEditingIcon = false;
            this.dgvSalesDetails.Size = new System.Drawing.Size(996, 176);
            this.dgvSalesDetails.TabIndex = 36;
            this.dgvSalesDetails.TabStop = false;
            this.dgvSalesDetails.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSalesDetails_CellContentClick);
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.ForeColor = System.Drawing.Color.Black;
            this.metroLabel2.Location = new System.Drawing.Point(245, 50);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(41, 25);
            this.metroLabel2.TabIndex = 44;
            this.metroLabel2.Text = "To :";
            // 
            // mdtpTo
            // 
            this.mdtpTo.CustomFormat = "dd-MM-yyyy";
            this.mdtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mdtpTo.Location = new System.Drawing.Point(292, 50);
            this.mdtpTo.MinimumSize = new System.Drawing.Size(0, 29);
            this.mdtpTo.Name = "mdtpTo";
            this.mdtpTo.Size = new System.Drawing.Size(163, 29);
            this.mdtpTo.TabIndex = 1;
            this.mdtpTo.Value = new System.DateTime(2019, 12, 9, 0, 0, 0, 0);
            this.mdtpTo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mdtpTo_KeyUp);
            // 
            // mdtpFrom
            // 
            this.mdtpFrom.CustomFormat = "dd-MM-yyyy";
            this.mdtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.mdtpFrom.Location = new System.Drawing.Point(76, 50);
            this.mdtpFrom.MinimumSize = new System.Drawing.Size(0, 29);
            this.mdtpFrom.Name = "mdtpFrom";
            this.mdtpFrom.Size = new System.Drawing.Size(163, 29);
            this.mdtpFrom.TabIndex = 0;
            this.mdtpFrom.Value = new System.DateTime(2019, 12, 9, 0, 0, 0, 0);
            this.mdtpFrom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mdtpFrom_KeyUp);
            // 
            // mcmbParty
            // 
            this.mcmbParty.BackColor = System.Drawing.Color.White;
            this.mcmbParty.DropDownHeight = 250;
            this.mcmbParty.ForeColor = System.Drawing.Color.Black;
            this.mcmbParty.FormattingEnabled = true;
            this.mcmbParty.IntegralHeight = false;
            this.mcmbParty.ItemHeight = 23;
            this.mcmbParty.Location = new System.Drawing.Point(528, 50);
            this.mcmbParty.Name = "mcmbParty";
            this.mcmbParty.Size = new System.Drawing.Size(259, 29);
            this.mcmbParty.TabIndex = 2;
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
            this.metroLabel1.Location = new System.Drawing.Point(8, 50);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(63, 25);
            this.metroLabel1.TabIndex = 40;
            this.metroLabel1.Text = "From :";
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackColor = System.Drawing.Color.LimeGreen;
            this.mbtnRefresh.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.mbtnRefresh.ForeColor = System.Drawing.Color.White;
            this.mbtnRefresh.Location = new System.Drawing.Point(905, 34);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(106, 45);
            this.mbtnRefresh.TabIndex = 4;
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
            this.mbtnSearch.Location = new System.Drawing.Point(793, 34);
            this.mbtnSearch.Name = "mbtnSearch";
            this.mbtnSearch.Size = new System.Drawing.Size(106, 45);
            this.mbtnSearch.TabIndex = 3;
            this.mbtnSearch.Text = "&Search";
            this.mbtnSearch.UseCustomBackColor = true;
            this.mbtnSearch.UseCustomForeColor = true;
            this.mbtnSearch.UseSelectable = true;
            this.mbtnSearch.Click += new System.EventHandler(this.MbtnSearch_Click);
            this.mbtnSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.mbtnSearch_KeyUp);
            // 
            // mlblName
            // 
            this.mlblName.AutoSize = true;
            this.mlblName.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.mlblName.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.mlblName.ForeColor = System.Drawing.Color.Black;
            this.mlblName.Location = new System.Drawing.Point(461, 50);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(61, 25);
            this.mlblName.TabIndex = 41;
            this.mlblName.Text = "Party :";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.ForeColor = System.Drawing.Color.Black;
            this.metroLabel3.Location = new System.Drawing.Point(480, 606);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(104, 19);
            this.metroLabel3.TabIndex = 45;
            this.metroLabel3.Text = "Total Quantity :";
            this.metroLabel3.UseCustomBackColor = true;
            this.metroLabel3.UseCustomForeColor = true;
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.BackColor = System.Drawing.Color.Transparent;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.ForeColor = System.Drawing.Color.Black;
            this.metroLabel4.Location = new System.Drawing.Point(696, 606);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(100, 19);
            this.metroLabel4.TabIndex = 46;
            this.metroLabel4.Text = "Total Amount :";
            this.metroLabel4.UseCustomBackColor = true;
            this.metroLabel4.UseCustomForeColor = true;
            // 
            // mtxtTotalQty
            // 
            // 
            // 
            // 
            this.mtxtTotalQty.CustomButton.Image = null;
            this.mtxtTotalQty.CustomButton.Location = new System.Drawing.Point(85, 1);
            this.mtxtTotalQty.CustomButton.Name = "";
            this.mtxtTotalQty.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtTotalQty.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtTotalQty.CustomButton.TabIndex = 1;
            this.mtxtTotalQty.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtTotalQty.CustomButton.UseSelectable = true;
            this.mtxtTotalQty.CustomButton.Visible = false;
            this.mtxtTotalQty.Lines = new string[0];
            this.mtxtTotalQty.Location = new System.Drawing.Point(583, 604);
            this.mtxtTotalQty.MaxLength = 50;
            this.mtxtTotalQty.Name = "mtxtTotalQty";
            this.mtxtTotalQty.PasswordChar = '\0';
            this.mtxtTotalQty.ReadOnly = true;
            this.mtxtTotalQty.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtTotalQty.SelectedText = "";
            this.mtxtTotalQty.SelectionLength = 0;
            this.mtxtTotalQty.SelectionStart = 0;
            this.mtxtTotalQty.ShortcutsEnabled = true;
            this.mtxtTotalQty.Size = new System.Drawing.Size(107, 23);
            this.mtxtTotalQty.TabIndex = 47;
            this.mtxtTotalQty.TabStop = false;
            this.mtxtTotalQty.Tag = "";
            this.mtxtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtxtTotalQty.UseSelectable = true;
            this.mtxtTotalQty.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtTotalQty.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // mtxtTotalAmt
            // 
            // 
            // 
            // 
            this.mtxtTotalAmt.CustomButton.Image = null;
            this.mtxtTotalAmt.CustomButton.Location = new System.Drawing.Point(187, 1);
            this.mtxtTotalAmt.CustomButton.Name = "";
            this.mtxtTotalAmt.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.mtxtTotalAmt.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtTotalAmt.CustomButton.TabIndex = 1;
            this.mtxtTotalAmt.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtTotalAmt.CustomButton.UseSelectable = true;
            this.mtxtTotalAmt.CustomButton.Visible = false;
            this.mtxtTotalAmt.Lines = new string[0];
            this.mtxtTotalAmt.Location = new System.Drawing.Point(802, 606);
            this.mtxtTotalAmt.MaxLength = 50;
            this.mtxtTotalAmt.Name = "mtxtTotalAmt";
            this.mtxtTotalAmt.PasswordChar = '\0';
            this.mtxtTotalAmt.ReadOnly = true;
            this.mtxtTotalAmt.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtTotalAmt.SelectedText = "";
            this.mtxtTotalAmt.SelectionLength = 0;
            this.mtxtTotalAmt.SelectionStart = 0;
            this.mtxtTotalAmt.ShortcutsEnabled = true;
            this.mtxtTotalAmt.Size = new System.Drawing.Size(209, 23);
            this.mtxtTotalAmt.TabIndex = 48;
            this.mtxtTotalAmt.TabStop = false;
            this.mtxtTotalAmt.Tag = "";
            this.mtxtTotalAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mtxtTotalAmt.UseSelectable = true;
            this.mtxtTotalAmt.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtTotalAmt.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // FrmListSales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(1026, 635);
            this.Controls.Add(this.mtxtTotalAmt);
            this.Controls.Add(this.mtxtTotalQty);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.mdtpTo);
            this.Controls.Add(this.mdtpFrom);
            this.Controls.Add(this.mcmbParty);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.mbtnRefresh);
            this.Controls.Add(this.mbtnSearch);
            this.Controls.Add(this.mlblName);
            this.Controls.Add(this.dgvSalesDetails);
            this.Controls.Add(this.dgvLstSalesMaster);
            this.Controls.Add(this.hlblHeader);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmListSales";
            this.Resizable = false;
            this.Load += new System.EventHandler(this.FrmListSales_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmListSales_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLstSalesMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Drawing.Html.HtmlLabel hlblHeader;
        private System.Windows.Forms.DataGridView dgvLstSalesMaster;
        private System.Windows.Forms.DataGridView dgvSalesDetails;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroDateTime mdtpTo;
        private MetroFramework.Controls.MetroDateTime mdtpFrom;
        private MetroFramework.Controls.MetroComboBox mcmbParty;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroButton mbtnRefresh;
        private MetroFramework.Controls.MetroButton mbtnSearch;
        private MetroFramework.Controls.MetroLabel mlblName;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroTextBox mtxtTotalQty;
        private MetroFramework.Controls.MetroTextBox mtxtTotalAmt;
    }
}