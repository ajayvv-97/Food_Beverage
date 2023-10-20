using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

public class AYTextbox : TextBox
{
    [System.Runtime.InteropServices.DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
    private static extern IntPtr CreateRoundRectRgn(
        int nLeftRect,  // X-coordinate of upper-left corner or padding at start
        int nTopRect,   // Y-coordinate of upper-left corner or padding at the top of the textbox
        int nRightRect, // X-coordinate of lower-right corner or Width of the object
        int nBottomRect, // Y-coordinate of lower-right corner or Height of the object
        int nheightRect, // Height of ellipse
        int nweightRect // Width of ellipse
    );

    private Color borderColor = Color.Black; // Border color
    private int borderWidth = 2; // Border width

    public Color BorderColor
    {
        get { return borderColor; }
        set { borderColor = value; Invalidate(); }
    }

    public int BorderWidth
    {
        get { return borderWidth; }
        set { borderWidth = value; Invalidate(); }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(2, 3, this.Width, this.Height, 15, 15));
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        using (Pen borderPen = new Pen(BorderColor, BorderWidth))
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int radius = 15; // Adjust the radius as needed
            int width = this.Width;
            int height = this.Height;

            using (var path = new GraphicsPath())
            {
                path.AddArc(new Rectangle(1, 1, 2 * radius, 2 * radius), 180, 90);
                path.AddArc(new Rectangle(width - 2 * radius - 1, 1, 2 * radius, 2 * radius), 270, 90);
                path.AddArc(new Rectangle(width - 2 * radius - 1, height - 2 * radius - 1, 2 * radius, 2 * radius), 0, 90);
                path.AddArc(new Rectangle(1, height - 2 * radius - 1, 2 * radius, 2 * radius), 90, 90);

                e.Graphics.DrawPath(borderPen, path);
            }
        }
    }
}
