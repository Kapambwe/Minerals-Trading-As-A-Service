using Platform.Trading.Management.Models.Identity;
using Platform.Trading.Management.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Platform.Trading.Management.Services.Mock;

/// <summary>
/// Mock implementation of IIdentityService for development and testing.
/// </summary>
public class MockIdentityService : IIdentityService
{
    private readonly List<UserAccount> _users = new();
    private readonly List<Role> _roles = new();
    private readonly List<ApiToken> _apiTokens = new();

    public MockIdentityService()
    {
        SeedData();
    }

    private void SeedData()
    {
        // Seed roles
        _roles.AddRange(new[]
        {
            new Role
            {
                RoleName = "Administrator",
                Description = "Full system access",
                Category = "System",
                IsSystemRole = true,
                Permissions = new List<Permission>
                {
                    new Permission { PermissionCode = "ADMIN_FULL", PermissionName = "Full Admin Access", Module = "System", Action = "Execute" }
                }
            },
            new Role
            {
                RoleName = "Trader",
                Description = "Trading and order management",
                Category = "Trading",
                Permissions = new List<Permission>
                {
                    new Permission { PermissionCode = "TRADE_CREATE", PermissionName = "Create Trades", Module = "Trading", Action = "Create" },
                    new Permission { PermissionCode = "TRADE_VIEW", PermissionName = "View Trades", Module = "Trading", Action = "Read" },
                    new Permission { PermissionCode = "ORDER_CREATE", PermissionName = "Create Orders", Module = "Trading", Action = "Create" },
                    new Permission { PermissionCode = "ORDER_CANCEL", PermissionName = "Cancel Orders", Module = "Trading", Action = "Delete" }
                }
            },
            new Role
            {
                RoleName = "Compliance Officer",
                Description = "AML/KYC and regulatory compliance",
                Category = "Compliance",
                Permissions = new List<Permission>
                {
                    new Permission { PermissionCode = "KYC_REVIEW", PermissionName = "Review KYC", Module = "Compliance", Action = "Update" },
                    new Permission { PermissionCode = "AML_SCREENING", PermissionName = "AML Screening", Module = "Compliance", Action = "Execute" },
                    new Permission { PermissionCode = "SAR_CREATE", PermissionName = "Create SAR", Module = "Compliance", Action = "Create" },
                    new Permission { PermissionCode = "AUDIT_VIEW", PermissionName = "View Audit Logs", Module = "Audit", Action = "Read" }
                }
            },
            new Role
            {
                RoleName = "Warehouse Manager",
                Description = "Warehouse and inventory management",
                Category = "Operations",
                Permissions = new List<Permission>
                {
                    new Permission { PermissionCode = "WAREHOUSE_MANAGE", PermissionName = "Manage Warehouse", Module = "Warehouse", Action = "Update" },
                    new Permission { PermissionCode = "WARRANT_ISSUE", PermissionName = "Issue Warrants", Module = "Warehouse", Action = "Create" },
                    new Permission { PermissionCode = "CUSTODY_VERIFY", PermissionName = "Verify Custody", Module = "ChainOfCustody", Action = "Update" }
                }
            }
        });

        // Seed users
        _users.AddRange(new[]
        {
            new UserAccount
            {
                Username = "admin",
                Email = "admin@zme.co.zm",
                DisplayName = "System Administrator",
                OrganizationType = "Admin",
                OrganizationName = "Zambia Metal Exchange",
                IsActive = true,
                AccountStatus = "Active",
                MfaEnabled = true,
                MfaMethod = "TOTP",
                MfaVerified = true,
                Roles = new List<string> { "Administrator" },
                Permissions = new List<string> { "ADMIN_FULL" },
                LastLoginDate = DateTime.Now.AddHours(-2)
            },
            new UserAccount
            {
                Username = "jsmith",
                Email = "jsmith@coppertrading.com",
                DisplayName = "John Smith",
                OrganizationId = "buyer-001",
                OrganizationType = "Buyer",
                OrganizationName = "Copper Trading Ltd",
                IsActive = true,
                AccountStatus = "Active",
                MfaEnabled = true,
                MfaMethod = "SMS",
                MfaVerified = true,
                Roles = new List<string> { "Trader" },
                Permissions = new List<string> { "TRADE_CREATE", "TRADE_VIEW", "ORDER_CREATE", "ORDER_CANCEL" },
                LastLoginDate = DateTime.Now.AddHours(-1)
            },
            new UserAccount
            {
                Username = "mwilson",
                Email = "mwilson@zme.co.zm",
                DisplayName = "Mary Wilson",
                OrganizationType = "Exchange",
                OrganizationName = "Zambia Metal Exchange",
                IsActive = true,
                AccountStatus = "Active",
                MfaEnabled = true,
                MfaMethod = "TOTP",
                MfaVerified = true,
                Roles = new List<string> { "Compliance Officer" },
                Permissions = new List<string> { "KYC_REVIEW", "AML_SCREENING", "SAR_CREATE", "AUDIT_VIEW" },
                LastLoginDate = DateTime.Now.AddMinutes(-30)
            }
        });

        // Seed API tokens
        _apiTokens.Add(new ApiToken
        {
            UserId = _users[1].Id,
            TokenName = "Trading Bot Token",
            TokenPrefix = "zme_",
            TokenHash = ComputeHash("zme_test_token_123"),
            Scopes = new List<string> { "read:trades", "write:orders" },
            ExpiryDate = DateTime.Now.AddMonths(6),
            IsActive = true,
            RateLimitPerMinute = 60,
            RateLimitPerHour = 1000
        });
    }

