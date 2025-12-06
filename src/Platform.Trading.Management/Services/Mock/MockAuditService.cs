using Platform.Trading.Management.Models.Audit;
using Platform.Trading.Management.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of IAuditService for development and testing.
/// </summary>
public class MockAuditService : IAuditService
{
    private readonly List<AuditLogEntry> _auditLogs = new();
    private readonly List<SecurityAuditEvent> _securityEvents = new();

    public MockAuditService()
    {
        SeedData();
    }

    private void SeedData()
    {
        var entries = new[]
        {
            new AuditLogEntry
            {
                UserId = "user-001",
                Username = "jsmith",
                UserRole = "Trader",
                IpAddress = "192.168.1.100",
                ActionType = "Create",
                ActionCategory = "Trade",
                Description = "Created new trade TRD-2024-001",
                EntityType = "Trade",
                EntityId = "trade-001",
                EntityName = "TRD-2024-001",
                IsSuccessful = true
            },
            new AuditLogEntry
            {
                UserId = "user-002",
                Username = "mwilson",
                UserRole = "Compliance",
                IpAddress = "192.168.1.101",
                ActionType = "Update",
                ActionCategory = "Compliance",
                Description = "Approved KYC for buyer BUY-001",
                EntityType = "Buyer",
                EntityId = "buyer-001",
                EntityName = "Copper Trading Ltd",
                IsSuccessful = true,
                FieldChanges = new List<AuditFieldChange>
                {
                    new AuditFieldChange { FieldName = "KYCStatus", OldValue = "Pending", NewValue = "Approved" }
                }
            }
        };

        foreach (var entry in entries)
        {
            entry.HashValue = ComputeHash(entry);
            _auditLogs.Add(entry);
        }

        // Update chain links
        for (int i = 1; i < _auditLogs.Count; i++)
        {
            _auditLogs[i].PreviousHashValue = _auditLogs[i - 1].HashValue;
        }

        _securityEvents.AddRange(new[]
        {
            new SecurityAuditEvent
            {
                EventType = "Login",
                EventCategory = "Authentication",
                UserId = "user-001",
                Username = "jsmith",
                IpAddress = "192.168.1.100",
                AuthenticationMethod = "Password",
                AuthenticationSuccessful = true,
                SessionId = Guid.NewGuid().ToString()
            },
            new SecurityAuditEvent
            {
                EventType = "FailedLogin",
                EventCategory = "Authentication",
                Username = "unknown",
                IpAddress = "10.0.0.55",
                AuthenticationMethod = "Password",
                AuthenticationSuccessful = false,
                FailureReason = "Invalid credentials",
                FailedAttemptCount = 3,
                IsAnomalous = true,
                AnomalyDescription = "Multiple failed login attempts from unknown IP"
            }
        });
    }

