using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class LoadingGif : Form
    {
        private readonly Timer animationTimer;
        private int animationAngle;

        public LoadingGif()
        {
            InitializeComponent();

            // Replace only the old GIF presentation; keep existing callers unchanged.
            pb_loadingimg.Visible = false;
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            ClientSize = new Size(330, 142);
            BackColor = Color.FromArgb(250, 248, 255);

            animationTimer = new Timer { Interval = 45 };
            animationTimer.Tick += delegate
            {
                animationAngle = (animationAngle + 12) % 360;
                Invalidate();
            };
        }

        private async void LoadingGif_LoadAsync(object sender, EventArgs e)
        {
            animationTimer.Start();

            // Preserve the existing short display duration used by FrmOrderConsole.
            await System.Threading.Tasks.Task.Delay(500);

            if (!IsDisposed)
                Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle card = new Rectangle(1, 1, ClientSize.Width - 3, ClientSize.Height - 3);
            using (GraphicsPath path = CreateRoundedPath(card, 20))
            using (SolidBrush background = new SolidBrush(Color.White))
            using (Pen border = new Pen(Color.FromArgb(225, 210, 255), 1.3f))
            {
                g.FillPath(background, path);
                g.DrawPath(border, path);
            }

            PointF center = new PointF(53, 57);
            const int segmentCount = 12;
            for (int i = 0; i < segmentCount; i++)
            {
                int angle = (i * 360 / segmentCount + animationAngle) % 360;
                int alpha = 65 + i * 16;
                using (Pen pen = new Pen(Color.FromArgb(alpha, 139, 55, 226), 4.5f))
                {
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    double radians = angle * Math.PI / 180.0;
                    g.DrawLine(pen,
                        center.X + (float)Math.Cos(radians) * 17f,
                        center.Y + (float)Math.Sin(radians) * 17f,
                        center.X + (float)Math.Cos(radians) * 25f,
                        center.Y + (float)Math.Sin(radians) * 25f);
                }
            }

            using (Font titleFont = new Font("Segoe UI", 11F, FontStyle.Bold))
            using (Font subtitleFont = new Font("Segoe UI", 8.5F, FontStyle.Regular))
            using (SolidBrush titleBrush = new SolidBrush(Color.FromArgb(43, 26, 82)))
            using (SolidBrush subtitleBrush = new SolidBrush(Color.FromArgb(125, 112, 151)))
            {
                g.DrawString("Preparing your workspace", titleFont, titleBrush, 94, 43);
                g.DrawString("Loading order console…", subtitleFont, subtitleBrush, 95, 68);
            }
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            animationTimer.Stop();
            animationTimer.Dispose();
            base.OnFormClosed(e);
        }

        private static GraphicsPath CreateRoundedPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
