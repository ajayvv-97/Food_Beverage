
namespace pLocker.Forms
{
    partial class FrmTaxSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTaxSettings));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnMin = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.mbtnSave = new System.Windows.Forms.Button();
            this.btnrefresh = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.mcmbTaxType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mtxtVatPrcnt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mtxtIgstPrcnt = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.mtxtCgstPrcnt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mtxtSgstPrcnt = new System.Windows.Forms.TextBox();
            this.btncolse = new System.Windows.Forms.Button();
            this.dgvTaxSettings = new System.Windows.Forms.DataGridView();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(4)))), ((int)(((byte)(209)))));
            this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
            this.panel3.Controls.Add(this.btnMin);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(915, 44);
            this.panel3.TabIndex = 1;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // btnMin
            // 
            this.btnMin.BackColor = System.Drawing.Color.Transparent;
            this.btnMin.BackgroundImage = global::pLocker.Properties.Resources.Minimize_1;
            this.btnMin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMin.FlatAppearance.BorderSize = 0;
            this.btnMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMin.ForeColor = System.Drawing.Color.White;
            this.btnMin.Location = new System.Drawing.Point(857, 0);
            this.btnMin.Name = "btnMin";
            this.btnMin.Size = new System.Drawing.Size(27, 30);
            this.btnMin.TabIndex = 171;
            this.btnMin.UseVisualStyleBackColor = false;
            this.btnMin.Click += new System.EventHandler(this.btnMin_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = global::pLocker.Properties.Resources.close_1_button;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Transparent;
            this.button2.Location = new System.Drawing.Point(887, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(26, 30);
            this.button2.TabIndex = 0;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(188)))), ((int)(((byte)(45)))));
            this.label8.Location = new System.Drawing.Point(3, 9);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 25);
            this.label8.TabIndex = 67;
            this.label8.Text = "Tax Settings";
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.BackgroundImage = global::pLocker.Properties.Resources.save_1;
            this.mbtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnSave.FlatAppearance.BorderSize = 0;
            this.mbtnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnSave.Location = new System.Drawing.Point(749, 65);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(132, 44);
            this.mbtnSave.TabIndex = 5;
            this.mbtnSave.UseVisualStyleBackColor = false;
            this.mbtnSave.Click += new System.EventHandler(this.mbtnSave_Click);
            this.mbtnSave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mbtnSave_KeyDown);
            // 
            // btnrefresh
            // 
            this.btnrefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnrefresh.BackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.BackgroundImage = global::pLocker.Properties.Resources.refresh_1;
            this.btnrefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnrefresh.FlatAppearance.BorderSize = 0;
            this.btnrefresh.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnrefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnrefresh.Location = new System.Drawing.Point(749, 123);
            this.btnrefresh.Name = "btnrefresh";
            this.btnrefresh.Size = new System.Drawing.Size(132, 44);
            this.btnrefresh.TabIndex = 6;
            this.btnrefresh.UseVisualStyleBackColor = false;
            this.btnrefresh.Click += new System.EventHandler(this.btnrefresh_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Silver;
            this.label5.Location = new System.Drawing.Point(26, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 25);
            this.label5.TabIndex = 145;
            this.label5.Text = "Tax Type :";
            // 
            // mcmbTaxType
            // 
            this.mcmbTaxType.DropDownHeight = 150;
            this.mcmbTaxType.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mcmbTaxType.FormattingEnabled = true;
            this.mcmbTaxType.IntegralHeight = false;
            this.mcmbTaxType.Items.AddRange(new object[] {
            "GST",
            "VAT"});
            this.mcmbTaxType.Location = new System.Drawing.Point(127, 69);
            this.mcmbTaxType.Name = "mcmbTaxType";
            this.mcmbTaxType.Size = new System.Drawing.Size(107, 33);
            this.mcmbTaxType.TabIndex = 0;
            this.mcmbTaxType.SelectedIndexChanged += new System.EventHandler(this.mcmbTaxType_SelectedIndexChanged);
            this.mcmbTaxType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mcmbTaxType_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(250, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 25);
            this.label1.TabIndex = 147;
            this.label1.Text = "VAT % :";
            // 
            // mtxtVatPrcnt
            // 
            this.mtxtVatPrcnt.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtVatPrcnt.Location = new System.Drawing.Point(343, 73);
            this.mtxtVatPrcnt.Multiline = true;
            this.mtxtVatPrcnt.Name = "mtxtVatPrcnt";
            this.mtxtVatPrcnt.Size = new System.Drawing.Size(140, 29);
            this.mtxtVatPrcnt.TabIndex = 1;
            this.mtxtVatPrcnt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtVatPrcnt_KeyDown);
            this.mtxtVatPrcnt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtVatPrcnt_KeyPress);
            this.mtxtVatPrcnt.Validating += new System.ComponentModel.CancelEventHandler(this.mtxtVatPrcnt_Validating);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(26, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 25);
            this.label2.TabIndex = 149;
            this.label2.Text = "IGST % :";
            // 
            // mtxtIgstPrcnt
            // 
            this.mtxtIgstPrcnt.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtIgstPrcnt.Location = new System.Drawing.Point(127, 130);
            this.mtxtIgstPrcnt.Multiline = true;
            this.mtxtIgstPrcnt.Name = "mtxtIgstPrcnt";
            this.mtxtIgstPrcnt.Size = new System.Drawing.Size(107, 29);
            this.mtxtIgstPrcnt.TabIndex = 2;
            this.mtxtIgstPrcnt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtIgstPrcnt_KeyDown);
            this.mtxtIgstPrcnt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtIgstPrcnt_KeyPress);
            this.mtxtIgstPrcnt.Validating += new System.ComponentModel.CancelEventHandler(this.mtxtIgstPrcnt_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(250, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 25);
            this.label3.TabIndex = 151;
            this.label3.Text = "CGST % :";
            // 
            // mtxtCgstPrcnt
            // 
            this.mtxtCgstPrcnt.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtCgstPrcnt.Location = new System.Drawing.Point(343, 130);
            this.mtxtCgstPrcnt.Multiline = true;
            this.mtxtCgstPrcnt.Name = "mtxtCgstPrcnt";
            this.mtxtCgstPrcnt.Size = new System.Drawing.Size(140, 29);
            this.mtxtCgstPrcnt.TabIndex = 3;
            this.mtxtCgstPrcnt.TextChanged += new System.EventHandler(this.mtxtCgstPrcnt_TextChanged);
            this.mtxtCgstPrcnt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtCgstPrcnt_KeyDown);
            this.mtxtCgstPrcnt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtCgstPrcnt_KeyPress);
            this.mtxtCgstPrcnt.Validating += new System.ComponentModel.CancelEventHandler(this.mtxtCgstPrcnt_Validating);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(497, 133);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 25);
            this.label4.TabIndex = 153;
            this.label4.Text = "SGST % :";
            // 
            // mtxtSgstPrcnt
            // 
            this.mtxtSgstPrcnt.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtxtSgstPrcnt.Location = new System.Drawing.Point(587, 129);
            this.mtxtSgstPrcnt.Multiline = true;
            this.mtxtSgstPrcnt.Name = "mtxtSgstPrcnt";
            this.mtxtSgstPrcnt.Size = new System.Drawing.Size(140, 29);
            this.mtxtSgstPrcnt.TabIndex = 4;
            this.mtxtSgstPrcnt.TextAlignChanged += new System.EventHandler(this.mtxtSgstPrcnt_TextAlignChanged);
            this.mtxtSgstPrcnt.TextChanged += new System.EventHandler(this.mtxtSgstPrcnt_TextChanged);
            this.mtxtSgstPrcnt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtSgstPrcnt_KeyDown);
            this.mtxtSgstPrcnt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtxtSgstPrcnt_KeyPress);
            this.mtxtSgstPrcnt.Validating += new System.ComponentModel.CancelEventHandler(this.mtxtSgstPrcnt_Validating);
            // 
            // btncolse
            // 
            this.btncolse.BackColor = System.Drawing.Color.Transparent;
            this.btncolse.BackgroundImage = global::pLocker.Properties.Resources.close_1;
            this.btncolse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btncolse.FlatAppearance.BorderSize = 0;
            this.btncolse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btncolse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btncolse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btncolse.Location = new System.Drawing.Point(752, 182);
            this.btncolse.Name = "btncolse";
            this.btncolse.Size = new System.Drawing.Size(132, 44);
            this.btncolse.TabIndex = 7;
            this.btncolse.UseVisualStyleBackColor = false;
            this.btncolse.Click += new System.EventHandler(this.btncolse_Click);
            // 
            // dgvTaxSettings
            // 
            this.dgvTaxSettings.AllowUserToAddRows = false;
            this.dgvTaxSettings.AllowUserToDeleteRows = false;
            this.dgvTaxSettings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTaxSettings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTaxSettings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvTaxSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvTaxSettings.ColumnHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTaxSettings.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgvTaxSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvTaxSettings.Location = new System.Drawing.Point(0, 232);
            this.dgvTaxSettings.Name = "dgvTaxSettings";
            this.dgvTaxSettings.ReadOnly = true;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTaxSettings.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvTaxSettings.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvTaxSettings.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTaxSettings.ShowCellErrors = false;
            this.dgvTaxSettings.ShowCellToolTips = false;
            this.dgvTaxSettings.ShowEditingIcon = false;
            this.dgvTaxSettings.Size = new System.Drawing.Size(915, 346);
            this.dgvTaxSettings.TabIndex = 154;
            // 
            // FrmTaxSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(38)))), ((int)(((byte)(58)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(915, 578);
            this.Controls.Add(this.dgvTaxSettings);
            this.Controls.Add(this.btncolse);
            this.Controls.Add(this.mtxtSgstPrcnt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.mtxtCgstPrcnt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.mtxtIgstPrcnt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.mtxtVatPrcnt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mcmbTaxType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnrefresh);
            this.Controls.Add(this.mbtnSave);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTaxSettings";
            this.Text = "FrmTaxSettings";
            this.Load += new System.EventHandler(this.FrmTaxSettings_Load_1);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaxSettings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnMin;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button mbtnSave;
        private System.Windows.Forms.Button btnrefresh;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox mcmbTaxType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox mtxtVatPrcnt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox mtxtIgstPrcnt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox mtxtCgstPrcnt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox mtxtSgstPrcnt;
        private System.Windows.Forms.Button btncolse;
        private System.Windows.Forms.DataGridView dgvTaxSettings;
    }
}