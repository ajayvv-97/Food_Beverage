using BLL;
using CustomMessageBox;
using Domain;
using FoodandBeverage.Classes;
using FoodandBeverage.Controls;
using FoodandBeverage.Forms.UserControl;
using FoodandBeverage.Forms.UserControls;
using FoodandBeverage.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmCombinedBill : Form
    {
        public FrmCombinedBill()
        {
            InitializeComponent();
            FormClosed += FrmCombinedBill_BlurOverlayFormClosed;
        }

        #region Declaration
        public List<DmnNameList> CheckedtableNames = new List<DmnNameList>();      

        public int panelsPerRow = 0;
        public int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        ClsGeneral clsgen = new ClsGeneral();

        #region Modal Dialog Blur Overlay

        /// <summary>
        /// Opens a modal dialog with the complete application dashboard blurred behind it.
        /// FrmCombinedBill is hosted inside FrmDashboard.panelDesktop, so the overlay
        /// must be attached to FrmDashboard rather than this embedded form.
        /// </summary>
        private DialogResult ShowDialogWithBlur(Form dialog)
        {
            if (dialog == null || dialog.IsDisposed)
                return DialogResult.Cancel;

            ShowModalBlurOverlay();

            Form dialogOwner = (_modalBlurOwner != null &&
                !_modalBlurOwner.IsDisposed && !_modalBlurOwner.Disposing)
                ? _modalBlurOwner
                : this;

            try
            {
                return dialog.ShowDialog(dialogOwner);
            }
            finally
            {
                HideModalBlurOverlay();
            }
        }

        private Form ResolveModalBlurOwner()
        {
            FrmDashboard dashboard = FrmDashboard.Instance;

            if (dashboard != null && !dashboard.IsDisposed && !dashboard.Disposing)
                return dashboard;

            return this;
        }

        private void ShowModalBlurOverlay()
        {
            if (_isModalBlurVisible)
                return;

            Bitmap nextBlurImage = null;

            try
            {
                Form owner = ResolveModalBlurOwner();

                if (_modalBlurOwner != owner)
                {
                    if (_modalBlurOwner != null)
                    {
                        _modalBlurOwner.Resize -= FrmCombinedBill_BlurOverlayResize;

                        if (_modalBlurOwner != this)
                            _modalBlurOwner.FormClosed -= FrmCombinedBill_BlurOverlayFormClosed;
                    }

                    _modalBlurOwner = owner;
                }

                _modalBlurOwner.Resize -= FrmCombinedBill_BlurOverlayResize;
                _modalBlurOwner.Resize += FrmCombinedBill_BlurOverlayResize;

                if (_modalBlurOwner != this)
                {
                    _modalBlurOwner.FormClosed -= FrmCombinedBill_BlurOverlayFormClosed;
                    _modalBlurOwner.FormClosed += FrmCombinedBill_BlurOverlayFormClosed;
                }

                using (Bitmap original = CaptureCombinedBillForm(_modalBlurOwner))
                using (Bitmap small = ResizeModalBlurPreview(
                    original,
                    Math.Max(1, original.Width / ModalBlurPreviewScale),
                    Math.Max(1, original.Height / ModalBlurPreviewScale)))
                {
                    nextBlurImage = CreateModalDarkBlurPreview(small);
                }

                EnsureModalBlurPictureBox();

                Bitmap previousImage = _modalBlurImage;
                _modalBlurImage = nextBlurImage;
                nextBlurImage = null;

                _modalBlurOverlay.Image = _modalBlurImage;
                _modalBlurOverlay.Bounds = _modalBlurOwner.ClientRectangle;
                _modalBlurOverlay.Visible = true;
                _modalBlurOverlay.BringToFront();
                _isModalBlurVisible = true;

                if (previousImage != null)
                    previousImage.Dispose();
            }
            catch (Exception ex)
            {
                if (nextBlurImage != null)
                    nextBlurImage.Dispose();

                _isModalBlurVisible = false;

                clsgen.LogException(
                    "CombinedBill ShowModalBlurOverlay()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.CombinedBillpage));
            }
        }

        private void HideModalBlurOverlay()
        {
            _isModalBlurVisible = false;

            if (_modalBlurOverlay != null && !_modalBlurOverlay.IsDisposed)
            {
                _modalBlurOverlay.Visible = false;
                _modalBlurOverlay.Image = null;
            }

            if (_modalBlurImage != null)
            {
                _modalBlurImage.Dispose();
                _modalBlurImage = null;
            }
        }

        private void EnsureModalBlurPictureBox()
        {
            if (_modalBlurOwner == null || _modalBlurOwner.IsDisposed)
                return;

            if (_modalBlurOverlay != null &&
                !_modalBlurOverlay.IsDisposed &&
                _modalBlurOverlay.Parent == _modalBlurOwner)
                return;

            if (_modalBlurOverlay != null && !_modalBlurOverlay.IsDisposed)
            {
                if (_modalBlurOverlay.Parent != null)
                    _modalBlurOverlay.Parent.Controls.Remove(_modalBlurOverlay);

                _modalBlurOverlay.Dispose();
            }

            _modalBlurOverlay = new PictureBox
            {
                Name = "picCombinedBillModalBlur",
                Bounds = _modalBlurOwner.ClientRectangle,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Black,
                TabStop = false,
                Enabled = false,
                Visible = false
            };

            _modalBlurOwner.Controls.Add(_modalBlurOverlay);
        }

        private Bitmap CaptureCombinedBillForm(Form owner)
        {
            int width = Math.Max(1, owner.ClientSize.Width);
            int height = Math.Max(1, owner.ClientSize.Height);
            Bitmap captured = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            try
            {
                // Capture the actual dashboard client area so the side menu,
                // application header/footer and embedded page are all included.
                Rectangle screenBounds = owner.RectangleToScreen(owner.ClientRectangle);

                try
                {
                    using (Graphics graphics = Graphics.FromImage(captured))
                    {
                        graphics.CopyFromScreen(
                            screenBounds.Location,
                            Point.Empty,
                            screenBounds.Size,
                            CopyPixelOperation.SourceCopy);
                    }
                }
                catch
                {
                    // DrawToBitmap is retained as a fallback for environments
                    // where screen capture is unavailable.
                    owner.DrawToBitmap(captured, new Rectangle(0, 0, width, height));
                }

                return captured;
            }
            catch
            {
                captured.Dispose();
                throw;
            }
        }

        private Bitmap ResizeModalBlurPreview(Bitmap image, int width, int height)
        {
            Bitmap resized = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(resized))
            {
                graphics.InterpolationMode = InterpolationMode.Low;
                graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.DrawImage(image, 0, 0, width, height);
            }

            return resized;
        }

        private Bitmap CreateModalDarkBlurPreview(Bitmap image)
        {
            Bitmap blurred = BlurModalImageFast(image);

            try
            {
                using (Graphics graphics = Graphics.FromImage(blurred))
                using (SolidBrush shade = new SolidBrush(
                    Color.FromArgb(ModalBlurDarkOverlayAlpha, 0, 0, 0)))
                {
                    graphics.CompositingMode = CompositingMode.SourceOver;
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

        private Bitmap BlurModalImageFast(Bitmap image)
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
                        int blue = 0;
                        int green = 0;
                        int red = 0;

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

        private void FrmCombinedBill_BlurOverlayResize(object sender, EventArgs e)
        {
            if (_modalBlurOwner != null &&
                !_modalBlurOwner.IsDisposed &&
                _modalBlurOverlay != null &&
                !_modalBlurOverlay.IsDisposed)
            {
                _modalBlurOverlay.Bounds = _modalBlurOwner.ClientRectangle;
            }
        }

        private void FrmCombinedBill_BlurOverlayFormClosed(object sender, FormClosedEventArgs e)
        {
            HideModalBlurOverlay();

            Form owner = _modalBlurOwner;
            if (owner != null)
            {
                owner.Resize -= FrmCombinedBill_BlurOverlayResize;

                if (owner != this)
                    owner.FormClosed -= FrmCombinedBill_BlurOverlayFormClosed;

                if (!owner.IsDisposed &&
                    _modalBlurOverlay != null &&
                    !_modalBlurOverlay.IsDisposed &&
                    owner.Controls.Contains(_modalBlurOverlay))
                {
                    owner.Controls.Remove(_modalBlurOverlay);
                }
            }

            if (_modalBlurOverlay != null && !_modalBlurOverlay.IsDisposed)
            {
                _modalBlurOverlay.Dispose();
                _modalBlurOverlay = null;
            }

            _modalBlurOwner = null;
        }

        #endregion

        private void dgvorderview_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvorderview.Rows.Count)
                    return;

                if (e.ColumnIndex == actionno)
                {
                    int sid = Convert.ToInt32(dgvorderview.Rows[e.RowIndex].Cells[Salemasteridno].Value);
                    int pbkid = Convert.ToInt32(dgvorderview.Rows[e.RowIndex].Cells[partybookingidno].Value);
                    int tidno = Convert.ToInt32(dgvorderview.Rows[e.RowIndex].Cells[tableidno].Value);
                    int pickupid = Convert.ToInt32(dgvorderview.Rows[e.RowIndex].Cells[pickupidno].Value);

                    PrintBillBySaleType(sid, pbkid, tidno, pickupid);
                }
                else if (e.ColumnIndex == paytypename)
                {
                    int sid = Convert.ToInt32(dgvorderview.Rows[e.RowIndex].Cells[Salemasteridno].Value);
                    DmnSearch obj = new DmnSearch();
                    BllSearch<DmnGridFields> bllpayments = new BllSearch<DmnGridFields>();
                    obj.Select = "PaidAmt Field1,g.Caption Field2,SalesMasterId Field3,Paymode Field4 ";
                    obj.Table = "tblPartPaymentDetails p "+
                        " left join tblGeneralOptions g on g.GTypeSubId=p.Paymode and g.GTypeId=2";
                    obj.WhereNdGroupBy = " where p.status=1 and p.SalesMasterId=" + sid;
                    var paymentresult = bllpayments.GetSearchData("prdSearch", obj);
                    if (paymentresult.Count > 0)
                    {
                        lblsplit_heading.Text = "Split Payment";
                        dgv_split.Rows.Clear();
                        dgv_split.Rows.Add(paymentresult.Count);
                        int x = dgv_split.RowCount;
                        for (int i = 0; i < paymentresult.Count; i++)
                        {
                            dgv_split.Rows[i].Cells[sslno].Value = i + 1;
                            dgv_split.Rows[i].Cells[spaymentamtno].Value = paymentresult[i].Field1;
                            dgv_split.Rows[i].Cells[spaymentmodeno].Value = paymentresult[i].Field2;
                            dgv_split.Rows[i].Cells[ssalemasteridno].Value = paymentresult[i].Field3;
                            dgv_split.Rows[i].Cells[spayidno].Value = paymentresult[i].Field4;
                        }
                    }
                    pnl_split.Location = new Point(this.ClientSize.Width / 2 - pnl_split.Size.Width / 2, this.ClientSize.Height / 2 - pnl_split.Size.Height / 2);
                    pnl_split.Visible = true;
                    pnl_split.BringToFront();

                }

            }
            catch (Exception ex)
            {
                clsgen.LogException("CombinedBill Page dgvorderview_CellDoubleClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        public void printBill(int oid,int btype)
        {
            try
            {
                DmnSearch obj = new DmnSearch();
                BllSearch<DmnGridFields> bllpayments = new BllSearch<DmnGridFields>();
                obj.Select = "PaymentMode Field1,SalesMasterId Field2";
                obj.Table = "tblSalesMaster";
                obj.WhereNdGroupBy = " where status=1 and SalesMasterId=" + oid;
                var result = bllpayments.GetSearchData("prdSearch", obj);
                if (result.Count > 0)
                {
                    //if (Convert.ToInt32(result[0].Field1) != 12)
                    //{
                        pnl_split.Visible = false;
                        using (FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(oid, Convert.ToInt32(result[0].Field1), 0, btype))
                        {
                            frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                            ShowDialogWithBlur(frmSalesInvoice);
                        }
                    //}
                    //else
                    //{
                    //    obj.Select = "Amount Field1,SplitType Field2,SalemasterId Field3,PayId Field4 ";
                    //    obj.Table = "tblpayment";
                    //    obj.WhereNdGroupBy = " where status=1 and SaleMasterId=" + oid;
                    //    var paymentresult = bllpayments.GetSearchData("prdSearch", obj);
                    //    if (paymentresult.Count > 0)
                    //    {
                    //        dgv_split.Rows.Clear();
                    //        dgv_split.Rows.Add(paymentresult.Count);
                    //        int x = dgv_split.RowCount;
                    //        for (int i = 0; i < paymentresult.Count; i++)
                    //        {
                    //            dgv_split.Rows[i].Cells[0].Value = i + 1;
                    //            dgv_split.Rows[i].Cells[1].Value = paymentresult[i].Field1;
                    //            DataGridViewImageCell cell = (DataGridViewImageCell)dgv_split.Rows[i].Cells[2];
                    //            cell.ImageLayout = DataGridViewImageCellLayout.Stretch;
                    //            cell.Value = Properties.Resources.eye;
                    //            dgv_split.Rows[i].Cells[3].Value = paymentresult[i].Field3;
                    //            dgv_split.Rows[i].Cells[4].Value = paymentresult[i].Field4;
                    //        }
                    //    }
                    //    pnl_split.Location = new Point(this.ClientSize.Width / 2 - pnl_split.Size.Width / 2, this.ClientSize.Height / 2 - pnl_split.Size.Height / 2);
                    //    pnl_split.Visible = true;
                    //    pnl_split.BringToFront();
                    //}
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page printBill()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        public void FillTablecode_Comb(int rowindex, int salemasterid, int ordermasterid, int ordertype)
        {
            try
            {
                BllSearch<DmnComboBox> bllSearch = new BllSearch<DmnComboBox>();
                DmnSearch search = new DmnSearch();

                if (ordertype == 0)
                {
                    search.Select = "bd.MTID id, bd.Name value";
                    search.Table = "tblOrderMaster om " +
                                   "LEFT JOIN tblbookMaster bm ON om.BookmasterId = bm.BookmasterId " +
                                   "LEFT JOIN tblBookDetails bd ON bd.BookmasterId = bm.BookmasterId AND bd.Rid = 0";
                    search.WhereNdGroupBy = "WHERE om.OrderMasterId = " + ordermasterid;
                }
                else
                {
                    search.Select =
                        "com.tid id, " +
                        "CASE WHEN ISNULL(com.PickupOrderFlag,0) = 1 " +
                        "THEN 'PKID ' + CONVERT(VARCHAR(20),com.PkId) " +
                        "ELSE ISNULL(ti.TableCode,'') END value";
                    search.Table = "tblCombinedOrderBill com " +
                                   "LEFT JOIN tblTable_info ti ON ti.tid = com.Tid";
                    search.WhereNdGroupBy = "WHERE com.SalesMasterId = " + salemasterid;
                }

                search.OrderBy = "";
                List<DmnComboBox> source = bllSearch.GetSearchData("prdComboFill", search)
                    ?? new List<DmnComboBox>();

                DataGridViewComboBoxCell cell =
                    (DataGridViewComboBoxCell)dgvorderview.Rows[rowindex].Cells[tablecodeno];

                List<ComboBoxItem> items = new List<ComboBoxItem>();

                if (source.Count == 0)
                {
                    items.Add(new ComboBoxItem(0, "N/A"));
                    cell.ReadOnly = true;
                }
                else if (source.Count == 1)
                {
                    items.Add(new ComboBoxItem(source[0].Id, source[0].Value));
                    cell.ReadOnly = true;
                }
                else
                {
                    items.Add(new ComboBoxItem(0, "Multiple Tables (" + source.Count + ")"));
                    foreach (DmnComboBox item in source)
                        items.Add(new ComboBoxItem(item.Id, item.Value));

                    cell.ReadOnly = false;
                }

                cell.DataSource = items;
                cell.ValueMember = "Id";
                cell.DisplayMember = "Value";
                cell.Value = items[0].Id;
                dgvorderview.Rows[rowindex].Cells[tablecodeno].ReadOnly = cell.ReadOnly;
            }
            catch (Exception ex)
            {
                clsgen.LogException(
                    "CombinedBill Page FillTablecode_Comb()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        public class ComboBoxItem
        {
            public long Id { get; set; }
            public string Value { get; set; }

            public ComboBoxItem(long id, string value)
            {
                Id = id;
                Value = value;
            }

            public override string ToString()
            {
                return Value;
            }
        }
        private void btn_close_split_Click(object sender, EventArgs e)
        {
            pnl_split.Visible = false;
        }
        private void FrmCombinedBill_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FrmDashboard.AutoLogoutWhenIdle)
            {
                AppSession.UnregisterForm(this);
            }
            if (FrmDashboard.Instance != null && !FrmDashboard.Instance.IsDisposed)
            {
                FrmDashboard.Instance.lbl_drawer.Visible = false;
            }
        }
        private void yout_Click(object sender, EventArgs e)
        {
            try
            {
                int permissionlevel = clsgen.CheckButtonPermission(Convert.ToInt32(FrmLogin.UserAccountTypeId), Convert.ToInt32(RolePermission.YOut));
                if (permissionlevel == Convert.ToInt32(PermissionLevel.NoPermission))
                {

                    frmExtraAccessChk frm = new frmExtraAccessChk(Convert.ToInt32(RolePermission.YOut));
                    ShowDialogWithBlur(frm);
                    if (frmExtraAccessChk.access == true)
                    {
                        DrawerCheck();
                        using (FoodandBeverage.SaleBill.FrmZout frmzout = new FoodandBeverage.SaleBill.FrmZout(Convert.ToInt32(BillCategory.Yout), Drawerassginstaffid))
                        {
                            if (FrmDashboard.XYDalvalue)
                                ShowDialogWithBlur(frmzout);
                            else
                                frmzout.PrintAll();
                        }
                        CMessageBox.Show("Yout Print Completed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (permissionlevel == Convert.ToInt32(PermissionLevel.FullPermission) || permissionlevel == Convert.ToInt32(PermissionLevel.ReadOnly))
                {
                    DrawerCheck();
                    using (FoodandBeverage.SaleBill.FrmZout frmzout = new FoodandBeverage.SaleBill.FrmZout(Convert.ToInt32(BillCategory.Yout), Drawerassginstaffid))
                    {
                        if (FrmDashboard.XYDalvalue)
                            ShowDialogWithBlur(frmzout);
                        else
                            frmzout.PrintAll();
                    }
                    CMessageBox.Show("Yout Print Completed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("POSDashboard page yout_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        //private void zout_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        int permissionlevel = clsgen.CheckButtonPermission(Convert.ToInt32(FrmLogin.UserAccountTypeId), Convert.ToInt32(RolePermission.YOut));
        //        if (permissionlevel == Convert.ToInt32(PermissionLevel.NoPermission))
        //        {
        //            frmExtraAccessChk frm = new frmExtraAccessChk(Convert.ToInt32(RolePermission.YOut));
        //            frm.ShowDialog();
        //            if (frmExtraAccessChk.access == true)
        //            {
        //                #region Zout
        //                DrawerCheck();
        //                if (FrmDrawer.DrawerID > 0)
        //                {
        //                    var confirmResult =CMessageBox.Show(
        //                     "This will print the Z-Out report and sign you out.\n\n" +
        //                     "Make sure all transactions are completed.\n\n" +
        //                     "Do you want to proceed and end your shift?",
        //                     "Confirm Z-Out and Sign Out",
        //                     MessageBoxButtons.YesNo,
        //                     MessageBoxIcon.Question
        //                 );

        //                    if (confirmResult != DialogResult.Yes)
        //                        return;

        //                    using (FoodandBeverage.SaleBill.FrmZout frmzout = new FoodandBeverage.SaleBill.FrmZout(Convert.ToInt32(BillCategory.Zout), Drawerassginstaffid))
        //                    {
        //                        if (FrmDashboard.XYDalvalue)
        //                            frmzout.ShowDialog();
        //                        else
        //                            frmzout.PrintAll();
        //                    }

        //                    CMessageBox.Show("Zout Print Completed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //                    Dmndrawer dmndrawer = new Dmndrawer();
        //                    Blldrawer blldrawer = new Blldrawer();

        //                    if (flagDrawerF)
        //                    {
        //                        dmndrawer.Id = Drawerassginid;
        //                    }
        //                    else
        //                    {
        //                        if (FrmDrawer.DrawerID > 0)
        //                            dmndrawer.Id = FrmDrawer.DrawerID;
        //                    }

        //                    dmndrawer.BranchId = FrmDashboard.BranchId;
        //                    dmndrawer.CreatedBy = FrmDashboard.UserId;
        //                    dmndrawer.Status = 0;
        //                    var InsertStatus = blldrawer.AddEditDatadrawer("prdInsertDrawer", dmndrawer);
        //                    if (InsertStatus.Id > 0)
        //                    {
        //                        FrmDrawer.DrawerID = 0;
        //                        tlp_outbtn.Visible = false;
        //                        tlp_previous_bill.Visible = false;
        //                        tlp_control.Visible = false;
        //                        FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
        //                        FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
        //                    }

        //                    #region Close all form Return Login page
        //                    Application.Exit();
        //                    //foreach (Form openForm in Application.OpenForms.Cast<Form>().ToList())
        //                    //{
        //                    //    if (!(openForm is FoodandBeverage.Forms.FrmLogin) && !(openForm is FoodandBeverage.Forms.FrmSplash))
        //                    //        openForm.Close();
        //                    //}

        //                    //System.Threading.Thread.Sleep(500);
        //                    //if (existingLogin == null)
        //                    //{
        //                    //    FoodandBeverage.Forms.FrmLogin loginForm = new FoodandBeverage.Forms.FrmLogin();
        //                    //    loginForm.Show();
        //                    //}
        //                    //else
        //                    //{
        //                    //    existingLogin.Opacity = 100;
        //                    //    existingLogin.BringToFront();
        //                    //    existingLogin.Focus();
        //                    //}
        //                    #endregion

        //                }
        //                else
        //                {
        //                    CMessageBox.Show("Zout Cannot be Completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                }
        //                #endregion
        //            }
        //        }
        //        else if (permissionlevel == Convert.ToInt32(PermissionLevel.FullPermission) || permissionlevel == Convert.ToInt32(PermissionLevel.ReadOnly))
        //        {
        //            #region Zout
        //            DrawerCheck();
        //            if (FrmDrawer.DrawerID > 0)
        //            {
        //                var confirmResult = CMessageBox.Show(
        //                 "This will print the Z-Out report and sign you out.\n\n" +
        //                 "Make sure all transactions are completed.\n\n" +
        //                 "Do you want to proceed and end your shift?",
        //                 "Confirm Z-Out and Sign Out",
        //                 MessageBoxButtons.YesNo,
        //                 MessageBoxIcon.Question
        //             );

        //                if (confirmResult != DialogResult.Yes)
        //                    return;

        //                using (FoodandBeverage.SaleBill.FrmZout frmzout = new FoodandBeverage.SaleBill.FrmZout(Convert.ToInt32(BillCategory.Zout), Drawerassginstaffid))
        //                {
        //                    if (FrmDashboard.XYDalvalue)
        //                        frmzout.ShowDialog();
        //                    else
        //                        frmzout.PrintAll();
        //                }

        //                CMessageBox.Show("Zout Print Completed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                //return;

        //                Dmndrawer dmndrawer = new Dmndrawer();
        //                Blldrawer blldrawer = new Blldrawer();

        //                if (flagDrawerF)
        //                {
        //                    dmndrawer.Id = Drawerassginid;
        //                }
        //                else
        //                {
        //                    if (FrmDrawer.DrawerID > 0)
        //                        dmndrawer.Id = FrmDrawer.DrawerID;
        //                }

        //                dmndrawer.BranchId = FrmDashboard.BranchId;
        //                dmndrawer.CreatedBy = FrmDashboard.UserId;
        //                dmndrawer.Status = 0;
        //                var InsertStatus = blldrawer.AddEditDatadrawer("prdInsertDrawer", dmndrawer);
        //                if (InsertStatus.Id > 0)
        //                {
        //                    FrmDrawer.DrawerID = 0;
        //                    tlp_outbtn.Visible = false;
        //                    tlp_previous_bill.Visible = false;
        //                    tlp_control.Visible = false;
        //                    FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
        //                    FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
        //                }
        //                #region Close all form Return Login page
        //                Application.Exit();
        //                //foreach (Form openForm in Application.OpenForms.Cast<Form>().ToList())
        //                //{
        //                //    if (!(openForm is FoodandBeverage.Forms.FrmLogin) && !(openForm is FoodandBeverage.Forms.FrmSplash))
        //                //        openForm.Close();
        //                //}

        //                //System.Threading.Thread.Sleep(500);
        //                //if (existingLogin == null)
        //                //{
        //                //    FoodandBeverage.Forms.FrmLogin loginForm = new FoodandBeverage.Forms.FrmLogin();
        //                //    loginForm.Show();
        //                //}
        //                //else
        //                //{
        //                //    existingLogin.Opacity = 100;
        //                //    existingLogin.BringToFront();
        //                //    existingLogin.Focus();
        //                //}
        //                #endregion

        //            }
        //            else
        //            {
        //                CMessageBox.Show("Zout Cannot be Completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            }
        //            #endregion
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        clsgen.LogException("POSDashboard page zout_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
        //    }
        //}
        private void zout_Click(object sender, EventArgs e)
        {
            clsgen.WriteZoutSeparator();
            clsgen.StartZoutLog();
            
            clsgen.TrackZoutLog("POSDashboard", "ZOUT_BUTTON_CLICK", "Zout button clicked");

            try
            {
                clsgen.TrackZoutLog("POSDashboard", "PERMISSION_CHECK_START", "Checking Zout permission. UserAccountTypeId=" + FrmLogin.UserAccountTypeId);

                int permissionlevel = clsgen.CheckButtonPermission(Convert.ToInt32(FrmLogin.UserAccountTypeId), Convert.ToInt32(RolePermission.YOut));

                clsgen.TrackZoutLog("POSDashboard", "PERMISSION_CHECK_RESULT", "Zout permission level=" + permissionlevel);

                if (permissionlevel == Convert.ToInt32(PermissionLevel.NoPermission))
                {
                    clsgen.TrackZoutLog("POSDashboard", "EXTRA_ACCESS_REQUIRED", "User does not have direct Zout permission");

                    frmExtraAccessChk frm = new frmExtraAccessChk(Convert.ToInt32(RolePermission.YOut));

                    clsgen.TrackZoutLog("POSDashboard", "EXTRA_ACCESS_FORM_OPEN", "Opening extra access check form");

                    ShowDialogWithBlur(frm);

                    clsgen.TrackZoutLog("POSDashboard", "EXTRA_ACCESS_FORM_CLOSED", "Extra access form closed. Access=" + frmExtraAccessChk.access);

                    if (frmExtraAccessChk.access == true)
                    {
                        clsgen.TrackZoutLog("POSDashboard", "EXTRA_ACCESS_GRANTED", "Zout access granted");
                        ProcessZout();
                    }
                    else
                    {
                        clsgen.TrackZoutLog("POSDashboard", "EXTRA_ACCESS_DENIED", "Zout access denied");
                    }
                }
                else if (permissionlevel == Convert.ToInt32(PermissionLevel.FullPermission) ||
                         permissionlevel == Convert.ToInt32(PermissionLevel.ReadOnly))
                {
                    clsgen.TrackZoutLog("POSDashboard", "DIRECT_ZOUT_PERMISSION", "User has direct Zout permission");
                    ProcessZout();
                }
                else
                {
                    clsgen.TrackZoutLog("POSDashboard", "ZOUT_PERMISSION_UNKNOWN", "Unexpected permission level=" + permissionlevel);
                }
            }
            catch (Exception ex)
            {
                clsgen.TrackZoutLog("POSDashboard", "ZOUT_CLICK_EXCEPTION", "Exception=" + ex.ToString());

                clsgen.LogException(
                    "POSDashboard page zout_Click()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.CombinedBillpage));
            }
        }

        private void ProcessZout()
        {
            clsgen.TrackZoutLog("POSDashboard", "PROCESS_ZOUT_START", "Zout process started");

            try
            {
                // =========================================================
                // DRAWER CHECK
                // =========================================================

                clsgen.TrackZoutLog("POSDashboard", "DRAWER_CHECK_START", "Checking assigned drawer");

                DrawerCheck();

                clsgen.TrackZoutLog("POSDashboard", "DRAWER_CHECK_RESULT", "DrawerID=" + FrmDrawer.DrawerID);

                if (FrmDrawer.DrawerID <= 0)
                {
                    clsgen.TrackZoutLog("POSDashboard", "DRAWER_NOT_ASSIGNED", "Zout cannot continue because DrawerID <= 0");

                    CMessageBox.Show(
                        "Zout Cannot be Completed",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    clsgen.TrackZoutLog("POSDashboard", "PROCESS_ZOUT_END", "Zout process stopped because drawer is not assigned");

                    return;
                }

                clsgen.TrackZoutLog("POSDashboard", "DRAWER_VALID", "Drawer is assigned. DrawerID=" + FrmDrawer.DrawerID);

                // =========================================================
                // ZOUT CONFIRMATION
                // =========================================================

                clsgen.TrackZoutLog("POSDashboard", "ZOUT_CONFIRMATION_START", "Showing Zout confirmation message");

                var confirmResult = CMessageBox.Show(
                    "This will print the Z-Out report and sign you out.\n\n" +
                    "Make sure all transactions are completed.\n\n" +
                    "Do you want to proceed and end your shift?",
                    "Confirm Z-Out and Sign Out",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                clsgen.TrackZoutLog("POSDashboard", "ZOUT_CONFIRMATION_RESULT", "Confirmation result=" + confirmResult);

                if (confirmResult != DialogResult.Yes)
                {
                    clsgen.TrackZoutLog("POSDashboard", "ZOUT_CANCELLED", "User cancelled Zout confirmation");
                    return;
                }

                clsgen.TrackZoutLog("POSDashboard", "ZOUT_CONFIRMATION_ACCEPTED", "User confirmed Zout");

                // =========================================================
                // CREATE ZOUT FORM
                // =========================================================

                clsgen.TrackZoutLog(
                    "POSDashboard",
                    "ZOUT_FORM_CREATE_START",
                    "Creating FrmZout. BillCategory=" + Convert.ToInt32(BillCategory.Zout) +
                    ", DrawerAssignStaffId=" + Drawerassginstaffid);

                using (FoodandBeverage.SaleBill.FrmZout frmzout =
                       new FoodandBeverage.SaleBill.FrmZout(
                           Convert.ToInt32(BillCategory.Zout),
                           Drawerassginstaffid))
                {
                    clsgen.TrackZoutLog("POSDashboard", "ZOUT_FORM_CREATED", "FrmZout created successfully");

                    clsgen.TrackZoutLog("POSDashboard", "ZOUT_MODE_CHECK", "XYDalvalue=" + FrmDashboard.XYDalvalue);

                    if (FrmDashboard.XYDalvalue)
                    {
                        clsgen.TrackZoutLog("POSDashboard", "ZOUT_SHOW_DIALOG_START", "Opening FrmZout.ShowDialog()");

                        ShowDialogWithBlur(frmzout);

                        clsgen.TrackZoutLog("POSDashboard", "ZOUT_SHOW_DIALOG_RETURN", "FrmZout.ShowDialog() returned");
                    }
                    else
                    {
                        clsgen.TrackZoutLog("POSDashboard", "ZOUT_PRINTALL_START", "Calling FrmZout.PrintAll()");

                        frmzout.PrintAll();

                        clsgen.TrackZoutLog("POSDashboard", "ZOUT_PRINTALL_RETURN", "FrmZout.PrintAll() completed");
                    }
                }

                clsgen.TrackZoutLog("POSDashboard", "ZOUT_FORM_DISPOSED", "FrmZout disposed successfully");

                // =========================================================
                // PRINT COMPLETED MESSAGE
                // =========================================================

                clsgen.TrackZoutLog("POSDashboard", "ZOUT_PRINT_COMPLETED_MESSAGE_START", "Showing Zout Print Completed message");

                CMessageBox.Show(
                    "Zout Print Completed",
                    "Successful",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                clsgen.TrackZoutLog("POSDashboard", "ZOUT_PRINT_COMPLETED_MESSAGE_CLOSED", "Zout Print Completed message closed");

                // =========================================================
                // DRAWER STATUS UPDATE
                // =========================================================

                clsgen.TrackZoutLog("POSDashboard", "DRAWER_STATUS_UPDATE_START", "Starting drawer status update");

                Dmndrawer dmndrawer = new Dmndrawer();
                Blldrawer blldrawer = new Blldrawer();

                if (flagDrawerF)
                {
                    dmndrawer.Id = Drawerassginid;

                    clsgen.TrackZoutLog("POSDashboard", "DRAWER_ID_SELECTED", "Using Drawerassginid=" + Drawerassginid);
                }
                else
                {
                    if (FrmDrawer.DrawerID > 0)
                    {
                        dmndrawer.Id = FrmDrawer.DrawerID;

                        clsgen.TrackZoutLog("POSDashboard", "DRAWER_ID_SELECTED", "Using FrmDrawer.DrawerID=" + FrmDrawer.DrawerID);
                    }
                    else
                    {
                        clsgen.TrackZoutLog("POSDashboard", "DRAWER_ID_MISSING", "FrmDrawer.DrawerID is not greater than zero");
                    }
                }

                dmndrawer.BranchId = FrmDashboard.BranchId;
                dmndrawer.CreatedBy = FrmDashboard.UserId;
                dmndrawer.Status = 0;

                clsgen.TrackZoutLog(
                    "POSDashboard",
                    "DRAWER_STATUS_UPDATE_PARAMETERS",
                    "DrawerId=" + dmndrawer.Id +
                    ", BranchId=" + dmndrawer.BranchId +
                    ", CreatedBy=" + dmndrawer.CreatedBy +
                    ", Status=" + dmndrawer.Status);

                clsgen.TrackZoutLog("POSDashboard", "DRAWER_STATUS_UPDATE_DATABASE_START", "Calling prdInsertDrawer");

                var InsertStatus = blldrawer.AddEditDatadrawer("prdInsertDrawer", dmndrawer);

                if (InsertStatus == null)
                {
                    clsgen.TrackZoutLog("POSDashboard", "DRAWER_STATUS_UPDATE_DATABASE_RESULT", "InsertStatus is NULL");
                }
                else
                {
                    clsgen.TrackZoutLog("POSDashboard", "DRAWER_STATUS_UPDATE_DATABASE_RESULT", "InsertStatus.Id=" + InsertStatus.Id);
                }

                if (InsertStatus != null && InsertStatus.Id > 0)
                {
                    clsgen.TrackZoutLog("POSDashboard", "DRAWER_STATUS_UPDATE_SUCCESS", "Drawer successfully closed/updated");

                    FrmDrawer.DrawerID = 0;

                    clsgen.TrackZoutLog("POSDashboard", "DRAWER_ID_RESET", "FrmDrawer.DrawerID reset to 0");

                    tlp_outbtn.Visible = false;
                    tlp_previous_bill.Visible = false;
                    tlp_control.Visible = false;

                    FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
                    FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;

                    clsgen.TrackZoutLog("POSDashboard", "DASHBOARD_DRAWER_UI_UPDATED", "Dashboard drawer status changed to Not Assigned");
                }
                else
                {
                    clsgen.TrackZoutLog(
                        "POSDashboard",
                        "DRAWER_STATUS_UPDATE_FAILED",
                        "Drawer update failed. InsertStatus.Id=" +
                        (InsertStatus == null ? "NULL" : InsertStatus.Id.ToString()));
                }

                // =========================================================
                // APPLICATION EXIT
                // =========================================================

                clsgen.TrackZoutLog("POSDashboard", "APPLICATION_EXIT_START", "Calling Application.Exit()");

                Application.Exit();

                clsgen.TrackZoutLog("POSDashboard", "APPLICATION_EXIT_RETURNED", "Application.Exit() returned");
            }
            catch (Exception ex)
            {
                clsgen.TrackZoutLog("POSDashboard", "PROCESS_ZOUT_EXCEPTION", "Exception=" + ex.ToString());

                clsgen.LogException(
                    "POSDashboard page ProcessZout()",
                    ex,
                    FrmLogin.EmployeName,
                    FrmLogin.Designation,
                    Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        private void normal_rpt_Click(object sender, EventArgs e)
        {
            try
            {
                DrawerCheck();
                using (FoodandBeverage.SaleBill.FrmZout frmzout = new FoodandBeverage.SaleBill.FrmZout(Convert.ToInt32(BillCategory.DailyCollection), Drawerassginstaffid))
                {
                    if (FrmDashboard.XYDalvalue)
                        frmzout.ShowDialog();
                    else
                        frmzout.PrintAll();
                }
               CMessageBox.Show("Normal Report Print Completed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsgen.LogException("POSDashboard page normal_rpt_Click()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }
        private void detailed_rpt_Click(object sender, EventArgs e)
        {
            DrawerCheck();
            using (FoodandBeverage.SaleBill.FrmZout frmzout = new FoodandBeverage.SaleBill.FrmZout(Convert.ToInt32(BillCategory.DetailedCollection), 0))
            {
                frmzout.PrintDetailsCollection();
            }
           CMessageBox.Show("Details Collection Report Printing Completed", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void txtinitialamt_Click(object sender, EventArgs e)
        {
            if (FrmDashboard.Enablenumberkeypad)
            {
                decimal kdata = 0;
                using (Frmnumdiskeypad frm = new Frmnumdiskeypad(Convert.ToInt32(KeypadType.None)))
                {
                    frm.ShowDialog();
                    kdata = Convert.ToDecimal(frm.txtkeypadvalue.Text);
                    frm.Dispose();
                }
                if (sender is TextBox txt)
                {
                    txt.Text = kdata.ToString("0.##");
                }

            }
        }
        private void txtinitialamt_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (char.IsControl(e.KeyChar))
            {
                return;
            }
            if (char.IsDigit(e.KeyChar))
            {
                return;
            }
            if (e.KeyChar == '.' && !txt.Text.Contains("."))
            {
                return;
            }
            e.Handled = true;
        }
        private void dgv_split_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (e.ColumnIndex == 2)
                //{
                //    FoodandBeverage.SaleBill.FrmSaleInvoice frmSalesInvoice = new FoodandBeverage.SaleBill.FrmSaleInvoice(Convert.ToInt32(dgv_split.Rows[e.RowIndex].Cells[3].Value), Convert.ToInt32(PaymentType.SPLIT), Convert.ToInt32(dgv_split.Rows[e.RowIndex].Cells[4].Value), Convert.ToInt32(BillType.NormalBill));
                //    frmSalesInvoice.StartPosition = FormStartPosition.CenterScreen;
                //    frmSalesInvoice.ShowDialog();
                //}
            }
            catch (Exception ex)
            {
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK);
                clsgen.LogException("CombinedBill Page dgv_split_CellContentClick()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }
        }

        private void dgvorderview_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        #region Keyboard
        public void ShowKeyboard(TextBox activeTextBox)
        {
            if (activeTextBox == null) return;

            Point textBoxScreenLocation = activeTextBox.PointToScreen(Point.Empty);
            int desiredX = 0, desiredY = 0;
            if (Frmkeyboard.IsOpen)
            {
                #region Calculate Position of Keyboard 
                desiredX = textBoxScreenLocation.X + (activeTextBox.Width / 2) - (Frmkeyboard.Instance.Width / 2);
                desiredY = textBoxScreenLocation.Y + activeTextBox.Height + 5;
                Rectangle screenBounds = Screen.FromControl(activeTextBox).WorkingArea;
                if (desiredX < screenBounds.Left)
                    desiredX = screenBounds.Left;
                else if (desiredX + Frmkeyboard.Instance.Width > screenBounds.Right)
                    desiredX = screenBounds.Right - Frmkeyboard.Instance.Width;
                if (desiredY + Frmkeyboard.Instance.Height > screenBounds.Bottom)
                {
                    desiredY = textBoxScreenLocation.Y - Frmkeyboard.Instance.Height - 5;
                    if (desiredY < screenBounds.Top)
                        desiredY = screenBounds.Top;
                }
                #endregion
            }

            if (Frmkeyboard.IsOpen)
            {
                Frmkeyboard.Instance.StartPosition = FormStartPosition.Manual;
                Frmkeyboard.Instance.Location = new Point(desiredX, desiredY);
                Frmkeyboard.Instance.BringToFront();
            }
            else
            {
                Frmkeyboard frmKey = new Frmkeyboard(0);

                #region Calculate Position of Keyboard 
                desiredX = textBoxScreenLocation.X + (activeTextBox.Width / 2) - (Frmkeyboard.Instance.Width / 2);
                desiredY = textBoxScreenLocation.Y + activeTextBox.Height + 5;
                Rectangle screenBounds = Screen.FromControl(activeTextBox).WorkingArea;
                if (desiredX < screenBounds.Left)
                    desiredX = screenBounds.Left;
                else if (desiredX + Frmkeyboard.Instance.Width > screenBounds.Right)
                    desiredX = screenBounds.Right - Frmkeyboard.Instance.Width;
                if (desiredY + Frmkeyboard.Instance.Height > screenBounds.Bottom)
                {
                    desiredY = textBoxScreenLocation.Y - Frmkeyboard.Instance.Height - 5;
                    if (desiredY < screenBounds.Top)
                        desiredY = screenBounds.Top;
                }
                Frmkeyboard.Instance.StartPosition = FormStartPosition.Manual;
                Frmkeyboard.Instance.Location = new Point(desiredX, desiredY);
                #endregion


                frmKey.OnKeyPressed += (keyText) =>
                {
                    if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.backspace))
                    {
                        if (activeTextBox.Text.Length > 0)
                            activeTextBox.Text = activeTextBox.Text.Substring(0, activeTextBox.Text.Length - 1);
                    }
                    else if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.space))
                    {
                        activeTextBox.Text += " ";
                    }
                    else if (FoodandBeverage.Forms.UserControl.Frmkeyboard.KeyTypevalue == Convert.ToInt32(KeyType.clear))
                    {
                        activeTextBox.Text = "";
                    }
                    else
                    {
                        activeTextBox.Text += keyText;
                    }
                    activeTextBox.SelectionStart = activeTextBox.Text.Length;
                    activeTextBox.SelectionLength = 0;
                };

                frmKey.Show();
            }

        }
        #endregion        

        #region drawerAssign
        public void AssignDrawerCheck()
        {
            try
            {
                BllSearch<DmnGridFields> bllsearchdrawer = new BllSearch<DmnGridFields>();
                DmnSearch dmnsearckdrawer = new DmnSearch();
                dmnsearckdrawer.Select = "COUNT(*) OVER() AS Field1,t.CreatedBy as Field2,e.EmployeeName as Field3,d.DesignationName as Field4,t.Id as Field5,t.LoanAmount as Field6,t.Drops as Field7,u.UserAccountId as Field8,e.EmployeeId as Field9";
                dmnsearckdrawer.Table = "tblDrawerAssign t   left join tblUserAccount  u on u.EmployeeId= t.EmployeeId left join tblEmployee  e on e.EmployeeId=t.EmployeeId left join tblDesignation  d on d.DesignationId=e.DesignationId";
                dmnsearckdrawer.WhereNdGroupBy = "where t.DName='" + System.Environment.MachineName + "'  and t.Status=1";
                dmnsearckdrawer.OrderBy = "";
                var gridesource = bllsearchdrawer.GetSearchData("prdSearch", dmnsearckdrawer);
                if (gridesource.Count > 0)
                {

                    //if (FrmDashboard.AllowDrawerpermissionFullControl) //full permission 
                    //{
                    //    if (FrmDashboard.Instance != null && !FrmDashboard.Instance.IsDisposed)
                    //    {
                    //        FrmDashboard.Instance.lbl_drawer.Text = "Drawer Assigned on " + gridesource[0].Field3 + " , " + gridesource[0].Field4;
                    //        FrmDashboard.Instance.lbl_drawer.Visible = true;
                    //        FrmDashboard.Instance.lbl_drawer.ForeColor = Color.White;
                    //    }
                    //}
                    //else// only self assign permission 
                    //{
                        if (Convert.ToInt32(gridesource[0].Field9) == Convert.ToInt32(FrmLogin.EmployeeId)) //assigned employee login  
                        {
                           FrmDrawer.DrawerID = Convert.ToInt32(gridesource[0].Field5);
                            FrmDashboard.Instance.lbl_drawer.Text = "Drawer Assigned on " + gridesource[0].Field3 + " , " + gridesource[0].Field4;
                            FrmDashboard.Instance.lbl_drawer.Visible = true;
                            FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Green;
                        }
                        else // other employee drawer assigned on other employee
                        {
                           CMessageBox.Show("Drawer Already Assigned on " + gridesource[0].Field3 + " , " + gridesource[0].Field4, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            if (FrmDashboard.Instance != null && !FrmDashboard.Instance.IsDisposed)
                            {
                                FrmDashboard.Instance.lbl_drawer.Text = "Drawer Assigned on " + gridesource[0].Field3 + " , " + gridesource[0].Field4;
                                FrmDashboard.Instance.lbl_drawer.Visible = true;
                                FrmDashboard.Instance.lbl_drawer.ForeColor = Color.White;
                            }
                            return;
                        }
                   // }
                }
                else //drawer not assign
                {
                    //if (FrmDashboard.AllowDrawerpermissionFullControl)
                    //{

                    //}
                    if (FrmDashboard.AllowCashDrawer)
                    {
                        FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
                        FrmDashboard.Instance.lbl_drawer.Visible = true;
                        FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
                        if (DrawerCheckBranch(Convert.ToInt32(FrmLogin.EmployeeId)))
                        {
                           CMessageBox.Show("Drawer Already Assigned on " + Drawersystemname + "System", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            return;
                        }
                        DialogResult result =CMessageBox.Show("Please Assign Drawer First", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);

                        if (result == DialogResult.OK)
                        {
                            if (System.Windows.Forms.Application.OpenForms["FrmDrawer"] != null)
                            {
                                FrmDrawer _instanceFrmDrawer = Application.OpenForms["FrmDrawer"] as FrmDrawer;
                                if (_instanceFrmDrawer.WindowState == FormWindowState.Minimized)
                                    _instanceFrmDrawer.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                                else
                                {
                                    _instanceFrmDrawer.Visible = true;
                                    _instanceFrmDrawer.Dock = DockStyle.Fill;
                                    _instanceFrmDrawer.ShowDialog();
                                    _instanceFrmDrawer.BringToFront();
                                }
                            }
                            else
                            {
                                using (FrmDrawer _instanceFrmDrawer = new FrmDrawer())
                                {
                                    _instanceFrmDrawer.BringToFront();
                                    _instanceFrmDrawer.ShowDialog();
                                }
                            }
                        }
                        else
                        {
                            if (FrmDashboard.Instance != null && !FrmDashboard.Instance.IsDisposed)
                            {
                                FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
                                FrmDashboard.Instance.lbl_drawer.Visible = true;
                                FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
                            }
                        }
                    }

                    if (FrmDashboard.AllowDrawerpermissionFullControl == false && FrmDashboard.AllowCashDrawer == false) //Both permission are false
                    {
                       CMessageBox.Show("You do not have permission to assign a drawer.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        FrmDashboard.Instance.lbl_drawer.Text = "Cash Drawer Not Assigned";
                        FrmDashboard.Instance.lbl_drawer.Visible = true;
                        FrmDashboard.Instance.lbl_drawer.ForeColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                clsgen.LogException("CombinedBillpage page  AssignDrawerCheck()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
            }

        }

        public void DrawerCheck()
        {
            AssignDrawerCheck();
            if (FrmDrawer.DrawerID == 0)
            {
                return;
            }
            else
            {
                if (FrmDrawer.DrawerID > 0 && !CombineBillAutoTimer.Enabled)
                {
                    tlp_outbtn.Visible = true;
                    Billcomplete_Fill();
                    cmb_tbl_names.Items.Add("List");

                    pnl_previous_bill.Visible = false;
                    flp_billlayout.Visible = true;
                    flp_billlayout.BringToFront();

                }
                else if(!tlp_control.Visible)
                {
                    tlp_outbtn.Visible = true;
                    tlp_previous_bill.Visible = true;
                    tlp_control.Visible = true;
                }
            }

        }

        private void btn_showmenu_Click(object sender, EventArgs e)
        {
            clsgen.Memoryrelease();

            flp_sidemenu.Visible = !flp_sidemenu.Visible;
            if (!flp_sidemenu.Visible)
                return;

            // 1. Dispose old controls safely
            foreach (Control ctrl in flp_sidemenu.Controls)
            {
                menuTooltip.SetToolTip(ctrl, null);  // unlink tooltip
                ctrl.Dispose();
            }
            flp_sidemenu.Controls.Clear();

            flp_sidemenu.AutoScroll = false;
            flp_sidemenu.Refresh();

            // 2. Setup panel
            flp_sidemenu.FlowDirection = FlowDirection.LeftToRight;
            flp_sidemenu.Width = btn_showmenu.Width;
            flp_sidemenu.Height = (int)(this.Height * 0.5);

            flp_sidemenu.AutoScroll = true;
            flp_sidemenu.HorizontalScroll.Enabled = true;
            flp_sidemenu.HorizontalScroll.Visible = true;
            flp_sidemenu.VerticalScroll.Enabled = false;
            flp_sidemenu.VerticalScroll.Visible = false;

            // 3. Positioning
            Point btnScreen = btn_showmenu.PointToScreen(Point.Empty);
            int desiredX = btnScreen.X + (btn_showmenu.Width / 2) - (flp_sidemenu.Width / 2);
            int desiredY = btnScreen.Y + btn_showmenu.Height + 20;

            Rectangle screenBounds = Screen.FromControl(btn_showmenu).WorkingArea;

            if (desiredX < screenBounds.Left)
                desiredX = screenBounds.Left;
            else if (desiredX + flp_sidemenu.Width > screenBounds.Right)
                desiredX = screenBounds.Right - flp_sidemenu.Width;

            if (desiredY + flp_sidemenu.Height > screenBounds.Bottom)
            {
                desiredY = btnScreen.Y - flp_sidemenu.Height - 10;
                if (desiredY < screenBounds.Top)
                    desiredY = screenBounds.Top;
            }

            Point clientPos = this.PointToClient(new Point(desiredX, desiredY));
            flp_sidemenu.Location = clientPos;

            // 4. Button sizing
            int bheight = (int)(btn_showmenu.Height * 0.9);
            int bWidth = btn_showmenu.Width - 25;

            // 5. Fetch categories
            BllSearch<DmnCategory> bllSearch = new BllSearch<DmnCategory>();
            DmnSearch objSearch = new DmnSearch
            {
                Select = "Fcid,CategoryName",
                Table = "tblCategory_FTI",
                WhereNdGroupBy = "WHERE Status = 1 AND CategoryType = 1",
                OrderBy = ""
            };

            var itemSource = bllSearch.GetSearchData("prdSearch", objSearch);

            // 6. ADD "ALL" BUTTON
            Button btnAll = new Button
            {
                Name = "btnSideMenu_All",
                Tag = 0,
                Text = "All",
                Width = bWidth,
                Height = bheight,
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                FlatStyle = FlatStyle.Flat,
                Font = AppFonts.GetFont(10),
                BackgroundImage = (Image)menuButtonImage.Clone(), // SAFE IMAGE
                BackgroundImageLayout = ImageLayout.Stretch,
            };
            btnAll.FlatAppearance.BorderSize = 0;
            btnAll.Click += SideMenuClick;
            menuTooltip.SetToolTip(btnAll, "All");
            flp_sidemenu.Controls.Add(btnAll);

            // 7. ADD CATEGORY BUTTONS
            if (itemSource != null && itemSource.Count > 0)
            {
                foreach (var item in itemSource)
                {
                    Button btn = new Button
                    {
                        Name = "btnSideMenu_" + item.Fcid,
                        Tag = item.Fcid,
                        Text = item.CategoryName,
                        Width = bWidth,
                        Height = bheight,
                        FlatStyle = FlatStyle.Flat,
                        BackColor = Color.Transparent,
                        ForeColor = Color.White,
                        Font = AppFonts.GetFont(10),
                        BackgroundImage = (Image)menuButtonImage.Clone(),
                        BackgroundImageLayout = ImageLayout.Stretch,
                    };

                    btn.FlatAppearance.BorderSize = 0;
                    btn.Click += SideMenuClick;

                    menuTooltip.SetToolTip(btn, item.CategoryName);

                    flp_sidemenu.Controls.Add(btn);
                }
            }

            // 8. Round Corners (SAFE)
            UIHelper.RoundCorners(flp_sidemenu, 25);
        }

        private void FrmCombinedBill_Resize(object sender, EventArgs e)
        {
            UIHelper.RoundCorners(flp_sidemenu, 25);
        }

        private void btnclose_Click(object sender, EventArgs e)
        {
            FrmDashboard.Instance.Menuhome.PerformClick();
            this.Close(); 
        }

        private void btn_pickup_Click(object sender, EventArgs e)
        {
            long mem = Process.GetCurrentProcess().PrivateMemorySize64 / (1024 * 1024); // MB

            if (mem > 1000)
            {
                this.Close();
                FrmDashboard.Instance.StartSyncAnimation();
            }
            else
            {
                FrmDashboard.Instance.Pickuppage();
            }
           
        }



        public bool DrawerCheckBranch(int empid)
        {
            try
            {
                Drawersystemname = "";
                BllSearch<DmnGridFields> bllsearchdrawer = new BllSearch<DmnGridFields>();
                DmnSearch dmnsearckdrawer = new DmnSearch();
                dmnsearckdrawer.Select = "COUNT(*) OVER() AS Field1,t.CreatedBy as Field2,e.EmployeeName as Field3,d.DesignationName as Field4,t.Id as Field5,t.LoanAmount as Field6,t.Drops as Field7,u.UserAccountId as Field8,e.EmployeeId as Field9,t.DName as Field10";
                dmnsearckdrawer.Table = "tblDrawerAssign t   left join tblUserAccount  u on u.EmployeeId= t.EmployeeId left join tblEmployee  e on e.EmployeeId=t.EmployeeId left join tblDesignation  d on d.DesignationId=e.DesignationId";
                dmnsearckdrawer.WhereNdGroupBy = "where  t.BranchId=" + FrmDashboard.BranchId + " and t.Status=1 and t.EmployeeId =" + empid;
                dmnsearckdrawer.OrderBy = "";
                var gridesource = bllsearchdrawer.GetSearchData("prdSearch", dmnsearckdrawer);
                if (gridesource.Count > 0)
                {
                    Drawersystemname = gridesource[0].Field10;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                clsgen.LogException("Admin page DrawerCheckBranch()", ex, FrmLogin.EmployeName, FrmLogin.Designation, Convert.ToInt32(FormType.CombinedBillpage));
                //MessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
        }
        #endregion

    }
}

