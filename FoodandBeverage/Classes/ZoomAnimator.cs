using System;
using System.Windows.Forms;

namespace FoodandBeverage.Classes
{
    public class ZoomAnimator : IDisposable
    {
        private readonly PictureBox _pic;
        private readonly Timer _timer;
        private readonly int _zoomSize = 12;  // zoom size
        private int _targetW, _targetH;
        private readonly int _originalW, _originalH;

        private bool _disposed = false;

        public ZoomAnimator(PictureBox pic)
        {
            _pic = pic;

            // Store original size
            _originalW = pic.Width;
            _originalH = pic.Height;

            // Setup animation timer
            _timer = new Timer();
            _timer.Interval = 15;
            _timer.Tick += Animate;

            // Auto-dispose when PictureBox is disposed
            pic.Disposed += (s, e) => Dispose();
        }

        public void StartZoom(bool zoomIn)
        {
            if (_disposed) return;

            _timer.Stop();

            if (zoomIn)
            {
                _targetW = _originalW + _zoomSize;
                _targetH = _originalH + _zoomSize;
            }
            else
            {
                _targetW = _originalW;
                _targetH = _originalH;
            }

            _timer.Start();
        }

        private void Animate(object sender, EventArgs e)
        {
            if (_disposed) return;

            int speed = 2;

            // Zooming width
            if (_pic.Width < _targetW) _pic.Width += speed;
            if (_pic.Width > _targetW) _pic.Width -= speed;

            // Zooming height
            if (_pic.Height < _targetH) _pic.Height += speed;
            if (_pic.Height > _targetH) _pic.Height -= speed;

            // Keep image centered
            if (_pic.Parent != null)
            {
                _pic.Left = (_pic.Parent.Width - _pic.Width) / 2;
            }

            // Stop animation
            if (Math.Abs(_pic.Width - _targetW) < 2 &&
                Math.Abs(_pic.Height - _targetH) < 2)
            {
                _pic.Width = _targetW;
                _pic.Height = _targetH;
                _timer.Stop();
            }
        }

        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            if (_timer != null)
            {
                _timer.Stop();
                _timer.Tick -= Animate;
                _timer.Dispose();
            }
        }
    }
}
