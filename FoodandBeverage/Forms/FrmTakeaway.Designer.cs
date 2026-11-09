
namespace FoodandBeverage.Forms
{
    partial class FrmTakeaway
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTakeaway));
            this.AutoTimer = new System.Windows.Forms.Timer(this.components);
            this.pbempty = new System.Windows.Forms.PictureBox();
            this.tlp_search = new System.Windows.Forms.TableLayoutPanel();
            this.btnaddnew = new System.Windows.Forms.Button();
            this.txtPkid_search = new System.Windows.Forms.TextBox();
            this.btnsearch = new System.Windows.Forms.Button();
            this.btn_cancle_pickup = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.lblcanpickupcount = new System.Windows.Forms.Label();
            this.btn_backCompleorder = new System.Windows.Forms.Button();
            this.btnclose = new System.Windows.Forms.Button();
            this.btnnext = new System.Windows.Forms.Button();
            this.lblmemory = new System.Windows.Forms.Label();
            this.btnprev = new System.Windows.Forms.Button();
            this.tlp_footer = new System.Windows.Forms.TableLayoutPanel();
            this.lblpagerinfo = new System.Windows.Forms.Label();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.flp_pickup_view = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pbempty)).BeginInit();
            this.tlp_search.SuspendLayout();
            this.tlp_footer.SuspendLayout();
            this.tlp_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // AutoTimer
            // 
            this.AutoTimer.Interval = 2000;
            // 
            // pbempty
            // 
            this.pbempty.BackgroundImage = global::FoodandBeverage.Properties.Resources.empty_orders;
            this.pbempty.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbempty.Location = new System.Drawing.Point(42, 74);
            this.pbempty.Name = "pbempty";
            this.pbempty.Size = new System.Drawing.Size(65, 57);
            this.pbempty.TabIndex = 2;
            this.pbempty.TabStop = false;
            // 
            // tlp_search
            // 
            this.tlp_search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_search.ColumnCount = 9;
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.Controls.Add(this.btnaddnew, 5, 0);
            this.tlp_search.Controls.Add(this.txtPkid_search, 0, 0);
            this.tlp_search.Controls.Add(this.btnsearch, 1, 0);
            this.tlp_search.Controls.Add(this.btn_cancle_pickup, 3, 0);
            this.tlp_search.Controls.Add(this.btn_refresh, 2, 0);
            this.tlp_search.Controls.Add(this.lblcanpickupcount, 4, 0);
            this.tlp_search.Controls.Add(this.btn_backCompleorder, 7, 0);
            this.tlp_search.Controls.Add(this.btnclose, 8, 0);
            this.tlp_search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_search.Location = new System.Drawing.Point(3, 3);
            this.tlp_search.Name = "tlp_search";
            this.tlp_search.RowCount = 1;
            this.tlp_search.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_search.Size = new System.Drawing.Size(940, 49);
            this.tlp_search.TabIndex = 0;
            // 
            // btnaddnew
            // 
            this.btnaddnew.BackColor = System.Drawing.Color.Transparent;
            this.btnaddnew.BackgroundImage = global::FoodandBeverage.Properties.Resources.ADD;
            this.btnaddnew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnaddnew.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnaddnew.FlatAppearance.BorderSize = 0;
            this.btnaddnew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaddnew.Location = new System.Drawing.Point(590, 3);
            this.btnaddnew.Name = "btnaddnew";
            this.btnaddnew.Size = new System.Drawing.Size(56, 43);
            this.btnaddnew.TabIndex = 1;
            this.btnaddnew.UseVisualStyleBackColor = false;
            this.btnaddnew.Click += new System.EventHandler(this.btnaddnew_Click);
            // 
            // txtPkid_search
            // 
            this.txtPkid_search.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            // btnsearch
            // 
            this.btnsearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnsearch.BackgroundImage")));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnsearch.FlatAppearance.BorderSize = 0;
            this.btnsearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsearch.Location = new System.Drawing.Point(346, 3);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(50, 42);
            this.btnsearch.TabIndex = 1;
            this.btnsearch.UseVisualStyleBackColor = true;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // btn_cancle_pickup
            // 
            this.btn_cancle_pickup.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_orderpage_cancelorder;
            this.btn_cancle_pickup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_cancle_pickup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_cancle_pickup.FlatAppearance.BorderSize = 0;
            this.btn_cancle_pickup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancle_pickup.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancle_pickup.ForeColor = System.Drawing.Color.White;
            this.btn_cancle_pickup.Location = new System.Drawing.Point(458, 3);
            this.btn_cancle_pickup.Name = "btn_cancle_pickup";
            this.btn_cancle_pickup.Size = new System.Drawing.Size(101, 43);
            this.btn_cancle_pickup.TabIndex = 292;
            this.btn_cancle_pickup.UseVisualStyleBackColor = true;
            this.btn_cancle_pickup.Visible = false;
            this.btn_cancle_pickup.Click += new System.EventHandler(this.btn_cancle_pickup_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_refresh.BackgroundImage")));
            this.btn_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_refresh.FlatAppearance.BorderSize = 0;
            this.btn_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_refresh.Location = new System.Drawing.Point(402, 3);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(50, 42);
            this.btn_refresh.TabIndex = 2;
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // lblcanpickupcount
            // 
            this.lblcanpickupcount.AutoSize = true;
            this.lblcanpickupcount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblcanpickupcount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblcanpickupcount.Location = new System.Drawing.Point(565, 0);
            this.lblcanpickupcount.Name = "lblcanpickupcount";
            this.lblcanpickupcount.Size = new System.Drawing.Size(19, 49);
            this.lblcanpickupcount.TabIndex = 293;
            this.lblcanpickupcount.Text = "0";
            this.lblcanpickupcount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblcanpickupcount.Visible = false;
            // 
            // btn_backCompleorder
            // 
            this.btn_backCompleorder.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_orderpage_nokot_printbill;
            this.btn_backCompleorder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_backCompleorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_backCompleorder.FlatAppearance.BorderSize = 0;
            this.btn_backCompleorder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_backCompleorder.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_backCompleorder.ForeColor = System.Drawing.Color.White;
            this.btn_backCompleorder.Location = new System.Drawing.Point(706, 3);
            this.btn_backCompleorder.Name = "btn_backCompleorder";
            this.btn_backCompleorder.Size = new System.Drawing.Size(115, 43);
            this.btn_backCompleorder.TabIndex = 4;
            this.btn_backCompleorder.Text = "Bill Page";
            this.btn_backCompleorder.UseVisualStyleBackColor = true;
            this.btn_backCompleorder.Click += new System.EventHandler(this.btn_backCompleorder_Click);
            // 
            // btnclose
            // 
            this.btnclose.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_dashboard_button;
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnclose.FlatAppearance.BorderSize = 0;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(827, 3);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(110, 43);
            this.btnclose.TabIndex = 293;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btnnext
            // 
            this.btnnext.BackgroundImage = global::FoodandBeverage.Properties.Resources.categ_next;
            this.btnnext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnnext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnext.FlatAppearance.BorderSize = 0;
            this.btnnext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnnext.Location = new System.Drawing.Point(877, 3);
            this.btnnext.Name = "btnnext";
            this.btnnext.Size = new System.Drawing.Size(60, 37);
            this.btnnext.TabIndex = 291;
            this.btnnext.UseVisualStyleBackColor = true;
            this.btnnext.Click += new System.EventHandler(this.btnnext_Click);
            // 
            // lblmemory
            // 
            this.lblmemory.AutoSize = true;
            this.lblmemory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblmemory.Font = new System.Drawing.Font("Microsoft Tai Le", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmemory.Location = new System.Drawing.Point(69, 0);
            this.lblmemory.Name = "lblmemory";
            this.lblmemory.Size = new System.Drawing.Size(268, 43);
            this.lblmemory.TabIndex = 292;
            this.lblmemory.Text = "Memory";
            this.lblmemory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnprev
            // 
            this.btnprev.BackgroundImage = global::FoodandBeverage.Properties.Resources.categ_back;
            this.btnprev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnprev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnprev.FlatAppearance.BorderSize = 0;
            this.btnprev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnprev.Location = new System.Drawing.Point(3, 3);
            this.btnprev.Name = "btnprev";
            this.btnprev.Size = new System.Drawing.Size(60, 37);
            this.btnprev.TabIndex = 0;
            this.btnprev.UseVisualStyleBackColor = true;
            this.btnprev.Click += new System.EventHandler(this.btnprev_Click);
            // 
            // tlp_footer
            // 
            this.tlp_footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_footer.ColumnCount = 4;
            this.tlp_footer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_footer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 274F));
            this.tlp_footer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_footer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_footer.Controls.Add(this.btnprev, 0, 0);
            this.tlp_footer.Controls.Add(this.lblpagerinfo, 2, 0);
            this.tlp_footer.Controls.Add(this.btnnext, 3, 0);
            this.tlp_footer.Controls.Add(this.lblmemory, 1, 0);
            this.tlp_footer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_footer.Location = new System.Drawing.Point(3, 402);
            this.tlp_footer.Name = "tlp_footer";
            this.tlp_footer.RowCount = 1;
            this.tlp_footer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_footer.Size = new System.Drawing.Size(940, 43);
            this.tlp_footer.TabIndex = 25;
            // 
            // lblpagerinfo
            // 
            this.lblpagerinfo.AutoSize = true;
            this.lblpagerinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblpagerinfo.Location = new System.Drawing.Point(343, 0);
            this.lblpagerinfo.Name = "lblpagerinfo";
            this.lblpagerinfo.Size = new System.Drawing.Size(528, 43);
            this.lblpagerinfo.TabIndex = 0;
            this.lblpagerinfo.Text = "Midasfun";
            this.lblpagerinfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlp_main
            // 
            this.tlp_main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tlp_footer, 0, 2);
            this.tlp_main.Controls.Add(this.tlp_search, 0, 0);
            this.tlp_main.Controls.Add(this.flp_pickup_view, 0, 1);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 3;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.Size = new System.Drawing.Size(946, 448);
            this.tlp_main.TabIndex = 3;
            // 
            // flp_pickup_view
            // 
            this.flp_pickup_view.AutoScroll = true;
            this.flp_pickup_view.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.flp_pickup_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_pickup_view.Location = new System.Drawing.Point(3, 58);
            this.flp_pickup_view.Name = "flp_pickup_view";
            this.flp_pickup_view.Padding = new System.Windows.Forms.Padding(5);
            this.flp_pickup_view.Size = new System.Drawing.Size(940, 338);
            this.flp_pickup_view.TabIndex = 1;
            // 
            // FrmTakeaway
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 448);
            this.Controls.Add(this.pbempty);
            this.Controls.Add(this.tlp_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTakeaway";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Text = "FrmTakeaway";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTakeaway_FormClosing);
            this.Load += new System.EventHandler(this.FrmTakeaway_Load);
            this.Resize += new System.EventHandler(this.FrmTakeaway_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbempty)).EndInit();
            this.tlp_search.ResumeLayout(false);
            this.tlp_search.PerformLayout();
            this.tlp_footer.ResumeLayout(false);
            this.tlp_footer.PerformLayout();
            this.tlp_main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Timer AutoTimer;
        private System.Windows.Forms.PictureBox pbempty;
        private System.Windows.Forms.TableLayoutPanel tlp_search;
        private System.Windows.Forms.Button btnaddnew;
        private System.Windows.Forms.TextBox txtPkid_search;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btn_cancle_pickup;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.Label lblcanpickupcount;
        private System.Windows.Forms.Button btn_backCompleorder;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Button btnnext;
        private System.Windows.Forms.Label lblmemory;
        private System.Windows.Forms.Button btnprev;
        private System.Windows.Forms.TableLayoutPanel tlp_footer;
        private System.Windows.Forms.Label lblpagerinfo;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.FlowLayoutPanel flp_pickup_view;
    }
}