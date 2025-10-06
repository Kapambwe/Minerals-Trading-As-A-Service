using Platform.Mining.Trading.Models;
using System.Text;

namespace Platform.Mining.Trading.Services
{
    public class MockAuditService : IAuditService
    {
        private List<AuditLogEntry> _logs = new();

        public MockAuditService()
        {
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            _logs = new List<AuditLogEntry>
            {
                new AuditLogEntry
                {
                    LogId = "LOG-001",
                    Timestamp = DateTime.Now.AddHours(-2),
                    UserId = "USER-001",
                    Username = "trader1",
                    Action = "Order.Place",
                    Resource = "COPPER-JAN24",
                    RequestPayload = "{\"orderType\":\"Limit\",\"quantity\":500,\"price\":8500}",
                    ResponsePayload = "{\"orderId\":\"ORD-2024-001\",\"status\":\"Active\"}",
                    SessionId = "SESS-12345",
                    IpAddress = "192.168.1.100",
                    Status = "Success"
                },
                new AuditLogEntry
                {
                    LogId = "LOG-002",
                    Timestamp = DateTime.Now.AddHours(-3),
                    UserId = "USER-002",
                    Username = "compliance.officer",
                    Action = "Account.Freeze",
                    Resource = "ACC-TRADE-045",
                    RequestPayload = "{\"accountId\":\"ACC-TRADE-045\",\"reason\":\"Suspicious activity\"}",
                    ResponsePayload = "{\"success\":true}",
                    SessionId = "SESS-12346",
                    IpAddress = "192.168.1.101",
                    Status = "Success"
                },
                new AuditLogEntry
                {
                    LogId = "LOG-003",
                    Timestamp = DateTime.Now.AddMinutes(-30),
                    UserId = "USER-003",
                    Username = "marketops",
                    Action = "Market.Halt",
                    Resource = "ZMX-001",
                    RequestPayload = "{\"marketId\":\"ZMX-001\",\"reason\":\"Emergency halt\"}",
                    ResponsePayload = "{\"success\":true}",
                    SessionId = "SESS-12347",
                    IpAddress = "192.168.1.102",
                    Status = "Success"
                }
            };
        }

        public async Task<List<AuditLogEntry>> SearchLogsAsync(DateTime? fromDate = null, DateTime? toDate = null, string? userId = null, string? action = null)
        {
            await Task.Delay(150);
            
            var filteredLogs = _logs.AsQueryable();
            
            if (fromDate.HasValue)
                filteredLogs = filteredLogs.Where(l => l.Timestamp >= fromDate.Value);
            
            if (toDate.HasValue)
                filteredLogs = filteredLogs.Where(l => l.Timestamp <= toDate.Value);
            
            if (!string.IsNullOrEmpty(userId))
                filteredLogs = filteredLogs.Where(l => l.UserId == userId);
            
            if (!string.IsNullOrEmpty(action))
                filteredLogs = filteredLogs.Where(l => l.Action.Contains(action, StringComparison.OrdinalIgnoreCase));

            return filteredLogs.OrderByDescending(l => l.Timestamp).ToList();
        }

        public async Task<byte[]> ExportLogsAsync(List<AuditLogEntry> logs)
        {
            await Task.Delay(200);
            
            var sb = new StringBuilder();
            sb.AppendLine("LogId,Timestamp,UserId,Username,Action,Resource,SessionId,IpAddress,Status");
            
            foreach (var log in logs)
            {
                sb.AppendLine($"{log.LogId},{log.Timestamp:yyyy-MM-dd HH:mm:ss},{log.UserId},{log.Username}," +
                             $"{log.Action},{log.Resource},{log.SessionId},{log.IpAddress},{log.Status}");
            }
            
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        public async Task<bool> AttachToCaseAsync(string logId, string caseId)
        {
            await Task.Delay(100);
            // Simulate attaching log to case
            return true;
        }
    }

    public class MockSystemMonitoringService : ISystemMonitoringService
    {
        private Random _random = new();

        public async Task<List<SystemMetric>> GetSystemMetricsAsync(string? component = null)
        {
            await Task.Delay(100);
            
            var metrics = new List<SystemMetric>
            {
                new SystemMetric
                {
                    MetricId = "MET-001",
                    Timestamp = DateTime.Now,
                    Component = "MatchingEngine",
                    MetricName = "CPU Usage",
                    Value = 45.5m,
                    Unit = "%"
                },
                new SystemMetric
                {
                    MetricId = "MET-002",
                    Timestamp = DateTime.Now,
                    Component = "MatchingEngine",
                    MetricName = "Latency",
                    Value = 2.3m,
                    Unit = "ms"
                },
                new SystemMetric
                {
                    MetricId = "MET-003",
                    Timestamp = DateTime.Now,
                    Component = "Database",
                    MetricName = "Connection Pool",
                    Value = 75,
                    Unit = "connections"
                },
                new SystemMetric
                {
                    MetricId = "MET-004",
                    Timestamp = DateTime.Now,
                    Component = "API",
                    MetricName = "Requests Per Second",
                    Value = 1250,
                    Unit = "req/s"
                }
            };

            if (!string.IsNullOrEmpty(component))
                return metrics.Where(m => m.Component == component).ToList();
            
            return metrics;
        }

