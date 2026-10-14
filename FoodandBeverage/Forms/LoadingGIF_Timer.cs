using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class LoadingGIF_Timer : Form
    {
        private readonly Timer animationTimer;
        private int animationAngle;
        private string titleText = "Preparing your workspace";
        private string statusText = "Loading dashboard…";

        public string TitleText
        {
            get { return titleText; }
            set { titleText = string.IsNullOrWhiteSpace(value) ? "Please wait" : value.Trim(); Invalidate(); }
        }

        public string StatusText
        {
            get { return statusText; }
            set { statusText = string.IsNullOrWhiteSpace(value) ? "Working…" : value.Trim(); Invalidate(); }
        }

        public LoadingGIF_Timer()
        {
            InitializeComponent();

            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            TopMost = true;
            ClientSize = new Size(330, 142);
            BackColor = Color.FromArgb(250, 248, 255);

            animationTimer = new Timer { Interval = 45 };
            animationTimer.Tick += delegate
            {
                animationAngle = (animationAngle + 12) % 360;
                Invalidate();
            };

            Resize += delegate { UpdateRoundedRegion(); };
            UpdateRoundedRegion();
        }

        private void LoadingGIF_Timer_Load(object sender, EventArgs e)
        {
            animationTimer.Start();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            animationTimer.Stop();
            animationTimer.Dispose();
            base.OnFormClosed(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Rectangle card = new Rectangle(1, 1, ClientSize.Width - 3, ClientSize.Height - 3);
            using (GraphicsPath cardPath = CreateRoundedPath(card, 20))
            using (SolidBrush background = new SolidBrush(Color.White))
            using (Pen border = new Pen(Color.FromArgb(225, 210, 255), 1.3f))
            {
                g.FillPath(background, cardPath);
                g.DrawPath(border, cardPath);
            }

            // Modern rotating purple progress ring.
            PointF center = new PointF(53, 57);
            const int segmentCount = 12;
            for (int i = 0; i < segmentCount; i++)
            {
                int relative = (i * 360 / segmentCount + animationAngle) % 360;
                int shade = 70 + (i * 15);
                int alpha = 65 + (i * 16);
                using (Pen pen = new Pen(Color.FromArgb(Math.Min(255, alpha),
                    Math.Min(255, 95 + shade / 3),
                    Math.Min(255, 35 + shade / 2),
                    225), 4.5f))
                {
                    pen.StartCap = LineCap.Round;
                    pen.EndCap = LineCap.Round;
                    double radians = relative * Math.PI / 180.0;
                    float inner = 17f;
                    float outer = 25f;
                    g.DrawLine(pen,
                        center.X + (float)Math.Cos(radians) * inner,
                        center.Y + (float)Math.Sin(radians) * inner,
                        center.X + (float)Math.Cos(radians) * outer,
                        center.Y + (float)Math.Sin(radians) * outer);
                }
            }

            using (Font titleFont = new Font("Segoe UI", 11F, FontStyle.Bold))
            using (Font subtitleFont = new Font("Segoe UI", 8.5F, FontStyle.Regular))
            using (SolidBrush titleBrush = new SolidBrush(Color.FromArgb(43, 26, 82)))
            using (SolidBrush subtitleBrush = new SolidBrush(Color.FromArgb(125, 112, 151)))
            {
                g.DrawString(TitleText, titleFont, titleBrush, 94, 43);
                RectangleF statusBounds = new RectangleF(95, 67, ClientSize.Width - 108, 28);
                g.DrawString(StatusText, subtitleFont, subtitleBrush, statusBounds);
            }

            using (SolidBrush dotBrush = new SolidBrush(Color.FromArgb(139, 55, 226)))
            {
                g.FillEllipse(dotBrush, 96, 94, 5, 5);
                g.FillEllipse(dotBrush, 106, 94, 5, 5);
                g.FillEllipse(dotBrush, 116, 94, 5, 5);
            }
        }

        private void UpdateRoundedRegion()
        {
            if (ClientSize.Width < 10 || ClientSize.Height < 10)
                return;

            using (GraphicsPath path = CreateRoundedPath(
                new Rectangle(0, 0, ClientSize.Width, ClientSize.Height), 20))
            {
                Region oldRegion = Region;
                Region = new Region(path);
                if (oldRegion != null)
                    oldRegion.Dispose();
            }
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
