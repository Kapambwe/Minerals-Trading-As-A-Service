namespace Platform.Trading.Management.Models.Audit;

/// <summary>
/// Represents system access and security events.
/// Addresses Gap TI-008: Security audit trail.
/// </summary>
public class SecurityAuditEvent
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    
    // Event Type
    public string EventType { get; set; } = string.Empty; // Login, Logout, FailedLogin, PasswordChange, MfaChallenge, etc.
    public string EventCategory { get; set; } = string.Empty; // Authentication, Authorization, DataAccess, SystemConfig
    
    // User Information
    public string? UserId { get; set; }
    public string? Username { get; set; }
    public string IpAddress { get; set; } = string.Empty;
    public string? GeoLocation { get; set; }
    public string? DeviceInfo { get; set; }
    
    // Authentication Details
    public string? AuthenticationMethod { get; set; } // Password, MFA, SSO, API Key
    public bool AuthenticationSuccessful { get; set; }
    public string? FailureReason { get; set; }
    public int? FailedAttemptCount { get; set; }
    
    // Session Information
    public string? SessionId { get; set; }
    public DateTime? SessionStartTime { get; set; }
    public DateTime? SessionEndTime { get; set; }
    
    // Risk Indicators
    public bool IsAnomalous { get; set; }
    public string? AnomalyDescription { get; set; }
    public decimal? RiskScore { get; set; }
    
    // Action Taken
    public string? ActionTaken { get; set; } // None, AccountLocked, AlertGenerated, etc.
    
    public string? Notes { get; set; }
}
