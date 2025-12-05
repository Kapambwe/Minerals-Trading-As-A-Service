namespace Platform.Trading.Management.Models.Identity;

/// <summary>
/// Represents a role definition in the system.
/// Addresses Gap TI-010: Role-based access control.
/// </summary>
public class Role
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string RoleName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // System, Trading, Compliance, Admin
    
    // Permissions
    public List<Permission> Permissions { get; set; } = new();
    
    // Hierarchy
    public string? ParentRoleId { get; set; }
    public bool IsSystemRole { get; set; } // Cannot be deleted
    
    // Status
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? LastModifiedDate { get; set; }
}

/// <summary>
/// Represents a permission that can be assigned to roles.
/// </summary>
public class Permission
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string PermissionCode { get; set; } = string.Empty; // e.g., "TRADE_CREATE", "COMPLIANCE_VIEW"
    public string PermissionName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Module { get; set; } = string.Empty; // Trading, Settlement, Compliance, Admin
    public string Action { get; set; } = string.Empty; // Create, Read, Update, Delete, Execute
}

/// <summary>
/// Represents an API token for programmatic access.
/// Addresses Gap TI-002: API Gateway access tokens.
/// </summary>
public class ApiToken
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; } = string.Empty;
    public string TokenName { get; set; } = string.Empty;
    public string? TokenPrefix { get; set; } // First few characters for identification
    public string TokenHash { get; set; } = string.Empty; // Hashed token value
    
    // Scope and Permissions
    public List<string> Scopes { get; set; } = new(); // e.g., "read:trades", "write:orders"
    public List<string> AllowedIpAddresses { get; set; } = new();
    
    // Validity
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime? ExpiryDate { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsRevoked { get; set; }
    public DateTime? RevokedDate { get; set; }
    public string? RevokedBy { get; set; }
    
    // Usage Tracking
    public DateTime? LastUsedDate { get; set; }
    public string? LastUsedIp { get; set; }
    public long UsageCount { get; set; }
    
    // Rate Limiting
    public int? RateLimitPerMinute { get; set; }
    public int? RateLimitPerHour { get; set; }
    
    public string? Notes { get; set; }
}
