using Domain;
using FoodandBeverage.Forms;
using FoodandBeverage.Forms.UserAccount;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
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

        public static class NotificationService
        {
            public static event Action<OrderNotification> OnNotify;

            public static void Raise(string message, int orderId, int refId)
            {
                OnNotify?.Invoke(new OrderNotification
                {
                    Message = message,
                    OrderMasterId = orderId,
                    TableOrPickupId = refId,
                    Time = DateTime.Now
                });
            }
        }
        public static void UpdateNotificationStatus(int id, int tableType, DateTime notifyTime)
        {

        }
        public static void SetRoundCorner(Control ctrl, int radius)
        {
            if (ctrl == null) return;

            Rectangle rect = new Rectangle(0, 0, ctrl.Width, ctrl.Height);
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Width - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Width - d, rect.Height - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Height - d, d, d, 90, 90);
            path.CloseFigure();

            ctrl.Region = new Region(path);
        }
        #region  open form closeinng and Dispose
        public static void CloseIfOpen<T>() where T : Form
        {
            try
            {
                var form = Application.OpenForms
                                       .OfType<T>()
                                       .FirstOrDefault();

                if (form != null && !form.IsDisposed)
                {
                    form.Close();
                    form.Dispose();
                }
            }
            catch
            {
                // Optional: log error
            }
        }
        public static void CloseAllMasterForms()
        {
            CloseIfOpen<FrmDepartment>();
            CloseIfOpen<FrmDesignation>();
            CloseIfOpen<FrmDrawer>();
            CloseIfOpen<FrmEmployeeShort>();
            CloseIfOpen<FrmExpence>();
            CloseIfOpen<FrmFTICategory>();
            CloseIfOpen<FrmIngredient>();
            CloseIfOpen<FrmItemRegistartion>();
            CloseIfOpen<FrmKitchenView>();
            CloseIfOpen<FrmMenu>();
            CloseIfOpen<FrmOfferDiscount>();
            CloseIfOpen<FrmPromo>();
            CloseIfOpen<FrmPurchase>();
            CloseIfOpen<FrmPurchaseReturn>();
            CloseIfOpen<FrmRefund>();
            CloseIfOpen<FrmRegistration>();
            CloseIfOpen<FrmTableInfo>();
            CloseIfOpen<FrmTaxSetting>();
            CloseIfOpen<FrmUnit>();
            CloseIfOpen<FrmUnitConversion>();
            CloseIfOpen<FrmUserAccount>();
            CloseIfOpen<FrmUserPermission>();
            CloseIfOpen<FrmVendor>();
            CloseIfOpen<Frmvoidsale>();
            CloseIfOpen<FrmTMpanel>();
            CloseIfOpen<FrmPartyPackSetup>();
            CloseIfOpen<FrmProductionMapping>();
        }
        #endregion

        #region Closing button clcik
        public static void CloseFormSafe(Form form)
        {
            if (form == null || form.IsDisposed)
                return;

            try
            {
                // Stop & dispose all timers on the form
                foreach (var timer in form.Controls
                                          .OfType<Control>()
                                          .SelectMany(GetAllControls)
                                          .OfType<Timer>())
                {
                    timer.Stop();
                    timer.Dispose();
                }

                // Dispose all child controls
                DisposeControls(form);

                // Close form (this triggers FormClosing)
                form.Close();
            }
            catch
            {
                // Optional logging
            }
        }

        private static void DisposeControls(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                DisposeControls(ctrl);
                ctrl.Dispose();
            }
        }

        private static System.Collections.Generic.IEnumerable<Control> GetAllControls(Control parent)
        {
            foreach (Control child in parent.Controls)
            {
                yield return child;
                foreach (var grandChild in GetAllControls(child))
                    yield return grandChild;
            }
        }
        #endregion

        public static void AutoFitLabelFont(Label lbl)
        {
            if (lbl == null || string.IsNullOrEmpty(lbl.Text)) return;

            int maxWidth = lbl.Width - 4;   // padding
            int maxHeight = lbl.Height - 4;

            float fontSize = lbl.Font.Size;

            using (Graphics g = lbl.CreateGraphics())
            {
                SizeF size = g.MeasureString(lbl.Text, lbl.Font);

                while ((size.Width > maxWidth || size.Height > maxHeight) && fontSize > 5f)
                {
                    fontSize -= 0.5f;

                    lbl.Font = new Font(
                        lbl.Font.FontFamily,
                        fontSize,
                        lbl.Font.Style
                    );

                    size = g.MeasureString(lbl.Text, lbl.Font);
                }
            }
        }
        public static void MakeDashboardTile(Control ctrl,
                                     int radius = 20,
                                     Color backColor = default,
                                     Color borderColor = default,
                                     int borderSize = 2)
        {
            if (backColor == default) backColor = Color.White;
            if (borderColor == default) borderColor = Color.FromArgb(200, 200, 200);

            ctrl.Paint += (s, e) =>
            {
                Rectangle r = new Rectangle(0, 0, ctrl.Width - 1, ctrl.Height - 1);
                using (GraphicsPath path = GetRoundRectangle(r, radius))
                using (SolidBrush brush = new SolidBrush(backColor))
                using (Pen pen = new Pen(borderColor, borderSize))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.FillPath(brush, path);
                    e.Graphics.DrawPath(pen, path);
                }
            };

            ctrl.Resize += (s, e) => ctrl.Invalidate();
        }
        private static GraphicsPath GetRoundRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            float r = radius * 2F;

            path.AddArc(rect.X, rect.Y, r, r, 180, 90);
            path.AddArc(rect.Right - r, rect.Y, r, r, 270, 90);
            path.AddArc(rect.Right - r, rect.Bottom - r, r, r, 0, 90);
            path.AddArc(rect.X, rect.Bottom - r, r, r, 90, 90);

            path.CloseFigure();
            return path;
        }


        public static void AutoResizeFont(Label label)
        {
            if (string.IsNullOrEmpty(label.Text))
                return;

            int maxHeight = label.Height;
            int maxWidth = label.Width;

            using (Graphics g = label.CreateGraphics())
            {
                float fontSize = label.Font.Size;

                // reduce until it fits
                while (fontSize > 5)
                {
                    SizeF size = g.MeasureString(
                        label.Text,
                        new Font(label.Font.FontFamily, fontSize, label.Font.Style)
                    );

                    if (size.Width <= maxWidth && size.Height <= maxHeight)
                        break;

                    fontSize -= 0.5f;
                }
                label.Font = new Font(label.Font.FontFamily, fontSize, label.Font.Style);
            }
        }
        public static void RoundControl_2(Control ctrl, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(ctrl.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(ctrl.Width - radius, ctrl.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, ctrl.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            ctrl.Region = new Region(path);
        }

        public static void RoundControl(Control ctrl, int radius, Color? borderColor = null, int borderWidth = 2)
        {
            if (ctrl == null || radius <= 0)
                return;

            GraphicsPath path = new GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, ctrl.Width, ctrl.Height);
            int diameter = radius * 2;
            Rectangle arc = new Rectangle(rect.Location, new Size(diameter, diameter));

            // Top-left
            path.AddArc(arc, 180, 90);

            // Top-right
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Bottom-right
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Bottom-left
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();

            ctrl.Region = new Region(path);

            if (borderColor.HasValue)
            {
                ctrl.Paint += (s, e) =>
                {
                    using (Pen pen = new Pen(borderColor.Value, borderWidth))
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        e.Graphics.DrawPath(pen, path);
                    }
                };
                ctrl.Invalidate();
            }
        }

        /// <summary>
        /// Centers a control on the parent form, resizes it based on percentage,
        /// makes it visible and brings it to front.
        /// </summary>
        /// <param name="ctrl">Control to modify</param>
        /// <param name="widthPercent">Width percentage (0–100)</param>
        /// <param name="heightPercent">Height percentage (0–100)</param>
        public static void CenterAndResize(Control ctrl, int widthPercent = 80, int heightPercent = 80)
        {
            if (ctrl?.Parent == null)
                return;

            Control parent = ctrl.Parent;

            // --- Calculate new size ---
            int newWidth = (parent.Width * widthPercent) / 100;
            int newHeight = (parent.Height * heightPercent) / 100;

            //ctrl.Size = new Size(newWidth, newHeight);

            // --- Center the control ---
            int x = (parent.Width - ctrl.Width) / 2;
            int y = (parent.Height - ctrl.Height) / 2;

            ctrl.Location = new Point(x, y);

            // --- Make visible and front ---
            ctrl.Visible = true;
            ctrl.BringToFront();
        }
        public static void ResizeOnly(Control ctrl, int widthPercent = 80, int heightPercent = 80)
        {
            if (ctrl?.Parent == null)
                return;

            Control parent = ctrl.Parent;

            int newWidth = (parent.Width * widthPercent) / 100;
            int newHeight = (parent.Height * heightPercent) / 100;

            ctrl.Size = new Size(newWidth, newHeight);
        }
        public static void AutoScaleFont(Control ctrl, int baseSize)
        {
            if (ctrl.Width <= 0 || ctrl.Height <= 0) return;

            float scale = Math.Min(ctrl.Width / 150f, ctrl.Height / 50f); // base size
            float newSize = baseSize * scale;

            if (newSize < 8f) newSize = 8f; // minimum readable

            ctrl.Font = new Font(ctrl.Font.FontFamily, newSize, ctrl.Font.Style);
        }
        public static void AutoResizeFlowPanel(
          FlowLayoutPanel panel,
          int btnWidth,
          int btnHeight,
          int spacing,
          int visibleCount
      )
        {
            if (visibleCount == 0) return;

            panel.WrapContents = true;       // IMPORTANT
            panel.AutoSize = false;          // So we can manually set height/width
            panel.PerformLayout();           // Refresh layout before measuring

            int maxColumns = 4;
            int columns = Math.Min(visibleCount, maxColumns);

            // Calculate rows (max 2)
            int rows = (int)Math.Ceiling(visibleCount / (double)maxColumns);
            rows = Math.Min(rows, 2);

            // Calculate final size
            int width = (btnWidth * columns) + (spacing * (columns + 1));
            int height = (btnHeight * rows) + (spacing * (rows + 1));

            panel.SuspendLayout();           // Prevent flickering while resizing
            panel.Width = width;
            panel.Height = btnHeight *2;
            panel.ResumeLayout();
        }




        public static void ShowCentered(Control parent, Control child)
        {
            if (parent == null || child == null) return;

            // Center Position
            int x = (parent.ClientSize.Width - child.Width) / 2;
            int y = (parent.ClientSize.Height - child.Height) / 2;

            child.Location = new Point(x, y);

            // Show
            child.Visible = true;

            // Bring to front (above all other controls)
            child.BringToFront();

            // Focus
            child.Focus();
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
