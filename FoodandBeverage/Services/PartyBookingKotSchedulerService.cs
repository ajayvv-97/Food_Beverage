using CrystalDecisions.CrystalReports.Engine;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
    /// Scheduler runs on the POS dashboard. It catches up all overdue bookings
    /// after startup/restart, processes stock and missing queue rows idempotently,
    /// and prints only the unfinished route assigned to the current workstation.
    /// Queue claiming and print-status updates are enforced by SQL.
    /// </summary>
    public sealed class PartyBookingKotSchedulerService : IDisposable
    {
        private readonly int _branchId;
        private readonly int _employeeId;
        private readonly bool _enabled;
        private readonly int _workstationRole;
        private readonly System.Windows.Forms.Timer _timer;
        private readonly HashSet<int> _remindedBookingIds = new HashSet<int>();
        private bool _isProcessing;
        private bool _disposed;

        public event Action<IList<PartyBookingReminder>> UpcomingBookingsFound;
        public event Action<IList<PartyBookingReminder>> UpcomingBookingsUpdated;
        public event Action<int, Exception> ProcessingFailed;

        public PartyBookingKotSchedulerService(int branchId, int employeeId, bool enabled)
            : this(branchId, employeeId, enabled, 0)
        {
        }

        public PartyBookingKotSchedulerService(
            int branchId, int employeeId, bool enabled, int workstationRole)
        {
            _branchId = branchId;
            _employeeId = employeeId;
            _enabled = enabled;
            _workstationRole = workstationRole;
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = 30000;
            _timer.Tick += Timer_Tick;
        }

        public void Start()
        {
            if (_disposed || !_enabled || _branchId <= 0)
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
            if (_disposed || !_enabled || _isProcessing)
                return;

            _isProcessing = true;
            try
            {
                IList<PartyBookingReminder> upcoming =
                    await Task.Run(() => LoadUpcomingBookings());

                HashSet<int> currentUpcomingIds = new HashSet<int>(
                    upcoming.Select(x => x.PartyBookingId));
                _remindedBookingIds.RemoveWhere(id => !currentUpcomingIds.Contains(id));

                List<PartyBookingReminder> newReminders = upcoming
                    .Where(x => _remindedBookingIds.Add(x.PartyBookingId))
                    .ToList();

                if (newReminders.Count > 0 && UpcomingBookingsFound != null)
                    UpcomingBookingsFound(newReminders);

                Action<IList<PartyBookingReminder>> updatedHandler = UpcomingBookingsUpdated;
                if (updatedHandler != null)
                    updatedHandler(upcoming);

                // Process every due booking that still has an unfinished stock or
                // KOT route. This is intentionally not limited to the upcoming
                // reminder window: when the PC/application starts late (for example,
                // after a power failure), overdue KOTs are recovered immediately.
                //
                // prdPartyBooking_ProcessScheduledKOT is idempotent for stock:
                // ConsumedRawMaterialStatus prevents a second raw-material deduction,
                // while its queue insert creates only missing detail/route rows.
                List<int> dueBookingIds = await Task.Run(() => LoadDueBookingIds());
                foreach (int bookingId in dueBookingIds)
                {
                    try
                    {
                        using (IDbConnection connection =
                            DAL.DapperRepository<PartyBookingReminder>.OpenConnection())
                        {
                            // Re-open a previously printed route when its print flag
                            // was reset after rescheduling. Older databases may not yet
                            // contain the optional upgrade procedure, so use the same
                            // stock-neutral update as a fallback.
                            RequeueResetRoutes(connection, bookingId);

                            // Ensure stock processing and any missing KOT queue rows
                            // exist before the printer claims pending tickets.
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

                // A workstation has exactly one registered role. Process only
                // that route; Office workstations continue to show reminders but
                // must be reconfigured as Kitchen or Counter to print KOTs.
                if (_workstationRole == KOTWorkstationRegistration.Kitchen ||
                    _workstationRole == 0)
                {
                    PartyBookingKOTQueuePrinter.ProcessPendingAsync(
                        _branchId,
                        Convert.ToInt32(FoodandBeverage.Interfaces.KOTRouting.kitchen),
                        _employeeId,
                        RaiseProcessingFailed);
                }

                if (_workstationRole == KOTWorkstationRegistration.Counter ||
                    _workstationRole == 0)
                {
                    PartyBookingKOTQueuePrinter.ProcessPendingAsync(
                        _branchId,
                        Convert.ToInt32(FoodandBeverage.Interfaces.KOTRouting.Counter),
                        _employeeId,
                        RaiseProcessingFailed);
                }
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
                SELECT
                    p.PartyBookingId,
                    p.CustomerName,
                    p.Phone,
                    COALESCE(
                        NULLIF(LTRIM(RTRIM(e.ItemName)), ''),
                        NULLIF(LTRIM(RTRIM(p.EventDescription)), ''),
                        '') AS EventDescription,
                    p.BookingDateTime,
                    p.KOTDatetime,
                    ISNULL(p.NoOfAdults, 0) AS NoOfAdults,
                    ISNULL(p.NoOfChildren, 0) AS NoOfChildren
                FROM dbo.tblPartyBooking p
                LEFT JOIN dbo.tblEventCommonItems e
                    ON e.EventItemId = p.EventType
                   AND e.Status = 1
                WHERE p.BranchId = @BranchId
                  AND p.Status = 1
                  AND p.KOTDatetime IS NOT NULL
                  AND p.KOTDatetime >= GETDATE()
                  AND p.KOTDatetime < DATEADD(MINUTE, 45, GETDATE())
                  AND
                  (
                      ISNULL(p.KoTPrintStatus_Kitchen, 0) = 0
                      OR ISNULL(p.KoTPrintStatus_counter, 0) = 0
                  )
                ORDER BY p.KOTDatetime;";

            using (IDbConnection connection =
                DAL.DapperRepository<PartyBookingReminder>.OpenConnection())
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
                  AND
                  (
                      ISNULL(ConsumedRawMaterialStatus, 0) = 0
                      OR ISNULL(KoTPrintStatus_Kitchen, 0) = 0
                      OR ISNULL(KoTPrintStatus_counter, 0) = 0
                      OR EXISTS
                      (
                          SELECT 1
                          FROM dbo.tblPartyBookingKOTQueue q
                          WHERE q.PartyBookingId = tblPartyBooking.PartyBookingId
                            AND q.BranchId = tblPartyBooking.BranchId
                            AND q.KOTDatetime = tblPartyBooking.KOTDatetime
                            AND q.Status IN (0, 1)
                      )
                  )
                ORDER BY KOTDatetime, PartyBookingId;";

            using (IDbConnection connection =
                DAL.DapperRepository<PartyBookingReminder>.OpenConnection())
            {
                return connection.Query<int>(sql, new { BranchId = _branchId }).ToList();
            }
        }

        private void RequeueResetRoutes(IDbConnection connection, int bookingId)
        {
            const string procedureExistsSql = @"
                SELECT CASE
                    WHEN OBJECT_ID(N'dbo.prdPartyBookingKOT_RequeueResetRoutes', N'P') IS NULL
                        THEN 0
                    ELSE 1
                END;";

            int procedureExists = connection.ExecuteScalar<int>(procedureExistsSql);
            if (procedureExists == 1)
            {
                connection.Execute(
                    "dbo.prdPartyBookingKOT_RequeueResetRoutes",
                    new
                    {
                        PartyBookingId = bookingId,
                        BranchId = _branchId
                    },
                    commandType: CommandType.StoredProcedure);
                return;
            }

            // Compatibility fallback for databases where the manual-reschedule
            // upgrade script has not been applied yet. This only reopens print
            // queue rows; it never changes stock or stock-deduction status.
            const string fallbackSql = @"
                UPDATE q
                SET
                    q.ProductName = LEFT(CONVERT(NVARCHAR(250),
                        COALESCE(
                            NULLIF(LTRIM(RTRIM(bd.Product)), N''),
                            NULLIF(LTRIM(RTRIM(p.Product)), N''), N'')), 250),
                    q.Quantity = CONVERT(DECIMAL(18,3), bd.Qty),
                    q.TableType = ISNULL(bd.TableType, 0),
                    q.CategoryName = bd.CategoryName,
                    q.CustomerName = b.CustomerName,
                    q.Phone = b.Phone,
                    q.EventDescription = b.EventDescription,
                    q.BookingDateTime = b.BookingDateTime,
                    q.KOTDatetime = b.KOTDatetime,
                    q.NoOfAdults = ISNULL(b.NoOfAdults, 0),
                    q.NoOfChildren = ISNULL(b.NoOfChildren, 0),
                    q.Status = 0,
                    q.ClaimedOn = NULL,
                    q.PrintedOn = NULL,
                    q.PrintedBy = NULL,
                    q.PrintMachine = NULL,
                    q.LastError = NULL
                FROM dbo.tblPartyBookingKOTQueue q
                INNER JOIN dbo.tblPartyBooking b
                    ON b.PartyBookingId = q.PartyBookingId
                   AND b.BranchId = q.BranchId
                INNER JOIN dbo.tblPartyBookingDetails bd
                    ON bd.PartyBookingId = q.PartyBookingId
                   AND bd.PartyBookingDetailsId = q.PartyBookingDetailsId
                   AND bd.BranchId = q.BranchId
                   AND bd.Status = 1
                   AND bd.ProductType = 9
                   AND ISNULL(bd.Qty, 0) > 0
                INNER JOIN dbo.tblProduct p
                    ON p.ProductId = bd.ProductId
                   AND p.Status = 1
                   AND p.PreparationMode = q.PreparationMode
                WHERE q.PartyBookingId = @PartyBookingId
                  AND q.BranchId = @BranchId
                  AND q.Status IN (0, 2)
                  AND b.Status = 1
                  AND b.KOTDatetime IS NOT NULL
                  AND
                  (
                      (q.PreparationMode = 1 AND ISNULL(b.KoTPrintStatus_Kitchen, 0) = 0)
                      OR
                      (q.PreparationMode = 2 AND ISNULL(b.KoTPrintStatus_counter, 0) = 0)
                  );";

            connection.Execute(
                fallbackSql,
                new
                {
                    PartyBookingId = bookingId,
                    BranchId = _branchId
                });
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
    /// Route-specific printer worker. Kitchen View requests PreparationMode=1;
    /// POS dashboard requests PreparationMode=2. SQL verifies that the calling
    /// machine is registered for that exact station before any queue row is
    /// claimed. The local Windows default printer is used on that workstation.
    /// </summary>
    public static class PartyBookingKOTQueuePrinter
    {
        private static readonly SemaphoreSlim CounterGate = new SemaphoreSlim(1, 1);
        private static readonly SemaphoreSlim KitchenGate = new SemaphoreSlim(1, 1);

        private sealed class PartyBookingKOTPrintContext
        {
            public string EventType { get; set; }
            public string EmployeeName { get; set; }
        }

        public static void ProcessPendingAsync(
            int branchId,
            int preparationMode,
            int employeeId,
            Action<int, Exception> processingFailed = null)
        {
            if (preparationMode != 1 && preparationMode != 2)
                return;

            SemaphoreSlim gate = preparationMode == 1 ? KitchenGate : CounterGate;
            if (!gate.Wait(0))
                return;

            Task.Run(() =>
            {
                try
                {
                    ProcessPending(branchId, preparationMode, employeeId, processingFailed);
                }
                finally
                {
                    gate.Release();
                }
            });
        }

        public static void ProcessPending(int branchId, int preparationMode, int employeeId)
        {
            ProcessPending(branchId, preparationMode, employeeId, null);
        }

        private static void ProcessPending(
            int branchId,
            int preparationMode,
            int employeeId,
            Action<int, Exception> processingFailed)
        {
            if (branchId <= 0 || (preparationMode != 1 && preparationMode != 2))
                return;

            try
            {
                List<PartyBookingKOTQueueItem> claimed;
                using (IDbConnection connection =
                    DAL.DapperRepository<PartyBookingKOTQueueItem>.OpenConnection())
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

                foreach (IGrouping<int, PartyBookingKOTQueueItem> ticket in
                    claimed.GroupBy(x => x.PartyBookingId))
                {
                    List<PartyBookingKOTQueueItem> rows = ticket.ToList();
                    try
                    {
                        PartyBookingKOTPrintContext printContext =
                            LoadPartyBookingKOTPrintContext(
                                rows[0].PartyBookingId,
                                rows[0].BranchId);

                        PrintTicket(rows, preparationMode, printContext);
                        MarkTicketPrinted(
                            rows[0].PartyBookingId,
                            rows[0].BranchId,
                            preparationMode,
                            employeeId);
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            MarkTicketFailed(
                                rows[0].PartyBookingId,
                                rows[0].BranchId,
                                preparationMode,
                                Environment.MachineName,
                                ex.Message);
                        }
                        catch (Exception markFailedException)
                        {
                            Trace.TraceError(
                                "Unable to reset failed Party Booking KOT queue. Booking={0}. {1}",
                                rows[0].PartyBookingId,
                                markFailedException);
                        }

                        Trace.TraceError(
                            "Party booking KOT print failed. Booking={0}, Station={1}, Machine={2}. {3}",
                            rows[0].PartyBookingId,
                            preparationMode == 1 ? "KITCHEN" : "COUNTER",
                            Environment.MachineName,
                            ex);

                        NotifyProcessingFailed(
                            processingFailed,
                            rows[0].PartyBookingId,
                            ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.TraceError(
                    "Party booking KOT worker failed. Branch={0}, PreparationMode={1}, Machine={2}. {3}",
                    branchId, preparationMode, Environment.MachineName, ex);

                NotifyProcessingFailed(processingFailed, 0, ex);
            }
        }

        private static void NotifyProcessingFailed(
            Action<int, Exception> processingFailed,
            int bookingId,
            Exception exception)
        {
            if (processingFailed == null)
                return;

            try
            {
                processingFailed(bookingId, exception);
            }
            catch (Exception callbackException)
            {
                Trace.TraceError("Party Booking KOT error callback failed. {0}", callbackException);
            }
        }

        private static PartyBookingKOTPrintContext LoadPartyBookingKOTPrintContext(
            int partyBookingId,
            int branchId)
        {
            const string sql = @"
                SELECT
                    COALESCE(NULLIF(LTRIM(RTRIM(e.ItemName)), ''), '') AS EventType,
                    COALESCE(NULLIF(LTRIM(RTRIM(emp.EmployeeName)), ''), '') AS EmployeeName
                FROM dbo.tblPartyBooking b
                LEFT JOIN dbo.tblEventCommonItems e
                    ON e.EventItemId = b.EventType
                   AND e.Status = 1
                LEFT JOIN dbo.tblEmployee emp
                    ON emp.EmployeeId = b.CreatedBy
                   AND emp.BranchId = b.BranchId
                WHERE b.PartyBookingId = @PartyBookingId
                  AND b.BranchId = @BranchId;";

            using (IDbConnection connection =
                DAL.DapperRepository<PartyBookingKOTPrintContext>.OpenConnection())
            {
                return connection.Query<PartyBookingKOTPrintContext>(
                    sql,
                    new
                    {
                        PartyBookingId = partyBookingId,
                        BranchId = branchId
                    }).FirstOrDefault() ?? new PartyBookingKOTPrintContext
                    {
                        EventType = string.Empty,
                        EmployeeName = string.Empty
                    };
            }
        }

        private static void PrintTicket(
            IList<PartyBookingKOTQueueItem> rows,
            int preparationMode,
            PartyBookingKOTPrintContext printContext)
        {
            if (rows == null || rows.Count == 0)
                return;

            string station = preparationMode == 1 ? "KITCHEN" : "COUNTER";
            DataTable table = CreatePartyKitchenBillTable();
            decimal totalQuantity = rows.Sum(x => x.Quantity);

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
                data["EmployeeName"] = printContext == null
                    ? string.Empty
                    : (printContext.EmployeeName ?? string.Empty);
                data["Station"] = station;
                data["GuestDetails"] =
                    "PAX Adult :" + row.NoOfAdults + " Child :" + row.NoOfChildren;
                data["Orderno"] = "PB-" + row.PartyBookingId;
                data["PriorityType"] = 1;
                data["CounterKotNo"] = row.PartyBookingId;
                data["Billtype"] = 0;
                data["TokenNo"] = "PB-" + row.PartyBookingId;
                table.Rows.Add(data);
            }

            // Print directly through Crystal Reports. Do not depend on the
            // Kitchen View form being open and do not display a report preview.
            PrintCrystalReportDirectly(
                table,
                printContext == null ? string.Empty : printContext.EventType);
        }

        private static void PrintCrystalReportDirectly(
            DataTable ticketTable,
            string eventType)
        {
            Exception printException = null;

            // Crystal Reports printing is executed on a dedicated STA thread.
            // A CrystalReportViewer/Form is not required for PrintToPrinter and
            // can display a preview window instead of silently printing the KOT.
            Thread printThread = new Thread(() =>
            {
                ReportDocument report = null;
                DataSet reportData = null;

                try
                {
                    report = new FoodandBeverage.Forms.KitchenBill.CRKitchenBill_Party();
                    reportData = new DataSet();
                    reportData.Tables.Add(ticketTable.Copy());

                    report.SetDataSource(reportData);
                    report.SetParameterValue("Eventtype", eventType ?? string.Empty);

                    // Uses the Windows default printer configured on this PC.
                    report.PrintToPrinter(1, false, 0, 0);
                }
                catch (Exception ex)
                {
                    printException = ex;
                }
                finally
                {
                    if (report != null)
                    {
                        try { report.Close(); } catch { }
                        try { report.Dispose(); } catch { }
                    }

                    if (reportData != null)
                    {
                        try { reportData.Dispose(); } catch { }
                    }
                }
            });

            printThread.IsBackground = true;
            printThread.SetApartmentState(ApartmentState.STA);
            printThread.Start();
            printThread.Join();

            if (printException != null)
                throw new InvalidOperationException(
                    "Unable to send the Party Booking KOT to the Windows default printer.",
                    printException);
        }

        private static DataTable CreatePartyKitchenBillTable()
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

        private static void MarkTicketPrinted(
            int partyBookingId, int branchId, int preparationMode, int employeeId)
        {
            using (IDbConnection connection =
                DAL.DapperRepository<PartyBookingKOTQueueItem>.OpenConnection())
            {
                connection.Execute(
                    "dbo.prdPartyBookingKOT_MarkPrinted",
                    new
                    {
                        PartyBookingId = partyBookingId,
                        BranchId = branchId,
                        PreparationMode = preparationMode,
                        EmployeeId = employeeId,
                        MachineName = Environment.MachineName
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }

        private static void MarkTicketFailed(
            int partyBookingId, int branchId, int preparationMode,
            string machineName, string error)
        {
            using (IDbConnection connection =
                DAL.DapperRepository<PartyBookingKOTQueueItem>.OpenConnection())
            {
                connection.Execute(
                    "dbo.prdPartyBookingKOT_MarkFailed",
                    new
                    {
                        PartyBookingId = partyBookingId,
                        BranchId = branchId,
                        PreparationMode = preparationMode,
                        MachineName = machineName,
                        ErrorMessage = (error ?? string.Empty).Length > 1900
                            ? error.Substring(0, 1900)
                            : error
                    },
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}