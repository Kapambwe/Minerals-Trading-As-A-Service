using MiningTradingMobileApp.Models;

namespace MiningTradingMobileApp.Services
{
    public interface IMarginService
    {
        Task<IEnumerable<Margin>> GetMarginsByTradeIdAsync(string tradeId);
    }
}