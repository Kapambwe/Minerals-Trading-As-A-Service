using Platform.Trading.Management.Models;

namespace Platform.Trading.Management.Services.Interfaces;

public interface ITradeService
{
    Task<IEnumerable<Trade>> GetAllTradesAsync();
    Task<Trade?> GetTradeByIdAsync(string id);
    Task<Trade> CreateTradeAsync(Trade trade);
    Task<Trade> UpdateTradeAsync(Trade trade);
    Task<bool> DeleteTradeAsync(string id);
    Task<Trade> NovateTradeAsync(string tradeId);
    Task<Trade> ConfirmTradeAsync(string tradeId);
}