    private string ComputeHash(string input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToBase64String(bytes);
    }

    // User Accounts
    public Task<IEnumerable<UserAccount>> GetAllUsersAsync()
        => Task.FromResult<IEnumerable<UserAccount>>(_users);

    public Task<UserAccount?> GetUserByIdAsync(string id)
        => Task.FromResult(_users.FirstOrDefault(u => u.Id == id));

    public Task<UserAccount?> GetUserByUsernameAsync(string username)
        => Task.FromResult(_users.FirstOrDefault(u => u.Username == username));

    public Task<UserAccount?> GetUserByEmailAsync(string email)
        => Task.FromResult(_users.FirstOrDefault(u => u.Email == email));

    public Task<IEnumerable<UserAccount>> GetUsersByOrganizationAsync(string organizationId)
        => Task.FromResult<IEnumerable<UserAccount>>(_users.Where(u => u.OrganizationId == organizationId));

    public Task<UserAccount> CreateUserAsync(UserAccount user)
    {
        user.Id = Guid.NewGuid().ToString();
        user.CreatedDate = DateTime.Now;
        _users.Add(user);
        return Task.FromResult(user);
    }

    public Task<UserAccount> UpdateUserAsync(UserAccount user)
    {
        var existing = _users.FirstOrDefault(u => u.Id == user.Id);
        if (existing != null)
        {
            _users.Remove(existing);
            user.LastModifiedDate = DateTime.Now;
            _users.Add(user);
        }
        return Task.FromResult(user);
    }

    public Task<bool> DeleteUserAsync(string id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            _users.Remove(user);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<UserAccount> ActivateUserAsync(string id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            user.IsActive = true;
            user.AccountStatus = "Active";
            user.LastModifiedDate = DateTime.Now;
        }
        return Task.FromResult(user!);
    }

