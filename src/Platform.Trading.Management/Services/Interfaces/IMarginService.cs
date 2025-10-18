using Platform.Trading.Management.Models;

namespace Platform.Trading.Management.Services.Interfaces;

public interface IMarginService
{
    Task<IEnumerable<Margin>> GetAllMarginsAsync();
    Task<Margin?> GetMarginByIdAsync(string id);
    Task<IEnumerable<Margin>> GetMarginsByTradeIdAsync(string tradeId);
    Task<Margin> CreateMarginAsync(Margin margin);
    Task<Margin> UpdateMarginAsync(Margin margin);
    Task<bool> DeleteMarginAsync(string id);
    Task<Margin> CalculateVariationMarginAsync(string tradeId, decimal currentMarketPrice);
}
