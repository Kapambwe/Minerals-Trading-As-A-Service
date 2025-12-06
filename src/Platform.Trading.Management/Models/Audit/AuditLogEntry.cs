namespace Platform.Trading.Management.Models.Audit;

/// <summary>
/// Represents an immutable audit log entry for tracking all system activities.
/// Addresses Gap TI-008: Audit Logging with immutable audit trail.
/// </summary>
public class AuditLogEntry
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    // Actor Information
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string UserRole { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string? UserAgent { get; set; }
    
    // Action Details
    public string ActionType { get; set; } = string.Empty; // Create, Read, Update, Delete, Login, Logout, Export, etc.
    public string ActionCategory { get; set; } = string.Empty; // Trade, Settlement, Compliance, User, System, etc.
    public string Description { get; set; } = string.Empty;
    
    // Entity Information
    public string EntityType { get; set; } = string.Empty; // Trade, Buyer, Seller, Warrant, etc.
    public string EntityId { get; set; } = string.Empty;
    public string? EntityName { get; set; }
    
    // Change Details
    public string? OldValue { get; set; } // JSON representation of old state
    public string? NewValue { get; set; } // JSON representation of new state
    public List<AuditFieldChange> FieldChanges { get; set; } = new();
    
    // Request Context
    public string? RequestId { get; set; } // Correlation ID for tracing
    public string? SessionId { get; set; }
    public string? ApplicationModule { get; set; }
    
    // Outcome
    public bool IsSuccessful { get; set; } = true;
    public string? ErrorMessage { get; set; }
    
    // Tamper Detection
    public string? HashValue { get; set; } // Hash of the log entry for integrity verification
    public string? PreviousHashValue { get; set; } // Hash of previous entry for chain integrity
}

/// <summary>
/// Represents a single field change within an audit log entry.
/// </summary>
public class AuditFieldChange
{
    public string FieldName { get; set; } = string.Empty;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
}
