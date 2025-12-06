using Platform.Trading.Management.Models.Audit;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Service interface for audit logging operations.
/// </summary>
public interface IAuditService
{
    // Audit Log Entries
    Task<IEnumerable<AuditLogEntry>> GetAuditLogsAsync(DateTime? fromDate = null, DateTime? toDate = null);
    Task<IEnumerable<AuditLogEntry>> GetAuditLogsByEntityAsync(string entityType, string entityId);
    Task<IEnumerable<AuditLogEntry>> GetAuditLogsByUserAsync(string userId);
    Task<IEnumerable<AuditLogEntry>> GetAuditLogsByActionAsync(string actionType);
    Task<AuditLogEntry?> GetAuditLogByIdAsync(string id);
    Task<AuditLogEntry> CreateAuditLogAsync(AuditLogEntry entry);
    Task<bool> VerifyAuditLogIntegrityAsync(string id);
    Task<bool> VerifyAuditChainIntegrityAsync(DateTime fromDate, DateTime toDate);
    
    // Security Audit Events
    Task<IEnumerable<SecurityAuditEvent>> GetSecurityEventsAsync(DateTime? fromDate = null, DateTime? toDate = null);
    Task<IEnumerable<SecurityAuditEvent>> GetSecurityEventsByUserAsync(string userId);
    Task<IEnumerable<SecurityAuditEvent>> GetFailedLoginAttemptsAsync(string? userId = null);
    Task<SecurityAuditEvent> CreateSecurityEventAsync(SecurityAuditEvent auditEvent);
    Task<IEnumerable<SecurityAuditEvent>> GetAnomalousEventsAsync();
}
