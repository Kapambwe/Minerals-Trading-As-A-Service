using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public interface IAuditService
    {
        Task<List<AuditLogEntry>> SearchLogsAsync(DateTime? fromDate = null, DateTime? toDate = null, string? userId = null, string? action = null);
        Task<byte[]> ExportLogsAsync(List<AuditLogEntry> logs);
        Task<bool> AttachToCaseAsync(string logId, string caseId);
    }

    public interface ISystemMonitoringService
    {
        Task<List<SystemMetric>> GetSystemMetricsAsync(string? component = null);
        Task<List<FixSession>> GetFixSessionsAsync();
        Task<bool> ResetSequenceAsync(string sessionId);
        Task<List<ApiEndpoint>> GetApiEndpointsAsync();
        Task<bool> ThrottleEndpointAsync(string endpointId, int maxRequests);
    }

    public interface IReconciliationService
    {
        Task<List<ReconciliationReport>> GetReportsAsync();
        Task<string> RunReconciliationAsync(string reportType, DateTime date);
        Task<List<ReconciliationMismatch>> GetMismatchesAsync(string reportId);
        Task<bool> FlagMismatchAsync(string mismatchId);
        Task<bool> PostJournalEntryAsync(string mismatchId);
        Task<List<Invoice>> GetInvoicesAsync();
        Task<string> GenerateInvoiceAsync(Invoice invoice);
        Task<byte[]> ExportForAccountingAsync(DateTime fromDate, DateTime toDate);
    }
}
