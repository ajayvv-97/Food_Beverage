namespace FoodandBeverage.Forms.UserControls
{
    partial class FrmDataGridView<T>
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
			this.dgvSearchGrid = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.dgvSearchGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// dgvSearchGrid
			// 
			this.dgvSearchGrid.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.dgvSearchGrid.AllowUserToAddRows = false;
			this.dgvSearchGrid.AllowUserToDeleteRows = false;
			this.dgvSearchGrid.AllowUserToResizeColumns = false;
			this.dgvSearchGrid.AllowUserToResizeRows = false;
			this.dgvSearchGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvSearchGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.dgvSearchGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(126)))), ((int)(((byte)(227)))));
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dgvSearchGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dgvSearchGrid.ColumnHeadersHeight = 40;
			this.dgvSearchGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvSearchGrid.DefaultCellStyle = dataGridViewCellStyle2;
			this.dgvSearchGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
			this.dgvSearchGrid.EnableHeadersVisualStyles = false;
			this.dgvSearchGrid.Location = new System.Drawing.Point(12, 12);
			this.dgvSearchGrid.MultiSelect = false;
			this.dgvSearchGrid.Name = "dgvSearchGrid";
			this.dgvSearchGrid.ReadOnly = true;
			this.dgvSearchGrid.RowHeadersVisible = false;
			this.dgvSearchGrid.RowTemplate.Height = 35;
			this.dgvSearchGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dgvSearchGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgvSearchGrid.ShowCellErrors = false;
			this.dgvSearchGrid.ShowCellToolTips = false;
			this.dgvSearchGrid.ShowEditingIcon = false;
			this.dgvSearchGrid.Size = new System.Drawing.Size(100, 75);
			this.dgvSearchGrid.TabIndex = 1;
			this.dgvSearchGrid.TabStop = false;
			// 
			// FrmDataGridView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(124, 99);
			this.Controls.Add(this.dgvSearchGrid);
			this.Name = "FrmDataGridView";
			this.Text = "FrmDataGridView";
			((System.ComponentModel.ISupportInitialize)(this.dgvSearchGrid)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvSearchGrid;
    }
}