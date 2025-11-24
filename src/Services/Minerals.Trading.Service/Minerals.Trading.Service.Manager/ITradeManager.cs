using Minerals.Trading.Service.Model;

namespace Minerals.Trading.Service.Manager;

public interface ITradeManager
{
    Task<IEnumerable<Trade>> GetAllTradesAsync();
    Task<Trade?> GetTradeByIdAsync(string id);
    Task<Trade> CreateTradeAsync(Trade trade);
    Task<Trade> UpdateTradeAsync(Trade trade);
    Task<bool> DeleteTradeAsync(string id);
    Task<Trade> NovateTradeAsync(string tradeId);
    Task<Trade> ConfirmTradeAsync(string tradeId);
    Task<Trade> CancelTradeAsync(string tradeId, string reason);
    Task<bool> ValidateTradeAsync(Trade trade);
    Task<IEnumerable<Trade>> GetTradesByStatusAsync(TradeStatus status);
}
