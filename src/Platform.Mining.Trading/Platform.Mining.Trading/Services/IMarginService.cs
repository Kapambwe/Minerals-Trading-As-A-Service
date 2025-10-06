using Platform.Mining.Trading.Models;

namespace Platform.Mining.Trading.Services
{
    public interface IMarginService
    {
        Task<MarginSummary> GetMarginSummaryAsync();
        Task<List<Collateral>> GetCollateralAsync();
        Task<bool> PostCollateralAsync(Collateral collateral);
        Task<bool> WithdrawCollateralAsync(string collateralId);
        Task<bool> AcceptMarginCallAsync(string marginCallId);
        Task<List<string>> GetEligibleCollateralTypesAsync();
    }
}
