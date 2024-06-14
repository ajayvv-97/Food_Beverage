
namespace FoodandBeverage.Forms.UserAccount
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
            this.txtkeypadno = new System.Windows.Forms.TextBox();
            this.tlp_main = new System.Windows.Forms.TableLayoutPanel();
            this.tlp_keynumber = new System.Windows.Forms.TableLayoutPanel();
            this.btn_7 = new System.Windows.Forms.Button();
            this.btn_8 = new System.Windows.Forms.Button();
            this.btndel = new System.Windows.Forms.Button();
            this.btn_9 = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_4 = new System.Windows.Forms.Button();
            this.btn_0 = new System.Windows.Forms.Button();
            this.btn_5 = new System.Windows.Forms.Button();
            this.btn_3 = new System.Windows.Forms.Button();
            this.btn_6 = new System.Windows.Forms.Button();
            this.btn_2 = new System.Windows.Forms.Button();
            this.btn_1 = new System.Windows.Forms.Button();
            this.tlp_header = new System.Windows.Forms.TableLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.tlp_save_cls_button = new System.Windows.Forms.TableLayoutPanel();
            this.mbtnClose = new System.Windows.Forms.Button();
            this.mbtnSave = new System.Windows.Forms.Button();
            this.tlp_main.SuspendLayout();
            this.tlp_keynumber.SuspendLayout();
            this.tlp_header.SuspendLayout();
            this.tlp_save_cls_button.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtkeypadno
            // 
            this.txtkeypadno.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtkeypadno.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtkeypadno.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtkeypadno.Location = new System.Drawing.Point(3, 3);
            this.txtkeypadno.Name = "txtkeypadno";
            this.txtkeypadno.Size = new System.Drawing.Size(235, 40);
            this.txtkeypadno.TabIndex = 18;
            this.txtkeypadno.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtkeypadno_KeyDown);
            this.txtkeypadno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtkeypadno_KeyPress);
            // 
            // tlp_main
            // 
            this.tlp_main.BackColor = System.Drawing.Color.SteelBlue;
            this.tlp_main.ColumnCount = 1;
            this.tlp_main.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_main.Controls.Add(this.tlp_keynumber, 0, 1);
            this.tlp_main.Controls.Add(this.tlp_header, 0, 0);
            this.tlp_main.Controls.Add(this.tlp_save_cls_button, 0, 2);
            this.tlp_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_main.Location = new System.Drawing.Point(5, 5);
            this.tlp_main.Name = "tlp_main";
            this.tlp_main.RowCount = 3;
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.22751F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.77248F));
            this.tlp_main.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tlp_main.Size = new System.Drawing.Size(282, 372);
            this.tlp_main.TabIndex = 60;
            // 
            // tlp_keynumber
            // 
            this.tlp_keynumber.ColumnCount = 3;
            this.tlp_keynumber.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_keynumber.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_keynumber.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 94F));
            this.tlp_keynumber.Controls.Add(this.btn_7, 0, 0);
            this.tlp_keynumber.Controls.Add(this.btn_8, 1, 0);
            this.tlp_keynumber.Controls.Add(this.btndel, 2, 3);
            this.tlp_keynumber.Controls.Add(this.btn_9, 2, 0);
            this.tlp_keynumber.Controls.Add(this.btn_clear, 1, 3);
            this.tlp_keynumber.Controls.Add(this.btn_4, 0, 1);
            this.tlp_keynumber.Controls.Add(this.btn_0, 0, 3);
            this.tlp_keynumber.Controls.Add(this.btn_5, 1, 1);
            this.tlp_keynumber.Controls.Add(this.btn_3, 2, 2);
            this.tlp_keynumber.Controls.Add(this.btn_6, 2, 1);
            this.tlp_keynumber.Controls.Add(this.btn_2, 1, 2);
            this.tlp_keynumber.Controls.Add(this.btn_1, 0, 2);
            this.tlp_keynumber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_keynumber.Location = new System.Drawing.Point(3, 46);
            this.tlp_keynumber.Name = "tlp_keynumber";
            this.tlp_keynumber.RowCount = 4;
            this.tlp_keynumber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp_keynumber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp_keynumber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp_keynumber.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tlp_keynumber.Size = new System.Drawing.Size(276, 280);
            this.tlp_keynumber.TabIndex = 0;
            // 
            // btn_7
            // 
            this.btn_7.BackColor = System.Drawing.Color.Transparent;
            this.btn_7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_7.BackgroundImage")));
            this.btn_7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_7.FlatAppearance.BorderSize = 0;
            this.btn_7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_7.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_7.ForeColor = System.Drawing.Color.White;
            this.btn_7.Location = new System.Drawing.Point(3, 3);
            this.btn_7.Name = "btn_7";
            this.btn_7.Size = new System.Drawing.Size(85, 64);
            this.btn_7.TabIndex = 1;
            this.btn_7.Text = "7";
            this.btn_7.UseVisualStyleBackColor = false;
            this.btn_7.Click += new System.EventHandler(this.btn_7_Click);
            // 
            // btn_8
            // 
            this.btn_8.BackColor = System.Drawing.Color.Transparent;
            this.btn_8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_8.BackgroundImage")));
            this.btn_8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_8.FlatAppearance.BorderSize = 0;
            this.btn_8.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_8.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_8.ForeColor = System.Drawing.Color.White;
            this.btn_8.Location = new System.Drawing.Point(94, 3);
            this.btn_8.Name = "btn_8";
            this.btn_8.Size = new System.Drawing.Size(85, 64);
            this.btn_8.TabIndex = 2;
            this.btn_8.Text = "8";
            this.btn_8.UseVisualStyleBackColor = false;
            this.btn_8.Click += new System.EventHandler(this.btn_8_Click);
            // 
            // btndel
            // 
            this.btndel.BackColor = System.Drawing.Color.Transparent;
            this.btndel.BackgroundImage = global::FoodandBeverage.Properties.Resources.keypadbutton;
            this.btndel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btndel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btndel.FlatAppearance.BorderSize = 0;
            this.btndel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btndel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btndel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btndel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndel.ForeColor = System.Drawing.Color.White;
            this.btndel.Location = new System.Drawing.Point(185, 213);
            this.btndel.Name = "btndel";
            this.btndel.Size = new System.Drawing.Size(88, 64);
            this.btndel.TabIndex = 19;
            this.btndel.Text = "DEL";
            this.btndel.UseVisualStyleBackColor = false;
            this.btndel.Click += new System.EventHandler(this.btndel_Click);
            // 
            // btn_9
            // 
            this.btn_9.BackColor = System.Drawing.Color.Transparent;
            this.btn_9.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_9.BackgroundImage")));
            this.btn_9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_9.FlatAppearance.BorderSize = 0;
            this.btn_9.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_9.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_9.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_9.ForeColor = System.Drawing.Color.White;
            this.btn_9.Location = new System.Drawing.Point(185, 3);
            this.btn_9.Name = "btn_9";
            this.btn_9.Size = new System.Drawing.Size(88, 64);
            this.btn_9.TabIndex = 3;
            this.btn_9.Text = "9";
            this.btn_9.UseVisualStyleBackColor = false;
            this.btn_9.Click += new System.EventHandler(this.btn_9_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.BackColor = System.Drawing.Color.Transparent;
            this.btn_clear.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_clear.BackgroundImage")));
            this.btn_clear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_clear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_clear.FlatAppearance.BorderSize = 0;
            this.btn_clear.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_clear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_clear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_clear.ForeColor = System.Drawing.Color.White;
            this.btn_clear.Location = new System.Drawing.Point(94, 213);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(85, 64);
            this.btn_clear.TabIndex = 15;
            this.btn_clear.Text = "C";
            this.btn_clear.UseVisualStyleBackColor = false;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_4
            // 
            this.btn_4.BackColor = System.Drawing.Color.Transparent;
            this.btn_4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_4.BackgroundImage")));
            this.btn_4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_4.FlatAppearance.BorderSize = 0;
            this.btn_4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_4.ForeColor = System.Drawing.Color.White;
            this.btn_4.Location = new System.Drawing.Point(3, 73);
            this.btn_4.Name = "btn_4";
            this.btn_4.Size = new System.Drawing.Size(85, 64);
            this.btn_4.TabIndex = 5;
            this.btn_4.Text = "4";
            this.btn_4.UseVisualStyleBackColor = false;
            this.btn_4.Click += new System.EventHandler(this.btn_4_Click);
            // 
            // btn_0
            // 
            this.btn_0.BackColor = System.Drawing.Color.Transparent;
            this.btn_0.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_0.BackgroundImage")));
            this.btn_0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_0.FlatAppearance.BorderSize = 0;
            this.btn_0.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_0.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_0.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_0.ForeColor = System.Drawing.Color.White;
            this.btn_0.Location = new System.Drawing.Point(3, 213);
            this.btn_0.Name = "btn_0";
            this.btn_0.Size = new System.Drawing.Size(85, 64);
            this.btn_0.TabIndex = 14;
            this.btn_0.Text = "0";
            this.btn_0.UseVisualStyleBackColor = false;
            this.btn_0.Click += new System.EventHandler(this.btn_0_Click);
            // 
            // btn_5
            // 
            this.btn_5.BackColor = System.Drawing.Color.Transparent;
            this.btn_5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_5.BackgroundImage")));
            this.btn_5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_5.FlatAppearance.BorderSize = 0;
            this.btn_5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_5.ForeColor = System.Drawing.Color.White;
            this.btn_5.Location = new System.Drawing.Point(94, 73);
            this.btn_5.Name = "btn_5";
            this.btn_5.Size = new System.Drawing.Size(85, 64);
            this.btn_5.TabIndex = 6;
            this.btn_5.Text = "5";
            this.btn_5.UseVisualStyleBackColor = false;
            this.btn_5.Click += new System.EventHandler(this.btn_5_Click);
            // 
            // btn_3
            // 
            this.btn_3.BackColor = System.Drawing.Color.Transparent;
            this.btn_3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_3.BackgroundImage")));
            this.btn_3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_3.FlatAppearance.BorderSize = 0;
            this.btn_3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_3.ForeColor = System.Drawing.Color.White;
            this.btn_3.Location = new System.Drawing.Point(185, 143);
            this.btn_3.Name = "btn_3";
            this.btn_3.Size = new System.Drawing.Size(88, 64);
            this.btn_3.TabIndex = 11;
            this.btn_3.Text = "3";
            this.btn_3.UseVisualStyleBackColor = false;
            this.btn_3.Click += new System.EventHandler(this.btn_3_Click);
            // 
            // btn_6
            // 
            this.btn_6.BackColor = System.Drawing.Color.Transparent;
            this.btn_6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_6.BackgroundImage")));
            this.btn_6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_6.FlatAppearance.BorderSize = 0;
            this.btn_6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_6.ForeColor = System.Drawing.Color.White;
            this.btn_6.Location = new System.Drawing.Point(185, 73);
            this.btn_6.Name = "btn_6";
            this.btn_6.Size = new System.Drawing.Size(88, 64);
            this.btn_6.TabIndex = 7;
            this.btn_6.Text = "6";
            this.btn_6.UseVisualStyleBackColor = false;
            this.btn_6.Click += new System.EventHandler(this.btn_6_Click);
            // 
            // btn_2
            // 
            this.btn_2.BackColor = System.Drawing.Color.Transparent;
            this.btn_2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_2.BackgroundImage")));
            this.btn_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_2.FlatAppearance.BorderSize = 0;
            this.btn_2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_2.ForeColor = System.Drawing.Color.White;
            this.btn_2.Location = new System.Drawing.Point(94, 143);
            this.btn_2.Name = "btn_2";
            this.btn_2.Size = new System.Drawing.Size(85, 64);
            this.btn_2.TabIndex = 10;
            this.btn_2.Text = "2";
            this.btn_2.UseVisualStyleBackColor = false;
            this.btn_2.Click += new System.EventHandler(this.btn_2_Click);
            // 
            // btn_1
            // 
            this.btn_1.BackColor = System.Drawing.Color.Transparent;
            this.btn_1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_1.BackgroundImage")));
            this.btn_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_1.FlatAppearance.BorderSize = 0;
            this.btn_1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btn_1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btn_1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_1.ForeColor = System.Drawing.Color.White;
            this.btn_1.Location = new System.Drawing.Point(3, 143);
            this.btn_1.Name = "btn_1";
            this.btn_1.Size = new System.Drawing.Size(85, 64);
            this.btn_1.TabIndex = 9;
            this.btn_1.Text = "1";
            this.btn_1.UseVisualStyleBackColor = false;
            this.btn_1.Click += new System.EventHandler(this.btn_1_Click);
            // 
            // tlp_header
            // 
            this.tlp_header.ColumnCount = 2;
            this.tlp_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.42515F));
            this.tlp_header.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.57485F));
            this.tlp_header.Controls.Add(this.button2, 1, 0);
            this.tlp_header.Controls.Add(this.txtkeypadno, 0, 0);
            this.tlp_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlp_header.Location = new System.Drawing.Point(3, 3);
            this.tlp_header.Name = "tlp_header";
            this.tlp_header.RowCount = 1;
            this.tlp_header.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_header.Size = new System.Drawing.Size(276, 37);
            this.tlp_header.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.BackgroundImage = global::FoodandBeverage.Properties.Resources.keypad_clear;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(244, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(29, 31);
            this.button2.TabIndex = 333;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tlp_save_cls_button
            // 
            this.tlp_save_cls_button.ColumnCount = 2;
            this.tlp_save_cls_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_save_cls_button.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_save_cls_button.Controls.Add(this.mbtnClose, 1, 0);
            this.tlp_save_cls_button.Controls.Add(this.mbtnSave, 0, 0);
            this.tlp_save_cls_button.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlp_save_cls_button.Location = new System.Drawing.Point(3, 332);
            this.tlp_save_cls_button.Name = "tlp_save_cls_button";
            this.tlp_save_cls_button.RowCount = 1;
            this.tlp_save_cls_button.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlp_save_cls_button.Size = new System.Drawing.Size(276, 37);
            this.tlp_save_cls_button.TabIndex = 2;
            // 
            // mbtnClose
            // 
            this.mbtnClose.BackgroundImage = global::FoodandBeverage.Properties.Resources.keypad_cancel_button;
            this.mbtnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbtnClose.FlatAppearance.BorderSize = 0;
            this.mbtnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnClose.Location = new System.Drawing.Point(141, 3);
            this.mbtnClose.Name = "mbtnClose";
            this.mbtnClose.Size = new System.Drawing.Size(132, 31);
            this.mbtnClose.TabIndex = 64;
            this.mbtnClose.UseVisualStyleBackColor = true;
            this.mbtnClose.Click += new System.EventHandler(this.mbtnClose_Click);
            // 
            // mbtnSave
            // 
            this.mbtnSave.BackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.BackgroundImage = global::FoodandBeverage.Properties.Resources.keypad_save_button;
            this.mbtnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mbtnSave.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mbtnSave.FlatAppearance.BorderSize = 0;
            this.mbtnSave.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.mbtnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mbtnSave.Location = new System.Drawing.Point(3, 3);
            this.mbtnSave.Name = "mbtnSave";
            this.mbtnSave.Size = new System.Drawing.Size(132, 31);
            this.mbtnSave.TabIndex = 65;
            this.mbtnSave.UseVisualStyleBackColor = false;
            this.mbtnSave.Click += new System.EventHandler(this.mbtnSave_Click);
            // 
            // Frmkeypad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(292, 382);
            this.Controls.Add(this.tlp_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Frmkeypad";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "Frmkeypad";
            this.Load += new System.EventHandler(this.Frmkeypad_Load);
            this.tlp_main.ResumeLayout(false);
            this.tlp_keynumber.ResumeLayout(false);
            this.tlp_header.ResumeLayout(false);
            this.tlp_header.PerformLayout();
            this.tlp_save_cls_button.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox txtkeypadno;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_0;
        private System.Windows.Forms.Button btn_3;
        private System.Windows.Forms.Button btn_2;
        private System.Windows.Forms.Button btn_1;
        private System.Windows.Forms.Button btn_6;
        private System.Windows.Forms.Button btn_5;
        private System.Windows.Forms.Button btn_9;
        private System.Windows.Forms.Button btn_8;
        private System.Windows.Forms.Button btn_7;
        private System.Windows.Forms.Button btn_4;
        private System.Windows.Forms.Button mbtnClose;
        private System.Windows.Forms.Button mbtnSave;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TableLayoutPanel tlp_main;
        private System.Windows.Forms.TableLayoutPanel tlp_keynumber;
        private System.Windows.Forms.TableLayoutPanel tlp_header;
        private System.Windows.Forms.Button btndel;
        private System.Windows.Forms.TableLayoutPanel tlp_save_cls_button;
    }
}