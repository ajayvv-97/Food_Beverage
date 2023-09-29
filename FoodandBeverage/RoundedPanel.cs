using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace FoodandBeverage
{
    public class RoundedPanel : Panel
    {
        private int cornerRadius = 30; // Adjust this value for the desired corner radius

        public int CornerRadius
        {
            get { return cornerRadius; }
            set { cornerRadius = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (var path = new GraphicsPath())
            {
                int radius = cornerRadius * 2;
                int width = Width;
                int height = Height;

                path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
                path.AddArc(new Rectangle(width - radius, 0, radius, radius), 270, 90);
                path.AddArc(new Rectangle(width - radius, height - radius, radius, radius), 0, 90);
                path.AddArc(new Rectangle(0, height - radius, radius, radius), 90, 90);
                path.CloseFigure();

                Region = new Region(path);
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.FillPath(new SolidBrush(BackColor), path);
            }
        }
    }
}
