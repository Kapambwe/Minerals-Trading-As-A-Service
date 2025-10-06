using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public interface IUserManagementService
    {
        Task<List<UserAccount>> GetUsersAsync();
        Task<UserAccount> GetUserByIdAsync(string userId);
        Task<string> CreateUserAsync(UserAccount user);
        Task<bool> DisableUserAsync(string userId);
        Task<bool> ResetPasswordAsync(string userId);
        Task<bool> GrantPermissionAsync(string userId, string permission);
        Task<bool> RevokePermissionAsync(string userId, string permission);
        Task<List<ApiToken>> GetApiTokensAsync(string userId);
        Task<string> IssueApiKeyAsync(ApiToken token);
        Task<bool> RevokeApiKeyAsync(string tokenId);
        Task<List<RolePermission>> GetRolesAsync();
    }
}
