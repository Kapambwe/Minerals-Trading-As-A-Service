using Platform.Trading.Management.Models.Identity;

namespace Platform.Trading.Management.Services.Interfaces;

/// <summary>
/// Service interface for identity and access management operations.
/// </summary>
public interface IIdentityService
{
    // User Accounts
    Task<IEnumerable<UserAccount>> GetAllUsersAsync();
    Task<UserAccount?> GetUserByIdAsync(string id);
    Task<UserAccount?> GetUserByUsernameAsync(string username);
    Task<UserAccount?> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserAccount>> GetUsersByOrganizationAsync(string organizationId);
    Task<UserAccount> CreateUserAsync(UserAccount user);
    Task<UserAccount> UpdateUserAsync(UserAccount user);
    Task<bool> DeleteUserAsync(string id);
    Task<UserAccount> ActivateUserAsync(string id);
    Task<UserAccount> DeactivateUserAsync(string id);
    Task<UserAccount> LockUserAsync(string id, DateTime? lockoutEndTime);
    Task<UserAccount> UnlockUserAsync(string id);
    
    // Authentication
    Task<bool> ValidateCredentialsAsync(string username, string password);
    Task<bool> ChangePasswordAsync(string userId, string newPassword);
    Task RecordLoginAttemptAsync(string userId, bool successful, string ipAddress);
    
    // MFA
    Task<bool> EnableMfaAsync(string userId, string mfaMethod);
    Task<bool> DisableMfaAsync(string userId);
    Task<bool> ValidateMfaTokenAsync(string userId, string token);
    
    // Roles and Permissions
    Task<IEnumerable<Role>> GetAllRolesAsync();
    Task<Role?> GetRoleByIdAsync(string id);
    Task<Role> CreateRoleAsync(Role role);
    Task<Role> UpdateRoleAsync(Role role);
    Task<bool> DeleteRoleAsync(string id);
    Task<bool> AssignRoleToUserAsync(string userId, string roleId);
    Task<bool> RemoveRoleFromUserAsync(string userId, string roleId);
    Task<IEnumerable<Permission>> GetUserPermissionsAsync(string userId);
    
    // API Tokens
    Task<IEnumerable<ApiToken>> GetApiTokensByUserAsync(string userId);
    Task<ApiToken?> GetApiTokenByIdAsync(string id);
    Task<(ApiToken token, string rawToken)> CreateApiTokenAsync(ApiToken token);
    Task<bool> ValidateApiTokenAsync(string rawToken);
    Task<ApiToken> RevokeApiTokenAsync(string id, string revokedBy);
}
