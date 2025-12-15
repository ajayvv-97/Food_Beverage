// FrmTableTabletView.Designer.cs
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    partial class FrmTableTabletView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTableTabletView));
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pblogo = new System.Windows.Forms.PictureBox();
            this.pbusericon = new System.Windows.Forms.PictureBox();
            this.btn_minimium = new System.Windows.Forms.Button();
            this.tlp_racbutton = new System.Windows.Forms.TableLayoutPanel();
            this.btncanceltable = new System.Windows.Forms.Button();
            this.btnaviltable = new System.Windows.Forms.Button();
            this.btn_running = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.txttablecode_search = new System.Windows.Forms.TextBox();
            this.btnsearch = new System.Windows.Forms.Button();
            this.btn_refresh = new System.Windows.Forms.Button();
            this.tlp_side_tableview = new System.Windows.Forms.TableLayoutPanel();
            this.flp_tableview = new System.Windows.Forms.FlowLayoutPanel();
            this.flp_sidemenu = new System.Windows.Forms.FlowLayoutPanel();
            this.tlp_footer = new System.Windows.Forms.TableLayoutPanel();
            this.lblmemory = new System.Windows.Forms.Label();
            this.btnprev = new System.Windows.Forms.Button();
            this.lblpagerinfo = new System.Windows.Forms.Label();
            this.btnnext = new System.Windows.Forms.Button();
            this.pnl_userdetails = new System.Windows.Forms.Panel();
            this.tlp_userdetails = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_edepart = new System.Windows.Forms.Label();
            this.lbl_edesign = new System.Windows.Forms.Label();
            this.lbl_ename = new System.Windows.Forms.Label();
            this.tlp_des_logout = new System.Windows.Forms.TableLayoutPanel();
            this.btn_elogout = new System.Windows.Forms.Button();
            this.tlp_des_lock = new System.Windows.Forms.TableLayoutPanel();
            this.btn_lock = new System.Windows.Forms.Button();
            this.AutoTimer = new System.Windows.Forms.Timer(this.components);
            this.tlp_main.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pblogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbusericon)).BeginInit();
            this.tlp_racbutton.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tlp_side_tableview.SuspendLayout();
            this.tlp_footer.SuspendLayout();
            this.pnl_userdetails.SuspendLayout();
            this.tlp_userdetails.SuspendLayout();
            this.tlp_des_logout.SuspendLayout();
            this.tlp_des_lock.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_main
            // 
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_racbutton, 0, 1);
            this.tlp_main.Controls.Add(this.tableLayoutPanel3, 0, 2);
            this.tlp_main.Controls.Add(this.tlp_side_tableview, 0, 3);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(8, 8);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 4;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Size = new System.Drawing.Size(552, 413);
            this.tlp_main.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 92.30769F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 7.692307F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.Controls.Add(this.pblogo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbusericon, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btn_minimium, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(546, 24);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pblogo
            // 
            this.pblogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pblogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pblogo.Image = global::FoodandBeverage.Properties.Resources.midas_logo;
            this.pblogo.Location = new System.Drawing.Point(4, 4);
            this.pblogo.Margin = new System.Windows.Forms.Padding(4);
            this.pblogo.Name = "pblogo";
            this.pblogo.Size = new System.Drawing.Size(22, 16);
            this.pblogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pblogo.TabIndex = 7;
            this.pblogo.TabStop = false;
            this.pblogo.Click += new System.EventHandler(this.pblogo_Click);
            // 
            // pbusericon
            // 
            this.pbusericon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pbusericon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbusericon.Image = global::FoodandBeverage.Properties.Resources.usericon_dashboard;
            this.pbusericon.Location = new System.Drawing.Point(486, 4);
            this.pbusericon.Margin = new System.Windows.Forms.Padding(4);
            this.pbusericon.Name = "pbusericon";
            this.pbusericon.Size = new System.Drawing.Size(29, 16);
            this.pbusericon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbusericon.TabIndex = 5;
            this.pbusericon.TabStop = false;
            this.pbusericon.Click += new System.EventHandler(this.pbusericon_Click);
            // 
            // btn_minimium
            // 
            this.btn_minimium.BackColor = System.Drawing.Color.Transparent;
            this.btn_minimium.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_minimium.BackgroundImage")));
            this.btn_minimium.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_minimium.FlatAppearance.BorderSize = 0;
            this.btn_minimium.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_minimium.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_minimium.ForeColor = System.Drawing.Color.Transparent;
            this.btn_minimium.Location = new System.Drawing.Point(522, 3);
            this.btn_minimium.Name = "btn_minimium";
            this.btn_minimium.Size = new System.Drawing.Size(21, 18);
            this.btn_minimium.TabIndex = 8;
            this.btn_minimium.UseVisualStyleBackColor = false;
            this.btn_minimium.Click += new System.EventHandler(this.btn_minimium_Click);
            // 
            // tlp_racbutton
            // 
            this.tlp_racbutton.ColumnCount = 6;
            this.tlp_racbutton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.75163F));
            this.tlp_racbutton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_racbutton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_racbutton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_racbutton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_racbutton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.24837F));
            this.tlp_racbutton.Controls.Add(this.btncanceltable, 1, 0);
            this.tlp_racbutton.Controls.Add(this.btnaviltable, 1, 0);
            this.tlp_racbutton.Controls.Add(this.btn_running, 1, 0);
            this.tlp_racbutton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_racbutton.Location = new System.Drawing.Point(3, 33);
            this.tlp_racbutton.Name = "tlp_racbutton";
            this.tlp_racbutton.RowCount = 1;
            this.tlp_racbutton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_racbutton.Size = new System.Drawing.Size(546, 34);
            this.tlp_racbutton.TabIndex = 1;
            // 
            // btncanceltable
            // 
            this.btncanceltable.BackgroundImage = global::FoodandBeverage.Properties.Resources.fb_Pickup;
            this.btncanceltable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btncanceltable.FlatAppearance.BorderSize = 0;
            this.btncanceltable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncanceltable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncanceltable.ForeColor = System.Drawing.Color.Red;
            this.btncanceltable.Location = new System.Drawing.Point(313, 4);
            this.btncanceltable.Margin = new System.Windows.Forms.Padding(4);
            this.btncanceltable.Name = "btncanceltable";
            this.btncanceltable.Size = new System.Drawing.Size(87, 26);
            this.btncanceltable.TabIndex = 12;
            this.btncanceltable.Text = "Cancelled ";
            this.btncanceltable.UseVisualStyleBackColor = true;
            this.btncanceltable.Click += new System.EventHandler(this.btncanceltable_Click);
            // 
            // btnaviltable
            // 
            this.btnaviltable.BackgroundImage = global::FoodandBeverage.Properties.Resources.fb_Pickup;
            this.btnaviltable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnaviltable.FlatAppearance.BorderSize = 0;
            this.btnaviltable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnaviltable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnaviltable.ForeColor = System.Drawing.Color.White;
            this.btnaviltable.Location = new System.Drawing.Point(218, 4);
            this.btnaviltable.Margin = new System.Windows.Forms.Padding(4);
            this.btnaviltable.Name = "btnaviltable";
            this.btnaviltable.Size = new System.Drawing.Size(87, 26);
            this.btnaviltable.TabIndex = 10;
            this.btnaviltable.Text = "Avilable";
            this.btnaviltable.UseVisualStyleBackColor = true;
            this.btnaviltable.Click += new System.EventHandler(this.btnaviltable_Click);
            // 
            // btn_running
            // 
            this.btn_running.BackgroundImage = global::FoodandBeverage.Properties.Resources.fb_Pickup;
            this.btn_running.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_running.FlatAppearance.BorderSize = 0;
            this.btn_running.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_running.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_running.ForeColor = System.Drawing.Color.White;
            this.btn_running.Location = new System.Drawing.Point(123, 4);
            this.btn_running.Margin = new System.Windows.Forms.Padding(4);
            this.btn_running.Name = "btn_running";
            this.btn_running.Size = new System.Drawing.Size(87, 26);
            this.btn_running.TabIndex = 9;
            this.btn_running.Text = "Runing";
            this.btn_running.UseVisualStyleBackColor = true;
            this.btn_running.Click += new System.EventHandler(this.btn_running_Click);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 5;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.txttablecode_search, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnsearch, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.btn_refresh, 3, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 73);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(546, 28);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // txttablecode_search
            // 
            this.txttablecode_search.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txttablecode_search.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txttablecode_search.Location = new System.Drawing.Point(136, 3);
            this.txttablecode_search.Multiline = true;
            this.txttablecode_search.Name = "txttablecode_search";
            this.txttablecode_search.Size = new System.Drawing.Size(186, 22);
            this.txttablecode_search.TabIndex = 1;
            this.txttablecode_search.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txttablecode_KeyPress);
            // 
            // btnsearch
            // 
            this.btnsearch.BackgroundImage = global::FoodandBeverage.Properties.Resources.search;
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnsearch.FlatAppearance.BorderSize = 0;
            this.btnsearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsearch.Location = new System.Drawing.Point(329, 4);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(39, 20);
            this.btnsearch.TabIndex = 2;
            this.btnsearch.UseVisualStyleBackColor = true;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // btn_refresh
            // 
            this.btn_refresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_refresh.BackgroundImage")));
            this.btn_refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_refresh.FlatAppearance.BorderSize = 0;
            this.btn_refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_refresh.Location = new System.Drawing.Point(375, 3);
            this.btn_refresh.Name = "btn_refresh";
            this.btn_refresh.Size = new System.Drawing.Size(34, 22);
            this.btn_refresh.TabIndex = 3;
            this.btn_refresh.UseVisualStyleBackColor = true;
            this.btn_refresh.Click += new System.EventHandler(this.btn_refresh_Click);
            // 
            // tlp_side_tableview
            // 
            this.tlp_side_tableview.ColumnCount = 2;
            this.tlp_side_tableview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.21612F));
            this.tlp_side_tableview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.78388F));
            this.tlp_side_tableview.Controls.Add(this.flp_tableview, 1, 0);
            this.tlp_side_tableview.Controls.Add(this.flp_sidemenu, 0, 0);
            this.tlp_side_tableview.Controls.Add(this.tlp_footer, 1, 1);
            this.tlp_side_tableview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_side_tableview.Location = new System.Drawing.Point(3, 107);
            this.tlp_side_tableview.Name = "tlp_side_tableview";
            this.tlp_side_tableview.RowCount = 2;
            this.tlp_side_tableview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_side_tableview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tlp_side_tableview.Size = new System.Drawing.Size(546, 303);
            this.tlp_side_tableview.TabIndex = 3;
            // 
            // flp_tableview
            // 
            this.flp_tableview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_tableview.Location = new System.Drawing.Point(97, 3);
            this.flp_tableview.Name = "flp_tableview";
            this.flp_tableview.Size = new System.Drawing.Size(446, 273);
            this.flp_tableview.TabIndex = 0;
            // 
            // flp_sidemenu
            // 
            this.flp_sidemenu.AutoSize = true;
            this.flp_sidemenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_sidemenu.Location = new System.Drawing.Point(3, 3);
            this.flp_sidemenu.Name = "flp_sidemenu";
            this.flp_sidemenu.Size = new System.Drawing.Size(88, 273);
            this.flp_sidemenu.TabIndex = 0;
            // 
            // tlp_footer
            // 
            this.tlp_footer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.tlp_footer.ColumnCount = 4;
            this.tlp_footer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_footer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 144F));
            this.tlp_footer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_footer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_footer.Controls.Add(this.lblmemory, 0, 0);
            this.tlp_footer.Controls.Add(this.btnprev, 0, 0);
            this.tlp_footer.Controls.Add(this.lblpagerinfo, 2, 0);
            this.tlp_footer.Controls.Add(this.btnnext, 3, 0);
            this.tlp_footer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_footer.Location = new System.Drawing.Point(97, 282);
            this.tlp_footer.Name = "tlp_footer";
            this.tlp_footer.RowCount = 1;
            this.tlp_footer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_footer.Size = new System.Drawing.Size(446, 18);
            this.tlp_footer.TabIndex = 25;
            // 
            // lblmemory
            // 
            this.lblmemory.AutoSize = true;
            this.lblmemory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblmemory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblmemory.Location = new System.Drawing.Point(34, 0);
            this.lblmemory.Name = "lblmemory";
            this.lblmemory.Size = new System.Drawing.Size(138, 18);
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
            this.btnprev.Size = new System.Drawing.Size(25, 12);
            this.btnprev.TabIndex = 0;
            this.btnprev.UseVisualStyleBackColor = true;
            this.btnprev.Click += new System.EventHandler(this.btnprev_Click);
            // 
            // lblpagerinfo
            // 
            this.lblpagerinfo.AutoSize = true;
            this.lblpagerinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblpagerinfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpagerinfo.Location = new System.Drawing.Point(178, 0);
            this.lblpagerinfo.Name = "lblpagerinfo";
            this.lblpagerinfo.Size = new System.Drawing.Size(234, 18);
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
            this.btnnext.Location = new System.Drawing.Point(418, 3);
            this.btnnext.Name = "btnnext";
            this.btnnext.Size = new System.Drawing.Size(25, 12);
            this.btnnext.TabIndex = 291;
            this.btnnext.UseVisualStyleBackColor = true;
            this.btnnext.Click += new System.EventHandler(this.btnnext_Click);
            // 
            // pnl_userdetails
            // 
            this.pnl_userdetails.Controls.Add(this.tlp_userdetails);
            this.pnl_userdetails.Location = new System.Drawing.Point(389, 128);
            this.pnl_userdetails.Margin = new System.Windows.Forms.Padding(2);
            this.pnl_userdetails.Name = "pnl_userdetails";
            this.pnl_userdetails.Padding = new System.Windows.Forms.Padding(2);
            this.pnl_userdetails.Size = new System.Drawing.Size(163, 247);
            this.pnl_userdetails.TabIndex = 38;
            this.pnl_userdetails.Visible = false;
            // 
            // tlp_userdetails
            // 
            this.tlp_userdetails.ColumnCount = 1;
            this.tlp_userdetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_userdetails.Controls.Add(this.lbl_edepart, 0, 2);
            this.tlp_userdetails.Controls.Add(this.lbl_edesign, 0, 1);
            this.tlp_userdetails.Controls.Add(this.lbl_ename, 0, 0);
            this.tlp_userdetails.Controls.Add(this.tlp_des_logout, 0, 4);
            this.tlp_userdetails.Controls.Add(this.tlp_des_lock, 0, 3);
            this.tlp_userdetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_userdetails.Location = new System.Drawing.Point(2, 2);
            this.tlp_userdetails.Margin = new System.Windows.Forms.Padding(2);
            this.tlp_userdetails.Name = "tlp_userdetails";
            this.tlp_userdetails.RowCount = 5;
            this.tlp_userdetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_userdetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_userdetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_userdetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlp_userdetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tlp_userdetails.Size = new System.Drawing.Size(159, 243);
            this.tlp_userdetails.TabIndex = 0;
            // 
            // lbl_edepart
            // 
            this.lbl_edepart.AutoSize = true;
            this.lbl_edepart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_edepart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_edepart.Location = new System.Drawing.Point(2, 98);
            this.lbl_edepart.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_edepart.Name = "lbl_edepart";
            this.lbl_edepart.Size = new System.Drawing.Size(155, 49);
            this.lbl_edepart.TabIndex = 2;
            this.lbl_edepart.Text = "Department";
            this.lbl_edepart.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_edesign
            // 
            this.lbl_edesign.AutoSize = true;
            this.lbl_edesign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_edesign.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_edesign.Location = new System.Drawing.Point(2, 49);
            this.lbl_edesign.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_edesign.Name = "lbl_edesign";
            this.lbl_edesign.Size = new System.Drawing.Size(155, 49);
            this.lbl_edesign.TabIndex = 1;
            this.lbl_edesign.Text = "Designation";
            this.lbl_edesign.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ename
            // 
            this.lbl_ename.AutoSize = true;
            this.lbl_ename.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_ename.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ename.Location = new System.Drawing.Point(2, 0);
            this.lbl_ename.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_ename.Name = "lbl_ename";
            this.lbl_ename.Size = new System.Drawing.Size(155, 49);
            this.lbl_ename.TabIndex = 0;
            this.lbl_ename.Text = "Emlpyee Name";
            this.lbl_ename.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlp_des_logout
            // 
            this.tlp_des_logout.ColumnCount = 3;
            this.tlp_des_logout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_des_logout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_des_logout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_des_logout.Controls.Add(this.btn_elogout, 1, 0);
            this.tlp_des_logout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_des_logout.Location = new System.Drawing.Point(3, 199);
            this.tlp_des_logout.Name = "tlp_des_logout";
            this.tlp_des_logout.RowCount = 1;
            this.tlp_des_logout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_des_logout.Size = new System.Drawing.Size(153, 41);
            this.tlp_des_logout.TabIndex = 267;
            // 
            // btn_elogout
            // 
            this.btn_elogout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_elogout.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_LOGOUT;
            this.btn_elogout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_elogout.FlatAppearance.BorderSize = 0;
            this.btn_elogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_elogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_elogout.ForeColor = System.Drawing.Color.White;
            this.btn_elogout.Location = new System.Drawing.Point(30, 3);
            this.btn_elogout.Name = "btn_elogout";
            this.btn_elogout.Size = new System.Drawing.Size(92, 35);
            this.btn_elogout.TabIndex = 9;
            this.btn_elogout.UseVisualStyleBackColor = true;
            this.btn_elogout.Click += new System.EventHandler(this.btn_elogout_Click);
            // 
            // tlp_des_lock
            // 
            this.tlp_des_lock.ColumnCount = 3;
            this.tlp_des_lock.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_des_lock.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_des_lock.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_des_lock.Controls.Add(this.btn_lock, 1, 0);
            this.tlp_des_lock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_des_lock.Location = new System.Drawing.Point(3, 150);
            this.tlp_des_lock.Name = "tlp_des_lock";
            this.tlp_des_lock.RowCount = 1;
            this.tlp_des_lock.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_des_lock.Size = new System.Drawing.Size(153, 43);
            this.tlp_des_lock.TabIndex = 267;
            // 
            // btn_lock
            // 
            this.btn_lock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_lock.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_LOCKSCREEN;
            this.btn_lock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_lock.FlatAppearance.BorderSize = 0;
            this.btn_lock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_lock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_lock.ForeColor = System.Drawing.Color.White;
            this.btn_lock.Location = new System.Drawing.Point(18, 3);
            this.btn_lock.Name = "btn_lock";
            this.btn_lock.Size = new System.Drawing.Size(116, 37);
            this.btn_lock.TabIndex = 10;
            this.btn_lock.UseVisualStyleBackColor = true;
            this.btn_lock.Click += new System.EventHandler(this.btn_lock_Click);
            // 
            // AutoTimer
            // 
            this.AutoTimer.Interval = 2000;
            this.AutoTimer.Tick += new System.EventHandler(this.AutoTimer_Tick);
            // 
            // FrmTableTabletView
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(568, 429);
            this.Controls.Add(this.pnl_userdetails);
            this.Controls.Add(this.tlp_main);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTableTabletView";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tablets - Table View";
            this.Load += new System.EventHandler(this.FrmTableTabletView_Load);
            this.Resize += new System.EventHandler(this.FrmTableTabletView_Resize);
            this.tlp_main.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pblogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbusericon)).EndInit();
            this.tlp_racbutton.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tlp_side_tableview.ResumeLayout(false);
            this.tlp_side_tableview.PerformLayout();
            this.tlp_footer.ResumeLayout(false);
            this.tlp_footer.PerformLayout();
            this.pnl_userdetails.ResumeLayout(false);
            this.tlp_userdetails.ResumeLayout(false);
            this.tlp_userdetails.PerformLayout();
            this.tlp_des_logout.ResumeLayout(false);
            this.tlp_des_lock.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private TableLayoutPanel tlp_main;
        private TableLayoutPanel tableLayoutPanel1;
        public PictureBox pblogo;
        public PictureBox pbusericon;
        private TableLayoutPanel tlp_racbutton;
        private Button btncanceltable;
        private Button btnaviltable;
        private Button btn_running;
        private TableLayoutPanel tableLayoutPanel3;
        private Button btnsearch;
        private TableLayoutPanel tlp_side_tableview;
        private FlowLayoutPanel flp_sidemenu;
        private FlowLayoutPanel flp_tableview;
        private TextBox txttablecode_search;
        private Panel pnl_userdetails;
        private TableLayoutPanel tlp_userdetails;
        private Label lbl_edepart;
        private Label lbl_edesign;
        private Label lbl_ename;
        private TableLayoutPanel tlp_des_logout;
        private Button btn_elogout;
        private TableLayoutPanel tlp_des_lock;
        private Button btn_lock;
        private Button btn_minimium;
        public Timer AutoTimer;
        private TableLayoutPanel tlp_footer;
        private Label lblmemory;
        private Button btnprev;
        private Label lblpagerinfo;
        private Button btnnext;
        private Button btn_refresh;
    }
}
