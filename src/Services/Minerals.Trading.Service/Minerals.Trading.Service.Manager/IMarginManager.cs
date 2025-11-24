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
}
