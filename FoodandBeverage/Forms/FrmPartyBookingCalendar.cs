using BLL;
using Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public partial class FrmPartyBookingCalendar : Form
    {
        private enum CalendarView { Month, Week, Day }

        private sealed class PartyBookingEvent
        {
            public int BookingId { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public string EventName { get; set; }
            public string CustomerName { get; set; }
            public string Phone { get; set; }
            public decimal NetAmount { get; set; }
            public decimal PaidAmount { get; set; }

            public string PaymentStatus
            {
                get
                {
                    if (NetAmount > 0 && PaidAmount >= NetAmount)
                        return "PAYMENT COMPLETE";
                    if (PaidAmount > 0)
                        return "ADVANCE ₹" + PaidAmount.ToString("0.00") +
                               "  ·  DUE ₹" + Math.Max(0, NetAmount - PaidAmount).ToString("0.00");
                    return "PAYMENT PENDING  ·  ₹" + Math.Max(0, NetAmount).ToString("0.00");
                }
            }

            public Color PaymentStatusColor
            {
                get
                {
                    if (NetAmount > 0 && PaidAmount >= NetAmount)
                        return Color.FromArgb(22, 142, 86);
                    if (PaidAmount > 0)
                        return Color.FromArgb(189, 112, 20);
                    return Color.FromArgb(197, 55, 72);
                }
            }
        }

        private sealed class BookingChoiceDialog : Form
        {
            public PartyBookingEvent SelectedBooking { get; private set; }

            public BookingChoiceDialog(DateTime date, IList<PartyBookingEvent> items, Color accentColor)
            {
                Text = "Bookings";
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                ShowInTaskbar = false;
                MinimizeBox = false;
                MaximizeBox = false;
                BackColor = Color.FromArgb(246, 244, 253);
                Font = new Font("Segoe UI", 9F);
                Width = 490;
                Height = Math.Min(720, Math.Max(270, 155 + items.Count * 116));

                Panel header = new Panel
                {
                    Dock = DockStyle.Top,
                    Height = 58,
                    BackColor = accentColor,
                    Padding = new Padding(18, 0, 12, 0)
                };
                Label title = new Label
                {
                    Dock = DockStyle.Fill,
                    Text = "Bookings · " + date.ToString("ddd, dd MMM yyyy"),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleLeft
                };
                header.Controls.Add(title);

                Label hint = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 34,
                    Text = "Select a booking to use its date and time.",
                    ForeColor = Color.FromArgb(91, 85, 122),
                    Font = new Font("Segoe UI", 9F),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(4, 0, 0, 0)
                };

                FlowLayoutPanel list = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    AutoScroll = true,
                    Padding = new Padding(10, 6, 10, 10),
                    BackColor = Color.FromArgb(246, 244, 253)
                };

                foreach (PartyBookingEvent booking in items.OrderBy(x => x.Start))
                {
                    Panel card = new Panel
                    {
                        Width = 430,
                        Height = 104,
                        Margin = new Padding(2, 3, 2, 6),
                        BackColor = Color.White,
                        Padding = new Padding(10, 5, 8, 5),
                        Cursor = Cursors.Hand,
                        Tag = booking
                    };
                    Panel stripe = new Panel
                    {
                        Dock = DockStyle.Left,
                        Width = 5,
                        BackColor = accentColor
                    };
                    TableLayoutPanel details = new TableLayoutPanel
                    {
                        Dock = DockStyle.Fill,
                        ColumnCount = 2,
                        RowCount = 4,
                        Margin = Padding.Empty,
                        Padding = new Padding(7, 0, 0, 0),
                        BackColor = Color.Transparent,
                        Cursor = Cursors.Hand
                    };
                    details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 63F));
                    details.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 37F));
                    details.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
                    details.RowStyles.Add(new RowStyle(SizeType.Absolute, 22F));
                    details.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
                    details.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

                    Label eventName = new Label
                    {
                        Dock = DockStyle.Fill,
                        Text = booking.EventName,
                        ForeColor = Color.FromArgb(38, 35, 73),
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                        AutoEllipsis = true,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Cursor = Cursors.Hand
                    };
                    Label payment = new Label
                    {
                        Dock = DockStyle.Fill,
                        Text = booking.PaymentStatus,
                        ForeColor = Color.White,
                        BackColor = booking.PaymentStatusColor,
                        Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                        TextAlign = ContentAlignment.MiddleLeft,
                        AutoEllipsis = true,
                        Padding = new Padding(7, 0, 3, 0),
                        Margin = new Padding(0, 2, 0, 1),
                        Cursor = Cursors.Hand
                    };
                    Label customer = new Label
                    {
                        Dock = DockStyle.Fill,
                        Text = "Customer: " + (string.IsNullOrWhiteSpace(booking.CustomerName) ? "Not provided" : booking.CustomerName),
                        ForeColor = Color.FromArgb(48, 44, 82),
                        Font = new Font("Segoe UI", 8.5F, FontStyle.Bold),
                        AutoEllipsis = true,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Cursor = Cursors.Hand
                    };
                    Label phone = new Label
                    {
                        Dock = DockStyle.Fill,
                        Text = "Phone: " + (string.IsNullOrWhiteSpace(booking.Phone) ? "Not provided" : booking.Phone),
                        ForeColor = Color.FromArgb(75, 70, 105),
                        Font = new Font("Segoe UI", 8.5F, FontStyle.Regular),
                        AutoEllipsis = true,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Cursor = Cursors.Hand
                    };
                    Label time = new Label
                    {
                        Dock = DockStyle.Fill,
                        Text = booking.Start.ToString("dd MMM yyyy  ·  hh:mm tt") +
                               (booking.End > booking.Start ? "  –  " + booking.End.ToString("dd MMM yyyy  ·  hh:mm tt") : ""),
                        ForeColor = Color.FromArgb(99, 92, 130),
                        Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                        Cursor = Cursors.Hand
                    };
                    details.Controls.Add(eventName, 0, 0);
                    details.SetColumnSpan(eventName, 2);
                    details.Controls.Add(customer, 0, 1);
                    details.Controls.Add(phone, 1, 1);
                    details.Controls.Add(time, 0, 2);
                    details.SetColumnSpan(time, 2);
                    details.Controls.Add(payment, 0, 3);
                    details.SetColumnSpan(payment, 2);
                    card.Controls.Add(details);
                    card.Controls.Add(stripe);
                    EventHandler choose = (s, e) =>
                    {
                        SelectedBooking = booking;
                        DialogResult = DialogResult.OK;
                        Close();
                    };
                    card.Click += choose;
                    eventName.Click += choose;
                    time.Click += choose;
                    card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(239, 234, 255);
                    card.MouseLeave += (s, e) => card.BackColor = Color.White;
                    list.Controls.Add(card);
                }

                Button closeButton = new Button
                {
                    Dock = DockStyle.Bottom,
                    Height = 40,
                    Text = "Close",
                    BackColor = Color.FromArgb(235, 231, 249),
                    ForeColor = Color.FromArgb(64, 52, 119),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    DialogResult = DialogResult.Cancel
                };
                closeButton.FlatAppearance.BorderSize = 0;
                Controls.Add(list);
                Controls.Add(closeButton);
                Controls.Add(hint);
                Controls.Add(header);
                AcceptButton = null;
                CancelButton = closeButton;
            }
        }

        public int SelectedBookingId { get; private set; }

        private sealed class TimeChoiceDialog : Form
        {
            private readonly ComboBox hourBox = new ComboBox();
            private readonly NumericUpDown minuteBox = new NumericUpDown();
            private readonly ComboBox periodBox = new ComboBox();

            public TimeSpan SelectedTime { get; private set; }

            public TimeChoiceDialog(TimeSpan initialTime, Color accentColor)
            {
                Text = "Select Time";
                StartPosition = FormStartPosition.CenterParent;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                ShowInTaskbar = false;
                MinimizeBox = false;
                MaximizeBox = false;
                Width = 390;
                Height = 300;
                BackColor = Color.FromArgb(246, 244, 253);
                Font = new Font("Segoe UI", 9F);

                Panel header = new Panel { Dock = DockStyle.Top, Height = 52, BackColor = accentColor };
                header.Controls.Add(new Label
                {
                    Dock = DockStyle.Fill,
                    Text = "Choose booking time",
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(16, 0, 0, 0)
                });

                int hour = initialTime.Hours % 12;
                if (hour == 0) hour = 12;
                hourBox.DropDownStyle = ComboBoxStyle.DropDownList;
                for (int i = 1; i <= 12; i++) hourBox.Items.Add(i.ToString("00"));
                hourBox.SelectedItem = hour.ToString("00");
                hourBox.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
                hourBox.Width = 82;

                minuteBox.Minimum = 0;
                minuteBox.Maximum = 59;
                minuteBox.Value = initialTime.Minutes;
                minuteBox.Width = 82;
                minuteBox.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
                minuteBox.TextAlign = HorizontalAlignment.Center;

                periodBox.DropDownStyle = ComboBoxStyle.DropDownList;
                periodBox.Items.AddRange(new object[] { "AM", "PM" });
                periodBox.SelectedItem = initialTime.Hours >= 12 ? "PM" : "AM";
                periodBox.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
                periodBox.Width = 82;

                FlowLayoutPanel timeRow = new FlowLayoutPanel
                {
                    Dock = DockStyle.Top,
                    Height = 62,
                    Padding = new Padding(22, 12, 8, 4),
                    BackColor = Color.White,
                    WrapContents = false
                };
                timeRow.Controls.Add(hourBox);
                timeRow.Controls.Add(new Label { Text = ":", AutoSize = true, Font = new Font("Segoe UI", 16F, FontStyle.Bold), Margin = new Padding(4, 7, 4, 0) });
                timeRow.Controls.Add(minuteBox);
                timeRow.Controls.Add(periodBox);

                Label quickLabel = new Label
                {
                    Dock = DockStyle.Top,
                    Height = 30,
                    Text = "Quick select",
                    ForeColor = Color.FromArgb(70, 63, 105),
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Padding = new Padding(16, 0, 0, 0)
                };
                FlowLayoutPanel quickRow = new FlowLayoutPanel
                {
                    Dock = DockStyle.Top,
                    Height = 44,
                    Padding = new Padding(12, 2, 4, 2),
                    WrapContents = false,
                    BackColor = Color.FromArgb(246, 244, 253)
                };
                AddPreset(quickRow, "09:00 AM", 9, 0, "AM");
                AddPreset(quickRow, "12:00 PM", 12, 0, "PM");
                AddPreset(quickRow, "06:00 PM", 6, 0, "PM");
                AddPreset(quickRow, "11:00 PM", 11, 0, "PM");

                FlowLayoutPanel actions = new FlowLayoutPanel
                {
                    Dock = DockStyle.Bottom,
                    Height = 50,
                    FlowDirection = FlowDirection.RightToLeft,
                    Padding = new Padding(8),
                    BackColor = Color.White
                };
                Button ok = new Button
                {
                    Text = "Use Time",
                    Width = 110,
                    Height = 32,
                    BackColor = accentColor,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    DialogResult = DialogResult.OK
                };
                ok.FlatAppearance.BorderSize = 0;
                ok.Click += (s, e) =>
                {
                    int selectedHour = Convert.ToInt32(hourBox.SelectedItem);
                    if (selectedHour == 12) selectedHour = 0;
                    if (Convert.ToString(periodBox.SelectedItem) == "PM") selectedHour += 12;
                    SelectedTime = new TimeSpan(selectedHour, (int)minuteBox.Value, 0);
                };
                Button cancel = new Button
                {
                    Text = "Cancel",
                    Width = 90,
                    Height = 32,
                    BackColor = Color.FromArgb(235, 231, 249),
                    ForeColor = Color.FromArgb(64, 52, 119),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    DialogResult = DialogResult.Cancel
                };
                cancel.FlatAppearance.BorderSize = 0;
                actions.Controls.Add(ok);
                actions.Controls.Add(cancel);

                Controls.Add(actions);
                Controls.Add(quickRow);
                Controls.Add(quickLabel);
                Controls.Add(timeRow);
                Controls.Add(header);
                AcceptButton = ok;
                CancelButton = cancel;
            }

            private void AddPreset(FlowLayoutPanel panel, string caption, int hour, int minute, string period)
            {
                Button button = new Button
                {
                    Text = caption,
                    Width = 82,
                    Height = 30,
                    Margin = new Padding(3),
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(64, 52, 119),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 8F, FontStyle.Bold)
                };
                button.FlatAppearance.BorderColor = Color.FromArgb(218, 211, 243);
                button.Click += (s, e) =>
                {
                    hourBox.SelectedItem = hour.ToString("00");
                    minuteBox.Value = minute;
                    periodBox.SelectedItem = period;
                };
                panel.Controls.Add(button);
            }
        }

        private readonly Color accent = Color.FromArgb(101, 80, 185);
        private readonly Color border = Color.FromArgb(224, 220, 241);
        private readonly Color textColor = Color.FromArgb(38, 35, 73);
        // Shared fonts avoid allocating a new GDI font for every cell on each redraw.
        private static readonly Font WeekdayFont = new Font("Segoe UI", 9F, FontStyle.Bold);
        private static readonly Font DayNumberFont = new Font("Segoe UI", 11F, FontStyle.Bold);
        private static readonly Font TodayNumberFont = new Font("Segoe UI", 12.5F, FontStyle.Bold);
        private static readonly Font EventNameFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        private static readonly Font EventNameCompactFont = new Font("Segoe UI", 8F, FontStyle.Bold);
        private static readonly Font EventTimeFont = new Font("Segoe UI", 8F, FontStyle.Bold);
        private static readonly Font EventTimeCompactFont = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        private static readonly Font MoreBookingsFont = new Font("Segoe UI", 8F, FontStyle.Bold);
        private static readonly Font LegendFont = new Font("Segoe UI", 9F, FontStyle.Bold);
        private readonly Dictionary<string, Color> eventColors = new Dictionary<string, Color>(StringComparer.OrdinalIgnoreCase);
        private List<PartyBookingEvent> bookings = new List<PartyBookingEvent>();
        private DateTime currentDate = DateTime.Today;
        private CalendarView currentView = CalendarView.Month;
        private bool filterApplied;
        private int animationDirection = 1;
        private System.Windows.Forms.Timer calendarAnimationTimer;

        public FrmPartyBookingCalendar()
        {
            InitializeComponent();
            DoubleBuffered = true;
            pnl_calender.AutoScroll = true;
            pnl_calender.BackColor = Color.White;
            flp_indicators.WrapContents = true;
            flp_indicators.AutoScroll = false;

            ApplyModernTheme();
            Resize += FrmPartyBookingCalendar_Resize;
            tlp_navbar.Paint += TlpNavbar_Paint;
            ConfigureDateTimePickers();
            dtp_book_fromtime.MouseClick += BookingTimePicker_MouseClick;
            dtp_book_totime.MouseClick += BookingTimePicker_MouseClick;
        }

        private void ApplyModernTheme()
        {
            BackColor = Color.FromArgb(244, 242, 252);
            Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            Padding = new Padding(1);

            tlp_main.BackColor = Color.FromArgb(244, 242, 252);
            tlp_main.Padding = new Padding(8);
            tlp_navbar.BackColor = accent;
            tlp_navbar.Padding = new Padding(10, 2, 8, 2);
            label12.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            label12.ForeColor = Color.White;
            btncolse.BackColor = Color.FromArgb(239, 68, 88);
            btncolse.ForeColor = Color.White;
            btncolse.FlatStyle = FlatStyle.Flat;
            btncolse.FlatAppearance.BorderSize = 0;
            btncolse.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btncolse.Cursor = Cursors.Hand;

            StyleSurface(tableLayoutPanel1);
            StyleSurface(tableLayoutPanel8);
            StyleSurface(pnl_calender);

            tableLayoutPanel2.BackColor = Color.White;
            tableLayoutPanel3.BackColor = Color.White;
            tableLayoutPanel5.BackColor = Color.White;
            tableLayoutPanel4.BackColor = Color.White;
            tableLayoutPanel7.BackColor = Color.White;
            tableLayoutPanel6.BackColor = Color.White;

            StyleLabels(this);
            // Use real WinForms text buttons rather than stretched bitmap backgrounds.
            // This keeps captions aligned and readable at every supported screen size.
            StyleButton(btn_search, accent);
            btn_search.Text = "Apply Filter";
            btn_search.BackgroundImage = null;
            StyleButton(btnclear, Color.FromArgb(239, 237, 250));
            btnclear.Text = "Clear";
            btnclear.BackgroundImage = null;
            StyleButton(btnsumbit, Color.FromArgb(91, 62, 205));
            btnsumbit.Text = "Create";
            btnsumbit.BackgroundImage = null;
            StyleButton(btn_prev, Color.FromArgb(239, 237, 250));
            btn_prev.Text = "‹";
            btn_prev.BackgroundImage = null;
            StyleButton(btn_next, Color.FromArgb(239, 237, 250));
            btn_next.Text = "›";
            btn_next.BackgroundImage = null;
            StyleButton(btn_today, Color.FromArgb(239, 237, 250));
            btn_today.Text = "Today";
            btn_today.BackgroundImage = null;
            StyleButton(btn_month, accent);
            btn_month.Text = "Month";
            btn_month.BackgroundImage = null;
            StyleButton(btn_week, Color.FromArgb(239, 237, 250));
            btn_week.Text = "Week";
            btn_week.BackgroundImage = null;
            StyleButton(btn_day, Color.FromArgb(239, 237, 250));
            btn_day.Text = "Day";
            btn_day.BackgroundImage = null;

            lbl_calenderinfo.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lbl_calenderinfo.ForeColor = Color.FromArgb(40, 35, 78);
            flp_indicators.BackColor = Color.White;
            flp_indicators.Padding = new Padding(4, 2, 4, 2);
            pnl_calender.Padding = new Padding(2);
        }

        private void StyleSurface(Control control)
        {
            control.BackColor = Color.White;
            control.Padding = new Padding(4);
            control.Margin = new Padding(4);
        }

        private void StyleLabels(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                Label label = control as Label;
                if (label != null && label != label12 && label != lbl_calenderinfo)
                {
                    label.ForeColor = Color.FromArgb(49, 45, 84);
                    if (label.Font.Size < 9F)
                        label.Font = new Font("Segoe UI", 8.5F, FontStyle.Regular);
                }

                DateTimePicker picker = control as DateTimePicker;
                if (picker != null)
                {
                    picker.CalendarMonthBackground = Color.White;
                    picker.CalendarTitleBackColor = accent;
                    picker.CalendarTitleForeColor = Color.White;
                    picker.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
                }

                foreach (Control child in control.Controls)
                    StyleLabels(child);
            }
        }

        private void StyleButton(Button button, Color backColor)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = backColor;
            button.ForeColor = backColor == Color.FromArgb(239, 237, 250)
                ? Color.FromArgb(64, 52, 119)
                : Color.White;
            button.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Padding = new Padding(6, 2, 6, 2);
            button.Margin = new Padding(3);
            button.MinimumSize = new Size(0, 32);
            button.FlatAppearance.MouseOverBackColor = backColor == accent ||
                backColor == Color.FromArgb(91, 62, 205)
                ? Color.FromArgb(119, 96, 211)
                : Color.FromArgb(226, 220, 249);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(79, 57, 170);
            button.BackgroundImage = null;
            button.Resize -= RoundedButton_Resize;
            button.Resize += RoundedButton_Resize;
            ApplyRoundedRegion(button, 10);
        }

        private void RoundedButton_Resize(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control != null)
                ApplyRoundedRegion(control, 10);
        }

        private void TlpNavbar_Paint(object sender, PaintEventArgs e)
        {
            using (System.Drawing.Drawing2D.LinearGradientBrush brush =
                new System.Drawing.Drawing2D.LinearGradientBrush(
                    tlp_navbar.ClientRectangle,
                    Color.FromArgb(91, 65, 190),
                    Color.FromArgb(126, 91, 232),
                    0F))
            {
                e.Graphics.FillRectangle(brush, tlp_navbar.ClientRectangle);
            }
        }

        private void FrmPartyBookingCalendar_Resize(object sender, EventArgs e)
        {
            ApplyRoundedRegion(this, 18);
            ApplyRoundedRegion(tlp_navbar, 14);
            ApplyRoundedRegion(tableLayoutPanel1, 14);
            ApplyRoundedRegion(tableLayoutPanel8, 12);
            ApplyRoundedRegion(pnl_calender, 12);
        }

        private void ApplyRoundedRegion(Control control, int radius)
        {
            if (control == null || control.Width < 2 || control.Height < 2)
                return;

            using (System.Drawing.Drawing2D.GraphicsPath path =
                new System.Drawing.Drawing2D.GraphicsPath())
            {
                int diameter = Math.Max(2, radius * 2);
                Rectangle bounds = new Rectangle(0, 0, control.Width, control.Height);
                path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
                path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
                path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
                path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
                path.CloseFigure();

                Region oldRegion = control.Region;
                control.Region = new Region(path);
                if (oldRegion != null)
                    oldRegion.Dispose();
            }
        }

        private void ConfigureDateTimePickers()
        {
            foreach (DateTimePicker picker in new[]
            {
                dtp_fromdate, dtp_todate, dtp_book_fromdate, dtp_book_todate
            })
            {
                picker.Format = DateTimePickerFormat.Custom;
                picker.CustomFormat = "dd/MM/yyyy";
                picker.ShowUpDown = false;
            }

            foreach (DateTimePicker picker in new[]
            {
                dtp_fromtime, dtp_totime, dtp_book_fromtime, dtp_book_totime
            })
            {
                picker.Format = DateTimePickerFormat.Custom;
                picker.CustomFormat = "hh:mm tt";
                picker.ShowUpDown = true;
            }

            DateTime now = DateTime.Now;
            dtp_fromdate.Value = now.Date;
            dtp_fromtime.Value = DateTime.Today;
            dtp_todate.Value = now.Date.AddMonths(1);
            dtp_totime.Value = DateTime.Today.AddHours(23).AddMinutes(59);
            dtp_book_fromdate.Value = now.Date;
            dtp_book_fromtime.Value = DateTime.Today.Add(now.TimeOfDay);
            dtp_book_todate.Value = now.Date;
            dtp_book_totime.Value = DateTime.Today.AddHours(23);
        }

        private void BookingTimePicker_MouseClick(object sender, MouseEventArgs e)
        {
            DateTimePicker picker = sender as DateTimePicker;
            if (picker == null || e.Button != MouseButtons.Left || e.X >= picker.Width - 22)
                return;

            using (TimeChoiceDialog dialog = new TimeChoiceDialog(picker.Value.TimeOfDay, accent))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                    picker.Value = DateTime.Today.Add(dialog.SelectedTime);
            }
        }

        private void FrmPartyBookingCalendar_Load(object sender, EventArgs e)
        {
            ApplyScreenSize();
            currentDate = DateTime.Today;
            LoadBookings();
            RenderCalendar();
        }

        private void ApplyScreenSize()
        {
            Screen screen = Owner != null
                ? Screen.FromControl(Owner)
                : Screen.FromControl(this);
            Rectangle area = screen.WorkingArea;
            Width = Math.Max(900, (int)(area.Width * 0.92));
            Height = Math.Max(650, (int)(area.Height * 0.92));
            Width = Math.Min(Width, area.Width);
            Height = Math.Min(Height, area.Height);
            StartPosition = FormStartPosition.Manual;
            Location = new Point(
                area.Left + (area.Width - Width) / 2,
                area.Top + (area.Height - Height) / 2);
        }

        private void btncolse_Click(object sender, EventArgs e) { Close(); }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            animationDirection = -1;
            if (currentView == CalendarView.Month) currentDate = currentDate.AddMonths(-1);
            else if (currentView == CalendarView.Week) currentDate = currentDate.AddDays(-7);
            else currentDate = currentDate.AddDays(-1);
            LoadBookings();
            RenderCalendar(true);
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            animationDirection = 1;
            if (currentView == CalendarView.Month) currentDate = currentDate.AddMonths(1);
            else if (currentView == CalendarView.Week) currentDate = currentDate.AddDays(7);
            else currentDate = currentDate.AddDays(1);
            LoadBookings();
            RenderCalendar(true);
        }

        private void btn_month_Click(object sender, EventArgs e)
        {
            currentView = CalendarView.Month;
            animationDirection = 1;
            LoadBookings();
            RenderCalendar(true);
        }

        private void btn_week_Click(object sender, EventArgs e)
        {
            currentView = CalendarView.Week;
            animationDirection = 1;
            LoadBookings();
            RenderCalendar(true);
        }

        private void btn_day_Click(object sender, EventArgs e)
        {
            currentView = CalendarView.Day;
            animationDirection = 1;
            LoadBookings();
            RenderCalendar(true);
        }

        private void btn_today_Click(object sender, EventArgs e)
        {
            currentDate = DateTime.Today;
            LoadBookings();
            RenderCalendar();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            DateTime from = CombineDateAndTime(dtp_fromdate.Value, dtp_fromtime.Value);
            DateTime to = CombineDateAndTime(dtp_todate.Value, dtp_totime.Value);
            if (to < from)
            {
                MessageBox.Show("To date and time must be later than From date and time.",
                    "Invalid date range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            filterApplied = true;
            currentDate = from.Date;
            currentView = CalendarView.Month;
            LoadBookings();
            RenderCalendar();
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            filterApplied = false;
            ConfigureDateTimePickers();
            currentDate = DateTime.Today;
            currentView = CalendarView.Month;
            LoadBookings();
            RenderCalendar();
        }

        private void btnsumbit_Click(object sender, EventArgs e)
        {
            DateTime from = CombineDateAndTime(dtp_book_fromdate.Value, dtp_book_fromtime.Value);
            DateTime to = CombineDateAndTime(dtp_book_todate.Value, dtp_book_totime.Value);
            if (to < from)
            {
                MessageBox.Show("Booking end date and time must be after the start date and time.",
                    "Invalid booking time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (FoodandBeverage.FrmPartyBooking bookingForm = new FoodandBeverage.FrmPartyBooking())
            {
                bookingForm.SetCalendarBookingInterval(from, to);
                bookingForm.ShowDialog(this);
            }

            currentDate = from.Date;
            LoadBookings();
            RenderCalendar();
        }

        private static DateTime CombineDateAndTime(DateTime date, DateTime time)
        {
            return date.Date.Add(time.TimeOfDay);
        }

        private void LoadBookings()
        {
            try
            {
                DateTime visibleStart, visibleEnd;
                GetVisibleRange(out visibleStart, out visibleEnd);

                DateTime queryStart = filterApplied
                    ? CombineDateAndTime(dtp_fromdate.Value, dtp_fromtime.Value)
                    : visibleStart;
                DateTime queryEnd = filterApplied
                    ? CombineDateAndTime(dtp_todate.Value, dtp_totime.Value)
                    : visibleEnd;

                string startText = queryStart.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                string endText = queryEnd.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                DmnSearch search = new DmnSearch
                {
                    ColumnHeaders = "ID,Customer,Phone,Event,Start,End,NetAmount,PaidAmount",
                    Select = "b.PartyBookingId as Id, b.CustomerName as Field1, b.Phone as Field2, " +
                             "ec.ItemName as Field3, b.BookingDateTime as Field4, " +
                             "ISNULL(b.BookingToDateTime, b.BookingDateTime) as Field5, " +
                             "ISNULL(b.NetAmount, 0) as Field6, ISNULL(pay.PaidAmount, 0) as Field7",
                    Table = "tblPartyBooking b " +
                            "LEFT JOIN tblEventCommonItems ec ON ec.EventItemId = b.EventType " +
                            "LEFT JOIN (SELECT PartyBookingId, SUM(PaidAmount) AS PaidAmount " +
                            "FROM tblPartyBookingPayment WHERE Status = 1 GROUP BY PartyBookingId) pay " +
                            "ON pay.PartyBookingId = b.PartyBookingId",
                    WhereNdGroupBy = "WHERE b.Status = 1 AND b.BookingDateTime <= CONVERT(datetime, '" + endText + "', 120) " +
                                     "AND ISNULL(b.BookingToDateTime, b.BookingDateTime) >= CONVERT(datetime, '" + startText + "', 120)",
                    OrderBy = "ORDER BY b.BookingDateTime"
                };

                List<DmnGridFields> result = new BllSearch<DmnGridFields>().GetSearchData("prdSearch", search);
                bookings = new List<PartyBookingEvent>();
                if (result == null)
                {
                    BuildIndicators();
                    return;
                }

                foreach (DmnGridFields row in result)
                {
                    DateTime start;
                    if (!DateTime.TryParse(row.Field4, CultureInfo.CurrentCulture, DateTimeStyles.None, out start))
                        continue;

                    DateTime end;
                    if (!DateTime.TryParse(row.Field5, CultureInfo.CurrentCulture, DateTimeStyles.None, out end))
                        end = start;

                    decimal netAmount, paidAmount;
                    decimal.TryParse(row.Field6, NumberStyles.Any, CultureInfo.CurrentCulture, out netAmount);
                    decimal.TryParse(row.Field7, NumberStyles.Any, CultureInfo.CurrentCulture, out paidAmount);

                    bookings.Add(new PartyBookingEvent
                    {
                        BookingId = Convert.ToInt32(row.Id),
                        Start = start,
                        End = end < start ? start : end,
                        EventName = string.IsNullOrWhiteSpace(row.Field3) ? "Booking" : row.Field3.Trim(),
                        CustomerName = row.Field1,
                        Phone = row.Field2,
                        NetAmount = netAmount,
                        PaidAmount = paidAmount
                    });
                }

                BuildIndicators();
            }
            catch (Exception ex)
            {
                bookings = new List<PartyBookingEvent>();
                BuildIndicators();
                MessageBox.Show("Unable to load party bookings. Please check the database connection.\r\n\r\n" + ex.Message,
                    "Party Booking Calendar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetVisibleRange(out DateTime start, out DateTime end)
        {
            if (currentView == CalendarView.Month)
            {
                DateTime first = new DateTime(currentDate.Year, currentDate.Month, 1);
                start = first.AddDays(-(int)first.DayOfWeek);
                int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
                int calendarDays = (int)first.DayOfWeek + daysInMonth;
                int weekCount = (int)Math.Ceiling(calendarDays / 7.0);
                end = start.AddDays(weekCount * 7).AddTicks(-1);
            }
            else if (currentView == CalendarView.Week)
            {
                start = currentDate.Date.AddDays(-(int)currentDate.DayOfWeek);
                end = start.AddDays(7).AddTicks(-1);
            }
            else
            {
                start = currentDate.Date;
                end = start.AddDays(1).AddTicks(-1);
            }
        }

        private void RenderCalendar(bool animate = false)
        {
            if (IsDisposed || pnl_calender == null) return;
            StopCalendarAnimation();
            pnl_calender.SuspendLayout();
            try
            {
                ClearCalendarControls();
                DateTime rangeStart, rangeEnd;
                GetVisibleRange(out rangeStart, out rangeEnd);

                if (currentView == CalendarView.Month)
                    lbl_calenderinfo.Text = currentDate.ToString("MMMM yyyy");
                else if (currentView == CalendarView.Week)
                    lbl_calenderinfo.Text = rangeStart.ToString("dd MMM") + " – " + rangeEnd.ToString("dd MMM yyyy");
                else
                    lbl_calenderinfo.Text = currentDate.ToString("dddd, dd MMMM yyyy");

                TableLayoutPanel grid = new TableLayoutPanel
                {
                    Dock = animate ? DockStyle.None : DockStyle.Fill,
                    BackColor = border,
                    Margin = Padding.Empty,
                    Padding = new Padding(1),
                    ColumnCount = currentView == CalendarView.Day ? 1 : 7,
                    RowCount = currentView == CalendarView.Month
                        ? (int)Math.Ceiling(((int)new DateTime(currentDate.Year, currentDate.Month, 1).DayOfWeek +
                            DateTime.DaysInMonth(currentDate.Year, currentDate.Month)) / 7.0) + 1
                        : 2,
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                    GrowStyle = TableLayoutPanelGrowStyle.FixedSize
                };

                int columns = currentView == CalendarView.Day ? 1 : 7;
                for (int col = 0; col < columns; col++)
                    grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columns));

                if (currentView == CalendarView.Month)
                {
                    int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
                    int calendarDays = (int)new DateTime(currentDate.Year, currentDate.Month, 1).DayOfWeek + daysInMonth;
                    int weekCount = (int)Math.Ceiling(calendarDays / 7.0);
                    grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                    for (int i = 0; i < weekCount; i++)
                        grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / weekCount));

                    string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
                    for (int col = 0; col < 7; col++)
                        grid.Controls.Add(CreateWeekdayHeader(days[col]), col, 0);

                    int totalDays = weekCount * 7;
                    for (int index = 0; index < totalDays; index++)
                    {
                        DateTime date = rangeStart.AddDays(index);
                        grid.Controls.Add(CreateDayCell(date, date.Month != currentDate.Month, false), index % 7, (index / 7) + 1);
                    }
                }
                else if (currentView == CalendarView.Week)
                {
                    grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
                    grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                    for (int col = 0; col < 7; col++)
                    {
                        DateTime date = rangeStart.AddDays(col);
                        grid.Controls.Add(CreateWeekdayHeader(date.ToString("ddd, dd MMM")), col, 0);
                        grid.Controls.Add(CreateDayCell(date, false, true), col, 1);
                    }
                }
                else
                {
                    grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
                    grid.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
                    grid.Controls.Add(CreateWeekdayHeader(currentDate.ToString("dddd, dd MMMM")), 0, 0);
                    grid.Controls.Add(CreateDayCell(currentDate, false, true), 0, 1);
                }

                if (animate)
                {
                    grid.Dock = DockStyle.None;
                    grid.Size = pnl_calender.ClientSize;
                    grid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                    grid.Location = new Point(animationDirection * 28, 0);
                    pnl_calender.Controls.Add(grid);
                    StartCalendarAnimation(grid);
                }
                else
                {
                    pnl_calender.Controls.Add(grid);
                }
            }
            finally
            {
                pnl_calender.ResumeLayout(true);
            }
        }

        private void StartCalendarAnimation(Control grid)
        {
            calendarAnimationTimer = new System.Windows.Forms.Timer();
            calendarAnimationTimer.Interval = 15;
            int step = 0;
            calendarAnimationTimer.Tick += (s, e) =>
            {
                if (grid.IsDisposed || pnl_calender.IsDisposed)
                {
                    StopCalendarAnimation();
                    return;
                }

                step++;
                int remaining = Math.Max(0, 4 - step);
                grid.Left = animationDirection * remaining * 7;
                if (step >= 4)
                {
                    grid.Left = 0;
                    StopCalendarAnimation();
                }
            };
            calendarAnimationTimer.Start();
        }

        private void StopCalendarAnimation()
        {
            if (calendarAnimationTimer == null) return;
            calendarAnimationTimer.Stop();
            calendarAnimationTimer.Dispose();
            calendarAnimationTimer = null;
        }

        private void ClearCalendarControls()
        {
            foreach (Control control in pnl_calender.Controls.Cast<Control>().ToArray())
            {
                pnl_calender.Controls.Remove(control);
                control.Dispose();
            }
        }

        private Label CreateWeekdayHeader(string caption)
        {
            return new Label
            {
                Text = caption,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(246, 245, 250),
                ForeColor = textColor,
                Font = WeekdayFont,
                Margin = Padding.Empty,
                AutoEllipsis = true
            };
        }

        private Panel CreateDayCell(DateTime date, bool outsideMonth, bool expanded)
        {
            Panel cell = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = date.Date == DateTime.Today ? Color.FromArgb(239, 232, 255) : Color.White,
                Margin = Padding.Empty,
                Padding = new Padding(2),
                Tag = date
            };

            FlowLayoutPanel eventList = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = expanded,
                Padding = Padding.Empty,
                Margin = Padding.Empty,
                BackColor = Color.Transparent
            };

            Label dayNumber = new Label
            {
                Text = date.Day.ToString(),
                Dock = DockStyle.Top,
                Height = 23,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = date.Date == DateTime.Today ? TodayNumberFont : DayNumberFont,
                BackColor = date.Date == DateTime.Today ? Color.FromArgb(101, 65, 220) : Color.Transparent,
                ForeColor = date.Date == DateTime.Today
                    ? Color.White
                    : (outsideMonth ? Color.FromArgb(180, 180, 196)
                    : (date.DayOfWeek == DayOfWeek.Sunday ? Color.FromArgb(220, 48, 67) : textColor)),
                Padding = new Padding(5, 0, 0, 0),
                Margin = new Padding(0, 0, 0, 3)
            };

            // Add the day header first; the fill-docked event list then uses only
            // the remaining area and cannot paint over the date number.
            cell.Controls.Add(dayNumber);
            cell.Controls.Add(eventList);

            List<PartyBookingEvent> dayBookings = bookings
                .Where(b => b.Start.Date <= date.Date && b.End.Date >= date.Date)
                .OrderBy(b => b.Start)
                .ToList();

            int maxVisible;
            if (expanded)
                maxVisible = int.MaxValue;
            else
            {
                // Month cells are intentionally compact: show complete cards and use an
                // overflow count instead of rendering clipped booking text.
                int availableHeight;
                if (currentView == CalendarView.Month)
                {
                    DateTime firstOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                    int daysInMonth = DateTime.DaysInMonth(currentDate.Year, currentDate.Month);
                    int weekCount = (int)Math.Ceiling(((int)firstOfMonth.DayOfWeek + daysInMonth) / 7.0);
                    int estimatedCellHeight = Math.Max(1, pnl_calender.ClientSize.Height / Math.Max(1, weekCount));
                    availableHeight = Math.Max(0, estimatedCellHeight - 28);
                }
                else
                {
                    availableHeight = Math.Max(0, cell.ClientSize.Height - 28);
                }

                maxVisible = Math.Max(1, Math.Min(3, availableHeight / 38));
            }
            foreach (PartyBookingEvent item in dayBookings.Take(maxVisible))
                eventList.Controls.Add(CreateEventCard(item, expanded));

            if (!expanded && dayBookings.Count > maxVisible)
            {
                Label moreBookings = new Label
                {
                    Text = "+" + (dayBookings.Count - maxVisible) + " more booking(s)",
                    AutoSize = false,
                    Height = 20,
                    Width = Math.Max(60, pnl_calender.ClientSize.Width / 7 - 14),
                    ForeColor = accent,
                    Font = MoreBookingsFont,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Margin = new Padding(1),
                    Cursor = Cursors.Hand
                };
                moreBookings.Click += (s, e) => ShowBookingsForDate(date, dayBookings);
                moreBookings.MouseEnter += (s, e) => moreBookings.ForeColor = Color.FromArgb(132, 76, 220);
                moreBookings.MouseLeave += (s, e) => moreBookings.ForeColor = accent;
                eventList.Controls.Add(moreBookings);
            }

            cell.Cursor = Cursors.Hand;
            cell.Click += (s, e) => SelectBookingDate(date);
            dayNumber.Click += (s, e) => SelectBookingDate(date);
            eventList.Click += (s, e) => SelectBookingDate(date);
            return cell;
        }

        private Panel CreateEventCard(PartyBookingEvent item, bool expanded)
        {
            Color baseColor = GetEventColor(item.EventName);
            Panel card = new Panel
            {
                Height = expanded ? 48 : 37,
                Width = Math.Max(80, pnl_calender.ClientSize.Width / (currentView == CalendarView.Day ? 1 : 7) - 16),
                BackColor = ControlPaint.Light(baseColor, 0.78f),
                Margin = new Padding(1, 1, 1, 2),
                Padding = new Padding(5, 1, 3, 1),
                Cursor = Cursors.Hand,
                Tag = item
            };

            TableLayoutPanel cardContent = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = Padding.Empty,
                Padding = Padding.Empty,
                BackColor = Color.Transparent
            };
            cardContent.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            cardContent.RowStyles.Add(new RowStyle(SizeType.Absolute, expanded ? 21F : 17F));
            cardContent.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Label name = new Label
            {
                Text = item.EventName,
                Dock = DockStyle.Fill,
                AutoEllipsis = true,
                ForeColor = Color.FromArgb(45, 42, 75),
                Font = expanded ? EventNameFont : EventNameCompactFont,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = Padding.Empty
            };
            Label time = new Label
            {
                Text = item.Start.ToString("hh:mm tt") + (item.End > item.Start ? " - " + item.End.ToString("hh:mm tt") : ""),
                Dock = DockStyle.Fill,
                AutoEllipsis = true,
                ForeColor = Color.FromArgb(75, 70, 105),
                Font = expanded ? EventTimeFont : EventTimeCompactFont,
                TextAlign = ContentAlignment.MiddleLeft,
                Margin = Padding.Empty
            };
            cardContent.Controls.Add(name, 0, 0);
            cardContent.Controls.Add(time, 0, 1);
            card.Controls.Add(cardContent);
            card.Paint += (s, e) =>
            {
                using (SolidBrush stripe = new SolidBrush(baseColor))
                    e.Graphics.FillRectangle(stripe, 0, 0, 4, card.Height);
            };
            card.Click += (s, e) => ShowBookingDetails(item);
            name.Click += (s, e) => ShowBookingDetails(item);
            time.Click += (s, e) => ShowBookingDetails(item);
            return card;
        }

        private void SelectBookingDate(DateTime date)
        {
            dtp_book_fromdate.Value = date.Date;
            dtp_book_todate.Value = date.Date;
        }

        private void ShowBookingsForDate(DateTime date, IList<PartyBookingEvent> items)
        {
            using (BookingChoiceDialog dialog = new BookingChoiceDialog(date, items, accent))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK || dialog.SelectedBooking == null)
                    return;

                PartyBookingEvent selected = dialog.SelectedBooking;
                SelectedBookingId = selected.BookingId;
                dtp_book_fromdate.Value = selected.Start.Date;
                dtp_book_fromtime.Value = DateTime.Today.Add(selected.Start.TimeOfDay);
                dtp_book_todate.Value = selected.End.Date;
                dtp_book_totime.Value = DateTime.Today.Add(selected.End.TimeOfDay);

                // Returning OK closes the calendar. The parent FrmPartyBooking
                // then loads the selected booking using its normal data-loading workflow.
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void ShowBookingDetails(PartyBookingEvent item)
        {
            ShowBookingsForDate(item.Start.Date, bookings
                .Where(b => b.Start.Date == item.Start.Date)
                .ToList());
        }

        private Color GetEventColor(string eventName)
        {
            if (eventColors.TryGetValue(eventName, out Color color)) return color;
            string value = (eventName ?? "").ToLowerInvariant();
            if (value.Contains("birthday") || value.Contains("cake")) color = Color.FromArgb(255, 71, 145);
            else if (value.Contains("family") || value.Contains("anniversary")) color = Color.FromArgb(35, 132, 244);
            else if (value.Contains("corporate") || value.Contains("business")) color = Color.FromArgb(35, 164, 104);
            else if (value.Contains("engagement") || value.Contains("wedding")) color = Color.FromArgb(146, 78, 235);
            else if (value.Contains("get together") || value.Contains("get-together")) color = Color.FromArgb(245, 145, 35);
            else color = Color.FromArgb(207, 154, 94);
            eventColors[eventName] = color;
            return color;
        }

        private void BuildIndicators()
        {
            flp_indicators.SuspendLayout();
            try
            {
                foreach (Control c in flp_indicators.Controls.Cast<Control>().ToArray())
                {
                    flp_indicators.Controls.Remove(c);
                    c.Dispose();
                }

                string[] categories = bookings
                    .Select(b => b.EventName)
                    .Where(name => !string.IsNullOrWhiteSpace(name) &&
                                   !string.Equals(name, "Booking", StringComparison.OrdinalIgnoreCase))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .OrderBy(name => name)
                    .ToArray();

                foreach (string category in categories)
                {
                    FlowLayoutPanel item = new FlowLayoutPanel
                    {
                        AutoSize = true,
                        WrapContents = false,
                        FlowDirection = FlowDirection.LeftToRight,
                        Margin = new Padding(4, 4, 8, 4),
                        Padding = Padding.Empty,
                        BackColor = Color.Transparent
                    };
                    item.Controls.Add(new Panel
                    {
                        Width = 10,
                        Height = 10,
                        BackColor = GetEventColor(category),
                        Margin = new Padding(0, 4, 4, 0)
                    });
                    item.Controls.Add(new Label
                    {
                        AutoSize = true,
                        Text = category,
                        ForeColor = textColor,
                        Font = LegendFont,
                        Margin = new Padding(0, 0, 2, 0),
                        AutoEllipsis = false
                    });
                    flp_indicators.Controls.Add(item);
                }
            }
            finally { flp_indicators.ResumeLayout(true); }
        }
    }
}