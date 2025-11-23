using System;
using System.Drawing;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    partial class FrmTableTabletView
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelTopBar;
        private PictureBox pictureLogo;
        private FlowLayoutPanel flpRightButtons;
        private PictureBox pictureProfile;
        private Button btnLogout;
        private Button btnLock;
        private Label lblPageTitle;

        private FlowLayoutPanel flpFilterButtons;
        private Button btnRefresh;
        private Button btnAvailable;
        private Button btnActive;
        private Button btnReserved;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTableTabletView));
            this.panelTopBar = new System.Windows.Forms.Panel();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.pictureLogo = new System.Windows.Forms.PictureBox();
            this.flpRightButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureProfile = new System.Windows.Forms.PictureBox();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnLock = new System.Windows.Forms.Button();
            this.flpFilterButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAvailable = new System.Windows.Forms.Button();
            this.btnActive = new System.Windows.Forms.Button();
            this.btnReserved = new System.Windows.Forms.Button();
            this.panelTopBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).BeginInit();
            this.flpRightButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureProfile)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTopBar
            // 
            this.panelTopBar.BackColor = System.Drawing.Color.White;
            this.panelTopBar.Controls.Add(this.lblPageTitle);
            this.panelTopBar.Controls.Add(this.pictureLogo);
            this.panelTopBar.Controls.Add(this.flpRightButtons);
            this.panelTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopBar.Location = new System.Drawing.Point(0, 70);
            this.panelTopBar.Name = "panelTopBar";
            this.panelTopBar.Padding = new System.Windows.Forms.Padding(10);
            this.panelTopBar.Size = new System.Drawing.Size(1280, 80);
            this.panelTopBar.TabIndex = 0;
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPageTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.Location = new System.Drawing.Point(70, 10);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(910, 60);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "TABLE VIEW PAGE";
            this.lblPageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureLogo
            // 
            this.pictureLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureLogo.Image = ((System.Drawing.Image)(resources.GetObject("pictureLogo.Image")));
            this.pictureLogo.Location = new System.Drawing.Point(10, 10);
            this.pictureLogo.Name = "pictureLogo";
            this.pictureLogo.Size = new System.Drawing.Size(60, 60);
            this.pictureLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureLogo.TabIndex = 1;
            this.pictureLogo.TabStop = false;
            // 
            // flpRightButtons
            // 
            this.flpRightButtons.AutoSize = true;
            this.flpRightButtons.Controls.Add(this.pictureProfile);
            this.flpRightButtons.Controls.Add(this.btnLogout);
            this.flpRightButtons.Controls.Add(this.btnLock);
            this.flpRightButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpRightButtons.Location = new System.Drawing.Point(980, 10);
            this.flpRightButtons.Name = "flpRightButtons";
            this.flpRightButtons.Size = new System.Drawing.Size(290, 60);
            this.flpRightButtons.TabIndex = 2;
            this.flpRightButtons.WrapContents = false;
            // 
            // pictureProfile
            // 
            this.pictureProfile.Image = ((System.Drawing.Image)(resources.GetObject("pictureProfile.Image")));
            this.pictureProfile.Location = new System.Drawing.Point(0, 0);
            this.pictureProfile.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.pictureProfile.Name = "pictureProfile";
            this.pictureProfile.Size = new System.Drawing.Size(60, 60);
            this.pictureProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureProfile.TabIndex = 0;
            this.pictureProfile.TabStop = false;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(70, 10);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(0, 10, 10, 10);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(100, 40);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            // 
            // btnLock
            // 
            this.btnLock.Location = new System.Drawing.Point(180, 10);
            this.btnLock.Margin = new System.Windows.Forms.Padding(0, 10, 10, 10);
            this.btnLock.Name = "btnLock";
            this.btnLock.Size = new System.Drawing.Size(100, 40);
            this.btnLock.TabIndex = 2;
            this.btnLock.Text = "Lock";
            // 
            // flpFilterButtons
            // 
            this.flpFilterButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpFilterButtons.Location = new System.Drawing.Point(0, 0);
            this.flpFilterButtons.Name = "flpFilterButtons";
            this.flpFilterButtons.Padding = new System.Windows.Forms.Padding(10);
            this.flpFilterButtons.Size = new System.Drawing.Size(1280, 70);
            this.flpFilterButtons.TabIndex = 1;
            this.flpFilterButtons.WrapContents = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.Location = new System.Drawing.Point(196, 163);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(10);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(150, 50);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "Refresh";
            // 
            // btnAvailable
            // 
            this.btnAvailable.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnAvailable.Location = new System.Drawing.Point(366, 163);
            this.btnAvailable.Margin = new System.Windows.Forms.Padding(10);
            this.btnAvailable.Name = "btnAvailable";
            this.btnAvailable.Size = new System.Drawing.Size(150, 50);
            this.btnAvailable.TabIndex = 1;
            this.btnAvailable.Text = "Available";
            // 
            // btnActive
            // 
            this.btnActive.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnActive.Location = new System.Drawing.Point(536, 163);
            this.btnActive.Margin = new System.Windows.Forms.Padding(10);
            this.btnActive.Name = "btnActive";
            this.btnActive.Size = new System.Drawing.Size(150, 50);
            this.btnActive.TabIndex = 2;
            this.btnActive.Text = "Active";
            // 
            // btnReserved
            // 
            this.btnReserved.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnReserved.Location = new System.Drawing.Point(721, 163);
            this.btnReserved.Margin = new System.Windows.Forms.Padding(10);
            this.btnReserved.Name = "btnReserved";
            this.btnReserved.Size = new System.Drawing.Size(150, 50);
            this.btnReserved.TabIndex = 3;
            this.btnReserved.Text = "Reserved";
            // 
            // FrmTableTabletView
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.panelTopBar);
            this.Controls.Add(this.btnAvailable);
            this.Controls.Add(this.flpFilterButtons);
            this.Controls.Add(this.btnActive);
            this.Controls.Add(this.btnReserved);
            this.Name = "FrmTableTabletView";
            this.Text = "Tablet Table View";
            this.panelTopBar.ResumeLayout(false);
            this.panelTopBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLogo)).EndInit();
            this.flpRightButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureProfile)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
