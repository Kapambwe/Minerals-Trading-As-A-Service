using MiningTradingMobileApp.Models;

namespace MiningTradingMobileApp.Services
{
    public interface IMarginRequestService
    {
        Task<IEnumerable<MarginRequest>> GetMarginRequestsForTradeAsync(string tradeId);
    }
}