using CrystalDecisions.CrystalReports.Engine;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodandBeverage.Services
{
    public sealed class PartyBookingReminder
    {
        public int PartyBookingId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string EventDescription { get; set; }
        public DateTime BookingDateTime { get; set; }
        public DateTime KOTDatetime { get; set; }
        public int NoOfAdults { get; set; }
        public int NoOfChildren { get; set; }
    }

    public sealed class PartyBookingKOTQueueItem
    {
        public long PartyBookingKOTQueueId { get; set; }
        public int PartyBookingId { get; set; }
        public int PartyBookingDetailsId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public int PreparationMode { get; set; }
        public int TableType { get; set; }
        public string CategoryName { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string EventDescription { get; set; }
        public DateTime BookingDateTime { get; set; }
        public DateTime KOTDatetime { get; set; }
        public int NoOfAdults { get; set; }
        public int NoOfChildren { get; set; }
        public int BranchId { get; set; }
    }

    /// <summary>
    /// Runs from the logged-in POS dashboard. The database procedure protects
    /// stock deduction with a booking-row lock and the ConsumedRawMaterialStatus
    /// flag, so repeated timer ticks cannot deduct the same booking twice.
    /// </summary>
    public sealed class PartyBookingKotSchedulerService : IDisposable
    {
        private readonly int _branchId;
        private readonly int _employeeId;
        private readonly System.Windows.Forms.Timer _timer;
        private readonly HashSet<int> _remindedBookingIds = new HashSet<int>();
        private bool _isProcessing;
        private bool _disposed;

        public event Action<IList<PartyBookingReminder>> UpcomingBookingsFound;
        public event Action<int, Exception> ProcessingFailed;

        public PartyBookingKotSchedulerService(int branchId, int employeeId)
        {
            _branchId = branchId;
            _employeeId = employeeId;
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 30000;
            _timer.Tick += Timer_Tick;
        }

        public void Start()
        {
            if (_disposed || _branchId <= 0)
                return;

            if (!_timer.Enabled)
                _timer.Start();

            Timer_Tick(this, EventArgs.Empty);
        }

        public void Stop()
        {
            if (_timer != null)
                _timer.Stop();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            if (_disposed || _isProcessing)
                return;

            _isProcessing = true;
            try
            {
                IList<PartyBookingReminder> upcoming =
                    await Task.Run(() => LoadUpcomingBookings());

                List<PartyBookingReminder> newReminders = upcoming
                    .Where(x => !_remindedBookingIds.Contains(x.PartyBookingId))
                    .ToList();

                foreach (PartyBookingReminder reminder in newReminders)
                    _remindedBookingIds.Add(reminder.PartyBookingId);

                if (newReminders.Count > 0 && UpcomingBookingsFound != null)
                    UpcomingBookingsFound(newReminders);

                List<int> dueBookingIds = await Task.Run(() => LoadDueBookingIds());
                foreach (int bookingId in dueBookingIds)
                {
                    try
                    {
                        using (IDbConnection connection = DAL.DapperRepository<PartyBookingReminder>.OpenConnection())
                        {
                            connection.Execute(
                                "dbo.prdPartyBooking_ProcessScheduledKOT",
                                new
                                {
                                    PartyBookingId = bookingId,
                                    BranchId = _branchId,
                                    EmployeeId = _employeeId
                                },
                                commandType: CommandType.StoredProcedure);
                        }
                    }
                    catch (Exception ex)
                    {
                        RaiseProcessingFailed(bookingId, ex);
                    }
                }

                // Counter-routed items are printed by the POS workstation.
                await Task.Run(() => PartyBookingKOTQueuePrinter.ProcessPending(
                    _branchId,
                    Convert.ToInt32(FoodandBeverage.Interfaces.KOTRouting.Counter),
                    _employeeId));
            }
            catch (Exception ex)
            {
                RaiseProcessingFailed(0, ex);
            }
            finally
            {
                _isProcessing = false;
            }
        }

        private IList<PartyBookingReminder> LoadUpcomingBookings()
        {
            const string sql = @"
SELECT PartyBookingId, CustomerName, Phone, EventDescription, BookingDateTime,
       KOTDatetime, ISNULL(NoOfAdults, 0) AS NoOfAdults,
       ISNULL(NoOfChildren, 0) AS NoOfChildren
FROM dbo.tblPartyBooking
WHERE BranchId = @BranchId
  AND Status = 1
  AND KOTDatetime IS NOT NULL
  AND KOTDatetime >= GETDATE()
  AND KOTDatetime < DATEADD(MINUTE, 45, GETDATE())
  AND ISNULL(KoTPrintStatus, 0) = 0
ORDER BY KOTDatetime;";

            using (IDbConnection connection = DAL.DapperRepository<PartyBookingReminder>.OpenConnection())
            {
                return connection.Query<PartyBookingReminder>(
                    sql, new { BranchId = _branchId }).ToList();
            }
        }

        private List<int> LoadDueBookingIds()
        {
            const string sql = @"
SELECT PartyBookingId
FROM dbo.tblPartyBooking
WHERE BranchId = @BranchId
  AND Status = 1
  AND KOTDatetime IS NOT NULL
  AND KOTDatetime <= GETDATE()
  AND (ISNULL(ConsumedRawMaterialStatus, 0) = 0
       OR ISNULL(KoTPrintStatus, 0) = 0)
ORDER BY KOTDatetime, PartyBookingId;";

            using (IDbConnection connection = DAL.DapperRepository<PartyBookingReminder>.OpenConnection())
            {
                return connection.Query<int>(sql, new { BranchId = _branchId }).ToList();
            }
        }

        private void RaiseProcessingFailed(int bookingId, Exception exception)
        {
            Action<int, Exception> handler = ProcessingFailed;
            if (handler != null)
                handler(bookingId, exception);
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            _disposed = true;
            _timer.Stop();
            _timer.Tick -= Timer_Tick;
            _timer.Dispose();
        }
    }

    /// <summary>
    /// Shared print agent. The POS calls it for Counter items; Kitchen View calls
    /// it for Kitchen items. Each workstation prints on its own Windows default
    /// printer, which keeps the two physical printer routes independent.
    /// </summary>
    public static class PartyBookingKOTQueuePrinter
    {
        private static readonly SemaphoreSlim CounterGate = new SemaphoreSlim(1, 1);
        private static readonly SemaphoreSlim KitchenGate = new SemaphoreSlim(1, 1);

        public static void ProcessPendingAsync(int branchId, int preparationMode, int employeeId)
        {
            SemaphoreSlim gate = preparationMode ==
                Convert.ToInt32(FoodandBeverage.Interfaces.KOTRouting.kitchen)
                    ? KitchenGate
                    : CounterGate;

            if (!gate.Wait(0))
                return;

            Task.Run(() =>
            {
                try
                {
                    ProcessPending(branchId, preparationMode, employeeId);
                }
                finally
                {
                    gate.Release();
                }
            });
        }

        public static void ProcessPending(int branchId, int preparationMode, int employeeId)
        {
            if (branchId <= 0)
                return;

            List<PartyBookingKOTQueueItem> claimed;
            using (IDbConnection connection = DAL.DapperRepository<PartyBookingKOTQueueItem>.OpenConnection())
            {
                claimed = connection.Query<PartyBookingKOTQueueItem>(
                    "dbo.prdPartyBookingKOT_ClaimPending",
                    new
                    {
                        BranchId = branchId,
                        PreparationMode = preparationMode,
                        MachineName = Environment.MachineName
                    },
                    commandType: CommandType.StoredProcedure).ToList();
            }

            foreach (IGrouping<int, PartyBookingKOTQueueItem> ticket in claimed.GroupBy(x => x.PartyBookingId))
            {
                List<PartyBookingKOTQueueItem> rows = ticket.ToList();
                try
                {
                    PrintTicket(rows, preparationMode);

                    foreach (PartyBookingKOTQueueItem row in rows)
                        MarkPrinted(row.PartyBookingKOTQueueId, employeeId);
                }
                catch (Exception ex)
                {
                    foreach (PartyBookingKOTQueueItem row in rows)
                        MarkFailed(row.PartyBookingKOTQueueId, ex.Message);
                }
            }
        }

        private static void PrintTicket(IList<PartyBookingKOTQueueItem> rows, int preparationMode)
        {
            if (rows == null || rows.Count == 0)
                return;

            string reportFile = preparationMode ==
                Convert.ToInt32(FoodandBeverage.Interfaces.KOTRouting.kitchen)
                    ? "crkitchenbill.rpt"
                    : "CRkitchenbillnokot.rpt";

            string reportPath = Path.Combine(
                Convert.ToString(FoodandBeverage.Forms.FrmLogin.KitchenBillPath ?? string.Empty),
                reportFile);

            if (!File.Exists(reportPath))
                throw new FileNotFoundException("Party KOT report file was not found.", reportPath);

            DataTable table = CreateKitchenBillTable();
            decimal totalQuantity = rows.Sum(x => x.Quantity);
            string station = preparationMode ==
                Convert.ToInt32(FoodandBeverage.Interfaces.KOTRouting.kitchen)
                    ? "KITCHEN"
                    : "COUNTER";

            foreach (PartyBookingKOTQueueItem row in rows)
            {
                DataRow data = table.NewRow();
                data["ItemName"] = row.ProductName ?? string.Empty;
                data["Quantity"] = row.Quantity;
                data["Notes"] = string.Empty;
                data["Description"] = row.EventDescription ?? string.Empty;
                data["TableCode"] = "PARTY BOOKING";
                data["OrderMasterId"] = row.PartyBookingId;
                data["AddonStatus"] = 0;
                data["AV_id"] = string.Empty;
                data["MainCategory"] = Convert.ToString(row.ProductId);
                data["MainCategoryCount"] = rows.Count(x => x.ProductId == row.ProductId);
                data["offeritemname"] = string.Empty;
                data["offerqty"] = 0;
                data["TableType"] = row.TableType;
                data["cobmoitemname"] = string.Empty;
                data["Kitchennumber"] = string.Empty;
                data["KitchenNumber"] = string.Empty;
                data["TotalQTY"] = totalQuantity;
                data["Pkid"] = 0;
                data["EmployeeName"] = "Party Booking";
                data["Station"] = station;
                data["GuestDetails"] = "PARTY #" + row.PartyBookingId +
                    " | " + (row.CustomerName ?? string.Empty) +
                    " | PAX A:" + row.NoOfAdults + " C:" + row.NoOfChildren +
                    " | " + (row.Phone ?? string.Empty);
                data["Orderno"] = "PB-" + row.PartyBookingId;
                data["PriorityType"] = 1;
                data["CounterKotNo"] = row.PartyBookingId;
                data["Billtype"] = 0;
                data["TokenNo"] = "PB-" + row.PartyBookingId;
                table.Rows.Add(data);
            }

            Exception printException = null;
            Thread printThread = new Thread(() =>
            {
                ReportDocument report = new ReportDocument();
                try
                {
                    DataSet dataSet = new DataSet();
                    dataSet.Tables.Add(table.Copy());
                    report.Load(reportPath);
                    report.SetDataSource(dataSet);
                    report.PrintToPrinter(1, false, 0, 0);
                }
                catch (Exception ex)
                {
                    printException = ex;
                }
                finally
                {
                    report.Close();
                    report.Dispose();
                }
            });

            printThread.SetApartmentState(ApartmentState.STA);
            printThread.Start();
            printThread.Join();

            if (printException != null)
                throw new InvalidOperationException("Unable to print party KOT on the " + station + " printer.", printException);
        }

        private static DataTable CreateKitchenBillTable()
        {
            DataTable table = new DataTable("DT_kitchenbill");
            table.Columns.Add("ItemName", typeof(string));
            table.Columns.Add("Quantity", typeof(decimal));
            table.Columns.Add("Notes", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("TableCode", typeof(string));
            table.Columns.Add("OrderMasterId", typeof(int));
            table.Columns.Add("AddonStatus", typeof(int));
            table.Columns.Add("AV_id", typeof(string));
            table.Columns.Add("MainCategory", typeof(string));
            table.Columns.Add("MainCategoryCount", typeof(int));
            table.Columns.Add("offeritemname", typeof(string));
            table.Columns.Add("offerqty", typeof(int));
            table.Columns.Add("TableType", typeof(int));
            table.Columns.Add("cobmoitemname", typeof(string));
            table.Columns.Add("Kitchennumber", typeof(string));
            table.Columns.Add("KitchenNumber", typeof(string));
            table.Columns.Add("TotalQTY", typeof(decimal));
            table.Columns.Add("Pkid", typeof(int));
            table.Columns.Add("EmployeeName", typeof(string));
            table.Columns.Add("Station", typeof(string));
            table.Columns.Add("GuestDetails", typeof(string));
            table.Columns.Add("Orderno", typeof(string));
            table.Columns.Add("PriorityType", typeof(int));
            table.Columns.Add("CounterKotNo", typeof(int));
            table.Columns.Add("Billtype", typeof(int));
            table.Columns.Add("TokenNo", typeof(string));
            return table;
        }

        private static void MarkPrinted(long queueId, int employeeId)
        {
            using (IDbConnection connection = DAL.DapperRepository<PartyBookingKOTQueueItem>.OpenConnection())
            {
                connection.Execute(
                    "dbo.prdPartyBookingKOT_MarkPrinted",
                    new { PartyBookingKOTQueueId = queueId, EmployeeId = employeeId },
                    commandType: CommandType.StoredProcedure);
            }
        }

        private static void MarkFailed(long queueId, string error)
        {
            try
            {
                using (IDbConnection connection = DAL.DapperRepository<PartyBookingKOTQueueItem>.OpenConnection())
                {
                    connection.Execute(
                        "dbo.prdPartyBookingKOT_MarkFailed",
                        new
                        {
                            PartyBookingKOTQueueId = queueId,
                            ErrorMessage = (error ?? string.Empty).Length > 1900
                                ? error.Substring(0, 1900)
                                : error
                        },
                        commandType: CommandType.StoredProcedure);
                }
            }
            catch
            {
                // Leave the queue row claimed; the SQL claim procedure reclaims
                // stale claims after its lease expires.
            }
        }
    }
}