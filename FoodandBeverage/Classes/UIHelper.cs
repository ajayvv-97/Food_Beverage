using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FoodandBeverage.Classes
{
    public static class UIHelper
    {
        /// <summary>
        /// Makes any Panel, TableLayoutPanel, or Control have rounded corners.
        /// </summary>
        /// <param name="ctrl">The control to round (e.g., Panel, TableLayoutPanel)</param>
        /// <param name="radius">Corner radius (in pixels)</param>
        /// <param name="borderColor">Optional border color (default = none)</param>
        public static void RoundCorners(Control ctrl, int radius, Color? borderColor = null)
        {
            if (ctrl == null || radius <= 0)
                return;

            // Create rounded rectangle path
            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, ctrl.Width, ctrl.Height);
            int diameter = radius * 2;
            Rectangle arc = new Rectangle(rect.Location, new Size(diameter, diameter));

            // Top-left corner
            path.AddArc(arc, 180, 90);

            // Top-right corner
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Bottom-right corner
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Bottom-left corner
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            // Apply region to control
            ctrl.Region = new Region(path);

            // Optional border drawing
            if (borderColor.HasValue)
            {
                ctrl.Paint += (s, e) =>
                {
                    using (Pen pen = new Pen(borderColor.Value, 2))
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.DrawPath(pen, path);
                    }
                };
                ctrl.Invalidate(); // force repaint to show border
            }
        }
        public static void ApplyRoundedCorners(Form form, int borderRadius)
        {
            try
            {
                if (form == null || borderRadius <= 0)
                    return;

                Rectangle bounds = new Rectangle(0, 0, form.Width, form.Height);

                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddArc(bounds.Left, bounds.Top, borderRadius, borderRadius, 180, 90);
                    path.AddArc(bounds.Right - borderRadius, bounds.Top, borderRadius, borderRadius, 270, 90);
                    path.AddArc(bounds.Right - borderRadius, bounds.Bottom - borderRadius, borderRadius, borderRadius, 0, 90);
                    path.AddArc(bounds.Left, bounds.Bottom - borderRadius, borderRadius, borderRadius, 90, 90);
                    path.CloseFigure();

                    form.Region = new Region(path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Rounded corner error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void MakePickupBagShape(TableLayoutPanel table, int cornerRadius, Color borderColor)
        {
            if (table == null) return;

            // Create bag-like path
            GraphicsPath path = new GraphicsPath();

            int w = table.Width;
            int h = table.Height;

            int handleHeight = h / 5; // top curve height
            int handleWidth = w / 3;  // width of the handle gap

            // Start bottom left
            path.StartFigure();

            // Bottom left corner (rounded)
            path.AddArc(0, h - cornerRadius, cornerRadius, cornerRadius, 90, 90);

            // Bottom edge
            path.AddLine(cornerRadius, h, w - cornerRadius, h);

            // Bottom right corner (rounded)
            path.AddArc(w - cornerRadius, h - cornerRadius, cornerRadius, cornerRadius, 0, 90);

            // Right edge
            path.AddLine(w, h - cornerRadius, w, handleHeight + cornerRadius);

            // Top-right curve (handle start)
            path.AddArc(w - handleWidth, handleHeight - cornerRadius / 2, cornerRadius, cornerRadius, 0, -60);

            // Handle top arc
            path.AddArc((w - handleWidth) / 2, 0, handleWidth, handleHeight, 0, 180);

            // Top-left curve (handle end)
            path.AddArc(handleWidth - cornerRadius, handleHeight - cornerRadius / 2, cornerRadius, cornerRadius, 240, -60);

            // Left edge
            path.AddLine(0, handleHeight + cornerRadius, 0, h - cornerRadius);

            path.CloseFigure();

            // Apply the region to make it shaped
            table.Region = new Region(path);

            // Draw border manually
            table.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(borderColor, 2))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawPath(pen, path);
                }
            };

            // Optional background color (white pickup box)
            table.BackColor = Color.White;
        }
        public static void MakePanelCircular(Panel panel, Color borderColor, int borderThickness = 2)
        {
            panel.Paint += (s, e) =>
            {
                // Create circular shape
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, panel.Width - 1, panel.Height - 1);
                    panel.Region = new Region(path);

                    // Enable smooth drawing
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    // Draw border
                    using (Pen pen = new Pen(borderColor, borderThickness))
                    {
                        e.Graphics.DrawEllipse(pen, 0, 0, panel.Width - 1, panel.Height - 1);
                    }
                }
            };

            // Redraw when resized
            panel.Resize += (s, e) => panel.Invalidate();
        }
        public static void AutoResizeSideMenuButtons(FlowLayoutPanel flp)
        {
            if (flp == null || flp.Controls.Count == 0)
                return;

            int totalButtons = flp.Controls.Count;
            int panelHeight = flp.ClientSize.Height;
            int spacing = flp.Padding.Vertical + (flp.Margin.Vertical * (totalButtons - 1));

            // Subtract space used by panel padding/margins
            int usableHeight = panelHeight - spacing;

            // Evenly divide
            int buttonHeight = Math.Max(50, usableHeight / totalButtons);
            int buttonWidth = flp.ClientSize.Width - 10;

            foreach (Control ctrl in flp.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Width = buttonWidth;
                    btn.Height = buttonHeight;
                    btn.Margin = new Padding(2);
                    btn.TextAlign = ContentAlignment.MiddleRight;
                    btn.ImageAlign = ContentAlignment.MiddleLeft;

                    // Resize icon to match new height (slightly smaller)
                    int iconSize = Math.Min(buttonHeight - 10, 64);
                    if (btn.Image != null)
                    {
                        btn.Image = new Bitmap(btn.Image, new Size(iconSize, iconSize));
                    }
                }
            }
        }
        /// <summary>
        /// Automatically reduces the button text font size so it fits in a single line.
        /// </summary>
        public static void AutoAdjustButtonFont(Button btn, int maxFontSize = 14, int minFontSize = 7)
        {
            if (btn == null || string.IsNullOrEmpty(btn.Text))
                return;

            using (Graphics g = btn.CreateGraphics())
            {
                Font currentFont = new Font(btn.Font.FontFamily, maxFontSize, btn.Font.Style);
                SizeF textSize = g.MeasureString(btn.Text, currentFont);

                // Keep reducing until text fits within button width (minus padding for icon)
                int padding = btn.Image != null ? 60 : 20;
                while (textSize.Width > (btn.Width - padding) && currentFont.Size > minFontSize)
                {
                    currentFont = new Font(currentFont.FontFamily, currentFont.Size - 0.5f, btn.Font.Style);
                    textSize = g.MeasureString(btn.Text, currentFont);
                }

                btn.Font = currentFont;
            }
        }

        /// <summary>
        /// Apply consistent style to menu buttons.
        /// </summary>
        public static void StyleMenuButton(Button btn)
        {
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.ImageAlign = ContentAlignment.MiddleLeft;
            btn.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn.AutoEllipsis = true;
            btn.UseCompatibleTextRendering = false;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Padding = new Padding(10, 0, 0, 0);
            btn.ForeColor = Color.White;
        }
    
    public static Size GetScaledIconSize(int? referenceHeight = null)
        {
            var screen = Screen.PrimaryScreen.Bounds;
            int screenWidth = screen.Width;
            int screenHeight = screen.Height;

            // Base scaling factor relative to 1920x1080
            float widthScale = screenWidth / 1920f;
            float heightScale = screenHeight / 1080f;
            float scale = Math.Min(widthScale, heightScale);

            int baseSize = 48; // default icon size

            // If we know button height, base it on that
            if (referenceHeight.HasValue)
            {
                baseSize = (int)(referenceHeight.Value * 0.6f);
            }

            int iconSize = Math.Max(24, Math.Min(64, (int)(baseSize * scale)));

            return new Size(iconSize, iconSize);
        }

        public static void StyleSideMenuButton(Button btn, int radius, int iconSize, Color backColor)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = backColor;
            btn.ForeColor = Color.White;
            btn.TextAlign = ContentAlignment.MiddleRight;
            btn.ImageAlign = ContentAlignment.MiddleLeft;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Margin = new Padding(5, 8, 5, 8);
            btn.Height = 60;

            // Round corners
            btn.Region = new Region(CreateRoundRect(btn.ClientRectangle, radius));

            // Resize icon
            if (btn.Image != null)
                btn.Image = new Bitmap(btn.Image, new Size(iconSize, iconSize));
        }

        private static GraphicsPath CreateRoundRect(Rectangle r, int radius)
        {
            GraphicsPath gp = new GraphicsPath();
            int d = radius * 2;
            gp.AddArc(r.X, r.Y, d, d, 180, 90);
            gp.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            gp.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            gp.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            gp.CloseFigure();
            return gp;
        }
    }
}
