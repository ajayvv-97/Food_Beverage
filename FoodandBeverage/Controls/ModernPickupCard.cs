using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace FoodandBeverage.Controls
{
    /// <summary>
    /// A lightweight owner-drawn pickup-order card. It contains no child labels or panels.
    /// A card is updated in-place as the pickup order's status changes.
    /// </summary>
    internal sealed class ModernPickupCard : Control
    {
        private enum PickupVisualStatus
        {
            OrderSent,
            CookingStarted,
            CancelledByKitchen
        }

        private static readonly Font HeaderFont = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        private static readonly Font TokenFont = new Font("Segoe UI", 13F, FontStyle.Bold);
        private static readonly Font StatusFont = new Font("Segoe UI", 7.1F, FontStyle.Bold);
        private static readonly Font CountFont = new Font("Segoe UI", 7F, FontStyle.Bold);
        private static readonly StringFormat CenterFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            Trimming = StringTrimming.EllipsisCharacter,
            FormatFlags = StringFormatFlags.NoWrap
        };

        private PickupVisualStatus visualStatus = PickupVisualStatus.OrderSent;
        private string tokenNumber = string.Empty;
        private int cancelledItemCount;
        private bool isHovered;
        private bool isPressed;

        public ModernPickupCard()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable |
                ControlStyles.SupportsTransparentBackColor,
                true);

            // Leave the area outside the rounded card transparent so the host's
            // lavender background shows through (no unwanted square tile).
            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(31, 41, 55);
            Cursor = Cursors.Hand;
            TabStop = false;
            AccessibleRole = AccessibleRole.PushButton;
        }

        public void SetPickupData(string token, int orderStatus, int cancelCount, bool cancelledByKitchen)
        {
            PickupVisualStatus nextStatus = cancelledByKitchen
                ? PickupVisualStatus.CancelledByKitchen
                : (orderStatus == 1 ? PickupVisualStatus.CookingStarted : PickupVisualStatus.OrderSent);

            string nextToken = string.IsNullOrWhiteSpace(token) ? "—" : token.Trim();
            int nextCancelCount = Math.Max(0, cancelCount);

            if (visualStatus == nextStatus &&
                string.Equals(tokenNumber, nextToken, StringComparison.Ordinal) &&
                cancelledItemCount == nextCancelCount)
                return;

            visualStatus = nextStatus;
            tokenNumber = nextToken;
            cancelledItemCount = nextCancelCount;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Width < 70 || Height < 55)
                return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            PickupPalette palette = GetPalette(visualStatus, cancelledItemCount > 0);
            RectangleF card = new RectangleF(2F, 3F, Width - 5F, Height - 6F);

            if (!isPressed)
            {
                RectangleF shadow = card;
                shadow.Offset(0F, 2F);
                using (GraphicsPath shadowPath = RoundedPath(shadow, 12F))
                using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(22, 30, 41, 59)))
                    g.FillPath(shadowBrush, shadowPath);
            }

            using (GraphicsPath cardPath = RoundedPath(card, 12F))
            using (SolidBrush cardBrush = new SolidBrush(
                isPressed ? Blend(palette.Background, palette.Accent, 0.10F) :
                isHovered ? Blend(palette.Background, palette.Accent, 0.045F) : palette.Background))
            using (Pen border = new Pen(isHovered ? palette.Accent : palette.Border, isHovered ? 1.5F : 1F))
            {
                g.FillPath(cardBrush, cardPath);
                g.DrawPath(border, cardPath);
            }

            using (Pen accent = new Pen(palette.Accent, 3F))
            {
                accent.StartCap = LineCap.Round;
                accent.EndCap = LineCap.Round;
                g.DrawLine(accent, card.Left + 11F, card.Top + 7F, card.Right - 11F, card.Top + 7F);
            }

            DrawPickupIcon(g, card.Left + 12F, card.Top + 16F, palette.Accent);

            RectangleF header = new RectangleF(card.Left + 29F, card.Top + 12F, card.Width - 40F, 20F);
            using (SolidBrush headerBrush = new SolidBrush(palette.MutedText))
                g.DrawString("PICKUP ORDER", HeaderFont, headerBrush, header, CenterFormat);

            RectangleF token = new RectangleF(card.Left + 8F, card.Top + 30F, card.Width - 16F, 27F);
            using (SolidBrush tokenBrush = new SolidBrush(palette.TokenText))
                g.DrawString("TOKEN  " + tokenNumber, TokenFont, tokenBrush, token, CenterFormat);

            string statusLabel = GetStatusLabel(visualStatus, cancelledItemCount);
            RectangleF statusPill = new RectangleF(card.Left + 8F, card.Bottom - 27F, card.Width - 16F, 18F);

            using (GraphicsPath pillPath = RoundedPath(statusPill, 8F))
            using (SolidBrush pillBrush = new SolidBrush(palette.StatusBackground))
            using (SolidBrush pillText = new SolidBrush(palette.StatusText))
            {
                g.FillPath(pillBrush, pillPath);
                g.DrawString(statusLabel, StatusFont, pillText, statusPill, CenterFormat);
            }

            if (cancelledItemCount > 0)
                DrawCancelledBadge(g, card);
        }

        private void DrawPickupIcon(Graphics g, float x, float y, Color color)
        {
            using (Pen pen = new Pen(color, 1.5F))
            {
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;
                g.DrawLine(pen, x + 1F, y + 5F, x + 13F, y + 5F);
                g.DrawLine(pen, x + 2F, y + 5F, x + 3F, y + 16F);
                g.DrawLine(pen, x + 12F, y + 5F, x + 11F, y + 16F);
                g.DrawLine(pen, x + 3F, y + 16F, x + 11F, y + 16F);
                g.DrawArc(pen, x + 4F, y, 6F, 9F, 180F, 180F);
            }
        }

        private void DrawCancelledBadge(Graphics g, RectangleF card)
        {
            string text = cancelledItemCount > 99 ? "99+" : cancelledItemCount.ToString();
            SizeF measured = g.MeasureString(text, CountFont);
            float width = Math.Max(21F, measured.Width + 9F);
            RectangleF badge = new RectangleF(card.Right - width - 7F, card.Top + 11F, width, 17F);

            using (GraphicsPath badgePath = RoundedPath(badge, 8F))
            using (SolidBrush badgeBrush = new SolidBrush(Color.FromArgb(205, 48, 55)))
            using (SolidBrush badgeText = new SolidBrush(Color.White))
            {
                g.FillPath(badgeBrush, badgePath);
                g.DrawString(text, CountFont, badgeText, badge, CenterFormat);
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

        private static string GetStatusLabel(PickupVisualStatus status, int cancelledItems)
        {
            if (status == PickupVisualStatus.CancelledByKitchen)
                return "CANCELLED BY KITCHEN";

            if (cancelledItems > 0)
                return "CANCELLED ITEMS: " + cancelledItems;

            return status == PickupVisualStatus.CookingStarted
                ? "PAID  •  COOKING STARTED"
                : "PAID  •  ORDER SENT";
        }

        private static readonly PickupPalette OrderSentPalette =
            new PickupPalette(Color.FromArgb(239, 246, 255), Color.FromArgb(49, 121, 203),
                Color.FromArgb(43, 106, 179), Color.White, Color.FromArgb(31, 41, 55));
        private static readonly PickupPalette CookingPalette =
            new PickupPalette(Color.FromArgb(235, 249, 240), Color.FromArgb(46, 161, 97),
                Color.FromArgb(36, 132, 77), Color.White, Color.FromArgb(31, 41, 55));
        private static readonly PickupPalette CancelledItemsPalette =
            new PickupPalette(Color.FromArgb(255, 244, 244), Color.FromArgb(213, 83, 83),
                Color.FromArgb(184, 52, 52), Color.White, Color.FromArgb(31, 41, 55));
        private static readonly PickupPalette CancelledPalette =
            new PickupPalette(Color.FromArgb(255, 241, 241), Color.FromArgb(211, 66, 66),
                Color.FromArgb(174, 42, 42), Color.White, Color.FromArgb(125, 31, 31));

        private static PickupPalette GetPalette(PickupVisualStatus status, bool hasCancelledItems)
        {
            if (status == PickupVisualStatus.CancelledByKitchen)
                return CancelledPalette;

            if (hasCancelledItems)
                return CancelledItemsPalette;

            return status == PickupVisualStatus.CookingStarted
                ? CookingPalette
                : OrderSentPalette;
        }

        private static Color Blend(Color first, Color second, float amount)
        {
            amount = Math.Max(0F, Math.Min(1F, amount));
            return Color.FromArgb(first.A,
                (int)(first.R + (second.R - first.R) * amount),
                (int)(first.G + (second.G - first.G) * amount),
                (int)(first.B + (second.B - first.B) * amount));
        }

        private static GraphicsPath RoundedPath(RectangleF bounds, float radius)
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

        private sealed class PickupPalette
        {
            public readonly Color Background;
            public readonly Color Accent;
            public readonly Color Border;
            public readonly Color StatusBackground;
            public readonly Color StatusText;
            public readonly Color TokenText;
            public readonly Color MutedText;

            public PickupPalette(Color background, Color accent, Color statusBackground,
                Color statusText, Color tokenText)
            {
                Background = background;
                Accent = accent;
                Border = ControlPaint.Light(accent, 0.65F);
                StatusBackground = statusBackground;
                StatusText = statusText;
                TokenText = tokenText;
                MutedText = Color.FromArgb(91, 101, 116);
            }
        }
    }
}
