using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FoodandBeverage.Controls
{
    public enum KitchenOrderVisualState
    {
        NewOrder,
        InProgress,
        Ready,
        WaitingForCustomer
    }

    public enum KitchenActionStyle
    {
        Start,
        Ready,
        Cancel,
        Reprint,
        Okay
    }

    /// <summary>
    /// Small rounded state pill displayed on the KOT ticket header.
    /// </summary>
    public sealed class ModernKitchenStatusLabel : Label
    {
        public ModernKitchenStatusLabel()
        {
            AutoSize = false;
            TextAlign = ContentAlignment.MiddleCenter;
            BorderStyle = BorderStyle.None;
            Margin = new Padding(4, 5, 4, 5);
            Padding = new Padding(5, 0, 5, 0);
            Font = ModernKitchenOrderCard.StatusFont;
            BackColor = Color.FromArgb(238, 230, 250);
            ForeColor = Color.FromArgb(76, 45, 128);
            UseCompatibleTextRendering = false;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (Width <= 0 || Height <= 0)
                return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath path = new GraphicsPath())
            using (SolidBrush brush = new SolidBrush(BackColor))
            {
                float diameter = Math.Min(Height, Width);
                path.AddArc(0, 0, diameter, Height, 90F, 180F);
                path.AddArc(Width - diameter, 0, diameter, Height, 270F, 180F);
                path.CloseFigure();
                e.Graphics.FillPath(brush, path);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            TextRenderer.DrawText(
                e.Graphics, Text, Font, ClientRectangle, ForeColor,
                TextFormatFlags.HorizontalCenter |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis |
                TextFormatFlags.SingleLine);
        }
    }

    /// <summary>
    /// Ticket-style KOT container. It remains a TableLayoutPanel so existing
    /// Controls.Find, grid lookup and cached-card workflows keep working.
    /// </summary>
    public sealed class ModernKitchenOrderCard : TableLayoutPanel
    {
        public static readonly Color TicketPurple = Color.FromArgb(77, 43, 135);
        private Color accentColor = Color.FromArgb(125, 82, 190);
        private Region roundedRegion;

        internal static readonly Font HeaderFont = new Font("Segoe UI", 11F, FontStyle.Bold);
        internal static readonly Font MetaFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        internal static readonly Font GridHeaderFont = new Font("Segoe UI", 9F, FontStyle.Bold);
        internal static readonly Font GridFont = new Font("Segoe UI", 9F, FontStyle.Regular);
        internal static readonly Font StatusFont = new Font("Segoe UI", 8F, FontStyle.Bold);
        internal static readonly Font DetailFont = new Font("Segoe UI", 9F, FontStyle.Regular);

        public ModernKitchenOrderCard()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            BackColor = Color.White;
            ForeColor = Color.FromArgb(29, 25, 45);
            CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            Margin = new Padding(8);
            Padding = new Padding(7);
            Dock = DockStyle.Top;
            ColumnCount = 4;
            RowCount = 5;

            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));

            RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            RowStyles.Add(new RowStyle(SizeType.Absolute, 66F));
            RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            RowStyles.Add(new RowStyle(SizeType.Absolute, 52F));
            RowStyles.Add(new RowStyle(SizeType.Absolute, 104F));
        }

        public void SetKitchenState(KitchenOrderVisualState state)
        {
            Color statusBack;
            Color statusFore;
            string statusText;

            switch (state)
            {
                case KitchenOrderVisualState.WaitingForCustomer:
                    statusBack = Color.FromArgb(255, 244, 224);
                    statusFore = Color.FromArgb(150, 85, 15);
                    statusText = "WAITING";
                    break;
                case KitchenOrderVisualState.Ready:
                    statusBack = Color.FromArgb(224, 246, 232);
                    statusFore = Color.FromArgb(28, 112, 70);
                    statusText = "READY";
                    break;
                case KitchenOrderVisualState.InProgress:
                    statusBack = Color.FromArgb(226, 237, 255);
                    statusFore = Color.FromArgb(41, 83, 150);
                    statusText = "IN PROGRESS";
                    break;
                default:
                    statusBack = Color.FromArgb(238, 230, 250);
                    statusFore = TicketPurple;
                    statusText = "NEW";
                    break;
            }

            accentColor = Color.FromArgb(125, 82, 190);
            foreach (Control control in Controls)
            {
                Label label = control as Label;
                if (label == null)
                    continue;

                if (label.Name.StartsWith("lbl_status_K_", StringComparison.Ordinal))
                {
                    label.BackColor = statusBack;
                    label.ForeColor = statusFore;
                    label.Text = statusText;
                }
                else if (label.Name.StartsWith("lbl_kot_title_", StringComparison.Ordinal))
                {
                    label.BackColor = TicketPurple;
                    label.ForeColor = Color.White;
                }
                else if (label.Name.StartsWith("lbl_tablecod_k_", StringComparison.Ordinal) ||
                         label.Name.StartsWith("lbl_tokkenno_k_", StringComparison.Ordinal) ||
                         label.Name.StartsWith("lbl_order_type_k_", StringComparison.Ordinal) ||
                         label.Name.StartsWith("lbl_time_K_", StringComparison.Ordinal))
                {
                    label.BackColor = Color.FromArgb(244, 239, 251);
                    label.ForeColor = Color.FromArgb(49, 31, 83);
                }
            }

            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateRoundedRegion();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Color parentColor = Parent == null ? SystemColors.Control : Parent.BackColor;
            using (SolidBrush parentBrush = new SolidBrush(parentColor))
                e.Graphics.FillRectangle(parentBrush, ClientRectangle);

            Rectangle bounds = new Rectangle(1, 1, Math.Max(1, Width - 3), Math.Max(1, Height - 3));
            using (GraphicsPath shadowPath = CreateRoundedPath(
                new Rectangle(3, 4, Math.Max(1, bounds.Width), Math.Max(1, bounds.Height)), 15F))
            using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(28, 48, 28, 78)))
                e.Graphics.FillPath(shadowBrush, shadowPath);

            using (GraphicsPath path = CreateRoundedPath(bounds, 15F))
            using (SolidBrush cardBrush = new SolidBrush(BackColor))
                e.Graphics.FillPath(cardBrush, path);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Width < 10 || Height < 10)
                return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle bounds = new Rectangle(1, 1, Width - 3, Height - 3);
            using (GraphicsPath path = CreateRoundedPath(bounds, 15F))
            using (Pen border = new Pen(Color.FromArgb(220, 210, 236), 1F))
            using (Pen accent = new Pen(accentColor, 2F))
            using (Pen separator = new Pen(Color.FromArgb(137, 98, 190), 1F))
            {
                separator.DashStyle = DashStyle.Dash;
                e.Graphics.DrawPath(border, path);
                accent.StartCap = LineCap.Round;
                accent.EndCap = LineCap.Round;
                e.Graphics.DrawLine(accent, 15F, 2.5F, Math.Max(15F, Width - 16F), 2.5F);

                int[] rowHeights = GetRowHeights();
                if (rowHeights != null && rowHeights.Length >= 4)
                {
                    int metaBottom = rowHeights[0] + rowHeights[1] + Padding.Top;
                    e.Graphics.DrawLine(separator, 14, metaBottom, Width - 14, metaBottom);
                    int notesTop = metaBottom + rowHeights[2];
                    e.Graphics.DrawLine(separator, 14, notesTop, Width - 14, notesTop);
                }
            }
        }

        private void UpdateRoundedRegion()
        {
            if (Width <= 2 || Height <= 2)
                return;

            Rectangle bounds = new Rectangle(0, 0, Width, Height);
            using (GraphicsPath path = CreateRoundedPath(bounds, 15F))
            {
                Region next = new Region(path);
                Region = next;

                if (roundedRegion != null)
                    roundedRegion.Dispose();

                roundedRegion = next;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && roundedRegion != null)
            {
                if (ReferenceEquals(Region, roundedRegion))
                    Region = null;

                roundedRegion.Dispose();
                roundedRegion = null;
            }

            base.Dispose(disposing);
        }

        private static GraphicsPath CreateRoundedPath(Rectangle bounds, float radius)
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

    /// <summary>
    /// Vector-drawn action button. PictureBox inheritance preserves the existing
    /// click handlers and their Tag / AccessibleDescription lookup.
    /// </summary>
    public sealed class ModernKitchenActionButton : PictureBox
    {
        private bool isHovered;
        private bool isPressed;
        private static readonly Font ButtonFont = new Font("Segoe UI", 9.5F, FontStyle.Bold);
        private static readonly Font CompactButtonFont = new Font("Segoe UI", 8F, FontStyle.Bold);

        public string ButtonText { get; set; }
        public KitchenActionStyle ActionStyle { get; set; }

        public ModernKitchenActionButton()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor,
                true);
            BackColor = Color.Transparent;
            SizeMode = PictureBoxSizeMode.Normal;
            ButtonText = string.Empty;
            Cursor = Cursors.Hand;
            TabStop = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Width <= 4 || Height <= 4)
                return;

            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle bounds = new Rectangle(1, 1, Width - 3, Height - 3);
            Color top;
            Color bottom;
            GetButtonColors(out top, out bottom);

            if (isHovered && Enabled)
            {
                top = ControlPaint.Light(top, 0.12F);
                bottom = ControlPaint.Light(bottom, 0.10F);
            }
            if (isPressed && Enabled)
            {
                top = ControlPaint.Dark(top, 0.08F);
                bottom = ControlPaint.Dark(bottom, 0.08F);
            }

            using (GraphicsPath path = CreateRoundedPath(bounds, 10F))
            using (LinearGradientBrush gradient = new LinearGradientBrush(
                bounds, top, bottom, LinearGradientMode.Vertical))
            using (Pen border = new Pen(ControlPaint.Dark(bottom, 0.08F), 1F))
            {
                graphics.FillPath(gradient, path);
                graphics.DrawPath(border, path);
            }

            Rectangle iconBounds = new Rectangle(10, (Height - 23) / 2, 23, 23);
            using (Pen iconPen = new Pen(Color.White, 2.1F))
            using (SolidBrush iconBrush = new SolidBrush(Color.White))
            {
                iconPen.StartCap = LineCap.Round;
                iconPen.EndCap = LineCap.Round;
                DrawActionIcon(graphics, iconPen, iconBrush, iconBounds);
            }

            Rectangle textBounds = new Rectangle(
                iconBounds.Right + 8, 0,
                Math.Max(0, Width - iconBounds.Right - 12), Height);

            TextRenderer.DrawText(
                graphics,
                ButtonText ?? string.Empty,
                Width < 155 ? CompactButtonFont : ButtonFont,
                textBounds,
                Enabled ? Color.White : Color.FromArgb(205, 205, 210),
                TextFormatFlags.HorizontalCenter |
                TextFormatFlags.VerticalCenter |
                TextFormatFlags.EndEllipsis |
                TextFormatFlags.SingleLine);
        }

        private void GetButtonColors(out Color top, out Color bottom)
        {
            switch (ActionStyle)
            {
                case KitchenActionStyle.Cancel:
                    top = Color.FromArgb(239, 55, 55);
                    bottom = Color.FromArgb(178, 22, 36);
                    break;
                case KitchenActionStyle.Reprint:
                    top = Color.FromArgb(74, 78, 94);
                    bottom = Color.FromArgb(39, 43, 57);
                    break;
                case KitchenActionStyle.Ready:
                    top = Color.FromArgb(117, 66, 180);
                    bottom = Color.FromArgb(76, 34, 132);
                    break;
                case KitchenActionStyle.Okay:
                    top = Color.FromArgb(119, 62, 190);
                    bottom = Color.FromArgb(70, 24, 125);
                    break;
                default:
                    top = Color.FromArgb(117, 66, 180);
                    bottom = Color.FromArgb(76, 34, 132);
                    break;
            }

            if (!Enabled)
            {
                top = Color.FromArgb(150, top);
                bottom = Color.FromArgb(150, bottom);
            }
        }

        private void DrawActionIcon(Graphics graphics, Pen pen, Brush brush, Rectangle bounds)
        {
            switch (ActionStyle)
            {
                case KitchenActionStyle.Start:
                    graphics.DrawEllipse(pen, bounds);
                    graphics.FillPolygon(brush, new[]
                    {
                        new Point(bounds.Left + 9, bounds.Top + 6),
                        new Point(bounds.Left + 17, bounds.Top + 11),
                        new Point(bounds.Left + 9, bounds.Top + 17)
                    });
                    break;

                case KitchenActionStyle.Ready:
                case KitchenActionStyle.Okay:
                    graphics.DrawEllipse(pen, bounds);
                    graphics.DrawLines(pen, new[]
                    {
                        new Point(bounds.Left + 5, bounds.Top + 12),
                        new Point(bounds.Left + 10, bounds.Top + 17),
                        new Point(bounds.Left + 18, bounds.Top + 7)
                    });
                    break;

                case KitchenActionStyle.Cancel:
                    graphics.DrawEllipse(pen, bounds);
                    graphics.DrawLine(pen, bounds.Left + 7, bounds.Top + 7,
                        bounds.Right - 7, bounds.Bottom - 7);
                    graphics.DrawLine(pen, bounds.Right - 7, bounds.Top + 7,
                        bounds.Left + 7, bounds.Bottom - 7);
                    break;

                case KitchenActionStyle.Reprint:
                    graphics.DrawRectangle(pen, bounds.Left + 3, bounds.Top + 7, 17, 10);
                    graphics.DrawRectangle(pen, bounds.Left + 6, bounds.Top + 2, 11, 7);
                    graphics.DrawRectangle(pen, bounds.Left + 6, bounds.Top + 15, 11, 7);
                    graphics.DrawLine(pen, bounds.Left + 8, bounds.Top + 18,
                        bounds.Right - 7, bounds.Top + 18);
                    break;
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
                isPressed = true;
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isPressed = false;
            Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            Invalidate();
        }

        private static GraphicsPath CreateRoundedPath(Rectangle bounds, float radius)
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
