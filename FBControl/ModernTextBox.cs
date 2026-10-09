using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FBControl
{
    /// <summary>
    /// Clean rounded textbox for .NET Framework 4.7.2.
    /// Uses a single white input surface with a crisp border and no glow/shadow.
    /// </summary>
    [DefaultEvent("TextChanged")]
    public class ModernTextBox : UserControl
    {
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        private readonly Panel inputSurface;
        private readonly Label iconLabel;
        private readonly TextBox textBox;

        private string placeholder = "Enter text";
        private string iconText = string.Empty;
        private int cornerRadius = 12;
        private Color borderColor = Color.FromArgb(218, 210, 246);
        private Color focusBorderColor = Color.FromArgb(112, 42, 235);
        private Color inputBackColor = Color.White;
        private Color inputForeColor = Color.FromArgb(35, 35, 72);
        private Color iconColor = Color.FromArgb(105, 28, 235);
        private bool usePassword;
        private bool showIcon = true;

        public ModernTextBox()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);

            Size = new Size(350, 48);
            MinimumSize = new Size(150, 40);
            BackColor = Color.Transparent;
            Padding = Padding.Empty;
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);

            inputSurface = new Panel
            {
                Location = new Point(2, 2),
                Size = new Size(Math.Max(1, Width - 4), Math.Max(1, Height - 4)),
                BackColor = inputBackColor,
                Padding = Padding.Empty,
                Margin = Padding.Empty
            };

            iconLabel = new Label
            {
                Dock = DockStyle.None,
                Width = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe MDL2 Assets", 17F, FontStyle.Regular),
                ForeColor = iconColor,
                BackColor = inputBackColor,
                Margin = Padding.Empty,
                AutoSize = false,
                UseCompatibleTextRendering = true,
                Visible = false
            };

            textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Multiline = false,
                Dock = DockStyle.None,
                Font = Font,
                ForeColor = inputForeColor,
                BackColor = inputBackColor,
                UseSystemPasswordChar = false,
                Margin = Padding.Empty,
                ShortcutsEnabled = true,
                TabStop = true
            };

            inputSurface.Controls.Add(textBox);
            inputSurface.Controls.Add(iconLabel);
            Controls.Add(inputSurface);

            textBox.HandleCreated += delegate { ApplyPlaceholder(); };
            textBox.GotFocus += delegate { Invalidate(); };
            textBox.LostFocus += delegate { Invalidate(); };
            textBox.Enter += delegate { OnEnter(EventArgs.Empty); };
            textBox.Leave += delegate { OnLeave(EventArgs.Empty); };
            textBox.Click += delegate { OnClick(EventArgs.Empty); };
            textBox.KeyDown += delegate(object sender, KeyEventArgs e) { OnKeyDown(e); };
            textBox.KeyPress += delegate(object sender, KeyPressEventArgs e) { OnKeyPress(e); };
            textBox.TextChanged += delegate { OnTextChanged(EventArgs.Empty); };
            iconLabel.Click += delegate
            {
                textBox.Focus();
                OnClick(EventArgs.Empty);
            };
            inputSurface.Resize += delegate
            {
                UpdateSurfaceRegion();
                UpdateInputLayout();
            };

            ApplyPlaceholder();
            ApplyPasswordSetting();
            UpdateLayoutAndRegions();
        }

        [Category("Appearance")]
        [Description("Placeholder shown when the textbox is empty.")]
        public string Placeholder
        {
            get { return placeholder; }
            set { placeholder = value ?? string.Empty; ApplyPlaceholder(); }
        }

        [Category("Appearance")]
        [Description("Optional symbol displayed on the left.")]
        public string IconText
        {
            get { return iconText; }
            set
            {
                iconText = value ?? string.Empty;
                iconLabel.Text = iconText;
                iconLabel.Visible = showIcon && iconText.Length > 0;
                UpdateInputLayout();
                if (iconLabel.Visible)
                    iconLabel.BringToFront();
                if (iconLabel.Visible)
                    iconLabel.BringToFront();
            }
        }

        [Category("Appearance")]
        [DefaultValue(true)]
        public bool ShowIcon
        {
            get { return showIcon; }
            set
            {
                showIcon = value;
                iconLabel.Visible = showIcon && iconText.Length > 0;
                UpdateInputLayout();
            }
        }

        [Category("Behavior")]
        [DefaultValue(false)]
        public bool UsePassword
        {
            get { return usePassword; }
            set { usePassword = value; ApplyPasswordSetting(); }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TextBox InputTextBox
        {
            get { return textBox; }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TextValue
        {
            get { return textBox.Text; }
            set { textBox.Text = value ?? string.Empty; }
        }

        [Category("Appearance")]
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color FocusBorderColor
        {
            get { return focusBorderColor; }
            set { focusBorderColor = value; Invalidate(); }
        }

        [Category("Appearance")]
        public Color InputBackColor
        {
            get { return inputBackColor; }
            set
            {
                inputBackColor = value;
                inputSurface.BackColor = value;
                iconLabel.BackColor = value;
                textBox.BackColor = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public Color InputForeColor
        {
            get { return inputForeColor; }
            set { inputForeColor = value; textBox.ForeColor = value; }
        }

        [Category("Appearance")]
        public Color IconColor
        {
            get { return iconColor; }
            set { iconColor = value; iconLabel.ForeColor = value; }
        }

        [Category("Appearance")]
        [DefaultValue(12)]
        public int CornerRadius
        {
            get { return cornerRadius; }
            set
            {
                cornerRadius = Math.Max(5, Math.Min(value, 24));
                UpdateLayoutAndRegions();
                Invalidate();
            }
        }

        public override string Text
        {
            get { return textBox == null ? base.Text : textBox.Text; }
            set
            {
                if (textBox == null) base.Text = value;
                else textBox.Text = value ?? string.Empty;
            }
        }

        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                if (textBox != null) textBox.Font = value;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateLayoutAndRegions();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Width < 8 || Height < 8) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            Rectangle bounds = new Rectangle(1, 1, Width - 3, Height - 3);

            using (GraphicsPath path = CreateRoundedPath(bounds, cornerRadius))
            using (SolidBrush brush = new SolidBrush(inputBackColor))
            using (Pen pen = new Pen(ContainsFocus ? focusBorderColor : borderColor,
                ContainsFocus ? 1.7F : 1.15F))
            {
                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void UpdateLayoutAndRegions()
        {
            if (inputSurface == null || Width < 8 || Height < 8) return;

            inputSurface.Bounds = new Rectangle(2, 2, Width - 4, Height - 4);
            UpdateSurfaceRegion();
            UpdateInputLayout();

            using (GraphicsPath path = CreateRoundedPath(
                new Rectangle(0, 0, Width, Height), cornerRadius))
            {
                Region old = Region;
                Region = new Region(path);
                if (old != null) old.Dispose();
            }
            Invalidate();
        }

        private void UpdateSurfaceRegion()
        {
            if (inputSurface == null || inputSurface.Width < 4 || inputSurface.Height < 4) return;
            using (GraphicsPath path = CreateRoundedPath(
                new Rectangle(0, 0, inputSurface.Width, inputSurface.Height),
                Math.Max(3, cornerRadius - 2)))
            {
                Region old = inputSurface.Region;
                inputSurface.Region = new Region(path);
                if (old != null) old.Dispose();
            }
        }

        private void UpdateInputLayout()
        {
            if (inputSurface == null || textBox == null || iconLabel == null ||
                inputSurface.ClientSize.Width <= 0 || inputSurface.ClientSize.Height <= 0)
                return;

            int surfaceHeight = inputSurface.ClientSize.Height;
            int textHeight = Math.Min(surfaceHeight - 4,
                Math.Max(22, TextRenderer.MeasureText("Ag", textBox.Font).Height + 2));
            int textY = Math.Max(2, (surfaceHeight - textHeight) / 2);
            int left = 14;

            if (iconLabel.Visible)
            {
                iconLabel.Bounds = new Rectangle(8, 0, 32, surfaceHeight);
                iconLabel.TextAlign = ContentAlignment.MiddleCenter;
                iconLabel.BringToFront();
                left = iconLabel.Right + 8;
            }
            else
            {
                iconLabel.Bounds = Rectangle.Empty;
            }

            textBox.Bounds = new Rectangle(
                left,
                textY,
                Math.Max(20, inputSurface.ClientSize.Width - left - 14),
                textHeight);
        }

        private void ApplyPlaceholder()
        {
            if (textBox == null || !textBox.IsHandleCreated) return;
            SendMessage(textBox.Handle, EM_SETCUEBANNER, IntPtr.Zero, placeholder);
        }

        private void ApplyPasswordSetting()
        {
            if (textBox != null) textBox.UseSystemPasswordChar = usePassword;
        }

        private static GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = Math.Max(2, Math.Min(radius * 2,
                Math.Min(bounds.Width, bounds.Height)));
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        public void ClearInput()
        {
            textBox.Clear();
            textBox.Focus();
        }

        public void FocusInput()
        {
            textBox.Focus();
        }
    }
}