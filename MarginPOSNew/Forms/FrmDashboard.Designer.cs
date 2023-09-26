namespace MarginPOSNew.Forms
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDashboard));
            this.tmrClock = new System.Windows.Forms.Timer(this.components);
            this.mlDateTime = new MetroFramework.Controls.MetroLabel();
            this.mlStatus1 = new MetroFramework.Controls.MetroLabel();
            this.mlCompanyname = new MetroFramework.Controls.MetroLabel();
            this.mpMenu = new MetroFramework.Controls.MetroPanel();
            this.mtExtra = new MetroFramework.Controls.MetroTile();
            this.mtLogOut = new MetroFramework.Controls.MetroTile();
            this.mtCloseCurrent = new MetroFramework.Controls.MetroTile();
            this.mtSettings = new MetroFramework.Controls.MetroTile();
            this.mtReports = new MetroFramework.Controls.MetroTile();
            this.mtTransaction = new MetroFramework.Controls.MetroTile();
            this.mtMasters = new MetroFramework.Controls.MetroTile();
            this.mpMaster = new MetroFramework.Controls.MetroPanel();
            this.mtFinancialYear = new System.Windows.Forms.PictureBox();
            this.mtCompany = new System.Windows.Forms.PictureBox();
            this.mtAccountGroup = new System.Windows.Forms.PictureBox();
            this.mtDepartment = new System.Windows.Forms.PictureBox();
            this.mtDesignation = new System.Windows.Forms.PictureBox();
            this.myEmployee = new System.Windows.Forms.PictureBox();
            this.mtUnit = new System.Windows.Forms.PictureBox();
            this.mtItemClass = new System.Windows.Forms.PictureBox();
            this.mtCategory = new System.Windows.Forms.PictureBox();
            this.mtListItem = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.mtAccountLedger = new System.Windows.Forms.PictureBox();
            this.mpTransaction = new MetroFramework.Controls.MetroPanel();
            this.mtOffer = new System.Windows.Forms.PictureBox();
            this.mtOutward = new System.Windows.Forms.Button();
            this.mtInward = new System.Windows.Forms.Button();
            this.mtSalesPOS = new System.Windows.Forms.PictureBox();
            this.mtSalesReturn = new System.Windows.Forms.PictureBox();
            this.mtPurchaseReturn = new System.Windows.Forms.PictureBox();
            this.mtSales = new System.Windows.Forms.PictureBox();
            this.mtPurchase = new System.Windows.Forms.PictureBox();
            this.mpReports = new MetroFramework.Controls.MetroPanel();
            this.mtSalesPosWiseReport = new System.Windows.Forms.PictureBox();
            this.mtSalesReport = new System.Windows.Forms.PictureBox();
            this.mtRptSalesDetails = new System.Windows.Forms.PictureBox();
            this.mpSettings = new MetroFramework.Controls.MetroPanel();
            this.mtUserAccount = new System.Windows.Forms.PictureBox();
            this.mtUserAccountType = new System.Windows.Forms.PictureBox();
            this.mtGeneral = new System.Windows.Forms.PictureBox();
            this.mtGeneralType = new System.Windows.Forms.PictureBox();
            this.mlMarginPOSVersion = new MetroFramework.Controls.MetroLabel();
            this.pbclose = new System.Windows.Forms.PictureBox();
            this.mpMenu.SuspendLayout();
            this.mpMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mtFinancialYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtCompany)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtAccountGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtDepartment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtDesignation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myEmployee)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtItemClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtListItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtAccountLedger)).BeginInit();
            this.mpTransaction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mtOffer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtSalesPOS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtSalesReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtPurchaseReturn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtPurchase)).BeginInit();
            this.mpReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mtSalesPosWiseReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtSalesReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtRptSalesDetails)).BeginInit();
            this.mpSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mtUserAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtUserAccountType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtGeneral)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtGeneralType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbclose)).BeginInit();
            this.SuspendLayout();
            // 
            // tmrClock
            // 
            this.tmrClock.Enabled = true;
            this.tmrClock.Tick += new System.EventHandler(this.TmrClock_Tick);
            // 
            // mlDateTime
            // 
            this.mlDateTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.mlDateTime.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.mlDateTime.ForeColor = System.Drawing.Color.White;
            this.mlDateTime.Location = new System.Drawing.Point(882, 3);
            this.mlDateTime.Name = "mlDateTime";
            this.mlDateTime.Size = new System.Drawing.Size(664, 26);
            this.mlDateTime.TabIndex = 5;
            this.mlDateTime.Text = "DateTime";
            this.mlDateTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.mlDateTime.UseCustomBackColor = true;
            this.mlDateTime.UseCustomForeColor = true;
            // 
            // mlStatus1
            // 
            this.mlStatus1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.mlStatus1.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.mlStatus1.ForeColor = System.Drawing.Color.White;
            this.mlStatus1.Location = new System.Drawing.Point(699, 3);
            this.mlStatus1.Name = "mlStatus1";
            this.mlStatus1.Size = new System.Drawing.Size(239, 26);
            this.mlStatus1.TabIndex = 4;
            this.mlStatus1.Text = "Status1";
            this.mlStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mlStatus1.UseCustomBackColor = true;
            this.mlStatus1.UseCustomForeColor = true;
            // 
            // mlCompanyname
            // 
            this.mlCompanyname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.mlCompanyname.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.mlCompanyname.ForeColor = System.Drawing.Color.White;
            this.mlCompanyname.Location = new System.Drawing.Point(235, 3);
            this.mlCompanyname.Name = "mlCompanyname";
            this.mlCompanyname.Size = new System.Drawing.Size(468, 26);
            this.mlCompanyname.TabIndex = 3;
            this.mlCompanyname.Text = "Company Name";
            this.mlCompanyname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mlCompanyname.UseCustomBackColor = true;
            this.mlCompanyname.UseCustomForeColor = true;
            // 
            // mpMenu
            // 
            this.mpMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.mpMenu.Controls.Add(this.mtExtra);
            this.mpMenu.Controls.Add(this.mtLogOut);
            this.mpMenu.Controls.Add(this.mtCloseCurrent);
            this.mpMenu.Controls.Add(this.mtSettings);
            this.mpMenu.Controls.Add(this.mtReports);
            this.mpMenu.Controls.Add(this.mtTransaction);
            this.mpMenu.Controls.Add(this.mtMasters);
            this.mpMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.mpMenu.HorizontalScrollbarBarColor = true;
            this.mpMenu.HorizontalScrollbarHighlightOnWheel = false;
            this.mpMenu.HorizontalScrollbarSize = 10;
            this.mpMenu.Location = new System.Drawing.Point(0, 30);
            this.mpMenu.Name = "mpMenu";
            this.mpMenu.Size = new System.Drawing.Size(80, 710);
            this.mpMenu.Style = MetroFramework.MetroColorStyle.Black;
            this.mpMenu.TabIndex = 11;
            this.mpMenu.UseStyleColors = true;
            this.mpMenu.VerticalScrollbarBarColor = true;
            this.mpMenu.VerticalScrollbarHighlightOnWheel = false;
            this.mpMenu.VerticalScrollbarSize = 10;
            // 
            // mtExtra
            // 
            this.mtExtra.ActiveControl = null;
            this.mtExtra.AutoSize = true;
            this.mtExtra.BackColor = System.Drawing.Color.Transparent;
            this.mtExtra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mtExtra.Dock = System.Windows.Forms.DockStyle.Top;
            this.mtExtra.Location = new System.Drawing.Point(0, 642);
            this.mtExtra.Name = "mtExtra";
            this.mtExtra.Size = new System.Drawing.Size(80, 96);
            this.mtExtra.TabIndex = 6;
            this.mtExtra.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mtExtra.UseCustomBackColor = true;
            this.mtExtra.UseCustomForeColor = true;
            this.mtExtra.UseSelectable = true;
            this.mtExtra.UseTileImage = true;
            // 
            // mtLogOut
            // 
            this.mtLogOut.ActiveControl = null;
            this.mtLogOut.AutoSize = true;
            this.mtLogOut.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mtLogOut.Dock = System.Windows.Forms.DockStyle.Top;
            this.mtLogOut.Location = new System.Drawing.Point(0, 535);
            this.mtLogOut.Name = "mtLogOut";
            this.mtLogOut.Size = new System.Drawing.Size(80, 107);
            this.mtLogOut.TabIndex = 5;
            this.mtLogOut.TileImage = ((System.Drawing.Image)(resources.GetObject("mtLogOut.TileImage")));
            this.mtLogOut.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mtLogOut.UseCustomBackColor = true;
            this.mtLogOut.UseCustomForeColor = true;
            this.mtLogOut.UseSelectable = true;
            this.mtLogOut.UseTileImage = true;
            this.mtLogOut.Click += new System.EventHandler(this.MtExit_Click_1);
            // 
            // mtCloseCurrent
            // 
            this.mtCloseCurrent.ActiveControl = null;
            this.mtCloseCurrent.AutoSize = true;
            this.mtCloseCurrent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mtCloseCurrent.Dock = System.Windows.Forms.DockStyle.Top;
            this.mtCloseCurrent.Location = new System.Drawing.Point(0, 428);
            this.mtCloseCurrent.Name = "mtCloseCurrent";
            this.mtCloseCurrent.Size = new System.Drawing.Size(80, 107);
            this.mtCloseCurrent.TabIndex = 4;
            this.mtCloseCurrent.TileImage = ((System.Drawing.Image)(resources.GetObject("mtCloseCurrent.TileImage")));
            this.mtCloseCurrent.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mtCloseCurrent.UseCustomBackColor = true;
            this.mtCloseCurrent.UseCustomForeColor = true;
            this.mtCloseCurrent.UseSelectable = true;
            this.mtCloseCurrent.UseTileImage = true;
            this.mtCloseCurrent.Click += new System.EventHandler(this.MtCloseCurrent_Click);
            this.mtCloseCurrent.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MtCloseCurrent_MouseDown);
            this.mtCloseCurrent.MouseHover += new System.EventHandler(this.MtCloseCurrent_MouseHover);
            this.mtCloseCurrent.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MtCloseCurrent_MouseUp);
            // 
            // mtSettings
            // 
            this.mtSettings.ActiveControl = null;
            this.mtSettings.AutoSize = true;
            this.mtSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mtSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.mtSettings.Location = new System.Drawing.Point(0, 321);
            this.mtSettings.Name = "mtSettings";
            this.mtSettings.Size = new System.Drawing.Size(80, 107);
            this.mtSettings.TabIndex = 3;
            this.mtSettings.TileImage = ((System.Drawing.Image)(resources.GetObject("mtSettings.TileImage")));
            this.mtSettings.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mtSettings.UseCustomBackColor = true;
            this.mtSettings.UseCustomForeColor = true;
            this.mtSettings.UseSelectable = true;
            this.mtSettings.UseTileImage = true;
            this.mtSettings.Click += new System.EventHandler(this.MtSettings_Click);
            // 
            // mtReports
            // 
            this.mtReports.ActiveControl = null;
            this.mtReports.AutoSize = true;
            this.mtReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mtReports.Dock = System.Windows.Forms.DockStyle.Top;
            this.mtReports.Location = new System.Drawing.Point(0, 214);
            this.mtReports.Name = "mtReports";
            this.mtReports.Size = new System.Drawing.Size(80, 107);
            this.mtReports.TabIndex = 2;
            this.mtReports.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtReports.TileImage = ((System.Drawing.Image)(resources.GetObject("mtReports.TileImage")));
            this.mtReports.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mtReports.UseCustomBackColor = true;
            this.mtReports.UseCustomForeColor = true;
            this.mtReports.UseSelectable = true;
            this.mtReports.UseTileImage = true;
            this.mtReports.Click += new System.EventHandler(this.MtReports_Click);
            // 
            // mtTransaction
            // 
            this.mtTransaction.ActiveControl = null;
            this.mtTransaction.AutoSize = true;
            this.mtTransaction.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mtTransaction.Dock = System.Windows.Forms.DockStyle.Top;
            this.mtTransaction.Location = new System.Drawing.Point(0, 107);
            this.mtTransaction.Name = "mtTransaction";
            this.mtTransaction.Size = new System.Drawing.Size(80, 107);
            this.mtTransaction.TabIndex = 1;
            this.mtTransaction.TileImage = ((System.Drawing.Image)(resources.GetObject("mtTransaction.TileImage")));
            this.mtTransaction.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mtTransaction.UseCustomBackColor = true;
            this.mtTransaction.UseCustomForeColor = true;
            this.mtTransaction.UseSelectable = true;
            this.mtTransaction.UseTileImage = true;
            this.mtTransaction.Click += new System.EventHandler(this.MtTransaction_Click);
            // 
            // mtMasters
            // 
            this.mtMasters.ActiveControl = null;
            this.mtMasters.AutoSize = true;
            this.mtMasters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mtMasters.Dock = System.Windows.Forms.DockStyle.Top;
            this.mtMasters.Location = new System.Drawing.Point(0, 0);
            this.mtMasters.Name = "mtMasters";
            this.mtMasters.Size = new System.Drawing.Size(80, 107);
            this.mtMasters.TabIndex = 0;
            this.mtMasters.TileImage = ((System.Drawing.Image)(resources.GetObject("mtMasters.TileImage")));
            this.mtMasters.TileImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mtMasters.UseCustomBackColor = true;
            this.mtMasters.UseCustomForeColor = true;
            this.mtMasters.UseSelectable = true;
            this.mtMasters.UseTileImage = true;
            this.mtMasters.Click += new System.EventHandler(this.MtMasters_Click);
            // 
            // mpMaster
            // 
            this.mpMaster.Controls.Add(this.mtFinancialYear);
            this.mpMaster.Controls.Add(this.mtCompany);
            this.mpMaster.Controls.Add(this.mtAccountGroup);
            this.mpMaster.Controls.Add(this.mtDepartment);
            this.mpMaster.Controls.Add(this.mtDesignation);
            this.mpMaster.Controls.Add(this.myEmployee);
            this.mpMaster.Controls.Add(this.mtUnit);
            this.mpMaster.Controls.Add(this.mtItemClass);
            this.mpMaster.Controls.Add(this.mtCategory);
            this.mpMaster.Controls.Add(this.mtListItem);
            this.mpMaster.Controls.Add(this.pictureBox1);
            this.mpMaster.Controls.Add(this.mtAccountLedger);
            this.mpMaster.HorizontalScrollbarBarColor = true;
            this.mpMaster.HorizontalScrollbarHighlightOnWheel = false;
            this.mpMaster.HorizontalScrollbarSize = 10;
            this.mpMaster.Location = new System.Drawing.Point(82, 30);
            this.mpMaster.Name = "mpMaster";
            this.mpMaster.Size = new System.Drawing.Size(1281, 134);
            this.mpMaster.TabIndex = 12;
            this.mpMaster.Tag = "SubMenu";
            this.mpMaster.UseStyleColors = true;
            this.mpMaster.VerticalScrollbarBarColor = true;
            this.mpMaster.VerticalScrollbarHighlightOnWheel = false;
            this.mpMaster.VerticalScrollbarSize = 10;
            // 
            // mtFinancialYear
            // 
            this.mtFinancialYear.Image = ((System.Drawing.Image)(resources.GetObject("mtFinancialYear.Image")));
            this.mtFinancialYear.Location = new System.Drawing.Point(184, 274);
            this.mtFinancialYear.Name = "mtFinancialYear";
            this.mtFinancialYear.Size = new System.Drawing.Size(118, 107);
            this.mtFinancialYear.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtFinancialYear.TabIndex = 28;
            this.mtFinancialYear.TabStop = false;
            this.mtFinancialYear.Click += new System.EventHandler(this.MtFinancialYear_Click);
            // 
            // mtCompany
            // 
            this.mtCompany.Image = ((System.Drawing.Image)(resources.GetObject("mtCompany.Image")));
            this.mtCompany.Location = new System.Drawing.Point(5, 274);
            this.mtCompany.Name = "mtCompany";
            this.mtCompany.Size = new System.Drawing.Size(118, 107);
            this.mtCompany.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtCompany.TabIndex = 27;
            this.mtCompany.TabStop = false;
            this.mtCompany.Click += new System.EventHandler(this.MtCompany_Click);
            // 
            // mtAccountGroup
            // 
            this.mtAccountGroup.Image = ((System.Drawing.Image)(resources.GetObject("mtAccountGroup.Image")));
            this.mtAccountGroup.Location = new System.Drawing.Point(721, 143);
            this.mtAccountGroup.Name = "mtAccountGroup";
            this.mtAccountGroup.Size = new System.Drawing.Size(118, 107);
            this.mtAccountGroup.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtAccountGroup.TabIndex = 26;
            this.mtAccountGroup.TabStop = false;
            this.mtAccountGroup.Click += new System.EventHandler(this.MtAccountGroup_Click);
            // 
            // mtDepartment
            // 
            this.mtDepartment.Image = ((System.Drawing.Image)(resources.GetObject("mtDepartment.Image")));
            this.mtDepartment.Location = new System.Drawing.Point(542, 143);
            this.mtDepartment.Name = "mtDepartment";
            this.mtDepartment.Size = new System.Drawing.Size(118, 107);
            this.mtDepartment.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtDepartment.TabIndex = 25;
            this.mtDepartment.TabStop = false;
            this.mtDepartment.Click += new System.EventHandler(this.MtDepartment_Click);
            // 
            // mtDesignation
            // 
            this.mtDesignation.Image = ((System.Drawing.Image)(resources.GetObject("mtDesignation.Image")));
            this.mtDesignation.Location = new System.Drawing.Point(363, 143);
            this.mtDesignation.Name = "mtDesignation";
            this.mtDesignation.Size = new System.Drawing.Size(118, 107);
            this.mtDesignation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtDesignation.TabIndex = 24;
            this.mtDesignation.TabStop = false;
            this.mtDesignation.Click += new System.EventHandler(this.MtDesignation_Click);
            // 
            // myEmployee
            // 
            this.myEmployee.Image = ((System.Drawing.Image)(resources.GetObject("myEmployee.Image")));
            this.myEmployee.Location = new System.Drawing.Point(184, 143);
            this.myEmployee.Name = "myEmployee";
            this.myEmployee.Size = new System.Drawing.Size(118, 107);
            this.myEmployee.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.myEmployee.TabIndex = 23;
            this.myEmployee.TabStop = false;
            this.myEmployee.Click += new System.EventHandler(this.MyEmployee_Click);
            // 
            // mtUnit
            // 
            this.mtUnit.Image = global::MarginPOSNew.Properties.Resources.uom1;
            this.mtUnit.Location = new System.Drawing.Point(721, 7);
            this.mtUnit.Name = "mtUnit";
            this.mtUnit.Size = new System.Drawing.Size(118, 107);
            this.mtUnit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtUnit.TabIndex = 22;
            this.mtUnit.TabStop = false;
            this.mtUnit.Click += new System.EventHandler(this.MtUnit_Click);
            // 
            // mtItemClass
            // 
            this.mtItemClass.Image = ((System.Drawing.Image)(resources.GetObject("mtItemClass.Image")));
            this.mtItemClass.Location = new System.Drawing.Point(542, 7);
            this.mtItemClass.Name = "mtItemClass";
            this.mtItemClass.Size = new System.Drawing.Size(118, 107);
            this.mtItemClass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtItemClass.TabIndex = 21;
            this.mtItemClass.TabStop = false;
            this.mtItemClass.Click += new System.EventHandler(this.MtItemClass_Click);
            // 
            // mtCategory
            // 
            this.mtCategory.Image = ((System.Drawing.Image)(resources.GetObject("mtCategory.Image")));
            this.mtCategory.Location = new System.Drawing.Point(363, 7);
            this.mtCategory.Name = "mtCategory";
            this.mtCategory.Size = new System.Drawing.Size(118, 107);
            this.mtCategory.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtCategory.TabIndex = 20;
            this.mtCategory.TabStop = false;
            this.mtCategory.Click += new System.EventHandler(this.MtCategory_Click);
            // 
            // mtListItem
            // 
            this.mtListItem.Image = ((System.Drawing.Image)(resources.GetObject("mtListItem.Image")));
            this.mtListItem.Location = new System.Drawing.Point(5, 7);
            this.mtListItem.Name = "mtListItem";
            this.mtListItem.Size = new System.Drawing.Size(118, 107);
            this.mtListItem.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtListItem.TabIndex = 19;
            this.mtListItem.TabStop = false;
            this.mtListItem.Click += new System.EventHandler(this.MtListItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(184, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(118, 107);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.MtItem_Click);
            // 
            // mtAccountLedger
            // 
            this.mtAccountLedger.Image = ((System.Drawing.Image)(resources.GetObject("mtAccountLedger.Image")));
            this.mtAccountLedger.Location = new System.Drawing.Point(5, 143);
            this.mtAccountLedger.Name = "mtAccountLedger";
            this.mtAccountLedger.Size = new System.Drawing.Size(118, 107);
            this.mtAccountLedger.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtAccountLedger.TabIndex = 17;
            this.mtAccountLedger.TabStop = false;
            this.mtAccountLedger.Click += new System.EventHandler(this.MtAccountLedger_Click);
            // 
            // mpTransaction
            // 
            this.mpTransaction.Controls.Add(this.mtOffer);
            this.mpTransaction.Controls.Add(this.mtOutward);
            this.mpTransaction.Controls.Add(this.mtInward);
            this.mpTransaction.Controls.Add(this.mtSalesPOS);
            this.mpTransaction.Controls.Add(this.mtSalesReturn);
            this.mpTransaction.Controls.Add(this.mtPurchaseReturn);
            this.mpTransaction.Controls.Add(this.mtSales);
            this.mpTransaction.Controls.Add(this.mtPurchase);
            this.mpTransaction.HorizontalScrollbarBarColor = true;
            this.mpTransaction.HorizontalScrollbarHighlightOnWheel = false;
            this.mpTransaction.HorizontalScrollbarSize = 10;
            this.mpTransaction.Location = new System.Drawing.Point(86, 216);
            this.mpTransaction.Name = "mpTransaction";
            this.mpTransaction.Size = new System.Drawing.Size(1281, 179);
            this.mpTransaction.TabIndex = 13;
            this.mpTransaction.Tag = "SubMenu";
            this.mpTransaction.VerticalScrollbarBarColor = true;
            this.mpTransaction.VerticalScrollbarHighlightOnWheel = false;
            this.mpTransaction.VerticalScrollbarSize = 10;
            // 
            // mtOffer
            // 
            this.mtOffer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mtOffer.Image = ((System.Drawing.Image)(resources.GetObject("mtOffer.Image")));
            this.mtOffer.Location = new System.Drawing.Point(917, 9);
            this.mtOffer.Name = "mtOffer";
            this.mtOffer.Size = new System.Drawing.Size(118, 107);
            this.mtOffer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtOffer.TabIndex = 32;
            this.mtOffer.TabStop = false;
            this.mtOffer.Click += new System.EventHandler(this.mtOffer_Click);
            // 
            // mtOutward
            // 
            this.mtOutward.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mtOutward.BackgroundImage")));
            this.mtOutward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mtOutward.Font = new System.Drawing.Font("Aharoni", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtOutward.ForeColor = System.Drawing.Color.Black;
            this.mtOutward.Location = new System.Drawing.Point(774, 8);
            this.mtOutward.Name = "mtOutward";
            this.mtOutward.Size = new System.Drawing.Size(124, 107);
            this.mtOutward.TabIndex = 31;
            this.mtOutward.Text = "Outward";
            this.mtOutward.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.mtOutward.UseVisualStyleBackColor = true;
            this.mtOutward.Click += new System.EventHandler(this.mtOutward_Click);
            // 
            // mtInward
            // 
            this.mtInward.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("mtInward.BackgroundImage")));
            this.mtInward.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mtInward.Font = new System.Drawing.Font("Aharoni", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtInward.ForeColor = System.Drawing.Color.Black;
            this.mtInward.Location = new System.Drawing.Point(639, 8);
            this.mtInward.Name = "mtInward";
            this.mtInward.Size = new System.Drawing.Size(118, 107);
            this.mtInward.TabIndex = 30;
            this.mtInward.Text = "Inward";
            this.mtInward.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.mtInward.UseVisualStyleBackColor = true;
            this.mtInward.Click += new System.EventHandler(this.mtInward_Click);
            // 
            // mtSalesPOS
            // 
            this.mtSalesPOS.Image = global::MarginPOSNew.Properties.Resources.Sal11;
            this.mtSalesPOS.Location = new System.Drawing.Point(1064, 3);
            this.mtSalesPOS.Name = "mtSalesPOS";
            this.mtSalesPOS.Size = new System.Drawing.Size(118, 107);
            this.mtSalesPOS.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtSalesPOS.TabIndex = 29;
            this.mtSalesPOS.TabStop = false;
            this.mtSalesPOS.Visible = false;
            this.mtSalesPOS.Click += new System.EventHandler(this.MtSalesPOS_Click);
            // 
            // mtSalesReturn
            // 
            this.mtSalesReturn.Image = ((System.Drawing.Image)(resources.GetObject("mtSalesReturn.Image")));
            this.mtSalesReturn.Location = new System.Drawing.Point(494, 8);
            this.mtSalesReturn.Name = "mtSalesReturn";
            this.mtSalesReturn.Size = new System.Drawing.Size(118, 107);
            this.mtSalesReturn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtSalesReturn.TabIndex = 10;
            this.mtSalesReturn.TabStop = false;
            this.mtSalesReturn.Click += new System.EventHandler(this.mtSalesReturn_Click);
            // 
            // mtPurchaseReturn
            // 
            this.mtPurchaseReturn.Image = ((System.Drawing.Image)(resources.GetObject("mtPurchaseReturn.Image")));
            this.mtPurchaseReturn.Location = new System.Drawing.Point(332, 8);
            this.mtPurchaseReturn.Name = "mtPurchaseReturn";
            this.mtPurchaseReturn.Size = new System.Drawing.Size(118, 107);
            this.mtPurchaseReturn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtPurchaseReturn.TabIndex = 9;
            this.mtPurchaseReturn.TabStop = false;
            this.mtPurchaseReturn.Click += new System.EventHandler(this.MtPurchaseReturn_Click);
            // 
            // mtSales
            // 
            this.mtSales.Image = ((System.Drawing.Image)(resources.GetObject("mtSales.Image")));
            this.mtSales.Location = new System.Drawing.Point(5, 8);
            this.mtSales.Name = "mtSales";
            this.mtSales.Size = new System.Drawing.Size(118, 107);
            this.mtSales.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtSales.TabIndex = 8;
            this.mtSales.TabStop = false;
            this.mtSales.Click += new System.EventHandler(this.MtSales_Click);
            // 
            // mtPurchase
            // 
            this.mtPurchase.Image = ((System.Drawing.Image)(resources.GetObject("mtPurchase.Image")));
            this.mtPurchase.Location = new System.Drawing.Point(160, 8);
            this.mtPurchase.Name = "mtPurchase";
            this.mtPurchase.Size = new System.Drawing.Size(118, 107);
            this.mtPurchase.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtPurchase.TabIndex = 7;
            this.mtPurchase.TabStop = false;
            this.mtPurchase.Click += new System.EventHandler(this.MtPurchase_Click);
            // 
            // mpReports
            // 
            this.mpReports.Controls.Add(this.mtSalesPosWiseReport);
            this.mpReports.Controls.Add(this.mtSalesReport);
            this.mpReports.Controls.Add(this.mtRptSalesDetails);
            this.mpReports.HorizontalScrollbarBarColor = true;
            this.mpReports.HorizontalScrollbarHighlightOnWheel = false;
            this.mpReports.HorizontalScrollbarSize = 10;
            this.mpReports.Location = new System.Drawing.Point(86, 587);
            this.mpReports.Name = "mpReports";
            this.mpReports.Size = new System.Drawing.Size(1277, 181);
            this.mpReports.TabIndex = 14;
            this.mpReports.Tag = "SubMenu";
            this.mpReports.VerticalScrollbarBarColor = true;
            this.mpReports.VerticalScrollbarHighlightOnWheel = false;
            this.mpReports.VerticalScrollbarSize = 10;
            // 
            // mtSalesPosWiseReport
            // 
            this.mtSalesPosWiseReport.Image = ((System.Drawing.Image)(resources.GetObject("mtSalesPosWiseReport.Image")));
            this.mtSalesPosWiseReport.Location = new System.Drawing.Point(359, 9);
            this.mtSalesPosWiseReport.Name = "mtSalesPosWiseReport";
            this.mtSalesPosWiseReport.Size = new System.Drawing.Size(118, 107);
            this.mtSalesPosWiseReport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtSalesPosWiseReport.TabIndex = 31;
            this.mtSalesPosWiseReport.TabStop = false;
            this.mtSalesPosWiseReport.Click += new System.EventHandler(this.mtSalesPosWiseReport_Click);
            // 
            // mtSalesReport
            // 
            this.mtSalesReport.Image = ((System.Drawing.Image)(resources.GetObject("mtSalesReport.Image")));
            this.mtSalesReport.Location = new System.Drawing.Point(180, 9);
            this.mtSalesReport.Name = "mtSalesReport";
            this.mtSalesReport.Size = new System.Drawing.Size(118, 107);
            this.mtSalesReport.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtSalesReport.TabIndex = 30;
            this.mtSalesReport.TabStop = false;
            this.mtSalesReport.Click += new System.EventHandler(this.mtSalesReport_Click);
            // 
            // mtRptSalesDetails
            // 
            this.mtRptSalesDetails.Image = ((System.Drawing.Image)(resources.GetObject("mtRptSalesDetails.Image")));
            this.mtRptSalesDetails.Location = new System.Drawing.Point(1, 9);
            this.mtRptSalesDetails.Name = "mtRptSalesDetails";
            this.mtRptSalesDetails.Size = new System.Drawing.Size(118, 107);
            this.mtRptSalesDetails.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtRptSalesDetails.TabIndex = 10;
            this.mtRptSalesDetails.TabStop = false;
            this.mtRptSalesDetails.Click += new System.EventHandler(this.mtRptSalesDetails_Click);
            // 
            // mpSettings
            // 
            this.mpSettings.Controls.Add(this.mtUserAccount);
            this.mpSettings.Controls.Add(this.mtUserAccountType);
            this.mpSettings.Controls.Add(this.mtGeneral);
            this.mpSettings.Controls.Add(this.mtGeneralType);
            this.mpSettings.HorizontalScrollbarBarColor = true;
            this.mpSettings.HorizontalScrollbarHighlightOnWheel = false;
            this.mpSettings.HorizontalScrollbarSize = 10;
            this.mpSettings.Location = new System.Drawing.Point(82, 401);
            this.mpSettings.Name = "mpSettings";
            this.mpSettings.Size = new System.Drawing.Size(1281, 180);
            this.mpSettings.TabIndex = 15;
            this.mpSettings.Tag = "SubMenu";
            this.mpSettings.VerticalScrollbarBarColor = true;
            this.mpSettings.VerticalScrollbarHighlightOnWheel = false;
            this.mpSettings.VerticalScrollbarSize = 10;
            // 
            // mtUserAccount
            // 
            this.mtUserAccount.Image = ((System.Drawing.Image)(resources.GetObject("mtUserAccount.Image")));
            this.mtUserAccount.Location = new System.Drawing.Point(542, 8);
            this.mtUserAccount.Name = "mtUserAccount";
            this.mtUserAccount.Size = new System.Drawing.Size(118, 107);
            this.mtUserAccount.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtUserAccount.TabIndex = 12;
            this.mtUserAccount.TabStop = false;
            this.mtUserAccount.Click += new System.EventHandler(this.MtUserAccount_Click);
            // 
            // mtUserAccountType
            // 
            this.mtUserAccountType.Image = ((System.Drawing.Image)(resources.GetObject("mtUserAccountType.Image")));
            this.mtUserAccountType.Location = new System.Drawing.Point(363, 8);
            this.mtUserAccountType.Name = "mtUserAccountType";
            this.mtUserAccountType.Size = new System.Drawing.Size(118, 107);
            this.mtUserAccountType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtUserAccountType.TabIndex = 11;
            this.mtUserAccountType.TabStop = false;
            this.mtUserAccountType.Click += new System.EventHandler(this.MtUserAccountType_Click);
            // 
            // mtGeneral
            // 
            this.mtGeneral.Image = ((System.Drawing.Image)(resources.GetObject("mtGeneral.Image")));
            this.mtGeneral.Location = new System.Drawing.Point(184, 8);
            this.mtGeneral.Name = "mtGeneral";
            this.mtGeneral.Size = new System.Drawing.Size(118, 107);
            this.mtGeneral.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtGeneral.TabIndex = 10;
            this.mtGeneral.TabStop = false;
            this.mtGeneral.Click += new System.EventHandler(this.MtGeneral_Click);
            // 
            // mtGeneralType
            // 
            this.mtGeneralType.Image = ((System.Drawing.Image)(resources.GetObject("mtGeneralType.Image")));
            this.mtGeneralType.Location = new System.Drawing.Point(5, 8);
            this.mtGeneralType.Name = "mtGeneralType";
            this.mtGeneralType.Size = new System.Drawing.Size(118, 107);
            this.mtGeneralType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.mtGeneralType.TabIndex = 9;
            this.mtGeneralType.TabStop = false;
            this.mtGeneralType.Click += new System.EventHandler(this.MtGeneralType_Click);
            // 
            // mlMarginPOSVersion
            // 
            this.mlMarginPOSVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(72)))), ((int)(((byte)(103)))));
            this.mlMarginPOSVersion.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.mlMarginPOSVersion.ForeColor = System.Drawing.Color.White;
            this.mlMarginPOSVersion.Location = new System.Drawing.Point(0, 3);
            this.mlMarginPOSVersion.Name = "mlMarginPOSVersion";
            this.mlMarginPOSVersion.Size = new System.Drawing.Size(235, 26);
            this.mlMarginPOSVersion.TabIndex = 2;
            this.mlMarginPOSVersion.Text = "MarginPOS Version";
            this.mlMarginPOSVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.mlMarginPOSVersion.UseCustomBackColor = true;
            this.mlMarginPOSVersion.UseCustomForeColor = true;
            // 
            // pbclose
            // 
            this.pbclose.BackColor = System.Drawing.Color.Navy;
            this.pbclose.Image = ((System.Drawing.Image)(resources.GetObject("pbclose.Image")));
            this.pbclose.Location = new System.Drawing.Point(1176, 3);
            this.pbclose.Name = "pbclose";
            this.pbclose.Size = new System.Drawing.Size(33, 26);
            this.pbclose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbclose.TabIndex = 29;
            this.pbclose.TabStop = false;
            this.pbclose.Click += new System.EventHandler(this.pbclose_Click);
            // 
            // FrmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 740);
            this.ControlBox = false;
            this.Controls.Add(this.pbclose);
            this.Controls.Add(this.mpMaster);
            this.Controls.Add(this.mpSettings);
            this.Controls.Add(this.mpReports);
            this.Controls.Add(this.mpTransaction);
            this.Controls.Add(this.mpMenu);
            this.Controls.Add(this.mlDateTime);
            this.Controls.Add(this.mlCompanyname);
            this.Controls.Add(this.mlStatus1);
            this.Controls.Add(this.mlMarginPOSVersion);
            this.DisplayHeader = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDashboard";
            this.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.Resizable = false;
            this.TransparencyKey = System.Drawing.Color.Empty;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmDashboard_Load);
            this.mpMenu.ResumeLayout(false);
            this.mpMenu.PerformLayout();
            this.mpMaster.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mtFinancialYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtCompany)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtAccountGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtDepartment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtDesignation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myEmployee)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtItemClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtListItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtAccountLedger)).EndInit();
            this.mpTransaction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mtOffer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtSalesPOS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtSalesReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtPurchaseReturn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtPurchase)).EndInit();
            this.mpReports.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mtSalesPosWiseReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtSalesReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtRptSalesDetails)).EndInit();
            this.mpSettings.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mtUserAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtUserAccountType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtGeneral)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtGeneralType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbclose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer tmrClock;
        private MetroFramework.Controls.MetroLabel mlDateTime;
        private MetroFramework.Controls.MetroLabel mlStatus1;
        private MetroFramework.Controls.MetroLabel mlCompanyname;
        private MetroFramework.Controls.MetroPanel mpMenu;
        private MetroFramework.Controls.MetroTile mtLogOut;
        private MetroFramework.Controls.MetroTile mtCloseCurrent;
        private MetroFramework.Controls.MetroTile mtSettings;
        private MetroFramework.Controls.MetroTile mtReports;
        private MetroFramework.Controls.MetroTile mtTransaction;
        private MetroFramework.Controls.MetroTile mtMasters;
        private MetroFramework.Controls.MetroPanel mpMaster;
        private MetroFramework.Controls.MetroPanel mpTransaction;
        private MetroFramework.Controls.MetroPanel mpReports;
        private MetroFramework.Controls.MetroPanel mpSettings;
        private MetroFramework.Controls.MetroLabel mlMarginPOSVersion;
        private MetroFramework.Controls.MetroTile mtExtra;
        private System.Windows.Forms.PictureBox mtAccountLedger;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox mtPurchase;
        private System.Windows.Forms.PictureBox mtSales;
        private System.Windows.Forms.PictureBox mtPurchaseReturn;
        private System.Windows.Forms.PictureBox mtSalesReturn;
        private System.Windows.Forms.PictureBox mtListItem;
        private System.Windows.Forms.PictureBox mtCategory;
        private System.Windows.Forms.PictureBox mtUnit;
        private System.Windows.Forms.PictureBox mtItemClass;
        private System.Windows.Forms.PictureBox mtAccountGroup;
        private System.Windows.Forms.PictureBox mtDepartment;
        private System.Windows.Forms.PictureBox mtDesignation;
        private System.Windows.Forms.PictureBox myEmployee;
        private System.Windows.Forms.PictureBox mtFinancialYear;
        private System.Windows.Forms.PictureBox mtCompany;
        private System.Windows.Forms.PictureBox mtSalesPOS;
        private System.Windows.Forms.PictureBox mtRptSalesDetails;
        private System.Windows.Forms.PictureBox mtUserAccount;
        private System.Windows.Forms.PictureBox mtUserAccountType;
        private System.Windows.Forms.PictureBox mtGeneral;
        private System.Windows.Forms.PictureBox mtGeneralType;
        private System.Windows.Forms.PictureBox mtSalesReport;
        private System.Windows.Forms.PictureBox mtSalesPosWiseReport;
        private System.Windows.Forms.PictureBox pbclose;
        private System.Windows.Forms.Button mtInward;
        private System.Windows.Forms.Button mtOutward;
        private System.Windows.Forms.PictureBox mtOffer;
    }
}