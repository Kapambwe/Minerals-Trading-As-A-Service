using MiningTradingMobileApp.Models;

namespace MiningTradingMobileApp.Services
{
    public interface ITradeService
    {
        Task<Trade?> GetTradeByIdAsync(string tradeId);
        Task<IEnumerable<Trade>> GetAllTradesAsync();
    }
}