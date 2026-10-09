using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FBControl
{
    /// <summary>
    /// Rounded, themeable textbox for the Midas Fun login panel.
    /// Compatible with .NET Framework 4.7.2.
    /// </summary>
    [DefaultEvent("TextChanged")]
    public class ModernTextBox : UserControl
    {
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(
            IntPtr hWnd, int msg, IntPtr wParam, string lParam);

        private readonly Panel inputSurface;
        private readonly Label iconLabel;
        private readonly TextBox textBox;

        private string placeholder = "Enter text";
        private string iconText = "";
        private int cornerRadius = 12;
        private Color borderColor = Color.FromArgb(221, 211, 248);
        private Color focusBorderColor = Color.FromArgb(116, 45, 238);
        private Color inputBackColor = Color.FromArgb(255, 255, 255);
        private Color inputForeColor = Color.FromArgb(35, 35, 72);
        private Color placeholderColor = Color.FromArgb(132, 128, 157);
        private Color iconColor = Color.FromArgb(105, 28, 235);
        private bool usePassword;
        private bool showIcon = true;

        public ModernTextBox()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);

            Size = new Size(350, 46);
            MinimumSize = new Size(150, 38);
            BackColor = Color.Transparent;
            Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);

            inputSurface = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = inputBackColor,
                Padding = new Padding(3, 2, 3, 2),
                Margin = Padding.Empty
            };

            iconLabel = new Label
            {
                Dock = DockStyle.Left,
                Width = 42,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI Symbol", 15F, FontStyle.Regular),
                ForeColor = iconColor,
                BackColor = inputBackColor,
                Margin = Padding.Empty
            };

            textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Multiline = false,
                Dock = DockStyle.Fill,
                Font = Font,
                ForeColor = inputForeColor,
                BackColor = inputBackColor,
                UseSystemPasswordChar = false,
                Margin = Padding.Empty,
                ShortcutsEnabled = true,
                TabStop = true
            };

            // Add the textbox first so the left icon remains visible above it.
            inputSurface.Controls.Add(textBox);
            inputSurface.Controls.Add(iconLabel);
            Controls.Add(inputSurface);

            textBox.GotFocus += delegate { Invalidate(); };
            textBox.LostFocus += delegate { Invalidate(); };
            textBox.TextChanged += delegate
            {
                OnTextChanged(EventArgs.Empty);
            };

            ApplyPlaceholder();
            ApplyPasswordSetting();
        }

        [Category("Appearance")]
        [Description("Placeholder text displayed when the input is empty.")]
        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                placeholder = value ?? string.Empty;
                ApplyPlaceholder();
            }
        }

        [Category("Appearance")]
        [Description("Symbol displayed at the left side of the input.")]
        public string IconText
        {
            get { return iconText; }
            set
            {
                iconText = value ?? string.Empty;
                iconLabel.Text = iconText;
                iconLabel.Visible = showIcon && iconText.Length > 0;
            }
        }

        [Category("Appearance")]
        [Description("Show or hide the left icon area.")]
        [DefaultValue(true)]
        public bool ShowIcon
        {
            get { return showIcon; }
            set
            {
                showIcon = value;
                iconLabel.Visible = showIcon && iconText.Length > 0;
            }
        }

        [Category("Behavior")]
        [Description("Masks characters when true.")]
        [DefaultValue(false)]
        public bool UsePassword
        {
            get { return usePassword; }
            set
            {
                usePassword = value;
                ApplyPasswordSetting();
            }
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
            set
            {
                inputForeColor = value;
                textBox.ForeColor = value;
            }
        }

        [Category("Appearance")]
        public Color PlaceholderColor
        {
            get { return placeholderColor; }
            set
            {
                placeholderColor = value;
                ApplyPlaceholder();
            }
        }

        [Category("Appearance")]
        public Color IconColor
        {
            get { return iconColor; }
            set
            {
                iconColor = value;
                iconLabel.ForeColor = value;
            }
        }

        [Category("Appearance")]
        [DefaultValue(12)]
        public int CornerRadius
        {
            get { return cornerRadius; }
            set
            {
                cornerRadius = Math.Max(4, Math.Min(value, 22));
                UpdateRoundedRegion();
                Invalidate();
            }
        }

        public event EventHandler TextChanged;

        public override string Text
        {
            get { return textBox == null ? base.Text : textBox.Text; }
            set
            {
                if (textBox == null)
                    base.Text = value;
                else
                    textBox.Text = value ?? string.Empty;
            }
        }

        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                if (textBox != null)
                    textBox.Font = value;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateRoundedRegion();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Width < 5 || Height < 5)
                return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle bounds = new Rectangle(
                1, 1, Width - 3, Height - 3);

            using (GraphicsPath path = CreateRoundedPath(bounds, cornerRadius))
            using (SolidBrush brush = new SolidBrush(inputBackColor))
            using (Pen pen = new Pen(ContainsFocus ? focusBorderColor : borderColor,
                ContainsFocus ? 1.8F : 1.2F))
            {
                e.Graphics.FillPath(brush, path);
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void ApplyPlaceholder()
        {
            if (textBox == null || !textBox.IsHandleCreated)
                return;

            // Native cue banner keeps placeholder text out of the real Text value.
            SendMessage(textBox.Handle, EM_SETCUEBANNER,
                new IntPtr(1), placeholder);
        }

        private void ApplyPasswordSetting()
        {
            if (textBox != null)
                textBox.UseSystemPasswordChar = usePassword;
        }

        private void UpdateRoundedRegion()
        {
            if (Width < 5 || Height < 5)
                return;

            using (GraphicsPath path = CreateRoundedPath(
                new Rectangle(0, 0, Width, Height), cornerRadius))
            {
                Region oldRegion = Region;
                Region = new Region(path);
                if (oldRegion != null)
                    oldRegion.Dispose();
            }
        }

        private static GraphicsPath CreateRoundedPath(
            Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = Math.Max(2, radius * 2);
            diameter = Math.Min(diameter,
                Math.Min(bounds.Width, bounds.Height));

            path.AddArc(bounds.X, bounds.Y,
                diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y,
                diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter,
                bounds.Bottom - diameter,
                diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter,
                diameter, diameter, 90, 90);
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Child controls are disposed by the parent UserControl.
            }
            base.Dispose(disposing);
        }
    }
}