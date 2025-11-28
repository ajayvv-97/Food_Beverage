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
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pblogo = new System.Windows.Forms.PictureBox();
            this.pbusericon = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_pickup = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btnsearch = new System.Windows.Forms.Button();
            this.tlp_side_tableview = new System.Windows.Forms.TableLayoutPanel();
            this.flp_sidemenu = new System.Windows.Forms.FlowLayoutPanel();
            this.flp_tableview = new System.Windows.Forms.FlowLayoutPanel();
            this.txttablecode = new System.Windows.Forms.TextBox();
            this.tlp_main.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pblogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbusericon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tlp_side_tableview.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlp_main
            // 
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlp_main.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tlp_main.Controls.Add(this.tableLayoutPanel2, 0, 1);
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
            this.tlp_main.Size = new System.Drawing.Size(784, 432);
            this.tlp_main.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 90.07092F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 9.929078F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.tableLayoutPanel1.Controls.Add(this.pblogo, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pbusericon, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(778, 36);
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
            this.pblogo.Size = new System.Drawing.Size(42, 28);
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
            this.pbusericon.Location = new System.Drawing.Point(651, 4);
            this.pbusericon.Margin = new System.Windows.Forms.Padding(4);
            this.pbusericon.Name = "pbusericon";
            this.pbusericon.Size = new System.Drawing.Size(57, 28);
            this.pbusericon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbusericon.TabIndex = 5;
            this.pbusericon.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::FoodandBeverage.Properties.Resources.usericon_dashboard;
            this.pictureBox1.Location = new System.Drawing.Point(716, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(58, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.button3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.button2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.button1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_pickup, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 45);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(778, 49);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::FoodandBeverage.Properties.Resources.fb_Pickup;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(393, 4);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(123, 41);
            this.button3.TabIndex = 12;
            this.button3.Text = "Pick Up";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.BackgroundImage = global::FoodandBeverage.Properties.Resources.fb_Pickup;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(524, 4);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 41);
            this.button2.TabIndex = 11;
            this.button2.Text = "Pick Up";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::FoodandBeverage.Properties.Resources.fb_Pickup;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(131, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 41);
            this.button1.TabIndex = 10;
            this.button1.Text = "Pick Up";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btn_pickup
            // 
            this.btn_pickup.BackgroundImage = global::FoodandBeverage.Properties.Resources.fb_Pickup;
            this.btn_pickup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_pickup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_pickup.FlatAppearance.BorderSize = 0;
            this.btn_pickup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pickup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_pickup.ForeColor = System.Drawing.Color.White;
            this.btn_pickup.Location = new System.Drawing.Point(262, 4);
            this.btn_pickup.Margin = new System.Windows.Forms.Padding(4);
            this.btn_pickup.Name = "btn_pickup";
            this.btn_pickup.Size = new System.Drawing.Size(123, 41);
            this.btn_pickup.TabIndex = 9;
            this.btn_pickup.Text = "Pick Up";
            this.btn_pickup.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.txttablecode, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.btnsearch, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 100);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(778, 42);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // btnsearch
            // 
            this.btnsearch.BackgroundImage = global::FoodandBeverage.Properties.Resources.search;
            this.btnsearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnsearch.FlatAppearance.BorderSize = 0;
            this.btnsearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsearch.Location = new System.Drawing.Point(548, 4);
            this.btnsearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnsearch.Name = "btnsearch";
            this.btnsearch.Size = new System.Drawing.Size(55, 34);
            this.btnsearch.TabIndex = 2;
            this.btnsearch.UseVisualStyleBackColor = true;
            this.btnsearch.Click += new System.EventHandler(this.btnsearch_Click);
            // 
            // tlp_side_tableview
            // 
            this.tlp_side_tableview.ColumnCount = 2;
            this.tlp_side_tableview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.9383F));
            this.tlp_side_tableview.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 84.0617F));
            this.tlp_side_tableview.Controls.Add(this.flp_tableview, 1, 0);
            this.tlp_side_tableview.Controls.Add(this.flp_sidemenu, 0, 0);
            this.tlp_side_tableview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_side_tableview.Location = new System.Drawing.Point(3, 148);
            this.tlp_side_tableview.Name = "tlp_side_tableview";
            this.tlp_side_tableview.RowCount = 1;
            this.tlp_side_tableview.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_side_tableview.Size = new System.Drawing.Size(778, 281);
            this.tlp_side_tableview.TabIndex = 3;
            // 
            // flp_sidemenu
            // 
            this.flp_sidemenu.AutoSize = true;
            this.flp_sidemenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_sidemenu.Location = new System.Drawing.Point(3, 3);
            this.flp_sidemenu.Name = "flp_sidemenu";
            this.flp_sidemenu.Size = new System.Drawing.Size(117, 275);
            this.flp_sidemenu.TabIndex = 0;
            // 
            // flp_tableview
            // 
            this.flp_tableview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_tableview.Location = new System.Drawing.Point(126, 3);
            this.flp_tableview.Name = "flp_tableview";
            this.flp_tableview.Size = new System.Drawing.Size(649, 275);
            this.flp_tableview.TabIndex = 0;
            // 
            // txttablecode
            // 
            this.txttablecode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txttablecode.Location = new System.Drawing.Point(173, 3);
            this.txttablecode.Multiline = true;
            this.txttablecode.Name = "txttablecode";
            this.txttablecode.Size = new System.Drawing.Size(368, 36);
            this.txttablecode.TabIndex = 1;
            this.txttablecode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txttablecode_KeyPress);
            // 
            // FrmTableTabletView
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(220)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(800, 448);
            this.Controls.Add(this.tlp_main);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(800, 400);
            this.Name = "FrmTableTabletView";
            this.Padding = new System.Windows.Forms.Padding(8);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Tablets - Table View";
            this.Load += new System.EventHandler(this.FrmTableTabletView_Load);
            this.tlp_main.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pblogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbusericon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tlp_side_tableview.ResumeLayout(false);
            this.tlp_side_tableview.PerformLayout();
            this.ResumeLayout(false);

        }

        private TableLayoutPanel tlp_main;
        private TableLayoutPanel tableLayoutPanel1;
        public PictureBox pblogo;
        public PictureBox pbusericon;
        public PictureBox pictureBox1;
        private TableLayoutPanel tableLayoutPanel2;
        private Button button3;
        private Button button2;
        private Button button1;
        private Button btn_pickup;
        private TableLayoutPanel tableLayoutPanel3;
        private Button btnsearch;
        private TableLayoutPanel tlp_side_tableview;
        private FlowLayoutPanel flp_sidemenu;
        private FlowLayoutPanel flp_tableview;
        private TextBox txttablecode;
    }
}
