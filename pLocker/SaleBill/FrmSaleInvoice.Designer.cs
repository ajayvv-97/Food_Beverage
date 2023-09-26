
namespace pLocker.SaleBill
{
    partial class FrmSaleInvoice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSaleInvoice));
            this.crvSalesBill = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tlp_printbox = new System.Windows.Forms.TableLayoutPanel();
            this.lab_prinnter = new System.Windows.Forms.Label();
            this.txt_gmail_report = new System.Windows.Forms.TextBox();
            this.btn_gmail = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.txt_ph_report = new System.Windows.Forms.TextBox();
            this.lab_repor_cls = new System.Windows.Forms.Label();
            this.btn_print = new System.Windows.Forms.Button();
            this.btn_whatsup = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.btnMin = new System.Windows.Forms.Button();
            this.hlTitle = new System.Windows.Forms.Label();
            this.btnclose = new System.Windows.Forms.Button();
            this.tlp_printbox.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // crvSalesBill
            // 
            this.crvSalesBill.ActiveViewIndex = -1;
            this.crvSalesBill.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.crvSalesBill.AutoScroll = true;
            this.crvSalesBill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvSalesBill.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvSalesBill.Location = new System.Drawing.Point(160, 55);
            this.crvSalesBill.Name = "crvSalesBill";
            this.crvSalesBill.ShowCloseButton = false;
            this.crvSalesBill.ShowCopyButton = false;
            this.crvSalesBill.ShowGotoPageButton = false;
            this.crvSalesBill.ShowGroupTreeButton = false;
            this.crvSalesBill.ShowLogo = false;
            this.crvSalesBill.ShowParameterPanelButton = false;
            this.crvSalesBill.ShowPrintButton = false;
            this.crvSalesBill.ShowRefreshButton = false;
            this.crvSalesBill.ShowTextSearchButton = false;
            this.crvSalesBill.ShowZoomButton = false;
            this.crvSalesBill.Size = new System.Drawing.Size(341, 567);
            this.crvSalesBill.TabIndex = 66;
            this.crvSalesBill.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // tlp_printbox
            // 
            this.tlp_printbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlp_printbox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlp_printbox.ColumnCount = 1;
            this.tlp_printbox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_printbox.Controls.Add(this.lab_prinnter, 0, 1);
            this.tlp_printbox.Controls.Add(this.txt_gmail_report, 0, 2);
            this.tlp_printbox.Controls.Add(this.btn_gmail, 0, 3);
            this.tlp_printbox.Controls.Add(this.btn_cancel, 0, 6);
            this.tlp_printbox.Controls.Add(this.txt_ph_report, 0, 4);
            this.tlp_printbox.Controls.Add(this.lab_repor_cls, 0, 7);
            this.tlp_printbox.Controls.Add(this.btn_print, 0, 0);
            this.tlp_printbox.Controls.Add(this.btn_whatsup, 0, 5);
            this.tlp_printbox.Location = new System.Drawing.Point(19, 55);
            this.tlp_printbox.Name = "tlp_printbox";
            this.tlp_printbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tlp_printbox.RowCount = 8;
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_printbox.Size = new System.Drawing.Size(125, 379);
            this.tlp_printbox.TabIndex = 67;
            // 
            // lab_prinnter
            // 
            this.lab_prinnter.AutoSize = true;
            this.lab_prinnter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lab_prinnter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_prinnter.ForeColor = System.Drawing.Color.Red;
            this.lab_prinnter.Location = new System.Drawing.Point(3, 77);
            this.lab_prinnter.Name = "lab_prinnter";
            this.lab_prinnter.Size = new System.Drawing.Size(119, 18);
            this.lab_prinnter.TabIndex = 60;
            this.lab_prinnter.Text = "Press F4";
            this.lab_prinnter.Visible = false;
            // 
            // txt_gmail_report
            // 
            this.txt_gmail_report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_gmail_report.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gmail_report.Location = new System.Drawing.Point(3, 98);
            this.txt_gmail_report.Name = "txt_gmail_report";
            this.txt_gmail_report.Size = new System.Drawing.Size(119, 20);
            this.txt_gmail_report.TabIndex = 55;
            // 
            // btn_gmail
            // 
            this.btn_gmail.BackgroundImage = global::pLocker.Properties.Resources.L_gmail;
            this.btn_gmail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_gmail.FlatAppearance.BorderSize = 0;
            this.btn_gmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_gmail.Location = new System.Drawing.Point(3, 124);
            this.btn_gmail.Name = "btn_gmail";
            this.btn_gmail.Size = new System.Drawing.Size(69, 71);
            this.btn_gmail.TabIndex = 56;
            this.btn_gmail.UseVisualStyleBackColor = true;
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackgroundImage = global::pLocker.Properties.Resources.close_1;
            this.btn_cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Location = new System.Drawing.Point(3, 304);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(69, 38);
            this.btn_cancel.TabIndex = 59;
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // txt_ph_report
            // 
            this.txt_ph_report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_ph_report.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ph_report.Location = new System.Drawing.Point(3, 201);
            this.txt_ph_report.Name = "txt_ph_report";
            this.txt_ph_report.Size = new System.Drawing.Size(119, 20);
            this.txt_ph_report.TabIndex = 58;
            // 
            // lab_repor_cls
            // 
            this.lab_repor_cls.AutoSize = true;
            this.lab_repor_cls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lab_repor_cls.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_repor_cls.ForeColor = System.Drawing.Color.Red;
            this.lab_repor_cls.Location = new System.Drawing.Point(3, 345);
            this.lab_repor_cls.Name = "lab_repor_cls";
            this.lab_repor_cls.Size = new System.Drawing.Size(119, 34);
            this.lab_repor_cls.TabIndex = 61;
            this.lab_repor_cls.Text = "Press F6";
            this.lab_repor_cls.Visible = false;
            // 
            // btn_print
            // 
            this.btn_print.BackgroundImage = global::pLocker.Properties.Resources.L_printer;
            this.btn_print.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_print.FlatAppearance.BorderSize = 0;
            this.btn_print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_print.Location = new System.Drawing.Point(3, 3);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(69, 71);
            this.btn_print.TabIndex = 54;
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            // 
            // btn_whatsup
            // 
            this.btn_whatsup.BackgroundImage = global::pLocker.Properties.Resources.L_whatsapp;
            this.btn_whatsup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_whatsup.FlatAppearance.BorderSize = 0;
            this.btn_whatsup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_whatsup.Location = new System.Drawing.Point(3, 227);
            this.btn_whatsup.Name = "btn_whatsup";
            this.btn_whatsup.Size = new System.Drawing.Size(69, 71);
            this.btn_whatsup.TabIndex = 57;
            this.btn_whatsup.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(4)))), ((int)(((byte)(209)))));
            this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btnMin);
            this.panel2.Controls.Add(this.hlTitle);
            this.panel2.Controls.Add(this.btnclose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(513, 39);
            this.panel2.TabIndex = 163;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImage = global::pLocker.Properties.Resources.close_1_button;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(470, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 31);
            this.button1.TabIndex = 163;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.btnMin.Location = new System.Drawing.Point(437, 4);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(27, 30);
            this.btnMin.TabIndex = 174;
            this.btnMin.Text = "-";
            this.btnMin.UseVisualStyleBackColor = false;
            // 
            // hlTitle
            // 
            this.hlTitle.AutoSize = true;
            this.hlTitle.BackColor = System.Drawing.Color.Transparent;
            this.hlTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hlTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
            this.hlTitle.Location = new System.Drawing.Point(14, 6);
            this.hlTitle.Name = "hlTitle";
            this.hlTitle.Size = new System.Drawing.Size(47, 25);
            this.hlTitle.TabIndex = 161;
            this.hlTitle.Text = "BILL";
            // 
            // btnclose
            // 
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(978, 0);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(26, 31);
            this.btnclose.TabIndex = 0;
            this.btnclose.Text = "X";
            this.btnclose.UseVisualStyleBackColor = true;
            // 
            // FrmSaleInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(513, 634);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.tlp_printbox);
            this.Controls.Add(this.crvSalesBill);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSaleInvoice";
            this.Text = "FrmSaleInvoice";
            this.Load += new System.EventHandler(this.FrmSaleInvoice_Load);
            this.tlp_printbox.ResumeLayout(false);
            this.tlp_printbox.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvSalesBill;
        private System.Windows.Forms.TableLayoutPanel tlp_printbox;
        public System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.Label lab_prinnter;
        private System.Windows.Forms.TextBox txt_gmail_report;
        private System.Windows.Forms.Button btn_gmail;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TextBox txt_ph_report;
        private System.Windows.Forms.Button btn_whatsup;
        private System.Windows.Forms.Label lab_repor_cls;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Label hlTitle;
        private System.Windows.Forms.Button btnclose;
    }
}