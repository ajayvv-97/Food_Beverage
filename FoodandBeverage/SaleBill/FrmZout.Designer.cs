
namespace FoodandBeverage.SaleBill
{
    partial class FrmZout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmZout));
            this.tlp_Zout = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_printbox = new System.Windows.Forms.TableLayoutPanel();
            this.btn_gmail = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_whatsup = new System.Windows.Forms.Button();
            this.btn_print = new System.Windows.Forms.Button();
            this.crvout = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.tlp_Zout.SuspendLayout();
            this.tlp_printbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_Zout
            // 
            this.tlp_Zout.ColumnCount = 2;
            this.tlp_Zout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.01156F));
            this.tlp_Zout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.98844F));
            this.tlp_Zout.Controls.Add(this.tlp_printbox, 0, 0);
            this.tlp_Zout.Controls.Add(this.crvout, 1, 0);
            this.tlp_Zout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_Zout.Location = new System.Drawing.Point(0, 0);
            this.tlp_Zout.Name = "tlp_Zout";
            this.tlp_Zout.RowCount = 1;
            this.tlp_Zout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_Zout.Size = new System.Drawing.Size(800, 450);
            this.tlp_Zout.TabIndex = 69;
            // 
            // tlp_printbox
            // 
            this.tlp_printbox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlp_printbox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tlp_printbox.ColumnCount = 1;
            this.tlp_printbox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_printbox.Controls.Add(this.btn_gmail, 0, 5);
            this.tlp_printbox.Controls.Add(this.btn_cancel, 0, 9);
            this.tlp_printbox.Controls.Add(this.btn_whatsup, 0, 7);
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
            this.tlp_printbox.Size = new System.Drawing.Size(202, 444);
            this.tlp_printbox.TabIndex = 66;
            // 
            // btn_gmail
            // 
            this.btn_gmail.BackgroundImage = global::FoodandBeverage.Properties.Resources.GMAIL;
            this.btn_gmail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_gmail.FlatAppearance.BorderSize = 0;
            this.btn_gmail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_gmail.Location = new System.Drawing.Point(25, 120);
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
            this.btn_cancel.Location = new System.Drawing.Point(25, 284);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(25, 3, 3, 3);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(74, 41);
            this.btn_cancel.TabIndex = 59;
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // btn_whatsup
            // 
            this.btn_whatsup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_whatsup.BackgroundImage")));
            this.btn_whatsup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_whatsup.FlatAppearance.BorderSize = 0;
            this.btn_whatsup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_whatsup.Location = new System.Drawing.Point(25, 197);
            this.btn_whatsup.Margin = new System.Windows.Forms.Padding(25, 3, 3, 3);
            this.btn_whatsup.Name = "btn_whatsup";
            this.btn_whatsup.Size = new System.Drawing.Size(69, 71);
            this.btn_whatsup.TabIndex = 57;
            this.btn_whatsup.UseVisualStyleBackColor = true;
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
            // 
            // crvout
            // 
            this.crvout.ActiveViewIndex = -1;
            this.crvout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvout.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvout.Location = new System.Drawing.Point(211, 3);
            this.crvout.Name = "crvout";
            this.crvout.ShowCloseButton = false;
            this.crvout.ShowCopyButton = false;
            this.crvout.ShowGotoPageButton = false;
            this.crvout.ShowGroupTreeButton = false;
            this.crvout.ShowLogo = false;
            this.crvout.ShowParameterPanelButton = false;
            this.crvout.ShowPrintButton = false;
            this.crvout.ShowTextSearchButton = false;
            this.crvout.ShowZoomButton = false;
            this.crvout.Size = new System.Drawing.Size(586, 444);
            this.crvout.TabIndex = 67;
            this.crvout.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // FrmZout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tlp_Zout);
            this.Name = "FrmZout";
            this.Text = "FrmZout";
            this.tlp_Zout.ResumeLayout(false);
            this.tlp_printbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_Zout;
        private System.Windows.Forms.TableLayoutPanel tlp_printbox;
        private System.Windows.Forms.Button btn_gmail;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_whatsup;
        public System.Windows.Forms.Button btn_print;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvout;
    }
}