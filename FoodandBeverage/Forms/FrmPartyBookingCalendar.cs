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
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
            public string EventName { get; set; }
        }

        private readonly Color accent = Color.FromArgb(101, 80, 185);
        private readonly Color border = Color.FromArgb(224, 220, 241);
        private readonly Color textColor = Color.FromArgb(38, 35, 73);
        // Shared fonts avoid allocating a new GDI font for every cell on each redraw.
        private static readonly Font WeekdayFont = new Font("Segoe UI", 9F, FontStyle.Bold);
        private static readonly Font DayNumberFont = new Font("Segoe UI", 9F, FontStyle.Bold);
        private static readonly Font EventNameFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        private static readonly Font EventNameCompactFont = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        private static readonly Font EventTimeFont = new Font("Segoe UI", 8F, FontStyle.Regular);
        private static readonly Font EventTimeCompactFont = new Font("Segoe UI", 7F, FontStyle.Regular);
        private static readonly Font MoreBookingsFont = new Font("Segoe UI", 7.5F, FontStyle.Bold);
        private static readonly Font LegendFont = new Font("Segoe UI", 8F, FontStyle.Regular);
        private readonly Dictionary<string, Color> eventColors = new Dictionary<string, Color>(StringComparer.OrdinalIgnoreCase);
        private List<PartyBookingEvent> bookings = new List<PartyBookingEvent>();
        private DateTime currentDate = DateTime.Today;
        private CalendarView currentView = CalendarView.Month;
        private bool filterApplied;

        public FrmPartyBookingCalendar()
        {
            InitializeComponent();
            DoubleBuffered = true;
            pnl_calender.AutoScroll = true;
            pnl_calender.BackColor = Color.White;
            flp_indicators.WrapContents = true;
            flp_indicators.AutoScroll = false;
            ConfigureDateTimePickers();
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

        private void FrmPartyBookingCalendar_Load(object sender, EventArgs e)
        {
            currentDate = DateTime.Today;
            BuildIndicators();
            LoadBookings();
            RenderCalendar();
        }

        private void btncolse_Click(object sender, EventArgs e) { Close(); }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            if (currentView == CalendarView.Month) currentDate = currentDate.AddMonths(-1);
            else if (currentView == CalendarView.Week) currentDate = currentDate.AddDays(-7);
            else currentDate = currentDate.AddDays(-1);
            LoadBookings();
            RenderCalendar();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (currentView == CalendarView.Month) currentDate = currentDate.AddMonths(1);
            else if (currentView == CalendarView.Week) currentDate = currentDate.AddDays(7);
            else currentDate = currentDate.AddDays(1);
            LoadBookings();
            RenderCalendar();
        }

        private void btn_month_Click(object sender, EventArgs e)
        {
            currentView = CalendarView.Month;
            LoadBookings();
            RenderCalendar();
        }

        private void btn_week_Click(object sender, EventArgs e)
        {
            currentView = CalendarView.Week;
            LoadBookings();
            RenderCalendar();
        }

        private void btn_day_Click(object sender, EventArgs e)
        {
            currentView = CalendarView.Day;
            LoadBookings();
            RenderCalendar();
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
                bookingForm.SetCalendarBookingStart(from);
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
                    ColumnHeaders = "Start,End,Event",
                    Select = "b.BookingDateTime as Field1, b.BookingToDateTime as Field2, ISNULL(ec.ItemName, 'Other Event') as Field3",
                    Table = "tblPartyBooking b LEFT JOIN tblEventCommonItems ec ON ec.EventItemId = b.EventType",
                    WhereNdGroupBy = "WHERE b.Status = 1 AND b.BookingDateTime <= CONVERT(datetime, '" + endText + "', 120) " +
                                     "AND ISNULL(b.BookingToDateTime, b.BookingDateTime) >= CONVERT(datetime, '" + startText + "', 120)",
                    OrderBy = "ORDER BY b.BookingDateTime"
                };

                List<DmnGridFields> result = new BllSearch<DmnGridFields>().GetSearchData("prdSearch", search);
                bookings = new List<PartyBookingEvent>();
                if (result == null) return;

                foreach (DmnGridFields row in result)
                {
                    DateTime start;
                    if (!DateTime.TryParse(row.Field1, CultureInfo.CurrentCulture, DateTimeStyles.None, out start))
                        continue;

                    DateTime end;
                    if (!DateTime.TryParse(row.Field2, CultureInfo.CurrentCulture, DateTimeStyles.None, out end))
                        end = start;

                    bookings.Add(new PartyBookingEvent
                    {
                        Start = start,
                        End = end < start ? start : end,
                        EventName = string.IsNullOrWhiteSpace(row.Field3) ? "Other Event" : row.Field3.Trim()
                    });
                }
            }
            catch (Exception ex)
            {
                bookings = new List<PartyBookingEvent>();
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
                end = start.AddDays(42).AddTicks(-1);
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

        private void RenderCalendar()
        {
            if (IsDisposed || pnl_calender == null) return;
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
                    Dock = DockStyle.Fill,
                    BackColor = border,
                    Margin = Padding.Empty,
                    Padding = new Padding(1),
                    ColumnCount = currentView == CalendarView.Day ? 1 : 7,
                    RowCount = currentView == CalendarView.Month ? 7 : 2,
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                    GrowStyle = TableLayoutPanelGrowStyle.FixedSize
                };

                int columns = currentView == CalendarView.Day ? 1 : 7;
                for (int col = 0; col < columns; col++)
                    grid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / columns));

                if (currentView == CalendarView.Month)
                {
                    grid.RowStyles.Add(new RowStyle(SizeType.Absolute, 28));
                    for (int i = 0; i < 6; i++)
                        grid.RowStyles.Add(new RowStyle(SizeType.Percent, 16.6667f));

                    string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
                    for (int col = 0; col < 7; col++)
                        grid.Controls.Add(CreateWeekdayHeader(days[col]), col, 0);

                    for (int index = 0; index < 42; index++)
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

                pnl_calender.Controls.Add(grid);
            }
            finally
            {
                pnl_calender.ResumeLayout(true);
            }
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
                BackColor = date.Date == DateTime.Today ? Color.FromArgb(250, 248, 255) : Color.White,
                Margin = Padding.Empty,
                Padding = new Padding(4),
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
                Height = 22,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = DayNumberFont,
                ForeColor = outsideMonth ? Color.Silver : (date.DayOfWeek == DayOfWeek.Sunday ? Color.Firebrick : textColor),
                Margin = Padding.Empty
            };

            cell.Controls.Add(eventList);
            cell.Controls.Add(dayNumber);

            List<PartyBookingEvent> dayBookings = bookings
                .Where(b => b.Start.Date <= date.Date && b.End.Date >= date.Date)
                .OrderBy(b => b.Start)
                .ToList();

            int maxVisible = expanded ? int.MaxValue : 3;
            foreach (PartyBookingEvent item in dayBookings.Take(maxVisible))
                eventList.Controls.Add(CreateEventCard(item, expanded));

            if (!expanded && dayBookings.Count > maxVisible)
            {
                eventList.Controls.Add(new Label
                {
                    Text = "+" + (dayBookings.Count - maxVisible) + " more booking(s)",
                    AutoSize = false,
                    Height = 18,
                    Width = Math.Max(60, pnl_calender.ClientSize.Width / 7 - 14),
                    ForeColor = accent,
                    Font = MoreBookingsFont,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Margin = new Padding(1)
                });
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
                Height = expanded ? 46 : 36,
                Width = Math.Max(80, pnl_calender.ClientSize.Width / (currentView == CalendarView.Day ? 1 : 7) - 18),
                BackColor = ControlPaint.Light(baseColor, 0.78f),
                Margin = new Padding(1, 1, 1, 3),
                Padding = new Padding(4, 2, 3, 2),
                Cursor = Cursors.Hand,
                Tag = item
            };

            Label name = new Label
            {
                Text = item.EventName,
                Dock = DockStyle.Top,
                Height = expanded ? 20 : 17,
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
            card.Controls.Add(time);
            card.Controls.Add(name);
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

        private void ShowBookingDetails(PartyBookingEvent item)
        {
            MessageBox.Show(item.EventName + "\r\nFrom: " + item.Start.ToString("dd/MM/yyyy hh:mm tt") +
                            "\r\nTo: " + item.End.ToString("dd/MM/yyyy hh:mm tt"),
                "Party Booking", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                string[] categories = { "Birthday Party", "Family Gathering", "Corporate Event", "Engagement", "Get Together", "Other Event" };
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
                        Margin = Padding.Empty
                    });
                    flp_indicators.Controls.Add(item);
                }
            }
            finally { flp_indicators.ResumeLayout(true); }
        }
    }
}