        public async Task<List<FixSession>> GetFixSessionsAsync()
        {
            await Task.Delay(100);
            
            return new List<FixSession>
            {
                new FixSession
                {
                    SessionId = "FIX-001",
                    CounterpartyId = "BROKER-A",
                    Status = "LoggedOn",
                    InboundSeqNum = 12345,
                    OutboundSeqNum = 12340,
                    LastMessageTime = DateTime.Now.AddMinutes(-2),
                    LatencyMs = 5.2m
                },
                new FixSession
                {
                    SessionId = "FIX-002",
                    CounterpartyId = "BROKER-B",
                    Status = "LoggedOn",
                    InboundSeqNum = 8765,
                    OutboundSeqNum = 8760,
                    LastMessageTime = DateTime.Now.AddMinutes(-1),
                    LatencyMs = 3.8m
                }
            };
        }

        public async Task<bool> ResetSequenceAsync(string sessionId)
        {
            await Task.Delay(150);
            return true;
        }

        public async Task<List<ApiEndpoint>> GetApiEndpointsAsync()
        {
            await Task.Delay(100);
            
            return new List<ApiEndpoint>
            {
                new ApiEndpoint
                {
                    EndpointId = "EP-001",
                    Path = "/api/orders",
                    Method = "POST",
                    IsActive = true,
                    RequestCount = 15420,
                    AvgLatencyMs = 45.2m,
                    ErrorCount = 12
                },
                new ApiEndpoint
                {
                    EndpointId = "EP-002",
                    Path = "/api/marketdata",
                    Method = "GET",
                    IsActive = true,
                    RequestCount = 98765,
                    AvgLatencyMs = 12.5m,
                    ErrorCount = 3
                }
            };
        }

        public async Task<bool> ThrottleEndpointAsync(string endpointId, int maxRequests)
        {
            await Task.Delay(100);
            return true;
        }
    }

    public class MockReconciliationService : IReconciliationService
    {
        private List<ReconciliationReport> _reports = new();
        private List<ReconciliationMismatch> _mismatches = new();
        private List<Invoice> _invoices = new();

        public MockReconciliationService()
        {
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            _reports = new List<ReconciliationReport>
            {
                new ReconciliationReport
                {
                    ReportId = "RECON-001",
                    ReportDate = DateTime.Today,
                    ReportType = "TradeToClear",
                    TotalRecords = 150,
                    MatchedRecords = 148,
                    UnmatchedRecords = 2,
                    Status = "Completed"
                }
            };

            _mismatches = new List<ReconciliationMismatch>
            {
                new ReconciliationMismatch
                {
                    MismatchId = "MIS-001",
                    ReportId = "RECON-001",
                    RecordId = "TRD-2024-0145",
                    MismatchType = "Price Difference",
                    InternalValue = "8500.00",
                    ExternalValue = "8505.00",
                    DifferenceAmount = 5.00m,
                    Status = "Open"
                }
            };

            _invoices = new List<Invoice>
            {
                new Invoice
                {
                    InvoiceId = "INV-001",
                    ParticipantId = "PART-001",
                    InvoiceDate = DateTime.Today,
                    DueDate = DateTime.Today.AddDays(30),
                    TotalAmount = 15250.00m,
                    Currency = "ZMW",
                    Status = "Sent",
                    LineItems = new List<InvoiceLineItem>
                    {
                        new InvoiceLineItem { Description = "Trading Fees", Quantity = 1, UnitPrice = 12500.00m, TotalPrice = 12500.00m },
                        new InvoiceLineItem { Description = "Data Subscription", Quantity = 1, UnitPrice = 2750.00m, TotalPrice = 2750.00m }
                    }
                }
            };
        }

        public async Task<List<ReconciliationReport>> GetReportsAsync()
        {
            await Task.Delay(100);
            return _reports;
        }

        public async Task<string> RunReconciliationAsync(string reportType, DateTime date)
        {
            await Task.Delay(500);
            var report = new ReconciliationReport
            {
                ReportId = $"RECON-{_reports.Count + 1:D3}",
                ReportDate = date,
                ReportType = reportType,
                TotalRecords = 200,
                MatchedRecords = 198,
                UnmatchedRecords = 2,
                Status = "Completed"
            };
            _reports.Add(report);
            return report.ReportId;
        }

        public async Task<List<ReconciliationMismatch>> GetMismatchesAsync(string reportId)
        {
            await Task.Delay(100);
            return _mismatches.Where(m => m.ReportId == reportId).ToList();
        }

        public async Task<bool> FlagMismatchAsync(string mismatchId)
        {
            await Task.Delay(100);
            var mismatch = _mismatches.FirstOrDefault(m => m.MismatchId == mismatchId);
            if (mismatch != null)
            {
                mismatch.Status = "Investigating";
                return true;
            }
            return false;
        }

        public async Task<bool> PostJournalEntryAsync(string mismatchId)
        {
            await Task.Delay(150);
            return true;
        }

        public async Task<List<Invoice>> GetInvoicesAsync()
        {
            await Task.Delay(100);
            return _invoices;
        }

        public async Task<string> GenerateInvoiceAsync(Invoice invoice)
        {
            await Task.Delay(200);
            invoice.InvoiceId = $"INV-{_invoices.Count + 1:D3}";
            invoice.InvoiceDate = DateTime.Today;
            invoice.Status = "Draft";
            _invoices.Add(invoice);
            return invoice.InvoiceId;
        }

        public async Task<byte[]> ExportForAccountingAsync(DateTime fromDate, DateTime toDate)
        {
            await Task.Delay(200);
            var sb = new StringBuilder();
            sb.AppendLine("Export for Accounting");
            sb.AppendLine($"Period: {fromDate:yyyy-MM-dd} to {toDate:yyyy-MM-dd}");
            return Encoding.UTF8.GetBytes(sb.ToString());
        }
    }
}
