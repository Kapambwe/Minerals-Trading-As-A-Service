using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public interface IComplianceService
    {
        Task<List<ParticipantProfile>> GetParticipantProfilesAsync();
        Task<ParticipantProfile> GetParticipantByIdAsync(string participantId);
        Task<bool> ApproveParticipantAsync(string participantId);
        Task<bool> RejectParticipantAsync(string participantId, string reason);
        Task<bool> RequestMoreDocumentsAsync(string participantId, string documentType);
        Task<List<ComplianceDocument>> GetDocumentsAsync(string participantId);
        Task<bool> RunSanctionsCheckAsync(string participantId);
        Task<bool> RunPepCheckAsync(string participantId);
        Task<List<AccountLimit>> GetAccountLimitsAsync(string participantId);
        Task<bool> SetAccountLimitAsync(AccountLimit limit);
    }
}
