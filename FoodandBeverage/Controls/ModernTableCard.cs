using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace FoodandBeverage.Controls
{
    internal enum ModernTableCardState
    {
        Available,
        OrderTaken,
        Checkout,
        Reservation,
        ReservationExpired,
        CancelledItems,
        StaffCancelled,
        ReconfirmOrder
    }

    /// <summary>
    /// A single, double-buffered, owner-drawn table card. It deliberately has no child controls,
    /// keeping the table map lightweight while allowing each table to repaint only when its state changes.
    /// </summary>
    internal sealed class ModernTableCard : Control
    {
        private static readonly Font TableNameFont = new Font("Segoe UI", 10F, FontStyle.Bold);
        private static readonly Font StatusFont = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        private static readonly StringFormat CenterFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            Trimming = StringTrimming.EllipsisCharacter,
            FormatFlags = StringFormatFlags.NoWrap
        };

        private ModernTableCardState visualState = ModernTableCardState.Available;
        private string statusText = "Available";
        private int cancelledCount;
        private bool isHovered;
        private bool isPressed;

        public ModernTableCard()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.SupportsTransparentBackColor,
                true);

            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(31, 41, 55);
            Cursor = Cursors.Hand;
            TabStop = false;
            AccessibleRole = AccessibleRole.PushButton;
        }

        public void SetVisualState(ModernTableCardState state, string text, int cancelledItems)
        {
            string nextText = string.IsNullOrWhiteSpace(text) ? GetDefaultStatusText(state) : text;
            int nextCount = Math.Max(0, cancelledItems);

            if (visualState == state &&
                string.Equals(statusText, nextText, StringComparison.Ordinal) &&
                cancelledCount == nextCount)
            {
                return;
            }

            visualState = state;
            statusText = nextText;
            cancelledCount = nextCount;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Width < 30 || Height < 35)
                return;

            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            TablePalette palette = GetPalette(visualState);
            RectangleF body = new RectangleF(2F, 3F, Width - 5F, Height - 6F);

            if (!isPressed)
            {
                RectangleF shadowBounds = body;
                shadowBounds.Offset(0F, 2F);
                using (GraphicsPath shadowPath = CreateRoundedPath(shadowBounds, 12F))
                using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(22, 25, 38, 55)))
                    graphics.FillPath(shadowBrush, shadowPath);
            }

            using (GraphicsPath bodyPath = CreateRoundedPath(body, 12F))
            using (SolidBrush bodyBrush = new SolidBrush(
                isPressed ? Blend(palette.Background, palette.Accent, 0.10F) :
                isHovered ? Blend(palette.Background, palette.Accent, 0.045F) : palette.Background))
            using (Pen borderPen = new Pen(isHovered ? palette.Accent : palette.Border, isHovered ? 1.5F : 1F))
            {
                graphics.FillPath(bodyBrush, bodyPath);
                graphics.DrawPath(borderPen, bodyPath);
            }

            using (Pen accentPen = new Pen(palette.Accent, 3F))
            {
                accentPen.StartCap = LineCap.Round;
                accentPen.EndCap = LineCap.Round;
                graphics.DrawLine(accentPen, body.Left + 13F, body.Top + 6F, body.Right - 13F, body.Top + 6F);
            }

            RectangleF titleBounds = new RectangleF(
                body.Left + 8F, body.Top + 17F, body.Width - 16F, Math.Max(26F, body.Height - 54F));
            using (SolidBrush titleBrush = new SolidBrush(ForeColor))
                graphics.DrawString(string.IsNullOrWhiteSpace(Text) ? "Table" : Text,
                    TableNameFont, titleBrush, titleBounds, CenterFormat);

            DrawStatusPill(graphics, body, palette);

            if (cancelledCount > 0)
                DrawCancelledCount(graphics, body);
        }

        private void DrawStatusPill(Graphics graphics, RectangleF body, TablePalette palette)
        {
            string label = (string.IsNullOrWhiteSpace(statusText)
                ? GetDefaultStatusText(visualState)
                : statusText).ToUpperInvariant();

            float maxWidth = Math.Max(66F, body.Width - 22F);
            SizeF textSize = graphics.MeasureString(label, StatusFont);
            float pillWidth = Math.Min(maxWidth, Math.Max(66F, textSize.Width + 20F));
            float pillHeight = 18F;
            RectangleF pill = new RectangleF(
                body.Left + (body.Width - pillWidth) / 2F,
                body.Bottom - pillHeight - 9F,
                pillWidth,
                pillHeight);

            using (GraphicsPath pillPath = CreateRoundedPath(pill, 8F))
            using (SolidBrush pillBrush = new SolidBrush(palette.Badge))
            using (SolidBrush textBrush = new SolidBrush(Color.White))
            {
                graphics.FillPath(pillBrush, pillPath);
                graphics.DrawString(label, StatusFont, textBrush, pill, CenterFormat);
            }
        }

        private void DrawCancelledCount(Graphics graphics, RectangleF body)
        {
            const float diameter = 21F;
            RectangleF badge = new RectangleF(body.Right - diameter - 5F, body.Top + 8F, diameter, diameter);

            using (SolidBrush badgeBrush = new SolidBrush(Color.FromArgb(205, 48, 55)))
            using (Pen outline = new Pen(Color.White, 1.5F))
            using (SolidBrush textBrush = new SolidBrush(Color.White))
            {
                graphics.FillEllipse(badgeBrush, badge);
                graphics.DrawEllipse(outline, badge);
                graphics.DrawString(cancelledCount > 99 ? "99+" : cancelledCount.ToString(),
                    StatusFont, textBrush, badge, CenterFormat);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isHovered = false;
            isPressed = false;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPressed = true;
                Invalidate();
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isPressed = false;
            Invalidate();
            base.OnMouseUp(e);
        }

        private static string GetDefaultStatusText(ModernTableCardState state)
        {
            switch (state)
            {
                case ModernTableCardState.OrderTaken: return "Order Taken";
                case ModernTableCardState.Checkout: return "Checkout";
                case ModernTableCardState.Reservation: return "Reserved";
                case ModernTableCardState.ReservationExpired: return "Reservation Expired";
                case ModernTableCardState.CancelledItems: return "Cancelled Items";
                case ModernTableCardState.StaffCancelled: return "Staff Cancel Order";
                case ModernTableCardState.ReconfirmOrder: return "Reconfirm Order";
                default: return "Available";
            }
        }

        private static TablePalette GetPalette(ModernTableCardState state)
        {
            switch (state)
            {
                case ModernTableCardState.OrderTaken:
                    return new TablePalette(Color.FromArgb(239, 246, 255), Color.FromArgb(49, 121, 203),
                        Color.FromArgb(225, 237, 253), Color.FromArgb(43, 106, 179));
                case ModernTableCardState.Checkout:
                    return new TablePalette(Color.FromArgb(235, 249, 240), Color.FromArgb(46, 161, 97),
                        Color.FromArgb(222, 244, 230), Color.FromArgb(36, 132, 77));
                case ModernTableCardState.Reservation:
                    return new TablePalette(Color.FromArgb(246, 249, 234), Color.FromArgb(125, 157, 38),
                        Color.FromArgb(234, 242, 211), Color.FromArgb(91, 119, 26));
                case ModernTableCardState.ReservationExpired:
                    return new TablePalette(Color.FromArgb(255, 241, 241), Color.FromArgb(211, 66, 66),
                        Color.FromArgb(253, 226, 226), Color.FromArgb(174, 42, 42));
                case ModernTableCardState.CancelledItems:
                    return new TablePalette(Color.FromArgb(255, 244, 244), Color.FromArgb(213, 83, 83),
                        Color.FromArgb(255, 229, 229), Color.FromArgb(184, 52, 52));
                case ModernTableCardState.StaffCancelled:
                    return new TablePalette(Color.FromArgb(255, 248, 235), Color.FromArgb(203, 137, 31),
                        Color.FromArgb(252, 237, 207), Color.FromArgb(151, 97, 15));
                case ModernTableCardState.ReconfirmOrder:
                    return new TablePalette(Color.FromArgb(255, 245, 235), Color.FromArgb(220, 120, 47),
                        Color.FromArgb(252, 230, 213), Color.FromArgb(172, 81, 24));
                default:
                    return new TablePalette(Color.White, Color.FromArgb(156, 163, 175),
                        Color.FromArgb(229, 233, 239), Color.FromArgb(91, 101, 116));
            }
        }

        private static Color Blend(Color first, Color second, float amount)
        {
            amount = Math.Max(0F, Math.Min(1F, amount));
            return Color.FromArgb(
                first.A,
                (int)(first.R + (second.R - first.R) * amount),
                (int)(first.G + (second.G - first.G) * amount),
                (int)(first.B + (second.B - first.B) * amount));
        }

        private static GraphicsPath CreateRoundedPath(RectangleF bounds, float radius)
        {
            float diameter = Math.Min(radius * 2F, Math.Min(bounds.Width, bounds.Height));
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.Left, bounds.Top, diameter, diameter, 180F, 90F);
            path.AddArc(bounds.Right - diameter, bounds.Top, diameter, diameter, 270F, 90F);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0F, 90F);
            path.AddArc(bounds.Left, bounds.Bottom - diameter, diameter, diameter, 90F, 90F);
            path.CloseFigure();
            return path;
        }

        private sealed class TablePalette
        {
            public readonly Color Background;
            public readonly Color Accent;
            public readonly Color Border;
            public readonly Color Badge;

            public TablePalette(Color background, Color accent, Color border, Color badge)
            {
                Background = background;
                Accent = accent;
                Border = border;
                Badge = badge;
            }
        }
    }
}
