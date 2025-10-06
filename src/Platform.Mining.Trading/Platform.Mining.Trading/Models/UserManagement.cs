namespace Platform.Mining.Trading.Models
{
    public class UserAccount
    {
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool MfaEnabled { get; set; }
        public List<string> PermittedInstruments { get; set; } = new();
        public List<string> Permissions { get; set; } = new();
    }

    public class ApiToken
    {
        public string TokenId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string TokenName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsActive { get; set; }
        public List<string> Scopes { get; set; } = new();
        public string? LastUsed { get; set; }
    }

    public class RolePermission
    {
        public string RoleId { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Permissions { get; set; } = new();
    }
}
