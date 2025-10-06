using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public interface IClearingService
    {
        Task<List<ClearingMember>> GetClearingMembersAsync();
        Task<List<NettingResult>> GetNettingResultsAsync(DateTime? date = null);
        Task<List<SettlementObligation>> GetSettlementObligationsAsync();
        Task<bool> InitiateSettlementAsync(string obligationId);
        Task<bool> PerformNovationAsync(string tradeId);
        Task<List<MarginCall>> GetMarginCallsAsync();
        Task<string> IssueMarginCallAsync(MarginCall call);
        Task<bool> CloseOutDefaultAsync(string memberId);
    }
}
