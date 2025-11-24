using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public interface IMarginManager
{
    Task<IEnumerable<Margin>> GetAllMarginsAsync();
    Task<Margin?> GetMarginByIdAsync(string id);
    Task<IEnumerable<Margin>> GetMarginsByTradeIdAsync(string tradeId);
    Task<Margin> CreateMarginAsync(Margin margin);
    Task<Margin> UpdateMarginAsync(Margin margin);
    Task<bool> DeleteMarginAsync(string id);
    Task<Margin> CalculateInitialMarginAsync(string tradeId, decimal marginPercentage = 0.10m);
    Task<Margin> CalculateVariationMarginAsync(string tradeId, decimal currentMarketPrice);
    Task<decimal> GetTotalMarginRequirementAsync(string tradeId);
}
