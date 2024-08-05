
namespace FoodandBeverage.Forms
{
    partial class FrmPickup
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPickup));
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_search = new System.Windows.Forms.TableLayoutPanel();
            this.txtPkid_search = new System.Windows.Forms.TextBox();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.btnsearch = new System.Windows.Forms.Button();
            this.flp_pickup_view = new System.Windows.Forms.FlowLayoutPanel();
            this.tlp_addnew = new System.Windows.Forms.TableLayoutPanel();
            this.btnaddnew = new System.Windows.Forms.Button();
            this.lbladdnew = new System.Windows.Forms.Label();
            this.AutoTimer = new System.Windows.Forms.Timer(this.components);
            this.tlp_main.SuspendLayout();
            this.tlp_search.SuspendLayout();
            this.flp_pickup_view.SuspendLayout();
            this.tlp_addnew.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_main
            // 
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tlp_search, 0, 1);
            this.tlp_main.Controls.Add(this.flp_pickup_view, 0, 2);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 3;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.Size = new System.Drawing.Size(597, 438);
            this.tlp_main.TabIndex = 1;
            // 
            // tlp_search
            // 
            this.tlp_search.ColumnCount = 4;
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 136F));
            this.tlp_search.Controls.Add(this.txtPkid_search, 0, 0);
            this.tlp_search.Controls.Add(this.btn_refresh, 2, 0);
            this.tlp_search.Controls.Add(this.btnsearch, 1, 0);
            this.tlp_search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_search.Location = new System.Drawing.Point(3, 13);
            this.tlp_search.Name = "tlp_search";
            this.tlp_search.RowCount = 1;
            this.tlp_search.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_search.Size = new System.Drawing.Size(591, 49);
            this.tlp_search.TabIndex = 0;
            // 
            // txtPkid_search
            // 
            this.txtPkid_search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPkid_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPkid_search.ForeColor = System.Drawing.Color.DarkGray;
            this.txtPkid_search.Location = new System.Drawing.Point(3, 3);
            this.txtPkid_search.Multiline = true;
            this.txtPkid_search.Name = "txtPkid_search";
            this.txtPkid_search.Size = new System.Drawing.Size(337, 43);
            this.txtPkid_search.TabIndex = 0;
            this.txtPkid_search.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPkid_search.Enter += new System.EventHandler(this.txtPkid_search_Enter);
            this.txtPkid_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPkid_search_KeyDown);
            this.txtPkid_search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPkid_search_KeyPress);
            this.txtPkid_search.Leave += new System.EventHandler(this.txtPkid_search_Leave);
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_refresh.BackgroundImage")));
            this.btn_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_refresh.FlatAppearance.BorderSize = 0;
            this.btn_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_refresh.Location = new System.Drawing.Point(402, 3);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(50, 43);
            this.btn_refresh.TabIndex = 2;
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // btnsearch
            // 
            this.btnsearch.BackgroundImage = global::FoodandBeverage.Properties.Resources.search;
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnsearch.FlatAppearance.BorderSize = 0;
            this.btnsearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsearch.Location = new System.Drawing.Point(346, 3);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(50, 43);
            this.btnsearch.TabIndex = 1;
            this.btnsearch.UseVisualStyleBackColor = true;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // flp_pickup_view
            // 
            this.flp_pickup_view.AutoScroll = true;
            this.flp_pickup_view.BackColor = System.Drawing.Color.White;
            this.flp_pickup_view.Controls.Add(this.tlp_addnew);
            this.flp_pickup_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_pickup_view.Location = new System.Drawing.Point(3, 68);
            this.flp_pickup_view.Name = "flp_pickup_view";
            this.flp_pickup_view.Padding = new System.Windows.Forms.Padding(5);
            this.flp_pickup_view.Size = new System.Drawing.Size(591, 367);
            this.flp_pickup_view.TabIndex = 1;
            // 
            // tlp_addnew
            // 
            this.tlp_addnew.ColumnCount = 1;
            this.tlp_addnew.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_addnew.Controls.Add(this.btnaddnew, 0, 0);
            this.tlp_addnew.Controls.Add(this.lbladdnew, 0, 1);
            this.tlp_addnew.Location = new System.Drawing.Point(20, 15);
            this.tlp_addnew.Margin = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.tlp_addnew.Name = "tlp_addnew";
            this.tlp_addnew.RowCount = 2;
            this.tlp_addnew.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_addnew.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
            this.tlp_addnew.Size = new System.Drawing.Size(115, 112);
            this.tlp_addnew.TabIndex = 0;
            this.tlp_addnew.Click += new System.EventHandler(this.btnaddnew_Click);
            // 
            // btnaddnew
            // 
            this.btnaddnew.BackColor = System.Drawing.Color.Transparent;
            this.btnaddnew.BackgroundImage = global::FoodandBeverage.Properties.Resources.plus;
            this.btnaddnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnaddnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnaddnew.FlatAppearance.BorderSize = 0;
            this.btnaddnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaddnew.Location = new System.Drawing.Point(3, 3);
            this.btnaddnew.Name = "btnaddnew";
            this.btnaddnew.Size = new System.Drawing.Size(109, 69);
            this.btnaddnew.TabIndex = 1;
            this.btnaddnew.UseVisualStyleBackColor = false;
            this.btnaddnew.Click += new System.EventHandler(this.btnaddnew_Click);
            // 
            // lbladdnew
            // 
            this.lbladdnew.AutoSize = true;
            this.lbladdnew.BackColor = System.Drawing.Color.Transparent;
            this.lbladdnew.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbladdnew.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbladdnew.Location = new System.Drawing.Point(3, 75);
            this.lbladdnew.Name = "lbladdnew";
            this.lbladdnew.Size = new System.Drawing.Size(109, 37);
            this.lbladdnew.TabIndex = 1;
            this.lbladdnew.Text = "ADD NEW";
            this.lbladdnew.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbladdnew.Click += new System.EventHandler(this.btnaddnew_Click);
            // 
            // AutoTimer
            // 
            this.AutoTimer.Interval = 2000;
            this.AutoTimer.Tick += new System.EventHandler(this.AutoTimer_Tick);
            // 
            // FrmPickup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 438);
            this.Controls.Add(this.tlp_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmPickup";
            this.Text = "FrmPickup";
            this.Load += new System.EventHandler(this.FrmPickup_Load);
            this.Resize += new System.EventHandler(this.FrmPickup_Resize);
            this.tlp_main.ResumeLayout(false);
            this.tlp_search.ResumeLayout(false);
            this.tlp_search.PerformLayout();
            this.flp_pickup_view.ResumeLayout(false);
            this.tlp_addnew.ResumeLayout(false);
            this.tlp_addnew.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TableLayoutPanel tlp_search;
        private System.Windows.Forms.TextBox txtPkid_search;
        public System.Windows.Forms.Timer AutoTimer;
        private System.Windows.Forms.FlowLayoutPanel flp_pickup_view;
        private System.Windows.Forms.TableLayoutPanel tlp_addnew;
        private System.Windows.Forms.Button btnaddnew;
        private System.Windows.Forms.Label lbladdnew;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btn_refresh;
    }
}