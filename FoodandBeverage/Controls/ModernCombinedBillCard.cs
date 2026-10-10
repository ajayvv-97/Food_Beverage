using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace FoodandBeverage.Controls
{
    /// <summary>
    /// Lightweight, owner-drawn combined-bill tile. Selection and order-opening actions
    /// are surfaced as events so the form retains responsibility for billing logic.
    /// </summary>
    internal sealed class ModernCombinedBillCard : Control
    {
        private static readonly Font TableCodeFont = new Font("Segoe UI", 10F, FontStyle.Bold);
        private static readonly Font AmountFont = new Font("Segoe UI", 9F, FontStyle.Bold);
        private static readonly Font ActionFont = new Font("Segoe UI", 7.2F, FontStyle.Bold);
        private static readonly StringFormat CenterFormat = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center,
            Trimming = StringTrimming.EllipsisCharacter,
            FormatFlags = StringFormatFlags.NoWrap
        };

        private string tableCode = string.Empty;
        private string amountText = string.Empty;
        private string actionText = "ADD";
        private bool isSelected;
        private bool isBooking;
        private bool isHovered;
        private bool isPressed;

        public event EventHandler SelectRequested;
        public event EventHandler OpenRequested;

        public ModernCombinedBillCard()
        {
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw |
                     ControlStyles.Selectable |
                     ControlStyles.SupportsTransparentBackColor, true);

            BackColor = Color.Transparent;
            ForeColor = Color.FromArgb(31, 41, 55);
            Cursor = Cursors.Hand;
            TabStop = false;
            AccessibleRole = AccessibleRole.PushButton;
        }

        public void SetBillData(string code, string amount, string accessibleDescription,
            int billType, bool booking, bool selected)
        {
            string nextCode = code ?? string.Empty;
            string nextAmount = amount ?? string.Empty;
            bool changed = tableCode != nextCode || amountText != nextAmount ||
                AccessibleDescription != accessibleDescription || Tag == null ||
                Convert.ToInt32(Tag) != billType || isBooking != booking || isSelected != selected;

            tableCode = nextCode;
            amountText = nextAmount;
            AccessibleDescription = accessibleDescription ?? string.Empty;
            Tag = billType;
            isBooking = booking;
            isSelected = selected;
            actionText = booking ? "BOOKING" : (selected ? "SELECTED" : "ADD");

            if (changed)
                Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Width < 70 || Height < 60)
                return;

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            Color accent = isBooking
                ? Color.FromArgb(125, 157, 38)
                : isSelected
                    ? Color.FromArgb(46, 161, 97)
                    : Color.FromArgb(156, 163, 175);
            Color background = isBooking
                ? Color.FromArgb(246, 249, 234)
                : isSelected
                    ? Color.FromArgb(235, 249, 240)
                    : Color.White;
            Color border = isBooking
                ? Color.FromArgb(225, 237, 190)
                : isSelected
                    ? Color.FromArgb(212, 238, 222)
                    : Color.FromArgb(229, 233, 239);
            Color statusBackground = isBooking
                ? Color.FromArgb(91, 119, 26)
                : isSelected
                    ? Color.FromArgb(36, 132, 77)
                    : Color.FromArgb(91, 101, 116);

            RectangleF card = new RectangleF(2F, 3F, Width - 5F, Height - 6F);
            if (!isPressed)
            {
                RectangleF shadow = card;
                shadow.Offset(0F, 2F);
                using (GraphicsPath shadowPath = RoundedPath(shadow, 12F))
                using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(22, 25, 38, 55)))
                    g.FillPath(shadowBrush, shadowPath);
            }

            using (GraphicsPath path = RoundedPath(card, 12F))
            using (SolidBrush fill = new SolidBrush(
                isPressed ? Blend(background, accent, 0.10F) :
                isHovered ? Blend(background, accent, 0.045F) : background))
            using (Pen outline = new Pen(isHovered ? accent : border, isHovered ? 1.5F : 1F))
            {
                g.FillPath(fill, path);
                g.DrawPath(outline, path);
            }

            using (Pen accentLine = new Pen(accent, 3F))
            {
                accentLine.StartCap = LineCap.Round;
                accentLine.EndCap = LineCap.Round;
                g.DrawLine(accentLine, card.Left + 13F, card.Top + 6F, card.Right - 13F, card.Top + 6F);
            }

            RectangleF codeBounds = new RectangleF(card.Left + 7F, card.Top + 17F,
                card.Width - 14F, Math.Max(22F, card.Height * 0.30F));
            using (SolidBrush codeBrush = new SolidBrush(ForeColor))
                g.DrawString(string.IsNullOrWhiteSpace(tableCode) ? "Table" : tableCode,
                    TableCodeFont, codeBrush, codeBounds, CenterFormat);

            RectangleF amountBounds = new RectangleF(card.Left + 7F, card.Top + 43F,
                card.Width - 14F, Math.Max(18F, card.Height * 0.22F));
            using (SolidBrush amountBrush = new SolidBrush(
                isBooking ? Color.FromArgb(91, 119, 26) : Color.FromArgb(36, 91, 156)))
                g.DrawString(amountText, AmountFont, amountBrush, amountBounds, CenterFormat);

            float actionWidth = Math.Min(card.Width - 18F, Math.Max(70F, card.Width * 0.70F));
            RectangleF action = new RectangleF(card.Left + (card.Width - actionWidth) / 2F,
                card.Bottom - 27F, actionWidth, 18F);
            using (GraphicsPath actionPath = RoundedPath(action, 8F))
            using (SolidBrush actionFill = new SolidBrush(statusBackground))
            using (SolidBrush actionTextBrush = new SolidBrush(Color.White))
            {
                g.FillPath(actionFill, actionPath);
                g.DrawString(actionText, ActionFont, actionTextBrush, action, CenterFormat);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            // Routing is handled once from OnMouseUp, where the release position
            // is available. Do not also raise open/selection from Click: WinForms
            // raises Click after MouseUp and that would execute both actions.
            base.OnClick(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            // Intentionally no routing here. The decision was made in OnMouseUp.
            base.OnMouseClick(e);
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

            // Use the release point so dragging from another spot onto the badge
            // doesn't accidentally toggle selection.
            bool isLeftClick = e.Button == MouseButtons.Left;
            Point releasePoint = e.Location;

            base.OnMouseUp(e);

            if (!isLeftClick || IsDisposed || Disposing)
                return;

            if (isBooking)
            {
                OpenRequested?.Invoke(this, EventArgs.Empty);
                return;
            }

            RectangleF card = new RectangleF(2F, 3F, Width - 5F, Height - 6F);
            float actionWidth = Math.Min(card.Width - 18F, Math.Max(70F, card.Width * 0.70F));
            RectangleF action = new RectangleF(
                card.Left + (card.Width - actionWidth) / 2F,
                card.Bottom - 27F,
                actionWidth,
                18F);

            if (action.Contains(releasePoint))
            {
                // ADD / SELECTED badge: only toggle selection.
                SelectRequested?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                // Anywhere else on the card: open the order page.
                OpenRequested?.Invoke(this, EventArgs.Empty);
            }
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
    }
}