    private string ComputeHash(AuditLogEntry entry)
    {
        var json = JsonSerializer.Serialize(new
        {
            entry.Timestamp,
            entry.UserId,
            entry.ActionType,
            entry.EntityType,
            entry.EntityId,
            entry.Description
        });
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(json));
        return Convert.ToBase64String(bytes);
    }

    public Task<IEnumerable<AuditLogEntry>> GetAuditLogsAsync(DateTime? fromDate = null, DateTime? toDate = null)
    {
        var query = _auditLogs.AsEnumerable();
        if (fromDate.HasValue)
            query = query.Where(l => l.Timestamp >= fromDate.Value);
        if (toDate.HasValue)
            query = query.Where(l => l.Timestamp <= toDate.Value);
        return Task.FromResult<IEnumerable<AuditLogEntry>>(query.OrderByDescending(l => l.Timestamp).ToList());
    }

    public Task<IEnumerable<AuditLogEntry>> GetAuditLogsByEntityAsync(string entityType, string entityId)
        => Task.FromResult<IEnumerable<AuditLogEntry>>(
            _auditLogs.Where(l => l.EntityType == entityType && l.EntityId == entityId));

    public Task<IEnumerable<AuditLogEntry>> GetAuditLogsByUserAsync(string userId)
        => Task.FromResult<IEnumerable<AuditLogEntry>>(_auditLogs.Where(l => l.UserId == userId));

    public Task<IEnumerable<AuditLogEntry>> GetAuditLogsByActionAsync(string actionType)
        => Task.FromResult<IEnumerable<AuditLogEntry>>(_auditLogs.Where(l => l.ActionType == actionType));

    public Task<AuditLogEntry?> GetAuditLogByIdAsync(string id)
        => Task.FromResult(_auditLogs.FirstOrDefault(l => l.Id == id));

    public Task<AuditLogEntry> CreateAuditLogAsync(AuditLogEntry entry)
    {
        entry.Id = Guid.NewGuid().ToString();
        entry.Timestamp = DateTime.UtcNow;
        
        if (_auditLogs.Count > 0)
        {
            entry.PreviousHashValue = _auditLogs[^1].HashValue;
        }
        
        entry.HashValue = ComputeHash(entry);
        _auditLogs.Add(entry);
        return Task.FromResult(entry);
    }

    public Task<bool> VerifyAuditLogIntegrityAsync(string id)
    {
        var entry = _auditLogs.FirstOrDefault(l => l.Id == id);
        if (entry == null) return Task.FromResult(false);
        
        var computedHash = ComputeHash(entry);
        return Task.FromResult(computedHash == entry.HashValue);
    }

    public Task<bool> VerifyAuditChainIntegrityAsync(DateTime fromDate, DateTime toDate)
    {
        var entries = _auditLogs
            .Where(l => l.Timestamp >= fromDate && l.Timestamp <= toDate)
            .OrderBy(l => l.Timestamp)
            .ToList();

        for (int i = 1; i < entries.Count; i++)
        {
            if (entries[i].PreviousHashValue != entries[i - 1].HashValue)
            {
                return Task.FromResult(false);
            }
        }
        return Task.FromResult(true);
    }

    public Task<IEnumerable<SecurityAuditEvent>> GetSecurityEventsAsync(DateTime? fromDate = null, DateTime? toDate = null)
    {
        var query = _securityEvents.AsEnumerable();
        if (fromDate.HasValue)
            query = query.Where(e => e.Timestamp >= fromDate.Value);
        if (toDate.HasValue)
            query = query.Where(e => e.Timestamp <= toDate.Value);
        return Task.FromResult<IEnumerable<SecurityAuditEvent>>(query.OrderByDescending(e => e.Timestamp).ToList());
    }

    public Task<IEnumerable<SecurityAuditEvent>> GetSecurityEventsByUserAsync(string userId)
        => Task.FromResult<IEnumerable<SecurityAuditEvent>>(_securityEvents.Where(e => e.UserId == userId));

    public Task<IEnumerable<SecurityAuditEvent>> GetFailedLoginAttemptsAsync(string? userId = null)
    {
        var query = _securityEvents.Where(e => e.EventType == "FailedLogin");
        if (!string.IsNullOrEmpty(userId))
            query = query.Where(e => e.UserId == userId);
        return Task.FromResult<IEnumerable<SecurityAuditEvent>>(query);
    }

    public Task<SecurityAuditEvent> CreateSecurityEventAsync(SecurityAuditEvent auditEvent)
    {
        auditEvent.Id = Guid.NewGuid().ToString();
        auditEvent.Timestamp = DateTime.UtcNow;
        _securityEvents.Add(auditEvent);
        return Task.FromResult(auditEvent);
    }

    public Task<IEnumerable<SecurityAuditEvent>> GetAnomalousEventsAsync()
        => Task.FromResult<IEnumerable<SecurityAuditEvent>>(_securityEvents.Where(e => e.IsAnomalous));
}
