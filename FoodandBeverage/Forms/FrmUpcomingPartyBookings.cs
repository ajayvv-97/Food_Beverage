using FoodandBeverage.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FoodandBeverage.Forms
{
    public sealed class FrmUpcomingPartyBookings : Form
    {
        private readonly DataGridView _grid;
        private readonly Label _title;
        private readonly Label _subtitle;
        private readonly Button _closeButton;
        private readonly BindingSource _bindingSource = new BindingSource();
        private readonly Dictionary<int, PartyBookingReminder> _bookings =
            new Dictionary<int, PartyBookingReminder>();

        public FrmUpcomingPartyBookings(IEnumerable<PartyBookingReminder> bookings)
        {
            Text = "Upcoming Party Bookings";
            Name = "FrmUpcomingPartyBookings";
            StartPosition = FormStartPosition.Manual;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            TopMost = true;
            BackColor = Color.White;
            Size = new Size(850, 400);
            MinimumSize = new Size(760, 340);

            _title = new Label
            {
                Text = "Upcoming Party KOT",
                Font = new Font("Poppins", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(35, 48, 72),
                AutoSize = true,
                Location = new Point(18, 14)
            };

            _subtitle = new Label
            {
                Text = "These bookings have a KOT preparation time within the next 45 minutes.",
                Font = new Font("Poppins", 8.5F, FontStyle.Regular),
                ForeColor = Color.DimGray,
                AutoSize = true,
                Location = new Point(20, 47)
            };

            _grid = new DataGridView
            {
                Name = "dgv_upcomingparty",
                Location = new Point(18, 78),
                Size = new Size(800, 235),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoGenerateColumns = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                GridColor = Color.FromArgb(230, 233, 239),
                EnableHeadersVisualStyles = false
            };

            _grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(242, 245, 250);
            _grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(50, 60, 80);
            _grid.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 8F, FontStyle.Bold);
            _grid.DefaultCellStyle.Font = new Font("Poppins", 8F, FontStyle.Regular);
            _grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(224, 235, 250);
            _grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 40, 55);
            _grid.RowTemplate.Height = 34;

            AddTextColumn("PartyBookingId", "Booking #", 78);
            AddTextColumn("CustomerName", "Customer", 145);
            AddTextColumn("Phone", "Phone", 105);
            AddTextColumn("EventDescription", "Event", 155);
            AddTextColumn("BookingDateTime", "Party Time", 125, "dd-MMM hh:mm tt");
            AddTextColumn("KOTDatetime", "KOT Time", 125, "dd-MMM hh:mm tt");
            AddTextColumn("GuestCount", "Guests", 65);

            _closeButton = new Button
            {
                Text = "Close",
                Font = new Font("Poppins", 8.5F, FontStyle.Bold),
                BackColor = Color.FromArgb(43, 90, 160),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 34),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(718, 328)
            };
            _closeButton.FlatAppearance.BorderSize = 0;
            _closeButton.Click += (s, e) => Close();

            Controls.Add(_title);
            Controls.Add(_subtitle);
            Controls.Add(_grid);
            Controls.Add(_closeButton);

            Shown += (s, e) => PositionAtBottomRight();
            UpdateBookings(bookings);
        }

        private void AddTextColumn(string property, string header, int width, string format = null)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                DataPropertyName = property,
                HeaderText = header,
                Name = property,
                Width = width,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };

            if (!string.IsNullOrWhiteSpace(format))
                column.DefaultCellStyle.Format = format;

            _grid.Columns.Add(column);
        }

        public void UpdateBookings(IEnumerable<PartyBookingReminder> bookings)
        {
            if (IsDisposed)
                return;

            if (InvokeRequired)
            {
                BeginInvoke(new Action<IEnumerable<PartyBookingReminder>>(UpdateBookings), bookings);
                return;
            }

            if (bookings != null)
            {
                foreach (PartyBookingReminder booking in bookings)
                {
                    if (booking != null && booking.PartyBookingId > 0)
                        _bookings[booking.PartyBookingId] = booking;
                }
            }

            var rows = _bookings.Values
                .OrderBy(x => x.KOTDatetime)
                .Select(x => new
                {
                    x.PartyBookingId,
                    x.CustomerName,
                    x.Phone,
                    x.EventDescription,
                    x.BookingDateTime,
                    x.KOTDatetime,
                    GuestCount = x.NoOfAdults + x.NoOfChildren
                })
                .ToList();

            _bindingSource.DataSource = rows;
            _grid.DataSource = _bindingSource;
            _subtitle.Text = rows.Count == 1
                ? "1 party booking is approaching its KOT preparation time."
                : rows.Count + " party bookings are approaching their KOT preparation time.";
        }

        private void PositionAtBottomRight()
        {
            Rectangle area = Screen.FromControl(this).WorkingArea;
            Location = new Point(
                Math.Max(area.Left + 10, area.Right - Width - 18),
                Math.Max(area.Top + 10, area.Bottom - Height - 18));
        }
    }
}