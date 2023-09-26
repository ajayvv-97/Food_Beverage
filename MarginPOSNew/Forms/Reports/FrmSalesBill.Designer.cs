namespace MarginPOSNew.Forms.Reports
{
    partial class FrmSalesBill
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
            this.crvSalesBill = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crvSalesBill
            // 
            this.crvSalesBill.ActiveViewIndex = -1;
            this.crvSalesBill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvSalesBill.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvSalesBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvSalesBill.Location = new System.Drawing.Point(20, 60);
            this.crvSalesBill.Name = "crvSalesBill";
            this.crvSalesBill.Size = new System.Drawing.Size(760, 370);
            this.crvSalesBill.TabIndex = 0;
            this.crvSalesBill.Tag = "SalesBill";
            this.crvSalesBill.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // FrmSalesBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.crvSalesBill);
            this.Name = "FrmSalesBill";
            this.Load += new System.EventHandler(this.FrmSalesBill_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvSalesBill;
    }
}