using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmProgressBar : Form
    {
        private Timer animationTimer;
        private Timer progressTimer;
        private int dotCount = 0;
        private int fakeProgress = 0;

        public FrmProgressBar()
        {
            InitializeComponent();
        }

        private void FrmProgressBar_Load(object sender, EventArgs e)
        {
            SetRoundedCorners(40);
            // ProgressBar style
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Value = 0;

            // Dots animation
            animationTimer = new Timer();
            animationTimer.Interval = 500; // 0.5 sec
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();

            // Fake percentage animation
            progressTimer = new Timer();
            progressTimer.Interval = 200; // 0.2 sec
            progressTimer.Tick += ProgressTimer_Tick;
            progressTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            dotCount = (dotCount + 1) % 4;
            lblheading.Text = "Installing Crystal Reports" + new string('.', dotCount);
        }

        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            if (fakeProgress < 90) // go up to 90% while installer runs
            {
                fakeProgress++;
                progressBar1.Value = fakeProgress;
                lblPercentage.Text = fakeProgress + "%";
            }
        }

        public void StartProgress()
        {
            if (InvokeRequired)
            {
                Invoke((Action)StartProgress);
                return;
            }
            progressBar1.Visible = true;
            lblPercentage.Visible = true;
            animationTimer?.Start();
            progressTimer?.Start();
        }

        public void StopProgress()
        {
            if (InvokeRequired)
            {
                Invoke((Action)StopProgress);
                return;
            }

            // Jump to 100% at the end
            progressTimer?.Stop();
            progressBar1.Value = 100;
            lblPercentage.Text = "100%";

            animationTimer?.Stop();
        }

        private void FrmProgressBar_Paint(object sender, PaintEventArgs e)
        {

            try
            {
                int radius = 40;
                int borderWidth = 6; // reduced from 10 to improve performance
                Color borderColor = ColorTranslator.FromHtml("#630000");

                using (GraphicsPath path = GetRoundedPath(new Rectangle(0, 0, this.Width, this.Height), radius))
                using (Pen pen = new Pen(borderColor, borderWidth))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawPath(pen, path);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void SetRoundedCorners(int radius)
        {
            try
            {
                Rectangle bounds = new Rectangle(0, 0, this.Width, this.Height);
                GraphicsPath path = new GraphicsPath();
                int diameter = radius * 2;

                path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
                path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
                path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
                path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
                path.CloseAllFigures();

                this.Region = new Region(path);
            }
            catch (Exception ex)
            {
               
            }
        }
        private GraphicsPath GetRoundedPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseAllFigures();
            return path;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetRoundedCorners(40);
        }
    }

}
