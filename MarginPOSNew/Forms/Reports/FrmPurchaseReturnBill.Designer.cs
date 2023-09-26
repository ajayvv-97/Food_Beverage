namespace MarginPOSNew.Forms.Reports
{
    partial class FrmPurchaseReturnBill
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
            this.crvPurchaseReturnBill = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crvPurchaseReturnBill
            // 
            this.crvPurchaseReturnBill.ActiveViewIndex = -1;
            this.crvPurchaseReturnBill.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crvPurchaseReturnBill.Cursor = System.Windows.Forms.Cursors.Default;
            this.crvPurchaseReturnBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crvPurchaseReturnBill.Location = new System.Drawing.Point(20, 60);
            this.crvPurchaseReturnBill.Name = "crvPurchaseReturnBill";
            this.crvPurchaseReturnBill.Size = new System.Drawing.Size(760, 370);
            this.crvPurchaseReturnBill.TabIndex = 1;
            this.crvPurchaseReturnBill.Tag = "SalesBill";
            this.crvPurchaseReturnBill.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            // 
            // FrmPurchaseReturnBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.crvPurchaseReturnBill);
            this.Name = "FrmPurchaseReturnBill";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer crvPurchaseReturnBill;
    }
}