
namespace FoodandBeverage.Forms
{
    partial class FrmDashboard
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.flp_menu = new System.Windows.Forms.FlowLayoutPanel();
            this.Menuhome = new FontAwesome.Sharp.IconButton();
            this.Menuoperation = new FontAwesome.Sharp.IconButton();
            this.Menubilling = new FontAwesome.Sharp.IconButton();
            this.Menureport = new FontAwesome.Sharp.IconButton();
            this.Menusetting = new FontAwesome.Sharp.IconButton();
            this.Menulogout = new FontAwesome.Sharp.IconButton();
            this.pnl_header = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.panelMenu.SuspendLayout();
            this.flp_menu.SuspendLayout();
            this.pnl_header.SuspendLayout();
            this.tlp_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.AutoScroll = true;
            this.panelMenu.AutoScrollMargin = new System.Drawing.Size(0, 50);
            this.panelMenu.AutoScrollMinSize = new System.Drawing.Size(0, 10);
            this.panelMenu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMenu.BackColor = System.Drawing.Color.Silver;
            this.panelMenu.Controls.Add(this.flp_menu);
            this.panelMenu.Controls.Add(this.pnl_header);
            this.panelMenu.ForeColor = System.Drawing.SystemColors.Window;
            this.panelMenu.Location = new System.Drawing.Point(22, 22);
            this.panelMenu.Margin = new System.Windows.Forms.Padding(2);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(183, 470);
            this.panelMenu.TabIndex = 4;
            this.panelMenu.Tag = "MenuBar";
            // 
            // flp_menu
            // 
            this.flp_menu.BackColor = System.Drawing.Color.White;
            this.flp_menu.Controls.Add(this.Menuhome);
            this.flp_menu.Controls.Add(this.Menuoperation);
            this.flp_menu.Controls.Add(this.Menubilling);
            this.flp_menu.Controls.Add(this.Menureport);
            this.flp_menu.Controls.Add(this.Menusetting);
            this.flp_menu.Controls.Add(this.Menulogout);
            this.flp_menu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flp_menu.Location = new System.Drawing.Point(0, 63);
            this.flp_menu.Name = "flp_menu";
            this.flp_menu.Size = new System.Drawing.Size(183, 407);
            this.flp_menu.TabIndex = 1;
            // 
            // Menuhome
            // 
            this.Menuhome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Menuhome.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Menuhome.FlatAppearance.BorderSize = 0;
            this.Menuhome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Menuhome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menuhome.IconChar = FontAwesome.Sharp.IconChar.XRay;
            this.Menuhome.IconColor = System.Drawing.Color.White;
            this.Menuhome.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Menuhome.IconSize = 30;
            this.Menuhome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Menuhome.Location = new System.Drawing.Point(2, 10);
            this.Menuhome.Margin = new System.Windows.Forms.Padding(2, 10, 2, 2);
            this.Menuhome.Name = "Menuhome";
            this.Menuhome.Size = new System.Drawing.Size(179, 52);
            this.Menuhome.TabIndex = 3;
            this.Menuhome.Tag = "Location";
            this.Menuhome.Text = "Home";
            this.Menuhome.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Menuhome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Menuhome.UseVisualStyleBackColor = false;
            this.Menuhome.Click += new System.EventHandler(this.Menuhome_Click);
            // 
            // Menuoperation
            // 
            this.Menuoperation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Menuoperation.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Menuoperation.FlatAppearance.BorderSize = 0;
            this.Menuoperation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Menuoperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menuoperation.IconChar = FontAwesome.Sharp.IconChar.S;
            this.Menuoperation.IconColor = System.Drawing.Color.White;
            this.Menuoperation.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Menuoperation.IconSize = 30;
            this.Menuoperation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Menuoperation.Location = new System.Drawing.Point(2, 74);
            this.Menuoperation.Margin = new System.Windows.Forms.Padding(2, 10, 2, 2);
            this.Menuoperation.Name = "Menuoperation";
            this.Menuoperation.Size = new System.Drawing.Size(179, 52);
            this.Menuoperation.TabIndex = 4;
            this.Menuoperation.Tag = "operation";
            this.Menuoperation.Text = "Operations";
            this.Menuoperation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Menuoperation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Menuoperation.UseVisualStyleBackColor = false;
            this.Menuoperation.Click += new System.EventHandler(this.Menuoperation_Click);
            // 
            // Menubilling
            // 
            this.Menubilling.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Menubilling.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Menubilling.FlatAppearance.BorderSize = 0;
            this.Menubilling.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Menubilling.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menubilling.IconChar = FontAwesome.Sharp.IconChar.Landmark;
            this.Menubilling.IconColor = System.Drawing.Color.White;
            this.Menubilling.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Menubilling.IconSize = 30;
            this.Menubilling.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Menubilling.Location = new System.Drawing.Point(2, 138);
            this.Menubilling.Margin = new System.Windows.Forms.Padding(2, 10, 2, 2);
            this.Menubilling.Name = "Menubilling";
            this.Menubilling.Size = new System.Drawing.Size(179, 52);
            this.Menubilling.TabIndex = 6;
            this.Menubilling.Tag = "Location";
            this.Menubilling.Text = "Billing";
            this.Menubilling.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Menubilling.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Menubilling.UseVisualStyleBackColor = false;
            this.Menubilling.Click += new System.EventHandler(this.Menubilling_Click);
            // 
            // Menureport
            // 
            this.Menureport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Menureport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Menureport.FlatAppearance.BorderSize = 0;
            this.Menureport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Menureport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menureport.IconChar = FontAwesome.Sharp.IconChar.Landmark;
            this.Menureport.IconColor = System.Drawing.Color.White;
            this.Menureport.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Menureport.IconSize = 30;
            this.Menureport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Menureport.Location = new System.Drawing.Point(2, 202);
            this.Menureport.Margin = new System.Windows.Forms.Padding(2, 10, 2, 2);
            this.Menureport.Name = "Menureport";
            this.Menureport.Size = new System.Drawing.Size(179, 52);
            this.Menureport.TabIndex = 5;
            this.Menureport.Tag = "Report";
            this.Menureport.Text = "Report";
            this.Menureport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Menureport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Menureport.UseVisualStyleBackColor = false;
            this.Menureport.Click += new System.EventHandler(this.Menureport_Click);
            // 
            // Menusetting
            // 
            this.Menusetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Menusetting.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Menusetting.FlatAppearance.BorderSize = 0;
            this.Menusetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Menusetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menusetting.IconChar = FontAwesome.Sharp.IconChar.Landmark;
            this.Menusetting.IconColor = System.Drawing.Color.White;
            this.Menusetting.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Menusetting.IconSize = 30;
            this.Menusetting.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Menusetting.Location = new System.Drawing.Point(2, 266);
            this.Menusetting.Margin = new System.Windows.Forms.Padding(2, 10, 2, 2);
            this.Menusetting.Name = "Menusetting";
            this.Menusetting.Size = new System.Drawing.Size(179, 52);
            this.Menusetting.TabIndex = 7;
            this.Menusetting.Tag = "Location";
            this.Menusetting.Text = "Settings";
            this.Menusetting.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Menusetting.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Menusetting.UseVisualStyleBackColor = false;
            this.Menusetting.Click += new System.EventHandler(this.Menusetting_Click);
            // 
            // Menulogout
            // 
            this.Menulogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Menulogout.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.Menulogout.FlatAppearance.BorderSize = 0;
            this.Menulogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Menulogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Menulogout.IconChar = FontAwesome.Sharp.IconChar.Landmark;
            this.Menulogout.IconColor = System.Drawing.Color.White;
            this.Menulogout.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.Menulogout.IconSize = 30;
            this.Menulogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Menulogout.Location = new System.Drawing.Point(2, 330);
            this.Menulogout.Margin = new System.Windows.Forms.Padding(2, 10, 2, 2);
            this.Menulogout.Name = "Menulogout";
            this.Menulogout.Size = new System.Drawing.Size(179, 52);
            this.Menulogout.TabIndex = 8;
            this.Menulogout.Tag = "Location";
            this.Menulogout.Text = "Logout";
            this.Menulogout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Menulogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Menulogout.UseVisualStyleBackColor = false;
            this.Menulogout.Click += new System.EventHandler(this.Menulogout_Click);
            // 
            // pnl_header
            // 
            this.pnl_header.BackColor = System.Drawing.Color.Black;
            this.pnl_header.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl_header.Controls.Add(this.label2);
            this.pnl_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_header.Location = new System.Drawing.Point(0, 0);
            this.pnl_header.Margin = new System.Windows.Forms.Padding(2);
            this.pnl_header.Name = "pnl_header";
            this.pnl_header.Size = new System.Drawing.Size(183, 63);
            this.pnl_header.TabIndex = 0;
            this.pnl_header.Click += new System.EventHandler(this.pnl_header_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Maiandra GD", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Menu;
            this.label2.Location = new System.Drawing.Point(45, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "f&b";
            // 
            // panelDesktop
            // 
            this.panelDesktop.BackColor = System.Drawing.Color.White;
            this.panelDesktop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop.Location = new System.Drawing.Point(220, 23);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(756, 468);
            this.panelDesktop.TabIndex = 5;
            // 
            // tlp_main
            // 
            this.tlp_main.BackColor = System.Drawing.Color.White;
            this.tlp_main.ColumnCount = 5;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 10F));
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_main.Controls.Add(this.panelDesktop, 3, 1);
            this.tlp_main.Controls.Add(this.panelMenu, 1, 1);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(0, 0);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 3;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlp_main.Size = new System.Drawing.Size(1005, 515);
            this.tlp_main.TabIndex = 6;
            // 
            // FrmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 515);
            this.Controls.Add(this.tlp_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmDashboard";
            this.Text = "FrmDashboard";
            this.Load += new System.EventHandler(this.FrmDashboard_Load);
            this.panelMenu.ResumeLayout(false);
            this.flp_menu.ResumeLayout(false);
            this.pnl_header.ResumeLayout(false);
            this.pnl_header.PerformLayout();
            this.tlp_main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel pnl_header;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flp_menu;
        private System.Windows.Forms.Panel panelDesktop;
        private FontAwesome.Sharp.IconButton Menuoperation;
        private FontAwesome.Sharp.IconButton Menubilling;
        private FontAwesome.Sharp.IconButton Menureport;
        private FontAwesome.Sharp.IconButton Menusetting;
        private FontAwesome.Sharp.IconButton Menulogout;
        private FontAwesome.Sharp.IconButton Menuhome;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
    }
}