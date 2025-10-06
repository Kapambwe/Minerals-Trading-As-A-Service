using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public class MockUserManagementService : IUserManagementService
    {
        private List<UserAccount> _users = new();
        private List<ApiToken> _tokens = new();
        private List<RolePermission> _roles = new();

        public MockUserManagementService()
        {
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            _users = new List<UserAccount>
            {
                new UserAccount
                {
                    UserId = "USER-001",
                    Username = "trader1",
                    Email = "trader1@zmx.com",
                    Role = "Trader",
                    IsActive = true,
                    CreatedDate = DateTime.Now.AddMonths(-6),
                    LastLogin = DateTime.Now.AddHours(-2),
                    MfaEnabled = true,
                    PermittedInstruments = new List<string> { "COPPER-JAN24", "COPPER-FEB24", "EMERALD-FEB24" },
                    Permissions = new List<string> { "trade.view", "trade.create", "order.modify" }
                },
                new UserAccount
                {
                    UserId = "USER-002",
                    Username = "compliance.officer",
                    Email = "compliance@zmx.com",
                    Role = "Compliance",
                    IsActive = true,
                    CreatedDate = DateTime.Now.AddMonths(-12),
                    LastLogin = DateTime.Now.AddDays(-1),
                    MfaEnabled = true,
                    PermittedInstruments = new List<string> { "All" },
                    Permissions = new List<string> { "surveillance.view", "case.create", "account.freeze" }
                },
                new UserAccount
                {
                    UserId = "USER-003",
                    Username = "marketops",
                    Email = "marketops@zmx.com",
                    Role = "MarketOps",
                    IsActive = true,
                    CreatedDate = DateTime.Now.AddYears(-1),
                    LastLogin = DateTime.Now.AddMinutes(-30),
                    MfaEnabled = true,
                    PermittedInstruments = new List<string> { "All" },
                    Permissions = new List<string> { "market.control", "auction.schedule", "circuit.breaker" }
                }
            };

            _tokens = new List<ApiToken>
            {
                new ApiToken
                {
                    TokenId = "TKN-001",
                    UserId = "USER-001",
                    TokenName = "Trading API Key",
                    CreatedDate = DateTime.Now.AddMonths(-3),
                    ExpiryDate = DateTime.Now.AddMonths(9),
                    IsActive = true,
                    Scopes = new List<string> { "trade:read", "trade:write", "market:read" },
                    LastUsed = DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:mm:ss")
                }
            };

            _roles = new List<RolePermission>
            {
                new RolePermission
                {
                    RoleId = "ROLE-001",
                    RoleName = "Trader",
                    Description = "Trading desk trader with order entry capabilities",
                    Permissions = new List<string> { "trade.view", "trade.create", "order.modify", "order.cancel", "position.view" }
                },
                new RolePermission
                {
                    RoleId = "ROLE-002",
                    RoleName = "Compliance",
                    Description = "Compliance and surveillance officer",
                    Permissions = new List<string> { "surveillance.view", "case.create", "account.freeze", "audit.view" }
                },
                new RolePermission
                {
                    RoleId = "ROLE-003",
                    RoleName = "MarketOps",
                    Description = "Market operations and control",
                    Permissions = new List<string> { "market.control", "auction.schedule", "circuit.breaker", "session.manage" }
                },
                new RolePermission
                {
                    RoleId = "ROLE-004",
                    RoleName = "Admin",
                    Description = "System administrator with full access",
                    Permissions = new List<string> { "all" }
                }
            };
        }

        public async Task<List<UserAccount>> GetUsersAsync()
        {
            await Task.Delay(100);
            return _users;
        }

        public async Task<UserAccount> GetUserByIdAsync(string userId)
        {
            await Task.Delay(50);
            return _users.FirstOrDefault(u => u.UserId == userId) ?? new UserAccount();
        }

        public async Task<string> CreateUserAsync(UserAccount user)
        {
            await Task.Delay(150);
            user.UserId = $"USER-{_users.Count + 1:D3}";
            user.CreatedDate = DateTime.Now;
            user.IsActive = true;
            _users.Add(user);
            return user.UserId;
        }

        public async Task<bool> DisableUserAsync(string userId)
        {
            await Task.Delay(100);
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                user.IsActive = false;
                return true;
            }
            return false;
        }

        public async Task<bool> ResetPasswordAsync(string userId)
        {
            await Task.Delay(150);
            // Simulate password reset
            return true;
        }

        public async Task<bool> GrantPermissionAsync(string userId, string permission)
        {
            await Task.Delay(100);
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            if (user != null && !user.Permissions.Contains(permission))
            {
                user.Permissions.Add(permission);
                return true;
            }
            return false;
        }

        public async Task<bool> RevokePermissionAsync(string userId, string permission)
        {
            await Task.Delay(100);
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            if (user != null && user.Permissions.Contains(permission))
            {
                user.Permissions.Remove(permission);
                return true;
            }
            return false;
        }

        public async Task<List<ApiToken>> GetApiTokensAsync(string userId)
        {
            await Task.Delay(100);
            return _tokens.Where(t => t.UserId == userId).ToList();
        }

        public async Task<string> IssueApiKeyAsync(ApiToken token)
        {
            await Task.Delay(150);
            token.TokenId = $"TKN-{_tokens.Count + 1:D3}";
            token.CreatedDate = DateTime.Now;
            token.IsActive = true;
            _tokens.Add(token);
            return token.TokenId;
        }

        public async Task<bool> RevokeApiKeyAsync(string tokenId)
        {
            await Task.Delay(100);
            var token = _tokens.FirstOrDefault(t => t.TokenId == tokenId);
            if (token != null)
            {
                token.IsActive = false;
                return true;
            }
            return false;
        }

        public async Task<List<RolePermission>> GetRolesAsync()
        {
            await Task.Delay(100);
            return _roles;
        }
    }
}
