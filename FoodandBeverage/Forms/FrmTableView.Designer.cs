
namespace FoodandBeverage.Forms
{
    partial class FrmTableView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTableView));
            this.tlp_tablestatus = new System.Windows.Forms.TableLayoutPanel();
            this.btntrefresh = new System.Windows.Forms.Button();
            this.btn_avial = new System.Windows.Forms.Button();
            this.btn_rev = new System.Windows.Forms.Button();
            this.btn_running = new System.Windows.Forms.Button();
            this.tlp_tablestatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_tablestatus
            // 
            this.tlp_tablestatus.ColumnCount = 4;
            this.tlp_tablestatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tlp_tablestatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.24028F));
            this.tlp_tablestatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.57895F));
            this.tlp_tablestatus.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 280F));
            this.tlp_tablestatus.Controls.Add(this.btntrefresh, 0, 0);
            this.tlp_tablestatus.Controls.Add(this.btn_avial, 1, 0);
            this.tlp_tablestatus.Controls.Add(this.btn_rev, 3, 0);
            this.tlp_tablestatus.Controls.Add(this.btn_running, 2, 0);
            this.tlp_tablestatus.Location = new System.Drawing.Point(139, 76);
            this.tlp_tablestatus.Name = "tlp_tablestatus";
            this.tlp_tablestatus.RowCount = 1;
            this.tlp_tablestatus.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_tablestatus.Size = new System.Drawing.Size(437, 51);
            this.tlp_tablestatus.TabIndex = 1;
            this.tlp_tablestatus.Visible = false;
            // 
            // btntrefresh
            // 
            this.btntrefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btntrefresh.BackgroundImage")));
            this.btntrefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btntrefresh.FlatAppearance.BorderSize = 0;
            this.btntrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btntrefresh.Location = new System.Drawing.Point(3, 3);
            this.btntrefresh.Name = "btntrefresh";
            this.btntrefresh.Size = new System.Drawing.Size(46, 42);
            this.btntrefresh.TabIndex = 3;
            this.btntrefresh.UseVisualStyleBackColor = true;
            this.btntrefresh.Click += new System.EventHandler(this.btntrefresh_Click);
            // 
            // btn_avial
            // 
            this.btn_avial.BackgroundImage = global::FoodandBeverage.Properties.Resources.available;
            this.btn_avial.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_avial.FlatAppearance.BorderSize = 0;
            this.btn_avial.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_avial.Location = new System.Drawing.Point(55, 3);
            this.btn_avial.Name = "btn_avial";
            this.btn_avial.Size = new System.Drawing.Size(49, 42);
            this.btn_avial.TabIndex = 0;
            this.btn_avial.UseVisualStyleBackColor = true;
            this.btn_avial.Click += new System.EventHandler(this.btn_avial_Click);
            // 
            // btn_rev
            // 
            this.btn_rev.BackgroundImage = global::FoodandBeverage.Properties.Resources.reserve;
            this.btn_rev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_rev.FlatAppearance.BorderSize = 0;
            this.btn_rev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_rev.Location = new System.Drawing.Point(159, 3);
            this.btn_rev.Name = "btn_rev";
            this.btn_rev.Size = new System.Drawing.Size(52, 42);
            this.btn_rev.TabIndex = 1;
            this.btn_rev.UseVisualStyleBackColor = true;
            this.btn_rev.Click += new System.EventHandler(this.btn_rev_Click);
            // 
            // btn_running
            // 
            this.btn_running.BackgroundImage = global::FoodandBeverage.Properties.Resources.time;
            this.btn_running.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_running.FlatAppearance.BorderSize = 0;
            this.btn_running.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_running.Location = new System.Drawing.Point(110, 3);
            this.btn_running.Name = "btn_running";
            this.btn_running.Size = new System.Drawing.Size(43, 42);
            this.btn_running.TabIndex = 2;
            this.btn_running.UseVisualStyleBackColor = true;
            this.btn_running.Click += new System.EventHandler(this.btn_running_Click);
            // 
            // FrmTableView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 526);
            this.Controls.Add(this.tlp_tablestatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTableView";
            this.Text = "FrmTableView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmTableView_Load);
            this.tlp_tablestatus.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_tablestatus;
        private System.Windows.Forms.Button btntrefresh;
        private System.Windows.Forms.Button btn_avial;
        private System.Windows.Forms.Button btn_rev;
        private System.Windows.Forms.Button btn_running;
    }
}