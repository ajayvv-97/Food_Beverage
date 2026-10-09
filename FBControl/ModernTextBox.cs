
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FBControl
{
    public class ModernTextBox : UserControl
    {
        private readonly TextBox textBox;
        private readonly Label iconLabel;

        private string placeholder = "Enter text";
        private bool showingPlaceholder;
        private bool usePassword;

        public string Placeholder
        {
            get { return placeholder; }
            set
            {
                placeholder = value ?? "";
                ShowPlaceholderIfNeeded();
            }
        }

        public string TextValue
        {
            get { return showingPlaceholder ? "" : textBox.Text; }
            set
            {
                textBox.Text = value ?? "";
                showingPlaceholder = false;
                ShowPlaceholderIfNeeded();
            }
        }

        public bool UsePassword
        {
            get { return usePassword; }
            set
            {
                usePassword = value;
                textBox.UseSystemPasswordChar =
                    value && !showingPlaceholder;
            }
        }

        public string IconText
        {
            get { return iconLabel.Text; }
            set { iconLabel.Text = value; }
        }

        public ModernTextBox()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);

            Size = new Size(350, 46);
            BackColor = Color.White;
            Padding = new Padding(2);

            iconLabel = new Label
            {
                Text = "",
                Width = 45,
                Dock = DockStyle.Left,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 15F),
                ForeColor = Color.FromArgb(105, 0, 230),
                BackColor = Color.White
            };

            textBox = new TextBox
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(35, 35, 70),
                BackColor = Color.White,
                Dock = DockStyle.Fill
            };

            Controls.Add(textBox);
            Controls.Add(iconLabel);

            textBox.Enter += (s, e) =>
            {
                if (showingPlaceholder)
                {
                    textBox.Clear();
                    showingPlaceholder = false;
                    textBox.ForeColor =
                        Color.FromArgb(35, 35, 70);
                    textBox.UseSystemPasswordChar = usePassword;
                }

                Invalidate();
            };

            textBox.Leave += (s, e) =>
            {
                ShowPlaceholderIfNeeded();
                Invalidate();
            };

            textBox.TextChanged += (s, e) =>
            {
                Invalidate();
            };

            ShowPlaceholderIfNeeded();
        }

        private void ShowPlaceholderIfNeeded()
        {
            if (textBox == null || textBox.Focused)
                return;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                showingPlaceholder = true;
                textBox.Text = placeholder;
                textBox.ForeColor = Color.Gray;
                textBox.UseSystemPasswordChar = false;
            }
            else
            {
                showingPlaceholder = false;
                textBox.ForeColor =
                    Color.FromArgb(35, 35, 70);
                textBox.UseSystemPasswordChar = usePassword;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode =
                SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(
                1, 1, Width - 3, Height - 3);

            Color borderColor = ContainsFocus
                ? Color.FromArgb(130, 60, 255)
                : Color.FromArgb(220, 210, 245);

            using (GraphicsPath path =
                CreateRoundedPath(rect, 13))
            using (Pen pen = new Pen(borderColor, 1.5f))
            {
                e.Graphics.DrawPath(pen, path);
            }
        }

        private GraphicsPath CreateRoundedPath(
            Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d,
                rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X,
                rect.Bottom - d, d, d, 90, 90);

            path.CloseFigure();
            return path;
        }

        public void ClearInput()
        {
            textBox.Text = "";
            showingPlaceholder = false;
            ShowPlaceholderIfNeeded();
        }
    }
}
