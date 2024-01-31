
namespace FoodandBeverage.SaleBill
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
            this.tlp_printbox = new System.Windows.Forms.TableLayoutPanel();
            this.lab_prinnter = new System.Windows.Forms.Label();
            this.txt_gmail_report = new System.Windows.Forms.TextBox();
            this.btn_gmail = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.txt_ph_report = new System.Windows.Forms.TextBox();
            this.btn_whatsup = new System.Windows.Forms.Button();
            this.lab_repor_cls = new System.Windows.Forms.Label();
            this.btn_print = new System.Windows.Forms.Button();
            this.pd_POS = new System.Windows.Forms.PrintDialog();
            this.crvSalesBill = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tlp_main_bill = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_printbox.SuspendLayout();
            this.tlp_main_bill.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_printbox
            // 
            this.tlp_printbox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlp_printbox.ColumnCount = 1;
            this.tlp_printbox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_printbox.Controls.Add(this.lab_prinnter, 0, 3);
            this.tlp_printbox.Controls.Add(this.txt_gmail_report, 0, 4);
            this.tlp_printbox.Controls.Add(this.btn_gmail, 0, 5);
            this.tlp_printbox.Controls.Add(this.btn_cancel, 0, 9);
            this.tlp_printbox.Controls.Add(this.txt_ph_report, 0, 6);
            this.tlp_printbox.Controls.Add(this.btn_whatsup, 0, 7);
            this.tlp_printbox.Controls.Add(this.lab_repor_cls, 0, 10);
            this.tlp_printbox.Controls.Add(this.btn_print, 0, 2);
            this.tlp_printbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_printbox.Location = new System.Drawing.Point(3, 3);
            this.tlp_printbox.Name = "tlp_printbox";
            this.tlp_printbox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tlp_printbox.RowCount = 11;
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_printbox.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_printbox.Size = new System.Drawing.Size(128, 652);
            this.tlp_printbox.TabIndex = 66;
            // 
            // lab_prinnter
            // 
            this.lab_prinnter.AutoSize = true;
            this.lab_prinnter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lab_prinnter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_prinnter.ForeColor = System.Drawing.Color.Red;
            this.lab_prinnter.Location = new System.Drawing.Point(3, 117);
            this.lab_prinnter.Name = "lab_prinnter";
            this.lab_prinnter.Size = new System.Drawing.Size(122, 18);
            this.lab_prinnter.TabIndex = 60;
            this.lab_prinnter.Text = "Press F4";
            this.lab_prinnter.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lab_prinnter.Visible = false;
            // 
            // txt_gmail_report
            // 
            this.txt_gmail_report.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txt_gmail_report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_gmail_report.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_gmail_report.Location = new System.Drawing.Point(3, 138);
            this.txt_gmail_report.Multiline = true;
            this.txt_gmail_report.Name = "txt_gmail_report";
            this.txt_gmail_report.Size = new System.Drawing.Size(122, 31);
            this.txt_gmail_report.TabIndex = 55;
            this.txt_gmail_report.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_gmail_report_KeyPress);
            // 
            // btn_gmail
            // 
            this.btn_gmail.BackgroundImage = global::FoodandBeverage.Properties.Resources.GMAIL;
            this.btn_gmail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_gmail.FlatAppearance.BorderSize = 0;
            this.btn_gmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_gmail.Location = new System.Drawing.Point(25, 175);
            this.btn_gmail.Margin = new System.Windows.Forms.Padding(25, 3, 3, 3);
            this.btn_gmail.Name = "btn_gmail";
            this.btn_gmail.Size = new System.Drawing.Size(69, 71);
            this.btn_gmail.TabIndex = 56;
            this.btn_gmail.UseVisualStyleBackColor = true;
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackgroundImage = global::FoodandBeverage.Properties.Resources.Closebutton;
            this.btn_cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Location = new System.Drawing.Point(25, 376);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(25, 3, 3, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(74, 41);
            this.btn_cancel.TabIndex = 59;
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            this.btn_cancel.MouseLeave += new System.EventHandler(this.btn_cancel_MouseLeave);
            this.btn_cancel.MouseHover += new System.EventHandler(this.btn_cancel_MouseHover);
            // 
            // txt_ph_report
            // 
            this.txt_ph_report.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(223)))));
            this.txt_ph_report.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_ph_report.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ph_report.Location = new System.Drawing.Point(3, 252);
            this.txt_ph_report.Multiline = true;
            this.txt_ph_report.Name = "txt_ph_report";
            this.txt_ph_report.Size = new System.Drawing.Size(122, 31);
            this.txt_ph_report.TabIndex = 58;
            this.txt_ph_report.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_ph_report_KeyPress);
            // 
            // btn_whatsup
            // 
            this.btn_whatsup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_whatsup.BackgroundImage")));
            this.btn_whatsup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_whatsup.FlatAppearance.BorderSize = 0;
            this.btn_whatsup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_whatsup.Location = new System.Drawing.Point(25, 289);
            this.btn_whatsup.Margin = new System.Windows.Forms.Padding(25, 3, 3, 3);
            this.btn_whatsup.Name = "btn_whatsup";
            this.btn_whatsup.Size = new System.Drawing.Size(69, 71);
            this.btn_whatsup.TabIndex = 57;
            this.btn_whatsup.UseVisualStyleBackColor = true;
            // 
            // lab_repor_cls
            // 
            this.lab_repor_cls.AutoSize = true;
            this.lab_repor_cls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lab_repor_cls.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab_repor_cls.ForeColor = System.Drawing.Color.Red;
            this.lab_repor_cls.Location = new System.Drawing.Point(3, 420);
            this.lab_repor_cls.Name = "lab_repor_cls";
            this.lab_repor_cls.Size = new System.Drawing.Size(122, 232);
            this.lab_repor_cls.TabIndex = 61;
            this.lab_repor_cls.Text = "Press F6";
            this.lab_repor_cls.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lab_repor_cls.Visible = false;
            // 
            // btn_print
            // 
            this.btn_print.BackgroundImage = global::FoodandBeverage.Properties.Resources.printer;
            this.btn_print.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_print.FlatAppearance.BorderSize = 0;
            this.btn_print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_print.Location = new System.Drawing.Point(25, 43);
            this.btn_print.Margin = new System.Windows.Forms.Padding(25, 3, 3, 3);
            this.btn_print.Name = "btn_print";
            this.btn_print.Size = new System.Drawing.Size(69, 71);
            this.btn_print.TabIndex = 54;
            this.btn_print.UseVisualStyleBackColor = true;
            this.btn_print.Click += new System.EventHandler(this.btn_print_Click);
            this.btn_print.MouseLeave += new System.EventHandler(this.btn_print_MouseLeave);
            this.btn_print.MouseHover += new System.EventHandler(this.btn_print_MouseHover);
            // 
            // pd_POS
            // 
            this.pd_POS.UseEXDialog = true;
            // 
            // crvSalesBill
            // 
            this.crvSalesBill.ActiveViewIndex = -1;
            this.crvSalesBill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvSalesBill.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvSalesBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvSalesBill.Location = new System.Drawing.Point(137, 3);
            this.crvSalesBill.Name = "crvSalesBill";
            this.crvSalesBill.ShowCloseButton = false;
            this.crvSalesBill.ShowCopyButton = false;
            this.crvSalesBill.ShowGotoPageButton = false;
            this.crvSalesBill.ShowGroupTreeButton = false;
            this.crvSalesBill.ShowLogo = false;
            this.crvSalesBill.ShowParameterPanelButton = false;
            this.crvSalesBill.ShowPrintButton = false;
            this.crvSalesBill.ShowTextSearchButton = false;
            this.crvSalesBill.ShowZoomButton = false;
            this.crvSalesBill.Size = new System.Drawing.Size(379, 652);
            this.crvSalesBill.TabIndex = 67;
            this.crvSalesBill.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // tlp_main_bill
            // 
            this.tlp_main_bill.ColumnCount = 2;
            this.tlp_main_bill.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.01156F));
            this.tlp_main_bill.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.98844F));
            this.tlp_main_bill.Controls.Add(this.tlp_printbox, 0, 0);
            this.tlp_main_bill.Controls.Add(this.crvSalesBill, 1, 0);
            this.tlp_main_bill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main_bill.Location = new System.Drawing.Point(0, 0);
            this.tlp_main_bill.Name = "tlp_main_bill";
            this.tlp_main_bill.RowCount = 1;
            this.tlp_main_bill.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_main_bill.Size = new System.Drawing.Size(519, 658);
            this.tlp_main_bill.TabIndex = 68;
            // 
            // FrmSaleInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 658);
            this.Controls.Add(this.tlp_main_bill);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmSaleInvoice";
            this.Load += new System.EventHandler(this.FrmSaleInvoice_Load);
            this.tlp_printbox.ResumeLayout(false);
            this.tlp_printbox.PerformLayout();
            this.tlp_main_bill.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lab_repor_cls;
        private System.Windows.Forms.Label lab_prinnter;
        private System.Windows.Forms.Button btn_cancel;
        public System.Windows.Forms.Button btn_print;
        private System.Windows.Forms.TextBox txt_ph_report;
        private System.Windows.Forms.Button btn_whatsup;
        private System.Windows.Forms.Button btn_gmail;
        private System.Windows.Forms.TextBox txt_gmail_report;
        private System.Windows.Forms.TableLayoutPanel tlp_printbox;
        private System.Windows.Forms.PrintDialog pd_POS;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvSalesBill;
        private System.Windows.Forms.TableLayoutPanel tlp_main_bill;
    }
}