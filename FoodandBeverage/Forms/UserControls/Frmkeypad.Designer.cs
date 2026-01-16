
namespace FoodandBeverage.Forms.UserControl
{
    partial class Frmkeypad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frmkeypad));
            this.btndel = new System.Windows.Forms.Button();
            this.txtkeypadno = new System.Windows.Forms.TextBox();
            this.pnlnumberpad = new System.Windows.Forms.Panel();
            this.pb_midaslogo = new System.Windows.Forms.PictureBox();
            this.btnsubmit = new System.Windows.Forms.Button();
            this.btn_closekeypad = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_0 = new System.Windows.Forms.Button();
            this.btn_3 = new System.Windows.Forms.Button();
            this.btn_2 = new System.Windows.Forms.Button();
            this.btn_1 = new System.Windows.Forms.Button();
            this.btn_6 = new System.Windows.Forms.Button();
            this.btn_5 = new System.Windows.Forms.Button();
            this.btn_9 = new System.Windows.Forms.Button();
            this.btn_8 = new System.Windows.Forms.Button();
            this.btn_7 = new System.Windows.Forms.Button();
            this.btn_4 = new System.Windows.Forms.Button();
            this.pnlnumberpad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_midaslogo)).BeginInit();
            this.SuspendLayout();
            // 
            // btndel
            // 
            this.btndel.BackColor = System.Drawing.Color.Transparent;
            this.btndel.BackgroundImage = global::FoodandBeverage.Properties.Resources.blank;
            this.btndel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btndel.FlatAppearance.BorderSize = 0;
            this.btndel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btndel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btndel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndel.ForeColor = System.Drawing.Color.White;
            this.btndel.Location = new System.Drawing.Point(241, 359);
            this.btndel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btndel.Name = "btndel";
            this.btndel.Size = new System.Drawing.Size(107, 98);
            this.btndel.TabIndex = 19;
            this.btndel.Text = "DEL";
            this.btndel.UseVisualStyleBackColor = false;
            this.btndel.Click += new System.EventHandler(this.btndel_Click);
            this.btndel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btndel_MouseDown);
            this.btndel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btndel_MouseUp);
            // 
            // txtkeypadno
            // 
            this.txtkeypadno.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtkeypadno.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtkeypadno.Location = new System.Drawing.Point(4, 5);
            this.txtkeypadno.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtkeypadno.Name = "txtkeypadno";
            this.txtkeypadno.Size = new System.Drawing.Size(341, 50);
            this.txtkeypadno.TabIndex = 18;
            this.txtkeypadno.Text = "00-0000-0000";
            this.txtkeypadno.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtkeypadno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtkeypadno_KeyDown);
            this.txtkeypadno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtkeypadno_KeyPress);
            // 
            // pnlnumberpad
            // 
            this.pnlnumberpad.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlnumberpad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlnumberpad.Controls.Add(this.btnsubmit);
            this.pnlnumberpad.Controls.Add(this.btndel);
            this.pnlnumberpad.Controls.Add(this.txtkeypadno);
            this.pnlnumberpad.Controls.Add(this.btn_closekeypad);
            this.pnlnumberpad.Controls.Add(this.btn_clear);
            this.pnlnumberpad.Controls.Add(this.btn_0);
            this.pnlnumberpad.Controls.Add(this.btn_3);
            this.pnlnumberpad.Controls.Add(this.btn_2);
            this.pnlnumberpad.Controls.Add(this.btn_1);
            this.pnlnumberpad.Controls.Add(this.btn_6);
            this.pnlnumberpad.Controls.Add(this.btn_5);
            this.pnlnumberpad.Controls.Add(this.btn_9);
            this.pnlnumberpad.Controls.Add(this.btn_8);
            this.pnlnumberpad.Controls.Add(this.btn_7);
            this.pnlnumberpad.Controls.Add(this.btn_4);
            this.pnlnumberpad.Location = new System.Drawing.Point(12, 33);
            this.pnlnumberpad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlnumberpad.Name = "pnlnumberpad";
            this.pnlnumberpad.Size = new System.Drawing.Size(355, 528);
            this.pnlnumberpad.TabIndex = 62;
            // 
            // pb_midaslogo
            // 
            this.pb_midaslogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_midaslogo.Location = new System.Drawing.Point(43, 2);
            this.pb_midaslogo.Margin = new System.Windows.Forms.Padding(4);
            this.pb_midaslogo.Name = "pb_midaslogo";
            this.pb_midaslogo.Size = new System.Drawing.Size(55, 26);
            this.pb_midaslogo.TabIndex = 63;
            this.pb_midaslogo.TabStop = false;
            // 
            // btnsubmit
            // 
            this.btnsubmit.BackgroundImage = global::FoodandBeverage.Properties.Resources.Ok_2;
            this.btnsubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnsubmit.FlatAppearance.BorderSize = 0;
            this.btnsubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnsubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsubmit.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnsubmit.Location = new System.Drawing.Point(177, 463);
            this.btnsubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(136, 58);
            this.btnsubmit.TabIndex = 20;
            this.btnsubmit.Text = "Ok";
            this.btnsubmit.UseVisualStyleBackColor = true;
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // btn_closekeypad
            // 
            this.btn_closekeypad.BackgroundImage = global::FoodandBeverage.Properties.Resources.N_CANCEL;
            this.btn_closekeypad.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_closekeypad.FlatAppearance.BorderSize = 0;
            this.btn_closekeypad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_closekeypad.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_closekeypad.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_closekeypad.Location = new System.Drawing.Point(28, 463);
            this.btn_closekeypad.Margin = new System.Windows.Forms.Padding(4);
            this.btn_closekeypad.Name = "btn_closekeypad";
            this.btn_closekeypad.Size = new System.Drawing.Size(136, 58);
            this.btn_closekeypad.TabIndex = 17;
            this.btn_closekeypad.UseVisualStyleBackColor = true;
            this.btn_closekeypad.Click += new System.EventHandler(this.btn_closekeypad_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.BackColor = System.Drawing.Color.Transparent;
            this.btn_clear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_clear.BackgroundImage")));
            this.btn_clear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_clear.FlatAppearance.BorderSize = 0;
            this.btn_clear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_clear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_clear.ForeColor = System.Drawing.Color.White;
            this.btn_clear.Location = new System.Drawing.Point(123, 359);
            this.btn_clear.Margin = new System.Windows.Forms.Padding(4);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(107, 98);
            this.btn_clear.TabIndex = 15;
            this.btn_clear.Text = "C";
            this.btn_clear.UseVisualStyleBackColor = false;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            this.btn_clear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_clear_MouseDown);
            this.btn_clear.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_clear_MouseUp);
            // 
            // btn_0
            // 
            this.btn_0.BackColor = System.Drawing.Color.Transparent;
            this.btn_0.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_0.BackgroundImage")));
            this.btn_0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_0.FlatAppearance.BorderSize = 0;
            this.btn_0.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_0.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_0.ForeColor = System.Drawing.Color.White;
            this.btn_0.Location = new System.Drawing.Point(4, 359);
            this.btn_0.Margin = new System.Windows.Forms.Padding(4);
            this.btn_0.Name = "btn_0";
            this.btn_0.Size = new System.Drawing.Size(107, 98);
            this.btn_0.TabIndex = 14;
            this.btn_0.Text = "0";
            this.btn_0.UseVisualStyleBackColor = false;
            this.btn_0.Click += new System.EventHandler(this.btn_0_Click);
            this.btn_0.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_0_MouseDown);
            this.btn_0.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_0_MouseUp);
            // 
            // btn_3
            // 
            this.btn_3.BackColor = System.Drawing.Color.Transparent;
            this.btn_3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_3.BackgroundImage")));
            this.btn_3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_3.FlatAppearance.BorderSize = 0;
            this.btn_3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_3.ForeColor = System.Drawing.Color.White;
            this.btn_3.Location = new System.Drawing.Point(241, 261);
            this.btn_3.Margin = new System.Windows.Forms.Padding(4);
            this.btn_3.Name = "btn_3";
            this.btn_3.Size = new System.Drawing.Size(107, 98);
            this.btn_3.TabIndex = 11;
            this.btn_3.Text = "3";
            this.btn_3.UseVisualStyleBackColor = false;
            this.btn_3.Click += new System.EventHandler(this.btn_3_Click);
            this.btn_3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_3_MouseDown);
            this.btn_3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_3_MouseUp);
            // 
            // btn_2
            // 
            this.btn_2.BackColor = System.Drawing.Color.Transparent;
            this.btn_2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_2.BackgroundImage")));
            this.btn_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_2.FlatAppearance.BorderSize = 0;
            this.btn_2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_2.ForeColor = System.Drawing.Color.White;
            this.btn_2.Location = new System.Drawing.Point(123, 261);
            this.btn_2.Margin = new System.Windows.Forms.Padding(4);
            this.btn_2.Name = "btn_2";
            this.btn_2.Size = new System.Drawing.Size(107, 98);
            this.btn_2.TabIndex = 10;
            this.btn_2.Text = "2";
            this.btn_2.UseVisualStyleBackColor = false;
            this.btn_2.Click += new System.EventHandler(this.btn_2_Click);
            this.btn_2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_2_MouseDown);
            this.btn_2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_2_MouseUp);
            // 
            // btn_1
            // 
            this.btn_1.BackColor = System.Drawing.Color.Transparent;
            this.btn_1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_1.BackgroundImage")));
            this.btn_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_1.FlatAppearance.BorderSize = 0;
            this.btn_1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_1.ForeColor = System.Drawing.Color.White;
            this.btn_1.Location = new System.Drawing.Point(4, 261);
            this.btn_1.Margin = new System.Windows.Forms.Padding(4);
            this.btn_1.Name = "btn_1";
            this.btn_1.Size = new System.Drawing.Size(107, 98);
            this.btn_1.TabIndex = 9;
            this.btn_1.Text = "1";
            this.btn_1.UseVisualStyleBackColor = false;
            this.btn_1.Click += new System.EventHandler(this.btn_1_Click);
            this.btn_1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_1_MouseDown);
            this.btn_1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_1_MouseUp);
            // 
            // btn_6
            // 
            this.btn_6.BackColor = System.Drawing.Color.Transparent;
            this.btn_6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_6.BackgroundImage")));
            this.btn_6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_6.FlatAppearance.BorderSize = 0;
            this.btn_6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_6.ForeColor = System.Drawing.Color.White;
            this.btn_6.Location = new System.Drawing.Point(241, 162);
            this.btn_6.Margin = new System.Windows.Forms.Padding(4);
            this.btn_6.Name = "btn_6";
            this.btn_6.Size = new System.Drawing.Size(107, 98);
            this.btn_6.TabIndex = 7;
            this.btn_6.Text = "6";
            this.btn_6.UseVisualStyleBackColor = false;
            this.btn_6.Click += new System.EventHandler(this.btn_6_Click);
            this.btn_6.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_6_MouseDown);
            this.btn_6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_6_MouseUp);
            // 
            // btn_5
            // 
            this.btn_5.BackColor = System.Drawing.Color.Transparent;
            this.btn_5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_5.BackgroundImage")));
            this.btn_5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_5.FlatAppearance.BorderSize = 0;
            this.btn_5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_5.ForeColor = System.Drawing.Color.White;
            this.btn_5.Location = new System.Drawing.Point(123, 162);
            this.btn_5.Margin = new System.Windows.Forms.Padding(4);
            this.btn_5.Name = "btn_5";
            this.btn_5.Size = new System.Drawing.Size(107, 98);
            this.btn_5.TabIndex = 6;
            this.btn_5.Text = "5";
            this.btn_5.UseVisualStyleBackColor = false;
            this.btn_5.Click += new System.EventHandler(this.btn_5_Click);
            this.btn_5.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_5_MouseDown);
            this.btn_5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_5_MouseUp);
            // 
            // btn_9
            // 
            this.btn_9.BackColor = System.Drawing.Color.Transparent;
            this.btn_9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_9.BackgroundImage")));
            this.btn_9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_9.FlatAppearance.BorderSize = 0;
            this.btn_9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_9.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_9.ForeColor = System.Drawing.Color.White;
            this.btn_9.Location = new System.Drawing.Point(241, 64);
            this.btn_9.Margin = new System.Windows.Forms.Padding(4);
            this.btn_9.Name = "btn_9";
            this.btn_9.Size = new System.Drawing.Size(107, 98);
            this.btn_9.TabIndex = 3;
            this.btn_9.Text = "9";
            this.btn_9.UseVisualStyleBackColor = false;
            this.btn_9.Click += new System.EventHandler(this.btn_9_Click);
            this.btn_9.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_9_MouseDown);
            this.btn_9.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_9_MouseUp);
            // 
            // btn_8
            // 
            this.btn_8.BackColor = System.Drawing.Color.Transparent;
            this.btn_8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_8.BackgroundImage")));
            this.btn_8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_8.FlatAppearance.BorderSize = 0;
            this.btn_8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_8.ForeColor = System.Drawing.Color.White;
            this.btn_8.Location = new System.Drawing.Point(123, 64);
            this.btn_8.Margin = new System.Windows.Forms.Padding(4);
            this.btn_8.Name = "btn_8";
            this.btn_8.Size = new System.Drawing.Size(107, 98);
            this.btn_8.TabIndex = 2;
            this.btn_8.Text = "8";
            this.btn_8.UseVisualStyleBackColor = false;
            this.btn_8.Click += new System.EventHandler(this.btn_8_Click);
            this.btn_8.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_8_MouseDown);
            this.btn_8.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_8_MouseUp);
            // 
            // btn_7
            // 
            this.btn_7.BackColor = System.Drawing.Color.Transparent;
            this.btn_7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_7.BackgroundImage")));
            this.btn_7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_7.FlatAppearance.BorderSize = 0;
            this.btn_7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_7.ForeColor = System.Drawing.Color.White;
            this.btn_7.Location = new System.Drawing.Point(4, 64);
            this.btn_7.Margin = new System.Windows.Forms.Padding(4);
            this.btn_7.Name = "btn_7";
            this.btn_7.Size = new System.Drawing.Size(107, 98);
            this.btn_7.TabIndex = 1;
            this.btn_7.Text = "7";
            this.btn_7.UseVisualStyleBackColor = false;
            this.btn_7.Click += new System.EventHandler(this.btn_7_Click);
            this.btn_7.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_7_MouseDown);
            this.btn_7.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_7_MouseUp);
            // 
            // btn_4
            // 
            this.btn_4.BackColor = System.Drawing.Color.Transparent;
            this.btn_4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_4.BackgroundImage")));
            this.btn_4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_4.FlatAppearance.BorderSize = 0;
            this.btn_4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_4.ForeColor = System.Drawing.Color.White;
            this.btn_4.Location = new System.Drawing.Point(4, 162);
            this.btn_4.Margin = new System.Windows.Forms.Padding(4);
            this.btn_4.Name = "btn_4";
            this.btn_4.Size = new System.Drawing.Size(107, 98);
            this.btn_4.TabIndex = 5;
            this.btn_4.Text = "4";
            this.btn_4.UseVisualStyleBackColor = false;
            this.btn_4.Click += new System.EventHandler(this.btn_4_Click);
            this.btn_4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_4_MouseDown);
            this.btn_4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_4_MouseUp);
            // 
            // Frmkeypad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 592);
            this.Controls.Add(this.pb_midaslogo);
            this.Controls.Add(this.pnlnumberpad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Frmkeypad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "23";
            this.Text = "Frmkeypad";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frmkeypad_FormClosing);
            this.Load += new System.EventHandler(this.Frmkeypad_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Frmkeypad_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Frmkeypad_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Frmkeypad_MouseUp);
            this.pnlnumberpad.ResumeLayout(false);
            this.pnlnumberpad.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_midaslogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_midaslogo;
        private System.Windows.Forms.Button btnsubmit;
        private System.Windows.Forms.Button btndel;
        private System.Windows.Forms.TextBox txtkeypadno;
        private System.Windows.Forms.Button btn_closekeypad;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_0;
        private System.Windows.Forms.Button btn_3;
        private System.Windows.Forms.Button btn_1;
        private System.Windows.Forms.Button btn_6;
        private System.Windows.Forms.Button btn_5;
        private System.Windows.Forms.Button btn_9;
        private System.Windows.Forms.Button btn_8;
        private System.Windows.Forms.Button btn_7;
        private System.Windows.Forms.Button btn_4;
        private System.Windows.Forms.Panel pnlnumberpad;
        private System.Windows.Forms.Button btn_2;
    }
}