namespace Platform.Trading.Management.Models.Identity;

/// <summary>
/// Represents a user account in the system.
/// Addresses Gap TI-010: Identity Management with SSO, MFA, and role-based access control.
/// </summary>
public class UserAccount
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    
    // Organization Linkage
    public string? OrganizationId { get; set; } // Links to Buyer or Seller
    public string? OrganizationType { get; set; } // "Buyer", "Seller", "Admin", "Exchange"
    public string? OrganizationName { get; set; }
    
    // Authentication
    public bool IsActive { get; set; } = true;
    public string AccountStatus { get; set; } = "Active"; // Active, Suspended, Locked, Pending
    public DateTime? PasswordLastChanged { get; set; }
    public bool PasswordExpired { get; set; }
    public int FailedLoginAttempts { get; set; }
    public DateTime? LockoutEndTime { get; set; }
    
    // Multi-Factor Authentication
    public bool MfaEnabled { get; set; }
    public string? MfaMethod { get; set; } // TOTP, SMS, Email, Hardware
    public bool MfaVerified { get; set; }
    
    // Single Sign-On
    public bool SsoEnabled { get; set; }
    public string? SsoProvider { get; set; } // Azure AD, Okta, etc.
    public string? SsoExternalId { get; set; }
    
    // Roles and Permissions
    public List<string> Roles { get; set; } = new(); // e.g., "Trader", "Compliance", "Admin"
    public List<string> Permissions { get; set; } = new(); // Specific permissions
    
    // Session Management
    public DateTime? LastLoginDate { get; set; }
    public string? LastLoginIp { get; set; }
    public int ActiveSessions { get; set; }
    
    // Audit
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string? CreatedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
    public string? LastModifiedBy { get; set; }
    
    public string? Notes { get; set; }
}
