
namespace FoodandBeverage.Forms
{
    partial class FrmDashboardReport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDashboardReport));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.pnl_dashboard = new System.Windows.Forms.Panel();
            this.tlp_main_dashboard = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_date_dashboard = new System.Windows.Forms.TableLayoutPanel();
            this.dtp_dashboard = new System.Windows.Forms.DateTimePicker();
            this.tlb_panelboxdetails = new System.Windows.Forms.TableLayoutPanel();
            this.cpbsaletop = new CircularProgressBar.CircularProgressBar();
            this.pnl_cur_date_sales = new System.Windows.Forms.Panel();
            this.tlp_cur_total = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lbldinetotal = new System.Windows.Forms.Label();
            this.lbldinetotalno = new System.Windows.Forms.Label();
            this.pnltotalsales = new System.Windows.Forms.Panel();
            this.tlp_saletotal = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblsaletotal = new System.Windows.Forms.Label();
            this.lbltotalsaleno = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnl_dashboard.SuspendLayout();
            this.tlp_main_dashboard.SuspendLayout();
            this.tlp_date_dashboard.SuspendLayout();
            this.tlb_panelboxdetails.SuspendLayout();
            this.pnl_cur_date_sales.SuspendLayout();
            this.tlp_cur_total.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.pnltotalsales.SuspendLayout();
            this.tlp_saletotal.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_dashboard
            // 
            this.pnl_dashboard.Controls.Add(this.tlp_main_dashboard);
            this.pnl_dashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_dashboard.Location = new System.Drawing.Point(0, 0);
            this.pnl_dashboard.Name = "pnl_dashboard";
            this.pnl_dashboard.Size = new System.Drawing.Size(800, 450);
            this.pnl_dashboard.TabIndex = 3;
            // 
            // tlp_main_dashboard
            // 
            this.tlp_main_dashboard.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tlp_main_dashboard.ColumnCount = 1;
            this.tlp_main_dashboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main_dashboard.Controls.Add(this.tlp_date_dashboard, 0, 0);
            this.tlp_main_dashboard.Controls.Add(this.tlb_panelboxdetails, 0, 1);
            this.tlp_main_dashboard.Controls.Add(this.chart1, 0, 2);
            this.tlp_main_dashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main_dashboard.Location = new System.Drawing.Point(0, 0);
            this.tlp_main_dashboard.Name = "tlp_main_dashboard";
            this.tlp_main_dashboard.RowCount = 3;
            this.tlp_main_dashboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.175912F));
            this.tlp_main_dashboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlp_main_dashboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75.95238F));
            this.tlp_main_dashboard.Size = new System.Drawing.Size(800, 450);
            this.tlp_main_dashboard.TabIndex = 0;
            // 
            // tlp_date_dashboard
            // 
            this.tlp_date_dashboard.ColumnCount = 2;
            this.tlp_date_dashboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75.13228F));
            this.tlp_date_dashboard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 24.86773F));
            this.tlp_date_dashboard.Controls.Add(this.dtp_dashboard, 1, 0);
            this.tlp_date_dashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_date_dashboard.Location = new System.Drawing.Point(3, 3);
            this.tlp_date_dashboard.Name = "tlp_date_dashboard";
            this.tlp_date_dashboard.RowCount = 1;
            this.tlp_date_dashboard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_date_dashboard.Size = new System.Drawing.Size(794, 12);
            this.tlp_date_dashboard.TabIndex = 1;
            // 
            // dtp_dashboard
            // 
            this.dtp_dashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtp_dashboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_dashboard.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_dashboard.Location = new System.Drawing.Point(599, 3);
            this.dtp_dashboard.Name = "dtp_dashboard";
            this.dtp_dashboard.Size = new System.Drawing.Size(192, 24);
            this.dtp_dashboard.TabIndex = 0;
            // 
            // tlb_panelboxdetails
            // 
            this.tlb_panelboxdetails.BackColor = System.Drawing.Color.Silver;
            this.tlb_panelboxdetails.ColumnCount = 3;
            this.tlb_panelboxdetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlb_panelboxdetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlb_panelboxdetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlb_panelboxdetails.Controls.Add(this.cpbsaletop, 2, 0);
            this.tlb_panelboxdetails.Controls.Add(this.pnl_cur_date_sales, 1, 0);
            this.tlb_panelboxdetails.Controls.Add(this.pnltotalsales, 0, 0);
            this.tlb_panelboxdetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlb_panelboxdetails.Location = new System.Drawing.Point(3, 21);
            this.tlb_panelboxdetails.Name = "tlb_panelboxdetails";
            this.tlb_panelboxdetails.Padding = new System.Windows.Forms.Padding(5);
            this.tlb_panelboxdetails.RowCount = 1;
            this.tlb_panelboxdetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlb_panelboxdetails.Size = new System.Drawing.Size(794, 83);
            this.tlb_panelboxdetails.TabIndex = 2;
            // 
            // cpbsaletop
            // 
            this.cpbsaletop.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.cpbsaletop.AnimationSpeed = 500;
            this.cpbsaletop.BackColor = System.Drawing.Color.Transparent;
            this.cpbsaletop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cpbsaletop.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold);
            this.cpbsaletop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.cpbsaletop.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cpbsaletop.InnerMargin = 2;
            this.cpbsaletop.InnerWidth = -1;
            this.cpbsaletop.Location = new System.Drawing.Point(530, 8);
            this.cpbsaletop.MarqueeAnimationSpeed = 2000;
            this.cpbsaletop.Name = "cpbsaletop";
            this.cpbsaletop.OuterColor = System.Drawing.Color.White;
            this.cpbsaletop.OuterMargin = -25;
            this.cpbsaletop.OuterWidth = 26;
            this.cpbsaletop.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.cpbsaletop.ProgressWidth = 25;
            this.cpbsaletop.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 36F);
            this.cpbsaletop.Size = new System.Drawing.Size(256, 67);
            this.cpbsaletop.StartAngle = 270;
            this.cpbsaletop.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.cpbsaletop.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.cpbsaletop.SubscriptText = ".23";
            this.cpbsaletop.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.cpbsaletop.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.cpbsaletop.SuperscriptText = "°C";
            this.cpbsaletop.TabIndex = 0;
            this.cpbsaletop.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.cpbsaletop.Value = 68;
            // 
            // pnl_cur_date_sales
            // 
            this.pnl_cur_date_sales.BackColor = System.Drawing.Color.Lime;
            this.pnl_cur_date_sales.Controls.Add(this.tlp_cur_total);
            this.pnl_cur_date_sales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_cur_date_sales.Location = new System.Drawing.Point(269, 8);
            this.pnl_cur_date_sales.Name = "pnl_cur_date_sales";
            this.pnl_cur_date_sales.Size = new System.Drawing.Size(255, 67);
            this.pnl_cur_date_sales.TabIndex = 1;
            // 
            // tlp_cur_total
            // 
            this.tlp_cur_total.ColumnCount = 1;
            this.tlp_cur_total.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_cur_total.Controls.Add(this.label5, 0, 0);
            this.tlp_cur_total.Controls.Add(this.tableLayoutPanel6, 0, 1);
            this.tlp_cur_total.Controls.Add(this.lbldinetotalno, 0, 2);
            this.tlp_cur_total.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_cur_total.Location = new System.Drawing.Point(0, 0);
            this.tlp_cur_total.Name = "tlp_cur_total";
            this.tlp_cur_total.RowCount = 3;
            this.tlp_cur_total.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_cur_total.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_cur_total.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_cur_total.Size = new System.Drawing.Size(255, 67);
            this.tlp_cur_total.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(249, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Dine In";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.ColumnCount = 2;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10.40462F));
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 89.59538F));
            this.tableLayoutPanel6.Controls.Add(this.pictureBox3, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.lbldinetotal, 1, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 1;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(249, 24);
            this.tableLayoutPanel6.TabIndex = 1;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox3.BackgroundImage")));
            this.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox3.Location = new System.Drawing.Point(3, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(19, 18);
            this.pictureBox3.TabIndex = 0;
            this.pictureBox3.TabStop = false;
            // 
            // lbldinetotal
            // 
            this.lbldinetotal.AutoSize = true;
            this.lbldinetotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbldinetotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldinetotal.Location = new System.Drawing.Point(28, 0);
            this.lbldinetotal.Name = "lbldinetotal";
            this.lbldinetotal.Size = new System.Drawing.Size(218, 24);
            this.lbldinetotal.TabIndex = 1;
            this.lbldinetotal.Text = "label6";
            this.lbldinetotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbldinetotalno
            // 
            this.lbldinetotalno.AutoSize = true;
            this.lbldinetotalno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbldinetotalno.Location = new System.Drawing.Point(3, 50);
            this.lbldinetotalno.Name = "lbldinetotalno";
            this.lbldinetotalno.Size = new System.Drawing.Size(249, 17);
            this.lbldinetotalno.TabIndex = 2;
            this.lbldinetotalno.Text = "label7";
            // 
            // pnltotalsales
            // 
            this.pnltotalsales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.pnltotalsales.Controls.Add(this.tlp_saletotal);
            this.pnltotalsales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnltotalsales.Location = new System.Drawing.Point(8, 8);
            this.pnltotalsales.Name = "pnltotalsales";
            this.pnltotalsales.Size = new System.Drawing.Size(255, 67);
            this.pnltotalsales.TabIndex = 0;
            // 
            // tlp_saletotal
            // 
            this.tlp_saletotal.ColumnCount = 1;
            this.tlp_saletotal.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_saletotal.Controls.Add(this.label4, 0, 0);
            this.tlp_saletotal.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tlp_saletotal.Controls.Add(this.lbltotalsaleno, 0, 2);
            this.tlp_saletotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_saletotal.Location = new System.Drawing.Point(0, 0);
            this.tlp_saletotal.Name = "tlp_saletotal";
            this.tlp_saletotal.RowCount = 3;
            this.tlp_saletotal.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_saletotal.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_saletotal.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_saletotal.Size = new System.Drawing.Size(255, 67);
            this.tlp_saletotal.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(249, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Total Sales";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.71676F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.28323F));
            this.tableLayoutPanel4.Controls.Add(this.pictureBox2, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.lblsaletotal, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 23);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(249, 24);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox2.BackgroundImage")));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(25, 18);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // lblsaletotal
            // 
            this.lblsaletotal.AutoSize = true;
            this.lblsaletotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblsaletotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsaletotal.Location = new System.Drawing.Point(34, 0);
            this.lblsaletotal.Name = "lblsaletotal";
            this.lblsaletotal.Size = new System.Drawing.Size(212, 24);
            this.lblsaletotal.TabIndex = 1;
            this.lblsaletotal.Text = "label7";
            this.lblsaletotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbltotalsaleno
            // 
            this.lbltotalsaleno.AutoSize = true;
            this.lbltotalsaleno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbltotalsaleno.Location = new System.Drawing.Point(3, 50);
            this.lbltotalsaleno.Name = "lbltotalsaleno";
            this.lbltotalsaleno.Size = new System.Drawing.Size(249, 17);
            this.lbltotalsaleno.TabIndex = 2;
            this.lbltotalsaleno.Text = "label6";
            // 
            // chart1
            // 
            chartArea3.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(3, 110);
            this.chart1.Name = "chart1";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(520, 300);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            // 
            // FrmDashboardReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnl_dashboard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDashboardReport";
            this.Text = "FrmDashboardReport";
            this.Load += new System.EventHandler(this.FrmDashboardReport_Load);
            this.pnl_dashboard.ResumeLayout(false);
            this.tlp_main_dashboard.ResumeLayout(false);
            this.tlp_date_dashboard.ResumeLayout(false);
            this.tlb_panelboxdetails.ResumeLayout(false);
            this.pnl_cur_date_sales.ResumeLayout(false);
            this.tlp_cur_total.ResumeLayout(false);
            this.tlp_cur_total.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.pnltotalsales.ResumeLayout(false);
            this.tlp_saletotal.ResumeLayout(false);
            this.tlp_saletotal.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_dashboard;
        private System.Windows.Forms.TableLayoutPanel tlp_main_dashboard;
        private System.Windows.Forms.TableLayoutPanel tlp_date_dashboard;
        private System.Windows.Forms.DateTimePicker dtp_dashboard;
        private System.Windows.Forms.TableLayoutPanel tlb_panelboxdetails;
        private CircularProgressBar.CircularProgressBar cpbsaletop;
        private System.Windows.Forms.Panel pnl_cur_date_sales;
        private System.Windows.Forms.TableLayoutPanel tlp_cur_total;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lbldinetotal;
        private System.Windows.Forms.Label lbldinetotalno;
        private System.Windows.Forms.Panel pnltotalsales;
        private System.Windows.Forms.TableLayoutPanel tlp_saletotal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblsaletotal;
        private System.Windows.Forms.Label lbltotalsaleno;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}