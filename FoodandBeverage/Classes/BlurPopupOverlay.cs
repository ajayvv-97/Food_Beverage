using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FoodandBeverage.Classes
{
    /// <summary>
    /// Reusable blurred/dimmed backdrop for panel popups and modal child forms.
    /// </summary>
    public sealed class BlurPopupOverlay : IDisposable
    {
        private const int PreviewScale = 4;
        private const byte DarkOverlayAlpha = 76;
        private readonly Form _owner;
        private readonly List<Control> _popupControls = new List<Control>();
        private readonly HashSet<Control> _activePopups = new HashSet<Control>();
        private PictureBox _overlay;
        private Bitmap _blurImage;
        private bool _isUpdating;
        private bool _releaseScheduled;
        private bool _ownerBlurRequested;
        private bool _disposed;

        public BlurPopupOverlay(Form owner, params Control[] popupControls)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");
            _owner = owner;

            if (popupControls != null)
            {
                foreach (Control popup in popupControls)
                {
                    if (popup == null || popup.IsDisposed)
                        continue;
                    _popupControls.Add(popup);
                    popup.VisibleChanged -= Popup_VisibleChanged;
                    popup.VisibleChanged += Popup_VisibleChanged;
                }
            }

            _owner.SizeChanged += Owner_SizeChanged;
            _owner.FormClosed += Owner_FormClosed;
        }

        public void ShowOwnerBlur()
        {
            if (_disposed || _owner.IsDisposed || _owner.Disposing)
                return;
            _ownerBlurRequested = true;
            ShowBlur(null);
        }

        public void HideOwnerBlur()
        {
            if (_disposed)
                return;

            _ownerBlurRequested = false;
            RemoveHiddenPopups();

            if (!HasVisiblePopup())
                ScheduleRelease();
        }

        /// <summary>
        /// Removes stale popup entries when panels are hidden through other
        /// dashboard code paths and releases the overlay when none remain visible.
        /// </summary>
        private void RemoveHiddenPopups()
        {
            _activePopups.RemoveWhere(popup =>
                popup == null || popup.IsDisposed || !popup.Visible);
        }

        private bool HasVisiblePopup()
        {
            RemoveHiddenPopups();

            foreach (Control popup in _popupControls)
            {
                if (popup != null && !popup.IsDisposed && popup.Visible)
                    return true;
            }

            return _activePopups.Count > 0;
        }

        private void ReleaseIfNoPopupIsVisible()
        {
            RemoveHiddenPopups();

            if (!_ownerBlurRequested && !HasVisiblePopup())
                ReleaseImage();
        }

        private void Popup_VisibleChanged(object sender, EventArgs e)
        {
            if (_isUpdating || _disposed)
                return;
            Control popup = sender as Control;
            if (popup == null || popup.IsDisposed)
                return;

            if (popup.Visible)
                ShowBlur(popup);
            else
            {
                _activePopups.Remove(popup);
                RemoveHiddenPopups();

                if (!HasVisiblePopup() && !_ownerBlurRequested)
                    ScheduleRelease();
            }
        }

        private void ShowBlur(Control popup)
        {
            if (_disposed || _owner.IsDisposed || _owner.Disposing)
                return;
            if (popup != null && (popup.IsDisposed || !IsHostedByOwner(popup)))
                return;

            try
            {
                bool shouldCapture = _blurImage == null ||
                    (_activePopups.Count == 0 && !_ownerBlurRequested);

                if (shouldCapture)
                {
                    bool wasPopupVisible = popup != null && popup.Visible;
                    bool wasOverlayVisible = _overlay != null &&
                        !_overlay.IsDisposed && _overlay.Visible;

                    _isUpdating = true;
                    try
                    {
                        if (wasPopupVisible)
                            popup.Visible = false;
                        if (wasOverlayVisible)
                            _overlay.Visible = false;

                        Bitmap nextImage;
                        using (Bitmap original = CaptureOwner())
                        using (Bitmap preview = ResizePreview(original,
                            Math.Max(1, original.Width / PreviewScale),
                            Math.Max(1, original.Height / PreviewScale)))
                        {
                            nextImage = CreateDarkBlurPreview(preview);
                        }

                        EnsureOverlay();
                        Bitmap previousImage = _blurImage;
                        _blurImage = nextImage;
                        _overlay.Image = _blurImage;
                        if (previousImage != null)
                            previousImage.Dispose();
                    }
                    finally
                    {
                        if (wasPopupVisible && popup != null && !popup.IsDisposed)
                            popup.Visible = true;
                        _isUpdating = false;
                        if (wasOverlayVisible && _overlay != null && !_overlay.IsDisposed)
                            _overlay.Visible = true;
                    }
                }

                EnsureOverlay();
                _overlay.Image = _blurImage;
                _overlay.Bounds = _owner.ClientRectangle;
                _overlay.Visible = true;

                if (popup != null)
                {
                    _activePopups.Add(popup);
                    PositionOverlayBehindPopup(popup);
                }
                else
                    _overlay.BringToFront();
            }
            catch (Exception ex)
            {
                _isUpdating = false;
                Debug.WriteLine("BlurPopupOverlay.ShowBlur: " + ex);
            }
        }

        private bool IsHostedByOwner(Control popup)
        {
            Control current = popup;
            while (current != null && current.Parent != null)
            {
                if (current.Parent == _owner)
                    return true;
                current = current.Parent;
            }
            return false;
        }

        private void PositionOverlayBehindPopup(Control popup)
        {
            if (popup == null || popup.IsDisposed ||
                _overlay == null || _overlay.IsDisposed ||
                !_owner.Controls.Contains(_overlay))
                return;

            Control topLevelPopup = popup;
            while (topLevelPopup.Parent != null && topLevelPopup.Parent != _owner)
                topLevelPopup = topLevelPopup.Parent;

            if (topLevelPopup.Parent != _owner)
            {
                _overlay.BringToFront();
                return;
            }

            topLevelPopup.BringToFront();
            int popupIndex = _owner.Controls.GetChildIndex(topLevelPopup);
            int overlayIndex = Math.Min(popupIndex + 1, _owner.Controls.Count - 1);
            _owner.Controls.SetChildIndex(_overlay, overlayIndex);
        }

        private void EnsureOverlay()
        {
            if (_overlay != null && !_overlay.IsDisposed)
                return;
            _overlay = new PictureBox
            {
                Name = "picBlurPopupOverlay",
                Bounds = _owner.ClientRectangle,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Black,
                TabStop = false,
                Enabled = false,
                Visible = false
            };
            _owner.Controls.Add(_overlay);
        }

        private Bitmap CaptureOwner()
        {
            int width = Math.Max(1, _owner.ClientSize.Width);
            int height = Math.Max(1, _owner.ClientSize.Height);
            Bitmap captured = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            try
            {
                _owner.DrawToBitmap(captured, new Rectangle(0, 0, width, height));
                return captured;
            }
            catch
            {
                captured.Dispose();
                throw;
            }
        }

        private Bitmap ResizePreview(Bitmap image, int width, int height)
        {
            Bitmap resized = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(resized))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return resized;
        }

        private Bitmap CreateDarkBlurPreview(Bitmap image)
        {
            Bitmap blurred = BlurImageFast(image);
            try
            {
                using (Graphics graphics = Graphics.FromImage(blurred))
                using (SolidBrush shade = new SolidBrush(Color.FromArgb(DarkOverlayAlpha, 0, 0, 0)))
                {
                    graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                    graphics.FillRectangle(shade, 0, 0, blurred.Width, blurred.Height);
                }
                return blurred;
            }
            catch
            {
                blurred.Dispose();
                throw;
            }
        }

        private Bitmap BlurImageFast(Bitmap image)
        {
            const int bytesPerPixel = 4;
            int width = image.Width;
            int height = image.Height;
            int rowBytes = width * bytesPerPixel;
            Bitmap blurred = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            BitmapData sourceData = null;
            BitmapData destinationData = null;
            bool completed = false;

            try
            {
                Rectangle bounds = new Rectangle(0, 0, width, height);
                sourceData = image.LockBits(bounds, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                destinationData = blurred.LockBits(bounds, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                byte[] sourcePixels = new byte[rowBytes * height];
                byte[] resultPixels = new byte[sourcePixels.Length];

                for (int y = 0; y < height; y++)
                {
                    IntPtr sourceRow = IntPtr.Add(sourceData.Scan0, y * sourceData.Stride);
                    Marshal.Copy(sourceRow, sourcePixels, y * rowBytes, rowBytes);
                }

                Buffer.BlockCopy(sourcePixels, 0, resultPixels, 0, sourcePixels.Length);
                for (int y = 1; y < height - 1; y++)
                {
                    for (int x = 1; x < width - 1; x++)
                    {
                        int blue = 0, green = 0, red = 0;
                        for (int fy = -1; fy <= 1; fy++)
                        {
                            int rowOffset = (y + fy) * rowBytes;
                            for (int fx = -1; fx <= 1; fx++)
                            {
                                int offset = rowOffset + ((x + fx) * bytesPerPixel);
                                blue += sourcePixels[offset];
                                green += sourcePixels[offset + 1];
                                red += sourcePixels[offset + 2];
                            }
                        }

                        int targetOffset = (y * rowBytes) + (x * bytesPerPixel);
                        resultPixels[targetOffset] = (byte)(blue / 9);
                        resultPixels[targetOffset + 1] = (byte)(green / 9);
                        resultPixels[targetOffset + 2] = (byte)(red / 9);
                        resultPixels[targetOffset + 3] = sourcePixels[targetOffset + 3];
                    }
                }

                for (int y = 0; y < height; y++)
                {
                    IntPtr destinationRow = IntPtr.Add(destinationData.Scan0, y * destinationData.Stride);
                    Marshal.Copy(resultPixels, y * rowBytes, destinationRow, rowBytes);
                }

                completed = true;
                return blurred;
            }
            finally
            {
                try
                {
                    if (destinationData != null)
                        blurred.UnlockBits(destinationData);
                }
                finally
                {
                    try
                    {
                        if (sourceData != null)
                            image.UnlockBits(sourceData);
                    }
                    finally
                    {
                        if (!completed)
                            blurred.Dispose();
                    }
                }
            }
        }

        private void ScheduleRelease()
        {
            if (_releaseScheduled)
                return;
            _releaseScheduled = true;

            if (!_owner.IsHandleCreated || _owner.IsDisposed || _owner.Disposing)
            {
                _releaseScheduled = false;
                ReleaseImage();
                return;
            }

            try
            {
                _owner.BeginInvoke((MethodInvoker)delegate
                {
                    _releaseScheduled = false;
                    if (_disposed || _owner.IsDisposed || _owner.Disposing)
                        return;
                    ReleaseIfNoPopupIsVisible();
                });
            }
            catch (InvalidOperationException)
            {
                _releaseScheduled = false;
                ReleaseImage();
            }
        }

        private void ReleaseImage()
        {
            if (_overlay != null && !_overlay.IsDisposed)
            {
                _overlay.Visible = false;
                _overlay.Image = null;
            }
            if (_blurImage != null)
            {
                _blurImage.Dispose();
                _blurImage = null;
            }
        }

        private void Owner_SizeChanged(object sender, EventArgs e)
        {
            if (_overlay != null && !_overlay.IsDisposed)
                _overlay.Bounds = _owner.ClientRectangle;
        }

        private void Owner_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_disposed)
                return;
            _disposed = true;

            foreach (Control popup in _popupControls)
            {
                if (popup != null && !popup.IsDisposed)
                    popup.VisibleChanged -= Popup_VisibleChanged;
            }
            _popupControls.Clear();
            _activePopups.Clear();
            _owner.SizeChanged -= Owner_SizeChanged;
            _owner.FormClosed -= Owner_FormClosed;
            ReleaseImage();

            if (_overlay != null)
            {
                if (!_owner.IsDisposed && !_owner.Disposing && _owner.Controls.Contains(_overlay))
                    _owner.Controls.Remove(_overlay);
                _overlay.Dispose();
                _overlay = null;
            }
        }
    }
}
