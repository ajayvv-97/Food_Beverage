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

    /// <summary>
    /// Rounded kitchen ticket container. It derives from TableLayoutPanel so the existing
    /// kitchen handlers and Controls.Find lookups continue to work, while keeping all
    /// order controls directly on one container instead of nested header/button/description panels.
    /// </summary>
    public sealed class ModernKitchenOrderCard : TableLayoutPanel
    {
        private Color accentColor = Color.FromArgb(56, 112, 186);
        private Color headerColor = Color.FromArgb(60, 90, 160);
        private Region roundedRegion;

        internal static readonly Font TableCodeFont = new Font("Segoe UI", 10F, FontStyle.Bold);
        internal static readonly Font TokenFont = new Font("Segoe UI", 10F, FontStyle.Bold);
        internal static readonly Font StatusFont = new Font("Segoe UI", 8F, FontStyle.Bold);
        internal static readonly Font TimeFont = new Font("Segoe UI", 7.5F, FontStyle.Bold);

        public ModernKitchenOrderCard()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            BackColor = Color.White;
            ForeColor = Color.FromArgb(31, 41, 55);
            CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
            GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            Margin = new Padding(12, 8, 8, 0);
            Padding = new Padding(0);
            Dock = DockStyle.Top;
            ColumnCount = 4;
            RowCount = 4;

            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 22F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 24F));

            RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            RowStyles.Add(new RowStyle(SizeType.Absolute, 0F));
            RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
        }

        public void SetKitchenState(KitchenOrderVisualState state)
        {
            switch (state)
            {
                case KitchenOrderVisualState.WaitingForCustomer:
                    accentColor = Color.FromArgb(208, 132, 35);
                    headerColor = Color.FromArgb(146, 88, 21);
                    break;
                case KitchenOrderVisualState.Ready:
                    accentColor = Color.FromArgb(42, 151, 91);
                    headerColor = Color.FromArgb(35, 126, 77);
                    break;
                case KitchenOrderVisualState.InProgress:
                    accentColor = Color.FromArgb(47, 132, 203);
                    headerColor = Color.FromArgb(44, 96, 163);
                    break;
                default:
                    accentColor = Color.FromArgb(56, 112, 186);
                    headerColor = Color.FromArgb(60, 90, 160);
                    break;
            }

            foreach (Control control in Controls)
            {
                Label label = control as Label;
                if (label == null)
                    continue;

                if (label.Name.StartsWith("lbl_status_K_", StringComparison.Ordinal))
                {
                    label.BackColor = Color.White;
                    label.ForeColor = headerColor;
                    switch (state)
                    {
                        case KitchenOrderVisualState.WaitingForCustomer:
                            label.Text = "WAITING CUSTOMER";
                            break;
                        case KitchenOrderVisualState.Ready:
                            label.Text = "READY";
                            break;
                        case KitchenOrderVisualState.InProgress:
                            label.Text = "IN PROGRESS";
                            break;
                        default:
                            label.Text = "NEW ORDER";
                            break;
                    }
                }
                else if (label.Name.StartsWith("lbl_tablecod_k_", StringComparison.Ordinal) ||
                         label.Name.StartsWith("lbl_tokkenno_k_", StringComparison.Ordinal) ||
                         label.Name.StartsWith("lbl_time_K_", StringComparison.Ordinal))
                {
                    label.BackColor = headerColor;
                    label.ForeColor = Color.White;
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
            using (GraphicsPath path = CreateRoundedPath(bounds, 13F))
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
            using (GraphicsPath path = CreateRoundedPath(bounds, 13F))
            using (Pen border = new Pen(Color.FromArgb(218, 225, 235), 1F))
            using (Pen accent = new Pen(accentColor, 3F))
            {
                e.Graphics.DrawPath(border, path);
                accent.StartCap = LineCap.Round;
                accent.EndCap = LineCap.Round;
                e.Graphics.DrawLine(accent, 13F, 2.5F, Math.Max(13F, Width - 14F), 2.5F);
            }
        }

        private void UpdateRoundedRegion()
        {
            if (Width <= 2 || Height <= 2)
                return;

            Rectangle bounds = new Rectangle(0, 0, Width, Height);
            using (GraphicsPath path = CreateRoundedPath(bounds, 13F))
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
}