    public Task<UserAccount> DeactivateUserAsync(string id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            user.IsActive = false;
            user.AccountStatus = "Suspended";
            user.LastModifiedDate = DateTime.Now;
        }
        return Task.FromResult(user!);
    }

    public Task<UserAccount> LockUserAsync(string id, DateTime? lockoutEndTime)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            user.AccountStatus = "Locked";
            user.LockoutEndTime = lockoutEndTime ?? DateTime.Now.AddMinutes(30);
            user.LastModifiedDate = DateTime.Now;
        }
        return Task.FromResult(user!);
    }

    public Task<UserAccount> UnlockUserAsync(string id)
    {
        var user = _users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            user.AccountStatus = "Active";
            user.LockoutEndTime = null;
            user.FailedLoginAttempts = 0;
            user.LastModifiedDate = DateTime.Now;
        }
        return Task.FromResult(user!);
    }

    // Authentication
    public Task<bool> ValidateCredentialsAsync(string username, string password)
    {
        var user = _users.FirstOrDefault(u => u.Username == username && u.IsActive);
        return Task.FromResult(user != null);
    }

    public Task<bool> ChangePasswordAsync(string userId, string newPassword)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        if (user != null)
        {
            user.PasswordLastChanged = DateTime.Now;
            user.PasswordExpired = false;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task RecordLoginAttemptAsync(string userId, bool successful, string ipAddress)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        if (user != null)
        {
            if (successful)
            {
                user.LastLoginDate = DateTime.Now;
                user.LastLoginIp = ipAddress;
                user.FailedLoginAttempts = 0;
            }
            else
            {
                user.FailedLoginAttempts++;
            }
        }
        return Task.CompletedTask;
    }

    // MFA
    public Task<bool> EnableMfaAsync(string userId, string mfaMethod)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        if (user != null)
        {
            user.MfaEnabled = true;
            user.MfaMethod = mfaMethod;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> DisableMfaAsync(string userId)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        if (user != null)
        {
            user.MfaEnabled = false;
            user.MfaMethod = null;
            user.MfaVerified = false;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> ValidateMfaTokenAsync(string userId, string token)
    {
        return Task.FromResult(token == "123456"); // Mock validation
    }

    // Roles and Permissions
    public Task<IEnumerable<Role>> GetAllRolesAsync()
        => Task.FromResult<IEnumerable<Role>>(_roles);

    public Task<Role?> GetRoleByIdAsync(string id)
        => Task.FromResult(_roles.FirstOrDefault(r => r.Id == id));

    public Task<Role> CreateRoleAsync(Role role)
    {
        role.Id = Guid.NewGuid().ToString();
        role.CreatedDate = DateTime.Now;
        _roles.Add(role);
        return Task.FromResult(role);
    }

    public Task<Role> UpdateRoleAsync(Role role)
    {
        var existing = _roles.FirstOrDefault(r => r.Id == role.Id);
        if (existing != null)
        {
            _roles.Remove(existing);
            role.LastModifiedDate = DateTime.Now;
            _roles.Add(role);
        }
        return Task.FromResult(role);
    }

    public Task<bool> DeleteRoleAsync(string id)
    {
        var role = _roles.FirstOrDefault(r => r.Id == id);
        if (role != null && !role.IsSystemRole)
        {
            _roles.Remove(role);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> AssignRoleToUserAsync(string userId, string roleId)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        var role = _roles.FirstOrDefault(r => r.Id == roleId);
        if (user != null && role != null)
        {
            if (!user.Roles.Contains(role.RoleName))
            {
                user.Roles.Add(role.RoleName);
                user.Permissions.AddRange(role.Permissions.Select(p => p.PermissionCode));
            }
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<bool> RemoveRoleFromUserAsync(string userId, string roleId)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        var role = _roles.FirstOrDefault(r => r.Id == roleId);
        if (user != null && role != null)
        {
            user.Roles.Remove(role.RoleName);
            foreach (var perm in role.Permissions)
            {
                user.Permissions.Remove(perm.PermissionCode);
            }
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<IEnumerable<Permission>> GetUserPermissionsAsync(string userId)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId);
        if (user == null) return Task.FromResult<IEnumerable<Permission>>(new List<Permission>());

        var permissions = new List<Permission>();
        foreach (var roleName in user.Roles)
        {
            var role = _roles.FirstOrDefault(r => r.RoleName == roleName);
            if (role != null)
            {
                permissions.AddRange(role.Permissions);
            }
        }
        return Task.FromResult<IEnumerable<Permission>>(permissions.DistinctBy(p => p.PermissionCode));
    }

    // API Tokens
    public Task<IEnumerable<ApiToken>> GetApiTokensByUserAsync(string userId)
        => Task.FromResult<IEnumerable<ApiToken>>(_apiTokens.Where(t => t.UserId == userId));

    public Task<ApiToken?> GetApiTokenByIdAsync(string id)
        => Task.FromResult(_apiTokens.FirstOrDefault(t => t.Id == id));

    public Task<(ApiToken token, string rawToken)> CreateApiTokenAsync(ApiToken token)
    {
        token.Id = Guid.NewGuid().ToString();
        token.CreatedDate = DateTime.Now;
        var rawToken = $"zme_{Guid.NewGuid():N}";
        token.TokenPrefix = "zme_";
        token.TokenHash = ComputeHash(rawToken);
        _apiTokens.Add(token);
        return Task.FromResult((token, rawToken));
    }

    public Task<bool> ValidateApiTokenAsync(string rawToken)
    {
        var hash = ComputeHash(rawToken);
        var token = _apiTokens.FirstOrDefault(t => t.TokenHash == hash && t.IsActive && !t.IsRevoked);
        if (token != null)
        {
            if (token.ExpiryDate.HasValue && token.ExpiryDate.Value < DateTime.Now)
                return Task.FromResult(false);

            token.LastUsedDate = DateTime.Now;
            token.UsageCount++;
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task<ApiToken> RevokeApiTokenAsync(string id, string revokedBy)
    {
        var token = _apiTokens.FirstOrDefault(t => t.Id == id);
        if (token != null)
        {
            token.IsRevoked = true;
            token.RevokedDate = DateTime.Now;
            token.RevokedBy = revokedBy;
            token.IsActive = false;
        }
        return Task.FromResult(token!);
    }
}
