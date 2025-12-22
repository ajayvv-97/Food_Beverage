
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTableView));
            this.AutoTimer = new System.Windows.Forms.Timer(this.components);
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_footer = new System.Windows.Forms.TableLayoutPanel();
            this.lblmemory = new System.Windows.Forms.Label();
            this.btnprev = new System.Windows.Forms.Button();
            this.lblpagerinfo = new System.Windows.Forms.Label();
            this.btnnext = new System.Windows.Forms.Button();
            this.tlp_search = new System.Windows.Forms.TableLayoutPanel();
            this.btnclose = new System.Windows.Forms.Button();
            this.btn_showmenu = new FontAwesome.Sharp.IconButton();
            this.txttablecode_search = new System.Windows.Forms.TextBox();
            this.btnsearch = new System.Windows.Forms.Button();
            this.btn_cancle_pickup = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.flp_tableview = new System.Windows.Forms.FlowLayoutPanel();
            this.flp_sidemenu = new System.Windows.Forms.FlowLayoutPanel();
            this.tlp_main.SuspendLayout();
            this.tlp_footer.SuspendLayout();
            this.tlp_search.SuspendLayout();
            this.SuspendLayout();
            // 
            // AutoTimer
            // 
            this.AutoTimer.Interval = 2000;
            this.AutoTimer.Tick += new System.EventHandler(this.AutoTimer_Tick);
            // 
            // tlp_main
            // 
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tlp_footer, 0, 3);
            this.tlp_main.Controls.Add(this.tlp_search, 0, 1);
            this.tlp_main.Controls.Add(this.flp_tableview, 0, 2);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Margin = new System.Windows.Forms.Padding(2);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 4;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.891809F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.10819F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.tlp_main.Size = new System.Drawing.Size(714, 526);
            this.tlp_main.TabIndex = 0;
            // 
            // tlp_footer
            // 
            this.tlp_footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_footer.ColumnCount = 4;
            this.tlp_footer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_footer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tlp_footer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_footer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_footer.Controls.Add(this.lblmemory, 0, 0);
            this.tlp_footer.Controls.Add(this.btnprev, 0, 0);
            this.tlp_footer.Controls.Add(this.lblpagerinfo, 2, 0);
            this.tlp_footer.Controls.Add(this.btnnext, 3, 0);
            this.tlp_footer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_footer.Location = new System.Drawing.Point(3, 484);
            this.tlp_footer.Name = "tlp_footer";
            this.tlp_footer.RowCount = 1;
            this.tlp_footer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_footer.Size = new System.Drawing.Size(708, 39);
            this.tlp_footer.TabIndex = 24;
            // 
            // lblmemory
            // 
            this.lblmemory.AutoSize = true;
            this.lblmemory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblmemory.Location = new System.Drawing.Point(69, 0);
            this.lblmemory.Name = "lblmemory";
            this.lblmemory.Size = new System.Drawing.Size(78, 39);
            this.lblmemory.TabIndex = 293;
            this.lblmemory.Text = "Memeory";
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
            this.btnprev.Size = new System.Drawing.Size(60, 33);
            this.btnprev.TabIndex = 0;
            this.btnprev.UseVisualStyleBackColor = true;
            this.btnprev.Click += new System.EventHandler(this.btnprev_Click);
            // 
            // lblpagerinfo
            // 
            this.lblpagerinfo.AutoSize = true;
            this.lblpagerinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblpagerinfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpagerinfo.Location = new System.Drawing.Point(153, 0);
            this.lblpagerinfo.Name = "lblpagerinfo";
            this.lblpagerinfo.Size = new System.Drawing.Size(486, 39);
            this.lblpagerinfo.TabIndex = 0;
            this.lblpagerinfo.Text = "Midasfun";
            this.lblpagerinfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnnext
            // 
            this.btnnext.BackgroundImage = global::FoodandBeverage.Properties.Resources.categ_next;
            this.btnnext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnnext.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnnext.FlatAppearance.BorderSize = 0;
            this.btnnext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnnext.Location = new System.Drawing.Point(645, 3);
            this.btnnext.Name = "btnnext";
            this.btnnext.Size = new System.Drawing.Size(60, 33);
            this.btnnext.TabIndex = 291;
            this.btnnext.UseVisualStyleBackColor = true;
            this.btnnext.Click += new System.EventHandler(this.btnnext_Click);
            // 
            // tlp_search
            // 
            this.tlp_search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_search.ColumnCount = 7;
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_search.Controls.Add(this.btnclose, 6, 0);
            this.tlp_search.Controls.Add(this.btn_showmenu, 0, 0);
            this.tlp_search.Controls.Add(this.txttablecode_search, 2, 0);
            this.tlp_search.Controls.Add(this.btnsearch, 3, 0);
            this.tlp_search.Controls.Add(this.btn_cancle_pickup, 5, 0);
            this.tlp_search.Controls.Add(this.btn_refresh, 4, 0);
            this.tlp_search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_search.Location = new System.Drawing.Point(3, 19);
            this.tlp_search.Name = "tlp_search";
            this.tlp_search.RowCount = 1;
            this.tlp_search.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_search.Size = new System.Drawing.Size(708, 40);
            this.tlp_search.TabIndex = 1;
            // 
            // btnclose
            // 
            this.btnclose.BackgroundImage = global::FoodandBeverage.Properties.Resources.Closebutton;
            this.btnclose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnclose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnclose.FlatAppearance.BorderSize = 0;
            this.btnclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnclose.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnclose.ForeColor = System.Drawing.Color.White;
            this.btnclose.Location = new System.Drawing.Point(595, 3);
            this.btnclose.Name = "btnclose";
            this.btnclose.Size = new System.Drawing.Size(110, 34);
            this.btnclose.TabIndex = 294;
            this.btnclose.Text = "Close";
            this.btnclose.UseVisualStyleBackColor = true;
            this.btnclose.Click += new System.EventHandler(this.btnclose_Click);
            // 
            // btn_showmenu
            // 
            this.btn_showmenu.BackColor = System.Drawing.Color.Transparent;
            this.btn_showmenu.BackgroundImage = global::FoodandBeverage.Properties.Resources.fb_sidemenu;
            this.btn_showmenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_showmenu.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btn_showmenu.FlatAppearance.BorderSize = 0;
            this.btn_showmenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_showmenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_showmenu.ForeColor = System.Drawing.Color.White;
            this.btn_showmenu.IconChar = FontAwesome.Sharp.IconChar.Microsoft;
            this.btn_showmenu.IconColor = System.Drawing.Color.White;
            this.btn_showmenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_showmenu.IconSize = 30;
            this.btn_showmenu.Location = new System.Drawing.Point(0, 0);
            this.btn_showmenu.Margin = new System.Windows.Forms.Padding(0);
            this.btn_showmenu.Name = "btn_showmenu";
            this.btn_showmenu.Size = new System.Drawing.Size(276, 39);
            this.btn_showmenu.TabIndex = 21;
            this.btn_showmenu.Tag = "Table Category";
            this.btn_showmenu.Text = "Table Category";
            this.btn_showmenu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_showmenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_showmenu.UseVisualStyleBackColor = false;
            this.btn_showmenu.Click += new System.EventHandler(this.btn_showmenu_Click);
            // 
            // txttablecode_search
            // 
            this.txttablecode_search.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txttablecode_search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txttablecode_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttablecode_search.ForeColor = System.Drawing.Color.DarkGray;
            this.txttablecode_search.Location = new System.Drawing.Point(112, 3);
            this.txttablecode_search.Name = "txttablecode_search";
            this.txttablecode_search.Size = new System.Drawing.Size(258, 33);
            this.txttablecode_search.TabIndex = 0;
            this.txttablecode_search.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnsearch
            // 
            this.btnsearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnsearch.BackgroundImage")));
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnsearch.FlatAppearance.BorderSize = 0;
            this.btnsearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsearch.Location = new System.Drawing.Point(376, 3);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(50, 32);
            this.btnsearch.TabIndex = 1;
            this.btnsearch.UseVisualStyleBackColor = true;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // btn_cancle_pickup
            // 
            this.btn_cancle_pickup.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_orderpage_cancelorder;
            this.btn_cancle_pickup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_cancle_pickup.FlatAppearance.BorderSize = 0;
            this.btn_cancle_pickup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancle_pickup.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancle_pickup.ForeColor = System.Drawing.Color.White;
            this.btn_cancle_pickup.Location = new System.Drawing.Point(488, 3);
            this.btn_cancle_pickup.Name = "btn_cancle_pickup";
            this.btn_cancle_pickup.Size = new System.Drawing.Size(101, 32);
            this.btn_cancle_pickup.TabIndex = 292;
            this.btn_cancle_pickup.UseVisualStyleBackColor = true;
            this.btn_cancle_pickup.Visible = false;
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_refresh.BackgroundImage")));
            this.btn_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_refresh.FlatAppearance.BorderSize = 0;
            this.btn_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_refresh.Location = new System.Drawing.Point(432, 3);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(50, 32);
            this.btn_refresh.TabIndex = 2;
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // flp_tableview
            // 
            this.flp_tableview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_tableview.Location = new System.Drawing.Point(2, 64);
            this.flp_tableview.Margin = new System.Windows.Forms.Padding(2);
            this.flp_tableview.Name = "flp_tableview";
            this.flp_tableview.Size = new System.Drawing.Size(710, 415);
            this.flp_tableview.TabIndex = 22;
            // 
            // flp_sidemenu
            // 
            this.flp_sidemenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.flp_sidemenu.Location = new System.Drawing.Point(135, 163);
            this.flp_sidemenu.Margin = new System.Windows.Forms.Padding(2);
            this.flp_sidemenu.Name = "flp_sidemenu";
            this.flp_sidemenu.Size = new System.Drawing.Size(207, 293);
            this.flp_sidemenu.TabIndex = 21;
            this.flp_sidemenu.Visible = false;
            // 
            // FrmTableView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(714, 526);
            this.Controls.Add(this.flp_sidemenu);
            this.Controls.Add(this.tlp_main);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTableView";
            this.Tag = "30";
            this.Text = "FrmTableView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTableView_FormClosing);
            this.Load += new System.EventHandler(this.FrmTableView_Load);
            this.Resize += new System.EventHandler(this.FrmTableView_Resize);
            this.tlp_main.ResumeLayout(false);
            this.tlp_footer.ResumeLayout(false);
            this.tlp_footer.PerformLayout();
            this.tlp_search.ResumeLayout(false);
            this.tlp_search.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Timer AutoTimer;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private FontAwesome.Sharp.IconButton btn_showmenu;
        private System.Windows.Forms.FlowLayoutPanel flp_sidemenu;
        private System.Windows.Forms.FlowLayoutPanel flp_tableview;
        private System.Windows.Forms.TableLayoutPanel tlp_search;
        private System.Windows.Forms.TextBox txttablecode_search;
        private System.Windows.Forms.Button btnsearch;
        private System.Windows.Forms.Button btn_cancle_pickup;
        private System.Windows.Forms.Button btn_refresh;
        private System.Windows.Forms.TableLayoutPanel tlp_footer;
        private System.Windows.Forms.Button btnprev;
        private System.Windows.Forms.Label lblpagerinfo;
        private System.Windows.Forms.Button btnnext;
        private System.Windows.Forms.Button btnclose;
        private System.Windows.Forms.Label lblmemory;
    }
}