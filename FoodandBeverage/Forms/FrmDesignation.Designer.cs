namespace FoodandBeverage.Forms
{
    partial class FrmDesignation
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDesignation));
            this.mtxtName = new MetroFramework.Controls.MetroTextBox();
            this.mcmbDepartment = new MetroFramework.Controls.MetroComboBox();
            this.dgvDesignation = new System.Windows.Forms.DataGridView();
            this.dtpWorkingHrs = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.mlblEmpSrlNo = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.metroLabel1 = new System.Windows.Forms.Label();
            this.mbtnClose = new System.Windows.Forms.Button();
            this.mbtnRefresh = new System.Windows.Forms.Button();
            this.mbtnSave = new System.Windows.Forms.Button();
            this.mlblName = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.label23 = new System.Windows.Forms.Label();
            this.btncolse = new System.Windows.Forms.Button();
            this.btnmini = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesignation)).BeginInit();
            this.mlblEmpSrlNo.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            this.SuspendLayout();
            // 
            // mtxtName
            // 
            // 
            // 
            // 
            this.mtxtName.CustomButton.Image = null;
            this.mtxtName.CustomButton.Location = new System.Drawing.Point(179, 1);
            this.mtxtName.CustomButton.Name = "";
            this.mtxtName.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.mtxtName.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtName.CustomButton.TabIndex = 1;
            this.mtxtName.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtName.CustomButton.UseSelectable = true;
            this.mtxtName.CustomButton.Visible = false;
            this.mtxtName.Lines = new string[0];
            this.mtxtName.Location = new System.Drawing.Point(147, 139);
            this.mtxtName.MaxLength = 50;
            this.mtxtName.Name = "mtxtName";
            this.mtxtName.PasswordChar = '\0';
            this.mtxtName.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtName.SelectedText = "";
            this.mtxtName.SelectionLength = 0;
            this.mtxtName.SelectionStart = 0;
            this.mtxtName.ShortcutsEnabled = true;
            this.mtxtName.Size = new System.Drawing.Size(207, 29);
            this.mtxtName.TabIndex = 1;
            this.mtxtName.Tag = "Designation";
            this.mtxtName.UseSelectable = true;
            this.mtxtName.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtName.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbDepartment_KeyDown);
            this.mtxtName.Leave += new System.EventHandler(this.mtxtName_Leave);
            // 
            // mcmbDepartment
            // 
            this.mcmbDepartment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mcmbDepartment.DropDownHeight = 250;
            this.mcmbDepartment.ForeColor = System.Drawing.Color.Black;
            this.mcmbDepartment.FormattingEnabled = true;
            this.mcmbDepartment.IntegralHeight = false;
            this.mcmbDepartment.ItemHeight = 23;
            this.mcmbDepartment.Location = new System.Drawing.Point(147, 65);
            this.mcmbDepartment.Name = "mcmbDepartment";
            this.mcmbDepartment.Size = new System.Drawing.Size(207, 29);
            this.mcmbDepartment.TabIndex = 0;
            this.mcmbDepartment.Tag = "Department";
            this.mcmbDepartment.UseSelectable = true;
            this.mcmbDepartment.SelectedIndexChanged += new System.EventHandler(this.mcmbDepartment_SelectedIndexChanged);
            this.mcmbDepartment.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbDepartment_KeyDown);
            // 
            // dgvDesignation
            // 
            this.dgvDesignation.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.dgvDesignation.AllowUserToAddRows = false;
            this.dgvDesignation.AllowUserToDeleteRows = false;
            this.dgvDesignation.AllowUserToResizeColumns = false;
            this.dgvDesignation.AllowUserToResizeRows = false;
            this.dgvDesignation.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDesignation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvDesignation.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDesignation.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDesignation.ColumnHeadersHeight = 30;
            this.dgvDesignation.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDesignation.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDesignation.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvDesignation.EnableHeadersVisualStyles = false;
            this.dgvDesignation.Location = new System.Drawing.Point(3, 3);
            this.dgvDesignation.MultiSelect = false;
            this.dgvDesignation.Name = "dgvDesignation";
            this.dgvDesignation.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDesignation.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDesignation.RowHeadersVisible = false;
            this.dgvDesignation.RowTemplate.Height = 30;
            this.dgvDesignation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvDesignation.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDesignation.ShowCellErrors = false;
            this.dgvDesignation.ShowCellToolTips = false;
            this.dgvDesignation.ShowEditingIcon = false;
            this.dgvDesignation.Size = new System.Drawing.Size(397, 491);
            this.dgvDesignation.TabIndex = 3;
            this.dgvDesignation.TabStop = false;
            this.dgvDesignation.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvDesignation_CellDoubleClick);
            this.dgvDesignation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DgvDesignation_KeyDown);
            // 
            // dtpWorkingHrs
            // 
            this.dtpWorkingHrs.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpWorkingHrs.CustomFormat = "HH:mm";
            this.dtpWorkingHrs.DropDownAlign = System.Windows.Forms.LeftRightAlignment.Right;
            this.dtpWorkingHrs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpWorkingHrs.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpWorkingHrs.Location = new System.Drawing.Point(281, 203);
            this.dtpWorkingHrs.Name = "dtpWorkingHrs";
            this.dtpWorkingHrs.ShowUpDown = true;
            this.dtpWorkingHrs.Size = new System.Drawing.Size(73, 26);
            this.dtpWorkingHrs.TabIndex = 2;
            this.dtpWorkingHrs.ValueChanged += new System.EventHandler(this.dtpWorkingHrs_ValueChanged);
            this.dtpWorkingHrs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbDepartment_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(126, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(124, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "*";
            // 
            // mlblEmpSrlNo
            // 
            this.mlblEmpSrlNo.BackColor = System.Drawing.Color.AliceBlue;
            this.mlblEmpSrlNo.Controls.Add(this.panel2);
            this.mlblEmpSrlNo.Controls.Add(this.panel1);
            this.mlblEmpSrlNo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mlblEmpSrlNo.Location = new System.Drawing.Point(0, 0);
            this.mlblEmpSrlNo.Name = "mlblEmpSrlNo";
            this.mlblEmpSrlNo.Size = new System.Drawing.Size(794, 557);
            this.mlblEmpSrlNo.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgvDesignation);
            this.panel2.Location = new System.Drawing.Point(391, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(385, 491);
            this.panel2.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.metroLabel1);
            this.panel1.Controls.Add(this.mbtnClose);
            this.panel1.Controls.Add(this.mcmbDepartment);
            this.panel1.Controls.Add(this.mbtnRefresh);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.mbtnSave);
            this.panel1.Controls.Add(this.mlblName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dtpWorkingHrs);
            this.panel1.Controls.Add(this.mtxtName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(0, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(385, 499);
            this.panel1.TabIndex = 19;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroLabel1.ForeColor = System.Drawing.Color.Black;
            this.metroLabel1.Location = new System.Drawing.Point(11, 65);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(127, 25);
            this.metroLabel1.TabIndex = 13;
            this.metroLabel1.Text = "Department :";
            // 
            // mbtnClose
            // 
            this.mbtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnClose.FlatAppearance.BorderSize = 0;
            this.mbtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnClose.Location = new System.Drawing.Point(258, 340);
            this.mbtnClose.Name = "mbtnClose";
            this.mbtnClose.Size = new System.Drawing.Size(77, 23);
            this.mbtnClose.TabIndex = 18;
            this.mbtnClose.UseVisualStyleBackColor = true;
            this.mbtnClose.Click += new System.EventHandler(this.mbtnClose_Click);
            this.mbtnClose.MouseLeave += new System.EventHandler(this.mbtnClose_MouseLeave);
            this.mbtnClose.MouseHover += new System.EventHandler(this.mbtnClose_MouseHover);
            // 
            // mbtnRefresh
            // 
            this.mbtnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnRefresh.FlatAppearance.BorderSize = 0;
            this.mbtnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnRefresh.Location = new System.Drawing.Point(137, 340);
            this.mbtnRefresh.Name = "mbtnRefresh";
            this.mbtnRefresh.Size = new System.Drawing.Size(77, 23);
            this.mbtnRefresh.TabIndex = 17;
            this.mbtnRefresh.UseVisualStyleBackColor = true;
            this.mbtnRefresh.Click += new System.EventHandler(this.MbtnRefresh_Click);
            this.mbtnRefresh.MouseLeave += new System.EventHandler(this.mbtnRefresh_MouseLeave);
            this.mbtnRefresh.MouseHover += new System.EventHandler(this.mbtnRefresh_MouseHover);
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnSave.FlatAppearance.BorderSize = 0;
            this.mbtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnSave.Location = new System.Drawing.Point(16, 340);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(77, 23);
            this.mbtnSave.TabIndex = 16;
            this.mbtnSave.UseVisualStyleBackColor = true;
            this.mbtnSave.Click += new System.EventHandler(this.MbtnSave_Click);
            this.mbtnSave.MouseLeave += new System.EventHandler(this.mbtnSave_MouseLeave);
            this.mbtnSave.MouseHover += new System.EventHandler(this.mbtnClose_MouseHover);
            // 
            // mlblName
            // 
            this.mlblName.AutoSize = true;
            this.mlblName.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mlblName.ForeColor = System.Drawing.Color.Black;
            this.mlblName.Location = new System.Drawing.Point(11, 139);
            this.mlblName.Name = "mlblName";
            this.mlblName.Size = new System.Drawing.Size(125, 25);
            this.mlblName.TabIndex = 14;
            this.mlblName.Text = "Designation :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(124, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 25);
            this.label4.TabIndex = 15;
            this.label4.Text = "Working Hours :";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.SteelBlue;
            this.panel5.Controls.Add(this.pictureBox10);
            this.panel5.Controls.Add(this.label23);
            this.panel5.Controls.Add(this.btncolse);
            this.panel5.Controls.Add(this.btnmini);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(794, 59);
            this.panel5.TabIndex = 276;
            // 
            // pictureBox10
            // 
            this.pictureBox10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox10.Location = new System.Drawing.Point(16, 14);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(38, 27);
            this.pictureBox10.TabIndex = 278;
            this.pictureBox10.TabStop = false;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Elephant", 14.25F, System.Drawing.FontStyle.Bold);
            this.label23.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label23.Location = new System.Drawing.Point(58, 14);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(136, 25);
            this.label23.TabIndex = 277;
            this.label23.Text = "Designation";
            // 
            // btncolse
            // 
            this.btncolse.BackColor = System.Drawing.Color.White;
            this.btncolse.FlatAppearance.BorderSize = 0;
            this.btncolse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncolse.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btncolse.ForeColor = System.Drawing.Color.Red;
            this.btncolse.Location = new System.Drawing.Point(736, 17);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(25, 25);
            this.btncolse.TabIndex = 11;
            this.btncolse.Text = "X";
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // btnmini
            // 
            this.btnmini.BackColor = System.Drawing.Color.White;
            this.btnmini.FlatAppearance.BorderSize = 0;
            this.btnmini.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnmini.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmini.ForeColor = System.Drawing.Color.Black;
            this.btnmini.Location = new System.Drawing.Point(705, 17);
            this.btnmini.Name = "btnmini";
            this.btnmini.Size = new System.Drawing.Size(25, 25);
            this.btnmini.TabIndex = 10;
            this.btnmini.Text = "-";
            this.btnmini.UseVisualStyleBackColor = false;
            this.btnmini.Click += new System.EventHandler(this.btnmini_Click);
            // 
            // FrmDesignation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 557);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.mlblEmpSrlNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDesignation";
            this.Load += new System.EventHandler(this.FrmDesignation_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDesignation_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmDesignation_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDesignation)).EndInit();
            this.mlblEmpSrlNo.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox mtxtName;
        private MetroFramework.Controls.MetroComboBox mcmbDepartment;
        private System.Windows.Forms.DataGridView dgvDesignation;
        private System.Windows.Forms.DateTimePicker dtpWorkingHrs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel mlblEmpSrlNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label mlblName;
        private System.Windows.Forms.Label metroLabel1;
        private System.Windows.Forms.Button mbtnClose;
        private System.Windows.Forms.Button mbtnRefresh;
        private System.Windows.Forms.Button mbtnSave;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.Button btnmini;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        //private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        //private Guna.UI2.WinForms.Guna2Button guna2Button2;
        //private Guna.UI2.WinForms.Guna2GradientTileButton guna2GradientTileButton1;
        //private Guna.UI2.WinForms.Guna2Button guna2Button3;
        //private Guna.UI2.WinForms.Guna2CirclePictureBox guna2CirclePictureBox7;
        //private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        //private Guna.UI2.WinForms.Guna2Button guna2Button1;
        //private Guna.UI2.WinForms.Guna2GradientTileButton guna2GradientTileButton2;
        //private Guna.UI2.WinForms.Guna2Button guna2Button4;
    }
}